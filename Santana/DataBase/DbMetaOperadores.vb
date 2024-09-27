Imports System.Data
Imports System.Data.SqlClient


Public Class DbMetaOperadores


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub


    Public Sub InserirRegistro(ByVal DT_DE As DateTime, _
                              ByVal OPERADOR As String, _
                              ByVal VLR_TC As Double)

        Dim sql As String = "INSERT INTO FX_PJ_META_VLR(DT_DE, OPERADOR, VLR_TC) " & _
                            "VALUES (@DT_DE, @OPERADOR, @VLR_TC)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE
        _cmd.Parameters.Add("@OPERADOR", SqlDbType.VarChar, 100).Value = OPERADOR
        _cmd.Parameters.Add("@VLR_TC", SqlDbType.Float).Value = VLR_TC


        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal DT_DE As DateTime, _
                              ByVal OPERADOR As String, _
                              ByVal VLR_TC As Double)

        Dim sql As String = "UPDATE FX_PJ_META_VLR SET VLR_TC=@VLR_TC " & _
                            "Where OPERADOR=@OPERADOR " & _
                            "AND DT_DE=@DT_DE "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)


        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE
        _cmd.Parameters.Add("@OPERADOR", SqlDbType.VarChar, 100).Value = OPERADOR
        _cmd.Parameters.Add("@VLR_TC", SqlDbType.Float).Value = VLR_TC

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub




    Public Sub ApagarRegistro(ByVal DT_DE As DateTime, _
                              ByVal OPERADOR As String)


        Dim sql As String = "DELETE FROM FX_PJ_META_VLR Where DT_DE=@DT_DE AND OPERADOR=@OPERADOR"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE
        _cmd.Parameters.Add("@OPERADOR", SqlDbType.VarChar, 100).Value = OPERADOR

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub



    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "select * from FX_PJ_META_VLR (NOLOCK) ORDER BY DT_DE,OPERADOR"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal DT_DE As DateTime, _
                              ByVal OPERADOR As String) As DataTable

        Dim sql As String = "select * from FX_PJ_META_VLR (NOLOCK) WHERE DT_DE=@DT_DE AND OPERADOR=@OPERADOR"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DE", SqlDbType.SmallDateTime).Value = DT_DE
        _cmd.Parameters.Add("@OPERADOR", SqlDbType.VarChar, 100).Value = OPERADOR

        _cmd.Prepare()

        Dim da As New SqlDataAdapter(_cmd)

        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function



    Public Sub CarregarOperador(ByVal ddlOperador As DropDownList)

        Dim sql As String = "select OPER_ABV_NOM from TB_OPER (nolock) order by 1"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)


        Dim ddlValues As SqlDataReader
        ddlValues = _cmd.ExecuteReader()

        ddlOperador.DataSource = ddlValues
        ddlOperador.DataValueField = "OPER_ABV_NOM"
        ddlOperador.DataTextField = "OPER_ABV_NOM"
        ddlOperador.DataBind()

        ddlOperador.SelectedIndex = 0

        ddlValues.Close()

        _conn.Close()
        _conn.Dispose()

    End Sub

End Class




