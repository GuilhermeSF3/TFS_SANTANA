<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="ConferePagto.aspx.vb" Inherits="Santana.Paginas.Comercial.ConferePagto" Title="Confere Pagamento" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Confere Pagamento</title>



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
                                        <p class="navbar-text" style="float: none; margin: 0px">Data Inicial:</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDataAnteriorDe" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnteriorDe_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <cc1:Datax ID="txtDataDe" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaDataDe" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaDataDe_Click"></asp:Button>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Data Até:</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDataAnteriorAte" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnteriorAte_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <cc1:Datax ID="txtDataAte" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaDataAte" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaDataAte_Click"></asp:Button>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Agentes</p>
                                    </div>
                                    <asp:DropDownList ID="ddlAgente" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn" OnSelectedIndexChanged="ddlAgente_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Pagamentos</p>
                                    </div>
                                    <asp:DropDownList ID="ddlPagto" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
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
                                        <p class="navbar-text" style="float: none; margin: auto">Favorecido</p>
                                    </div>
                                    <asp:DropDownList ID="ddlFavorecido" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Qte. De Contratos:</p>
                                    </div>
                                    <asp:TextBox ID="txtQteContratoRealizado" runat="server" MaxLength="10" Width="100px" enabled="false" CssClass="form-control navbar-btn" ></asp:TextBox>
                                </div>
                            </li>

                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Valor Produção:</p>
                                    </div>
                                    <asp:TextBox ID="TxtVlr" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn" enabled="false" ></asp:TextBox>
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

            <div id="dvGrid" runat="server" style="height: 590px; width: 100%; overflow: auto; visibility: visible ">

                <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                    AllowPaging="True" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowCreated="GridView1_RowCreated" DataKeyNames="OPNROPER" ShowFooter="false" 
                    OnDataBound="GridView1_DataBound" Font-Size="9pt">
                    
                    
                    <RowStyle Height="28px" />
                    <Columns>

                        <asp:TemplateField HeaderText="COD.AGENTE" SortExpression="A13CODORG">
                            <ItemStyle Width="3%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="AGENTE" SortExpression="A13DESCR" >
                            <ItemStyle Width="8%" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                                                                      
                        <asp:TemplateField HeaderText="CONTRATO" SortExpression="OPNROPER">
                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="DATA BASE" SortExpression="OPDTBASE" >
                            <ItemStyle Width="5%" HorizontalAlign="Center"/>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CPF/CNPJ" SortExpression="OPCODCLI">
                            <ItemStyle Width="5%" HorizontalAlign="Right"/>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="NOME DO CLIENTE" SortExpression="CLNOMECLI">
                            <ItemStyle Width="15%" HorizontalAlign="Left"/>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PLACA" SortExpression="ABRENAVAM">
                            <ItemStyle Width="5%" HorizontalAlign="Center"  />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="RENAVAM" SortExpression="NRRENAVAM">
                            <ItemStyle Width="5%" HorizontalAlign="Center"  />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="NOME DO FAVORECIDO" SortExpression="DCFAVNOME">
                            <ItemStyle Width="15%" HorizontalAlign="Left"/>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="VALOR" SortExpression="DCVALOR">
                            <ItemStyle Width="5%" HorizontalAlign="RIGHT"  />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PAGFROTA" SortExpression="DCVALOR_PAGFROTA">
                            <ItemStyle Width="5%" HorizontalAlign="RIGHT"  />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="BOLETO" SortExpression="DCVALOR_BOLETO">
                            <ItemStyle Width="5%" HorizontalAlign="RIGHT"  />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ACESSÓRIOS" SortExpression="DCVALOR_ACESSORIOS">
                            <ItemStyle Width="5%" HorizontalAlign="RIGHT"  />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CHASSI" SortExpression="ABCHASSI">
                            <ItemStyle Width="7%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="UF LICENCIAMENTO" SortExpression="ABUFLCPL">
                            <ItemStyle Width="5%" HorizontalAlign="RIGHT" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="DATA" SortExpression="EPDTLNC">
                            <ItemStyle Width="6%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="HORA INCLUSÃO" SortExpression="EPHORALNC">
                            <ItemStyle Width="6%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="COD OPERADOR" SortExpression="OPCODORG3">
                            <ItemStyle Width="6%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="OPERADOR" SortExpression="OPERADOR">
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="COD LOJA" SortExpression="OPCODORG4">
                            <ItemStyle Width="6%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="LOJA" SortExpression="LOJA">
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PRODUTO" SortExpression="OPCODPROD">
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
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


        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(StopSpin);
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(StartSpin);


    </script>


</asp:Content>





