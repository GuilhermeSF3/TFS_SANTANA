Imports System
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Web.UI
Imports Santana.Seguranca

Namespace Paginas.TI
    Public Class AgendaRecusar
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
                    RecusarAgendas(ids)
                End If
            Else
                lblMessage.Text = "Por favor, confirme a recusa."
            End If
        End Sub

        Private Sub RecusarAgendas(ByVal ids As String)
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Using conn As New SqlConnection(strConn)
                conn.Open()
                Dim sql As String = "UPDATE TB_AGENDAMENTO_SIG SET STATUS = 'RECUSADO' WHERE ID IN (" & ids & ")"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            lblMessage.Text = "Agendas recusadas com sucesso."
            pnlAgendas.Visible = False

            ' Enviar e-mails
            EnviarEmails(ids)
        End Sub

        Private Sub EnviarEmails(ByVal ids As String)
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Using conn As New SqlConnection(strConn)
                conn.Open()
                Dim sql As String = "SELECT * FROM TB_AGENDAMENTO_SIG WHERE ID IN (" & ids & ")"
                Using cmd As New SqlCommand(sql, conn)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim emailDigitador As String = reader("Digitador").ToString()
                            Dim assunto As String = "Agenda Recusada"
                            Dim motivo As String = txtMotivo.Text
                            Dim corpo As String = $"A(s) agenda(s) com ID(s) {ids} foram recusadas.<br />Motivo: {motivo}"

                            ' Enviar e-mail para o digitador
                            EnviarEmail(emailDigitador, assunto, corpo)
                        End While
                    End Using
                End Using
            End Using
        End Sub

        Private Sub EnviarEmail(ByVal para As String, ByVal assunto As String, ByVal corpo As String)
            Dim email As New MailMessage()
            email.From = New MailAddress("contasapagar@santanafinanceira.onmicrosoft.com")
            email.To.Add("menoti@sf3.com.br")
            email.Subject = assunto
            email.Body = corpo
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