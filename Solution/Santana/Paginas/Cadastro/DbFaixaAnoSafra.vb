Imports System.Data
Imports System.Data.SqlClient


Public Class DbFaixaAnoSafra


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub


    Public Sub InserirRegistro(ByVal DT_DE As DateTime, _
                                 ByVal DESCR As String, _
                                 ByVal DE As Integer, _
                                 ByVal ATE As Integer)

        Dim sql As String = "INSERT INTO FX_ANO_safra(DT_DE, DESCR, DE, ATE) " & _
                            "VALUES (@DT_DE, @DESCR, @DE, @ATE)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 30).Value = DESCR
        _cmd.Parameters.Add("@DE", SqlDbType.Int).Value = DE
        _cmd.Parameters.Add("@ATE", SqlDbType.Int).Value = ATE

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal DT_DE As DateTime, _
                                 ByVal DESCR As String, _
                                 ByVal DE As Integer, _
                                 ByVal ATE As Integer)

        Dim sql As String = "UPDATE FX_ANO_safra SET DESCR=@DESCR, ATE=@ATE " & _
                            "WHERE DT_DE=@DT_DE AND DE=@DE"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 30).Value = DESCR
        _cmd.Parameters.Add("@DE", SqlDbType.Int).Value = DE
        _cmd.Parameters.Add("@ATE", SqlDbType.Int).Value = ATE

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub ApagarRegistro(ByVal DT_DE As DateTime, ByVal DE As Integer)


        Dim sql As String = "DELETE FROM FX_ANO_safra WHERE DT_DE=@DT_DE AND DE=@DE"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE
        _cmd.Parameters.Add("@DE", SqlDbType.Int).Value = DE

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT * FROM FX_ANO_safra ORDER BY DT_DE desc"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal DT_DE As DateTime, ByVal DE As Integer) As DataTable

        Dim sql As String = "SELECT * FROM FX_ANO_safra WHERE DT_DE=@DT_DE AND DE=@DE order by DT_DE desc"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE
        _cmd.Parameters.Add("@DE", SqlDbType.Int).Value = DE

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


