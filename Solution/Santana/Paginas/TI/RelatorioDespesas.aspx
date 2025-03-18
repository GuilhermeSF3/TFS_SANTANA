<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="RelatorioDespesas.aspx.vb" Inherits="Santana.Paginas.TI.RelatorioDespesas" Title="Relatorio de Despesas" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Relatório de despesas</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <nav class="navbar navbar-default" role="navigation">
                <div style="display: flex; justify-content: end; margin-right: 20px; align-items: end;" class="ml-auto">
                    <asp:Button ID="btnMenu" runat="server" Text="Menu Principal" CssClass="btn btn-default navbar-btn" OnClick="btnMenu_Click" />
                </div>
            </nav>


            <!-- END NAVBAR -->
            <!-- CONTAINER -->


            <div style="padding-left: 60px; padding-right: 60px;">
                <div style="display: flex; width: 100%; align-items: center; margin-bottom: 10px">
                    <div style="display: flex; align-items: center;">
                        <label for="ddlFiltros" class="form-label"></label>
                        <asp:DropDownList AutoPostBack="true" ID="DropDownList2" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Selecione" Value="" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Data de pagamento" Value="Data de pagamento"></asp:ListItem>
                            <asp:ListItem Text="Digitador" Value="Digitador"></asp:ListItem>
                            <asp:ListItem Text="Departamento" Value="Departamento"></asp:ListItem>
                            <asp:ListItem Text="Favorecido" Value="Favorecido"></asp:ListItem>
                        </asp:DropDownList>
                        <div style="margin-left: 10px;">
                            <asp:TextBox ID="txtFiltro" runat="server" MaxLength="10" CssClass="form-control datepicker" Style="width: 400px"></asp:TextBox>
                        </div>
                        <div style="margin-left: 10px">
                            <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary navbar-btn" />
                        </div>
                    </div>
                </div>


                <table class="table caption-top" style="width: 100%; table-layout: fixed;">
                    <thead style="background-color: lightgray">
                        <tr>
                            <th scope="col" style="text-align: center; vertical-align: middle;">DATA DE
                                <br>
                                PAGAMENTO</th>
                            <th scope="col" style="text-align: center; vertical-align: middle;">DIGITADOR</th>
                            <th scope="col" style="text-align: center; vertical-align: middle;">DEPARTAMENTO</th>
                            <th scope="col" style="text-align: center; vertical-align: middle;">HISTÓRICO</th>
                            <th scope="col" style="text-align: center; vertical-align: middle;">VALOR
                                <br>
                                BRUTO</th>
                            <th scope="col" style="text-align: center; vertical-align: middle;">VALOR
                                <br>
                                LIQUIDO</th>
                            <th scope="col" style="text-align: center; vertical-align: middle;">FAVORECIDO</th>
                            <th scope="col" style="text-align: center; vertical-align: middle;">CPF_CNPJ</th>
                            <th scope="col" style="text-align: center; vertical-align: middle;">FORMA DE
                                <br>
                                PAGAMENTO</th>
                            <th scope="col" style="text-align: center; vertical-align: middle;">BANCO</th>
                            <th scope="col" style="text-align: center; vertical-align: middle;">AGENCIA</th>
                            <th scope="col" style="text-align: center; vertical-align: middle;">CONTA
                                <br>
                                CORRENTE</th>
                        </tr>
                    </thead>

                    <tbody>
                    </tbody>
                </table>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



    <script type="text/javascript" src="https://uxsolutions.github.io/bootstrap-datepicker/boot‌​strap-datepicker/js/‌​locales/bootstrap-da‌​tepicker.pt-BR.min.j‌​s"></script>


    <script type="text/javascript">



        function favoritarPagina() {
            $.ajax({
                type: "POST",
                url: 'Favoritar.aspx/AdicionarFavorito',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    alert("Página favoritada com sucesso!");

                    // Alterar o ícone para o coração preenchido
                    document.getElementById('heart-icon').classList.add('favorited');
                    document.getElementById('heart-icon').classList.remove('bi-heart');
                    document.getElementById('heart-icon').classList.add('bi-heart-fill');
                },
                error: function (response) {
                    alert("Erro ao favoritar a página!");
                }
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
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/locales/bootstrap-datepicker.pt-BR.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

</asp:Content>





