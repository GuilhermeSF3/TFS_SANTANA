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

Public Class RollrateProjetadoAnalitico

    Inherits SantanaPage

    Public safra_prod As Integer = 1

    Dim strSortField As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            txtDataRef.Text = Now.Date.AddDays(-1).ToString("dd/MM/yyyy")

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
            txtDataFech.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
            txtDataRef.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)

            UltimoDiaMesAnterior = Now.Date.ToString("dd/MM/yyyy")
            txtDataProj.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)

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

        Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)


    End Sub


    Protected Sub btnDataAnterior_Click(sender As Object, e As EventArgs)

        Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataFech.Text)
        UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(-1)

        If UltimoDiaMesAnterior.Year = Now.Date.Year Then
            If UltimoDiaMesAnterior.Month <= Now.Date.Month Then
                txtDataFech.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
            End If
        ElseIf UltimoDiaMesAnterior.Year < Now.Date.Year Then
            txtDataFech.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
        End If

    End Sub


    Protected Sub btnProximaData_Click(sender As Object, e As EventArgs)

        Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataFech.Text)
        UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(1)

        If UltimoDiaMesAnterior.Year = Now.Date.Year Then
            If UltimoDiaMesAnterior.Month <= Now.Date.Month Then
                txtDataFech.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
            End If
        ElseIf UltimoDiaMesAnterior.Year < Now.Date.Year Then
            txtDataFech.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
        End If

    End Sub

    Protected Sub btnDataRefAnt_Click(sender As Object, e As EventArgs)


        Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataRef.Text)
        UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)

        txtDataRef.Text = UltimoDiaMesAnterior

    End Sub


    Protected Sub btnProximaDataRef_Click(sender As Object, e As EventArgs)

        Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataRef.Text)
        UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(1)

        txtDataRef.Text = UltimoDiaMesAnterior

    End Sub

    Protected Sub btnDataProjAnt_Click(sender As Object, e As EventArgs)


        Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataProj.Text)
        UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)

        txtDataProj.Text = UltimoDiaMesAnterior

    End Sub


    Protected Sub btnProximaDataProj_Click(sender As Object, e As EventArgs)

        Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataProj.Text)
        UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(1)

        txtDataProj.Text = UltimoDiaMesAnterior

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

            Dim objDataAgente = New DbAgente
            Dim codGerente As String

            If ContextoWeb.UsuarioLogado.Perfil = 8 Then
                codGerente = ContextoWeb.UsuarioLogado.codGerente
            Else
                codGerente = "99"
            End If

            If codGerente = "99" Then
                objDataAgente.CarregarTodosRegistros(ddlAgente)

                ddlAgente.Items.Insert(0, New ListItem("Todos", "99"))
                ddlAgente.SelectedIndex = 0
            Else
                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim con As New SqlConnection(strConn)
                Dim Vagente As String = ""

                Dim cmd As New SqlCommand("Select top 1 O3DESCR from CDCSANTANAMicroCredito..TORG3 (nolock) where O3codorg='" & codGerente.PadLeft(6, "0") & "'", con)

                cmd.Connection.Open()

                Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

                While dr.Read()
                    Vagente = Trim(dr.GetString(0))
                End While
                dr.Close()
                con.Close()

                Dim AGENTE1 = New ListItem
                AGENTE1.Value = codGerente
                AGENTE1.Text = Trim(Vagente)
                ddlAgente.Items.Add(AGENTE1)
            End If

        Catch ex As Exception

        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Carrega_Cobradora()

        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim connection As New SqlConnection(strConn)
            Dim ds As New DataSet
            Dim dt As DataTable = Nothing

            Dim Produto As Integer = ContextoWeb.UsuarioLogado.Produto

            Dim command As SqlCommand = New SqlCommand(
            "select COCod, CODescr from TCobradora (nolock) where COAtivo=1 and COCodProduto = '" & Left(Produto, 1) & "' order by CODescr", connection)

            command.Connection.Open()

            Dim ddlValues As SqlDataReader
            ddlValues = command.ExecuteReader()

            ddlCobradora.DataSource = ddlValues
            ddlCobradora.DataValueField = "COCOD"
            ddlCobradora.DataTextField = "CODESCR"
            ddlCobradora.DataBind()

            ddlCobradora.Items.Insert(0, New ListItem("TODOS", "99"))
            ddlCobradora.SelectedIndex = 0

            ddlValues.Close()

            command.Connection.Close()
            command.Connection.Dispose()

            connection.Close()

        Catch ex As Exception

        Finally
            GC.Collect()
        End Try
    End Sub

    Protected Sub btnProcessar_Click(sender As Object, e As EventArgs)

        Session.Timeout = 5  ' 5 MIN DE PROCESSAMENTO

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim connection As New SqlConnection(strConn)
        Dim command As SqlCommand = New SqlCommand(
        "scr_PROCESSA_RRproj1 '" & Right(txtDataRef.Text, 4) & Mid(txtDataRef.Text, 4, 2) & Left(txtDataRef.Text, 2) & "', '" & Right(txtDataFech.Text, 4) & Mid(txtDataFech.Text, 4, 2) & Left(txtDataFech.Text, 2) & "', '" & Right(txtDataProj.Text, 4) & Mid(txtDataProj.Text, 4, 2) & Left(txtDataProj.Text, 2) & "'", connection)

        'teste de timeout
        command.CommandTimeout = Convert.ToInt32(2000000)
        command.Connection.Open()
        Dim ddlValues As SqlDataReader
        ddlValues = command.ExecuteReader()
        ddlValues.Close()
        command.Connection.Close()
        command.Connection.Dispose()
        connection.Close()

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

                    If IsDBNull(drow("CONTRATO")) Then
                        e.Row.Cells(1).Text = ""
                    Else
                        e.Row.Cells(1).Text = drow("CONTRATO")
                    End If

                    If IsDBNull(drow("AGENTE")) Then
                        e.Row.Cells(2).Text = ""
                    Else
                        e.Row.Cells(2).Text = drow("AGENTE")
                    End If

                    If IsDBNull(drow("COBRADORA")) Then
                        e.Row.Cells(3).Text = ""
                    Else
                        e.Row.Cells(3).Text = drow("COBRADORA")
                    End If

                    If IsDBNull(drow("NVL_RISCO")) Then
                        e.Row.Cells(4).Text = ""
                    Else
                        e.Row.Cells(4).Text = drow("NVL_RISCO")
                    End If

                    If IsDBNull(drow("NVL_RISCO_ANT")) Then
                        e.Row.Cells(5).Text = ""
                    Else
                        e.Row.Cells(5).Text = drow("NVL_RISCO_ANT")
                    End If

                    If IsDBNull(drow("SALDO_INSCRITO")) Then
                        e.Row.Cells(6).Text = ""
                    Else
                        e.Row.Cells(6).Text = drow("SALDO_INSCRITO")
                    End If

                    If IsDBNull(drow("SALDO_INSCRITO_ANT")) Then
                        e.Row.Cells(7).Text = ""
                    Else
                        e.Row.Cells(7).Text = drow("SALDO_INSCRITO_ANT")
                    End If

                    If IsDBNull(drow("PDD_ANT")) Then
                        e.Row.Cells(8).Text = ""
                    Else
                        e.Row.Cells(8).Text = drow("PDD_ANT")
                    End If

                    If IsDBNull(drow("ATRASO_PROJ")) Then
                        e.Row.Cells(9).Text = ""
                    Else
                        e.Row.Cells(9).Text = drow("ATRASO_PROJ")
                        GridViewRiscoAnalitico.Columns(9).HeaderStyle.BackColor = Color.Yellow
                    End If

                    If IsDBNull(drow("PDD_prc")) Then
                        e.Row.Cells(10).Text = ""
                    Else
                        e.Row.Cells(10).Text = drow("PDD_prc")
                        GridViewRiscoAnalitico.Columns(10).HeaderStyle.BackColor = Color.Yellow
                    End If

                    If IsDBNull(drow("PDD_vlr")) Then
                        e.Row.Cells(11).Text = ""
                    Else
                        e.Row.Cells(11).Text = drow("PDD_vlr")
                        GridViewRiscoAnalitico.Columns(11).HeaderStyle.BackColor = Color.Yellow
                    End If

                    If IsDBNull(drow("IMPACTO")) Then
                        e.Row.Cells(12).Text = ""
                    Else
                        e.Row.Cells(12).Text = drow("IMPACTO")
                        GridViewRiscoAnalitico.Columns(12).HeaderStyle.BackColor = Color.Yellow
                    End If

                    If IsDBNull(drow("Atraso")) Then
                        e.Row.Cells(13).Text = ""
                    Else
                        e.Row.Cells(13).Text = drow("Atraso")
                    End If

                    If IsDBNull(drow("PLANO")) Then
                        e.Row.Cells(14).Text = ""
                    Else
                        e.Row.Cells(14).Text = drow("PLANO")
                    End If

                    If IsDBNull(drow("PARCELA_AVENCER")) Then
                        e.Row.Cells(15).Text = ""
                    Else
                        e.Row.Cells(15).Text = drow("PARCELA_AVENCER")
                    End If

                    If IsDBNull(drow("PARCELA_ABERTO")) Then
                        e.Row.Cells(16).Text = ""
                    Else
                        e.Row.Cells(16).Text = drow("PARCELA_ABERTO")
                    End If

                    If IsDBNull(drow("PARCELA_PAGA")) Then
                        e.Row.Cells(17).Text = ""
                    Else
                        e.Row.Cells(17).Text = drow("PARCELA_PAGA")
                    End If

                    If IsDBNull(drow("TIPO")) Then
                        e.Row.Cells(18).Text = ""
                    Else
                        e.Row.Cells(18).Text = drow("TIPO")
                    End If

                    If IsDBNull(drow("TIPO_PROJ")) Then
                        e.Row.Cells(19).Text = ""
                    Else
                        e.Row.Cells(19).Text = drow("TIPO_PROJ")
                    End If

                    If IsDBNull(drow("COD_CLI")) Then
                        e.Row.Cells(20).Text = ""
                    Else
                        e.Row.Cells(20).Text = drow("COD_CLI")
                    End If

                    If IsDBNull(drow("VEICULO_ORIGINAL")) Then
                        e.Row.Cells(21).Text = ""
                    Else
                        e.Row.Cells(21).Text = drow("VEICULO_ORIGINAL")
                    End If

                    If IsDBNull(drow("COD_MODA")) Then
                        e.Row.Cells(22).Text = ""
                    Else
                        e.Row.Cells(22).Text = drow("COD_MODA")
                    End If

                    If IsDBNull(drow("TAXA_JUROS")) Then
                        e.Row.Cells(23).Text = ""
                    Else
                        e.Row.Cells(23).Text = CNumero.FormataNumero(drow("TAXA_JUROS"), 4)
                    End If

                    If IsDBNull(drow("CPF")) Then
                        e.Row.Cells(24).Text = ""
                    Else
                        e.Row.Cells(24).Text = drow("CPF")
                    End If

                    If IsDBNull(drow("DATA_BASE")) Then
                        e.Row.Cells(25).Text = ""
                    Else
                        e.Row.Cells(25).Text = drow("DATA_BASE")
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

        GridViewRiscoAnalitico.DataSource = GetData()
        GridViewRiscoAnalitico.DataBind()
        GridViewRiscoAnalitico.AllowPaging = "True"
    End Sub

    Protected Sub BindGridView1DataView()

        GridViewRiscoAnalitico.DataSource = DataGridView
        GridViewRiscoAnalitico.DataBind()
    End Sub


    Private Function GetData() As DataTable
        Dim CODAGENTE As Integer = ContextoWeb.UsuarioLogado.codGerente

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim table As DataTable

        table = Util.ClassBD.GetExibirGrid("[scr_RR_projetado_ANALITICO] '" & Right(txtDataRef.Text, 4) & Mid(txtDataRef.Text, 4, 2) & Left(txtDataRef.Text, 2) & "', '" &
                                           Right(txtDataFech.Text, 4) & Mid(txtDataFech.Text, 4, 2) & Left(txtDataFech.Text, 2) & "', '" &
                                           Right(txtDataProj.Text, 4) & Mid(txtDataProj.Text, 4, 2) & Left(txtDataProj.Text, 2) & "', '" &
                                           ddlAgente.SelectedValue & "', '" &
                                           ddlCobradora.SelectedValue & "'", "RollrateProjAnalitico", strConn)

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
                Dim filename As String = String.Format("RollRateProjAnalitico_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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

                    Response.Write("RollRate Projetado Analítico. Data Ref: " & txtDataRef.Text & "  - Data Projeção : " & txtDataProj.Text & " - Data Fechamento : " & txtDataFech.Text)

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

End Class

