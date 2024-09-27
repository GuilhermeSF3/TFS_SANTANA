Imports System.Data
Imports System.Data.SqlClient


Public Class DbAgOperSvc


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub



    Public Sub InserirRegistro(ByVal codAG As String, _
                              ByVal codSVC As String, _
                              ByVal CODOP As String)

        Dim sql As String = "INSERT INTO USUARIO_SVC(codAGENTE, codAGsvc, CodOPERADOR) " &
                            "VALUES (@codAG, @codSVC, @CODop)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codAG", SqlDbType.VarChar, 20).Value = codAG.Substring(0, 3)
        _cmd.Parameters.Add("@codSVC", SqlDbType.VarChar, 20).Value = codSVC
        _cmd.Parameters.Add("@CODop", SqlDbType.VarChar, 100).Value = CODOP.PadLeft(6, "0").Trim()

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal codAG As String, _
                              ByVal codSVC As String, _
                              ByVal codOP As String)

        Dim sql As String = "UPDATE Usuario_Svc SET  CodOPERADOR=@codOP " &
                            "Where codAGENTE=@codAG and CodAgSVC=@codSVC"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        If codOP.Contains("-") Then
            codOP = codOP.Substring(0, codOP.IndexOf("-"))
        End If

        codOP = codOP.PadLeft(6, "0")

        _cmd.Parameters.Add("@codAG", SqlDbType.VarChar, 20).Value = codAG.Substring(0, codAG.IndexOf("-") - 1).Trim()
        _cmd.Parameters.Add("@codSVC", SqlDbType.VarChar, 20).Value = codSVC.Substring(0, codSVC.IndexOf("-") - 1).Trim()
        _cmd.Parameters.Add("@codOP", SqlDbType.VarChar, 20).Value = codOP.Trim()

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub ApagarRegistro(ByVal codAG As String,
                              ByVal codSVC As String,
                              ByVal codOP As String)


        Dim sql As String = "DELETE FROM Usuario_Svc Where codAGENTE=@codAG AND codAGsvc=@codSVC AND CodOperador=@codOP"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codAG", SqlDbType.VarChar, 20).Value = codAG.Substring(0, codAG.IndexOf("-") - 1).Trim()
        _cmd.Parameters.Add("@codSVC", SqlDbType.VarChar, 20).Value = codSVC.Substring(0, codSVC.IndexOf("-") - 1).Trim()
        _cmd.Parameters.Add("@codOP", SqlDbType.VarChar, 20).Value = codOP.Substring(0, codOP.IndexOf("-") - 1).Trim()

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT DISTINCT CODAGENTE + ' - ' + NOMEUSUARIO AS CODAGENTE, CODAGSVC + ' - ' + NOME AS CODAGSVC, " &
                            "CODOPERADOR + ' - ' + O6DESCR AS CODOPERADOR FROM usuario_svc (NOLOCK) " &
                            "INNER JOIN USUARIO (NOLOCK) ON CODAGENTE = SUBSTRING(CODGERENTE,1,3)  " &
                            "INNER JOIN TBL_SVC (NOLOCK) ON CODAGSVC = CODIGO  " &
                            "LEFT JOIN CDCSANTANAMICROCREDITO..TORG6 (NOLOCK) ON REPLICATE('0',6-LEN(CODOPERADOR)) + RTRIM(CODOPERADOR) = O6CODORG " &
                            "ORDER BY codAGENTE"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal codAG As String,
                                        ByVal codSVC As String,
                                        ByVal codOp As String) As DataTable

        Dim sql As String = "SELECT * FROM usuario_svc (NOLOCK) WHERE codAGENTE=@codAG AND codAGsvc=@codSVC"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codAG", SqlDbType.VarChar, 20).Value = codAG
        _cmd.Parameters.Add("@codSVC", SqlDbType.VarChar, 20).Value = codSVC
        _cmd.Parameters.Add("@codOP", SqlDbType.VarChar, 20).Value = codOp.PadLeft(6, "0").Trim()

        _cmd.Prepare()

        Dim da As New SqlDataAdapter(_cmd)

        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro1(ByVal codAG As String,
                                        ByVal codSVC As String,
                                        ByVal codOp As String) As DataTable

        Dim sql As String = "SELECT * FROM usuario_svc (NOLOCK) WHERE CodOperador=@codOP"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codOP", SqlDbType.VarChar, 20).Value = codOp.PadLeft(6, "0").Trim()

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

        Dim sql As String = "select codgerente as CodAg,nomeUsuario as Ag from usuario (nolock) where perfil=8 order by nomeUsuario"

        Dim conn As New SqlConnection(_connectionString)
        conn.Open()
        Dim cmd = New SqlCommand(sql, conn)


        Dim ddlValues As SqlDataReader
        ddlValues = cmd.ExecuteReader()

        ddlProduto.DataSource = ddlValues
        ddlProduto.DataValueField = "CodAg"
        ddlProduto.DataTextField = "Ag"
        ddlProduto.DataBind()

        ddlProduto.SelectedIndex = 0

        ddlValues.Close()

        conn.Close()
        conn.Dispose()

    End Sub

    Public Sub CarregaServices(ByVal ddlService As DropDownList)

        Dim sql As String = "select codigo,nome from tbl_svc (nolock)  order by nome"

        Dim conn As New SqlConnection(_connectionString)
        conn.Open()
        Dim cmd = New SqlCommand(sql, conn)


        Dim ddlValues As SqlDataReader
        ddlValues = cmd.ExecuteReader()

        ddlService.DataSource = ddlValues
        ddlService.DataValueField = "Codigo"
        ddlService.DataTextField = "nome"
        ddlService.DataBind()

        ddlService.SelectedIndex = 0

        ddlValues.Close()

        conn.Close()
        conn.Dispose()

    End Sub
End Class


