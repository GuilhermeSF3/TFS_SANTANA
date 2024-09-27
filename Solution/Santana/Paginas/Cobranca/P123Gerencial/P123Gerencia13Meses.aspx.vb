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

Imports System.Web.UI.WebControls.WebParts
Imports System.Web
Imports System.Web.Security
Imports System.Drawing.Printing
Imports System.Data.Common


Namespace Paginas.Cobranca.P123Gerencial

Public Class P123Gerencia13Meses
    Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"
        Private _hfDataSerie1 As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then
                Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-7).ToString("MM/yyyy"))
                txtData.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)

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

            If ultimoDiaMesAnterior <= Convert.ToDateTime("01/dec/2014") Then

                ultimoDiaMesAnterior = Convert.ToDateTime("31/dec/2014")
                
            End If

        Return ultimoDiaMesAnterior.ToString("dd/MM/yyyy")

    End Function




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

                    col = 14
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


                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row



                        If IsDBNull(drow("DESCR")) Then
                            e.Row.Cells(0).Text = ""
                        Else
                            e.Row.Cells(0).Text = drow("DESCR")
                        End If

                        If InStr(drow("DESCR"), "PRODU") = 0 And drow("m15") > drow("m1") Then
                            e.Row.Cells(1).Font.Bold = True
                        Else
                            e.Row.Cells(1).Font.Bold = False
                        End If
                        If IsDBNull(drow("m15")) OrElse drow("m15") = 0 Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = CNumero.FormataNumero(drow("m15"), 2, False)
                        End If

                        If InStr(drow("DESCR"), "PRODU") = 0 And drow("m14") > drow("m1") Then
                            e.Row.Cells(2).Font.Bold = True
                        Else
                            e.Row.Cells(2).Font.Bold = False
                        End If
                        If IsDBNull(drow("m14")) OrElse drow("m14") = 0 Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = CNumero.FormataNumero(drow("m14"), 2, False)
                        End If

                        If InStr(drow("DESCR"), "PRODU") = 0 And drow("m13") > drow("m1") Then
                            e.Row.Cells(3).Font.Bold = True
                        Else
                            e.Row.Cells(3).Font.Bold = False
                        End If
                        If IsDBNull(drow("m13")) OrElse drow("m13") = 0 Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = CNumero.FormataNumero(drow("m13"), 2, False)
                        End If


                        If InStr(drow("DESCR"), "PRODU") = 0 And drow("m12") > drow("m1") Then
                            e.Row.Cells(4).Font.Bold = True
                        Else
                            e.Row.Cells(4).Font.Bold = False
                        End If
                        If IsDBNull(drow("m12")) OrElse drow("m12") = 0 Then
                            e.Row.Cells(4).Text = ""
                        Else
                            e.Row.Cells(4).Text = CNumero.FormataNumero(drow("m12"), 2, False)
                        End If

                        If InStr(drow("DESCR"), "PRODU") = 0 And drow("m11") > drow("m1") Then
                            e.Row.Cells(5).Font.Bold = True
                        Else
                            e.Row.Cells(5).Font.Bold = False
                        End If
                        If IsDBNull(drow("m11")) OrElse drow("m11") = 0 Then
                            e.Row.Cells(5).Text = ""
                        Else
                            e.Row.Cells(5).Text = CNumero.FormataNumero(drow("m11"), 2, False)
                        End If

                        If InStr(drow("DESCR"), "PRODU") = 0 And drow("m10") > drow("m1") Then
                            e.Row.Cells(6).Font.Bold = True
                        Else
                            e.Row.Cells(6).Font.Bold = False
                        End If
                        If IsDBNull(drow("m10")) OrElse drow("m10") = 0 Then
                            e.Row.Cells(6).Text = ""
                        Else
                            e.Row.Cells(6).Text = CNumero.FormataNumero(drow("m10"), 2, False)
                        End If

                        If InStr(drow("DESCR"), "PRODU") = 0 And drow("m9") > drow("m1") Then
                            e.Row.Cells(7).Font.Bold = True
                        Else
                            e.Row.Cells(7).Font.Bold = False
                        End If
                        If IsDBNull(drow("m9")) OrElse drow("m9") = 0 Then
                            e.Row.Cells(7).Text = ""
                        Else
                            e.Row.Cells(7).Text = CNumero.FormataNumero(drow("m9"), 2, False)
                        End If

                        If InStr(drow("DESCR"), "PRODU") = 0 And drow("m8") > drow("m1") Then
                            e.Row.Cells(8).Font.Bold = True
                        Else
                            e.Row.Cells(8).Font.Bold = False
                        End If
                        If IsDBNull(drow("m8")) OrElse drow("m8") = 0 Then
                            e.Row.Cells(8).Text = ""
                        Else
                            e.Row.Cells(8).Text = CNumero.FormataNumero(drow("m8"), 2, False)
                        End If

                        If InStr(drow("DESCR"), "PRODU") = 0 And drow("m7") > drow("m1") Then
                            e.Row.Cells(9).Font.Bold = True
                        Else
                            e.Row.Cells(9).Font.Bold = False
                        End If
                        If IsDBNull(drow("m7")) OrElse drow("m7") = 0 Then
                            e.Row.Cells(9).Text = ""
                        Else
                            e.Row.Cells(9).Text = CNumero.FormataNumero(drow("m7"), 2, False)

                        End If

                        If InStr(drow("DESCR"), "PRODU") = 0 And drow("m6") > drow("m1") Then
                            e.Row.Cells(10).Font.Bold = True
                        Else
                            e.Row.Cells(10).Font.Bold = False
                        End If
                        If IsDBNull(drow("m6")) OrElse drow("m6") = 0 Then
                            e.Row.Cells(10).Text = ""
                        Else
                            e.Row.Cells(10).Text = CNumero.FormataNumero(drow("m13"), 2, False)
                        End If

                        If InStr(drow("DESCR"), "PRODU") = 0 And drow("m5") > drow("m1") Then
                            e.Row.Cells(11).Font.Bold = True
                        Else
                            e.Row.Cells(11).Font.Bold = False
                        End If
                        If IsDBNull(drow("m5")) OrElse drow("m5") = 0 Then
                            e.Row.Cells(11).Text = ""
                        Else
                            e.Row.Cells(11).Text = CNumero.FormataNumero(drow("m5"), 2, False)
                        End If

                        If InStr(drow("DESCR"), "PRODU") = 0 And drow("m4") > drow("m1") Then
                            e.Row.Cells(12).Font.Bold = True
                        Else
                            e.Row.Cells(12).Font.Bold = False
                        End If
                        If IsDBNull(drow("m4")) OrElse drow("m4") = 0 Then
                            e.Row.Cells(12).Text = ""
                        Else
                            e.Row.Cells(12).Text = CNumero.FormataNumero(drow("m4"), 2, False)
                        End If

                        If InStr(drow("DESCR"), "PRODU") = 0 And drow("m3") > drow("m1") Then
                            e.Row.Cells(13).Font.Bold = True
                        Else
                            e.Row.Cells(13).Font.Bold = False
                        End If
                        If IsDBNull(drow("m3")) OrElse drow("m3") = 0 Then
                            e.Row.Cells(13).Text = ""
                        Else
                            e.Row.Cells(13).Text = CNumero.FormataNumero(drow("m3"), 2, False)
                        End If

                        If InStr(drow("DESCR"), "PRODU") = 0 And drow("m2") > drow("m1") Then
                            e.Row.Cells(14).Font.Bold = True
                        Else
                            e.Row.Cells(14).Font.Bold = False
                        End If
                        If IsDBNull(drow("m2")) OrElse drow("m2") = 0 Then
                            e.Row.Cells(14).Text = ""
                        Else
                            e.Row.Cells(14).Text = CNumero.FormataNumero(drow("m2"), 2, False)
                        End If

                        If IsDBNull(drow("m1")) OrElse drow("m1") = 0 Then
                            e.Row.Cells(15).Text = ""
                        Else
                            e.Row.Cells(15).Text = CNumero.FormataNumero(drow("m1"), 2, False)
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

            '            ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = Nothing
        GridView1.DataSource = GetData()
        GridView1.PageIndex = 0
        GridView1.DataBind()

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



        Private Function GetData() As DataTable
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim table1 As DataTable = Nothing

            table1 = ClassBD.GetExibirGrid("[scr_vlr_GerencialP123_13M] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "'", "GerencialP123_13M", strConn)


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
        End If
    End Sub








    Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
    End Sub


    Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Score x Inadimplencia - 13 meses' ,'Inadimplencia por Score nas safras, acima de 90 dias após a P1-1a parcela (P123).');", True)
    End Sub


    Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../../Menu.aspx")
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
                    Dim filename As String = String.Format("GerencialP123_13M_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename)
                    Response.ContentEncoding = System.Text.Encoding.Default

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

                        Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "Score x Inadimplencia (visão 13 meses)")


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
            Session(hfGridView1SVID) = hfGridView1SV.Value
        End Sub

        Protected Sub hfGridView1SH_ValueChanged(sender As Object, e As EventArgs)
            Session(hfGridView1SHID) = hfGridView1SH.Value
        End Sub



    Protected Sub GridView1_DataBound(sender As Object, e As EventArgs)
            Try
                Dim gridView As GridView = CType(sender, GridView)
                If Not IsNothing(gridView.DataSource) AndAlso CType(gridView.DataSource, System.Data.DataTable).Rows.Count > 0 Then
                    Util.CWebPageUtil.PreencheListaDePaginas(gridView, CType(gridView.BottomPagerRow.Cells(0).FindControl("PagerPages"), DropDownList))
                End If
            Catch ex As Exception
            End Try
    End Sub


    Protected Sub PagerPages_SelectedIndexChanged(sender As Object, e As EventArgs)
        GridView1.DataSource = DataGridView()
        GridView1.PageIndex = CType(sender, DropDownList).SelectedIndex
        GridView1.DataBind()
    End Sub



  
End Class

End Namespace
