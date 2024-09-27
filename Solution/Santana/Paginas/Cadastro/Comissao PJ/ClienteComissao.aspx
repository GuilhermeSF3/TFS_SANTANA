<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="ClienteComissao.aspx.vb" Inherits="Santana.Paginas.Cadastro.Comissao_PJ.ClienteComissao" Title="Cliente Comissao" EnableEventValidation="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Cliente Comissão</title>
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

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="cod,DT_DE"
                    ShowFooter="True" ShowHeader="True" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound"
                    OnRowDeleting="GridView1_RowDeleting" OnRowCreated="GridView1_RowCreated"
                    OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCommand="GridView1_RowCommand"
                    CellPadding="0" ForeColor="#333333" CssClass="table table-striped table-bordered table-condensed" Width="700px"
                    AllowPaging="True" PageSize="5" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnDataBound="GridView1_DataBound">


                    <Columns>
                        <asp:TemplateField HeaderText="Cliente">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Código Cedente"></asp:Label>
                                <asp:DropDownList ID="ddlNewcod" runat="server" Width="250px">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("cod")%>' ID="lblcod" Width="100px"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("cod")%>' ID="lblcod" Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="A partir de">
                            <HeaderTemplate>
                                <asp:Label ID="lblDATA_DeDesc" runat="server" Text="Comissão a partir de"></asp:Label>
                                <asp:TextBox ID="txtNewDATA_De" runat="server" Width="150px" CssClass="clDate" MaxLength="10"></asp:TextBox>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("DT_DE", "{0:dd/MM/yyyy}")%>' ID="lblDT_DE" Width="100px"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("DT_DE", "{0:dd/MM/yyyy}")%>' ID="lblDT_DE" Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nova Modalidade">
                            <HeaderTemplate>
                                <asp:Label ID="lblNOVA_MODALIDADEDesc" runat="server" Text="Nova Modalidade"></asp:Label>
                                <asp:DropDownList ID="ddlNewNOVA_MODALIDADE" runat="server" Width="250px">
                                    <asp:ListItem Value="0">0% - taxas abaixo de 2,55%</asp:ListItem>
                                    <asp:ListItem Value="60">Operações 2,55% a 2,79% - 6%</asp:ListItem>
                                    <asp:ListItem Value="50">Operações Paga 50%</asp:ListItem>
                                    <asp:ListItem Value="80">Operações  - 2% Definido Sr. João</asp:ListItem>
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlNOVA_MODALIDADE" runat="server" Width="250px">
                                    <asp:ListItem Value="0">0% - taxas abaixo de 2,55%</asp:ListItem>
                                    <asp:ListItem Value="60">Operações 2,55% a 2,79% - 6%</asp:ListItem>
                                    <asp:ListItem Value="50">Operações Paga 50%</asp:ListItem>
                                    <asp:ListItem Value="80">Operações  - 2% Definido Sr. João</asp:ListItem>                                   
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("NOVA_MODALIDADE")%>' ID="lblNOVA_MODALIDADE" Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Taxa De">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Taxa De"></asp:Label>
                                <asp:TextBox ID="txtNewTX_DE" runat="server" Width="100px" CssClass="allownumericwithdecimal" MaxLength="53"></asp:TextBox>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("TX_DE")%>' ID="txtTX_DE" CssClass="allownumericwithdecimal" Width="100px" MaxLength="10"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("TX_DE")%>' ID="lblTX_DE" Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Taxa Até">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Taxa Até"></asp:Label>
                                <asp:TextBox ID="txtNewTX_ATE" runat="server" Width="100px" CssClass="allownumericwithdecimal" MaxLength="53"></asp:TextBox>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("TX_ATE")%>' ID="txtTX_ATE" CssClass="allownumericwithdecimal" Width="100px" MaxLength="10"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("TX_ATE")%>' ID="lblTX_ATE" Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PRC_COMISSAO">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Comissão %"></asp:Label>
                                <asp:TextBox ID="txtNewPRC_COMISSAO" runat="server" Width="100px" CssClass="allownumericwithdecimal" MaxLength="53"></asp:TextBox>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("PRC_COMISSAO")%>' ID="txtPRC_COMISSAO" CssClass="allownumericwithdecimal" Width="100px" MaxLength="10"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("PRC_COMISSAO")%>' ID="lblPRC_COMISSAO" Width="100px"></asp:Label>
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
            // make unbind to avoid memory leaks.
            $(".clDate").unbind();

        }

        function EndRequest(sender, args) {
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





