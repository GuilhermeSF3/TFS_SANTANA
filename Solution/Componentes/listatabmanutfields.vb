Public Class listatabmanutfields
    Inherits listatabmanut
    Implements javascripts

    Public Enum TIPO_CAMPO
        TEXTBOX = 0
        NUMERO = 1
        DATA = 2
        COMBO = 3
        CHECKBOX = 4
        DATA_HORA = 5
    End Enum

    Public Sub seta_ds_combo(ByVal coluna As String, ByVal ds As DataTable, ByVal coluna_codigo As String, ByVal coluna_texto As String)
        Dim i As Integer
        For i = 0 To Me._colunas.Count - 1
            If Me._colunas(i)(listatab.PTIPO_CAMPO) = TIPO_CAMPO.COMBO And _
               Me._colunas(i)(listatab.PDETALHE) = coluna Then
                Me._colunas(i)(listatab.PSINTAXE) = Me.retorna_sintaxe_combo(coluna, ds, Me._colunas(i)(listatab.PTITULO), coluna_texto, coluna_codigo)
                Return
            End If
        Next

    End Sub


    Public Function maiscampo(ByVal t As TIPO_CAMPO, ByVal titulo As String, ByVal largura As String, ByVal coluna As String, _
    ByVal coluna_ind_habilitado As String, ByVal formato As String, _
    ByVal ds As DataTable, ByVal tamanho As Integer, ByVal coluna_codigo As String, _
    ByVal coluna_texto As String, ByVal obrigatorio As Boolean, _
    Optional ByVal col_ind_visivel As String = "", _
    Optional ByVal col_texto_alt As String = "", _
    Optional ByVal ind_texto_alt_fixo As Boolean = True, _
        Optional ByVal col_ind_visivel_hora As String = "") As Boolean
        Me.maiscoluna(listatab.TIPO_COLUNA.TIPO_CAMPO, False, titulo, largura, coluna, False, _
         "", True, listatab.TIPO_TARGET.TIPO_SELF, "", Nothing, formato, _
          col_ind_visivel, col_texto_alt, ind_texto_alt_fixo)

        Me._colunas(Me._colunas.Count - 1)(listatab.PTIPO_CAMPO) = t
        Me._colunas(Me._colunas.Count - 1)(listatab.PCOLUNA_DEFINE_HABILITADO) = coluna_ind_habilitado
        Me._colunas(Me._colunas.Count - 1)(listatab.PTAMANHO) = tamanho
        Me._colunas(Me._colunas.Count - 1)(listatab.POBRIGATORIO) = obrigatorio
        Me._colunas(Me._colunas.Count - 1)(listatab.PCOLUNA_DEFINE_VISIVEL_HORA) = col_ind_visivel_hora


        If t = TIPO_CAMPO.COMBO Then
            Me._colunas(Me._colunas.Count - 1)(listatab.PSINTAXE) = Me.retorna_sintaxe_combo(coluna, ds, titulo, coluna_texto, coluna_codigo)

        ElseIf t = TIPO_CAMPO.TEXTBOX Then
            Dim tx As New textbox()
            tx.Control_Init(Nothing, Nothing)
            tx.MaxLength = tamanho
            tx.Width = New System.Web.UI.WebControls.Unit((CLng(largura) - 10))
            tx.ID = Me.ClientID & "_c" & (Me._colunas.Count - 1) & "_rowXXXX_"
            tx.Text = "{" & coluna & "}"

            Dim text As New IO.StringWriter()
            Dim writer As New Web.UI.HtmlTextWriter(text)
            tx.RenderControl(writer)
            Me._colunas(Me._colunas.Count - 1)(listatab.PSINTAXE) = text.ToString()

        ElseIf t = TIPO_CAMPO.NUMERO Then
            Dim nu As New numero()
            nu.Control_Init(Nothing, Nothing)
            nu.TAMANHO_INTEIRO = IIf(formato.IndexOf(".") >= 0, formato.IndexOf("."), formato.Length)
            nu.TAMANHO_DECIMAL = IIf(formato.IndexOf(".") >= 0, formato.Length - formato.IndexOf(".") - 1, 0)
            'nu.Width = New System.Web.UI.WebControls.Unit((CLng(largura) - 10))
            nu.ID = Me.ClientID & "_c" & (Me._colunas.Count - 1) & "_rowXXXX_"
            nu.Text = "{#VALOR#}"
            Dim text As New IO.StringWriter()
            Dim writer As New Web.UI.HtmlTextWriter(text)
            nu.RenderControl(writer)
            Me._colunas(Me._colunas.Count - 1)(listatab.PSINTAXE) = text.ToString()

        ElseIf t = TIPO_CAMPO.DATA Then
            Dim dt As New datahora()
            dt.Control_Init(Nothing, Nothing)
            dt.FORMATO = IIf(formato.Length = 10, datahora._formato.DiaMesAno, IIf(formato.Length = 7, datahora._formato.MesAno, datahora._formato.Ano))
            dt.ID = Me.ClientID & "_c" & (Me._colunas.Count - 1) & "_rowXXXX_"
            'dt.Text = "{#VALOR#}"
            dt.awa_preenche(Date.Today)
            dt.MASK_SIMPLES = True

            Dim text As New IO.StringWriter()
            Dim writer As New Web.UI.HtmlTextWriter(text)
            dt.RenderControl(writer)
            Dim st As String = text.ToString()

            Me._colunas(Me._colunas.Count - 1)(listatab.PSINTAXE) = st.Replace(Date.Today.ToString("dd/MM/yyyy"), "{#VALOR#}")
        ElseIf t = TIPO_CAMPO.DATA_HORA Then
            Dim dt As New datahora()
            dt.Control_Init(Nothing, Nothing)
            dt.FORMATO = datahora._formato.DiaMesAno

            dt.ID = Me.ClientID & "_c" & (Me._colunas.Count - 1) & "_rowXXXX_"
            'dt.Text = "{#VALOR#}"
            dt.awa_preenche(Date.Today)
            dt.MASK_SIMPLES = True

            Dim text As New IO.StringWriter()
            Dim writer As New Web.UI.HtmlTextWriter(text)
            dt.RenderControl(writer)
            Dim st As String = text.ToString()

            Me._colunas(Me._colunas.Count - 1)(listatab.PSINTAXE) = st.Replace(Date.Today.ToString("dd/MM/yyyy"), "{#VALOR#}")

            '            Dim h As New hora()
            '            h.Control_Init(Nothing, Nothing)
            '            h.FORMATO_HORA = hora._formato_hora.HoraMinuto
            '            h.ID = Me.ClientID & "_h" & (Me._colunas.Count - 1) & "_rowXXXX_"
            '            Dim hor As String = DateTime.Now.ToString("HH:mm")
            '           h.awa_preenche(hor)
            '           h.MASK_SIMPLES = True

            '         Dim text1 As New IO.StringWriter()
            '         Dim writer1 As New Web.UI.HtmlTextWriter(text1)
            '          h.RenderControl(writer1)
            '            Dim st1 As String = text1.ToString()
            '            Me._colunas(Me._colunas.Count - 1)(listatab.PSINTAXE) &= "&nbsp;" & st1.Replace(hor, "{#VALORHORA#}")

        ElseIf t = TIPO_CAMPO.CHECKBOX Then
            Dim cb As New checkbox()
            cb.ID = Me.ClientID & "_c" & (Me._colunas.Count - 1) & "_rowXXXX_"
            cb.awa_preenche("S")
            Dim text As New IO.StringWriter()
            Dim writer As New Web.UI.HtmlTextWriter(text)
            cb.checkbox_Init(Nothing, Nothing)
            cb.RenderControl(writer)
            Dim st As String = text.ToString()
            st = st.Replace("CHECKED", "{#VALOR#}")
            st = st.Replace("""S""", "{#VALORSN#}")
            Me._colunas(Me._colunas.Count - 1)(listatab.PSINTAXE) = st.Replace("color", "color1")

        End If
    End Function

    Dim conta_lin = 0

    Protected Overrides Function processa_coluna(ByVal d() As Object, ByVal l As System.Data.DataRow) As String
        '## verifica o tipo
        '## se fr campo, verifica a coluna de habilitacao
        '## se for habailitada, cria o campo de retorno e renderiza-o usando um writer
        '## 

        Dim dd(d.Length) As Object
        Array.Copy(d, dd, d.Length)

        Dim tipo_ini As TIPO_COLUNA
        tipo_ini = dd(listatab.PTIPO)
        If dd(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_CAMPO Then
            If dd(listatab.PCOLUNA_DEFINE_HABILITADO) <> "" Then
                If l(dd(listatab.PCOLUNA_DEFINE_HABILITADO)) = "N" Then
                    If dd(listatab.PTIPO_CAMPO) = TIPO_CAMPO.TEXTBOX Then
                        dd(listatab.PSINTAXE) = "{" & dd(listatab.PDETALHE) & "}"
                    ElseIf dd(listatab.PTIPO_CAMPO) = TIPO_CAMPO.NUMERO Or dd(listatab.PTIPO_CAMPO) = TIPO_CAMPO.DATA Then
                        dd(listatab.PSINTAXE) = "{0:" & dd(listatab.PFORMATO) & "}"
                    ElseIf dd(listatab.PTIPO_CAMPO) = TIPO_CAMPO.DATA_HORA Then
                        If dd(listatab.PCOLUNA_DEFINE_VISIVEL_HORA) <> "" Then
                            dd(listatab.PSINTAXE) = "{0:" & dd(listatab.PFORMATO) & "}"

                            If l(dd(listatab.PCOLUNA_DEFINE_VISIVEL_HORA)).toupper = "N" Then
                                dd(listatab.PSINTAXE) = "{0:" & Left(dd(listatab.PFORMATO), 10) & "}"

                            End If
                        End If
                    End If
                End If
            End If
        End If
        Dim sint As String = dd(listatab.PSINTAXE)
        If dd(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_CAMPO And dd(listatab.PTIPO_CAMPO) = TIPO_CAMPO.NUMERO Then
            dd(listatab.PTIPO) = TIPO_COLUNA.TIPO_NUMERO
            dd(listatab.PSINTAXE) = "{0:" & dd(listatab.PFORMATO) & "}"
        ElseIf dd(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_CAMPO And dd(listatab.PTIPO_CAMPO) = TIPO_CAMPO.DATA Then
            dd(listatab.PTIPO) = TIPO_COLUNA.TIPO_DATA
            dd(listatab.PSINTAXE) = "{0:" & dd(listatab.PFORMATO) & "}"
        ElseIf dd(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_CAMPO And dd(listatab.PTIPO_CAMPO) = TIPO_CAMPO.DATA_HORA Then
            dd(listatab.PTIPO) = TIPO_COLUNA.TIPO_DATAHORA
            dd(listatab.PSINTAXE) = "{0:" & dd(listatab.PFORMATO) & "}"
        ElseIf dd(listatab.PTIPO) = listatab.TIPO_COLUNA.TIPO_CAMPO And dd(listatab.PTIPO_CAMPO) = TIPO_CAMPO.CHECKBOX Then
            dd(listatab.PTIPO) = TIPO_COLUNA.TIPO_STRING
            dd(listatab.PSINTAXE) = "{" & dd(listatab.PDETALHE) & "}"
        End If


        Dim rend As String = MyBase.processa_coluna(dd, l)
        If tipo_ini = listatab.TIPO_COLUNA.TIPO_CAMPO Then
            If dd(listatab.PCOLUNA_DEFINE_VISIVEL) <> "" Then
                If l(dd(listatab.PCOLUNA_DEFINE_VISIVEL)) = "N" Then
                    sint = ""
                End If
            End If
            If dd(listatab.PTIPO_CAMPO) = TIPO_CAMPO.NUMERO Or _
                                                 dd(listatab.PTIPO_CAMPO) = TIPO_CAMPO.DATA Or _
                                                 dd(listatab.PTIPO_CAMPO) = TIPO_CAMPO.DATA_HORA Then
                If sint.IndexOf("{#VALOR#}") >= 0 Then
                    If dd(listatab.PTIPO_CAMPO) = TIPO_CAMPO.DATA_HORA Then

                        '## se tiver que ficar oculto, retira tudo de hora
                        If dd(listatab.PCOLUNA_DEFINE_VISIVEL_HORA) <> "" Then
                            If l(dd(listatab.PCOLUNA_DEFINE_VISIVEL_HORA)).toupper = "N" Then
                                sint = Left(sint, sint.IndexOf("&nbsp;"))
                            End If
                        End If

                        Dim hor As String = "00:00"
                        If rend.Length >= 16 Then hor = rend.Substring(11, 5)
                        rend = sint.Replace("{#VALOR#}", Left(rend, 10))
                        rend = rend.Replace("{#VALORHORA#}", hor)
                    Else
                        rend = sint.Replace("{#VALOR#}", rend)
                    End If
                End If

            ElseIf dd(listatab.PTIPO_CAMPO) = TIPO_CAMPO.CHECKBOX Then
                rend = rend.ToUpper
                rend = sint.Replace("{#VALOR#}", IIf(rend = "S", "checked", "")).Replace("{#VALORSN#}", IIf(rend = "S", "S", "N"))
                If dd(listatab.PCOLUNA_DEFINE_HABILITADO) <> "" Then
                    If l(dd(listatab.PCOLUNA_DEFINE_HABILITADO)).toupper = "N" Then
                        rend = rend.Replace("style=""", "disabled style=""")
                    End If
                End If
            End If
            rend = rend.Replace("_rowXXXX_", "r" & conta_lin)
        End If
        conta_lin = conta_lin + 1
        Return rend
    End Function

    '## se a linha foi alterada, seta-a como alterada e altera o datasource



    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        writer.WriteLine("<script language=javascript> ")
        writer.WriteLine("function " & Me.ClientID & "_seleciona_combo(id,valor) {")
        writer.WriteLine("var obj = document.getElementById(id);")
        writer.WriteLine(" if (obj == null) return;")
        writer.WriteLine(" for(i = 0; i < obj.length; i++) {")
        writer.WriteLine(" if (obj.options(i).value == valor) {")
        writer.WriteLine(" obj.options(obj.selectedIndex).selected = false;")
        writer.WriteLine(" obj.options(i).selected = true;")
        writer.WriteLine(" return; } } }")
        writer.WriteLine(" </script>")
        MyBase.Render(writer)


    End Sub

    Protected Overrides Sub CreateChildControls()
        Me.LISTA_SELECAO = False
        MyBase.CreateChildControls()
    End Sub

    Private Sub listatabmanutfields_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.Page.IsPostBack Then
            '## captura os valores das colunas digitadas
            Dim i As Integer = 0
            Dim l As Integer = 0
            Dim nu As New numero()
            Dim dt As New datahora()
            '            Dim h As New hora()
            Dim cb As New checkbox()
            Dim r, nr As DataRow
            Dim valor, formato As String
            Dim valida_linha As Boolean

            For Each r In Me.datasource.Rows
                valida_linha = True
                nr = Me.datasource.NewRow
                nr.ItemArray = r.ItemArray

                For i = 0 To Me._colunas.Count - 1
                    If Me._colunas(i)(listatab.PTIPO) = listatabmanutfields.TIPO_COLUNA.TIPO_CAMPO Then
                        valor = Web.HttpContext.Current.Request.Form(Me.ClientID & "_c" & i & "r" & l)

                        If Me._colunas(i)(listatab.PTIPO_CAMPO) = listatabmanutfields.TIPO_CAMPO.TEXTBOX Then
                            If Me._colunas(i)(listatab.POBRIGATORIO) Then
                                If valor.Trim = "" Then
                                    valida_linha = False
                                Else
                                    nr(Me._colunas(i)(listatab.PDETALHE)) = valor
                                End If
                            Else
                                nr(Me._colunas(i)(listatab.PDETALHE)) = valor
                            End If
                        ElseIf Me._colunas(i)(listatab.PTIPO_CAMPO) = listatabmanutfields.TIPO_CAMPO.NUMERO Then
                            formato = Me._colunas(i)(listatab.PFORMATO)
                            nu.TAMANHO_INTEIRO = IIf(formato.IndexOf(".") >= 0, formato.IndexOf("."), formato.Length)
                            nu.TAMANHO_DECIMAL = IIf(formato.IndexOf(".") >= 0, formato.Length - formato.IndexOf(".") - 1, 0)

                            nu.OBRIGATORIO = Me._colunas(i)(listatab.POBRIGATORIO)
                            nu.Text = valor
                            If nu.awa_valida() <> "" Then
                                valida_linha = False
                            Else
                                nr(Me._colunas(i)(listatab.PDETALHE)) = nu.VALOR
                            End If

                        ElseIf Me._colunas(i)(listatab.PTIPO_CAMPO) = listatabmanutfields.TIPO_CAMPO.DATA Then
                            formato = Me._colunas(i)(listatab.PFORMATO)
                            dt.FORMATO = IIf(formato.Length = 10, datahora._formato.DiaMesAno, IIf(formato.Length = 7, datahora._formato.MesAno, datahora._formato.Ano))
                            dt.OBRIGATORIO = Me._colunas(i)(listatab.POBRIGATORIO)
                            dt.Text = valor
                            If dt.awa_valida() <> "" Then
                                valida_linha = False
                            Else
                                nr(Me._colunas(i)(listatab.PDETALHE)) = dt.VALOR
                            End If
                        ElseIf Me._colunas(i)(listatab.PTIPO_CAMPO) = listatabmanutfields.TIPO_CAMPO.DATA_HORA Then
                            formato = Me._colunas(i)(listatab.PFORMATO)
                            dt.FORMATO = datahora._formato.DiaMesAno
                            dt.OBRIGATORIO = Me._colunas(i)(listatab.POBRIGATORIO)
                            dt.Text = valor
                            If dt.awa_valida() <> "" Then
                                valida_linha = False
                            Else
                                nr(Me._colunas(i)(listatab.PDETALHE)) = dt.VALOR
                            End If
                            If valida_linha And valor <> "" Then
                                '                                h.FORMATO_HORA = hora._formato_hora.HoraMinuto
                                '                                h.OBRIGATORIO = Me._colunas(i)(listatab.POBRIGATORIO)
                                '                                h.Text = Web.HttpContext.Current.Request.Form(Me.ClientID & "_h" & i & "r" & l)
                                '                                If h.awa_valida() <> "" Then
                                '                                    valida_linha = False
                                '                                Else
                                '                                    nr(Me._colunas(i)(listatab.PDETALHE)) = dt.VALOR.AddHours(h.VALOR.Hour).AddMinutes(h.VALOR.Minute)
                                '                                End If
                            End If

                        ElseIf Me._colunas(i)(listatab.PTIPO_CAMPO) = listatabmanutfields.TIPO_CAMPO.COMBO Then
                            If Me._colunas(i)(listatab.POBRIGATORIO) And (valor = "" Or valor = "0") Then
                                valida_linha = False
                            Else
                                nr(Me._colunas(i)(listatab.PDETALHE)) = valor
                            End If

                        ElseIf Me._colunas(i)(listatab.PTIPO_CAMPO) = listatabmanutfields.TIPO_CAMPO.CHECKBOX Then
                            valor = Web.HttpContext.Current.Request.Form("HI_" & Me.ClientID & "_c" & i & "r" & l)

                            cb.awa_preenche(IIf(valor Is Nothing, "N", valor))
                            nr(Me._colunas(i)(listatab.PDETALHE)) = cb.VALOR
                        End If
                    End If
                    l = l + 1
                Next
                If valida_linha Then
                    If valida_linha_lista(nr) Then
                        r.ItemArray = nr.ItemArray
                    End If
                End If
            Next


        End If
    End Sub

    Protected Overridable Function valida_linha_lista(ByVal r As DataRow) As Boolean
        Return True
    End Function

    Private Function retorna_sintaxe_combo(ByVal coluna As String, ByVal ds As DataTable, ByVal titulo As String, ByVal coluna_texto As String, _
    ByVal coluna_codigo As String) As String
        Me.inscreve_ds_tipo_datasource(titulo, ds)
        '## renderiza um componente combo
        Dim cb As New combo()
        cb.DataSource = ds
        cb.DataTextField = coluna_texto
        cb.DataValueField = coluna_codigo
        cb.ID = Me.ClientID & "_c" & (Me._colunas.Count - 1) & "_rowXXXX_"
        cb.DataBind()

        Dim text As New IO.StringWriter()
        Dim writer As New Web.UI.HtmlTextWriter(text)
        cb.RenderControl(writer)

        '## aplica um javascript que controla a selecao
        Dim scr_sel As String = ""
        scr_sel = "<script language=javascript> " & Microsoft.VisualBasic.ControlChars.CrLf
        scr_sel = scr_sel & Me.ClientID & "_seleciona_combo('" & cb.ID & "','{" & coluna & "}');" & Microsoft.VisualBasic.ControlChars.CrLf
        scr_sel = scr_sel & "</script>" & Microsoft.VisualBasic.ControlChars.CrLf

        Return text.ToString() & scr_sel

    End Function

    Public Property ARQUIVOS() As String() Implements Componentes.javascripts.ARQUIVOS
        Get
            '## devolve todos os javascripts dos quais a Aplicacao depende
            Dim str(0) As String
            str(0) = caminho_js & "REGEXP.js"
            Return str
        End Get
        Set(ByVal Value As String())

        End Set
    End Property

    Public Shared caminho_js As String
    Public Property CAMINHO_JAVASCRIPTS() As String Implements Componentes.javascripts.CAMINHO_JAVASCRIPTS
        Get
            Return caminho_js
        End Get
        Set(ByVal Value As String)
            caminho_js = Value
        End Set
    End Property

    Public Shared ReadOnly Property RETORNA_SCRIPT_JS() As String
        Get
            Return "<script src=""" & caminho_js & "REGEXP.js""></script>"
        End Get
    End Property


End Class
