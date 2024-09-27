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

Public Class RrMensalCUBO
    Inherits SantanaPage

    Private Const hfGridView1SVID As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
    Private Const hfGridView1SHID As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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
                BindGridView1Data("M0_DESCR1")
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

        If UltimoDiaMesAnterior <= Convert.ToDateTime("01/aug/2014") Then

            If (UltimoDiaMesAnterior.DayOfWeek = DayOfWeek.Sunday) Then
                UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-2)
            ElseIf (UltimoDiaMesAnterior.DayOfWeek = DayOfWeek.Saturday) Then
                UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)
            End If
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
            "select cubo, Descr from Tcubo (nolock)  order by cubo", connection)

            command.Connection.Open()

            Dim ddlValues As SqlDataReader
            ddlValues = command.ExecuteReader()

            ddlAgente.DataSource = ddlValues
            ddlAgente.DataValueField = "cubo"
            ddlAgente.DataTextField = "DESCR"
            ddlAgente.DataBind()

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


                    If drow("M0_DESCR1") = "TOTAL" Or drow("M0_DESCR1") = "SUB-TOTAL" Then
                        Cor = Drawing.ColorTranslator.FromOle(&HCC0000)
                    End If

                    e.Row.Cells(0).Text = drow("DT_FECHA")
                    e.Row.Cells(0).ForeColor = Cor

                    e.Row.Cells(1).Text = drow("M0_DESCR1")
                    e.Row.Cells(1).ForeColor = Cor

                    e.Row.Cells(2).Text = If(drow("M0_CARTEIRA") = 0.0, "", CNumero.FormataNumero(drow("M0_CARTEIRA"), 2))
                    e.Row.Cells(2).ForeColor = Cor

                    e.Row.Cells(3).Text = If(drow("M0_PDD") = 0.0, "", CNumero.FormataNumero(drow("M0_PDD"), 2))
                    e.Row.Cells(3).ForeColor = Cor

                    e.Row.Cells(4).Text = If(drow("M0_PDD_PRC") = 0.0, "", CNumero.FormataNumero(drow("M0_PDD_PRC"), 2))
                    e.Row.Cells(4).ForeColor = Cor

                    e.Row.Cells(5).Text = If(drow("M1_PREJUIZO") = 0.0, "", CNumero.FormataNumero(drow("M1_PREJUIZO"), 2))
                    e.Row.Cells(5).ForeColor = Cor

                    e.Row.Cells(6).Text = If(drow("M1_DESPESA") = 0.0, "", CNumero.FormataNumero(drow("M1_DESPESA"), 2))
                    e.Row.Cells(6).ForeColor = Cor

                    e.Row.Cells(7).Text = drow("M1_DESCR1")
                    e.Row.Cells(7).ForeColor = Cor

                    e.Row.Cells(8).Text = If(drow("M1_CARTEIRA") = 0.0, "", CNumero.FormataNumero(drow("M1_CARTEIRA"), 2))
                    e.Row.Cells(8).ForeColor = Cor

                    e.Row.Cells(9).Text = If(drow("M1_PDD") = 0.0, "", CNumero.FormataNumero(drow("M1_PDD"), 2))
                    e.Row.Cells(9).ForeColor = Cor

                    e.Row.Cells(10).Text = If(drow("M1_PDD_PRC") = 0.0, "", CNumero.FormataNumero(drow("M1_PDD_PRC"), 2))
                    e.Row.Cells(10).ForeColor = Cor


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


    Protected Sub BindGridView1Data(ByVal strSortField As String)



        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

        GridView1.DataSource = Util.ClassBD.GetExibirGrid("[scr_CUBO_RRMENSAL] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', '" & ddlAgente.SelectedValue & "','" & strSortField & "'", "RRMENSALcubo", strConn)

        GridView1.DataBind()



    End Sub


    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        BindGridView1Data("M0_DESCR1")
    End Sub



    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs)


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
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('PDD Mensal' ,'Fechamento da PDD mensal, quebra por FILTRO selecionado na lista. Ordenação crescente: clique no cabeçalho da coluna. Descrescente: 2o. clique.');", True)
    End Sub


    Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
        Response.Redirect("Menu.aspx")
    End Sub


    Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
        Try

            BindGridView1Data("M0_DESCR1")

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
                Dim filename As String = String.Format("pdd_MENSAL_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
                Response.AddHeader("content-disposition", "attachment;filename=" + filename)
                Response.Charset = ""
                Response.ContentType = "application/vnd.ms-excel"

                Using sw As New StringWriter()

                    Dim hw As New HtmlTextWriter(sw)

                    GridView1.AllowPaging = False
                    BindGridView1Data("M0_DESCR1")

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
        Session(hfGridView1SVID) = hfGridView1SV.Value
    End Sub

    Protected Sub hfGridView1SH_ValueChanged(sender As Object, e As EventArgs)
        Session(hfGridView1SHID) = hfGridView1SH.Value
    End Sub

    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        Dim arrSortExpr() As String
        Dim i As Integer
        If e.SortExpression = "" Then Return
        BindGridView1Data(e.SortExpression)
        arrSortExpr = Split(e.SortExpression, " ")

        For i = 0 To GridView1.Columns().Count - 1
            If (GridView1.Columns(i).SortExpression = e.SortExpression) Then
                If arrSortExpr.Length = 1 Then
                    ReDim Preserve arrSortExpr(2)
                    arrSortExpr.SetValue("ASC", 1)
                End If
                If UCase(arrSortExpr(1)) = "ASC" Then
                    If UCase(arrSortExpr(1)) = "ASC" Then
                        arrSortExpr(1) = "DESC"
                    ElseIf UCase(arrSortExpr(1)) = "DESC" Then
                        arrSortExpr(1) = "ASC"
                    End If
                    GridView1.Columns(i).SortExpression = arrSortExpr(0) & " " & arrSortExpr(1)
                End If
                Exit For
            End If

        Next

    End Sub

End Class

