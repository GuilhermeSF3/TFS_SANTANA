Imports System.Data
Imports System.Data.SqlClient


Public Class DbCubo


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub


    Public Sub InserirRegistro(ByVal CUBO As Int32, _
                               ByVal DESCR As String)

        Dim sql As String = "INSERT INTO tcUBO(CUBO, DESCR) " & _
                            "VALUES (@CUBO, @DESCR)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@CUBO", SqlDbType.Int).Value = CUBO
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 100).Value = DESCR

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal CUBO As Int32, _
                                 ByVal DESCR As String)

        Dim sql As String = "UPDATE tcUBO SET DESCR=@DESCR " & _
                            "Where CUBO=@CUBO "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@CUBO", SqlDbType.Int).Value = CUBO
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 100).Value = DESCR

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub ApagarRegistro(ByVal CUBO As Int32)


        Dim sql As String = "DELETE FROM tcUBO Where CUBO=@CUBO"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@CUBO", SqlDbType.Int).Value = CUBO

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT * FROM tcUBO (NOLOCK) ORDER BY CUBO"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal CUBO As Int32) As DataTable

        Dim sql As String = "SELECT * FROM tcUBO (NOLOCK) WHERE CUBO=@CUBO"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@CUBO", SqlDbType.Int).Value = CUBO

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


