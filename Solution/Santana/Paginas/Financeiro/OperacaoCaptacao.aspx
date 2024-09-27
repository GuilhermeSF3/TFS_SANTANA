<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="OperacaoCaptacao.aspx.vb" Inherits="Santana.Paginas.Financeiro.OperacaoCaptacao" Title="Operação Captação - Financeiro" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Operação Captação - Financeiro</title>
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
                                        <p class="navbar-text" style="float: none; margin: 0px">Data Ref:</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDataAnterior2" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnteriorRef_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <cc1:Datax ID="txtDataRef" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaData2" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaDataRef_Click"></asp:Button>
                                    </div>
                                </div>
                            </li>
                            
                            
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0px">Data Aplicação:</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDataAnterior1" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnteriorDe_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <cc1:Datax ID="txtDataAplicacao" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaData1" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaDataDe_Click"></asp:Button>
                                    </div>
                                </div>
                            </li>

                            <li>
                                <div style="margin: 5px; z-index: 1">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Cliente: </p>
                                    </div>
                                    <asp:DropDownList ID="ddlCliente" Width="220px" runat="server" Visible="True" Enabled="true" AutoPostBack="true"
                                        CausesValidation="True" CssClass="selectpicker navbar-btn" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </li>

                            <li>
                                <div style="margin: 5px; z-index: 1">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Ativo Numero: </p>
                                    </div>

                                    <asp:ImageButton ID="btnEditar" runat="server" CssClass="btn btn-default navbar-btn" OnClick="btnEditar_Click" ImageUrl="~/imagens/pen.png"></asp:ImageButton>
                                    <asp:DropDownList ID="ddlAtivo" Width="220px" runat="server" Visible="True" Enabled="true" AutoPostBack="true"
                                        CausesValidation="True" CssClass="selectpicker navbar-btn">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtAtivo" runat="server" Width="220px" Visible="False" CssClass="btn btn-default allownumericwithoutdecimal" MaxLength="10" Style="text-align: left"></asp:TextBox>

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



                    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
                        <ul class="nav navbar-nav">
                            
                            
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0px">Opção CDI futuro:</p>
                                    </div>
                                    <div class="btn-group">
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <asp:CheckBox ID="chkCdi" runat="server" CssClass="form-control navbar-btn" Checked="False" OnCheckedChanged="chkCdi_OnCheckedChanged" AutoPostBack="True"></asp:CheckBox>
                                        </span>
                                    </div>
                                </div>
                            </li>

                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0px">Valor CDI % a.a.:</p>
                                    </div>
                                    <div class="btn-group">
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <asp:TextBox ID="txtValorCdi" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn allownumericwithdecimal" ReadOnly="True"></asp:TextBox>
                                        </span>
                                    </div>
                                </div>
                            </li>        
                            
                            
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0px">Data Vencto:</p>
                                    </div>
                                    <div class="btn-group">
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <asp:TextBox ID="txtDataVencimento" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn" ReadOnly="True"></asp:TextBox>
                                        </span>
                                    </div>
                                </div>
                            </li>

                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0px">Valor Captado:</p>
                                    </div>
                                    <div class="btn-group">
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <asp:TextBox ID="txtValorCaptado" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn" ReadOnly="True"></asp:TextBox>
                                        </span>
                                    </div>
                                </div>
                            </li>

                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0px">Corretor:</p>
                                    </div>
                                    <div class="btn-group">
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <asp:TextBox ID="txtCorretor" runat="server" MaxLength="10" Width="220px" CssClass="form-control navbar-btn" ReadOnly="True"></asp:TextBox>
                                        </span>
                                    </div>
                                </div>
                            </li>

                                              
                            
                            
                            

                        </ul>
                    </div>



                    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-4">
                        <ul class="nav navbar-nav">
                            
                            
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0px">Tipo Taxa:</p>
                                    </div>
                                    <div class="btn-group">
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <asp:TextBox ID="txtTipoTaxa" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn" ReadOnly="True"></asp:TextBox>
                                        </span>
                                    </div>
                                </div>
                            </li>

                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0px">Taxa Intermed %:</p>
                                    </div>
                                    <div class="btn-group">
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <asp:TextBox ID="txtTaxaIntermed" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn" ReadOnly="True"></asp:TextBox>
                                        </span>
                                    </div>
                                </div>
                            </li>                              
                            
                            

                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0px">Taxa Cliente:</p>
                                    </div>
                                    <div class="btn-group">
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <asp:TextBox ID="txtTaxaCliente" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn" ReadOnly="True"></asp:TextBox>
                                        </span>
                                    </div>
                                </div>
                            </li>


                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0px">Código do Papel:</p>
                                    </div>
                                    <div class="btn-group">
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <asp:TextBox ID="txtCodigoPapel" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn" ReadOnly="True"></asp:TextBox>
                                        </span>
                                    </div>
                                </div>
                            </li>
                            
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0px">Taxa Final (C. Agente) %:</p>
                                    </div>
                                    <div class="btn-group">
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <asp:TextBox ID="txtTaxaFinal" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn" ReadOnly="True"></asp:TextBox>
                                        </span>
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

                        <asp:TemplateField HeaderText="DATA" SortExpression="dt_proj">
                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="TAXA CDI" SortExpression="tx_cdi">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="FACTOR 1" SortExpression="factor1">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FACTOR 2" SortExpression="factor2">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Intermediate Factor" SortExpression="intermed_factor">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Result" SortExpression="resultado">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Valor R$" SortExpression="vlr_dia">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Atual. Dia" SortExpression="atualiz_dia">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
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

        $(document).ready(function () {


            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            NumericAllow();

        });


        function InitializeRequest(sender, args) {
            StartSpin();
        }

        function EndRequest(sender, args) {

            StopSpin();
            NumericAllow();
        }

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


    </script>


</asp:Content>





