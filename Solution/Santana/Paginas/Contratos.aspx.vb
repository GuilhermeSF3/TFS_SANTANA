Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports Util
Imports System
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.IO

Public Class Cobranca_Dias_Atraso_Contratos
    Inherits System.Web.UI.Page
#Region "Variáveis"
    Public sDAtraso As String
    Public nMes As Integer
    Public nAno As Integer
    Public dataref As Date
    Public sFAIXA As String
    Public sPARCELAS As String
    Public dtini As String
    Public dtfim As String
    Public codProd As String
    Public parcelas As String
    Public atraso As Integer
    Public Tabela As DataTable = Nothing
    Public Tabela2 As DataTable = Nothing
    Public FilteredResults As New DataTable
    Public FilteredResults2 As New DataTable
    Public ds As New DataSet
    Public Contador As Integer
    Public sVop As Double
    Public sPmt As Double
    Public sPmt2 As Double
    Public sTtl As Integer
    Public codEmp As String
    Public sSldo As Double
    Public sParAtr As Integer
    Public sVlAtr As Double
    Public codGer As Integer
    Public codFil As Integer
    Public codOper As Integer
    Public codLoj As Integer
    Public codReg As String
    Public codCob As String

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sUsuario As String = Session.Item("acesso")
        Dim sPerfil As Integer = Session.Item("perfil")

        If sUsuario = Nothing Then
            Response.Redirect("~/Logon.aspx", True)
        End If

        lblMensagem.Text = ""



        Session.Add("Analista", codProd)

        If txtInicio.Text = "" Then
            txtInicio.Text = "01/" & Right("0" & CType(Now.Month, String), 2) & "/" & CType(Now.Year, String)
        End If

        If txtFim.Text = "" Then
            txtFim.Text = Right("0" & CType(Now.Day, String), 2) & "/" & Right("0" & CType(Now.Month, String), 2) & "/" & CType(Now.Year, String)
        End If

        If sPerfil >= 5 Then
            Exportar.Visible = True
        End If

        If sPerfil <= 1 Then
            Response.Redirect("~/Menu.aspx", True)
        End If

        codProd = Request.QueryString("codProd")

        If Not IsPostBack Then
            codProd = "31"
            Carrega_lojas()
            Carrega_Operador()
            Carrega_Gerente()
            Carrega_Regiao()
            Carrega_Filial()
            Carrega_Cobradoras()
            Carrega_Empregadores()

            If Request.QueryString("dtini") <> "" Then
                txtInicio.Text = Request.QueryString("dtini")
                txtFim.Text = Request.QueryString("dtfim")
                codProd = Request.QueryString("codProd")
                txtAtraso.Text = Request.QueryString("atraso")
                Carrega_lojas()
                Carrega_Operador()
                Carrega_Gerente()
                Carrega_Regiao()
                Carrega_Filial()
                Carrega_Cobradoras()
                Carrega_Empregadores()
            End If

        Else
            GridView1.DataSource = Session.Item("GridDados")
        End If

        carregar()

        OK.Focus()
        TxtLocalizar.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + BtnLocalizar.UniqueID + "').click();return false;}} else {return true}; ")
        txtFim.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + OK.UniqueID + "').click();return false;}} else {return true}; ")
        txtInicio.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + OK.UniqueID + "').click();return false;}} else {return true}; ")
    End Sub

    Protected Sub Retorna_Click(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles Retorna.Click
        Session.Remove("GridDados")
        Session.Remove("GridDados2")
        Session.Remove("Filtro")
        Session.Remove("FilTodos")
        Response.Redirect("~/Menu.aspx", True)
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) ' Handles GridView1.PageIndexChanging
        If Session.Item("Filtro") Is Nothing Then
            Tabela = Session.Item("GridDados")
        Else
            Tabela = Session.Item("Filtro")
        End If

        GridView1.DataSource = Tabela
        If e.NewPageIndex >= 0 Then
            GridView1.PageIndex = e.NewPageIndex
            GridView1.DataBind()
        End If
    End Sub

    Sub PagerPages_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Session.Item("Filtro") Is Nothing Then
            Tabela = Session.Item("GridDados")
        Else
            Tabela = Session.Item("Filtro")
        End If

        GridView1.DataSource = Tabela
        GridView1.PageIndex = CType(sender, DropDownList).SelectedIndex
        GridView1.DataBind()
    End Sub

    Protected Sub GridView1_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DataBound
        Dim gridView As GridView = CType(sender, GridView)
        If CType(gridView.DataSource, System.Data.DataTable).Rows.Count > 0 Then
            Util.CWebPageUtil.PreencheListaDePaginas(gridView, CType(gridView.BottomPagerRow.Cells(0).FindControl("PagerPages"), DropDownList))
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If Not e.Row.DataItem Is Nothing Then
            Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(0).Text = drow("PANROPER")
            If (e.Row.RowType = DataControlRowType.DataRow) Then
                e.Row.Cells(0).CssClass = "locked"
            End If
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(1).Text = drow("OPDTBASE") & "&nbsp;"
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(2).Text = drow("REGIAO") & "&nbsp;"
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(3).Text = drow("O1DESCR")
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(4).Text = drow("CLNOMECLI")
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(5).Text = drow("CLCGC") & "&nbsp;"
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(6).Text = drow("CLFONEFIS")
            e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(7).Text = drow("CLFONECOM")
            e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(8).Text = drow("CODEMPREGADOR")
            e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(9).Text = drow("O4NOME")
            e.Row.Cells(10).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(19).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(19).Text = drow("PADTVCTO") & "&nbsp;"
            e.Row.Cells(20).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(20).Text = drow("PADTVCTO2") & "&nbsp;"
            e.Row.Cells(21).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(23).Text = drow("COBRADORA")
            e.Row.Cells(24).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(24).Text = drow("DT_ENVIO_COBR")
            e.Row.Cells(25).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(25).Text = drow("ENVIO_EST_NOV")
            e.Row.Cells(26).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(26).Text = drow("O2DESCR")
            e.Row.Cells(27).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(27).Text = drow("O3DESCR")
            e.Row.Cells(28).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(28).Text = drow("Modalidade")
            e.Row.Cells(29).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(29).Text = drow("AbAnoFab")
            e.Row.Cells(30).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(30).Text = drow("AbModelo")
            e.Row.Cells(31).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(31).Text = drow("PDD")
            e.Row.Cells(32).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(33).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(33).Text = drow("MATRICULA")

            If e.Row.RowType = DataControlRowType.DataRow Then
                sVop = DirectCast(e.Row.DataItem, Data.DataRowView).DataView.Table.Compute("sum(OPVLRFIN)", "")
                sPmt = DirectCast(e.Row.DataItem, Data.DataRowView).DataView.Table.Compute("sum(PMT)", "")
                sPmt2 = DirectCast(e.Row.DataItem, Data.DataRowView).DataView.Table.Compute("sum(PMT2)", "")
                sTtl = DirectCast(e.Row.DataItem, Data.DataRowView).DataView.Table.Compute("count(CLNOMECLI)", "")
                sSldo = DirectCast(e.Row.DataItem, Data.DataRowView).DataView.Table.Compute("sum(SALDO)", "")
                sParAtr = DirectCast(e.Row.DataItem, Data.DataRowView).DataView.Table.Compute("sum(QTD_ATRASO)", "")
                sVlAtr = DirectCast(e.Row.DataItem, Data.DataRowView).DataView.Table.Compute("sum(VLR_ATRASO)", "")

            ElseIf e.Row.RowType = DataControlRowType.Footer Then
                Dim lbl As Label
                Dim lbl2 As Label
                Dim lbl3 As Label
                Dim lbl4 As Label
                Dim lbl5 As Label
                Dim lbl6 As Label
                Dim lbl7 As Label
                lbl = e.Row.FindControl("lbltotal")
                lbl.Text = String.Format("{0:C}", sVop)
                lbl2 = e.Row.FindControl("lbltotal2")
                lbl2.Text = String.Format("{0:C}", sPmt)
                lbl3 = e.Row.FindControl("lbltotal3")
                lbl3.Text = String.Format("{0:C}", sTtl)
                lbl4 = e.Row.FindControl("lbltotal4")
                lbl4.Text = String.Format("{0:C}", sPmt2)
                lbl5 = e.Row.FindControl("lbltotal4")
                lbl5.Text = String.Format("{0:C}", sSldo)
                lbl6 = e.Row.FindControl("lbltotal4")
                lbl6.Text = String.Format("{0:C}", sParAtr)
                lbl7 = e.Row.FindControl("lbltotal4")
                lbl7.Text = String.Format("{0:C}", sVlAtr)

            End If

            For i = 0 To 33
                e.Row.Cells(i).ForeColor = Drawing.Color.Black
            Next i
        End If
    End Sub

    Protected Sub OK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OK.Click
        'TxtLocalizar.Text = ""
        Session.Remove("FilTodos")
        Session.Remove("Filtro")
        Session.Remove("GridDados")

        codReg = ddlRegiao.SelectedValue
        codFil = ddlFil.SelectedValue
        codGer = ddlGer.SelectedValue
        codOper = ddlOper.SelectedValue
        codLoj = ddlLojas.SelectedValue
        codCob = ddlCob.SelectedValue
        codEmp = ddlEmpreg.SelectedValue

        If codProd = Nothing Then
            codProd = "31"
        End If

        carregar()
        Try
            sDAtraso = txtAtraso.Text

            dtini = Right(Trim(txtInicio.Text), 4) + Mid(Trim(txtInicio.Text), 4, 2) + Left(Trim(txtInicio.Text), 2)
            nMes = CType(Mid(Trim(txtInicio.Text), 4, 2), Integer)
            nAno = CType(Right(Trim(txtInicio.Text), 4), Integer)

            dtfim = Right(Trim(txtFim.Text), 4) + Mid(Trim(txtFim.Text), 4, 2) + Left(Trim(txtFim.Text), 2)
            nMes = CType(Mid(Trim(txtFim.Text), 4, 2), Integer)
            nAno = CType(Right(Trim(txtFim.Text), 4), Integer)


            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim connection As New SqlConnection(strConn)
            Dim ds As New DataSet
            Dim dataTable As DataTable = Nothing
            Dim sTabela As String = ""
            Dim sTitulo As String = ""
            Dim sRodape As String = ""

            GridView1.DataSource = Util.ClassBD.GetExibirGrid("scr_RR_mensal_DETALHE'" & dtini & "'," & RTrim(codReg) & ",'" & codFil & "','" & codGer & "','" & codOper & "','" & codLoj & "','" & codCob & "','" & txtParcelas.Text & "','" & sDAtraso & "','" & codEmp & "' ", "Estrutura", strConn)

            Session.Add("GridDados", GridView1.DataSource())
            Session.Add("FilTodos", GridView1.DataSource())
            GridView1.DataBind()

        Catch ex As Exception
            lblMensagem.Text = (ex.Message & "&nbsp;Favor entrar em contato com o   Ramal - 5748")
        Finally
            GC.Collect()
        End Try

    End Sub

    Protected Sub BtnLocalizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnLocalizar.Click
        FiltroDataTable()
    End Sub

    Private Sub FiltroDataTable()

        Tabela = Session.Item("GridDados")

        If Tabela Is Nothing Then
            OK_Click(Nothing, Nothing)
            Tabela = Session.Item("GridDados")
        End If

        FilteredResults = Tabela.Clone()

        Dim DataRows() As DataRow
        DataRows = Tabela.Select("PANROPER Like '%" & TxtLocalizar.Text & "%' or CLNOMECLI Like '%" & TxtLocalizar.Text & "%' or O4NOME Like '%" & TxtLocalizar.Text & "%' or COBRADORA Like '%" & TxtLocalizar.Text & "%' or O1DESCR Like '%" & TxtLocalizar.Text & "%' or O2DESCR Like '%" & TxtLocalizar.Text & "%' or O3DESCR Like '%" & TxtLocalizar.Text & "%' or CLCGC Like '%" & TxtLocalizar.Text & "%' or REGIAO Like '%" & TxtLocalizar.Text & "%' or AbAnoFab Like '%" & TxtLocalizar.Text & "%' or AbModelo Like '%" & TxtLocalizar.Text & "%' or MATRICULA Like '%" & TxtLocalizar.Text & "%'  or MODALIDADE Like '%" & TxtLocalizar.Text & "%' or OPDTBASE Like '%" & TxtLocalizar.Text & "%'")

        Dim i As Integer

        For i = 0 To DataRows.GetUpperBound(0)

            FilteredResults.ImportRow(DataRows(i))

        Next i

        GridView1.DataSource = FilteredResults
        GridView1.DataBind()
        Session.Add("Filtro", FilteredResults)
        GridView1.Dispose()

    End Sub

    Protected Sub BtnTodos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnTodos.Click
        TxtLocalizar.Text = ""
        DataTableTodos()
    End Sub

    Private Sub DataTableTodos()

        Tabela = Session.Item("FilTodos")

        If Tabela Is Nothing Then
            OK_Click(Nothing, Nothing)
            Tabela = Session.Item("FilTodos")
        End If

        FilteredResults = Tabela.Clone()

        Dim DataRows() As DataRow
        DataRows = Tabela.Select("")

        Dim i As Integer

        For i = 0 To DataRows.GetUpperBound(0)

            FilteredResults.ImportRow(DataRows(i))

        Next i

        GridView1.DataSource = FilteredResults
        GridView1.DataBind()

        Session.Remove("Filtro")
        GridView1.Dispose()

    End Sub



