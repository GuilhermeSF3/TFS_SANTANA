<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="Inadimplencia.aspx.vb" Inherits="Santana.Paginas.Cobranca.P123Gerencial.Inadimplencia" Title="Inadimplência" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Inadimplência</title>
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
                                        <p class="navbar-text" style="float: none; margin: 0px">Safra De:</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDataAnterior1" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnterior1_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <cc1:Datax ID="txtDataDe" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaData1" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaData1_Click"></asp:Button>
                                    </div>
                                </div>
                            </li>   
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Safra Até:</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDataAnterior2" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnterior2_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <cc1:Datax ID="txtDataAte" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaData2" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaData2_Click"></asp:Button>
                                    </div>

                                </div>
                            </li>

                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Data PDD:</p>
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
                                        <p class="navbar-text" style="float: none; margin: auto">Relatório</p>
                                    </div>
                                    <asp:DropDownList ID="ddlRelatorio" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
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

            <div id="dvConsultas" runat="server" style="height: 590px; width: 100%; overflow: auto; visibility: visible">

                <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                    AllowPaging="True" PageSize="13" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowCreated="GridView1_RowCreated" ShowFooter="false"
                    OnDataBound="GridView1_DataBound" Font-Size="9pt">


                    <RowStyle Height="28px" />
                    <Columns>


                        <asp:TemplateField HeaderText="" SortExpression="Descricao">
                            <ItemStyle Width="15%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="vlr A" SortExpression="fxA_vlr">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="A- 0 a 14" SortExpression="fxA">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="vlr B" SortExpression="fxB_vlr">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="B- 15 a 30" SortExpression="fxB">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="vlr C" SortExpression="fxC_vlr">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="C- 31 a 60" SortExpression="fxC">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="vlr D" SortExpression="fxD_vlr">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="D- 61 a 90" SortExpression="fxD">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="vlr E" SortExpression="fxE_vlr">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="E- 91 a 120" SortExpression="fxE">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="vlr F" SortExpression="fxF_vlr">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="F- 121 a 150" SortExpression="fxF">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="vlr G" SortExpression="fxG_vlr">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="G- 151 a 180" SortExpression="fxG">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="vlr H" SortExpression="fxH_vlr">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="H- 181 a 359" SortExpression="fxH">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="vlr HH" SortExpression="fxHH_vlr">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="HH- > 360 d" SortExpression="fxHH">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Produção" SortExpression="VLR_PROD">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="0-14" SortExpression="inad0">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="15-90" SortExpression="inad15">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="91-360" SortExpression="inad91">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText=">360" SortExpression="inad360">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
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





