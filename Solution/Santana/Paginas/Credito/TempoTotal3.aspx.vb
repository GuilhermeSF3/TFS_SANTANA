Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Santana.Seguranca
Imports Util

Namespace Paginas.Credito


    Public Class TempoTotal3
        Inherits SantanaPage

        Private Const hfGridView1SVID As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const hfGridView1SHID As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            If Not IsPostBack Then


                txtFaixaTempo.Text = ContextoWeb.DadosTransferencia.CodFaixaTempo.ToString() & " " & ContextoWeb.DadosTransferencia.FaixaTempo.ToString() & " " & ContextoWeb.DadosTransferencia.Classe.ToString ' apr rep int

                txtDataDe.Text = ContextoWeb.DadosTransferencia.DataReferencia.ToString()
                txtDataAte.Text = ContextoWeb.DadosTransferencia.DataReferencia2.ToString()

                Carrega_Produto()
                ddlProduto.SelectedIndex = ddlProduto.Items.IndexOf(ddlProduto.Items.FindByValue(ContextoWeb.DadosTransferencia.CodProduto.ToString()))

                Carrega_Analistas()
                ddlAnalista.SelectedIndex = ddlAnalista.Items.IndexOf(ddlAnalista.Items.FindByValue(ContextoWeb.DadosTransferencia.CodAnalista.ToString()))

                Carrega_Agente()
                ddlAgente.SelectedIndex = ddlAgente.Items.IndexOf(ddlAgente.Items.FindByValue(ContextoWeb.DadosTransferencia.CodAgente.ToString()))

                Carrega_Operador()
                ddlOperador.SelectedIndex = ddlOperador.Items.IndexOf(ddlOperador.Items.FindByValue(ContextoWeb.DadosTransferencia.CodOperador.ToString()))

                Carrega_Loja()
                ddlLoja.SelectedIndex = ddlLoja.Items.IndexOf(ddlLoja.Items.FindByValue(ContextoWeb.DadosTransferencia.CodLoja.ToString()))



                txtProposta.Text = ContextoWeb.DadosTransferencia.NumeroProposta.ToString

                If Session(hfGridView1SVID) IsNot Nothing Then
                    hfGridView1SV.Value = DirectCast(Session(hfGridView1SVID), String)
                    Session.Remove(hfGridView1SVID)
                End If

                If Session(hfGridView1SHID) IsNot Nothing Then
                    hfGridView1SH.Value = DirectCast(Session(hfGridView1SHID), String)
                    Session.Remove(hfGridView1SHID)
                End If

            End If


            Const script As String = "$(document).ready(function () { $('.selectpicker').selectpicker(); });"
            ScriptManager.RegisterStartupScript(Me, GetType(String), "selectpicker", script, True)

        End Sub


        Protected Sub btnDataAnteriorDe_Click(sender As Object, e As EventArgs)

            Dim diaAnterior As Date = Convert.ToDateTime(txtDataDe.Text)
            diaAnterior = diaAnterior.AddDays(-1)
            txtDataDe.Text = diaAnterior.ToString("dd/MM/yyyy")


        End Sub


        Protected Sub btnProximaDataDe_Click(sender As Object, e As EventArgs)

            Dim proximoDia As Date = Convert.ToDateTime(txtDataDe.Text)
            proximoDia = proximoDia.AddDays(+1)
            txtDataDe.Text = proximoDia.ToString("dd/MM/yyyy")

        End Sub



        Protected Sub btnDataAnteriorAte_Click(sender As Object, e As EventArgs)

            Dim diaAnterior As Date = Convert.ToDateTime(txtDataAte.Text)
            diaAnterior = diaAnterior.AddDays(-1)
            txtDataAte.Text = diaAnterior.ToString("dd/MM/yyyy")


        End Sub


        Protected Sub btnProximaDataAte_Click(sender As Object, e As EventArgs)

            Dim proximoDia As Date = Convert.ToDateTime(txtDataAte.Text)
            proximoDia = proximoDia.AddDays(+1)
            txtDataAte.Text = proximoDia.ToString("dd/MM/yyyy")

        End Sub


        Private Sub Carrega_Produto()

            Try

                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)
                Dim command As SqlCommand = New SqlCommand(
                "SELECT descr_tipo, descr_tipo as PRODUTO FROM Ttipo_prod (NOLOCK) where COD_PROD  in ( 'V') order by descr_tipo", connection)

                command.Connection.Open()

                Dim ddlValues As SqlDataReader
                ddlValues = command.ExecuteReader()

                ddlProduto.DataSource = ddlValues
                ddlProduto.DataValueField = "PRODUTO"
                ddlProduto.DataTextField = "descr_tipo"
                ddlProduto.DataBind()

                ddlProduto.Items.Insert(0, New ListItem("Todos", "0"))

                ddlProduto.SelectedIndex = 0

                ddlValues.Close()
                command.Connection.Close()
                command.Connection.Dispose()
                connection.Close()


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub



        Private Sub Carrega_Analistas()

            Try

                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)
                Dim command As SqlCommand = New SqlCommand(
                " SELECT USNOMEUSUC, USNOMEUSU FROM ACESSOCORP..TUSU (NOLOCK) WHERE USATIVO='S' AND USGRUPO1 IN ('04895','04896','04897') ORDER BY USNOMEUSUC", connection)

                command.Connection.Open()

                Dim ddlValues As SqlDataReader
                ddlValues = command.ExecuteReader()

                ddlAnalista.DataSource = ddlValues
                ddlAnalista.DataValueField = "USNOMEUSUC"
                ddlAnalista.DataTextField = "USNOMEUSUC"
                ddlAnalista.DataBind()

                ddlAnalista.Items.Insert(0, New ListItem("Todos", "0"))
                ddlAnalista.SelectedIndex = 0

                ddlValues.Close()
                command.Connection.Close()
                command.Connection.Dispose()
                connection.Close()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
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
                    objDataAgente.CarregarTodosRegistros(ddlAgente)

                    ddlAgente.Items.Insert(0, New ListItem("Todos", "0"))
                    ddlAgente.SelectedIndex = 0

                Else
                    Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                    Dim con As New SqlConnection(strConn)
                    Dim Vagente As String = ""


                    Dim cmd As New SqlCommand("Select top 1 A13DESCR from CDCSANTANAMicroCredito..TORG3 (nolock) where O3codorg='" & codGerente & "'", con)

                    cmd.Connection.Open()

                    Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                    While dr.Read()
                        Vagente = Trim(dr.GetString(0))
                    End While
                    dr.Close()
                    con.Close()


                    Dim AGENTE1 = New ListItem
                    AGENTE1.Value = codGerente
                    AGENTE1.Text = Trim(Vagente)
                    ddlAgente.Items.Add(AGENTE1)
                End If


            Catch ex As Exception

            Finally
                GC.Collect()
            End Try

        End Sub



        Private Sub Carrega_Operador()

            Try

                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)


                Dim sql As String = "SELECT O3CODORG,O3DESCR FROM 	CDCSANTANAMicrocredito..TORG3 O2  (NOLOCK) WHERE O3ATIVA='A' ORDER BY O3DESCR "

                Dim command As SqlCommand = New SqlCommand(sql, connection)

                command.Connection.Open()

                Dim ddlValues As SqlDataReader
                ddlValues = command.ExecuteReader()

                ddlOperador.DataSource = ddlValues
                ddlOperador.DataValueField = "O3CODORG"
                ddlOperador.DataTextField = "O3DESCR"
                ddlOperador.DataBind()

                ddlOperador.Items.Insert(0, New ListItem("Todos", "0"))
                ddlOperador.SelectedIndex = 0

                ddlValues.Close()
                command.Connection.Close()
                command.Connection.Dispose()
                connection.Close()

            Catch ex As Exception

            Finally
                GC.Collect()
            End Try
        End Sub



        Private Sub Carrega_Loja()

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim connection As New SqlConnection(strConn)


            Dim sql As String = "SELECT O4CODORG,O4DESCR FROM 	CDCSANTANAMicrocredito..TORG4 O2  (NOLOCK) WHERE O4ATIVA='A' ORDER BY O4DESCR"

            Dim command As SqlCommand = New SqlCommand(sql, connection)

            command.Connection.Open()

            Dim ddlValues As SqlDataReader
            ddlValues = command.ExecuteReader()

            ddlLoja.DataSource = ddlValues
            ddlLoja.DataValueField = "O4CODORG"
            ddlLoja.DataTextField = "O4DESCR"
            ddlLoja.DataBind()

            ddlLoja.Items.Insert(0, New ListItem("Todos", "0"))
            ddlLoja.SelectedIndex = 0

            ddlValues.Close()
            command.Connection.Close()
            command.Connection.Dispose()
            connection.Close()



        End Sub




        Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

            Try

                If e.Row.RowType = DataControlRowType.DataRow Then
                    If Not e.Row.DataItem Is Nothing Then

                        Dim drow As DataRow = CType(e.Row.DataItem, DataRowView).Row
                        Dim col As Integer




                        col = 0
                        If IsDBNull(drow("epnrprop")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("epnrprop")
                        End If

                        col += 1
                        If IsDBNull(drow("Ini")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("Ini")
                        End If

                        col += 1
                        If IsDBNull(drow("Fim")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("Fim")
                        End If

                        col += 1
                        If IsDBNull(drow("tempo")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("tempo")
                        End If

                        col += 1
                        If IsDBNull(drow("Moda")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("Moda")
                        End If

                        col += 1
                        If IsDBNull(drow("produto")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("produto")
                        End If


                        col += 1
                        If IsDBNull(drow("situacao")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("situacao")
                        End If

                        col += 1
                        If IsDBNull(drow("EPFASE")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("EPFASE")
                        End If

                        col += 1
                        If IsDBNull(drow("EPALCADA")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("EPALCADA")
                        End If

                        col += 1
                        If IsDBNull(drow("seq")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("seq")
                        End If

                        col += 1
                        If IsDBNull(drow("entra")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("entra")
                        End If

                        col += 1
                        If IsDBNull(drow("fim_fase")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("fim_fase")
                        End If

                        col += 1
                        If IsDBNull(drow("Fase")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("Fase")
                        End If

                        col += 1
                        If IsDBNull(drow("tempo_fase")) Then
                            e.Row.Cells(col).Text = ""
                        Else
                            e.Row.Cells(col).Text = drow("tempo_fase")
                        End If



                 
                    End If
                End If


            Catch ex As Exception

            End Try
        End Sub




        Protected Sub BindGridView1Data()

            ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = Nothing
            GridView1.DataSource = GetData()
            GridView1.DataBind()

        End Sub


        Protected Sub BindGridView1DataView()

            GridView1.DataSource = DataGridView
            GridView1.DataBind()

        End Sub


        Private Function GetData() As DataTable

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim table As DataTable = ClassBD.GetExibirGrid("SCR_credito_tempo_total3 '" & Right(txtDataDe.Text, 4) & Mid(txtDataDe.Text, 4, 2) & Left(txtDataDe.Text, 2) &
                                                                                 "', '" & Right(txtDataAte.Text, 4) & Mid(txtDataAte.Text, 4, 2) & Left(txtDataAte.Text, 2) &
                                                                                 "', '" & ContextoWeb.DadosTransferencia.CodFaixaTempo.ToString &
                                                                                 "', '" & ContextoWeb.DadosTransferencia.Classe.ToString &
                                                                                 "', '" & ddlProduto.SelectedValue &
                                                                                 "', '" & ddlAnalista.SelectedValue &
                                                                                 "', '" & ddlAgente.SelectedValue &
                                                                                 "', '" & ddlOperador.SelectedValue &
                                                                                 "', '" & ddlLoja.SelectedValue &
                                                                                 "', '" & txtProposta.Text & "'", "CreditoTempoTotal3", strConn)

            Return table

        End Function



        Protected Sub btnImpressao_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "tmp", "Alerta('Em construção!' ,'Esta funcionalidade esta em desenvolvimento.');", True)
        End Sub


        Protected Sub btnHelp_Click(sender As Object, e As EventArgs)
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "tmp", "Alerta('Ajuda' ,'Proposta selecionada com os tempos por FASE.');", True)
        End Sub


        Protected Sub btnMenu_Click(sender As Object, e As EventArgs)
            Response.Redirect("/Paginas/Credito/TempoTotal2.aspx")
        End Sub


        Protected Sub btnCarregar_Click(sender As Object, e As EventArgs)
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

                If Not IsNothing(GridView1.HeaderRow) Then

                    Response.Clear()
                    Response.Buffer = True
                    Dim filename As String = String.Format("CreditoTempoTotal3_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename)
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.ms-excel"

                    Using sw As New StringWriter()

                        Dim hw As New HtmlTextWriter(sw)

                        GridView1.AllowPaging = False
                        BindGridView1Data()

                        GridView1.HeaderRow.BackColor = Color.White
                        For Each cell As TableCell In GridView1.HeaderRow.Cells
                            cell.CssClass = "GridviewScrollC3Header"
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

                        GridView1.RenderControl(hw)

                        Dim fi As FileInfo = New FileInfo(Server.MapPath("~/css/GridviewScroll.css"))
                        Dim sb As New System.Text.StringBuilder
                        Dim sr As StreamReader = fi.OpenText()
                        Do While sr.Peek() >= 0
                            sb.Append(sr.ReadLine())
                        Loop


                        sr.Close()

                        Dim style As String = "<html><head><style type='text/css'>" & sb.ToString() & "</style><head>"
                        Response.Write(style)
                        Response.Write("Tempos Totais - Tela 3  <p>")
                        Response.Write("De: " & txtDataDe.Text & " Até: " & txtDataAte.Text & " Produto: " & ddlProduto.SelectedItem.ToString & " Analista: " & ddlAnalista.SelectedItem.ToString & " Agente: " & ddlAgente.SelectedItem.ToString & " Operador: " & ddlOperador.SelectedItem.ToString & " Loja: " & ddlLoja.SelectedItem.ToString & " Proposta: " & txtProposta.Text)
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
            Session(hfGridView1SVID) = hfGridView1SV.Value
        End Sub

        Protected Sub hfGridView1SH_ValueChanged(sender As Object, e As EventArgs)
            Session(hfGridView1SHID) = hfGridView1SH.Value
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



        Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

            If e.NewPageIndex >= 0 Then
                GridView1.PageIndex = e.NewPageIndex
                BindGridView1DataView()
            End If
        End Sub



    


        Private Sub SortDropDown(ByVal dd As DropDownList)
            Dim ar As ListItem()
            Dim i As Long = 0
            For Each li As ListItem In dd.Items
                ReDim Preserve ar(i)
                ar(i) = li
                i += 1
            Next
            Dim ar1 As Array = ar

            ar1.Sort(ar1, New ListItemComparer)
            dd.Items.Clear()
            dd.Items.AddRange(ar1)
            dd.SelectedIndex = 0
        End Sub

        Private Class ListItemComparer : Implements IComparer

            Public Function Compare(ByVal x As Object,
              ByVal y As Object) As Integer _
              Implements System.Collections.IComparer.Compare
                Dim a As ListItem = x
                Dim b As ListItem = y
                Dim c As New CaseInsensitiveComparer
                Return c.Compare(a.Text, b.Text)
            End Function
        End Class


    End Class
End Namespace