Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Santana.Seguranca
Imports Util


Namespace Paginas.Comercial

    Public Class RepacRefin

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                'Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Now.Date.AddMonths(-1).ToString("MM/yyyy"))
                'txtDataAte.Text = UltimoDiaUtilMesAnterior(UltimoDiaMesAnterior)

                'txtDataDe.Text = (Convert.ToDateTime(txtDataAte.Text).AddMonths(-5)).ToString("dd/MM/yyyy")
                'txtDataDe.Text = "01/" + Right(txtDataDe.Text, 7)

                'If ContextoWeb.DadosTransferencia.CodAgente = 0 Then
                '    Carrega_Agente()
                'Else
                '    Carrega_Agente()
                '    ddlAgente.SelectedIndex = ddlAgente.Items.IndexOf(ddlAgente.Items.FindByValue(ContextoWeb.DadosTransferencia.CodAgente.ToString()))
                'End If


                If ContextoWeb.DadosTransferencia.CodAgente <> 0 Then
                    BindGridView1Data()
                    ContextoWeb.DadosTransferencia.CodAgente = 0
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
        Private Function UltimoDiaUtilMesAnterior(Data As Date) As String

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime("01/" + Data.ToString("MM/yyyy"))

            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(1)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)

            If UltimoDiaMesAnterior <= Convert.ToDateTime("01/aug/2014") Then

                If (UltimoDiaMesAnterior.DayOfWeek = DayOfWeek.Sunday) Then
                    UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-2)
                ElseIf (UltimoDiaMesAnterior.DayOfWeek = DayOfWeek.Saturday) Then
                    UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)
                End If
            End If
            Return UltimoDiaMesAnterior.ToString("dd/MM/yyyy")

        End Function


        Protected Sub btnDataAnteriorDe_Click(sender As Object, e As EventArgs)


            'Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataDe.Text)
            'UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(-1)

            'txtDataDe.Text = UltimoDiaMesAnterior

        End Sub


        Protected Sub btnProximaDataDe_Click(sender As Object, e As EventArgs)

            'Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataDe.Text)
            'UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddMonths(1)

            'txtDataDe.Text = UltimoDiaMesAnterior

        End Sub

        Protected Sub btnDataAnteriorAte_Click(sender As Object, e As EventArgs)


            'Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataAte.Text)
            'ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddMonths(-1)

            'If ultimoDiaMesAnterior.Year = Now.Date.Year Then
            '    If ultimoDiaMesAnterior.Month <= Now.Date.Month Then
            '        txtDataAte.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)
            '    End If
            'ElseIf ultimoDiaMesAnterior.Year < Now.Date.Year Then
            '    txtDataAte.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)
            'End If

        End Sub


        Protected Sub btnProximaDataAte_Click(sender As Object, e As EventArgs)

            'Dim ultimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataAte.Text)
            'ultimoDiaMesAnterior = ultimoDiaMesAnterior.AddMonths(1)

            'If ultimoDiaMesAnterior.Year = Now.Date.Year Then
            '    If ultimoDiaMesAnterior.Month <= Now.Date.Month Then
            '        txtDataAte.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)
            '    End If
            'ElseIf ultimoDiaMesAnterior.Year < Now.Date.Year Then
            '    txtDataAte.Text = UltimoDiaUtilMesAnterior(ultimoDiaMesAnterior)
            'End If

        End Sub



        Private Sub Carrega_Agente()

            'Try

            '    Dim objDataAgente = New DbAgente
            '    Dim codGerente As String

            '    Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            '    Dim connection As New SqlConnection(strConn)

            '    If ContextoWeb.UsuarioLogado.Perfil = 8 Then
            '        codGerente = ContextoWeb.UsuarioLogado.codGerente
            '    Else
            '        codGerente = "99"
            '    End If


            '    If codGerente = "99" Then
            '        objDataAgente.CarregarTodosRegistros(ddlAgente)

            '        ddlAgente.Items.Insert(0, New ListItem("Todos", "99"))
            '        ddlAgente.SelectedIndex = 0

            '    Else
            '        Dim con As New SqlConnection(strConn)
            '        Dim Vagente As String = ""

            '        Dim cmd As New SqlCommand("Select O3DESCR, O3CODORG from CDCSANTANAMicroCredito..TORG3 (nolock) where O3codorg IN (" & codGerente & ")", con)

            '        cmd.Connection.Open()

            '        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            '        While dr.Read()
            '            Vagente = Trim(dr.GetString(0))
            '            Dim AGENTE1 = New ListItem
            '            AGENTE1.Value = Trim(dr.GetString(1))
            '            AGENTE1.Text = Trim(Vagente)
            '            ddlAgente.Items.Add(AGENTE1)
            '        End While
            '        dr.Close()
            '        con.Close()
            '    End If

            'Catch ex As Exception

            'Finally
            '    GC.Collect()
            'End Try
        End Sub

        Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

            Try

                If e.Row.RowType = DataControlRowType.DataRow Then

                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

                        If IsDBNull(drow("CONTRATO")) Then
                            e.Row.Cells(0).Text = ""
                        Else
                            e.Row.Cells(0).Text = drow("CONTRATO")
                        End If

                        If IsDBNull(drow("CGC")) Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = drow("CGC")
                        End If

                        If IsDBNull(drow("NOME")) Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = drow("NOME")
                        End If

                        If IsDBNull(drow("ENDERECO")) Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = drow("ENDERECO")
                        End If

                        If IsDBNull(drow("BAIRRO")) Then
                            e.Row.Cells(4).Text = ""
                        Else
                            e.Row.Cells(4).Text = drow("BAIRRO")
                        End If

                        If IsDBNull(drow("CIDADE")) Then
                            e.Row.Cells(5).Text = ""
                        Else
                            e.Row.Cells(5).Text = drow("CIDADE")
                        End If

                        If IsDBNull(drow("CEP")) Then
                            e.Row.Cells(6).Text = ""
                        Else
                            e.Row.Cells(6).Text = drow("CEP")
                        End If

                        If IsDBNull(drow("UF")) Then
                            e.Row.Cells(7).Text = ""
                        Else
                            e.Row.Cells(7).Text = drow("UF")
                        End If

                        If IsDBNull(drow("TELFIXO")) Then
                            e.Row.Cells(8).Text = ""
                        Else
                            e.Row.Cells(8).Text = drow("TELFIXO")
                        End If

                        If IsDBNull(drow("CELULAR")) Then
                            e.Row.Cells(9).Text = ""
                        Else
                            e.Row.Cells(9).Text = drow("CELULAR")
                        End If

                        If IsDBNull(drow("PROFISSAO")) Then
                            e.Row.Cells(10).Text = ""
                        Else
                            e.Row.Cells(10).Text = drow("PROFISSAO")
                        End If

                        If IsDBNull(drow("OCUPACAO")) Then
                            e.Row.Cells(11).Text = ""
                        Else
                            e.Row.Cells(11).Text = drow("OCUPACAO")
                        End If

                        If IsDBNull(drow("RENDA")) Then
                            e.Row.Cells(12).Text = ""
                        Else
                            e.Row.Cells(12).Text = CNumero.FormataNumero(drow("RENDA"), 2)
                        End If

                        If IsDBNull(drow("PRODUTO")) Then
                            e.Row.Cells(13).Text = ""
                        Else
                            e.Row.Cells(13).Text = drow("PRODUTO")
                        End If

                        If IsDBNull(drow("VALOR_LIB")) Then
                            e.Row.Cells(14).Text = ""
                        Else
                            e.Row.Cells(14).Text = CNumero.FormataNumero(drow("VALOR_LIB"), 2)
                        End If

                        If IsDBNull(drow("VALOR_FIN")) Then
                            e.Row.Cells(15).Text = ""
                        Else
                            e.Row.Cells(15).Text = CNumero.FormataNumero(drow("VALOR_FIN"), 2)
                        End If

                        If IsDBNull(drow("PLANO")) Then
                            e.Row.Cells(16).Text = ""
                        Else
                            e.Row.Cells(16).Text = drow("PLANO")
                        End If

                        If IsDBNull(drow("VLR_PARC")) Then
                            e.Row.Cells(17).Text = ""
                        Else
                            e.Row.Cells(17).Text = drow("VLR_PARC")
                        End If

                        If IsDBNull(drow("SALDO_DEVEDOR")) Then
                            e.Row.Cells(18).Text = ""
                        Else
                            e.Row.Cells(18).Text = drow("SALDO_DEVEDOR")
                        End If

                        If IsDBNull(drow("COD_AGENTE")) Then
                            e.Row.Cells(19).Text = ""
                        Else
                            e.Row.Cells(19).Text = drow("COD_AGENTE")
                        End If

                        If IsDBNull(drow("AGENTE")) Then
                            e.Row.Cells(20).Text = ""
                        Else
                            e.Row.Cells(20).Text = drow("AGENTE")
                        End If

                        If IsDBNull(drow("MODALIDADE")) Then
                            e.Row.Cells(21).Text = ""
                        Else
                            e.Row.Cells(21).Text = drow("MODALIDADE")
                        End If

                        If IsDBNull(drow("HIST_RENEG")) Then
                            e.Row.Cells(22).Text = ""
                        Else
                            e.Row.Cells(22).Text = drow("HIST_RENEG")
                        End If

                        If IsDBNull(drow("ATRASO")) Then
                            e.Row.Cells(23).Text = ""
                        Else
                            e.Row.Cells(23).Text = drow("ATRASO")
                        End If

                        If IsDBNull(drow("MARCA")) Then
                            e.Row.Cells(24).Text = ""
                        Else
                            e.Row.Cells(24).Text = drow("MARCA")
                        End If

                        If IsDBNull(drow("MODELO")) Then
                            e.Row.Cells(25).Text = ""
                        Else
                            e.Row.Cells(25).Text = drow("MODELO")
                        End If

                        If IsDBNull(drow("ANO_FABRIC")) Then
                            e.Row.Cells(26).Text = ""
                        Else
                            e.Row.Cells(26).Text = drow("ANO_FABRIC")
                        End If

                        If IsDBNull(drow("ANO_MODELO")) Then
                            e.Row.Cells(27).Text = ""
                        Else
                            e.Row.Cells(27).Text = drow("ANO_MODELO")
                        End If

                        If IsDBNull(drow("VLR_FIPE")) Then
                            e.Row.Cells(28).Text = ""
                        Else
                            e.Row.Cells(28).Text = CNumero.FormataNumero(drow("VLR_FIPE"), 2)
                        End If

                        If IsDBNull(drow("PREJUIZO")) Then
                            e.Row.Cells(29).Text = ""
                        Else
                            e.Row.Cells(29).Text = drow("PREJUIZO")
                        End If

                        If IsDBNull(drow("BNDU")) Then
                            e.Row.Cells(30).Text = ""
                        Else
                            e.Row.Cells(30).Text = drow("BNDU")
                        End If

                        If IsDBNull(drow("ATRASO_5D")) Then
                            e.Row.Cells(31).Text = ""
                        Else
                            e.Row.Cells(31).Text = drow("ATRASO_5D")
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
            GridView1.PageIndex = 0
            GridView1.DataBind()
            GridView1.AllowPaging = "True"

        End Sub

        Protected Sub BindGridView1DataView()

            GridView1.DataSource = DataGridView
            GridView1.DataBind()

        End Sub


        Private Function GetData() As DataTable

            'Dim oDataDe As New CDataHora(Convert.ToDateTime(txtDataDe.Text))
            'Dim oDataAte As New CDataHora(Convert.ToDateTime(txtDataAte.Text))


            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim table As DataTable = Util.ClassBD.GetExibirGrid("SCR_REPAC_REFIN", "SCR_REPAC_REFIN", strConn)

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
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Base de Acionamento de Cliente' ,'Acionamento de Clientes');", True)
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
                    Dim filename As String = String.Format("RepacsRefin_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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

                        Response.Write("Repacs de Refin.")

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

    End Class

End Namespace