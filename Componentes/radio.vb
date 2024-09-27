Public Class radio
    Inherits Web.UI.WebControls.RadioButton

    Public Sub Control_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
        Me.Font.Bold = True
        Me.Font.Name = "Verdana"
        Me.Font.Size = New Web.UI.WebControls.FontUnit(8)
        Me.ForeColor = Drawing.Color.Gray


    End Sub


    Private Sub radio_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ForeColor = Drawing.Color.White
        '## trata o radio button para evitar que perca o estado
        Dim nome As String
        Dim achou As Boolean = False

        For Each nome In Web.HttpContext.Current.Request.Form.AllKeys
            If nome.EndsWith(Me.GroupName) Then
                Me.Checked = (Web.HttpContext.Current.Request.Form(nome) = Me.ID)
                achou = True
            End If
        Next
        If Not achou And Not Me.ViewState("CHECKED") Is Nothing Then Me.Checked = Me.ViewState("CHECKED")
    End Sub

    '###################### AW-ALLIANCES ###########################


    '## preenche o campo com o valor informado
    Public Sub awa_preenche(ByVal val As String)
        Checked = (val = "S")
        RaiseEvent clicado(Nothing, Nothing)
    End Sub

    '## armazena o valor default do objeto
    Public Event clicado(ByVal sender As Object, ByVal e As System.EventArgs)




    Dim _default As String = "N"
    Public Property PADRAO() As String
        Get
            Return _default
        End Get
        Set(ByVal Value As String)
            If Value = "N" Or Value = "S" Then
                _default = Value
            End If
        End Set
    End Property

    '## retorna o valor do objeto
    Public ReadOnly Property VALOR() As String
        Get
            Return IIf(Me.Checked, "S", "N")
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


    Public Function awa_valida() As String
        Return ""
    End Function


    Protected Overrides Function SaveViewState() As Object
        Me.ViewState("CHECKED") = Me.Checked
        Dim o(3) As Object
        o(0) = MyBase.SaveViewState
        o(1) = Me._default
        Return o
    End Function

    Protected Overrides Sub LoadViewState(ByVal savedState As Object)
        MyBase.LoadViewState(savedState(0))
        Me._default = savedState(1)
        Me.Checked = Me.ViewState("CHECKED")
    End Sub



    Private Sub radio_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.CheckedChanged
        RaiseEvent clicado(sender, e)
    End Sub

    Public Sub setfocus()
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), Me.ClientID & "focus", "<script language=javascript>document.all." & Me.ClientID & ".focus();</script>")
    End Sub
End Class
