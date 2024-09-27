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

Public Class MapaPDVVeicLoja
    Inherits SantanaPage
    Dim strSortField As String = ""

    Private Const hfGridView1SVID As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
    Private Const hfGridView1SHID As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
            txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)


            If ContextoWeb.DadosTransferencia.CodAgente = 0 Then
                Carrega_Agente()
            Else
                Carrega_Agente()
                ddlAgente.SelectedIndex = ddlAgente.Items.IndexOf(ddlAgente.Items.FindByValue(ContextoWeb.DadosTransferencia.CodAgente.ToString()))
            End If

            If Session(hfGridView1SVID) IsNot Nothing Then
                hfGridView1SV.Value = DirectCast(Session(hfGridView1SVID), String)
                Session.Remove(hfGridView1SVID)
            End If

            If Session(hfGridView1SHID) IsNot Nothing Then
                hfGridView1SH.Value = DirectCast(Session(hfGridView1SHID), String)
                Session.Remove(hfGridView1SHID)
            End If


            Quantidade3 = 0
            ValorRisco3 = 0
            Quantidade2 = 0
            ValorRisco2 = 0
            Quantidade1 = 0
            ValorRisco1 = 0
            QTD_LJ_SAI2 = 0
            PERDA = 0

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


    Private Function UltimoDiaUtilMesAnterior(Data As Date) As String

        Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Data.ToString("MM/yyyy"))

        UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(1)
        UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)
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

                Dim cmd As New SqlCommand("Select O3DESCR, O3CODORG from CDCSANTANAMicroCredito..TORG3 (nolock) where O3codorg IN (" & codGerente & ")", con)

                cmd.Connection.Open()

                Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

                While dr.Read()
                    Vagente = Trim(dr.GetString(0))
                    Dim AGENTE1 = New ListItem
                    AGENTE1.Value = Trim(dr.GetString(1))
                    AGENTE1.Text = Trim(Vagente)
                    ddlAgente.Items.Add(AGENTE1)
                End While
                dr.Close()
                con.Close()
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

            ddlCobradora.Items.Insert(0, New ListItem("TODOS", "0"))
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



    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then

                    Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

                    e.Row.Cells(0).Text = drow("AGENTE")
                    e.Row.Cells(1).Text = CNumero.FormataNumero(drow("QTDM3"), 0)
                    e.Row.Cells(2).Text = CNumero.FormataNumero(drow("QTDM2"), 0)
                    e.Row.Cells(3).Text = CNumero.FormataNumero(drow("QTDM1"), 0)
                    e.Row.Cells(4).Text = If(IsDBNull(drow("LJ_3M")), 0, CNumero.FormataNumero(drow("LJ_3M"), 0))
                    e.Row.Cells(5).Text = If(IsDBNull(drow("LJ_ATIVA")), 0, CNumero.FormataNumero(drow("LJ_ATIVA"), 0))
                    e.Row.Cells(6).Text = If(IsDBNull(drow("GRAU")), 0, CNumero.FormataNumero(drow("GRAU"), 2))
                    e.Row.Cells(7).Text = If(IsDBNull(drow("QTD_LJ_SAI")), 0, CNumero.FormataNumero(drow("QTD_LJ_SAI"), 0))
                    e.Row.Cells(8).Text = If(IsDBNull(drow("PERDA")), 0, CNumero.FormataNumero(drow("PERDA"), 0))
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

            If e.Row.RowType = DataControlRowType.Header Then
                Dim grvObjeto As GridView = DirectCast(sender, GridView)
                Dim gvrObjetoLinha As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
                Dim tclCelula As New TableCell()


                tclCelula = New TableCell()
                tclCelula.Text = ""
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)


                tclCelula = New TableCell()
                tclCelula.Text = Mes3
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                tclCelula = New TableCell()
                tclCelula.Text = Mes2
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                tclCelula = New TableCell()
                tclCelula.Text = Mes1
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

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

        GridView1.DataSource = GetData()
        GridView1.DataBind()

    End Sub


    Private Function GetData() As DataTable


        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

        Dim codGerente As String

        If ContextoWeb.UsuarioLogado.Perfil = 8 Then
            codGerente = ContextoWeb.UsuarioLogado.codGerente
        Else
            codGerente = "99"
        End If


        Dim Table As DataTable = Util.ClassBD.GetExibirGrid("[SCR_MAPA_PDV_VEIC_LOJA] '" & Convert.ToDateTime(txtData.Text).ToString("yyyyMMdd") & "','" & ddlAgente.SelectedValue & "'", "IndicePerformance", strConn)

        QTD_LJ_SAI2 = Table.Compute("sum(QTD_LJ_SAI)", "")

        Quantidade3 = Table.Compute("sum(QTDM3)", "")
        ValorRisco3 = Table.Compute("sum(QTDM2)", "")

        Quantidade2 = Table.Compute("sum(QTDM1)", "")
        ValorRisco2 = Table.Compute("sum(LJ_3M)", "")

        Quantidade1 = Table.Compute("sum(LJ_ATIVA)", "")

        Return Table

    End Function

    Protected Sub BindGridView1DataView()

        GridView1.DataSource = DataGridView
        GridView1.DataBind()

    End Sub


    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

        If e.NewPageIndex >= 0 Then
            GridView1.PageIndex = e.NewPageIndex
            BindGridView1DataView()
        End If
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
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
    End Sub


    Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
        Response.Redirect("../Menu.aspx")
    End Sub


    Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
        Try

            Dim oData As New CDataHora(Convert.ToDateTime(txtData.Text))
            Mes1 = oData.NomeMes + " " + oData.Data.Year.ToString()
            oData.Data = oData.Data.AddMonths(-1)
            Mes2 = oData.NomeMes + " " + oData.Data.Year.ToString()
            oData.Data = oData.Data.AddMonths(-1)
            Mes3 = oData.NomeMes + " " + oData.Data.Year.ToString()

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

                Response.Clear()
                Response.Buffer = True
                Dim filename As String = String.Format("PDV_VEIC_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
                Response.AddHeader("content-disposition", "attachment;filename=" + filename)
                Response.Charset = ""
                Response.ContentType = "application/vnd.ms-excel"

                Using sw As New StringWriter()

                    Dim hw As New HtmlTextWriter(sw)

                    GridView1.AllowPaging = False
                    BindGridView1Data()

                    GridView1.HeaderRow.BackColor = Color.White
                    For Each cell As TableCell In GridView1.HeaderRow.Cells
                        cell.CssClass = "GridviewScrollC3Header"
                    Next
                    For Each row As GridViewRow In GridView1.Rows
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

                    GridView1.RenderControl(hw)

                    Dim fi As FileInfo = New FileInfo(Server.MapPath("~/css/GridviewScroll.css"))
                    Dim sb As New System.Text.StringBuilder
                    Dim sr As StreamReader = fi.OpenText()
                    Do While sr.Peek() >= 0
                        sb.Append(sr.ReadLine())
                    Loop
                    sr.Close()

                    Dim style As String = "<html><head><style type='text/css'>" & sb.ToString() & "</style><head>"
                    Response.Write(style)
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
        Session(hfGridView1SVID) = hfGridView1SV.Value
    End Sub

    Protected Sub hfGridView1SH_ValueChanged(sender As Object, e As EventArgs)
        Session(hfGridView1SHID) = hfGridView1SH.Value
    End Sub



    Public Property Quantidade3() As Integer
        Get
            If ViewState("D824C796-5419-4D61-AF1F-D3856B12A4C8") Is Nothing Then
                ViewState("D824C796-5419-4D61-AF1F-D3856B12A4C8") = 0
            End If
            Return DirectCast(ViewState("D824C796-5419-4D61-AF1F-D3856B12A4C8"), Integer)
        End Get
        Set(value As Integer)
            ViewState("D824C796-5419-4D61-AF1F-D3856B12A4C8") = value
        End Set
    End Property

    Public Property ValorRisco3() As Integer
        Get
            If ViewState("76A16527-CE4F-4740-AB70-8C97A779E94C") Is Nothing Then
                ViewState("76A16527-CE4F-4740-AB70-8C97A779E94C") = 0
            End If
            Return DirectCast(ViewState("76A16527-CE4F-4740-AB70-8C97A779E94C"), Integer)
        End Get
        Set(value As Integer)
            ViewState("76A16527-CE4F-4740-AB70-8C97A779E94C") = value
        End Set
    End Property

    Public Property Quantidade2() As Integer
        Get
            If ViewState("AF4C368B-A56F-41A9-BC74-8DC4D1AE2E7E") Is Nothing Then
                ViewState("AF4C368B-A56F-41A9-BC74-8DC4D1AE2E7E") = 0
            End If
            Return DirectCast(ViewState("AF4C368B-A56F-41A9-BC74-8DC4D1AE2E7E"), Integer)
        End Get
        Set(value As Integer)
            ViewState("AF4C368B-A56F-41A9-BC74-8DC4D1AE2E7E") = value
        End Set
    End Property

    Public Property ValorRisco2() As Integer
        Get
            If ViewState("24726240-9606-4A91-82DB-A2632B8109CE") Is Nothing Then
                ViewState("24726240-9606-4A91-82DB-A2632B8109CE") = 0
            End If
            Return DirectCast(ViewState("24726240-9606-4A91-82DB-A2632B8109CE"), Integer)
        End Get
        Set(value As Integer)
            ViewState("24726240-9606-4A91-82DB-A2632B8109CE") = value
        End Set
    End Property



    Public Property Quantidade1() As Integer
        Get
            If ViewState("E3BAFEC9-4556-4035-B7D8-1056F48AFEAB") Is Nothing Then
                ViewState("E3BAFEC9-4556-4035-B7D8-1056F48AFEAB") = 0
            End If
            Return DirectCast(ViewState("E3BAFEC9-4556-4035-B7D8-1056F48AFEAB"), Integer)
        End Get
        Set(value As Integer)
            ViewState("E3BAFEC9-4556-4035-B7D8-1056F48AFEAB") = value
        End Set
    End Property

    Public Property QTD_LJ_SAI2() As Integer
        Get
            If ViewState("EEE95F84-9142-45BB-B1A9-53897697A244") Is Nothing Then
                ViewState("EEE95F84-9142-45BB-B1A9-53897697A244") = 0
            End If
            Return DirectCast(ViewState("EEE95F84-9142-45BB-B1A9-53897697A244"), Integer)
        End Get
        Set(value As Integer)
            ViewState("EEE95F84-9142-45BB-B1A9-53897697A244") = value
        End Set
    End Property

    Public Property ValorRisco1() As Integer
        Get
            If ViewState("EEE95F84-9142-45BB-B1A9-53897697A244") Is Nothing Then
                ViewState("EEE95F84-9142-45BB-B1A9-53897697A244") = 0
            End If
            Return DirectCast(ViewState("EEE95F84-9142-45BB-B1A9-53897697A244"), Integer)
        End Get
        Set(value As Integer)
            ViewState("EEE95F84-9142-45BB-B1A9-53897697A244") = value
        End Set
    End Property

    Public Property PERDA() As Integer
        Get
            If ViewState("EEE95F84-9142-45BB-B1A9-53897697A244") Is Nothing Then
                ViewState("EEE95F84-9142-45BB-B1A9-53897697A244") = 0
            End If
            Return DirectCast(ViewState("EEE95F84-9142-45BB-B1A9-53897697A244"), Integer)
        End Get
        Set(value As Integer)
            ViewState("EEE95F84-9142-45BB-B1A9-53897697A244") = value
        End Set
    End Property



    Public Property Mes1() As String
        Get
            If ViewState("45360BB1-18CC-4FE0-8647-D2E849807D47") Is Nothing Then
                ViewState("45360BB1-18CC-4FE0-8647-D2E849807D47") = 0
            End If
            Return DirectCast(ViewState("45360BB1-18CC-4FE0-8647-D2E849807D47"), String)
        End Get
        Set(value As String)
            ViewState("45360BB1-18CC-4FE0-8647-D2E849807D47") = value
        End Set
    End Property


    Public Property Mes2() As String
        Get
            If ViewState("32456B75-2CF5-4519-83BB-8C1D8FF3AEFF") Is Nothing Then
                ViewState("32456B75-2CF5-4519-83BB-8C1D8FF3AEFF") = 0
            End If
            Return DirectCast(ViewState("32456B75-2CF5-4519-83BB-8C1D8FF3AEFF"), String)
        End Get
        Set(value As String)
            ViewState("32456B75-2CF5-4519-83BB-8C1D8FF3AEFF") = value
        End Set
    End Property


    Public Property Mes3() As String
        Get
            If ViewState("8D9E1035-A50D-44C1-A69A-74D492CAB74B") Is Nothing Then
                ViewState("8D9E1035-A50D-44C1-A69A-74D492CAB74B") = 0
            End If
            Return DirectCast(ViewState("8D9E1035-A50D-44C1-A69A-74D492CAB74B"), String)
        End Get
        Set(value As String)
            ViewState("8D9E1035-A50D-44C1-A69A-74D492CAB74B") = value
        End Set
    End Property


    Protected Sub GridView1_DataBound(sender As Object, e As EventArgs)
        Dim gridView As GridView = CType(sender, GridView)
        If CType(gridView.DataSource, System.Data.DataTable).Rows.Count > 0 Then
            Util.CWebPageUtil.PreencheListaDePaginas(gridView, CType(gridView.BottomPagerRow.Cells(0).FindControl("PagerPages"), DropDownList))
        End If
    End Sub


    Protected Sub PagerPages_SelectedIndexChanged(sender As Object, e As EventArgs)
        GridView1.DataSource = DataGridView()
        GridView1.PageIndex = CType(sender, DropDownList).SelectedIndex
        GridView1.DataBind()
    End Sub

    Protected Sub GridViewCarteiras_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        Dim arrSortExpr() As String
        Dim i As Integer
        If e.SortExpression = "" Then Return

        SortField = e.SortExpression
        BindGridView1Data()
        arrSortExpr = Split(e.SortExpression, " ")

        For i = 0 To GridView1.Columns().Count - 1
            If (GridView1.Columns(i).SortExpression = e.SortExpression) Then
                If arrSortExpr.Length = 1 Then
                    ReDim Preserve arrSortExpr(2)
                    arrSortExpr.SetValue("ASC", 1)
                End If
                If UCase(arrSortExpr(1)) = "ASC" Then
                    If UCase(arrSortExpr(1)) = "ASC" Then
                        arrSortExpr(1) = "DESC"
                    ElseIf UCase(arrSortExpr(1)) = "DESC" Then
                        arrSortExpr(1) = "ASC"
                    End If
                    GridView1.Columns(i).SortExpression = arrSortExpr(0) & " " & arrSortExpr(1)
                End If
                Exit For
            End If

        Next

    End Sub

    Public Property SortField As String
        Get

            If ViewState("34AF154F-7129-4CA1-90B4-C1175088D384") Is Nothing Then
                ViewState("34AF154F-7129-4CA1-90B4-C1175088D384") = strSortField
            End If

            Return DirectCast(ViewState("34AF154F-7129-4CA1-90B4-C1175088D384"), String)
        End Get
        Set(value As String)
            ViewState("34AF154F-7129-4CA1-90B4-C1175088D384") = value
        End Set
    End Property

End Class

