Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Santana.Seguranca
Imports Util

Namespace Paginas.Financeiro

    Public Class OperacaoCaptacao

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                Dim primeiroDiaMesCorrente As Date = Convert.ToDateTime(Now.Date.ToString("dd/MM/yyyy"))
                txtDataAplicacao.Text = primeiroDiaMesCorrente
                txtDataRef.Text = primeiroDiaMesCorrente

                Carrega_Clientes()
                Carrega_Ativo()

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

            Dim diaAnterior As Date = Convert.ToDateTime(txtDataAplicacao.Text)
            diaAnterior = diaAnterior.AddDays(-1)
            txtDataAplicacao.Text = diaAnterior.ToString("dd/MM/yyyy")


        End Sub


        Protected Sub btnProximaDataDe_Click(sender As Object, e As EventArgs)

            Dim proximoDia As Date = Convert.ToDateTime(txtDataAplicacao.Text)
            proximoDia = proximoDia.AddDays(+1)
            txtDataAplicacao.Text = proximoDia.ToString("dd/MM/yyyy")

        End Sub

        Protected Sub btnDataAnteriorRef_Click(sender As Object, e As EventArgs)

            Dim diaAnterior As Date = Convert.ToDateTime(txtDataRef.Text)
            diaAnterior = diaAnterior.AddDays(-1)
            txtDataRef.Text = diaAnterior.ToString("dd/MM/yyyy")


        End Sub


        Protected Sub btnProximaDataRef_Click(sender As Object, e As EventArgs)

            Dim proximoDia As Date = Convert.ToDateTime(txtDataRef.Text)
            proximoDia = proximoDia.AddDays(+1)
            txtDataRef.Text = proximoDia.ToString("dd/MM/yyyy")

        End Sub



        Protected Sub btnEditar_Click(sender As Object, e As EventArgs)

            ddlAtivo.Visible = Not ddlAtivo.Visible
            txtAtivo.Visible = Not ddlAtivo.Visible

            If txtAtivo.Visible Then
                txtAtivo.Focus()
            Else
                Carrega_Ativo()
            End If

        End Sub



        Private Sub Carrega_Clientes()

            Try
                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)

                Dim command As SqlCommand = New SqlCommand("select codigo, nome from IOpen_Shop..OPN_CLIENTES (nolock) order by nome", connection)

                command.Connection.Open()

                Dim ddlValues As SqlDataReader
                ddlValues = command.ExecuteReader()

                ddlCliente.DataSource = ddlValues
                ddlCliente.DataValueField = "codigo"
                ddlCliente.DataTextField = "nome"
                ddlCliente.DataBind()

                ddlCliente.Items.Insert(0, New ListItem("TODOS", "0"))
                ddlCliente.SelectedIndex = 0

                ddlValues.Close()

                command.Connection.Close()
                command.Connection.Dispose()

                connection.Close()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub


        Private Sub Carrega_Ativo()

            Try
                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)

                Dim oData As New CDataHora(Convert.ToDateTime(txtDataAplicacao.Text))
                Dim command As SqlCommand

                If ddlCliente.SelectedValue = 0 Then
                    command = New SqlCommand("select codigo from IOpen_Shop..opn_movimento (nolock) where data ='" & oData.Data.ToString("yyyy-MM-dd") & "'", connection)
                Else
                    command = New SqlCommand("select codigo from IOpen_Shop..opn_movimento (nolock) where cliente_codigo = " & ddlCliente.SelectedValue & " and data ='" & oData.Data.ToString("yyyy-MM-dd") & "'", connection)
                End If

                command.Connection.Open()

                Dim ddlValues As SqlDataReader
                ddlValues = command.ExecuteReader()

                ddlAtivo.DataSource = ddlValues
                ddlAtivo.DataValueField = "codigo"
                ddlAtivo.DataTextField = "codigo"
                ddlAtivo.DataBind()


                ddlValues.Close()

                command.Connection.Close()
                command.Connection.Dispose()

                connection.Close()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub




        Protected Sub ddlCliente_SelectedIndexChanged(sender As Object, e As EventArgs)

            Carrega_Ativo()

        End Sub




        Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

            Dim cor As Color

            Try

                If e.Row.RowType = DataControlRowType.DataRow Then
                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As DataRow = CType(e.Row.DataItem, DataRowView).Row
                        Dim col As Integer

                        col = 0
                        If IsDBNull(drow("dt_proj")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("dt_proj")
                        End If
                        e.Row.Cells(col).BackColor = cor

                        col += 1
                        If IsDBNull(drow("tx_cdi")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("tx_cdi"), 2)
                        End If
                        e.Row.Cells(col).BackColor = cor

                        col += 1
                        If IsDBNull(drow("factor1")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("factor1"), 8)
                        End If
                        e.Row.Cells(col).BackColor = cor

                        col += 1
                        If IsDBNull(drow("factor2")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("factor2"), 16)
                        End If
                        e.Row.Cells(col).BackColor = cor

                        col += 1
                        If IsDBNull(drow("intermed_factor")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("intermed_factor"), 16)
                        End If
                        e.Row.Cells(col).BackColor = cor

                        col += 1
                        If IsDBNull(drow("resultado")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("resultado"), 8)
                        End If
                        e.Row.Cells(col).BackColor = cor

                        col += 1
                        If IsDBNull(drow("vlr_dia")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("vlr_dia"), 2)
                        End If
                        e.Row.Cells(col).BackColor = cor

                        col += 1
                        If IsDBNull(drow("atualiz_dia")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("atualiz_dia"), 2)
                        End If
                        e.Row.Cells(col).BackColor = cor

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
            Dim oData As New CDataHora(Convert.ToDateTime(txtDataAplicacao.Text))
            Dim valorCdi As String
            Dim rData As New CDataHora(Convert.ToDateTime(txtDataRef.Text))

            If txtValorCdi.Text = "" Then
                valorCdi = "0.0"
            Else
                valorCdi = txtValorCdi.Text.Replace(",", ".")
            End If

            If RTrim(txtAtivo.Text) = "" Then
                table = ClassBD.GetExibirGrid("[SCR_fin_operacao_proj] '" & oData.Data.ToString("yyyyMMdd") & "', '" & ddlCliente.SelectedValue & "', '" & ddlAtivo.SelectedValue & "', '" & rData.Data.ToString("yyyyMMdd") & "'," & valorCdi, "FinOperacao", strConn)
            Else
                table = ClassBD.GetExibirGrid("[SCR_fin_operacao_proj] '" & oData.Data.ToString("yyyyMMdd") & "', '" & ddlCliente.SelectedValue & "', '" & txtAtivo.Text & "', '" & rData.Data.ToString("yyyyMMdd") & "'," & valorCdi, "FinOperacao", strConn)
            End If

            Return table

        End Function


        Private Sub GetData2()

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim table As DataTable = Nothing
            Dim oData As New CDataHora(Convert.ToDateTime(txtDataAplicacao.Text))


            If ddlAtivo.Visible = false And RTrim(txtAtivo.Text) <> "" Then
                table = ClassBD.GetExibirGrid("[SCR_fin_operacao1] '" & RTrim(txtAtivo.Text) & "'", "FinOperacao", strConn)
            Else
                table = ClassBD.GetExibirGrid("[SCR_fin_operacao] '" & oData.Data.ToString("yyyyMMdd") & "', '" & ddlCliente.SelectedValue & "', '" & ddlAtivo.SelectedValue & "'", "FinOperacao", strConn)
            End If


            For Each row As DataRow In table.Rows
                txtDataVencimento.Text = row("dt_vcto")
                txtValorCaptado.Text = row("vlr_original")
                txtCorretor.Text = row("cod_gerente")
                txtTipoTaxa.Text = row("pre_cdi")
                txtCodigoPapel.Text = row("cod_papel")
                txtTaxaIntermed.Text = row("prc_cdi_real")
                txtTaxaCliente.Text = row("prc_cdi_real")
                txtTaxaFinal.Text = row("prc_cdi_real")
            Next row




        End Sub


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
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../Menu.aspx")
        End Sub

        Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
            Try

                GetData2()
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
                    Dim filename As String = String.Format("OperacaoCaptacao_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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

                        Response.Write("Operação Captação - Financeiro")

                        Response.Write("Vencto: " & txtDataVencimento.Text & " Valor Aplicado: " & txtValorCaptado.Text & " Gerente: " & txtCorretor.Text)
                        Response.Write("Tipo: " & txtTipoTaxa.Text & " Papel: " & txtCodigoPapel.Text & " Tx: " & txtTaxaIntermed.Text)

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


        Protected Sub chkCdi_OnCheckedChanged(sender As Object, e As EventArgs)
            txtValorCdi.ReadOnly = Not chkCdi.Checked
            If Not txtValorCdi.ReadOnly Then
                txtValorCdi.Focus()
            End If
        End Sub
    End Class
End Namespace