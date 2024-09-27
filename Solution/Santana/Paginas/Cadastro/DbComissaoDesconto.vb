Imports System.Data
Imports System.Data.SqlClient


Public Class DbComissaoDesconto


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub



    Public Sub InserirRegistro(ByVal dataref As String, ByVal codop As String, ByVal cpfop As String, ByVal op As String, ByVal comissao As String)

        Dim sql As String = "INSERT INTO TBL_COMISSAO_DESCONTO(DATA_REF, COD_OPERADOR, CPF_OPERADOR, OPERADOR, PRC_COMISSAO) " &
                            "VALUES (@data_ref, @cod_op, @cpf_op, @op, @comissao)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@data_ref", SqlDbType.VarChar, 20).Value = Convert.ToDateTime(dataref).ToString("yyyyMMdd").Trim
        _cmd.Parameters.Add("@cod_op", SqlDbType.VarChar, 20).Value = codop.Trim
        _cmd.Parameters.Add("@cpf_op", SqlDbType.VarChar, 20).Value = cpfop.Trim
        _cmd.Parameters.Add("@op", SqlDbType.VarChar, 60).Value = op.Trim
        _cmd.Parameters.Add("@comissao", SqlDbType.Float, 20).Value = comissao.Trim

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal dataref As String, ByVal codop As String, ByVal op As String, ByVal comissao As String)

        Dim sql As String = "UPDATE TBL_COMISSAO_DESCONTO SET  prc_comissao=@comissao " &
                            "Where data_ref=@data_ref and cod_operador=@cod_op and operador = @op"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@data_ref", SqlDbType.SmallDateTime, 20).Value = dataref.Trim
        _cmd.Parameters.Add("@cod_op", SqlDbType.VarChar, 50).Value = codop.Trim
        _cmd.Parameters.Add("@op", SqlDbType.VarChar, 50).Value = op.Trim
        _cmd.Parameters.Add("@comissao", SqlDbType.Float, 50).Value = comissao.Trim

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub ApagarRegistro(ByVal dataref As DateTime, ByVal codop As String, ByVal op As String, ByVal comissao As String)


        Dim sql As String = "DELETE FROM TBL_COMISSAO_DESCONTO Where data_ref=@data_ref AND cod_operador=@cod_op and operador = @op and prc_comissao = @comissao"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@data_ref", SqlDbType.SmallDateTime, 20).Value = dataref
        _cmd.Parameters.Add("@cod_op", SqlDbType.VarChar, 50).Value = codop.Trim
        _cmd.Parameters.Add("@op", SqlDbType.VarChar, 50).Value = op.Trim
        _cmd.Parameters.Add("@comissao", SqlDbType.Float, 50).Value = comissao.Trim

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT CONVERT(CHAR,DATA_REF,103) AS DATA_REF, COD_OPERADOR, CPF_OPERADOR, CAST(COD_OPERADOR AS VARCHAR(20)) + ' - ' + OPERADOR AS OPERADOR, PRC_COMISSAO FROM TBL_COMISSAO_DESCONTO (NOLOCK)"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal dataref As String, ByVal codOp As String, ByVal Op As String, ByVal cpfOp As String) As DataTable

        Dim sql As String = "SELECT * FROM TBL_COMISSAO_DESCONTO (NOLOCK) WHERE DATA_REF = @DATAREF AND COD_OPERADOR = @CODOP AND CPF_OPERADOR=@CPFOP AND OPERADOR = @OP"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DATAREF", SqlDbType.VarChar, 20).Value = Convert.ToDateTime(dataref).ToString("yyyyMMdd").Trim
        _cmd.Parameters.Add("@CODOP", SqlDbType.VarChar, 20).Value = codOp.Trim
        _cmd.Parameters.Add("@CPFOP", SqlDbType.VarChar, 20).Value = cpfOp.Trim
        _cmd.Parameters.Add("@OP", SqlDbType.VarChar, 20).Value = Op.Trim

        _cmd.Prepare()

        Dim da As New SqlDataAdapter(_cmd)

        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Sub CarregaCodOp(ByVal ddlCodOp As DropDownList)

        Dim sql As String = "SELECT DISTINCT A.AGECODIGO AS CODIGO, CAST(A.AGECODIGO AS VARCHAR(10)) + ' - ' + P.PESNOME + ' - ' + A.PESCNPJCPF AS OPERADOR FROM NETFACTOR..NFAGENTE A (NOLOCK) JOIN NETFACTOR..NFPESSOA P (NOLOCK)  ON P.PESCNPJCPF = A.PESCNPJCPF " &
                            "UNION SELECT '999'    AS CODIGO,'999 - TOTAL GERAL - 999' AS OPERADOR FROM NETFACTOR..NFAGENTE A (NOLOCK) JOIN NETFACTOR..NFPESSOA P (NOLOCK)  ON P.PESCNPJCPF = A.PESCNPJCPF  ORDER BY A.AGECODIGO"

        Dim conn As New SqlConnection(_connectionString)
        conn.Open()
        Dim cmd = New SqlCommand(sql, conn)


        Dim ddlValues As SqlDataReader
        ddlValues = cmd.ExecuteReader()

        ddlCodOp.DataSource = ddlValues

        ddlCodOp.DataValueField = "CODIGO"
        ddlCodOp.DataTextField = "OPERADOR"

        ddlCodOp.DataBind()

        ddlCodOp.SelectedIndex = 0

        ddlValues.Close()

        conn.Close()
        conn.Dispose()

    End Sub

End Class


