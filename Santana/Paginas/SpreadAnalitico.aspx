<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="SpreadAnalitico.aspx.vb" Inherits="Santana.SpreadAnalitico" Title="Spread - Risco Analítico" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Spread - Risco Analítico</title>



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
                                        <p class="navbar-text" style="float: none; margin: auto">Data Produção DE:</p>
                                    </div>
                                    <cc1:Datax ID="txtDataDE" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0px">Data Produção ATÉ:</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDataAnterior" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnterior_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <cc1:Datax ID="txtData" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaData" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaData_Click"></asp:Button>
                                    </div>
                                </div>
                            </li>



                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Analítico</p>
                                    </div>
                                    <asp:DropDownList ID="ddlAgente" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>

                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Veículos</p>
                                    </div>
                                    <asp:DropDownList ID="ddlCobradora" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
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
                                   <%-- <asp:Button ID="btnProd" runat="server" Text="Carregar Produção" class="btn btn-primary navbar-btn" OnClick="btnCarregarProd_Click"></asp:Button>--%>
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
                                        <p class="navbar-text" style="float: none; margin: auto">Data Safra Risco DE:</p>
                                    </div>
                                    <cc1:Datax ID="txtDataINI" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Data Safra Risco ATÉ:</p>
                                    </div>
                                    <cc1:Datax ID="txtDataFIM" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Dias Atraso:</p>
                                    </div>
                                    <asp:TextBox ID="txtDiasAtraso" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></asp:TextBox>
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

            <div id="dvRiscoAnalitico" runat="server" style="height: 590px; width: 100%; overflow: auto; visibility: visible ">

                <asp:GridView ID="GridViewRiscoAnalitico" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                    AllowPaging="True" PageSize="30" OnPageIndexChanging="GridViewRiscoAnalitico_PageIndexChanging"
                    OnRowCreated="GridViewRiscoAnalitico_RowCreated" DataKeyNames="NUM_OPE" ShowFooter="false" 
                    OnDataBound="GridViewRiscoAnalitico_DataBound" Font-Size="9pt">
                    
                    
                    <RowStyle Height="28px" />
                    <Columns>
                        
                        <asp:TemplateField HeaderText="DATA REF" SortExpression="DT_FECHA">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="NUM_OPE" SortExpression="NUM_OPE" >
                            <ItemStyle Width="3%" HorizontalAlign="Right"/>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="DT_BASE" SortExpression="DT_BASE">
                            <ItemStyle Width="3%" HorizontalAlign="Right"/>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="VLR_OP" SortExpression="VLR_OP">
                            <ItemStyle Width="3%" HorizontalAlign="Right"  />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="VLR_PARC" SortExpression="VLR_PARC">
                            <ItemStyle Width="3%" HorizontalAlign="Right"  />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PRAZO" SortExpression="PRAZO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PLANO" SortExpression="PLANO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="VLR_ATRASO" SortExpression="VLR_ATRASO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="VLR_VINCENDO" SortExpression="VLR_VINCENDO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="TAXA" SortExpression="TAXA">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="RETORNO" SortExpression="RETORNO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="TAXA_SEM_RET" SortExpression="TAXA_SEM_RET">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ATRASO" SortExpression="ATRASO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="RENEGOCIADO" SortExpression="RENEGOCIADO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="RATING" SortExpression="RATING">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CODCLI" SortExpression="CODCLI">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CODPROD" SortExpression="CODPROD">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CODMODA" SortExpression="CODMODA">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="AGENTE" SortExpression="AGENTE">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="VEICULO" SortExpression="VEICULO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="ANO_MODELO" SortExpression="ANO_MODELO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="RENDA" SortExpression="RENDA">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="FX_RENDA" SortExpression="FX_RENDA">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="FX_PRAZO" SortExpression="FX_PRAZO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="CODOPER" SortExpression="CODOPER">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="CODLOJA" SortExpression="CODLOJA">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="TX_X_VLR" SortExpression="TX_X_VLR">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="PRZ_X_VLR" SortExpression="PRZ_X_VLR">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="idade" SortExpression="idade">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="fx_idade" SortExpression="fx_idade">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="fx_ANO" SortExpression="fx_ANO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="OCUPACAO" SortExpression="OCUPACAO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="CONTRATO_ORIGINAL" SortExpression="CONTRATO_ORIGINAL">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="VEICULO_ORIGINAL" SortExpression="VEICULO_ORIGINAL">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Vlr Futuro" SortExpression="vf">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Vlr Risco" SortExpression="vlr_risco">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Vlr Futuro Original" SortExpression="vf_ORIGINAL">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="SCORE" SortExpression="SCORE">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Vlr Veículo" SortExpression="VLR_VEICULO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemStyle Width="1%" />
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



    <%--    <asp:HiddenField ID="hfGridViewCarteirasSV" runat="server" OnValueChanged="hfGridViewCarteirasSV_ValueChanged" />
    <asp:HiddenField ID="hfGridViewCarteirasSH" runat="server" OnValueChanged="hfGridViewCarteirasSH_ValueChanged" />--%>

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





