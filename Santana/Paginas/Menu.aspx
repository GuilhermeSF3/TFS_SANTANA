<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="false" CodeBehind="Menu.aspx.vb" Inherits="Santana.Menu" Title="Santana Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="dvMenu">

        <div class="navbar navbar-default ">
            <div class="container-full">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" runat="server" href="~/Paginas/Menu.aspx">Menu</a>
                </div>
                <div class="navbar-collapse collapse">
                    <ul class="nav navbar-nav" id="MenuRoot" runat="server">
                        <%-- <li class="dropdown">
                          <a href="#" class="dropdown-toggle" data-toggle="dropdown">Cadastro <b class="caret"></b></a>
                          <ul class="dropdown-menu">
                            <li><a href="#">Cadastro 1</a></li>
                            <li><a href="#">Cadastro 2</a></li>
                            <li class="divider"></li>
                            <li><a href="#">Cadastro abc</a></li>
                            <li class="divider"></li>
                            <li><a href="#">Cadastro xyz</a></li>
                          </ul>
                        </li>--%>

          <%--              <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">teste<b class="caret"></b></a>
                            <ul id="testes" class="dropdown-menu" runat="server">
                            </ul>style="visibility:hidden"
                        </li>--%>


                        <li class="dropdown" id="Parametros" runat="server" Visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Parametros<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="../Paginas/Cadastro/Usuarios.aspx">Usuários</a></li>

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
                                <li><a href="../Paginas/Cadastro/Cobradora.aspx">Cobradora</a></li>

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
                                    </ul>
                                </li>
                                
                                <li><a href="../Paginas/Cadastro/FaixaAnoSafra.aspx">Faixa de Ano - Safra</a></li>
                                
                                <li class="menu-item dropdown dropdown-submenu" id="ParametrosGerencial" runat="server" Visible="False">

                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Gerencial</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Paginas/Cadastro/Gerencial/Contas.aspx">Contas</a></li>
                                        <li><a href="../Paginas/Cadastro/Gerencial/ContaCalculo.aspx">Conta Cálculo</a></li>
                                        <li><a href="../Paginas/Cadastro/Gerencial/Relatorios.aspx">Relatórios</a></li>
                                        <li><a href="../Paginas/Cadastro/Gerencial/RelatoriosItens.aspx">Relatório - Conteúdo</a></li>
                                    </ul>
                                </li>
                                
                                <li class="menu-item dropdown dropdown-submenu" id="ParametrosAR" runat="server" Visible="False">

                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">AR</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Paginas/Cadastro/AR/Segmento.aspx">Segmento</a></li>
                                        <li><a href="../Paginas/Cadastro/AR/NumeroCarta.aspx">Número Carta</a></li>
                                        <li><a href="../Paginas/Cadastro/AR/Retorno.aspx">Retorno</a></li>
                                        <li><a href="../Paginas/Cadastro/AR/Motivo.aspx">Motivo</a></li>
                                        <li><a href="../Paginas/Cadastro/AR/DeParaOcorrenciasAR.aspx">De Para Ocorrencias</a></li>
                                    </ul>
                                </li>

                                <li class="menu-item dropdown dropdown-submenu" id="ParametrosPT" runat="server" Visible="False">

                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Potencial</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Paginas/Cadastro/PT/Regiao.aspx">Região</a></li>
                                        <li><a href="../Paginas/Cadastro/PT/Grupo.aspx">Grupo</a></li>
                                        <li><a href="../Paginas/Cadastro/PT/UF.aspx">UF</a></li>
                                        <li><a href="../Paginas/Cadastro/PT/Cidade.aspx">Cidade</a></li>
                                    </ul>
                                </li>


                                <li class="menu-item dropdown dropdown-submenu" id="ParametrosProtesto" runat="server" Visible="False">

                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Protesto</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Paginas/Cadastro/Protesto/OcorrenciasCartorio.aspx">Ocorrencias Cartório</a></li>
                                        <li><a href="../Paginas/Cadastro/Protesto/DeParaOcorrencias.aspx">De Para Ocorrencias</a></li>
                                        <li><a href="../Paginas/Cadastro/Protesto/ContratosExcecao.aspx">Contratos Exceção</a></li>
                                    </ul>
                                </li>


                            </ul>
                        </li>


                        <li class="dropdown" id="Cobranca" runat="server" Visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Cobrança<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li class="menu-item dropdown dropdown-submenu" id="Relatorios" runat="server" Visible="False">

                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Relatórios</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Paginas/RollrateMensal.aspx">Rollrate Mensal</a></li>
                                        <li><a href="RollrateDiario.aspx">Rollrate Diário</a></li>

                                        <li class="divider"></li>
                                        <li><a href="IndicePerformance.aspx">Indice de Performance</a></li>
                                        <li><a href="IndicePerformanceVR.aspx">IP Valor Recebido</a></li>
                                        <li><a href="IPSafraDiaria.aspx">IP Safra Diário</a></li>
                                        <li><a href="Cobranca/Relatorios/IPAnalitico.aspx">IP Analítico</a></li>

                                        <li class="divider"></li>
                                        <li><a href="UraGerarArquivo.aspx">URA - gerar arquivo</a></li>

                                        <li class="divider"></li>
                                        <li><a href="Carteira3M.aspx">Carteira 3 Meses</a></li>
                                        
                                        <li class="divider"></li>
                                        <li><a href="Cobranca/Relatorios/ARs.aspx">ARs</a></li>

                                    </ul>
                                </li>

                                <li class="menu-item dropdown dropdown-submenu" id="Graficos" runat="server" Visible="False">

                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Gráficos</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="Graficos/GraficoCarteira.aspx">Carteira</a></li>
                                        <li><a href="Graficos/Rolagem.aspx?opcao=1">Rolagens</a></li>
                                        <li><a href="Graficos/Rolagem.aspx?opcao=2">Perda</a></li>
                                        <li><a href="Graficos/Estoque.aspx">Estoque e Comportamento</a></li>
                                    </ul>
                                </li>

                                <li class="menu-item dropdown dropdown-submenu" id="Protesto" runat="server" Visible="False">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Protesto</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="Protesto/GerarRemessa.aspx">Gerar Remessa</a></li>
                                        <li><a href="Protesto/CargaConfirmacao.aspx">Carga Confirmação</a></li>
                                        <li><a href="Protesto/CargaRetorno.aspx">Carga Retorno</a></li>
                                        <li><a href="Protesto/GerarCapturaGV.aspx">Gera Captura GV</a></li>
                                        <li><a href="Protesto/Protesto.aspx">Acompanhamento Protesto</a></li>
                                    </ul>
                                </li>

                                <li class="menu-item dropdown dropdown-submenu" id="RiscoGerencial" runat="server" Visible="False">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">Risco Gerencial</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="Cobranca/P123Gerencial/P123Gerencial.aspx">p123 Gerencial</a></li>
                                        <li><a href="Cobranca/P123Gerencial/Inadimplencia.aspx">Inadimplência</a></li>
                                        <li><a href="Cobranca/P123Gerencial/Risco.aspx">Score</a></li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                        <li class="dropdown" id="RiscoCredito" runat="server" Visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Risco de Crédito <b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="RRMensalCUBO.aspx">Carteira / PDD</a></li>
                                <li><a href="PDD_GERENCIAL.aspx">Carteira PDD - Risco x Safra</a></li>
                                <li><a href="Reneg_geral.aspx">Risco Safra</a></li>
                                <li><a href="baseCred.aspx">Base de Crédito Consolidada</a></li>
                                <li><a href="SPREAD.aspx">Spread</a></li>
                                <li><a href="SPREADanalitico.aspx">Spread Analítico</a></li>

                                <li class="menu-item dropdown dropdown-submenu" id="P123" runat="server" Visible="False">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">P 1 2 3</a>
                                    <ul class="dropdown-menu">
                                        <li><a href="Graficos/P123Analise.aspx">P123 Análise</a></li>
                                        <li><a href="Graficos/P123Produto.aspx">P123 Produto</a></li>
                                        <li><a href="P123Agente.aspx">P123 Agente</a></li>
                                    </ul>
                                </li>

                            </ul>
                        </li>



                        <li class="dropdown" id="ComercialCredito" runat="server" Visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Comercial e Crédito<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="FechaComercial/FechaComCreOpRealizadas.aspx">Operações Realizadas</a></li>
                                <li><a href="FechaComercial/FechaComCreProducaoProdutos.aspx">Produção Mensal / Produto (Gráf.)</a></li>
                                <li><a href="FechaComercial/FechaComCreAnaliseProd.aspx">Análise de Produção</a></li>
                                <li><a href="FechaComercial/FechaComPropostasOp.aspx">Análise de Propostas Operador</a></li>
                                <li><a href="FechaComercial/FechaComPropostasHis.aspx">Histórico Analise de Propostas</a></li>
                                <li><a href="FechaComercial/FechaComTipoVeiculo.aspx">Financiamento por Tipo de Veiculo</a></li>
                                <li><a href="FechaComercial/FechaComAnaliseVeiculoPlano.aspx">Análise de Veículos Plano</a></li>
                                <li><a href="FechaComercial/FechaComAnaliseVeiculoAno.aspx">Análise de Veículos Ano</a></li>
                                <li><a href="FechaComercial/FechaComAnaliseVeiculoTipoAno.aspx">Análise Tipo Ano</a></li>
                                <li><a href="FechaComercial/FechaComClientes.aspx">Análise Maiores Clientes</a></li>
                                <li><a href="FechaComercial/FechaComScoreMedio.aspx">Score Médio por Agente / Mensal</a></li>
                            </ul>
                        </li>



                        <li class="dropdown" id="Comercial" runat="server" Visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Comercial<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="Comercial/MapaComercial.aspx">Mapa Comercial</a></li>
                                <li><a href="Comercial/MapaPDV.aspx">Mapa PDV - PJ</a></li>
                                <li><a href="Comercial/MapaPDVveicLOJA.aspx">Mapa PDV - Veículos</a></li>
                                <li><a href="Comercial/PontecialMktShare.aspx">Potencial MarketShare</a></li>

                                <li class="divider"></li>
                                <li><a href="../Paginas/Cetip/CETIPCruzamento.aspx">Conversão CETIP</a></li>
                                <li><a href="../Paginas/Cetip/CETIPRankingConsulta.aspx">Ranking CETIP</a></li>
                                <li><a href="../Paginas/Cetip/CETIPPotencialAnaliticoConsulta.aspx">Potencial Analitico CETIP</a></li>
                                <li><a href="../Paginas/Cetip/CETIPPotencialSinteticoConsulta.aspx">Potencial Sintetico CETIP</a></li>
                            </ul>
                        </li>


                        <li class="dropdown" id="Credito" runat="server" Visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Crédito<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="Credito/AnaliseAnalista.aspx">Análise Analista</a></li>
                                <li><a href="Credito/ProducaoAnalista.aspx">Produção Analista</a></li>
                                <li><a href="Credito/InadinplenciaPorAnalista.aspx">Inadimplencia Analista</a></li>
                                <li class="divider"></li>
                                <li><a href="Credito/SinteticoAnalise.aspx">Sintético Analista (tempos)</a></li>
                                <li><a href="Credito/SinteticoInadimpl.aspx">Sintético Inadimplencia</a></li>
                                <li class="divider"></li>
                                <li><a href="Credito/Excecao.aspx">Monitor das Aprovadas em Exceção</a></li>
                            </ul>
                        </li>

                        <li class="dropdown" id="Gerencial" runat="server" Visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Gerencial<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="../Paginas/Cadastro/Gerencial/Cubo.aspx">Cubo</a></li>
                                <li><a href="../Paginas/Cadastro/Gerencial/Balancete.aspx">Balancete</a></li>
                                <li><a href="../Paginas/Cadastro/Gerencial/PadraoTrezeMeses.aspx">Relatório Padrão 1</a></li>
                                <li><a href="../Paginas/Cadastro/Gerencial/PadraoDozeMeses.aspx">Relatório Padrão 2</a></li>
                                <li><a href="../Paginas/Cadastro/Gerencial/Balanco.aspx">Balanço</a></li>
                            </ul>
                        </li>
                        
                        
                        
                        <li class="dropdown" id="CETIP" runat="server" Visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">CETIP<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="Cetip/CETIPupload.aspx">Download Retorno</a></li>
                                <li><a href="Cetip/CETIPGerarArquivo.aspx">Gerar Remessa</a></li>
                                <li><a href="../Paginas/Cetip/CETIPRanking.aspx">Ranking CETIP - Carga</a></li>
                                <li><a href="../Paginas/Cetip/CETIPPotencialAnalitico.aspx">Potencial Analitico CETIP - Carga</a></li>
                                <li><a href="../Paginas/Cetip/CETIPPotencialSintetico.aspx">Potencial Sintetico CETIP - Carga</a></li>
                            </ul>
                        </li>


                        <li class="dropdown" id="Li3" runat="server" Visible="False">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Comissão PJ<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                        <li><a href="../Paginas/Cadastro/Comissao PJ/ComissaoPJ.aspx">Comissão PJ</a></li>
                                        <li><a href="../Paginas/Cadastro/Comissao PJ/ComissaoPJAnalitico.aspx">Comissão PJ Analítico</a></li>
                            </ul>
                        </li>


                       <li class="dropdown" id="PJ" runat="server" Visible="false">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">PJ<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                        <li><a href="../Paginas/PJ/AssinaDigital.aspx">Assinatura Digital</a></li>
                            </ul>
                        </li>


                        <%-- <li class="dropdown">
                          <a href="#" class="dropdown-toggle" data-toggle="dropdown">Relatórios <b class="caret"></b></a>
                          <ul class="dropdown-menu">
                            <li><a href="#">Relatórios 1</a></li>
                            <li><a href="#">Relatórios 2</a></li>
                            <li class="divider"></li>
                            <li><a href="#">Relatórios abc</a></li>
                            <li class="divider"></li>
                            <li><a href="#">Relatórios xyz</a></li>
                          </ul>
                        </li>--%>
                    </ul>

                    <ul class="nav navbar-nav navbar-right">
                        <li><a runat="server" href="~/Paginas/Logon.aspx">Sair</a></li>
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
</asp:Content>

