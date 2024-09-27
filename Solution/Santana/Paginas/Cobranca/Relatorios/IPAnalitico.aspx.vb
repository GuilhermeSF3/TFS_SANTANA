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

    Public Class IPAnalitico

        Inherits SantanaPage

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
                txtDataDe.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)

                txtDataAte.Text = (DateTime.Now.AddDays(-1)).ToString("dd/MM/yyyy")


                Carrega_Faixas()

                Carrega_recebido()
                cbRecebido.SelectedIndex = 0

                If ContextoWeb.DadosTransferencia.CodCobradora = 0 Then
                    Carrega_Cobradora()
                Else
                    Carrega_Cobradora()
                    ddlCobradora.SelectedIndex = ddlCobradora.Items.IndexOf(ddlCobradora.Items.FindByValue(ContextoWeb.DadosTransferencia.CodCobradora.ToString()))

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
        Private Sub Carrega_recebido()

            Try

                cbRecebido.Items.Insert(0, New ListItem("TODOS", "99"))
                cbRecebido.Items.Insert(1, New ListItem("NÃO", "0"))
                cbRecebido.Items.Insert(2, New ListItem("SIM", "1"))


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub


        Private Sub Carrega_Faixas()

            Try

                ddlFaixa.Items.Insert(0, New ListItem("TODOS", "0"))
                ddlFaixa.Items.Insert(1, New ListItem("6 a 15", "2"))
                ddlFaixa.Items.Insert(2, New ListItem("16 a 30", "3"))
                ddlFaixa.Items.Insert(3, New ListItem("31 a 60", "4"))
                ddlFaixa.Items.Insert(4, New ListItem("61 a 90", "5"))
                ddlFaixa.Items.Insert(5, New ListItem("91 a 120", "6"))
                ddlFaixa.Items.Insert(6, New ListItem("121 a 150", "7"))
                ddlFaixa.Items.Insert(7, New ListItem("151 a 180", "8"))
                ddlFaixa.Items.Insert(8, New ListItem("181 a 360", "9"))
                ddlFaixa.Items.Insert(9, New ListItem(">360", "99"))

                ddlFaixa.SelectedIndex = 0


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
                "select COCod, CODescr from TCobradora (nolock) where COAtivo=1 and COCodProduto = '" & Left(Produto, 1) & "' AND ( COCOD IN ( 699,691, 710,711, 722,723,730 ) OR COTELA =1) order by CODescr", connection)

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

        Private Sub GridViewRiscoAnalitico_RowCreated1(sender As Object, e As GridViewRowEventArgs) Handles GridViewRiscoAnalitico.RowCreated

        End Sub



        Private Sub GridViewRiscoAnalitico_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewRiscoAnalitico.RowDataBound

            Try

                If e.Row.RowType = DataControlRowType.DataRow Then

                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

                        e.Row.Cells(0).Text = drow("CONTRATO")
                        e.Row.Cells(1).Text = CNumero.FormataNumero(drow("PARCELA"), 0)
                        e.Row.Cells(2).Text = drow("DATA_REF")
                        e.Row.Cells(3).Text = drow("DATA_PAGAMENTO")
                        e.Row.Cells(4).Text = drow("RECEBIDO")
                        e.Row.Cells(5).Text = CNumero.FormataNumero(drow("VALORREC"), 2)
                        e.Row.Cells(6).Text = drow("CODCOBRADORA")
                        e.Row.Cells(7).Text = drow("NOME_CLIENTE")
                        e.Row.Cells(8).Text = drow("CARTEIRA")
                        e.Row.Cells(9).Text = drow("MODALIDADE")
                        e.Row.Cells(10).Text = drow("DATA_CONTRATO")
                        e.Row.Cells(11).Text = drow("PLANO")
                        e.Row.Cells(12).Text = drow("ATRASO")
                        e.Row.Cells(13).Text = drow("VENCIMENTO")
                        e.Row.Cells(14).Text = CNumero.FormataNumero(drow("VLR_PARCELA"), 2)
                        e.Row.Cells(15).Text = CNumero.FormataNumero(drow("QTD_PARC_ATRASO"), 0)
                        e.Row.Cells(16).Text = CNumero.FormataNumero(drow("VALOR_FINANCIADO"), 2)
                        e.Row.Cells(17).Text = CNumero.FormataNumero(drow("TAC"), 2)
                        e.Row.Cells(18).Text = CNumero.FormataNumero(drow("IOC"), 2)
                        e.Row.Cells(19).Text = drow("CPF_CNPJ")
                        e.Row.Cells(20).Text = drow("RG")
                        e.Row.Cells(21).Text = drow("ENDERECO")
                        e.Row.Cells(22).Text = drow("BAIRRO")
                        e.Row.Cells(23).Text = drow("CIDADE")
                        e.Row.Cells(24).Text = CNumero.FormataNumero(drow("CEP"), 0)
                        e.Row.Cells(25).Text = drow("UF")
                        e.Row.Cells(26).Text = drow("TELEFONE")
                        e.Row.Cells(27).Text = drow("CELULAR")
                        e.Row.Cells(28).Text = drow("FONE_COMERCIAL")
                        e.Row.Cells(29).Text = drow("LOCAL_TRABALHO")
                        e.Row.Cells(30).Text = CNumero.FormataNumero(drow("VALORBEM"), 0)
                        e.Row.Cells(31).Text = drow("MARCA")
                        e.Row.Cells(32).Text = drow("MODELO")
                        e.Row.Cells(33).Text = drow("ANO_FABRIC")
                        e.Row.Cells(34).Text = drow("ANO_MODELO")
                        e.Row.Cells(35).Text = drow("COBRADORA")
                        e.Row.Cells(36).Text = drow("REGRA")
                        e.Row.Cells(37).Text = drow("BLOQUEIO")
                        e.Row.Cells(38).Text = drow("DESBLOQUEIO")
                        e.Row.Cells(39).Text = drow("FAIXA")
                        e.Row.Cells(40).Text = CNumero.FormataNumero(drow("QTDPAGAR"), 0)
                        e.Row.Cells(41).Text = drow("PROFISSAO")
                        e.Row.Cells(42).Text = drow("CARGO")
                        e.Row.Cells(43).Text = CNumero.FormataNumero(drow("VLRPAGO"), 2)
                        e.Row.Cells(44).Text = drow("USUARIO")

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

            table = Util.ClassBD.GetExibirGrid("[SCR_IP_ANALITICO] '" & Right(txtDataDe.Text, 4) & Mid(txtDataDe.Text, 4, 2) & Left(txtDataDe.Text, 2) &
                                                                "', '" & Right(txtDataAte.Text, 4) & Mid(txtDataAte.Text, 4, 2) & Left(txtDataAte.Text, 2) &
                                                                "', '" & ddlCobradora.SelectedValue & "','" & cbRecebido.SelectedValue & "','" & ddlFaixa.SelectedValue & "' ", "IPANALITICO", strConn)
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
                    Dim filename As String = String.Format("IP_ANALITICO_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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
                        Response.Write("Relatório IP analítico")
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