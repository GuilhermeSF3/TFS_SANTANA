Public Class GridViewPaging
    Inherits System.Web.UI.UserControl

    Public pagingClickArgs As EventHandler

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            trErrorMessage.Visible = False
            If Not IsPostBack Then
                SelectedPageNo.Text = "1"
                GetPageDisplaySummary()
            End If
        Catch ex As Exception
            ShowGridViewPagingErrorMessage(ex.Message.ToString())
        End Try
    End Sub


    Protected Sub First_Click(sender As Object, e As EventArgs)
        Try
            If Not IsValid() Then
                Return
            End If


            SelectedPageNo.Text = "1"
            GetPageDisplaySummary()
            pagingClickArgs(sender, e)
        Catch ex As Exception
            ShowGridViewPagingErrorMessage(ex.Message.ToString())
        End Try
    End Sub
    Protected Sub Previous_Click(sender As Object, e As EventArgs)
        Try
            If Not IsValid() Then
                Return
            End If


            If Convert.ToInt32(SelectedPageNo.Text) > 1 Then
                SelectedPageNo.Text = (Convert.ToInt32(SelectedPageNo.Text) - 1).ToString()
            End If
            GetPageDisplaySummary()
            pagingClickArgs(sender, e)
        Catch ex As Exception
            ShowGridViewPagingErrorMessage(ex.Message.ToString())
        End Try
    End Sub
    Protected Sub SelectedPageNo_TextChanged(sender As Object, e As EventArgs)
        Try
            If Not IsValid() Then
                Return
            End If


            Dim currentPageNo As Integer = Convert.ToInt32(SelectedPageNo.Text)
            If currentPageNo < GetTotalPagesCount() Then
                SelectedPageNo.Text = (currentPageNo).ToString()
            End If
            GetPageDisplaySummary()
            pagingClickArgs(sender, e)
        Catch ex As Exception
            ShowGridViewPagingErrorMessage(ex.Message.ToString())
        End Try
    End Sub
    Protected Sub Next_Click(sender As Object, e As EventArgs)
        Try
            If Not IsValid() Then
                Return
            End If


            Dim currentPageNo As Integer = Convert.ToInt32(SelectedPageNo.Text)
            If currentPageNo < GetTotalPagesCount() Then
                SelectedPageNo.Text = (currentPageNo + 1).ToString()
            End If
            GetPageDisplaySummary()
            pagingClickArgs(sender, e)
        Catch ex As Exception
            ShowGridViewPagingErrorMessage(ex.Message.ToString())
        End Try
    End Sub
    Protected Sub Last_Click(sender As Object, e As EventArgs)
        Try
            If Not IsValid() Then
                Return
            End If


            SelectedPageNo.Text = GetTotalPagesCount().ToString()
            GetPageDisplaySummary()
            pagingClickArgs(sender, e)
        Catch ex As Exception
            ShowGridViewPagingErrorMessage(ex.Message.ToString())
        End Try
    End Sub
    Private Function GetTotalPagesCount() As Integer
        Try
            Dim totalPages As Integer = Convert.ToInt32(TotalRows.Value) / Convert.ToInt32(PageRowSize.SelectedValue)
            ' total page item to be displyed
            Dim pageItemRemain As Integer = Convert.ToInt32(TotalRows.Value) Mod Convert.ToInt32(PageRowSize.SelectedValue)
            ' remaing no of pages
            If pageItemRemain > 0 Then
                ' set total No of pages
                totalPages = totalPages + 1
            Else
                totalPages = totalPages + 0
            End If
            Return totalPages
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub GetPageDisplaySummary()
        Try
            PageDisplaySummary.Text = "Page " & SelectedPageNo.Text.ToString() & " of " & GetTotalPagesCount().ToString()
            Dim startRow As Integer = (Convert.ToInt32(PageRowSize.SelectedValue) * (Convert.ToInt32(SelectedPageNo.Text) - 1)) + 1
            Dim endRow As Integer = Convert.ToInt32(PageRowSize.SelectedValue) * Convert.ToInt32(SelectedPageNo.Text)
            Dim totalRecords As Integer = Convert.ToInt32(TotalRows.Value)
            If totalRecords >= endRow Then
                RecordDisplaySummary.Text = "Records: " & startRow.ToString() & " - " & endRow.ToString() & " of " & totalRecords.ToString()
            Else
                RecordDisplaySummary.Text = "Records: " & startRow.ToString() & " - " & totalRecords.ToString() & " of " & totalRecords.ToString()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function IsValid() As Boolean
        Try
            If [String].IsNullOrEmpty(SelectedPageNo.Text.Trim()) OrElse (SelectedPageNo.Text = "0") Then
                SelectedPageNo.Text = "1"
                Return False
            ElseIf Not IsNumeric(SelectedPageNo.Text) Then
                ShowGridViewPagingErrorMessage("Please Insert Valid Page No.")
                Return False
            Else
                Return True
            End If
        Catch generatedExceptionName As FormatException
            Return False
        End Try
    End Function
    Private Function IsNumeric(PageNo As String) As Boolean
        Try
            Dim i As Integer = Convert.ToInt32(PageNo)
            Return True
        Catch generatedExceptionName As FormatException
            Return False
        End Try
    End Function
    Private Sub ShowGridViewPagingErrorMessage(msg As String)
        trErrorMessage.Visible = True
        GridViewPagingError.Text = "Error: " & msg
    End Sub










End Class