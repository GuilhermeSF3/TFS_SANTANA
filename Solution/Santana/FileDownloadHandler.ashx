<%@ WebHandler Language="VB" Class="FileDownloadHandler" %>

Imports System
Imports System.Web
Imports System.IO

Public Class FileDownloadHandler : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim filePath As String = context.Request.QueryString("filePath")
        If Not String.IsNullOrEmpty(filePath) AndAlso File.Exists(filePath) Then
            Dim fileName As String = Path.GetFileName(filePath)
            context.Response.ContentType = "application/octet-stream"
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" & fileName)
            context.Response.WriteFile(filePath)
            context.Response.End()
        Else
            context.Response.StatusCode = 404
            context.Response.Write("Arquivo não encontrado.")
        End If
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class