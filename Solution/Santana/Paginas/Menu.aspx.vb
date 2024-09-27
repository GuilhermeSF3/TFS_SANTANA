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

            End If

        Catch ex As Exception
        Finally
            GC.Collect()
        End Try

    End Sub


End Class
