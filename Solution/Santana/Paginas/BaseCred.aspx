<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="BaseCred.aspx.vb" Inherits="Santana.BaseCred" Title="Base de Crédito Consolidada" EnableEventValidation="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Base de Crédito Consolidada</title>



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
                                        <p class="navbar-text" style="float: none; margin: 0px">Data de Referencia</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDataAnterior" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnterior_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <cc1:Datax ID="txtData" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaData" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaData_Click"></asp:Button>
                                    </div>
                                    <div>
                            </li>



                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Clientes</p>
                                    </div>
                                    <asp:DropDownList ID="ddlAgente" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>
                            </ul>


                                                <ul class="nav navbar-nav">
                            <li>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtLocalizar" Width="180px" runat="server" Visible="True" Enabled="true"  CssClass="form-control navbar-btn"></asp:TextBox>
                                        </td>
                                        <td>
                                                <asp:Button ID="btnCarregar" runat="server" Text="Carregar" CssClass="btn btn-default navbar-btn" OnClick="btnCarregar_Click"></asp:Button>
                                                <asp:Button ID="btnMenu" runat="server" Text="Menu Principal" CssClass="btn btn-default navbar-btn" OnClick="btnMenu_Click"></asp:Button>
                                                <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="btn btn-primary navbar-btn" OnClick="btnLimpar_Click" ></asp:Button>
                                        </td>
                                        <td>
                                        </td> 
                                    </tr>

                                </table> 
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
            </nav>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="dvConsultas" style="height: 1060px; width: 100%; overflow: auto;">


                <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                    AllowPaging="True" PageSize="20" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowCreated="GridView1_RowCreated" ShowFooter="false" OnDataBound="GridView1_DataBound" DataKeyNames="empresa">


                    <RowStyle Height="31px" />
                    <Columns>

                      <asp:TemplateField HeaderText="Empr.(1=SCM,2=Fact)" SortExpression="Empresa">
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                      <asp:TemplateField HeaderText="CONTRATO" SortExpression="CONTRATO">
                            <ItemStyle Width="6%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                      <asp:TemplateField HeaderText="CPF CNPJ" SortExpression="CPF_CNPJ">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                     <asp:TemplateField HeaderText="NOME CLIENTE" SortExpression="NOME_CLIENTE">
                            <ItemStyle Width="25%" HorizontalAlign="Left" />
                     </asp:TemplateField>

                        <asp:TemplateField HeaderText="ATRASO" SortExpression="ATRASO">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Modalidade" SortExpression="Modalidade">
                            <ItemStyle Width="17%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cod.Cli." SortExpression="cod_cli">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rating Original" SortExpression="Rating_Original">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rating Final" SortExpression="Rating_Final">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SALDO INSCRITO" SortExpression="SALDO_INSCRITO">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VALOR FUTURO" SortExpression="VF">
                            <ItemStyle Width="10%" HorizontalAlign="Right"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="cod.modal." SortExpression="cod_modalidade">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        
                        <asp:TemplateField >
                            <ItemStyle Width="1%"  />
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



    <asp:HiddenField ID="hfGridView1SV" runat="server" OnValueChanged="hfGridView1SV_ValueChanged" />
    <asp:HiddenField ID="hfGridView1SH" runat="server" OnValueChanged="hfGridView1SH_ValueChanged" />

    <script type="text/javascript">


        function pageLoad() {
            $('#<%=GridView1.ClientID%>').gridviewScroll({
                width: 100 + "%",
                height: 100 + "%",
                freezesize: 0,
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





