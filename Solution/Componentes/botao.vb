Public Class botao
    Inherits Web.UI.WebControls.Button


    Private Sub botao_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Init
        Me.Font.Bold = False
        Me.Font.Names = Split("Verdana,Arial,Helvetica,sans-serif", ",")
        Me.Font.Size = WebControls.FontUnit.Point(8)
        Me.BorderStyle = Web.UI.WebControls.BorderStyle.Solid
        Me.BorderWidth = New Web.UI.WebControls.Unit(1)
        Me.BackColor = Drawing.Color.FromName("#DAEAF9")
        Me.BorderColor = Drawing.Color.FromName("#1887E6")
        Me.ForeColor = Drawing.Color.FromName("#004C8C")
    End Sub
End Class
