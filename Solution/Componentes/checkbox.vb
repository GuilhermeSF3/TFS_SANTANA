Public Class checkbox
    Inherits Web.UI.WebControls.CheckBox


    '###################### AW-ALLIANCES ###########################

    Dim _verifica_viewstate As Boolean = True
    Public Property VERIFICA_VIEWSTATE() As Boolean
        Get
            Return Me._verifica_viewstate
        End Get
        Set(ByVal Value As Boolean)
            Me._verifica_viewstate = Value
        End Set
    End Property

    Public Property OBRIGATORIO() As Boolean
        Get
            Return True
        End Get
        Set(ByVal Value As Boolean)
        End Set
    End Property

    Dim selecionado As Boolean = False
    '## preenche o campo com o valor informado
    Public Sub awa_preenche(ByVal val As String)
        Checked = (val = "S" Or val.ToUpper = "ON")
        selecionado = Checked
    End Sub

    '## armazena o valor default do objeto


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
            Return IIf(Me.selecionado, "S", "N")
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
        Dim o(3) As Object
        o(0) = MyBase.SaveViewState
        o(1) = Me._default
        o(2) = Me.selecionado

        Return o
    End Function

    Protected Overrides Sub LoadViewState(ByVal savedState As Object)
        MyBase.LoadViewState(savedState(0))
        Me._default = savedState(1)
        selecionado = savedState(2)
    End Sub


    Public Sub checkbox_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Init
        Me.Font.Bold = False
        Me.Font.Names = Split("Verdana,Arial,Helvetica,sans-serif", ",")
        Me.Font.Size = WebControls.FontUnit.Point(8)
        Me.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
        Me.BorderWidth = New Web.UI.WebControls.Unit(0)
        Me.BackColor = Drawing.Color.White
        Me.BorderColor = Drawing.Color.FromName("#1887E6")
        Me.ForeColor = Drawing.Color.FromName("#004C8C")
    End Sub


    Private Sub checkbox_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ForeColor = Drawing.Color.White

        If Me._verifica_viewstate Then
            Dim ar() As String = Me.Page.Request.Form.AllKeys
            If Array.IndexOf(ar, "HI_" & Me.ClientID) >= 0 Then
                Me.Checked = (Me.Page.Request.Form("HI_" & Me.ClientID) = "S")
            Else
                Me.Checked = selecionado
            End If
            selecionado = Me.Checked
        End If
    End Sub

    Dim so_desenha As Boolean = False
    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)

        If so_desenha Then
            Me.Checked = selecionado
            MyBase.Render(writer)
            Return
        End If


        Dim text As New IO.StringWriter()
        Dim writer1 As New Web.UI.HtmlTextWriter(text)
        so_desenha = True
        MyBase.RenderControl(writer1)
        Dim sint As String = text.ToString

        Dim estilo As String = sint.Substring(sint.IndexOf("style="))
        estilo = estilo.Substring(7)
        estilo = Left(estilo, estilo.IndexOf(""""))

        Dim para_span As String = estilo.Replace("back", "back1")
        para_span = para_span.Replace("border", "border1")

        writer.Write("<span style=""" & para_span & """ ")

        If Me.AutoPostBack Then
            writer.Write("onmousedown=""Form1.HI_" & Me.ClientID & ".value=(getElementById('" & Me.ClientID & "').checked ? 'N' : 'S');"" >")
        Else
            writer.Write("onclick=""Form1.HI_" & Me.ClientID & ".value=(getElementById('" & Me.ClientID & "').checked ? 'S' : 'N');"" >")
        End If

        '## processa a selecao do estilo do span para colocá-lo no checkbox
        Dim estilo_posic As String = Me.Attributes("style")
        Dim estilo_check As String = estilo
        If Not estilo_posic Is Nothing Then estilo_check = estilo_check.Replace(estilo_posic, "")
        estilo_check = estilo_check.Replace("font", "font1")
        estilo_check = estilo_check.Replace(";width:", ";width1:")
        writer.Write("<input id=""" & Me.ClientID & """ type=""checkbox"" name=""" & Me.ClientID & """ style=""" & estilo_check & """ " & IIf(Me.selecionado, "CHECKED", "") & " />")
        writer.WriteLine(Me.Text & "</span>")


        '## gera um campo a mais hidden para confirmar se esta ou nao clicado. 
        '## este campo eh usado apenas em forms que tem campos com persistencia
        '## alem do postback
        writer.WriteLine("<input type=""hidden"" name=""HI_" & Me.ClientID & """ value=""" & IIf(Me.selecionado, "S", "N") & """ />")
    End Sub

    Public Sub setfocus()
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), Me.ClientID & "focus", "<script language=javascript>document.all." & Me.ClientID & ".focus();</script>")
    End Sub


End Class
