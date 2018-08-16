Imports System.IO
Imports System.Xml

Class Language
    Private ReadOnly _loaded As Boolean = False
    Private ReadOnly _loadedStrings As New Dictionary(Of String, Dictionary(Of String, String))()
    Friend Sub New(filename As String)
        If File.Exists(filename) Then
            Dim xmlDoc As New XmlDocument()
            ' Create an XML document object
            xmlDoc.Load(filename)
            ' Load the XML document from the specified file
            Dim nodes As XmlNodeList = xmlDoc.SelectNodes("//Strings").Item(0).ChildNodes
            For Each node As XmlNode In nodes
                If Not node.NodeType = XmlNodeType.Comment Then
                    If Not _loadedStrings.ContainsKey(node.Name.ToLower()) Then
                        _loadedStrings.Add(node.Name.ToLower(), New Dictionary(Of String, String)())
                    End If
                    For Each childNode As XmlNode In node.ChildNodes
                        If Not childNode.NodeType = XmlNodeType.Comment Then
                            If Not _loadedStrings(node.Name.ToLower()).ContainsKey(childNode.Attributes("id").Value.ToLower()) Then
                                _loadedStrings(node.Name.ToLower()).Add(childNode.Attributes("id").Value.ToLower(), childNode.FirstChild.Value)
                            End If
                        End If
                    Next
                End If
            Next
            'Try to load it into dictionaries.
            _loaded = True
        End If
    End Sub

    Friend Function Loaded() As Boolean
        Return _loaded
    End Function

    Friend Function HasString(section As String, id As String) As Boolean
        If _loadedStrings.ContainsKey(section.ToLower()) Then
            If _loadedStrings(section.ToLower()).ContainsKey(id.ToLower()) Then
                Return True
            End If
        End If
        Return False
    End Function

    Friend Function GetString(section As String, id As String, ParamArray args As Object()) As String
        Try
            Return String.Format(_loadedStrings(section.ToLower())(id.ToLower()), args)
        Catch generatedExceptionName As FormatException
            Return "Format Exception!"
        End Try
    End Function
End Class