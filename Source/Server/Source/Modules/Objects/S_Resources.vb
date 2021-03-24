Imports System.IO
Imports Asfw
Imports Asfw.IO
Imports Ini = Asfw.IO.TextFile

Friend Module S_Resources

#Region "Globals & Types"

    Friend SkillExpTable(100) As Integer
    Friend ResourceCache(MAX_CACHED_MAPS) As ResourceCacheRec

    Friend Structure MapResourceRec
        Dim ResourceState As Byte
        Dim ResourceTimer As Integer
        Dim X As Integer
        Dim Y As Integer
        Dim CurHealth As Byte
    End Structure

    Friend Structure ResourceCacheRec
        Dim ResourceCount As Integer
        Dim ResourceData() As MapResourceRec
    End Structure

#End Region

#Region "DataBase"

    Sub SaveResources()
        Dim i As Integer

        For i = 1 To MAX_RESOURCES
            SaveResource(i)
        Next

    End Sub

    Sub SaveResource(ResourceNum As Integer)
        Dim filename As String

        filename = Path.Resource(ResourceNum)

        Dim writer As New ByteStream(100)

        writer.WriteString(Resource(ResourceNum).Name)
        writer.WriteString(Resource(ResourceNum).SuccessMessage)
        writer.WriteString(Resource(ResourceNum).EmptyMessage)
        writer.WriteInt32(Resource(ResourceNum).ResourceType)
        writer.WriteInt32(Resource(ResourceNum).ResourceImage)
        writer.WriteInt32(Resource(ResourceNum).ExhaustedImage)
        writer.WriteInt32(Resource(ResourceNum).ExpReward)
        writer.WriteInt32(Resource(ResourceNum).ItemReward)
        writer.WriteInt32(Resource(ResourceNum).LvlRequired)
        writer.WriteInt32(Resource(ResourceNum).ToolRequired)
        writer.WriteInt32(Resource(ResourceNum).Health)
        writer.WriteInt32(Resource(ResourceNum).RespawnTime)
        writer.WriteBoolean(Resource(ResourceNum).Walkthrough)
        writer.WriteInt32(Resource(ResourceNum).Animation)

        ByteFile.Save(filename, writer)
    End Sub

    Sub LoadResources()
        Dim i As Integer

        Call CheckResources()

        For i = 1 To MAX_RESOURCES
            LoadResource(i)
            Application.DoEvents()
        Next

    End Sub

    Sub LoadResource(ResourceNum As Integer)
        Dim filename As String

        filename = Path.Resource(ResourceNum)
        Dim reader As New ByteStream()
        ByteFile.Load(filename, reader)

        Resource(ResourceNum).Name = reader.ReadString()
        Resource(ResourceNum).SuccessMessage = reader.ReadString()
        Resource(ResourceNum).EmptyMessage = reader.ReadString()
        Resource(ResourceNum).ResourceType = reader.ReadInt32()
        Resource(ResourceNum).ResourceImage = reader.ReadInt32()
        Resource(ResourceNum).ExhaustedImage = reader.ReadInt32()
        Resource(ResourceNum).ExpReward = reader.ReadInt32()
        Resource(ResourceNum).ItemReward = reader.ReadInt32()
        Resource(ResourceNum).LvlRequired = reader.ReadInt32()
        Resource(ResourceNum).ToolRequired = reader.ReadInt32()
        Resource(ResourceNum).Health = reader.ReadInt32()
        Resource(ResourceNum).RespawnTime = reader.ReadInt32()
        Resource(ResourceNum).Walkthrough = reader.ReadBoolean()
        Resource(ResourceNum).Animation = reader.ReadInt32()

        If Resource(ResourceNum).Name Is Nothing Then Resource(ResourceNum).Name = ""
        If Resource(ResourceNum).EmptyMessage Is Nothing Then Resource(ResourceNum).EmptyMessage = ""
        If Resource(ResourceNum).SuccessMessage Is Nothing Then Resource(ResourceNum).SuccessMessage = ""

    End Sub

    Sub CheckResources()
        For i = 1 To MAX_RESOURCES

            If Not File.Exists(Path.Resource(i)) Then
                SaveResource(i)
            End If

        Next

    End Sub

    Sub ClearResource(index As Integer)
        Resource(index) = Nothing
        Resource(index).Name = ""
        Resource(index).EmptyMessage = ""
        Resource(index).SuccessMessage = ""
    End Sub

    Sub ClearResources()
        For i = 1 To MAX_RESOURCES
            ClearResource(i)
        Next
    End Sub

    Friend Sub CacheResources(mapNum As Integer)
        Dim x As Integer, y As Integer, Resource_Count As Integer
        Resource_Count = 0

        For x = 0 To Map(mapNum).MaxX
            For y = 0 To Map(mapNum).MaxY

                If Map(mapNum).Tile(x, y).Type = TileType.Resource Then
                    Resource_Count += 1
                    ReDim Preserve ResourceCache(mapNum).ResourceData(Resource_Count)
                    ResourceCache(mapNum).ResourceData(Resource_Count).X = x
                    ResourceCache(mapNum).ResourceData(Resource_Count).Y = y
                    ResourceCache(mapNum).ResourceData(Resource_Count).CurHealth = Resource(Map(mapNum).Tile(x, y).Data1).Health
                End If

            Next
        Next

        ResourceCache(mapNum).ResourceCount = Resource_Count
    End Sub

    Function ResourcesData() As Byte()
        Dim buffer As New ByteStream(4)
        For i = 1 To MAX_RESOURCES
            If Not Len(Trim$(Resource(i).Name)) > 0 Then Continue For
            buffer.WriteBlock(ResourceData(i))
        Next
        Return buffer.ToArray
    End Function

    Function ResourceData(ResourceNum As Integer) As Byte()
        Dim buffer As New ByteStream(4)
        buffer.WriteInt32(ResourceNum)
        buffer.WriteInt32(Resource(ResourceNum).Animation)
        buffer.WriteString((Resource(ResourceNum).EmptyMessage))
        buffer.WriteInt32(Resource(ResourceNum).ExhaustedImage)
        buffer.WriteInt32(Resource(ResourceNum).Health)
        buffer.WriteInt32(Resource(ResourceNum).ExpReward)
        buffer.WriteInt32(Resource(ResourceNum).ItemReward)
        buffer.WriteString((Resource(ResourceNum).Name))
        buffer.WriteInt32(Resource(ResourceNum).ResourceImage)
        buffer.WriteInt32(Resource(ResourceNum).ResourceType)
        buffer.WriteInt32(Resource(ResourceNum).RespawnTime)
        buffer.WriteString((Resource(ResourceNum).SuccessMessage))
        buffer.WriteInt32(Resource(ResourceNum).LvlRequired)
        buffer.WriteInt32(Resource(ResourceNum).ToolRequired)
        buffer.WriteInt32(Resource(ResourceNum).Walkthrough)
        Return buffer.ToArray
    End Function

    Sub LoadSkillExp()
        Dim cf = Path.Database & "SkillExp.ini"
        For i = 1 To 100
            SkillExpTable(i) = Ini.Read(cf, "Level", i)
        Next
    End Sub

