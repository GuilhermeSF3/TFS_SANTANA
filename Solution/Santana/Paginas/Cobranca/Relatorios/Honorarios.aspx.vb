Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic
Imports Santana.Seguranca
Imports Util

Namespace Paginas.Cobranca.Relatorios

    Public Class Honorarios

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then
                Dim today As DateTime = DateTime.Now
                Dim previousDate As DateTime

                txtData.Text = Now.ToString("dd/MM/yyyy")



                If Session(HfGridView1Svid) IsNot Nothing Then
                    hfGridView1SV.Value = DirectCast(Session(HfGridView1Svid), String)
                    Session.Remove(HfGridView1Svid)
                End If

                If Session(HfGridView1Shid) IsNot Nothing Then
                    hfGridView1SH.Value = DirectCast(Session(HfGridView1Shid), String)
                    Session.Remove(HfGridView1Shid)
                End If

                If ContextoWeb.DadosTransferencia.CodAnalista <> 0 Then
                    BindGridView1Data()
                    ContextoWeb.DadosTransferencia.CodAnalista = 0
                End If

            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub










        Private Sub GridViewRiscoAnalitico_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewRiscoAnalitico.RowDataBound

            Try

                If e.Row.RowType = DataControlRowType.DataRow Then

                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

                        If IsDBNull(drow("PCNRCESS")) Then
                            e.Row.Cells(0).Text = ""
                        Else
                            e.Row.Cells(0).Text = drow("PCNRCESS")
                        End If
                        If IsDBNull(drow("EFNROPER")) Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = drow("EFNROPER")
                        End If
                        If IsDBNull(drow("EFNRPARC")) Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = drow("EFNRPARC")
                        End If

                        If IsDBNull(drow("EFDTMOV")) Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = drow("EFDTMOV")
                        End If

                        If IsDBNull(drow("VLR_PAGO")) Then
                            e.Row.Cells(4).Text = ""
                        Else
                            Dim totalValor As Decimal = Convert.ToDecimal(drow("VLR_PAGO"))
                            e.Row.Cells(4).Text = "R$ " & totalValor.ToString("N2")
                        End If

                        If IsDBNull(drow("EFCODMOV")) Then
                            e.Row.Cells(5).Text = ""
                        Else
                            e.Row.Cells(5).Text = drow("EFCODMOV")
                        End If
                        If IsDBNull(drow("PASEUNRO")) Then
                            e.Row.Cells(6).Text = ""
                        Else
                            e.Row.Cells(6).Text = drow("PASEUNRO")
                        End If
                        If IsDBNull(drow("RENROPERA")) Then
                            e.Row.Cells(7).Text = ""
                        Else
                            e.Row.Cells(7).Text = drow("RENROPERA")
                        End If
                    End If
                End If


            Catch ex As Exception

            End Try
        End Sub


        Protected Sub GridViewRiscoAnalitico_RowCreated(sender As Object, e As GridViewRowEventArgs)
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

                    Dim dataReferencia As DateTime
                    If DateTime.TryParse(txtData.Text, dataReferencia) Then
                        ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = GetData(dataReferencia)
                    Else
                        Throw New Exception("Data inválida no ViewState")
                    End If
                End If

                Return DirectCast(ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C"), DataTable)
            End Get
            Set(value As DataTable)
                ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = value
            End Set
        End Property



        Protected Sub BindGridView1Data()
            Dim dataReferencia As DateTime
            Dim dataStr As String = txtData.Text.Trim()

            Debug.WriteLine("Valor de txtData.Text: " & dataStr)

            If DateTime.TryParseExact(dataStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, dataReferencia) Then

                Debug.WriteLine("Data convertida: " & dataReferencia.ToString("yyyyMMdd"))
                Dim dataFormatada As String = dataReferencia.ToString("yyyyMMdd")
                GridViewRiscoAnalitico.DataSource = GetData(dataFormatada)
                GridViewRiscoAnalitico.DataBind()
                GridViewRiscoAnalitico.AllowPaging = "True"
            Else

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Data inválida!', 'Por favor, forneça uma data válida.');", True)
            End If
        End Sub


        Protected Sub BindGridView1DataView()

            Dim dataReferencia As DateTime
            Dim dataStr As String = txtData.Text.Trim()

            Debug.WriteLine("Valor de txtData.Text: " & dataStr)

            If DateTime.TryParseExact(dataStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, dataReferencia) Then

                Debug.WriteLine("Data convertida: " & dataReferencia.ToString("yyyyMMdd"))
                Dim dataFormatada As String = dataReferencia.ToString("yyyyMMdd")
                GridViewRiscoAnalitico.DataSource = GetData(dataFormatada)
                GridViewRiscoAnalitico.DataBind()
                GridViewRiscoAnalitico.AllowPaging = "True"
            Else

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Data inválida!', 'Por favor, forneça uma data válida.');", True)
            End If

        End Sub




        'Private Function GetData() As DataTable

        '    Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        '    Dim table As DataTable

        '    table = Util.ClassBD.GetExibirGrid("[SCR_CP_COLAB_AGENTES]", "SCR_CP_COLAB_AGENTES", strConn)

        '    Return table

        'End Function



        'Private Function GetData(dataReferencia As String) As DataTable
        '    Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        '    Dim table As New DataTable()


        '    Using con As New SqlConnection(strConn)
        '        Using cmd As New SqlCommand($"EXEC SCR_CNAB550_CARGA '{dataReferencia}'", con)
        '            cmd.CommandType = CommandType.Text

        '            Using sda As New SqlDataAdapter(cmd)
        '                Try
        '                    sda.Fill(table)
        '                    If table.Rows.Count = 0 Then

        '                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Sem Resposta' ,'Nenhum arquivo encontrado na data informada.');", True)
        '                    End If
        '                Catch ex As Exception

        '                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Sem Resposta' ,'Nenhum arquivo encontrado na data informada.');", True)
        '                End Try
        '            End Using
        '        End Using
        '    End Using

        '    Return table
        'End Function


        Private Function GetData(dataReferencia As String) As DataTable
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim resultTable As New DataTable()
            Dim usuarioLogado As String = ContextoWeb.UsuarioLogado.Login

            Using con As New SqlConnection(strConn)
                Using cmd As New SqlCommand($"EXEC SCR_HONORARIOS '{dataReferencia}'", con)
                    cmd.CommandType = CommandType.Text
                    con.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        Try
                            resultTable.Load(reader)
                            If resultTable.Rows.Count = 0 Then

                            Else

                            End If
                        Catch ex As Exception
                            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Erro', 'Ocorreu um erro ao buscar os dados.');", True)
                        End Try
                    End Using
                End Using
            End Using
            Return resultTable
        End Function





        Protected Sub GridViewRiscoAnalitico_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

            GridViewRiscoAnalitico.PageIndex = IIf(e.NewPageIndex < 0, 0, e.NewPageIndex)
            BindGridView1DataView()
        End Sub




        Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)


            'ContextoWeb.DadosTransferencia.CodAgente = ddlAnalista.SelectedValue
            'ContextoWeb.DadosTransferencia.Agente = ddlAnalista.SelectedItem.ToString()

            'ContextoWeb.DadosTransferencia.CodCobradora = ddlProduto.SelectedValue
            'ContextoWeb.DadosTransferencia.Cobradora = ddlProduto.SelectedItem.ToString()


            'Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

            'Dim ds As dsRollrateMensal
            'Dim cmd As New SqlCommand("[scr_RR_mensal] '" & Convert.ToDateTime(txtData.Text).ToString("MM/dd/yy") & "', '" & ddlAnalista.SelectedValue & "','" & ddlProduto.SelectedValue & "'")
            'Using con As New SqlConnection(strConn)
            '    Using sda As New SqlDataAdapter()
            '        cmd.Connection = con
            '        sda.SelectCommand = cmd
            '        ds = New dsRollrateMensal()
            '        sda.Fill(ds, "RR_ROLLRATE_RPT")
            '    End Using
            'End Using

            '' ContextoWeb.NewReportContext()
            'ContextoWeb.Relatorio.reportFileName = "~/Relatorios/rptRollrateMensal.rpt"
            'ContextoWeb.Relatorio.reportDatas.Add(New reportData(ds))

            'ContextoWeb.Navegacao.LinkPaginaAnteriorRelatorio = Me.AppRelativeVirtualPath
            '' ContextoWeb.Navegacao.TituloPaginaAtual = Me.Title
            'Response.Redirect("Relatorio.aspx")

            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("../Menu.aspx")
        End Sub

        Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
            Try

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
                GridViewRiscoAnalitico.AllowPaging = False
                BindGridView1Data()
                ExportExcel(GridViewRiscoAnalitico)

            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        Private Sub ExportExcel(objGrid As GridView)
            Dim dataStr As String = txtData.Text.Trim()
            Try
                If Not IsNothing(objGrid.HeaderRow) Then
                    Response.Clear()
                    Response.Buffer = True
                    Dim filename As String = String.Format($"Honorários.xls", DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year)
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
                            For i As Integer = 0 To row.Cells.Count - 1
                                Dim cell As TableCell = row.Cells(i)

                                If i = 0 Then
                                    cell.Text = FormatValueForFirstColumn(cell.Text)
                                End If

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

                        Response.Write("Relatório de honorários </p> ")

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

        Private Function FormatValueForFirstColumn(value As String) As String
            Dim number As Decimal
            If Decimal.TryParse(value, number) Then
                Return number.ToString("F2")
            End If
            Return value
        End Function


        Protected Sub hfGridView1SV_ValueChanged(sender As Object, e As EventArgs)
            Session(HfGridView1Svid) = hfGridView1SV.Value
        End Sub

        Protected Sub hfGridView1SH_ValueChanged(sender As Object, e As EventArgs)
            Session(HfGridView1Shid) = hfGridView1SH.Value
        End Sub

        Protected Sub GridViewRiscoAnalitico_DataBound(sender As Object, e As EventArgs)
            Dim gridView As GridView = CType(sender, GridView)
            If Not IsNothing(gridView.DataSource) AndAlso CType(gridView.DataSource, System.Data.DataTable).Rows.Count > 0 Then
                Util.CWebPageUtil.PreencheListaDePaginas(gridView, CType(gridView.BottomPagerRow.Cells(0).FindControl("PagerPages"), DropDownList))
            End If
        End Sub

        Protected Sub PagerPages_SelectedIndexChanged(sender As Object, e As EventArgs)

            GridViewRiscoAnalitico.DataSource = DataGridView()
            GridViewRiscoAnalitico.PageIndex = CType(sender, DropDownList).SelectedIndex
            GridViewRiscoAnalitico.DataBind()

        End Sub

    End Class
End Namespace