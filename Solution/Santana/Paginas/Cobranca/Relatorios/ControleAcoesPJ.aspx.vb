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


Namespace Paginas.Cobranca.Relatorios

    Public Class ControleAcoesPJ

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                txtDataRef.Text = (DateTime.Now.AddDays(-1)).ToString("dd/MM/yyyy")
                Dim primeiroDiaMesCorrente As Date = Convert.ToDateTime("01/" + Now.Date.ToString("MM/yyyy"))
                txtDtDe.Text = primeiroDiaMesCorrente
                txtDtAte.Text = (DateTime.Now.ToString("dd/MM/yyyy"))

                Carrega_Escritorio()
                Carrega_Status()
                Carrega_Autor()
                Carrega_Pessoa()
                Carrega_Acao()

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

        Protected Sub btnRefAnt_Click(sender As Object, e As EventArgs)

            Dim diaAnterior As Date = Convert.ToDateTime(txtDataRef.Text)
            diaAnterior = diaAnterior.AddDays(-1)
            txtDataRef.Text = diaAnterior.ToString("dd/MM/yyyy")

        End Sub

        Protected Sub btnRefPro_Click(sender As Object, e As EventArgs)

            Dim proximoDia As Date = Convert.ToDateTime(txtDataRef.Text)
            proximoDia = proximoDia.AddDays(+1)
            txtDataRef.Text = proximoDia.ToString("dd/MM/yyyy")

        End Sub

        Protected Sub btnDtDeAnt_Click(sender As Object, e As EventArgs)

            Dim diaAnterior As Date = Convert.ToDateTime(txtDtDe.Text)
            diaAnterior = diaAnterior.AddDays(-1)
            txtDtDe.Text = diaAnterior.ToString("dd/MM/yyyy")

        End Sub

        Protected Sub btnDtDePro_Click(sender As Object, e As EventArgs)

            Dim proximoDia As Date = Convert.ToDateTime(txtDtDe.Text)
            proximoDia = proximoDia.AddDays(+1)
            txtDtDe.Text = proximoDia.ToString("dd/MM/yyyy")

        End Sub

        Protected Sub btnDtAteAnt_Click(sender As Object, e As EventArgs)

            Dim diaAnterior As Date = Convert.ToDateTime(txtDtAte.Text)
            diaAnterior = diaAnterior.AddDays(-1)
            txtDtAte.Text = diaAnterior.ToString("dd/MM/yyyy")

        End Sub

        Protected Sub btnDtAtePro_Click(sender As Object, e As EventArgs)

            Dim proximoDia As Date = Convert.ToDateTime(txtDtAte.Text)
            proximoDia = proximoDia.AddDays(+1)
            txtDtAte.Text = proximoDia.ToString("dd/MM/yyyy")

        End Sub

        Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

            Try

                If e.Row.RowType = DataControlRowType.DataRow Then
                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

                        If IsDBNull(drow("DATAREF")) Then
                            e.Row.Cells(0).Text = ""
                        Else
                            e.Row.Cells(0).Text = drow("DATAREF")
                        End If

                        If IsDBNull(drow("TITPROC")) Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = drow("TITPROC")
                        End If

                        If IsDBNull(drow("NUMPROC")) Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = drow("NUMPROC")
                        End If

                        If IsDBNull(drow("ESCRITORIO")) Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = drow("ESCRITORIO")
                        End If

                        If IsDBNull(drow("VALOR_CAUSA")) Then
                            e.Row.Cells(4).Text = ""
                        Else
                            e.Row.Cells(4).Text = CNumero.FormataNumero(drow("VALOR_CAUSA"), 2)
                        End If

                        If IsDBNull(drow("CUSTAS_PROCESSUAIS")) Then
                            e.Row.Cells(5).Text = ""
                        Else
                            e.Row.Cells(5).Text = CNumero.FormataNumero(drow("CUSTAS_PROCESSUAIS"), 2)
                        End If

                        If IsDBNull(drow("HONORARIOS")) Then
                            e.Row.Cells(6).Text = ""
                        Else
                            e.Row.Cells(6).Text = CNumero.FormataNumero(drow("HONORARIOS"), 2)
                        End If

                        If IsDBNull(drow("STATUS")) Then
                            e.Row.Cells(7).Text = ""
                        Else
                            e.Row.Cells(7).Text = drow("STATUS")
                        End If

                        If IsDBNull(drow("DATA_ENTRADA")) Then
                            e.Row.Cells(8).Text = ""
                        Else
                            e.Row.Cells(8).Text = drow("DATA_ENTRADA")
                        End If

                        If IsDBNull(drow("TIPO_PESSOA")) Then
                            e.Row.Cells(9).Text = ""
                        Else
                            e.Row.Cells(9).Text = drow("TIPO_PESSOA")
                        End If

                        If IsDBNull(drow("TIPO_AUTOR")) Then
                            e.Row.Cells(10).Text = ""
                        Else
                            e.Row.Cells(10).Text = drow("TIPO_AUTOR")
                        End If

                        If IsDBNull(drow("TIPO_ACAO")) Then
                            e.Row.Cells(11).Text = ""
                        Else
                            e.Row.Cells(11).Text = drow("TIPO_ACAO")
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
            Dim table As DataTable

            table = Util.ClassBD.GetExibirGrid("[SCR_CONTROLE_PJ] '" & Convert.ToDateTime(txtDataRef.Text).ToString("yyyyMMdd") & "', '" & txtProcesso.Text & "', '" & ddlEscritorio.Text.Trim & "', '" &
                                               ddlStatus.SelectedValue & "', '" & Convert.ToDateTime(txtDtDe.Text).ToString("yyyyMMdd") & "', '" & Convert.ToDateTime(txtDtAte.Text).ToString("yyyyMMdd") & "', '" &
                                               ddlTipoPessoa.SelectedValue & "', '" & ddlTipoAutor.SelectedValue & "', '" & ddlAcao.SelectedValue & "'", "ControleAcoesPJ", strConn)

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
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../../Menu.aspx")
        End Sub

        Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
            Try

                BindGridView1Data()

                Dim strConn1 As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim conexao As New SqlConnection(strConn1)

                Dim comando As SqlCommand = New SqlCommand(
                    "SCR_CONTROLE_TTL_PJ  '" & Convert.ToDateTime(txtDataRef.Text).ToString("yyyyMMdd") & "', '" & txtProcesso.Text & "', '" & ddlEscritorio.Text.Trim & "', '" &
                                               ddlStatus.SelectedValue & "', '" & Convert.ToDateTime(txtDtDe.Text).ToString("yyyyMMdd") & "', '" & Convert.ToDateTime(txtDtAte.Text).ToString("yyyyMMdd") & "', '" &
                                               ddlTipoPessoa.SelectedValue & "', '" & ddlTipoAutor.SelectedValue & "', '" & ddlAcao.SelectedValue & "'", conexao)

                comando.Connection.Open()
                Dim ddlValues As SqlDataReader
                ddlValues = comando.ExecuteReader()
                ddlValues.Read()
                txtVlrCausa.Text = CNumero.FormataNumero(ddlValues("VALOR_CAUSA").ToString(), 2)
                txtCustaProc.Text = CNumero.FormataNumero(ddlValues("CUSTAS_PROCESSUAIS").ToString(), 2)
                txtHonorarios.Text = CNumero.FormataNumero(ddlValues("HONORARIOS").ToString(), 2)
                ddlValues.Close()
                comando.Connection.Close()
                comando.Connection.Dispose()
                conexao.Close()

                Dim strConn2 As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim conexao1 As New SqlConnection(strConn2)

                Dim comando1 As SqlCommand = New SqlCommand(
                    "SCR_CONTROLE_TTL_ENC_PJ  '" & Convert.ToDateTime(txtDataRef.Text).ToString("yyyyMMdd") & "', '" & txtProcesso.Text & "', '" & ddlEscritorio.Text.Trim & "', '" &
                                               ddlStatus.SelectedValue & "', '" & Convert.ToDateTime(txtDtDe.Text).ToString("yyyyMMdd") & "', '" & Convert.ToDateTime(txtDtAte.Text).ToString("yyyyMMdd") & "', '" &
                                               ddlTipoPessoa.SelectedValue & "', '" & ddlTipoAutor.SelectedValue & "', '" & ddlAcao.SelectedValue & "'", conexao1)

                comando1.Connection.Open()
                Dim ddlValues1 As SqlDataReader
                ddlValues1 = comando1.ExecuteReader()
                ddlValues1.Read()
                txtVlrCausaE.Text = CNumero.FormataNumero(ddlValues1("VALOR_CAUSA").ToString(), 2)
                txtCustaProcE.Text = CNumero.FormataNumero(ddlValues1("CUSTAS_PROCESSUAIS").ToString(), 2)
                txtHonorariosE.Text = CNumero.FormataNumero(ddlValues1("HONORARIOS").ToString(), 2)
                ddlValues1.Close()
                comando1.Connection.Close()
                comando1.Connection.Dispose()
                conexao1.Close()


                Dim strConn3 As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim conexao2 As New SqlConnection(strConn3)

                Dim comando2 As SqlCommand = New SqlCommand(
                    "SCR_CONTROLE_TTL_GERAL_PJ  '" & Convert.ToDateTime(txtDataRef.Text).ToString("yyyyMMdd") & "', '" & txtProcesso.Text & "', '" & ddlEscritorio.Text.Trim & "', '" &
                                               ddlStatus.SelectedValue & "', '" & Convert.ToDateTime(txtDtDe.Text).ToString("yyyyMMdd") & "', '" & Convert.ToDateTime(txtDtAte.Text).ToString("yyyyMMdd") & "', '" &
                                               ddlTipoPessoa.SelectedValue & "', '" & ddlTipoAutor.SelectedValue & "', '" & ddlAcao.SelectedValue & "'", conexao2)

                comando2.Connection.Open()
                Dim ddlValues2 As SqlDataReader
                ddlValues2 = comando2.ExecuteReader()
                ddlValues2.Read()
                txtVlrCausaS.Text = CNumero.FormataNumero(ddlValues2("VALOR_CAUSA").ToString(), 2)
                txtCustaProcS.Text = CNumero.FormataNumero(ddlValues2("CUSTAS_PROCESSUAIS").ToString(), 2)
                txtHonorariosS.Text = CNumero.FormataNumero(ddlValues2("HONORARIOS").ToString(), 2)
                ddlValues2.Close()
                comando2.Connection.Close()
                comando2.Connection.Dispose()
                conexao2.Close()

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
                    Dim filename As String = String.Format("ControleDeAcoes_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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
                        Response.Write("Controle de Ações")
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

        Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

        End Sub

        Private Sub Carrega_Escritorio()

            Try

                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)
                Dim command As SqlCommand = New SqlCommand(
                "SELECT DISTINCT ESCRITORIO AS CODESC, ESCRITORIO AS NOMEESC FROM AUX_CONTROLE_ACOES_PJ (NOLOCK) " &
                "ORDER BY ESCRITORIO", connection)

                command.Connection.Open()

                Dim ddlValues As SqlDataReader
                ddlValues = command.ExecuteReader()

                ddlEscritorio.DataSource = ddlValues
                ddlEscritorio.DataValueField = "CODESC"
                ddlEscritorio.DataValueField = "NOMEESC"
                ddlEscritorio.DataBind()

                ddlEscritorio.Items.Insert(0, New ListItem("Todos", "99"))
                ddlEscritorio.SelectedIndex = 0

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

                ddlStatus.Items.Insert(0, New ListItem("Todos", "99"))
                ddlStatus.Items.Insert(1, New ListItem("Aberto", "ABERTO"))
                ddlStatus.Items.Insert(2, New ListItem("Encerrado", "ENCERRADO"))

                ddlStatus.SelectedIndex = 0

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub Carrega_Pessoa()
            Try

                ddlTipoPessoa.Items.Insert(0, New ListItem("Todos", "99"))
                ddlTipoPessoa.Items.Insert(1, New ListItem("PF", "PF"))
                ddlTipoPessoa.Items.Insert(2, New ListItem("PJ", "PJ"))

                ddlTipoPessoa.SelectedIndex = 0

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub Carrega_Autor()
            Try

                ddlTipoAutor.Items.Insert(0, New ListItem("Todos", "99"))
                ddlTipoAutor.Items.Insert(1, New ListItem("Autor", "AUTOR"))
                ddlTipoAutor.Items.Insert(2, New ListItem("Réu", "RÉU"))

                ddlTipoPessoa.SelectedIndex = 0

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub Carrega_Acao()

            Try

                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)
                Dim command As SqlCommand = New SqlCommand(
                "SELECT DISTINCT TIPO_ACAO AS CODACAO, TIPO_ACAO AS NOMEACAO FROM AUX_CONTROLE_ACOES_PJ (NOLOCK) " &
                "ORDER BY TIPO_ACAO", connection)

                command.Connection.Open()

                Dim ddlValues As SqlDataReader
                ddlValues = command.ExecuteReader()

                ddlAcao.DataSource = ddlValues
                ddlAcao.DataValueField = "CODACAO"
                ddlAcao.DataValueField = "NOMEACAO"
                ddlAcao.DataBind()

                ddlAcao.Items.Insert(0, New ListItem("Todos", "99"))
                ddlAcao.SelectedIndex = 0

                ddlValues.Close()
                command.Connection.Close()
                command.Connection.Dispose()
                connection.Close()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
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

    End Class
End Namespace