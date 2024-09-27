<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SantanaWeb.Master" CodeBehind="Grafico.aspx.vb" Inherits="Santana.Grafico" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <script type="text/javascript" src="../visifire/Visifire.js"></script>
    <script type="text/javascript">
        var vChart = new Visifire("../visifire/SL.Visifire.Charts.xap", 500, 300);
    </script>

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


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 850px">
    
        <table style="width: 90%;">
            <tr>
                <td class="style2">
                    <div id="VisifireChart1"                         
                        style="height: 300px; width: 500px; font-family: Arial, Helvetica, sans-serif; font-size: 24px;">
                        Check several options to get the chart...
                        <br />
                        Also read &quot;HowToRun.txt&quot;<br />
                    </div>
                </td>
                <td class="style3" align="top">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <br />
                        <br />
                        <br />
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:CheckBox ID="chkInappropriate" runat="server" AutoPostBack="True" 
                                        checked="false" Font-Size="12px" 
                                        oncheckedchanged="chkInappropriate_CheckedChanged" text="Inappropriate Mails" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" 
                                    style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;">
                                    Select Chart Type :</td>
                                <td>
                                    <asp:RadioButton ID="radioButColumn" GroupName="GroupRednerAs" runat="server" Font-Size="12px" 
                                        Text="Column Chart" oncheckedchanged="RadioButton1_CheckedChanged" 
                                        AutoPostBack="True" Checked="True" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:RadioButton ID="radioButBar" GroupName="GroupRednerAs" runat="server" Font-Size="12px" 
                                        Text="Bar Chart" oncheckedchanged="RadioButton2_CheckedChanged" 
                                        AutoPostBack="True" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" 
                                    style="font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;">
                                    Select Theme :</td>
                                <td>
                                    <asp:DropDownList ID="drpTheme" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="drpTheme_SelectedIndexChanged">
                                        <asp:ListItem>Theme1</asp:ListItem>
                                        <asp:ListItem>Theme2</asp:ListItem>
                                        <asp:ListItem Selected="True">Theme3</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                   </asp:UpdatePanel>
                </td>
            </tr>
          </table>    
    </div>
</asp:Content>
