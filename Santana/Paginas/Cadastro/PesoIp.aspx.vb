Imports System
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Santana.Seguranca

Namespace Paginas.Cadastro


    Public Class PesoIp
        Inherits SantanaPage

        Dim objData As DbPesoIp

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                FillDataInGrid()

            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub


        Protected Sub FillDataInGrid()

            Dim dtData As DataTable = New DbPesoIp().CarregarTodosRegistros()
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
                Throw New Exception(ex.Message.ToString(), ex)
            End Try

        End Sub





        Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim strMensagem As String = ""

            If e.CommandName.Equals("AddNew") Then


                Dim txtNewDATA_REF As TextBox
                txtNewDATA_REF = CType(GridView1.HeaderRow.FindControl("txtNewDATA_REF"), TextBox)

                Dim cmbNewRATING As DropDownList
                cmbNewRATING = CType(GridView1.HeaderRow.FindControl("cmbNewRATING"), DropDownList)

                Dim cmbNewFAIXA As DropDownList
                cmbNewFAIXA = CType(GridView1.HeaderRow.FindControl("cmbNewFAIXA"), DropDownList)

                Dim txtNewPESO As TextBox
                txtNewPESO = CType(GridView1.HeaderRow.FindControl("txtNewPESO"), TextBox)


                Dim result As Date
                If Not Date.TryParse(txtNewDATA_REF.Text, Globalization.CultureInfo.GetCultureInfo("pt-BR"), Globalization.DateTimeStyles.None, result) Then
                    strMensagem = "Por favor, preencher com uma data válida."
                    FillDataInGrid()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Existe Algo Errado!' ,'" + strMensagem + "', 'danger');", True)
                    Return
                End If


                If txtNewDATA_REF.Text.Trim() = "" Or
                    txtNewPESO.Text.Trim() = "" Then

                    strMensagem = "Por favor, preencher todos os campos."

                Else

                    objData = New DbPesoIp
                    Dim tdUser As DataTable = objData.CarregarRegistro(txtNewDATA_REF.Text.Trim, cmbNewRATING.SelectedValue, cmbNewFAIXA.SelectedValue)
                    If tdUser.Rows.Count > 0 Then
                        strMensagem = "O registro já esta cadastrado"
                    End If


                    If strMensagem = "" Then
                        objData.InserirRegistro(txtNewDATA_REF.Text, _
                                                     cmbNewRATING.SelectedValue, _
                                                     cmbNewFAIXA.SelectedValue, _
                                                     Convert.ToDouble(txtNewPESO.Text))


                        FillDataInGrid()
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Atualização' ,'Os dados foram salvos com sucesso.', 'success');", True)
                    End If
                End If

            ElseIf e.CommandName.Equals("Delete") Then
                objData = New DbPesoIp
                Dim index As Integer
                index = Convert.ToInt32(e.CommandArgument)

                Dim dataRef As DateTime = Convert.ToDateTime(GridView1.DataKeys(index).Values(0).ToString())

                objData.ApagarRegistro(dataRef, GridView1.DataKeys(index).Values(1).ToString(), GridView1.DataKeys(index).Values(2).ToString())
                FillDataInGrid()

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Exclusão de Registro' ,'Registro excluido com sucesso.', 'success');", True)

            End If



            If strMensagem <> "" Then

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Existe Algo Errado!' ,'" + strMensagem + "', 'danger');", True)

                FillDataInGrid()

            End If

        End Sub


        Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

            Try
                If e.Row.RowType = DataControlRowType.DataRow Then
                    
                    Dim cmbRATING As DropDownList
                    cmbRATING = CType(e.Row.FindControl("cmbRATING"), DropDownList)

                    If Not cmbRATING Is Nothing Then
                        cmbRATING.SelectedValue = GridView1.DataKeys(e.Row.RowIndex).Values(1).ToString()
                    End If

                    
                    Dim cmbFAIXA As DropDownList
                    cmbFAIXA = CType(e.Row.FindControl("cmbFAIXA"), DropDownList)

                    If Not cmbFAIXA Is Nothing Then
                        cmbFAIXA.SelectedValue = GridView1.DataKeys(e.Row.RowIndex).Values(2).ToString()
                    End If
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

            CType(GridView1.HeaderRow.FindControl("txtNewDATA_REF"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("cmbNewRATING"), DropDownList).Visible = True
            CType(GridView1.HeaderRow.FindControl("cmbNewFAIXA"), DropDownList).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtNewPESO"), TextBox).Visible = True

            CType(GridView1.HeaderRow.FindControl("lnkAddNew"), LinkButton).Visible = True


        End Sub

        Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs)
            GridView1.EditIndex = e.NewEditIndex
            FillDataInGrid()

            CType(GridView1.HeaderRow.FindControl("txtNewDATA_REF"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("cmbNewRATING"), DropDownList).Visible = False
            CType(GridView1.HeaderRow.FindControl("cmbNewFAIXA"), DropDownList).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtNewPESO"), TextBox).Visible = False

            CType(GridView1.HeaderRow.FindControl("lnkAddNew"), LinkButton).Visible = False
        End Sub


        Protected Sub GridView1_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)

            Dim strMensagem As String = ""

            If (GridView1.EditIndex <> -1) Then

                Dim txtPESO As TextBox
                txtPESO = CType(GridView1.Rows(e.RowIndex).FindControl("txtPESO"), TextBox)


                If txtPESO.Text.Trim() = "" Then

                    strMensagem = "Por favor, preencher todos os campos."

                Else
                    objData = New DbPesoIp
                    If strMensagem = "" Then

                        Dim dataRef As DateTime = Convert.ToDateTime(GridView1.DataKeys(e.RowIndex).Values(0).ToString())

                        objData.AtualizarRegistro(dataRef, _
                                                  GridView1.DataKeys(e.RowIndex).Values(1).ToString(), _
                                                  GridView1.DataKeys(e.RowIndex).Values(2).ToString(), _
                                                  Convert.ToDouble(txtPESO.Text))
                        GridView1.EditIndex = -1
                        FillDataInGrid()
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Atualização' ,'Os dados foram salvos com sucesso.', 'success');", True)
                    End If

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
                    Dim filename As String = String.Format("Cadastro_PesoIP_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename)
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.ms-excel"

                    Using sw As New StringWriter()

                        Dim hw As New HtmlTextWriter(sw)

                        GridView1.FooterRow.Visible = False
                        GridView1.Columns(4).Visible = False
                        GridView1.Columns(5).Visible = False


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
                                        cell.Controls.Add(New Literal() With { _
                                         .Text = TryCast(control, Label).Text _
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
                                            cell.Controls.Add(New Literal() With { _
                                             .Text = TryCast(control, Label).Text _
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