<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="FechaComAnaliseVeiculoAno.aspx.vb" Inherits="Santana.FechaComAnaliseVeiculoAno" Title="Análise de Veículos Ano" EnableEventValidation="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Análise de Veículos Ano</title>
    <script src="../../Scripts/Chart.js" type="text/javascript"></script>
    <script src="../../Scripts/legend.js"></script>
    <link rel="stylesheet" href="../../Content/chart-legend.css" />
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
                                        <p class="navbar-text" style="float: none; margin: 0">Data de Referencia</p>
                                    </div>
                                    <div class="btn-group">
                                        <asp:Button ID="btnDataAnterior" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnterior_Click"></asp:Button>
                                        <span class="btn" style="padding: 0; border-width: 0;">
                                            <cc1:Datax ID="txtData" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaData" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaData_Click"></asp:Button>
                                    </div>
                                    <div>
                            </li>


                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p id="ddlVeiculolabel" class="navbar-text" style="float: none; margin: auto">Veículo</p>
                                    </div>
                                    <asp:DropDownList ID="ddlVeiculo" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn" OnSelectedIndexChanged="ddlVeiculo_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </li>


                            <li>
                                <div style="margin: 5px">
                                    <div>
                                        <p id="ddlRelatorioLabel" class="navbar-text" style="float: none; margin: auto">Relatório</p>
                                    </div>
                                    <asp:DropDownList ID="ddlRelatorio" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn" OnSelectedIndexChanged="ddlRelatorio_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </li>

                            <li>
                                <div style="width: 15px" class="btn-group"></div>
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


                <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                    AllowPaging="True" PageSize="12" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowCreated="GridView1_RowCreated" ShowFooter="false" OnDataBound="GridView1_DataBound" DataKeyNames="DESCR">

                    <RowStyle Height="31px" />
                    <Columns>


                        <asp:TemplateField HeaderText="Período" SortExpression="DESCR">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="De 0km a 2006" SortExpression="fx1">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="De 2005 a 2000" SortExpression="fx2">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="De 1999 a 1996" SortExpression="fx3">
                            <ItemStyle Width="7%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="De 1995 a 1991" SortExpression="fx4">
                            <ItemStyle Width="7%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="De 1990 a 1986" SortExpression="fx5">
                            <ItemStyle Width="7%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="COR" SortExpression="COR_LINHA" Visible="false">
                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                                             
                        <asp:TemplateField>
                            <ItemStyle Width="1%" />
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

    <div style="margin-left: 38px;">
    <table>
        <tr>
            <td>
                <div>
                    <canvas id="Chart1" width="1100" height="400"></canvas>
                </div>
            </td>
            <td>
                <div style="height: 20px">
                </div>
                <div id="dvLegend">
                </div>
            </td>
        </tr>
    </table>


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

        var lineChart;

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

            $(document).on("click", "[id$=btnCarregar]", function () {

                var data = $$("txtData").val();
                var veiculo = $$("ddlVeiculo").val();
                var relatorio = $$("ddlRelatorio").val();

                var jsonData = JSON.stringify({
                    txtData: data,
                    ddlVeiculo: veiculo,
                    ddlRelatorio: relatorio
                });

                $.ajax({
                    type: "POST",
                    url: "FechaComAnaliseVeiculoAno.aspx/BindGraphData",
                    data: jsonData,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess_,
                    error: OnErrorCall_
                });

                function OnSuccess_(reponse) {

                    var aData = reponse.d;
                    var aLabels = aData[0];

                    var aDataSetLabel1 = aData[1];
                    var aDatasets1 = aData[2];

                    var aDataSetLabel2 = aData[3];
                    var aDatasets2 = aData[4];

                    var aDataSetLabel3 = aData[5];
                    var aDatasets3 = aData[6];

                    var aDataSetLabel4 = aData[7];
                    var aDatasets4 = aData[8];

                    var aDataSetLabel5 = aData[9];
                    var aDatasets5 = aData[10];

                    var dataGraph = {
                        labels: aLabels,
                        datasets: [{
                            label: aDataSetLabel1,
                            data: aDatasets1,


                            fill: false,
                            lineTension: 0.1,
                            backgroundColor: "rgba(0,255,255,0.4)",
                            borderColor: "rgba(0,255,255,1)",

                            borderCapStyle: 'butt',
                            borderDash: [],
                            borderDashOffset: 0.0,
                            borderJoinStyle: 'miter',
                            pointBackgroundColor: "#fff",
                            pointBorderWidth: 1,
                            pointHoverRadius: 5,
                            pointBorderColor: "rgba(0,255,255,1)",
                            pointHoverBackgroundColor: "rgba(0,255,255,1)",
                            pointHoverBorderColor: "rgba(0,255,255,1)",

                            pointHoverBorderWidth: 2,
                            pointRadius: 1,
                            pointHitRadius: 10

                        },
                        {
                            label: aDataSetLabel2,
                            data: aDatasets2,


                            fill: false,
                            lineTension: 0.1,
                            backgroundColor: "rgba(0,64,128,0.4)",
                            borderColor: "rgba(0,64,128,1)",

                            borderCapStyle: 'butt',
                            borderDash: [],
                            borderDashOffset: 0.0,
                            borderJoinStyle: 'miter',
                            pointBackgroundColor: "#fff",
                            pointBorderWidth: 1,
                            pointHoverRadius: 5,

                            pointBorderColor: "rgba(0,64,128,1)",
                            pointHoverBackgroundColor: "rgba(0,64,128,1)",
                            pointHoverBorderColor: "rgba(0,64,128,1)",

                            pointHoverBorderWidth: 2,
                            pointRadius: 1,
                            pointHitRadius: 10
                        },
                        {
                            label: aDataSetLabel3,
                            data: aDatasets3,


                            fill: false,
                            lineTension: 0.1,
                            backgroundColor: "rgba(191,63,63,1)",
                            borderColor: "rgba(0,64,128,1)",

                            borderCapStyle: 'butt',
                            borderDash: [],
                            borderDashOffset: 0.0,
                            borderJoinStyle: 'miter',
                            pointBackgroundColor: "#fff",
                            pointBorderWidth: 1,
                            pointHoverRadius: 5,

                            pointBorderColor: "rgba(0,64,128,1)",
                            pointHoverBackgroundColor: "rgba(0,64,128,1)",
                            pointHoverBorderColor: "rgba(0,64,128,1)",

                            pointHoverBorderWidth: 2,
                            pointRadius: 1,
                            pointHitRadius: 10
                        },
                        {
                            label: aDataSetLabel4,
                            data: aDatasets4,


                            fill: false,
                            lineTension: 0.1,
                            backgroundColor: "rgba(247,247,120,0.96)",
                            borderColor: "rgba(0,64,128,1)",

                            borderCapStyle: 'butt',
                            borderDash: [],
                            borderDashOffset: 0.0,
                            borderJoinStyle: 'miter',
                            pointBackgroundColor: "#fff",
                            pointBorderWidth: 1,
                            pointHoverRadius: 5,

                            pointBorderColor: "rgba(0,64,128,1)",
                            pointHoverBackgroundColor: "rgba(0,64,128,1)",
                            pointHoverBorderColor: "rgba(0,64,128,1)",

                            pointHoverBorderWidth: 2,
                            pointRadius: 1,
                            pointHitRadius: 10
                        },
                        {
                            label: aDataSetLabel5,
                            data: aDatasets5,


                            fill: false,
                            lineTension: 0.1,
                            backgroundColor: "rgba(199,199,9,0.96)",
                            borderColor: "rgba(0,64,128,1)",

                            borderCapStyle: 'butt',
                            borderDash: [],
                            borderDashOffset: 0.0,
                            borderJoinStyle: 'miter',
                            pointBackgroundColor: "#fff",
                            pointBorderWidth: 1,
                            pointHoverRadius: 5,

                            pointBorderColor: "rgba(0,64,128,1)",
                            pointHoverBackgroundColor: "rgba(0,64,128,1)",
                            pointHoverBorderColor: "rgba(0,64,128,1)",

                            pointHoverBorderWidth: 2,
                            pointRadius: 1,
                            pointHitRadius: 10
                        }]
                    };


                    var ctx = $("#Chart1");
                    ctx[0].height = 400;
                    ctx[0].width = 1100;

                    debugger;
                    lineChart = new Chart(ctx, {
                        type: 'bar',
                        data: dataGraph,
                        options: {
                            bezierCurve: false, //true 13/2/17

                            legend: {
                                display: true,
                                position: 'bottom'
                            }
                        }
                    });
                }



                function OnErrorCall_(response) {

                    alert(response.d);

                }





            });



            $(document).bind("change", "[id$=txtData],[id$=ddlVeiculo],[id$=ddlRelatorio]", function () {
                lineChart.clear();
                lineChart.destroy();
            });


            function $$(id, context) {
                var el = $("#" + id, context);
                if (el.length < 1)
                    el = $("[id$=_" + id + "]", context);
                return el;
            }
    </script>


</asp:Content>





