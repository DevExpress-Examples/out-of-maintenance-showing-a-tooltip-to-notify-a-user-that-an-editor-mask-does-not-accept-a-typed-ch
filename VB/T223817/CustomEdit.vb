Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.XtraEditors.Registrator
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraEditors.Popup
Imports System.Globalization
Imports DevExpress.Data.Mask

Namespace T223817
    <UserRepositoryItem("RegisterCustomEdit")> _
    Public Class RepositoryItemCustomEdit
        Inherits RepositoryItemTextEdit

        Private Shared ReadOnly _invalidCharacter As New Object()
        Shared Sub New()
            RegisterCustomEdit()
        End Sub

        Public Const CustomEditName As String = "CustomEdit"
        Public Sub New()
        End Sub

        Public Overrides ReadOnly Property EditorTypeName() As String
            Get
                Return CustomEditName
            End Get
        End Property

        Public Shared Sub RegisterCustomEdit()
            Dim img As Image = Nothing
            EditorRegistrationInfo.Default.Editors.Add(New EditorClassInfo(CustomEditName, GetType(CustomEdit), GetType(RepositoryItemCustomEdit), GetType(TextEditViewInfo), New TextEditPainter(), True, img))
        End Sub

        Public Overrides Sub Assign(ByVal item As RepositoryItem)
            Dim source As RepositoryItemCustomEdit = TryCast(item, RepositoryItemCustomEdit)
            BeginUpdate()
            Try
                MyBase.Assign(item)
                If source Is Nothing Then
                    Return
                End If
            Finally
                EndUpdate()
            End Try
            Events.AddHandler(_invalidCharacter, source.Events(_invalidCharacter))
        End Sub

        Public Custom Event InvalidCharacter As InvalidCharacterEventHandler
            AddHandler(ByVal value As InvalidCharacterEventHandler)
                Me.Events.AddHandler(_invalidCharacter, value)
            End AddHandler
            RemoveHandler(ByVal value As InvalidCharacterEventHandler)
                Me.Events.RemoveHandler(_invalidCharacter, value)
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As InvalidCharacterEventArgs)
            End RaiseEvent
        End Event

        Protected Friend Overridable Sub RaiseInvalidCharacter(ByVal e As InvalidCharacterEventArgs)
            Dim handler As InvalidCharacterEventHandler = CType(Me.Events(_invalidCharacter), InvalidCharacterEventHandler)
            If handler IsNot Nothing Then
                handler(GetEventSender(), e)
            End If
        End Sub
    End Class

    Public Delegate Sub InvalidCharacterEventHandler(ByVal sender As Object, ByVal e As InvalidCharacterEventArgs)
    Public Class InvalidCharacterEventArgs
        Inherits EventArgs

        Private privateInsertionString As String
        Public Property InsertionString() As String
            Get
                Return privateInsertionString
            End Get
            Private Set(ByVal value As String)
                privateInsertionString = value
            End Set
        End Property
        Public Property Handled() As Boolean
        Public Sub New(ByVal insertionString As String)
            Me.InsertionString = insertionString
        End Sub
    End Class

    <ToolboxItem(True)> _
    Public Class CustomEdit
        Inherits TextEdit

        Shared Sub New()
            RepositoryItemCustomEdit.RegisterCustomEdit()
        End Sub

        Public Sub New()
        End Sub

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        Public Shadows ReadOnly Property Properties() As RepositoryItemCustomEdit
            Get
                Return TryCast(MyBase.Properties, RepositoryItemCustomEdit)
            End Get
        End Property

        Public Overrides ReadOnly Property EditorTypeName() As String
            Get
                Return RepositoryItemCustomEdit.CustomEditName
            End Get
        End Property

        Private fManager As NumericMaskManager
        Protected Overrides Function CreateMaskManager(ByVal mask As DevExpress.XtraEditors.Mask.MaskProperties) As MaskManager
            fManager = CType(MyBase.CreateMaskManager(mask), NumericMaskManager)
            Return fManager
        End Function

        Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
            MyBase.OnKeyPress(e)
            Dim insertion As String = KeyCharToInsertableString(e.KeyChar)
            If insertion Is Nothing Then
                Return
            End If
            If fManager.Insert(insertion) Then
                fManager.Backspace()
                Return
            End If
            ProcessInvalidCharacter(insertion)
        End Sub

        Private Sub ProcessInvalidCharacter(ByVal insertion As String)
            Dim args As New InvalidCharacterEventArgs(insertion)
            Me.Properties.RaiseInvalidCharacter(args)
            If args.Handled Then
                Return
            End If
            Me.ToolTipController.ShowBeak = True
            Me.ToolTipController.ShowHint("An editor can accept only numbers or maximum length achieved", Me.PointToScreen(Point.Empty))
        End Sub

        Private Function KeyCharToInsertableString(ByVal keyChar As Char) As String
            If Not Char.IsControl(keyChar) Then
                Return keyChar.ToString(CultureInfo.InvariantCulture)
            End If
            If keyChar = ControlChars.Cr AndAlso Me.MaskBox.Multiline AndAlso Me.MaskBox.AcceptsReturn Then
                Return ControlChars.CrLf
            End If
            If keyChar = ControlChars.Tab AndAlso Me.MaskBox.Multiline AndAlso Me.MaskBox.AcceptsTab Then
                Return ControlChars.Tab
            End If
            Return Nothing
        End Function
    End Class
End Namespace
