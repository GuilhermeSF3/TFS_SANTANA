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

Namespace Paginas.Protesto

    Public Class CargaRetorno

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                txtDataDe.Text = Convert.ToDateTime(Now.Date.AddDays(-1).ToString("dd/MM/yyyy"))
                txtDataAte.Text = Convert.ToDateTime(Now.Date.ToString("dd/MM/yyyy"))

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


        Protected Sub btnDataAnterior1_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataDe.Text)
            txtDataDe.Text = UltimoDiaMesAnterior.AddDays(-1)

        End Sub


        Protected Sub btnProximaData1_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataDe.Text)
            txtDataDe.Text = UltimoDiaMesAnterior.AddDays(1)

        End Sub


        Protected Sub btnDataAnterior2_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataAte.Text)
            txtDataAte.Text = UltimoDiaMesAnterior.AddDays(-1)

        End Sub


        Protected Sub btnProximaData2_Click(sender As Object, e As EventArgs)

            Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataAte.Text)
            txtDataAte.Text = UltimoDiaMesAnterior.AddDays(1)

        End Sub



        Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
            If FileUpload1.HasFile Then


                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)
                Dim command As New SqlCommand()


                Try


                    Dim strDataMov As String
                    Dim strCodigoCliente As String
                    Dim strNomeCliente As String
                    Dim strContrato As String
                    Dim strParcela As String
                    Dim strProtocolo As String
                    Dim strDataRecepcao As String
                    Dim strCartorio As String
                    Dim strIbge As String
                    Dim strCidade As String
                    Dim strDataOcorrencia As String
                    Dim strStatus As String
                    Dim strDescrStatus As String 
                    Dim strValorCustas As String

                    strDataMov = Right(txtDataAte.Text, 4) & Mid(txtDataAte.Text, 4, 2) & Left(txtDataAte.Text, 2)


                    Dim strBatche As New StringBuilder()
                    Dim Arquivo = "C:\downloads\" & FileUpload1.FileName


                    FileUpload1.SaveAs(Arquivo)
                    Label1.Text = "File name: " & FileUpload1.PostedFile.FileName

                    Dim textLine As String
                    Dim count As Integer
                    Dim countBatche As Integer

                    If File.Exists(Arquivo) = True Then


                        Dim objReader As New StreamReader(Arquivo)


                        command.Connection = connection
                        command.CommandText = "TRUNCATE TABLE PROT_RETORNO"
                        command.Connection.Open()
                        command.ExecuteNonQuery()
                        command.Connection.Close()

                        strBatche.AppendFormat(" Begin Tran ")

                        Do While objReader.Peek() <> -1

                            textLine = objReader.ReadLine()

                            If textLine.Trim() = "" Then Continue Do

                            count = count + 1
                            countBatche = countBatche + 1

                            If count = 1 Then Continue Do

                            Dim lineArray As Array
                            lineArray = textLine.Split(";")


                            strCodigoCliente = IIf(Len(lineArray(0)) = 2, "''", "'" + Mid(lineArray(0), 2, Len(lineArray(0)) - 2) + "'") 'formataTexto(lineArray(0))
                            strNomeCliente = IIf(Len(lineArray(1)) = 2, "''", "'" + Mid(lineArray(1), 2, Len(lineArray(1)) - 2) + "'") 'formataTexto(lineArray(1))
                            strContrato = IIf(Len(lineArray(2)) = 2, "''", "'" + Mid(lineArray(2), 2, Len(lineArray(2)) - 2) + "'") 'formataTexto(lineArray(2))
                            strContrato = IIf(InStr(strContrato, "/") > 0, Left(strContrato, InStr(strContrato, "/") - 1) + "'", strContrato)

                            strParcela = IIf(Len(lineArray(3)) = 2, "''", "'" + Mid(lineArray(3), 2, Len(lineArray(3)) - 2) + "'") 'formataTexto(lineArray(3))
                            strProtocolo = IIf(Len(lineArray(4)) = 2, "''", "'" + Mid(lineArray(4), 2, Len(lineArray(4)) - 2) + "'") 'formataTexto(lineArray(4))
                            strDataRecepcao = IIf(Len(lineArray(5)) = 2, "''", "'" + Mid(lineArray(5), 2, 10) + "'") ' formataTexto(lineArray(5)))
                            strCartorio = IIf(Len(lineArray(6)) = 2, "''", "'" + Mid(lineArray(6), 2, Len(lineArray(6)) - 2) + "'") 'formataTexto(lineArray(6))
                            strIbge = IIf(Len(lineArray(7)) = 2, "''", "'" + Mid(lineArray(7), 2, Len(lineArray(7)) - 2) + "'") 'formataTexto(lineArray(7))
                            strCidade = IIf(Len(lineArray(8)) = 2, "''", "'" + Mid(lineArray(8), 2, Len(lineArray(8)) - 2) + "'") 'formataTexto(lineArray(8))
                            strDataOcorrencia = IIf(Len(lineArray(9)) = 2, "''", "'" + Mid(lineArray(9), 2, 10) + "'") 'formataTexto(lineArray(9))
                            strStatus = IIf(Len(lineArray(10)) = 2, "''", "'" + Mid(lineArray(10), 2, Len(lineArray(10)) - 2) + "'") 'formataTexto(lineArray(10))
                            strDescrStatus = IIf(Len(lineArray(11)) = 2, "''", "'" + Mid(lineArray(11), 2, Len(lineArray(11)) - 2) + "'") 'formataTexto(lineArray(11))
                            strValorCustas = IIf(Len(lineArray(12)) = 2, "0.0", Mid(lineArray(12), 2, Len(lineArray(12)) - 2)) 'formataTexto(lineArray(12))) ' formataValor(lineArray(11), 2)
                            strValorCustas = Replace(strValorCustas, ",", ".")

                            strBatche.AppendFormat(" Insert Into PROT_RETORNO Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12})",
                                                    strCodigoCliente,
                                                    strNomeCliente,
                                                    strContrato,
                                                    strParcela,
                                                    strProtocolo,
                                                    strDataRecepcao,
                                                    strCartorio,
                                                    strIbge,
                                                    strCidade,
                                                    strDataOcorrencia,
                                                    strStatus, strDescrStatus,
                                                    strValorCustas)


                            If countBatche >= 10 Then

                                strBatche.AppendFormat(" Commit ")

                                command.Connection = connection
                                command.CommandText = strBatche.ToString()
                                command.Connection.Open()
                                command.ExecuteNonQuery()
                                command.Connection.Close()

                                countBatche = 0
                                strBatche.Clear()
                                strBatche.AppendFormat(" Begin Tran ")

                            End If

                        Loop

                        strBatche.AppendFormat(" Commit ")

                        command.Connection = connection
                        command.CommandText = strBatche.ToString()
                        command.Connection.Open()
                        command.ExecuteNonQuery()
                        command.Connection.Close()
                        strBatche.Clear()

                        connection.Close()
                        objReader.Close()

                    Else

                        Label1.Text = "Ocorreu erro ao carregar arquivo"

                    End If

                    strBatche.Clear()


                    strBatche.Append(" Begin Tran ")
                    strBatche.Append(" INSERT INTO PROT_RETORNO_HIST ")
                    strBatche.Append(" Select '")
                    strBatche.Append(strDataMov)
                    strBatche.Append("',")
                    strBatche.Append("	   [CODIGO_CLIENTE] ")
                    strBatche.Append("      ,[NOME_CLIENTE] ")
                    strBatche.Append("      ,[CONTRATO] ")
                    strBatche.Append("      ,[PARCELA] ")
                    strBatche.Append("      ,[PROTOCOLO] ")
                    strBatche.Append("      ,[DT_RECEPCAO] ")
                    strBatche.Append("      ,[CARTORIO] ")
                    strBatche.Append("      ,[IBGE] ")
                    strBatche.Append("      ,[CIDADE] ")
                    strBatche.Append("      ,[DATA_OCORRENCIA] ")
                    strBatche.Append("      ,[STATUS], [DESCR_STATUS] ")
                    strBatche.Append("      ,[VALOR_CUSTAS] ")
                    strBatche.Append("From PROT_RETORNO ")

                    strBatche.Append(" Commit ")



                    command.Connection = connection
                    command.CommandText = strBatche.ToString()
                    command.Connection.Open()
                    command.ExecuteNonQuery()
                    command.Connection.Close()

                    count = count - 1
                    Label1.Text = "Concluido com Sucesso! " + count.ToString() + " Registros Importados!"

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


                        If IsDBNull(drow("Data_Mov")) Then
                            e.Row.Cells(0).Text = ""
                        Else
                            e.Row.Cells(0).Text = drow("Data_Mov")
                        End If

                        If IsDBNull(drow("CODIGO_CLIENTE")) Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = drow("CODIGO_CLIENTE")
                        End If

                        If IsDBNull(drow("NOME_CLIENTE")) Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = drow("NOME_CLIENTE")
                        End If

                        If IsDBNull(drow("CONTRATO")) Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = drow("CONTRATO")
                        End If

                        If IsDBNull(drow("PARCELA")) Then
                            e.Row.Cells(4).Text = ""
                        Else
                            e.Row.Cells(4).Text = drow("PARCELA")
                        End If

                        If IsDBNull(drow("PROTOCOLO")) Then
                            e.Row.Cells(5).Text = ""
                        Else
                            e.Row.Cells(5).Text = drow("PROTOCOLO")
                        End If

                        If IsDBNull(drow("DT_RECEPCAO")) Then
                            e.Row.Cells(6).Text = ""
                        Else
                            e.Row.Cells(6).Text = drow("DT_RECEPCAO")
                        End If

                        If IsDBNull(drow("CARTORIO")) Then
                            e.Row.Cells(7).Text = ""
                        Else
                            e.Row.Cells(7).Text = drow("CARTORIO")
                        End If

                        If IsDBNull(drow("IBGE")) Then
                            e.Row.Cells(8).Text = ""
                        Else
                            e.Row.Cells(8).Text = drow("IBGE")
                        End If

                        If IsDBNull(drow("CIDADE")) Then
                            e.Row.Cells(9).Text = ""
                        Else
                            e.Row.Cells(9).Text = drow("CIDADE")
                        End If


                        If IsDBNull(drow("DATA_OCORRENCIA")) Then
                            e.Row.Cells(10).Text = ""
                        Else
                            e.Row.Cells(10).Text = drow("DATA_OCORRENCIA")
                        End If

                        If IsDBNull(drow("STATUS")) Then
                            e.Row.Cells(11).Text = ""
                        Else
                            e.Row.Cells(11).Text = drow("STATUS")
                        End If

                        If IsDBNull(drow("DESCR_STATUS")) Then
                            e.Row.Cells(12).Text = ""
                        Else
                            e.Row.Cells(12).Text = drow("DESCR_STATUS")
                        End If

                        If IsDBNull(drow("VALOR_CUSTAS")) OrElse drow("VALOR_CUSTAS") = 0 Then
                            e.Row.Cells(13).Text = ""
                        Else
                            e.Row.Cells(13).Text = CNumero.FormataNumero(drow("VALOR_CUSTAS"), 2)
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


            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim table As DataTable

            table = Util.ClassBD.GetExibirGrid("[SCR_PROT_RETORNO] '" & Right(txtDataDe.Text, 4) & Mid(txtDataDe.Text, 4, 2) & Left(txtDataDe.Text, 2) &
                                                                "', '" & Right(txtDataAte.Text, 4) & Mid(txtDataAte.Text, 4, 2) & Left(txtDataAte.Text, 2) & "' ", "CARGARETORNO", strConn)

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
                    Dim filename As String = String.Format("Carga_Retorno_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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
                        Response.Write("Retorno")
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
                retorno = Valor
            End If

            If retorno.Trim() = "" Then
                retorno = "0"
            End If

            Return retorno
        End Function

    End Class
End Namespace