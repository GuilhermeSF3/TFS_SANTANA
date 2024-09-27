Imports System.Data
Imports System.Data.SqlClient


Public Class DbAgente


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub


    Public Function CarregarTodosRegistros(ByVal ddl As DropDownList, Optional ByVal perfil As integer = 0, Optional ByVal codGerente As String = "")

        Dim sql As String

        If perfil = 8 Then
            sql = "SELECT O3codOrg as AGCod, O3descr as AGDescr FROM	CDCSANTANAMicroCredito..TORG3 A (NOLOCK) where O3ATIVA IN ('S','A') AND AGCod in (" + codGerente + ")"
        Else
            sql = "SELECT O3codOrg as AGCod, O3descr as AGDescr FROM	CDCSANTANAMicroCredito..TORG3 A (NOLOCK) WHERE O3ATIVA IN ('S','A') ORDER BY O3DESCR"
        End If

        'Dim sql As String = "select AGCod, AGDescr from TAgente (nolock) where AGAtivo=1 order by AGDescr"
       ' Dim sql As String = "SELECT a13codOrg as AGCod, a13descr as AGDescr FROM	CDCSANTANAMicroCredito..TA1O3 A (NOLOCK)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Prepare()

        Dim ddlValues As SqlDataReader
        ddlValues = _cmd.ExecuteReader()

        ddl.DataSource = ddlValues
        ddl.DataValueField = "AGCod"
        ddl.DataTextField = "AGDescr"
        ddl.DataBind()

        'ddl.SelectedIndex = 0

        ddlValues.Close()

        _conn.Close()
        _conn.Dispose()

    End Function


End Class


