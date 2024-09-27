Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports Util
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.IO
Imports System.Web
Imports System.Web.Security
Imports System.Drawing.Printing
Imports System.Data.Common
Imports System
Imports System.Configuration
Imports System.Drawing

Imports Santana.Seguranca

Public Class SpreadAnaliticoDesconto

    Inherits SantanaPage

    Public safra_prod As Integer = 1

    Dim strSortField As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            'Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
            'txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)

            'txtDataDE.Text = (Convert.ToDateTime(txtData.Text).AddMonths(-2)).ToString("dd/MM/yyyy")
            'txtDataDE.Text = "01/" + Right(txtDataDE.Text, 7)

            If Today.Day > "25" Then
                txtDataDE.Text = Convert.ToDateTime("26/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
                txtData.Text = Convert.ToDateTime("25/" + Now.Date.ToString("MM/yyyy"))
            Else
                txtDataDE.Text = Convert.ToDateTime("26/" + Now.Date.AddMonths(-2).ToString("MM/yyyy"))
                txtData.Text = Convert.ToDateTime("25/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
            End If

            safra_prod = 1

            If ContextoWeb.DadosTransferencia.CodAgente = 0 Then
                Carrega_Agente()
            Else
                Carrega_Agente()
                ddlAgente.SelectedIndex = ddlAgente.Items.IndexOf(ddlAgente.Items.FindByValue(ContextoWeb.DadosTransferencia.CodAgente.ToString()))
            End If

            Carrega_DtRisco()

            If ContextoWeb.DadosTransferencia.CodCobradora <> 0 Or ContextoWeb.DadosTransferencia.CodAgente <> 0 Then
                BindGridView1Data()
                ContextoWeb.DadosTransferencia.CodAgente = 0
                ContextoWeb.DadosTransferencia.CodCobradora = 0
            End If

        End If

        txtData.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnCarregar.UniqueID + "').click();return false;}} else {return true}; ")

        Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)


    End Sub


    Protected Sub btnDataAnterior_Click(sender As Object, e As EventArgs)

        Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtData.Text)
        UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(-1)

        If UltimoDiaMesAnterior.Year = Now.Date.Year Then
            If UltimoDiaMesAnterior.Month <= Now.Date.Month Then
                txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
            End If
        ElseIf UltimoDiaMesAnterior.Year < Now.Date.Year Then
            txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
        End If

    End Sub

    Protected Sub btnDataDeAnterior_Click(sender As Object, e As EventArgs)

        txtDataDE.Text = (Convert.ToDateTime(txtDataDE.Text).AddDays(-1)).ToString("dd/MM/yyyy")

    End Sub



    Protected Sub btnProximaData_Click(sender As Object, e As EventArgs)

        Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtData.Text)
        UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(1)

        If UltimoDiaMesAnterior.Year = Now.Date.Year Then
            If UltimoDiaMesAnterior.Month <= Now.Date.Month Then
                txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
            End If
        ElseIf UltimoDiaMesAnterior.Year < Now.Date.Year Then
            txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
        End If

    End Sub

    Protected Sub btnProximaDataDe_Click(sender As Object, e As EventArgs)

        txtDataDE.Text = (Convert.ToDateTime(txtDataDE.Text).AddDays(1)).ToString("dd/MM/yyyy")

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

    Private Sub Carrega_Agente()
        Try

            ddlAgente.Items.Insert(0, New ListItem("Produção", "1"))
            ddlAgente.SelectedIndex = 0


        Catch ex As Exception

        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Carrega_DtRisco()
        Try

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim connection As New SqlConnection(strConn)
            Dim command As SqlCommand = New SqlCommand(
            "SELECT CONVERT(CHAR,MAX(DATA_REF),103) AS DT_RISCO FROM TBL_RISCO_DESCONTO (NOLOCK)", connection)

            command.Connection.Open()

            Dim ddlValues As SqlDataReader
            ddlValues = command.ExecuteReader()

            If ddlValues.HasRows Then
                ddlValues.Read()
                txtDtRisco.Text = ddlValues.GetValue(0).ToString().Trim
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

    Private Sub GridViewRiscoAnalitico_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewRiscoAnalitico.RowDataBound

        Try


            If e.Row.RowType = DataControlRowType.DataRow Then

                If Not e.Row.DataItem Is Nothing Then

                    Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

                    If IsDBNull(drow("DT_FECHA")) Then
                        e.Row.Cells(0).Text = ""
                    Else
                        e.Row.Cells(0).Text = drow("DT_FECHA")
                    End If

                    If IsDBNull(drow("NUM_OPE")) Then
                        e.Row.Cells(1).Text = ""
                    Else
                        e.Row.Cells(1).Text = drow("NUM_OPE")
                    End If

                    If IsDBNull(drow("DT_BASE")) Then
                        e.Row.Cells(2).Text = ""
                    Else
                        e.Row.Cells(2).Text = drow("DT_BASE")
                    End If

                    If IsDBNull(drow("VLR_OP")) OrElse drow("VLR_OP") = 0 Then
                        e.Row.Cells(3).Text = ""
                    Else
                        e.Row.Cells(3).Text = CNumero.FormataNumero(drow("VLR_OP"), 4)
                    End If

                    If IsDBNull(drow("VLR_PARC")) OrElse drow("VLR_PARC") = 0 Then
                        e.Row.Cells(4).Text = ""
                    Else
                        e.Row.Cells(4).Text = CNumero.FormataNumero(drow("VLR_PARC"), 4)
                    End If

                    If IsDBNull(drow("PRAZO")) OrElse drow("PRAZO") = 0 Then
                        e.Row.Cells(5).Text = ""
                    Else
                        e.Row.Cells(5).Text = drow("PRAZO")
                    End If

                    If IsDBNull(drow("PLANO")) OrElse drow("PLANO") = 0 Then
                        e.Row.Cells(6).Text = ""
                    Else
                        e.Row.Cells(6).Text = drow("PLANO")
                    End If

                    If IsDBNull(drow("VLR_ATRASO")) OrElse drow("VLR_ATRASO") = 0 Then
                        e.Row.Cells(7).Text = ""
                    Else
                        e.Row.Cells(7).Text = CNumero.FormataNumero(drow("VLR_ATRASO"), 4)
                    End If

                    If IsDBNull(drow("VLR_VINCENDO")) OrElse drow("VLR_VINCENDO") = 0 Then
                        e.Row.Cells(8).Text = ""
                    Else
                        e.Row.Cells(8).Text = CNumero.FormataNumero(drow("VLR_VINCENDO"), 4)
                    End If

                    If IsDBNull(drow("TAXA")) OrElse drow("TAXA") = 0 Then
                        e.Row.Cells(9).Text = ""
                    Else
                        e.Row.Cells(9).Text = CNumero.FormataNumero(drow("TAXA"), 4)
                    End If

                    If IsDBNull(drow("ATRASO")) OrElse drow("ATRASO") = 0 Then
                        e.Row.Cells(10).Text = ""
                    Else
                        e.Row.Cells(10).Text = drow("ATRASO")
                    End If

                    If IsDBNull(drow("RATING")) Then
                        e.Row.Cells(11).Text = ""
                    Else
                        e.Row.Cells(11).Text = drow("RATING")
                    End If

                    If IsDBNull(drow("CODCLI")) Then
                        e.Row.Cells(12).Text = ""
                    Else
                        e.Row.Cells(12).Text = drow("CODCLI")
                    End If

                    If IsDBNull(drow("CODPROD")) OrElse drow("CODPROD") = 0 Then
                        e.Row.Cells(13).Text = ""
                    Else
                        e.Row.Cells(13).Text = drow("CODPROD")
                    End If

                    If IsDBNull(drow("CODMODA")) OrElse drow("CODMODA") = 0 Then
                        e.Row.Cells(14).Text = ""
                    Else
                        e.Row.Cells(14).Text = drow("CODMODA")
                    End If

                    If IsDBNull(drow("OPERADOR")) Then
                        e.Row.Cells(15).Text = ""
                    Else
                        e.Row.Cells(15).Text = drow("OPERADOR")
                    End If

                    If IsDBNull(drow("PRODUTO")) Then
                        e.Row.Cells(16).Text = ""
                    Else
                        e.Row.Cells(16).Text = drow("PRODUTO")
                    End If

                    If IsDBNull(drow("TX_X_VLR")) OrElse drow("TX_X_VLR") = 0 Then
                        e.Row.Cells(17).Text = ""
                    Else
                        e.Row.Cells(17).Text = CNumero.FormataNumero(drow("TX_X_VLR"), 4)
                    End If

                    If IsDBNull(drow("PRZ_X_VLR")) OrElse drow("PRZ_X_VLR") = 0 Then
                        e.Row.Cells(18).Text = ""
                    Else
                        e.Row.Cells(18).Text = CNumero.FormataNumero(drow("PRZ_X_VLR"), 4)
                    End If

                    If IsDBNull(drow("vf")) OrElse drow("vf") = 0 Then
                        e.Row.Cells(19).Text = ""
                    Else
                        e.Row.Cells(19).Text = CNumero.FormataNumero(drow("vf"), 4)
                    End If

                    If IsDBNull(drow("vlr_risco")) Or drow("vlr_risco") = 0 Then
                        e.Row.Cells(20).Text = ""
                    Else
                        e.Row.Cells(20).Text = CNumero.FormataNumero(drow("vlr_risco"), 4)
                    End If

                    If IsDBNull(drow("SLD_CONTABIL")) Or drow("SLD_CONTABIL") = 0 Then
                        e.Row.Cells(21).Text = ""
                    Else
                        e.Row.Cells(21).Text = CNumero.FormataNumero(drow("SLD_CONTABIL"), 2)
                    End If

                    If IsDBNull(drow("SCORE")) Or drow("SCORE") = 0 Then
                        e.Row.Cells(22).Text = ""
                    Else
                        e.Row.Cells(22).Text = CNumero.FormataNumero(drow("SCORE"), 0)
                    End If

                    If IsDBNull(drow("qtd_Parc_Avencer")) Then
                        e.Row.Cells(23).Text = ""
                    Else
                        e.Row.Cells(23).Text = drow("qtd_Parc_Avencer")
                    End If

                    If IsDBNull(drow("qtd_Parc_Abertas")) Then
                        e.Row.Cells(24).Text = ""
                    Else
                        e.Row.Cells(24).Text = drow("qtd_Parc_Abertas")
                    End If

                    If IsDBNull(drow("qtd_Parc_Pagas")) Then
                        e.Row.Cells(25).Text = ""
                    Else
                        e.Row.Cells(25).Text = drow("qtd_Parc_Pagas")
                    End If

                    If IsDBNull(drow("Tx_Cliente_am")) Then
                        e.Row.Cells(26).Text = ""
                    Else
                        e.Row.Cells(26).Text = CNumero.FormataNumero(drow("Tx_Cliente_am"), 2)
                    End If

                    If IsDBNull(drow("CPF_Cliente")) Then
                        e.Row.Cells(27).Text = ""
                    Else
                        e.Row.Cells(27).Text = drow("CPF_Cliente")
                    End If

                    If IsDBNull(drow("Dt_vcto_contrato")) Then
                        e.Row.Cells(28).Text = ""
                    Else
                        e.Row.Cells(28).Text = drow("Dt_vcto_contrato")
                    End If

                    If IsDBNull(drow("Dt_Liq_Oper")) Then
                        e.Row.Cells(29).Text = ""
                    Else
                        e.Row.Cells(29).Text = drow("Dt_Liq_Oper")
                    End If

                    If IsDBNull(drow("PMT")) Then
                        e.Row.Cells(30).Text = ""
                    Else
                        e.Row.Cells(30).Text = drow("PMT")
                    End If

                    If IsDBNull(drow("SPREAD_AUX")) Then
                        e.Row.Cells(31).Text = ""
                    Else
                        e.Row.Cells(31).Text = drow("SPREAD_AUX")
                    End If
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

        Select Case ddlAgente.SelectedValue
            Case 1
                GridViewRiscoAnalitico.DataSource = GetData()
        End Select

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
            codGerente = "0"
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim table As DataTable

        table = Util.ClassBD.GetExibirGrid("[scr_Spread_DESCONTO_analitico_prod] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', '" & Right(txtDataDE.Text, 4) & Mid(txtDataDE.Text, 4, 2) & Left(txtDataDE.Text, 2) & "', '" & Right(txtDtRisco.Text, 4) & Mid(txtDtRisco.Text, 4, 2) & Left(txtDtRisco.Text, 2) & "'", "SPREADANALITICODESCONTO", strConn)

        Return table

    End Function

    Protected Sub GridViewRiscoAnalitico_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

        GridViewRiscoAnalitico.PageIndex = IIf(e.NewPageIndex < 0, 0, e.NewPageIndex)
        BindGridView1DataView()
    End Sub




    Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)


        'ContextoWeb.DadosTransferencia.CodAgente = ddlAgente.SelectedValue
        'ContextoWeb.DadosTransferencia.Agente = ddlAgente.SelectedItem.ToString()

        'ContextoWeb.DadosTransferencia.CodCobradora = ddlCobradora.SelectedValue
        'ContextoWeb.DadosTransferencia.Cobradora = ddlCobradora.SelectedItem.ToString()


        'Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

        'Dim ds As dsRollrateMensal
        'Dim cmd As New SqlCommand("[scr_RR_mensal] '" & Convert.ToDateTime(txtData.Text).ToString("MM/dd/yy") & "', '" & ddlAgente.SelectedValue & "','" & ddlCobradora.SelectedValue & "'")
        'Using con As New SqlConnection(strConn)
        '    Using sda As New SqlDataAdapter()
        '        cmd.Connection = con
        '        sda.SelectCommand = cmd
        '        ds = New dsRollrateMensal()
        '        sda.Fill(ds, "RR_ROLLRATE_RPT")
        '    End Using
        'End Using

        '' ContextoWeb.NewReportContext()
        'ContextoWeb.Relatorio.reportFileName = "~/Relatorios/rptRollrateMensal.rpt"
        'ContextoWeb.Relatorio.reportDatas.Add(New reportData(ds))

        'ContextoWeb.Navegacao.LinkPaginaAnteriorRelatorio = Me.AppRelativeVirtualPath
        '' ContextoWeb.Navegacao.TituloPaginaAtual = Me.Title
        'Response.Redirect("Relatorio.aspx")

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
    End Sub


    Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('PDD Mensal' ,'Fechamento da PDD mensal, quebra por FILTRO selecionado na lista. Ordenação crescente: clique no cabeçalho da coluna. Descrescente: 2o. clique.');", True)
    End Sub


    Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
        Response.Redirect("Menu.aspx")
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
                Dim filename As String = String.Format("SPREAD_ANALITICO_DESCONTO_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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

                    Select Case ddlAgente.SelectedValue
                        Case 1
                            Response.Write("Produção De " & txtDataDE.Text & " Até " & txtData.Text & " - Relatório Spread - Risco Analítico Descont -  Contratos " & ddlAgente.SelectedItem.Text)
                    End Select

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

    Private Sub Carrega_Promotora()

        Try

            Dim objDataAgente = New DbAgente
            Dim codGerente As String

            If ContextoWeb.UsuarioLogado.Perfil = 8 Then
                codGerente = ContextoWeb.UsuarioLogado.codGerente
            Else
                codGerente = "99"
            End If

            If codGerente = "99" Then
                objDataAgente.CarregarTodosRegistros(ddlAgente)

            Else
                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim con As New SqlConnection(strConn)
                Dim Vagente As String = ""

                Dim cmd As New SqlCommand("Select O3DESCR, O3CODORG from CDCSANTANAMicroCredito..TORG3 (nolock) WHERE O3codorg IN (" & codGerente & ")", con)
                cmd.Connection.Open()

                Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)


                While dr.Read()
                    Vagente = Trim(dr.GetString(0))
                    Dim AGENTE1 = New ListItem
                    AGENTE1.Value = Trim(dr.GetString(1))
                    AGENTE1.Text = Trim(Vagente)
                End While
                dr.Close()
                con.Close()

            End If


        Catch ex As Exception

        Finally
            GC.Collect()
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

End Class

