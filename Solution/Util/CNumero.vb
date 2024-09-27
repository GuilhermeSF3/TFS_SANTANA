''' <summary>
''' Classe para tratamento e formatação de valores numéricos
''' </summary>
Public Class CNumero

    Private mNumero As Double
    Private Shared Unidades As String() = New String() _
        {vbNullString, "um", "dois", "três", "quatro", "cinco", "seis", "sete", "oito", "nove", "dez", "onze", "doze", "treze", "quatorze", "quinze", "dezesseis", "dezessete", "dezoito", "dezenove"}
    Private Shared Dezenas As String() = New String() _
        {vbNullString, vbNullString, "vinte", "trinta", "quarenta", "cinqüenta", "sessenta", "setenta", "oitenta", "noventa"}
    Private Shared Centenas As String() = New String() _
        {vbNullString, "cento", "duzentos", "trezentos", "quatrocentos", "quinhentos", "seiscentos", "setecentos", "oitocentos", "novecentos"}
    Private Shared PotenciasSingular As String() = New String() _
        {vbNullString, "mil", "milhão", "bilhão", "trilhão", "quatrilhão"}
    Private Shared PotenciasPlural As String() = New String() _
        {vbNullString, "mil", "milhões", "bilhões", "trilhões", "quatrilhões"}

    ''' <summary>
    ''' Retorna a sequência de caracteres com o extenso do grupo do separador de milhares.
    ''' </summary>
    ''' <param name="valor">Valor do quadrante de até 3 dígitos</param>
    ''' <returns>Sequência de caracteres com o valor em extenso</returns>
    Private Shared Function Quadrante(ByVal valor As Integer) As String
        If valor > 999 Then
            Return vbNullString
        End If

        Dim strValor As String = valor.ToString

        While strValor.Length < 3
            strValor = "0" & strValor
        End While

        If valor > 0 And valor <= 19 Then
            Return Unidades(valor)
        ElseIf valor > 19 And valor < 100 Then
            Return Dezenas(Integer.Parse(strValor.Substring(1, 1))) & IIf(strValor.Substring(2, 1) = "0", "", " e " & Quadrante(Integer.Parse(strValor.Substring(2, 1))))
        ElseIf valor = 100 Then
            Return "cem"
        ElseIf valor > 100 And valor <= 999 Then
            Return Centenas(Integer.Parse(strValor.Substring(0, 1))) & IIf(strValor.Substring(1, 2) = "00", "", " e " & Quadrante(Integer.Parse(strValor.Substring(1, 2))))
        Else
            Return ""
        End If
    End Function

    ''' <summary>
    ''' Retorna a sequência de caracteres com o extenso do valor informado.
    ''' </summary>
    ''' <param name="Valor">Valor desejado em extenso</param>
    ''' <param name="MoedaPlural">Plural da descrição da moeda (opcional).</param>
    ''' <param name="MoedaSingular">Singular da descrição da moeda (opcional)</param>
    ''' <param name="CentavoPlural">Plural da descrição dos centavos moeda (opcional).</param>
    ''' <param name="CentavoSingular">Singular da descrição dos centavos moeda (opcional).</param>
    ''' <returns>Sequência de caracteres com o extenso do valor</returns>
    Public Shared Function FormataExtenso(ByVal Valor As Double, _
                            Optional ByVal MoedaPlural As String = "Reais", _
                            Optional ByVal MoedaSingular As String = "Real", _
                            Optional ByVal CentavoPlural As String = "Centavos", _
                            Optional ByVal CentavoSingular As String = "Centavo") As String

        Dim Negativo As Boolean = (Valor < 0)

        If Negativo Then
            Valor = Valor * -1
        End If

        Dim i, _quadrante, _centena As Integer
        Dim _extenso As String = ""
        Dim _inteiro, _decimal As Integer
        Dim mascara As String = "000000000000000000.00"

        Dim strInteiro As String = Left(Format(Valor, mascara), 18)
        Dim strDecimal As String = Right(Format(Valor, mascara), 2)

        For i = 0 To 17 Step 3
            _quadrante = IIf(i = 0, 1, IIf(i Mod 3 = 0, (i / 3) + 1, 0))
            _centena = Integer.Parse(strInteiro.Substring(i, 3))

            If _centena > 0 Then
                If _extenso.Length > 0 Then
                    _extenso = _extenso & ", "
                End If

                If _quadrante = 1 Then
                    _extenso = _extenso & Quadrante(_centena) & " " & IIf(_centena = 1, PotenciasSingular(5), PotenciasPlural(5))
                ElseIf _quadrante = 2 Then
                    _extenso = _extenso & Quadrante(_centena) & " " & IIf(_centena = 1, PotenciasSingular(4), PotenciasPlural(4))
                ElseIf _quadrante = 3 Then
                    _extenso = _extenso & Quadrante(_centena) & " " & IIf(_centena = 1, PotenciasSingular(3), PotenciasPlural(3))
                ElseIf _quadrante = 4 Then
                    _extenso = _extenso & Quadrante(_centena) & " " & IIf(_centena = 1, PotenciasSingular(2), PotenciasPlural(2))
                ElseIf _quadrante = 5 Then
                    _extenso = _extenso & Quadrante(_centena) & " " & IIf(_centena = 1, PotenciasSingular(1), PotenciasPlural(1))
                ElseIf _quadrante = 6 Then
                    _extenso = _extenso & Quadrante(_centena)
                End If
            End If
        Next

        _inteiro = Integer.Parse(strInteiro)
        _decimal = Integer.Parse(strDecimal)

        If _inteiro > 0 Then
            _extenso = _extenso & " " & IIf(_inteiro = 1, MoedaSingular, MoedaPlural)
        End If

        If _decimal > 0 Then
            _extenso = _extenso & IIf(_inteiro > 0, " e ", "") & Quadrante(_decimal) & " " & IIf(_decimal = 1, CentavoSingular, CentavoPlural)
        End If

        If _extenso.Length = 0 Then
            Return "Zero"
        ElseIf _extenso.Substring(0, 2) = "um" Then
            Return "H" & _extenso
        Else
            Return UCase(_extenso.Substring(0, 1)) & LCase(_extenso.Substring(1))
        End If
    End Function

    ''' <summary>
    ''' Retorna a sequência de caracteres com o número formatado
    ''' </summary>
    ''' <param name="Valor">Valor a ser formatado.</param>
    ''' <param name="decimais">Quantidade de casas decimais desejado.</param>
    ''' <returns>Valor formatado</returns>
    Public Shared Function FormataNumero(ByVal valor As Double, ByVal decimais As Integer, Optional ByVal percentual As Boolean = False) As String
        Dim i As Integer
        Dim mascara As String = "#,##0"

        If decimais > 0 Then
            mascara = mascara & "."

            For i = 1 To decimais
                mascara = mascara & "0"
            Next
        End If

        Return valor.ToString(mascara, New System.Globalization.CultureInfo("pt-BR")) + IIf(percentual = True, "%", "")
    End Function

    Public Shared Function MesGrid(ByVal nMes As Integer, ByVal nAno As Integer) As String

        Dim matriz(12) As String

        matriz(1) = "Jan/" & CType(nAno, String)
        matriz(2) = "Fev/" & CType(nAno, String)
        matriz(3) = "Mar/" & CType(nAno, String)
        matriz(4) = "Abr/" & CType(nAno, String)
        matriz(5) = "Mai/" & CType(nAno, String)
        matriz(6) = "Jun/" & CType(nAno, String)
        matriz(7) = "Jul/" & CType(nAno, String)
        matriz(8) = "Ago/" & CType(nAno, String)
        matriz(9) = "Set/" & CType(nAno, String)
        matriz(10) = "Out/" & CType(nAno, String)
        matriz(11) = "Nov/" & CType(nAno, String)
        matriz(12) = "Dez/" & CType(nAno, String)

        MesGrid = matriz(nMes)

        Return MesGrid
    End Function

    ''' <summary>
    ''' Retorna a sequência de caracteres com o número para digitação
    ''' </summary>
    ''' <param name="Valor">Valor a ser formatado.</param>
    ''' <param name="decimais">Quantidade de casas decimais desejado.</param>
    ''' <returns>Valor formatado</returns>
    Public Shared Function FormataNumeroDigitacao(ByVal valor As Double, ByVal decimais As Integer) As String
        Dim i As Integer
        Dim mascara As String = "###0"

        If decimais > 0 Then
            mascara = mascara & "."

            For i = 1 To decimais
                mascara = mascara & "0"
            Next
        End If

        Return valor.ToString(mascara, New System.Globalization.CultureInfo("en-US"))
    End Function

    ''' <summary>
    ''' Retorna a sequência de caracteres com o número contábil formatado
    ''' </summary>
    ''' <param name="Valor">Valor a ser formatado.</param>
    ''' <param name="decimais">Quantidade de casas decimais desejado.</param>
    ''' <returns>Valor formatado</returns>
    Public Shared Function FormataNumeroContabil(ByVal valor As Double, ByVal decimais As Integer) As String
        Dim i As Integer
        Dim mascara As String = "#,##0"

        If decimais > 0 Then
            mascara = mascara & "."

            For i = 1 To decimais
                mascara = mascara & "0"
            Next
        End If

        mascara = mascara & ";(" & mascara & ")"
        Return valor.ToString(mascara, New System.Globalization.CultureInfo("pt-BR"))
    End Function

    ''' <summary>
    ''' Retorna o double representado pela sequência informada no formato 0.00
    ''' </summary>
    ''' <param name="valor"></param>
    ''' <returns></returns>
    Public Shared Function CriaDouble(ByRef valor As String) As Double
        Return Double.Parse(valor, New System.Globalization.CultureInfo("pt-BR"))
    End Function

    Sub New()
    End Sub

    Sub New(ByVal value As Double)
        Numero = value
    End Sub

    Sub New(ByVal value As String)
        Numero = Double.Parse(value)
    End Sub

    ''' <value>Número a ser representado.</value>
    ''' <returns>Número representado.</returns>
    Public Property Numero() As Double
        Get
            Return mNumero
        End Get
        Set(ByVal value As Double)
            mNumero = value
        End Set
    End Property

    ''' <returns>Sequência de caracteres com o número formatado com as casas duas casas decimais.</returns>
    Public ReadOnly Property NumeroFormatado() As String
        Get
            Return FormataNumero(mNumero, 2)
        End Get
    End Property

    ''' <param name="decimais">Número de casas decimais desejado</param>
    ''' <returns>Sequência de caracteres com o número formatado com as casas decimais informada.</returns>
    Public ReadOnly Property NumeroFormatado(ByVal decimais As Integer) As String
        Get
            Return FormataNumero(mNumero, decimais)
        End Get
    End Property

    ''' <param name="MoedaPlural">Plural da descrição da moeda (opcional).</param>
    ''' <param name="MoedaSingular">Singular da descrição da moeda (opcional)</param>
    ''' <param name="CentavoPlural">Plural da descrição dos centavos moeda (opcional).</param>
    ''' <param name="CentavoSingular">Singular da descrição dos centavos moeda (opcional).</param>
    ''' <returns>Sequência de caracteres com o extenso do valor</returns>
    Public ReadOnly Property NumeroExtenso( _
                            Optional ByVal MoedaPlural As String = "Reais", _
                            Optional ByVal MoedaSingular As String = "Real", _
                            Optional ByVal CentavoPlural As String = "Centavos", _
                            Optional ByVal CentavoSingular As String = "Centavo") As String
        Get
            Return FormataExtenso(mNumero, MoedaPlural, MoedaSingular, CentavoPlural, CentavoSingular)
        End Get
    End Property
End Class