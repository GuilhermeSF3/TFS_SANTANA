Imports System.Data
Imports System.Data.SqlClient

Public Class DbOperadorSubstabelecido


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable

    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub

    Public Sub InserirRegistro(ByVal codOperador As String)

        Dim sql As String = "INSERT INTO TB_OPERADOR_SUBSTABELECIDO(COD_OPERADOR) " &
                            "VALUES (@codOperador)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codOperador", SqlDbType.VarChar, 20).Value = codOperador

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub

    Public Sub AtualizarRegistro(ByVal codOperador As String)

        Dim sql As String = "UPDATE TB_OPERADOR_SUBSTABELECIDO " &
                            "Where COD_OPERADOR=@codOperador"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codOperador", SqlDbType.VarChar, 20).Value = codOperador.Substring(0, codOperador.IndexOf("-") - 1).Trim()

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub

    Public Sub ApagarRegistro(ByVal codOperador As String)

        Dim sql As String = "DELETE FROM TB_OPERADOR_SUBSTABELECIDO Where COD_OPERADOR = @codOperador"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codOperador", SqlDbType.VarChar, 20).Value = codOperador.Substring(0, codOperador.IndexOf("-") - 1).Trim()

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub

    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT COD_OPERADOR + ' - ' + O6DESCR AS CODOPERADOR FROM TB_OPERADOR_SUBSTABELECIDO (NOLOCK) " &
                            "INNER JOIN CDCSANTANAMICROCREDITO..TORG6 (NOLOCK) ON COD_OPERADOR = O6CODORG"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function

    Public Function CarregarRegistro(ByVal codOperador As String) As DataTable

        Dim sql As String = "SELECT * FROM TB_OPERADOR_SUBSTABELECIDO (NOLOCK) WHERE COD_OPERADOR=@codOperador"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codOperador", SqlDbType.VarChar, 20).Value = codOperador

        _cmd.Prepare()

        Dim da As New SqlDataAdapter(_cmd)

        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function

    Public Sub CarregaOperador(ByVal ddlOperador As DropDownList)

        Dim sql As String = "SELECT O6CODORG AS CODOPERADOR, O6DESCR AS NOMEOPERADOR FROM CDCSANTANAMICROCREDITO..TORG6 (NOLOCK) WHERE O6ATIVA = 'A' ORDER BY O6DESCR"

        Dim conn As New SqlConnection(_connectionString)
        conn.Open()
        Dim cmd = New SqlCommand(sql, conn)

        Dim ddlValues As SqlDataReader
        ddlValues = cmd.ExecuteReader()

        ddlOperador.DataSource = ddlValues
        ddlOperador.DataValueField = "CODOPERADOR"
        ddlOperador.DataTextField = "NOMEOPERADOR"
        ddlOperador.DataBind()

        ddlOperador.SelectedIndex = 0

        ddlValues.Close()

        conn.Close()
        conn.Dispose()

    End Sub

End Class


