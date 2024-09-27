Imports System.Data
Imports System.Data.SqlClient


Public Class DbContas


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub



    Public Sub InserirRegistro(ByVal COD_CONTA As String, _
                              ByVal CONTA_FMT As String, _
                              ByVal DESCR As String, _
                              ByVal TTL As Boolean)

        Dim sql As String = "INSERT INTO GER_CONTA_CALC(COD_CONTA, CONTA_FMT, DESCR, TTL) " & _
                            "VALUES (@COD_CONTA, @CONTA_FMT, @DESCR, @TTL)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD_CONTA", SqlDbType.VarChar, 30).Value = COD_CONTA
        _cmd.Parameters.Add("@CONTA_FMT", SqlDbType.VarChar, 50).Value = CONTA_FMT
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 200).Value = DESCR
        _cmd.Parameters.Add("@TTL", SqlDbType.Bit).Value = TTL


        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal COD_CONTA As String, _
                              ByVal CONTA_FMT As String, _
                              ByVal DESCR As String, _
                              ByVal TTL As Boolean)

        Dim sql As String = "UPDATE GER_CONTA_CALC SET CONTA_FMT=@CONTA_FMT, " & _
                            "DESCR=@DESCR, " & _
                            "TTL=@TTL " & _
                            "WHERE COD_CONTA=@COD_CONTA "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD_CONTA", SqlDbType.VarChar, 30).Value = COD_CONTA
        _cmd.Parameters.Add("@CONTA_FMT", SqlDbType.VarChar, 50).Value = CONTA_FMT
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 200).Value = DESCR
        _cmd.Parameters.Add("@TTL", SqlDbType.Bit).Value = TTL

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub ApagarRegistro(ByVal COD_CONTA As String)


        Dim sql As String = "DELETE FROM GER_CONTA_CALC Where COD_CONTA=@COD_CONTA"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD_CONTA", SqlDbType.VarChar, 30).Value = COD_CONTA

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT * FROM GER_CONTA_CALC (NOLOCK) ORDER BY COD_CONTA"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal COD_CONTA As String) As DataTable

        Dim sql As String = "SELECT * FROM GER_CONTA_CALC (NOLOCK) WHERE COD_CONTA=@COD_CONTA"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD_CONTA", SqlDbType.VarChar, 30).Value = COD_CONTA

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


