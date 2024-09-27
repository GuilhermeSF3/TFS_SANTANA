<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Relatorio.aspx.vb" Inherits="Santana.Relatorio" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Santana Relatórios</title>
    <link rel="stylesheet" href="../css/Style.css" />
    <link rel="stylesheet" href="../Content/bootstrap.css" />

    <script type="text/javascript" src="../Scripts/jquery-2.1.0.min.js"></script> 
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.bootstrap-growl.js"></script>
    <script type="text/javascript" src="../Scripts/bootstrap-growl.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    

        <nav class="navbar navbar-default" role="navigation">
            <div class="container-fluid">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>

                </div>

                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">

                    <ul class="nav navbar-nav" >
                        <li>
                            <div class="btn-group">
                                <asp:Button ID="btnMenu" runat="server" Text="Menu Principal" class="btn btn-default navbar-btn" OnClick="btnMenu_Click"></asp:Button>
                                <asp:Button ID="btnVoltar" runat="server" Text="" class="btn btn-default navbar-btn" OnClick="btnVoltar_Click"></asp:Button>
                            </div>
                        </li>

                    </ul>

                </div>

            </div>
        </nav>


        <div class="row">
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"  />
        </div>
  
    </form>
</body>
</html>
