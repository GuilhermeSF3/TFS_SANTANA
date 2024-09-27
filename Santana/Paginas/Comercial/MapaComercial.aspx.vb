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

Public Class MapaComercial
    Inherits SantanaPage


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
            Carrega_Agente()
            Carrega_Cobradora()

            'If ContextoWeb.DadosTransferencia.CodAgente = 0 Then
            '    Carrega_Agente()
            'Else
            '    Carrega_Agente()
            '    ddlAgente.SelectedIndex = ddlAgente.Items.IndexOf(ddlAgente.Items.FindByValue(ContextoWeb.DadosTransferencia.CodAgente.ToString()))
            'End If


            'If ContextoWeb.DadosTransferencia.CodCobradora = 0 Then
            '    Carrega_Cobradora()
            'Else
            '    Carrega_Cobradora()
            '    ddlCobradora.SelectedIndex = ddlCobradora.Items.IndexOf(ddlCobradora.Items.FindByValue(ContextoWeb.DadosTransferencia.CodCobradora.ToString()))
            'End If

            Carrega_Relatorio()
            ddlRelatorio.SelectedIndex = 0


            If ContextoWeb.DadosTransferencia.CodCobradora <> 0 Or ContextoWeb.DadosTransferencia.CodAgente <> 0 Then
                BindGridView1Data()
                ContextoWeb.DadosTransferencia.CodAgente = 0
                ContextoWeb.DadosTransferencia.CodCobradora = 0
            End If

            'dvConsultasOperador.Visible = False
            'dvConsultasCarteiras.Visible = False
            'dvConsultasLojas.Visible = False

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

            ddlAgente.Items.Insert(0, New ListItem("Carteira", "1"))
            ddlAgente.Items.Insert(1, New ListItem("Carteira + Prejuizo", "2"))
            ddlAgente.Items.Insert(2, New ListItem("Prejuizo", "3"))

            ddlAgente.SelectedIndex = 0


        Catch ex As Exception

        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Carrega_Cobradora()
        ' VEICULO
        Try

            'ddlCobradora.Items.Insert(0, New ListItem("Todos", "99"))
            ddlCobradora.Items.Insert(0, New ListItem("Leves", "1"))
            ddlCobradora.Items.Insert(1, New ListItem("Motos", "2"))
            ddlCobradora.Items.Insert(2, New ListItem("Utilitários", "3"))
            'ddlCobradora.Items.Insert(4, New ListItem("Refinanciamento", "4"))
            'ddlCobradora.Items.Insert(5, New ListItem("Renegociação", "5"))
            'ddlCobradora.Items.Insert(6, New ListItem("Leves c/ Renegociação", "6"))
            'ddlCobradora.Items.Insert(7, New ListItem("Motos c/ Renegociação", "7"))
            'ddlCobradora.Items.Insert(8, New ListItem("Utilitários c/ Renegociação", "8"))

            ddlCobradora.SelectedIndex = 0


        Catch ex As Exception

        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Carrega_Relatorio()
        ' VEICULO
        Try

            ddlRelatorio.Items.Insert(0, New ListItem("Carteira", "1"))
            ddlRelatorio.Items.Insert(1, New ListItem("Faixa de Ano", "2"))
            ddlRelatorio.Items.Insert(2, New ListItem("Faixa de Parcelas", "3"))
            ddlRelatorio.Items.Insert(3, New ListItem("Faixa de Renda", "4"))
            ddlRelatorio.Items.Insert(4, New ListItem("Faixa de Idade", "8"))
            ddlRelatorio.Items.Insert(5, New ListItem("Ocupação", "9"))

            ddlRelatorio.Items.Insert(6, New ListItem("Agentes", "5"))
            ddlRelatorio.Items.Insert(7, New ListItem("Operador", "6"))
            ddlRelatorio.Items.Insert(8, New ListItem("Loja", "7"))

            ddlRelatorio.Items.Insert(9, New ListItem("Modelo Veic.", "10"))

            ddlRelatorio.SelectedIndex = 0


        Catch ex As Exception

        Finally
            GC.Collect()
        End Try
    End Sub





    Private Sub GridViewCarteiras_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewCarteiras.RowDataBound

        Try
            Dim Cor As Drawing.Color
            Dim Cor2 As Drawing.Color
            Dim Cor3 As Drawing.Color
            Cor2 = ColorTranslator.FromOle(&HFBD7D7)    'violeta
            Cor3 = ColorTranslator.FromOle(&HD3D3D3)    'cinza

            ' trata cabecalhos
            If e.Row.RowType = DataControlRowType.Header Then

                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)
                Dim ds As New DataSet
                Dim dt As DataTable = Nothing
                Dim table1 As DataTable = Nothing

                Dim command As SqlCommand = New SqlCommand( _
"SELECT F.DESCR FROM FX_ANO_COML F (NOLOCK) WHERE DT_DE = (SELECT	MAX(DT_DE) " & _
                                                        " FROM	FX_ANO_COML A1 (NOLOCK) " & _
                                                        " WHERE	A1.DT_DE <=  '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "' AND A1.VEICULO='" & ddlCobradora.SelectedValue & "'  ) " & " AND F.VEICULO='" & ddlCobradora.SelectedValue & "'  order by Ordem DESC", connection)

                command.Connection.Open()

                Dim ddlValues As SqlDataReader
                ddlValues = command.ExecuteReader()
                Dim I As Integer
                I = 4
                While (ddlValues.Read())
                    e.Row.Cells(I).Text = ddlValues.GetString(0)
                    I = I + 1
                End While
                'e.Row.Cells(2).Text = ddlValues.GetValue("DESCR")
                'e.Row.Cells(3).Text = ddlValues.GetValue("DESCR")
                'e.Row.Cells(4).Text = ddlValues.GetValue("DESCR")
                'e.Row.Cells(5).Text = ddlValues.GetValue("DESCR")


                command.Connection.Close()
                command.Connection.Dispose()
            End If


            ' DADOS DO RELATORIO
            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then

                    Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row


                    e.Row.Cells(0).Text = drow("DT_FECHA")
                    e.Row.Cells(0).ForeColor = Cor

                    e.Row.Cells(1).Text = drow("DESCR_VEIC")
                    e.Row.Cells(1).ForeColor = Cor

                    e.Row.Cells(2).Text = drow("DESCRICAO")
                    e.Row.Cells(2).ForeColor = Cor

                    e.Row.Cells(3).Text = drow("agente")
                    e.Row.Cells(3).ForeColor = Cor

                    If drow("fx1") = 0.0 Then
                        e.Row.Cells(4).Text = ""
                    Else
                        e.Row.Cells(4).Text = CNumero.FormataNumero(drow("fx1"), 4)
                    End If
                    e.Row.Cells(4).ForeColor = Cor

                    If drow("fx2") = 0.0 Then
                        e.Row.Cells(5).Text = ""
                    Else
                        e.Row.Cells(5).Text = CNumero.FormataNumero(drow("fx2"), 4)
                    End If
                    e.Row.Cells(5).ForeColor = Cor

                    If drow("fx3") = 0.0 Then
                        e.Row.Cells(6).Text = ""
                    Else
                        e.Row.Cells(6).Text = CNumero.FormataNumero(drow("fx3"), 4)
                    End If
                    e.Row.Cells(6).ForeColor = Cor

                    If drow("fx4") = 0.0 Then
                        e.Row.Cells(7).Text = ""
                    Else
                        e.Row.Cells(7).Text = CNumero.FormataNumero(drow("fx4"), 4)
                    End If
                    e.Row.Cells(7).ForeColor = Cor


                    If drow("fx5") = 0.0 Then
                        e.Row.Cells(8).Text = ""
                    Else
                        e.Row.Cells(8).Text = CNumero.FormataNumero(drow("fx5"), 4)
                    End If
                    e.Row.Cells(8).ForeColor = Cor
                    e.Row.Cells(9).Text = CNumero.FormataNumero(drow("TTL"), 4)
                    e.Row.Cells(9).ForeColor = Cor

                    'PERDA	COMISSAO	SPREAD	CARTEIRA	PRC_CARTEIRA	PRODUCAO_TRIM	PRC_PRODUCAO_TRIM


                    e.Row.Cells(10).Text = drow("ordem_linha")
                    e.Row.Cells(10).ForeColor = Cor

                    If InStr(drow("DESCRICAO"), "% 2") > 0 Then
                        e.Row.Cells(4).BackColor = Cor2
                        e.Row.Cells(5).BackColor = Cor2
                        e.Row.Cells(6).BackColor = Cor2
                        e.Row.Cells(7).BackColor = Cor3
                        e.Row.Cells(8).BackColor = Cor3
                    End If

                    If InStr(drow("DESCRICAO"), "% 3") > 0 Then
                        e.Row.Cells(6).BackColor = Cor2
                        e.Row.Cells(7).BackColor = Cor2
                        e.Row.Cells(8).BackColor = Cor2
                    End If

                End If
            End If


        Catch ex As Exception

        End Try
    End Sub





    Protected Sub GridViewCarteiras_RowCreated(sender As Object, e As GridViewRowEventArgs)
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


    Protected Sub BindGridView1Data()

        Dim objGrid As GridView

        objGrid = GridViewCarteiras
        
        objGrid.DataSource = GetData()
        objGrid.DataBind()
        objGrid.AllowPaging = "True"
    End Sub

    Protected Sub BindGridView1DataView()


        Dim objGrid As GridView

        objGrid = GridViewCarteiras
        
        objGrid.DataSource = DataGridView
        objGrid.DataBind()

    End Sub


    Private Function GetData() As DataTable

        ' System.Threading.Thread.Sleep(3000)

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim table As DataTable

        '        table = Util.ClassBD.GetExibirGrid("[scr_MAPACOMERCIAL] '" & Right(txtDataDE.Text, 4) & Mid(txtDataDE.Text, 4, 2) & Left(txtDataDE.Text, 2) & "', '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "','" & ddlCobradora.SelectedValue & "','" & ddlAgente.SelectedValue & "','" & ddlRelatorio.SelectedValue & "'", "RRMENSALcubo", strConn)

        table = Util.ClassBD.GetExibirGrid("[scr_MAPACOMERCIAL] '" & Right(txtDataDE.Text, 4) & Mid(txtDataDE.Text, 4, 2) & Left(txtDataDE.Text, 2) & "', '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "','" & ddlCobradora.SelectedValue & "'", "RRMENSALcubo", strConn)

        Return table

    End Function


    Protected Sub GridViewCarteiras_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GridViewCarteiras.PageIndex = IIf(e.NewPageIndex < 0, 0, e.NewPageIndex)
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
        Response.Redirect("../Menu.aspx")
    End Sub


    Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
        'Try
        'descricao,ordem_linha
        SortField = "descricao"
        BindGridView1Data()
        dvConsultasCarteiras.Visible = True


        'Catch ex As Exception

        'Finally
        ' GC.Collect()
        'End Try

    End Sub


    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        'Not Remove
        ' Confirms that an HtmlForm control is rendered for the specified ASP.NET
        '     server control at run time. 

    End Sub


    Protected Sub btnExcel_Click(sender As Object, e As EventArgs)

        Try

            Dim objGrid As GridView

            GridViewCarteiras.AllowPaging = False
            BindGridView1Data()
            objGrid = GridViewCarteiras
            

            ExportExcel(objGrid)


        Catch ex As Exception
            Throw ex
        End Try

    End Sub




    Private Sub ExportExcel(objGrid As GridView)


        Try

            If Not IsNothing(objGrid.HeaderRow) Then

                Response.Clear()
                Response.Buffer = True
                Dim filename As String = String.Format("MapaComercial_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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
                    'Response.Write("Produção De " & txtDataDE.Text & " Até " & txtData.Text & " - Relatório " & ddlRelatorio.SelectedItem.Text & " -  " & "  - Veiculo " & ddlCobradora.SelectedItem.Text)
                    Response.Write("Produção De " & txtDataDE.Text & " Até " & txtData.Text & " - Veiculo " & ddlCobradora.SelectedItem.Text)

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



    Protected Sub GridViewCarteiras_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridViewCarteiras.Sorting
        Dim arrSortExpr() As String
        Dim i As Integer
        If e.SortExpression = "" Then Return

        SortField = e.SortExpression
        BindGridView1Data()
        arrSortExpr = Split(e.SortExpression, " ")

        For i = 0 To GridViewCarteiras.Columns().Count - 1
            If (GridViewCarteiras.Columns(i).SortExpression = e.SortExpression) Then
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
                    GridViewCarteiras.Columns(i).SortExpression = arrSortExpr(0) & " " & arrSortExpr(1)
                End If
                Exit For
            End If

        Next

    End Sub






    Protected Sub ddlRelatorio_SelectedIndexChanged(sender As Object, e As EventArgs)

        GridViewCarteiras.DataSource = Nothing
        GridViewCarteiras.DataBind()
        ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = Nothing

        ''        dvConsultasCarteiras.Visible = False
        SortField = "DESCRICAO"
        '"ordem_linha,DESCR_veic"
        

    End Sub

    Protected Sub GridViewCarteiras_DataBound(sender As Object, e As EventArgs)
        Dim gridView As GridView = CType(sender, GridView)
        If Not IsNothing(gridView.DataSource) AndAlso CType(gridView.DataSource, System.Data.DataTable).Rows.Count > 0 Then
            Util.CWebPageUtil.PreencheListaDePaginas(gridView, CType(gridView.BottomPagerRow.Cells(0).FindControl("PagerPages"), DropDownList))
        End If
    End Sub


    Protected Sub PagerPages_SelectedIndexChanged(sender As Object, e As EventArgs)


        Dim objGrid As GridView

        objGrid = GridViewCarteiras
        

        objGrid.DataSource = DataGridView()
        objGrid.PageIndex = CType(sender, DropDownList).SelectedIndex
        objGrid.DataBind()


    End Sub

End Class

