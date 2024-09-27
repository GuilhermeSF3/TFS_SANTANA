Imports System
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Santana.DataBase
Imports Santana.DataBase.PT
Imports Santana.Seguranca

Namespace Paginas.Cadastro.PT


    Public Class Cidade
        Inherits SantanaPage

        Dim objData As DbCidade

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                FillDataInGrid()

            End If

   
            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub


        Private Function GetData() As DataTable
            Return New DbCidade().CarregarTodosRegistros()
        End Function

        Protected Sub FillDataInGrid()

            DataGridView = GetData()
            Dim dtData As DataTable = DataGridView()

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


                Dim txtNewCOD As TextBox
                txtNewCOD = CType(GridView1.HeaderRow.FindControl("txtNewCOD"), TextBox)

                Dim txtNewDescr As TextBox
                txtNewDescr = CType(GridView1.HeaderRow.FindControl("txtNewDescr"), TextBox)

                Dim cmbNewRegiao As DropDownList
                cmbNewRegiao = CType(GridView1.HeaderRow.FindControl("cmbNewRegiao"), DropDownList)

                Dim cmbNewGrupo As DropDownList
                cmbNewGrupo = CType(GridView1.HeaderRow.FindControl("cmbNewGrupo"), DropDownList)

                Dim cmbNewUF As DropDownList
                cmbNewUF = CType(GridView1.HeaderRow.FindControl("cmbNewUF"), DropDownList)

                Dim txtNewCep_De As TextBox
                txtNewCep_De = CType(GridView1.HeaderRow.FindControl("txtNewCep_De"), TextBox)

                Dim txtNewCep_Ate As TextBox
                txtNewCep_Ate = CType(GridView1.HeaderRow.FindControl("txtNewCep_Ate"), TextBox)

                Dim cmbNewAgente As DropDownList
                cmbNewAgente = CType(GridView1.HeaderRow.FindControl("cmbNewAgente"), DropDownList)

                If txtNewCOD.Text.Trim() = "" Or
                    txtNewDescr.Text.Trim() = "" Then

                    strMensagem = "Por favor, preencher todos os campos."

                Else

                    objData = New DbCidade
                    Dim tdData As DataTable = objData.CarregarRegistro(txtNewCOD.Text.Trim())
                    If tdData.Rows.Count > 0 Then
                        strMensagem = "O registro já esta cadastrado"
                    End If


                    If strMensagem = "" Then
                        objData.InserirRegistro(txtNewCOD.Text.Trim(),
                                                txtNewDescr.Text.Trim(),
                                                cmbNewUF.SelectedValue.Trim(),
                                                cmbNewRegiao.SelectedValue.Trim(),
                                                cmbNewGrupo.SelectedValue.Trim(),
                                                txtNewCep_De.Text.Trim(),
                                                txtNewCep_Ate.Text.Trim(), cmbNewAgente.SelectedValue.Trim())

                        FillDataInGrid()
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Atualização' ,'Os dados foram salvos com sucesso.', 'success');", True)

                    End If
                End If

            ElseIf e.CommandName.Equals("Delete") Then
                objData = New DbCidade
                Dim deletebtn As LinkButton = e.CommandSource
                Dim row As GridViewRow = deletebtn.NamingContainer

                objData.ApagarRegistro(GridView1.DataKeys(row.RowIndex).Values(0).ToString())
                FillDataInGrid()
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Exclusão de Registro' ,'Registro excluido com sucesso.', 'success');", True)


            End If


            If strMensagem <> "" Then
                FillDataInGrid()
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Existe Algo Errado!' ,'" + strMensagem + "', 'danger');", True)
            End If

        End Sub


        Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

            If e.Row.RowType = DataControlRowType.DataRow AndAlso GridView1.EditIndex = e.Row.RowIndex Then
                Dim cmbRegiao As DropDownList = DirectCast(e.Row.FindControl("cmbRegiao"), DropDownList)
                Dim objRegiao = New DbRegiao
                objRegiao.CarregarTodosRegistros(cmbRegiao)

                cmbRegiao.Items.FindByValue((DirectCast(e.Row.FindControl("lblregiao"), Label).Text)).Selected = True

                Dim cmbGrupo As DropDownList = DirectCast(e.Row.FindControl("cmbGrupo"), DropDownList)
                Dim objGrupo = New DbGrupo
                objGrupo.CarregarTodosRegistros(cmbGrupo)
                cmbGrupo.Items.FindByValue((DirectCast(e.Row.FindControl("lblgrupo"), Label).Text)).Selected = True

                Dim cmbAGENTE As DropDownList = DirectCast(e.Row.FindControl("cmbAGENTE"), DropDownList)
                Dim objAGENTE = New DbAgente
                objAGENTE.CarregarTodosRegistros(cmbAGENTE) ', 0, "")  ' PARAMETROS OPCIONAIS
                cmbAGENTE.Items.FindByValue((DirectCast(e.Row.FindControl("lblAGENTE"), Label).Text)).Selected = True

            End If

            Try
                If e.Row.RowType = DataControlRowType.DataRow Then

                End If

                If e.Row.RowType = DataControlRowType.Header Then

                    Dim cmbNewUF As DropDownList
                    cmbNewUF = CType(e.Row.FindControl("cmbNewUF"), DropDownList)

                    Dim cmbNewRegiao As DropDownList
                    cmbNewRegiao = CType(e.Row.FindControl("cmbNewRegiao"), DropDownList)

                    Dim cmbNewGrupo As DropDownList
                    cmbNewGrupo = CType(e.Row.FindControl("cmbNewGrupo"), DropDownList)

                    Dim cmbNewAgente As DropDownList
                    cmbNewAgente = CType(e.Row.FindControl("cmbNewAgente"), DropDownList)

                    If Not cmbNewUF Is Nothing Then
                        Dim objDataUF = New DbUF
                        objDataUF.CarregarTodosRegistros(cmbNewUF)
                    End If

                    If Not cmbNewRegiao Is Nothing Then
                        Dim objDataRegiao = New DbRegiao
                        objDataRegiao.CarregarTodosRegistros(cmbNewRegiao)
                    End If

                    If Not cmbNewGrupo Is Nothing Then
                        Dim objDataGrupo = New DbGrupo
                        objDataGrupo.CarregarTodosRegistros(cmbNewGrupo)
                    End If


                    If Not cmbNewAgente Is Nothing Then
                        Dim objDataAgente = New DbAgente
                        objDataAgente.CarregarTodosRegistros(cmbNewAgente)
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

            CType(GridView1.HeaderRow.FindControl("txtNewCOD"), TextBox).Visible = True
            CType(GridView1.HeaderRow.FindControl("txtNewDescr"), TextBox).Visible = True

            CType(GridView1.HeaderRow.FindControl("lnkAddNew"), LinkButton).Visible = True


        End Sub

        Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs)
            GridView1.EditIndex = e.NewEditIndex
            FillDataInGrid()



            CType(GridView1.HeaderRow.FindControl("txtNewCOD"), TextBox).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtNewDescr"), TextBox).Visible = False

            CType(GridView1.HeaderRow.FindControl("lnkAddNew"), LinkButton).Visible = False


        End Sub


        Protected Sub GridView1_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)

            Dim strMensagem As String = ""

            Dim cod_regiao As String = TryCast(GridView1.Rows(e.RowIndex).FindControl("cmbRegiao"), DropDownList).SelectedItem.Value
            Dim cod_grupo As String = TryCast(GridView1.Rows(e.RowIndex).FindControl("cmbGrupo"), DropDownList).SelectedItem.Value
            Dim cod_AGENTE1 As String = TryCast(GridView1.Rows(e.RowIndex).FindControl("cmbAGENTE"), DropDownList).SelectedItem.Value


            If (GridView1.EditIndex <> -1) Then

                Dim txtDescr As TextBox
                txtDescr = CType(GridView1.Rows(e.RowIndex).FindControl("txtDescr"), TextBox)

                Dim cmbUF As DropDownList
                cmbUF = CType(GridView1.Rows(e.RowIndex).FindControl("cmbUF"), DropDownList)

                Dim txtCep_De As TextBox
                txtCep_De = CType(GridView1.Rows(e.RowIndex).FindControl("txtCep_De"), TextBox)

                Dim txtCep_Ate As TextBox
                txtCep_Ate = CType(GridView1.Rows(e.RowIndex).FindControl("txtCep_Ate"), TextBox)

                If txtDescr.Text.Trim() = "" Then

                    strMensagem = "Por favor, preencher todos os campos."

                Else
                    objData = New DbCidade
                    If strMensagem = "" Then
                        objData.AtualizarRegistro(GridView1.DataKeys(e.RowIndex).Values(0).ToString(), txtDescr.Text, 0,
                                                cod_regiao,
                                                cod_grupo,
                                                txtCep_De.Text.Trim(),
                                                txtCep_Ate.Text.Trim(), cod_AGENTE1)
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
                    GridView1.AllowPaging = False
                    FillDataInGrid()
                    ExportExcel(GridView1)
                End If

            Catch ex As Exception
                Throw ex
            End Try

        End Sub




        Private Sub ExportExcel(objGrid As GridView)


            Try

                Response.Clear()
                Response.Buffer = True
                Dim filename As String = String.Format("Cidades_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
                Response.AddHeader("content-disposition", "attachment;filename=" + filename)
                Response.Charset = ""
                Response.ContentType = "application/vnd.ms-excel"

                Using sw As New StringWriter()

                    Dim hw As New HtmlTextWriter(sw)

                    GridView1.FooterRow.Visible = False

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
                    objGrid.RenderControl(hw)

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