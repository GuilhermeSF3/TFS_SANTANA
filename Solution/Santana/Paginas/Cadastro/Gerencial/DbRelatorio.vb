Imports System.Data
Imports System.Data.SqlClient


Public Class DbRelatorio


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub




    Public Sub InserirRegistro(ByVal COD_RPT As String, _
                              ByVal DESCR As String, _
                              ByVal MODELO As Int32)

        Dim sql As String = "INSERT INTO GER_RELATORIO(COD_RPT, DESCR, MODELO) " & _
                            "VALUES (@COD_RPT, @DESCR, @MODELO)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD_RPT", SqlDbType.VarChar, 4).Value = COD_RPT
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 200).Value = DESCR
        _cmd.Parameters.Add("@MODELO", SqlDbType.Int).Value = MODELO


        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal COD_RPT As String, _
                              ByVal DESCR As String, _
                              ByVal MODELO As Int32)

        Dim sql As String = "UPDATE GER_RELATORIO SET DESCR=@DESCR, " & _
                            "MODELO=@MODELO " & _
                            "WHERE COD_RPT=@COD_RPT "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD_RPT", SqlDbType.VarChar, 4).Value = COD_RPT
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 200).Value = DESCR
        _cmd.Parameters.Add("@MODELO", SqlDbType.Int).Value = MODELO

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub ApagarRegistro(ByVal COD_RPT As String)


        Dim sql As String = "DELETE FROM GER_RELATORIO Where COD_RPT=@COD_RPT"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD_RPT", SqlDbType.VarChar, 4).Value = COD_RPT

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT * FROM GER_RELATORIO (NOLOCK) ORDER BY COD_RPT"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal COD_RPT As String) As DataTable

        Dim sql As String = "SELECT * FROM GER_CONTA_CALC (NOLOCK) WHERE COD_RPT=@COD_RPT"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD_RPT", SqlDbType.VarChar, 4).Value = COD_RPT

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


