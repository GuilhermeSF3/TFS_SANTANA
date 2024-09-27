<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="ControleAcoes.aspx.vb" Inherits="Santana.Paginas.Cobranca.Relatorios.ControleAcoes" Title="Controle de Ações - Busca e Apreensão" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Controle de Ações - Busca e Apreensão </title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
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
                        <ul class="nav navbar-nav">
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0px">Data Referência:</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnRefAnt" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnRefAnt_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <cc1:Datax ID="txtDataRef" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnRefPro" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnRefPro_Click"></asp:Button>
                                    </div>
                                </div>
                            </li>   
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Agentes:</p>
                                    </div>
                                    <asp:DropDownList ID="ddlAgente" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li> 
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Escritórios:</p>
                                    </div>
                                    <asp:DropDownList ID="ddlEscritorio" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>   
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Contrato:</p>
                                    </div>
                                    <asp:TextBox ID="txtContrato" runat="server" MaxLength="15" Width="110px" CssClass="form-control navbar-btn"></asp:TextBox>
                                </div>
                            </li>  
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">CPF:</p>
                                    </div>
                                    <asp:TextBox ID="txtCPF" runat="server" MaxLength="15" Width="130px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>                                                                                                                    
                            <li>
                                <div style="margin: 5px">
                                    <div style="height: 20px">
                                    </div>
                                    <asp:Button ID="btnCarregar" runat="server" Text="Carregar" class="btn btn-primary navbar-btn" OnClick="btnCarregar_Click"></asp:Button>                                    
                                    <asp:Button ID="btnMenu" runat="server" Text="Menu Principal" class="btn btn-default navbar-btn" OnClick="btnMenu_Click"></asp:Button>
                                </div>
                            </li>
                        </ul>

                        <ul class="nav navbar-nav navbar-right">
                            <li>
                                <div style="margin: 5px">
                                    <div style="height: 20px">
                                    </div>
                                    <div class="btn-group-sm  ">
                                        <asp:ImageButton ID="btnExcel" runat="server" CssClass="btn btn-default navbar-btn" OnClick="btnExcel_Click" ImageUrl="~/imagens/excel2424.png"></asp:ImageButton>
                                        <asp:ImageButton ID="btnImpressao" runat="server" CssClass="btn btn-default navbar-btn" OnClick="btnImpressao_Click" ImageUrl="~/imagens/printer2424.png"></asp:ImageButton>
                                        <asp:ImageButton ID="btnHelp" runat="server" CssClass="btn btn-default navbar-btn" OnClick="btnHelp_Click" ImageUrl="~/imagens/help2424.png"></asp:ImageButton>
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </div>

                    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
                        <ul class="nav navbar-nav">
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0px">Data Processo (De):</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDtDeAnt" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDtDeAnt_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <cc1:Datax ID="txtDtDe" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnDtDePro" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDtDePro_Click"></asp:Button>
                                    </div>
                                </div>
                            </li>   
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: 0px">Data Processo (Até):</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDtAteAnt" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDtAteAnt_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px; border-width: 0px;">
                                            <cc1:Datax ID="txtDtAte" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="bntDtAtePro" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDtAtePro_Click"></asp:Button>
                                    </div>
                                </div>
                            </li>   
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Status:</p>
                                    </div>
                                    <asp:DropDownList ID="ddlStatus" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn"></asp:DropDownList>
                                </div>
                            </li>                                                                                                                   
                        </ul>
                    </div>

<b>Processos em Aberto:</b>
                    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-3">
                        <ul class="nav navbar-nav">
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Custas Processuais:</p>
                                    </div>
                                    <asp:TextBox ID="txtCustaProc" ReadOnly="true" runat="server" MaxLength="15" Width="150px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Honorários:</p>
                                    </div>
                                    <asp:TextBox ID="txtHonorarios" ReadOnly="true" runat="server" MaxLength="15" Width="150px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Valor Venda:</p>
                                    </div>
                                    <asp:TextBox ID="txtVlrVenda" ReadOnly="true" runat="server" MaxLength="15" Width="150px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Resultado Venda:</p>
                                    </div>
                                    <asp:TextBox ID="txtResultVenda" ReadOnly="true" runat="server" MaxLength="15" Width="150px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Valor FV:</p>
                                    </div>
                                    <asp:TextBox ID="txtValorFV" ReadOnly="true" runat="server" MaxLength="15" Width="150px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Valor Pago Liquidação:</p>
                                    </div>
                                    <asp:TextBox ID="txtVlrPgLiq" ReadOnly="true" runat="server" MaxLength="15" Width="150px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Saldo Inscrito:</p>
                                    </div>
                                    <asp:TextBox ID="txtSaldoInsc" ReadOnly="true" runat="server" MaxLength="15" Width="150px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>
                         </ul>
                    </div>
