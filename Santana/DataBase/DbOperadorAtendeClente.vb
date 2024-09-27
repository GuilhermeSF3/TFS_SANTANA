Imports System.Data
Imports System.Data.SqlClient

Public Class DbOperadorAtendeClente

    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub


    Public Sub InserirRegistro(ByVal COD As Integer, _
                              ByVal DT_DE As DateTime, _
                              ByVal DT_ATE As DateTime, ByVal COD_OPERADOR As Integer)

        Dim sql As String = "INSERT INTO FX_PJ_CLIENTE_OPERADOR(COD, DT_DE, DT_ATE,COD_OPERADOR) " & _
                            "VALUES (@COD, @DT_DE, @DT_ATE,@COD_OPERADOR)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD", SqlDbType.Int).Value = COD
        _cmd.Parameters.Add("@DT_DE", SqlDbType.DateTime, 100).Value = DT_DE
        _cmd.Parameters.Add("@DT_ATE", SqlDbType.DateTime).Value = DT_ATE
        _cmd.Parameters.Add("@COD_OPERADOR", SqlDbType.Int).Value = COD_OPERADOR


        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal COD As Integer, _
                              ByVal DT_DE As DateTime, _
                              ByVal DT_ATE As DateTime, ByVal COD_OPERADOR As Integer)

        Dim sql As String = "UPDATE FX_PJ_CLIENTE_OPERADOR SET COD_OPERADOR = @COD_OPERADOR " & _
                            "Where COD = @COD and  DT_DE = @DT_DE"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD", SqlDbType.Int).Value = COD
        _cmd.Parameters.Add("@DT_DE", SqlDbType.DateTime).Value = DT_DE
        _cmd.Parameters.Add("@DT_ATE", SqlDbType.DateTime).Value = DT_ATE
        _cmd.Parameters.Add("@COD_OPERADOR", SqlDbType.Int).Value = COD_OPERADOR

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub




    Public Sub ApagarRegistro(ByVal COD As Integer, _
                              ByVal DT_DE As DateTime)


        Dim sql As String = "DELETE FROM FX_PJ_CLIENTE_OPERADOR Where COD=@COD AND DT_DE=@DT_DE"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD", SqlDbType.Int).Value = COD
        _cmd.Parameters.Add("@DT_DE", SqlDbType.DateTime, 100).Value = DT_DE

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub



    Public Function CarregarTodosRegistros() As DataTable

        'Dim sql As String = "select CO.*,O.OPER_ABV_NOM as OpName, C.Name as CName, GRUPO_OPERADOR= 'FALTA TABELA' from FX_PJ_CLIENTE_OPERADOR CO (NOLOCK) " +
        '                    "INNER JOIN TB_OPER O ON CO.COD_OPERADOR = O.cod " +
        '                    "INNER JOIN Customer C ON C.Code = CO.COD"

        Dim sql As String = "SELECT * from FX_PJ_CLIENTE_OPERADOR (NOLOCK) ORDER BY DT_DE,COD"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal COD As Integer, ByVal DT_DE As DateTime, ByVal DT_ATE As DateTime, ByVal COD_OPERADOR As Integer) As DataTable

        Dim sql As String = "select * FROM FX_PJ_CLIENTE_OPERADOR (NOLOCK) Where COD=@COD AND DT_DE=@DT_DE AND DT_ATE=@DT_ATE AND COD_OPERADOR=@COD_OPERADOR"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)


        _cmd.Parameters.Add("@COD", SqlDbType.Int).Value = COD
        _cmd.Parameters.Add("@DT_DE", SqlDbType.DateTime, 100).Value = DT_DE
        _cmd.Parameters.Add("@DT_ATE", SqlDbType.DateTime).Value = DT_ATE
        _cmd.Parameters.Add("@COD_OPERADOR", SqlDbType.Int).Value = COD_OPERADOR

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

        Dim sql As String = "select Code,CONCAT(Code ,' ',Name ) as CODNOME from Customer (nolock) order by Name"

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



    Public Sub CarregarOperador(ByVal ddlOperador As DropDownList)

        Dim sql As String = "select cod,CONCAT(cod,' ',OPER_ABV_NOM ) as CODNOME from TB_OPER (nolock) order by OPER_ABV_NOM"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)


        Dim ddlValues As SqlDataReader
        ddlValues = _cmd.ExecuteReader()

        ddlOperador.DataSource = ddlValues
        ddlOperador.DataValueField = "cod"
        ddlOperador.DataTextField = "CODNOME"
        ddlOperador.DataBind()

        ddlOperador.SelectedIndex = 0

        ddlValues.Close()

        _conn.Close()
        _conn.Dispose()

    End Sub

End Class





