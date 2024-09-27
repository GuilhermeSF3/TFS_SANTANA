Imports System.Data
Imports System.Data.SqlClient


Public Class DbMetaPromotoras


    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable


    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub


    Public Sub InserirRegistro(ByVal DT_REF As DateTime,
                              ByVal COD_AGENTE As String,
                              ByVal VLR As Double)

        Dim sql As String = "INSERT INTO TB_META_PROMOTORA(DT_REF, COD_AGENTE, VLR) " &
                            "VALUES (@DT_REF, @COD_AGENTE, @VLR)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_REF", SqlDbType.SmallDateTime).Value = DT_REF
        _cmd.Parameters.Add("@COD_AGENTE", SqlDbType.VarChar, 6).Value = COD_AGENTE
        _cmd.Parameters.Add("@VLR", SqlDbType.Float).Value = VLR


        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Sub AtualizarRegistro(ByVal DT_REF As DateTime,
                              ByVal COD_AGENTE As String,
                              ByVal VLR As Double)

        Dim sql As String = "UPDATE TB_META_PROMOTORA SET VLR=@VLR " &
                            "Where COD_AGENTE=@COD_AGENTE " &
                            "AND DT_REF=@DT_REF "

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)


        _cmd.Parameters.Add("@DT_REF", SqlDbType.SmallDateTime).Value = DT_REF
        _cmd.Parameters.Add("@COD_AGENTE", SqlDbType.VarChar, 6).Value = COD_AGENTE
        _cmd.Parameters.Add("@VLR", SqlDbType.Float).Value = VLR

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub




    Public Sub ApagarRegistro(ByVal DT_REF As DateTime,
                              ByVal COD_AGENTE As String)


        Dim sql As String = "DELETE FROM TB_META_PROMOTORA Where DT_REF=@DT_REF AND COD_AGENTE=@COD_AGENTE"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_REF", SqlDbType.SmallDateTime).Value = DT_REF
        _cmd.Parameters.Add("@COD_AGENTE", SqlDbType.VarChar, 6).Value = COD_AGENTE

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub



    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT DT_REF, COD_AGENTE + ' - ' + O3DESCR AS COD_AGENTE, VLR FROM TB_META_PROMOTORA (NOLOCK) " &
                            "INNER JOIN CDCSANTANAMICROCREDITO..TORG3 (NOLOCK) ON COD_AGENTE = O3CODORG " &
                            "ORDER BY DT_REF DESC,COD_AGENTE"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal DT_REF As DateTime,
                              ByVal COD_AGENTE As String) As DataTable

        Dim sql As String = "select * from TB_META_PROMOTORA (NOLOCK) WHERE DT_REF=@DT_REF AND COD_AGENTE=@COD_AGENTE"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_REF", SqlDbType.SmallDateTime).Value = DT_REF
        _cmd.Parameters.Add("@COD_AGENTE", SqlDbType.VarChar, 6).Value = COD_AGENTE

        _cmd.Prepare()

        Dim da As New SqlDataAdapter(_cmd)

        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function



    Public Sub CarregarPromotora(ByVal dllPromotora As DropDownList)

        Dim sql As String = "SELECT O3CODORG,O3DESCR FROM CDCSANTANAMICROCREDITO..TORG3 (NOLOCK) " &
                            "WHERE   O3ATIVA IN ('S','A') AND O3CODORG <> '000149' " &
                            "ORDER BY O3DESCR"
        '  O3DESCR Not Like '%SHOP%' AND antes de 11/9/19 -- removi
        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)


        Dim ddlValues As SqlDataReader
        ddlValues = _cmd.ExecuteReader()

        dllPromotora.DataSource = ddlValues
        dllPromotora.DataValueField = "O3CODORG"
        dllPromotora.DataTextField = "O3DESCR"
        dllPromotora.DataBind()

        dllPromotora.SelectedIndex = 0

        ddlValues.Close()

        _conn.Close()
        _conn.Dispose()

    End Sub

End Class




