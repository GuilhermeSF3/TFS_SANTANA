<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="AlteracaoGarantia.aspx.vb" Inherits="Santana.Paginas.Cadastro.AlteracaoGarantia" Title="Alteração Garantia" EnableEventValidation="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Alteração Garantia</title>
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
                                        <p class="navbar-text" style="float: none; margin: auto">Proposta:</p>
                                    </div>
                                    <asp:TextBox ID="txtProposta" runat="server" MaxLength="15" Width="110px" CssClass="form-control navbar-btn"></asp:TextBox>
                                </div>
                            </li>  

                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Garantia:</p>
                                    </div>
                                    <asp:TextBox ID="txtGarantia" runat="server" MaxLength="15" Width="110px" CssClass="form-control navbar-btn"></asp:TextBox>
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



    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>


            <div id="dvConsultas" style="height: 590px; width: 100%; overflow: auto; visibility: visible">


                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ABNRPROP, ABNRGAR"
                    ShowFooter="False" ShowHeader="True" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound"
                    OnRowDeleting="GridView1_RowDeleting" OnRowCreated="GridView1_RowCreated"
                    OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCommand="GridView1_RowCommand"
                    CellPadding="0" ForeColor="#333333" CssClass="table table-striped table-bordered table-condensed">


                    <Columns>
                        <asp:TemplateField HeaderText="Proposta">
                            <ItemStyle Width="7%" />   
                            <EditItemTemplate>
                                <asp:Label ID="lblProposta" runat="server" Text='<%# Eval("ABNRPROP")%>' Width="50px"></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblProposta" runat="server" Text='<%# Eval("ABNRPROP")%>' Width="50px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Garantia">
                            <ItemStyle Width="7%" />                      
                            <EditItemTemplate>
                                <asp:Label ID="lblGarantia" runat="server" Text='<%# Eval("ABNRGAR")%>'  Width="50px"></asp:Label>
                                <%--<asp:TextBox runat="server" Text='<%# Eval("ABNRGAR")%>' ID="txtScore" Width="50px" MaxLength="50"></asp:TextBox>--%>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblGarantia" runat="server" Text='<%# Eval("ABNRGAR")%>' Width="50px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Modelo">
                            <ItemStyle Width="30%" />                      
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("ABMODELO")%>' ID="txtModelo" Width="700px" MaxLength="100"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblModelo" runat="server" Text='<%# Eval("ABMODELO")%>' Width="500px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Marca">
                            <ItemStyle Width="7%" />                      
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("TMDESCR")%>' ID="txtMarca" Width="70px" MaxLength="100"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblMarca" runat="server" Text='<%# Eval("TMDESCR")%>' Width="50px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Ano Modelo">
                            <ItemStyle Width="7%" />                      
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("ABANOMOD")%>' ID="txtAnoMod" Width="70px" MaxLength="100"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAnoMod" runat="server" Text='<%# Eval("ABANOMOD")%>' Width="50px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Ano Fabr.">
                            <ItemStyle Width="7%" />                      
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("ABANOFAB")%>' ID="txtAnoFab" Width="70px" MaxLength="100"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAnoFab" runat="server" Text='<%# Eval("ABANOFAB")%>' Width="50px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Valor Fipe">
                            <ItemStyle Width="7%" />                      
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("ABVLRMLC")%>' ID="txtVlrMolicar" Width="70px" MaxLength="100"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblVlrMolicar" runat="server" Text='<%# Eval("ABVLRMLC")%>' Width="50px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Valor Venda">
                            <ItemStyle Width="7%" />                      
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("ABVLRVND")%>' ID="txtVlrVenda" Width="70px" MaxLength="100"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblVlrVenda" runat="server" Text='<%# Eval("ABVLRVND")%>' Width="50px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Valor Tabela">
                            <ItemStyle Width="7%" />                      
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("ABVLRTAB")%>' ID="txtVlrTabela" Width="70px" MaxLength="100"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTabela" runat="server" Text='<%# Eval("ABVLRTAB")%>' Width="50px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Placa">
                            <ItemStyle Width="7%" />                      
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("ABPLACA")%>' ID="txtPlaca" Width="70px" MaxLength="100"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPlaca" runat="server" Text='<%# Eval("ABPLACA")%>' Width="50px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Renavam">
                            <ItemStyle Width="7%" />                      
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("ABRENAVAM")%>' ID="txtRenavam" Width="70px" MaxLength="100"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblRenavam" runat="server" Text='<%# Eval("ABRENAVAM")%>' Width="50px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Atualizar" ShowHeader="False">                         
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Edit" CssClass="btn btn-primary" Text="Atualizar"></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="True" CssClass="btn btn-info" CommandName="Update" Text="Atualizar "></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CssClass="btn btn-info" CommandName="Cancel" Text="Cancelar"></asp:LinkButton>
                            </EditItemTemplate>
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

    <asp:HiddenField ID="hfGridView1SV" runat="server" OnValueChanged="hfGridView1SV_ValueChanged" />
    <asp:HiddenField ID="hfGridView1SH" runat="server" OnValueChanged="hfGridView1SH_ValueChanged" />



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

    </script>
</asp:Content>





