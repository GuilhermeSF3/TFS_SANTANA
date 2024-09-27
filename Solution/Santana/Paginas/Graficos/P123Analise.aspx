<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="P123Analise.aspx.vb" Inherits="Santana.Paginas.Graficos.P123Analise" Title="Gráfico Análise das Parcelas p123" EnableEventValidation="false"%>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Gráfico Análise das Parcelas p123</title>
    <script src="../../Scripts/Chart.js" type="text/javascript"></script>
    <script src="../../Scripts/legend.js"></script>
    <link rel="stylesheet" href="../../Content/chart-legend.css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel" runat="server" >
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

                        <ul class="nav navbar-nav" >
          
                            <li> 
                                <div style="margin:5px" >
                                    <div>
                                        <p class="navbar-text" style="float:none; margin:0px" >Data de Referencia</p>
                                    </div>
                                    <div class="btn-group">                                                              
                                        <asp:Button ID="btnDataAnterior" runat="server" Text="&laquo;" CssClass="btn btn-default navbar-btn" OnClick="btnDataAnterior_Click"></asp:Button>
                                        <span class="btn" style="padding: 0px;border-width: 0px;"> 
                                            <cc1:Datax ID="txtData" runat="server" MaxLength="10" Width="100px" CssClass="form-control navbar-btn"></cc1:Datax>
                                        </span>
                                        <asp:Button ID="btnProximaData" runat="server" Text="&raquo;" CssClass="btn btn-default navbar-btn" OnClick="btnProximaData_Click"></asp:Button>                                   
                                    </div>
                                 <div>
                            </li>
                      
                                
                            <li>
                                <div style="margin:5px; z-index: 1">
                                    <div>
                                        <p class="navbar-text" style="float:none; margin:auto" >Parcela</p>
                                    </div>
                                    <asp:DropDownList ID="ddlFaixa" Width="50px" runat="server" Visible="True" Enabled="true" AutoPostBack="true" CausesValidation="True" CssClass="selectpicker navbar-btn" OnSelectedIndexChanged="ddlFaixa_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </li>

                            <li>
                                <div style="width: 15px" class="btn-group"></div>
                            </li>
                            <li>
                                <div style="margin:5px">
                                    <div style="height:20px " >
                                       
                                    </div>
                                    <asp:Button ID="btnCarregar" runat="server" Text="Carregar" class="btn btn-primary navbar-btn" OnClick="btnCarregar_Click"></asp:Button>
                                    <asp:Button ID="btnMenu" runat="server" Text="Menu Principal" class="btn btn-default navbar-btn" OnClick="btnMenu_Click"></asp:Button>
                                   
                                </div> 
                            </li>

                        </ul>

                        <ul class="nav navbar-nav navbar-right">
                            <li>
                                <div style="margin:5px">
                                    <div style="height:20px " >
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

    <div style="margin-left: 38px;">
    <table>
        <tr>
            <td>
                <div>
                    <canvas id="Chart" width="1100" height="400"></canvas>
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
        <ContentTemplate>


            <div style="width: 15px" class="btn-group"></div>
            
            <div id="dvConsultas" style="width: 100%; overflow: auto;">


                <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                    AllowPaging="True" PageSize="14" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowCreated="GridView1_RowCreated" ShowFooter="true" OnDataBound="GridView1_DataBound">


                    <RowStyle Height="31px" />
                    <Columns>
                        

                        <asp:TemplateField HeaderText="Descrição" SortExpression="Descricao" ItemStyle-BackColor="#EFEFEF">
                            <ItemStyle Width="13%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" SortExpression="M1">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" SortExpression="M2">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" SortExpression="M3">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>						
                        <asp:TemplateField HeaderText="" SortExpression="M4">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" SortExpression="M5">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" SortExpression="M6">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" SortExpression="M7">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" SortExpression="M8">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" SortExpression="M9">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>						
                        <asp:TemplateField HeaderText="" SortExpression="M10">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" SortExpression="M11">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" SortExpression="M12">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>	
                        <asp:TemplateField HeaderText="" SortExpression="M13">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" SortExpression="M14">
                            <ItemStyle Width="6%" HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="FORMATO" SortExpression="FORMATO" ItemStyle-BackColor="#EFEFEF" Visible="false">
                            <ItemStyle Width="13%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ORDEM_LINHA" SortExpression="ORDEM_LINHA" ItemStyle-BackColor="#EFEFEF" Visible="false">
                            <ItemStyle Width="13%" HorizontalAlign="Left" />
                        </asp:TemplateField>

                    </Columns>

                    <PagerTemplate>
                        <div class="btn-group btn-group-sm" style="margin:5px" >
                            <asp:ImageButton ID="PagerFirst" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/skip_backward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="First" UseSubmitBehavior="false"></asp:ImageButton>
                            <asp:ImageButton ID="PagerPrev" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/seek_backward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="Prev" UseSubmitBehavior="false"></asp:ImageButton>
                            <asp:ImageButton ID="PagerNext" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/seek_forward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="Next" UseSubmitBehavior="false"></asp:ImageButton>
                            <asp:ImageButton ID="PagerLast" runat="server" CssClass="btn btn-default" ImageUrl="~/imagens/skip_forward2424.png" CausesValidation="false" CommandName="Page" CommandArgument="Last" UseSubmitBehavior="false"></asp:ImageButton>
                        </div>

                        <asp:DropDownList ID="PagerPages" runat="server" AutoPostBack="true" Width="60px" CausesValidation="false" OnSelectedIndexChanged="PagerPages_SelectedIndexChanged" CssClass="navbar-btn" style="z-index:2000" />&nbsp;
                    </PagerTemplate>

                    <HeaderStyle CssClass="GridviewScrollC3Header" />
                    <RowStyle CssClass="GridviewScrollC3Item" />
                    <PagerStyle CssClass="GridviewScrollC3Pager" />
                    <FooterStyle CssClass="GridviewScrollC3Footer" />
                </asp:GridView>

            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
    </div>

    <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel"  >
        <ProgressTemplate>
            <div class="overlay" />
            <div id="SpingLoad" class="overlayContent">
               <h2>Carregando</h2>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

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

                var faixa = $$("ddlFaixa").val();
                var data = $$("txtData").val();

                var jsonData = JSON.stringify({
                    ddlFaixa: faixa,
                    txtData: data
                });

                $.ajax({
                    type: "POST",
                    url: "P123Analise.aspx/BindGraphData",
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
                            backgroundColor: "rgba(255, 0, 0, 0.4)",
                            borderColor: "rgba(255, 0, 0, 1.0)",

                            borderCapStyle: 'butt',
                            borderDash: [],
                            borderDashOffset: 0.0,
                            borderJoinStyle: 'miter',
                            pointBackgroundColor: "#fff",
                            pointBorderWidth: 1,
                            pointHoverRadius: 5,
                            pointBorderColor: "rgba(255, 0, 0, 1.0)",
                            pointHoverBackgroundColor: "rgba(255, 0, 0, 1.0)",
                            pointHoverBorderColor: "rgba(255, 0, 0, 1.0)",

                            pointHoverBorderWidth: 2,
                            pointRadius: 1,
                            pointHitRadius: 10
                        }]
                    };


                    var ctx = $("#Chart");
                    ctx[0].height = 400;
                    ctx[0].width = 1100;

                    debugger;
                    lineChart = new Chart(ctx, {
                        type: 'line',
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



            $(document).bind("change", "[id$=ddlFaixa],[id$=txtData]", function () {
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
