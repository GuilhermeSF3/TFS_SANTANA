<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="false" CodeBehind="Menu.aspx.vb" Inherits="Santana.Menu" Title="Santana Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="dvMenu">

        <div class="navbar navbar-default" style="background-color: white; color: black !important; box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06);">
            <div class="container-full">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" runat="server" href="~/Paginas/Menu.aspx"></a>
                </div>
                <div class="navbar-collapse collapse">
                    <ul class="nav navbar-nav" id="MenuRoot" runat="server">

                        <li class="dropdown" id="Parametros" runat="server" visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="bi bi-display-fill"></i> Paramet<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li class="menu-item dropdown dropdown-submenu" id="Usuarios" runat="server" visible="true">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Usuários</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Paginas/Cadastro/Usuarios.aspx">Usuários</a></li>
                                        <li><a href="../Paginas/Cadastro/Usu_services.aspx">Services</a></li>
                                        <li><a href="../Paginas/Cadastro/Usu_Operador_SVC.aspx">Agentes Operadores nas Services</a></li>

                                    </ul>
                                </li>
                                <li class="menu-item dropdown dropdown-submenu" id="ComissaoPj" runat="server" visible="true">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown"> Comissão PJ</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Paginas/Cadastro/Comissao PJ/MetaOperadores.aspx">Meta dos operadores</a></li>
                                        <li><a href="../Paginas/Cadastro/Comissao PJ/TcOperacaoXMeta.aspx">TC Por Operação x Meta</a></li>
                                        <li><a href="../Paginas/Cadastro/Comissao PJ/TaxaComissãoXMeta.aspx">Taxa Comissão x META</a></li>
                                        <li><a href="../Paginas/Cadastro/Comissao PJ/TxReduz.aspx">Taxa Reduzida</a></li>
                                        <li><a href="../Paginas/Cadastro/Comissao PJ/Desconto.aspx">Desconto</a></li>
                                        <li><a href="../Paginas/Cadastro/Comissao PJ/OperadorAtendeCliente.aspx">Operador Atende Cliente</a></li>
                                        <li><a href="../Paginas/Cadastro/Comissao PJ/ClienteComissao.aspx">Cliente Comissão</a></li>
                                        <li><a href="../Paginas/Cadastro/Comissao PJ/ClienteEntrada.aspx">Cliente - Entrada na Santana</a></li>
                                        <li><a href="../Paginas/Cadastro/Comissao PJ/Fixo.aspx">Fixo - Ajuda de Custo</a></li>
                                        <li class="divider"></li>
                                        <li><a href="../Paginas/Cadastro/Comissao PJ/ComissaoPJ.aspx">Comissão PJ</a></li>
                                        <li><a href="../Paginas/Cadastro/Comissao PJ/ComissaoPJAnalitico.aspx">Comissão PJ Analítico</a></li>
                                    </ul>
                                </li>
                                <li><a href="../Paginas/Cadastro/PesoIp.aspx">Peso IP</a></li>
                                <li class="menu-item dropdown dropdown-submenu" id="Cobrança" runat="server" visible="true">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Cobrança</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Paginas/Cadastro/Cobradora.aspx">Cobradora</a></li>
                                        <li><a href="../Paginas/Cadastro/MetaPDD.aspx">Meta PDD</a></li>
                                        <li><a href="../Paginas/Cadastro/MetaICM.aspx">Meta ICM</a></li>
                                        <li><a href="../Paginas/Cadastro/MetaICMRepac.aspx">Meta ICM - Microcrédito</a></li>
                                        <li><a href="../Paginas/Cadastro/MetaBNDU.aspx">Meta BNDU</a></li>
                                    </ul>
                                </li>
                                <li class="menu-item dropdown dropdown-submenu" id="Li1" runat="server" visible="true">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">PRODUTOS</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Paginas/Cadastro/Produto.aspx">Produtos</a></li>
                                        <li><a href="../Paginas/Cadastro/TipoProduto.aspx">Tipo de Produto</a></li>
                                        <li><a href="../Paginas/Cadastro/Modalidade.aspx">Modalidade</a></li>
                                    </ul>
                                </li>
                                <li class="menu-item dropdown dropdown-submenu" id="Li2" runat="server" visible="true">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">SPREAD</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Paginas/Cadastro/Cubo.aspx">Cubo</a></li>
                                        <li><a href="../Paginas/Cadastro/TaxCaptacao.aspx">Taxa de Captação</a></li>
                                        <li><a href="../Paginas/Cadastro/Comissao.aspx">% Comissão</a></li>
                                        <li><a href="../Paginas/Cadastro/SalarioMinimo.aspx">Salário Mínimo</a></li>
                                        <li><a href="../Paginas/Cadastro/FaixaAnoSpread.aspx">Faixa de Ano - Spread e Cred.Cobr.</a></li>
                                        <li><a href="../Paginas/Cadastro/Modelo.aspx">Modelos</a></li>
                                        <li><a href="../Paginas/Cadastro/FaixaDePlano.aspx">Faixa de Plano</a></li>
                                        <li class="divider"></li>
                                        <li><a href="../Paginas/Cadastro/Risco.aspx">Risco - Desconto</a></li>
                                        <li><a href="../Paginas/Cadastro/ComissaoDesconto.aspx">Comissão - Desconto</a></li>

                                    </ul>
                                </li>

                                <li><a href="../Paginas/Cadastro/FaixaAnoSafra.aspx">Faixa de Ano - Safra</a></li>

                                <li class="menu-item dropdown dropdown-submenu" id="ParametrosGerencial" runat="server" visible="False">

                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Gerencial</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Paginas/Cadastro/Gerencial/Feriados.aspx">Feriados</a></li>
                                        <li><a href="../Paginas/Cadastro/Gerencial/CadastroDeContaComissao.aspx">Conta - Comissão</a></li>
                                        <li><a href="../Paginas/Cadastro/DeParaOperadoresFuncaoNF.aspx">Operadores - Função X NF</a></li>
                                        <li><a href="../Paginas/Cadastro/CadastroGrupoPJ.aspx">Cadastro de Grupo PJ</a></li>
                                        <li><a href="../Paginas/Cadastro/Gerencial/Contas.aspx">Contas</a></li>
                                        <li><a href="../Paginas/Cadastro/Gerencial/ContaCalculo.aspx">Conta Cálculo</a></li>
                                        <li><a href="../Paginas/Cadastro/Gerencial/Relatorios.aspx">Relatórios</a></li>
                                        <li><a href="../Paginas/Cadastro/Gerencial/RelatoriosItens.aspx">Relatório - Conteúdo</a></li>
                                    </ul>
                                </li>

                                <li class="menu-item dropdown dropdown-submenu" id="ParametrosAR" runat="server" visible="False">

                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">AR</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Paginas/Cadastro/AR/Segmento.aspx">Segmento</a></li>
                                        <li><a href="../Paginas/Cadastro/AR/NumeroCarta.aspx">Número Carta</a></li>
                                        <li><a href="../Paginas/Cadastro/AR/Retorno.aspx">Retorno</a></li>
                                        <li><a href="../Paginas/Cadastro/AR/Motivo.aspx">Motivo</a></li>
                                        <li><a href="../Paginas/Cadastro/AR/DeParaOcorrenciasAR.aspx">De Para Ocorrencias</a></li>
                                    </ul>
                                </li>

                                <li class="menu-item dropdown dropdown-submenu" id="ParametrosPT" runat="server" visible="False">

                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Potencial</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Paginas/Cadastro/PT/Regiao.aspx">Região</a></li>
                                        <li><a href="../Paginas/Cadastro/PT/Grupo.aspx">Grupo</a></li>
                                        <li><a href="../Paginas/Cadastro/PT/UF.aspx">UF</a></li>
                                        <li><a href="../Paginas/Cadastro/PT/Cidade.aspx">Cidade</a></li>
                                    </ul>
                                </li>

                                <li class="menu-item dropdown dropdown-submenu" id="ParametrosProtesto" runat="server" visible="False">

                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Protesto</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Paginas/Cadastro/Protesto/OcorrenciasCartorio.aspx">Ocorrencias Cartório</a></li>
                                        <li><a href="../Paginas/Cadastro/Protesto/DeParaOcorrencias.aspx">De Para Ocorrencias</a></li>
                                        <li><a href="../Paginas/Cadastro/Protesto/ContratosExcecao.aspx">Contratos Exceção</a></li>
                                    </ul>
                                </li>

                                <li class="menu-item dropdown dropdown-submenu" id="MetaFormaliz" runat="server" visible="False">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Formalização</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Paginas/Cadastro/MetaFormaliz.aspx">Metas Mensal</a></li>
                                        <li><a href="../Paginas/Cadastro/MetaPromotoras.aspx">Metas Promotoras</a></li>
                                        <li><a href="../Paginas/Cadastro/MetaOperadoresInternos.aspx">Metas Operadores</a></li>
                                        <li><a href="../Paginas/Cadastro/DeParaAgentes.aspx">De Para Agentes</a></li>
                                        <li><a href="../Paginas/Cadastro/DeParaGerencia.aspx">De Para Gerência</a></li>
                                        <li><a href="../Paginas/Cadastro/RegiaoCadastro.aspx">Cadastro Região</a></li>
                                        <li><a href="../Paginas/Cadastro/DeParaRegiao.aspx">De Para Região</a></li>
                                        <li><a href="../Paginas/Cadastro/EquipeDinamo.aspx">Equipe Dinamo</a></li>
                                        <li><a href="../Paginas/Cadastro/CadastroEquipe.aspx">Cadastro Equipe</a></li>
                                        <li><a href="../Paginas/Cadastro/EmpresaRegistro.aspx">Empresa Registro</a></li>
                                        <li><a href="../Paginas/Cadastro/OperadorSubstabelecido.aspx">Cadastro Operador Substabelecido</a></li>
                                        <li><a href="../Paginas/Cadastro/PromotorasAdiantamento.aspx">Cadastro Promotoras - Adiantamento</a></li>
                                        <%--                                        <li class="menu-item dropdown dropdown-submenu" id="Gravame" runat="server" visible="False">
                                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Gravame</a>
                                            <ul class="dropdown-menu">
                                                <li><a href="../Paginas/Cadastro/GravameInclusao.aspx">Inclusão</a></li>
                                                <li><a href="../Paginas/Cadastro/GravameQuitacao.aspx">Quitação</a></li>
                                             </ul>
                                        </li>--%>
                                    </ul>
                                </li>

                                <li class="menu-item dropdown dropdown-submenu" id="ParamRC" runat="server" visible="true">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Risco Crédito</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Paginas/Cadastro/RcFaixaScore.aspx">Faixas Score</a></li>
                                    </ul>
                                </li>

                                <li class="menu-item dropdown dropdown-submenu" id="Li4" runat="server" visible="true">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Ajuste Proposta</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Paginas/Cadastro/Score.aspx">Score</a></li>
                                        <li><a href="../Paginas/Cadastro/Database.aspx">Data Base</a></li>
                                        <li><a href="../Paginas/Cadastro/AlteracaoGarantia.aspx">Alteração Garantia</a></li>
                                    </ul>
                                </li>
                                <%-- <li><a href="../Paginas/Cadastro/IndexadorFIPE.aspx">Indexador FIPE</a></li>--%>
                            </ul>
                        </li>

                        <li class="dropdown" id="Cadastro" runat="server" visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="bi bi-clipboard2-check-fill"></i> Cadastro<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="../Paginas/Cadastro/Cadastro/Tabelas.aspx">Tabelas</a></li>
                                <li><a href="../Paginas/Cadastro/Cadastro/Produtos.aspx">Produtos</a></li>
                                <li><a href="../Paginas/Cadastro/Cadastro/Lojas.aspx">Lojas</a></li>
                                <li><a href="../Paginas/Cadastro/Cadastro/Promotoras.aspx">Promotoras</a></li>
                                <li><a href="../Paginas/Cadastro/Cadastro/Operadores.aspx">Operadores</a></li>
                            </ul>
                        </li>

                        <li class="dropdown" id="Cobranca" runat="server" visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="bi bi-headset"></i> Cobrança<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li class="menu-item dropdown dropdown-submenu" id="Relatorios" runat="server" visible="False">

                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Relatórios</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Paginas/RollrateMensal.aspx">Rollrate Mensal</a></li>
                                        <li><a href="RollrateDiario.aspx">Rollrate Diário</a></li>
                                        <li><a href="RollrateProjetadoAnalitico.aspx">Rollrate Projetado - Analítico</a></li>

                                        <li class="divider"></li>
                                        <li><a href="Cobranca/Relatorios/ParcelaPulada.aspx">Parcela Pulada</a></li>
                                        <li><a href="Cobranca/Relatorios/RecebParcela.aspx">Recebimento de Parcela</a></li>
                                        <li><a href="Cobranca/Relatorios/PosCobRenegRepac.aspx">Pos. Cobrança Reneg/Repac</a></li>
                                        <%--<li><a href="Cobranca/Relatorios/PosCob.aspx">Pos. Cobrança</a></li>--%>

                                        <li class="divider"></li>
                                        <li><a href="Cobranca/Relatorios/ControleAcoes.aspx">Controle de Ações - BA</a></li>
                                        <li><a href="Cobranca/Relatorios/ControleAcoesPJ.aspx">Controle de Ações - PF/PJ</a></li>
                                        <li class="divider"></li>
                                        <li><a href="UraGerarArquivo.aspx">URA - gerar arquivo</a></li>

                                        <li class="divider"></li>
                                        <li><a href="Carteira3M.aspx">Carteira 3 Meses</a></li>

                                        <li class="divider"></li>
                                        <li><a href="Cobranca/Relatorios/ARs.aspx">ARs</a></li>

                                        <li class="divider"></li>
                                        <li><a href="Cobranca/Relatorios/baseReneg.aspx">Renegociados</a></li>
                                        <li><a href="Cobranca/Relatorios/Ocorrencia.aspx">Ocorrencias</a></li>
                                        <li><a href="Cobranca/Relatorios/Repactuacao.aspx">Repactuação</a></li>
                                        <li class="divider"></li>
                                        <li><a href="Cobranca/Relatorios/Honorarios.aspx">Honorários</a></li>
                                    </ul>
                                </li>
                                <li class="menu-item dropdown dropdown-submenu" id="Graficos" runat="server" visible="False">

                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Gráficos</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="Graficos/GraficoCarteira.aspx">Carteira</a></li>
                                        <li><a href="Graficos/Rolagem.aspx?opcao=1">Rolagens</a></li>
                                        <li><a href="Graficos/Rolagem.aspx?opcao=2">Perda</a></li>
                                        <li><a href="Graficos/Estoque.aspx">Estoque e Comportamento</a></li>
                                        <li><a href="Cobranca/Consolidado/GraficoRolagemConsolidado.aspx">Consolidado</a></li>
                                    </ul>
                                </li>

                                <li class="menu-item dropdown dropdown-submenu" id="Protesto" runat="server" visible="False">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Protesto</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="Protesto/GerarRemessa.aspx">Gerar Remessa</a></li>
                                        <li><a href="Protesto/CargaConfirmacao.aspx">Carga Confirmação</a></li>
                                        <li><a href="Protesto/CargaRetorno.aspx">Carga Retorno</a></li>
                                        <li><a href="Protesto/GerarCapturaGV.aspx">Gera Captura GV</a></li>
                                        <li><a href="Protesto/Protesto.aspx">Acompanhamento Protesto</a></li>
                                    </ul>
                                </li>

                                <li class="menu-item dropdown dropdown-submenu" id="RiscoGerencial" runat="server" visible="False">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Risco Gerencial</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="Cobranca/P123Gerencial/P123Gerencial.aspx">p123 Gerencial</a></li>
                                        <li><a href="Cobranca/P123Gerencial/Inadimplencia.aspx">Inadimplência</a></li>
                                        <li><a href="Cobranca/P123Gerencial/Risco.aspx">Score</a></li>
                                        <li><a href="Cobranca/P123Gerencial/P123Gerencia13Meses.aspx">Score x Inadimplencia (visão 13 meses)</a></li>
                                    </ul>
                                </li>
                                <li class="menu-item dropdown dropdown-submenu" id="IP" runat="server" visible="False">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">IP</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="IndicePerformance.aspx">Indice de Performance</a></li>
                                        <li><a href="IndicePerformanceVR.aspx">IP Valor Recebido</a></li>
                                        <li><a href="IPSafraDiaria.aspx">IP Safra Diário</a></li>
                                        <li><a href="Cobranca/Relatorios/IPAnalitico.aspx">IP Analítico</a></li>
                                        <li class="divider"></li>
                                        <li><a href="Paginas/Cobranca/Relatorios/AjusteIP.aspx">Ajuste IP</a></li>
                                    </ul>
                                </li>
                                 <li class="menu-item dropdown dropdown-submenu" id="Fechamento" runat="server" visible="false">
                                     <a href="#" class="dropdown-toggle" data-toggle="dropdown">Fechamento</a>
                                     <ul class="dropdown-menu">
                                     <li><a href="Cobranca/FechamentoCobranca/FechamentoCobranca.aspx">Cobrança</a></li>
                                     </ul>
                                </li>
                                <li class="menu-item dropdown dropdown-submenu" id="ICM" runat="server" visible="false">
                                   <a href="#" class="dropdown-toggle" data-toggle="dropdown">ICM</a>
                                   <ul class="dropdown-menu">
                                     <li><a href="Cobranca/ICM/ForcarICM.aspx">Forçar ICM</a></li>
                                    </ul>
                                </li>
                                <li class="menu-item dropdown dropdown-submenu" id="FOP" runat="server" visible="false">
                                   <a href="#" class="dropdown-toggle" data-toggle="dropdown">FOP</a>
                                   <ul class="dropdown-menu">
                                     <li><a href="Cobranca/FOP/ArquivoFOP.aspx">Arquivo FOP</a></li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                        <li class="dropdown" id="RiscoCredito" runat="server" visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="bi bi-ban-fill"></i> Risco<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="RRMensalCUBO.aspx">Carteira / PDD</a></li>
                                <li><a href="PDD_GERENCIAL.aspx">Carteira PDD - Risco x Safra</a></li>
                                <li><a href="Reneg_geral.aspx">Risco Safra</a></li>
                                <li><a href="baseCred.aspx">Base de Crédito Consolidada</a></li>

                                <li class="menu-item dropdown dropdown-submenu" id="Spread" runat="server" visible="False">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Spread</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="SPREAD.aspx">Veículos</a></li>
                                        <li><a href="spreadDesconto.aspx">Desconto</a></li>
                                    </ul>
                                </li>

                                <li class="menu-item dropdown dropdown-submenu" id="SpreadAnalitico" runat="server" visible="False">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Spread Analítico</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="SPREADanalitico.aspx">Veículos</a></li>
                                        <li><a href="SpreadCapitalDeGiro.aspx">Capital de Giro</a></li>
                                        <li><a href="SPREADanaliticoDesconto.aspx">Desconto</a></li>
                                    </ul>
                                </li>

                                <li><a href="../Paginas/RiscoCredito/PDDSafraTempo.aspx">PDD Por Safra No Tempo</a></li>
                                <li><a href="../Paginas/RiscoCredito/Prestamista.aspx">Cobrança com Prestamista</a></li>

                                <li class="menu-item dropdown dropdown-submenu" id="P123" runat="server" visible="False">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">P 1 2 3</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="Graficos/P123Analise.aspx">P123 Análise</a></li>
                                        <li><a href="Graficos/P123Produto.aspx">P123 Produto</a></li>
                                        <li><a href="P123Agente.aspx">P123 Agente</a></li>
                                        <li><a href="P123Operador.aspx">P123 Operador</a></li>
                                        <li><a href="P123_Analista.aspx">P123 Analista</a></li>
                                    </ul>
                                </li>
                                <li class="menu-item dropdown dropdown-submenu" id="P123Microcredito" runat="server" visible="False">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">P 1 2 3 Microcrédito</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="Graficos/P123AnaliseMicrocredito.aspx">P123 Análise</a></li>
                                        <li><a href="P123OperadorMicrocredito.aspx">P123 Operador</a></li>
                                    </ul>
                                </li>

                                <li class="menu-item dropdown dropdown-submenu" id="RiscoCreditoInd" runat="server" visible="False">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Risco Crédito</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="RiscoCreditoIndicador.aspx">Indicadores</a></li>
                                        <li><a href="RiscoCreditoSafra.aspx">Safra</a></li>
                                    </ul>
                                </li>

                            </ul>
                        </li>

                        <li class="dropdown" id="ComercialCredito" runat="server" visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="bi bi-credit-card"></i> Com.-Crédito<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="FechaComercial/FechaComCreOpRealizadas.aspx">Operações Realizadas</a></li>
                                <li><a href="FechaComercial/OpRealizadasAnalitica.aspx">Operações Realizadas - Analítico</a></li>
                                <li><a href="FechaComercial/FechaComCreProducaoProdutos.aspx">Produção Mensal / Produto (Gráf.)</a></li>
                                <li><a href="FechaComercial/FechaComCreProducaoProdutosOP.aspx">Vlr Operado - Produção Mensal / Produto (Gráf.)</a></li>
                                <li><a href="FechaComercial/FechaComCreAnaliseProd.aspx">Análise de Produção</a></li>
                                <li><a href="FechaComercial/FechaComPropostasOp.aspx">Análise de Propostas Operador</a></li>
                                <li><a href="FechaComercial/FechaComPropostasHis.aspx">Histórico Analise de Propostas</a></li>
                                <li><a href="FechaComercial/FechaComTipoVeiculo.aspx">Financiamento por Tipo de Veiculo</a></li>
                                <li><a href="FechaComercial/FechaComAnaliseVeiculoPlano.aspx">Análise de Veículos Plano</a></li>
                                <li><a href="FechaComercial/FechaComAnaliseVeiculoAno.aspx">Análise de Veículos Ano</a></li>
                                <li><a href="FechaComercial/FechaComAnaliseVeiculoTipoAno.aspx">Análise Tipo Ano</a></li>
                                <li><a href="FechaComercial/FechaComClientes.aspx">Análise Maiores Clientes</a></li>
                                <li><a href="FechaComercial/FechaComScoreMedio.aspx">Score Médio por Agente / Mensal</a></li>
                                <li><a href="FechaComercial/PagasRecebidas.aspx">Pagas X Recebidas</a></li>
                                <li><a href="FechaComercial/Fipe.aspx">Base Fipe</a></li>
                                <li><a href="Comercial/Recebimento.aspx">Base de Recebimentos</a></li>
                                <li class="menu-item dropdown dropdown-submenu" id="ddownComissao" runat="server" visible="true">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Comissão</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="FechaComercial/Comissao.aspx">Veículos</a></li>
                                        <li><a href="FechaComercial/ComissaoDinamo.aspx">Dínamo</a></li>
                                        <li><a href="FechaComercial/ComissaoAdiantamento.aspx">Adiantamento</a></li>
                                        <li><a href="FechaComercial/ComissaoSintetico.aspx">Sintético</a></li>
                                        <li><a href="FechaComercial/GerarArquivoComissao.aspx">Gerar Arquivo</a></li>
                                    </ul>
                                </li>
                                <li><a href="Comercial/BasePropostaTempo.aspx">Base Propostas - Mesa</a></li>
                                <li><a href="Comercial/RelCadastroLojas.aspx">Cadastro de Lojas</a></li>

                            </ul>
                        </li>

                        <li class="dropdown" id="Comercial" runat="server" visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="bi bi-basket2-fill"></i> Comercial<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="Comercial/MapaComercial.aspx">Mapa Comercial</a></li>
                                <li><a href="Comercial/MapaPDV.aspx">Mapa PDV - PJ</a></li>
                                <li><a href="Comercial/MapaPDVveicLOJA.aspx">Mapa PDV - Veículos</a></li>
                                <li><a href="Comercial/PontecialMktShare.aspx">Potencial MarketShare</a></li>
                                <li class="divider"></li>
                                <li class="menu-item dropdown dropdown-submenu" id="CRV" runat="server" visible="true">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">CRV</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="Comercial/PendenciasCRV.aspx">Pendências de CRV</a></li>
                                        <li><a href="Comercial/PendenciasCRVInativos.aspx">Pendências Antigas de CRV</a></li>
                                        <%--<li><a href="Comercial/PendenciasCRVPromotora.aspx">Pendências CRV - Promotora</a></li>--%>
                                        <%--<li><a href="Comercial/PendenciasCRVEquipe.aspx">Pendências CRV - Equipe</a></li>--%>
                                        <li><a href="Comercial/PendenciasCRVBaixa.aspx">Pendências CRV - Baixa</a></li>
                                    </ul>
                                </li>
                                <li><a href="Comercial/PendenciasContrato.aspx">Pendências de Contrato</a></li>
                                <li><a href="Comercial/PendenciasContratoSint.aspx">Pendências de Contrato - Sint.</a></li>
                                <li><a href="COMERCIAL/PendenciasGravames.aspx">Pendencia de Gravames</a></li>
                                <li><a href="COMERCIAL/PendenciasGravameSint.aspx">Pendencia de Gravames - Sint.</a></li>
                                <li><a href="Comercial/PagtoPendencia.aspx">Contratos Pagos com Pendência</a></li>
                                <li><a href="Comercial/ContratoPgPendSint.aspx">Contratos Pagos com Pendência - Sint.</a></li>
                                <li><a href="Comercial/Prestamista.aspx">Base Prestamista</a></li>
                                <li><a href="COMERCIAL/AcionamentoCliente.aspx">Acionamento de Clientes</a></li>
                                <li><a href="COMERCIAL/RepacRefin.aspx">Repacs para Refin</a></li>
                                <li><a href="Comercial/ExtracaoPlacas.aspx">Extração de Placas</a></li>
                                <li><a href="Comercial/BaseNPS.aspx">Base NPS</a></li>
                                <li><a href="Comercial/Aniversariantes.aspx">Aniversariantes</a></li>
                                <li><a href="Comercial/AniversariantesPJ.aspx">Aniversariantes PJ</a></li>
                                <li><a href="Comercial/QuitacaoContrato.aspx">Quitação Contrato</a></li>
                                <li><a href="Comercial/CarteiraClienteAtivoInativo.aspx">Carteira de Clientes Ativos e Inativos</a></li>
                                <li class="divider"></li>
                                <li><a href="../Paginas/Cetip/CETIPCruzamento.aspx">Conversão CETIP</a></li>
                                <li><a href="../Paginas/Cetip/CETIPSintetico.aspx">Conversão Sintético CETIP</a></li>
                                <li><a href="../Paginas/Cetip/CETIPRankingConsulta.aspx">Ranking CETIP</a></li>
                                <li><a href="../Paginas/Cetip/CETIPPotencialAnaliticoConsulta.aspx">Potencial Analitico CETIP</a></li>
                                <li><a href="../Paginas/Cetip/CETIPPotencialSinteticoConsulta.aspx">Potencial Sintetico CETIP</a></li>

                                <li class="divider"></li>

                                <li class="menu-item dropdown dropdown-submenu" id="CobrancaP123" runat="server" visible="true">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Cobrança P123</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Paginas/Comercial/CobrançaP123.aspx">Veículos</a></li>
                                        <li><a href="../Paginas/Comercial/CobrançaP123Microcredito.aspx">Microcrédito</a></li>
                                    </ul>
                                </li>
                                <li><a href="Cobranca/Relatorios/ParcelaVcto.aspx">Parcelas por Vencimento</a></li>
                                <li><a href="Comercial/ConsultaCarne.aspx">Consulta Carnê</a></li>
                                <li><a href="Comercial/ConsultaCarneSintetico.aspx">Consulta Carnê - Sint.</a></li>
                            </ul>
                        </li>

                        <li class="dropdown" id="Credito" runat="server" visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="bi bi-credit-card-fill"></i> Crédito<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="Credito/AnaliseAnalista.aspx">Análise Analista</a></li>
                                <li><a href="Credito/ProducaoAnalista.aspx">Produção Analista</a></li>
                                <li><a href="Credito/InadinplenciaPorAnalista.aspx">Inadimplencia Analista</a></li>
                                <li><a href="Credito/P123Analista.aspx">P123 Analista</a></li>
                                <li class="divider"></li>
                                <li><a href="Credito/SinteticoAnalise.aspx">Sintético Analista (tempos)</a></li>
                                <li><a href="Credito/SinteticoInadimpl.aspx">Sintético Inadimplencia</a></li>
                                <li class="divider"></li>
                                <li><a href="Credito/Excecao.aspx">Monitor das Aprovadas em Exceção</a></li>
                                <li><a href="FechaComercial/AprovadasReducao.aspx">Monitor das Aprovadas com Redução</a></li>
                                <li><a href="Credito/Propostas_nok65.aspx">Propostas - NOK 65</a></li>
                                <li><a href="Credito/TempoTotal.aspx">Tempo Total de Propostas</a></li>
                                <li class="divider"></li>
                                <li><a href="Credito/BaseProposta.aspx">Base de Propostas</a></li>
                                <li><a href="Credito/ResumoProposta.aspx">Resumo Base de Propostas</a></li>
                                <li><a href="Credito/PagaRecebidaAnalitica.aspx">Pagas x Recebidas - Analítica</a></li>
                                <li><a href="Credito/Abandonadas.aspx">Recebidas x Abandonadas</a></li>
                                <li><a href="Credito/AbandonadasAnali.aspx">Recebidas x Abandonadas - Analítica</a></li>
                                <li><a href="Credito/MotivoPenRecProposta.aspx">Motivos Pen. Rec. Proposta</a></li>
                                <li><a href="Credito/Crivo_Apr_Aut.aspx">Crivo - Aprov. Automática</a></li>
                                <li class="divider"></li>
                                <li><a href="Credito/PassoEsteira.aspx">Tempo - Passo da Esteira Crédito</a></li>
                                <li><a href="Credito/TempoCreditoAnalitico.aspx">Dashboard Analítico Crédito</a></li>
                                <%--<li><a href="Credito/AnaliseTempo.aspx">Análise Tempo</a></li>--%>
                                <%--<li class="divider"></li>--%>
                                <%--<li><a href="../Paginas/Comercial/ECred.aspx">eCred</a></li>--%>
                            </ul>
                        </li>

                        <li class="dropdown" id="Gerencial" runat="server" visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="bi bi-file-bar-graph-fill"></i> Gerencial<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="../Paginas/Cadastro/Gerencial/Cubo.aspx">Cubo</a></li>
                                <li><a href="../Paginas/Cadastro/Gerencial/Balancete.aspx">Balancete</a></li>
                                <li><a href="../Paginas/Cadastro/Gerencial/PadraoTrezeMeses.aspx">Relatório Padrão 1</a></li>
                                <li><a href="../Paginas/Cadastro/Gerencial/PadraoDozeMeses.aspx">Relatório Padrão 2</a></li>
                                <li><a href="../Paginas/Cadastro/Gerencial/Balanco.aspx">Balanço</a></li>
                                <li><a href="../Paginas/Cadastro/Gerencial/Liquidados.aspx">Liquidados</a></li>
                                <li><a href="../Paginas/Cadastro/Gerencial/ValidacaoCadastral.aspx">Validação Cadastral</a></li>
                                <li class="divider"></li>
                                <li><a href="../Paginas/Cadastro/Gerencial/DRECarga.aspx">DRE - Contas (carga)</a></li>
                                <li><a href="../Paginas/Cadastro/Gerencial/ColaboradoresPLD.aspx">Colaboradores - PLD</a></li>
                                <li><a href="../Paginas/Comercial/SegundaGarantia.aspx">Segunda Garantia</a></li>
                            </ul>
                        </li>

                        <li class="dropdown" id="CETIP" runat="server" visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="bi bi-diagram-3-fill"></i>CETIP<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="Cetip/CETIPupload.aspx">Download Retorno</a></li>
                                <li><a href="Cetip/CETIPGerarArquivo.aspx">Gerar Remessa</a></li>
                                <li><a href="../Paginas/Cetip/CETIPRanking.aspx">Ranking CETIP - Carga</a></li>
                                <li><a href="../Paginas/Cetip/CETIPPotencialAnalitico.aspx">Potencial Analitico CETIP - Carga</a></li>
                                <li><a href="../Paginas/Cetip/CETIPPotencialSintetico.aspx">Potencial Sintetico CETIP - Carga</a></li>
                            </ul>
                        </li>

                        <li class="dropdown" id="Li3" runat="server" visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="bi bi-building-check"></i>Comissão PJ<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="../Paginas/Cadastro/Comissao PJ/ComissaoPJ.aspx">Comissão PJ</a></li>
                                <li><a href="../Paginas/Cadastro/Comissao PJ/ComissaoPJAnalitico.aspx">Comissão PJ Analítico</a></li>
                            </ul>
                        </li>

                        <li class="dropdown" id="PJ" runat="server" visible="false">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="bi bi-buildings"></i>PJ<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="../Paginas/PJ/AssinaDigital.aspx">Assinatura Digital</a></li>
                            </ul>
                        </li>

                        <%--                        <li class="dropdown" id="BankFacil" runat="server" visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Services<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="../Paginas/BankFacil/Operacoes.aspx">Operações BKF</a></li>
                                <li><a href="../Paginas/BankFacil/Cobranca.aspx">Cobrança BKF</a></li>
                                <li><a href="../Paginas/BankFacil/Pagamentos.aspx">Pagamentos BKF</a></li>
                                <li><a href="../Paginas/BankFacil/CnabGerarArquivo.aspx">CNAB</a></li>
                                <li><a href="../Paginas/BankFacil/PosicaoVencimento.aspx">Posição de Vencimento</a></li>
                                <li><a href="../Paginas/BankFacil/Lastro.aspx">Lastro de Cessão</a></li>
                            </ul>
                        </li>--%>

                        <li class="dropdown" id="DCO" runat="server" visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="bi bi-inboxes-fill"></i> Formaliz<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="Comercial/PendenciasCRVsint.aspx">Pendências de CRV Sintético</a></li>
                                <li><a href="Comercial/PendenciasFormalizacao.aspx">Pendências de Formalização</a></li>
                                <li><a href="Comercial/PendenciasFormalizacaoAnalitico.aspx">Pendências de Formalização - Analítico</a></li>
                                <li><a href="Formaliz/PendenciaRegistroContrato.aspx">Pendência de Registro de Contrato</a></li>
                                <li class="menu-item dropdown dropdown-submenu" id="ConferePagamento" runat="server" visible="False">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Confere Pagamentos</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="Comercial/ConferePagto_Det.aspx">Detalhado</a></li>
                                        <li><a href="Comercial/ConferePagto.aspx">Resumido</a></li>
                                    </ul>
                                </li>
                                <li class="menu-item dropdown dropdown-submenu" id="GravIncQui" runat="server" visible="False">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Gravame</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="Comercial/GravameInclusao.aspx">Inclusão</a></li>
                                        <li><a href="Comercial/GravameQuitacao.aspx">Quitação</a></li>
                                    </ul>
                                </li>
                                <li class="menu-item dropdown dropdown-submenu" id="PendCrvCartAtiva" runat="server" visible="False">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Pendência CRV - Carteira Ativa</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="Comercial/PendenciasCRVCartAtivaAnali.aspx">Analítico</a></li>
                                        <li><a href="Comercial/PendenciasCRVCartAtivaSint.aspx">Sintético</a></li>
                                        <li><a href="Comercial/PendenciasCRVCartAtivaLiq.aspx">Liquidados</a></li>
                                    </ul>
                                </li>
                                <li><a href="Comercial/SubstituicaoGarantia.aspx">Substituição de Garantia</a></li>
                                <li><a href="Formaliz/PagtoCtaAgente.aspx">Pago na Cta Agente</a></li>
                                <li><a href="Formaliz/PassoEsteiraFormaliz.aspx">Tempo - Passo da Esteira Formalização</a></li>
                                <li><a href="Formaliz/TempoFormalizAnalitico.aspx">Dashboard Analítico Formalização</a></li>
                                <li><a href="Comercial/PropostasBalcao.aspx">Propostas Balcão</a></li>
                                <li><a href="Comercial/ConsultaCrivo.aspx">Consulta Crivo</a></li>
                                <li><a href="Comercial/BaseSMS.aspx">Base SMS</a></li>
                                <li><a href="Formaliz/AssinaturaDigital.aspx">Assinatura Digital</a></li>
                                <li><a href="Formaliz/RegistroGravame.aspx">Registro Contrato</a></li>
                            </ul>
                        </li>
                        <li class="dropdown" id="FundoQuata" runat="server" visible="true">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="bi bi-wallet-fill"></i> Fundo-Quata<b class="caret"></b></a>
                            <ul class="dropdown-menu ">
                                <li class="dropdown-submenu">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Cnab Baixa</a>
                                    <ul class="dropdown-menu">
                                        <li class="dropdown-submenu">
                                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Extraordinário</a>
                                            <ul class="dropdown-menu ">
                                                <li><a href="../Paginas/FundoQuata/Cnab550.aspx">Cnab - Carga</a></li>
                                                <li><a href="../Paginas/FundoQuata/GerarCnab.aspx">Cnab - Gerar</a></li>
                                                <li><a href="../Paginas/FundoQuata/CnabBaixaInclusao.aspx">Cnab - Atualizar</a></li>
                                                <li class="dropdown-submenu">
                                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Contratos Inexistente</a>
                                                    <ul class="dropdown-menu">
                                                        <li><a href="../Paginas/FundoQuata/CnabAlter.aspx">Atualizar - Alterada</a></li>
                                                        <li><a href="../Paginas/FundoQuata/GerarCnabAlter.aspx">Gerar - Alterada</a></li>
                                                    </ul>
                                                </li>
                                            </ul>
                                        </li>
                                        <li class="dropdown-submenu">
                                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"> Ordinário</a>
                                            <ul class="dropdown-menu">
                                                <li><a href="../Paginas/FundoQuata/OrdinarioCarga_O.aspx">Cnab - Carga</a></li>
                                                <li><a href="../Paginas/FundoQuata/OrdinarioCnabGerar_O.aspx">Cnab - Gerar</a></li>
                                                <li><a href="../Paginas/FundoQuata/OrdinarioCnabAtualizar_O.aspx">Cnab - Atualizar</a></li>
                                                <li class="dropdown-submenu">
                                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Contratos Inexistente</a>
                                                    <ul class="dropdown-menu">
                                                        <li><a href="../Paginas/FundoQuata/OrdinarioCnabCriticaAtualizar_O.aspx">Atualizar - Alterada</a></li>
                                                        <li><a href="../Paginas/FundoQuata/OrdinarioCnabCriticaGerar_O.aspx">Gerar - Alterada</a></li>
                                                    </ul>
                                                </li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li class="dropdown-submenu">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Cnab Renegociação</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Paginas/FundoQuata/RenegCnabCarga_RN.aspx">Cnab - Carga</a></li>
                                        <li><a href="../Paginas/FundoQuata/RenegCnabGerar_RN.aspx">Cnab - Gerar</a></li>
                                    </ul>
                                </li>
                                <li class="dropdown-submenu">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Cnab Estorno</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Paginas/FundoQuata/EstornoCnabCarga_ES.aspx">Cnab - Carga</a></li>
                                        <li><a href="../Paginas/FundoQuata/EstornoCnabGerar_ES.aspx">Cnab - Gerar</a></li>
                                    </ul>
                                </li>
                            </ul>
                        </li>

                        <li class="dropdown" id="Financeiro" runat="server" visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="bi bi-bank2"></i> Financeiro<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="../Paginas/Financeiro/OperacaoCaptacao.aspx">Operação Captação</a></li>
                                <li><a href="../Paginas/Financeiro/BaseCaptacao.aspx">Base de Captação</a></li>
                                <li><a href="../Paginas/Cadastro/Financeiro/TaxaCDI.aspx">Cadastro de Taxa CDI</a></li>
                                <li><a href="../Paginas/Cadastro/Financeiro/Caixa.aspx">Lançamento de Caixa</a></li>
                                <li><a href="../Paginas/Financeiro/Remessa.aspx">Remessa</a></li>
                                      <li class="dropdown-submenu">
          <a href="#" class="dropdown-toggle" data-toggle="dropdown">Agendamento</a>
          <ul class="dropdown-menu">
              <li><a href="../Paginas/TI/Agendador.aspx">Agendador</a></li>
              <li><a href="../Paginas/TI/CadastroHistorico.aspx">Cadastro Histórico</a></li>
                    <li><a href="../Paginas/TI/ListaDeAgendas.aspx">Lista De Agendas</a></li>
          </ul>
      </li>
                            </ul>
                        </li>

                        <li class="dropdown" id="RH" runat="server" visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="bi bi-people-fill"></i> RH<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="RH/CPColaboradores.aspx">CP Colaboradores</a></li>
                                <li><a href="RH/CPColaboradoresSintetico.aspx">CP Colaboradores - Sintético</a></li>
                            </ul>
                        </li>

                        <li class="dropdown" id="Desconto" runat="server" visible="false">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="bi bi-buildings"></i> PJ<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="../Paginas/Desconto/DescontoProducao.aspx">Produção</a></li>
                                <li><a href="../Paginas/Desconto/Desconto8020.aspx">80 20</a></li>
                                <li><a href="../Paginas/Desconto/CobrancaPJ.aspx">Cobrança PJ</a></li>
                                <li><a href="../Paginas/Desconto/ContratoOperador.aspx">Contratos por Operador</a></li>
                                <li><a href="../Paginas/Desconto/Onboarding.aspx">Onboarding</a></li>
                                <li><a href="../Paginas/Desconto/Assertiva.aspx">Assertiva</a></li>
                            </ul>
                        </li>
