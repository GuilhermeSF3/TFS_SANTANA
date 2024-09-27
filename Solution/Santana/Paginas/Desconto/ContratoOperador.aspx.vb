Imports System
Imports System.Configuration
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic
Imports Santana.Seguranca
Imports Util
Imports System.Data.SqlClient

Namespace Paginas.Desconto

    Public Class ContratoOperador

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                Dim primeiroDiaMesCorrente As Date = Convert.ToDateTime("01/" + Now.Date.ToString("MM/yyyy"))
                txtDataDe.Text = primeiroDiaMesCorrente
                txtDataAte.Text = (DateTime.Now.AddDays(-1)).ToString("dd/MM/yyyy")

                If ContextoWeb.DadosTransferencia.Relatorio <> 0 Then
                    BindGridView1Data()
                    ContextoWeb.DadosTransferencia.Relatorio = 0
                End If

                If ContextoWeb.DadosTransferencia.CodAgente = 0 Then
                    Carrega_Operador()
                    Carrega_Status()
                Else
                    Carrega_Operador()
                    Carrega_Status()
                    ddlOperador.SelectedIndex = ddlOperador.Items.IndexOf(ddlOperador.Items.FindByValue(ContextoWeb.DadosTransferencia.CodAgente.ToString()))
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




        Protected Sub btnDataAnteriorDe_Click(sender As Object, e As EventArgs)

            Dim diaAnterior As Date = Convert.ToDateTime(txtDataDe.Text)
            diaAnterior = diaAnterior.AddDays(-1)
            txtDataDe.Text = diaAnterior.ToString("dd/MM/yyyy")


        End Sub


        Protected Sub btnProximaDataDe_Click(sender As Object, e As EventArgs)

            Dim proximoDia As Date = Convert.ToDateTime(txtDataDe.Text)
            proximoDia = proximoDia.AddDays(+1)
            txtDataDe.Text = proximoDia.ToString("dd/MM/yyyy")

        End Sub



        Protected Sub btnDataAnteriorAte_Click(sender As Object, e As EventArgs)

            Dim diaAnterior As Date = Convert.ToDateTime(txtDataAte.Text)
            diaAnterior = diaAnterior.AddDays(-1)
            txtDataAte.Text = diaAnterior.ToString("dd/MM/yyyy")


        End Sub


        Protected Sub btnProximaDataAte_Click(sender As Object, e As EventArgs)

            Dim proximoDia As Date = Convert.ToDateTime(txtDataAte.Text)
            proximoDia = proximoDia.AddDays(+1)
            txtDataAte.Text = proximoDia.ToString("dd/MM/yyyy")

        End Sub

        Private Sub Carrega_Operador()
            Try
                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)
                Dim ds As New DataSet
                Dim dt As DataTable = Nothing

                Dim codGerente As String
                Dim Produto As Integer = ContextoWeb.UsuarioLogado.Produto

                If ContextoWeb.UsuarioLogado.Perfil = 8 Then
                    codGerente = ContextoWeb.UsuarioLogado.codGerente
                Else
                    codGerente = "99"
                End If

                Dim command As SqlCommand
                Dim ddlValues As SqlDataReader


                If codGerente = "99" Then
                    command = New SqlCommand(
                    "select  O6DESCR, O6CODORG FROM CDCSANTANAMicroCredito..TORG6 (NOLOCK)  WHERE O6ATIVA = 'A' order by O6DESCR", connection)

                    command.Connection.Open()

                    ddlValues = command.ExecuteReader()

                    ddlOperador.DataSource = ddlValues
                    ddlOperador.DataValueField = "O6CODORG"
                    ddlOperador.DataTextField = "O6DESCR"
                    ddlOperador.DataBind()
                    ddlOperador.Items.Insert(0, New ListItem("TODOS", "99"))
                    ddlOperador.SelectedIndex = 0

                Else
                    command = New SqlCommand(
                    "select  O6DESCR, O6CODORG FROM CDCSANTANAMicroCredito..TORG6 (NOLOCK)  where O6codorg IN( " & codGerente & ") order by O6DESCR", connection)

                    command.Connection.Open()

                    ddlValues = command.ExecuteReader()

                    ddlOperador.DataSource = ddlValues
                    ddlOperador.DataValueField = "O6CODORG"
                    ddlOperador.DataTextField = "O6DESCR"
                    ddlOperador.DataBind()
                    ddlOperador.SelectedIndex = 0


                End If


                ddlValues.Close()

                command.Connection.Close()
                command.Connection.Dispose()

                connection.Close()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub Carrega_Status()

            Try

                ddlStatus.Items.Insert(0, New ListItem("Todos", "0"))
                ddlStatus.Items.Insert(1, New ListItem("Liquidados", "1"))
                ddlStatus.Items.Insert(2, New ListItem("Não Liquidados", "2"))

                ddlStatus.SelectedIndex = 0

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

            Try

                If e.Row.RowType = DataControlRowType.DataRow Then
                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As DataRow = CType(e.Row.DataItem, DataRowView).Row
                        Dim col As Integer


                        col = 0
                        If IsDBNull(drow("CONTRATO")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("CONTRATO")
                        End If

                        col += 1
                        If IsDBNull(drow("DATA_BASE")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("DATA_BASE")
                        End If


                        col += 1
                        If IsDBNull(drow("DATA_LIQUIDACAO")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("DATA_LIQUIDACAO")
                        End If

                        col += 1
                        If IsDBNull(drow("CPF_CNPJ")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("CPF_CNPJ")
                        End If

                        col += 1
                        If IsDBNull(drow("CLIENTE")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("CLIENTE")
                        End If

                        col += 1
                        If IsDBNull(drow("MODALIDADE")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("MODALIDADE")
                        End If

                        col += 1
                        If IsDBNull(drow("VALOR_FINANCIADO")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("VALOR_FINANCIADO"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("ATRASO")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("ATRASO")
                        End If

                        col += 1
                        If IsDBNull(drow("TC")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("TC"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("COD_OPERADOR")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("COD_OPERADOR")
                        End If

                        col += 1
                        If IsDBNull(drow("OPERADOR")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("OPERADOR")
                        End If

                        col += 1
                        If IsDBNull(drow("QTD_PARC")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("QTD_PARC")
                        End If

                        col += 1
                        If IsDBNull(drow("QTD_PARC_PG")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("QTD_PARC_PG")
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
            Dim oCpf As String

            table = ClassBD.GetExibirGrid("[SCR_CONTRATO_OPERADOR] '" & oDataDe.Data.ToString("yyyyMMdd") & "', '" & oDataAte.Data.ToString("yyyyMMdd") & "', '" & ddlOperador.SelectedValue & "', '" & ddlStatus.SelectedValue & "'", "ContratoOperador", strConn)


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
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Vencidos PJ' ,'Contratos Vencidos no Período');", True)
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
                    Dim filename As String = String.Format("VencidosPJ_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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

                        Response.Write("Vencidos - PJ - DE: " & txtDataDe.Data & " ATÉ: " & txtDataAte.Data)

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