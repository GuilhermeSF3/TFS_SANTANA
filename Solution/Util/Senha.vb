Imports System.Security.Cryptography
Imports System.text

Public Class Senha
    Public Shared Function GeraHash(ByVal texto As String) As String
        'Cria um objeto enconding para assegurar o padrão 
        'de encondig para o texto origem
        Dim Ue As New UnicodeEncoding
        'Retorna um byte array baseado no texto origem
        Dim ByteSourceText() As Byte = Ue.GetBytes(texto)
        'Instancia um objeto MD5
        Dim Md5 As New MD5CryptoServiceProvider
        'Calcula o valor do hash para o texto origem
        Dim ByteHash() As Byte = Md5.ComputeHash(ByteSourceText)
        'Converte o valor obtido para o formato string
        Return Convert.ToBase64String(ByteHash)
    End Function
End Class

