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

Public Class p123_Analista
    Inherits SantanaPage

    Private Const hfGridView1SVID As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
    Private Const hfGridView1SHID As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
            txtData.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)

            If ContextoWeb.DadosTransferencia.CodCobradora <> 0 Or ContextoWeb.DadosTransferencia.CodAgente <> 0 Then
                BindGridView1Data()
                ContextoWeb.DadosTransferencia.CodAgente = 0
                ContextoWeb.DadosTransferencia.CodCobradora = 0
            End If

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
        End If

        GridView1.DataSource = Nothing
        GridView1.DataBind()

        Return ultimoDiaMesAnterior.ToString("dd/MM/yyyy")
    End Function


    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        Try
            Dim Cor As Drawing.Color

            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then

                    Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row


                    If drow("ANALISTA") = "TOTAL" Then
                        Cor = Drawing.ColorTranslator.FromOle(&HCC0000)
                    ElseIf drow("ANALISTA") = "SAFRA" Then
                        Cor = Drawing.ColorTranslator.FromOle(&HCC)
                    Else
                        Cor = Drawing.ColorTranslator.FromOle(&H666666)
                    End If

                    If IsDBNull(drow("DT_FECHA")) Then
                        e.Row.Cells(0).Text = ""
                    Else
                        e.Row.Cells(0).Text = drow("DT_FECHA")
                        e.Row.Cells(0).ForeColor = Cor
                    End If

                    If IsDBNull(drow("ANALISTA")) Then
                        e.Row.Cells(1).Text = ""
                    Else
                        e.Row.Cells(1).Text = drow("ANALISTA")
                        e.Row.Cells(1).ForeColor = Cor
                    End If

                    If IsDBNull(drow("P1_QTD_ANA")) Then
                        e.Row.Cells(2).Text = ""
                    Else
                        e.Row.Cells(2).Text = drow("P1_QTD_ANA")
                        e.Row.Cells(2).ForeColor = Cor
                    End If

                    If IsDBNull(drow("P1QTD")) Then
                        e.Row.Cells(3).Text = ""
                    Else
                        e.Row.Cells(3).Text = drow("P1QTD")
                        e.Row.Cells(3).ForeColor = Cor
                    End If

                    If IsDBNull(drow("PCT_P1_ANA")) Then
                        e.Row.Cells(4).Text = ""
                    Else
                        e.Row.Cells(4).Text = CNumero.FormataNumero(Convert.ToDouble(drow("PCT_P1_ANA")), 2)
                        e.Row.Cells(4).ForeColor = Cor
                    End If

                    If IsDBNull(drow("P1_VLR_ANA")) Then
                        e.Row.Cells(5).Text = ""
                    Else
                        e.Row.Cells(5).Text = CNumero.FormataNumero(Convert.ToDouble(drow("P1_VLR_ANA")), 2)
                        e.Row.Cells(5).ForeColor = Cor
                    End If

                    If IsDBNull(drow("P1VLR")) Then
                        e.Row.Cells(6).Text = ""
                    Else
                        e.Row.Cells(6).Text = CNumero.FormataNumero(Convert.ToDouble(drow("P1VLR")), 2)
                        e.Row.Cells(6).ForeColor = Cor
                    End If

                    If IsDBNull(drow("PCT_P1_VLR_ANA")) Then
                        e.Row.Cells(7).Text = ""
                    Else
                        e.Row.Cells(7).Text = CNumero.FormataNumero(Convert.ToDouble(drow("PCT_P1_VLR_ANA")), 2)
                        e.Row.Cells(7).ForeColor = Cor
                    End If

                    If IsDBNull(drow("P1PCT")) Then
                        e.Row.Cells(8).Text = ""
                    Else
                        e.Row.Cells(8).Text = CNumero.FormataNumero(Convert.ToDouble(drow("P1PCT")), 2)
                        e.Row.Cells(8).ForeColor = Cor
                    End If

                    If IsDBNull(drow("P2_QTD_ANA")) Then
                        e.Row.Cells(9).Text = ""
                    Else
                        e.Row.Cells(9).Text = drow("P2_QTD_ANA")
                        e.Row.Cells(9).ForeColor = Cor
                    End If

                    If IsDBNull(drow("P2QTD")) Then
                        e.Row.Cells(10).Text = ""
                    Else
                        e.Row.Cells(10).Text = drow("P2QTD")
                        e.Row.Cells(10).ForeColor = Cor
                    End If

                    If IsDBNull(drow("PCT_P2_ANA")) Then
                        e.Row.Cells(11).Text = ""
                    Else
                        e.Row.Cells(11).Text = CNumero.FormataNumero(Convert.ToDouble(drow("PCT_P2_ANA")), 2)
                        e.Row.Cells(11).ForeColor = Cor
                    End If

                    If IsDBNull(drow("P2_VLR_ANA")) Then
                        e.Row.Cells(12).Text = ""
                    Else
                        e.Row.Cells(12).Text = CNumero.FormataNumero(Convert.ToDouble(drow("P2_VLR_ANA")), 2)
                        e.Row.Cells(12).ForeColor = Cor
                    End If

                    If IsDBNull(drow("P2VLR")) Then
                        e.Row.Cells(13).Text = ""
                    Else
                        e.Row.Cells(13).Text = CNumero.FormataNumero(Convert.ToDouble(drow("P2VLR")), 2)
                        e.Row.Cells(13).ForeColor = Cor
                    End If

                    If IsDBNull(drow("PCT_P2_VLR_ANA")) Then
                        e.Row.Cells(14).Text = ""
                    Else
                        e.Row.Cells(14).Text = CNumero.FormataNumero(Convert.ToDouble(drow("PCT_P2_VLR_ANA")), 2)
                        e.Row.Cells(14).ForeColor = Cor
                    End If

                    If IsDBNull(drow("P2PCT")) Then
                        e.Row.Cells(15).Text = ""
                    Else
                        e.Row.Cells(15).Text = CNumero.FormataNumero(Convert.ToDouble(drow("P2PCT")), 2)
                        e.Row.Cells(15).ForeColor = Cor
                    End If

                    If IsDBNull(drow("P3_QTD_ANA")) Then
                        e.Row.Cells(16).Text = ""
                    Else
                        e.Row.Cells(16).Text = drow("P3_QTD_ANA")
                        e.Row.Cells(16).ForeColor = Cor
                    End If

                    If IsDBNull(drow("P3QTD")) Then
                        e.Row.Cells(17).Text = ""
                    Else
                        e.Row.Cells(17).Text = drow("P3QTD")
                        e.Row.Cells(17).ForeColor = Cor
                    End If

                    If IsDBNull(drow("PCT_P3_ANA")) Then
                        e.Row.Cells(18).Text = ""
                    Else
                        e.Row.Cells(18).Text = CNumero.FormataNumero(Convert.ToDouble(drow("PCT_P3_ANA")), 2)
                        e.Row.Cells(18).ForeColor = Cor
                    End If

                    If IsDBNull(drow("P3_VLR_ANA")) Then
                        e.Row.Cells(19).Text = ""
                    Else
                        e.Row.Cells(19).Text = CNumero.FormataNumero(Convert.ToDouble(drow("P3_VLR_ANA")), 2)
                        e.Row.Cells(19).ForeColor = Cor
                    End If

                    If IsDBNull(drow("P3VLR")) Then
                        e.Row.Cells(20).Text = ""
                    Else
                        e.Row.Cells(20).Text = CNumero.FormataNumero(Convert.ToDouble(drow("P3VLR")), 2)
                        e.Row.Cells(20).ForeColor = Cor
                    End If

                    If IsDBNull(drow("PCT_P3_VLR_ANA")) Then
                        e.Row.Cells(21).Text = ""
                    Else
                        e.Row.Cells(21).Text = CNumero.FormataNumero(Convert.ToDouble(drow("PCT_P3_VLR_ANA")), 2)
                        e.Row.Cells(21).ForeColor = Cor
                    End If

                    If IsDBNull(drow("P3PCT")) Then
                        e.Row.Cells(22).Text = ""
                    Else
                        e.Row.Cells(22).Text = CNumero.FormataNumero(Convert.ToDouble(drow("P3PCT")), 2)
                        e.Row.Cells(22).ForeColor = Cor
                    End If


                    If IsDBNull(drow("P123QTD")) Then
                        e.Row.Cells(23).Text = ""
                    Else
                        e.Row.Cells(23).Text = drow("P123QTD")
                        e.Row.Cells(23).ForeColor = Cor
                    End If

                    If IsDBNull(drow("P123VLR")) Then
                        e.Row.Cells(24).Text = ""
                    Else
                        e.Row.Cells(24).Text = CNumero.FormataNumero(Convert.ToDouble(drow("P123VLR")), 2)
                        e.Row.Cells(24).ForeColor = Cor
                    End If

                    If IsDBNull(drow("P123PCT")) Then
                        e.Row.Cells(25).Text = ""
                    Else
                        e.Row.Cells(25).Text = CNumero.FormataNumero(Convert.ToDouble(drow("P123PCT")), 2)
                        e.Row.Cells(25).ForeColor = Cor
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

            If e.Row.RowType = DataControlRowType.Header Then
                Dim grvObjeto As GridView = DirectCast(sender, GridView)
                Dim gvrObjetoLinha As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
                Dim tclCelula As New TableCell()

                Dim oData As New CDataHora(Convert.ToDateTime(txtData.Text))
                oData.Data = oData.Data.AddMonths(-3)

                tclCelula = New TableCell()
                tclCelula.Text = "P123 3º Mom."
                tclCelula.ColumnSpan = 2
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)


                tclCelula = New TableCell()
                tclCelula.Text = "P1 - 3º Mom. - " + oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                tclCelula.ColumnSpan = 7
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                oData.Data = oData.Data.AddMonths(-1)

                tclCelula = New TableCell()
                tclCelula.Text = "P2 - 3º Mom. - " + oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                tclCelula.ColumnSpan = 7
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                oData.Data = oData.Data.AddMonths(-1)

                tclCelula = New TableCell()
                tclCelula.Text = "P3 - 3º Mom. - " + oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                tclCelula.ColumnSpan = 7
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                tclCelula = New TableCell()
                tclCelula.Text = "P1+P2+P3"
                tclCelula.ColumnSpan = 3
                tclCelula.HorizontalAlign = HorizontalAlign.Center
                gvrObjetoLinha.Cells.Add(tclCelula)
                grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

            End If

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub BindGridView1Data()

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

        GridView1.DataSource = Util.ClassBD.GetExibirGrid("[SCR_P123_ANALISTA_SINT] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "'", "p123_analista", strConn)
        GridView1.DataBind()

    End Sub


    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        BindGridView1Data()
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs)

        Dim index As Integer
        Dim Data As Date
        index = Convert.ToInt32(e.CommandArgument)
        Data = GridView1.DataKeys(index).Values(1).ToString()

        ContextoWeb.DadosTransferencia.DataReferencia = Data.ToString("dd/MM/yyyy")
        'ContextoWeb.DadosTransferencia.DataReferencia = txtData.Text
        ContextoWeb.DadosTransferencia.Classe = e.CommandName
        ContextoWeb.DadosTransferencia.Linha = GridView1.DataKeys(index).Values(0).ToString()

        ContextoWeb.Navegacao.LinkPaginaDetalhe = Me.AppRelativeVirtualPath
        Response.Redirect("DetalheContratosDiario.aspx")


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
                Dim filename As String = String.Format("P123_Analista_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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
                        row.Height = 25
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

End Class

