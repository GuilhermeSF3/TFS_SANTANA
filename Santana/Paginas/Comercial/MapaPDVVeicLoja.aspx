<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="MapaPDVVeicLoja.aspx.vb" Inherits="Santana.MapaPDVVeicLoja" Title="Mapa PDV Veiculos" EnableEventValidation="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Mapa PDV Veiculos</title>



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
                                        <p class="navbar-text" style="float: none; margin: 0px">Data de Referencia</p>
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
                            <%--            <p class="navbar-text" style="float: none; margin: auto">Agente</p>--%>
                                    </div>
                                    <asp:DropDownList ID="ddlAgente" Width="50px" runat="server" Visible="false" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>


                            <li>
                                <div style="margin: 5px">
                                    <div>
                               <%--         <p class="navbar-text" style="float: none; margin: auto">Cobradora</p>--%>
                                    </div>
                                    <asp:DropDownList ID="ddlCobradora" Width="50px" runat="server" Visible="false" Enabled="true" AutoPostBack="false" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
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

            <div id="dvConsultas" style="height: 420px; width: 100%; overflow: auto;">

                <%--AllowSorting="TRUE"  DataKeyNames="LOJA"--%>

                <asp:GridView ID="GridView1" runat="server"  AllowSorting="TRUE"  DataKeyNames="agente"
                    AutoGenerateColumns="false" GridLines="None"                     
                    AllowPaging="True" PageSize="8" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowCreated="GridView1_RowCreated" ShowFooter="true" OnDataBound="GridView1_DataBound">


                    <RowStyle Height="31px" />
                    <Columns>


                        <asp:TemplateField HeaderText="Agente - cod" SortExpression="Agente" ItemStyle-BackColor="#EFEFEF">
                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="QTDE M3" SortExpression="QTDM3">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                            <FooterTemplate>
                                <span style="display:block; padding:0; text-align: right"><%=Quantidade3.ToString("#,##0##")%></span>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="QTDE M2" SortExpression="QTDM2" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                            <FooterTemplate>
                                <span style="display:block; padding:0; text-align: right"><%=ValorRisco3.ToString("#,##0##")%></span>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="QTDE M1" SortExpression="QTDM1" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                            <FooterTemplate>
                                <span style="display:block; padding:0; text-align: right"><%=Quantidade2.ToString("#,##0##")%></span>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="LOJAS 3M" SortExpression="LJ_3M" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                            <FooterTemplate>
                                <span style="display:block; padding:0; text-align: right"><%=ValorRisco2.ToString("#,##0##")%></span>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="LOJAS ATIVAS (mes ref)" SortExpression="LJ_ATIVA" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                            <FooterTemplate>
                                <span style="display:block; padding:0; text-align: right"><%=Quantidade1.ToString("#,##0##")%></span>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="GRAU USO %" SortExpression="GRAU">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                            <FooterTemplate>
                                <span style="display:block; padding:0; text-align: right"><%=(ValorRisco2 / Quantidade1 * 100.0).ToString("#,##0##.00")%></span>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="QTDE LJ DEIXARAM OPER." SortExpression="QTD_LJ_SAI" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                            <FooterTemplate>
                                <span style="display:block; padding:0; text-align: right"><%=QTD_LJ_SAI2.ToString("#,##0")%></span>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Perda %" SortExpression="PERDA" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                            <FooterTemplate>
                                <span style="display:block; padding:0; text-align: right"><%=(QTD_LJ_SAI2 / Quantidade1 * 100.0).ToString("#,##0")%></span>
                            </FooterTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField >
                            <ItemStyle Width="1%"  />
                        </asp:TemplateField>

                    </Columns>

                    <PagerTemplate>
                        <div class="btn-group btn-group-sm" style="margin:5px" >
                            <asp:ImageButton ID="PagerFirst" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/skip_backward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="First" UseSubmitBehavior="false"></asp:ImageButton>
                            <asp:ImageButton ID="PagerPrev" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/seek_backward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="Prev" UseSubmitBehavior="false"></asp:ImageButton>
                            <asp:ImageButton ID="PagerNext" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/seek_forward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="Next" UseSubmitBehavior="false"></asp:ImageButton>
                            <asp:ImageButton ID="PagerLast" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/skip_forward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="Last" UseSubmitBehavior="false"></asp:ImageButton>
                        </div>

                        <asp:DropDownList ID="PagerPages" runat="server" AutoPostBack="true" Width="60px" CausesValidation="false" OnSelectedIndexChanged="PagerPages_SelectedIndexChanged" CssClass="navbar-btn" style="z-index:2000" />&nbsp;
                    </PagerTemplate>

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


<%--        function pageLoad() {
            $('#<%=GridView1.ClientID%>').gridviewScroll({
                width: 100 + "%",
                height: 100 + "%",
                freezesize: 0,
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
--%>
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





