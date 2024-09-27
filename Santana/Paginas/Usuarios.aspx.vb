Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Web
Imports System.Data.SqlClient
Imports System.Data

Public Class Tabelas_Usuarios
    Inherits System.Web.UI.Page
    Protected Sub Fechar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Retorna.Click
        Response.Redirect("..\Menu.aspx", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sUsuario As String = Session.Item("acesso")
        Dim sPerfil As Integer = Session.Item("perfil")
        lblMensagem2.Text = ""
        lblMensagem.Text = ""


        If sUsuario = Nothing Then
            Response.Redirect("..\Logon.aspx", True)
        End If

        If sPerfil < 9 Then
            Response.Redirect("..\Menu.aspx", True)
        End If

        If sPerfil > 8 Then
            Exportar.Visible = True
        End If

        HyperLinkImprime.NavigateUrl = "javascript:ImprimeTab_Usuarios('" & Trim(Label1.Text) & "')"
    End Sub

    Protected Sub Exportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Exportar.Click

        GridView1.HeaderStyle.BackColor = Drawing.Color.White

        GridView1.AllowPaging = False

        GridView1.ShowHeader = True

        GridView1.DataBind()

        ExportarExcel(GridView1, "TABELAS_USUARIOS")

        GridView1.AllowPaging = True

        GridView1.ShowHeader = False

        GridView1.DataBind()
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Não apagar esta Sub -> Exportação para o Excel
    End Sub

    Sub ExportarExcel(ByVal dgv As GridView, ByVal saveAsFile As String)

        If dgv.Rows.Count.ToString + 1 < 65536 Then

            Dim tw As New StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim frm As HtmlForm = New HtmlForm()

            Response.Clear()
            ' Response.ContentEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1")
            ' Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250")
            Response.ContentEncoding = System.Text.Encoding.Default

            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment;filename=" & saveAsFile & ".xls")

            Response.Charset = ""
            EnableViewState = False
            'EnableEventValidation = False

            Controls.Add(frm)
            frm.Controls.Add(dgv)
            frm.RenderControl(hw)

            Response.Write(tw.ToString())
            ' Response.Flush()
            Response.End()

        Else
            lblMensagem2.Text = " planilha possui muitas linhas, não é possível exportar para o Excel"
        End If

    End Sub

    Protected Sub BtnLocalizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnLocalizar.Click
        'SqlDataSource1.SelectCommand = "select * from Tempregador where emDescr like '%" & TxtLocalizar.Text & "%' "
        'SqlCommand.Parameters.Add(New SqlParameter("@nome", TxtLocalizar.Text))
        SqlDataSource1.DataBind()

    End Sub
    Protected Sub BtnTodos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnTodos.Click
        TxtLocalizar.Text = "%%"
        SqlDataSource1.DataBind()

    End Sub

    Protected Sub HyperLinkImprime_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles HyperLinkImprime.Load
        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim connection As New SqlConnection(strConn)
        Dim ds As New DataSet
        Dim dataTable As DataTable = Nothing
        Dim sTabela As String = ""
        Dim sTitulo As String = ""

        connection.Open()
        Dim command As SqlCommand = New SqlCommand("SELECT Login, NomeUsuario, Funcao, CodGerente, CodFilial, Cpf, EMail, Ativo, NomeCompleto, senha, perfil, codfilial, funcao, codgerente FROM Usuario where Login like '%" & TxtLocalizar.Text & "%' or  email like '%" & TxtLocalizar.Text & "%' or nomeUsuario like '%" & TxtLocalizar.Text & "%' ", connection)

        dataTable = Util.ClassBD.ExecuteDataTable(command)
        connection.Close()

        sTabela = "Usuario"
        sTitulo = "Tabela Usuários"
        Session.Add("Usuario", dataTable)
        Session.Add("RELATORIO", sTabela)
        Session.Add("TITULO", sTitulo)
    End Sub

    Protected Sub DetailsView1_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewInsertedEventArgs) Handles DetailsView1.ItemInserted
        With GridView1
            .DataBind()
            .SelectedIndex = -1
        End With
        lblMensagem2.Text = "Usuário cadastrado com sucesso."
    End Sub

    Sub DetailsView1_ItemInserting(ByVal sender As Object, ByVal e As DetailsViewInsertEventArgs) Handles DetailsView1.ItemInserting
        Dim t1 As TextBox = DirectCast(DetailsView1.Rows(0).Cells(1).Controls(0), TextBox)
        Dim t2 As TextBox = DirectCast(DetailsView1.Rows(1).Cells(1).Controls(0), TextBox)
        Dim t3 As TextBox = DirectCast(DetailsView1.Rows(2).Cells(1).Controls(0), TextBox)
        Dim t4 As TextBox = DirectCast(DetailsView1.Rows(3).Cells(1).Controls(0), TextBox)
        Dim t5 As TextBox = DirectCast(DetailsView1.Rows(4).Cells(1).Controls(0), TextBox)
        Dim t6 As TextBox = DirectCast(DetailsView1.Rows(5).Cells(1).Controls(0), TextBox)
        Dim t7 As TextBox = DirectCast(DetailsView1.Rows(6).Cells(1).Controls(0), TextBox)
        Dim t8 As TextBox = DirectCast(DetailsView1.Rows(7).Cells(1).Controls(0), TextBox)


        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim conexao As New SqlConnection(strConn)
        'Dim conexao2 As New SqlConnection(strConn)
        Dim comando3 As New SqlCommand
        Dim dtProces1 As String = Nothing
        Dim dtProces2 As String = Nothing

        conexao.Open()
        comando3 = New SqlCommand("SELECT distinct Login, NomeUsuario FROM Usuario where login='" & t1.Text & "' or NomeUsuario='" & t2.Text & "' ", conexao)
        comando3.CommandTimeout = 800000
        comando3.ExecuteNonQuery()

        Dim dr As SqlDataReader = comando3.ExecuteReader
        While dr.Read()
            dtProces1 = dr.GetString(0)
            dtProces2 = dr.GetString(1)
        End While
        dr.Close()
        conexao.Close()

        If t1.Text = "" Or t2.Text = "" Or t3.Text = "" Or t4.Text = "" Or t5.Text = "" Or t6.Text = "" Or t7.Text = "" Or t8.Text = "" Then
            lblMensagem2.Text = "Favor preencher todos os campos."
            e.Cancel = True
            Exit Sub
        End If

        Dim keyValue As String = e.Values("login").ToString()
        Dim keyValue2 As String = e.Values("NomeUsuario").ToString()

        If keyValue.ToString = dtProces1 Then
            lblMensagem2.Text = " O Login " & t1.Text & " ja esta cadastrado para o usuário " & dtProces2 & "."
            e.Cancel = True
            Exit Sub
        Else
            If keyValue2.ToString = dtProces2 Then
                lblMensagem2.Text = "Já consta um usuário com o nome " & t2.Text & "."
                e.Cancel = True
                Exit Sub
            End If
        End If

    End Sub

    Protected Sub DetailsView1_ItemDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewDeletedEventArgs) Handles DetailsView1.ItemDeleted
        With GridView1
            .DataBind()
            .SelectedIndex = -1
        End With
        lblMensagem2.Text = "Usuário  excluido com sucesso."
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim gridRow As GridViewRow
        Dim login As String

        gridRow = CType(sender, Button).Parent.Parent
        login = gridRow.Cells(1).Text
        hdLogin.Value = login
        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim connection As New SqlConnection(strConn)
            Dim ds As New DataSet
            Dim dataTable As DataTable = Nothing

            connection.Open()
            Dim command As SqlCommand = New SqlCommand("update usuario set senha='zgv9FQWbaNZ2iIhNej0+jA=='  where login= '" & login & "' ", connection)
            command.ExecuteNonQuery()
            connection.Close()


        Catch ex As Exception
            lblMensagem.Text = "Falha Geral <SISTEMA MIS>, Entrar em contato Ramal - 5748"
            Exit Sub
        Finally
            GC.Collect()
        End Try
        GridView1.DataBind()
        lblMensagem.Text = "Senha Alterada Com Sucesso."
    End Sub
End Class

