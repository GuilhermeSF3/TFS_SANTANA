<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="PDDSafraTempo.aspx.vb" Inherits="Santana.Paginas.RiscoCredito.PDDSafraTempo" Title="PDD Por Safra No Tempo" EnableEventValidation="false"%>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>PDD Por Safra No Tempo</title>
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
               

                <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                    AllowPaging="False" PageSize="34"
                    OnRowCreated="GridView1_RowCreated" ShowFooter="false" DataKeyNames="DESCR">


                    <RowStyle Height="31px" />
                    <Columns>

                        <asp:TemplateField HeaderText="ANO-MÊS PRODUCAO  X MESES DECORRIDOS (% )" SortExpression="DESCR">
                            <ItemStyle Width="400px" HorizontalAlign="left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="0" SortExpression="m0">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="1" SortExpression="m1">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="2" SortExpression="m2">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="3" SortExpression="m3">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="4" SortExpression="m4">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="5" SortExpression="m5">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="6" SortExpression="m6">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="7" SortExpression="m7">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="8" SortExpression="m8">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="9" SortExpression="m9">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="10" SortExpression="m10">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="11" SortExpression="m11">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="12" SortExpression="m12">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="13" SortExpression="m13">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="14" SortExpression="m14">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="15" SortExpression="m15">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="16" SortExpression="m16">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="17" SortExpression="m17">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="18" SortExpression="m18">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="19" SortExpression="m19">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="20" SortExpression="m20">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="21" SortExpression="m21">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="22" SortExpression="m22">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>		
                        				
                        <asp:TemplateField HeaderText="23" SortExpression="m23">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="24" SortExpression="m24">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="25" SortExpression="m25">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>						

                        <asp:TemplateField HeaderText="26" SortExpression="m26">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>
	
                        <asp:TemplateField HeaderText="27" SortExpression="m27">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="28" SortExpression="m28">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="29" SortExpression="m29">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="30" SortExpression="m30">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="31" SortExpression="m31">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="32" SortExpression="m32">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="33" SortExpression="m33">
                            <ItemStyle Width="6%" HorizontalAlign="right" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField>
                            <ItemStyle Width="1%" />
                        </asp:TemplateField>



                    </Columns>

                    <HeaderStyle CssClass="GridviewScrollC3Header" />
                    <RowStyle CssClass="GridviewScrollC3Item" />
                    <PagerStyle CssClass="GridviewScrollC3Pager" />

                </asp:GridView>
                
                
                
                <asp:GridView ID="GridView2" runat="server"
                        AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                        AllowPaging="False" PageSize="30"
                        OnRowCreated="GridView2_RowCreated" ShowFooter="true">


                        <RowStyle Height="31px" />
                        <Columns>


                            <asp:TemplateField HeaderText="Referência" SortExpression="DESCR">
                                <ItemStyle Width="400px" HorizontalAlign="left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="0" SortExpression="M1">
                                <ItemStyle Width="6%" HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="1" SortExpression="M2">
                                <ItemStyle Width="6%" HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="2" SortExpression="M3">
                                <ItemStyle Width="6%" HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="3" SortExpression="M4">
                                <ItemStyle Width="6%" HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="4" SortExpression="M5">
                                <ItemStyle Width="6%" HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="5" SortExpression="M6">
                                <ItemStyle Width="6%" HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="6" SortExpression="M7">
                                <ItemStyle Width="6%" HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="7" SortExpression="M8">
                                <ItemStyle Width="6%" HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="8" SortExpression="M9">
                                <ItemStyle Width="6%" HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="9" SortExpression="M10">
                                <ItemStyle Width="6%" HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="10" SortExpression="M11">
                                <ItemStyle Width="6%" HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="11" SortExpression="M12">
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
