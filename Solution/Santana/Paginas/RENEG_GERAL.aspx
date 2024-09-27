<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="RENEG_GERAL.aspx.vb" Inherits="Santana.RENEG_GERAL" Title="RENEGOCIADOS GERAL" EnableEventValidation="false"%>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>RENEGOCIADOS GERAL</title>



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

                        <ul class="nav navbar-nav" >
          
                            <li> 
                                <div style="margin:5px" >

                                    <div>
                                        <p class="navbar-text" style="float:none; margin:0px" >Data Inicial:</p>
                                        <cc1:Datax ID="txtDataINI" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>

                                        <p class="navbar-text" style="float:none; margin:0px" >Data Final:</p>
                                        <cc1:Datax ID="txtDataFIM" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        
                                        <p class="navbar-text" style="float:none; margin:0px" >Data de Referencia</p>


                                        <asp:Button ID="btnDataAnterior" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnterior_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px;border-width: 0px;"> 
                                            <cc1:Datax ID="txtData" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaData" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaData_Click"></asp:Button>                                   
                                 <div>
                            </li>
                      
                                
      
                            <li>
                                <div style="margin:5px">
                                    <div>
                                        <p class="navbar-text" style="float:none; margin:auto" >Agente</p>
                                    </div>
                                    <asp:DropDownList ID="ddlAgente" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>

           
                            <li>
                                <div style="margin:5px">
                                    <div>
                                        <p class="navbar-text" style="float:none; margin:auto" >Cobradora</p>
                                    </div>
                                    <asp:DropDownList ID="ddlCobradora" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="false" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>

                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Relatorio</p>
                                    </div>
                                    <asp:DropDownList ID="ddlRelatorio" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn" OnSelectedIndexChanged="ddlRelatorio_SelectedIndexChanged"></asp:DropDownList>
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

            <div id="dvConsultas" style="height: 590px; width: 100%; overflow: auto;">


                <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                    AllowPaging="True" PageSize="60" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowCreated="GridView1_RowCreated" OnRowCommand="GridView1_RowCommand" DataKeyNames="ATIVIDADE">


                    <RowStyle Height="31px" />
                    <Columns>

                      <asp:TemplateField HeaderText="PROFISSÃO" SortExpression="ATIVIDADE" ItemStyle-BackColor="#EFEFEF">
                            <ItemStyle Width="12%" HorizontalAlign="left" />
                        </asp:TemplateField>
                     <asp:TemplateField HeaderText="VLR OPERADO" SortExpression="OPERADO">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="% Vlr Oper" SortExpression="PER_OPERADO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="RECEBIDO" SortExpression="RECEBIDO">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="% Rec" SortExpression="PERC_RECEBIDO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PREJUIZO" SortExpression="PREJUIZO">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="% Prej" SortExpression="PERC_PREJUIZO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="% PERDA Prej" SortExpression="PERC_PREJUIZO_PERDA">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="DESCONTO" SortExpression="DESCONTO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="% Desc" SortExpression="PERC_DESCONTO">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="% PERDA Desc" SortExpression="PERC_DESCONTO_PERDA">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="TOTAL" SortExpression="PERDA_TOTAL">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="% Tot" SortExpression="PERC_TOTAL_PERDA">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="% PERDA (1)" SortExpression="PERC_TOTAL_PERDA1">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="VLR LIQ." SortExpression="LIQUIDADO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="% Vlr Liq (3)" SortExpression="PERC_LIQUIDADO">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="% PERDA (2)" SortExpression="PERC_LIQUIDADO_PERDA">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="VLR A RECEB." SortExpression="RECEBER">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="% Vlr A Rec (3)" SortExpression="PERC_ARECEBER">
                            <ItemStyle Width="4%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="% a Rec" SortExpression="PERC_ARECEBER_PERDA">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PDD" SortExpression="PDD">
                            <ItemStyle Width="4%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="% PERDA Vlr a Rec" SortExpression="PERC_PDD">
                            <ItemStyle Width="4%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="SAFRA" SortExpression="SAFRA">
                            <ItemStyle Width="4%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="% Safra" SortExpression="PERC_SAFRA">
                            <ItemStyle Width="4%" HorizontalAlign="Right" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="PERDA REALIZ." SortExpression="PERDA_REALIZADA">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="% PERDA Total" SortExpression="PERC_PERDA_REALIZADA"> 
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemStyle Width="3%" />
                        </asp:TemplateField>

                    </Columns>

                    <HeaderStyle CssClass="GridviewScrollC3Header" />
                    <RowStyle CssClass="GridviewScrollC3Item" />
                    <PagerStyle CssClass="GridviewScrollC3Pager" />

                </asp:GridView>



            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel"  >
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





