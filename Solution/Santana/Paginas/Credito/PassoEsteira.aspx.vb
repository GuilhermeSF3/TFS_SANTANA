Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic
Imports Santana.Seguranca
Imports Util

Namespace Paginas.Credito

    Public Class PassoEsteira

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                txtDataDe.Text = (DateTime.Now.AddDays(-1)).ToString("dd/MM/yyyy")
                txtDataAte.Text = (DateTime.Now.AddDays(-1)).ToString("dd/MM/yyyy")

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

                ddlLoja.Items.Insert(0, New ListItem("Todos", "99"))
                ddlAgente.Items.Insert(0, New ListItem("Todos", "99"))
                ddlProduto.Items.Insert(0, New ListItem("Todos", "99"))
                ddlAnalista.Items.Insert(0, New ListItem("Todos", "99"))
                ddlSituacao.Items.Insert(0, New ListItem("Todos", "99"))

                If ContextoWeb.DadosTransferencia.CodReanalise = 0 Then
                    Carrega_Reanalise()
                    Carrega_Alcada()
                    CarregaGerencia()
                Else
                    Carrega_Reanalise()
                    Carrega_Alcada()
                    CarregaGerencia()
                    ddlReanalise.SelectedIndex = ddlReanalise.Items.IndexOf(ddlProduto.Items.FindByValue(ContextoWeb.DadosTransferencia.CodReanalise.ToString()))
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

        Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

            Try

                If e.Row.RowType = DataControlRowType.DataRow Then
                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As DataRow = CType(e.Row.DataItem, DataRowView).Row
                        Dim col As Integer

                        col = 0
                        If IsDBNull(drow("ORDEM")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("ORDEM")
                        End If

                        col += 1
                        If IsDBNull(drow("PROCESSO")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("PROCESSO")
                        End If

                        col += 1
                        If IsDBNull(drow("PASSO")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("PASSO")
                        End If

                        col += 1
                        If IsDBNull(drow("TEMPO")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("TEMPO")
                        End If

                        col += 1
                        If IsDBNull(drow("TEMPO_HORA")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("TEMPO_HORA")
                        End If

                        col += 1
                        If IsDBNull(drow("TEMPO_PROPOSTA")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("TEMPO_PROPOSTA")
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


            table = ClassBD.GetExibirGrid("[SCR_PASSO_ESTEIRA] '" & oDataDe.Data.ToString("yyyyMMdd") & "', '" & oDataAte.Data.ToString("yyyyMMdd") & "', '" & ddlLoja.SelectedValue & "', '" & ddlAgente.SelectedValue &
                                          "', '" & ddlProduto.SelectedValue & "', '" & ddlAnalista.SelectedValue & "', '" & ddlSituacao.SelectedValue & "', '" & txtValorDe.Text & "', '" & txtValorAte.Text & "', '" & ddlReanalise.SelectedValue &
                                          "', " & ddlAlcada.SelectedValue & ", '" & ddlGerencia.SelectedValue & "'", "TempoPassoEsteira", strConn)

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
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Passo da Esteira' ,'Tempo total para cada passo da esteira.');", True)
        End Sub

        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../Menu.aspx")
        End Sub

        Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
            Try

                BindGridView1Data()

                If (ddlLoja.Text = "99") Then
                    Carrega_Lojas()
                End If

                If (ddlAgente.Text = "99") Then
                    Carrega_Agentes()
                End If

                If (ddlProduto.Text = "99") Then
                    Carrega_Produtos()
                End If

                If (ddlAnalista.Text = "99") Then
                    Carrega_Analistas()
                End If

                If (ddlSituacao.Text = "99") Then
                    Carrega_Situacao()
                End If

                Dim strConn1 As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim conexao As New SqlConnection(strConn1)

                Dim oDataDe As New CDataHora(Convert.ToDateTime(txtDataDe.Text))
                Dim oDataAte As New CDataHora(Convert.ToDateTime(txtDataAte.Text))

                Dim comando As SqlCommand = New SqlCommand(
                    "SCR_PASSO_ESTEIRA_CRED_TOTAL '" & oDataDe.Data.ToString("yyyyMMdd") & "', '" & oDataAte.Data.ToString("yyyyMMdd") & "'", conexao)

                comando.Connection.Open()
                Dim ddlValues As SqlDataReader
                ddlValues = comando.ExecuteReader()
                ddlValues.Read()
                txtQtde.Text = ddlValues("TOTAL").ToString()
                txtTempoMesa.Text = ddlValues("TEMPO_HORA_MESA").ToString()
                ddlValues.Close()
                comando.Connection.Close()
                comando.Connection.Dispose()
                conexao.Close()

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
                    Dim filename As String = String.Format("PassoEsteiraCredito_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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

                        Response.Write("Relatório Tempo Total - Passo da Esteira Crédito: " & txtDataDe.Data & " ATÉ: " & txtDataAte.Data)

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

        Private Sub Carrega_Reanalise()

            Try

                ddlReanalise.Items.Insert(0, New ListItem("Todos", "99"))
                ddlReanalise.Items.Insert(1, New ListItem("1a Análise", "0"))
                ddlReanalise.Items.Insert(2, New ListItem("Reanálise", "1"))

                ddlReanalise.SelectedIndex = 0


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub Carrega_Alcada()

            Try

                ddlAlcada.Items.Insert(0, New ListItem("Todos", "99"))
                ddlAlcada.Items.Insert(1, New ListItem("Alçada B", "0"))
                ddlAlcada.Items.Insert(2, New ListItem("Alçada A", "1"))
                ddlAlcada.Items.Insert(3, New ListItem("Alçada A1", "2"))
                ddlAlcada.Items.Insert(4, New ListItem("Alçada AA", "3"))
                ddlAlcada.Items.Insert(5, New ListItem("Alçada AA2", "4"))

                ddlAlcada.SelectedIndex = 0


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub CarregaGerencia()

            Try

                ddlGerencia.Items.Insert(0, New ListItem("Todos", "99"))
                ddlGerencia.Items.Insert(1, New ListItem("São Paulo", "1"))
                ddlGerencia.Items.Insert(2, New ListItem("Interior", "2"))

                ddlGerencia.SelectedIndex = 0


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub
        Private Sub Carrega_Lojas()

            Try

                Dim oDataDe As New CDataHora(Convert.ToDateTime(txtDataDe.Text))
                Dim oDataAte As New CDataHora(Convert.ToDateTime(txtDataAte.Text))

                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)
                Dim command As SqlCommand = New SqlCommand(
                "SELECT DISTINCT PPCODORG4 AS O4CODORG, O4DESCR AS O4NOME FROM PROPWEBSMC..EPROP (NOLOCK) INNER JOIN PROPWEBSMC..CPROP (NOLOCK) ON EPNRPROP = PPNRPROP " &
                    "INNER JOIN CDCSANTANAMICROCREDITO..TORG4 (NOLOCK) ON PPCODORG4 = O4CODORG " &
                    "WHERE EPDTLNC BETWEEN '" & oDataDe.Data.ToString("yyyyMMdd") & "' AND '" & oDataAte.Data.ToString("yyyyMMdd") & "' AND PPCODPRD <> '000221' " &
                    "AND EPNRDECI IN ('120') " &
                    "ORDER BY O4NOME", connection)

                command.Connection.Open()

                Dim ddlValues As SqlDataReader
                ddlValues = command.ExecuteReader()

                ddlLoja.DataSource = ddlValues
                ddlLoja.DataValueField = "O4CODORG"
                ddlLoja.DataTextField = "O4NOME"
                ddlLoja.DataBind()

                ddlLoja.Items.Insert(0, New ListItem("Todos", "99"))
                ddlLoja.SelectedIndex = 0

                ddlValues.Close()
                command.Connection.Close()
                command.Connection.Dispose()
                connection.Close()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub Carrega_Agentes()

            Try

                Dim oDataDe As New CDataHora(Convert.ToDateTime(txtDataDe.Text))
                Dim oDataAte As New CDataHora(Convert.ToDateTime(txtDataAte.Text))

                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)
                Dim command As SqlCommand = New SqlCommand(
                "SELECT DISTINCT PPCODORG3 AS O3CODORG, O3DESCR AS O3NOME FROM PROPWEBSMC..EPROP (NOLOCK) INNER JOIN PROPWEBSMC..CPROP (NOLOCK) ON EPNRPROP = PPNRPROP " &
                    "INNER JOIN CDCSANTANAMICROCREDITO..TORG3 (NOLOCK) ON PPCODORG3 = O3CODORG " &
                    "WHERE EPDTLNC BETWEEN '" & oDataDe.Data.ToString("yyyyMMdd") & "' AND '" & oDataAte.Data.ToString("yyyyMMdd") & "' AND PPCODPRD <> '000221' " &
                    "AND EPNRDECI IN ('120') " &
                    "ORDER BY O3NOME", connection)

                command.Connection.Open()

                Dim ddlValues As SqlDataReader
                ddlValues = command.ExecuteReader()

                ddlAgente.DataSource = ddlValues
                ddlAgente.DataValueField = "O3CODORG"
                ddlAgente.DataTextField = "O3NOME"
                ddlAgente.DataBind()

                ddlAgente.Items.Insert(0, New ListItem("Todos", "99"))
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

        Private Sub Carrega_Produtos()

            Try

                Dim oDataDe As New CDataHora(Convert.ToDateTime(txtDataDe.Text))
                Dim oDataAte As New CDataHora(Convert.ToDateTime(txtDataAte.Text))

                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)
                Dim command As SqlCommand = New SqlCommand(
                "SELECT DISTINCT PPCODPRD AS CODPRD, TPDESCR AS PRDNOME FROM PROPWEBSMC..EPROP (NOLOCK) INNER JOIN PROPWEBSMC..CPROP (NOLOCK) On EPNRPROP = PPNRPROP " &
                "INNER JOIN CDCSANTANAMICROCREDITO..TPROD (NOLOCK) ON PPCODPRD = TPCODPRD " &
                "WHERE EPDTLNC BETWEEN '" & oDataDe.Data.ToString("yyyyMMdd") & "' AND '" & oDataAte.Data.ToString("yyyyMMdd") & "' AND PPCODPRD <> '000221' " &
                "AND EPNRDECI IN ('120') " &
                "ORDER BY PRDNOME", connection)

                command.Connection.Open()

                Dim ddlValues As SqlDataReader
                ddlValues = command.ExecuteReader()

                ddlProduto.DataSource = ddlValues
                ddlProduto.DataValueField = "CODPRD"
                ddlProduto.DataTextField = "PRDNOME"
                ddlProduto.DataBind()

                ddlProduto.Items.Insert(0, New ListItem("Todos", "99"))
                ddlProduto.SelectedIndex = 0

                ddlValues.Close()
                command.Connection.Close()
                command.Connection.Dispose()
                connection.Close()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub Carrega_Situacao()

            Try

                Dim oDataDe As New CDataHora(Convert.ToDateTime(txtDataDe.Text))
                Dim oDataAte As New CDataHora(Convert.ToDateTime(txtDataAte.Text))

                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)
                Dim command As SqlCommand = New SqlCommand(
                "SELECT DISTINCT MPSIT AS SITCOD, MPSIT AS SITNOME FROM PROPWEBSMC..EPROP E (NOLOCK) " &
                "INNER JOIN PROPWEBSMC..CPROP (NOLOCK) ON E.EPNRPROP = PPNRPROP INNER JOIN PROPWEBSMC..CMOVP (NOLOCK) ON PPNRPROP = MPNRPROP " &
                "WHERE E.EPDTLNC BETWEEN '" & oDataDe.Data.ToString("yyyyMMdd") & "' AND '" & oDataAte.Data.ToString("yyyyMMdd") & "' AND PPCODPRD <> '000221' " &
                "AND E.EPNRDECI IN ('120') " &
                "ORDER BY SITNOME", connection)

                command.Connection.Open()

                Dim ddlValues As SqlDataReader
                ddlValues = command.ExecuteReader()

                ddlSituacao.DataSource = ddlValues
                ddlSituacao.DataValueField = "SITCOD"
                ddlSituacao.DataTextField = "SITNOME"
                ddlSituacao.DataBind()

                ddlSituacao.Items.Insert(0, New ListItem("Todos", "99"))
                ddlSituacao.SelectedIndex = 0

                ddlValues.Close()
                command.Connection.Close()
                command.Connection.Dispose()
                connection.Close()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub Carrega_Analistas()

            Try

                Dim oDataDe As New CDataHora(Convert.ToDateTime(txtDataDe.Text))
                Dim oDataAte As New CDataHora(Convert.ToDateTime(txtDataAte.Text))

                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)
                Dim command As SqlCommand = New SqlCommand(
                "SELECT DISTINCT EPUSUARIO AS EPUSUARIO, EPUSUARIO AS NOME  FROM PROPWEBSMC..EPROP E (NOLOCK) INNER JOIN PROPWEBSMC..CPROP (NOLOCK) ON EPNRPROP = PPNRPROP " &
                    "INNER JOIN CDCSANTANAMICROCREDITO..TPROD (NOLOCK) ON PPCODPRD = TPCODPRD " &
                    "WHERE EPDTLNC BETWEEN '" & oDataDe.Data.ToString("yyyyMMdd") & "' AND '" & oDataAte.Data.ToString("yyyyMMdd") & "' AND PPCODPRD <> '000221' " &
                    "AND EPNRDECI IN ('120') " &
                    "AND EPDTLNC = (SELECT MAX(E1.EPDTLNC) FROM PROPWEBSMC..EPROP E1 (NOLOCK) WHERE E1.EPNRPROP = E.EPNRPROP AND E.EPNRDECI IN ('120')) " &
                    "AND EPUSUARIO IS NOT NULL " &
                    "ORDER BY E.EPUSUARIO", connection)

                command.Connection.Open()

                Dim ddlValues As SqlDataReader
                ddlValues = command.ExecuteReader()

                ddlAnalista.DataSource = ddlValues
                ddlAnalista.DataValueField = "EPUSUARIO"
                ddlAnalista.DataTextField = "NOME"
                ddlAnalista.DataBind()

                ddlAnalista.Items.Insert(0, New ListItem("Todos", "99"))
                ddlAnalista.SelectedIndex = 0

                ddlValues.Close()
                command.Connection.Close()
                command.Connection.Dispose()
                connection.Close()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

    End Class
End Namespace