<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" EnableEventValidation="false" AutoEventWireup="false" CodeBehind="Contratos.aspx.vb" Inherits="Santana.Cobranca_Dias_Atraso_Contratos" Title="Untitled Page" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Table ID="Table2" Visible="true" BorderStyle="Double" BorderColor="DarkGreen" runat="server" BorderWidth="1" Width="100%" CellSpacing="1"
        CellPadding="0" HorizontalAlign="Center" GridLines="None">
        <asp:TableRow Height="20px" BorderStyle="Double" BackColor="White" CssClass="tr-7">
            <asp:TableCell ColumnSpan="4" HorizontalAlign="Center">
                <asp:Label ID="Label1" runat="server" ForeColor="#006600" Text="" CssClass="font-default-6" Font-Bold="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </asp:TableCell>
            <asp:TableCell Width="4%" VerticalAlign="Middle" HorizontalAlign="Left">
                <asp:ImageButton ID="Exportar" runat="server" Visible="false" Height="30px" Width="30px" ImageUrl="~/imagens/excel1.bmp" ToolTip="Exportar MS-Excel" />&nbsp;&nbsp; 
           <asp:ImageButton ID="ExpBrOffice" ImageAlign="AbsMiddle" runat="server" Visible="true" Height="25px" Width="30px" ImageUrl="~/imagens/BrOfficeXls.png" ToolTip="Exportar BR Office" />&nbsp;&nbsp;
            </asp:TableCell>
            <asp:TableCell BackColor="White" Height="30px" Width="30px"
                ToolTip="Dias de Atraso Estrutura, podendo selecionar o numero da parcela e intervalo de vencimento, com filtro de: Operação,Cliente,CPF/CNPJ,Loja,Cobradora,Filial,Supervisor,Operador. Posição on-line."
                VerticalAlign="Middle" HorizontalAlign="Left">
                <asp:Image ID="imgHelp" runat="server" BackColor="White" ImageUrl="~/imagens/Help3.jpg" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>


    <asp:ScriptManager ID="SManager" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdateManager" runat="server">
        <ContentTemplate>

            <asp:Table ID="Table4" runat="server" BorderWidth="1" Width="100%">
                <asp:TableRow CssClass="tr-0">
                    <asp:TableCell Width="100px" HorizontalAlign="Left" VerticalAlign="Middle">
                        <asp:Label ID="Label3" Height="17px" Width="90px" runat="server" Text="Atraso  Acima  de:" CssClass="font-default-0" Font-Bold="true"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="96%" HorizontalAlign="Left" VerticalAlign="Middle">
                        <asp:TextBox ID="txtAtraso" Text="0" runat="server" MaxLength="10" Width="50px"></asp:TextBox>&nbsp;
                        <asp:Label ID="Label4" Height="17px" runat="server" Text="Dias" CssClass="font-default-0" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label5" Height="17px" runat="server" Text="Parcelas:" CssClass="font-default-0" Font-Bold="true"></asp:Label>&nbsp;
                        <asp:TextBox ID="txtParcelas" runat="server" MaxLength="10" Width="5%"></asp:TextBox>&nbsp;
                        <asp:Label ID="Label19" Height="17px" runat="server" Text="Vencimento de:" CssClass="font-default-0" Font-Bold="true"></asp:Label>&nbsp;
                        <cc1:Datax ID="txtInicio" runat="server"> </cc1:Datax>
                        <asp:Label ID="Label20" Height="17px" runat="server" Text="Até:" CssClass="font-default-0" Font-Bold="true"></asp:Label>&nbsp;
                        <cc1:Datax ID="txtFim" runat="server"> </cc1:Datax>&nbsp;&nbsp;
                        <asp:TextBox ID="TxtLocalizar" runat="server" Width="20%"></asp:TextBox>
                        <asp:Button ID="BtnLocalizar" Font-Bold="true" Height="22px" runat="server" Text="Localizar" />
                        <asp:Button ID="BtnTodos" Font-Bold="true" Height="22px" runat="server" Text="Todos" />
                        <br />
                        <asp:Label ID="lblMensagem" runat="server" Font-Size="10" ForeColor="Red"></asp:Label>
                    </asp:TableCell>

                </asp:TableRow>
            </asp:Table>
            <asp:Table ID="Table1" runat="server" BorderWidth="1" Width="100%" CellSpacing="1"
                CellPadding="0" HorizontalAlign="Center" GridLines="None">
                <asp:TableRow Height="35px" CssClass="tr-7" BackColor="#73b9b9">
                    <asp:TableCell ColumnSpan="4" Wrap="true" HorizontalAlign="Center">
                        <asp:Label ID="Label2" runat="server" Text="Cobradoras" Font-Bold="true" />
                        <asp:DropDownList ID="ddlCob" Width="11%" runat="server" Visible="True" Enabled="true" AutoPostBack="false" CausesValidation="True"></asp:DropDownList>&nbsp;&nbsp;  
             <asp:Label ID="Label21" runat="server" Text="Região" Font-Bold="true" />
                        <asp:DropDownList ID="ddlRegiao" Width="6%" runat="server" Visible="True" Enabled="true" AutoPostBack="false" CausesValidation="True"></asp:DropDownList>&nbsp;&nbsp;  
             <asp:Label ID="Label6" runat="server" Text="Filial" Font-Bold="true" />
                        <asp:DropDownList ID="ddlFil" Width="10%" runat="server" Visible="True" Enabled="true" AutoPostBack="false" CausesValidation="True"></asp:DropDownList>&nbsp;              
             <asp:Label ID="Label22" runat="server" Text="Gerente" Font-Bold="true" />
                        <asp:DropDownList ID="ddlGer" Width="12%" runat="server" Visible="True" Enabled="true" AutoPostBack="false" CausesValidation="True"></asp:DropDownList>&nbsp;&nbsp;  
             <asp:Label ID="Label23" runat="server" Text="Operador" Font-Bold="true" />
                        <asp:DropDownList ID="ddlOper" Width="25%" runat="server" Visible="True" Enabled="true" AutoPostBack="false" CausesValidation="True"></asp:DropDownList>&nbsp;&nbsp;  
             <asp:Label ID="Label24" runat="server" Text="Lojas" Font-Bold="true" />
                        <asp:DropDownList ID="ddlLojas" Width="11%" runat="server" Visible="True" Enabled="true" AutoPostBack="false" CausesValidation="True"></asp:DropDownList>&nbsp;&nbsp;    
             <asp:Button ID="OK" runat="server" Text="OK" Height="22px" Font-Bold="True" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:Table ID="Table6" runat="server" BorderWidth="1" Width="100%">
                <asp:TableRow Height="20px" BackColor="#73b9b9">
                    <asp:TableCell ColumnSpan="3" HorizontalAlign="left">
                        <asp:Label ID="lblEmpreg" runat="server" Width="6%" Text="Empregadores:" CssClass="font-default-0" Font-Bold="true" />
                        <asp:DropDownList ID="ddlEmpreg" Width="30%" runat="server" Visible="True" AutoPostBack="false" CausesValidation="True"></asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Table ID="Table7" runat="server" BorderWidth="1" Width="100%">
        <asp:TableRow CssClass="tr-0">
            <asp:TableCell Width="10%" HorizontalAlign="center" BackColor="#00aaaa" Font-Bold="true" Font-Underline="false" ForeColor="White" VerticalAlign="Middle">
                <asp:Button ID="btnCdcVei" runat="server" BackColor="#00aaaa" Text="CDC VEÍCULOS" Height="25px" CssClass="item" />
                <asp:Button ID="btnLeasVei" runat="server" BackColor="#00aaaa" Text="LEASING VEÍCULOS" Height="25px" CssClass="item" />
                <asp:Button ID="btnOmni" runat="server" BackColor="#00aaaa" Text="OMNI" Height="25px" CssClass="item" />
                <asp:Button ID="btnLeasCdc" runat="server" BackColor="#00aaaa" Text="LEASING + CDC" Height="25px" CssClass="item" />
                <asp:Button ID="btnConsPriv" runat="server" BackColor="#00aaaa" Text="CONSIGNADO PRIVADO" Height="25px" CssClass="item" />
                <asp:Button ID="btnCdcLj" runat="server" BackColor="#00aaaa" Text="CDC LOJAS E CP" Height="25px" CssClass="item" />
                <asp:Button ID="btnConsPub" runat="server" BackColor="#00aaaa" Text="CONSIGNADO PUBLICO" Height="25px" CssClass="item" />
                <asp:Button ID="btnPubAqui" runat="server" BackColor="#00aaaa" Text="CONS.PUBLICO AQUI." Height="25px" CssClass="item" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>

            <div id="dvConsulta" style="height: 550px; width: 100%; overflow: auto;" align="left">






                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" AllowSorting="true" HeaderStyle-CssClass="font-default-4"
                    AllowPaging="true" PageSize="26" CellPadding="0" CellSpacing="1" Width="4290px" FooterStyle-BackColor="LightCyan" OnSorting="GridView1_Sorting"
                    GridLines="Vertical" ShowHeader="True" ShowFooter="true" HorizontalAlign="Center" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right"
                    BorderStyle="none" EmptyDataText="Data de Referencia Não Encontrada">

                    <HeaderStyle BackColor="#006666" HorizontalAlign="Left" Font-Bold="True"
                        ForeColor="White" BorderColor="#006666" BorderStyle="None" />
                    <FooterStyle CssClass="tr-4a" Height="21px" />
                    <PagerStyle CssClass="GVFixedFooter" Height="34" VerticalAlign="Top" Font-Bold="True" HorizontalAlign="Left" />
                    <RowStyle BackColor="#EFF3FB" Height="16px" HorizontalAlign="Left" ForeColor="#333333" />
                    <AlternatingRowStyle BackColor="White" HorizontalAlign="Left" ForeColor="#284775" />




                    <Columns>
                        <asp:TemplateField HeaderText="OPERAÇÃO" FooterText="Total" FooterStyle-Font-Size="9" FooterStyle-CssClass="font-11" HeaderStyle-CssClass="locked" FooterStyle-HorizontalAlign="Right" SortExpression="PANROPER">
                            <FooterStyle CssClass="locked" />
                            <ItemTemplate>
                            </ItemTemplate>
                            <ItemStyle Width="140px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DT.BASE" SortExpression="OPDTBASE" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="100px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="REGIÃO" SortExpression="REGIAO" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="120px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FILIAL" SortExpression="O1DESCR" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="200px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CLIENTE" SortExpression="CLNOMECLI" FooterStyle-HorizontalAlign="Left" FooterStyle-CssClass="font-11" HeaderStyle-HorizontalAlign="Center">
                            <FooterTemplate><%=sTtl.ToString("#,##0##")%></FooterTemplate>
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="480px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CPF/CNPJ" SortExpression="CLCGC" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="180px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TEL.RES." SortExpression="CLFONEFIS" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="170px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TEL.COM." SortExpression="CLFONECOM" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="170px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="COD.EMPREG." SortExpression="CODEMPREGADOR" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="200px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LOJA" SortExpression="O4NOME" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="500px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PRZ." SortExpression="OPQTDDPARC" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VOP" SortExpression="OPVLRFIN" FooterStyle-CssClass="font-11" HeaderStyle-HorizontalAlign="Center">
                            <FooterTemplate><%=sVop.ToString("N2") %></FooterTemplate>
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="100px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PARC.PG." SortExpression="PAGAS" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="80px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="< PARC." SortExpression="MENOR_PARC" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="100px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="> PARC." SortExpression="MAIOR_PARC" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="100px" HorizontalAlign="Right" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="QTDE. PARC. ABERTO" SortExpression="QTD_ATRASO" FooterStyle-CssClass="font-11" HeaderStyle-HorizontalAlign="Center">
                            <FooterTemplate><%=sParAtr.ToString("#,##0##")%></FooterTemplate>
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="190px" HorizontalAlign="Right" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SALDO EM ATRASO" SortExpression="VLR_ATRASO" FooterStyle-CssClass="font-11" HeaderStyle-HorizontalAlign="Center">
                            <FooterTemplate><%=sVlAtr.ToString("N2")%></FooterTemplate>
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="150px" HorizontalAlign="Right" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PMT1" SortExpression="PMT" FooterStyle-CssClass="font-11" HeaderStyle-HorizontalAlign="Center">
                            <FooterTemplate><%=sPmt.ToString("N2") %></FooterTemplate>
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="100px" HorizontalAlign="Right" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PMT2" SortExpression="PMT2" FooterStyle-CssClass="font-11" HeaderStyle-HorizontalAlign="Center">
                            <FooterTemplate><%=sPmt2.ToString("N2")%></FooterTemplate>
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="100px" HorizontalAlign="Right" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="< VCTO." SortExpression="PADTVCTO" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="100px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="> VCTO." SortExpression="PADTVCTO2" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="100px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="< ATRASO" SortExpression="DIF_DIAS2" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="80px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="> ATRASO" SortExpression="DIF_DIAS" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="80px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="COBRADORA" SortExpression="COBRADORA" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="300px" HorizontalAlign="Right" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DT. ENVIO" SortExpression="DT_ENVIO_COBR" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="100px" HorizontalAlign="Right" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NOVA / ESTOQUE" SortExpression="ENVIO_EST_NOV" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="150px" HorizontalAlign="Right" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SUPERVISOR" SortExpression="O2DESCR" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="300px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="OPERADOR" SortExpression="O3DESCR" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="500px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MODALIDADE" SortExpression="Modalidade" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="300px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ANO/MOD." SortExpression="AbAnoFab" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="100px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MODELO" SortExpression="AbModelo" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="300px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RATING" SortExpression="PDD" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="70px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SALDO" SortExpression="SALDO" FooterStyle-CssClass="font-11" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                            <FooterTemplate><%=sSldo.ToString("N2")%></FooterTemplate>
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="100px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MATRICULA" SortExpression="MATRICULA" FooterStyle-CssClass="font-11" FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="100px" HorizontalAlign="Center" CssClass="font-default-4" />
                        </asp:TemplateField>

                    </Columns>
                    <PagerTemplate>
                        <asp:Button ID="PagerFirst" runat="server" Text="|&lt;&lt;" CausesValidation="false"
                            CommandName="Page" CommandArgument="First" UseSubmitBehavior="false" />&nbsp;
                            <asp:Button ID="PagerPrev" runat="server" Text="&lt;&lt;" CausesValidation="false"
                                CommandName="Page" CommandArgument="Prev" UseSubmitBehavior="false" />&nbsp;
                            <asp:DropDownList ID="PagerPages" runat="server" AutoPostBack="true" CausesValidation="false"
                                OnSelectedIndexChanged="PagerPages_SelectedIndexChanged" />&nbsp;
                            <asp:Button ID="PagerNext" runat="server" Text="&gt;&gt;" CausesValidation="false"
                                CommandName="Page" CommandArgument="Next" UseSubmitBehavior="false" />&nbsp;
                            <asp:Button ID="PagerLast" runat="server" Text="&gt;&gt;|" CausesValidation="false"
                                CommandName="Page" CommandArgument="Last" UseSubmitBehavior="false" />&nbsp;
                    </PagerTemplate>

                </asp:GridView>


                <asp:UpdateProgress runat="server" ID="Progress" AssociatedUpdatePanelID="UpdateManager">
                    <ProgressTemplate>
                        <asp:Table ID="TableProcess" runat="server" Width="99%" CellPadding="0" CellSpacing="0" Height="25px">
                            <asp:TableRow>
                                <asp:TableCell Width="50%" HorizontalAlign="Center">
                                    <asp:Image ID="ImageProcess" runat="server" ImageUrl="~/imagens/animation.gif" />&nbsp;&nbsp;&nbsp
                                        <asp:Label ID="LblProcess" runat="server" Text="Pesquisando... " Font-Italic="true" Font-Bold="true" ForeColor="Red"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </ProgressTemplate>
                </asp:UpdateProgress>

            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Exportar" />
        </Triggers>
    </asp:UpdatePanel>


    <asp:Table ID="Table3" runat="server" BorderWidth="1" Width="100%" CellSpacing="0"
        CellPadding="0">
        <asp:TableRow CssClass="tr-0">
            <asp:TableCell HorizontalAlign="Center">
                <asp:Button ID="Retorna" runat="server" Text="Retorna" Font-Bold="True" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>






