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



Public Class FechaComTipoVeiculo
    Inherits SantanaPage

    Private Const hfGridView1SVID As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
    Private Const hfGridView1SHID As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"
    Private _hfDataSerie1 As String = ""
    Private _hfDataSerie2 As String = ""
    Private _hfDataSerie3 As String = ""
    Private _hfDataSerie4 As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
            txtData.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)

            Carrega_Relatorio()


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

        End If

        GridView1.DataSource = Nothing
        GridView1.DataBind()

        Return ultimoDiaMesAnterior.ToString("dd/MM/yyyy")
    End Function


    Private Sub Carrega_Relatorio()

        Try
            ddlRelatorio.Items.Insert(0, New ListItem("Quantidade", "1"))
            ddlRelatorio.Items.Insert(1, New ListItem("Valor", "2"))

            ddlRelatorio.SelectedIndex = 0

        Catch ex As Exception

        Finally
            GC.Collect()
        End Try
    End Sub


    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        Try



            If e.Row.RowType = DataControlRowType.Header Then
                Dim cor As Color
                Dim cor2 As Color

                Dim oData As New CDataHora(Convert.ToDateTime(txtData.Text))
                Dim col As Integer
                Dim currentYear As Integer


                cor = Drawing.ColorTranslator.FromOle(&HFBD7D7)
                cor2 = Drawing.ColorTranslator.FromOle(&HD3D3D3)

                oData = New CDataHora(Convert.ToDateTime(txtData.Text))
                currentYear = oData.Data.Year

                col = 25
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                e.Row.Cells(col).BackColor = cor

                col -= 2
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 2
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 2
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 2
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 2
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 2
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 2
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 2
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 2
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 2
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 2
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

                col -= 2
                oData.Data = oData.Data.AddMonths(-1)
                e.Row.Cells(col).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                If currentYear = oData.Data.Year Then
                    e.Row.Cells(col).BackColor = cor
                Else
                    e.Row.Cells(col).BackColor = cor2
                End If

            ElseIf e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then
                    Dim cor As Color
                    Dim cor2 As Color

                    Dim drow As DataRow = CType(e.Row.DataItem, DataRowView).Row
                    Dim col As Integer


                    If IsDBNull(drow("COR_LINHA")) Then
                        cor = e.Row.Cells(0).BackColor
                        cor2 = cor
                    ElseIf drow("COR_LINHA") = "A1" Then
                        cor = Drawing.ColorTranslator.FromOle(&HDAEBFF)
                    ElseIf drow("COR_LINHA") = "A2" Then
                        cor = Drawing.ColorTranslator.FromOle(&HB7F1B7)
                    ElseIf drow("COR_LINHA") = "A3" Then
                        cor = Drawing.ColorTranslator.FromOle(&HF6E5D6)
                    ElseIf drow("COR_LINHA") = "A4" Then
                        cor = Drawing.ColorTranslator.FromOle(&HF9E4F9)
                    ElseIf drow("COR_LINHA") = "A5" Then
                        cor2 = Drawing.ColorTranslator.FromOle(&HECDFE4)
                    Else
                        cor = e.Row.Cells(0).BackColor
                        cor2 = cor
                    End If



                    col = 0
                    If IsDBNull(drow("DESCR")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("DESCR")
                    End If
                    e.Row.Cells(col).BackColor = cor

                    col += 1
                    If IsDBNull(drow("m13")) Or drow("m13") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m13"), 0)
                    End If
                    e.Row.Cells(col).Text = drow("m13")
                    e.Row.Cells(col).BackColor = cor2

                    col += 1
                    If IsDBNull(drow("p13")) Or drow("p13") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("p13"), 2)
                    End If
                    e.Row.Cells(col).BackColor = cor

                    col += 1
                    If IsDBNull(drow("m12")) Or drow("m12") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m12"), 0)
                    End If
                    e.Row.Cells(col).BackColor = cor2

                    col += 1
                    If IsDBNull(drow("p12")) Or drow("p12") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("p12"), 2)
                    End If
                    e.Row.Cells(col).BackColor = cor

