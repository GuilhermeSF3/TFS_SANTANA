Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Collections

Imports Componentes.HierarcGrid

Namespace HierarcGrid
    ''' <summary>
    ''' The HierarColumn is derived from the DataGridColumn and contains an image with a plus/minus icon 
    ''' and a DynamicControlsPlaceholder that takes the dynamically loaded templates
    ''' </summary>
    Public Class HierarColumn
        Inherits DataGridColumn
        ''' <summary>
        ''' Initializes a new instance of HierarColumn class.
        ''' </summary>
        Public Sub New()
            MyBase.New()
        End Sub

        ''' <summary>
        ''' On initialization the HierarGridColumn adds a plus image and a DynamicControlsPlaceholder 
        ''' that is later filled with the templates
        ''' </summary>
        ''' <param name="cell"></param>
        ''' <param name="columnIndex"></param>
        ''' <param name="itemType"></param>
        Public Overrides Sub InitializeCell(ByVal cell As TableCell, ByVal columnIndex As Integer, ByVal itemType As ListItemType)
            Dim divCssClass As String = [String].Empty

            MyBase.InitializeCell(cell, columnIndex, itemType)

            Select Case itemType
                Case ListItemType.Item, ListItemType.AlternatingItem, ListItemType.SelectedItem
                    If True Then
                        AddControls(cell, itemType)
                        Exit Select
                    End If
                Case ListItemType.EditItem
                    Exit Select
            End Select
        End Sub

        ''' <summary>
        ''' Adds a plus image and a DynamicControlsPlaceholder to the child collection
        ''' </summary>
        ''' <param name="cell"></param>
        ''' <param name="itemType"></param>
        Protected Overridable Sub AddControls(ByVal cell As TableCell, ByVal itemType As ListItemType)
            Dim image As New Image()
            image.ID = "Icon"
            image.ImageUrl = "~/images/plus.gif"
            image.Attributes.Add("onClick", "javascript:HierarGrid_toggleRow(this);")
            cell.Controls.Add(image)

            Dim dcp As New DynamicControlsPlaceholder()
            dcp.ID = "DCP"
            cell.Controls.Add(dcp)
        End Sub
    End Class
End Namespace

