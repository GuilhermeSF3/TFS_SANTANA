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

Namespace Paginas.Cobranca.ICM

    Public Class ForcarICM

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
                    BindGridViewConsultar()
                    ContextoWeb.DadosTransferencia.CodAnalista = 0
                End If
                If ContextoWeb.DadosTransferencia.CodAnalista <> 0 Then
                    BindGridViewDeletar()
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
                        If IsDBNull(drow("STATUS")) Then
                            e.Row.Cells(0).Text = ""
                        Else
                            e.Row.Cells(0).Text = drow("STATUS")
                        End If

                        If IsDBNull(drow("CONTRATO")) Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = drow("CONTRATO")
                        End If

                        If IsDBNull(drow("PARCELA")) Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = drow("PARCELA")
                        End If

                        If IsDBNull(drow("VLR_PARCELA")) Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = "R$ " & drow("VLR_PARCELA")
                        End If

                        If IsDBNull(drow("DATA_PAGAMENTO")) Then
                            e.Row.Cells(4).Text = ""
                        Else
                            e.Row.Cells(4).Text = drow("DATA_PAGAMENTO")
                        End If

                        If IsDBNull(drow("ATRASO")) Then
                            e.Row.Cells(5).Text = ""
                        Else
                            e.Row.Cells(5).Text = drow("ATRASO")
                        End If
                        If IsDBNull(drow("RATING")) Then
                            e.Row.Cells(6).Text = ""
                        Else
                            e.Row.Cells(6).Text = drow("RATING")
                        End If
                        If IsDBNull(drow("FAIXA")) Then
                            e.Row.Cells(7).Text = ""
                        Else
                            e.Row.Cells(7).Text = drow("FAIXA")
                        End If
                        If IsDBNull(drow("FAIXA_RATING")) Then
                            e.Row.Cells(8).Text = ""
                        Else
                            e.Row.Cells(8).Text = drow("FAIXA_RATING")
                        End If
                        If IsDBNull(drow("NOME_CLIENTE")) Then
                            e.Row.Cells(9).Text = ""
                        Else
                            e.Row.Cells(9).Text = drow("NOME_CLIENTE")
                        End If
                        If IsDBNull(drow("CPF_CNPJ")) Then
                            e.Row.Cells(10).Text = ""
                        Else
                            e.Row.Cells(10).Text = drow("CPF_CNPJ")
                        End If
                        If IsDBNull(drow("CODCOBRADORA")) Then
                            e.Row.Cells(11).Text = ""
                        Else
                            e.Row.Cells(11).Text = drow("CODCOBRADORA")
                        End If
                        If IsDBNull(drow("COBRADORA")) Then
                            e.Row.Cells(12).Text = ""
                        Else
                            e.Row.Cells(12).Text = drow("COBRADORA")
                        End If
                        If IsDBNull(drow("AGENTE")) Then
                            e.Row.Cells(13).Text = ""
                        Else
                            e.Row.Cells(13).Text = drow("AGENTE")
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
                    Dim contract As String = txtContract.Text.Trim()
                    Dim parcel As String = TxtParcel.Text.Trim()
                    Dim codCobr As String = TxtCodCobr.Text.Trim()
                    If DateTime.TryParse(txtData.Text, dataReferencia) Then
                        ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = Inserir(dataReferencia, contract, parcel, codCobr)
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

        Protected Sub BindGridView1DataView()
            Dim contract As String = txtContract.Text.Trim()
            Dim parcel As String = TxtParcel.Text.Trim()
            Dim dataReferencia As DateTime
            Dim dataStr As String = txtData.Text.Trim()
            Dim codCobr As String = TxtCodCobr.Text.Trim()

            Debug.WriteLine("Valor de txtData.Text: " & dataStr)

            If DateTime.TryParseExact(dataStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, dataReferencia) Then

                Debug.WriteLine("Data convertida: " & dataReferencia.ToString("yyyyMMdd"))
                Dim dataFormatada As String = dataReferencia.ToString("yyyyMMdd")
                GridViewRiscoAnalitico.DataSource = Inserir(dataFormatada, parcel, contract, codCobr)
                GridViewRiscoAnalitico.DataBind()
                GridViewRiscoAnalitico.AllowPaging = "True"
                btnExcluir.Visible = True

            Else

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Data inválida!', 'Por favor, forneça uma data válida.');", True)
            End If

        End Sub

        Protected Sub GridViewRiscoAnalitico_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

            GridViewRiscoAnalitico.PageIndex = IIf(e.NewPageIndex < 0, 0, e.NewPageIndex)
            BindGridView1DataView()

        End Sub

        ' Alterar
        Protected Sub btnAlterar_Click(sender As Object, e As EventArgs)
            Try

                BindGridViewAlterar()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try

        End Sub

        Protected Sub BindGridViewAlterar()
            Dim dataReferencia As DateTime
            Dim contract As String = txtContract.Text.Trim()
            Dim parcel As String = txtParcel.Text.Trim()
            Dim codCobr As String = txtCodCobr.Text.Trim()
            Dim dataStr As String = txtData.Text.Trim()

            Debug.WriteLine("Valor de txtData.Text: " & dataStr)

            If DateTime.TryParseExact(dataStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, dataReferencia) Then

                Debug.WriteLine("Data convertida: " & dataReferencia.ToString("yyyyMMdd"))
                Dim dataFormatada As String = dataReferencia.ToString("yyyyMMdd")
                GridViewRiscoAnalitico.DataSource = Alterar(contract, parcel, codCobr)
                GridViewRiscoAnalitico.DataBind()
                GridViewRiscoAnalitico.AllowPaging = "True"
            Else

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Data inválida!', 'Por favor, forneça uma data válida.');", True)
            End If
        End Sub

        Private Function Alterar(contract As String, parcel As String, codCobr As String) As DataTable
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim resultTable As New DataTable()

            Using con As New SqlConnection(strConn)
                Using cmd As New SqlCommand($"SCR_ALTERAR_COBRADORA_ICM_SIG'{contract}','{parcel}','{codCobr}'", con)
                    cmd.CommandType = CommandType.Text
                    con.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        Try
                            resultTable.Load(reader)
                            If resultTable.Rows.Count = 0 Then
                                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Sem Resposta', 'Nenhum arquivo encontrado na data informada.');", True)
                            End If
                        Catch ex As Exception
                            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Erro', 'Ocorreu um erro ao buscar os dados.');", True)
                        End Try
                    End Using
                End Using
            End Using
            Return resultTable
        End Function

        ' End alterar

        ' Processar
        Protected Sub btnProcessar_Click(sender As Object, e As EventArgs)
            Try

                BindGridView1DataProcessar()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try

        End Sub

        Protected Sub BindGridView1DataProcessar()
            Dim dataReferenciaDel As DateTime
            Dim dataStr As String = txtData.Text.Trim()

            If DateTime.TryParseExact(dataStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, dataReferenciaDel) Then

                Dim dataFormatada As String = dataReferenciaDel.ToString("yyyyMMdd")
                GridViewRiscoAnalitico.DataSource = GetDataProcessar(dataFormatada)
                GridViewRiscoAnalitico.DataBind()
                GridViewRiscoAnalitico.AllowPaging = "True"
            Else

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Data inválida!', 'Por favor, forneça uma data válida.');", True)
            End If

        End Sub

        Public Function GetDataProcessar(dataReferencia As String) As DataTable
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim resultTable As New DataTable()
            Dim contract As String = txtContract.Text.Trim()

            Using con As New SqlConnection(strConn)
                Try
                    Using cmd As New SqlCommand($"SCR_PROCESSAR_ICM_SIG_BKP '{dataReferencia}'", con)
                        cmd.CommandType = CommandType.Text
                        con.Open()
                        Using reader As SqlDataReader = cmd.ExecuteReader()
                        End Using
                    End Using
                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", $"Alerta ('Aviso', 'Processado com sucesso!');", True)
                Catch ex As Exception

                End Try

            End Using
            Return resultTable
        End Function

        ' End processar

        ' Deletar
        Protected Sub btnCarregar_ClickDelete(sender As Object, e As EventArgs)
            Try

                BindGridViewDeletar()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try

        End Sub

        Protected Sub BindGridViewDeletar()
            Dim dataReferencia As DateTime
            Dim contract As String = txtContract.Text.Trim()
            Dim parcel As String = txtParcel.Text.Trim()
            Dim codCobr As String = txtCodCobr.Text.Trim()
            Dim dataStr As String = txtData.Text.Trim()

            Debug.WriteLine("Valor de txtData.Text: " & dataStr)

            If DateTime.TryParseExact(dataStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, dataReferencia) Then

                Debug.WriteLine("Data convertida: " & dataReferencia.ToString("yyyyMMdd"))
                Dim dataFormatada As String = dataReferencia.ToString("yyyyMMdd")
                GridViewRiscoAnalitico.DataSource = Deletar(dataFormatada, contract, parcel)
                GridViewRiscoAnalitico.DataBind()
                GridViewRiscoAnalitico.AllowPaging = "True"
            Else

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Data inválida!', 'Por favor, forneça uma data válida.');", True)
            End If
        End Sub

        Private Function Deletar(dataStr As String, contract As String, parcel As String) As DataTable
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim resultTable As New DataTable()

            Using con As New SqlConnection(strConn)
                Using cmd As New SqlCommand($"SCR_REMOVER_ICM_SIG  '{dataStr}','{contract}','{parcel}'", con)
                    cmd.CommandType = CommandType.Text
                    con.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        Try
                            resultTable.Load(reader)
                            If resultTable.Rows.Count = 0 Then
                                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Sem Resposta', 'Nenhum arquivo encontrado na data informada.');", True)
                            End If
                        Catch ex As Exception
                            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Erro', 'Ocorreu um erro ao buscar os dados.');", True)
                        End Try
                    End Using
                End Using
            End Using
            Return resultTable
        End Function

        ' End deletar

        ' Inserir 
        Protected Sub btnInserir_Click(sender As Object, e As EventArgs)
            Try

                BindGridViewInserir()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try

        End Sub

        Protected Sub BindGridViewInserir()
            Dim dataReferencia As DateTime
            Dim contract As String = txtContract.Text.Trim()
            Dim parcel As String = txtParcel.Text.Trim()
            Dim codCobr As String = txtCodCobr.Text.Trim()
            Dim dataStr As String = txtData.Text.Trim()

            Debug.WriteLine("Valor de txtData.Text: " & dataStr)

            If DateTime.TryParseExact(dataStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, dataReferencia) Then

                Debug.WriteLine("Data convertida: " & dataReferencia.ToString("yyyyMMdd"))
                Dim dataFormatada As String = dataReferencia.ToString("yyyyMMdd")
                GridViewRiscoAnalitico.DataSource = Inserir(dataFormatada, contract, parcel, codCobr)
                GridViewRiscoAnalitico.DataBind()
                GridViewRiscoAnalitico.AllowPaging = "True"
            Else

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Data inválida!', 'Por favor, forneça uma data válida.');", True)
            End If
        End Sub

        Private Function Inserir(dataStr As String, contract As String, parcel As String, codCobr As String) As DataTable
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim resultTable As New DataTable()

            Using con As New SqlConnection(strConn)
                Using cmd As New SqlCommand($"SCR_INSERIR_ICM_SIG '{dataStr}','{contract}','{parcel}','{codCobr}'", con)
                    cmd.CommandType = CommandType.Text
                    con.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        Try
                            resultTable.Load(reader)
                            If resultTable.Rows.Count = 0 Then
                                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Sem Resposta', 'Nenhum arquivo encontrado na data informada.');", True)
                            End If
                        Catch ex As Exception
                            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Erro', 'Ocorreu um erro ao buscar os dados.');", True)
                        End Try
                    End Using
                End Using
            End Using
            Return resultTable
        End Function

        ' End inserir

        ' Consultar
        Protected Sub btnConsultar_Click(sender As Object, e As EventArgs)
            Try

                BindGridViewConsultar()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try

        End Sub

        Protected Sub BindGridViewConsultar()
            Dim contract As String = txtContract.Text.Trim()
            Dim parcel As String = txtParcel.Text.Trim()

            ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = Nothing
            GridViewRiscoAnalitico.DataSource = Consultar(contract, parcel)
            GridViewRiscoAnalitico.PageIndex = 0
            GridViewRiscoAnalitico.DataBind()

        End Sub

        Private Function Consultar(contract As String, parcel As String) As DataTable
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim resultTable As New DataTable()

            Using con As New SqlConnection(strConn)
                Using cmd As New SqlCommand($"SCR_CONSULTAR_ICM_SIG '{contract}','{parcel}'", con)
                    cmd.CommandType = CommandType.Text
                    con.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        Try
                            resultTable.Load(reader)
                            If resultTable.Rows.Count = 0 Then
                                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Sem Resposta', 'Nenhum arquivo encontrado na data informada.');", True)
                            End If
                        Catch ex As Exception
                            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Erro', 'Ocorreu um erro ao buscar os dados.');", True)
                        End Try
                    End Using
                End Using
            End Using
            Return resultTable
        End Function

        ' End consultar

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
                GridViewRiscoAnalitico.AllowPaging = False
                ExportExcel(GridViewRiscoAnalitico)

            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        Private Sub ExportExcel(objGrid As GridView)
            Try
                If Not IsNothing(objGrid.HeaderRow) Then
                    Response.Clear()
                    Response.Buffer = True
                    Dim filename As String = String.Format("Cnab-arquivo-baixa-{0}-{1}-{2}.xls", DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year)
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

                        Response.Write("ICM - Forçados </p> ")

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