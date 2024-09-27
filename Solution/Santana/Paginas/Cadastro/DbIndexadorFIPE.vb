Imports System.Data
Imports System.Data.SqlClient


Public Class DbIndexadorFIPE


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub

    Public Sub AjustarIndexadorFIPE(txtTaxa As String)
        Dim sql As String = "EXEC [SCR_ATUALIZA_INDEXADOR_FIPE] " + txtTaxa.Trim

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()
    End Sub

End Class


