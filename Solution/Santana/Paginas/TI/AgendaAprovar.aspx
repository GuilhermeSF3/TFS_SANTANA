<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="AgendaAprovar.aspx.vb" Inherits="Santana.Paginas.TI.AgendaAprovar" Title="Aprovar Agenda" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Aprovar Agenda</title>
    <style>
        .grid-view th, .grid-view td {
            padding: 10px;
            text-align: left;
        }

        .grid-view th {
            background-color: #f2f2f2;
        }

        .grid-view tr:nth-child(even) {
            background-color: #f9f9f9;
        }

        .grid-view tr:hover {
            background-color: #f1f1f1;
        }

        .btn-confirm {
            background-color: #4CAF50;
            color: white;
            padding: 10px 20px;
            border: none;
            cursor: pointer;
        }

        .modal-full {
    width: 95%;
    max-width: none;
}
        .checkbox-horizontal td {
    padding-right: 20px; /* espaço entre os itens */
    white-space: nowrap;
}

            .btn-confirm:hover {
                background-color: #45a049;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            <asp:Panel ID="pnlAgendas" runat="server" Visible="False">
                <h3>Detalhes das Agendas</h3>
                <asp:GridView ID="gvAgendas" runat="server" AutoGenerateColumns="False" CssClass="grid-view">
                    <Columns>
                        <asp:BoundField DataField="Historico" HeaderText="Histórico" />
                        <asp:BoundField DataField="Departamento" HeaderText="Departamento" />
                        <asp:BoundField DataField="Data_Pagamento" HeaderText="Data de Pagamento" />
                        <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                        <asp:BoundField DataField="Valor_Bruto" HeaderText="Valor Bruto" />
                        <asp:BoundField DataField="Valor_Liquido" HeaderText="Valor Líquido" />
                        <asp:BoundField DataField="Favorecido" HeaderText="Favorecido" />
                        <asp:BoundField DataField="Cpf_Cnpj" HeaderText="CPF/CNPJ" />
                        <asp:BoundField DataField="Forma_de_Pagamento" HeaderText="Forma de Pagamento" />
                        <asp:BoundField DataField="Banco" HeaderText="Banco" />
                        <asp:BoundField DataField="Agencia" HeaderText="Agência" />
                        <asp:BoundField DataField="Conta_Corrente" HeaderText="Conta Corrente" />
                          <asp:BoundField DataField="Empresa" HeaderText="Empresa" />
                          <asp:BoundField DataField="Digitador" HeaderText="Digitador" />
                    </Columns>
                </asp:GridView>
                <div style="margin-top: 20px;">
                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModalCenter">Confirmar Aprovação</button>
                </div>
            </asp:Panel>

            <!-- Modal de Confirmação -->
            <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                <div class="modal-dialog modal-full modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="confirmModalLabel">Confirmação</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <p>Deseja realmente aprovar a agenda?</p>
                            <asp:CheckBox ID="chkConfirm" runat="server" Text="Aprovar" />
                            <br />
                            <p>Recebedor </p>
                            <asp:DropDownList CssClass="form-control" ID="ddlAprovador" runat="server">
                                <asp:ListItem Text="BPO Financeiro" Value="bpo@logtechcontabil.com.br"></asp:ListItem>
                                <asp:ListItem Text="CloudWalk" Value="renato.kempe@cloudwalk.io"></asp:ListItem>  
                            </asp:DropDownList>
                            <br />
                            <p>Selecione as pessoas que estarão em cópia:</p>
                           <asp:CheckBoxList ID="chkCopiaEmails" runat="server" CssClass="checkbox-horizontal" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Cesar" Value="cesar@sf3.com.br"></asp:ListItem>
                                <asp:ListItem Text="Marcelo" Value="mm@sf3.com.br"></asp:ListItem>
                                <asp:ListItem Text="Luiz" Value="luiz@sf3.com.br"></asp:ListItem>
                                <asp:ListItem Text="Junior" Value="junior@sf3.com.br"></asp:ListItem>
                                <asp:ListItem Text="Laura" Value="laura@sf3.com.br"></asp:ListItem>
                                <asp:ListItem Text="Contas a Pagar" Value="contasapagar@sf3.com.br"></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="btnModalConfirmar" runat="server" Text="Confirmar" CssClass="btn btn-primary" OnClick="btnModalConfirmar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnModalConfirmar" />
        </Triggers>

    </asp:UpdatePanel>

    <script type="text/javascript">
        function openModal() {
            $('#confirmModal').modal('show');
        }
    </script>
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

