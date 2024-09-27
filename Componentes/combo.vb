Public Class combo
    Inherits Web.UI.WebControls.DropDownList

    Public Sub Control_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
        Me.Font.Bold = False
        Me.Font.Name = "Arial"
        Me.Font.Size = System.Web.UI.WebControls.FontUnit.Point(8)
        Me.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
        Me.BorderWidth = New Web.UI.WebControls.Unit(1)
        Me.BorderColor = Drawing.Color.DarkBlue
        Me.BackColor = Drawing.Color.FromName("#DAEAF9")

        'Me.AutoPostBack = True
    End Sub

    Public Sub carrega(ByVal coluna_texto As String, ByVal coluna_codigo As String, ByVal dt As DataTable)
        Me.DataTextField = coluna_texto
        Me.DataValueField = coluna_codigo
        Me.DataSource = dt
        Me.DataBind()
    End Sub

    '###################### AW-ALLIANCES ###########################

    '## armazena a quantidade de itens adicionais criados
    Dim _qtd_adic As Integer = 0
    Dim _adicionais As Collections.Specialized.NameValueCollection

    '## adiciona itens extras no combo
    Public Function adicional(ByVal txt As String, ByVal cod As String, ByVal selecionar As Boolean) As Integer

        If Not Me.Items.FindByValue(cod) Is Nothing Then Return IIf(IsNumeric(cod), cod, 0)
        If Me._adicionais Is Nothing Then Me._adicionais = New Collections.Specialized.NameValueCollection()

        Me._qtd_adic = Me._qtd_adic + 1
        Me._adicionais.Add(txt, cod)
        Dim li As New Web.UI.WebControls.ListItem(txt, cod)

        Me.Items.Insert(0, li)

        If selecionar Then
            Me.awa_preenche(cod)
        End If

        Return IIf(IsNumeric(cod), cod, 0)
    End Function


    Private Sub adiciona_adicionais()
        If Me._qtd_adic <= 0 Then Return
        Dim s As String
        Dim li As Web.UI.WebControls.ListItem
        For Each s In Me._adicionais.AllKeys
            If Me.Items.FindByText(s) Is Nothing Then
                li = New Web.UI.WebControls.ListItem(s, Me._adicionais(s))
                Me.Items.Insert(0, li)
            End If
        Next
    End Sub


    '## seleciona por codigo
    Public Sub seleciona(ByVal cod As Long)
        Me.awa_preenche(cod)
    End Sub


    Public Sub seleciona(ByVal cod As String)
        Me.awa_preenche(cod)
    End Sub

    Public Sub seleciona_texto(ByVal cod As String)
        Dim it As Web.UI.WebControls.ListItem = Me.Items.FindByText(cod)
        If Not it Is Nothing Then
            Me.SelectedItem.Selected = False
            it.Selected = True
        End If
    End Sub


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

    '## preenche o campo com o valor informado
    Public Sub awa_preenche(ByVal val As String)
        Dim li As Web.UI.WebControls.ListItem
        li = Me.Items.FindByValue(val)
        If Not li Is Nothing Then
            If Not Me.SelectedItem Is Nothing Then
                Me.SelectedItem.Selected = False
            End If
            li.Selected = True
        End If
    End Sub

    '## armazena o valor default do objeto
    Dim _default As String = 0
    Public Property PADRAO() As String
        Get
            Return _default
        End Get
        Set(ByVal Value As String)
            _default = Value
        End Set
    End Property

    '## retorna o valor do objeto
    Public ReadOnly Property VALOR() As String
        Get
            If Not Me.SelectedItem Is Nothing Then
                Return Me.SelectedItem.Value
            End If
            Return 0
        End Get
    End Property

    '## retorna o valor do objeto
    Public ReadOnly Property TEXTO() As String
        Get
            If Not Me.SelectedItem Is Nothing Then
                Return Me.SelectedItem.Text
            End If
            Return ""
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
        If Me._obrigatorio And Me.SelectedItem Is Nothing Then Return Me._nome & " é obrigatório."
        If Me._obrigatorio Then
            If IsNumeric(Me.SelectedItem.Value) Then
                If Me.SelectedItem.Value <= 0 Then Return Me._nome & " é obrigatório."
            Else
                If Me.SelectedItem.Value = "" Then Return Me._nome & " é obrigatório."
            End If
        End If
        Return ""
    End Function

    Public Function obtem(ByVal col As String) As Object
        If Me.DataSource Is Nothing Then Return Nothing
        If Me.SelectedIndex < 0 Then Return Nothing

        If Me.DataSource.GetType.ToString.ToUpper.IndexOf("DATATABLE") < 0 Then Return Nothing
        Dim dt As DataTable = Me.DataSource
        If Not dt.Columns.Contains(col) Then Return Nothing
        If Me.SelectedIndex - Me._qtd_adic < 0 Then Return Nothing
        If dt.Rows.Count <= Me.SelectedIndex - Me._qtd_adic Then Return Nothing
        Return dt.Rows(Me.SelectedIndex - Me._qtd_adic)(col)
    End Function


    Protected Overrides Function SaveViewState() As Object
        Dim o(10) As Object
        o(0) = MyBase.SaveViewState()
        o(1) = Me._obrigatorio
        o(2) = Me._default
        o(3) = Me._qtd_adic
        o(4) = Me._adicionais
        o(5) = Me._salva_data_source
        If Me._salva_data_source Then
            o(6) = datatableutil.saveviewstate_datatable(Me.DataSource)
            o(7) = Me.DataTextField
            o(8) = Me.DataValueField
        End If

        Return o
    End Function

    Protected Overrides Sub LoadViewState(ByVal savedState As Object)
        MyBase.LoadViewState(savedState(0))
        Me._obrigatorio = savedState(1)
        Me._default = savedState(2)
        Me._qtd_adic = savedState(3)
        Me._adicionais = savedState(4)
        Me._salva_data_source = savedState(5)
        If Me._salva_data_source Then
            Me.DataSource = datatableutil.loadviewstate_datatable(savedState(6))
            Me.DataTextField = savedState(7)
            Me.DataValueField = savedState(8)
        End If
        Me.adiciona_adicionais()
    End Sub



    Public Overrides Sub DataBind()
        MyBase.DataBind()
        Me.adiciona_adicionais()
    End Sub

    Dim _salva_data_source As Boolean = False
    Public Property SALVA_DATASOURCE() As Boolean
        Get
            Return Me._salva_data_source
        End Get
        Set(ByVal Value As Boolean)
            Me._salva_data_source = Value
        End Set
    End Property

    Public Sub zera_adicionais()
        Me._adicionais = New Collections.Specialized.NameValueCollection
    End Sub

    Public Sub setfocus()
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), Me.ClientID & "focus", "<script language=javascript>document.all." & Me.ClientID & ".focus();</script>")
    End Sub


End Class
