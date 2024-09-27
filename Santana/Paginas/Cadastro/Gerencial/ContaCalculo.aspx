<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="ContaCalculo.aspx.vb" Inherits="Santana.Paginas.Cadastro.Gerencial.ContaCalculo" Title="Cadastro de Contas Cálculos" EnableEventValidation="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Cadastro de Contas Cálculos</title>
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
                                    <div>
                                        <p id="ddlCodContalabel" class="navbar-text" style="float: none; margin: auto">Conta</p>
                                    </div>
                                    <asp:DropDownList ID="ddlCodConta" Width="100px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>
                            <li>
                                <div style="width: 15px" class="btn-group"></div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div style="height: 20px">
                                    </div>
                                    <asp:Button ID="btnCarregar" runat="server" Text="Filtrar" class="btn btn-primary navbar-btn" OnClick="btnCarregar_Click"></asp:Button>
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
                                        <asp:ImageButton ID="btnExcel" runat ="server" CssClass="btn btn-default navbar-btn" OnClick="btnExcel_Click" ImageUrl="~/imagens/excel2424.png"></asp:ImageButton>
                                        <asp:ImageButton ID="btnHelp"  runat ="server" CssClass="btn btn-default navbar-btn" OnClick="btnHelp_Click" ImageUrl="~/imagens/help2424.png"></asp:ImageButton>
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


                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="COD_CONTA,CONTA_CALCULO,CONTA_CAMPO,SINAL"
                    ShowFooter="True" ShowHeader="True" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound"
                    OnRowDeleting="GridView1_RowDeleting" OnRowCreated="GridView1_RowCreated"
                    OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCommand="GridView1_RowCommand"
                    CellPadding="0" ForeColor="#333333" CssClass="table table-striped table-bordered table-condensed" Width="700px"
                    AllowPaging="True" PageSize="30" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnDataBound="GridView1_DataBound">


                    <Columns>
                        <asp:TemplateField HeaderText="Código">
                            <HeaderTemplate>
                                <asp:Label ID="lblCOD_CONTA" runat="server" Text="Código"></asp:Label>
                                <asp:DropDownList ID="cmbNewCOD_CONTA" runat="server" Width="250px"></asp:DropDownList>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblCOD_CONTA" runat="server" Text='<%# Eval("COD_CONTA")%>' Width="250px"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCOD_CONTA" runat="server" Text='<%# Eval("COD_CONTA")%>' Width="250px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Tipo de Conta">
                            <HeaderTemplate>
                                <asp:Label ID="lblCONTA_CAMPO" runat="server" Text="Tipo de Conta"></asp:Label>
                                <asp:DropDownList ID="cmbNewCONTA_CAMPO" runat="server" Width="130px" AutoPostBack="true" 
                                    OnSelectedIndexChanged="cmbNewCONTA_CAMPO_OnSelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="SLD_ANT">Saldo Anterior</asp:ListItem>
                                    <asp:ListItem Value="MOV_DEB">Mov. Debito</asp:ListItem>
                                    <asp:ListItem Value="MOV_CRED">Mov. Credito</asp:ListItem>
                                    <asp:ListItem Value="MOV_D_C">Mov. Cred. Deb.</asp:ListItem>
                                    <asp:ListItem Value="SLD_ATU">Saldo Atual</asp:ListItem>
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <EditItemTemplate>
<%--                                <asp:Label ID="lblCONTA_CAMPO" runat="server" Text='<%# Eval("CONTA_CAMPO")%>' Width="130px"></asp:Label>--%>
                                <asp:DropDownList ID="cmbCONTA_CAMPO"  Text='<%# Eval("CONTA_CAMPO")%>' runat="server" Width="130px" AutoPostBack="true" 
                                    OnSelectedIndexChanged="cmbCONTA_CAMPO_OnSelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="SLD_ANT">Saldo Anterior</asp:ListItem>
                                    <asp:ListItem Value="MOV_DEB">Mov. Debito</asp:ListItem>
                                    <asp:ListItem Value="MOV_CRED">Mov. Credito</asp:ListItem>
                                    <asp:ListItem Value="MOV_D_C">Mov. Cred. Deb.</asp:ListItem>
                                    <asp:ListItem Value="SLD_ATU">Saldo Atual</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCONTA_CAMPO" runat="server" Text='<%# Eval("CONTA_CAMPO")%>' Width="130px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Sinal">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Sinal"></asp:Label>
                                <asp:DropDownList ID="cmbNewSINAL" runat="server" Width="50px">
                                    <asp:ListItem Selected="True" Value="+">+</asp:ListItem>
                                    <asp:ListItem Value="-">-</asp:ListItem>
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" Text='<%# Eval("SINAL")%>' ID="cmbSINAL" Width="50px">
                                    <asp:ListItem Selected="True" Value="+">+</asp:ListItem>
                                    <asp:ListItem Value="-">-</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("SINAL").ToString()%>' ID="Label3" Width="50px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Plano de Contas">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Plano de Contas"></asp:Label>
                                <asp:DropDownList ID="cmbNewCONTA_CALCULO" runat="server" Width="130px"></asp:DropDownList>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblCONTA_CALCULO" runat="server" Text='<%# Eval("CONTA_CALCULO")%>' Width="130px"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCONTA_CALCULO" runat="server" Text='<%# Eval("CONTA_CALCULO")%>' Width="130px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Conta Formatada">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Conta Formatada"></asp:Label>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblCONTA_format" runat="server" Text='<%# Eval("CONTA_format")%>' Width="130px"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCONTA_format" runat="server" Text='<%# Eval("CONTA_format")%>' Width="130px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nome da Conta">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Nome da Conta"></asp:Label>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblNome_CONT" runat="server" Text='<%# Eval("Nome_CONT")%>' Width="500px"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblNome_CONT" runat="server" Text='<%# Eval("Nome_CONT")%>' Width="500px"></asp:Label>
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





