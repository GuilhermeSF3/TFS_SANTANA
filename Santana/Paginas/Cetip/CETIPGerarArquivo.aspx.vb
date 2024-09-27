Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic
Imports Santana.Seguranca
Imports Util

Namespace Paginas.Credito

    Public Class CETIPGerarArquivo

        Inherits SantanaPage

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                Dim UltimoDiaSemana As Date = Convert.ToDateTime(Now.Date.AddDays(-7).ToString("dd/MM/yyyy"))
                txtDataDe.Text = UltimoDiaSemana

                txtDataAte.Text = (DateTime.Now.AddDays(-1)).ToString("dd/MM/yyyy")


                If ContextoWeb.DadosTransferencia.CodAnalista <> 0 Then
                    BindGridView1Data()
                    ContextoWeb.DadosTransferencia.CodAnalista = 0
                End If

            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub


        Protected Sub btnDataAnterior1_Click(sender As Object, e As EventArgs)

            Dim DataAntes As Date = Convert.ToDateTime(txtDataDe.Text)
            DataAntes = DataAntes.AddDays(-1)

            txtDataDe.Text = DataAntes

        End Sub


        Protected Sub btnProximaData1_Click(sender As Object, e As EventArgs)

            Dim DataAntes As Date = Convert.ToDateTime(txtDataDe.Text)
            DataAntes = DataAntes.AddDays(1)

            txtDataDe.Text = DataAntes


        End Sub


        Protected Sub btnDataAnterior2_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataAte.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)

            txtDataAte.Text = UltimoDiaMesAnterior

        End Sub


        Protected Sub btnProximaData2_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataAte.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(1)

            txtDataAte.Text = UltimoDiaMesAnterior


        End Sub




        Private Sub GridViewRiscoAnalitico_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewRiscoAnalitico.RowDataBound

            Try

                If e.Row.RowType = DataControlRowType.DataRow Then

                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row


                        'If IsDBNull(drow("TipoDocumento")) Then
                        'e.Row.Cells(0).Text = ""
                        'Else
                        e.Row.Cells(0).Text = drow("TipoDocumento")
                        'End If

                        'If IsDBNull(drow("Documento")) Then
                        'e.Row.Cells(1).Text = ""
                        'Else
                        e.Row.Cells(1).Text = drow("Documento")
                        'End If


                        '   If IsDBNull(drow("CNPJLoja")) Then
                        'e.Row.Cells(2).Text = ""
                        'Else
                        e.Row.Cells(2).Text = drow("CNPJLoja")
                        'End If

                        'If IsDBNull(drow("ValorAprovado")) OrElse drow("ValorAprovado") = 0 Then
                        'e.Row.Cells(3).Text = ""
                        'Else
                        e.Row.Cells(3).Text = drow("ValorAprovado")
                        'End If

                        'If IsDBNull(drow("DataAprovacao")) Then
                        'e.Row.Cells(4).Text = ""
                        'Else
                        e.Row.Cells(4).Text = drow("DataAprovacao")
                        'End If

                    End If
                End If


            Catch ex As Exception

            End Try
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
                    ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = GetData()
                End If

                Return DirectCast(ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C"), DataTable)
            End Get
            Set(value As DataTable)
                ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = value
            End Set
        End Property



        Protected Sub BindGridView1Data()

            GridViewRiscoAnalitico.DataSource = GetData()
            GridViewRiscoAnalitico.DataBind()
            GridViewRiscoAnalitico.AllowPaging = "True"
        End Sub

        Protected Sub BindGridView1DataView()

            GridViewRiscoAnalitico.DataSource = DataGridView
            GridViewRiscoAnalitico.DataBind()

        End Sub


        Private Function GetData() As DataTable

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim table As DataTable

            table = Util.ClassBD.GetExibirGrid("[SCR_CETIP_GERARARQUIVO] '" & Right(txtDataDe.Text, 4) & Mid(txtDataDe.Text, 4, 2) & Left(txtDataDe.Text, 2) & _
                                                                "', '" & Right(txtDataAte.Text, 4) & Mid(txtDataAte.Text, 4, 2) & Left(txtDataAte.Text, 2) & _
                                                                "'", "GERARARQUIVO", strConn)

            Return table

        End Function


        Protected Sub GridViewRiscoAnalitico_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
            GridViewRiscoAnalitico.PageIndex = IIf(e.NewPageIndex < 0, 0, e.NewPageIndex)
            BindGridView1DataView()
        End Sub




        Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../Menu.aspx")
        End Sub

        ' carregar Safra analitica
        Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
            Try

                BindGridView1Data()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try

        End Sub
        ' carregar Producao analitica
        Protected Sub btnCarregarProd_Click(sender As Object, e As EventArgs)
            Try

                BindGridView1Data()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try

        End Sub


        Public Overrides Sub VerifyRenderingInServerForm(control As Control)
            'Not Remove
            ' Confirms that an HtmlForm control is rendered for the specified ASP.NET
            '     server control at run time. 

        End Sub


        Protected Sub btnExcel_Click(sender As Object, e As EventArgs)

            Try
                GridViewRiscoAnalitico.AllowPaging = False
                BindGridView1Data()
                ExportExcel(GridViewRiscoAnalitico)

            Catch ex As Exception
                Throw ex
            End Try

        End Sub


        Private Sub ExportExcel(objGrid As GridView)


            Try

                If Not IsNothing(objGrid.HeaderRow) Then

                    Response.Clear()
                    Response.Buffer = True
                    Dim filename As String = String.Format("CETIP_{0}_{1}_{2}.txt", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename)
                    Response.ContentEncoding = System.Text.Encoding.Default
                    Response.Charset = ""
                    Response.ContentType = "application/octet-stream"

                    Using sw As New StringWriter()
                        'Const codUsuario = "000000000000000"
                        'Const subCodigo = "0000"
                        'Const login As String = "        "
                        'Const login As String = "00000000"  ' ALTERADO RAQUEL 18/9/15

                        'Response.Write("HEGRME00001" + codUsuario + subCodigo + login + Date.Now.ToString("yyyyMMddhhmmss"))
                        '              'HEGRME 0000100000000000142000010001ADCS20310815101312
                        Response.Write("HEGRME 0000100000000000142000010001ADCS20" + Date.Now.ToString("ddMMyyhhmmss"))

                        '               HEGRME 0000100000000000142000010001ADCS20180915041918
                        Response.Write(vbCrLf)

                        For Each row As GridViewRow In objGrid.Rows
                            Response.Write("D")
                            Response.Write(row.Cells(0).Text)
                            Response.Write(row.Cells(1).Text)
                            Response.Write(row.Cells(2).Text)
                            Response.Write(row.Cells(3).Text)
                            Response.Write(row.Cells(4).Text)
                            Response.Write(vbCrLf)
                        Next

                        Response.Write("T" + Right("000000" + objGrid.Rows.Count.ToString(), 6))
                        Response.Write(vbCrLf)
                        Response.Output.Write(sw.ToString())
                        HttpContext.Current.Response.Flush()
                        HttpContext.Current.Response.SuppressContent = True
                        HttpContext.Current.ApplicationInstance.CompleteRequest()
                    End Using

                End If

            Catch ex As Exception
                Throw ex
            End Try



        End Sub


        Protected Sub GridViewRiscoAnalitico_DataBound(sender As Object, e As EventArgs)
            Dim gridView As GridView = CType(sender, GridView)
            If Not IsNothing(gridView.DataSource) AndAlso CType(gridView.DataSource, System.Data.DataTable).Rows.Count > 0 Then
                Util.CWebPageUtil.PreencheListaDePaginas(gridView, CType(gridView.BottomPagerRow.Cells(0).FindControl("PagerPages"), DropDownList))
            End If
        End Sub

        Protected Sub PagerPages_SelectedIndexChanged(sender As Object, e As EventArgs)

            GridViewRiscoAnalitico.DataSource = DataGridView()
            GridViewRiscoAnalitico.PageIndex = CType(sender, DropDownList).SelectedIndex
            GridViewRiscoAnalitico.DataBind()

        End Sub

    End Class
End Namespace