Imports System
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Santana.Seguranca
Imports System.Data.SqlClient

Namespace Paginas.Cadastro

    Public Class CadastroDeContaComissao
        Inherits SantanaPage

        Dim objContas As DbContaComissao

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                FillCustomerInGrid()

            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub

        Protected Sub FillCustomerInGrid()

            Dim dtUsers As DataTable = New DbContaComissao().CarregarTodasContas()
            Try

                If dtUsers.Rows.Count > 0 Then
                    GridView1.DataSource = dtUsers
                    GridView1.DataBind()
                Else
                    dtUsers.Rows.Add(dtUsers.NewRow())
                    Me.GridView1.DataSource = dtUsers
                    GridView1.DataBind()

                    Dim totalColumns As Integer
                    totalColumns = GridView1.Rows(0).Cells.Count
                    GridView1.Rows(0).Cells.Clear()
                    GridView1.Rows(0).Cells.Add(New TableCell())
                    GridView1.Rows(0).Cells(0).ColumnSpan = totalColumns
                    GridView1.Rows(0).Cells(0).Style.Add("text-align", "center")
                    GridView1.Rows(0).Cells(0).Text = "Não há usuário cadastrado"

                End If

            Catch ex As Exception
                Throw New Exception(ex.Message.ToString(), ex)
            End Try

        End Sub

        Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim strMensagem As String = ""
            Try
                If e.CommandName.Equals("AddNew") Then

                    Dim txtDescRapida As TextBox
                    txtDescRapida = CType(GridView1.HeaderRow.FindControl("txtDescRapida"), TextBox)

                    Dim txtContaBancaria As TextBox
                    txtContaBancaria = CType(GridView1.HeaderRow.FindControl("txtContaBancaria"), TextBox)

                    Dim txtFormaPagamento As TextBox
                    txtFormaPagamento = CType(GridView1.HeaderRow.FindControl("txtFormaPagamento"), TextBox)

                    Dim txtCentroCusto As TextBox
                    txtCentroCusto = CType(GridView1.HeaderRow.FindControl("txtCentroCusto"), TextBox)

                    Dim txtCodCliente As TextBox
                    txtCodCliente = CType(GridView1.HeaderRow.FindControl("txtCodCliente"), TextBox)

                    Dim txtDebitoCredito As TextBox
                    txtDebitoCredito = CType(GridView1.HeaderRow.FindControl("txtDebitoCredito"), TextBox)

                    Dim txtCodBanco As TextBox
                    txtCodBanco = CType(GridView1.HeaderRow.FindControl("txtCodBanco"), TextBox)

                    Dim txtAgenciaDestino As TextBox
                    txtAgenciaDestino = CType(GridView1.HeaderRow.FindControl("txtAgenciaDestino"), TextBox)

                    Dim txtDigitoContaDestino As TextBox
                    txtDigitoContaDestino = CType(GridView1.HeaderRow.FindControl("txtDigitoContaDestino"), TextBox)

                    Dim txtContaDestino As TextBox
                    txtContaDestino = CType(GridView1.HeaderRow.FindControl("txtContaDestino"), TextBox)

                    Dim txtNomeFavorecido As DropDownList
                    txtNomeFavorecido = CType(GridView1.HeaderRow.FindControl("ddlNomeFavorecido"), DropDownList)

                    Dim txtModalidade As TextBox
                    txtModalidade = CType(GridView1.HeaderRow.FindControl("txtModalidade"), TextBox)

                    Dim txtDigitoAgencia As TextBox
                    txtDigitoAgencia = CType(GridView1.HeaderRow.FindControl("txtDigitoAgencia"), TextBox)

                    Dim txtCpfCnpjFavorecido As TextBox
                    txtCpfCnpjFavorecido = CType(GridView1.HeaderRow.FindControl("txtCpfCnpjFavorecido"), TextBox)

                    Dim txtNomeBanco As TextBox
                    txtNomeBanco = CType(GridView1.HeaderRow.FindControl("txtNomeBanco"), TextBox)

                    Dim txtTipoConta As TextBox
                    txtTipoConta = CType(GridView1.HeaderRow.FindControl("txtTipoConta"), TextBox)

                    Dim txtFinalidade As TextBox
                    txtFinalidade = CType(GridView1.HeaderRow.FindControl("txtFinalidade"), TextBox)

                    Dim txtFinalidadeDoc As TextBox
                    txtFinalidadeDoc = CType(GridView1.HeaderRow.FindControl("txtFinalidadeDoc"), TextBox)

                    Dim txtFinalidadeTed As TextBox
                    txtFinalidadeTed = CType(GridView1.HeaderRow.FindControl("txtFinalidadeTed"), TextBox)
                    

                    If txtCpfCnpjFavorecido.Text.Trim() = "" Or txtNomeFavorecido.SelectedValue = "" Then

                        strMensagem = "Por favor, preencher os campos obrigatórios marcados com ""*""."

                    Else

                        objContas = New DbContaComissao
                        Dim tdUser As DataTable = objContas.CarregarContaPorCPFCNPJ(txtCpfCnpjFavorecido.Text.Trim())
                        If tdUser.Rows.Count > 0 Then
                            strMensagem = "O CPF/CNPJ " & txtCpfCnpjFavorecido.Text.Trim() & " já esta cadastrado para outra conta."
                        End If

                        If strMensagem = "" Then
                            objContas.InserirConta(txtDescRapida.Text,
                                                   txtCpfCnpjFavorecido.Text,
                                                   txtContaBancaria.Text,
                                                   txtFormaPagamento.Text,
                                                   txtCentroCusto.Text,
                                                   txtCodCliente.Text,
                                                   txtDebitoCredito.Text,
                                                   txtCodBanco.Text,
                                                   txtAgenciaDestino.Text,
                                                   txtDigitoContaDestino.Text,
                                                   txtContaDestino.Text,
                                                   txtNomeFavorecido.SelectedValue,
                                                   txtModalidade.Text,
                                                   txtDigitoAgencia.Text,
                                                   txtNomeBanco.Text,
                                                   txtTipoConta.Text,
                                                   txtFinalidade.Text,
                                                   txtFinalidadeDoc.Text,
                                                   txtFinalidadeTed.Text)

                            FillCustomerInGrid()
                            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Atualização' ,'Os dados foram salvos com sucesso.', 'success');", True)
                        End If
                    End If

                ElseIf e.CommandName.Equals("Delete") Then
                    objContas = New DbContaComissao
                    Dim index As Integer
                    index = Convert.ToInt32(e.CommandArgument)
                    objContas.ApagarConta(GridView1.DataKeys(index).Values(0).ToString())
                    FillCustomerInGrid()

                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Exclusão de Registro' ,'Conta excluida com sucesso.', 'success');", True)

                End If

                If strMensagem <> "" Then

                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Existe Algo Errado!' ,'" + strMensagem + "', 'danger');", True)

                    FillCustomerInGrid()

                End If
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Existe Algo Errado!' ,'" + ex.Message + "', 'danger');", True)

                FillCustomerInGrid()
            End Try

        End Sub

        Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

            Try
                If e.Row.RowType = DataControlRowType.DataRow Then
                End If
                If e.Row.RowType = DataControlRowType.Header Then
                    Dim cmbFavorecido As DropDownList
                    cmbFavorecido = CType(e.Row.FindControl("ddlNomeFavorecido"), DropDownList)
                    If Not cmbFavorecido Is Nothing Then
                        objContas = New DbContaComissao()
                        objContas.CarregarFavorecido(cmbFavorecido)
                    End If
                End If

            Catch ex As Exception
                Throw New Exception(ex.Message.ToString(), ex)
            End Try

        End Sub

        Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)

        End Sub

        Protected Sub GridView1_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GridView1.RowCancelingEdit
            GridView1.EditIndex = -1
            FillCustomerInGrid()

            CType(GridView1.HeaderRow.FindControl("txtDescRapida"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtContaBancaria"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtFormaPagamento"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtCentroCusto"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtCodCliente"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtDebitoCredito"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtCodBanco"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtAgenciaDestino"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtDigitoContaDestino"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtContaDestino"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("ddlNomeFavorecido"), DropDownList).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtModalidade"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtDigitoAgencia"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtCpfCnpjFavorecido"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtNomeBanco"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtTipoConta"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtFinalidade"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtFinalidadeDoc"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtFinalidadeTed"), TextBox).Visible = True

        End Sub

        Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs)
            GridView1.EditIndex = e.NewEditIndex
            FillCustomerInGrid()

            CType(GridView1.HeaderRow.FindControl("txtDescRapida"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtContaBancaria"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtFormaPagamento"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtCentroCusto"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtCodCliente"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtDebitoCredito"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtCodBanco"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtAgenciaDestino"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtDigitoContaDestino"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtContaDestino"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("ddlNomeFavorecido"), DropDownList).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtModalidade"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtDigitoAgencia"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtCpfCnpjFavorecido"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtNomeBanco"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtTipoConta"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtFinalidade"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtFinalidadeDoc"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtFinalidadeTed"), TextBox).Visible = False

        End Sub

        Protected Sub GridView1_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)

            Dim strMensagem As String = ""

            Try
                If (GridView1.EditIndex <> -1) Then

                    Dim txtDescRapida As TextBox
                    txtDescRapida = CType(GridView1.Rows(e.RowIndex).FindControl("lblDescRapida"), TextBox)

                    Dim txtContaBancaria As TextBox
                    txtContaBancaria = CType(GridView1.Rows(e.RowIndex).FindControl("lblContaBancaria"), TextBox)

                    Dim txtFormaPagamento As TextBox
                    txtFormaPagamento = CType(GridView1.Rows(e.RowIndex).FindControl("lblFormaPagamento"), TextBox)

                    Dim txtCentroCusto As TextBox
                    txtCentroCusto = CType(GridView1.Rows(e.RowIndex).FindControl("lblCentroCusto"), TextBox)

                    Dim txtCodCliente As TextBox
                    txtCodCliente = CType(GridView1.Rows(e.RowIndex).FindControl("lblCodCliente"), TextBox)

                    Dim txtDebitoCredito As TextBox
                    txtDebitoCredito = CType(GridView1.Rows(e.RowIndex).FindControl("lblDebitoCredito"), TextBox)

                    Dim txtCodBanco As TextBox
                    txtCodBanco = CType(GridView1.Rows(e.RowIndex).FindControl("lblCodBanco"), TextBox)

                    Dim txtAgenciaDestino As TextBox
                    txtAgenciaDestino = CType(GridView1.Rows(e.RowIndex).FindControl("lblAgenciaDestino"), TextBox)

                    Dim txtDigitoContaDestino As TextBox
                    txtDigitoContaDestino = CType(GridView1.Rows(e.RowIndex).FindControl("lblDigitoContaDestino"), TextBox)

                    Dim txtContaDestino As TextBox
                    txtContaDestino = CType(GridView1.Rows(e.RowIndex).FindControl("lblContaDestino"), TextBox)

                    Dim txtNomeFavorecido As Label
                    txtNomeFavorecido = CType(GridView1.Rows(e.RowIndex).FindControl("lblNomeFavorecido"), Label)

                    Dim txtModalidade As TextBox
                    txtModalidade = CType(GridView1.Rows(e.RowIndex).FindControl("lblModalidade"), TextBox)

                    Dim txtDigitoAgencia As TextBox
                    txtDigitoAgencia = CType(GridView1.Rows(e.RowIndex).FindControl("lblDigitoAgencia"), TextBox)

                    Dim txtCpfCnpjFavorecido As TextBox
                    txtCpfCnpjFavorecido = CType(GridView1.Rows(e.RowIndex).FindControl("lblCpfCnpjFavorecido"), TextBox)

                    Dim txtNomeBanco As TextBox
                    txtNomeBanco = CType(GridView1.Rows(e.RowIndex).FindControl("lblNomeBanco"), TextBox)

                    Dim txtTipoConta As TextBox
                    txtTipoConta = CType(GridView1.Rows(e.RowIndex).FindControl("lblTipoConta"), TextBox)

                    Dim txtFinalidade As TextBox
                    txtFinalidade = CType(GridView1.Rows(e.RowIndex).FindControl("lblFinalidade"), TextBox)

                    Dim txtFinalidadeDoc As TextBox
                    txtFinalidadeDoc = CType(GridView1.Rows(e.RowIndex).FindControl("lblFinalidadeDoc"), TextBox)

                    Dim txtFinalidadeTed As TextBox
                    txtFinalidadeTed = CType(GridView1.Rows(e.RowIndex).FindControl("lblFinalidadeTed"), TextBox)

                    If txtCpfCnpjFavorecido.Text.Trim() = "" Then
                        'txtDescRapida.Text.Trim() = "" Or
                        'txtContaBancaria.Text.Trim() = "" Or
                        'txtFormaPagamento.Text.Trim() = "" Or
                        'txtCentroCusto.Text.Trim() = "" Or
                        'txtCodCliente.Text.Trim() = "" Or
                        'txtDebitoCredito.Text.Trim() = "" Or
                        'txtCodBanco.Text.Trim() = "" Or
                        'txtAgenciaDestino.Text.Trim() = "" Or
                        'txtDigitoContaDestino.Text.Trim() = "" Or
                        'txtContaDestino.Text.Trim() = "" Or
                        'txtNomeFavorecido.Text.Trim() = "" Or
                        'txtModalidade.Text.Trim() = "" Or
                        'txtDigitoAgencia.Text.Trim() = "" Or
                        'txtNomeBanco.Text.Trim() = "" Or
                        'txtTipoConta.Text.Trim() = "" Or
                        'txtFinalidade.Text.Trim() = "" Or
                        'txtFinalidadeDoc.Text.Trim() = "" Or
                        'txtFinalidadeTed.Text.Trim() = "" 

                        strMensagem = "Por favor, preencher todos os campos."

                    Else

                        objContas = New DbContaComissao

                        Dim index As Integer
                        Dim cpfCnpjIdentity As String
                        Dim cpfcnpjPreview As String
                        Dim tdUser As DataTable

                        index = Convert.ToInt32(e.RowIndex)
                        cpfCnpjIdentity = GridView1.DataKeys(index).Values(0).ToString()
                        tdUser = objContas.CarregarContaPorCPFCNPJ(cpfCnpjIdentity)
                        cpfcnpjPreview = tdUser.Rows(0).Field(Of String)("DSCPFCNPJFAVORECIDO")

                        If tdUser.Rows.Count > 0 AndAlso txtCpfCnpjFavorecido.Text <> cpfcnpjPreview Then

                            strMensagem = "O CPF/CNPJ " & txtCpfCnpjFavorecido.Text.Trim() & " já esta cadastrado para outra conta."

                        End If

                        If strMensagem = "" Then

                            objContas.AtualizarConta(txtDescRapida.Text,
                                                   txtCpfCnpjFavorecido.Text,
                                                   txtContaBancaria.Text,
                                                   txtFormaPagamento.Text,
                                                   txtCentroCusto.Text,
                                                   txtCodCliente.Text,
                                                   txtDebitoCredito.Text,
                                                   txtCodBanco.Text,
                                                   txtAgenciaDestino.Text,
                                                   txtDigitoContaDestino.Text,
                                                   txtContaDestino.Text,
                                                   txtModalidade.Text,
                                                   txtDigitoAgencia.Text,
                                                   txtNomeBanco.Text,
                                                   txtTipoConta.Text,
                                                   txtFinalidade.Text,
                                                   txtFinalidadeDoc.Text,
                                                   txtFinalidadeTed.Text,
                                                   cpfCnpjIdentity)

                            GridView1.EditIndex = -1

                            FillCustomerInGrid()
                            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Atualização' ,'Os dados foram salvos com sucesso.', 'success');", True)
                        End If

                    End If

                End If

                If strMensagem <> "" Then
                    FillCustomerInGrid()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Existe Algo Errado!' ,'" + strMensagem + "', 'danger');", True)
                End If

            Catch ex As Exception
                FillCustomerInGrid()
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Existe Algo Errado!' ,'" + ex.Message + "', 'danger');", True)
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

        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../../Menu.aspx")
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
                    Dim filename As String = String.Format("Cadastro_Contas_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename)
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.ms-excel"

                    Using sw As New StringWriter()

                        Dim hw As New HtmlTextWriter(sw)

                        GridView1.FooterRow.Visible = False
                        GridView1.Columns(11).Visible = False
                        GridView1.Columns(12).Visible = False

                        GridView1.HeaderRow.BackColor = Color.White
                        For Each cell As TableCell In GridView1.HeaderRow.Cells
                            cell.CssClass = "GridviewScrollC3Header"

                            Dim controls As New List(Of Control)()
                            For Each control As Control In cell.Controls
                                controls.Add(control)
                            Next

                            For Each control As Control In controls
                                Select Case control.GetType().Name
                                    Case "Label"
                                        cell.Controls.Add(New Literal() With { _
                                         .Text = TryCast(control, Label).Text _
                                        })
                                        Exit Select

                                End Select
                                cell.Controls.Remove(control)
                            Next

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
                                        Case "Label"
                                            cell.Controls.Add(New Literal() With { _
                                             .Text = TryCast(control, Label).Text _
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

                        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default
                        HttpContext.Current.Response.Write(style)
                        HttpContext.Current.Response.Output.Write(sw.ToString())
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