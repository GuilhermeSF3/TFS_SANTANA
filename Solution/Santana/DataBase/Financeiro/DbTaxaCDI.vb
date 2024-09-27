Imports System.Data
Imports System.Data.SqlClient


Public Class DbTaxaCDI


   ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub


    Public Sub InserirRegistro(ByVal DT_DE As DateTime, _
                               ByVal PRC_CDI As Double)
        ',     ByVal PRC_DIA As Double)

        '        Dim sql As String = "INSERT INTO TCDI(DT_DE, PRC_CDI, PRC_DIA) " & _
        '                           "VALUES (@DT_DE, @PRC_CDI, @PRC_DIA)"
        Dim sql As String = "INSERT INTO TCDI(DT_DE, PRC_CDI) " & _
                           "VALUES (@DT_DE, @PRC_CDI)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE
        _cmd.Parameters.Add("@PRC_CDI", SqlDbType.Float).Value = PRC_CDI
        '_cmd.Parameters.Add("@PRC_DIA", SqlDbType.Float).Value = PRC_DIA

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal DT_DE As DateTime, _
                                 ByVal PRC_CDI As Double)
        ',                                  ByVal PRC_DIA As Double)

        ' Dim sql As String = "UPDATE TCDI SET PRC_CDI=@PRC_CDI, PRC_DIA=@PRC_DIA Where DT_DE=@DT_DE "
        Dim sql As String = "UPDATE TCDI SET PRC_CDI=@PRC_CDI  " & _
                            "Where DT_DE=@DT_DE "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE
        _cmd.Parameters.Add("@PRC_CDI", SqlDbType.Float).Value = PRC_CDI
        '_cmd.Parameters.Add("@PRC_DIA", SqlDbType.Float).Value = PRC_DIA

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub ApagarRegistro(ByVal DT_DE As DateTime)


        Dim sql As String = "DELETE FROM TCDI Where DT_DE=@DT_DE"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT * FROM TCDI ORDER BY DT_DE DESC"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal DT_DE As DateTime) As DataTable

        Dim sql As String = "SELECT * FROM TCDI WHERE DT_DE=@DT_DE"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE

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


