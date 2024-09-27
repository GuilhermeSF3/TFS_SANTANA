Public Class multilinetextbox
    Inherits Web.UI.WebControls.TextBox

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

    '## propriedade que define se deve ser sempre maiusculo
    Dim _maiusculo As Boolean = False
    Public Overridable Property MAIUSCULO() As Boolean
        Get
            Return Me._maiusculo
        End Get
        Set(ByVal Value As Boolean)
            Me._maiusculo = Value
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
        If Me.Columns <= 10 Then Me.Columns = 80
        If Me.Rows <= 1 Then Me.Rows = 3

    End Sub


    Private Sub textbox_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.MaxLength = 0 Then
            Me.MaxLength = 2
        End If
    End Sub

    '###################### AW-ALLIANCES ###########################


    '## preenche o campo com o valor informado
    Public Overridable Sub awa_preenche(ByVal val As String)
        If val.Length > Me.MaxLength And Me.MaxLength > 0 Then val = val.Substring(0, Me.MaxLength)
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
            If Text.Length > Me.MaxLength Then Return Me.Text.Substring(0, Me.MaxLength)
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
        If Me._obrigatorio And Me.Text.Trim() = "" Then
            Return Me._nome & " é obrigatório."
        ElseIf Me._maiusculo Then
            Me.Text = Me.Text.ToUpper
        End If

        Return ""
    End Function


    Protected Overrides Function SaveViewState() As Object
        Dim o(4) As Object
        o(0) = MyBase.SaveViewState
        o(1) = Me._obrigatorio
        o(2) = Me._maiusculo
        o(3) = Me._default
        o(4) = Me._nome
        Return o
    End Function

    Protected Overrides Sub LoadViewState(ByVal savedState As Object)
        MyBase.LoadViewState(savedState(0))
        Me._obrigatorio = savedState(1)
        Me._maiusculo = savedState(2)
        Me._default = savedState(3)
        Me._nome = savedState(4)
    End Sub

    Public Overrides Property TextMode() As System.Web.UI.WebControls.TextBoxMode
        Get
            Return Web.UI.WebControls.TextBoxMode.MultiLine
        End Get
        Set(ByVal Value As System.Web.UI.WebControls.TextBoxMode)

        End Set
    End Property

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        Me.Attributes.Add("onkeypress", Me.ID & "_keydown(this)")
        MyBase.Render(writer)

        writer.Write("<script language=javascript>" & ControlChars.CrLf)
        writer.Write("function " & Me.ID & "_keydown(campo) { " & ControlChars.CrLf)
        writer.Write("if (campo.value.length >= " & Me.MaxLength & ") { event.returnValue = false; return;}" & ControlChars.CrLf)

        writer.Write("}" & ControlChars.CrLf)
        writer.Write("</script>" & ControlChars.CrLf)
    End Sub

    Public Sub setfocus()
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), Me.ClientID & "focus", "<script language=javascript>document.all." & Me.ClientID & ".focus();</script>")
    End Sub
End Class


