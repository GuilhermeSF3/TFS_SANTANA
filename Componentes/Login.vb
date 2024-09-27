Public Class Login
    Inherits Web.UI.WebControls.TextBox

#Region "Public Properties"

    Private mCampoAssociado As String

    Public Property CampoAssociado() As String
        Get
            Return mCampoAssociado
        End Get

        Set(ByVal value As String)
            mCampoAssociado = value
        End Set
    End Property

#End Region

    Public Sub Control_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
        Me.MaxLength = 20
    End Sub

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        Me.Style.Add("text-align", "left")

        Me.Attributes.Remove("onKeyPress")
        Me.Attributes.Add("onKeyPress", Me.ID & "_onKeyPress(this, event.keyCode)")

        Me.Attributes.Remove("onBlur")
        Me.Attributes.Add("onBlur", Me.ID & "_onBlur(this)")

        MyBase.Render(writer)

        writer.Write(ControlChars.CrLf)
        writer.Write("<script type=""text/javascript"">" & ControlChars.CrLf)

        writer.Write("function " & Me.ID & "_onKeyPress(componente, tecla) {" & ControlChars.CrLf)
        writer.Write("    if ((tecla < 97 || tecla > 122) && (tecla < 65 || tecla > 90) && (tecla < 46 || tecla > 46))" & ControlChars.CrLf)
        writer.Write("        tecla = 0;" & ControlChars.CrLf)
        writer.Write("    event.keyCode = tecla;" & ControlChars.CrLf)
        writer.Write("}" & ControlChars.CrLf)

        writer.Write("function " & Me.ID & "_onBlur(componente) {" & ControlChars.CrLf)
        'writer.Write("    var barras = 0;" & ControlChars.CrLf)
        'writer.Write("    var mensagem = """";" & ControlChars.CrLf)
        'writer.Write("    if (componente.value.length == 0)" & ControlChars.CrLf)
        'writer.Write("        mensagem = ""Login em Branco!"";" & ControlChars.CrLf)
        'writer.Write("    if (mensagem.length != 0) {" & ControlChars.CrLf)
        'writer.Write("        window.alert(mensagem);" & ControlChars.CrLf)
        'writer.Write("        componente.focus();" & ControlChars.CrLf)
        'writer.Write("    }" & ControlChars.CrLf)
        If Not CampoAssociado Is Nothing Then
            writer.Write(" " & CampoAssociado & ".value = componente.value; " & ControlChars.CrLf)
        End If
        writer.Write("}" & ControlChars.CrLf)

        writer.Write("</script>" & ControlChars.CrLf)

    End Sub

End Class
