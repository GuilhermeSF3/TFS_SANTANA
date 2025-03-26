<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="Agendador.aspx.vb" Inherits="Santana.Paginas.TI.Agendador" Title="Agendador de Agendas" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Agendador de Agendas</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <!--    <nav class="navbar navbar-default" role="navigation">
                <div style="display: flex; justify-content: end; margin-right: 20px;" class="ml-auto">
                    <asp:Button ID="btnMenu" runat="server" Text="Menu Principal" CssClass="btn btn-default navbar-btn" OnClick="btnMenu_Click" />
                </div>
            </nav -->
            <!-- END NAVBAR -->
            <!-- CONTAINER -->

            <div style="padding: 20px; display: flex; flex-direction: column; gap: 20px">
                <div class="container-large">
                    <p style="font-size: 16px; margin-bottom: 30px;">Agendador de Despesas </p>
                    <div class="row">
                        <!-- Formulário (lado esquerdo) -->
                        <div class="col-md-12 mb-4">
                            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
    <ContentTemplate>
                            <div style="margin-bottom: 10px;" class="col-md-2 mb-4">
                                <label for="ddlHistorico" class="form-label">Departamento</label>
                                <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlHistorico_SelectedIndexChanged" ID="DropDownList1" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Selecione" Value="" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="RECURSOS HUMANOS (RH)" Value="RH"></asp:ListItem>
                                    <asp:ListItem Text="FINANCEIRO" Value="Financeiro"></asp:ListItem>
                                    <asp:ListItem Text="TECNOLOGIA" Value="TI"></asp:ListItem>
                                    <asp:ListItem Text="JURIDICO" Value="Jurídico"></asp:ListItem>
                                    <asp:ListItem Text="RECUPERAÇÃO" Value="Recuperação"></asp:ListItem>
                                    <asp:ListItem Text="CONTABILIDADE" Value="Contabilidade"></asp:ListItem>
                                    <asp:ListItem Text="FORMALIZAÇÃO" Value="Formalização"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div style="margin-bottom: 10px;" class="col-md-3 mb-3">
                                <label for="ddlHistorico" class="form-label">Histórico</label>
                                <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlHistorico_SelectedIndexChanged" ID="ddlHistorico" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>

          </ContentTemplate>
