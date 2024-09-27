Imports System.Data
Imports System.Data.SqlClient


Public Class DbDeParaGerencia


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub



    Public Sub InserirRegistro(ByVal codAgente As String,
                              ByVal codGerencia As String)

        Dim sql As String = "INSERT INTO DEPARA_GERENCIA(COD_AGENTE, COD_GERENCIA) " &
                            "VALUES (@codAgente, @codGerencia)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codAgente", SqlDbType.VarChar, 20).Value = codAgente
        _cmd.Parameters.Add("@codGerencia", SqlDbType.VarChar, 20).Value = codGerencia

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal codAgente As String,
                              ByVal codGerencia As String)

        Dim sql As String = "UPDATE DEPARA_GERENCIA SET  COD_GERENCIA=@codGerencia" &
                            "Where COD_AGENTE=@codAgente"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codAgente", SqlDbType.VarChar, 20).Value = codAgente.Substring(0, codAgente.IndexOf("-") - 1).Trim()
        _cmd.Parameters.Add("@codGerencia", SqlDbType.VarChar, 20).Value = codGerencia.Substring(0, codGerencia.IndexOf("-") - 1).Trim()

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub ApagarRegistro(ByVal codAgente As String,
                              ByVal codGerencia As String)


        Dim sql As String = "DELETE FROM DEPARA_GERENCIA Where cod_agente = @codAgente"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codAgente", SqlDbType.VarChar, 20).Value = codAgente.Substring(0, codAgente.IndexOf("-") - 1).Trim()

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros() As DataTable
        Dim sql As String = "SELECT COD_AGENTE + ' - ' + O3DESCR AS CODAGENTE, CASE WHEN COD_GERENCIA = 1 THEN 'SÃO PAULO' " &
                            "WHEN COD_GERENCIA = 2 THEN 'INTERIOR' WHEN COD_GERENCIA = 3 THEN 'DINAMO' " &
                            "WHEN COD_GERENCIA = 4 THEN 'PARCERIAS DIGITAIS' END AS CODGERENCIA " &
                            "FROM DEPARA_GERENCIA (NOLOCK) INNER JOIN CDCSANTANAMICROCREDITO..TORG3 (NOLOCK) ON COD_AGENTE = O3CODORG"


        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal codAgente As String,
                                        ByVal codGerencia As String) As DataTable

        Dim sql As String = "SELECT * FROM DEPARA_GERENCIA (NOLOCK) WHERE cod_AGENTE=@codAgente And COD_GERENCIA = @codGerencia"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codAgente", SqlDbType.VarChar, 20).Value = codAgente
        _cmd.Parameters.Add("@codGerencia", SqlDbType.VarChar, 20).Value = codGerencia

        _cmd.Prepare()

        Dim da As New SqlDataAdapter(_cmd)

        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function

    Public Sub CarregaAgente(ByVal ddlAgente As DropDownList)

        Dim sql As String = "SELECT O3CODORG AS CODAGENTE, O3DESCR AS NOMEAGENTE FROM CDCSANTANAMICROCREDITO..TORG3 (NOLOCK) ORDER BY O3DESCR"

        Dim conn As New SqlConnection(_connectionString)
        conn.Open()
        Dim cmd = New SqlCommand(sql, conn)


        Dim ddlValues As SqlDataReader
        ddlValues = cmd.ExecuteReader()

        ddlAgente.DataSource = ddlValues
        ddlAgente.DataValueField = "CODAGENTE"
        ddlAgente.DataTextField = "NOMEAGENTE"
        ddlAgente.DataBind()

        ddlAgente.SelectedIndex = 0

        ddlValues.Close()

        conn.Close()
        conn.Dispose()

    End Sub

    Public Sub CarregaGerencia(ByVal ddlGerencia As DropDownList)

        ddlGerencia.Items.Insert(0, New ListItem("São Paulo", "1"))
        ddlGerencia.Items.Insert(1, New ListItem("Interior", "2"))
        ddlGerencia.Items.Insert(2, New ListItem("Dinamo", "3"))
        ddlGerencia.Items.Insert(3, New ListItem("Parcerias Digitais", "4"))

    End Sub
End Class


