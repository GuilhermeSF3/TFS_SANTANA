Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Componentes
Imports Ionic.Zip
Imports Microsoft.VisualBasic
Imports Santana.Paginas.TI.Agendador
Imports Santana.Seguranca
Imports Util

Namespace Paginas.TI


    Public Class AgendaAprovar

        Inherits SantanaPage

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            If Not IsPostBack Then
                Dim ids As String = Request.QueryString("ids")
                If Not String.IsNullOrEmpty(ids) Then
                    CarregarAgendas(ids)
                End If
            End If
        End Sub

        Private Sub CarregarAgendas(ByVal ids As String)
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Using conn As New SqlConnection(strConn)
                conn.Open()
                Dim sql As String = "SELECT * FROM TB_AGENDAMENTO_SIG WHERE ID IN (" & ids & ")"
                Using cmd As New SqlCommand(sql, conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        Dim dt As New DataTable()
                        dt.Load(reader)
                        gvAgendas.DataSource = dt
                        gvAgendas.DataBind()
                        pnlAgendas.Visible = True
                    End Using
                End Using
            End Using
        End Sub

        Protected Sub btnModalConfirmar_Click(ByVal sender As Object, ByVal e As EventArgs)
            If chkConfirm.Checked Then
                Dim ids As String = Request.QueryString("ids")
                If Not String.IsNullOrEmpty(ids) Then
                    AprovarAgendas(ids)
                End If
            Else
                lblMessage.Text = "Por favor, confirme a aprovação."
            End If
        End Sub

        Private Sub AprovarAgendas(ByVal ids As String)
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Using conn As New SqlConnection(strConn)
                conn.Open()
                Dim sql As String = "UPDATE TB_AGENDAMENTO_SIG SET STATUS = 'ENVIADO' WHERE ID IN (" & ids & ")"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            lblMessage.Text = "Agendas aprovadas com sucesso."
            pnlAgendas.Visible = False
            EnviarEmails(ids)
        End Sub

        Private Sub EnviarEmails(ByVal ids As String)
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Using conn As New SqlConnection(strConn)
                conn.Open()
                Dim sql As String = "SELECT * FROM TB_AGENDAMENTO_SIG WHERE ID IN (" & ids & ")"
                Using cmd As New SqlCommand(sql, conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim dataPagamento As String = Convert.ToDateTime(reader("Data_Pagamento")).ToString("dd/MM/yyyy")
                            Dim empresa As String = reader("Empresa").ToString()
                            Dim emailAprovador As String = ddlAprovador.SelectedValue
                            Dim emailDigitador As String = reader("email_digitador").ToString()
                            Dim assunto As String = $"Agenda Aprovada - Empresa: {empresa} - Data de Pagamento: {dataPagamento}"
                            Dim corpo As String = ConstruirCorpoEmail(reader)
                            Dim emailDigitadorAprovador As String = reader("Aprovador").ToString()
                            EnviarEmail1(emailAprovador, assunto, corpo, reader)
                            EnviarEmailColaborador(ids, emailDigitadorAprovador)
                        End While
                    End Using
                End Using
            End Using
        End Sub

        Private Function ConstruirCorpoEmail(ByVal reader As SqlDataReader) As String
            Dim contexto = New Contexto
            Dim empresa As String = reader("Empresa").ToString()
            Dim body As String = "<h3>Informações de Despesas</h3>"
            body &= "<table border='1' cellpadding='5' cellspacing='0' style='border-collapse:collapse; font-style: 10px;'>"
            body &= "<tr><th>Data Pagamento</th><th>Descrição</th><th>Valor Bruto</th><th>Valor Líquido</th><th>Favorecido</th><th>CPF/CNPJ</th><th>Forma de Pagamento</th><th>Banco</th><th>Agência</th><th>Conta Corrente</th></tr>"
            body &= $"<tr><td>{reader("Data_Pagamento")}</td><td>{reader("Descricao")}</td><td>{reader("Valor_Bruto")}</td><td>{reader("Valor_Liquido")}</td><td>{reader("Favorecido")}</td><td>{reader("Cpf_Cnpj")}</td><td>{reader("Forma_de_Pagamento")}</td><td>{reader("Banco")}</td><td>{reader("Agencia")}</td><td>{reader("Conta_Corrente")}</td></tr>"
            body &= "</table>"
            body &= $"<br>Empresa: {empresa}"

            Return body
        End Function

        Private Sub EnviarEmail1(ByVal para As String, ByVal assunto As String, ByVal corpo As String, ByVal reader As SqlDataReader)
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim email As New MailMessage()
            Dim contexto = New Contexto
            email.From = New MailAddress("contasapagar@santanafinanceira.onmicrosoft.com")
            Dim emailsCopia As New List(Of String)()
            Dim destinatario As String = ddlAprovador.SelectedValue
            email.To.Add(destinatario)

            For Each item As ListItem In chkCopiaEmails.Items
                If item.Selected Then
                    email.CC.Add(item.Value)
                End If
            Next
            email.Subject = assunto
            email.Body = corpo
            email.IsBodyHtml = True

            Dim arquivosPasta As String = reader("ArquivosPasta").ToString()
            Dim descricao As String = reader("Descricao").ToString()
            If Not String.IsNullOrEmpty(arquivosPasta) AndAlso Directory.Exists(arquivosPasta) Then
                Dim zipFileName As String = Path.Combine(arquivosPasta, "AGENDAMENTO_" & descricao & ".zip")
                Using zip As New ZipFile()
                    For Each file As String In Directory.GetFiles(arquivosPasta)
                        zip.AddFile(file, "")
                    Next
                    zip.Save(zipFileName)
                End Using
                email.Attachments.Add(New Attachment(zipFileName))
            End If

            Dim smtp As New SmtpClient("smtp.office365.com")
            smtp.Port = 587
            smtp.Credentials = New NetworkCredential("contasapagar@santanafinanceira.onmicrosoft.com", "Xay16092")
            smtp.EnableSsl = True
            smtp.Send(email)
        End Sub

        Private Sub EnviarEmailColaborador(ByVal ids As String, ByVal emailDigitador As String)
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim assunto As String = "Agenda Aprovada"
            Dim email As New MailMessage()
            email.From = New MailAddress("contasapagar@santanafinanceira.onmicrosoft.com")
            email.To.Add(emailDigitador)
            email.Subject = assunto
            Dim body As String = "<h3>Agenda(s) aprovada(s)</h3>"
            body &= $"Sua(s) agenda(s) foram aprovadas."
            body &= $"Acompanhe pela lista de agendas o pagamento da fatura, e visualize o comprovante."
            email.Body = body
            email.IsBodyHtml = True
            Dim smtp As New SmtpClient("smtp.office365.com")
            smtp.Port = 587
            smtp.Credentials = New NetworkCredential("contasapagar@santanafinanceira.onmicrosoft.com", "Xay16092")
            smtp.EnableSsl = True
            smtp.Send(email)
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