#End Region

#Region "Gather Skills"

    Function GetPlayerGatherSkillLvl(index As Integer, SkillSlot As Integer) As Integer

        GetPlayerGatherSkillLvl = 0

        If index > MAX_PLAYERS Then Exit Function

        GetPlayerGatherSkillLvl = Player(index).Character(TempPlayer(index).CurChar).GatherSkills(SkillSlot).SkillLevel
    End Function

    Function GetPlayerGatherSkillExp(index As Integer, SkillSlot As Integer) As Integer

        GetPlayerGatherSkillExp = 0

        If index > MAX_PLAYERS Then Exit Function

        GetPlayerGatherSkillExp = Player(index).Character(TempPlayer(index).CurChar).GatherSkills(SkillSlot).SkillCurExp
    End Function

    Function GetPlayerGatherSkillMaxExp(index As Integer, SkillSlot As Integer) As Integer

        GetPlayerGatherSkillMaxExp = 0

        If index > MAX_PLAYERS Then Exit Function

        GetPlayerGatherSkillMaxExp = Player(index).Character(TempPlayer(index).CurChar).GatherSkills(SkillSlot).SkillNextLvlExp
    End Function

    Sub SetPlayerGatherSkillLvl(index As Integer, SkillSlot As Integer, lvl As Integer)
        If index > MAX_PLAYERS Then Exit Sub

        Player(index).Character(TempPlayer(index).CurChar).GatherSkills(SkillSlot).SkillLevel = lvl
    End Sub

    Sub SetPlayerGatherSkillExp(index As Integer, SkillSlot As Integer, Exp As Integer)
        If index > MAX_PLAYERS Then Exit Sub

        Player(index).Character(TempPlayer(index).CurChar).GatherSkills(SkillSlot).SkillCurExp = Exp
    End Sub

    Sub SetPlayerGatherSkillMaxExp(index As Integer, SkillSlot As Integer, MaxExp As Integer)
        If index > MAX_PLAYERS Then Exit Sub

        Player(index).Character(TempPlayer(index).CurChar).GatherSkills(SkillSlot).SkillNextLvlExp = MaxExp
    End Sub

    Sub CheckResourceLevelUp(index As Integer, SkillSlot As Integer)
        Dim expRollover As Integer, level_count As Integer

        level_count = 0

        If GetPlayerGatherSkillLvl(index, SkillSlot) = 100 Then Exit Sub

        Do While GetPlayerGatherSkillExp(index, SkillSlot) >= GetPlayerGatherSkillMaxExp(index, SkillSlot)
            expRollover = GetPlayerGatherSkillExp(index, SkillSlot) - GetPlayerGatherSkillMaxExp(index, SkillSlot)
            SetPlayerGatherSkillLvl(index, SkillSlot, GetPlayerGatherSkillLvl(index, SkillSlot) + 1)
            SetPlayerGatherSkillExp(index, SkillSlot, expRollover)
            SetPlayerGatherSkillMaxExp(index, SkillSlot, GetSkillNextLevel(index, SkillSlot))
            level_count = level_count + 1
        Loop

        If level_count > 0 Then
            If level_count = 1 Then
                'singular
                PlayerMsg(index, String.Format("Your {0} has gone up a level!", GetResourceSkillName(SkillSlot)), ColorType.BrightGreen)
            Else
                'plural
                PlayerMsg(index, String.Format("Your {0} has gone up by {1} levels!", GetResourceSkillName(SkillSlot), level_count), ColorType.BrightGreen)
            End If

            SavePlayer(index)
            SendPlayerData(index)
        End If
    End Sub

    Private Function GetResourceSkillName(ResSkill As ResourceSkills) As String
        Select Case ResSkill
            Case ResourceSkills.Herbalist
                GetResourceSkillName = "herbalism"
            Case ResourceSkills.WoodCutter
                GetResourceSkillName = "woodcutting"
            Case ResourceSkills.Miner
                GetResourceSkillName = "mining"
            Case ResourceSkills.Fisherman
                GetResourceSkillName = "fishing"
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    Function GetSkillNextLevel(index As Integer, SkillSlot As Integer) As Integer
        GetSkillNextLevel = 0
        If index < 0 OrElse index > MAX_PLAYERS Then Exit Function

        GetSkillNextLevel = SkillExpTable(GetPlayerGatherSkillLvl(index, SkillSlot) + 1)
    End Function

