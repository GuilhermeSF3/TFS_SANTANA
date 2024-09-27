Imports System
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Santana.Seguranca

Namespace Paginas.Cadastro


    Public Class IndexadorFIPE
        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Dim objIndexadorFIPE As DbIndexadorFIPE

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then
                If Session(HfGridView1Svid) IsNot Nothing Then
                    hfGridView1SV.Value = DirectCast(Session(HfGridView1Svid), String)
                    Session.Remove(HfGridView1Svid)
                End If

                If Session(HfGridView1Shid) IsNot Nothing Then
                    hfGridView1SH.Value = DirectCast(Session(HfGridView1Shid), String)
                    Session.Remove(HfGridView1Shid)
                End If

            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub

        Protected Sub btnAplicar_Click(sender As Object, e As EventArgs)
            Dim valuePercentage As String() = Split(txtTaxa.Text.Trim, ",")
            Dim sDecimal As Integer = 0

            If (valuePercentage.Length > 1) Then
                sDecimal = valuePercentage(1).Length
            End If
            Dim vlTaxa As Double = Convert.ToDouble(txtTaxa.Text.Trim())
            If (txtTaxa.Text.Trim = "") Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Atenção!' ,'Obrigatório informar o valor de ajuste!', 'danger');", True)
                txtTaxa.Text = ""

            ElseIf (vlTaxa <= 0 Or vlTaxa > 1000) Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Atenção!' ,'Obrigatório informar somente valores entre 0,01 e 1000,00!', 'danger');", True)
                txtTaxa.Text = ""

            ElseIf (sDecimal > 2) Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Atenção!' ,'Informe valor somente até 2 casas decimais. Ex:0,99', 'danger');", True)
                txtTaxa.Text = ""

            Else
                objIndexadorFIPE = New DbIndexadorFIPE
                Dim txtTaxaString As String = txtTaxa.Text.Trim.Replace(",", ".")

                objIndexadorFIPE.AjustarIndexadorFIPE(txtTaxaString)

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Atualização' ,'Taxa aplicada com sucesso.', 'success');", True)
                txtTaxa.Text = ""

            End If
        End Sub
        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../Menu.aspx")
        End Sub


        Protected Sub hfGridView1SV_ValueChanged(sender As Object, e As EventArgs)
            Session(HfGridView1Svid) = hfGridView1SV.Value
        End Sub

        Protected Sub hfGridView1SH_ValueChanged(sender As Object, e As EventArgs)
            Session(HfGridView1Shid) = hfGridView1SH.Value
        End Sub
    End Class
End Namespace