Imports System.Windows.Forms
Imports ASFW
Imports ASFW.IO

Namespace Network
    Partial Friend Module modNetwork
        Sub InitializePacketPointers()
            Socket.PacketId(ServerPacket.SAlertMsg) = AddressOf Packet_AlertMSG
            Socket.PacketId(ServerPacket.SKeyPair) = AddressOf Packet_KeyPair
            Socket.PacketId(ServerPacket.SLoadCharOk) = AddressOf Packet_LoadCharOk
            Socket.PacketId(ServerPacket.SLoginOk) = AddressOf Packet_LoginOk
            Socket.PacketId(ServerPacket.SNewCharClasses) = AddressOf Packet_NewCharClasses
            Socket.PacketId(ServerPacket.SClassesData) = AddressOf Packet_ClassesData
            Socket.PacketId(ServerPacket.SInGame) = AddressOf Packet_InGame
            Socket.PacketId(ServerPacket.SPlayerInv) = AddressOf Packet_PlayerInv
            Socket.PacketId(ServerPacket.SPlayerInvUpdate) = AddressOf Packet_PlayerInvUpdate
            Socket.PacketId(ServerPacket.SPlayerWornEq) = AddressOf Packet_PlayerWornEquipment
            Socket.PacketId(ServerPacket.SPlayerHp) = AddressOf Packet_PlayerHP
            Socket.PacketId(ServerPacket.SPlayerMp) = AddressOf Packet_PlayerMP
            Socket.PacketId(ServerPacket.SPlayerSp) = AddressOf Packet_PlayerSP
            Socket.PacketId(ServerPacket.SPlayerStats) = AddressOf Packet_PlayerStats
            Socket.PacketId(ServerPacket.SPlayerData) = AddressOf Packet_PlayerData
            Socket.PacketId(ServerPacket.SPlayerMove) = AddressOf Packet_PlayerMove
            Socket.PacketId(ServerPacket.SNpcMove) = AddressOf Packet_NpcMove
            Socket.PacketId(ServerPacket.SPlayerDir) = AddressOf Packet_PlayerDir
            Socket.PacketId(ServerPacket.SNpcDir) = AddressOf Packet_NpcDir
            Socket.PacketId(ServerPacket.SPlayerXY) = AddressOf Packet_PlayerXY
            Socket.PacketId(ServerPacket.SAttack) = AddressOf Packet_Attack
            Socket.PacketId(ServerPacket.SNpcAttack) = AddressOf Packet_NpcAttack
            Socket.PacketId(ServerPacket.SCheckForMap) = AddressOf Packet_CheckMap
            Socket.PacketId(ServerPacket.SMapData) = AddressOf Packet_MapData
            Socket.PacketId(ServerPacket.SMapNpcData) = AddressOf Packet_MapNPCData
            Socket.PacketId(ServerPacket.SMapNpcUpdate) = AddressOf Packet_MapNPCUpdate
            Socket.PacketId(ServerPacket.SMapDone) = AddressOf Packet_MapDone
            Socket.PacketId(ServerPacket.SGlobalMsg) = AddressOf Packet_GlobalMessage
            Socket.PacketId(ServerPacket.SPlayerMsg) = AddressOf Packet_PlayerMessage
            Socket.PacketId(ServerPacket.SMapMsg) = AddressOf Packet_MapMessage
            Socket.PacketId(ServerPacket.SSpawnItem) = AddressOf Packet_SpawnItem
            Socket.PacketId(ServerPacket.SUpdateItem) = AddressOf Packet_UpdateItem
            Socket.PacketId(ServerPacket.SSpawnNpc) = AddressOf Packet_SpawnNPC
            Socket.PacketId(ServerPacket.SNpcDead) = AddressOf Packet_NpcDead
            Socket.PacketId(ServerPacket.SUpdateNpc) = AddressOf Packet_UpdateNPC
            Socket.PacketId(ServerPacket.SMapKey) = AddressOf Packet_MapKey
            Socket.PacketId(ServerPacket.SEditMap) = AddressOf Packet_EditMap
            Socket.PacketId(ServerPacket.SUpdateShop) = AddressOf Packet_UpdateShop
            Socket.PacketId(ServerPacket.SUpdateSkill) = AddressOf Packet_UpdateSkill
            Socket.PacketId(ServerPacket.SSkills) = AddressOf Packet_Skills
            Socket.PacketId(ServerPacket.SLeftMap) = AddressOf Packet_LeftMap
            Socket.PacketId(ServerPacket.SResourceCache) = AddressOf Packet_ResourceCache
            Socket.PacketId(ServerPacket.SUpdateResource) = AddressOf Packet_UpdateResource
            Socket.PacketId(ServerPacket.SSendPing) = AddressOf Packet_Ping
            Socket.PacketId(ServerPacket.SDoorAnimation) = AddressOf Packet_DoorAnimation
            Socket.PacketId(ServerPacket.SActionMsg) = AddressOf Packet_ActionMessage
            Socket.PacketId(ServerPacket.SPlayerEXP) = AddressOf Packet_PlayerExp
            Socket.PacketId(ServerPacket.SBlood) = AddressOf Packet_Blood
            Socket.PacketId(ServerPacket.SUpdateAnimation) = AddressOf Packet_UpdateAnimation
            Socket.PacketId(ServerPacket.SAnimation) = AddressOf Packet_Animation
            Socket.PacketId(ServerPacket.SMapNpcVitals) = AddressOf Packet_NPCVitals
            Socket.PacketId(ServerPacket.SCooldown) = AddressOf Packet_Cooldown
            Socket.PacketId(ServerPacket.SClearSkillBuffer) = AddressOf Packet_ClearSkillBuffer
            Socket.PacketId(ServerPacket.SSayMsg) = AddressOf Packet_SayMessage
            Socket.PacketId(ServerPacket.SOpenShop) = AddressOf Packet_OpenShop
            Socket.PacketId(ServerPacket.SResetShopAction) = AddressOf Packet_ResetShopAction
            Socket.PacketId(ServerPacket.SStunned) = AddressOf Packet_Stunned
            Socket.PacketId(ServerPacket.SMapWornEq) = AddressOf Packet_MapWornEquipment
            Socket.PacketId(ServerPacket.SBank) = AddressOf Packet_OpenBank
            Socket.PacketId(ServerPacket.SLeftGame) = AddressOf Packet_LeftGame

            Socket.PacketId(ServerPacket.SClearTradeTimer) = AddressOf Packet_ClearTradeTimer
            Socket.PacketId(ServerPacket.STradeInvite) = AddressOf Packet_TradeInvite
            Socket.PacketId(ServerPacket.STrade) = AddressOf Packet_Trade
            Socket.PacketId(ServerPacket.SCloseTrade) = AddressOf Packet_CloseTrade
            Socket.PacketId(ServerPacket.STradeUpdate) = AddressOf Packet_TradeUpdate
            Socket.PacketId(ServerPacket.STradeStatus) = AddressOf Packet_TradeStatus

            Socket.PacketId(ServerPacket.SGameData) = AddressOf Packet_GameData
            Socket.PacketId(ServerPacket.SMapReport) = AddressOf Packet_Mapreport 'Mapreport
            Socket.PacketId(ServerPacket.STarget) = AddressOf Packet_Target

            Socket.PacketId(ServerPacket.SAdmin) = AddressOf Packet_Admin
            Socket.PacketId(ServerPacket.SMapNames) = AddressOf Packet_MapNames

            Socket.PacketId(ServerPacket.SCritical) = AddressOf Packet_Critical
            Socket.PacketId(ServerPacket.SNews) = AddressOf Packet_News
            Socket.PacketId(ServerPacket.SrClick) = AddressOf Packet_RClick
            Socket.PacketId(ServerPacket.STotalOnline) = AddressOf Packet_TotalOnline

            'quests
            Socket.PacketId(ServerPacket.SUpdateQuest) = AddressOf Packet_UpdateQuest
            Socket.PacketId(ServerPacket.SPlayerQuest) = AddressOf Packet_PlayerQuest
            Socket.PacketId(ServerPacket.SPlayerQuests) = AddressOf Packet_PlayerQuests
            Socket.PacketId(ServerPacket.SQuestMessage) = AddressOf Packet_QuestMessage

            'Housing
            Socket.PacketId(ServerPacket.SHouseConfigs) = AddressOf Packet_HouseConfigurations
            Socket.PacketId(ServerPacket.SBuyHouse) = AddressOf Packet_HouseOffer
            Socket.PacketId(ServerPacket.SVisit) = AddressOf Packet_Visit
            Socket.PacketId(ServerPacket.SFurniture) = AddressOf Packet_Furniture

            'hotbar
            Socket.PacketId(ServerPacket.SHotbar) = AddressOf Packet_Hotbar

            'Events
            Socket.PacketId(ServerPacket.SSpawnEvent) = AddressOf Packet_SpawnEvent
            Socket.PacketId(ServerPacket.SEventMove) = AddressOf Packet_EventMove
            Socket.PacketId(ServerPacket.SEventDir) = AddressOf Packet_EventDir
            Socket.PacketId(ServerPacket.SEventChat) = AddressOf Packet_EventChat
            Socket.PacketId(ServerPacket.SEventStart) = AddressOf Packet_EventStart
            Socket.PacketId(ServerPacket.SEventEnd) = AddressOf Packet_EventEnd
            Socket.PacketId(ServerPacket.SPlayBGM) = AddressOf Packet_PlayBGM
            Socket.PacketId(ServerPacket.SPlaySound) = AddressOf Packet_PlaySound
            Socket.PacketId(ServerPacket.SFadeoutBGM) = AddressOf Packet_FadeOutBGM
            Socket.PacketId(ServerPacket.SStopSound) = AddressOf Packet_StopSound
            Socket.PacketId(ServerPacket.SSwitchesAndVariables) = AddressOf Packet_SwitchesAndVariables
            Socket.PacketId(ServerPacket.SMapEventData) = AddressOf Packet_MapEventData
            Socket.PacketId(ServerPacket.SChatBubble) = AddressOf Packet_ChatBubble
            Socket.PacketId(ServerPacket.SSpecialEffect) = AddressOf Packet_SpecialEffect
            'SPic
            Socket.PacketId(ServerPacket.SHoldPlayer) = AddressOf Packet_HoldPlayer

            Socket.PacketId(ServerPacket.SUpdateProjectile) = AddressOf HandleUpdateProjectile
            Socket.PacketId(ServerPacket.SMapProjectile) = AddressOf HandleMapProjectile

            'craft
            Socket.PacketId(ServerPacket.SUpdateRecipe) = AddressOf Packet_UpdateRecipe
            Socket.PacketId(ServerPacket.SSendPlayerRecipe) = AddressOf Packet_SendPlayerRecipe
            Socket.PacketId(ServerPacket.SOpenCraft) = AddressOf Packet_OpenCraft
            Socket.PacketId(ServerPacket.SUpdateCraft) = AddressOf Packet_UpdateCraft

            'emotes
            Socket.PacketId(ServerPacket.SEmote) = AddressOf Packet_Emote

            'party
            Socket.PacketId(ServerPacket.SPartyInvite) = AddressOf Packet_PartyInvite
            Socket.PacketId(ServerPacket.SPartyUpdate) = AddressOf Packet_PartyUpdate
            Socket.PacketId(ServerPacket.SPartyVitals) = AddressOf Packet_PartyVitals

            'pets
            Socket.PacketId(ServerPacket.SUpdatePet) = AddressOf Packet_UpdatePet
            Socket.PacketId(ServerPacket.SUpdatePlayerPet) = AddressOf Packet_UpdatePlayerPet
            Socket.PacketId(ServerPacket.SPetMove) = AddressOf Packet_PetMove
            Socket.PacketId(ServerPacket.SPetDir) = AddressOf Packet_PetDir
            Socket.PacketId(ServerPacket.SPetVital) = AddressOf Packet_PetVital
            Socket.PacketId(ServerPacket.SClearPetSkillBuffer) = AddressOf Packet_ClearPetSkillBuffer
            Socket.PacketId(ServerPacket.SPetAttack) = AddressOf Packet_PetAttack
            Socket.PacketId(ServerPacket.SPetXY) = AddressOf Packet_PetXY
            Socket.PacketId(ServerPacket.SPetExp) = AddressOf Packet_PetExperience

            Socket.PacketId(ServerPacket.SClock) = AddressOf Packet_Clock
            Socket.PacketId(ServerPacket.STime) = AddressOf Packet_Time
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

            MessageBox.Show(msg, GameName, MessageBoxButtons.OK)
            Terminate()
        End Sub

        Private Sub Packet_KeyPair(ByRef data() As Byte)
            Dim buffer As New ByteStream(data)
            Configuration.Encryption.ImportKeyString(buffer.ReadString())
            buffer.Dispose()
        End Sub

        Private Sub Packet_LoadCharOk(ByRef data() As Byte)
            Dim buffer As New ByteStream(data)
            ' Now we can receive game data
            Myindex = buffer.ReadInt32

            buffer.Dispose()

            Pnlloadvisible = True
            SetStatus(Configuration.Language.gamegui.datarecieve)
        End Sub

        Private Sub Packet_LoginOk(ByRef data() As Byte)
            Dim charName As String, sprite As Integer
            Dim level As Integer, className As String, gender As Byte

            ' save options
            Configuration.Settings.SavePass = ChkSavePassChecked
            Configuration.Settings.Username = TempUserName.Trim

            If ChkSavePassChecked = False Then
                Configuration.Settings.Password = ""
            Else
                Configuration.Settings.Password = TempPassword.Trim
            End If

            Configuration.SaveSettings()

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

        Private Sub Packet_NewCharClasses(ByRef data() As Byte)
            Dim i As Integer, z As Integer, x As Integer
            Dim buffer As New ByteStream(data)
            ' Max classes
            MaxClasses = buffer.ReadInt32
            ReDim Classes(MaxClasses)

            SelectedChar = 1

            For i = 1 To MaxClasses

                With Classes(i)
                    .Name = buffer.ReadString.Trim
                    .Desc = buffer.ReadString.Trim

                    ReDim .Vital(VitalType.Count - 1)

                    .Vital(VitalType.HP) = buffer.ReadInt32
                    .Vital(VitalType.MP) = buffer.ReadInt32
                    .Vital(VitalType.SP) = buffer.ReadInt32

                    ' get array size
                    z = buffer.ReadInt32
                    ' redim array
                    ReDim .MaleSprite(z + 1)
                    ' loop-receive data
                    For x = 1 To z + 1
                        .MaleSprite(x) = buffer.ReadInt32
                    Next

                    ' get array size
                    z = buffer.ReadInt32
                    ' redim array
                    ReDim .FemaleSprite(z + 1)
                    ' loop-receive data
                    For x = 1 To z + 1
                        .FemaleSprite(x) = buffer.ReadInt32
                    Next

                    ReDim .Stat(StatType.Count - 1)

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

            ' Used for if the player is creating a new character
            Frmmenuvisible = True
            Pnlloadvisible = False
            PnlCreditsVisible = False
            PnlRegisterVisible = False
            PnlCharCreateVisible = True
            PnlLoginVisible = False

            ReDim Cmbclass(MaxClasses)

            For i = 1 To MaxClasses
                Cmbclass(i) = Classes(i).Name
            Next

            FrmMenu.DrawCharacter()

            NewCharSprite = 1
        End Sub

        Private Sub Packet_ClassesData(ByRef data() As Byte)
            Dim i As Integer, z As Integer, x As Integer
            Dim buffer As New ByteStream(data)
            ' Max classes
            MaxClasses = buffer.ReadInt32
            ReDim Classes(MaxClasses)

            SelectedChar = 1

            For i = 1 To MaxClasses

                With Classes(i)
                    .Name = buffer.ReadString.Trim
                    .Desc = buffer.ReadString.Trim

                    ReDim .Vital(VitalType.Count - 1)

                    .Vital(VitalType.HP) = buffer.ReadInt32
                    .Vital(VitalType.MP) = buffer.ReadInt32
                    .Vital(VitalType.SP) = buffer.ReadInt32

                    ' get array size
                    z = buffer.ReadInt32
                    ' redim array
                    ReDim .MaleSprite(z + 1)
                    ' loop-receive data
                    For x = 1 To z + 1
                        .MaleSprite(x) = buffer.ReadInt32
                    Next

                    ' get array size
                    z = buffer.ReadInt32
                    ' redim array
                    ReDim .FemaleSprite(z + 1)
                    ' loop-receive data
                    For x = 1 To z + 1
                        .FemaleSprite(x) = buffer.ReadInt32
                    Next

                    ReDim .Stat(StatType.Count - 1)

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

            ReDim Cmbclass(MaxClasses)
            For i = 1 To MaxClasses
                Cmbclass(i) = Classes(i).Name
            Next
            FrmMenu.DrawCharacter()
            NewCharSprite = 1

            buffer.Dispose()
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

        Private Sub Packet_PlayerHP(ByRef data() As Byte)
            Dim buffer As New ByteStream(data)
            Player(Myindex).MaxHp = buffer.ReadInt32

            SetPlayerVital(Myindex, VitalType.HP, buffer.ReadInt32)

            If GetPlayerMaxVital(Myindex, VitalType.HP) > 0 Then
                LblHpText = GetPlayerVital(Myindex, VitalType.HP) & "/" & GetPlayerMaxVital(Myindex, VitalType.HP)
                ' hp bar
                PicHpWidth = CInt(((GetPlayerVital(Myindex, VitalType.HP) / 169) / (GetPlayerMaxVital(Myindex, VitalType.HP) / 169)) * 169)
            End If

            buffer.Dispose()
        End Sub

        Private Sub Packet_PlayerMP(ByRef data() As Byte)
            Dim buffer As New ByteStream(data)
            Player(Myindex).MaxMp = buffer.ReadInt32
            SetPlayerVital(Myindex, VitalType.MP, buffer.ReadInt32)

            If GetPlayerMaxVital(Myindex, VitalType.MP) > 0 Then
                LblManaText = GetPlayerVital(Myindex, VitalType.MP) & "/" & GetPlayerMaxVital(Myindex, VitalType.MP)
                ' mp bar
                PicManaWidth = CInt(((GetPlayerVital(Myindex, VitalType.MP) / 169) / (GetPlayerMaxVital(Myindex, VitalType.MP) / 169)) * 169)
            End If

            buffer.Dispose()
        End Sub

        Private Sub Packet_PlayerSP(ByRef data() As Byte)
            Dim buffer As New ByteStream(data)
            Player(Myindex).MaxSp = buffer.ReadInt32
            SetPlayerVital(Myindex, VitalType.SP, buffer.ReadInt32)

            buffer.Dispose()
        End Sub

        Private Sub Packet_PlayerStats(ByRef data() As Byte)
            Dim i As Integer, index As Integer
            Dim buffer As New ByteStream(data)
            index = buffer.ReadInt32
            For i = 1 To StatType.Count - 1
                SetPlayerStat(index, i, buffer.ReadInt32)
            Next
            UpdateCharacterPanel = True

            buffer.Dispose()
        End Sub

        Private Sub Packet_PlayerData(ByRef data() As Byte)
            Dim i As Integer, x As Integer
            Dim buffer As New ByteStream(data)
            i = buffer.ReadInt32
            SetPlayerName(i, buffer.ReadString)
            SetPlayerClass(i, buffer.ReadInt32)
            SetPlayerLevel(i, buffer.ReadInt32)
            SetPlayerPoints(i, buffer.ReadInt32)
            SetPlayerSprite(i, buffer.ReadInt32)
            SetPlayerMap(i, buffer.ReadInt32)
            SetPlayerX(i, buffer.ReadInt32)
            SetPlayerY(i, buffer.ReadInt32)
            SetPlayerDir(i, buffer.ReadInt32)
            SetPlayerAccess(i, buffer.ReadInt32)
            SetPlayerPk(i, buffer.ReadInt32)

            For x = 1 To StatType.Count - 1
                SetPlayerStat(i, x, buffer.ReadInt32)
            Next

            Player(i).InHouse = buffer.ReadInt32

            For x = 0 To ResourceSkills.Count - 1
                Player(i).GatherSkills(x).SkillLevel = buffer.ReadInt32
                Player(i).GatherSkills(x).SkillCurExp = buffer.ReadInt32
                Player(i).GatherSkills(x).SkillNextLvlExp = buffer.ReadInt32
            Next

            For x = 1 To MAX_RECIPE
                Player(i).RecipeLearned(x) = buffer.ReadInt32
            Next

            ' Check if the player is the client player
            If i = Myindex Then
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

            If i = Myindex Then PlayerData = True

            buffer.Dispose()
        End Sub

        Private Sub Packet_PlayerMove(ByRef data() As Byte)
            Dim i As Integer, x As Integer, y As Integer
            Dim dir As Integer, n As Byte
            Dim buffer As New ByteStream(data)
            i = buffer.ReadInt32
            x = buffer.ReadInt32
            y = buffer.ReadInt32
            dir = buffer.ReadInt32
            n = buffer.ReadInt32

            SetPlayerX(i, x)
            SetPlayerY(i, y)
            SetPlayerDir(i, dir)
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

        Private Sub Packet_PlayerDir(ByRef data() As Byte)
            Dim dir As Integer, i As Integer
            Dim buffer As New ByteStream(data)
            i = buffer.ReadInt32
            dir = buffer.ReadInt32

            SetPlayerDir(i, dir)

            With Player(i)
                .XOffset = 0
                .YOffset = 0
                .Moving = 0
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

        Private Sub Packet_PlayerXY(ByRef data() As Byte)
            Dim x As Integer, y As Integer, dir As Integer
            Dim buffer As New ByteStream(data)
            x = buffer.ReadInt32
            y = buffer.ReadInt32
            dir = buffer.ReadInt32

            SetPlayerX(Myindex, x)
            SetPlayerY(Myindex, y)
            SetPlayerDir(Myindex, dir)

            ' Make sure they aren't walking
            Player(Myindex).Moving = 0
            Player(Myindex).XOffset = 0
            Player(Myindex).YOffset = 0

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

            msg = buffer.ReadString.Trim

            buffer.Dispose()

            AddText(msg, QColorType.GlobalColor)
        End Sub

        Private Sub Packet_MapMessage(ByRef data() As Byte)
            Dim msg As String
            Dim buffer As New ByteStream(data)

            msg = buffer.ReadString.Trim

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

            msg = buffer.ReadString.Trim

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
            Npc(i).AttackSay = buffer.ReadString().Trim
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
            Npc(i).Name = buffer.ReadString().Trim
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
            Skill(skillnum).Name = buffer.ReadString().Trim
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
            message = buffer.ReadString.Trim
            color = buffer.ReadInt32
            tmpType = buffer.ReadInt32
            x = buffer.ReadInt32
            y = buffer.ReadInt32

            buffer.Dispose()

            CreateActionMsg(message, color, tmpType, x, y)
        End Sub



        Private Sub Packet_PlayerExp(ByRef data() As Byte)
            Dim index As Integer, tnl As Integer
            Dim buffer As New ByteStream(data)
            index = buffer.ReadInt32
            SetPlayerExp(index, buffer.ReadInt32)
            tnl = buffer.ReadInt32

            If tnl = 0 Then tnl = 1
            NextlevelExp = tnl

            buffer.Dispose()
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

        Private Sub Packet_UpdateAnimation(ByRef data() As Byte)
            Dim n As Integer, i As Integer
            Dim buffer As New ByteStream(data)
            n = buffer.ReadInt32
            ' Update the Animation
            For i = 0 To Animation(n).Frames.GetUpperBound(0)
                Animation(n).Frames(i) = buffer.ReadInt32()
            Next

            For i = 0 To Animation(n).LoopCount.GetUpperBound(0)
                Animation(n).LoopCount(i) = buffer.ReadInt32()
            Next

            For i = 0 To Animation(n).LoopTime.GetUpperBound(0)
                Animation(n).LoopTime(i) = buffer.ReadInt32()
            Next

            Animation(n).Name = buffer.ReadString.Trim
            Animation(n).Sound = buffer.ReadString.Trim

            If Animation(n).Name Is Nothing Then Animation(n).Name = ""
            If Animation(n).Sound Is Nothing Then Animation(n).Sound = ""

            For i = 0 To Animation(n).Sprite.GetUpperBound(0)
                Animation(n).Sprite(i) = buffer.ReadInt32()
            Next
            buffer.Dispose()
        End Sub

        Private Sub Packet_Animation(ByRef data() As Byte)
            Dim buffer As New ByteStream(data)
            AnimationIndex = AnimationIndex + 1
            If AnimationIndex >= Byte.MaxValue Then AnimationIndex = 1

            With AnimInstance(AnimationIndex)
                .Animation = buffer.ReadInt32
                .X = buffer.ReadInt32
                .Y = buffer.ReadInt32
                .LockType = buffer.ReadInt32
                .lockindex = buffer.ReadInt32
                .Used(0) = True
                .Used(1) = True
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
            name = buffer.ReadString.Trim
            access = buffer.ReadInt32
            pk = buffer.ReadInt32
            message = buffer.ReadString.Trim
            header = buffer.ReadString.Trim

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
            MaxClasses = buffer.ReadInt32
            ReDim Classes(MaxClasses)

            For i = 0 To MaxClasses
                ReDim Classes(i).Stat(StatType.Count - 1)
            Next

            For i = 0 To MaxClasses
                ReDim Classes(i).Vital(VitalType.Count - 1)
            Next

            For i = 1 To MaxClasses

                With Classes(i)
                    .Name = buffer.ReadString.Trim
                    .Desc = buffer.ReadString.Trim

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
                Item(n).Name = buffer.ReadString().Trim
                Item(n).Paperdoll = buffer.ReadInt32()
                Item(n).Pic = buffer.ReadInt32()
                Item(n).Price = buffer.ReadInt32()
                Item(n).Rarity = buffer.ReadInt32()
                Item(n).Speed = buffer.ReadInt32()

                Item(n).Randomize = buffer.ReadInt32()
                Item(n).RandomMin = buffer.ReadInt32()
                Item(n).RandomMax = buffer.ReadInt32()

                Item(n).Stackable = buffer.ReadInt32()
                Item(n).Description = buffer.ReadString().Trim

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
                For z = 0 To Animation(n).Frames.GetUpperBound(0)
                    Animation(n).Frames(z) = buffer.ReadInt32()
                Next

                For z = 0 To Animation(n).LoopCount.GetUpperBound(0)
                    Animation(n).LoopCount(z) = buffer.ReadInt32()
                Next

                For z = 0 To Animation(n).LoopTime.GetUpperBound(0)
                    Animation(n).LoopTime(z) = buffer.ReadInt32()
                Next

                Animation(n).Name = buffer.ReadString.Trim
                Animation(n).Sound = buffer.ReadString.Trim

                If Animation(n).Name Is Nothing Then Animation(n).Name = ""
                If Animation(n).Sound Is Nothing Then Animation(n).Sound = ""

                For z = 0 To Animation(n).Sprite.GetUpperBound(0)
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
                Npc(n).AttackSay = buffer.ReadString().Trim
                Npc(n).Behaviour = buffer.ReadInt32()
                For z = 1 To 5
                    Npc(n).DropChance(z) = buffer.ReadInt32()
                    Npc(n).DropItem(z) = buffer.ReadInt32()
                    Npc(n).DropItemValue(z) = buffer.ReadInt32()
                Next

                Npc(n).Exp = buffer.ReadInt32()
                Npc(n).Faction = buffer.ReadInt32()
                Npc(n).Hp = buffer.ReadInt32()
                Npc(n).Name = buffer.ReadString().Trim
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
                Shop(n).Name = buffer.ReadString().Trim
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
                Skill(n).Name = buffer.ReadString().Trim
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
                Resource(n).EmptyMessage = buffer.ReadString().Trim
                Resource(n).ExhaustedImage = buffer.ReadInt32()
                Resource(n).Health = buffer.ReadInt32()
                Resource(n).ExpReward = buffer.ReadInt32()
                Resource(n).ItemReward = buffer.ReadInt32()
                Resource(n).Name = buffer.ReadString().Trim
                Resource(n).ResourceImage = buffer.ReadInt32()
                Resource(n).ResourceType = buffer.ReadInt32()
                Resource(n).RespawnTime = buffer.ReadInt32()
                Resource(n).SuccessMessage = buffer.ReadString().Trim
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
                MapNames(I) = buffer.ReadString().Trim
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
                MapNames(I) = buffer.ReadString().Trim
            Next

            buffer.Dispose()
        End Sub

        Private Sub Packet_Hotbar(ByRef data() As Byte)
            Dim i As Integer
            Dim buffer As New ByteStream(data)
            For i = 1 To MaxHotbar
                Player(Myindex).Hotbar(i).Slot = buffer.ReadInt32
                Player(Myindex).Hotbar(i).SType = buffer.ReadInt32
            Next

            buffer.Dispose()
        End Sub

        Private Sub Packet_Critical(ByRef data() As Byte)
            ShakeTimerEnabled = True
            ShakeTimer = GetTickCount()
        End Sub

        Private Sub Packet_News(ByRef data() As Byte)
            Dim buffer As New ByteStream(data)
            GameName = buffer.ReadString
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
            message = buffer.ReadString.Trim
            colour = buffer.ReadInt32
            AddChatBubble(target, targetType, message, colour)

            buffer.Dispose()

        End Sub

        Private Sub Packet_LeftGame(ByRef data() As Byte)
            Terminate()
        End Sub
    End Module
End Namespace




