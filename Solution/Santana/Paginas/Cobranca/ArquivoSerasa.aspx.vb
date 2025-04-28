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

    Public Class ArquivoSerasa

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


        Protected Sub btnGerar_Click(sender As Object, e As EventArgs)
            If Not fuOrigem.HasFile Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alerta", "alert('Selecione o arquivo de origem.');", True)
                Return
            End If

            If Not fuDestino.HasFile Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alerta", "alert('Selecione o arquivo de destino.');", True)
                Return
            End If

            Try
                Dim origemLinhas As List(Of String)
                Using readerOrigem As New StreamReader(fuOrigem.PostedFile.InputStream)
                    origemLinhas = readerOrigem.ReadToEnd().Split({vbCrLf, vbLf}, StringSplitOptions.RemoveEmptyEntries).ToList()
                End Using

                Dim destinoLinhas As List(Of String)
                Using readerDestino As New StreamReader(fuDestino.PostedFile.InputStream)
                    destinoLinhas = readerDestino.ReadToEnd().Split({vbCrLf, vbLf}, StringSplitOptions.RemoveEmptyEntries).ToList()
                End Using

                If origemLinhas.Count < 3 Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alerta", "alert('Arquivo de origem inválido.');", True)
                    Return
                End If

                If destinoLinhas.Count < 2 Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alerta", "alert('Arquivo de destino inválido.');", True)
                    Return
                End If
                Dim meioOrigem As New List(Of String)()
                For Each linha As String In origemLinhas.Skip(1).Take(origemLinhas.Count - 2)
                    If linha.Length >= 9 Then
                        Dim valorAtual As String = linha.Substring(5, 3)
                        If valorAtual = "100" Then
                            linha = linha.Substring(0, 5) & "182" & linha.Substring(8)
                        End If
                    End If
                    meioOrigem.Add(linha)
                Next

                Dim linhasCompletas As New List(Of String)
                linhasCompletas.Add(destinoLinhas.First())
                linhasCompletas.AddRange(destinoLinhas.Skip(1).Take(destinoLinhas.Count - 2))
                linhasCompletas.AddRange(meioOrigem)
                linhasCompletas.Add(destinoLinhas.Last())
                Dim linhasFinal As New List(Of String)()
                Dim contador As Integer = 1

                For Each linha As String In linhasCompletas

                    If linha.Length < 501 Then
                        linha = linha.PadRight(501)
                    End If
                    Dim sequencia As String = contador.ToString("D4")
                    linha = linha.Substring(0, 496) & sequencia

                    linhasFinal.Add(linha)
                    contador += 1
                Next

                Dim nomeArquivo = "FUNDO SF3 CORRIGIDO " & Date.Now.ToString("ddMMyyyy") & ".txt"
                Dim bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(String.Join(Environment.NewLine, linhasFinal))

                Response.Clear()
                Response.Buffer = True
                Response.Charset = ""
                Response.ContentType = "text/plain"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & nomeArquivo)
                Response.OutputStream.Write(bytes, 0, bytes.Length)
                Response.Flush()
                Response.SuppressContent = True
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alerta", $"alert('Erro ao processar os arquivos: {ex.Message}');", True)
            End Try
        End Sub



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
            Response.Redirect("Menu.aspx")
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