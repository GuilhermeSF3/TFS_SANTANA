Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic
Imports Santana.Seguranca
Imports Util

Namespace Paginas.RiscoCredito


    Public Class PDDSafraTempo
        Inherits SantanaPage

        Private Const hfGridView1SVID As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const hfGridView1SHID As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

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


     


        Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

            Try


                If e.Row.RowType = DataControlRowType.Header Then

                ElseIf e.Row.RowType = DataControlRowType.DataRow Then
                    If Not e.Row.DataItem Is Nothing Then
                        Dim cor As Color

                        Dim drow As DataRow = CType(e.Row.DataItem, DataRowView).Row
                        Dim col As Integer
                        Dim ordem As Integer

                        cor = Drawing.ColorTranslator.FromOle(&HE8FFE9)

                        If IsDBNull(drow("ORDEM")) Then
                            ordem = 0
                        Else
                            ordem = drow("ORDEM")
                        End If


                        col = 0
                        If IsDBNull(drow("DESCR")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("DESCR")
                        End If

                        col += 1
                        If IsDBNull(drow("m0")) Or drow("m0") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m0"), 2)
                        End If
                        If Right(drow("DESCR"), 2) >= "01" And Right(drow("DESCR"), 2) <= "10" Then
                            e.Row.Cells(col).BackColor = cor
                        End If

                        col += 1
                        If IsDBNull(drow("m1")) Or drow("m1") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m1"), 2)
                        End If
                        If Right(drow("DESCR"), 2) >= "01" And Right(drow("DESCR"), 2) <= "09" Then
                            e.Row.Cells(col).BackColor = cor
                        End If


                        col += 1
                        If IsDBNull(drow("m2")) Or drow("m2") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m2"), 2)
                        End If
                        If Right(drow("DESCR"), 2) >= "01" And Right(drow("DESCR"), 2) <= "08" Then
                            e.Row.Cells(col).BackColor = cor
                        End If

                        col += 1
                        If IsDBNull(drow("m3")) Or drow("m3") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m3"), 2)
                        End If
                        If Right(drow("DESCR"), 2) >= "01" And Right(drow("DESCR"), 2) <= "07" Then
                            e.Row.Cells(col).BackColor = cor
                        End If

                        col += 1
                        If IsDBNull(drow("m4")) Or drow("m4") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m4"), 2)
                        End If
                        If Right(drow("DESCR"), 2) >= "01" And Right(drow("DESCR"), 2) <= "06" Then
                            e.Row.Cells(col).BackColor = cor
                        End If

                        col += 1
                        If IsDBNull(drow("m5")) Or drow("m5") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m5"), 2)
                        End If
                        If Right(drow("DESCR"), 2) >= "01" And Right(drow("DESCR"), 2) <= "05" Then
                            e.Row.Cells(col).BackColor = cor
                        End If

                        col += 1
                        If IsDBNull(drow("m6")) Or drow("m6") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m6"), 2)
                        End If
                        If Right(drow("DESCR"), 2) >= "01" And Right(drow("DESCR"), 2) <= "04" Then
                            e.Row.Cells(col).BackColor = cor
                        End If

                        col += 1
                        If IsDBNull(drow("m7")) Or drow("m7") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m7"), 2)
                        End If
                        If Right(drow("DESCR"), 2) >= "01" And Right(drow("DESCR"), 2) <= "03" Then
                            e.Row.Cells(col).BackColor = cor
                        End If

                        col += 1
                        If IsDBNull(drow("m8")) Or drow("m8") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m8"), 2)
                        End If
                        If Right(drow("DESCR"), 2) >= "01" And Right(drow("DESCR"), 2) <= "02" Then
                            e.Row.Cells(col).BackColor = cor
                        End If

                        col += 1
                        If IsDBNull(drow("m9")) Or drow("m9") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m9"), 2)
                        End If
                        If Right(drow("DESCR"), 2) = "01" Then
                            e.Row.Cells(col).BackColor = cor
                        End If


                        col += 1
                        If IsDBNull(drow("m10")) Or drow("m10") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m10"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m11")) Or drow("m11") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m11"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m12")) Or drow("m12") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m12"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m13")) Or drow("m13") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m13"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m14")) Or drow("m14") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m14"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m15")) Or drow("m15") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m15"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m16")) Or drow("m16") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m16"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m17")) Or drow("m17") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m17"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m18")) Or drow("m18") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m18"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m19")) Or drow("m19") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m19"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m20")) Or drow("m20") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m20"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m21")) Or drow("m21") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m21"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m22")) Or drow("m22") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m22"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m23")) Or drow("m23") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m23"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m24")) Or drow("m24") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m24"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m25")) Or drow("m25") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m25"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m26")) Or drow("m26") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m26"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m27")) Or drow("m27") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m27"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m28")) Or drow("m28") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m28"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m29")) Or drow("m29") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m29"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m30")) Or drow("m30") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m30"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m31")) Or drow("m31") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m31"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m32")) Or drow("m32") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m32"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m33")) Or drow("m33") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m33"), 2)
                        End If




                    End If
                End If

          
            Catch ex As Exception

            End Try
        End Sub


        
        Private Sub GridView2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView2.RowDataBound

            Try
               
                Dim col As Integer

                If e.Row.RowType = DataControlRowType.DataRow Then
                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As DataRow = CType(e.Row.DataItem, DataRowView).Row

                        col = 0
                        If IsDBNull(drow("DESCR")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("DESCR")
                        End If


                        col += 1
                        If IsDBNull(drow("m1")) Or drow("m1") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m1"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m2")) Or drow("m2") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m2"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m3")) Or drow("m3") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m3"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m4")) Or drow("m4") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m4"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m5")) Or drow("m5") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m5"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m6")) Or drow("m6") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m6"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m7")) Or drow("m7") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m7"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m8")) Or drow("m8") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m8"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m9")) Or drow("m9") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m9"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m10")) Or drow("m10") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m10"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m11")) Or drow("m11") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m11"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("m12")) Or drow("m12") = 0 Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = CNumero.FormataNumero(drow("m12"), 2)
                        End If

                    End If
                End If


            Catch ex As Exception

            End Try
        End Sub


        Protected Sub BindGridView1Data()

            ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = Nothing
            GridView1.DataSource = GetData()
            GridView1.DataBind()

        End Sub

        Protected Sub BindGridView2Data()

            ViewState("09E17044-8B60-4578-8380-856CD030512C") = Nothing
            GridView2.DataSource = GetData2()
            GridView2.DataBind()

        End Sub


        Protected Sub BindGridView1DataView()

            GridView1.DataSource = DataGridView
            GridView1.DataBind()

        End Sub

        Protected Sub BindGridView2DataView()

            GridView2.DataSource = DataGridView2
            GridView2.DataBind()

        End Sub


        Private Function GetData() As DataTable

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim table As DataTable = ClassBD.GetExibirGrid("[scr_risco_pdd_safra_tempo] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "'", "PDDSafraTempo", strConn)

            Return table

        End Function


        Private Function GetData2() As DataTable

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim table As DataTable = ClassBD.GetExibirGrid("scr_risco_pdd_safra_tempo2 '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "'", "PDDSafraTempo2", strConn)

            Return table

        End Function



        Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "tmp", "Alerta('PDD por Safra no Tempo' ,'Apresenta o percentual da despesa de provisão de PDD por Prazo Decorrido nas Datas de Originação (safra).');", True)
        End Sub


        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../Menu.aspx")
        End Sub


        Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
            Try

                BindGridView1Data()
                BindGridView2Data()

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
                    Dim filename As String = String.Format("PDDSafraTempo_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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

                        Response.Write("<P></P>")
                        GridView2.RenderControl(hw)

                        sr.Close()

                        Dim style As String = "<html><head><style type='text/css'>" & sb.ToString() & "</style><head>"
                        Response.Write(style)
                        Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "PDD por Safra no Tempo")

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


        Protected Sub GridView2_RowCreated(sender As Object, e As GridViewRowEventArgs)
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

        Public Property DataGridView2 As DataTable
            Get

                If ViewState("09E17044-8B60-4578-8380-856CD030512C") Is Nothing Then
                    ViewState("09E17044-8B60-4578-8380-856CD030512C") = GetData2()
                End If

                Return DirectCast(ViewState("09E17044-8B60-4578-8380-856CD030512C"), DataTable)
            End Get
            Set(value As DataTable)
                ViewState("09E17044-8B60-4578-8380-856CD030512C") = value
            End Set
        End Property

    End Class
End NameSpace