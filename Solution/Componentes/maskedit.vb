<ComponentModel.Designer("Componentes.maskeditdesigner")> _
Public Class maskedit
    Inherits Web.UI.WebControls.TextBox
    Implements javascripts

    Private caracs() As Char = {"#", "0", "$", "@"}

    Dim _obrigatorio As Boolean = False
    Public Property OBRIGATORIO() As Boolean
        Get
            Return Me._obrigatorio
        End Get
        Set(ByVal Value As Boolean)
            Me._obrigatorio = Value
        End Set
    End Property

    Dim _formatacao As String
    Public Property FORMATACAO() As String
        Get
            Return IIf(Me._formatacao Is Nothing, "", Me._formatacao)
        End Get
        Set(ByVal Value As String)
            If Value Is Nothing Then Return
            Me._formatacao = Value
            Me.processa_formatacao(Value)
            Me.Width = Web.UI.WebControls.Unit.Pixel(Value.Length * 8)
            If Me.Text <> "" Then
                Me.separa_valor(Text, False)
            End If
        End Set
    End Property

    Dim _regexp As String = ""
    Public Property EXPRESSAO_REGULAR() As String
        Get
            Return _regexp
        End Get
        Set(ByVal Value As String)
            Me._regexp = Value
        End Set
    End Property

    Dim _funcjavascriptvalida As String = ""

    Public Property FUNCAO_JS_VALIDA() As String
        Get
            Return Me._funcjavascriptvalida
        End Get
        Set(ByVal Value As String)
            Me._funcjavascriptvalida = Value
        End Set
    End Property

    Dim _mask_simples As Boolean = True
    Public Property MASK_SIMPLES() As Boolean
        Get
            Return Me._mask_simples
        End Get
        Set(ByVal Value As Boolean)
            Me._mask_simples = Value
        End Set
    End Property

    Dim EH_VALIDO As Boolean = True  '## usado para verificar se a funcao eh valida
    Public ReadOnly Property VALIDO() As Boolean
        Get
            Return EH_VALIDO
        End Get
    End Property

    Private Sub processa_formatacao(ByVal f As String)
        If Me.masks Is Nothing Then
            Me.masks = New ArrayList()
            Me.seps = New ArrayList()
            Me.tamanho = New ArrayList()
        Else
            Me.masks.Clear()
            Me.seps.Clear()
            Me.tamanho.Clear()
        End If

        Dim i, p, t As Integer
        Dim car() As Char = {"#", "0", "$", "@"}
        i = f.IndexOfAny(car)
        If i = 0 Then
            Me.seps.Add("")
        ElseIf i < 0 Then
            Me.seps.Add(f)
            Me.seps.Add("")
            Me.masks.Add("")
            Return
        Else
            Me.seps.Add(f.Substring(0, i))
        End If

        While i < f.Length
            p = i
            t = 0
            While i < f.Length And f.IndexOfAny(caracs, i) = i
                t = t + IIf(f.Substring(i, 1) = "@" Or f.Substring(i, 1) = "$", 12, 8)
                i = i + 1
            End While
            Me.masks.Add(f.Substring(p, i - p))
            Me.tamanho.Add(t)
            If i < f.Length Then
                p = i
                While i < f.Length And Not f.IndexOfAny(caracs, i) = i
                    i = i + 1
                End While
                Me.seps.Add(f.Substring(p, i - p))
            End If
        End While
        If Array.IndexOf(caracs, f.Chars(f.Length - 1)) >= 0 Then
            Me.seps.Add("")
        End If
    End Sub

    Dim masks As New ArrayList()
    Dim seps As New ArrayList()
    Dim valor As New ArrayList()
    Dim tamanho As New ArrayList()

    Public Property TEXTO_FORMATADO() As String
        Get
            If Not Me.EH_VALIDO Then Return ""
            Return Me.Text
        End Get
        Set(ByVal Value As String)
            If Me.FORMATACAO = "" Then Return
            Me.Text = Value
            separa_valor(Value, True)
        End Set
    End Property

    Public Property TEXTO_NAO_FORMATADO() As String
        Get
            If Not Me.EH_VALIDO Then Return ""
            Return Join(Me.valor.ToArray, "")
        End Get
        Set(ByVal Value As String)
            If Me.FORMATACAO = "" Then Return
            separa_valor(Value, False)
            Me.Text = valor_formatado()
        End Set
    End Property

    Private Function valor_formatado() As String
        Dim ret As String = ""
        ret = seps(0)
        Dim i As Integer
        For i = 0 To valor.Count - 1
            ret = ret & valor(i) & seps(i + 1)
        Next
        Return ret
    End Function

    Private Sub separa_valor(ByVal val As String, ByVal formatado As Boolean)

        If valor Is Nothing Then
            valor = New ArrayList()
        Else
            valor.Clear()
        End If

        If Me.masks.Count = 0 Then
            valor.Add("")
            Return
        End If

        '## para cada mask, percorre até ela trocando os separadores de val por nada
        Dim cada As String = ""
        Dim som As String = Join(Me.masks.ToArray, "")
        Dim tammasks As Integer = som.Length
        Dim tamger As Integer = Me._formatacao.Length
        Dim i As Integer = 0
        Dim v As Integer = 0
        Dim m As Integer = 0

        If val.Length > tamger Then
            val = val.Substring(0, tamger)
        End If

        If Not formatado And val <> "" Then
            While i < val.Length And m < masks.Count And v < tammasks
                If eh_compativel(val.Substring(i, 1), som.Substring(v, 1)) Then
                    cada = cada & val.Substring(i, 1)
                    v = v + 1
                    If cada.Length = Me.masks(m).length Then
                        m = m + 1
                        Me.valor.Add(cada)
                        cada = ""
                    End If
                End If
                i = i + 1
            End While

            If cada <> "" And m < masks.Count Then
                Me.valor.Add(cada)
            End If
        ElseIf val <> "" Then '############### FORMATADO
            m = 1 '## pula a primeira formatacao
            i = seps(0).length

            Dim p As Integer

            While p >= 0 And i < val.Length And v < tamger And m < seps.Count - 1 '## pula a ultima formatacao
                p = val.IndexOf(seps(m), i)
                If p >= 0 Then
                    Me.valor.Add(val.Substring(i, p - i))
                    i = p + seps(m).length
                    m = m + 1
                End If
            End While

            If m = seps.Count - 1 Then '## se foi o ultimo mas ainda tem
                Me.valor.Add(val.Substring(i, val.Length - Me.seps(m).length - i))
            End If
        End If

        While valor.Count < masks.Count
            valor.Add("")
        End While
    End Sub

    Private Function eh_compativel(ByVal v As String, ByVal m As String) As Boolean
        If IsNumeric(v) And (m = "#" Or m = "0") Then
            Return True
        ElseIf v >= "0" And v <= "z" And (m = "@" Or m = "$") Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Overrides Sub LoadViewState(ByVal savedState As Object)
        MyBase.LoadViewState(savedState)
        Me._obrigatorio = Me.ViewState("OBRIGATORIO")
        Me._formatacao = Me.ViewState("FORMATACAO")
        Me.EH_VALIDO = Me.ViewState("EH_VALIDO")
        Me._funcjavascriptvalida = Me.ViewState("FUNCAO_JS_VALIDA")
        Me._regexp = Me.ViewState("REGEXP")

        If Not Me.ViewState("MASKS") Is Nothing Then
            Me.masks = Me.ViewState("MASKS")
            Me.seps = Me.ViewState("SEPS")
            Me.tamanho = Me.ViewState("TAMANHO")
            Me.MASK_SIMPLES = Me.ViewState("MASK_SIMPLES")
        End If
    End Sub

    Protected Overrides Function SaveViewState() As Object
        Me.ViewState("OBRIGATORIO") = Me.OBRIGATORIO
        Me.ViewState("FORMATACAO") = Me.FORMATACAO
        Me.ViewState("EH_VALIDO") = Me.EH_VALIDO
        Me.ViewState("FUNCAO_JS_VALIDA") = Me._funcjavascriptvalida
        Me.ViewState("REGEXP") = Me._regexp
        Me.ViewState("MASK_SIMPLES") = Me._mask_simples

        If Not Me.masks Is Nothing Then
            Me.ViewState("MASKS") = Me.masks
            Me.ViewState("SEPS") = Me.seps
            Me.ViewState("TAMANHO") = Me.tamanho
        End If
        Return MyBase.SaveViewState
    End Function

    Public render_control As Boolean = False

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        If Not Me.Enabled Or Me.render_control Or Me.ReadOnly Then
            MyBase.Render(writer)
            Return
        End If

        '## renderiza o campo com valor total e a chamada da funcao de expressao regular
        If Me.MASK_SIMPLES Then
            Dim cf As String = ""

            If Me.OBRIGATORIO Then
                cf = cf & "if (this.value == '') alert('campo obrigatório');"
            End If

            If Me.EXPRESSAO_REGULAR <> "" Then
                cf = cf & "if (!checa_regexp('/" & Me._regexp & "/', this.value)) alert('valor inválido') ;"
            End If

            If Me.FUNCAO_JS_VALIDA <> "" Then
                cf = cf & "if (!" & Me.FUNCAO_JS_VALIDA & "(this.value)) alert('verifique o valor digitado');"
            End If

            Me.Attributes.Add("onblur", cf)
            Me.render_control = True
            MyBase.Render(writer)

            Return
        End If


        Dim text As New IO.StringWriter
        Dim writer1 As New Web.UI.HtmlTextWriter(text)

        render_control = True
        MyBase.RenderControl(writer1)
        render_control = False

        Dim span As String = text.ToString()

        span = span.Replace(" id=""", " id=""SPAN")

        Dim seqfocusout As String = "if(document.activeElement == null || ("

        Dim i As Integer

        For i = 0 To masks.Count - 1
            seqfocusout = seqfocusout & "document.activeElement.id != '" & Me.ClientID & i & "' && "
        Next

        If Me.masks.Count > 0 Then
            If Me.AutoPostBack Then
                seqfocusout = seqfocusout & "document.activeElement.id != 'SPAN" & Me.ClientID & "')) if(" & Me.ClientID & "_valida()) " & Me.ClientID & ".fireEvent('onchange'); "
            Else
                seqfocusout = seqfocusout & "document.activeElement.id != 'SPAN" & Me.ClientID & "')) " & Me.ClientID & "_valida();"
            End If
        Else
            seqfocusout = ""
        End If

        '## incluir codigo para onpaste oncut oncopy
        span = span.Replace(" width:", " width1:")
        span = span.Replace(";width:", ";width1:")
        span = span.Replace("onchange", "oncopy")
        span = span.Replace("tabindex", "tabindex1")
        span = span.Replace("<input ", "<span onblur=""" & seqfocusout & """ ")
        span = span.Replace("style=""", "style=""background-color: " & Me.BackColor.Name & "; margin-top: -2px; margin-bottom: -2px;font-color: " & Me.ForeColor.Name & "; ")
        span = span.Replace("/>", ">")
        'writer.WriteLine(span)

        If Me.TabIndex = 0 Then
            Me.TabIndex = (Me.Page.FindControl("Form1").Controls.IndexOf(Page.FindControl(Me.ID)) + 1) * 10
        End If

        writer.WriteLine(seps(0))
        '## imprime cada campo na pagina com a chamada do script de validacao
        Dim zindex, altcampo As Integer
        Dim atuval, naoobrig As String

        zindex = Me.Style("Z-INDEX")
        altcampo = Web.UI.WebControls.Unit.Parse(Me.Style("HEIGHT")).Value
        altcampo = IIf(altcampo = 0, Me.Height.Value, altcampo)
        altcampo = IIf(altcampo = 0, 18, altcampo - 2)
        atuval = "document.all." & Me.ClientID & ".value = '" & Me.seps(0) & "'  "
        naoobrig = ""

        Dim margem As String = ""
        Dim sepsMenores As String = ".,;:|"

        For i = 0 To masks.Count - 1
            If sepsMenores.IndexOf(seps(i + 1)) >= 0 Then
                margem = "-1px"
            Else
                margem = "-3px"
            End If

            'writer.WriteLine("<input tabindex=""" & (Me.TabIndex + i - 1) & """ type=""text"" value=""" & valor(i) & """ maxlength=""" & masks(i).length & """ id=""" & Me.ClientID & i & """")
            writer.WriteLine("<input type=""text"" value=""" & valor(i) & """ maxlength=""" & masks(i).length & """ id=""" & Me.ClientID & i & """")

            If i = 0 Then
                writer.WriteLine("style=""font-size: " & Me.Font.Size.ToString & "; background-color: " & Me.BackColor.Name & ";margin-bottom: -1px; margin-top: -2px; margin-left:  0px; margin-right:  " & margem & ";")
            ElseIf i = masks.Count - 1 Then
                writer.WriteLine("style=""font-size: " & Me.Font.Size.ToString & "; background-color: " & Me.BackColor.Name & ";margin-bottom: -1px; margin-top: -2px; margin-left: " & margem & "; margin-right:  0px;")
            Else
                writer.WriteLine("style=""font-size: " & Me.Font.Size.ToString & "; background-color: " & Me.BackColor.Name & ";margin-bottom: -1px; margin-top: -2px; margin-left: " & margem & "; margin-right: " & margem & ";")
            End If

            writer.WriteLine("border-width:0px; height:" & altcampo & "px; Z-INDEX: " & zindex & "; width:" & tamanho(i) & "px;""")
            '## renderizar onkeydown onkeyup 
            writer.WriteLine("onkeypress=""" & Me.ClientID & i & "ant = " & Me.ClientID & "_keyup('" & Me.masks(i) & "'," & Me.ClientID & i & "ant," & IIf(i = Me.masks.Count - 1, "null", Me.ClientID & (i + 1)) & "," & IIf(i = 0, "null", Me.ClientID & (i - 1)) & ");""")
            writer.WriteLine("onkeydown=""" & Me.ClientID & i & "ant = " & Me.ClientID & "_keydown('" & Me.masks(i) & "'," & Me.ClientID & i & "ant," & IIf(i = Me.masks.Count - 1, "null", Me.ClientID & (i + 1)) & "," & IIf(i = 0, "null", Me.ClientID & (i - 1)) & ");""")
            'writer.WriteLine("onblur=""" & Me.ClientID & "_focusout();""")
            writer.WriteLine("onclick=""if (this.value.length == " & Me.masks(i).length & ") this.select();""")

            writer.WriteLine("/>")
            writer.WriteLine("<script language=javascript>var " & Me.ClientID & i & "ant = '" & valor(i) & "';</script>")
            writer.WriteLine(seps(i + 1))

            '## gera comando que atualiza o campo
            atuval = atuval & " + document.all." & Me.ClientID & i & ".value + '" & Me.seps(i + 1) & "' "
            naoobrig = naoobrig & " document.all." & Me.ClientID & i & ".value == '' && "
        Next

        Me.Width = Web.UI.WebControls.Unit.Pixel(0)
        Me.Height = Web.UI.WebControls.Unit.Pixel(0)
        Me.Attributes("onfocus") = Me.ClientID & "0.focus();"
        'Me.Attributes("tabindex") = "0"
        Me.TabIndex = 0
        render_control = True

        MyBase.RenderControl(writer)

        render_control = False

        atuval = atuval & ";"
        writer.WriteLine("<input type=""hidden"" value=""" & EH_VALIDO & """ id=""" & Me.ClientID & "valido"" name=""" & Me.ClientID & "valido""/>")

        If Me.masks.Count > 0 Then
            RaiseEvent Render_Resto(writer)
            naoobrig = "if (" & naoobrig.Substring(0, naoobrig.Length - 4) & ") valido = true;"
            writer.WriteLine("<script language=javascript>")

            writer.WriteLine("function " & Me.ClientID & "_keyup(m, va, pr, an) {")
            writer.WriteLine(" var vn = event.srcElement.value;")

            If Not Me._lb_enter Is Nothing Then
                writer.WriteLine("if ((pr == null) && (event.keyCode == 13)) {" & Me.ClientID & "_valida()  ; __doPostBack('" & Me._lb_enter.ClientID & "','');" & "; return va}")
            End If

            writer.WriteLine(" if ((pr != null) && (document.selection.type == 'None'))")
            writer.WriteLine(" if((vn.length == m.length && va.length < m.length) || (vn.length == m.length && va.length == m.length && vn.substring(m.length - 1, m.length) != va.substring(m.length - 1, m.length))) pr.focus(); ")

            writer.WriteLine(" return vn;}")

            writer.WriteLine("function " & Me.ClientID & "_focusout() {")
            writer.WriteLine(atuval)
            writer.WriteLine("}")

            writer.WriteLine("function " & Me.ClientID & "_keydown(m, va, pr, an) {")
            writer.WriteLine(" var vn = event.srcElement.value;")

            If Not Me._lb_enter Is Nothing Then
                writer.WriteLine("if ((pr == null) && (event.keyCode == 13)) {return va}")
            End If

            writer.WriteLine(" if (an != null && (vn.length == m.length || vn == '') && event.keyCode == 37 && document.selection.type == 'None') {")
            writer.WriteLine("  an.focus(); an.select(); return va}")
            writer.WriteLine(" if (pr != null && (vn.length == m.length  || vn == '') && event.keyCode == 39 && document.selection.type == 'None') {")
            writer.WriteLine("  pr.focus(); pr.select(); return va }")
            writer.WriteLine(" if (an != null && vn == '' && event.keyCode == 8) {")
            writer.WriteLine("  an.style.textAlign='RIGHT';  an.focus(); an.value = an.value + '';  an.style.textAlign='LEFT';")
            writer.WriteLine("  }")
            writer.WriteLine("  if (event.keyCode == 8 || event.keyCode == 46 || event.keyCode == 9) return va; ")
            writer.WriteLine("  if (m.indexOf('@') < 0 && m.indexOf('$') < 0 && !(event.keyCode >= 48 && event.keyCode <= 57) && !((event.keyCode >= 96) && (event.keyCode <= 105))) { event.returnValue = false; return va; }")
            writer.WriteLine(" return va;}")

            writer.WriteLine("function " & Me.ClientID & "_valida() {")
            writer.WriteLine(" " & Me.ClientID & "_focusout();")

            writer.WriteLine(" var vn = document.all." & Me.ClientID & ".value;")
            writer.WriteLine(" var valido = true;")

            If Me.OBRIGATORIO Then
                writer.WriteLine("if (vn == '') valido = false;")
            End If
            If Me.EXPRESSAO_REGULAR <> "" Then
                writer.WriteLine("var re = /" & Me._regexp & "/;")
                writer.WriteLine("valido = (valido && re.test(vn));")
            End If
            If Me.FUNCAO_JS_VALIDA <> "" Then
                writer.WriteLine("valido = valido && " & Me.FUNCAO_JS_VALIDA & "(vn);")
            End If
            If Not Me.OBRIGATORIO Then
                writer.WriteLine(naoobrig)
            End If

            writer.WriteLine("document.all.SPAN" & Me.ClientID & ".style.borderColor = (valido ? '" & Me.BorderColor.Name & "' : '" & Me.BorderColor.Name & "');")
            writer.WriteLine("document.all." & Me.ClientID & "valido.value = valido;")
            writer.WriteLine(" return valido}")
            writer.WriteLine(Me.ClientID & "_valida();")
            writer.WriteLine("</script>")
        End If

        'writer.WriteLine("</span>")
    End Sub

    Public Event Render_Resto(ByVal writer As System.Web.UI.HtmlTextWriter)

    Private Sub maskedit_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.Page.IsPostBack Then
            If Me.Page.Request.Form(Me.ClientID & "valido") <> "" Then
                Me.EH_VALIDO = Me.Page.Request.Form(Me.ClientID & "valido")
            End If
            Me.separa_valor(Text, True)
        End If
    End Sub

    Private Sub maskedit_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        If valor Is Nothing Then valor = New ArrayList
        While valor.Count < masks.Count
            valor.Add("")
        End While
    End Sub

    Public Overridable Function VALIDA(ByVal nome As String) As String
        If Me._obrigatorio And Me.TEXTO_NAO_FORMATADO = "" Then Return nome & " é obrigatório."
        If Not Me.EH_VALIDO Then Return nome & " inválido."
        Return ""
    End Function

    Public Sub setfocus()
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), Me.ClientID & "focus", "<script language=javascript>document.all." & Me.ClientID & "0.focus();</script>")
    End Sub

    Public Property ARQUIVOS() As String() Implements Componentes.javascripts.ARQUIVOS
        Get
            '## devolve todos os javascripts dos quais a Aplicacao depende
            Dim str(0) As String
            str(0) = caminho_js & "REGEXP.js"
            Return str
        End Get
        Set(ByVal Value As String())

        End Set
    End Property

    Public Shared caminho_js As String
    Public Property CAMINHO_JAVASCRIPTS() As String Implements Componentes.javascripts.CAMINHO_JAVASCRIPTS
        Get
            Return caminho_js
        End Get
        Set(ByVal Value As String)
            caminho_js = Value
        End Set
    End Property


    Public Shared ReadOnly Property RETORNA_SCRIPT_JS() As String
        Get
            Return "<script src=""" & caminho_js & "REGEXP.js""></script>"
        End Get
    End Property



    Public ReadOnly Property PARTE(ByVal i As Integer, Optional ByVal SEP_ANTERIOR As Boolean = False, Optional ByVal SEP_POSTERIOR As Boolean = False) As String
        Get
            If i < 0 Or i > Me.valor.Count - 1 Then Return ""
            Return IIf(SEP_ANTERIOR, Me.seps(i), "") & Me.valor(i) & IIf(SEP_POSTERIOR, Me.seps(i + 1), "")
        End Get
    End Property

    Dim _lb_enter As Web.UI.WebControls.LinkButton
    Dim _txt_enter As Web.UI.WebControls.TextBox

    Public Property LB_ENTER() As Web.UI.WebControls.LinkButton
        Get
            Return Me._lb_enter
        End Get
        Set(ByVal Value As Web.UI.WebControls.LinkButton)
            Me._lb_enter = Value
        End Set
    End Property

    Public ReadOnly Property TEXTO_VAZIO() As String
        Get
            Dim i As Integer
            Dim ret As String = Me.FORMATACAO
            For i = 0 To Me.caracs.Length - 1
                ret = ret.Replace(Me.caracs(i), "")
            Next
            Return ret
        End Get
    End Property
End Class



'#####################################################################
'#### DESIGNER DA TABELA
'#####################################################################

Public Class maskeditdesigner
    Inherits Web.UI.Design.ControlDesigner

    Public Sub New()

    End Sub

    Public Overrides Function GetDesignTimeHtml() As String
        Dim lt As maskedit = CType(Me.Component, maskedit)
        Dim pnl As New Web.UI.WebControls.TextBox
        pnl.Width = lt.Width
        pnl.Height = lt.Height
        pnl.BorderStyle = lt.BorderStyle
        pnl.BorderWidth = lt.BorderWidth
        pnl.Text = lt.FORMATACAO

        Dim text As New IO.StringWriter
        Dim writer As New Web.UI.HtmlTextWriter(text)
        pnl.RenderControl(writer)
        Return text.ToString
    End Function

    Public Overrides ReadOnly Property AllowResize() As Boolean
        Get
            Return True
        End Get
    End Property


End Class