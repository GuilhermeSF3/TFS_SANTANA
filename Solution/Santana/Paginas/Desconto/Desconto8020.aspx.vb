Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Santana.Seguranca
Imports Util


Namespace Paginas.Desconto

    Public Class Desconto8020

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.ToString("MM/yyyy"))
                txtDataReferencia.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)

                Carrega_Relatorio()

                If Session(HfGridView1Svid) IsNot Nothing Then
                    hfGridView1SV.Value = DirectCast(Session(HfGridView1Svid), String)
                    Session.Remove(HfGridView1Svid)
                End If

                If Session(HfGridView1Shid) IsNot Nothing Then
                    hfGridView1SH.Value = DirectCast(Session(HfGridView1Shid), String)
                    Session.Remove(HfGridView1Shid)
                End If

            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub



        Protected Sub btnDataAnteriorRef_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataReferencia.Text)
            txtDataReferencia.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)

        End Sub


        Protected Sub btnProximaDataRef_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataReferencia.Text)

            UltimoDiaMesAnterior = Convert.ToDateTime(UltimoDiaMesAnterior.Date.AddMonths(1).ToString("MM/yyyy"))
            txtDataReferencia.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)

        End Sub


        Private Function UltimoDiaUtilMesAnterior(Data As Date) As String

            Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Data.ToString("MM/yyyy"))

            ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddDays(-1)

            If ultimoDiaMesAnterior <= Convert.ToDateTime("01/aug/2014") Then
                If (ultimoDiaMesAnterior.DayOfWeek = DayOfWeek.Sunday) Then
                    ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddDays(-2)
                ElseIf (ultimoDiaMesAnterior.DayOfWeek = DayOfWeek.Saturday) Then
                    ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddDays(-1)
                End If
            End If

            Return ultimoDiaMesAnterior.ToString("dd/MM/yyyy")

        End Function



        Private Sub Carrega_Relatorio()

            Try

                ddlRelatorio.Items.Insert(0, New ListItem("Cedente", "1"))
                'ddlRelatorio.Items.Insert(1, New ListItem("Operador", "2"))
                'ddlRelatorio.Items.Insert(2, New ListItem("Resumo Operador", "3"))

                ddlRelatorio.SelectedIndex = 0

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub






        Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

            Try

                If e.Row.RowType = DataControlRowType.Header Then

                    Dim oData As New CDataHora(Convert.ToDateTime(txtDataReferencia.Text))
                    Dim currentYear As Integer
                    Dim currentMonth As String
                    Dim previousMonth As String
                    Dim nextMonth As String

                    Dim currentMonthYear As String
                    Dim previousMonthYear As String
                    Dim nextMonthYear As String


                    currentYear = oData.Data.Year

                    oData.Data = oData.Data.AddMonths(1)
                    nextMonth = oData.NomeMesSigla
                    nextMonthYear = String.Concat(nextMonth, "/", oData.Data.Year)

                    oData.Data = oData.Data.AddMonths(-1)
                    currentMonth = oData.NomeMesSigla
                    currentMonthYear = String.Concat(currentMonth, "/", oData.Data.Year)


                    oData.Data = oData.Data.AddMonths(-1)
                    previousMonth = oData.NomeMesSigla
                    previousMonthYear = String.Concat(previousMonth, "/", oData.Data.Year)



                    e.Row.Cells(14).Text = String.Concat("Rating Mercado <BR>", previousMonthYear)
                    e.Row.Cells(15).Text = String.Concat("Rating Mercado <BR>", currentMonthYear)
                    e.Row.Cells(16).Text = String.Concat("Rating Mercado <BR>", nextMonthYear)


                ElseIf e.Row.RowType = DataControlRowType.DataRow Then

                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row


                        If IsDBNull(drow("SEQ") Or drow("SEQ") = 0) Then
                            e.Row.Cells(0).Text = ""
                        Else
                            e.Row.Cells(0).Text = CNumero.FormataNumero(drow("SEQ"), 0)
                        End If

                        If IsDBNull(drow("PRC") Or drow("PRC") = 0) Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = String.Concat(CNumero.FormataNumero(drow("PRC"), 2), "%")
                        End If

                        If IsDBNull(drow("OPERADOR")) Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = drow("OPERADOR")
                        End If

                        If IsDBNull(drow("FREQ")) Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = drow("FREQ")
                        End If

                        If IsDBNull(drow("STATUS")) Then
                            e.Row.Cells(4).Text = ""
                        Else
                            e.Row.Cells(4).Text = drow("STATUS")
                        End If

                        If IsDBNull(drow("CPFCNPJ")) Then
                            e.Row.Cells(5).Text = ""
                        Else
                            e.Row.Cells(5).Text = drow("CPFCNPJ")
                        End If

                        If IsDBNull(drow("CEDENTE")) Then
                            e.Row.Cells(6).Text = ""
                        Else
                            e.Row.Cells(6).Text = drow("CEDENTE")
                        End If

                        If IsDBNull(drow("RISCO_DESCONTO") Or drow("RISCO_DESCONTO") = 0) Then
                            e.Row.Cells(7).Text = ""
                        Else
                            e.Row.Cells(7).Text = CNumero.FormataNumero(drow("RISCO_DESCONTO"), 2)
                        End If

                        If IsDBNull(drow("RISCO_FIDC") Or drow("RISCO_FIDC") = 0) Then
                            e.Row.Cells(8).Text = ""
                        Else
                            e.Row.Cells(8).Text = CNumero.FormataNumero(drow("RISCO_FIDC"), 2)
                        End If

                        If IsDBNull(drow("RISCO_GIRO") Or drow("RISCO_GIRO") = 0) Then
                            e.Row.Cells(9).Text = ""
                        Else
                            e.Row.Cells(9).Text = CNumero.FormataNumero(drow("RISCO_GIRO"), 2)
                        End If

                        If IsDBNull(drow("RISCO_TTL") Or drow("RISCO_TTL") = 0) Then
                            e.Row.Cells(10).Text = ""
                        Else
                            e.Row.Cells(10).Text = CNumero.FormataNumero(drow("RISCO_TTL"), 2)
                        End If

                        If IsDBNull(drow("RISCO_ACUMULADO") Or drow("RISCO_ACUMULADO") = 0) Then
                            e.Row.Cells(11).Text = ""
                        Else
                            e.Row.Cells(11).Text = CNumero.FormataNumero(drow("RISCO_ACUMULADO"), 2)
                        End If

                        If IsDBNull(drow("PRC_REPRES") Or drow("PRC_REPRES") = 0) Then
                            e.Row.Cells(12).Text = ""
                        Else
                            e.Row.Cells(12).Text = String.Concat(CNumero.FormataNumero(drow("PRC_REPRES"), 1), "%")
                        End If

                        If IsDBNull(drow("PRC_REPRES_ACUMUL") Or drow("PRC_REPRES_ACUMUL") = 0) Then
                            e.Row.Cells(13).Text = ""
                        Else
                            e.Row.Cells(13).Text = String.Concat(CNumero.FormataNumero(drow("PRC_REPRES_ACUMUL"), 1), "%")
                        End If

                        If IsDBNull(drow("RATING_MERCADO_M3")) Then
                            e.Row.Cells(14).Text = ""
                        Else
                            e.Row.Cells(14).Text = drow("RATING_MERCADO_M3")
                        End If

                        If IsDBNull(drow("RATING_MERCADO_M2")) Then
                            e.Row.Cells(15).Text = ""
                        Else
                            e.Row.Cells(15).Text = drow("RATING_MERCADO_M2")
                        End If

                        If IsDBNull(drow("RATING_MERCADO_M1")) Then
                            e.Row.Cells(16).Text = ""
                        Else
                            e.Row.Cells(16).Text = drow("RATING_MERCADO_M1")
                        End If

                        If IsDBNull(drow("SCORE") Or drow("SCORE") = 0) Then
                            e.Row.Cells(17).Text = ""
                        Else
                            e.Row.Cells(17).Text = drow("SCORE")
                        End If

                        If IsDBNull(drow("COD_GRUPO")) Then
                            e.Row.Cells(18).Text = ""
                        Else
                            e.Row.Cells(18).Text = drow("COD_GRUPO")
                        End If


                    End If
                End If


            Catch ex As Exception

            End Try
        End Sub



        Protected Sub GridView1_RowCreated(sender As Object, e As GridViewRowEventArgs)
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
            Dim data As DataTable = GetData()
            GridView1.DataSource = data
            GridView1.PageIndex = 0
            GridView1.DataBind()
            GridView1.AllowPaging = "True"


        End Sub

        Protected Sub BindGridView1DataView()

            GridView1.DataSource = DataGridView
            GridView1.DataBind()

        End Sub


        Private Function GetData() As DataTable


            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim table As DataTable = Util.ClassBD.GetExibirGrid("[SCR_DESCONTO_80_20] '" & Right(txtDataReferencia.Text, 4) & Mid(txtDataReferencia.Text, 4, 2) & Left(txtDataReferencia.Text, 2) & "', '" & ddlRelatorio.SelectedValue & "'", "Desconto8020", strConn)

            Return table

        End Function




        Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

            GridView1.PageIndex = IIf(e.NewPageIndex < 0, 0, e.NewPageIndex)
            BindGridView1DataView()
        End Sub




        Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Desconto Produção' ,'Desconto Produção", True)
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
                GridView1.AllowPaging = False
                BindGridView1Data()
                ExportExcel(GridView1)

            Catch ex As Exception
                Throw ex
            End Try

        End Sub




        Private Sub ExportExcel(objGrid As GridView)


            Try

                If Not IsNothing(objGrid.HeaderRow) Then

                    Response.Clear()
                    Response.Buffer = True
                    Dim filename As String = String.Format("Desconto8020_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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
                                            cell.Controls.Add(New Literal() With {
                                             .Text = TryCast(control, HyperLink).Text
                                            })
                                            Exit Select
                                        Case "TextBox"
                                            cell.Controls.Add(New Literal() With {
                                             .Text = TryCast(control, TextBox).Text
                                            })
                                            Exit Select
                                        Case "LinkButton"
                                            cell.Controls.Add(New Literal() With {
                                             .Text = TryCast(control, LinkButton).Text
                                            })
                                            Exit Select
                                        Case "CheckBox"
                                            cell.Controls.Add(New Literal() With {
                                             .Text = TryCast(control, CheckBox).Text
                                            })
                                            Exit Select
                                        Case "RadioButton"
                                            cell.Controls.Add(New Literal() With {
                                             .Text = TryCast(control, RadioButton).Text
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


                        Select Case ddlRelatorio.SelectedValue
                            Case 1
                                Response.Write(String.Format("CLIENTES PJ EM ORDEM DECRESCENTE DE RISCO - {0} - GERAL", txtDataReferencia.Text))
                            Case 2
                                Response.Write(String.Format("CLIENTES PJ EM ORDEM DECRESCENTE DE RISCO - {0} - POR OPERADOR", txtDataReferencia.Text))
                            Case 3
                                Response.Write(String.Format("CLIENTES PJ EM ORDEM DECRESCENTE DE RISCO - {0} - RECORRENTES", txtDataReferencia.Text))
                        End Select
                        Response.Write("<p>")


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



        Protected Sub hfGridView1SV_ValueChanged(sender As Object, e As EventArgs)
            Session(HfGridView1Svid) = hfGridView1SV.Value
        End Sub

        Protected Sub hfGridView1SH_ValueChanged(sender As Object, e As EventArgs)
            Session(HfGridView1Shid) = hfGridView1SH.Value
        End Sub

        Protected Sub GridView1_DataBound(sender As Object, e As EventArgs)
            Dim gridView As GridView = CType(sender, GridView)
            If Not IsNothing(gridView.DataSource) AndAlso CType(gridView.DataSource, System.Data.DataTable).Rows.Count > 0 Then
                Util.CWebPageUtil.PreencheListaDePaginas(gridView, CType(gridView.BottomPagerRow.Cells(0).FindControl("PagerPages"), DropDownList))
            End If
        End Sub

        Protected Sub PagerPages_SelectedIndexChanged(sender As Object, e As EventArgs)

            GridView1.DataSource = DataGridView()
            GridView1.PageIndex = CType(sender, DropDownList).SelectedIndex
            GridView1.DataBind()

        End Sub

    End Class

End Namespace