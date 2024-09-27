Imports System.Data
Imports System.Data.SqlClient

Public Class DbTaxaOperacaoXMeta

    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub


    Public Sub InserirRegistro(ByVal DT_DE As DateTime, _
                              ByVal DESCR As String, _
                              ByVal vlr_prod As Double, _
                              ByVal META_DE As Double, _
                              ByVal META_ATE As Double, _
                              ByVal PRC_COMISSAO As Double, _
                              ByVal ORDEM As Integer
                              )

        Dim sql As String = "INSERT INTO FX_PJ_META(DT_DE, DESCR,vlr_prod,META_DE,META_ATE,PRC_COMISSAO,ORDEM) " & _
                            "VALUES (@DT_DE, @DESCR,@vlr_prod, @META_DE,@META_ATE,@PRC_COMISSAO,@ORDEM)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 100).Value = DESCR
        _cmd.Parameters.Add("@vlr_prod", SqlDbType.Float).Value = vlr_prod
        _cmd.Parameters.Add("@META_DE", SqlDbType.Float).Value = META_DE
        _cmd.Parameters.Add("@META_ATE", SqlDbType.Float).Value = META_ATE
        _cmd.Parameters.Add("@PRC_COMISSAO", SqlDbType.Float).Value = PRC_COMISSAO
        _cmd.Parameters.Add("@ORDEM", SqlDbType.Int).Value = ORDEM


        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal DT_DE As DateTime, _
                              ByVal DESCR As String, _
                              ByVal vlr_prod As Double, _
                              ByVal META_DE As Double, _
                              ByVal META_ATE As Double, _
                              ByVal PRC_COMISSAO As Double, _
                              ByVal ORDEM As Integer)

        Dim sql As String = "UPDATE FX_PJ_META SET vlr_prod=@vlr_prod, META_DE=@META_DE, META_ATE=@META_ATE, PRC_COMISSAO =@PRC_COMISSAO,ORDEM=@ORDEM " & _
                            "Where DT_DE=@DT_DE " & _
                            "AND DESCR=@DESCR "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)


        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE
        _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 100).Value = DESCR
        _cmd.Parameters.Add("@vlr_prod", SqlDbType.Float).Value = vlr_prod
        _cmd.Parameters.Add("@META_DE", SqlDbType.Float).Value = META_DE
        _cmd.Parameters.Add("@META_ATE", SqlDbType.Float).Value = META_ATE
        _cmd.Parameters.Add("@PRC_COMISSAO", SqlDbType.Float).Value = PRC_COMISSAO
        _cmd.Parameters.Add("@ORDEM", SqlDbType.Int).Value = ORDEM

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub




    Public Sub ApagarRegistro(ByVal DT_DE As DateTime, _
                              ByVal DESCR As String)


        Dim sql As String = "DELETE FROM FX_PJ_META Where DT_DE=@DT_DE AND DESCR=@DESCR"

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

        Dim sql As String = "select * from FX_PJ_META (NOLOCK) ORDER BY DT_DE DESC,DESCR"

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

        Dim sql As String = "select * from FX_PJ_META (NOLOCK) WHERE DT_DE=@DT_DE AND DESCR=@DESCR"

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



