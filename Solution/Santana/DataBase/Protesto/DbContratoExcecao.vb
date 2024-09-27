Imports System.Data
Imports System.Data.SqlClient

Namespace DataBase.Protesto


    Public Class DbContratoExcecao


        ReadOnly _connectionString As String
        Dim _conn As SqlConnection
        Dim _cmd As SqlCommand
        Dim _dt As DataTable


        Public Sub New()
            _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
        End Sub


        Public Sub InserirRegistro(ByVal CONTRATO As String, _
                                   ByVal OBS As String)

            Dim sql As String = "INSERT INTO prot_contratos (CONTRATO, OBS) " & _
                                "VALUES (@CONTRATO, @OBS)"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@CONTRATO", SqlDbType.VarChar, 20).Value = CONTRATO
            _cmd.Parameters.Add("@OBS", SqlDbType.VarChar, 300).Value = OBS

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Sub AtualizarRegistro(ByVal CONTRATO As String, _
                                     ByVal OBS As String)

            Dim sql As String = "UPDATE prot_contratos SET OBS=@OBS " & _
                                "Where CONTRATO=@CONTRATO "

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@CONTRATO", SqlDbType.VarChar, 20).Value = CONTRATO
            _cmd.Parameters.Add("@OBS", SqlDbType.VarChar, 300).Value = OBS

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Sub ApagarRegistro(ByVal CONTRATO As String)


            Dim sql As String = "DELETE FROM prot_contratos Where CONTRATO=@CONTRATO"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@CONTRATO", SqlDbType.VarChar, 20).Value = CONTRATO

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Function CarregarTodosRegistros() As DataTable

            Dim sql As String = "SELECT * FROM prot_contratos (NOLOCK) ORDER BY CONTRATO"

            Dim da As New SqlDataAdapter(sql, _connectionString)
            _dt = New DataTable

            Try
                da.Fill(_dt)
            Catch ex As Exception

            End Try

            Return _dt

        End Function


        Public Function CarregarRegistro(ByVal CONTRATO As String) As DataTable

            Dim sql As String = "SELECT * FROM prot_contratos (NOLOCK) WHERE CONTRATO=@CONTRATO"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@CONTRATO", SqlDbType.VarChar, 20).Value = CONTRATO

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