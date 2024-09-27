Imports System.Data
Imports System.Data.SqlClient

Namespace DataBase.AR


    Public Class DbSegmento


        ReadOnly _connectionString As String
        Dim _conn As SqlConnection
        Dim _cmd As SqlCommand
        Dim _dt As DataTable


        Public Sub New()
            _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
        End Sub


        Public Sub InserirRegistro(ByVal COD As String, _
                                   ByVal DESCR As String)

            Dim sql As String = "INSERT INTO AR_SEGMENTO(COD, DESCR) " & _
                                "VALUES (@COD, @DESCR)"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 3).Value = COD
            _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 100).Value = DESCR

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Sub AtualizarRegistro(ByVal COD As String, _
                                     ByVal DESCR As String)

            Dim sql As String = "UPDATE AR_SEGMENTO SET DESCR=@DESCR " & _
                                "Where COD=@COD "

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 3).Value = COD
            _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 100).Value = DESCR

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Sub ApagarRegistro(ByVal COD As string)


            Dim sql As String = "DELETE FROM AR_SEGMENTO Where COD=@COD"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 3).Value = COD

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Function CarregarTodosRegistros() As DataTable

            Dim sql As String = "SELECT * FROM AR_SEGMENTO (NOLOCK) ORDER BY COD"

            Dim da As New SqlDataAdapter(sql, _connectionString)
            _dt = New DataTable

            Try
                da.Fill(_dt)
            Catch ex As Exception

            End Try

            Return _dt

        End Function


        Public Function CarregarRegistro(ByVal COD As string) As DataTable

            Dim sql As String = "SELECT * FROM AR_SEGMENTO (NOLOCK) WHERE COD=@COD"

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

End NameSpace