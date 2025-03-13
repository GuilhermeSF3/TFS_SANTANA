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


    Public Class CadastroHistorico

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


            If Not IsPostBack Then
                Dim today As DateTime = DateTime.Now
                Dim previousDate As DateTime

            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

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

        Protected Sub btnSalvarhistorico_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

            Using conn As New SqlConnection(strConn)
                Dim query As String = "UPDATE TB_HISTORICOS SET Descricao = @Descricao, Favorecido = @Favorecido, Cpf_Cnpj = @CpfCnpj, Forma_de_Pagamento = @FormaPagamento, Banco = @Banco, Agencia = @Agencia, Conta_Corrente = @ContaCorrente WHERE NOME_HISTORICO = @Historico"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Descricao", txtDescricao.Text)
                    cmd.Parameters.AddWithValue("@Historico", ddlHistorico.SelectedValue)
                    cmd.Parameters.AddWithValue("@Favorecido", txtFavorecido.Text)
                    cmd.Parameters.AddWithValue("@CpfCnpj", txtCpfCnpj.Text)
                    cmd.Parameters.AddWithValue("@FormaPagamento", ddlFormaPagamento.SelectedValue)
                    cmd.Parameters.AddWithValue("@Banco", txtBanco.Text)
                    cmd.Parameters.AddWithValue("@Agencia", txtAgencia.Text)
                    cmd.Parameters.AddWithValue("@ContaCorrente", txtContaCorrente.Text)

                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Histórico cadastrado com sucesso!');", True)
            ddlHistorico.SelectedIndex = 0
            txtDescricao.Text = ""
            txtFavorecido.Text = ""
            txtCpfCnpj.Text = ""
            ddlFormaPagamento.SelectedIndex = 0
            txtBanco.Text = ""
            txtAgencia.Text = ""
            txtContaCorrente.Text = ""
        End Sub



        Protected Sub btnEditarhistorico_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

            Using conn As New SqlConnection(strConn)
                Dim query As String = "UPDATE TB_HISTORICOS SET Descricao = @Descricao, Favorecido = @Favorecido, Cpf_Cnpj = @CpfCnpj, Forma_de_Pagamento = @FormaPagamento, Banco = @Banco, Agencia = @Agencia, Conta_Corrente = @ContaCorrente WHERE NOME_HISTORICO = @Historico"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Descricao", txtDescricao.Text)
                    cmd.Parameters.AddWithValue("@Historico", ddlHistorico.SelectedValue)
                    cmd.Parameters.AddWithValue("@Favorecido", txtFavorecido.Text)
                    cmd.Parameters.AddWithValue("@CpfCnpj", txtCpfCnpj.Text)
                    cmd.Parameters.AddWithValue("@FormaPagamento", ddlFormaPagamento.SelectedValue)
                    cmd.Parameters.AddWithValue("@Banco", txtBanco.Text)
                    cmd.Parameters.AddWithValue("@Agencia", txtAgencia.Text)
                    cmd.Parameters.AddWithValue("@ContaCorrente", txtContaCorrente.Text)

                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Histórico editado com sucesso!');", True)
            ddlHistorico.SelectedIndex = 0
            txtDescricao.Text = ""
            txtFavorecido.Text = ""
            txtCpfCnpj.Text = ""
            ddlFormaPagamento.SelectedIndex = 0
            txtBanco.Text = ""
            txtAgencia.Text = ""
            txtContaCorrente.Text = ""
        End Sub


        Protected Sub btnExcluirhistorico_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

            Using conn As New SqlConnection(strConn)
                Dim query As String = "DELETE FROM TB_HISTORICOS WHERE NOME_HISTORICO = @Historico"

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Historico", ddlHistorico.SelectedValue)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Histórico excluido com sucesso!');", True)
            ddlHistorico.SelectedIndex = 0
            txtDescricao.Text = ""
            txtFavorecido.Text = ""
            txtCpfCnpj.Text = ""
            ddlFormaPagamento.SelectedIndex = 0
            txtBanco.Text = ""
            txtAgencia.Text = ""
            txtContaCorrente.Text = ""
        End Sub




        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../Menu.aspx")
        End Sub
    End Class
End Namespace