Public Class cnpjcpf
    Inherits maskedit

    Private Const recnpj As String = "^[0-9]{2}.[0-9]{3}.[0-9]{3}\/[0-9]{4}-[0-9]{2}$"
    Private Const recpf As String = "^[0-9]{3}.[0-9]{3}.[0-9]{3}-[0-9]{2}$"

    Private _ehcnpj As Boolean = True
    Private _default As String = ""
    Private _nome As String

    ''' <summary>
    ''' Informa o tipo de registro.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EH_CNPJ() As Boolean
        Get
            Return Me._ehcnpj
        End Get

        Set(ByVal Value As Boolean)
            Me._ehcnpj = Value
            If Value Then
                Me.FORMATACAO = "##.###.###/####-##"
                Me.EXPRESSAO_REGULAR = cnpjcpf.recnpj
            Else
                Me.FORMATACAO = "###.###.###-##"
                Me.EXPRESSAO_REGULAR = cnpjcpf.recpf
            End If
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overrides Function SaveViewState() As Object
        Dim o(5) As Object
        o(0) = MyBase.SaveViewState
        o(2) = Me._ehcnpj
        o(3) = Me._nome
        o(4) = Me._default

        Return o
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub Control_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
        Me.MaxLength = IIf(Me._ehcnpj, 20, 14)
        Me.Attributes.Add("size", IIf(Me._ehcnpj, 20, 14))
        Me.EH_CNPJ = Me._ehcnpj
        Me.FUNCAO_JS_VALIDA = "DigitoCPFCNPJ"
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="savedState"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub LoadViewState(ByVal savedState As Object)
        Dim o() As Object = savedState
        MyBase.LoadViewState(o(0))
        Me._ehcnpj = o(2)
        Me._nome = o(3)
        Me._default = o(4)
    End Sub

    ''' <summary>
    ''' Preenche o campo com o valor informado.
    ''' </summary>
    ''' <param name="val"></param>
    ''' <remarks></remarks>
    Public Overridable Sub awa_preenche(ByVal val As String)
        If IsNumeric(val) Then
            Me.TEXTO_NAO_FORMATADO = Right("00000000000000" & val, IIf(Me.EH_CNPJ, 14, 11))
        Else
            Me.TEXTO_FORMATADO = val
        End If
    End Sub

    ''' <summary>
    ''' Armazena o valor default do objeto.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Property PADRAO() As String
        Get
            Return _default
        End Get
        Set(ByVal Value As String)
            _default = Value
        End Set
    End Property

    ''' <summary>
    ''' Retorna o valor do objeto.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable ReadOnly Property VALOR() As String
        Get
            Return Me.TEXTO_FORMATADO
        End Get
    End Property

    ''' <summary>
    ''' Nome usado em mensagens.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NOME() As String
        Get
            Return _nome
        End Get
        Set(ByVal Value As String)
            _nome = Value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Function awa_valida() As String
        Dim val As String = Me.VALIDA(Me.NOME)

        If val <> "" Then
            Return val
        ElseIf Not Me.OBRIGATORIO And Me.TEXTO_NAO_FORMATADO = "" Then
            Return ""
        ElseIf Not Me.ValidaCnpjCpf(IIf(Me.EH_CNPJ, "J", "F"), Me.TEXTO_NAO_FORMATADO) Then
            Return Me.NOME & ": dígito inválido. Confira."
        Else
            Return ""
        End If

    End Function

    ''' <summary>
    ''' Escreve as funcoes js de validacao de CNPJ e CPF.
    ''' </summary>
    ''' <param name="writer"></param>
    ''' <remarks></remarks>
    Private Sub CNPJCPF_Render_Resto(ByVal writer As System.Web.UI.HtmlTextWriter) Handles MyBase.Render_Resto
        writer.WriteLine("<script type=""text/javascript"">" & ControlChars.CrLf)

        writer.WriteLine("  function DigitoCPFCNPJ(numRegistro) {" & ControlChars.CrLf)

        writer.WriteLine("      var sonum = new String();" & ControlChars.CrLf)
        writer.WriteLine("      sonum = """";" & ControlChars.CrLf)

        writer.WriteLine("      for (var i = 0; i < numRegistro.length; i++) {" & ControlChars.CrLf)
        writer.WriteLine("          if ((numRegistro.substring(i,i+1) >= ""0"") && (numRegistro.substring(i,i+1) <= ""9"")) {" & ControlChars.CrLf)
        writer.WriteLine("              sonum = sonum + numRegistro.substring(i,i+1);" & ControlChars.CrLf)
        writer.WriteLine("          }" & ControlChars.CrLf)
        writer.WriteLine("      }" & ControlChars.CrLf)

        writer.WriteLine("      numRegistro = sonum;" & ControlChars.CrLf)

        If Not Me.OBRIGATORIO Then
            writer.Write("      if (numRegistro == '') { return true; }" & ControlChars.CrLf)
        End If

        writer.WriteLine("      var numDois = numRegistro.substring(numRegistro.length-2, numRegistro.length);" & ControlChars.CrLf)
        writer.WriteLine("      var novoRegistro = numRegistro.substring(0, numRegistro.length-2);" & ControlChars.CrLf)

        writer.WriteLine("      switch (numRegistro.length) {" & ControlChars.CrLf)
        writer.WriteLine("          case 11 :" & ControlChars.CrLf)
        writer.WriteLine("              numLim = 11;" & ControlChars.CrLf)

        If Me.EH_CNPJ Then
            writer.WriteLine("          return false;" & ControlChars.CrLf)
        End If

        writer.WriteLine("              break;" & ControlChars.CrLf)
        writer.WriteLine("          case 14 :" & ControlChars.CrLf)
        writer.WriteLine("              numLim = 9;" & ControlChars.CrLf)

        If Not Me.EH_CNPJ Then
            writer.WriteLine("          return false;" & ControlChars.CrLf)
        End If

        writer.WriteLine("              break;" & ControlChars.CrLf)
        writer.WriteLine("          default : " & ControlChars.CrLf)
        writer.WriteLine("              return false;" & ControlChars.CrLf)
        writer.WriteLine("      }" & ControlChars.CrLf)

        writer.WriteLine("      var res = new String();" & ControlChars.CrLf)
        writer.WriteLine("      res = numRegistro;" & ControlChars.CrLf)

        writer.WriteLine("      while ((res != """") && (res.substring(0,1) == res.substring(1,2))) {" & ControlChars.CrLf)
        writer.WriteLine("          res = res.substring(1,res.length);" & ControlChars.CrLf)

        writer.WriteLine("          if (res.length == 1) res = """";" & ControlChars.CrLf)
        writer.WriteLine("      }" & ControlChars.CrLf)

        writer.WriteLine("      if (res == """") { return false; }" & ControlChars.CrLf)

        writer.WriteLine("      var numSoma = 0;" & ControlChars.CrLf)
        writer.WriteLine("      var fator = 1;" & ControlChars.CrLf)

        writer.WriteLine("      for (var i=novoRegistro.length-1; i>=0 ; i--) {" & ControlChars.CrLf)
        writer.WriteLine("          fator = fator + 1;" & ControlChars.CrLf)

        writer.WriteLine("          if (fator > numLim) {" & ControlChars.CrLf)
        writer.WriteLine("              fator = 2;" & ControlChars.CrLf)
        writer.WriteLine("          }" & ControlChars.CrLf)

        writer.WriteLine("          numSoma = numSoma + (fator * Number(novoRegistro.substring(i, i+1)));" & ControlChars.CrLf)
        writer.WriteLine("      }" & ControlChars.CrLf)

        writer.WriteLine("      numSoma = numSoma / 11;" & ControlChars.CrLf)
        writer.WriteLine("      var numResto = Math.round(11 * (numSoma - Math.floor(numSoma)));" & ControlChars.CrLf)

        writer.WriteLine("      if (numResto > 1) {" & ControlChars.CrLf)
        writer.WriteLine("          numResto = 11 - numResto;" & ControlChars.CrLf)
        writer.WriteLine("      } else {" & ControlChars.CrLf)
        writer.WriteLine("          numResto = 0;" & ControlChars.CrLf)
        writer.WriteLine("      }" & ControlChars.CrLf)

        writer.WriteLine("      var numDigito = String(numResto);" & ControlChars.CrLf)
        writer.WriteLine("      novoRegistro = novoRegistro.concat(numResto);" & ControlChars.CrLf)
        writer.WriteLine("      numSoma = 0;" & ControlChars.CrLf)
        writer.WriteLine("      fator = 1;" & ControlChars.CrLf)

        writer.WriteLine("      for (var i=novoRegistro.length-1; i>=0 ; i--) {" & ControlChars.CrLf)
        writer.WriteLine("          fator = fator + 1;" & ControlChars.CrLf)

        writer.WriteLine("          if (fator > numLim) {" & ControlChars.CrLf)
        writer.WriteLine("              fator = 2;" & ControlChars.CrLf)
        writer.WriteLine("          }" & ControlChars.CrLf)

        writer.WriteLine("          numSoma = numSoma + (fator * Number(novoRegistro.substring(i, i+1)));" & ControlChars.CrLf)
        writer.WriteLine("      }" & ControlChars.CrLf)

        writer.WriteLine("      numSoma = numSoma / 11;" & ControlChars.CrLf)
        writer.WriteLine("      numResto = numResto = Math.round( 11 * (numSoma - Math.floor(numSoma)));" & ControlChars.CrLf)

        writer.WriteLine("      if (numResto > 1) {" & ControlChars.CrLf)
        writer.WriteLine("          numResto = 11 - numResto;" & ControlChars.CrLf)
        writer.WriteLine("      } else {" & ControlChars.CrLf)
        writer.WriteLine("          numResto = 0;" & ControlChars.CrLf)
        writer.WriteLine("      }" & ControlChars.CrLf)

        writer.WriteLine("      numDigito = numDigito.concat(numResto);" & ControlChars.CrLf)

        writer.WriteLine("      if (numDigito == numDois) {" & ControlChars.CrLf)
        writer.WriteLine("          return true;" & ControlChars.CrLf)
        writer.WriteLine("      } else {" & ControlChars.CrLf)
        writer.WriteLine("          return false;" & ControlChars.CrLf)
        writer.WriteLine("      }" & ControlChars.CrLf)
        writer.WriteLine("  }" & ControlChars.CrLf)
        writer.WriteLine("</script>" & ControlChars.CrLf)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pTipoDoc"></param>
    ''' <param name="pNumDoc"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidaCnpjCpf(ByVal pTipoDoc, ByVal pNumDoc) As Boolean
        '        =================
        'REM -- pTipoDoc: "J" ou "F"
        'Separa os dígitos do número

        Dim nNDig, nFrom, nTam, nAux, nCont, nCont1, nTo, nSum
        Dim aAux(0)

        nNDig = 2
        nFrom = 2
        nTam = Len(pNumDoc)

        REM -- Tipo do documento e nro de digitos

        If (pTipoDoc = "F") And (nTam = 11) Then
            nTo = 11
        ElseIf (pTipoDoc = "J") And (nTam = 14) Then
            nTo = 9
        Else
            Return False
        End If

        REM -- Não aceita todos os caracteres repetidos (nova definição SRF)

        If (Replace(pNumDoc, Mid(pNumDoc, 1, 1), "") = "") Then
            Return False
        End If

        nAux = nNDig
        nCont = 0

        Do While (nCont < nNDig)
            aAux(nCont) = Mid(pNumDoc, (nTam + 1) - nAux, 1)
            nAux = nAux - 1
            nCont = nCont + 1
            ReDim Preserve aAux(nCont)
        Loop

        pNumDoc = Trim(Mid(pNumDoc, 1, nTam - nNDig))
        nTam = Len(pNumDoc)
        nCont = 0

        Do While (nCont < nNDig)
            nCont1 = nFrom
            nSum = 0

            Do While (nTam > 0)
                nSum = nSum + nCont1 * (Int(Mid(pNumDoc, nTam, 1)))
                If (nCont1 = nTo) Then
                    nCont1 = nFrom
                Else
                    nCont1 = nCont1 + 1
                End If
                nTam = nTam - 1
            Loop

            nAux = nSum Mod 11

            If (nAux = 0) Or (nAux = 1) Then
                nAux = 0
            Else
                nAux = 11 - nAux
            End If

            If (aAux(nCont) <> CStr(nAux)) Then
                Return False
            Else
                pNumDoc = pNumDoc & aAux(nCont)
                nTam = Len(pNumDoc)
            End If

            nCont = nCont + 1
        Loop

        Return True
    End Function
End Class
