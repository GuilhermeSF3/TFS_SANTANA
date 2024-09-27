Imports System.Data
Imports System.Data.SqlClient

Namespace DataBase.PT


    Public Class DbGrupo

        ReadOnly _connectionString As String
        Dim _conn As SqlConnection
        Dim _cmd As SqlCommand
        Dim _dt As DataTable

        Public Sub New()
            _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
        End Sub


        Public Sub InserirRegistro(ByVal COD As String,
                                   ByVal DESCR As String,
                                   ByVal REGIAO As String)

            Dim sql As String = "INSERT INTO TGRUPO(codigo, grupo, cod_regiao) " &
                                "VALUES (@COD, @DESCR, @REGIAO)"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.Int).Value = COD
            _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 200).Value = DESCR
            _cmd.Parameters.Add("@REGIAO", SqlDbType.Int).Value = REGIAO

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Sub AtualizarRegistro(ByVal COD As String,
                                     ByVal DESCR As String)

            Dim sql As String = "UPDATE TGRUPO SET grupo=@DESCR " &
                                "Where codigo=@COD "

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.Int).Value = COD
            _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 100).Value = DESCR


            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Sub ApagarRegistro(ByVal COD As String)


            Dim sql As String = "DELETE FROM TGRUPO Where codigo=@COD"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 3).Value = COD

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Function CarregarTodosRegistros() As DataTable

            Dim sql As String = "SELECT G.codigo, G.grupo, G.cod_regiao, R.regiao FROM TGRUPO G (NOLOCK) INNER JOIN TREGIAO R  (NOLOCK) ON G.cod_regiao = R.codigo  ORDER BY grupo"

            Dim da As New SqlDataAdapter(sql, _connectionString)
            _dt = New DataTable

            Try
                da.Fill(_dt)
            Catch ex As Exception

            End Try

            Return _dt

        End Function

        Public Function CarregarTodosRegistros(ByVal ddl As DropDownList)

            Dim sql As String = "SELECT G.codigo, G.grupo, G.cod_regiao, R.regiao FROM TGRUPO G (NOLOCK) INNER JOIN TREGIAO R  (NOLOCK) ON G.cod_regiao = R.codigo  ORDER BY grupo"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Prepare()

            Dim ddlValues As SqlDataReader
            ddlValues = _cmd.ExecuteReader()

            ddl.DataSource = ddlValues
            ddl.DataValueField = "codigo"
            ddl.DataTextField = "grupo"
            ddl.DataBind()

            'ddl.SelectedIndex = 0

            ddlValues.Close()

            _conn.Close()
            _conn.Dispose()


        End Function


        Public Function CarregarRegistro(ByVal COD As String) As DataTable

            Dim sql As String = "SELECT * FROM TGRUPO (NOLOCK) WHERE codigo=@COD"

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