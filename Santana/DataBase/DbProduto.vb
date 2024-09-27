Imports System.Data
Imports System.Data.SqlClient


Public Class DbProduto


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub


    Public Sub InserirRegistro(ByVal cod_prod As String, _
                               ByVal descr_prod As String)

        Dim sql As String = "INSERT INTO tproduto(cod_prod, descr_prod) " & _
                            "VALUES (@cod_prod, @descr_prod)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod_prod", SqlDbType.VarChar, 4).Value = cod_prod
        _cmd.Parameters.Add("@descr_prod", SqlDbType.VarChar, 100).Value = descr_prod

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal cod_prod As String, _
                                 ByVal descr_prod As String)

        Dim sql As String = "UPDATE tproduto SET descr_prod=@descr_prod " & _
                            "Where cod_prod=@cod_prod "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod_prod", SqlDbType.VarChar, 4).Value = cod_prod
        _cmd.Parameters.Add("@descr_prod", SqlDbType.VarChar, 100).Value = descr_prod

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub ApagarRegistro(ByVal cod_prod As String)


        Dim sql As String = "DELETE FROM tproduto Where cod_prod=@cod_prod"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod_prod", SqlDbType.VarChar, 4).Value = cod_prod

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT * FROM tproduto (NOLOCK) ORDER BY cod_prod"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal cod_prod As String) As DataTable

        Dim sql As String = "SELECT * FROM tproduto (NOLOCK) WHERE cod_prod=@cod_prod"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod_prod", SqlDbType.VarChar, 4).Value = cod_prod

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


