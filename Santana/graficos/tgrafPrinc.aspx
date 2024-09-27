<%@ Page Language="VB"  EnableEventValidation="false" AutoEventWireup="false" CodeFile="tgrafprinc.aspx.vb" Inherits="tgrafprinc" title="Untitled Page" %>
<head>
    <link rel="stylesheet" href="css/Main.css" />
    <title>SOFISA/SUPERBANK/MIS - 2008</title>

    <script type="text/javascript" src="Visifire.js"></script>
    <script type="text/javascript">
// Returns XmlHttp object

function GetXMLHttpObj()

{

var objXmlHttp; // XMLHttpRequest object

 

try

{ 

// Firefox, Opera 8.0+, Safari

objXmlHttp = new XMLHttpRequest();

}

catch (e)

{

// Internet Explorer

try

{

objXmlHttp = new ActiveXObject("Msxml2.XMLHTTP");

}

catch (e)

{

try

{

objXmlHttp = new ActiveXObject("Microsoft.XMLHTTP");

}

catch (e)

{

alert("Your browser does not support AJAX!");

return null;

}

} 

}

 

return objXmlHttp;

}

// Loads Visifire Chart
function onLoad2()
{
var xmlHttp = GetXMLHttpObj();

xmlHttp.onreadystatechange = function()
{
if( xmlHttp.readyState == 4 )
{

/* The following 3 lines of codes are enough to create a simple chart  */
/* Create a new chart object. Arguments are Visifire.xap uri, width, height */
var vChart = new Visifire("Visifire.xap",1200,400); 
 
/* Set the data XAML where response text contains the Data xaml is passed as argument */
vChart.setDataXml(xmlHttp.responseText); 

/* Render the chart, target div element id is passed as argument */
vChart.render("Visifire1");
}
}

// Sending request
xmlHttp.open("GET", "tgrafPrinc.aspx" + "?action=GetXML", true);
xmlHttp.send(null);
}

        function onMouseLeftButtonDown(e)
        {
            var info;
            info = e.Element + ' Clicked.';
            info += 'YValue : ' + e.YValue;
            alert(info);
        } 
        </script>

<body>
    <form id="form1" runat="server">

    <asp:Table ID="Table2" runat="server" Width="100%" CellPadding="0" 
        CellSpacing="0" BackColor="White">
        <asp:TableRow CssClass="tr-7">
            <asp:TableCell Width="98%" HorizontalAlign="Left" >
                <asp:Image ID="Image1" runat="server" ImageUrl="imagens/sofisa5.bmp" />
            </asp:TableCell>
     
           <asp:TableCell Width="1%" HorizontalAlign="Right">
                <a href="javascript:window.close()" class="menu_h">[X] Sair</a>
            </asp:TableCell>
            <asp:TableCell Width="1%">&nbsp;</asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <div >
         <asp:Table ID="Table3" runat="server" BorderWidth="1" Width="100%" CellSpacing="0"
            CellPadding="0" BorderColor="#003300" BorderStyle="Solid">
            <asp:TableRow CssClass="tr-9">      
                <asp:TableCell ColumnSpan="6" HorizontalAlign="Right">                                                                  
                     <asp:HyperLink ID="HyperLink1" visible="true" ImageUrl="~/imagens/lock.bmp" runat="server"
                        ToolTip="Ambiente Seguro" />&nbsp;
                    <asp:Label ID="Label35" runat="server" Text="USUÁRIO: "
                        CssClass="font-default-5" Font-Bold="true" Font-Italic="false" Height="16px" Font-Size= "X-Small" ForeColor="BLACK" />
                    <asp:Label ID="LblUsu" Text="x" runat="server" CssClass="font-default-5" Font-Bold="true" Font-Italic="false" Height="16px" Font-Size= "X-Small" ForeColor="BLACK"/>        
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

