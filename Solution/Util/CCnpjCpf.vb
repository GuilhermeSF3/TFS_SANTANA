''' <summary>
''' Classe para tratamento de identificadores de CNPJ para pessoa jur�dica ou CPF para pessoa f�sica
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
    ''' Retorna o n�mero completo a partir do par�metro informado.
    ''' </summary>
    ''' <param name="numero">
    ''' Sequ�ncia de caracteres com o n�mero do CNPJ/CPF. Em caso de pessoa jur�dica,
    ''' pode ser informado somente o radical. Nestes casos, a classe completa com 0001 e calcula o d�gito.
    ''' </param>
    ''' <param name="tipoPessoa">
    ''' CCnpjCpf.PESSOAJURIDICA ou "J" para pessoa jur�dica
    ''' CCnpjCpf.PESSOAFISICA ou "F" para pessoa f�sica
    ''' </param>
    ''' <returns>N�mero completo a partir do par�metro informado.</returns>
    Public Shared Function GetCnpjCpf(ByVal numero As String, ByVal tipoPessoa As String) As String
        If (tipoPessoa <> PESSOAJURIDICA And tipoPessoa <> PESSOAFISICA) Then
            Throw New Exception("Indicador inv�lido")
        End If

        If Not IsNumeric(numero) Then
            Throw New Exception("C�digo inv�lido!!!")
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
    ''' Retorna o d�gito de controle do CNPJ/CPF.
    ''' </summary>
    ''' <param name="codigo">
    ''' Sequ�ncia de caracteres com o n�mero do CNPJ/CPF. Em caso de pessoa jur�dica,
    ''' pode ser informado somente o radical. Nestes casos, a classe completa com 0001 e calcula o d�gito.
    ''' </param>
    ''' <param name="tipoPessoa">
    ''' CCnpjCpf.PESSOAJURIDICA ou "J" para pessoa jur�dica
    ''' CCnpjCpf.PESSOAFISICA ou "F" para pessoa f�sica
    ''' </param>
    ''' <returns>D�gito de controle do CNPJ/CPF.</returns>
    Public Shared Function CalcDigito(ByVal codigo As String, ByVal tipoPessoa As String) As String
        If (tipoPessoa <> PESSOAJURIDICA And tipoPessoa <> PESSOAFISICA) Then
            Throw New Exception("Indicador inv�lido")
        End If

        If Not IsNumeric(codigo) Then
            Throw New Exception("C�digo inv�lido!!!")
        End If

        ' Calculo do primeiro n�mero do CNPJ/CPF

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

        ' Calculo do segundo n�mero do CNPJ/CPF

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
    ''' Formata o n�mero no formato CNPJ/CPF.
    ''' </summary>
    ''' <param name="codigo">
    ''' Sequ�ncia de caracteres com o n�mero do CNPJ/CPF. Em caso de pessoa jur�dica,
    ''' pode ser informado somente o radical. Nestes casos, a classe completa com 0001 e calcula o d�gito.
    ''' </param>
    ''' <param name="tipoPessoa">
    ''' CCnpjCpf.PESSOAJURIDICA ou "J" para pessoa jur�dica
    ''' CCnpjCpf.PESSOAFISICA ou "F" para pessoa f�sica
    ''' </param>
    ''' <returns>N�mero no formato CNPJ/CPF.</returns>
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
    ''' Sequ�ncia de caracteres com o n�mero do CNPJ/CPF. Em caso de pessoa jur�dica,
    ''' pode ser informado somente o radical. Nestes casos, a classe completa com 0001 e calcula o d�gito.
    ''' </param>
    ''' <param name="tipoPessoa">
    ''' CCnpjCpf.PESSOAJURIDICA ou "J" para pessoa jur�dica
    ''' CCnpjCpf.PESSOAFISICA ou "F" para pessoa f�sica
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
    ''' Sequ�ncia de caracteres com o n�mero do CNPJ/CPF. Em caso de pessoa jur�dica,
    ''' pode ser informado somente o radical. Nestes casos, a classe completa com 0001 e calcula o d�gito.
    ''' </param>
    ''' <param name="tipoPessoa">
    ''' CCnpjCpf.PESSOAJURIDICA ou "J" para pessoa jur�dica
    ''' CCnpjCpf.PESSOAFISICA ou "F" para pessoa f�sica
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
    ''' Construtor baseado no n�mero e no identificador de CNPJ ou CPF
    ''' </summary>
    ''' <param name="numero">
    ''' Sequ�ncia de caracteres com o n�mero do CNPJ/CPF. Em caso de pessoa jur�dica,
    ''' pode ser informado somente o radical. Nestes casos, a classe completa com 0001 e calcula o d�gito.
    ''' </param>
    ''' <param name="tipoPessoa">
    ''' CCnpjCpf.PESSOAJURIDICA ou "J" para pessoa jur�dica
    ''' CCnpjCpf.PESSOAFISICA ou "F" para pessoa f�sica
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
    ''' Retorna a sequ�ncia de caracteres com o n�mero sem formata��o com 15 posi��es.
    ''' </summary>
    ''' <returns>Sequ�ncia de caracteres com o n�mero sem formata��o com 15 posi��es.</returns>
    Public ReadOnly Property CnpjCpf() As String
        Get
            Return mRadical & mComplemento & mDigito
        End Get
    End Property

    ''' <summary>
    ''' Retorna a sequ�ncia de caracteres com o n�mero sem formata��o com 15 posi��es para pessoa f�sica e
    ''' 9 posi��es para pessoa f�sica.
    ''' </summary>
    ''' <returns>Sequ�ncia de caracteres com o n�mero sem formata��o</returns>
    Public ReadOnly Property CnpjCpfRadical() As String
        Get
            Return IIf(mValido And mTipoPessoa = PESSOAJURIDICA, mRadical, CnpjCpf)
        End Get
    End Property

    ''' <summary>
    ''' Retorna a sequ�ncia de caracteres com o d�gito de controle com 2 posi��es.
    ''' </summary>
    ''' <returns>Sequ�ncia de caracteres com o d�gito de controle com 2 posi��es.</returns>
    Public ReadOnly Property Digito() As String
        Get
            Return mDigito
        End Get
    End Property

    ''' <summary>
    ''' Retorna o identificador de pessoa: J (pessoa jur�dica) ou F (pessoa f�sica)
    ''' </summary>
    ''' <returns>Identificador de pessoa: J (pessoa jur�dica) ou F (pessoa f�sica)</returns>
    Public ReadOnly Property TipoPessoa() As String
        Get
            Return mTipoPessoa
        End Get
    End Property

    ''' <summary>
    ''' Retorna o indicador de n�mero v�lido ou n�o v�lido.
    ''' </summary>
    ''' <returns>Verdadeiro indica CNPJ/CPF v�lido</returns>
    Public ReadOnly Property Valido() As Boolean
        Get
            Return mValido
        End Get
    End Property

    ''' <summary>
    ''' Retorna a sequ�ncia de caracteres com o n�mero completo formatado.
    ''' </summary>
    ''' <returns>Sequ�ncia de caracteres com o n�mero completo formatado.</returns>
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
    ''' Retorna a sequ�ncia de caracteres com o n�mero radical formatado para pessoa jur�dica e
    ''' o n�mero completo formatado para pessoa f�sica.
    ''' </summary>
    ''' <returns>Sequ�ncia de caracteres com o n�mero radical formatado</returns>
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