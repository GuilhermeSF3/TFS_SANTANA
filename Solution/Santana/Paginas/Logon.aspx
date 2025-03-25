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

    <form id="form1" action="Logon.aspx" runat="server">
        <div style="display: flex; height: 100vh;">

            <!-- Coluna da Imagem à Esquerda -->
            <div style="flex: 1; background-image: url('../Imagens/IMG-LOGIN.jpg'); border-radius:0px 67px 67px 0px; background-size: cover; background-position: center;">
            </div>

            
                   
              

             <div style="flex: 1; display: flex;   align-items: center; align-items:center; flex-direction:column; gap:30px;">
      <img src="../Imagens/LOGOESCURO.png" alt="Logo" style="width: 300px; margin-top:100px; height: auto; text-align:center; justify-content:center;" />

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
                                          <div style="height:360px; width:360px; align-items: center;  margin-left:40px;"  class="" >
                                             
                                              <div class="panel-body " style="width:400px; height:100px;">
                                                  <h4 class="page-header  text-left">Seja Bem-Vindo(a) ao <span style="color:darkblue; font-weight:600;">SIG</span></h4>
                                                  <div class="control-group">
                                                        <h5 class="text-left">Usuário</h5>
                                                        <asp:TextBox ID="txtUsuario" runat="server" style="" CssClass="form-control" Width="300px"></asp:TextBox>
                                                  </div>
                                                  <div class="control-group">
                                                      <div><h5 class="text-left">Senha&nbsp;&nbsp;&nbsp;&nbsp;</h5></div> 
                                                      <asp:TextBox ID="txtSenha" runat="server" CssClass="form-control" Width="300px" TextMode="Password"></asp:TextBox>
                                                      <small><asp:LinkButton ID="txtAlterarSenha" runat="server" Text="Alterar senha" CssClass="text-left" OnClick="txtAlterarSenha_Click" ></asp:LinkButton></small>
                                                  </div> 
                                                  
                                                  <asp:Button ID="btnLogin" runat="server" Text="Login" style="width:300px ; font-style: normal; border: none; color:white; border-radius: 15px; background-color:#152B61; height:35px; margin-top: 50px;" OnClick="btnLogin_Click" Height="50px" ></asp:Button>
                                                    
                                                  
                                              </div>
                                          </div> 
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
                
        </div>
            </div>
        <div class="container-full">
    <hr />
    <div style="font-family: Arial; font-size: small; color: darkgray; text-align: center;">
        <p>&copy; <%: DateTime.Now.Year %> - Santana SA - Crédito Financiamento e Investimento.</p>
    </div>
</div>
    </form>

   

    

   
  
</body>

 
</html>
