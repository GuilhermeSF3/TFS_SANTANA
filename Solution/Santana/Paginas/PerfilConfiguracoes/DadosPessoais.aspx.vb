Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic
Imports Santana.Seguranca
Imports Util

Namespace Paginas.PerfilConfiguracoes

    Public Class DadosPessoais

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then
                Dim today As DateTime = DateTime.Now
                Dim previousDate As DateTime

                txtNomeUsuario.Text = ContextoWeb.UsuarioLogado.NomeUsuario
                txtEmail.Text = ContextoWeb.UsuarioLogado.EMail
                txtNomeCompleto.Text = ContextoWeb.UsuarioLogado.NomeCompleto


            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub

        Protected Sub GridViewRiscoAnalitico_RowCreated(sender As Object, e As GridViewRowEventArgs)
            Try


                If e.Row.RowType = DataControlRowType.DataRow And e.Row.RowState = DataControlRowState.Normal Then
                    e.Row.CssClass = "GridviewScrollC3Item"
                End If
                If e.Row.RowType = DataControlRowType.DataRow And e.Row.RowState = DataControlRowState.Alternate Then
                    e.Row.CssClass = "GridviewScrollC3Item2"
                End If


            Catch ex As Exception

            End Try
        End Sub

        Protected Sub btnSalvar_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim nomeUsuario As String = txtNomeUsuario.Text
            Dim email As String = txtEmail.Text
            Dim nomeCompleto As String = txtNomeCompleto.Text
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")


            Try
                Using conn As New SqlConnection(strConn)
                    conn.Open()

                    Dim query As String = "UPDATE USUARIO SET NomeUsuario = @NomeUsuario, EMail = @Email, NomeCompleto = @NomeCompleto WHERE Login = @Login"
                    Using cmd As New SqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@NomeUsuario", nomeUsuario)
                        cmd.Parameters.AddWithValue("@Email", email)
                        cmd.Parameters.AddWithValue("@NomeCompleto", nomeCompleto)
                        cmd.Parameters.AddWithValue("@Login", ContextoWeb.UsuarioLogado.Login)

                        cmd.ExecuteNonQuery()
                    End Using
                End Using
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Dados atualizados com sucesso!');", True)

            Catch ex As Exception

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Erro ao atualizar os dados: " & ex.Message & "');", True)
            End Try
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Sucesso', Dados alterado com sucesso!!);", True)
        End Sub





        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../Menu.aspx")
        End Sub



        Public Overrides Sub VerifyRenderingInServerForm(control As Control)
            'Not Remove
            ' Confirms that an HtmlForm control is rendered for the specified ASP.NET
            '     server control at run time. 

        End Sub




        Private Function FormatValueForFirstColumn(value As String) As String
            Dim number As Decimal
            If Decimal.TryParse(value, number) Then
                Return number.ToString("F2")
            End If
            Return value
        End Function


        Protected Sub hfGridView1SV_ValueChanged(sender As Object, e As EventArgs)
            Session(HfGridView1Svid) = hfGridView1SV.Value
        End Sub

        Protected Sub hfGridView1SH_ValueChanged(sender As Object, e As EventArgs)
            Session(HfGridView1Shid) = hfGridView1SH.Value
        End Sub





    End Class
End Namespace