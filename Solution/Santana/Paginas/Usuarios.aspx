<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" EnableEventValidation="false" AutoEventWireup="false" CodeBehind="Usuarios.aspx.vb" Inherits="Santana.Tabelas_Usuarios" Title="Santana Usuários" %>



<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:Table ID="Table1" runat="server" BorderWidth="1" Width="100%" CellSpacing="1"
        CellPadding="0" HorizontalAlign="Center">
        <asp:TableRow Height="20px" CssClass="tr-7">
            <asp:TableCell ColumnSpan="4" HorizontalAlign="Center">
                <asp:Label ID="Label1" runat="server" Text="Cadastro de Usuários SIV" CssClass="font-default-6"
                    Font-Bold="true" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:ScriptManager ID="SManager" runat="server">
    </asp:ScriptManager>
    <asp:Button ID="Button1" runat="server" Text="Button" />

    <asp:UpdatePanel ID="UpdateManager" runat="server">
        <ContentTemplate>

            <asp:Table ID="Table4" runat="server" BorderWidth="1" Width="100%">
                <asp:TableRow CssClass="tr-0">
                    <asp:TableCell Width="80%" VerticalAlign="Middle" HorizontalAlign="Left">
                        <asp:TextBox ID="TxtLocalizar" runat="server" Width="220px" Text="%%"></asp:TextBox>
                        <asp:Button ID="BtnLocalizar" runat="server" Text="Localizar" />
                        <asp:Button ID="BtnTodos" runat="server" Text="Todos" />&nbsp;&nbsp;
                        <asp:HiddenField ID="hdLogin" runat="server" />
                        <asp:Label ID="lblMensagem" runat="server" Font-Size="10" ForeColor="Red"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="4%" VerticalAlign="Middle" HorizontalAlign="Left">
                        <asp:ImageButton ID="Exportar" runat="server" Visible="false" Height="30px" Width="30px" ImageUrl="~/imagens/excel1.bmp" ToolTip="Exportar MS-Excel" />
                    </asp:TableCell>
                    <asp:TableCell Width="4%" VerticalAlign="Middle" HorizontalAlign="Left">
                        <asp:HyperLink ID="HyperLinkImprime" Visible="true" ImageUrl="~/imagens/16_print.gif" runat="server"
                            ToolTip="Imprimir" />
                    </asp:TableCell>
                    <asp:TableCell
                        ToolTip="Cadastro de Usuários SIV. Só o Perfil maior ou igual a 5 permite copiar os dados em Excel. Perfil 10 permite fazer manutenção nas Tabelas de Cobrança. Perfil 11 nas tabelas de produtos. Função pode ser: DIR, GER, ADM, ASS. Perfil = 1  Operador. Produto: 0= Todos 1= Consignado Publico 2= Consignado Prícvado 3= Veículos 4= CDC Lojas e CP."
                        Width="4%" VerticalAlign="Middle" HorizontalAlign="Left">
                        <asp:Image ID="imgHelp" runat="server" ImageUrl="~/imagens/Help3.jpg" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <div id="dvConsulta" style="height: 100%; width: 100%; overflow: auto;" align="left">
                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                    ConnectionString="<%$ ConnectionStrings:LEG_FUNConnectionString %>"
                    SelectCommand="SELECT * FROM [Usuario] where [Login] like '%'+@nome+'%' or [nomeUsuario] like '%'+@nome+'%' or [email] like '%'+@nome+'%' or [funcao] like '%'+@nome+'%' "
                    UpdateCommand="update [Usuario] Set  [nomeUsuario]=@nomeUsuario, [email]=@email, [ativo]=@ativo, [perfil]=@perfil, [codfilial]=@codfilial, [funcao]=@funcao, [codgerente]=@codgerente, [produto]=@produto  where  [login]=@login ">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtLocalizar" Name="nome"
                            PropertyName="text" />
                    </SelectParameters>

                    <UpdateParameters>
                        <asp:ControlParameter ControlID="GridView1" Name="nomeUsuario"
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="GridView1" Name="email"
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="GridView1" Name="ativo"
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="GridView1" Name="perfil"
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="GridView1" Name="login"
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="GridView1" Name="codfilial"
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="GridView1" Name="funcao"
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="GridView1" Name="codgerente"
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="GridView1" Name="produto"
                            PropertyName="SelectedValue" />
                    </UpdateParameters>

                </asp:SqlDataSource>




                <asp:GridView ID="GridView1" runat="server" Width="1250px" HeaderStyle-HorizontalAlign="Left" AllowPaging="True"
                    AllowSorting="True" AutoGenerateColumns="False" AutoGenerateEditButton="True"
                    AutoGenerateSelectButton="True" PageSize="25"
                    DataSourceID="SqlDataSource1" CellPadding="4"
                    DataKeyNames="login" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#E3EAEB" />
                    <Columns>
                        <asp:BoundField DataField="login" ReadOnly="True" HeaderStyle-HorizontalAlign="Center" HeaderText="Login" ItemStyle-HorizontalAlign="Center"
                            SortExpression="login" ItemStyle-Width="180px" ControlStyle-Width="200px" />
                        <asp:BoundField DataField="NomeUsuario" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="200px" ControlStyle-Width="200px" HeaderText="Nome do Usuário"
                            SortExpression="NomeUsuario" />
                        <asp:BoundField DataField="email" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="200px" ControlStyle-Width="200px" HeaderText="eMail"
                            SortExpression="email" />
                        <asp:BoundField DataField="Ativo" HeaderText="Ativo"
                            SortExpression="Ativo" ItemStyle-Width="80px" />
                        <asp:BoundField DataField="perfil" HeaderStyle-HorizontalAlign="Left" HeaderText="Perfil"
                            SortExpression="perfil" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left" ControlStyle-Width="100px" />
                        <asp:BoundField DataField="codfilial" HeaderStyle-HorizontalAlign="Left" HeaderText="Cod.Filial"
                            SortExpression="codfilial" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left" ControlStyle-Width="100px" />
                        <asp:BoundField DataField="funcao" HeaderStyle-HorizontalAlign="Left" HeaderText="Função"
                            SortExpression="funcao" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left" ControlStyle-Width="100px" />
                        <asp:BoundField DataField="codgerente" HeaderStyle-HorizontalAlign="Left" HeaderText="Cod.Gerente"
                            SortExpression="codgerente" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left" ControlStyle-Width="100px" />
                        <asp:BoundField DataField="produto" HeaderStyle-HorizontalAlign="Left" HeaderText="Produto"
                            SortExpression="produto" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left" ControlStyle-Width="100px" />
                        <asp:TemplateField HeaderText="Alterar Senha">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" ForeColor="#3a7676" OnClientClick="javascript:return ConfirmaEdit()" OnClick="btnEdit_Click" Text="Alterar Senha" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#33CCCC" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#009999" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <br />

                <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                    ConflictDetection="CompareAllValues"
                    ConnectionString="<%$ ConnectionStrings:LEG_FUNConnectionString %>"
                    InsertCommand="INSERT INTO USUARIO(Login, NomeUsuario, Cpf, EMail, Ativo, NomeCompleto, senha, perfil,  Funcao, CodGerente, CodFilial, Produto) VALUES (@Login, @NomeUsuario,'1', @EMail, @Ativo, @NomeUsuario,'zgv9FQWbaNZ2iIhNej0+jA==', @perfil, @funcao,  @codgerente, @codfilial, @Produto)"
                    SelectCommand="SELECT [Login], [NomeUsuario], [Cpf], [EMail], [Ativo], [NomeCompleto], [senha], [perfil], [Funcao], [CodGerente], [CodFilial], [Produto] FROM [Usuario] where [Login]=@login"
                    DeleteCommand="DELETE FROM [Usuario] where [Login]=@login">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="GridView1" Name="login"
                            PropertyName="SelectedValue" Type="String" />

                    </SelectParameters>

                    <InsertParameters>
                        <asp:Parameter Name="login" Type="String" />
                        <asp:Parameter Name="NomeUsuario" Type="String" />
                        <asp:Parameter Name="email" Type="String" />
                        <asp:Parameter Name="ativo" Type="Int32" />
                        <asp:Parameter Name="perfil" Type="String" />
                        <asp:Parameter Name="codfilial" Type="String" />
                        <asp:Parameter Name="funcao" Type="String" />
                        <asp:Parameter Name="codgerente" Type="String" />
                    </InsertParameters>
                </asp:SqlDataSource>


                <asp:DetailsView ID="DetailsView1" runat="server"
                    DataKeyNames="login" CellPadding="4"
                    DataSourceID="SqlDataSource2" ForeColor="#333333" GridLines="None"
                    Height="50px" Width="491px" AutoGenerateRows="False"
                    AutoGenerateDeleteButton="True" OnItemDeleted="DetailsView1_ItemDeleted"
                    AutoGenerateInsertButton="True" OnItemInserted="DetailsView1_ItemInserted"
                    AutoGenerateSelectButton="true">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <Fields>
                        <asp:BoundField DataField="login" HeaderStyle-HorizontalAlign="Left" HeaderText="Login"
                            ItemStyle-Width="100%" ControlStyle-Width="100%">
                            <ControlStyle Width="100%" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="100%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NomeUsuario" HeaderStyle-Width="30%"
                            ControlStyle-Width="100%" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-Width="100%" HeaderText="Nome do Usuário"
                            SortExpression="NomeUsuario">
                            <ControlStyle Width="100%" />
                            <HeaderStyle HorizontalAlign="Left" Width="30%" />
                            <ItemStyle Width="100%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="email" HeaderStyle-HorizontalAlign="Left"
                            ControlStyle-Width="100%" ItemStyle-Width="100%" HeaderText="eMail"
                            SortExpression="email">
                            <ControlStyle Width="100%" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="100%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Ativo" ControlStyle-Width="30%" HeaderText="Ativo"
                            SortExpression="Ativo" ItemStyle-Width="30%">
                            <ControlStyle Width="30%" />
                            <ItemStyle Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="perfil" HeaderStyle-HorizontalAlign="Left" HeaderText="Perfil"
                            SortExpression="perfil" ItemStyle-Width="5%"
                            ItemStyle-HorizontalAlign="Left" ControlStyle-Width="30%">
                            <ControlStyle Width="30%" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="codfilial" HeaderStyle-HorizontalAlign="Left" HeaderText="Cod.Filial"
                            SortExpression="codfilial" ItemStyle-Width="5%"
                            ItemStyle-HorizontalAlign="Left" ControlStyle-Width="30%">
                            <ControlStyle Width="30%" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="funcao" HeaderStyle-HorizontalAlign="Left" HeaderText="Função"
                            SortExpression="funcao" ItemStyle-Width="5%"
                            ItemStyle-HorizontalAlign="Left" ControlStyle-Width="30%">
                            <ControlStyle Width="30%" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="codgerente" HeaderStyle-HorizontalAlign="Left" HeaderText="Cod.Gerente"
                            SortExpression="codgerente" ItemStyle-Width="5%"
                            ItemStyle-HorizontalAlign="Left" ControlStyle-Width="30%">
                            <ControlStyle Width="30%" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="produto" HeaderStyle-HorizontalAlign="Left" HeaderText="Produto"
                            SortExpression="produto" ItemStyle-Width="5%"
                            ItemStyle-HorizontalAlign="Left" ControlStyle-Width="30%">
                            <ControlStyle Width="30%" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                        </asp:BoundField>
                    </Fields>
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"
                        Width="50%" />
                    <EditRowStyle BackColor="#999999" Width="50%" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:DetailsView>
                <asp:Label ID="lblMensagem2" runat="server" Font-Size="10" ForeColor="Red"></asp:Label>
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
    <script language="javascript" type="text/javascript">
        function ConfirmaEdit() {
            return confirm('Deseja realmente alterar esta senha?');
        }
    </script>
</asp:Content>
