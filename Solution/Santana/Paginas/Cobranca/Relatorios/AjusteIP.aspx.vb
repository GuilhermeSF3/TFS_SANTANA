Imports System
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Santana.Seguranca

Namespace Paginas.Cadastro


    Public Class AjusteIP
        Inherits SantanaPage

        Dim objAjusteIP As DbAjusteIP

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
                txtDataFech.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)

            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub

        Protected Sub btnDataAnterior_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataFech.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(-1)

            If UltimoDiaMesAnterior.Year = Now.Date.Year Then
                If UltimoDiaMesAnterior.Month <= Now.Date.Month Then
                    txtDataFech.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
                End If
            ElseIf UltimoDiaMesAnterior.Year < Now.Date.Year Then
                txtDataFech.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
            End If

        End Sub

        Protected Sub btnProximaData_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataFech.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(1)

            If UltimoDiaMesAnterior.Year = Now.Date.Year Then
                If UltimoDiaMesAnterior.Month <= Now.Date.Month Then
                    txtDataFech.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
                End If
            ElseIf UltimoDiaMesAnterior.Year < Now.Date.Year Then
                txtDataFech.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)
            End If

        End Sub

        Protected Sub FillCustomerInGrid()

            Dim dtCobranca As DataTable = New DbAjusteIP().CarregarContratos(txtDataFech.Text, txtContrato.Text, txtParcela.Text)
            Try

                If dtCobranca.Rows.Count > 0 Then
                    GridView1.DataSource = dtCobranca
                    GridView1.DataBind()
                Else
                    dtCobranca.Rows.Add(dtCobranca.NewRow())
                    Me.GridView1.DataSource = dtCobranca
                    GridView1.DataBind()

                    Dim totalColumns As Integer
                    totalColumns = GridView1.Rows(0).Cells.Count
                    GridView1.Rows(0).Cells.Clear()
                    GridView1.Rows(0).Cells.Add(New TableCell())
                    GridView1.Rows(0).Cells(0).ColumnSpan = totalColumns
                    GridView1.Rows(0).Cells(0).Style.Add("text-align", "center")
                    GridView1.Rows(0).Cells(0).Text = "Não há pagamentos nessa data de referência."

                End If

            Catch ex As Exception
                Throw New Exception(ex.Message.ToString(), ex)
            End Try

        End Sub



        Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim strMensagem As String = ""

            If e.CommandName.Equals("Reabrir") Then

                objAjusteIP = New DbAjusteIP
                Dim index As Integer
                index = Convert.ToInt32(e.CommandArgument)
                objAjusteIP.ReabrirPagto(GridView1.DataKeys(index).Values(0).ToString(), GridView1.DataKeys(index).Values(1).ToString(), GridView1.DataKeys(index).Values(2).ToString(), GridView1.DataKeys(index).Values(3).ToString(), GridView1.DataKeys(index).Values(4).ToString())
                FillCustomerInGrid()

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Ajuste IP' ,'Pagamento reaberto com sucesso.', 'success');", True)


            ElseIf e.CommandName.Equals("Copiar") Then
                objAjusteIP = New DbAjusteIP
                Dim index As Integer
                index = Convert.ToInt32(e.CommandArgument)
                Dim DataPara As TextBox = GridView1.Rows(index).FindControl("txtDataPara")
                Dim txtDataPara As String = DataPara.Text
                Dim CobPara As TextBox = GridView1.Rows(index).FindControl("txtCobradoraPara")
                Dim txtCobPara As String = CobPara.Text

                If txtCobPara.Trim = "" Or txtDataPara.Trim = "" Then
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Copiar IP' ,'Entre com a Data e a Cobradora!', 'danger');", True)
                    Exit Sub

                Else
                    objAjusteIP.CopiarPagto(GridView1.DataKeys(index).Values(0).ToString(), GridView1.DataKeys(index).Values(1).ToString(), GridView1.DataKeys(index).Values(2).ToString(), GridView1.DataKeys(index).Values(3).ToString(), txtDataPara, txtCobPara)

                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Ajuste IP' ,'Cópia realizada com sucesso.', 'success');", True)

                    FillCustomerInGrid()
                End If

            End If

            If strMensagem <> "" Then

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Existe Algo Errado!' ,'" + strMensagem + "', 'danger');", True)

                FillCustomerInGrid()

            End If
        End Sub


        Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        End Sub

        Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)

        End Sub

        Protected Sub GridView1_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GridView1.RowCancelingEdit
            GridView1.EditIndex = -1
            FillCustomerInGrid()

        End Sub

        Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs)
            GridView1.EditIndex = e.NewEditIndex
            FillCustomerInGrid()

        End Sub


        Protected Sub GridView1_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)

            Dim strMensagem As String = ""

            If (GridView1.EditIndex <> -1) Then


                Dim lblData As Label
                lblData = CType(GridView1.Rows(e.RowIndex).FindControl("lblData"), Label)

                Dim lblContrato As Label
                lblContrato = CType(GridView1.Rows(e.RowIndex).FindControl("lblContrato"), Label)

                Dim lblParcela As Label
                lblParcela = CType(GridView1.Rows(e.RowIndex).FindControl("lblParcela"), Label)

                Dim lblValor As Label
                lblValor = CType(GridView1.Rows(e.RowIndex).FindControl("lblValor"), Label)

                Dim lblCodCob As Label
                lblCodCob = CType(GridView1.Rows(e.RowIndex).FindControl("lblCodCob"), Label)

                Dim lblCobradora As Label
                lblCobradora = CType(GridView1.Rows(e.RowIndex).FindControl("lblCobradora"), Label)

                Dim txtDtPagto As TextBox
                txtDtPagto = CType(GridView1.Rows(e.RowIndex).FindControl("txtDtPagto"), TextBox)

                Dim lblRecebido As Label
                lblRecebido = CType(GridView1.Rows(e.RowIndex).FindControl("lblRecebido"), Label)

                Dim lblValorRec As Label
                lblValorRec = CType(GridView1.Rows(e.RowIndex).FindControl("lblValorRec"), Label)

                If txtDtPagto.Text.Trim() = "" Then

                    strMensagem = "Por favor, preencher data de pagamento."

                Else
                    objAjusteIP = New DbAjusteIP
                    Dim txtData As String = lblData.Text
                    Dim txtContrato As String = lblContrato.Text
                    Dim txtParcela As String = lblParcela.Text
                    Dim txtCodCob As String = lblCodCob.Text

                    If strMensagem = "" Then
                        objAjusteIP.PagtoManual(txtData, txtContrato, txtParcela, txtCodCob, txtDtPagto.Text)
                        GridView1.EditIndex = -1

                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Atualização' ,'Os dados foram salvos com sucesso.', 'success');", True)

                        FillCustomerInGrid()
                    End If

                End If

            End If


            If strMensagem <> "" Then
                FillCustomerInGrid()
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Existe Algo Errado!' ,'" + strMensagem + "', 'danger');", True)
            End If

        End Sub


        Protected Sub GridView1_RowCreated(sender As Object, e As GridViewRowEventArgs)
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

        Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
            FillCustomerInGrid()
        End Sub

        Protected Sub btnProcessar_Click(sender As Object, e As EventArgs)

            objAjusteIP = New DbAjusteIP
            objAjusteIP.CalcularIP(txtDataFech.Text)

            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Ajuste IP' ,'Processamento realizado com sucesso.', 'success');", True)
        End Sub

        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../../Menu.aspx")
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


        Public Overrides Sub VerifyRenderingInServerForm(control As Control)
            'Not Remove
            ' Confirms that an HtmlForm control is rendered for the specified ASP.NET
            '     server control at run time. 

        End Sub


        Protected Sub btnExcel_Click(sender As Object, e As EventArgs)

            Try

                If Not IsNothing(GridView1.HeaderRow) Then

                    Response.Clear()
                    Response.Buffer = True
                    Dim filename As String = String.Format("Ajuste_IP_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename)
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.ms-excel"

                    Using sw As New StringWriter()

                        Dim hw As New HtmlTextWriter(sw)

                        GridView1.FooterRow.Visible = False
                        GridView1.Columns(11).Visible = False
                        GridView1.Columns(12).Visible = False


                        GridView1.HeaderRow.BackColor = Color.White
                        For Each cell As TableCell In GridView1.HeaderRow.Cells
                            cell.CssClass = "GridviewScrollC3Header"


                            Dim controls As New List(Of Control)()
                            For Each control As Control In cell.Controls
                                controls.Add(control)
                            Next

                            For Each control As Control In controls
                                Select Case control.GetType().Name
                                    Case "Label"
                                        cell.Controls.Add(New Literal() With {
                                         .Text = TryCast(control, Label).Text
                                        })
                                        Exit Select

                                End Select
                                cell.Controls.Remove(control)
                            Next

                        Next


                        For Each row As GridViewRow In GridView1.Rows
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
                                        Case "Label"
                                            cell.Controls.Add(New Literal() With {
                                             .Text = TryCast(control, Label).Text
                                            })
                                            Exit Select

                                    End Select
                                    cell.Controls.Remove(control)
                                Next
                            Next
                        Next

                        GridView1.RenderControl(hw)

                        Dim fi As FileInfo = New FileInfo(Server.MapPath("~/css/GridviewScroll.css"))
                        Dim sb As New System.Text.StringBuilder
                        Dim sr As StreamReader = fi.OpenText()
                        Do While sr.Peek() >= 0
                            sb.Append(sr.ReadLine())
                        Loop
                        sr.Close()

                        Dim style As String = "<html><head><style type='text/css'>" & sb.ToString() & "</style><head>"

                        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default
                        HttpContext.Current.Response.Write(style)
                        HttpContext.Current.Response.Output.Write(sw.ToString())
                        HttpContext.Current.Response.Flush()
                        HttpContext.Current.Response.SuppressContent = True
                        HttpContext.Current.ApplicationInstance.CompleteRequest()
                    End Using

                End If

            Catch ex As Exception
                Throw ex
            End Try

        End Sub


    End Class
End Namespace