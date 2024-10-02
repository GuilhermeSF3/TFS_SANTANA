Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic
Imports Santana.Seguranca
Imports Util

Namespace Paginas.FundoQuata

    Public Class OrdinarioCnabGerar_O

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then
                Dim today As DateTime = DateTime.Now
                Dim previousDate As DateTime
                txtData.Text = Now.ToString("dd/MM/yyyy")
            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub

        Protected Sub GridViewRiscoAnalitico_RowCreated(sender As Object, e As GridViewRowEventArgs)
            Try


                If e.Row.RowType = DataControlRowType.DataRow And e.Row.RowState = DataControlRowState.Normal Then
                    e.Row.CssClass = "GridviewScrollC3Item"
                End If
                If e.Row.RowType = DataControlRowType.DataRow And e.Row.RowState = DataControlRowState.Alternate Then
                    e.Row.CssClass = "GridviewScrollC3Item2"
                End If


            Catch ex As Exception

            End Try
        End Sub


        Public Property DataGridView As DataTable
            Get
                If ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") Is Nothing Then

                    Dim dataReferencia As DateTime
                    If DateTime.TryParse(txtData.Text, dataReferencia) Then
                        ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = GetData(dataReferencia)
                    Else
                        Throw New Exception("Data inválida no ViewState")
                    End If
                End If

                Return DirectCast(ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C"), DataTable)
            End Get
            Set(value As DataTable)
                ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = value
            End Set
        End Property

        Private Sub GravarLogExecucao(usuario As String, dataReferencia As String)
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim observ As String = $"Cnab Gerado com sucesso e exportado"
            Dim acao As String = "Ordinario - Cnab - Gerar"

            Try
                Using con As New SqlConnection(strConn)
                    Using cmd As New SqlCommand("INSERT INTO LogCnabOrdinario (Usuario, DataReferencia, DataExecucao, Observacao, Acao) VALUES (@Usuario, @DataReferencia, @DataExecucao, @Observacao, @Acao)", con)
                        cmd.Parameters.AddWithValue("@Usuario", usuario)
                        cmd.Parameters.AddWithValue("@DataReferencia", dataReferencia)
                        cmd.Parameters.AddWithValue("@DataExecucao", DateTime.Now)
                        cmd.Parameters.AddWithValue("@Observacao", observ) '
                        cmd.Parameters.AddWithValue("@Acao", acao)

                        con.Open()
                        cmd.ExecuteNonQuery()
                    End Using
                End Using



            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Erro', 'Ocorreu um erro ao salvar o log.');", True)
            End Try
        End Sub



        Protected Sub BindGridView1Data(sender As Object, e As EventArgs)

            Dim dataReferencia As DateTime
            Dim dataStr As String = txtData.Text.Trim()
            If DateTime.TryParseExact(dataStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, dataReferencia) Then
                Dim dataFormatada As String = dataReferencia.ToString("yyyyMMdd")
                Dim resultTable As DataTable = GetData(dataFormatada)

                If resultTable IsNot Nothing AndAlso resultTable.Rows.Count > 0 Then
                    ExportarParaTXT(resultTable)
                Else

                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Sem Resposta', 'Nenhum arquivo encontrado na data informada.');", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Data inválida!', 'Por favor, forneça uma data válida.');", True)
            End If
        End Sub





        Private Function GetData(dataReferencia As String) As DataTable
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim resultTable As New DataTable()
            Dim usuarioLogado As String = ContextoWeb.UsuarioLogado.Login
            Using con As New SqlConnection(strConn)
                Using cmd As New SqlCommand($"EXEC SCR_CNAB550_GERAR_O '{dataReferencia}'", con)
                    cmd.CommandType = CommandType.Text
                    con.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        resultTable.Load(reader)
                    End Using
                End Using
            End Using
            If resultTable.Rows.Count > 0 AndAlso resultTable.Rows(0)(0).ToString().Contains("Sem movimentação para gerar o CNAB550 - BAIXAS") Then

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Sem movimentação para gerar o CNAB550 - BAIXAS');", True)

                Return Nothing
                GravarLogExecucao(usuarioLogado, dataReferencia)
            End If
            Return resultTable
        End Function

        Private Sub ExportarParaTXT(dt As DataTable)
            Try
                Dim nomeArquivo As String = "CB" & DateTime.Now.ToString("ddMM") & "01" & "_O" & ".rem"
                Response.Clear()
                Response.Buffer = True
                Response.Charset = ""
                Response.ContentType = "application/octet-stream"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & nomeArquivo)

                Using sw As New StringWriter()
                    For Each row As DataRow In dt.Rows
                        Dim linha As String = ""
                        For Each coluna As DataColumn In dt.Columns
                            Dim valor As String = row(coluna.ColumnName).ToString().PadRight(10)
                            linha &= valor
                        Next
                        linha = linha.PadRight(550).Substring(0, 550)
                        sw.WriteLine(linha)
                    Next

                    Response.Output.Write(sw.ToString())
                End Using
                Response.Flush()
                Response.Close()
            Catch ex As Exception
                Debug.WriteLine("Exceção: " & ex.Message)
                Debug.WriteLine("Stack Trace: " & ex.StackTrace)
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Erro', 'Ocorreu um erro ao gerar o arquivo.');", True)
            End Try
        End Sub





        Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)


            'ContextoWeb.DadosTransferencia.CodAgente = ddlAnalista.SelectedValue
            'ContextoWeb.DadosTransferencia.Agente = ddlAnalista.SelectedItem.ToString()

            'ContextoWeb.DadosTransferencia.CodCobradora = ddlProduto.SelectedValue
            'ContextoWeb.DadosTransferencia.Cobradora = ddlProduto.SelectedItem.ToString()


            'Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

            'Dim ds As dsRollrateMensal
            'Dim cmd As New SqlCommand("[scr_RR_mensal] '" & Convert.ToDateTime(txtData.Text).ToString("MM/dd/yy") & "', '" & ddlAnalista.SelectedValue & "','" & ddlProduto.SelectedValue & "'")
            'Using con As New SqlConnection(strConn)
            '    Using sda As New SqlDataAdapter()
            '        cmd.Connection = con
            '        sda.SelectCommand = cmd
            '        ds = New dsRollrateMensal()
            '        sda.Fill(ds, "RR_ROLLRATE_RPT")
            '    End Using
            'End Using

            '' ContextoWeb.NewReportContext()
            'ContextoWeb.Relatorio.reportFileName = "~/Relatorios/rptRollrateMensal.rpt"
            'ContextoWeb.Relatorio.reportDatas.Add(New reportData(ds))

            'ContextoWeb.Navegacao.LinkPaginaAnteriorRelatorio = Me.AppRelativeVirtualPath
            '' ContextoWeb.Navegacao.TituloPaginaAtual = Me.Title
            'Response.Redirect("Relatorio.aspx")

            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../Menu.aspx")
        End Sub



        Public Overrides Sub VerifyRenderingInServerForm(control As Control)
            'Not Remove
            ' Confirms that an HtmlForm control is rendered for the specified ASP.NET
            '     server control at run time. 

        End Sub




        Private Function FormatValueForFirstColumn(value As String) As String
            Dim number As Decimal
            If Decimal.TryParse(value, number) Then
                Return number.ToString("F2")
            End If
            Return value
        End Function


        Protected Sub hfGridView1SV_ValueChanged(sender As Object, e As EventArgs)
            Session(HfGridView1Svid) = hfGridView1SV.Value
        End Sub

        Protected Sub hfGridView1SH_ValueChanged(sender As Object, e As EventArgs)
            Session(HfGridView1Shid) = hfGridView1SH.Value
        End Sub





    End Class
End Namespace