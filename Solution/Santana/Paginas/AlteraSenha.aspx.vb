Imports System.Data
Imports System.Data.SqlClient
Imports Util
Imports Microsoft.VisualBasic
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.IO
Imports System.Web
Imports System.Web.Security
Imports System.Security.Cryptography
Imports System.Text


Public Class AlteraSenha
    Inherits System.Web.UI.Page
    Dim valido As Boolean = False
    Dim grupos As String = ""
    Dim sAUTENTICACAO As String = ""
    Dim sVALIDACAO As String = ""
    Dim sPerfil As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim usuario As String = DirectCast(HttpContext.Current.Session("UsuarioInformado"), String)

            If usuario <> Nothing Then
                If usuario.Trim() <> "" Then
                    txtUsuario.Text = usuario
                    HttpContext.Current.Session.Remove("UsuarioInformado")
                    txtUsuario.Focus()
                End If
            End If

        End If

    End Sub



    Protected Sub btnAlterar_Click(sender As Object, e As EventArgs)



        If txtSenhaNova.Text.Trim() = "" Or txtConfirmaSenha.Text.Trim() = "" Or txtUsuario.Text.Trim() = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Existe Algo Errado!', 'Usuário e Senhas devem ser Preenchidos');", True)
            Exit Sub
        End If

        If txtSenhaNova.Text <> txtConfirmaSenha.Text Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Existe Algo Errado!', 'Confirmação de Senha inválida');", True)
            Exit Sub
        End If

        Dim numberPattern As String = "^.*(?=.{8,})(?=.*\d)(?=.*[A-Za-z])(?=.*[!$#%]).*$"
        If Not Regex.IsMatch(txtSenhaNova.Text, numberPattern) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Existe Algo Errado!', 'Sua senha precisa atender a política de segurança solicitada.');", True)
            Exit Sub
        End If


        If ValidarDireitos(txtUsuario.Text, txtSenhaAntiga.Text, True) Then
            GravarNovaSenha(txtUsuario.Text, txtConfirmaSenha.Text)
            Response.Redirect("Menu.aspx")
        Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Existe Algo Errado!', 'Senha atual inválida');", True)
        End If

    End Sub



    Private Function ValidarDireitos(ByVal strUsuario As String, ByVal strPwd As String, ByVal AutenticacaoModoBD As Boolean) As Boolean
        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim con As New SqlConnection(strConn)
        Dim Vsenha As String = ""
        Dim Validacao As String = ""
        Dim cmd As New SqlCommand(
        "Select top 1 senha from usuario (nolock) where Login='" & strUsuario & "' and Ativo = 1", con)
        cmd.Connection.Open()

        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        While dr.Read()

            Vsenha = Trim(dr.GetString(0))
            Validacao = Util.Senha.GeraHash(Trim(strPwd))
            If (AutenticacaoModoBD = True And Validacao = Vsenha) Or AutenticacaoModoBD = False Then
                Return True
            End If

        End While

        dr.Close()
        con.Close()

        Return False

    End Function

    Private Sub GravarNovaSenha(ByVal Usuario As String, ByVal Senha As String)

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim conexao As New SqlConnection(strConn)
        Dim comando As SqlCommand
        Dim Validacao As String

        Validacao = Util.Senha.GeraHash(Trim(Senha))
        conexao.Open()
        comando = New SqlCommand("update usuario set senha='" & Validacao & "' where Login='" & Usuario & "'", conexao)
        comando.ExecuteNonQuery()
        conexao.Close()

    End Sub


    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        Response.Redirect("Menu.aspx")
    End Sub

End Class
