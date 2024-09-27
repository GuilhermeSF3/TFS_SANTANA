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

Namespace Paginas.Cadastro.Gerencial

    Public Class DRECarga

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then

                txtDataDe.Text = Convert.ToDateTime(Now.Date.AddDays(-1).ToString("dd/MM/yyyy"))
                ' txtDataAte.Text = Convert.ToDateTime(Now.Date.ToString("dd/MM/yyyy"))

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


        'Protected Sub btnDataAnterior2_Click(sender As Object, e As EventArgs)

        '    Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataAte.Text)
        '    txtDataAte.Text = UltimoDiaMesAnterior.AddDays(-1)

        'End Sub


        'Protected Sub btnProximaData2_Click(sender As Object, e As EventArgs)

        '    Dim UltimoDiaMesAnterior As Date = Convert.ToDateTime(txtDataAte.Text)
        '    txtDataAte.Text = UltimoDiaMesAnterior.AddDays(1)

        'End Sub



        Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
            If FileUpload1.HasFile Then


                Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
                Dim connection As New SqlConnection(strConn)
                Dim command As New SqlCommand()


                Try


                    Dim strDT_REF As String
                    Dim strORDEM As String
                    Dim strRECEITA_DESPESA As String
                    Dim strTIPO As String
                    Dim strCONTA As String
                    Dim strCOSIF As String
                    Dim strPOR_CONTRATO As String
                    Dim strEMPRESA As String
                    Dim strRAZAO As String
                    Dim strCONTA_DESCR As String
                    Dim strDt_fim As String
                    Dim strCod_grupo As String
                    Dim strDESCR_GRUPO As String

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
                        command.CommandText = "TRUNCATE TABLE DRE_CARGA_CONTA"
                        command.Connection.Open()
                        command.ExecuteNonQuery()
                        command.Connection.Close()

                        strBatche.AppendFormat(" Begin Tran ")

                        Do While objReader.Peek() <> -1

                            textLine = objReader.ReadLine()

                            If textLine.Trim() = "" Then Continue Do

                            count = count + 1
                            countBatche = countBatche + 1
                            ' PULA A 1A LINHA DE CABEÇALHOS
                            If count = 1 Then Continue Do


                            Dim lineArray As Array
                            'lineArray = textLine.Split(";")
                            lineArray = textLine.Split(";") ' CSV COM ;

                            strDT_REF = formataTexto(lineArray(0))
                            strORDEM = formataTexto(lineArray(1))
                            strRECEITA_DESPESA = formataTexto(lineArray(2))
                            strTIPO = formataTexto(lineArray(3))
                            strCONTA = formataTexto(lineArray(4))
                            strCOSIF = formataTexto(lineArray(5))
                            strPOR_CONTRATO = formataTexto(lineArray(6))
                            strEMPRESA = formataTexto(lineArray(7))
                            strRAZAO = formataTexto(lineArray(8))
                            strCONTA_DESCR = formataTexto(lineArray(9))
                            strDt_fim = formataTexto(lineArray(10))
                            strCod_grupo = formataTexto(lineArray(11))
                            strDESCR_GRUPO = formataTexto(lineArray(12))

                            'strBatche.AppendFormat(" Insert Into DRE_CARGA_CONTA Values ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12})",
                            strBatche.AppendFormat(" Insert  DRE_CARGA_CONTA  ( dt_ref," + _
                            "ORDEM, RECEITA_DESPESA, TIPO, CONTA, COSIF, POR_CONTRATO," + _
                            "EMPRESA, RAZAO, CONTA_DESCR, dt_fim, cod_grupo, DESCR_GRUPO) Values ( {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}) ",
                            strDT_REF,
                            strORDEM,
                            strRECEITA_DESPESA,
                            strTIPO,
                            strCONTA,
                            strCOSIF,
                            strPOR_CONTRATO,
                            strEMPRESA,
                            strRAZAO,
                            strCONTA_DESCR,
                            strDt_fim,
                            strCod_grupo,
                            strDESCR_GRUPO
                                                    )

                            ' ANTES 8 CAMPOS 9
                            If countBatche >= 13 Then

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



                    strBatche.Append(" exec scr_DRE_proc_CONTAsNovas ")
                    'strBatche.Append(" INSERT INTO PROT_CONFIRMA_HISTORICO ")

                    'strBatche.Append("Select  '" & Right(txtDataDe.Text, 4) & Mid(txtDataDe.Text, 4, 2) & Left(txtDataDe.Text, 2) & "'")
                    'strBatche.Append("	   ,[CODIGO_CLIENTE] ")
                    'strBatche.Append("      ,[NOME_CLIENTE] ")
                    'strBatche.Append("      ,[CONTRATO] ")
                    'strBatche.Append("      ,[PARCELA] ")
                    'strBatche.Append("      ,[PROTOCOLO] ")
                    'strBatche.Append("      ,[DT_RECEPCAO] ")
                    'strBatche.Append("      ,[CARTORIO] ")
                    'strBatche.Append("      ,[IBGE] ")
                    'strBatche.Append("      ,[CIDADE] ")
                    'strBatche.Append("From PROT_CARGA_CONFIRMA ")
                    'strBatche.Append(" TRUNCATE TABLE PROT_CARGA_CONFIRMA ")
                    'strBatche.Append(" Commit ")

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


                        If IsDBNull(drow("dt_ref")) Then
                            e.Row.Cells(0).Text = ""
                        Else
                            e.Row.Cells(0).Text = drow("dt_ref")
                        End If

                        If IsDBNull(drow("ORDEM")) Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = drow("ORDEM")
                        End If

                        If IsDBNull(drow("RECEITA_DESPESA")) Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = drow("RECEITA_DESPESA")
                        End If

                        If IsDBNull(drow("TIPO")) Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = drow("TIPO")
                        End If

                        If IsDBNull(drow("CONTA")) Then
                            e.Row.Cells(4).Text = ""
                        Else
                            e.Row.Cells(4).Text = drow("CONTA")
                        End If

                        If IsDBNull(drow("COSIF")) Then
                            e.Row.Cells(5).Text = ""
                        Else
                            e.Row.Cells(5).Text = drow("COSIF")
                        End If

                        If IsDBNull(drow("POR_CONTRATO")) Then
                            e.Row.Cells(6).Text = ""
                        Else
                            e.Row.Cells(6).Text = drow("POR_CONTRATO")
                        End If

                        If IsDBNull(drow("EMPRESA")) Then
                            e.Row.Cells(7).Text = ""
                        Else
                            e.Row.Cells(7).Text = drow("EMPRESA")
                        End If

                        If IsDBNull(drow("RAZAO")) Then
                            e.Row.Cells(8).Text = ""
                        Else
                            e.Row.Cells(8).Text = drow("RAZAO")
                        End If

                        If IsDBNull(drow("CONTA_DESCR")) Then
                            e.Row.Cells(9).Text = ""
                        Else
                            e.Row.Cells(9).Text = drow("CONTA_DESCR")
                        End If

                        If IsDBNull(drow("dt_fim")) Then
                            e.Row.Cells(10).Text = ""
                        Else
                            e.Row.Cells(10).Text = drow("dt_fim")
                        End If
                        If IsDBNull(drow("cod_grupo")) Then
                            e.Row.Cells(11).Text = ""
                        Else
                            e.Row.Cells(11).Text = drow("cod_grupo")
                        End If
                        If IsDBNull(drow("DESCR_grupo")) Then
                            e.Row.Cells(12).Text = ""
                        Else
                            e.Row.Cells(12).Text = drow("DESCR_grupo")
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

            table = Util.ClassBD.GetExibirGrid("[SCR_DRE_CONTA] '" & Right(txtDataDe.Text, 4) & Mid(txtDataDe.Text, 4, 2) & Left(txtDataDe.Text, 2) &
                                                                "' ", "CARGACONFIRMACAO", strConn)

            Return table

        End Function


        Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

            GridView1.PageIndex = IIf(e.NewPageIndex < 0, 0, e.NewPageIndex)
            BindGridView1DataView()
        End Sub

        Protected Sub btnProcessar_Click(sender As Object, e As EventArgs)

            Session.Timeout = 8  ' 8 MIN DE PROCESSAMENTO

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim connection As New SqlConnection(strConn)
            Dim command As SqlCommand = New SqlCommand( _
            "scr_DRE_PROCESSAR '" & Right(txtDataDe.Text, 4) & Mid(txtDataDe.Text, 4, 2) & Left(txtDataDe.Text, 2) & "'", connection)

            command.CommandTimeout = Convert.ToInt32(2000000)
            command.Connection.Open()
            Dim ddlValues As SqlDataReader
            ddlValues = command.ExecuteReader()
            ddlValues.Close()
            command.Connection.Close()
            command.Connection.Dispose()
            connection.Close()
        End Sub

        Protected Sub btnRemover_Click(sender As Object, e As EventArgs)
            '            Response.Redirect("../../Menu.aspx")
 
        Dim strConn2 As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
        Dim connection2 As New SqlConnection(strConn2)
            Dim command2 As SqlCommand = New SqlCommand( _
            "scr_DRE_REMOVER '" & Right(txtDataDe.Text, 4) & Mid(txtDataDe.Text, 4, 2) & Left(txtDataDe.Text, 2) & "'", connection2)

            command2.CommandTimeout = Convert.ToInt32(2000000)
            command2.Connection.Open()
        Dim ddlValues2 As SqlDataReader
            ddlValues2 = command2.ExecuteReader()
            ddlValues2.Close()

            command2.Connection.Close()
            command2.Connection.Dispose()

            connection2.Close()


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
                    Dim filename As String = String.Format("DRE_CONTAS_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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
                        Response.Write("DRE CONTAS")
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
                retorno = Left(Valor, Len(Valor) - Decimais) + "." + Right(Valor, Decimais)
            End If

            If retorno.Trim() = "" Then
                retorno = "0"
            End If

            Return retorno
        End Function

    End Class
End Namespace