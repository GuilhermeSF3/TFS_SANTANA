<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="ListaDeAgendas.aspx.vb" Inherits="Santana.Paginas.TI.ListaDeAgendas" Title="Lista de Agendas" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Lista de Agendas</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div style="padding-left: 60px; padding-right: 60px;">
                <div style="width: 100%; display: flex; flex-direction: column; gap: 10px; margin-bottom: 10px">
                    <div class="" style="padding-top: 35px;">
                        <div>
                            <div>
                                <p>Nova Agenda <a href="../TI/Agendador.aspx"><i class="bi bi-plus-circle-fill"></i></a></p>
                            </div>
                        </div>
                        <table id="tblAgendas" class="table caption-top minha-tabela" style="width: 100%; table-layout: fixed;">
                            <thead style="background-color: lightgray">
                                <tr>
                                      <th scope="col" style="text-align: center; vertical-align: middle;">SELECIONAR AGENDA
      </th>
                                    <th scope="col" style="text-align: center; vertical-align: middle;">NUMERO DA
                                         <br />
                                        AGENDA</th>
                                    <th scope="col" style="text-align: center; vertical-align: middle;">DATA DA
                                    <br>
                                        AGENDA</th>
                                    <th scope="col" style="text-align: center; vertical-align: middle;">DIGITADOR</th>
                                    <th scope="col" style="text-align: center; vertical-align: middle;">DEPARTAMENTO</th>
                                    <th scope="col" style="text-align: center; vertical-align: middle;">DESCRICAO</th>
                                    <th scope="col" style="text-align: center; vertical-align: middle;">FAVORECIDO</th>
                                    <th scope="col" style="text-align: center; vertical-align: middle;">DATA DE
                                    <br />
                                        PAGAMENTO </th>
                                    <th scope="col" style="text-align: center; vertical-align: middle;">APROVADOR</th>
                                    <th scope="col" style="text-align: center; vertical-align: middle;">STATUS</th>
                                    <th scope="col" style="text-align: center; vertical-align: middle; width: 200px;">AÇÃO</th>
                            </thead>

                            <tbody id="tabelaDados">
                                <asp:Literal ID="litTabela" runat="server"></asp:Literal>
                            </tbody>
                        </table>
                        <div style="display: flex; justify-content: space-between;">
                            <div>
                                <nav aria-label="Página de navegação">
                                    <ul class="pagination justify-content-center" id="paginacao">
                                        <li class="page-item" id="paginaAnterior">
                                            <a class="page-link" href="#" aria-label="Anterior">
                                                <span aria-hidden="true"><i class="bi bi-chevron-left"></i></span>
                                            </a>
                                        </li>
                                    </ul>
                                </nav>

                            </div>
                            <div>
                     
                              <button id="btnGerenciamento" runat="server" style="margin-top: 20px;" class="btn btn-light" type="button" data-toggle="modal" data-target="#modalGerenciarAgenda">Gerenciamento</button>
                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-default" Text="Menu Principal" Style="margin-top: 20px;" OnClick="btnMenu_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="modalVisualizarAgenda" tabindex="-1" aria-labelledby="modalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="modalLabel">Detalhes da Agenda</h5>

                            </div>
                            <div class="modal-body">
                                <p>
                                    <strong>Histórico:</strong>
                                    <asp:Label ID="lblHistorico" runat="server" Text=""></asp:Label>
                                </p>
                                <p>
                                    <strong>Data de Pagamento:</strong>
                                    <asp:Label ID="lblDataPagamento" runat="server" Text=""></asp:Label>
                                </p>
                                <p>
                                    <strong>Descrição:</strong>
                                    <asp:Label ID="lblDescricao" runat="server" Text=""></asp:Label>
                                </p>
                                <p>
                                    <strong>Valor Bruto:</strong>
                                    <asp:Label ID="lblValorBruto" runat="server" Text=""></asp:Label>
                                </p>
                                <p>
                                    <strong>Valor Líquido:</strong>
                                    <asp:Label ID="lblValorLiquido" runat="server" Text=""></asp:Label>
                                </p>
                                <p>
                                    <strong>Favorecido:</strong>
                                    <asp:Label ID="lblFavorecido" runat="server" Text=""></asp:Label>
                                </p>
                                <p>
                                    <strong>CPF/CNPJ:</strong>
                                    <asp:Label ID="lblCpfCnpj" runat="server" Text=""></asp:Label>
                                </p>
                                <p>
                                    <strong>Forma de Pagamento:</strong>
                                    <asp:Label ID="lblFormaPagamento" runat="server" Text=""></asp:Label>
                                </p>
                                <p>
                                    <strong>Banco:</strong>
                                    <asp:Label ID="lblBanco" runat="server" Text=""></asp:Label>
                                </p>
                                <p>
                                    <strong>Agência:</strong>
                                    <asp:Label ID="lblAgencia" runat="server" Text=""></asp:Label>
                                </p>
                                <p>
                                    <strong>Conta Corrente:</strong>
                                    <asp:Label ID="lblContaCorrente" runat="server" Text=""></asp:Label>
                                </p>
                                <p>
                                    <strong>Digitador:</strong>
                                    <asp:Label ID="lblDigitador" runat="server" Text=""></asp:Label>
                                </p>
                                <p>
                                    <strong>Departamento:</strong>
                                    <asp:Label ID="lblDepartamento" runat="server" Text=""></asp:Label>
                                </p>
                                <p>
                                    <strong>Data da Agenda:</strong>
                                    <asp:Label ID="lblDataAgenda" runat="server" Text=""></asp:Label>
                                </p>
                                <p>
                                    <strong>Aprovador:</strong>
                                    <asp:Label ID="lblAprovador" runat="server" Text=""></asp:Label>
                                </p>
                            </div>

                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                            </div>
                        </div>
                    </div>
                </div>
