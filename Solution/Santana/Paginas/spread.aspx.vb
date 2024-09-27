Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports Util
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.IO
Imports System.Web
Imports System.Web.Security
Imports System.Drawing.Printing
Imports System.Data.Common
Imports System
Imports System.Configuration
Imports System.Drawing

Imports Santana.Seguranca

Public Class Spread
    Inherits SantanaPage


    Dim strSortField As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

        If Not IsPostBack Then

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
            txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)

            txtDataDE.Text = (Convert.ToDateTime(txtData.Text).AddMonths(-17)).ToString("dd/MM/yyyy")
            txtDataDE.Text = "01/" + Right(txtDataDE.Text, 7)

            txtDataINI.Text = (Convert.ToDateTime(txtDataDE.Text).AddMonths(-3)).ToString("dd/MM/yyyy")
            txtDataFIM.Text = (Convert.ToDateTime(txtDataINI.Text).AddMonths(18)).ToString("dd/MM/yyyy")
            txtDataFIM.Text = (Convert.ToDateTime(txtDataFIM.Text).AddDays(-1)).ToString("dd/MM/yyyy")
            txtDiasAtraso.Text = "60"

            If ContextoWeb.DadosTransferencia.CodAgente = 0 Then
                Carrega_Agente()
            Else
                Carrega_Agente()
                ddlAgente.SelectedIndex = ddlAgente.Items.IndexOf(ddlAgente.Items.FindByValue(ContextoWeb.DadosTransferencia.CodAgente.ToString()))
            End If


            If ContextoWeb.DadosTransferencia.CodCobradora = 0 Then
                Carrega_Cobradora()
            Else
                Carrega_Cobradora()
                ddlCobradora.SelectedIndex = ddlCobradora.Items.IndexOf(ddlCobradora.Items.FindByValue(ContextoWeb.DadosTransferencia.CodCobradora.ToString()))
            End If

            Carrega_Relatorio()
            ddlRelatorio.SelectedIndex = 0

            If ContextoWeb.UsuarioLogado.Perfil = 8 Then
                ddlPromotora.Visible = True
                ddlPromotoraLabel.Visible = True
                Carrega_Promotora()
            End If

            If ContextoWeb.DadosTransferencia.CodCobradora <> 0 Or ContextoWeb.DadosTransferencia.CodAgente <> 0 Then
                BindGridView1Data()
                ContextoWeb.DadosTransferencia.CodAgente = 0
                ContextoWeb.DadosTransferencia.CodCobradora = 0
            End If

            dvConsultasOperador.Visible = False
            dvConsultasCarteiras.Visible = False
            dvConsultasLojas.Visible = False

            txtData.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnCarregar.UniqueID + "').click();return false;}} else {return true}; ")
        End If

        Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
        ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

    End Sub


    Protected Sub btnDataAnterior_Click(sender As Object, e As EventArgs)

        Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtData.Text)
        UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(-1)

        If UltimoDiaMesAnterior.Year = Now.Date.Year Then
            If UltimoDiaMesAnterior.Month <= Now.Date.Month Then
                txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
            End If
        ElseIf UltimoDiaMesAnterior.Year < Now.Date.Year Then
            txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
        End If

    End Sub


    Protected Sub btnProximaData_Click(sender As Object, e As EventArgs)

        Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtData.Text)
        UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(1)

        If UltimoDiaMesAnterior.Year = Now.Date.Year Then
            If UltimoDiaMesAnterior.Month <= Now.Date.Month Then
                txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
            End If
        ElseIf UltimoDiaMesAnterior.Year < Now.Date.Year Then
            txtData.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
        End If

    End Sub


    Private Function UltimoDiaUtilMesAnterior(Data As Date) As String

        Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Data.ToString("MM/yyyy"))

        UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(1)
        UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)

        If UltimoDiaMesAnterior <= Convert.ToDateTime("01/aug/2014") Then

            If (UltimoDiaMesAnterior.DayOfWeek = DayOfWeek.Sunday) Then
                UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-2)
            ElseIf (UltimoDiaMesAnterior.DayOfWeek = DayOfWeek.Saturday) Then
                UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)
            End If
        End If
        Return UltimoDiaMesAnterior.ToString("dd/MM/yyyy")

    End Function






    Private Sub Carrega_Agente()
        Try
            ddlAgente.Items.Insert(0, New ListItem("Carteira", "1"))
            ddlAgente.Items.Insert(1, New ListItem("Carteira + Prejuizo", "2"))
            ddlAgente.Items.Insert(2, New ListItem("Prejuizo", "3"))
            ddlAgente.SelectedIndex = 0


        Catch ex As Exception

        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Carrega_Promotora()

        Try

            Dim objDataAgente = New DbAgente
            Dim codGerente As String

            If ContextoWeb.UsuarioLogado.Perfil = 8 Then
                codGerente = ContextoWeb.UsuarioLogado.codGerente
            Else
                codGerente = "99"
            End If

            If codGerente = "99" Then
                objDataAgente.CarregarTodosRegistros(ddlAgente)

                ddlPromotora.Items.Insert(0, New ListItem("Todos", "99"))
                ddlPromotora.SelectedIndex = 0
            Else
                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim con As New SqlConnection(strConn)
                Dim Vagente As String = ""

                Dim cmd As New SqlCommand("Select O3DESCR, O3CODORG from CDCSANTANAMicroCredito..TORG3 (nolock) WHERE O3codorg IN (" & codGerente & ")", con)


                cmd.Connection.Open()

                Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

                While dr.Read()
                    Vagente = Trim(dr.GetString(0))
                    Dim AGENTE1 = New ListItem
                    AGENTE1.Value = Trim(dr.GetString(1))
                    AGENTE1.Text = Trim(Vagente)
                    ddlPromotora.Items.Add(AGENTE1)
                End While
                dr.Close()
                con.Close()

            End If


        Catch ex As Exception

        Finally
            GC.Collect()
        End Try
    End Sub


    Private Sub Carrega_Cobradora()
        Try

            ddlCobradora.Items.Insert(0, New ListItem("Todos (c/ Renegociação)", "90"))
            ddlCobradora.Items.Insert(1, New ListItem("Leves", "1"))
            ddlCobradora.Items.Insert(2, New ListItem("Motos", "2"))
            ddlCobradora.Items.Insert(3, New ListItem("Utilitários", "3"))
            ddlCobradora.Items.Insert(4, New ListItem("Refinanciamento", "4"))
            ddlCobradora.Items.Insert(5, New ListItem("Renegociação", "5"))
            ddlCobradora.Items.Insert(6, New ListItem("Leves c/ Renegociação", "6"))
            ddlCobradora.Items.Insert(7, New ListItem("Motos c/ Renegociação", "7"))
            ddlCobradora.Items.Insert(8, New ListItem("Utilitários c/ Renegociação", "8"))

            ddlCobradora.SelectedIndex = 0


        Catch ex As Exception

        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Carrega_Relatorio()
        Try


            If ContextoWeb.UsuarioLogado.Perfil = 8 Then
                ddlRelatorio.Items.Insert(0, New ListItem("Agentes", "5"))
                ddlRelatorio.Items.Insert(1, New ListItem("Operador", "6"))
                ddlRelatorio.Items.Insert(2, New ListItem("Loja", "7"))
            Else
                ddlRelatorio.Items.Insert(0, New ListItem("Carteira", "1"))
                ddlRelatorio.Items.Insert(1, New ListItem("Faixa de Ano", "2"))
                ddlRelatorio.Items.Insert(2, New ListItem("Faixa de Parcelas", "3"))
                ddlRelatorio.Items.Insert(3, New ListItem("Faixa de Renda", "4"))
                ddlRelatorio.Items.Insert(4, New ListItem("Faixa de Idade", "8"))
                ddlRelatorio.Items.Insert(5, New ListItem("Ocupação", "9"))

                ddlRelatorio.Items.Insert(6, New ListItem("Agentes", "5"))
                ddlRelatorio.Items.Insert(7, New ListItem("Operador", "6"))
                ddlRelatorio.Items.Insert(8, New ListItem("Loja", "7"))

                ddlRelatorio.Items.Insert(9, New ListItem("Modelo Veic.", "10"))

                ddlRelatorio.Items.Insert(10, New ListItem("Tabela", "13"))
                ddlRelatorio.Items.Insert(11, New ListItem("Balcão", "14"))
                ddlRelatorio.Items.Insert(12, New ListItem("Tabela (Total)", "15"))
                ddlRelatorio.Items.Insert(13, New ListItem("Agentes (De/Para)", "16"))
            End If


            ddlRelatorio.SelectedIndex = 0


        Catch ex As Exception

        Finally
            GC.Collect()
        End Try
    End Sub





    Private Sub GridViewCarteiras_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewCarteiras.RowDataBound

        Try
            Dim Cor As Drawing.Color

            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then

                    Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row


                    e.Row.Cells(0).Text = drow("DT_FECHA")
                    e.Row.Cells(0).ForeColor = Cor

                    e.Row.Cells(1).Text = drow("DESCR_VEIC")
                    e.Row.Cells(1).ForeColor = Cor

                    e.Row.Cells(2).Text = drow("DESCRICAO")
                    e.Row.Cells(2).ForeColor = Cor

                    e.Row.Cells(3).Text = CNumero.FormataNumero(drow("PRAZO_MEDIO"), 4)
                    e.Row.Cells(3).ForeColor = Cor

                    e.Row.Cells(4).Text = CNumero.FormataNumero(drow("TAXA_MEDIA"), 4)
                    e.Row.Cells(4).ForeColor = Cor

                    e.Row.Cells(5).Text = CNumero.FormataNumero(drow("TICKET_MEDIO"), 4)
                    e.Row.Cells(5).ForeColor = Cor

                    e.Row.Cells(6).Text = If(drow("VLR_CONTRATOS") = 0.0, "", CNumero.FormataNumero(drow("VLR_CONTRATOS"), 4))
                    e.Row.Cells(6).ForeColor = Cor

                    e.Row.Cells(7).Text = If(drow("QTD_CONTRATOS") = 0.0, "", CNumero.FormataNumero(drow("QTD_CONTRATOS"), 0))
                    e.Row.Cells(7).ForeColor = Cor


                    e.Row.Cells(8).Text = If(drow("CAPTACAO") = 0.0, "", CNumero.FormataNumero(drow("CAPTACAO"), 4))
                    e.Row.Cells(8).ForeColor = Cor

                    e.Row.Cells(9).Text = If(drow("PERDA") = 0.0, "", CNumero.FormataNumero(drow("PERDA"), 4))
                    e.Row.Cells(9).ForeColor = Cor

                    e.Row.Cells(10).Text = If(drow("COMISSAO") = 0.0, "", CNumero.FormataNumero(drow("COMISSAO"), 4))
                    e.Row.Cells(10).ForeColor = Cor


                    e.Row.Cells(11).Text = If(drow("SPREAD") = 0.0, "", CNumero.FormataNumero(drow("SPREAD"), 4))
                    e.Row.Cells(11).ForeColor = Cor

                    e.Row.Cells(12).Text = If(drow("CARTEIRA") = 0.0, "", CNumero.FormataNumero(drow("CARTEIRA"), 4))
                    e.Row.Cells(12).ForeColor = Cor

                    e.Row.Cells(13).Text = If(drow("PRC_CARTEIRA") = 0.0, "", CNumero.FormataNumero(drow("PRC_CARTEIRA"), 4))
                    e.Row.Cells(13).ForeColor = Cor

                    e.Row.Cells(14).Text = If(drow("PRODUCAO_TRIM") = 0.0, "", CNumero.FormataNumero(drow("PRODUCAO_TRIM"), 4))
                    e.Row.Cells(14).ForeColor = Cor

                    e.Row.Cells(15).Text = If(drow("PRC_PRODUCAO_TRIM") = 0.0, "", CNumero.FormataNumero(drow("PRC_PRODUCAO_TRIM"), 4))
                    e.Row.Cells(15).ForeColor = Cor

                    e.Row.Cells(16).Text = If(drow("VF_SAFRA") = 0.0, "", CNumero.FormataNumero(drow("VF_SAFRA"), 4))
                    e.Row.Cells(16).ForeColor = Cor

                    e.Row.Cells(17).Text = If(drow("VLR_PERDA") = 0.0, "", CNumero.FormataNumero(drow("VLR_PERDA"), 4))
                    e.Row.Cells(17).ForeColor = Cor

                    e.Row.Cells(18).Text = drow("ordem_linha")
                    e.Row.Cells(18).ForeColor = Cor

                End If
            End If


        Catch ex As Exception

        End Try
    End Sub


    Private Sub GridViewOperador_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewOperador.RowDataBound

        Try


            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then

                    Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row


                    If IsDBNull(drow("COD_OPER")) Then
                        e.Row.Cells(0).Text = ""
                    Else
                        e.Row.Cells(0).Text = drow("COD_OPER")
                    End If

                    If IsDBNull(drow("DES_OPER")) Then
                        e.Row.Cells(1).Text = ""
                    Else
                        e.Row.Cells(1).Text = drow("DES_OPER")
                    End If

                    If IsDBNull(drow("AGENTE")) Then
                        e.Row.Cells(2).Text = ""
                    Else
                        e.Row.Cells(2).Text = drow("AGENTE")
                    End If

                    If IsDBNull(drow("CARTEIRA")) OrElse drow("CARTEIRA") = 0 Then
                        e.Row.Cells(3).Text = ""
                    Else
                        e.Row.Cells(3).Text = CNumero.FormataNumero(drow("CARTEIRA"), 4)
                    End If

                    If IsDBNull(drow("PRAZO_MEDIO")) OrElse drow("PRAZO_MEDIO") = 0 Then
                        e.Row.Cells(4).Text = ""
                    Else
                        e.Row.Cells(4).Text = CNumero.FormataNumero(drow("PRAZO_MEDIO"), 4)
                    End If

                    If IsDBNull(drow("TAXA_MEDIA")) OrElse drow("TAXA_MEDIA") = 0 Then
                        e.Row.Cells(5).Text = ""
                    Else
                        e.Row.Cells(5).Text = CNumero.FormataNumero(drow("TAXA_MEDIA"), 4)
                    End If

                    If IsDBNull(drow("RISCO")) OrElse drow("RISCO") = 0 Then
                        e.Row.Cells(6).Text = ""
                    Else
                        e.Row.Cells(6).Text = CNumero.FormataNumero(drow("RISCO"), 4)
                    End If

                    If IsDBNull(drow("SPREAD1")) OrElse drow("SPREAD1") = 0 Then
                        e.Row.Cells(7).Text = ""
                    Else
                        e.Row.Cells(7).Text = CNumero.FormataNumero(drow("SPREAD1"), 4)
                    End If

                    If IsDBNull(drow("SPREAD4")) OrElse drow("SPREAD4") = 0 Then
                        e.Row.Cells(8).Text = ""
                    Else
                        e.Row.Cells(8).Text = CNumero.FormataNumero(drow("SPREAD4"), 4)
                    End If

                    If IsDBNull(drow("QTDE")) OrElse drow("QTDE") = 0 Then
                        e.Row.Cells(9).Text = ""
                    Else
                        e.Row.Cells(9).Text = CNumero.FormataNumero(drow("QTDE"), 4)
                    End If

                    If IsDBNull(drow("VLR_PRODUCAO")) OrElse drow("VLR_PRODUCAO") = 0 Then
                        e.Row.Cells(10).Text = ""
                    Else
                        e.Row.Cells(10).Text = CNumero.FormataNumero(drow("VLR_PRODUCAO"), 4)
                    End If

                    e.Row.Cells(11).Text = If(drow("VF_SAFRA") = 0.0, "", CNumero.FormataNumero(drow("VF_SAFRA"), 4))
                    e.Row.Cells(12).Text = If(drow("VLR_PERDA") = 0.0, "", CNumero.FormataNumero(drow("VLR_PERDA"), 4))

                End If
            End If



        Catch ex As Exception

        End Try
    End Sub


    Private Sub GridViewLojas_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewLojas.RowDataBound

        Try


            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then

                    Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

                    If IsDBNull(drow("COD_LOJA")) Then
                        e.Row.Cells(0).Text = ""
                    Else
                        e.Row.Cells(0).Text = drow("COD_LOJA")
                    End If

                    If IsDBNull(drow("DES_LOJA")) Then
                        e.Row.Cells(1).Text = ""
                    Else
                        e.Row.Cells(1).Text = drow("DES_LOJA")
                    End If

                    If IsDBNull(drow("SIT")) Then
                        e.Row.Cells(2).Text = ""
                    Else
                        e.Row.Cells(2).Text = drow("SIT")
                    End If

                    If IsDBNull(drow("COD_OPER")) Then
                        e.Row.Cells(3).Text = ""
                    Else
                        e.Row.Cells(3).Text = drow("COD_OPER")
                    End If

                    If IsDBNull(drow("AGENTE")) Then
                        e.Row.Cells(4).Text = ""
                    Else
                        e.Row.Cells(4).Text = drow("AGENTE")
                    End If

                    If IsDBNull(drow("DES_OPER")) Then
                        e.Row.Cells(5).Text = ""
                    Else
                        e.Row.Cells(5).Text = drow("DES_OPER")
                    End If

                    If IsDBNull(drow("PROD_1M")) OrElse drow("PROD_1M") = 0 Then
                        e.Row.Cells(6).Text = ""
                    Else
                        e.Row.Cells(6).Text = CNumero.FormataNumero(drow("PROD_1M"), 4)
                    End If

                    If IsDBNull(drow("CARTEIRA")) OrElse drow("CARTEIRA") = 0 Then
                        e.Row.Cells(7).Text = ""
                    Else
                        e.Row.Cells(7).Text = CNumero.FormataNumero(drow("CARTEIRA"), 4)
                    End If

                    If IsDBNull(drow("PRAZO_MEDIO")) OrElse drow("PRAZO_MEDIO") = 0 Then
                        e.Row.Cells(8).Text = ""
                    Else
                        e.Row.Cells(8).Text = CNumero.FormataNumero(drow("PRAZO_MEDIO"), 4)
                    End If

                    If IsDBNull(drow("TAXA_MEDIA")) OrElse drow("TAXA_MEDIA") = 0 Then
                        e.Row.Cells(9).Text = ""
                    Else
                        e.Row.Cells(9).Text = CNumero.FormataNumero(drow("TAXA_MEDIA"), 4)
                    End If

                    If IsDBNull(drow("CARTEIRA_TRIM")) OrElse drow("CARTEIRA_TRIM") = 0 Then
                        e.Row.Cells(10).Text = ""
                    Else
                        e.Row.Cells(10).Text = CNumero.FormataNumero(drow("CARTEIRA_TRIM"), 4)
                    End If

                    If IsDBNull(drow("PROD_2M")) OrElse drow("PROD_2M") = 0 Then
                        e.Row.Cells(11).Text = ""
                    Else
                        e.Row.Cells(11).Text = CNumero.FormataNumero(drow("PROD_2M"), 4)
                    End If

                    If IsDBNull(drow("PROD_3M")) OrElse drow("PROD_3M") = 0 Then
                        e.Row.Cells(12).Text = ""
                    Else
                        e.Row.Cells(12).Text = CNumero.FormataNumero(drow("PROD_3M"), 4)
                    End If

                    If IsDBNull(drow("RISCO")) OrElse drow("RISCO") = 0 Then
                        e.Row.Cells(13).Text = ""
                    Else
                        e.Row.Cells(13).Text = CNumero.FormataNumero(drow("RISCO"), 4)
                    End If

                    If IsDBNull(drow("VLR_PERDA")) OrElse drow("VLR_PERDA") = 0 Then
                        e.Row.Cells(14).Text = ""
                    Else
                        e.Row.Cells(14).Text = CNumero.FormataNumero(drow("VLR_PERDA"), 4)
                    End If

                    If IsDBNull(drow("SPREAD_MEDIO")) OrElse drow("SPREAD_MEDIO") = 0 Then
                        e.Row.Cells(15).Text = ""
                    Else
                        e.Row.Cells(15).Text = CNumero.FormataNumero(drow("SPREAD_MEDIO"), 4)
                    End If

                    If IsDBNull(drow("TAXA_MEDIA_TRIM")) OrElse drow("TAXA_MEDIA_TRIM") = 0 Then
                        e.Row.Cells(16).Text = ""
                    Else
                        e.Row.Cells(16).Text = CNumero.FormataNumero(drow("TAXA_MEDIA_TRIM"), 4)
                    End If


                    If IsDBNull(drow("SPREAD_TRIM")) OrElse drow("SPREAD_TRIM") = 0 Then
                        e.Row.Cells(17).Text = ""
                    Else
                        e.Row.Cells(17).Text = CNumero.FormataNumero(drow("SPREAD_TRIM"), 4)
                    End If

                    e.Row.Cells(18).Text = If(drow("VF_SAFRA") = 0.0, "", CNumero.FormataNumero(drow("VF_SAFRA"), 4))

                    e.Row.Cells(19).Text = If(drow("VLR_PERDA") = 0.0, "", CNumero.FormataNumero(drow("VLR_PERDA"), 4))

                End If
            End If


        Catch ex As Exception

        End Try
    End Sub


    Private Sub GridViewBalcao_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewBalcao.RowDataBound

        Try


            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not e.Row.DataItem Is Nothing Then

                    Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

                    If IsDBNull(drow("COD_LOJA")) Then
                        e.Row.Cells(0).Text = ""
                    Else
                        e.Row.Cells(0).Text = drow("COD_LOJA")
                    End If

                    If IsDBNull(drow("DES_LOJA")) Then
                        e.Row.Cells(1).Text = ""
                    Else
                        e.Row.Cells(1).Text = drow("DES_LOJA")
                    End If

                    If IsDBNull(drow("SIT")) Then
                        e.Row.Cells(2).Text = ""
                    Else
                        e.Row.Cells(2).Text = drow("SIT")
                    End If

                    If IsDBNull(drow("AGENTE")) Then
                        e.Row.Cells(3).Text = ""
                    Else
                        e.Row.Cells(3).Text = drow("AGENTE")
                    End If

                    If IsDBNull(drow("PROD_1M")) OrElse drow("PROD_1M") = 0 Then
                        e.Row.Cells(4).Text = ""
                    Else
                        e.Row.Cells(4).Text = CNumero.FormataNumero(drow("PROD_1M"), 0)
                    End If

                    If IsDBNull(drow("CARTEIRA")) OrElse drow("CARTEIRA") = 0 Then
                        e.Row.Cells(5).Text = ""
                    Else
                        e.Row.Cells(5).Text = CNumero.FormataNumero(drow("CARTEIRA"), 4)
                    End If

                    If IsDBNull(drow("PRAZO_MEDIO")) OrElse drow("PRAZO_MEDIO") = 0 Then
                        e.Row.Cells(6).Text = ""
                    Else
                        e.Row.Cells(6).Text = CNumero.FormataNumero(drow("PRAZO_MEDIO"), 4)
                    End If

                    If IsDBNull(drow("TAXA_MEDIA")) OrElse drow("TAXA_MEDIA") = 0 Then
                        e.Row.Cells(7).Text = ""
                    Else
                        e.Row.Cells(7).Text = CNumero.FormataNumero(drow("TAXA_MEDIA"), 4)
                    End If

                    If IsDBNull(drow("CARTEIRA_TRIM")) OrElse drow("CARTEIRA_TRIM") = 0 Then
                        e.Row.Cells(8).Text = ""
                    Else
                        e.Row.Cells(8).Text = CNumero.FormataNumero(drow("CARTEIRA_TRIM"), 2)
                    End If

                    If IsDBNull(drow("PROD_2M")) OrElse drow("PROD_2M") = 0 Then
                        e.Row.Cells(9).Text = ""
                    Else
                        e.Row.Cells(9).Text = CNumero.FormataNumero(drow("PROD_2M"), 0)
                    End If

                    If IsDBNull(drow("PROD_3M")) OrElse drow("PROD_3M") = 0 Then
                        e.Row.Cells(10).Text = ""
                    Else
                        e.Row.Cells(10).Text = CNumero.FormataNumero(drow("PROD_3M"), 2)
                    End If

                    If IsDBNull(drow("RISCO")) OrElse drow("RISCO") = 0 Then
                        e.Row.Cells(11).Text = ""
                    Else
                        e.Row.Cells(11).Text = CNumero.FormataNumero(drow("RISCO"), 4)
                    End If

                    If IsDBNull(drow("VLR_PERDA")) OrElse drow("VLR_PERDA") = 0 Then
                        e.Row.Cells(12).Text = ""
                    Else
                        e.Row.Cells(12).Text = CNumero.FormataNumero(drow("VLR_PERDA"), 4)
                    End If

                    If IsDBNull(drow("SPREAD_MEDIO")) OrElse drow("SPREAD_MEDIO") = 0 Then
                        e.Row.Cells(13).Text = ""
                    Else
                        e.Row.Cells(13).Text = CNumero.FormataNumero(drow("SPREAD_MEDIO"), 4)
                    End If

                    If IsDBNull(drow("TAXA_MEDIA_TRIM")) OrElse drow("TAXA_MEDIA_TRIM") = 0 Then
                        e.Row.Cells(14).Text = ""
                    Else
                        e.Row.Cells(14).Text = CNumero.FormataNumero(drow("TAXA_MEDIA_TRIM"), 4)
                    End If

                    If IsDBNull(drow("SPREAD_TRIM")) OrElse drow("SPREAD_TRIM") = 0 Then
                        e.Row.Cells(15).Text = ""
                    Else
                        e.Row.Cells(15).Text = CNumero.FormataNumero(drow("SPREAD_TRIM"), 4)
                    End If

                    If IsDBNull(drow("CAPTACAO")) OrElse drow("CAPTACAO") = 0 Then
                        e.Row.Cells(16).Text = ""
                    Else
                        e.Row.Cells(16).Text = CNumero.FormataNumero(drow("CAPTACAO"), 4)
                    End If

                    If IsDBNull(drow("VF_SAFRA")) OrElse drow("VF_SAFRA") = 0 Then
                        e.Row.Cells(17).Text = ""
                    Else
                        e.Row.Cells(17).Text = CNumero.FormataNumero(drow("VF_SAFRA"), 4)
                    End If

                    If IsDBNull(drow("VLR_PERDA")) OrElse drow("VLR_PERDA") = 0 Then
                        e.Row.Cells(18).Text = ""
                    Else
                        e.Row.Cells(18).Text = CNumero.FormataNumero(drow("VLR_PERDA"), 4)
                    End If

                    If IsDBNull(drow("PROD_BL_3M")) OrElse drow("PROD_BL_3M") = 0 Then
                        e.Row.Cells(19).Text = ""
                    Else
                        e.Row.Cells(19).Text = CNumero.FormataNumero(drow("PROD_BL_3M"), 4)
                    End If

                    If IsDBNull(drow("PROD_BL_2M")) OrElse drow("PROD_BL_2M") = 0 Then
                        e.Row.Cells(20).Text = ""
                    Else
                        e.Row.Cells(20).Text = CNumero.FormataNumero(drow("PROD_BL_2M"), 4)
                    End If

                    If IsDBNull(drow("PROD_BL_1M")) OrElse drow("PROD_BL_1M") = 0 Then
                        e.Row.Cells(21).Text = ""
                    Else
                        e.Row.Cells(21).Text = CNumero.FormataNumero(drow("PROD_BL_1M"), 4)
                    End If

                    If IsDBNull(drow("PROD_AG_3M")) OrElse drow("PROD_AG_3M") = 0 Then
                        e.Row.Cells(22).Text = ""
                    Else
                        e.Row.Cells(22).Text = CNumero.FormataNumero(drow("PROD_AG_3M"), 4)
                    End If

                    If IsDBNull(drow("PROD_AG_2M")) OrElse drow("PROD_AG_2M") = 0 Then
                        e.Row.Cells(23).Text = ""
                    Else
                        e.Row.Cells(23).Text = CNumero.FormataNumero(drow("PROD_AG_2M"), 4)
                    End If

                    If IsDBNull(drow("PROD_AG_1M")) OrElse drow("PROD_AG_1M") = 0 Then
                        e.Row.Cells(24).Text = ""
                    Else
                        e.Row.Cells(24).Text = CNumero.FormataNumero(drow("PROD_AG_1M"), 2)
                    End If

                    If IsDBNull(drow("PROD_AG_TRIM")) OrElse drow("PROD_AG_TRIM") = 0 Then
                        e.Row.Cells(25).Text = ""
                    Else
                        e.Row.Cells(25).Text = CNumero.FormataNumero(drow("PROD_AG_TRIM"), 2)
                    End If

                    If IsDBNull(drow("PRC_BALCAO_AG")) OrElse drow("PRC_BALCAO_AG") = 0 Then
                        e.Row.Cells(26).Text = ""
                    Else
                        e.Row.Cells(26).Text = CNumero.FormataNumero(drow("PRC_BALCAO_AG"), 4)
                        If CNumero.FormataNumero(drow("PRC_BALCAO_AG"), 4) > 20 Then
                            e.Row.Cells(26).ForeColor = Drawing.Color.Red
                        End If
                    End If

                End If
            End If


        Catch ex As Exception

        End Try
    End Sub


    Protected Sub GridViewCarteiras_RowCreated(sender As Object, e As GridViewRowEventArgs)
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


    Protected Sub GridViewOperador_RowCreated(sender As Object, e As GridViewRowEventArgs)
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


    Protected Sub GridViewLojas_RowCreated(sender As Object, e As GridViewRowEventArgs)
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


    Protected Sub GridViewBalcao_RowCreated(sender As Object, e As GridViewRowEventArgs)
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


    Public Property DataGridView As DataTable
        Get

            If ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") Is Nothing Then
                ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = GetData()
            End If

            Return DirectCast(ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C"), DataTable)
        End Get
        Set(value As DataTable)
            ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = value
        End Set
    End Property


    Public Property SortField As String
        Get

            If ViewState("34AF154F-7129-4CA1-90B4-C1175088D384") Is Nothing Then
                ViewState("34AF154F-7129-4CA1-90B4-C1175088D384") = strSortField
            End If

            Return DirectCast(ViewState("34AF154F-7129-4CA1-90B4-C1175088D384"), String)
        End Get
        Set(value As String)
            ViewState("34AF154F-7129-4CA1-90B4-C1175088D384") = value
        End Set
    End Property


    Protected Sub BindGridView1Data()

        Dim objGrid As GridView

        Select Case ddlRelatorio.SelectedValue
            Case 6
                objGrid = GridViewOperador
            Case 7
                objGrid = GridViewLojas
            Case 10
                objGrid = GridViewLojas
            Case 14
                objGrid = GridViewBalcao
            Case Else
                objGrid = GridViewCarteiras
        End Select

        objGrid.DataSource = GetData()
        objGrid.DataBind()
        objGrid.AllowPaging = "True"
    End Sub

    Protected Sub BindGridView1DataView()


        Dim objGrid As GridView

        Select Case ddlRelatorio.SelectedValue
            Case 6
                objGrid = GridViewOperador
            Case 7
                objGrid = GridViewLojas
            Case 10
                objGrid = GridViewLojas
            Case 14
                objGrid = GridViewBalcao
            Case Else
                objGrid = GridViewCarteiras
        End Select

        objGrid.DataSource = DataGridView
        objGrid.DataBind()

    End Sub


    Private Function GetData() As DataTable

        Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim table As DataTable

        Dim codGerente As String

        If ContextoWeb.UsuarioLogado.Perfil = 8 Then
            codGerente = ContextoWeb.UsuarioLogado.codGerente
        Else
            codGerente = "99"
        End If


        Select Case ddlRelatorio.SelectedValue
            Case 6
                table = Util.ClassBD.GetExibirGrid("[scr_SPREAD_Operador] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', '" & Right(txtDataDE.Text, 4) & Mid(txtDataDE.Text, 4, 2) & Left(txtDataDE.Text, 2) & "', '" & ddlRelatorio.SelectedValue & "','" & ddlAgente.SelectedValue & "','" & ddlCobradora.SelectedValue & "','" & SortField & "','" & Right(txtDataINI.Text, 4) & Mid(txtDataINI.Text, 4, 2) & Left(txtDataINI.Text, 2) & "','" & Right(txtDataFIM.Text, 4) & Mid(txtDataFIM.Text, 4, 2) & Left(txtDataFIM.Text, 2) & "','" & txtDiasAtraso.Text & "','" & IIf(codGerente = "99", codGerente, ddlPromotora.SelectedValue) & "'", "RRMENSALcubo", strConn)
            Case 7
                table = Util.ClassBD.GetExibirGrid("[scr_SPREAD_Loja] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', '" & Right(txtDataDE.Text, 4) & Mid(txtDataDE.Text, 4, 2) & Left(txtDataDE.Text, 2) & "', '" & ddlRelatorio.SelectedValue & "','" & ddlAgente.SelectedValue & "','" & ddlCobradora.SelectedValue & "','" & SortField & "','" & Right(txtDataINI.Text, 4) & Mid(txtDataINI.Text, 4, 2) & Left(txtDataINI.Text, 2) & "','" & Right(txtDataFIM.Text, 4) & Mid(txtDataFIM.Text, 4, 2) & Left(txtDataFIM.Text, 2) & "','" & txtDiasAtraso.Text & "','" & IIf(codGerente = "99", codGerente, ddlPromotora.SelectedValue) & "'", "RRMENSALcubo", strConn)
            Case 10
                table = Util.ClassBD.GetExibirGrid("[scr_SPREAD_Loja] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', '" & Right(txtDataDE.Text, 4) & Mid(txtDataDE.Text, 4, 2) & Left(txtDataDE.Text, 2) & "', '" & ddlRelatorio.SelectedValue & "','" & ddlAgente.SelectedValue & "','" & ddlCobradora.SelectedValue & "','" & SortField & "','" & Right(txtDataINI.Text, 4) & Mid(txtDataINI.Text, 4, 2) & Left(txtDataINI.Text, 2) & "','" & Right(txtDataFIM.Text, 4) & Mid(txtDataFIM.Text, 4, 2) & Left(txtDataFIM.Text, 2) & "','" & txtDiasAtraso.Text & "','" & IIf(codGerente = "99", codGerente, ddlPromotora.SelectedValue) & "'", "RRMENSALcubo", strConn)
            Case 14
                table = Util.ClassBD.GetExibirGrid("[scr_SPREAD_Balcao] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', '" & Right(txtDataDE.Text, 4) & Mid(txtDataDE.Text, 4, 2) & Left(txtDataDE.Text, 2) & "', '7','" & ddlAgente.SelectedValue & "','" & ddlCobradora.SelectedValue & "','" & SortField & "','" & Right(txtDataINI.Text, 4) & Mid(txtDataINI.Text, 4, 2) & Left(txtDataINI.Text, 2) & "','" & Right(txtDataFIM.Text, 4) & Mid(txtDataFIM.Text, 4, 2) & Left(txtDataFIM.Text, 2) & "','" & txtDiasAtraso.Text & "','" & IIf(codGerente = "99", codGerente, ddlPromotora.SelectedValue) & "'", "RRMENSALcubo", strConn)
            Case 16
                table = Util.ClassBD.GetExibirGrid("[scr_SPREAD_Agente] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', '" & Right(txtDataDE.Text, 4) & Mid(txtDataDE.Text, 4, 2) & Left(txtDataDE.Text, 2) & "', '" & ddlRelatorio.SelectedValue & "','" & ddlAgente.SelectedValue & "','" & ddlCobradora.SelectedValue & "','" & SortField & "','" & Right(txtDataINI.Text, 4) & Mid(txtDataINI.Text, 4, 2) & Left(txtDataINI.Text, 2) & "','" & Right(txtDataFIM.Text, 4) & Mid(txtDataFIM.Text, 4, 2) & Left(txtDataFIM.Text, 2) & "','" & txtDiasAtraso.Text & "','" & IIf(codGerente = "99", codGerente, ddlPromotora.SelectedValue) & "'", "RRMENSALcubo", strConn)
            Case Else
                table = Util.ClassBD.GetExibirGrid("[scr_SPREAD] '" & Right(txtData.Text, 4) & Mid(txtData.Text, 4, 2) & Left(txtData.Text, 2) & "', '" & Right(txtDataDE.Text, 4) & Mid(txtDataDE.Text, 4, 2) & Left(txtDataDE.Text, 2) & "', '" & ddlRelatorio.SelectedValue & "','" & ddlAgente.SelectedValue & "','" & ddlCobradora.SelectedValue & "','" & SortField & "','" & Right(txtDataINI.Text, 4) & Mid(txtDataINI.Text, 4, 2) & Left(txtDataINI.Text, 2) & "','" & Right(txtDataFIM.Text, 4) & Mid(txtDataFIM.Text, 4, 2) & Left(txtDataFIM.Text, 2) & "','" & txtDiasAtraso.Text & "','" & IIf(codGerente = "99", codGerente, ddlPromotora.SelectedValue) & "'", "RRMENSALcubo", strConn)

        End Select

        Return table

    End Function


    Protected Sub GridViewCarteiras_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GridViewCarteiras.PageIndex = IIf(e.NewPageIndex < 0, 0, e.NewPageIndex)
        BindGridView1DataView()
    End Sub

    Protected Sub GridViewOperador_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GridViewOperador.PageIndex = IIf(e.NewPageIndex < 0, 0, e.NewPageIndex)
        BindGridView1DataView()
    End Sub

    Protected Sub GridViewLojas_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

        GridViewLojas.PageIndex = IIf(e.NewPageIndex < 0, 0, e.NewPageIndex)
        BindGridView1DataView()
    End Sub

    Protected Sub GridViewBalcao_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

        GridViewBalcao.PageIndex = IIf(e.NewPageIndex < 0, 0, e.NewPageIndex)
        BindGridView1DataView()
    End Sub



    Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)


        'ContextoWeb.DadosTransferencia.CodAgente = ddlAgente.SelectedValue
        'ContextoWeb.DadosTransferencia.Agente = ddlAgente.SelectedItem.ToString()

        'ContextoWeb.DadosTransferencia.CodCobradora = ddlCobradora.SelectedValue
        'ContextoWeb.DadosTransferencia.Cobradora = ddlCobradora.SelectedItem.ToString()


        'Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

        'Dim ds As dsRollrateMensal
        'Dim cmd As New SqlCommand("[scr_RR_mensal] '" & Convert.ToDateTime(txtData.Text).ToString("MM/dd/yy") & "', '" & ddlAgente.SelectedValue & "','" & ddlCobradora.SelectedValue & "'")
        'Using con As New SqlConnection(strConn)
        '    Using sda As New SqlDataAdapter()
        '        cmd.Connection = con
        '        sda.SelectCommand = cmd
        '        ds = New dsRollrateMensal()
        '        sda.Fill(ds, "RR_ROLLRATE_RPT")
        '    End Using
        'End Using

        '' ContextoWeb.NewReportContext()
        'ContextoWeb.Relatorio.reportFileName = "~/Relatorios/rptRollrateMensal.rpt"
        'ContextoWeb.Relatorio.reportDatas.Add(New reportData(ds))

        'ContextoWeb.Navegacao.LinkPaginaAnteriorRelatorio = Me.AppRelativeVirtualPath
        '' ContextoWeb.Navegacao.TituloPaginaAtual = Me.Title
        'Response.Redirect("Relatorio.aspx")

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
    End Sub


    Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('PDD Mensal' ,'Fechamento da PDD mensal, quebra por FILTRO selecionado na lista. Ordenação crescente: clique no cabeçalho da coluna. Descrescente: 2o. clique.');", True)
    End Sub


    Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
        Response.Redirect("Menu.aspx")
    End Sub


    Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
        Try

            Select Case ddlRelatorio.SelectedValue
                Case 6
                    SortField = "DES_OPER"
                    BindGridView1Data()
                    dvConsultasOperador.Visible = True
                    dvConsultasCarteiras.Visible = False
                    dvConsultasLojas.Visible = False
                    dvConsultasBalcao.Visible = False
                Case 7
                    SortField = "DES_LOJA"
                    BindGridView1Data()
                    dvConsultasOperador.Visible = False
                    dvConsultasCarteiras.Visible = False
                    dvConsultasLojas.Visible = True
                    dvConsultasBalcao.Visible = False
                Case 10
                    SortField = "DES_LOJA"
                    BindGridView1Data()
                    dvConsultasOperador.Visible = False
                    dvConsultasCarteiras.Visible = False
                    dvConsultasLojas.Visible = True
                    dvConsultasBalcao.Visible = False
                Case 14
                    SortField = "DES_LOJA"
                    BindGridView1Data()
                    dvConsultasOperador.Visible = False
                    dvConsultasCarteiras.Visible = False
                    dvConsultasLojas.Visible = False
                    dvConsultasBalcao.Visible = True
                Case Else
                    SortField = "ordem_linha, DESCR_VEIC"
                    BindGridView1Data()
                    dvConsultasOperador.Visible = False
                    dvConsultasCarteiras.Visible = True
                    dvConsultasLojas.Visible = False
                    dvConsultasBalcao.Visible = False
            End Select


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


    Protected Sub btnExcel_Click(sender As Object, e As EventArgs)

        Try

            Dim objGrid As GridView

            Select Case ddlRelatorio.SelectedValue
                Case 6
                    GridViewOperador.AllowPaging = False
                    BindGridView1Data()
                    objGrid = GridViewOperador
                Case 7
                    GridViewLojas.AllowPaging = False
                    BindGridView1Data()
                    objGrid = GridViewLojas
                Case 10
                    GridViewLojas.AllowPaging = False
                    BindGridView1Data()
                    objGrid = GridViewLojas
                Case 14
                    GridViewBalcao.AllowPaging = False
                    BindGridView1Data()
                    objGrid = GridViewBalcao
                Case Else
                    GridViewCarteiras.AllowPaging = False
                    BindGridView1Data()
                    objGrid = GridViewCarteiras
            End Select


            ExportExcel(objGrid)


        Catch ex As Exception
            Throw ex
        End Try

    End Sub




    Private Sub ExportExcel(objGrid As GridView)


        Try

            If Not IsNothing(objGrid.HeaderRow) Then

                Response.Clear()
                Response.Buffer = True
                Dim filename As String = String.Format("SPREAD_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
                Response.AddHeader("content-disposition", "attachment;filename=" + filename)
                Response.ContentEncoding = System.Text.Encoding.Default
                Response.Charset = ""
                Response.ContentType = "application/vnd.ms-excel"

                Using sw As New StringWriter()

                    Dim hw As New HtmlTextWriter(sw)

                    objGrid.HeaderRow.BackColor = Color.White
                    For Each cell As TableCell In objGrid.HeaderRow.Cells
                        cell.CssClass = "GridviewScrollC3Header"
                    Next
                    For Each row As GridViewRow In objGrid.Rows
                        row.BackColor = Color.White
                        For Each cell As TableCell In row.Cells
                            If row.RowIndex Mod 2 = 0 Then
                                cell.CssClass = "GridviewScrollC3Item"
                            Else
                                cell.CssClass = "GridviewScrollC3Item2"
                            End If

                            Dim controls As New List(Of Control)()
                            For Each control As Control In cell.Controls
                                controls.Add(control)
                            Next

                            For Each control As Control In controls
                                Select Case control.GetType().Name
                                    Case "HyperLink"
                                        cell.Controls.Add(New Literal() With {
                                         .Text = TryCast(control, HyperLink).Text
                                        })
                                        Exit Select
                                    Case "TextBox"
                                        cell.Controls.Add(New Literal() With {
                                         .Text = TryCast(control, TextBox).Text
                                        })
                                        Exit Select
                                    Case "LinkButton"
                                        cell.Controls.Add(New Literal() With {
                                         .Text = TryCast(control, LinkButton).Text
                                        })
                                        Exit Select
                                    Case "CheckBox"
                                        cell.Controls.Add(New Literal() With {
                                         .Text = TryCast(control, CheckBox).Text
                                        })
                                        Exit Select
                                    Case "RadioButton"
                                        cell.Controls.Add(New Literal() With {
                                         .Text = TryCast(control, RadioButton).Text
                                        })
                                        Exit Select
                                End Select
                                cell.Controls.Remove(control)
                            Next
                        Next
                    Next

                    objGrid.RenderControl(hw)


                    Dim fi As FileInfo = New FileInfo(Server.MapPath("~/css/GridviewScroll.css"))
                    Dim sb As New System.Text.StringBuilder
                    Dim sr As StreamReader = fi.OpenText()
                    Do While sr.Peek() >= 0
                        sb.Append(sr.ReadLine())
                    Loop
                    sr.Close()

                    Dim style As String = "<html><head><style type='text/css'>" & sb.ToString() & "</style><head>"
                    Response.Write(style)

                    Response.Write("Produção De " & txtDataDE.Text & " Até " & txtData.Text & " - Relatório " & ddlRelatorio.SelectedItem.Text & " - Safra De " & txtDataINI.Text & " Até " & txtDataFIM.Text & " - Atraso " & txtDiasAtraso.Text & " -  Contratos " & ddlAgente.SelectedItem.Text & "  - Veiculo " & ddlCobradora.SelectedItem.Text)

                    Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.Flush()
                    HttpContext.Current.Response.SuppressContent = True
                    HttpContext.Current.ApplicationInstance.CompleteRequest()
                End Using

            End If

        Catch ex As Exception
            Throw ex
        End Try



    End Sub



    Protected Sub GridViewCarteiras_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridViewCarteiras.Sorting
        Dim arrSortExpr() As String
        Dim i As Integer
        If e.SortExpression = "" Then Return

        SortField = e.SortExpression
        BindGridView1Data()
        arrSortExpr = Split(e.SortExpression, " ")

        For i = 0 To GridViewCarteiras.Columns().Count - 1
            If (GridViewCarteiras.Columns(i).SortExpression = e.SortExpression) Then
                If arrSortExpr.Length = 1 Then
                    ReDim Preserve arrSortExpr(2)
                    arrSortExpr.SetValue("ASC", 1)
                End If
                If UCase(arrSortExpr(1)) = "ASC" Then
                    If UCase(arrSortExpr(1)) = "ASC" Then
                        arrSortExpr(1) = "DESC"
                    ElseIf UCase(arrSortExpr(1)) = "DESC" Then
                        arrSortExpr(1) = "ASC"
                    End If
                    GridViewCarteiras.Columns(i).SortExpression = arrSortExpr(0) & " " & arrSortExpr(1)
                End If
                Exit For
            End If

        Next

    End Sub


    Protected Sub GridViewOperador_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridViewOperador.Sorting
        Dim arrSortExpr() As String
        Dim i As Integer
        If e.SortExpression = "" Then Return

        SortField = e.SortExpression
        BindGridView1Data()
        arrSortExpr = Split(e.SortExpression, " ")

        For i = 0 To GridViewOperador.Columns().Count - 1
            If (GridViewOperador.Columns(i).SortExpression = e.SortExpression) Then
                If arrSortExpr.Length = 1 Then
                    ReDim Preserve arrSortExpr(2)
                    arrSortExpr.SetValue("ASC", 1)
                End If
                If UCase(arrSortExpr(1)) = "ASC" Then
                    If UCase(arrSortExpr(1)) = "ASC" Then
                        arrSortExpr(1) = "DESC"
                    ElseIf UCase(arrSortExpr(1)) = "DESC" Then
                        arrSortExpr(1) = "ASC"
                    End If
                    GridViewOperador.Columns(i).SortExpression = arrSortExpr(0) & " " & arrSortExpr(1)
                End If
                Exit For
            End If

        Next

    End Sub



    Protected Sub GridViewLojas_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridViewLojas.Sorting
        Dim arrSortExpr() As String
        Dim i As Integer
        If e.SortExpression = "" Then Return

        SortField = e.SortExpression
        BindGridView1Data()
        arrSortExpr = Split(e.SortExpression, " ")

        For i = 0 To GridViewLojas.Columns().Count - 1
            If (GridViewLojas.Columns(i).SortExpression = e.SortExpression) Then
                If arrSortExpr.Length = 1 Then
                    ReDim Preserve arrSortExpr(2)
                    arrSortExpr.SetValue("ASC", 1)
                End If
                If UCase(arrSortExpr(1)) = "ASC" Then
                    If UCase(arrSortExpr(1)) = "ASC" Then
                        arrSortExpr(1) = "DESC"
                    ElseIf UCase(arrSortExpr(1)) = "DESC" Then
                        arrSortExpr(1) = "ASC"
                    End If
                    GridViewLojas.Columns(i).SortExpression = arrSortExpr(0) & " " & arrSortExpr(1)
                End If
                Exit For
            End If

        Next

    End Sub


    Protected Sub GridViewBalcao_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridViewLojas.Sorting
        Dim arrSortExpr() As String
        Dim i As Integer
        If e.SortExpression = "" Then Return

        SortField = e.SortExpression
        BindGridView1Data()
        arrSortExpr = Split(e.SortExpression, " ")

        For i = 0 To GridViewBalcao.Columns().Count - 1
            If (GridViewBalcao.Columns(i).SortExpression = e.SortExpression) Then
                If arrSortExpr.Length = 1 Then
                    ReDim Preserve arrSortExpr(2)
                    arrSortExpr.SetValue("ASC", 1)
                End If
                If UCase(arrSortExpr(1)) = "ASC" Then
                    If UCase(arrSortExpr(1)) = "ASC" Then
                        arrSortExpr(1) = "DESC"
                    ElseIf UCase(arrSortExpr(1)) = "DESC" Then
                        arrSortExpr(1) = "ASC"
                    End If
                    GridViewBalcao.Columns(i).SortExpression = arrSortExpr(0) & " " & arrSortExpr(1)
                End If
                Exit For
            End If

        Next

    End Sub

    Protected Sub ddlRelatorio_SelectedIndexChanged(sender As Object, e As EventArgs)

        GridViewCarteiras.DataSource = Nothing
        GridViewCarteiras.DataBind()
        GridViewOperador.DataSource = Nothing
        GridViewOperador.DataBind()
        GridViewLojas.DataSource = Nothing
        GridViewLojas.DataBind()
        GridViewBalcao.DataSource = Nothing
        GridViewBalcao.DataBind()
        ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = Nothing

        dvConsultasOperador.Visible = False
        dvConsultasCarteiras.Visible = False
        dvConsultasLojas.Visible = False
        dvConsultasBalcao.Visible = False

        Select Case ddlRelatorio.SelectedValue
            Case 6
                SortField = "DES_OPER"
            Case 7
                SortField = "DES_LOJA"
            Case 10
                SortField = "DES_LOJA"
            Case 14
                SortField = "DES_LOJA"
            Case Else
                SortField = "ordem_linha,DESCR_veic"
        End Select


    End Sub

    Protected Sub GridViewCarteiras_DataBound(sender As Object, e As EventArgs)
        Dim gridView As GridView = CType(sender, GridView)
        If Not IsNothing(gridView.DataSource) AndAlso CType(gridView.DataSource, System.Data.DataTable).Rows.Count > 0 Then
            Util.CWebPageUtil.PreencheListaDePaginas(gridView, CType(gridView.BottomPagerRow.Cells(0).FindControl("PagerPages"), DropDownList))
        End If
    End Sub

    Protected Sub GridViewOperador_DataBound(sender As Object, e As EventArgs)
        Dim gridView As GridView = CType(sender, GridView)
        If Not IsNothing(gridView.DataSource) AndAlso CType(gridView.DataSource, System.Data.DataTable).Rows.Count > 0 Then
            Util.CWebPageUtil.PreencheListaDePaginas(gridView, CType(gridView.BottomPagerRow.Cells(0).FindControl("PagerPages"), DropDownList))
        End If
    End Sub

    Protected Sub GridViewLojas_DataBound(sender As Object, e As EventArgs)
        Dim gridView As GridView = CType(sender, GridView)
        If Not IsNothing(gridView.DataSource) AndAlso CType(gridView.DataSource, System.Data.DataTable).Rows.Count > 0 Then
            Util.CWebPageUtil.PreencheListaDePaginas(gridView, CType(gridView.BottomPagerRow.Cells(0).FindControl("PagerPages"), DropDownList))
        End If
    End Sub

    Protected Sub GridViewBalcao_DataBound(sender As Object, e As EventArgs)
        Dim gridView As GridView = CType(sender, GridView)
        If Not IsNothing(gridView.DataSource) AndAlso CType(gridView.DataSource, System.Data.DataTable).Rows.Count > 0 Then
            Util.CWebPageUtil.PreencheListaDePaginas(gridView, CType(gridView.BottomPagerRow.Cells(0).FindControl("PagerPages"), DropDownList))
        End If
    End Sub

    Protected Sub PagerPages_SelectedIndexChanged(sender As Object, e As EventArgs)


        Dim objGrid As GridView

        Select Case ddlRelatorio.SelectedValue
            Case 6
                objGrid = GridViewOperador
            Case 7
                objGrid = GridViewLojas
            Case 10  ' modelo
                objGrid = GridViewLojas
            Case 14
                objGrid = GridViewBalcao
            Case Else
                objGrid = GridViewCarteiras
        End Select


        objGrid.DataSource = DataGridView()
        objGrid.PageIndex = CType(sender, DropDownList).SelectedIndex
        objGrid.DataBind()


    End Sub

End Class

