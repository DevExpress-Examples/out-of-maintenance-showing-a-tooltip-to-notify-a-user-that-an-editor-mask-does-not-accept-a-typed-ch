Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace T223817
    Partial Public Class MainForm
        Inherits Form

        Public Sub New()
            InitializeComponent()
            For i As Integer = 0 To 9
                bindingSource1.Add(New DataItem() With {.Count = i})
            Next i
        End Sub

        Private Sub repositoryItemCustomEdit1_InvalidCharacter(ByVal sender As Object, ByVal e As InvalidCharacterEventArgs) Handles repositoryItemCustomEdit1.InvalidCharacter
            ' if you need to show a custom tooltip, do this in this event and set the e.Handled property to True to prevent the default tooltip
        End Sub
    End Class

    Public Class DataItem
        Public Property Count() As Integer
        Public Sub New()
        End Sub
    End Class
End Namespace
