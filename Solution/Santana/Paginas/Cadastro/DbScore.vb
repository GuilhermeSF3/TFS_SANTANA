Imports System.Data
Imports System.Data.SqlClient


Public Class DbScore


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub

    Public Function CarregarScore(txtContrato As String, txtProposta As String) As DataTable
        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim table As DataTable

        table = Util.ClassBD.GetExibirGrid("EXEC [SCR_CONSULTA_SCORE] '" + txtContrato.Trim & "', '" + txtProposta.Trim + "'", "ConsultaScore", strConn)

        Return table
    End Function

    Public Sub AtualizaScore(txtContrato As String, txtProposta As String, txtScore As String)
        Dim sql As String = " EXEC [SCR_ATUALIZA_SCORE] '" + txtContrato.Trim & "', '" + txtProposta.Trim + "', " + txtScore

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()
    End Sub

End Class