<li class="dropdown" id="TI" runat="server" visible="False">
    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
        <i class="bi bi-browser-edge"></i> TI <b class="caret"></b>
    </a>
    <ul class="dropdown-menu">
        <li><a href="../Paginas/TI/Inventario2.aspx">Inventário</a></li>
        
<<<<<<< HEAD
  
=======
        <li class="dropdown-submenu">
            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Agendamento</a>
            <ul class="dropdown-menu">
                <li><a href="../Paginas/TI/Agendador.aspx">Agendador</a></li>
                <li><a href="../Paginas/TI/CadastroHistorico.aspx">Cadastro Histórico</a></li>
                <li><a href="../Paginas/TI/RelatorioDespesas.aspx">Relatório de Despesas</a></li>
            </ul>
        </li>
>>>>>>> renato
    </ul>
</li>
                    </ul>







                    <ul class="nav navbar-nav navbar-right">
                        <li><a runat="server" href="~/Paginas/Logon.aspx"><i class="bi bi-box-arrow-right"></i> Sair</a></li>
                    </ul>

                </div>
            </div>
        </div>

        <div class="modal fade" id="favoritoModal" tabindex="-1" role="dialog" aria-labelledby="favoritoModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" style="font-weight:600;" id="favoritoModalLabel">Adicionar Favorito</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <div class="form-group">
                            <p style="font-size: 16px; font-weight: 600;">Escolha o módulo que deseja favoritar</p>
                            <div style="display: flex; flex-wrap: wrap; gap: 10px; margin-bottom: 10px;">

                                <button type="button" class="btn btn-primary" onclick="parametativ('parametrosContent', event)">Parâmetros</button>
                                <button type="button" class="btn btn-primary" onclick="parametativ('cadastrosContent', event)">Cadastros</button>
                                <button type="button" class="btn btn-primary" onclick="parametativ('riscoContent', event)">Risco</button>
                                <button type="button" class="btn btn-primary" onclick="parametativ('comCreditoContent', event)">Com-Crédito</button>
                                <button type="button" class="btn btn-primary" onclick="parametativ('cobrancaContent', event)">Cobrança</button>
                                <button type="button" class="btn btn-primary" onclick="parametativ('comercialContent', event)">Comercial</button>
                                <button type="button" class="btn btn-primary" onclick="parametativ('creditoContent', event)">Crédito</button>
                                <button type="button" class="btn btn-primary" onclick="parametativ('gerencialContent', event)">Gerencial</button>
                                <button type="button" class="btn btn-primary" onclick="parametativ('formalizacaoContent', event)">Formalização</button>
                                <button type="button" class="btn btn-primary" onclick="parametativ('fundoQuataContent', event)">Fundo-Quata</button>
                                <button type="button" class="btn btn-primary" onclick="parametativ('financeiroContent', event)">Financeiro</button>
                                <button type="button" class="btn btn-primary" onclick="parametativ('rhContent', event)">RH</button>
                                <button type="button" class="btn btn-primary" onclick="parametativ('pjContent', event)">PJ</button>
                            </div>

                            <div class="form-group">
                                <label for="txtNomeFavorito">Nome do Favorito</label>
                                <asp:TextBox ID="txtNomeFavorito" runat="server" CssClass="form-control" />
                            </div>


                            <div id="parametrosContent" class="desativado">
                                <label for="ddlPaginas">Parâmetros</label>
                                <asp:DropDownList ID="ddlPaginas1" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Selecione uma página" Value="" />
                                    <asp:ListItem Text="--------Parâmetros--------" Value="" />
                                    <asp:ListItem Text="Usuários" Value="../Paginas/Cadastro/Usuarios.aspx" />
                                    <asp:ListItem Text="Services" Value="../Paginas/Cadastro/Usu_services.aspx" />
                                    <asp:ListItem Text="Agentes Operadores" Value="../Paginas/Cadastro/Usu_Operador_SVC.aspx" />
                                    <asp:ListItem Text="--------Comissão PJ--------" Value="" />
                                    <asp:ListItem Text="Meta dos operadores" Value="../Paginas/Cadastro/Comissao PJ/MetaOperadores.aspx" />
                                    <asp:ListItem Text="TC Por Operação x Meta" Value="../Paginas/Cadastro/Comissao PJ/TcOperacaoXMeta.aspx" />
                                    <asp:ListItem Text="Taxa Comissão x META" Value="../Paginas/Cadastro/Comissao PJ/TaxaComissãoXMeta.aspx" />
                                    <asp:ListItem Text="Taxa Reduzida" Value="../Paginas/Cadastro/Comissao PJ/TxReduz.aspx" />
                                    <asp:ListItem Text="Desconto" Value="../Paginas/Cadastro/Comissao PJ/Desconto.aspx" />
                                    <asp:ListItem Text="Operador Atende Cliente" Value="../Paginas/Cadastro/Comissao PJ/OperadorAtendeCliente.aspx" />
                                    <asp:ListItem Text="Cliente Comissão" Value="../Paginas/Cadastro/Comissao PJ/ClienteComissao.aspx" />
                                    <asp:ListItem Text="Cliente - Entrada na Santana" Value="../Paginas/Cadastro/Comissao PJ/ClienteEntrada.aspx" />
                                    <asp:ListItem Text="Fixo - Ajuda de Custo" Value="../Paginas/Cadastro/Comissao PJ/Fixo.aspx" />
                                    <asp:ListItem Text="Comissão PJ" Value="../Paginas/Cadastro/Comissao PJ/ComissaoPJ.aspx" />
                                    <asp:ListItem Text="Comissão PJ Analítico" Value="../Paginas/Cadastro/Comissao PJ/ComissaoPJAnalitico.aspx" />
                                    <asp:ListItem Text="Peso IP" Value="../Paginas/Cadastro/PesoIp.aspx" />
                                    <asp:ListItem Text="--------Cobrança--------" Value="" />
                                    <asp:ListItem Text="Cobradora" Value="../Paginas/Cadastro/Cobradora.aspx" />
                                    <asp:ListItem Text="Meta PDD" Value="../Paginas/Cadastro/MetaPDD.aspx" />
                                    <asp:ListItem Text="Meta ICM" Value="../Paginas/Cadastro/MetaICM.aspx" />
                                    <asp:ListItem Text="Meta ICM - Microcrédito" Value="../Paginas/Cadastro/MetaICMRepac.aspx" />
                                    <asp:ListItem Text="Meta BNDU" Value="../Paginas/Cadastro/MetaBNDU.aspx" />
                                    <asp:ListItem Text="--------PRODUTOS--------" Value="" />
                                    <asp:ListItem Text="Produtos" Value="../Paginas/Cadastro/Produto.aspx" />
                                    <asp:ListItem Text="Tipo de Produto" Value="../Paginas/Cadastro/TipoProduto.aspx" />
                                    <asp:ListItem Text="Modalidade" Value="../Paginas/Cadastro/Modalidade.aspx" />
                                    <asp:ListItem Text="--------SPREAD--------" Value="" />
                                    <asp:ListItem Text="Cubo" Value="../Paginas/Cadastro/Cubo.aspx" />
                                    <asp:ListItem Text="Taxa de Captação" Value="../Paginas/Cadastro/TaxCaptacao.aspx" />
                                    <asp:ListItem Text="% Comissão" Value="../Paginas/Cadastro/Comissao.aspx" />
                                    <asp:ListItem Text="Salário Mínimo" Value="../Paginas/Cadastro/SalarioMinimo.aspx" />
                                    <asp:ListItem Text="Faixa de Ano - Spread e Cred.Cobr." Value="../Paginas/Cadastro/FaixaAnoSpread.aspx" />
                                    <asp:ListItem Text="Modelos" Value="../Paginas/Cadastro/Modelo.aspx" />
                                    <asp:ListItem Text="Faixa de Plano" Value="../Paginas/Cadastro/FaixaDePlano.aspx" />
                                    <asp:ListItem Text="Risco - Desconto" Value="../Paginas/Cadastro/Risco.aspx" />
                                    <asp:ListItem Text="Comissão - Desconto" Value="../Paginas/Cadastro/ComissaoDesconto.aspx" />
                                    <asp:ListItem Text="Faixa de Ano - Safra" Value="../Paginas/Cadastro/FaixaAnoSafra.aspx" />
                                    <asp:ListItem Text="--------Gerencial--------" Value="" />
                                    <asp:ListItem Text="Feriados" Value="../Paginas/Cadastro/Gerencial/Feriados.aspx" />
                                    <asp:ListItem Text="Conta - Comissão" Value="../Paginas/Cadastro/Gerencial/CadastroDeContaComissao.aspx" />
                                    <asp:ListItem Text="Operadores - Função X NF" Value="../Paginas/Cadastro/DeParaOperadoresFuncaoNF.aspx" />
                                    <asp:ListItem Text="Cadastro de Grupo PJ" Value="../Paginas/Cadastro/CadastroGrupoPJ.aspx" />
                                    <asp:ListItem Text="Contas" Value="../Paginas/Cadastro/Gerencial/Contas.aspx" />
                                    <asp:ListItem Text="Conta Cálculo" Value="../Paginas/Cadastro/Gerencial/ContaCalculo.aspx" />
                                    <asp:ListItem Text="Relatórios" Value="../Paginas/Cadastro/Gerencial/Relatorios.aspx" />
                                    <asp:ListItem Text="Relatório - Conteúdo" Value="../Paginas/Cadastro/Gerencial/RelatoriosItens.aspx" />
                                    <asp:ListItem Text="--------AR--------" Value="" />
                                    <asp:ListItem Text="Segmento" Value="../Paginas/Cadastro/AR/Segmento.aspx" />
                                    <asp:ListItem Text="Número Carta" Value="../Paginas/Cadastro/AR/NumeroCarta.aspx" />
                                    <asp:ListItem Text="Retorno" Value="../Paginas/Cadastro/AR/Retorno.aspx" />
                                    <asp:ListItem Text="Motivo" Value="../Paginas/Cadastro/AR/Motivo.aspx" />
                                    <asp:ListItem Text="De Para Ocorrencias" Value="../Paginas/Cadastro/AR/DeParaOcorrenciasAR.aspx" />
                                    <asp:ListItem Text="Potencial" Value="" />
                                    <asp:ListItem Text="Região" Value="../Paginas/Cadastro/PT/Regiao.aspx" />
                                    <asp:ListItem Text="Grupo" Value="../Paginas/Cadastro/PT/Grupo.aspx" />
                                    <asp:ListItem Text="UF" Value="../Paginas/Cadastro/PT/UF.aspx" />
                                    <asp:ListItem Text="Cidade" Value="../Paginas/Cadastro/PT/Cidade.aspx" />
                                    <asp:ListItem Text="--------Protesto--------" Value="" />
                                    <asp:ListItem Text="Ocorrencias Cartório" Value="../Paginas/Cadastro/Protesto/OcorrenciasCartorio.aspx" />
                                    <asp:ListItem Text="De Para Ocorrencias" Value="../Paginas/Cadastro/Protesto/DeParaOcorrencias.aspx" />
                                    <asp:ListItem Text="Contratos Exceção" Value="../Paginas/Cadastro/Protesto/ContratosExcecao.aspx" />
                                    <asp:ListItem Text="--------Formalização--------" Value="" />
                                    <asp:ListItem Text="Metas Mensal" Value="../Paginas/Cadastro/MetaFormaliz.aspx" />
                                    <asp:ListItem Text="Metas Promotoras" Value="../Paginas/Cadastro/MetaPromotoras.aspx" />
                                    <asp:ListItem Text="Metas Operadores" Value="../Paginas/Cadastro/MetaOperadoresInternos.aspx" />
                                    <asp:ListItem Text="De Para Agentes" Value="../Paginas/Cadastro/DeParaAgentes.aspx" />
                                    <asp:ListItem Text="De Para Gerência" Value="../Paginas/Cadastro/DeParaGerencia.aspx" />
                                    <asp:ListItem Text="Cadastro Região" Value="../Paginas/Cadastro/RegiaoCadastro.aspx" />
                                    <asp:ListItem Text="De Para Região" Value="../Paginas/Cadastro/DeParaRegiao.aspx" />
                                    <asp:ListItem Text="Equipe Dinamo" Value="../Paginas/Cadastro/EquipeDinamo.aspx" />
                                    <asp:ListItem Text="Cadastro Equipe" Value="../Paginas/Cadastro/CadastroEquipe.aspx" />
                                    <asp:ListItem Text="Empresa Registro" Value="../Paginas/Cadastro/EmpresaRegistro.aspx" />
                                    <asp:ListItem Text="Cadastro Operador Substabelecido" Value="../Paginas/Cadastro/OperadorSubstabelecido.aspx" />
                                    <asp:ListItem Text="Cadastro Promotoras - Adiantamento" Value="../Paginas/Cadastro/PromotorasAdiantamento.aspx" />
                                    <asp:ListItem Text="--------Risco Crédito--------" Value="" />
                                    <asp:ListItem Text="Faixas Score" Value="../Paginas/Cadastro/RcFaixaScore.aspx" />
                                    <asp:ListItem Text="--------Ajuste Proposta--------" Value="" />
                                    <asp:ListItem Text="Score" Value="../Paginas/Cadastro/Score.aspx" />
                                    <asp:ListItem Text="Data Base" Value="../Paginas/Cadastro/Database.aspx" />
                                    <asp:ListItem Text="Alteração Garantia" Value="../Paginas/Cadastro/AlteracaoGarantia.aspx" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="cadastrosContent" class="desativado">
                            <!--cadastro-->
                            <label for="ddlPaginas">Cadastros</label>
                            <asp:DropDownList ID="ddlPaginas2" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Selecione uma página" Value="" />
                                <asp:ListItem Text="--------Cadastro--------" Value="" />
                                <asp:ListItem Text="Tabelas" Value="../Paginas/Cadastro/Cadastro/Tabelas.aspx" />
                                <asp:ListItem Text="Produtos" Value="../Paginas/Cadastro/Cadastro/Produtos.aspx" />
                                <asp:ListItem Text="Lojas" Value="../Paginas/Cadastro/Cadastro/Lojas.aspx" />
                                <asp:ListItem Text="Promotoras" Value="../Paginas/Cadastro/Cadastro/Promotoras.aspx" />
                                <asp:ListItem Text="Operadores" Value="../Paginas/Cadastro/Cadastro/Operadores.aspx" />
                            </asp:DropDownList>
                        </div>
                        <!--END cadastro-->
                        <div id="cobrancaContent" class="desativado">
                            <!--Cobrança-->
                            <label for="ddlPaginas">Cobrança</label>
                            <asp:DropDownList ID="ddlPaginas3" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Selecione uma página" Value="" />
                                <asp:ListItem Text="Rollrate Mensal" Value="../Paginas/RollrateMensal.aspx" />
                                <asp:ListItem Text="Rollrate Diário" Value="RollrateDiario.aspx" />
                                <asp:ListItem Text="Rollrate Projetado - Analítico" Value="RollrateProjetadoAnalitico.aspx" />
                                <asp:ListItem Text="--------Relatórios--------" Value="" />
                                <asp:ListItem Text="Parcela Pulada" Value="Cobranca/Relatorios/ParcelaPulada.aspx" />
                                <asp:ListItem Text="Recebimento de Parcela" Value="Cobranca/Relatorios/RecebParcela.aspx" />
                                <asp:ListItem Text="Pos. Cobrança Reneg/Repac" Value="Cobranca/Relatorios/PosCobRenegRepac.aspx" />
                                <asp:ListItem Text="--------Controle de Ações--------" Value="" />
                                <asp:ListItem Text="Controle de Ações - BA" Value="Cobranca/Relatorios/ControleAcoes.aspx" />
                                <asp:ListItem Text="Controle de Ações - PF/PJ" Value="Cobranca/Relatorios/ControleAcoesPJ.aspx" />
                                <asp:ListItem Text="--------URA--------" Value="" />
                                <asp:ListItem Text="URA - gerar arquivo" Value="UraGerarArquivo.aspx" />
                                <asp:ListItem Text="--------Carteira--------" Value="" />
                                <asp:ListItem Text="Carteira 3 Meses" Value="Carteira3M.aspx" />
                                <asp:ListItem Text="--------ARs--------" Value="" />
                                <asp:ListItem Text="ARs" Value="Cobranca/Relatorios/ARs.aspx" />
                                <asp:ListItem Text="Renegociados" Value="Cobranca/Relatorios/baseReneg.aspx" />
                                <asp:ListItem Text="Ocorrencias" Value="Cobranca/Relatorios/Ocorrencia.aspx" />
                                <asp:ListItem Text="Repactuação" Value="Cobranca/Relatorios/Repactuacao.aspx" />
                                <asp:ListItem Text="--------Honorários--------" Value="" />
                                <asp:ListItem Text="Honorários" Value="Cobranca/Relatorios/Honorarios.aspx" />
                                <asp:ListItem Text="--------ICM--------" Value="" />
                                <asp:ListItem Text="Forçar ICM" Value="Cobranca/ICM/ForcarICM.aspx" />
                                <asp:ListItem Text="--------FOP--------" Value="" />
                                <asp:ListItem Text="Arquivo FOP" Value="Cobranca/FOP/ArquivoFOP.aspx" />
                            </asp:DropDownList>
                        </div>

                        <!--END Cobrança-->
                        <div id="riscoContent" class="desativado">
                            <!--Risco-->
                            <label for="ddlPaginas">Risco</label>
                            <asp:DropDownList ID="ddlPaginas4" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Selecione uma página" Value="" />
                                <asp:ListItem Text="--------Gráficos--------" Value="" />
                                <asp:ListItem Text="Carteira" Value="Graficos/GraficoCarteira.aspx" />
                                <asp:ListItem Text="Rolagens" Value="Graficos/Rolagem.aspx?opcao=1" />
                                <asp:ListItem Text="Perda" Value="Graficos/Rolagem.aspx?opcao=2" />
                                <asp:ListItem Text="Estoque e Comportamento" Value="Graficos/Estoque.aspx" />
                                <asp:ListItem Text="Consolidado" Value="Cobranca/Consolidado/GraficoRolagemConsolidado.aspx" />

                                <asp:ListItem Text="--------Protesto--------" Value="" />
                                <asp:ListItem Text="Gerar Remessa" Value="Protesto/GerarRemessa.aspx" />
                                <asp:ListItem Text="Carga Confirmação" Value="Protesto/CargaConfirmacao.aspx" />
                                <asp:ListItem Text="Carga Retorno" Value="Protesto/CargaRetorno.aspx" />
                                <asp:ListItem Text="Gera Captura GV" Value="Protesto/GerarCapturaGV.aspx" />
                                <asp:ListItem Text="Acompanhamento Protesto" Value="Protesto/Protesto.aspx" />

                                <asp:ListItem Text="--------Risco Gerencial--------" Value="" />
                                <asp:ListItem Text="p123 Gerencial" Value="Cobranca/P123Gerencial/P123Gerencial.aspx" />
                                <asp:ListItem Text="Inadimplência" Value="Cobranca/P123Gerencial/Inadimplencia.aspx" />
                                <asp:ListItem Text="Score" Value="Cobranca/P123Gerencial/Risco.aspx" />
                                <asp:ListItem Text="Score x Inadimplencia (visão 13 meses)" Value="Cobranca/P123Gerencial/P123Gerencia13Meses.aspx" />

                                <asp:ListItem Text="--------IP--------" Value="" />
                                <asp:ListItem Text="Indice de Performance" Value="IndicePerformance.aspx" />
                                <asp:ListItem Text="IP Valor Recebido" Value="IndicePerformanceVR.aspx" />
                                <asp:ListItem Text="IP Safra Diário" Value="IPSafraDiaria.aspx" />
                                <asp:ListItem Text="IP Analítico" Value="Cobranca/Relatorios/IPAnalitico.aspx" />
                                <asp:ListItem Text="--------Ajuste--------" Value="" />
                                <asp:ListItem Text="Ajuste IP" Value="Cobranca/Relatorios/AjusteIP.aspx" />
                            </asp:DropDownList>
                        </div>
                        <!--END Risco-->
                        <div id="comCreditoContent" class="desativado">

                            <!--COM.RÉDITO-->
                            <label for="ddlPaginas">Com-Crédito</label>
                            <asp:DropDownList ID="ddlPaginas5" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Selecione uma página" Value="" />
                                <asp:ListItem Text="--------Com.-Crédito--------" Value="" />
                                <asp:ListItem Text="Operações Realizadas" Value="FechaComercial/FechaComCreOpRealizadas.aspx" />
                                <asp:ListItem Text="Operações Realizadas - Analítico" Value="FechaComercial/OpRealizadasAnalitica.aspx" />
                                <asp:ListItem Text="Produção Mensal / Produto (Gráf.)" Value="FechaComercial/FechaComCreProducaoProdutos.aspx" />
                                <asp:ListItem Text="Vlr Operado - Produção Mensal / Produto (Gráf.)" Value="FechaComercial/FechaComCreProducaoProdutosOP.aspx" />
                                <asp:ListItem Text="Análise de Produção" Value="FechaComercial/FechaComCreAnaliseProd.aspx" />
                                <asp:ListItem Text="Análise de Propostas Operador" Value="FechaComercial/FechaComPropostasOp.aspx" />
                                <asp:ListItem Text="Histórico Analise de Propostas" Value="FechaComercial/FechaComPropostasHis.aspx" />
                                <asp:ListItem Text="Financiamento por Tipo de Veículo" Value="FechaComercial/FechaComTipoVeiculo.aspx" />
                                <asp:ListItem Text="Análise de Veículos Plano" Value="FechaComercial/FechaComAnaliseVeiculoPlano.aspx" />
                                <asp:ListItem Text="Análise de Veículos Ano" Value="FechaComercial/FechaComAnaliseVeiculoAno.aspx" />
                                <asp:ListItem Text="Análise Tipo Ano" Value="FechaComercial/FechaComAnaliseVeiculoTipoAno.aspx" />
                                <asp:ListItem Text="Análise Maiores Clientes" Value="FechaComercial/FechaComClientes.aspx" />
                                <asp:ListItem Text="Score Médio por Agente / Mensal" Value="FechaComercial/FechaComScoreMedio.aspx" />
                                <asp:ListItem Text="Pagas X Recebidas" Value="FechaComercial/PagasRecebidas.aspx" />
                                <asp:ListItem Text="Base Fipe" Value="FechaComercial/Fipe.aspx" />
                                <asp:ListItem Text="Base de Recebimentos" Value="Comercial/Recebimento.aspx" />

                                <asp:ListItem Text="--------Comissão--------" Value="" />
                                <asp:ListItem Text="Veículos" Value="FechaComercial/Comissao.aspx" />
                                <asp:ListItem Text="Dínamo" Value="FechaComercial/ComissaoDinamo.aspx" />
                                <asp:ListItem Text="Adiantamento" Value="FechaComercial/ComissaoAdiantamento.aspx" />
                                <asp:ListItem Text="Sintético" Value="FechaComercial/ComissaoSintetico.aspx" />
                                <asp:ListItem Text="Gerar Arquivo" Value="FechaComercial/GerarArquivoComissao.aspx" />

                                <asp:ListItem Text="Base Propostas - Mesa" Value="Comercial/BasePropostaTempo.aspx" />
                                <asp:ListItem Text="Cadastro de Lojas" Value="Comercial/RelCadastroLojas.aspx" />
                            </asp:DropDownList>
                        </div>
                        <!--END COM.CRÉDITO-->
                        <div id="comercialContent" class="desativado">

                            <!--COMERCIAL-->
                            <label for="ddlPaginas">Comercial</label>
                            <asp:DropDownList ID="ddlPaginas6" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Selecione uma página" Value="" />
                                <asp:ListItem Text="--------Comercial--------" Value="" />
                                <asp:ListItem Text="Mapa Comercial" Value="Comercial/MapaComercial.aspx" />
                                <asp:ListItem Text="Mapa PDV - PJ" Value="Comercial/MapaPDV.aspx" />
                                <asp:ListItem Text="Mapa PDV - Veículos" Value="Comercial/MapaPDVveicLOJA.aspx" />
                                <asp:ListItem Text="Potencial MarketShare" Value="Comercial/PontecialMktShare.aspx" />

                                <asp:ListItem Text="--------CRV--------" Value="" />
                                <asp:ListItem Text="Pendências de CRV" Value="Comercial/PendenciasCRV.aspx" />
                                <asp:ListItem Text="Pendências Antigas de CRV" Value="Comercial/PendenciasCRVInativos.aspx" />
                                <asp:ListItem Text="Pendências CRV - Baixa" Value="Comercial/PendenciasCRVBaixa.aspx" />

                                <asp:ListItem Text="Pendências de Contrato" Value="Comercial/PendenciasContrato.aspx" />
                                <asp:ListItem Text="Pendências de Contrato - Sint." Value="Comercial/PendenciasContratoSint.aspx" />
                                <asp:ListItem Text="Pendência de Gravames" Value="COMERCIAL/PendenciasGravames.aspx" />
                                <asp:ListItem Text="Pendência de Gravames - Sint." Value="COMERCIAL/PendenciasGravameSint.aspx" />
                                <asp:ListItem Text="Contratos Pagos com Pendência" Value="Comercial/PagtoPendencia.aspx" />
                                <asp:ListItem Text="Contratos Pagos com Pendência - Sint." Value="Comercial/ContratoPgPendSint.aspx" />
                                <asp:ListItem Text="Base Prestamista" Value="Comercial/Prestamista.aspx" />
                                <asp:ListItem Text="Acionamento de Clientes" Value="COMERCIAL/AcionamentoCliente.aspx" />
                                <asp:ListItem Text="Repacs para Refin" Value="COMERCIAL/RepacRefin.aspx" />
                                <asp:ListItem Text="Extração de Placas" Value="Comercial/ExtracaoPlacas.aspx" />
                                <asp:ListItem Text="Base NPS" Value="Comercial/BaseNPS.aspx" />
                                <asp:ListItem Text="Aniversariantes" Value="Comercial/Aniversariantes.aspx" />
                                <asp:ListItem Text="Aniversariantes PJ" Value="Comercial/AniversariantesPJ.aspx" />
                                <asp:ListItem Text="Quitação Contrato" Value="Comercial/QuitacaoContrato.aspx" />
                                <asp:ListItem Text="Carteira de Clientes Ativos e Inativos" Value="Comercial/CarteiraClienteAtivoInativo.aspx" />

                                <asp:ListItem Text="--------CETIP--------" Value="" />
                                <asp:ListItem Text="Conversão CETIP" Value="../Paginas/Cetip/CETIPCruzamento.aspx" />
                                <asp:ListItem Text="Conversão Sintético CETIP" Value="../Paginas/Cetip/CETIPSintetico.aspx" />
                                <asp:ListItem Text="Ranking CETIP" Value="../Paginas/Cetip/CETIPRankingConsulta.aspx" />
                                <asp:ListItem Text="Potencial Analítico CETIP" Value="../Paginas/Cetip/CETIPPotencialAnaliticoConsulta.aspx" />
                                <asp:ListItem Text="Potencial Sintético CETIP" Value="../Paginas/Cetip/CETIPPotencialSinteticoConsulta.aspx" />

                                <asp:ListItem Text="--------Cobrança P123--------" Value="" />
                                <asp:ListItem Text="Cobrança P123 - Veículos" Value="../Paginas/Comercial/CobrançaP123.aspx" />
                                <asp:ListItem Text="Cobrança P123 - Microcrédito" Value="../Paginas/Comercial/CobrançaP123Microcredito.aspx" />

                                <asp:ListItem Text="Parcelas por Vencimento" Value="Cobranca/Relatorios/ParcelaVcto.aspx" />
                                <asp:ListItem Text="Consulta Carnê" Value="Comercial/ConsultaCarne.aspx" />
                                <asp:ListItem Text="Consulta Carnê - Sint." Value="Comercial/ConsultaCarneSintetico.aspx" />

                            </asp:DropDownList>
                        </div>
                        <!--END COMERCIAL-->
                        <div id="creditoContent" class="desativado">

                            <!--CRÉDITO-->
                            <label for="ddlPaginas">Crédito</label>
                            <asp:DropDownList ID="ddlPaginas7" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Selecione uma página" Value="" />
                                <asp:ListItem Text="--------Crédito--------" Value="" />
                                <asp:ListItem Text="Análise Analista" Value="Credito/AnaliseAnalista.aspx" />
                                <asp:ListItem Text="Produção Analista" Value="Credito/ProducaoAnalista.aspx" />
                                <asp:ListItem Text="Inadimplencia Analista" Value="Credito/InadinplenciaPorAnalista.aspx" />
                                <asp:ListItem Text="P123 Analista" Value="Credito/P123Analista.aspx" />

                                <asp:ListItem Text="--------Sintéticos--------" Value="" />
                                <asp:ListItem Text="Sintético Analista (tempos)" Value="Credito/SinteticoAnalise.aspx" />
                                <asp:ListItem Text="Sintético Inadimplência" Value="Credito/SinteticoInadimpl.aspx" />

                                <asp:ListItem Text="Monitor das Aprovadas em Exceção" Value="Credito/Excecao.aspx" />
                                <asp:ListItem Text="Monitor das Aprovadas com Redução" Value="FechaComercial/AprovadasReducao.aspx" />
                                <asp:ListItem Text="Propostas - NOK 65" Value="Credito/Propostas_nok65.aspx" />
                                <asp:ListItem Text="Tempo Total de Propostas" Value="Credito/TempoTotal.aspx" />

                                <asp:ListItem Text="--------Base de Propostas--------" Value="" />
                                <asp:ListItem Text="Base de Propostas" Value="Credito/BaseProposta.aspx" />
                                <asp:ListItem Text="Resumo Base de Propostas" Value="Credito/ResumoProposta.aspx" />
                                <asp:ListItem Text="Pagas x Recebidas - Analítica" Value="Credito/PagaRecebidaAnalitica.aspx" />
                                <asp:ListItem Text="Recebidas x Abandonadas" Value="Credito/Abandonadas.aspx" />
                                <asp:ListItem Text="Recebidas x Abandonadas - Analítica" Value="Credito/AbandonadasAnali.aspx" />
                                <asp:ListItem Text="Motivos Pen. Rec. Proposta" Value="Credito/MotivoPenRecProposta.aspx" />
                                <asp:ListItem Text="Crivo - Aprov. Automática" Value="Credito/Crivo_Apr_Aut.aspx" />

                                <asp:ListItem Text="--------Esteira de Crédito--------" Value="" />
                                <asp:ListItem Text="Tempo - Passo da Esteira Crédito" Value="Credito/PassoEsteira.aspx" />
                                <asp:ListItem Text="Dashboard Analítico Crédito" Value="Credito/TempoCreditoAnalitico.aspx" />

                            </asp:DropDownList>
                        </div>
                        <!--END CRÉDITO-->
                        <div id="gerencialContent" class="desativado">
                            <!--GERENCIAL-->
                            <label for="ddlPaginas">Gerencial</label>
                            <asp:DropDownList ID="ddlPaginas8" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Selecione uma página" Value="" />
                                <asp:ListItem Text="--------Gerencial--------" Value="" />
                                <asp:ListItem Text="Cubo" Value="../Paginas/Cadastro/Gerencial/Cubo.aspx" />
                                <asp:ListItem Text="Balancete" Value="../Paginas/Cadastro/Gerencial/Balancete.aspx" />
                                <asp:ListItem Text="Relatório Padrão 1" Value="../Paginas/Cadastro/Gerencial/PadraoTrezeMeses.aspx" />
                                <asp:ListItem Text="Relatório Padrão 2" Value="../Paginas/Cadastro/Gerencial/PadraoDozeMeses.aspx" />
                                <asp:ListItem Text="Balanço" Value="../Paginas/Cadastro/Gerencial/Balanco.aspx" />
                                <asp:ListItem Text="Liquidados" Value="../Paginas/Cadastro/Gerencial/Liquidados.aspx" />
                                <asp:ListItem Text="Validação Cadastral" Value="../Paginas/Cadastro/Gerencial/ValidacaoCadastral.aspx" />
                                <asp:ListItem Text="--------DRE e Colaboradores--------" Value="" />
                                <asp:ListItem Text="DRE - Contas (carga)" Value="../Paginas/Cadastro/Gerencial/DRECarga.aspx" />
                                <asp:ListItem Text="Colaboradores - PLD" Value="../Paginas/Cadastro/Gerencial/ColaboradoresPLD.aspx" />
                                <asp:ListItem Text="Segunda Garantia" Value="../Paginas/Comercial/SegundaGarantia.aspx" />

                                <asp:ListItem Text="--------CETIP--------" Value="" />
                                <asp:ListItem Text="Download Retorno" Value="Cetip/CETIPupload.aspx" />
                                <asp:ListItem Text="Gerar Remessa" Value="Cetip/CETIPGerarArquivo.aspx" />
                                <asp:ListItem Text="Ranking CETIP - Carga" Value="../Paginas/Cetip/CETIPRanking.aspx" />
                                <asp:ListItem Text="Potencial Analitico CETIP - Carga" Value="../Paginas/Cetip/CETIPPotencialAnalitico.aspx" />
                                <asp:ListItem Text="Potencial Sintetico CETIP - Carga" Value="../Paginas/Cetip/CETIPPotencialSintetico.aspx" />

                                <asp:ListItem Text="--------Comissão PJ--------" Value="" />
                                <asp:ListItem Text="Comissão PJ" Value="../Paginas/Cadastro/Comissao PJ/ComissaoPJ.aspx" />
                                <asp:ListItem Text="Comissão PJ Analítico" Value="../Paginas/Cadastro/Comissao PJ/ComissaoPJAnalitico.aspx" />

                                <asp:ListItem Text="--------PJ--------" Value="" />
                                <asp:ListItem Text="Assinatura Digital" Value="../Paginas/PJ/AssinaDigital.aspx" />

                            </asp:DropDownList>
                        </div>

                        <!--END GERENCIAL-->
                        <div id="formalizacaoContent" class="desativado">
                            <!--FORMALIZ-->
                            <label for="ddlPaginas">Formalização</label>
                            <asp:DropDownList ID="ddlPaginas9" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Selecione uma página" Value="" />
                                <asp:ListItem Text="--------Formaliz--------" Value="" />
                                <asp:ListItem Text="Pendências de CRV Sintético" Value="Comercial/PendenciasCRVsint.aspx" />
                                <asp:ListItem Text="Pendências de Formalização" Value="Comercial/PendenciasFormalizacao.aspx" />
                                <asp:ListItem Text="Pendências de Formalização - Analítico" Value="Comercial/PendenciasFormalizacaoAnalitico.aspx" />
                                <asp:ListItem Text="Pendência de Registro de Contrato" Value="Formaliz/PendenciaRegistroContrato.aspx" />

                                <asp:ListItem Text="--------Confere Pagamentos--------" Value="" />
                                <asp:ListItem Text="Detalhado" Value="Comercial/ConferePagto_Det.aspx" />
                                <asp:ListItem Text="Resumido" Value="Comercial/ConferePagto.aspx" />

                                <asp:ListItem Text="--------Gravame--------" Value="" />
                                <asp:ListItem Text="Inclusão" Value="Comercial/GravameInclusao.aspx" />
                                <asp:ListItem Text="Quitação" Value="Comercial/GravameQuitacao.aspx" />

                                <asp:ListItem Text="--------Pendência CRV - Carteira Ativa--------" Value="" />
                                <asp:ListItem Text="Analítico" Value="Comercial/PendenciasCRVCartAtivaAnali.aspx" />
                                <asp:ListItem Text="Sintético" Value="Comercial/PendenciasCRVCartAtivaSint.aspx" />
                                <asp:ListItem Text="Liquidados" Value="Comercial/PendenciasCRVCartAtivaLiq.aspx" />

                                <asp:ListItem Text="Substituição de Garantia" Value="Comercial/SubstituicaoGarantia.aspx" />
                                <asp:ListItem Text="Pago na Cta Agente" Value="Formaliz/PagtoCtaAgente.aspx" />
                                <asp:ListItem Text="Tempo - Passo da Esteira Formalização" Value="Formaliz/PassoEsteiraFormaliz.aspx" />
                                <asp:ListItem Text="Dashboard Analítico Formalização" Value="Formaliz/TempoFormalizAnalitico.aspx" />
                                <asp:ListItem Text="Propostas Balcão" Value="Comercial/PropostasBalcao.aspx" />
                                <asp:ListItem Text="Consulta Crivo" Value="Comercial/ConsultaCrivo.aspx" />
                                <asp:ListItem Text="Base SMS" Value="Comercial/BaseSMS.aspx" />
                                <asp:ListItem Text="Assinatura Digital" Value="Formaliz/AssinaturaDigital.aspx" />
                                <asp:ListItem Text="Registro Contrato" Value="Formaliz/RegistroGravame.aspx" />

                            </asp:DropDownList>
                        </div>
                        <!--END FORMALIZL-->
                        <div id="fundoQuataContent" class="desativado">

                            <!--FUNDO QUATAL-->
                            <label for="ddlPaginas">Fundo Quata</label>
                            <asp:DropDownList ID="ddlPaginas10" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Selecione uma página" Value="" />
                                <asp:ListItem Text="--------Extraordinário--------" Value="" />
                                <asp:ListItem Text="Cnab - Carga" Value="../Paginas/FundoQuata/Cnab550.aspx" />
                                <asp:ListItem Text="Cnab - Gerar" Value="../Paginas/FundoQuata/GerarCnab.aspx" />
                                <asp:ListItem Text="Cnab - Atualizar" Value="../Paginas/FundoQuata/CnabBaixaInclusao.aspx" />

                                <asp:ListItem Text="--------Contratos Inexistente--------" Value="" />
                                <asp:ListItem Text="Atualizar - Alterada" Value="../Paginas/FundoQuata/CnabAlter.aspx" />
                                <asp:ListItem Text="Gerar - Alterada" Value="../Paginas/FundoQuata/GerarCnabAlter.aspx" />

                                <asp:ListItem Text="--------Ordinário--------" Value="" />
                                <asp:ListItem Text="Cnab - Carga" Value="../Paginas/FundoQuata/OrdinarioCarga_O.aspx" />
                                <asp:ListItem Text="Cnab - Gerar" Value="../Paginas/FundoQuata/OrdinarioCnabGerar_O.aspx" />
                                <asp:ListItem Text="Cnab - Atualizar" Value="../Paginas/FundoQuata/OrdinarioCnabAtualizar_O.aspx" />

                                <asp:ListItem Text="--------Contratos Inexistente--------" Value="" />
                                <asp:ListItem Text="Atualizar - Alterada" Value="../Paginas/FundoQuata/OrdinarioCnabCriticaAtualizar_O.aspx" />
                                <asp:ListItem Text="Gerar - Alterada" Value="../Paginas/FundoQuata/OrdinarioCnabCriticaGerar_O.aspx" />

                                <asp:ListItem Text="--------Cnab Renegociação--------" Value="" />
                                <asp:ListItem Text="Cnab - Carga" Value="../Paginas/FundoQuata/RenegCnabCarga_RN.aspx" />
                                <asp:ListItem Text="Cnab - Gerar" Value="../Paginas/FundoQuata/RenegCnabGerar_RN.aspx" />

                                <asp:ListItem Text="--------Cnab Estorno--------" Value="" />
                                <asp:ListItem Text="Cnab - Carga" Value="../Paginas/FundoQuata/EstornoCnabCarga_ES.aspx" />
                                <asp:ListItem Text="Cnab - Gerar" Value="../Paginas/FundoQuata/EstornoCnabGerar_ES.aspx" />

                            </asp:DropDownList>
                        </div>

                        <!--END FUNDO QUATA-->
                        <div id="financeiroContent" class="desativado">
                            <!--FINANCEIRO-->
                            <label for="ddlPaginas">Financeiro</label>
                            <asp:DropDownList ID="ddlPaginas11" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Selecione uma página" Value="" />
                                <asp:ListItem Text="--------Financeiro--------" Value="" />

                                <asp:ListItem Text="Operação Captação" Value="Financeiro/OperacaoCaptacao.aspx" />
                                <asp:ListItem Text="Base de Captação" Value="Financeiro/BaseCaptacao.aspx" />
                                <asp:ListItem Text="Cadastro de Taxa CDI" Value="../Paginas/Cadastro/Financeiro/TaxaCDI.aspx" />
                                <asp:ListItem Text="Lançamento de Caixa" Value="../Paginas/Cadastro/Financeiro/Caixa.aspx" />
                                <asp:ListItem Text="Remessa" Value="Financeiro/Remessa.aspx" />

                            </asp:DropDownList>
                        </div>

                        <!--END FINANCEIRO-->
                        <div id="rhContent" class="desativado">
                            <!--RH-->
                            <label for="ddlPaginas">RH</label>
                            <asp:DropDownList ID="ddlPaginas12" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Selecione uma página" Value="" />
                                <asp:ListItem Text="--------RH--------" Value="" />

                                <asp:ListItem Text="CP Colaboradores" Value="RH/CPColaboradores.aspx" />
                                <asp:ListItem Text="CP Colaboradores - Sintético" Value="RH/CPColaboradoresSintetico.aspx" />

                            </asp:DropDownList>
                        </div>
                        <!--END RHL-->
                        <div id="pjContent" class="desativado">
                            <!--PJ-->
                            <label for="ddlPaginas">PJ</label>
                            <asp:DropDownList ID="ddlPaginas13" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Selecione uma página" Value="" />
                                <asp:ListItem Text="--------PJ--------" Value="" />

                                <asp:ListItem Text="Produção" Value="../Paginas/Desconto/DescontoProducao.aspx" />
                                <asp:ListItem Text="80 20" Value="../Paginas/Desconto/Desconto8020.aspx" />
                                <asp:ListItem Text="Cobrança PJ" Value="../Paginas/Desconto/CobrancaPJ.aspx" />
                                <asp:ListItem Text="Contratos por Operador" Value="../Paginas/Desconto/ContratoOperador.aspx" />
                                <asp:ListItem Text="Onboarding" Value="../Paginas/Desconto/Onboarding.aspx" />
                                <asp:ListItem Text="Assertiva" Value="../Paginas/Desconto/Assertiva.aspx" />

                            </asp:DropDownList>
                            <!--END PJ-->
                        </div>

                    </div>
                    <div class="modal-footer">

                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                        <asp:Button ID="btnFavoritar" runat="server" Text="Favoritar" CssClass="btn btn-primary" OnClick="btnFavoritar_Click" />
                        <asp:Button ID="btnExcluirFavorito" runat="server" Text="Excluir Favorito" CssClass="btn btn-danger" OnClick="btnExcluirFavorito_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div style="display: flex; justify-content: space-between; align-items: flex-start;">
            <!-- Seção de Favoritos -->
            <div style="flex: 1; padding-right: 20px; margin-left: 50px;">
                <h2>Seções Favoritas</h2>
                <p>Organize as páginas mais usadas como favoritas para acessar de forma mais rápida e melhorar sua eficiência no sistema. </p>
                <button type="button"  style="width:180px ; margin-bottom:10px; font-style:bold; border: none; color:white; border-radius: 5px; background-color:#152B61; height:35px; " data-toggle="modal";" data-target="#favoritoModal">
                           Gerenciar Favoritos
                 </button>
                <div>
                    <asp:Literal ID="litFavoritos" runat="server"></asp:Literal>
                </div>
            </div>
            <!-- Seção de Imagens -->
            <div style="flex: 1; text-align: center;">
                <img src="/imagens/SIG 1.png" style="max-width: 100%; height: auto;" />
            </div>
        </div>
        <asp:Table ID="TbGeral" runat="server" Width="100%" CellPadding="0" CellSpacing="100">
            <asp:TableRow ID="TableRow1" runat="server" HorizontalAlign="Center">
                <asp:TableCell ID="TableCell2" Width="100%" Height="100%" runat="server" VerticalAlign="Top">
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

    </div>

    <style>
        .favorite-box {
            background-color: rgba(255, 255, 255, 0.8);
            border: 1px solid darkblue;
            border-radius: 5px;
            padding: 15px;
            margin: 10px 0;
            transition: transform 0.2s;
            width: 40%;
        }


            .favorite-box:hover {
                transform: scale(1.05);
            }

        .favorite-link {
            color: black;
            text-decoration: none;
            font-weight: bold;
        }

            .favorite-link:hover {
                text-decoration: none;
                color: darkblue;
            }

        .modal.show {
            opacity: 1;
            pointer-events: auto;
        }

        .ativado {
            display: flex;
            flex-direction: column
        }

        .desativado {
            display: none;
        }
    </style>

    <script>


</script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>

        document.querySelector('button[data-toggle="modal"]').addEventListener('click', function (event) {
            event.preventDefault(); // Evita a submissão do formulário
        });
        function reloadPage() {
            location.reload();
        }

        function setPageUrl() {
            var selectedValue = document.getElementById("pageSelect").value;
            document.getElementById("hiddenPageUrl").value = selectedValue;

            if (selectedValue) {

                __doPostBack('<%= btnFavoritar.UniqueID %>', '');
            } else {
                alert("Por favor, selecione uma página.");
            }
        }


        let activeButton = null;
        function parametativ(contentId, event) {
            event.preventDefault();


            if (activeButton) {

                let previousContent = document.getElementById(activeButton);
                previousContent.classList.remove('ativado');
                previousContent.classList.add('desativado');
            }


            if (activeButton === contentId) {
                activeButton = null;
            } else {
                activeButton = contentId;
            }
            if (activeButton) {
                let currentContent = document.getElementById(activeButton);
                currentContent.classList.remove('desativado');
                currentContent.classList.add('ativado');
            }
        }

    </script>



</asp:Content>

