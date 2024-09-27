Imports System.Data
Imports System.Data.SqlClient


Public Class DbCobradora


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub



    Public Sub InserirRegistro(ByVal COCodProduto As Int32, _
                              ByVal COCod As Int32, _
                              ByVal CODescr As String, _
                              ByVal COAtivo As Boolean, _
                              ByVal coTela As Int32)

        Dim sql As String = "INSERT INTO tCOBRADORA(COCodProduto, COCod, CODescr, COAtivo, coTela) " & _
                            "VALUES (@COCodProduto, @COCod, @CODescr, @COAtivo, @coTela)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COCodProduto", SqlDbType.Int).Value = COCodProduto
        _cmd.Parameters.Add("@COCod", SqlDbType.Int).Value = COCod
        _cmd.Parameters.Add("@CODescr", SqlDbType.VarChar, 200).Value = CODescr
        _cmd.Parameters.Add("@COAtivo", SqlDbType.Bit).Value = COAtivo
        _cmd.Parameters.Add("@coTela", SqlDbType.Int).Value = coTela

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal COCodProduto As Int32, _
                              ByVal COCod As Int32, _
                              ByVal CODescr As String, _
                              ByVal COAtivo As Boolean, _
                              ByVal coTela As Int32)

        Dim sql As String = "UPDATE tCOBRADORA SET CODescr=@CODescr, " & _
                            "COAtivo=@COAtivo, " & _
                            "coTela=@coTela " & _
                            "Where COCodProduto=@COCodProduto " & _
                            "AND COCod=@COCod "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COCodProduto", SqlDbType.Int).Value = COCodProduto
        _cmd.Parameters.Add("@COCod", SqlDbType.Int).Value = COCod
        _cmd.Parameters.Add("@CODescr", SqlDbType.VarChar, 200).Value = CODescr
        _cmd.Parameters.Add("@COAtivo", SqlDbType.Bit).Value = COAtivo
        _cmd.Parameters.Add("@coTela", SqlDbType.Int).Value = coTela

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub ApagarRegistro(ByVal COCodProduto As Int32, _
                              ByVal COCod As Int32)


        Dim sql As String = "DELETE FROM tCOBRADORA Where COCodProduto=@COCodProduto AND COCod=@COCod"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COCodProduto", SqlDbType.Int).Value = COCodProduto
        _cmd.Parameters.Add("@COCod", SqlDbType.Int).Value = COCod

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT * FROM tCOBRADORA ORDER BY COCodProduto, COCod"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal COCodProduto As Int32, _
                                        ByVal COCod As Int32) As DataTable

        Dim sql As String = "SELECT * FROM tCOBRADORA WHERE COCodProduto=@COCodProduto AND COCod=@COCod"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@COCodProduto", SqlDbType.Int).Value = COCodProduto
        _cmd.Parameters.Add("@COCod", SqlDbType.Int).Value = COCod

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


