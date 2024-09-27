Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Status = "301 Moved Permanently"
        Response.AddHeader("Location", "Paginas/Logon.aspx")
    End Sub

End Class