Imports System.Data
Imports Microsoft.VisualBasic
Imports Util
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.IO
Imports System.Web
Imports System
Imports System.Configuration
Imports System.Drawing

Imports Santana.Seguranca

Public Class SpreadAnalitico

    Inherits SantanaPage

    Public safra_prod As Integer = 1

    Dim strSortField As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
            txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)

            ' txtDataDE.Text = "01/01/2014"
            ' dia 1 de 2 meses anteriores, fecha o trimestre
            txtDataDE.Text = (Convert.ToDateTime(txtData.Text).AddMonths(-2)).ToString("dd/MM/yyyy")
            txtDataDE.Text = "01/" + Right(txtDataDE.Text, 7)

            ' INI igual ao DE, 3 anos antes. 36 meses de SAFRA
            txtDataINI.Text = (Convert.ToDateTime(txtDataDE.Text).AddYears(-3)).ToString("dd/MM/yyyy")
            txtDataFIM.Text = (Convert.ToDateTime(txtDataDE.Text).AddDays(-1)).ToString("dd/MM/yyyy")
            txtDiasAtraso.Text = "60"
            safra_prod = 1

            If ContextoWeb.DadosTransferencia.CodAgente = 0 Then
                Carrega_Agente()
            Else
                Carrega_Agente()
                ddlAgente.SelectedIndex = ddlAgente.Items.IndexOf(ddlAgente.Items.FindByValue(ContextoWeb.DadosTransferencia.CodAgente.ToString()))
            End If


            If ContextoWeb.DadosTransferencia.CodCobradora = 0 Then
                Carrega_Cobradora()
            Else
                Carrega_Cobradora()
                ddlCobradora.SelectedIndex = ddlCobradora.Items.IndexOf(ddlCobradora.Items.FindByValue(ContextoWeb.DadosTransferencia.CodCobradora.ToString()))
            End If

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

        ' após 1/8/2014 Ago usar ULTIMO DIA DO MES
        ' antes ultimo dia util

        If UltimoDiaMesAnterior.Year = Now.Date.Year Then
            If UltimoDiaMesAnterior.Month <= Now.Date.Month Then
                txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
            End If
        ElseIf UltimoDiaMesAnterior.Year < Now.Date.Year Then
            txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
        End If

    End Sub


    Protected Sub btnProximaData_Click(sender As Object, e As EventArgs)

        Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtData.Text)
        UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(1)

        ' após 1/8/2014 Ago usar ULTIMO DIA DO MES
        ' antes ultimo dia util

        If UltimoDiaMesAnterior.Year = Now.Date.Year Then
            If UltimoDiaMesAnterior.Month <= Now.Date.Month Then
                txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
            End If
        ElseIf UltimoDiaMesAnterior.Year < Now.Date.Year Then
            txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
        End If

    End Sub


    Private Function UltimoDiaUtilMesAnterior(Data As Date) As String

        Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Data.ToString("MM/yyyy"))

        UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(1)
        UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)

        ' após 1/8/2014 Ago usar ULTIMO DIA DO MES
        If UltimoDiaMesAnterior <= Convert.ToDateTime("01/aug/2014") Then

            ' ultimo dia util
            If (UltimoDiaMesAnterior.DayOfWeek = DayOfWeek.Sunday) Then
                UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-2)
            ElseIf (UltimoDiaMesAnterior.DayOfWeek = DayOfWeek.Saturday) Then
                UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)
            End If
        End If
        Return UltimoDiaMesAnterior.ToString("dd/MM/yyyy")

    End Function






    Private Sub Carrega_Agente()
        ' CARTEIRA
        Try

            ddlAgente.Items.Insert(0, New ListItem("Safra", "1"))
            ddlAgente.Items.Insert(1, New ListItem("Produção", "2"))
            ddlAgente.Items.Insert(2, New ListItem("Carteira (Ref Produção ATÉ)", "3"))

            ddlAgente.SelectedIndex = 0


        Catch ex As Exception

        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Carrega_Cobradora()
        ' VEICULO
        Try

            ddlCobradora.Items.Insert(0, New ListItem("Todos", "99"))
            ddlCobradora.Items.Insert(1, New ListItem("Leves", "1"))
            ddlCobradora.Items.Insert(2, New ListItem("Motos", "2"))
            ddlCobradora.Items.Insert(3, New ListItem("Utilitários", "3"))
            ddlCobradora.Items.Insert(4, New ListItem("Refinanciamento", "4"))
            ddlCobradora.Items.Insert(5, New ListItem("Renegociação", "5"))
            ddlCobradora.Items.Insert(6, New ListItem("Leves c/ Renegociação", "6"))
            ddlCobradora.Items.Insert(7, New ListItem("Motos c/ Renegociação", "7"))
            ddlCobradora.Items.Insert(8, New ListItem("Utilitários c/ Renegociação", "8"))

            ddlCobradora.SelectedIndex = 0


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

                    If IsDBNull(drow("RETORNO")) OrElse drow("RETORNO") = 0 Then
                        e.Row.Cells(10).Text = ""
                    Else
                        e.Row.Cells(10).Text = CNumero.FormataNumero(drow("RETORNO"), 4)
                    End If

                    If IsDBNull(drow("TAXA_SEM_RET")) OrElse drow("TAXA_SEM_RET") = 0 Then
                        e.Row.Cells(11).Text = ""
                    Else
                        e.Row.Cells(11).Text = CNumero.FormataNumero(drow("TAXA_SEM_RET"), 4)
                    End If

                    If IsDBNull(drow("ATRASO")) OrElse drow("ATRASO") = 0 Then
                        e.Row.Cells(12).Text = ""
                    Else
                        e.Row.Cells(12).Text = drow("ATRASO")
                    End If

                    If IsDBNull(drow("RENEGOCIADO")) OrElse drow("RENEGOCIADO") = 0 Then
                        e.Row.Cells(13).Text = ""
                    Else
                        e.Row.Cells(13).Text = drow("RENEGOCIADO")
                    End If

                    If IsDBNull(drow("RATING")) Then
                        e.Row.Cells(14).Text = ""
                    Else
                        e.Row.Cells(14).Text = drow("RATING")
                    End If

                    If IsDBNull(drow("CODCLI")) Then
                        e.Row.Cells(15).Text = ""
                    Else
                        e.Row.Cells(15).Text = drow("CODCLI")
                    End If

                    If IsDBNull(drow("CODPROD")) OrElse drow("CODPROD") = 0 Then
                        e.Row.Cells(16).Text = ""
                    Else
                        e.Row.Cells(16).Text = drow("CODPROD")
                    End If

                    If IsDBNull(drow("CODMODA")) OrElse drow("CODMODA") = 0 Then
                        e.Row.Cells(17).Text = ""
                    Else
                        e.Row.Cells(17).Text = drow("CODMODA")
                    End If

                    If IsDBNull(drow("AGENTE")) Then
                        e.Row.Cells(18).Text = ""
                    Else
                        e.Row.Cells(18).Text = drow("AGENTE")
                    End If

                    If IsDBNull(drow("VEICULO")) Then
                        e.Row.Cells(19).Text = ""
                    Else
                        e.Row.Cells(19).Text = drow("VEICULO")
                    End If

                    If IsDBNull(drow("ANO_MODELO")) Then
                        e.Row.Cells(20).Text = ""
                    Else
                        e.Row.Cells(20).Text = drow("ANO_MODELO")
                    End If

                    If IsDBNull(drow("RENDA")) OrElse drow("RENDA") = 0 Then
                        e.Row.Cells(21).Text = ""
                    Else
                        e.Row.Cells(21).Text = CNumero.FormataNumero(drow("RENDA"), 4)
                    End If

                    If IsDBNull(drow("FX_RENDA")) Then
                        e.Row.Cells(22).Text = ""
                    Else
                        e.Row.Cells(22).Text = drow("FX_RENDA")
                    End If

                    If IsDBNull(drow("FX_PRAZO")) Then
                        e.Row.Cells(23).Text = ""
                    Else
                        e.Row.Cells(23).Text = drow("FX_PRAZO")
                    End If

                    If IsDBNull(drow("CODOPER")) Then
                        e.Row.Cells(24).Text = ""
                    Else
                        e.Row.Cells(24).Text = drow("CODOPER")
                    End If

                    If IsDBNull(drow("CODLOJA")) Then
                        e.Row.Cells(25).Text = ""
                    Else
                        e.Row.Cells(25).Text = drow("CODLOJA")
                    End If

                    If IsDBNull(drow("TX_X_VLR")) OrElse drow("TX_X_VLR") = 0 Then
                        e.Row.Cells(26).Text = ""
                    Else
                        e.Row.Cells(26).Text = CNumero.FormataNumero(drow("TX_X_VLR"), 4)
                    End If

                    If IsDBNull(drow("PRZ_X_VLR")) OrElse drow("PRZ_X_VLR") = 0 Then
                        e.Row.Cells(27).Text = ""
                    Else
                        e.Row.Cells(27).Text = CNumero.FormataNumero(drow("PRZ_X_VLR"), 4)
                    End If

                    If IsDBNull(drow("idade")) OrElse drow("idade") = 0 Then
                        e.Row.Cells(28).Text = ""
                    Else
                        e.Row.Cells(28).Text = drow("idade")
                    End If

                    If IsDBNull(drow("fx_idade")) Then
                        e.Row.Cells(29).Text = ""
                    Else
                        e.Row.Cells(29).Text = drow("fx_idade")
                    End If

                    If IsDBNull(drow("fx_ANO")) Then
                        e.Row.Cells(30).Text = ""
                    Else
                        e.Row.Cells(30).Text = drow("fx_ANO")
                    End If

                    If IsDBNull(drow("OCUPACAO")) Then
                        e.Row.Cells(31).Text = ""
                    Else
                        e.Row.Cells(31).Text = drow("OCUPACAO")
                    End If

                    If IsDBNull(drow("CONTRATO_ORIGINAL")) Then
                        e.Row.Cells(32).Text = ""
                    Else
                        e.Row.Cells(32).Text = drow("CONTRATO_ORIGINAL")
                    End If

                    If IsDBNull(drow("VEICULO_ORIGINAL")) Then
                        e.Row.Cells(33).Text = ""
                    Else
                        e.Row.Cells(33).Text = drow("VEICULO_ORIGINAL")
                    End If

                    If IsDBNull(drow("vf")) OrElse drow("vf") = 0 Then
                        e.Row.Cells(34).Text = ""
                    Else
                        e.Row.Cells(34).Text = CNumero.FormataNumero(drow("vf"), 4)
                    End If

                    If IsDBNull(drow("vlr_risco")) Or drow("vlr_risco") = 0 Then
                        e.Row.Cells(35).Text = ""
                    Else
                        e.Row.Cells(35).Text = CNumero.FormataNumero(drow("vlr_risco"), 4)
                    End If

                    ' If IsDBNull(drow("VF_ORIGINAL")) Or drow("VF_ORIGINAL") = 0 Then
                    'e.Row.Cells(36).Text = ""
                    'Else
                    e.Row.Cells(36).Text = CNumero.FormataNumero(drow("VF_ORIGINAL"), 4)
                    'End If

                    e.Row.Cells(37).Text = drow("SCORE")
                    e.Row.Cells(38).Text = CNumero.FormataNumero(drow("VLR_VEICULO"), 2)


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
        ' se botao de safra =1 ou producao =2 ou carteira =3
        ' 
        Select Case ddlAgente.SelectedValue
            Case 1
                GridViewRiscoAnalitico.DataSource = GetData()
            Case 2
                GridViewRiscoAnalitico.DataSource = GetDataProd()
            Case 3
                GridViewRiscoAnalitico.DataSource = GetDataCart()
        End Select

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

        table = Util.ClassBD.GetExibirGrid("[scr_SPREAD_ANALITICO] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', '" & Right(txtDataDE.Text, 4) & Mid(txtDataDE.Text, 4, 2) & Left(txtDataDE.Text, 2) & "', '" & ddlAgente.SelectedValue & "', '" & ddlCobradora.SelectedValue & "', '" & Right(txtDataINI.Text, 4) & Mid(txtDataINI.Text, 4, 2) & Left(txtDataINI.Text, 2) & "', '" & Right(txtDataFIM.Text, 4) & Mid(txtDataFIM.Text, 4, 2) & Left(txtDataFIM.Text, 2) & "', '" & txtDiasAtraso.Text & "'", "SPREADANALITICO", strConn)

        Return table

    End Function

    Private Function GetDataProd() As DataTable

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim table As DataTable

        table = Util.ClassBD.GetExibirGrid("[scr_SPREAD_ANALITICO_Prod] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', '" & Right(txtDataDE.Text, 4) & Mid(txtDataDE.Text, 4, 2) & Left(txtDataDE.Text, 2) & "'", "SPREADANALITICO", strConn)

        Return table

    End Function

    Private Function GetDataCart() As DataTable

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim table As DataTable

        table = Util.ClassBD.GetExibirGrid("[scr_SPREAD_ANALITICO_CART] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', '" & Right(txtDataDE.Text, 4) & Mid(txtDataDE.Text, 4, 2) & Left(txtDataDE.Text, 2) & "', '" & ddlAgente.SelectedValue & "', '" & ddlCobradora.SelectedValue & "', '" & Right(txtDataINI.Text, 4) & Mid(txtDataINI.Text, 4, 2) & Left(txtDataINI.Text, 2) & "', '" & Right(txtDataFIM.Text, 4) & Mid(txtDataFIM.Text, 4, 2) & Left(txtDataFIM.Text, 2) & "', '" & txtDiasAtraso.Text & "'", "SPREADANALITICO", strConn)

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

    ' carregar Safra analitica
    Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
        Try

            BindGridView1Data()

        Catch ex As Exception

        Finally
            GC.Collect()
        End Try

    End Sub
    ' carregar Producao analitica
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
                Dim filename As String = String.Format("SPREAD_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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

                    '         ddlRelatorio.SelectedValue   
                    '  Table = Util.ClassBD.GetExibirGrid("[scr_SPREAD_Operador] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', '" & Right(txtDataDE.Text, 4) & Mid(txtDataDE.Text, 4, 2) & Left(txtDataDE.Text, 2) & "', '" & ddlRelatorio.SelectedValue & "','" & ddlAgente.SelectedValue & "','" & ddlCobradora.SelectedValue & "','" & SortField & "','" & Right(txtDataINI.Text, 4) & Mid(txtDataINI.Text, 4, 2) & Left(txtDataINI.Text, 2) & "','" & Right(txtDataFIM.Text, 4) & Mid(txtDataFIM.Text, 4, 2) & Left(txtDataFIM.Text, 2) & "','" & txtDiasAtraso.Text & "'", "RRMENSALcubo", strConn)
                    '  txtDataINI.Text = "01/01/2011",  txtDataFIM.Text = "31/12/2013",       txtDiasAtraso.Text = "60"

                    '  cabecalho com os parametros 
                    Select ddlAgente.SelectedValue
                        Case 1
                            Response.Write("Produção De " & txtDataDE.Text & " Até " & txtData.Text & " - Relatório Spread - Risco Analítico - Safra De " & txtDataINI.Text & " Até " & txtDataFIM.Text & " - Atraso " & txtDiasAtraso.Text & " -  Contratos " & ddlAgente.SelectedItem.Text & "  - Veiculo " & ddlCobradora.SelectedItem.Text)
                        Case 2
                            Response.Write("Produção De " & txtDataDE.Text & " Até " & txtData.Text & " - Relatório Spread - Produção Analítica - De " & txtDataINI.Text & " Até " & txtDataFIM.Text)
                        Case 3
                            Response.Write("Produção De " & txtDataDE.Text & " Até " & txtData.Text & " - Relatório Spread - CARTEIRA - REF " & txtData.Text)
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



    'Protected Sub GridViewRiscoAnalitico_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridViewRiscoAnalitico.Sorting
    '    Dim arrSortExpr() As String
    '    Dim i As Integer
    '    If e.SortExpression = "" Then Return

    '    SortField = e.SortExpression
    '    BindGridView1Data()
    '    arrSortExpr = Split(e.SortExpression, " ")

    '    For i = 0 To GridViewRiscoAnalitico.Columns().Count - 1
    '        If (GridViewRiscoAnalitico.Columns(i).SortExpression = e.SortExpression) Then
    '            If arrSortExpr.Length = 1 Then
    '                ReDim Preserve arrSortExpr(2)
    '                arrSortExpr.SetValue("ASC", 1)
    '            End If
    '            If UCase(arrSortExpr(1)) = "ASC" Then
    '                If UCase(arrSortExpr(1)) = "ASC" Then
    '                    arrSortExpr(1) = "DESC"
    '                ElseIf UCase(arrSortExpr(1)) = "DESC" Then
    '                    arrSortExpr(1) = "ASC"
    '                End If
    '                GridViewRiscoAnalitico.Columns(i).SortExpression = arrSortExpr(0) & " " & arrSortExpr(1)
    '            End If
    '            Exit For
    '        End If

    '    Next

    'End Sub



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

