Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic
Imports Santana.Seguranca
Imports Util

Namespace Paginas.Cobranca

    Public Class FechamentoCobranca

        Inherits SantanaPage

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                'Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
                'txtDataDe.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)

                'txtDataAte.Text = (DateTime.Now.AddDays(-1)).ToString("dd/MM/yyyy")

                'If ContextoWeb.DadosTransferencia.CodCobradora = 0 Then
                '    Carrega_Cobradora()
                'Else
                '    Carrega_Cobradora()
                '    ddlCobradora.SelectedIndex = ddlCobradora.Items.IndexOf(ddlCobradora.Items.FindByValue(ContextoWeb.DadosTransferencia.CodCobradora.ToString()))

                'End If

                'If ContextoWeb.DadosTransferencia.CodCobradora <> 0 Or ContextoWeb.DadosTransferencia.CodAgente <> 0 Then

                '    ContextoWeb.DadosTransferencia.CodCobradora = 0
                'End If

            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub





        Private Function rodarFechamento()

            Dim UltimoDiaDoMesAnterior As String = txtDataDe3.Text
            Dim PrimeiroDiaDoMesAnterior As String = txtDataDe2.Text
            Dim UltimoDiaUtilDoMesAnterior As String = txtDataDe1.Text
            Dim ultimoDiaFormatado As String = DateTime.Parse(UltimoDiaDoMesAnterior).ToString("yyyyMMdd")
            Dim primeiroDiaFormatado As String = DateTime.Parse(PrimeiroDiaDoMesAnterior).ToString("yyyyMMdd")
            Dim ultimoDiaUtilFormatado As String = DateTime.Parse(UltimoDiaUtilDoMesAnterior).ToString("yyyyMMdd")


            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

            Using con As New SqlConnection(strConn)
                Try
                    Using cmd As New SqlCommand($"EXEC SCR_PC_FECHAMENTO_COB @primeiroDiaFormatado,  @ultimoDiaUtilFormatado,  @ultimoDiaFormatado ", con) '

                    End Using

                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Sem Resposta' ,'Erro ao realizar o fechamento de cobrança');", True)
                End Try

            End Using




        End Function





        'Private Function GetData(dataReferencia As String) As DataTable
        '    Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        '    Dim table As New DataTable()


        '    Using con As New SqlConnection(strConn)
        '        Using cmd As New SqlCommand($"EXEC SCR_CNAB550_CARGA '{dataReferencia}'", con)
        '            cmd.CommandType = CommandType.Text

        '            Using sda As New SqlDataAdapter(cmd)
        '                Try
        '                    sda.Fill(table)
        '                    If table.Rows.Count = 0 Then

        '                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Sem Resposta' ,'Nenhum arquivo encontrado na data informada.');", True)
        '                    End If
        '                Catch ex As Exception

        '                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Sem Resposta' ,'Nenhum arquivo encontrado na data informada.');", True)
        '                End Try
        '            End Using
        '        End Using
        '    End Using

        '    Return table
        'End Function





        Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Fechamento de Cobrança' ,'Fechamento de cobrança, realizar processo validando as datas.');", True)
        End Sub


        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../../Menu.aspx")
        End Sub

        Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
            Try

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "confirm", "if(confirm('Tem certeza que deseja carregar o fechamento?')) { rodarFechamento(); }", True)

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try

        End Sub

        Protected Sub btnCarregarProd_Click(sender As Object, e As EventArgs)
            Try



            Catch ex As Exception

            Finally
                GC.Collect()
            End Try

        End Sub


        Public Overrides Sub VerifyRenderingInServerForm(control As Control)
            'Not Remove
            ' Confirms that an HtmlForm control is rendered for the specified ASP.NET
            '     server control at run time. 

        End Sub


    End Class
End Namespace