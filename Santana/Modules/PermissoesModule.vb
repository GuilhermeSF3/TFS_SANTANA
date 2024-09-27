Imports System.Collections.Generic
Imports System.Web
Imports System.Web.SessionState
Imports System.Linq


Imports Santana.Seguranca


Namespace Seguranca
    Public Class PermissoesModule
        Implements IHttpModule
        Implements IRequiresSessionState

        Private _context As HttpApplication

        Public Sub Dispose() Implements IHttpModule.Dispose
        End Sub

        Public Sub Init(context As HttpApplication) Implements IHttpModule.Init
            _context = context
            AddHandler context.PostRequestHandlerExecute, AddressOf context_PostRequestHandlerExecute
            AddHandler context.PreRequestHandlerExecute, AddressOf context_PreRequestHandlerExecute
        End Sub


        Private Sub context_PreRequestHandlerExecute(sender As Object, e As EventArgs)
            Dim handler = HttpContext.Current.Handler
            Dim application As HttpApplication = sender
            Dim contexto As New Santana.Seguranca.Contexto

            If Not IsNothing(application.Request) And application.Request.RawUrl.ToLower().Contains(".aspx") And Not application.Request.RawUrl.ToLower().Contains("crystal") Then

                If application.Request.AppRelativeCurrentExecutionFilePath.ToLower() <> "~/paginas/logon.aspx" _
                    AndAlso application.Request.AppRelativeCurrentExecutionFilePath.ToLower() <> "~/paginas/alterasenha.aspx" _
                    AndAlso application.Request.AppRelativeCurrentExecutionFilePath.ToLower() <> "~/default.aspx" Then

                    If IsNothing(contexto.UsuarioLogado) OrElse contexto.UsuarioLogado.Ativo = 0 OrElse Not contexto.DadosMenu.ListMenu.Any(Function(x) x.Perfil.Contains(contexto.UsuarioLogado.Perfil)) Then

                        application.Session.Clear()
                        application.Session.Abandon()

                        _context.Response.Redirect("~/paginas/logon.aspx")
                        _context.CompleteRequest()

                    End If

                    Debug.Print(application.Request.FilePath)

                    Dim itemPermissao As Boolean = False
                    For Each item As ItemMenu In contexto.DadosMenu.ListMenu.Where(Function(x) x.Perfil.Contains(contexto.UsuarioLogado.Perfil))

                        If [String].Equals(application.Request.FilePath, item.MenuId, StringComparison.CurrentCultureIgnoreCase) Then
                            itemPermissao = True
                            Exit For
                        End If
                    Next

                    If itemPermissao = False Then
                        _context.Response.Redirect("~/paginas/Menu.aspx")
                        _context.CompleteRequest()
                    End If
                End If
            End If
        End Sub


        Private Sub context_PostRequestHandlerExecute(sender As Object, e As EventArgs)

            'Dim currentPage = TryCast(HttpContext.Current.Handler, Page)
            'If Not currentPage Is Nothing Then
            '    Dim Contexto As New Santana.Seguranca.Contexto
            '    'Contexto.Navegacao.TituloPaginaAtual = currentPage.Title
            '    'Contexto.Navegacao.LinkPaginaAtual = currentPage.AppRelativeVirtualPath
            'End If
        End Sub

    End Class

    
End Namespace