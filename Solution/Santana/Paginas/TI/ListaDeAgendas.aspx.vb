Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Componentes
Imports Microsoft.VisualBasic
Imports Santana.Paginas.TI.Agendador
Imports Santana.Seguranca
Imports Util

Namespace Paginas.TI


    Public Class ListaDeAgendas

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"
        Public Property Newtonsoft As Object

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            If Not IsPostBack Then
                Dim today As DateTime = DateTime.Now
                CarregarAgenda()
            End If


            Dim contexto As New Contexto()

            If Not contexto.PossuiPerfil(1) Then

                btnGerenciamento.Visible = False
            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)



        End Sub

        Protected Sub BuscarAgenda_Click(sender As Object, ByVal e As EventArgs)
            Dim numeroAgenda As String = txtNumeroAgenda.Text

            If String.IsNullOrEmpty(numeroAgenda) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Por favor, informe o número da agenda.');", True)
                Return
            End If

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Using conn As New SqlConnection(strConn)
                conn.Open()

                Dim sql As String = "SELECT HISTORICO, DATA_PAGAMENTO, DESCRICAO, VALOR_BRUTO, VALOR_LIQUIDO, 
                             FAVORECIDO, CPF_CNPJ, FORMA_DE_PAGAMENTO, BANCO, AGENCIA, CONTA_CORRENTE 
                             FROM TB_AGENDAMENTO_SIG WHERE ID = @ID"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@ID", numeroAgenda)

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            txtEditarHistorico.Text = reader("HISTORICO").ToString()
                            txtEditarDataPagamento.Text = reader("DATA_PAGAMENTO").ToString()
                            txtEditarDescricao.Text = reader("DESCRICAO").ToString()
                            txtEditarValorBruto.Text = reader("VALOR_BRUTO").ToString()
                            txtEditarValorLiquido.Text = reader("VALOR_LIQUIDO").ToString()
                            txtEditarFavorecido.Text = reader("FAVORECIDO").ToString()
                            txtEditarCpfCnpj.Text = reader("CPF_CNPJ").ToString()
                            txtEditarFormaPagamento.Text = reader("FORMA_DE_PAGAMENTO").ToString()
                            txtEditarBanco.Text = reader("BANCO").ToString()
                            txtEditarAgencia.Text = reader("AGENCIA").ToString()
                            txtEditarContaCorrente.Text = reader("CONTA_CORRENTE").ToString()
                        Else
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Agenda não encontrada.');", True)
                        End If
                    End Using

                    conn.Close()

                End Using
            End Using
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowModalScript", "abrirModal();", True)
        End Sub


        Protected Sub SalvarEdicao(idAgenda1 As Integer, historico As String, dataPagamento As String, descricao As String, valorBruto As String, valorLiquido As String, favorecido As String, cpfCnpj As String, formaPagamento As String, banco As String, agencia As String, contaCorrente As String)
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Using conn As New SqlConnection(strConn)
                conn.Open()
                Dim sql As String = "UPDATE TB_AGENDAMENTO_SIG SET HISTORICO = @Historico, DATA_PAGAMENTO = @DataPagamento, DESCRICAO = @Descricao, VALOR_BRUTO = @ValorBruto, VALOR_LIQUIDO = @ValorLiquido, FAVORECIDO = @Favorecido, CPF_CNPJ = @CpfCnpj, FORMA_DE_PAGAMENTO = @FormaPagamento, BANCO = @Banco, AGENCIA = @Agencia, CONTA_CORRENTE = @ContaCorrente WHERE ID = @Id"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@Id", idAgenda1)
                    cmd.Parameters.AddWithValue("@Historico", historico)
                    cmd.Parameters.AddWithValue("@DataPagamento", dataPagamento)
                    cmd.Parameters.AddWithValue("@Descricao", descricao)
                    cmd.Parameters.AddWithValue("@ValorBruto", valorBruto)
                    cmd.Parameters.AddWithValue("@ValorLiquido", valorLiquido)
                    cmd.Parameters.AddWithValue("@Favorecido", favorecido)
                    cmd.Parameters.AddWithValue("@CpfCnpj", cpfCnpj)
                    cmd.Parameters.AddWithValue("@FormaPagamento", formaPagamento)
                    cmd.Parameters.AddWithValue("@Banco", banco)
                    cmd.Parameters.AddWithValue("@Agencia", agencia)
                    cmd.Parameters.AddWithValue("@ContaCorrente", contaCorrente)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Response.Redirect(Request.Url.AbsoluteUri)
        End Sub



        Protected Sub AtualizarStatusAgendas(sender As Object, e As EventArgs)

            Dim agendaID As String = txtId.Text
            Dim status As String = ddlStatus.SelectedItem.Value
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Using conn As New SqlConnection(strConn)
                conn.Open()
                Dim sql As String = "UPDATE TB_AGENDAMENTO_SIG SET STATUS = @STATUS WHERE ID = @ID"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@STATUS", status)
                    cmd.Parameters.AddWithValue("@ID", agendaID)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Response.Redirect(Request.Url.AbsoluteUri)
        End Sub

        Protected Sub AdicionarArquivoAgenda(sender As Object, e As EventArgs)
            If txtId Is Nothing OrElse String.IsNullOrEmpty(txtId.Text) Then
                Throw New NullReferenceException("O controle txtId não foi inicializado ou está vazio.")
            End If


            If Not FileUpload.HasFile Then
                Throw New NullReferenceException("Nenhum arquivo foi enviado.")
            End If

            Dim agendaID As String = txtId.Text
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim caminhoPasta As String = ""
            Using conn As New SqlConnection(strConn)
                conn.Open()
                Dim sql As String = "SELECT ARQUIVOSPASTA FROM TB_AGENDAMENTO_SIG WHERE ID = @ID"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@ID", agendaID)
                    Dim result = cmd.ExecuteScalar()
                    If result Is Nothing OrElse IsDBNull(result) Then
                        Throw New NullReferenceException("O caminho da pasta não foi encontrado no banco de dados.")
                    End If
                    caminhoPasta = result.ToString().Trim()
                End Using
            End Using

            If String.IsNullOrEmpty(caminhoPasta) Then
                Throw New NullReferenceException("O caminho da pasta está vazio.")
            End If

            If Not Directory.Exists(caminhoPasta) Then
                Directory.CreateDirectory(caminhoPasta)
            End If

            For Each file As HttpPostedFile In FileUpload.PostedFiles
                If file IsNot Nothing AndAlso file.ContentLength > 0 Then
                    Dim filePath As String = Path.Combine(caminhoPasta, Path.GetFileName(file.FileName))
                    file.SaveAs(filePath)
                End If
            Next

            Response.Redirect(Request.Url.AbsoluteUri)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Atualização' ,'Arquivo Anexado com sucesso!!!.', 'success');", True)
        End Sub
        Protected Sub ExcluirAgenda(sender As Object, e As EventArgs)
            Dim agendaID As String = txtId.Text
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Using conn As New SqlConnection(strConn)

                conn.Open()
                Dim sql As String = "DELETE FROM TB_AGENDAMENTO_SIG WHERE ID = @ID"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@ID", agendaID)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Response.Redirect(Request.Url.AbsoluteUri)
        End Sub

        Protected Sub CarregarAgenda()
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim contexto As New Contexto()
            Dim usuarioLogado As String = contexto.UsuarioLogado.NomeCompleto
            Dim possuiAcessoTotal As Boolean = contexto.PossuiPerfil(1)

            Using conn As New SqlConnection(strConn)
                conn.Open()
                Dim sql As String
                If possuiAcessoTotal Then

                    sql = "SELECT ID, DATA_DA_AGENDA, DIGITADOR, DEPARTAMENTO, DESCRICAO, FAVORECIDO, DATA_PAGAMENTO, APROVADOR, STATUS " &
                  "FROM TB_AGENDAMENTO_SIG " &
                  "ORDER BY ID DESC"
                Else

                    sql = "SELECT ID, DATA_DA_AGENDA, DIGITADOR, DEPARTAMENTO, DESCRICAO, FAVORECIDO, DATA_PAGAMENTO, APROVADOR, STATUS " &
                  "FROM TB_AGENDAMENTO_SIG " &
                  "WHERE DIGITADOR = @UsuarioLogado " &
                  "ORDER BY ID DESC"
                End If
                Using cmd As New SqlCommand(sql, conn)

                    If Not possuiAcessoTotal Then
                        cmd.Parameters.AddWithValue("@UsuarioLogado", usuarioLogado)
                    End If

                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)

                    Dim html As New StringBuilder()
                    For Each row As DataRow In dt.Rows
                        Dim idAgenda As String = row("ID").ToString()
                        Dim idAgenda1 As String = row("ID").ToString()
                        Dim status As String = row("STATUS").ToString()
                        Dim statusClass As String = GetStatusClass(status)
                        html.Append("<tr>")
                        html.Append($"<td style='text-align:center;'>{row("ID")}</td>")
                        html.Append($"<td style='text-align:center;'>{row("DATA_DA_AGENDA")}</td>")
                        html.Append($"<td style='text-align:center;'>{row("DIGITADOR")}</td>")
                        html.Append($"<td style='text-align:center;'>{row("DEPARTAMENTO")}</td>")
                        html.Append($"<td style='text-align:center;'>{row("DESCRICAO")}</td>")
                        html.Append($"<td style='text-align:center;'>{row("FAVORECIDO")}</td>")
                        html.Append($"<td style='text-align:center;'>{row("DATA_PAGAMENTO")}</td>")
                        html.Append($"<td style='text-align:center;'>{row("APROVADOR")}</td>")
                        html.Append($"<td style='text-align:center;'><span class='{statusClass}'>{status}</span></td>")
                        html.Append("<td style='text-align:center;'>")
                        html.Append($"<button type='button' class='btn btn-light' data-toggle='modal' data-target='#modalVisualizarAgenda{idAgenda}'><i class=""bi bi-eye""></i></button>")
                        html.Append("<button type='button' style='margin-left:10px;' class='btn btn-light' onclick='imprimirModalConteudo(" & idAgenda & ")'><i class=""bi bi-printer""></i></button>")
                        html.Append("</td>")
                        html.Append("</tr>")
                        html.Append(GerarModal(idAgenda))

                    Next

                    litTabela.Text = html.ToString()
                End Using
            End Using
        End Sub


        Private Function GerarModal(idAgenda As String) As String
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Using conn As New SqlConnection(strConn)
                conn.Open()

                Dim sql As String = "SELECT HISTORICO, DATA_PAGAMENTO, DESCRICAO, VALOR_BRUTO, VALOR_LIQUIDO, 
                            FAVORECIDO, CPF_CNPJ, FORMA_DE_PAGAMENTO, BANCO, AGENCIA, CONTA_CORRENTE, 
                            DIGITADOR, DEPARTAMENTO, DATA_DA_AGENDA, APROVADOR, ARQUIVOSPASTA, STATUS
                     FROM TB_AGENDAMENTO_SIG WHERE ID = @ID"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@ID", idAgenda)

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim modalHtml As New StringBuilder()
                            Dim caminhoPasta As String = If(IsDBNull(reader("ARQUIVOSPASTA")), "", reader("ARQUIVOSPASTA").ToString().Trim())

                            modalHtml.Append($"<div class='modal fade' id='modalVisualizarAgenda{idAgenda}' tabindex='-1' aria-labelledby='modalLabel{idAgenda}' aria-hidden='true'>")
                            modalHtml.Append("<div class='modal-dialog'>")
                            modalHtml.Append("<div class='modal-content'>")
                            modalHtml.Append("<div class='modal-header'>")
                            modalHtml.Append($"<h5 class='modal-title' id='modalLabel{idAgenda}'>Detalhes da Agenda</h5>")
                            modalHtml.Append("</div>")
                            modalHtml.Append("<div class='modal-body'>")
                            modalHtml.Append($"<p><strong>Histórico:</strong> {reader("HISTORICO").ToString()}</p>")
                            modalHtml.Append($"<p><strong>Data de Pagamento:</strong> {reader("DATA_PAGAMENTO").ToString()}</p>")
                            modalHtml.Append($"<p><strong>Descrição:</strong> {reader("DESCRICAO").ToString()}</p>")
                            modalHtml.Append($"<p><strong>Valor Bruto:</strong> {reader("VALOR_BRUTO").ToString()}</p>")
                            modalHtml.Append($"<p><strong>Valor Líquido:</strong> {reader("VALOR_LIQUIDO").ToString()}</p>")
                            modalHtml.Append($"<p><strong>Favorecido:</strong> {reader("FAVORECIDO").ToString()}</p>")
                            modalHtml.Append($"<p><strong>CPF/CNPJ:</strong> {reader("CPF_CNPJ").ToString()}</p>")
                            modalHtml.Append($"<p><strong>Forma de Pagamento:</strong> {reader("FORMA_DE_PAGAMENTO").ToString()}</p>")
                            modalHtml.Append($"<p><strong>Banco:</strong> {reader("BANCO").ToString()}</p>")
                            modalHtml.Append($"<p><strong>Agência:</strong> {reader("AGENCIA").ToString()}</p>")
                            modalHtml.Append($"<p><strong>Conta Corrente:</strong> {reader("CONTA_CORRENTE").ToString()}</p>")
                            modalHtml.Append($"<p><strong>Digitador:</strong> {reader("DIGITADOR").ToString()}</p>")
                            modalHtml.Append($"<p><strong>Departamento:</strong> {reader("DEPARTAMENTO").ToString()}</p>")
                            modalHtml.Append($"<p><strong>Data da Agenda:</strong> {reader("DATA_DA_AGENDA").ToString()}</p>")
                            modalHtml.Append($"<p><strong>Aprovador:</strong> {reader("APROVADOR").ToString()}</p>")
                            modalHtml.Append($"<p><strong>Status:</strong> {reader("STATUS").ToString()}</p>")
                            If Not String.IsNullOrEmpty(caminhoPasta) AndAlso Directory.Exists(caminhoPasta) Then
                                Dim arquivos As String() = Directory.GetFiles(caminhoPasta)

                                If arquivos.Length > 0 Then
                                    modalHtml.Append("<h5>Arquivos Relacionados:</h5>")
                                    modalHtml.Append("<ul>")
                                    For Each arquivo As String In arquivos
                                        Dim nomeArquivo As String = Path.GetFileName(arquivo)
                                        Dim urlArquivo As String = ResolveUrl("~/FileDownloadHandler.ashx?filePath=" & HttpUtility.UrlEncode(arquivo))
                                        modalHtml.Append($"<li><a href='{urlArquivo}' download='{nomeArquivo}' target='_blank'>{nomeArquivo}</a></li>")
                                    Next
                                    modalHtml.Append("</ul>")
                                Else
                                    modalHtml.Append("<p><strong>Arquivos Relacionados:</strong> Nenhum arquivo encontrado.</p>")
                                End If
                            Else
                                modalHtml.Append("<p><strong>Arquivos Relacionados:</strong> Caminho da pasta não definido ou inexistente.</p>")
                            End If
                            modalHtml.Append("<hr>")
                            modalHtml.Append("<div class='modal-footer'>")
                            modalHtml.Append("<button type='button' class='btn btn-secondary' data-dismiss='modal'>Fechar</button>")
                            modalHtml.Append("</div>")
                            modalHtml.Append("</div>")
                            modalHtml.Append("</div>")
                            modalHtml.Append("</div>")

                            Return modalHtml.ToString()
                        Else
                            Return ""
                        End If
                    End Using
                End Using
            End Using
        End Function

        Private Function GetStatusClass(status As String) As String
            Select Case status.ToUpper()
                Case "DIGITADO"
                    Return "status-digitado"
                Case "ENVIADO"
                    Return "status-enviado"
                Case "RECUSADO"
                    Return "status-recusado"
                Case "PAGO"
                    Return "status-pago"
                Case Else
                    Return ""
            End Select
        End Function

        Protected Sub UploadArquivo(sender As Object, e As EventArgs)
            Dim agendaId As String = Request.Form("agendaId")
            Dim caminhoPasta As String = ""
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Using conn As New SqlConnection(strConn)
                conn.Open()
                Dim sql As String = "SELECT ARQUIVOSPASTA FROM TB_AGENDAMENTO_SIG WHERE ID = @ID"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@ID", agendaId)
                    caminhoPasta = cmd.ExecuteScalar().ToString().Trim()
                End Using
            End Using
            If Not Directory.Exists(caminhoPasta) Then
                Directory.CreateDirectory(caminhoPasta)
            End If
            For Each file As HttpPostedFile In Request.Files
                If file.ContentLength > 0 Then
                    Dim filePath As String = Path.Combine(caminhoPasta, Path.GetFileName(file.FileName))
                    file.SaveAs(filePath)
                End If
            Next
            Response.Redirect(Request.Url.AbsoluteUri)
        End Sub
        Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../Menu.aspx")
        End Sub

        Protected Sub btnAgendador_Click(sender As Object, e As EventArgs)
            Response.Redirect("../Paginas/TI/CadastroHistorico.aspx")
        End Sub




    End Class
End Namespace