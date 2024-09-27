
Imports System.Data
Imports System.Data.SqlClient
Imports Santana.Seguranca


'Imports System.DirectoryServices
'Imports System.DirectoryServices.AccountManagement

Partial Class Logon
    Inherits SantanaPage
    'Inherits System.Web.UI.Page

    Dim AutenticacaoModo As Integer = 0


    'Private userSearchRoot As DirectoryEntry


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Clear()
        txtUsuario.Focus()
        If Not IsPostBack Then
            ' ContextoWeb.NewUserContext()
            CarregaAutenticacaoModo()

        End If
    End Sub


    Private Sub CarregaAutenticacaoModo()

        'Dim domainList As New StringCollection()
        'Dim root As System.DirectoryServices.DirectoryEntry
        'Dim dominio As System.DirectoryServices.DirectoryEntry
        'Dim intCount As Integer = 0
        Try
            'cboAutenticacaoModo.Items.Add(New ListItem("Banco de dados", intCount.ToString()))

            'root = New System.DirectoryServices.DirectoryEntry("WinNT:")

            'For Each dominio In root.Children
            '    intCount += 1
            '    cboAutenticacaoModo.Items.Add(New ListItem(dominio.Name, intCount.ToString()))
            'Next

            'If cboAutenticacaoModo.Items.Count > 1 Then
            '    cboAutenticacaoModo.SelectedValue = 1
            'End If

            AutenticacaoModo = 0

        Catch ex As Exception
            'Não tratar caso serviço de dominio esteja fora do ar, ficara apenas opção banco de dados
            ' Mensagem.Text = ""
        End Try


    End Sub


    Private Function AutenticaBancoDados(ByVal strUser As String, ByVal strPwd As String) As String
        Dim strNovasenha As String = ""
        Dim strValidacao As String = ""
        Dim strRetorno As String = ""

        Try

            'If strNovasenha = "9" Then
            '    strValidacao = Util.Senha.GeraHash(Trim(strPwd))
            '    GravarNovaSenha(Trim(strUser), strValidacao)
            'End If

            strRetorno = SetUserSessions(Trim(strUser), Trim(strPwd), True)

        Catch ex As Exception
            strRetorno = "Falha na autenticação."
        Finally
            GC.Collect()
        End Try

        Return strRetorno

    End Function




    'Private Function AutenticaLDAP(ByVal strUser As String, ByVal strPwd As String, ByVal strDomain As String) As String
    '    Dim strUsuario As String = ""
    '    Dim strRetorno As String = ""

    '    Try

    '        Dim de As New DirectoryEntry("LDAP://" + Trim(strDomain), Trim(strUser), Trim(strPwd), AuthenticationTypes.Secure)
    '        Try
    '            Dim ds As DirectorySearcher = New DirectorySearcher(de)
    '            ds.FindOne()

    '            strRetorno = SetUserSessions(Trim(strUser), Trim(strPwd), False)

    '        Catch
    '            strRetorno = "Usuário ou Senha Inválidos"
    '        End Try

    '    Catch ex As Exception
    '        strRetorno = "Falha na autenticação"
    '    Finally
    '        GC.Collect()
    '    End Try

    '    Return strRetorno
    'End Function

    Private Function SetUserSessions(ByVal strUser As String, ByVal strPwd As String, ByVal AutenticacaoModoBD As Boolean) As String

        Dim strRetorno As String = ""

        Try
            ValidarDireitos(strUser, strPwd, AutenticacaoModoBD)
            LoadMenuItens()

            If IsNothing(ContextoWeb.UsuarioLogado) Or ContextoWeb.UsuarioLogado.SingIn = False Then
                strRetorno = "Por favor, verifique seu usuário e senha."
            ElseIf ContextoWeb.UsuarioLogado.Ativo = 0 Then
                strRetorno = "Verifique seu usuário, ele esta inativo."
                'Else
                '   ContextoWeb.UpdateUser()
            ElseIf Not ContextoWeb.DadosMenu.ListMenu.Any(Function(x) x.Perfil.Contains(ContextoWeb.UsuarioLogado.Perfil)) Then
                strRetorno = "Perfil não encontrado para este usuário."
            End If

        Catch ex As SqlException
            strRetorno = "Falha na comunicação com o banco de dados."
        Catch ex As Exception
            strRetorno = "Falha na autenticação."
        Finally
            GC.Collect()
        End Try

        Return strRetorno
    End Function


    Private Sub ValidarDireitos(ByVal strUsuario As String, ByVal strPwd As String, ByVal AutenticacaoModoBD As Boolean)
        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim con As New SqlConnection(strConn)
        Dim Vsenha As String = ""
        Dim Validacao As String = ""
        Dim cmd As New SqlCommand(
                    "Select top 1 Login, NomeUsuario, Funcao,codGerente as codGerente, convert(int,CodFilial) as CodFilial, Cpf, EMail, Ativo, NomeCompleto, senha, perfil, Produto from usuario (nolock) where Login='" & strUsuario & "'", con)
        '        "Select top 1 Login, NomeUsuario, Funcao, convert(int,codGerente) as codGerente, convert(int,CodFilial) as CodFilial, Cpf, EMail, Ativo, NomeCompleto, senha, perfil, Produto from usuario (nolock) where Login='" & strUsuario & "'", con)
        cmd.Connection.Open()

        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)


        While dr.Read()
            ' se usuario ativo
            'If dr.GetInt32(1) = 1 Then

            Vsenha = Trim(dr.GetString(9))
            Validacao = Util.Senha.GeraHash(Trim(strPwd))
            ' se a senha esta correta
            If (AutenticacaoModoBD = True And Validacao = Vsenha) Or AutenticacaoModoBD = False Then

                ' ContextoWeb.NewUserContext()
                ContextoWeb.UsuarioLogado.Login = dr.GetString(0)
                ContextoWeb.UsuarioLogado.NomeUsuario = dr.GetString(1)
                ContextoWeb.UsuarioLogado.Funcao = dr.GetString(2)
                ContextoWeb.UsuarioLogado.codGerente = dr.GetString(3)  ' alterado fev/16 por Raquel
                ContextoWeb.UsuarioLogado.CodFilial = dr.GetInt32(4)
                ContextoWeb.UsuarioLogado.Cpf = dr.GetString(5)
                ContextoWeb.UsuarioLogado.EMail = dr.GetString(6)
                ContextoWeb.UsuarioLogado.Ativo = dr.GetInt32(7)
                ContextoWeb.UsuarioLogado.NomeCompleto = dr.GetString(8)
                ContextoWeb.UsuarioLogado.Perfil = dr.GetInt32(10)
                ContextoWeb.UsuarioLogado.Produto = dr.GetInt32(11)
                ContextoWeb.UsuarioLogado.SingIn = True

            End If

        End While

        dr.Close()
        con.Close()

    End Sub

    'Private Function GravarNovaSenha(ByVal Usuario As String, ByVal Senha As String) As String
    '    Dim strConn As String = ConfigurationManager.AppSettings("BancoDados")
    '    Dim conexao As SqlConnection
    '    Dim comando As SqlCommand
    '    conexao = New SqlConnection(strConn)
    '    conexao.Open()
    '    comando = New SqlCommand("update usuario set senha='" & Senha & "' where Login='" & Usuario & "'", conexao)
    '    comando.ExecuteNonQuery()
    '    conexao.Close()
    '    Return "A"
    'End Function

    '  Protected Sub Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cancelar.Click
    'senha.Enabled = True
    'usuario.Text = ""
    'senha.Text = ""
    'Mensagem.Text = ""
    'usuario.Focus()
    ' End Sub


    'Private Function GravarSenhas(ByVal Usuario As String, ByVal senha As String) As String

    '    Dim conexao As SqlConnection
    '    Dim comando As SqlCommand

    '    conexao = New SqlConnection("server=localhost;uid=sa;pwd=senha;database=TesteDB")

    '    conexao.Open()
    '    comando = New SqlCommand("Insert Teste ( coluna ) Values ( 'Teste' )", conexao)
    '    comando.ExecuteNonQuery()
    '    conexao.Close()
    '    Return "a"
    'End Function






    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Dim strMensagem As String = ""

        Try

            If txtUsuario.Text.Trim() = "" Or txtSenha.Text.Trim() = "" Then
                strMensagem = "Por favor, verifique seu usuário e senha."
            Else

                If AutenticacaoModo = 0 Then
                    strMensagem = AutenticaBancoDados(Trim(txtUsuario.Text), Trim(txtSenha.Text))
                    'Else
                    '    strMensagem = AutenticaLDAP(Trim(usuario.Text), Trim(senha.Text), cboAutenticacaoModo.SelectedItem.Text)

                End If
            End If

        Catch ex As Exception
            strMensagem = "Falha na autenticação."
        Finally
            GC.Collect()
        End Try



        If strMensagem <> "" Then

            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Existe Algo Errado!' ,'" + strMensagem + "');", True)

            txtUsuario.Text = ""
            txtSenha.Text = ""
            txtUsuario.Focus()
        Else
            Response.Redirect("Menu.aspx", False)
        End If
    End Sub


    Protected Sub txtAlterarSenha_Click(sender As Object, e As EventArgs)
        HttpContext.Current.Session("UsuarioInformado") = txtUsuario.Text
        Response.Redirect("AlteraSenha.aspx", False)
    End Sub



    Private Sub LoadMenuItens()



        'MENU, liberação de menus
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Parametros", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("ParametrosGerencial", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Cobranca", New List(Of Integer)({0, 1, 5})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("RiscoGerencial", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Relatorios", New List(Of Integer)({0, 1, 5})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Graficos", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("RiscoCredito", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("P123", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("ComercialCredito", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Comercial", New List(Of Integer)({0, 1, 3, 4, 6, 7})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Credito", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Gerencial", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("CETIP", New List(Of Integer)({1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("ParametrosAR", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("ParametrosPT", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("ParametrosProtesto", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("PJ", New List(Of Integer)({1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Protesto", New List(Of Integer)({0, 1})))




        'PAGINAS, liberação de url

        'principal
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Menu.aspx", New List(Of Integer)({0, 1, 3, 4, 5, 6, 7})))
        'Parametros
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/PesoIp.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Usuarios.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Cobradora.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Produto.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Cubo.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/TaxCaptacao.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Comissao.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/SalarioMinimo.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/FaixaAnoSpread.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/FaixaAnoSafra.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/TipoProduto.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Modalidade.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Modelo.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/FaixaDePlano.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/Contas.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/ContaCalculo.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/Relatorios.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/RelatoriosItens.aspx", New List(Of Integer)({0, 1})))

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Comissao PJ/MetaOperadores.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Comissao PJ/TcOperacaoXMeta.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Comissao PJ/TaxaComissãoXMeta.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Comissao PJ/TxReduz.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Comissao PJ/Desconto.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Comissao PJ/OperadorAtendeCliente.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Comissao PJ/ClienteComissao.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Comissao PJ/ClienteEntrada.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Comissao PJ/Fixo.aspx", New List(Of Integer)({0, 1})))

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Comissao PJ/ComissaoPJ.aspx", New List(Of Integer)({0, 1, 6})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Comissao PJ/ComissaoPJAnalitico.aspx", New List(Of Integer)({0, 1})))

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/AR/Segmento.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/AR/NumeroCarta.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/AR/Retorno.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/AR/Motivo.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/AR/DeParaOcorrenciasAR.aspx", New List(Of Integer)({0, 1})))

        'PT
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/PT/Regiao.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/PT/Grupo.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/PT/UF.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/PT/Cidade.aspx", New List(Of Integer)({0, 1})))

        'Protesto
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Protesto/OcorrenciasCartorio.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Protesto/DeParaOcorrencias.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Protesto/ContratosExcecao.aspx", New List(Of Integer)({0, 1})))


        'PJ
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/PJ/AssinaDigital.aspx", New List(Of Integer)({1})))


        'Cobrança
        'Cobrança Relatório
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/RollrateMensal.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/DETALHECONTRATOS.aspx", New List(Of Integer)({0, 1})))

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/RollrateDiario.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/DETALHECONTRATOSDIARIO.aspx", New List(Of Integer)({0, 1})))

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/IndicePerformance.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/IndicePerformanceVR.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/IPSafraDiaria.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/Relatorios/IPAnalitico.aspx", New List(Of Integer)({0, 1})))

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/P123Gerencial/P123Gerencial.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/P123Gerencial/Inadimplencia.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/P123Gerencial/Risco.aspx", New List(Of Integer)({0, 1})))


        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/UraGerarArquivo.aspx", New List(Of Integer)({0, 1, 5})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Carteira3M.aspx", New List(Of Integer)({0, 1})))
        'Cobrança Grafico
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Graficos/GraficoCarteira.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Graficos/Rolagem.aspx", New List(Of Integer)({0, 1}))) 'opção 1 e 2
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Graficos/Estoque.aspx", New List(Of Integer)({0, 1})))

        'Protesto
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Protesto/GerarRemessa.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Protesto/CargaConfirmacao.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Protesto/CargaRetorno.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Protesto/GerarCapturaGV.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Protesto/Protesto.aspx", New List(Of Integer)({0, 1})))


        'ARs
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/Relatorios/ARs.aspx", New List(Of Integer)({0, 1})))


        'Risco e Crédito
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/RRMensalCUBO.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/pdd_gerencial.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Reneg_geral.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/baseCred.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/SPREAD.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/SPREADanalitico.aspx", New List(Of Integer)({0, 1})))
        'RiscoCredito P123
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Graficos/P123Analise.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Graficos/P123Produto.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/P123Agente.aspx", New List(Of Integer)({0, 1})))

        'ComercialCredito
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/FechaComercial/FechaComCreOpRealizadas.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/FechaComercial/FechaComCreProducaoProdutos.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/FechaComCreAnaliseProd.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/FechaComPropostasOp.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/FechaComPropostasHis.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/FechaComTipoVeiculo.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/FechaComAnaliseVeiculoPlano.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/FechaComAnaliseVeiculoAno.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/FechaComAnaliseVeiculoTipoAno.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/FechaComClientes.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/FechaComScoreMedio.aspx", New List(Of Integer)({0, 1, 3})))

        'Comercial
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Comercial/MapaComercial.aspx", New List(Of Integer)({0, 1, 3, 4})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Comercial/MapaPDV.aspx", New List(Of Integer)({0, 1, 3, 4, 6})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/MapaPDVveicLOJA.aspx", New List(Of Integer)({0, 1, 3, 4})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/PontecialMktShare.aspx", New List(Of Integer)({0, 1, 3, 4})))

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cetip/CETIPCruzamento.aspx", New List(Of Integer)({0, 1, 3, 4})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cetip/CetipPotencialAnaliticoConsulta.aspx", New List(Of Integer)({0, 1, 3, 4, 7})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cetip/CetipPotencialSinteticoConsulta.aspx", New List(Of Integer)({0, 1, 3, 4, 7})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cetip/CetipRankingConsulta.aspx", New List(Of Integer)({0, 1, 3, 4, 7})))

        'Crédito
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Credito/AnaliseAnalista.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/ProducaoAnalista.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/InadinplenciaPorAnalista.aspx", New List(Of Integer)({0, 1})))

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/SinteticoAnalise.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/SinteticoInadimpl.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/Excecao.aspx", New List(Of Integer)({0, 1})))


        'Gerencial
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/Balancete.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/Cubo.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/PadraoTrezeMeses.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/PadraoDozeMeses.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/Balanco.aspx", New List(Of Integer)({0, 1})))

        'CETIP
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cetip/CetipUpload.aspx", New List(Of Integer)({1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cetip/CetipGerarArquivo.aspx", New List(Of Integer)({1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cetip/CetipPotencialAnalitico.aspx", New List(Of Integer)({1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cetip/CetipPotencialSintetico.aspx", New List(Of Integer)({1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cetip/CetipRanking.aspx", New List(Of Integer)({1})))



    End Sub


End Class
