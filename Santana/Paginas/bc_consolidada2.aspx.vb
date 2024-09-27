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

Public Class BC_consolidada
    Inherits SantanaPage

    Private Const hfGridView1SVID As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
    Private Const hfGridView1SHID As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

    Private sortImage As New WebControls.Image

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            '  ContextoWeb.Navegacao.TituloPaginaAtual = Me.Title
            btnVoltar.Text = "Voltar"


            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
            txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)

            '            lblDataReferencia.Text = ContextoWeb.DadosTransferencia.DataReferencia
            'lblAgente.Text = ContextoWeb.DadosTransferencia.Agente
            'lblCobradora.Text = ContextoWeb.DadosTransferencia.Cobradora

            Select Case ContextoWeb.DadosTransferencia.Classe
                'ajustei os prazos das faixas
                Case "AA"
                    lblClasse.Text = "AA - 0 a 5 d"
                Case "A"
                    lblClasse.Text = "A - 6 a 14 d"
                Case "B"
                    lblClasse.Text = "B - 15 a 30 d"
                Case "C"
                    lblClasse.Text = "C - 31 a 60 d"
                Case "D"
                    lblClasse.Text = "D - 61 a 90 d"
                Case "E"
                    lblClasse.Text = "E - 91 a 120 d"
                Case "F"
                    lblClasse.Text = "F - 121 a 150 d"
                Case "G"
                    lblClasse.Text = "G - 151 a 180 d"
                Case "H"
                    lblClasse.Text = "H - 181 a 359 d"
                Case "HH"
                    lblClasse.Text = "HH - acima 360 d"

            End Select

            SaldoTotal = 0
            QuantidadeRegistros = 0

            txtData.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnCarregar.UniqueID + "').click();return false;}} else {return true}; ")

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

            '            BindGridView1Data()

        End If

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

    Protected Sub BindGridView1Data()

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

        GridView1.DataSource = GetData()

        GridView1.DataBind()

    End Sub


    Private Function GetData() As DataTable

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

        ' alterei o nome da PROC
        Dim Table As DataTable = Util.ClassBD.GetExibirGrid("[scr_CARTEIRA_DETALHE] '" & _
                               Convert.ToDateTime(ContextoWeb.DadosTransferencia.DataReferencia).ToString("yyyyMMdd") & "'" _
                                                              , "RollrateDIARIO", strConn)


        '& "', '" & _
        '                               ContextoWeb.DadosTransferencia.Agente & "', '" & _
        '                               ContextoWeb.DadosTransferencia.Cobradora & "', " & _
        '                               ContextoWeb.DadosTransferencia.Linha & ", '" & _
        '                               ContextoWeb.DadosTransferencia.Classe & "'" _
        '                               , "RollrateDIARIO", strConn)


        'SaldoTotal = Table.Compute("sum(SALDO_INSCRITO_VAL)", "")
        'QuantidadeRegistros = Table.Compute("count(CONTRATO)", "")

        Return Table


    End Function

    Protected Sub BindGridView1DataView()

        GridView1.DataSource = DataGridView()
        GridView1.DataBind()

    End Sub


    Protected Sub btnVoltar_Click(sender As Object, e As EventArgs)

        ' ContextoWeb.Navegacao.LinkPaginaAtual = Me.AppRelativeVirtualPath

        Response.Redirect(ContextoWeb.Navegacao.LinkPaginaDetalhe)

    End Sub

    Protected Sub btnLocalizar_Click(sender As Object, e As EventArgs)
        FiltroDataTable()
    End Sub

    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs)
        txtLocalizar.Text = ""
        BindGridView1DataView()
    End Sub

    Private Sub FiltroDataTable()

        Dim Tabela As DataTable = DataGridView()
        Dim FilteredResults As New DataTable
        Dim DataRows() As DataRow

        FilteredResults = Tabela.Clone()

        DataRows = Tabela.Select("CONTRATO Like '%" & txtLocalizar.Text & _
        "%' or CPF_CNPJ Like '%" & txtLocalizar.Text & _
        "%' or NOME_CLIENTE Like '%" & txtLocalizar.Text & _
        "%' or AGENTE Like '%" & txtLocalizar.Text & _
        "%' or PLANO Like '%" & txtLocalizar.Text & _
        "%' or PARCELA_ABERTO Like '%" & txtLocalizar.Text & _
        "%' or ATRASO Like '%" & txtLocalizar.Text & _
        "%' or VLR_PARCELA Like '%" & txtLocalizar.Text & _
        "%' or QTD_PARC_ATRASO Like '%" & txtLocalizar.Text & _
        "%' or SALDO_INSCRITO Like '%" & txtLocalizar.Text & _
        "%' or VLR_FINANCIADO Like '%" & txtLocalizar.Text & _
        "%' or MARCA Like '%" & txtLocalizar.Text & _
        "%' or MODELO Like '%" & txtLocalizar.Text & _
        "%' or ANO_FABRIC Like '%" & txtLocalizar.Text & _
        "%' or PARC_PAGAS Like '%" & txtLocalizar.Text & _
        "%' or PROFISSAO Like '%" & txtLocalizar.Text & _
        "%' or CARGO Like '%" & txtLocalizar.Text &
        "%' or DATA_CONTRATO Like '%" & txtLocalizar.Text & _
        "%' or PRIMEIRO_VCTO Like '%" & txtLocalizar.Text & _
        "%' or VENCIMENTO Like '%" & txtLocalizar.Text & "%'")

        Dim i As Integer
        For i = 0 To DataRows.GetUpperBound(0)
            FilteredResults.ImportRow(DataRows(i))
        Next i

        'FilteredResults2.Clear()
        GridView1.DataSource = FilteredResults
        GridView1.DataBind()

        ' Session.Add("Filtro", FilteredResults)

        GridView1.Dispose()

    End Sub




    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        'Not Remove
        ' Confirms that an HtmlForm control is rendered for the specified ASP.NET
        '     server control at run time. 

    End Sub

    Protected Sub btnExcel_Click(sender As Object, e As ImageClickEventArgs)

        Try

            If Not IsNothing(GridView1.HeaderRow) Then

                Response.Clear()
                Response.Buffer = True
                Dim filename As String = String.Format("Contrato_DIARIO_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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

    Protected Sub btnImpressao_Click(sender As Object, e As ImageClickEventArgs)

        'Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

        'Dim ds As dsRollrateMensal
        'Dim cmd As New SqlCommand("[scr_RR_mensal_DETALHE] '" & _
        '                       Right(ContextoWeb.DadosTransferencia.DataReferencia, 4) & _
        '                       Mid(ContextoWeb.DadosTransferencia.DataReferencia, 4, 2) & _
        '                       Left(ContextoWeb.DadosTransferencia.DataReferencia, 2) & _
        '                       "','" & _
        '                           ContextoWeb.DadosTransferencia.Agente & "', '" & _
        '                           ContextoWeb.DadosTransferencia.Cobradora & "', " & _
        '                           ContextoWeb.DadosTransferencia.Linha & ", '" & _
        '                           ContextoWeb.DadosTransferencia.Classe & "'")

        'Using con As New SqlConnection(strConn)
        '    Using sda As New SqlDataAdapter()
        '        cmd.Connection = con
        '        sda.SelectCommand = cmd
        '        ds = New dsRollrateMensal()
        '        sda.Fill(ds, "TAUX_CONTRATO")
        '    End Using
        'End Using

        '' ContextoWeb.NewReportContext()
        'ContextoWeb.Relatorio.reportFileName = "../Relatorios/rptContratoDetalhe.rpt"
        'ContextoWeb.Relatorio.reportDatas.Add(New reportData(ds))

        '' ContextoWeb.Navegacao.LinkPaginaAtual = Me.AppRelativeVirtualPath
        '' ContextoWeb.Navegacao.TituloPaginaAtual = Me.Title

        '' ContextoWeb.Navegacao.LinkPaginaAnterior2 = ContextoWeb.Navegacao.LinkPaginaAnterior
        'ContextoWeb.Navegacao.LinkPaginaAnteriorRelatorio = Me.AppRelativeVirtualPath
        'Response.Redirect("Relatorio.aspx")

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
    End Sub

    Protected Sub btnHelp_Click(sender As Object, e As ImageClickEventArgs)
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
    End Sub

    Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
        Response.Redirect("Menu.aspx")
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        If e.NewPageIndex >= 0 Then
            GridView1.PageIndex = e.NewPageIndex
            BindGridView1DataView()
        End If
    End Sub

    Protected Sub GridView1_RowCreated(sender As Object, e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow And e.Row.RowState = DataControlRowState.Normal Then
            e.Row.CssClass = "GridviewScrollC3Item"
        End If
        If e.Row.RowType = DataControlRowType.DataRow And e.Row.RowState = DataControlRowState.Alternate Then
            e.Row.CssClass = "GridviewScrollC3Item2"
        End If

    End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then

                    Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

                    If IsDBNull(drow("EMPRESA")) Then
                        e.Row.Cells(0).Text = ""
                    Else
                        e.Row.Cells(0).Text = drow("EMPRESA")
                    End If
                    If IsDBNull(drow("CONTRATO")) Then
                        e.Row.Cells(1).Text = ""
                    Else
                        e.Row.Cells(1).Text = drow("CONTRATO")
                    End If
                    If IsDBNull(drow("CPF_CNPJ")) Then
                        e.Row.Cells(2).Text = ""
                    Else
                        e.Row.Cells(2).Text = drow("CPF_CNPJ")
                    End If
                    If IsDBNull(drow("NOME_CLIENTE")) Then
                        e.Row.Cells(3).Text = ""
                    Else
                        e.Row.Cells(3).Text = drow("NOME_CLIENTE")
                    End If
                    'If IsDBNull(drow("AGENTE")) Then
                    '    e.Row.Cells(0).Text = ""
                    'Else
                    '    e.Row.Cells(3).Text = drow("AGENTE")
                    'End If
                    'If IsDBNull(drow("DATA_CONTRATO")) Then
                    '    e.Row.Cells(0).Text = ""
                    'Else
                    '    e.Row.Cells(4).Text = drow("DATA_CONTRATO")
                    'End If
                    'If IsDBNull(drow("PLANO")) Then
                    '    e.Row.Cells(0).Text = ""
                    'Else
                    '    e.Row.Cells(5).Text = drow("PLANO")
                    'End If
                    'If IsDBNull(drow("PARCELA_ABERTO")) Then
                    '    e.Row.Cells(0).Text = ""
                    'Else
                    '    e.Row.Cells(6).Text = drow("PARCELA_ABERTO")
                    'End If
                    'If IsDBNull(drow("PRIMEIRO_VCTO")) Then
                    '    e.Row.Cells(0).Text = ""
                    'Else
                    '    e.Row.Cells(7).Text = drow("PRIMEIRO_VCTO")
                    'End If
                    'If IsDBNull(drow("VLR_FINANCIADO")) Then
                    '    e.Row.Cells(0).Text = ""
                    'Else
                    '    e.Row.Cells(13).Text = Util.CNumero.FormataNumero(Convert.ToInt32(Convert.ToDecimal(drow("VLR_FINANCIADO").ToString().Replace(".", ","))), 0)
                    'End If
                    'If IsDBNull(drow("MARCA")) Then
                    '    e.Row.Cells(0).Text = ""
                    'Else
                    '    e.Row.Cells(14).Text = drow("MARCA")
                    'End If
                    'If IsDBNull(drow("MODELO")) Then
                    '    e.Row.Cells(0).Text = ""
                    'Else
                    '    e.Row.Cells(15).Text = drow("MODELO")
                    'End If
                    'If IsDBNull(drow("ANO_FABRIC")) Then
                    '    e.Row.Cells(0).Text = ""
                    'Else
                    '    e.Row.Cells(16).Text = drow("ANO_FABRIC")
                    'End If
                    'If IsDBNull(drow("PARC_PAGAS")) Then
                    '    e.Row.Cells(0).Text = ""
                    'Else
                    '    e.Row.Cells(17).Text = drow("PARC_PAGAS")
                    'End If
                    'If IsDBNull(drow("PROFISSAO")) Then
                    '    e.Row.Cells(0).Text = ""
                    'Else
                    '    e.Row.Cells(18).Text = drow("PROFISSAO")
                    'End If
                    'If IsDBNull(drow("CARGO")) Then
                    '    e.Row.Cells(0).Text = ""
                    'Else
                    '    e.Row.Cells(19).Text = drow("CARGO")
                    'End If


                    If IsDBNull(drow("ATRASO")) Then
                        e.Row.Cells(4).Text = ""
                    Else
                        e.Row.Cells(4).Text = drow("ATRASO")
                    End If
                    If IsDBNull(drow("MODALIDADE")) Then
                        e.Row.Cells(5).Text = ""
                    Else
                        e.Row.Cells(5).Text = drow("MODALIDADE")
                    End If
                    If IsDBNull(drow("COD_CLI")) Then
                        e.Row.Cells(6).Text = ""
                    Else
                        e.Row.Cells(6).Text = drow("COD_CLI")
                    End If
                    If IsDBNull(drow("RATING_ORIGINAL")) Then
                        e.Row.Cells(7).Text = ""
                    Else
                        e.Row.Cells(7).Text = drow("RATING_ORIGINAL")
                    End If
                    If IsDBNull(drow("RATING_FINAL")) Then
                        e.Row.Cells(8).Text = ""
                    Else
                        e.Row.Cells(8).Text = drow("RATING_FINAL")
                    End If


                    If IsDBNull(drow("SALDO_INSCRITO")) Then
                        e.Row.Cells(9).Text = ""
                    Else
                        e.Row.Cells(9).Text = CNumero.FormataNumero(Convert.ToInt32(Convert.ToDecimal(drow("SALDO_INSCRITO").ToString().Replace(".", ","))), 0)
                    End If
                    If IsDBNull(drow("VF")) Then
                        e.Row.Cells(10).Text = ""
                    Else
                        e.Row.Cells(10).Text = Util.CNumero.FormataNumero(Convert.ToInt32(Convert.ToDecimal(drow("VF").ToString().Replace(".", ","))), 0)
                    End If
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub


    Protected Sub GridView1_Sorting(sender As Object, e As GridViewSortEventArgs)


        Dim SortDir As String = String.Empty

        If Direction = SortDirection.Ascending Then
            Direction = SortDirection.Descending
            SortDir = "Desc"
            sortImage.ImageUrl = "~/Imagens/desc.png"
        Else
            Direction = SortDirection.Ascending
            SortDir = "Asc"
            sortImage.ImageUrl = "~/Imagens/asc.png"
        End If

        Dim sortedView As New DataView(DataGridView())
        sortedView.Sort = e.SortExpression + " " & SortDir
        GridView1.DataSource = sortedView
        GridView1.DataBind()

        Dim columnIndex As Integer = 0
        For Each headerCell As DataControlFieldHeaderCell In GridView1.HeaderRow.Cells
            If headerCell.ContainingField.SortExpression = e.SortExpression Then
                columnIndex = GridView1.HeaderRow.Cells.GetCellIndex(headerCell)
            End If
        Next

        GridView1.HeaderRow.Cells(columnIndex).Controls.Add(sortImage)

    End Sub


    Public Property Direction() As SortDirection
        Get
            If ViewState("ABAFF5A7-9F4B-4750-979D-7F4340F879AE") Is Nothing Then
                ViewState("ABAFF5A7-9F4B-4750-979D-7F4340F879AE") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("ABAFF5A7-9F4B-4750-979D-7F4340F879AE"), SortDirection)
        End Get
        Set(value As SortDirection)
            ViewState("ABAFF5A7-9F4B-4750-979D-7F4340F879AE") = value
        End Set
    End Property


    Public Property DataGridView As DataTable
        Get
            If ViewState("1DF1A4A8-8FBD-450A-9303-786975E4D659") Is Nothing Then
                ViewState("1DF1A4A8-8FBD-450A-9303-786975E4D659") = GetData()
            End If
            Return DirectCast(ViewState("1DF1A4A8-8FBD-450A-9303-786975E4D659"), DataTable)
        End Get
        Set(value As DataTable)
            ViewState("1DF1A4A8-8FBD-450A-9303-786975E4D659") = value
        End Set
    End Property


    Public Property SaldoTotal() As Double
        Get
            If ViewState("D21F2315-2431-454D-8B10-89F1FC7B74AE") Is Nothing Then
                ViewState("D21F2315-2431-454D-8B10-89F1FC7B74AE") = 0
            End If
            Return DirectCast(ViewState("D21F2315-2431-454D-8B10-89F1FC7B74AE"), Double)
        End Get
        Set(value As Double)
            ViewState("D21F2315-2431-454D-8B10-89F1FC7B74AE") = value
        End Set
    End Property

    Public Property QuantidadeRegistros() As Integer
        Get
            If ViewState("91DD078E-DCD5-41DC-9A2E-C478EBCEE6ED") Is Nothing Then
                ViewState("91DD078E-DCD5-41DC-9A2E-C478EBCEE6ED") = 0
            End If
            Return DirectCast(ViewState("91DD078E-DCD5-41DC-9A2E-C478EBCEE6ED"), Integer)
        End Get
        Set(value As Integer)
            ViewState("91DD078E-DCD5-41DC-9A2E-C478EBCEE6ED") = value
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


End Class

