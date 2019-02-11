Friend Module modTypes
    ' Common data structure arrays
    Friend Classes() As ClassStruct
    Friend Item(MAX_ITEMS) As ItemStruct
    Friend Npc(MAX_NPCS) As NpcStruct
    Friend Shop(MAX_SHOPS) As ShopStruct
    Friend Skill(MAX_SKILLS) As SkillStruct
    Friend Resource(MAX_RESOURCES) As ResourceStruct
    Friend Animation(MAX_ANIMATIONS) As AnimationStruct

    ' Common data structures
    Friend Structure RandInvStruct
        Dim Prefix As String
        Dim Suffix As String
        Dim Stat() As Integer
        Dim Rarity As Integer
        Dim Damage As Integer
        Dim Speed As Integer
    End Structure

    Friend Structure ResourceSkillsStruct
        Dim SkillLevel As Integer
        Dim SkillCurExp As Integer
        Dim SkillNextLvlExp As Integer
    End Structure

    Friend Structure AnimationStruct
        Dim Name As String
        Dim Sound As String
        Dim Sprite() As Integer
        Dim Frames() As Integer
        Dim LoopCount() As Integer
        Dim LoopTime() As Integer
    End Structure

    Friend Structure Rect
        Dim Top As Integer
        Dim Left As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure

    Friend Structure ResourceStruct
        Dim Name As String
        Dim SuccessMessage As String
        Dim EmptyMessage As String
        Dim ResourceType As Integer
        Dim ResourceImage As Integer
        Dim ExhaustedImage As Integer
        Dim ExpReward As Integer
        Dim ItemReward As Integer
        Dim LvlRequired As Integer
        Dim ToolRequired As Integer
        Dim Health As Integer
        Dim RespawnTime As Integer
        Dim Walkthrough As Boolean
        Dim Animation As Integer
    End Structure

    Friend Structure SkillStruct
        Dim Name As String
        Dim Type As Byte
        Dim MpCost As Integer
        Dim LevelReq As Integer
        Dim AccessReq As Integer
        Dim ClassReq As Integer
        Dim CastTime As Integer
        Dim CdTime As Integer
        Dim Icon As Integer
        Dim Map As Integer
        Dim X As Integer
        Dim Y As Integer
        Dim Dir As Byte
        Dim Vital As Integer
        Dim Duration As Integer
        Dim Interval As Integer
        Dim Range As Integer
        Dim IsAoE As Boolean
        Dim AoE As Integer
        Dim CastAnim As Integer
        Dim SkillAnim As Integer
        Dim StunDuration As Integer

        'projectiles
        Dim IsProjectile As Integer '0 is no, 1 is yes
        Dim Projectile As Integer

        Dim KnockBack As Byte '0 is no, 1 is yes
        Dim KnockBackTiles As Byte
    End Structure

    Friend Structure ShopStruct
        Dim Name As String
        Dim Face As Byte
        Dim BuyRate As Integer
        Dim TradeItem() As TradeItemStruct
    End Structure

    Friend Structure PlayerInvStruct
        Dim Num As Integer
        Dim Value As Integer
    End Structure

    Friend Structure BankStruct
        Dim Item() As PlayerInvStruct
        Dim ItemRand() As RandInvStruct
    End Structure

    Friend Structure TileDataStruct
        Dim X As Byte
        Dim Y As Byte
        Dim Tileset As Byte
        Dim AutoTile As Byte
    End Structure

    Friend Structure TileStruct
        Dim Layer() As TileDataStruct
        Dim Type As Byte
        Dim Data1 As Integer
        Dim Data2 As Integer
        Dim Data3 As Integer
        Dim DirBlock As Byte
    End Structure

    Friend Structure ItemStruct
        Dim Name As String
        Dim Pic As Integer
        Dim Description As String

        Dim Type As Byte
        Dim SubType As Byte
        Dim Data1 As Integer
        Dim Data2 As Integer
        Dim Data3 As Integer
        Dim ClassReq As Integer
        Dim AccessReq As Integer
        Dim LevelReq As Integer
        Dim Mastery As Byte
        Dim Price As Integer
        Dim Add_Stat() As Byte
        Dim Rarity As Byte
        Dim Speed As Integer
        Dim TwoHanded As Integer
        Dim BindType As Byte
        Dim Stat_Req() As Byte
        Dim Animation As Integer
        Dim Paperdoll As Integer

        Dim Randomize As Byte
        Dim RandomMin As Byte
        Dim RandomMax As Byte

        Dim Stackable As Byte
        Dim ItemLevel As Byte

        'Housing
        Dim FurnitureWidth As Integer
        Dim FurnitureHeight As Integer
        Dim FurnitureBlocks(,) As Integer
        Dim FurnitureFringe(,) As Integer

        Dim KnockBack As Byte
        Dim KnockBackTiles As Byte

        Dim Projectile As Integer
        Dim Ammo As Integer
    End Structure

    Friend Structure AnimInstanceStruct
        Dim Animation As Integer
        Dim X As Integer
        Dim Y As Integer
        ' used for locking to players/npcs
        Dim lockindex As Integer
        Dim LockType As Byte
        ' timing
        Dim Timer() As Integer
        ' rendering check
        Dim Used() As Boolean
        ' counting the loop
        Dim LoopIndex() As Integer
        Dim FrameIndex() As Integer
    End Structure

    Friend Structure NpcStruct
        Dim Name As String
        Dim AttackSay As String
        Dim Sprite As Integer
        Dim SpawnTime As Byte
        Dim SpawnSecs As Integer
        Dim Behaviour As Byte
        Dim Range As Byte
        Dim DropChance() As Integer
        Dim DropItem() As Integer
        Dim DropItemValue() As Integer
        Dim Stat() As Byte
        Dim Faction As Byte
        Dim Hp As Integer
        Dim Exp As Integer
        Dim Animation As Integer
        Dim QuestNum As Integer
        Dim Skill() As Byte

        Dim Level As Integer
        Dim Damage As Integer
    End Structure

    Friend Structure TradeItemStruct
        Dim Item As Integer
        Dim ItemValue As Integer
        Dim CostItem As Integer
        Dim CostValue As Integer
    End Structure

    Friend Structure ClassStruct
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
End Module