Imports System
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Santana.Seguranca
Imports System.Data.SqlClient

Namespace Paginas.Cadastro


    Public Class ClienteEntrada
        Inherits SantanaPage

        Dim objData As DbClienteEntrada

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                FillDataInGrid()
               
            End If



            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub
      
        Private Sub Carrega_Clientes()

            Try
                Dim ddlNewcod As DropDownList
                ddlNewcod = CType(GridView1.HeaderRow.FindControl("ddlNewcod"), DropDownList)

                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)

                Dim produto As Integer = ContextoWeb.UsuarioLogado.Produto

                Dim command As SqlCommand = New SqlCommand( _
                    "select cednte_cedn as Code,(convert(varchar(10),cednte_cedn)+' '+cednte_nome ) as CODNOME  from finsgrdbs..tb_cednte (nolock) order by cednte_nome", connection)

                command.Connection.Open()

                Dim ddlValues As SqlDataReader
                ddlValues = command.ExecuteReader()

                ddlNewcod.DataSource = ddlValues
                ddlNewcod.DataValueField = "Code"
                ddlNewcod.DataTextField = "CODNOME"
                ddlNewcod.DataBind()

                ddlNewcod.Items.Insert(0, New ListItem("", "0"))
                ddlNewcod.SelectedIndex = 0

                ddlValues.Close()

                command.Connection.Close()
                command.Connection.Dispose()

                connection.Close()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Protected Sub FillDataInGrid()



            Dim dtData As DataTable = New DbClienteEntrada().CarregarTodosRegistros()
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

                Carrega_Clientes()
            Catch ex As Exception
                Throw New Exception(ex.Message.ToString(), ex)
            End Try

        End Sub
        Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim strMensagem As String = ""

            If e.CommandName.Equals("AddNew") Then


                Dim ddlNewcod As DropDownList
                ddlNewcod = CType(GridView1.HeaderRow.FindControl("ddlNewcod"), DropDownList)

                Dim txtNewDATA_DE As TextBox
                txtNewDATA_DE = CType(GridView1.HeaderRow.FindControl("txtNewDATA_DE"), TextBox)


                If ddlNewcod.SelectedValue().Trim() = "" Or
                    txtNewDATA_DE.Text.Trim() = "0" Then

                    strMensagem = "Por favor, preencher todos os campos."

                Else

                    objData = New DbClienteEntrada
                    Dim tdUser As DataTable = objData.CarregarRegistro(Convert.ToInt32(ddlNewcod.SelectedValue.Trim()))
                    If tdUser.Rows.Count > 0 Then
                        strMensagem = "O registro já esta cadastrado"
                    End If

                   
                    If strMensagem = "" Then
                        objData.InserirRegistro(Convert.ToInt32(ddlNewcod.SelectedValue.Trim()), _
                                                                       Convert.ToDateTime(txtNewDATA_DE.Text.Trim()))
                        FillDataInGrid()
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Atualização' ,'Os dados foram salvos com sucesso.', 'success');", True)
                    End If
                End If

            ElseIf e.CommandName.Equals("Delete") Then
                objData = New DbClienteEntrada
                Dim index As Integer
                index = Convert.ToInt32(e.CommandArgument)


                Dim lblcod As Label
                lblcod = CType(GridView1.Rows(index).FindControl("lblcod"), Label)

                Dim lblDT_DE As Label
                lblDT_DE = CType(GridView1.Rows(index).FindControl("lblDT_DE"), Label)


                objData.ApagarRegistro(Convert.ToInt32(lblcod.Text.Trim()))
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
            CType(GridView1.HeaderRow.FindControl("txtNewDATA_DE"), TextBox).Visible = True
            
            CType(GridView1.HeaderRow.FindControl("lnkAddNew"), LinkButton).Visible = True



        End Sub

        Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs)
            GridView1.EditIndex = e.NewEditIndex
            FillDataInGrid()

            CType(GridView1.HeaderRow.FindControl("ddlNewcod"), DropDownList).Visible = False
            CType(GridView1.HeaderRow.FindControl("txtNewDATA_DE"), TextBox).Visible = False

            CType(GridView1.HeaderRow.FindControl("lnkAddNew"), LinkButton).Visible = False
        End Sub


        Protected Sub GridView1_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)

            Dim strMensagem As String = ""

            If (GridView1.EditIndex <> -1) Then


                Dim lblcod As Label
                lblcod = CType(GridView1.Rows(e.RowIndex).FindControl("lblcod"), Label)

                Dim txtDATA_De As TextBox
                txtDATA_De = CType(GridView1.Rows(e.RowIndex).FindControl("txtDATA_De"), TextBox)




                If txtDATA_De.Text.Trim() = "" Then
                    strMensagem = "Por favor, preencher todos os campos."

                Else
                    objData = New DbClienteEntrada
                    If strMensagem = "" Then

                        Dim DATA_DE As String
                        DATA_DE = txtDATA_De.Text.Trim()




                        objData.AtualizarRegistro(Convert.ToInt32(lblcod.Text.Trim()), _
                                                                       Convert.ToDateTime(DATA_DE.Trim()))
                        GridView1.EditIndex = -1
                        FillDataInGrid()
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Atualização' ,'Os dados foram salvos com sucesso.', 'success');", True)
                    End If

                End If

            End If


            If strMensagem <> "" Then
                FillDataInGrid()

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
                    Dim filename As String = String.Format("ClienteEntrada_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename)
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.ms-excel"

                    Using sw As New StringWriter()

                        Dim hw As New HtmlTextWriter(sw)

                        GridView1.FooterRow.Visible = False
                        GridView1.Columns(2).Visible = False
                        GridView1.Columns(3).Visible = False


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