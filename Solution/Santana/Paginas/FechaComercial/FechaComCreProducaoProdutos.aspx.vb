Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Web.Services
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic
Imports Santana.Seguranca
Imports Util

Public Class FechaComCreProducaoProdutos
    Inherits SantanaPage

    Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
    Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"
    Private _codCarteira As String = ""
    Private _codProduto As String = ""
    Private _hfDataSerie1 As String = ""
    Dim prod As String = ""
    Dim prod_sel As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
            txtData.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)

            If ContextoWeb.DadosTransferencia.Produto = 0 Then
                Carrega_Produto()
            Else
                Carrega_Produto()
                prod = ContextoWeb.DadosTransferencia.Produto
                Select Case prod
                    Case "1"
                        prod_sel = "D"
                    Case "2"
                        prod_sel = "V"
                End Select
                ddlProduto.SelectedIndex = ddlProduto.Items.IndexOf(ddlProduto.Items.FindByText(prod_sel))
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

        Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
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

    Private Function UltimoDiaUtilMesAnterior(Data As Date) As String

        Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Data.ToString("MM/yyyy"))

        ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddMonths(1)
        ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddDays(-1)

        If ultimoDiaMesAnterior <= Convert.ToDateTime("01/aug/2014") Then

            If (ultimoDiaMesAnterior.DayOfWeek = DayOfWeek.Sunday) Then
                ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddDays(-2)
            ElseIf (ultimoDiaMesAnterior.DayOfWeek = DayOfWeek.Saturday) Then
                ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddDays(-1)
            End If
        End If

        Return ultimoDiaMesAnterior.ToString("dd/MM/yyyy")

    End Function



    Private Sub Carrega_Produto()


        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim connection As New SqlConnection(strConn)
            Dim ds As New DataSet
            Dim dt As DataTable = Nothing

            Dim Produto As Integer = ContextoWeb.UsuarioLogado.Produto

            Dim command As SqlCommand = New SqlCommand(
            "select * from Tproduto (NOLOCK)", connection)

            command.Connection.Open()

            Dim ddlValues As SqlDataReader
            ddlValues = command.ExecuteReader()

            ddlProduto.DataSource = ddlValues
            ddlProduto.DataValueField = "cod_prod"
            ddlProduto.DataTextField = "descr_prod"
            ddlProduto.DataBind()

            ddlProduto.Items.Insert(0, New ListItem("Selecione Produto", "0"))
            ddlProduto.SelectedIndex = 0

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


            If e.Row.RowType = DataControlRowType.Header Then

                Dim oData As New CDataHora(Convert.ToDateTime(txtData.Text))

                Dim cor As Color
                Dim cor2 As Color
                Dim col As Integer
                Dim currentYear As Integer

                cor = ColorTranslator.FromOle(&HFBD7D7)
                cor2 = ColorTranslator.FromOle(&HD3D3D3)

                currentYear = oData.Data.Year

                col = 18
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                e.Row.Cells(col).BackColor = cor

                col -= 1
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 1
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 1
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 1
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 1
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 1
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 1
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 1
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 1
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 1
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 1
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 1
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If


            End If

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim cor As Color

                If Not e.Row.DataItem Is Nothing Then

                    Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

                    If IsDBNull(drow("COR_LINHA")) Then
                        cor = e.Row.Cells(0).BackColor
                    ElseIf drow("COR_LINHA") = "AZ2" Then
                        cor = Drawing.ColorTranslator.FromOle(&HEBCE87)
                    ElseIf drow("COR_LINHA") = "AM2" Then
                        cor = Drawing.ColorTranslator.FromOle(&HD7FF)
                    ElseIf drow("COR_LINHA") = "VD1" Then
                        cor = Drawing.ColorTranslator.FromOle(&H98FB98)
                    ElseIf drow("COR_LINHA") = "CZ1" Then
                        cor = Drawing.ColorTranslator.FromOle(&HDCDCDC)
                    ElseIf drow("COR_LINHA") = "AM1" Then
                        cor = Drawing.ColorTranslator.FromOle(&HE0FFFF)
                    ElseIf drow("COR_LINHA") = "LR" Then
                        cor = Drawing.ColorTranslator.FromOle(&HA5FF)
                    ElseIf drow("COR_LINHA") = "VM1" Then
                        cor = Drawing.ColorTranslator.FromOle(&HFF)
                    ElseIf drow("COR_LINHA") = "VD2" Then
                        cor = Drawing.ColorTranslator.FromOle(&H6400)
                    Else
                        cor = e.Row.Cells(0).BackColor
                    End If





                    If IsDBNull(drow("DT_FECHA")) Then
                        e.Row.Cells(0).Text = ""
                    Else
                        e.Row.Cells(0).Text = drow("DT_FECHA")
                    End If
                    e.Row.Cells(0).BackColor = cor

                    If IsDBNull(drow("PRODUTO")) Then
                        e.Row.Cells(1).Text = ""
                    Else
                        e.Row.Cells(1).Text = drow("PRODUTO")
                    End If
                    e.Row.Cells(1).BackColor = cor

                    If IsDBNull(drow("COD_AGENTE")) Then
                        e.Row.Cells(2).Text = ""
                    Else
                        e.Row.Cells(2).Text = drow("COD_AGENTE")
                    End If
                    e.Row.Cells(2).BackColor = cor

                    If IsDBNull(drow("AGENTE")) Then
                        e.Row.Cells(3).Text = ""
                    Else
                        e.Row.Cells(3).Text = drow("AGENTE")
                    End If
                    e.Row.Cells(3).BackColor = cor

                    If IsDBNull(drow("COD_OPERADOR")) Then
                        e.Row.Cells(4).Text = ""
                    Else
                        e.Row.Cells(4).Text = drow("COD_OPERADOR")
                    End If
                    e.Row.Cells(4).BackColor = cor

                    If IsDBNull(drow("OPERADOR")) Then
                        e.Row.Cells(5).Text = ""
                    Else
                        e.Row.Cells(5).Text = drow("OPERADOR")
                    End If
                    e.Row.Cells(5).BackColor = cor






                    If drow("m13") = 0.0 Then
                        e.Row.Cells(6).Text = ""
                    Else
                        e.Row.Cells(6).Text = CNumero.FormataNumero(drow("m13"), 0)
                    End If
                    e.Row.Cells(6).BackColor = cor


                    If drow("m12") = 0.0 Then
                        e.Row.Cells(7).Text = ""
                    Else
                        e.Row.Cells(7).Text = CNumero.FormataNumero(drow("m12"), 0)
                    End If
                    e.Row.Cells(7).BackColor = cor


                    If drow("m11") = 0.0 Then
                        e.Row.Cells(8).Text = ""
                    Else
                        e.Row.Cells(8).Text = CNumero.FormataNumero(drow("m11"), 0)
                    End If
                    e.Row.Cells(8).BackColor = cor


                    If drow("m10") = 0.0 Then
                        e.Row.Cells(9).Text = ""
                    Else
                        e.Row.Cells(9).Text = CNumero.FormataNumero(drow("m10"), 0)
                    End If
                    e.Row.Cells(9).BackColor = cor


                    If drow("m9") = 0 Then
                        e.Row.Cells(10).Text = ""
                    Else
                        e.Row.Cells(10).Text = CNumero.FormataNumero(drow("m9"), 0)
                    End If
                    e.Row.Cells(10).BackColor = cor


                    If drow("m8") = 0.0 Then
                        e.Row.Cells(11).Text = ""
                    Else
                        e.Row.Cells(11).Text = CNumero.FormataNumero(drow("m8"), 0)
                    End If
                    e.Row.Cells(11).BackColor = cor


                    If drow("m7") = 0.0 Then
                        e.Row.Cells(12).Text = ""
                    Else
                        e.Row.Cells(12).Text = CNumero.FormataNumero(drow("m7"), 0)
                    End If
                    e.Row.Cells(12).BackColor = cor


                    If drow("m6") = 0.0 Then
                        e.Row.Cells(13).Text = ""
                    Else
                        e.Row.Cells(13).Text = CNumero.FormataNumero(drow("m6"), 0)
                    End If
                    e.Row.Cells(13).BackColor = cor


                    If drow("m5") = 0.0 Then
                        e.Row.Cells(14).Text = ""
                    Else
                        e.Row.Cells(14).Text = CNumero.FormataNumero(drow("m5"), 0)
                    End If
                    e.Row.Cells(14).BackColor = cor


                    If drow("m4") = 0.0 Then
                        e.Row.Cells(15).Text = ""
                    Else
                        e.Row.Cells(15).Text = CNumero.FormataNumero(drow("m4"), 0)
                    End If
                    e.Row.Cells(15).BackColor = cor


                    If drow("m3") = 0.0 Then
                        e.Row.Cells(16).Text = ""
                    Else
                        e.Row.Cells(16).Text = CNumero.FormataNumero(drow("m3"), 0)
                    End If
                    e.Row.Cells(16).BackColor = cor


                    If drow("m2") = 0.0 Then
                        e.Row.Cells(17).Text = ""
                    Else
                        e.Row.Cells(17).Text = CNumero.FormataNumero(drow("m2"), 0)
                    End If
                    e.Row.Cells(17).BackColor = cor


                    If drow("m1") = 0.0 Then
                        e.Row.Cells(18).Text = ""
                    Else
                        e.Row.Cells(18).Text = CNumero.FormataNumero(drow("m1"), 0)
                    End If
                    e.Row.Cells(18).BackColor = cor

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


    Private Function GetData() As DataTable

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim table1 As DataTable

        table1 = ClassBD.GetExibirGrid("[scr_COMERCIAL_PRODUCAO2] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "','" & ddlProduto.SelectedValue & "'", "FechaComCreProducaoProdutos", strConn)

        Return table1
    End Function

    Protected Sub BindGridView1DataView()

        GridView1.DataSource = DataGridView
        GridView1.DataBind()

    End Sub


    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

        If e.NewPageIndex >= 0 Then
            GridView1.PageIndex = e.NewPageIndex
            BindGridView1DataView()
            BindGraphData(txtData.Text, ddlProduto.SelectedValue)

        End If
    End Sub

    Protected Sub ddlProduto_SelectedIndexChanged(sender As Object, e As EventArgs)

        GridView1.DataSource = Nothing
        GridView1.DataBind()

    End Sub
    Protected Sub txtData_TextChanged(sender As Object, e As EventArgs)

        GridView1.DataSource = Nothing
        GridView1.DataBind()

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
                Dim filename As String = String.Format("FechaComCreProducaoProdutos{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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


                    Select Case ddlProduto.SelectedValue
                        Case "T"
                            Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "Análise Produção Mensal vs Produtos" & " - TOTAL ")
                        Case "D"
                            Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "Análise Produção Mensal vs Produtos" & " - Desconto ")
                        Case "CG"
                            Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "Análise Produção Mensal vs Produtos" & " - Capital Giro ")
                        Case "V"
                            Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "Análise Produção Mensal vs Produtos" & " - Veiculo ")
                        Case "R"
                            Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "Análise Produção Mensal vs Produtos" & " - Renegociacao ")
                        Case "CP"
                            Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "Análise Produção Mensal vs Produtos" & " - Credito Pessoal ")
                    End Select


                    Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.Flush()
                    HttpContext.Current.Response.SuppressContent = True
                    HttpContext.Current.ApplicationInstance.CompleteRequest()
                End Using

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

    <WebMethod()>
    Public Shared Function BindGraphData(txtData As String, ddlProduto As String) As List(Of Object)


        Dim iData As New List(Of Object)()
        Dim labels As New List(Of String)()
        Dim legendas As New List(Of String)()

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

        Dim oData As New CDataHora()
        oData.Data = Convert.ToDateTime(txtData)

        Dim objData As DataTable = ClassBD.GetExibirGrid("[scr_COMERCIAL_GRAFICO_PROD2] '" & Right(txtData, 4) & Mid(txtData, 4, 2) & Left(txtData, 2) & "','" & ddlProduto & "'", "FechaComCreProducaoProdutos_Graf", strConn)

        labels.Add("Jan")
        labels.Add("Fev")
        labels.Add("Mar")
        labels.Add("Abr")
        labels.Add("Mai")
        labels.Add("Jun")
        labels.Add("Jul")
        labels.Add("Ago")
        labels.Add("Set")
        labels.Add("Out")
        labels.Add("Nov")
        labels.Add("Dez")

        iData.Add(labels)

        Dim ano As String = Right(txtData.ToString(), 4)
        For Each oDataRow As DataRow In objData.Rows

            Dim dataItem As New List(Of Double)()

            If Not IsDBNull(oDataRow("M12")) Then
                dataItem.Add(oDataRow("M12"))
            End If
            If Not IsDBNull(oDataRow("M11")) Then
                dataItem.Add(oDataRow("M11"))
            End If
            If Not IsDBNull(oDataRow("M10")) Then
                dataItem.Add(oDataRow("M10"))
            End If
            If Not IsDBNull(oDataRow("M9")) Then
                dataItem.Add(oDataRow("M9"))
            End If
            If Not IsDBNull(oDataRow("M8")) Then
                dataItem.Add(oDataRow("M8"))
            End If
            If Not IsDBNull(oDataRow("M7")) Then
                dataItem.Add(oDataRow("M7"))
            End If
            If Not IsDBNull(oDataRow("M6")) Then
                dataItem.Add(oDataRow("M6"))
            End If
            If Not IsDBNull(oDataRow("M5")) Then
                dataItem.Add(oDataRow("M5"))
            End If
            If Not IsDBNull(oDataRow("M4")) Then
                dataItem.Add(oDataRow("M4"))
            End If
            If Not IsDBNull(oDataRow("M3")) Then
                dataItem.Add(oDataRow("M3"))
            End If
            If Not IsDBNull(oDataRow("M2")) Then
                dataItem.Add(oDataRow("M2"))
            End If
            If Not IsDBNull(oDataRow("M1")) Then
                dataItem.Add(oDataRow("M1"))
            End If
            iData.Add(ano)
            iData.Add(dataItem)
            ano = ano - 1

        Next

        Return iData

    End Function

End Class