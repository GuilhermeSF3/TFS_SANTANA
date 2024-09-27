Imports System.Data
Imports System.Data.SqlClient

Public Class DbTcOperacaoXMeta

    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub


    Public Sub InserirRegistro(ByVal DT_DE As DateTime, _
                              ByVal DESCR As String, _
                              ByVal META_DE As Double, _
                              ByVal META_ATE As Double, _
                              ByVal VLR_TC As Double, _
                              ByVal ORDEM As Integer
                              )

        Dim sql As String = "INSERT INTO FX_PJ_TC(DT_DE, DESCR,META_DE,META_ATE,VLR_TC,ORDEM) " & _
                            "VALUES (@DT_DE, @DESCR, @META_DE,@META_ATE,@VLR_TC,@ORDEM)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 100).Value = DESCR
        _cmd.Parameters.Add("@META_DE", SqlDbType.Float).Value = META_DE
        _cmd.Parameters.Add("@META_ATE", SqlDbType.Float).Value = META_ATE
        _cmd.Parameters.Add("@VLR_TC", SqlDbType.Float).Value = VLR_TC
        _cmd.Parameters.Add("@ORDEM", SqlDbType.Int).Value = ORDEM


        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal DT_DE As DateTime, _
                              ByVal DESCR As String, _
                              ByVal META_DE As Double, _
                              ByVal META_ATE As Double, _
                              ByVal VLR_TC As Double, _
                              ByVal ORDEM As Integer)

        Dim sql As String = "UPDATE FX_PJ_TC SET META_DE=@META_DE, META_ATE=@META_ATE, VLR_TC =@VLR_TC,ORDEM=@ORDEM " & _
                            "Where DT_DE=@DT_DE " & _
                            "AND DESCR=@DESCR "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)


        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 100).Value = DESCR
        _cmd.Parameters.Add("@META_DE", SqlDbType.Float).Value = META_DE
        _cmd.Parameters.Add("@META_ATE", SqlDbType.Float).Value = META_ATE
        _cmd.Parameters.Add("@VLR_TC", SqlDbType.Float).Value = VLR_TC
        _cmd.Parameters.Add("@ORDEM", SqlDbType.Int).Value = ORDEM

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub




    Public Sub ApagarRegistro(ByVal DT_DE As DateTime, _
                              ByVal DESCR As String)


        Dim sql As String = "DELETE FROM FX_PJ_TC Where DT_DE=@DT_DE AND DESCR=@DESCR"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 100).Value = DESCR


        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub



    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "select * from FX_PJ_TC (NOLOCK) ORDER BY DT_DE,ORDEM"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal DT_DE As DateTime, _
                              ByVal DESCR As String) As DataTable

        Dim sql As String = "select * from FX_PJ_TC (NOLOCK) WHERE DT_DE=@DT_DE AND DESCR=@DESCR"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 100).Value = DESCR



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