<b>Processos Encerrados:</b>
                    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-4">
                        <ul class="nav navbar-nav">
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Custas Processuais:</p>
                                    </div>
                                    <asp:TextBox ID="txtCustaProcE" ReadOnly="true" runat="server" MaxLength="15" Width="150px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Honorários:</p>
                                    </div>
                                    <asp:TextBox ID="txtHonorariosE" ReadOnly="true" runat="server" MaxLength="15" Width="150px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Valor Venda:</p>
                                    </div>
                                    <asp:TextBox ID="txtVlrVendaE" ReadOnly="true" runat="server" MaxLength="15" Width="150px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Resultado Venda:</p>
                                    </div>
                                    <asp:TextBox ID="txtResultVendaE" ReadOnly="true" runat="server" MaxLength="15" Width="150px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Valor FV:</p>
                                    </div>
                                    <asp:TextBox ID="txtValorFVE" ReadOnly="true" runat="server" MaxLength="15" Width="150px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Valor Pago Liquidação:</p>
                                    </div>
                                    <asp:TextBox ID="txtVlrPgLiqE" ReadOnly="true" runat="server" MaxLength="15" Width="150px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Saldo Inscrito:</p>
                                    </div>
                                    <asp:TextBox ID="txtSaldoInscE" ReadOnly="true" runat="server" MaxLength="15" Width="150px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>
                         </ul>
                    </div>
