Imports System.Data
Imports System.Data.SqlClient


Public Class DbUsu_Services


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub

    Public Sub InserirRegistro(ByVal codigo As Integer, _
                              ByVal nome As String)

        Dim sql As String = "INSERT INTO tbl_svc(codigo, nome) " & _
                            "VALUES (@codigo, @nome)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codigo", SqlDbType.VarChar, 4).Value = codigo
        _cmd.Parameters.Add("@nome", SqlDbType.VarChar, 100).Value = nome

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal cod As String, _
                              ByVal descr As String)

        Dim sql As String = "UPDATE Tbl_Svc SET nome=@descr " & _
                            "Where codigo=@cod "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod", SqlDbType.VarChar, 4).Value = cod
        _cmd.Parameters.Add("@descr", SqlDbType.VarChar, 100).Value = descr

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub ApagarRegistro(ByVal cod As String)


        Dim sql As String = "DELETE FROM Tbl_Svc Where codigo=@cod"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod", SqlDbType.VarChar, 4).Value = cod

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT * FROM Tbl_Svc ORDER BY NOME"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal cod_prod As String) As DataTable

        Dim sql As String = "SELECT * FROM Tbl_Svc (NOLOCK) WHERE codIGO=@cod_prod"

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


