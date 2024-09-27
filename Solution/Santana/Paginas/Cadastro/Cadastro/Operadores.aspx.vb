Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Santana.Seguranca
Imports Util


Namespace Paginas.Cadastro

    Public Class Operadores

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.ToString("MM/yyyy"))

                Carrega_Relatorio()

                If ContextoWeb.DadosTransferencia.CodAgente = 0 Then
                    Carrega_Agente()
                Else
                    Carrega_Agente()
                    ddlAgente.SelectedIndex = ddlAgente.Items.IndexOf(ddlAgente.Items.FindByValue(ContextoWeb.DadosTransferencia.CodAgente.ToString()))
                End If

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

        Private Sub Carrega_Relatorio()

            Try

                ddlRelatorio.Items.Insert(0, New ListItem("Todos", "1"))
                ddlRelatorio.Items.Insert(1, New ListItem("Ativos", "2"))
                ddlRelatorio.Items.Insert(2, New ListItem("Inativos", "3"))

                ddlRelatorio.SelectedIndex = 0

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

            Try

                If e.Row.RowType = DataControlRowType.Header Then

                ElseIf e.Row.RowType = DataControlRowType.DataRow Then

                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

                        If IsDBNull(drow("CÓDIGO OPERADOR")) Then
                            e.Row.Cells(0).Text = ""
                        Else
                            e.Row.Cells(0).Text = drow("CÓDIGO OPERADOR")
                        End If

                        If IsDBNull(drow("NOME OPERADOR")) Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = drow("NOME OPERADOR")
                        End If

                        If IsDBNull(drow("CÓDIGO PROMOTORA")) Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = drow("CÓDIGO PROMOTORA")
                        End If

                        If IsDBNull(drow("NOME PROMOTORA")) Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = drow("NOME PROMOTORA")
                        End If

                        If IsDBNull(drow("ATIVO")) Then
                            e.Row.Cells(4).Text = ""
                        Else
                            e.Row.Cells(4).Text = drow("ATIVO")
                        End If

                        If IsDBNull(drow("USNOMEUSU")) Then
                            e.Row.Cells(5).Text = ""
                        Else
                            e.Row.Cells(5).Text = drow("USNOMEUSU")
                        End If

                        If IsDBNull(drow("DEPARA")) Then
                            e.Row.Cells(6).Text = ""
                        Else
                            e.Row.Cells(6).Text = drow("DEPARA")
                        End If

                        If IsDBNull(drow("NOME COMPLETO")) Then
                            e.Row.Cells(7).Text = ""
                        Else
                            e.Row.Cells(7).Text = drow("NOME COMPLETO")
                        End If

                        If IsDBNull(drow("CPF")) Then
                            e.Row.Cells(8).Text = ""
                        Else
                            e.Row.Cells(8).Text = drow("CPF")
                        End If

                        If IsDBNull(drow("EMAIL")) Then
                            e.Row.Cells(9).Text = ""
                        Else
                            e.Row.Cells(9).Text = drow("EMAIL")
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

            ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = Nothing
            Dim data As DataTable = GetData()
            GridView1.DataSource = data
            txtQtde.Text = data.Rows.Count
            GridView1.PageIndex = 0
            GridView1.DataBind()
            GridView1.AllowPaging = "True"

        End Sub

        Protected Sub BindGridView1DataView()

            GridView1.DataSource = DataGridView
            GridView1.DataBind()

        End Sub


        Private Function GetData() As DataTable


            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim table As DataTable = Util.ClassBD.GetExibirGrid("[SCR_OPERADORES] '" & ddlRelatorio.SelectedValue & "', '" & IIf(ddlAgente.SelectedValue <> "99", ddlAgente.SelectedValue.PadLeft(6, "0"), ddlAgente.SelectedValue) & "'", "Operadores", strConn)

            Return table

        End Function




        Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

            GridView1.PageIndex = IIf(e.NewPageIndex < 0, 0, e.NewPageIndex)
            BindGridView1DataView()
        End Sub




        Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Desconto Produção' ,'Desconto Produção", True)
        End Sub


        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../../Menu.aspx")
        End Sub

        Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
            Try
                Session.Timeout = 2
                BindGridView1Data()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try

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
                GridView1.AllowPaging = False
                BindGridView1Data()
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
                    Dim filename As String = String.Format("Operadores_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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


                        Select Case ddlRelatorio.SelectedValue
                            Case 1
                                Response.Write(String.Format("RELAÇÃO GERAL DE OPERADORES"))
                            Case 2
                                Response.Write(String.Format("RELAÇÃO DE OPERADORES ATIVOS"))
                            Case 3
                                Response.Write(String.Format("RELAÇÃO DE OPERADORES INATIVOS"))
                        End Select
                        Response.Write("<p>")


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

        Private Sub Carrega_Agente()

            Try

                Dim objDataAgente = New DbAgente
                Dim codGerente As String

                If ContextoWeb.UsuarioLogado.Perfil = 8 Then
                    codGerente = ContextoWeb.UsuarioLogado.codGerente
                Else
                    codGerente = "99"
                End If

                If codGerente = "99" Then

                    Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                    Dim connection As New SqlConnection(strConn)
                    Dim command As SqlCommand = New SqlCommand(
                    "SELECT DISTINCT O3CODORG, O3DESCR AS O3NOME FROM CDCSANTANAMICROCREDITO..TORG3 (NOLOCK) ORDER BY O3DESCR", connection)

                    command.Connection.Open()

                    Dim ddlValues As SqlDataReader
                    ddlValues = command.ExecuteReader()

                    ddlAgente.DataSource = ddlValues
                    ddlAgente.DataValueField = "O3CODORG"
                    ddlAgente.DataTextField = "O3NOME"
                    ddlAgente.DataBind()

                    ddlAgente.Items.Insert(0, New ListItem("Todos", "99"))
                    ddlAgente.SelectedIndex = 0

                    ddlValues.Close()
                    command.Connection.Close()
                    command.Connection.Dispose()
                    connection.Close()

                Else
                    Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                    Dim con As New SqlConnection(strConn)
                    Dim Vagente As String = ""

                    Dim cmd As New SqlCommand("Select O3DESCR, O3CODORG from CDCSANTANAMicroCredito..TORG3 (nolock) where O3codorg IN (" & codGerente & ")", con)

                    cmd.Connection.Open()

                    Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

                    While dr.Read()
                        Vagente = Trim(dr.GetString(0))
                        Dim AGENTE1 = New ListItem
                        AGENTE1.Value = Trim(dr.GetString(1))
                        AGENTE1.Text = Trim(Vagente)
                        ddlAgente.Items.Add(AGENTE1)
                    End While
                    dr.Close()
                    con.Close()

                End If


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

    End Class

End Namespace