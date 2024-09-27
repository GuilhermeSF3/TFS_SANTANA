<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="spread.aspx.vb" Inherits="Santana.Spread" Title="Spread" EnableEventValidation="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Spread</title>



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
                                        <p class="navbar-text" style="float: none; margin: auto">Data Produção DE:</p>
                                    </div>
                                    <cc1:Datax ID="txtDataDE" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0px">Data Produção ATÉ:</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDataAnterior" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnterior_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <cc1:Datax ID="txtData" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaData" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaData_Click"></asp:Button>
                                    </div>
                                </div>
                            </li>



                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Contratos</p>
                                    </div>
                                    <asp:DropDownList ID="ddlAgente" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>

                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Veículos</p>
                                    </div>
                                    <asp:DropDownList ID="ddlCobradora" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Relatorio</p>
                                    </div>
                                    <asp:DropDownList ID="ddlRelatorio" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn" OnSelectedIndexChanged="ddlRelatorio_SelectedIndexChanged"></asp:DropDownList>
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
                                    <asp:Button ID="btnMenu" runat="server" Text="Menu Principal" class="btn btn-default navbar-btn" OnClick="btnMenu_Click"></asp:Button>

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

                    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">

                        <ul class="nav navbar-nav">
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Data Safra Risco DE:</p>
                                    </div>
                                    <cc1:Datax ID="txtDataINI" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Data Safra Risco ATÉ:</p>
                                    </div>
                                    <cc1:Datax ID="txtDataFIM" runat="server" MaxLength="12" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Dias Atraso:</p>
                                    </div>
                                    <asp:TextBox ID="txtDiasAtraso" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></asp:TextBox>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p id="ddlPromotoraLabel" runat="server" class="navbar-text" style="float: none; margin: auto" visible="False">Agentes</p>
                                    </div>
                                    <asp:DropDownList ID="ddlPromotora" Width="50px" runat="server" Visible="False" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
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

            <div id="dvConsultasCarteiras" runat="server" style="height: 590px; width: 100%; overflow: auto; visibility: visible ">


                <asp:GridView ID="GridViewCarteiras" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="TRUE"
                    AllowPaging="True" PageSize="15" OnPageIndexChanging="GridViewCarteiras_PageIndexChanging"
                    OnRowCreated="GridViewCarteiras_RowCreated" DataKeyNames="ordem_linha,descr_veic" ShowFooter="true" OnDataBound="GridViewCarteiras_DataBound" Font-Size="9pt">


                    <RowStyle Height="28px" />
                    <Columns>


                        <asp:TemplateField HeaderText="DATA REF" SortExpression="DT_FECHA" ItemStyle-BackColor="#EFEFEF">
                            <ItemStyle Width="5%" HorizontalAlign="LEFT" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Veículo" SortExpression="DESCR_VEIC" ItemStyle-BackColor="#EFEFEF">
                            <ItemStyle Width="18%" HorizontalAlign="Left" BackColor="#EFEFEF" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Relatório" SortExpression="DESCRICAO" ItemStyle-BackColor="#EFEFEF">
                            <ItemStyle Width="8%" HorizontalAlign="LEFT" BackColor="#EFEFEF" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PRAZO (meses)" SortExpression="PRAZO_MEDIO">
                            <ItemStyle Width="9%" HorizontalAlign="RIGHT"  />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="TAXA am (%)" SortExpression="TAXA_MEDIA">
                            <ItemStyle Width="3%" HorizontalAlign="Right"  />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="TICKET MÉDIO" SortExpression="TICKET_MEDIO">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="VLR CONTRATOS" SortExpression="VLR_CONTRATOS">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="QTD CONTRATOS" SortExpression="QTD_CONTRATOS">
                            <ItemStyle Width="18%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CAPTAÇÃO (%)" SortExpression="CAPTACAO">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PERDA (%)" SortExpression="PERDA">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="COMISSÃO (%)" SortExpression="COMISSAO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="SPREAD LÍQUIDO TIPO" SortExpression="SPREAD">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CARTEIRA" SortExpression="CARTEIRA">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="% CARTEIRA" SortExpression="PRC_CARTEIRA">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PRODUCAO TRIM" SortExpression="PRODUCAO_TRIM">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PARTICIPAÇÃO" SortExpression="PRC_PRODUCAO_TRIM">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="VF SAFRA" SortExpression="VF_SAFRA">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VLR RISCO" SortExpression="VLR_PERDA">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ordem_linha" SortExpression="ordem_linha" Visible="false">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemStyle Width="1%" />
                        </asp:TemplateField>

                    </Columns>
                    
                    <PagerTemplate>
                        <div class="btn-group btn-group-sm" style="margin:5px" >
                            <asp:ImageButton ID="PagerFirst" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/skip_backward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="First" UseSubmitBehavior="false"></asp:ImageButton>
                            <asp:ImageButton ID="PagerPrev" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/seek_backward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="Prev" UseSubmitBehavior="false"></asp:ImageButton>
                            <asp:ImageButton ID="PagerNext" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/seek_forward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="Next" UseSubmitBehavior="false"></asp:ImageButton>
                            <asp:ImageButton ID="PagerLast" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/skip_forward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="Last" UseSubmitBehavior="false"></asp:ImageButton>
                        </div>

                        <asp:DropDownList ID="PagerPages" runat="server" AutoPostBack="true" Width="60px" CausesValidation="false" OnSelectedIndexChanged="PagerPages_SelectedIndexChanged" CssClass="navbar-btn" style="z-index:2000" />&nbsp;
                    </PagerTemplate>

                    <HeaderStyle CssClass="GridviewScrollC3Header" />
                    <RowStyle CssClass="GridviewScrollC3Item" />
                    <PagerStyle CssClass="GridviewScrollC3Pager" />
                    <FooterStyle CssClass="GridviewScrollC3Footer" />

                </asp:GridView>



            </div>




            <div id="dvConsultasOperador" runat="server" style="height: 590px; width: 100%; overflow: auto; visibility: visible">


                <asp:GridView ID="GridViewOperador" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="TRUE"
                    AllowPaging="True" PageSize="15" OnPageIndexChanging="GridViewOperador_PageIndexChanging"
                    OnRowCreated="GridViewOperador_RowCreated" DataKeyNames="DES_OPER" ShowFooter="true" OnDataBound="GridViewOperador_DataBound"
                    Font-Size="9pt">


                    <RowStyle Height="28px" />
                    <Columns>


                        <asp:TemplateField HeaderText="Código" SortExpression="COD_OPER">
                            <ItemStyle Width="7%" HorizontalAlign="LEFT" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nome" SortExpression="DES_OPER">
                            <ItemStyle Width="18%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Agente" SortExpression="AGENTE">
                            <ItemStyle Width="18%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Carteira (Mil)" SortExpression="CARTEIRA">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Prazo Médio" SortExpression="PRAZO_MEDIO">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tx % am" SortExpression="TAXA_MEDIA">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Risco" SortExpression="RISCO">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Spread" SortExpression="SPREAD1">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Spred 4o Trim 2013" SortExpression="SPREAD4" Visible="FALSE">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="QTDE PROD" SortExpression="QTDE">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VLR PRODUCAO" SortExpression="VLR_PRODUCAO">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="VF SAFRA" SortExpression="VF_SAFRA">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VLR RISCO" SortExpression="VLR_PERDA">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemStyle Width="1%" />
                        </asp:TemplateField>

                    </Columns>

                    <PagerTemplate>
                        <div class="btn-group btn-group-sm" style="margin:5px" >
                            <asp:ImageButton ID="PagerFirst" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/skip_backward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="First" UseSubmitBehavior="false"></asp:ImageButton>
                            <asp:ImageButton ID="PagerPrev" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/seek_backward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="Prev" UseSubmitBehavior="false"></asp:ImageButton>
                            <asp:ImageButton ID="PagerNext" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/seek_forward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="Next" UseSubmitBehavior="false"></asp:ImageButton>
                            <asp:ImageButton ID="PagerLast" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/skip_forward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="Last" UseSubmitBehavior="false"></asp:ImageButton>
                        </div>

                        <asp:DropDownList ID="PagerPages" runat="server" AutoPostBack="true" Width="60px" CausesValidation="false" OnSelectedIndexChanged="PagerPages_SelectedIndexChanged" CssClass="navbar-btn" style="z-index:2000" />&nbsp;
                    </PagerTemplate>

                    <HeaderStyle CssClass="GridviewScrollC3Header" />
                    <RowStyle CssClass="GridviewScrollC3Item" />
                    <PagerStyle CssClass="GridviewScrollC3Pager" />
                    <FooterStyle CssClass="GridviewScrollC3Footer" />

                </asp:GridView>



            </div>





            <div id="dvConsultasLojas" runat="server" style="height: 590px; width: 100%; overflow: auto; visibility: visible">


                <asp:GridView ID="GridViewLojas" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="TRUE"
                    AllowPaging="True" PageSize="15" OnPageIndexChanging="GridViewLojas_PageIndexChanging"
                    OnRowCreated="GridViewLojas_RowCreated" DataKeyNames="DES_LOJA" ShowFooter="true" OnDataBound="GridViewLojas_DataBound"
                    Font-Size="9pt">


                    <RowStyle Height="28px" />
                    <Columns>


                        <asp:TemplateField HeaderText="Código" SortExpression="COD_LOJA">
                            <ItemStyle Width="7%" HorizontalAlign="LEFT" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nome" SortExpression="DES_LOJA">
                            <ItemStyle Width="18%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Sit" SortExpression="SIT">
                            <ItemStyle Width="7%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cod. Oper." SortExpression="COD_OPER">
                            <ItemStyle Width="7%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Agente" SortExpression="AGENTE">
                            <ItemStyle Width="18%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Operador" SortExpression="DES_OPER">
                            <ItemStyle Width="18%" HorizontalAlign="Left" />
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Qtd Contr Cart" SortExpression="PROD_1M">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Carteira (Mil)" SortExpression="CARTEIRA">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Prazo Médio" SortExpression="PRAZO_MEDIO">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tx % am" SortExpression="TAXA_MEDIA">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Produção Trim" SortExpression="CARTEIRA_TRIM">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qtd Contr Prod" SortExpression="PROD_2M">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tkt Médio" SortExpression="PROD_3M" >
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Risco" SortExpression="RISCO">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Perda" SortExpression="VLR_PERDA"  >
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Spred Média geral" SortExpression="SPREAD_MEDIO">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tx % am (Trim. Ant.)" SortExpression="TAXA_MEDIA_TRIM"  Visible="false">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Spread (Trim. Ant.)" SortExpression="SPREAD_TRIM" Visible="false">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="VF SAFRA" SortExpression="VF_SAFRA">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VLR RISCO" SortExpression="VLR_PERDA">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemStyle Width="1%" />
                        </asp:TemplateField>

                    </Columns>

                    <PagerTemplate>
                        <div class="btn-group btn-group-sm" style="margin:5px" >
                            <asp:ImageButton ID="PagerFirst" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/skip_backward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="First" UseSubmitBehavior="false"></asp:ImageButton>
                            <asp:ImageButton ID="PagerPrev" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/seek_backward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="Prev" UseSubmitBehavior="false"></asp:ImageButton>
                            <asp:ImageButton ID="PagerNext" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/seek_forward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="Next" UseSubmitBehavior="false"></asp:ImageButton>
                            <asp:ImageButton ID="PagerLast" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/skip_forward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="Last" UseSubmitBehavior="false"></asp:ImageButton>
                        </div>

                        <asp:DropDownList ID="PagerPages" runat="server" AutoPostBack="true" Width="60px" CausesValidation="false" OnSelectedIndexChanged="PagerPages_SelectedIndexChanged" CssClass="navbar-btn" style="z-index:2000" />&nbsp;
                    </PagerTemplate>

                    <HeaderStyle CssClass="GridviewScrollC3Header" />
                    <RowStyle CssClass="GridviewScrollC3Item" />
                    <PagerStyle CssClass="GridviewScrollC3Pager" />
                    <FooterStyle CssClass="GridviewScrollC3Footer" />

                </asp:GridView>



            </div>


        <div id="dvConsultasBalcao" runat="server" style="height: 590px; width: 100%; overflow: auto; visibility: visible">


                <asp:GridView ID="GridViewBalcao" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="TRUE"
                    AllowPaging="True" PageSize="15" OnPageIndexChanging="GridViewBalcao_PageIndexChanging"
                    OnRowCreated="GridViewBalcao_RowCreated" DataKeyNames="DES_LOJA" ShowFooter="true" OnDataBound="GridViewBalcao_DataBound"
                    Font-Size="9pt">


                    <RowStyle Height="28px" />
                    <Columns>

                        <asp:TemplateField HeaderText="Código" SortExpression="COD_LOJA">
                            <ItemStyle Width="7%" HorizontalAlign="LEFT" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nome" SortExpression="DES_LOJA">
                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Sit" SortExpression="SIT">
                            <ItemStyle Width="7%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Agente" SortExpression="AGENTE">
                            <ItemStyle Width="18%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Qtd Contr Cart" SortExpression="PROD_1M">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Carteira (Mil)" SortExpression="CARTEIRA">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Prazo Médio" SortExpression="PRAZO_MEDIO">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tx % am" SortExpression="TAXA_MEDIA">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Produção Trim" SortExpression="CARTEIRA_TRIM">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Qtd Contr Prod" SortExpression="PROD_2M">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tkt Médio" SortExpression="PROD_3M" >
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Risco" SortExpression="RISCO">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Perda" SortExpression="VLR_PERDA"  >
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Spred Média geral" SortExpression="SPREAD_MEDIO">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tx % am (Trim. Ant.)" SortExpression="TAXA_MEDIA_TRIM">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Spread (Trim. Ant.)" SortExpression="SPREAD_TRIM">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Captação" SortExpression="CAPTAÇÃO">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="VF SAFRA" SortExpression="VF_SAFRA">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="VLR RISCO" SortExpression="VLR_PERDA">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Produção 3m" SortExpression="PROD_BL_3M">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Produção 2m" SortExpression="PROD_BL_2M">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Produção 1m" SortExpression="PROD_BL_2M">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Produção Agente 3m" SortExpression="PROD_AG_3M">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Produção Agente 2m" SortExpression="PROD_AG_2M">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Produção Agente 1m" SortExpression="PROD_AG_1M">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Produção Agente Trim" SortExpression="PROD_AG_TRIM">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="% Balcao Agente" SortExpression="PRC_BALCAO_AG">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemStyle Width="1%" />
                        </asp:TemplateField>

                    </Columns>

                    <PagerTemplate>
                        <div class="btn-group btn-group-sm" style="margin:5px" >
                            <asp:ImageButton ID="PagerFirst" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/skip_backward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="First" UseSubmitBehavior="false"></asp:ImageButton>
                            <asp:ImageButton ID="PagerPrev" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/seek_backward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="Prev" UseSubmitBehavior="false"></asp:ImageButton>
                            <asp:ImageButton ID="PagerNext" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/seek_forward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="Next" UseSubmitBehavior="false"></asp:ImageButton>
                            <asp:ImageButton ID="PagerLast" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/skip_forward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="Last" UseSubmitBehavior="false"></asp:ImageButton>
                        </div>

                        <asp:DropDownList ID="PagerPages" runat="server" AutoPostBack="true" Width="60px" CausesValidation="false" OnSelectedIndexChanged="PagerPages_SelectedIndexChanged" CssClass="navbar-btn" style="z-index:2000" />&nbsp;
                    </PagerTemplate>

                    <HeaderStyle CssClass="GridviewScrollC3Header" />
                    <RowStyle CssClass="GridviewScrollC3Item" />
                    <PagerStyle CssClass="GridviewScrollC3Pager" />
                    <FooterStyle CssClass="GridviewScrollC3Footer" />

                </asp:GridView>



            </div>

        </ContentTemplate>
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





