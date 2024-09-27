''' <summary>
''' Classe para tratamento e formatação de data e hora
''' </summary>
Public Class CDataHora

    Private mData As DateTime = DateTime.Now
    Private Shared mMesesSigla As String() = New String() _
        {"Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"}
    Private Shared mMeses As String() = New String() _
        {"Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"}

    ''' <summary>
    ''' Retorna a a data informada no formato dd/mm/aaaa.
    ''' </summary>
    ''' <param name="dia"></param>
    ''' <param name="mes"></param>
    ''' <param name="ano"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FormataDataSimples(ByVal dia As Integer, ByVal mes As Integer, ByVal ano As Integer) As String
        Return FormataDataSimples(DateTime.Parse(dia & "/" & mes & "/" & ano, New System.Globalization.CultureInfo("pt-BR")))
    End Function

    Public Shared Function ParseDate(ByVal data as String) As Date
        Return DateTime.Parse(data, New System.Globalization.CultureInfo("pt-BR"))
    End Function



    ''' <summary>
    ''' Retorna a a data informada no formato dd/mm/aaaa.
    ''' </summary>
    ''' <param name="data">Objeto DateTime a ser formatado</param>
    ''' <returns>Sequência de caracteres que representa a data no formato dd/mm/aaaa.</returns>
    Public Shared Function FormataDataSimples(ByRef data As DateTime) As String
        If data = "#12:00:00 AM#" Then
            Return ""
        Else If data.ToString = "1/1/1900 00:00:00" Then
            Return ""
        Else
            Return String.Format("{0:dd/MM/yyyy}", data)
        End If
    End Function

    ''' <summary>
    ''' Retorna a sequência de caracteres que representa a data e a hora no formato dd/mm/aaaa hh:mm:ss.
    ''' </summary>
    ''' <param name="data">Objeto DateTime a ser formatado</param>
    ''' <returns>Sequência de caracteres que representa a data no formato dd/mm/aaaa hh:mm:ss.</returns>
    Public Shared Function FormataDataHoraSimples(ByRef data As DateTime) As String
        If data = "#12:00:00 AM#" Then
            Return ""
        Else
            Return String.Format("{0:dd/MM/yyyy HH:mm:ss}", data)
        End If
    End Function

    ''' <summary>
    ''' Retorna o nome do Mes informado em extenso.
    ''' </summary>
    ''' <param name="mes">Mes desejado em extenso</param>
    ''' <returns>Nome do Mes informado em extenso.</returns>
    Public Shared Function FormataNomeMes(ByVal mes As Integer) As String
        Return IIf(mes >= 1 And mes <= 12, mMeses(mes - 1), "")
    End Function

    ''' <summary>
    ''' Retorna a sigla do nome do Mes informado em extenso.
    ''' </summary>
    ''' <param name="mes">Mes desejado em extenso</param>
    ''' <returns>Nome do Mes informado em extenso.</returns>
    Public Shared Function FormataSiglaMes(ByVal mes As Integer) As String
        Select Case mes
            Case 1
                Return "Jan"
            Case 2
                Return "Fev"
            Case 3
                Return "Mar"
            Case 4
                Return "Abr"
            Case 5
                Return "Mai"
            Case 6
                Return "Jun"
            Case 7
                Return "Jul"
            Case 8
                Return "Ago"
            Case 9
                Return "Set"
            Case 10
                Return "Out"
            Case 11
                Return "Nov"
            Case 12
                Return "Dez"
            Case Else
                Return IIf(mes >= 1 And mes <= 12, mMesesSigla(mes - 1), "")
        End Select

    End Function

    ''' <summary>
    ''' Retorna a sequência de caracteres que data em extenso.
    ''' Exemplo: 05 de Janeiro de 2006.
    ''' </summary>
    ''' <param name="data">Objeto DateTime a ser formatado</param>
    ''' <returns>Sequência de caracteres que representa a data</returns>
    Public Shared Function FormataDataExtenso(ByRef data As DateTime) As String
        If data = "#12:00:00 AM#" Then
            Return ""
        Else
            Return String.Format("{0:dd}", data) & " de " & FormataNomeMes(data.Month) & " de " & String.Format("{0:yyyy}", data)
        End If
    End Function

    ''' <summary>
    ''' Retorna a data e hora representada pela sequência informada no formato dd/MM/aaaa hh:mm:ss
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Shared Function CriaDateTime(ByRef data As String) As DateTime
        Return DateTime.Parse(data, New System.Globalization.CultureInfo("pt-BR"))
    End Function

    ''' <summary>
    ''' Construtor padrão.
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Construtor a partir de um objeto DateTime.
    ''' </summary>
    ''' <param name="value">Objeto DateTime</param>
    Sub New(ByVal value As DateTime)
        Data = value
    End Sub

    ''' <summary>
    ''' Construtor a partir da quantidade de "ticks" informado.
    ''' </summary>
    ''' <param name="value">Quantidade de "ticks".</param>
    Sub New(ByVal value As Long)
        Data = New DateTime(value)
    End Sub

    ''' <summary>
    ''' Construtor a partir do ano, mes e dia informado.
    ''' </summary>
    ''' <param name="dia"></param>
    ''' <param name="mes"></param>
    ''' <param name="ano"></param>
    ''' <remarks></remarks>
    Sub New(ByVal dia As Integer, ByVal mes As Integer, ByVal ano As Integer)
        Me.New(dia & "/" & mes & "/" & ano)
    End Sub

    ''' <summary>
    ''' Construtor a partir da sequência de caracteres no formato "dd/mm/aaaa hh:mm:ss".
    ''' </summary>
    ''' <param name="value">Data e hora no formato "dd/mm/aaaa hh:mm:ss".</param>
    Sub New(ByVal value As String)
        Data = DateTime.Parse(value, New System.Globalization.CultureInfo("pt-BR"))
    End Sub

    ''' <summary>
    ''' Propriedade que contém o objeto DateTime da classe.
    ''' </summary>
    ''' <value>DateTime base do objeto.</value>
    ''' <returns>DateTime base do objeto.</returns>
    Public Property Data() As DateTime
        Get
            Return mData
        End Get
        Set(ByVal value As DateTime)
            mData = value
        End Set
    End Property

    ''' <summary>
    ''' Retorna a sequência de caracteres que representa a data no formato dd/mm/aaaa.
    ''' </summary>
    ''' <returns>Sequência de caracteres que representa a data no formato dd/mm/aaaa.</returns>
    Public ReadOnly Property DataSimples() As String
        Get
            Return FormataDataSimples(mData)
        End Get
    End Property

    ''' <summary>
    ''' Retorna a sequência de caracteres que representa a data e a hora no formato dd/mm/aaaa hh:mm:ss.
    ''' </summary>
    ''' <returns>Sequência de caracteres que representa a data e a hora no formato dd/mm/aaaa hh:mm:ss.</returns>
    Public ReadOnly Property DataHoraSimples() As String
        Get
            Return FormataDataHoraSimples(mData)
        End Get
    End Property

    ''' <summary>
    ''' Retorna a sequência de caracteres que data em extenso.
    ''' Exemplo: 05 de Janeiro de 2006.
    ''' </summary>
    ''' <returns>Sequência de caracteres que representa a data</returns>
    Public ReadOnly Property DataExtenso() As String
        Get
            Return FormataDataExtenso(mData)
        End Get
    End Property

    ''' <summary>
    ''' Retorna o nome do Mes em extenso.
    ''' </summary>
    ''' <returns>Mes em extenso.</returns>
    Public ReadOnly Property NomeMes() As String
        Get
            Return FormataNomeMes(mData.Month)
        End Get
    End Property

    ''' <summary>
    ''' Retorna o nome do Mes em sigla.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property NomeMesSigla() As String
        Get
            Return FormataSiglaMes(mData.Month)
        End Get
    End Property



End Class