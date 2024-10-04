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
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Comissão PJ</a>
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
                                        <li><a href="Cobranca/Relatorios/AjusteIP.aspx">Ajuste IP</a></li>
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
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="bi bi-diagram-3-fill"></i> CETIP<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="Cetip/CETIPupload.aspx">Download Retorno</a></li>
                                <li><a href="Cetip/CETIPGerarArquivo.aspx">Gerar Remessa</a></li>
                                <li><a href="../Paginas/Cetip/CETIPRanking.aspx">Ranking CETIP - Carga</a></li>
                                <li><a href="../Paginas/Cetip/CETIPPotencialAnalitico.aspx">Potencial Analitico CETIP - Carga</a></li>
                                <li><a href="../Paginas/Cetip/CETIPPotencialSintetico.aspx">Potencial Sintetico CETIP - Carga</a></li>
                            </ul>
                        </li>

                        <li class="dropdown" id="Li3" runat="server" visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="bi bi-building-check"></i> Comissão PJ<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="../Paginas/Cadastro/Comissao PJ/ComissaoPJ.aspx">Comissão PJ</a></li>
                                <li><a href="../Paginas/Cadastro/Comissao PJ/ComissaoPJAnalitico.aspx">Comissão PJ Analítico</a></li>
                            </ul>
                        </li>

                        <li class="dropdown" id="PJ" runat="server" visible="false">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="bi bi-buildings"></i> PJ<b class="caret"></b></a>
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
                                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Ordinário</a>
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
                                <li><a href="Financeiro/OperacaoCaptacao.aspx">Operação Captação</a></li>
                                <li><a href="Financeiro/BaseCaptacao.aspx">Base de Captação</a></li>
                                <li><a href="../Paginas/Cadastro/Financeiro/TaxaCDI.aspx">Cadastro de Taxa CDI</a></li>
                                <li><a href="../Paginas/Cadastro/Financeiro/Caixa.aspx">Lançamento de Caixa</a></li>
                                <li><a href="Financeiro/Remessa.aspx">Remessa</a></li>
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
                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        <li><a runat="server" href="~/Paginas/Logon.aspx"><i class="bi bi-box-arrow-right"></i> Sair</a></li>
                    </ul>

                </div>
            </div>
        </div>

        <asp:Table ID="TbGeral" runat="server" Width="100%" CellPadding="0" CellSpacing="100"
            Height="510px">
            <asp:TableRow ID="TableRow1" runat="server" HorizontalAlign="Center">
                <asp:TableCell ID="TableCell2" Width="100%" Height="100%" runat="server" VerticalAlign="Top">
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

    </div>

    <style>
    </style>


    <script>

        function manutencao() {

            alert("Módulo em desenvolvimento")
        }

    </script>

</asp:Content>

