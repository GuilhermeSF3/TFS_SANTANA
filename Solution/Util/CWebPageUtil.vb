Imports System.Web.UI.WebControls

Public Class CWebPageUtil

    ''' <summary>
    ''' Calcula o total de páginas de uma GridView informada e monta a lista de páginas
    ''' no DropDownList informado.
    ''' </summary>
    ''' <param name="gridView"></param>
    ''' <param name="pageList"></param>
    ''' <remarks></remarks>
    Public Shared Sub PreencheListaDePaginas(ByVal gridView As GridView, ByVal pageList As DropDownList)
        Dim pageNumber As Integer

        If Not IsNothing(gridView.DataSource) AndAlso CType(gridView.DataSource, System.Data.DataTable).Rows.Count > 0 Then
            If Not pageList Is Nothing Then
                ' Cria os valores para o DropDownList baseado no total de número de páginas
                For i As Integer = 0 To gridView.PageCount - 1
                    pageNumber = i + 1
                    Dim item As ListItem = New ListItem(pageNumber.ToString())

                    ' Pelo fato dos itens do DropDownList serem recriados sempre que o GridView for recriado, é
                    ' necessário indicar novamente o item selecionado.
                    If i = gridView.PageIndex Then
                        item.Selected = True
                    End If

                    ' Adiciona o ListItem no DropDownList.
                    pageList.Items.Add(item)
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' Formata uma desterminada "string" com o tamanho máximo indicado.
    ''' </summary>
    ''' <param name="valor"></param>
    ''' <param name="maximo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FormataTamanhoMaximo(ByVal valor As String, ByVal maximo As Integer) As String
        Dim retorno As String = ""

        If Not valor Is Nothing Then
            If valor.Length > maximo Then
                retorno = retorno & valor.Substring(0, maximo)
            Else
                retorno = retorno & valor
            End If
        End If

        Return retorno
    End Function

End Class
