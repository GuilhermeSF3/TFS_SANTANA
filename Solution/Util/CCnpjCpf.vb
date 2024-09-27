''' <summary>
''' Classe para tratamento de identificadores de CNPJ para pessoa jurídica ou CPF para pessoa física
''' </summary>
Public Class CCnpjCpf

    Private mRadical As String
    Private mComplemento As String
    Private mDigito As String
    Private mTipoPessoa As String
    Private mValido As Boolean
    Public Const PESSOAFISICA As String = "F"
    Public Const PESSOAJURIDICA As String = "J"

    ''' <summary>
    ''' Retorna o número completo a partir do parâmetro informado.
    ''' </summary>
    ''' <param name="numero">
    ''' Sequência de caracteres com o número do CNPJ/CPF. Em caso de pessoa jurídica,
    ''' pode ser informado somente o radical. Nestes casos, a classe completa com 0001 e calcula o dígito.
    ''' </param>
    ''' <param name="tipoPessoa">
    ''' CCnpjCpf.PESSOAJURIDICA ou "J" para pessoa jurídica
    ''' CCnpjCpf.PESSOAFISICA ou "F" para pessoa física
    ''' </param>
    ''' <returns>Número completo a partir do parâmetro informado.</returns>
    Public Shared Function GetCnpjCpf(ByVal numero As String, ByVal tipoPessoa As String) As String
        If (tipoPessoa <> PESSOAJURIDICA And tipoPessoa <> PESSOAFISICA) Then
            Throw New Exception("Indicador inválido")
        End If

        If Not IsNumeric(numero) Then
            Throw New Exception("Código inválido!!!")
        End If

        If tipoPessoa = PESSOAJURIDICA And numero.Length <= 9 Then
            While numero.Length < 9
                numero = "0" & numero
            End While

            numero = numero & "0001"
            numero = numero & CalcDigito(numero, tipoPessoa)
        Else
            While numero.Length < 15
                numero = "0" & numero
            End While
        End If

        Return numero
    End Function

    ''' <summary>
    ''' Retorna o dígito de controle do CNPJ/CPF.
    ''' </summary>
    ''' <param name="codigo">
    ''' Sequência de caracteres com o número do CNPJ/CPF. Em caso de pessoa jurídica,
    ''' pode ser informado somente o radical. Nestes casos, a classe completa com 0001 e calcula o dígito.
    ''' </param>
    ''' <param name="tipoPessoa">
    ''' CCnpjCpf.PESSOAJURIDICA ou "J" para pessoa jurídica
    ''' CCnpjCpf.PESSOAFISICA ou "F" para pessoa física
    ''' </param>
    ''' <returns>Dígito de controle do CNPJ/CPF.</returns>
    Public Shared Function CalcDigito(ByVal codigo As String, ByVal tipoPessoa As String) As String
        If (tipoPessoa <> PESSOAJURIDICA And tipoPessoa <> PESSOAFISICA) Then
            Throw New Exception("Indicador inválido")
        End If

        If Not IsNumeric(codigo) Then
            Throw New Exception("Código inválido!!!")
        End If

        ' Calculo do primeiro número do CNPJ/CPF

        Dim i1 As Integer = 13
        Dim i2 As Integer = 1
        Dim soma As Integer = 0
        Dim digito1, digito2 As Integer

        While i1 > 1
            i1 -= 1
            i2 += 1

            If tipoPessoa = PESSOAJURIDICA And i2 > 9 Then
                i2 = 2
            End If

            soma += Integer.Parse(codigo.Substring(i1, 1)) * i2
        End While

        digito1 = IIf((soma Mod 11) = 0 Or (soma Mod 11) = 1, 0, 11 - (soma Mod 11))

        ' Calculo do segundo número do CNPJ/CPF

        i1 = 13
        i2 = 2
        soma = digito1 * 2

        While i1 > 1
            i1 -= 1
            i2 += 1

            If tipoPessoa = PESSOAJURIDICA And i2 > 9 Then
                i2 = 2
            End If

            soma += Integer.Parse(codigo.Substring(i1, 1)) * i2
        End While

        digito2 = IIf((soma Mod 11) = 0 Or (soma Mod 11) = 1, 0, 11 - (soma Mod 11))

        Return digito1.ToString & digito2.ToString
    End Function

    ''' <summary>
    ''' Formata o número no formato CNPJ/CPF.
    ''' </summary>
    ''' <param name="codigo">
    ''' Sequência de caracteres com o número do CNPJ/CPF. Em caso de pessoa jurídica,
    ''' pode ser informado somente o radical. Nestes casos, a classe completa com 0001 e calcula o dígito.
    ''' </param>
    ''' <param name="tipoPessoa">
    ''' CCnpjCpf.PESSOAJURIDICA ou "J" para pessoa jurídica
    ''' CCnpjCpf.PESSOAFISICA ou "F" para pessoa física
    ''' </param>
    ''' <returns>Número no formato CNPJ/CPF.</returns>
    Public Shared Function FormataCnpjCpf(ByVal codigo As String, ByVal tipoPessoa As String) As String
        codigo = GetCnpjCpf(codigo, tipoPessoa)

        If tipoPessoa = PESSOAJURIDICA Then
            Return codigo.Substring(0, 3) & "." & codigo.Substring(3, 3) & "." & codigo.Substring(6, 3) & "/" & codigo.Substring(9, 4) & "-" & codigo.Substring(13, 2)
        Else
            Return codigo.Substring(4, 3) & "." & codigo.Substring(7, 3) & "." & codigo.Substring(10, 3) & "-" & codigo.Substring(13, 2)
        End If
    End Function

    ''' <summary>
    ''' Formata o radical do CNPJ/CPF.
    ''' </summary>
    ''' <param name="codigo">
    ''' Sequência de caracteres com o número do CNPJ/CPF. Em caso de pessoa jurídica,
    ''' pode ser informado somente o radical. Nestes casos, a classe completa com 0001 e calcula o dígito.
    ''' </param>
    ''' <param name="tipoPessoa">
    ''' CCnpjCpf.PESSOAJURIDICA ou "J" para pessoa jurídica
    ''' CCnpjCpf.PESSOAFISICA ou "F" para pessoa física
    ''' </param>
    ''' <returns>Radical formatado do CNPJ/CPF</returns>
    Public Shared Function FormataRadicalCnpjCpf(ByVal codigo As String, ByVal tipoPessoa As String) As String
        codigo = GetCnpjCpf(codigo, tipoPessoa)

        If tipoPessoa = PESSOAJURIDICA Then
            Return codigo.Substring(0, 3) & "." & codigo.Substring(3, 3) & "." & codigo.Substring(6, 3)
        Else
            Return FormataCnpjCpf(codigo, tipoPessoa)
        End If
    End Function

    ''' <summary>
    ''' Formata o radical do CNPJ/CPF.
    ''' </summary>
    ''' <param name="codigo">
    ''' Sequência de caracteres com o número do CNPJ/CPF. Em caso de pessoa jurídica,
    ''' pode ser informado somente o radical. Nestes casos, a classe completa com 0001 e calcula o dígito.
    ''' </param>
    ''' <param name="tipoPessoa">
    ''' CCnpjCpf.PESSOAJURIDICA ou "J" para pessoa jurídica
    ''' CCnpjCpf.PESSOAFISICA ou "F" para pessoa física
    ''' </param>
    ''' <returns>Radical formatado do CNPJ/CPF</returns>
    Public Shared Function RadicalCnpjCpf(ByVal codigo As String, ByVal tipoPessoa As String) As String
        codigo = GetCnpjCpf(codigo, tipoPessoa)

        If tipoPessoa = PESSOAJURIDICA Then
            Return codigo.Substring(0, 9)
        Else
            Return codigo
        End If
    End Function

    ''' <summary>
    ''' Construtor baseado no número e no identificador de CNPJ ou CPF
    ''' </summary>
    ''' <param name="numero">
    ''' Sequência de caracteres com o número do CNPJ/CPF. Em caso de pessoa jurídica,
    ''' pode ser informado somente o radical. Nestes casos, a classe completa com 0001 e calcula o dígito.
    ''' </param>
    ''' <param name="tipoPessoa">
    ''' CCnpjCpf.PESSOAJURIDICA ou "J" para pessoa jurídica
    ''' CCnpjCpf.PESSOAFISICA ou "F" para pessoa física
    ''' </param>
    Sub New(ByVal numero As String, ByVal tipoPessoa As String)
        Dim digito As String

        numero = GetCnpjCpf(numero, tipoPessoa)
        digito = CalcDigito(numero, tipoPessoa)

        If tipoPessoa = PESSOAJURIDICA Then
            mRadical = numero.Substring(0, 9)
            mComplemento = numero.Substring(9, 4)
        Else
            mRadical = numero.Substring(0, 13)
            mComplemento = ""
        End If

        mDigito = numero.Substring(13, 2)
        mTipoPessoa = tipoPessoa
        mValido = mDigito = digito
    End Sub

    ''' <summary>
    ''' Retorna a sequência de caracteres com o número sem formatação com 15 posições.
    ''' </summary>
    ''' <returns>Sequência de caracteres com o número sem formatação com 15 posições.</returns>
    Public ReadOnly Property CnpjCpf() As String
        Get
            Return mRadical & mComplemento & mDigito
        End Get
    End Property

    ''' <summary>
    ''' Retorna a sequência de caracteres com o número sem formatação com 15 posições para pessoa física e
    ''' 9 posições para pessoa física.
    ''' </summary>
    ''' <returns>Sequência de caracteres com o número sem formatação</returns>
    Public ReadOnly Property CnpjCpfRadical() As String
        Get
            Return IIf(mValido And mTipoPessoa = PESSOAJURIDICA, mRadical, CnpjCpf)
        End Get
    End Property

    ''' <summary>
    ''' Retorna a sequência de caracteres com o dígito de controle com 2 posições.
    ''' </summary>
    ''' <returns>Sequência de caracteres com o dígito de controle com 2 posições.</returns>
    Public ReadOnly Property Digito() As String
        Get
            Return mDigito
        End Get
    End Property

    ''' <summary>
    ''' Retorna o identificador de pessoa: J (pessoa jurídica) ou F (pessoa física)
    ''' </summary>
    ''' <returns>Identificador de pessoa: J (pessoa jurídica) ou F (pessoa física)</returns>
    Public ReadOnly Property TipoPessoa() As String
        Get
            Return mTipoPessoa
        End Get
    End Property

    ''' <summary>
    ''' Retorna o indicador de número válido ou não válido.
    ''' </summary>
    ''' <returns>Verdadeiro indica CNPJ/CPF válido</returns>
    Public ReadOnly Property Valido() As Boolean
        Get
            Return mValido
        End Get
    End Property

    ''' <summary>
    ''' Retorna a sequência de caracteres com o número completo formatado.
    ''' </summary>
    ''' <returns>Sequência de caracteres com o número completo formatado.</returns>
    Public ReadOnly Property CnpjCpfFormatado() As String
        Get
            If Not Valido Then
                Return CnpjCpf
            Else
                Return FormataCnpjCpf(CnpjCpf, TipoPessoa)
            End If
        End Get
    End Property

    ''' <summary>
    ''' Retorna a sequência de caracteres com o número radical formatado para pessoa jurídica e
    ''' o número completo formatado para pessoa física.
    ''' </summary>
    ''' <returns>Sequência de caracteres com o número radical formatado</returns>
    Public ReadOnly Property CnpjCpfRadicalFormatado() As String
        Get
            If Not Valido Then
                Return CnpjCpfRadical
            Else
                Return FormataRadicalCnpjCpf(CnpjCpf, TipoPessoa)
            End If
        End Get
    End Property
End Class