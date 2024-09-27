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

    Public Class ParcelaVcto

        Inherits SantanaPage

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                Carrega_Modalidade()
                Carrega_Agente()

                Dim primeiroDiaMesCorrente As Date = Convert.ToDateTime("01/" + Now.Date.ToString("MM/yyyy"))
                txtDataDe.Text = primeiroDiaMesCorrente

                txtDataAte.Text = (DateTime.Now.AddDays(0)).ToString("dd/MM/yyyy")

                If ContextoWeb.DadosTransferencia.CodCobradora <> 0 Or ContextoWeb.DadosTransferencia.CodAgente <> 0 Then
                    BindGridView1Data()
                    ContextoWeb.DadosTransferencia.CodCobradora = 0
                End If

            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub

        Protected Sub btnDataAnterior1_Click(sender As Object, e As EventArgs)

            Dim diaAnterior As Date = Convert.ToDateTime(txtDataDe.Text)
            diaAnterior = diaAnterior.AddDays(-1)
            txtDataDe.Text = diaAnterior.ToString("dd/MM/yyyy")

        End Sub


        Protected Sub btnProximaData1_Click(sender As Object, e As EventArgs)

            Dim proximoDia As Date = Convert.ToDateTime(txtDataDe.Text)
            proximoDia = proximoDia.AddDays(+1)
            txtDataDe.Text = proximoDia.ToString("dd/MM/yyyy")

        End Sub


        Protected Sub btnDataAnterior2_Click(sender As Object, e As EventArgs)

            Dim diaAnterior As Date = Convert.ToDateTime(txtDataAte.Text)
            diaAnterior = diaAnterior.AddDays(-1)
            txtDataAte.Text = diaAnterior.ToString("dd/MM/yyyy")

        End Sub


        Protected Sub btnProximaData2_Click(sender As Object, e As EventArgs)

            Dim proximoDia As Date = Convert.ToDateTime(txtDataAte.Text)
            proximoDia = proximoDia.AddDays(+1)
            txtDataAte.Text = proximoDia.ToString("dd/MM/yyyy")


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

        Private Sub Carrega_Modalidade()
            Try

                ddlModa.Items.Insert(0, New ListItem("Todas", "99"))
                ddlModa.Items.Insert(1, New ListItem("Reneg", "0"))
                ddlModa.Items.Insert(2, New ListItem("Repac", "1"))

                ddlModa.SelectedIndex = 0


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub Carrega_Agente()
            Try
                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)
                Dim ds As New DataSet
                Dim dt As DataTable = Nothing

                Dim codGerente As String
                Dim Produto As Integer = ContextoWeb.UsuarioLogado.Produto

                If ContextoWeb.UsuarioLogado.Perfil = 8 Then
                    codGerente = ContextoWeb.UsuarioLogado.codGerente
                Else
                    codGerente = "99"
                End If

                Dim command As SqlCommand
                Dim ddlValues As SqlDataReader


                If codGerente = "99" Then
                    command = New SqlCommand(
                    "select  O3DESCR, O3CODORG FROM CDCSANTANAMicroCredito..TORG3 (NOLOCK)  order by O3DESCR", connection)

                    command.Connection.Open()

                    ddlValues = command.ExecuteReader()

                    ddlAgente.DataSource = ddlValues
                    ddlAgente.DataValueField = "O3CODORG"
                    ddlAgente.DataTextField = "O3DESCR"
                    ddlAgente.DataBind()
                    ddlAgente.Items.Insert(0, New ListItem("TODOS", "99"))
                    ddlAgente.SelectedIndex = 0

                Else
                    command = New SqlCommand(
                    "select  O3DESCR, O3CODORG FROM CDCSANTANAMicroCredito..TORG3 (NOLOCK)  where O3codorg IN( " & codGerente & ") order by O3DESCR", connection)

                    command.Connection.Open()

                    ddlValues = command.ExecuteReader()

                    ddlAgente.DataSource = ddlValues
                    ddlAgente.DataValueField = "O3CODORG"
                    ddlAgente.DataTextField = "O3DESCR"
                    ddlAgente.DataBind()
                    ddlAgente.SelectedIndex = 0


                End If


                ddlValues.Close()

                command.Connection.Close()
                command.Connection.Dispose()

                connection.Close()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub



        Private Sub GridViewRiscoAnalitico_RowCreated1(sender As Object, e As GridViewRowEventArgs) Handles GridViewRiscoAnalitico.RowCreated

        End Sub



        Private Sub GridViewRiscoAnalitico_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewRiscoAnalitico.RowDataBound

            Try

                If e.Row.RowType = DataControlRowType.DataRow Then

                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

                        If IsDBNull(drow("PANROPER")) Then
                            e.Row.Cells(0).Text = ""
                        Else
                            e.Row.Cells(0).Text = drow("PANROPER")
                        End If

                        If IsDBNull(drow("PADTVCTO")) Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = drow("PADTVCTO")
                        End If

                        If IsDBNull(drow("PANRPARC")) Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = drow("PANRPARC")
                        End If

                        If IsDBNull(drow("OPCODOP")) Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = drow("OPCODOP")
                        End If

                        If IsDBNull(drow("TONOMEOP")) Then
                            e.Row.Cells(4).Text = ""
                        Else
                            e.Row.Cells(4).Text = drow("TONOMEOP")
                        End If

                        If IsDBNull(drow("CLNOMECLI")) Then
                            e.Row.Cells(5).Text = ""
                        Else
                            e.Row.Cells(5).Text = drow("CLNOMECLI")
                        End If

                        If IsDBNull(drow("CLCGC")) Then
                            e.Row.Cells(6).Text = ""
                        Else
                            e.Row.Cells(6).Text = drow("CLCGC")
                        End If

                        If IsDBNull(drow("OPVLRPARC")) Then
                            e.Row.Cells(7).Text = ""
                        Else
                            e.Row.Cells(7).Text = drow("OPVLRPARC")
                        End If

                        If IsDBNull(drow("CELULAR")) Then
                            e.Row.Cells(8).Text = ""
                        Else
                            e.Row.Cells(8).Text = drow("CELULAR")
                        End If

                        If IsDBNull(drow("RESIDENCIAL")) Then
                            e.Row.Cells(9).Text = ""
                        Else
                            e.Row.Cells(9).Text = drow("RESIDENCIAL")
                        End If

                        If IsDBNull(drow("COMERCIAL")) Then
                            e.Row.Cells(10).Text = ""
                        Else
                            e.Row.Cells(10).Text = drow("COMERCIAL")
                        End If

                        If IsDBNull(drow("COD_AGENTE")) Then
                            e.Row.Cells(11).Text = ""
                        Else
                            e.Row.Cells(11).Text = drow("COD_AGENTE")
                        End If

                        If IsDBNull(drow("AGENTE")) Then
                            e.Row.Cells(12).Text = ""
                        Else
                            e.Row.Cells(12).Text = drow("AGENTE")
                        End If

                        If IsDBNull(drow("ATRASO")) Then
                            e.Row.Cells(12).Text = ""
                        Else
                            e.Row.Cells(13).Text = drow("ATRASO")
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


            table = Util.ClassBD.GetExibirGrid("[SCR_PARCELA_VCTO] '" & Right(txtDataDe.Text, 4) & Mid(txtDataDe.Text, 4, 2) & Left(txtDataDe.Text, 2) &
                                                                "', '" & Right(txtDataAte.Text, 4) & Mid(txtDataAte.Text, 4, 2) & Left(txtDataAte.Text, 2) &
                                                                "', '" & ddlModa.SelectedValue &
                                                                "', '" & ddlAgente.SelectedValue & "'", "PARC_VCTO", strConn)


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
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Parcelas por Vencimentos' ,'Relatório de Parcelas por Vencimento.');", True)
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
                    Dim filename As String = String.Format("PArcelasVencimento_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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
                        Response.Write("Relatório Parcelas por Vencimento - Período De: " & txtDataDe.Text & " Até: " & txtDataAte.Text)
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