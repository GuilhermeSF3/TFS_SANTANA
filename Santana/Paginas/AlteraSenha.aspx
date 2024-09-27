<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="AlteraSenha.aspx.vb" Inherits="Santana.AlteraSenha" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Santana - Alteração de Senha</title>
    <link rel="stylesheet" href="../css/Style.css" />
    <link rel="stylesheet" href="../Content/bootstrap.css" />

    <script type="text/javascript" src="../Scripts/jquery-2.1.0.min.js"></script>
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.bootstrap-growl.js"></script>
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

<body>

    <div style="height: 200px">
        <table border="0" cellpadding="0" cellspacing="0" style="height: 112px; width: 100%">
            <tbody>
                <tr class="tr-1-r1-c1">
                    <td class="td-1-r1-c1" colspan="3">&nbsp; &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="td-1-r2-c1" style="width: 387px; height: 88px;">
                        <div style="width: 387px; height: 88px;"></div>
                    </td>
                    <td class="td-1-r2-c2" style="width: 100%; height: 88px"></td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
            </tbody>
        </table>
    </div>


    <form id="form1" action="AlteraSenha.aspx" runat="server" cssclass="body">

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
                                            <div style="height: 450px; width: 360px" class="panel panel-default">
                                                <div class="panel-heading">
                                                    <h3 class="panel-title">Altere Sua Senha</h3>
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
                                                    <asp:Button ID="btnAlterar" runat="server" Text="Alterar" CssClass="btn btn-success navbar-btn btn-lg" OnClick="btnAlterar_Click" Height="50px"></asp:Button>
                                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-default navbar-btn btn-lg" OnClick="btnCancelar_Click" Height="50px"></asp:Button>


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