#End Region

#Region "Incoming Packets"

    Sub Packet_EditResource(index As Integer, ByRef data() As Byte)
        Dim Buffer As New ByteStream(4)

        AddDebug("Recieved EMSG: RequestEditResource")

        ' Prevent hacking
        If GetPlayerAccess(index) < AdminType.Developer Then Exit Sub

        Buffer.WriteInt32(ServerPackets.SResourceEditor)
        Socket.SendDataTo(index, Buffer.Data, Buffer.Head)

        AddDebug("Sent SMSG: SResourceEditor")

        Buffer.Dispose()
    End Sub

    Sub Packet_SaveResource(index As Integer, ByRef data() As Byte)
        Dim resourcenum As Integer
        Dim buffer As New ByteStream(data)

        AddDebug("Recieved EMSG: SaveResource")

        ' Prevent hacking
        If GetPlayerAccess(index) < AdminType.Developer Then Exit Sub

        resourcenum = buffer.ReadInt32

        ' Prevent hacking
        If resourcenum <= 0 OrElse resourcenum > MAX_RESOURCES Then Exit Sub

        Resource(resourcenum).Animation = buffer.ReadInt32()
        Resource(resourcenum).EmptyMessage = buffer.ReadString()
        Resource(resourcenum).ExhaustedImage = buffer.ReadInt32()
        Resource(resourcenum).Health = buffer.ReadInt32()
        Resource(resourcenum).ExpReward = buffer.ReadInt32()
        Resource(resourcenum).ItemReward = buffer.ReadInt32()
        Resource(resourcenum).Name = buffer.ReadString()
        Resource(resourcenum).ResourceImage = buffer.ReadInt32()
        Resource(resourcenum).ResourceType = buffer.ReadInt32()
        Resource(resourcenum).RespawnTime = buffer.ReadInt32()
        Resource(resourcenum).SuccessMessage = buffer.ReadString()
        Resource(resourcenum).LvlRequired = buffer.ReadInt32()
        Resource(resourcenum).ToolRequired = buffer.ReadInt32()
        Resource(resourcenum).Walkthrough = buffer.ReadInt32()

        ' Save it
        SendUpdateResourceToAll(resourcenum)
        SaveResource(resourcenum)

        Addlog(GetPlayerLogin(index) & " saved Resource #" & resourcenum & ".", ADMIN_LOG)

        buffer.Dispose()
    End Sub

    Sub Packet_RequestResources(index As Integer, ByRef data() As Byte)
        AddDebug("Recieved CMSG: CRequestResources")

        SendResources(index)
    End Sub

