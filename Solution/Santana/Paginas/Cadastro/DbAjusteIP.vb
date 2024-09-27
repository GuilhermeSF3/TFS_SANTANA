Imports System.Data
Imports System.Data.SqlClient


Public Class DbAjusteIP


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub

    Public Function CarregarContratos(txtData As String, txtContrato As String, txtParcela As String) As DataTable
        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim table As DataTable

        table = Util.ClassBD.GetExibirGrid("[IP_consulta] '" + Mid(txtData, 7, 4) & "-" & Mid(txtData, 4, 2) & "-" & Mid(txtData, 1, 2) & "', '" & txtContrato & "' , '" & txtParcela & "'", "ContratoCobranca", strConn)

        Return table
    End Function

    Public Sub PagtoManual(txtDataRef As String, txtContrato As String, txtParcela As String, txtCobradora As String, txtDataPagto As String)
        Dim sql As String = " EXEC [IP_Pagto_manual] '" + Mid(txtDataRef, 7, 4) & "-" & Mid(txtDataRef, 4, 2) & "-" & Mid(txtDataRef, 1, 2) & "', '" & txtContrato & "' , '" & txtParcela & "', '" & txtCobradora & "' , '" & Mid(txtDataPagto, 7, 4) & "-" & Mid(txtDataPagto, 4, 2) & "-" & Mid(txtDataPagto, 1, 2) & "'"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()
    End Sub

    Public Sub ReabrirPagto(txtDataRef As String, txtContrato As String, txtParcela As String, txtCobradora As String, txtDataPagto As String)
        Dim sql As String = " EXEC [IP_REABRE_manual] '" + Mid(txtDataRef, 7, 4) & "-" & Mid(txtDataRef, 4, 2) & "-" & Mid(txtDataRef, 1, 2) & "', '" & txtContrato & "' , '" & txtParcela & "', '" & txtCobradora & "' , '" & Mid(txtDataPagto, 7, 4) & "-" & Mid(txtDataPagto, 4, 2) & "-" & Mid(txtDataPagto, 1, 2) & "'"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()
    End Sub

    Public Sub CopiarPagto(txtDataRef As String, txtContrato As String, txtParcela As String, txtCobradora As String, txtDataPara As String, txtCobPara As String)
        Dim sql As String = " EXEC [IP_COPIAR_manual] '" + Mid(txtDataRef, 7, 4) & "-" & Mid(txtDataRef, 4, 2) & "-" & Mid(txtDataRef, 1, 2) & "', '" & txtContrato & "' , '" & txtParcela & "', '" & txtCobradora & "' , '" & Mid(txtDataPara, 7, 4) & "-" & Mid(txtDataPara, 4, 2) & "-" & Mid(txtDataPara, 1, 2) & "' , '" & txtCobPara & "'"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()
    End Sub

    Public Sub CalcularIP(txtDataRef As String)

        'EXEC [SIG_IP_PAGAMENTO_DT] '20191031'
        Dim sql As String = " EXEC [SIG_IP_PAGAMENTO_DT] '" + Mid(txtDataRef, 7, 4) & "-" & Mid(txtDataRef, 4, 2) & "-" & Mid(txtDataRef, 1, 2) & "'"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)
        _cmd.CommandTimeout = Convert.ToInt32(10000000)
        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()
        _conn.Dispose()

        sql = " EXEC PRC_IP_PGTO_PARCIAL_HH '" + Mid(txtDataRef, 7, 4) & "-" & Mid(txtDataRef, 4, 2) & "-01' , '" & Mid(txtDataRef, 7, 4) & "-" & Mid(txtDataRef, 4, 2) & "-" & Mid(txtDataRef, 1, 2) & "' "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)
        _cmd.CommandTimeout = Convert.ToInt32(10000000)
        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()
        _conn.Dispose()

        sql = " EXEC [SIG_IP_CALCULO] '" + Mid(txtDataRef, 7, 4) & "-" & Mid(txtDataRef, 4, 2) & "-" & Mid(txtDataRef, 1, 2) & "'"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)
        _cmd.CommandTimeout = Convert.ToInt32(10000000)
        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()
        _conn.Dispose()

        sql = " EXEC [SIG_IP_RELATORIO] '" + Mid(txtDataRef, 7, 4) & "-" & Mid(txtDataRef, 4, 2) & "-" & Mid(txtDataRef, 1, 2) & "'"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)
        _cmd.CommandTimeout = Convert.ToInt32(10000000)
        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()
        _conn.Dispose()


    End Sub

End Class


