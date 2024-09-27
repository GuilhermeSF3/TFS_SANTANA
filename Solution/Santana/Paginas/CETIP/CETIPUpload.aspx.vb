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

    Public Class CETIPUpload

        Inherits SantanaPage

        Dim strTipoRegistro As String
        Dim strTipoDocumento As String
        Dim strDocumento As String
        Dim strCNPJLoja As String
        Dim strValorAprovado As String
        Dim strDataAprovacao As String
        Dim strDataInclusao As String
        Dim strDataContrato As String
        Dim strMarca As String
        Dim strAnoModelo As String
        Dim strAnoFabricacao As String
        Dim strPrazoOperacao As String
        Dim strCodigoFIPE As String
        Dim strValorFIPE As String
        Dim strValorFaixaAprovado As String
        Dim strCNPJAgente As String
        Dim strNomeAgente As String
        Dim strPagoLoja As String

        Dim strTipoArquivo As String
        Dim strCodigoSistema As String
        Dim strCodigoServico As String
        Dim strDataProcessamento As String

        Dim strQuantRegisto As String

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


            If Not IsPostBack Then


            End If
            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub

        Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
            If FileUpload1.HasFile Then
                Try
                    Dim Arquivo = "C:\downloads\" & FileUpload1.FileName


                    FileUpload1.SaveAs(Arquivo)
                    Label1.Text = "File name: " & FileUpload1.PostedFile.FileName

                    Dim TextLine As String

                    If System.IO.File.Exists(Arquivo) = True Then

                        Dim objReader As New System.IO.StreamReader(Arquivo)
                        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                        Dim connection As New SqlConnection(strConn)

                        Dim command As SqlCommand

                        Do While objReader.Peek() <> -1

                            TextLine = objReader.ReadLine()

                            strTipoRegistro = Mid(TextLine, 1, 1)

                            If (strTipoRegistro = "H") Then
                                strTipoArquivo = Mid(TextLine, 2, 1)
                                strCodigoSistema = Mid(TextLine, 3, 5)
                                strCodigoServico = Mid(TextLine, 8, 5)
                                strDataProcessamento = formataData(Mid(TextLine, 13, 14))
                            End If

                            If (strTipoRegistro = "D") Then
                                strTipoDocumento = formataTexto(Mid(TextLine, 2, 1))
                                strDocumento = formataTexto(Mid(TextLine, 3, 14))
                                strCNPJLoja = formataValor(Mid(TextLine, 17, 14), 0)
                                strValorAprovado = formataValor(Mid(TextLine, 31, 11), 2)

                                strDataAprovacao = formataData(Mid(TextLine, 42, 14))
                                strDataInclusao = formataData(Mid(TextLine, 56, 8))
                                strDataContrato = formataData(Mid(TextLine, 64, 8))


                                strMarca = formataTexto(Mid(TextLine, 72, 50))
                                strAnoModelo = formataValor(Mid(TextLine, 122, 4), 0)
                                strAnoFabricacao = formataValor(Mid(TextLine, 126, 4), 0)
                                strPrazoOperacao = formataValor(Mid(TextLine, 130, 3), 0)
                                strCodigoFIPE = formataTexto(Mid(TextLine, 133, 10))
                                strValorFIPE = formataValor(Mid(TextLine, 143, 11), 2)
                                strValorFaixaAprovado = formataValor(Mid(TextLine, 154, 5), 0)
                                strCNPJAgente = formataValor(Mid(TextLine, 159, 14), 0)
                                strNomeAgente = formataTexto(Mid(TextLine, 173, 50))
                                strPagoLoja = formataTexto(Mid(TextLine, 223, 1))

                                command = New SqlCommand(
                                "SCR_CETIP_UPLOAD " +
                                    strDataProcessamento + ", " +
                                    strTipoDocumento + ", " +
                                    strDocumento + ", " +
                                    strCNPJLoja + ", " +
                                    strValorAprovado + ", " +
                                    strDataAprovacao + ", " +
                                    strDataInclusao + ", " +
                                    strDataContrato + ", " +
                                    strMarca + ", " +
                                    strAnoModelo + ", " +
                                    strAnoFabricacao + ", " +
                                    strPrazoOperacao + ", " +
                                    strCodigoFIPE + ", " +
                                    strValorFIPE + ", " +
                                    strValorFaixaAprovado + ", " +
                                    strCNPJAgente + ", " +
                                    strNomeAgente + ", " +
                                    strPagoLoja, connection)

                                command.Connection.Open()
                                command.ExecuteReader()
                                command.Connection.Close()

                            End If

                            If (strTipoRegistro = "T") Then

                                strQuantRegisto = Mid(TextLine, 2, 6)
                            End If

                        Loop

                        connection.Close()
                        objReader.Close()

                    Else

                        Label1.Text = "Ocorreu erro lendo arquivo"

                    End If
                    Label1.Text = "Concluido com Sucesso! " + strQuantRegisto + " Registros Importados!"


                Catch ex As Exception
                    Label1.Text = "ERROR: " & ex.Message.ToString()
                End Try

            Else
                Label1.Text = "Necessário selecionar um arquivo."
            End If



        End Sub


        Private Sub GridViewRiscoAnalitico_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewRiscoAnalitico.RowDataBound

            Try

                If e.Row.RowType = DataControlRowType.DataRow Then

                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row


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
                    Dim filename As String = String.Format("Cruzamento_CETIP_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename)
                    Response.ContentEncoding = System.Text.Encoding.Default
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.ms-excel"

                    Using sw As New StringWriter()

                        Dim hw As New HtmlTextWriter(sw)

                        objGrid.HeaderRow.BackColor = Color.White
                        For Each cell As TableCell In objGrid.HeaderRow.Cells
                            cell.CssClass = "GridviewScrollC3Header"
                        Next
                        For Each row As GridViewRow In objGrid.Rows
                            row.BackColor = Color.White
                            For Each cell As TableCell In row.Cells
                                If row.RowIndex Mod 2 = 0 Then
                                    cell.CssClass = "GridviewScrollC3Item"
                                Else
                                    cell.CssClass = "GridviewScrollC3Item2"
                                End If

                                Dim controls As New List(Of Control)()
                                For Each control As Control In cell.Controls
                                    controls.Add(control)
                                Next

                                For Each control As Control In controls
                                    Select Case control.GetType().Name
                                        Case "HyperLink"
                                            cell.Controls.Add(New Literal() With { _
                                                                 .Text = TryCast(control, HyperLink).Text _
                                                                 })
                                            Exit Select
                                        Case "TextBox"
                                            cell.Controls.Add(New Literal() With { _
                                                                 .Text = TryCast(control, TextBox).Text _
                                                                 })
                                            Exit Select
                                        Case "LinkButton"
                                            cell.Controls.Add(New Literal() With { _
                                                                 .Text = TryCast(control, LinkButton).Text _
                                                                 })
                                            Exit Select
                                        Case "CheckBox"
                                            cell.Controls.Add(New Literal() With { _
                                                                 .Text = TryCast(control, CheckBox).Text _
                                                                 })
                                            Exit Select
                                        Case "RadioButton"
                                            cell.Controls.Add(New Literal() With { _
                                                                 .Text = TryCast(control, RadioButton).Text _
                                                                 })
                                            Exit Select
                                    End Select
                                    cell.Controls.Remove(control)
                                Next
                            Next
                        Next

                        objGrid.RenderControl(hw)


                        Dim fi As FileInfo = New FileInfo(Server.MapPath("~/css/GridviewScroll.css"))
                        Dim sb As New System.Text.StringBuilder
                        Dim sr As StreamReader = fi.OpenText()
                        Do While sr.Peek() >= 0
                            sb.Append(sr.ReadLine())
                        Loop
                        sr.Close()

                        Dim style As String = "<html><head><style type='text/css'>" & sb.ToString() & "</style><head>"
                        Response.Write(style)
                        Response.Write("Relatório Inadiplencia por Analista")
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

        Protected Sub GridViewRiscoAnalitico_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridViewRiscoAnalitico.SelectedIndexChanged

        End Sub

        Private Function formataData(Data As String) As String
            Dim retorno As String
            retorno = "'" + Mid(Data, 1, 4) + "-" + Mid(Data, 5, 2) + "-" + Mid(Data, 7, 2) + "'"
            If retorno = "'0000-00-00'" Then
                retorno = "'1753-01-01'"
            End If
            Return retorno
        End Function

        Private Function formataTexto(Texto As String) As String
            Dim retorno As String
            retorno = "'" + Texto.Replace("'", "''").Trim() + "'"
            Return retorno
        End Function

        Private Function formataValor(Valor As String, Decimais As Integer) As String
            Dim retorno As String

            If Decimais = 0 Then
                retorno = Valor
            Else
                retorno = Left(Valor, Len(Valor) - Decimais) + "." + Right(Valor, Decimais)
            End If

            If retorno.Trim() = "" Then
                retorno = "0"
            End If

            Return retorno
        End Function

    End Class
End Namespace