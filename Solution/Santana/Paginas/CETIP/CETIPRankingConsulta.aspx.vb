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

    Public Class CETIPRankingConsulta

        Inherits SantanaPage

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
                        e.Row.Cells(5).Text = drow("Agente")
                        e.Row.Cells(6).Text = drow("Loja_Ativa")
                        e.Row.Cells(7).Text = drow("CNAE")
                        e.Row.Cells(8).Text = drow("Endereco")
                        e.Row.Cells(9).Text = drow("Complemento")
                        e.Row.Cells(10).Text = drow("Número")
                        e.Row.Cells(11).Text = drow("Bairro")
                        e.Row.Cells(12).Text = drow("UF")
                        e.Row.Cells(13).Text = drow("Cidade")
                        e.Row.Cells(14).Text = drow("CEP")
                        e.Row.Cells(15).Text = drow("QuantidadedeBancos")
                        e.Row.Cells(16).Text = drow("RankingCliente")


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

            ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = Nothing
            GridViewRiscoAnalitico.DataSource = GetData()
            GridViewRiscoAnalitico.DataBind()
            GridViewRiscoAnalitico.AllowPaging = "True"
        End Sub

        Protected Sub BindGridView1DataView()

            GridViewRiscoAnalitico.DataSource = DataGridView
            GridViewRiscoAnalitico.DataBind()

        End Sub


        Private Function GetData() As DataTable
            Dim codGerente As String

            If ContextoWeb.UsuarioLogado.Perfil = 8 Then
                codGerente = ContextoWeb.UsuarioLogado.codGerente
            Else
                codGerente = "99"
            End If

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim table As DataTable

            table = Util.ClassBD.GetExibirGrid("[SCR_CETIP_RANKING_CONSULTA] '" & Right(txtDataDe.Text, 4) & Mid(txtDataDe.Text, 4, 2) & "', '" & txtLocalizar.Text & "', 99", "RCETIP", strConn)

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
                        Response.Write("Cetip Ranking  - Ref : " + txtDataDe.Text + " Pesquisar: " + txtLocalizar.Text + " Agente: " + ContextoWeb.UsuarioLogado.codGerente)
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