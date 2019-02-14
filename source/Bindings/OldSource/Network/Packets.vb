Friend Module Packets

    ' Packets sent by client to server
    Friend Enum ClientPackets
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
        CNeedMap
        CMapGetItem
        CMapDropItem
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

        '*************************
        '***   EDITOR PACKETS  ***
        '*************************

        ' Mapper Packets
        CMapRespawn
        CMapReport
        CSaveMap

        ' AutoMapper Packets
        CRequestAutoMap
        CSaveAutoMap

        ' ####################
        ' ### Dev+ Packets ###
        ' ####################

        'animations
        CRequestEditAnimation
        CSaveAnimation

        'Class Editor
        CRequestEditClasses
        CSaveClasses

        'houses
        CRequestEditHouse
        CSaveHouses

        'items
        CRequestEditItem
        CSaveItem

        'npc's
        CRequestEditNpc
        CSaveNpc

        'pets
        CRequestEditPet
        CSavePet

        'projectiles
        CRequestEditProjectiles
        CSaveProjectile

        'quests
        CRequestEditQuest
        CSaveQuest

        'recipe
        CRequestEditRecipes
        CSaveRecipe

        'resources
        CRequestEditResource
        CSaveResource

        'shops
        CRequestEditShop
        CSaveShop

        'skills
        CRequestEditSkill
        CSaveSkill

        ' Make sure COUNT is below everything else
        Count
    End Enum

    ' Packets sent by server to client
    Friend Enum ServerPackets
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

End Module