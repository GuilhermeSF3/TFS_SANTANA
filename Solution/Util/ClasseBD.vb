Imports Microsoft.VisualBasic
Imports System.Web.Configuration
Imports System.Data
Imports System.Data.SqlClient

Public Class ClassBD

    Private wSql As String
    Private wTabela As String
    Private wstrConn As String

    Public Shared Function GetExibirGrid(ByVal wSql As String, ByVal wTabela As String, ByVal wstrConn As String) As DataTable
        Dim ds As DataSet = New DataSet
        ds = Consulta(wSql, wTabela, wstrConn)
        'ds.WriteXmlSchema("C:\PROJETOS\MIS2\SafraP1.xsd")
        Return ds.Tables(wTabela)
    End Function


    Public Shared Function Consulta(ByVal [select] As String, ByVal tabela As String, ByVal wstrConn As String) As DataSet
        'Dim strConn As String = "Data Source=200.219.204.140;Initial Catalog=MIS;User=funcao;password=funcao"

        Dim strConn As String = wstrConn
        'Dim strConn As String = "Data Source=172.17.2.10;Initial Catalog=siv;User=usr_siv;password=s35i@v78;Connect Timeout=800000"
        Dim sCon As New SqlConnection(strConn)
        Dim da As New SqlDataAdapter()

        da.SelectCommand = New SqlCommand([select], sCon)
        da.SelectCommand.CommandTimeout = Convert.ToInt32(8000000)
        da.SelectCommand.CommandType = CommandType.Text
        Dim ds As New DataSet()
        da.Fill(ds, tabela)
        sCon.Close()

        Return ds
    End Function

    Public Shared Function ExecuteDataTable(ByRef command As System.Data.SqlClient.SqlCommand) As System.Data.DataTable

        Dim datatable As New System.Data.DataTable
        'Dim result As IAsyncResult = command.BeginExecuteReader()

        'While Not result.IsCompleted
        '    Threading.Thread.Sleep(100)
        'End While

        'datatable.Load(command.EndExecuteReader(result))
        Dim reader As System.Data.SqlClient.SqlDataReader = command.ExecuteReader()

        Try
            datatable.Load(reader)
        Finally
            reader.Close()
            command.Connection = Nothing
        End Try

        Return datatable
    End Function

End Class
