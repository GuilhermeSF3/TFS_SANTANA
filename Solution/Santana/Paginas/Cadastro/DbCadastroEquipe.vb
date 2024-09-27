Imports System.Data
Imports System.Data.SqlClient


Public Class DbCadastroEquipe

    ReadOnly _connectionString As String
    Dim _conn As SqlConnection
    Dim _cmd As SqlCommand
    Dim _dt As DataTable

    Public Sub New()
        _connectionString = ConfigurationManager.AppSettings("ConexaoPrincipal").ToString()
    End Sub

    Public Sub InserirRegistro(ByVal codAgente As String, ByVal codEquipe As String)

        Dim tabela As String = ""

        If codEquipe = 1 Then

            tabela = "TB_EQUIPE_DIAMANTE"

        ElseIf codEquipe = 2 Then

            tabela = "TB_EQUIPE_OURO"
        Else

            tabela = "TB_EQUIPE_PLATINUM"

        End If

        Dim sql As String = "INSERT INTO " + tabela + "(COD_AGENTE) " &
                            "VALUES (@codAgente)"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codAgente", SqlDbType.VarChar, 20).Value = codAgente

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub

    Public Sub AtualizarRegistro(ByVal codAgente As String, ByVal tabela As String)

        Dim sql As String = "UPDATE TB_EQUIPE_PRATA " &
                            "Where COD_AGENTE=@codAgente"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codAgente", SqlDbType.VarChar, 20).Value = codAgente.Substring(0, codAgente.IndexOf("-") - 1).Trim()

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub

    Public Sub ApagarRegistro(ByVal codAgente As String, ByVal tabela As String)

        Dim sql As String = "DELETE FROM TB_EQUIPE_" + tabela + " WHERE COD_AGENTE = @codAgente"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codAgente", SqlDbType.VarChar, 20).Value = codAgente.Substring(0, codAgente.IndexOf("-") - 1).Trim()

        _cmd.Prepare()
        _cmd.ExecuteNonQuery()
        _conn.Close()

    End Sub


    Public Function CarregarTodosRegistros() As DataTable
        Dim sql As String = "SELECT COD_AGENTE + ' - ' + O3DESCR AS CODAGENTE, 'PLATINUM' AS CODEQUIPE , 'SÃO PAULO ' AS CODGERENCIA FROM TB_EQUIPE_PLATINUM INNER JOIN CDCSANTANAMICROCREDITO..TORG3 (NOLOCK) ON COD_AGENTE = O3CODORG " &
                            "UNION SELECT COD_AGENTE + ' - ' + O3DESCR AS CODAGENTE, 'DIAMANTE' AS CODEQUIPE ,'SÃO PAULO ' AS CODGERENCIA FROM TB_EQUIPE_DIAMANTE INNER JOIN CDCSANTANAMICROCREDITO..TORG3 (NOLOCK) ON COD_AGENTE = O3CODORG " &
                            "UNION SELECT COD_AGENTE + ' - ' + O3DESCR AS CODAGENTE,'OURO' AS CODEQUIPE, 'SÃO PAULO ' AS CODGERENCIA FROM TB_EQUIPE_OURO INNER JOIN CDCSANTANAMICROCREDITO..TORG3 (NOLOCK) ON COD_AGENTE = O3CODORG"

        Dim da As New SqlDataAdapter(sql, _connectionString)
        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function


    Public Function CarregarRegistro(ByVal codAgente As String) As DataTable

        Dim sql As String = "SELECT COD_AGENTE FROM [TB_EQUIPE_PLATINUM] (NOLOCK) WHERE COD_AGENTE=@codAgente" &
                            " UNION SELECT COD_AGENTE FROM [TB_EQUIPE_DIAMANTE] (NOLOCK) WHERE COD_AGENTE=@codAgente" &
                            " UNION SELECT COD_AGENTE FROM [TB_EQUIPE_OURO](NOLOCK) WHERE COD_AGENTE=@codAgente"

        _conn = New SqlConnection(_connectionString)
        _conn.Open()
        _cmd = New SqlCommand(sql, _conn)

        _cmd.Parameters.Add("@codAgente", SqlDbType.VarChar, 20).Value = codAgente

        _cmd.Prepare()

        Dim da As New SqlDataAdapter(_cmd)

        _dt = New DataTable

        Try
            da.Fill(_dt)
        Catch ex As Exception

        End Try

        Return _dt

    End Function

    Public Sub CarregaAgente(ByVal ddlAgente As DropDownList)

        Dim sql As String = "SELECT O3CODORG AS CODAGENTE, O3DESCR AS NOMEAGENTE FROM CDCSANTANAMICROCREDITO..TORG3 (NOLOCK) ORDER BY O3DESCR"

        Dim conn As New SqlConnection(_connectionString)
        conn.Open()
        Dim cmd = New SqlCommand(sql, conn)


        Dim ddlValues As SqlDataReader
        ddlValues = cmd.ExecuteReader()

        ddlAgente.DataSource = ddlValues
        ddlAgente.DataValueField = "CODAGENTE"
        ddlAgente.DataTextField = "NOMEAGENTE"
        ddlAgente.DataBind()

        ddlAgente.SelectedIndex = 0

        ddlValues.Close()

        conn.Close()
        conn.Dispose()

    End Sub

    Public Sub CarregaGerencia(ByVal ddlGerencia As DropDownList)

        ddlGerencia.Items.Insert(0, New ListItem("São Paulo", "1"))
        ddlGerencia.Items.Insert(1, New ListItem("Interior", "2"))

    End Sub

    Public Sub CarregaEquipe(ByVal ddlEquipe As DropDownList)

        ddlEquipe.Items.Insert(0, New ListItem("Diamante", "1"))
        ddlEquipe.Items.Insert(1, New ListItem("Ouro", "2"))
        ddlEquipe.Items.Insert(2, New ListItem("Platinum", "3"))

    End Sub
End Class


