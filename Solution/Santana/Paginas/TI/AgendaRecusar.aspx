<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="AgendaRecusar.aspx.vb" Inherits="Santana.Paginas.TI.AgendaRecusar" Title="Recusar Agenda" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>

    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Recusar Agenda</title>
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
            background-color: #f44336;
            color: white;
            padding: 10px 20px;
            border: none;
            cursor: pointer;
        }
        .btn-confirm:hover {
            background-color: #e53935;
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
                           <asp:TemplateField HeaderText="Selecionar">
            <ItemTemplate>
                <asp:CheckBox ID="chkSelecionar" runat="server" />
                <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("ID") %>' />
            </ItemTemplate>
        </asp:TemplateField>
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
                    </Columns>
                </asp:GridView>
                        <div style="margin-top: 20px;">
<button type="button" class="btn btn-danger" data-toggle="modal" data-target="#exampleModalCenter2">Recusar</button>
             </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

     <div class="modal fade" id="exampleModalCenter2" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
     <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="confirmModalLabel">Confirmação</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
     <p style="margin-right: 15px;">Deseja realmente recusar a agenda?</p>
     <asp:CheckBox ID="chkConfirm" runat="server" />
     <br />
     <p>Motivo</p>
     <asp:TextBox ID="txtMotivo" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
 </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnModalConfirmar" runat="server" Text="Confirmar" CssClass="btn btn-primary" OnClick="btnModalConfirmar_Click" />
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript" src="https://uxsolutions.github.io/bootstrap-datepicker/boot‌​strap-datepicker/js/‌​locales/bootstrap-da‌​tepicker.pt-BR.min.j‌​s"></script>



    <script>

        let arquivos = [];
        let removidos = [];

        function removerArquivo(index) {

            removidos.push(arquivos[index].name);

            arquivos.splice(index, 1);

            hdnRemovidos.value = removidos.join(",");

            atualizarLista();
        }

        function fecharModal() {
            $('#exampleModalCenter').modal('hide'); // Fecha o modal após o postback
        }

        function formatarDocumento(input) {
            let documento = input.value.replace(/\D/g, '');

            if (documento.length <= 11) {

                input.value = formatarCPF(documento);
            } else if (documento.length <= 14) {

                input.value = formatarCNPJ(documento);
            }
        }


        function formatarCPF(cpf) {
            cpf = cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
            return cpf;
        }

        function imprimirModalConteudo(idAgenda) {
            var modalConteudo = document.getElementById('modalVisualizarAgenda' + idAgenda)
            var janelaImpressao = window.open('', '_blank');
            janelaImpressao.document.write('<html><head><title>Imprimir Detalhes da Agenda</title>');
            janelaImpressao.document.write('<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />');
            janelaImpressao.document.write('</head><body>');
            janelaImpressao.document.write(modalConteudo.innerHTML);
            janelaImpressao.document.write('</body></html>');
            janelaImpressao.document.close();
            janelaImpressao.print();
        }

        function formatarCNPJ(cnpj) {
            cnpj = cnpj.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, '$1.$2.$3/$4-$5');
            return cnpj;
        }

        function formatarValor(input) {
            let valor = input.value.replace(/\D/g, '');
            valor = valor.replace(/^0+/, '');

            if (valor.length > 2) {
                valor = valor.slice(0, valor.length - 2) + ',' + valor.slice(valor.length - 2);
            }


            if (valor.length > 6) {
                valor = valor.slice(0, valor.length - 6) + '.' + valor.slice(valor.length - 6);
            }
            if (valor.length > 9) {
                valor = valor.slice(0, valor.length - 9) + '.' + valor.slice(valor.length - 9);
            }


            input.value = valor ? 'R$ ' + valor : '';
        }

        window.onload = function () {
            verificarAprovador();
        };

        document.addEventListener("DOMContentLoaded", function () {
            var table = document.getElementById("tabelaDados");
            var rows = table.getElementsByTagName("tr");
            var rowsPerPage = 8;
            var currentPage = 1;

            function showPage(page) {
                var start = (page - 1) * rowsPerPage;
                var end = start + rowsPerPage;

                for (var i = 0; i < rows.length; i++) {
                    rows[i].style.display = (i >= start && i < end) ? "table-row" : "none";
                }
            }

            function createPagination() {
                var pagination = document.getElementById("paginacao");
                var numPages = Math.ceil(rows.length / rowsPerPage);

                pagination.innerHTML = "";

                var prevLi = document.createElement("li");
                prevLi.className = "page-item";
                var prevLink = document.createElement("a");
                prevLink.className = "page-link";
                prevLink.href = "#";
                prevLink.innerText = "«";
                prevLink.onclick = function () {
                    if (currentPage > 1) {
                        currentPage--;
                        showPage(currentPage);
                        updateActivePage();
                    }
                    return false;
                };
                prevLi.appendChild(prevLink);
                pagination.appendChild(prevLi);

                for (var i = 1; i <= numPages; i++) {
                    var li = document.createElement("li");
                    li.className = "page-item";
                    var link = document.createElement("a");
                    link.className = "page-link";
                    link.href = "#";
                    link.innerText = i;
                    link.onclick = (function (page) {
                        return function () {
                            currentPage = page;
                            showPage(page);
                            updateActivePage();
                            return false;
                        };
                    })(i);
                    li.appendChild(link);
                    pagination.appendChild(li);
                }

                var nextLi = document.createElement("li");
                nextLi.className = "page-item";
                var nextLink = document.createElement("a");
                nextLink.className = "page-link";
                nextLink.href = "#";
                nextLink.innerText = "»";
                nextLink.onclick = function () {
                    if (currentPage < numPages) {
                        currentPage++;
                        showPage(currentPage);
                        updateActivePage();
                    }
                    return false;
                };
                nextLi.appendChild(nextLink);
                pagination.appendChild(nextLi);

                updateActivePage();
            }

            function updateActivePage() {
                var pages = document.querySelectorAll("#paginacao .page-item");
                pages.forEach((page, index) => {
                    if (index === currentPage) {
                        page.classList.add("active");
                    } else {
                        page.classList.remove("active");
                    }
                });
            }

            showPage(currentPage);
            createPagination();
        });




    </script>

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

