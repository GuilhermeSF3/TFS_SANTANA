Imports System.Data
Imports System.Data.SqlClient


Public Class DbDeParaOperadoresFuncaoNF

    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub

    Public Sub InserirRegistro(ByVal codAgente As String,
                              ByVal codAgAtivo As String)

        Dim sql As String = "INSERT INTO DEPARA_OPERADOR_FUNCAONF(COD_OPER_FUNCAO, COD_OPER_NF) " &
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

    'Public Sub AtualizarRegistro(ByVal codAgente As String,
    '                          ByVal codAgAtivo As String)

    '    Dim sql As String = "UPDATE DEPARA_AGENTE SET  cod_ativa=@codAtiva" &
    '                        "Where cod_viuva=@codAgAtivo and cod_ativa=@codAtiva"

    '    _conn = New SqlConnection(_connectionString)
    '    _conn.Open()
    '    _cmd = New SqlCommand(sql, _conn)

    '    _cmd.Parameters.Add("@codAgente", SqlDbType.VarChar, 20).Value = codAgente.Substring(0, codAgente.IndexOf("-") - 1).Trim()
    '    _cmd.Parameters.Add("@codAgAtivo", SqlDbType.VarChar, 20).Value = codAgAtivo.Substring(0, codAgAtivo.IndexOf("-") - 1).Trim()

    '    _cmd.Prepare()
    '    _cmd.ExecuteNonQuery()
    '    _conn.Close()

    'End Sub

    Public Sub ApagarRegistro(ByVal codAgente As String,
                              ByVal codAgAtivo As String)

        Dim sql As String = "DELETE FROM DEPARA_OPERADOR_FUNCAONF WHERE COD_OPER_FUNCAO=@codAgente AND COD_OPER_NF=@codAgAtivo"

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
        Dim sql As String = "SELECT DISTINCT COD_OPER_FUNCAO + ' - ' + T6.O6DESCR AS CODOPERFUNCAO, COD_OPER_NF + ' - ' + P.PESNOME AS CODOPERNF FROM DEPARA_OPERADOR_FUNCAONF INNER JOIN CDCSANTANAMICROCREDITO..TORG6 T6 (NOLOCK) ON COD_OPER_FUNCAO = T6.O6CODORG INNER JOIN [SRV-AWS.SHOPCRED.LOCAL].[NETFACTOR].DBO.NFAGENTE NFA (NOLOCK) ON COD_OPER_NF = NFA.AGECODIGO INNER JOIN [SRV-AWS.SHOPCRED.LOCAL].[NETFACTOR].DBO.NFPESSOA P (NOLOCK) ON P.PESCNPJCPF = NFA.PESCNPJCPF"

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

        Dim sql As String = "SELECT * FROM DEPARA_OPERADOR_FUNCAONF (NOLOCK) WHERE COD_OPER_FUNCAO=@codAgente AND COD_OPER_NF=@codAgAtivo"

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

    Public Sub CarregaOperadorFuncao(ByVal ddlAgente As DropDownList)

        Dim sql As String = "SELECT DISTINCT OPCODORG6 AS COD, O6DESCR AS NOME FROM CDCSANTANAMICROCREDITO..COPER O (NOLOCK) INNER JOIN CDCSANTANAMICROCREDITO..TORG6 T6 ON O.OPCODORG6 = T6.O6CODORG WHERE OPCODOP IN (SELECT M.COD_MODA FROM (SELECT * FROM TTIPO_PROD T1 (NOLOCK) WHERE T1.COD_PROD IN('MC','CG') )AS T INNER JOIN TMODA_TIPO_PROD M (NOLOCK) ON T.COD_PROD = M.COD_PROD AND T.COD_MODALIDADE = M.COD_MODALIDADE) AND O6ATIVA = 'A' ORDER BY NOME"

        Dim conn As New SqlConnection(_connectionString)
        conn.Open()
        Dim cmd = New SqlCommand(sql, conn)


        Dim ddlValues As SqlDataReader
        ddlValues = cmd.ExecuteReader()

        ddlAgente.DataSource = ddlValues
        ddlAgente.DataValueField = "COD"
        ddlAgente.DataTextField = "NOME"
        ddlAgente.DataBind()

        ddlAgente.SelectedIndex = 0

        ddlValues.Close()

        conn.Close()
        conn.Dispose()

    End Sub

    Public Sub CarregaOperadorNF(ByVal ddlAgAtivo As DropDownList)

        Dim sql As String = "SELECT DISTINCT AGECODIGO AS COD, P.PESNOME AS NOME FROM [SRV-AWS.SHOPCRED.LOCAL].NETFACTOR.DBO.NFAGENTE A INNER JOIN [SRV-AWS.SHOPCRED.LOCAL].NETFACTOR.DBO.NFPESSOA P (NOLOCK) ON P.PESCNPJCPF = A.PESCNPJCPF WHERE A.AGEDESATIVADO = 0 ORDER BY NOME"

        Dim conn As New SqlConnection(_connectionString)
        conn.Open()
        Dim cmd = New SqlCommand(sql, conn)


        Dim ddlValues As SqlDataReader
        ddlValues = cmd.ExecuteReader()

        ddlAgAtivo.DataSource = ddlValues
        ddlAgAtivo.DataValueField = "COD"
        ddlAgAtivo.DataTextField = "NOME"
        ddlAgAtivo.DataBind()

        ddlAgAtivo.SelectedIndex = 0

        ddlValues.Close()

        conn.Close()
        conn.Dispose()

    End Sub
End Class


