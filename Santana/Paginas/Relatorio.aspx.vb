'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.Shared
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Imports Santana.Seguranca


Public Class Relatorio
    Inherits SantanaPage


    'Dim rptDoc As New ReportDocument()

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init


        'CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX
        'CrystalReportViewer1.HasDrilldownTabs = False
        'CrystalReportViewer1.EnableDatabaseLogonPrompt = False
        'CrystalReportViewer1.EnableParameterPrompt = False
        'CrystalReportViewer1.HasCrystalLogo = False
        'CrystalReportViewer1.HasExportButton = True
        'CrystalReportViewer1.ReuseParameterValuesOnRefresh = True
        'CrystalReportViewer1.HasToggleGroupTreeButton = False
        'CrystalReportViewer1.HasToggleParameterPanelButton = False
        'CrystalReportViewer1.HasSearchButton = True
        'CrystalReportViewer1.HasDrilldownTabs = False
        'CrystalReportViewer1.HasDrillUpButton = False
        'CrystalReportViewer1.HasRefreshButton = False
        'CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None

        ' rptDoc.Load(Server.MapPath(ContextoWeb.Relatorio.reportFileName))
        ' rptDoc.SetDataSource(ContextoWeb.Relatorio.reportDatas(0).reportDataItem)
        '            CrystalReportViewer1.ReportSource = rptDoc

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            ' ContextoWeb.Navegacao.TituloPaginaAtual = Me.Title
            btnVoltar.Text = "Voltar"

        End If
    End Sub

    Protected Sub btnVoltar_Click(sender As Object, e As EventArgs)

        '  ContextoWeb.Navegacao.LinkPaginaAtual = Me.AppRelativeVirtualPath
        Response.Redirect(ContextoWeb.Navegacao.LinkPaginaAnteriorRelatorio)

    End Sub



    Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
        Response.Redirect("Menu.aspx")
    End Sub
End Class