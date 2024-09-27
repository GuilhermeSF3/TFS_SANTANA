<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="Prestamista.aspx.vb" Inherits="Santana.Paginas.RiscoCredito.Prestamista" Title="Prestamista " EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Prestamista</title>



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
                                        <p class="navbar-text" style="float: none; margin: auto">Agente:</p>
                                    </div>
                                    <asp:DropDownList ID="ddlAgente" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>


                            <li>
                                <div style="width: 15px" class="btn-group"></div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div style="height: 20px">
                                    </div>
                                    <asp:Button ID="btnCarregar" runat="server" Text="Carregar" class="btn btn-primary navbar-btn" OnClick="btnCarregar_Click"></asp:Button>
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
        </Triggers>
    </asp:UpdatePanel>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div id="msgHeader" runat="server" visible="False">

                    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">

                        <ul class="nav navbar-nav">

                        </ul>
                    </div>
            </div>


            <div id="dvGrid" runat="server" style="height: 590px; width: 100%; overflow: auto; visibility: visible">

                <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                    AllowPaging="True" PageSize="20" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowCreated="GridView1_RowCreated" ShowFooter="false"
                    OnDataBound="GridView1_DataBound" Font-Size="9pt">


                    <RowStyle Height="28px" />
                    <Columns>

                        <asp:TemplateField HeaderText="Data Referência" SortExpression="DATA_REFERENCIA">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Agente" SortExpression="AGENTE">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CPF/CNPJ" SortExpression="CPF_CNPJ">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nome Cliente" SortExpression="NOME_CLIENTE">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Ocupação" SortExpression="OCUPACAO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Fone Residencial" SortExpression="FONE_RESIDENCIAL">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Fone Celular" SortExpression="FONE_CELULAR">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Operação" SortExpression="OPERACAO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cobradora" SortExpression="COBRADORA">
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Valor Prestamista" SortExpression="VALOR_PRESTAMISTA">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Qtd. Parcelas Operação" SortExpression="QTDE_PARCELAS_OPERACAO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Qtd. Parcelas em Aberto" SortExpression="QTDE_PARCELAS_ABERTO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Data Base Operação" SortExpression="DATA_BASE_OPERACAO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Último Vencimento Operação" SortExpression="ULTIMO_VCTO_OPERACAO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dias de Atraso" SortExpression="DIAS_ATRASO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Saldo Devedor na Referência" SortExpression="SALDO_DEVEDOR_NA_REF">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Data Último Pgto." SortExpression="DATA_ULTIMO_PGTO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dias Último Pgto." SortExpression="DIAS_ULTIMO_PGTO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
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



    <asp:HiddenField ID="hfGridView1SV" runat="server" OnValueChanged="hfGridView1SV_ValueChanged" />
    <asp:HiddenField ID="hfGridView1SH" runat="server" OnValueChanged="hfGridView1SH_ValueChanged" />

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





