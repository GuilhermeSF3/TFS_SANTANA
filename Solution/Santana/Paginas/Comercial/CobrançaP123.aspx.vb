Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic
Imports Santana.Seguranca
Imports Util

Namespace Paginas.Comercial

    Public Class CobrançaP123
        Inherits SantanaPage

        Private _hfDataSerie1 As String = ""


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

                Carrega_Produto()
                Carrega_Faixa()

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

            Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + data.ToString("MM/yyyy"))

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


        Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
            If e.NewPageIndex >= 0 Then
                GridView1.PageIndex = e.NewPageIndex
                BindGridView1DataView()
            End If
        End Sub



        Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

            Try

                If e.Row.RowType = DataControlRowType.Header Then

                ElseIf e.Row.RowType = DataControlRowType.DataRow Then

                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row


                        If IsDBNull(drow("DT_FECHA")) Then
                            e.Row.Cells(0).Text = ""
                        Else
                            e.Row.Cells(0).Text = drow("DT_FECHA")
                        End If

                        If IsDBNull(drow("COD_OPERACAO")) Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = drow("COD_OPERACAO")
                        End If

                        If IsDBNull(drow("CPF_CNPJ")) Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = drow("CPF_CNPJ")
                        End If

                        If IsDBNull(drow("NOME_CLIENTE")) Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = drow("NOME_CLIENTE")
                        End If

                        If IsDBNull(drow("AGENTE")) Then
                            e.Row.Cells(4).Text = ""
                        Else
                            e.Row.Cells(4).Text = drow("AGENTE")
                        End If

                        If IsDBNull(drow("COD_PROMOTORA")) Then
                            e.Row.Cells(5).Text = ""
                        Else
                            e.Row.Cells(5).Text = drow("COD_PROMOTORA")
                        End If

                        If IsDBNull(drow("PROMOTORA")) Then
                            e.Row.Cells(6).Text = ""
                        Else
                            e.Row.Cells(6).Text = drow("PROMOTORA")
                        End If

                        If IsDBNull(drow("COD_LOJA")) Then
                            e.Row.Cells(7).Text = ""
                        Else
                            e.Row.Cells(7).Text = drow("COD_LOJA")
                        End If

                        If IsDBNull(drow("NOME_LOJA")) Then
                            e.Row.Cells(8).Text = ""
                        Else
                            e.Row.Cells(8).Text = drow("NOME_LOJA")
                        End If

                        If IsDBNull(drow("CARTEIRA")) Then
                            e.Row.Cells(9).Text = ""
                        Else
                            e.Row.Cells(9).Text = drow("CARTEIRA")
                        End If

                        If IsDBNull(drow("MODALIDADE")) Then
                            e.Row.Cells(10).Text = ""
                        Else
                            e.Row.Cells(10).Text = drow("MODALIDADE")
                        End If

                        If IsDBNull(drow("DATA_CONTRATO")) Then
                            e.Row.Cells(11).Text = ""
                        Else
                            e.Row.Cells(11).Text = drow("DATA_CONTRATO")
                        End If

                        If IsDBNull(drow("PRODUTO")) Then
                            e.Row.Cells(12).Text = ""
                        Else
                            e.Row.Cells(12).Text = drow("PRODUTO")
                        End If

                        If IsDBNull(drow("PLANO")) Then
                            e.Row.Cells(13).Text = ""
                        Else
                            e.Row.Cells(13).Text = drow("PLANO")
                        End If

                        If IsDBNull(drow("ATRASO")) Then
                            e.Row.Cells(14).Text = ""
                        Else
                            e.Row.Cells(14).Text = drow("ATRASO")
                        End If

                        If IsDBNull(drow("RATING")) Then
                            e.Row.Cells(15).Text = ""
                        Else
                            e.Row.Cells(15).Text = drow("RATING")
                        End If

                        If IsDBNull(drow("VLR_PARCELA")) Then
                            e.Row.Cells(16).Text = ""
                        Else
                            e.Row.Cells(16).Text = drow("VLR_PARCELA")
                        End If

                        If IsDBNull(drow("R$_ATRASO")) Then
                            e.Row.Cells(17).Text = ""
                        Else
                            e.Row.Cells(17).Text = drow("R$_ATRASO")
                        End If

                        If IsDBNull(drow("VLR_FINANCIADO")) Then
                            e.Row.Cells(18).Text = ""
                        Else
                            e.Row.Cells(18).Text = drow("VLR_FINANCIADO")
                        End If

                        If IsDBNull(drow("ENDERECO")) Then
                            e.Row.Cells(19).Text = ""
                        Else
                            e.Row.Cells(19).Text = drow("ENDERECO")
                        End If

                        If IsDBNull(drow("BAIRRO")) Then
                            e.Row.Cells(20).Text = ""
                        Else
                            e.Row.Cells(20).Text = drow("BAIRRO")
                        End If

                        If IsDBNull(drow("CIDADE")) Then
                            e.Row.Cells(21).Text = ""
                        Else
                            e.Row.Cells(21).Text = drow("CIDADE")
                        End If

                        If IsDBNull(drow("CEP")) Then
                            e.Row.Cells(22).Text = ""
                        Else
                            e.Row.Cells(22).Text = drow("CEP")
                        End If

                        If IsDBNull(drow("UF")) Then
                            e.Row.Cells(23).Text = ""
                        Else
                            e.Row.Cells(23).Text = drow("UF")
                        End If

                        If IsDBNull(drow("TELEFONE")) Then
                            e.Row.Cells(24).Text = ""
                        Else
                            e.Row.Cells(24).Text = drow("TELEFONE")
                        End If

                        If IsDBNull(drow("CELULAR")) Then
                            e.Row.Cells(25).Text = ""
                        Else
                            e.Row.Cells(25).Text = drow("CELULAR")
                        End If

                        If IsDBNull(drow("FONE_COMERCIAL")) Then
                            e.Row.Cells(26).Text = ""
                        Else
                            e.Row.Cells(26).Text = drow("FONE_COMERCIAL")
                        End If

                        If IsDBNull(drow("LOCAL_TRABALHO")) Then
                            e.Row.Cells(27).Text = ""
                        Else
                            e.Row.Cells(27).Text = drow("LOCAL_TRABALHO")
                        End If

                        If IsDBNull(drow("REFERENCIA1")) Then
                            e.Row.Cells(28).Text = ""
                        Else
                            e.Row.Cells(28).Text = drow("REFERENCIA1")
                        End If

                        If IsDBNull(drow("REFERENCIA2")) Then
                            e.Row.Cells(29).Text = ""
                        Else
                            e.Row.Cells(29).Text = drow("REFERENCIA2")
                        End If

                        If IsDBNull(drow("MARCA")) Then
                            e.Row.Cells(30).Text = ""
                        Else
                            e.Row.Cells(30).Text = drow("MARCA")
                        End If

                        If IsDBNull(drow("MODELO")) Then
                            e.Row.Cells(31).Text = ""
                        Else
                            e.Row.Cells(31).Text = drow("MODELO")
                        End If

                        If IsDBNull(drow("ANO_FABRIC")) Then
                            e.Row.Cells(32).Text = ""
                        Else
                            e.Row.Cells(32).Text = drow("ANO_FABRIC")
                        End If

                        If IsDBNull(drow("ANO_MODELO")) Then
                            e.Row.Cells(33).Text = ""
                        Else
                            e.Row.Cells(33).Text = drow("ANO_MODELO")
                        End If

                        If IsDBNull(drow("COR")) Then
                            e.Row.Cells(34).Text = ""
                        Else
                            e.Row.Cells(34).Text = drow("COR")
                        End If

                        If IsDBNull(drow("PLACA")) Then
                            e.Row.Cells(35).Text = ""
                        Else
                            e.Row.Cells(35).Text = drow("PLACA")
                        End If

                        If IsDBNull(drow("VALOR_DO_BEM")) Then
                            e.Row.Cells(36).Text = ""
                        Else
                            e.Row.Cells(36).Text = drow("VALOR_DO_BEM")
                        End If

                        If IsDBNull(drow("COD_COBRADORA")) Then
                            e.Row.Cells(37).Text = ""
                        Else
                            e.Row.Cells(37).Text = drow("COD_COBRADORA")
                        End If

                        If IsDBNull(drow("COBRADORA")) Then
                            e.Row.Cells(38).Text = ""
                        Else
                            e.Row.Cells(38).Text = drow("COBRADORA")
                        End If

                        If IsDBNull(drow("REGRA")) Then
                            e.Row.Cells(39).Text = ""
                        Else
                            e.Row.Cells(39).Text = drow("REGRA")
                        End If

                        If IsDBNull(drow("RENAVAM")) Then
                            e.Row.Cells(40).Text = ""
                        Else
                            e.Row.Cells(40).Text = drow("RENAVAM")
                        End If

                        If IsDBNull(drow("CHASSI")) Then
                            e.Row.Cells(41).Text = ""
                        Else
                            e.Row.Cells(41).Text = drow("CHASSI")
                        End If

                        If IsDBNull(drow("PROFISSAO")) Then
                            e.Row.Cells(42).Text = ""
                        Else
                            e.Row.Cells(42).Text = drow("PROFISSAO")
                        End If

                        If IsDBNull(drow("FAIXA")) Then
                            e.Row.Cells(43).Text = ""
                        Else
                            e.Row.Cells(43).Text = drow("FAIXA")
                        End If

                        If IsDBNull(drow("P1VENC")) Then
                            e.Row.Cells(44).Text = ""
                        Else
                            e.Row.Cells(44).Text = drow("P1VENC")
                        End If

                        If IsDBNull(drow("DT_P1PAGTO")) Then
                            e.Row.Cells(45).Text = ""
                        Else
                            e.Row.Cells(45).Text = drow("DT_P1PAGTO")
                        End If

                        If IsDBNull(drow("SITP1")) Then
                            e.Row.Cells(46).Text = ""
                        Else
                            e.Row.Cells(46).Text = drow("SITP1")
                        End If

                        If IsDBNull(drow("P2VENC")) Then
                            e.Row.Cells(47).Text = ""
                        Else
                            e.Row.Cells(47).Text = drow("P2VENC")
                        End If

                        If IsDBNull(drow("DT_P2PAGTO")) Then
                            e.Row.Cells(48).Text = ""
                        Else
                            e.Row.Cells(48).Text = drow("DT_P2PAGTO")
                        End If

                        If IsDBNull(drow("SITP2")) Then
                            e.Row.Cells(49).Text = ""
                        Else
                            e.Row.Cells(49).Text = drow("SITP2")
                        End If

                        If IsDBNull(drow("P3VENC")) Then
                            e.Row.Cells(50).Text = ""
                        Else
                            e.Row.Cells(50).Text = drow("P3VENC")
                        End If

                        If IsDBNull(drow("DT_P3PAGTO")) Then
                            e.Row.Cells(51).Text = ""
                        Else
                            e.Row.Cells(51).Text = drow("DT_P3PAGTO")
                        End If

                        If IsDBNull(drow("SITP3")) Then
                            e.Row.Cells(52).Text = ""
                        Else
                            e.Row.Cells(52).Text = drow("SITP3")
                        End If

                        If IsDBNull(drow("VEICULOS")) Then
                            e.Row.Cells(53).Text = ""
                        Else
                            e.Row.Cells(53).Text = drow("VEICULOS")
                        End If

                        If IsDBNull(drow("SALDO_INSCRITO")) Then
                            e.Row.Cells(54).Text = ""
                        Else
                            e.Row.Cells(54).Text = drow("SALDO_INSCRITO")
                        End If

                        If IsDBNull(drow("RISCO")) Then
                            e.Row.Cells(55).Text = ""
                        Else
                            e.Row.Cells(55).Text = drow("RISCO")
                        End If

                        If IsDBNull(drow("ANALISTA")) Then
                            e.Row.Cells(56).Text = ""
                        Else
                            e.Row.Cells(56).Text = drow("ANALISTA")
                        End If
                    End If
                End If


            Catch ex As Exception

            End Try
        End Sub



        Protected Sub BindGridView1Data()

            GridView1.DataSource = GetData()
            GridView1.DataBind()

        End Sub

        Protected Sub BindGridView1DataView()

            GridView1.DataSource = DataGridView
            GridView1.DataBind()

        End Sub


        Private Function GetData() As DataTable

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

            Dim table As DataTable = ClassBD.GetExibirGrid("SCR_COBRANCAP123 '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', " & ddlAgente.SelectedValue.PadLeft(6, "0") & ", '" & ddlProduto.SelectedValue.PadLeft(6, "0") & "', " & ddlFaixa.SelectedValue & ", " & IIf(chkEstoque.Checked = True, 1, 0), "CobrancaP123", strConn)

            Return table

        End Function


        Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "tmp", "Alerta('P123 Análise' ,'1o Momento = dia 30 do mes de Vencimento,     2o Momento = 30 dias após o Vencimento,    3o Momento = 60 dias após o Vencimento');", True)
        End Sub


        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../Menu.aspx")
        End Sub


        Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
            Try
                If chkEstoque.Checked = True And ddlAgente.SelectedValue = "99" Then
                    ScriptManager.RegisterStartupScript(Page, Me.GetType(), "tmp", "Alerta('Atenção!' ,'Para gerar estoque, favor selecionar o agente.');", True)
                Else
                    BindGridView1Data()
                End If

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
                GridView1.AllowPaging = False
                BindGridView1Data()
                ExportExcel(GridView1)

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

        Protected Sub txtData_TextChanged(sender As Object, e As EventArgs)

            GridView1.DataSource = Nothing
            GridView1.DataBind()

        End Sub


        Protected Sub GridView1_DataBound(sender As Object, e As EventArgs)
            Dim gridView As GridView = CType(sender, GridView)
            If Not IsNothing(gridView.DataSource) AndAlso CType(gridView.DataSource, System.Data.DataTable).Rows.Count > 0 Then
                Util.CWebPageUtil.PreencheListaDePaginas(gridView, CType(gridView.BottomPagerRow.Cells(0).FindControl("PagerPages"), DropDownList))
            End If
        End Sub


        Protected Sub ddlProduto_SelectedIndexChanged(sender As Object, e As EventArgs)

            GridView1.DataSource = Nothing
            GridView1.DataBind()

        End Sub
        Private Sub Carrega_Produto()
            Try

                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)
                Dim ds As New DataSet
                Dim dt As DataTable = Nothing

                Dim command As SqlCommand = New SqlCommand(
                "SELECT * FROM Ttipo_prod (NOLOCK) WHERE COD_PROD='V' AND SIGLA IS NOT NULL ORDER BY DESCR_TIPO", connection)

                command.Connection.Open()

                Dim ddlValues As SqlDataReader
                ddlValues = command.ExecuteReader()

                ddlProduto.DataSource = ddlValues
                ddlProduto.DataValueField = "cod_modalidade"
                ddlProduto.DataTextField = "descr_tipo"
                ddlProduto.DataBind()

                ddlProduto.Items.Insert(0, New ListItem("Todos", "0"))

                ddlProduto.SelectedIndex = 0

                ddlValues.Close()

                command.Connection.Close()
                command.Connection.Dispose()

                connection.Close()

            Catch ex As Exception
            Finally
                GC.Collect()
            End Try

        End Sub
        Private Sub Carrega_Agente()

            Try

                Dim objDataAgente = New DbAgente
                Dim codGerente As String

                If ContextoWeb.UsuarioLogado.Perfil = 8 Then
                    codGerente = ContextoWeb.UsuarioLogado.codGerente
                Else
                    codGerente = "99"
                End If

                If codGerente = "99" Then
                    objDataAgente.CarregarTodosRegistros(ddlAgente)

                    ddlAgente.Items.Insert(0, New ListItem("Todos", "99"))
                    ddlAgente.SelectedIndex = 0

                Else
                    Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                    Dim con As New SqlConnection(strConn)
                    Dim Vagente As String = ""

                    Dim cmd As New SqlCommand("Select O3DESCR, O3CODORG from CDCSANTANAMicroCredito..TORG3 (nolock) where O3codorg IN (" & codGerente & ")", con)

                    cmd.Connection.Open()

                    Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

                    While dr.Read()
                        Vagente = Trim(dr.GetString(0))
                        Dim AGENTE1 = New ListItem
                        AGENTE1.Value = Trim(dr.GetString(1))
                        AGENTE1.Text = Trim(Vagente)
                        ddlAgente.Items.Add(AGENTE1)
                    End While
                    dr.Close()
                    con.Close()
                End If


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub Carrega_Faixa()

            Try

                ddlFaixa.Items.Insert(0, New ListItem("Todas", "0"))
                ddlFaixa.Items.Insert(1, New ListItem("Parcela 1", "1"))
                ddlFaixa.Items.Insert(2, New ListItem("Parcela 2", "2"))
                ddlFaixa.Items.Insert(3, New ListItem("Parcela 3", "3"))

                ddlFaixa.SelectedIndex = 0


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Protected Sub ddlFaixa_SelectedIndexChanged(sender As Object, e As EventArgs)

            GridView1.DataSource = Nothing
            GridView1.DataBind()

        End Sub

        Protected Sub chkEstoque_OnCheckedChanged(sender As Object, e As EventArgs)
            If chkEstoque.Checked = "True" Then
                txtData.Enabled = False
                btnDataAnterior.Enabled = False
                btnProximaData.Enabled = False
            Else
                txtData.Enabled = True
                btnDataAnterior.Enabled = False
                btnProximaData.Enabled = False
            End If
        End Sub
        Private Sub ExportExcel(objGrid As GridView)


            Try

                If Not IsNothing(objGrid.HeaderRow) Then

                    Response.Clear()
                    Response.Buffer = True
                    Dim filename As String = String.Format("CobrançaP123_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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



                        Response.Write(String.Format("COBRANÇA P123"))


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
    End Class
End Namespace