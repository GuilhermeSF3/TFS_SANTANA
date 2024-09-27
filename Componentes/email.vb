Public Class email
    Inherits Web.UI.WebControls.TextBox

    Const re = "^[\w]([\w-]{0,}[\.]{0,1})*[^\.]@[^\.]([\w-]{1,}[\.]{0,1})*[a-z]{2,}$"

    '## propriedade que define a obrigatoriedade do textbox
    Dim _obrigatorio As Boolean = False

    Public Property OBRIGATORIO() As Boolean
        Get
            Return Me._obrigatorio
        End Get
        Set(ByVal Value As Boolean)
            Me._obrigatorio = Value
        End Set
    End Property


    Public Sub Control_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
        Me.Font.Bold = False
        Me.Font.Names = Split("Verdana,Arial,Helvetica,sans-serif", ",")
        Me.Font.Size = WebControls.FontUnit.Point(8)
        Me.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
        Me.BorderWidth = New Web.UI.WebControls.Unit(1)
        Me.BackColor = Drawing.Color.FromName("#DAEAF9")
        Me.BorderColor = Drawing.Color.FromName("#1887E6")
        Me.ForeColor = Drawing.Color.FromName("#004C8C")

    End Sub


    Private Sub textbox_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MaxLength = 50

    End Sub

    '###################### AW-ALLIANCES ###########################


    '## preenche o campo com o valor informado
    Public Overridable Sub awa_preenche(ByVal val As String)
        Text = val
    End Sub

    '## armazena o valor default do objeto
    Dim _default As String = ""
    Public Overridable Property PADRAO() As String
        Get
            Return _default
        End Get
        Set(ByVal Value As String)
            _default = Value
        End Set
    End Property

    '## retorna o valor do objeto
    Public Overridable ReadOnly Property VALOR() As String
        Get
            Return Text
        End Get
    End Property

    '## nome usado em mensagens
    Dim _nome As String
    Public Property NOME() As String
        Get
            Return _nome
        End Get
        Set(ByVal Value As String)
            _nome = Value
        End Set
    End Property


    Public Overridable Function awa_valida() As String
        Me.Text = Me.Text.Trim
        If Me._obrigatorio And Me.Text = "" Then
            Return Me._nome & " é obrigatório."
        ElseIf Me.Text <> "" Then
            Dim r As New System.Text.RegularExpressions.Regex(email.re)
            Dim m As System.Text.RegularExpressions.Match = r.Match(Me.Text)
            Return IIf(m.Success, "", Me._nome & " inválido.")
        Else
            Return ""
        End If
    End Function


    Protected Overrides Function SaveViewState() As Object
        Dim o(3) As Object
        o(0) = MyBase.SaveViewState
        o(1) = Me._obrigatorio

        o(3) = Me._default
        Return o
    End Function

    Protected Overrides Sub LoadViewState(ByVal savedState As Object)
        MyBase.LoadViewState(savedState(0))
        Me._obrigatorio = savedState(1)

        Me._default = savedState(3)
    End Sub

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        Me.Attributes.Add("onchange", Me.ID & "_valida(this);")
        MyBase.Render(writer)
        writer.Write("<script language=JavaScript>" & ControlChars.CrLf)
        writer.Write("function " & Me.ID & "_valida(campo){ " & ControlChars.CrLf)
        writer.Write("var valido = true;" & ControlChars.CrLf)
        If Me.OBRIGATORIO Then
            writer.Write("if (campo.value == '') valido = false;" & ControlChars.CrLf)
        End If
        writer.Write("var re = /" & email.re & "/;" & ControlChars.CrLf)
        writer.Write("valido = (valido && re.test(campo.value));" & ControlChars.CrLf)

        writer.Write("campo.style.borderColor = (valido ? '" & Me.BorderColor.Name & "' : 'Red');" & ControlChars.CrLf)
        writer.Write("}" & ControlChars.CrLf)
        writer.Write("</script>" & ControlChars.CrLf)


    End Sub

    Public Sub setfocus()
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), Me.ClientID & "focus", "<script language=javascript>document.all." & Me.ClientID & ".focus();</script>")
    End Sub
End Class


