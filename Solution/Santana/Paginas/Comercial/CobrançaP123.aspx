<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="CobrançaP123.aspx.vb" Inherits="Santana.Paginas.Comercial.CobrançaP123" Title="Cobrança P123" EnableEventValidation="false"%>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Cobrança P123</title>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel" runat="server" >
        <ContentTemplate>
            <nav class="navbar navbar-default" role="navigation">
                <div class="container-fluid">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                    </div>

                    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">

                        <ul class="nav navbar-nav" >
          
                            <li> 
                                <div style="margin:5px" >
                                    <div>
                                        <p class="navbar-text" style="float:none; margin:0px" >Data de Referencia</p>
                                    </div>
                                    <div class="btn-group">                                                              
                                        <asp:Button ID="btnDataAnterior" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnterior_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px;border-width: 0px;"> 
                                            <cc1:Datax ID="txtData" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaData" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaData_Click"></asp:Button>                                   
                                    </div>
                                 <div>
                            </li>

                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Agente</p>
                                    </div>
                                    <asp:DropDownList ID="ddlAgente" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>

                            <li>
                                <div style="margin:5px; z-index: 1">
                                    <div>
                                        <p class="navbar-text" style="float:none; margin:auto" >Produto</p>
                                    </div>
                                    <asp:DropDownList ID="ddlProduto" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn" OnSelectedIndexChanged="ddlProduto_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </li>
                            <li>
                                <div style="margin:5px; z-index: 1">
                                    <div>
                                        <p class="navbar-text" style="float:none; margin:auto" >Parcela</p>
                                    </div>
                                    <asp:DropDownList ID="ddlFaixa" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn" OnSelectedIndexChanged="ddlFaixa_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </li>

                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0px">Estoque</p>
                                    </div>
                                    <div class="btn-group">
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <asp:CheckBox ID="chkEstoque" runat="server" CssClass="form-control navbar-btn" Checked="False" OnCheckedChanged="chkEstoque_OnCheckedChanged" AutoPostBack="True"></asp:CheckBox>
                                        </span>
                                    </div>
                                </div>
                            </li>

                            <li>
                                <div style="width: 15px" class="btn-group"></div>
                            </li>
                            <li>
                                <div style="margin:5px">
                                    <div style="height:20px " >
                                       
                                    </div>
                                    <asp:Button ID="btnCarregar" runat="server" Text="Carregar" class="btn btn-primary navbar-btn" OnClick="btnCarregar_Click"></asp:Button>
                                    <asp:Button ID="btnMenu" runat="server" Text="Menu Principal" class="btn btn-default navbar-btn" OnClick="btnMenu_Click"></asp:Button>
                                   
                                </div> 
                            </li>

                        </ul>

                        <ul class="nav navbar-nav navbar-right">
                            <li>
                                <div style="margin:5px">
                                    <div style="height:20px " >
                                    </div>
                                    <div class="btn-group-sm  ">
                                        <asp:ImageButton ID="btnExcel" runat="server" CssClass="btn btn-default navbar-btn" OnClick="btnExcel_Click" ImageUrl="~/imagens/excel2424.png"></asp:ImageButton>
                                        <asp:ImageButton ID="btnImpressao" runat="server" CssClass="btn btn-default navbar-btn" OnClick="btnImpressao_Click" ImageUrl="~/imagens/printer2424.png"></asp:ImageButton>
                                        <asp:ImageButton ID="btnHelp" runat="server" CssClass="btn btn-default navbar-btn" OnClick="btnHelp_Click" ImageUrl="~/imagens/help2424.png"></asp:ImageButton>
                                    </div>
                                </div> 
                            </li>

                        </ul>
                    </div>

                </div>
            </nav>
        </ContentTemplate>
        <Triggers>
                <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
        <ContentTemplate>


            <div style="width: 15px" class="btn-group"></div>
            
            <div id="dvConsultas" style="width: 100%; overflow: auto;">


                <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                    AllowPaging="True" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowCreated="GridView1_RowCreated" ShowFooter="false"
                    OnDataBound="GridView1_DataBound" Font-Size="9pt">


                    <RowStyle Height="28px" />
                    <Columns>
                        

                        <asp:TemplateField HeaderText="Data Fechamento" SortExpression="DT_FECHA">
                            <ItemStyle Width="13%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Operação" SortExpression="COD_OPERACAO">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CPF/CNPJ" SortExpression="CPF_CNPJ">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nome Cliente" SortExpression="NOME_CLIENTE">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>						
                        <asp:TemplateField HeaderText="Nome Agente" SortExpression="AGENTE">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" Código Promotora" SortExpression="COD_PROMOTORA">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nome Promotora" SortExpression="PROMOTORA">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Código Loja" SortExpression="COD_LOJA">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nome Loja" SortExpression="NOME_LOJA">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Carteira" SortExpression="CARTEIRA">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>						
                        <asp:TemplateField HeaderText="Modalidade" SortExpression="MODALIDADE">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Data Contrato" SortExpression="DATA_CONTRATO">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Produto" SortExpression="PRODUTO">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>	
                        <asp:TemplateField HeaderText="Plano" SortExpression="PLANO">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Atraso" SortExpression="ATRASO">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rating" SortExpression="RATING">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor Parcela" SortExpression="VLR_PARCELA">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="R$ Atraso" SortExpression="R$_ATRASO">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor Financiado" SortExpression="VLR_FINANCIADO">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Endereco" SortExpression="ENDERECO">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bairro" SortExpression="BAIRRO">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cidade" SortExpression="CIDADE">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CEP" SortExpression="CEP">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UF" SortExpression="UF">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Telefone" SortExpression="TELEFONE">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Celular" SortExpression="CELULAR">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fone Comercial" SortExpression="FONE_COMERCIAL">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Local Trabalho" SortExpression="LOCAL_TRABALHO">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Referência 1" SortExpression="REFERENCIA1">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Referência 2" SortExpression="REFERENCIA2">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Marca" SortExpression="MARCA">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Modelo" SortExpression="MODELO">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ano Fabricação" SortExpression="ANO_FABRIC">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ano Modelo" SortExpression="ANO_MODELO">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cor" SortExpression="COR">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Placa" SortExpression="PLACA">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor do Bem" SortExpression="VALOR_DO_BEM">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Código Cobradora" SortExpression="COD_COBRADORA">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cobradora" SortExpression="COBRADORA">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Regra" SortExpression="REGRA">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Renavam" SortExpression="RENAVAM">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Chassi" SortExpression="CHASSI">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Profissao" SortExpression="PROFISSAO">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Faixa" SortExpression="FAIXA">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vencimento P1" SortExpression="P1VENC">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pagamento P1" SortExpression="DT_P1PAGTO">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Situação P1" SortExpression="SITP1">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vencimento P2" SortExpression="P2VENC">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pagamento P2" SortExpression="DT_P2PAGTO">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Situação P2" SortExpression="SITP2">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="vencimento P3" SortExpression="P3VENC">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pagamento P3" SortExpression="DT_P3PAGTO">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Situação P3" SortExpression="SITP3">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Veículos" SortExpression="VEICULOS">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Saldo Inscrito" SortExpression="SALDO_INSCRITO">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Risco" SortExpression="RISCO">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Analista" SortExpression="ANALISTA">
                            <ItemStyle Width="6%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                    </Columns>

                    <HeaderStyle CssClass="GridviewScrollC3Header" />
                    <RowStyle CssClass="GridviewScrollC3Item" />
                    <PagerStyle CssClass="GridviewScrollC3Pager" />
                    <FooterStyle CssClass="GridviewScrollC3Footer" />
                </asp:GridView>

            </div>



        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel"  >
        <ProgressTemplate>
            <div class="overlay" />
            <div id="SpingLoad" class="overlayContent">
               <h2>Carregando</h2>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>


    <script type="text/javascript">


            function Alerta(titulo, mensagem) {
                $.growl({
                    icon: 'glyphicon glyphicon-info-sign',
                    title: titulo,
                    message: mensagem
                },
                {
                    template: { title_divider: '<hr class="separator" />' },
                    position: {
                        from: "top",
                        align: "center" //center, left, right
                    },
                    offset: 120,
                    pause_on_mouseover: true,
                    type: "info" //info, success, warning, danger

                });
            };



            var spinner;

            function StartSpin() {

                var opts = {
                    lines: 17, // The number of lines to draw
                    length: 10, // The length of each line
                    width: 7, // The line thickness
                    radius: 21, // The radius of the inner circle
                    corners: 1, // Corner roundness (0..1)
                    rotate: 0, // The rotation offset
                    direction: 1, // 1: clockwise, -1: counterclockwise
                    color: '#000', // #rgb or #rrggbb or array of colors
                    speed: 1, // Rounds per second
                    trail: 80, // Afterglow percentage
                    shadow: false, // Whether to render a shadow
                    hwaccel: false, // Whether to use hardware acceleration
                    className: 'spinner', // The CSS class to assign to the spinner
                    zIndex: 2e9, // The z-index (defaults to 2000000000)
                    top: '50%', // Top position relative to parent
                    left: '50%' // Left position relative to parent
                };

                var target = document.getElementById('SpingLoad');
                spinner = new Spinner(opts).spin(target);

            };


            function StopSpin() {
                spinner.stop();
            };


            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(StopSpin);
            Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(StartSpin);

    </script>


</asp:Content>
