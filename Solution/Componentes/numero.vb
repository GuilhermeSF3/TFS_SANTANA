Public Class numero
    Inherits Web.UI.WebControls.TextBox



    '## armazena o valor a ser editado etc

    ReadOnly Property FORMATO() As String
        Get
            Dim frm As String = ""
            Dim i As Integer
            For i = 1 To Me._tamanho_inteiro
                If i = Me._tamanho_inteiro Then
                    frm = frm & "0"
                Else
                    frm = frm & "#"
                End If
            Next
            If Me._tamanho_decimal > 0 Then
                frm = frm & "."
            End If
            For i = 1 To Me._tamanho_decimal
                frm = frm & "#"
            Next
            Return frm

        End Get
    End Property

    '## define qual eh o tamanho e casas decimais
    Dim _tamanho_inteiro As Integer = 5
    Dim _tamanho_decimal As Integer = 0
    Public Property TAMANHO_INTEIRO() As Integer
        Get
            Return _tamanho_inteiro
        End Get
        Set(ByVal Value As Integer)
            If Value > 0 Then _tamanho_inteiro = Value


        End Set
    End Property
    Public Property TAMANHO_DECIMAL() As Integer
        Get
            Return _tamanho_decimal
        End Get
        Set(ByVal Value As Integer)
            If Value >= 0 Then _tamanho_decimal = Value

        End Set
    End Property

    '## faixas de validade 
    Dim _val_min As Double = 0
    Dim _val_max As Double = 99999999999
    Public Property VALOR_MINIMO() As Double
        Get
            Return Me._val_min
        End Get
        Set(ByVal Value As Double)
            Me._val_min = Value
        End Set
    End Property
    Public Property VALOR_MAXIMO() As Double
        Get
            Return Me._val_max
        End Get
        Set(ByVal Value As Double)
            Me._val_max = Value
        End Set
    End Property


    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        Me.Style.Add("text-align", "right")

        If Me.Width.IsEmpty Or Me.Width.Value < Me.FORMATO.Length * 8 Then
            Me.Width = Web.UI.WebControls.Unit.Pixel(Me.FORMATO.Length * 8)
        End If
        'Me.Attributes.Add("size", Me.FORMATO.Length)
        Me.Attributes.Add("maxlength", Me.FORMATO.Length)

        Me.Attributes.Add("onkeypress", Me.ID & "_keydown()")
        If Not Me.AutoPostBack Then
            Me.Attributes.Add("onchange", Me.ID & "_valida(this);")
        End If
        MyBase.Render(writer)

        writer.Write("<script language=javascript>" & ControlChars.CrLf)
        writer.Write("function " & Me.ID & "_keydown() { var i = " & Me.TAMANHO_INTEIRO & "; var d = " & Me.TAMANHO_DECIMAL & ";" & ControlChars.CrLf)

        writer.Write("if ((event.keyCode != 44) && !((event.keyCode >= 48) && (event.keyCode <= 57)) )" & ControlChars.CrLf)
        writer.Write("{ event.returnValue = false; return }" & ControlChars.CrLf)
        writer.Write("var cont = event.srcElement.value;" & ControlChars.CrLf)
        writer.Write("if ((event.keyCode == 44) && ((d == 0) || (cont.indexOf("","") >= 0))) " & ControlChars.CrLf)
        writer.Write("{ event.returnValue = false; return }" & ControlChars.CrLf)
        writer.Write("}" & ControlChars.CrLf)
        writer.Write("</script>" & ControlChars.CrLf)

        writer.Write("<script language=JavaScript>" & ControlChars.CrLf)
        writer.Write("function " & Me.ID & "_valida(campo){ " & ControlChars.CrLf)
        writer.Write("var valido = true;" & ControlChars.CrLf)
        If Me.OBRIGATORIO Then
            writer.Write("if (campo.value == '') valido = false;" & ControlChars.CrLf)
        End If
        writer.Write("var re = /^[0-9]{0," & Me.TAMANHO_INTEIRO & "}" & IIf(Me.TAMANHO_DECIMAL > 0, ",{0,1}[0-9]{0," & Me.TAMANHO_DECIMAL & "}", "") & "$/;" & ControlChars.CrLf)
        writer.Write("valido = (valido && re.test(campo.value));" & ControlChars.CrLf)

        writer.Write("campo.style.borderColor = (valido ? '" & Me.BorderColor.Name & "' : 'Red');" & ControlChars.CrLf)
        writer.Write("}" & ControlChars.CrLf)
        writer.Write("</script>" & ControlChars.CrLf)



    End Sub





    Protected Overrides Function SaveViewState() As Object
        Dim o(5) As Object
        o(0) = MyBase.SaveViewState
        o(1) = Me._tamanho_inteiro
        o(2) = Me._tamanho_decimal
        o(3) = Me._val_min
        o(4) = Me._val_max


        Return o
    End Function

    Public Sub Control_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
        Me.Font.Bold = False
        Me.Font.Names = Split("Verdana,Arial,Helvetica,sans-serif", ",")
        Me.Font.Size = WebControls.FontUnit.Point(8)
        Me.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
        Me.BorderWidth = New Web.UI.WebControls.Unit(1)
        Me.BackColor = Drawing.Color.FromName("#DAEAF9")
        Me.BorderColor = Drawing.Color.FromName("#1887E6")
        Me.ForeColor = Drawing.Color.FromName("#004C8C")

        Me.Height = New Web.UI.WebControls.Unit(20)
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




    Protected Overrides Sub LoadViewState(ByVal savedState As Object)
        Dim o() As Object = savedState
        MyBase.LoadViewState(o(0))
        Me._tamanho_inteiro = o(1)
        Me._tamanho_decimal = o(2)
        Me._val_min = o(3)
        Me._val_max = o(4)

    End Sub

    Public Overridable Sub awa_preenche(ByVal val As DBNull)
        Me.Text = ""
    End Sub

    '## preenche o campo com o valor informado
    Public Overridable Sub awa_preenche(ByVal val As Double)
        Me.Text = String.Format("{0:" & Me.FORMATO & "}", val)
    End Sub


    '## preenche o campo com o valor informado
    Public Overridable Sub awa_preenche(ByVal val As String)
        If val = "" Or Not IsNumeric(val) Then
            Me.Text = ""
        Else
            Text = String.Format("{0:" & Me.FORMATO & "}", CDbl(val))
        End If
    End Sub


    '## armazena o valor default do objeto
    Dim _default As Double = 0
    Public Overridable Property PADRAO() As Double
        Get
            Return _default
        End Get
        Set(ByVal Value As Double)
            _default = Value
        End Set
    End Property

    '## retorna o valor do objeto
    Public Overridable ReadOnly Property VALOR() As Double
        Get
            If IsNumeric(Text) Then
                Return CDbl(Text)
            Else
                Return 0
            End If
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
        Dim mens As String = ""
        If Me._obrigatorio And Me.Text.Trim() = "" Then
            mens = Me._nome & " é obrigatório."
        End If

        Dim valor As Double
        If Not IsNumeric(Text) And Text <> "" Then
            mens = mens & Me._nome & ": número inválido." & Chr(13) & Chr(10)
        Else
            If Text = "" Then
                valor = 0
            Else
                valor = CDbl(Text)
            End If
            Dim posvg As Integer
            If Me.TAMANHO_DECIMAL > 0 Then
                posvg = Text.IndexOf(",")
                If posvg >= 0 Then
                    If (Text.Length - posvg - 1) > Me.TAMANHO_DECIMAL Then
                        mens = mens & Me._nome & ": máximo de " & Me.TAMANHO_DECIMAL & " após a vírgula." & "<BR>"
                    End If
                Else
                    posvg = Text.Length
                End If
            Else
                posvg = Text.Length
            End If
            If posvg > Me.TAMANHO_INTEIRO Then
                mens = mens & Me._nome & ": máximo de " & Me.TAMANHO_INTEIRO & " dígitos inteiros." & "<BR>"
            End If
            If Me._val_max >= Me._val_min Then
                If valor < Me._val_min Or valor > Me._val_max Then
                    mens = mens & Me._nome & " fora da faixa(" & Me._val_min & "-" & Me._val_max & ")"
                End If
            End If
        End If
        Return mens
    End Function

    Public Sub setfocus()
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), Me.ClientID & "focus", "<script language=javascript>document.all." & Me.ClientID & ".focus();</script>")
    End Sub


End Class