<b>Total (Aberto + Encerrado):</b>
                    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-5">
                        <ul class="nav navbar-nav">
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Custas Processuais:</p>
                                    </div>
                                    <asp:TextBox ID="txtCustaProcS" ReadOnly="true" runat="server" MaxLength="15" Width="150px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Honorários:</p>
                                    </div>
                                    <asp:TextBox ID="txtHonorariosS" ReadOnly="true" runat="server" MaxLength="15" Width="150px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Valor Venda:</p>
                                    </div>
                                    <asp:TextBox ID="txtVlrVendaS" ReadOnly="true" runat="server" MaxLength="15" Width="150px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Resultado Venda:</p>
                                    </div>
                                    <asp:TextBox ID="txtResultVendaS" ReadOnly="true" runat="server" MaxLength="15" Width="150px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Valor FV:</p>
                                    </div>
                                    <asp:TextBox ID="txtValorFVS" ReadOnly="true" runat="server" MaxLength="15" Width="150px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Valor Pago Liquidação:</p>
                                    </div>
                                    <asp:TextBox ID="txtVlrPgLiqS" ReadOnly="true" runat="server" MaxLength="15" Width="150px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>
                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Saldo Inscrito:</p>
                                    </div>
                                    <asp:TextBox ID="txtSaldoInscS" ReadOnly="true" runat="server" MaxLength="15" Width="150px" CssClass="form-control navbar-btn allownumericwithdecimal"></asp:TextBox>
                                </div>
                            </li>
                         </ul>
                    </div>
                </div>
                </div>
            </nav>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="dvConsultas" style="height: 590px; width: 100%; overflow: auto;">
                <asp:Label ID="lblRelatorio" runat="server" Text=""></asp:Label>
                <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                    AllowPaging="True" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowCreated="GridView1_RowCreated" ShowFooter="false"
                    OnDataBound="GridView1_DataBound" Font-Size="9pt">

                    <RowStyle Height="31px" />
                    <Columns>

                        <asp:TemplateField HeaderText="Data Ref." SortExpression="DATAREF">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Operação" SortExpression="OPNROPER">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Processo" SortExpression="NUMPROC">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CPF" SortExpression="CLCGC">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cliente" SortExpression="CLNOMECLI">
                            <ItemStyle Width="15%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Agente" SortExpression="O3DESCR">
                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Escritório" SortExpression="ESCRITORIO">
                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Data Envio Escritório" SortExpression="EODTOCOR">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dias Envio Docto." SortExpression="DIASENVIODOC">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Data de Distribuição" SortExpression="DATA_DIST">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dias da Distribuição" SortExpression="DIASDIST">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Data Apreensão" SortExpression="VADTAPREE">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dias Apreensão" SortExpression="DIASAPRE">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Data Entrada Pátio" SortExpression="VADTENTR">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dias Pátio" SortExpression="DIASPATIO">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Data da Venda" SortExpression="VADTVENDA">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dias para Venda" SortExpression="DIASVENDA">
                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Custas Processuais" SortExpression="CUSTAS_PROCESSUAIS">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Honorários" SortExpression="HONORARIOS">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Valor da Venda" SortExpression="VALOR_VENDA">
                            <ItemStyle Width="10%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Resultado da Venda" SortExpression="RESULTADO_VENDA">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Valor FV" SortExpression="GERENCIAL2">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Valor Pago (Liquidação)" SortExpression="VALOR_PAGO_LIQUIDAÇÃO">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tipo Liquidação" SortExpression="TIPO_LIQ">
                            <ItemStyle Width="10%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Saldo Inscrito" SortExpression="SLD_INSCRITO">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status Processo" SortExpression="STATUS">
                            <ItemStyle Width="7%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status Operação" SortExpression="STATUS_OPER">
                            <ItemStyle Width="7%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                    </Columns>

                    <HeaderStyle CssClass="GridviewScrollC3Header" />
                    <RowStyle CssClass="GridviewScrollC3Item" />
                    <PagerStyle CssClass="GridviewScrollC3Pager" />
                    <FooterStyle CssClass="GridviewScrollC3Footer" />
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel">
        <ProgressTemplate>
            <div class="overlay" />
            <div id="SpingLoad" class="overlayContent">
                <h2>Carregando</h2>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:HiddenField ID="hfGridView1SV" runat="server" OnValueChanged="hfGridView1SV_ValueChanged" />
    <asp:HiddenField ID="hfGridView1SH" runat="server" OnValueChanged="hfGridView1SH_ValueChanged" />

    <script type="text/javascript">
   
        function pageLoad() {
            $('#<%=GridView1.ClientID%>').gridviewScroll({
                width: 100 + "%",
                height: 100 + "%",
                freezesize: 1,
                headerrowcount: 1,
                startVertical: $("#<%=hfGridView1SV.ClientID%>").val(),
                startHorizontal: $("#<%=hfGridView1SH.ClientID%>").val(),

                onScrollVertical: function (delta) {
                    $("#<%=hfGridView1SV.ClientID%>").val(delta);
                },
                onScrollHorizontal: function (delta) {
                    $("#<%=hfGridView1SH.ClientID%>").val(delta);
                },
                arrowsize: 30,
                varrowtopimg: "/imagens/arrowvt.png",
                varrowbottomimg: "/imagens/arrowvb.png",
                harrowleftimg: "/imagens/arrowhl.png",
                harrowrightimg: "/imagens/arrowhr.png"
            });

        }

        $(document).ready(function () {

            NumericAllow();

        });


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

        var spinner;

        function StartSpin() {

            var opts = {
                lines: 17, // The number of lines to draw
                length: 10, // The length of each line
                width: 7, // The line thickness
                radius: 21, // The radius of the inner circle
                corners: 1, // Corner roundness (0..1)
                rotate: 0, // The rotation offset
                direction: 1, // 1: clockwise, -1: counterclockwise
                color: '#000', // #rgb or #rrggbb or array of colors
                speed: 1, // Rounds per second
                trail: 80, // Afterglow percentage
                shadow: false, // Whether to render a shadow
                hwaccel: false, // Whether to use hardware acceleration
                className: 'spinner', // The CSS class to assign to the spinner
                zIndex: 2e9, // The z-index (defaults to 2000000000)
                top: '50%', // Top position relative to parent
                left: '50%' // Left position relative to parent
            };

            var target = document.getElementById('SpingLoad');
            spinner = new Spinner(opts).spin(target);

        };


        function StopSpin() {
            spinner.stop();
        };


        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(StopSpin);
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(StartSpin);


    </script>


</asp:Content>





