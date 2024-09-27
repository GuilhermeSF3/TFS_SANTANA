Imports System.Data
Imports System.Data.SqlClient


Public Class DbRegiaoCadastro


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub


    Public Sub InserirRegistro(ByVal COD As String,
                                   ByVal DESCR As String)

        Dim sql As String = "INSERT INTO TAB_REGIAO(COD_REGIAO, NOME_REGIAO) " &
                                "VALUES (@COD, @DESCR)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 10).Value = COD
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 500).Value = DESCR

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal COD As String,
                                     ByVal DESCR As String)

        Dim sql As String = "UPDATE TAB_REGIAO SET NOME_REGIAO=@DESCR " &
                                "Where COD_REGIAO=@COD"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 10).Value = COD
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 500).Value = DESCR

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub ApagarRegistro(ByVal COD As String)


        Dim sql As String = "DELETE FROM TAB_REGIAO Where COD_REGIAO=@COD"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 10).Value = COD

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT * FROM TAB_REGIAO (NOLOCK) ORDER BY COD_REGIAO"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal COD As String) As DataTable

        Dim sql As String = "SELECT * FROM TAB_REGIAO (NOLOCK) WHERE COD_REGIAO=@COD"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 10).Value = COD

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