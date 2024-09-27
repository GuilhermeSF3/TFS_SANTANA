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


Namespace Paginas.Cobranca.Relatorios

    Public Class ARs

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

                    Dim strCodigo As String
                    Dim strTipoRegistro As String
                    Dim strSequencia As String
                    Dim strSistema As String
                    Dim strSegmento As String
                    Dim strDocumento As String
                    Dim strContrato As String
                    Dim strValor As String
                    Dim strDataVctoDebito As String
                    Dim strDataInclusaoDebito As String
                    Dim strDtEnvioCarta As String
                    Dim strDtAgendamCarta As String
                    Dim strTipoCarta As String
                    Dim strNoCarta As String
                    Dim strNSUCarta As String
                    Dim strDtRetornoCorreio As String
                    Dim strCodRetorno As String
                    Dim strMotivoDevolucao As String
                    Dim strCodCliente As String
                    Dim strEndereco As String
                    Dim strBairro As String
                    Dim strCidade As String
                    Dim strUF As String
                    Dim strCEP As String
                    Dim strReservado As String
                    Dim strControle As String
                    Dim strMesReferencia As String = String.Concat(Right(txtDataDe.Text, 4), Mid(txtDataDe.Text, 4, 2), Left(txtDataDe.Text, 2)) 

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
                        command.CommandText = "TRUNCATE TABLE AR_CARGA"
                        command.Connection.Open()
                        command.ExecuteNonQuery()
                        command.Connection.Close()

                        strBatche.AppendFormat(" Begin Tran ")

                        Do While objReader.Peek() <> -1

                            textLine = objReader.ReadLine()

                            If Mid(textLine, 1, 18) = "000000000000000000" Then Continue Do
                            If Mid(textLine, 1, 18) = "999999999999999999" Then Continue Do

                            count = count + 1
                            countBatche = countBatche + 1

                            strCodigo = formataTexto(Mid(textLine, 1, 8))
                            strTipoRegistro = formataTexto(Mid(textLine, 9, 1))
                            strSequencia = formataTexto(Mid(textLine, 10, 5))
                            strSistema = formataTexto(Mid(textLine, 15, 1))
                            strSegmento = formataTexto(Mid(textLine, 16, 3))
                            strDocumento = formataTexto(Mid(textLine, 19, 20))
                            strContrato = formataTexto(Mid(textLine, 39, 30))
                            strValor = formataValor(Mid(textLine, 69, 11), 2)
                            strDataVctoDebito = formataData(Mid(textLine, 80, 8))
                            strDataInclusaoDebito = formataData(Mid(textLine, 88, 8))
                            strDtEnvioCarta = formataData(Mid(textLine, 96, 8))
                            strDtAgendamCarta = formataData(Mid(textLine, 104, 8))
                            strTipoCarta = formataTexto(Mid(textLine, 112, 2))
                            strNoCarta = formataTexto(Mid(textLine, 114, 2))
                            strNSUCarta = formataTexto(Mid(textLine, 116, 11))
                            strDtRetornoCorreio = formataData(Mid(textLine, 127, 8))
                            strCodRetorno = formataTexto(Mid(textLine, 135, 2))
                            strMotivoDevolucao = formataTexto(Mid(textLine, 137, 2))
                            strCodCliente = formataTexto(Mid(textLine, 139, 8))
                            strEndereco = formataTexto(Mid(textLine, 147, 50))
                            strBairro = formataTexto(Mid(textLine, 197, 20))
                            strCidade = formataTexto(Mid(textLine, 217, 20))
                            strUF = formataTexto(Mid(textLine, 237, 2))
                            strCEP = formataTexto(Mid(textLine, 239, 8))
                            strReservado = formataTexto(Mid(textLine, 247, 53))
                            strControle = formataTexto(Mid(textLine, 300, 1))


                            strBatche.AppendFormat(" Insert Into AR_CARGA Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, " &
                                                  "{17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25})",
                                                    strCodigo,
                                                    strTipoRegistro,
                                                    strSequencia,
                                                    strSistema,
                                                    strSegmento,
                                                    strDocumento,
                                                    strContrato,
                                                    strValor,
                                                    strDataVctoDebito,
                                                    strDataInclusaoDebito,
                                                    strDtEnvioCarta,
                                                    strDtAgendamCarta,
                                                    strTipoCarta,
                                                    strNoCarta,
                                                    strNSUCarta,
                                                    strDtRetornoCorreio,
                                                    strCodRetorno,
                                                    strMotivoDevolucao,
                                                    strCodCliente,
                                                    strEndereco,
                                                    strBairro,
                                                    strCidade,
                                                    strUF,
                                                    strCEP,
                                                    strReservado,
                                                    strControle)


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
                    strBatche.Append(" INSERT INTO AR_HISTORICO ")
                    strBatche.AppendFormat("Select '{0}'", strMesReferencia)
                    strBatche.Append("	   ,[CODigo] ")
                    strBatche.Append("      ,[Tipo_Registro] ")
                    strBatche.Append("      ,[Sequencia] ")
                    strBatche.Append("      ,[Sistema] ")
                    strBatche.Append("      ,[Segmento] ")
                    strBatche.Append("      ,[Documento] ")
                    strBatche.Append("      ,[Contrato] ")
                    strBatche.Append("      ,[Valor] ")
                    strBatche.Append("      ,[Data_Vcto_Debito] ")
                    strBatche.Append("      ,[Data_Inclusao_Debito] ")
                    strBatche.Append("      ,[Dt_Envio_Carta] ")
                    strBatche.Append("      ,[Dt_Agendam_Carta] ")
                    strBatche.Append("      ,[Tipo_Carta] ")
                    strBatche.Append("      ,[No_Carta] ")
                    strBatche.Append("      ,[NSU_Carta] ")
                    strBatche.Append("      ,[Dt_Retorno_Correio] ")
                    strBatche.Append("      ,[Cod_Retorno] ")
                    strBatche.Append("      ,[Motivo_Devolucao] ")
                    strBatche.Append("      ,[Cod_Cliente] ")
                    strBatche.Append("      ,[Endereco] ")
                    strBatche.Append("      ,[Bairro] ")
                    strBatche.Append("      ,[Cidade] ")
                    strBatche.Append("      ,[UF] ")
                    strBatche.Append("      ,[CEP] ")
                    strBatche.Append("      ,[Reservado] ")
                    strBatche.Append("      ,[Controle] ")
                    strBatche.Append("From AR_CARGA ")
                    strBatche.Append(" TRUNCATE TABLE AR_CARGA ")
                    strBatche.Append(" Commit ")

                    command.Connection = connection
                    command.CommandText = strBatche.ToString()
                    command.Connection.Open()
                    command.ExecuteNonQuery()
                    command.Connection.Close()

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

                        If IsDBNull(drow("CODigo")) Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = drow("CODigo")
                        End If

                        If IsDBNull(drow("Tipo_Registro")) Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = drow("Tipo_Registro")
                        End If

                        If IsDBNull(drow("Sequencia")) Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = drow("Sequencia")
                        End If

                        If IsDBNull(drow("Sistema")) Then
                            e.Row.Cells(4).Text = ""
                        Else
                            e.Row.Cells(4).Text = drow("Sistema")
                        End If

                        If IsDBNull(drow("Segmento")) Then
                            e.Row.Cells(5).Text = ""
                        Else
                            e.Row.Cells(5).Text = drow("Segmento")
                        End If

                        If IsDBNull(drow("DESCR_SEGMENTO")) Then
                            e.Row.Cells(6).Text = ""
                        Else
                            e.Row.Cells(6).Text = drow("DESCR_SEGMENTO")
                        End If

                        If IsDBNull(drow("Documento")) Then
                            e.Row.Cells(7).Text = ""
                        Else
                            e.Row.Cells(7).Text = drow("Documento")
                        End If

                        If IsDBNull(drow("Contrato")) Then
                            e.Row.Cells(8).Text = ""
                        Else
                            e.Row.Cells(8).Text = drow("Contrato")
                        End If

                        If IsDBNull(drow("Valor") Or drow("Valor") = 0) Then
                            e.Row.Cells(9).Text = ""
                        Else
                            e.Row.Cells(9).Text = CNumero.FormataNumero(drow("Valor"), 2)
                        End If

                        If IsDBNull(drow("Data_Vcto_Debito")) Then
                            e.Row.Cells(10).Text = ""
                        Else
                            e.Row.Cells(10).Text = drow("Data_Vcto_Debito")
                        End If

                        If IsDBNull(drow("Data_Inclusao_Debito")) Then
                            e.Row.Cells(11).Text = ""
                        Else
                            e.Row.Cells(11).Text = drow("Data_Inclusao_Debito")
                        End If

                        If IsDBNull(drow("Dt_Envio_Carta")) Then
                            e.Row.Cells(12).Text = ""
                        Else
                            e.Row.Cells(12).Text = drow("Dt_Envio_Carta")
                        End If

                        If IsDBNull(drow("Dt_Agendam_Carta")) Then
                            e.Row.Cells(13).Text = ""
                        Else
                            e.Row.Cells(13).Text = drow("Dt_Agendam_Carta")
                        End If

                        If IsDBNull(drow("Tipo_Carta")) Then
                            e.Row.Cells(14).Text = ""
                        Else
                            e.Row.Cells(14).Text = drow("Tipo_Carta")
                        End If

                        If IsDBNull(drow("No_Carta")) Then
                            e.Row.Cells(15).Text = ""
                        Else
                            e.Row.Cells(15).Text = drow("No_Carta")
                        End If

                        If IsDBNull(drow("DESCR_NUM_CARTA")) Then
                            e.Row.Cells(16).Text = ""
                        Else
                            e.Row.Cells(16).Text = drow("DESCR_NUM_CARTA")
                        End If

                        If IsDBNull(drow("NSU_Carta")) Then
                            e.Row.Cells(17).Text = ""
                        Else
                            e.Row.Cells(17).Text = drow("NSU_Carta")
                        End If

                        If IsDBNull(drow("Dt_Retorno_Correio")) Then
                            e.Row.Cells(18).Text = ""
                        Else
                            e.Row.Cells(18).Text = drow("Dt_Retorno_Correio")
                        End If

                        If IsDBNull(drow("Cod_Retorno")) Then
                            e.Row.Cells(19).Text = ""
                        Else
                            e.Row.Cells(19).Text = drow("Cod_Retorno")
                        End If

                        If IsDBNull(drow("DESCR_RETORNO")) Then
                            e.Row.Cells(20).Text = ""
                        Else
                            e.Row.Cells(20).Text = drow("DESCR_RETORNO")
                        End If

                        If IsDBNull(drow("Motivo_Devolucao")) Then
                            e.Row.Cells(21).Text = ""
                        Else
                            e.Row.Cells(21).Text = drow("Motivo_Devolucao")
                        End If

                        If IsDBNull(drow("DESCR_MOTIVO_DEVOL")) Then
                            e.Row.Cells(22).Text = ""
                        Else
                            e.Row.Cells(22).Text = drow("DESCR_MOTIVO_DEVOL")
                        End If

                        If IsDBNull(drow("Cod_Cliente")) Then
                            e.Row.Cells(23).Text = ""
                        Else
                            e.Row.Cells(23).Text = drow("Cod_Cliente")
                        End If

                        If IsDBNull(drow("Endereco")) Then
                            e.Row.Cells(24).Text = ""
                        Else
                            e.Row.Cells(24).Text = drow("Endereco")
                        End If

                        If IsDBNull(drow("Bairro")) Then
                            e.Row.Cells(25).Text = ""
                        Else
                            e.Row.Cells(25).Text = drow("Bairro")
                        End If

                        If IsDBNull(drow("Cidade")) Then
                            e.Row.Cells(26).Text = ""
                        Else
                            e.Row.Cells(26).Text = drow("Cidade")
                        End If

                        If IsDBNull(drow("UF")) Then
                            e.Row.Cells(27).Text = ""
                        Else
                            e.Row.Cells(27).Text = drow("UF")
                        End If

                        If IsDBNull(drow("CEP")) Then
                            e.Row.Cells(28).Text = ""
                        Else
                            e.Row.Cells(28).Text = drow("CEP")
                        End If

                        If IsDBNull(drow("Reservado")) Then
                            e.Row.Cells(29).Text = ""
                        Else
                            e.Row.Cells(29).Text = drow("Reservado")
                        End If

                        If IsDBNull(drow("Controle")) Then
                            e.Row.Cells(30).Text = ""
                        Else
                            e.Row.Cells(30).Text = drow("Controle")
                        End If
                        e.Row.Cells(31).Text = drow("FC_COD")
                        e.Row.Cells(32).Text = drow("FC_DESCR")
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
            GridView1.DataSource = GetData()
            GridView1.PageIndex = 0
            GridView1.DataBind()
        End Sub

        Protected Sub BindGridView1DataView()

            GridView1.DataSource = DataGridView
            GridView1.DataBind()

        End Sub


        Private Function GetData() As DataTable


            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim table As DataTable

            table = Util.ClassBD.GetExibirGrid("[SCR_AR] '" & Right(txtDataDe.Text, 4) & Mid(txtDataDe.Text, 4, 2) & Left(txtDataDe.Text, 2) &
                                                                "', '" & Right(txtDataAte.Text, 4) & Mid(txtDataAte.Text, 4, 2) & Left(txtDataAte.Text, 2) & "' ", "ARs", strConn)

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
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('CARGA e CONSULTA DO AR BVS' ,'Troca de arquivos com a Boa Vista (BVS) de Remessa Controle Primeiro Comunicado das cartas de AVISO, AR ou agendamento de AR.       Para UPLOAD de arquivos favor informar a data DE.         Para CONSULTAR os dados recebidos favor informar o intervalo de DATAS e clicar no botão CARREGAR.           Para Enviar Ocorrencias ao Função clicar no botão TXT.');", True)
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
                    Dim filename As String = String.Format("ARs_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())
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
                        Response.Write("ARs")
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


        Protected Sub btnExportOcorrencias_Click(sender As Object, e As EventArgs)

            Try

                If Not IsNothing(GridView1.HeaderRow) Then
                    GridView1.AllowPaging = False
                    BindGridView1Data()
                    ExportCsv(GridView1)
                End If


            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        Protected Sub ExportCsv(objGrid As GridView)


            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.Buffer = True

            Dim filename As String = String.Format("CAPT_GV_{0}_{1}_{2}.txt", DateTime.Today.Day.ToString(), DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString())

            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + filename)
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default
            HttpContext.Current.Response.Charset = ""
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"



            Dim sb As New StringBuilder()
            Dim sequence As Int32 = 1


            'Adiciona cabeçalho
            sb.Append("0") 'Tipo de Registro
            sb.Append("P".PadRight(22)) 'Identificador
            sb.Append("".PadRight(6)) 'Brancos
            sb.Append(Date.Now.ToString("ddMMyy")) 'Data do Sistema
            sb.Append("".PadRight(248)) 'Brancos
            sb.Append(Right("000000" & RTrim(sequence.ToString().PadRight(6)), 6)) 'Seqüencial

            'append new line
            sb.Append(vbCr & vbLf)


            'Adiciona linhas
            For i As Integer = 0 To objGrid.Rows.Count - 1
                ' CHECA SE TEM COD DE RETORNO, SE NÃO, NÃO ENVIA NO ARQUIVO DE OCORRENCIAS
                If Strings.LTrim(objGrid.Rows(i).Cells(19).Text) <> "" Then
                    ' CONTA COLUNAS
                    For k As Integer = 0 To 6


                        Select Case k
                            Case 0
                                sb.Append("10000") 'Tipo de Registro
                                'Case 1
                                'sb.Append(objGrid.Rows(i).Cells(k).Text.PadRight(4)) 'Cod. Produto do Backoffice
                            Case 1
                                sb.Append(objGrid.Rows(i).Cells(8).Text.PadRight(15)) 'Numero do Contrato
                                'sb.Append(objGrid.Rows(i).Cells(3).Text.PadRight(15)) 'Numero do Contrato
                            Case 2
                                sb.Append("999")
                                'sb.Append(Right("000" & RTrim(objGrid.Rows(i).Cells(4).Text.PadRight(3)), 3)) 'Numero da Parcela
                            Case 3
                                'sb.Append("000")
                                sb.Append(objGrid.Rows(i).Cells(31).Text.PadRight(3)) 'Código da Ocorrência FUNÇÃO
                                'sb.Append(Right("000" & RTrim(objGrid.Rows(i).Cells(5).Text.PadRight(3)), 3)) 'Código da Ocorrência
                            Case 4
                                sb.Append("01") 'Seqüencial da Ocorrência
                                'sb.Append(Right("00" & RTrim(objGrid.Rows(i).Cells(6).Text.PadRight(2)), 2)) 'Seqüencial da Ocorrência
                            Case 5
                                sb.Append(objGrid.Rows(i).Cells(32).Text.PadRight(255)) 'Observação Ocorrência  FUNÇÃO   
                                'sb.Append("".PadRight(255)) ' brancos
                                'sb.Append(objGrid.Rows(i).Cells(7).Text.PadRight(255)) 'Observação Ocorrência

                        End Select
                    Next

                    sequence = sequence + 1
                    'sb.Append(sequence.ToString().PadRight(6)) 'Seqüencial sem zeros
                    sb.Append(Right("000000" & RTrim(sequence.ToString().PadRight(6)), 6)) 'Seqüencial

                    'append new line
                    sb.Append(vbCr & vbLf)
                End If
            Next

            sequence = sequence + 1
            'Adiciona rodape
            sb.Append("9") 'Tipo de Registro
            sb.Append("".PadRight(282)) 'Brancos
            '    sb.Append(sequence.ToString().PadRight(6)) 'Seqüencial sem zeros
            sb.Append(Right("000000" & RTrim(sequence.ToString().PadRight(6)), 6)) 'Seqüencial


            'Dim style As String = "<html><head><style type='text/css'>" & sb.ToString() & "</style><head>"
            'Response.Write(style)
            'Response.Write("ARsCapt")


            Response.Output.Write(sb.ToString())
            HttpContext.Current.Response.Flush()
            HttpContext.Current.Response.SuppressContent = True
            HttpContext.Current.ApplicationInstance.CompleteRequest()

        End Sub


        Private Function formataData(Data As String) As String
            Dim retorno As String
  
            retorno = string.Concat("'", Right(Data, 4), Mid(Data, 3, 2), Left(Data, 2), "'")

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