Public Class Numerox
    Inherits Web.UI.WebControls.TextBox

#Region "Private variables"

    Private mInteiro As Integer = 6
    Private mCasaDecimal As Integer = 0

#End Region

#Region "Public Properties"

    ''' <summary>
    ''' Quantidade de números inteiros.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Inteiro() As Integer
        Get
            Return mInteiro
        End Get
        Set(ByVal Value As Integer)
            If Value > 0 Then
                mInteiro = Value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Quantidade de casas decimais
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CasaDecimal() As Integer
        Get
            Return mCasaDecimal
        End Get
        Set(ByVal Value As Integer)
            If Value >= 0 Then
                mCasaDecimal = Value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Valor digitado.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Valor() As Double
        Get
            Try
                If Not IsNumeric(Me.Text) Then
                    Return 0
                Else
                    Return Double.Parse(Me.Text, New System.Globalization.CultureInfo("pt-BR"))
                End If
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

#End Region

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub Control_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
        Me.MaxLength = IIf(Me.CasaDecimal = 0, Me.Inteiro, Me.Inteiro + Me.CasaDecimal + 1)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="writer"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        Me.Style.Add("text-align", "right")

        Me.Attributes.Remove("onKeyPress")
        Me.Attributes.Add("onKeyPress", Me.ID & "_onKeyPress(this, event.keyCode)")
        Me.Attributes.Remove("onBlur")
        Me.Attributes.Add("onBlur", Me.ID & "_onBlur(this)")
        MyBase.Render(writer)
        '<input type="text" name="cnpjCpf" size="16" maxlength="15" onKeyPress="soNumeros(event.keyCode)"  value=''>

        writer.Write(ControlChars.CrLf)
        writer.Write("<script type=""text/javascript"">" & ControlChars.CrLf)
        writer.Write("function " & Me.ID & "_onKeyPress(componente, tecla) {" & ControlChars.CrLf)

        If Me.CasaDecimal > 0 Then
            writer.Write("    if (tecla == 46)" & ControlChars.CrLf) 'ponto
            writer.Write("        tecla = 44;" & ControlChars.CrLf) 'vírgula

            writer.Write("    var virgula = componente.value.indexOf("","");" & ControlChars.CrLf)

            'writer.Write("    if ((tecla == 44 && virgula > 0) || (tecla != 44 && (tecla < 48 || tecla > 57)) || (virgula > 0 && componente.value.slice(virgula, componente.value.length).length > " & Me.CasaDecimal & "))" & ControlChars.CrLf)
            writer.Write("    if ((tecla == 44 && virgula > 0) || (tecla != 44 && (tecla < 48 || tecla > 57)))" & ControlChars.CrLf)
            writer.Write("        tecla = 0;" & ControlChars.CrLf)
        Else
            writer.Write("    if (tecla < 48 || tecla > 57)" & ControlChars.CrLf)
            writer.Write("        tecla = 0;" & ControlChars.CrLf)
        End If

        writer.Write("    event.keyCode = tecla;" & ControlChars.CrLf)
        writer.Write("}" & ControlChars.CrLf)

        writer.Write("function " & Me.ID & "_onBlur(componente) {" & ControlChars.CrLf)
        writer.Write("    if (isNaN(componente.value.replace(',','.'))) {" & ControlChars.CrLf)
        writer.Write("        window.alert(""Número inválido!!!"");" & ControlChars.CrLf)
        writer.Write("        componente.focus();" & ControlChars.CrLf)
        writer.Write("    }" & ControlChars.CrLf)
        writer.Write("    else {" & ControlChars.CrLf)
        writer.Write("        var virgula2 = componente.value.indexOf("","");" & ControlChars.CrLf)
        writer.Write("        if (virgula2 > 0 && virgula2 == componente.value.length - 1) { " & ControlChars.CrLf)
        writer.Write("            componente.value = componente.value.replace(',','');" & ControlChars.CrLf)
        writer.Write("        }" & ControlChars.CrLf)
        writer.Write("    }" & ControlChars.CrLf)
        writer.Write("}" & ControlChars.CrLf)
        writer.Write("</script>" & ControlChars.CrLf)

    End Sub

End Class