Imports System.Data
Imports System.Data.SqlClient


Public Class DbPesoIp


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub


    Public Sub InserirRegistro(ByVal DATA_REF As DateTime, _
                              ByVal RATING As String, _
                              ByVal FAIXA As String, _
                              ByVal PESO As Double)

        Dim sql As String = "INSERT INTO IP_peso(DATA_REF, RATING, FAIXA, PESO) " & _
                            "VALUES (@DATA_REF, @RATING, @FAIXA, @PESO)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DATA_REF", SqlDbType.SmallDateTime).Value = DATA_REF
        _cmd.Parameters.Add("@RATING", SqlDbType.VarChar, 3).Value = RATING
        _cmd.Parameters.Add("@FAIXA", SqlDbType.VarChar, 50).Value = FAIXA
        _cmd.Parameters.Add("@PESO", SqlDbType.Float).Value = PESO

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal DATA_REF As String,
                              ByVal RATING As String,
                              ByVal FAIXA As String,
                              ByVal PESO As Double)

        Dim sql As String = "UPDATE IP_peso SET PESO=@PESO " &
                            "Where DATA_REF=@DATA_REF " &
                            "AND RATING=@RATING " &
                            "AND FAIXA=@FAIXA"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DATA_REF", SqlDbType.VarChar, 12).Value = DATA_REF
        _cmd.Parameters.Add("@RATING", SqlDbType.VarChar, 3).Value = RATING
        _cmd.Parameters.Add("@FAIXA", SqlDbType.VarChar, 50).Value = FAIXA
        _cmd.Parameters.Add("@PESO", SqlDbType.Float).Value = PESO

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub




    Public Sub ApagarRegistro(ByVal DATA_REF As String, _
                              ByVal RATING As String, _
                              ByVal FAIXA As String)

        Dim sql As String = "DELETE FROM IP_peso Where DATA_REF=@DATA_REF AND RATING=@RATING AND FAIXA=@FAIXA"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DATA_REF", SqlDbType.VarChar, 12).Value = DATA_REF
        _cmd.Parameters.Add("@RATING", SqlDbType.VarChar, 3).Value = RATING
        _cmd.Parameters.Add("@FAIXA", SqlDbType.VarChar, 50).Value = FAIXA

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub



    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT * FROM IP_peso ORDER BY DATA_REF desc, RATING, FAIXA "

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal DATA_REF As DateTime, _
                                      ByVal RATING As String, _
                                      ByVal FAIXA As String) As DataTable

        Dim sql As String = "SELECT * FROM IP_peso WHERE DATA_REF=@DATA_REF AND RATING=@RATING AND FAIXA=@FAIXA"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DATA_REF", SqlDbType.SmallDateTime).Value = DATA_REF
        _cmd.Parameters.Add("@RATING", SqlDbType.VarChar, 3).Value = RATING
        _cmd.Parameters.Add("@FAIXA", SqlDbType.VarChar, 50).Value = FAIXA

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


