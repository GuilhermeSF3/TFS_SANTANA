<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Logon.aspx.vb" Inherits="Santana.Logon" Title="Santana Logon" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Santana Logon</title>
    <link rel="stylesheet" href="../css/Style.css" />
    <link rel="stylesheet" href="../Content/bootstrap.css" />


    <script type="text/javascript" src="../Scripts/jquery-2.1.0.min.js"></script> 
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.bootstrap-growl.js"></script>
    <script type="text/javascript" src="../Scripts/bootstrap-growl.js"></script>

</head>


<script type="text/javascript">

    var request;
    var response;
    var erro;
    var usuario;
    var tipo;
    var mensagem;
    var senhanova;

    function alterarSenha() {

        usuario = document.getElementById("usuario").value;

        if (usuario != '') {
            var senhanova = window.showModalDialog("AlteraSenha.aspx?tipo=" + tipo + "&usuario=" + usuario, "", "dialogHeight: 380px; dialogWidth: 540px; edge: Raised; center: Yes; resizable: Yes; status: Yes;");
        } else {
            document.getElementById("usuario").value = '';
        }

        document.getElementById("senha").value = senhanova;
        document.getElementById("alterar").value = tipo;
        document.getElementById("senha").focus();
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


<body>

    <div style="height: 200px">
        <table border="0" cellpadding="0" cellspacing="0" style="height: 112px; width: 100%">
            <tbody>
                <tr class="tr-1-r1-c1">
                    <td class="td-1-r1-c1" colspan="3">&nbsp; &nbsp;
                    </td>
                </tr>
                <tr>
                    <td  class ="td-1-r2-c1" style="width:387px; height:88px;">
                        <div style="width:387px; height:88px;"></div>
                    </td>

                    <td class="td-1-r2-c2" style="width:100%; height:88px">
                    </td>

                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
            </tbody>

        </table>
    </div>

    <form id="form1" action="Logon.aspx" runat="server">

         <asp:Table ID="Table2" runat="server" Width="100%" CellPadding="0" CellSpacing="100" Height="400px">
            <asp:TableRow ID="TableRow2" runat="server" HorizontalAlign="Center">
                <asp:TableCell ID="TableCell1" Width="100%" Height="100%" runat="server" VerticalAlign="Top">
                    <asp:Table runat="server" ID="Table3" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Middle" >

                              <asp:Table ID="Table4" runat="server" CellPadding="0" CellSpacing="0" BorderStyle="solid"
                                    BorderColor="#636363" BorderWidth="0" HorizontalAlign="Center">

                                        <asp:TableRow>
                                            <asp:TableCell>
                                                  <div style="height:360px; width:360px" class="panel panel-default" >
                                                      <div class="panel-heading"><h3 class="panel-title">Seja Bem-Vindo(a)</h3></div>
                                                      <div class="panel-body">
                                                          <h4 class="page-header pa text-left">Efetue seu login</h4>
                                                          <div class="control-group">
                                                                <h5 class="text-left">Usuário</h5>
                                                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                                          </div>
                                                          <div class="control-group">
                                                              <div><h5 class="text-left">Senha&nbsp;&nbsp;&nbsp;&nbsp;<small><asp:LinkButton ID="txtAlterarSenha" runat="server" Text="Alterar senha" CssClass="text-left" OnClick="txtAlterarSenha_Click" ></asp:LinkButton></small></h5>   </div> 
                                                              <asp:TextBox ID="txtSenha" runat="server" CssClass="form-control" Width="300px" TextMode="Password"></asp:TextBox>
                                                          </div> 
                                                          
                                                          <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-success navbar-btn btn-lg" OnClick="btnLogin_Click" Height="50px" ></asp:Button>
                                                            
                                                          
                                                      </div>
                                                  </div> 
                                                  <%--<div id="divMensagem" runat="server" class="alert alert-warning" style="visibility:collapse"><asp:Label ID="lblMensagem" runat ="server" Text="..."></asp:Label></div>--%>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                  

                                </asp:Table>
                            </asp:TableCell>
                            <asp:TableCell VerticalAlign="Top" HorizontalAlign="Center" RowSpan="2">
                            <br />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

    </form>

        <div class="container-full">
            <hr />
            <div style="font-family: Arial; font-size: small; color: darkgray">
                <p>&copy; <%: DateTime.Now.Year %> - Santana SA - Credito Financiamento e Investimento.</p>
            </div>
        </div>

  
</body>

 
</html>
