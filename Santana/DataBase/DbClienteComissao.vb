

Imports System.Data
Imports System.Data.SqlClient

Public Class DbClienteComissao

    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub


    Public Sub InserirRegistro(ByVal cod As Integer, _
                              ByVal DT_DE As DateTime, _
                              ByVal NOVA_MODALIDADE As Integer, _
                              ByVal TX_DE As Double, _
                              ByVal TX_ATE As Double, _
                              ByVal PRC_COMISSAO As Double)
        Dim sql As String = ""

        If NOVA_MODALIDADE < 0 Then
            sql = "INSERT INTO FX_PJ_CLIENTE_COMISSAO(cod, DT_DE, TX_DE,TX_ATE,PRC_COMISSAO) " & _
                            "VALUES (@cod, @DT_DE, @TX_DE,@TX_ATE,@PRC_COMISSAO)"

        Else
            sql = "INSERT INTO FX_PJ_CLIENTE_COMISSAO(cod, DT_DE, NOVA_MODALIDADE,TX_DE,TX_ATE,PRC_COMISSAO) " & _
                          "VALUES (@cod, @DT_DE, @NOVA_MODALIDADE,@TX_DE,@TX_ATE,@PRC_COMISSAO)"
        End If


        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod", SqlDbType.Int).Value = cod
        _cmd.Parameters.Add("@DT_DE", SqlDbType.DateTime, 100).Value = DT_DE

        _cmd.Parameters.Add("@NOVA_MODALIDADE", SqlDbType.Int).Value = NOVA_MODALIDADE
        _cmd.Parameters.Add("@TX_DE", SqlDbType.Float).Value = TX_DE
        _cmd.Parameters.Add("@TX_ATE", SqlDbType.Float).Value = TX_ATE
        _cmd.Parameters.Add("@PRC_COMISSAO", SqlDbType.Float).Value = PRC_COMISSAO



        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal cod As Integer, _
                              ByVal DT_DE As DateTime, _
                              ByVal NOVA_MODALIDADE As Integer, _
                              ByVal TX_DE As Double, _
                              ByVal TX_ATE As Double, _
                              ByVal PRC_COMISSAO As Double)

        Dim sql As String = ""

        If NOVA_MODALIDADE < 0 Then
            sql = "UPDATE FX_PJ_CLIENTE_COMISSAO SET NOVA_MODALIDADE = null, TX_DE=@TX_DE, TX_ATE=@TX_ATE, PRC_COMISSAO=@PRC_COMISSAO " & _
                            "Where cod = @cod and  DT_DE = @DT_DE "

        Else
            sql = "UPDATE FX_PJ_CLIENTE_COMISSAO SET NOVA_MODALIDADE = @NOVA_MODALIDADE, TX_DE=@TX_DE, TX_ATE=@TX_ATE, PRC_COMISSAO=@PRC_COMISSAO " & _
                            "Where cod = @cod and  DT_DE = @DT_DE "
        End If



        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod", SqlDbType.Int).Value = cod
        _cmd.Parameters.Add("@DT_DE", SqlDbType.DateTime, 100).Value = DT_DE
        _cmd.Parameters.Add("@NOVA_MODALIDADE", SqlDbType.Int).Value = NOVA_MODALIDADE
        _cmd.Parameters.Add("@TX_DE", SqlDbType.Float).Value = TX_DE
        _cmd.Parameters.Add("@TX_ATE", SqlDbType.Float).Value = TX_ATE
        _cmd.Parameters.Add("@PRC_COMISSAO", SqlDbType.Float).Value = PRC_COMISSAO

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub




    Public Sub ApagarRegistro(ByVal cod As Integer, _
                              ByVal DT_DE As DateTime)

        Dim sql As String = "DELETE FROM FX_PJ_CLIENTE_COMISSAO Where cod=@cod AND DT_DE=@DT_DE "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod", SqlDbType.Int).Value = cod
        _cmd.Parameters.Add("@DT_DE", SqlDbType.DateTime, 100).Value = DT_DE

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub



    Public Function CarregarTodosRegistros() As DataTable

        'Dim sql As String = "select CO.*,O.OPER_ABV_NOM as OpName, C.Name as CName, GRUPO_OPERADOR= 'FALTA TABELA' from FX_PJ_CLIENTE_COMISSAO CO (NOLOCK) " +
        '                    "INNER JOIN TB_OPER O ON CO.COD_OPERADOR = O.cod " +
        '                    "INNER JOIN Customer C ON C.Code = CO.COD"

        Dim sql As String = "SELECT * FROM FX_PJ_CLIENTE_COMISSAO (NOLOCK)"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal cod As Integer, ByVal DT_DE As DateTime) As DataTable

        Dim sql As String = "select * FROM FX_PJ_CLIENTE_COMISSAO (NOLOCK) Where cod=@cod AND DT_DE=@DT_DE"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod", SqlDbType.Int).Value = cod
        _cmd.Parameters.Add("@DT_DE", SqlDbType.DateTime, 100).Value = DT_DE

        _cmd.Prepare()

        Dim da As New SqlDataAdapter(_cmd)

        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function



    Public Sub CarregarUsuario(ByVal ddlUsuario As DropDownList)

        Dim sql As String = "select Code, CONCAT(Code,' ',Name) as CODNOME from Customer (nolock) order by Name"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)


        Dim ddlValues As SqlDataReader
        ddlValues = _cmd.ExecuteReader()

        ddlUsuario.DataSource = ddlValues
        ddlUsuario.DataValueField = "Code"
        ddlUsuario.DataTextField = "CODNOME"
        ddlUsuario.DataBind()

        ddlUsuario.SelectedIndex = 0

        ddlValues.Close()

        _conn.Close()
        _conn.Dispose()

    End Sub

End Class





