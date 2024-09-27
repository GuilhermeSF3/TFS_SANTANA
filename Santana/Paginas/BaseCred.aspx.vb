Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Web.Security
Imports System.Drawing.Printing
Imports System.Data.Common

Imports Microsoft.VisualBasic
Imports Santana.Seguranca
Imports Util

Public Class BaseCred
    Inherits SantanaPage


    Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
    Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load


        If Not IsPostBack Then

            Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime(Now.Date.ToString("dd/MM/yyyy"))
            ' inicia com fim do mes anterior
            ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddMonths(-1)
            ' mostra data na tela
            txtData.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)


            If ContextoWeb.DadosTransferencia.CodAgente = 0 Then
                Carrega_Agente()
            Else
                Carrega_Agente()
                ddlAgente.SelectedIndex = ddlAgente.Items.IndexOf(ddlAgente.Items.FindByValue(ContextoWeb.DadosTransferencia.CodAgente.ToString()))
            End If


            'If ContextoWeb.DadosTransferencia.CodCobradora = 0 Then
            '    Carrega_Cobradora()
            'Else
            '    Carrega_Cobradora()
            '    ddlCobradora.SelectedIndex = ddlCobradora.Items.IndexOf(ddlCobradora.Items.FindByValue(ContextoWeb.DadosTransferencia.CodCobradora.ToString()))
            'End If

            ' If ContextoWeb.DadosTransferencia.CodCobradora <> 0 Or ContextoWeb.DadosTransferencia.CodAgente <> 0 Then
            If ContextoWeb.DadosTransferencia.CodAgente <> 0 Then
                BindGridView1Data()
                ContextoWeb.DadosTransferencia.CodAgente = 0
                ' ContextoWeb.DadosTransferencia.CodCobradora = 0
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

        txtData.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnCarregar.UniqueID + "').click();return false;}} else {return true}; ")

        Const script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
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

    Protected Sub BindGridView1Data()

        GridView1.DataSource = GetData()
        GridView1.DataBind()

    End Sub


    Private Function GetData() As DataTable

        ' System.Threading.Thread.Sleep(3000)

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        'Dim table As DataTable = ClassBD.GetExibirGrid("[scr_ip_safra_diaria] '" & Convert.ToDateTime(txtData.Text).ToString("yyyyMMdd") & "', '" & ddlAgente.SelectedValue & "','" & ddlCobradora.SelectedValue & "'", "IndicePerformance", strConn)
        'Dim table As DataTable = ClassBD.GetExibirGrid("[scr_CARTEIRA_DETALHE] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', '" & ddlAgente.SelectedValue & "','" & ddlCobradora.SelectedValue & "'", "IndicePerformance", strConn)

        Dim table As DataTable = ClassBD.GetExibirGrid("[scr_CARTEIRA_DETALHE] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', '" & ddlAgente.SelectedValue & "', '" & txtLocalizar.Text & "'", "BaseCred", strConn)
        ' antes sem carteira ou prejuizo
        ' Dim table As DataTable = ClassBD.GetExibirGrid("[scr_CARTEIRA_DETALHE] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "'", "BaseCred", strConn)

        Return table

    End Function




    Private Sub Carrega_Agente()

        Try

            ddlAgente.Items.Insert(0, New ListItem("Carteira", "1"))
            ddlAgente.Items.Insert(1, New ListItem("Prejuizo", "2"))
            ddlAgente.Items.Insert(1, New ListItem("Todos", "3"))

            ddlAgente.SelectedIndex = 0


        Catch ex As Exception

        Finally
            GC.Collect()
        End Try

    End Sub



    Protected Sub BindGridView1DataView()

        GridView1.DataSource = DataGridView
        GridView1.DataBind()

    End Sub


    
    ' FUNCIONA COM O BOTAO CARREGAR

    'Protected Sub btnLocalizar_Click(sender As Object, e As EventArgs)
    'FiltroDataTable()
    'End Sub

    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs)


        txtLocalizar.Text = ""
        '        BindGridView1DataView()


        'txtLocalizar.Text = ""

        'FilteredResults.Clear()
        'GridView1.DataSource = FilteredResults
        'GridView1.DataBind()

        'GridView1.DataSource = GetData()
        'GridView1.DataBind()

        'nao existe a sub, existe
        'BindGridView1DataView()

        ' Session.Add("Filtro", FilteredResults)
        'GridView1.Dispose()


    End Sub

    Private Sub FiltroDataTable()

        '        Public Tabela As DataTable '= DataGridView()
        '        Public FilteredResults As New DataTable

        Dim Tabela As DataTable = DataGridView()
        Dim FilteredResults As New DataTable

        Tabela = DataGridView()
        Dim DataRows() As DataRow

        FilteredResults = Tabela.Clone()

        DataRows = Tabela.Select("CONTRATO Like '%" & txtLocalizar.Text & _
        "%' or CPF_CNPJ Like '%" & txtLocalizar.Text & _
        "%' or NOME_CLIENTE Like '%" & txtLocalizar.Text & _
        "%' or cod_cli Like '%" & txtLocalizar.Text & "%'")

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


    Protected Sub btnExcel_Click(sender As Object, e As EventArgs)

        Try

            If Not IsNothing(GridView1.HeaderRow) Then

                Response.Clear()
                Response.Buffer = True
                Dim filename As String = String.Format("BaseCred_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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

                            Dim controls As List(Of Control)
                            controls = cell.Controls.Cast(Of Control)().ToList()

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
                    Dim sb As New StringBuilder
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
            Throw
        End Try

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

        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
    End Sub


    Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
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


    Protected Sub PagerPages_SelectedIndexChanged(sender As Object, e As EventArgs)
        GridView1.DataSource = DataGridView()
        GridView1.PageIndex = CType(sender, DropDownList).SelectedIndex
        GridView1.DataBind()
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

        If e.NewPageIndex >= 0 Then
            GridView1.PageIndex = e.NewPageIndex
            BindGridView1DataView()
        End If
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













    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        Try
            'Dim Cor As Drawing.Color
            'Dim bold As Boolean

            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then

                    Dim drow As DataRow = CType(e.Row.DataItem, DataRowView).Row


                    'If drow("DESCRICAO") = "TOTAL GERAL" Then
                    '    bold = True
                    '    Cor = Drawing.ColorTranslator.FromOle(&HCC0000)
                    'ElseIf drow("FORMATO") = "A" Then
                    '    Cor = Drawing.ColorTranslator.FromOle(&HCC)
                    'Else
                    '    bold = False
                    '    Cor = Drawing.ColorTranslator.FromOle(&H666666)
                    'End If

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
                        e.Row.Cells(9).Text = CNumero.FormataNumero(drow("SALDO_INSCRITO"), 2)
                    End If
                    If IsDBNull(drow("VF")) Then
                        e.Row.Cells(10).Text = ""
                    Else
                        e.Row.Cells(10).Text = Util.CNumero.FormataNumero(drow("VF"), 2)
                    End If
                    If IsDBNull(drow("cod_modalidade")) Then
                        e.Row.Cells(11).Text = ""
                    Else
                        e.Row.Cells(11).Text = Util.CNumero.FormataNumero(drow("cod_modalidade"), 0)
                    End If


                End If
            End If


        Catch ex As Exception

        End Try
    End Sub


    Protected Sub hfGridView1SV_ValueChanged(sender As Object, e As EventArgs)
        Session(HfGridView1Svid) = hfGridView1SV.Value
    End Sub

    Protected Sub hfGridView1SH_ValueChanged(sender As Object, e As EventArgs)
        Session(HfGridView1Shid) = hfGridView1SH.Value
    End Sub


    'Public Property DataGridView As DataTable
    '    Get

    '        If ViewState("1DF1A4A8-8FBD-450A-9303-786975E4D659") Is Nothing Then
    '            ViewState("1DF1A4A8-8FBD-450A-9303-786975E4D659") = GetData()
    '        End If

    '        Return DirectCast(ViewState("1DF1A4A8-8FBD-450A-9303-786975E4D659"), DataTable)
    '    End Get
    '    Set(value As DataTable)
    '        ViewState("1DF1A4A8-8FBD-450A-9303-786975E4D659") = value
    '    End Set
    'End Property

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



    Protected Sub GridView1_DataBound(sender As Object, e As EventArgs)
        Dim gridView As GridView = CType(sender, GridView)
        If CType(gridView.DataSource, DataTable).Rows.Count > 0 Then
            CWebPageUtil.PreencheListaDePaginas(gridView, CType(gridView.BottomPagerRow.Cells(0).FindControl("PagerPages"), DropDownList))
        End If
    End Sub



End Class
'End Namespace