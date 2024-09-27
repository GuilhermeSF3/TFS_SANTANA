Imports System.Data
Imports System.Data.SqlClient


Public Class DbAlteracaoGarantia


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub

    Public Function CarregarGarantia(txtProposta As String, txtGarantia As String) As DataTable
        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim table As DataTable

        table = Util.ClassBD.GetExibirGrid("EXEC [SCR_CONSULTA_ALTERA_GARANTIA] '" + txtProposta.Trim & "', '" + txtGarantia.Trim + "'", "ConsultaGarantia", strConn)

        Return table
    End Function

    Public Sub AtualizaGarantia(txtProposta As String, txtGarantia As String, txtModelo As String, txtMarca As String, txtAnoMod As String, txtAnoFab As String, VlrMolicar As String, VlrVenda As String, VlrTabela As String, txtPlaca As String, txtRenavam As String)
        Dim sql As String = "EXEC [SCR_ATUALIZA_ALTERA_GARANTIA] '" + txtProposta.Trim + "', '" + txtGarantia.Trim + "', '" + txtModelo.Trim + "', '" + txtMarca.Trim + "', " + txtAnoMod.Trim + ", " + txtAnoFab.Trim + ", " + VlrMolicar.Trim.Replace(",", ".") + ", " + VlrVenda.Trim.Replace(",", ".") + ", " + VlrTabela.Trim.Replace(",", ".") + ", '" + txtPlaca.Trim.Replace(",", ".") + "', '" + txtRenavam.Trim.Replace(",", ".") + "'"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()
    End Sub

End Class


