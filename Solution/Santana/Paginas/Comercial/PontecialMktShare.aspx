<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="PontecialMktShare.aspx.vb" Inherits="Santana.Paginas.Comercial.PontecialMktShare" Title="Pontecial x Market Share" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Potencial x Market Share</title>
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
                                <div style="margin: 1px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0px">Data Referencia DE:</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDataAnterior1" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnterior1_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                                <asp:TextBox ID="txtDataDe" Width="80px"  runat="server"  Enabled="true"  CssClass="form-control navbar-btn"></asp:TextBox>
                                        </span>
                                        <asp:Button ID="btnProximaData1" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaData1_Click"></asp:Button>
                                    </div>
                                </div>
                            </li>

                            <li>
                                <div style="margin: 1px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0px">ATÉ:</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDataAnterior2" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnterior2_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                        <asp:TextBox ID="txtDataATE" Width="80px"   runat="server"  Enabled="true"  CssClass="form-control navbar-btn"></asp:TextBox>
                                        </span>
                                        <asp:Button ID="btnProximaData2" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaData2_Click"></asp:Button>
                                    </div>
                                </div>
                            </li>
                            
                            <li>
                                <div style="margin: 1px">
                                    <div>
                                        <p  class="navbar-text" style="float: none; margin: auto; visibility: hidden" >Pesquisar:         </p>
                                        <asp:TextBox ID="txtLocalizar" Width="180px" runat="server" Visible="false" Enabled="true"  CssClass="form-control navbar-btn"></asp:TextBox>
                                </div>
                            </li>

                            <li>
                                <div style="margin: 1px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto; visibility: hidden">CEP DE:</p>
                                        <asp:TextBox ID="txtCepDe" Width="100px" runat="server" Visible="false" Enabled="true"  CssClass="form-control navbar-btn"></asp:TextBox>
                                    </div>
                                </div>
                            </li>

                            <li>
                                <div style="margin: 1px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto; visibility: hidden">CEP ATÉ:</p>
                                        <asp:TextBox ID="TxtCepAte" Width="100px" runat="server" Visible="false" Enabled="true"  CssClass="form-control navbar-btn"></asp:TextBox>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 1px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">REGIÃO: </p>
                                    </div>
                                    <asp:listbox ID="lbREGIAO" runat="server" height="100px" ></asp:listbox>
                                </div>
                            </li>

                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Faixa de Ano: </p>
                                    </div>
                                    <asp:DropDownList ID="ddlFxAno" Width="30px" runat="server"  visible="True" Enabled="true" CausesValidation="True" CssClass="selectpicker navbar-btn">
                                         <asp:ListItem Value="99" Text="(Todos)" />
                                         <asp:ListItem Value="1992" Text="1992 a 1997" />
                                         <asp:ListItem Value="1998" Text="1998 a 2001" />
                                         <asp:ListItem Value="2002" Text="2002 a 2005" />
                                         <asp:ListItem Value="2006" Text="2006 a 2011" />
                                         <asp:ListItem Value="2012" Text="2012 a 0 Km" />
                                         <asp:ListItem Value="2004" Text="até  2004" />
                                         <asp:ListItem Value="2007" Text="após 2004" />
                                        
                                    </asp:DropDownList>
                                </div>
                            </li>
     
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Cidade: </p>
                                    </div>
                                    <asp:DropDownList ID="ddlCidade" Width="30px" runat="server"  visible="True" Enabled="true" CausesValidation="True" CssClass="selectpicker navbar-btn">
                                    </asp:DropDownList>


                                    <asp:Button ID="btnCarregar" runat="server" Text="Carregar" class="btn btn-primary navbar-btn" OnClick="btnCarregar_Click"></asp:Button>
                                    <asp:Button ID="btnMenu"     runat="server" Text="Menu"     class="btn btn-default navbar-btn" OnClick="btnMenu_Click"></asp:Button>

                                </div>


                        <ul class="nav navbar-nav"> 
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">UF: </p>
                                    </div>
                                    <asp:DropDownList ID="ddlUF" Width="30px" runat="server" Visible="True" Enabled="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                        </ul>
                        <ul class="nav navbar-nav">
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">AGENTE: </p>
                                    </div>
                                    <asp:DropDownList ID="ddlAGENTE" Width="30px" runat="server" Visible="True" Enabled="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>
                        </ul>



                                <div style="margin: 5px">
                                    <div style="height: 20px">
                                    </div>

                                        <asp:ImageButton ID="btnExcel" runat="server" CssClass="btn btn-default navbar-btn" OnClick="btnExcel_Click" ImageUrl="~/imagens/excel2424.png"></asp:ImageButton>
                                        <asp:ImageButton ID="btnImpressao" runat="server" CssClass="btn btn-default navbar-btn" OnClick="btnImpressao_Click" ImageUrl="~/imagens/printer2424.png"></asp:ImageButton>
                                        <asp:ImageButton ID="btnHelp" runat="server" CssClass="btn btn-default navbar-btn" OnClick="btnHelp_Click" ImageUrl="~/imagens/help2424.png"></asp:ImageButton>
                                    </div>
                        
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

            <div id="dvRiscoAnalitico" runat="server" style="height: 590px; width: 100%; overflow: auto; visibility: visible">

                <asp:GridView ID="GridViewRiscoAnalitico" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                    AllowPaging="True" PageSize="12" OnPageIndexChanging="GridViewRiscoAnalitico_PageIndexChanging"
                    OnRowCreated="GridViewRiscoAnalitico_RowCreated"  ShowFooter="false"
                    OnDataBound="GridViewRiscoAnalitico_DataBound" Font-Size="9pt">


                    <RowStyle Height="28px" />
                    <Columns>
                        
                        <asp:TemplateField HeaderText="UF" SortExpression="UF">
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="REGIAO" SortExpression="REGIAO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="GRUPO" SortExpression="GRUPO">
                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CIDADE" SortExpression="CIDADE">
                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="AGENTE" SortExpression="AGENTE">
                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CEP DE" SortExpression="CEP_DE">
                            <ItemStyle Width="7%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CEP ATE" SortExpression="CEP_ATE">
                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="FX ANO" SortExpression="FX_ANO">
                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="QTDE Produção" SortExpression="FX1_QTDE">    
                            <ItemStyle Width="3%" HorizontalAlign="RIGHT" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="VALOR PRODUÇÃO" SortExpression="FX1_VLR">
                            <ItemStyle Width="5%" HorizontalAlign="RIGHT" />
                        </asp:TemplateField>
                        
                        
                        <asp:TemplateField HeaderText="%  (< 2004)" SortExpression="FX1_PRC" Visible="false">
                            <ItemStyle Width="5%" HorizontalAlign="RIGHT" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="QTDE  Mercado" SortExpression="FX2_QTDE">
                            <ItemStyle Width="7%" HorizontalAlign="RIGHT" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="VALOR Mercado" SortExpression="FX2_VLR">
                            <ItemStyle Width="7%" HorizontalAlign="RIGHT" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="MARKET SHARE %" SortExpression="FX2_MKT" Visible="false">
                            <ItemStyle Width="5%" HorizontalAlign="RIGHT" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="MARKET SHARE  %" SortExpression="FX2_PRC">
                            <ItemStyle Width="5%" HorizontalAlign="RIGHT" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="MARKET SHARE QTDE %" SortExpression="FX1_MKT" Visible="TRUE">
                            <ItemStyle Width="7%" HorizontalAlign="RIGHT" />
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





