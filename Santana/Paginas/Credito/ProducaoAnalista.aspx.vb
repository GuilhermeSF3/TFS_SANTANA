Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic
Imports Santana.Seguranca
Imports Util

Namespace Paginas.Credito

    Public Class ProducaoAnalista

        Inherits SantanaPage

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
                txtDataDe.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)

                txtDataAte.Text = (DateTime.Now.AddDays(-1)).ToString("dd/MM/yyyy")



                If ContextoWeb.DadosTransferencia.CodAnalista = 0 Then
                    Carrega_Analistas()
                Else
                    Carrega_Analistas()
                    ddlAnalista.SelectedIndex = ddlAnalista.Items.IndexOf(ddlAnalista.Items.FindByValue(ContextoWeb.DadosTransferencia.CodAnalista.ToString()))
                End If

                Carrega_Datas()

                If ContextoWeb.DadosTransferencia.CodAnalista <> 0 Then
                    BindGridView1Data()
                    ContextoWeb.DadosTransferencia.CodAnalista = 0
                End If

            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub


        Protected Sub btnDataAnterior1_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataDe.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(-1)

            ' após 1/8/2014 Ago usar ULTIMO DIA DO MES
            ' antes ultimo dia util

            If UltimoDiaMesAnterior.Year = Now.Date.Year Then
                If UltimoDiaMesAnterior.Month <= Now.Date.Month Then
                    txtDataDe.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
                End If
            ElseIf UltimoDiaMesAnterior.Year < Now.Date.Year Then
                txtDataDe.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
            End If

        End Sub


        Protected Sub btnProximaData1_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataDe.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(1)

            ' após 1/8/2014 Ago usar ULTIMO DIA DO MES
            ' antes ultimo dia util

            If UltimoDiaMesAnterior.Year = Now.Date.Year Then
                If UltimoDiaMesAnterior.Month <= Now.Date.Month Then
                    txtDataDe.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
                End If
            ElseIf UltimoDiaMesAnterior.Year < Now.Date.Year Then
                txtDataDe.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
            End If

        End Sub


        Protected Sub btnDataAnterior2_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnteriorDe As Date = Convert.ToDateTime(txtDataDe.Text)
            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataAte.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)

            If UltimoDiaMesAnterior.Year = Now.Date.Year Then
                If UltimoDiaMesAnterior.Date >= UltimoDiaMesAnteriorDe.Date Then
                    txtDataAte.Text = UltimoDiaMesAnterior
                End If
            ElseIf UltimoDiaMesAnterior.Year < Now.Date.Year Then
                txtDataAte.Text = UltimoDiaMesAnterior
            End If

        End Sub


        Protected Sub btnProximaData2_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataAte.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(1)

            txtDataAte.Text = UltimoDiaMesAnterior


        End Sub


        Private Function UltimoDiaUtilMesAnterior(Data As Date) As String

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Data.ToString("MM/yyyy"))

            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(1)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)

            ' após 1/8/2014 Ago usar ULTIMO DIA DO MES
            If UltimoDiaMesAnterior <= Convert.ToDateTime("01/aug/2014") Then

                ' ultimo dia util
                If (UltimoDiaMesAnterior.DayOfWeek = DayOfWeek.Sunday) Then
                    UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-2)
                ElseIf (UltimoDiaMesAnterior.DayOfWeek = DayOfWeek.Saturday) Then
                    UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)
                End If
            End If
            Return UltimoDiaMesAnterior.ToString("dd/MM/yyyy")

        End Function






        Private Sub Carrega_Analistas()

            Try

                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)
                Dim command As SqlCommand = New SqlCommand( _
                " SELECT USNOMEUSUC, USNOMEUSU FROM ACESSOCORP..TUSU (NOLOCK) WHERE USATIVO='S' AND USGRUPO1 IN ('04895','04896','04897') ORDER BY USNOMEUSUC", connection)

                command.Connection.Open()

                Dim ddlValues As SqlDataReader
                ddlValues = command.ExecuteReader()

                ddlAnalista.DataSource = ddlValues
                ddlAnalista.DataValueField = "USNOMEUSUC"
                ddlAnalista.DataTextField = "USNOMEUSUC"
                ddlAnalista.DataBind()

                ddlAnalista.Items.Insert(0, New ListItem("Todos", ""))
                ddlAnalista.Items.Insert(1, New ListItem("TOTAL", "TOTAL"))
                ddlAnalista.SelectedIndex = 0

                ddlValues.Close()
                command.Connection.Close()
                command.Connection.Dispose()
                connection.Close()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub Carrega_Datas()

            Try

                ddlDatas.Items.Insert(0, New ListItem("Data Base", "0"))        'Proposta
                ddlDatas.Items.Insert(1, New ListItem("Data Cadastro", "1"))    'Contrato

                ddlDatas.SelectedIndex = 0


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub



        Private Sub GridViewRiscoAnalitico_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewRiscoAnalitico.RowDataBound

            Try

                If e.Row.RowType = DataControlRowType.DataRow Then

                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

                        'If IsDBNull(drow("usuario")) Then
                        '    e.Row.Cells(0).Text = ""
                        'Else
                        '    e.Row.Cells(0).Text = drow("usuario")
                        'End If

                        If IsDBNull(drow("analista")) Then
                            e.Row.Cells(0).Text = ""
                        Else
                            e.Row.Cells(0).Text = drow("analista")
                        End If

                        If IsDBNull(drow("pendente")) Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = drow("pendente")
                        End If

                        If IsDBNull(drow("prc_pendente")) OrElse drow("prc_pendente") = 0 Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = CNumero.FormataNumero(drow("prc_pendente"), 2)
                        End If

                        If IsDBNull(drow("aprovada")) Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = drow("aprovada")
                        End If

                        If IsDBNull(drow("prc_aprovada")) OrElse drow("prc_aprovada") = 0 Then
                            e.Row.Cells(4).Text = ""
                        Else
                            e.Row.Cells(4).Text = CNumero.FormataNumero(drow("prc_aprovada"), 2)
                        End If

                        If IsDBNull(drow("reprovada")) Then
                            e.Row.Cells(5).Text = ""
                        Else
                            e.Row.Cells(5).Text = drow("reprovada")
                        End If

                        If IsDBNull(drow("prc_reprovada")) OrElse drow("prc_reprovada") = 0 Then
                            e.Row.Cells(6).Text = ""
                        Else
                            e.Row.Cells(6).Text = CNumero.FormataNumero(drow("prc_reprovada"), 2)
                        End If

                        If IsDBNull(drow("cancelada")) Then
                            e.Row.Cells(7).Text = ""
                        Else
                            e.Row.Cells(7).Text = drow("cancelada")
                        End If

                        If IsDBNull(drow("prc_cancelada")) OrElse drow("prc_cancelada") = 0 Then
                            e.Row.Cells(8).Text = ""
                        Else
                            e.Row.Cells(8).Text = CNumero.FormataNumero(drow("prc_cancelada"), 2)
                        End If

                        e.Row.Cells(9).Text = drow("analise")

                        If IsDBNull(drow("Total")) Then
                            e.Row.Cells(10).Text = ""
                        Else
                            e.Row.Cells(10).Text = drow("Total")
                        End If


                    End If
                End If


            Catch ex As Exception

            End Try
        End Sub



        Protected Sub GridViewRiscoAnalitico_RowCreated(sender As Object, e As GridViewRowEventArgs)
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

            GridViewRiscoAnalitico.DataSource = GetData()
            GridViewRiscoAnalitico.DataBind()
            GridViewRiscoAnalitico.AllowPaging = "True"
        End Sub

        Protected Sub BindGridView1DataView()

            GridViewRiscoAnalitico.DataSource = DataGridView
            GridViewRiscoAnalitico.DataBind()

        End Sub


        Private Function GetData() As DataTable

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim table As DataTable

            table = Util.ClassBD.GetExibirGrid("[SCR_credito_producao] '" & Right(txtDataDe.Text, 4) & Mid(txtDataDe.Text, 4, 2) & Left(txtDataDe.Text, 2) & _
                                                                "', '" & Right(txtDataAte.Text, 4) & Mid(txtDataAte.Text, 4, 2) & Left(txtDataAte.Text, 2) & _
                                                                "', '" & ddlAnalista.SelectedValue & _
                                                                "', '" & ddlDatas.SelectedValue & _
                                                                "'", "PRODUCAOANALISTA", strConn)

            Return table

        End Function


        Protected Sub GridViewRiscoAnalitico_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

            GridViewRiscoAnalitico.PageIndex = IIf(e.NewPageIndex < 0, 0, e.NewPageIndex)
            BindGridView1DataView()
        End Sub




        Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)


            'ContextoWeb.DadosTransferencia.CodAgente = ddlAnalista.SelectedValue
            'ContextoWeb.DadosTransferencia.Agente = ddlAnalista.SelectedItem.ToString()

            'ContextoWeb.DadosTransferencia.CodCobradora = ddlProduto.SelectedValue
            'ContextoWeb.DadosTransferencia.Cobradora = ddlProduto.SelectedItem.ToString()


            'Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

            'Dim ds As dsRollrateMensal
            'Dim cmd As New SqlCommand("[scr_RR_mensal] '" & Convert.ToDateTime(txtData.Text).ToString("MM/dd/yy") & "', '" & ddlAnalista.SelectedValue & "','" & ddlProduto.SelectedValue & "'")
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

        ' carregar Safra analitica
        Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
            Try

                BindGridView1Data()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try

        End Sub
        ' carregar Producao analitica
        Protected Sub btnCarregarProd_Click(sender As Object, e As EventArgs)
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
                GridViewRiscoAnalitico.AllowPaging = False
                BindGridView1Data()
                ExportExcel(GridViewRiscoAnalitico)

            Catch ex As Exception
                Throw ex
            End Try

        End Sub




        Private Sub ExportExcel(objGrid As GridView)


            Try

                If Not IsNothing(objGrid.HeaderRow) Then

                    Response.Clear()
                    Response.Buffer = True
                    Dim filename As String = String.Format("Producao_Analista_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename)
                    Response.ContentEncoding = System.Text.Encoding.Default
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.ms-excel"

                    Using sw As New StringWriter()

                        Dim hw As New HtmlTextWriter(sw)

                        objGrid.HeaderRow.BackColor = Color.White
                        For Each cell As TableCell In objGrid.HeaderRow.Cells
                            cell.CssClass = "GridviewScrollC3Header"
                        Next
                        For Each row As GridViewRow In objGrid.Rows
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

                        objGrid.RenderControl(hw)


                        Dim fi As FileInfo = New FileInfo(Server.MapPath("~/css/GridviewScroll.css"))
                        Dim sb As New System.Text.StringBuilder
                        Dim sr As StreamReader = fi.OpenText()
                        Do While sr.Peek() >= 0
                            sb.Append(sr.ReadLine())
                        Loop
                        sr.Close()

                        Dim style As String = "<html><head><style type='text/css'>" & sb.ToString() & "</style><head>"
                        Response.Write(style)
                        Response.Write("Relatório Produção Crédito     De: " & txtDataDe.Text & "  Até: " & txtDataAte.Text & " Analista : " & ddlAnalista.SelectedValue & " Datas (0=proposta, 1=cadastro) : " & ddlDatas.SelectedValue)
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


        Protected Sub GridViewRiscoAnalitico_DataBound(sender As Object, e As EventArgs)
            Dim gridView As GridView = CType(sender, GridView)
            If Not IsNothing(gridView.DataSource) AndAlso CType(gridView.DataSource, System.Data.DataTable).Rows.Count > 0 Then
                Util.CWebPageUtil.PreencheListaDePaginas(gridView, CType(gridView.BottomPagerRow.Cells(0).FindControl("PagerPages"), DropDownList))
            End If
        End Sub

        Protected Sub PagerPages_SelectedIndexChanged(sender As Object, e As EventArgs)

            GridViewRiscoAnalitico.DataSource = DataGridView()
            GridViewRiscoAnalitico.PageIndex = CType(sender, DropDownList).SelectedIndex
            GridViewRiscoAnalitico.DataBind()

        End Sub

    End Class
End Namespace