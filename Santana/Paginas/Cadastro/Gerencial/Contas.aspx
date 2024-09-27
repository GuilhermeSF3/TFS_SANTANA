<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="Contas.aspx.vb" Inherits="Santana.Paginas.Cadastro.Gerencial.Contas" Title="Cadastro de Contas" EnableEventValidation="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Cadastro de Contas</title>
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


                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="COD_CONTA,TTL"
                    ShowFooter="False" ShowHeader="True" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound"
                    OnRowDeleting="GridView1_RowDeleting" OnRowCreated="GridView1_RowCreated"
                    OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCommand="GridView1_RowCommand"
                    CellPadding="0" ForeColor="#333333" CssClass="table table-striped table-bordered table-condensed" Width="700px">


                    <Columns>
                        <asp:TemplateField HeaderText="Código">
                            <HeaderTemplate>
                                <asp:Label ID="lblCOD_CONTA" runat="server" Text="Código"></asp:Label>
                                <asp:TextBox ID="txtNewCOD_CONTA" runat="server" Width="150px"  MaxLength="30"></asp:TextBox>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblCOD_CONTA" runat="server" Text='<%# Eval("COD_CONTA")%>' Width="150px"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCOD_CONTA" runat="server" Text='<%# Eval("COD_CONTA")%>' Width="150px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        
                        <asp:TemplateField HeaderText="Conta Formatada">
                            <HeaderTemplate>
                                <asp:Label ID="lblCONTA_FMT" runat="server" Text="Conta Formatada"></asp:Label>
                                <asp:TextBox ID="txtNewCONTA_FMT" runat="server" Width="130px" MaxLength="50"></asp:TextBox>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("CONTA_FMT")%>' ID="txtCONTA_FMT" Width="130px" MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCONTA_FMT" runat="server" Text='<%# Eval("CONTA_FMT")%>' Width="130px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                                                      
                        
                        <asp:TemplateField HeaderText="Descrição">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Descrição"></asp:Label>
                                <asp:TextBox ID="txtNewDESCR" runat="server" Width="350px" MaxLength="200"></asp:TextBox>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("DESCR")%>' ID="txtDESCR" Width="350px" MaxLength="200"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("DESCR").ToString().Substring(0, Math.Min(Eval("DESCR").ToString().Length, 60))%>' ID="Label3" Width="350px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Totalizadora">
                            <HeaderTemplate>
                                <asp:Label runat="server" Text="Totalizadora"></asp:Label>
                                <asp:DropDownList ID="cmbNewTTL" runat="server" Width="120px">
                                    <asp:ListItem Value="1">Sim</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="0">Não</asp:ListItem>
                                </asp:DropDownList>
                            </HeaderTemplate>                             
                            <EditItemTemplate>
                                <asp:DropDownList ID="cmbTTL" runat="server" Width="120px">
                                    <asp:ListItem Value="1">Sim</asp:ListItem>
                                    <asp:ListItem Value="0">Não</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%#IIf(Eval("TTL").Equals(DBNull.Value) OrElse Eval("TTL").Equals(False), "Não", "Sim")%>' Width="120px"></asp:Label>
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