#End Region

#Region "Outgoing Packets"

    Sub SendResourceCacheTo(index As Integer, Resource_num As long)
        Dim i As Integer, mapnum As Integer
        Dim buffer As New ByteStream(4)

        mapnum = GetPlayerMap(index)

        buffer.WriteInt32(ServerPackets.SResourceCache)
        buffer.WriteInt32(ResourceCache(mapnum).ResourceCount)

        AddDebug("Sent SMSG: SResourcesCache")

        If ResourceCache(mapnum).ResourceCount > 0 Then

            For i = 0 To ResourceCache(mapnum).ResourceCount
                buffer.WriteInt32(ResourceCache(mapnum).ResourceData(i).ResourceState)
                buffer.WriteInt32(ResourceCache(mapnum).ResourceData(i).X)
                buffer.WriteInt32(ResourceCache(mapnum).ResourceData(i).Y)
            Next

        End If

        Socket.SendDataTo(index, buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendResourceCacheToMap(mapNum As Integer, Resource_num As Integer)
        Dim i As Integer
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SResourceCache)
        buffer.WriteInt32(ResourceCache(mapNum).ResourceCount)

        AddDebug("Sent SMSG: SResourceCache")

        If ResourceCache(mapNum).ResourceCount > 0 Then

            For i = 0 To ResourceCache(mapNum).ResourceCount
                buffer.WriteInt32(ResourceCache(mapNum).ResourceData(i).ResourceState)
                buffer.WriteInt32(ResourceCache(mapNum).ResourceData(i).X)
                buffer.WriteInt32(ResourceCache(mapNum).ResourceData(i).Y)
            Next

        End If

        SendDataToMap(mapNum, buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendResources(index As Integer)
        Dim i As Integer

        For i = 1 To MAX_RESOURCES

            If Len(Trim$(Resource(i).Name)) > 0 Then
                SendUpdateResourceTo(index, i)
            End If

        Next

    End Sub

    Sub SendUpdateResourceTo(index As Integer, ResourceNum As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SUpdateResource)

        buffer.WriteBlock(ResourceData(ResourceNum))

        AddDebug("Sent SMSG: SUpdateResources")

        Socket.SendDataTo(index, buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendUpdateResourceToAll(ResourceNum As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SUpdateResource)

        buffer.WriteBlock(ResourceData(ResourceNum))

        AddDebug("Sent SMSG: SUpdateResource")

        SendDataToAll(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

#End Region

#Region "Functions"

    Sub CheckResource(index As Integer, x As Integer, y As Integer)
        Dim Resource_num As Integer, ResourceType As Byte
        Dim Resource_index As Integer
        Dim rX As Integer, rY As Integer
        Dim Damage As Integer

        If Map(GetPlayerMap(index)).Tile(x, y).Type = TileType.Resource Then
            Resource_num = 0
            Resource_index = Map(GetPlayerMap(index)).Tile(x, y).Data1
            ResourceType = Resource(Resource_index).ResourceType

            ' Get the cache number
            For i = 0 To ResourceCache(GetPlayerMap(index)).ResourceCount
                If ResourceCache(GetPlayerMap(index)).ResourceData(i).X = x Then
                    If ResourceCache(GetPlayerMap(index)).ResourceData(i).Y = y Then
                        Resource_num = i
                    End If
                End If
            Next

            If Resource_num > 0 Then
                If GetPlayerEquipment(index, EquipmentType.Weapon) > 0 OrElse Resource(Resource_index).ToolRequired = 0 Then
                    If Item(GetPlayerEquipment(index, EquipmentType.Weapon)).Data3 = Resource(Resource_index).ToolRequired Then

                        ' inv space?
                        If Resource(Resource_index).ItemReward > 0 Then
                            If FindOpenInvSlot(index, Resource(Resource_index).ItemReward) = 0 Then
                                PlayerMsg(index, "You have no inventory space.", ColorType.Yellow)
                                Exit Sub
                            End If
                        End If

                        'required lvl?
                        If Resource(Resource_index).LvlRequired > GetPlayerGatherSkillLvl(index, ResourceType) Then
                            PlayerMsg(index, "Your level is too low!", ColorType.Yellow)
                            Exit Sub
                        End If

                        ' check if already cut down
                        If ResourceCache(GetPlayerMap(index)).ResourceData(Resource_num).ResourceState = 0 Then

                            rX = ResourceCache(GetPlayerMap(index)).ResourceData(Resource_num).X
                            rY = ResourceCache(GetPlayerMap(index)).ResourceData(Resource_num).Y

                            If Resource(Resource_index).ToolRequired = 0 Then
                                Damage = 1 * GetPlayerGatherSkillLvl(index, ResourceType)
                            Else
                                Damage = Item(GetPlayerEquipment(index, EquipmentType.Weapon)).Data2
                            End If

                            ' check if damage is more than health
                            If Damage > 0 Then
                                ' cut it down!
                                If ResourceCache(GetPlayerMap(index)).ResourceData(Resource_num).CurHealth - Damage <= 0 Then
                                    ResourceCache(GetPlayerMap(index)).ResourceData(Resource_num).ResourceState = 1 ' Cut
                                    ResourceCache(GetPlayerMap(index)).ResourceData(Resource_num).ResourceTimer = GetTimeMs()
                                    SendResourceCacheToMap(GetPlayerMap(index), Resource_num)
                                    SendActionMsg(GetPlayerMap(index), Trim$(Resource(Resource_index).SuccessMessage), ColorType.BrightGreen, 1, (GetPlayerX(index) * 32), (GetPlayerY(index) * 32))
                                    GiveInvItem(index, Resource(Resource_index).ItemReward, 1)
                                    SendAnimation(GetPlayerMap(index), Resource(Resource_index).Animation, rX, rY)
                                    SetPlayerGatherSkillExp(index, ResourceType, GetPlayerGatherSkillExp(index, ResourceType) + Resource(Resource_index).ExpReward)
                                    'send msg
                                    PlayerMsg(index, String.Format("Your {0} has earned {1} experience. ({2}/{3})", GetResourceSkillName(ResourceType), Resource(Resource_index).ExpReward, GetPlayerGatherSkillExp(index, ResourceType), GetPlayerGatherSkillMaxExp(index, ResourceType)), ColorType.BrightGreen)
                                    SendPlayerData(index)

                                    CheckResourceLevelUp(index, ResourceType)
                                Else
                                    ' just do the damage
                                    ResourceCache(GetPlayerMap(index)).ResourceData(Resource_num).CurHealth = ResourceCache(GetPlayerMap(index)).ResourceData(Resource_num).CurHealth - Damage
                                    SendActionMsg(GetPlayerMap(index), "-" & Damage, ColorType.BrightRed, 1, (rX * 32), (rY * 32))
                                    SendAnimation(GetPlayerMap(index), Resource(Resource_index).Animation, rX, rY)
                                End If
                                CheckTasks(index, QuestType.Gather, Resource_index)
                            Else
                                ' too weak
                                SendActionMsg(GetPlayerMap(index), "Miss!", ColorType.BrightRed, 1, (rX * 32), (rY * 32))
                            End If
                        Else
                            SendActionMsg(GetPlayerMap(index), Trim$(Resource(Resource_index).EmptyMessage), ColorType.BrightRed, 1, (GetPlayerX(index) * 32), (GetPlayerY(index) * 32))
                        End If
                    Else
                        PlayerMsg(index, "You have the wrong type of tool equiped.", ColorType.Yellow)
                    End If
                Else
                    PlayerMsg(index, "You need a tool to gather this resource.", ColorType.Yellow)
                End If
            End If
        End If
    End Sub

#End Region

End Module