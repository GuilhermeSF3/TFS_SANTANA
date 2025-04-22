Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Runtime.InteropServices.ComTypes
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Componentes
Imports Microsoft.VisualBasic
Imports Santana.Seguranca
Imports Util

Namespace Paginas.TI


    Public Class RelatorioDespesas

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                Pesquisar_Relatorio(Nothing, Nothing)
            End If

            ' Ativa o selectpicker, se necessário
            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

            ' Controla a exibição dos campos de data
            AtualizarVisibilidadeCampos()
        End Sub


        Protected Sub ddlFiltros_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            AtualizarVisibilidadeCampos()
        End Sub

        Private Sub AtualizarVisibilidadeCampos()
            If ddlFiltros.SelectedValue = "5" Or ddlFiltros.SelectedValue = "6" Then
                txtFiltro.Visible = False
                campoDataPagamento.Style("display") = "flex"
            Else
                txtFiltro.Visible = True
                campoDataPagamento.Style("display") = "none"
            End If
        End Sub


        Protected Sub Pesquisar_Relatorio(sender As Object, e As EventArgs)

            Dim IdFiltro As Int32 = 0 ' ou outro valor padrão

            If ddlFiltros.SelectedItem.Value <> "" Then
                IdFiltro = Convert.ToInt32(ddlFiltros.SelectedItem.Value)
            End If

            Dim dataDe As DateTime? = Nothing
            Dim dataAte As DateTime? = Nothing

            If Not String.IsNullOrWhiteSpace(txtDataDe.Text) Then
                dataDe = Convert.ToDateTime(txtDataDe.Text)
            End If

            If Not String.IsNullOrWhiteSpace(txtDataAte.Text) Then
                dataAte = Convert.ToDateTime(txtDataAte.Text)
            End If

            Dim filtro As String = txtFiltro.Text
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

            Using conn As New SqlConnection(strConn)
                conn.Open()

                ' Recomendado: usar parâmetros em vez de interpolação
                Dim sql As String = "EXEC SCR_SIG_RELAT_DESPESA @IdFiltro, @Filtro, @txtDataDe, @txtDataAte"

                Using cmd As New SqlCommand(sql, conn)

                    cmd.Parameters.AddWithValue("@IdFiltro", IdFiltro)
                    cmd.Parameters.AddWithValue("@Filtro", filtro)
                    cmd.Parameters.AddWithValue("@txtDataDe", If(dataDe.HasValue, dataDe, DBNull.Value))
                    cmd.Parameters.AddWithValue("@txtDataAte", If(dataAte.HasValue, dataAte, DBNull.Value))

                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)

                    Dim html As New StringBuilder()

                    For Each row As DataRow In dt.Rows

                        html.Append("<tr>")
                        html.Append($"<td style='text-align:center;'>{row("DATA_DA_AGENDA")}</td>")
                        html.Append($"<td style='text-align:center;'>{row("EMPRESA")}</td>")
                        html.Append($"<td style='text-align:center;'>{row("DEPARTAMENTO")}</td>")
                        html.Append($"<td style='text-align:center;'>{row("DIGITADOR")}</td>")
                        html.Append($"<td style='text-align:center;'>{row("FAVORECIDO")}</td>")
                        html.Append($"<td style='text-align:center;'>{row("CPF_CNPJ")}</td>")
                        html.Append($"<td style='text-align:center;'>{row("HISTORICO")}</td>")
                        html.Append($"<td style='text-align:center;'>{row("VALOR_BRUTO")}</td>")
                        html.Append($"<td style='text-align:center;'>{row("VALOR_LIQUIDO")}</td>")
                        html.Append($"<td style='text-align:center;'>{row("DATA_PAGAMENTO")}</td>")
                        html.Append($"<td style='text-align:center;'>{row("FORMA_DE_PAGAMENTO")}</td>")
                        html.Append($"<td style='text-align:center;'>{row("BANCO")}</td>")
                        html.Append($"<td style='text-align:center;'>{row("AGENCIA")}</td>")
                        html.Append($"<td style='text-align:center;'>{row("CONTA_CORRENTE")}</td>")
                        html.Append($"<td style='text-align:center;'>{row("STATUS")}</td>")
                        html.Append("</tr>")
                    Next

                    html.Append("</tbody>")
                    html.Append("</table>")

                    ' Joga o HTML no literal
                    litTabela.Text = html.ToString()
                End Using
            End Using
        End Sub

        Protected Sub Exportar_Excel(sender As Object, e As EventArgs)
            Dim IdFiltro As Int32 = 0

            If ddlFiltros.SelectedItem.Value <> "" Then
                IdFiltro = Convert.ToInt32(ddlFiltros.SelectedItem.Value)
            End If

            Dim dataDe As DateTime? = Nothing
            Dim dataAte As DateTime? = Nothing

            If Not String.IsNullOrWhiteSpace(txtDataDe.Text) Then
                dataDe = Convert.ToDateTime(txtDataDe.Text)
            End If

            If Not String.IsNullOrWhiteSpace(txtDataAte.Text) Then
                dataAte = Convert.ToDateTime(txtDataAte.Text)
            End If

            Dim filtro As String = txtFiltro.Text
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

            Using conn As New SqlConnection(strConn)
                conn.Open()

                Dim sql As String = "EXEC SCR_SIG_RELAT_DESPESA @IdFiltro, @Filtro, @txtDataDe, @txtDataAte"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@IdFiltro", IdFiltro)
                    cmd.Parameters.AddWithValue("@Filtro", filtro)
                    cmd.Parameters.AddWithValue("@txtDataDe", If(dataDe.HasValue, dataDe, DBNull.Value))
                    cmd.Parameters.AddWithValue("@txtDataAte", If(dataAte.HasValue, dataAte, DBNull.Value))

                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)

                    Dim sw As New StringWriter()
                    Dim htw As New HtmlTextWriter(sw)
                    Dim grid As New GridView()

                    grid.DataSource = dt
                    grid.DataBind()

                    ' Exporta como Excel
                    Response.Clear()
                    Response.Buffer = True
                    Response.AddHeader("content-disposition", "attachment;filename=RelatorioDespesas.xls")
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.ms-excel"
                    Response.ContentEncoding = Encoding.UTF8

                    ' Evita erro de renderização
                    Dim frm As New HtmlForm()
                    frm.Controls.Add(grid)
                    Page.Controls.Add(frm)

                    frm.RenderControl(htw)
                    Response.Write(sw.ToString())
                    Response.End()
                End Using
            End Using
        End Sub


        Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../Menu.aspx")
        End Sub

    End Class
End Namespace