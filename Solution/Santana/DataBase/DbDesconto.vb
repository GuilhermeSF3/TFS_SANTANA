Imports System.Data
Imports System.Data.SqlClient


Public Class DbDesconto

    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub


    Public Sub InserirRegistro(ByVal DT_DESCONTO As DateTime, _
                              ByVal GRUPO_OPERADOR As String, _
                              ByVal VLR_DESCONTO As Double)

        Dim sql As String = "INSERT INTO FX_PJ_DESCONTO(DT_DESCONTO, GRUPO_OPERADOR, VLR_DESCONTO) " & _
                            "VALUES (@DT_DESCONTO, @GRUPO_OPERADOR, @VLR_DESCONTO)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DESCONTO", SqlDbType.SmallDateTime).Value = DT_DESCONTO
        _cmd.Parameters.Add("@GRUPO_OPERADOR", SqlDbType.VarChar, 100).Value = GRUPO_OPERADOR
        _cmd.Parameters.Add("@VLR_DESCONTO", SqlDbType.Float).Value = VLR_DESCONTO


        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal DT_DESCONTO As DateTime, _
                              ByVal GRUPO_OPERADOR As String, _
                              ByVal VLR_DESCONTO As Double)

        Dim sql As String = "UPDATE FX_PJ_DESCONTO SET VLR_DESCONTO=@VLR_DESCONTO " & _
                            "Where GRUPO_OPERADOR=@GRUPO_OPERADOR " & _
                            "AND DT_DESCONTO=@DT_DESCONTO "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)


        _cmd.Parameters.Add("@DT_DESCONTO", SqlDbType.SmallDateTime).Value = DT_DESCONTO
        _cmd.Parameters.Add("@GRUPO_OPERADOR", SqlDbType.VarChar, 100).Value = GRUPO_OPERADOR
        _cmd.Parameters.Add("@VLR_DESCONTO", SqlDbType.Float).Value = VLR_DESCONTO

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub




    Public Sub ApagarRegistro(ByVal DT_DESCONTO As DateTime, _
                              ByVal GRUPO_OPERADOR As String)


        Dim sql As String = "DELETE FROM FX_PJ_DESCONTO Where DT_DESCONTO=@DT_DESCONTO AND GRUPO_OPERADOR=@GRUPO_OPERADOR"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DESCONTO", SqlDbType.SmallDateTime).Value = DT_DESCONTO
        _cmd.Parameters.Add("@GRUPO_OPERADOR", SqlDbType.VarChar, 100).Value = GRUPO_OPERADOR

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub



    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "select * from FX_PJ_DESCONTO (NOLOCK) ORDER BY DT_DESCONTO DESC,GRUPO_OPERADOR"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal DT_DESCONTO As DateTime, _
                              ByVal GRUPO_OPERADOR As String) As DataTable

        Dim sql As String = "select * from FX_PJ_DESCONTO (NOLOCK) WHERE DT_DESCONTO=@DT_DESCONTO AND GRUPO_OPERADOR=@GRUPO_OPERADOR"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_DESCONTO", SqlDbType.SmallDateTime).Value = DT_DESCONTO
        _cmd.Parameters.Add("@GRUPO_OPERADOR", SqlDbType.VarChar, 100).Value = GRUPO_OPERADOR

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

        'Dim sql As String = "select DISTINCT OPER_ABV_NOM from finsgrdbs..TB_OPER (nolock) order by 1"
        'Dim sql As String = "select oper_abv_nom,min(oper_cod) as cod from finsgrdbs..tb_oper (nolock) group by oper_abv_nom"

        Dim sql As String = "select DISTINCT oper_abv_nom from finsgrdbs..tb_oper (nolock) order by 1 "

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



