<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="CETIPUpload.aspx.vb" Inherits="Santana.Paginas.Credito.CETIPUpload" Title="CETIP Upload" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>CETIP Upload</title>
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
                                        <p class="navbar-text" style="float: none; margin: 0px" class="btn btn-primary navbar-btn">Selecionar Arquivo:</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:FileUpload ID="FileUpload1" runat="server" /><asp:Label ID="Label1" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </li>
             
     
                            <li>
                                <div style="width: 15px" class="btn-group"></div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div style="height: 20px">
                                    </div>
                                    <asp:Button ID="Button1" class="btn btn-primary navbar-btn" runat="server" Text="Upload"  OnClick="Button1_Click"/>
                                    <asp:Button ID="btnMenu" runat="server" Text="Menu Principal" class="btn btn-default navbar-btn" OnClick="btnMenu_Click"></asp:Button>

                                </div>
                            </li>

                        </ul>

                        <ul class="nav navbar-nav navbar-right">
                            <li>
                                <div style="margin: 5px">
                                    <div style="height: 20px">
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

                </div>
            </nav>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
            <asp:PostBackTrigger ControlID="Button1" />
        </Triggers>
    </asp:UpdatePanel>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="dvRiscoAnalitico" runat="server" style="height: 590px; width: 100%; overflow: auto; visibility: visible">

                <asp:GridView ID="GridViewRiscoAnalitico" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                    AllowPaging="True" PageSize="12" OnPageIndexChanging="GridViewRiscoAnalitico_PageIndexChanging"
                    OnRowCreated="GridViewRiscoAnalitico_RowCreated"  ShowFooter="false"
                    OnDataBound="GridViewRiscoAnalitico_DataBound" Font-Size="9pt">


                    <RowStyle Height="28px" />
                    <Columns>

                        <asp:TemplateField HeaderText="Cód. loja" SortExpression="COD_LOJA">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CGC loja" SortExpression="CGC_LOJA">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nome da loja" SortExpression="NOME_LOJA">
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cód. operador" SortExpression="COD_OPERADOR">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nome do operador" SortExpression="NOME_OPERADOR">
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cód. agente" SortExpression="COD_AGENTE">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nome do agente" SortExpression="NOME_AGENTE">
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Núm. proposta" SortExpression="NUMERO_PROPOSTA">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Núm. operaçao" SortExpression="NUMERO_OPERACAO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Plano" SortExpression="PLANO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>           

                        <asp:TemplateField HeaderText="Nome do cliente" SortExpression="NOME_CLIENTE">
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="CPF cliente" SortExpression="CPF_CLIENTE">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="Produto" SortExpression="PRODUTO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="Cód. tabela" SortExpression="COD_TABELA">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="Data cadasto" SortExpression="DT_CADASTRO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="Data base" SortExpression="DT_BASE">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="Valor financiado" SortExpression="VL_FINANCIADO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="Valor liberado" SortExpression="VL_LIBERADO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="Finalizado" SortExpression="FINALIZADO">
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="Situação" SortExpression="SITUACAO">
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="Modelo proposta" SortExpression="MODELO_PROPOSTA">
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="Ano fab. proposta" SortExpression="ANO_FAB_POPOSTA">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="Ano mod. proposta" SortExpression="ANO_MOD_PROPOSTA">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="CNPJ loja paga" SortExpression="CNPJ_LOJA_PAGA">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="Valor aprov. pago" SortExpression="VLR_APROV_PAGO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="Marca paga" SortExpression="MARCA_PAGO">
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="Ano mod. pago" SortExpression="ANO_MODELO_PAGO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="Ano fab. pago" SortExpression="ANO_FABRICACAO_PAGO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="Prazo pago" SortExpression="PRAZO_PAGO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="Cód. Fipe" SortExpression="COD_FIPE">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="Valor Fipe pago" SortExpression="VALOR_FIPE_PAGO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="CNPJ ag. financeiro" SortExpression="CNPJ_AGENTE_FINANCEIRO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>           


                        <asp:TemplateField HeaderText="Nome ag. financeiro" SortExpression="NOME_AGENTE_FINANCEIRO">
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                        </asp:TemplateField>           

                        <asp:TemplateField HeaderText="Pago loja proposta" SortExpression="PAGO_LOJA_PROPOSTA">
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                        </asp:TemplateField>           




                        <asp:TemplateField>
                            <ItemStyle Width="1%" />
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

    <script type="text/javascript">
        

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





