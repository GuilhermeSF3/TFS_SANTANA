Imports System.Data
Imports System.Data.SqlClient

Namespace DataBase.PT


    Public Class DbCidade

        ReadOnly _connectionString As String
        Dim _conn As SqlConnection
        Dim _cmd As SqlCommand
        Dim _dt As DataTable


        Public Sub New()
            _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
        End Sub


        Public Sub InserirRegistro(ByVal COD As String,
                                   ByVal CIDADE As String,
                                   ByVal UF As String,
                                   ByVal REGIAO As String,
                                   ByVal GRUPO As String,
                                   ByVal CEP_DE As String,
                                   ByVal CEP_ATE As String,
                                   ByVal AGENTE As String)

            Dim sql As String = "INSERT INTO TCIDADE(codigo,cidade,cod_uf,cod_regiao,cod_grupo,cep_de,cep_ate,cod_agente) " &
                                "VALUES (@COD, @CIDADE, @UF, @REGIAO, @GRUPO, @CEP_DE, @CEP_ATE, @AGENTE)"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 3).Value = COD
            _cmd.Parameters.Add("@CIDADE", SqlDbType.VarChar, 100).Value = CIDADE

            _cmd.Parameters.Add("@UF", SqlDbType.VarChar, 100).Value = UF
            _cmd.Parameters.Add("@REGIAO", SqlDbType.VarChar, 100).Value = REGIAO
            _cmd.Parameters.Add("@GRUPO", SqlDbType.VarChar, 100).Value = GRUPO
            _cmd.Parameters.Add("@CEP_DE", SqlDbType.VarChar, 100).Value = CEP_DE
            _cmd.Parameters.Add("@CEP_ATE", SqlDbType.VarChar, 100).Value = CEP_ATE
            _cmd.Parameters.Add("@AGENTE", SqlDbType.VarChar, 100).Value = AGENTE

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Sub AtualizarRegistro(ByVal COD As String,
                                   ByVal CIDADE As String,
                                   ByVal UF As String,
                                   ByVal REGIAO As String,
                                   ByVal GRUPO As String,
                                   ByVal CEP_DE As String,
                                   ByVal CEP_ATE As String,
                                   ByVal AGENTE As String)
            ',cod_uf = @UF,cod_regiao = @REGIAO,cod_grupo = @GRUPO,
            'cod_agente = @AGENTE
            Dim sql As String = "UPDATE TCIDADE SET codigo = @COD,cidade = @CIDADE, COD_REGIAO= @REGIAO, cod_grupo = @GRUPO, cep_de = @CEP_DE,cep_ate = @CEP_ATE , cod_agente = @AGENTE " &
                                "Where codigo=@COD "

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 3).Value = COD
            _cmd.Parameters.Add("@CIDADE", SqlDbType.VarChar, 100).Value = CIDADE

            _cmd.Parameters.Add("@UF", SqlDbType.VarChar, 100).Value = UF
            _cmd.Parameters.Add("@REGIAO", SqlDbType.VarChar, 100).Value = REGIAO
            _cmd.Parameters.Add("@GRUPO", SqlDbType.VarChar, 100).Value = GRUPO
            _cmd.Parameters.Add("@CEP_DE", SqlDbType.VarChar, 100).Value = CEP_DE
            _cmd.Parameters.Add("@CEP_ATE", SqlDbType.VarChar, 100).Value = CEP_ATE
            _cmd.Parameters.Add("@AGENTE", SqlDbType.VarChar, 100).Value = AGENTE

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Sub ApagarRegistro(ByVal COD As String)


            Dim sql As String = "DELETE FROM TCIDADE Where codigo=@COD"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 3).Value = COD

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Function CarregarTodosRegistros() As DataTable
            'TODO
            'Dim sql As String = "SELECT * FROM TCIDADE (NOLOCK) ORDER BY codigo"
            '" (SELECT O3DESCR from CDCSANTANAMicroCredito..TORG3 OPE1 (NOLOCK) WHERE CONVERT(INT,O3CODORG)=CONVERT(INT,cod_agente) )+'-'+CONVERT(VARCHAR(3),COD_AGENTE) AS cod_agente " 

            Dim sql As String = "SELECT codigo,cidade,cod_uf, COD_REGIAO as COD_REGIAO1, (SELECT regiao FROM	TREGIAO C (NOLOCK) WHERE CODIGO=COD_REGIAO)+'-'+CONVERT(VARCHAR(3),cod_regiao)  AS cod_regiao, " &
 " (SELECT GRUPO FROM	TGRUPO C (NOLOCK) WHERE CODIGO=COD_GRUPO)+'-'+CONVERT(VARCHAR(3),COD_GRUPO)  AS COD_GRUPO, COD_GRUPO AS COD_GRUPO1,  cod_agente AS cod_agente1 , " &
 " cep_de,cep_ate,(select O3DESCR from CDCSANTANAMicroCredito..TORG3 A1 (NOLOCK) WHERE CONVERT(INT,O3CODORG)=CONVERT(INT,cod_agente) )+'-'+CONVERT(VARCHAR(3),COD_AGENTE) AS cod_agente  " &
 " FROM TCIDADE (NOLOCK) ORDER BY cidade "

            Dim da As New SqlDataAdapter(Sql, _connectionString)
            _dt = New DataTable

            Try
                da.Fill(_dt)
            Catch ex As Exception

            End Try

            Return _dt

        End Function

        Public Function CarregarTodosRegistrosDDL(ByVal ddl As DropDownList) As DataTable
            'TODO
            'Dim sql As String = "SELECT * FROM TCIDADE (NOLOCK) ORDER BY codigo"
            '" (SELECT O3DESCR from CDCSANTANAMicroCredito..TORG3 OPE1 (NOLOCK) WHERE CONVERT(INT,O3CODORG)=CONVERT(INT,cod_agente) )+'-'+CONVERT(VARCHAR(3),COD_AGENTE) AS cod_agente " 

            Dim sql As String = "SELECT codigo,cidade,cod_uf, COD_GRUPO,  cep_de,cep_ate, cod_agente " & _
 " FROM TCIDADE (NOLOCK) ORDER BY cidade "


            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Prepare()

            Dim ddlValues As SqlDataReader
            ddlValues = _cmd.ExecuteReader()

            ddl.DataSource = ddlValues
            ddl.DataValueField = "codigo"
            ddl.DataTextField = "cidade"
            ddl.DataBind()

            ddl.SelectedIndex = 0

            ddlValues.Close()

            _conn.Close()
            _conn.Dispose()

            'Return   _conn
        End Function


        Public Function CarregarRegistro(ByVal COD As String) As DataTable

            Dim sql As String = "SELECT * FROM TCIDADE (NOLOCK) WHERE codigo=@COD"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 3).Value = COD

            _cmd.Prepare()

            Dim da As New SqlDataAdapter(_cmd)

            _dt = New DataTable

            Try
                da.Fill(_dt)
            Catch ex As Exception

            End Try

            Return _dt

        End Function

    End Class

End Namespace