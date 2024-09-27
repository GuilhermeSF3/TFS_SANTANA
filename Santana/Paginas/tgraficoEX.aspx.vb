Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Web
Imports System.Web.UI
'Imports System.Web.UI.Datavisualization.charting.chart
Imports System.Windows.Forms.DataVisualization.Charting


Imports Santana.Seguranca

Public Class tgraficoEx
    Inherits SantanaPage


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Chart1.ChartAreas("ChartArea1").AxisY.CustomLabels.Add(-1, 1, "teste 1")
        'Chart1.ChartAreas("ChartArea1").AxisY.CustomLabels.Add(2, 0, "teste 2")


        '    '        Dim yValues As Double() = {txtInvAmt.Text, txtTimeAmt.Text, txtBillAmt.Text} ' Getting values from Textboxes 

        '    Dim yValues As Double() = {"5.0", "3.0", "4.0"} ' Getting values from Textboxes 
        '    Dim xValues As String() = {"Invoice Amount", "Time Amount", "Bill Amount"} ' Headings
        '    'Dim seriesName As String = Nothing

        '    '' Note 1 : Clear chart before fill - VERY IMPORTANT and can generate exception if you are generating
        '    ''          multiple charts in loop and have not included below lines !
        '    '' Note 2 : Chrt variable here is the Name of your Chart 
        '    Chart1.Series.Clear()
        '    Chart1.Titles.Clear()

        '    '' Give unique Series Name
        '    'Chart1.seriesName = "ChartInv"
        '    'Chart1.Series.Add(seriesName)

        '    '' Bind X and Y values
        '    'Chart1.Series(seriesName).Points.DataBindXY(xValues, yValues)
        '    Chart1.Series("Series1").Points.DataBindXY(xValues, yValues)

        '    '' Chart Area Modification (Optional)
        '    '' Dim CArea As ChartArea = chrt.ChartAreas(0)
        '    ''CArea.BackColor = Color.Azure
        '    ''CArea.ShadowColor = Color.Red
        '    ''CArea.Area3DStyle.Enable3D = True

        '    '' Define Custom Chart Colors
        '    ''Chart1.Series(seriesName).Points(0).Color = Color.MediumSeaGreen
        '    ''Chart1.Series(seriesName).Points(1).Color = Color.PaleGreen
        '    ''Chart1.Series(seriesName).Points(2).Color = Color.LawnGreen

        '    '' Define Chart Type
        '    'Chart1.Series(seriesName).ChartType = SeriesChartType.Pie

        '    'Chart1.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True

        '    '' Formatting the Chart Title
        '    'Dim T As Title = chrt.Titles.Add("Amount Distribution")
        '    'With T
        '    '    .ForeColor = Color.Black
        '    '    .BackColor = Color.LightBlue
        '    '    .Font = New System.Drawing.Font("Times New Roman", 11.0F, System.Drawing.FontStyle.Bold)
        '    '    .BorderColor = Color.Black
        '    'End With

        '    '' If you want to show Chart Legends
        '    'Chart1.Legends(0).Enabled = True

        '    '' If you don't want to show data values and headings as label inside each Pie in chart
        '    'Chart1.Series(seriesName)("PieLabelStyle") = "Disabled"
        '    'Chart1.Series("ChartInv").IsValueShownAsLabel = False

        '    '' If you want to show datavalues as label inside each Pie in chart
        '    'Chart1.Series("ChartInv").IsValueShownAsLabel = True




    End Sub

    'Protected Sub Exportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Exportar.Click

    'GridView1.HeaderStyle.BackColor = Drawing.Color.White

    'GridView1.AllowPaging = False

    'GridView1.ShowHeader = True

    'GridView1.DataBind()

    'ExportarExcel(GridView1, "PDD_MESANO")

    'GridView1.AllowPaging = True

    'GridView1.ShowHeader = False

    'GridView1.DataBind()
    'End Sub

    'Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    '    ' Não apagar esta Sub -> Exportação para o Excel
    'End Sub

    ' Sub ExportarExcel(ByVal dgv As GridView, ByVal saveAsFile As String)

    'If dgv.Rows.Count.ToString + 1 < 65536 Then

    '    Dim tw As New StringWriter()
    '    Dim hw As New System.Web.UI.HtmlTextWriter(tw)
    '    Dim frm As HtmlForm = New HtmlForm()

    '    Response.Clear()
    '    ' Response.ContentEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1")
    '    ' Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250")
    '    Response.ContentEncoding = System.Text.Encoding.Default

    '    Response.ContentType = "application/vnd.ms-excel"
    '    Response.AddHeader("content-disposition", "attachment;filename=" & saveAsFile & ".xls")


    '    Response.Charset = ""
    '    EnableViewState = False
    '    'EnableEventValidation = False

    '    Controls.Add(frm)
    '    frm.Controls.Add(dgv)
    '    frm.RenderControl(hw)

    '    Response.Write(tw.ToString())
    '    ' Response.Flush()
    '    Response.End()

    'Else
    '    MsgBox(" planilha possui muitas linhas, não é possível exportar para o Excel")
    'End If

    'End Sub


End Class

