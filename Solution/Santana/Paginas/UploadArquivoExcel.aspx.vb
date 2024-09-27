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
Imports System.Data.OleDb
Imports System.Net
Imports System.Net.Mail
Imports System.Text.StringBuilder

Namespace Paginas
    Public Class UploadArquivoExcel

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                If Session(HfGridView1Svid) IsNot Nothing Then
                    hfGridView1SV.Value = DirectCast(Session(HfGridView1Svid), String)
                    Session.Remove(HfGridView1Svid)
                End If

                If Session(HfGridView1Shid) IsNot Nothing Then
                    hfGridView1SH.Value = DirectCast(Session(HfGridView1Shid), String)
                    Session.Remove(HfGridView1Shid)
                End If
            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)


        End Sub

        Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
            If FileUpload1.HasFile Then


                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)
                Dim command As New SqlCommand()

                Try
                    Dim strBatche As New StringBuilder()
                    Dim arquivo As String = "D:\SIG\PLD\" + FileUpload1.PostedFile.FileName
                    'Dim arquivo As String = "C:\Eguardian\" + FileUpload1.PostedFile.FileName
                    Label1.Text = "File name: " & FileUpload1.PostedFile.FileName


                    If File.Exists(arquivo) = True Then
                        'arquivo = arquivo.Replace("xlsx", "xls")
                        Dim tabela As String = "AUX_TB_COLABORADORES"
                        Dim queryexcel As String = "SELECT * FROM [Sheet0$]"
                        'Dim excelCon As String = (Convert.ToString("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & arquivo & ";Extended Properties=\Excel 12.0 Xml;HDR=YES;IMEX=1"))
                        Dim excelCon As String = (Convert.ToString("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=") & arquivo) + ";Extended Properties=" + """Excel 8.0;"""
                        Dim clearsql As String = Convert.ToString("delete from ") & tabela
                        Dim sqlcmd As New SqlCommand(clearsql, connection)
                        connection.Open()
                        sqlcmd.ExecuteNonQuery()
                        connection.Close()

                        Dim oleconn As New OleDbConnection(excelCon)
                        Dim oledbcmd As New OleDbCommand(queryexcel, oleconn)
                        oleconn.Open()
                        Dim dr As OleDbDataReader = oledbcmd.ExecuteReader()
                        Dim bulkcopy As New SqlBulkCopy(connection)
                        bulkcopy.DestinationTableName = tabela
                        connection.Open()
                        Try
                            bulkcopy.WriteToServer(dr)
                        Catch ex As Exception
                            Console.WriteLine(ex.Message)
                        End Try
                        dr.Close()
                        oleconn.Close()
                        connection.Close()
                    Else
                        Label1.Text = "Ocorreu erro ao carregar arquivo " + arquivo
                    End If

                    'BindGridView1DataView()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Carregado!' ,'Arquivo carregado.');", True)
                Catch ex As Exception

                    If Not IsNothing(command) AndAlso Not IsNothing(command.Connection) AndAlso command.Connection.State = ConnectionState.Open Then
                        command.Connection.Close()
                    End If
                    Label1.Text = "ERROR: " & ex.Message.ToString()
                End Try

            Else
                Label1.Text = "Necessário selecionar um arquivo."
            End If

        End Sub


        Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

            Try

                If e.Row.RowType = DataControlRowType.DataRow Then
                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row


                        If IsDBNull(drow("MATRICULA")) Then
                            e.Row.Cells(0).Text = ""
                        Else
                            e.Row.Cells(0).Text = drow("MATRICULA")
                        End If

                        If IsDBNull(drow("CODIGO")) Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = drow("CODIGO")
                        End If

                        If IsDBNull(drow("NOME_FUNC")) Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = drow("NOME_FUNC")
                        End If

                        If IsDBNull(drow("EMPRESA")) Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = drow("EMPRESA")
                        End If

                        If IsDBNull(drow("LOGRADOURO")) Then
                            e.Row.Cells(4).Text = ""
                        Else
                            e.Row.Cells(4).Text = drow("LOGRADOURO")
                        End If

                        If IsDBNull(drow("NUMERO")) Then
                            e.Row.Cells(5).Text = ""
                        Else
                            e.Row.Cells(5).Text = drow("NUMERO")
                        End If

                        If IsDBNull(drow("CIDADE")) Then
                            e.Row.Cells(6).Text = ""
                        Else
                            e.Row.Cells(6).Text = drow("CIDADE")
                        End If

                        If IsDBNull(drow("UF")) Then
                            e.Row.Cells(7).Text = ""
                        Else
                            e.Row.Cells(7).Text = drow("UF")
                        End If

                        If IsDBNull(drow("BAIRRO")) Then
                            e.Row.Cells(8).Text = ""
                        Else
                            e.Row.Cells(8).Text = drow("BAIRRO")
                        End If

                        If IsDBNull(drow("CEP")) Then
                            e.Row.Cells(9).Text = ""
                        Else
                            e.Row.Cells(9).Text = drow("CEP")
                        End If

                        If IsDBNull(drow("ADMISSAO")) Then
                            e.Row.Cells(10).Text = ""
                        Else
                            e.Row.Cells(10).Text = drow("ADMISSAO")
                        End If

                        If IsDBNull(drow("DEMISSAO")) Then
                            e.Row.Cells(11).Text = ""
                        Else
                            e.Row.Cells(11).Text = drow("DEMISSAO")
                        End If

                        If IsDBNull(drow("DT_REF")) Then
                            e.Row.Cells(12).Text = ""
                        Else
                            e.Row.Cells(12).Text = drow("DT_REF")
                        End If

                    End If
                End If


            Catch ex As Exception

            End Try
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


        Public Property DataGridView As DataTable
            Get

                'If ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") Is Nothing Then
                '    ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = GetData()
                'End If

                Return DirectCast(ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C"), DataTable)
            End Get
            Set(value As DataTable)
                ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = value
            End Set
        End Property



        Protected Sub BindGridView1Data()

            GridView1.DataBind()
            GridView1.AllowPaging = "True"
        End Sub

        Protected Sub BindGridView1Data1()

            GridView1.DataBind()
            GridView1.AllowPaging = "True"
        End Sub

        Protected Sub BindGridView1DataView()

            ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = Nothing


        End Sub


        Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

            GridView1.PageIndex = IIf(e.NewPageIndex < 0, 0, e.NewPageIndex)
            BindGridView1DataView()
        End Sub




        Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../../Menu.aspx")
        End Sub


        Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
            Try

                BindGridView1Data1()

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
                GridView1.AllowPaging = False
                BindGridView1Data1()
                ExportExcel(GridView1)

            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        Private Sub ExportExcel(objGrid As GridView)


            Try

                If Not IsNothing(objGrid.HeaderRow) Then

                    Response.Clear()
                    Response.Buffer = True
                    Dim filename As String = String.Format("Colaboradores_PLD_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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
                        Response.Write("Colaboradores - PLD")
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


        Protected Sub hfGridView1SV_ValueChanged(sender As Object, e As EventArgs)
            Session(HfGridView1Svid) = hfGridView1SV.Value
        End Sub

        Protected Sub hfGridView1SH_ValueChanged(sender As Object, e As EventArgs)
            Session(HfGridView1Shid) = hfGridView1SH.Value
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


        Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

        End Sub



        Private Function formataData(Data As String) As String
            Dim retorno As String

            retorno = String.Concat("'", Right(Data, 4), Mid(Data, 3, 2), Left(Data, 2), "'")

            If retorno = "'00000000'" Then
                retorno = "'19000101'"
            End If
            Return retorno
        End Function

        Private Function formataTexto(Texto As String) As String
            Dim retorno As String
            retorno = "'" + Texto.Replace("'", "''").Trim() + "'"
            Return retorno
        End Function

        Private Function formataValor(Valor As String, Decimais As Integer) As String
            Dim retorno As String

            If Decimais = 0 Then
                retorno = Valor
            Else
                retorno = Valor '/100.0 - Decimais) + "." + Right(Valor, Decimais)
            End If

            If retorno.Trim() = "" Then
                retorno = "0"
            End If

            Return retorno
        End Function

    End Class
End Namespace