Imports System.Data
Imports System.Data.SqlClient

Namespace DataBase

    Public Class DbScoreRisco


        ReadOnly _connectionString As String
        Dim _conn As SqlConnection
        Dim _cmd As SqlCommand
        Dim _dt As DataTable


        Public Sub New()
            _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
        End Sub


        Public Sub InserirRegistro(ByVal COD As String, ByVal DESCR As String, ByVal SC_DE As String, ByVal SC_ATE As String)

            Dim sql As String = "INSERT INTO TRISCO(COD, RISCO, SC_DE, SC_ATE) " &
                                "VALUES (@COD, @DESCR)"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 3).Value = COD
            _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 100).Value = DESCR
            _cmd.Parameters.Add("@SC_DE", SqlDbType.VarChar, 100).Value = SC_DE
            _cmd.Parameters.Add("@SC_ATE", SqlDbType.VarChar, 100).Value = SC_ATE

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Sub AtualizarRegistro(ByVal COD As String,
                                     ByVal DESCR As String, ByVal SC_DE As String, ByVal SC_ATE As String)

            Dim sql As String = "UPDATE TRISCO SET DESCR = @DESCR, SC_DE = @SC_DE, SC_ATE = @SC_ATE " &
                                "Where COD=@COD "

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 3).Value = COD
            _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 100).Value = DESCR
            _cmd.Parameters.Add("@SC_DE", SqlDbType.VarChar, 100).Value = SC_DE
            _cmd.Parameters.Add("@SC_ATE", SqlDbType.VarChar, 100).Value = SC_ATE

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Sub ApagarRegistro(ByVal COD As String)


            Dim sql As String = "DELETE FROM TRISCO Where COD=@COD"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 3).Value = COD

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Function CarregarTodosRegistros() As DataTable

            Dim sql As String = "SELECT * FROM TRISCO (NOLOCK) ORDER BY COD"

            Dim da As New SqlDataAdapter(sql, _connectionString)
            _dt = New DataTable

            Try
                da.Fill(_dt)
            Catch ex As Exception

            End Try

            Return _dt

        End Function


        Public Function CarregarRegistro(ByVal COD As String) As DataTable

            Dim sql As String = "SELECT * FROM TRISCO (NOLOCK) WHERE COD=@COD"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 3).Value = COD

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

End Namespace