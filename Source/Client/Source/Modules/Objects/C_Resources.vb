Imports ASFW
Imports System.IO
Imports System.Windows.Forms

Module C_Resources
#Region "Globals & Types"
    ' Cache the Resources in an array
    Friend MapResource() As MapResourceRec
    Friend ResourceIndex As Integer
    Friend ResourcesInit As Boolean
#End Region

#Region "DataBase"
    Friend Sub CheckResources()
        Dim i As Integer
        i = 1

        While File.Exists(Application.StartupPath & GfxPath & "Resources\" & i & GfxExt)
            NumResources = NumResources + 1
            i = i + 1
        End While

        If NumResources = 0 Then Exit Sub
    End Sub

    Friend Sub ClearChanged_Resource()
        For i = 1 To MAX_RESOURCES
            ResourceChanged(i) = Nothing
        Next

        ReDim ResourceChanged(MAX_RESOURCES)
    End Sub

    Sub ClearResource(index As Integer)
        Resource(index) = Nothing
        Resource(index) = New ResourceRec With {
            .Name = ""
        }
    End Sub

    Sub ClearResources()
        Dim i As Integer

        For i = 1 To MAX_RESOURCES
            ClearResource(i)
        Next

    End Sub
#End Region

#Region "Incoming Packets"
    Sub Packet_ResourceCache(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)
        ResourceIndex = buffer.ReadInt32
        ResourcesInit = False

        If ResourceIndex > 0 Then
            ReDim Preserve MapResource(ResourceIndex)

            For i = 0 To ResourceIndex
                MapResource(i).ResourceState = buffer.ReadInt32
                MapResource(i).X = buffer.ReadInt32
                MapResource(i).Y = buffer.ReadInt32
            Next

            ResourcesInit = True
        Else
            ReDim MapResource(1)
        End If

        buffer.Dispose()
    End Sub

    Sub Packet_UpdateResource(ByRef data() As Byte)
        Dim resourceNum As Integer
        Dim buffer As New ByteStream(data)
        resourceNum = buffer.ReadInt32

        Resource(resourceNum).Animation = buffer.ReadInt32()
        Resource(resourceNum).EmptyMessage = Trim(buffer.ReadString())
        Resource(resourceNum).ExhaustedImage = buffer.ReadInt32()
        Resource(resourceNum).Health = buffer.ReadInt32()
        Resource(resourceNum).ExpReward = buffer.ReadInt32()
        Resource(resourceNum).ItemReward = buffer.ReadInt32()
        Resource(resourceNum).Name = Trim(buffer.ReadString())
        Resource(resourceNum).ResourceImage = buffer.ReadInt32()
        Resource(resourceNum).ResourceType = buffer.ReadInt32()
        Resource(resourceNum).RespawnTime = buffer.ReadInt32()
        Resource(resourceNum).SuccessMessage = Trim(buffer.ReadString())
        Resource(resourceNum).LvlRequired = buffer.ReadInt32()
        Resource(resourceNum).ToolRequired = buffer.ReadInt32()
        Resource(resourceNum).Walkthrough = buffer.ReadInt32()

        If Resource(resourceNum).Name Is Nothing Then Resource(resourceNum).Name = ""
        If Resource(resourceNum).EmptyMessage Is Nothing Then Resource(resourceNum).EmptyMessage = ""
        If Resource(resourceNum).SuccessMessage Is Nothing Then Resource(resourceNum).SuccessMessage = ""

        buffer.Dispose()
    End Sub
#End Region

#Region "Outgoing Packets"
    Sub SendRequestResources()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestResources)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub
#End Region
End Module
