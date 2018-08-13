Imports System
Imports System.Windows.Forms

Module B_Packets
    ' Packets sent by server to client
    Friend Enum ServerPacket
        SAlertMsg = 1
        SKeyPair
        SLoadCharOk
        SLoginOk
        SNewCharClasses
        SClassesData
        SInGame
        SPlayerInv
        SPlayerInvUpdate
        SPlayerWornEq
        SPlayerHp
        SPlayerMp
        SPlayerSp
        SPlayerStats
        SPlayerData
        SPlayerMove
        SNpcMove
        SPlayerDir
        SNpcDir
        SPlayerXY
        SAttack
        SNpcAttack
        SCheckForMap
        SMapData
        SMapItemData
        SMapNpcData
        SMapNpcUpdate
        SMapDone
        SGlobalMsg
        SAdminMsg
        SPlayerMsg
        SMapMsg
        SSpawnItem
        SItemEditor
        SUpdateItem
        SREditor
        SSpawnNpc
        SNpcDead
        SNpcEditor
        SUpdateNpc
        SMapKey
        SEditMap
        SShopEditor
        SUpdateShop
        SSkillEditor
        SUpdateSkill
        SSkills
        SLeftMap
        SResourceCache
        SResourceEditor
        SUpdateResource
        SSendPing
        SDoorAnimation
        SActionMsg
        SPlayerEXP
        SBlood
        SAnimationEditor
        SUpdateAnimation
        SAnimation
        SMapNpcVitals
        SCooldown
        SClearSkillBuffer
        SSayMsg
        SOpenShop
        SResetShopAction
        SStunned
        SMapWornEq
        SBank
        SLeftGame

        SClearTradeTimer
        STradeInvite
        STrade
        SCloseTrade
        STradeUpdate
        STradeStatus

        SGameData
        SMapReport
        STarget
        SAdmin
        SMapNames
        SCritical
        SNews
        SrClick
        STotalOnline

        'quests
        SQuestEditor
        SUpdateQuest
        SPlayerQuest
        SPlayerQuests
        SQuestMessage

        'Housing
        SBuyHouse
        SVisit
        SFurniture
        SHouseEdit
        SHouseConfigs

        'hotbar
        SHotbar

        'Events
        SSpawnEvent
        SEventMove
        SEventDir
        SEventChat
        SEventStart
        SEventEnd
        SPlayBGM
        SPlaySound
        SFadeoutBGM
        SStopSound
        SSwitchesAndVariables
        SMapEventData
        SChatBubble
        SSpecialEffect
        SPic
        SHoldPlayer

        SProjectileEditor
        SUpdateProjectile
        SMapProjectile

        'recipes
        SUpdateRecipe
        SRecipeEditor
        SSendPlayerRecipe
        SOpenCraft
        SUpdateCraft

        'Class Editor
        SClassEditor
        SUpdateClasses

        'AutoMapper
        SAutoMapper

        'emotes
        SEmote

        'Parties
        SPartyInvite
        SPartyUpdate
        SPartyVitals

        'pets
        SPetEditor
        SUpdatePet
        SUpdatePlayerPet
        SPetMove
        SPetDir
        SPetVital
        SClearPetSkillBuffer
        SPetAttack
        SPetXY
        SPetExp

        STime
        SClock

        ' Make sure COUNT is below everything else
        COUNT
    End Enum

    ' Packets sent by client to server
    Friend Enum ClientPacket
        CNewAccount = 1
        CDelAccount
        CLogin
        CAddChar
        CUseChar
        CDelChar
        CSayMsg
        CBroadcastMsg
        CPlayerMsg
        CPlayerMove
        CPlayerDir
        CUseItem
        CAttack
        CPlayerInfoRequest
        CWarpMeTo
        CWarpToMe
        CWarpTo
        CSetSprite
        CGetStats
        CRequestNewMap
        CSaveMap
        CNeedMap
        CMapGetItem
        CMapDropItem
        CMapRespawn
        CMapReport
        CKickPlayer
        CBanList
        CBanDestroy
        CBanPlayer
        CRequestEditMap

        CSetAccess
        CWhosOnline
        CSetMotd
        CSearch
        CSkills
        CCast
        CQuit
        CSwapInvSlots

        CCheckPing
        CUnequip
        CRequestPlayerData
        CRequestItems
        CRequestNPCS
        CRequestResources
        CSpawnItem
        CTrainStat

        CRequestAnimations
        CRequestSkills
        CRequestShops
        CRequestLevelUp
        CForgetSkill
        CCloseShop
        CBuyItem
        CSellItem
        CChangeBankSlots
        CDepositItem
        CWithdrawItem
        CCloseBank
        CAdminWarp

        CTradeInvite
        CTradeInviteAccept
        CAcceptTrade
        CDeclineTrade
        CTradeItem
        CUntradeItem

        CAdmin

        'quests
        CRequestQuests
        CQuestLogUpdate
        CPlayerHandleQuest
        CQuestReset

        'Housing
        CBuyHouse
        CVisit
        CAcceptVisit
        CPlaceFurniture

        CSellHouse

        'Hotbar
        CSetHotbarSlot
        CDeleteHotbarSlot
        CUseHotbarSlot

        'Events
        CEventChatReply
        CEvent
        CSwitchesAndVariables
        CRequestSwitchesAndVariables

        CRequestProjectiles
        CClearProjectile

        CRequestRecipes

        CCloseCraft
        CStartCraft

        CRequestClasses

        'emotes
        CEmote

        'party
        CRequestParty
        CAcceptParty
        CDeclineParty
        CLeaveParty
        CPartyChatMsg

        'pets
        CRequestPets
        CSummonPet
        CPetMove
        CSetBehaviour
        CReleasePet
        CPetSkill
        CPetUseStatPoint

        ' Make sure COUNT is below everything else
        Count
    End Enum

    Friend enum EditorPackets
            EditorRequestMap
            EditorSaveMap

            'editor
            RequestEditItem
            SaveItem
            RequestEditNpc
            SaveNpc
            RequestEditShop
            SaveShop
            RequestEditSkill
            SaveSkill
            RequestEditResource
            SaveResource
            RequestEditAnimation
            SaveAnimation
            RequestEditQuest
            SaveQuest
            RequestEditHouse
            SaveHouses
            RequestEditProjectiles
            SaveProjectile
            RequestEditRecipes
            SaveRecipe
            RequestEditClasses
            SaveClasses
            RequestAutoMap
            SaveAutoMap

            'pet
            CRequestEditPet
            CSavePet

    End enum
