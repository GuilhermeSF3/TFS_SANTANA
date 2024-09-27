Imports Microsoft.VisualBasic
<ComponentModel.Designer("Componentes.listatabdesigner")> _
Public Class listatab
    Inherits Web.UI.WebControls.Table

    Dim _datasource As DataTable
    Public Shadows Property [datasource]() As DataTable    '#usar o viewstate
        Get
            Return _datasource
        End Get
        Set(ByVal Value As DataTable)
            _datasource = Value
            _sotitulo = False
            RaiseEvent datasource_setado()
        End Set
    End Property

    '## evento disparado na setagem do datasource
    Public Event datasource_setado()


    Dim _sotitulo As Boolean = True
    Public Property [sotitulo]() As Boolean        '#usar o viewstate 
        Get
            Return _sotitulo
        End Get
        Set(ByVal Value As Boolean)
            _sotitulo = Value
        End Set
    End Property

    Dim _tabela_simples As Boolean = True
    Public Property TABELA_SIMPLES() As Boolean
        Get
            Return Me._tabela_simples
        End Get
        Set(ByVal Value As Boolean)
            Me._tabela_simples = Value
        End Set
    End Property

    Dim _tem_rodape As Boolean = False
    Public Property TEM_RODAPE() As Boolean
        Get
            Return Me._tem_rodape
        End Get
        Set(ByVal Value As Boolean)
            Me._tem_rodape = Value
        End Set
    End Property

    Dim _altura_principal As Integer
    Public Property ALTURA_PRINCIPAL() As Integer       '#usar o viewstate
        Get
            Return Me._altura_principal
        End Get
        Set(ByVal Value As Integer)
            If Value >= Me.Height.Value Then
                Me._altura_principal = Value
            End If
        End Set
    End Property

    Dim _lista_selecao As Boolean = True
    Public Property LISTA_SELECAO() As Boolean          '#usar o viewstate
        Get
            Return _lista_selecao
        End Get
        Set(ByVal Value As Boolean)
            Me._lista_selecao = Value
        End Set
    End Property

    Dim _lista_multi_selecao As Boolean = False
    Public Property MULTI_SELECAO() As Boolean          '#usar o viewstate
        Get
            Return Me._lista_multi_selecao
        End Get
        Set(ByVal Value As Boolean)
            Me._lista_multi_selecao = Value And Me._lista_selecao
        End Set
    End Property

    Dim _sem_titulo As Boolean = False
    Public Property SEM_TITULO() As Boolean
        Get
            Return Me._sem_titulo
        End Get
        Set(ByVal Value As Boolean)
            Me._sem_titulo = Value
        End Set
    End Property

    Dim _filtro_lista As String = ""
    Public Property FILTRO_LISTA() As String
        Get
            Return _filtro_lista
        End Get
        Set(ByVal Value As String)
            Me._filtro_lista = Value
        End Set
    End Property

    '    Dim _menu_operacoes As menu_popup
    '    Public Property MENU_OPERACOES() As menu_popup
    '        Get
    '            Return _menu_operacoes
    '        End Get
    '        Set(ByVal Value As menu_popup)
    '            Me._menu_operacoes = Value
    '      End Set
    '   End Property

    Dim _scroll_selecionada As Boolean = True
    Public Property SCROLL_SELECIONADA() As Boolean
        Get
            Return Me._scroll_selecionada
        End Get
        Set(ByVal Value As Boolean)
            Me._scroll_selecionada = Value
        End Set
    End Property

    Dim _tem_titulo As Boolean = True
    Public Property TEM_TITULO() As Boolean
        Get
            Return Me._tem_titulo
        End Get
        Set(ByVal Value As Boolean)
            Me._tem_titulo = False
        End Set
    End Property

    Public Sub Control_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
    End Sub

    Protected _estilo_titulo As String = "font-family: Verdana; font-size: 11;  font-weight: bold; align: center; color: white; background-color: #0066CC"
    Dim _estilo_coluna As String = "font-family: Verdana; font-size: 10; font-weight: 400; align: left; color: black; background-color: #FFFFFF"
    Dim _estilo_coluna_alternativa As String = "font-family: Verdana; font-size: 10; font-weight: 400; align: left; color: black; background-color: #FFFFCC"
    Dim _cor_fundo_selecao As String = "blue"
    Dim _cor_fonte_selecao As String = "white"
    Dim _linha_sel As String = ""
    Dim altura_linha = 18
    Dim _cor_fundo_linha As String = "white"
    Dim _cor_fonte_linha As String = "black"

    Dim _cor_fundo_linha_alternativa As String = "#FFFFCC"
    Dim _cor_fonte_linha_alternativa As String = "black"
    Dim _variacao_estilo_coluna As Integer = 2

    Public dr_nova As DataRow '## NOVA LINHA PARA INCLUSOES


    Protected Const PTIPO = 0
    Protected Const PTIPOVARIAVEL = 1, PTITULO = 2
    Protected Const PLARGURA = 4
    Protected Const PDETALHE = 5, PDETALHEFIXO = 6
    Protected Const PLINK = 7, PLINKFIXO = 8
    Public Const PPARMNOME = 0, PPARMVAL = 1, PPARMFIXO = 2
    Protected Const PPARAMETRO = 9
    Protected Const PFORMATO = 10
    Protected Const PSINTAXE = 11
    Protected Const PTIPO_TARGET = 12, PTARGET = 13
    Protected Const PESTILO_COLUNA = 14, PESTILO_TITULO = 15
    Protected Const PESTILO_COLUNA_ALTERNATIVA = 16, PVARIACAO_ESTILO_COLUNA = 17
    Protected Const PTIPO_CAMPO = 18, PCOLUNA_DEFINE_HABILITADO = 19
    Protected Const POBRIGATORIO = 20
    Protected Const PTAMANHO = 21
    Protected Const PCOLUNA_DEFINE_VISIVEL = 22
    Protected Const PTEXTO_ALT = 23
    Protected Const PTEXTO_ALT_FIXO = 24
    Protected Const PTEXTO_PURO = 25
    Protected Const PCOLUNA_DEFINE_VISIVEL_HORA = 26
    Const _total_dados = 26

    Public Shared TAMANHO_SCROLL As Long = 17


    Public Enum TIPO_COLUNA
        TIPO_STRING = 0
        TIPO_DATA = 1
        TIPO_IMAGEM = 2
        TIPO_NUMERO = 3
        TIPO_HTML = 4
        TIPO_CONTROL = 5
        TIPO_VARIAVEL = 6
        TIPO_CODIFICADO = 7
        TIPO_DATASOURCE = 8
        TIPO_CAMPO = 9
        TIPO_HIDDEN = 10
        TIPO_DATAHORA = 11
    End Enum

    Public Enum TIPO_TARGET
        TIPO_TOP = 0
        TIPO_PARENT = 1
        TIPO_SELF = 2
        TIPO_BLANK = 3
        TIPO_FRAME = 4
        TIPO_FUNCAOJAVASCRIPT = 5
        TIPO_EVENTO = 6
        TIPO_FUNCAOJAVASCRIPTDIRETA = 7
    End Enum


    Dim _qtd_colunas_hidden As Integer = 0

    Protected _colunas As ArrayList = New ArrayList()
    Dim _ds_tipo_datasource As New Collections.Specialized.ListDictionary()

    Public Sub inscreve_ds_tipo_datasource(ByVal titulo As String, ByVal ds As DataTable)
        Me._ds_tipo_datasource(titulo) = ds
    End Sub

    Public Function coluna_existe(ByVal nome As String) As Boolean
        Dim i As Integer
        Dim d() As Object

        For i = 0 To Me._colunas.Count - 1
            d = Me._colunas(i)
            If d(listatab.PDETALHE).toUpper() = nome.ToUpper Then Return True
        Next
        Return False
    End Function


    Public Sub maiscoluna(ByVal tipo As TIPO_COLUNA, ByVal tipo_variavel As String, _
    ByVal titulo As String, _
    ByVal largura As String, _
    ByVal detalhe As String, ByVal eh_detalhe_fixo As Boolean, _
    ByVal link As String, ByVal eh_link_fixo As Boolean, _
    ByVal tipo_target As TIPO_TARGET, ByVal target As String, _
    ByVal parametro As ArrayList, _
    ByVal formato As String)

        Dim d(listatab._total_dados) As Object

        d(listatab.PTIPO) = tipo
        d(listatab.PTIPOVARIAVEL) = tipo_variavel
        d(listatab.PTITULO) = titulo
        d(listatab.PLARGURA) = IIf(tipo = TIPO_COLUNA.TIPO_HIDDEN, "0", largura)
        d(listatab.PDETALHE) = detalhe
        d(listatab.PDETALHEFIXO) = eh_detalhe_fixo
        d(listatab.PLINK) = link
        d(listatab.PLINKFIXO) = eh_link_fixo
        d(listatab.PPARAMETRO) = parametro
        d(listatab.PTIPO_TARGET) = tipo_target
        d(listatab.PTARGET) = target
        d(listatab.PFORMATO) = formato
        d(listatab.PESTILO_COLUNA) = Me._estilo_coluna
        d(listatab.PESTILO_COLUNA_ALTERNATIVA) = Me._estilo_coluna_alternativa
        d(listatab.PESTILO_TITULO) = Me._estilo_titulo


        Me._colunas.Add(d.Clone)
        d = Me._colunas(Me._colunas.Count - 1)
        d(listatab.PSINTAXE) = sintaxe_celula(d)

        If tipo = TIPO_COLUNA.TIPO_HIDDEN Then
            Me._qtd_colunas_hidden = Me._qtd_colunas_hidden + 1
        End If

        d(listatab.PTEXTO_PURO) = False '## por padrao, nao eh texto puro

    End Sub



    Public Sub maiscoluna(ByVal tipo As TIPO_COLUNA, ByVal tipo_variavel As String, _
    ByVal titulo As String, _
    ByVal largura As String, _
    ByVal detalhe As String, ByVal eh_detalhe_fixo As Boolean, _
    ByVal link As String, ByVal eh_link_fixo As Boolean, _
    ByVal tipo_target As TIPO_TARGET, ByVal target As String, _
    ByVal parametro As ArrayList, _
    ByVal formato As String, _
    ByVal col_ind_visivel As String, _
    ByVal col_texto_alt As String, _
    ByVal ind_texto_alt_fixo As Boolean, _
    Optional ByVal col_ind_habilitado As String = "")

        Me.maiscoluna(tipo, tipo_variavel, titulo, largura, detalhe, eh_detalhe_fixo, _
          link, eh_link_fixo, tipo_target, target, parametro, formato)

        Me._colunas(Me._colunas.Count - 1)(listatab.PCOLUNA_DEFINE_VISIVEL) = col_ind_visivel
        Me._colunas(Me._colunas.Count - 1)(listatab.PTEXTO_ALT) = col_texto_alt
        Me._colunas(Me._colunas.Count - 1)(listatab.PTEXTO_ALT_FIXO) = ind_texto_alt_fixo
        Me._colunas(Me._colunas.Count - 1)(listatab.PCOLUNA_DEFINE_HABILITADO) = col_ind_habilitado
        Me._colunas(Me._colunas.Count - 1)(listatab.PSINTAXE) = sintaxe_celula(Me._colunas(Me._colunas.Count - 1))

    End Sub

    Public Sub maiscoluna(ByVal tipo As TIPO_COLUNA, _
        ByVal titulo As String, _
        ByVal largura As String, _
        ByVal detalhe As String, _
        ByVal formato As String)
        Me.maiscoluna(tipo, "", titulo, largura, detalhe, False, _
          "", True, TIPO_TARGET.TIPO_SELF, "", Nothing, formato)
    End Sub


    Public Sub maislink(ByVal tipo As TIPO_COLUNA, _
        ByVal titulo As String, _
        ByVal largura As String, _
        ByVal detalhe As String, _
        ByVal formato As String, _
        ByVal link As String, ByVal eh_link_fixo As Boolean, _
    ByVal tipo_target As TIPO_TARGET, ByVal target As String, _
    ByVal parametro As ArrayList)
        Me.maiscoluna(tipo, "", titulo, largura, detalhe, False, _
          link, eh_link_fixo, tipo_target, target, parametro, formato)
    End Sub


    Public Sub estilo_coluna(ByVal estilo_titulo As String, _
     ByVal estilo_coluna As String, ByVal estilo_coluna_alternativa As String, _
     ByVal variacao_estilo_coluna As Integer, _
    ByVal cor_fonte_linha As String, ByVal cor_fundo_linha As String, _
    ByVal cor_fonte_alternativa As String, ByVal cor_fundo_alternativa As String)
        If Me._colunas.Count = 0 Then
            Me._estilo_coluna = IIf(estilo_coluna = "", Me._estilo_coluna, estilo_coluna)
            Me._estilo_titulo = IIf(estilo_titulo = "", Me._estilo_titulo, estilo_titulo)
            Me._estilo_coluna_alternativa = IIf(estilo_coluna_alternativa = "", Me._estilo_coluna_alternativa, estilo_coluna_alternativa)
            Me._variacao_estilo_coluna = IIf(variacao_estilo_coluna < 0, _variacao_estilo_coluna, variacao_estilo_coluna)
            Me._cor_fonte_linha = cor_fonte_linha
            Me._cor_fundo_linha = cor_fundo_linha
            Me._cor_fonte_linha_alternativa = cor_fonte_alternativa
            Me._cor_fundo_linha_alternativa = cor_fundo_alternativa
            Exit Sub
        End If


        Dim d() As Object = Me._colunas.Item(Me._colunas.Count - 1)
        d(listatab.PESTILO_COLUNA) = IIf(estilo_coluna = "", Me._estilo_coluna, IIf(Left(estilo_coluna, 1) = "+", Me._estilo_coluna & ";" & Mid(estilo_coluna, 2, 1000), estilo_coluna))
        d(listatab.PESTILO_TITULO) = IIf(estilo_titulo = "", Me._estilo_titulo, IIf(Left(estilo_titulo, 1) = "+", Me._estilo_titulo & ";" & Mid(estilo_titulo, 2, 1000), estilo_titulo))
        d(listatab.PESTILO_COLUNA_ALTERNATIVA) = IIf(estilo_coluna_alternativa = "", Me._estilo_coluna_alternativa, IIf(Left(estilo_coluna_alternativa, 1) = "+", Me._estilo_coluna_alternativa & ";" & Mid(estilo_coluna_alternativa, 2, 1000), estilo_coluna_alternativa))
        d(listatab.PVARIACAO_ESTILO_COLUNA) = IIf(variacao_estilo_coluna < 0, _variacao_estilo_coluna, variacao_estilo_coluna)
    End Sub

    Public Sub altera_tamanho(ByVal numcol As Integer, ByVal tamanho As String)
        If Me._colunas.Count <= numcol Then Return
        Dim d() As Object = Me._colunas.Item(numcol)
        d(listatab.PLARGURA) = tamanho
        If Me.Rows.Count <> 0 Then
            Me.CreateChildControls()
        End If
    End Sub

    '## seta estilo de coluna e titulo desta quando ja foi declarada coluna
    Public Sub estilo_coluna(ByVal estilo_c As String, ByVal acrescenta_coluna As Boolean, _
    ByVal estilo_t As String, ByVal acrescenta_titulo As Boolean)
        If Me._colunas.Count = 0 Then Return

        Dim d() As Object = Me._colunas.Item(Me._colunas.Count - 1)
        d(listatab.PESTILO_COLUNA) = IIf(acrescenta_coluna, Me._estilo_coluna & ";" & estilo_c, estilo_c)
        d(listatab.PESTILO_TITULO) = IIf(acrescenta_titulo, Me._estilo_titulo & ";" & estilo_t, estilo_t)
    End Sub

    '## seta coluna com estilo de texto puro (retirando tags HTML)
    Public Sub coluna_texto_puro(ByVal texto_puro As Boolean)
        Dim d() As Object = Me._colunas.Item(Me._colunas.Count - 1)
        d(listatab.PTEXTO_PURO) = texto_puro
    End Sub

    Protected Function sintaxe_celula(ByRef d() As Object) As String
        Dim ret As String = ""
        If d(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_STRING Or _
           d(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_CODIFICADO Or _
           d(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_DATASOURCE Then
            ret = d(listatab.PDETALHE)
            If d(listatab.PDETALHEFIXO) = False Then
                ret = "{" & ret & "}"
            End If
        ElseIf d(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_NUMERO Or _
               d(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_DATA Or _
               d(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_DATAHORA Then
            If d(listatab.PDETALHEFIXO) = False Then
                If d(listatab.PFORMATO) <> "" Then
                    ret = "{0:" & d(listatab.PFORMATO) & "}"
                Else
                    ret = "{" & d(listatab.PDETALHE) & "}"
                End If
            End If
        ElseIf d(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_IMAGEM Then
            ret = d(listatab.PDETALHE)
            If d(listatab.PDETALHEFIXO) = False Then
                ret = "{" & ret & "}"
            End If

            Dim alt As String = d(listatab.PTEXTO_ALT)
            If Not d(listatab.PTEXTO_ALT_FIXO) Then alt = "{" & alt & "}"

            ret = "<img src=""" & ret & """ border=0 alt=""" & alt & """>"
        ElseIf d(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_HIDDEN Then
            ret = d(listatab.PDETALHE)
            If d(listatab.PDETALHEFIXO) = False Then
                ret = "{" & ret & "}"
            End If
        Else
            If d(listatab.PDETALHEFIXO) = False Then
                If d(listatab.PFORMATO) <> "" Then
                    ret = String.Format("{0:" & d(listatab.PFORMATO) & "}", d(listatab.PDETALHE))
                Else
                    ret = d(listatab.PDETALHE)
                End If
            End If
        End If

        '## monta o link e os targets
        If d(listatab.PLINK) <> "" And Not d(listatab.PLINK) Is Nothing Then
            Dim lnk As String = d(listatab.PLINK)
            If Not d(listatab.PLINKFIXO) Then
                lnk = "{" & lnk & "}"
            End If

            Dim trg As String = Nothing
            Dim tt As TIPO_TARGET = d(listatab.PTIPO_TARGET)
            If tt = TIPO_TARGET.TIPO_BLANK Then trg = "_blank"
            If tt = TIPO_TARGET.TIPO_PARENT Then trg = "_parent"
            If tt = TIPO_TARGET.TIPO_SELF Then trg = "_self"
            If tt = TIPO_TARGET.TIPO_TOP Then trg = "_top"
            If tt = TIPO_TARGET.TIPO_FRAME Then trg = d(listatab.PTARGET)
            If tt = TIPO_TARGET.TIPO_FUNCAOJAVASCRIPT Or tt = TIPO_TARGET.TIPO_FUNCAOJAVASCRIPTDIRETA Then
                trg = "_self"
                lnk = "javascript:" & lnk
            End If
            If tt = TIPO_TARGET.TIPO_EVENTO Then
                trg = "_self"
                lnk = "javascript:document.all." & Me.ClientID & "_" & (Me._colunas.Count) & "_link.value='"
            End If

            If Not d(listatab.PPARAMETRO) Is Nothing Then
                Dim i As Integer
                Dim prm As String = ""
                Dim p As ArrayList = d(listatab.PPARAMETRO)
                Dim trip() As Object
                If tt <> TIPO_TARGET.TIPO_FUNCAOJAVASCRIPT Then
                    If lnk.IndexOf("?") < 0 Then
                        prm = "?"
                    ElseIf lnk.IndexOf("?") > 0 Then
                        prm = "&"
                    End If
                End If
                For i = 0 To p.Count - 1
                    trip = p(i)
                    If trip(listatab.PPARMNOME) <> "" And trip(listatab.PPARMVAL) <> "" Then
                        If tt = TIPO_TARGET.TIPO_FUNCAOJAVASCRIPT Then
                            prm = prm & "'"
                        Else
                            prm = prm & trip(listatab.PPARMNOME) & "="
                        End If

                        If Not trip(listatab.PPARMFIXO) Then
                            prm = prm & "{" & trip(listatab.PPARMVAL) & "}"
                        Else
                            prm = prm & Me.Page.Server.UrlEncode(trip(listatab.PPARMVAL))
                        End If
                        If tt = TIPO_TARGET.TIPO_FUNCAOJAVASCRIPT Then
                            prm = prm & "',"
                        Else
                            prm = prm & "&"

                        End If
                    End If
                Next
                If (tt = TIPO_TARGET.TIPO_FUNCAOJAVASCRIPT And prm.EndsWith(",")) Or _
                   (tt <> TIPO_TARGET.TIPO_FUNCAOJAVASCRIPT And prm.EndsWith("&")) Then
                    prm = prm.Substring(0, prm.Length - 1)
                End If
                If prm.Length > 2 Then
                    If tt = TIPO_TARGET.TIPO_EVENTO Then
                        lnk = lnk & prm.Substring(1) & "'; Form1.submit();"
                    ElseIf tt = TIPO_TARGET.TIPO_FUNCAOJAVASCRIPT Then
                        lnk = lnk & "(" & prm & ")"
                    Else
                        lnk = lnk & prm
                    End If
                End If
            End If
            ret = "<a href=""" & lnk & """ target=""" & trg & """>" & ret & "</a>"
        End If


        sintaxe_celula = ret
    End Function


    '## FUNCAO QUE PERMITE GERAR PARAMETROS DE LINK
    Public Sub parametro_link(ByRef parm As ArrayList, ByVal nome As String, ByVal valor As String, ByVal eh_fixo As Boolean)
        If parm Is Nothing Then
            parm = New ArrayList()
        End If
        Dim vet(2) As Object

        vet(listatab.PPARMNOME) = nome
        vet(listatab.PPARMVAL) = valor
        vet(listatab.PPARMFIXO) = eh_fixo
        parm.Add(vet)

    End Sub

    Protected Sub mais_coluna_link_popup(ByVal titulo As String, ByVal coluna As String, _
        ByVal largura_coluna As String, ByVal eh_texto_fixo As Boolean, ByVal link As String, _
        ByVal link_eh_texto_fixo As Boolean, ByVal parametros As ArrayList, _
        ByVal altura_popup As Integer, ByVal largura_popup As Integer, ByVal nom_funcao_popup As String)

        link = nom_funcao_popup & "(" & altura_popup & "," & largura_popup & ",'" & link
        If Not parametros Is Nothing Then
            Me.parametro_link(parametros, "IDfecha", "')", True) '## passa o fecho da funcao depois dos parametros
        Else
            link = link & "')"
        End If

        Me.maiscoluna(Componentes.listatab.TIPO_COLUNA.TIPO_STRING, "", titulo, _
        largura_coluna, coluna, eh_texto_fixo, link, link_eh_texto_fixo, Componentes.listatab.TIPO_TARGET.TIPO_FUNCAOJAVASCRIPTDIRETA, _
        "_SELF", parametros, "")


    End Sub


    '##################################################
    '## define as linhas a serem renderizadas para o caso de listas
    '## muito grandes
    Protected NUM_LINHA_INICIAL As Long = 0
    Protected NUM_LINHA_FINAL As Long = 0

    '########################################################
    '########################################################
    '########################################################
    '########################################################
    '########################################################
    '## CRIA AS CELULAS LENDO DO DATASOURCE

    Protected Overrides Sub CreateChildControls()
        'monta a linha de titulo    
        Dim r As Web.UI.WebControls.TableRow
        Dim c As Web.UI.WebControls.TableCell
        Me.Rows.Clear()
        r = New Web.UI.WebControls.TableRow()

        '## calcula o tamanho final de cada celula em funcao do total especificado
        '## e da tabela final
        Dim d() As Object = Nothing

        Dim larg_fornecida As Long = 0
        Dim unid As Web.UI.WebControls.Unit
        For Each d In Me._colunas
            If d(listatab.PTIPO) <> listatab.TIPO_COLUNA.TIPO_HIDDEN Then
                unid = New Web.UI.WebControls.Unit(CType(d(listatab.PLARGURA), String))
                If unid.Value > 0 Then larg_fornecida = larg_fornecida + unid.Value
            End If
        Next

        Dim do_objeto As Long
        do_objeto = Me.Width.Value - TAMANHO_SCROLL
        If do_objeto > 0 And larg_fornecida > 0 Then
            For Each d In Me._colunas
                If d(listatab.PTIPO) <> listatab.TIPO_COLUNA.TIPO_HIDDEN Then
                    unid = New Web.UI.WebControls.Unit(CType(d(listatab.PLARGURA), String))
                    If unid.Value > 0 Then
                        d(listatab.PLARGURA) = CStr(CInt(unid.Value / larg_fornecida * do_objeto)) & "px"
                    End If
                End If
            Next
        End If

        Dim ii As Integer = 0
        For Each d In Me._colunas
            c = New Web.UI.WebControls.TableCell()
            c.Width = New Web.UI.WebControls.Unit(CType(d(listatab.PLARGURA), String))
            c.Text = d(listatab.PTITULO)
            c.Attributes.Add("Style", d(listatab.PESTILO_TITULO))
            c.Style.Add("margin-left", "3px")
            c.ID = Me.ClientID & "_coluna" & ii
            If d(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_HIDDEN Then
                c.Style("DISPLAY") = "none"
            End If
            r.Cells.Add(c)
            ii = ii + 1
        Next
        r.Height = New Web.UI.WebControls.Unit(Me.altura_linha, Web.UI.WebControls.UnitType.Pixel)
        r.ID = Me.ID & "_li0"
        r.Visible = Not Me.SEM_TITULO
        Me.Rows.Add(r)


        '## se tem menu, seta opcao de procura de menu
        '        If Not Me._menu_operacoes Is Nothing Then
        '            ii = 0
        '            For Each c In r.Cells
        '               Me._menu_operacoes.objeto_usa(c)
        '                Me._menu_operacoes.cria_menu(c, "Procurar...", menu_popup.tipo_exec.JAVASCRIPT, Me.ClientID & "_procurar(" & ii & ")")
        '               ii = ii + 1
        '          Next
        '     End If


        If Me._sotitulo Then Exit Sub

        Dim drs() As DataRow = Nothing
        If Not Me.datasource Is Nothing Then
            drs = Me._datasource.Select(Me._filtro_lista)
        End If

        If drs.Length = 0 Then
            r = New Web.UI.WebControls.TableRow()
            c = New Web.UI.WebControls.TableCell()
            c.ColumnSpan = Me._colunas.Count - Me._qtd_colunas_hidden
            c.Style.Add("margin-left", "3px")
            r.ID = Me.ID & "_li1"
            r.Height = New Web.UI.WebControls.Unit(Me.altura_linha, Web.UI.WebControls.UnitType.Pixel)
            Dim lb As New label()
            lb.Text = "Não há dados para exibir"
            c.Controls.Add(lb)
            r.Cells.Add(c)
            Me.Rows.Add(r)
            Exit Sub
        End If

        Dim l As DataRow
        Dim qtd_l As Long = 0
        Dim alternativo As Boolean = False
        Dim estilo, cor_fundo, cor_fonte As String
        Dim ix As Long
        Dim ini As Long = Math.Max(0, Me.NUM_LINHA_INICIAL)
        Dim fim As Long = IIf(Me.NUM_LINHA_FINAL = 0, drs.Length - 1, Me.NUM_LINHA_FINAL)

        If Me.linha_sel() < ini Or Me.linha_sel() > fim Then
            Me._linha_sel = Me.ClientID & "_li" & (ini + 1)
        End If


        For ix = ini To fim
            l = drs(ix)
            r = New Web.UI.WebControls.TableRow()
            If alternativo Then
                cor_fonte = Me._cor_fonte_linha_alternativa
                cor_fundo = Me._cor_fundo_linha_alternativa
                estilo = Me._estilo_coluna_alternativa
            Else
                cor_fonte = Me._cor_fonte_linha
                cor_fundo = Me._cor_fundo_linha
                estilo = Me._estilo_coluna
            End If

            estilo = IIf(estilo = "", d(listatab.PESTILO_COLUNA), estilo)

            For Each d In Me._colunas
                c = New Web.UI.WebControls.TableCell()
                c.Width = New Web.UI.WebControls.Unit(CType(d(listatab.PLARGURA), String))
                c.Attributes.Add("style", IIf(alternativo, d(listatab.PESTILO_COLUNA_ALTERNATIVA), d(listatab.PESTILO_COLUNA)))
                c.BackColor = Drawing.Color.FromName(cor_fundo)
                c.ForeColor = Drawing.Color.FromName(cor_fonte)

                c.Text = processa_coluna(d, l)
                c.Text = processa_link_coluna(d, l, c.Text)
                If d(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_HIDDEN Then
                    c.Style("DISPLAY") = "none"
                End If

                '   If d(listatab.PTEXTO_PURO) Then c.Text = HTMLEditor.extrai_tags_HTML(c.Text)

                c.Style.Add("padding-left", "2px")
                r.Cells.Add(c)
            Next
            qtd_l = qtd_l + 1
            r.ID = Me.ID & "_li" & (ini + qtd_l)

            r.Attributes.Add("cor_fundo_selecao", Me._cor_fundo_selecao)
            r.Attributes.Add("cor_fonte_selecao", Me._cor_fonte_selecao)
            r.Attributes.Add("cor_fundo", cor_fundo)
            r.Attributes.Add("cor_fonte", cor_fonte)

            '## coloca os itens que automatizam a selecao da lista
            If Me._lista_selecao And Me.Enabled Then
                r.Attributes.Add("onclick", Me.ClientID & "_seleciona(this)")
                If Me._funcao_duploclick <> "" Then
                    r.Attributes.Add("ondblclick", Me._funcao_duploclick & "(this); return false;")
                End If
            End If

            '            r.Height = New Web.UI.WebControls.Unit(Me.altura_linha, Web.UI.WebControls.UnitType.Pixel)
            r.Attributes.Add("style", IIf(Me._lista_selecao, "cursor: hand;", "") & estilo)
            r.Height = Web.UI.WebControls.Unit.Empty
            Me.Rows.Add(r)
            alternativo = Not alternativo
        Next
        'gera cada coluna com seu estilo
    End Sub


    'inscreve cada coluna conforme o tipo
    'setagem automatica de estilo de cabecalho e rodape
    'colunas de gifs, links e webcontrols


    Protected Overrides Function SaveViewState() As Object
        If Not Me.datasource Is Nothing Then
            If Me._salva_ds_session Then
                '## salva no viewstate o nome do session usado
                Dim nome As String
                Do
                    nome = Me.ClientID & "_DS_" & CLng(1000 * Rnd())
                Loop While Not Me.Page.Session(nome) Is Nothing

                Me.ViewState("NOME_SESSION_DS") = nome
                Me.Page.Session(nome) = Me.datasource
            Else
                Me.ViewState("DATASOURCE") = datatableutil.saveviewstate_datatable(Me._datasource)
            End If
        End If

        Me.ViewState("COLUNAS") = Me._colunas
        Me.ViewState("SOTITULO") = Me._sotitulo
        Me.ViewState("ALTURAPRINCIPAL") = Me._altura_principal
        Me.ViewState("LISTASELECAO") = Me._lista_selecao
        Me.ViewState("LISTAMULTISELECAO") = Me._lista_multi_selecao
        Me.ViewState("COLUNAAUTOINCREMENTO") = Me._coluna_auto_incremento
        Me.ViewState("SEMTITULO") = Me._sem_titulo
        Me.ViewState("FILTROLISTA") = Me._filtro_lista
        Me.ViewState("LINHASEL") = Me._linha_sel
        Me.ViewState("FUNCAO_DUPLOCLICK") = Me._funcao_duploclick
        Me.ViewState("QTD_COLUNAS_HIDDEN") = Me._qtd_colunas_hidden
        Me.ViewState("SCROLL_SELECIONADA") = Me._scroll_selecionada
        Me.ViewState("SALVA_DS_SESSION") = Me._salva_ds_session

        Me.ViewState("ESTILO_COLUNA") = Me._estilo_coluna
        Me.ViewState("ESTILO_TITULO") = Me._estilo_titulo
        Me.ViewState("ESTILO_COLUNA_ALTERNATIVA") = Me._estilo_coluna_alternativa
        Me.ViewState("COR_FUNDO_SELECAO") = Me._cor_fundo_selecao
        Me.ViewState("COR_FONTE_SELECAO") = Me._cor_fonte_selecao
        Me.ViewState("COR_FUNDO_LINHA") = Me._cor_fundo_linha
        Me.ViewState("COR_FONTE_LINHA") = Me._cor_fonte_linha
        Me.ViewState("COR_FUNDO_LINHA_ALTERNATIVA") = Me._cor_fundo_linha_alternativa
        Me.ViewState("COR_FONTE_LINHA_ALTERNATIVA") = Me._cor_fonte_linha_alternativa
        Me.ViewState("VARIACAO_ESTILO_COLUNA") = Me._variacao_estilo_coluna
        Me.ViewState("TABELA_SIMPLES") = Me._tabela_simples
        Me.ViewState("TEM_RODAPE") = Me._tem_rodape
        Me.ViewState("TEM_TITULO") = Me._tem_titulo

        Return MyBase.SaveViewState()
    End Function

    Protected Overrides Sub LoadViewState(ByVal savedState As Object)
        MyBase.LoadViewState(savedState)

        Me._salva_ds_session = Me.ViewState("SALVA_DS_SESSION")
        If Me._salva_ds_session Then
            If Not Me.ViewState("NOME_SESSION_DS") Is Nothing Then
                Me._datasource = Me.Page.Session(Me.ViewState("NOME_SESSION_DS"))
                Me.Page.Session.Remove(Me.ViewState("NOME_SESSION_DS"))
            End If
        Else
            If Not Me.ViewState("DATASOURCE") Is Nothing Then
                Me._datasource = datatableutil.loadviewstate_datatable(Me.ViewState("DATASOURCE"))
            End If
        End If

        Me._colunas = Me.ViewState("COLUNAS")
        Me._sotitulo = Me.ViewState("SOTITULO")
        Me._altura_principal = Me.ViewState("ALTURAPRINCIPAL")
        Me._lista_selecao = Me.ViewState("LISTASELECAO")
        Me._lista_multi_selecao = Me.ViewState("LISTAMULTISELECAO")
        Me._coluna_auto_incremento = Me.ViewState("COLUNAAUTOINCREMENTO")
        Me._sem_titulo = Me.ViewState("SEMTITULO")
        Me._filtro_lista = Me.ViewState("FILTROLISTA")
        Me._linha_sel = Me.ViewState("LINHASEL")
        Me._funcao_duploclick = Me.ViewState("FUNCAO_DUPLOCLICK")
        Me._qtd_colunas_hidden = Me.ViewState("QTD_COLUNAS_HIDDEN")
        Me._scroll_selecionada = Me.ViewState("SCROLL_SELECIONADA")

        Me._estilo_coluna = Me.ViewState("ESTILO_COLUNA")
        Me._estilo_titulo = Me.ViewState("ESTILO_TITULO")
        Me._estilo_coluna_alternativa = Me.ViewState("ESTILO_COLUNA_ALTERNATIVA")
        Me._cor_fundo_selecao = Me.ViewState("COR_FUNDO_SELECAO")
        Me._cor_fonte_selecao = Me.ViewState("COR_FONTE_SELECAO")
        Me._cor_fundo_linha = Me.ViewState("COR_FUNDO_LINHA")
        Me._cor_fonte_linha = Me.ViewState("COR_FONTE_LINHA")
        Me._cor_fundo_linha_alternativa = Me.ViewState("COR_FUNDO_LINHA_ALTERNATIVA")
        Me._cor_fonte_linha_alternativa = Me.ViewState("COR_FONTE_LINHA_ALTERNATIVA")
        Me._variacao_estilo_coluna = Me.ViewState("VARIACAO_ESTILO_COLUNA")
        Me._tabela_simples = Me.ViewState("TABELA_SIMPLES")
        Me._tem_rodape = Me.ViewState("TEM_RODAPE")
        Me._tem_titulo = Me.ViewState("TEM_TITULO")

    End Sub

    '## define o nome de uma funcao javascript a ser chamada caso ocorra duploclick
    Dim _funcao_duploclick As String = ""
    Public Property FUNCAO_DUPLO_CLICK() As String
        Get
            Return Me._funcao_duploclick
        End Get
        Set(ByVal Value As String)
            Me._funcao_duploclick = Value
        End Set
    End Property


    Protected Overridable Sub RenderRodape(ByVal writer As System.Web.UI.HtmlTextWriter, ByVal largura As Long)

    End Sub


    Protected Overridable Sub RenderLinhas(ByVal writer As Web.UI.HtmlTextWriter)
        Dim i As Long
        For i = 1 To Me.Rows.Count - 1
            Me.Rows(i).RenderControl(writer)
        Next
    End Sub

    'renderiza a pagina gerando html com divs
    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        '##inicia calcuando os DIVs para verificar o scroll
        Dim altura As Integer = Me.Height.Value
        Dim top As Integer = Web.UI.WebControls.Unit.Parse(Me.Style("TOP")).Value
        Dim left As Integer = Web.UI.WebControls.Unit.Parse(Me.Style("LEFT")).Value

        If altura <= 10 Then altura = 220 '## altura padrao para listas sem definicao

        Dim largura As Integer = Me.Width.Value
        If largura <= 10 Then largura = 500 '## largura padrao para listas sem definicao

        '## o div principal é do tamanho da tabela final se nao for setado fora
        If Me._altura_principal > 0 And Me._altura_principal >= altura Then
            altura = Me._altura_principal
        End If

        '## o div de conteudo deve ser do tamanho da tabela - 20 de altura e - 17 de largura
        Dim altura_conteudo As Integer = altura - IIf(Me.SEM_TITULO, 0, 20)
        Dim largura_conteudo As Integer = IIf((Me.Rows.Count - 1) * Me.altura_linha < altura_conteudo Or Me.TABELA_SIMPLES, largura, largura - TAMANHO_SCROLL)
        Dim usa_div_conteudo As Boolean = True
        '## se a largura total informada em string for maior que o tamanho setado da tabela
        '## nao poe o div em baixo para o scroll ocorrer na tabela como um todo
        If Me.Rows(1).Width.Value > largura_conteudo Then
            usa_div_conteudo = False
        End If

        If Not Me.TABELA_SIMPLES Then
            '## gera o div inicial
            largura = largura + TAMANHO_SCROLL
            writer.Write("<div style=""scrollbar-base-color: light-gray; vertical-overflow: auto; " & _
                         "border-style: ridge; border-width: 0; LEFT: " & Me.Style("LEFT") & _
                         "; POSITION: absolute; TOP: " & Me.Style("TOP") & _
                         "; width:" & largura & "px; height:" & altura & """>" & ControlChars.CrLf)
        Else
            writer.Write("<div style=""border-style: ridge; border-width: 0; LEFT: " & Me.Style("LEFT") & _
                         "; POSITION: absolute; TOP: " & Me.Style("TOP") & _
                         "; width:" & largura & "px; height:" & altura & """>" & ControlChars.CrLf)
        End If
        'Dim tabela As String
        '## gera a tabela e os titulos

        '        tabela = "<table id=""" & Me.ClientID & """ cellspacing=""" & Me.CellSpacing & _
        '                     """ cellpadding=""" & Me.CellPadding & """ border=""" & Me.BorderWidth.Value & _
        '                     """ style=""table-layout: fixed; width: " & Me.Width.Value & "px ;Z-INDEX: 103; "">" & ControlChars.CrLf
        '        writer.Write(tabela)
        Me.Style("POSITION") = "relative"
        Me.Style("LEFT") = ""
        Me.Style("TOP") = ""
        If Not Me.TABELA_SIMPLES Then
            Me.Style("table-layout") = "fixed"
        Else
            Me.Style("table-layout") = "flow"
        End If
        Me.Style("height") = ""
        Me.Height = Web.UI.WebControls.Unit.Empty
        Me.RenderBeginTag(writer)
        If Me.TEM_TITULO Then Me.Rows(0).RenderControl(writer)

        '## fecha a tabela só em caso de tabela complexa
        If Not Me.TABELA_SIMPLES Then writer.Write("</table>")

        '## gera o div de baixo
        If Not Me.TABELA_SIMPLES And usa_div_conteudo Then
            writer.Write("<div style=""scrollbar-base-color: light-gray; overflow: auto; " & _
                         "border-style: ridge; border-width: 0; " & _
                         " width:" & largura & "px; height:" & altura_conteudo & """>" & ControlChars.CrLf)
        End If
        '## gera a tabela de baixo e todas as linhas
        If Not Me.TABELA_SIMPLES Then
            Dim id_ant = Me.ID
            Me.ID = Me.ID & "_conteudo"
            Me.Style("WIDTH") = largura_conteudo
            Me.Width = Web.UI.WebControls.Unit.Pixel(largura_conteudo)
            Me.Style("POSITION") = "relative"
            Me.Style("LEFT") = "0"
            Me.Style("TOP") = "0"
            Me.Style("table-layout") = "fixed"
            Me.Height = WebControls.Unit.Empty
            Me.Style("height") = ""

            Me.RenderBeginTag(writer)
            Me.ID = id_ant

            'tabela = "<table id=""" & Me.ClientID & "_conteudo"" cellspacing=""" & Me.CellSpacing & _
            '             """ cellpadding=""" & Me.CellPadding & """ border=""" & Me.BorderWidth.Value & _
            '             """ style=""table-layout: fixed; width: " & largura_conteudo & "px ;Z-INDEX: 103; "">" & ControlChars.CrLf

            '            writer.Write(tabela)
        End If

        Dim i As Long
        Me.RenderLinhas(writer)
        writer.Write("</table>")

        '## fecha o div conteudo se necessario
        If Not Me.TABELA_SIMPLES And usa_div_conteudo Then writer.Write("</div>")
        '## fecha o div geral
        writer.Write("</div>")

        If Me.TEM_RODAPE Then
            writer.Write("<div style=""border-style: ridge; border-width: 0; LEFT: " & left & _
                         "; POSITION: absolute; TOP: " & (top + altura + 10) & _
                         "px; width:" & largura & "px; "">" & ControlChars.CrLf)
            Me.RenderRodape(writer, largura_conteudo)
            writer.Write("</div>")
        End If

        '## se for de selecao, monta o javascript para recuperar a selecao
        If Me._lista_selecao Then
            '## inclui um input hidden para armazenar o(s) selecionado(s)
            If Me._lista_multi_selecao Then
                writer.Write("<input type=""hidden"" id=""" & Me.ClientID & "_selecionado"" name=""" & Me.ClientID & "_selecionado"" value="","" />")
            Else
                writer.Write("<input type=""hidden"" id=""" & Me.ClientID & "_selecionado"" name=""" & Me.ClientID & "_selecionado"" value=""" & IIf(Me._datasource.Rows.Count = 0, "", Me._linha_sel) & """ />")
            End If

            writer.Write("<script language=javascript>" & ControlChars.CrLf)
            writer.Write("var " & Me.ClientID & "_atual;" & ControlChars.CrLf)

            writer.Write("function " & Me.ClientID & "_seleciona(lin) {" & ControlChars.CrLf)
            If Not Me._lista_multi_selecao Then
                writer.Write("if (" & Me.ClientID & "_atual == lin) { return;}" & ControlChars.CrLf)
                writer.Write("if (" & Me.ClientID & "_atual != null) {" & ControlChars.CrLf)
                writer.Write("" & Me.ClientID & "_atual.style.backgroundColor = " & Me.ClientID & "_atual.getAttribute('cor_fundo');" & ControlChars.CrLf)
                writer.Write("" & Me.ClientID & "_atual.style.color = " & Me.ClientID & "_atual.getAttribute('cor_fonte'); }" & ControlChars.CrLf)
                writer.Write("" & Me.ClientID & "_atual =  lin;" & ControlChars.CrLf)
                writer.Write("Form1." & Me.ClientID & "_selecionado.value = lin.id;" & ControlChars.CrLf)
                writer.Write("" & Me.ClientID & "_atual.style.backgroundColor = " & Me.ClientID & "_atual.getAttribute('cor_fundo_selecao');" & ControlChars.CrLf)
                writer.Write("" & Me.ClientID & "_atual.style.color = " & Me.ClientID & "_atual.getAttribute('cor_fonte_selecao'); }" & ControlChars.CrLf)
                If Me.QTD_LINHAS_EXIBIDAS > 0 Then
                    Me._linha_sel = Me.ClientID & "_li" & (Me.linha_sel() + 1)
                    writer.Write("" & Me.ClientID & "_seleciona(" & Me._linha_sel & "); " & ControlChars.CrLf)
                    If Me._scroll_selecionada Then
                        writer.Write("" & Me._linha_sel & ".scrollIntoView(); " & ControlChars.CrLf)
                    End If
                End If
            Else
                writer.WriteLine("if (document.all." & Me.ClientID & "_selecionado.value.indexOf(',' + lin.id + ',') >= 0) { ")
                writer.WriteLine("var p = document.all." & Me.ClientID & "_selecionado.value.indexOf(',' + lin.id + ',');")
                writer.WriteLine("document.all." & Me.ClientID & "_selecionado.value = document.all." & Me.ClientID & "_selecionado.value.substring(0,p + 1) + document.all." & Me.ClientID & "_selecionado.value.substring(p + lin.id.length + 2);")
                writer.WriteLine("lin.style.backgroundColor = lin.getAttribute('cor_fundo');")
                writer.WriteLine("lin.style.color = lin.getAttribute('cor_fonte'); }")
                writer.WriteLine("else {")
                writer.WriteLine("document.all." & Me.ClientID & "_selecionado.value +=  lin.id + ',';")
                writer.WriteLine("lin.style.backgroundColor = lin.getAttribute('cor_fundo_selecao');")
                writer.WriteLine("lin.style.color = lin.getAttribute('cor_fonte_selecao'); }")
                writer.WriteLine("}")
                If Me.QTD_LINHAS_EXIBIDAS > 0 Then
                    Dim ls() As String = Me.linhas_sel
                    If Not ls Is Nothing Then
                        For i = 0 To ls.Length - 1
                            writer.WriteLine("" & Me.ClientID & "_seleciona(" & ls(i) & "); ")
                            If i = 0 And Me._scroll_selecionada Then writer.Write("" & ls(i) & ".scrollIntoView(); ")
                        Next
                    End If
                End If
            End If

            If Me.QTD_LINHAS_EXIBIDAS > 0 Then
                '## se tem menu, cria a procura
                'If Not Me._menu_operacoes Is Nothing Then
                '    writer.Write("function " & Me.ClientID & "_procurar(coluna) {" & ControlChars.CrLf)

                '    writer.WriteLine(" var inf = window.prompt(" & Me.ClientID & ".rows[0].cells[coluna].innerText + "" contém com "", """");")
                '    writer.WriteLine(" if (inf == null) {return;}")

                '    writer.WriteLine(" if (inf == """") {return;}")

                '    writer.WriteLine("inf = inf.toUpperCase();")
                '    writer.WriteLine("var cont = """";")

                '    writer.WriteLine(" for (var i = 0; i < " & Me.ClientID & "_conteudo.rows.length; i ++)")
                '    writer.WriteLine("  {")
                '    writer.WriteLine("  cont = " & Me.ClientID & "_conteudo.rows[i].cells[coluna].innerText;")
                '    writer.WriteLine("  if (cont == null) cont = """";")
                '    writer.WriteLine("  cont = cont.toUpperCase();")

                '    writer.WriteLine("    if (cont.indexOf(inf) >= 0)")
                '    writer.WriteLine("    {")
                '    writer.WriteLine("       " & Me.ClientID & "_conteudo.rows[i].click();")
                '    writer.WriteLine("       " & Me.ClientID & "_conteudo.rows[i].scrollIntoView();")
                '    writer.WriteLine("       return;")
                '    writer.WriteLine("    }")
                '    writer.WriteLine("  }  ")
                '    writer.WriteLine("}")
                'End If

            End If
            writer.Write("</script>")


        End If

        Dim d() As Object
        i = 0
        For Each d In Me._colunas
            If d(listatab.PTIPO_TARGET) = listatab.TIPO_TARGET.TIPO_EVENTO Then
                writer.WriteLine("<input type=""hidden"" value ="""" name=""" & Me.ClientID & "_" & (i + 1) & "_link"" id=""" & Me.ClientID & "_" & (i + 1) & "_link"" />")
            End If
            i = i + 1
        Next



    End Sub

    '## obtem o identificador da lista selecionada
    Dim controla_load As Boolean = False

    Private Sub listatab_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.datasource Is Nothing Then Me.datasource = New DataTable("inicial")
        controla_load = True
        If Web.HttpContext.Current.Request.Form(Me.ClientID & "_selecionado") <> "" Then
            If Not Me._lista_multi_selecao Then
                Me._linha_sel = Web.HttpContext.Current.Request.Form(Me.ClientID & "_selecionado")
                If Me._linha_sel = "" Then Me._linha_sel = Me.ClientID & "_li" & (Me.NUM_LINHA_INICIAL + 1)
                Me._linha_sel = Me.ClientID & "_li" & (Me.linha_sel() + 1)
            Else
                Me._linhas_sel = Web.HttpContext.Current.Request.Form(Me.ClientID & "_selecionado")
            End If
        ElseIf Me._linha_sel = "" And Not Me._lista_multi_selecao Then
            Me._linha_sel = Me.ClientID & "_li" & (Me.NUM_LINHA_INICIAL + 1)
        ElseIf Me._linhas_sel = "" And Me._lista_multi_selecao Then
            Me._linhas_sel = ","
        End If

        '## procura os campos hidden resultantes de click na propria lista e dispara o evento
        If Not Me._colunas Is Nothing And Me.Page.IsPostBack Then
            Dim d() As Object
            Dim i As Integer = 1
            For Each d In Me._colunas
                If d(listatab.PLINK) <> "" And d(listatab.PTIPO_TARGET) = listatab.TIPO_TARGET.TIPO_EVENTO And _
                   Me.Page.Request.Form(Me.ClientID & "_" & i & "_link") <> "" Then
                    Dim dic As New Specialized.NameValueCollection()
                    Dim prm() As String = Split(Me.Page.Request.Form(Me.ClientID & "_" & i & "_link"), "&")
                    If Not prm Is Nothing Then
                        If prm.Length > 0 Then
                            Dim cada As String
                            Dim pos As Integer
                            For Each cada In prm
                                pos = cada.IndexOf("=")
                                If pos < 0 Then
                                    dic.Add(cada, "")
                                Else
                                    dic.Add(cada.Substring(0, pos), cada.Substring(pos + 1))
                                End If
                            Next
                        End If
                    End If

                    RaiseEvent CLICK_LINK(d(listatab.PDETALHE), dic)
                    Exit For
                End If
                i = i + 1
            Next
        End If
    End Sub


    Public Event CLICK_LINK(ByVal coluna_origem As String, ByVal parametros As Specialized.NameValueCollection)

    '## devolve a linha que foi selecionada
    Public Function linha_sel() As Long
        If Not Me.controla_load Then
            Me.listatab_Load(Nothing, Nothing)
        End If
        If Not Me._lista_selecao Then Return -1
        If Me._lista_multi_selecao Then Return -1
        If Me._linha_sel = "" Then Return -1
        If Me._datasource Is Nothing Then Return -1

        Dim num As String = ""
        Dim pos As Integer = Me._linha_sel.Length - 1
        While IsNumeric(Me._linha_sel.Substring(pos, 1))
            num = Me._linha_sel.Substring(pos, 1) & num
            pos = pos - 1
        End While
        Dim lng As Long = CLng(num) - 1
        If lng < 0 Then lng = 0

        '## obtem a quantidade de linhas filtradas
        If Me.QTD_LINHAS_EXIBIDAS = 0 Then Return -1


        Return lng

    End Function


    Dim _linhas_sel As String = ""
    '## devolve a linha que foi selecionada
    Public Function linhas_sel() As String()
        If Not Me._lista_selecao Then Return Nothing
        If Not Me._lista_multi_selecao Then Return Nothing
        Dim rep As String = Me._linhas_sel.Replace(",,", ",")
        If rep = "," Or rep = "" Then Return Nothing
        rep = rep.Substring(1, rep.Length - 2)
        Return Split(rep, ",")
    End Function


    '## devolve a linha selecionada inteira
    Public Function obtem_linha() As DataRow
        Dim linha = Me.linha_sel()
        If linha < 0 Then Return Nothing
        Return Me.linha_exibida(linha)
    End Function


    '## devolve informacoes da tabela na linha selecionada
    Public Function obtem(ByVal col As String) As Object
        Dim linha = Me.linha_sel()
        If linha < 0 Then Return Nothing
        If Not Me._datasource.Columns.Contains(col) Then Return Nothing
        Dim ret As Object = Me.linha_exibida(linha).Item(col)
        If ret Is System.DBNull.Value Then
            Return ""
        End If
        Return ret
    End Function

    '## salva informacoes na linha selecionada
    Public Overridable Sub salva(ByVal col As String, ByVal val As Object)
        Dim linha = Me.linha_sel()
        If linha < 0 Then Return
        If Not Me._datasource.Columns.Contains(col) Then Return
        If val Is Nothing Then
            Me.linha_exibida(linha).Item(col) = Nothing
        ElseIf val Is DBNull.Value Then
            Me.linha_exibida(linha).Item(col) = Nothing
        Else
            Me.linha_exibida(linha).Item(col) = val
        End If
    End Sub

    '## exclui a linha atualmente selecionada no datasource
    Public Overridable Sub exclui_atual()
        Dim linha = Me.linha_sel()
        If linha < 0 Then Return

        Me._datasource.Rows.Remove(Me.linha_exibida(linha))

        '## salva uma nova posicao para a linha atual
        Me._linha_sel = Me.ClientID & "_li" & (Me.linha_sel() + 1)
    End Sub

    '## nome da coluna que tem valor default de auto incremento
    Dim _coluna_auto_incremento As String = ""
    Public Property COLUNA_AUTO_INCREMENTO() As String
        Get
            Return Me._coluna_auto_incremento
        End Get
        Set(ByVal Value As String)
            Me._coluna_auto_incremento = Value
        End Set
    End Property


    '## nova linha na tabela, adicionada manualmente
    '## para inclusao de dados no datasource
    Public Overridable Sub inicia_linha()
        If Me._datasource Is Nothing Then Return
        Me.dr_nova = Me._datasource.NewRow
        '## se foi setada uma coluna de adicao automatica,
        '## adiciona automaticamente o valor
        If Me._coluna_auto_incremento <> "" And _
           Me._datasource.Columns.Contains(Me._coluna_auto_incremento) Then
            '## obtem o maior valor atual
            Dim drs() = Me._datasource.Select(Me._coluna_auto_incremento & " = Max(" & Me._coluna_auto_incremento & ")")
            Dim valor As Long
            If drs.Length = 0 Then
                valor = 1
            Else
                valor = drs(0)(Me._coluna_auto_incremento) + 1
            End If
            Me.dr_nova(Me._coluna_auto_incremento) = valor
        End If
    End Sub

    '## seta valores na nova linha conforme o que foi informado
    '## para uso no processo de inclusao de novas linhas
    Public Overridable Sub seta_linha(ByVal col As String, ByVal val As Object)
        If Me.dr_nova Is Nothing Then Return
        If Not Me._datasource.Columns.Contains(col) Then Return
        If val Is Nothing Then
            Me.dr_nova(col) = Nothing
        ElseIf val Is DBNull.Value Then
            Me.dr_nova(col) = Nothing
        Else
            Me.dr_nova(col) = val
        End If
    End Sub

    '## grava a nova linha no datasource
    '## apos sua inclusao e rola o div se necessario
    Public Overridable Sub grava_linha(ByVal rola_div As Boolean)
        If Me.dr_nova Is Nothing Then Return
        Me._datasource.Rows.Add(Me.dr_nova)
        If rola_div Then
            Me._linha_sel = Me.ClientID & "_li" & Me.datasource.Rows.Count
        End If
        Me.dr_nova = Nothing
    End Sub

    '## carrega o datasource com novos dados incluidos
    '## a partir de uma stored procedure
    Public Sub inclui_dados(ByVal dt As DataTable)
        Dim dr As DataRow
        Dim i As Integer

        For Each dr In dt.Rows
            '## verifica se a coluna fornecida existe
            Me.inicia_linha()
            For i = 0 To dt.Columns.Count - 1
                Me.seta_linha(dt.Columns(i).ColumnName, dr(i))
            Next
            Me.grava_linha(True)
        Next
    End Sub

    '## altera o datasource na linha atual com os dados fornecidos
    '## do select resultante de uma stored procedure
    Public Sub altera_dados(ByVal dt As DataTable)

        Dim i As Integer
        If dt.Rows.Count <> 1 Then Return

        For i = 0 To dt.Columns.Count - 1
            Me.salva(dt.Columns(i).ColumnName, dt.Rows(0)(i))
        Next
    End Sub

    Protected Overridable Function processa_coluna(ByVal d() As Object, ByVal l As DataRow) As String
        Dim sint As String = CType(d(listatab.PSINTAXE), String)
        Dim valores_codificados, valor As String
        Dim i, j As Integer
        Dim dt As DataTable

        If d(listatab.PCOLUNA_DEFINE_VISIVEL) <> "" Then
            If CType(l(d(listatab.PCOLUNA_DEFINE_VISIVEL)), String).ToUpper = "N" Then
                Return ""
            End If
        End If

        If d(listatab.PDETALHEFIXO) = True Then
            Return sint
        ElseIf d(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_CODIFICADO Then
            valores_codificados = d(listatab.PFORMATO).ToString
            valor = l.Item(d(listatab.PDETALHE))
            If l.Item(d(listatab.PDETALHE)) Is DBNull.Value Then valor = ""
            If l.Item(d(listatab.PDETALHE)) = Nothing Then valor = ""
            i = valores_codificados.IndexOf("{" & valor & ",")
            If i < 0 Then
                '## procura o default
                i = valores_codificados.ToLower.IndexOf("else:")
                If i >= 0 Then
                    i = i + 5
                    Dim val As String = valores_codificados.Substring(i)

                    Return sint.Replace("{" & d(listatab.PDETALHE) & "}", val)
                End If
            Else
                i = valores_codificados.IndexOf(",", i)
                If i < 0 Then Return ""
                i = i + 1
                j = valores_codificados.IndexOf("}", i)
                If j < 0 Then j = i + 1
                Dim val As String = valores_codificados.Substring(i, j - i)
                Return sint.Replace("{" & d(listatab.PDETALHE) & "}", val)

            End If
        ElseIf d(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_DATASOURCE Then

            If l.Item(d(listatab.PDETALHE)) Is DBNull.Value Then Return ""
            If l.Item(d(listatab.PDETALHE)) = Nothing Then Return ""
            If Me._ds_tipo_datasource.Contains(d(listatab.PTITULO)) Then
                dt = Me._ds_tipo_datasource(d(listatab.PTITULO))
                If dt Is Nothing Then Return ""
                '## procura a linha que tem o d(pdetalhe) = l.item(d(me.pdetalhe))
                '## e pega a coluna d(me.pformato)
                If dt.Columns.Contains(d(listatab.PDETALHE)) And _
                   dt.Columns.Contains(d(listatab.PFORMATO)) Then
                    valor = l.Item(d(listatab.PDETALHE)) '## valor do cod no ds principal
                    Dim drs() As DataRow = dt.Select(d(listatab.PDETALHE) & " = '" & valor & "'")
                    If drs.Length > 0 Then
                        Return sint.Replace("{" & d(listatab.PDETALHE) & "}", drs(0)(d(listatab.PFORMATO)))
                    End If

                End If
            End If
            Return ""
        ElseIf d(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_NUMERO And d(listatab.PFORMATO) <> "" Then
            If l.Item(d(listatab.PDETALHE)) Is DBNull.Value Then Return ""
            Return String.Format(sint, l.Item(d(listatab.PDETALHE)))
        ElseIf (d(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_DATA Or _
                d(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_DATAHORA) Then
            If Not IsDate(l.Item(d(listatab.PDETALHE))) Then
                Return ""
            End If

            If CDate(l.Item(d(listatab.PDETALHE))).Year = 1 Then Return ""
            If d(listatab.PFORMATO) <> "" Then
                Return String.Format(sint, l.Item(d(listatab.PDETALHE)))
            ElseIf d(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_DATA Then
                Return CDate(l.Item(d(listatab.PDETALHE))).ToString("dd/MM/yyyy")
            ElseIf d(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_DATAHORA Then
                Return CDate(l.Item(d(listatab.PDETALHE))).ToString("dd/MM/yyyy HH:mm")
            Else
                Return CDate(l.Item(d(listatab.PDETALHE))).ToString("dd/MM/yyyy")
            End If
        Else
            Dim val As Object
            val = l.Item(d(listatab.PDETALHE))
            If l.Item(d(listatab.PDETALHE)) Is DBNull.Value Then
                val = ""
            ElseIf l.Item(d(listatab.PDETALHE)) = Nothing Then
                val = ""
            End If
            Dim ret As String = sint.Replace("{" & d(listatab.PDETALHE) & "}", CType(val, String))
            If IsNumeric(d(listatab.PFORMATO)) And d(listatab.PFORMATO) <> "" Then
                If ret.TrimEnd.Length > CLng(d(listatab.PFORMATO)) Then
                    ret = Left(ret, CLng(d(listatab.PFORMATO))) & "..."
                End If
            End If
            Return ret
        End If
        Return ""
    End Function

    '## processa os links de coluna

    Protected Overridable Function processa_link_coluna(ByVal d() As Object, ByVal l As DataRow, ByVal t As String) As String

        If d(listatab.PTEXTO_ALT) <> "" And Not d(listatab.PTEXTO_ALT_FIXO) And t <> "" Then
            t = t.Replace("{" & d(listatab.PTEXTO_ALT) & "}", l(d(listatab.PTEXTO_ALT)))
        End If

        If d(listatab.PLINK) = "" Or d(listatab.PLINK) Is Nothing Or t = "" Then Return t


        If d(listatab.PCOLUNA_DEFINE_HABILITADO) <> "" Then
            If CType(l(d(listatab.PCOLUNA_DEFINE_HABILITADO)), String).ToUpper = "N" Then
                '## retira o link de t
                '     t = HTMLEditor.extrai_tags_HTML(t)
            End If
        End If

        If d(listatab.PLINKFIXO) = False Then
            t = t.Replace("{" & d(listatab.PLINK) & "}", l(d(listatab.PLINK)))
        End If


        '## se tiver parametros nao fixos, repassa-os
        If d(listatab.PPARAMETRO) Is Nothing Then Return t
        Dim prm As ArrayList = d(listatab.PPARAMETRO)
        Dim trip() As Object
        Dim i As Integer
        For i = 0 To prm.Count - 1
            trip = prm(i)
            If Not trip(listatab.PPARMFIXO) Then
                If l(trip(listatab.PPARMVAL)) Is DBNull.Value Then
                    t = t.Replace("{" & trip(listatab.PPARMVAL) & "}", "")
                Else
                    t = t.Replace("{" & trip(listatab.PPARMVAL) & "}", l(trip(listatab.PPARMVAL)))

                End If
            End If
        Next
        Return t

    End Function


    Public Function existe_exceto_atual(ByVal coluna As String, ByVal valor As Object, ByVal tipo As Type) As Boolean
        If Me._datasource Is Nothing Then Return False

        Dim qtd_ret As Integer = 1
        If Me.linha_sel() >= 0 Then
            If Me._datasource.Rows(Me.linha_sel)(coluna) = valor Then
                qtd_ret = 2
            End If
        End If

        Dim sint As String = coluna & " = "
        Dim tp As String = tipo.ToString.ToUpper
        If tp.IndexOf("STRING") >= 0 Then
            sint = sint & "'" & valor & "'"
        ElseIf tp.IndexOf("DATE") >= 0 Then
            sint = sint & "#" & Format(CDate(valor), "M/d/yyyy") & "#"
        Else
            sint = sint & valor
        End If

        Return (Me._datasource.Select(sint).Length >= qtd_ret)
    End Function


    Public Function existe(ByVal coluna As String, ByVal valor As Object, ByVal tipo As Type) As Boolean
        If Me._datasource Is Nothing Then Return False

        Dim sint As String = coluna & " = "
        Dim tp As String = tipo.ToString.ToUpper
        If tp.IndexOf("STRING") >= 0 Then
            sint = sint & "'" & valor & "'"
        ElseIf tp.IndexOf("DATE") >= 0 Then
            sint = sint & "#" & Format(CDate(valor), "M/d/yyyy") & "#"
        Else
            sint = sint & valor
        End If

        Return (Me._datasource.Select(sint).Length > 0)
    End Function

    Public ReadOnly Property QTD_LINHAS_EXIBIDAS() As Integer
        Get
            Dim qtd_linhas As Integer
            If Me._filtro_lista <> "" Then
                qtd_linhas = Me._datasource.Select(Me._filtro_lista).Length
            Else
                qtd_linhas = Me._datasource.Rows.Count
            End If
            If Me.NUM_LINHA_FINAL > 0 Then Return Math.Min(qtd_linhas, Me.NUM_LINHA_FINAL - Me.NUM_LINHA_INICIAL + 1)
            Return qtd_linhas
        End Get
    End Property

    Private Function linha_exibida(ByVal num As Integer) As DataRow
        If Me._filtro_lista <> "" Then
            Return Me._datasource.Select(Me._filtro_lista)(num + Me.NUM_LINHA_INICIAL)
        Else
            Return Me._datasource.Rows(num)
        End If
    End Function

    Private Function rowset_exibido() As DataRow()
        Return Me._datasource.Select(Me.FILTRO_LISTA)
    End Function


    Public Function linha_contem(ByRef col As Collections.Specialized.HybridDictionary, ByRef r As DataRow) As Boolean
        Dim nom As String
        For Each nom In col.Keys
            Try
                If r(nom) <> col(nom) Then
                    Return False
                End If
            Catch e As Exception
                Return False
            End Try
        Next
        Return True
    End Function

    Public Overridable Function procura(ByRef col As Collections.Specialized.NameValueCollection, ByVal seleciona As Boolean) As Integer
        If col Is Nothing Then Return 0
        If col.Count = 0 Then Return 0
        Dim i As Integer
        Dim rs() As DataRow = Me.rowset_exibido
        If rs.Length = 0 Then Return 0
        Dim dic As New Collections.Specialized.HybridDictionary()
        Dim nom, tipo As String
        For Each nom In col.AllKeys
            If Me._datasource.Columns.Contains(nom) Then
                tipo = Me._datasource.Columns(nom).DataType.ToString
                dic.Add(nom, System.Convert.ChangeType(col(nom), Type.GetType(tipo, False, True)))
            End If
        Next

        For i = 0 To rs.Length - 1
            If Me.linha_contem(dic, rs(i)) Then
                If seleciona Then
                    Me._linha_sel = Me.ClientID & "_li" & (i + 1)

                End If
                Return i + 1
            End If
        Next
        Return 0
    End Function

    Public Function selecionada(ByVal l As Long) As Boolean
        If Not Me._lista_selecao Or Not Me._lista_multi_selecao Then Return False
        Return (Me._linhas_sel.IndexOf("," & Me.ClientID & "_li" & l & ",") >= 0)
    End Function

    Public Function linhas_selecionadas() As DataRow()
        Dim dr As New ArrayList()
        Dim l() As String = Me.linhas_sel()
        If l Is Nothing Then Return Nothing
        If l.Length = 0 Then Return Nothing
        Dim li As String
        For Each li In l
            li = li.Replace(Me.ClientID & "_li", "")
            If IsNumeric(li) Then
                dr.Add(CLng(li) - 1)
            End If
        Next
        Dim r(dr.Count - 1) As DataRow
        Dim i As Integer
        dr.Sort()
        For i = 0 To dr.Count - 1
            r(i) = Me.datasource.Rows(dr(i))
        Next
        Return r
    End Function

    Public Function remove_linha(ByVal num As Long) As Boolean
        If num > Me.datasource.Rows.Count - 1 Then Return False
        Me.datasource.Rows.RemoveAt(num)
        num = num + 1
        Me._linhas_sel = Me._linhas_sel.Replace(Me.ClientID & "_li" & num & ",", "")
        Return True
    End Function

    Public Function remove_linha(ByVal r As DataRow) As Boolean
        Dim num As Long
        For num = 0 To Me.datasource.Rows.Count - 1
            If Me.datasource.Rows(num) Is r Then Exit For
        Next
        If num = Me.datasource.Rows.Count Then Return False
        num = num + 1
        Me.datasource.Rows.Remove(r)
        Me._linhas_sel = Me._linhas_sel.Replace(Me.ClientID & "_li" & num & ",", "")
        Return True
    End Function

    Public Function seleciona_linha(ByVal r As DataRow) As Boolean
        Dim num As Long
        For num = 0 To Me.datasource.Rows.Count - 1
            If Me.datasource.Rows(num) Is r Then Exit For
        Next
        If num = Me.datasource.Rows.Count Then Return False
        num = num + 1
        If Me.selecionada(num) Then Return True
        Me._linhas_sel = Me._linhas_sel & Me.ClientID & "_li" & num & ","
        Return True
    End Function

    Dim _salva_ds_session As Boolean = False
    Public Property SALVA_DS_SESSION() As Boolean
        Get
            Return Me._salva_ds_session
        End Get
        Set(ByVal Value As Boolean)
            Me._salva_ds_session = Value
        End Set
    End Property


    Protected Sub RenderTabela(ByRef writer As Web.UI.HtmlTextWriter, ByVal largura As Integer)
        Me.Style("WIDTH") = largura
        Me.Width = Web.UI.WebControls.Unit.Pixel(largura)
        Me.Style("POSITION") = "relative"
        Me.Style("LEFT") = "0"
        Me.Style("TOP") = "0"
        Me.Style("table-layout") = "fixed"
        Me.Height = WebControls.Unit.Empty
        Me.Style("height") = ""
        Me.RenderBeginTag(writer)
    End Sub

    Public Sub centraliza()
        Me.estilo_coluna("", "+text-align: center", "+text-align: center", -1, "", "", "", "")
    End Sub

    Public Sub direita(ByVal titulo_tambem As Boolean, ByVal margem_direita As Integer)
        Me.estilo_coluna(IIf(titulo_tambem, "+text-align: right; padding-right: " & margem_direita & "px", ""), _
             "+text-align: right; padding-right: " & margem_direita & "px", "+text-align: right; padding-right: " & margem_direita & "px", -1, "", "", "", "")
    End Sub

End Class


'#####################################################################
'#### DESIGNER DA TABELA
'#####################################################################

Public Class listatabdesigner
    Inherits Web.UI.Design.ControlDesigner

    Public Sub New()
    End Sub


    Public Overrides Function GetDesignTimeHtml() As String
        Dim lt As listatab = CType(Me.Component, listatab)
        Dim pnl As New Web.UI.WebControls.Label()
        pnl.Width = lt.Width
        pnl.Height = lt.Height
        pnl.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
        pnl.Text = "NAO SE ESQUECA DE: 1) verificar o tamanho total da lista, 2) verificar se o titulo ou linhas podem ocupar mais que o previsto"

        Dim text As New IO.StringWriter()
        Dim writer As New Web.UI.HtmlTextWriter(text)
        pnl.RenderControl(writer)
        Return text.ToString

    End Function

    Public Overrides ReadOnly Property AllowResize() As Boolean
        Get
            Return True
        End Get
    End Property



End Class

