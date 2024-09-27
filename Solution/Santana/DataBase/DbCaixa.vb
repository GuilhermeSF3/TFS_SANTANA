Imports System.Data
Imports System.Data.SqlClient


Public Class DbCaixa


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub


    Public Sub InserirRegistro(ByVal DATAREF As DateTime,
                              ByVal EMPRESA As String,
                              ByVal VALOR As Double)

        Dim sql As String = "INSERT INTO AUX_LANCTO_CAIXA(DATAREF, EMPRESA, VALOR) " &
                            "VALUES (@DATAREF, @EMPRESA, @VALOR)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DATAREF", SqlDbType.SmallDateTime).Value = DATAREF
        _cmd.Parameters.Add("@EMPRESA", SqlDbType.VarChar, 100).Value = EMPRESA
        _cmd.Parameters.Add("@VALOR", SqlDbType.Float).Value = VALOR


        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal DATAREF As DateTime,
                              ByVal EMPRESA As String,
                              ByVal VALOR As Double)

        Dim sql As String = "UPDATE AUX_LANCTO_CAIXA SET VALOR=@VALOR " &
                            "Where EMPRESA=@EMPRESA " &
                            "AND DATAREF=@DATAREF "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)


        _cmd.Parameters.Add("@DATAREF", SqlDbType.SmallDateTime).Value = DATAREF
        _cmd.Parameters.Add("@EMPRESA", SqlDbType.VarChar, 100).Value = EMPRESA
        _cmd.Parameters.Add("@VALOR", SqlDbType.Float).Value = VALOR

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub




    Public Sub ApagarRegistro(ByVal DATAREF As DateTime,
                              ByVal EMPRESA As String)


        Dim sql As String = "DELETE FROM AUX_LANCTO_CAIXA Where DATAREF=@DATAREF AND EMPRESA=@EMPRESA"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DATAREF", SqlDbType.SmallDateTime).Value = DATAREF
        _cmd.Parameters.Add("@EMPRESA", SqlDbType.VarChar, 100).Value = EMPRESA

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub



    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT DATAREF, EMPRESA, VALOR FROM AUX_LANCTO_CAIXA ORDER BY DATAREF DESC, EMPRESA"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal DATAREF As DateTime,
                              ByVal EMPRESA As String) As DataTable

        Dim sql As String = "select * from AUX_LANCTO_CAIXA (NOLOCK) WHERE DATAREF=@DATAREF AND EMPRESA=@EMPRESA"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DATAREF", SqlDbType.SmallDateTime).Value = DATAREF
        _cmd.Parameters.Add("@EMPRESA", SqlDbType.VarChar, 100).Value = EMPRESA

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




