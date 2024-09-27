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

Public Class RiscoCreditoIndicador
        Inherits SantanaPage

        Private _hfDataSerie1 As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
            txtData.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)

            Carrega_Produto1()
            Carrega_Produto2()
            Carrega_Filtros()
            Carrega_Indicador()

        End If

        txtData.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnCarregar.UniqueID + "').click();return false;}} else {return true}; ")

        Const script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)
    End Sub


    Protected Sub btnDataAnterior_Click(sender As Object, e As EventArgs)

            Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime(txtData.Text)
            ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddMonths(-1)

            If ultimoDiaMesAnterior.Year = Now.Date.Year Then
                If ultimoDiaMesAnterior.Month <= Now.Date.Month Then
                    txtData.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)
                End If
            ElseIf ultimoDiaMesAnterior.Year < Now.Date.Year Then
                txtData.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)
            End If
        End Sub


        Protected Sub btnProximaData_Click(sender As Object, e As EventArgs)

            Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime(txtData.Text)
            ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddMonths(1)

            If ultimoDiaMesAnterior.Year = Now.Date.Year Then
                If ultimoDiaMesAnterior.Month <= Now.Date.Month Then
                    txtData.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)
                End If
            ElseIf ultimoDiaMesAnterior.Year < Now.Date.Year Then
                txtData.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)
            End If
        End Sub


    Private Function UltimoDiaUtilMesAnterior(data As Date) As String

        Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + data.ToString("MM/yyyy"))

        ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddMonths(1)
        ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddDays(-1)

        If ultimoDiaMesAnterior <= Convert.ToDateTime("01/aug/2014") Then

            If (ultimoDiaMesAnterior.DayOfWeek = DayOfWeek.Sunday) Then
                ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddDays(-2)
            ElseIf (ultimoDiaMesAnterior.DayOfWeek = DayOfWeek.Saturday) Then
                ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddDays(-1)
            End If

            GridView1.DataSource = Nothing
            GridView1.DataBind()
        End If
        Return ultimoDiaMesAnterior.ToString("dd/MM/yyyy")
    End Function


    Private Sub Carrega_Produto1()

        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim connection As New SqlConnection(strConn)
            Dim ds As New DataSet
            Dim dt As DataTable = Nothing

            Dim command As SqlCommand = New SqlCommand(
            "SELECT DISTINCT T.COD_MODALIDADE, T.DESCR_TIPO FROM Ttipo_prod T (NOLOCK) , TModa_tipo_prod M (NOLOCK) WHERE  T.COD_PROD in ('V') AND M.COD_PROD = T.COD_PROD AND T.COD_MODALIDADE = M.COD_MODALIDADE", connection)

            command.Connection.Open()

            Dim ddlValues As SqlDataReader
            ddlValues = command.ExecuteReader()

            ddlProduto1.DataSource = ddlValues
            ddlProduto1.DataValueField = "COD_MODALIDADE"
            ddlProduto1.DataTextField = "DESCR_TIPO"
            ddlProduto1.DataBind()

            ddlProduto1.Items.Insert(0, New ListItem("TODOS", "0"))
            ddlProduto1.SelectedIndex = 0
            ddlProduto1.Items.Insert(ddlProduto1.Items.Count, New ListItem("TOTAL", "99"))

            ddlValues.Close()

            command.Connection.Close()
            command.Connection.Dispose()

            connection.Close()

        Catch ex As Exception

        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Carrega_Produto2()

        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim connection As New SqlConnection(strConn)
            Dim ds As New DataSet
            Dim dt As DataTable = Nothing

            Dim command As SqlCommand = New SqlCommand(
            "SELECT '0' AS COD_MODALIDADE, ' ' AS DESCR_TIPO UNION ALL SELECT DISTINCT T.COD_MODALIDADE, T.DESCR_TIPO FROM Ttipo_prod T (NOLOCK) , TModa_tipo_prod M (NOLOCK) WHERE  T.COD_PROD in ('V') AND M.COD_PROD = T.COD_PROD AND T.COD_MODALIDADE = M.COD_MODALIDADE", connection)

            command.Connection.Open()

            Dim ddlValues As SqlDataReader
            ddlValues = command.ExecuteReader()

            ddlProduto2.DataSource = ddlValues
            ddlProduto2.DataValueField = "COD_MODALIDADE"
            ddlProduto2.DataTextField = "DESCR_TIPO"
            ddlProduto2.DataBind()

            'ddlProduto2.Items.Insert(0, New ListItem("", "0"))
            ddlProduto2.SelectedIndex = 0

            ddlValues.Close()

            command.Connection.Close()
            command.Connection.Dispose()

            connection.Close()

        Catch ex As Exception

        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Carrega_Filtros()
        ddlFiltro.Items.Insert(0, New ListItem("Faixa de Renda", "1"))
        ddlFiltro.Items.Insert(1, New ListItem("Natureza Ocupação", "2"))
        ddlFiltro.Items.Insert(2, New ListItem("Faixa Idade", "3"))
        ddlFiltro.Items.Insert(3, New ListItem("Faixa Parcela", "4"))
        ddlFiltro.Items.Insert(4, New ListItem("Ano Veículo", "5"))
        ddlFiltro.Items.Insert(4, New ListItem("Valor Financiado", "6"))
        ddlFiltro.Items.Insert(4, New ListItem("Teto de Financiamento", "7"))
        ddlFiltro.Items.Insert(4, New ListItem("Comprometimento de Renda", "8"))
        ddlFiltro.Items.Insert(4, New ListItem("Score", "9"))
        ddlFiltro.Items.Insert(4, New ListItem("Avalista", "10"))
        ddlFiltro.Items.Insert(4, New ListItem("Exceção à Política", "11"))
    End Sub

    Private Sub Carrega_Indicador()
        ddlIndicador.Items.Insert(0, New ListItem("TODOS", "99"))
        ddlIndicador.Items.Insert(1, New ListItem("P1 – Momento 1", "1"))
        ddlIndicador.Items.Insert(2, New ListItem("P1 – Momento 2", "2"))
        ddlIndicador.Items.Insert(3, New ListItem("P1 – Momento 3", "3"))
        ddlIndicador.Items.Insert(4, New ListItem("P2 – Momento 1", "4"))
        ddlIndicador.Items.Insert(5, New ListItem("P2 – Momento 2", "5"))
        ddlIndicador.Items.Insert(6, New ListItem("P2 – Momento 3", "6"))
        ddlIndicador.Items.Insert(7, New ListItem("P3 – Momento 1", "7"))
        ddlIndicador.Items.Insert(8, New ListItem("P3 – Momento 2", "8"))
        ddlIndicador.Items.Insert(9, New ListItem("P3 – Momento 3", "9"))
        ddlIndicador.Items.Insert(10, New ListItem("P1a3 – Momento 1", "10"))
        ddlIndicador.Items.Insert(11, New ListItem("P1a3 – Momento 2", "11"))
        ddlIndicador.Items.Insert(12, New ListItem("P1a3 – Momento 3", "12"))
        ddlIndicador.Items.Insert(13, New ListItem("Perda", "13"))

        ddlIndicador.SelectedIndex = 0
    End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        Try
            Dim cor As Color


            If e.Row.RowType = DataControlRowType.Header Then

                Dim oData As New CDataHora(Convert.ToDateTime(txtData.Text))

                e.Row.Cells(17).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(16).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(15).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(14).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(13).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(12).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(11).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(10).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(9).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(8).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(7).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(6).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(5).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(4).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()

            ElseIf e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then

                    Dim drow As DataRow = CType(e.Row.DataItem, DataRowView).Row


                    'If IsDBNull(drow("FORMATO")) Then
                    '    cor = e.Row.Cells(0).BackColor
                    'ElseIf drow("FORMATO") = "C1" Then
                    '    cor = Drawing.ColorTranslator.FromOle(&HF2F2F2)
                    'ElseIf drow("FORMATO") = "C2" Then
                    '    cor = Drawing.ColorTranslator.FromOle(&HD9D9D9)
                    'ElseIf drow("FORMATO") = "C3" Then
                    '    cor = Drawing.ColorTranslator.FromOle(&HBFBFBF)
                    'ElseIf drow("FORMATO") = "C4" Then
                    '    cor = Drawing.ColorTranslator.FromOle(&HA6A6A6)
                    'Else
                    '    cor = e.Row.Cells(0).BackColor
                    'End If


                    If IsDBNull(drow("PRODUTO")) Then
                        e.Row.Cells(0).Text = ""
                    Else
                        e.Row.Cells(0).Text = drow("PRODUTO")
                    End If
                    e.Row.Cells(0).BackColor = cor

                    If IsDBNull(drow("INDICADOR")) Then
                        e.Row.Cells(1).Text = ""
                    Else
                        e.Row.Cells(1).Text = drow("INDICADOR")
                    End If
                    e.Row.Cells(1).BackColor = cor

                    If IsDBNull(drow("FILTRO")) Then
                        e.Row.Cells(2).Text = ""
                    Else
                        e.Row.Cells(2).Text = drow("FILTRO")
                    End If
                    e.Row.Cells(2).BackColor = cor

                    If IsDBNull(drow("DESCRICAO")) Then
                        e.Row.Cells(3).Text = ""
                    Else
                        e.Row.Cells(3).Text = drow("DESCRICAO")
                    End If
                    e.Row.Cells(3).BackColor = cor

                    If IsDBNull(drow("M1_VLR")) Then
                        e.Row.Cells(4).Text = ""
                    Else
                        e.Row.Cells(4).Text = If(drow("M1_VLR") = 0.0, "", CNumero.FormataNumero(drow("M1_VLR"), 2))
                    End If

                    If IsDBNull(drow("M2_VLR")) Then
                        e.Row.Cells(5).Text = ""
                    Else
                        e.Row.Cells(5).Text = If(drow("M2_VLR") = 0.0, "", CNumero.FormataNumero(drow("M2_VLR"), 2))
                    End If

                    If IsDBNull(drow("M3_VLR")) Then
                        e.Row.Cells(6).Text = ""
                    Else
                        e.Row.Cells(6).Text = If(drow("M3_VLR") = 0.0, "", CNumero.FormataNumero(drow("M3_VLR"), 2))
                    End If

                    If IsDBNull(drow("M4_VLR")) Then
                        e.Row.Cells(7).Text = ""
                    Else
                        e.Row.Cells(7).Text = If(drow("M4_VLR") = 0.0, "", CNumero.FormataNumero(drow("M4_VLR"), 2))
                    End If

                    If IsDBNull(drow("M5_VLR")) Then
                        e.Row.Cells(8).Text = ""
                    Else
                        e.Row.Cells(8).Text = If(drow("M5_VLR") = 0.0, "", CNumero.FormataNumero(drow("M5_VLR"), 2))
                    End If

                    If IsDBNull(drow("M6_VLR")) Then
                        e.Row.Cells(9).Text = ""
                    Else
                        e.Row.Cells(9).Text = If(drow("M6_VLR") = 0.0, "", CNumero.FormataNumero(drow("M6_VLR"), 2))
                    End If

                    If IsDBNull(drow("M7_VLR")) Then
                        e.Row.Cells(10).Text = ""
                    Else
                        e.Row.Cells(10).Text = If(drow("M7_VLR") = 0.0, "", CNumero.FormataNumero(drow("M7_VLR"), 2))
                    End If

                    If IsDBNull(drow("M8_VLR")) Then
                        e.Row.Cells(11).Text = ""
                    Else
                        e.Row.Cells(11).Text = If(drow("M8_VLR") = 0.0, "", CNumero.FormataNumero(drow("M8_VLR"), 2))
                    End If

                    If IsDBNull(drow("M9_VLR")) Then
                        e.Row.Cells(12).Text = ""
                    Else
                        e.Row.Cells(12).Text = If(drow("M9_VLR") = 0.0, "", CNumero.FormataNumero(drow("M9_VLR"), 2))
                    End If

                    If IsDBNull(drow("M10_VLR")) Then
                        e.Row.Cells(13).Text = ""
                    Else
                        e.Row.Cells(13).Text = If(drow("M10_VLR") = 0.0, "", CNumero.FormataNumero(drow("M10_VLR"), 2))
                    End If

                    If IsDBNull(drow("M11_VLR")) Then
                        e.Row.Cells(14).Text = ""
                    Else
                        e.Row.Cells(14).Text = If(drow("M11_VLR") = 0.0, "", CNumero.FormataNumero(drow("M11_VLR"), 2))
                    End If

                    If IsDBNull(drow("M12_VLR")) Then
                        e.Row.Cells(15).Text = ""
                    Else
                        e.Row.Cells(15).Text = If(drow("M12_VLR") = 0.0, "", CNumero.FormataNumero(drow("M12_VLR"), 2))
                    End If

                    If IsDBNull(drow("M13_VLR")) Then
                        e.Row.Cells(16).Text = ""
                    Else
                        e.Row.Cells(16).Text = If(drow("M13_VLR") = 0.0, "", CNumero.FormataNumero(drow("M13_VLR"), 2))
                    End If

                    If IsDBNull(drow("M14_VLR")) Then
                        e.Row.Cells(17).Text = ""
                    Else
                        e.Row.Cells(17).Text = If(drow("M14_VLR") = 0.0, "", CNumero.FormataNumero(drow("M14_VLR"), 2))
                    End If

                    If IsDBNull(drow("COR_LINHA")) Then
                        e.Row.Cells(18).Text = ""
                    Else
                        e.Row.Cells(18).Text = drow("COR_LINHA")
                    End If

                    If IsDBNull(drow("ORDEM")) Then
                        e.Row.Cells(19).Text = ""
                    Else
                        e.Row.Cells(19).Text = drow("ORDEM_LINHA")
                    End If

                End If
            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BindGridView1Data()

            GridView1.DataSource = GetData()
            GridView1.DataBind()

        End Sub

    Protected Sub BindGridView1DataView()

        GridView1.DataSource = DataGridView
        GridView1.DataBind()

    End Sub


    Private Function GetData() As DataTable

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

        Dim table As DataTable = ClassBD.GetExibirGrid("scr_RISCO_CREDITO_INDICADOR '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', '" & ddlProduto1.SelectedValue & "', '" & ddlProduto2.SelectedValue & "', '" & ddlFiltro.SelectedValue & "', '" & ddlIndicador.SelectedValue & "', 'ASC'", "GraficoMensal", strConn)

        Return table

    End Function

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
            If e.NewPageIndex >= 0 Then
                GridView1.PageIndex = e.NewPageIndex
                BindGridView1DataView()
            End If
        End Sub


    Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
    End Sub


    Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "tmp", "Alerta('Risco Crédito - Indicadores' ,'Risco Crédito - Indicadores", True)

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

            If Not IsNothing(GridView1.HeaderRow) Then

                Response.Clear()
                Response.Buffer = True
                Dim filename As String = String.Format("RiscoCreditoIndicadores_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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


    Protected Sub ddlFaixa_SelectedIndexChanged(sender As Object, e As EventArgs)

        GridView1.DataSource = Nothing
        GridView1.DataBind()

    End Sub

    Protected Sub ddlProduto_SelectedIndexChanged(sender As Object, e As EventArgs)

        GridView1.DataSource = Nothing
        GridView1.DataBind()

    End Sub

    Protected Sub txtData_TextChanged(sender As Object, e As EventArgs)

        GridView1.DataSource = Nothing
        GridView1.DataBind()

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