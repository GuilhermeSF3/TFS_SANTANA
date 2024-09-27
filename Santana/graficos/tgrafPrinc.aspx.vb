Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Web

Partial Class tgrafprinc
    Inherits System.Web.UI.Page

    Protected Sub Fechar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Retorna.Click
        Response.Redirect("Menu.aspx", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sUsuario As String = Session.Item("acesso")
        Dim sPerfil As Integer = Session.Item("perfil")
        If sUsuario = Nothing Then
            Response.Redirect("Logon.aspx", True)
        Else
            LblUsu.Text = sUsuario
        End If
    End Sub

    'Protected Sub Exportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Exportar.Click

    'GridView1.HeaderStyle.BackColor = Drawing.Color.White

    'GridView1.AllowPaging = False

    'GridView1.ShowHeader = True

    'GridView1.DataBind()

    'ExportarExcel(GridView1, "PDD_MESANO")

    'GridView1.AllowPaging = True

    'GridView1.ShowHeader = False

    'GridView1.DataBind()
    'End Sub
    'Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    ' Não apagar esta Sub -> Exportação para o Excel
    'End Sub

    'Sub ExportarExcel(ByVal dgv As GridView, ByVal saveAsFile As String)

    'If dgv.Rows.Count.ToString + 1 < 65536 Then

    '    Dim tw As New StringWriter()
    '    Dim hw As New System.Web.UI.HtmlTextWriter(tw)
    '    Dim frm As HtmlForm = New HtmlForm()

    '    Response.Clear()
    '    ' Response.ContentEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1")
    '    ' Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250")
    '    Response.ContentEncoding = System.Text.Encoding.Default

    '    Response.ContentType = "application/vnd.ms-excel"
    '    Response.AddHeader("content-disposition", "attachment;filename=" & saveAsFile & ".xls")


    '    Response.Charset = ""
    '    EnableViewState = False
    '    'EnableEventValidation = False

    '    Controls.Add(frm)
    '    frm.Controls.Add(dgv)
    '    frm.RenderControl(hw)

    '    Response.Write(tw.ToString())
    '    ' Response.Flush()
    '    Response.End()

    'Else
    '    MsgBox(" planilha possui muitas linhas, não é possível exportar para o Excel")
    'End If

    'End Sub




    Protected Sub tabMenu_MenuItemClick(ByVal sender As Object, ByVal e As MenuEventArgs)


        Select Case e.Item.Value

            Case "t1"
                MultiView1.ActiveViewIndex = 0


                'javascript:show('Chart')
                'Response.Redirect("tgrafico.aspx", True)
                Exit Sub

            Case "t2"
                MultiView1.ActiveViewIndex = 1

                Exit Sub

            Case "t3"
                MultiView1.ActiveViewIndex = 2
                Response.Redirect("tgrafico.aspx", True)
                Exit Sub

            Case "t4"
                MultiView1.ActiveViewIndex = 3
                Response.Redirect("tgrafico.aspx", True)
                Exit Sub

            Case "t5"
                MultiView1.ActiveViewIndex = 4
                Response.Redirect("tgrafico.aspx", True)
                Exit Sub

            Case "t6"
                MultiView1.ActiveViewIndex = 5
                Response.Redirect("tgrafico.aspx", True)
                Exit Sub

            Case "t7"
                MultiView1.ActiveViewIndex = 6
                Response.Redirect("tgrafico.aspx", True)
                Exit Sub

        End Select

    End Sub
End Class