</asp:UpdatePanel>

                            <div style="margin-bottom: 10px;" class="col-md-4 mb-3">
                                <label for="txtDescricao" class="form-label">Descrição</label>
                                <asp:TextBox ID="txtDescricao" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div style="margin-bottom: 10px;" class="col-md-2 mb-3">
                                <label for="txtFavorecido" class="form-label">Favorecido</label>
                                <asp:TextBox ID="txtFavorecido" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>


                        </div>
                    </div>
                    <div class="row">
                        <!-- Formulário (lado esquerdo) -->
                        <div class="col-md-12 mb-4">

                            <div style="margin-bottom: 10px;" class="col-md-2 mb-3">
                                <label for="txtCpfCnpj" class="form-label">CPF/CNPJ</label>
                                <asp:TextBox ID="txtCpfCnpj" runat="server" CssClass="form-control" onkeyup="formatarDocumento(this)" MaxLength="18"></asp:TextBox>
                            </div>


                            <div style="margin-bottom: 10px;" class="col-md-2 mb-3">
                                <label for="txtValorBruto" class="form-label">Valor Bruto</label>
                                <asp:TextBox ID="txtValorBruto" runat="server" CssClass="form-control" onkeyup="formatarValor(this)" MaxLength="15"></asp:TextBox>
                            </div>


                            <div style="margin-bottom: 10px;" class="col-md-2 mb-3">
                                <label for="txtValorLiquido" class="form-label">Valor Líquido</label>
                                <asp:TextBox ID="txtValorLiquido" runat="server" CssClass="form-control" onkeyup="formatarValor(this)" MaxLength="15"></asp:TextBox>
                            </div>



                            <div style="margin-bottom: 10px;" class="col-md-2 mb-3">
                                <label for="ddlFormaPagamento" class="form-label">Forma de Pagamento</label>
                                <asp:DropDownList ID="ddlFormaPagamento" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Selecione" Value="" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="BOLETO" Value="BOLETO"></asp:ListItem>
                                    <asp:ListItem Text="PIX" Value="PIX"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <!-- Formulário (lado esquerdo) -->
                        <div class="col-md-12 mb-4">

                            <div style="margin-bottom: 10px;" class="col-md-2 mb-3">
                                <label for="txtBanco" class="form-label">Banco</label>
                                <asp:TextBox ID="txtBanco" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div style="margin-bottom: 10px;" class="col-md-2 mb-3">
                                <label for="txtAgencia" class="form-label">Agência</label>
                                <asp:TextBox ID="txtAgencia" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div style="margin-bottom: 10px;" class="col-md-2 mb-3">
                                <label for="txtContaCorrente" class="form-label">Conta Corrente</label>
                                <asp:TextBox ID="txtContaCorrente" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <!-- Formulário (lado esquerdo) -->
                        <div class="col-md-12 mb-4">
                            <div class="col-md-4 mb-3">
                                <label for="FileUpload1" style="color: #363636" class="form-label">Anexo</label>

                                <div class="upload-area">
                                    <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" class="form-control" />

                                </div>
                                <asp:HiddenField ID="hdnArquivosRemovidos" runat="server" />
                                <div id="listaArquivos" class="mt-2"></div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <!-- Formulário (lado esquerdo) -->
                        <div class="col-md-12 mb-4" style="margin-top: 15px;">

                            <div class="col-md-3" style="margin-top: 10px; gap: 10px; display: flex; justify-content: start; margin-bottom: 30px;">
                                <div>
                                    <asp:Button ID="btnSalvarAgenda" runat="server" Text="Salvar Agenda" CssClass="btn btn-success" OnClick="btnSalvarAgendaSelected" />
                                </div>
                                <div>
                                    <asp:Button ID="btnReiniciar" runat="server" Text="Limpar" CssClass="btn btn-danger" OnClick="btnReiniciar_Click" />

                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-6 mb-4" style="margin-top: 15px;">
                            <asp:GridView ID="gvAgendas" runat="server" AutoGenerateColumns="False" Style="margin-bottom: 30px;" CssClass="table table-bordered ">
                                <Columns>
                                    <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                    <asp:BoundField DataField="ValorBruto" HeaderText="Valor Bruto" DataFormatString="{0:C}" />
                                    <asp:BoundField DataField="ValorLiquido" HeaderText="Valor Líquido" DataFormatString="{0:C}" />
                                    <asp:TemplateField HeaderText="Ação">
                                        <ItemTemplate>
                                            <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CssClass="btn btn-danger btn-sm"
                                                CommandArgument='<%# Container.DataItemIndex %>' OnClick="btnExcluirAgenda_Click" />
                                        
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>


                    <div class="row" style="margin-top: 10px;">
                        <div class="col-md-12" style="display: flex; margin-top: 15px;">

                            <div style="margin-bottom: 10px;" class="col-md-3 mb-3">
                                <label for="ddlEmpresa" class="form-label">Empresa</label>
                                <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Selecione" Value="" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="SF3" Value="SF3"></asp:ListItem>
                                    <asp:ListItem Text="SHOPCRED" Value="SHOPCRED"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2" style="margin-bottom: 10px;">
                                <label for="txtDataPagamento" class="form-label">Data de Pagamento</label>
                                <asp:TextBox ID="txtDataPagamento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="col-md-3" style="margin-bottom: 10px;">
                                <label for="ddlAprovador" class="form-label">Aprovador</label>
                                <asp:DropDownList ID="ddlAprovador" runat="server" Style="margin-bottom: 30px;" CssClass="form-control" onchange="verificarAprovador()">
                                    <asp:ListItem Text="Selecione" Value="" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Luiz" Value="luiz@sf3.com.br"></asp:ListItem>
                                    <asp:ListItem Text="Junior" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Marcelo" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Cesar" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Guilherme" Value="menoti@sf3.com.br"></asp:ListItem>
                                </asp:DropDownList>


                            </div>
                            <div class="col-md-2" style="margin-bottom: 16px; display: flex; gap: 10px; align-self: center;">
                                <asp:Button ID="btnEnviarEmail" runat="server" CssClass="btn btn-primary" Text="Enviar Agenda" OnClick="btnEnviarEmail_Click" />
                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-default" Text="Menu Principal" OnClick="btnMenu_Click" />
                            </div>
                        </div>
                    </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnEnviarEmail" />
            <asp:PostBackTrigger ControlID="btnSalvarAgenda" />
            <asp:PostBackTrigger ControlID="DropDownList1" />
            <asp:PostBackTrigger ControlID="ddlHistorico" />

        </Triggers>
    </asp:UpdatePanel>

 
    <!-- END CONTAINER -->
    <style>
        .upload-area {
            border: 2px dashed #000;
            border-radius: 8px;
            width: 100%;
            height: 50px;
            display: flex;
            align-items: center;
            justify-content: center;
            cursor: pointer;
            position: relative;
            box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.1);
        }

            .upload-area input {
                position: absolute;
                width: 100%;
                height: 100%;
                cursor: pointer;
            }

        .table {
            border-radius: 10px;
            overflow: hidden;
        }

            .table th {
                background-color: #152B61; /* Azul escuro */
                color: white;
                text-align: center;
                padding: 12px;
                border-bottom: 1px solid gray;
                box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
            }

            .table td {
                text-align: center;
                vertical-align: middle;
                padding: 10px;
                margin-bottom: 1px;
                border-bottom: 1px solid gray;
            }

            .table tbody tr:hover {
                background-color: #f5f5f5;
                transition: background 0.3s ease-in-out;
            }

        .btn-danger {
            background-color: #d9534f;
            border-color: #d43f3a;
            padding: 6px 12px;
            border-radius: 6px;
        }

            .btn-danger:hover {
                background-color: #c9302c;
                border-color: #ac2925;
            }


        .upload-icon {
            display: flex;
            flex-direction: column;
            align-items: center;
            color: #7c7c7c;
        }

            .upload-icon i {
                font-size: 40px;
            }
    </style>
    <script>


            console.log("Scripts foram recarregados!");
            let arquivos = [];
            let removidos = [];
            let input = document.getElementById('<%= FileUpload1.ClientID %>');
            let hdnRemovidos = document.getElementById('<%= hdnArquivosRemovidos.ClientID %>');
 let lista = document.getElementById("listaArquivos");


 input.addEventListener('change', function () {
     mostrarArquivos(input);
 });

 //document.forms[0].addEventListener('submit', function () {
 //    atualizarInput(); // Garante que todos os arquivos estão no FileUpload antes do envio
 //});
 function mostrarArquivos(fileInput) {
     let novosArquivos = Array.from(fileInput.files);

     novosArquivos.forEach(novoArquivo => {
         if (!arquivos.some(arquivo => arquivo.name === novoArquivo.name)) {
             arquivos.push(novoArquivo);
         }
     });

     atualizarInput();
     atualizarLista();
            }

            Sys.Application.add_load(function () {
                iniciarScripts();
            });


 function atualizarLista() {
     lista.innerHTML = "";

     arquivos.forEach((file, index) => {
         let fileURL = URL.createObjectURL(file);
         let fileType = file.type;
         let preview = "";

         if (fileType === "application/pdf") {
             preview = `<a href="${fileURL}" target="_blank" class="btn btn-light btn-sm text-primary" title="Visualizar PDF">
                 <i class="bi bi-search"></i>
               </a>`;
         } else if (fileType.startsWith("image/")) {
             preview = `<img src="${fileURL}" style="width: 50px; height: 50px; border-radius: 5px; object-fit: cover; margin-right: 10px;">`;
         } else {
             preview = `<i class="bi bi-file-earmark-text text-primary" style="font-size: 24px; margin-right: 10px;"></i>`;
         }

         let div = document.createElement("div");
         div.className = "d-flex align-items-center mb-2";
         div.innerHTML = `
     <div style="border: 1px solid #152B61; margin-top:15px; display: flex; align-items: center; justify-content: space-between; border-radius: 10px; padding: 10px; width: 100%;">
         ${preview}
         <span style="flex: 1; margin-left: 10px;">${file.name}</span>
         <button type="button" class="btn btn-light btn-sm text-danger" onclick="removerArquivo(${index})" title="Remover">
             <i class="bi bi-trash"></i>
         </button>
     </div>
 `;
         lista.appendChild(div);
     });
 }
 function removerArquivo(index) {
     arquivos.splice(index, 1);
     atualizarInput();
     atualizarLista();
 }

 function atualizarInput() {
     const dataTransfer = new DataTransfer();
     arquivos.forEach(arquivo => dataTransfer.items.add(arquivo));
     input.files = dataTransfer.files;
 }


 Sys.Application.add_load(function () {
     let input = document.getElementById('<%= FileUpload1.ClientID %>');
     input.addEventListener('change', function () {
         mostrarArquivos(input);
     });
 });
    
       

        function removerArquivo(index) {

            removidos.push(arquivos[index].name);

            arquivos.splice(index, 1);

            hdnRemovidos.value = removidos.join(",");

            atualizarLista();
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

        document.getElementById('<%= ddlAprovador.ClientID %>').addEventListener('change', function () {
            verificarAprovador();  // A validação acontece apenas quando o valor do aprovador mudar
        });

        function verificarAprovador() {
            var aprovador = document.getElementById('<%= ddlAprovador.ClientID %>').value;
            var btnEnviarEmail = document.getElementById('<%= btnEnviarEmail.ClientID %>');


            if (aprovador) {
                btnEnviarEmail.disabled = false;
            } else {
                btnEnviarEmail.disabled = true;
            }
        }




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

        $(document).ready(function () {
            $('.selectpicker').selectpicker();
        });
        $(document).ready(function () {
            $('.datepicker').datepicker({
                format: 'dd/mm/yyyy',
                language: 'pt-BR',
                autoclose: true
            });
        });




    </script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/locales/bootstrap-datepicker.pt-BR.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/locales/bootstrap-datepicker.pt-BR.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>


        
    

</asp:Content>





