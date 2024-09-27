Imports System.Data
Imports System.Data.SqlClient


Public Class DbMetaICM

    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable

    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub

    Public Sub InserirRegistro(ByVal DT_REF As DateTime,
                              ByVal COD_COBRADORA As String,
                              ByVal FAIXA As String,
                              ByVal ICM As Double)

        Dim sql As String = "INSERT INTO TB_META_ICM(DT_REF, COD_COBRADORA, FAIXA, ICM) " &
                            "VALUES (@DT_REF, @COD_COBRADORA, @FAIXA, @ICM)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_REF", SqlDbType.SmallDateTime).Value = DT_REF
        _cmd.Parameters.Add("@COD_COBRADORA", SqlDbType.VarChar, 6).Value = COD_COBRADORA
        _cmd.Parameters.Add("@FAIXA", SqlDbType.VarChar, 50).Value = FAIXA
        _cmd.Parameters.Add("@ICM", SqlDbType.Float).Value = ICM

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub

    Public Sub AtualizarRegistro(ByVal DT_REF As DateTime,
                              ByVal COD_COBRADORA As String,
                              ByVal FAIXA As String,
                              ByVal ICM As Double)

        Dim sql As String = "UPDATE TB_META_ICM SET ICM=@ICM " &
                            "Where COD_COBRADORA=@COD_COBRADORA " &
                            "AND DT_REF=@DT_REF " &
                            "AND FAIXA=@FAIXA"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)


        _cmd.Parameters.Add("@DT_REF", SqlDbType.SmallDateTime).Value = DT_REF
        _cmd.Parameters.Add("@COD_COBRADORA", SqlDbType.VarChar, 6).Value = COD_COBRADORA
        _cmd.Parameters.Add("@FAIXA", SqlDbType.VarChar, 50).Value = FAIXA
        _cmd.Parameters.Add("@ICM", SqlDbType.Float).Value = ICM

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub

    Public Sub ApagarRegistro(ByVal DT_REF As DateTime,
                              ByVal COD_COBRADORA As String)

        Dim sql As String = "DELETE FROM TB_META_ICM Where DT_REF=@DT_REF AND COD_COBRADORA=@COD_COBRADORA"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_REF", SqlDbType.SmallDateTime).Value = DT_REF
        _cmd.Parameters.Add("@COD_COBRADORA", SqlDbType.VarChar, 6).Value = COD_COBRADORA

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub

    Public Function CarregarTodosRegistros() As DataTable

        Dim sql As String = "SELECT DT_REF, CAST(COCOD AS VARCHAR(20)) + ' - ' + CODESCR AS COD_COBRADORA, FAIXA, ICM FROM TB_META_ICM (NOLOCK) " &
                            "INNER JOIN TCOBRADORA (NOLOCK) ON COD_COBRADORA = COCOD " &
                            "ORDER BY DT_REF DESC,COD_COBRADORA"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal DT_REF As DateTime,
                              ByVal FAIXA As String,
                              ByVal COD_COBRADORA As String) As DataTable

        Dim sql As String = "select * from TB_META_ICM (NOLOCK) WHERE DT_REF=@DT_REF AND COD_COBRADORA=@COD_COBRADORA AND FAIXA = @FAIXA"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@DT_REF", SqlDbType.SmallDateTime).Value = DT_REF
        _cmd.Parameters.Add("@COD_COBRADORA", SqlDbType.VarChar, 6).Value = COD_COBRADORA
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



    Public Sub CarregarCobradora(ByVal dllPromotora As DropDownList)

        Dim sql As String = "SELECT COCOD,CODESCR FROM tCOBRADORA (NOLOCK) " &
                            "WHERE   COATIVO = 1 ORDER BY CODESCR"
        '  O3DESCR Not Like '%SHOP%' AND antes de 11/9/19 -- removi
        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)


        Dim ddlValues As SqlDataReader
        ddlValues = _cmd.ExecuteReader()

        dllPromotora.DataSource = ddlValues
        dllPromotora.DataValueField = "COCOD"
        dllPromotora.DataTextField = "CODESCR"
        dllPromotora.DataBind()

        dllPromotora.SelectedIndex = 0

        ddlValues.Close()

        _conn.Close()
        _conn.Dispose()

    End Sub

End Class




