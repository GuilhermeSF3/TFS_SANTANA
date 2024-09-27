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

Namespace Paginas.Cobranca.P123Gerencial

    Public Class Inadimplencia

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                txtDataDe.Text = Convert.ToDateTime("01/" + Now.Date.AddMonths(-7).ToString("MM/yyyy"))
                txtDataAte.Text = Convert.ToDateTime("01/" + Now.Date.AddMonths(-6).ToString("MM/yyyy")).AddDays(-1).ToString("dd/MM/yyyy")
                txtData.Text = Convert.ToDateTime("01/" + Now.Date.ToString("MM/yyyy")).AddDays(-1).ToString("dd/MM/yyyy")


                If ContextoWeb.DadosTransferencia.Relatorio = 0 Then
                    Carrega_Relatorio()
                Else
                    Carrega_Relatorio()
                    ddlRelatorio.SelectedIndex = ddlRelatorio.Items.IndexOf(ddlRelatorio.Items.FindByValue(ContextoWeb.DadosTransferencia.Relatorio.ToString()))
                End If

                If ContextoWeb.DadosTransferencia.Relatorio <> 0 Then
                    BindGridView1Data()
                    ContextoWeb.DadosTransferencia.Relatorio = 0
                End If

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



        Protected Sub btnDataAnterior_Click(sender As Object, e As EventArgs)
            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtData.Text).AddMonths(-1)
            txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
        End Sub
        Protected Sub btnProximaData_Click(sender As Object, e As EventArgs)
            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtData.Text).AddMonths(1)
            txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
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


        Protected Sub btnDataAnterior1_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataDe.Text)
            txtDataDe.Text = UltimoDiaMesAnterior.AddMonths(-1)

        End Sub


        Protected Sub btnProximaData1_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataDe.Text)
            txtDataDe.Text = UltimoDiaMesAnterior.AddMonths(1)

        End Sub


        Protected Sub btnDataAnterior2_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataAte.Text).AddMonths(-1)
            txtDataAte.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
        End Sub


        Protected Sub btnProximaData2_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataAte.Text).AddMonths(1)
            txtDataAte.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)

        End Sub

        Private Sub Carrega_Relatorio()

            Try

                ddlRelatorio.Items.Insert(0, New ListItem("Inadimplência / Safra", "1"))
                ddlRelatorio.Items.Insert(1, New ListItem("Score x Inadimplência", "2"))
                ddlRelatorio.Items.Insert(2, New ListItem("Inadimplência / Agente", "3"))

                ddlRelatorio.SelectedIndex = 0


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub



        Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

            Try
                Dim Bold As Boolean


                If e.Row.RowType = DataControlRowType.Header Then

                        Select Case ddlRelatorio.SelectedValue
                            Case 1
                                e.Row.Cells(0).Text = "Safras"
                            Case 2
                                e.Row.Cells(0).Text = "Classificação de Risco"
                            Case 3
                                e.Row.Cells(0).Text = "Agente"
                        End Select
               
                ElseIf e.Row.RowType = DataControlRowType.DataRow Then

                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

                        e.Row.Cells(0).Font.Bold = Bold
                        e.Row.Cells(0).Text = drow("DESCRICAO")

                        e.Row.Cells(1).Font.Bold = Bold
                        If IsDBNull(drow("fxA_vlr")) OrElse drow("fxA_vlr") = 0 Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = CNumero.FormataNumero(drow("fxA_vlr"), 2)
                        End If

                        e.Row.Cells(2).Font.Bold = Bold
                        If IsDBNull(drow("fxA")) OrElse drow("fxA") = 0 Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = CNumero.FormataNumero(drow("fxA"), 2)
                        End If

                        e.Row.Cells(3).Font.Bold = Bold
                        If IsDBNull(drow("fxB_vlr")) OrElse drow("fxB_vlr") = 0 Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = CNumero.FormataNumero(drow("fxB_vlr"), 2)
                        End If

                        e.Row.Cells(4).Font.Bold = Bold
                        If IsDBNull(drow("fxB")) OrElse drow("fxB") = 0 Then
                            e.Row.Cells(4).Text = ""
                        Else
                            e.Row.Cells(4).Text = CNumero.FormataNumero(drow("fxB"), 2)
                        End If

                        e.Row.Cells(5).Font.Bold = Bold
                        If IsDBNull(drow("fxC_vlr")) OrElse drow("fxC_vlr") = 0 Then
                            e.Row.Cells(5).Text = ""
                        Else
                            e.Row.Cells(5).Text = CNumero.FormataNumero(drow("fxC_vlr"), 2)
                        End If

                        e.Row.Cells(6).Font.Bold = Bold
                        If IsDBNull(drow("fxC")) OrElse drow("fxC") = 0 Then
                            e.Row.Cells(6).Text = ""
                        Else
                            e.Row.Cells(6).Text = CNumero.FormataNumero(drow("fxC"), 2)
                        End If

                        e.Row.Cells(7).Font.Bold = Bold
                        If IsDBNull(drow("fxD_vlr")) OrElse drow("fxD_vlr") = 0 Then
                            e.Row.Cells(7).Text = ""
                        Else
                            e.Row.Cells(7).Text = CNumero.FormataNumero(drow("fxD_vlr"), 2)
                        End If

                        e.Row.Cells(8).Font.Bold = Bold
                        If IsDBNull(drow("fxD")) OrElse drow("fxD") = 0 Then
                            e.Row.Cells(8).Text = ""
                        Else
                            e.Row.Cells(8).Text = CNumero.FormataNumero(drow("fxD"), 2)
                        End If

                        e.Row.Cells(9).Font.Bold = Bold
                        If IsDBNull(drow("fxE_vlr")) OrElse drow("fxE_vlr") = 0 Then
                            e.Row.Cells(9).Text = ""
                        Else
                            e.Row.Cells(9).Text = CNumero.FormataNumero(drow("fxE_vlr"), 2)
                        End If

                        e.Row.Cells(10).Font.Bold = Bold
                        If IsDBNull(drow("fxE")) OrElse drow("fxE") = 0 Then
                            e.Row.Cells(10).Text = ""
                        Else
                            e.Row.Cells(10).Text = CNumero.FormataNumero(drow("fxE"), 2)
                        End If

                        e.Row.Cells(11).Font.Bold = Bold
                        If IsDBNull(drow("fxF_vlr")) OrElse drow("fxF_vlr") = 0 Then
                            e.Row.Cells(11).Text = ""
                        Else
                            e.Row.Cells(11).Text = CNumero.FormataNumero(drow("fxF_vlr"), 2)
                        End If

                        e.Row.Cells(12).Font.Bold = Bold
                        If IsDBNull(drow("fxF")) OrElse drow("fxF") = 0 Then
                            e.Row.Cells(12).Text = ""
                        Else
                            e.Row.Cells(12).Text = CNumero.FormataNumero(drow("fxF"), 2)
                        End If

                        e.Row.Cells(13).Font.Bold = Bold
                        If IsDBNull(drow("fxG_vlr")) OrElse drow("fxG_vlr") = 0 Then
                            e.Row.Cells(13).Text = ""
                        Else
                            e.Row.Cells(13).Text = CNumero.FormataNumero(drow("fxG_vlr"), 2)
                        End If

                        e.Row.Cells(14).Font.Bold = Bold
                        If IsDBNull(drow("fxG")) OrElse drow("fxG") = 0 Then
                            e.Row.Cells(14).Text = ""
                        Else
                            e.Row.Cells(14).Text = CNumero.FormataNumero(drow("fxG"), 2)
                        End If

                        e.Row.Cells(15).Font.Bold = Bold
                        If IsDBNull(drow("fxH_vlr")) OrElse drow("fxH_vlr") = 0 Then
                            e.Row.Cells(15).Text = ""
                        Else
                            e.Row.Cells(15).Text = CNumero.FormataNumero(drow("fxH_vlr"), 2)
                        End If

                        e.Row.Cells(16).Font.Bold = Bold
                        If IsDBNull(drow("fxH")) OrElse drow("fxH") = 0 Then
                            e.Row.Cells(16).Text = ""
                        Else
                            e.Row.Cells(16).Text = CNumero.FormataNumero(drow("fxH"), 2)
                        End If

                        e.Row.Cells(17).Font.Bold = Bold
                        If IsDBNull(drow("fxHH_vlr")) OrElse drow("fxHH_vlr") = 0 Then
                            e.Row.Cells(17).Text = ""
                        Else
                            e.Row.Cells(17).Text = CNumero.FormataNumero(drow("fxHH_vlr"), 2)
                        End If

                        e.Row.Cells(18).Font.Bold = Bold
                        If IsDBNull(drow("fxHH")) OrElse drow("fxHH") = 0 Then
                            e.Row.Cells(18).Text = ""
                        Else
                            e.Row.Cells(18).Text = CNumero.FormataNumero(drow("fxHH"), 2)
                        End If

                        e.Row.Cells(19).Font.Bold = Bold
                        If IsDBNull(drow("VLR_PROD")) OrElse drow("VLR_PROD") = 0 Then
                            e.Row.Cells(19).Text = ""
                        Else
                            e.Row.Cells(19).Text = CNumero.FormataNumero(drow("VLR_PROD"), 2)
                        End If

                        e.Row.Cells(20).Font.Bold = Bold
                        If IsDBNull(drow("inad0")) OrElse drow("inad0") = 0 Then
                            e.Row.Cells(20).Text = ""
                        Else
                            e.Row.Cells(20).Text = CNumero.FormataNumero(drow("inad0"), 2)
                        End If

                        e.Row.Cells(21).Font.Bold = Bold
                        If IsDBNull(drow("inad15")) OrElse drow("inad15") = 0 Then
                            e.Row.Cells(21).Text = ""
                        Else
                            e.Row.Cells(21).Text = CNumero.FormataNumero(drow("inad15"), 2)
                        End If

                        e.Row.Cells(22).Font.Bold = Bold
                        If IsDBNull(drow("inad91")) OrElse drow("inad91") = 0 Then
                            e.Row.Cells(22).Text = ""
                        Else
                            e.Row.Cells(22).Text = CNumero.FormataNumero(drow("inad91"), 2)
                        End If

                        e.Row.Cells(23).Font.Bold = Bold
                        If IsDBNull(drow("inad360")) OrElse drow("inad360") = 0 Then
                            e.Row.Cells(23).Text = ""
                        Else
                            e.Row.Cells(23).Text = CNumero.FormataNumero(drow("inad360"), 2)
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
            GridView1.DataSource = GetData()
            GridView1.PageIndex = 0
            GridView1.DataBind()

        End Sub

        Protected Sub BindGridView1DataView()

            GridView1.DataSource = DataGridView
            GridView1.DataBind()

        End Sub


        Private Function GetData() As DataTable

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim table As DataTable = Nothing

            
            Dim oDataDe As New CDataHora(Convert.ToDateTime(txtDataDe.Text))
            Dim oDataAte As New CDataHora(Convert.ToDateTime(txtDataAte.Text))
            Dim oDataRef As New CDataHora(Convert.ToDateTime(txtData.Text))

            '    oDataRef
            Select Case ddlRelatorio.SelectedValue
                Case 1
                    table = ClassBD.GetExibirGrid("[Scr_inadimpl_safra] '" & oDataDe.Data.ToString("yyyyMMdd") & "', '" & oDataAte.Data.ToString("yyyyMMdd") & "','" & oDataRef.Data.ToString("yyyyMMdd") & "'", "INADIMPLENCIA", strConn)
                Case 2
                    table = ClassBD.GetExibirGrid("[Scr_inadimpl_score] '" & oDataDe.Data.ToString("yyyyMMdd") & "', '" & oDataAte.Data.ToString("yyyyMMdd") & "','" & oDataRef.Data.ToString("yyyyMMdd") & "'", "INADIMPLENCIA", strConn)
                Case 3
                    table = ClassBD.GetExibirGrid("[Scr_inadimpl_agente] '" & oDataDe.Data.ToString("yyyyMMdd") & "', '" & oDataAte.Data.ToString("yyyyMMdd") & "','" & oDataRef.Data.ToString("yyyyMMdd") & "'", "INADIMPLENCIA", strConn)
            End Select

           
            Return table

        End Function


        Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

            If e.NewPageIndex >= 0 Then
                GridView1.PageIndex = e.NewPageIndex
                BindGridView1DataView()
            End If

        End Sub




        Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Inadimplencia Gerencial' ,'Calculo da Inadimplencia p123 a 90 dias por Safra de Produção.');", True)
        End Sub


        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../../Menu.aspx")
        End Sub

        Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
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

                If Not IsNothing(GridView1.HeaderRow) Then
                    GridView1.AllowPaging = False
                    BindGridView1Data()
                    ExportExcel(GridView1)
                End If


            Catch ex As Exception
                Throw ex
            End Try

        End Sub




        Private Sub ExportExcel(objGrid As GridView)


            Try

                If Not IsNothing(objGrid.HeaderRow) Then

                    Response.Clear()
                    Response.Buffer = True
                    Dim filename As String = String.Format("Inadimplencia_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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

                        Response.Write("Relatório " & ddlRelatorio.SelectedItem.Text)

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