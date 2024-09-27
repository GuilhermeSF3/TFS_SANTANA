<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="RollrateMensal.aspx.vb" Inherits="Santana.RollrateMensal" Title="Rollrate Mensal" EnableEventValidation="false"%>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Rollrate Mensal</title>



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
                                        <p class="navbar-text" style="float:none; margin:0px" >Data de Referencia</p>
                                    </div>
                                    <div class="btn-group">                                                              
                                        <asp:Button ID="btnDataAnterior" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnterior_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px;border-width: 0px;"> 
                                            <cc1:Datax ID="txtData" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaData" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaData_Click"></asp:Button>                                   
                                    </div>
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
                    OnRowCreated="GridView1_RowCreated" OnRowCommand="GridView1_RowCommand" DataKeyNames="ORDEM_LINHA">


                    <RowStyle Height="31px" />
                    <Columns>

                      <asp:TemplateField HeaderText="DT FECHA" SortExpression="DT_FECHA" ItemStyle-BackColor="#EFEFEF">
                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                     <asp:TemplateField HeaderText="DESCRICAO" SortExpression="DESCRICAO" ItemStyle-BackColor="#EFEFEF">
                            <ItemStyle Width="6%" HorizontalAlign="Left" BackColor="#EFEFEF" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="A QTDE" SortExpression="A_QTDE">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="A SALDO" SortExpression="A_SALDO">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton  ID="AddButtonA" runat="server" CommandName="A" CommandArgument='<%# Eval("ORDEM_LINHA")%>' >
                                    <asp:Image runat="server" ImageUrl="~/imagens/list_add1616.png" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                             <asp:TemplateField HeaderText="B QTDE" SortExpression="B_QTDE">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="B SALDO" SortExpression="B_SALDO">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="AddButtonB" runat="server" CommandName="B" CommandArgument='<%# Eval("ORDEM_LINHA")%>' >
                                    <asp:Image runat="server" ImageUrl="~/imagens/list_add1616.png" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="C QTDE" SortExpression="C_QTDE">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="C SALDO" SortExpression="C_SALDO">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="AddButtonC" runat="server" CommandName="C" CommandArgument='<%# Eval("ORDEM_LINHA")%>' >
                                    <asp:Image runat="server" ImageUrl="~/imagens/list_add1616.png" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="D QTDE" SortExpression="D_QTDE">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="D SALDO" SortExpression="D_SALDO">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="AddButtonD" runat="server" CommandName="D" CommandArgument='<%# Eval("ORDEM_LINHA")%>' >
                                    <asp:Image runat="server" ImageUrl="~/imagens/list_add1616.png" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="E QTDE" SortExpression="E_QTDE">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="E SALDO" SortExpression="E_SALDO">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="AddButtonE" runat="server" CommandName="E" CommandArgument='<%# Eval("ORDEM_LINHA")%>' >
                                    <asp:Image runat="server" ImageUrl="~/imagens/list_add1616.png" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="F QTDE" SortExpression="F_QTDE">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="F SALDO" SortExpression="F_SALDO">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="AddButtonF" runat="server" CommandName="F" CommandArgument='<%# Eval("ORDEM_LINHA")%>' >
                                    <asp:Image runat="server" ImageUrl="~/imagens/list_add1616.png" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="G QTDE" SortExpression="G_QTDE">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="G SALDO" SortExpression="G_SALDO">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="AddButtonG" runat="server" CommandName="G" CommandArgument='<%# Eval("ORDEM_LINHA")%>' >
                                    <asp:Image runat="server" ImageUrl="~/imagens/list_add1616.png" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="H QTDE" SortExpression="H_QTDE">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="H SALDO" SortExpression="H_SALDO">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="AddButtonH" runat="server" CommandName="H" CommandArgument='<%# Eval("ORDEM_LINHA")%>' >
                                    <asp:Image runat="server" ImageUrl="~/imagens/list_add1616.png" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HH QTDE" SortExpression="HH_QTDE">
                            <ItemStyle Width="4%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HH SALDO" SortExpression="HH_SALDO">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="AddButtonHH" runat="server" CommandName="HH" CommandArgument='<%# Eval("ORDEM_LINHA")%>' >
                                    <asp:Image runat="server" ImageUrl="~/imagens/list_add1616.png" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TTL QTDE" SortExpression="TTL_QTDE">
                            <ItemStyle Width="4%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TTL SALDO" SortExpression="TTL_SALDO">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
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





