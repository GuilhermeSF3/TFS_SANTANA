<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="Agendador.aspx.vb" Inherits="Santana.Paginas.TI.Agendador" Title="Agendador de Agendas" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Agendador de Agendas</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <nav class="navbar navbar-default" role="navigation">
    <div style="display:flex; justify-content:end; margin-right: 20px;" class="ml-auto">
        <asp:Button ID="btnMenu" runat="server" Text="Menu Principal" CssClass="btn btn-default navbar-btn" OnClick="btnMenu_Click" />
    </div>
</nav>
            <!-- END NAVBAR -->
            <!-- CONTAINER -->

            <div style="padding: 20px">
                <div class="container">
                    <p style="font-size: 18px; margin-bottom: 30px; font-weight: bold;">Agendador de Despesas </p>
                    <div class="row">
                        <!-- Formulário (lado esquerdo) -->
                        <div class="col-md-12">
                            <div class="row">

                                <div style="margin-bottom: 10px;" class="col-md-4 mb-3">
                                    <label for="ddlHistorico" class="form-label">Histórico</label>
                                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlHistorico_SelectedIndexChanged" ID="ddlHistorico" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Selecione" Value="" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="PAGAMENTO ONNE TECNOLOGIA" Value="Salario"></asp:ListItem>
                                        <asp:ListItem Text="PAGAMENTO INGRAM" Value="Fornecedor"></asp:ListItem>
                                         <asp:ListItem Text="LOCALWEB" Value="LocalWeb"></asp:ListItem>
                                    </asp:DropDownList>
                                </div> 
                                <div style="margin-bottom: 10px;" class="col-md-2 mb-3">
                                    <label for="txtDataPagamento" class="form-label">Data de Pagamento</label>
                                    <asp:TextBox ID="txtDataPagamento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div style="margin-bottom: 10px;" class="col-md-4 mb-3">
                                    <label for="txtDescricao" class="form-label">Descrição</label>
                                    <asp:TextBox ID="txtDescricao" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div style="margin-bottom: 10px;" class="col-md-2 mb-3">
                                    <label for="txtValorBruto" class="form-label">Valor Bruto</label>
                                    <asp:TextBox ID="txtValorBruto" runat="server" CssClass="form-control"   onkeyup="formatarValor(this)" maxlength="15"   ></asp:TextBox>
                                </div>
                                <div style="margin-bottom: 10px;" class="col-md-2 mb-3">
                                    <label for="txtValorLiquido" class="form-label">Valor Líquido</label>
                                    <asp:TextBox ID="txtValorLiquido" runat="server" CssClass="form-control"  onkeyup="formatarValor(this)" maxlength="15" ></asp:TextBox>
                                </div>

                                <div style="margin-bottom: 10px;" class="col-md-3 mb-3">
                                    <label for="txtFavorecido" class="form-label">Favorecido</label>
                                    <asp:TextBox ID="txtFavorecido" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div style="margin-bottom: 10px;" class="col-md-3 mb-3">
                                    <label for="txtCpfCnpj" class="form-label">CPF/CNPJ</label>
                                    <asp:TextBox ID="txtCpfCnpj" runat="server" CssClass="form-control" onkeyup="formatarDocumento(this)" maxlength="18"></asp:TextBox>
                                </div>

                                <div style="margin-bottom: 10px;" class="col-md-3 mb-3">
                                    <label for="ddlFormaPagamento" class="form-label" >Forma de Pagamento</label>
                                    <asp:DropDownList ID="ddlFormaPagamento"  runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Selecione" Value="" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="BOLETO" Value="BOLETO"></asp:ListItem>
                                        <asp:ListItem Text="PIX" Value="PIX"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div style="margin-bottom: 10px;" class="col-md-3 mb-3">
                                    <label for="txtBanco" class="form-label">Banco</label>
                                    <asp:TextBox ID="txtBanco" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div style="margin-bottom: 10px;" class="col-md-3 mb-3">
                                    <label for="txtAgencia" class="form-label">Agência</label>
                                    <asp:TextBox ID="txtAgencia" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div style="margin-bottom: 10px;" class="col-md-3 mb-3">
                                    <label for="txtContaCorrente" class="form-label">Conta Corrente</label>
                                    <asp:TextBox ID="txtContaCorrente" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div style="margin-bottom: 10px;" class="col-md-3 mb-3">
                                    <label for="ddlAprovador" class="form-label">Aprovador</label>
                                    <asp:DropDownList ID="ddlAprovador" runat="server" CssClass="form-control" onchange="verificarAprovador()">
                                        <asp:ListItem Text="Selecione" Value="" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Luiz" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Junior" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Cesar" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Guilherme" Value="menoti@sf3.com.br"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="col-md-6 mb-3">
                                            <label for="FileUpload1" style="color: #363636" class="form-label">Anexar Arquivos</label>
                                            <asp:FileUpload  ID="FileUpload1" runat="server" AllowMultiple="true" class="form-control" />
                                            <asp:HiddenField ID="hdnArquivosRemovidos" runat="server" />
                                            <div id="listaArquivos" class="mt-2"></div>
                                        </div>
                                        <div style="margin-top: 10px;" class="col-md-6 mt-3">
                                            <asp:Button ID="btnEnviarEmail" runat="server" CssClass="btn btn-primary" Text="Enviar Agenda" OnClick="EnviarEmail" />
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnEnviarEmail" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <!-- END CONTAINER -->
                            </div>
                        </div>
        </ContentTemplate>

    </asp:UpdatePanel>

    <script type="text/javascript" src="https://uxsolutions.github.io/bootstrap-datepicker/boot‌​strap-datepicker/js/‌​locales/bootstrap-da‌​tepicker.pt-BR.min.j‌​s"></script>
    <script>
        let arquivos = [];
        let removidos = [];
        let input = document.getElementById('<%= FileUpload1.ClientID %>');
        let hdnRemovidos = document.getElementById('<%= hdnArquivosRemovidos.ClientID %>');


        input.addEventListener('change', function () {
            mostrarArquivos(input);
        });

        function mostrarArquivos(fileInput) {

            arquivos = arquivos.concat(Array.from(fileInput.files));
            atualizarLista();
        }

        function atualizarLista() {
            let lista = document.getElementById("listaArquivos");
            lista.innerHTML = "";  

            arquivos.forEach((file, index) => {
                let div = document.createElement("div");
                div.className = "d-flex justify-content-between align-items-center mb-2";
                div.innerHTML = `
            <div style="border: 1px solid #152B61; display:flex; margin-top: 10px; margin-bottom: 10px; justify-content: space-between; border-radius: 10px; padding: 5px;">
                <span style="margin: 10px;">${file.name}</span>
                <button type="button" class="btn btn-danger btn-sm" onclick="removerArquivo(${index})">Remover</button>
            </div>
        `;
                lista.appendChild(div);
            });
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





