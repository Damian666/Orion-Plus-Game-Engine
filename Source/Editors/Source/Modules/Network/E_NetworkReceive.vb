Imports ASFW
Imports ASFW.IO

Module E_NetworkReceive

    Sub PacketRouter()
        Socket.PacketId(ServerPackets.SAlertMsg) = AddressOf Packet_AlertMSG
        Socket.PacketId(ServerPackets.SKeyPair) = AddressOf Packet_KeyPair

        Socket.PacketId(ServerPackets.SLoginOk) = AddressOf Packet_LoginOk
        Socket.PacketId(ServerPackets.SClassesData) = AddressOf Packet_ClassesData

        Socket.PacketId(ServerPackets.SMapData) = AddressOf Packet_MapData

        Socket.PacketId(ServerPackets.SMapNpcData) = AddressOf Packet_MapNPCData
        Socket.PacketId(ServerPackets.SMapNpcUpdate) = AddressOf Packet_MapNPCUpdate

        Socket.PacketId(ServerPackets.SItemEditor) = AddressOf Packet_EditItem
        Socket.PacketId(ServerPackets.SUpdateItem) = AddressOf Packet_UpdateItem

        Socket.PacketId(ServerPackets.SREditor) = AddressOf Packet_ResourceEditor

        Socket.PacketId(ServerPackets.SNpcEditor) = AddressOf Packet_NPCEditor
        Socket.PacketId(ServerPackets.SUpdateNpc) = AddressOf Packet_UpdateNPC

        Socket.PacketId(ServerPackets.SEditMap) = AddressOf Packet_EditMap

        Socket.PacketId(ServerPackets.SShopEditor) = AddressOf Packet_EditShop
        Socket.PacketId(ServerPackets.SUpdateShop) = AddressOf Packet_UpdateShop

        Socket.PacketId(ServerPackets.SSkillEditor) = AddressOf Packet_EditSkill
        Socket.PacketId(ServerPackets.SUpdateSkill) = AddressOf Packet_UpdateSkill

        Socket.PacketId(ServerPackets.SResourceEditor) = AddressOf Packet_ResourceEditor
        Socket.PacketId(ServerPackets.SUpdateResource) = AddressOf Packet_UpdateResource

        Socket.PacketId(ServerPackets.SAnimationEditor) = AddressOf Packet_EditAnimation
        Socket.PacketId(ServerPackets.SUpdateAnimation) = AddressOf Packet_UpdateAnimation

        Socket.PacketId(ServerPackets.SGameData) = AddressOf Packet_GameData
        Socket.PacketId(ServerPackets.SMapReport) = AddressOf Packet_Mapreport 'Mapreport

        Socket.PacketId(ServerPackets.SMapNames) = AddressOf Packet_MapNames

        'quests
        Socket.PacketId(ServerPackets.SQuestEditor) = AddressOf Packet_QuestEditor
        Socket.PacketId(ServerPackets.SUpdateQuest) = AddressOf Packet_UpdateQuest

        'Housing
        Socket.PacketId(ServerPackets.SHouseConfigs) = AddressOf Packet_HouseConfigurations
        Socket.PacketId(ServerPackets.SFurniture) = AddressOf Packet_Furniture
        Socket.PacketId(ServerPackets.SHouseEdit) = AddressOf Packet_EditHouses

        'Events
        Socket.PacketId(ServerPackets.SSpawnEvent) = AddressOf Packet_SpawnEvent
        Socket.PacketId(ServerPackets.SEventMove) = AddressOf Packet_EventMove
        Socket.PacketId(ServerPackets.SEventDir) = AddressOf Packet_EventDir
        Socket.PacketId(ServerPackets.SEventChat) = AddressOf Packet_EventChat
        Socket.PacketId(ServerPackets.SEventStart) = AddressOf Packet_EventStart
        Socket.PacketId(ServerPackets.SEventEnd) = AddressOf Packet_EventEnd
        Socket.PacketId(ServerPackets.SSwitchesAndVariables) = AddressOf Packet_SwitchesAndVariables
        Socket.PacketId(ServerPackets.SMapEventData) = AddressOf Packet_MapEventData
        Socket.PacketId(ServerPackets.SHoldPlayer) = AddressOf Packet_HoldPlayer

        Socket.PacketId(ServerPackets.SProjectileEditor) = AddressOf HandleProjectileEditor
        Socket.PacketId(ServerPackets.SUpdateProjectile) = AddressOf HandleUpdateProjectile
        Socket.PacketId(ServerPackets.SMapProjectile) = AddressOf HandleMapProjectile

        'craft
        Socket.PacketId(ServerPackets.SUpdateRecipe) = AddressOf Packet_UpdateRecipe
        Socket.PacketId(ServerPackets.SRecipeEditor) = AddressOf Packet_RecipeEditor

        Socket.PacketId(ServerPackets.SClassEditor) = AddressOf Packet_ClassEditor

        'Auto Mapper
        Socket.PacketId(ServerPackets.SAutoMapper) = AddressOf Packet_AutoMapper

        'pets
        Socket.PacketId(ServerPackets.SPetEditor) = AddressOf Packet_PetEditor
        Socket.PacketId(ServerPackets.SUpdatePet) = AddressOf Packet_UpdatePet

        Socket.PacketId(ServerPackets.SNews) = AddressOf Packet_News
    End Sub

    Private Sub Packet_News(ByRef data() As Byte)
        ' Do nothing we didnt want it anyway >.> ~SpiceyWolf
    End Sub

    Private Sub Packet_AlertMSG(ByRef data() As Byte)
        Dim Msg As String
        Dim buffer As New ByteStream(data)
        Msg = buffer.ReadString

        buffer.Dispose()

        MsgBox(Msg, vbOKOnly, "OrionClient+ Editors")

        CloseEditor()
    End Sub

    Private Sub Packet_KeyPair(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)
        EKeyPair.ImportKeyString(buffer.ReadString())
        buffer.Dispose()
    End Sub

    Private Sub Packet_LoginOk(ByRef data() As Byte)
        InitEditor = True
    End Sub

    Private Sub Packet_ClassesData(ByRef data() As Byte)
        Dim i As Integer
        Dim z As Integer, X As Integer
        Dim buffer As New ByteStream(data)
        ' Max classes
        Max_Classes = buffer.ReadInt32
        ReDim Classes(Max_Classes)

        For i = 0 To Max_Classes
            ReDim Classes(i).Stat(StatType.Count - 1)
        Next

        For i = 0 To Max_Classes
            ReDim Classes(i).Vital(VitalType.Count - 1)
        Next

        For i = 1 To Max_Classes

            With Classes(i)
                .Name = buffer.ReadString
                .Desc = buffer.ReadString

                .Vital(VitalType.HP) = buffer.ReadInt32
                .Vital(VitalType.MP) = buffer.ReadInt32
                .Vital(VitalType.SP) = buffer.ReadInt32

                ' get array size
                z = buffer.ReadInt32
                ' redim array
                ReDim .MaleSprite(z)
                ' loop-receive data
                For X = 0 To z
                    .MaleSprite(X) = buffer.ReadInt32
                Next

                ' get array size
                z = buffer.ReadInt32
                ' redim array
                ReDim .FemaleSprite(z)
                ' loop-receive data
                For X = 0 To z
                    .FemaleSprite(X) = buffer.ReadInt32
                Next

                .Stat(StatType.Strength) = buffer.ReadInt32
                .Stat(StatType.Endurance) = buffer.ReadInt32
                .Stat(StatType.Vitality) = buffer.ReadInt32
                .Stat(StatType.Intelligence) = buffer.ReadInt32
                .Stat(StatType.Luck) = buffer.ReadInt32
                .Stat(StatType.Spirit) = buffer.ReadInt32

                ReDim .StartItem(5)
                ReDim .StartValue(5)
                For q = 1 To 5
                    .StartItem(q) = buffer.ReadInt32
                    .StartValue(q) = buffer.ReadInt32
                Next

                .StartMap = buffer.ReadInt32
                .StartX = buffer.ReadInt32
                .StartY = buffer.ReadInt32

                .BaseExp = buffer.ReadInt32
            End With

        Next

        buffer.Dispose()
    End Sub

    Private Sub Packet_MapData(ByRef data() As Byte)
        Dim X As Integer, Y As Integer, i As Integer
        Dim buffer As New ByteStream(Compression.DecompressBytes(data))

        MapData = False

        SyncLock MapLock
            If buffer.ReadInt32 = 1 Then
                ClearMap()
                Map.mapNum = buffer.ReadInt32
                Map.Name = Trim(buffer.ReadString)
                Map.Music = Trim(buffer.ReadString)
                Map.Revision = buffer.ReadInt32
                Map.Moral = buffer.ReadInt32
                Map.tileset = buffer.ReadInt32
                Map.Up = buffer.ReadInt32
                Map.Down = buffer.ReadInt32
                Map.Left = buffer.ReadInt32
                Map.Right = buffer.ReadInt32
                Map.BootMap = buffer.ReadInt32
                Map.BootX = buffer.ReadInt32
                Map.BootY = buffer.ReadInt32
                Map.MaxX = buffer.ReadInt32
                Map.MaxY = buffer.ReadInt32
                Map.WeatherType = buffer.ReadInt32
                Map.Fogindex = buffer.ReadInt32
                Map.WeatherIntensity = buffer.ReadInt32
                Map.FogAlpha = buffer.ReadInt32
                Map.FogSpeed = buffer.ReadInt32
                Map.HasMapTint = buffer.ReadInt32
                Map.MapTintR = buffer.ReadInt32
                Map.MapTintG = buffer.ReadInt32
                Map.MapTintB = buffer.ReadInt32
                Map.MapTintA = buffer.ReadInt32

                Map.Instanced = buffer.ReadInt32
                Map.Panorama = buffer.ReadInt32
                Map.Parallax = buffer.ReadInt32

                ReDim Map.Tile(Map.MaxX, Map.MaxY)

                For X = 1 To MAX_MAP_NPCS
                    Map.Npc(X) = buffer.ReadInt32
                Next

                For X = 0 To Map.MaxX
                    For Y = 0 To Map.MaxY
                        Map.Tile(X, Y).Data1 = buffer.ReadInt32
                        Map.Tile(X, Y).Data2 = buffer.ReadInt32
                        Map.Tile(X, Y).Data3 = buffer.ReadInt32
                        Map.Tile(X, Y).DirBlock = buffer.ReadInt32

                        ReDim Map.Tile(X, Y).Layer(LayerType.Count - 1)

                        For i = 0 To LayerType.Count - 1
                            Map.Tile(X, Y).Layer(i).Tileset = buffer.ReadInt32
                            Map.Tile(X, Y).Layer(i).X = buffer.ReadInt32
                            Map.Tile(X, Y).Layer(i).Y = buffer.ReadInt32
                            Map.Tile(X, Y).Layer(i).AutoTile = buffer.ReadInt32
                        Next
                        Map.Tile(X, Y).Type = buffer.ReadInt32
                    Next
                Next

                'Event Data!
                ResetEventdata()

                Map.EventCount = buffer.ReadInt32

                If Map.EventCount > 0 Then
                    ReDim Map.Events(Map.EventCount)
                    For i = 1 To Map.EventCount
                        With Map.Events(i)
                            .Name = Trim(buffer.ReadString)
                            .Globals = buffer.ReadInt32
                            .X = buffer.ReadInt32
                            .Y = buffer.ReadInt32
                            .PageCount = buffer.ReadInt32
                        End With
                        If Map.Events(i).PageCount > 0 Then
                            ReDim Map.Events(i).Pages(Map.Events(i).PageCount)
                            For X = 1 To Map.Events(i).PageCount
                                With Map.Events(i).Pages(X)
                                    .ChkVariable = buffer.ReadInt32
                                    .Variableindex = buffer.ReadInt32
                                    .VariableCondition = buffer.ReadInt32
                                    .VariableCompare = buffer.ReadInt32

                                    .ChkSwitch = buffer.ReadInt32
                                    .Switchindex = buffer.ReadInt32
                                    .SwitchCompare = buffer.ReadInt32

                                    .ChkHasItem = buffer.ReadInt32
                                    .HasItemindex = buffer.ReadInt32
                                    .HasItemAmount = buffer.ReadInt32

                                    .ChkSelfSwitch = buffer.ReadInt32
                                    .SelfSwitchindex = buffer.ReadInt32
                                    .SelfSwitchCompare = buffer.ReadInt32

                                    .GraphicType = buffer.ReadInt32
                                    .Graphic = buffer.ReadInt32
                                    .GraphicX = buffer.ReadInt32
                                    .GraphicY = buffer.ReadInt32
                                    .GraphicX2 = buffer.ReadInt32
                                    .GraphicY2 = buffer.ReadInt32

                                    .MoveType = buffer.ReadInt32
                                    .MoveSpeed = buffer.ReadInt32
                                    .MoveFreq = buffer.ReadInt32

                                    .MoveRouteCount = buffer.ReadInt32

                                    .IgnoreMoveRoute = buffer.ReadInt32
                                    .RepeatMoveRoute = buffer.ReadInt32

                                    If .MoveRouteCount > 0 Then
                                        ReDim Map.Events(i).Pages(X).MoveRoute(.MoveRouteCount)
                                        For Y = 1 To .MoveRouteCount
                                            .MoveRoute(Y).Index = buffer.ReadInt32
                                            .MoveRoute(Y).Data1 = buffer.ReadInt32
                                            .MoveRoute(Y).Data2 = buffer.ReadInt32
                                            .MoveRoute(Y).Data3 = buffer.ReadInt32
                                            .MoveRoute(Y).Data4 = buffer.ReadInt32
                                            .MoveRoute(Y).Data5 = buffer.ReadInt32
                                            .MoveRoute(Y).Data6 = buffer.ReadInt32
                                        Next
                                    End If

                                    .WalkAnim = buffer.ReadInt32
                                    .DirFix = buffer.ReadInt32
                                    .WalkThrough = buffer.ReadInt32
                                    .ShowName = buffer.ReadInt32
                                    .Trigger = buffer.ReadInt32
                                    .CommandListCount = buffer.ReadInt32

                                    .Position = buffer.ReadInt32
                                    .Questnum = buffer.ReadInt32

                                    .ChkPlayerGender = buffer.ReadInt32
                                End With

                                If Map.Events(i).Pages(X).CommandListCount > 0 Then
                                    ReDim Map.Events(i).Pages(X).CommandList(Map.Events(i).Pages(X).CommandListCount)
                                    For Y = 1 To Map.Events(i).Pages(X).CommandListCount
                                        Map.Events(i).Pages(X).CommandList(Y).CommandCount = buffer.ReadInt32
                                        Map.Events(i).Pages(X).CommandList(Y).ParentList = buffer.ReadInt32
                                        If Map.Events(i).Pages(X).CommandList(Y).CommandCount > 0 Then
                                            ReDim Map.Events(i).Pages(X).CommandList(Y).Commands(Map.Events(i).Pages(X).CommandList(Y).CommandCount)
                                            For z = 1 To Map.Events(i).Pages(X).CommandList(Y).CommandCount
                                                With Map.Events(i).Pages(X).CommandList(Y).Commands(z)
                                                    .Index = buffer.ReadInt32
                                                    .Text1 = Trim(buffer.ReadString)
                                                    .Text2 = Trim(buffer.ReadString)
                                                    .Text3 = Trim(buffer.ReadString)
                                                    .Text4 = Trim(buffer.ReadString)
                                                    .Text5 = Trim(buffer.ReadString)
                                                    .Data1 = buffer.ReadInt32
                                                    .Data2 = buffer.ReadInt32
                                                    .Data3 = buffer.ReadInt32
                                                    .Data4 = buffer.ReadInt32
                                                    .Data5 = buffer.ReadInt32
                                                    .Data6 = buffer.ReadInt32
                                                    .ConditionalBranch.CommandList = buffer.ReadInt32
                                                    .ConditionalBranch.Condition = buffer.ReadInt32
                                                    .ConditionalBranch.Data1 = buffer.ReadInt32
                                                    .ConditionalBranch.Data2 = buffer.ReadInt32
                                                    .ConditionalBranch.Data3 = buffer.ReadInt32
                                                    .ConditionalBranch.ElseCommandList = buffer.ReadInt32
                                                    .MoveRouteCount = buffer.ReadInt32
                                                    If .MoveRouteCount > 0 Then
                                                        ReDim Preserve .MoveRoute(.MoveRouteCount)
                                                        For w = 1 To .MoveRouteCount
                                                            .MoveRoute(w).Index = buffer.ReadInt32
                                                            .MoveRoute(w).Data1 = buffer.ReadInt32
                                                            .MoveRoute(w).Data2 = buffer.ReadInt32
                                                            .MoveRoute(w).Data3 = buffer.ReadInt32
                                                            .MoveRoute(w).Data4 = buffer.ReadInt32
                                                            .MoveRoute(w).Data5 = buffer.ReadInt32
                                                            .MoveRoute(w).Data6 = buffer.ReadInt32
                                                        Next
                                                    End If
                                                End With
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
                'End Event Data

            End If

            For i = 1 To MAX_MAP_ITEMS
                MapItem(i).Num = buffer.ReadInt32
                MapItem(i).Value = buffer.ReadInt32()
                MapItem(i).X = buffer.ReadInt32()
                MapItem(i).Y = buffer.ReadInt32()
            Next

            For i = 1 To MAX_MAP_NPCS
                MapNpc(i).Num = buffer.ReadInt32()
                MapNpc(i).X = buffer.ReadInt32()
                MapNpc(i).Y = buffer.ReadInt32()
                MapNpc(i).Dir = buffer.ReadInt32()
                MapNpc(i).Vital(VitalType.HP) = buffer.ReadInt32()
                MapNpc(i).Vital(VitalType.MP) = buffer.ReadInt32()
            Next

            If buffer.ReadInt32 = 1 Then
                Resource_index = buffer.ReadInt32
                Resources_Init = False

                If Resource_index > 0 Then
                    ReDim MapResource(Resource_index)

                    For i = 0 To Resource_index
                        MapResource(i).ResourceState = buffer.ReadInt32
                        MapResource(i).X = buffer.ReadInt32
                        MapResource(i).Y = buffer.ReadInt32
                    Next

                    Resources_Init = True
                Else
                    ReDim MapResource(1)
                End If
            End If

            buffer.Dispose()

        End SyncLock

        ClearTempTile()
        InitAutotiles()

        MapData = True

        CurrentWeather = Map.WeatherType
        CurrentWeatherIntensity = Map.WeatherIntensity
        CurrentFog = Map.Fogindex
        CurrentFogSpeed = Map.FogSpeed
        CurrentFogOpacity = Map.FogAlpha
        CurrentTintR = Map.MapTintR
        CurrentTintG = Map.MapTintG
        CurrentTintB = Map.MapTintB
        CurrentTintA = Map.MapTintA

        InMapEditor = True

        GettingMap = False
    End Sub

    Private Sub Packet_MapNPCData(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)

        For i = 1 To MAX_MAP_NPCS

            With MapNpc(i)
                .Num = buffer.ReadInt32
                .X = buffer.ReadInt32
                .Y = buffer.ReadInt32
                .Dir = buffer.ReadInt32
                .Vital(VitalType.HP) = buffer.ReadInt32
                .Vital(VitalType.MP) = buffer.ReadInt32
            End With

        Next

        buffer.Dispose()
    End Sub

    Private Sub Packet_MapNPCUpdate(ByRef data() As Byte)
        Dim NpcNum As Integer
        Dim buffer As ByteStream
        buffer = New ByteStream(data)

        NpcNum = buffer.ReadInt32

        With MapNpc(NpcNum)
            .Num = buffer.ReadInt32
            .X = buffer.ReadInt32
            .Y = buffer.ReadInt32
            .Dir = buffer.ReadInt32
            .Vital(VitalType.HP) = buffer.ReadInt32
            .Vital(VitalType.MP) = buffer.ReadInt32
        End With

        buffer.Dispose()
    End Sub

    Private Sub Packet_NPCEditor(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)
        InitNPCEditor = True

        buffer.Dispose()
    End Sub

    Private Sub Packet_UpdateNPC(ByRef data() As Byte)
        Dim i As Integer, x As Integer
        Dim buffer As New ByteStream(data)

        i = buffer.ReadInt32
        ' Update the Npc
        Npc(i).Animation = buffer.ReadInt32()
        Npc(i).AttackSay = Trim(buffer.ReadString())
        Npc(i).Behaviour = buffer.ReadInt32()
        ReDim Npc(i).DropChance(5)
        ReDim Npc(i).DropItem(5)
        ReDim Npc(i).DropItemValue(5)
        For x = 1 To 5
            Npc(i).DropChance(x) = buffer.ReadInt32()
            Npc(i).DropItem(x) = buffer.ReadInt32()
            Npc(i).DropItemValue(x) = buffer.ReadInt32()
        Next

        Npc(i).Exp = buffer.ReadInt32()
        Npc(i).Faction = buffer.ReadInt32()
        Npc(i).Hp = buffer.ReadInt32()
        Npc(i).Name = Trim(buffer.ReadString())
        Npc(i).Range = buffer.ReadInt32()
        Npc(i).SpawnTime = buffer.ReadInt32()
        Npc(i).SpawnSecs = buffer.ReadInt32()
        Npc(i).Sprite = buffer.ReadInt32()

        For x = 0 To StatType.Count - 1
            Npc(i).Stat(x) = buffer.ReadInt32()
        Next

        Npc(i).QuestNum = buffer.ReadInt32()

        For x = 1 To MAX_NPC_SKILLS
            Npc(i).Skill(x) = buffer.ReadInt32()
        Next

        Npc(i).Level = buffer.ReadInt32()
        Npc(i).Damage = buffer.ReadInt32()

        If Npc(i).AttackSay Is Nothing Then Npc(i).AttackSay = ""
        If Npc(i).Name Is Nothing Then Npc(i).Name = ""

        buffer.Dispose()
    End Sub

    Private Sub Packet_EditMap(ByRef data() As Byte)
        InitMapEditor = True
    End Sub

    Private Sub Packet_EditShop(ByRef data() As Byte)
        InitShopEditor = True
    End Sub

    Private Sub Packet_UpdateShop(ByRef data() As Byte)
        Dim shopnum As Integer
        Dim buffer As New ByteStream(data)
        shopnum = buffer.ReadInt32

        Shop(shopnum).BuyRate = buffer.ReadInt32()
        Shop(shopnum).Name = Trim(buffer.ReadString())
        Shop(shopnum).Face = buffer.ReadInt32()

        For i = 0 To MAX_TRADES
            Shop(shopnum).TradeItem(i).CostItem = buffer.ReadInt32()
            Shop(shopnum).TradeItem(i).CostValue = buffer.ReadInt32()
            Shop(shopnum).TradeItem(i).Item = buffer.ReadInt32()
            Shop(shopnum).TradeItem(i).ItemValue = buffer.ReadInt32()
        Next

        If Shop(shopnum).Name Is Nothing Then Shop(shopnum).Name = ""

        buffer.Dispose()
    End Sub

    Private Sub Packet_EditSkill(ByRef data() As Byte)
        InitSkillEditor = True
    End Sub

    Private Sub Packet_UpdateSkill(ByRef data() As Byte)
        Dim skillnum As Integer
        Dim buffer As New ByteStream(data)
        skillnum = buffer.ReadInt32

        Skill(skillnum).AccessReq = buffer.ReadInt32()
        Skill(skillnum).AoE = buffer.ReadInt32()
        Skill(skillnum).CastAnim = buffer.ReadInt32()
        Skill(skillnum).CastTime = buffer.ReadInt32()
        Skill(skillnum).CdTime = buffer.ReadInt32()
        Skill(skillnum).ClassReq = buffer.ReadInt32()
        Skill(skillnum).Dir = buffer.ReadInt32()
        Skill(skillnum).Duration = buffer.ReadInt32()
        Skill(skillnum).Icon = buffer.ReadInt32()
        Skill(skillnum).Interval = buffer.ReadInt32()
        Skill(skillnum).IsAoE = buffer.ReadInt32()
        Skill(skillnum).LevelReq = buffer.ReadInt32()
        Skill(skillnum).Map = buffer.ReadInt32()
        Skill(skillnum).MpCost = buffer.ReadInt32()
        Skill(skillnum).Name = Trim(buffer.ReadString())
        Skill(skillnum).Range = buffer.ReadInt32()
        Skill(skillnum).SkillAnim = buffer.ReadInt32()
        Skill(skillnum).StunDuration = buffer.ReadInt32()
        Skill(skillnum).Type = buffer.ReadInt32()
        Skill(skillnum).Vital = buffer.ReadInt32()
        Skill(skillnum).X = buffer.ReadInt32()
        Skill(skillnum).Y = buffer.ReadInt32()

        Skill(skillnum).IsProjectile = buffer.ReadInt32()
        Skill(skillnum).Projectile = buffer.ReadInt32()

        Skill(skillnum).KnockBack = buffer.ReadInt32()
        Skill(skillnum).KnockBackTiles = buffer.ReadInt32()

        If Skill(skillnum).Name Is Nothing Then Skill(skillnum).Name = ""

        buffer.Dispose()

    End Sub

    Private Sub Packet_ResourceEditor(ByRef data() As Byte)
        InitResourceEditor = True
    End Sub

    Private Sub Packet_UpdateResource(ByRef data() As Byte)
        Dim ResourceNum As Integer
        Dim buffer As New ByteStream(data)
        ResourceNum = buffer.ReadInt32

        Resource(ResourceNum).Animation = buffer.ReadInt32()
        Resource(ResourceNum).EmptyMessage = Trim(buffer.ReadString())
        Resource(ResourceNum).ExhaustedImage = buffer.ReadInt32()
        Resource(ResourceNum).Health = buffer.ReadInt32()
        Resource(ResourceNum).ExpReward = buffer.ReadInt32()
        Resource(ResourceNum).ItemReward = buffer.ReadInt32()
        Resource(ResourceNum).Name = Trim(buffer.ReadString())
        Resource(ResourceNum).ResourceImage = buffer.ReadInt32()
        Resource(ResourceNum).ResourceType = buffer.ReadInt32()
        Resource(ResourceNum).RespawnTime = buffer.ReadInt32()
        Resource(ResourceNum).SuccessMessage = Trim(buffer.ReadString())
        Resource(ResourceNum).LvlRequired = buffer.ReadInt32()
        Resource(ResourceNum).ToolRequired = buffer.ReadInt32()
        Resource(ResourceNum).Walkthrough = buffer.ReadInt32()

        If Resource(ResourceNum).Name Is Nothing Then Resource(ResourceNum).Name = ""
        If Resource(ResourceNum).EmptyMessage Is Nothing Then Resource(ResourceNum).EmptyMessage = ""
        If Resource(ResourceNum).SuccessMessage Is Nothing Then Resource(ResourceNum).SuccessMessage = ""

        buffer.Dispose()
    End Sub

    Private Sub Packet_EditAnimation(ByRef data() As Byte)
        InitAnimationEditor = True
    End Sub

    Private Sub Packet_UpdateAnimation(ByRef data() As Byte)
        Dim n As Integer, i As Integer
        Dim buffer As New ByteStream(data)
        n = buffer.ReadInt32
        ' Update the Animation
        For i = 0 To UBound(Animation(n).Frames)
            Animation(n).Frames(i) = buffer.ReadInt32()
        Next

        For i = 0 To UBound(Animation(n).LoopCount)
            Animation(n).LoopCount(i) = buffer.ReadInt32()
        Next

        For i = 0 To UBound(Animation(n).LoopTime)
            Animation(n).LoopTime(i) = buffer.ReadInt32()
        Next

        Animation(n).Name = Trim$(buffer.ReadString)
        Animation(n).Sound = Trim$(buffer.ReadString)

        If Animation(n).Name Is Nothing Then Animation(n).Name = ""
        If Animation(n).Sound Is Nothing Then Animation(n).Sound = ""

        For i = 0 To UBound(Animation(n).Sprite)
            Animation(n).Sprite(i) = buffer.ReadInt32()
        Next
        buffer.Dispose()
    End Sub

    Private Sub Packet_GameData(ByRef data() As Byte)
        Dim n As Integer, i As Integer, z As Integer, x As Integer, a As Integer, b As Integer
        Dim buffer As New ByteStream(Compression.DecompressBytes(data))

        '\\\Read Class Data\\\

        ' Max classes
        Max_Classes = buffer.ReadInt32
        ReDim Classes(Max_Classes)

        For i = 0 To Max_Classes
            ReDim Classes(i).Stat(StatType.Count - 1)
        Next

        For i = 0 To Max_Classes
            ReDim Classes(i).Vital(VitalType.Count - 1)
        Next

        For i = 1 To Max_Classes

            With Classes(i)
                .Name = Trim(buffer.ReadString)
                .Desc = Trim$(buffer.ReadString)

                .Vital(VitalType.HP) = buffer.ReadInt32
                .Vital(VitalType.MP) = buffer.ReadInt32
                .Vital(VitalType.SP) = buffer.ReadInt32

                ' get array size
                z = buffer.ReadInt32
                ' redim array
                ReDim .MaleSprite(z)
                ' loop-receive data
                For x = 0 To z
                    .MaleSprite(x) = buffer.ReadInt32
                Next

                ' get array size
                z = buffer.ReadInt32
                ' redim array
                ReDim .FemaleSprite(z)
                ' loop-receive data
                For x = 0 To z
                    .FemaleSprite(x) = buffer.ReadInt32
                Next

                .Stat(StatType.Strength) = buffer.ReadInt32
                .Stat(StatType.Endurance) = buffer.ReadInt32
                .Stat(StatType.Vitality) = buffer.ReadInt32
                .Stat(StatType.Intelligence) = buffer.ReadInt32
                .Stat(StatType.Luck) = buffer.ReadInt32
                .Stat(StatType.Spirit) = buffer.ReadInt32

                ReDim .StartItem(5)
                ReDim .StartValue(5)
                For q = 1 To 5
                    .StartItem(q) = buffer.ReadInt32
                    .StartValue(q) = buffer.ReadInt32
                Next

                .StartMap = buffer.ReadInt32
                .StartX = buffer.ReadInt32
                .StartY = buffer.ReadInt32

                .BaseExp = buffer.ReadInt32
            End With

        Next

        i = 0
        x = 0
        n = 0
        z = 0

        '\\\End Read Class Data\\\

        '\\\Read Item Data\\\\\\\
        x = buffer.ReadInt32

        For i = 1 To x
            n = buffer.ReadInt32

            ' Update the item
            Item(n).AccessReq = buffer.ReadInt32()

            For z = 0 To StatType.Count - 1
                Item(n).Add_Stat(z) = buffer.ReadInt32()
            Next

            Item(n).Animation = buffer.ReadInt32()
            Item(n).BindType = buffer.ReadInt32()
            Item(n).ClassReq = buffer.ReadInt32()
            Item(n).Data1 = buffer.ReadInt32()
            Item(n).Data2 = buffer.ReadInt32()
            Item(n).Data3 = buffer.ReadInt32()
            Item(n).TwoHanded = buffer.ReadInt32()
            Item(n).LevelReq = buffer.ReadInt32()
            Item(n).Mastery = buffer.ReadInt32()
            Item(n).Name = Trim$(buffer.ReadString())
            Item(n).Paperdoll = buffer.ReadInt32()
            Item(n).Pic = buffer.ReadInt32()
            Item(n).Price = buffer.ReadInt32()
            Item(n).Rarity = buffer.ReadInt32()
            Item(n).Speed = buffer.ReadInt32()

            Item(n).Randomize = buffer.ReadInt32()
            Item(n).RandomMin = buffer.ReadInt32()
            Item(n).RandomMax = buffer.ReadInt32()

            Item(n).Stackable = buffer.ReadInt32()
            Item(n).Description = Trim$(buffer.ReadString())

            For z = 0 To StatType.Count - 1
                Item(n).Stat_Req(z) = buffer.ReadInt32()
            Next

            Item(n).Type = buffer.ReadInt32()
            Item(n).SubType = buffer.ReadInt32()

            Item(n).ItemLevel = buffer.ReadInt32()

            'Housing
            Item(n).FurnitureWidth = buffer.ReadInt32()
            Item(n).FurnitureHeight = buffer.ReadInt32()

            For a = 0 To 3
                For b = 0 To 3
                    Item(n).FurnitureBlocks(a, b) = buffer.ReadInt32()
                    Item(n).FurnitureFringe(a, b) = buffer.ReadInt32()
                Next
            Next

            Item(n).KnockBack = buffer.ReadInt32()
            Item(n).KnockBackTiles = buffer.ReadInt32()

            Item(n).Projectile = buffer.ReadInt32()
            Item(n).Ammo = buffer.ReadInt32()
        Next

        i = 0
        n = 0
        x = 0
        z = 0

        '\\\End Read Item Data\\\\\\\

        '\\\Read Animation Data\\\\\\\
        x = buffer.ReadInt32

        For i = 1 To x
            n = buffer.ReadInt32
            ' Update the Animation
            For z = 0 To UBound(Animation(n).Frames)
                Animation(n).Frames(z) = buffer.ReadInt32()
            Next

            For z = 0 To UBound(Animation(n).LoopCount)
                Animation(n).LoopCount(z) = buffer.ReadInt32()
            Next

            For z = 0 To UBound(Animation(n).LoopTime)
                Animation(n).LoopTime(z) = buffer.ReadInt32()
            Next

            Animation(n).Name = Trim(buffer.ReadString)
            Animation(n).Sound = Trim(buffer.ReadString)

            If Animation(n).Name Is Nothing Then Animation(n).Name = ""
            If Animation(n).Sound Is Nothing Then Animation(n).Sound = ""

            For z = 0 To UBound(Animation(n).Sprite)
                Animation(n).Sprite(z) = buffer.ReadInt32()
            Next
        Next

        i = 0
        n = 0
        x = 0
        z = 0

        '\\\End Read Animation Data\\\\\\\

        '\\\Read NPC Data\\\\\\\
        x = buffer.ReadInt32
        For i = 1 To x
            n = buffer.ReadInt32
            ' Update the Npc
            Npc(n).Animation = buffer.ReadInt32()
            Npc(n).AttackSay = Trim(buffer.ReadString())
            Npc(n).Behaviour = buffer.ReadInt32()
            For z = 1 To 5
                Npc(n).DropChance(z) = buffer.ReadInt32()
                Npc(n).DropItem(z) = buffer.ReadInt32()
                Npc(n).DropItemValue(z) = buffer.ReadInt32()
            Next

            Npc(n).Exp = buffer.ReadInt32()
            Npc(n).Faction = buffer.ReadInt32()
            Npc(n).Hp = buffer.ReadInt32()
            Npc(n).Name = Trim(buffer.ReadString())
            Npc(n).Range = buffer.ReadInt32()
            Npc(n).SpawnTime = buffer.ReadInt32()
            Npc(n).SpawnSecs = buffer.ReadInt32()
            Npc(n).Sprite = buffer.ReadInt32()

            For z = 0 To StatType.Count - 1
                Npc(n).Stat(z) = buffer.ReadInt32()
            Next

            Npc(n).QuestNum = buffer.ReadInt32()

            ReDim Npc(n).Skill(MAX_NPC_SKILLS)
            For z = 1 To MAX_NPC_SKILLS
                Npc(n).Skill(z) = buffer.ReadInt32()
            Next

            Npc(i).Level = buffer.ReadInt32()
            Npc(i).Damage = buffer.ReadInt32()

            If Npc(n).AttackSay Is Nothing Then Npc(n).AttackSay = ""
            If Npc(n).Name Is Nothing Then Npc(n).Name = ""
        Next

        i = 0
        n = 0
        x = 0
        z = 0

        '\\\End Read NPC Data\\\\\\\

        '\\\Read Shop Data\\\\\\\
        x = buffer.ReadInt32

        For i = 1 To x
            n = buffer.ReadInt32

            Shop(n).BuyRate = buffer.ReadInt32()
            Shop(n).Name = Trim(buffer.ReadString())
            Shop(n).Face = buffer.ReadInt32()

            For z = 0 To MAX_TRADES
                Shop(n).TradeItem(z).CostItem = buffer.ReadInt32()
                Shop(n).TradeItem(z).CostValue = buffer.ReadInt32()
                Shop(n).TradeItem(z).Item = buffer.ReadInt32()
                Shop(n).TradeItem(z).ItemValue = buffer.ReadInt32()
            Next

            If Shop(n).Name Is Nothing Then Shop(n).Name = ""
        Next

        i = 0
        n = 0
        x = 0
        z = 0

        '\\\End Read Shop Data\\\\\\\

        '\\\Read Skills Data\\\\\\\\\\
        x = buffer.ReadInt32

        For i = 1 To x
            n = buffer.ReadInt32

            Skill(n).AccessReq = buffer.ReadInt32()
            Skill(n).AoE = buffer.ReadInt32()
            Skill(n).CastAnim = buffer.ReadInt32()
            Skill(n).CastTime = buffer.ReadInt32()
            Skill(n).CdTime = buffer.ReadInt32()
            Skill(n).ClassReq = buffer.ReadInt32()
            Skill(n).Dir = buffer.ReadInt32()
            Skill(n).Duration = buffer.ReadInt32()
            Skill(n).Icon = buffer.ReadInt32()
            Skill(n).Interval = buffer.ReadInt32()
            Skill(n).IsAoE = buffer.ReadInt32()
            Skill(n).LevelReq = buffer.ReadInt32()
            Skill(n).Map = buffer.ReadInt32()
            Skill(n).MpCost = buffer.ReadInt32()
            Skill(n).Name = Trim(buffer.ReadString())
            Skill(n).Range = buffer.ReadInt32()
            Skill(n).SkillAnim = buffer.ReadInt32()
            Skill(n).StunDuration = buffer.ReadInt32()
            Skill(n).Type = buffer.ReadInt32()
            Skill(n).Vital = buffer.ReadInt32()
            Skill(n).X = buffer.ReadInt32()
            Skill(n).Y = buffer.ReadInt32()

            Skill(n).IsProjectile = buffer.ReadInt32()
            Skill(n).Projectile = buffer.ReadInt32()

            Skill(n).KnockBack = buffer.ReadInt32()
            Skill(n).KnockBackTiles = buffer.ReadInt32()

            If Skill(n).Name Is Nothing Then Skill(n).Name = ""
        Next

        i = 0
        x = 0
        n = 0
        z = 0

        '\\\End Read Skills Data\\\\\\\\\\

        '\\\Read Resource Data\\\\\\\\\\\\
        x = buffer.ReadInt32

        For i = 1 To x
            n = buffer.ReadInt32

            Resource(n).Animation = buffer.ReadInt32()
            Resource(n).EmptyMessage = Trim(buffer.ReadString())
            Resource(n).ExhaustedImage = buffer.ReadInt32()
            Resource(n).Health = buffer.ReadInt32()
            Resource(n).ExpReward = buffer.ReadInt32()
            Resource(n).ItemReward = buffer.ReadInt32()
            Resource(n).Name = Trim(buffer.ReadString())
            Resource(n).ResourceImage = buffer.ReadInt32()
            Resource(n).ResourceType = buffer.ReadInt32()
            Resource(n).RespawnTime = buffer.ReadInt32()
            Resource(n).SuccessMessage = Trim(buffer.ReadString())
            Resource(n).LvlRequired = buffer.ReadInt32()
            Resource(n).ToolRequired = buffer.ReadInt32()
            Resource(n).Walkthrough = buffer.ReadInt32()

            If Resource(n).Name Is Nothing Then Resource(n).Name = ""
            If Resource(n).EmptyMessage Is Nothing Then Resource(n).EmptyMessage = ""
            If Resource(n).SuccessMessage Is Nothing Then Resource(n).SuccessMessage = ""
        Next

        i = 0
        n = 0
        x = 0
        z = 0

        '\\\End Read Resource Data\\\\\\\\\\\\

        buffer.Dispose()
    End Sub

    Private Sub Packet_Mapreport(ByRef data() As Byte)
        Dim I As Integer
        Dim buffer As New ByteStream(data)
        For I = 1 To MAX_MAPS
            MapNames(I) = Trim(buffer.ReadString())
        Next

        UpdateMapnames = True

        buffer.Dispose()
    End Sub

    Private Sub Packet_MapNames(ByRef data() As Byte)
        Dim I As Integer
        Dim buffer As New ByteStream(data)
        For I = 1 To MAX_MAPS
            MapNames(I) = Trim(buffer.ReadString())
        Next

        buffer.Dispose()
    End Sub

    Private Sub Packet_ClassEditor(ByRef data() As Byte)
        InitClassEditor = True
    End Sub

End Module