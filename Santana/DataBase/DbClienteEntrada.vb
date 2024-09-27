Imports System.Data
Imports System.Data.SqlClient



Public Class DbClienteEntrada

    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub


    Public Sub InserirRegistro(ByVal cod As Integer, _
                              ByVal DT_DE As DateTime)
        Dim sql As String = ""

       
        sql = "INSERT INTO  FX_PJ_CLIENTE_entrada(cod, DT_DE) " & _
                      "VALUES (@cod, @DT_DE)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod", SqlDbType.Int).Value = cod
        _cmd.Parameters.Add("@DT_DE", SqlDbType.DateTime, 100).Value = DT_DE



        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal cod As Integer, _
                              ByVal DT_DE As DateTime)

        Dim sql As String
        sql = "UPDATE  FX_PJ_CLIENTE_entrada SET DT_DE=@DT_DE " & _
                            "Where cod = @cod "

     


        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod", SqlDbType.Int).Value = cod
        _cmd.Parameters.Add("@DT_DE", SqlDbType.DateTime, 100).Value = DT_DE
    

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub




    Public Sub ApagarRegistro(ByVal cod As Integer)

        Dim sql As String = "DELETE FROM  FX_PJ_CLIENTE_entrada Where cod=@cod"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod", SqlDbType.Int).Value = cod

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub



    Public Function CarregarTodosRegistros() As DataTable

        'Dim sql As String = "select CO.*,O.OPER_ABV_NOM as OpName, C.Name as CName, GRUPO_OPERADOR= 'FALTA TABELA' from  FX_PJ_CLIENTE_entrada CO (NOLOCK) " +
        '                    "INNER JOIN TB_OPER O ON CO.COD_OPERADOR = O.cod " +
        '                    "INNER JOIN Customer C ON C.Code = CO.COD"

        Dim sql As String = "SELECT * FROM  FX_PJ_CLIENTE_entrada (NOLOCK)"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal cod As Integer) As DataTable

        Dim sql As String = "select * FROM  FX_PJ_CLIENTE_entrada (NOLOCK) Where cod=@cod"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@cod", SqlDbType.Int).Value = cod

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





