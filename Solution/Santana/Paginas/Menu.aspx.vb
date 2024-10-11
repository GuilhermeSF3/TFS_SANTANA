Imports System.Data.SqlClient
Imports Santana.Seguranca

Public Class Menu
    Inherits SantanaPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If IsNothing(ContextoWeb.UsuarioLogado) OrElse ContextoWeb.UsuarioLogado.Ativo = 0 Then
                Response.Redirect("Logon.aspx", True)

            Else
                For Each item As ItemMenu In ContextoWeb.DadosMenu.ListMenu.Where(Function(x) x.Perfil.Contains(ContextoWeb.UsuarioLogado.Perfil))
                    Dim content As ContentPlaceHolder = Page.Master.FindControl("ContentPlaceHolder1")
                    If Not IsNothing(content) Then
                        Dim MenuId As HtmlGenericControl = content.FindControl(item.MenuId)
                        If Not IsNothing(MenuId) Then
                            MenuId.Visible = True
                        End If
                    End If
                Next
                CarregarFavoritos()
            End If

        Catch ex As Exception
        Finally
            GC.Collect()
        End Try

    End Sub






    Protected Sub btnFavoritar_Click(sender As Object, e As EventArgs)
        Dim currentPage As String = ""
        Dim functionName As String = txtNomeFavorito.Text.Trim()
        If ddlPaginas1.SelectedIndex > 0 Then
            currentPage = ddlPaginas1.SelectedValue
        ElseIf ddlPaginas2.SelectedIndex > 0 Then
            currentPage = ddlPaginas2.SelectedValue
        ElseIf ddlPaginas3.SelectedIndex > 0 Then
            currentPage = ddlPaginas3.SelectedValue
        ElseIf ddlPaginas4.SelectedIndex > 0 Then
            currentPage = ddlPaginas4.SelectedValue
        ElseIf ddlPaginas5.SelectedIndex > 0 Then
            currentPage = ddlPaginas5.SelectedValue
        ElseIf ddlPaginas6.SelectedIndex > 0 Then
            currentPage = ddlPaginas6.SelectedValue
        ElseIf ddlPaginas7.SelectedIndex > 0 Then
            currentPage = ddlPaginas7.SelectedValue
        ElseIf ddlPaginas8.SelectedIndex > 0 Then
            currentPage = ddlPaginas8.SelectedValue
        ElseIf ddlPaginas9.SelectedIndex > 0 Then
            currentPage = ddlPaginas9.SelectedValue
        ElseIf ddlPaginas10.SelectedIndex > 0 Then
            currentPage = ddlPaginas10.SelectedValue
        ElseIf ddlPaginas11.SelectedIndex > 0 Then
            currentPage = ddlPaginas11.SelectedValue
        ElseIf ddlPaginas12.SelectedIndex > 0 Then
            currentPage = ddlPaginas12.SelectedValue
        ElseIf ddlPaginas13.SelectedIndex > 0 Then
            currentPage = ddlPaginas13.SelectedValue
        End If


        If String.IsNullOrEmpty(functionName) Then
            functionName = System.IO.Path.GetFileName(currentPage)
        End If


        If String.IsNullOrEmpty(currentPage) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Por favor, selecione uma página.');", True)
            Return
        End If

        Dim loginUsuario As String = ContextoWeb.UsuarioLogado.Login
        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

        Try
            Using conn As New SqlConnection(strConn)
                conn.Open()
                Dim queryVerificar As String = "SELECT COUNT(*) FROM UserFavorites WHERE UserID = @UserID AND FunctionURL = @FunctionURL"
                Using cmdVerificar As New SqlCommand(queryVerificar, conn)
                    cmdVerificar.Parameters.AddWithValue("@UserID", loginUsuario)
                    cmdVerificar.Parameters.AddWithValue("@FunctionURL", currentPage)
                    Dim count As Integer = Convert.ToInt32(cmdVerificar.ExecuteScalar())

                    If count = 0 Then
                        Dim query As String = "INSERT INTO UserFavorites (UserID, FunctionName, FunctionURL) VALUES (@UserID, @FunctionName, @FunctionURL)"
                        Using cmd As New SqlCommand(query, conn)
                            cmd.Parameters.AddWithValue("@UserID", loginUsuario)
                            cmd.Parameters.AddWithValue("@FunctionName", functionName)
                            cmd.Parameters.AddWithValue("@FunctionURL", currentPage)

                            cmd.ExecuteNonQuery()
                        End Using


                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "closeModal", "$('#favoritoModal').modal('hide'); location.reload();", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "closeModal", "$('#favoritoModal').modal('hide'); alert('Esta página já está favoritada.');", True)
                    End If
                End Using
            End Using
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Erro ao favoritar a página: " & ex.Message & "');", True)
        End Try
    End Sub


    Protected Sub btnExcluirFavorito_Click(sender As Object, e As EventArgs)
        Dim currentPage As String = ""
        Dim loginUsuario As String = ContextoWeb.UsuarioLogado.Login
        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        If ddlPaginas1.SelectedIndex > 0 Then
            currentPage = ddlPaginas1.SelectedValue
        ElseIf ddlPaginas2.SelectedIndex > 0 Then
            currentPage = ddlPaginas2.SelectedValue
        ElseIf ddlPaginas3.SelectedIndex > 0 Then
            currentPage = ddlPaginas3.SelectedValue
        ElseIf ddlPaginas4.SelectedIndex > 0 Then
            currentPage = ddlPaginas4.SelectedValue
        ElseIf ddlPaginas5.SelectedIndex > 0 Then
            currentPage = ddlPaginas5.SelectedValue
        ElseIf ddlPaginas6.SelectedIndex > 0 Then
            currentPage = ddlPaginas6.SelectedValue
        ElseIf ddlPaginas7.SelectedIndex > 0 Then
            currentPage = ddlPaginas7.SelectedValue
        ElseIf ddlPaginas8.SelectedIndex > 0 Then
            currentPage = ddlPaginas8.SelectedValue
        ElseIf ddlPaginas9.SelectedIndex > 0 Then
            currentPage = ddlPaginas9.SelectedValue
        ElseIf ddlPaginas10.SelectedIndex > 0 Then
            currentPage = ddlPaginas10.SelectedValue
        ElseIf ddlPaginas11.SelectedIndex > 0 Then
            currentPage = ddlPaginas11.SelectedValue
        ElseIf ddlPaginas12.SelectedIndex > 0 Then
            currentPage = ddlPaginas12.SelectedValue
        ElseIf ddlPaginas13.SelectedIndex > 0 Then
            currentPage = ddlPaginas12.SelectedValue
        End If
        Try
            Using conn As New SqlConnection(strConn)
                conn.Open()
                Dim query As String = "DELETE FROM UserFavorites WHERE UserID = @UserID AND FunctionURL = @FunctionURL"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@UserID", loginUsuario)
                    cmd.Parameters.AddWithValue("@FunctionURL", currentPage)
                    cmd.ExecuteNonQuery()
                End Using
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "closeModal", "$('#favoritoModal').modal('hide'); $('.modal-backdrop').remove(); setTimeout(function() { alert('Favorito excluído com sucesso!'); }, 500);", True)
            End Using
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Erro ao excluir o favorito: " & ex.Message & "');", True)
        End Try
    End Sub


    Private Sub CarregarFavoritos()
        Dim loginUsuario As String = ContextoWeb.UsuarioLogado.Login
        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim sb As New StringBuilder()

        Try
            Using conn As New SqlConnection(strConn)
                conn.Open()
                Dim query As String = "SELECT FunctionName, FunctionURL FROM UserFavorites WHERE UserID = @UserID"
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@UserID", loginUsuario)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.HasRows Then
                            While reader.Read()
                                Dim nomeFavorito As String = reader("FunctionName").ToString()
                                Dim urlFavorito As String = reader("FunctionURL").ToString()

                                sb.AppendFormat("<div class='favorite-box' style='cursor: pointer;' onclick='location.href=""{0}"";'><span>{1}</span></div>", urlFavorito, nomeFavorito)
                            End While
                        Else
                            litFavoritos.Text = "Nenhum favorito encontrado."
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception

        End Try

        If sb.Length > 0 Then
            litFavoritos.Text = sb.ToString()
        End If
    End Sub




End Class


