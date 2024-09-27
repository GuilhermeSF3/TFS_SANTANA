Imports System.Data
Imports System.Data.SqlClient


Public Class DbDeParaAgente


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable

    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub

    Public Sub InserirRegistro(ByVal codAgente As String,
                              ByVal codAgAtivo As String)

        Dim sql As String = "INSERT INTO DEPARA_AGENTE(COD_VIUVA, COD_ATIVA) " &
                            "VALUES (@codAgente, @codAgAtivo)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codAgente", SqlDbType.VarChar, 20).Value = codAgente
        _cmd.Parameters.Add("@codAgAtivo", SqlDbType.VarChar, 20).Value = codAgAtivo

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal codAgente As String,
                              ByVal codAgAtivo As String)

        Dim sql As String = "UPDATE DEPARA_AGENTE SET  cod_ativa=@codAtiva" &
                            "Where cod_viuva=@codAgAtivo and cod_ativa=@codAtiva"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codAgente", SqlDbType.VarChar, 20).Value = codAgente.Substring(0, codAgente.IndexOf("-") - 1).Trim()
        _cmd.Parameters.Add("@codAgAtivo", SqlDbType.VarChar, 20).Value = codAgAtivo.Substring(0, codAgAtivo.IndexOf("-") - 1).Trim()

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub ApagarRegistro(ByVal codAgente As String,
                              ByVal codAgAtivo As String)


        Dim sql As String = "DELETE FROM DEPARA_AGENTE Where cod_viuva=@codAgente AND cod_ativa=@codAgAtivo"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codAgente", SqlDbType.VarChar, 20).Value = codAgente.Substring(0, codAgente.IndexOf("-") - 1).Trim()
        _cmd.Parameters.Add("@codAgAtivo", SqlDbType.VarChar, 20).Value = codAgAtivo.Substring(0, codAgAtivo.IndexOf("-") - 1).Trim()

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros() As DataTable
        Dim sql As String = "SELECT COD_VIUVA + ' - ' + T1.O3DESCR AS CODAGENTE, COD_ATIVA + ' - ' + T2.O3DESCR AS CODAGENTEATIVO FROM DEPARA_AGENTE " &
                            "INNER JOIN CDCSANTANAMICROCREDITO..TORG3 T1 (NOLOCK) ON COD_VIUVA = T1.O3CODORG INNER JOIN CDCSANTANAMICROCREDITO..TORG3 T2 (NOLOCK) ON COD_ATIVA = T2.O3CODORG"


        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal codAgente As String,
                                        ByVal codAgAtivo As String) As DataTable

        Dim sql As String = "SELECT * FROM DEPARA_AGENTE (NOLOCK) WHERE cod_viuva=@codAgente AND cod_ativa=@codAgAtivo"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codAgente", SqlDbType.VarChar, 20).Value = codAgente
        _cmd.Parameters.Add("@codAgAtivo", SqlDbType.VarChar, 20).Value = codAgAtivo

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

    Public Sub CarregaAgenteAtivo(ByVal ddlAgAtivo As DropDownList)

        Dim sql As String = "SELECT O3CODORG AS CODAGENTEATIVO, O3DESCR AS NOMEAGENTEATIVO FROM CDCSANTANAMICROCREDITO..TORG3 (NOLOCK) ORDER BY O3DESCR"

        Dim conn As New SqlConnection(_connectionString)
        conn.Open()
        Dim cmd = New SqlCommand(sql, conn)


        Dim ddlValues As SqlDataReader
        ddlValues = cmd.ExecuteReader()

        ddlAgAtivo.DataSource = ddlValues
        ddlAgAtivo.DataValueField = "CODAGENTEATIVO"
        ddlAgAtivo.DataTextField = "NOMEAGENTEATIVO"
        ddlAgAtivo.DataBind()

        ddlAgAtivo.SelectedIndex = 0

        ddlValues.Close()

        conn.Close()
        conn.Dispose()

    End Sub
End Class


