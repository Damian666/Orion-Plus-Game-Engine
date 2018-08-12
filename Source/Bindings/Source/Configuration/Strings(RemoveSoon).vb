Imports System
Imports System.IO

Friend NotInheritable Class Strings
    Private Sub New()
    End Sub

    Friend Enum OrionComponent
        Client = 0
        Editor
        Server
    End Enum

    Private Shared _defaultLanguage As Language
    Private Shared _selectedLanguage As Language

    Friend Shared Sub Init(component As OrionComponent, language As String)
        Dim path As String = Environment.CurrentDirectory & "\Data\"
        If Not Directory.Exists(path) Then Directory.CreateDirectory(path)
        path += "Languages\"
        If Not Directory.Exists(path) Then Directory.CreateDirectory(path)

        _defaultLanguage = New Language(path & component.ToString() & "_English.xml")
        Dim fPath As String = path & component.ToString() & "_" & language & ".xml"
        If File.Exists(fPath) Then _selectedLanguage = New Language(fPath)
    End Sub

    Friend Shared Function [Get](section As String, id As String, ParamArray args As Object()) As String
        If _selectedLanguage IsNot Nothing AndAlso _selectedLanguage.Loaded() AndAlso _selectedLanguage.HasString(section, id) Then
            Return _selectedLanguage.GetString(section, id, args)
        End If
        If _defaultLanguage IsNot Nothing AndAlso _defaultLanguage.Loaded() AndAlso _defaultLanguage.HasString(section, id) Then
            Return _defaultLanguage.GetString(section, id, args)
        End If
        Return "Not Found"
    End Function
End Class