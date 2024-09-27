Public Class Datax
    Inherits Web.UI.WebControls.TextBox

#Region "Public Properties"

    Private mCampoAssociado as String
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Data() As DateTime
        Get
            Try
                Return DateTime.Parse(Me.Text, New System.Globalization.CultureInfo("pt-BR"))
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property

    Public Property CampoAssociado() As String
        Get
            Return mCampoAssociado
        End Get

        Set(ByVal value As String)
            mCampoAssociado = value
        End Set
    End Property


#End Region

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub Control_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
        Me.MaxLength = 10
    End Sub

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

        writer.Write("    var barras = 0;" & ControlChars.CrLf)

        writer.Write("    for (i = 0; i < componente.value.length; i++) {" & ControlChars.CrLf)
        writer.Write("        if (componente.value.slice(i,i+1) == ""/"")" & ControlChars.CrLf)
        writer.Write("            barras++;" & ControlChars.CrLf)
        writer.Write("    }" & ControlChars.CrLf)

        writer.Write("    if ((tecla == 47 && barras > 1) || (tecla != 47 && (tecla < 48 || tecla > 57)))" & ControlChars.CrLf)
        writer.Write("        tecla = 0;" & ControlChars.CrLf)

        writer.Write("    event.keyCode = tecla;" & ControlChars.CrLf)
        writer.Write("}" & ControlChars.CrLf)

        writer.Write("function " & Me.ID & "_onBlur(componente) {" & ControlChars.CrLf)
        writer.Write("    var dia = 0, mes = 0, ano = 0;" & ControlChars.CrLf)
        writer.Write("    var barras = 0;" & ControlChars.CrLf)
        writer.Write("    var data = """";" & ControlChars.CrLf)
        writer.Write("    var mensagem = """";" & ControlChars.CrLf)

        writer.Write("    if (componente.value.length == 0)" & ControlChars.CrLf)
        writer.Write("        mensagem = ""Data Inválida!!!"";" & ControlChars.CrLf)

        'writer.Write("    if (componente.value.length == 0)" & ControlChars.CrLf)
        'writer.Write("        return;" & ControlChars.CrLf)

        writer.Write("    for (i = 0; i < componente.value.length; i++) {" & ControlChars.CrLf)
        writer.Write("        if (componente.value.slice(i,i+1) == ""/"")" & ControlChars.CrLf)
        writer.Write("            barras++;" & ControlChars.CrLf)
        writer.Write("        else" & ControlChars.CrLf)
        writer.Write("            data += componente.value.slice(i,i+1);" & ControlChars.CrLf)
        writer.Write("    }" & ControlChars.CrLf)

        writer.Write("    if ((barras != 0 && barras != 2) || (data.length != 6 && data.length != 8)) {" & ControlChars.CrLf)
        writer.Write("        window.alert(""Data inválida!"");" & ControlChars.CrLf)
        writer.Write("        componente.focus();" & ControlChars.CrLf)
        writer.Write("        return;" & ControlChars.CrLf)
        writer.Write("    } else {" & ControlChars.CrLf)
        writer.Write("        dia = data.slice(0,2);" & ControlChars.CrLf)
        writer.Write("        mes = data.slice(2,4);" & ControlChars.CrLf)
        writer.Write("        ano = data.slice(4);" & ControlChars.CrLf)
        writer.Write("    }" & ControlChars.CrLf)

        writer.Write("    if (ano < 100)" & ControlChars.CrLf)
        writer.Write("        ano = ""20"" + ano;" & ControlChars.CrLf)
        writer.Write("    else if (ano < 1000)" & ControlChars.CrLf)
        writer.Write("        ano = ""2"" + ano;" & ControlChars.CrLf)

        'writer.Write("        window.alert(componente.value.length);" & ControlChars.CrLf)

        writer.Write("    if (ano.length != 2 && ano.length != 4) {" & ControlChars.CrLf)
        writer.Write("        mensagem = ""Ano Inválido"";" & ControlChars.CrLf)
        writer.Write("    } else if (dia < 1 || dia > 31) {" & ControlChars.CrLf)
        writer.Write("        mensagem = ""Dia Inválido."";" & ControlChars.CrLf)
        writer.Write("    } else if (mes < 1 || mes > 12) {" & ControlChars.CrLf)
        writer.Write("        mensagem = ""Mes Inválido."";" & ControlChars.CrLf)
        writer.Write("    } else if (ano <= 1800) {" & ControlChars.CrLf)
        writer.Write("        mensagem = ""Ano deve ser maior que 1800."";" & ControlChars.CrLf)
        writer.Write("    } else if (dia > 30 && (mes == 04 || mes == 06 || mes == 09 || mes == 11)) {" & ControlChars.CrLf)
        writer.Write("        mensagem = ""Dia Inválido para o mes "" + mes + ""."";" & ControlChars.CrLf)
        writer.Write("    } else if (mes == 2 && dia > 29) {" & ControlChars.CrLf)
        writer.Write("        mensagem = ""Dia Inválido para fevereiro."";" & ControlChars.CrLf)
        writer.Write("    } else if (dia == 29 && mes == 2 && (ano % 4 != 0)) {" & ControlChars.CrLf)
        writer.Write("        mensagem = ""Dia Inválido"";" & ControlChars.CrLf)
        writer.Write("    }" & ControlChars.CrLf)

        writer.Write("    try {" & ControlChars.CrLf)
        writer.Write("        new Date(ano, mes, dia)" & ControlChars.CrLf)
        writer.Write("        componente.value = dia + ""/"" + mes + ""/"" + ano;" & ControlChars.CrLf)
        writer.Write("    } catch(err) {" & ControlChars.CrLf)
        writer.Write("        mensagem = ""Data Inválida!!!"";" & ControlChars.CrLf)
        writer.Write("    }" & ControlChars.CrLf)

        writer.Write("    if (mensagem.length != 0) {" & ControlChars.CrLf)
        writer.Write("        window.alert(mensagem);" & ControlChars.CrLf)
        writer.Write("        componente.focus();" & ControlChars.CrLf)
        writer.Write("    }" & ControlChars.CrLf)

        If Not CampoAssociado is Nothing Then
            writer.Write(" " & CampoAssociado & ".value = componente.value; " & ControlChars.CrLf)
        End If

        writer.Write("}" & ControlChars.CrLf)
        writer.Write("</script>" & ControlChars.CrLf)

    End Sub

End Class