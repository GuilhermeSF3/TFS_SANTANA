Imports System.Data
Imports System.Data.SqlClient


Public Class DbFeriados


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub


    Public Sub InserirRegistro(ByVal DT_FERI As DateTime,
                              ByVal DESCR As String)

        Dim sql As String = "INSERT INTO TB_FERIADOS(DT_FERI, DESCR_FERI) " &
                            "VALUES (@DT_FERI, @DESCR)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_FERI", SqlDbType.SmallDateTime).Value = DT_FERI
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 100).Value = DESCR


        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal DT_FERI As DateTime,
                              ByVal DESCR As String)

        Dim sql As String = "UPDATE TB_FERIADOS SET DESCR_FERI=@DESCR " &
                            "Where DT_FERI=@DT_FERI "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)


        _cmd.Parameters.Add("@DT_FERI", SqlDbType.SmallDateTime).Value = DT_FERI
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 100).Value = DESCR

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub




    Public Sub ApagarRegistro(ByVal DT_FERI As DateTime)


        Dim sql As String = "DELETE FROM TB_FERIADOS Where DT_FERI=@DT_FERI"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_FERI", SqlDbType.SmallDateTime).Value = DT_FERI

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub



    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT DT_FERI, DESCR_FERI FROM TB_FERIADOS ORDER BY DT_FERI DESC"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal DATAREF As DateTime) As DataTable

        Dim sql As String = "select * from TB_FERIADOS (NOLOCK) WHERE DT_FERI=@DT_FERI"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_FERI", SqlDbType.SmallDateTime).Value = DATAREF

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




