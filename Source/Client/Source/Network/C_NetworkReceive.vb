Imports ASFW
Imports ASFW.IO

Module C_NetworkReceive

    Sub PacketRouter()
        Socket.PacketId(ServerPackets.SAlertMsg) = AddressOf Packet_AlertMSG
        Socket.PacketId(ServerPackets.SKeyPair) = AddressOf Packet_KeyPair
        Socket.PacketId(ServerPackets.SLoadCharOk) = AddressOf Packet_LoadCharOk
        Socket.PacketId(ServerPackets.SLoginOk) = AddressOf Packet_LoginOk
        Socket.PacketId(ServerPackets.SNewCharClasses) = AddressOf Packet_NewCharClasses
        Socket.PacketId(ServerPackets.SClassesData) = AddressOf Packet_ClassesData
        Socket.PacketId(ServerPackets.SInGame) = AddressOf Packet_InGame
        Socket.PacketId(ServerPackets.SPlayerInv) = AddressOf Packet_PlayerInv
        Socket.PacketId(ServerPackets.SPlayerInvUpdate) = AddressOf Packet_PlayerInvUpdate
        Socket.PacketId(ServerPackets.SPlayerWornEq) = AddressOf Packet_PlayerWornEquipment
        Socket.PacketId(ServerPackets.SPlayerHp) = AddressOf Packet_PlayerHP
        Socket.PacketId(ServerPackets.SPlayerMp) = AddressOf Packet_PlayerMP
        Socket.PacketId(ServerPackets.SPlayerSp) = AddressOf Packet_PlayerSP
        Socket.PacketId(ServerPackets.SPlayerStats) = AddressOf Packet_PlayerStats
        Socket.PacketId(ServerPackets.SPlayerData) = AddressOf Packet_PlayerData
        Socket.PacketId(ServerPackets.SPlayerMove) = AddressOf Packet_PlayerMove
        Socket.PacketId(ServerPackets.SNpcMove) = AddressOf Packet_NpcMove
        Socket.PacketId(ServerPackets.SPlayerDir) = AddressOf Packet_PlayerDir
        Socket.PacketId(ServerPackets.SNpcDir) = AddressOf Packet_NpcDir
        Socket.PacketId(ServerPackets.SPlayerXY) = AddressOf Packet_PlayerXY
        Socket.PacketId(ServerPackets.SAttack) = AddressOf Packet_Attack
        Socket.PacketId(ServerPackets.SNpcAttack) = AddressOf Packet_NpcAttack
        Socket.PacketId(ServerPackets.SCheckForMap) = AddressOf Packet_CheckMap
        Socket.PacketId(ServerPackets.SMapData) = AddressOf Packet_MapData
        Socket.PacketId(ServerPackets.SMapNpcData) = AddressOf Packet_MapNPCData
        Socket.PacketId(ServerPackets.SMapNpcUpdate) = AddressOf Packet_MapNPCUpdate
        Socket.PacketId(ServerPackets.SMapDone) = AddressOf Packet_MapDone
        Socket.PacketId(ServerPackets.SGlobalMsg) = AddressOf Packet_GlobalMessage
        Socket.PacketId(ServerPackets.SPlayerMsg) = AddressOf Packet_PlayerMessage
        Socket.PacketId(ServerPackets.SMapMsg) = AddressOf Packet_MapMessage
        Socket.PacketId(ServerPackets.SSpawnItem) = AddressOf Packet_SpawnItem
        Socket.PacketId(ServerPackets.SUpdateItem) = AddressOf Packet_UpdateItem
        Socket.PacketId(ServerPackets.SSpawnNpc) = AddressOf Packet_SpawnNPC
        Socket.PacketId(ServerPackets.SNpcDead) = AddressOf Packet_NpcDead
        Socket.PacketId(ServerPackets.SUpdateNpc) = AddressOf Packet_UpdateNPC
        Socket.PacketId(ServerPackets.SMapKey) = AddressOf Packet_MapKey
        Socket.PacketId(ServerPackets.SEditMap) = AddressOf Packet_EditMap
        Socket.PacketId(ServerPackets.SUpdateShop) = AddressOf Packet_UpdateShop
        Socket.PacketId(ServerPackets.SUpdateSkill) = AddressOf Packet_UpdateSkill
        Socket.PacketId(ServerPackets.SSkills) = AddressOf Packet_Skills
        Socket.PacketId(ServerPackets.SLeftMap) = AddressOf Packet_LeftMap
        Socket.PacketId(ServerPackets.SResourceCache) = AddressOf Packet_ResourceCache
        Socket.PacketId(ServerPackets.SUpdateResource) = AddressOf Packet_UpdateResource
        Socket.PacketId(ServerPackets.SSendPing) = AddressOf Packet_Ping
        Socket.PacketId(ServerPackets.SDoorAnimation) = AddressOf Packet_DoorAnimation
        Socket.PacketId(ServerPackets.SActionMsg) = AddressOf Packet_ActionMessage
        Socket.PacketId(ServerPackets.SPlayerEXP) = AddressOf Packet_PlayerExp
        Socket.PacketId(ServerPackets.SBlood) = AddressOf Packet_Blood
        Socket.PacketId(ServerPackets.SUpdateAnimation) = AddressOf Packet_UpdateAnimation
        Socket.PacketId(ServerPackets.SAnimation) = AddressOf Packet_Animation
        Socket.PacketId(ServerPackets.SMapNpcVitals) = AddressOf Packet_NPCVitals
        Socket.PacketId(ServerPackets.SCooldown) = AddressOf Packet_Cooldown
        Socket.PacketId(ServerPackets.SClearSkillBuffer) = AddressOf Packet_ClearSkillBuffer
        Socket.PacketId(ServerPackets.SSayMsg) = AddressOf Packet_SayMessage
        Socket.PacketId(ServerPackets.SOpenShop) = AddressOf Packet_OpenShop
        Socket.PacketId(ServerPackets.SResetShopAction) = AddressOf Packet_ResetShopAction
        Socket.PacketId(ServerPackets.SStunned) = AddressOf Packet_Stunned
        Socket.PacketId(ServerPackets.SMapWornEq) = AddressOf Packet_MapWornEquipment
        Socket.PacketId(ServerPackets.SBank) = AddressOf Packet_OpenBank
        Socket.PacketId(ServerPackets.SLeftGame) = AddressOf Packet_LeftGame

        Socket.PacketId(ServerPackets.SClearTradeTimer) = AddressOf Packet_ClearTradeTimer
        Socket.PacketId(ServerPackets.STradeInvite) = AddressOf Packet_TradeInvite
        Socket.PacketId(ServerPackets.STrade) = AddressOf Packet_Trade
        Socket.PacketId(ServerPackets.SCloseTrade) = AddressOf Packet_CloseTrade
        Socket.PacketId(ServerPackets.STradeUpdate) = AddressOf Packet_TradeUpdate
        Socket.PacketId(ServerPackets.STradeStatus) = AddressOf Packet_TradeStatus

        Socket.PacketId(ServerPackets.SGameData) = AddressOf Packet_GameData
        Socket.PacketId(ServerPackets.SMapReport) = AddressOf Packet_Mapreport 'Mapreport
        Socket.PacketId(ServerPackets.STarget) = AddressOf Packet_Target

        Socket.PacketId(ServerPackets.SAdmin) = AddressOf Packet_Admin
        Socket.PacketId(ServerPackets.SMapNames) = AddressOf Packet_MapNames

        Socket.PacketId(ServerPackets.SCritical) = AddressOf Packet_Critical
        Socket.PacketId(ServerPackets.SNews) = AddressOf Packet_News
        Socket.PacketId(ServerPackets.SrClick) = AddressOf Packet_RClick
        Socket.PacketId(ServerPackets.STotalOnline) = AddressOf Packet_TotalOnline

        'quests
        Socket.PacketId(ServerPackets.SUpdateQuest) = AddressOf Packet_UpdateQuest
        Socket.PacketId(ServerPackets.SPlayerQuest) = AddressOf Packet_PlayerQuest
        Socket.PacketId(ServerPackets.SPlayerQuests) = AddressOf Packet_PlayerQuests
        Socket.PacketId(ServerPackets.SQuestMessage) = AddressOf Packet_QuestMessage

        'Housing
        Socket.PacketId(ServerPackets.SHouseConfigs) = AddressOf Packet_HouseConfigurations
        Socket.PacketId(ServerPackets.SBuyHouse) = AddressOf Packet_HouseOffer
        Socket.PacketId(ServerPackets.SVisit) = AddressOf Packet_Visit
        Socket.PacketId(ServerPackets.SFurniture) = AddressOf Packet_Furniture

        'hotbar
        Socket.PacketId(ServerPackets.SHotbar) = AddressOf Packet_Hotbar

        'Events
        Socket.PacketId(ServerPackets.SSpawnEvent) = AddressOf Packet_SpawnEvent
        Socket.PacketId(ServerPackets.SEventMove) = AddressOf Packet_EventMove
        Socket.PacketId(ServerPackets.SEventDir) = AddressOf Packet_EventDir
        Socket.PacketId(ServerPackets.SEventChat) = AddressOf Packet_EventChat
        Socket.PacketId(ServerPackets.SEventStart) = AddressOf Packet_EventStart
        Socket.PacketId(ServerPackets.SEventEnd) = AddressOf Packet_EventEnd
        Socket.PacketId(ServerPackets.SPlayBGM) = AddressOf Packet_PlayBGM
        Socket.PacketId(ServerPackets.SPlaySound) = AddressOf Packet_PlaySound
        Socket.PacketId(ServerPackets.SFadeoutBGM) = AddressOf Packet_FadeOutBGM
        Socket.PacketId(ServerPackets.SStopSound) = AddressOf Packet_StopSound
        Socket.PacketId(ServerPackets.SSwitchesAndVariables) = AddressOf Packet_SwitchesAndVariables
        Socket.PacketId(ServerPackets.SMapEventData) = AddressOf Packet_MapEventData
        Socket.PacketId(ServerPackets.SChatBubble) = AddressOf Packet_ChatBubble
        Socket.PacketId(ServerPackets.SSpecialEffect) = AddressOf Packet_SpecialEffect
        'SPic
        Socket.PacketId(ServerPackets.SHoldPlayer) = AddressOf Packet_HoldPlayer

        Socket.PacketId(ServerPackets.SUpdateProjectile) = AddressOf HandleUpdateProjectile
        Socket.PacketId(ServerPackets.SMapProjectile) = AddressOf HandleMapProjectile

        'craft
        Socket.PacketId(ServerPackets.SUpdateRecipe) = AddressOf Packet_UpdateRecipe
        Socket.PacketId(ServerPackets.SSendPlayerRecipe) = AddressOf Packet_SendPlayerRecipe
        Socket.PacketId(ServerPackets.SOpenCraft) = AddressOf Packet_OpenCraft
        Socket.PacketId(ServerPackets.SUpdateCraft) = AddressOf Packet_UpdateCraft

        'emotes
        Socket.PacketId(ServerPackets.SEmote) = AddressOf Packet_Emote

        'party
        Socket.PacketId(ServerPackets.SPartyInvite) = AddressOf Packet_PartyInvite
        Socket.PacketId(ServerPackets.SPartyUpdate) = AddressOf Packet_PartyUpdate
        Socket.PacketId(ServerPackets.SPartyVitals) = AddressOf Packet_PartyVitals

        'pets
        Socket.PacketId(ServerPackets.SUpdatePet) = AddressOf Packet_UpdatePet
        Socket.PacketId(ServerPackets.SUpdatePlayerPet) = AddressOf Packet_UpdatePlayerPet
        Socket.PacketId(ServerPackets.SPetMove) = AddressOf Packet_PetMove
        Socket.PacketId(ServerPackets.SPetDir) = AddressOf Packet_PetDir
        Socket.PacketId(ServerPackets.SPetVital) = AddressOf Packet_PetVital
        Socket.PacketId(ServerPackets.SClearPetSkillBuffer) = AddressOf Packet_ClearPetSkillBuffer
        Socket.PacketId(ServerPackets.SPetAttack) = AddressOf Packet_PetAttack
        Socket.PacketId(ServerPackets.SPetXY) = AddressOf Packet_PetXY
        Socket.PacketId(ServerPackets.SPetExp) = AddressOf Packet_PetExperience

        Socket.PacketId(ServerPackets.SClock) = AddressOf Packet_Clock
        Socket.PacketId(ServerPackets.STime) = AddressOf Packet_Time

        ' EDITOR PACKETS ONLY
        Socket.PacketId(ServerPackets.SItemEditor) = AddressOf Packet_EditItem
        Socket.PacketId(ServerPackets.SREditor) = AddressOf Packet_ResourceEditor
        Socket.PacketId(ServerPackets.SNpcEditor) = AddressOf Packet_NPCEditor
        Socket.PacketId(ServerPackets.SShopEditor) = AddressOf Packet_EditShop
        Socket.PacketId(ServerPackets.SSkillEditor) = AddressOf Packet_EditSkill
        Socket.PacketId(ServerPackets.SResourceEditor) = AddressOf Packet_ResourceEditor
        Socket.PacketId(ServerPackets.SAnimationEditor) = AddressOf Packet_EditAnimation
        Socket.PacketId(ServerPackets.SQuestEditor) = AddressOf Packet_QuestEditor
        Socket.PacketId(ServerPackets.SHouseEdit) = AddressOf Packet_EditHouses
        Socket.PacketId(ServerPackets.SProjectileEditor) = AddressOf HandleProjectileEditor
        Socket.PacketId(ServerPackets.SRecipeEditor) = AddressOf Packet_RecipeEditor
        Socket.PacketId(ServerPackets.SClassEditor) = AddressOf Packet_ClassEditor
        Socket.PacketId(ServerPackets.SAutoMapper) = AddressOf Packet_AutoMapper
        Socket.PacketId(ServerPackets.SPetEditor) = AddressOf Packet_PetEditor
    End Sub

    Private Sub Packet_AlertMSG(ByRef data() As Byte)
        Dim msg As String
        Dim buffer As New ByteStream(data)
        Pnlloadvisible = False

        If FrmMenu.Visible = False Then
            Frmmenuvisible = True
            Frmmaingamevisible = False
        End If

        PnlCharCreateVisible = False
        PnlLoginVisible = False
        PnlRegisterVisible = False
        PnlCharSelectVisible = False

        msg = buffer.ReadString()

        buffer.Dispose()

        MsgBox(msg, vbOKOnly, Settings.GameName)
        DestroyGame()
    End Sub

    Private Sub Packet_KeyPair(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)
        EKeyPair.ImportKeyString(buffer.ReadString())
        buffer.Dispose()
    End Sub

    Private Sub Packet_LoadCharOk(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)
        ' Now we can receive game data
        Myindex = buffer.ReadInt32

        buffer.Dispose()

        Pnlloadvisible = True
        SetStatus(Language.Game.DataReceive)
    End Sub

    Private Sub Packet_LoginOk(ByRef data() As Byte)
        Dim charName As String, sprite As Integer
        Dim level As Integer, className As String, gender As Byte

        ' save options
        Settings.SavePass = ChkSavePassChecked
        Settings.Username = Trim$(TempUserName)

        If ChkSavePassChecked = False Then
            Settings.Password = ""
        Else
            Settings.Password = Trim$(TempPassword)
        End If

        SaveSettings()

        ' Request classes.
        SendRequestClasses()

        Dim buffer As New ByteStream(data)
        ' Now we can receive char data
        MaxChars = buffer.ReadInt32
        ReDim CharSelection(MaxChars)

        SelectedChar = 1

        'reset for deleting chars
        For i = 1 To MaxChars
            CharSelection(i).Name = ""
            CharSelection(i).Sprite = 0
            CharSelection(i).Level = 0
            CharSelection(i).ClassName = ""
            CharSelection(i).Gender = 0
        Next

        For i = 1 To MaxChars
            charName = buffer.ReadString
            sprite = buffer.ReadInt32
            level = buffer.ReadInt32
            className = buffer.ReadString
            gender = buffer.ReadInt32

            CharSelection(i).Name = charName
            CharSelection(i).Sprite = sprite
            CharSelection(i).Level = level
            CharSelection(i).ClassName = className
            CharSelection(i).Gender = gender
        Next

        buffer.Dispose()

        ' Used for if the player is creating a new character
        Frmmenuvisible = True
        Pnlloadvisible = False
        PnlCreditsVisible = False
        PnlRegisterVisible = False
        PnlCharCreateVisible = False
        PnlLoginVisible = False

        PnlCharSelectVisible = True

        FrmMenu.DrawCharacter()

        DrawCharSelect = True

    End Sub



    Private Sub Packet_InGame(ByRef data() As Byte)
        InGame = True
        CanMoveNow = True
        GameInit()
    End Sub

    Private Sub Packet_PlayerInv(ByRef data() As Byte)
        Dim i As Integer, invNum As Integer, amount As Integer
        Dim buffer As New ByteStream(data)
        For i = 1 To MAX_INV
            invNum = buffer.ReadInt32
            amount = buffer.ReadInt32
            SetPlayerInvItemNum(Myindex, i, invNum)
            SetPlayerInvItemValue(Myindex, i, amount)

            Player(Myindex).RandInv(i).Prefix = buffer.ReadString
            Player(Myindex).RandInv(i).Suffix = buffer.ReadString
            Player(Myindex).RandInv(i).Rarity = buffer.ReadInt32
            For n = 1 To StatType.Count - 1
                Player(Myindex).RandInv(i).Stat(n) = buffer.ReadInt32
            Next
            Player(Myindex).RandInv(i).Damage = buffer.ReadInt32
            Player(Myindex).RandInv(i).Speed = buffer.ReadInt32
        Next

        ' changes to inventory, need to clear any drop menu
        FrmGame.pnlCurrency.Visible = False
        FrmGame.txtCurrency.Text = ""
        TmpCurrencyItem = 0
        CurrencyMenu = 0 ' clear

        buffer.Dispose()
    End Sub

    Private Sub Packet_PlayerInvUpdate(ByRef data() As Byte)
        Dim n As Integer, i As Integer
        Dim buffer As New ByteStream(data)
        n = buffer.ReadInt32
        SetPlayerInvItemNum(Myindex, n, buffer.ReadInt32)
        SetPlayerInvItemValue(Myindex, n, buffer.ReadInt32)

        Player(Myindex).RandInv(n).Prefix = buffer.ReadString
        Player(Myindex).RandInv(n).Suffix = buffer.ReadString
        Player(Myindex).RandInv(n).Rarity = buffer.ReadInt32
        For i = 1 To StatType.Count - 1
            Player(Myindex).RandInv(n).Stat(i) = buffer.ReadInt32
        Next
        Player(Myindex).RandInv(n).Damage = buffer.ReadInt32
        Player(Myindex).RandInv(n).Speed = buffer.ReadInt32

        ' changes, clear drop menu
        FrmGame.pnlCurrency.Visible = False
        FrmGame.txtCurrency.Text = ""
        TmpCurrencyItem = 0
        CurrencyMenu = 0 ' clear

        buffer.Dispose()
    End Sub

    Private Sub Packet_PlayerWornEquipment(ByRef data() As Byte)
        Dim i As Integer, n As Integer
        Dim buffer As New ByteStream(data)
        For i = 1 To EquipmentType.Count - 1
            SetPlayerEquipment(Myindex, buffer.ReadInt32, i)
        Next

        For i = 1 To EquipmentType.Count - 1
            Player(Myindex).RandEquip(i).Prefix = buffer.ReadString
            Player(Myindex).RandEquip(i).Suffix = buffer.ReadString
            Player(Myindex).RandEquip(i).Damage = buffer.ReadInt32
            Player(Myindex).RandEquip(i).Speed = buffer.ReadInt32
            Player(Myindex).RandEquip(i).Rarity = buffer.ReadInt32

            For n = 1 To StatType.Count - 1
                Player(Myindex).RandEquip(i).Stat(n) = buffer.ReadInt32
            Next
        Next

        ' changes to inventory, need to clear any drop menu

        FrmGame.pnlCurrency.Visible = False
        FrmGame.txtCurrency.Text = ""
        TmpCurrencyItem = 0
        CurrencyMenu = 0 ' clear

        buffer.Dispose()
    End Sub

    Private Sub Packet_NpcMove(ByRef data() As Byte)
        Dim mapNpcNum As Integer, movement As Integer
        Dim x As Integer, y As Integer, dir As Integer
        Dim buffer As New ByteStream(data)
        mapNpcNum = buffer.ReadInt32
        x = buffer.ReadInt32
        y = buffer.ReadInt32
        dir = buffer.ReadInt32
        movement = buffer.ReadInt32

        With MapNpc(mapNpcNum)
            .X = x
            .Y = y
            .Dir = dir
            .XOffset = 0
            .YOffset = 0
            .Moving = movement

            Select Case .Dir
                Case DirectionType.Up
                    .YOffset = PicY
                Case DirectionType.Down
                    .YOffset = PicY * -1
                Case DirectionType.Left
                    .XOffset = PicX
                Case DirectionType.Right
                    .XOffset = PicX * -1
            End Select
        End With

        buffer.Dispose()
    End Sub

    Private Sub Packet_NpcDir(ByRef data() As Byte)
        Dim dir As Integer, i As Integer
        Dim buffer As New ByteStream(data)
        i = buffer.ReadInt32
        dir = buffer.ReadInt32

        With MapNpc(i)
            .Dir = dir
            .XOffset = 0
            .YOffset = 0
            .Moving = 0
        End With

        buffer.Dispose()
    End Sub

    Private Sub Packet_Attack(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)
        i = buffer.ReadInt32

        ' Set player to attacking
        Player(i).Attacking = 1
        Player(i).AttackTimer = GetTickCount()

        buffer.Dispose()
    End Sub

    Private Sub Packet_NpcAttack(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)
        i = buffer.ReadInt32

        ' Set npc to attacking
        MapNpc(i).Attacking = 1
        MapNpc(i).AttackTimer = GetTickCount()

        buffer.Dispose()
    End Sub

    Private Sub Packet_GlobalMessage(ByRef data() As Byte)
        Dim msg As String
        Dim buffer As New ByteStream(data)

        msg = Trim(buffer.ReadString)

        buffer.Dispose()

        AddText(msg, QColorType.GlobalColor)
    End Sub

    Private Sub Packet_MapMessage(ByRef data() As Byte)
        Dim msg As String
        Dim buffer As New ByteStream(data)

        msg = Trim(buffer.ReadString)

        buffer.Dispose()

        AddText(msg, QColorType.BroadcastColor)

    End Sub

    Private Sub Packet_SpawnItem(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)

        i = buffer.ReadInt32

        With MapItem(i)
            .Num = buffer.ReadInt32
            .Value = buffer.ReadInt32
            .X = buffer.ReadInt32
            .Y = buffer.ReadInt32
        End With

        buffer.Dispose()
    End Sub

    Private Sub Packet_PlayerMessage(ByRef data() As Byte)
        Dim msg As String, colour As Integer
        Dim buffer As New ByteStream(data)

        msg = Trim(buffer.ReadString)

        colour = buffer.ReadInt32

        buffer.Dispose()

        AddText(msg, colour)
    End Sub

    Private Sub Packet_SpawnNPC(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)
        i = buffer.ReadInt32

        With MapNpc(i)
            .Num = buffer.ReadInt32
            .X = buffer.ReadInt32
            .Y = buffer.ReadInt32
            .Dir = buffer.ReadInt32

            For i = 1 To VitalType.Count - 1
                .Vital(i) = buffer.ReadInt32
            Next
            ' Client use only
            .XOffset = 0
            .YOffset = 0
            .Moving = 0
        End With

        buffer.Dispose()
    End Sub

    Private Sub Packet_NpcDead(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)
        i = buffer.ReadInt32
        ClearMapNpc(i)

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

    Private Sub Packet_MapKey(ByRef data() As Byte)
        Dim n As Integer, x As Integer, y As Integer
        Dim buffer As New ByteStream(data)
        x = buffer.ReadInt32
        y = buffer.ReadInt32
        n = buffer.ReadInt32
        TempTile(x, y).DoorOpen = n

        buffer.Dispose()
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

    Private Sub Packet_Skills(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)
        For i = 1 To MAX_PLAYER_SKILLS
            PlayerSkills(i) = buffer.ReadInt32
        Next

        buffer.Dispose()
    End Sub

    Private Sub Packet_LeftMap(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)
        ClearPlayer(buffer.ReadInt32)

        buffer.Dispose()
    End Sub

    Private Sub Packet_Ping(ByRef data() As Byte)
        PingEnd = GetTickCount()
        Ping = PingEnd - PingStart
    End Sub

    Private Sub Packet_DoorAnimation(ByRef data() As Byte)
        Dim x As Integer, y As Integer
        Dim buffer As New ByteStream(data)
        x = buffer.ReadInt32
        y = buffer.ReadInt32
        With TempTile(x, y)
            .DoorFrame = 1
            .DoorAnimate = 1 ' 0 = nothing| 1 = opening | 2 = closing
            .DoorTimer = GetTickCount()
        End With

        buffer.Dispose()
    End Sub

    Private Sub Packet_ActionMessage(ByRef data() As Byte)
        Dim x As Integer, y As Integer, message As String, color As Integer, tmpType As Integer
        Dim buffer As New ByteStream(data)
        message = Trim(buffer.ReadString)
        color = buffer.ReadInt32
        tmpType = buffer.ReadInt32
        x = buffer.ReadInt32
        y = buffer.ReadInt32

        buffer.Dispose()

        CreateActionMsg(message, color, tmpType, x, y)
    End Sub

    Private Sub Packet_Blood(ByRef data() As Byte)
        Dim x As Integer, y As Integer, sprite As Integer
        Dim buffer As New ByteStream(data)
        x = buffer.ReadInt32
        y = buffer.ReadInt32

        ' randomise sprite
        sprite = Rand(1, 3)

        BloodIndex = BloodIndex + 1
        If BloodIndex >= Byte.MaxValue Then BloodIndex = 1

        With Blood(BloodIndex)
            .X = x
            .Y = y
            .Sprite = sprite
            .Timer = GetTickCount()
        End With

        buffer.Dispose()
    End Sub



    Private Sub Packet_NPCVitals(ByRef data() As Byte)
        Dim mapNpcNum As Integer
        Dim buffer As New ByteStream(data)
        mapNpcNum = buffer.ReadInt32
        For i = 1 To VitalType.Count - 1
            MapNpc(mapNpcNum).Vital(i) = buffer.ReadInt32
        Next

        buffer.Dispose()
    End Sub

    Private Sub Packet_Cooldown(ByRef data() As Byte)
        Dim slot As Integer
        Dim buffer As New ByteStream(data)
        slot = buffer.ReadInt32
        SkillCd(slot) = GetTickCount()

        buffer.Dispose()
    End Sub

    Private Sub Packet_ClearSkillBuffer(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)
        SkillBuffer = 0
        SkillBufferTimer = 0

        buffer.Dispose()
    End Sub

    Private Sub Packet_SayMessage(ByRef data() As Byte)
        Dim access As Integer, name As String, message As String
        Dim header As String, pk As Integer
        Dim buffer As New ByteStream(data)
        name = Trim(buffer.ReadString)
        access = buffer.ReadInt32
        pk = buffer.ReadInt32
        message = Trim(buffer.ReadString)
        header = Trim(buffer.ReadString)

        AddText(header & name & ": " & message, QColorType.SayColor)

        buffer.Dispose()
    End Sub

    Private Sub Packet_Stunned(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)
        StunDuration = buffer.ReadInt32

        buffer.Dispose()
    End Sub

    Private Sub Packet_MapWornEquipment(ByRef data() As Byte)
        Dim playernum As Integer
        Dim buffer As New ByteStream(data)
        playernum = buffer.ReadInt32
        SetPlayerEquipment(playernum, buffer.ReadInt32, EquipmentType.Armor)
        SetPlayerEquipment(playernum, buffer.ReadInt32, EquipmentType.Weapon)
        SetPlayerEquipment(playernum, buffer.ReadInt32, EquipmentType.Helmet)
        SetPlayerEquipment(playernum, buffer.ReadInt32, EquipmentType.Shield)
        SetPlayerEquipment(playernum, buffer.ReadInt32, EquipmentType.Shoes)
        SetPlayerEquipment(playernum, buffer.ReadInt32, EquipmentType.Gloves)

        buffer.Dispose()
    End Sub

    Private Sub Packet_GameData(ByRef data() As Byte)
        Dim n As Integer, i As Integer, z As Integer, x As Integer, a As Integer, b As Integer
        Dim buffer As New ByteStream(Compression.DecompressBytes(data))

        '\\\Read Class Data\\\

        ' Max classes

        For i = 0 To MAX_CLASSES
            ReDim Classes(i).Stat(StatType.Count - 1)
        Next

        For i = 0 To MAX_CLASSES
            ReDim Classes(i).Vital(VitalType.Count - 1)
        Next

        For i = 1 To MAX_CLASSES

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
            Item(n).SubType = buffer.ReadInt32

            Item(n).ItemLevel = buffer.ReadInt32

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

        ' changes to inventory, need to clear any drop menu

        FrmGame.pnlCurrency.Visible = False
        FrmGame.txtCurrency.Text = ""
        TmpCurrencyItem = 0
        CurrencyMenu = 0 ' clear

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

            Animation(n).Name = Trim$(buffer.ReadString)
            Animation(n).Sound = Trim$(buffer.ReadString)

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

    Private Sub Packet_Target(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)
        MyTarget = buffer.ReadInt32
        MyTargetType = buffer.ReadInt32

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

    Private Sub Packet_Admin(ByRef data() As Byte)
        Adminvisible = True
    End Sub

    Private Sub Packet_MapNames(ByRef data() As Byte)
        Dim I As Integer
        Dim buffer As New ByteStream(data)
        For I = 1 To MAX_MAPS
            MapNames(I) = Trim(buffer.ReadString())
        Next

        buffer.Dispose()
    End Sub



    Private Sub Packet_Critical(ByRef data() As Byte)
        ShakeTimerEnabled = True
        ShakeTimer = GetTickCount()
    End Sub

    Private Sub Packet_News(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)
        Settings.GameName = buffer.ReadString
        News = buffer.ReadString

        UpdateNews = True

        buffer.Dispose()
    End Sub

    Private Sub Packet_RClick(ByRef data() As Byte)
        ShowRClick = True
    End Sub

    Private Sub Packet_TotalOnline(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)
        TotalOnline = buffer.ReadInt32

        buffer.Dispose()
    End Sub

    Private Sub Packet_Emote(ByRef data() As Byte)
        Dim index As Integer, emote As Integer
        Dim buffer As New ByteStream(data)
        index = buffer.ReadInt32
        emote = buffer.ReadInt32

        With Player(index)
            .Emote = emote
            .EmoteTimer = GetTickCount() + 5000
        End With

        buffer.Dispose()

    End Sub

    Private Sub Packet_ChatBubble(ByRef data() As Byte)
        Dim targetType As Integer, target As Integer, message As String, colour As Integer
        Dim buffer As New ByteStream(data)

        target = buffer.ReadInt32
        targetType = buffer.ReadInt32
        message = Trim(buffer.ReadString)
        colour = buffer.ReadInt32
        AddChatBubble(target, targetType, message, colour)

        buffer.Dispose()

    End Sub

    Private Sub Packet_LeftGame(ByRef data() As Byte)
        DestroyGame()
    End Sub

    '**********************
    '***  EDITOR STUFF  ***
    '**********************

    Private Sub Packet_EditAnimation(ByRef data() As Byte)
        InitAnimationEditor = True
    End Sub

    Private Sub Packet_ClassEditor(ByRef data() As Byte)
        InitClassEditor = True
    End Sub

    Sub Packet_EditItem(ByRef data() As Byte)
        InitItemEditor = True
    End Sub

    Private Sub Packet_NPCEditor(ByRef data() As Byte)
        InitNPCEditor = True
    End Sub

    Private Sub Packet_ResourceEditor(ByRef data() As Byte)
        InitResourceEditor = True
    End Sub

    Friend Sub Packet_PetEditor(ByRef data() As Byte)
        InitPetEditor = True
    End Sub

    Friend Sub HandleProjectileEditor(ByRef data() As Byte)
        InitProjectileEditor = True
    End Sub

    Private Sub Packet_EditShop(ByRef data() As Byte)
        InitShopEditor = True
    End Sub

    Private Sub Packet_EditSkill(ByRef data() As Byte)
        InitSkillEditor = True
    End Sub


End Module