5:
                    col += 1
                    If IsDBNull(drow("m11")) Or drow("m11") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m11"), 0)
                    End If
                    e.Row.Cells(col).BackColor = cor2

                    col += 1
                    If IsDBNull(drow("p11")) Or drow("p11") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("p11"), 2)
                    End If
                    e.Row.Cells(col).BackColor = cor

                    col += 1
                    If IsDBNull(drow("m10")) Or drow("m10") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m10"), 0)
                    End If
                    e.Row.Cells(col).BackColor = cor2

                    col += 1
                    If IsDBNull(drow("p10")) Or drow("p10") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("p10"), 2)
                    End If
                    e.Row.Cells(col).BackColor = cor

                    col += 1
                    If IsDBNull(drow("m9")) Or drow("m9") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m9"), 0)
                    End If
                    e.Row.Cells(col).BackColor = cor2

10:
                    col += 1
                    If IsDBNull(drow("p9")) Or drow("p9") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("p9"), 2)
                    End If
                    e.Row.Cells(col).BackColor = cor

                    col += 1
                    If IsDBNull(drow("m8")) Or drow("m8") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m8"), 0)
                    End If
                    e.Row.Cells(col).BackColor = cor2

                    col += 1
                    If IsDBNull(drow("p8")) Or drow("p8") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("p8"), 2)
                    End If
                    e.Row.Cells(col).BackColor = cor

                    col += 1
                    If IsDBNull(drow("m7")) Or drow("m7") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m7"), 0)
                    End If
                    e.Row.Cells(col).BackColor = cor2

                    col += 1
                    If IsDBNull(drow("p7")) Or drow("p7") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("p7"), 2)
                    End If
                    e.Row.Cells(col).BackColor = cor

15:
                    col += 1
                    If IsDBNull(drow("m6")) Or drow("m6") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m6"), 0)
                    End If
                    e.Row.Cells(col).BackColor = cor2

                    col += 1
                    If IsDBNull(drow("p6")) Or drow("p6") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("p6"), 2)
                    End If
                    e.Row.Cells(col).BackColor = cor

                    col += 1
                    If IsDBNull(drow("m5")) Or drow("m5") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m5"), 0)
                    End If
                    e.Row.Cells(col).BackColor = cor2

                    col += 1
                    If IsDBNull(drow("p5")) Or drow("p5") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("p5"), 2)
                    End If
                    e.Row.Cells(col).BackColor = cor

                    col += 1
                    If IsDBNull(drow("m4")) Or drow("m4") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m4"), 0)
                    End If
                    e.Row.Cells(col).BackColor = cor2

20:
                    col += 1
                    If IsDBNull(drow("p4")) Or drow("p4") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("p4"), 2)
                    End If
                    e.Row.Cells(col).BackColor = cor

                    col += 1
                    If IsDBNull(drow("m3")) Or drow("m3") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m3"), 0)
                    End If
                    e.Row.Cells(col).BackColor = cor2

                    col += 1
                    If IsDBNull(drow("p3")) Or drow("p3") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("p3"), 2)
                    End If
                    e.Row.Cells(col).BackColor = cor

                    col += 1
                    If IsDBNull(drow("m2")) Or drow("m2") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m2"), 0)
                    End If
                    e.Row.Cells(col).BackColor = cor2

                    col += 1
                    If IsDBNull(drow("p2")) Or drow("p2") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("p2"), 2)
                    End If
                    e.Row.Cells(col).BackColor = cor

