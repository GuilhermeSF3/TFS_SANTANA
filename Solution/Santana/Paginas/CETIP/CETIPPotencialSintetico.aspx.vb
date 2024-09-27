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

Namespace Paginas.Cetip

    Public Class CETIPPotencialSintetico

        Inherits SantanaPage



        Dim strCNPJ As String
        Dim strClassificacao As String
        Dim strRazaoSocial As String
        Dim strNomeFantasia As String
        Dim strCNAE As String
        Dim strEndereco As String
        Dim strComplemento As String
        Dim strNumero As String
        Dim strBairro As String
        Dim strUF As String
        Dim strCidade As String
        Dim strCEP As String
        Dim strQuantidade As String
        Dim strVolume As String
        Dim strMarketShareQuant As String
        Dim strMarketShareValor As String
        Dim strMarketShareAcimaQuant As String
        Dim strMarketShareAcimaValor As String
        Dim strMarketShareAbaixoQuant As String
        Dim strMarketShareAbaxoValor As String

        Dim strTipoRegistro As String
        Dim strQuantRegisto As String


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
                txtDataDe.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)
            BindGridView1Data()

        End Sub


        Protected Sub btnDataAnterior1_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataDe.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(-1)

            If UltimoDiaMesAnterior.Year = Now.Date.Year Then
                If UltimoDiaMesAnterior.Month <= Now.Date.Month Then
                    txtDataDe.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
                End If
            ElseIf UltimoDiaMesAnterior.Year < Now.Date.Year Then
                txtDataDe.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
            End If

        End Sub


        Protected Sub btnProximaData1_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataDe.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(1)

            If UltimoDiaMesAnterior.Year = Now.Date.Year Then
                If UltimoDiaMesAnterior.Month <= Now.Date.Month Then
                    txtDataDe.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
                End If
            ElseIf UltimoDiaMesAnterior.Year < Now.Date.Year Then
                txtDataDe.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
            End If

        End Sub

        Private Function UltimoDiaUtilMesAnterior(Data As Date) As String

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Data.ToString("MM/yyyy"))

            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(1)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)

            If UltimoDiaMesAnterior <= Convert.ToDateTime("01/aug/2014") Then

                If (UltimoDiaMesAnterior.DayOfWeek = DayOfWeek.Sunday) Then
                    UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-2)
                ElseIf (UltimoDiaMesAnterior.DayOfWeek = DayOfWeek.Saturday) Then
                    UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)
                End If
            End If
            Return UltimoDiaMesAnterior.ToString("dd/MM/yyyy")

        End Function



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

                        Dim strMesReferencia As String

                        strMesReferencia = formataData(txtDataDe.Text)

                        Do While objReader.Peek() <> -1

                            TextLine = objReader.ReadLine()

                            strTipoRegistro = Mid(TextLine, 1, 1)

                            If (strTipoRegistro = "H") Then
                                command = New SqlCommand( _
                                "SCR_CETIP_POTENCIAL_SINTETICO_LIMPAR " + strMesReferencia, connection)

                                command.Connection.Open()
                                command.ExecuteReader()
                                command.Connection.Close()
                            End If

                            If (strTipoRegistro = "D") Then

                                strCNPJ = formataTexto(Mid(TextLine, 2, 14))
                                strClassificacao = formataTexto(Mid(TextLine, 16, 1))
                                strRazaoSocial = formataTexto(Mid(TextLine, 17, 150))
                                strNomeFantasia = formataTexto(Mid(TextLine, 167, 55))
                                strCNAE = formataTexto(Mid(TextLine, 222, 174))
                                strEndereco = formataTexto(Mid(TextLine, 396, 64))
                                strComplemento = formataTexto(Mid(TextLine, 460, 137))
                                strNumero = formataTexto(Mid(TextLine, 597, 10))
                                strBairro = formataTexto(Mid(TextLine, 607, 50))
                                strUF = formataTexto(Mid(TextLine, 657, 2))
                                strCidade = formataTexto(Mid(TextLine, 659, 32))
                                strCEP = formataTexto(Mid(TextLine, 691, 8))


                                strQuantidade = formataValor(Mid(TextLine, 699, 9), 0)
                                strVolume = formataValor(Mid(TextLine, 708, 15), 2)
                                strMarketShareQuant = formataValor(Mid(TextLine, 723, 5), 0)
                                strMarketShareValor = formataValor(Mid(TextLine, 728, 5), 0)
                                strMarketShareAcimaQuant = formataValor(Mid(TextLine, 733, 5), 0)
                                strMarketShareAcimaValor = formataValor(Mid(TextLine, 738, 5), 0)
                                strMarketShareAbaixoQuant = formataValor(Mid(TextLine, 743, 5), 0)
                                strMarketShareAbaxoValor = formataValor(Mid(TextLine, 748, 5), 0)

                                command = New SqlCommand( _
                                "SCR_CETIP_POTENCIAL_SINTETICO_GRAVAR " + _
                                    strMesReferencia + ", " + _
                                    strCNPJ + ", " + _
                                    strClassificacao + ", " + _
                                    strRazaoSocial + ", " + _
                                    strNomeFantasia + ", " + _
                                    strCNAE + ", " + _
                                    strEndereco + ", " + _
                                    strComplemento + ", " + _
                                    strNumero + ", " + _
                                    strBairro + ", " + _
                                    strUF + ", " + _
                                    strCidade + ", " + _
                                    strCEP + ", " + _
                                    strQuantidade + ", " + _
                                    strVolume + ", " + _
                                    strMarketShareQuant + ", " + _
                                    strMarketShareValor + ", " + _
                                    strMarketShareAcimaQuant + ", " + _
                                    strMarketShareAcimaValor + ", " + _
                                    strMarketShareAbaixoQuant + ", " + _
                                    strMarketShareAbaxoValor, connection)

                                command.Connection.Open()
                                command.ExecuteReader()
                                command.Connection.Close()

                            End If

                            If (strTipoRegistro = "T") Then

                                strQuantRegisto = Mid(TextLine, 2, 9)
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

                        e.Row.Cells(0).Text = drow("MesReferencia")
                        e.Row.Cells(1).Text = drow("CNPJ")
                        e.Row.Cells(2).Text = drow("Classificacao")
                        e.Row.Cells(3).Text = drow("RazaoSocial")
                        e.Row.Cells(4).Text = drow("NomeFantasia")
                        e.Row.Cells(5).Text = drow("CNAE")
                        e.Row.Cells(6).Text = drow("Endereco")
                        e.Row.Cells(7).Text = drow("Complemento")
                        e.Row.Cells(8).Text = drow("Numero")
                        e.Row.Cells(9).Text = drow("Bairro")
                        e.Row.Cells(10).Text = drow("UF")
                        e.Row.Cells(11).Text = drow("Cidade")
                        e.Row.Cells(12).Text = drow("CEP")
                        e.Row.Cells(13).Text = drow("Quantidade")
                        e.Row.Cells(14).Text = drow("Volume")
                        e.Row.Cells(15).Text = drow("MarketShareQuant")
                        e.Row.Cells(16).Text = drow("MarketShareValor")
                        e.Row.Cells(17).Text = drow("MarketShareAcimaQuant")
                        e.Row.Cells(18).Text = drow("MarketShareAcimaValor")
                        e.Row.Cells(19).Text = drow("MarketShareAbaixoQuant")
                        e.Row.Cells(20).Text = drow("MarketShareAbaxoValor")

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

            table = Util.ClassBD.GetExibirGrid("[SCR_CETIP_POTENCIAL_SINTETICO_DADOS]", "PSCETIP", strConn)

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
                    Dim filename As String = String.Format("Ranking_CETIP_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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
                        Response.Write("Cetip Potencial Sintetico")
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
            retorno = "'" + Mid(Data, 7, 4) + Mid(Data, 4, 2) + "'"
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