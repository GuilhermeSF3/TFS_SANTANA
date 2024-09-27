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


Namespace Paginas.Cobranca.P123Gerencial

    Public Class P123Gerencial
        Inherits SantanaPage

        Private Const hfGridView1SVID As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const hfGridView1SHID As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Private _hfDataSerie1 As String = ""


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            If Not IsPostBack Then
                Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-7).ToString("MM/yyyy"))
                txtDataSafra.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)

                Carrega_Faixa()
                Carrega_Grafico()

            End If


            Const script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)
        End Sub

        Protected Sub btnDataSafraAnterior_Click(sender As Object, e As EventArgs)

            Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataSafra.Text)
            ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddMonths(-1)

            If ultimoDiaMesAnterior.Year = Now.Date.Year Then
                If ultimoDiaMesAnterior.Month <= Now.Date.Month Then
                    txtDataSafra.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)
                End If
            ElseIf ultimoDiaMesAnterior.Year < Now.Date.Year Then
                txtDataSafra.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)
            End If
        End Sub


        Protected Sub btnProximaDataSafra_Click(sender As Object, e As EventArgs)

            Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataSafra.Text)
            ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddMonths(1)

            If ultimoDiaMesAnterior.Year = Now.Date.Year Then
                If ultimoDiaMesAnterior.Month <= Now.Date.Month Then
                    txtDataSafra.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)
                End If
            ElseIf ultimoDiaMesAnterior.Year < Now.Date.Year Then
                txtDataSafra.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)
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

                GridView1.DataSource = Nothing
                GridView1.DataBind()

                GridView2.DataSource = Nothing
                GridView2.DataBind()
            End If
            Return ultimoDiaMesAnterior.ToString("dd/MM/yyyy")
        End Function



        Private Sub Carrega_Faixa()

            Try

                ddlFaixa.Items.Insert(0, New ListItem("Parcela 1", "1"))
                ddlFaixa.Items.Insert(1, New ListItem("Parcela 2", "2"))
                ddlFaixa.Items.Insert(2, New ListItem("Parcela 3", "3"))
                ddlFaixa.Items.Insert(3, New ListItem("Parcela 1+2+3", "4"))

                ddlFaixa.SelectedIndex = 0


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub


        Private Sub Carrega_Grafico()

            Try

                ddlGrafico.Items.Insert(0, New ListItem("1o Momento", "1"))
                ddlGrafico.Items.Insert(1, New ListItem("2o Momento", "2"))
                ddlGrafico.Items.Insert(2, New ListItem("3o Momento", "3"))
                ddlGrafico.Items.Insert(3, New ListItem("4o Momento", "4"))

                ddlGrafico.SelectedIndex = 0


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

            Try
                Dim cor As Color


                If e.Row.RowType = DataControlRowType.Header Then

                    Dim oData As New CDataHora(Convert.ToDateTime(txtDataSafra.Text))

                    e.Row.Cells(14).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                    oData.Data = oData.Data.AddMonths(-1)
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

                        If InStr(UCase(drow("Descricao")), "QTDE") > 0 Then
                            e.Row.Cells(13).Text = CNumero.FormataNumero(drow("M13"), 0)
                        Else
                            e.Row.Cells(13).Text = CNumero.FormataNumero(drow("M13"), 2)
                        End If

                        If InStr(UCase(drow("Descricao")), "QTDE") > 0 Then
                            e.Row.Cells(14).Text = CNumero.FormataNumero(drow("M14"), 0)
                        Else
                            e.Row.Cells(14).Text = CNumero.FormataNumero(drow("M14"), 2)
                        End If

                        If IsDBNull(drow("ORDEM_LINHA")) Then
                            e.Row.Cells(15).Text = ""
                        Else
                            e.Row.Cells(15).Text = drow("ORDEM_LINHA")
                        End If

                        If drow("ORDEM_LINHA") <= 3 And ddlFaixa.SelectedValue = "1" Then
                            e.Row.Cells(13).BackColor = Drawing.ColorTranslator.FromOle(&HA6A6A6)
                        End If
                        If drow("ORDEM_LINHA") = 4 And ddlFaixa.SelectedValue = "1" Then
                            e.Row.Cells(12).BackColor = Drawing.ColorTranslator.FromOle(&HA6A6A6)
                        End If
                        If drow("ORDEM_LINHA") = 5 And ddlFaixa.SelectedValue = "1" Then
                            e.Row.Cells(11).BackColor = Drawing.ColorTranslator.FromOle(&HA6A6A6)
                        End If

                        If drow("ORDEM_LINHA") <= 3 And ddlFaixa.SelectedValue = "2" Then
                            e.Row.Cells(12).BackColor = Drawing.ColorTranslator.FromOle(&HA6A6A6)
                        End If

                        If drow("ORDEM_LINHA") = 4 And ddlFaixa.SelectedValue = "2" Then
                            e.Row.Cells(11).BackColor = Drawing.ColorTranslator.FromOle(&HA6A6A6)
                        End If

                        If drow("ORDEM_LINHA") = 5 And ddlFaixa.SelectedValue = "2" Then
                            e.Row.Cells(10).BackColor = Drawing.ColorTranslator.FromOle(&HA6A6A6)
                        End If

                        If drow("ORDEM_LINHA") <= 3 And ddlFaixa.SelectedValue = "3" Then
                            e.Row.Cells(11).BackColor = Drawing.ColorTranslator.FromOle(&HA6A6A6)
                        End If
                        If drow("ORDEM_LINHA") = 4 And ddlFaixa.SelectedValue = "3" Then
                            e.Row.Cells(10).BackColor = Drawing.ColorTranslator.FromOle(&HA6A6A6)
                        End If
                        If drow("ORDEM_LINHA") = 5 And ddlFaixa.SelectedValue = "3" Then
                            e.Row.Cells(9).BackColor = Drawing.ColorTranslator.FromOle(&HA6A6A6)
                        End If



                End If
                End If


            Catch ex As Exception

            End Try
        End Sub



        Private Sub GridView2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView2.RowDataBound

            Try
                Dim cor As Color


                If e.Row.RowType = DataControlRowType.Header Then

                    Dim oData As New CDataHora(Convert.ToDateTime(txtDataSafra.Text))
                    oData.Data = oData.Data.AddYears(-1)


                    e.Row.Cells(14).Text = oData.NomeMesSigla + "/" + oData.Data.Year.ToString()
                    oData.Data = oData.Data.AddMonths(-1)
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

                        If drow("ORDEM_LINHA") <= 3 And ddlFaixa.SelectedValue = "1" Then
                            e.Row.Cells(13).BackColor = Drawing.ColorTranslator.FromOle(&HA6A6A6)
                        End If
                        If drow("ORDEM_LINHA") = 4 And ddlFaixa.SelectedValue = "1" Then
                            e.Row.Cells(12).BackColor = Drawing.ColorTranslator.FromOle(&HA6A6A6)
                        End If
                        If drow("ORDEM_LINHA") = 5 And ddlFaixa.SelectedValue = "1" Then
                            e.Row.Cells(11).BackColor = Drawing.ColorTranslator.FromOle(&HA6A6A6)
                        End If

                        If drow("ORDEM_LINHA") <= 3 And ddlFaixa.SelectedValue = "2" Then
                            e.Row.Cells(12).BackColor = Drawing.ColorTranslator.FromOle(&HA6A6A6)
                        End If
                        If drow("ORDEM_LINHA") = 4 And ddlFaixa.SelectedValue = "2" Then
                            e.Row.Cells(11).BackColor = Drawing.ColorTranslator.FromOle(&HA6A6A6)
                        End If
                        If drow("ORDEM_LINHA") = 5 And ddlFaixa.SelectedValue = "2" Then
                            e.Row.Cells(10).BackColor = Drawing.ColorTranslator.FromOle(&HA6A6A6)
                        End If

                        If drow("ORDEM_LINHA") <= 3 And ddlFaixa.SelectedValue = "3" Then
                            e.Row.Cells(11).BackColor = Drawing.ColorTranslator.FromOle(&HA6A6A6)
                        End If
                        If drow("ORDEM_LINHA") = 4 And ddlFaixa.SelectedValue = "3" Then
                            e.Row.Cells(10).BackColor = Drawing.ColorTranslator.FromOle(&HA6A6A6)
                        End If
                        If drow("ORDEM_LINHA") = 5 And ddlFaixa.SelectedValue = "3" Then
                            e.Row.Cells(9).BackColor = Drawing.ColorTranslator.FromOle(&HA6A6A6)
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

        Protected Sub BindGridView2Data()

            GridView2.DataSource = GetData2()
            GridView2.DataBind()

        End Sub

        Protected Sub BindGridView1DataView()

            GridView1.DataSource = DataGridView1
            GridView1.DataBind()

        End Sub

        Protected Sub BindGridView2DataView()

            GridView2.DataSource = DataGridView2
            GridView2.DataBind()

        End Sub


        Private Function GetData1() As DataTable

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

            Dim oDataSafra As New CDataHora(Convert.ToDateTime(txtDataSafra.Text))

            Dim table As DataTable = ClassBD.GetExibirGrid("scr_vlr_GerencialP123 '" & oDataSafra.Data.ToString("yyyyMMdd") & "', '" & oDataSafra.Data.ToString("yyyyMMdd") & "', '" & ddlFaixa.SelectedValue & "', '" & ddlGrafico.SelectedValue & "', 2", "GraficoMensal", strConn)

            Return table

        End Function

        Private Function GetData2() As DataTable

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

            Dim oDataSafra As New CDataHora(Convert.ToDateTime(txtDataSafra.Text))

            oDataSafra.Data = oDataSafra.Data.AddYears(-1)


            Dim table As DataTable = ClassBD.GetExibirGrid("scr_vlr_GerencialP123_AnoAnterior '" & oDataSafra.Data.ToString("yyyyMMdd") & "', '" & oDataSafra.Data.ToString("yyyyMMdd") & "', '" & ddlFaixa.SelectedValue & "', '" & ddlGrafico.SelectedValue & "', 2", "GraficoMensal", strConn)

            Return table

        End Function



        Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
            If e.NewPageIndex >= 0 Then
                GridView1.PageIndex = e.NewPageIndex
                BindGridView1DataView()
            End If
        End Sub


        Protected Sub GridView2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
            If e.NewPageIndex >= 0 Then
                GridView2.PageIndex = e.NewPageIndex
                BindGridView2DataView()
            End If
        End Sub


        Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "tmp", "Alerta('P123 Gerencial' ,'Para a parcela selecionada, mostra os dados das Safras mensais e suas inadimplencias ao longo do tempo (30, 60, 90 e acima de 90d após o vencimento da 1a parcela) no ano e no ano anterior.');", True)

        End Sub


        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../../Menu.aspx")
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
                    Dim filename As String = String.Format("Risco_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename)
                    Response.ContentEncoding = System.Text.Encoding.Default
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.ms-excel"

                    Using sw As New StringWriter()

                        Dim hw As New HtmlTextWriter(sw)

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
                        GridView2.RenderControl(hw)


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



        Protected Sub ddlFaixa_SelectedIndexChanged(sender As Object, e As EventArgs)

            GridView1.DataSource = Nothing
            GridView1.DataBind()

            GridView2.DataSource = Nothing
            GridView2.DataBind()

        End Sub

        Protected Sub ddlGrafico_SelectedIndexChanged(sender As Object, e As EventArgs)

            GridView1.DataSource = Nothing
            GridView1.DataBind()

            GridView2.DataSource = Nothing
            GridView2.DataBind()

        End Sub

        Protected Sub txtData_TextChanged(sender As Object, e As EventArgs)

            GridView1.DataSource = Nothing
            GridView1.DataBind()

            GridView2.DataSource = Nothing
            GridView2.DataBind()

        End Sub



        Protected Sub GridView1_DataBound(sender As Object, e As EventArgs)
            Dim gridView As GridView = CType(sender, GridView)
            If Not IsNothing(gridView.DataSource) AndAlso CType(gridView.DataSource, System.Data.DataTable).Rows.Count > 0 Then
                Util.CWebPageUtil.PreencheListaDePaginas(gridView, CType(gridView.BottomPagerRow.Cells(0).FindControl("PagerPages"), DropDownList))
            End If
        End Sub


        Protected Sub GridView2_DataBound(sender As Object, e As EventArgs)
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

        Protected Sub PagerPages2_SelectedIndexChanged(sender As Object, e As EventArgs)
            GridView2.DataSource = DataGridView2()
            GridView2.PageIndex = CType(sender, DropDownList).SelectedIndex
            GridView2.DataBind()
        End Sub



        <WebMethod()>
        Public Shared Function BindGraphData(txtDataSafra As String, ddlFaixa As Integer, ddlGrafico As Integer) As List(Of Object)


            Dim iData As New List(Of Object)()
            Dim labels As New List(Of String)()
            Dim legendas As New List(Of String)()

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

            Dim oData As New CDataHora()
            oData.Data = Convert.ToDateTime(txtDataSafra)

            Dim oDataSafra As New CDataHora()
            oDataSafra.Data = Convert.ToDateTime(txtDataSafra)

            Dim objData As DataTable = ClassBD.GetExibirGrid("scr_grafico_P123Gerencial '" & oDataSafra.Data.ToString("yyyyMMdd") & "', '" & oDataSafra.Data.ToString("yyyyMMdd") & "', '" & ddlFaixa & "', '" & ddlGrafico & "'", "Grafico", strConn)

            labels.Add("Jan")
            labels.Add("Fev")
            labels.Add("Mar")
            labels.Add("Abr")
            labels.Add("Mai")
            labels.Add("Jun")

            labels.Add("Jul")
            labels.Add("Ago")
            labels.Add("Set")
            labels.Add("Out")
            labels.Add("Nov")
            labels.Add("Dez")

            iData.Add(labels)


            For Each oDataRow As DataRow In objData.Rows

                Dim dataItem As New List(Of Double)()

                If Not IsDBNull(oDataRow("M1")) Then
                    If oDataRow("M1") < 20.0 Then
                        dataItem.Add(oDataRow("M1"))
                    End If
                End If
                If Not IsDBNull(oDataRow("M2")) Then
                    If oDataRow("M2") < 20.0 Then
                        dataItem.Add(oDataRow("M2"))
                    End If
                End If
                If Not IsDBNull(oDataRow("M3")) Then
                    If oDataRow("M3") < 20.0 Then
                        dataItem.Add(oDataRow("M3"))
                    End If
                End If
                If Not IsDBNull(oDataRow("M4")) Then
                    If oDataRow("M4") < 20.0 Then
                        dataItem.Add(oDataRow("M4"))
                    End If
                End If
                If Not IsDBNull(oDataRow("M5")) Then
                    If oDataRow("M5") < 20.0 Then
                        dataItem.Add(oDataRow("M5"))
                    End If
                End If
                If Not IsDBNull(oDataRow("M6")) Then
                    If oDataRow("M6") < 20.0 Then
                        dataItem.Add(oDataRow("M6"))
                    End If
                End If
                If Not IsDBNull(oDataRow("M7")) Then
                    If oDataRow("M7") < 20.0 Then
                        dataItem.Add(oDataRow("M7"))
                    End If
                End If
                If Not IsDBNull(oDataRow("M8")) Then
                    If oDataRow("M8") < 20.0 Then
                        dataItem.Add(oDataRow("M8"))
                    End If
                End If
                If Not IsDBNull(oDataRow("M9")) Then
                    If oDataRow("M9") < 20.0 Then
                        dataItem.Add(oDataRow("M9"))
                    End If
                End If
                If Not IsDBNull(oDataRow("M10")) Then
                    If oDataRow("M10") < 20.0 Then
                        dataItem.Add(oDataRow("M10"))
                    End If
                End If

                If IsDBNull(oDataRow("M11")) Then
                Else
                    If oDataRow("M11") < 20.0 Then
                        dataItem.Add(oDataRow("M11"))
                    End If
                End If

                If IsDBNull(oDataRow("M12")) Then
                Else
                    If oDataRow("M12") < 20.0 Then
                        dataItem.Add(oDataRow("M12"))
                    End If
                End If

                iData.Add(oDataRow("Descricao"))
                iData.Add(dataItem)


            Next


            Return iData

        End Function

        Protected Sub hfGridView1SV_ValueChanged(sender As Object, e As EventArgs)
            Session(hfGridView1SVID) = hfGridView1SV.Value
        End Sub

        Protected Sub hfGridView1SH_ValueChanged(sender As Object, e As EventArgs)
            Session(hfGridView1SHID) = hfGridView1SH.Value
        End Sub

    End Class




End Namespace