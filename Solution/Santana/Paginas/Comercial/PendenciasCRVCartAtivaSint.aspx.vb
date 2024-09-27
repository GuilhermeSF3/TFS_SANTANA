Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic
Imports Santana.Seguranca
Imports Util


Namespace Paginas.Comercial

    Public Class PendenciasCRVCartAtivaSint

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                Dim dtIni As String = Now.Date.ToString("dd/MM/yyyy")
                txtDataReferencia.Text = Convert.ToDateTime(dtIni)

                txtDataDe.Text = Convert.ToDateTime(dtIni)
                txtDataDe.Text = "01/01/1997"
                txtDataAte.Text = Convert.ToDateTime(Now.Date.AddDays(-60).ToString("dd/MM/yyyy"))

                Dim dtBaixa As String = Convert.ToDateTime(Now.Date.AddDays(-1).ToString("dd/MM/yyyy"))
                txtDtAteBaixa.Text = Convert.ToDateTime(dtBaixa)
                txtDtDeBaixa.Text = Convert.ToDateTime(dtBaixa).AddMonths(-1)
                txtDtDeBaixa.Text = Convert.ToDateTime(txtDtDeBaixa.Text).AddDays(1).ToString("01/MM/yyyy")

                If ContextoWeb.DadosTransferencia.CodAgente = 0 Then
                    Carrega_Regiao()
                    Carrega_Gerencia()
                    Carrega_Prejuizo()
                    Carrega_Renegs()
                    Carrega_Ativos()
                Else
                    Carrega_Regiao()
                    Carrega_Gerencia
                    Carrega_Prejuizo()
                    Carrega_Renegs()
                    Carrega_Ativos()
                End If


                'If ContextoWeb.DadosTransferencia.CodAgente <> 0 Then
                '    BindGridView1Data()
                '    ContextoWeb.DadosTransferencia.CodAgente = 0
                'End If

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

        Protected Sub btnDataAnteriorRef_Click(sender As Object, e As EventArgs)


            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataReferencia.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)

            txtDataReferencia.Text = UltimoDiaMesAnterior

        End Sub


        Protected Sub btnProximaDataRef_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataReferencia.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(1)

            txtDataReferencia.Text = UltimoDiaMesAnterior

        End Sub

        Protected Sub btnDataAnteriorDe_Click(sender As Object, e As EventArgs)


            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataDe.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)

            txtDataDe.Text = UltimoDiaMesAnterior

        End Sub


        Protected Sub btnProximaDataDe_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataDe.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(1)

            txtDataDe.Text = UltimoDiaMesAnterior

        End Sub

        Protected Sub btnDataAnteriorAte_Click(sender As Object, e As EventArgs)


            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataAte.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)

            txtDataAte.Text = UltimoDiaMesAnterior

        End Sub


        Protected Sub btnProximaDataAte_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataAte.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(1)

            txtDataAte.Text = UltimoDiaMesAnterior

        End Sub

        Protected Sub btnDtDeAnterior_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDtDeBaixa.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)

            txtDtDeBaixa.Text = UltimoDiaMesAnterior

        End Sub

        Protected Sub btnDtDeProxima_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDtDeBaixa.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(1)

            txtDtDeBaixa.Text = UltimoDiaMesAnterior

        End Sub

        Protected Sub btnDtAteAnterior_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDtAteBaixa.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(-1)

            txtDtAteBaixa.Text = UltimoDiaMesAnterior

        End Sub

        Protected Sub btnDtAteProxima_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDtAteBaixa.Text)
            UltimoDiaMesAnterior = UltimoDiaMesAnterior.AddDays(1)

            txtDtAteBaixa.Text = UltimoDiaMesAnterior

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


        Private Sub Carrega_Gerencia()

            Try

                ddlGerencia.Items.Insert(0, New ListItem("TODOS", "99"))
                ddlGerencia.Items.Insert(1, New ListItem("São Paulo", "1"))
                ddlGerencia.Items.Insert(2, New ListItem("Interior", "2"))
                ddlGerencia.Items.Insert(3, New ListItem("Dinamo", "3"))

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub Carrega_Regiao()

            Try

                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim con As New SqlConnection(strConn)
                Dim Vagente As String = ""

                Dim Command As New SqlCommand("SELECT COD_REGIAO, NOME_REGIAO FROM TAB_REGIAO", con)
                Command.Connection.Open()

                Dim ddlValues As SqlDataReader
                ddlValues = Command.ExecuteReader()

                ddlRegiao.DataSource = ddlValues
                ddlRegiao.DataValueField = "COD_REGIAO"
                ddlRegiao.DataTextField = "NOME_REGIAO"
                ddlRegiao.DataBind()

                ddlRegiao.Items.Insert(0, New ListItem("TODOS", "99"))
                ddlRegiao.SelectedIndex = 0

                ddlValues.Close()
                Command.Connection.Close()
                Command.Connection.Dispose()
                con.Close()


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub Carrega_Prejuizo()

            Try

                ddlPreju.Items.Insert(0, New ListItem("TODOS", "99"))
                ddlPreju.Items.Insert(1, New ListItem("NÃO", "0"))
                ddlPreju.Items.Insert(2, New ListItem("SIM", "1"))


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub Carrega_Renegs()

            Try

                ddlReneg.Items.Insert(0, New ListItem("TODOS", "99"))
                ddlReneg.Items.Insert(1, New ListItem("NÃO", "0"))
                ddlReneg.Items.Insert(2, New ListItem("SIM", "1"))


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub Carrega_Ativos()

            Try

                ddlAtivo.Items.Insert(0, New ListItem("TODOS", "99"))
                ddlAtivo.Items.Insert(1, New ListItem("NÃO", "0"))
                ddlAtivo.Items.Insert(2, New ListItem("SIM", "1"))


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub

        Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
            Dim Cor As Drawing.Color
            Try

                If e.Row.RowType = DataControlRowType.DataRow Then

                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As Data.DataRow = CType(e.Row.DataItem, System.Data.DataRowView).Row

                        If IsDBNull(drow("COD_REGIAO")) Then
                            e.Row.Cells(0).Text = ""
                        Else
                            e.Row.Cells(0).Text = drow("COD_REGIAO")
                        End If

                        If IsDBNull(drow("NOME_REGIAO")) Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = drow("NOME_REGIAO")
                        End If

                        If IsDBNull(drow("NOME_GERENCIA")) Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = drow("NOME_GERENCIA")
                        End If

                        If IsDBNull(drow("TOTAL")) Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = drow("TOTAL")
                        End If

                        If IsDBNull(drow("PENDENCIA")) Then
                            e.Row.Cells(4).Text = ""
                        Else
                            e.Row.Cells(4).Text = drow("PENDENCIA")
                        End If

                        If IsDBNull(drow("PCT")) Then
                            e.Row.Cells(5).Text = ""
                        Else
                            If drow("PCT") > 10.0 Then
                                Cor = Drawing.ColorTranslator.FromOle(&HCC)
                            Else
                                Cor = Drawing.ColorTranslator.FromOle(&H666666)
                            End If
                            e.Row.Cells(5).Text = drow("PCT")
                            e.Row.Cells(5).ForeColor = Cor
                        End If

                        If IsDBNull(drow("BAIXADAS")) Then
                            e.Row.Cells(6).Text = ""
                        Else
                            e.Row.Cells(6).Text = drow("BAIXADAS")
                        End If

                        If IsDBNull(drow("BX_DT")) Then
                            e.Row.Cells(7).Text = ""
                        Else
                            e.Row.Cells(7).Text = drow("BX_DT")
                        End If

                        If IsDBNull(drow("BX_LIQ")) Then
                            e.Row.Cells(8).Text = ""
                        Else
                            e.Row.Cells(8).Text = drow("BX_LIQ")
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


            GridView1.DataSource = GetData()

            GridView1.DataBind()
            GridView1.AllowPaging = "True"

        End Sub

        Protected Sub BindGridView1DataView()

            GridView1.DataSource = DataGridView
            GridView1.DataBind()

        End Sub


        Private Function GetData() As DataTable

            'Dim oDataContrato As New CDataHora(Convert.ToDateTime(txtDataContrato.Text))
            Dim oDataReferencia As New CDataHora(Convert.ToDateTime(txtDataReferencia.Text))
            Dim oDataDe As New CDataHora(Convert.ToDateTime(txtDataDe.Text))
            Dim oDataAte As New CDataHora(Convert.ToDateTime(txtDataAte.Text))
            Dim oDataDeBx As New CDataHora(Convert.ToDateTime(txtDtDeBaixa.Text))
            Dim oDataAteBx As New CDataHora(Convert.ToDateTime(txtDtAteBaixa.Text))

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim table As DataTable

            table = Util.ClassBD.GetExibirGrid("SCR_CRV_CATIVA_SINT '', '" &
                                                             oDataReferencia.Data.ToString("yyyyMMdd") & "', '" &
                                                             oDataDe.Data.ToString("yyyyMMdd") & "', '" &
                                                             oDataAte.Data.ToString("yyyyMMdd") & "','99', '" &
                                                             ddlRegiao.SelectedValue & "', '" &
                                                             ddlPreju.SelectedValue & "', '" &
                                                             ddlReneg.SelectedValue & "', '" &
                                                             ddlAtivo.SelectedValue & "', '" &
                                                             ddlGerencia.SelectedValue & "', '" &
                                                             oDataDeBx.Data.ToString("yyyyMMdd") & "', '" &
                                                             oDataAteBx.Data.ToString("yyyyMMdd") & "'", "SCR_CRV_CATIVA_SINT", strConn)

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
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Pendencias CRV' ,'Pendencias CRV, Contratos de Veículos com pendencias de Regularização de Documentos (codigo da Pendencia 151).');", True)
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
                    Dim filename As String = String.Format("PendenciasCRVCarteiraAtiva_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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

                        Response.Write("Pendências CRV - Data ref. " & txtDataReferencia.Text & " Data De. " & txtDataDe.Text & " Data Até. " & txtDataAte.Text & " -  Gerência " & ddlGerencia.SelectedItem.Text & " </p> ")

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

End NameSpace