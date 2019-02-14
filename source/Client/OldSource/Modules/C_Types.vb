Module C_Types2

    ' client-side stuff


    Friend Chat As New List(Of ChatRec)

    'Mapreport
    Friend MapNames(MAX_MAPS) As String

    Friend Structure ChatRec
        Dim Text As String
        Dim Color As Integer
        Dim Y As Byte
    End Structure

    Friend Structure SkillAnim
        Dim Skillnum As Integer
        Dim Timer As Integer
        Dim FramePointer As Integer
    End Structure

    Friend Structure ChatBubbleRec
        Dim Msg As String
        Dim Colour As Integer
        Dim Target As Integer
        Dim TargetType As Byte
        Dim Timer As Integer
        Dim Active As Boolean
    End Structure

    Friend Structure MapResourceRec
        Dim X As Integer
        Dim Y As Integer
        Dim ResourceState As Byte
    End Structure



End Module