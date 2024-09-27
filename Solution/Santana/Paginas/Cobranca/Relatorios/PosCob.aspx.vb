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

    Public Class PosCob

        Inherits SantanaPage

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                txtDataDe.Text = Convert.ToDateTime(Now.Date.AddDays(-1).ToString("dd/MM/yyyy"))

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

        Private Sub GridViewRiscoAnalitico_RowCreated1(sender As Object, e As GridViewRowEventArgs) Handles GridViewRiscoAnalitico.RowCreated

        End Sub



        Private Sub GridViewRiscoAnalitico_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewRiscoAnalitico.RowDataBound

            Try

                If e.Row.RowType = DataControlRowType.DataRow Then

                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

                        If IsDBNull(drow("CONTRATO")) Then
                            e.Row.Cells(0).Text = ""
                        Else
                            e.Row.Cells(0).Text = drow("CONTRATO")
                        End If

                        If IsDBNull(drow("CPF_CNPJ")) Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = drow("CPF_CNPJ")
                        End If

                        If IsDBNull(drow("NOME_CLIENTE")) Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = drow("NOME_CLIENTE")
                        End If

                        If IsDBNull(drow("AGENTE")) Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = drow("AGENTE")
                        End If

                        If IsDBNull(drow("CODPROMOTORA")) Then
                            e.Row.Cells(4).Text = ""
                        Else
                            e.Row.Cells(4).Text = drow("CODPROMOTORA")
                        End If

                        If IsDBNull(drow("PROMOTORA")) Then
                            e.Row.Cells(5).Text = ""
                        Else
                            e.Row.Cells(5).Text = drow("PROMOTORA")
                        End If

                        If IsDBNull(drow("LOJA")) Then
                            e.Row.Cells(6).Text = ""
                        Else
                            e.Row.Cells(6).Text = drow("LOJA")
                        End If

                        If IsDBNull(drow("CARTEIRA")) Then
                            e.Row.Cells(7).Text = ""
                        Else
                            e.Row.Cells(7).Text = drow("CARTEIRA")
                        End If

                        If IsDBNull(drow("MODALIDADE")) Then
                            e.Row.Cells(8).Text = ""
                        Else
                            e.Row.Cells(8).Text = drow("MODALIDADE")
                        End If

                        If IsDBNull(drow("DATA_CONTRATO")) Then
                            e.Row.Cells(9).Text = ""
                        Else
                            e.Row.Cells(9).Text = drow("DATA_CONTRATO")
                        End If

                        If IsDBNull(drow("PLANO")) Then
                            e.Row.Cells(10).Text = ""
                        Else
                            e.Row.Cells(10).Text = drow("PLANO")
                        End If

                        If IsDBNull(drow("PARCELA")) Then
                            e.Row.Cells(11).Text = ""
                        Else
                            e.Row.Cells(11).Text = drow("PARCELA")
                        End If

                        If IsDBNull(drow("PRI_VCTO")) Then
                            e.Row.Cells(12).Text = ""
                        Else
                            e.Row.Cells(12).Text = drow("PRI_VCTO")
                        End If

                        If IsDBNull(drow("ATRASO")) Then
                            e.Row.Cells(13).Text = ""
                        Else
                            e.Row.Cells(13).Text = drow("ATRASO")
                        End If

                        If IsDBNull(drow("RATING")) Then
                            e.Row.Cells(14).Text = ""
                        Else
                            e.Row.Cells(14).Text = drow("RATING")
                        End If

                        If IsDBNull(drow("SITUACAOBACEN")) Then
                            e.Row.Cells(15).Text = ""
                        Else
                            e.Row.Cells(15).Text = drow("SITUACAOBACEN")
                        End If

                        If IsDBNull(drow("VENCIMENTO")) Then
                            e.Row.Cells(16).Text = ""
                        Else
                            e.Row.Cells(16).Text = drow("VENCIMENTO")
                        End If

                        If IsDBNull(drow("VLR_PARCELA")) Then
                            e.Row.Cells(17).Text = ""
                        Else
                            e.Row.Cells(17).Text = drow("VLR_PARCELA")
                        End If

                        If IsDBNull(drow("QTD_PARC_ATRASO")) Then
                            e.Row.Cells(18).Text = ""
                        Else
                            e.Row.Cells(18).Text = drow("QTD_PARC_ATRASO")
                        End If

                        If IsDBNull(drow("VALOR_RISCO")) Then
                            e.Row.Cells(19).Text = ""
                        Else
                            e.Row.Cells(19).Text = drow("VALOR_RISCO")
                        End If

                        If IsDBNull(drow("VALOR_FINANCIADO")) Then
                            e.Row.Cells(20).Text = ""
                        Else
                            e.Row.Cells(20).Text = drow("VALOR_FINANCIADO")
                        End If

                        If IsDBNull(drow("ENDERECO")) Then
                            e.Row.Cells(21).Text = ""
                        Else
                            e.Row.Cells(21).Text = drow("ENDERECO")
                        End If

                        If IsDBNull(drow("BAIRRO")) Then
                            e.Row.Cells(22).Text = ""
                        Else
                            e.Row.Cells(22).Text = drow("BAIRRO")
                        End If

                        If IsDBNull(drow("CIDADE")) Then
                            e.Row.Cells(23).Text = ""
                        Else
                            e.Row.Cells(23).Text = drow("CIDADE")
                        End If

                        If IsDBNull(drow("CEP")) Then
                            e.Row.Cells(24).Text = ""
                        Else
                            e.Row.Cells(24).Text = drow("CEP")
                        End If

                        If IsDBNull(drow("UF")) Then
                            e.Row.Cells(25).Text = ""
                        Else
                            e.Row.Cells(25).Text = drow("UF")
                        End If

                        If IsDBNull(drow("TELEFONE")) Then
                            e.Row.Cells(26).Text = ""
                        Else
                            e.Row.Cells(26).Text = drow("TELEFONE")
                        End If

                        If IsDBNull(drow("CELULAR")) Then
                            e.Row.Cells(27).Text = ""
                        Else
                            e.Row.Cells(27).Text = drow("CELULAR")
                        End If

                        If IsDBNull(drow("FONE_COMERCIAL")) Then
                            e.Row.Cells(28).Text = ""
                        Else
                            e.Row.Cells(28).Text = drow("FONE_COMERCIAL")
                        End If

                        If IsDBNull(drow("LOCAL_TRABALHO")) Then
                            e.Row.Cells(29).Text = ""
                        Else
                            e.Row.Cells(29).Text = drow("LOCAL_TRABALHO")
                        End If

                        If IsDBNull(drow("REFERENCIA1")) Then
                            e.Row.Cells(30).Text = ""
                        Else
                            e.Row.Cells(30).Text = drow("REFERENCIA1")
                        End If

                        If IsDBNull(drow("REFERENCIA2")) Then
                            e.Row.Cells(31).Text = ""
                        Else
                            e.Row.Cells(31).Text = drow("REFERENCIA2")
                        End If

                        If IsDBNull(drow("MARCA")) Then
                            e.Row.Cells(32).Text = ""
                        Else
                            e.Row.Cells(32).Text = drow("MARCA")
                        End If

                        If IsDBNull(drow("MODELO")) Then
                            e.Row.Cells(33).Text = ""
                        Else
                            e.Row.Cells(33).Text = drow("MODELO")
                        End If

                        If IsDBNull(drow("ANO_FABRIC")) Then
                            e.Row.Cells(34).Text = ""
                        Else
                            e.Row.Cells(34).Text = drow("ANO_FABRIC")
                        End If

                        If IsDBNull(drow("ANO_MODELO")) Then
                            e.Row.Cells(35).Text = ""
                        Else
                            e.Row.Cells(35).Text = drow("ANO_MODELO")
                        End If

                        If IsDBNull(drow("COR")) Then
                            e.Row.Cells(36).Text = ""
                        Else
                            e.Row.Cells(36).Text = drow("COR")
                        End If

                        If IsDBNull(drow("PLACA")) Then
                            e.Row.Cells(37).Text = ""
                        Else
                            e.Row.Cells(37).Text = drow("PLACA")
                        End If

                        If IsDBNull(drow("VALORBEM")) Then
                            e.Row.Cells(38).Text = ""
                        Else
                            e.Row.Cells(38).Text = drow("VALORBEM")
                        End If

                        If IsDBNull(drow("COBRADORA")) Then
                            e.Row.Cells(39).Text = ""
                        Else
                            e.Row.Cells(39).Text = drow("COBRADORA")
                        End If

                        If IsDBNull(drow("REGRA")) Then
                            e.Row.Cells(40).Text = ""
                        Else
                            e.Row.Cells(40).Text = drow("REGRA")
                        End If

                        If IsDBNull(drow("BLOQUEIO")) Then
                            e.Row.Cells(41).Text = ""
                        Else
                            e.Row.Cells(41).Text = drow("BLOQUEIO")
                        End If

                        If IsDBNull(drow("DESBLOQUEIO")) Then
                            e.Row.Cells(42).Text = ""
                        Else
                            e.Row.Cells(42).Text = drow("DESBLOQUEIO")
                        End If

                        If IsDBNull(drow("RENAVAM")) Then
                            e.Row.Cells(43).Text = ""
                        Else
                            e.Row.Cells(43).Text = drow("RENAVAM")
                        End If

                        If IsDBNull(drow("CHASSI")) Then
                            e.Row.Cells(44).Text = ""
                        Else
                            e.Row.Cells(44).Text = drow("CHASSI")
                        End If

                        If IsDBNull(drow("PARCELAS_PAGAS")) Then
                            e.Row.Cells(45).Text = ""
                        Else
                            e.Row.Cells(45).Text = drow("PARCELAS_PAGAS")
                        End If

                        If IsDBNull(drow("PROFISSAO")) Then
                            e.Row.Cells(46).Text = ""
                        Else
                            e.Row.Cells(46).Text = drow("PROFISSAO")
                        End If

                        If IsDBNull(drow("CARGO")) Then
                            e.Row.Cells(47).Text = ""
                        Else
                            e.Row.Cells(47).Text = drow("CARGO")
                        End If

                        If IsDBNull(drow("FAIXA_RATING")) Then
                            e.Row.Cells(48).Text = ""
                        Else
                            e.Row.Cells(48).Text = drow("FAIXA_RATING")
                        End If

                        If IsDBNull(drow("FAIXA")) Then
                            e.Row.Cells(49).Text = ""
                        Else
                            e.Row.Cells(49).Text = drow("FAIXA")
                        End If

                        If IsDBNull(drow("GRUPO")) Then
                            e.Row.Cells(50).Text = ""
                        Else
                            e.Row.Cells(50).Text = drow("GRUPO")
                        End If

                        If IsDBNull(drow("SALDO_INSC")) Then
                            e.Row.Cells(51).Text = ""
                        Else
                            e.Row.Cells(51).Text = drow("SALDO_INSC")
                        End If

                        If IsDBNull(drow("VLR_ATRASO")) Then
                            e.Row.Cells(52).Text = ""
                        Else
                            e.Row.Cells(52).Text = drow("VLR_ATRASO")
                        End If

                        If IsDBNull(drow("P1_VCTO")) Then
                            e.Row.Cells(53).Text = ""
                        Else
                            e.Row.Cells(53).Text = drow("P1_VCTO")
                        End If

                        If IsDBNull(drow("P1_PGTO")) Then
                            e.Row.Cells(54).Text = ""
                        Else
                            e.Row.Cells(54).Text = drow("P1_PGTO")
                        End If

                        If IsDBNull(drow("P2_VCTO")) Then
                            e.Row.Cells(55).Text = ""
                        Else
                            e.Row.Cells(55).Text = drow("P2_VCTO")
                        End If

                        If IsDBNull(drow("P2_PGTO")) Then
                            e.Row.Cells(56).Text = ""
                        Else
                            e.Row.Cells(56).Text = drow("P2_PGTO")
                        End If

                        If IsDBNull(drow("P3_VCTO")) Then
                            e.Row.Cells(57).Text = ""
                        Else
                            e.Row.Cells(57).Text = drow("P3_VCTO")
                        End If

                        If IsDBNull(drow("P3_PGTO")) Then
                            e.Row.Cells(58).Text = ""
                        Else
                            e.Row.Cells(58).Text = drow("P3_PGTO")
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


            table = Util.ClassBD.GetExibirGrid("[SCR_POSICAO_COB] '" & Right(txtDataDe.Text, 4) & Mid(txtDataDe.Text, 4, 2) & Left(txtDataDe.Text, 2) & "'", "POSICAO_COB", strConn)


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
                    Dim filename As String = String.Format("PosicaoCob_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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
                        Response.Write("Posição Cobrança"
                                       )
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