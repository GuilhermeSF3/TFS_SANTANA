<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="Inventario2.aspx.vb" Inherits="Santana.Paginas.TI.Inventario2" Title="Inventario2" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Parcela Pulada </title>
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
                                <div style="margin: 5px">
                                    <p style="font-size:16px;">Gerenciamento de Inventário</p>
                                    <div style="height: 20px">
                                    </div>
                                   <asp:Button ID="Incluir"  runat="server" Text="Adicionar"  CssClass="btn btn-primary navbar-btn" data-toggle="modal" data-target="#exampleModal" />
                                    <asp:Button ID="Button2"  runat="server" Text="Alterar"  CssClass="btn btn-primary navbar-btn" data-toggle="modal" data-target="#exampleModalAlterar" />
                                    <asp:Button ID="ExcluirInv"  runat="server" Text="Excluir"  CssClass="btn btn-danger navbar-btn" data-toggle="modal" data-target="#exampleModalDelete" />
                                    <asp:Button ID="btnMenu" runat="server" Text="Menu Principal." class="btn btn-default navbar-btn" OnClick="btnMenu_Click"></asp:Button>

                                </div>
                            </li>
                        </ul>

                        <ul class="nav navbar-nav navbar-right">
                            <li>
                                <div style="margin: 5px">
                                    <div style="height: 20px">
                                    </div>
                                    <div class="btn-group-sm  ">
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

            <div id="dvConsultas" style="height: 700px; width: 100%; overflow: auto;">
                <asp:Label ID="lblRelatorio" runat="server" Text=""></asp:Label>

                <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                    AllowPaging="True" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowCreated="GridView1_RowCreated" ShowFooter="false"
                    OnDataBound="GridView1_DataBound" Font-Size="9pt">

                    <RowStyle Height="31px" />
                    <Columns>
                        <asp:TemplateField HeaderText="IDENTIFICADOR" SortExpression="ID">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="USUÁRIO" SortExpression="USUARIO">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="OBSERVAÇÃO" SortExpression="OBSERVACAO">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="HOSTNAME" SortExpression="HOSTNAME">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ETIQUETA" SortExpression="ETIQUETA">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CONSERTO OS" SortExpression="CONSERTO_OS">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="DATA ENVIO" SortExpression="DATA_ENVIO">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="DATA DEVOLUÇÃO" SortExpression="DATA_DEVOLUCAO">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="STATUS" SortExpression="STATUS">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="MODELO" SortExpression="MODELO">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PROCESSADOR" SortExpression="PROCESSADOR">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="MEMÓRIA RAM" SortExpression="MEMORIA_RAM">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="MEMÓRIA" SortExpression="MEMORIA">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="FABRICANTE" SortExpression="FABRICANTE">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="TERMO" SortExpression="TERMO">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="DEPARTAMENTO" SortExpression="DEPARTAMENTO">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="TIPO" SortExpression="NOTE_OU_DESK">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CONEXÃO DE REDE" SortExpression="CONEXAO_REDE">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ÚLTIMO DONO" SortExpression="ULTIMO_DONO">
                            <ItemStyle HorizontalAlign="Center" />
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

    <!--MODAL Alterar-->
    <div class="modal fade" id="exampleModalAlterar" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Editar equipamento</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body" style="max-height: 70vh; overflow-y: auto;">

                    <div class="form-group" style="display: flex; flex-direction: column; gap: 10px;">
                        <div>
                            <label for="usuarioInput">Identificador</label>
                            <asp:TextBox ID="inputId" runat="server" CssClass="form-control" placeholder="Digite o Identificador"></asp:TextBox>
                        </div>
                        <div>

                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="buscarAlterarInventory" />

                        </div>
                    </div>

                    <div class="form-group">
                        <label for="usuarioInput">Usuário</label>
                        <asp:TextBox ID="inputUsuario" runat="server" CssClass="form-control" placeholder="Digite o Usuário"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="observacaoInput">Observação</label>
                        <asp:TextBox ID="inputObserv" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Digite a Observação"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="hostnameInput">Hostname</label>
                        <asp:TextBox ID="inputHost" runat="server" CssClass="form-control" placeholder="Digite o Hostname"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="etiquetaInput">Etiqueta</label>
                        <asp:TextBox ID="inputEtiqueta" runat="server" CssClass="form-control" placeholder="Digite a Etiqueta"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="consertoOSInput">Conserto OS</label>
                        <asp:TextBox ID="inputConserto" runat="server" CssClass="form-control" placeholder="Digite o Conserto OS"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="dataEnvioInput">Data Envio</label>
                        <asp:TextBox ID="inputDataEnvio" runat="server" CssClass="form-control" TextMode="Date" placeholder="Digite a Data de Envio"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="dataDevolucaoInput">Data Devolução</label>
                        <asp:TextBox ID="inputDataDevolucao" runat="server" CssClass="form-control" TextMode="Date" placeholder="Digite a Data de Devolução"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="statusInput">Status</label>
                        <asp:TextBox ID="inputStatus" runat="server" CssClass="form-control" placeholder="Digite o Status"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="modeloInput">Modelo</label>
                        <asp:TextBox ID="inputModelo" runat="server" CssClass="form-control" placeholder="Digite o Modelo"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="processadorInput">Processador</label>
                        <asp:TextBox ID="inputProcessador" runat="server" CssClass="form-control" placeholder="Digite o Processador"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="memoriaRAMInput">Memória RAM</label>
                        <asp:TextBox ID="inputMemoriaRam" runat="server" CssClass="form-control" placeholder="Digite a Memória RAM"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="memoriaInput">Memória</label>
                        <asp:TextBox ID="inputMemoria" runat="server" CssClass="form-control" placeholder="Digite a Memória"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="fabricanteInput">Fabricante</label>
                        <asp:TextBox ID="inputFabricante" runat="server" CssClass="form-control" placeholder="Digite o Fabricante"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="termoInput">Termo</label>
                        <asp:TextBox ID="inputTermo" runat="server" CssClass="form-control" placeholder="Digite o Termo"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="departamentoInput">Departamento</label>
                        <asp:TextBox ID="inputDepartamento" runat="server" CssClass="form-control" placeholder="Digite o Departamento"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="noteOuDeskInput">Note ou Desk</label>
                        <asp:TextBox ID="inputNoteOuDesk" runat="server" CssClass="form-control" placeholder="Digite Note ou Desk"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="conexaoRedeInput">Conexão Rede</label>
                        <asp:TextBox ID="inputConexao" runat="server" CssClass="form-control" placeholder="Digite a Conexão Rede"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="ultimoDonoInput">Último Dono</label>
                        <asp:TextBox ID="inputUltimoDono" runat="server" CssClass="form-control" placeholder="Digite o Último Dono"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>

                    <asp:Button ID="btnSalvarAlterada" runat="server" Text="Salvar" OnClick="AlterInventory" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>


    <!-- END MODAL alterar-->



    <!--MODAL INCLUIR-->
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Novo Equipamento</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body" style="max-height: 70vh; overflow-y: auto;">

                    <div class="form-group">
                        <label for="usuarioInput">Usuário</label>
                        <asp:TextBox ID="usuarioInput" runat="server" CssClass="form-control" placeholder="Digite o Usuário"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="observacaoInput">Observação</label>
                        <asp:TextBox ID="observacaoInput" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Digite a Observação"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="hostnameInput">Hostname</label>
                        <asp:TextBox ID="hostnameInput" runat="server" CssClass="form-control" placeholder="Digite o Hostname"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="etiquetaInput">Etiqueta</label>
                        <asp:TextBox ID="etiquetaInput" runat="server" CssClass="form-control" placeholder="Digite a Etiqueta"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="consertoOSInput">Conserto OS</label>
                        <asp:TextBox ID="consertoOSInput" runat="server" CssClass="form-control" placeholder="Digite o Conserto OS"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="dataEnvioInput">Data Envio</label>
                        <asp:TextBox ID="dataEnvioInput" runat="server" CssClass="form-control" TextMode="Date" placeholder="Digite a Data de Envio"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="dataDevolucaoInput">Data Devolução</label>
                        <asp:TextBox ID="dataDevolucaoInput" runat="server" CssClass="form-control" TextMode="Date" placeholder="Digite a Data de Devolução"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="statusInput">Status</label>
                        <asp:TextBox ID="statusInput" runat="server" CssClass="form-control" placeholder="Digite o Status"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="modeloInput">Modelo</label>
                        <asp:TextBox ID="modeloInput" runat="server" CssClass="form-control" placeholder="Digite o Modelo"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="processadorInput">Processador</label>
                        <asp:TextBox ID="processadorInput" runat="server" CssClass="form-control" placeholder="Digite o Processador"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="memoriaRAMInput">Memória RAM</label>
                        <asp:TextBox ID="memoriaRAMInput" runat="server" CssClass="form-control" placeholder="Digite a Memória RAM"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="memoriaInput">Memória</label>
                        <asp:TextBox ID="memoriaInput" runat="server" CssClass="form-control" placeholder="Digite a Memória"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="fabricanteInput">Fabricante</label>
                        <asp:TextBox ID="fabricanteInput" runat="server" CssClass="form-control" placeholder="Digite o Fabricante"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="termoInput">Termo</label>
                        <asp:TextBox ID="termoInput" runat="server" CssClass="form-control" placeholder="Digite o Termo"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="departamentoInput">Departamento</label>
                        <asp:TextBox ID="departamentoInput" runat="server" CssClass="form-control" placeholder="Digite o Departamento"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="noteOuDeskInput">Note ou Desk</label>
                        <asp:TextBox ID="noteOuDeskInput" runat="server" CssClass="form-control" placeholder="Digite Note ou Desk"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="conexaoRedeInput">Conexão Rede</label>
                        <asp:TextBox ID="conexaoRedeInput" runat="server" CssClass="form-control" placeholder="Digite a Conexão Rede"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="ultimoDonoInput">Último Dono</label>
                        <asp:TextBox ID="ultimoDonoInput" runat="server" CssClass="form-control" placeholder="Digite o Último Dono"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="newInventory" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="exampleModalDelete" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Excluir Equipamento</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body" style="max-height: 70vh; overflow-y: auto;">

                    <div class="form-group">
                        <label for="identificadorInput">Indentificador</label>
                        <asp:TextBox ID="identificadorInput" runat="server" CssClass="form-control" placeholder="Digite o ID do equipamento"></asp:TextBox>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                        <asp:Button ID="Button1" runat="server" Text="Excluir" CssClass="btn btn-danger" OnClick="deleteInventory" />
                    </div>

                </div>
            </div>
        </div>
    </div>




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

    <script type="text/javascript">


        function pageLoad() {
            $('#<%=GridView1.ClientID%>').gridviewScroll({
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


</asp:Content>





