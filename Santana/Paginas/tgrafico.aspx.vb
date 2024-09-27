Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Windows.Forms.DataVisualization.Charting


Imports Santana.Seguranca



Public Class tgrafico
    Inherits SantanaPage 'System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sUsuario As String = Session.Item("acesso")
        Dim sPerfil As Integer = Session.Item("perfil")
        'If sUsuario = Nothing Then
        '    Response.Redirect("Logon.aspx", True)
        'End If

        'If sPerfil <= 1 Then
        '    Response.Redirect("~/Menu.aspx", True)
        'End If

        'cria o arquivo XML

        Dim chartTitle As String = "Infant Mortality Rate"          ' Main title for chart

        Dim chartSubTitle = "(Global Survey)"                       ' Sub title for chart

        Dim axisXtitle = "Year"                                     ' X axis title

        Dim axisYtitle = "IMR Rate"                                 ' Y axis title

        Dim myXAML As String                                        ' String for Data xaml

        Dim numberOfDataPoints As Integer = 6                       ' Number of data point in the DataSeries



        ' A DataSeries is a two dimensional array of DataPoints

        '                              Year | IMR Rate

        Dim dataSeries(,) As String = {{"2000", "52.6"}, {"2001", "40.3"}, {"2002", "20"}, {"2003", "28.7"}, {"2004", "46.1"}, {"2005", "15.1"}}


        ' Constructing Data XAML 

        myXAML = "<vc:Chart Theme=""Theme2"" Width=""600"" Height=""400"" xmlns:vc=""clr-namespace:Visifire.Charts;assembly=Visifire.Charts"">" + vbNewLine + vbNewLine

        myXAML = myXAML + "<vc:Title Text=""" & chartTitle & """/>" + vbNewLine

        myXAML = myXAML + "<vc:Title Text=""" & chartSubTitle & """/>" + vbNewLine

        myXAML = myXAML + "<vc:AxisX Title=""" & axisXtitle & """/>" + vbNewLine

        myXAML = myXAML + "<vc:AxisY Title=""" & axisYtitle & """ ValueFormatString=""#0.##'%'""/>" + vbNewLine + vbNewLine

        myXAML = myXAML + "<vc:DataSeries RenderAs=""Column"">" + vbNewLine



        'Constructing XAML fragment for DataSeries 

        For dataPointIndex As Integer = 0 To numberOfDataPoints - 1



            ' Adding DataPoint XAML fragment

            myXAML = myXAML + "<vc:DataPoint AxisLabel=""" & dataSeries(dataPointIndex, 0) & """ YValue=""" & dataSeries(dataPointIndex, 1) & """/>" + vbNewLine

        Next

        myXAML = myXAML + vbNewLine + "</vc:DataSeries>" + vbNewLine

        myXAML = myXAML + "</vc:Chart>"



        ' Clear all response text

        Response.Clear()

        ' Write data xaml as response text

        Response.Write(myXAML)
        'Response.Write("Data.xml")

        '        End If


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
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Não apagar esta Sub -> Exportação para o Excel
    End Sub
    Sub ExportarExcel(ByVal dgv As GridView, ByVal saveAsFile As String)

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

    End Sub


End Class

