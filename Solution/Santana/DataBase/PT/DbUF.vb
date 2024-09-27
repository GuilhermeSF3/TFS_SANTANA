Imports System.Data
Imports System.Data.SqlClient

Namespace DataBase.PT


    Public Class DbUF


        ReadOnly _connectionString As String
        Dim _conn As SqlConnection
        Dim _cmd As SqlCommand
        Dim _dt As DataTable


        Public Sub New()
            _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
        End Sub


        Public Sub InserirRegistro(ByVal COD As String,
                                   ByVal DESCR As String)

            Dim sql As String = "INSERT INTO TUF(codigo, uf) " &
                                "VALUES (@COD, @DESCR)"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 3).Value = COD
            _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 100).Value = DESCR

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Sub AtualizarRegistro(ByVal COD As String,
                                     ByVal DESCR As String)

            Dim sql As String = "UPDATE TUF SET uf=@DESCR " &
                                "Where codigo=@COD "

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 3).Value = COD
            _cmd.Parameters.Add("@DESCR", SqlDbType.VarChar, 100).Value = DESCR

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Sub ApagarRegistro(ByVal COD As String)


            Dim sql As String = "DELETE FROM TUF Where codigo=@COD"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Parameters.Add("@COD", SqlDbType.VarChar, 3).Value = COD

            _cmd.Prepare()
            _cmd.ExecuteNonQuery()
            _conn.Close()

        End Sub


        Public Function CarregarTodosRegistros() As DataTable

            Dim sql As String = "SELECT * FROM TUF (NOLOCK) ORDER BY codigo"

            Dim da As New SqlDataAdapter(sql, _connectionString)
            _dt = New DataTable

            Try
                da.Fill(_dt)
            Catch ex As Exception

            End Try

            Return _dt

        End Function


        Public Function CarregarTodosRegistros(ByVal ddl As DropDownList)

            Dim sql As String = "SELECT * FROM TUF (NOLOCK) ORDER BY codigo"

            _conn = New SqlConnection(_connectionString)
            _conn.Open()
            _cmd = New SqlCommand(sql, _conn)

            _cmd.Prepare()

            Dim ddlValues As SqlDataReader
            ddlValues = _cmd.ExecuteReader()

            ddl.DataSource = ddlValues
            ddl.DataValueField = "codigo"
            ddl.DataTextField = "UF"
            ddl.DataBind()

            ddl.SelectedIndex = 0

            ddlValues.Close()

            _conn.Close()
            _conn.Dispose()

        End Function

        Public Function CarregarRegistro(ByVal COD As String) As DataTable

            Dim sql As String = "SELECT * FROM TUF (NOLOCK) WHERE codigo=@COD"

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