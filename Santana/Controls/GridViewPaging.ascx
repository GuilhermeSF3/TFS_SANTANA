<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="GridViewPaging.ascx.vb" Inherits="Santana.GridViewPaging" %>


<style type="text/css">
    .navigationButton {
        -webkit-box-shadow: rgba(0,0,0,0.2) 0 1px 0 0;
        -moz-box-shadow: rgba(0,0,0,0.2) 0 1px 0 0;
        box-shadow: rgba(0,0,0,0.2) 0 1px 0 0;
        border-bottom-color: #333;
        border: 1px solid #ffffff;
        background-color: #EAF2D3;
        border-radius: 5px;
        -moz-border-radius: 5px;
        -webkit-border-radius: 5px;
        color: #0d76c3;
        font-family: 'Verdana',Arial,sans-serif;
        font-size: 14px;
        text-shadow: #b2e2f5 0 1px 0;
        padding: 5px;
        cursor: pointer;
    }

    .tablePaging {
        font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
        width: 100%;
        border-collapse: collapse;
    }

        .tablePaging td {
            font-size: 1em;
            border: 1px solid #ffffff;
            padding: 3px 7px 2px 7px;
            background-color: #b1dbfa;
            font-size: 10pt;
        }
</style>





<table class="tablePaging">
    <tr>
        <td style="width: 15%; text-align: center;">
            <asp:Label ID="lblPageSize" runat="server" Text="Page Size: "></asp:Label>
            <asp:DropDownList ID="PageRowSize" runat="server">
                <asp:ListItem Selected="True">10</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
                <asp:ListItem>100</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td style="width: 25%; text-align: center;">
            <asp:Label ID="RecordDisplaySummary" runat="server"></asp:Label></td>
        <td style="width: 20%; text-align: center;">
            <asp:Label ID="PageDisplaySummary" runat="server"></asp:Label></td>
        <td style="width: 40%; text-align: center;">
            <asp:Button ID="First" runat="server" Text="<<" Width="45px" OnClick="First_Click" CssClass="navigationButton" />&nbsp;
     <asp:Button ID="Previous" runat="server" Text="<" Width="45px" OnClick="Previous_Click" CssClass="navigationButton" />&nbsp;
     <asp:TextBox ID="SelectedPageNo" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="Large" OnTextChanged="SelectedPageNo_TextChanged" Width="100px" AutoPostBack="True" MaxLength="8"></asp:TextBox>&nbsp;
     <asp:Button ID="Next" runat="server" Text=">" Width="45px" OnClick="Next_Click" CssClass="navigationButton" />&nbsp;
     <asp:Button ID="Last" runat="server" Text=">>" Width="45px" OnClick="Last_Click" CssClass="navigationButton" />&nbsp; 
        </td>
    </tr>
    <tr id="trErrorMessage" runat="server" visible="false">
        <td colspan="4" style="background-color: #e9e1e1;">
            <asp:Label ID="GridViewPagingError" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="Red"></asp:Label>
            <asp:HiddenField ID="TotalRows" runat="server" Value="0" />
        </td>
    </tr>
</table>

