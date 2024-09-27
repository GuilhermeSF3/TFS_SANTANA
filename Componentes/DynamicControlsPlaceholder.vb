Imports System.Collections
Imports System.Reflection
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.ComponentModel

Namespace HierarcGrid
    ''' <summary>
    ''' DynamicControlsPlaceholder solves the problem that dynamically added controls are not automatically recreated on subsequent requests.
    ''' The control uses the ViewState to store the types of the child controls recursively and recreates them automatically.
    ''' 
    ''' Please note that property values that are set before "TrackViewState" is called (usually in Controls.Add) are not persisted
    ''' </summary>
    <ControlBuilder(GetType(System.Web.UI.WebControls.PlaceHolderControlBuilder)), Designer("System.Web.UI.Design.ControlDesigner"), DefaultProperty("ID"), ToolboxData("<{0}:DynamicControlsPlaceholder runat=server></{0}:DynamicControlsPlaceholder>")> _
    Friend Class DynamicControlsPlaceholder
        Inherits PlaceHolder
#Region "custom events"
        ''' <summary>
        ''' Occurs when a control has been restored from ViewState
        ''' </summary>
        Public Event ControlRestored As DynamicControlEventHandler
        ''' <summary>
        ''' Occurs when the DynamicControlsPlaceholder is about to restore the child controls from ViewState
        ''' </summary>
        Public Event PreRestore As EventHandler
        ''' <summary>
        ''' Occurs after the DynamicControlsPlaceholder has restored the child controls from ViewState
        ''' </summary>
        Public Event PostRestore As EventHandler

        ''' <summary>
        ''' Raises the <see cref="ControlRestored">ControlRestored</see> event.
        ''' </summary>
        ''' <param name="e">The <see cref="DynamicControlEventArgs">DynamicControlEventArgs</see> object that contains the event data.</param>
        Protected Overridable Sub OnControlRestored(ByVal e As DynamicControlEventArgs)
            RaiseEvent ControlRestored(Me, e)
        End Sub

        ''' <summary>
        ''' Raises the <see cref="PreRestore">PreRestore</see> event.
        ''' </summary>
        ''' <param name="e">The <see cref="System.EventArgs">EventArgs</see> object that contains the event data.</param>
        Protected Overridable Sub OnPreRestore(ByVal e As EventArgs)
            RaiseEvent PreRestore(Me, e)
        End Sub

        ''' <summary>
        ''' Raises the <see cref="PostRestore">PostRestore</see> event.
        ''' </summary>
        ''' <param name="e">The <see cref="System.EventArgs">EventArgs</see> object that contains the event data.</param>
        Protected Overridable Sub OnPostRestore(ByVal e As EventArgs)
            RaiseEvent PostRestore(Me, e)
        End Sub
#End Region

#Region "custom propterties"
        ''' <summary>
        ''' Specifies whether Controls without IDs shall be persisted or if an exception shall be thrown
        ''' </summary>
        <DefaultValue(HandleDynamicControls.DontPersist)> _
        Public Property ControlsWithoutIDs() As HandleDynamicControls
            Get
                If ViewState("ControlsWithoutIDs") Is Nothing Then
                    Return HandleDynamicControls.DontPersist
                Else
                    Return CType(ViewState("ControlsWithoutIDs"), HandleDynamicControls)
                End If
            End Get
            Set(ByVal value As HandleDynamicControls)
                ViewState("ControlsWithoutIDs") = value
            End Set
        End Property
#End Region

