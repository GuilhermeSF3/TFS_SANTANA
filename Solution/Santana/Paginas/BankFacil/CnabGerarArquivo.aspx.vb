Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic
Imports Santana.Seguranca
Imports Util

Namespace Paginas

    Public Class CnabGerarArquivo

        Inherits SantanaPage

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub


        Protected Sub btnCessaoAnterior1_Click(sender As Object, e As EventArgs)

        End Sub


        Protected Sub btnProximaCessao_Click(sender As Object, e As EventArgs)

        End Sub

        Private Sub GridViewCessao_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewCessao.RowDataBound

            Try

                If e.Row.RowType = DataControlRowType.DataRow Then
                    If Not e.Row.DataItem Is Nothing Then
                        Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

                        e.Row.Cells(0).Text = drow("CESSAO")
                        e.Row.Cells(1).Text = Convert.ToDecimal(drow("VENDA")).ToString("N")

                    End If
                End If

            Catch ex As Exception

            End Try
        End Sub

        Protected Sub GridViewCessao_RowCreated(sender As Object, e As GridViewRowEventArgs)
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

        Protected Sub BindGridView1Data()

            GridViewCessao.DataSource = GetData()
            GridViewCessao.DataBind()
            GridViewCessao.AllowPaging = "True"
        End Sub

        Protected Sub BindGridView1DataView()

            GridViewCessao.DataSource = DataGridView
            GridViewCessao.DataBind()

        End Sub

        Private Function GetData() As DataTable

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim table As DataTable

            table = Util.ClassBD.GetExibirGrid("[SCR_CNAB_GERARARQUIVO] '" & txtCessao.Text & "'", "CNABGERARARQUIVO", strConn)

            Return table

        End Function


        Protected Sub GridViewCessao_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
            GridViewCessao.PageIndex = IIf(e.NewPageIndex < 0, 0, e.NewPageIndex)
            BindGridView1DataView()
        End Sub

        Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub

        Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub

        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../Menu.aspx")
        End Sub

        Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
            Dim strMensagem As String = ""

            If txtCessao.Text.Trim() = "" Then
                strMensagem = "Por favor, preencher id da cessão."
            Else

                Try
                    BindGridView1Data()
                Catch ex As Exception

                Finally
                    GC.Collect()
                End Try

            End If

            If strMensagem <> "" Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Existe Algo Errado!' ,'" + strMensagem + "', 'danger');", True)
            End If

        End Sub
        Protected Sub btnCarregarProd_Click(sender As Object, e As EventArgs)
            Try

                BindGridView1Data()

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
                ExportExcel(GridViewCessao, txtCessao.Text)

            Catch ex As Exception
                Throw ex
            End Try

        End Sub


        Private Sub ExportExcel(objGrid As GridView, idcessao As String)


            Try

                If Not IsNothing(objGrid.HeaderRow) Then
                    GeraCnab(idcessao)
                End If

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Protected Sub GridViewCessao_DataBound(sender As Object, e As EventArgs)
            Dim gridView As GridView = CType(sender, GridView)
            If Not IsNothing(gridView.DataSource) AndAlso CType(gridView.DataSource, System.Data.DataTable).Rows.Count > 0 Then
                Util.CWebPageUtil.PreencheListaDePaginas(gridView, CType(gridView.BottomPagerRow.Cells(0).FindControl("PagerPages"), DropDownList))
            End If
        End Sub

        Protected Sub PagerPages_SelectedIndexChanged(sender As Object, e As EventArgs)

            GridViewCessao.DataSource = DataGridView()
            GridViewCessao.PageIndex = CType(sender, DropDownList).SelectedIndex
            GridViewCessao.DataBind()

        End Sub

        Private Sub GeraCnab(idcessao As String)
            Response.Clear()
            Response.Buffer = True
            Dim filename As String = String.Format("CNAB_{0}_{1}_{2}.txt", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
            Response.AddHeader("content-disposition", "attachment;filename=" + filename)
            Response.ContentEncoding = System.Text.Encoding.Default
            Response.Charset = ""
            Response.ContentType = "application/octet-stream"

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim connection As New SqlConnection(strConn)
            Dim comm As String
            comm = "EXEC SCR_CNAB_CESSAO '" + idcessao + "'"
            Dim command As SqlCommand = New SqlCommand(comm, connection)

            command.Connection.Open()

            Dim ddlValues As SqlDataReader
            ddlValues = command.ExecuteReader()

            If ddlValues.HasRows Then
                Dim i As Integer = 1
                Using sw As New StringWriter()
                    While ddlValues.Read
                        Dim n As String
                        If Not IsDBNull(ddlValues.Item(0)) Then
                            n = i.ToString()
                            n = n.PadLeft(6, "0")
                            sw.WriteLine(ddlValues.Item(0) + n)
                            i = i + 1
                        End If
                    End While
                    Response.Output.Write(sw.ToString())
                    HttpContext.Current.Response.Flush()
                    HttpContext.Current.Response.SuppressContent = True
                    HttpContext.Current.ApplicationInstance.CompleteRequest()
                End Using
            End If
        End Sub
    End Class
End Namespace