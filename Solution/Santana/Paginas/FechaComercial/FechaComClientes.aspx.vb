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

Public Class FechaComClientes
    Inherits SantanaPage

    Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
    Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"
    Private _hfDataSerie1 As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
            txtData.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)


            If ContextoWeb.DadosTransferencia.CodCobradora <> 0 Or ContextoWeb.DadosTransferencia.CodAgente <> 0 Then
                BindGridView1Data()
                ContextoWeb.DadosTransferencia.CodAgente = 0
                ContextoWeb.DadosTransferencia.CodCobradora = 0
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



    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        Try

            Dim cor1 As Drawing.Color

            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then

                    Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row


                    If IsDBNull(drow("COR_LINHA")) Then
                        cor1 = e.Row.Cells(0).BackColor
                    ElseIf drow("COR_LINHA") = "A1" Then
                        cor1 = Drawing.ColorTranslator.FromOle(&HE0CED6)
                    ElseIf drow("COR_LINHA") = "A2" Then
                        cor1 = Drawing.ColorTranslator.FromOle(&HDBDCF2)
                    ElseIf drow("COR_LINHA") = "A3" Then
                        cor1 = Drawing.ColorTranslator.FromOle(&HC0FFFF)
                    ElseIf drow("COR_LINHA") = "A4" Then
                        cor1 = Drawing.ColorTranslator.FromOle(&HD9E9FD)
                    ElseIf drow("COR_LINHA") = "A5" Then
                        cor1 = Drawing.ColorTranslator.FromOle(&HE4CCB8)
                    ElseIf drow("COR_LINHA") = "A6" Then
                        cor1 = Drawing.ColorTranslator.FromOle(&H8FBFFA)
                    ElseIf drow("COR_LINHA") = "A7" Then
                        cor1 = Drawing.ColorTranslator.FromOle(&HF3EEDA)
                    ElseIf drow("COR_LINHA") = "A8" Then
                        cor1 = Drawing.ColorTranslator.FromOle(&HBCE4D8)
                    ElseIf drow("COR_LINHA") = "A9" Then
                        cor1 = Drawing.ColorTranslator.FromOle(&HEFBF79)
                    ElseIf drow("COR_LINHA") = "A10" Then
                        cor1 = Drawing.ColorTranslator.FromOle(&H8BD8FF)
                    ElseIf drow("COR_LINHA") = "A11" Then
                        cor1 = Drawing.ColorTranslator.FromOle(&HE1ECEE)
                    ElseIf drow("COR_LINHA") = "A12" Then
                        cor1 = Drawing.ColorTranslator.FromOle(&HDCCD92)
                    ElseIf drow("COR_LINHA") = "A13" Then
                        cor1 = Drawing.ColorTranslator.FromOle(&HD9D9D9)
                    Else
                        cor1 = e.Row.Cells(0).BackColor
                    End If


                    If IsDBNull(drow("DESCR")) Then
                        e.Row.Cells(0).Text = ""
                    Else
                        e.Row.Cells(0).Text = drow("DESCR")
                    End If
                    e.Row.Cells(0).BackColor = cor1

                    If IsDBNull(drow("pv") Or drow("pv") = 0) Then
                        e.Row.Cells(1).Text = ""
                    Else
                        e.Row.Cells(1).Text = CNumero.FormataNumero(drow("pv"), 2)
                    End If
                    e.Row.Cells(1).BackColor = cor1

                    If IsDBNull(drow("perc_part") Or drow("perc_part") = 0) Then
                        e.Row.Cells(2).Text = ""
                    Else
                        e.Row.Cells(2).Text = CNumero.FormataNumero(drow("perc_part"), 2)
                    End If
                    e.Row.Cells(2).BackColor = cor1

                    If IsDBNull(drow("perc_evol") Or drow("perc_evol") = 0) Then
                        e.Row.Cells(3).Text = ""
                    Else
                        e.Row.Cells(3).Text = CNumero.FormataNumero(drow("perc_evol"), 2)
                    End If
                    e.Row.Cells(3).BackColor = cor1

                  
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
        Dim table As DataTable = Util.ClassBD.GetExibirGrid("[scr_COMERCIAL_CLIENTES] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "'", "FechaComClientes", strConn)

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
            BindGraphData()
        End If
    End Sub



    Protected Sub BindGraphData()

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim objData As DataTable = ClassBD.GetExibirGrid("[scr_COMERCIAL_CLIENTES_GRAF] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "'", "FechaComClientes", strConn)


        _hfDataSerie1 = _hfDataSerie1 & "<vc:DataSeries ShowInLegend=""True"" RenderAs=""Pie"" MarkerType=""Circle"" SelectionEnabled=""True"" LineThickness=""3"" LabelText=""#AxisXLabel, #YValue"" >"
        _hfDataSerie1 = _hfDataSerie1 & "<vc:DataSeries.DataPoints> "

        For Each oDataRow As DataRow In objData.Rows

            If Not IsDBNull(oDataRow("perc_part")) Then
                If IsDBNull(oDataRow("COR_LINHA")) Then
                    _hfDataSerie1 = _hfDataSerie1 & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel= """ & oDataRow("DESCR") & """  YValue=""" & Replace((oDataRow("perc_part") / 1.0), ",", ".") & """/> "
                ElseIf oDataRow("COR_LINHA") = "A1" Then
                    _hfDataSerie1 = _hfDataSerie1 & "<vc:DataPoint Color=""#FFD6CEE0""  LabelEnabled=""True"" AxisXLabel= """ & oDataRow("DESCR") & """  YValue=""" & Replace((oDataRow("perc_part") / 1.0), ",", ".") & """/> "
                ElseIf oDataRow("COR_LINHA") = "A2" Then
                    _hfDataSerie1 = _hfDataSerie1 & "<vc:DataPoint Color=""#FFF2DCDB""  LabelEnabled=""True"" AxisXLabel= """ & oDataRow("DESCR") & """  YValue=""" & Replace((oDataRow("perc_part") / 1.0), ",", ".") & """/> "
                ElseIf oDataRow("COR_LINHA") = "A3" Then
                    _hfDataSerie1 = _hfDataSerie1 & "<vc:DataPoint Color=""#FFFFFFC0"" LabelEnabled=""True"" AxisXLabel= """ & oDataRow("DESCR") & """  YValue=""" & Replace((oDataRow("perc_part") / 1.0), ",", ".") & """/> "
                ElseIf oDataRow("COR_LINHA") = "A4" Then
                    _hfDataSerie1 = _hfDataSerie1 & "<vc:DataPoint Color=""#FFFDE9D9"" LabelEnabled=""True"" AxisXLabel= """ & oDataRow("DESCR") & """  YValue=""" & Replace((oDataRow("perc_part") / 1.0), ",", ".") & """/> "
                ElseIf oDataRow("COR_LINHA") = "A5" Then
                    _hfDataSerie1 = _hfDataSerie1 & "<vc:DataPoint Color=""#FFB8CCE4"" LabelEnabled=""True"" AxisXLabel= """ & oDataRow("DESCR") & """  YValue=""" & Replace((oDataRow("perc_part") / 1.0), ",", ".") & """/> "
                ElseIf oDataRow("COR_LINHA") = "A6" Then
                    _hfDataSerie1 = _hfDataSerie1 & "<vc:DataPoint Color=""#FFFABF8F"" LabelEnabled=""True"" AxisXLabel= """ & oDataRow("DESCR") & """  YValue=""" & Replace((oDataRow("perc_part") / 1.0), ",", ".") & """/> "
                ElseIf oDataRow("COR_LINHA") = "A7" Then
                    _hfDataSerie1 = _hfDataSerie1 & "<vc:DataPoint Color=""#FFDAEEF3"" LabelEnabled=""True"" AxisXLabel= """ & oDataRow("DESCR") & """  YValue=""" & Replace((oDataRow("perc_part") / 1.0), ",", ".") & """/> "
                ElseIf oDataRow("COR_LINHA") = "A8" Then
                    _hfDataSerie1 = _hfDataSerie1 & "<vc:DataPoint Color=""#FFD8E4BC"" LabelEnabled=""True"" AxisXLabel= """ & oDataRow("DESCR") & """  YValue=""" & Replace((oDataRow("perc_part") / 1.0), ",", ".") & """/> "
                ElseIf oDataRow("COR_LINHA") = "A9" Then
                    _hfDataSerie1 = _hfDataSerie1 & "<vc:DataPoint Color=""#FF79BFEF"" LabelEnabled=""True"" AxisXLabel= """ & oDataRow("DESCR") & """  YValue=""" & Replace((oDataRow("perc_part") / 1.0), ",", ".") & """/> "
                ElseIf oDataRow("COR_LINHA") = "A10" Then
                    _hfDataSerie1 = _hfDataSerie1 & "<vc:DataPoint Color=""#FFFFD88B"" LabelEnabled=""True"" AxisXLabel= """ & oDataRow("DESCR") & """  YValue=""" & Replace((oDataRow("perc_part") / 1.0), ",", ".") & """/> "
                ElseIf oDataRow("COR_LINHA") = "A11" Then
                    _hfDataSerie1 = _hfDataSerie1 & "<vc:DataPoint Color=""#FFEEECE1"" LabelEnabled=""True"" AxisXLabel= """ & oDataRow("DESCR") & """  YValue=""" & Replace((oDataRow("perc_part") / 1.0), ",", ".") & """/> "
                ElseIf oDataRow("COR_LINHA") = "A12" Then
                    _hfDataSerie1 = _hfDataSerie1 & "<vc:DataPoint Color=""#FF92CDDC"" LabelEnabled=""True"" AxisXLabel= """ & oDataRow("DESCR") & """  YValue=""" & Replace((oDataRow("perc_part") / 1.0), ",", ".") & """/> "
                ElseIf oDataRow("COR_LINHA") = "A13" Then
                    _hfDataSerie1 = _hfDataSerie1 & "<vc:DataPoint Color=""#FFD9D9D9"" LabelEnabled=""True"" AxisXLabel= """ & oDataRow("DESCR") & """  YValue=""" & Replace((oDataRow("perc_part") / 1.0), ",", ".") & """/> "
                Else
                    _hfDataSerie1 = _hfDataSerie1 & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel= """ & oDataRow("DESCR") & """  YValue=""" & Replace((oDataRow("perc_part") / 1.0), ",", ".") & """/> "
                End If
            End If
        Next

        _hfDataSerie1 = _hfDataSerie1 & "</vc:DataSeries.DataPoints> "
        _hfDataSerie1 = _hfDataSerie1 & "</vc:DataSeries> "

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

            BindGraphData()
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
                Dim filename As String = String.Format("FechaComClientes_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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

    Protected Sub txtData_TextChanged(sender As Object, e As EventArgs)

        GridView1.DataSource = Nothing
        GridView1.DataBind()

    End Sub


    <WebMethod()>
    Public Shared Function BindGraphData(txtData As String) As List(Of Object)


        Dim iData As New List(Of Object)()
        Dim labels As New List(Of String)()
        Dim legendas As New List(Of String)()

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

        Dim oData As New CDataHora()
        oData.Data = Convert.ToDateTime(txtData)

        Dim objData As DataTable = ClassBD.GetExibirGrid("[scr_COMERCIAL_CLIENTES_GRAF] '" & Right(txtData, 4) & Mid(txtData, 4, 2) & Left(txtData, 2) & "'", "FechaComClientes", strConn)

        labels.Add("1")

        iData.Add(labels)




        For Each oDataRow As DataRow In objData.Rows

            Dim dataItem As New List(Of Double)()

            If Not IsDBNull(oDataRow("DESCR")) Then
                dataItem.Add(oDataRow("PERC_PART"))
            End If
            iData.Add(oDataRow("DESCR"))
            iData.Add(dataItem)

        Next

        Return iData

    End Function

End Class

