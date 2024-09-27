Imports System.Data
Imports System.Data.SqlClient


Public Class DbDeParaRegiao


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub



    Public Sub InserirRegistro(ByVal codAgente As String,
                              ByVal codRegiao As String)

        Dim sql As String = "INSERT INTO DEPARA_REGIAO(COD_AGENTE, COD_REGIAO) " &
                            "VALUES (@codAgente, @codRegiao)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codAgente", SqlDbType.VarChar, 20).Value = codAgente
        _cmd.Parameters.Add("@codRegiao", SqlDbType.VarChar, 20).Value = codRegiao

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal codAgente As String,
                              ByVal codRegiao As String)

        Dim sql As String = "UPDATE DEPARA_REGIAO SET  codRegiao=@codRegiao" &
                            "Where cod_agente = @codAgente"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codAgente", SqlDbType.VarChar, 20).Value = codAgente.Substring(0, codAgente.IndexOf("-") - 1).Trim()
        _cmd.Parameters.Add("@codRegiao", SqlDbType.VarChar, 20).Value = codRegiao.Substring(0, codRegiao.IndexOf("-") - 1).Trim()

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub ApagarRegistro(ByVal codAgente As String,
                              ByVal codRegiao As String)


        Dim sql As String = "DELETE FROM DEPARA_REGIAO Where cod_agente=@codAgente and cod_Regiao=@codRegiao"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codAgente", SqlDbType.VarChar, 20).Value = codAgente.Substring(0, codAgente.IndexOf("-") - 1).Trim()
        _cmd.Parameters.Add("@codRegiao", SqlDbType.VarChar, 20).Value = codRegiao.Substring(0, codRegiao.IndexOf("-") - 1).Trim()

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros() As DataTable
        Dim sql As String = "SELECT COD_AGENTE + ' - ' + O3DESCR AS COD_AGENTE, CAST(D.COD_REGIAO AS VARCHAR(10)) + ' - ' + NOME_REGIAO AS COD_REGIAO FROM DEPARA_REGIAO D " &
                            "INNER JOIN CDCSANTANAMICROCREDITO..TORG3 (NOLOCK) ON COD_AGENTE = O3CODORG INNER JOIN TAB_REGIAO T ON T.COD_REGIAO = D.COD_REGIAO"


        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal codAgente As String,
                                        ByVal codRegiao As String) As DataTable

        Dim sql As String = "SELECT * FROM DEPARA_REGIAO (NOLOCK) WHERE COD_AGENTE=@codAgente AND COD_REGIAO=@codRegiao"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codAgente", SqlDbType.VarChar, 20).Value = codAgente
        _cmd.Parameters.Add("@codRegiao", SqlDbType.VarChar, 20).Value = codRegiao

        _cmd.Prepare()

        Dim da As New SqlDataAdapter(_cmd)

        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function

    Public Sub CarregaRegiao(ByVal ddlRegiao As DropDownList)

        Dim sql As String = "SELECT COD_REGIAO, NOME_REGIAO FROM TAB_REGIAO (NOLOCK) ORDER BY NOME_REGIAO"

        Dim conn As New SqlConnection(_connectionString)
        conn.Open()
        Dim cmd = New SqlCommand(sql, conn)


        Dim ddlValues As SqlDataReader
        ddlValues = cmd.ExecuteReader()

        ddlRegiao.DataSource = ddlValues
        ddlRegiao.DataValueField = "COD_REGIAO"
        ddlRegiao.DataTextField = "NOME_REGIAO"
        ddlRegiao.DataBind()

        ddlRegiao.SelectedIndex = 0

        ddlValues.Close()

        conn.Close()
        conn.Dispose()

    End Sub

    Public Sub CarregaAgente(ByVal ddlAtivo As DropDownList)

        Dim sql As String = "SELECT O3CODORG AS CODAGENTEATIVO, O3DESCR AS NOMEAGENTEATIVO FROM CDCSANTANAMICROCREDITO..TORG3 (NOLOCK) ORDER BY O3DESCR"

        Dim conn As New SqlConnection(_connectionString)
        conn.Open()
        Dim cmd = New SqlCommand(sql, conn)


        Dim ddlValues As SqlDataReader
        ddlValues = cmd.ExecuteReader()

        ddlAtivo.DataSource = ddlValues
        ddlAtivo.DataValueField = "CODAGENTEATIVO"
        ddlAtivo.DataTextField = "NOMEAGENTEATIVO"
        ddlAtivo.DataBind()

        ddlAtivo.SelectedIndex = 0

        ddlValues.Close()

        conn.Close()
        conn.Dispose()

    End Sub
End Class


