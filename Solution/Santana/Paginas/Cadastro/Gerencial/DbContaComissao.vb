Imports System.Data
Imports System.Data.SqlClient


Public Class DbContaComissao

    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable

    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub

    Public Sub InserirConta(ByVal txtDescRapida As String, ByVal txtCpfCnpjFavorecido As String, ByVal txtContaBancaria As String, ByVal txtFormaPagamento As String,
        ByVal txtCentroCusto As String, ByVal txtCodCliente As String, ByVal txtDebitoCredito As String, ByVal txtCodBanco As String, ByVal txtAgenciaDestino As String,
        ByVal txtDigitoContaDestino As String, ByVal txtContaDestino As String, ByVal txtNomeFavorecido As String, ByVal txtModalidade As String, ByVal txtDigitoAgencia As String,
        ByVal txtNomeBanco As String, ByVal txtTipoConta As String, ByVal txtFinalidade As String, ByVal txtFinalidadeDoc As String, ByVal txtFinalidadeTed As String)

        Dim sql As String = "INSERT INTO TB_CONTA_COMISSAO_CFI(DSRAPIDA,NRCONTABANCARIA,NRFORMAPAGAMENTO,CDCENTROCUSTO,CDCLIENTE,DSDEBITOCREDITO,CDBANCO,NRAGENCIADESTINO,NRDIGITOCONTADESTINO,NRCONTADESTINO,NMFAVORECIDO,NRMODALIDADE,NRDIGITOAGENCIA,DSCPFCNPJFAVORECIDO,NMBANCO,TPCONTA,DSFINALIDADE,DSFINALIDADEDOC,DSFINALIDADETED)" &
                            "VALUES (@DSRAPIDA,@NRCONTABANCARIA,@NRFORMAPAGAMENTO,@CDCENTROCUSTO,@CDCLIENTE,@DSDEBITOCREDITO,@CDBANCO,@NRAGENCIADESTINO,@NRDIGITOCONTADESTINO,@NRCONTADESTINO,@NMFAVORECIDO,@NRMODALIDADE,@NRDIGITOAGENCIA,@DSCPFCNPJFAVORECIDO,@NMBANCO,@TPCONTA,@DSFINALIDADE,@DSFINALIDADEDOC,@DSFINALIDADETED)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DSRAPIDA", SqlDbType.VarChar, 150).Value = txtDescRapida
        _cmd.Parameters.Add("@NRCONTABANCARIA", SqlDbType.VarChar, 150).Value = txtContaBancaria
        _cmd.Parameters.Add("@NRFORMAPAGAMENTO", SqlDbType.VarChar, 150).Value = txtFormaPagamento
        _cmd.Parameters.Add("@CDCENTROCUSTO", SqlDbType.VarChar, 150).Value = txtCentroCusto
        _cmd.Parameters.Add("@CDCLIENTE", SqlDbType.VarChar, 150).Value = txtCodCliente
        _cmd.Parameters.Add("@DSDEBITOCREDITO", SqlDbType.VarChar, 20).Value = txtDebitoCredito
        _cmd.Parameters.Add("@CDBANCO", SqlDbType.VarChar, 150).Value = txtCodBanco
        _cmd.Parameters.Add("@NRAGENCIADESTINO", SqlDbType.VarChar, 150).Value = txtAgenciaDestino
        _cmd.Parameters.Add("@NRDIGITOCONTADESTINO", SqlDbType.VarChar, 150).Value = txtDigitoContaDestino
        _cmd.Parameters.Add("@NRCONTADESTINO", SqlDbType.VarChar, 150).Value = txtContaDestino
        _cmd.Parameters.Add("@NMFAVORECIDO", SqlDbType.VarChar, 150).Value = txtNomeFavorecido
        _cmd.Parameters.Add("@NRMODALIDADE", SqlDbType.VarChar, 150).Value = txtModalidade
        _cmd.Parameters.Add("@NRDIGITOAGENCIA", SqlDbType.VarChar, 150).Value = txtDigitoAgencia
        _cmd.Parameters.Add("@DSCPFCNPJFAVORECIDO", SqlDbType.VarChar, 20).Value = txtCpfCnpjFavorecido
        _cmd.Parameters.Add("@NMBANCO", SqlDbType.VarChar, 20).Value = txtNomeBanco
        _cmd.Parameters.Add("@TPCONTA", SqlDbType.VarChar, 150).Value = txtTipoConta
        _cmd.Parameters.Add("@DSFINALIDADE", SqlDbType.VarChar, 150).Value = txtFinalidade
        _cmd.Parameters.Add("@DSFINALIDADEDOC", SqlDbType.VarChar, 150).Value = txtFinalidadeDoc
        _cmd.Parameters.Add("@DSFINALIDADETED", SqlDbType.VarChar, 150).Value = txtFinalidadeTed

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub

    Public Sub AtualizarConta(ByVal txtDescRapida As String, ByVal txtCpfCnpjFavorecido As String, ByVal txtContaBancaria As String, ByVal txtFormaPagamento As String,
        ByVal txtCentroCusto As String, ByVal txtCodCliente As String, ByVal txtDebitoCredito As String, ByVal txtCodBanco As String, ByVal txtAgenciaDestino As String,
        ByVal txtDigitoContaDestino As String, ByVal txtContaDestino As String, ByVal txtModalidade As String, ByVal txtDigitoAgencia As String,
        ByVal txtNomeBanco As String, ByVal txtTipoConta As String, ByVal txtFinalidade As String, ByVal txtFinalidadeDoc As String, ByVal txtFinalidadeTed As String, ByVal cpfcnpjIdentity As String)

        Dim sql As String = "UPDATE TB_CONTA_COMISSAO_CFI SET DSRAPIDA=@DSRAPIDA," &
                                        "NRCONTABANCARIA=@NRCONTABANCARIA, " &
                                        "NRFORMAPAGAMENTO=@NRFORMAPAGAMENTO, " &
                                        "CDCENTROCUSTO=@CDCENTROCUSTO, " &
                                        "CDCLIENTE=@CDCLIENTE, " &
                                        "DSDEBITOCREDITO=@DSDEBITOCREDITO, " &
                                        "CDBANCO=@CDBANCO, " &
                                        "NRAGENCIADESTINO=@NRAGENCIADESTINO, " &
                                        "NRDIGITOCONTADESTINO=@NRDIGITOCONTADESTINO, " &
                                        "NRCONTADESTINO=@NRCONTADESTINO, " &
                                        "NRMODALIDADE=@NRMODALIDADE, " &
                                        "NRDIGITOAGENCIA=@NRDIGITOAGENCIA, " &
                                        "DSCPFCNPJFAVORECIDO=@DSCPFCNPJFAVORECIDO, " &
                                        "NMBANCO = @NMBANCO, " &
                                        "TPCONTA=@TPCONTA, " &
                                        "DSFINALIDADE=@DSFINALIDADE, " &
                                        "DSFINALIDADEDOC=@DSFINALIDADEDOC, " &
                                        "DSFINALIDADETED=@DSFINALIDADETED " &
                                        "WHERE DSCPFCNPJFAVORECIDO=@CPFCNPJ"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DSRAPIDA", SqlDbType.VarChar, 150).Value = txtDescRapida
        _cmd.Parameters.Add("@NRCONTABANCARIA", SqlDbType.VarChar, 150).Value = txtContaBancaria
        _cmd.Parameters.Add("@NRFORMAPAGAMENTO", SqlDbType.VarChar, 150).Value = txtFormaPagamento
        _cmd.Parameters.Add("@CDCENTROCUSTO", SqlDbType.VarChar, 150).Value = txtCentroCusto
        _cmd.Parameters.Add("@CDCLIENTE", SqlDbType.VarChar, 150).Value = txtCodCliente
        _cmd.Parameters.Add("@DSDEBITOCREDITO", SqlDbType.VarChar, 20).Value = txtDebitoCredito
        _cmd.Parameters.Add("@CDBANCO", SqlDbType.VarChar, 150).Value = txtCodBanco
        _cmd.Parameters.Add("@NRAGENCIADESTINO", SqlDbType.VarChar, 150).Value = txtAgenciaDestino
        _cmd.Parameters.Add("@NRDIGITOCONTADESTINO", SqlDbType.VarChar, 150).Value = txtDigitoContaDestino
        _cmd.Parameters.Add("@NRCONTADESTINO", SqlDbType.VarChar, 150).Value = txtContaDestino
        '_cmd.Parameters.Add("@NMFAVORECIDO", SqlDbType.VarChar, 150).Value = txtNomeFavorecido
        _cmd.Parameters.Add("@NRMODALIDADE", SqlDbType.VarChar, 150).Value = txtModalidade
        _cmd.Parameters.Add("@NRDIGITOAGENCIA", SqlDbType.VarChar, 150).Value = txtDigitoAgencia
        _cmd.Parameters.Add("@DSCPFCNPJFAVORECIDO", SqlDbType.VarChar, 20).Value = txtCpfCnpjFavorecido
        _cmd.Parameters.Add("@NMBANCO", SqlDbType.VarChar, 20).Value = txtNomeBanco
        _cmd.Parameters.Add("@TPCONTA", SqlDbType.VarChar, 150).Value = txtTipoConta
        _cmd.Parameters.Add("@DSFINALIDADE", SqlDbType.VarChar, 150).Value = txtFinalidade
        _cmd.Parameters.Add("@DSFINALIDADEDOC", SqlDbType.VarChar, 150).Value = txtFinalidadeDoc
        _cmd.Parameters.Add("@DSFINALIDADETED", SqlDbType.VarChar, 150).Value = txtFinalidadeTed
        _cmd.Parameters.Add("@CPFCNPJ", SqlDbType.VarChar, 20).Value = cpfcnpjIdentity

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub

    Public Sub ApagarConta(ByVal cpfCnpjFavorecido As String)

        Dim sql As String = "DELETE FROM TB_CONTA_COMISSAO_CFI WHERE DSCPFCNPJFAVORECIDO=@cpfCnpj"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cpfCnpj", SqlDbType.VarChar, 20).Value = cpfCnpjFavorecido

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub

    Public Function CarregarTodasContas() As DataTable

        Dim sql As String = "SELECT * FROM TB_CONTA_COMISSAO_CFI (NOLOCK) ORDER BY NMFAVORECIDO"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function

    Public Function CarregarContaPorCPFCNPJ(cpfcnpj As String) As DataTable

        Dim sql As String = "SELECT * FROM TB_CONTA_COMISSAO_CFI (NOLOCK) WHERE DSCPFCNPJFAVORECIDO=@cpfcnpj"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cpfcnpj", SqlDbType.VarChar, 20).Value = cpfcnpj

        _cmd.Prepare()

        Dim da As New SqlDataAdapter(_cmd)

        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function

    Public Sub CarregarFavorecido(ByVal dllNomeFavorecido As DropDownList)

        Dim sql As String = "SELECT O3CODORG, O3DESCR FROM CDCSANTANAMICROCREDITO..TORG3 (NOLOCK) WHERE O3ATIVA = 'A' " &
                            "UNION " &
                            "SELECT O6CODORG, O6DESCR FROM TB_OPERADOR_SUBSTABELECIDO (NOLOCK) INNER JOIN CDCSANTANAMICROCREDITO..TORG6 (NOLOCK) ON COD_OPERADOR = O6CODORG"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        Dim ddlValues As SqlDataReader
        ddlValues = _cmd.ExecuteReader()

        dllNomeFavorecido.DataSource = ddlValues
        dllNomeFavorecido.DataValueField = "O3DESCR"
        dllNomeFavorecido.DataTextField = "O3DESCR"
        dllNomeFavorecido.DataBind()

        dllNomeFavorecido.Items.Insert(0, New ListItem("Selecione...", ""))
        dllNomeFavorecido.SelectedIndex = 0


        ddlValues.Close()

        _conn.Close()
        _conn.Dispose()

    End Sub

End Class


