Public Class cep
    Inherits maskedit

    Const re = "^[0-9]{5}-[0-9]{3}$"

    '## propriedade que define a obrigatoriedade do textbox


    Public Sub Control_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
        Me.Font.Bold = False
        Me.Font.Name = "Arial"
        Me.Font.Size = WebControls.FontUnit.Point(8)
        Me.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
        Me.BorderWidth = New Web.UI.WebControls.Unit(1)
        Me.BorderColor = Drawing.Color.DarkBlue
        Me.Height = New Web.UI.WebControls.Unit(20)
        Me.MaxLength = 9
        Me.FORMATACAO = "#####-###"
        Me.EXPRESSAO_REGULAR = cep.re
    End Sub


    Private Sub textbox_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    '###################### AW-ALLIANCES ###########################


    '## preenche o campo com o valor informado
    Public Overridable Sub awa_preenche(ByVal val As String)
        If val.Trim.Length = 8 And val.IndexOf("-") < 0 Then
            val = val.Substring(0, 5) & "-" & val.Substring(5)
        End If
        Me.TEXTO_FORMATADO = val
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
            Return Me.TEXTO_FORMATADO
        End Get
    End Property

    '## nome usado em mensagens
    Dim _nome As String = "CEP"
    Public Property NOME() As String
        Get
            Return _nome
        End Get
        Set(ByVal Value As String)
            _nome = Value
        End Set
    End Property


    Public Overridable Function awa_valida() As String
        Return Me.VALIDA(Me._nome)
    End Function


    Protected Overrides Function SaveViewState() As Object
        Dim o(3) As Object
        o(0) = MyBase.SaveViewState
        o(2) = Me._nome
        o(3) = Me._default
        Return o
    End Function

    Protected Overrides Sub LoadViewState(ByVal savedState As Object)
        MyBase.LoadViewState(savedState(0))
        Me._nome = savedState(2)
        Me._default = savedState(3)
    End Sub

End Class
