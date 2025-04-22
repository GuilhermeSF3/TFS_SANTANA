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

            <div style="padding-left: 60px; padding-right: 60px">
                <div style="width: 100%; display: flex; flex-direction: column; gap: 10px; margin-bottom: 10px">
                    <div style="display: flex; justify-content: space-between;">
                        <div style="display: flex; align-items: start; justify-content: start;">


                            <asp:UpdatePanel ID="updFiltros" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div style="display: flex; align-items: start;">

                                        <asp:DropDownList AutoPostBack="true" ID="ddlFiltros" runat="server"
                                            CssClass="form-control" OnSelectedIndexChanged="ddlFiltros_SelectedIndexChanged">
                                            <asp:ListItem Text="Selecione" Value="" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Data de agendamento" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="Data de pagamento" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Departamento" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Digitador" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Empresa" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="Favorecido" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Status" Value="4"></asp:ListItem>
                                        </asp:DropDownList>

                                        <div>
                                            <asp:TextBox Style="margin-left: 10px; width: 440px;" ID="txtFiltro" runat="server"
                                                CssClass="form-control datepicker"></asp:TextBox>
                                        </div>


                                        <div id="campoDataPagamento" runat="server" style="margin-left: 10px; display: none;">
                                            <asp:TextBox ID="txtDataDe" runat="server" CssClass="form-control"
                                                onkeyup="formatarData(this);" Placeholder="Data de"
                                                Style="width: 210px; display: inline-block; margin-right: 10px;" />

                                            <asp:TextBox ID="txtDataAte" runat="server" CssClass="form-control"
                                                onkeyup="formatarData(this);" Placeholder="Data até"
                                                Style="width: 210px; display: inline-block;" />
                                        </div>


                                        <div style="margin-left: 10px;">
                                            <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary"
                                                Style="background-color: #152B61; border-color: #152B61; color: white; width: 116px;"
                                                OnClick="Pesquisar_Relatorio" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>

                        <!-- Botão Exportar Excel -->
                        <div>
                            <asp:ImageButton ID="btnExcel" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/excel2424.png" OnClick="Exportar_Excel"></asp:ImageButton>
                        </div>
                    </div>


                    <table class="table caption-top minha-tabela" style="width: 100%; table-layout: fixed;">
                        <thead>
                            <tr style="background-color: lightgray">
                                <th scope="col" style="text-align: center; vertical-align: middle; width: 115px">DATA DE <br>AGENDAMENTO</th>
                                <th scope="col" style="text-align: center; vertical-align: middle; width: 90px">EMPRESA</th>
                                <th scope="col" style="text-align: center; vertical-align: middle; width: 120px;">DEPARTAMENTO</th>
                                <th scope="col" style="text-align: center; vertical-align: middle;">DIGITADOR</th>
                                <th scope="col" style="text-align: center; vertical-align: middle;">FAVORECIDO</th>
                                <th scope="col" style="text-align: center; vertical-align: middle; width:125px">CPF_CNPJ</th>
                                <th scope="col" style="text-align: center; vertical-align: middle;">HISTÓRICO</th>
                                <th scope="col" style="text-align: center; vertical-align: middle; width: 95px">VALOR <br>BRUTO</th>
                                <th scope="col" style="text-align: center; vertical-align: middle; width: 95px">VALOR <br>LIQUIDO</th>
                                <th scope="col" style="text-align: center; vertical-align: middle; width: 115px">DATA DE <br>PAGAMENTO</th>
                                <th scope="col" style="text-align: center; vertical-align: middle; width: 105px">FORMA DE<br>PAGAMENTO </th>
                                <th scope="col" style="text-align: center; vertical-align: middle; width: 80px;">BANCO</th>
                                <th scope="col" style="text-align: center; vertical-align: middle; width: 80px;">AGENCIA</th>
                                <th scope="col" style="text-align: center; vertical-align: middle; width: 80px;">CONTA</th>
                                <th scope="col" style="text-align: center; vertical-align: middle; width: 80px;">STATUS</th>
                            </tr>
                        </thead>
                        <tbody id="tabelaDados">
                            <asp:Literal ID="litTabela" runat="server"></asp:Literal>
                        </tbody>
                    </table>
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

                </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
            <asp:PostBackTrigger ControlID="btnFiltrar" />
            <asp:PostBackTrigger ControlID="ddlFiltros" />
        </Triggers>
    </asp:UpdatePanel>



    <script type="text/javascript" src="https://uxsolutions.github.io/bootstrap-datepicker/boot‌strap-datepicker/js/‌locales/bootstrap-da‌tepicker.pt-BR.min.j‌s"></script>
    <style>
        .desabilitado {
            display: none;
        }

        .habilitado {
            display: flex;
        }

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
    </style>

    <script type="text/javascript">

        function formatarData(input) {
            let valor = input.value.replace(/\D/g, '');
            if (valor.length > 2 && valor.length <= 4)
                valor = valor.replace(/(\d{2})(\d+)/, '$1/$2');
            else if (valor.length > 4)
                valor = valor.replace(/(\d{2})(\d{2})(\d+)/, '$1/$2/$3');
            input.value = valor;
        }

        function mostrarCampoData(select) {
            var campoDataPagamento = document.getElementById('campoDataPagamento');
            if (select.value === "5" || select.value === "6") { // Valor da opção "Data de pagamento"
                campoDataPagamento.style.display = "block";
            } else {
                campoDataPagamento.style.display = "none";
            }
        }


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



        document.addEventListener("DOMContentLoaded", function () {
            var table = document.getElementById("tabelaDados");
            var rows = table.getElementsByTagName("tr");
            var rowsPerPage = 10;
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





