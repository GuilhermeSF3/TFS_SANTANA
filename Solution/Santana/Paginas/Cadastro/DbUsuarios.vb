Imports System.Data
Imports System.Data.SqlClient


Public Class DbUsuarios


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub


    Public Sub InserirUsuario(ByVal login As String, _
                              ByVal nomeUsuario As String, _
                              ByVal funcao As String, _
                              ByVal codGerente As String, _
                              ByVal codFilial As String, _
                              ByVal cpf As String, _
                              ByVal eMail As String, _
                              ByVal ativo As Int32, _
                              ByVal nomeCompleto As String, _
                              ByVal perfil As Int32, _
                              ByVal produto As Int32)


            



        Dim sql As String = "INSERT INTO USUARIO(Login, NomeUsuario, Funcao, CodGerente, CodFilial, Cpf, EMail, Ativo, NomeCompleto, Senha, Perfil, Produto) " & _
                            "VALUES (@Login, @NomeUsuario, @Funcao, @CodGerente, @CodFilial, @Cpf, @EMail, @Ativo, @NomeCompleto, @Senha, @Perfil, @Produto)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@Login", SqlDbType.VarChar, 20).Value = Login
        _cmd.Parameters.Add("@NomeUsuario", SqlDbType.VarChar, 20).Value = NomeUsuario
        _cmd.Parameters.Add("@Funcao", SqlDbType.VarChar, 3).Value = funcao
        _cmd.Parameters.Add("@CodGerente", SqlDbType.VarChar, 100).Value = codGerente
        _cmd.Parameters.Add("@CodFilial", SqlDbType.VarChar, 5).Value = CodFilial
        _cmd.Parameters.Add("@Cpf", SqlDbType.VarChar, 11).Value = Cpf
        _cmd.Parameters.Add("@EMail", SqlDbType.VarChar, 50).Value = EMail
        _cmd.Parameters.Add("@Ativo", SqlDbType.Int).Value = Ativo
        _cmd.Parameters.Add("@NomeCompleto", SqlDbType.VarChar, 50).Value = NomeCompleto
        _cmd.Parameters.Add("@Senha", SqlDbType.VarChar, 40).Value = Util.Senha.GeraHash("a*123456")
        _cmd.Parameters.Add("@Perfil", SqlDbType.Int).Value = Perfil
        _cmd.Parameters.Add("@Produto", SqlDbType.Int).Value = Produto

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarUsuario(ByVal login As String, _
                              ByVal nomeUsuario As String, _
                              ByVal funcao As String, _
                              ByVal codGerente As String, _
                              ByVal codFilial As String, _
                              ByVal cpf As String, _
                              ByVal eMail As String, _
                              ByVal ativo As Int32, _
                              ByVal nomeCompleto As String, _
                              ByVal perfil As Int32, _
                              ByVal produto As Int32)

        Dim sql As String = "UPDATE USUARIO SET NomeUsuario=@NomeUsuario, " & _
                            "Funcao=@Funcao, " & _
                            "CodGerente=@CodGerente, " & _
                            "CodFilial=@CodFilial, " & _
                            "Cpf=@Cpf, " & _
                            "EMail=@EMail, " & _
                            "Ativo=@Ativo, " & _
                            "NomeCompleto=@NomeCompleto, " & _
                            "Perfil=@Perfil, " & _
                            "Produto=@Produto " & _
                            "Where Login=@Login"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@Login", SqlDbType.VarChar, 20).Value = login
        _cmd.Parameters.Add("@NomeUsuario", SqlDbType.VarChar, 20).Value = NomeUsuario
        _cmd.Parameters.Add("@Funcao", SqlDbType.VarChar, 3).Value = funcao
        _cmd.Parameters.Add("@CodGerente", SqlDbType.VarChar, 100).Value = codGerente
        _cmd.Parameters.Add("@CodFilial", SqlDbType.VarChar, 5).Value = CodFilial
        _cmd.Parameters.Add("@Cpf", SqlDbType.VarChar, 11).Value = Cpf
        _cmd.Parameters.Add("@EMail", SqlDbType.VarChar, 50).Value = EMail
        _cmd.Parameters.Add("@Ativo", SqlDbType.Int).Value = Ativo
        _cmd.Parameters.Add("@NomeCompleto", SqlDbType.VarChar, 50).Value = NomeCompleto
        _cmd.Parameters.Add("@Perfil", SqlDbType.Int).Value = Perfil
        _cmd.Parameters.Add("@Produto", SqlDbType.Int).Value = Produto

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub




    Public Sub ApagarUsuario(ByVal login As String)


        Dim sql As String = "DELETE FROM USUARIO Where Login=@Login"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@Login", SqlDbType.VarChar, 20).Value = Login

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub



    Public Sub RedefinirSenha(ByVal login As String)


        Dim sql As String = "UPDATE USUARIO SET Senha=@Senha Where Login=@Login"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@Login", SqlDbType.VarChar, 20).Value = login
        _cmd.Parameters.Add("@Senha", SqlDbType.VarChar, 40).Value = Util.Senha.GeraHash("a*123456")

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub



    Public Function CarregarTodosUsuarios() As DataTable

        Dim sql As String = "SELECT * FROM USUARIO (NOLOCK) ORDER BY NomeUsuario"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarUsuariosPorLoginOuNome(login As String, nome As String) As DataTable

        Dim sql As String = "SELECT * FROM USUARIO (NOLOCK) WHERE Login=@Login OR NomeUsuario=@NomeUsuario"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@Login", SqlDbType.VarChar, 20).Value = login
        _cmd.Parameters.Add("@NomeUsuario", SqlDbType.VarChar, 20).Value = nome

        _cmd.Prepare()

        Dim da As New SqlDataAdapter(_cmd)

        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function

End Class


