<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="AjusteIP.aspx.vb" Inherits="Santana.Paginas.Cadastro.AjusteIP" Title="Ajuste IP" EnableEventValidation="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Ajuste IP</title>
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
                                        <p class="navbar-text" style="float: none; margin: 0px">Data Referência:</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDataAnterior" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnterior_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <cc1:Datax ID="txtDataFech" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaData" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaData_Click"></asp:Button>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Contrato:</p>
                                    </div>
                                    <asp:TextBox ID="txtContrato" runat="server" MaxLength="15" Width="110px" CssClass="form-control navbar-btn"></asp:TextBox>
                                </div>
                            </li>  
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Parcela:</p>
                                    </div>
                                    <asp:TextBox ID="txtParcela" runat="server" MaxLength="15" Width="110px" CssClass="form-control navbar-btn"></asp:TextBox>
                                </div>
                            </li> 

                            <li>
                                <div style="width: 15px" class="btn-group"></div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div style="height: 20px">
                                    </div>
                                    <asp:Button ID="btnCarregar" runat="server" Text="Carregar" class="btn btn-primary navbar-btn" OnClick="btnCarregar_Click"></asp:Button>
                                    <asp:Button ID="btnMenu" runat="server" Text="Menu Principal" CssClass="btn btn-default navbar-btn" OnClick="btnMenu_Click"></asp:Button>
                                    <asp:Button ID="btnProcessar" onclientclick="return ShowConfirmProcess(this.id);" runat="server" Text="Processar"  CssClass= "btn btn-primary navbar-btn" OnClick="btnProcessar_Click"></asp:Button>
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


                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="data_ref,contrato,parcela,codcobradora,data_pagamento"
                    ShowFooter="False" ShowHeader="True" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound"
                    OnRowDeleting="GridView1_RowDeleting" OnRowCreated="GridView1_RowCreated"
                    OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCommand="GridView1_RowCommand"
                    CellPadding="0" ForeColor="#333333" CssClass="table table-striped table-bordered table-condensed">

                    <Columns>
                        <asp:TemplateField HeaderText="Data Ref.">
                            <EditItemTemplate>
                                <asp:Label ID="lblData" runat="server" Text='<%# Eval("data_ref")%>' Width="100px"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblData" runat="server" Text='<%# Eval("data_ref")%>' Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Contrato"> 
                            <ItemStyle Width="7%" />                           
                            <EditItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("contrato")%>' ID="lblContrato" Width="7%" MaxLength="20"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("contrato")%>' ID="lblContrato" Width="7%"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Parcela">                                                  
                            <EditItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("parcela")%>' ID="lblParcela" Width="7%" MaxLength="100"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("parcela")%>' ID="lblParcela" Width="7%"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Valor">
                            <ItemStyle HorizontalAlign="Right" />                       
                            <EditItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("vlr_parcela")%>' ID="lblValor" Width="50px" MaxLength="5"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("vlr_parcela")%>' ID="lblValor" Width="50px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cód. Cobradora">   
                            <ItemStyle HorizontalAlign="Right" />                         
                            <EditItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("codcobradora")%>' ID="lblCodCob" Width="100px" MaxLength="11"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("codcobradora")%>' ID="lblCodCob" Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cobradora">                                                     
                            <EditItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("cobradora")%>' ID="lblCobradora" Width="150px" MaxLength="50"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("cobradora")%>' ID="lblCobradora" Width="150px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Data Pagto.">
                            <ItemStyle Width="7%" />                      
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("data_pagamento")%>' ID="txtDtPagto" Width="100px" MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("data_pagamento")%>' ID="lblDtPagto" Width="7%"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Recebido">
                            <ItemStyle Width="5%" HorizontalAlign ="Left" />                        
                            <EditItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("recebido")%>' ID="lblRecebido" Width="100px" MaxLength="10"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("recebido")%>' ID="lblRecebido" Width="100px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Valor Recebido">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />                      
                            <EditItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("valorRec")%>' ID="lblValorRec" MaxLength="10"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("valorRec")%>' ID="lblValorRec" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Data Para">
                            <ItemStyle Width="7%" HorizontalAlign="Right" /> 
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtDataPara" MaxLength="15" Width ="100px" ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cobradora Para">
                            <ItemStyle Width="7%" HorizontalAlign="Right" /> 
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtCobradoraPara" MaxLength="5" Width ="100px" ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Pagamento" ShowHeader="False">                         
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Edit" CssClass="btn btn-primary" Text="Pagamento"></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="True" CssClass="btn btn-info" CommandName="Update" Text="Atualizar "></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CssClass="btn btn-info" CommandName="Cancel" Text="Cancelar"></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Reabrir" ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton onclientclick="return ShowConfirm(this.id);" ID="lnkB" runat="Server" CssClass="btn btn-primary" Text="Reabrir" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Reabrir"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Copiar" ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton onclientclick="return ShowConfirmReset(this.id);" ID="lnkR" runat="Server" CssClass="btn btn-primary" Text="Copiar" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Copiar"></asp:LinkButton>
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

            bootbox.confirm("Deseja reabrir este pagamento?", function (result) {
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

            bootbox.confirm("Deseja copiar o pagamento?", function (result) {
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

        function ShowConfirmProcess(controlID) {
            if (confirmed) { return true; }

            bootbox.confirm("Deseja realizar o processamento?", function (result) {
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





