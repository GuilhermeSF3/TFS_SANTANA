<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="FechamentoCobranca.aspx.vb" Inherits="Santana.Paginas.Cobranca.FechamentoCobranca" Title="Fechamento de cobranca" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Fechamento de Cobranca</title>
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
                            <li>
                                <div style="margin: 5px">
                                    <div style="height: 20px">
                                    </div>
                         <h2> Fechamento de Cobrança</h2>

                                </div>
                            </li>
                        </ul>
                        <ul class="nav navbar-nav navbar-right">
                            <li>
                                <div style="margin: 5px">
                                    <div style="height: 20px">
                                    </div>
                                    <div class="btn-group-sm  ">
                                           <asp:Button ID="btnMenu" runat="server" Text="Menu Principal" class="btn btn-default navbar-btn" OnClick="btnMenu_Click"></asp:Button>
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
                                </div>
                    </div>
            </nav>
            <div style="padding: 0px 0px 0px 80px;">
           
                <p>Forçar pagamentos recebidos no último dia </p>
                <div style="display: flex;">
                    <div style="margin: 0px;" class="btn-group">
                        <div style="margin: 5px">
                            <div>
                                <p class="navbar-text" style="float: none; margin: auto; color: gray;">Último dia útil do mês</p>
                            </div>
                            <span class="btn" style="padding: 0px; border-width: 0px;">
                                <cc1:Datax ID="txtDataDe1" runat="server" MaxLength="10" Width="100px" Enabled="false" CssClass="form-control navbar-btn"></cc1:Datax>
                            </span>         
                        </div>
                    </div>
                </div>
            </div>
            <div style="padding: 0px 0px 0px 80px;">
                <p>Forçar pagamentos parcial </p>
                <div style="display: flex;">
                    <div style="margin: 0px; display: flex;" class="btn-group">
                        <div style="margin: 5px">
                            <div>
                                <p class="navbar-text" style="float: none; margin: auto; color: gray;">Primeiro dia útil do mês</p>
                            </div>
                       
                            <span class="btn" style="padding: 0px; border-width: 0px;">
                                <cc1:Datax ID="txtDataDe2" runat="server" MaxLength="10" Width="100px" Enabled="false" CssClass="form-control navbar-btn"></cc1:Datax>
                            </span>
                         
                        </div>
                        <div style="margin: 5px">
                            <div>
                                <p class="navbar-text" style="float: none; margin: auto; color: gray;">Último dia útil do mês</p>
                            </div>
                     
                            <span class="btn" style="padding: 0px; border-width: 0px;">
                                <cc1:Datax ID="txtDataAte" runat="server" MaxLength="10" Width="100px" Enabled="false" CssClass="form-control navbar-btn"></cc1:Datax>
                            </span>
                      
                        </div>
                    </div>
                </div>
            </div>
            <div style="padding: 0px 0px 0px 80px;">
                <p>Relatórios do IP </p>
                <div style="display: flex;">
                    <div style="margin: 0px; display: flex;" class="btn-group">
                        <div style="margin: 5px">
                            <div>
                                <p class="navbar-text" style="float: none; margin: auto; color: gray;">Dia do fechamento</p>
                            </div>
                
                            <span class="btn" style="padding: 0px; border-width: 0px;">
                                <cc1:Datax ID="txtDataDe3" runat="server" MaxLength="10" Width="100px" Enabled="false" CssClass="form-control navbar-btn"></cc1:Datax>
                            </span>
                         
                        </div>
                    </div>
                </div>
                 <asp:Button ID="Button9" runat="server" Text="Carregar" class="btn btn-primary navbar-btn"  OnClick="btnCarregar_Click"></asp:Button>
            </div>

    

        </ContentTemplate>

    </asp:UpdatePanel>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
    <script>
        window.onload = function () {
            function getPrimeiroDiaDoMesAnterior() {
                var data = new Date();
                data.setMonth(data.getMonth() - 1); 
                data.setDate(1); 
                return formatDate(data);
            }

            function getUltimoDiaUtilMesAnterior() {
                var data = new Date();
                data.setMonth(data.getMonth() - 1); 
                data.setMonth(data.getMonth() + 1);  
                data.setDate(0);      
                if (data.getDay() === 0) {
                    data.setDate(data.getDate() - 2);
                } else if (data.getDay() === 6) {
                    data.setDate(data.getDate() - 1); 
                }

                return formatDate(data);
            }

            function getUltimoDiaUtil() {
                var data = new Date();
                data.setMonth(data.getMonth() - 1);
                data.setMonth(data.getMonth() + 1);
                data.setDate(0);
              

                return formatDate(data);
            }




            function formatDate(data) {
                var dia = data.getDate();
                var mes = data.getMonth() + 1; 
                var ano = data.getFullYear();
                return (dia < 10 ? '0' + dia : dia) + '/' + (mes < 10 ? '0' + mes : mes) + '/' + ano;
            }

      
            document.getElementById('<%= txtDataDe1.ClientID %>').value = getUltimoDiaUtilMesAnterior();
            document.getElementById('<%= txtDataDe2.ClientID %>').value = getPrimeiroDiaDoMesAnterior();
            document.getElementById('<%= txtDataDe3.ClientID %>').value = getUltimoDiaUtil();
            document.getElementById('<%= txtDataAte.ClientID %>').value = getUltimoDiaUtil();

        }
</script>

</asp:Content>





