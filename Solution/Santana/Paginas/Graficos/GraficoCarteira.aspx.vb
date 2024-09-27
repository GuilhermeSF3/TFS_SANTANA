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

    Public Class GraficoCarteria
        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"
        Private _hfDataSerie As String = ""


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

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

                ddlFaixa.Items.Insert(0, New ListItem("0 a 14 d", "1"))
                ddlFaixa.Items.Insert(1, New ListItem("15 a 90 d", "2"))
                ddlFaixa.Items.Insert(2, New ListItem("91 a 360 d", "3"))

                ddlFaixa.SelectedIndex = 0


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

            Try
                Dim cor As Drawing.Color

                If e.Row.RowType = DataControlRowType.DataRow Then
                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As DataRow = CType(e.Row.DataItem, DataRowView).Row

                        If drow("FORMATO") = "V" Then
                            cor = Drawing.ColorTranslator.FromOle(&HCC0000)
                        ElseIf drow("FORMATO") = "A" Then
                            cor = Drawing.ColorTranslator.FromOle(&HCC)
                        Else
                            cor = Drawing.ColorTranslator.FromOle(&H666666)
                        End If

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

                    End If
                End If


            Catch ex As Exception

            End Try
        End Sub


        Protected Sub BindGridView1Data()

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim strProcedure As String = "scr_grafico_cart0d"

            Select Case ddlFaixa.SelectedValue
                Case 1
                    strProcedure = "scr_grafico_cart0d"
                Case 2
                    strProcedure = "scr_grafico_cart15d"
                Case 3
                    strProcedure = "scr_grafico_cart91d"
            End Select


            GridView1.DataSource = ClassBD.GetExibirGrid(strProcedure & " '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', '" & ddlAgente.SelectedValue & "','" & ddlCobradora.SelectedValue & "'", "GraficoMensal", strConn)

            GridView1.DataBind()

        End Sub


        Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
            GridView1.PageIndex = e.NewPageIndex
            BindGridView1Data()
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

                _hfDataSerie = ""
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
        Protected Sub btnExcel_Click_teste(sender As Object, e As EventArgs)

            Dim attachment As String = "attachment; filename=Veiculos.xls"

            Response.ClearContent()

            Response.AddHeader("content-disposition", attachment)

            Response.ContentType = "application/ms-excel"




            Dim sw As StringWriter = New StringWriter()

            Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)

            Select Case ddlFaixa.SelectedValue
                Case 1
                    Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "% Carteira 0 a 14d")
                Case 2
                    Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "% Carteira 15 a 90d")
                Case 3
                    Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "% Carteira 91 a 360d")
            End Select

            GridView1.RenderControl(htw)

            Response.Write(sw.ToString())

            Response.End()

        End Sub


        Protected Sub btnExcel_Click(sender As Object, e As EventArgs)
            If GridView1.Rows.Count.ToString + 1 < 65536 Then

                Dim tw As New StringWriter()
                Dim hw As New HtmlTextWriter(tw)
                Dim frm As HtmlForm = New HtmlForm()

                Response.Clear()
                Response.ContentEncoding = Encoding.Default

                Response.ContentType = "application/vnd.ms-excel"
                Dim filename As String = String.Format("Carteira_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
                Response.AddHeader("content-disposition", "attachment;filename=" & filename & ".xls")


                Response.Charset = ""
                EnableViewState = False


                Controls.Add(frm)
                frm.Controls.Add(GridView1)
                frm.RenderControl(hw)

                Select Case ddlFaixa.SelectedValue
                    Case 1
                        Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "% Carteira 0 a 14d")
                    Case 2
                        Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "% Carteira 15 a 90d")
                    Case 3
                        Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "% Carteira 91 a 360d")
                End Select

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

        Protected Sub ddlFaixa_SelectedIndexChanged(sender As Object, e As EventArgs)

            GridView1.DataSource = Nothing
            GridView1.DataBind()

        End Sub

        Protected Sub ddlCobradora_SelectedIndexChanged(sender As Object, e As EventArgs)

            GridView1.DataSource = Nothing
            GridView1.DataBind()

        End Sub

        Protected Sub ddlAgente_SelectedIndexChanged(sender As Object, e As EventArgs)

            GridView1.DataSource = Nothing
            GridView1.DataBind()

        End Sub
        Protected Sub txtData_TextChanged(sender As Object, e As EventArgs)

            GridView1.DataSource = Nothing
            GridView1.DataBind()

        End Sub

        <WebMethod()>
        Public Shared Function BindGraphData(ddlAgente As Integer, ddlCobradora As Integer, ddlFaixa As Integer, txtData As String) As List(Of Object)


            Dim iData As New List(Of Object)()
            Dim labels As New List(Of String)()
            Dim legendas As New List(Of String)()

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

            Dim oData As New CDataHora()
            oData.Data = Convert.ToDateTime(txtData)

            Dim strProcedure As String = "scr_grafico_cart0d"

            Select Case ddlFaixa
                Case 1
                    strProcedure = "scr_grafico_cart0d"
                Case 2
                    strProcedure = "scr_grafico_cart15d"
                Case 3
                    strProcedure = "scr_grafico_cart91d"
            End Select

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