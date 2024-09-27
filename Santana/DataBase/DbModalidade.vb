Imports System.Data
Imports System.Data.SqlClient


Public Class DbModalidade


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub



    Public Sub InserirRegistro(ByVal cod_prod As String, _
                              ByVal cod_modalidade As String, _
                              ByVal cod_moda As String)

        Dim sql As String = "INSERT INTO TModa_tipo_prod(cod_prod, cod_modalidade, cod_moda) " & _
                            "VALUES (@cod_prod, @cod_modalidade, @cod_moda)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod_prod", SqlDbType.VarChar, 4).Value = cod_prod
        _cmd.Parameters.Add("@cod_modalidade", SqlDbType.VarChar, 10).Value = cod_modalidade
        _cmd.Parameters.Add("@cod_moda", SqlDbType.VarChar, 20).Value = cod_moda


        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub




    Public Sub ApagarRegistro(ByVal cod_prod As String, _
                              ByVal cod_modalidade As String, _
                              ByVal cod_moda As String)


        Dim sql As String = "DELETE FROM TModa_tipo_prod Where cod_prod=@cod_prod AND cod_modalidade=@cod_modalidade AND cod_moda=@cod_moda"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod_prod", SqlDbType.VarChar, 4).Value = cod_prod
        _cmd.Parameters.Add("@cod_modalidade", SqlDbType.VarChar, 10).Value = cod_modalidade
        _cmd.Parameters.Add("@cod_moda", SqlDbType.VarChar, 20).Value = cod_moda

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT * FROM TModa_tipo_prod (NOLOCK) ORDER BY cod_prod, cod_modalidade, cod_moda"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal cod_prod As String, _
                                    ByVal cod_modalidade As String, _
                                    ByVal cod_moda As String) As DataTable

        Dim sql As String = "SELECT * FROM TModa_tipo_prod (NOLOCK) WHERE cod_prod=@cod_prod AND cod_modalidade=@cod_modalidade AND cod_moda=@cod_moda"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod_prod", SqlDbType.VarChar, 4).Value = cod_prod
        _cmd.Parameters.Add("@cod_modalidade", SqlDbType.VarChar, 10).Value = cod_modalidade
        _cmd.Parameters.Add("@cod_moda", SqlDbType.VarChar, 20).Value = cod_moda

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

        Dim sql As String = "SELECT cod_prod, descr_prod FROM tproduto (NOLOCK) order by descr_prod"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)


        Dim ddlValues As SqlDataReader
        ddlValues = _cmd.ExecuteReader()

        ddlProduto.DataSource = ddlValues
        ddlProduto.DataValueField = "cod_prod"
        ddlProduto.DataTextField = "descr_prod"
        ddlProduto.DataBind()

        ddlProduto.SelectedIndex = 0

        ddlValues.Close()

        _conn.Close()
        _conn.Dispose()

    End Sub


    Public Sub CarregaTipoProduto(ByVal cod_prod As String, ByVal ddlTipoProduto As DropDownList)

        Dim sql As String = "SELECT cod_modalidade, descr_tipo FROM Ttipo_prod (NOLOCK) WHERE cod_prod=@cod_prod order by descr_tipo"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod_prod", SqlDbType.VarChar, 4).Value = cod_prod

        _cmd.Prepare()

        Dim ddlValues As SqlDataReader
        ddlValues = _cmd.ExecuteReader()

        ddlTipoProduto.DataSource = ddlValues
        ddlTipoProduto.DataValueField = "cod_modalidade"
        ddlTipoProduto.DataTextField = "descr_tipo"
        ddlTipoProduto.DataBind()

        ddlTipoProduto.SelectedIndex = 0

        ddlValues.Close()

        _conn.Close()
        _conn.Dispose()

    End Sub


    Public Sub AtualizarRegistro(ByVal cod_prod As String, _
                                    ByVal cod_modalidade As String, _
                                    ByVal cod_moda As String)

        Dim sql As String = "UPDATE TModa_tipo_prod set cod_moda=@cod_moda " & _
                            "Where cod_prod=@cod_prod " & _
                            "AND cod_modalidade=@cod_modalidade "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod_prod", SqlDbType.VarChar, 4).Value = cod_prod
        _cmd.Parameters.Add("@cod_modalidade", SqlDbType.VarChar, 10).Value = cod_modalidade
        _cmd.Parameters.Add("@cod_moda", SqlDbType.VarChar, 20).Value = cod_moda

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub

End Class


