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

    Public Class Repactuacao

        Inherits SantanaPage

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
                txtDataDe.Text = Convert.ToDateTime("01/" + Now.Date.AddMonths(0).ToString("MM/yyyy"))

                txtDataAte.Text = (DateTime.Now.AddDays(-1)).ToString("dd/MM/yyyy")

                If ContextoWeb.DadosTransferencia.CodAgente = 0 Then
                    Carrega_Loja()
                Else
                    Carrega_Loja()
                    ddlLoja.SelectedIndex = ddlLoja.Items.IndexOf(ddlLoja.Items.FindByValue(ContextoWeb.DadosTransferencia.CodAgente.ToString()))
                End If

                If ContextoWeb.DadosTransferencia.CodCobradora <> 0 Or ContextoWeb.DadosTransferencia.CodAgente <> 0 Then
                    BindGridView1Data()
                    ContextoWeb.DadosTransferencia.CodCobradora = 0
                End If

            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub

        Protected Sub btnDataAnterior1_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataDe.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(-1)

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

            If UltimoDiaMesAnterior <= Convert.ToDateTime("01/aug/2014") Then

                If (UltimoDiaMesAnterior.DayOfWeek = DayOfWeek.Sunday) Then
                    UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-2)
                ElseIf (UltimoDiaMesAnterior.DayOfWeek = DayOfWeek.Saturday) Then
                    UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)
                End If
            End If
            Return UltimoDiaMesAnterior.ToString("dd/MM/yyyy")

        End Function

        Private Sub Carrega_Loja()

            Try

                Dim objDataAgente = New DbAgente
                Dim codGerente As String

                If ContextoWeb.UsuarioLogado.Perfil = 8 Then
                    codGerente = ContextoWeb.UsuarioLogado.codGerente
                Else
                    codGerente = "99"
                End If

                ddlLoja.Items.Insert(0, New ListItem("Todos", "99"))

                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim con As New SqlConnection(strConn)
                Dim Vagente As String = ""

                Dim cmd As New SqlCommand("Select O4DESCR, O4CODORG from CDCSANTANAMicroCredito..TORG4 (nolock) where O4codorg IN ('0')", con)

                cmd.Connection.Open()

                Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                While dr.Read()
                    Vagente = Trim(dr.GetString(0))
                    Dim AGENTE1 = New ListItem
                    AGENTE1.Value = Trim(dr.GetString(1))
                    AGENTE1.Text = Trim(Vagente)
                    ddlLoja.Items.Add(AGENTE1)
                End While
                dr.Close()
                con.Close()
                ddlLoja.SelectedIndex = 0


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
                        Dim col As Integer

                        col = 0
                        If IsDBNull(drow("CLIENTE_CLNOMECLI")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("CLIENTE_CLNOMECLI")
                        End If

                        col += 1
                        If IsDBNull(drow("CLIENTE_CLEMAIL")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("CLIENTE_CLEMAIL")
                        End If

                        col += 1
                        If IsDBNull(drow("CLIENTE_CLGC")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("CLIENTE_CLGC")
                        End If


                        col += 1
                        If IsDBNull(drow("CLIENTE_CLRG")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("CLIENTE_CLRG")
                        End If

                        col += 1
                        If IsDBNull(drow("AVALISTA1_CLNOMECLI")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("AVALISTA1_CLNOMECLI")
                        End If

                        col += 1
                        If IsDBNull(drow("AVALISTA1_CLCGC")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("AVALISTA1_CLCGC")
                        End If

                        col += 1
                        If IsDBNull(drow("AVALISTA1_CLRG")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("AVALISTA1_CLRG")
                        End If

                        col += 1
                        If IsDBNull(drow("BEMVEICULOUNICO_ABMODELO")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("BEMVEICULOUNICO_ABMODELO")
                        End If

                        col += 1
                        If IsDBNull(drow("BEMVEICULOUNICO_ABCOR")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("BEMVEICULOUNICO_ABCOR")
                        End If

                        col += 1
                        If IsDBNull(drow("BEMVEICULOUNICO_ABANOFAB")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("BEMVEICULOUNICO_ABANOFAB")
                        End If

                        col += 1
                        If IsDBNull(drow("BEMVEICULOUNICO_ABANOMOD")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("BEMVEICULOUNICO_ABANOMOD")
                        End If

                        col += 1
                        If IsDBNull(drow("BEMVEICULOUNICO_ABCHASSI")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("BEMVEICULOUNICO_ABCHASSI")
                        End If

                        col += 1
                        If IsDBNull(drow("BEMVEICULOUNICO_ABPLACA")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("BEMVEICULOUNICO_ABPLACA")
                        End If

                        col += 1
                        If IsDBNull(drow("BEMVEICULOUNICO_ABRENAVAM")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("BEMVEICULOUNICO_ABRENAVAM")
                        End If

                        col += 1
                        If IsDBNull(drow("PROPOSTA_PPNROPER")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("PROPOSTA_PPNROPER")
                        End If

                        col += 1
                        If IsDBNull(drow("PROPOSTA_PPVLRPARC")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            If drow("PROPOSTA_PPVLRPARC") = "Mantido Valor Original" Then
                                e.Row.Cells(col).Text = drow("PROPOSTA_PPVLRPARC")
                            Else
                                e.Row.Cells(col).Text = "R$" + CNumero.FormataNumero(drow("PROPOSTA_PPVLRPARC"), 2)
                            End If
                        End If

                        col += 1
                        If IsDBNull(drow("PROPOSTA_PPVLRFIN")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = "R$" + CNumero.FormataNumero(drow("PROPOSTA_PPVLRFIN"), 2)
                        End If

                        col += 1
                        If IsDBNull(drow("PROPOSTA_PPTXNETAM")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("PROPOSTA_PPTXNETAM") + "%"
                        End If

                        col += 1
                        If IsDBNull(drow("PROPOSTA_PPTXJRCL")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("PROPOSTA_PPTXJRCL") + "%"
                        End If

                        col += 1
                        If IsDBNull(drow("PROPOSTA_PPQTDDPARC")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("PROPOSTA_PPQTDDPARC")
                        End If

                        col += 1
                        If IsDBNull(drow("PROPOSTA_PPDT1VCTO")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("PROPOSTA_PPDT1VCTO")
                        End If

                        col += 1
                        If IsDBNull(drow("PROPOSTA_PPDTVCTO")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("PROPOSTA_PPDTVCTO")
                        End If

                        col += 1
                        If IsDBNull(drow("PROPOSTA_PPDIABASE")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("PROPOSTA_PPDIABASE")
                        End If

                        col += 1
                        If IsDBNull(drow("PROPOSTA_PPMESBASEEXT")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("PROPOSTA_PPMESBASEEXT")
                        End If

                        col += 1
                        If IsDBNull(drow("PROPOSTA_PPANOBASE")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("PROPOSTA_PPANOBASE")
                        End If

                        col += 1
                        If IsDBNull(drow("CLIENTE_CLNOMECLI")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("CLIENTE_CLNOMECLI")
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


            table = Util.ClassBD.GetExibirGrid("[SCR_BASE_REPACTUACAO] '" & Right(txtDataDe.Text, 4) & Mid(txtDataDe.Text, 4, 2) & Left(txtDataDe.Text, 2) &
                                                                "', '" & Right(txtDataAte.Text, 4) & Mid(txtDataAte.Text, 4, 2) & Left(txtDataAte.Text, 2) &
                                                                "', '" & ddlLoja.SelectedValue & "'", "Repactuacao", strConn)


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
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Base de Repactuacao' ,'Repactuados no período indicado. Cobradora que efetivou a Repactuacao. Demais dados do Função e do IP.');", True)
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
                ExportCSV(GridViewRiscoAnalitico)

            Catch ex As Exception
                Throw ex
            End Try

        End Sub




        Private Sub ExportCSV(objGrid As GridView)


            Try
                HttpContext.Current.Response.Clear()
                HttpContext.Current.Response.Buffer = True

                Dim filename As String = String.Format("Repactuacao{0}_{1}_{2}.csv", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())

                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + filename)
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default
                HttpContext.Current.Response.Charset = ""
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"



                Dim sb As New StringBuilder()

                For k As Integer = 0 To objGrid.Columns.Count - 2
                    If k < objGrid.Columns.Count - 2 Then
                        sb.Append("""" + objGrid.Columns(k).HeaderText.Trim + """" + ","c)
                    Else
                        sb.Append("""" + objGrid.Columns(k).HeaderText.Trim + """")
                    End If
                Next
                sb.Append(vbCr & vbLf)

                For i As Integer = 0 To objGrid.Rows.Count - 1
                    For k As Integer = 0 To objGrid.Columns.Count - 2
                        If k < objGrid.Columns.Count - 2 Then
                            sb.Append("""" + objGrid.Rows(i).Cells(k).Text.Trim + """" + ","c)
                        Else
                            sb.Append("""" + objGrid.Rows(i).Cells(k).Text.Trim + """")
                        End If
                    Next
                    sb.Append(vbCr & vbLf)
                Next



                Response.Output.Write(sb.ToString())
                HttpContext.Current.Response.Flush()
                HttpContext.Current.Response.SuppressContent = True
                HttpContext.Current.ApplicationInstance.CompleteRequest()

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