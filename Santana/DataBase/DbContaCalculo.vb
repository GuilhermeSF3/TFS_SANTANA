Imports System.Data
Imports System.Data.SqlClient


Public Class DbContaCalculo


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub



    Public Sub InserirRegistro(ByVal COD_CONTA As String, _
                              ByVal CONTA_CALCULO As String, _
                              ByVal CONTA_CAMPO As String, _
                              ByVal SINAL As String)

        Dim sql As String = "INSERT INTO GER_CONTA_CALCULO(COD_CONTA, SINAL, CONTA_CALCULO, CONTA_CAMPO) " & _
                            "VALUES (@COD_CONTA, @SINAL, @CONTA_CALCULO, @CONTA_CAMPO)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD_CONTA", SqlDbType.VarChar, 30).Value = COD_CONTA
        _cmd.Parameters.Add("@SINAL", SqlDbType.VarChar, 1).Value = SINAL
        _cmd.Parameters.Add("@CONTA_CALCULO", SqlDbType.VarChar, 30).Value = CONTA_CALCULO
        _cmd.Parameters.Add("@CONTA_CAMPO", SqlDbType.VarChar, 20).Value = CONTA_CAMPO


        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal COD_CONTA As String, _
                                  ByVal CONTA_CALCULO As String, _
                                  ByVal CONTA_CAMPO As String, _
                                  ByVal SINAL As String)

        Dim sql As String = "UPDATE GER_CONTA_CALCULO SET SINAL=@SINAL, CONTA_CAMPO=@CONTA_CAMPO " & _
                            "WHERE COD_CONTA=@COD_CONTA AND " & _
                            "CONTA_CALCULO=@CONTA_CALCULO "
        '"CONTA_CAMPO=@CONTA_CAMPO AND " & _


        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD_CONTA", SqlDbType.VarChar, 30).Value = COD_CONTA
        _cmd.Parameters.Add("@SINAL", SqlDbType.VarChar, 1).Value = SINAL
        _cmd.Parameters.Add("@CONTA_CALCULO", SqlDbType.VarChar, 30).Value = CONTA_CALCULO
        _cmd.Parameters.Add("@CONTA_CAMPO", SqlDbType.VarChar, 20).Value = CONTA_CAMPO

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub ApagarRegistro(ByVal COD_CONTA As String, _
                                  ByVal CONTA_CALCULO As String, _
                                  ByVal CONTA_CAMPO As String)


        Dim sql As String = "DELETE FROM GER_CONTA_CALCULO Where COD_CONTA=@COD_CONTA AND " & _
                            "CONTA_CAMPO=@CONTA_CAMPO AND " & _
                            "CONTA_CALCULO=@CONTA_CALCULO "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD_CONTA", SqlDbType.VarChar, 30).Value = COD_CONTA
        _cmd.Parameters.Add("@CONTA_CALCULO", SqlDbType.VarChar, 30).Value = CONTA_CALCULO
        _cmd.Parameters.Add("@CONTA_CAMPO", SqlDbType.VarChar, 20).Value = CONTA_CAMPO

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros(ByVal COD_CONTA As String) As DataTable

        'Dim sql As String = "SELECT * FROM GER_CONTA_CALCULO (NOLOCK) Where (COD_CONTA=@COD_CONTA or @COD_CONTA='') ORDER BY COD_CONTA"

        Dim sql As String = "SELECT G.*,C.nume_cont_fmtd  as conta_format,C.nome_cont FROM GER_CONTA_CALCULO G (NOLOCK) " & _
