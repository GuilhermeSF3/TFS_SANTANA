<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="AlteraSenha.aspx.vb" Inherits="Santana.AlteraSenha" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Santana - Alteração de Senha</title>
    <link rel="stylesheet" href="../css/Style.css" />
    <link rel="stylesheet" href="../Content/bootstrap.css" />
   

    <script type="text/javascript" src="../Scripts/jquery-2.1.0.min.js"></script>
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="../Scripts/bootstrap-growl.js"></script>

</head>

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


</script>

<style>
        
      
    </style>

<body>




    <form id="form1" action="AlteraSenha.aspx" runat="server" cssclass="body">
        <div style="display: flex; height: 100vh;">

            <!-- Coluna da Imagem à Esquerda -->
            <div style="flex: 1; background-image: url('../Imagens/IMG-LOGIN.JPG'); border-radius: 0px 67px 67px 0px; background-size: cover; background-position: center;">
            </div>





            <div style="flex: 1; display: flex; margin  align-items: center; align-items: center; flex-direction: column; gap: 30px;">
                <img src="../Imagens/LOGOESCURO.png" alt="Logo" style="width: 300px; margin-top: 75px; margin-right:30px; height: auto; text-align: center; justify-content: center;" />

                <asp:Table ID="Table2" runat="server" Width="100%" CellPadding="0" CellSpacing="100" Height="400px">
                    <asp:TableRow ID="TableRow2" runat="server" HorizontalAlign="Center">
                        <asp:TableCell ID="TableCell1" Width="100%" Height="100%" runat="server" VerticalAlign="Top">
                            <asp:Table runat="server" ID="Table3" HorizontalAlign="Center">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Middle">

                                        <asp:Table ID="Table4" runat="server" CellPadding="0" CellSpacing="0" BorderStyle="solid"
                                            BorderColor="#636363" BorderWidth="0" HorizontalAlign="Center">

                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <div style="height: 500px; width: 360px" class="">
                                                        <div class="panel-heading " style="">
                                                            <h3 style="font-weight:500;" class="panel-title">Altere Sua Senha</h3>
                                                        </div>
                                                        <div class="panel-body">

                                                            <div class="control-group">
                                                                <h5 class="text-left">Usuário</h5>
                                                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                                                            </div>
                                                            <div class="control-group">
                                                                <div>
                                                                    <h5 class="text-left">Senha Atual</h5>
                                                                </div>
                                                                <asp:TextBox ID="txtSenhaAntiga" runat="server" CssClass="form-control" Width="300px" TextMode="Password"></asp:TextBox>
                                                            </div>
                                                            <div class="control-group">
                                                                <div>
                                                                    <h5 class="text-left">Nova Senha</h5>
                                                                </div>
                                                                <asp:TextBox ID="txtSenhaNova" runat="server" CssClass="form-control" Width="300px" TextMode="Password"></asp:TextBox>
                                                            </div>
                                                            <div class="control-group">
                                                                <div>
                                                                    <h5 class="text-left">Confirme Sua Senha</h5>
                                                                </div>
                                                                <asp:TextBox ID="txtConfirmaSenha" runat="server" CssClass="form-control" Width="300px" TextMode="Password"></asp:TextBox>
                                                            </div>


                                                            <div class="control-group" style="margin-top:10px;">
                                                                <div class="glyphicon glyphicon-pushpin"></div><small>Política de Segurança</small><br />
                                                                <h5><small>- Sua senha deve ter pelo menos 8 caracteres.<br />
                                                                    - Deve conter pelo menos uma letra, um digito.<br />
                                                                    - Deve conter pelo menos um caractere especial.<br />
                                                                    - Os caracteres especiais válidos são: <strong>! $ # %</strong>
                                                                </small></h5>
                                                            </div>
                                                            <asp:Button ID="btnAlterar" runat="server" Text="Alterar" Style="width: 300px; font-style: bold; border: none; color: white; border-radius: 15px; background-color: #152B61; height: 35px; margin-top: 25px;" OnClick="btnAlterar_Click" Height="50px"></asp:Button>
                                                       
                                                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Style="width: 300px; font-style: bold; border: 1px solid black;  color: black; border-radius: 15px; background-color: transparent; height: 35px; margin-top:15px;" OnClick="btnCancelar_Click" Height="50px"></asp:Button>


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
    </form>
</body>

</html>

