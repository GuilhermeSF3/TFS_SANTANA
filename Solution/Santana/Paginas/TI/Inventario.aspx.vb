Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Runtime.InteropServices.ComTypes
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic
Imports Santana.Paginas.Cadastro
Imports Santana.Seguranca
Imports Util

Namespace Paginas.TI

    Public Class Inventario

        Inherits SantanaPage

        Private Const HfGridView1Svid As String = "1399D405-7696-46C6-8FC6-2091DCE8A6F6"
        Private Const HfGridView1Shid As String = "D7177246-2FDD-477C-A27E-B94DEFD23EE4"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not IsPostBack Then


                Dim today As DateTime = DateTime.Now
                Dim previousDate As DateTime
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

                        If IsDBNull(drow("ID")) Then
                            e.Row.Cells(0).Text = ""
                        Else
                            e.Row.Cells(0).Text = drow("ID")
                        End If

                        If IsDBNull(drow("USUARIO")) Then
                            e.Row.Cells(1).Text = ""
                        Else
                            e.Row.Cells(1).Text = drow("USUARIO")
                        End If

                        If IsDBNull(drow("OBSERVACAO")) Then
                            e.Row.Cells(2).Text = ""
                        Else
                            e.Row.Cells(2).Text = drow("OBSERVACAO")
                        End If

                        If IsDBNull(drow("HOSTNAME")) Then
                            e.Row.Cells(3).Text = ""
                        Else
                            e.Row.Cells(3).Text = drow("HOSTNAME")
                        End If

                        If IsDBNull(drow("ETIQUETA")) Then
                            e.Row.Cells(4).Text = ""
                        Else
                            e.Row.Cells(4).Text = drow("ETIQUETA")
                        End If

                        If IsDBNull(drow("CONSERTO_OS")) Then
                            e.Row.Cells(5).Text = ""
                        Else
                            e.Row.Cells(5).Text = drow("CONSERTO_OS")
                        End If

                        If IsDBNull(drow("DATA_ENVIO")) Then
                            e.Row.Cells(6).Text = ""
                        Else
                            e.Row.Cells(6).Text = drow("DATA_ENVIO")
                        End If

                        If IsDBNull(drow("DATA_DEVOLUCAO")) Then
                            e.Row.Cells(7).Text = ""
                        Else
                            e.Row.Cells(7).Text = drow("DATA_DEVOLUCAO")
                        End If

                        If IsDBNull(drow("STATUS")) Then
                            e.Row.Cells(8).Text = ""
                        Else
                            e.Row.Cells(8).Text = drow("STATUS")
                        End If

                        If IsDBNull(drow("MODELO")) Then
                            e.Row.Cells(9).Text = ""
                        Else
                            e.Row.Cells(9).Text = drow("MODELO")
                        End If

                        If IsDBNull(drow("PROCESSADOR")) Then
                            e.Row.Cells(10).Text = ""
                        Else
                            e.Row.Cells(10).Text = drow("PROCESSADOR")
                        End If

                        If IsDBNull(drow("MEMORIA_RAM")) Then
                            e.Row.Cells(11).Text = ""
                        Else
                            e.Row.Cells(11).Text = drow("MEMORIA_RAM")
                        End If

                        If IsDBNull(drow("MEMORIA")) Then
                            e.Row.Cells(12).Text = ""
                        Else
                            e.Row.Cells(12).Text = drow("MEMORIA")
                        End If

                        If IsDBNull(drow("FABRICANTE")) Then
                            e.Row.Cells(13).Text = ""
                        Else
                            e.Row.Cells(13).Text = drow("FABRICANTE")
                        End If

                        If IsDBNull(drow("TERMO")) Then
                            e.Row.Cells(14).Text = ""
                        Else
                            e.Row.Cells(14).Text = drow("TERMO")
                        End If

                        If IsDBNull(drow("DEPARTAMENTO")) Then
                            e.Row.Cells(15).Text = ""
                        Else
                            e.Row.Cells(15).Text = drow("DEPARTAMENTO")
                        End If

                        If IsDBNull(drow("NOTE_OU_DESK")) Then
                            e.Row.Cells(16).Text = ""
                        Else
                            e.Row.Cells(16).Text = drow("NOTE_OU_DESK")
                        End If

                        If IsDBNull(drow("CONEXAO_REDE")) Then
                            e.Row.Cells(17).Text = ""
                        Else
                            e.Row.Cells(17).Text = drow("CONEXAO_REDE")
                        End If

                        If IsDBNull(drow("ULTIMO_DONO")) Then
                            e.Row.Cells(18).Text = ""
                        Else
                            e.Row.Cells(18).Text = drow("ULTIMO_DONO")
                        End If

                    End If
                End If
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Erro', 'Ocorreu um erro ao processar a linha.');", True)
            End Try
        End Sub


        Private Function inventarioLoad() As DataTable
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")
            Dim resultTable As New DataTable()

            Try
                Using con As New SqlConnection(strConn)
                    Using cmd As New SqlCommand("SELECT * FROM Equipamento", con)
                        cmd.CommandType = CommandType.Text
                        con.Open()

                        Using reader As SqlDataReader = cmd.ExecuteReader()
                            resultTable.Load(reader)

                            If resultTable.Rows.Count = 0 Then
                                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Sem Resposta', 'Nenhum arquivo encontrado na data informada.');", True)
                            End If
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "tmp", "Alerta('Erro', 'Ocorreu um erro ao salvar o log.');", True)
            End Try

            Return resultTable
        End Function





        Protected Sub newInventory(ByVal sender As Object, ByVal e As EventArgs)
            Dim id As Integer = 0
            Dim usuario As String = usuarioInput.Text
            Dim observacao As String = observacaoInput.Text
            Dim hostname As String = hostnameInput.Text
            Dim etiqueta As String = etiquetaInput.Text
            Dim conserto_OS As String = consertoOSInput.Text
            Dim data_Envio As DateTime = DateTime.Parse(dataEnvioInput.Text)
            Dim data_Devolucao As DateTime = DateTime.Parse(dataDevolucaoInput.Text)
            Dim status As String = statusInput.Text
            Dim modelo As String = modeloInput.Text
            Dim processador As String = processadorInput.Text
            Dim memoria_RAM As String = memoriaRAMInput.Text
            Dim memoria As String = memoriaInput.Text
            Dim fabricante As String = fabricanteInput.Text
            Dim termo As String = termoInput.Text
            Dim departamento As String = departamentoInput.Text
            Dim note_Ou_Desk As String = noteOuDeskInput.Text
            Dim conexao_Rede As String = conexaoRedeInput.Text
            Dim ultimo_Dono As String = ultimoDonoInput.Text


            Dim dataEnvioFormatada As String = data_Envio.ToString("dd/MM/yyyy")
            Dim dataDevolucaoFormatada As String = data_Devolucao.ToString("dd/MM/yyyy")

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

            Using conn As New SqlConnection(strConn)
                Dim sql As String = "INSERT INTO Equipamento (Usuario, Observacao, Hostname, Etiqueta, Conserto_os, Data_envio, Data_devolucao, Status, Modelo, Processador, Memoria_ram, Memoria, Fabricante, Termo, Departamento, Note_Ou_Desk, Conexao_Rede, Ultimo_Dono) " &
                            "VALUES (@Usuario, @Observacao, @Hostname, @Etiqueta, @Conserto_os, @Data_envio, @Data_devolucao, @Status, @Modelo, @Processador, @Memoria_ram, @Memoria, @Fabricante, @Termo, @Departamento, @Note_Ou_Desk, @Conexao_Rede, @Ultimo_Dono)"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@Usuario", usuario)
                    cmd.Parameters.AddWithValue("@Observacao", observacao)
                    cmd.Parameters.AddWithValue("@Hostname", hostname)
                    cmd.Parameters.AddWithValue("@Etiqueta", etiqueta)
                    cmd.Parameters.AddWithValue("@Conserto_os", conserto_OS)
                    cmd.Parameters.AddWithValue("@Data_envio", dataEnvioFormatada)
                    cmd.Parameters.AddWithValue("@Data_devolucao", dataDevolucaoFormatada)
                    cmd.Parameters.AddWithValue("@Status", status)
                    cmd.Parameters.AddWithValue("@Modelo", modelo)
                    cmd.Parameters.AddWithValue("@Processador", processador)
                    cmd.Parameters.AddWithValue("@Memoria_ram", memoria_RAM)
                    cmd.Parameters.AddWithValue("@Memoria", memoria)
                    cmd.Parameters.AddWithValue("@Fabricante", fabricante)
                    cmd.Parameters.AddWithValue("@Termo", termo)
                    cmd.Parameters.AddWithValue("@Departamento", departamento)
                    cmd.Parameters.AddWithValue("@Note_Ou_Desk", note_Ou_Desk)
                    cmd.Parameters.AddWithValue("@Conexao_Rede", conexao_Rede)
                    cmd.Parameters.AddWithValue("@Ultimo_Dono", ultimo_Dono)
                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                        BindGridView1Data()


                    Catch ex As Exception

                      
                    Finally
                        conn.Close()
                    End Try
                End Using
            End Using
        End Sub


        Public Property DataGridView As DataTable
            Get

                If ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") Is Nothing Then
                    ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = inventarioLoad()

                End If

                Return DirectCast(ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C"), DataTable)
            End Get
            Set(value As DataTable)
                ViewState("CC67EF01-E08F-4AD1-B1B9-3CF591164A8C") = value
            End Set
        End Property


        Protected Sub deleteInventory(ByVal sender As Object, ByVal e As EventArgs)
            Dim id As Integer = identificadorInput.Text

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

            Using conn As New SqlConnection(strConn)
                Dim sql As String = "DELETE FROM Equipamento WHERE ID = @ID"


                Using cmd As New SqlCommand(sql, conn)

                    cmd.Parameters.AddWithValue("@ID", id)
                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                        BindGridView1Data()

                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Sucesso", "alert('Equipamento removido com sucesso!');", True)
                    Catch ex As Exception

                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Erro ao remover o equipamento " & ex.Message & "');", True)
                    Finally
                        conn.Close()
                    End Try

                End Using

            End Using


        End Sub

        Protected Sub buscarAlterarInventory(ByVal sender As Object, ByVal e As EventArgs)
            Dim id As String = inputId.Text

            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

            Using conn As New SqlConnection(strConn)
                Dim sql As String = "SELECT * FROM Equipamento WHERE ID = @ID"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@ID", id)

                    Try
                        conn.Open()
                        Using reader As SqlDataReader = cmd.ExecuteReader()
                            If reader.Read() Then
                                inputUsuario.Text = reader("usuario").ToString()
                                inputObserv.Text = reader("observacao").ToString()
                                inputHost.Text = reader("hostname").ToString()
                                inputEtiqueta.Text = reader("etiqueta").ToString()
                                inputConserto.Text = reader("conserto_OS").ToString()
                                inputDataEnvio.Text = reader("data_Envio").ToString()
                                inputDataDevolucao.Text = reader("data_Devolucao").ToString()
                                inputStatus.Text = reader("status").ToString()
                                inputModelo.Text = reader("modelo").ToString()
                                inputProcessador.Text = reader("processador").ToString()
                                inputMemoriaRam.Text = reader("memoria_RAM").ToString()
                                inputMemoria.Text = reader("memoria").ToString()
                                inputFabricante.Text = reader("fabricante").ToString()
                                inputTermo.Text = reader("termo").ToString()
                                inputDepartamento.Text = reader("departamento").ToString()
                                inputNoteOuDesk.Text = reader("note_Ou_Desk").ToString()
                                inputConexao.Text = reader("conexao_Rede").ToString()
                                inputUltimoDono.Text = reader("ultimo_Dono").ToString()
                                BindGridView1Data()
                            Else

                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Usuário não encontrado.');", True)
                            End If
                        End Using
                    Catch ex As Exception
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Erro ao buscar o equipamento: " & ex.Message & "');", True)
                    Finally
                        conn.Close()
                    End Try
                End Using
            End Using

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowModalScript", "abrirModal();", True)
        End Sub


        Protected Sub AlterInventory(ByVal sender As Object, ByVal e As EventArgs)
            Dim id As String = inputId.Text
            Dim strConn As String = ConfigurationManager.AppSettings("ConexaoPrincipal")

            Using conn As New SqlConnection(strConn)
                Dim sql As String = "UPDATE Equipamento SET usuario = @usuario, observacao = @observacao, hostname = @hostname, etiqueta = @etiqueta, conserto_OS = @conserto_OS, data_Envio = @data_Envio, data_Devolucao = @data_Devolucao, status = @status, modelo = @modelo, processador = @processador, memoria_RAM = @memoria_RAM, memoria = @memoria, fabricante = @fabricante, termo = @termo, departamento = @departamento, note_Ou_Desk = @note_Ou_Desk, conexao_Rede = @conexao_Rede, ultimo_Dono = @ultimo_Dono WHERE ID = @ID"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@ID", id)
                    cmd.Parameters.AddWithValue("@usuario", inputUsuario.Text)
                    cmd.Parameters.AddWithValue("@observacao", inputObserv.Text)
                    cmd.Parameters.AddWithValue("@hostname", inputHost.Text)
                    cmd.Parameters.AddWithValue("@etiqueta", inputEtiqueta.Text)
                    cmd.Parameters.AddWithValue("@conserto_OS", inputConserto.Text)
                    cmd.Parameters.AddWithValue("@data_Envio", inputDataEnvio.Text)
                    cmd.Parameters.AddWithValue("@data_Devolucao", inputDataDevolucao.Text)
                    cmd.Parameters.AddWithValue("@status", inputStatus.Text)
                    cmd.Parameters.AddWithValue("@modelo", inputModelo.Text)
                    cmd.Parameters.AddWithValue("@processador", inputProcessador.Text)
                    cmd.Parameters.AddWithValue("@memoria_RAM", inputMemoriaRam.Text)
                    cmd.Parameters.AddWithValue("@memoria", inputMemoria.Text)
                    cmd.Parameters.AddWithValue("@fabricante", inputFabricante.Text)
                    cmd.Parameters.AddWithValue("@termo", inputTermo.Text)
                    cmd.Parameters.AddWithValue("@departamento", inputDepartamento.Text)
                    cmd.Parameters.AddWithValue("@note_Ou_Desk", inputNoteOuDesk.Text)
                    cmd.Parameters.AddWithValue("@conexao_Rede", inputConexao.Text)
                    cmd.Parameters.AddWithValue("@ultimo_Dono", inputUltimoDono.Text)

                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()
                        BindGridView1Data()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Sucesso", "alert('Equipamento atualizado com sucesso!');", True)
                    Catch ex As Exception
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Erro ao atualizar o equipamento: " & ex.Message & "');", True)
                    Finally
                        conn.Close()
                    End Try
                End Using
            End Using
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

        Protected Sub BindGridView1Data()
            GridViewRiscoAnalitico.DataSource = inventarioLoad()
            GridViewRiscoAnalitico.DataBind()
            GridViewRiscoAnalitico.AllowPaging = "True"
        End Sub

        Protected Sub BindGridView1DataView()
            GridViewRiscoAnalitico.DataSource = inventarioLoad()

            GridViewRiscoAnalitico.DataBind()
            GridViewRiscoAnalitico.AllowPaging = "True"


        End Sub

        Protected Sub GridViewRiscoAnalitico_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

            GridViewRiscoAnalitico.PageIndex = IIf(e.NewPageIndex < 0, 0, e.NewPageIndex)
            BindGridView1DataView()
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
            Try
                If Not IsNothing(objGrid.HeaderRow) Then
                    Response.Clear()
                    Response.Buffer = True
                    Dim filename As String = String.Format("Inventario.xls")
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

                        Response.Write("Cnab - Arquivo de Baixa </p> ")

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