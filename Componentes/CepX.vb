Public Class CepX
    Inherits Web.UI.WebControls.TextBox

    Private Sub CepX_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Me.MaxLength = 9
    End Sub

    Public ReadOnly Property Cep() As String
        Get
            Return Me.Text.Replace("-", "")
        End Get
    End Property

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        Me.Style.Add("text-align", "left")

        Me.Attributes.Remove("onKeyPress")
        Me.Attributes.Add("onKeyPress", Me.ID & "_onKeyPress(this, event.keyCode)")
        Me.Attributes.Remove("onBlur")
        Me.Attributes.Add("onBlur", Me.ID & "_onBlur(this)")
        MyBase.Render(writer)

        writer.Write("<script type=""text/javascript"">" & ControlChars.CrLf)
        writer.Write("" & ControlChars.CrLf)
        writer.Write("function " & Me.ID & "_onKeyPress(cep,evnt)" & ControlChars.CrLf)
        writer.Write("{" & ControlChars.CrLf)
        writer.Write("    if (cep.value.length > 4){" & ControlChars.CrLf)
        writer.Write("        if (cep.value.indexOf('') != 5){" & ControlChars.CrLf)
        writer.Write("            cep.value = cep.value.substring(0,5) " + "-" + " cep.value.substring(5);" & ControlChars.CrLf)
        writer.Write("        }" & ControlChars.CrLf)
        writer.Write("    }" & ControlChars.CrLf)
        writer.Write("    if (evnt.keyCode < 48 || evnt.keyCode > 57){" & ControlChars.CrLf)
        writer.Write("        evnt.keyCode = 0;" & ControlChars.CrLf)
        writer.Write("        return false;" & ControlChars.CrLf)
        writer.Write("    } " & ControlChars.CrLf)
        writer.Write("}" & ControlChars.CrLf)

        writer.Write("function " & Me.ID & "_onBlur(cep){" & ControlChars.CrLf)
        writer.Write("    var strcep = cep.value;" & ControlChars.CrLf)
        writer.Write("    if (strcep != ''){" & ControlChars.CrLf)
        writer.Write("        if (strcep.indexOf('-') != 5){" & ControlChars.CrLf)
        writer.Write("            alert('Formato de CEP informado inválido.');" & ControlChars.CrLf)
        writer.Write("            cep.focus();" & ControlChars.CrLf)
        writer.Write("                return false;" & ControlChars.CrLf)
        writer.Write("        }" & ControlChars.CrLf)
        writer.Write("    }" & ControlChars.CrLf)
        writer.Write("}" & ControlChars.CrLf)
        writer.Write("</script>" & ControlChars.CrLf)
    End Sub
End Class
