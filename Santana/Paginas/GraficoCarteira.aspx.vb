Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports Util
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.IO
Imports System.Web
Imports System.Web.Security
Imports System.Drawing.Printing
Imports System.Data.Common
Imports System
Imports System.Configuration
Imports System.Drawing

Imports Santana.Seguranca

Public Class GraficoCarteria
    Inherits SantanaPage

    Private Const hfGridView1SVID As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
    Private Const hfGridView1SHID As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"
    Private hfTipoGrafico As String = "1"
    Private hfDataSerie As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        hfTipoGrafico = Request.QueryString("opcao").ToString()

        If Not IsPostBack Then

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
            txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)

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

            If ContextoWeb.DadosTransferencia.CodCobradora <> 0 Or ContextoWeb.DadosTransferencia.CodAgente <> 0 Then
                BindGridView1Data()
                ContextoWeb.DadosTransferencia.CodAgente = 0
                ContextoWeb.DadosTransferencia.CodCobradora = 0
            End If

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

        Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

    End Sub


    Protected Sub btnDataAnterior_Click(sender As Object, e As EventArgs)

        Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtData.Text)
        UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(-1)

        If UltimoDiaMesAnterior.Year = Now.Date.Year Then
            If UltimoDiaMesAnterior.Month <= Now.Date.Month Then
                txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
            End If
        ElseIf UltimoDiaMesAnterior.Year < Now.Date.Year Then
            txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
        End If

    End Sub


    Protected Sub btnProximaData_Click(sender As Object, e As EventArgs)

        Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtData.Text)
        UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(1)

        If UltimoDiaMesAnterior.Year = Now.Date.Year Then
            If UltimoDiaMesAnterior.Month <= Now.Date.Month Then
                txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
            End If
        ElseIf UltimoDiaMesAnterior.Year < Now.Date.Year Then
            txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
        End If

    End Sub


    Private Function UltimoDiaUtilMesAnterior(Data As Date) As String

        Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Data.ToString("MM/yyyy"))

        UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(1)
        UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)

        If (UltimoDiaMesAnterior.DayOfWeek = DayOfWeek.Sunday) Then
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-2)
        ElseIf (UltimoDiaMesAnterior.DayOfWeek = DayOfWeek.Saturday) Then
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)
        End If

        Return UltimoDiaMesAnterior.ToString("dd/MM/yyyy")

    End Function

    Private Sub Carrega_Agente()

        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim connection As New SqlConnection(strConn)
            Dim ds As New DataSet
            Dim dt As DataTable = Nothing

            Dim Produto As Integer = ContextoWeb.UsuarioLogado.Produto

            Dim command As SqlCommand = New SqlCommand( _
            "select AGCod, AGDescr from TAgente (nolock) where AGAtivo=1 and AGCodProduto = '" & Left(Produto, 1) & "' order by AGDescr", connection)

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
            Dim ds As New DataSet
            Dim dt As DataTable = Nothing

            Dim Produto As Integer = ContextoWeb.UsuarioLogado.Produto

            Dim command As SqlCommand = New SqlCommand( _
            "select COCod, CODescr from TCobradora (nolock) where COAtivo=1 and COCodProduto = '" & Left(Produto, 1) & "' order by CODescr", connection)

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

    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        Try
            Dim Cor As Drawing.Color

            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then

                    Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

                    If drow("FORMATO") = "V" Then
                        Cor = Drawing.ColorTranslator.FromOle(&HCC0000)
                    ElseIf drow("FORMATO") = "A" Then
                        Cor = Drawing.ColorTranslator.FromOle(&HCC)
                    Else
                        Cor = Drawing.ColorTranslator.FromOle(&H666666)
                    End If

                    e.Row.Cells(0).Text = drow("ANO")
                    e.Row.Cells(0).ForeColor = Cor

                    e.Row.Cells(1).Text = CNumero.FormataNumero(drow("M1"), 2)
                    e.Row.Cells(1).ForeColor = Cor

                    If IsDBNull(drow("M2")) Then
                        e.Row.Cells(2).Text = ""
                    Else
                        e.Row.Cells(2).Text = CNumero.FormataNumero(drow("M2"), 2)
                    End If
                    e.Row.Cells(2).ForeColor = Cor

                    If IsDBNull(drow("M3")) Then
                        e.Row.Cells(3).Text = ""
                    Else
                        e.Row.Cells(3).Text = CNumero.FormataNumero(drow("M3"), 2)
                    End If
                    e.Row.Cells(3).ForeColor = Cor

                    If IsDBNull(drow("M4")) Then
                        e.Row.Cells(4).Text = ""
                    Else
                        e.Row.Cells(4).Text = CNumero.FormataNumero(drow("M4"), 2)
                    End If
                    e.Row.Cells(4).ForeColor = Cor

                    If IsDBNull(drow("M5")) Then
                        e.Row.Cells(5).Text = ""
                    Else
                        e.Row.Cells(5).Text = CNumero.FormataNumero(drow("M5"), 2)
                    End If
                    e.Row.Cells(5).ForeColor = Cor

                    If IsDBNull(drow("M6")) Then
                        e.Row.Cells(6).Text = ""
                    Else
                        e.Row.Cells(6).Text = CNumero.FormataNumero(drow("M6"), 2)
                    End If
                    e.Row.Cells(6).ForeColor = Cor

                    If IsDBNull(drow("M7")) Then
                        e.Row.Cells(7).Text = ""
                    Else
                        e.Row.Cells(7).Text = CNumero.FormataNumero(drow("M7"), 2)
                    End If
                    e.Row.Cells(7).ForeColor = Cor

                    If IsDBNull(drow("M8")) Then
                        e.Row.Cells(8).Text = ""
                    Else
                        e.Row.Cells(8).Text = CNumero.FormataNumero(drow("M8"), 2)
                    End If
                    e.Row.Cells(8).ForeColor = Cor

                    If IsDBNull(drow("M9")) Then
                        e.Row.Cells(9).Text = ""
                    Else
                        e.Row.Cells(9).Text = CNumero.FormataNumero(drow("M9"), 2)
                    End If
                    e.Row.Cells(9).ForeColor = Cor

                    If IsDBNull(drow("M10")) Then
                        e.Row.Cells(10).Text = ""
                    Else
                        e.Row.Cells(10).Text = CNumero.FormataNumero(drow("M10"), 2)
                    End If
                    e.Row.Cells(10).ForeColor = Cor

                    If IsDBNull(drow("M11")) Then
                        e.Row.Cells(11).Text = ""
                    Else
                        e.Row.Cells(11).Text = CNumero.FormataNumero(drow("M11"), 2)
                    End If
                    e.Row.Cells(11).ForeColor = Cor

                    If IsDBNull(drow("M12")) Then
                        e.Row.Cells(12).Text = ""
                    Else
                        e.Row.Cells(12).Text = CNumero.FormataNumero(drow("M12"), 2)
                    End If
                    e.Row.Cells(12).ForeColor = Cor


                    hfDataSerie = hfDataSerie & "<vc:DataSeries LegendText=""" & drow("ANO") & """ RenderAs=""Line"" MarkerType=""Circle"" SelectionEnabled=""True"" LineThickness=""3"">"


                    hfDataSerie = hfDataSerie & "<vc:DataSeries.DataPoints> "
                    'hfDataSerie = hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" />"  mostra label valor nos pontos
                    hfDataSerie = hfDataSerie & "<vc:DataPoint  LabelEnabled=""True"" AxisXLabel=""Jan"" YValue=""" & Replace((drow("M1") / 1.0), ",", ".") & """/> "
                    If Not IsDBNull(drow("M2")) Then
                        hfDataSerie = hfDataSerie & "<vc:DataPoint  LabelEnabled=""True"" AxisXLabel=""Fev"" YValue=""" & Replace((drow("M2") / 1.0), ",", ".") & """/> "
                        ' hfDataSerie = hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" />"
                    End If

                    If Not IsDBNull(drow("M3")) Then
                        hfDataSerie = hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""Mar"" YValue=""" & Replace((drow("M3") / 1.0), ",", ".") & """/> "
                    End If

                    If Not IsDBNull(drow("M4")) Then
                        hfDataSerie = hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""Abr"" YValue=""" & Replace((drow("M4") / 1.0), ",", ".") & """/> "
                    End If

                    If Not IsDBNull(drow("M5")) Then
                        hfDataSerie = hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""Mai"" YValue=""" & Replace((drow("M5") / 1.0), ",", ".") & """/> "
                    End If

                    If Not IsDBNull(drow("M6")) Then
                        hfDataSerie = hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""Jun"" YValue=""" & Replace((drow("M6") / 1.0), ",", ".") & """/> "
                    End If
                    If Not IsDBNull(drow("M7")) Then
                        hfDataSerie = hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""Jul"" YValue=""" & Replace((drow("M7") / 1.0), ",", ".") & """/> "
                    End If
                    If Not IsDBNull(drow("M8")) Then
                        hfDataSerie = hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""Aug"" YValue=""" & Replace((drow("M8") / 1.0), ",", ".") & """/> "
                    End If
                    If Not IsDBNull(drow("M9")) Then
                        hfDataSerie = hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""Set"" YValue=""" & Replace((drow("M9") / 1.0), ",", ".") & """/> "
                    End If
                    If Not IsDBNull(drow("M10")) Then
                        hfDataSerie = hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""Out"" YValue=""" & Replace((drow("M10") / 1.0), ",", ".") & """/> "
                    End If
                    If Not IsDBNull(drow("M11")) Then
                        hfDataSerie = hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""Nov"" YValue=""" & Replace((drow("M11") / 1.0), ",", ".") & """/> "
                    End If
                    If Not IsDBNull(drow("M12")) Then
                        hfDataSerie = hfDataSerie & "<vc:DataPoint LabelEnabled=""True"" AxisXLabel=""Dez"" YValue=""" & Replace((drow("M12") / 1.0), ",", ".") & """/> "
                    End If

                    hfDataSerie = hfDataSerie & "</vc:DataSeries.DataPoints> "

                    hfDataSerie = hfDataSerie & "</vc:DataSeries> "



                End If
            End If


        Catch ex As Exception

        End Try
    End Sub

 
    Protected Sub BindGridView1Data()

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim strProcedure As String = "scr_grafico_cart0d"

        Select Case hfTipoGrafico
            Case "1"
                strProcedure = "scr_grafico_cart0d"
            Case "2"
                strProcedure = "scr_grafico_cart15d"
            Case "3"
                strProcedure = "scr_grafico_cart91d"
        End Select


        GridView1.DataSource = Util.ClassBD.GetExibirGrid(strProcedure & " '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', '" & ddlAgente.SelectedValue & "','" & ddlCobradora.SelectedValue & "'", "GraficoMensal", strConn)

        ' GridView1.DataSource = Util.ClassBD.GetExibirGrid(strProcedure & " '" & Convert.ToDateTime(txtData.Text).ToString("dd/MM/yy") & "', '" & ddlAgente.SelectedValue & "','" & ddlCobradora.SelectedValue & "'", "GraficoMensal", strConn)
        GridView1.DataBind()

    End Sub


    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        BindGridView1Data()
    End Sub



    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs)


        ContextoWeb.DadosTransferencia.DataReferencia = txtData.Text
        ContextoWeb.DadosTransferencia.Classe = e.CommandName
        ContextoWeb.DadosTransferencia.Linha = Convert.ToInt32(e.CommandArgument)

        ContextoWeb.DadosTransferencia.CodAgente = ddlAgente.SelectedValue
        ContextoWeb.DadosTransferencia.Agente = ddlAgente.SelectedItem.ToString()

        ContextoWeb.DadosTransferencia.CodCobradora = ddlCobradora.SelectedValue
        ContextoWeb.DadosTransferencia.Cobradora = ddlCobradora.SelectedItem.ToString()

        ContextoWeb.Navegacao.LinkPaginaDetalhe = Me.AppRelativeVirtualPath
        Response.Redirect("DetalheContratos.aspx")

    End Sub

    Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
    End Sub


    Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
    End Sub


    Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
        Response.Redirect("Menu.aspx")
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
    Protected Sub btnExcel_Click_teste(sender As Object, e As EventArgs)


        'Sub ExportarExcel(ByVal dgv As GridView, ByVal saveAsFile As String)

        Dim attachment As String = "attachment; filename=Veiculos.xls"

        Response.ClearContent()

        Response.AddHeader("content-disposition", attachment)

        Response.ContentType = "application/ms-excel"




        Dim sw As StringWriter = New StringWriter()

        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)

        Select Case hfTipoGrafico
            Case "1"
                Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "% Carteira 0 a 14d")
            Case "2"
                Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "% Carteira 15 a 90d")
            Case "3"
                Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "% Carteira 91 a 360d")
        End Select

        GridView1.RenderControl(htw)

        Response.Write(sw.ToString())

        Response.End()



        'Dim attachment As String = "attachment; filename=Contacts.xls"

        'Response.ClearContent()

        'Response.AddHeader("content-disposition", attachment)

        'Response.ContentType = "application/ms-excel"

        'Response.Write("Data de Referencia &nbsp;")

        'Dim sw As StringWriter = New StringWriter()

        'Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)

        'GridView1.RenderControl(htw)

        'Response.Write(sw.ToString())

        'Response.End()

    End Sub


    Protected Sub btnExcel_Click(sender As Object, e As EventArgs)
        If GridView1.Rows.Count.ToString + 1 < 65536 Then

            Dim tw As New StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()

            Response.Clear()
            ' Response.ContentEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1")
            ' Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250")
            Response.ContentEncoding = System.Text.Encoding.Default

            Response.ContentType = "application/vnd.ms-excel"
            Dim filename As String = String.Format("Carteira_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
            Response.AddHeader("content-disposition", "attachment;filename=" & filename & ".xls")


            Response.Charset = ""
            EnableViewState = False
            'EnableEventValidation = False



            Controls.Add(frm)
            frm.Controls.Add(GridView1)
            frm.RenderControl(hw)

            ' nao está funcionando queria incluir o grafico
            ' frm.Controls.Add(VisifireChart1)  ' VisifireChart1
            ' frm.RenderControl(hw)


            Select Case hfTipoGrafico
                Case "1"
                    Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "% Carteira 0 a 14d")
                Case "2"
                    Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "% Carteira 15 a 90d")
                Case "3"
                    Response.Write("Data de Referencia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtData.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & "% Carteira 91 a 360d")
            End Select

            Response.Write(tw.ToString())
            ' Response.Flush()
            Response.End()

        Else
            MsgBox(" planilha possui muitas linhas, não é possível exportar para o Excel")
        End If


        'Try

        '    If Not IsNothing(GridView1.HeaderRow) Then

        '        Response.Clear()
        '        Response.Buffer = True
        '        Dim filename As String = String.Format("Carteira_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
        '        Response.AddHeader("content-disposition", "attachment;filename=" + filename)
        '        Response.Charset = ""
        '        Response.ContentType = "application/vnd.ms-excel"

        '        Using sw As New StringWriter()

        '            Dim hw As New HtmlTextWriter(sw)

        '            GridView1.AllowPaging = False
        '            BindGridView1Data()

        '            GridView1.HeaderRow.BackColor = Color.White
        '            For Each cell As TableCell In GridView1.HeaderRow.Cells
        '                cell.CssClass = "GridviewScrollC3Header"
        '            Next
        '            For Each row As GridViewRow In GridView1.Rows
        '                row.BackColor = Color.White
        '                For Each cell As TableCell In row.Cells
        '                    If row.RowIndex Mod 2 = 0 Then
        '                        cell.CssClass = "GridviewScrollC3Item"
        '                    Else
        '                        cell.CssClass = "GridviewScrollC3Item2"
        '                    End If

        '                    Dim controls As New List(Of Control)()
        '                    For Each control As Control In cell.Controls
        '                        controls.Add(control)
        '                    Next

        '                    For Each control As Control In controls
        '                        Select Case control.GetType().Name
        '                            Case "HyperLink"
        '                                cell.Controls.Add(New Literal() With { _
        '                                 .Text = TryCast(control, HyperLink).Text _
        '                                })
        '                                Exit Select
        '                            Case "TextBox"
        '                                cell.Controls.Add(New Literal() With { _
        '                                 .Text = TryCast(control, TextBox).Text _
        '                                })
        '                                Exit Select
        '                            Case "LinkButton"
        '                                cell.Controls.Add(New Literal() With { _
        '                                 .Text = TryCast(control, LinkButton).Text _
        '                                })
        '                                Exit Select
        '                            Case "CheckBox"
        '                                cell.Controls.Add(New Literal() With { _
        '                                 .Text = TryCast(control, CheckBox).Text _
        '                                })
        '                                Exit Select
        '                            Case "RadioButton"
        '                                cell.Controls.Add(New Literal() With { _
        '                                 .Text = TryCast(control, RadioButton).Text _
        '                                })
        '                                Exit Select
        '                        End Select
        '                        cell.Controls.Remove(control)
        '                    Next
        '                Next
        '            Next

        '            GridView1.RenderControl(hw)

        '            Dim fi As FileInfo = New FileInfo(Server.MapPath("~/css/GridviewScroll.css"))
        '            Dim sb As New System.Text.StringBuilder
        '            Dim sr As StreamReader = fi.OpenText()
        '            Do While sr.Peek() >= 0
        '                sb.Append(sr.ReadLine())
        '            Loop
        '            sr.Close()

        '            Dim style As String = "<html><head><style type='text/css'>" & sb.ToString() & "</style><head>"
        '            Response.Write(style)
        '            Response.Output.Write(sw.ToString())
        '            HttpContext.Current.Response.Flush()
        '            HttpContext.Current.Response.SuppressContent = True
        '            HttpContext.Current.ApplicationInstance.CompleteRequest()
        '        End Using

        '    End If

        'Catch ex As Exception
        '    Throw ex
        'End Try

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


    Public Function GetXAML() As [String]
        Dim myXAML As String


        myXAML = "<vc:Chart  xmlns:vc=""clr-namespace:Visifire.Charts;assembly=SLVisifire.Charts"" AnimatedUpdate=""True"" Width=""800"" Height=""400"" Theme=""Theme1"" BorderBrush=""Gray"" IndicatorEnabled=""True"">"
        myXAML = myXAML & "<vc:Chart.Titles>"

        Select Case hfTipoGrafico
            Case "1"
                myXAML = myXAML & "<vc:Title Text=""" & "Carteira 0 a 14 d " & """/> "
            Case "2"
                myXAML = myXAML & "<vc:Title Text=""" & "Carteira 15 a 90 d " & """/> "
            Case "3"
                myXAML = myXAML & "<vc:Title Text=""" & "Carteira 91 a 360 d " & """/> "
        End Select



        'myXAML = myXAML + "<vc:AxisX Title=""" & "meses" & """/> "
        'myXAML = myXAML + "<vc:AxisY Title=""" & "% carteira" & " ValueFormatString=""" & "#0.##'%'" & """/> "
        

        myXAML = myXAML & "</vc:Chart.Titles>"

        myXAML = myXAML & "<vc:Chart.AxesX> "
        myXAML = myXAML & "<vc:Axis Padding=""2""/> "
        myXAML = myXAML & "</vc:Chart.AxesX> "

        myXAML = myXAML & "<vc:Chart.AxesY> "
        myXAML = myXAML & "<vc:Axis Title=""%""/> "
        Select Case hfTipoGrafico
            Case "1"
                myXAML = myXAML & "<vc:Axis AxisMinimum=""74"" /> "
            Case "2"
                myXAML = myXAML & "<vc:Axis AxisMinimum=""11"" /> "
            Case "3"
                myXAML = myXAML & "<vc:Axis AxisMinimum=""5"" /> "
        End Select
        myXAML = myXAML & "</vc:Chart.AxesY> "



        myXAML = myXAML & "<vc:Chart.Series> "


        myXAML = myXAML & hfDataSerie

        myXAML = myXAML & "</vc:Chart.Series> "


        myXAML = myXAML & "</vc:Chart>"


        Return myXAML

    End Function



    Private Sub Page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Dim xaml As [String] = GetXAML()

        If hfDataSerie <> "" Then
            Dim s As String = "vChart.setDataXml('" & xaml & "');" & "vChart.render(""VisifireChart1"");"
            Dim scriptManager__1 As ScriptManager = ScriptManager.GetCurrent(Page)
            ScriptManager.RegisterClientScriptBlock(TryCast(sender, Page), Me.[GetType](), "onClick", "<script language='JavaScript'> " & s & " </script>", False)
        End If
    End Sub
End Class

