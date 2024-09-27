Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Elmah


Namespace Seguranca
    Public Class SantanaPage
        Inherits System.Web.UI.Page

        Private m_ContextoWeb As Seguranca.Contexto

        Public Sub New()
            m_ContextoWeb = New Seguranca.Contexto()
        End Sub

        Protected Overrides Sub OnLoad(e As EventArgs)
            MyBase.OnLoad(e)
        End Sub

        Public Overridable Property ContextoWeb() As Contexto
            Get
                Return m_ContextoWeb
            End Get
            Set(value As Contexto)
                m_ContextoWeb = value
            End Set
        End Property



    End Class
End Namespace