25:
                    col += 1
                    If IsDBNull(drow("m1")) Or drow("m1") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m1"), 0)
                    End If
                    e.Row.Cells(col).BackColor = cor2

                    col += 1
                    If IsDBNull(drow("p1")) Or drow("p1") = 0 Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("p1"), 2)
                    End If
                    e.Row.Cells(col).BackColor = cor

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Property DataGridView1 As DataTable
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
        Dim table As DataTable = Util.ClassBD.GetExibirGrid("[SCR_COMERCIAL_TIPO] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', 'V', '" & ddlRelatorio.SelectedValue & "'", "FechaComPropostasOp", strConn)

        Return table

    End Function


    Protected Sub BindGraphData1(ByVal procedure As String, ByRef dataserie As String)

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim table As DataTable = Util.ClassBD.GetExibirGrid(procedure & " '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', 'V', '" & ddlRelatorio.SelectedValue & "'", "FechaComPropostasOp", strConn)


        For Each oDataRow As DataRow In table.Rows

            If procedure = "[SCR_COMERCIAL_TIPO_GRAF1]" Then
                dataserie = dataserie & "<vc:DataSeries Color=""#FFFFEBDA"" LegendText=""" & oDataRow("DESCR") & """ RenderAs=""Line"" MarkerType=""Circle"" SelectionEnabled=""True"" LineThickness=""3"">"
            ElseIf procedure = "[SCR_COMERCIAL_TIPO_GRAF2]" Then
                dataserie = dataserie & "<vc:DataSeries Color=""#FFB7F1B7"" LegendText=""" & oDataRow("DESCR") & """ RenderAs=""Line"" MarkerType=""Circle"" SelectionEnabled=""True"" LineThickness=""3"">"
            ElseIf procedure = "[SCR_COMERCIAL_TIPO_GRAF3]" Then
                dataserie = dataserie & "<vc:DataSeries Color=""#FFD6E5F6"" LegendText=""" & oDataRow("DESCR") & """ RenderAs=""Line"" MarkerType=""Circle"" SelectionEnabled=""True"" LineThickness=""3"">"
            ElseIf procedure = "[SCR_COMERCIAL_TIPO_GRAF4]" Then
                dataserie = dataserie & "<vc:DataSeries Color=""#FFF9E4F9"" LegendText=""" & oDataRow("DESCR") & """ RenderAs=""Line"" MarkerType=""Circle"" SelectionEnabled=""True"" LineThickness=""3"">"
            End If

            dataserie = dataserie & "<vc:DataSeries.DataPoints> "

            Dim oData As New CDataHora()

            If Not IsDBNull(oDataRow("P13")) Then
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-12)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & Replace((oDataRow("P13") / 1.0), ",", ".") & """/> "
            Else
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-12)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & """/> "
            End If


            If Not IsDBNull(oDataRow("P12")) Then
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-11)
                dataserie = dataserie & "<vc:DataPoint  LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & Replace((oDataRow("P12") / 1.0), ",", ".") & """/> "
            Else
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-11)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & """/> "
            End If
            If Not IsDBNull(oDataRow("P11")) Then
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-10)
                dataserie = dataserie & "<vc:DataPoint  LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & Replace((oDataRow("P11") / 1.0), ",", ".") & """/> "
            Else
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-10)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & """/> "
            End If
            If Not IsDBNull(oDataRow("P10")) Then
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-9)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & Replace((oDataRow("P10") / 1.0), ",", ".") & """/> "
            Else
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-9)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & """/> "
            End If
            If Not IsDBNull(oDataRow("P9")) Then
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-8)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & Replace((oDataRow("P9") / 1.0), ",", ".") & """/> "
            Else
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-8)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & """/> "
            End If
            If Not IsDBNull(oDataRow("P8")) Then
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-7)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & Replace((oDataRow("P8") / 1.0), ",", ".") & """/> "
            Else
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-7)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & """/> "
            End If
            If Not IsDBNull(oDataRow("P7")) Then
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-6)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & Replace((oDataRow("P7") / 1.0), ",", ".") & """/> "
            Else
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-6)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & """/> "
            End If
            If Not IsDBNull(oDataRow("P6")) Then
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-5)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & Replace((oDataRow("P6") / 1.0), ",", ".") & """/> "
            Else
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-5)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & """/> "
            End If

            If Not IsDBNull(oDataRow("P5")) Then
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-4)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & Replace((oDataRow("P5") / 1.0), ",", ".") & """/> "
            Else
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-4)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & """/> "
            End If

            If Not IsDBNull(oDataRow("P4")) Then
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-3)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & Replace((oDataRow("P4") / 1.0), ",", ".") & """/> "
            Else
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-3)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & """/> "
            End If

            If Not IsDBNull(oDataRow("P3")) Then
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-2)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & Replace((oDataRow("P3") / 1.0), ",", ".") & """/> "
            Else
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-2)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & """/> "
            End If

            If Not IsDBNull(oDataRow("P2")) Then
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-1)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & Replace((oDataRow("P2") / 1.0), ",", ".") & """/> "
            Else
                oData.Data = Convert.ToDateTime(txtData.Text)
                oData.Data = oData.Data.AddMonths(-1)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & """/> "
            End If

            If Not IsDBNull(oDataRow("P1")) Then
                oData.Data = Convert.ToDateTime(txtData.Text)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & Replace((oDataRow("P1") / 1.0), ",", ".") & """/> "
            Else
                oData.Data = Convert.ToDateTime(txtData.Text)
                dataserie = dataserie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""" & oData.NomeMesSigla & "/" & oData.Data.ToString("yy") & """ YValue=""" & """/> "
            End If


            dataserie = dataserie & "</vc:DataSeries.DataPoints> "
            dataserie = dataserie & "</vc:DataSeries> "

        Next




    End Sub

    Protected Sub ddlRelatorio_SelectedIndexChanged(sender As Object, e As EventArgs)

        GridView1.DataSource = Nothing
        GridView1.DataBind()

    End Sub

    Protected Sub txtData_TextChanged(sender As Object, e As EventArgs)

        GridView1.DataSource = Nothing
        GridView1.DataBind()

    End Sub

    Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
    End Sub


    Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
    End Sub


    Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
        Response.Redirect("../Menu.aspx")
    End Sub


    Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
        Try

            BindGraphData1("[SCR_COMERCIAL_TIPO_GRAF1]", _hfDataSerie1)
            BindGraphData1("[SCR_COMERCIAL_TIPO_GRAF2]", _hfDataSerie2)
            BindGraphData1("[SCR_COMERCIAL_TIPO_GRAF3]", _hfDataSerie3)
            BindGraphData1("[SCR_COMERCIAL_TIPO_GRAF4]", _hfDataSerie4)
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
                Dim filename As String = String.Format("FechaComTipoVeiculo_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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

                    Select Case ddlRelatorio.SelectedValue
                        Case "1"
                            Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "Financiamento por tipo de Veículo - Evolução (por Quantidade)")
                        Case "2"
                            Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "Financiamento por tipo de Veículo - Evolução (por Valor)")
                    End Select

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




    Public Function GetXaml(ByVal title As String, ByVal DataSerie As String) As [String]
        Dim myXaml As String


        myXaml = "<vc:Chart  xmlns:vc=""clr-namespace:Visifire.Charts;assembly=SLVisifire.Charts"" AnimatedUpdate=""True"" Width=""800"" Height=""400"" Theme=""Theme1"" BorderBrush=""Gray"" IndicatorEnabled=""True""   ToolBarEnabled=""True""  >"
        myXaml = myXaml & "<vc:Chart.Titles>"

        myXaml = myXaml & "<vc:Title Text=""" & title & """/> "

        myXaml = myXaml & "</vc:Chart.Titles>"

        myXaml = myXaml & "<vc:Chart.AxesX> "
        myXaml = myXaml & "<vc:Axis Padding=""2""/> "
        myXaml = myXaml & "</vc:Chart.AxesX> "

        myXaml = myXaml & "<vc:Chart.AxesY> "
        myXaml = myXaml & "<vc:Axis Title=""""/> "

        myXaml = myXaml & "</vc:Chart.AxesY> "
        myXaml = myXaml & "<vc:Chart.Series> "

        myXaml = myXaml & DataSerie

        myXaml = myXaml & "</vc:Chart.Series> "
        myXaml = myXaml & "</vc:Chart>"


        Return myXaml
    End Function





    Private Sub Page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete

        Dim s As String = ""

        Dim xaml As [String] = GetXaml("Passeio", _hfDataSerie1)
        If _hfDataSerie1 <> "" Then
            s = "vChart1.setDataXml('" & xaml & "');" & "vChart1.render(""VisifireChart1"");"
        End If


        xaml = GetXaml("Motos", _hfDataSerie2)
        If _hfDataSerie2 <> "" Then
            s = s + "vChart2.setDataXml('" & xaml & "');" & "vChart2.render(""VisifireChart2""); "
        End If

        xaml = GetXaml("Utilitário", _hfDataSerie3)
        If _hfDataSerie2 <> "" Then
            s = s + "vChart3.setDataXml('" & xaml & "');" & "vChart3.render(""VisifireChart3""); "
        End If

        xaml = GetXaml("Refinanciamento", _hfDataSerie4)
        If _hfDataSerie2 <> "" Then
            s = s + "vChart4.setDataXml('" & xaml & "');" & "vChart4.render(""VisifireChart4""); "
        End If

        If s <> "" Then
            ScriptManager.RegisterClientScriptBlock(TryCast(sender, Page), [GetType](), "onClick", "<script language='JavaScript'> " & s & " </script>", False)
        End If
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        If e.NewPageIndex >= 0 Then
            GridView1.PageIndex = e.NewPageIndex
            BindGridView1DataView()
            BindGraphData1("[SCR_COMERCIAL_TIPO_GRAF1]", _hfDataSerie1)
            BindGraphData1("[SCR_COMERCIAL_TIPO_GRAF2]", _hfDataSerie2)
            BindGraphData1("[SCR_COMERCIAL_TIPO_GRAF3]", _hfDataSerie3)
            BindGraphData1("[SCR_COMERCIAL_TIPO_GRAF4]", _hfDataSerie4)
        End If
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
    Public Shared Function BindGraphData1(ddlRelatorio As Integer, txtData As String) As List(Of Object)

        Dim iData As New List(Of Object)()
        Dim labels As New List(Of String)()
        Dim legendas As New List(Of String)()

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim strProcedure As String = "scr_grafico_estoque 15, "



        Dim objData As DataTable = ClassBD.GetExibirGrid("SCR_COMERCIAL_TIPO_GRAF1" & " '" & Right(txtData, 4) & Mid(txtData, 4, 2) & Left(txtData, 2) & "', 'V','" & ddlRelatorio & "'", "GraficoMensal", strConn)

        Dim oData As New CDataHora(Convert.ToDateTime(txtData))

        oData.Data = oData.Data.AddYears(-1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())

        iData.Add(labels)


        For Each oDataRow As DataRow In objData.Rows

            Dim dataItem As New List(Of Double)()

            If Not IsDBNull(oDataRow("P13")) Then
                dataItem.Add(oDataRow("P13"))
            End If
            If Not IsDBNull(oDataRow("P12")) Then
                dataItem.Add(oDataRow("P12"))
            End If
            If Not IsDBNull(oDataRow("P11")) Then
                dataItem.Add(oDataRow("P11"))
            End If
            If Not IsDBNull(oDataRow("P10")) Then
                dataItem.Add(oDataRow("P10"))
            End If
            If Not IsDBNull(oDataRow("P9")) Then
                dataItem.Add(oDataRow("P9"))
            End If
            If Not IsDBNull(oDataRow("P8")) Then
                dataItem.Add(oDataRow("P8"))
            End If
            If Not IsDBNull(oDataRow("P7")) Then
                dataItem.Add(oDataRow("P7"))
            End If
            If Not IsDBNull(oDataRow("P6")) Then
                dataItem.Add(oDataRow("P6"))
            End If
            If Not IsDBNull(oDataRow("P5")) Then
                dataItem.Add(oDataRow("P5"))
            End If
            If Not IsDBNull(oDataRow("P4")) Then
                dataItem.Add(oDataRow("P4"))
            End If
            If Not IsDBNull(oDataRow("P3")) Then
                dataItem.Add(oDataRow("P3"))
            End If
            If Not IsDBNull(oDataRow("P2")) Then
                dataItem.Add(oDataRow("P2"))
            End If
            If Not IsDBNull(oDataRow("P1")) Then
                dataItem.Add(oDataRow("P1"))
            End If
            iData.Add(oDataRow("Descr"))
            iData.Add(dataItem)

        Next

        Return iData

    End Function

    <WebMethod()>
    Public Shared Function BindGraphData2(ddlRelatorio As Integer, txtData As String) As List(Of Object)

        Dim iData As New List(Of Object)()
        Dim labels As New List(Of String)()
        Dim legendas As New List(Of String)()

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim strProcedure As String = "scr_grafico_estoque 15, "



        Dim objData As DataTable = ClassBD.GetExibirGrid("SCR_COMERCIAL_TIPO_GRAF2" & " '" & Right(txtData, 4) & Mid(txtData, 4, 2) & Left(txtData, 2) & "', 'V','" & ddlRelatorio & "'", "GraficoMensal", strConn)

        Dim oData As New CDataHora(Convert.ToDateTime(txtData))

        oData.Data = oData.Data.AddYears(-1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())

        iData.Add(labels)


        For Each oDataRow As DataRow In objData.Rows

            Dim dataItem As New List(Of Double)()

            If Not IsDBNull(oDataRow("P13")) Then
                dataItem.Add(oDataRow("P13"))
            End If
            If Not IsDBNull(oDataRow("P12")) Then
                dataItem.Add(oDataRow("P12"))
            End If
            If Not IsDBNull(oDataRow("P11")) Then
                dataItem.Add(oDataRow("P11"))
            End If
            If Not IsDBNull(oDataRow("P10")) Then
                dataItem.Add(oDataRow("P10"))
            End If
            If Not IsDBNull(oDataRow("P9")) Then
                dataItem.Add(oDataRow("P9"))
            End If
            If Not IsDBNull(oDataRow("P8")) Then
                dataItem.Add(oDataRow("P8"))
            End If
            If Not IsDBNull(oDataRow("P7")) Then
                dataItem.Add(oDataRow("P7"))
            End If
            If Not IsDBNull(oDataRow("P6")) Then
                dataItem.Add(oDataRow("P6"))
            End If
            If Not IsDBNull(oDataRow("P5")) Then
                dataItem.Add(oDataRow("P5"))
            End If
            If Not IsDBNull(oDataRow("P4")) Then
                dataItem.Add(oDataRow("P4"))
            End If
            If Not IsDBNull(oDataRow("P3")) Then
                dataItem.Add(oDataRow("P3"))
            End If
            If Not IsDBNull(oDataRow("P2")) Then
                dataItem.Add(oDataRow("P2"))
            End If
            If Not IsDBNull(oDataRow("P1")) Then
                dataItem.Add(oDataRow("P1"))
            End If
            iData.Add(oDataRow("Descr"))
            iData.Add(dataItem)

        Next

        Return iData

    End Function

    <WebMethod()>
    Public Shared Function BindGraphData3(ddlRelatorio As Integer, txtData As String) As List(Of Object)

        Dim iData As New List(Of Object)()
        Dim labels As New List(Of String)()
        Dim legendas As New List(Of String)()

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim strProcedure As String = "scr_grafico_estoque 15, "



        Dim objData As DataTable = ClassBD.GetExibirGrid("SCR_COMERCIAL_TIPO_GRAF3" & " '" & Right(txtData, 4) & Mid(txtData, 4, 2) & Left(txtData, 2) & "', 'V','" & ddlRelatorio & "'", "GraficoMensal", strConn)

        Dim oData As New CDataHora(Convert.ToDateTime(txtData))

        oData.Data = oData.Data.AddYears(-1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())

        iData.Add(labels)


        For Each oDataRow As DataRow In objData.Rows

            Dim dataItem As New List(Of Double)()

            If Not IsDBNull(oDataRow("P13")) Then
                dataItem.Add(oDataRow("P13"))
            End If
            If Not IsDBNull(oDataRow("P12")) Then
                dataItem.Add(oDataRow("P12"))
            End If
            If Not IsDBNull(oDataRow("P11")) Then
                dataItem.Add(oDataRow("P11"))
            End If
            If Not IsDBNull(oDataRow("P10")) Then
                dataItem.Add(oDataRow("P10"))
            End If
            If Not IsDBNull(oDataRow("P9")) Then
                dataItem.Add(oDataRow("P9"))
            End If
            If Not IsDBNull(oDataRow("P8")) Then
                dataItem.Add(oDataRow("P8"))
            End If
            If Not IsDBNull(oDataRow("P7")) Then
                dataItem.Add(oDataRow("P7"))
            End If
            If Not IsDBNull(oDataRow("P6")) Then
                dataItem.Add(oDataRow("P6"))
            End If
            If Not IsDBNull(oDataRow("P5")) Then
                dataItem.Add(oDataRow("P5"))
            End If
            If Not IsDBNull(oDataRow("P4")) Then
                dataItem.Add(oDataRow("P4"))
            End If
            If Not IsDBNull(oDataRow("P3")) Then
                dataItem.Add(oDataRow("P3"))
            End If
            If Not IsDBNull(oDataRow("P2")) Then
                dataItem.Add(oDataRow("P2"))
            End If
            If Not IsDBNull(oDataRow("P1")) Then
                dataItem.Add(oDataRow("P1"))
            End If
            iData.Add(oDataRow("Descr"))
            iData.Add(dataItem)

        Next

        Return iData

    End Function


    <WebMethod()>
    Public Shared Function BindGraphData4(ddlRelatorio As Integer, txtData As String) As List(Of Object)

        Dim iData As New List(Of Object)()
        Dim labels As New List(Of String)()
        Dim legendas As New List(Of String)()

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim strProcedure As String = "scr_grafico_estoque 15, "



        Dim objData As DataTable = ClassBD.GetExibirGrid("SCR_COMERCIAL_TIPO_GRAF4" & " '" & Right(txtData, 4) & Mid(txtData, 4, 2) & Left(txtData, 2) & "', 'V','" & ddlRelatorio & "'", "GraficoMensal", strConn)

        Dim oData As New CDataHora(Convert.ToDateTime(txtData))

        oData.Data = oData.Data.AddYears(-1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())
        oData.Data = oData.Data.AddMonths(+1)
        labels.Add(oData.NomeMesSigla + "/" + oData.Data.Year.ToString())

        iData.Add(labels)


        For Each oDataRow As DataRow In objData.Rows

            Dim dataItem As New List(Of Double)()

            If Not IsDBNull(oDataRow("P13")) Then
                dataItem.Add(oDataRow("P13"))
            End If
            If Not IsDBNull(oDataRow("P12")) Then
                dataItem.Add(oDataRow("P12"))
            End If
            If Not IsDBNull(oDataRow("P11")) Then
                dataItem.Add(oDataRow("P11"))
            End If
            If Not IsDBNull(oDataRow("P10")) Then
                dataItem.Add(oDataRow("P10"))
            End If
            If Not IsDBNull(oDataRow("P9")) Then
                dataItem.Add(oDataRow("P9"))
            End If
            If Not IsDBNull(oDataRow("P8")) Then
                dataItem.Add(oDataRow("P8"))
            End If
            If Not IsDBNull(oDataRow("P7")) Then
                dataItem.Add(oDataRow("P7"))
            End If
            If Not IsDBNull(oDataRow("P6")) Then
                dataItem.Add(oDataRow("P6"))
            End If
            If Not IsDBNull(oDataRow("P5")) Then
                dataItem.Add(oDataRow("P5"))
            End If
            If Not IsDBNull(oDataRow("P4")) Then
                dataItem.Add(oDataRow("P4"))
            End If
            If Not IsDBNull(oDataRow("P3")) Then
                dataItem.Add(oDataRow("P3"))
            End If
            If Not IsDBNull(oDataRow("P2")) Then
                dataItem.Add(oDataRow("P2"))
            End If
            If Not IsDBNull(oDataRow("P1")) Then
                dataItem.Add(oDataRow("P1"))
            End If
            iData.Add(oDataRow("Descr"))
            iData.Add(dataItem)

        Next

        Return iData

    End Function

End Class