#Region "Excel"

    Protected Sub ExportarBROffice(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExpBrOffice.Click
        If Session.Item("Filtro") Is Nothing Then
            Tabela = Session.Item("GridDados")
        Else
            Tabela = Session.Item("Filtro")
        End If

        Dim ary As Array
        Dim dtRef As String = txtAtraso.Text
        Dim lbl1 As New Label
        Dim lbl2 As New Label
        Dim sw As New StringWriter()
        Dim tw As New StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        Dim frm As HtmlForm = New HtmlForm()

        lbl1.Text = "Contratos com Atraso acima de x dias, podendo selecionar o numero da parcela e intervalo de vencimento, Com filtro de Cliente, Loja, Filial ou Supervisora.<br>"
        lbl2.Text = "Posição on-line."

        Response.Clear()
        Response.ContentEncoding = System.Text.Encoding.Default
        Dim GridView2 As New GridView()
        GridView2.AllowPaging = False
        GridView2.DataSource = Tabela
        GridView2.ShowHeader = True
        GridView2.DataBind()

        If Tabela Is Nothing Then
            Exit Sub
        End If

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Dias_Atraso_Estrutura.xls")

        Response.Write(vbTab & vbTab & vbTab & Label1.Text & vbTab & vbTab & vbTab)

        Response.Write(vbCrLf)

        Response.Write(vbTab & vbTab & vbTab & "Atraso Acima de:" & vbTab & vbTab & txtAtraso.Text & vbTab & "Dias" & vbTab)
        Response.Write(vbCrLf)

        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"


        For i As Integer = 0 To GridView1.Columns.Count - 1
            GridView2.HeaderRow.Cells(i).Text = GridView1.Columns(i).HeaderText
            Response.Write(GridView2.HeaderRow.Cells(i).Text.ToString & vbTab)
        Next

        Response.Write(vbCrLf)
        For Each dr In Tabela.Rows
            ary = dr.ItemArray
            For i = 0 To UBound(ary)
                Response.Write(ary(i).ToString & vbTab)
            Next
            Response.Write(vbCrLf)
        Next

        Response.Output.Write(sw.ToString())
        Response.Write(tw.ToString())
        Response.Flush()
        Response.End()
        Session.Remove("GridDados")

    End Sub

    Protected Sub ExportarExcel(ByVal sender As Object, ByVal e As System.EventArgs) Handles Exportar.Click
        If Session.Item("Filtro") Is Nothing Then
            Tabela = Session.Item("GridDados")
        Else
            Tabela = Session.Item("Filtro")
        End If

        Dim dtRef As String = txtAtraso.Text
        Dim parc As String = txtParcelas.Text
        Dim lbl1 As New Label
        Dim lbl2 As New Label
        Dim sw As New StringWriter()
        Dim tw As New StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        Dim frm As HtmlForm = New HtmlForm()
        Dim style As String = "<style>.textmode{mso-number-format:\@;}</style>"

        lbl1.Text = "Contratos com Atraso acima de x dias, podendo selecionar o numero da parcela e intervalo de vencimento, Com filtro de Cliente, Loja, Filial ou Supervisora.<br>"
        lbl2.Text = "Posição on-line."

        Response.Clear()
        Response.ContentEncoding = System.Text.Encoding.Default

        Dim GridView2 As New GridView()
        GridView2.AllowPaging = False
        GridView2.DataSource = Tabela
        GridView2.ShowHeader = True
        GridView2.DataBind()

        If Tabela Is Nothing Then
            Exit Sub
        End If

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Dias_Atraso_Estrutura.xls")
        Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & Label1.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>")
        Response.Write("&nbsp;Atraso Acima de: &nbsp;&nbsp;" & dtRef & "&nbsp;&nbsp; Dias &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Parcelas:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtParcelas.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Vencimentos de: &nbsp;&nbsp;" & txtInicio.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Até &nbsp;&nbsp;" & txtFim.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>")
        Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"

        For i As Integer = 0 To GridView1.Columns.Count - 1
            GridView2.HeaderRow.Cells(i).Text = GridView1.Columns(i).HeaderText
        Next

        For i As Integer = 1 To GridView1.Rows.Count - 1
            GridView2.Rows(i).Attributes.Add("class", "textmode")
        Next
        GridView2.RenderControl(hw)
        frm.Controls.Add(lbl2)


        Response.Write(style)
        Response.Output.Write(sw.ToString())
        Response.Write(tw.ToString())
        Response.Flush()
        Response.End()

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Não apagar esta Sub -> Exportação para o Excel
    End Sub

#End Region

    Protected Sub GridView1_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DataBinding

    End Sub

#Region "Ordenação"

    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        If Session.Item("Filtro") Is Nothing Then
            Tabela = Session.Item("GridDados")
        Else
            Tabela = Session.Item("Filtro")
        End If

        Dim dv As DataView = Tabela.DefaultView
        Dim sd As String = ""

        If Not Tabela Is Nothing Or Not Tabela Is "" Then
            Dim arrSortExpr() As String
            Dim i As Integer
            If e.SortExpression = "" Then Return
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

            Try
                dv.Sort = e.SortExpression + " " + sd
                Tabela = dv.ToTable
                GridView1.DataSource = Tabela
                GridView1.DataBind()
            Catch ex As Exception
            End Try
        End If

    End Sub
#End Region

#Region "Botões Menu"
    Protected Sub btnCdcVei_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCdcVei.Click
        Session.Remove("GridDados")
        Session.Remove("GridDados2")
        Session.Remove("Filtro")
        Session.Remove("FilTodos")
        Response.Redirect("Contratos.aspx?&dtini=" + txtInicio.Text + "&dtfim=" + txtFim.Text + "&codProd=31" + "&atraso=" + txtAtraso.Text)
    End Sub

    Protected Sub btnLeasVei_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLeasVei.Click
        Session.Remove("GridDados")
        Session.Remove("GridDados2")
        Session.Remove("Filtro")
        Session.Remove("FilTodos")
        Response.Redirect("Contratos.aspx?&dtini=" + txtInicio.Text + "&dtfim=" + txtFim.Text + "&codProd=33" + "&atraso=" + txtAtraso.Text)
    End Sub

    Protected Sub btnOmni_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOmni.Click
        Session.Remove("GridDados")
        Session.Remove("GridDados2")
        Session.Remove("Filtro")
        Session.Remove("FilTodos")
        Response.Redirect("Contratos.aspx?&dtini=" + txtInicio.Text + "&dtfim=" + txtFim.Text + "&codProd=32" + "&atraso=" + txtAtraso.Text)
    End Sub

    Protected Sub btnLeasCdc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLeasCdc.Click
        Session.Remove("GridDados")
        Session.Remove("GridDados2")
        Session.Remove("Filtro")
        Session.Remove("FilTodos")
        Response.Redirect("Contratos.aspx?&dtini=" + txtInicio.Text + "&dtfim=" + txtFim.Text + "&codProd=39" + "&atraso=" + txtAtraso.Text)
    End Sub


    Protected Sub btnConsPriv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsPriv.Click
        Session.Remove("GridDados")
        Session.Remove("GridDados2")
        Session.Remove("Filtro")
        Session.Remove("FilTodos")
        Response.Redirect("Contratos.aspx?&dtini=" + txtInicio.Text + "&dtfim=" + txtFim.Text + "&codProd=21" + "&atraso=" + txtAtraso.Text)
    End Sub

    Protected Sub btnCdcLj_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCdcLj.Click
        Session.Remove("GridDados")
        Session.Remove("GridDados2")
        Session.Remove("Filtro")
        Session.Remove("FilTodos")
        Response.Redirect("Contratos.aspx?&dtini=" + txtInicio.Text + "&dtfim=" + txtFim.Text + "&codProd=22" + "&atraso=" + txtAtraso.Text)
    End Sub

    Protected Sub btnConsPub_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsPub.Click
        Session.Remove("GridDados")
        Session.Remove("GridDados2")
        Session.Remove("Filtro")
        Session.Remove("FilTodos")
        Response.Redirect("Contratos.aspx?&dtini=" + txtInicio.Text + "&dtfim=" + txtFim.Text + "&codProd=11" + "&atraso=" + txtAtraso.Text)
    End Sub

    Protected Sub btnPubAqui_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPubAqui.Click
        Session.Remove("GridDados")
        Session.Remove("GridDados2")
        Session.Remove("Filtro")
        Session.Remove("FilTodos")
        Response.Redirect("Contratos.aspx?&dtini=" + txtInicio.Text + "&dtfim=" + txtFim.Text + "&codProd=41" + "&atraso=" + txtAtraso.Text)
    End Sub

#End Region

    Public Sub carregar()
        If codProd = "31" Then
            Label1.Text = "COBRANÇA / DIAS DE ATRASO / CONTRATOS / CDC VEÍCULOS"
            btnCdcVei.BackColor = Drawing.Color.White
            btnCdcVei.BorderStyle = BorderStyle.Inset
            Table6.Visible = False
        End If

        If codProd = "33" Then
            Label1.Text = "COBRANÇA / DIAS DE ATRASO / CONTRATOS / LEASING"
            btnLeasVei.BackColor = Drawing.Color.White
            btnCdcVei.BorderStyle = BorderStyle.Inset
            Table6.Visible = False
        End If

        If codProd = "32" Then
            Label1.Text = "COBRANÇA / DIAS DE ATRASO / CONTRATOS / OMNI"
            btnOmni.BackColor = Drawing.Color.White
            btnCdcVei.BorderStyle = BorderStyle.Inset
            Table6.Visible = False
        End If

        If codProd = "39" Then
            Label1.Text = "COBRANÇA / DIAS DE ATRASO / CONTRATOS / LEASING + CDC"
            btnLeasCdc.BackColor = Drawing.Color.White
            btnCdcVei.BorderStyle = BorderStyle.Inset
            Table6.Visible = False
        End If

        If codProd = "21" Then
            Label1.Text = "COBRANÇA / DIAS DE ATRASO / CONTRATOS / CONSIGNADO PRIVADO"
            btnConsPriv.BackColor = Drawing.Color.White
            btnCdcVei.BorderStyle = BorderStyle.Inset
            Table6.Visible = False
        End If

        If codProd = "11" Then
            Label1.Text = "COBRANÇA / DIAS DE ATRASO / CONTRATOS / CONSIGNADO PUBLICO"
            btnConsPub.BackColor = Drawing.Color.White
            btnCdcVei.BorderStyle = BorderStyle.Inset
            Table6.Visible = True
        End If

        If codProd = "22" Then
            Label1.Text = "COBRANÇA / DIAS DE ATRASO / CONTRATOS / CDC LOJAS E CP"
            btnCdcLj.BackColor = Drawing.Color.White
            btnCdcVei.BorderStyle = BorderStyle.Inset
            Table6.Visible = False
        End If

        If codProd = "41" Then
            Label1.Text = "COBRANÇA / DIAS DE ATRASO / CONTRATOS / CONSIGNADO PUBLICO AQUI"
            btnPubAqui.BackColor = Drawing.Color.White
            btnCdcVei.BorderStyle = BorderStyle.Inset
            Table6.Visible = True
        End If

    End Sub

#Region "Funções Carregar"
    Private Sub Carrega_lojas()
        If codProd = "" Then
            codProd = 31
        End If
        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim connection As New SqlConnection(strConn)
            Dim ds As New DataSet
            Dim dt As DataTable = Nothing

            Dim command As SqlCommand = New SqlCommand("SELECT LJCOD,LJDESCR FROM TLOJA WHERE LJATIVO=1 AND LJCODPRODUTO='" & Left(codProd, 1) & "' ORDER BY LJDESCR", connection)
            command.Connection.Open()

            Dim ddlValues As SqlDataReader
            ddlValues = command.ExecuteReader()

            ddlLojas.DataSource = ddlValues
            ddlLojas.DataValueField = "LJCOD"
            ddlLojas.DataTextField = "LJDESCR"
            ddlLojas.DataBind()
            ddlLojas.Items.Insert(0, New ListItem("TODOS", "0"))
            ddlLojas.SelectedIndex = 0
            ddlValues.Close()

            command.Connection.Close()
            command.Connection.Dispose()

            connection.Close()

        Catch ex As Exception
            lblMensagem.Text = "Falha Geral <SISTEMA SIV>, Entrar em contato Ramal - 5748"
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Carrega_Operador()
        If codProd = "" Then
            codProd = 31
        End If
        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim connection As New SqlConnection(strConn)
            Dim ds As New DataSet
            Dim dt As DataTable = Nothing

            Dim command As SqlCommand = New SqlCommand("SELECT OPCOD,OPDESCR FROM TOPERADOR WHERE OPATIVO=1 AND OPCODPRODUTO='" & Left(codProd, 1) & "' ORDER BY OPDESCR", connection)
            command.Connection.Open()

            Dim ddlValues As SqlDataReader
            ddlValues = command.ExecuteReader()

            ddlOper.DataSource = ddlValues
            ddlOper.DataValueField = "OPCOD"
            ddlOper.DataTextField = "OPDESCR"
            ddlOper.DataBind()
            ddlOper.Items.Insert(0, New ListItem("TODOS", "0"))
            ddlOper.SelectedIndex = 0
            ddlValues.Close()

            command.Connection.Close()
            command.Connection.Dispose()

            connection.Close()

        Catch ex As Exception
            lblMensagem.Text = "Falha Geral <SISTEMA SIV>, Entrar em contato Ramal - 5748"
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Carrega_Gerente()
        If codProd = "" Then
            codProd = 31
        End If
        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim connection As New SqlConnection(strConn)
            Dim ds As New DataSet
            Dim dt As DataTable = Nothing

            Dim command As SqlCommand = New SqlCommand("SELECT GECOD,GEDESCR FROM TGERENTE WHERE GEATIVO=1 AND GECODPRODUTO='" & Left(codProd, 1) & "' ORDER BY GEDESCR", connection)
            command.Connection.Open()

            Dim ddlValues As SqlDataReader
            ddlValues = command.ExecuteReader()

            ddlGer.DataSource = ddlValues
            ddlGer.DataValueField = "GECOD"
            ddlGer.DataTextField = "GEDESCR"
            ddlGer.DataBind()
            ddlGer.Items.Insert(0, New ListItem("TODOS", "0"))
            ddlGer.SelectedIndex = 0
            ddlValues.Close()

            command.Connection.Close()
            command.Connection.Dispose()

            connection.Close()

        Catch ex As Exception
            lblMensagem.Text = "Falha Geral <SISTEMA SIV>, Entrar em contato Ramal - 5748"
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Carrega_Regiao()
        If codProd = "" Then
            codProd = 31
        End If
        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim connection As New SqlConnection(strConn)
            Dim ds As New DataSet
            Dim dt As DataTable = Nothing

            Dim command As SqlCommand = New SqlCommand("SELECT DISTINCT FICODREGIAO,FIREGIAO FROM TFILIAL WHERE FICODPRODUTO='" & Left(codProd, 1) & "' ORDER BY FIREGIAO", connection)
            command.Connection.Open()

            Dim ddlValues As SqlDataReader
            ddlValues = command.ExecuteReader()

            ddlRegiao.DataSource = ddlValues
            ddlRegiao.DataValueField = "FIREGIAO"
            ddlRegiao.DataTextField = "FIREGIAO"
            ddlRegiao.DataBind()
            ddlRegiao.Items.Insert(0, "TODOS")

            ddlRegiao.SelectedIndex = 0
            ddlValues.Close()

            command.Connection.Close()
            command.Connection.Dispose()

            connection.Close()

        Catch ex As Exception
            lblMensagem.Text = "Falha Geral <SISTEMA SIV>, Entrar em contato Ramal - 5748"
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Carrega_Filial()
        If codProd = "" Then
            codProd = 31
        End If
        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim connection As New SqlConnection(strConn)
            Dim ds As New DataSet
            Dim dt As DataTable = Nothing

            Dim command As SqlCommand = New SqlCommand("SELECT FICOD,FIDESCR FROM TFILIAL WHERE FIATIVO=1 AND FICODPRODUTO='" & Left(codProd, 1) & "' ORDER BY FIDESCR", connection)
            command.Connection.Open()

            Dim ddlValues As SqlDataReader
            ddlValues = command.ExecuteReader()

            ddlFil.DataSource = ddlValues
            ddlFil.DataValueField = "FICOD"
            ddlFil.DataTextField = "FIDESCR"
            ddlFil.DataBind()

            ddlFil.Items.Insert(0, New ListItem("TODOS", "0"))
            ddlFil.SelectedIndex = 0

            ddlValues.Close()

            command.Connection.Close()
            command.Connection.Dispose()

            connection.Close()

        Catch ex As Exception
            lblMensagem.Text = "Falha Geral <SISTEMA SIV>, Entrar em contato Ramal - 5748"
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Carrega_Cobradoras()
        If codProd = "" Then
            codProd = 31
        End If
        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim connection As New SqlConnection(strConn)
            Dim ds As New DataSet
            Dim dt As DataTable = Nothing

            Dim command As SqlCommand = New SqlCommand("select COCOD,CODESCR from tcobradora where coAtivo=1 and coCodProduto = '" & Left(codProd, 1) & "' order by codescr", connection)
            command.Connection.Open()

            Dim ddlValues As SqlDataReader
            ddlValues = command.ExecuteReader()

            ddlCob.DataSource = ddlValues
            ddlCob.DataValueField = "COCOD"
            ddlCob.DataTextField = "CODESCR"
            ddlCob.DataBind()

            ddlCob.Items.Insert(0, New ListItem("TODOS", "0"))
            ddlCob.SelectedIndex = 0

            ddlValues.Close()

            command.Connection.Close()
            command.Connection.Dispose()

            connection.Close()

        Catch ex As Exception
            lblMensagem.Text = "Falha Geral <SISTEMA SIV>, Entrar em contato Ramal - 5748"
        Finally
            GC.Collect()
        End Try
    End Sub

    Public Sub Carrega_Empregadores()

        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim connection As New SqlConnection(strConn)
            Dim ds As New DataSet
            Dim dataTable As DataTable = Nothing
            Dim command As SqlCommand = New SqlCommand("select distinct emdescrorgao from Tempregador where  EmCodProduto=1 and emcod>10000  order by emdescrorgao", connection)
            command.Connection.Open()

            Dim ddlValues As SqlDataReader
            ddlValues = command.ExecuteReader()

            ddlEmpreg.DataSource = ddlValues
            ddlEmpreg.DataValueField = "emdescrorgao"
            ddlEmpreg.DataTextField = "emdescrorgao"
            ddlEmpreg.DataBind()
            ddlEmpreg.Items.Insert(0, New ListItem("TODOS", "TODOS"))

            command.Connection.Close()
            command.Connection.Dispose()

            connection.Close()

        Catch ex As Exception
            lblMensagem.Text = "Falha Geral <SISTEMA SIV>, Entrar em contato Ramal - 5611/5658"
        Finally
            GC.Collect()
        End Try
    End Sub
#End Region

#Region "Dropdownlists"
    Protected Sub ddlOper_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOper.SelectedIndexChanged

    End Sub

    Protected Sub ddlFil_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFil.SelectedIndexChanged

    End Sub

    Protected Sub ddlLojas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLojas.SelectedIndexChanged

    End Sub

    Protected Sub ddlRegiao_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegiao.SelectedIndexChanged

    End Sub

    Protected Sub ddlGer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlGer.SelectedIndexChanged

    End Sub

    Protected Sub ddlCob_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCob.SelectedIndexChanged

    End Sub

    Protected Sub ddlEmpreg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEmpreg.SelectedIndexChanged

    End Sub
#End Region

End Class





