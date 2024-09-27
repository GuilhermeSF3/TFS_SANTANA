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

Public Class FechaComCreOpRealizadas
    Inherits SantanaPage

    Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
    Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
            txtData.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)

            If Session(hfGridView1SVID) IsNot Nothing Then
                hfGridView1SV.Value = DirectCast(Session(hfGridView1SVID), String)
                Session.Remove(hfGridView1SVID)
            End If

            If Session(hfGridView1SHID) IsNot Nothing Then
                hfGridView1SH.Value = DirectCast(Session(hfGridView1SHID), String)
                Session.Remove(hfGridView1SHID)
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





    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        Try
            Dim cor As Drawing.Color

            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then

                    Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

                    If IsDBNull(drow("COR_LINHA")) Then
                        cor = e.Row.Cells(0).BackColor
                    ElseIf drow("COR_LINHA") = "LR1" Then
                        cor = Drawing.ColorTranslator.FromOle(&HD7FF)
                    ElseIf drow("COR_LINHA") = "AZ1" Then
                        cor = Drawing.ColorTranslator.FromOle(&HEBCE85)
                    ElseIf drow("COR_LINHA") = "AZ2" Then
                        cor = Drawing.ColorTranslator.FromOle(&HEBCE87)
                    ElseIf drow("COR_LINHA") = "AM2" Then
                        cor = Drawing.ColorTranslator.FromOle(&HD7FF)
                    ElseIf drow("COR_LINHA") = "VD1" Then
                        cor = Drawing.ColorTranslator.FromOle(&H98FB98)
                    ElseIf drow("COR_LINHA") = "CZ1" Then
                        cor = Drawing.ColorTranslator.FromOle(&HDCDCDC)
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

                    If IsDBNull(drow("DESCR")) Then
                        e.Row.Cells(2).Text = ""
                    Else
                        e.Row.Cells(2).Text = drow("DESCR")
                    End If
                    e.Row.Cells(2).BackColor = cor

                    If IsDBNull(drow("COD")) Then
                        e.Row.Cells(3).Text = ""
                    Else
                        e.Row.Cells(3).Text = drow("COD")
                    End If
                    e.Row.Cells(3).BackColor = cor

                    If IsDBNull(drow("QTDE")) Or drow("QTDE") = 0 Then
                        e.Row.Cells(4).Text = ""
                    Else
                        e.Row.Cells(4).Text = drow("QTDE")
                    End If
                    e.Row.Cells(4).BackColor = cor

                    If IsDBNull(drow("PRAZO")) Or drow("PRAZO") = 0 Then
                        e.Row.Cells(5).Text = ""
                    Else
                        e.Row.Cells(5).Text = drow("PRAZO")
                    End If
                    e.Row.Cells(5).BackColor = cor

                    If IsDBNull(drow("PLANO")) Or drow("PLANO") = 0 Then
                        e.Row.Cells(6).Text = ""
                    Else
                        e.Row.Cells(6).Text = drow("PLANO")
                    End If
                    e.Row.Cells(6).BackColor = cor

                    If IsDBNull(drow("TX_AA")) Or drow("TX_AA") = 0 Then
                        e.Row.Cells(7).Text = ""
                    Else
                        e.Row.Cells(7).Text = CNumero.FormataNumero(drow("TX_AA"), 2)
                    End If
                    e.Row.Cells(7).BackColor = cor

                    If IsDBNull(drow("TX_AM")) Or drow("TX_AM") = 0 Then
                        e.Row.Cells(8).Text = ""
                    Else
                        e.Row.Cells(8).Text = CNumero.FormataNumero(drow("TX_AM"), 2)
                    End If
                    e.Row.Cells(8).BackColor = cor

                    If IsDBNull(drow("TX_CET")) Or drow("TX_CET") = 0 Then
                        e.Row.Cells(9).Text = ""
                    Else
                        e.Row.Cells(9).Text = CNumero.FormataNumero(drow("TX_CET"), 2)
                    End If
                    e.Row.Cells(9).BackColor = cor

                    If IsDBNull(drow("PV")) Or drow("PV") = 0 Then
                        e.Row.Cells(10).Text = ""
                    Else
                        e.Row.Cells(10).Text = CNumero.FormataNumero(drow("PV"), 0)
                    End If
                    e.Row.Cells(10).BackColor = cor

                    If IsDBNull(drow("RECEITA")) Or drow("RECEITA") = 0 Then
                        e.Row.Cells(11).Text = ""
                    Else
                        e.Row.Cells(11).Text = CNumero.FormataNumero(drow("RECEITA"), 0)
                    End If
                    e.Row.Cells(11).BackColor = cor

                    If IsDBNull(drow("FV")) Or drow("FV") = 0 Then
                        e.Row.Cells(12).Text = ""
                    Else
                        e.Row.Cells(12).Text = CNumero.FormataNumero(drow("FV"), 0)
                    End If
                    e.Row.Cells(12).BackColor = cor

                    If IsDBNull(drow("LIBERADO")) Or drow("LIBERADO") = 0 Then
                        e.Row.Cells(13).Text = ""
                    Else
                        e.Row.Cells(13).Text = CNumero.FormataNumero(drow("LIBERADO"), 0)
                    End If
                    e.Row.Cells(13).BackColor = cor

                    If IsDBNull(drow("TC_TARIFA")) Or drow("TC_TARIFA") = 0 Then
                        e.Row.Cells(14).Text = ""
                    Else
                        e.Row.Cells(14).Text = CNumero.FormataNumero(drow("TC_TARIFA"), 0)
                    End If
                    e.Row.Cells(14).BackColor = cor

                    If IsDBNull(drow("IOF")) Or drow("IOF") = 0 Then
                        e.Row.Cells(15).Text = ""
                    Else
                        e.Row.Cells(15).Text = CNumero.FormataNumero(drow("IOF"), 0)
                    End If
                    e.Row.Cells(15).BackColor = cor

                    If IsDBNull(drow("VLR_TC")) Or drow("VLR_TC") = 0 Then
                        e.Row.Cells(16).Text = ""
                    Else
                        e.Row.Cells(16).Text = CNumero.FormataNumero(drow("VLR_TC"), 0)
                    End If
                    e.Row.Cells(16).BackColor = cor

                    If IsDBNull(drow("VLR_TARIFA_SEGPRESTAMISTA_OPE")) Or drow("VLR_TARIFA_SEGPRESTAMISTA_OPE") = 0 Then
                        e.Row.Cells(17).Text = ""
                    Else
                        e.Row.Cells(17).Text = CNumero.FormataNumero(drow("VLR_TARIFA_SEGPRESTAMISTA_OPE"), 0)
                    End If
                    e.Row.Cells(17).BackColor = cor

                    If IsDBNull(drow("VLR_TARIFA_ACESSORIOS_OPE")) Or drow("VLR_TARIFA_ACESSORIOS_OPE") = 0 Then
                        e.Row.Cells(18).Text = ""
                    Else
                        e.Row.Cells(18).Text = CNumero.FormataNumero(drow("VLR_TARIFA_ACESSORIOS_OPE"), 0)
                    End If
                    e.Row.Cells(18).BackColor = cor

                    If IsDBNull(drow("VLT_TARIFA_CONTDETRAN_OPE")) Or drow("VLT_TARIFA_CONTDETRAN_OPE") = 0 Then
                        e.Row.Cells(19).Text = ""
                    Else
                        e.Row.Cells(19).Text = CNumero.FormataNumero(drow("VLT_TARIFA_CONTDETRAN_OPE"), 0)
                    End If
                    e.Row.Cells(19).BackColor = cor

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
                tclCelula.Text = "."
                tclCelula.ColumnSpan = 3
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                tclCelula = New TableCell()
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                tclCelula = New TableCell()
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                tclCelula = New TableCell()
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                tclCelula = New TableCell()
                tclCelula.Text = "TX aa"
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                tclCelula = New TableCell()
                tclCelula.Text = "TX am"
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                tclCelula = New TableCell()
                tclCelula.Text = "TX Cet am"
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                tclCelula = New TableCell()
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                tclCelula = New TableCell()
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                tclCelula = New TableCell()
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                tclCelula = New TableCell()
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                tclCelula = New TableCell()
                tclCelula.Text = "TC + "
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                tclCelula = New TableCell()
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                tclCelula = New TableCell()
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                tclCelula = New TableCell()
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                tclCelula = New TableCell()
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                tclCelula = New TableCell()
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                tclCelula = New TableCell()
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

        ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = Nothing
        GridView1.DataSource = GetData()
        GridView1.PageIndex = 0
        GridView1.DataBind()

    End Sub


    Private Function GetData() As DataTable

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim table As DataTable = Util.ClassBD.GetExibirGrid("[scr_COMERCIAL_OPERACAO] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "'", "FechaComCreOpRealizadas", strConn)

        Return table

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
                Dim filename As String = String.Format("FechaComCreOpRealizadas_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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

                    Response.Write("Operações Realizadas por Produto - Ref " & txtData.Text)

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

