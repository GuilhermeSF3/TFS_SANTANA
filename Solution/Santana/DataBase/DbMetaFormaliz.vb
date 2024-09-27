Imports System.Data
Imports System.Data.SqlClient


Public Class DbMetaFormaliz


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub


    Public Sub InserirRegistro(ByVal DATAREF As DateTime,
                              ByVal META As Double)

        Dim sql As String = "INSERT INTO AUX_META_FORMALIZ(DATAREF, META) " &
                            "VALUES (@DATAREF, @META)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DATAREF", SqlDbType.SmallDateTime).Value = DATAREF
        _cmd.Parameters.Add("@META", SqlDbType.Float).Value = META


        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal DATAREF As DateTime,
                              ByVal META As Double)

        Dim sql As String = "UPDATE AUX_META_FORMALIZ SET META=@META " &
                            "Where DATAREF=@DATAREF "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)


        _cmd.Parameters.Add("@DATAREF", SqlDbType.SmallDateTime).Value = DATAREF
        _cmd.Parameters.Add("@META", SqlDbType.Float).Value = META

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub




    Public Sub ApagarRegistro(ByVal DATAREF As DateTime)


        Dim sql As String = "DELETE FROM AUX_META_FORMALIZ Where DATAREF=@DATAREF"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DATAREF", SqlDbType.SmallDateTime).Value = DATAREF

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub



    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT DATAREF, META FROM AUX_META_FORMALIZ ORDER BY DATAREF DESC"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal DATAREF As DateTime) As DataTable

        Dim sql As String = "select * from AUX_META_FORMALIZ (NOLOCK) WHERE DATAREF=@DATAREF"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DATAREF", SqlDbType.SmallDateTime).Value = DATAREF

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




