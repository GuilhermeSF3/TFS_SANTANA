Imports System.Data
Imports System.Data.SqlClient

Namespace DataBase.AR


    Public Class DbMotivo


        ReadOnly _connectionString As String
        Dim _conn As SqlConnection
        Dim _cmd As SqlCommand
        Dim _dt As DataTable


        Public Sub New()
            _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
        End Sub


        Public Sub InserirRegistro(ByVal COD As Int32, _
                                   ByVal DESCR As String)

            Dim sql As String = "INSERT INTO AR_MOTIVO(COD, DESCR) " & _
                                "VALUES (@COD, @DESCR)"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.Int).Value = COD
            _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 100).Value = DESCR

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Sub AtualizarRegistro(ByVal COD As Int32, _
                                     ByVal DESCR As String)

            Dim sql As String = "UPDATE AR_MOTIVO SET DESCR=@DESCR " & _
                                "Where COD=@COD "

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.Int).Value = COD
            _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 100).Value = DESCR

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Sub ApagarRegistro(ByVal COD As Int32)


            Dim sql As String = "DELETE FROM AR_MOTIVO Where COD=@COD"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.Int).Value = COD

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Function CarregarTodosRegistros() As DataTable

            Dim sql As String = "SELECT * FROM AR_MOTIVO (NOLOCK) ORDER BY COD"

            Dim da As New SqlDataAdapter(sql, _connectionString)
            _dt = New DataTable

            Try
                da.Fill(_dt)
            Catch ex As Exception

            End Try

            Return _dt

        End Function


        Public Function CarregarRegistro(ByVal COD As Int32) As DataTable

            Dim sql As String = "SELECT * FROM AR_MOTIVO (NOLOCK) WHERE COD=@COD"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.Int).Value = COD

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

End NameSpace