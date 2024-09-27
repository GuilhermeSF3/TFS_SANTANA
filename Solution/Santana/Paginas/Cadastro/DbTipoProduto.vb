Imports System.Data
Imports System.Data.SqlClient


Public Class DbTipoProduto


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub



    Public Sub InserirRegistro(ByVal cod_prod As String,
                              ByVal cod_modalidade As String,
                              ByVal descr_tipo As String,
                              ByVal SIGLA As String, ByVal AGENTE As String, ByVal Canal As String)

        Dim sql As String = "INSERT INTO Ttipo_prod(cod_prod, cod_modalidade, descr_tipo, SIGLA, AGENTE,CANAL) " &
                            "VALUES (@cod_prod, @cod_modalidade, @descr_tipo, @SIGLA, @AGENTE, @CANAL)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod_prod", SqlDbType.VarChar, 4).Value = cod_prod
        _cmd.Parameters.Add("@cod_modalidade", SqlDbType.VarChar, 10).Value = cod_modalidade
        _cmd.Parameters.Add("@descr_tipo", SqlDbType.VarChar, 100).Value = descr_tipo
        _cmd.Parameters.Add("@SIGLA", SqlDbType.VarChar, 5).Value = SIGLA
        _cmd.Parameters.Add("@AGENTE", SqlDbType.VarChar, 6).Value = AGENTE
        _cmd.Parameters.Add("@CANAL", SqlDbType.VarChar, 5).Value = Canal


        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal cod_prod As String,
                              ByVal cod_modalidade As String,
                              ByVal descr_tipo As String,
                              ByVal SIGLA As String, ByVal AGENTE As String, ByVal CANAL As String)

        Dim sql As String = "UPDATE Ttipo_prod SET descr_tipo=@descr_tipo, " &
                            "SIGLA=@SIGLA , AGENTE=@AGENTE, CANAL=@CANAL " &
                            "Where cod_prod=@cod_prod " &
                            "AND cod_modalidade=@cod_modalidade "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod_prod", SqlDbType.VarChar, 4).Value = cod_prod
        _cmd.Parameters.Add("@cod_modalidade", SqlDbType.VarChar, 10).Value = cod_modalidade
        _cmd.Parameters.Add("@descr_tipo", SqlDbType.VarChar, 100).Value = descr_tipo
        _cmd.Parameters.Add("@SIGLA", SqlDbType.VarChar, 5).Value = SIGLA
        _cmd.Parameters.Add("@AGENTE", SqlDbType.VarChar, 6).Value = AGENTE
        _cmd.Parameters.Add("@CANAL", SqlDbType.VarChar, 5).Value = CANAL

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub ApagarRegistro(ByVal cod_prod As String, _
                              ByVal cod_modalidade As String)


        Dim sql As String = "DELETE FROM Ttipo_prod Where cod_prod=@cod_prod AND cod_modalidade=@cod_modalidade"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod_prod", SqlDbType.VarChar, 4).Value = cod_prod
        _cmd.Parameters.Add("@cod_modalidade", SqlDbType.VarChar, 10).Value = cod_modalidade

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT * FROM Ttipo_prod ORDER BY cod_prod, cod_modalidade"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal cod_prod As String, _
                                        ByVal cod_modalidade As String) As DataTable

        Dim sql As String = "SELECT * FROM Ttipo_prod (NOLOCK) WHERE cod_prod=@cod_prod AND cod_modalidade=@cod_modalidade"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod_prod", SqlDbType.VarChar, 4).Value = cod_prod
        _cmd.Parameters.Add("@cod_modalidade", SqlDbType.VarChar, 10).Value = cod_modalidade

        _cmd.Prepare()

        Dim da As New SqlDataAdapter(_cmd)

        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Sub CarregaProdutos(ByVal ddlProduto As DropDownList)

        Dim sql As String = "SELECT cod_prod, descr_prod FROM tproduto (nolock) order by descr_prod"

        Dim conn As New SqlConnection(_connectionString)
        conn.Open()
        Dim cmd = New SqlCommand(sql, conn)


        Dim ddlValues As SqlDataReader
        ddlValues = cmd.ExecuteReader()

        ddlProduto.DataSource = ddlValues
        ddlProduto.DataValueField = "cod_prod"
        ddlProduto.DataTextField = "descr_prod"
        ddlProduto.DataBind()

        ddlProduto.SelectedIndex = 0

        ddlValues.Close()

        conn.Close()
        conn.Dispose()

    End Sub

End Class


