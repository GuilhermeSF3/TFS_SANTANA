<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="BaseCaptacao.aspx.vb" Inherits="Santana.Paginas.Financeiro.BaseCaptacao" Title="Base de Captação - Financeiro" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Base de Captação - Financeiro</title>
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
                                        <p class="navbar-text" style="float: none; margin: 0px">Data Ref:</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDataAnteriorApp" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnteriorApp_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <cc1:Datax ID="txtDataAplicacao" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaDataApp" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaDataApp_Click"></asp:Button>
                                    </div>
                                </div>
                            </li>

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
                                    <div style="height: 20px">
                                    </div>
                                    <asp:Button ID="btnCarregar" runat="server" Text="Carregar" class="btn btn-primary navbar-btn" OnClick="btnCarregar_Click"></asp:Button>
                                    <asp:Button ID="btnMenu" runat="server" Text="Menu Principal" class="btn btn-default navbar-btn" OnClick="btnMenu_Click"></asp:Button>

                                </div>
                            </li>
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

            <div id="dvConsultas" runat="server" style="height: 590px; width: 100%; overflow: auto; visibility: visible">

                <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                    AllowPaging="True" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowCreated="GridView1_RowCreated" ShowFooter="false"
                    OnDataBound="GridView1_DataBound" Font-Size="9pt">



                    <RowStyle Height="28px" />
                    <Columns>

                        <asp:TemplateField HeaderText="Data emissão" SortExpression="DT_EMISSAO">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Data Vencto" SortExpression="DT_VCTO">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dias" SortExpression="DIAS">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Pré / CDI" SortExpression="PRE_CDI">
                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Taxa" SortExpression="TAXA">
                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Série / Operação" SortExpression="GERENTE">
                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Valor Original" SortExpression="VLR_ORIGINAL">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="" SortExpression="VLR_MES_ANT">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="" SortExpression="VLR_MES_ATUAL">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="" SortExpression="VLR_REND_MES">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Proj. Resgate Vencto" SortExpression="VLR_PROJETADO">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cliente" SortExpression="CLIENTE">
                            <ItemStyle Width="35%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Gerente" SortExpression="GERENTE">
                            <ItemStyle Width="7%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Origem" SortExpression="ORIGEM">
                            <ItemStyle Width="7%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Código do Papel" SortExpression="COD_PAPEL">
                            <ItemStyle Width="7%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Calc. Total" SortExpression="CALC_TOTAL">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Série / Operação" SortExpression="SERIE_OPERACAO">
                            <ItemStyle Width="7%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Papel_Codigo" SortExpression="PAPEL_CODIGO">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="% CDI Real" SortExpression="PRC_CDI_REAL">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField> 
                        
                        <asp:TemplateField HeaderText="% CDI Intermed" SortExpression="PRC_CDI_INTERM">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField> 
                        
                        <asp:TemplateField HeaderText="% CDI Final" SortExpression="PRC_CDI_FINAL">
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
                freezesize: 2,
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





