Imports System.Data
Imports System.Data.SqlClient


Public Class DbCadastroGrupoPJ


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub



    Public Sub InserirRegistro(ByVal codCliente As String,
                              ByVal codGrupo As String)

        Dim sql As String = "INSERT INTO TB_GRUPO_PJ(COD_CLIENTE, COD_GRUPO) " &
                            "VALUES (@codCliente, @codGrupo)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codCliente", SqlDbType.VarChar, 20).Value = codCliente
        _cmd.Parameters.Add("@codGrupo", SqlDbType.VarChar, 20).Value = codGrupo

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal codCliente As String,
                              ByVal codGrupo As String)

        Dim sql As String = "UPDATE TB_GRUPO_PJ SET COD_GRUPO=@codGrupo " &
                            "Where COD_CLIENTE = @codCliente"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codCliente", SqlDbType.VarChar, 20).Value = codCliente
        _cmd.Parameters.Add("@codGrupo", SqlDbType.VarChar, 20).Value = codGrupo

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub

    Public Sub ApagarRegistro(ByVal codCliente As String)

        Dim sql As String = "DELETE FROM TB_GRUPO_PJ WHERE COD_CLIENTE=@codCliente"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codCliente", SqlDbType.VarChar, 20).Value = codCliente

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT COD_CLIENTE, CLNOMECLI AS NOME_CLIENTE, COD_GRUPO FROM TB_GRUPO_PJ " &
                            "INNER JOIN CDCSANTANAMICROCREDITO..CCLIE (NOLOCK) ON CLCODCLI = COD_CLIENTE " &
                            "ORDER BY CLNOMECLI"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function

    Public Function CarregarRegistro(ByVal codCliente As String) As DataTable

        Dim sql As String = "SELECT * FROM TB_GRUPO_PJ (NOLOCK) WHERE COD_CLIENTE=@codCliente"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codCliente", SqlDbType.VarChar, 20).Value = codCliente

        _cmd.Prepare()

        Dim da As New SqlDataAdapter(_cmd)

        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function

    Public Sub CarregarClientes(ByVal ddlCliente As DropDownList)

        Dim sql As String = "SELECT DISTINCT CLCODCLI, CLNOMECLI FROM CDCSANTANAMICROCREDITO..CCLIE (NOLOCK)" &
                            "INNER JOIN CDCSANTANAMICROCREDITO..COPER (NOLOCK) ON OPCODCLI = CLCODCLI " &
                            "WHERE CLTPFJ = 'J' " &
                            "AND OPDTLIQ IS NULL " &
                            "ORDER BY 2"

        Dim conn As New SqlConnection(_connectionString)
        conn.Open()
        Dim cmd = New SqlCommand(sql, conn)

        Dim ddlValues As SqlDataReader
        ddlValues = cmd.ExecuteReader()

        ddlCliente.DataSource = ddlValues
        ddlCliente.DataValueField = "CLCODCLI"
        ddlCliente.DataTextField = "CLNOMECLI"
        ddlCliente.DataBind()

        ddlCliente.SelectedIndex = 0

        ddlValues.Close()

        conn.Close()
        conn.Dispose()

    End Sub

End Class


