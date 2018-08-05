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
        'SChatBubble
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
    End Sub

    Private Sub Packet_AlertMSG(ByRef data() As Byte)
        Dim msg As String
        dim buffer as New ByteStream(Data)
        pnlloadvisible = False

        If FrmMenu.Visible = False Then
            frmmenuvisible = True
            frmmaingamevisible = False
        End If

        pnlCharCreateVisible = False
        pnlLoginVisible = False
        pnlRegisterVisible = False
        pnlCharSelectVisible = False

        msg = buffer.ReadString()

        buffer.Dispose()

        MsgBox(Msg, vbOKOnly, GameName)
        DestroyGame()
    End Sub

    Private Sub Packet_KeyPair(ByRef data() As Byte)
        dim buffer as New ByteStream(Data)
        EKeyPair.ImportKeyString(Buffer.ReadString())
        Buffer.Dispose()
    End Sub

    Private Sub Packet_LoadCharOk(ByRef data() As Byte)
        dim buffer as New ByteStream(Data)
        ' Now we can receive game data
        MyIndex = Buffer.ReadInt32

        Buffer.Dispose()

        pnlloadvisible = True
        SetStatus(Strings.Get("gamegui", "datarecieve"))
    End Sub

    Private Sub Packet_LoginOk(ByRef data() As Byte)
        Dim charName As String, sprite As Integer
        Dim level As Integer, className As String, gender As Byte

        ' save options
        Options.SavePass = chkSavePassChecked
        Options.Username = Trim$(tempUserName)

        If chkSavePassChecked = False Then
            Options.Password = ""
        Else
            Options.Password = Trim$(tempPassword)
        End If

        SaveOptions()

        ' Request classes.
        SendRequestClasses()

        dim buffer as New ByteStream(Data)
        ' Now we can receive char data
        MaxChars = Buffer.ReadInt32
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
            sprite = Buffer.ReadInt32
            Level = Buffer.ReadInt32
            className = buffer.ReadString
            gender = Buffer.ReadInt32

            CharSelection(i).Name = CharName
            CharSelection(i).Sprite = Sprite
            CharSelection(i).Level = Level
            CharSelection(i).ClassName = ClassName
            CharSelection(i).Gender = Gender
        Next

        Buffer.Dispose()

        ' Used for if the player is creating a new character
        frmmenuvisible = True
        pnlloadvisible = False
        pnlCreditsVisible = False
        pnlRegisterVisible = False
        pnlCharCreateVisible = False
        pnlLoginVisible = False

        pnlCharSelectVisible = True

        FrmMenu.DrawCharacter()

        DrawCharSelect = True

    End Sub

    Private Sub Packet_NewCharClasses(ByRef data() As Byte)
        Dim i As Integer, z As Integer, x As Integer
        dim buffer as New ByteStream(Data)
        ' Max classes
        MaxClasses = Buffer.ReadInt32
        ReDim Classes(MaxClasses)

        SelectedChar = 1

        For i = 1 To MaxClasses

            With Classes(i)
                .Name = Trim(Buffer.ReadString)
                .Desc = Trim(Buffer.ReadString)

                ReDim .Vital(VitalType.Count - 1)

                .Vital(VitalType.HP) = Buffer.ReadInt32
                .Vital(VitalType.MP) = Buffer.ReadInt32
                .Vital(VitalType.SP) = Buffer.ReadInt32

                ' get array size
                z = Buffer.ReadInt32
                ' redim array
                ReDim .MaleSprite(z + 1)
                ' loop-receive data
                For X = 1 To z + 1
                    .MaleSprite(X) = Buffer.ReadInt32
                Next

                ' get array size
                z = Buffer.ReadInt32
                ' redim array
                ReDim .FemaleSprite(z + 1)
                ' loop-receive data
                For X = 1 To z + 1
                    .FemaleSprite(X) = Buffer.ReadInt32
                Next

                ReDim .Stat(StatType.Count - 1)

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

        ' Used for if the player is creating a new character
        frmmenuvisible = True
        pnlloadvisible = False
        pnlCreditsVisible = False
        pnlRegisterVisible = False
        pnlCharCreateVisible = True
        pnlLoginVisible = False

        ReDim cmbclass(MaxClasses)

        For i = 1 To MaxClasses
            cmbclass(i) = Classes(i).Name
        Next

        FrmMenu.DrawCharacter()

        newCharSprite = 1
    End Sub

    Private Sub Packet_ClassesData(ByRef data() As Byte)
        Dim i As Integer, z As Integer, x As Integer
        dim buffer as New ByteStream(Data)
        ' Max classes
        MaxClasses = Buffer.ReadInt32
        ReDim Classes(MaxClasses)

        SelectedChar = 1

        For i = 1 To MaxClasses

            With Classes(i)
                .Name = Trim(Buffer.ReadString)
                .Desc = Trim(Buffer.ReadString)

                ReDim .Vital(VitalType.Count - 1)

                .Vital(VitalType.HP) = Buffer.ReadInt32
                .Vital(VitalType.MP) = Buffer.ReadInt32
                .Vital(VitalType.SP) = Buffer.ReadInt32

                ' get array size
                z = Buffer.ReadInt32
                ' redim array
                ReDim .MaleSprite(z + 1)
                ' loop-receive data
                For X = 1 To z + 1
                    .MaleSprite(X) = Buffer.ReadInt32
                Next

                ' get array size
                z = Buffer.ReadInt32
                ' redim array
                ReDim .FemaleSprite(z + 1)
                ' loop-receive data
                For X = 1 To z + 1
                    .FemaleSprite(X) = Buffer.ReadInt32
                Next

                ReDim .Stat(StatType.Count - 1)

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

        ReDim cmbclass(MaxClasses)
        For i = 1 To MaxClasses
            cmbclass(i) = Classes(i).Name
        Next
        FrmMenu.DrawCharacter()
        newCharSprite = 1

        Buffer.Dispose()
    End Sub

    Private Sub Packet_InGame(ByRef data() As Byte)
        InGame = True
        CanMoveNow = True
        GameInit()
    End Sub

    Private Sub Packet_PlayerInv(ByRef data() As Byte)
        Dim i As Integer, invNum As Integer, amount As Integer
        dim buffer as New ByteStream(Data)
        For i = 1 To MAX_INV
            InvNum = Buffer.ReadInt32
            Amount = Buffer.ReadInt32
            SetPlayerInvItemNum(MyIndex, i, InvNum)
            SetPlayerInvItemValue(MyIndex, i, Amount)

            Player(MyIndex).RandInv(i).Prefix = Buffer.ReadString
            Player(MyIndex).RandInv(i).Suffix = Buffer.ReadString
            Player(MyIndex).RandInv(i).Rarity = Buffer.ReadInt32
            For n = 1 To StatType.Count - 1
                Player(MyIndex).RandInv(i).Stat(n) = Buffer.ReadInt32
            Next
            Player(MyIndex).RandInv(i).Damage = Buffer.ReadInt32
            Player(MyIndex).RandInv(i).Speed = Buffer.ReadInt32
        Next

        ' changes to inventory, need to clear any drop menu
        frmGame.pnlCurrency.Visible = False
        frmGame.txtCurrency.Text = ""
        tmpCurrencyItem = 0
        CurrencyMenu = 0 ' clear

        Buffer.Dispose()
    End Sub

    Private Sub Packet_PlayerInvUpdate(ByRef data() As Byte)
        Dim n As Integer, i As Integer
        dim buffer as New ByteStream(Data)
        n = Buffer.ReadInt32
        SetPlayerInvItemNum(MyIndex, n, Buffer.ReadInt32)
        SetPlayerInvItemValue(MyIndex, n, Buffer.ReadInt32)

        Player(MyIndex).RandInv(n).Prefix = Buffer.ReadString
        Player(MyIndex).RandInv(n).Suffix = Buffer.ReadString
        Player(MyIndex).RandInv(n).Rarity = Buffer.ReadInt32
        For i = 1 To StatType.Count - 1
            Player(MyIndex).RandInv(n).Stat(i) = Buffer.ReadInt32
        Next
        Player(MyIndex).RandInv(n).Damage = Buffer.ReadInt32
        Player(MyIndex).RandInv(n).Speed = Buffer.ReadInt32

        ' changes, clear drop menu
        frmGame.pnlCurrency.Visible = False
        frmGame.txtCurrency.Text = ""
        tmpCurrencyItem = 0
        CurrencyMenu = 0 ' clear

        Buffer.Dispose()
    End Sub

    Private Sub Packet_PlayerWornEquipment(ByRef data() As Byte)
        Dim i As Integer, n As Integer
        dim buffer as New ByteStream(Data)
        For i = 1 To EquipmentType.Count - 1
            SetPlayerEquipment(MyIndex, Buffer.ReadInt32, i)
        Next

        For i = 1 To EquipmentType.Count - 1
            Player(MyIndex).RandEquip(i).Prefix = Buffer.ReadString
            Player(MyIndex).RandEquip(i).Suffix = Buffer.ReadString
            Player(MyIndex).RandEquip(i).Damage = Buffer.ReadInt32
            Player(MyIndex).RandEquip(i).Speed = Buffer.ReadInt32
            Player(MyIndex).RandEquip(i).Rarity = Buffer.ReadInt32

            For n = 1 To StatType.Count - 1
                Player(MyIndex).RandEquip(i).Stat(n) = Buffer.ReadInt32
            Next
        Next

        ' changes to inventory, need to clear any drop menu

        frmGame.pnlCurrency.Visible = False
        frmGame.txtCurrency.Text = ""
        tmpCurrencyItem = 0
        CurrencyMenu = 0 ' clear

        Buffer.Dispose()
    End Sub

    Private Sub Packet_PlayerHP(ByRef data() As Byte)
        dim buffer as New ByteStream(Data)
        Player(MyIndex).MaxHP = Buffer.ReadInt32

        SetPlayerVital(MyIndex, VitalType.HP, Buffer.ReadInt32)

        If GetPlayerMaxVital(MyIndex, VitalType.HP) > 0 Then
            lblHPText = GetPlayerVital(MyIndex, VitalType.HP) & "/" & GetPlayerMaxVital(MyIndex, VitalType.HP)
            ' hp bar
            picHpWidth = Int(((GetPlayerVital(MyIndex, VitalType.HP) / 169) / (GetPlayerMaxVital(MyIndex, VitalType.HP) / 169)) * 169)
        End If

        Buffer.Dispose()
    End Sub

    Private Sub Packet_PlayerMP(ByRef data() As Byte)
        dim buffer as New ByteStream(Data)
        Player(MyIndex).MaxMP = Buffer.ReadInt32
        SetPlayerVital(MyIndex, VitalType.MP, Buffer.ReadInt32)

        If GetPlayerMaxVital(MyIndex, VitalType.MP) > 0 Then
            lblManaText = GetPlayerVital(MyIndex, VitalType.MP) & "/" & GetPlayerMaxVital(MyIndex, VitalType.MP)
            ' mp bar
            picManaWidth = Int(((GetPlayerVital(MyIndex, VitalType.MP) / 169) / (GetPlayerMaxVital(MyIndex, VitalType.MP) / 169)) * 169)
        End If

        Buffer.Dispose()
    End Sub

    Private Sub Packet_PlayerSP(ByRef data() As Byte)
        dim buffer as New ByteStream(Data)
        Player(MyIndex).MaxSP = Buffer.ReadInt32
        SetPlayerVital(MyIndex, VitalType.SP, Buffer.ReadInt32)

        Buffer.Dispose()
    End Sub

    Private Sub Packet_PlayerStats(ByRef data() As Byte)
        Dim i As Integer, index as integer
        dim buffer as New ByteStream(Data)
        index = Buffer.ReadInt32
        For i = 1 To StatType.Count - 1
            SetPlayerStat(index, i, Buffer.ReadInt32)
        Next
        UpdateCharacterPanel = True

        Buffer.Dispose()
    End Sub

    Private Sub Packet_PlayerData(ByRef data() As Byte)
        Dim i As Integer, x As Integer
        dim buffer as New ByteStream(Data)
        i = Buffer.ReadInt32
        SetPlayerName(i, Buffer.ReadString)
        SetPlayerClass(i, Buffer.ReadInt32)
        SetPlayerLevel(i, Buffer.ReadInt32)
        SetPlayerPOINTS(i, Buffer.ReadInt32)
        SetPlayerSprite(i, Buffer.ReadInt32)
        SetPlayerMap(i, Buffer.ReadInt32)
        SetPlayerX(i, Buffer.ReadInt32)
        SetPlayerY(i, Buffer.ReadInt32)
        SetPlayerDir(i, Buffer.ReadInt32)
        SetPlayerAccess(i, Buffer.ReadInt32)
        SetPlayerPK(i, Buffer.ReadInt32)

        For X = 1 To StatType.Count - 1
            SetPlayerStat(i, X, Buffer.ReadInt32)
        Next

        Player(i).InHouse = Buffer.ReadInt32

        For X = 0 To ResourceSkills.Count - 1
            Player(i).GatherSkills(X).SkillLevel = Buffer.ReadInt32
            Player(i).GatherSkills(X).SkillCurExp = Buffer.ReadInt32
            Player(i).GatherSkills(X).SkillNextLvlExp = Buffer.ReadInt32
        Next

        For X = 1 To MAX_RECIPE
            Player(i).RecipeLearned(X) = Buffer.ReadInt32
        Next

        ' Check if the player is the client player
        If i = MyIndex Then
            ' Reset directions
            DirUp = False
            DirDown = False
            DirLeft = False
            DirRight = False

            UpdateCharacterPanel = True
        End If

        ' Make sure they aren't walking
        Player(i).Moving = 0
        Player(i).XOffset = 0
        Player(i).YOffset = 0

        If i = MyIndex Then PlayerData = True

        Buffer.Dispose()
    End Sub

    Private Sub Packet_PlayerMove(ByRef data() As Byte)
        Dim i As Integer, x As Integer, y As Integer
        Dim dir As Integer, n As Byte
        dim buffer as New ByteStream(Data)
        i = Buffer.ReadInt32
        X = Buffer.ReadInt32
        Y = Buffer.ReadInt32
        Dir = Buffer.ReadInt32
        n = Buffer.ReadInt32

        SetPlayerX(i, X)
        SetPlayerY(i, Y)
        SetPlayerDir(i, Dir)
        Player(i).XOffset = 0
        Player(i).YOffset = 0
        Player(i).Moving = n

        Select Case GetPlayerDir(i)
            Case DirectionType.Up
                Player(i).YOffset = PicY
            Case DirectionType.Down
                Player(i).YOffset = PicY * -1
            Case DirectionType.Left
                Player(i).XOffset = PicX
            Case DirectionType.Right
                Player(i).XOffset = PicX * -1
        End Select

        Buffer.Dispose()
    End Sub

    Private Sub Packet_NpcMove(ByRef data() As Byte)
        Dim mapNpcNum As Integer, movement As Integer
        Dim x As Integer, y As Integer, dir As Integer
        dim buffer as New ByteStream(Data)
        MapNpcNum = Buffer.ReadInt32
        X = Buffer.ReadInt32
        Y = Buffer.ReadInt32
        Dir = Buffer.ReadInt32
        Movement = Buffer.ReadInt32

        With MapNpc(MapNpcNum)
            .X = X
            .Y = Y
            .Dir = Dir
            .XOffset = 0
            .YOffset = 0
            .Moving = Movement

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

        Buffer.Dispose()
    End Sub

    Private Sub Packet_PlayerDir(ByRef data() As Byte)
        Dim dir As Integer, i As Integer
        dim buffer as New ByteStream(Data)
        i = Buffer.ReadInt32
        Dir = Buffer.ReadInt32

        SetPlayerDir(i, Dir)

        With Player(i)
            .XOffset = 0
            .YOffset = 0
            .Moving = 0
        End With

        Buffer.Dispose()
    End Sub

    Private Sub Packet_NpcDir(ByRef data() As Byte)
        Dim dir As Integer, i As Integer
        dim buffer as New ByteStream(Data)
        i = Buffer.ReadInt32
        Dir = Buffer.ReadInt32

        With MapNpc(i)
            .Dir = Dir
            .XOffset = 0
            .YOffset = 0
            .Moving = 0
        End With

        Buffer.Dispose()
    End Sub

    Private Sub Packet_PlayerXY(ByRef data() As Byte)
        Dim x As Integer, y As Integer, dir As Integer
        dim buffer as New ByteStream(Data)
        X = Buffer.ReadInt32
        Y = Buffer.ReadInt32
        Dir = Buffer.ReadInt32

        SetPlayerX(MyIndex, X)
        SetPlayerY(MyIndex, Y)
        SetPlayerDir(MyIndex, Dir)

        ' Make sure they aren't walking
        Player(MyIndex).Moving = 0
        Player(MyIndex).XOffset = 0
        Player(MyIndex).YOffset = 0

        Buffer.Dispose()
    End Sub

    Private Sub Packet_Attack(ByRef data() As Byte)
        Dim i As Integer
        dim buffer as New ByteStream(Data)
        i = Buffer.ReadInt32

        ' Set player to attacking
        Player(i).Attacking = 1
        Player(i).AttackTimer = GetTickCount()

        Buffer.Dispose()
    End Sub

    Private Sub Packet_NpcAttack(ByRef data() As Byte)
        Dim i As Integer
        dim buffer as New ByteStream(Data)
        i = Buffer.ReadInt32

        ' Set npc to attacking
        MapNpc(i).Attacking = 1
        MapNpc(i).AttackTimer = GetTickCount()

        Buffer.Dispose()
    End Sub

    Private Sub Packet_GlobalMessage(ByRef data() As Byte)
        Dim msg As String
        dim buffer as New ByteStream(Data)

        msg = Trim(buffer.ReadString)

        buffer.Dispose()

        AddText(Msg, QColorType.GlobalColor)
    End Sub

    Private Sub Packet_MapMessage(ByRef data() As Byte)
        Dim msg As String
        dim buffer as New ByteStream(Data)

        msg = Trim(buffer.ReadString)

        buffer.Dispose()

        AddText(Msg, QColorType.BroadcastColor)

    End Sub

    Private Sub Packet_SpawnItem(ByRef data() As Byte)
        Dim i As Integer
        dim buffer as New ByteStream(Data)

        i = Buffer.ReadInt32

        With MapItem(i)
            .Num = Buffer.ReadInt32
            .Value = Buffer.ReadInt32
            .X = Buffer.ReadInt32
            .Y = Buffer.ReadInt32
        End With

        Buffer.Dispose()
    End Sub

    Private Sub Packet_PlayerMessage(ByRef data() As Byte)
        Dim msg As String, colour As Integer
        dim buffer as New ByteStream(Data)

        msg = Trim(buffer.ReadString)

        colour = Buffer.ReadInt32

        Buffer.Dispose()

        AddText(Msg, colour)
    End Sub



    Private Sub Packet_SpawnNPC(ByRef data() As Byte)
        Dim i As Integer
        dim buffer as New ByteStream(Data)
        i = Buffer.ReadInt32

        With MapNpc(i)
            .Num = Buffer.ReadInt32
            .X = Buffer.ReadInt32
            .Y = Buffer.ReadInt32
            .Dir = Buffer.ReadInt32

            For i = 1 To VitalType.Count - 1
                .Vital(i) = Buffer.ReadInt32
            Next
            ' Client use only
            .XOffset = 0
            .YOffset = 0
            .Moving = 0
        End With

        Buffer.Dispose()
    End Sub

    Private Sub Packet_NpcDead(ByRef data() As Byte)
        Dim i As Integer
        dim buffer as New ByteStream(Data)
        i = Buffer.ReadInt32
        ClearMapNpc(i)

        Buffer.Dispose()
    End Sub

    Private Sub Packet_UpdateNPC(ByRef data() As Byte)
        Dim i As Integer, x As Integer
        dim buffer as New ByteStream(Data)
        i = Buffer.ReadInt32

        ' Update the Npc
        Npc(i).Animation = Buffer.ReadInt32()
        Npc(i).AttackSay = Trim(Buffer.ReadString())
        Npc(i).Behaviour = Buffer.ReadInt32()
        ReDim Npc(i).DropChance(5)
        ReDim Npc(i).DropItem(5)
        ReDim Npc(i).DropItemValue(5)
        For x = 1 To 5
            Npc(i).DropChance(x) = Buffer.ReadInt32()
            Npc(i).DropItem(x) = Buffer.ReadInt32()
            Npc(i).DropItemValue(x) = Buffer.ReadInt32()
        Next

        Npc(i).Exp = Buffer.ReadInt32()
        Npc(i).Faction = Buffer.ReadInt32()
        Npc(i).Hp = Buffer.ReadInt32()
        Npc(i).Name = Trim(Buffer.ReadString())
        Npc(i).Range = Buffer.ReadInt32()
        Npc(i).SpawnTime = Buffer.ReadInt32()
        Npc(i).SpawnSecs = Buffer.ReadInt32()
        Npc(i).Sprite = Buffer.ReadInt32()

        For i = 0 To StatType.Count - 1
            Npc(i).Stat(i) = Buffer.ReadInt32()
        Next

        Npc(i).QuestNum = Buffer.ReadInt32()

        For x = 1 To MAX_NPC_SKILLS
            Npc(i).Skill(x) = Buffer.ReadInt32()
        Next

        Npc(i).Level = Buffer.ReadInt32()
        Npc(i).Damage = Buffer.ReadInt32()

        If Npc(i).AttackSay Is Nothing Then Npc(i).AttackSay = ""
        If Npc(i).Name Is Nothing Then Npc(i).Name = ""

        Buffer.Dispose()
    End Sub

    Private Sub Packet_MapKey(ByRef data() As Byte)
        Dim n As Integer, x As Integer, y As Integer
        dim buffer as New ByteStream(Data)
        X = Buffer.ReadInt32
        Y = Buffer.ReadInt32
        n = Buffer.ReadInt32
        TempTile(X, Y).DoorOpen = n

        Buffer.Dispose()
    End Sub

    Private Sub Packet_EditMap(ByRef data() As Byte)
        dim buffer as New ByteStream(Data)
        InitMapEditor = True

        Buffer.Dispose()
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

    Private Sub Packet_Skills(ByRef data() As Byte)
        Dim i As Integer
        dim buffer as New ByteStream(Data)
        For i = 1 To MAX_PLAYER_SKILLS
            PlayerSkills(i) = Buffer.ReadInt32
        Next

        Buffer.Dispose()
    End Sub

    Private Sub Packet_LeftMap(ByRef data() As Byte)
        dim buffer as New ByteStream(Data)
        ClearPlayer(Buffer.ReadInt32)

        Buffer.Dispose()
    End Sub



    Private Sub Packet_Ping(ByRef data() As Byte)
        PingEnd = GetTickCount()
        Ping = PingEnd - PingStart
    End Sub

    Private Sub Packet_DoorAnimation(ByRef data() As Byte)
        Dim x As Integer, y As Integer
        dim buffer as New ByteStream(Data)
        X = Buffer.ReadInt32
        Y = Buffer.ReadInt32
        With TempTile(X, Y)
            .DoorFrame = 1
            .DoorAnimate = 1 ' 0 = nothing| 1 = opening | 2 = closing
            .DoorTimer = GetTickCount()
        End With

        Buffer.Dispose()
    End Sub

    Private Sub Packet_ActionMessage(ByRef data() As Byte)
        Dim x As Integer, y As Integer, message As String, color As Integer, tmpType As Integer
        dim buffer as New ByteStream(Data)
        message = Trim(buffer.ReadString)
        color = Buffer.ReadInt32
        tmpType = Buffer.ReadInt32
        X = Buffer.ReadInt32
        Y = Buffer.ReadInt32

        Buffer.Dispose()

        CreateActionMsg(message, color, tmpType, X, Y)
    End Sub



    Private Sub Packet_PlayerExp(ByRef data() As Byte)
        Dim index as integer, tnl As Integer
        dim buffer as New ByteStream(Data)
        index = Buffer.ReadInt32
        SetPlayerExp(index, Buffer.ReadInt32)
        TNL = Buffer.ReadInt32

        If TNL = 0 Then TNL = 1
        NextlevelExp = TNL

        Buffer.Dispose()
    End Sub

    Private Sub Packet_Blood(ByRef data() As Byte)
        Dim x As Integer, y As Integer, sprite As Integer
        dim buffer as New ByteStream(Data)
        X = Buffer.ReadInt32
        Y = Buffer.ReadInt32

        ' randomise sprite
        Sprite = Rand(1, 3)

        BloodIndex = BloodIndex + 1
        If BloodIndex >= Byte.MaxValue Then BloodIndex = 1

        With Blood(BloodIndex)
            .X = X
            .Y = Y
            .Sprite = Sprite
            .Timer = GetTickCount()
        End With

        Buffer.Dispose()
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

    Private Sub Packet_Animation(ByRef data() As Byte)
        dim buffer as New ByteStream(Data)
        AnimationIndex = AnimationIndex + 1
        If AnimationIndex >= Byte.MaxValue Then AnimationIndex = 1

        With AnimInstance(AnimationIndex)
            .Animation = Buffer.ReadInt32
            .X = Buffer.ReadInt32
            .Y = Buffer.ReadInt32
            .LockType = Buffer.ReadInt32
            .lockindex = Buffer.ReadInt32
            .Used(0) = True
            .Used(1) = True
        End With

        Buffer.Dispose()
    End Sub

    Private Sub Packet_NPCVitals(ByRef data() As Byte)
        Dim mapNpcNum As Integer
        dim buffer as New ByteStream(Data)
        MapNpcNum = Buffer.ReadInt32
        For i = 1 To VitalType.Count - 1
            MapNpc(MapNpcNum).Vital(i) = Buffer.ReadInt32
        Next

        Buffer.Dispose()
    End Sub

    Private Sub Packet_Cooldown(ByRef data() As Byte)
        Dim slot As Integer
        dim buffer as New ByteStream(Data)
        slot = Buffer.ReadInt32
        SkillCD(slot) = GetTickCount()

        Buffer.Dispose()
    End Sub

    Private Sub Packet_ClearSkillBuffer(ByRef data() As Byte)
        dim buffer as New ByteStream(Data)
        SkillBuffer = 0
        SkillBufferTimer = 0

        Buffer.Dispose()
    End Sub

    Private Sub Packet_SayMessage(ByRef data() As Byte)
        Dim access As Integer, name As String, message As String
        Dim header As String, pk As Integer
        dim buffer as New ByteStream(Data)
        Name = Trim(Buffer.ReadString)
        Access = Buffer.ReadInt32
        PK = Buffer.ReadInt32
        'message = Trim(Buffer.ReadString)
        message = Trim(buffer.ReadString)
        header = Trim(Buffer.ReadString)

        AddText(Header & Name & ": " & message, QColorType.SayColor)

        Buffer.Dispose()
    End Sub

    Private Sub Packet_OpenShop(ByRef data() As Byte)
        Dim shopnum As Integer
        dim buffer as New ByteStream(Data)
        shopnum = Buffer.ReadInt32

        NeedToOpenShop = True
        NeedToOpenShopNum = shopnum

        Buffer.Dispose()
    End Sub

    Private Sub Packet_ResetShopAction(ByRef data() As Byte)
        ShopAction = 0
    End Sub

    Private Sub Packet_Stunned(ByRef data() As Byte)
        dim buffer as New ByteStream(Data)
        StunDuration = Buffer.ReadInt32

        Buffer.Dispose()
    End Sub

    Private Sub Packet_MapWornEquipment(ByRef data() As Byte)
        Dim playernum As Integer
        dim buffer as New ByteStream(Data)
        playernum = Buffer.ReadInt32
        SetPlayerEquipment(playernum, Buffer.ReadInt32, EquipmentType.Armor)
        SetPlayerEquipment(playernum, Buffer.ReadInt32, EquipmentType.Weapon)
        SetPlayerEquipment(playernum, Buffer.ReadInt32, EquipmentType.Helmet)
        SetPlayerEquipment(playernum, Buffer.ReadInt32, EquipmentType.Shield)
        SetPlayerEquipment(playernum, Buffer.ReadInt32, EquipmentType.Shoes)
        SetPlayerEquipment(playernum, Buffer.ReadInt32, EquipmentType.Gloves)

        Buffer.Dispose()
    End Sub

    Private Sub Packet_OpenBank(ByRef data() As Byte)
        Dim i As Integer, x As Integer
        Dim buffer As New ByteStream(Data)
        For i = 1 To MAX_BANK
            Bank.Item(i).Num = Buffer.ReadInt32
            Bank.Item(i).Value = Buffer.ReadInt32

            Bank.ItemRand(i).Prefix = Buffer.ReadString
            Bank.ItemRand(i).Suffix = Buffer.ReadString
            Bank.ItemRand(i).Rarity = Buffer.ReadInt32
            Bank.ItemRand(i).Damage = Buffer.ReadInt32
            Bank.ItemRand(i).Speed = Buffer.ReadInt32

            For x = 1 To StatType.Count - 1
                Bank.ItemRand(i).Stat(x) = Buffer.ReadInt32
            Next
        Next

        NeedToOpenBank = True

        Buffer.Dispose()
    End Sub

    Private Sub Packet_GameData(ByRef data() As Byte)
        Dim n As Integer, i As Integer, z As Integer, x As Integer, a As Integer, b As Integer
        dim buffer as New ByteStream(Compression.DecompressBytes(Data))

        '\\\Read Class Data\\\

        ' Max classes
        MaxClasses = Buffer.ReadInt32
        ReDim Classes(MaxClasses)

        For i = 0 To MaxClasses
            ReDim Classes(i).Stat(StatType.Count - 1)
        Next

        For i = 0 To MaxClasses
            ReDim Classes(i).Vital(VitalType.Count - 1)
        Next

        For i = 1 To MaxClasses

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
            Item(n).SubType = Buffer.ReadInt32

            Item(n).ItemLevel = Buffer.ReadInt32

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

        ' changes to inventory, need to clear any drop menu

        frmGame.pnlCurrency.Visible = False
        frmGame.txtCurrency.Text = ""
        tmpCurrencyItem = 0
        CurrencyMenu = 0 ' clear

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

            Animation(n).Name = Trim$(Buffer.ReadString)
            Animation(n).Sound = Trim$(Buffer.ReadString)

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

    Private Sub Packet_Target(ByRef data() As Byte)
        dim buffer as New ByteStream(Data)
        myTarget = Buffer.ReadInt32
        myTargetType = Buffer.ReadInt32

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

    Private Sub Packet_Admin(ByRef data() As Byte)
        Adminvisible = True
    End Sub

    Private Sub Packet_MapNames(ByRef data() As Byte)
        Dim I As Integer
        dim buffer as New ByteStream(Data)
        For I = 1 To MAX_MAPS
            MapNames(I) = Trim(Buffer.ReadString())
        Next

        Buffer.Dispose()
    End Sub

    Private Sub Packet_Hotbar(ByRef data() As Byte)
        Dim i As Integer
        dim buffer as New ByteStream(Data)
        For i = 1 To MaxHotbar
            Player(MyIndex).Hotbar(i).Slot = Buffer.ReadInt32
            Player(MyIndex).Hotbar(i).sType = Buffer.ReadInt32
        Next

        Buffer.Dispose()
    End Sub

    Private Sub Packet_Critical(ByRef data() As Byte)
        ShakeTimerEnabled = True
        ShakeTimer = GetTickCount()
    End Sub

    Private Sub Packet_News(ByRef data() As Byte)
        dim buffer as New ByteStream(Data)
        GameName = buffer.ReadString
        News = buffer.ReadString

        UpdateNews = True

        Buffer.Dispose()
    End Sub

    Private Sub Packet_RClick(ByRef data() As Byte)
        ShowRClick = True
    End Sub

    Private Sub Packet_TotalOnline(ByRef data() As Byte)
        dim buffer as New ByteStream(Data)
        TotalOnline = Buffer.ReadInt32

        Buffer.Dispose()
    End Sub

    Private Sub Packet_Emote(ByRef data() As Byte)
        Dim index as integer, emote As Integer
        dim buffer as New ByteStream(Data)
        index = Buffer.ReadInt32
        emote = Buffer.ReadInt32

        With Player(index)
            .Emote = emote
            .EmoteTimer = GetTickCount() + 5000
        End With

        Buffer.Dispose()

    End Sub

    Private Sub Packet_ChatBubble(ByRef data() As Byte)
        Dim targetType As Integer, target As Integer, message As String, colour As Integer
        Dim buffer As New ByteStream(data)

        target = Buffer.ReadInt32
        targetType = buffer.ReadInt32
        message = Trim(buffer.ReadString)
        colour = Buffer.ReadInt32
        AddChatBubble(target, targetType, Message, colour)

        Buffer.Dispose()

    End Sub

    Private Sub Packet_LeftGame(ByRef data() As Byte)
        DestroyGame()
    End Sub
End Module
