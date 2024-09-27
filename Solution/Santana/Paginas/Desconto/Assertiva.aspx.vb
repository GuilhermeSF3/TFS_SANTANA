Imports System
Imports System.Configuration
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic
Imports Santana.Seguranca
Imports Util

Namespace Paginas.Desconto

    Public Class Assertiva

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            BindGridView1Data()

            If Not IsPostBack Then

                If ContextoWeb.DadosTransferencia.Relatorio <> 0 Then
                    BindGridView1Data()
                    ContextoWeb.DadosTransferencia.Relatorio = 0
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

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)


        End Sub

        Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

            Try

                If e.Row.RowType = DataControlRowType.DataRow Then
                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As DataRow = CType(e.Row.DataItem, DataRowView).Row
                        Dim col As Integer

                        col = 0
                        If IsDBNull(drow("NOME_FANTASIA")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("NOME_FANTASIA")
                        End If

                        col += 1
                        If IsDBNull(drow("RAZAO_SOCIAL")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("RAZAO_SOCIAL")
                        End If

                        col += 1
                        If IsDBNull(drow("CNPJ_CPF")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("CNPJ_CPF")
                        End If

                        col += 1
                        If IsDBNull(drow("CONTATO")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("CONTATO")
                        End If

                        col += 1
                        If IsDBNull(drow("DDD_TELEFONE")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("DDD_TELEFONE")
                        End If

                        col += 1
                        If IsDBNull(drow("TELEFONE_CLIENTE")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("TELEFONE_CLIENTE")
                        End If

                        col += 1
                        If IsDBNull(drow("DDD_CELULAR")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("DDD_CELULAR")
                        End If

                        col += 1
                        If IsDBNull(drow("CELULAR_CLIENTE")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("CELULAR_CLIENTE")
                        End If

                        col += 1
                        If IsDBNull(drow("EMAIL")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("EMAIL")
                        End If

                        col += 1
                        If IsDBNull(drow("ENDERECO")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("ENDERECO")
                        End If

                        col += 1
                        If IsDBNull(drow("NUMERO_END")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("NUMERO_END")
                        End If

                        col += 1
                        If IsDBNull(drow("BAIRRO")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("BAIRRO")
                        End If

                        col += 1
                        If IsDBNull(drow("CIDADE")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("CIDADE")
                        End If

                        col += 1
                        If IsDBNull(drow("UF")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("UF")
                        End If

                        col += 1
                        If IsDBNull(drow("CEP")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("CEP")
                        End If

                        col += 1
                        If IsDBNull(drow("CNPJ_CONTRATANTE")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("CNPJ_CONTRATANTE")
                        End If

                        col += 1
                        If IsDBNull(drow("CNPJ_CPF_SAC")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("CNPJ_CPF_SAC")
                        End If

                        col += 1
                        If IsDBNull(drow("TITULO")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("TITULO")
                        End If

                        col += 1
                        If IsDBNull(drow("DT_FATURAMENTO")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("DT_FATURAMENTO")
                        End If

                        col += 1
                        If IsDBNull(drow("DT_EMISSAO")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("DT_EMISSAO")
                        End If

                        col += 1
                        If IsDBNull(drow("DT_VCTO")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("DT_VCTO")
                        End If

                        col += 1
                        If IsDBNull(drow("VLR_NOMINAL")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("VLR_NOMINAL"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("SALDO_DEVEDOR")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("SALDO_DEVEDOR"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("TP_DOCUMENTO")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("TP_DOCUMENTO")
                        End If

                        col += 1
                        If IsDBNull(drow("COD_BANCO")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("COD_BANCO")
                        End If

                        col += 1
                        If IsDBNull(drow("NN")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("NN")
                        End If

                        col += 1
                        If IsDBNull(drow("CNPJ_CEDENTE")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("CNPJ_CEDENTE")
                        End If

                        col += 1
                        If IsDBNull(drow("TP_PESSOA")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("TP_PESSOA")
                        End If

                        col += 1
                        If IsDBNull(drow("CNPJ_CEDENTE")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("CNPJ_CEDENTE")
                        End If

                        col += 1
                        If IsDBNull(drow("AGENCIA")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("AGENCIA")
                        End If

                        col += 1
                        If IsDBNull(drow("DIG_AGENCIA")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("DIG_AGENCIA")
                        End If

                        col += 1
                        If IsDBNull(drow("CONTA")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("CONTA")
                        End If

                        col += 1
                        If IsDBNull(drow("DIG_CONTA")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("DIG_CONTA")
                        End If

                        col += 1
                        If IsDBNull(drow("CARTEIRA")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("CARTEIRA")
                        End If

                        col += 1
                        If IsDBNull(drow("VAR_CARTEIRA")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("VAR_CARTEIRA")
                        End If

                        col += 1
                        If IsDBNull(drow("N_CONVENIO")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("N_CONVENIO")
                        End If

                        col += 1
                        If IsDBNull(drow("MODALIDADE")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("MODALIDADE")
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
            Dim table As DataTable = Nothing

            table = ClassBD.GetExibirGrid("[SCR_NETFACTOR_ASSERTIVA]", "VencidosPJ", strConn)

            Return table

        End Function

        Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

            If e.NewPageIndex >= 0 Then
                GridView1.PageIndex = e.NewPageIndex
                BindGridView1DataView()
            End If

        End Sub

        Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Assertiva' ,'Contratos Vencidos no Período');", True)
        End Sub

        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../Menu.aspx")
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

                If Not IsNothing(GridView1.HeaderRow) Then
                    GridView1.AllowPaging = False
                    BindGridView1Data()
                    ExportExcel(GridView1)
                End If


            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        Private Sub ExportExcel(objGrid As GridView)


            Try

                If Not IsNothing(objGrid.HeaderRow) Then

                    Response.Clear()
                    Response.Buffer = True
                    Dim filename As String = String.Format("Assertiva{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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

                        'Response.Write("Vencidos - PJ - DE: " & txtDataDe.Data & " ATÉ: " & txtDataAte.Data)

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

        'Protected Sub btnCSV_Click(sender As Object, e As EventArgs)

        '    Try
        '        GridView1.AllowPaging = False
        '        BindGridView1Data()
        '        ExportCSV(GridView1)

        '    Catch ex As Exception
        '        Throw ex
        '    End Try

        'End Sub

        'Private Sub ExportCSV(objGrid As GridView)

        '    Try
        '        HttpContext.Current.Response.Clear()
        '        HttpContext.Current.Response.Buffer = True

        '        Dim filename As String = String.Format("ASSERTIVA{0}_{1}_{2}.csv", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())

        '        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + filename)
        '        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default
        '        HttpContext.Current.Response.Charset = ""
        '        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"

        '        Dim sb As New StringBuilder()

        '        'For k As Integer = 0 To objGrid.Columns.Count - 1
        '        '    If k < objGrid.Columns.Count - 1 Then
        '        '        sb.Append("""" + objGrid.Columns(k).HeaderText.Trim + """" + ","c)
        '        '    Else
        '        '        sb.Append("""" + objGrid.Columns(k).HeaderText.Trim + """")
        '        '    End If
        '        'Next
        '        'sb.Append(vbCr & vbLf)

        '        For i As Integer = 0 To objGrid.Rows.Count - 1
        '            For k As Integer = 0 To objGrid.Columns.Count - 1
        '                If k < objGrid.Columns.Count Then
        '                    sb.Append("" + objGrid.Rows(i).Cells(k).Text.Trim + "" + ";"c)
        '                Else
        '                    sb.Append("" + objGrid.Rows(i).Cells(k).Text.Trim + "")
        '                End If
        '            Next
        '            sb.Append(vbCr & vbLf)
        '        Next

        '        Response.Output.Write(sb.ToString())
        '        HttpContext.Current.Response.Flush()
        '        HttpContext.Current.Response.SuppressContent = True
        '        HttpContext.Current.ApplicationInstance.CompleteRequest()

        '    Catch ex As Exception
        '        Throw ex
        '    End Try

        'End Sub

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

    End Class
End Namespace