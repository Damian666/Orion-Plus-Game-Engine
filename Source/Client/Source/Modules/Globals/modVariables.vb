﻿Imports System.Drawing

Module modVariables
    'char creation/selecting
    Friend SelectedChar As Byte
    Friend MaxChars As Byte

    Friend TotalOnline As Integer

    ' for directional blocking
    Friend DirArrowX(0 To 4) As Byte
    Friend DirArrowY(0 To 4) As Byte

    Friend TilesetsClr() As Color
    Friend LastTileset As Byte

    Friend UseFade As Boolean
    Friend FadeType As Integer
    Friend FadeAmount As Integer
    Friend FlashTimer As Integer

    ' targetting
    Friend myTarget As Integer
    Friend myTargetType As Integer

    ' chat bubble
    Friend chatBubble(0 To Byte.MaxValue) As ChatBubbleRec
    Friend chatBubbleIndex As Integer

    ' Cache the Resources in an array
    Friend MapResource() As MapResourceRec
    Friend Resource_Index As Integer
    Friend Resources_Init As Boolean

    ' inv drag + drop
    Friend DragInvSlotNum As Integer
    Friend InvX As Integer
    Friend InvY As Integer

    ' skill drag + drop
    Friend DragSkillSlotNum As Integer
    Friend SkillX As Integer
    Friend SkillY As Integer

    ' bank drag + drop
    Friend DragBankSlotNum As Integer
    Friend BankX As Integer
    Friend BankY As Integer

    ' gui
    Friend EqX As Integer
    Friend EqY As Integer
    Friend FPS As Integer
    Friend LPS As Integer
    Friend PingToDraw As String
    Friend ShowRClick As Boolean

    Friend InvItemFrame(0 To MAX_INV) As Byte ' Used for animated items
    Friend LastItemDesc As Integer ' Stores the last item we showed in desc
    Friend LastSkillDesc As Integer ' Stores the last skill we showed in desc
    Friend LastBankDesc As Integer ' Stores the last bank item we showed in desc
    Friend tmpCurrencyItem As Integer
    Friend InShop As Integer ' is the player in a shop?
    Friend ShopAction As Byte ' stores the current shop action
    Friend InBank As Integer
    Friend CurrencyMenu As Byte
    Friend HideGui As Boolean

    ' Player variables
    Friend MyIndex As Integer ' Index of actual player
    Friend PlayerInv(0 To MAX_INV) As PlayerInvRec   ' Inventory
    Friend PlayerSkills(0 To MAX_PLAYER_SKILLS) As Byte
    Friend InventoryItemSelected As Integer
    Friend SkillBuffer As Integer
    Friend SkillBufferTimer As Integer
    Friend SkillCD(0 To MAX_PLAYER_SKILLS) As Integer
    Friend StunDuration As Integer
    Friend NextlevelExp As Integer

    ' Stops movement when updating a map
    Friend CanMoveNow As Boolean

    ' Controls main gameloop
    Friend InGame As Boolean
    Friend isLogging As Boolean
    Friend MapData As Boolean
    Friend PlayerData As Boolean

    ' Text variables

    ' Draw map name location
    Friend DrawMapNameX As Single = 110
    Friend DrawMapNameY As Single = 70
    Friend DrawMapNameColor As SFML.Graphics.Color

    ' Game direction vars
    Friend DirUp As Boolean
    Friend DirDown As Boolean
    Friend DirLeft As Boolean
    Friend DirRight As Boolean
    Friend ShiftDown As Boolean
    Friend ControlDown As Boolean

    ' Used for dragging Picture Boxes
    Friend SOffsetX As Integer
    Friend SOffsetY As Integer

    ' Used to freeze controls when getting a new map
    Friend GettingMap As Boolean

    ' Used to check if FPS needs to be drawn
    Friend BFPS As Boolean
    Friend BLPS As Boolean
    Friend BLoc As Boolean

    ' FPS and Time-based movement vars
    Friend ElapsedTime As Integer
    'Friend ElapsedMTime As Integer
    Friend GameFPS As Integer
    Friend GameLPS As Integer

    ' Text vars
    Friend vbQuote As String

    ' Mouse cursor tile location
    Friend CurX As Integer
    Friend CurY As Integer
    Friend CurMouseX As Integer
    Friend CurMouseY As Integer

    ' Game editors
    Friend Editor As Byte
    Friend EditorIndex As Integer
    Friend AnimEditorFrame(0 To 1) As Integer
    Friend AnimEditorTimer(0 To 1) As Integer

    ' Used to check if in editor or not and variables for use in editor
    Friend SpawnNpcNum As Byte
    Friend SpawnNpcDir As Byte

    ' Used for map item editor
    Friend ItemEditorNum As Integer
    Friend ItemEditorValue As Integer

    ' Used for map key editor
    Friend KeyEditorNum As Integer
    Friend KeyEditorTake As Integer

    ' Used for map key open editor
    Friend KeyOpenEditorX As Integer
    Friend KeyOpenEditorY As Integer

    ' Map Resources
    Friend ResourceEditorNum As Integer

    ' Used for map editor heal & trap & slide tiles
    Friend MapEditorHealType As Integer
    Friend MapEditorHealAmount As Integer
    Friend MapEditorSlideDir As Integer

    ' Maximum classes
    Friend Max_Classes As Byte

    Friend Camera As Rectangle
    Friend TileView As Rect

    ' Pinging
    Friend PingStart As Integer
    Friend PingEnd As Integer
    Friend Ping As Integer

    ' indexing
    Friend ActionMsgIndex As Byte
    Friend BloodIndex As Byte
    Friend AnimationIndex As Byte

    ' Editor edited items array
    Friend Item_Changed(MAX_ITEMS) As Boolean
    Friend NPC_Changed(MAX_NPCS) As Boolean
    Friend Resource_Changed(MAX_NPCS) As Boolean
    Friend Animation_Changed(MAX_ANIMATIONS) As Boolean
    Friend Skill_Changed(MAX_SKILLS) As Boolean
    Friend Shop_Changed(MAX_SHOPS) As Boolean

    ' New char
    Friend newCharSprite As Integer
    Friend newCharClass As Integer

    Friend TempMapData() As Byte

    'dialog
    Friend DialogType As Byte
    Friend DialogMsg1 As String
    Friend DialogMsg2 As String
    Friend DialogMsg3 As String
    Friend UpdateDialog As Boolean
    Friend DialogButton1Text As String
    Friend DialogButton2Text As String

    'store news here
    Friend News As String
    Friend UpdateNews As Boolean

    ' fog
    Friend fogOffsetX As Integer
    Friend fogOffsetY As Integer

    'Weather Stuff... events take precedent OVER map settings so we will keep temp map weather settings here.
    Friend CurrentWeather As Integer
    Friend CurrentWeatherIntensity As Integer
    Friend CurrentFog As Integer
    Friend CurrentFogSpeed As Integer
    Friend CurrentFogOpacity As Integer
    Friend CurrentTintR As Integer
    Friend CurrentTintG As Integer
    Friend CurrentTintB As Integer
    Friend CurrentTintA As Integer
    Friend DrawThunder As Integer

    Friend ShakeTimerEnabled As Boolean
    Friend ShakeTimer As Integer
    Friend ShakeCount As Byte
    Friend LastDir As Byte

    Friend CraftTimerEnabled As Boolean
    Friend CraftTimer As Integer

    Friend ShowAnimLayers As Boolean
    Friend ShowAnimTimer As Integer


    Friend EKeyPair As New ASFW.IO.Encryption.KeyPair()
End Module