<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.vb" Inherits="Santana.Paginas.Cadastro.Usuarios" Title="Cadastro de Usuários" EnableEventValidation="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Cadastro de Usuários</title>
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


            <div id="dvConsultas" style="height: 590px; width: 100%; overflow: auto;">


                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Login,Funcao,Ativo"
                    ShowFooter="False" ShowHeader="True" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound"
                    OnRowDeleting="GridView1_RowDeleting" OnRowCreated="GridView1_RowCreated"
                    OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCommand="GridView1_RowCommand"
                    CellPadding="0" ForeColor="#333333" CssClass="table table-striped table-bordered table-condensed">

                    <Columns>
                        <asp:TemplateField HeaderText="Login">
                            <HeaderTemplate >
                                <asp:Label ID="lblLogin" runat="server" Text="Login"></asp:Label>
                                <asp:TextBox ID="txtNewLogin" runat="server" Width="100px" MaxLength="20"></asp:TextBox>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblLogin" runat="server" Text='<%# Eval("Login")%>' Width="100px"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblLogin" runat="server" Text='<%# Eval("Login")%>' Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nome">                            
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Nome"></asp:Label>
                                <asp:TextBox ID="txtNewNome" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("NomeUsuario")%>' ID="txtNome" Width="200px" MaxLength="20"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("NomeUsuario")%>' ID="Label2" Width="200px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Função">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Função"></asp:Label>
                               <asp:DropDownList ID="cmbNewFuncao" runat="server" Width="100px">
                                    <asp:ListItem>VIP</asp:ListItem>
                                    <asp:ListItem>DIR</asp:ListItem>
                                    <asp:ListItem>SUP</asp:ListItem>
                                    <asp:ListItem>GEG</asp:ListItem>
                                    <asp:ListItem>GER</asp:ListItem>
                                    <asp:ListItem>ASS</asp:ListItem>
                                    <asp:ListItem>ADM</asp:ListItem>
                                </asp:DropDownList>                                
                            </HeaderTemplate>                            
                            <EditItemTemplate>
                                <asp:DropDownList ID="cmbFuncao" runat="server" Width="100px">
                                    <asp:ListItem>VIP</asp:ListItem>
                                    <asp:ListItem>DIR</asp:ListItem>
                                    <asp:ListItem>SUP</asp:ListItem>
                                    <asp:ListItem>GEG</asp:ListItem>
                                    <asp:ListItem>GER</asp:ListItem>
                                    <asp:ListItem>ASS</asp:ListItem>
                                    <asp:ListItem>ADM</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("Funcao")%>' ID="Label3" Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Gerencia">                            
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Gerencia"></asp:Label>
                                <asp:TextBox ID="txtNewGerencia" runat="server" Width="100px" MaxLength="7"></asp:TextBox>
                            </HeaderTemplate>                            
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("CodGerente")%>' ID="txtGerencia" Width="100px" MaxLength="7"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("CodGerente")%>' ID="Label4" Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Filial">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Filial"></asp:Label>
                                <asp:TextBox ID="txtNewFilial" runat="server" Width="100px" MaxLength="5"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("CodFilial")%>' ID="txtFilial" Width="100px" MaxLength="5"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("CodFilial")%>' ID="Label5" Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cpf">                            
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Cpf"></asp:Label>
                                <asp:TextBox ID="txtNewCpf" runat="server" Width="150px" MaxLength="11"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("Cpf")%>' ID="txtCpf" Width="150px" MaxLength="11"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("Cpf")%>' ID="Label6" Width="150px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="e-Mail">                           
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="e-Mail"></asp:Label>
                                <asp:TextBox ID="txtNewEMail" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                            </HeaderTemplate>                              
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("EMail")%>' ID="txtEMail" Width="200px" MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("EMail")%>' ID="Label7" Width="200px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Ativo">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Ativo"></asp:Label>
                                <asp:DropDownList ID="cmbNewAtivo" runat="server" Width="100px">
                                    <asp:ListItem Selected="True" Value="1">Sim</asp:ListItem>
                                    <asp:ListItem Value="0">Não</asp:ListItem>
                                </asp:DropDownList>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:DropDownList ID="cmbAtivo" runat="server" Width="100px">
                                    <asp:ListItem Value="1">Sim</asp:ListItem>
                                    <asp:ListItem Value="0">Não</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>                                
                                <asp:Label ID="Label8" runat="server" Text='<%#IIf(Eval("Ativo").Equals(DBNull.Value) OrElse Eval("Ativo").Equals(0), "Não", "Sim")%>' Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nome Completo">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Nome Completo"></asp:Label>
                                <asp:TextBox ID="txtNewNomeCompleto" runat="server" Width="300px" MaxLength="50"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("NomeCompleto")%>' ID="txtNomeCompleto" Width="300px" MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("NomeCompleto")%>' ID="Label9" Width="300px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Perfil">
                            <ItemStyle Width="5%" />
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Perfil"></asp:Label>
                                <asp:TextBox ID="txtNewPerfil" runat="server" Width="100px" MaxLength="10"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("Perfil")%>' ID="txtPerfil" Width="100px" MaxLength="10"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("Perfil")%>' ID="Label10" Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Produto">
                            <ItemStyle Width="5%" />
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Produto"></asp:Label>
                                <asp:TextBox ID="txtNewProduto" runat="server" Width="100px" MaxLength="10"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("Produto")%>' ID="txtProduto" Width="100px" MaxLength="10"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("Produto")%>' ID="Label11" Width="100px"></asp:Label>
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
                        
                        <asp:TemplateField HeaderText="Senha" ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton onclientclick="return ShowConfirmReset(this.id);" ID="lnkR" runat="Server" CssClass="btn btn-primary" Text="Reset" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Reset"></asp:LinkButton>
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





