<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" EnableEventValidation="false" AutoEventWireup="false" CodeFile="tgrafico.aspx.vb" Inherits="tgrafico" title="Untitled Page" %>


<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>



<%--<%@ Page Language="VB" MasterPageFile="~/MasterPage.master"  AutoEventWireup="false" CodeFile="tempregador.aspx.vb" Inherits="tempregador" %>


--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<%--<head runat="server">--%>


     <style type="text/css">

        .style2

        {

            width: 449px;

            height: 230px;

        }

        .style3

        {

            height: 230px;

        }

    </style>

<%--</head>--%>




<%--<asp:Table ID="Table1" runat="server" BorderWidth="1" Width="100%" CellSpacing="1"
        CellPadding="0" HorizontalAlign="Center">
        <asp:TableRow Height="20px" CssClass="tr-7">
            <asp:TableCell ColumnSpan="4" HorizontalAlign="Center">
                <asp:Label ID="Label1" runat="server" Text="Cadastro SIV Empregadores" CssClass="font-default-6"
                    Font-Bold="true" />
            </asp:TableCell></asp:TableRow></asp:Table><asp:ScriptManager ID="SManager" runat="server">
</asp:ScriptManager>
    
<asp:UpdatePanel ID="UpdateManager" runat="server">
<contenttemplate>

<asp:Table ID="Table4" runat="server" BorderWidth="1" Width="100%">
  <asp:TableRow CssClass="tr-0">

    <asp:TableCell Width="4%" VerticalAlign="Middle" HorizontalAlign="Left">     
          <asp:ImageButton ID="Exportar" runat="server" Visible="false" Height="30px" Width="30px" ImageUrl="~/imagens/excel1.bmp" ToolTip="Exportar MS-Excel" /> 
    </asp:TableCell><asp:TableCell Width="4%" VerticalAlign="Middle" HorizontalAlign="Left">
         <asp:HyperLink ID="HyperLinkImprime" visible="true" ImageUrl="~/imagens/16_print.gif" runat="server"
         ToolTip="Imprimir" />
    </asp:TableCell><asp:TableCell
     ToolTip="Cadastro de Empregador de todos os produtos. Produto 1-Publico, 2-Privado, 3- Veiculos."
 Width="4%" VerticalAlign="Middle" HorizontalAlign="Left">
    <asp:Image ID="imgHelp" runat="server"   ImageUrl="~/imagens/Help3.jpg"/>      
    </asp:TableCell></asp:TableRow></asp:Table>
    --%>
    
    
    <%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
--%>


<html xmlns="http://www.w3.org/1999/xhtml">
    
    
    
    
<%--  versao anterior
    
      <head><title>Visifire Charts</title><script type="text/javascript" src="Visifire.js"></script>
    <script type="text/javascript">

        function onMouseLeftButtonDown(e)
        {
            var info;
            info = e.Element + ' Clicked.';
            info += 'YValue : ' + e.YValue;
            alert(info);
        }

    </script></head>--%>


    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
<%--    <body>--%>



        
        <div id="VisifireChart1">
    <%--<script language="javascript" type="text/javascript"></script>  Visifire/--%>

        <script type="text/javascript" src="Visifire.js">

    </script>

 

    <script type="text/javascript">

        var vChart = new Visifire("SL.Visifire.Charts.xap", 500, 300);


//        var vChart = new Visifire("SL.Visifire.Charts.xap",500,300);
        vChart.setDataUri("Data.xml"); // xml file name goes in the place of Data.xml
        //vChart.attachEvent('DataPoint','MouseLeftButtonDown', onMouseLeftButtonDown);
        vChart.render("VisifireChart1");

    </script>
    </div>
</body>
</html>
</asp:Content>

<%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:LEG_FUNConnectionString %>" 
        SelectCommand="SELECT [EmCodProduto], [EmCod], [EmDescr], [EmAtivo], [EmDescrOrgao], [EmDescrGrupo] FROM [TEmpregador]" 
        UpdateCommand="update [TEmpregador]
Set  [EmDescr]=@EmDescr, [EmDescrOrgao]=@EmDescrOrgao, [EmDescrGrupo]=@EmDescrGrupo
where  [EmCodProduto]=@EmCodProduto and   [EmCod]=@EmCod">
        <UpdateParameters>
            <asp:ControlParameter ControlID="GridView1" Name="EmDescr" 
                PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="GridView1" Name="EmDescrOrgao" 
                PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="GridView1" Name="EmDescrGrupo" 
                PropertyName="SelectedValue" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" AutoGenerateEditButton="True" 
        DataSourceID="SqlDataSource1" CellPadding="4" 
        DataKeyNames="EmCodProduto,EmCod" ForeColor="#333333" GridLines="None">
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#E3EAEB" />
        <Columns>
            <asp:BoundField DataField="EmCodProduto" HeaderText="Cod.Produto" 
                SortExpression="EmCodProduto"  ReadOnly="True" ControlStyle-Width="100%" />
            <asp:BoundField DataField="EmCod" HeaderText="Codigo" SortExpression="EmCod" ReadOnly="True" />
            <asp:BoundField DataField="EmDescr" HeaderText="Empregador" 
                SortExpression="EmDescr" ItemStyle-Width="35%" ControlStyle-Width="100%" />
            <asp:CheckBoxField DataField="EmAtivo" HeaderText="Ativo" 
                SortExpression="EmAtivo" ItemStyle-Width="5%" />
            <asp:BoundField DataField="EmDescrOrgao" HeaderText="Descrição SIV VOP" 
                SortExpression="EmDescrOrgao" ItemStyle-Width="30%"  ControlStyle-Width="100%"  />
            <asp:BoundField DataField="EmDescrGrupo" HeaderText="Grupo" 
                SortExpression="EmDescrGrupo" ItemStyle-Width="25%" ControlStyle-Width="100%" />
         </Columns>
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#33CCCC" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#009999" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
--%>


<%--    </form>
</body>
</html>

</contenttemplate>

    <triggers>
        <asp:postbacktrigger controlid="Exportar" />
    </triggers> 

</asp:UpdatePanel>
        
 <asp:Table ID="Table3" runat="server" BorderWidth="1" Width="100%" CellSpacing="0"
            CellPadding="0">
            <asp:TableRow CssClass="tr-0">
                <asp:TableCell  HorizontalAlign="Center">
                    <asp:Button ID="Retorna" runat="server" Text="Retorna"  Font-Bold="True" />
                </asp:TableCell></asp:TableRow></asp:Table></asp:Content>
--%>
