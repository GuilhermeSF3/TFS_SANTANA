Imports System
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Santana.Seguranca

Namespace Paginas.Cadastro


    Public Class Usuarios
        Inherits SantanaPage

        Dim objUsuarios As DbUsuarios

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                FillCustomerInGrid()

            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub


        Protected Sub FillCustomerInGrid()

            Dim dtUsers As DataTable = New DbUsuarios().CarregarTodosUsuarios()
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

            If e.CommandName.Equals("AddNew") Then

                Dim txtNewLogin As TextBox
                txtNewLogin = CType(GridView1.HeaderRow.FindControl("txtNewLogin"), TextBox)

                Dim txtNewNome As TextBox
                txtNewNome = CType(GridView1.HeaderRow.FindControl("txtNewNome"), TextBox)

                Dim cmbNewFuncao As DropDownList
                cmbNewFuncao = CType(GridView1.HeaderRow.FindControl("cmbNewFuncao"), DropDownList)

                Dim txtNewGerencia As TextBox
                txtNewGerencia = CType(GridView1.HeaderRow.FindControl("txtNewGerencia"), TextBox)

                Dim txtNewFilial As TextBox
                txtNewFilial = CType(GridView1.HeaderRow.FindControl("txtNewFilial"), TextBox)

                Dim txtNewCpf As TextBox
                txtNewCpf = CType(GridView1.HeaderRow.FindControl("txtNewCpf"), TextBox)

                Dim txtNewEMail As TextBox
                txtNewEMail = CType(GridView1.HeaderRow.FindControl("txtNewEMail"), TextBox)

                Dim cmbNewAtivo As DropDownList
                cmbNewAtivo = CType(GridView1.HeaderRow.FindControl("cmbNewAtivo"), DropDownList)

                Dim txtNewNomeCompleto As TextBox
                txtNewNomeCompleto = CType(GridView1.HeaderRow.FindControl("txtNewNomeCompleto"), TextBox)

                Dim txtNewPerfil As TextBox
                txtNewPerfil = CType(GridView1.HeaderRow.FindControl("txtNewPerfil"), TextBox)

                Dim txtNewProduto As TextBox
                txtNewProduto = CType(GridView1.HeaderRow.FindControl("txtNewProduto"), TextBox)

                If txtNewLogin.Text.Trim() = "" Or
                    txtNewNome.Text.Trim() = "" Or
                    txtNewGerencia.Text.Trim() = "" Or
                    txtNewFilial.Text.Trim() = "" Or
                    txtNewCpf.Text.Trim() = "" Or
                    txtNewEMail.Text.Trim() = "" Or
                    txtNewNomeCompleto.Text.Trim() = "" Or
                    txtNewPerfil.Text.Trim() = "" Or
                    txtNewProduto.Text.Trim() = "" Then

                    strMensagem = "Por favor, preencher todos os campos."

                Else

                    objUsuarios = New DbUsuarios
                    Dim tdUser As DataTable = objUsuarios.CarregarUsuariosPorLoginOuNome(txtNewLogin.Text.Trim, "")
                    If tdUser.Rows.Count > 0 Then
                        strMensagem = "O Login " & txtNewLogin.Text.Trim() & " já esta cadastrado para o usuário " & tdUser.Rows(0).Field(Of String)("NomeUsuario")
                    End If

                    tdUser = objUsuarios.CarregarUsuariosPorLoginOuNome("", txtNewNome.Text.Trim())
                    If tdUser.Rows.Count > 0 Then
                        strMensagem = "Já consta um usuário com o nome " & tdUser.Rows(0).Field(Of String)("NomeUsuario")
                    End If

                    If strMensagem = "" Then
                        objUsuarios.InserirUsuario(txtNewLogin.Text, _
                                                     txtNewNome.Text, _
                                                     cmbNewFuncao.SelectedValue, _
                                                     txtNewGerencia.Text, _
                                                     txtNewFilial.Text, _
                                                     txtNewCpf.Text, _
                                                     txtNewEMail.Text, _
                                                     Convert.ToInt32(cmbNewAtivo.SelectedValue), _
                                                     txtNewNomeCompleto.Text, _
                                                     Convert.ToInt32(txtNewPerfil.Text), _
                                                     Convert.ToInt32(txtNewProduto.Text))


                        FillCustomerInGrid()
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Atualização' ,'Os dados foram salvos com sucesso.', 'success');", True)
                    End If
                End If

            ElseIf e.CommandName.Equals("Delete") Then
                objUsuarios = New DbUsuarios
                Dim index As Integer
                index = Convert.ToInt32(e.CommandArgument)
                objUsuarios.ApagarUsuario(GridView1.DataKeys(index).Values(0).ToString())
                FillCustomerInGrid()

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Exclusão de Registro' ,'Usuário excluido com sucesso.', 'success');", True)


            ElseIf e.CommandName.Equals("Reset") Then
                objUsuarios = New DbUsuarios
                Dim index As Integer
                index = Convert.ToInt32(e.CommandArgument)
                objUsuarios.RedefinirSenha(GridView1.DataKeys(index).Values(0).ToString())

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Alterção de Senha' ,'Senha do usuário redefinida.', 'success');", True)

            End If



            If strMensagem <> "" Then

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Existe Algo Errado!' ,'" + strMensagem + "', 'danger');", True)

                FillCustomerInGrid()

            End If

        End Sub


        Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

            Try
                If e.Row.RowType = DataControlRowType.DataRow Then
                    
                    Dim cmbFuncao As DropDownList
                    cmbFuncao = CType(e.Row.FindControl("cmbFuncao"), DropDownList)

                    If Not cmbFuncao Is Nothing Then
                        cmbFuncao.SelectedValue = GridView1.DataKeys(e.Row.RowIndex).Values(1).ToString()
                    End If

                    
                    Dim cmbAtivo As DropDownList
                    cmbAtivo = CType(e.Row.FindControl("cmbAtivo"), DropDownList)

                    If Not cmbAtivo Is Nothing Then
                        cmbAtivo.SelectedValue = GridView1.DataKeys(e.Row.RowIndex).Values(2).ToString()
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

            CType(GridView1.HeaderRow.FindControl("txtNewLogin"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtNewNome"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("cmbNewFuncao"), DropDownList).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtNewGerencia"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtNewFilial"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtNewCpf"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtNewEMail"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("cmbNewAtivo"), DropDownList).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtNewNomeCompleto"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtNewPerfil"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtNewProduto"), TextBox).Visible = True

            CType(GridView1.HeaderRow.FindControl("lnkAddNew"), LinkButton).Visible = True


        End Sub

        Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs)
            GridView1.EditIndex = e.NewEditIndex
            FillCustomerInGrid()

            CType(GridView1.HeaderRow.FindControl("txtNewLogin"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtNewNome"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("cmbNewFuncao"), DropDownList).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtNewGerencia"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtNewFilial"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtNewCpf"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtNewEMail"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("cmbNewAtivo"), DropDownList).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtNewNomeCompleto"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtNewPerfil"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtNewProduto"), TextBox).Visible = False

            CType(GridView1.HeaderRow.FindControl("lnkAddNew"), LinkButton).Visible = False
        End Sub


        Protected Sub GridView1_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)

            Dim strMensagem As String = ""

            If (GridView1.EditIndex <> -1) Then

                Dim lblLogin As Label
                lblLogin = CType(GridView1.Rows(e.RowIndex).FindControl("lblLogin"), Label)

                Dim txtNome As TextBox
                txtNome = CType(GridView1.Rows(e.RowIndex).FindControl("txtNome"), TextBox)

                Dim cmbFuncao As DropDownList
                cmbFuncao = CType(GridView1.Rows(e.RowIndex).FindControl("cmbFuncao"), DropDownList)

                Dim txtGerencia As TextBox
                txtGerencia = CType(GridView1.Rows(e.RowIndex).FindControl("txtGerencia"), TextBox)

                Dim txtFilial As TextBox
                txtFilial = CType(GridView1.Rows(e.RowIndex).FindControl("txtFilial"), TextBox)

                Dim txtCpf As TextBox
                txtCpf = CType(GridView1.Rows(e.RowIndex).FindControl("txtCpf"), TextBox)

                Dim txtEMail As TextBox
                txtEMail = CType(GridView1.Rows(e.RowIndex).FindControl("txtEMail"), TextBox)

                Dim cmbAtivo As DropDownList
                cmbAtivo = CType(GridView1.Rows(e.RowIndex).FindControl("cmbAtivo"), DropDownList)

                Dim txtNomeCompleto As TextBox
                txtNomeCompleto = CType(GridView1.Rows(e.RowIndex).FindControl("txtNomeCompleto"), TextBox)

                Dim txtPerfil As TextBox
                txtPerfil = CType(GridView1.Rows(e.RowIndex).FindControl("txtPerfil"), TextBox)

                Dim txtProduto As TextBox
                txtProduto = CType(GridView1.Rows(e.RowIndex).FindControl("txtProduto"), TextBox)



                If txtNome.Text.Trim() = "" Or
                txtGerencia.Text.Trim() = "" Or
                txtFilial.Text.Trim() = "" Or
                txtCpf.Text.Trim() = "" Or
                txtEMail.Text.Trim() = "" Or
                txtNomeCompleto.Text.Trim() = "" Or
                txtPerfil.Text.Trim() = "" Or
                txtProduto.Text.Trim() = "" Then

                    strMensagem = "Por favor, preencher todos os campos."

                Else


                    objUsuarios = New DbUsuarios
                    Dim tdUser As DataTable = objUsuarios.CarregarUsuariosPorLoginOuNome("", txtNome.Text.Trim())
                    If tdUser.Rows.Count > 0 AndAlso lblLogin.Text <> tdUser.Rows(0).Field(Of String)("Login") Then
                        strMensagem = "Já consta um usuário com o nome " & tdUser.Rows(0).Field(Of String)("NomeUsuario")
                    End If

                    If strMensagem = "" Then
                        objUsuarios.AtualizarUsuario(GridView1.DataKeys(e.RowIndex).Values(0).ToString(), _
                                                     txtNome.Text, _
                                                     cmbFuncao.SelectedValue, _
                                                     txtGerencia.Text, _
                                                     txtFilial.Text, _
                                                     txtCpf.Text, _
                                                     txtEMail.Text, _
                                                     Convert.ToInt32(cmbAtivo.SelectedValue), _
                                                     txtNomeCompleto.Text, _
                                                     Convert.ToInt32(txtPerfil.Text), _
                                                     Convert.ToInt32(txtProduto.Text))
                        GridView1.EditIndex = -1

                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Atualização' ,'Os dados foram salvos com sucesso.', 'success');", True)

                        FillCustomerInGrid()
                    End If

                End If

            End If


            If strMensagem <> "" Then
                FillCustomerInGrid()
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Existe Algo Errado!' ,'" + strMensagem + "', 'danger');", True)
            End If

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
            Response.Redirect("../Menu.aspx")
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
                    Dim filename As String = String.Format("Cadastro_Usuarios_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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