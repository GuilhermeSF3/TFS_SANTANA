Imports System.Data
Imports System.Data.SqlClient

Public Class DbFixo

    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub


    Public Sub InserirRegistro(ByVal DT_DE As DateTime, _
                              ByVal DESCR As String, _
                              ByVal PRAZO_DE As Integer, _
                              ByVal PRAZO_ATE As Integer, _
                              ByVal VALOR_DE As Double, _
                              ByVal VALOR_ATE As Double, _
                              ByVal VLR_AJUDA As Double, _
                              ByVal ORDEM As Integer)

        Dim sql As String = "INSERT INTO  FX_PJ_AJUDA(DT_DE, DESCR,PRAZO_DE,PRAZO_ATE,VALOR_DE,VALOR_ATE,VLR_AJUDA,ORDEM) " & _
                            "VALUES (@DT_DE, @DESCR,@PRAZO_DE,@PRAZO_ATE,@VALOR_DE,@VALOR_ATE,@VLR_AJUDA,@ORDEM)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 100).Value = DESCR
        _cmd.Parameters.Add("@PRAZO_DE", SqlDbType.Int).Value = PRAZO_DE
        _cmd.Parameters.Add("@PRAZO_ATE", SqlDbType.Int).Value = PRAZO_ATE
        _cmd.Parameters.Add("@VALOR_DE", SqlDbType.Float).Value = VALOR_DE
        _cmd.Parameters.Add("@VALOR_ATE", SqlDbType.Float).Value = VALOR_ATE
        _cmd.Parameters.Add("@VLR_AJUDA", SqlDbType.Float).Value = VLR_AJUDA
        _cmd.Parameters.Add("@ORDEM", SqlDbType.Int).Value = ORDEM


        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal DT_DE As DateTime, _
                              ByVal DESCR As String, _
                              ByVal PRAZO_DE As Integer, _
                              ByVal PRAZO_ATE As Integer, _
                              ByVal VALOR_DE As Double, _
                              ByVal VALOR_ATE As Double, _
                              ByVal VLR_AJUDA As Double, _
                              ByVal ORDEM As Integer)


        Dim sql As String = "UPDATE  FX_PJ_AJUDA SET PRAZO_DE=@PRAZO_DE, PRAZO_ATE=@PRAZO_ATE, VALOR_DE=@VALOR_DE, VALOR_ATE =@VALOR_ATE,VLR_AJUDA=@VLR_AJUDA,ORDEM=@ORDEM " & _
                            "Where DT_DE=@DT_DE " & _
                            "AND DESCR=@DESCR "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 100).Value = DESCR
        _cmd.Parameters.Add("@PRAZO_DE", SqlDbType.Int).Value = PRAZO_DE
        _cmd.Parameters.Add("@PRAZO_ATE", SqlDbType.Int).Value = PRAZO_ATE
        _cmd.Parameters.Add("@VALOR_DE", SqlDbType.Float).Value = VALOR_DE
        _cmd.Parameters.Add("@VALOR_ATE", SqlDbType.Float).Value = VALOR_ATE
        _cmd.Parameters.Add("@VLR_AJUDA", SqlDbType.Float).Value = VLR_AJUDA
        _cmd.Parameters.Add("@ORDEM", SqlDbType.Int).Value = ORDEM


        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub




    Public Sub ApagarRegistro(ByVal DT_DE As DateTime, _
                              ByVal DESCR As String)


        Dim sql As String = "DELETE FROM  FX_PJ_AJUDA Where DT_DE=@DT_DE AND DESCR=@DESCR"

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

        Dim sql As String = "select * from  FX_PJ_AJUDA (NOLOCK) ORDER BY DT_DE DESC,DESCR"

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

        Dim sql As String = "select * from  FX_PJ_AJUDA (NOLOCK) WHERE DT_DE=@DT_DE AND DESCR=@DESCR"

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



