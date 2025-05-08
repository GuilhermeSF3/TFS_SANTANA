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
                ClientScript.RegisterStartupScript(Me.GetType(), "initUpload", "Sys.Application.add_load(function() { mostrarArquivos(document.getElementById('" & FileUpload1.ClientID & "')); });", True)

            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub
        Public Class Agenda
            Public Property Id As Integer
            Public Property Historico As String

            Public Property Empresa As String
            Public Property Departamento As String
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

        Protected Function SalvarArquivosEmPasta(ByVal fileUpload As FileUpload, ByVal agendaId As Integer) As String
            Dim uploadPath As String = Path.Combine("C:\Agendador\ARQUIVOSPASTA", agendaId.ToString())
            If Not Directory.Exists(uploadPath) Then
                Directory.CreateDirectory(uploadPath)
                Debug.WriteLine("Pasta criada: " & uploadPath)
            Else
                Debug.WriteLine("Pasta já existia: " & uploadPath)
            End If
            If fileUpload.HasFiles Then
                Debug.WriteLine("Quantidade de arquivos recebidos: " & fileUpload.PostedFiles.Count)

                For Each file As HttpPostedFile In fileUpload.PostedFiles
                    If file IsNot Nothing AndAlso file.ContentLength > 0 Then
                        Dim filePath As String = Path.Combine(uploadPath, Path.GetFileName(file.FileName))
                        file.SaveAs(filePath)
                        Debug.WriteLine("Arquivo salvo: " & filePath)
                    Else
                        Debug.WriteLine("Arquivo ignorado: " & file.FileName & " (Vazio ou inválido)")
                    End If
                Next
            Else
                Debug.WriteLine("Nenhum arquivo recebido")
            End If
            Return uploadPath
            Return "~/uploads/" & agendaId.ToString()
        End Function

        Protected Sub SalvarAgenda(ByVal agenda As Agenda, ByVal arquivos As List(Of HttpPostedFile))
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim contexto = New Contexto
            Using conn As New SqlConnection(strConn)
                conn.Open()
                Dim sql As String = "INSERT INTO TB_AGENDAMENTO_SIG 
                (Empresa, Historico, DATA_PAGAMENTO, DESCRICAO, VALOR_BRUTO , VALOR_LIQUIDO, FAVORECIDO, CPF_CNPJ, FORMA_DE_PAGAMENTO, BANCO, AGENCIA, CONTA_CORRENTE, DIGITADOR, DEPARTAMENTO, DATA_DA_AGENDA, APROVADOR, EMAIL_DIGITADOR) 
                             VALUES (@Empresa, @Historico, @DataPagamento, @Descricao, @ValorBruto, @ValorLiquido, @Favorecido, @CpfCnpj, @FormaPagamento, @Banco, @Agencia, @ContaCorrente, @Digitador, @departamento, @data_da_agenda, @Aprovador, @email_digitador);
                             SELECT SCOPE_IDENTITY();"

                Dim agendaId As Integer

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@Empresa", agenda.Empresa)
                    cmd.Parameters.AddWithValue("@Historico", agenda.Historico)
                    cmd.Parameters.AddWithValue("@DataPagamento", agenda.DataPagamento)
                    cmd.Parameters.AddWithValue("@Descricao", agenda.Descricao)
                    cmd.Parameters.AddWithValue("@ValorBruto", agenda.ValorBruto)
                    cmd.Parameters.AddWithValue("@ValorLiquido", agenda.ValorLiquido)
                    cmd.Parameters.AddWithValue("@Favorecido", agenda.Favorecido)
                    cmd.Parameters.AddWithValue("@CpfCnpj", agenda.CpfCnpj)
                    cmd.Parameters.AddWithValue("@FormaPagamento", agenda.FormaPagamento)
                    cmd.Parameters.AddWithValue("@Banco", agenda.Banco)
                    cmd.Parameters.AddWithValue("@Agencia", agenda.Agencia)
                    cmd.Parameters.AddWithValue("@ContaCorrente", agenda.ContaCorrente)
                    cmd.Parameters.AddWithValue("@Digitador", contexto.UsuarioLogado.NomeCompleto)
                    cmd.Parameters.AddWithValue("@Departamento", DropDownList1.SelectedValue)
                    cmd.Parameters.AddWithValue("@data_da_agenda", DateTime.Now)
                    cmd.Parameters.AddWithValue("@Aprovador", ddlAprovador.SelectedValue)
                    cmd.Parameters.AddWithValue("@email_digitador", contexto.UsuarioLogado.EMail)

                    agendaId = Convert.ToInt32(cmd.ExecuteScalar())
                End Using

                agenda.Id = agendaId

                Dim caminhoArquivos As String = SalvarArquivosEmPasta(FileUpload1, agendaId)
                Dim sqlUpdate As String = "UPDATE TB_AGENDAMENTO_SIG SET ArquivosPasta = @ArquivosPasta WHERE ID = @IdAgendamento"
                Using cmdUpdate As New SqlCommand(sqlUpdate, conn)
                    cmdUpdate.Parameters.AddWithValue("@ArquivosPasta", caminhoArquivos)
                    cmdUpdate.Parameters.AddWithValue("@IdAgendamento", agendaId)
                    cmdUpdate.ExecuteNonQuery()
                End Using


                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Inserido com Sucesso');", True)
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

        Protected Sub btnSalvarAgenda_Click()
            Try

                ' Verificar se os campos obrigatórios estão preenchidos
                Dim erros As New List(Of String)

                If String.IsNullOrEmpty(txtDataPagamento.Text) Then
                    erros.Add("O campo Data de Pagamento é obrigatório.")
                    rfvDataPagamento.IsValid = False
                End If

                If String.IsNullOrEmpty(txtDescricao.Text) Then
                    erros.Add("O campo Descrição é obrigatório.")
                    rfvDescricao.IsValid = False
                End If

                If String.IsNullOrEmpty(txtValorBruto.Text) Then
                    erros.Add("O campo Valor Bruto é obrigatório.")
                    rfvValorBruto.IsValid = False
                End If


                If String.IsNullOrEmpty(ddlEmpresa.SelectedValue) Then
                    erros.Add("O campo Empresa é obrigatório.")
                    rfvEmpresa.IsValid = False
                End If

                If String.IsNullOrEmpty(ddlAprovador.SelectedValue) Then
                    erros.Add("O campo Empresa é obrigatório.")
                    rfvAprovador.IsValid = False
                End If

                If String.IsNullOrEmpty(DropDownList1.SelectedValue) Then
                    erros.Add("O campo Empresa é obrigatório.")
                    rfvDepartamento.IsValid = False
                End If

                If String.IsNullOrEmpty(txtValorLiquido.Text) Then
                    erros.Add("O campo Valor Líquido é obrigatório.")
                    rfvValorLiquido.IsValid = False
                End If

                If String.IsNullOrEmpty(txtFavorecido.Text) Then
                    erros.Add("O campo Favorecido é obrigatório.")
                    rfvFavorecido.IsValid = False
                End If

                If String.IsNullOrEmpty(txtCpfCnpj.Text) Then
                    erros.Add("O campo CPF/CNPJ é obrigatório.")
                    rfvCpfCnpj.IsValid = False
                End If

                If String.IsNullOrEmpty(ddlFormaPagamento.SelectedValue) Then
                    erros.Add("O campo Forma de Pagamento é obrigatório.")
                    rfvFormaPagamento.IsValid = False
                End If

                ' Se houver erros, exibir mensagens e interromper o processo
                If erros.Count > 0 Then
                    For Each erro In erros
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", $"alert('{erro}');", True)
                    Next
                    Return
                End If

                Dim listaAgendas As List(Of Agenda) = TryCast(Session("Agendas"), List(Of Agenda))
                If listaAgendas Is Nothing Then
                    listaAgendas = New List(Of Agenda)()
                End If
                Dim novaAgenda As New Agenda() With {
                         .Empresa = ddlEmpresa.SelectedItem.Text,
            .Historico = If(ddlHistorico.SelectedItem IsNot Nothing, ddlHistorico.SelectedItem.Text, String.Empty),
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
                Dim uploadPath As String = "C:\Agendador\ARQUIVOS"
                '"\\192.168.0.230\dados\Agendador\ARQUIVOS"
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
                Dim arquivos As New List(Of HttpPostedFile)()
                SalvarAgenda(novaAgenda, arquivos)

                listaAgendas.Add(novaAgenda)
                Session("Agendas") = listaAgendas
                ddlHistorico.SelectedIndex = 0
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

            If listaAgendas IsNot Nothing AndAlso listaAgendas.Count > 0 Then
                btnEnviarEmail.Enabled = True
            Else
                btnEnviarEmail.Enabled = False
            End If
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
                Dim empresa As String = ddlEmpresa.SelectedValue
                Dim contexto = New Contexto
                Dim dataPagamento As String = txtDataPagamento.Text
                email.From = New MailAddress("contasapagar@santanafinanceira.onmicrosoft.com")
                email.To.Add(ddlAprovador.SelectedItem.Value)
                email.CC.Add(contexto.UsuarioLogado.EMail)
                email.Subject = $"SOLICITAÇÃO DE PAGAMENTO {empresa} - {dataPagamento} "
                email.IsBodyHtml = True
                Dim listaAgendas As List(Of Agenda) = TryCast(Session("Agendas"), List(Of Agenda))
                For Each agenda In listaAgendas
                    agenda.DataPagamento = dataPagamento
                Next
                Dim body As String = "<h3>Informações de Despesas</h3>"
                body &= "<table border='1' cellpadding='5'  cellspacing='0' style='border-collapse:collapse; font-style: 10px;'>"
                body &= "<tr><th>Data Pagamento</th><th>Descrição</th><th>Valor Bruto</th><th>Valor Líquido</th><th>Favorecido</th><th>CPF/CNPJ</th><th>Forma de Pagamento</th><th>Banco</th><th>Agência</th><th>Conta Corrente</th> </tr>"
                Dim ids As New List(Of String)()
                For Each agenda In listaAgendas
                    body &= $"<tr><td>{agenda.DataPagamento}</td><td>{agenda.Descricao}</td><td>{agenda.ValorBruto}</td><td>{agenda.ValorLiquido}</td><td>{agenda.Favorecido}</td><td>{agenda.CpfCnpj}</td><td>{agenda.FormaPagamento}</td><td>{agenda.Banco}</td><td>{agenda.Agencia}</td><td>{agenda.ContaCorrente}</td></tr>"
                    ids.Add(agenda.Id.ToString())
                Next
                body &= "</br>"
                body &= "</table>"
                body &= $"Digitador: {contexto.UsuarioLogado.NomeUsuario}"
                body &= "</br>"
                body &= $"Empresa: {empresa}"
                body &= "</br>"


                Dim idsParam As String = String.Join(",", ids)
                Dim approveUrl As String = $"http://192.168.0.227:180/Paginas/TI/AgendaAprovar.aspx?ids={idsParam}&ReturnUrl={HttpUtility.UrlEncode(Request.Url.ToString())}"
                Dim rejectUrl As String = $"http://192.168.0.227:180/Paginas/TI/AgendaRecusar.aspx?ids={idsParam}&ReturnUrl={HttpUtility.UrlEncode(Request.Url.ToString())}"
                body &= $"<a href='{approveUrl}' style='padding: 10px; background-color: green; color: white; text-decoration: none; margin-right: 10px;'>Aprovar</a>"
                body &= $"<a href='{rejectUrl}' style='padding: 10px; background-color: red; color: white; text-decoration: none;'>Recusar</a>"


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
                smtp.Credentials = New Net.NetworkCredential("contasapagar@santanafinanceira.onmicrosoft.com", "Xay16092")
                smtp.EnableSsl = True
                smtp.Send(email)
                Session("Agendas") = Nothing

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "alert('E-mail enviado com sucesso!');", True)

            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", $"alert('Erro ao enviar e-mail: {ex.Message}');", True)
            End Try
        End Sub
        Protected Sub btnSalvarAgendaSelected(sender As Object, e As EventArgs)
            btnSalvarAgenda_Click()
        End Sub

        Protected Sub btnReiniciar_Click(sender As Object, e As EventArgs)
            Try

                ddlEmpresa.SelectedIndex = 0
                ddlAprovador.SelectedIndex = 0
                DropDownList1.SelectedIndex = 0

                ddlHistorico.Items.Clear()
                ddlHistorico.Items.Insert(0, New ListItem("-- Selecione --", ""))


                txtDescricao.Text = ""
                txtDataPagamento.Text = ""
                txtValorBruto.Text = ""
                txtValorLiquido.Text = ""
                txtFavorecido.Text = ""
                txtCpfCnpj.Text = ""
                txtBanco.Text = ""
                txtAgencia.Text = ""
                txtContaCorrente.Text = ""


                ddlFormaPagamento.SelectedIndex = 0


                Session.Remove("Agendas")
                HttpContext.Current.Cache.Remove("Agendas")


                Page.Validate()
                For Each validator As BaseValidator In Page.Validators
                    validator.IsValid = True
                Next


                BindAgendas()


                UpdatePanel.Update()

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