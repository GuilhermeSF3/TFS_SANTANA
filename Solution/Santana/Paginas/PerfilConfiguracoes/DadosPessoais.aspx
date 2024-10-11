<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="DadosPessoais.aspx.vb" Inherits="Santana.Paginas.PerfilConfiguracoes.DadosPessoais" Title="Ordinario Carga" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Ordinario Carga</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
             <nav class="navbar navbar-default" role="navigation">
                <div class="container-fluid btn-group-sm" style="display: flex; justify-content: space-between; align-items: center;">
                    <div style="flex-grow: 1;">
                        <h2 style="margin: 0;">Dados Pessoais</h2>
                    </div>
                    <asp:Button ID="btnMenu" runat="server" Text="Menu Principal" CssClass="btn btn-default navbar-btn" style="height: 40px;" OnClick="btnMenu_Click" />
                </div>
            </nav>

            <div class="form-group">
                <label for="txtNomeUsuario"  style="margin-left:30px;">Nome de Usuário:</label>
                <asp:TextBox ID="txtNomeUsuario" runat="server" CssClass="form-control" style="width:22%; margin-left:30px;" Text='<%# ContextoWeb.UsuarioLogado.NomeUsuario %>' />
            </div>

            <div class="form-group">
                <label for="txtEmail"  style="margin-left:30px;">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" style="width:22%; margin-left:30px;" Text='<%# ContextoWeb.UsuarioLogado.EMail %>' />
            </div>

            <div class="form-group">
                <label for="txtNomeCompleto" style="margin-left:30px;">Nome Completo:</label>
                <asp:TextBox ID="txtNomeCompleto" runat="server" CssClass="form-control" style="width:22%; margin-left:30px;" Text='<%# ContextoWeb.UsuarioLogado.NomeCompleto %>' />
            </div>

            <div class="form-group">
                <label for="txtCPF"  style="margin-left:30px;">CPF:</label>
                <asp:TextBox ID="txtCPF" runat="server" CssClass="form-control" style="width:22%; margin-left:30px;" Text='<%# ContextoWeb.UsuarioLogado.Cpf %>' />
            </div>
            <asp:Button style="margin-left:30px;" ID="Button1" runat="server" Text="Salvar" CssClass="btn btn-success" OnClick= "btnSalvar_Click" />
            

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
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>


</asp:Content>





