<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="ForcarICM.aspx.vb" Inherits="Santana.Paginas.Cobranca.ICM.ForcarICM" Title="Forçar ICM" EnableEventValidation="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Forçar ICM</title>
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
                                <div style="width: 15px" class="btn-group"></div>
                            </li>
                            <lidiv style="margin: 5px">
                                 <div style="display: flex; flex-direction: row; gap: 20px;">


      <!-- Data de Referência -->
      <div style="display: flex; flex-direction: column; align-items: start; text-align: start;">
          <label class="navbar-text" style="float: none; margin: 0;">Data de Referência</label>
          <asp:TextBox ID="txtData" runat="server" MaxLength="10" CssClass="form-control navbar-btn datepicker" Style="width: 100px;"></asp:TextBox>
      </div>  
            
     
      <!-- Contrato -->
          
      <div style=" display: flex;  flex-direction: column; align-items: start; text-align: start;" >
          <label  class="navbar-text" style="float: none; margin: 0;">Contrato</label>
          <asp:TextBox ID="txtContract" runat="server" MaxLength="10" CssClass="form-control navbar-btn datepicker"  Style="width: 150px;" ></asp:TextBox>
      </div>
        

      <!-- Parcela -->
          
      <div  style=" display:flex; flex-direction: column; align-items: start; text-align: start;" >
          <label class="navbar-text" style="float: none; margin: 0;">Parcela</label>
          <asp:TextBox ID="txtParcel" runat="server" MaxLength="10" CssClass="form-control navbar-btn datepicker" Style="width: 150px;" ></asp:TextBox>
      </div>

      <!-- Cod cobradora -->
          
      <div  style=" display:flex; flex-direction: column; align-items: start; text-align: start;" >
          <label class="navbar-text" style="float: none; margin: 0;">Código da cobradora</label>
          <asp:TextBox ID="txtCodCobr" runat="server" MaxLength="10" CssClass="form-control navbar-btn datepicker" Style="width: 150px;" ></asp:TextBox>
      </div>
             

      <!-- Botões -->
        
      <div style=" display:flex; align-items: flex-end !important; flex-direction: row; gap: 10px;">    
          <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="btn btn-primary navbar-btn" OnClick="btnConsultar_Click"/>
          <asp:Button ID="btnIncluir" runat="server" Text="Incluir " CssClass="btn btn-primary navbar-btn" OnClick="btnInserir_Click"   />
          <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CssClass="btn btn-danger navbar-btn"  OnClick="btnCarregar_ClickDelete"   />
          <asp:Button ID="btnProcessar" runat="server" Text="Processar" CssClass="btn btn-success navbar-btn"  OnClick="btnProcessar_Click"   />
      </div>               
            
           </div>
                            </li>
                        </ul>
                        <ul class="nav navbar-nav navbar-right">
                            <li>
                                <div style="margin: 5px">
                                    <div style="height: 20px">
                                    </div>
                                    <div class="btn-group-sm  ">
                                        <asp:Button ID="btnMenu" runat="server" Text="Menu Principal" CssClass="btn btn-default navbar-btn w-300" OnClick="btnMenu_Click" />
                                        <asp:ImageButton ID="btnExcel" runat="server" CssClass="btn btn-default navbar-btn" OnClick="btnExcel_Click" ImageUrl="~/imagens/excel2424.png"></asp:ImageButton>
                                    </div>
                     
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
            <div id="dvRiscoAnalitico" runat="server" style="height: 590px; overflow: auto; visibility: visible; margin-left: 50px; margin-top: 0px; text-align: center;">
                <asp:GridView ID="GridViewRiscoAnalitico" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                    AllowPaging="True" PageSize="30" OnPageIndexChanging="GridViewRiscoAnalitico_PageIndexChanging"
                    OnRowCreated="GridViewRiscoAnalitico_RowCreated" ShowFooter="false"
                    OnDataBound="GridViewRiscoAnalitico_DataBound" Font-Size="9pt">
                    <RowStyle Height="28px" />
                    <Columns>
                        <asp:TemplateField HeaderText="STATUS" SortExpression="STATUS">
                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CONTRATO" SortExpression="CONTRATO">
                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PARCELA" SortExpression="PARCELA">
                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VALOR DA PARCELA" SortExpression="VLR_PARCELA">
                            <ItemStyle Width="7%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DATA DO PAGAMENTO" SortExpression="DATA_PAGAMENTO">
                            <ItemStyle Width="8%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ATRASO" SortExpression="ATRASO">
                            <ItemStyle Width="3%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RATING" SortExpression="RATING">
                            <ItemStyle Width="3%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FAIXA" SortExpression="FAIXA">
                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FAIXA RATING" SortExpression="FAIXA_RATING">
                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NOME DO CLIENTE" SortExpression="NOME_CLIENTE">
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CPF / CNPJ" SortExpression="CPF_CNPJ">
                            <ItemStyle Width="6%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CODIGO DA COBRADORA" SortExpression="CODCOBRADORA">
                            <ItemStyle Width="9%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="COBRADORA" SortExpression="COBRADORA">
                            <ItemStyle Width="9%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="AGENTE" SortExpression="AGENTE">
                            <ItemStyle Width="5%" HorizontalAlign="Center" />
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
    <script type="text/javascript" src="https://uxsolutions.github.io/bootstrap-datepicker/boot‌​strap-datepicker/js/‌​locales/bootstrap-da‌​tepicker.pt-BR.min.j‌​s"></script>


    <script type="text/javascript">

        jQuery(function ($) {
            $.datepicker.regional['pt-BR'] = {
                closeText: 'Fechar',
                prevText: '&#x3c;Anterior',
                nextText: 'Pr&oacute;ximo&#x3e;',
                currentText: 'Hoje',
                monthNames: ['Janeiro', 'Fevereiro', 'Mar&ccedil;o', 'Abril', 'Maio', 'Junho',
                    'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun',
                    'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                dayNames: ['Domingo', 'Segunda-feira', 'Ter&ccedil;a-feira', 'Quarta-feira', 'Quinta-feira', 'Sexta-feira', 'Sabado'],
                dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
                dayNamesMin: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
                weekHeader: 'Sm',
                dateFormat: 'dd/mm/yy',
                firstDay: 0,
                isRTL: false,
                showMonthAfterYear: false,
                yearSuffix: ''
            };
            $.datepicker.setDefaults($.datepicker.regional['pt-BR']);
        });




        function pageLoad() {

            $('#<%=GridViewRiscoAnalitico.ClientID%>').gridviewScroll({
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
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/locales/bootstrap-datepicker.pt-BR.min.js"></script>


</asp:Content>





