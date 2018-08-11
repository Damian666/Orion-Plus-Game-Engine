Imports ASFW
Imports ASFW.IO

Module E_NetworkReceive
    Sub PacketRouter()
        Socket.PacketId(ServerPackets.SAlertMsg) = AddressOf Packet_AlertMSG
        Socket.PacketId(ServerPackets.SKeyPair) = AddressOf Packet_KeyPair

        Socket.PacketId(ServerPackets.SLoginOk) = AddressOf Packet_LoginOk
        Socket.PacketId(ServerPackets.SClassesData) = AddressOf Packet_ClassesData
        
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

        'quests
        Socket.PacketId(ServerPackets.SQuestEditor) = AddressOf Packet_QuestEditor
        Socket.PacketId(ServerPackets.SUpdateQuest) = AddressOf Packet_UpdateQuest

        'Housing
        Socket.PacketId(ServerPackets.SHouseConfigs) = AddressOf Packet_HouseConfigurations
        Socket.PacketId(ServerPackets.SFurniture) = AddressOf Packet_Furniture
        Socket.PacketId(ServerPackets.SHouseEdit) = AddressOf Packet_EditHouses
        
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
    
    Private Sub Packet_ClassEditor(ByRef data() As Byte)
        InitClassEditor = True
    End Sub


End Module
