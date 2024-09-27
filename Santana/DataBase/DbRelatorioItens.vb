Imports System.Data
Imports System.Data.SqlClient


Public Class DbRelatorioItens


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub



    Public Sub InserirRegistro(ByVal COD_RPT As String, _
                              ByVal ORDEM As String, _
                              ByVal COR_LINHA As String, _
                              ByVal COD_CONTA As String, _
                              ByVal GRUPO_TTL As Int32)

        Dim sql As String = "INSERT INTO GER_RELATORIO_ITEM(COD_RPT, ORDEM, COR_LINHA, COD_CONTA, GRUPO_TTL) " & _
                            "VALUES (@COD_RPT, @ORDEM, @COR_LINHA, @COD_CONTA, @GRUPO_TTL)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD_RPT", SqlDbType.VarChar, 4).Value = COD_RPT
        _cmd.Parameters.Add("@ORDEM", SqlDbType.VarChar, 10).Value = ORDEM
        _cmd.Parameters.Add("@COR_LINHA", SqlDbType.VarChar, 8).Value = COR_LINHA
        _cmd.Parameters.Add("@COD_CONTA", SqlDbType.VarChar, 30).Value = COD_CONTA
        _cmd.Parameters.Add("@GRUPO_TTL", SqlDbType.Int).Value = GRUPO_TTL


        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal COD_RPT As String, _
                              ByVal ORDEM As String, _
                              ByVal COR_LINHA As String, _
                              ByVal COD_CONTA As String, _
                              ByVal GRUPO_TTL As Int32)

        Dim sql As String = "UPDATE GER_RELATORIO_ITEM SET ORDEM=@ORDEM, " & _
                            "COR_LINHA=@COR_LINHA, " & _
                            "GRUPO_TTL=@GRUPO_TTL " & _
                            "WHERE COD_RPT=@COD_RPT AND COD_CONTA=@COD_CONTA"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)


        _cmd.Parameters.Add("@COD_RPT", SqlDbType.VarChar, 4).Value = COD_RPT
        _cmd.Parameters.Add("@ORDEM", SqlDbType.VarChar, 10).Value = ORDEM
        _cmd.Parameters.Add("@COR_LINHA", SqlDbType.VarChar, 8).Value = COR_LINHA
        _cmd.Parameters.Add("@COD_CONTA", SqlDbType.VarChar, 30).Value = COD_CONTA
        _cmd.Parameters.Add("@GRUPO_TTL", SqlDbType.Int).Value = GRUPO_TTL

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub ApagarRegistro(ByVal COD_RPT As String, ByVal COD_CONTA As String)


        Dim sql As String = "DELETE FROM GER_RELATORIO_ITEM WHERE COD_RPT=@COD_RPT AND COD_CONTA=@COD_CONTA"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD_RPT", SqlDbType.VarChar, 4).Value = COD_RPT
        _cmd.Parameters.Add("@COD_CONTA", SqlDbType.VarChar, 30).Value = COD_CONTA

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros(ByVal COD_RPT As String) As DataTable

        Dim sql As String = "SELECT * FROM GER_RELATORIO_ITEM (NOLOCK) WHERE (COD_RPT=@COD_RPT OR @COD_RPT='') ORDER BY COD_RPT"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD_RPT", SqlDbType.VarChar, 4).Value = COD_RPT
        _cmd.Prepare()

        Dim da As New SqlDataAdapter(_cmd)

        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal COD_RPT As String, ByVal COD_CONTA As String) As DataTable

        Dim sql As String = "SELECT * FROM GER_CONTA_CALC (NOLOCK) WHERE COD_RPT=@COD_RPT AND COD_CONTA=@COD_CONTA"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD_RPT", SqlDbType.VarChar, 4).Value = COD_RPT
        _cmd.Parameters.Add("@COD_CONTA", SqlDbType.VarChar, 30).Value = COD_CONTA

        _cmd.Prepare()

        Dim da As New SqlDataAdapter(_cmd)

        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function



    Public Sub CarregarRelatorio(ByVal ddlRelatorio As DropDownList)


        Dim sql As String = "SELECT COD_RPT, (COD_RPT + ' - ' + DESCR) as descricao from GER_RELATORIO (nolock) "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Prepare()

        Dim ddlValues As SqlDataReader
        ddlValues = _cmd.ExecuteReader()

        ddlRelatorio.DataSource = ddlValues
        ddlRelatorio.DataValueField = "COD_RPT"
        ddlRelatorio.DataTextField = "descricao"
        ddlRelatorio.DataBind()

        ddlRelatorio.SelectedIndex = 0

        ddlValues.Close()

        _conn.Close()
        _conn.Dispose()

    End Sub


    Public Sub CarregarContaCalculo(ByVal ddlContaCalculo As DropDownList)


        Dim sql As String = "SELECT COD_CONTA, (COD_CONTA + ' - ' + DESCR) as descricao from GER_CONTA_CALC (nolock) order by COD_CONTA "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Prepare()

        Dim ddlValues As SqlDataReader
        ddlValues = _cmd.ExecuteReader()

        ddlContaCalculo.DataSource = ddlValues
        ddlContaCalculo.DataValueField = "COD_CONTA"
        ddlContaCalculo.DataTextField = "descricao"
        ddlContaCalculo.DataBind()

        ddlContaCalculo.SelectedIndex = 0

        ddlValues.Close()

        _conn.Close()
        _conn.Dispose()

    End Sub




End Class


