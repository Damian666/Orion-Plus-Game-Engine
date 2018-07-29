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
        dim buffer as New ByteStream(Data)
        Msg = Buffer.ReadString

        Buffer.Dispose()

        MsgBox(Msg, vbOKOnly, "OrionClient+ Editors")

        CloseEditor()
    End Sub

    Private Sub Packet_KeyPair(ByRef data() As Byte)
        dim buffer as New ByteStream(Data)
        EKeyPair.ImportKeyString(Buffer.ReadString())
        Buffer.Dispose()
    End Sub

    Private Sub Packet_LoginOk(ByRef data() As Byte)
        InitEditor = True
    End Sub

    Private Sub Packet_ClassesData(ByRef data() As Byte)
        Dim i As Integer
        Dim z As Integer, X As Integer
        dim buffer as New ByteStream(Data)
        ' Max classes
        Max_Classes = Buffer.ReadInt32
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

                .Vital(VitalType.HP) = Buffer.ReadInt32
                .Vital(VitalType.MP) = Buffer.ReadInt32
                .Vital(VitalType.SP) = Buffer.ReadInt32

                ' get array size
                z = Buffer.ReadInt32
                ' redim array
                ReDim .MaleSprite(z)
                ' loop-receive data
                For X = 0 To z
                    .MaleSprite(X) = Buffer.ReadInt32
                Next

                ' get array size
                z = Buffer.ReadInt32
                ' redim array
                ReDim .FemaleSprite(z)
                ' loop-receive data
                For X = 0 To z
                    .FemaleSprite(X) = Buffer.ReadInt32
                Next

                .Stat(StatType.Strength) = Buffer.ReadInt32
                .Stat(StatType.Endurance) = Buffer.ReadInt32
                .Stat(StatType.Vitality) = Buffer.ReadInt32
                .Stat(StatType.Intelligence) = Buffer.ReadInt32
                .Stat(StatType.Luck) = Buffer.ReadInt32
                .Stat(StatType.Spirit) = Buffer.ReadInt32

                ReDim .StartItem(5)
                ReDim .StartValue(5)
                For q = 1 To 5
                    .StartItem(q) = Buffer.ReadInt32
                    .StartValue(q) = Buffer.ReadInt32
                Next

                .StartMap = Buffer.ReadInt32
                .StartX = Buffer.ReadInt32
                .StartY = Buffer.ReadInt32

                .BaseExp = Buffer.ReadInt32
            End With

        Next

        Buffer.Dispose()
    End Sub

    Private Sub Packet_MapData(ByRef data() As Byte)
        Dim X As Integer, Y As Integer, i As Integer
        dim buffer as New ByteStream(Compression.DecompressBytes(Data))

        MapData = False

        SyncLock MapLock
            If Buffer.ReadInt32 = 1 Then
                ClearMap()
                Map.MapNum = Buffer.ReadInt32
                Map.Name = Trim(Buffer.ReadString)
                Map.Music = Trim(Buffer.ReadString)
                Map.Revision = Buffer.ReadInt32
                Map.Moral = Buffer.ReadInt32
                Map.tileset = Buffer.ReadInt32
                Map.Up = Buffer.ReadInt32
                Map.Down = Buffer.ReadInt32
                Map.Left = Buffer.ReadInt32
                Map.Right = Buffer.ReadInt32
                Map.BootMap = Buffer.ReadInt32
                Map.BootX = Buffer.ReadInt32
                Map.BootY = Buffer.ReadInt32
                Map.MaxX = Buffer.ReadInt32
                Map.MaxY = Buffer.ReadInt32
                Map.WeatherType = Buffer.ReadInt32
                Map.FogIndex = Buffer.ReadInt32
                Map.WeatherIntensity = Buffer.ReadInt32
                Map.FogAlpha = Buffer.ReadInt32
                Map.FogSpeed = Buffer.ReadInt32
                Map.HasMapTint = Buffer.ReadInt32
                Map.MapTintR = Buffer.ReadInt32
                Map.MapTintG = Buffer.ReadInt32
                Map.MapTintB = Buffer.ReadInt32
                Map.MapTintA = Buffer.ReadInt32

                Map.Instanced = Buffer.ReadInt32
                Map.Panorama = Buffer.ReadInt32
                Map.Parallax = Buffer.ReadInt32

                ReDim Map.Tile(Map.MaxX,Map.MaxY)

                For X = 1 To MAX_MAP_NPCS
                    Map.Npc(X) = Buffer.ReadInt32
                Next

                For X = 0 To Map.MaxX
                    For Y = 0 To Map.MaxY
                        Map.Tile(X, Y).Data1 = Buffer.ReadInt32
                        Map.Tile(X, Y).Data2 = Buffer.ReadInt32
                        Map.Tile(X, Y).Data3 = Buffer.ReadInt32
                        Map.Tile(X, Y).DirBlock = Buffer.ReadInt32

                        ReDim Map.Tile(X, Y).Layer(LayerType.Count - 1)

                        For i = 0 To LayerType.Count - 1
                            Map.Tile(X, Y).Layer(i).Tileset = Buffer.ReadInt32
                            Map.Tile(X, Y).Layer(i).X = Buffer.ReadInt32
                            Map.Tile(X, Y).Layer(i).Y = Buffer.ReadInt32
                            Map.Tile(X, Y).Layer(i).AutoTile = Buffer.ReadInt32
                        Next
                        Map.Tile(X, Y).Type = Buffer.ReadInt32
                    Next
                Next

                'Event Data!
                ResetEventdata()

                Map.EventCount = Buffer.ReadInt32

                If Map.EventCount > 0 Then
                    ReDim Map.Events(Map.EventCount)
                    For i = 1 To Map.EventCount
                        With Map.Events(i)
                            .Name = Trim(Buffer.ReadString)
                            .Globals = Buffer.ReadInt32
                            .X = Buffer.ReadInt32
                            .Y = Buffer.ReadInt32
                            .PageCount = Buffer.ReadInt32
                        End With
                        If Map.Events(i).PageCount > 0 Then
                            ReDim Map.Events(i).Pages(Map.Events(i).PageCount)
                            For X = 1 To Map.Events(i).PageCount
                                With Map.Events(i).Pages(X)
                                    .chkVariable = Buffer.ReadInt32
                                    .VariableIndex = Buffer.ReadInt32
                                    .VariableCondition = Buffer.ReadInt32
                                    .VariableCompare = Buffer.ReadInt32

                                    .chkSwitch = Buffer.ReadInt32
                                    .SwitchIndex = Buffer.ReadInt32
                                    .SwitchCompare = Buffer.ReadInt32

                                    .chkHasItem = Buffer.ReadInt32
                                    .HasItemIndex = Buffer.ReadInt32
                                    .HasItemAmount = Buffer.ReadInt32

                                    .chkSelfSwitch = Buffer.ReadInt32
                                    .SelfSwitchIndex = Buffer.ReadInt32
                                    .SelfSwitchCompare = Buffer.ReadInt32

                                    .GraphicType = Buffer.ReadInt32
                                    .Graphic = Buffer.ReadInt32
                                    .GraphicX = Buffer.ReadInt32
                                    .GraphicY = Buffer.ReadInt32
                                    .GraphicX2 = Buffer.ReadInt32
                                    .GraphicY2 = Buffer.ReadInt32

                                    .MoveType = Buffer.ReadInt32
                                    .MoveSpeed = Buffer.ReadInt32
                                    .MoveFreq = Buffer.ReadInt32

                                    .MoveRouteCount = Buffer.ReadInt32

                                    .IgnoreMoveRoute = Buffer.ReadInt32
                                    .RepeatMoveRoute = Buffer.ReadInt32

                                    If .MoveRouteCount > 0 Then
                                        ReDim Map.Events(i).Pages(X).MoveRoute(.MoveRouteCount)
                                        For Y = 1 To .MoveRouteCount
                                            .MoveRoute(Y).Index = Buffer.ReadInt32
                                            .MoveRoute(Y).Data1 = Buffer.ReadInt32
                                            .MoveRoute(Y).Data2 = Buffer.ReadInt32
                                            .MoveRoute(Y).Data3 = Buffer.ReadInt32
                                            .MoveRoute(Y).Data4 = Buffer.ReadInt32
                                            .MoveRoute(Y).Data5 = Buffer.ReadInt32
                                            .MoveRoute(Y).Data6 = Buffer.ReadInt32
                                        Next
                                    End If

                                    .WalkAnim = Buffer.ReadInt32
                                    .DirFix = Buffer.ReadInt32
                                    .WalkThrough = Buffer.ReadInt32
                                    .ShowName = Buffer.ReadInt32
                                    .Trigger = Buffer.ReadInt32
                                    .CommandListCount = Buffer.ReadInt32

                                    .Position = Buffer.ReadInt32
                                    .Questnum = Buffer.ReadInt32

                                    .chkPlayerGender = Buffer.ReadInt32
                                End With

                                If Map.Events(i).Pages(X).CommandListCount > 0 Then
                                    ReDim Map.Events(i).Pages(X).CommandList(Map.Events(i).Pages(X).CommandListCount)
                                    For Y = 1 To Map.Events(i).Pages(X).CommandListCount
                                        Map.Events(i).Pages(X).CommandList(Y).CommandCount = Buffer.ReadInt32
                                        Map.Events(i).Pages(X).CommandList(Y).ParentList = Buffer.ReadInt32
                                        If Map.Events(i).Pages(X).CommandList(Y).CommandCount > 0 Then
                                            ReDim Map.Events(i).Pages(X).CommandList(Y).Commands(Map.Events(i).Pages(X).CommandList(Y).CommandCount)
                                            For z = 1 To Map.Events(i).Pages(X).CommandList(Y).CommandCount
                                                With Map.Events(i).Pages(X).CommandList(Y).Commands(z)
                                                    .Index = Buffer.ReadInt32
                                                    .Text1 = Trim(Buffer.ReadString)
                                                    .Text2 = Trim(Buffer.ReadString)
                                                    .Text3 = Trim(Buffer.ReadString)
                                                    .Text4 = Trim(Buffer.ReadString)
                                                    .Text5 = Trim(Buffer.ReadString)
                                                    .Data1 = Buffer.ReadInt32
                                                    .Data2 = Buffer.ReadInt32
                                                    .Data3 = Buffer.ReadInt32
                                                    .Data4 = Buffer.ReadInt32
                                                    .Data5 = Buffer.ReadInt32
                                                    .Data6 = Buffer.ReadInt32
                                                    .ConditionalBranch.CommandList = Buffer.ReadInt32
                                                    .ConditionalBranch.Condition = Buffer.ReadInt32
                                                    .ConditionalBranch.Data1 = Buffer.ReadInt32
                                                    .ConditionalBranch.Data2 = Buffer.ReadInt32
                                                    .ConditionalBranch.Data3 = Buffer.ReadInt32
                                                    .ConditionalBranch.ElseCommandList = Buffer.ReadInt32
                                                    .MoveRouteCount = Buffer.ReadInt32
                                                    If .MoveRouteCount > 0 Then
                                                        ReDim Preserve .MoveRoute(.MoveRouteCount)
                                                        For w = 1 To .MoveRouteCount
                                                            .MoveRoute(w).Index = Buffer.ReadInt32
                                                            .MoveRoute(w).Data1 = Buffer.ReadInt32
                                                            .MoveRoute(w).Data2 = Buffer.ReadInt32
                                                            .MoveRoute(w).Data3 = Buffer.ReadInt32
                                                            .MoveRoute(w).Data4 = Buffer.ReadInt32
                                                            .MoveRoute(w).Data5 = Buffer.ReadInt32
                                                            .MoveRoute(w).Data6 = Buffer.ReadInt32
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
                MapItem(i).Num = Buffer.ReadInt32
                MapItem(i).Value = Buffer.ReadInt32()
                MapItem(i).X = Buffer.ReadInt32()
                MapItem(i).Y = Buffer.ReadInt32()
            Next

            For i = 1 To MAX_MAP_NPCS
                MapNpc(i).Num = Buffer.ReadInt32()
                MapNpc(i).X = Buffer.ReadInt32()
                MapNpc(i).Y = Buffer.ReadInt32()
                MapNpc(i).Dir = Buffer.ReadInt32()
                MapNpc(i).Vital(VitalType.HP) = Buffer.ReadInt32()
                MapNpc(i).Vital(VitalType.MP) = Buffer.ReadInt32()
            Next

            If Buffer.ReadInt32 = 1 Then
                Resource_Index = Buffer.ReadInt32
                Resources_Init = False

                If Resource_Index > 0 Then
                    ReDim MapResource(Resource_Index)

                    For i = 0 To Resource_Index
                        MapResource(i).ResourceState = Buffer.ReadInt32
                        MapResource(i).X = Buffer.ReadInt32
                        MapResource(i).Y = Buffer.ReadInt32
                    Next

                    Resources_Init = True
                Else
                    ReDim MapResource(1)
                End If
            End If

            Buffer.Dispose()

        End SyncLock

        ClearTempTile()
        InitAutotiles()

        MapData = True

        CurrentWeather = Map.WeatherType
        CurrentWeatherIntensity = Map.WeatherIntensity
        CurrentFog = Map.FogIndex
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
        dim buffer as New ByteStream(Data)

        For i = 1 To MAX_MAP_NPCS

            With MapNpc(i)
                .Num = Buffer.ReadInt32
                .X = Buffer.ReadInt32
                .Y = Buffer.ReadInt32
                .Dir = Buffer.ReadInt32
                .Vital(VitalType.HP) = Buffer.ReadInt32
                .Vital(VitalType.MP) = Buffer.ReadInt32
            End With

        Next

        Buffer.Dispose()
    End Sub

    Private Sub Packet_MapNPCUpdate(ByRef data() As Byte)
        Dim NpcNum As Integer
        dim buffer as ByteStream
        Buffer = New ByteStream(Data)

        NpcNum = Buffer.ReadInt32

        With MapNpc(NpcNum)
            .Num = Buffer.ReadInt32
            .X = Buffer.ReadInt32
            .Y = Buffer.ReadInt32
            .Dir = Buffer.ReadInt32
            .Vital(VitalType.HP) = Buffer.ReadInt32
            .Vital(VitalType.MP) = Buffer.ReadInt32
        End With

        Buffer.Dispose()
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

        For i = 0 To StatType.Count - 1
            Npc(i).Stat(i) = buffer.ReadInt32()
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
        dim buffer as New ByteStream(Data)
        shopnum = Buffer.ReadInt32

        Shop(shopnum).BuyRate = Buffer.ReadInt32()
        Shop(shopnum).Name = Trim(Buffer.ReadString())
        Shop(shopnum).Face = Buffer.ReadInt32()

        For i = 0 To MAX_TRADES
            Shop(shopnum).TradeItem(i).CostItem = Buffer.ReadInt32()
            Shop(shopnum).TradeItem(i).CostValue = Buffer.ReadInt32()
            Shop(shopnum).TradeItem(i).Item = Buffer.ReadInt32()
            Shop(shopnum).TradeItem(i).ItemValue = Buffer.ReadInt32()
        Next

        If Shop(shopnum).Name Is Nothing Then Shop(shopnum).Name = ""

        Buffer.Dispose()
    End Sub

    Private Sub Packet_EditSkill(ByRef data() As Byte)
        InitSkillEditor = True
    End Sub

    Private Sub Packet_UpdateSkill(ByRef data() As Byte)
        Dim skillnum As Integer
        dim buffer as New ByteStream(Data)
        skillnum = Buffer.ReadInt32

        Skill(skillnum).AccessReq = Buffer.ReadInt32()
        Skill(skillnum).AoE = Buffer.ReadInt32()
        Skill(skillnum).CastAnim = Buffer.ReadInt32()
        Skill(skillnum).CastTime = Buffer.ReadInt32()
        Skill(skillnum).CdTime = Buffer.ReadInt32()
        Skill(skillnum).ClassReq = Buffer.ReadInt32()
        Skill(skillnum).Dir = Buffer.ReadInt32()
        Skill(skillnum).Duration = Buffer.ReadInt32()
        Skill(skillnum).Icon = Buffer.ReadInt32()
        Skill(skillnum).Interval = Buffer.ReadInt32()
        Skill(skillnum).IsAoE = Buffer.ReadInt32()
        Skill(skillnum).LevelReq = Buffer.ReadInt32()
        Skill(skillnum).Map = Buffer.ReadInt32()
        Skill(skillnum).MpCost = Buffer.ReadInt32()
        Skill(skillnum).Name = Trim(Buffer.ReadString())
        Skill(skillnum).Range = Buffer.ReadInt32()
        Skill(skillnum).SkillAnim = Buffer.ReadInt32()
        Skill(skillnum).StunDuration = Buffer.ReadInt32()
        Skill(skillnum).Type = Buffer.ReadInt32()
        Skill(skillnum).Vital = Buffer.ReadInt32()
        Skill(skillnum).X = Buffer.ReadInt32()
        Skill(skillnum).Y = Buffer.ReadInt32()

        Skill(skillnum).IsProjectile = Buffer.ReadInt32()
        Skill(skillnum).Projectile = Buffer.ReadInt32()

        Skill(skillnum).KnockBack = Buffer.ReadInt32()
        Skill(skillnum).KnockBackTiles = Buffer.ReadInt32()

        If Skill(skillnum).Name Is Nothing Then Skill(skillnum).Name = ""

        Buffer.Dispose()

    End Sub

    Private Sub Packet_ResourceEditor(ByRef data() As Byte)
        InitResourceEditor = True
    End Sub

    Private Sub Packet_UpdateResource(ByRef data() As Byte)
        Dim ResourceNum As Integer
        dim buffer as New ByteStream(Data)
        ResourceNum = Buffer.ReadInt32

        Resource(ResourceNum).Animation = Buffer.ReadInt32()
        Resource(ResourceNum).EmptyMessage = Trim(Buffer.ReadString())
        Resource(ResourceNum).ExhaustedImage = Buffer.ReadInt32()
        Resource(ResourceNum).Health = Buffer.ReadInt32()
        Resource(ResourceNum).ExpReward = Buffer.ReadInt32()
        Resource(ResourceNum).ItemReward = Buffer.ReadInt32()
        Resource(ResourceNum).Name = Trim(Buffer.ReadString())
        Resource(ResourceNum).ResourceImage = Buffer.ReadInt32()
        Resource(ResourceNum).ResourceType = Buffer.ReadInt32()
        Resource(ResourceNum).RespawnTime = Buffer.ReadInt32()
        Resource(ResourceNum).SuccessMessage = Trim(Buffer.ReadString())
        Resource(ResourceNum).LvlRequired = Buffer.ReadInt32()
        Resource(ResourceNum).ToolRequired = Buffer.ReadInt32()
        Resource(ResourceNum).Walkthrough = Buffer.ReadInt32()

        If Resource(ResourceNum).Name Is Nothing Then Resource(ResourceNum).Name = ""
        If Resource(ResourceNum).EmptyMessage Is Nothing Then Resource(ResourceNum).EmptyMessage = ""
        If Resource(ResourceNum).SuccessMessage Is Nothing Then Resource(ResourceNum).SuccessMessage = ""

        Buffer.Dispose()
    End Sub

    Private Sub Packet_EditAnimation(ByRef data() As Byte)
        InitAnimationEditor = True
    End Sub

    Private Sub Packet_UpdateAnimation(ByRef data() As Byte)
        Dim n As Integer, i As Integer
        dim buffer as New ByteStream(Data)
        n = Buffer.ReadInt32
        ' Update the Animation
        For i = 0 To UBound(Animation(n).Frames)
            Animation(n).Frames(i) = Buffer.ReadInt32()
        Next

        For i = 0 To UBound(Animation(n).LoopCount)
            Animation(n).LoopCount(i) = Buffer.ReadInt32()
        Next

        For i = 0 To UBound(Animation(n).LoopTime)
            Animation(n).LoopTime(i) = Buffer.ReadInt32()
        Next

        Animation(n).Name = Trim$(Buffer.ReadString)
        Animation(n).Sound = Trim$(Buffer.ReadString)

        If Animation(n).Name Is Nothing Then Animation(n).Name = ""
        If Animation(n).Sound Is Nothing Then Animation(n).Sound = ""

        For i = 0 To UBound(Animation(n).Sprite)
            Animation(n).Sprite(i) = Buffer.ReadInt32()
        Next
        Buffer.Dispose()
    End Sub

    Private Sub Packet_GameData(ByRef data() As Byte)
        Dim n As Integer, i As Integer, z As Integer, x As Integer, a As Integer, b As Integer
        dim buffer as New ByteStream(Compression.DecompressBytes(Data))

        '\\\Read Class Data\\\

        ' Max classes
        Max_Classes = Buffer.ReadInt32
        ReDim Classes(Max_Classes)

        For i = 0 To Max_Classes
            ReDim Classes(i).Stat(StatType.Count - 1)
        Next

        For i = 0 To Max_Classes
            ReDim Classes(i).Vital(VitalType.Count - 1)
        Next

        For i = 1 To Max_Classes

            With Classes(i)
                .Name = Trim(Buffer.ReadString)
                .Desc = Trim$(Buffer.ReadString)

                .Vital(VitalType.HP) = Buffer.ReadInt32
                .Vital(VitalType.MP) = Buffer.ReadInt32
                .Vital(VitalType.SP) = Buffer.ReadInt32

                ' get array size
                z = Buffer.ReadInt32
                ' redim array
                ReDim .MaleSprite(z)
                ' loop-receive data
                For x = 0 To z
                    .MaleSprite(x) = Buffer.ReadInt32
                Next

                ' get array size
                z = Buffer.ReadInt32
                ' redim array
                ReDim .FemaleSprite(z)
                ' loop-receive data
                For x = 0 To z
                    .FemaleSprite(x) = Buffer.ReadInt32
                Next

                .Stat(StatType.Strength) = Buffer.ReadInt32
                .Stat(StatType.Endurance) = Buffer.ReadInt32
                .Stat(StatType.Vitality) = Buffer.ReadInt32
                .Stat(StatType.Intelligence) = Buffer.ReadInt32
                .Stat(StatType.Luck) = Buffer.ReadInt32
                .Stat(StatType.Spirit) = Buffer.ReadInt32

                ReDim .StartItem(5)
                ReDim .StartValue(5)
                For q = 1 To 5
                    .StartItem(q) = Buffer.ReadInt32
                    .StartValue(q) = Buffer.ReadInt32
                Next

                .StartMap = Buffer.ReadInt32
                .StartX = Buffer.ReadInt32
                .StartY = Buffer.ReadInt32

                .BaseExp = Buffer.ReadInt32
            End With

        Next

        i = 0
        x = 0
        n = 0
        z = 0

        '\\\End Read Class Data\\\

        '\\\Read Item Data\\\\\\\
        x = Buffer.ReadInt32

        For i = 1 To x
            n = Buffer.ReadInt32

            ' Update the item
            Item(n).AccessReq = Buffer.ReadInt32()

            For z = 0 To StatType.Count - 1
                Item(n).Add_Stat(z) = Buffer.ReadInt32()
            Next

            Item(n).Animation = Buffer.ReadInt32()
            Item(n).BindType = Buffer.ReadInt32()
            Item(n).ClassReq = Buffer.ReadInt32()
            Item(n).Data1 = Buffer.ReadInt32()
            Item(n).Data2 = Buffer.ReadInt32()
            Item(n).Data3 = Buffer.ReadInt32()
            Item(n).TwoHanded = Buffer.ReadInt32()
            Item(n).LevelReq = Buffer.ReadInt32()
            Item(n).Mastery = Buffer.ReadInt32()
            Item(n).Name = Trim$(Buffer.ReadString())
            Item(n).Paperdoll = Buffer.ReadInt32()
            Item(n).Pic = Buffer.ReadInt32()
            Item(n).Price = Buffer.ReadInt32()
            Item(n).Rarity = Buffer.ReadInt32()
            Item(n).Speed = Buffer.ReadInt32()

            Item(n).Randomize = Buffer.ReadInt32()
            Item(n).RandomMin = Buffer.ReadInt32()
            Item(n).RandomMax = Buffer.ReadInt32()

            Item(n).Stackable = Buffer.ReadInt32()
            Item(n).Description = Trim$(Buffer.ReadString())

            For z = 0 To StatType.Count - 1
                Item(n).Stat_Req(z) = Buffer.ReadInt32()
            Next

            Item(n).Type = Buffer.ReadInt32()
            Item(n).SubType = Buffer.ReadInt32()

            Item(n).ItemLevel = Buffer.ReadInt32()

            'Housing
            Item(n).FurnitureWidth = Buffer.ReadInt32()
            Item(n).FurnitureHeight = Buffer.ReadInt32()

            For a = 0 To 3
                For b = 0 To 3
                    Item(n).FurnitureBlocks(a, b) = buffer.ReadInt32()
                    Item(n).FurnitureFringe(a, b) = buffer.ReadInt32()
                Next
            Next

            Item(n).KnockBack = Buffer.ReadInt32()
            Item(n).KnockBackTiles = Buffer.ReadInt32()

            Item(n).Projectile = Buffer.ReadInt32()
            Item(n).Ammo = Buffer.ReadInt32()
        Next

        i = 0
        n = 0
        x = 0
        z = 0

        '\\\End Read Item Data\\\\\\\

        '\\\Read Animation Data\\\\\\\
        x = Buffer.ReadInt32

        For i = 1 To x
            n = Buffer.ReadInt32
            ' Update the Animation
            For z = 0 To UBound(Animation(n).Frames)
                Animation(n).Frames(z) = Buffer.ReadInt32()
            Next

            For z = 0 To UBound(Animation(n).LoopCount)
                Animation(n).LoopCount(z) = Buffer.ReadInt32()
            Next

            For z = 0 To UBound(Animation(n).LoopTime)
                Animation(n).LoopTime(z) = Buffer.ReadInt32()
            Next

            Animation(n).Name = Trim(Buffer.ReadString)
            Animation(n).Sound = Trim(Buffer.ReadString)

            If Animation(n).Name Is Nothing Then Animation(n).Name = ""
            If Animation(n).Sound Is Nothing Then Animation(n).Sound = ""

            For z = 0 To UBound(Animation(n).Sprite)
                Animation(n).Sprite(z) = Buffer.ReadInt32()
            Next
        Next

        i = 0
        n = 0
        x = 0
        z = 0

        '\\\End Read Animation Data\\\\\\\

        '\\\Read NPC Data\\\\\\\
        x = Buffer.ReadInt32
        For i = 1 To x
            n = Buffer.ReadInt32
            ' Update the Npc
            Npc(n).Animation = Buffer.ReadInt32()
            Npc(n).AttackSay = Trim(Buffer.ReadString())
            Npc(n).Behaviour = Buffer.ReadInt32()
            For z = 1 To 5
                Npc(n).DropChance(z) = Buffer.ReadInt32()
                Npc(n).DropItem(z) = Buffer.ReadInt32()
                Npc(n).DropItemValue(z) = Buffer.ReadInt32()
            Next

            Npc(n).Exp = Buffer.ReadInt32()
            Npc(n).Faction = Buffer.ReadInt32()
            Npc(n).Hp = Buffer.ReadInt32()
            Npc(n).Name = Trim(Buffer.ReadString())
            Npc(n).Range = Buffer.ReadInt32()
            Npc(n).SpawnTime = Buffer.ReadInt32()
            Npc(n).SpawnSecs = Buffer.ReadInt32()
            Npc(n).Sprite = Buffer.ReadInt32()

            For z = 0 To StatType.Count - 1
                Npc(n).Stat(z) = Buffer.ReadInt32()
            Next

            Npc(n).QuestNum = Buffer.ReadInt32()

            ReDim Npc(n).Skill(MAX_NPC_SKILLS)
            For z = 1 To MAX_NPC_SKILLS
                Npc(n).Skill(z) = Buffer.ReadInt32()
            Next

            Npc(i).Level = Buffer.ReadInt32()
            Npc(i).Damage = Buffer.ReadInt32()

            If Npc(n).AttackSay Is Nothing Then Npc(n).AttackSay = ""
            If Npc(n).Name Is Nothing Then Npc(n).Name = ""
        Next

        i = 0
        n = 0
        x = 0
        z = 0

        '\\\End Read NPC Data\\\\\\\

        '\\\Read Shop Data\\\\\\\
        x = Buffer.ReadInt32

        For i = 1 To x
            n = Buffer.ReadInt32

            Shop(n).BuyRate = Buffer.ReadInt32()
            Shop(n).Name = Trim(Buffer.ReadString())
            Shop(n).Face = Buffer.ReadInt32()

            For z = 0 To MAX_TRADES
                Shop(n).TradeItem(z).CostItem = Buffer.ReadInt32()
                Shop(n).TradeItem(z).CostValue = Buffer.ReadInt32()
                Shop(n).TradeItem(z).Item = Buffer.ReadInt32()
                Shop(n).TradeItem(z).ItemValue = Buffer.ReadInt32()
            Next

            If Shop(n).Name Is Nothing Then Shop(n).Name = ""
        Next

        i = 0
        n = 0
        x = 0
        z = 0

        '\\\End Read Shop Data\\\\\\\

        '\\\Read Skills Data\\\\\\\\\\
        x = Buffer.ReadInt32

        For i = 1 To x
            n = Buffer.ReadInt32

            Skill(n).AccessReq = Buffer.ReadInt32()
            Skill(n).AoE = Buffer.ReadInt32()
            Skill(n).CastAnim = Buffer.ReadInt32()
            Skill(n).CastTime = Buffer.ReadInt32()
            Skill(n).CdTime = Buffer.ReadInt32()
            Skill(n).ClassReq = Buffer.ReadInt32()
            Skill(n).Dir = Buffer.ReadInt32()
            Skill(n).Duration = Buffer.ReadInt32()
            Skill(n).Icon = Buffer.ReadInt32()
            Skill(n).Interval = Buffer.ReadInt32()
            Skill(n).IsAoE = Buffer.ReadInt32()
            Skill(n).LevelReq = Buffer.ReadInt32()
            Skill(n).Map = Buffer.ReadInt32()
            Skill(n).MpCost = Buffer.ReadInt32()
            Skill(n).Name = Trim(Buffer.ReadString())
            Skill(n).Range = Buffer.ReadInt32()
            Skill(n).SkillAnim = Buffer.ReadInt32()
            Skill(n).StunDuration = Buffer.ReadInt32()
            Skill(n).Type = Buffer.ReadInt32()
            Skill(n).Vital = Buffer.ReadInt32()
            Skill(n).X = Buffer.ReadInt32()
            Skill(n).Y = Buffer.ReadInt32()

            Skill(n).IsProjectile = Buffer.ReadInt32()
            Skill(n).Projectile = Buffer.ReadInt32()

            Skill(n).KnockBack = Buffer.ReadInt32()
            Skill(n).KnockBackTiles = Buffer.ReadInt32()

            If Skill(n).Name Is Nothing Then Skill(n).Name = ""
        Next

        i = 0
        x = 0
        n = 0
        z = 0

        '\\\End Read Skills Data\\\\\\\\\\

        '\\\Read Resource Data\\\\\\\\\\\\
        x = Buffer.ReadInt32

        For i = 1 To x
            n = Buffer.ReadInt32

            Resource(n).Animation = Buffer.ReadInt32()
            Resource(n).EmptyMessage = Trim(Buffer.ReadString())
            Resource(n).ExhaustedImage = Buffer.ReadInt32()
            Resource(n).Health = Buffer.ReadInt32()
            Resource(n).ExpReward = Buffer.ReadInt32()
            Resource(n).ItemReward = Buffer.ReadInt32()
            Resource(n).Name = Trim(Buffer.ReadString())
            Resource(n).ResourceImage = Buffer.ReadInt32()
            Resource(n).ResourceType = Buffer.ReadInt32()
            Resource(n).RespawnTime = Buffer.ReadInt32()
            Resource(n).SuccessMessage = Trim(Buffer.ReadString())
            Resource(n).LvlRequired = Buffer.ReadInt32()
            Resource(n).ToolRequired = Buffer.ReadInt32()
            Resource(n).Walkthrough = Buffer.ReadInt32()

            If Resource(n).Name Is Nothing Then Resource(n).Name = ""
            If Resource(n).EmptyMessage Is Nothing Then Resource(n).EmptyMessage = ""
            If Resource(n).SuccessMessage Is Nothing Then Resource(n).SuccessMessage = ""
        Next

        i = 0
        n = 0
        x = 0
        z = 0

        '\\\End Read Resource Data\\\\\\\\\\\\

        Buffer.Dispose()
    End Sub

    Private Sub Packet_Mapreport(ByRef data() As Byte)
        Dim I As Integer
        dim buffer as New ByteStream(Data)
        For I = 1 To MAX_MAPS
            MapNames(I) = Trim(Buffer.ReadString())
        Next

        UpdateMapnames = True

        Buffer.Dispose()
    End Sub

    Private Sub Packet_MapNames(ByRef data() As Byte)
        Dim I As Integer
        dim buffer as New ByteStream(Data)
        For I = 1 To MAX_MAPS
            MapNames(I) = Trim(Buffer.ReadString())
        Next

        Buffer.Dispose()
    End Sub

    Private Sub Packet_ClassEditor(ByRef data() As Byte)
        InitClassEditor = True
    End Sub


End Module
