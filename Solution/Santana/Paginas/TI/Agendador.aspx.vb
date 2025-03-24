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
Imports Santana.Seguranca
Imports Util

Namespace Paginas.TI


    Public Class Agendador

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"
        Public Property Newtonsoft As Object

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


            If Not IsPostBack Then
                Dim today As DateTime = DateTime.Now
                Dim previousDate As DateTime

            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub
        Public Class Agenda
            Public Property Historico As String
            Public Property DataPagamento As String
            Public Property Descricao As String
            Public Property ValorBruto As String
            Public Property ValorLiquido As String
            Public Property Favorecido As String
            Public Property CpfCnpj As String
            Public Property FormaPagamento As String
            Public Property Banco As String
            Public Property Agencia As String
            Public Property ContaCorrente As String
            Public Property ArquivoZip As String
        End Class

        Protected Sub btnSalvar_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Using conn As New SqlConnection(strConn)
                Dim sql As String = "INSERT INTO TB_AGENDAMENTO_SIG (Historico, DATA_PAGAMENTO, DESCRICAO, VALOR_BRUTO , VALOR_LIQUIDO, FAVORECIDO, CPF_CNPJ, FORMA_DE_PAGAMENTO, BANCO, AGENCIA, CONTA_CORRENTE)" & "VALUES (@Historico, @DataPagamento, @Descricao, @ValorBruto, @ValorLiquido, @Favorecido, @CpfCnpj, @FormaPagamento, @Banco, @Agencia, @ContaCorrente)"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@Historico", ddlHistorico.SelectedItem.Text)
                    cmd.Parameters.AddWithValue("@DataPagamento", txtDataPagamento.Text)
                    cmd.Parameters.AddWithValue("@Descricao", txtDescricao.Text)
                    cmd.Parameters.AddWithValue("@ValorBruto", txtValorBruto.Text)
                    cmd.Parameters.AddWithValue("@ValorLiquido", txtValorLiquido.Text)
                    cmd.Parameters.AddWithValue("@Favorecido", txtFavorecido.Text)
                    cmd.Parameters.AddWithValue("@CpfCnpj", txtCpfCnpj.Text)
                    cmd.Parameters.AddWithValue("@FormaPagamento", ddlFormaPagamento.SelectedItem.Text)
                    cmd.Parameters.AddWithValue("@Banco", txtBanco.Text)
                    cmd.Parameters.AddWithValue("@Agencia", txtAgencia.Text)
                    cmd.Parameters.AddWithValue("@ContaCorrente", txtContaCorrente.Text)

                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Data inválida!', 'Inserido com Sucesso');", True)


                    Catch ex As Exception


                    Finally
                        conn.Close()
                    End Try

                End Using
            End Using
        End Sub




        Protected Sub ddlHistorico_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            If ddlHistorico.SelectedIndex > 0 Then
                Dim historicoId As String = ddlHistorico.SelectedItem.Value
                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Using conn As New SqlConnection(strConn)
                    Dim sql As String = "SELECT * FROM TB_HISTORICOS WHERE NOME_HISTORICO= @Historico"

                    Using cmd As New SqlCommand(sql, conn)
                        cmd.Parameters.AddWithValue("@Historico", historicoId)

                        Try
                            conn.Open()
                            Dim reader As SqlDataReader = cmd.ExecuteReader()
                            If reader.HasRows Then
                                reader.Read()
                                txtDataPagamento.Text = reader("DATA_PAGAMENTO").ToString()
                                txtDescricao.Text = reader("DESCRICAO").ToString()
                                txtValorBruto.Text = reader("VALOR_BRUTO").ToString()
                                txtValorLiquido.Text = reader("VALOR_LIQUIDO").ToString()
                                txtFavorecido.Text = reader("FAVORECIDO").ToString()
                                txtCpfCnpj.Text = reader("CPF_CNPJ").ToString()
                                ddlFormaPagamento.SelectedValue = reader("FORMA_DE_PAGAMENTO").ToString()
                                txtBanco.Text = reader("BANCO").ToString()
                                txtAgencia.Text = reader("AGENCIA").ToString()
                                txtContaCorrente.Text = reader("CONTA_CORRENTE").ToString()
                            End If
                        Catch ex As Exception
                            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", $"alert('Erro ao buscar dados: {ex.Message}');", True)
                        Finally
                            conn.Close()
                        End Try
                    End Using
                End Using
            End If
        End Sub


        Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
            Dim departamentoSelecionado As String = DropDownList1.SelectedValue

            ' Limpar o DropDownList de histórico antes de carregar novos valores
            ddlHistorico.Items.Clear()

            If Not String.IsNullOrEmpty(departamentoSelecionado) Then
                CarregarHistorico(departamentoSelecionado)
            End If
        End Sub

        Private Sub CarregarHistorico(departamento As String)
            Dim dt As New DataTable()

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Using conn As New SqlConnection(strConn)
                Using cmd As New SqlCommand("SELECT NOME_HISTORICO FROM TB_HISTORICOS WHERE DEPARTAMENTO = @Departamento", conn)
                    cmd.Parameters.AddWithValue("@Departamento", departamento)
                    conn.Open()
                    dt.Load(cmd.ExecuteReader())
                End Using
            End Using

            ddlHistorico.DataSource = dt
            ddlHistorico.DataTextField = "NOME_HISTORICO"
            ddlHistorico.DataBind()


            ddlHistorico.Items.Insert(0, New ListItem("-- Selecione --", ""))
        End Sub

        Protected Sub btnSalvarAgenda_Click(sender As Object, e As EventArgs)
            Try

                Dim listaAgendas As List(Of Agenda) = TryCast(Session("Agendas"), List(Of Agenda))
                If listaAgendas Is Nothing Then
                    listaAgendas = New List(Of Agenda)()
                End If
                Dim novaAgenda As New Agenda() With {
            .Historico = ddlHistorico.SelectedItem.Text,
            .DataPagamento = txtDataPagamento.Text,
            .Descricao = txtDescricao.Text,
            .ValorBruto = txtValorBruto.Text,
            .ValorLiquido = txtValorLiquido.Text,
            .Favorecido = txtFavorecido.Text,
            .CpfCnpj = txtCpfCnpj.Text,
            .FormaPagamento = ddlFormaPagamento.SelectedItem.Text,
            .Banco = txtBanco.Text,
            .Agencia = txtAgencia.Text,
            .ContaCorrente = txtContaCorrente.Text,
            .ArquivoZip = ""
        }
                Dim uploadPath As String = Server.MapPath("~/Uploads/")
                If Not Directory.Exists(uploadPath) Then
                    Directory.CreateDirectory(uploadPath)
                End If
                If FileUpload1.HasFiles Then
                    Dim zipFileName As String = Path.Combine(uploadPath, "AGENDAMENTO_" & txtDescricao.Text & ".zip")

                    Using zip As New Ionic.Zip.ZipFile()
                        For Each file As HttpPostedFile In FileUpload1.PostedFiles
                            Dim fileData As Byte()
                            Using inputStream As New MemoryStream()
                                file.InputStream.CopyTo(inputStream)
                                fileData = inputStream.ToArray()
                            End Using
                            zip.AddEntry(Path.GetFileName(file.FileName), fileData)
                        Next
                        zip.Save(zipFileName)
                    End Using
                    novaAgenda.ArquivoZip = zipFileName
                End If
                listaAgendas.Add(novaAgenda)
                Session("Agendas") = listaAgendas
                ddlHistorico.SelectedIndex = 0
                txtDataPagamento.Text = ""
                txtDescricao.Text = ""
                txtValorBruto.Text = ""
                txtValorLiquido.Text = ""
                txtFavorecido.Text = ""
                txtCpfCnpj.Text = ""
                ddlFormaPagamento.SelectedIndex = 0
                txtBanco.Text = ""
                txtAgencia.Text = ""
                txtContaCorrente.Text = ""
                BindAgendas()
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('Agenda salva com sucesso!');", True)

            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", $"alert('Erro ao salvar agenda: {ex.Message}');", True)
            End Try
        End Sub
        Private Sub BindAgendas()
            Dim listaAgendas As List(Of Agenda) = TryCast(Session("Agendas"), List(Of Agenda))
            gvAgendas.DataSource = listaAgendas
            gvAgendas.DataBind()
        End Sub

        Protected Sub btnExcluirAgenda_Click(sender As Object, e As EventArgs)
            Try
                Dim btn As Button = CType(sender, Button)
                Dim index As Integer = Convert.ToInt32(btn.CommandArgument)
                Dim listaAgendas As List(Of Agenda) = TryCast(Session("Agendas"), List(Of Agenda))
                If listaAgendas IsNot Nothing AndAlso listaAgendas.Count > index Then
                    listaAgendas.RemoveAt(index)
                    Session("Agendas") = listaAgendas
                    BindAgendas()
                End If
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", $"alert('Erro ao excluir agenda: {ex.Message}');", True)
            End Try
        End Sub

        Protected Sub btnEnviarEmail_Click(sender As Object, e As EventArgs)
            Try
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim email As New MailMessage()
                Dim contexto = New Contexto
                Dim dataPagamento As String = txtDataPagamento.Text
                email.From = New MailAddress("menoti@sf3.com.br")
                email.To.Add(ddlAprovador.SelectedItem.Value)
                email.Subject = $"SOLICITAÇÃO DE PAGAMENTO SHOPCRED - {dataPagamento} "
                email.IsBodyHtml = True
                Dim listaAgendas As List(Of Agenda) = TryCast(Session("Agendas"), List(Of Agenda))
                For Each agenda In listaAgendas
                    agenda.DataPagamento = dataPagamento
                Next
                Dim body As String = "<h3>Informações de Despesas</h3>"
                body &= "<table border='1' cellpadding='10' cellspacing='0' style='border-collapse:collapse;'>"
                body &= "<tr><th>Data Pagamento</th><th>Descrição</th><th>Valor Bruto</th><th>Valor Líquido</th><th>Favorecido</th><th>CPF/CNPJ</th><th>Forma de Pagamento</th><th>Banco</th><th>Agência</th><th>Conta Corrente</th> </tr>"
                For Each agenda In listaAgendas
                    body &= $"<tr><td>{agenda.DataPagamento}</td><td>{agenda.Descricao}</td><td>R$ {agenda.ValorBruto}</td><td>R$ {agenda.ValorLiquido}</td><td>{agenda.Favorecido}</td><td>{agenda.CpfCnpj}</td><td>{agenda.FormaPagamento}</td><td>{agenda.Banco}</td><td>{agenda.Agencia}</td><td>{agenda.ContaCorrente}</td></tr>"
                Next
                body &= "</br>"
                body &= "</table>"
                body &= $"Digitador: {contexto.UsuarioLogado.NomeUsuario}"
                Dim av As AlternateView = AlternateView.CreateAlternateViewFromString(body, Encoding.UTF8, "text/html")
                email.AlternateViews.Add(av)
                Dim anexosAdicionados As Integer = 0
                For Each agenda In listaAgendas
                    If Not String.IsNullOrEmpty(agenda.ArquivoZip) AndAlso File.Exists(agenda.ArquivoZip) Then
                        email.Attachments.Add(New Attachment(agenda.ArquivoZip))
                        anexosAdicionados += 1
                    End If
                Next
                Dim smtp As New SmtpClient("smtp.office365.com")
                smtp.Port = 587
                smtp.Credentials = New Net.NetworkCredential("menoti@sf3.com.br", "Huq99291")
                smtp.EnableSsl = True
                smtp.Send(email)
                Session("Agendas") = Nothing

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('E-mail enviado com sucesso!');", True)

            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", $"alert('Erro ao enviar e-mail: {ex.Message}');", True)
            End Try
        End Sub

        Protected Sub btnReiniciar_Click(sender As Object, e As EventArgs)
            Try

                Session.Remove("Agendas")
                HttpContext.Current.Cache.Remove("Agendas")


                ddlAprovador.SelectedIndex = 0
                ddlHistorico.SelectedIndex = 0
                txtDescricao.Text = ""
                txtDataPagamento.Text = ""
                txtValorBruto.Text = ""
                txtValorLiquido.Text = ""
                txtFavorecido.Text = ""
                txtCpfCnpj.Text = ""
                ddlFormaPagamento.SelectedIndex = 0
                txtBanco.Text = ""
                txtAgencia.Text = ""
                txtContaCorrente.Text = ""
                BindAgendas()


                Response.Redirect(Request.RawUrl, False)
                HttpContext.Current.ApplicationInstance.CompleteRequest()



            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", $"alert('Erro ao reiniciar: {ex.Message}');", True)
            End Try
        End Sub


        Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../Menu.aspx")
        End Sub

    End Class
End Namespace