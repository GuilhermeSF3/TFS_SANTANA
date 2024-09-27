<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="Cidade.aspx.vb" Inherits="Santana.Paginas.Cadastro.PT.Cidade" Title="Cidade" EnableEventValidation="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Cidade</title>
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


                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo"
                    ShowFooter="True" ShowHeader="True" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound"
                    OnRowDeleting="GridView1_RowDeleting" OnRowCreated="GridView1_RowCreated"
                    OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCommand="GridView1_RowCommand"
                    CellPadding="0" ForeColor="#333333" CssClass="table table-striped table-bordered table-condensed" Width="700px"
                    AllowPaging="True" PageSize="8" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnDataBound="GridView1_DataBound">
                    

                    <Columns>
                       <asp:TemplateField HeaderText="Código">
                            <HeaderTemplate>
                                <asp:Label ID="lblCodigo" runat="server" Text="Código"></asp:Label>
                                <asp:TextBox ID="txtNewCod" runat="server" Width="50px" MaxLength="3"></asp:TextBox>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblCodigo" runat="server" Text='<%# Eval("codigo")%>' Width="50px"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCodigo" runat="server" Text='<%# Eval("codigo")%>' Width="50px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                                                                                                                           
                        <asp:TemplateField HeaderText="Cidade">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Cidade"></asp:Label>
                                <asp:TextBox ID="txtNewDescr" runat="server" Width="300px" MaxLength="100"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("cidade")%>' MaxLength="100" ID="txtDescr" Width="300px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("cidade")%>' ID="txtDescr1" Width="300px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="UF">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="UF"></asp:Label>
                                <asp:DropDownList ID="cmbNewUF" runat="server" Width="70px"></asp:DropDownList>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("cod_uf")%>' ID="txtUF" Width="70px"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("cod_uf")%>' ID="txtUF1" Width="70px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                       

                        <asp:TemplateField HeaderText="Região">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Região"></asp:Label>
                                <asp:DropDownList ID="cmbNewRegiao" runat="server" Width="210px"></asp:DropDownList>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:Label ID="lblRegiao" runat="server" Text='<%# Eval("cod_regiao1")%>' Visible = "false"></asp:Label>
                                <asp:DropDownList ID="cmbRegiao"    runat="server" Width="210px"   >
                                </asp:DropDownList>

                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("cod_regiao")%>' ID="txtRegiao1" Width="210px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Grupo">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Grupo"></asp:Label>
                                <asp:DropDownList ID="cmbNewGrupo" runat="server" Width="210px"></asp:DropDownList>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("cod_grupo")%>' ID="txtGrupo" Width="210px"  Visible = "false"></asp:Label>
                                <asp:Label ID="lblGrupo" runat="server" Text='<%# Eval("cod_grupo1")%>' Visible = "false"></asp:Label>
                                <asp:DropDownList ID="cmbGrupo"    runat="server" Width="210px"   >
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("cod_grupo")%>' ID="txtGrupo1" Width="210px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CEP">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Cep De"></asp:Label>
                                <asp:TextBox ID="txtNewCep_De" runat="server" Width="90px" MaxLength="8"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("cep_de")%>' ID="txtCep_De" MaxLength="8" Width="90px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("cep_de")%>' ID="txtCep_De1" Width="90px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CEP_ATE">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Cep Até"></asp:Label>
                                <asp:TextBox ID="txtNewCep_Ate" runat="server" Width="90px" MaxLength="8"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("cep_ate")%>' ID="txtCep_Ate" MaxLength="8" Width="90px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("cep_ate")%>' ID="txtCep_Ate1" Width="90px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Agente">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Agente"></asp:Label>
                                <asp:DropDownList ID="cmbNewAgente" runat="server" Width="200px"></asp:DropDownList>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("cod_agente")%>' ID="txtAgente" Width="200px"></asp:Label>
                                <asp:Label ID="lblAGENTE" runat="server" Text='<%# Eval("cod_AGENTE1")%>' Visible = "false"></asp:Label>
                                <asp:DropDownList ID="cmbAGENTE"    runat="server" Width="210px"   >
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("cod_agente")%>' ID="txtAgente1" Width="200px"></asp:Label>
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
                                <asp:LinkButton OnClientClick="return ShowConfirm(this.id);" ID="lnkB" runat="Server" CssClass="btn btn-primary" Text="Excluir" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    
                    <PagerTemplate>
                        <div class="btn-group btn-group-sm" style="margin: 5px">
                            <asp:ImageButton ID="PagerFirst" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/skip_backward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="First" UseSubmitBehavior="false"></asp:ImageButton>
                            <asp:ImageButton ID="PagerPrev" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/seek_backward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="Prev" UseSubmitBehavior="false"></asp:ImageButton>
                            <asp:ImageButton ID="PagerNext" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/seek_forward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="Next" UseSubmitBehavior="false"></asp:ImageButton>
                            <asp:ImageButton ID="PagerLast" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/skip_forward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="Last" UseSubmitBehavior="false"></asp:ImageButton>
                        </div>

                        <asp:DropDownList ID="PagerPages" runat="server" AutoPostBack="true" Width="60px" CausesValidation="false" OnSelectedIndexChanged="PagerPages_SelectedIndexChanged" CssClass="navbar-btn" Style="z-index: 2000" />&nbsp;
                    </PagerTemplate>

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

       // var isDecimal = false;

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

    </script>
</asp:Content>





