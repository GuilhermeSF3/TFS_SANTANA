Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic
Imports Santana.Seguranca
Imports Util

Namespace Paginas.Cadastro


    Public Class Risco

        Inherits SantanaPage

        Dim objData As DbRisco

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then


                FillDataInGrid()

            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub


        Protected Sub FillDataInGrid()

            Dim dtData As DataTable = New DbRisco().CarregarTodosRegistros()
            Try

                If dtData.Rows.Count > 0 Then
                    GridView1.DataSource = dtData
                    GridView1.DataBind()
                Else
                    dtData.Rows.Add(dtData.NewRow())
                    Me.GridView1.DataSource = dtData
                    GridView1.DataBind()

                    Dim totalColumns As Integer
                    totalColumns = GridView1.Rows(0).Cells.Count
                    GridView1.Rows(0).Cells.Clear()
                    GridView1.Rows(0).Cells.Add(New TableCell())
                    GridView1.Rows(0).Cells(0).ColumnSpan = totalColumns
                    GridView1.Rows(0).Cells(0).Style.Add("text-align", "center")
                    GridView1.Rows(0).Cells(0).Text = "Não há registros cadastrado"

                End If

            Catch ex As Exception
            End Try

        End Sub





        Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim strMensagem As String = ""

            If e.CommandName.Equals("AddNew") Then


                Dim txtDtRef As TextBox
                txtDtRef = CType(GridView1.HeaderRow.FindControl("txtDtRef"), TextBox)

                Dim cmbOp As DropDownList
                cmbOp = CType(GridView1.HeaderRow.FindControl("cmbOp"), DropDownList)

                Dim txtRisco As TextBox
                txtRisco = CType(GridView1.HeaderRow.FindControl("txtRisco"), TextBox)

                Dim txtVlrRisco As TextBox
                txtVlrRisco = CType(GridView1.HeaderRow.FindControl("txtVlrRisco"), TextBox)

                Dim str_op As String()
                str_op = cmbOp.SelectedItem.ToString.Split("-")

                Dim s_codOp As String = str_op(0)
                Dim s_nomeOp As String = str_op(1)
                Dim s_cpfOP As String = str_op(2)

                objData = New DbRisco
                Dim tdData As DataTable = objData.CarregarRegistro(txtDtRef.Text.ToString(), s_codOp.Trim, s_nomeOp.Trim, s_cpfOP.Trim)
                If tdData.Rows.Count > 0 Then
                    strMensagem = "O registro já esta cadastrado"
                End If

                If strMensagem = "" Then
                    objData.InserirRegistro(txtDtRef.Text.ToString(), s_codOp, s_cpfOP, s_nomeOp, txtRisco.Text.ToString(), txtVlrRisco.Text.ToString())

                    FillDataInGrid()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Atualização' ,'Os dados foram salvos com sucesso.', 'success');", True)
                End If

            ElseIf e.CommandName.Equals("Delete") Then

                objData = New DbRisco
                Dim index As Integer
                index = Convert.ToInt32(e.CommandArgument)

                Dim str_op As String()
                str_op = GridView1.DataKeys(index).Values(1).ToString().ToString.Split("-")

                Dim s_codOp As String = str_op(0)
                Dim s_nomeOp As String = str_op(1)
                Dim s_risco As String = GridView1.DataKeys(index).Values(2).ToString()
                Dim s_Vlrrisco As String = GridView1.DataKeys(index).Values(3).ToString()

                objData.ApagarRegistro(GridView1.DataKeys(index).Values(0).ToString(), s_codOp, s_nomeOp, s_risco, s_Vlrrisco)
                FillDataInGrid()
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Exclusão de Registro' ,'Registro excluido com sucesso.', 'success');", True)

            End If



            If strMensagem <> "" Then
                FillDataInGrid()
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Existe Algo Errado!' ,'" + strMensagem + "', 'danger');", True)
            End If

        End Sub


        Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

            Try
                If e.Row.RowType = DataControlRowType.DataRow Then

                End If


                If e.Row.RowType = DataControlRowType.Header Then

                    Dim cmbOp As DropDownList
                    cmbOp = CType(e.Row.FindControl("cmbOp"), DropDownList)

                    objData = New DbRisco
                    objData.CarregaCodOp(cmbOp)

                End If

            Catch ex As Exception
                Throw New Exception(ex.Message.ToString(), ex)
            End Try

        End Sub



        Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)

        End Sub

        Protected Sub GridView1_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GridView1.RowCancelingEdit
            GridView1.EditIndex = -1
            FillDataInGrid()

            CType(GridView1.HeaderRow.FindControl("txtDtRef"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("cmbOp"), DropDownList).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtRisco"), TextBox).Visible = True

            CType(GridView1.HeaderRow.FindControl("lnkAddNew"), LinkButton).Visible = True


        End Sub

        Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs)
            GridView1.EditIndex = e.NewEditIndex
            FillDataInGrid()

            CType(GridView1.HeaderRow.FindControl("txtDtRef"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("cmbOp"), DropDownList).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtRisco"), TextBox).Visible = False

            CType(GridView1.HeaderRow.FindControl("lnkAddNew"), LinkButton).Visible = False
        End Sub


        Protected Sub GridView1_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)

            Dim strMensagem As String = ""

            If (GridView1.EditIndex <> -1) Then

                objData = New DbRisco

                objData = New DbRisco
                If strMensagem = "" Then

                    Dim lblData As Label
                    lblData = CType(GridView1.Rows(e.RowIndex).FindControl("lblData"), Label)

                    Dim lblOp As Label
                    lblOp = CType(GridView1.Rows(e.RowIndex).FindControl("lblOp"), Label)

                    Dim lblRisco As TextBox
                    lblRisco = CType(GridView1.Rows(e.RowIndex).FindControl("lblRisco"), TextBox)

                    Dim lblVlrRisco As TextBox
                    lblVlrRisco = CType(GridView1.Rows(e.RowIndex).FindControl("lblVlrRisco"), TextBox)

                    Dim str_op As String()
                    str_op = lblOp.Text.Split("-")

                    Dim s_CodOp As String = str_op(0)
                    Dim s_NomeOp As String = str_op(1)

                    objData.AtualizarRegistro(lblData.Text, s_CodOp.Trim, s_NomeOp.Trim, lblRisco.Text, lblVlrRisco.Text)

                    GridView1.EditIndex = -1
                    FillDataInGrid()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Atualização' ,'Os dados foram salvos com sucesso.', 'success');", True)

                End If

            End If


            If strMensagem <> "" Then
                FillDataInGrid()
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

        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../Menu.aspx")
        End Sub

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
                    Dim filename As String = String.Format("CadastroRisco_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename)
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.ms-excel"

                    Using sw As New StringWriter()

                        Dim hw As New HtmlTextWriter(sw)

                        GridView1.FooterRow.Visible = False
                        GridView1.Columns(3).Visible = False
                        GridView1.Columns(4).Visible = False


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