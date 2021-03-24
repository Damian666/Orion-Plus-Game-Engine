Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports Asfw

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

        While File.Exists(Path.Graphics & "Resources\" & i & GfxExt)
            NumResources = NumResources + 1
            i = i + 1
        End While

        If NumResources = 0 Then Exit Sub
    End Sub

    Sub ClearResource(index As Integer)
        Resource(index) = Nothing
        Resource(index) = New ResourceStruct With {
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
        Resource(resourceNum).EmptyMessage = buffer.ReadString().Trim
        Resource(resourceNum).ExhaustedImage = buffer.ReadInt32()
        Resource(resourceNum).Health = buffer.ReadInt32()
        Resource(resourceNum).ExpReward = buffer.ReadInt32()
        Resource(resourceNum).ItemReward = buffer.ReadInt32()
        Resource(resourceNum).Name = buffer.ReadString().Trim
        Resource(resourceNum).ResourceImage = buffer.ReadInt32()
        Resource(resourceNum).ResourceType = buffer.ReadInt32()
        Resource(resourceNum).RespawnTime = buffer.ReadInt32()
        Resource(resourceNum).SuccessMessage = buffer.ReadString().Trim
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

#Region "Drawing"

    Friend Sub DrawResource(resource As Integer, dx As Integer, dy As Integer, rec As Rectangle)
        If resource < 1 OrElse resource > NumResources Then Exit Sub
        Dim x As Integer
        Dim y As Integer
        Dim width As Integer
        Dim height As Integer

        x = ConvertMapX(dx)
        y = ConvertMapY(dy)
        width = (rec.Right - rec.Left)
        height = (rec.Bottom - rec.Top)

        If rec.Width < 0 OrElse rec.Height < 0 Then Exit Sub

        If ResourcesGfxInfo(resource).IsLoaded = False Then
            LoadTexture(resource, 5)
        End If

        'seeying we still use it, lets update timer
        With ResourcesGfxInfo(resource)
            .TextureTimer = GetTickCount() + 100000
        End With

        RenderSprite(ResourcesSprite(resource), GameWindow, x, y, rec.X, rec.Y, rec.Width, rec.Height)

    End Sub

    Friend Sub DrawMapResource(resourceNum As Integer)
        Dim resourceMaster As Integer

        Dim resourceState As Integer
        Dim resourceSprite As Integer
        Dim rec As Rectangle
        Dim x As Integer, y As Integer

        If GettingMap Then Exit Sub
        If MapData = False Then Exit Sub

        If MapResource(resourceNum).X > Map.MaxX OrElse MapResource(resourceNum).Y > Map.MaxY Then Exit Sub

        ' Get the Resource type
        resourceMaster = Map.Tile(MapResource(resourceNum).X, MapResource(resourceNum).Y).Data1

        If resourceMaster = 0 Then Exit Sub

        If Resource(resourceMaster).ResourceImage = 0 Then Exit Sub

        ' Get the Resource state
        resourceState = MapResource(resourceNum).ResourceState

        If resourceState = 0 Then ' normal
            resourceSprite = Resource(resourceMaster).ResourceImage
        ElseIf resourceState = 1 Then ' used
            resourceSprite = Resource(resourceMaster).ExhaustedImage
        End If

        ' src rect
        With rec
            .Y = 0
            .Height = ResourcesGfxInfo(resourceSprite).Height
            .X = 0
            .Width = ResourcesGfxInfo(resourceSprite).Width
        End With

        ' Set base x + y, then the offset due to size
        x = (MapResource(resourceNum).X * PicX) - (ResourcesGfxInfo(resourceSprite).Width / 2) + 16
        y = (MapResource(resourceNum).Y * PicY) - ResourcesGfxInfo(resourceSprite).Height + 32

        DrawResource(resourceSprite, x, y, rec)
    End Sub

#End Region

End Module