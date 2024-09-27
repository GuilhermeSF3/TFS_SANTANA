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

'Namespace SANTANA.PDD_Gerencial

Public Class PDD_Gerencial
    Inherits SantanaPage


    Private _hfDataSerie1 As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
            txtData.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)
            '                txtDataSafra.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)

            'Carrega_Faixa()
            'Carrega_Grafico()

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


    'Protected Sub btnDataSafraAnterior_Click(sender As Object, e As EventArgs)

    '    Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataSafra.Text)
    '    ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddMonths(-1)

    '    If ultimoDiaMesAnterior.Year = Now.Date.Year Then
    '        If ultimoDiaMesAnterior.Month <= Now.Date.Month Then
    '            txtDataSafra.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)
    '        End If
    '    ElseIf ultimoDiaMesAnterior.Year < Now.Date.Year Then
    '        txtDataSafra.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)
    '    End If
    'End Sub


    'Protected Sub btnProximaDataSafra_Click(sender As Object, e As EventArgs)

    '    Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataSafra.Text)
    '    ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddMonths(1)

    '    If ultimoDiaMesAnterior.Year = Now.Date.Year Then
    '        If ultimoDiaMesAnterior.Month <= Now.Date.Month Then
    '            txtDataSafra.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)
    '        End If
    '    ElseIf ultimoDiaMesAnterior.Year < Now.Date.Year Then
    '        txtDataSafra.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)
    '    End If
    'End Sub


    Private Function UltimoDiaUtilMesAnterior(data As Date) As String

        Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + data.ToString("MM/yyyy"))

        ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddMonths(1)
        ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddDays(-1)

        ' após 1/8/2014 Ago usar ULTIMO DIA DO MES
        If ultimoDiaMesAnterior <= Convert.ToDateTime("01/aug/2014") Then

            ' ultimo dia util
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



    'Private Sub Carrega_Faixa()

    '    Try

    '        ddlFaixa.Items.Insert(0, New ListItem("Parcela 1", "1"))
    '        ddlFaixa.Items.Insert(1, New ListItem("Parcela 2", "2"))
    '        ddlFaixa.Items.Insert(2, New ListItem("Parcela 3", "3"))
    '        ddlFaixa.Items.Insert(3, New ListItem("Parcela 1+2+3", "4"))

    '        ddlFaixa.SelectedIndex = 0


    '    Catch ex As Exception

    '    Finally
    '        GC.Collect()
    '    End Try
    'End Sub


    'Private Sub Carrega_Grafico()

    '    Try

    '        ddlGrafico.Items.Insert(0, New ListItem("1o Momento", "1"))
    '        ddlGrafico.Items.Insert(1, New ListItem("2o Momento", "2"))
    '        ddlGrafico.Items.Insert(2, New ListItem("3o Momento", "3"))

    '        ddlGrafico.SelectedIndex = 0


    '    Catch ex As Exception

    '    Finally
    '        GC.Collect()
    '    End Try
    'End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        Try
            Dim cor As Color


            If e.Row.RowType = DataControlRowType.Header Then

                Dim oData As New CDataHora(Convert.ToDateTime(txtData.Text))

                'e.Row.Cells(14).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                'oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(14).Text = "Total"
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
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(3).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(2).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(1).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()

            ElseIf e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then

                    Dim drow As DataRow = CType(e.Row.DataItem, DataRowView).Row


                    If IsDBNull(drow("FORMATO")) Then
                        cor = e.Row.Cells(0).BackColor
                    ElseIf drow("FORMATO") = "C1" Then
                        cor = Drawing.ColorTranslator.FromOle(&HF2F2F2)
                    ElseIf drow("FORMATO") = "C2" Then
                        cor = Drawing.ColorTranslator.FromOle(&HD9D9D9)
                    ElseIf drow("FORMATO") = "C3" Then
                        cor = Drawing.ColorTranslator.FromOle(&HBFBFBF)
                    ElseIf drow("FORMATO") = "C4" Then
                        cor = Drawing.ColorTranslator.FromOle(&HA6A6A6)
                    Else
                        cor = e.Row.Cells(0).BackColor
                    End If


                    If IsDBNull(drow("Descricao")) Then
                        e.Row.Cells(0).Text = ""
                    Else
                        e.Row.Cells(0).Text = drow("Descricao")
                    End If
                    e.Row.Cells(0).BackColor = cor

                    If IsDBNull(drow("M1")) Then
                        e.Row.Cells(1).Text = ""
                    Else
                        If InStr(UCase(drow("Descricao")), "QTDE") > 0 Then
                            e.Row.Cells(1).Text = CNumero.FormataNumero(drow("M1"), 0)
                        Else
                            e.Row.Cells(1).Text = CNumero.FormataNumero(drow("M1"), 2)
                        End If
                    End If


                    If IsDBNull(drow("M2")) Then
                        e.Row.Cells(2).Text = ""
                    Else
                        If InStr(UCase(drow("Descricao")), "QTDE") > 0 Then
                            e.Row.Cells(2).Text = CNumero.FormataNumero(drow("M2"), 0)
                        Else
                            e.Row.Cells(2).Text = CNumero.FormataNumero(drow("M2"), 2)
                        End If
                    End If

                    If IsDBNull(drow("M3")) Then
                        e.Row.Cells(3).Text = ""
                    Else
                        If InStr(UCase(drow("Descricao")), "QTDE") > 0 Then
                            e.Row.Cells(3).Text = CNumero.FormataNumero(drow("M3"), 0)
                        Else
                            e.Row.Cells(3).Text = CNumero.FormataNumero(drow("M3"), 2)
                        End If
                    End If

                    If IsDBNull(drow("M4")) Then
                        e.Row.Cells(4).Text = ""
                    Else
                        If InStr(UCase(drow("Descricao")), "QTDE") > 0 Then
                            e.Row.Cells(4).Text = CNumero.FormataNumero(drow("M4"), 0)
                        Else
                            e.Row.Cells(4).Text = CNumero.FormataNumero(drow("M4"), 2)
                        End If
                    End If


                    If IsDBNull(drow("M5")) Then
                        e.Row.Cells(5).Text = ""
                    Else
                        If InStr(UCase(drow("Descricao")), "QTDE") > 0 Then
                            e.Row.Cells(5).Text = CNumero.FormataNumero(drow("M5"), 0)
                        Else
                            e.Row.Cells(5).Text = CNumero.FormataNumero(drow("M5"), 2)
                        End If
                    End If


                    If IsDBNull(drow("M6")) Then
                        e.Row.Cells(6).Text = ""
                    Else
                        If InStr(UCase(drow("Descricao")), "QTDE") > 0 Then
                            e.Row.Cells(6).Text = CNumero.FormataNumero(drow("M6"), 0)
                        Else
                            e.Row.Cells(6).Text = CNumero.FormataNumero(drow("M6"), 2)
                        End If
                    End If



                    If IsDBNull(drow("M7")) Then
                        e.Row.Cells(7).Text = ""
                    Else
                        If InStr(UCase(drow("Descricao")), "QTDE") > 0 Then
                            e.Row.Cells(7).Text = CNumero.FormataNumero(drow("M7"), 0)
                        Else
                            e.Row.Cells(7).Text = CNumero.FormataNumero(drow("M7"), 2)
                        End If
                    End If

                    If IsDBNull(drow("M8")) Then
                        e.Row.Cells(8).Text = ""
                    Else
                        If InStr(UCase(drow("Descricao")), "QTDE") > 0 Then
                            e.Row.Cells(8).Text = CNumero.FormataNumero(drow("M8"), 0)
                        Else
                            e.Row.Cells(8).Text = CNumero.FormataNumero(drow("M8"), 2)
                        End If
                    End If


                    If IsDBNull(drow("M9")) Then
                        e.Row.Cells(9).Text = ""
                    Else
                        If InStr(UCase(drow("Descricao")), "QTDE") > 0 Then
                            e.Row.Cells(9).Text = CNumero.FormataNumero(drow("M9"), 0)
                        Else
                            e.Row.Cells(9).Text = CNumero.FormataNumero(drow("M9"), 2)
                        End If
                    End If


                    If IsDBNull(drow("M10")) Then
                        e.Row.Cells(10).Text = ""
                    Else
                        If InStr(UCase(drow("Descricao")), "QTDE") > 0 Then
                            e.Row.Cells(10).Text = CNumero.FormataNumero(drow("M10"), 0)
                        Else
                            e.Row.Cells(10).Text = CNumero.FormataNumero(drow("M10"), 2)
                        End If
                    End If


                    If IsDBNull(drow("M11")) Then
                        e.Row.Cells(11).Text = ""
                    Else
                        If InStr(UCase(drow("Descricao")), "QTDE") > 0 Then
                            e.Row.Cells(11).Text = CNumero.FormataNumero(drow("M11"), 0)
                        Else
                            e.Row.Cells(11).Text = CNumero.FormataNumero(drow("M11"), 2)
                        End If
                    End If


                    If IsDBNull(drow("M12")) Then
                        e.Row.Cells(12).Text = ""
                    Else
                        If InStr(UCase(drow("Descricao")), "QTDE") > 0 Then
                            e.Row.Cells(12).Text = CNumero.FormataNumero(drow("M12"), 0)
                        Else
                            e.Row.Cells(12).Text = CNumero.FormataNumero(drow("M12"), 2)
                        End If
                    End If


                    If IsDBNull(drow("M13")) Then
                        e.Row.Cells(13).Text = ""
                    Else
                        If InStr(UCase(drow("Descricao")), "QTDE") > 0 Then
                            e.Row.Cells(13).Text = CNumero.FormataNumero(drow("M13"), 0)
                        Else
                            e.Row.Cells(13).Text = CNumero.FormataNumero(drow("M13"), 2)
                        End If
                    End If

                    If IsDBNull(drow("M14")) Then
                        e.Row.Cells(14).Text = ""
                    Else
                        If InStr(UCase(drow("Descricao")), "QTDE") > 0 Then
                            e.Row.Cells(14).Text = CNumero.FormataNumero(drow("M14"), 0)
                        Else
                            e.Row.Cells(14).Text = CNumero.FormataNumero(drow("M14"), 2)
                        End If
                    End If

                    If IsDBNull(drow("ORDEM_LINHA")) Then
                        e.Row.Cells(15).Text = ""
                    Else
                        e.Row.Cells(15).Text = drow("ORDEM_LINHA")
                    End If

 


                End If
            End If


        Catch ex As Exception

        End Try
    End Sub



    Protected Sub BindGridView1Data()

        GridView1.DataSource = GetData1()
        GridView1.DataBind()

    End Sub

 

    Protected Sub BindGridView1DataView()

        GridView1.DataSource = DataGridView1
        GridView1.DataBind()

    End Sub

 

    Private Function GetData1() As DataTable

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

        Dim oData As New CDataHora(Convert.ToDateTime(txtData.Text))
        Dim oDataSafra As New CDataHora(Convert.ToDateTime(txtData.Text))
        'Dim oDataSafra As New CDataHora(Convert.ToDateTime(txtDataSafra.Text))


        'Dim table As DataTable = ClassBD.GetExibirGrid("scr_vlr_GerencialP123 '" & oDataSafra.Data.ToString("yyyyMMdd") & "', '" & oData.Data.ToString("yyyyMMdd") & "', '" & ddlFaixa.SelectedValue & "', '" & ddlGrafico.SelectedValue & "', 2", "GraficoMensal", strConn)
        Dim table As DataTable = ClassBD.GetExibirGrid("scr_PDD_Gerencial '" & oDataSafra.Data.ToString("yyyyMMdd") & "'", "GraficoMensal", strConn)

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
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "tmp", "Alerta('Relatório de Fechamento da PDD por Safra. Gráfico com % da faixa por Total da Carteira na Safra de produção.');", True)

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
        If GridView1.Rows.Count.ToString + 1 < 65536 Then


            Dim tw As New StringWriter()
            Dim hw As New HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()

            Response.Clear()
            Response.ContentEncoding = System.Text.Encoding.Default

            Response.ContentType = "application/vnd.ms-excel"
            Dim filename As String = String.Format("P123Gerencial{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
            Response.AddHeader("content-disposition", "attachment;filename=" & filename & ".xls")


            Response.Charset = ""
            EnableViewState = False


            Controls.Add(frm)
            frm.Controls.Add(GridView1)
            frm.RenderControl(hw)


            'Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;P " & ddlFaixa.SelectedValue & " - % Valor")


            Response.Write(tw.ToString())
            Response.End()

        Else
            MsgBox(" planilha possui muitas linhas, não é possível exportar para o Excel")
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


 

    Public Property DataGridView1 As DataTable
        Get

            If ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") Is Nothing Then
                ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = GetData1()
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

    Protected Sub ddlGrafico_SelectedIndexChanged(sender As Object, e As EventArgs)

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
        GridView1.DataSource = DataGridView1()
        GridView1.PageIndex = CType(sender, DropDownList).SelectedIndex
        GridView1.DataBind()
    End Sub

    


    <WebMethod()>
    Public Shared Function BindGraphData(txtData As String) As List(Of Object)
        'Public Shared Function BindGraphData(txtDataSafra As String, txtData As String, ddlFaixa As Integer, ddlGrafico As Integer) As List(Of Object)

        Dim iData As New List(Of Object)()
        Dim labels As New List(Of String)()
        Dim legendas As New List(Of String)()

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

        Dim oData As New CDataHora()
        oData.Data = Convert.ToDateTime(txtData)

        'Dim oDataSafra As New CDataHora()
        'oDataSafra.Data = Convert.ToDateTime(txtDataSafra)

        Dim objData As DataTable = ClassBD.GetExibirGrid("scr_grafico_pddGerencial '" & oData.Data.ToString("yyyyMMdd") & "'", "Grafico", strConn)



        oData.Data = oData.Data.AddMonths(-13) 'm1
        labels.Add(oData.NomeMesSigla & "/" & oData.Data.ToString("yy"))
        oData.Data = oData.Data.AddMonths(-12) 'm1
        labels.Add(oData.NomeMesSigla & "/" & oData.Data.ToString("yy"))
        oData.Data = oData.Data.AddMonths(-11) 'm1
        labels.Add(oData.NomeMesSigla & "/" & oData.Data.ToString("yy"))
        oData.Data = oData.Data.AddMonths(-10) 'm1
        labels.Add(oData.NomeMesSigla & "/" & oData.Data.ToString("yy"))
        oData.Data = oData.Data.AddMonths(-9) 'm1
        labels.Add(oData.NomeMesSigla & "/" & oData.Data.ToString("yy"))
        oData.Data = oData.Data.AddMonths(-8) 'm1
        labels.Add(oData.NomeMesSigla & "/" & oData.Data.ToString("yy"))
        oData.Data = oData.Data.AddMonths(-7) 'm1
        labels.Add(oData.NomeMesSigla & "/" & oData.Data.ToString("yy"))
        oData.Data = oData.Data.AddMonths(-6) 'm1
        labels.Add(oData.NomeMesSigla & "/" & oData.Data.ToString("yy"))
        oData.Data = oData.Data.AddMonths(-5) 'm1
        labels.Add(oData.NomeMesSigla & "/" & oData.Data.ToString("yy"))
        oData.Data = oData.Data.AddMonths(-4) 'm1
        labels.Add(oData.NomeMesSigla & "/" & oData.Data.ToString("yy"))
        oData.Data = oData.Data.AddMonths(-3) 'm1
        labels.Add(oData.NomeMesSigla & "/" & oData.Data.ToString("yy"))
        oData.Data = oData.Data.AddMonths(-2) 'm1
        labels.Add(oData.NomeMesSigla & "/" & oData.Data.ToString("yy"))
        oData.Data = oData.Data.AddMonths(-1) 'm1
        labels.Add(oData.NomeMesSigla & "/" & oData.Data.ToString("yy"))
        oData.Data = Convert.ToDateTime(txtData)
        labels.Add(oData.NomeMesSigla & "/" & oData.Data.ToString("yy"))
        'oData.Data = Convert.ToDateTime(txtData)
        'labels.Add(oData.NomeMesSigla & "/" & oData.Data.ToString("yy"))

        iData.Add(labels)


        For Each oDataRow As DataRow In objData.Rows

            Dim dataItem As New List(Of Integer)()

            If Not IsDBNull(oDataRow("M1")) Then
                '  dataItem.Add(Replace((oDataRow("M1") / 1.0), ",", "."))
                dataItem.Add(oDataRow("M1"))
            Else
                dataItem.Add(0)
            End If
            If Not IsDBNull(oDataRow("M2")) Then
                ' dataItem.Add(Replace((oDataRow("M2") / 1.0), ",", "."))
                dataItem.Add(oDataRow("M2"))
            Else
                dataItem.Add(0)
            End If
            If Not IsDBNull(oDataRow("M3")) Then
                ' dataItem.Add(Replace((oDataRow("M3") / 1.0), ",", "."))
                dataItem.Add(oDataRow("M3"))
            Else
                dataItem.Add(0)
            End If
            If Not IsDBNull(oDataRow("M4")) Then
                ' dataItem.Add(Replace((oDataRow("M4") / 1.0), ",", "."))
                dataItem.Add(oDataRow("M4"))
            Else
                dataItem.Add(0)
            End If
            If Not IsDBNull(oDataRow("M5")) Then
                'dataItem.Add(Replace((oDataRow("M5") / 1.0), ",", "."))
                dataItem.Add(oDataRow("M5"))
            Else
                dataItem.Add(0)
            End If
            If Not IsDBNull(oDataRow("M6")) Then
                'dataItem.Add(Replace((oDataRow("M6") / 1.0), ",", "."))
                dataItem.Add(oDataRow("M6"))
            Else
                dataItem.Add(0)
            End If
            If Not IsDBNull(oDataRow("M7")) Then
                'dataItem.Add(Replace((oDataRow("M7") / 1.0), ",", "."))
                dataItem.Add(oDataRow("M7"))
            Else
                dataItem.Add(0)
            End If
            If Not IsDBNull(oDataRow("M8")) Then
                'dataItem.Add(Replace((oDataRow("M8") / 1.0), ",", "."))
                dataItem.Add(oDataRow("M8"))
            Else
                dataItem.Add(0)
            End If
            If Not IsDBNull(oDataRow("M9")) Then
                'dataItem.Add(Replace((oDataRow("M9") / 1.0), ",", "."))
                dataItem.Add(oDataRow("M9"))
            Else
                dataItem.Add(0)
            End If
            If Not IsDBNull(oDataRow("M10")) Then
                ' dataItem.Add(Replace((oDataRow("M10") / 1.0), ",", "."))
                dataItem.Add(oDataRow("M10"))
            Else
                dataItem.Add(0)
            End If
            If Not IsDBNull(oDataRow("M11")) Then
                'dataItem.Add(Replace((oDataRow("M11") / 1.0), ",", "."))
                dataItem.Add(oDataRow("M11"))
            Else
                dataItem.Add(0)
            End If
            If Not IsDBNull(oDataRow("M12")) Then
                'dataItem.Add(Replace((oDataRow("M12") / 1.0), ",", "."))
                dataItem.Add(oDataRow("M12"))
            Else
                dataItem.Add(0)
            End If
            If Not IsDBNull(oDataRow("M13")) Then
                'dataItem.Add(Replace((oDataRow("M13") / 1.0), ",", "."))
                dataItem.Add(oDataRow("M13"))
            Else
                dataItem.Add(0)
            End If
            If Not IsDBNull(oDataRow("M14")) Then
                'dataItem.Add(Replace((oDataRow("M14") / 1.0), ",", "."))
                dataItem.Add(oDataRow("M14"))
            Else
                dataItem.Add(0)
            End If

            iData.Add(oDataRow("Descricao"))
            iData.Add(dataItem)


        Next


        Return iData

    End Function


End Class




'End Namespace