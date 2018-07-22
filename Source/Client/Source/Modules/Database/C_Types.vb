Module C_Types
    ' options
    Friend Options As New C_Options()

    ' Friend data structures
    Friend Map As MapRec
    Friend MapLock As New Object()
    Friend Bank As BankRec
    Friend MapItem(MAX_MAP_ITEMS) As MapItemRec
    Friend MapNpc(MAX_MAP_NPCS) As MapNpcRec
    Friend TempTile(,) As TempTileRec
    Friend Player(MAX_PLAYERS) As PlayerRec

    ' client-side stuff
    Friend ActionMsg(Byte.MaxValue) As ActionMsgRec
    Friend Blood(Byte.MaxValue) As BloodRec
    Friend AnimInstance(Byte.MaxValue) As AnimInstanceRec
    Friend Chat As New List(Of ChatRec)

    'Mapreport
    Friend MapNames(MAX_MAPS) As String

    Friend CharSelection() As CharSelRec
    Friend Structure CharSelRec
        Dim Name As String
        Dim Sprite As Integer
        Dim Gender As Integer
        Dim ClassName As String
        Dim Level As Integer
    End Structure

    Friend Structure ChatRec
        Dim Text As String
        Dim Color As Integer
        Dim Y As Byte
    End Structure

    Friend Structure SkillAnim
        Dim Skillnum As Integer
        Dim Timer As Integer
        Dim FramePointer As Integer
    End Structure

    Friend Structure PlayerRec
        ' General
        Dim Name As String
        Dim Classes As Byte
        Dim Sprite As Integer
        Dim Level As Byte
        Dim Exp As Integer
        Dim Access As Byte
        Dim Pk As Byte
        ' Vitals
        Dim Vital() As Integer
        ' Stats
        Dim Stat() As Byte
        Dim Points As Byte
        ' Worn equipment
        Dim Equipment() As Integer
        ' Position
        Dim Map As Integer
        Dim X As Byte
        Dim Y As Byte
        Dim Dir As Byte

        ' Client use only
        Dim MaxHp As Integer
        Dim MaxMp As Integer
        Dim MaxSp As Integer
        Dim XOffset As Integer
        Dim YOffset As Integer
        Dim Moving As Byte
        Dim Attacking As Byte
        Dim AttackTimer As Integer
        Dim MapGetTimer As Integer
        Dim Steps As Byte

        Dim Emote As Integer
        Dim EmoteTimer As Integer

        Dim PlayerQuest() As PlayerQuestRec

        'Housing
        Dim House As PlayerHouseRec

        Dim InHouse As Integer
        Dim LastMap As Integer
        Dim LastX As Integer
        Dim LastY As Integer

        Dim Hotbar() As HotbarRec

        Dim EventTimer As Integer

        'gather skills
        Dim GatherSkills() As ResourceSkillsRec

        Dim RecipeLearned() As Byte

        ' Random Items
        Dim RandInv() As RandInvRec
        Dim RandEquip() As RandInvRec

        Dim Pet As PlayerPetRec
    End Structure

    Friend Structure MapRec
        Dim Name As String
        Dim Music As String

        Dim Revision As Integer
        Dim Moral As Byte
        Dim Tileset As Integer

        Dim Up As Integer
        Dim Down As Integer
        Dim Left As Integer
        Dim Right As Integer

        Dim BootMap As Integer
        Dim BootX As Byte
        Dim BootY As Byte

        Dim MaxX As Byte
        Dim MaxY As Byte

        Dim Tile(,) As TileRec
        Dim Npc() As Integer
        Dim EventCount As Integer
        Dim Events() As EventRec

        Dim WeatherType As Byte
        Dim Fogindex as integer
        Dim WeatherIntensity As Integer
        Dim FogAlpha As Byte
        Dim FogSpeed As Byte

        Dim HasMapTint As Byte
        Dim MapTintR As Byte
        Dim MapTintG As Byte
        Dim MapTintB As Byte
        Dim MapTintA As Byte

        Dim Instanced As Byte

        Dim Panorama As Byte
        Dim Parallax As Byte

        'Client Side Only -- Temporary
        Dim CurrentEvents As Integer
        Dim MapEvents() As MapEventRec
    End Structure

    Friend Structure ClassRec
        Dim Name As String
        Dim Desc As String
        Dim Stat() As Byte
        Dim MaleSprite() As Integer
        Dim FemaleSprite() As Integer
        Dim StartItem() As Integer
        Dim StartValue() As Integer
        Dim StartMap As Integer
        Dim StartX As Byte
        Dim StartY As Byte
        Dim BaseExp As Integer
        ' For client use
        Dim Vital() As Integer
    End Structure

    Friend Structure MapItemRec
        Dim Num As Integer
        Dim Value As Integer
        Dim Frame As Byte
        Dim X As Byte
        Dim Y As Byte

        Dim RandData As RandInvRec
    End Structure

    Friend Structure MapNpcRec
        Dim Num As Integer
        Dim Target As Integer
        Dim TargetType As Byte
        Dim Vital() As Integer
        Dim Map As Integer
        Dim X As Byte
        Dim Y As Byte
        Dim Dir As Byte
        ' Client use only
        Dim XOffset As Integer
        Dim YOffset As Integer
        Dim Moving As Byte
        Dim Attacking As Byte
        Dim AttackTimer As Integer
        Dim Steps As Integer
    End Structure

    Friend Structure ChatBubbleRec
        Dim Msg As String
        Dim Colour As Integer
        Dim Target As Integer
        Dim TargetType As Byte
        Dim Timer As Integer
        Dim Active As Boolean
    End Structure

    Friend Structure TempTileRec
        Dim DoorOpen As Byte
        Dim DoorFrame As Byte
        Dim DoorTimer As Integer
        Dim DoorAnimate As Byte ' 0 = nothing| 1 = opening | 2 = closing
    End Structure

    Friend Structure MapResourceRec
        Dim X As Integer
        Dim Y As Integer
        Dim ResourceState As Byte
    End Structure

    Friend Structure ActionMsgRec
        Dim Message As String
        Dim Created As Integer
        Dim Type As Integer
        Dim Color As Integer
        Dim Scroll As Integer
        Dim X As Integer
        Dim Y As Integer
        Dim Timer As Integer
    End Structure

    Friend Structure BloodRec
        Dim Sprite As Integer
        Dim Timer As Integer
        Dim X As Integer
        Dim Y As Integer
    End Structure

End Module