<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="RiscoCreditoSafra.aspx.vb" Inherits="Santana.RiscoCreditoSafra" Title="Risco Crédito - Safra" EnableEventValidation="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Risco Crédito - Safra</title>
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
                                        <p class="navbar-text" style="float: none; margin: 0px">Safra De</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDataAnterior" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnterior_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <cc1:Datax ID="txtData" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaData" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaData_Click"></asp:Button>
                                    </div>
                                    <div>
                            </li>

                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0px">Data Ref.</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDtAteAnterior" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDtAteAnterior_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <cc1:Datax ID="txtDataAte" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnDtAteProxima" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDtAteProxima_Click"></asp:Button>
                                    </div>
                                    <div>
                            </li>



                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Produtos</p>
                                    </div>
                                    <asp:DropDownList ID="ddlProduto1" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>


                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Produto 2</p>
                                    </div>
                                    <asp:DropDownList ID="ddlProduto2" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
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
            </nav>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="dvConsultas" style="height: 590px; width: 100%; overflow: auto;">


                <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="FALSE"
                    AllowPaging="True" PageSize="16" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowCreated="GridView1_RowCreated" OnRowCommand="GridView1_RowCommand"
                    font-size="9pt" >


                    <RowStyle Height="28px" />
                    <Columns>

                        <asp:TemplateField HeaderText="PRODUTO" SortExpression="PRODUTO" ItemStyle-BackColor="#EFEFEF">
                            <ItemStyle Width="20%" HorizontalAlign="LEFT" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="SAFRA" SortExpression="DT_FECHA" ItemStyle-BackColor="#EFEFEF">
                            <ItemStyle Width="7%" HorizontalAlign="Center" BackColor="#EFEFEF" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="DESCRIÇÃO" SortExpression="DESCRICAO" ItemStyle-BackColor="#EFEFEF">
                            <ItemStyle Width="17%" HorizontalAlign="Left" BackColor="#EFEFEF" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="P1 M1" SortExpression="P1_M1">
                            <ItemStyle Width="5%" HorizontalAlign="RIGHT" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="P1 M2" SortExpression="P1_M2">
                            <ItemStyle Width="5%" HorizontalAlign="RIGHT" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="P1 M3" SortExpression="P1_M3">
                            <ItemStyle Width="5%" HorizontalAlign="RIGHT"/>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="P2 M1" SortExpression="P2_M1">
                            <ItemStyle Width="5%" HorizontalAlign="RIGHT" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="P2 M2" SortExpression="P2_M2">
                            <ItemStyle Width="5%" HorizontalAlign="RIGHT" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="P2 M3" SortExpression="P2_M3">
                            <ItemStyle Width="5%" HorizontalAlign="RIGHT"/>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="P3 M1" SortExpression="P3_M1">
                            <ItemStyle Width="5%" HorizontalAlign="RIGHT" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="P3 M2" SortExpression="P3_M2">
                            <ItemStyle Width="5%" HorizontalAlign="RIGHT" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="P3 M3" SortExpression="P3_M3">
                            <ItemStyle Width="5%" HorizontalAlign="RIGHT" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="P123 M3" SortExpression="P123_M3">
                            <ItemStyle Width="5%" HorizontalAlign="RIGHT" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="P123 M4" SortExpression="P123_M4">
                            <ItemStyle Width="5%" HorizontalAlign="RIGHT" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Perda" SortExpression="PERDA">
                            <ItemStyle Width="5%" HorizontalAlign="RIGHT" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="" SortExpression="ORDEM" Visible="false">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemStyle Width="1%" />
                        </asp:TemplateField>

                    </Columns>

                    <HeaderStyle CssClass="GridviewScrollC3Header" />
                    <RowStyle CssClass="GridviewScrollC3Item" />
                    <PagerStyle CssClass="GridviewScrollC3Pager" />

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
                headerrowcount: 2,
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


            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(StopSpin);
            Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(StartSpin);


    </script>


</asp:Content>





