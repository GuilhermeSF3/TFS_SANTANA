Imports System.Data
Imports System.Data.SqlClient


Public Class DbFaixaAnoSpread


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
                                 ByVal ATE As Integer, _
                                 ByVal ORDEM As Integer)

        Dim sql As String = "INSERT INTO FX_ANO(DT_DE, DESCR, DE, ATE, ORDEM) " & _
                            "VALUES (@DT_DE, @DESCR, @DE, @ATE, @ORDEM)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 30).Value = DESCR
        _cmd.Parameters.Add("@DE", SqlDbType.Int).Value = DE
        _cmd.Parameters.Add("@ATE", SqlDbType.Int).Value = ATE
        _cmd.Parameters.Add("@ORDEM", SqlDbType.Int).Value = ORDEM

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal DT_DE As DateTime, _
                                 ByVal DESCR As String, _
                                 ByVal DE As Integer, _
                                 ByVal ATE As Integer, _
                                 ByVal ORDEM As Integer)

        Dim sql As String = "UPDATE FX_ANO SET DESCR=@DESCR, ATE=@ATE, ORDEM=@ORDEM " & _
                            "WHERE DT_DE=@DT_DE AND DE=@DE"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 30).Value = DESCR
        _cmd.Parameters.Add("@DE", SqlDbType.Int).Value = DE
        _cmd.Parameters.Add("@ATE", SqlDbType.Int).Value = ATE
        _cmd.Parameters.Add("@ORDEM", SqlDbType.Int).Value = ORDEM

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub ApagarRegistro(ByVal DT_DE As DateTime, ByVal DE As Integer)


        Dim sql As String = "DELETE FROM FX_ANO WHERE DT_DE=@DT_DE AND DE=@DE"

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

        Dim sql As String = "SELECT * FROM FX_ANO (NOLOCK) ORDER BY DT_DE desc"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal DT_DE As DateTime, ByVal DE As Integer) As DataTable

        Dim sql As String = "SELECT * FROM FX_ANO (NOLOCK) WHERE DT_DE=@DT_DE AND DE=@DE order by DT_DE desc"

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


