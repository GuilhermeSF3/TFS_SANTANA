Imports System.Data
Imports System.Data.SqlClient


Public Class DbModelo


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub


    Public Sub InserirRegistro(ByVal nroper As String, _
                               ByVal veic_mod As String, _
                               ByVal veic_mod2 As String)

        Dim sql As String = "INSERT INTO CBFIN2(nroper, veic_mod, veic_mod2) " & _
                            "VALUES (@nroper, @veic_mod,@veic_mod2)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@nroper", SqlDbType.VarChar, 15).Value = nroper
        _cmd.Parameters.Add("@veic_mod", SqlDbType.VarChar, 100).Value = veic_mod
        _cmd.Parameters.Add("@veic_mod2", SqlDbType.VarChar, 100).Value = veic_mod2

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal nroper As String, _
                                 ByVal veic_mod As String, _
                                 ByVal veic_mod2 As String)

        Dim sql As String = "UPDATE CBFIN2 SET veic_mod = @veic_mod,veic_mod2 = @veic_mod2 " & _
                            "Where nroper=@nroper "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@nroper", SqlDbType.VarChar, 15).Value = nroper
        _cmd.Parameters.Add("@veic_mod", SqlDbType.VarChar, 100).Value = veic_mod
        _cmd.Parameters.Add("@veic_mod2", SqlDbType.VarChar, 100).Value = veic_mod2

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub ApagarRegistro(ByVal nroper As String)


        Dim sql As String = "DELETE FROM CBFIN2 Where nroper=@nroper"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@nroper", SqlDbType.VarChar, 15).Value = nroper

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT max(nroper) as nroper,veic_mod, veic_mod2 FROM CBFIN2 (NOLOCK) group by veic_mod, veic_mod2 ORDER BY nroper"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal nroper As String) As DataTable

        Dim sql As String = "SELECT * FROM CBFIN2 (NOLOCK) WHERE nroper=@nroper"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@nroper", SqlDbType.VarChar, 4).Value = nroper

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


