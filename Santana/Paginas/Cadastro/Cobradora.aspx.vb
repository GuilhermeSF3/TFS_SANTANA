Imports System
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Net.Mime
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Santana.Seguranca
Imports Santana.da

Namespace Paginas.Cadastro


    Public Class Cobradora
        Inherits SantanaPage

        Dim objData As DbCobradora

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                FillDataInGrid()

            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub


        Protected Sub FillDataInGrid()

            Dim dtData As DataTable = New DbCobradora().CarregarTodosRegistros()
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


                Dim txtNewCOCodProduto As TextBox
                txtNewCOCodProduto = CType(GridView1.HeaderRow.FindControl("txtNewCOCodProduto"), TextBox)

                Dim txtNewCOCod As TextBox
                txtNewCOCod = CType(GridView1.HeaderRow.FindControl("txtNewCOCod"), TextBox)

                Dim txtNewCODescr As TextBox
                txtNewCODescr = CType(GridView1.HeaderRow.FindControl("txtNewCODescr"), TextBox)

                Dim cmbNewCOAtivo As DropDownList
                cmbNewCOAtivo = CType(GridView1.HeaderRow.FindControl("cmbNewCOAtivo"), DropDownList)

                Dim cmbNewcoTela As DropDownList
                cmbNewcoTela = CType(GridView1.HeaderRow.FindControl("cmbNewcoTela"), DropDownList)




                If txtNewCOCodProduto.Text.Trim() = "" Or
                    txtNewCOCod.Text.Trim() = "" Or
                    txtNewCODescr.Text.Trim() = "" Then

                    strMensagem = "Por favor, preencher todos os campos."

                Else

                    objData = New DbCobradora
                    Dim tdData As DataTable = objData.CarregarRegistro(Convert.ToInt32(txtNewCOCodProduto.Text.Trim()), Convert.ToInt32(txtNewCOCod.Text.Trim()))
                    If tdData.Rows.Count > 0 Then
                        strMensagem = "O registro já esta cadastrado"
                    End If


                    If strMensagem = "" Then
                        objData.InserirRegistro(Convert.ToInt32(txtNewCOCodProduto.Text.Trim()), _
                                                Convert.ToInt32(txtNewCOCod.Text.Trim()), _
                                                txtNewCODescr.Text.Trim(),
                                                IIf(cmbNewCOAtivo.SelectedValue.Equals("0"), False, True), _
                                                Convert.ToInt32(cmbNewcoTela.SelectedValue.ToString()))

                        FillDataInGrid()
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Atualização' ,'Os dados foram salvos com sucesso.', 'success');", True)


                    End If
                End If

            ElseIf e.CommandName.Equals("Delete") Then
                objData = New DbCobradora
                Dim index As Integer
                index = Convert.ToInt32(e.CommandArgument)

                objData.ApagarRegistro(Convert.ToInt32(GridView1.DataKeys(index).Values(0).ToString()), Convert.ToInt32(GridView1.DataKeys(index).Values(1).ToString()))
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

                    Dim cmbCOAtivo As DropDownList
                    cmbCOAtivo = CType(e.Row.FindControl("cmbCOAtivo"), DropDownList)

                    If Not cmbCOAtivo Is Nothing Then
                        cmbCOAtivo.SelectedValue = IIf(GridView1.DataKeys(e.Row.RowIndex).Values(2).Equals(False), "0", "1")
                    End If


                    Dim cmbcoTela As DropDownList
                    cmbcoTela = CType(e.Row.FindControl("cmbcoTela"), DropDownList)

                    If Not cmbcoTela Is Nothing Then
                        cmbcoTela.SelectedValue = GridView1.DataKeys(e.Row.RowIndex).Values(3)
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

            CType(GridView1.HeaderRow.FindControl("txtNewCOCodProduto"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtNewCOCod"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtNewCODescr"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("cmbNewCOAtivo"), DropDownList).Visible = True
            CType(GridView1.HeaderRow.FindControl("cmbNewcoTela"), DropDownList).Visible = True

            CType(GridView1.HeaderRow.FindControl("lnkAddNew"), LinkButton).Visible = True


        End Sub

        Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs)
            GridView1.EditIndex = e.NewEditIndex
            FillDataInGrid()


            CType(GridView1.HeaderRow.FindControl("txtNewCOCodProduto"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtNewCOCod"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtNewCODescr"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("cmbNewCOAtivo"), DropDownList).Visible = False
            CType(GridView1.HeaderRow.FindControl("cmbNewcoTela"), DropDownList).Visible = False

            CType(GridView1.HeaderRow.FindControl("lnkAddNew"), LinkButton).Visible = False
        End Sub


        Protected Sub GridView1_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)

            Dim strMensagem As String = ""

            If (GridView1.EditIndex <> -1) Then

                Dim txtCODescr As TextBox
                txtCODescr = CType(GridView1.Rows(e.RowIndex).FindControl("txtCODescr"), TextBox)


                Dim cmbCOAtivo As DropDownList
                cmbCOAtivo = CType(GridView1.Rows(e.RowIndex).FindControl("cmbCOAtivo"), DropDownList)


                Dim cmbcoTela As DropDownList
                cmbcoTela = CType(GridView1.Rows(e.RowIndex).FindControl("cmbcoTela"), DropDownList)


                If txtCODescr.Text.Trim() = "" Then

                    strMensagem = "Por favor, preencher todos os campos."

                Else
                    objData = New DbCobradora
                    If strMensagem = "" Then

                        objData.AtualizarRegistro(Convert.ToInt32(GridView1.DataKeys(e.RowIndex).Values(0).ToString()), _
                                                  Convert.ToInt32(GridView1.DataKeys(e.RowIndex).Values(1).ToString()), _
                                                  txtCODescr.Text.Trim(), _
                                                  IIf(cmbCOAtivo.SelectedValue.Equals("0"), False, True), _
                                                  Convert.ToInt32(cmbcoTela.SelectedValue))
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
                    Dim filename As String = String.Format("Cadastro_Cobradora_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename)
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.ms-excel"

                    Using sw As New StringWriter()

                        Dim hw As New HtmlTextWriter(sw)

                        GridView1.FooterRow.Visible = False
                        GridView1.Columns(5).Visible = False
                        GridView1.Columns(6).Visible = False


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