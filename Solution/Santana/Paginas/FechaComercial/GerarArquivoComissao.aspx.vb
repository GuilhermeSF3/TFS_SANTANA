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

Public Class GerarArquivoComissao

    Inherits SantanaPage

    Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
    Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            txtDataLancamento.Text = DateTime.Now.ToString("01/MM/yyyy")
            txtDataVencimento.Text = DateTime.Now.ToString("01/MM/yyyy")
            txtDataCompetencia.Text = DateTime.Now.ToString("01/MM/yyyy")
            txtDataEmissaoNF.Text = DateTime.Now.ToString("01/MM/yyyy")

            Carrega_Pagamentos()

        End If

        Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

    End Sub

    Private Sub Carrega_Pagamentos()
        Try

            ddlPagamento.Items.Insert(0, New ListItem("Selecione...", "0"))
            ddlPagamento.Items.Insert(1, New ListItem("Veículos", "1"))
            'ddlPagamento.Items.Insert(2, New ListItem("Prestamista", "2"))

            ddlPagamento.SelectedIndex = 0

        Catch ex As Exception

        Finally
            GC.Collect()
        End Try
    End Sub

    Protected Sub btnDataAnterior_Click(sender As Object, e As EventArgs)

        Dim btnID As String = sender.ID

        Select Case btnID
            Case "btnDataAnteriorLancamento"
                Dim diaAnterior As Date = Convert.ToDateTime(txtDataLancamento.Text)
                diaAnterior = diaAnterior.AddDays(-1)
                txtDataLancamento.Text = diaAnterior.ToString("dd/MM/yyyy")
            Case "btnDataAnteriorVencimento"
                Dim diaAnterior As Date = Convert.ToDateTime(txtDataVencimento.Text)
                diaAnterior = diaAnterior.AddDays(-1)
                txtDataVencimento.Text = diaAnterior.ToString("dd/MM/yyyy")
            Case "btnDataAnteriorCompetencia"
                Dim diaAnterior As Date = Convert.ToDateTime(txtDataCompetencia.Text)
                diaAnterior = diaAnterior.AddDays(-1)
                txtDataCompetencia.Text = diaAnterior.ToString("dd/MM/yyyy")
            Case "btnDataAnteriorEmissaoNF"
                Dim diaAnterior As Date = Convert.ToDateTime(txtDataEmissaoNF.Text)
                diaAnterior = diaAnterior.AddDays(-1)
                txtDataEmissaoNF.Text = diaAnterior.ToString("dd/MM/yyyy")
        End Select

    End Sub

    Protected Sub btnProximaData_Click(sender As Object, e As EventArgs)

        Dim btnID As String = sender.ID

        Select Case btnID
            Case "btnProximaDataLancamento"
                Dim proximoDia As Date = Convert.ToDateTime(txtDataLancamento.Text)
                proximoDia = proximoDia.AddDays(+1)
                txtDataLancamento.Text = proximoDia.ToString("dd/MM/yyyy")
            Case "btnProximaDataVencimento"
                Dim proximoDia As Date = Convert.ToDateTime(txtDataVencimento.Text)
                proximoDia = proximoDia.AddDays(+1)
                txtDataVencimento.Text = proximoDia.ToString("dd/MM/yyyy")
            Case "btnProximaDataCompetencia"
                Dim proximoDia As Date = Convert.ToDateTime(txtDataCompetencia.Text)
                proximoDia = proximoDia.AddDays(+1)
                txtDataCompetencia.Text = proximoDia.ToString("dd/MM/yyyy")
            Case "btnProximaDataEmissaoNF"
                Dim proximoDia As Date = Convert.ToDateTime(txtDataEmissaoNF.Text)
                proximoDia = proximoDia.AddDays(+1)
                txtDataEmissaoNF.Text = proximoDia.ToString("dd/MM/yyyy")
        End Select

    End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then

                    Dim drow As DataRow = CType(e.Row.DataItem, DataRowView).Row
                    Dim col As Integer

                    col = 0
                    If IsDBNull(drow("COD_EMPRESA")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("COD_EMPRESA")
                    End If

                    col += 1
                    If IsDBNull(drow("DESCR_RAPIDA")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("DESCR_RAPIDA")
                    End If

                    col += 1
                    If IsDBNull(drow("DATA_LANCAMENTO")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("DATA_LANCAMENTO")
                    End If

                    col += 1
                    If IsDBNull(drow("DATA_VENCIMENTO")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("DATA_VENCIMENTO")
                    End If

                    col += 1
                    If IsDBNull(drow("COMPETENCIA")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("COMPETENCIA")
                    End If

                    col += 1
                    If IsDBNull(drow("CONTA_BANCARIA")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("CONTA_BANCARIA")
                    End If

                    col += 1
                    If IsDBNull(drow("FORMA_PAGAMENTO")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("FORMA_PAGAMENTO")
                    End If


                    col += 1
                    If IsDBNull(drow("COD_HISTORICO")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("COD_HISTORICO")
                    End If


                    col += 1
                    If IsDBNull(drow("COMPL_HISTÓRICO")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("COMPL_HISTÓRICO")
                    End If


                    col += 1
                    If IsDBNull(drow("CONTABIL")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("CONTABIL")
                    End If


                    col += 1
                    If IsDBNull(drow("CENTRO_CUSTO")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("CENTRO_CUSTO")
                    End If


                    col += 1
                    If IsDBNull(drow("COD_CLIENTE")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("COD_CLIENTE")
                    End If

                    col += 1
                    If IsDBNull(drow("VALOR")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = CNumero.FormataNumero(drow("VALOR"), 2)
                    End If

                    col += 1
                    If IsDBNull(drow("DESCONTO")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("DESCONTO")
                    End If

                    col += 1
                    If IsDBNull(drow("DEBITO_CREDITO")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("DEBITO_CREDITO")
                    End If

                    col += 1
                    If IsDBNull(drow("NOTA_FISCAL")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("NOTA_FISCAL")
                    End If

                    col += 1
                    If IsDBNull(drow("DATA_EMISSÃO_NF")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("DATA_EMISSÃO_NF")
                    End If

                    col += 1
                    If IsDBNull(drow("COD_BANCO")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("COD_BANCO")
                    End If

                    col += 1
                    If IsDBNull(drow("AGENCIA_DESTINO")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("AGENCIA_DESTINO")
                    End If

                    col += 1
                    If IsDBNull(drow("DIGITO_CONTA_DESTINO")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("DIGITO_CONTA_DESTINO")
                    End If

                    col += 1
                    If IsDBNull(drow("CONTA_DESTINO")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("CONTA_DESTINO")
                    End If

                    col += 1
                    If IsDBNull(drow("NOME_FAVORECIDO")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("NOME_FAVORECIDO")
                    End If

                    col += 1
                    If IsDBNull(drow("MODALIDADE")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("MODALIDADE")
                    End If

                    col += 1
                    If IsDBNull(drow("DIGITO_AGENCIA")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("DIGITO_AGENCIA")
                    End If

                    col += 1
                    If IsDBNull(drow("CPF_CNPJ_FAVORECIDO")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("CPF_CNPJ_FAVORECIDO")
                    End If

                    col += 1
                    If IsDBNull(drow("NOME_BANCO")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("NOME_BANCO")
                    End If

                    col += 1
                    If IsDBNull(drow("TIPO_CONTA")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("TIPO_CONTA")
                    End If

                    col += 1
                    If IsDBNull(drow("FINALIDADE")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("FINALIDADE")
                    End If

                    col += 1
                    If IsDBNull(drow("FINALIDADE_DOC")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("FINALIDADE_DOC")
                    End If

                    col += 1
                    If IsDBNull(drow("FINALIDADE_TED")) Then
                        e.Row.Cells(col).Text = ""
                    Else
                        e.Row.Cells(col).Text = drow("FINALIDADE_TED")
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

        ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = Nothing
        GridView1.DataSource = GetData()
        GridView1.PageIndex = 0
        GridView1.DataBind()

    End Sub

    Protected Sub BindGridView1DataView()

        GridView1.DataSource = DataGridView
        GridView1.DataBind()

    End Sub

    Private Function GetData() As DataTable

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim table As DataTable = Nothing

        Dim oDataLancamento As New CDataHora(Convert.ToDateTime(txtDataLancamento.Text))
        Dim oDataVencimento As New CDataHora(Convert.ToDateTime(txtDataVencimento.Text))
        Dim oDataCompetencia As New CDataHora(Convert.ToDateTime(txtDataCompetencia.Text))
        Dim oDataEmissaoNF As New CDataHora(Convert.ToDateTime(txtDataEmissaoNF.Text))
        Dim dtInicio = FuncGetDtInicio(Convert.ToDateTime(txtDataLancamento.Text))
        Dim dtFim = FuncGetDtFim(Convert.ToDateTime(txtDataLancamento.Text))

        GeraComissaoGeral(dtInicio, dtFim)
        GeraComissaoDinamo(dtInicio, dtFim)

        table = ClassBD.GetExibirGrid("[SCR_GERAR_ARQUIVO_COMISSAO] '" & oDataLancamento.Data.ToString("yyyyMMdd") & "', '" & oDataVencimento.Data.ToString("yyyyMMdd") & "', '" & oDataCompetencia.Data.ToString("yyyyMMdd") & "', '" & oDataEmissaoNF.Data.ToString("yyyyMMdd") & "', '" & ddlPagamento.SelectedValue & "'", "Comissao", strConn)

        Return table

    End Function

    Public Function FuncGetDtInicio(paramData As Date) As Date
        FuncGetDtInicio = DateAdd("m", -1, DateSerial(Year(paramData), Month(paramData), 1))
    End Function

    Public Function FuncGetDtFim(paramData As Date) As Date
        FuncGetDtFim = DateAdd("d", -1, DateSerial(Year(paramData), Month(paramData), 1))
    End Function

    Private Sub GeraComissaoGeral(dtInicio As Date, dtFim As Date)

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim connection As New SqlConnection(strConn)
        Dim comm As String
        comm = "EXEC SCR_COMISSAO '" + dtInicio.ToString("yyyyMMdd") + "','" + dtFim.ToString("yyyyMMdd") + "'"
        Dim command As SqlCommand = New SqlCommand(comm, connection)

        command.Connection.Open()
        command.ExecuteReader()

    End Sub

    Private Sub GeraComissaoDinamo(dtInicio As Date, dtFim As Date)

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim connection As New SqlConnection(strConn)
        Dim comm As String
        comm = "EXEC SCR_COMISSAO_DINAMO '" + dtInicio.ToString("yyyyMMdd") + "','" + dtFim.ToString("yyyyMMdd") + "'"
        Dim command As SqlCommand = New SqlCommand(comm, connection)

        command.Connection.Open()
        command.ExecuteReader()

    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

        If e.NewPageIndex >= 0 Then
            GridView1.PageIndex = e.NewPageIndex
            BindGridView1DataView()
        End If

    End Sub

    Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
    End Sub

    Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Comissão' ,'Gerar Arquivo Comissão.');", True)
    End Sub

    Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
        Response.Redirect("../Menu.aspx")
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

    Protected Sub btnExcel_Click(sender As Object, e As EventArgs)

        Try

            If ddlPagamento.SelectedValue = "0" Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Existe Algo Errado!', 'Selecione o tipo de pagamento', 'danger');", True)
            Else
                BindGridView1Data()
            End If

            If Not IsNothing(GridView1.HeaderRow) Then

                Response.Clear()
                Response.Buffer = True
                Dim filename As String = String.Format("ComissãoCFI_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
                Response.AddHeader("content-disposition", "attachment;filename=" + filename)
                Response.ContentEncoding = System.Text.Encoding.Default
                Response.Charset = ""
                Response.ContentType = "application/vnd.ms-excel"

                Using sw As New StringWriter()

                    Dim hw As New HtmlTextWriter(sw)

                    GridView1.AllowPaging = False
                    BindGridView1Data()

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

End Class