End Module

Namespace Network
    '''' <summary>
    '''' Packets sent by the Client and received by the Server.
    '''' </summary>
    'Friend Enum ClientPacket As Integer
    '    Quit

    '    ' Main Menu Headers.
    '    Login
    '    Register
    '    ForgotPassword
    '    VerificationResend
    '    VerificationConfirm

    '    ' Character Select Headers.
    '    NewCharacter
    '    UseCharacter
    '    DelCharacter
    '    ChangePassword

    '    ' Game Database Headers.
    '    GameDatabaseRequest

    '    ' (Temporary) Other.
    '    Report
    '    Message
    '    Count
    'End Enum

    '''' <summary>
    '''' Packets sent by the Server and received by the Client.
    '''' </summary>
    'Friend Enum ServerPacket As Integer
    '    Popup
    '    EncryptionKey

    '    ' Main Menu Headers.
    '    CharacterSelect
    '    ForgotPassword
    '    Verification

    '    ' Character Select Headers.
    '    EnterGame
    '    RefreshCharacters
    '    ChangePassword

    '    ' Game Database Headers.
    '    GameDatabaseCheck
    '    GameDatabaseUpdate

    '    ' (Temporary) Other.
    '    Message
    '    PlayerData
    '    Count
    'End Enum

    Friend Module modPacket
        Friend Sub PacketLog(index As Integer, header As Integer, data As Byte())
            Dim cd = Environment.CurrentDirectory & "/Packet Logs/"
            If Not IO.Directory.Exists(cd) Then IO.Directory.CreateDirectory(cd)

            Dim cf = cd
#If SERVER Then
            If IsLoggedIn(index) Then
                cf += Player(index).Login
            Else
                cf += "[Unlogged User]"
            End If
#End If

            Dim time = DateTime.Now.ToString()
            Using packetReport As New IO.StreamWriter(cf & "_" & time & ".bin")
#If CLIENT Then
                packetReport.WriteLineAsync(CType(header, ServerPacket).ToString())
#ElseIf SERVER Then
                packetReport.WriteLineAsync(CType(header, ClientPacket).ToString())
#End If
                packetReport.WriteLineAsync("") ' Spacing

                packetReport.WriteLineAsync(data.ToString())
            End Using
        End Sub
    End Module
End Namespace