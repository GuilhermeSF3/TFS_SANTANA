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

Namespace Paginas.Graficos

    Public Class Rolagem
        Inherits SantanaPage

        Private _hfTipoGrafico As String = "1"
        Private _hfDataSerie As String = ""


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            _hfTipoGrafico = Request.QueryString("opcao").ToString()

            If Not IsPostBack Then

                Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
                txtData.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)

                If ContextoWeb.DadosTransferencia.CodAgente = 0 Then
                    Carrega_Agente()
                Else
                    Carrega_Agente()
                    ddlAgente.SelectedIndex = ddlAgente.Items.IndexOf(ddlAgente.Items.FindByValue(ContextoWeb.DadosTransferencia.CodAgente.ToString()))
                End If


                If ContextoWeb.DadosTransferencia.CodCobradora = 0 Then
                    Carrega_Cobradora()
                Else
                    Carrega_Cobradora()
                    ddlCobradora.SelectedIndex = ddlCobradora.Items.IndexOf(ddlCobradora.Items.FindByValue(ContextoWeb.DadosTransferencia.CodCobradora.ToString()))
                End If

                Carrega_Faixa()

                If ContextoWeb.DadosTransferencia.CodCobradora <> 0 Or ContextoWeb.DadosTransferencia.CodAgente <> 0 Then
                    BindGridView1Data()
                    BindGridView2Data()
                    ContextoWeb.DadosTransferencia.CodAgente = 0
                    ContextoWeb.DadosTransferencia.CodCobradora = 0
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
            GridView1.DataSource = Nothing
            GridView1.DataBind()

            GridView2.DataSource = Nothing
            GridView2.DataBind()

            Return ultimoDiaMesAnterior.ToString("dd/MM/yyyy")

        End Function

        Private Sub Carrega_Agente()

            Try
                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)

                Dim produto As Integer = ContextoWeb.UsuarioLogado.Produto

                Dim command As SqlCommand = New SqlCommand( _
                    "select AGCod, AGDescr from TAgente (nolock) where AGAtivo=1 and AGCodProduto = '" & Left(produto, 1) & "' order by AGDescr", connection)

                command.Connection.Open()

                Dim ddlValues As SqlDataReader
                ddlValues = command.ExecuteReader()

                ddlAgente.DataSource = ddlValues
                ddlAgente.DataValueField = "AGCOD"
                ddlAgente.DataTextField = "AGDESCR"
                ddlAgente.DataBind()

                ddlAgente.Items.Insert(0, New ListItem("TODOS", "0"))
                ddlAgente.SelectedIndex = 0

                ddlValues.Close()

                command.Connection.Close()
                command.Connection.Dispose()

                connection.Close()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub Carrega_Cobradora()

            Try
                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)

                Dim produto As Integer = ContextoWeb.UsuarioLogado.Produto

                Dim command As SqlCommand = New SqlCommand( _
                    "select COCod, CODescr from TCobradora (nolock) where COAtivo=1 and COCodProduto = '" & Left(produto, 1) & "' order by CODescr", connection)

                command.Connection.Open()

                Dim ddlValues As SqlDataReader
                ddlValues = command.ExecuteReader()

                ddlCobradora.DataSource = ddlValues
                ddlCobradora.DataValueField = "COCOD"
                ddlCobradora.DataTextField = "CODESCR"
                ddlCobradora.DataBind()

                ddlCobradora.Items.Insert(0, New ListItem("TODOS", "0"))
                ddlCobradora.SelectedIndex = 0

                ddlValues.Close()

                command.Connection.Close()
                command.Connection.Dispose()

                connection.Close()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub Carrega_Faixa()

            Try

                If _hfTipoGrafico = "1" Then

                    ddlFaixa.Items.Insert(0, New ListItem("15 a 30 > 31 a 60 d", "1"))
                    ddlFaixa.Items.Insert(1, New ListItem("31 a 60 > 61 a 90 d", "2"))
                    ddlFaixa.Items.Insert(2, New ListItem("61 a 90 > 91 a 120 d", "3"))
                    ddlFaixa.Items.Insert(3, New ListItem("91 a 120 > 121 a 150 d", "4"))
                    ddlFaixa.Items.Insert(4, New ListItem("121 a 150 > 151 a 180 d", "5"))
                    ddlFaixa.Items.Insert(5, New ListItem("151 a 180 > 181 a 360 d", "6"))

                ElseIf _hfTipoGrafico = "2" Then
                    ddlFaixa.Items.Insert(0, New ListItem("Perda Máxima Estimada", "1"))
                End If



                ddlFaixa.SelectedIndex = 0


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

            Try
                Dim cor As Color

                If e.Row.RowType = DataControlRowType.DataRow Then
                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As DataRow = CType(e.Row.DataItem, DataRowView).Row

                        e.Row.Cells(0).Text = drow("ANO")
                        e.Row.Cells(0).ForeColor = cor

                        If IsDBNull(drow("M1")) Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = CNumero.FormataNumero(drow("M1"), 2)
                        End If
                        e.Row.Cells(1).ForeColor = cor

                        If IsDBNull(drow("M2")) Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = CNumero.FormataNumero(drow("M2"), 2)
                        End If
                        e.Row.Cells(2).ForeColor = cor

                        If IsDBNull(drow("M3")) Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = CNumero.FormataNumero(drow("M3"), 2)
                        End If
                        e.Row.Cells(3).ForeColor = cor

                        If IsDBNull(drow("M4")) Then
                            e.Row.Cells(4).Text = ""
                        Else
                            e.Row.Cells(4).Text = CNumero.FormataNumero(drow("M4"), 2)
                        End If
                        e.Row.Cells(4).ForeColor = cor

                        If IsDBNull(drow("M5")) Then
                            e.Row.Cells(5).Text = ""
                        Else
                            e.Row.Cells(5).Text = CNumero.FormataNumero(drow("M5"), 2)
                        End If
                        e.Row.Cells(5).ForeColor = cor

                        If IsDBNull(drow("M6")) Then
                            e.Row.Cells(6).Text = ""
                        Else
                            e.Row.Cells(6).Text = CNumero.FormataNumero(drow("M6"), 2)
                        End If
                        e.Row.Cells(6).ForeColor = cor

                        If IsDBNull(drow("M7")) Then
                            e.Row.Cells(7).Text = ""
                        Else
                            e.Row.Cells(7).Text = CNumero.FormataNumero(drow("M7"), 2)
                        End If
                        e.Row.Cells(7).ForeColor = cor

                        If IsDBNull(drow("M8")) Then
                            e.Row.Cells(8).Text = ""
                        Else
                            e.Row.Cells(8).Text = CNumero.FormataNumero(drow("M8"), 2)
                        End If
                        e.Row.Cells(8).ForeColor = cor

                        If IsDBNull(drow("M9")) Then
                            e.Row.Cells(9).Text = ""
                        Else
                            e.Row.Cells(9).Text = CNumero.FormataNumero(drow("M9"), 2)
                        End If
                        e.Row.Cells(9).ForeColor = cor

                        If IsDBNull(drow("M10")) Then
                            e.Row.Cells(10).Text = ""
                        Else
                            e.Row.Cells(10).Text = CNumero.FormataNumero(drow("M10"), 2)
                        End If
                        e.Row.Cells(10).ForeColor = cor

                        If IsDBNull(drow("M11")) Then
                            e.Row.Cells(11).Text = ""
                        Else
                            e.Row.Cells(11).Text = CNumero.FormataNumero(drow("M11"), 2)
                        End If
                        e.Row.Cells(11).ForeColor = cor

                        If IsDBNull(drow("M12")) Then
                            e.Row.Cells(12).Text = ""
                        Else
                            e.Row.Cells(12).Text = CNumero.FormataNumero(drow("M12"), 2)
                        End If
                        e.Row.Cells(12).ForeColor = cor


                        _hfDataSerie = _hfDataSerie & "<vc:DataSeries LegendText=""" & drow("ANO") & """ RenderAs=""Line"" MarkerType=""Circle"" SelectionEnabled=""True"" LineThickness=""3"">"


                        _hfDataSerie = _hfDataSerie & "<vc:DataSeries.DataPoints> "

                        If Not IsDBNull(drow("M1")) Then
                            _hfDataSerie = _hfDataSerie & "<vc:DataPoint  LabelEnabled=""True"" AxisXLabel=""Jan"" YValue=""" & Replace((drow("M1") / 1.0), ",", ".") & """/> "
                        End If
                        If Not IsDBNull(drow("M2")) Then
                            _hfDataSerie = _hfDataSerie & "<vc:DataPoint  LabelEnabled=""True"" AxisXLabel=""Fev"" YValue=""" & Replace((drow("M2") / 1.0), ",", ".") & """/> "
                        End If
                        If Not IsDBNull(drow("M3")) Then
                            _hfDataSerie = _hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""Mar"" YValue=""" & Replace((drow("M3") / 1.0), ",", ".") & """/> "
                        End If
                        If Not IsDBNull(drow("M4")) Then
                            _hfDataSerie = _hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""Abr"" YValue=""" & Replace((drow("M4") / 1.0), ",", ".") & """/> "
                        End If
                        If Not IsDBNull(drow("M5")) Then
                            _hfDataSerie = _hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""Mai"" YValue=""" & Replace((drow("M5") / 1.0), ",", ".") & """/> "
                        End If
                        If Not IsDBNull(drow("M6")) Then
                            _hfDataSerie = _hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""Jun"" YValue=""" & Replace((drow("M6") / 1.0), ",", ".") & """/> "
                        End If
                        If Not IsDBNull(drow("M7")) Then
                            _hfDataSerie = _hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""Jul"" YValue=""" & Replace((drow("M7") / 1.0), ",", ".") & """/> "
                        End If
                        If Not IsDBNull(drow("M8")) Then
                            _hfDataSerie = _hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""Aug"" YValue=""" & Replace((drow("M8") / 1.0), ",", ".") & """/> "
                        End If
                        If Not IsDBNull(drow("M9")) Then
                            _hfDataSerie = _hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""Set"" YValue=""" & Replace((drow("M9") / 1.0), ",", ".") & """/> "
                        End If
                        If Not IsDBNull(drow("M10")) Then
                            _hfDataSerie = _hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""Out"" YValue=""" & Replace((drow("M10") / 1.0), ",", ".") & """/> "
                        End If
                        If Not IsDBNull(drow("M11")) Then
                            _hfDataSerie = _hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""Nov"" YValue=""" & Replace((drow("M11") / 1.0), ",", ".") & """/> "
                        End If
                        If Not IsDBNull(drow("M12")) Then
                            _hfDataSerie = _hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""Dez"" YValue=""" & Replace((drow("M12") / 1.0), ",", ".") & """/> "
                        End If

                        _hfDataSerie = _hfDataSerie & "</vc:DataSeries.DataPoints> "

                        _hfDataSerie = _hfDataSerie & "</vc:DataSeries> "



                    End If
                End If


            Catch ex As Exception

            End Try
        End Sub



        Private Sub GridView2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView2.RowDataBound

            Try
                Dim cor As Color

                If e.Row.RowType = DataControlRowType.DataRow Then
                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As DataRow = CType(e.Row.DataItem, DataRowView).Row

                        e.Row.Cells(0).Text = drow("ANO")
                        e.Row.Cells(0).ForeColor = cor


                        If IsDBNull(drow("M1")) Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = CNumero.FormataNumero(drow("M1"), 0)
                        End If
                        e.Row.Cells(1).ForeColor = cor

                        If IsDBNull(drow("M2")) Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = CNumero.FormataNumero(drow("M2"), 0)
                        End If
                        e.Row.Cells(2).ForeColor = cor

                        If IsDBNull(drow("M3")) Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = CNumero.FormataNumero(drow("M3"), 0)
                        End If
                        e.Row.Cells(3).ForeColor = cor

                        If IsDBNull(drow("M4")) Then
                            e.Row.Cells(4).Text = ""
                        Else
                            e.Row.Cells(4).Text = CNumero.FormataNumero(drow("M4"), 0)
                        End If
                        e.Row.Cells(4).ForeColor = cor

                        If IsDBNull(drow("M5")) Then
                            e.Row.Cells(5).Text = ""
                        Else
                            e.Row.Cells(5).Text = CNumero.FormataNumero(drow("M5"), 0)
                        End If
                        e.Row.Cells(5).ForeColor = cor

                        If IsDBNull(drow("M6")) Then
                            e.Row.Cells(6).Text = ""
                        Else
                            e.Row.Cells(6).Text = CNumero.FormataNumero(drow("M6"), 0)
                        End If
                        e.Row.Cells(6).ForeColor = cor

                        If IsDBNull(drow("M7")) Then
                            e.Row.Cells(7).Text = ""
                        Else
                            e.Row.Cells(7).Text = CNumero.FormataNumero(drow("M7"), 0)
                        End If
                        e.Row.Cells(7).ForeColor = cor

                        If IsDBNull(drow("M8")) Then
                            e.Row.Cells(8).Text = ""
                        Else
                            e.Row.Cells(8).Text = CNumero.FormataNumero(drow("M8"), 0)
                        End If
                        e.Row.Cells(8).ForeColor = cor

                        If IsDBNull(drow("M9")) Then
                            e.Row.Cells(9).Text = ""
                        Else
                            e.Row.Cells(9).Text = CNumero.FormataNumero(drow("M9"), 0)
                        End If
                        e.Row.Cells(9).ForeColor = cor

                        If IsDBNull(drow("M10")) Then
                            e.Row.Cells(10).Text = ""
                        Else
                            e.Row.Cells(10).Text = CNumero.FormataNumero(drow("M10"), 0)
                        End If
                        e.Row.Cells(10).ForeColor = cor

                        If IsDBNull(drow("M11")) Then
                            e.Row.Cells(11).Text = ""
                        Else
                            e.Row.Cells(11).Text = CNumero.FormataNumero(drow("M11"), 0)
                        End If
                        e.Row.Cells(11).ForeColor = cor

                        If IsDBNull(drow("M12")) Then
                            e.Row.Cells(12).Text = ""
                        Else
                            e.Row.Cells(12).Text = CNumero.FormataNumero(drow("M12"), 0)
                        End If
                        e.Row.Cells(12).ForeColor = cor


                    End If
                End If


            Catch ex As Exception

            End Try
        End Sub


        Protected Sub BindGridView1Data()

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim strProcedure As String = "scr_grafico_rola"

            If _hfTipoGrafico = "1" Then
                strProcedure = "scr_grafico_rola 15,"
                Select Case ddlFaixa.SelectedValue
                    Case 1
                        strProcedure = "scr_grafico_rola 15, "
                    Case 2
                        strProcedure = "scr_grafico_rola 31, "
                    Case 3
                        strProcedure = "scr_grafico_rola 61, "
                    Case 4
                        strProcedure = "scr_grafico_rola 91, "
                    Case 5
                        strProcedure = "scr_grafico_rola 121, "
                    Case 6
                        strProcedure = "scr_grafico_rola 151, "
                End Select
            ElseIf _hfTipoGrafico = "2" Then
                strProcedure = "scr_grafico_perda"
            End If

            If _hfTipoGrafico = "1" Then
                GridView1.DataSource = ClassBD.GetExibirGrid(strProcedure & " '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', '" & ddlAgente.SelectedValue & "','" & ddlCobradora.SelectedValue & "'", "GraficoMensal", strConn)
            Else
                GridView1.DataSource = ClassBD.GetExibirGrid(strProcedure & " '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', '" & ddlAgente.SelectedValue & "','" & ddlCobradora.SelectedValue & "'", "GraficoMensal", strConn)
            End If

            GridView1.DataBind()

        End Sub


        Protected Sub BindGridView2Data()

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim strProcedure As String = "scr_vlr_rola15d"

            If _hfTipoGrafico = "1" Then
                Select Case ddlFaixa.SelectedValue
                    Case 1
                        strProcedure = "scr_vlr_rola15d"
                    Case 2
                        strProcedure = "scr_vlr_rola31d"
                    Case 3
                        strProcedure = "scr_vlr_rola61d"
                    Case 4
                        strProcedure = "scr_vlr_rola91d"
                    Case 5
                        strProcedure = "scr_vlr_rola121d"
                    Case 6
                        strProcedure = "scr_vlr_rola151d"
                End Select
            ElseIf _hfTipoGrafico = "2" Then
                strProcedure = "scr_vlr_perda"
            End If


            GridView2.DataSource = ClassBD.GetExibirGrid(strProcedure & " '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', '" & ddlAgente.SelectedValue & "','" & ddlCobradora.SelectedValue & "'", "GraficoMensal", strConn)

            GridView2.DataBind()

        End Sub


        Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
            GridView1.PageIndex = e.NewPageIndex
            BindGridView1Data()
        End Sub


        Protected Sub GridView2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
            GridView2.PageIndex = e.NewPageIndex
            BindGridView2Data()
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
            If GridView1.Rows.Count.ToString + 1 < 65536 Then

                Dim tw As New StringWriter()
                Dim hw As New HtmlTextWriter(tw)
                Dim frm As HtmlForm = New HtmlForm()

                Response.Clear()
                Response.ContentEncoding = System.Text.Encoding.Default

                Response.ContentType = "application/vnd.ms-excel"
                Dim filename As String = String.Format("Rolagem{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
                Response.AddHeader("content-disposition", "attachment;filename=" & filename & ".xls")


                Response.Charset = ""
                EnableViewState = False

                Controls.Add(frm)
                frm.Controls.Add(GridView1)
                frm.Controls.Add(GridView2)
                frm.RenderControl(hw)

                If _hfTipoGrafico = "1" Then
                    Select Case ddlFaixa.SelectedValue
                        Case 1
                            Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "Rolagens 15 a 30 > 31 a 60")
                        Case 2
                            Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "Rolagens 31 a 60 > 61 a 90")
                        Case 3
                            Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "Rolagens 61 a 90 > 91 a 120")
                        Case 4
                            Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "Rolagens 91 a 120 > 121 a 150")
                        Case 5
                            Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "Rolagens 121 a 150 > 151 a 180")
                        Case 6
                            Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "Rolagens 151 a 180 > 181 a 360")
                    End Select
                ElseIf _hfTipoGrafico = "2" Then
                    Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "Perda Máxima Estimada")
                End If

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


        Protected Sub GridView2_RowCreated(sender As Object, e As GridViewRowEventArgs)
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

                    tclCelula.Text = "R$ Mil"
                    tclCelula.ColumnSpan = 13
                    tclCelula.HorizontalAlign = HorizontalAlign.Center
                    gvrObjetoLinha.Cells.Add(tclCelula)
                    grvObjeto.Controls(0).Controls.AddAt(0, gvrObjetoLinha)

                End If

            Catch ex As Exception

            End Try
        End Sub

        Protected Sub ddlFaixa_SelectedIndexChanged(sender As Object, e As EventArgs)

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

        Protected Sub ddlCobradora_SelectedIndexChanged(sender As Object, e As EventArgs)

            GridView1.DataSource = Nothing
            GridView1.DataBind()

        End Sub

        Protected Sub ddlAgente_SelectedIndexChanged(sender As Object, e As EventArgs)

            GridView1.DataSource = Nothing
            GridView1.DataBind()

        End Sub

        <WebMethod()>
        Public Shared Function BindGraphData(ddlAgente As Integer, ddlCobradora As Integer, ddlFaixa As Integer, txtData As String, tipo As String) As List(Of Object)

            Dim iData As New List(Of Object)()
            Dim labels As New List(Of String)()
            Dim legendas As New List(Of String)()

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim strProcedure As String = "scr_grafico_rola"

            If Mid(tipo, Len(tipo)) = "1" Then
                strProcedure = "scr_grafico_rola 15, "
                Select Case ddlFaixa
                    Case 1
                        strProcedure = "scr_grafico_rola 15, "
                    Case 2
                        strProcedure = "scr_grafico_rola 31, "
                    Case 3
                        strProcedure = "scr_grafico_rola 61, "
                    Case 4
                        strProcedure = "scr_grafico_rola 91, "
                    Case 5
                        strProcedure = "scr_grafico_rola 121, "
                    Case 6
                        strProcedure = "scr_grafico_rola 151, "
                End Select
            ElseIf Mid(tipo, Len(tipo)) = "2" Then
                strProcedure = "scr_grafico_perda"
            End If


            Dim objData As DataTable = ClassBD.GetExibirGrid(strProcedure & " '" & Right(txtData, 4) & Mid(txtData, 4, 2) & Left(txtData, 2) & "', '" & ddlAgente & "','" & ddlCobradora & "'", "GraficoMensal", strConn)

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
                    dataItem.Add(oDataRow("M1"))
                End If
                If Not IsDBNull(oDataRow("M2")) Then
                    dataItem.Add(oDataRow("M2"))
                End If
                If Not IsDBNull(oDataRow("M3")) Then
                    dataItem.Add(oDataRow("M3"))
                End If
                If Not IsDBNull(oDataRow("M4")) Then
                    dataItem.Add(oDataRow("M4"))
                End If
                If Not IsDBNull(oDataRow("M5")) Then
                    dataItem.Add(oDataRow("M5"))
                End If
                If Not IsDBNull(oDataRow("M6")) Then
                    dataItem.Add(oDataRow("M6"))
                End If
                If Not IsDBNull(oDataRow("M7")) Then
                    dataItem.Add(oDataRow("M7"))
                End If
                If Not IsDBNull(oDataRow("M8")) Then
                    dataItem.Add(oDataRow("M8"))
                End If
                If Not IsDBNull(oDataRow("M9")) Then
                    dataItem.Add(oDataRow("M9"))
                End If
                If Not IsDBNull(oDataRow("M10")) Then
                    dataItem.Add(oDataRow("M10"))
                End If
                If Not IsDBNull(oDataRow("M11")) Then
                    dataItem.Add(oDataRow("M11"))
                End If
                If Not IsDBNull(oDataRow("M12")) Then
                    dataItem.Add(oDataRow("M12"))
                End If
                iData.Add(oDataRow("Ano"))
                iData.Add(dataItem)

            Next

            Return iData

        End Function
    End Class
End Namespace