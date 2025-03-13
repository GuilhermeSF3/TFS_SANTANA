
Imports System.Data
Imports System.Data.SqlClient
Imports Santana.Seguranca

Partial Class Logon
    Inherits SantanaPage

    Dim AutenticacaoModo As Integer = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Clear()
        txtUsuario.Focus()
        If Not IsPostBack Then
            CarregaAutenticacaoModo()

        End If
    End Sub

    Private Sub CarregaAutenticacaoModo()

        Try

            AutenticacaoModo = 0

        Catch ex As Exception

        End Try


    End Sub

    Private Function AutenticaBancoDados(ByVal strUser As String, ByVal strPwd As String) As String
        Dim strNovasenha As String = ""
        Dim strValidacao As String = ""
        Dim strRetorno As String = ""

        Try

            strRetorno = SetUserSessions(Trim(strUser), Trim(strPwd), True)

        Catch ex As Exception
            strRetorno = "Falha na autenticação."
        Finally
            GC.Collect()
        End Try

        Return strRetorno

    End Function

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
        cmd.Connection.Open()

        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)


        While dr.Read()

            Vsenha = Trim(dr.GetString(9))
            Validacao = Util.Senha.GeraHash(Trim(strPwd))
            If (AutenticacaoModoBD = True And Validacao = Vsenha) Or AutenticacaoModoBD = False Then

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
                ContextoWeb.UsuarioLogado.Produto = dr.GetInt32(11)   'int ex veic = 2
                ContextoWeb.UsuarioLogado.SingIn = True

            End If

        End While

        dr.Close()
        con.Close()

    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Dim strMensagem As String = ""

        Try

            If txtUsuario.Text.Trim() = "" Or txtSenha.Text.Trim() = "" Then
                strMensagem = "Por favor, verifique seu usuário e senha."
            Else

                If AutenticacaoModo = 0 Then
                    strMensagem = AutenticaBancoDados(Trim(txtUsuario.Text), Trim(txtSenha.Text))

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

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Parametros", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("ParametrosGerencial", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Cobranca", New List(Of Integer)({0, 1, 5})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("ICM", New List(Of Integer)({0, 1, 5})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("FOP", New List(Of Integer)({0, 1, 5})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("CobrancaP123", New List(Of Integer)({0, 1, 3, 4, 6, 7, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("RiscoGerencial", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Relatorios", New List(Of Integer)({0, 1, 5})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Graficos", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("RiscoCredito", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("IP", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("P123", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("P123Microcredito", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("RiscoCreditoInd", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("SpreadAnalitico", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Spread", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("ComercialCredito", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Comercial", New List(Of Integer)({0, 1, 3, 4, 6, 7, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Credito", New List(Of Integer)({0, 1, 6})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Gerencial", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("CETIP", New List(Of Integer)({1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("ParametrosAR", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("ParametrosPT", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("ParametrosProtesto", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("MetaFormaliz", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Gravame", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("PJ", New List(Of Integer)({1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Protesto", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("ConferePagamento", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("GravIncQui", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("PendCrvCartAtiva", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("BankFacil", New List(Of Integer)({0, 1, 9})))  ' 9= BKF
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Financeiro", New List(Of Integer)({0, 1, 11})))   ' 11= FINANC
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("RH", New List(Of Integer)({0, 12})))   ' 12= RH

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("TI", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("DCO", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Desconto", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Cadastro", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("Fechamento", New List(Of Integer)({0, 1})))

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Menu.aspx", New List(Of Integer)({0, 1, 3, 4, 5, 6, 7, 8, 9, 11, 12})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/UploadArquivoExcel.aspx", New List(Of Integer)({0, 1})))
        'Parametros
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/PesoIp.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Usuarios.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/CadastroGrupoPJ.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/", New List(Of Integer)({0, 1})))
        'nova tela de cadastro services out/17
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/usu_services.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/usu_Operador_SVC.aspx", New List(Of Integer)({0, 1})))

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Cobradora.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/MetaPDD.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/MetaICM.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/MetaICMRepac.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/MetaBNDU.aspx", New List(Of Integer)({0, 1})))
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
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Risco.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/ComissaoDesconto.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/RcFaixaScore.aspx", New List(Of Integer)({0, 1})))

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/Feriados.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/DeParaOperadoresFuncaoNF.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/Contas.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/ContaCalculo.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/Relatorios.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/RelatoriosItens.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Desconto/Onboarding.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Desconto/Assertiva.aspx", New List(Of Integer)({0, 1})))

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

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/MetaFormaliz.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/MetaPromotoras.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/MetaOperadoresInternos.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/DeParaAgentes.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/DeParaGerencia.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/RegiaoCadastro.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/DeParaRegiao.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/EquipeDinamo.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/CadastroEquipe.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/OperadorSubstabelecido.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/PromotorasAdiantamento.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/EmpresaRegistro.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/GravameInclusao.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/GravameQuitacao.aspx", New List(Of Integer)({0, 1})))


        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Score.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/DataBase.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/AlteracaoGarantia.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/IndexadorFIPE.aspx", New List(Of Integer)({0, 1})))

        'Cobrança
        'Cobrança Relatório
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/RollrateMensal.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/DETALHECONTRATOS.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/RollrateProjetadoAnalitico.aspx", New List(Of Integer)({0, 1})))

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/RollrateDiario.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/DETALHECONTRATOSDIARIO.aspx", New List(Of Integer)({0, 1})))

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/IndicePerformance.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/IndicePerformanceVR.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/IPSafraDiaria.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/Relatorios/IPAnalitico.aspx", New List(Of Integer)({0, 1})))

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/Relatorios/BaseReneg.aspx", New List(Of Integer)({0, 1, 5})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/Relatorios/Ocorrencia.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/Relatorios/Repactuacao.aspx", New List(Of Integer)({0, 1})))

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/P123Gerencial/P123Gerencial.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/P123Gerencial/Inadimplencia.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/P123Gerencial/Risco.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/P123Gerencial/P123Gerencia13Meses.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/Relatorios/ParcelaPulada.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/Relatorios/RecebParcela.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/Relatorios/PosCobRenegRepac.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/Relatorios/PosCob.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/Relatorios/ParcelaVcto.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/Relatorios/ControleAcoes.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/Relatorios/ControleAcoesPJ.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/Relatorios/AjusteIP.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/Relatorios/Honorarios.aspx", New List(Of Integer)({0, 1})))

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/ICM/ForcarICM.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/FOP/ArquivoFOP.aspx", New List(Of Integer)({0, 1})))

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/UraGerarArquivo.aspx", New List(Of Integer)({0, 1, 5})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Carteira3M.aspx", New List(Of Integer)({0, 1})))
        'Cobrança Grafico
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Graficos/GraficoCarteira.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Graficos/Rolagem.aspx", New List(Of Integer)({0, 1}))) 'opção 1 e 2
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Graficos/Estoque.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/Consolidado/GraficoRolagemConsolidado.aspx", New List(Of Integer)({0, 1})))


        'Protesto
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Protesto/GerarRemessa.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Protesto/CargaConfirmacao.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Protesto/CargaRetorno.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Protesto/GerarCapturaGV.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Protesto/Protesto.aspx", New List(Of Integer)({0, 1})))

        'ARs
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cobranca/Relatorios/ARs.aspx", New List(Of Integer)({0, 1})))

        'Risco e Crédito
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/RRMensalCUBO.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/pdd_gerencial.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Reneg_geral.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/baseCred.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/SPREAD.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/SPREADDEsconto.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/SPREADanalitico.aspx", New List(Of Integer)({0, 1, 8}))) ' INCLUIDO ACESSO AGENTE 19/2/18
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/SPREADcapitaldegiro.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/SPREADanaliticoDesconto.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/RiscoCredito/PDDSafraTempo.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/RiscoCredito/Prestamista.aspx", New List(Of Integer)({0, 1, 8})))

        'RiscoCredito P123
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Graficos/P123Analise.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Graficos/P123Produto.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/P123Agente.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/P123Operador.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/p123_Analista.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/RiscoCreditoIndicador.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/RiscoCreditoSafra.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Graficos/P123AnaliseMicrocredito.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/P123OperadorMicrocredito.aspx", New List(Of Integer)({0, 1})))

        'ComercialCredito
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/FechaComercial/FechaComCreOpRealizadas.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/FechaComercial/FechaComCreProducaoProdutos.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/FechaComercial/FechaComCreProducaoProdutosOP.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/FechaComCreAnaliseProd.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/FechaComPropostasOp.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/OpRealizadasAnalitica.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/FechaComPropostasHis.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/FechaComTipoVeiculo.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/FechaComAnaliseVeiculoPlano.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/FechaComAnaliseVeiculoAno.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/FechaComAnaliseVeiculoTipoAno.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/FechaComClientes.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/FechaComScoreMedio.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/PagasRecebidas.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/Fipe.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/Comissao.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/ComissaoDinamo.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/ComissaoAdiantamento.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/GerarArquivoComissao.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/ComissaoSintetico.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Comercial/BasePropostaTempo.aspx", New List(Of Integer)({0, 1, 3})))
        
        'Comercial
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Comercial/MapaComercial.aspx", New List(Of Integer)({0, 1, 3, 4, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Comercial/MapaPDV.aspx", New List(Of Integer)({0, 1, 3, 4, 6})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/MapaPDVveicLOJA.aspx", New List(Of Integer)({0, 1, 3, 4, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/PontecialMktShare.aspx", New List(Of Integer)({0, 1, 3, 4, 8})))
        '-
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/PendenciasCRV.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/PendenciasCRVInativos.aspx", New List(Of Integer)({0, 1, 8})))
        'ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/PendenciasCRVPromotora.aspx", New List(Of Integer)({0, 1, 8})))
        'ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/PendenciasCRVEquipe.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/PendenciasCRVBaixa.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/PendenciasContrato.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/PendenciasContratoSint.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/PendenciasGravames.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/PendenciasGravameSint.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/PagtoPendencia.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/ContratoPgPendSint.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cetip/CETIPCruzamento.aspx", New List(Of Integer)({0, 1, 3, 4, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cetip/CETIPSintetico.aspx", New List(Of Integer)({0, 1, 3, 4, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cetip/CetipPotencialAnaliticoConsulta.aspx", New List(Of Integer)({0, 1, 3, 4, 7, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cetip/CetipPotencialSinteticoConsulta.aspx", New List(Of Integer)({0, 1, 3, 4, 7, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cetip/CetipRankingConsulta.aspx", New List(Of Integer)({0, 1, 3, 4, 7, 8})))
        'ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/ECred.aspx", New List(Of Integer)({0, 1, 3, 4, 7})))

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/CobrançaP123.aspx", New List(Of Integer)({0, 1, 3, 4, 7, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/CobrançaP123Microcredito.aspx", New List(Of Integer)({0, 1, 3, 4, 7, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/Recebimento.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/Prestamista.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/AcionamentoCliente.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/RepacRefin.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/ExtracaoPlacas.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/BaseNPS.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/Aniversariantes.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/AniversariantesPJ.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Comercial/QuitacaoContrato.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Comercial/CarteiraClienteAtivoInativo.aspx", New List(Of Integer)({0, 1})))

        'Crédito
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Credito/AnaliseAnalista.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/ProducaoAnalista.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/InadinplenciaPorAnalista.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/P123Analista.aspx", New List(Of Integer)({0, 1})))

        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/SinteticoAnalise.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/SinteticoInadimpl.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/Excecao.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/TempoTotal.aspx", New List(Of Integer)({0, 1, 6, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/TempoTotal2.aspx", New List(Of Integer)({0, 1, 6, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/TempoTotal3.aspx", New List(Of Integer)({0, 1, 6, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/BaseProposta.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/ResumoProposta.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/Abandonadas.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/AbandonadasAnali.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/MotivoPenRecProposta.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/Crivo_Apr_Aut.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/PassoEsteira.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/FechaComercial/AprovadasReducao.aspx", New List(Of Integer)({0, 1, 3})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/Propostas_nok65.aspx", New List(Of Integer)({0, 1})))
        'ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/AnaliseTempo.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/TempoCreditoAnalitico.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Credito/PagaRecebidaAnalitica.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/RelCadastroLojas.aspx", New List(Of Integer)({0, 1, 3})))

        'Gerencial
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/Balancete.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/Cubo.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/PadraoTrezeMeses.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/PadraoDozeMeses.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/Balanco.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/Liquidados.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/ValidacaoCadastral.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/DRECarga.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Gerencial/ColaboradoresPLD.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/SegundaGarantia.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cadastro/Gerencial/CadastroDeContaComissao.aspx", New List(Of Integer)({0, 1, 3})))

        'CETIP
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cetip/CetipUpload.aspx", New List(Of Integer)({1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cetip/CetipGerarArquivo.aspx", New List(Of Integer)({1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cetip/CetipPotencialAnalitico.aspx", New List(Of Integer)({1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cetip/CetipPotencialSintetico.aspx", New List(Of Integer)({1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cetip/CetipRanking.aspx", New List(Of Integer)({1})))

        'BANKFACIL
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/BankFacil/Operacoes.aspx", New List(Of Integer)({0, 1, 9})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/BankFacil/Cobranca.aspx", New List(Of Integer)({0, 1, 9})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/BankFacil/Pagamentos.aspx", New List(Of Integer)({0, 1, 9})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/BankFacil/CnabGerarArquivo.aspx", New List(Of Integer)({0, 1, 9})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/BankFacil/PosicaoVencimento.aspx", New List(Of Integer)({0, 1, 9})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/BankFacil/Lastro.aspx", New List(Of Integer)({0, 1, 9})))

        'FINANCEIRO
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Financeiro/OperacaoCaptacao.aspx", New List(Of Integer)({1, 11})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Financeiro/BaseCaptacao.aspx", New List(Of Integer)({1, 11})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cadastro/Financeiro/TaxaCDI.aspx", New List(Of Integer)({1, 11})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/Cadastro/Financeiro/Caixa.aspx", New List(Of Integer)({1, 11})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Financeiro/Remessa.aspx", New List(Of Integer)({0, 1, 11})))

        'RH
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/RH/CPColaboradores.aspx", New List(Of Integer)({0, 12})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/paginas/RH/CPColaboradoresSintetico.aspx", New List(Of Integer)({0, 12})))

        'FORMALIZAÇÃO
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/PendenciasCRVsint.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/PendenciasFormalizacao.aspx", New List(Of Integer)({0, 1}))) ' sem acesso aos 8=Operadores 
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/PendenciasFormalizacaoAnalitico.aspx", New List(Of Integer)({0, 1}))) ' sem acesso aos 8=Operadores 
        
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/ConferePagto.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/ConferePagto_Det.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/GravameInclusao.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/GravameQuitacao.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/PendenciasCRVCartAtivaAnali.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/PendenciasCRVCartAtivaSint.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/PendenciasCRVCartAtivaLiq.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/SubstituicaoGarantia.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Formaliz/PagtoCtaAgente.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Formaliz/PassoEsteiraFormaliz.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Formaliz/TempoFormalizAnalitico.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/PropostasBalcao.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/ConsultaCrivo.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/BaseSMS.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/ConsultaCarne.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Comercial/ConsultaCarneSintetico.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Formaliz/AssinaturaDigital.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Formaliz/RegistroGravame.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Formaliz/PendenciaRegistroContrato.aspx", New List(Of Integer)({0, 1})))

        'DESCONTO
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Desconto/DescontoProducao.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Desconto/Desconto8020.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Desconto/CobrancaPJ.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Desconto/ContratoOperador.aspx", New List(Of Integer)({0, 1})))

        'CADASTRO
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Cadastro/Tabelas.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Cadastro/Produtos.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Cadastro/Lojas.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Cadastro/Promotoras.aspx", New List(Of Integer)({0, 1, 8})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cadastro/Cadastro/Operadores.aspx", New List(Of Integer)({0, 1, 8})))

        'FUNDOQUATA
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/FundoQuata/Cnab550.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/FundoQuata/CnabBaixaInclusao.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/FundoQuata/GerarCnab.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/FundoQuata/CnabAlter.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/FundoQuata/GerarCnabAlter.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/FundoQuata/OrdinarioCarga_O.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/FundoQuata/OrdinarioCnabGerar_O.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/FundoQuata/RenegCnabCarga_RN.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/FundoQuata/RenegCnabGerar_RN.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/FundoQuata/EstornoCnabGerar_ES.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/FundoQuata/EstornoCnabCarga_ES.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/FundoQuata/OrdinarioCnabAtualizar_O.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/FundoQuata/OrdinarioCnabCriticaGerar_O.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/FundoQuata/OrdinarioCnabCriticaAtualizar_O.aspx", New List(Of Integer)({0, 1})))

        'PERFILCONFIGURACOES
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/PerfilConfiguracoes/DadosPessoais.aspx", New List(Of Integer)({0, 1})))


        'Tecnologia'
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/TI/Inventario2.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/TI/Agendador.aspx", New List(Of Integer)({0, 1})))
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/TI/CadastroHistorico.aspx", New List(Of Integer)({0, 1})))



        'FECHAMENTO DE COBRAÇA'
        ContextoWeb.DadosMenu.ListMenu.Add(New ItemMenu("/Paginas/Cobranca/FechamentoCobranca/FechamentoCobranca.aspx", New List(Of Integer)({0, 1})))


    End Sub
End Class