"JOIN (select nume_cont_fmtd,nome_cont,nume_cont from CONTABIL_SANTANA..PLANO_CONTABIL (nolock) ) AS C " & _
"ON nume_cont = CONTA_CALCULO Where (COD_CONTA=@COD_CONTA or @COD_CONTA='') ORDER BY COD_CONTA "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

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


    Public Function CarregarRegistro(ByVal COD_CONTA As String, _
                                  ByVal CONTA_CALCULO As String, _
                                  ByVal CONTA_CAMPO As String) As DataTable

        Dim sql As String = "SELECT * FROM GER_CONTA_CALCULO (NOLOCK) WHERE COD_CONTA=@COD_CONTA AND " & _
                            "CONTA_CAMPO=@CONTA_CAMPO AND " & _
                            "CONTA_CALCULO=@CONTA_CALCULO "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COD_CONTA", SqlDbType.VarChar, 30).Value = COD_CONTA
        _cmd.Parameters.Add("@CONTA_CALCULO", SqlDbType.VarChar, 30).Value = CONTA_CALCULO
        _cmd.Parameters.Add("@CONTA_CAMPO", SqlDbType.VarChar, 20).Value = CONTA_CAMPO

        _cmd.Prepare()

        Dim da As New SqlDataAdapter(_cmd)

        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function



    Public Sub CarregarContas(ByVal ddlContas As DropDownList)

        Dim sql As String = "SELECT COD_CONTA, (COD_CONTA+' - '+ DESCR) AS DESCRICAO FROM GER_CONTA_CALC (NOLOCK) ORDER BY COD_CONTA"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)


        Dim ddlValues As SqlDataReader
        ddlValues = _cmd.ExecuteReader()

        ddlContas.DataSource = ddlValues
        ddlContas.DataValueField = "COD_CONTA"
        ddlContas.DataTextField = "DESCRICAO"
        ddlContas.DataBind()

        ddlContas.SelectedIndex = 0

        ddlValues.Close()

        _conn.Close()
        _conn.Dispose()

    End Sub



    Public Sub CarregarContaCalculo(ByVal CONTA_CAMPO As String, ByVal ddlContaCalculo As DropDownList)

        'Dim sql As String = "SELECT CONTA_CONTABIL as nume_cont, CONTA_CONTABIL as descricao FROM PLANO_CONTAS (NOLOCK) " & _
        '"WHERE CONTA_CONTABIL NOT IN (SELECT CONTA_CALCULO FROM GER_CONTA_CALCULO (NOLOCK) WHERE CONTA_CAMPO=@CONTA_CAMPO ) ORDER BY CONTA_CONTABIL"

        '        Dim sql As String = "SELECT nume_cont, CONCAT(nume_cont_fmtd, ' - ', nome_cont) as descricao from CONTABIL_SANTANA..PLANO_CONTABIL (nolock) " & _
        '       "WHERE GRAU_CONT NOT IN (7) AND nume_cont NOT IN (SELECT CONTA_CALCULO FROM GER_CONTA_CALCULO (NOLOCK) WHERE CONTA_CAMPO=@CONTA_CAMPO ) ORDER BY CONTA_CONTABIL"


        Dim sql As String = "SELECT nume_cont,(nume_cont_fmtd+ ' - '+ nome_cont) as descricao from CONTABIL_SANTANA..PLANO_CONTABIL (nolock) " & _
        "WHERE GRAU_CONT NOT IN (7) AND nume_cont NOT IN (SELECT CONTA_CALCULO FROM GER_CONTA_CALCULO (NOLOCK) WHERE cod_CONTA=@CONTA_CAMPO ) ORDER BY nume_cont"


        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@CONTA_CAMPO", SqlDbType.VarChar, 20).Value = CONTA_CAMPO

        _cmd.Prepare()

        Dim ddlValues As SqlDataReader
        ddlValues = _cmd.ExecuteReader()

        ddlContaCalculo.DataSource = ddlValues
        ddlContaCalculo.DataValueField = "nume_cont"
        ddlContaCalculo.DataTextField = "descricao"
        ddlContaCalculo.DataBind()

        ddlContaCalculo.SelectedIndex = 0

        ddlValues.Close()

        _conn.Close()
        _conn.Dispose()

    End Sub

End Class


