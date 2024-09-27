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


Namespace Paginas.Formaliz

    Public Class RegistroGravame

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                Dim dtIni As String = (DateTime.Now.AddDays(-1)).ToString("dd/MM/yyyy")

                txtDataDe.Text = Convert.ToDateTime(dtIni)
                txtDataAte.Text = Convert.ToDateTime(dtIni)

                Carrega_Status()
                Carrega_UF()
                Carrega_Empresa()

                If ContextoWeb.DadosTransferencia.CodAgente <> 0 Then
                    BindGridView1Data()
                    ContextoWeb.DadosTransferencia.CodAgente = 0
                End If

                If Session(HfGridView1Svid) IsNot Nothing Then
                    hfGridView1SV.Value = DirectCast(Session(HfGridView1Svid), String)
                    Session.Remove(HfGridView1Svid)
                End If

                If Session(HfGridView1Shid) IsNot Nothing Then
                    hfGridView1SH.Value = DirectCast(Session(HfGridView1Shid), String)
                    Session.Remove(HfGridView1Shid)
                End If

            End If


            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)


        End Sub

        Protected Sub btnDataAnteriorDe_Click(sender As Object, e As EventArgs)


            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataDe.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)

            txtDataDe.Text = UltimoDiaMesAnterior

        End Sub

        Protected Sub btnDataAnteriorAte_Click(sender As Object, e As EventArgs)


            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataAte.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)

            txtDataAte.Text = UltimoDiaMesAnterior

        End Sub


        Protected Sub btnProximaDataDe_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataDe.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(1)

            txtDataDe.Text = UltimoDiaMesAnterior

        End Sub

        Protected Sub btnProximaDataAte_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataAte.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(1)

            txtDataAte.Text = UltimoDiaMesAnterior

        End Sub

        Private Sub Carrega_Status()
            Try

                ddlStatus.Items.Insert(0, New ListItem("Registrados", "1"))
                ddlStatus.Items.Insert(1, New ListItem("Não Registrados", "2"))

                ddlStatus.SelectedIndex = 0


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub Carrega_UF()
            Try

                ddlUF.Items.Insert(0, New ListItem("Todos", "99"))
                ddlUF.Items.Insert(1, New ListItem("SP", "SP"))
                ddlUF.Items.Insert(2, New ListItem("MG", "MG"))
                ddlUF.Items.Insert(3, New ListItem("PR", "PR"))
                ddlUF.Items.Insert(4, New ListItem("SC", "SC"))

                ddlUF.SelectedIndex = 0


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub Carrega_Empresa()
            Try

                ddlEmpresa.Items.Insert(0, New ListItem("Todos", "99"))
                ddlEmpresa.Items.Insert(1, New ListItem("Tecnobank", "TECNOBANK"))
                ddlEmpresa.Items.Insert(2, New ListItem("Vetera", "VETERA"))

                ddlEmpresa.SelectedIndex = 0


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub


        Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

            Try

                If e.Row.RowType = DataControlRowType.DataRow Then
                    Dim Bold As Boolean

                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

                        If IsDBNull(drow("DT_REF")) Then
                            e.Row.Cells(0).Text = ""
                        Else
                            e.Row.Cells(0).Text = drow("DT_REF")
                            e.Row.Cells(0).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("CNPJ")) Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = drow("CNPJ")
                            e.Row.Cells(1).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("UF_REGISTRO")) Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = drow("UF_REGISTRO")
                            e.Row.Cells(2).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("CHASSI")) Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = drow("CHASSI")
                            e.Row.Cells(3).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("GRAVAME")) Then
                            e.Row.Cells(4).Text = ""
                        Else
                            e.Row.Cells(4).Text = drow("GRAVAME")
                            e.Row.Cells(4).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("REMARCADO")) Then
                            e.Row.Cells(5).Text = ""
                        Else
                            e.Row.Cells(5).Text = drow("REMARCADO")
                            e.Row.Cells(5).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("OKM")) Then
                            e.Row.Cells(6).Text = ""
                        Else
                            e.Row.Cells(6).Text = drow("OKM")
                            e.Row.Cells(6).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("PLACA")) Then
                            e.Row.Cells(7).Text = ""
                        Else
                            e.Row.Cells(7).Text = drow("PLACA")
                            e.Row.Cells(7).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("UF_PLACA")) Then
                            e.Row.Cells(8).Text = ""
                        Else
                            e.Row.Cells(8).Text = drow("UF_PLACA")
                            e.Row.Cells(8).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("RENAVAM")) Then
                            e.Row.Cells(9).Text = ""
                        Else
                            e.Row.Cells(9).Text = drow("RENAVAM")
                            e.Row.Cells(9).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("TIPO")) Then
                            e.Row.Cells(10).Text = ""
                        Else
                            e.Row.Cells(10).Text = drow("TIPO")
                            e.Row.Cells(10).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("ANO_FABRICACAO")) Then
                            e.Row.Cells(11).Text = ""
                        Else
                            e.Row.Cells(11).Text = drow("ANO_FABRICACAO")
                            e.Row.Cells(11).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("ANO_MODELO")) Then
                            e.Row.Cells(12).Text = ""
                        Else
                            e.Row.Cells(12).Text = drow("ANO_MODELO")
                            e.Row.Cells(12).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("MARCA")) Then
                            e.Row.Cells(13).Text = ""
                        Else
                            e.Row.Cells(13).Text = drow("MARCA")
                            e.Row.Cells(13).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("MODELO")) Then
                            e.Row.Cells(14).Text = ""
                        Else
                            e.Row.Cells(14).Text = drow("MODELO")
                            e.Row.Cells(14).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("CPF_FINANCIADO")) Then
                            e.Row.Cells(15).Text = ""
                        Else
                            e.Row.Cells(15).Text = drow("CPF_FINANCIADO")
                            e.Row.Cells(15).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("NOME_FINANCIADO")) Then
                            e.Row.Cells(16).Text = ""
                        Else
                            e.Row.Cells(16).Text = drow("NOME_FINANCIADO")
                            e.Row.Cells(16).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("CEP")) Then
                            e.Row.Cells(17).Text = ""
                        Else
                            e.Row.Cells(17).Text = drow("CEP")
                            e.Row.Cells(17).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("LOGRADOURO")) Then
                            e.Row.Cells(18).Text = ""
                        Else
                            e.Row.Cells(18).Text = drow("LOGRADOURO")
                            e.Row.Cells(18).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("BAIRRO")) Then
                            e.Row.Cells(19).Text = ""
                        Else
                            e.Row.Cells(19).Text = drow("BAIRRO")
                            e.Row.Cells(19).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("UF")) Then
                            e.Row.Cells(20).Text = ""
                        Else
                            e.Row.Cells(20).Text = drow("UF")
                            e.Row.Cells(20).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("MUNICIPIO")) Then
                            e.Row.Cells(21).Text = ""
                        Else
                            e.Row.Cells(21).Text = drow("MUNICIPIO")
                            e.Row.Cells(21).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("NUMERO")) Then
                            e.Row.Cells(22).Text = ""
                        Else
                            e.Row.Cells(22).Text = drow("NUMERO")
                            e.Row.Cells(22).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("COMPLEMENTO")) Then
                            e.Row.Cells(23).Text = ""
                        Else
                            e.Row.Cells(23).Text = drow("COMPLEMENTO")
                            e.Row.Cells(23).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("DDD")) Then
                            e.Row.Cells(24).Text = ""
                        Else
                            e.Row.Cells(24).Text = drow("DDD")
                            e.Row.Cells(24).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("TELEFONE")) Then
                            e.Row.Cells(25).Text = ""
                        Else
                            e.Row.Cells(25).Text = drow("TELEFONE")
                            e.Row.Cells(25).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("EMAIL")) Then
                            e.Row.Cells(26).Text = ""
                        Else
                            e.Row.Cells(26).Text = drow("EMAIL")
                            e.Row.Cells(26).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("TIPO_RESTRICAO")) Then
                            e.Row.Cells(27).Text = ""
                        Else
                            e.Row.Cells(27).Text = drow("TIPO_RESTRICAO")
                            e.Row.Cells(27).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("TERCEIRO_GARANTIDOR")) Then
                            e.Row.Cells(28).Text = ""
                        Else
                            e.Row.Cells(28).Text = drow("TERCEIRO_GARANTIDOR")
                            e.Row.Cells(28).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("CONTRATO")) Then
                            e.Row.Cells(29).Text = ""
                        Else
                            e.Row.Cells(29).Text = drow("CONTRATO")
                            e.Row.Cells(29).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("DATA_CONTRATO")) Then
                            e.Row.Cells(30).Text = ""
                        Else
                            e.Row.Cells(30).Text = drow("DATA_CONTRATO")
                            e.Row.Cells(30).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("DATA_COMPRA")) Then
                            e.Row.Cells(31).Text = ""
                        Else
                            e.Row.Cells(31).Text = drow("DATA_COMPRA")
                            e.Row.Cells(31).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("QUANTIDADE_MESES")) Then
                            e.Row.Cells(32).Text = ""
                        Else
                            e.Row.Cells(32).Text = drow("QUANTIDADE_MESES")
                            e.Row.Cells(32).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("PRIMEIRO_VENCIMENTO")) Then
                            e.Row.Cells(33).Text = ""
                        Else
                            e.Row.Cells(33).Text = drow("PRIMEIRO_VENCIMENTO")
                            e.Row.Cells(33).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("ULTIMO_VENCIMENTO")) Then
                            e.Row.Cells(34).Text = ""
                        Else
                            e.Row.Cells(34).Text = drow("ULTIMO_VENCIMENTO")
                            e.Row.Cells(34).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("DATA_LIEBERACAO_CREDITO")) Then
                            e.Row.Cells(35).Text = ""
                        Else
                            e.Row.Cells(35).Text = drow("DATA_LIEBERACAO_CREDITO")
                            e.Row.Cells(35).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("VALOR_PARCELA")) Then
                            e.Row.Cells(36).Text = ""
                        Else
                            e.Row.Cells(36).Text = drow("VALOR_PARCELA")
                            e.Row.Cells(36).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("VALOR_DIVIDA")) Then
                            e.Row.Cells(37).Text = ""
                        Else
                            e.Row.Cells(37).Text = drow("VALOR_DIVIDA")
                            e.Row.Cells(37).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("VALOR_CREDITO")) Then
                            e.Row.Cells(38).Text = ""
                        Else
                            e.Row.Cells(38).Text = drow("VALOR_CREDITO")
                            e.Row.Cells(38).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("TAXA_CONTRATO")) Then
                            e.Row.Cells(39).Text = ""
                        Else
                            e.Row.Cells(39).Text = drow("TAXA_CONTRATO")
                            e.Row.Cells(39).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("IOF")) Then
                            e.Row.Cells(40).Text = ""
                        Else
                            e.Row.Cells(40).Text = drow("IOF")
                            e.Row.Cells(40).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("JUROS_MÊS")) Then
                            e.Row.Cells(41).Text = ""
                        Else
                            e.Row.Cells(41).Text = drow("JUROS_MÊS")
                            e.Row.Cells(41).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("JUROS_ANO")) Then
                            e.Row.Cells(42).Text = ""
                        Else
                            e.Row.Cells(42).Text = drow("JUROS_ANO")
                            e.Row.Cells(42).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("INDICATIVO_MULTA")) Then
                            e.Row.Cells(43).Text = ""
                        Else
                            e.Row.Cells(43).Text = drow("INDICATIVO_MULTA")
                            e.Row.Cells(43).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("TAXA_MULTA")) Then
                            e.Row.Cells(44).Text = ""
                        Else
                            e.Row.Cells(44).Text = drow("TAXA_MULTA")
                            e.Row.Cells(44).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("INDICATIVO_MORA_DIA")) Then
                            e.Row.Cells(45).Text = ""
                        Else
                            e.Row.Cells(45).Text = drow("INDICATIVO_MORA_DIA")
                            e.Row.Cells(45).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("TAXA_MORA")) Then
                            e.Row.Cells(46).Text = ""
                        Else
                            e.Row.Cells(46).Text = drow("TAXA_MORA")
                            e.Row.Cells(46).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("INDICATIVO_PENALIDADE")) Then
                            e.Row.Cells(47).Text = ""
                        Else
                            e.Row.Cells(47).Text = drow("INDICATIVO_PENALIDADE")
                            e.Row.Cells(47).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("DESCRICAO_PENALIDADE")) Then
                            e.Row.Cells(48).Text = ""
                        Else
                            e.Row.Cells(48).Text = drow("DESCRICAO_PENALIDADE")
                            e.Row.Cells(48).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("INDICATIVO_COMISSAO")) Then
                            e.Row.Cells(49).Text = ""
                        Else
                            e.Row.Cells(49).Text = drow("INDICATIVO_COMISSAO")
                            e.Row.Cells(49).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("COMISSAO")) Then
                            e.Row.Cells(50).Text = ""
                        Else
                            e.Row.Cells(50).Text = drow("COMISSAO")
                            e.Row.Cells(50).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("UF_CREDITO")) Then
                            e.Row.Cells(51).Text = ""
                        Else
                            e.Row.Cells(51).Text = drow("UF_CREDITO")
                            e.Row.Cells(51).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("CIDADE_CREDITO")) Then
                            e.Row.Cells(52).Text = ""
                        Else
                            e.Row.Cells(52).Text = drow("CIDADE_CREDITO")
                            e.Row.Cells(52).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("CORRECAO_MONETARIA")) Then
                            e.Row.Cells(53).Text = ""
                        Else
                            e.Row.Cells(53).Text = drow("CORRECAO_MONETARIA")
                            e.Row.Cells(53).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("OBSERVACAO")) Then
                            e.Row.Cells(54).Text = ""
                        Else
                            e.Row.Cells(54).Text = drow("OBSERVACAO")
                            e.Row.Cells(54).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("COR")) Then
                            e.Row.Cells(55).Text = ""
                        Else
                            e.Row.Cells(55).Text = drow("COR")
                            e.Row.Cells(55).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("VALOR_REGISTRO")) Then
                            e.Row.Cells(56).Text = ""
                        Else
                            e.Row.Cells(56).Text = drow("VALOR_REGISTRO")
                            e.Row.Cells(56).Font.Bold = Bold
                        End If

                        If IsDBNull(drow("EMPRESA")) Then
                            e.Row.Cells(57).Text = ""
                        Else
                            e.Row.Cells(57).Text = drow("EMPRESA")
                            e.Row.Cells(57).Font.Bold = Bold
                        End If

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


            GridView1.DataSource = GetData()

            GridView1.DataBind()
            GridView1.AllowPaging = "True"

        End Sub

        Protected Sub BindGridView1DataView()

            GridView1.DataSource = DataGridView
            GridView1.DataBind()

        End Sub


        Private Function GetData() As DataTable

            Dim oDataDe As New CDataHora(Convert.ToDateTime(txtDataDe.Text))
            Dim oDataAte As New CDataHora(Convert.ToDateTime(txtDataAte.Text))

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim table As DataTable

            table = Util.ClassBD.GetExibirGrid("SCR_CONSULTA_REGISTRO_GRAVAME '" &
                                               oDataDe.Data.ToString("yyyyMMdd") & "', '" &
                                               oDataAte.Data.ToString("yyyyMMdd") & "', " &
                                               ddlStatus.SelectedValue & ", '" &
                                               ddlUF.SelectedValue & "', '" &
                                               ddlEmpresa.SelectedValue & "', '" &
                                               txtContrato.Text.Trim() & "'", "REGISTRO_GRAVAME", strConn)
            



            Return table

        End Function




        Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

            GridView1.PageIndex = IIf(e.NewPageIndex < 0, 0, e.NewPageIndex)
            BindGridView1DataView()
        End Sub




        Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Registro Contrato' ,'Registro Contrato, Registro Contrato');", True)
        End Sub


        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../Menu.aspx")
        End Sub

        Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
            Try

                BindGridView1Data()
                txtQtde.Text = CType(GridView1.DataSource, System.Data.DataTable).Rows.Count

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
                GridView1.AllowPaging = False
                BindGridView1Data()
                ExportExcel(GridView1)

            Catch ex As Exception
                Throw ex
            End Try

        End Sub




        Private Sub ExportExcel(objGrid As GridView)


            Try

                If Not IsNothing(objGrid.HeaderRow) Then

                    Response.Clear()
                    Response.Buffer = True
                    Dim filename As String = String.Format("RegistroContrato_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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

                        Response.Write("Registro Contrato - Data DE: " & txtDataDe.Text & " </p> ")

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
            Session(HfGridView1Svid) = hfGridView1SV.Value
        End Sub

        Protected Sub hfGridView1SH_ValueChanged(sender As Object, e As EventArgs)
            Session(HfGridView1Shid) = hfGridView1SH.Value
        End Sub

        Protected Sub GridView1_DataBound(sender As Object, e As EventArgs)
            Dim gridView As GridView = CType(sender, GridView)
            If Not IsNothing(gridView.DataSource) AndAlso CType(gridView.DataSource, System.Data.DataTable).Rows.Count > 0 Then
                Util.CWebPageUtil.PreencheListaDePaginas(gridView, CType(gridView.BottomPagerRow.Cells(0).FindControl("PagerPages"), DropDownList))
            End If
        End Sub

        Protected Sub PagerPages_SelectedIndexChanged(sender As Object, e As EventArgs)

            GridView1.DataSource = DataGridView()
            GridView1.PageIndex = CType(sender, DropDownList).SelectedIndex
            GridView1.DataBind()

        End Sub

    End Class

End Namespace