#Region "ViewState management"
        ''' <summary>
        ''' Recreates all dynamically added child controls of the Placeholder and then calls the default 
        ''' LoadViewState mechanism
        ''' </summary>
        ''' <param name="savedState">Array of objects that contains the child structure in the first item, 
        ''' and the base ViewState in the second item</param>
        Protected Overrides Sub LoadViewState(ByVal savedState As Object)
            Dim viewState As Object() = DirectCast(savedState, Object())

            'Raise PreRestore event
            OnPreRestore(EventArgs.Empty)

            'recreate the child controls recursively
            Dim persistInfo As Pair = DirectCast(viewState(0), Pair)
            For Each pair As Pair In DirectCast(persistInfo.Second, ArrayList)
                RestoreChildStructure(pair, Me)
            Next

            'Raise PostRestore event
            OnPostRestore(EventArgs.Empty)

            MyBase.LoadViewState(viewState(1))
        End Sub

        ''' <summary>
        ''' Walks recursively through all child controls and stores their type in ViewState and then calls the default 
        ''' SaveViewState mechanism
        ''' </summary>
        ''' <returns>Array of objects that contains the child structure in the first item, 
        ''' and the base ViewState in the second item</returns>
        Protected Overrides Function SaveViewState() As Object
            If HttpContext.Current Is Nothing Then
                Return Nothing
            End If

            Dim viewState As Object() = New Object(1) {}
            viewState(0) = PersistChildStructure(Me, "C")
            viewState(1) = MyBase.SaveViewState()
            Return viewState
        End Function

        ''' <summary>
        ''' Recreates a single control and recursively calls itself for all child controls
        ''' </summary>
        ''' <param name="persistInfo">A pair that contains the controls persisted information in the first property,
        ''' and an ArrayList with the child's persisted information in the second property</param>
        ''' <param name="parent">The parent control to which Controls collection it is added</param>
        Private Sub RestoreChildStructure(ByVal persistInfo As Pair, ByVal parent As Control)
            Dim control As Control

            Dim persistedString As String() = persistInfo.First.ToString().Split(";"c)

            Dim typeName As String() = persistedString(1).Split(":"c)
            Select Case typeName(0)
                'restore the UserControl by calling Page.LoadControl
                Case "UC"
                    'when running under ASP.NET >= 2.0 load the user control based on its type
                    If Environment.Version.Major > 1 Then
                        Dim ucType As Type = Type.[GetType](typeName(1), True, True)
                        Try
                            'calling the overload Page.LoadControl(ucType, null) via reflection (which is not very nice but necessary when compiled against 1.0)
                            Dim mi As MethodInfo = GetType(Page).GetMethod("LoadControl", New Type(1) {GetType(Type), GetType(Object())})
                            control = DirectCast(mi.Invoke(Me.Page, New Object(1) {ucType, Nothing}), Control)
                        Catch e As Exception
                            Throw New ArgumentException([String].Format("The type '{0}' cannot be recreated from ViewState", ucType.ToString()), e)
                        End Try
                    Else
                        'in ASP.NET 1.0/1.1 load the user control based on the file
                        'recreate the Filename from the Typename
                        Dim ucFilename As String = typeName(2) & "/" & typeName(1).Split("."c)(1).Replace("_", ".")
                        If Not System.IO.File.Exists(Context.Server.MapPath(ucFilename)) Then
                            'original filename must have contained a "_"
                            Dim filePattern As String = typeName(1).Split("."c)(1).Replace("_", "*")
                            'due to some strange behaviour of windows you can't use the '?' wildcard to find a '.'... We'll use the * instead,
                            Dim files As String() = System.IO.Directory.GetFiles(Context.Server.MapPath(typeName(2)), filePattern)
                            If files.Length = 1 Then
                                ucFilename = typeName(2) & "/" & System.IO.Path.GetFileName(files(0))
                            Else
                                Throw New ApplicationException(String.Format("Could not load UserControl '{2}' from VRoot '{0}' with PersistenceString: {1}. Found {3} files that match the pattern {4}", Me.Context.Request.ApplicationPath, persistedString(1), ucFilename, files.Length.ToString(), Context.Server.MapPath(typeName(2)) & "\" & filePattern))
                            End If
                        End If
                        control = Page.LoadControl(ucFilename)
                    End If
                    Exit Select
                Case "C"
                    'create a new instance of the control's type
                    Dim type__1 As Type = Type.[GetType](typeName(1), True, True)
                    Try
                        control = DirectCast(Activator.CreateInstance(type__1), Control)
                    Catch e As Exception
                        Throw New ArgumentException([String].Format("The type '{0}' cannot be recreated from ViewState", type__1.ToString()), e)
                    End Try
                    Exit Select
                Case Else
                    Throw New ArgumentException("Unknown type - cannot recreate from ViewState")
            End Select

            control.ID = persistedString(2)

            Select Case persistedString(0)
                'adding control to "Controls" collection
                Case "C"
                    parent.Controls.Add(control)
                    Exit Select
            End Select

            'Raise OnControlRestoredEvent
            OnControlRestored(New DynamicControlEventArgs(control))

            'recreate all the child controls
            For Each pair As Pair In DirectCast(persistInfo.Second, ArrayList)
                RestoreChildStructure(pair, control)
            Next
        End Sub

        ''' <summary>
        ''' Saves a single control and recursively calls itself to save all child controls
        ''' </summary>
        ''' <param name="control">reference to the control</param>
        ''' <param name="controlCollectionName">contains an abbreviation to indicate to which control collection the control belongs</param>
        ''' <returns>A pair that contains the controls persisted information in the first property,
        ''' and an ArrayList with the child's persisted information in the second property</returns>
        Private Function PersistChildStructure(ByVal control As Control, ByVal controlCollectionName As String) As Pair
            Dim typeName As String
            Dim childPersistInfo As New ArrayList()

            'check if the control has an ID
            If control.ID Is Nothing Then
                If ControlsWithoutIDs = HandleDynamicControls.ThrowException Then
                    Throw New NotSupportedException("DynamicControlsPlaceholder does not support child controls whose ID is not set, as this may have unintended side effects: " & control.[GetType]().ToString())
                ElseIf ControlsWithoutIDs = HandleDynamicControls.DontPersist Then
                    Return Nothing
                End If
            End If

            If TypeOf control Is UserControl Then
                If Environment.Version.Major > 1 Then
                    typeName = "UC:" & control.[GetType]().AssemblyQualifiedName
                Else
                    'in ASP.NET >= 2.0 save the full type name
                    typeName = "UC:" & DirectCast(control, UserControl).[GetType]().ToString() & ":" & control.TemplateSourceDirectory
                    'otherwise get the directory
                End If
            Else
                typeName = "C:" & control.[GetType]().AssemblyQualifiedName
            End If

            Dim persistedString As String = controlCollectionName & ";" & typeName & ";" & control.ID

            'childs of a UserControl need not be saved as they are recreated on Page.LoadControl, same for CheckBoxList
            If Not (TypeOf control Is UserControl) AndAlso Not (TypeOf control Is CheckBoxList) Then
                'saving all child controls from "Controls" collection
                For counter As Integer = 0 To control.Controls.Count - 1
                    Dim child As Control = control.Controls(counter)
                    Dim pair As Pair = PersistChildStructure(child, "C")
                    If pair IsNot Nothing Then
                        childPersistInfo.Add(pair)
                    End If
                Next
            End If

            Return New Pair(persistedString, childPersistInfo)
        End Function
#End Region
    End Class

    ''' <summary>
    ''' Specifies the possibilities if controls shall be persisted or not
    ''' </summary>
    Friend Enum HandleDynamicControls
        ''' <summary>
        ''' DynamicControl shall not be persisted
        ''' </summary>
        DontPersist
        ''' <summary>
        ''' DynamicControl shall be persisted
        ''' </summary>
        Persist
        ''' <summary>
        ''' An Exception shall be thrown
        ''' </summary>
        ThrowException
    End Enum

    ''' <summary>
    ''' Represents the method that will handle any DynamicControl event.
    ''' </summary>
    <Serializable()> _
    Friend Delegate Sub DynamicControlEventHandler(ByVal sender As Object, ByVal e As DynamicControlEventArgs)

    ''' <summary>
    ''' Provides data for the ControlRestored event
    ''' </summary>
    Friend Class DynamicControlEventArgs
        Inherits EventArgs
        Private _dynamicControl As Control

        ''' <summary>
        ''' Gets the referenced Control when the event is raised
        ''' </summary>
        Public ReadOnly Property DynamicControl() As Control
            Get
                Return _dynamicControl
            End Get
        End Property

        ''' <summary>
        ''' Initializes a new instance of DynamicControlEventArgs class.
        ''' </summary>
        ''' <param name="dynamicControl">The control that was just restored.</param>
        Public Sub New(ByVal dynamicControl As Control)
            _dynamicControl = dynamicControl
        End Sub
    End Class
End Namespace