<div class="modal fade" id="modalEditarAgenda" tabindex="-1"  aria-labelledby="modalLabelEditar" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="modalLabelEditar">Editar Agenda</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <div class="form-group">
                    <label for="txtNumeroAgenda">Número da Agenda</label>
                    <asp:TextBox ID="txtNumeroAgenda" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
               <asp:Button ID="btnBuscarAgenda" runat="server" CssClass="btn btn-primary" Text="Buscar" 
           OnClick="BuscarAgenda_Click" />

                <hr />
                <div class="form-group">
                    <label for="txtEditarHistorico">Histórico</label>
                    <asp:TextBox ID="txtEditarHistorico" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEditarDataPagamento">Data de Pagamento</label>
                    <asp:TextBox ID="txtEditarDataPagamento" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEditarDescricao">Descrição</label>
                    <asp:TextBox ID="txtEditarDescricao" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEditarValorBruto">Valor Bruto</label>
                    <asp:TextBox ID="txtEditarValorBruto" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEditarValorLiquido">Valor Líquido</label>
                    <asp:TextBox ID="txtEditarValorLiquido" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEditarFavorecido">Favorecido</label>
                    <asp:TextBox ID="txtEditarFavorecido" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEditarCpfCnpj">CPF/CNPJ</label>
                    <asp:TextBox ID="txtEditarCpfCnpj" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEditarFormaPagamento">Forma de Pagamento</label>
                    <asp:TextBox ID="txtEditarFormaPagamento" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEditarBanco">Banco</label>
                    <asp:TextBox ID="txtEditarBanco" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEditarAgencia">Agência</label>
                    <asp:TextBox ID="txtEditarAgencia" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEditarContaCorrente">Conta Corrente</label>
                    <asp:TextBox ID="txtEditarContaCorrente" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnSalvarEdicao" runat="server" CssClass="btn btn-success" Text="Salvar" />
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
            </div>
        </div>
    </div>
