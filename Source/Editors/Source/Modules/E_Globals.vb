Module E_Globals
    Friend Const INSTANCED_MAP_MASK As Integer = 16777216 '1 << 24
    Friend Const MAP_NUMBER_MASK As Integer = INSTANCED_MAP_MASK - 1

    Friend SelectedTab As Byte
    Friend HideCursor As Boolean
    Friend TakeScreenShot As Boolean
    Friend ScreenShotTimer As Integer
    Friend MakeCache As Boolean
    Friend FPS As Integer

    Friend GameStarted As Boolean
    Friend GameDestroyed As Boolean

    Friend TilesetsClr() As Color
    Friend LastTileset As Byte

    ' Gfx Path and variables
    Friend Const GFX_PATH As String = "\Data\graphics\"

    Friend Const GFX_GUI_PATH As String = "\Data\graphics\gui\"
    Friend Const GFX_EXT As String = ".png"

    ' path constants
    Friend Const SOUND_PATH As String = "\Data\sound\"

    Friend Const MUSIC_PATH As String = "\Data\music\"

    Friend Max_Classes As Byte

    Friend MapData As Boolean

    ' Cache the Resources in an array
    Friend MapResource() As MapResourceRec

    Friend Resource_index As Integer
    Friend Resources_Init As Boolean

    ' fog
    Friend fogOffsetX As Integer

    Friend fogOffsetY As Integer

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

    ' Editor edited items array
    Friend Item_Changed(MAX_ITEMS) As Boolean

    Friend NPC_Changed(MAX_NPCS) As Boolean
    Friend Resource_Changed(MAX_RESOURCES) As Boolean
    Friend Animation_Changed(MAX_ANIMATIONS) As Boolean
    Friend Skill_Changed(MAX_SKILLS) As Boolean
    Friend Shop_Changed(MAX_SHOPS) As Boolean

    'Editors
    Friend InitEditor As Boolean

    Friend InitMapEditor As Boolean
    Friend InitItemEditor As Boolean
    Friend InitResourceEditor As Boolean
    Friend InitNPCEditor As Boolean
    Friend InitSkillEditor As Boolean
    Friend InitShopEditor As Boolean
    Friend InitAnimationEditor As Boolean
    Friend InitClassEditor As Boolean
    Friend InitAutoMapper As Boolean

    Friend InitMapProperties As Boolean

    ' Game editor constants
    Friend Const EDITOR_ITEM As Byte = 1

    Friend Const EDITOR_NPC As Byte = 2
    Friend Const EDITOR_SKILL As Byte = 3
    Friend Const EDITOR_SHOP As Byte = 4
    Friend Const EDITOR_RESOURCE As Byte = 5
    Friend Const EDITOR_ANIMATION As Byte = 6
    Friend Const EDITOR_QUEST As Byte = 7
    Friend Const EDITOR_HOUSE As Byte = 8
    Friend Const EDITOR_RECIPE As Byte = 9
    Friend Const EDITOR_CLASSES As Byte = 10

    'Mapreport
    Friend UpdateMapnames As Boolean

    ' Game editors
    Friend Editor As Byte

    Friend Editorindex As Integer
    Friend AnimEditorFrame(1) As Integer
    Friend AnimEditorTimer(1) As Integer

    ' Used to check if in editor or not and variables for use in editor
    Friend InMapEditor As Boolean

    Friend EditorTileX As Integer
    Friend EditorViewX As Integer
    Friend EditorViewY As Integer
    Friend EditorTileY As Integer
    Friend EditorTileWidth As Integer
    Friend EditorTileHeight As Integer
    Friend EditorWarpMap As Integer
    Friend EditorWarpX As Integer
    Friend EditorWarpY As Integer
    Friend SpawnNpcNum As Integer
    Friend SpawnNpcDir As Byte
    Friend EditorShop As Integer
    Friend EditorTileSelStart As Point
    Friend EditorTileSelEnd As Point

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

    ' Map constants
    Friend SCREEN_MAPX As Byte = 35

    Friend SCREEN_MAPY As Byte = 26

    ' Used to freeze controls when getting a new map
    Friend GettingMap As Boolean

    ' Font variables
    Friend Const FONT_NAME As String = "Arial.ttf"

    Friend Const FONT_SIZE As Byte = 13

    ' Tile size constants
    Friend Const PIC_X As Integer = 32

    Friend Const PIC_Y As Integer = 32

    ' Sprite, item, skill size constants
    Friend Const SIZE_X As Integer = 32

    Friend Const SIZE_Y As Integer = 32

    'Graphics
    Friend Const FPS_LIMIT As Integer = 64

    Friend Camera As Rectangle
    Friend TileView As Rect

    ' for directional blocking
    Friend DirArrowX(4) As Byte

    Friend DirArrowY(4) As Byte

    Friend HalfX As Integer = ((SCREEN_MAPX + 1) \ 2) * PIC_X
    Friend HalfY As Integer = ((SCREEN_MAPY + 1) \ 2) * PIC_Y
    Friend ScreenX As Integer = (SCREEN_MAPX + 1) * PIC_X
    Friend ScreenY As Integer = (SCREEN_MAPY + 1) * PIC_Y

    ' Number of tiles in width in tilesets
    Friend Const TILESHEET_WIDTH As Integer = 15 ' * PIC_X pixels

    Friend MapGrid As Boolean

    ' Mouse cursor tile location
    Friend CurX As Integer

    Friend CurY As Integer
    Friend CurMouseX As Integer
    Friend CurMouseY As Integer

    ' Draw map name location
    Friend DrawMapNameX As Single

    Friend DrawMapNameY As Single
    Friend DrawMapNameColor As SFML.Graphics.Color

    Friend LoadClassInfo As Boolean

    Friend EKeyPair As New ASFW.IO.Encryption.KeyPair()
End Module