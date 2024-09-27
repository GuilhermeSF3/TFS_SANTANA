Imports System.Data
Imports System.Data.SqlClient

Public Class DbMetaOperadoresInternos

    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable

    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub

    Public Sub InserirRegistro(ByVal DT_REF As DateTime,
                              ByVal COD_OPERADOR As String,
                              ByVal VLR As Double)

        Dim sql As String = "INSERT INTO TB_META_OPERADOR_INTERNO(DT_REF, COD_OPERADOR, VLR) " &
                            "VALUES (@DT_REF, @COD_OPERADOR, @VLR)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_REF", SqlDbType.SmallDateTime).Value = DT_REF
        _cmd.Parameters.Add("@COD_OPERADOR", SqlDbType.VarChar, 6).Value = COD_OPERADOR
        _cmd.Parameters.Add("@VLR", SqlDbType.Float).Value = VLR

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub

    Public Sub AtualizarRegistro(ByVal DT_REF As DateTime,
                              ByVal COD_OPERADOR As String,
                              ByVal VLR As Double)

        Dim sql As String = "UPDATE TB_META_OPERADOR_INTERNO SET VLR=@VLR " &
                            "Where COD_OPERADOR=@COD_OPERADOR " &
                            "AND DT_REF=@DT_REF "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_REF", SqlDbType.SmallDateTime).Value = DT_REF
        _cmd.Parameters.Add("@COD_OPERADOR", SqlDbType.VarChar, 6).Value = COD_OPERADOR
        _cmd.Parameters.Add("@VLR", SqlDbType.Float).Value = VLR

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub

    Public Sub ApagarRegistro(ByVal DT_REF As DateTime,
                              ByVal COD_OPERADOR As String)

        Dim sql As String = "DELETE FROM TB_META_OPERADOR_INTERNO Where DT_REF=@DT_REF AND COD_OPERADOR=@COD_OPERADOR"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_REF", SqlDbType.SmallDateTime).Value = DT_REF
        _cmd.Parameters.Add("@COD_OPERADOR", SqlDbType.VarChar, 6).Value = COD_OPERADOR

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub

    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT DT_REF, COD_OPERADOR + ' - ' + O6DESCR AS COD_OPERADOR, VLR FROM TB_META_OPERADOR_INTERNO (NOLOCK) INNER JOIN CDCSANTANAMICROCREDITO..TORG6 (NOLOCK) ON COD_OPERADOR = O6CODORG ORDER BY DT_REF DESC,COD_OPERADOR"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function

    Public Function CarregarRegistro(ByVal DT_REF As DateTime,
                              ByVal COD_OPERADOR As String) As DataTable

        Dim sql As String = "select * from TB_META_OPERADOR_INTERNO (NOLOCK) WHERE DT_REF=@DT_REF AND COD_OPERADOR=@COD_OPERADOR"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_REF", SqlDbType.SmallDateTime).Value = DT_REF
        _cmd.Parameters.Add("@COD_OPERADOR", SqlDbType.VarChar, 6).Value = COD_OPERADOR

        _cmd.Prepare()

        Dim da As New SqlDataAdapter(_cmd)

        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function

    Public Sub CarregarOperador(ByVal dllOperador As DropDownList)

        Dim sql As String = "SELECT O6CODORG, O6DESCR FROM CDCSANTANAMICROCREDITO..TORG3 (NOLOCK) " &
                            "INNER JOIN CDCSANTANAMICROCREDITO..TORG6 (NOLOCK) ON O3CODORG = O6CODORG3 " &
                            "WHERE O3DESCR LIKE '%SHOP%' AND O6DESCR LIKE '%SHOP%' " &
                            "AND O3ATIVA = 'A' " &
                            "AND O6ATIVA = 'A' ORDER BY O6DESCR "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        Dim ddlValues As SqlDataReader
        ddlValues = _cmd.ExecuteReader()

        dllOperador.DataSource = ddlValues
        dllOperador.DataValueField = "O6CODORG"
        dllOperador.DataTextField = "O6DESCR"
        dllOperador.DataBind()

        dllOperador.SelectedIndex = 0

        ddlValues.Close()

        _conn.Close()
        _conn.Dispose()

    End Sub

End Class




