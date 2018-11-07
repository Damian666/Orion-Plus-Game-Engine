Module E_Types

    ' options
    Friend Options As New E_Options()

    ' Friend data structures
    Friend Map As MapRec

    Friend TempTile(,) As TempTileRec
    Friend MapLock As New Object()
    Friend MapItem(MAX_MAP_ITEMS) As MapItemRec
    Friend MapNpc(MAX_MAP_NPCS) As MapNpcRec

    'Mapreport
    Friend MapNames(MAX_MAPS) As String

    Friend Structure MapRec
        Dim mapNum As Integer
        Dim Name As String
        Dim Music As String

        Dim Revision As Integer
        Dim Moral As Byte
        Dim tileset As Integer

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
        Dim Fogindex As Integer
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
    End Structure

    Friend Structure MapNpcRec
        Dim Num As Byte
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

End Module