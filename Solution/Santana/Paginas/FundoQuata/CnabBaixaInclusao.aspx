<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="CnabBaixaInclusao.aspx.vb" Inherits="Santana.Paginas.FundoQuata.CnabBaixaInclusao" Title="Cnab Baixa Inclusao" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Cnab Baixa Inclusao</title>
    

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
                                <div style="width: 15px" class="btn-group"></div>
                            </li>
                            <lidiv style="margin: 5px">

                                <div style="display: flex; flex-direction: row; gap: 30px;">


                                    <!-- Data de Referência -->
                                    <div style="display: flex; flex-direction: column; align-items: start; text-align: start;">
                                        <label class="navbar-text" style="float: none; margin: 0;">Data de Referência</label>
                                        <asp:TextBox ID="txtData" runat="server" MaxLength="10" CssClass="form-control navbar-btn datepicker" Style="width: 100px;"></asp:TextBox>
                                    </div>

                                    <div  style="display: flex; align-items: flex-end !important; flex-direction: row; gap: 10px;">
                                        <asp:Button ID="btnCarregaInv" runat="server" Text="Carregar" CssClass="btn btn-success navbar-btn"  OnClick="btnCarregar_Click"  />
                                    </div>

                                    <!-- Contrato -->
                                    <div style=" display: flex;  flex-direction: column; align-items: start; text-align: start;">
                                        <label class="navbar-text" style="float: none; margin: 0;">Contrato</label>
                                        <asp:TextBox ID="txtContract" runat="server" MaxLength="10" CssClass="form-control navbar-btn datepicker"  Style="width: 150px;"></asp:TextBox>
                                    </div>

                                    <!-- Parcela -->
                                    <div style=" display:flex; flex-direction: column; align-items: start; text-align: start;">
                                        <label class="navbar-text" style="float: none; margin: 0;">Parcela</label>
                                        <asp:TextBox ID="TxtParcel" runat="server" MaxLength="10" CssClass="form-control navbar-btn datepicker" Style="width: 150px;"></asp:TextBox>
                                    </div>

                                    <!-- Botões -->
                                    <div style=" display:flex; align-items: flex-end !important; flex-direction: row; gap: 10px;">
                                        <asp:Button ID="btnCarregar" runat="server" Text="Incluir " CssClass="btn btn-warning navbar-btn" OnClick="btnCarregar_Click" />
                                        <asp:Button ID="btnCarregar2" runat="server" Text="Excluir" CssClass="btn btn-danger navbar-btn"  OnClick="btnCarregar_ClickDelete" />
                                    </div>


                                </div>
                            </li>
                        </ul>
                        <ul class="nav navbar-nav navbar-right">
                            <li>
                                <div style="margin: 5px">
                                    <div style="height: 20px">
                                    </div>
                                    <div class="btn-group-sm">
                                        <asp:Button ID="btnMenu" runat="server" Text="Menu Principal" CssClass="btn btn-default navbar-btn w-300" OnClick="btnMenu_Click" />
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
            <div id="dvRiscoAnalitico" runat="server" style="height: 590px; overflow: auto; visibility: visible; margin-left: 50px; margin-top: 0px; text-align: center;">
                <asp:GridView ID="GridViewRiscoAnalitico" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                    AllowPaging="True" PageSize="30" OnPageIndexChanging="GridViewRiscoAnalitico_PageIndexChanging"
                    OnRowCreated="GridViewRiscoAnalitico_RowCreated" ShowFooter="false"
                    OnDataBound="GridViewRiscoAnalitico_DataBound" Font-Size="9pt">
                    <RowStyle Height="28px" />
                    <Columns>
                        <asp:TemplateField HeaderText="SEU NUMERO" SortExpression="PASEUNRO">
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="OPERAÇÃO" SortExpression="OPNROPER">
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VENCIMENTO PARCELA" SortExpression="PADTVCTO">
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VALOR PAGO" SortExpression="VLR_PAGO">
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VALOR TOTAL" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightGreen" SortExpression="TotalValor">
                            <ItemStyle Width="10%" HorizontalAlign="Center" Font-Bold="true" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="QUANTIDADE DE BAIXAS" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="LightGreen" SortExpression="TotalPaseunro">
                            <ItemStyle Width="10%" HorizontalAlign="Center" Font-Bold="true" />
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
    <script type="text/javascript" src="https://uxsolutions.github.io/bootstrap-datepicker/boot‌​strap-datepicker/js/‌​locales/bootstrap-da‌​tepicker.pt-BR.min.j‌​s"></script>


    <script type="text/javascript">

        jQuery(function ($) {
            $.datepicker.regional['pt-BR'] = {
                closeText: 'Fechar',
                prevText: '&#x3c;Anterior',
                nextText: 'Pr&oacute;ximo&#x3e;',
                currentText: 'Hoje',
                monthNames: ['Janeiro', 'Fevereiro', 'Mar&ccedil;o', 'Abril', 'Maio', 'Junho',
                    'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun',
                    'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                dayNames: ['Domingo', 'Segunda-feira', 'Ter&ccedil;a-feira', 'Quarta-feira', 'Quinta-feira', 'Sexta-feira', 'Sabado'],
                dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
                dayNamesMin: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
                weekHeader: 'Sm',
                dateFormat: 'dd/mm/yy',
                firstDay: 0,
                isRTL: false,
                showMonthAfterYear: false,
                yearSuffix: ''
            };
            $.datepicker.setDefaults($.datepicker.regional['pt-BR']);
        });
      
            function habilitarCampos() {
                document.getElementById('<%= txtContract.ClientID %>').disabled = false;
        document.getElementById('<%= TxtParcel.ClientID %>').disabled = false;
        document.getElementById('<%= btnCarregar.ClientID %>').disabled = false;
        document.getElementById('<%= btnCarregar2.ClientID %>').disabled = false;
    }




        function pageLoad() {

            $('#<%=GridViewRiscoAnalitico.ClientID%>').gridviewScroll({
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
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/locales/bootstrap-datepicker.pt-BR.min.js"></script>
     


</asp:Content>





