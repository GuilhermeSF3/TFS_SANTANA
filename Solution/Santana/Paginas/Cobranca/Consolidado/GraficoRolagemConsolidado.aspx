<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="GraficoRolagemConsolidado.aspx.vb" Inherits="Santana.Paginas.Cobranca.Consolidado.GraficoRolagemConsolidado" Title="Gráfico Rolagem Consolidado" EnableEventValidation="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Gráfico Rolagem Consolidado</title>
    <script src="../../../Scripts/Chart.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../../../Content/chart-legend.css" />
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
                                <div style="margin: 5px; z-index: 1">
                                    <div>
                                        <p class="navbar-text" style="float: none; margin: auto">Tipo: </p>
                                    </div>
                                    <asp:DropDownList ID="ddlTipo" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true"
                                        CausesValidation="True" CssClass="selectpicker navbar-btn" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged">
                                    </asp:DropDownList>
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


    <div id="chartContainer">
        <canvas id="Chart1" width="1400" height="400"></canvas>
    </div>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:GridView ID="GridView1" runat="server"
                AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                AllowPaging="False" PageSize="34"
                OnRowCreated="GridView1_RowCreated" ShowFooter="false" DataKeyNames="DESCR">


                <RowStyle Height="31px" />
                <Columns>

                    <asp:TemplateField HeaderText="Descrição" SortExpression="DESCR">
                        <ItemStyle Width="200px" HorizontalAlign="left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="0" SortExpression="m0">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="1" SortExpression="m1">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="2" SortExpression="m2">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="3" SortExpression="m3">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="4" SortExpression="m4">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="5" SortExpression="m5">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="6" SortExpression="m6">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="7" SortExpression="m7">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="8" SortExpression="m8">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="9" SortExpression="m9">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="10" SortExpression="m10">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="11" SortExpression="m11">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="12" SortExpression="m12">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="13" SortExpression="m13">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="14" SortExpression="m14">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="15" SortExpression="m15">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="16" SortExpression="m16">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="17" SortExpression="m17">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="18" SortExpression="m18">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="19" SortExpression="m19">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="20" SortExpression="m20">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="21" SortExpression="m21">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="22" SortExpression="m22">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="23" SortExpression="m23">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="24" SortExpression="m24">
                        <ItemStyle Width="6%" HorizontalAlign="right" />
                    </asp:TemplateField>


                    <asp:TemplateField>
                        <ItemStyle Width="1%" />
                    </asp:TemplateField>



                </Columns>

                <HeaderStyle CssClass="GridviewScrollC3Header" />
                <RowStyle CssClass="GridviewScrollC3Item" />
                <PagerStyle CssClass="GridviewScrollC3Pager" />

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

        var lineChart1;

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

                $("#entradas").toggle(true);
                $("#estoque").toggle(true);
                $("#regularizacao").toggle(true);
                $("#rolagem").toggle(true);

            };


            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(StopSpin);
            Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(StartSpin);



            $(document).on("click", "[id$=btnCarregar]", function () {

                var txtData = $$("txtData").val();
                var tipo = $$("ddlTipo").val();

                var jsonData = JSON.stringify({
                    txtData: txtData,
                    tipo: tipo
                });

                $.ajax({
                    type: "POST",
                    url: "GraficoRolagemConsolidado.aspx/BindGraphData",
                    data: jsonData,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: EntradaOnSuccess_,
                    error: OnErrorCall_
                });

                function EntradaOnSuccess_(reponse) {

                    var aData = reponse.d;                    
                    GraphOnSuccess(aData);
                }



                function OnErrorCall_(response) {

                    alert(response.d);

                }

            });



            function GraphOnSuccess(aData) {
                var aLabels = aData[0];
                var colorsAlpha = aData[1];
                var colors = aData[2];


                var dataGraph = {
                    labels: aLabels,
                    datasets: []
                };

                var dataset = {};
                var color = 0;
                var tamanho = aData.length;
                for (var i = 3; i < tamanho; i++) {


                    dataset = {
                        label: aData[i],
                        data: aData[++i],

                        fill: false,
                        lineTension: 0.1,
                        backgroundColor: colorsAlpha[color],
                        borderColor: colors[color],
                        borderCapStyle: 'butt',
                        borderDash: [],
                        borderDashOffset: 0.0,
                        borderJoinStyle: 'miter',
                        pointColor: colors[color],
                        pointBackgroundColor: colors[color],
                        pointRadius: 5,
                    };

                    color++;
                    dataGraph.datasets.push(dataset);

                }


                var options = {
                    scales: {
                        xAxes: [{
                            gridLines: {
                                //display: false,
                                color: "rgba(255, 255, 255, 255)",
                            }
                        }],
                        yAxes: [{
                            gridLines: {
                                color: "rgba(0, 0, 0, 0)",
                            }
                        }]
                    }
                }


                var options = {

                    bezierCurve: true,
                    legend: {
                        display: true,
                        position: 'bottom'
                    },

                    tooltips: {
                        callbacks: {
                            label: function (tooltipItem) {
                                debugger;
                                return tooltipItem.yLabel;
                            },
                            labelColor: function(z) {
                                debugger;
                            }
                        }
                    },
                
                    showAllTooltips: true
                
                };

                Chart.defaults.global.tooltips.backgroundColor = 'rgba(0,0,0,0.0)';
                Chart.defaults.global.tooltips.titleFontColor = '#000';
                Chart.defaults.global.tooltips.bodyFontColor = '#000';
                Chart.defaults.global.tooltips.displayColors = false;
                Chart.defaults.global.tooltips.titleFontColor = 'rgba(0,0,0,0.0)';
                Chart.defaults.global.tooltips.titleAlign = 'center';



                Chart.pluginService.register({
                    beforeRender: function(chart) {
                        if (chart.config.options.showAllTooltips) {
                            // create an array of tooltips
                            // we can't use the chart tooltip because there is only one tooltip per chart
                            chart.pluginTooltips = [];
                            chart.config.data.datasets.forEach(function(dataset, i) {
                                chart.getDatasetMeta(i).data.forEach(function(sector, j) {
                                    chart.pluginTooltips.push(new Chart.Tooltip({
                                        _chart: chart.chart,
                                        _chartInstance: chart,
                                        _data: chart.data,
                                        _options: chart.options.tooltips,
                                        _active: [sector]
                                    }, chart));
                                });
                            });

                            // turn off normal tooltips
                            chart.options.tooltips.enabled = false;
                        }
                    },
                    afterDraw: function(chart, easing) {
                        if (chart.config.options.showAllTooltips) {
                            // we don't want the permanent tooltips to animate, so don't do anything till the animation runs atleast once
                            if (!chart.allTooltipsOnce) {
                                if (easing !== 1)
                                    return;
                                chart.allTooltipsOnce = true;
                            }

                            // turn on tooltips
                            chart.options.tooltips.enabled = true;
                            Chart.helpers.each(chart.pluginTooltips, function(tooltip) {
                                tooltip.initialize();
                                tooltip.update();
                                // we don't actually need this since we are not animating tooltips
                                tooltip.pivot();
                                tooltip.transition(easing).draw();
                            });
                            chart.options.tooltips.enabled = false;
                        }
                    }
                });

                var ctx = $("#Chart1");
                lineChart1 = new Chart(ctx, {
                        type: 'line',
                        data: dataGraph,
                        options: options
                    });
               }

                function $$(id, context) {
                    var el = $("#" + id, context);
                    if (el.length < 1)
                        el = $("[id$=_" + id + "]", context);
                    return el;
                }


                $(document).bind("change", "", function () {
                    lineChart1.clear();
                    lineChart1.destroy();

                });



    </script>


    


</asp:Content>


