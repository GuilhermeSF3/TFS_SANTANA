<%@ Page Language="vb" MasterPageFile="~/SantanaWeb.Master" AutoEventWireup="true" CodeBehind="DetalheContratosDiario.aspx.vb" Inherits="Santana.DetalheContratosDiario" Title="Contrato Detalhe Diário"  EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Contrato Detalhe</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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

                        <ul class="nav navbar-nav" >

             
                            <li> 

                                <table style="margin-top:10px">
                                    <tr>
                                        <td style="width:150px;">
                                            <p class="navbar-text" style="margin:4px">Data de Referencia</p>
                                        </td>
                                        <td style="width:350px">
                                                <asp:Label ID="lblDataReferencia" runat="server" Text="" class="navbar-text" style="margin:4px "></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p class="navbar-text" style="margin:4px">Agente</p>
                                        </td> 
                                        <td>
                                            <asp:Label ID="lblAgente" runat="server" Text="" class="navbar-text" style="margin:4px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p class="navbar-text" style="margin:4px">Cobradora</p>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCobradora" runat="server" Text="" class="navbar-text" style="margin:4px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p class="navbar-text" style="margin:4px">Classe de Atrasso</p>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblClasse" runat="server" Text="" class="navbar-text" style="margin:4px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>                                 

                            </li>
                        </ul>


                        <ul class="nav navbar-nav">
                            <li>
                                <table>
                                    <tr>
                                        <td>
                                            <div class="btn-group">
                                                <asp:Button ID="btnMenu" runat="server" Text="Menu Principal" class="btn btn-default navbar-btn" OnClick="btnMenu_Click"></asp:Button>
                                                <asp:Button ID="btnVoltar" runat="server" class="btn btn-default navbar-btn" OnClick="btnVoltar_Click"></asp:Button>
                                            </div>
                                        </td>
                                        <td>
                                        </td> 
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtLocalizar" Width="180px" runat="server" Visible="True" Enabled="true"  CssClass="form-control navbar-btn"></asp:TextBox>
                                        </td>
                                        <td>
                                            <div class="btn-group">
                                                <asp:Button ID="btnLocalizar" runat="server" Text="Localizar" class="btn btn-primary navbar-btn" OnClick="btnLocalizar_Click" ></asp:Button>
                                                <asp:Button ID="btnLimpar" runat="server" Text="Limpar" class="btn btn-primary navbar-btn" OnClick="btnLimpar_Click" ></asp:Button>
                                            </div> 
                                        </td>
                                    </tr>

                                </table> 
                            </li>

                        </ul>

                        <ul class="nav navbar-nav navbar-right">
                            <li>
                                <div class="btn-group-sm  ">
                                    <asp:ImageButton ID="btnExcel" runat="server" CssClass="btn btn-default navbar-btn" OnClick="btnExcel_Click"  ImageUrl="~/imagens/excel2424.png"></asp:ImageButton>
                                    <asp:ImageButton ID="btnImpressao" runat="server" CssClass="btn btn-default navbar-btn" OnClick="btnImpressao_Click"  ImageUrl="~/imagens/printer2424.png"></asp:ImageButton>
                                    <asp:ImageButton ID="btnHelp" runat="server" CssClass="btn btn-default navbar-btn" OnClick="btnHelp_Click" ImageUrl="~/imagens/help2424.png"></asp:ImageButton>
                                </div>
                            </li>

                        </ul>
                    </div>

                </div>
            </nav>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
        <ContentTemplate>

            <div id="dvConsultas" style="height: 360px; width: 100%; overflow: auto;">


                <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="true"
                    AllowPaging="True" PageSize="7" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowCreated="GridView1_RowCreated" ShowFooter="true" OnSorting="GridView1_Sorting"
                    OnDataBound="GridView1_DataBound" >


                    <RowStyle Height="31px" />
                    <Columns>

                      <asp:TemplateField HeaderText="CONTRATO" SortExpression="CONTRATO">
                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                      <asp:TemplateField HeaderText="CPF CNPJ" SortExpression="CPF_CNPJ">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                     <asp:TemplateField HeaderText="NOME CLIENTE" SortExpression="NOME_CLIENTE">
                            <ItemStyle Width="20%" HorizontalAlign="Left" />
<%--                            <FooterTemplate>NUMERO DE REGISTROS: <%=QuantidadeRegistros.ToString("#,##0##")%> - TOTAL DE SALDO INSCRITO: <%=SaldoTotal.ToString("#,##0##")%></FooterTemplate>--%>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="AGENTE" SortExpression="AGENTE">
                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                             <asp:TemplateField HeaderText="DATA CONTRATO" SortExpression="DATA_CONTRATO">
                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PLANO" SortExpression="PLANO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PARCELA ABERTO" SortExpression="PARCELA_ABERTO">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PRIMEIRO VCTO" SortExpression="PRIMEIRO_VCTO">
                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ATRASO" SortExpression="ATRASO">
                            <ItemStyle Width="3%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VENCIMENTO" SortExpression="VENCIMENTO">
                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VLR PARCELA" SortExpression="VLR_PARCELA">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="QTD PARC ATRASO" SortExpression="QTD_PARC_ATRASO">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SALDO INSCRITO" SortExpression="SALDO_INSCRITO">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VLR FINANCIADO" SortExpression="VLR_FINANCIADO">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MARCA" SortExpression="MARCA">
                            <ItemStyle Width="15%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MODELO" SortExpression="MODELO">
                            <ItemStyle Width="15%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ANO FABRIC" SortExpression="ANO_FABRIC">
                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PARC PAGAS" SortExpression="PARC_PAGAS">
                            <ItemStyle Width="4%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PROFISSAO" SortExpression="PROFISSAO">
                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CARGO" SortExpression="CARGO">
                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemStyle Width="3%" />
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



    <script type="text/javascript">


        function pageLoad() {
            $('#<%=GridView1.ClientID%>').gridviewScroll({
                width: 100 + "%",
                height: 100 + "%",
                freezesize: 0,
                arrowsize: 30,
                varrowtopimg: "/imagens/arrowvt.png",
                varrowbottomimg: "/imagens/arrowvb.png",
                harrowleftimg: "/imagens/arrowhl.png",
                harrowrightimg: "/imagens/arrowhr.png"
            });
        }

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

    </script>


</asp:Content>