</div>

           <div class="modal fade" id="modalGerenciarAgenda" tabindex="-1" aria-labelledby="modalLabelGerenciar" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="modalLabelGerenciar">Gerenciar Agenda</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <p>Informe o número da agenda que deseja gerenciar.</p>
                <div style="margin-bottom: 40px; margin-top: 25px; width: 200px;">
                    <label for="txtId" class="form-label">Número Agenda</label>
                    <asp:TextBox ID="txtId" class="col-md-2" runat="server" CssClass="form-control col-md-2"></asp:TextBox>
                </div>
                <form id="formGerenciarAgenda" method="post" enctype="multipart/form-data">
                    <input type="hidden" name="agendaId" id="agendaId" />
                    <div class="form-group">
                        <label for="ddlstatus">Status</label>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Text="DIGITADO" Value="DIGITADO" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="ENVIADO" Value="ENVIADO"></asp:ListItem>
                            <asp:ListItem Text="RECUSADO" Value="RECUSADO"></asp:ListItem>
                            <asp:ListItem Text="PAGO" Value="PAGO"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
      <label for="fileUpload">Adicionar Arquivo</label>
      <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control-file" AllowMultiple="true" />
  </div>
                    <asp:Button ID="SalvarArquivo" runat="server" CssClass="btn btn-primary" Text="Salvar Arquivo" OnClick="AdicionarArquivoAgenda" />
                </form>
                           
            </div>
   
            <div class="modal-footer">
                
               
                <asp:Button ID="StatusAgenda" runat="server" CssClass="btn btn-success" Text="Salvar Alteração" OnClick="AtualizarStatusAgendas" />
                <asp:Button ID="ExcluirAgendas" runat="server" CssClass="btn btn-danger" Text="Excluir Agenda" OnClick="ExcluirAgenda" />
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
            </div>
        </div>
    </div>
</div>

                 
            
                <button type="button" style="display: none" class="btn btn-light" data-toggle="modal" data-target="#modalVisualizarAgenda"></button>
                <button type="button" style="display: none" class="btn btn-light" data-toggle="modal" data-target="#modalGerenciarAgenda"></button>
                  <asp:Button ID="btnReenviarEmails" runat="server" Text="Reenviar E-mails" CssClass="btn btn-primary" OnClick="btnEnviarEmail_Click" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ExcluirAgendas" />
            <asp:PostBackTrigger ControlID="StatusAgenda" />
            <asp:PostBackTrigger ControlID="SalvarArquivo" />
             <asp:PostBackTrigger ControlID="btnReenviarEmails" />
            <asp:PostBackTrigger ControlID="btnBuscarAgenda" />


        </Triggers>

    </asp:UpdatePanel>

    <script type="text/javascript" src="https://uxsolutions.github.io/bootstrap-datepicker/boot‌​strap-datepicker/js/‌​locales/bootstrap-da‌​tepicker.pt-BR.min.j‌​s"></script>


    <style>
        .minha-tabela {
            width: 80%;
            margin: auto;
            table-layout: fixed;
            border-collapse: collapse;
        }

            .minha-tabela th, .minha-tabela td {
                padding: 5px;
                font-size: 12px;
                text-align: center;
                word-wrap: break-word;
            }

            .minha-tabela th {
                background-color: #f4f4f4;
            }

        .pagination {
            border: none;
            color: black;
            background-color: transparent;
        }

            .pagination .page-item {
                border: none;
                color: black;
                background-color: transparent;
            }

            .pagination .page-link {
                border: none;
                color: black;
                background-color: transparent;
            }

        .status-digitado {
            background-color: #f7efad;
            padding: 4px;
            text-align: center;
            height: 20px;
            width: 200px;
            border-radius: 5px;
        }

        .status-enviado {
            background-color: #bee2f7;
            padding: 4px;
            text-align: center;
            height: 20px;
            width: 200px;
            border-radius: 5px;
        }


        .status-recusado {
            background-color: #f7bebe;
            padding: 4px;
            text-align: center;
            height: 20px;
            width: 200px;
            border-radius: 5px;
        }


        .status-pago {
            background-color: #c5f7be;
            padding: 4px;
            text-align: center;
            height: 20px;
            width: 200px;
            border-radius: 5px;
        }
    </style>


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
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>



</asp:Content>

