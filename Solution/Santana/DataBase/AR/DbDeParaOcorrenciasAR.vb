Imports System.Data
Imports System.Data.SqlClient

Namespace DataBase.AR


    Public Class DbDeParaOcorrenciasAR


        ReadOnly _connectionString As String
        Dim _conn As SqlConnection
        Dim _cmd As SqlCommand
        Dim _dt As DataTable


        Public Sub New()
            _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
        End Sub


        Public Sub InserirRegistro(ByVal COD_CART As String, _
                                   ByVal OCORRENCIA_CART As String,
                                   ByVal COD_FC As String, _
                                   ByVal OCORRENCIA_FC As String)

            Dim sql As String = "INSERT INTO AR_ocorrencia_de_para(COD_RETORNO, OCORRENCIA_RET, COD_FC, OCORRENCIA_FC) " & _
                                "VALUES (@COD_CART, @OCORRENCIA_CART, @COD_FC, @OCORRENCIA_FC)"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD_CART", SqlDbType.VarChar, 20).Value = COD_CART
            _cmd.Parameters.Add("@OCORRENCIA_CART", SqlDbType.VarChar, 300).Value = OCORRENCIA_CART
            _cmd.Parameters.Add("@COD_FC", SqlDbType.VarChar, 20).Value = COD_FC
            _cmd.Parameters.Add("@OCORRENCIA_FC", SqlDbType.VarChar, 300).Value = OCORRENCIA_FC

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Sub AtualizarRegistro(ByVal COD_CART As String, _
                                   ByVal OCORRENCIA_CART As String,
                                   ByVal COD_FC As String, _
                                   ByVal OCORRENCIA_FC As String)

            Dim sql As String = "UPDATE AR_ocorrencia_de_para SET OCORRENCIA_RET=@OCORRENCIA_CART, COD_FC=@COD_FC, OCORRENCIA_FC=@OCORRENCIA_FC " & _
                                "Where COD_RETORNO=@COD_CART "

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD_CART", SqlDbType.VarChar, 20).Value = COD_CART
            _cmd.Parameters.Add("@OCORRENCIA_CART", SqlDbType.VarChar, 300).Value = OCORRENCIA_CART
            _cmd.Parameters.Add("@COD_FC", SqlDbType.VarChar, 20).Value = COD_FC
            _cmd.Parameters.Add("@OCORRENCIA_FC", SqlDbType.VarChar, 300).Value = OCORRENCIA_FC

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Sub ApagarRegistro(ByVal COD_CART As String)


            Dim sql As String = "DELETE FROM AR_ocorrencia_de_para Where COD_RETORNO=@COD_CART"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD_CART", SqlDbType.VarChar, 20).Value = COD_CART

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Function CarregarTodosRegistros() As DataTable

            Dim sql As String = "SELECT * FROM AR_ocorrencia_de_para (NOLOCK) ORDER BY COD_RETORNO"

            Dim da As New SqlDataAdapter(sql, _connectionString)
            _dt = New DataTable

            Try
                da.Fill(_dt)
            Catch ex As Exception

            End Try

            Return _dt

        End Function


        Public Function CarregarRegistro(ByVal COD_CART As String) As DataTable

            Dim sql As String = "SELECT * FROM AR_ocorrencia_de_para (NOLOCK) WHERE COD_RETORNO=@COD_CART"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD_CART", SqlDbType.VarChar, 20).Value = COD_CART

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