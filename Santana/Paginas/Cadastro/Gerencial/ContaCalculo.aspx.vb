Imports System
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Santana.Seguranca

Namespace Paginas.Cadastro.Gerencial


    Public Class ContaCalculo
        Inherits SantanaPage

        Dim objData As DbContaCalculo

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                CarregarContas()
                FillDataInGrid()

            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub

        Private Function GetData() As DataTable
            Return New DbContaCalculo().CarregarTodosRegistros(ddlCodConta.SelectedValue)
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

                Return

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


        Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
            Try
                FillDataInGrid()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try

        End Sub



        Private Sub CarregarContas()

            Try

                objData = New DbContaCalculo()
                objData.CarregarContas(ddlCodConta)


                ddlCodConta.Items.Insert(0, New ListItem("Todas", ""))
                ddlCodConta.SelectedIndex = 0

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub





        Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim strMensagem As String = ""

            If e.CommandName.Equals("AddNew") Then


                Dim cmbNewCOD_CONTA As DropDownList
                cmbNewCOD_CONTA = CType(GridView1.HeaderRow.FindControl("cmbNewCOD_CONTA"), DropDownList)

                Dim cmbNewCONTA_CALCULO As DropDownList
                cmbNewCONTA_CALCULO = CType(GridView1.HeaderRow.FindControl("cmbNewCONTA_CALCULO"), DropDownList)

                Dim cmbNewCONTA_CAMPO As DropDownList
                cmbNewCONTA_CAMPO = CType(GridView1.HeaderRow.FindControl("cmbNewCONTA_CAMPO"), DropDownList)

                Dim cmbNewSINAL As DropDownList
                cmbNewSINAL = CType(GridView1.HeaderRow.FindControl("cmbNewSINAL"), DropDownList)


                If cmbNewCONTA_CALCULO.SelectedValue.Trim() = "" Then

                    strMensagem = "Por favor, preencher todos os campos."

                Else

                    objData = New DbContaCalculo
                    Dim tdData As DataTable = objData.CarregarRegistro(cmbNewCOD_CONTA.SelectedValue, cmbNewCONTA_CALCULO.SelectedValue, cmbNewCONTA_CAMPO.SelectedValue)
                    If tdData.Rows.Count > 0 Then
                        strMensagem = "O registro já esta cadastrado"
                    End If


                    If strMensagem = "" Then
                        objData.InserirRegistro(cmbNewCOD_CONTA.SelectedValue, _
                                                cmbNewCONTA_CALCULO.SelectedValue, _
                                                cmbNewCONTA_CAMPO.SelectedValue, _
                                                cmbNewSINAL.SelectedValue)

                        FillDataInGrid()
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Atualização' ,'Os dados foram salvos com sucesso.', 'success');", True)


                    End If
                End If


            ElseIf e.CommandName.Equals("Delete") Then
                objData = New DbContaCalculo
                'Dim index As Integer
                'index = Convert.ToInt32(e.CommandArgument)

                Dim deletebtn As LinkButton = e.CommandSource
                Dim row As GridViewRow = deletebtn.NamingContainer

                objData.ApagarRegistro(GridView1.DataKeys(row.RowIndex).Values(0).ToString(), GridView1.DataKeys(row.RowIndex).Values(1).ToString(), GridView1.DataKeys(row.RowIndex).Values(2).ToString())
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


                    'Dim cmbCOD_CONTA As DropDownList
                    'cmbCOD_CONTA = CType(e.Row.FindControl("cmbCOD_CONTA"), DropDownList)

                    'If Not cmbCOD_CONTA Is Nothing Then
                    '    cmbCOD_CONTA.SelectedValue = GridView1.DataKeys(e.Row.RowIndex).Values(0)
                    'End If

                    'Dim cmbCONTA_CALCULO As DropDownList
                    'cmbCONTA_CALCULO = CType(e.Row.FindControl("cmbCONTA_CALCULO"), DropDownList)

                    'If Not cmbCONTA_CALCULO Is Nothing Then
                    '    cmbCONTA_CALCULO.SelectedValue = GridView1.DataKeys(e.Row.RowIndex).Values(1)
                    'End If

                    Dim cmbCONTA_CAMPO As DropDownList
                    cmbCONTA_CAMPO = CType(e.Row.FindControl("cmbCONTA_CAMPO"), DropDownList)

                    If Not cmbCONTA_CAMPO Is Nothing Then
                        cmbCONTA_CAMPO.SelectedValue = GridView1.DataKeys(e.Row.RowIndex).Values(2)
                    End If

                    Dim cmbSINAL As DropDownList
                    cmbSINAL = CType(e.Row.FindControl("cmbSINAL"), DropDownList)

                    If Not cmbSINAL Is Nothing Then
                        cmbSINAL.SelectedValue = GridView1.DataKeys(e.Row.RowIndex).Values(3)
                    End If


                End If



                If e.Row.RowType = DataControlRowType.Header Then

                    Dim cmbNewCOD_CONTA As DropDownList
                    cmbNewCOD_CONTA = CType(e.Row.FindControl("cmbNewCOD_CONTA"), DropDownList)

                    If Not cmbNewCOD_CONTA Is Nothing Then
                        objData = New DbContaCalculo()
                        objData.CarregarContas(cmbNewCOD_CONTA)
                    End If



                    Dim cmbNewCONTA_CAMPO As DropDownList
                    cmbNewCONTA_CAMPO = CType(e.Row.FindControl("cmbNewCONTA_CAMPO"), DropDownList)


                    Dim cmbNewCONTA_CALCULO As DropDownList
                    cmbNewCONTA_CALCULO = CType(e.Row.FindControl("cmbNewCONTA_CALCULO"), DropDownList)

                    If Not cmbNewCONTA_CALCULO Is Nothing Then
                        objData = New DbContaCalculo
                        objData.CarregarContaCalculo(cmbNewCONTA_CAMPO.SelectedValue, cmbNewCONTA_CALCULO)
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

            CType(GridView1.HeaderRow.FindControl("cmbNewCOD_CONTA"), DropDownList).Visible = True
            CType(GridView1.HeaderRow.FindControl("cmbNewCONTA_CAMPO"), DropDownList).Visible = True
            CType(GridView1.HeaderRow.FindControl("cmbNewSINAL"), DropDownList).Visible = True
            CType(GridView1.HeaderRow.FindControl("cmbNewCONTA_CALCULO"), DropDownList).Visible = True

            CType(GridView1.HeaderRow.FindControl("lnkAddNew"), LinkButton).Visible = True


        End Sub

        Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs)
            GridView1.EditIndex = e.NewEditIndex
            FillDataInGrid()


            CType(GridView1.HeaderRow.FindControl("cmbNewCOD_CONTA"), DropDownList).Visible = False
            CType(GridView1.HeaderRow.FindControl("cmbNewCONTA_CAMPO"), DropDownList).Visible = False
            CType(GridView1.HeaderRow.FindControl("cmbNewSINAL"), DropDownList).Visible = False
            CType(GridView1.HeaderRow.FindControl("cmbNewCONTA_CALCULO"), DropDownList).Visible = False

            CType(GridView1.HeaderRow.FindControl("lnkAddNew"), LinkButton).Visible = False
        End Sub


        Protected Sub GridView1_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)

            Dim strMensagem As String = ""

            If (GridView1.EditIndex <> -1) Then

                Dim cmbCONTA_CAMPO As DropDownList
                cmbCONTA_CAMPO = CType(GridView1.Rows(e.RowIndex).FindControl("cmbCONTA_CAMPO"), DropDownList)


                Dim cmbSINAL As DropDownList
                cmbSINAL = CType(GridView1.Rows(e.RowIndex).FindControl("cmbSINAL"), DropDownList)


                objData = New DbContaCalculo
                If strMensagem = "" Then

                    '  GridView1.DataKeys(e.RowIndex).Values(2).ToString(), 
                    objData.AtualizarRegistro(GridView1.DataKeys(e.RowIndex).Values(0).ToString(), _
                                              GridView1.DataKeys(e.RowIndex).Values(1).ToString(), _
                                              cmbCONTA_CAMPO.SelectedValue, _
                                              cmbSINAL.SelectedValue)
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
                    Dim filename As String = String.Format("Cadastro_Conta_Calculo_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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


        Protected Sub cmbNewCONTA_CAMPO_OnSelectedIndexChanged(sender As Object, e As EventArgs)

            Dim cmbNewCONTA_CAMPO As DropDownList = sender
            Dim currentRow As GridViewRow = cmbNewCONTA_CAMPO.NamingContainer

            Dim cmbNewCONTA_CALCULO As DropDownList
            cmbNewCONTA_CALCULO = CType(currentRow.FindControl("cmbNewCONTA_CALCULO"), DropDownList)


            If (Not IsNothing(cmbNewCONTA_CAMPO) AndAlso cmbNewCONTA_CAMPO.SelectedIndex > 0 AndAlso Not IsNothing(cmbNewCONTA_CALCULO)) Then

                objData = New DbContaCalculo()
                objData.CarregarContaCalculo(cmbNewCONTA_CAMPO.SelectedValue, cmbNewCONTA_CALCULO)

            ElseIf Not IsNothing(cmbNewCONTA_CALCULO) Then

                cmbNewCONTA_CALCULO.Items.Clear()

            End If


            Dim dtData As DataTable = New DbModalidade().CarregarTodosRegistros()

            If dtData.Rows.Count = 0 Then
                Dim totalColumns As Integer
                totalColumns = GridView1.Rows(0).Cells.Count
                GridView1.Rows(0).Cells.Clear()
                GridView1.Rows(0).Cells.Add(New TableCell())
                GridView1.Rows(0).Cells(0).ColumnSpan = totalColumns
                GridView1.Rows(0).Cells(0).Style.Add("text-align", "center")
                GridView1.Rows(0).Cells(0).Text = "Não há registros cadastrado"
            End If

        End Sub
        Protected Sub cmbCONTA_CAMPO_OnSelectedIndexChanged(sender As Object, e As EventArgs)

            Dim cmbCONTA_CAMPO As DropDownList = sender
            Dim currentRow As GridViewRow = cmbCONTA_CAMPO.NamingContainer

            Dim cmbCONTA_CALCULO As DropDownList
            cmbCONTA_CALCULO = CType(currentRow.FindControl("cmbCONTA_CALCULO"), DropDownList)


            If (Not IsNothing(cmbCONTA_CAMPO) AndAlso cmbCONTA_CAMPO.SelectedIndex > 0 AndAlso Not IsNothing(cmbCONTA_CALCULO)) Then

                objData = New DbContaCalculo()
                objData.CarregarContaCalculo(cmbCONTA_CAMPO.SelectedValue, cmbCONTA_CALCULO)

            ElseIf Not IsNothing(cmbCONTA_CALCULO) Then

                cmbCONTA_CALCULO.Items.Clear()

            End If


            Dim dtData As DataTable = New DbModalidade().CarregarTodosRegistros()

            If dtData.Rows.Count = 0 Then
                Dim totalColumns As Integer
                totalColumns = GridView1.Rows(0).Cells.Count
                GridView1.Rows(0).Cells.Clear()
                GridView1.Rows(0).Cells.Add(New TableCell())
                GridView1.Rows(0).Cells(0).ColumnSpan = totalColumns
                GridView1.Rows(0).Cells(0).Style.Add("text-align", "center")
                GridView1.Rows(0).Cells(0).Text = "Não há registros cadastrado"
            End If

        End Sub

        Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Gerencial - Cadastro de Contas Calculadas' ,'Calculos com as contas contábeis para cada linha de relatório.');", True)
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