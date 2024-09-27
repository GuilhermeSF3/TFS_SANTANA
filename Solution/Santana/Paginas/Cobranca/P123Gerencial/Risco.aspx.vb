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

    Public Class Risco

        Inherits SantanaPage


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                txtDataDe.Text = Convert.ToDateTime(Now.Date.ToString("dd/MM/yyyy"))
                txtDataDe.Text = (Convert.ToDateTime(txtDataDe.Text).AddMonths(-6)).ToString("dd/MM/yyyy")
                txtDataDe.Text = (Convert.ToDateTime("01/" + Right(txtDataDe.Text, 7))).ToString("dd/MM/yyyy")

                txtDataAte.Text = (Convert.ToDateTime(txtDataDe.Text).AddMonths(+1).AddDays(-1)).ToString("dd/MM/yyyy")


                If ContextoWeb.DadosTransferencia.CodAgente = 0 Then
                    Carrega_Agente()
                Else
                    Carrega_Agente()
                    ddlAgente.SelectedIndex = ddlAgente.Items.IndexOf(ddlAgente.Items.FindByValue(ContextoWeb.DadosTransferencia.CodAgente.ToString()))
                End If


                Carrega_Relatorio()
                ddlRelatorio.SelectedIndex = 0

                If ContextoWeb.DadosTransferencia.CodAgente <> 0 Then
                    BindGridView1Data()
                    ContextoWeb.DadosTransferencia.CodAgente = 0
                End If

                dvConsultasAgentes.Visible = False


            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)


        End Sub



        Protected Sub btnDataAnterior_Click(sender As Object, e As EventArgs)
            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataDe.Text).AddMonths(-1)
            txtDataDe.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)

        End Sub


        Protected Sub btnProximaData_Click(sender As Object, e As EventArgs)
            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataDe.Text).AddMonths(1)
            txtDataDe.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)

        End Sub


        Protected Sub btnDataAnterior1_Click(sender As Object, e As EventArgs)
            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataDe.Text)
            txtDataDe.Text = UltimoDiaMesAnterior.AddMonths(-1)

        End Sub


        Protected Sub btnProximaData1_Click(sender As Object, e As EventArgs)
            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataDe.Text)
            txtDataDe.Text = UltimoDiaMesAnterior.AddMonths(+1)

        End Sub


        Protected Sub btnDataAnterior2_Click(sender As Object, e As EventArgs)
            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataAte.Text).AddMonths(-1)
            txtDataAte.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)

        End Sub


        Protected Sub btnProximaData2_Click(sender As Object, e As EventArgs)
            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataAte.Text).AddMonths(1)
            txtDataAte.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)


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



        Private Sub Carrega_Relatorio()

            Try

                ddlRelatorio.Items.Insert(0, New ListItem("Risco / Produção", "1"))
                ddlRelatorio.Items.Insert(1, New ListItem("Agente x Produção", "2"))


                ddlRelatorio.SelectedIndex = 0


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub




   Private Sub Carrega_Agente()

        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim connection As New SqlConnection(strConn)
            Dim ds As New DataSet
                Dim dt As DataTable = Nothing

                Dim command As SqlCommand = New SqlCommand( _
            "select AGCod, (AGDescr + ' - ' + CAST(AGCod AS VARCHAR(10))) as Descricao from TAgente (nolock) where AGAtivo=1 order by AGDescr", connection)

            command.Connection.Open()

            Dim ddlValues As SqlDataReader
            ddlValues = command.ExecuteReader()

            ddlAgente.DataSource = ddlValues
            ddlAgente.DataValueField = "AGCod"
            ddlAgente.DataTextField = "Descricao"
            ddlAgente.DataBind()

            ddlAgente.Items.Insert(0, New ListItem("TODOS - 99", "99"))
            ddlAgente.SelectedIndex = 0

            ddlValues.Close()

            command.Connection.Close()
            command.Connection.Dispose()

            connection.Close()

        Catch ex As Exception

        Finally
            GC.Collect()
        End Try
    End Sub



        Private Sub GridViewScore_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewScore.RowDataBound

            Try

                If e.Row.RowType = DataControlRowType.DataRow Then

                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row


                        If IsDBNull(drow("risco")) Then
                            e.Row.Cells(0).Text = ""
                        Else
                            e.Row.Cells(0).Text = drow("risco")
                        End If


                        If IsDBNull(drow("qtde")) OrElse drow("qtde") = 0 Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = CNumero.FormataNumero(drow("qtde"), 0)
                        End If


                        If IsDBNull(drow("vlr_financ")) OrElse drow("vlr_financ") = 0 Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = CNumero.FormataNumero(drow("vlr_financ"), 2)
                        End If


                        If IsDBNull(drow("prc")) OrElse drow("prc") = 0 Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = CNumero.FormataNumero(drow("prc"), 2)
                        End If

                        e.Row.Cells(4).Text = CNumero.FormataNumero(drow("prc_90d"), 2)

                    End If
                End If


            Catch ex As Exception

            End Try
        End Sub




        Private Sub GridViewAgentes_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewAgentes.RowDataBound

            Try

                Dim cor As Color

                If e.Row.RowType = DataControlRowType.DataRow Then

                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row


                        If IsDBNull(drow("formato")) Then
                            cor = e.Row.Cells(0).BackColor
                        ElseIf drow("formato") = "AM" Then
                            cor = Drawing.ColorTranslator.FromOle(&H00FFFF)
                        Else
                            cor = e.Row.Cells(0).BackColor
                        End If


                        If IsDBNull(drow("risco")) Then
                            e.Row.Cells(0).Text = ""
                        Else
                            e.Row.Cells(0).Text = drow("risco")
                        End If
                        e.Row.Cells(0).BackColor = cor


                        If IsDBNull(drow("agente")) Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = drow("agente")
                        End If
                        e.Row.Cells(1).BackColor = cor


                        If IsDBNull(drow("vlr_prod")) OrElse drow("vlr_prod") = 0 Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = CNumero.FormataNumero(drow("vlr_prod"), 2)
                        End If
                        e.Row.Cells(2).BackColor = cor


                        If IsDBNull(drow("prc")) OrElse drow("prc") = 0 Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = CNumero.FormataNumero(drow("prc"), 2)
                        End If
                        e.Row.Cells(3).BackColor = cor
                        e.Row.Cells(4).Text = CNumero.FormataNumero(drow("prc_90d"), 2)

                    End If
                End If


            Catch ex As Exception

            End Try
        End Sub


        Protected Sub GridViewScore_RowCreated(sender As Object, e As GridViewRowEventArgs)
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


        Protected Sub GridViewAgentes_RowCreated(sender As Object, e As GridViewRowEventArgs)
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


            Dim objGrid As GridView

            Select Case ddlRelatorio.SelectedValue
                Case 1
                    objGrid = GridViewScore
                Case Else
                    objGrid = GridViewAgentes

            End Select

            ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = Nothing
            objGrid.DataSource = GetData()
            objGrid.PageIndex = 0
            objGrid.DataBind()
            objGrid.AllowPaging = "True"

        End Sub

        Protected Sub BindGridView1DataView()


            Dim objGrid As GridView

            Select Case ddlRelatorio.SelectedValue
                Case 1
                    objGrid = GridViewScore
                Case Else
                    objGrid = GridViewAgentes

            End Select

            objGrid.DataSource = DataGridView
            objGrid.DataBind()

        End Sub


        Private Function GetData() As DataTable

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim table As DataTable = Nothing


            Dim oDataDe As New CDataHora(Convert.ToDateTime(txtDataDe.Text))
            Dim oDataAte As New CDataHora(Convert.ToDateTime(txtDataAte.Text))

            Select Case ddlRelatorio.SelectedValue
                Case 1
                    table = ClassBD.GetExibirGrid("[Scr_Score_prod] '" & oDataDe.Data.ToString("yyyyMMdd") & "', '" & oDataAte.Data.ToString("yyyyMMdd") & "','" & oDataAte.Data.ToString("yyyyMMdd") & "'", "SCORE", strConn)
                Case 2
                    table = ClassBD.GetExibirGrid("[Scr_agente_prod] '" & oDataDe.Data.ToString("yyyyMMdd") & "', '" & oDataAte.Data.ToString("yyyyMMdd") & "','" & oDataAte.Data.ToString("yyyyMMdd") & "','" & ddlAgente.SelectedValue & "'", "AGENTES", strConn)
            End Select


            Return table

        End Function


        Protected Sub GridViewScore_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

            If e.NewPageIndex >= 0 Then
                GridViewScore.PageIndex = e.NewPageIndex
                BindGridView1DataView()
            End If

        End Sub


        Protected Sub GridViewAgentes_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

            If e.NewPageIndex >= 0 Then
                GridViewAgentes.PageIndex = e.NewPageIndex
                BindGridView1DataView()
            End If

        End Sub




        Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../../Menu.aspx")
        End Sub

        Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
            Try

                BindGridView1Data()

                Select Case ddlRelatorio.SelectedValue
                    Case 1

                        BindGridView1Data()
                        dvConsultasScore.Visible = True
                        dvConsultasAgentes.Visible = False

                    Case Else

                        BindGridView1Data()
                        dvConsultasScore.Visible = False
                        dvConsultasAgentes.Visible = True
                End Select

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
                Dim objGrid As GridView

                Select Case ddlRelatorio.SelectedValue
                    Case 1
                        GridViewScore.AllowPaging = False
                        BindGridView1Data()
                        objGrid = GridViewScore

                    Case Else
                        GridViewAgentes.AllowPaging = False
                        BindGridView1Data()
                        objGrid = GridViewAgentes
                End Select


                ExportExcel(objGrid)


            Catch ex As Exception
                Throw ex
            End Try

        End Sub




        Private Sub ExportExcel(objGrid As GridView)


            Try

                If Not IsNothing(objGrid.HeaderRow) Then

                    Response.Clear()
                    Response.Buffer = True
                    Dim filename As String = String.Format("Risco_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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

                        Response.Write("Produção De " & txtDataDe.Text & " Até " & txtDataAte.Text & " - Relatório " & ddlRelatorio.SelectedItem.Text)


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



        Protected Sub ddlRelatorio_SelectedIndexChanged(sender As Object, e As EventArgs)

            Select Case ddlRelatorio.SelectedValue
                Case 1
                    ddlAgenteLabel.Visible = false 
                    ddlAgente.Visible = false 

                Case Else
                    ddlAgenteLabel.Visible = true 
                    ddlAgente.Visible = true 

            End Select

            GridViewScore.DataSource = Nothing
            GridViewScore.DataBind()
            GridViewAgentes.DataSource = Nothing
            GridViewAgentes.DataBind()

            ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = Nothing

            dvConsultasScore.Visible = False
            dvConsultasAgentes.Visible = False

        End Sub



        Protected Sub GridViewScore_DataBound(sender As Object, e As EventArgs)
            Dim gridView As GridView = CType(sender, GridView)
            If Not IsNothing(gridView.DataSource) AndAlso CType(gridView.DataSource, System.Data.DataTable).Rows.Count > 0 Then
                Util.CWebPageUtil.PreencheListaDePaginas(gridView, CType(gridView.BottomPagerRow.Cells(0).FindControl("PagerPages"), DropDownList))
            End If
        End Sub


        Protected Sub GridViewAgentes_DataBound(sender As Object, e As EventArgs)
            Dim gridView As GridView = CType(sender, GridView)
            If Not IsNothing(gridView.DataSource) AndAlso CType(gridView.DataSource, System.Data.DataTable).Rows.Count > 0 Then
                Util.CWebPageUtil.PreencheListaDePaginas(gridView, CType(gridView.BottomPagerRow.Cells(0).FindControl("PagerPages"), DropDownList))
            End If
        End Sub

        Protected Sub PagerPages_SelectedIndexChanged(sender As Object, e As EventArgs)


            Dim objGrid As GridView

            Select Case ddlRelatorio.SelectedValue
                Case 1
                    objGrid = GridViewScore

                Case Else
                    objGrid = GridViewAgentes

            End Select


            objGrid.DataSource = DataGridView()
            objGrid.PageIndex = CType(sender, DropDownList).SelectedIndex
            objGrid.DataBind()

        End Sub


    End Class
End Namespace