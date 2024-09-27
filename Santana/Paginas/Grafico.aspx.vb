Public Class Grafico
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    ''' <summary>
    ''' drpTheme_SelectedIndexChanged() function to select Theme from DropDownList
    ''' </summary> 
    Protected Sub drpTheme_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim xaml As [String] = GetXAML()
        Dim s As String = "vChart.setDataXml('" & xaml & "');" & "vChart.render(""VisifireChart1"");"
        Dim scriptManager__1 As ScriptManager = ScriptManager.GetCurrent(Page)
        ScriptManager.RegisterClientScriptBlock(TryCast(sender, DropDownList), Me.[GetType](), "onClick", "<script language='JavaScript'> " & s & " </script>", False)

    End Sub

    ''' <summary>
    ''' RadioButton1_CheckedChanged() function to change chart type
    ''' </summary>  
    Protected Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs)
        Dim xaml As [String] = GetXAML()
        Dim s As String = "vChart.setDataXml('" & xaml & "');" & "vChart.render(""VisifireChart1"");"
        Dim scriptManager__1 As ScriptManager = ScriptManager.GetCurrent(Page)
        ScriptManager.RegisterClientScriptBlock(TryCast(sender, RadioButton), Me.[GetType](), "onClick", "<script language='JavaScript'> " & s & " </script>", False)
    End Sub

    ''' <summary>
    ''' RadioButton2_CheckedChanged() function to change chart type 
    ''' </summary> 
    Protected Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs)

        Dim xaml As [String] = GetXAML()
        Dim s As String = "vChart.setDataXml('" & xaml & "');" & "vChart.render(""VisifireChart1"");"
        Dim scriptManager__1 As ScriptManager = ScriptManager.GetCurrent(Page)
        ScriptManager.RegisterClientScriptBlock(TryCast(sender, RadioButton), Me.[GetType](), "onClick", "<script language='JavaScript'> " & s & " </script>", False)
    End Sub

    ''' <summary>
    ''' chkInappropriate_CheckedChanged() function to get Inappropriate Mails
    ''' </summary>
    Protected Sub chkInappropriate_CheckedChanged(sender As Object, e As EventArgs)
        Dim xaml As [String] = GetXAML()
        Dim s As String = "vChart.setDataXml('" & xaml & "');" & "vChart.render(""VisifireChart1"");"
        Dim scriptManager__1 As ScriptManager = ScriptManager.GetCurrent(Page)
        ScriptManager.RegisterClientScriptBlock(TryCast(sender, CheckBox), Me.[GetType](), "onClick", "<script language='JavaScript'> " & s & " </script>", False)
    End Sub



    ''' <summary>
    ''' Creating Chart XAML
    ''' </summary>
    ''' <returns>returns Data xaml</returns>
    Public Function GetXAML() As [String]
        Dim chartTitle As String = "IceCom Statistical Analysis"
        ' Main title for chart
        Dim axisXtitle As String = "Month"
        ' X axis title
        Dim axisYtitle As String = "Volume Of Emails"
        ' Y axis title
        Dim myXAML As String
        ' String for Data xaml
        Dim numberOfDataPoints As Integer = 12
        'Number of data point
        'in the DataSeries    
        Dim inappropriateMails As [Boolean] = chkInappropriate.Checked
        Dim chartTheme As [String] = drpTheme.SelectedItem.Text
        Dim renderAs As [String] = "Column"
        ' Default
        If radioButColumn.Checked Then
            renderAs = "Column"
        ElseIf radioButBar.Checked Then
            renderAs = "Bar"
        End If


        'renderAs = "Line"

        ' A DataSeries is a two dimensional array of DataPoints
        ' Time/Year | Volume
        Dim dataSeries1 As String(,) = {{"2000", "52.6"}, {"2001", "40.3"}, {"2002", "20"}, {"2003", "28.7"}, {"2004", "46.1"}, {"2005", "15.1"}, _
            {"2006", "15.1"}, {"2007", "50.5"}, {"2008", "52.1"}, {"2009", "64.8"}, {"2010", "38.6"}, {"2011", "40.23"}}

        'DATA SERIES 2 - axisx stays the same but yaxis values change.
        Dim dataSeries2 As String(,) = {{"2000", "10.2"}, {"2001", "11"}, {"2002", "10.5"}, {"2003", "8"}, {"2004", "8.5"}, {"2005", "7.3"}, _
            {"2006", "7.6"}, {"2007", "7.5"}, {"2008", "8.9"}, {"2009", "10.2"}, {"2010", "10"}, {"2011", "8"}}

        ' Constructing Data XAML
        myXAML = "<vc:Chart Theme=""" & chartTheme & """ Width=""500"" Height=""300""  xmlns:vc=""clr-namespace:Visifire.Charts;assembly=SLVisifire.Charts"" >"
        myXAML = myXAML & "<vc:Chart.Titles>"
        myXAML = myXAML & "<vc:Title Text=""" & chartTitle & """ />"
        myXAML = myXAML & "</vc:Chart.Titles>"
        myXAML = myXAML & "<vc:Chart.AxesX>"
        myXAML = myXAML & "<vc:Axis Title=""" & axisXtitle & """ />"
        myXAML = myXAML & "</vc:Chart.AxesX>"
        myXAML = myXAML & "<vc:Chart.AxesY>"
        myXAML = myXAML & "<vc:Axis Title=""" & axisYtitle & """ ValueFormatString=""#0.##%"" />"
        myXAML = myXAML & "</vc:Chart.AxesY>"
        myXAML = myXAML & "<vc:Chart.Series>"

        ' Create DataSeries XAML fragment
        If inappropriateMails = False Then
            ' Display Single DataSeries
            myXAML += CreateDataSeriesXML(dataSeries1, numberOfDataPoints, renderAs)
        Else
            ' if (inappropriateMails == True)  // Display Double DataSeries with //Stacked Charts
            myXAML += CreateDataSeriesXML(dataSeries1, numberOfDataPoints, "Stacked" & renderAs)
            myXAML += CreateDataSeriesXML(dataSeries2, numberOfDataPoints, "Stacked" & renderAs)
        End If
        ' End of Series and chart Tags
        myXAML = myXAML & "</vc:Chart.Series>"
        myXAML = myXAML & "</vc:Chart>"

        ' Write data xaml as response text
        Return myXAML
    End Function


    ''' <summary>
    ''' Create DataSeries XAML
    ''' </summary>   
    ''' <returns>return myXAML</returns> 
    Private Function CreateDataSeriesXML(ByRef dataSeries As [String](,), numberOfDataPoints As Int32, renderAs As [String]) As [String]
        Dim myXAML As [String]

        myXAML = [String].Format("<vc:DataSeries RenderAs=""{0}"" >", renderAs)
        myXAML = myXAML & "<vc:DataSeries.DataPoints>"
        ' Constructing XAML fragment for DataSeries
        For dataPointIndex As Integer = 0 To numberOfDataPoints - 1
            ' Adding DataPoint XAML fragment
            myXAML = myXAML & "<vc:DataPoint AxisXLabel=""" & Convert.ToString(dataSeries(dataPointIndex, 0)) & """ YValue=""" & Convert.ToString(dataSeries(dataPointIndex, 1)) & """ />"
        Next
        myXAML = myXAML & "</vc:DataSeries.DataPoints>"

        myXAML = myXAML & "</vc:DataSeries>"

        Return myXAML
    End Function



End Class