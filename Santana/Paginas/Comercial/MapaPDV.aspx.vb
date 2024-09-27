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

Public Class MapaPDV
    Inherits SantanaPage


    Dim strSortField As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            'Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(Now.Date.AddDAY(-1).ToString("MM/yyyy"))
            '            txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)

            txtData.Text = Now.Date.AddDays(-1).ToString("dd/MM/yyyy")
            Carrega_Relatorio()
            ddlRelatorio.SelectedIndex = 0


            'If ContextoWeb.DadosTransferencia.CodCobradora <> 0 Or ContextoWeb.DadosTransferencia.CodAgente <> 0 Then
            'ContextoWeb.DadosTransferencia.CodAgente = 0
            'ContextoWeb.DadosTransferencia.CodCobradora = 0
            'End If

        Else
            BindGridView1Data()
        End If

        txtData.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnCarregar.UniqueID + "').click();return false;}} else {return true}; ")

        Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

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


            ' DADOS DO RELATORIO
            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then

                    Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

                    '  DT_FECHA	COD_CEDN	NOM_CEDN	COD_OPERADOR	NOM_OPERADOR	CNPJ	TELEFONE	CONTATO	QTD_CANHOTO	PRC_CANHOTO	

                    e.Row.Cells(0).Text = drow("DT_FECHA")
                    e.Row.Cells(0).ForeColor = Cor

                    e.Row.Cells(1).Text = drow("COD_CEDN")
                    e.Row.Cells(1).ForeColor = Cor

                    e.Row.Cells(2).Text = drow("NOM_CEDN")
                    e.Row.Cells(2).ForeColor = Cor

                    e.Row.Cells(3).Text = drow("COD_OPERADOR")
                    e.Row.Cells(3).ForeColor = Cor

                    e.Row.Cells(4).Text = drow("NOM_OPERADOR")
                    e.Row.Cells(4).ForeColor = Cor

                    e.Row.Cells(5).Text = drow("CNPJ")
                    e.Row.Cells(5).ForeColor = Cor

                    e.Row.Cells(6).Text = drow("TELEFONE")
                    e.Row.Cells(6).ForeColor = Cor
                    e.Row.Cells(7).Text = drow("CONTATO")
                    e.Row.Cells(7).ForeColor = Cor
                    e.Row.Cells(8).Text = CNumero.FormataNumero(drow("QTD_CANHOTO"), 2)
                    e.Row.Cells(8).ForeColor = Cor
                    e.Row.Cells(9).Text = CNumero.FormataNumero(drow("PRC_CANHOTO"), 2)
                    e.Row.Cells(9).ForeColor = Cor

                    '   SLD_DEVEDOR_ATT	SLD_DEVEDOR_CDC	SLD_DEVEDOR_TTL	LIMITE	SLD_DISPONIVEL	COR_LINHA	ORDEM_LINHA
                    e.Row.Cells(10).Text = CNumero.FormataNumero(drow("SLD_DEVEDOR_ATT"), 2)
                    e.Row.Cells(10).ForeColor = Cor
                    e.Row.Cells(11).Text = CNumero.FormataNumero(drow("SLD_DEVEDOR_CDC"), 2)
                    e.Row.Cells(12).ForeColor = Cor
                    e.Row.Cells(12).Text = CNumero.FormataNumero(drow("SLD_DEVEDOR_TTL"), 2)
                    e.Row.Cells(13).ForeColor = Cor
                    e.Row.Cells(13).Text = CNumero.FormataNumero(drow("LIMITE"), 2)
                    e.Row.Cells(14).ForeColor = Cor
                    e.Row.Cells(14).Text = CNumero.FormataNumero(drow("SLD_DISPONIVEL"), 2)
                    e.Row.Cells(15).ForeColor = Cor
                    e.Row.Cells(15).Text = drow("COR_LINHA")
                    e.Row.Cells(16).ForeColor = Cor
                    e.Row.Cells(16).Text = drow("ORDEM_LINHA")

                    e.Row.Cells(17).Text = CNumero.FormataNumero(drow("saldoCC"), 2)
                    e.Row.Cells(17).ForeColor = Cor
                    e.Row.Cells(18).Text = CNumero.FormataNumero(drow("Prc_checa"), 2)
                    e.Row.Cells(18).ForeColor = Cor
                    e.Row.Cells(19).Text = CNumero.FormataNumero(drow("vlr_carteira_PV"), 2)
                    e.Row.Cells(19).ForeColor = Cor
                    e.Row.Cells(20).Text = CNumero.FormataNumero(drow("limite_op"), 2)
                    e.Row.Cells(20).ForeColor = Cor

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

        table = Util.ClassBD.GetExibirGrid("[SCR_MAPA_PDV] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', 1", "RRMENSALcubo", strConn)

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
        SortField = "cod_operador"
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
                Dim filename As String = String.Format("MapaPDV_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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
                    Response.Write("Produção De " & txtData.Text & " - Desconto ")

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
        SortField = "cod_operador"
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

