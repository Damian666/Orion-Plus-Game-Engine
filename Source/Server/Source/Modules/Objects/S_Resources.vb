Imports System.IO

Friend Module S_Resources
    Friend SkillExpTable(100) As Integer

    Sub LoadSkillExp()
        Dim i As Integer
        Dim myXml As New XmlClass With {
            .Filename = Application.StartupPath & "\Data\SkillExp.xml",
            .Root = "Data"
        }

        myXml.LoadXml()

        For i = 1 To 100
            SkillExpTable(i) = myXml.ReadString("Level", i)
        Next

        myXml.CloseXml(False)
    End Sub

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
        Dim expRollover As Integer, skillname As String = ""
        Dim level_count As Integer

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
End Module