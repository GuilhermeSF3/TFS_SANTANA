Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic
Imports Santana.Seguranca
Imports Util

Namespace Paginas.TI


    Public Class Agendador

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


            If Not IsPostBack Then
                Dim today As DateTime = DateTime.Now
                Dim previousDate As DateTime



                'If Session(HfGridView1Svid) IsNot Nothing Then
                '    hfGridView1SV.Value = DirectCast(Session(HfGridView1Svid), String)
                '    Session.Remove(HfGridView1Svid)
                'End If

                'If Session(HfGridView1Shid) IsNot Nothing Then
                '    hfGridView1SH.Value = DirectCast(Session(HfGridView1Shid), String)
                '    Session.Remove(HfGridView1Shid)
                'End If

                'If ContextoWeb.DadosTransferencia.CodAnalista <> 0 Then
                '    BindGridViewData()
                '    ContextoWeb.DadosTransferencia.CodAnalista = 0
                'End If

            End If

            Dim script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub

        'Private Sub GridViewRiscoAnalitico_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewRiscoAnalitico.RowDataBound

        '    Try
        '        If e.Row.RowType = DataControlRowType.DataRow Then

        '            If Not e.Row.DataItem Is Nothing Then

        '                Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

        '                If IsDBNull(drow("DATA_PAGAMENTO")) Then
        '                    e.Row.Cells(0).Text = ""
        '                Else
        '                    e.Row.Cells(0).Text = drow("DATA_PAGAMENTO")
        '                End If
        '                If IsDBNull(drow("DESCRICAO")) Then
        '                    e.Row.Cells(1).Text = ""
        '                Else
        '                    e.Row.Cells(1).Text = drow("DESCRICAO")
        '                End If
        '                If IsDBNull(drow("VALOR_BRUTO")) Then
        '                    e.Row.Cells(2).Text = ""
        '                Else
        '                    Dim totalValor1 As Decimal = Convert.ToDecimal(drow("VALOR_BRUTO"))
        '                    e.Row.Cells(2).Text = "R$ " & totalValor1.ToString("N2")
        '                End If

        '                If IsDBNull(drow("VALOR_LIQUIDO")) Then
        '                    e.Row.Cells(3).Text = ""
        '                Else
        '                    Dim totalValor As Decimal = Convert.ToDecimal(drow("VALOR_LIQUIDO"))
        '                    e.Row.Cells(3).Text = "R$ " & totalValor.ToString("N2")
        '                End If

        '                If IsDBNull(drow("FAVORECIDO")) Then
        '                    e.Row.Cells(4).Text = ""
        '                Else
        '                    e.Row.Cells(4).Text = drow("FAVORECIDO")
        '                End If

        '                If IsDBNull(drow("CPF_CNPJ")) Then
        '                    e.Row.Cells(5).Text = ""
        '                Else
        '                    e.Row.Cells(5).Text = drow("CPF_CNPJ")
        '                End If
        '                If IsDBNull(drow("CPF_CNPJ")) Then
        '                    e.Row.Cells(6).Text = ""
        '                Else
        '                    e.Row.Cells(6).Text = drow("CPF_CNPJ")
        '                End If
        '                If IsDBNull(drow("CPF_CNPJ")) Then
        '                    e.Row.Cells(7).Text = ""
        '                Else
        '                    e.Row.Cells(7).Text = drow("CPF_CNPJ")
        '                End If
        '                If IsDBNull(drow("CPF_CNPJ")) Then
        '                    e.Row.Cells(8).Text = ""
        '                Else
        '                    e.Row.Cells(8).Text = drow("CPF_CNPJ")
        '                End If
        '                If IsDBNull(drow("CONTA_CORRENTE")) Then
        '                    e.Row.Cells(9).Text = ""
        '                Else
        '                    e.Row.Cells(9).Text = drow("CPF_CNPJ")
        '                End If
        '            End If
        '        End If


        '    Catch ex As Exception

        '    End Try
        'End Sub



        Protected Sub btnSalvar_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Using conn As New SqlConnection(strConn)
                Dim sql As String = "INSERT INTO TB_AGENDAMENTO_SIG (Historico, DATA_PAGAMENTO, DESCRICAO, VALOR_BRUTO , VALOR_LIQUIDO, FAVORECIDO, CPF_CNPJ, FORMA_DE_PAGAMENTO, BANCO, AGENCIA, CONTA_CORRENTE)" & "VALUES (@Historico, @DataPagamento, @Descricao, @ValorBruto, @ValorLiquido, @Favorecido, @CpfCnpj, @FormaPagamento, @Banco, @Agencia, @ContaCorrente)"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@Historico", ddlHistorico.SelectedItem.Text)
                    cmd.Parameters.AddWithValue("@DataPagamento", txtDataPagamento.Text)
                    cmd.Parameters.AddWithValue("@Descricao", txtDescricao.Text)
                    cmd.Parameters.AddWithValue("@ValorBruto", txtValorBruto.Text)
                    cmd.Parameters.AddWithValue("@ValorLiquido", txtValorLiquido.Text)
                    cmd.Parameters.AddWithValue("@Favorecido", txtFavorecido.Text)
                    cmd.Parameters.AddWithValue("@CpfCnpj", txtCpfCnpj.Text)
                    cmd.Parameters.AddWithValue("@FormaPagamento", ddlFormaPagamento.SelectedItem.Text)
                    cmd.Parameters.AddWithValue("@Banco", txtBanco.Text)
                    cmd.Parameters.AddWithValue("@Agencia", txtAgencia.Text)
                    cmd.Parameters.AddWithValue("@ContaCorrente", txtContaCorrente.Text)

                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Data inválida!', 'Inserido com Sucesso');", True)


                    Catch ex As Exception


                    Finally
                        conn.Close()
                    End Try

                End Using
            End Using
        End Sub




        Protected Sub ddlHistorico_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            If ddlHistorico.SelectedIndex > 0 Then
                Dim historicoId As String = ddlHistorico.SelectedItem.Value
                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Using conn As New SqlConnection(strConn)
                    Dim sql As String = "SELECT * FROM TB_HISTORICOS WHERE NOME_HISTORICO= @Historico"

                    Using cmd As New SqlCommand(sql, conn)
                        cmd.Parameters.AddWithValue("@Historico", historicoId)

                        Try
                            conn.Open()
                            Dim reader As SqlDataReader = cmd.ExecuteReader()
                            If reader.HasRows Then
                                reader.Read()
                                txtDataPagamento.Text = reader("DATA_PAGAMENTO").ToString()
                                txtDescricao.Text = reader("DESCRICAO").ToString()
                                txtValorBruto.Text = reader("VALOR_BRUTO").ToString()
                                txtValorLiquido.Text = reader("VALOR_LIQUIDO").ToString()
                                txtFavorecido.Text = reader("FAVORECIDO").ToString()
                                txtCpfCnpj.Text = reader("CPF_CNPJ").ToString()
                                ddlFormaPagamento.SelectedValue = reader("FORMA_DE_PAGAMENTO").ToString()
                                txtBanco.Text = reader("BANCO").ToString()
                                txtAgencia.Text = reader("AGENCIA").ToString()
                                txtContaCorrente.Text = reader("CONTA_CORRENTE").ToString()
                            End If
                        Catch ex As Exception
                            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", $"alert('Erro ao buscar dados: {ex.Message}');", True)
                        Finally
                            conn.Close()
                        End Try
                    End Using
                End Using
            End If
        End Sub



        Protected Sub EnviarEmail(sender As Object, e As EventArgs)
            Try
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                Dim email As New MailMessage()
                email.From = New MailAddress("menoti@sf3.com.br")
                email.To.Add(ddlAprovador.SelectedItem.Value)
                Dim dataAtual As String = DateTime.Now.ToString("dd/MM/yyyy")
                email.Subject = "SOLICITAÇÃO DE PAGAMENTO SHOPCRED - " & dataAtual
                email.IsBodyHtml = True

                Dim body As String = "<h3>Informações de Despesa</h3>"
                body &= "<table border='1' cellpadding='10' cellspacing='0' style='border-collapse:collapse;'>"
                body &= "<tr><th>Histórico</th><td>" & ddlHistorico.SelectedItem.Text & "</td></tr>"
                body &= "<tr><th>Data Pagamento</th><td>" & txtDataPagamento.Text & "</td></tr>"
                body &= "<tr><th>Descrição</th><td>" & txtDescricao.Text & "</td></tr>"
                body &= "<tr><th>Valor Bruto</th><td>" & "R$ " & txtValorBruto.Text & "</td></tr>"
                body &= "<tr><th>Valor Líquido</th><td>" & "R$ " & txtValorLiquido.Text & "</td></tr>"
                body &= "<tr><th>Favorecido</th><td>" & txtFavorecido.Text & "</td></tr>"
                body &= "<tr><th>CPF/CNPJ</th><td>" & txtCpfCnpj.Text & "</td></tr>"
                body &= "<tr><th>Forma de Pagamento</th><td>" & ddlFormaPagamento.SelectedItem.Text & "</td></tr>"
                body &= "<tr><th>Banco</th><td>" & txtBanco.Text & "</td></tr>"
                body &= "<tr><th>Agência</th><td>" & txtAgencia.Text & "</td></tr>"
                body &= "<tr><th>Conta Corrente</th><td>" & txtContaCorrente.Text & "</td></tr>"
                body &= "</table>"

                Dim av As AlternateView = AlternateView.CreateAlternateViewFromString(body, Encoding.UTF8, "text/html")
                email.AlternateViews.Add(av)

                If FileUpload1.HasFiles Then
                    Dim zipFileName As String = Server.MapPath("~/Uploads/AGENDAMENTO_" & txtDescricao.Text & ".zip")
                    Using zip As New Ionic.Zip.ZipFile()
                        For Each file As HttpPostedFile In FileUpload1.PostedFiles
                            zip.AddEntry(Path.GetFileName(file.FileName), file.InputStream)
                        Next
                        zip.Save(zipFileName)
                    End Using

                    Dim attach As New Attachment(zipFileName)
                    email.Attachments.Add(attach)
                End If

                Dim smtp As New SmtpClient("smtp.office365.com")
                smtp.Port = 587
                smtp.Credentials = New Net.NetworkCredential("menoti@sf3.com.br", "Huq99291")
                smtp.EnableSsl = True

                smtp.Send(email)
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", $"alert('Agenda enviada com sucesso!');", True)
                btnSalvar_Click(sender, e)



            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert", $"alert('Erro: {ex.Message}');", True)
            End Try
        End Sub




        'Protected Sub GridViewRiscoAnalitico_RowCreated(sender As Object, e As GridViewRowEventArgs)
        '    Try


        '        If e.Row.RowType = DataControlRowType.DataRow And e.Row.RowState = DataControlRowState.Normal Then
        '            e.Row.CssClass = "GridviewScrollC3Item"
        '        End If
        '        If e.Row.RowType = DataControlRowType.DataRow And e.Row.RowState = DataControlRowState.Alternate Then
        '            e.Row.CssClass = "GridviewScrollC3Item2"
        '        End If


        '    Catch ex As Exception

        '    End Try
        'End Sub


        'Public Property DataGridView As DataTable
        '    Get
        '        If ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") Is Nothing Then


        '            ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = GetData()

        '        End If

        '        Return DirectCast(ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C"), DataTable)
        '    End Get
        '    Set(value As DataTable)
        '        ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = value
        '    End Set
        'End Property


        'Protected Sub BindGridViewData()
        '    GridViewRiscoAnalitico.DataSource = GetData()

        '    GridViewRiscoAnalitico.DataBind()

        'End Sub

        'Protected Sub BindGridViewDataView()

        '    GridViewRiscoAnalitico.DataSource.DataSource = DataGridView
        '    GridViewRiscoAnalitico.DataSource.DataBind()

        'End Sub


        'Protected Sub BindGridView1DataView()

        '    Dim dataReferencia As DateTime
        '    Dim dataStr As String = txtData.Text.Trim()

        '    Debug.WriteLine("Valor de txtData.Text: " & dataStr)

        '    If DateTime.TryParseExact(dataStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, dataReferencia) Then

        '        Debug.WriteLine("Data convertida: " & dataReferencia.ToString("yyyyMMdd"))
        '        Dim dataFormatada As String = dataReferencia.ToString("yyyyMMdd")
        '        GridViewRiscoAnalitico.DataSource = GetData(dataFormatada)
        '        GridViewRiscoAnalitico.DataBind()
        '        GridViewRiscoAnalitico.AllowPaging = "True"
        '    Else

        '        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Data inválida!', 'Por favor, forneça uma data válida.');", True)
        '    End If

        'End Sub




        'Private Function GetData() As DataTable

        '    Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        '    Dim table As DataTable

        '    table = Util.ClassBD.GetExibirGrid("[SCR_CP_COLAB_AGENTES]", "SCR_CP_COLAB_AGENTES", strConn)

        '    Return table

        'End Function



        'Private Function GetData() As DataTable
        '    Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        '    Dim table As New DataTable()


        '    Using con As New SqlConnection(strConn)
        '        Using cmd As New SqlCommand($"SELECT * FROM TB_AGENDAMENTO_SIG ", con)
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


        'Private Function GetData(dataReferencia As String) As DataTable
        '    Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        '    Dim resultTable As New DataTable()
        '    Dim usuarioLogado As String = ContextoWeb.UsuarioLogado.Login

        '    Using con As New SqlConnection(strConn)
        '        Using cmd As New SqlCommand($"EXEC SCR_CNAB550_CARGA '{dataReferencia}'", con)
        '            cmd.CommandType = CommandType.Text
        '            con.Open()
        '            Using reader As SqlDataReader = cmd.ExecuteReader()
        '                Try
        '                    resultTable.Load(reader)
        '                    If resultTable.Rows.Count = 0 Then
        '                        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Sem Resposta', 'Nenhum arquivo encontrado na data informada.');", True)
        '                    Else
        '                        Dim totalValor As Decimal = 0
        '                        Dim totalPaseunro As Integer = 0


        '                        For Each row As DataRow In resultTable.Rows
        '                            totalValor += Convert.ToDecimal(row("VLR_PAGO"))
        '                            If Not IsDBNull(row("PASEUNRO")) Then
        '                                totalPaseunro += 1
        '                            End If
        '                        Next


        '                        resultTable.Columns.Add("TotalValor", GetType(Decimal))
        '                        resultTable.Columns.Add("TotalPaseunro", GetType(Integer))


        '                        If resultTable.Rows.Count > 0 Then
        '                            resultTable.Rows(0)("TotalValor") = totalValor
        '                            resultTable.Rows(0)("TotalPaseunro") = totalPaseunro
        '                        End If
        '                        For i As Integer = 1 To resultTable.Rows.Count - 1
        '                            resultTable.Rows(i)("TotalValor") = DBNull.Value
        '                            resultTable.Rows(i)("TotalPaseunro") = DBNull.Value
        '                        Next


        '                        GravarLogExecucao(usuarioLogado, dataReferencia, totalValor, totalPaseunro)
        '                    End If
        '                Catch ex As Exception
        '                    ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Erro', 'Ocorreu um erro ao buscar os dados.');", True)
        '                End Try
        '            End Using
        '        End Using
        '    End Using
        '    Return resultTable
        'End Function





        'Protected Sub GridViewRiscoAnalitico_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

        '    GridViewRiscoAnalitico.PageIndex = IIf(e.NewPageIndex < 0, 0, e.NewPageIndex)
        '    BindGridViewDataView()
        'End Sub




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

        'Public Sub btnCarregar_Click(sender As Object, e As EventArgs)
        '    Try


        '        BindGridViewData()

        '    Catch ex As Exception

        '    Finally
        '        GC.Collect()
        '    End Try

        'End Sub




        Public Overrides Sub VerifyRenderingInServerForm(control As Control)
            'Not Remove
            ' Confirms that an HtmlForm control is rendered for the specified ASP.NET
            '     server control at run time. 

        End Sub


        'Protected Sub btnExcel_Click(sender As Object, e As EventArgs)

        '    Try
        '        GridViewRiscoAnalitico.AllowPaging = False
        '        'BindGridView1Data()
        '        ExportExcel(GridViewRiscoAnalitico)

        '    Catch ex As Exception
        '        Throw ex
        '    End Try

        'End Sub

        'Private Sub ExportExcel(objGrid As GridView)
        '    Try
        '        If Not IsNothing(objGrid.HeaderRow) Then
        '            Response.Clear()
        '            Response.Buffer = True
        '            Dim filename As String = String.Format("Cnab-arquivo-baixa-{0}-{1}-{2}.xls", DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year)
        '            Response.AddHeader("content-disposition", "attachment;filename=" + filename)
        '            Response.ContentEncoding = System.Text.Encoding.Default
        '            Response.Charset = ""
        '            Response.ContentType = "application/vnd.ms-excel"

        '            Using sw As New StringWriter()
        '                Dim hw As New HtmlTextWriter(sw)

        '                objGrid.HeaderRow.BackColor = Color.White
        '                For Each cell As TableCell In objGrid.HeaderRow.Cells
        '                    cell.CssClass = "GridviewScrollC3Header"
        '                Next
        '                For Each row As GridViewRow In objGrid.Rows
        '                    row.BackColor = Color.White
        '                    For i As Integer = 0 To row.Cells.Count - 1
        '                        Dim cell As TableCell = row.Cells(i)

        '                        If i = 0 Then
        '                            cell.Text = FormatValueForFirstColumn(cell.Text)
        '                        End If

        '                        If row.RowIndex Mod 2 = 0 Then
        '                            cell.CssClass = "GridviewScrollC3Item"
        '                        Else
        '                            cell.CssClass = "GridviewScrollC3Item2"
        '                        End If

        '                        Dim controls As New List(Of Control)()
        '                        For Each control As Control In cell.Controls
        '                            controls.Add(control)
        '                        Next

        '                        For Each control As Control In controls
        '                            Select Case control.GetType().Name
        '                                Case "HyperLink"
        '                                    cell.Controls.Add(New Literal() With {
        '                                .Text = TryCast(control, HyperLink).Text
        '                            })
        '                                    Exit Select
        '                                Case "TextBox"
        '                                    cell.Controls.Add(New Literal() With {
        '                                .Text = TryCast(control, TextBox).Text
        '                            })
        '                                    Exit Select
        '                                Case "LinkButton"
        '                                    cell.Controls.Add(New Literal() With {
        '                                .Text = TryCast(control, LinkButton).Text
        '                            })
        '                                    Exit Select
        '                                Case "CheckBox"
        '                                    cell.Controls.Add(New Literal() With {
        '                                .Text = TryCast(control, CheckBox).Text
        '                            })
        '                                    Exit Select
        '                                Case "RadioButton"
        '                                    cell.Controls.Add(New Literal() With {
        '                                .Text = TryCast(control, RadioButton).Text
        '                            })
        '                                    Exit Select
        '                            End Select
        '                            cell.Controls.Remove(control)
        '                        Next
        '                    Next
        '                Next

        '                objGrid.RenderControl(hw)

        '                Dim fi As FileInfo = New FileInfo(Server.MapPath("~/css/GridviewScroll.css"))
        '                Dim sb As New System.Text.StringBuilder
        '                Dim sr As StreamReader = fi.OpenText()
        '                Do While sr.Peek() >= 0
        '                    sb.Append(sr.ReadLine())
        '                Loop
        '                sr.Close()

        '                Dim style As String = "<html><head><style type='text/css'>" & sb.ToString() & "</style><head>"
        '                Response.Write(style)

        '                Response.Write("Cnab - Arquivo de Baixa </p> ")

        '                Response.Output.Write(sw.ToString())

        '                HttpContext.Current.Response.Flush()
        '                HttpContext.Current.Response.SuppressContent = True
        '                HttpContext.Current.ApplicationInstance.CompleteRequest()
        '            End Using
        '        End If
        '    Catch ex As Exception
        '        Throw ex
        '    End Try
        'End Sub

        'Private Function FormatValueForFirstColumn(value As String) As String
        '    Dim number As Decimal
        '    If Decimal.TryParse(value, number) Then
        '        Return number.ToString("F2")
        '    End If
        '    Return value
        'End Function


        'Protected Sub hfGridView1SV_ValueChanged(sender As Object, e As EventArgs)
        '    Session(HfGridView1Svid) = hfGridView1SV.Value
        'End Sub

        'Protected Sub hfGridView1SH_ValueChanged(sender As Object, e As EventArgs)
        '    Session(HfGridView1Shid) = hfGridView1SH.Value
        'End Sub

        'Protected Sub GridViewRiscoAnalitico_DataBound(sender As Object, e As EventArgs)
        '    Dim gridView As GridView = CType(sender, GridView)
        '    If Not IsNothing(gridView.DataSource) AndAlso CType(gridView.DataSource, System.Data.DataTable).Rows.Count > 0 Then
        '        Util.CWebPageUtil.PreencheListaDePaginas(gridView, CType(gridView.BottomPagerRow.Cells(0).FindControl("PagerPages"), DropDownList))
        '    End If
        'End Sub

        'Protected Sub PagerPages_SelectedIndexChanged(sender As Object, e As EventArgs)

        '    GridViewRiscoAnalitico.DataSource = DataGridView()
        '    GridViewRiscoAnalitico.PageIndex = CType(sender, DropDownList).SelectedIndex
        '    GridViewRiscoAnalitico.DataBind()

        'End Sub

    End Class
End Namespace