</div>
    <br />
    <asp:Table ID="Table5" runat="server" BorderWidth="1" Width="100%">
        <asp:TableRow CssClass="tr-0">
            <asp:TableCell BackColor="#999966" Font-Bold="true" Font-Underline="false" 
                ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="10%">
            <asp:Menu ID="tabMenu" runat="server" OnMenuItemClick="tabMenu_MenuItemClick" 
                Orientation="Horizontal">
                <Items>
                    <asp:MenuItem Text="CDC VEÍCULOS" Value="t1" />
                    <asp:MenuItem Text="LEASING VEÍCULOS" Value="t2" />
                    <asp:MenuItem Text="TOT. VEÍCULOS" Value="t3" />
                    <asp:MenuItem Text="CONS. PUB." Value="t4" />
                    <asp:MenuItem Text="CESSÕES" Value="t5" />
                    <asp:MenuItem Text="CONS. PRIV. + CDC lOJAS + CP CH." Value="t6" />
                    <asp:MenuItem Text="MASSIFICADO" Value="t7" />
                </Items>
            </asp:Menu>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
      
    <asp:MultiView ID="MultiView1" runat="server">

<%-- <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" OnActiveViewChanged="MultiView1_ActiveViewChanged">
--%>
        <asp:View ID="View1" runat="server"> 
        <div id="ds">             
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:LEG_FUNConnectionString %>" 
                SelectCommand="SELECT [EmCodProduto], [EmCod], [EmDescr], [EmAtivo], [EmDescrOrgao], [EmDescrGrupo] FROM [TEmpregador]" 
                ProviderName="<%$ ConnectionStrings:LEG_FUNConnectionString.ProviderName %>">
            </asp:SqlDataSource>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" AllowSorting="true"
            DataSourceID="SqlDataSource1" 
                        AllowPaging="true" PageSize="3" CellPadding="0" CellSpacing="1" Width="99%"               
                        GridLines="None" ShowHeader="true" ShowFooter="true" HorizontalAlign="Center"
                        BorderStyle="none" 
                        EmptyDataText="Data de Referencia Não Encontrada" 
                        EnableSortingAndPagingCallbacks="True">
            <Columns>
                    <asp:BoundField DataField="EmCodProduto" HeaderText="Cod.Produto" 
                        SortExpression="EmCodProduto"  ReadOnly="True" ControlStyle-Width="100%" />
                    <asp:BoundField DataField="EmCodProduto" HeaderText="Cod.Produto" 
                        SortExpression="EmCodProduto"  ReadOnly="True" ControlStyle-Width="100%" />
             </Columns>
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#33CCCC" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#009999" />
        <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        </div>      
        <br>
            <div id="VisifireChart1">

            <script language="javascript" type="text/javascript">
