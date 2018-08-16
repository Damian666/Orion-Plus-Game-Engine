Imports System.Xml
Imports System.IO
Imports System.Text

Friend Class XmlClass
    Private xmlDoc As New XmlDocument()

    Friend Property Root As String = "Settings"
    Friend Property Filename As String = vbNullString

    Sub NewXmlDocument()

        Dim xmlTextWrite As New XmlTextWriter(Filename, Encoding.UTF8)

        'Write blank xml document.
        With xmlTextWrite
            .WriteStartDocument(True)
            .WriteStartElement(Root)
            .WriteEndElement()
            .WriteEndDocument()
            .Flush()
            .Close()
        End With

    End Sub

    Friend Sub WriteString(Selection As String, name As String, value As String)
        'Dim xmlDoc As New XmlDocument()

        'Check if xml filename is here.
        If Not File.Exists(Filename) Then
            'Create new blank xml file.
            NewXmlDocument()
        End If

        'Load xml document.
        'xmlDoc.Load(Me.Filename)

        'Check for settings selection.
        If xmlDoc.SelectSingleNode(Root & "/" & Selection) Is Nothing Then
            'Create selection.
            xmlDoc.DocumentElement.AppendChild(DirectCast(xmlDoc.CreateElement(Selection), XmlNode))
        End If

        'Check for element node
        Dim xmlNode As XmlNode = xmlDoc.SelectSingleNode(Root & "/" & Selection & "/Element[@Name='" & Name & "']")

        If xmlNode Is Nothing Then
            Dim element As XmlElement = xmlDoc.CreateElement("Element")
            'Write new element values.
            element.SetAttribute("Name", Name)
            element.SetAttribute("Value", Value)
            'Add new node.
            xmlDoc.DocumentElement(Selection).AppendChild(DirectCast(element, XmlNode))
        Else
            'Update node values.
            xmlNode.Attributes("Name").Value = Name
            xmlNode.Attributes("Value").Value = Value
        End If
        'Save xml data.

        'xmlDoc.Save(Me.Filename)

        'xmlDoc = Nothing
    End Sub

    Friend Function ReadString(Selection As String, name As String, Optional defaultValue As String = "") As String
        'Dim xmlDoc As New XmlDocument()

        If Not File.Exists(Filename) Then
            Return DefaultValue
        Else
            'Load xml document.
            'xmlDoc.Load(Filename)
            'Read node value.
            Dim XmlNode = xmlDoc.SelectSingleNode(Root & "/" & Selection & "/Element[@Name='" & Name & "']")

            'Check if node is here.
            If XmlNode Is Nothing Then
                Return DefaultValue
            Else
                'Return xml node value
                Return (XmlNode.Attributes("Value").Value)
                'Clean up
                'xmlDoc = Nothing

            End If
        End If
    End Function

    Friend Sub RemoveNode(Selection As String, name As String)
        'Dim xmlDoc As New XmlDocument()

        'Remove xml node
        If File.Exists(Filename) Then
            'Load xml document.
            ' xmlDoc.Load(Filename)
            'Read node value.
            Dim XmlNode = xmlDoc.SelectSingleNode(Root & "/" & Selection & "/Element[@Name='" & Name & "']")
            'Check if node is here.
            If Not XmlNode Is Nothing Then
                xmlDoc.SelectSingleNode(Root & "/" & Selection).RemoveChild(XmlNode)
                ''Update xml document.
                'xmlDoc.Save(Filename)
            End If
        End If
    End Sub

    Friend Sub LoadXml()
        'Load xml document.
        xmlDoc.Load(Filename)
    End Sub

    Friend Sub CloseXml(Save As Boolean)
        If Save Then
            'Update xml document.
            xmlDoc.Save(Filename)
        End If
        'Clean up
        xmlDoc = Nothing
    End Sub
End Class


