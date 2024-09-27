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

Namespace Paginas.PJ

    Public Class AssinaDigital

        Inherits SantanaPage

        Public NOMEarquivo As String
        Dim strTipoRegistro As String

        Public strHEADER1 As String
        Public strHEADERcnpj As String
        Public strHEADER2 As String
        Public strHEADERdata As String
        Public strHEADER3 As String
        Public strHEADERfixo As String
        Public strHEADER4 As String

        Public strDET1 As String
        Public strDETfixo As String
        Public strDET2 As String

        Public ORDEM As String

        Public strTRAILER As String
        Dim strQuantRegisto As String


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)
            If Not IsPostBack Then
                BindGridView1Data()
            End If

        End Sub

        Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
            If FileUpload1.HasFile Then
                Try
                    Dim Arquivo = "C:\downloads\" & FileUpload1.FileName

                    FileUpload1.SaveAs(Arquivo)
                    Label1.Text = "File name: " & FileUpload1.PostedFile.FileName
                    NOMEarquivo = FileUpload1.PostedFile.FileName

                    Dim TextLine As String

                    If System.IO.File.Exists(Arquivo) = True Then

                        Dim objReader As New System.IO.StreamReader(Arquivo)
                        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                        Dim connection As New SqlConnection(strConn)

                        Dim command As SqlCommand
                        command = New SqlCommand("SCR_PJ_ASSINADIG_LIMPAR ", connection)

                        command.Connection.Open()
                        command.ExecuteReader()
                        command.Connection.Close()

                        Do While objReader.Peek() <> -1

                            TextLine = objReader.ReadLine()

                            strTipoRegistro = Mid(TextLine, 1, 1)

                            If (strTipoRegistro = "0") Then  ' HEADER
                                strHEADER1 = Mid(TextLine, 1, 26)
                                strHEADERcnpj = Mid(TextLine, 27, 20)
                                strHEADER2 = Mid(TextLine, 47, 94 - 46)
                                strHEADERdata = Right("00" & LTrim(DatePart(DateInterval.Day, Now)), 2) & Right("00" & LTrim(DatePart(DateInterval.Month, Now)), 2) & (DatePart(DateInterval.Year, Now) - 2000)
                                strHEADER3 = Mid(TextLine, 102, 109 - 101)
                                strHEADERfixo = "MX000100"
                                strHEADER4 = Mid(TextLine, 117, 400 - 116)


                                Dim cmd As New SqlCommand(
                                            "SELECT cednte_cedn 	FROM		FinSGRDBS..tb_CEDNTE O (NOLOCK) where cednte_cpfcgc ='" & Right(strHEADERcnpj, 15) & "'", connection)
                                cmd.Connection.Open()

                                Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

                                While dr.Read()
                                    strHEADERcnpj = CStr(dr.GetDecimal(0))
                                End While
                                strHEADERcnpj = Right("00000000000000000000" & strHEADERcnpj, 20)
                                cmd.Connection.Close()

                                strDET1 = Mid(TextLine, 1, 20)
                                strDETfixo = Mid(TextLine, 21, 26 - 20) & Left(strHEADERcnpj, 18)
                                strDET2 = Right(strHEADERcnpj, 2) & strHEADER2 & strHEADERdata & strHEADER3 & strHEADERfixo & strHEADER4
                                ORDEM = Mid(TextLine, 395, 6)
                                command = New SqlCommand(
                                                "SCR_PJ_ASSINADIG_GRAVAR '" +
                                                strDET1 + "', '" +
                                                strDETfixo + "', '" +
                                                strDET2 + "', '" +
                                                ORDEM + "' ", connection)

                                command.Connection.Open()
                                command.ExecuteReader()
                                command.Connection.Close()

                            End If

                            If (strTipoRegistro = "1") Then     ' DETALHE
                                strDET1 = Mid(TextLine, 1, 20)
                                strDETfixo = "00110000000000000000000"
                                strDET2 = Mid(TextLine, 45, 401 - 44)
                                ORDEM = Mid(TextLine, 395, 6)

                                strQuantRegisto = Mid(TextLine, 395, 6)  ' INFORMAR NA TELA

                                command = New SqlCommand(
                                                "SCR_PJ_ASSINADIG_GRAVAR '" +
                                                strDET1 + "', '" +
                                                strDETfixo + "', '" +
                                                strDET2 + "', '" +
                                                ORDEM + "' ", connection)

                                command.Connection.Open()
                                command.ExecuteReader()
                                command.Connection.Close()

                            End If

                            If (strTipoRegistro = "9") Then     'TRAILER

                                strTRAILER = Mid(TextLine, 1, 400)

                                strDET1 = Mid(TextLine, 1, 20)
                                strDETfixo = Mid(TextLine, 21, 45 - 20)
                                strDET2 = Mid(TextLine, 45, 400 - 44)
                                ORDEM = Mid(TextLine, 395, 6)

                                command = New SqlCommand(
                                                "SCR_PJ_ASSINADIG_GRAVAR '" +
                                                strDET1 + "', '" +
                                                strDETfixo + "', '" +
                                                strDET2 + "', '" +
                                                ORDEM + "' ", connection)

                                command.Connection.Open()
                                command.ExecuteReader()
                                command.Connection.Close()


                            End If

                        Loop

                        connection.Close()
                        objReader.Close()

                    Else

                        Label1.Text = "Ocorreu erro lendo arquivo"

                    End If
                    Label1.Text = "Concluido com Sucesso! " + strQuantRegisto + " Registros Importados!"
                    BindGridView1Data()


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

                        e.Row.Cells(0).Text = drow("strDET1")
                        e.Row.Cells(1).Text = drow("strDETfixo")
                        e.Row.Cells(2).Text = drow("strDET2")

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
            table = Util.ClassBD.GetExibirGrid("[SCR_PJ_ASSINADIG] ", "CRUZAMENTOCETIP", strConn)

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
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Assinatura Digital' ,'Ler arquivos em padrão CNAB Itaú e Exportar o arquivo Texto para o formato do WBA.');", True)
        End Sub


        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../Menu.aspx")
        End Sub

        Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
            Try

                BindGridView1Data()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try

        End Sub
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
                    Dim filename As String = NOMEarquivo & "T.txt"
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename)
                    Response.ContentEncoding = System.Text.Encoding.Default
                    Response.Charset = ""
                    Response.ContentType = "application/octet-stream"

                    Using sw As New StringWriter()

                        For Each row As GridViewRow In objGrid.Rows
                            If row.Cells(0).Text = "1" Then
                                Response.Write("1                  ")
                            Else
                                Response.Write(row.Cells(0).Text)
                            End If
                            Response.Write(row.Cells(1).Text)
                            Response.Write(row.Cells(2).Text)
                            Response.Write(vbCrLf)
                        Next

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