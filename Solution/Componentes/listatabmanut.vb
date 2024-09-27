Public Class listatabmanut
    Inherits listatab

    '## lista que controla operacoes de Inc, Alt e Exc dentro do
    '## datasource, sem alterar no BD e marcando as linhas que foram
    '## incluidas, alteradas e excluidas para posterior operacao no BD

    Dim dt_excluidas As DataTable
    '## copia a estrutura do datatable setado para exclusao e adiciona coluna de
    '## controle

    Protected ReadOnly nom_coluna_controle = "_AWA_operacao"
    Protected ReadOnly nom_coluna_num = "_AWA_operacao_num"

    Private Sub listatabmanut_datasource_setado() Handles MyBase.datasource_setado
        Dim acres As Boolean = False

        If Not Me.datasource.Columns.Contains(nom_coluna_controle) Then
            Dim c As New DataColumn(nom_coluna_controle, Type.GetType("system.string", False, True))
            c.DefaultValue = ""
            Me.datasource.Columns.Add(c)
            acres = True
        End If
        If Not Me.datasource.Columns.Contains(nom_coluna_num) Then
            Dim c As New DataColumn(nom_coluna_num, Type.GetType("system.string", False, True))
            c.DefaultValue = 0
            Me.datasource.Columns.Add(c)
            Dim i As Long
            For i = 0 To Me.datasource.Rows.Count - 1
                Me.datasource.Rows(i)(nom_coluna_num) = i + 1
            Next
            If Me.datasource.Columns.Count > 0 And acres Then
                Me.dt_excluidas = Me.datasource.Clone()
            End If
        End If
    End Sub

    '## inicia a nova linha e seta operacao de inclusao
    Public Overrides Sub inicia_linha()
        MyBase.inicia_linha()
        Me.dr_nova(nom_coluna_controle) = "I"

        '## numera a coluna de controle
        If Me.datasource.Rows.Count > 0 Then
            Me.dr_nova(nom_coluna_num) = Me.datasource.Compute("max(" & nom_coluna_num & ")", "") + 1
        Else
            Me.dr_nova(nom_coluna_num) = 1
        End If
    End Sub

    '## conforme o caso, seta a operacao de alteracao
    Public Overrides Sub salva(ByVal col As String, ByVal val As Object)
        Dim linha = Me.linha_sel()
        If linha < 0 Then Return
        If Not Me.datasource.Columns.Contains(col) Then Return
        If Not Me.datasource.Rows(linha).Item(col) Is System.DBNull.Value Then
            If Me.datasource.Rows(linha).Item(col).ToString = val.ToString Then Return
        End If

        If Me.datasource.Rows(linha).Item(nom_coluna_controle) <> "I" Then
            Me.datasource.Rows(linha).Item(nom_coluna_controle) = "A"
        End If
        MyBase.salva(col, val)
    End Sub

    '## trata a exclusao de linhas do datasource
    Public Overrides Sub exclui_atual()
        Dim linha = Me.linha_sel()
        If linha < 0 Then Return
        '## soh na inclusao nao mantem a linha
        If Me.datasource.Rows(linha).Item(nom_coluna_controle) <> "I" Then
            Me.dt_excluidas.ImportRow(Me.datasource.Rows(linha))
        End If
        MyBase.exclui_atual()

    End Sub

    Public Function linha_para_alterar(ByVal num_dt As Long) As DataRow
        If num_dt >= Me.datasource.Rows.Count Then Return Nothing
        If Me.datasource.Rows(num_dt).Item(nom_coluna_controle) <> "I" Then
            Me.datasource.Rows(num_dt).Item(nom_coluna_controle) = "A"
        End If
        Return Me.datasource.Rows(num_dt)
    End Function

    Public Sub salva_por_numero(ByVal num As Long, ByVal col As String, ByVal val As Object)
        Dim dr() As DataRow = Me.datasource.Select(nom_coluna_num & " = " & CStr(num))
        If dr.Length <> 1 Then Return
        If Not Me.datasource.Columns.Contains(col) Then Return
        If Not dr(0).Item(col) Is System.DBNull.Value Then
            If dr(0).Item(col).ToString = val.ToString Then Return
        End If

        If dr(0).Item(nom_coluna_controle) <> "I" Then
            dr(0).Item(nom_coluna_controle) = "A"
        End If
        dr(0)(col) = val
    End Sub


    Public Sub exclui_por_numero(ByVal num As Long)
        Dim dr() As DataRow = Me.datasource.Select(nom_coluna_num & " = " & CStr(num))
        If dr.Length = 1 Then
            If dr(0)(nom_coluna_controle) <> "I" Then
                Me.dt_excluidas.ImportRow(dr(0))
            End If
            Me.datasource.Rows.Remove(dr(0))
        End If
    End Sub

    Public Function retorna_linha_por_numero(ByVal num As Long) As DataRow
        Dim dr() As DataRow = Me.datasource.Select(nom_coluna_num & " = '" & CStr(num) & "'")
        If dr.Length = 1 Then
            If dr(0).Item(nom_coluna_controle) <> "I" Then
                dr(0).Item(nom_coluna_controle) = "A"
            End If
            Return dr(0)
        End If
        Return Nothing

    End Function

    Public Function update(ByVal col As String, ByVal val As Object, ByVal col_proc As String, ByVal val_proc As Object) As Boolean
        datatableutil.update(Me.datasource, col, val, col_proc, val_proc)
    End Function


    Public Function sort(ByVal criterio As String) As Boolean
        Dim rorig As DataRow = Me.obtem_linha()
        Dim col As New Collections.Specialized.NameValueCollection()

        If Not rorig Is Nothing Then
            '## monta sequencia para busca
            Dim i As Integer
            For i = 0 To Me.datasource.Columns.Count - 1
                col.Add(Me.datasource.Columns(i).ColumnName, rorig(i).ToString)
            Next
        End If

        datatableutil.sort(Me.datasource, criterio)

        If Not rorig Is Nothing Then
            Me.procura(col, True)
        End If

        Return True
    End Function

    Public Function retorna_excluidas() As DataRow()
        If Me.dt_excluidas Is Nothing Then Return Nothing
        Return Me.dt_excluidas.Select("")
    End Function

    Public Function retorna_incluidas_alteradas() As DataRow()
        Return Me.datasource.Select(nom_coluna_controle & " in('I','A')", nom_coluna_controle & " desc")
    End Function

    Public Function retorna_incluidas() As DataRow()
        Return Me.datasource.Select(nom_coluna_controle & " = 'I'", nom_coluna_controle & " desc")
    End Function

    Public Function retorna_alteradas() As DataRow()
        Return Me.datasource.Select(nom_coluna_controle & " = 'A'", nom_coluna_controle & " desc")
    End Function

    Protected Overrides Function SaveViewState() As Object
        If Not Me.dt_excluidas Is Nothing Then
            Me.ViewState("dt_excluidas") = datatableutil.saveviewstate_datatable(Me.dt_excluidas)
        End If
        Return MyBase.SaveViewState()
    End Function

    Protected Overrides Sub LoadViewState(ByVal savedState As Object)
        MyBase.LoadViewState(savedState)
        If Not Me.ViewState("dt_excluidas") Is Nothing Then Me.dt_excluidas = datatableutil.loadviewstate_datatable(Me.ViewState("dt_excluidas"))
    End Sub



End Class
