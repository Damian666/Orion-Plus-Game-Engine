Imports System.Drawing

Module C_Variables

    'char creation/selecting
    Friend SelectedChar As Byte

    Friend MaxChars As Byte

    Friend TotalOnline As Integer

    ' for directional blocking
    Friend DirArrowX(4) As Byte

    Friend DirArrowY(4) As Byte

    Friend LastTileset As Byte

    Friend UseFade As Boolean
    Friend FadeType As Integer
    Friend FadeAmount As Integer
    Friend FlashTimer As Integer

    ' targetting
    Friend MyTarget As Integer
    Friend MyTargetType As Integer

    ' chat bubble
    Friend ChatBubble(Byte.MaxValue) As ChatBubbleRec
    Friend ChatBubbleindex As Integer

    ' skill drag + drop
    Friend DragSkillSlotNum As Integer

    Friend SkillX As Integer
    Friend SkillY As Integer

    ' gui
    Friend EqX As Integer

    Friend EqY As Integer
    Friend Fps As Integer
    Friend Lps As Integer
    Friend PingToDraw As String
    Friend ShowRClick As Boolean

    Friend LastSkillDesc As Integer ' Stores the last skill we showed in desc

    Friend TmpCurrencyItem As Integer

    Friend CurrencyMenu As Byte
    Friend HideGui As Boolean

    ' Player variables
    Friend Myindex As Integer ' Index of actual player


    Friend PlayerSkills(MAX_PLAYER_SKILLS) As Byte
    Friend InventoryItemSelected As Integer
    Friend SkillBuffer As Integer
    Friend SkillBufferTimer As Integer
    Friend SkillCd(MAX_PLAYER_SKILLS) As Integer
    Friend StunDuration As Integer
    Friend NextlevelExp As Integer

    ' Stops movement when updating a map
    Friend CanMoveNow As Boolean

    ' Controls main gameloop
    Friend InGame As Boolean

    Friend IsLogging As Boolean
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
    Friend GettingMap As Boolean = True

    ' Used to check if FPS needs to be drawn
    Friend Bfps As Boolean

    Friend Blps As Boolean
    Friend BLoc As Boolean

    ' FPS and Time-based movement vars
    Friend ElapsedTime As Integer

    Friend GameFps As Integer
    Friend GameLps As Integer

    ' Text vars
    Friend VbQuote As String

    ' Mouse cursor tile location
    Friend CurX As Integer

    Friend CurY As Integer
    Friend CurMouseX As Integer
    Friend CurMouseY As Integer

    ' Game editors
    Friend Editor As Byte

    Friend Editorindex As Integer

    ' Used to check if in editor or not and variables for use in editor
    Friend SpawnNpcNum As Integer

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

    Friend Camera As Rectangle
    Friend TileView As Rect

    ' Pinging
    Friend PingStart As Integer

    Friend PingEnd As Integer
    Friend Ping As Integer

    ' indexing
    Friend ActionMsgIndex As Byte

    Friend BloodIndex As Byte


    ' New char
    Friend NewCharSprite As Integer

    Friend NewCharClass As Integer

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

    Friend ShakeTimerEnabled As Boolean
    Friend ShakeTimer As Integer
    Friend ShakeCount As Byte
    Friend LastDir As Byte

    Friend ShowAnimLayers As Boolean
    Friend ShowAnimTimer As Integer

    Friend EKeyPair As New ASFW.IO.Encryption.KeyPair()

    ' Editor edited items array
    Friend Item_Changed(MAX_ITEMS) As Boolean
    Friend NPC_Changed(MAX_NPCS) As Boolean
    Friend Resource_Changed(MAX_RESOURCES) As Boolean
    Friend Animation_Changed(MAX_ANIMATIONS) As Boolean
    Friend Skill_Changed(MAX_SKILLS) As Boolean
    Friend Shop_Changed(MAX_SHOPS) As Boolean
    Friend Pet_Changed(MAX_PETS) As Boolean

    Friend AnimEditorFrame(1) As Integer
    Friend AnimEditorTimer(1) As Integer

    'Editors
    Friend InitEditor As Boolean
    Friend InitMapEditor As Boolean
    Friend InitPetEditor As Boolean
    Friend InitItemEditor As Boolean
    Friend InitResourceEditor As Boolean
    Friend InitNPCEditor As Boolean
    Friend InitSkillEditor As Boolean
    Friend InitShopEditor As Boolean
    Friend InitAnimationEditor As Boolean
    Friend InitClassEditor As Boolean
    Friend InitAutoMapper As Boolean

    ' Game editor constants
    Friend Const EDITOR_ITEM As Byte = 1
    Friend Const EDITOR_NPC As Byte = 2
    Friend Const EDITOR_SKILL As Byte = 3
    Friend Const EDITOR_SHOP As Byte = 4
    Friend Const EDITOR_RESOURCE As Byte = 5
    Friend Const EDITOR_ANIMATION As Byte = 6
    Friend Const EDITOR_PET As Byte = 7
    Friend Const EDITOR_QUEST As Byte = 7
    Friend Const EDITOR_HOUSE As Byte = 8
    Friend Const EDITOR_RECIPE As Byte = 9
    Friend Const EDITOR_CLASSES As Byte = 10
End Module