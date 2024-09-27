Public Class CodigoReferenciax
    Inherits Web.UI.WebControls.TextBox

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="writer"></param>
    ''' <remarks></remarks>
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

        writer.Write("    if (tecla != 44 && (tecla < 48 || tecla > 57))" & ControlChars.CrLf)
        writer.Write("        tecla = 0;" & ControlChars.CrLf)

        writer.Write("    event.keyCode = tecla;" & ControlChars.CrLf)
        writer.Write("}" & ControlChars.CrLf)

        writer.Write("function " & Me.ID & "_onBlur(componente) {" & ControlChars.CrLf)
        writer.Write("    if (componente.value.length > 0 && componente.value.slice(componente.value.length - 1) == ',') {" & ControlChars.CrLf)
        writer.Write("      if (componente.value.length == 1) {" & ControlChars.CrLf)
        writer.Write("          componente.value = '';" & ControlChars.CrLf)
        writer.Write("      } else {" & ControlChars.CrLf)
        writer.Write("          componente.value = componente.value.substring(0, componente.value.length - 1);" & ControlChars.CrLf)
        writer.Write("      }" & ControlChars.CrLf)
        writer.Write("    }" & ControlChars.CrLf)
        writer.Write("}" & ControlChars.CrLf)

        writer.Write("</script>" & ControlChars.CrLf)

    End Sub

End Class