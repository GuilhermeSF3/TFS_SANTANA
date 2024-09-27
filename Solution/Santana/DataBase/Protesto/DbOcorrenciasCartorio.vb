Imports System.Data
Imports System.Data.SqlClient

Namespace DataBase.Protesto


    Public Class DbOcorrenciasCartorio


        ReadOnly _connectionString As String
        Dim _conn As SqlConnection
        Dim _cmd As SqlCommand
        Dim _dt As DataTable


        Public Sub New()
            _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
        End Sub


        Public Sub InserirRegistro(ByVal COD As String, _
                                   ByVal OCORRENCIA As String)

            Dim sql As String = "INSERT INTO prot_ocorrencia_cart(COD, OCORRENCIA) " & _
                                "VALUES (@COD, @OCORRENCIA)"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 20).Value = COD
            _cmd.Parameters.Add("@OCORRENCIA", SqlDbType.VarChar, 300).Value = OCORRENCIA

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Sub AtualizarRegistro(ByVal COD As String, _
                                     ByVal OCORRENCIA As String)

            Dim sql As String = "UPDATE prot_ocorrencia_cart SET OCORRENCIA=@OCORRENCIA " & _
                                "Where COD=@COD "

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 20).Value = COD
            _cmd.Parameters.Add("@OCORRENCIA", SqlDbType.VarChar, 300).Value = OCORRENCIA

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Sub ApagarRegistro(ByVal COD As String)


            Dim sql As String = "DELETE FROM prot_ocorrencia_cart Where COD=@COD"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 20).Value = COD

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Function CarregarTodosRegistros() As DataTable

            Dim sql As String = "SELECT * FROM prot_ocorrencia_cart (NOLOCK) ORDER BY COD"

            Dim da As New SqlDataAdapter(sql, _connectionString)
            _dt = New DataTable

            Try
                da.Fill(_dt)
            Catch ex As Exception

            End Try

            Return _dt

        End Function


        Public Function CarregarRegistro(ByVal COD As String) As DataTable

            Dim sql As String = "SELECT * FROM prot_ocorrencia_cart (NOLOCK) WHERE COD=@COD"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 20).Value = COD

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