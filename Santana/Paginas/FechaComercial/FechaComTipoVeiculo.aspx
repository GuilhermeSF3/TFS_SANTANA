<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="FechaComTipoVeiculo.aspx.vb" Inherits="Santana.FechaComTipoVeiculo" Title="Evolução Tipo de Veículo" EnableEventValidation="false"%>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Evolução Tipo de Veículo</title>

    <script type="text/javascript" src="../../visifire/Visifire.js"></script>
    <script type="text/javascript">
        var vChart1 = new Visifire("../../visifire/SL.Visifire.Charts.xap", 800, 400);
        vChart1.setWindowlessState(true);
        var vChart2 = new Visifire("../../visifire/SL.Visifire.Charts.xap", 800, 400);
        vChart2.setWindowlessState(true);
        var vChart3 = new Visifire("../../visifire/SL.Visifire.Charts.xap", 800, 400);
        vChart3.setWindowlessState(true);
        var vChart4 = new Visifire("../../visifire/SL.Visifire.Charts.xap", 800, 400);
        vChart4.setWindowlessState(true);
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel" runat="server" >
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

                        <ul class="nav navbar-nav" >
          
                            <li> 
                                <div style="margin:5px" >
                                    <div>
                                        <p class="navbar-text" style="float:none; margin:0" >Data de Referencia</p>
                                    </div>
                                    <div class="btn-group">                                                              
                                        <asp:Button ID="btnDataAnterior" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnterior_Click"></asp:Button>
                                        <span class="btn" style="padding: 0;border-width: 0;"> 
                                            <cc1:Datax ID="txtData" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaData" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaData_Click"></asp:Button>                                   
                                    </div>
                                 <div>
                            </li>
                      
                                
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p id="ddlRelatorioLabel" class="navbar-text" style="float: none; margin: auto">Relatório</p>
                                    </div>
                                    <asp:DropDownList ID="ddlRelatorio" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>      


                            <li>
                                <div style="width: 15px" class="btn-group"></div>
                            </li>
                            <li>
                                <div style="margin:5px">
                                    <div style="height:20px " >
                                       
                                    </div>
                                    <asp:Button ID="btnCarregar" runat="server" Text="Carregar" class="btn btn-primary navbar-btn" OnClick="btnCarregar_Click"></asp:Button>
                                    <asp:Button ID="btnMenu" runat="server" Text="Menu Principal" class="btn btn-default navbar-btn" OnClick="btnMenu_Click"></asp:Button>
                                   
                                </div> 
                            </li>

                        </ul>

                        <ul class="nav navbar-nav navbar-right">
                            <li>
                                <div style="margin:5px">
                                    <div style="height:20px " >
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



    <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
        <ContentTemplate>



            <div id="dvConsultas" style="width: 100%; overflow: auto;">
                <%--<span style="display:block; padding:0; text-align: left">Financiamento por tipo de Veiculo -  gráfico de evolução (por <%=ddlRelatorio.SelectedItem %>) - Ano <%=txtData.Data.Year%></span>--%>

                <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                    AllowPaging="True" PageSize="12" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowCreated="GridView1_RowCreated" ShowFooter="false" OnDataBound="GridView1_DataBound" DataKeyNames="DESCR">


                    <RowStyle Height="31px" />
                    <Columns>

                        <asp:TemplateField HeaderText="DESCR" SortExpression="DESCR">
                            <ItemStyle Width="200px" HorizontalAlign="left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="M13" SortExpression="m13">
                            <ItemStyle Width="6%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P13" SortExpression="p13">
                            <ItemStyle Width="4%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="M12" SortExpression="m12">
                            <ItemStyle Width="6%" HorizontalAlign="center" />
                        </asp:TemplateField>						
                        <asp:TemplateField HeaderText="P12" SortExpression="p12">
                            <ItemStyle Width="4%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="M11" SortExpression="m11">
                            <ItemStyle Width="6%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P11" SortExpression="p11">
                            <ItemStyle Width="4%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="M10" SortExpression="m10">
                            <ItemStyle Width="6%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P10" SortExpression="p10">
                            <ItemStyle Width="4%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="M9" SortExpression="m9">
                            <ItemStyle Width="6%" HorizontalAlign="center" />
                        </asp:TemplateField>						
                        <asp:TemplateField HeaderText="P9" SortExpression="p9">
                            <ItemStyle Width="4%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="M8" SortExpression="m8">
                            <ItemStyle Width="6%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P8" SortExpression="p8">
                            <ItemStyle Width="4%" HorizontalAlign="center" />
                        </asp:TemplateField>	
                        <asp:TemplateField HeaderText="M7" SortExpression="m7">
                            <ItemStyle Width="6%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P7" SortExpression="p7">
                            <ItemStyle Width="4%" HorizontalAlign="center" />
                        </asp:TemplateField>


<%--                        <asp:TemplateField HeaderText="" SortExpression="mediaAnt">
                            <ItemStyle Width="6%" HorizontalAlign="center" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="M6" SortExpression="m6">
                            <ItemStyle Width="6%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P6" SortExpression="p6">
                            <ItemStyle Width="4%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="M5" SortExpression="m5">
                            <ItemStyle Width="6%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P5" SortExpression="p5">
                            <ItemStyle Width="4%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="M4" SortExpression="m4">
                            <ItemStyle Width="6%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P4" SortExpression="p4">
                            <ItemStyle Width="4%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="M3" SortExpression="m3">
                            <ItemStyle Width="6%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P3" SortExpression="p3">
                            <ItemStyle Width="4%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="M2" SortExpression="m2">
                            <ItemStyle Width="6%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P2" SortExpression="p2">
                            <ItemStyle Width="4%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="M1" SortExpression="m1">
                            <ItemStyle Width="6%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P1" SortExpression="p1">
                            <ItemStyle Width="4%" HorizontalAlign="center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="cor" SortExpression="cor_linha" Visible="false">
                            <ItemStyle Width="6%" HorizontalAlign="center" />
                        </asp:TemplateField>


                    </Columns>

                    <HeaderStyle CssClass="GridviewScrollC3Header" />
                    <RowStyle CssClass="GridviewScrollC3Item" />
                    <PagerStyle CssClass="GridviewScrollC3Pager" />

                </asp:GridView>

            </div>



            <div id="VisifireChart1" style="height: 400px; width: 800px; font-family: Arial, Helvetica, sans-serif; font-size: 24px;">
            </div>

            <div style="width: 15px" class="btn-group"></div>
            
            <div id="VisifireChart2" style="height: 400px; width: 800px; font-family: Arial, Helvetica, sans-serif; font-size: 24px;">
            </div>

            <div id="VisifireChart3" style="height: 400px; width: 800px; font-family: Arial, Helvetica, sans-serif; font-size: 24px;">
            </div>

            <div style="width: 15px" class="btn-group"></div>
            
            <div id="VisifireChart4" style="height: 400px; width: 800px; font-family: Arial, Helvetica, sans-serif; font-size: 24px;">
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
