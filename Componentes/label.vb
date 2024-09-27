Public Class label
    Inherits Web.UI.WebControls.Label



    Private Sub label_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Init
        Me.Font.Bold = False
        Me.Font.Names = Split("Verdana,Arial,Helvetica,sans-serif", ",")
        Me.Font.Size = WebControls.FontUnit.Point(8)
        Me.ForeColor = Drawing.Color.Black
    End Sub

    Private Sub label_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '# tratamento de trducao
    End Sub
End Class


