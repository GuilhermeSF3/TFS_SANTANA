Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Santana.Seguranca

Namespace Paginas.Cadastro.Comissao_PJ


    Public Class ClienteComissao
        Inherits SantanaPage

        Dim objData As DbClienteComissao

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                FillDataInGrid()

            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub


        Private Function GetData() As DataTable
            Return New DbClienteComissao().CarregarTodosRegistros()
        End Function

        Protected Sub FillDataInGrid()

            DataGridView = GetData()
            Dim dtData As DataTable = DataGridView()

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


        Protected Sub BindGridView1DataView()

            GridView1.DataSource = DataGridView
            GridView1.DataBind()

        End Sub

        Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim strMensagem As String = ""

            If e.CommandName.Equals("AddNew") Then


                Dim ddlNewcod As DropDownList
                ddlNewcod = CType(GridView1.HeaderRow.FindControl("ddlNewcod"), DropDownList)

                Dim txtNewDATA_De As TextBox
                txtNewDATA_De = CType(GridView1.HeaderRow.FindControl("txtNewDATA_De"), TextBox)

                Dim ddlNewNOVA_MODALIDADE As DropDownList
                ddlNewNOVA_MODALIDADE = CType(GridView1.HeaderRow.FindControl("ddlNewNOVA_MODALIDADE"), DropDownList)

                Dim txtNewTX_DE As TextBox
                txtNewTX_DE = CType(GridView1.HeaderRow.FindControl("txtNewTX_DE"), TextBox)

                Dim txtNewTX_ATE As TextBox
                txtNewTX_ATE = CType(GridView1.HeaderRow.FindControl("txtNewTX_ATE"), TextBox)

                Dim txtNewPRC_COMISSAO As TextBox
                txtNewPRC_COMISSAO = CType(GridView1.HeaderRow.FindControl("txtNewPRC_COMISSAO"), TextBox)


                If ddlNewcod.SelectedValue().Trim() = "" Or
                    txtNewDATA_De.Text.Trim() = "0" Or
                    ddlNewNOVA_MODALIDADE.SelectedValue().Trim() = "nenhum" Or
                    txtNewTX_DE.Text.Trim() = "" Or
                    txtNewTX_ATE.Text.Trim() = "" Or
                    txtNewPRC_COMISSAO.Text.Trim() = "" Then



                    strMensagem = "Por favor, preencher todos os campos."

                Else

                    objData = New DbClienteComissao
                    Dim tdUser As DataTable = objData.CarregarRegistro(Convert.ToInt32(ddlNewcod.SelectedValue.Trim()), _
                                                                       Convert.ToDateTime(txtNewDATA_De.Text.Trim()))
                    If tdUser.Rows.Count > 0 Then
                        strMensagem = "O registro já esta cadastrado"
                    End If

                    Dim vlrNovaModalidade As Integer
                    If (ddlNewNOVA_MODALIDADE.SelectedValue().Trim() = "") Then
                        vlrNovaModalidade = -1
                    Else
                        vlrNovaModalidade = Convert.ToInt32(ddlNewNOVA_MODALIDADE.SelectedValue().Trim())
                    End If

                    If strMensagem = "" Then
                        objData.InserirRegistro(Convert.ToInt32(ddlNewcod.SelectedValue.Trim()), _
                                                                       Convert.ToDateTime(txtNewDATA_De.Text.Trim()), _
                                                                       vlrNovaModalidade, _
                                                                       Convert.ToDouble(txtNewTX_DE.Text.Trim()), _
                                                                       Convert.ToDouble(txtNewTX_ATE.Text.Trim()), _
                                                                       Convert.ToDouble(txtNewPRC_COMISSAO.Text.Trim()))
                        FillDataInGrid()
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Atualização' ,'Os dados foram salvos com sucesso.', 'success');", True)
                    End If
                End If

            ElseIf e.CommandName.Equals("Delete") Then
                objData = New DbClienteComissao
                Dim deletebtn As LinkButton = e.CommandSource
                Dim row As GridViewRow = deletebtn.NamingContainer


                objData.ApagarRegistro(GridView1.DataKeys(row.RowIndex).Values(0).ToString(), GridView1.DataKeys(row.RowIndex).Values(1).ToString())
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

  
                End If


                If e.Row.RowType = DataControlRowType.Header Then

                    Dim ddlNewcod As DropDownList
                    ddlNewcod = CType(e.Row.FindControl("ddlNewcod"), DropDownList)

                    If Not ddlNewcod Is Nothing Then
                        objData = New DbClienteComissao()
                        objData.CarregarUsuario(ddlNewcod)
                    End If

                End If

            Catch ex As Exception
                Throw New Exception(ex.Message.ToString(), ex)
            End Try

        End Sub



        Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)

        End Sub


        Public Sub GetName()
            CType(GridView1.HeaderRow.FindControl("lblNewNOME"), Label).Text = "teste"
        End Sub

        Protected Sub GridView1_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GridView1.RowCancelingEdit
            GridView1.EditIndex = -1
            FillDataInGrid()
            CType(GridView1.HeaderRow.FindControl("ddlNewcod"), DropDownList).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtNewDATA_De"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("ddlNewNOVA_MODALIDADE"), DropDownList).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtNewTX_DE"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtNewTX_ATE"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtNewPRC_COMISSAO"), TextBox).Visible = True

            CType(GridView1.HeaderRow.FindControl("lnkAddNew"), LinkButton).Visible = True



        End Sub

        Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs)
            GridView1.EditIndex = e.NewEditIndex
            FillDataInGrid()

            'Atualiza_NOVA_MODALIDADE(e.NewEditIndex)

            CType(GridView1.HeaderRow.FindControl("ddlNewcod"), DropDownList).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtNewDATA_De"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("ddlNewNOVA_MODALIDADE"), DropDownList).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtNewTX_DE"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtNewTX_ATE"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtNewPRC_COMISSAO"), TextBox).Visible = False

            CType(GridView1.HeaderRow.FindControl("lnkAddNew"), LinkButton).Visible = False
        End Sub


        Protected Sub GridView1_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)

            Dim strMensagem As String = ""

            If (GridView1.EditIndex <> -1) Then


                Dim lblcod As Label
                lblcod = CType(GridView1.Rows(e.RowIndex).FindControl("lblcod"), Label)

                Dim lblDT_DE As Label
                lblDT_DE = CType(GridView1.Rows(e.RowIndex).FindControl("lblDT_DE"), Label)

                Dim ddlNOVA_MODALIDADE As DropDownList
                ddlNOVA_MODALIDADE = CType(GridView1.Rows(e.RowIndex).FindControl("ddlNOVA_MODALIDADE"), DropDownList)

                Dim txtTX_DE As TextBox
                txtTX_DE = CType(GridView1.Rows(e.RowIndex).FindControl("txtTX_DE"), TextBox)

                Dim txtTX_ATE As TextBox
                txtTX_ATE = CType(GridView1.Rows(e.RowIndex).FindControl("txtTX_ATE"), TextBox)

                Dim txtPRC_COMISSAO As TextBox
                txtPRC_COMISSAO = CType(GridView1.Rows(e.RowIndex).FindControl("txtPRC_COMISSAO"), TextBox)



                If ddlNOVA_MODALIDADE.SelectedValue().Trim() = "nenhum" Or
                    txtTX_DE.Text.Trim() = "" Or
                    txtTX_ATE.Text.Trim() = "" Or
                    txtPRC_COMISSAO.Text.Trim() = "" Then
                    strMensagem = "Por favor, preencher todos os campos."

                Else
                    objData = New DbClienteComissao
                    If strMensagem = "" Then

                        Dim data_de As String
                        data_de = lblDT_DE.Text.Trim()

                        Dim vlrNovaModalidade As Integer
                        If ddlNOVA_MODALIDADE.SelectedValue().Trim() = "" Then
                            vlrNovaModalidade = -1
                        Else
                            vlrNovaModalidade = Convert.ToInt32(ddlNOVA_MODALIDADE.SelectedValue().Trim())
                        End If


                        objData.AtualizarRegistro(Convert.ToInt32(lblcod.Text.Trim()), _
                                                                       Convert.ToDateTime(data_de.Trim()), _
                                                                       vlrNovaModalidade, _
                                                                       Convert.ToDouble(txtTX_DE.Text.Trim()), _
                                                                       Convert.ToDouble(txtTX_ATE.Text.Trim()), _
                                                                       Convert.ToDouble(txtPRC_COMISSAO.Text.Trim()))
                        GridView1.EditIndex = -1
                        FillDataInGrid()
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Atualização' ,'Os dados foram salvos com sucesso.', 'success');", True)
                    End If

                End If

            End If


            If strMensagem <> "" Then
                FillDataInGrid()
                'Atualiza_NOVA_MODALIDADE(e.RowIndex)
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Existe Algo Errado!' ,'" + strMensagem + "', 'danger');", True)
            Else

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
            Response.Redirect("../../Menu.aspx")
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
                    Dim filename As String = String.Format("OperadorAtendeCliente_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename)
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.ms-excel"

                    Using sw As New StringWriter()

                        Dim hw As New HtmlTextWriter(sw)

                        GridView1.FooterRow.Visible = False
                        GridView1.Columns(6).Visible = False
                        GridView1.Columns(7).Visible = False


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


        Protected Sub GridView1_DataBound(sender As Object, e As EventArgs)
            Dim gridView As GridView = CType(sender, GridView)
            If Not IsNothing(gridView.DataSource) AndAlso CType(gridView.DataSource, System.Data.DataTable).Rows.Count > 0 Then
                Util.CWebPageUtil.PreencheListaDePaginas(gridView, CType(gridView.BottomPagerRow.Cells(0).FindControl("PagerPages"), DropDownList))
            End If
        End Sub

        Protected Sub PagerPages_SelectedIndexChanged(sender As Object, e As EventArgs)

            GridView1.DataSource = DataGridView()
            GridView1.PageIndex = CType(sender, DropDownList).SelectedIndex
            GridView1.DataBind()

        End Sub


        Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)


            If e.NewPageIndex >= 0 Then
                GridView1.PageIndex = e.NewPageIndex
                BindGridView1DataView()
            End If

        End Sub


    End Class


End Namespace