<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="CadastroDeContaComissao.aspx.vb" Inherits="Santana.Paginas.Cadastro.CadastroDeContaComissao" Title="Cadastro de Conta" EnableEventValidation="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Cadastro de Conta</title>
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
                                    <asp:Button ID="btnMenu" runat="server" Text="Menu Principal" CssClass="btn btn-default navbar-btn" OnClick="btnMenu_Click"></asp:Button>
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
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="dvConsultas" style="height: 600px; width: 100%; overflow: auto;">

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="DSCPFCNPJFAVORECIDO"
                    ShowFooter="False" ShowHeader="True" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound"
                    OnRowDeleting="GridView1_RowDeleting" OnRowCreated="GridView1_RowCreated"
                    OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCommand="GridView1_RowCommand"
                    CellPadding="0" ForeColor="#333333" CssClass="table table-striped table-bordered table-condensed">

                    <Columns>
                        <asp:TemplateField HeaderText="Descrição Rápida">
                            <HeaderTemplate >
                                <asp:Label ID="lblDescRapida" runat="server" Text="Descrição Rápida"></asp:Label>
                                <asp:TextBox ID="txtDescRapida" runat="server" Width="150px" MaxLength="150"></asp:TextBox>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="lblDescRapida" runat="server" Text='<%# Eval("DSRAPIDA")%>' Width="100px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDescRapida" runat="server" Text='<%# Eval("DSRAPIDA")%>' Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Conta bancária">                            
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Conta bancária"></asp:Label>
                                <asp:TextBox ID="txtContaBancaria" runat="server" Width="150px" MaxLength="20"></asp:TextBox>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("NRCONTABANCARIA")%>' ID="lblContaBancaria" Width="150px" MaxLength="20"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("NRCONTABANCARIA")%>' ID="lblContaBancaria" Width="150px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Forma de pagamento">                            
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Forma de pagamento"></asp:Label>
                                <asp:TextBox ID="txtFormaPagamento" runat="server" Width="150px" MaxLength="10"></asp:TextBox>
                            </HeaderTemplate>                            
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("NRFORMAPAGAMENTO")%>' ID="lblFormaPagamento" Width="150px" MaxLength="100"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("NRFORMAPAGAMENTO")%>' ID="lblFormaPagamento" Width="150px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Centro de Custo">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Centro de Custo"></asp:Label>
                                <asp:TextBox ID="txtCentroCusto" runat="server" Width="150px" MaxLength="20"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("CDCENTROCUSTO")%>' ID="lblCentroCusto" Width="100px" MaxLength="20"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("CDCENTROCUSTO")%>' ID="Label5" Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Código do cliente">                            
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Código do cliente"></asp:Label>
                                <asp:TextBox ID="txtCodCliente" runat="server" Width="120px" MaxLength="20"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("CDCLIENTE")%>' ID="lblCodCliente" Width="120px" MaxLength="20"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("CDCLIENTE")%>' ID="lblCodCliente" Width="120px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Débito/Crédito">                           
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Débito/Crédito"></asp:Label>
                                <asp:TextBox ID="txtDebitoCredito" runat="server" Width="100px" MaxLength="50"></asp:TextBox>
                            </HeaderTemplate>                              
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("DSDEBITOCREDITO")%>' ID="lblDebitoCredito" Width="100px" MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("DSDEBITOCREDITO")%>' ID="lblDebitoCredito" Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                                              

                        <asp:TemplateField HeaderText="Código do Banco">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Código do Banco"></asp:Label>
                                <asp:TextBox ID="txtCodBanco" runat="server" Width="120px" MaxLength="20"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("CDBANCO")%>' ID="lblCodBanco" Width="100px" MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("CDBANCO")%>' ID="lblCodBanco" Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Agência Destino">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Agência Destino"></asp:Label>
                                <asp:TextBox ID="txtAgenciaDestino" runat="server" Width="110px" MaxLength="10"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("NRAGENCIADESTINO")%>' ID="lblAgenciaDestino" Width="100px" MaxLength="10"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("NRAGENCIADESTINO")%>' ID="lblAgenciaDestino" Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dígito Conta Destino">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Dígito Conta Destino"></asp:Label>
                                <asp:TextBox ID="txtDigitoContaDestino" runat="server" Width="150px" MaxLength="5"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("NRDIGITOCONTADESTINO")%>' ID="lblDigitoContaDestino" Width="100px" MaxLength="5"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("NRDIGITOCONTADESTINO")%>' ID="lblDigitoContaDestino" Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Conta Destino">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Conta Destino"></asp:Label>
                                <asp:TextBox ID="txtContaDestino" runat="server" Width="150px" MaxLength="10"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("NRCONTADESTINO")%>' ID="lblContaDestino" Width="100px" MaxLength="10"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("NRCONTADESTINO")%>' ID="lblContaDestino" Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nome do Favorecido">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Nome do Favorecido">Nome do Favorecido <span style="color:#ff0000">*</span></asp:Label>
                                <asp:DropDownList ID="ddlNomeFavorecido" runat="server" Width="250px">
								</asp:DropDownList>    
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("NMFAVORECIDO")%>' ID="lblNomeFavorecido" Width="200px"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("NMFAVORECIDO")%>' ID="lblNomeFavorecido" Width="200px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="CPF/CNPJ Favorecido">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="CPF/CNPJ Favorecido">CPF/CNPJ Favorecido <span style="color:#ff0000">*</span></asp:Label>
                                <asp:TextBox ID="txtCpfCnpjFavorecido" runat="server" Width="160px" MaxLength="20"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("DSCPFCNPJFAVORECIDO")%>' ID="lblCpfCnpjFavorecido" Width="150px" MaxLength="20"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("DSCPFCNPJFAVORECIDO")%>' ID="lblCpfCnpjFavorecido" Width="150px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Modalidade">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Modalidade"></asp:Label>
                                <asp:TextBox ID="txtModalidade" runat="server" Width="150px" MaxLength="10"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("NRMODALIDADE")%>' ID="lblModalidade" Width="150px" MaxLength="10"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("NRMODALIDADE")%>' ID="lblModalidade" Width="150px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Digito da Agência">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Digito da Agência"></asp:Label>
                                <asp:TextBox ID="txtDigitoAgencia" runat="server" Width="150px" MaxLength="10"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("NRDIGITOAGENCIA")%>' ID="lblDigitoAgencia" Width="150px" MaxLength="10"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("NRDIGITOAGENCIA")%>' ID="lblDigitoAgencia" Width="150px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                        

                        <asp:TemplateField HeaderText="Nome do Banco">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Nome do Banco"></asp:Label>
                                <asp:TextBox ID="txtNomeBanco" runat="server" Width="150px" MaxLength="20"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("NMBANCO")%>' ID="lblNomeBanco" Width="150px" MaxLength="20"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("NMBANCO")%>' ID="lblNomeBanco" Width="150px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tipo da Conta">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Tipo da Conta"></asp:Label>
                                <asp:TextBox ID="txtTipoConta" runat="server" Width="100px" MaxLength="20"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("TPCONTA")%>' ID="lblTipoConta" Width="100px" MaxLength="20"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("TPCONTA")%>' ID="lblTipoConta" Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Finalidade">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Finalidade"></asp:Label>
                                <asp:TextBox ID="txtFinalidade" runat="server" Width="100px" MaxLength="20"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("DSFINALIDADE")%>' ID="lblFinalidade" Width="100px" MaxLength="20"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("DSFINALIDADE")%>' ID="lblFinalidade" Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Finalidade DOC">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Finalidade DOC"></asp:Label>
                                <asp:TextBox ID="txtFinalidadeDoc" runat="server" Width="110px" MaxLength="20"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("DSFINALIDADEDOC")%>' ID="lblFinalidadeDoc" Width="110px" MaxLength="20"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("DSFINALIDADEDOC")%>' ID="lblFinalidadeDoc" Width="110px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Finalidade TED">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Finalidade Ted"></asp:Label>
                                <asp:TextBox ID="txtFinalidadeTed" runat="server" Width="100px" MaxLength="20"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("DSFINALIDADETED")%>' ID="lblFinalidadeTed" Width="100px" MaxLength="20"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("DSFINALIDADETED")%>' ID="lblFinalidadeTed" Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                                                                      

                        <asp:TemplateField HeaderText="" ShowHeader="False">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Edição "></asp:Label>
                                <asp:LinkButton ID="lnkAddNew" CssClass="btn btn-primary" runat="server" CommandName="AddNew">Incluir</asp:LinkButton>
                            </HeaderTemplate>                             
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Edit" CssClass="btn btn-primary" Text="Editar"></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="True" CssClass="btn btn-info" CommandName="Update" Text="Atualizar "></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CssClass="btn btn-info" CommandName="Cancel" Text="Cancelar"></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Exclusão" ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton onclientclick="return ShowConfirm(this.id);" ID="lnkB" runat="Server" CssClass="btn btn-primary" Text="Excluir" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                    
                </asp:GridView>

            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
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

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            // Place here the first init of the DatePicker
            $(".clDate").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });


            NumericAllow();


        });


        function InitializeRequest(sender, args) {
            StartSpin();

            // make unbind to avoid memory leaks.
            $(".clDate").unbind();

        }

        function EndRequest(sender, args) {

            StopSpin();

            // after update occur on UpdatePanel re-init the DatePicker
            $(".clDate").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });


            NumericAllow();
        }


        var confirmed = false;

        function ShowConfirm(controlID) {
            if (confirmed) { return true; }

            bootbox.confirm("Deseja excluir este registro?", function (result) {
                if (result) {
                    if (controlID != null) {
                        var controlToClick = document.getElementById(controlID);
                        if (controlToClick != null) {
                            confirmed = true;
                            controlToClick.click();
                            confirmed = false;
                        }
                    }
                }
            });

            return false;
        }


        function ShowConfirmReset(controlID) {
            if (confirmed) { return true; }

            bootbox.confirm("Deseja redefinir senha?", function (result) {
                if (result) {
                    if (controlID != null) {
                        var controlToClick = document.getElementById(controlID);
                        if (controlToClick != null) {
                            confirmed = true;
                            controlToClick.click();
                            confirmed = false;
                        }
                    }
                }
            });

            return false;
        }

    </script>
</asp:Content>





