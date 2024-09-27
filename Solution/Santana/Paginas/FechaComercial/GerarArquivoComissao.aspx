<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="GerarArquivoComissao.aspx.vb" Inherits="Santana.GerarArquivoComissao" Title="Comissão - Gerar Arquivo" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Comissão - Gerar Arquivo</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
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

                        <ul class="nav navbar-nav">
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0px">Data do Lançamento:</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDataAnteriorLancamento" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnterior_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <cc1:Datax ID="txtDataLancamento" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaDataLancamento" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaData_Click"></asp:Button>
                                    </div>
                                </div>
                            </li>   
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Data do Vencimento:</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDataAnteriorVencimento" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnterior_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <cc1:Datax ID="txtDataVencimento" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaDataVencimento" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaData_Click"></asp:Button>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Data da Competência:</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDataAnteriorCompetencia" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnterior_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <cc1:Datax ID="txtDataCompetencia" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaDataCompetencia" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaData_Click"></asp:Button>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Data de Emissão de NF:</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDataAnteriorEmissaoNF" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnterior_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <cc1:Datax ID="txtDataEmissaoNF" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaDataEmissaoNF" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaData_Click"></asp:Button>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Tipo de Pagamento:</p>
                                    </div>
                                    <asp:DropDownList ID="ddlPagamento" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>

                            <li>
                                <div style="width: 15px" class="btn-group"></div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div style="height: 20px">
                                    </div>
                                    
                                    <%--<asp:Button ID="btnCarregar" runat="server" Text="Carregar" class="btn btn-primary navbar-btn" OnClick="btnCarregar_Click"></asp:Button>--%>       
                                    <asp:ImageButton ID="btnExcel" runat="server" CssClass="btn btn-default navbar-btn" OnClick="btnExcel_Click" ImageUrl="~/imagens/excel2424.png"></asp:ImageButton>                             
                                    <asp:Button ID="btnMenu" runat="server" Text="Menu Principal" class="btn btn-default navbar-btn" OnClick="btnMenu_Click"></asp:Button>

                                </div>
                            </li>

                        </ul>

                        <ul class="nav navbar-nav navbar-right">
                            <li>
                                <div style="margin: 5px">
                                    <div style="height: 20px">
                                    </div>
                                    <div class="btn-group-sm">
                                        <%--<asp:ImageButton ID="btnExcel" runat="server" CssClass="btn btn-default navbar-btn" OnClick="btnExcel_Click" ImageUrl="~/imagens/excel2424.png"></asp:ImageButton>--%>
                                        <%--<asp:ImageButton ID="btnImpressao" runat="server" CssClass="btn btn-default navbar-btn" OnClick="btnImpressao_Click" ImageUrl="~/imagens/printer2424.png"></asp:ImageButton>--%>
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



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="dvConsultas" runat="server" style="height: 590px; width: 100%; overflow: auto; visibility: visible">

                <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                    AllowPaging="True" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowCreated="GridView1_RowCreated" ShowFooter="false"
                    OnDataBound="GridView1_DataBound" Font-Size="9pt">
                    
                    <RowStyle Height="28px" />
                    <Columns>

                        <asp:TemplateField HeaderText="Código da Empresa" SortExpression="COD_EMPRESA">
                            <ItemStyle Width="7%" HorizontalAlign="Right"/>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Descrição Rapida" SortExpression="DESCR_RAPIDA">
                            <ItemStyle Width="15%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Data do Lançamento" SortExpression="DATA_LANCAMENTO">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Data do Vencimento" SortExpression="DATA_VENCIMENTO">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Competência" SortExpression="COMPETENCIA">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Conta Bancária" SortExpression="CONTA_BANCARIA">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Forma de Pagamento " SortExpression="FORMA_PAGAMENTO">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Código do Histórico" SortExpression="COD_HISTORICO">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Complemento do Histórico" SortExpression="COMPL_HISTÓRICO">
                            <ItemStyle Width="12%" HorizontalAlign="Left" />
                        </asp:TemplateField>                       

                        <asp:TemplateField HeaderText="Evento Contabil" SortExpression="CONTABIL">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Centro de Custo" SortExpression="CENTRO_CUSTO">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Código do Cliente" SortExpression="COD_CLIENTE">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Valor" SortExpression="VALOR">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Desconto" SortExpression="DESCONTO">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Debito/Credito" SortExpression="DEBITO_CREDITO">
                            <ItemStyle Width="12%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nota Fiscal" SortExpression="NOTA_FISCAL">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Data Emissão NF" SortExpression="DATA_EMISSÃO_NF">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Código do Banco" SortExpression="COD_BANCO">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Agência Destino" SortExpression="AGENCIA_DESTINO">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dígito Conta Destino" SortExpression="DIGITO_CONTA_DESTINO">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Conta Destino" SortExpression="CONTA_DESTINO">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                                
                        <asp:TemplateField HeaderText="Nome do Favorecido" SortExpression="NOME_FAVORECIDO">
                            <ItemStyle Width="12%" HorizontalAlign="Left" />
                        </asp:TemplateField>    
                        
                        <asp:TemplateField HeaderText="Modalidade" SortExpression="MODALIDADE">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>    

                        <asp:TemplateField HeaderText="Digito da Agencia" SortExpression="DIGITO_AGENCIA">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>    
                        
                        <asp:TemplateField HeaderText="CPF/CNPJ Favorecido" SortExpression="CPF_CNPJ_FAVORECIDO">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>   
                        
                        <asp:TemplateField HeaderText="Nome do Banco" SortExpression="NOME_BANCO">
                            <ItemStyle Width="12%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tipo da Conta" SortExpression="TIPO_CONTA">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Finalidade" SortExpression="FINALIDADE">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>    
                        
                        <asp:TemplateField HeaderText="Finalidade DOC" SortExpression="FINALIDADE_DOC">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Finalidade TED" SortExpression="FINALIDADE_TED">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
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


    <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel">
        <ProgressTemplate>
            <div class="overlay" />
            <div id="SpingLoad" class="overlayContent">
                <h2>Carregando</h2>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>



    <asp:HiddenField ID="hfGridView1SV" runat="server" OnValueChanged="hfGridView1SV_ValueChanged" />
    <asp:HiddenField ID="hfGridView1SH" runat="server" OnValueChanged="hfGridView1SH_ValueChanged" />

    <script type="text/javascript">


        function pageLoad() {
           
            $('#<%=GridView1.ClientID%>').gridviewScroll({
                width: 100 + "%",
                height: 100 + "%",
                freezesize: 1,
                headerrowcount: 1,
                startVertical: $("#<%=hfGridView1SV.ClientID%>").val(),
                startHorizontal: $("#<%=hfGridView1SH.ClientID%>").val(),

                onScrollVertical: function (delta) {
                    $("#<%=hfGridView1SV.ClientID%>").val(delta);
                },
                onScrollHorizontal: function (delta) {
                    $("#<%=hfGridView1SH.ClientID%>").val(delta);
                },
                arrowsize: 30,
                varrowtopimg: "/imagens/arrowvt.png",
                varrowbottomimg: "/imagens/arrowvb.png",
                harrowleftimg: "/imagens/arrowhl.png",
                harrowrightimg: "/imagens/arrowhr.png"
            });

            }

        $(document).ready(function () {

            NumericAllow();

        });


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





