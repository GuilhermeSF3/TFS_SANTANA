Imports Santana.Seguranca

Public Class SantanaWeb
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim contexto = New Contexto
        lblUser.Text = contexto.UsuarioLogado.NomeCompleto + ". Seja bem-vindo(a)   Versão 3.5"
    End Sub

End Class