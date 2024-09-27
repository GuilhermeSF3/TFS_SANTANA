Public Class datahora
    Inherits maskedit

    '## faixas de validade 
    Dim _val_min As DateTime = New DateTime(1901, 1, 1)
    Dim _val_max As DateTime = DateTime.Now.AddYears(100)

    Public Property VALOR_MINIMO() As DateTime
        Get
            Return Me._val_min
        End Get
        Set(ByVal Value As DateTime)
            Me._val_min = Value
        End Set
    End Property
    Public Property VALOR_MAXIMO() As DateTime
        Get
            Return Me._val_max
        End Get
        Set(ByVal Value As DateTime)
            Me._val_max = Value
        End Set
    End Property


    Private Sub dathora_Render_Resto(ByVal writer As System.Web.UI.HtmlTextWriter) Handles MyBase.Render_Resto
        Me.Style.Add("text-align", "left")
        If Me.FORMATO = _formato.Ano Then
            Me.MaxLength = 4
        ElseIf Me.FORMATO = _formato.MesAno Then
            Me.MaxLength = 7
        ElseIf Me.FORMATO = _formato.DiaMesAno Then
            Me.MaxLength = 10
        End If

        Me.Attributes.Add("size", Me.MaxLength)

        If Not Me._calendario Then
            '## funcoes para validar data
            writer.Write("<script language=javascript>" & ControlChars.CrLf)
            writer.Write("function " & Me.ID & "_y2k(number) { return (number < 1000) ? number + 1900 : number; }" & ControlChars.CrLf)
            writer.Write("function " & Me.ID & "_isDate (day,month,year) " & ControlChars.CrLf)
            writer.Write("{" & ControlChars.CrLf)
            writer.Write("    var today = new Date();" & ControlChars.CrLf)
            writer.Write("    year = ((!year) ? " & Me.ID & "_y2k(today.getYear()):year);" & ControlChars.CrLf)
            writer.Write("    month = ((!month) ? today.getMonth():month-1);" & ControlChars.CrLf)
            writer.Write("    if (!day) return false" & ControlChars.CrLf)
            writer.Write("    var test = new Date(year,month,day);" & ControlChars.CrLf)
            writer.Write("    if ( (" & Me.ID & "_y2k(test.getYear()) == year) &&" & ControlChars.CrLf)
            writer.Write("         (month == test.getMonth()) &&" & ControlChars.CrLf)
            writer.Write("         (day == test.getDate()) )" & ControlChars.CrLf)
            writer.Write("        return true;" & ControlChars.CrLf)
            writer.Write("    else" & ControlChars.CrLf)
            writer.Write("        return false" & ControlChars.CrLf)
            writer.Write("}" & ControlChars.CrLf)

            writer.Write("function _validadata(campo)" & ControlChars.CrLf)
            writer.Write("{" & ControlChars.CrLf)
            writer.Write("  var formato = " & Me.FORMATO & ";" & ControlChars.CrLf)
            writer.Write("  var valido = true;" & ControlChars.CrLf)
            writer.Write("  if (formato == 0)  " & ControlChars.CrLf)
            writer.Write("     valido = !(campo.length != 10 || campo.substring(2,3) != '/' || campo.substring(5,6) != '/' );" & ControlChars.CrLf)
            writer.Write("  if (formato == 1)  " & ControlChars.CrLf)
            writer.Write("     valido = !(campo.length != 7 || campo.substring(2,3) != '/');" & ControlChars.CrLf)
            writer.Write("  if (formato == 2)  " & ControlChars.CrLf)
            writer.Write("     valido = !(campo.length != 4);" & ControlChars.CrLf)
            writer.Write("  if (valido) {" & ControlChars.CrLf)
            writer.Write("  var c1 = '', c2 = '', c3 = '';" & ControlChars.CrLf)
            writer.Write("  if (formato == 0)  {  c1 = campo.substring(0,2); c2 = campo.substring(3,5); c3 = campo.substring(6,10); }" & ControlChars.CrLf)
            writer.Write("  if (formato == 1)  {  c1 = '01'; c2 = campo.substring(0,2); c3 = campo.substring(3,7); }" & ControlChars.CrLf)
            writer.Write("  if (formato == 2)  {  c1 = '01'; c2 = '01'; c3 = campo; }" & ControlChars.CrLf)
            writer.Write("  if (!(" & Me.ID & "_isDate(c1, c2, c3)))" & ControlChars.CrLf)
            writer.Write("  { valido = false; }" & ControlChars.CrLf)
            writer.Write("  }" & ControlChars.CrLf)
            If Not Me.OBRIGATORIO Then
                writer.Write("  if (campo == '') {valido = true;}  " & ControlChars.CrLf)
            End If
            writer.Write("return valido;" & ControlChars.CrLf)
            writer.Write("}" & ControlChars.CrLf)
            writer.Write("</script>" & ControlChars.CrLf)
        End If

    End Sub

    Protected Overrides Function SaveViewState() As Object
        '        If Not Me.CAMPO_HORA Is Nothing Then
        '            Me.ViewState("CAMPO_HORA") = Me.CAMPO_HORA.ID
        '       End If

        Dim o(7) As Object
        o(0) = MyBase.SaveViewState
        o(1) = Me._default
        o(3) = Me._val_min
        o(4) = Me._val_max
        o(5) = Me._calendario
        o(6) = Me._mostra_calendario
        o(7) = Me.FORMATO
        Return o
    End Function

    '## formatos de validacao de data
    Dim vre(2) As String
    Dim vf(2) As String

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
        vre(datahora._formato.Ano) = "^[12]{1}[0-9]{3}$"
        vre(datahora._formato.MesAno) = "^[01]{1}[0-9]{1}\/[12]{1}[0-9]{3}$"
        vre(datahora._formato.DiaMesAno) = "^[0123]{1}[0-9]{1}\/[01]{1}[0-9]{1}\/[12]{1}[0-9]{3}$"
        vf(datahora._formato.Ano) = "####"
        vf(datahora._formato.MesAno) = "##/####"
        vf(datahora._formato.DiaMesAno) = "##/##/####"
        Me.FUNCAO_JS_VALIDA = "_validadata"
        Me.FORMATO = Me._forma
        Me.MASK_SIMPLES = False
    End Sub


    Protected Overrides Sub LoadViewState(ByVal savedState As Object)
        Dim o() As Object = savedState
        MyBase.LoadViewState(o(0))
        Me._default = o(1)
        Me._val_min = o(3)
        Me._val_max = o(4)
        Me._calendario = o(5)
        Me._mostra_calendario = o(6)
        Me.FORMATO = o(7)
        If Not Me.ViewState("CAMPO_HORA") Is Nothing Then
            '            Me.CAMPO_HORA = Me.Page.FindControl(Me.ViewState("CAMPO_HORA"))
        End If
    End Sub

    Public Overridable Sub awa_preenche(ByVal val As DBNull)
        Me.TEXTO_FORMATADO = ""
    End Sub

    '## preenche o campo com o valor informado
    Public Overridable Sub awa_preenche(ByVal val As DateTime)
        If val = Nothing Or val < DateAdd(DateInterval.Year, -200, DateTime.Now) Then
            Me.TEXTO_FORMATADO = ""
            '            If Not Me.CAMPO_HORA Is Nothing Then
            '            Me.CAMPO_HORA.awa_preenche(val)
            '        End If
        Return
        End If
        If Me.FORMATO = _formato.DiaMesAno Then
            Text = String.Format("{0:dd/MM/yyyy}", CDate(val))
        ElseIf Me.FORMATO = _formato.MesAno Then
            Text = String.Format("{0:MM/yyyy}", CDate(val))
        ElseIf Me.FORMATO = _formato.Ano Then
            Text = String.Format("{0:yyyy}", CDate(val))
        End If
        Me.TEXTO_FORMATADO = Text
        '        If Not Me.CAMPO_HORA Is Nothing Then
        '            Me.CAMPO_HORA.awa_preenche(val)
        '       End If
    End Sub

    '## preenche o campo com o valor informado
    Public Overridable Sub awa_preenche(ByVal val As String)
        If val Is Nothing Or val = Nothing Or val = "" Or Not IsDate(val) Then
            Me.TEXTO_FORMATADO = ""
            Return
        Else

            If Me.FORMATO = _formato.DiaMesAno Then
                Dim dt As DateTime = CDate(val)
                If val < DateAdd(DateInterval.Year, -200, DateTime.Now) Then
                    Text = ""
                    Return
                End If
                Text = String.Format("{0:dd/MM/yyyy}", CDate(val))
            ElseIf Me.FORMATO = _formato.MesAno Then
                Text = String.Format("{0:MM/yyyy}", CDate(val))
            ElseIf Me.FORMATO = _formato.Ano Then
                Text = String.Format("{0:yyyy}", CDate(val))
            End If
            Me.TEXTO_FORMATADO = Text
        End If

    End Sub
    Public ReadOnly Property EH_VAZIO() As Boolean
        Get
            Dim dt As DateTime = Nothing
            '            If Not Me.CAMPO_HORA Is Nothing Then
            '                dt = Me.CAMPO_HORA.VALOR
            '            End If

            If Me.VALOR = dt Then
                Return True
            End If
            Return False
        End Get
    End Property

    '## armazena o valor default do objeto
    Dim _default As DateTime = DateTime.Now.Date
    Public Overridable Property PADRAO() As DateTime
        Get
            Return _default
        End Get
        Set(ByVal Value As DateTime)
            _default = Value
        End Set
    End Property

    '## retorna o valor do objeto
    Public Overridable ReadOnly Property VALOR() As DateTime
        Get
            Dim dt As DateTime = Nothing
            '            If Not Me.CAMPO_HORA Is Nothing Then
            '                dt = Me.CAMPO_HORA.VALOR
            '           End If
            If Text.Length <> Me.FORMATACAO.Length Then Return dt

            Dim texto_comp As String
            If Me.FORMATO = _formato.Ano Then
                texto_comp = Me.Text & "-01-01"
            ElseIf Me.FORMATO = _formato.MesAno Then
                texto_comp = Me.Text.Substring(3, 4) & "-" & Me.Text.Substring(0, 2) & "-01"
            Else
                texto_comp = Text.Substring(6, 4) & "-" & Text.Substring(3, 2) & "-" & Text.Substring(0, 2)
            End If

            Dim ret As DateTime
            ret = IIf(IsDate(texto_comp) And Text <> "", texto_comp, Nothing)
            If Not dt = Nothing Then
                If ret = Nothing Then Return dt
                ret = New DateTime(ret.Year, ret.Month, ret.Day, dt.Hour, dt.Minute, dt.Second)
            End If
            Return ret
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
        mens = Me.VALIDA(Me._nome)

        If mens <> "" Then Return mens
        If Not Me.OBRIGATORIO And (Text = "" Or Me.Text = Me.TEXTO_VAZIO) Then Return ""
        If Text.Length > 0 And Text.Length <> Me.FORMATACAO.Length Then
            mens = Me._nome & " inválido (" & Me.FORMATO & ")"
            Return mens
        End If

        Dim texto_comp As String
        If Me.FORMATO = _formato.Ano Then
            texto_comp = Me.Text & "-01-01"
        ElseIf Me.FORMATO = _formato.MesAno Then
            texto_comp = Me.Text.Substring(3, 4) & "-" & Me.Text.Substring(0, 2) & "-01"
        Else
            texto_comp = Text.Substring(6, 4) & "-" & Text.Substring(3, 2) & "-" & Text.Substring(0, 2)
        End If

        Dim value As DateTime = IIf(IsDate(texto_comp), texto_comp, Nothing)
        If value = Nothing Then
            mens = Me._nome & " inválido (" & Me.FORMATO & ")"
        Else
            If Me._val_max >= Me._val_min Then
                If value < Me._val_min Or value > Me._val_max Then
                    mens = mens & Me._nome & " fora da faixa(" & Me._val_min & "-" & Me._val_max & ")"
                End If
            End If
        End If
        '        If Not Me.CAMPO_HORA Is Nothing Then
        '            mens = mens & Me.CAMPO_HORA.awa_valida()
        '        End If
        Return mens
    End Function

    Public Enum _formato
        DiaMesAno = 0
        MesAno = 1
        Ano = 2
    End Enum

    Dim _forma As _formato = _formato.DiaMesAno
    Public Property FORMATO() As _formato
        Get
            Return _forma
        End Get
        Set(ByVal Value As _formato)
            _forma = Value
            Me.FORMATACAO = Me.vf(Value)
            Me.EXPRESSAO_REGULAR = Me.vre(Value)
        End Set
    End Property

    Dim _calendario As Boolean = False
    Public Property CALENDARIO() As Boolean
        Get
            Return _calendario
        End Get
        Set(ByVal Value As Boolean)
            Me._calendario = Value
        End Set
    End Property

    Public _mostra_calendario As Boolean = False


    '   Public _obj_calendario As CALENDARIO

    Private Sub datahora_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        '       If Not Me._obj_calendario Is Nothing Then
        '       Me._obj_calendario.Style("DISPLAY") = IIf(Me._mostra_calendario, "inline", "none")
        '       Me._obj_calendario.Style("LEFT") = Me.Style("LEFT")
        '       Me._obj_calendario.Style("TOP") = (Web.UI.WebControls.Unit.Parse(Me.Style("TOP")).Value + 22) & "px"
        '       Me._obj_calendario.SelectedDate = IIf(Me.VALOR = Nothing, Date.Now, Me.VALOR)
        '      Me.Attributes("onclick") = "document.all." & Me._obj_calendario.ClientID & ".style.display = (document.all." & Me._obj_calendario.ClientID & ".style.display == 'inline' ? 'none' : 'inline');"
        '      Me.ReadOnly = True
        '      End If
    End Sub


    '    Dim _campo_hora As hora = Nothing
    '    Public Property CAMPO_HORA() As hora
    '        Get
    '            Return Me._campo_hora
    '        End Get
    '        Set(ByVal Value As hora)
    '            Me._campo_hora = Value
    '            Me._campo_hora.Style("LEFT") = (Web.UI.WebControls.Unit.Parse(Me.Style("LEFT")).Value + Me.Width.Value) & "px"
    '        End Set
    '    End Property
End Class

