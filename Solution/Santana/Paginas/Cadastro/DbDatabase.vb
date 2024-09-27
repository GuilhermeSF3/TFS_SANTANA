Imports System.Data
Imports System.Data.SqlClient


Public Class DbDatabase


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub

    Public Function CarregarDataBase(txtProposta As String) As DataTable
        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim table As DataTable

        table = Util.ClassBD.GetExibirGrid("EXEC [SCR_CONSULTA_DATABASE] '" + txtProposta.Trim + "'", "ConsultaDataBase", strConn)

        Return table
    End Function

    Public Sub AtualizaDatabase(txtProposta As String, txtDatabase As String)
        Dim sql As String = " EXEC [SCR_ATUALIZA_DATABASE] '" + txtProposta.Trim + "', '" + txtDatabase + "'"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()
    End Sub

End Class


