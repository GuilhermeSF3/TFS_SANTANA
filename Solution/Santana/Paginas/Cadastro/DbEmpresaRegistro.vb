Imports System.Data
Imports System.Data.SqlClient


Public Class DbEmpresaRegistro


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub



    Public Sub InserirRegistro(ByVal UF As String,
                              ByVal EMPRESA As String,
                              ByVal VALOR As Double)

        Dim sql As String = "INSERT INTO TB_PARAM_REGISTRO_CONTRATO(UF, EMPRESA, VALOR) " &
                            "VALUES (@UF, @EMPRESA, @VALOR)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@UF", SqlDbType.VarChar, 2).Value = UF
        _cmd.Parameters.Add("@EMPRESA", SqlDbType.VarChar, 50).Value = EMPRESA
        _cmd.Parameters.Add("@VALOR", SqlDbType.Float).Value = VALOR


        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub




    Public Sub ApagarRegistro(ByVal UF As String,
                              ByVal EMPRESA As String,
                              ByVal VALOR As String)


        Dim sql As String = "DELETE FROM TB_PARAM_REGISTRO_CONTRATO Where UF=@UF AND EMPRESA=@EMPRESA"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@UF", SqlDbType.VarChar, 2).Value = UF
        _cmd.Parameters.Add("@EMPRESA", SqlDbType.VarChar, 50).Value = EMPRESA

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT * FROM TB_PARAM_REGISTRO_CONTRATO (NOLOCK)  ORDER BY EMPRESA, UF"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function
    'Public Function VerificaRegistro(ByVal EMPRESA As String,
    '                                 ByVal UF As String) As DataTable

    '    Dim sql As String = "SELECT * FROM TB_PARAM_REGISTRO_CONTRATO (NOLOCK) WHERE EMPRESA=@EMPRESA AND UF=@UF"

    '    _conn = New SqlConnection(_connectionString)
    '    _conn.Open()
    '    _cmd = New SqlCommand(sql, _conn)

    '    _cmd.Parameters.Add("@EMPRESA", SqlDbType.VarChar, 20).Value = EMPRESA

    '    _cmd.Prepare()

    '    Dim da As New SqlDataAdapter(_cmd)

    '    _dt = New DataTable

    '    Try
    '        da.Fill(_dt)
    '    Catch ex As Exception

    '    End Try

    '    Return _dt

    'End Function


    Public Function CarregarRegistro(ByVal UF As String,
                                    ByVal EMPRESA As String) As DataTable

        Dim sql As String = "SELECT * FROM TB_PARAM_REGISTRO_CONTRATO (NOLOCK)  WHERE UF=@UF AND EMPRESA=@EMPRESA"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@UF", SqlDbType.VarChar, 4).Value = UF
        _cmd.Parameters.Add("@EMPRESA", SqlDbType.VarChar, 10).Value = EMPRESA

        _cmd.Prepare()

        Dim da As New SqlDataAdapter(_cmd)

        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Sub CarregaUF(ByVal ddlProduto As DropDownList)

        Dim sql As String = "SELECT DISTINCT ABUFLCPL AS COD_UF, ABUFLCPL AS NOME_UF FROM CDCSANTANAMICROCREDITO..CBFIN (NOLOCK) WHERE ISNULL(ABUFLCPL,'') <> ''"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        Dim cmd = New SqlCommand(sql, _conn)


        Dim ddlValues As SqlDataReader
        ddlValues = cmd.ExecuteReader()

        ddlProduto.DataSource = ddlValues
        ddlProduto.DataValueField = "COD_UF"
        ddlProduto.DataTextField = "NOME_UF"
        ddlProduto.DataBind()

        ddlProduto.SelectedIndex = 0

        ddlValues.Close()

        _conn.Close()
        _conn.Dispose()

    End Sub


    Public Sub CarregaEmpresa(ByVal COD_EMPR As String, ByVal ddlTipoProduto As DropDownList)

        Dim sql As String = "SELECT 'TECNOBANK' AS COD_EMP, 'TECNOBANK' AS NOME_EMP UNION " &
                            "SELECT 'VETERA' AS COD_EMP, 'VETERA' AS NOME_EMP"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD_EMPR", SqlDbType.VarChar, 4).Value = COD_EMPR

        _cmd.Prepare()

        Dim ddlValues As SqlDataReader
        ddlValues = _cmd.ExecuteReader()

        ddlTipoProduto.DataSource = ddlValues
        ddlTipoProduto.DataValueField = "COD_EMP"
        ddlTipoProduto.DataTextField = "NOME_EMP"
        ddlTipoProduto.DataBind()

        ddlTipoProduto.SelectedIndex = 0

        ddlValues.Close()

        _conn.Close()
        _conn.Dispose()

    End Sub


    Public Sub AtualizarRegistro(ByVal UF As String,
                              ByVal EMPRESA As String,
                              ByVal VALOR As String)

        Dim sql As String = "UPDATE TB_PARAM_REGISTRO_CONTRATO set VALOR=@VALOR " &
                            "Where EMPRESA=@EMPRESA " &
                            "AND UF=@UF "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@UF", SqlDbType.VarChar, 2).Value = UF
        _cmd.Parameters.Add("@EMPRESA", SqlDbType.VarChar, 50).Value = EMPRESA
        _cmd.Parameters.Add("@VALOR", SqlDbType.Float).Value = VALOR

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub

End Class


