Public Class datatableutil

    Private Const identa = "   "
    Private Const ordem = "000000"


    Private Shared Function cria_coluna_processo(ByRef dt As DataTable, ByVal pref As String, ByVal nome As String, ByVal tipo As Type) As String
        If Not dt.Columns.Contains("_AWA_" & pref & "_" & nome) Then
            Dim c As New DataColumn("_AWA_" & pref & "_" & nome, tipo)
            c.DefaultValue = IIf(tipo Is Type.GetType("System.string", False, True), "", 0)
            dt.Columns.Add(c)
        End If
        Return "_AWA_" & pref & "_" & nome
    End Function

    Private Shared Function retorna_nome_coluna(ByRef dt As DataTable, ByVal pref As String) As String
        Dim c As DataColumn
        For Each c In dt.Columns
            If c.ColumnName.StartsWith("_AWA_" & pref & "_") Then Return c.ColumnName
        Next
        Return ""
    End Function


    Public Shared Sub processa_recursao(ByVal coluna_codigo As String, ByVal coluna_codigo_pai As String, _
                                        ByVal coluna_nome As String, ByVal coluna_prefixo As String, _
                                        ByRef dti As DataTable)

        '## gera as colunas de recursao se nao existirem
        Dim col_ordem = datatableutil.cria_coluna_processo(dti, "ORDEM", coluna_codigo, Type.GetType("System.String"))
        Dim col_pai_sobe = datatableutil.cria_coluna_processo(dti, "PAI_SOBE", coluna_codigo_pai, Type.GetType("System.Int64"))
        Dim col_texto_id = datatableutil.cria_coluna_processo(dti, "TEXTO_IDENTADO", coluna_nome, Type.GetType("System.String"))
        Dim col_texto_rec = datatableutil.cria_coluna_processo(dti, "TEXTO_RECURSIVO", coluna_prefixo, Type.GetType("System.String"))
        Dim col_processado = datatableutil.cria_coluna_processo(dti, "PROCESSADO", coluna_codigo, Type.GetType("System.String"))

        '## atualiza-as todas para "" (nao processado)
        Dim r, filha, filhas() As DataRow
        Dim i As Integer = 0
        '## coloca coluna de nome em todas de texto recursivo
        For Each r In dti.Rows
            i = i + 1
            r(col_texto_rec) = r(coluna_nome)
            r(col_texto_id) = r(coluna_nome)
            r(col_pai_sobe) = r(coluna_codigo_pai)
            If r(col_pai_sobe) Is System.DBNull.Value Then
                r(col_pai_sobe) = 0
            End If

            '## se o pai nao estah na lista, anula-o
            If dti.Select(coluna_codigo & " = " & r(col_pai_sobe)).Length = 0 Then
                r(col_pai_sobe) = 0
            End If
            r(col_ordem) = Right(datatableutil.ordem & i, datatableutil.ordem.Length)
        Next

        Do
            '## processa cada linha
            For Each r In dti.Rows
                '## obtem todas as linhas filhas da atual
                filhas = dti.Select(col_pai_sobe & " = " & r(coluna_codigo) & " and " & col_pai_sobe & " <> " & coluna_codigo_pai)
                For Each filha In filhas
                    '## coloca o nome atual antes do nome de todas
                    filha(col_texto_rec) = r(coluna_nome) & " - " & filha(col_texto_rec)
                    '## identa com os espacos na coluna identada
                    filha(col_texto_id) = datatableutil.identa & filha(col_texto_id)
                    '## coloca a ordenacao conforme informado
                    filha(col_ordem) = r(col_ordem) & filha(col_ordem)
                    '## coloca o pai de todas como o pai da atual
                    filha(col_pai_sobe) = r(coluna_codigo_pai)
                Next

                r(col_processado) = IIf(r(col_processado) = "", IIf(filhas.Length > 0, "R", "F"), r(col_processado))
            Next
        Loop While dti.Select(col_pai_sobe & " > 0 and " & col_pai_sobe & " <> " & coluna_codigo_pai).Length > 0

        '## acrescenta a coluna de prefixo na coluna recursiva
        If coluna_prefixo <> "" Then
            For Each r In dti.Rows
                i = i + 1
                r(col_texto_rec) = r(coluna_prefixo) & " - " & r(col_texto_rec)
            Next
        End If

        '## seta a ordem padrao
        dti.DefaultView.Sort = col_ordem
    End Sub

    Public Shared Function so_folhas(ByVal dt As DataTable) As DataTable
        Dim nom_col_processado = datatableutil.retorna_nome_coluna(dt, "PROCESSADO")
        If nom_col_processado = "" Then Return dt
        Dim dr() As DataRow = dt.Select(nom_col_processado & " = 'F'")
        Dim r As DataRow
        Dim dtret As DataTable = dt.Clone()
        For Each r In dr
            dtret.ImportRow(r)
        Next
        Return dtret
    End Function

    Public Enum tipo_coluna
        TEXTO_IDENTADO = 0
        TEXTO_RECURSIVO = 1
        ORDEM = 2
    End Enum

    Public Shared Function coluna(ByVal dt As DataTable, ByVal c As tipo_coluna) As String
        If c = tipo_coluna.ORDEM Then
            Return datatableutil.retorna_nome_coluna(dt, "ORDEM")
        ElseIf c = tipo_coluna.TEXTO_IDENTADO Then
            Return datatableutil.retorna_nome_coluna(dt, "TEXTO_IDENTADO")
        ElseIf c = tipo_coluna.TEXTO_RECURSIVO Then
            Return datatableutil.retorna_nome_coluna(dt, "TEXTO_RECURSIVO")
        Else
            Return ""
        End If
    End Function

    Public Shared Function valores_coluna_separados(ByVal dt As DataTable, ByVal c As String, ByVal separador As String) As String
        Dim ar As String = ""
        If dt Is Nothing Then Return ar
        If Not dt.Columns.Contains(c) Then Return ar
        Dim r As DataRow
        For Each r In dt.Rows
            ar = ar & r(c).ToString & separador
        Next

        If ar <> "" Then ar = ar.Substring(0, ar.Length - separador.Length)
        Return ar
    End Function



    Public Shared Function valores_coluna(ByVal dt As DataTable, ByVal c As String) As Long()
        Dim d As Integer = 0

        Dim ar(d) As Long
        If dt Is Nothing Then Return ar
        If Not dt.Columns.Contains(c) Then Return ar
        If Not dt.Columns(c).DataType.ToString.StartsWith("System.Int") Then Return ar
        ReDim ar(dt.Rows.Count)
        Dim r As DataRow
        Dim i As Integer = 0
        For Each r In dt.Rows

            ar(i) = r(c)
            i = i + 1
        Next
        Return ar
    End Function


    Public Shared Function valores_coluna_str(ByVal dt As DataTable, ByVal c As String) As String()
        Dim d As Integer = 0

        Dim ar(d) As String
        If dt Is Nothing Then Return ar
        If Not dt.Columns.Contains(c) Then Return ar
        ReDim ar(dt.Rows.Count)
        Dim r As DataRow
        Dim i As Integer = 0
        For Each r In dt.Rows

            ar(i) = r(c).ToString
            i = i + 1
        Next
        Return ar
    End Function


    Public Shared Sub sort(ByRef dt As DataTable, ByVal criterio As String)
        Dim dt1 As DataTable = dt.Clone()
        Dim dr() As DataRow = dt.Select("", criterio)

        Dim r As DataRow

        For Each r In dr
            dt1.Rows.Add(r.ItemArray)
        Next

        dt = Nothing
        dt = dt1
    End Sub

    Public Shared Sub update(ByRef dt As DataTable, ByVal col As String, ByVal val As Object, ByVal col_proc As String, ByVal val_proc As Object)
        Dim dr() As DataRow = dt.Select(col_proc & "= '" & val_proc & "'")
        If dr.Length = 0 Then Return
        Dim r As DataRow
        For Each r In dr
            r(col) = val
        Next
    End Sub

    Public Shared Sub update_coluna(ByRef dt As DataTable, ByVal col As String, ByVal val As String)
        Dim r As DataRow
        For Each r In dt.Rows
            r(col) = r(val)
        Next

    End Sub


    Public Shared Sub delete(ByRef dt As DataTable, ByVal col_proc As String, ByVal val_proc As Object)
        Dim dr() As DataRow = dt.Select(col_proc & "= '" & val_proc & "'")
        If dr.Length = 0 Then Return
        Dim r As DataRow
        For Each r In dr
            dt.Rows.Remove(r)
        Next
    End Sub

    Public Shared Function count(ByRef dt As DataTable, ByVal criterio As String) As Long
        If dt Is Nothing Then Return 0
        If dt.Rows.Count = 0 Then Return 0

        Return dt.Select(criterio).Length
    End Function

    Public Shared Function copia_linha(ByVal r As DataRow, ByRef dt_origem As DataTable, ByRef dt_destino As DataTable) As Long
        Dim rnova As DataRow
        Dim i As Integer

        rnova = dt_destino.NewRow()
        For i = 0 To dt_destino.Columns.Count - 1
            If dt_origem.Columns.Contains(dt_destino.Columns(i).ColumnName) Then
                rnova(dt_destino.Columns(i).ColumnName) = r(dt_destino.Columns(i).ColumnName)
            End If
        Next
        dt_destino.Rows.Add(rnova)
        Return dt_destino.Rows.Count - 1

    End Function


    Public Shared Function copia(ByRef dt_origem As DataTable, ByRef dt_destino As DataTable, ByVal criterio As String) As Long
        Dim dr() As DataRow = dt_origem.Select(criterio)
        Dim r As DataRow

        For Each r In dr
            datatableutil.copia_linha(r, dt_origem, dt_destino)
        Next
        '## re-classifica o datatable destino
        If dt_destino.DefaultView.Sort <> "" Then
            datatableutil.sort(dt_destino, dt_destino.DefaultView.Sort)
        End If
        Return dr.Length

    End Function

    Public Shared Function move(ByRef dt_origem As DataTable, ByRef dt_destino As DataTable, ByVal criterio As String) As Long

        Dim qtd As Long = datatableutil.copia(dt_origem, dt_destino, criterio)
        If qtd = 0 Then Return 0

        Dim dr() As DataRow = dt_origem.Select(criterio)
        Dim i As Integer
        For i = dr.Length - 1 To 0 Step -1
            dt_origem.Rows.RemoveAt(i)
        Next

    End Function

    Public Shared Function subtrai(ByRef ORIGEM As DataTable, ByRef RETIRA_DA_ORIGEM As DataTable, ByVal keys() As String) As Long

        If keys.Length = 0 Then Return 0
        Dim r As DataRow
        Dim i, ii, total As Integer
        Dim esta As Boolean
        total = 0

        For Each r In RETIRA_DA_ORIGEM.Rows
            For ii = ORIGEM.Rows.Count - 1 To 0 Step -1
                esta = True
                For i = 0 To keys.Length - 1
                    If r(keys(i)) <> ORIGEM.Rows(ii)(keys(i)) Then esta = False
                Next
                If esta Then
                    ORIGEM.Rows.RemoveAt(ii)
                    total = total + 1
                End If
            Next
        Next

        Return total
    End Function

    Public Shared Function saveviewstate_datatable(ByVal dt As DataTable) As DataTable
        If dt Is Nothing Then Return dt
        If dt.Columns.Count = 0 Then Return dt

        '## coluna por coluna, cria adicionais de data
        Dim c, nc As DataColumn
        Dim i, cc As Integer
        cc = 0
        For i = dt.Columns.Count - 1 To 0 Step -1
            c = dt.Columns(i)
            If c.DataType.Name.ToUpper.IndexOf("DATE") >= 0 Then
                nc = New DataColumn("_AWA_DATE_" & c.ColumnName, _
                                    Type.GetType("System.String", False, True))
                dt.Columns.Add(nc)
                datatableutil.update_coluna(dt, nc.ColumnName, c.ColumnName)
                dt.Columns.Remove(c)
                cc = cc + 1
            End If
        Next
        Return dt
    End Function

    Public Shared Function loadviewstate_datatable(ByVal dt As DataTable) As DataTable
        If dt Is Nothing Then Return dt
        If dt.Columns.Count = 0 Then Return dt

        Dim c, nc As DataColumn
        Dim i, cc As Integer
        Dim nome As String
        cc = 0
        For i = dt.Columns.Count - 1 To 0 Step -1
            c = dt.Columns(i)
            If c.ColumnName.StartsWith("_AWA_DATE_") Then
                nome = c.ColumnName.Substring(10)
                nc = New DataColumn(nome, _
                                    Type.GetType("System.DateTime", False, True))
                dt.Columns.Add(nc)
                datatableutil.update_coluna(dt, nc.ColumnName, c.ColumnName)
                dt.Columns.Remove(c)
                cc = cc + 1
            End If
        Next
        Return dt
    End Function

    Public Shared Function awa_classificacao(ByVal dt As DataTable, ByVal dtc As DataTable) As DataTable
        Dim dtr As New DataTable()
        dtr = dt.Clone

        If dtr.Columns.Contains("AWA_CLASSIFICACAO") Then
            dtr.Columns.Remove("AWA_CLASSIFICACAO")
        End If
        dtr.Columns.Add("AWA_CLASSIFICACAO", Type.GetType("System.String"))
        dtr.Columns("AWA_CLASSIFICACAO").DefaultValue = ""

        Dim i As Long

        Dim r, dr() As DataRow
        For i = 0 To dtc.Rows.Count - 1
            dr = dt.Select(dtc.Columns(0).ColumnName & " = '" & dtc.Rows(i)(0) & "'")
            If Not dr Is Nothing Then
                If dr.Length = 1 Then
                    r = dtr.NewRow
                    r.ItemArray = dr(0).ItemArray
                    r("AWA_CLASSIFICACAO") = dtc.Rows(i)(1)
                    dtr.Rows.Add(r)
                End If
            End If
        Next
        Return dtr

    End Function
End Class
