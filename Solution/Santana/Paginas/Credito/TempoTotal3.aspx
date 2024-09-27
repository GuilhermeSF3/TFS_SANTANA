<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="TempoTotal3.aspx.vb" Inherits="Santana.Paginas.Credito.TempoTotal3" Title="Tempo Total de Propostas Tela 3" EnableEventValidation="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Tempo Total de Propostas Tela 3</title>
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
                                        <p class="navbar-text" style="float: none; margin: 0">Data Inicial</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDataAnteriorDe" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnteriorDe_Click"></asp:Button>
                                        <span class="btn" style="padding: 0; border-width: 0;">
                                            <cc1:Datax ID="txtDataDe" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaDataDe" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaDataDe_Click"></asp:Button>
                                    </div>
                                    <div>
                            </li>

                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0">Data Final</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDataAnteriorAte" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnteriorAte_Click"></asp:Button>
                                        <span class="btn" style="padding: 0; border-width: 0;">
                                            <cc1:Datax ID="txtDataAte" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaDataAte" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaDataAte_Click"></asp:Button>
                                    </div>
                                    <div>
                            </li>

                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Produto</p>
                                    </div>
                                    <asp:DropDownList ID="ddlProduto" Width="50px" runat="server" Visible="True" Enabled="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>

                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Analista</p>
                                    </div>
                                    <asp:DropDownList ID="ddlAnalista" Width="50px" runat="server" Visible="True" Enabled="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>
                            
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Faixa de Tempo</p>
                                    </div>
                                    <asp:TextBox ID="txtFaixaTempo" runat="server" Visible="True" Enabled="false" Width="130px" CssClass="navbar-btn" MaxLength="15"></asp:TextBox>
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
                                    <asp:Button ID="btnMenu" runat="server" Text="Voltar Tempo Total 2" class="btn btn-default navbar-btn" OnClick="btnMenu_Click"></asp:Button>

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
                                        <p class="navbar-text" style="float: none; margin: auto">Agente</p>
                                    </div>
                                    <asp:DropDownList ID="ddlAgente" Width="50px" runat="server" Visible="True" Enabled="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>

                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Operador</p>
                                    </div>
                                    <asp:DropDownList ID="ddlOperador" Width="50px" runat="server" Visible="True" Enabled="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>

                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Loja</p>
                                    </div>
                                    <asp:DropDownList ID="ddlLoja" Width="50px" runat="server" Visible="True" Enabled="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>

                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Proposta</p>
                                    </div>
                                    <asp:TextBox ID="txtProposta" runat="server" Visible="True" Enabled="true" Width="130px" CssClass="allownumericwithoutdecimal navbar-btn" MaxLength="15"></asp:TextBox>
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



            <div id="dvConsultas" style="width: 100%; overflow: auto;">


                <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                    AllowPaging="True" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowCreated="GridView1_RowCreated" ShowFooter="false"  DataKeyNames="epnrprop">


                    <RowStyle Height="31px" />
                    <Columns>
                        
                    
                        <asp:TemplateField HeaderText="Proposta" SortExpression="epnrprop">
                            <ItemStyle Width="5%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Início Geral" SortExpression="Ini">
                            <ItemStyle Width="8%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Fim Geral" SortExpression="Fim">
                            <ItemStyle Width="8%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tempo Total" SortExpression="tempo">
                            <ItemStyle Width="5%" HorizontalAlign="right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Modalidade" SortExpression="Moda">
                            <ItemStyle Width="5%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Produto" SortExpression="produto">
                            <ItemStyle Width="5%" HorizontalAlign="left" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Situação" SortExpression="situacao">
                            <ItemStyle Width="5%" HorizontalAlign="left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Fase" SortExpression="EPFASE">
                            <ItemStyle Width="5%" HorizontalAlign="right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Alçada" SortExpression="EPALCADA">
                            <ItemStyle Width="5%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Sequencia" SortExpression="seq">
                            <ItemStyle Width="5%" HorizontalAlign="right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Inicio da Fase" SortExpression="entra">
                            <ItemStyle Width="8%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Fim da Fase" SortExpression="fim_fase">
                            <ItemStyle Width="8%" HorizontalAlign="right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Fase" SortExpression="Fase">
                            <ItemStyle Width="5%" HorizontalAlign="left" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Tempo (minutos)" SortExpression="tempo_fase">
                            <ItemStyle Width="8%" HorizontalAlign="right" />
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