var xmlHttp = GetXMLHttpObj();
            </script >
           
  <%          
      Dim chartTitle As String = "Infant Mortality Rate"          ' Main title for chart
      Dim chartSubTitle = "(Global Survey)"                       ' Sub title for chart
      Dim axisXtitle = "Year"                                     ' X axis title
      Dim axisYtitle = "IMR Rate"                                 ' Y axis title
      Dim myXAML As String                                        ' String for Data xaml
      Dim numberOfDataPoints As Integer = 6                       ' Number of data point in the DataSeries
      ' A DataSeries is a two dimensional array of DataPoints
      '                              Year | IMR Rate
      Dim dataSeries(,) As String = {{"2000", "52.6"}, {"2001", "40.3"}, {"2002", "20"}, {"2003", "28.7"}, {"2004", "46.1"}, {"2005", "15.1"}}
      ' Constructing Data XAML 
      myXAML = "<vc:Chart Theme=""Theme2"" Width=""600"" Height=""400"" xmlns:vc=""clr-namespace:Visifire.Charts;assembly=Visifire.Charts"">" + vbNewLine + vbNewLine
      myXAML = myXAML + "<vc:Title Text=""" & chartTitle & """/>" + vbNewLine
      myXAML = myXAML + "<vc:Title Text=""" & chartSubTitle & """/>" + vbNewLine
      myXAML = myXAML + "<vc:AxisX Title=""" & axisXtitle & """/>" + vbNewLine
      myXAML = myXAML + "<vc:AxisY Title=""" & axisYtitle & """ ValueFormatString=""#0.##'%'""/>" + vbNewLine + vbNewLine
      myXAML = myXAML + "<vc:DataSeries RenderAs=""Column"">" + vbNewLine

      'Constructing XAML fragment for DataSeries 

      For dataPointIndex As Integer = 0 To numberOfDataPoints - 1
          ' Adding DataPoint XAML fragment
          myXAML = myXAML + "<vc:DataPoint AxisLabel=""" & dataSeries(dataPointIndex, 0) & """ YValue=""" & dataSeries(dataPointIndex, 1) & """/>" + vbNewLine
      Next
      myXAML = myXAML + vbNewLine + "</vc:DataSeries>" + vbNewLine
      myXAML = myXAML + "</vc:Chart>"
      'LblUsu.Text = myXAML
      Session.Add("myXML", myXAML)

      ' Clear all response text
      'Response.Clear()
      ' Write data xaml as response text
      'Response.Write(myXAML)

%>
            <script language="javascript" type="text/javascript">
/*xmlHttp.onreadystatechange = function()
{
if( xmlHttp.readyState == 4 )
{*/
                var vChart = new Visifire("Visifire.xap",500,300);
                //  funcao para criar XML com dados do banco
                vChart.setDataUri("Data.xml"); // xml file name goes in the place of Data.xml
/* Set the data XAML where response text contains the Data xaml is passed as argument */
//vChart.setDataXml(xmlHttp.responseText); 
//vChart.setDataXml(myXAML); 
                vChart.attachEvent('DataPoint','MouseLeftButtonDown', onMouseLeftButtonDown);
                vChart.render("VisifireChart1");

           </script>
        </div>      
       </asp:View>

       <asp:View ID="View2" runat="server">
            <div id="Div1">
           </script>       
            </div>
       </asp:View>

       <asp:View ID="View3" runat="server">
       </asp:View>
       
       <asp:View ID="View4" runat="server">
       
       </asp:View>
       <asp:View ID="View5" runat="server">
       
       </asp:View>
       <asp:View ID="View6" runat="server">
       
       </asp:View>               
       <asp:View ID="View7" runat="server">
       
       </asp:View>       
     
       </asp:MultiView>
    
    </asp:MultiView>


 <%--   <triggers>
        <asp:postbacktrigger controlid="Exportar" />
    </triggers> --%>
        
 <asp:Table ID="Table4" runat="server" BorderWidth="1" Width="100%" CellSpacing="0"
            CellPadding="0">
            <asp:TableRow CssClass="tr-0">
                <asp:TableCell  HorizontalAlign="Center">
                    <asp:Button ID="Retorna" runat="server" Text="Retorna"  Font-Bold="True" /> 
                </asp:TableCell>
            </asp:TableRow>
 </asp:Table>       

<div>
         <asp:Table ID="Table1" runat="server" BorderWidth="1" Width="100%" CellSpacing="0" 
            CellPadding="0">
            <asp:TableRow CssClass="tr-9">
                <asp:TableCell ColumnSpan="6" HorizontalAlign="Right" BackColor="#999966" >
                    <asp:Label ID="Label1" runat="server" Text="© Copyright 2008 - SUPERBANK FINANCEIRA S.A. - CONFIDENCIAL - USO EXCLUSIVO DO BANCO SOFISA S.A."
                        CssClass="font-default-5" Font-Bold="true" Font-Italic="true" Height="16px" Font-Size=  "X-Small" ForeColor="blue" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table> 
</div>      

    </form>
    </body>
</html>

<%--


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
--%>
<%--
<html xmlns="http://www.w3.org/1999/xhtml">
<%@ Page Language="VB" MasterPageFile="~/MasterPage.master"  AutoEventWireup="false" CodeFile="tempregador.aspx.vb" Inherits="tempregador" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
--%>