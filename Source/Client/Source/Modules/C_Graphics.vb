Imports System.IO
Imports SFML.Graphics
Imports SFML.Window

Module C_Graphics

#Region "Declarations"

    Friend GameWindow As RenderWindow
    Friend TilesetWindow As RenderWindow

    Friend EditorItem_Furniture As RenderWindow
    Friend EditorSkill_Icon As RenderWindow
    Friend EditorAnimation_Anim1 As RenderWindow
    Friend EditorAnimation_Anim2 As RenderWindow

    Friend SfmlGameFont As SFML.Graphics.Font

    Friend CursorGfx As Texture
    Friend CursorSprite As Sprite
    Friend CursorInfo As GraphicInfo

    'TileSets
    Friend TileSetTexture() As Texture
    Friend TileSetImgsGFX() As Bitmap

    Friend TileSetSprite() As Sprite
    Friend TileSetTextureInfo() As GraphicInfo

    'Characters
    Friend CharacterGfx() As Texture

    Friend CharacterSprite() As Sprite
    Friend CharacterGfxInfo() As GraphicInfo

    'Paperdolls
    Friend PaperDollGfx() As Texture

    Friend PaperDollSprite() As Sprite
    Friend PaperDollGfxInfo() As GraphicInfo

    'Items
    Friend ItemsGfx() As Texture

    Friend ItemsSprite() As Sprite
    Friend ItemsGfxInfo() As GraphicInfo

    'Resources
    Friend ResourcesGfx() As Texture

    Friend ResourcesSprite() As Sprite
    Friend ResourcesGfxInfo() As GraphicInfo

    'Animations
    Friend AnimationsGfx() As Texture

    Friend AnimationsSprite() As Sprite
    Friend AnimationsGfxInfo() As GraphicInfo

    'Skills
    Friend SkillIconsGfx() As Texture

    Friend SkillIconsSprite() As Sprite
    Friend SkillIconsGfxInfo() As GraphicInfo

    'Housing
    Friend FurnitureGfx() As Texture

    Friend FurnitureSprite() As Sprite
    Friend FurnitureGfxInfo() As GraphicInfo

    'Faces
    Friend FacesGfx() As Texture

    Friend FacesSprite() As Sprite
    Friend FacesGfxInfo() As GraphicInfo

    'Projectiles
    Friend ProjectileGfx() As Texture

    Friend ProjectileSprite() As Sprite
    Friend ProjectileGfxInfo() As GraphicInfo

    'Fogs
    Friend FogGfx() As Texture

    Friend FogSprite() As Sprite
    Friend FogGfxInfo() As GraphicInfo

    'Emotes
    Friend EmotesGfx() As Texture

    Friend EmotesSprite() As Sprite
    Friend EmotesGfxInfo() As GraphicInfo

    'Panoramas
    Friend PanoramasGfx() As Texture

    Friend PanoramasSprite() As Sprite
    Friend PanoramasGfxInfo() As GraphicInfo

    'Parallax
    Friend ParallaxGfx() As Texture

    Friend ParallaxSprite() As Sprite
    Friend ParallaxGfxInfo() As GraphicInfo

    'Door
    Friend DoorGfx As Texture

    Friend DoorSprite As Sprite
    Friend DoorGfxInfo As GraphicInfo

    'Blood
    Friend BloodGfx As Texture

    Friend BloodSprite As Sprite
    Friend BloodGfxInfo As GraphicInfo

    'Directions
    Friend DirectionsGfx As Texture

    Friend DirectionsSprite As Sprite
    Friend DirectionsGfxInfo As GraphicInfo

    'Weather
    Friend WeatherGfx As Texture

    Friend WeatherSprite As Sprite
    Friend WeatherGfxInfo As GraphicInfo

    'Hotbar
    Friend HotBarGfx As Texture

    Friend HotBarSprite As Sprite
    Friend HotBarGfxInfo As GraphicInfo

    'Chat
    Friend ChatWindowGfx As Texture

    Friend ChatWindowSprite As Sprite
    Friend ChatWindowGfxInfo As GraphicInfo

    'MyChat
    Friend MyChatWindowGfx As Texture

    Friend MyChatWindowSprite As Sprite
    Friend MyChatWindowGfxInfo As GraphicInfo

    'Buttons
    Friend ButtonGfx As Texture

    Friend ButtonSprite As Sprite
    Friend ButtonGfxInfo As GraphicInfo
    Friend ButtonHoverGfx As Texture
    Friend ButtonHoverSprite As Sprite
    Friend ButtonHoverGfxInfo As GraphicInfo

    'Hud
    Friend HudPanelGfx As Texture

    Friend HudPanelSprite As Sprite
    Friend HudPanelGfxInfo As GraphicInfo

    'Bars
    Friend HpBarGfx As Texture

    Friend HpBarSprite As Sprite
    Friend HpBarGfxInfo As GraphicInfo
    Friend MpBarGfx As Texture
    Friend MpBarSprite As Sprite
    Friend MpBarGfxInfo As GraphicInfo
    Friend ExpBarGfx As Texture
    Friend ExpBarSprite As Sprite
    Friend ExpBarGfxInfo As GraphicInfo

    Friend ActionPanelGfx As Texture
    Friend ActionPanelSprite As Sprite
    Friend ActionPanelGfxInfo As GraphicInfo
    Friend ActionPanelButtonsGfx(8) As Texture
    Friend ActionPanelButtonsSprite(8) As Sprite
    Friend ActionPanelButtonGfxInfo(8) As GraphicInfo

    Friend InvPanelGfx As Texture
    Friend InvPanelSprite As Sprite
    Friend InvPanelGfxInfo As GraphicInfo

    Friend SkillPanelGfx As Texture
    Friend SkillPanelSprite As Sprite
    Friend SkillPanelGfxInfo As GraphicInfo

    Friend CharPanelGfx As Texture
    Friend CharPanelSprite As Sprite
    Friend CharPanelGfxInfo As GraphicInfo
    Friend CharPanelPlusGfx As Texture
    Friend CharPanelPlusSprite As Sprite
    Friend CharPanelPlusGfxInfo As GraphicInfo
    Friend CharPanelMinGfx As Texture
    Friend CharPanelMinSprite As Sprite
    Friend CharPanelMinGfxInfo As GraphicInfo

    Friend BankPanelGfx As Texture
    Friend BankPanelSprite As Sprite
    Friend BankPanelGfxInfo As GraphicInfo

    Friend TradePanelGfx As Texture
    Friend TradePanelSprite As Sprite
    Friend TradePanelGfxInfo As GraphicInfo

    Friend ShopPanelGfx As Texture
    Friend ShopPanelSprite As Sprite
    Friend ShopPanelGfxInfo As GraphicInfo

    Friend EventChatGfx As Texture
    Friend EventChatSprite As Sprite
    Friend EventChatGfxInfo As GraphicInfo

    Friend TargetGfx As Texture
    Friend TargetSprite As Sprite
    Friend TargetGfxInfo As GraphicInfo

    Friend DescriptionGfx As Texture
    Friend DescriptionSprite As Sprite
    Friend DescriptionGfxInfo As GraphicInfo

    Friend QuestGfx As Texture
    Friend QuestSprite As Sprite
    Friend QuestGfxInfo As GraphicInfo

    Friend CraftGfx As Texture
    Friend CraftSprite As Sprite
    Friend CraftGfxInfo As GraphicInfo

    Friend ProgBarGfx As Texture
    Friend ProgBarSprite As Sprite
    Friend ProgBarGfxInfo As GraphicInfo

    Friend RClickGfx As Texture
    Friend RClickSprite As Sprite
    Friend RClickGfxInfo As GraphicInfo

    Friend ChatBubbleGfx As Texture
    Friend ChatBubbleSprite As Sprite
    Friend ChatBubbleGfxInfo As GraphicInfo

    Friend PetStatsGfx As Texture
    Friend PetStatsSprite As Sprite
    Friend PetStatsGfxInfo As GraphicInfo

    Friend PetBarGfx As Texture
    Friend PetBarSprite As Sprite
    Friend PetbarGfxInfo As GraphicInfo

    Friend MapTintGfx As New RenderTexture(1152, 864)
    Friend MapTintSprite As Sprite

    Friend MapFadeSprite As Sprite

    ' Number of graphic files
    Friend NumTileSets As Integer

    Friend NumCharacters As Integer
    Friend NumPaperdolls As Integer
    Friend NumItems As Integer
    Friend NumResources As Integer
    Friend NumAnimations As Integer
    Friend NumSkillIcons As Integer
    Friend NumFaces As Integer
    Friend NumFogs As Integer
    Friend NumEmotes As Integer
    Friend NumPanorama As Integer
    Friend NumParallax As Integer

    ' #Day/Night
    Friend NightGfx As New RenderTexture(1152, 864)

    Friend NightSprite As Sprite
    Friend NightGfxInfo As GraphicInfo

    Friend LightGfx As Texture
    Friend LightSprite As Sprite
    Friend LightGfxInfo As GraphicInfo

    Friend ShadowGfx As Texture
    Friend ShadowSprite As Sprite
    Friend ShadowGfxInfo As GraphicInfo

    Friend MapEditorBackBuffer As Bitmap

#End Region

#Region "Types"

    Friend Structure GraphicInfo
        Dim Width As Integer
        Dim Height As Integer
        Dim IsLoaded As Boolean
        Dim TextureTimer As Integer
    End Structure

    Friend Structure GraphicsTiles
        Dim Tile(,) As Texture
    End Structure

#End Region

#Region "initialisation"

    Sub InitGraphics()

        GameWindow = New RenderWindow(FrmGame.picscreen.Handle)
        TilesetWindow = New RenderWindow(FrmEditor_MapEditor.picBackSelect.Handle)

        EditorItem_Furniture = New RenderWindow(frmEditor_Item.picFurniture.Handle)
        EditorSkill_Icon = New RenderWindow(frmEditor_Skill.picSprite.Handle)
        EditorAnimation_Anim1 = New RenderWindow(FrmEditor_Animation.picSprite0.Handle)
        EditorAnimation_Anim2 = New RenderWindow(FrmEditor_Animation.picSprite1.Handle)

        SfmlGameFont = New SFML.Graphics.Font(Environment.GetFolderPath(Environment.SpecialFolder.Fonts) + "\" + FontName)

        'this stuff only loads when needed :)
        ReDim TileSetImgsGFX(0 To NumTileSets)
        ReDim TileSetTexture(NumTileSets)
        ReDim TileSetSprite(NumTileSets)
        ReDim TileSetTextureInfo(NumTileSets)

        ReDim CharacterGfx(NumCharacters)
        ReDim CharacterSprite(NumCharacters)
        ReDim CharacterGfxInfo(NumCharacters)

        ReDim PaperDollGfx(NumPaperdolls)
        ReDim PaperDollSprite(NumPaperdolls)
        ReDim PaperDollGfxInfo(NumPaperdolls)

        ReDim ItemsGfx(NumItems)
        ReDim ItemsSprite(NumItems)
        ReDim ItemsGfxInfo(NumItems)

        ReDim ResourcesGfx(NumResources)
        ReDim ResourcesSprite(NumResources)
        ReDim ResourcesGfxInfo(NumResources)

        ReDim AnimationsGfx(NumAnimations)
        ReDim AnimationsSprite(NumAnimations)
        ReDim AnimationsGfxInfo(NumAnimations)

        ReDim SkillIconsGfx(NumSkillIcons)
        ReDim SkillIconsSprite(NumSkillIcons)
        ReDim SkillIconsGfxInfo(NumSkillIcons)

        ReDim FacesGfx(NumFaces)
        ReDim FacesSprite(NumFaces)
        ReDim FacesGfxInfo(NumFaces)

        ReDim FurnitureGfx(NumFurniture)
        ReDim FurnitureSprite(NumFurniture)
        ReDim FurnitureGfxInfo(NumFurniture)

        ReDim ProjectileGfx(NumProjectiles)
        ReDim ProjectileSprite(NumProjectiles)
        ReDim ProjectileGfxInfo(NumProjectiles)

        ReDim FogGfx(NumFogs)
        ReDim FogSprite(NumFogs)
        ReDim FogGfxInfo(NumFogs)

        ReDim EmotesGfx(NumEmotes)
        ReDim EmotesSprite(NumEmotes)
        ReDim EmotesGfxInfo(NumEmotes)

        ReDim PanoramasGfx(NumPanorama)
        ReDim PanoramasSprite(NumPanorama)
        ReDim PanoramasGfxInfo(NumPanorama)

        ReDim ParallaxGfx(NumParallax)
        ReDim ParallaxSprite(NumParallax)
        ReDim ParallaxGfxInfo(NumParallax)

        'sadly, gui shit is always needed, so we preload it :/
        CursorInfo = New GraphicInfo
        If File.Exists(Path.Graphics & "Misc\Cursor" & GfxExt) Then
            'Load texture first, dont care about memory streams (just use the filename)
            CursorGfx = New Texture(Path.Graphics & "Misc\Cursor" & GfxExt)
            CursorSprite = New Sprite(CursorGfx)

            'Cache the width and height
            CursorInfo.Width = CursorGfx.Size.X
            CursorInfo.Height = CursorGfx.Size.Y
        End If

        DoorGfxInfo = New GraphicInfo
        If File.Exists(Path.Graphics & "Misc\Door" & GfxExt) Then
            'Load texture first, dont care about memory streams (just use the filename)
            DoorGfx = New Texture(Path.Graphics & "Misc\Door" & GfxExt)
            DoorSprite = New Sprite(DoorGfx)

            'Cache the width and height
            DoorGfxInfo.Width = DoorGfx.Size.X
            DoorGfxInfo.Height = DoorGfx.Size.Y
        End If

        BloodGfxInfo = New GraphicInfo
        If File.Exists(Path.Graphics & "Misc\Blood" & GfxExt) Then
            'Load texture first, dont care about memory streams (just use the filename)
            BloodGfx = New Texture(Path.Graphics & "Misc\Blood" & GfxExt)
            BloodSprite = New Sprite(BloodGfx)

            'Cache the width and height
            BloodGfxInfo.Width = BloodGfx.Size.X
            BloodGfxInfo.Height = BloodGfx.Size.Y
        End If

        DirectionsGfxInfo = New GraphicInfo
        If File.Exists(Path.Graphics & "Misc\Direction" & GfxExt) Then
            'Load texture first, dont care about memory streams (just use the filename)
            DirectionsGfx = New Texture(Path.Graphics & "Misc\Direction" & GfxExt)
            DirectionsSprite = New Sprite(DirectionsGfx)

            'Cache the width and height
            DirectionsGfxInfo.Width = DirectionsGfx.Size.X
            DirectionsGfxInfo.Height = DirectionsGfx.Size.Y
        End If

        WeatherGfxInfo = New GraphicInfo
        If File.Exists(Path.Graphics & "Misc\Weather" & GfxExt) Then
            'Load texture first, dont care about memory streams (just use the filename)
            WeatherGfx = New Texture(Path.Graphics & "Misc\Weather" & GfxExt)
            WeatherSprite = New Sprite(WeatherGfx)

            'Cache the width and height
            WeatherGfxInfo.Width = WeatherGfx.Size.X
            WeatherGfxInfo.Height = WeatherGfx.Size.Y
        End If

        HotBarGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Main\HotBar" & GfxExt) Then
            'Load texture first, dont care about memory streams (just use the filename)
            HotBarGfx = New Texture(Path.Gui & "Main\HotBar" & GfxExt)
            HotBarSprite = New Sprite(HotBarGfx)

            'Cache the width and height
            HotBarGfxInfo.Width = HotBarGfx.Size.X
            HotBarGfxInfo.Height = HotBarGfx.Size.Y
        End If

        ChatWindowGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Main\" & "Chat" & GfxExt) Then
            ChatWindowGfx = New Texture(Path.Gui & "Main\" & "Chat" & GfxExt)
            ChatWindowSprite = New Sprite(ChatWindowGfx)

            'Cache the width and height
            ChatWindowGfxInfo.Width = ChatWindowGfx.Size.X
            ChatWindowGfxInfo.Height = ChatWindowGfx.Size.Y
        End If

        MyChatWindowGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Main\" & "MyChat" & GfxExt) Then
            MyChatWindowGfx = New Texture(Path.Gui & "Main\" & "MyChat" & GfxExt)
            MyChatWindowSprite = New Sprite(MyChatWindowGfx)

            'Cache the width and height
            MyChatWindowGfxInfo.Width = MyChatWindowGfx.Size.X
            MyChatWindowGfxInfo.Height = MyChatWindowGfx.Size.Y
        End If

        ButtonGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Button" & GfxExt) Then
            ButtonGfx = New Texture(Path.Gui & "Button" & GfxExt)
            ButtonSprite = New Sprite(ButtonGfx)

            'Cache the width and height
            ButtonGfxInfo.Width = ButtonGfx.Size.X
            ButtonGfxInfo.Height = ButtonGfx.Size.Y
        End If

        ButtonHoverGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Button_Hover" & GfxExt) Then
            ButtonHoverGfx = New Texture(Path.Gui & "Button_Hover" & GfxExt)
            ButtonHoverSprite = New Sprite(ButtonHoverGfx)

            'Cache the width and height
            ButtonHoverGfxInfo.Width = ButtonHoverGfx.Size.X
            ButtonHoverGfxInfo.Height = ButtonHoverGfx.Size.Y
        End If

        HudPanelGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Main\HUD" & GfxExt) Then
            'Load texture first, dont care about memory streams (just use the filename)
            HudPanelGfx = New Texture(Path.Gui & "Main\HUD" & GfxExt)
            HudPanelSprite = New Sprite(HudPanelGfx)

            'Cache the width and height
            HudPanelGfxInfo.Width = HudPanelGfx.Size.X
            HudPanelGfxInfo.Height = HudPanelGfx.Size.Y
        End If

        HpBarGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "HPBar" & GfxExt) Then
            HpBarGfx = New Texture(Path.Gui & "HPBar" & GfxExt)
            HpBarSprite = New Sprite(HpBarGfx)

            'Cache the width and height
            HpBarGfxInfo.Width = HpBarGfx.Size.X
            HpBarGfxInfo.Height = HpBarGfx.Size.Y
        End If

        MpBarGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "MPBar" & GfxExt) Then
            MpBarGfx = New Texture(Path.Gui & "MPBar" & GfxExt)
            MpBarSprite = New Sprite(MpBarGfx)

            'Cache the width and height
            MpBarGfxInfo.Width = MpBarGfx.Size.X
            MpBarGfxInfo.Height = MpBarGfx.Size.Y
        End If

        ExpBarGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "EXPBar" & GfxExt) Then
            ExpBarGfx = New Texture(Path.Gui & "EXPBar" & GfxExt)
            ExpBarSprite = New Sprite(ExpBarGfx)

            'Cache the width and height
            ExpBarGfxInfo.Width = ExpBarGfx.Size.X
            ExpBarGfxInfo.Height = ExpBarGfx.Size.Y
        End If

        ActionPanelGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "ActionBar\ActionBar" & GfxExt) Then
            'Load texture first, dont care about memory streams (just use the filename)
            ActionPanelGfx = New Texture(Path.Gui & "ActionBar\ActionBar" & GfxExt)
            ActionPanelSprite = New Sprite(ActionPanelGfx)

            'Cache the width and height
            ActionPanelGfxInfo.Width = ActionPanelGfx.Size.X
            ActionPanelGfxInfo.Height = ActionPanelGfx.Size.Y
        End If

        InvPanelGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Main\inventory" & GfxExt) Then
            'Load texture first, dont care about memory streams (just use the filename)
            InvPanelGfx = New Texture(Path.Gui & "Main\inventory" & GfxExt)
            InvPanelSprite = New Sprite(InvPanelGfx)

            'Cache the width and height
            InvPanelGfxInfo.Width = InvPanelGfx.Size.X
            InvPanelGfxInfo.Height = InvPanelGfx.Size.Y
        End If

        SkillPanelGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Main\skills" & GfxExt) Then
            'Load texture first, dont care about memory streams (just use the filename)
            SkillPanelGfx = New Texture(Path.Gui & "Main\skills" & GfxExt)
            SkillPanelSprite = New Sprite(SkillPanelGfx)

            'Cache the width and height
            SkillPanelGfxInfo.Width = SkillPanelGfx.Size.X
            SkillPanelGfxInfo.Height = SkillPanelGfx.Size.Y
        End If

        CharPanelGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Main\char" & GfxExt) Then
            'Load texture first, dont care about memory streams (just use the filename)
            CharPanelGfx = New Texture(Path.Gui & "Main\char" & GfxExt)
            CharPanelSprite = New Sprite(CharPanelGfx)

            'Cache the width and height
            CharPanelGfxInfo.Width = CharPanelGfx.Size.X
            CharPanelGfxInfo.Height = CharPanelGfx.Size.Y
        End If

        CharPanelPlusGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Main\plus" & GfxExt) Then
            'Load texture first, dont care about memory streams (just use the filename)
            CharPanelPlusGfx = New Texture(Path.Gui & "Main\plus" & GfxExt)
            CharPanelPlusSprite = New Sprite(CharPanelPlusGfx)

            'Cache the width and height
            CharPanelPlusGfxInfo.Width = CharPanelPlusGfx.Size.X
            CharPanelPlusGfxInfo.Height = CharPanelPlusGfx.Size.Y
        End If

        CharPanelMinGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Main\min" & GfxExt) Then
            'Load texture first, dont care about memory streams (just use the filename)
            CharPanelMinGfx = New Texture(Path.Gui & "Main\min" & GfxExt)
            CharPanelMinSprite = New Sprite(CharPanelMinGfx)

            'Cache the width and height
            CharPanelMinGfxInfo.Width = CharPanelMinGfx.Size.X
            CharPanelMinGfxInfo.Height = CharPanelMinGfx.Size.Y
        End If

        BankPanelGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Main\Bank" & GfxExt) Then
            'Load texture first, dont care about memory streams (just use the filename)
            BankPanelGfx = New Texture(Path.Gui & "Main\Bank" & GfxExt)
            BankPanelSprite = New Sprite(BankPanelGfx)

            'Cache the width and height
            BankPanelGfxInfo.Width = BankPanelGfx.Size.X
            BankPanelGfxInfo.Height = BankPanelGfx.Size.Y
        End If

        ShopPanelGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Main\Shop" & GfxExt) Then
            'Load texture first, dont care about memory streams (just use the filename)
            ShopPanelGfx = New Texture(Path.Gui & "Main\Shop" & GfxExt)
            ShopPanelSprite = New Sprite(ShopPanelGfx)

            'Cache the width and height
            ShopPanelGfxInfo.Width = ShopPanelGfx.Size.X
            ShopPanelGfxInfo.Height = ShopPanelGfx.Size.Y
        End If

        TradePanelGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Main\Trade" & GfxExt) Then
            'Load texture first, dont care about memory streams (just use the filename)
            TradePanelGfx = New Texture(Path.Gui & "Main\Trade" & GfxExt)
            TradePanelSprite = New Sprite(TradePanelGfx)

            'Cache the width and height
            TradePanelGfxInfo.Width = TradePanelGfx.Size.X
            TradePanelGfxInfo.Height = TradePanelGfx.Size.Y
        End If

        EventChatGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Main\EventChat" & GfxExt) Then
            'Load texture first, dont care about memory streams (just use the filename)
            EventChatGfx = New Texture(Path.Gui & "Main\EventChat" & GfxExt)
            EventChatSprite = New Sprite(EventChatGfx)

            'Cache the width and height
            EventChatGfxInfo.Width = EventChatGfx.Size.X
            EventChatGfxInfo.Height = EventChatGfx.Size.Y
        End If

        TargetGfxInfo = New GraphicInfo
        If File.Exists(Path.Graphics & "Misc\Target" & GfxExt) Then
            'Load texture first, dont care about memory streams (just use the filename)
            TargetGfx = New Texture(Path.Graphics & "Misc\Target" & GfxExt)
            TargetSprite = New Sprite(TargetGfx)

            'Cache the width and height
            TargetGfxInfo.Width = TargetGfx.Size.X
            TargetGfxInfo.Height = TargetGfx.Size.Y
        End If

        DescriptionGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Main\" & "Description" & GfxExt) Then
            DescriptionGfx = New Texture(Path.Gui & "Main\" & "Description" & GfxExt)
            DescriptionSprite = New Sprite(DescriptionGfx)

            'Cache the width and height
            DescriptionGfxInfo.Width = DescriptionGfx.Size.X
            DescriptionGfxInfo.Height = DescriptionGfx.Size.Y
        End If

        RClickGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Main\" & "RightClick" & GfxExt) Then
            RClickGfx = New Texture(Path.Gui & "Main\" & "RightClick" & GfxExt)
            RClickSprite = New Sprite(RClickGfx)

            'Cache the width and height
            RClickGfxInfo.Width = RClickGfx.Size.X
            RClickGfxInfo.Height = RClickGfx.Size.Y
        End If

        QuestGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Main\" & "QuestLog" & GfxExt) Then
            QuestGfx = New Texture(Path.Gui & "Main\" & "QuestLog" & GfxExt)
            QuestSprite = New Sprite(QuestGfx)

            'Cache the width and height
            QuestGfxInfo.Width = QuestGfx.Size.X
            QuestGfxInfo.Height = QuestGfx.Size.Y
        End If

        CraftGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Main\" & "Craft" & GfxExt) Then
            CraftGfx = New Texture(Path.Gui & "Main\" & "Craft" & GfxExt)
            CraftSprite = New Sprite(CraftGfx)

            'Cache the width and height
            CraftGfxInfo.Width = CraftGfx.Size.X
            CraftGfxInfo.Height = CraftGfx.Size.Y
        End If

        ProgBarGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Main\" & "ProgBar" & GfxExt) Then
            ProgBarGfx = New Texture(Path.Gui & "Main\" & "ProgBar" & GfxExt)
            ProgBarSprite = New Sprite(ProgBarGfx)

            'Cache the width and height
            ProgBarGfxInfo.Width = ProgBarGfx.Size.X
            ProgBarGfxInfo.Height = ProgBarGfx.Size.Y
        End If

        ChatBubbleGfxInfo = New GraphicInfo
        If File.Exists(Path.Graphics & "Misc\ChatBubble" & GfxExt) Then
            ChatBubbleGfx = New Texture(Path.Graphics & "Misc\ChatBubble" & GfxExt)
            ChatBubbleSprite = New Sprite(ChatBubbleGfx)
            'Cache the width and height
            ChatBubbleGfxInfo.Width = ChatBubbleGfx.Size.X
            ChatBubbleGfxInfo.Height = ChatBubbleGfx.Size.Y
        End If

        PetStatsGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Main\Pet" & GfxExt) Then
            'Load texture first, dont care about memory streams (just use the filename)
            PetStatsGfx = New Texture(Path.Gui & "Main\Pet" & GfxExt)
            PetStatsSprite = New Sprite(PetStatsGfx)

            'Cache the width and height
            PetStatsGfxInfo.Width = PetStatsGfx.Size.X
            PetStatsGfxInfo.Height = PetStatsGfx.Size.Y
        End If

        PetbarGfxInfo = New GraphicInfo
        If File.Exists(Path.Gui & "Main\Petbar" & GfxExt) Then
            'Load texture first, dont care about memory streams (just use the filename)
            PetBarGfx = New Texture(Path.Gui & "Main\Petbar" & GfxExt)
            PetBarSprite = New Sprite(PetBarGfx)

            'Cache the width and height
            PetbarGfxInfo.Width = PetBarGfx.Size.X
            PetbarGfxInfo.Height = PetBarGfx.Size.Y
        End If

        LightGfxInfo = New GraphicInfo
        If File.Exists(Path.Graphics & "Misc\Light" & GfxExt) Then
            LightGfx = New Texture(Path.Graphics & "Misc\Light" & GfxExt)
            LightSprite = New Sprite(LightGfx)

            'Cache the width and height
            LightGfxInfo.Width = LightGfx.Size.X
            LightGfxInfo.Height = LightGfx.Size.Y
        End If

        ShadowGfxInfo = New GraphicInfo
        If File.Exists(Path.Graphics & "Misc\Shadow" & GfxExt) Then
            ShadowGfx = New Texture(Path.Graphics & "Misc\Shadow" & GfxExt)
            ShadowSprite = New Sprite(ShadowGfx)

            'Cache the width and height
            ShadowGfxInfo.Width = ShadowGfx.Size.X
            ShadowGfxInfo.Height = ShadowGfx.Size.Y
        End If
    End Sub

    Friend Sub LoadTexture(index As Integer, texType As Byte)

        If texType = 1 Then 'tilesets
            If index < 0 OrElse index > NumTileSets Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            TileSetTexture(index) = New Texture(Path.Graphics & "tilesets\" & index & GfxExt)
            TileSetSprite(index) = New Sprite(TileSetTexture(index))

            'Cache the width and height
            With TileSetTextureInfo(index)
                .Width = TileSetTexture(index).Size.X
                .Height = TileSetTexture(index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With

        ElseIf texType = 2 Then 'characters
            If index < 0 OrElse index > NumCharacters Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            CharacterGfx(index) = New Texture(Path.Graphics & "characters\" & index & GfxExt)
            CharacterSprite(index) = New Sprite(CharacterGfx(index))

            'Cache the width and height
            With CharacterGfxInfo(index)
                .Width = CharacterGfx(index).Size.X
                .Height = CharacterGfx(index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With

        ElseIf texType = 3 Then 'paperdoll
            If index < 0 OrElse index > NumPaperdolls Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            PaperDollGfx(index) = New Texture(Path.Graphics & "Paperdolls\" & index & GfxExt)
            PaperDollSprite(index) = New Sprite(PaperDollGfx(index))

            'Cache the width and height
            With PaperDollGfxInfo(index)
                .Width = PaperDollGfx(index).Size.X
                .Height = PaperDollGfx(index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With

        ElseIf texType = 4 Then 'items
            If index <= 0 OrElse index > NumItems Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            ItemsGfx(index) = New Texture(Path.Graphics & "items\" & index & GfxExt)
            ItemsSprite(index) = New Sprite(ItemsGfx(index))

            'Cache the width and height
            With ItemsGfxInfo(index)
                .Width = ItemsGfx(index).Size.X
                .Height = ItemsGfx(index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With

        ElseIf texType = 5 Then 'resources
            If index < 0 OrElse index > NumResources Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            ResourcesGfx(index) = New Texture(Path.Graphics & "resources\" & index & GfxExt)
            ResourcesSprite(index) = New Sprite(ResourcesGfx(index))

            'Cache the width and height
            With ResourcesGfxInfo(index)
                .Width = ResourcesGfx(index).Size.X
                .Height = ResourcesGfx(index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With

        ElseIf texType = 6 Then 'animations
            If index <= 0 OrElse index > NumAnimations Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            AnimationsGfx(index) = New Texture(Path.Graphics & "Animations\" & index & GfxExt)
            AnimationsSprite(index) = New Sprite(AnimationsGfx(index))

            'Cache the width and height
            With AnimationsGfxInfo(index)
                .Width = AnimationsGfx(index).Size.X
                .Height = AnimationsGfx(index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With

        ElseIf texType = 7 Then 'faces
            If index < 0 OrElse index > NumFaces Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            FacesGfx(index) = New Texture(Path.Graphics & "Faces\" & index & GfxExt)
            FacesSprite(index) = New Sprite(FacesGfx(index))

            'Cache the width and height
            With FacesGfxInfo(index)
                .Width = FacesGfx(index).Size.X
                .Height = FacesGfx(index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With

        ElseIf texType = 8 Then 'fogs
            If index < 0 OrElse index > NumFogs Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            FogGfx(index) = New Texture(Path.Graphics & "Fogs\" & index & GfxExt)
            FogSprite(index) = New Sprite(FogGfx(index))

            'Cache the width and height
            With FogGfxInfo(index)
                .Width = FogGfx(index).Size.X
                .Height = FogGfx(index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With

        ElseIf texType = 9 Then 'skill icons
            If index <= 0 OrElse index > NumSkillIcons Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            SkillIconsGfx(index) = New Texture(Path.Graphics & "SkillIcons\" & index & GfxExt)
            SkillIconsSprite(index) = New Sprite(SkillIconsGfx(index))

            'Cache the width and height
            With SkillIconsGfxInfo(index)
                .Width = SkillIconsGfx(index).Size.X
                .Height = SkillIconsGfx(index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With

        ElseIf texType = 10 Then 'furniture
            If index < 0 OrElse index > NumFurniture Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            FurnitureGfx(index) = New Texture(Path.Graphics & "Furniture\" & index & GfxExt)
            FurnitureSprite(index) = New Sprite(FurnitureGfx(index))

            'Cache the width and height
            With FurnitureGfxInfo(index)
                .Width = FurnitureGfx(index).Size.X
                .Height = FurnitureGfx(index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With

        ElseIf texType = 11 Then 'projectiles
            If index < 0 OrElse index > NumProjectiles Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            ProjectileGfx(index) = New Texture(Path.Graphics & "Projectiles\" & index & GfxExt)
            ProjectileSprite(index) = New Sprite(ProjectileGfx(index))

            'Cache the width and height
            With ProjectileGfxInfo(index)
                .Width = ProjectileGfx(index).Size.X
                .Height = ProjectileGfx(index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With

        ElseIf texType = 12 Then 'emotes
            If index < 0 OrElse index > NumEmotes Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            EmotesGfx(index) = New Texture(Path.Graphics & "Emotes\" & index & GfxExt)
            EmotesSprite(index) = New Sprite(EmotesGfx(index))

            'Cache the width and height
            With EmotesGfxInfo(index)
                .Width = EmotesGfx(index).Size.X
                .Height = EmotesGfx(index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With

        ElseIf texType = 13 Then 'Panoramas
            If index < 0 OrElse index > NumPanorama Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            PanoramasGfx(index) = New Texture(Path.Graphics & "Panoramas\" & index & GfxExt)
            PanoramasSprite(index) = New Sprite(PanoramasGfx(index))

            'Cache the width and height
            With PanoramasGfxInfo(index)
                .Width = PanoramasGfx(index).Size.X
                .Height = PanoramasGfx(index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With
        ElseIf texType = 14 Then 'Parallax
            If index < 0 OrElse index > NumParallax Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            ParallaxGfx(index) = New Texture(Path.Graphics & "Parallax\" & index & GfxExt)
            ParallaxSprite(index) = New Sprite(ParallaxGfx(index))

            'Cache the width and height
            With ParallaxGfxInfo(index)
                .Width = ParallaxGfx(index).Size.X
                .Height = ParallaxGfx(index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With
        End If

    End Sub

#End Region

    Friend Sub DrawEmotes(x2 As Integer, y2 As Integer, sprite As Integer)
        Dim rec As Rectangle
        Dim x As Integer, y As Integer, anim As Integer
        'Dim width As Integer, height As Integer

        If sprite < 1 OrElse sprite > NumEmotes Then Exit Sub

        If EmotesGfxInfo(sprite).IsLoaded = False Then
            LoadTexture(sprite, 12)
        End If

        'seeying we still use it, lets update timer
        With EmotesGfxInfo(sprite)
            .TextureTimer = GetTickCount() + 100000
        End With

        If ShowAnimLayers = True Then
            anim = 1
        Else
            anim = 0
        End If

        With rec
            .Y = 0
            .Height = PicX
            .X = anim * (EmotesGfxInfo(sprite).Width / 2)
            .Width = (EmotesGfxInfo(sprite).Width / 2)
        End With

        x = ConvertMapX(x2)
        y = ConvertMapY(y2) - (PicY + 16)

        RenderSprite(EmotesSprite(sprite), GameWindow, x, y, rec.X, rec.Y, rec.Width, rec.Height)

    End Sub

    Sub DrawChat()
        Dim i As Integer, x As Integer, y As Integer
        Dim text As String

        'first draw back image
        RenderSprite(ChatWindowSprite, GameWindow, ChatWindowX, ChatWindowY - 2, 0, 0, ChatWindowGfxInfo.Width, ChatWindowGfxInfo.Height)

        y = 5
        x = 5

        FirstLineindex = (Chat.Count - MaxChatDisplayLines) - ScrollMod 'First element is the 5th from the last in the list
        If FirstLineindex < 0 Then FirstLineindex = 0 'if the list has less than 5 elements, the first is the 0th index or first element

        LastLineindex = (FirstLineindex + MaxChatDisplayLines) ' - ScrollMod
        If (LastLineindex >= Chat.Count) Then LastLineindex = Chat.Count - 1  'Based off of index 0, so the last element should be Chat.Count -1

        'only loop tru last entries
        For i = FirstLineindex To LastLineindex
            text = Chat(i).Text

            If text <> "" Then ' or not
                DrawText(ChatWindowX + x, ChatWindowY + y, text, GetSfmlColor(Chat(i).Color), SFML.Graphics.Color.Black, GameWindow)
                y = y + ChatLineSpacing + 1
            End If

        Next

        'My Text
        'first draw back image
        RenderSprite(MyChatWindowSprite, GameWindow, MyChatX, MyChatY - 5, 0, 0, MyChatWindowGfxInfo.Width, MyChatWindowGfxInfo.Height)

        If Len(ChatInput.CurrentMessage) > 0 Then
            Dim subText As String = ChatInput.CurrentMessage
            While GetTextWidth(subText) > MyChatWindowGfxInfo.Width - ChatEntryPadding
                subText = subText.Substring(1)
            End While
            DrawText(MyChatX + 5, MyChatY - 3, subText, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        End If
    End Sub

    Friend Sub DrawButton(text As String, destX As Integer, destY As Integer, hover As Byte)
        If hover = 0 Then
            RenderSprite(ButtonSprite, GameWindow, destX, destY, 0, 0, ButtonGfxInfo.Width, ButtonGfxInfo.Height)

            DrawText(destX + (ButtonGfxInfo.Width \ 2) - (GetTextWidth(text) \ 2), destY + (ButtonGfxInfo.Height \ 2) - (FontSize \ 2), text, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        Else
            RenderSprite(ButtonHoverSprite, GameWindow, destX, destY, 0, 0, ButtonHoverGfxInfo.Width, ButtonHoverGfxInfo.Height)

            DrawText(destX + (ButtonHoverGfxInfo.Width \ 2) - (GetTextWidth(text) \ 2), destY + (ButtonHoverGfxInfo.Height \ 2) - (FontSize \ 2), text, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        End If

    End Sub

    Friend Sub RenderSprite(tmpSprite As Sprite, target As RenderWindow, destX As Integer, destY As Integer, sourceX As Integer, sourceY As Integer,
           sourceWidth As Integer, sourceHeight As Integer)

        If tmpSprite Is Nothing Then Exit Sub

        tmpSprite.TextureRect = New IntRect(sourceX, sourceY, sourceWidth, sourceHeight)
        tmpSprite.Position = New Vector2f(destX, destY)
        target.Draw(tmpSprite)
    End Sub

    Friend Sub RenderTextures(txture As Texture, target As RenderWindow, dX As Single, dY As Single, sx As Single, sy As Single, dWidth As Single, dHeight As Single, sWidth As Single, sHeight As Single)
        Dim tmpImage As Sprite = New Sprite(txture) With {
            .TextureRect = New IntRect(sx, sy, sWidth, sHeight),
            .Scale = New Vector2f(dWidth / sWidth, dHeight / sHeight),
            .Position = New Vector2f(dX, dY)
        }
        target.Draw(tmpImage)
    End Sub

    Friend Sub DrawDirections(x As Integer, y As Integer)
        Dim rec As Rectangle, i As Integer

        ' render grid
        rec.Y = 24
        rec.X = 0
        rec.Width = 32
        rec.Height = 32

        RenderSprite(DirectionsSprite, GameWindow, ConvertMapX(x * PicX), ConvertMapY(y * PicY), rec.X, rec.Y, rec.Width, rec.Height)

        ' render dir blobs
        For i = 1 To 4
            rec.X = (i - 1) * 8
            rec.Width = 8
            ' find out whether render blocked or not
            If Not IsDirBlocked(Map.Tile(x, y).DirBlock, (i)) Then
                rec.Y = 8
            Else
                rec.Y = 16
            End If
            rec.Height = 8

            RenderSprite(DirectionsSprite, GameWindow, ConvertMapX(x * PicX) + DirArrowX(i), ConvertMapY(y * PicY) + DirArrowY(i), rec.X, rec.Y, rec.Width, rec.Height)
        Next
    End Sub

    Friend Function ConvertMapX(x As Integer) As Integer
        ConvertMapX = x - (TileView.Left * PicX) - Camera.Left
    End Function

    Friend Function ConvertMapY(y As Integer) As Integer
        ConvertMapY = y - (TileView.Top * PicY) - Camera.Top
    End Function



    Friend Sub DrawPaperdoll(x2 As Integer, y2 As Integer, sprite As Integer, anim As Integer, spritetop As Integer)
        Dim rec As Rectangle
        Dim x As Integer, y As Integer
        Dim width As Integer, height As Integer

        If sprite < 1 OrElse sprite > NumPaperdolls Then Exit Sub

        If PaperDollGfxInfo(sprite).IsLoaded = False Then
            LoadTexture(sprite, 3)
        End If

        ' we use it, lets update timer
        With PaperDollGfxInfo(sprite)
            .TextureTimer = GetTickCount() + 100000
        End With

        With rec
            .Y = spritetop * (PaperDollGfxInfo(sprite).Height / 4)
            .Height = (PaperDollGfxInfo(sprite).Height / 4)
            .X = anim * (PaperDollGfxInfo(sprite).Width / 4)
            .Width = (PaperDollGfxInfo(sprite).Width / 4)
        End With

        x = ConvertMapX(x2)
        y = ConvertMapY(y2)
        width = (rec.Right - rec.Left)
        height = (rec.Bottom - rec.Top)

        RenderSprite(PaperDollSprite(sprite), GameWindow, x, y, rec.X, rec.Y, rec.Width, rec.Height)

    End Sub

    Friend Sub DrawNpc(mapNpcNum As Integer)
        Dim anim As Byte
        Dim x As Integer
        Dim y As Integer
        Dim sprite As Integer, spriteleft As Integer
        Dim destrec As Rectangle
        Dim srcrec As Rectangle
        Dim attackspeed As Integer

        If MapNpc(mapNpcNum).Num = 0 Then Exit Sub ' no npc set

        If MapNpc(mapNpcNum).X < TileView.Left OrElse MapNpc(mapNpcNum).X > TileView.Right Then Exit Sub
        If MapNpc(mapNpcNum).Y < TileView.Top OrElse MapNpc(mapNpcNum).Y > TileView.Bottom Then Exit Sub

        sprite = Npc(MapNpc(mapNpcNum).Num).Sprite

        If sprite < 1 OrElse sprite > NumCharacters Then Exit Sub

        attackspeed = 1000

        ' Reset frame
        anim = 0

        ' Check for attacking animation
        If MapNpc(mapNpcNum).AttackTimer + (attackspeed / 2) > GetTickCount() Then
            If MapNpc(mapNpcNum).Attacking = 1 Then
                anim = 3
            End If
        Else
            ' If not attacking, walk normally
            Select Case MapNpc(mapNpcNum).Dir
                Case DirectionType.Up
                    If (MapNpc(mapNpcNum).YOffset > 8) Then anim = MapNpc(mapNpcNum).Steps
                Case DirectionType.Down
                    If (MapNpc(mapNpcNum).YOffset < -8) Then anim = MapNpc(mapNpcNum).Steps
                Case DirectionType.Left
                    If (MapNpc(mapNpcNum).XOffset > 8) Then anim = MapNpc(mapNpcNum).Steps
                Case DirectionType.Right
                    If (MapNpc(mapNpcNum).XOffset < -8) Then anim = MapNpc(mapNpcNum).Steps
            End Select
        End If

        ' Check to see if we want to stop making him attack
        With MapNpc(mapNpcNum)
            If .AttackTimer + attackspeed < GetTickCount() Then
                .Attacking = 0
                .AttackTimer = 0
            End If
        End With

        ' Set the left
        Select Case MapNpc(mapNpcNum).Dir
            Case DirectionType.Up
                spriteleft = 3
            Case DirectionType.Right
                spriteleft = 2
            Case DirectionType.Down
                spriteleft = 0
            Case DirectionType.Left
                spriteleft = 1
        End Select

        srcrec = New Rectangle((anim) * (CharacterGfxInfo(sprite).Width / 4), spriteleft * (CharacterGfxInfo(sprite).Height / 4), (CharacterGfxInfo(sprite).Width / 4), (CharacterGfxInfo(sprite).Height / 4))

        ' Calculate the X
        x = MapNpc(mapNpcNum).X * PicX + MapNpc(mapNpcNum).XOffset - ((CharacterGfxInfo(sprite).Width / 4 - 32) / 2)

        ' Is the player's height more than 32..?
        If (CharacterGfxInfo(sprite).Height / 4) > 32 Then
            ' Create a 32 pixel offset for larger sprites
            y = MapNpc(mapNpcNum).Y * PicY + MapNpc(mapNpcNum).YOffset - ((CharacterGfxInfo(sprite).Height / 4) - 32)
        Else
            ' Proceed as normal
            y = MapNpc(mapNpcNum).Y * PicY + MapNpc(mapNpcNum).YOffset
        End If

        destrec = New Rectangle(x, y, CharacterGfxInfo(sprite).Width / 4, CharacterGfxInfo(sprite).Height / 4)

        DrawCharacter(sprite, x, y, srcrec)

        If Npc(MapNpc(mapNpcNum).Num).Behaviour = NpcBehavior.Quest Then
            If CanStartQuest(Npc(MapNpc(mapNpcNum).Num).QuestNum) Then
                If Player(Myindex).PlayerQuest(Npc(MapNpc(mapNpcNum).Num).QuestNum).Status = QuestStatusType.NotStarted Then
                    DrawEmotes(x, y, 5)
                End If
            ElseIf Player(Myindex).PlayerQuest(Npc(MapNpc(mapNpcNum).Num).QuestNum).Status = QuestStatusType.Started Then
                DrawEmotes(x, y, 9)
            End If
        End If

    End Sub

    Friend Sub DrawItem(itemnum As Integer)
        Dim srcrec As Rectangle, destrec As Rectangle
        Dim picNum As Integer
        Dim x As Integer, y As Integer

        picNum = Item(MapItem(itemnum).Num).Pic

        If picNum < 1 OrElse picNum > NumItems Then Exit Sub

        If ItemsGfxInfo(picNum).IsLoaded = False Then
            LoadTexture(picNum, 4)
        End If

        'seeying we still use it, lets update timer
        With ItemsGfxInfo(picNum)
            .TextureTimer = GetTickCount() + 100000
        End With

        With MapItem(itemnum)
            If .X < TileView.Left OrElse .X > TileView.Right Then Exit Sub
            If .Y < TileView.Top OrElse .Y > TileView.Bottom Then Exit Sub
        End With

        If ItemsGfxInfo(picNum).Width > 32 Then ' has more than 1 frame
            srcrec = New Rectangle((MapItem(itemnum).Frame * 32), 0, 32, 32)
            destrec = New Rectangle(ConvertMapX(MapItem(itemnum).X * PicX), ConvertMapY(MapItem(itemnum).Y * PicY), 32, 32)
        Else
            srcrec = New Rectangle(0, 0, PicX, PicY)
            destrec = New Rectangle(ConvertMapX(MapItem(itemnum).X * PicX), ConvertMapY(MapItem(itemnum).Y * PicY), PicX, PicY)
        End If

        x = ConvertMapX(MapItem(itemnum).X * PicX)
        y = ConvertMapY(MapItem(itemnum).Y * PicY)

        RenderSprite(ItemsSprite(picNum), GameWindow, x, y, srcrec.X, srcrec.Y, srcrec.Width, srcrec.Height)

    End Sub

    Friend Sub DrawCharacter(sprite As Integer, x2 As Integer, y2 As Integer, rec As Rectangle)
        Dim x As Integer
        Dim y As Integer
        Dim width As Integer
        Dim height As Integer
        'On Error Resume Next

        If sprite < 1 OrElse sprite > NumCharacters Then Exit Sub

        If CharacterGfxInfo(sprite).IsLoaded = False Then
            LoadTexture(sprite, 2)
        End If

        'seeying we still use it, lets update timer
        With CharacterGfxInfo(sprite)
            .TextureTimer = GetTickCount() + 100000
        End With

        x = ConvertMapX(x2)
        y = ConvertMapY(y2)
        width = (rec.Width)
        height = (rec.Height)

        'shadow first
        RenderSprite(ShadowSprite, GameWindow, x - 1, y + 6, 0, 0, ShadowGfxInfo.Width, ShadowGfxInfo.Height)

        RenderSprite(CharacterSprite(sprite), GameWindow, x, y, rec.X, rec.Y, rec.Width, rec.Height)

    End Sub

    Friend Sub DrawBlood(index As Integer)
        Dim dest As Point = New Point(FrmGame.PointToScreen(FrmGame.picscreen.Location))
        Dim srcrec As Rectangle
        Dim destrec As Rectangle
        Dim x As Integer
        Dim y As Integer

        With Blood(index)
            If .X < TileView.Left OrElse .X > TileView.Right Then Exit Sub
            If .Y < TileView.Top OrElse .Y > TileView.Bottom Then Exit Sub

            ' check if we should be seeing it
            If .Timer + 20000 < GetTickCount() Then Exit Sub

            x = ConvertMapX(Blood(index).X * PicX)
            y = ConvertMapY(Blood(index).Y * PicY)

            srcrec = New Rectangle((.Sprite - 1) * PicX, 0, PicX, PicY)

            destrec = New Rectangle(ConvertMapX(.X * PicX), ConvertMapY(.Y * PicY), PicX, PicY)

            RenderSprite(BloodSprite, GameWindow, x, y, srcrec.X, srcrec.Y, srcrec.Width, srcrec.Height)

        End With

    End Sub

    Friend Function IsValidMapPoint(x As Integer, y As Integer) As Boolean
        IsValidMapPoint = False

        If x < 0 Then Exit Function
        If y < 0 Then Exit Function
        If x > Map.MaxX Then Exit Function
        If y > Map.MaxY Then Exit Function
        IsValidMapPoint = True
    End Function

    Friend Sub UpdateCamera()
        Dim offsetX As Integer, offsetY As Integer
        Dim startX As Integer, startY As Integer
        Dim endX As Integer, endY As Integer

        offsetX = Player(Myindex).XOffset + PicX
        offsetY = Player(Myindex).YOffset + PicY
        startX = GetPlayerX(Myindex) - ((ScreenMapx + 1) \ 2) - 1
        startY = GetPlayerY(Myindex) - ((ScreenMapy + 1) \ 2) - 1

        If startX < 0 Then
            offsetX = 0

            If startX = -1 Then
                If Player(Myindex).XOffset > 0 Then
                    offsetX = Player(Myindex).XOffset
                End If
            End If

            startX = 0
        End If

        If startY < 0 Then
            offsetY = 0

            If startY = -1 Then
                If Player(Myindex).YOffset > 0 Then
                    offsetY = Player(Myindex).YOffset
                End If
            End If

            startY = 0
        End If

        endX = startX + (ScreenMapx + 1) + 1
        endY = startY + (ScreenMapy + 1) + 1

        If endX > Map.MaxX Then
            offsetX = 32

            If endX = Map.MaxX + 1 Then
                If Player(Myindex).XOffset < 0 Then
                    offsetX = Player(Myindex).XOffset + PicX
                End If
            End If

            endX = Map.MaxX
            startX = endX - ScreenMapx - 1
        End If

        If endY > Map.MaxY Then
            offsetY = 32

            If endY = Map.MaxY + 1 Then
                If Player(Myindex).YOffset < 0 Then
                    offsetY = Player(Myindex).YOffset + PicY
                End If
            End If

            endY = Map.MaxY
            startY = endY - ScreenMapy - 1
        End If

        With TileView
            .Top = startY
            .Bottom = endY
            .Left = startX
            .Right = endX
        End With

        With Camera
            .Y = offsetY
            .Height = ScreenY + 32
            .X = offsetX
            .Width = ScreenX + 32
        End With

        UpdateDrawMapName()

    End Sub

    Sub ClearGfx()

        'clear tilesets
        For I = 1 To NumTileSets
            If TileSetTextureInfo(I).IsLoaded Then
                If TileSetTextureInfo(I).TextureTimer < GetTickCount() Then
                    TileSetTexture(I).Dispose()
                    TileSetSprite(I).Dispose()
                    TileSetTextureInfo(I).IsLoaded = False
                    TileSetTextureInfo(I).TextureTimer = 0
                End If
            End If
        Next

        'clear characters
        For I = 1 To NumCharacters
            If CharacterGfxInfo(I).IsLoaded Then
                If CharacterGfxInfo(I).TextureTimer < GetTickCount() Then
                    CharacterGfx(I).Dispose()
                    CharacterSprite(I).Dispose()
                    CharacterGfxInfo(I).IsLoaded = False
                    CharacterGfxInfo(I).TextureTimer = 0
                End If
            End If
        Next

        'clear paperdoll
        For I = 1 To NumPaperdolls
            If PaperDollGfxInfo(I).IsLoaded Then
                If PaperDollGfxInfo(I).TextureTimer < GetTickCount() Then
                    PaperDollGfx(I).Dispose()
                    PaperDollSprite(I).Dispose()
                    PaperDollGfxInfo(I).IsLoaded = False
                    PaperDollGfxInfo(I).TextureTimer = 0
                End If
            End If
        Next

        'clear items
        For I = 1 To NumItems
            If ItemsGfxInfo(I).IsLoaded Then
                If ItemsGfxInfo(I).TextureTimer < GetTickCount() Then
                    ItemsGfx(I).Dispose()
                    ItemsSprite(I).Dispose()
                    ItemsGfxInfo(I).IsLoaded = False
                    ItemsGfxInfo(I).TextureTimer = 0
                End If
            End If
        Next

        'clear resources
        For I = 1 To NumResources
            If ResourcesGfxInfo(I).IsLoaded Then
                If ResourcesGfxInfo(I).TextureTimer < GetTickCount() Then
                    ResourcesGfx(I).Dispose()
                    ResourcesSprite(I).Dispose()
                    ResourcesGfxInfo(I).IsLoaded = False
                    ResourcesGfxInfo(I).TextureTimer = 0
                End If
            End If
        Next

        'animations
        For I = 1 To NumAnimations
            If AnimationsGfxInfo(I).IsLoaded Then
                If AnimationsGfxInfo(I).TextureTimer < GetTickCount() Then
                    AnimationsGfx(I).Dispose()
                    AnimationsGfxInfo(I).IsLoaded = False
                    AnimationsGfxInfo(I).TextureTimer = 0
                End If
            End If
        Next

        'clear faces
        For I = 1 To NumFaces
            If FacesGfxInfo(I).IsLoaded Then
                If FacesGfxInfo(I).TextureTimer < GetTickCount() Then
                    FacesGfx(I).Dispose()
                    FacesSprite(I).Dispose()
                    FacesGfxInfo(I).IsLoaded = False
                    FacesGfxInfo(I).TextureTimer = 0
                End If
            End If
        Next

        'clear fogs
        For I = 1 To NumFogs
            If FogGfxInfo(I).IsLoaded Then
                If FogGfxInfo(I).TextureTimer < GetTickCount() Then
                    FogGfx(I).Dispose()
                    FogGfxInfo(I).IsLoaded = False
                    FogGfxInfo(I).TextureTimer = 0
                End If
            End If
        Next

        'clear SkillIcons
        For I = 1 To NumSkillIcons
            If SkillIconsGfxInfo(I).IsLoaded Then
                If SkillIconsGfxInfo(I).TextureTimer < GetTickCount() Then
                    SkillIconsGfx(I).Dispose()
                    SkillIconsSprite(I).Dispose()
                    SkillIconsGfxInfo(I).IsLoaded = False
                    SkillIconsGfxInfo(I).TextureTimer = 0
                End If
            End If
        Next

        'clear Furniture
        For I = 1 To NumFurniture
            If FurnitureGfxInfo(I).IsLoaded Then
                If FurnitureGfxInfo(I).TextureTimer < GetTickCount() Then
                    FurnitureGfx(I).Dispose()
                    FurnitureSprite(I).Dispose()
                    FurnitureGfxInfo(I).IsLoaded = False
                    FurnitureGfxInfo(I).TextureTimer = 0
                End If
            End If
        Next

        'clear Projectiles
        For I = 1 To NumProjectiles
            If ProjectileGfxInfo(I).IsLoaded Then
                If ProjectileGfxInfo(I).TextureTimer < GetTickCount() Then
                    ProjectileGfx(I).Dispose()
                    ProjectileSprite(I).Dispose()
                    ProjectileGfxInfo(I).IsLoaded = False
                    ProjectileGfxInfo(I).TextureTimer = 0
                End If
            End If
        Next

        'clear Emotes
        For I = 1 To NumEmotes
            If EmotesGfxInfo(I).IsLoaded Then
                If EmotesGfxInfo(I).TextureTimer < GetTickCount() Then
                    EmotesGfx(I).Dispose()
                    EmotesSprite(I).Dispose()
                    EmotesGfxInfo(I).IsLoaded = False
                    EmotesGfxInfo(I).TextureTimer = 0
                End If
            End If
        Next

        'clear Panoramas
        For I = 1 To NumPanorama
            If PanoramasGfxInfo(I).IsLoaded Then
                If PanoramasGfxInfo(I).TextureTimer < GetTickCount() Then
                    PanoramasGfx(I).Dispose()
                    PanoramasSprite(I).Dispose()
                    PanoramasGfxInfo(I).IsLoaded = False
                    PanoramasGfxInfo(I).TextureTimer = 0
                End If
            End If
        Next

        'clear Parallax
        For I = 1 To NumParallax
            If ParallaxGfxInfo(I).IsLoaded Then
                If ParallaxGfxInfo(I).TextureTimer < GetTickCount() Then
                    ParallaxGfx(I).Dispose()
                    ParallaxSprite(I).Dispose()
                    ParallaxGfxInfo(I).IsLoaded = False
                    ParallaxGfxInfo(I).TextureTimer = 0
                End If
            End If
        Next
    End Sub

    Friend Sub Render_Graphics()
        Dim x As Integer, y As Integer, I As Integer

        'Don't Render IF
        If FrmGame.WindowState = FormWindowState.Minimized Then Exit Sub
        If GettingMap Then Exit Sub

        'lets get going

        'update view around player
        UpdateCamera()

        'let program do other things
        Application.DoEvents()

        'Clear each of our render targets
        GameWindow.DispatchEvents()
        GameWindow.Clear(SFML.Graphics.Color.Black)

        'If CurMouseX > 0 AndAlso CurMouseX <= GameWindow.Size.X Then
        '    If CurMouseY > 0 AndAlso CurMouseY <= GameWindow.Size.Y Then
        '        GameWindow.SetMouseCursorVisible(False)
        '    End If
        'End If

        If NumPanorama > 0 AndAlso Map.Panorama > 0 Then
            DrawPanorama(Map.Panorama)
        End If

        If NumParallax > 0 AndAlso Map.Parallax > 0 Then
            DrawParallax(Map.Parallax)
        End If

        ' blit lower tiles
        If NumTileSets > 0 Then

            For x = TileView.Left To TileView.Right + 1
                For y = TileView.Top To TileView.Bottom + 1
                    If IsValidMapPoint(x, y) Then
                        DrawMapTile(x, y)
                    End If
                Next
            Next
        End If

        ' Furniture
        If FurnitureHouse > 0 Then
            If FurnitureHouse = Player(Myindex).InHouse Then
                If FurnitureCount > 0 Then
                    For I = 1 To FurnitureCount
                        If Furniture(I).ItemNum > 0 Then
                            DrawFurniture(I, 0)
                        End If
                    Next
                End If
            End If
        End If

        ' events
        If Map.CurrentEvents > 0 AndAlso Map.CurrentEvents <= Map.EventCount Then

            For I = 1 To Map.CurrentEvents
                If Map.MapEvents(I).Position = 0 Then
                    DrawEvent(I)
                End If
            Next
        End If

        'blood
        For I = 1 To Byte.MaxValue
            DrawBlood(I)
        Next

        ' Draw out the items
        If NumItems > 0 Then
            For I = 1 To MAX_MAP_ITEMS
                If MapItem(I).Num > 0 Then
                    DrawItem(I)
                End If
            Next
        End If

        'Draw sum d00rs.
        If GettingMap Then Exit Sub

        For x = TileView.Left To TileView.Right
            For y = TileView.Top To TileView.Bottom
                If IsValidMapPoint(x, y) Then
                    If Map.Tile Is Nothing Then Exit Sub
                    If Map.Tile(x, y).Type = TileType.Door Then
                        DrawDoor(x, y)
                    End If
                End If
            Next
        Next

        ' draw animations
        If NumAnimations > 0 Then
            For I = 1 To Byte.MaxValue
                If AnimInstance(I).Used(0) Then
                    DrawAnimation(I, 0)
                End If
            Next
        End If

        ' Y-based render. Renders Players, Npcs and Resources based on Y-axis.
        For y = 0 To Map.MaxY

            If NumCharacters > 0 Then
                ' Players
                For I = 1 To TotalOnline 'MAX_PLAYERS
                    If IsPlaying(I) AndAlso GetPlayerMap(I) = GetPlayerMap(Myindex) Then
                        If Player(I).Y = y Then
                            DrawPlayer(I)
                        End If
                        If PetAlive(I) Then
                            If Player(I).Pet.Y = y Then
                                DrawPet(I)
                            End If
                        End If
                    End If
                Next

                ' Npcs
                For I = 1 To MAX_MAP_NPCS
                    If MapNpc(I).Y = y Then
                        DrawNpc(I)
                    End If
                Next

                ' events
                If Map.CurrentEvents > 0 AndAlso Map.CurrentEvents <= Map.EventCount Then
                    For I = 1 To Map.CurrentEvents
                        If Map.MapEvents(I).Position = 1 Then
                            If y = Map.MapEvents(I).Y Then
                                DrawEvent(I)
                            End If
                        End If
                    Next
                End If

                ' Draw the target icon
                If MyTarget > 0 Then
                    If MyTargetType = TargetType.Player Then
                        DrawTarget(Player(MyTarget).X * 32 - 16 + Player(MyTarget).XOffset, Player(MyTarget).Y * 32 + Player(MyTarget).YOffset)
                    ElseIf MyTargetType = TargetType.Npc Then
                        DrawTarget(MapNpc(MyTarget).X * 32 - 16 + MapNpc(MyTarget).XOffset, MapNpc(MyTarget).Y * 32 + MapNpc(MyTarget).YOffset)
                    ElseIf MyTargetType = TargetType.Pet Then
                        DrawTarget(Player(MyTarget).Pet.X * 32 - 16 + Player(MyTarget).Pet.XOffset, (Player(MyTarget).Pet.Y * 32) + Player(MyTarget).Pet.YOffset)
                    End If
                End If

                For I = 1 To TotalOnline 'MAX_PLAYERS
                    If IsPlaying(I) Then
                        If Player(I).Map = Player(Myindex).Map Then
                            If CurX = Player(I).X AndAlso CurY = Player(I).Y Then
                                If MyTargetType = TargetType.Player AndAlso MyTarget = I Then
                                    ' dont render lol
                                Else
                                    DrawHover(Player(I).X * 32 - 16, Player(I).Y * 32 + Player(I).YOffset)
                                End If
                            End If

                        End If
                    End If
                Next
            End If

            ' Resources
            If NumResources > 0 Then
                If ResourcesInit Then
                    If ResourceIndex > 0 Then
                        For I = 1 To ResourceIndex
                            If MapResource(I).Y = y Then
                                DrawMapResource(I)
                            End If
                        Next
                    End If
                End If
            End If
        Next

        ' animations
        If NumAnimations > 0 Then
            For I = 1 To Byte.MaxValue
                If AnimInstance(I - 1).Used(1) Then
                    DrawAnimation(I - 1, 1)
                End If
            Next
        End If

        'projectiles
        If NumProjectiles > 0 Then
            For I = 1 To MaxProjectiles
                If MapProjectiles(I).ProjectileNum > 0 Then
                    DrawProjectile(I)
                End If
            Next
        End If

        'events
        If Map.CurrentEvents > 0 AndAlso Map.CurrentEvents <= Map.EventCount Then
            For I = 1 To Map.CurrentEvents
                If Map.MapEvents(I).Position = 2 Then
                    DrawEvent(I)
                End If
            Next
        End If

        ' blit out upper tiles
        If NumTileSets > 0 Then
            For x = TileView.Left To TileView.Right + 1
                For y = TileView.Top To TileView.Bottom + 1
                    If IsValidMapPoint(x, y) Then
                        DrawMapFringeTile(x, y)
                    End If
                Next
            Next
        End If

        ' Furniture
        If FurnitureHouse > 0 Then
            If FurnitureHouse = Player(Myindex).InHouse Then
                If FurnitureCount > 0 Then
                    For I = 1 To FurnitureCount
                        If Furniture(I).ItemNum > 0 Then
                            DrawFurniture(I, 1)
                        End If
                    Next
                End If
            End If
        End If

        DrawNight()

        DrawWeather()
        DrawThunderEffect()
        DrawMapTint()

        ' Draw out a square at mouse cursor
        If MapGrid = True AndAlso InMapEditor Then
            DrawGrid()
        End If

        If FrmEditor_MapEditor.tabpages.SelectedTab Is FrmEditor_MapEditor.tpDirBlock Then
            For x = TileView.Left To TileView.Right
                For y = TileView.Top To TileView.Bottom
                    If IsValidMapPoint(x, y) Then
                        DrawDirections(x, y)
                    End If
                Next
            Next
        End If

        If InMapEditor Then FrmEditor_MapEditor.DrawTileOutline()

        'furniture
        If FurnitureSelected > 0 Then
            If Player(Myindex).InHouse = Myindex Then
                DrawFurnitureOutline()
            End If
        End If

        ' draw cursor, player X and Y locations
        If BLoc Then
            DrawText(1, HudWindowY + HudPanelGfxInfo.Height + 1, Trim$(String.Format(Language.Game.MapCurLoc, CurX, CurY)), SFML.Graphics.Color.Yellow, SFML.Graphics.Color.Black, GameWindow)
            DrawText(1, HudWindowY + HudPanelGfxInfo.Height + 15, Trim$(String.Format(Language.Game.MapLoc, GetPlayerX(Myindex), GetPlayerY(Myindex))), SFML.Graphics.Color.Yellow, SFML.Graphics.Color.Black, GameWindow)
            DrawText(1, HudWindowY + HudPanelGfxInfo.Height + 30, Trim$(String.Format(Language.Game.MapCurMap, GetPlayerMap(Myindex))), SFML.Graphics.Color.Yellow, SFML.Graphics.Color.Black, GameWindow)
        End If

        ' draw player names
        For I = 1 To TotalOnline 'MAX_PLAYERS
            If IsPlaying(I) AndAlso GetPlayerMap(I) = GetPlayerMap(Myindex) Then
                DrawPlayerName(I)
                If PetAlive(I) Then
                    DrawPlayerPetName(I)
                End If
            End If
        Next

        'draw event names
        For I = 1 To Map.CurrentEvents
            If Map.MapEvents(I).Visible = 1 Then
                If Map.MapEvents(I).ShowName = 1 Then
                    DrawEventName(I)
                End If
            End If
        Next

        ' draw npc names
        For I = 1 To MAX_MAP_NPCS
            If MapNpc(I).Num > 0 Then
                DrawNpcName(I)
            End If
        Next

        If CurrentFog > 0 Then
            DrawFog()
        End If

        ' draw the messages
        For I = 1 To Byte.MaxValue
            If ChatBubble(I).Active Then
                DrawChatBubble(I)
            End If
        Next

        'action msg
        For I = 1 To Byte.MaxValue
            DrawActionMsg(I)
        Next

        ' Blit out map attributes
        If InMapEditor Then
            DrawMapAttributes()
        End If

        If InMapEditor AndAlso FrmEditor_MapEditor.tabpages.SelectedTab Is FrmEditor_MapEditor.tpEvents Then
            DrawEvents()
            EditorEvent_DrawGraphic()
        End If

        If GettingMap Then Exit Sub

        'draw hp and casting bars
        DrawBars()

        'party
        DrawParty()

        'Render GUI
        DrawGui()

        DrawMapFade()

        'and finally show everything on screen
        GameWindow.Display()
    End Sub

    Friend Sub DrawPanorama(index As Integer)
        If Map.Moral = MapMoralType.Indoors Then Exit Sub

        If index < 1 OrElse index > NumParallax Then Exit Sub

        If PanoramasGfxInfo(index).IsLoaded = False Then
            LoadTexture(index, 13)
        End If

        ' we use it, lets update timer
        With PanoramasGfxInfo(index)
            .TextureTimer = GetTickCount() + 100000
        End With

        PanoramasSprite(index).TextureRect = New IntRect(0, 0, GameWindow.Size.X, GameWindow.Size.Y)

        PanoramasSprite(index).Position = New Vector2f(0, 0)

        GameWindow.Draw(PanoramasSprite(index))

    End Sub

    Friend Sub DrawParallax(index As Integer)
        Dim horz As Integer = 0
        Dim vert As Integer = 0

        If Map.Moral = MapMoralType.Indoors Then Exit Sub

        If index < 1 OrElse index > NumParallax Then Exit Sub

        If ParallaxGfxInfo(index).IsLoaded = False Then
            LoadTexture(index, 14)
        End If

        ' we use it, lets update timer
        With ParallaxGfxInfo(index)
            .TextureTimer = GetTickCount() + 100000
        End With

        horz = ConvertMapX(GetPlayerX(Myindex))
        vert = ConvertMapY(GetPlayerY(Myindex))

        ParallaxSprite(index).Position = New Vector2f((horz * 2.5) - 50, (vert * 2.5) - 50)

        GameWindow.Draw(ParallaxSprite(index))
    End Sub

    Friend Sub DrawBars()
        Dim tmpY As Integer
        Dim tmpX As Integer
        Dim barWidth As Integer
        Dim rec(1) As Rectangle

        If GettingMap Then Exit Sub

        ' check for casting time bar
        If SkillBuffer > 0 Then
            ' lock to player
            tmpX = GetPlayerX(Myindex) * PicX + Player(Myindex).XOffset
            tmpY = GetPlayerY(Myindex) * PicY + Player(Myindex).YOffset + 35
            If Skill(PlayerSkills(SkillBuffer)).CastTime = 0 Then Skill(PlayerSkills(SkillBuffer)).CastTime = 1
            ' calculate the width to fill
            barWidth = ((GetTickCount() - SkillBufferTimer) / ((GetTickCount() - SkillBufferTimer) + (Skill(PlayerSkills(SkillBuffer)).CastTime * 1000)) * 64)
            ' draw bars
            rec(1) = New Rectangle(ConvertMapX(tmpX), ConvertMapY(tmpY), barWidth, 4)
            Dim rectShape As New RectangleShape(New Vector2f(barWidth, 4)) With {
                .Position = New Vector2f(ConvertMapX(tmpX), ConvertMapY(tmpY)),
                .FillColor = SFML.Graphics.Color.Cyan
            }
            GameWindow.Draw(rectShape)
        End If

        If Settings.ShowNpcBar = 1 Then
            ' check for hp bar
            For i = 1 To MAX_MAP_NPCS
                If Map.Npc Is Nothing Then Exit Sub
                If Map.Npc(i) > 0 Then
                    If Npc(MapNpc(i).Num).Behaviour = NpcBehavior.AttackOnSight OrElse Npc(MapNpc(i).Num).Behaviour = NpcBehavior.AttackWhenAttacked OrElse Npc(MapNpc(i).Num).Behaviour = NpcBehavior.Guard Then
                        ' lock to npc
                        tmpX = MapNpc(i).X * PicX + MapNpc(i).XOffset
                        tmpY = MapNpc(i).Y * PicY + MapNpc(i).YOffset + 35
                        If MapNpc(i).Vital(VitalType.HP) > 0 Then
                            ' calculate the width to fill
                            barWidth = ((MapNpc(i).Vital(VitalType.HP) / (Npc(MapNpc(i).Num).Hp) * 32))
                            ' draw bars
                            rec(1) = New Rectangle(ConvertMapX(tmpX), ConvertMapY(tmpY), barWidth, 4)
                            Dim rectShape As New RectangleShape(New Vector2f(barWidth, 4)) With {
                                .Position = New Vector2f(ConvertMapX(tmpX), ConvertMapY(tmpY - 75)),
                                .FillColor = SFML.Graphics.Color.Red
                            }
                            GameWindow.Draw(rectShape)

                            If MapNpc(i).Vital(VitalType.MP) > 0 Then
                                ' calculate the width to fill
                                barWidth = ((MapNpc(i).Vital(VitalType.MP) / (Npc(MapNpc(i).Num).Stat(StatType.Intelligence) * 2) * 32))
                                ' draw bars
                                rec(1) = New Rectangle(ConvertMapX(tmpX), ConvertMapY(tmpY), barWidth, 4)
                                Dim rectShape2 As New RectangleShape(New Vector2f(barWidth, 4)) With {
                                    .Position = New Vector2f(ConvertMapX(tmpX), ConvertMapY(tmpY - 80)),
                                    .FillColor = SFML.Graphics.Color.Blue
                                }
                                GameWindow.Draw(rectShape2)
                            End If
                        End If
                    End If
                End If
            Next
        End If

        If PetAlive(Myindex) Then
            ' draw own health bar
            If Player(Myindex).Pet.Health > 0 AndAlso Player(Myindex).Pet.Health <= Player(Myindex).Pet.MaxHp Then
                'Debug.Print("pethealth:" & Player(Myindex).Pet.Health)
                ' lock to Player
                tmpX = Player(Myindex).Pet.X * PicX + Player(Myindex).Pet.XOffset
                tmpY = Player(Myindex).Pet.Y * PicX + Player(Myindex).Pet.YOffset + 35
                ' calculate the width to fill
                barWidth = ((Player(Myindex).Pet.Health) / (Player(Myindex).Pet.MaxHp)) * 32
                ' draw bars
                rec(1) = New Rectangle(ConvertMapX(tmpX), ConvertMapY(tmpY), barWidth, 4)
                Dim rectShape As New RectangleShape(New Vector2f(barWidth, 4)) With {
                    .Position = New Vector2f(ConvertMapX(tmpX), ConvertMapY(tmpY - 75)),
                    .FillColor = SFML.Graphics.Color.Red
                }
                GameWindow.Draw(rectShape)
            End If
        End If
        ' check for pet casting time bar
        If PetSkillBuffer > 0 Then
            If Skill(Pet(Player(Myindex).Pet.Num).Skill(PetSkillBuffer)).CastTime > 0 Then
                ' lock to pet
                tmpX = Player(Myindex).Pet.X * PicX + Player(Myindex).Pet.XOffset
                tmpY = Player(Myindex).Pet.Y * PicY + Player(Myindex).Pet.YOffset + 35

                ' calculate the width to fill
                barWidth = (GetTickCount() - PetSkillBufferTimer) / ((Skill(Pet(Player(Myindex).Pet.Num).Skill(PetSkillBuffer)).CastTime * 1000)) * 64
                ' draw bar background
                rec(1) = New Rectangle(ConvertMapX(tmpX), ConvertMapY(tmpY), barWidth, 4)
                Dim rectShape As New RectangleShape(New Vector2f(barWidth, 4)) With {
                    .Position = New Vector2f(ConvertMapX(tmpX), ConvertMapY(tmpY)),
                    .FillColor = SFML.Graphics.Color.Cyan
                }
                GameWindow.Draw(rectShape)
            End If
        End If
    End Sub

    Sub DrawMapName()
        DrawText(DrawMapNameX, DrawMapNameY, Language.Game.MapName & Map.Name, DrawMapNameColor, SFML.Graphics.Color.Black, GameWindow)
    End Sub

    Friend Sub DrawDoor(x As Integer, y As Integer)
        Dim rec As Rectangle

        Dim x2 As Integer, y2 As Integer

        ' sort out animation
        With TempTile(x, y)
            If .DoorAnimate = 1 Then ' opening
                If .DoorTimer + 100 < GetTickCount() Then
                    If .DoorFrame < 4 Then
                        .DoorFrame = .DoorFrame + 1
                    Else
                        .DoorAnimate = 2 ' set to closing
                    End If
                    .DoorTimer = GetTickCount()
                End If
            ElseIf .DoorAnimate = 2 Then ' closing
                If .DoorTimer + 100 < GetTickCount() Then
                    If .DoorFrame > 1 Then
                        .DoorFrame = .DoorFrame - 1
                    Else
                        .DoorAnimate = 0 ' end animation
                    End If
                    .DoorTimer = GetTickCount()
                End If
            End If

            If .DoorFrame = 0 Then .DoorFrame = 1
        End With

        With rec
            .Y = 0
            .Height = DoorGfxInfo.Height
            .X = ((TempTile(x, y).DoorFrame - 1) * DoorGfxInfo.Width / 4)
            .Width = DoorGfxInfo.Width / 4
        End With

        x2 = (x * PicX)
        y2 = (y * PicY) - (DoorGfxInfo.Height / 2) + 4

        RenderSprite(DoorSprite, GameWindow, ConvertMapX(x * PicX), ConvertMapY((y * PicY) - PicY), rec.X, rec.Y, rec.Width, rec.Height)

    End Sub



    Friend Sub DrawFurnitureOutline()
        Dim rec As Rectangle

        With rec
            .Y = 0
            .Height = Item(GetPlayerInvItemNum(Myindex, FurnitureSelected)).FurnitureHeight * PicY
            .X = 0
            .Width = Item(GetPlayerInvItemNum(Myindex, FurnitureSelected)).FurnitureWidth * PicX
        End With

        Dim rec2 As New RectangleShape With {
            .OutlineColor = New SFML.Graphics.Color(SFML.Graphics.Color.Blue),
            .OutlineThickness = 0.6,
            .FillColor = New SFML.Graphics.Color(SFML.Graphics.Color.Transparent),
            .Size = New Vector2f(rec.Width, rec.Height),
            .Position = New Vector2f(ConvertMapX(CurX * PicX), ConvertMapY(CurY * PicY))
        }
        GameWindow.Draw(rec2)
    End Sub

    Friend Sub DrawGrid()
        For x = TileView.Left To TileView.Right ' - 1
            For y = TileView.Top To TileView.Bottom ' - 1
                If IsValidMapPoint(x, y) Then

                    Dim rec As New RectangleShape With {
                        .OutlineColor = New SFML.Graphics.Color(SFML.Graphics.Color.White),
                        .OutlineThickness = 0.6,
                        .FillColor = New SFML.Graphics.Color(SFML.Graphics.Color.Transparent),
                        .Size = New Vector2f((x * PicX), (y * PicX)),
                        .Position = New Vector2f(ConvertMapX((x - 1) * PicX), ConvertMapY((y - 1) * PicY))
                    }

                    GameWindow.Draw(rec)
                End If
            Next
        Next

    End Sub

    Friend Sub DrawMapTint()

        If Map.HasMapTint = 0 Then Exit Sub

        MapTintGfx.Clear(New SFML.Graphics.Color(CurrentTintR, CurrentTintG, CurrentTintB, CurrentTintA))

        'MapTintSprite.Color = New SFML.Graphics.Color(CurrentTintR, CurrentTintG, CurrentTintB, CurrentTintA)
        MapTintSprite = New Sprite(MapTintGfx.Texture) With {
            .TextureRect = New IntRect(0, 0, GameWindow.Size.X, GameWindow.Size.Y),
            .Position = New Vector2f(0, 0)
        }

        MapTintGfx.Display()

        GameWindow.Draw(MapTintSprite)

    End Sub

    Friend Sub DrawMapFade()
        If UseFade = False Then Exit Sub

        MapFadeSprite = New Sprite(New Texture(New SFML.Graphics.Image(GameWindow.Size.X, GameWindow.Size.Y, SFML.Graphics.Color.Black))) With {
            .Color = New SFML.Graphics.Color(0, 0, 0, FadeAmount),
            .TextureRect = New IntRect(0, 0, GameWindow.Size.X, GameWindow.Size.Y),
            .Position = New Vector2f(0, 0)
        }

        GameWindow.Draw(MapFadeSprite)

    End Sub

    Sub DestroyGraphics()
        Try
            For i = 0 To NumAnimations
                If Not AnimationsGfx(i) Is Nothing Then AnimationsGfx(i).Dispose()
            Next i

            For i = 0 To NumCharacters
                If Not CharacterGfx(i) Is Nothing Then CharacterGfx(i).Dispose()
            Next

            For i = 0 To NumItems
                If Not ItemsGfx(i) Is Nothing Then ItemsGfx(i).Dispose()
            Next

            For i = 0 To NumPaperdolls
                If Not PaperDollGfx(i) Is Nothing Then PaperDollGfx(i).Dispose()
            Next

            For i = 0 To NumResources
                If Not ResourcesGfx(i) Is Nothing Then ResourcesGfx(i).Dispose()
            Next

            For i = 0 To NumSkillIcons
                If Not SkillIconsGfx(i) Is Nothing Then SkillIconsGfx(i).Dispose()
            Next

            For i = 0 To NumTileSets
                If Not TileSetTexture(i) Is Nothing Then TileSetTexture(i).Dispose()
            Next i

            For i = 0 To NumFurniture
                If Not FurnitureGfx(i) Is Nothing Then FurnitureGfx(i).Dispose()
            Next

            For i = 0 To NumFaces
                If Not FacesGfx(i) Is Nothing Then FacesGfx(i).Dispose()
            Next

            For i = 0 To NumFogs
                If Not FogGfx(i) Is Nothing Then FogGfx(i).Dispose()
            Next

            For i = 0 To NumProjectiles
                If Not ProjectileGfx(i) Is Nothing Then ProjectileGfx(i).Dispose()
            Next

            For i = 0 To NumEmotes
                If Not EmotesGfx(i) Is Nothing Then EmotesGfx(i).Dispose()
            Next

            For i = 0 To NumPanorama
                If Not PanoramasGfx(i) Is Nothing Then PanoramasGfx(i).Dispose()
            Next

            For i = 0 To NumParallax
                If Not ParallaxGfx(i) Is Nothing Then ParallaxGfx(i).Dispose()
            Next

            If Not CursorGfx Is Nothing Then CursorGfx.Dispose()
            If Not DoorGfx Is Nothing Then DoorGfx.Dispose()
            If Not BloodGfx Is Nothing Then BloodGfx.Dispose()
            If Not DirectionsGfx Is Nothing Then DirectionsGfx.Dispose()
            If Not ActionPanelGfx Is Nothing Then ActionPanelGfx.Dispose()
            If Not InvPanelGfx Is Nothing Then InvPanelGfx.Dispose()
            If Not CharPanelGfx Is Nothing Then CharPanelGfx.Dispose()
            If Not CharPanelPlusGfx Is Nothing Then CharPanelPlusGfx.Dispose()
            If Not CharPanelMinGfx Is Nothing Then CharPanelMinGfx.Dispose()
            If Not TargetGfx Is Nothing Then TargetGfx.Dispose()
            If Not WeatherGfx Is Nothing Then WeatherGfx.Dispose()
            If Not HotBarGfx Is Nothing Then HotBarGfx.Dispose()
            If Not ChatWindowGfx Is Nothing Then ChatWindowGfx.Dispose()
            If Not BankPanelGfx Is Nothing Then BankPanelGfx.Dispose()
            If Not ShopPanelGfx Is Nothing Then ShopPanelGfx.Dispose()
            If Not TradePanelGfx Is Nothing Then TradePanelGfx.Dispose()
            If Not EventChatGfx Is Nothing Then EventChatGfx.Dispose()
            If Not RClickGfx Is Nothing Then RClickGfx.Dispose()
            If Not ButtonGfx Is Nothing Then ButtonGfx.Dispose()
            If Not ButtonHoverGfx Is Nothing Then ButtonHoverGfx.Dispose()
            If Not QuestGfx Is Nothing Then QuestGfx.Dispose()
            If Not CraftGfx Is Nothing Then CraftGfx.Dispose()
            If Not ProgBarGfx Is Nothing Then ProgBarGfx.Dispose()
            If Not ChatBubbleGfx Is Nothing Then ChatBubbleGfx.Dispose()

            If Not HpBarGfx Is Nothing Then HpBarGfx.Dispose()
            If Not MpBarGfx Is Nothing Then MpBarGfx.Dispose()
            If Not ExpBarGfx Is Nothing Then ExpBarGfx.Dispose()

            If Not LightGfx Is Nothing Then LightGfx.Dispose()
            If Not NightGfx Is Nothing Then NightGfx.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Sub DrawHud()
        Dim rec As Rectangle

        'first render backpanel
        With rec
            .Y = 0
            .Height = HudPanelGfxInfo.Height
            .X = 0
            .Width = HudPanelGfxInfo.Width
        End With

        RenderSprite(HudPanelSprite, GameWindow, HudWindowX, HudWindowY, rec.X, rec.Y, rec.Width, rec.Height)

        If Player(Myindex).Sprite <= NumFaces Then
            Dim tmpSprite As Sprite = New Sprite(FacesGfx(Player(Myindex).Sprite))

            If FacesGfxInfo(Player(Myindex).Sprite).IsLoaded = False Then
                LoadTexture(Player(Myindex).Sprite, 7)
            End If

            'seeying we still use it, lets update timer
            With FacesGfxInfo(Player(Myindex).Sprite)
                .TextureTimer = GetTickCount() + 100000
            End With

            'then render face
            With rec
                .Y = 0
                .Height = FacesGfxInfo(Player(Myindex).Sprite).Height
                .X = 0
                .Width = FacesGfxInfo(Player(Myindex).Sprite).Width
            End With

            RenderSprite(FacesSprite(Player(Myindex).Sprite), GameWindow, HudFaceX, HudFaceY, rec.X, rec.Y, rec.Width, rec.Height)
        End If

        'Hp Bar etc
        DrawStatBars()

        DrawText(HudWindowX + HudhpBarX + HpBarGfxInfo.Width + 10, HudWindowY + HudhpBarY + 4, Language.Game.Fps & Fps, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        DrawText(HudWindowX + HudmpBarX + MpBarGfxInfo.Width + 10, HudWindowY + HudmpBarY + 4, Language.Game.Ping & PingToDraw, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        DrawText(HudWindowX + HudexpBarX + ExpBarGfxInfo.Width + 10, HudWindowY + HudexpBarY + 4, Language.Game.Time & Time.Instance.ToString("h:mm"), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)

        If Blps Then
            DrawText(HudWindowX + HudexpBarX + ExpBarGfxInfo.Width + 10, HudWindowY + HudexpBarY + 20, Language.Game.Lps & Lps, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        End If

        ' Draw map name
        DrawMapName()
    End Sub

    Sub DrawStatBars()
        Dim rec As Rectangle
        Dim curHp As Integer, curMp As Integer, curExp As Integer

        'HP Bar
        curHp = (GetPlayerVital(Myindex, 1) / GetPlayerMaxVital(Myindex, 1)) * 100

        With rec
            .Y = 0
            .Height = HpBarGfxInfo.Height
            .X = 0
            .Width = curHp * HpBarGfxInfo.Width / 100
        End With

        'then render full ontop of it
        RenderSprite(HpBarSprite, GameWindow, HudWindowX + HudhpBarX, HudWindowY + HudhpBarY + 4, rec.X, rec.Y, rec.Width, rec.Height)

        'then draw the text onto that
        DrawText(HudWindowX + HudhpBarX + 65, HudWindowY + HudhpBarY + 4, GetPlayerVital(Myindex, 1) & "/" & GetPlayerMaxVital(Myindex, 1), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)

        '==============================

        'MP Bar
        curMp = (GetPlayerVital(Myindex, 2) / GetPlayerMaxVital(Myindex, 2)) * 100

        'then render full ontop of it
        With rec
            .Y = 0
            .Height = MpBarGfxInfo.Height
            .X = 0
            .Width = curMp * MpBarGfxInfo.Width / 100
        End With

        RenderSprite(MpBarSprite, GameWindow, HudWindowX + HudmpBarX, HudWindowY + HudmpBarY + 4, rec.X, rec.Y, rec.Width, rec.Height)

        'draw text onto that
        DrawText(HudWindowX + HudmpBarX + 65, HudWindowY + HudmpBarY + 4, GetPlayerVital(Myindex, 2) & "/" & GetPlayerMaxVital(Myindex, 2), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)

        '====================================================
        'EXP Bar
        curExp = (GetPlayerExp(Myindex) / NextlevelExp) * 100

        'then render full ontop of it
        With rec
            .Y = 0
            .Height = ExpBarGfxInfo.Height
            .X = 0
            .Width = curExp * ExpBarGfxInfo.Width / 100
        End With

        RenderSprite(ExpBarSprite, GameWindow, HudWindowX + HudexpBarX, HudWindowY + HudexpBarY + 4, rec.X, rec.Y, rec.Width, rec.Height)

        'draw text onto that
        DrawText(HudWindowX + HudexpBarX + 65, HudWindowY + HudexpBarY + 4, GetPlayerExp(Myindex) & "/" & NextlevelExp, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
    End Sub

    Sub DrawActionPanel()
        Dim rec As Rectangle

        'first render backpanel
        With rec
            .Y = 0
            .Height = ActionPanelGfxInfo.Height
            .X = 0
            .Width = ActionPanelGfxInfo.Width
        End With

        RenderSprite(ActionPanelSprite, GameWindow, ActionPanelX, ActionPanelY, rec.X, rec.Y, rec.Width, rec.Height)

    End Sub



    Friend Sub DrawInventoryItem(x As Integer, y As Integer)
        Dim rec As Rectangle
        Dim itemnum As Integer, itempic As Integer

        itemnum = GetPlayerInvItemNum(Myindex, DragInvSlotNum)

        If itemnum > 0 AndAlso itemnum <= MAX_ITEMS Then

            itempic = Item(itemnum).Pic
            If itempic = 0 Then Exit Sub

            If ItemsGfxInfo(itempic).IsLoaded = False Then
                LoadTexture(itempic, 4)
            End If

            'seeying we still use it, lets update timer
            With ItemsGfxInfo(itempic)
                .TextureTimer = GetTickCount() + 100000
            End With

            With rec
                .Y = 0
                .Height = PicY
                .X = 0
                .Width = PicX
            End With

            RenderSprite(ItemsSprite(itempic), GameWindow, x + 16, y + 16, rec.X, rec.Y, rec.Width, rec.Height)
        End If
    End Sub

    Sub DrawInventory()
        Dim i As Integer, x As Integer, y As Integer, itemnum As Integer, itempic As Integer
        Dim amount As String
        Dim rec As Rectangle, recPos As Rectangle
        Dim colour As SFML.Graphics.Color

        If Not InGame Then Exit Sub

        'first render panel
        RenderSprite(InvPanelSprite, GameWindow, InvWindowX, InvWindowY, 0, 0, InvPanelGfxInfo.Width, InvPanelGfxInfo.Height)

        For i = 1 To MAX_INV
            itemnum = GetPlayerInvItemNum(Myindex, i)

            If itemnum > 0 AndAlso itemnum <= MAX_ITEMS Then
                itempic = Item(itemnum).Pic
                If itempic = 0 Then GoTo NextLoop

                If ItemsGfxInfo(itempic).IsLoaded = False Then
                    LoadTexture(itempic, 4)
                End If

                'seeying we still use it, lets update timer
                With ItemsGfxInfo(itempic)
                    .TextureTimer = GetTickCount() + 100000
                End With

                ' exit out if we're offering item in a trade.
                If InTrade > 0 Then
                    For x = 1 To MAX_INV
                        If TradeYourOffer(x).Num = i Then
                            GoTo NextLoop
                        End If
                    Next
                End If

                If itempic > 0 AndAlso itempic <= NumItems Then
                    If ItemsGfxInfo(itempic).Width <= 64 Then ' more than 1 frame is handled by anim sub

                        With rec
                            .Y = 0
                            .Height = 32
                            .X = 0
                            .Width = 32
                        End With

                        With recPos
                            .Y = InvWindowY + InvTop + ((InvOffsetY + 32) * ((i - 1) \ InvColumns))
                            .Height = PicY
                            .X = InvWindowX + InvLeft + ((InvOffsetX + 32) * (((i - 1) Mod InvColumns)))
                            .Width = PicX
                        End With

                        RenderSprite(ItemsSprite(itempic), GameWindow, recPos.X, recPos.Y, rec.X, rec.Y, rec.Width, rec.Height)

                        ' If item is a stack - draw the amount you have
                        If GetPlayerInvItemValue(Myindex, i) > 1 Then
                            y = recPos.Top + 22
                            x = recPos.Left - 4
                            amount = GetPlayerInvItemValue(Myindex, i)

                            colour = SFML.Graphics.Color.White

                            ' Draw currency but with k, m, b etc. using a convertion function
                            If CLng(amount) < 1000000 Then
                                colour = SFML.Graphics.Color.White
                            ElseIf CLng(amount) > 1000000 AndAlso CLng(amount) < 10000000 Then
                                colour = SFML.Graphics.Color.Yellow
                            ElseIf CLng(amount) > 10000000 Then
                                colour = SFML.Graphics.Color.Green
                            End If

                            DrawText(x, y, ConvertCurrency(amount), colour, SFML.Graphics.Color.Black, GameWindow)

                        End If
                    End If
                End If
            End If
NextLoop:
        Next

        DrawAnimatedInvItems()
    End Sub

    Sub DrawAnimatedInvItems()
        Dim i As Integer
        Dim itemnum As Integer, itempic As Integer

        Dim x As Integer, y As Integer
        Dim maxFrames As Byte
        Dim amount As Integer
        Dim rec As Rectangle, recPos As Rectangle
        Dim clearregion(1) As Rectangle
        Static tmr100 As Integer
        If tmr100 = 0 Then tmr100 = GetTickCount() + 100

        If Not InGame Then Exit Sub

        If GetTickCount() > tmr100 Then
            ' check for map animation changes#
            For i = 1 To MAX_MAP_ITEMS

                If MapItem(i).Num > 0 Then
                    itempic = Item(MapItem(i).Num).Pic

                    If itempic < 1 OrElse itempic > NumItems Then Exit Sub
                    maxFrames = (ItemsGfxInfo(itempic).Width / 2) / 32 ' Work out how many frames there are. /2 because of inventory icons as well as ingame

                    If MapItem(i).Frame < maxFrames - 1 Then
                        MapItem(i).Frame = MapItem(i).Frame + 1
                    Else
                        MapItem(i).Frame = 1
                    End If
                End If
            Next
        End If

        For i = 1 To MAX_INV
            itemnum = GetPlayerInvItemNum(Myindex, i)

            If itemnum > 0 AndAlso itemnum <= MAX_ITEMS Then
                itempic = Item(itemnum).Pic
                If itempic > 0 AndAlso itempic <= NumItems Then
                    If ItemsGfxInfo(itempic).Width > 64 Then

                        maxFrames = (ItemsGfxInfo(itempic).Width / 2) / 32 ' Work out how many frames there are. /2 because of inventory icons as well as ingame

                        If GetTickCount() > tmr100 Then
                            If InvItemFrame(i) < maxFrames - 1 Then
                                InvItemFrame(i) = InvItemFrame(i) + 1
                            Else
                                InvItemFrame(i) = 1
                            End If
                            tmr100 = GetTickCount() + 100
                        End If

                        With rec
                            .Y = 0
                            .Height = 32
                            .X = (ItemsGfxInfo(itempic).Width / 2) + (InvItemFrame(i) * 32) ' middle to get the start of inv gfx, then +32 for each frame
                            .Width = 32
                        End With

                        With recPos
                            .Y = InvTop + ((InvOffsetY + 32) * ((i - 1) \ InvColumns))
                            .Height = PicY
                            .X = InvLeft + ((InvOffsetX + 32) * (((i - 1) Mod InvColumns)))
                            .Width = PicX
                        End With

                        With clearregion(1)
                            .Y = recPos.Y
                            .Height = recPos.Height
                            .X = recPos.X
                            .Width = recPos.Width
                        End With

                        ' We'll now re-draw the item, and place the currency value over it again :P
                        RenderSprite(ItemsSprite(itempic), GameWindow, recPos.X, recPos.Y, rec.X, rec.Y, rec.Width, rec.Height)

                        ' If item is a stack - draw the amount you have
                        If GetPlayerInvItemValue(Myindex, i) > 1 Then
                            y = recPos.Top + 22
                            x = recPos.Left - 4
                            amount = CStr(GetPlayerInvItemValue(Myindex, i))
                            ' Draw currency but with k, m, b etc. using a convertion function
                            DrawText(x, y, ConvertCurrency(amount), SFML.Graphics.Color.Yellow, SFML.Graphics.Color.Black, GameWindow)

                        End If
                    End If
                End If
            End If

        Next
    End Sub

    Friend Sub DrawSkillItem(x As Integer, y As Integer)
        Dim rec As Rectangle
        Dim skillnum As Integer, skillpic As Integer

        skillnum = DragSkillSlotNum

        If skillnum > 0 AndAlso skillnum <= MAX_SKILLS Then

            skillpic = Skill(skillnum).Icon
            If skillpic = 0 Then Exit Sub

            If SkillIconsGfxInfo(skillpic).IsLoaded = False Then
                LoadTexture(skillpic, 9)
            End If

            'seeying we still use it, lets update timer
            With SkillIconsGfxInfo(skillnum)
                .TextureTimer = GetTickCount() + 100000
            End With

            With rec
                .Y = 0
                .Height = PicY
                .X = 0
                .Width = PicX
            End With

            RenderSprite(SkillIconsSprite(skillpic), GameWindow, x + 16, y + 16, rec.X, rec.Y, rec.Width, rec.Height)
        End If
    End Sub

    Sub DrawPlayerSkills()
        Dim i As Integer, skillnum As Integer, skillicon As Integer
        Dim rec As Rectangle, recPos As Rectangle

        If Not InGame Then Exit Sub

        'first render panel
        RenderSprite(SkillPanelSprite, GameWindow, SkillWindowX, SkillWindowY, 0, 0, SkillPanelGfxInfo.Width, SkillPanelGfxInfo.Height)

        For i = 1 To MAX_PLAYER_SKILLS
            skillnum = PlayerSkills(i)

            If skillnum > 0 AndAlso skillnum <= MAX_SKILLS Then
                skillicon = Skill(skillnum).Icon

                If skillicon > 0 AndAlso skillicon <= NumSkillIcons Then

                    If SkillIconsGfxInfo(skillicon).IsLoaded = False Then
                        LoadTexture(skillicon, 9)
                    End If

                    'seeying we still use it, lets update timer
                    With SkillIconsGfxInfo(skillicon)
                        .TextureTimer = GetTickCount() + 100000
                    End With

                    With rec
                        .Y = 0
                        .Height = 32
                        .X = 0
                        .Width = 32
                    End With

                    If Not SkillCd(i) = 0 Then
                        rec.X = 32
                        rec.Width = 32
                    End If

                    With recPos
                        .Y = SkillWindowY + SkillTop + ((SkillOffsetY + 32) * ((i - 1) \ SkillColumns))
                        .Height = PicY
                        .X = SkillWindowX + SkillLeft + ((SkillOffsetX + 32) * (((i - 1) Mod SkillColumns)))
                        .Width = PicX
                    End With

                    RenderSprite(SkillIconsSprite(skillicon), GameWindow, recPos.X, recPos.Y, rec.X, rec.Y, rec.Width, rec.Height)
                End If
            End If
        Next

    End Sub

    Friend Function ToSfmlColor(toConvert As Drawing.Color) As SFML.Graphics.Color
        Return New SFML.Graphics.Color(toConvert.R, toConvert.G, toConvert.G, toConvert.A)
    End Function

    Friend Sub DrawTarget(x2 As Integer, y2 As Integer)
        Dim rec As Rectangle
        Dim x As Integer, y As Integer
        Dim width As Integer, height As Integer

        With rec
            .Y = 0
            .Height = TargetGfxInfo.Height
            .X = 0
            .Width = TargetGfxInfo.Width / 2
        End With

        x = ConvertMapX(x2)
        y = ConvertMapY(y2)
        width = (rec.Right - rec.Left)
        height = (rec.Bottom - rec.Top)

        RenderSprite(TargetSprite, GameWindow, x, y, rec.X, rec.Y, rec.Width, rec.Height)
    End Sub

    Friend Sub DrawHover(x2 As Integer, y2 As Integer)
        Dim rec As Rectangle
        Dim x As Integer, y As Integer
        Dim width As Integer, height As Integer

        With rec
            .Y = 0
            .Height = TargetGfxInfo.Height
            .X = TargetGfxInfo.Width / 2
            .Width = TargetGfxInfo.Width / 2 + TargetGfxInfo.Width / 2
        End With

        x = ConvertMapX(x2)
        y = ConvertMapY(y2)
        width = (rec.Right - rec.Left)
        height = (rec.Bottom - rec.Top)

        RenderSprite(TargetSprite, GameWindow, x, y, rec.X, rec.Y, rec.Width, rec.Height)
    End Sub

    Friend Sub DrawItemDesc()
        Dim xoffset As Integer, yoffset As Integer, y As Integer

        y = 0

        If PnlCharacterVisible = True Then
            xoffset = CharWindowX
            yoffset = CharWindowY
        End If
        If PnlInventoryVisible = True Then
            xoffset = InvWindowX
            yoffset = InvWindowY
        End If
        If PnlBankVisible = True Then
            xoffset = BankWindowX
            yoffset = BankWindowY
        End If
        If PnlShopVisible = True Then
            xoffset = ShopWindowX
            yoffset = ShopWindowY
        End If
        If PnlTradeVisible = True Then
            xoffset = TradeWindowX
            yoffset = TradeWindowY
        End If

        'first render panel
        RenderSprite(DescriptionSprite, GameWindow, xoffset - DescriptionGfxInfo.Width, yoffset, 0, 0, DescriptionGfxInfo.Width, DescriptionGfxInfo.Height)

        'name
        For Each str As String In WordWrap(ItemDescName, 22, WrapMode.Characters, WrapType.BreakWord)
            'description
            DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 12 + y, str, ItemDescRarityColor, ItemDescRarityBackColor, GameWindow)
            y += 15
        Next

        If ShiftDown OrElse VbKeyShift = True Then
            'info
            DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 56, ItemDescInfo, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)

            'cost
            'DrawText(Xoffset - DescriptionGFXInfo.width + 10, Yoffset + 74, "Worth: " & ItemDescCost, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
            'type
            'DrawText(Xoffset - DescriptionGFXInfo.width + 10, Yoffset + 90, "Type: " & ItemDescType, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
            'speed
            DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 74, "Speed: " & ItemDescSpeed, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
            'level
            DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 90, "Level required: " & ItemDescLevel, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
            'bonuses
            DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 118, "=Bonuses=", SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
            'strength
            DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 134, "Strenght: " & ItemDescStr, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
            'vitality
            DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 150, "Vitality: " & ItemDescVit, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
            'intelligence
            DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 166, "Intelligence: " & ItemDescInt, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
            'endurance
            DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 182, "Endurance: " & ItemDescEnd, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
            'luck
            DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 198, "Luck: " & ItemDescLuck, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
            'spirit
            DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 214, "Spirit: " & ItemDescSpr, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        Else
            For Each str As String In WordWrap(ItemDescDescription, 22, WrapMode.Characters, WrapType.BreakWord)
                'description
                DrawText(xoffset - DescriptionGfxInfo.Width + 10, yoffset + 44 + y, str, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
                y += 15
            Next
        End If

    End Sub

    Friend Sub DrawSkillDesc()
        'first render panel
        RenderSprite(DescriptionSprite, GameWindow, SkillWindowX - DescriptionGfxInfo.Width, SkillWindowY, 0, 0, DescriptionGfxInfo.Width, DescriptionGfxInfo.Height)

        'name
        DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 12, SkillDescName, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        'type
        DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 28, SkillDescInfo, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        'cast time
        DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 44, "Cast Time: " & SkillDescCastTime, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        'cool down
        DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 58, "CoolDown: " & SkillDescCoolDown, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        'Damage
        DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 74, "Damage: " & SkillDescDamage, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        'AOE
        DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 90, "Aoe: " & SkillDescAoe, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        'range
        DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 104, "Range: " & SkillDescRange, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)

        'requirements
        DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 128, "=Requirements=", SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        'Mp
        DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 144, "MP: " & SkillDescReqMp, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        'level
        DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 160, "Level: " & SkillDescReqLvl, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        'Access
        DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 176, "Access: " & SkillDescReqAccess, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        'Class
        DrawText(SkillWindowX - DescriptionGfxInfo.Width + 10, SkillWindowY + 192, "Class: " & SkillDescReqClass, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)

    End Sub

    Friend Sub DrawDialogPanel()
        'first render panel
        RenderSprite(EventChatSprite, GameWindow, DialogPanelX, DialogPanelY, 0, 0, EventChatGfxInfo.Width, EventChatGfxInfo.Height)

        DrawText(DialogPanelX + 175, DialogPanelY + 10, Trim(DialogMsg1), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)

        If Len(DialogMsg2) > 0 Then
            DrawText(DialogPanelX + 60, DialogPanelY + 30, Trim(DialogMsg2), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        End If

        If Len(DialogMsg3) > 0 Then
            DrawText(DialogPanelX + 60, DialogPanelY + 50, Trim(DialogMsg3), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        End If

        If DialogType = DialogueTypeQuest Then
            If QuestAcceptTag > 0 Then
                'render accept button
                DrawButton(DialogButton1Text, DialogPanelX + OkButtonX, DialogPanelY + OkButtonY, 0)
                DrawButton(DialogButton2Text, DialogPanelX + CancelButtonX, DialogPanelY + CancelButtonY, 0)
            Else
                'render cancel button
                DrawButton(DialogButton2Text, DialogPanelX + CancelButtonX - 140, DialogPanelY + CancelButtonY, 0)
            End If
        Else
            'render ok button
            DrawButton(DialogButton1Text, DialogPanelX + OkButtonX, DialogPanelY + OkButtonY, 0)

            'render cancel button
            DrawButton(DialogButton2Text, DialogPanelX + CancelButtonX, DialogPanelY + CancelButtonY, 0)
        End If

    End Sub

    Friend Sub DrawRClick()
        'first render panel
        RenderSprite(RClickSprite, GameWindow, RClickX, RClickY, 0, 0, RClickGfxInfo.Width, RClickGfxInfo.Height)

        DrawText(RClickX + (RClickGfxInfo.Width \ 2) - (GetTextWidth(RClickname) \ 2), RClickY + 10, RClickname, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)

        DrawText(RClickX + (RClickGfxInfo.Width \ 2) - (GetTextWidth("Invite to Trade") \ 2), RClickY + 35, "Invite to Trade", SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)

        DrawText(RClickX + (RClickGfxInfo.Width \ 2) - (GetTextWidth("Invite to Party") \ 2), RClickY + 60, "Invite to Party", SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)

        DrawText(RClickX + (RClickGfxInfo.Width \ 2) - (GetTextWidth("Invite to House") \ 2), RClickY + 85, "Invite to House", SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)

    End Sub

    Friend Sub DrawGui()
        'hide GUI when mapping...
        If HideGui = True Then Exit Sub

        If HudVisible = True Then
            DrawHud()
            DrawActionPanel()
            DrawChat()
            DrawHotbar()
            DrawPetBar()
            DrawPetStats()
        End If

        If PnlCharacterVisible = True Then
            DrawEquipment()
            If ShowItemDesc = True Then DrawItemDesc()
        End If

        If PnlInventoryVisible = True Then
            DrawInventory()
            If ShowItemDesc = True Then DrawItemDesc()
        End If

        If PnlSkillsVisible = True Then
            DrawPlayerSkills()
            If ShowSkillDesc = True Then DrawSkillDesc()
        End If

        If DialogPanelVisible = True Then
            DrawDialogPanel()
        End If

        If PnlBankVisible = True Then
            DrawBank()
        End If

        If PnlShopVisible = True Then
            DrawShop()
        End If

        If PnlTradeVisible = True Then
            DrawTrade()
        End If

        If PnlEventChatVisible = True Then
            DrawEventChat()
        End If

        If PnlRClickVisible = True Then
            DrawRClick()
        End If

        If PnlQuestLogVisible = True Then
            DrawQuestLog()
        End If

        If PnlCraftVisible = True Then
            DrawCraftPanel()
        End If

        If DragInvSlotNum > 0 Then
            DrawInventoryItem(CurMouseX, CurMouseY)
        End If

        If DragBankSlotNum > 0 Then
            DrawBankItem(CurMouseX, CurMouseY)
        End If

        If DragSkillSlotNum > 0 Then
            DrawSkillItem(CurMouseX, CurMouseY)
        End If

        'draw cursor
        'DrawCursor()
    End Sub

    Sub DrawNight()
        Dim x As Integer, y As Integer

        If Map.Moral = MapMoralType.Indoors Then Exit Sub

        Select Case Time.Instance.TimeOfDay
            Case TimeOfDay.Dawn
                NightGfx.Clear(New SFML.Graphics.Color(0, 0, 0, 100))
                Exit Select

            Case TimeOfDay.Dusk
                NightGfx.Clear(New SFML.Graphics.Color(0, 0, 0, 150))
                Exit Select

            Case TimeOfDay.Night
                NightGfx.Clear(New SFML.Graphics.Color(0, 0, 0, 200))
                Exit Select

            Case Else
                Exit Sub
        End Select

        For x = TileView.Left To TileView.Right + 1
            For y = TileView.Top To TileView.Bottom + 1
                If IsValidMapPoint(x, y) Then
                    If Map.Tile(x, y).Type = TileType.Light Then
                        Dim x1 = ConvertMapX(x * 32) + 16 - LightGfxInfo.Width / 2
                        Dim y1 = ConvertMapY(y * 32) + 16 - LightGfxInfo.Height / 2

                        'Create the light texture to multiply over the dark texture.
                        LightSprite.Position = New Vector2f(x1, y1)
                        LightSprite.Color = SFML.Graphics.Color.Red
                        NightGfx.Draw(LightSprite, New RenderStates(BlendMode.Multiply))

                        ''Create the light texture to multiply over the dark texture.
                        'LightSprite.Position = New Vector2f(X1, Y1)
                        'LightAreaSprite.Position = New Vector2f(X1, Y1)
                        ''LightSprite.Color = New SFML.Graphics.Color(SFML.Graphics.Color.Red)
                        ''LightAreaSprite.Color = New SFML.Graphics.Color(SFML.Graphics.Color.Red)
                        'NightGfx.Draw(LightSprite, New RenderStates(BlendMode.Multiply))
                        'NightGfx.Draw(LightAreaSprite, New RenderStates(BlendMode.Multiply))
                    End If
                End If
            Next
        Next

        NightSprite = New Sprite(NightGfx.Texture)

        NightGfx.Display()
        GameWindow.Draw(NightSprite)
    End Sub

    Sub DrawCursor()
        RenderSprite(CursorSprite, GameWindow, CurMouseX, CurMouseY, 0, 0, CursorInfo.Width, CursorInfo.Height)
    End Sub


    Friend Sub EditorItem_DrawItem()
        Dim itemnum As Integer
        itemnum = frmEditor_Item.nudPic.Value

        If itemnum < 1 OrElse itemnum > NumItems Then
            frmEditor_Item.picItem.BackgroundImage = Nothing
            Exit Sub
        End If

        If File.Exists(Path.Graphics & "items\" & itemnum & GfxExt) Then
            frmEditor_Item.picItem.BackgroundImage = Drawing.Image.FromFile(Path.Graphics & "items\" & itemnum & GfxExt)
        End If

    End Sub

    Friend Sub EditorItem_DrawPaperdoll()
        Dim Sprite As Integer

        Sprite = frmEditor_Item.nudPaperdoll.Value

        If Sprite < 1 OrElse Sprite > NumPaperdolls Then
            frmEditor_Item.picPaperdoll.BackgroundImage = Nothing
            Exit Sub
        End If

        If File.Exists(Path.Graphics & "paperdolls\" & Sprite & GfxExt) Then
            frmEditor_Item.picPaperdoll.BackgroundImage = Drawing.Image.FromFile(Path.Graphics & "paperdolls\" & Sprite & GfxExt)
        End If
    End Sub

    Friend Sub EditorItem_DrawFurniture()
        Dim Furniturenum As Integer
        Dim sRECT As Rectangle
        Dim dRECT As Rectangle
        Furniturenum = frmEditor_Item.nudFurniture.Value

        If Furniturenum < 1 OrElse Furniturenum > NumFurniture Then
            EditorItem_Furniture.Clear(ToSfmlColor(frmEditor_Item.picFurniture.BackColor))
            EditorItem_Furniture.Display()
            Exit Sub
        End If

        If FurnitureGfxInfo(Furniturenum).IsLoaded = False Then
            LoadTexture(Furniturenum, 10)
        End If

        'seeying we still use it, lets update timer
        With FurnitureGfxInfo(Furniturenum)
            .TextureTimer = GetTickCount() + 100000
        End With

        ' rect for source
        With sRECT
            .Y = 0
            .Height = FurnitureGfxInfo(Furniturenum).Height
            .X = 0
            .Width = FurnitureGfxInfo(Furniturenum).Width
        End With

        ' same for destination as source
        dRECT = sRECT

        EditorItem_Furniture.Clear(ToSfmlColor(frmEditor_Item.picFurniture.BackColor))

        RenderSprite(FurnitureSprite(Furniturenum), EditorItem_Furniture, dRECT.X, dRECT.Y, sRECT.X, sRECT.Y, sRECT.Width, sRECT.Height)

        If frmEditor_Item.optSetBlocks.Checked = True Then
            For X = 0 To 3
                For Y = 0 To 3
                    If X <= (FurnitureGfxInfo(Furniturenum).Width / 32) - 1 Then
                        If Y <= (FurnitureGfxInfo(Furniturenum).Height / 32) - 1 Then
                            If Item(Editorindex).FurnitureBlocks(X, Y) = 1 Then
                                DrawText(X * 32 + 8, Y * 32 + 8, "X", Color.Red, Color.Black, EditorItem_Furniture)
                            Else
                                DrawText(X * 32 + 8, Y * 32 + 8, "O", Color.Blue, Color.Black, EditorItem_Furniture)
                            End If
                        End If
                    End If
                Next
            Next
        ElseIf frmEditor_Item.optSetFringe.Checked = True Then
            For X = 0 To 3
                For Y = 0 To 3
                    If X <= (FurnitureGfxInfo(Furniturenum).Width / 32) - 1 Then
                        If Y <= (FurnitureGfxInfo(Furniturenum).Height / 32) Then
                            If Item(Editorindex).FurnitureFringe(X, Y) = 1 Then
                                DrawText(X * 32 + 8, Y * 32 + 8, "O", Color.Blue, Color.Black, EditorItem_Furniture)
                            End If
                        End If
                    End If
                Next
            Next
        End If
        EditorItem_Furniture.Display()
    End Sub

    Friend Sub EditorNpc_DrawSprite()
        Dim Sprite As Integer

        Sprite = frmEditor_NPC.nudSprite.Value

        If Sprite < 1 OrElse Sprite > NumCharacters Then
            frmEditor_NPC.picSprite.BackgroundImage = Nothing
            Exit Sub
        End If

        If File.Exists(Path.Graphics & "characters\" & Sprite & GfxExt) Then
            frmEditor_NPC.picSprite.Width = Drawing.Image.FromFile(Path.Graphics & "characters\" & Sprite & GfxExt).Width / 4
            frmEditor_NPC.picSprite.Height = Drawing.Image.FromFile(Path.Graphics & "characters\" & Sprite & GfxExt).Height / 4
            frmEditor_NPC.picSprite.BackgroundImage = Drawing.Image.FromFile(Path.Graphics & "characters\" & Sprite & GfxExt)
        End If
    End Sub

    Friend Sub EditorResource_DrawSprite()
        Dim Sprite As Integer

        ' normal sprite
        Sprite = frmEditor_Resource.nudNormalPic.Value

        If Sprite < 1 OrElse Sprite > NumResources Then
            frmEditor_Resource.picNormalpic.BackgroundImage = Nothing
        Else
            If File.Exists(Path.Graphics & "resources\" & Sprite & GfxExt) Then
                frmEditor_Resource.picNormalpic.BackgroundImage = Drawing.Image.FromFile(Path.Graphics & "resources\" & Sprite & GfxExt)
            End If
        End If

        ' exhausted sprite
        Sprite = frmEditor_Resource.nudExhaustedPic.Value

        If Sprite < 1 OrElse Sprite > NumResources Then
            frmEditor_Resource.picExhaustedPic.BackgroundImage = Nothing
        Else
            If File.Exists(Path.Graphics & "resources\" & Sprite & GfxExt) Then
                frmEditor_Resource.picExhaustedPic.BackgroundImage = Drawing.Image.FromFile(Path.Graphics & "resources\" & Sprite & GfxExt)
            End If
        End If
    End Sub

    Friend Sub EditorSkill_BltIcon()
        Dim iconnum As Integer
        Dim sRECT As Rectangle
        Dim dRECT As Rectangle
        iconnum = frmEditor_Skill.nudIcon.Value

        If iconnum < 1 OrElse iconnum > NumSkillIcons Then
            EditorSkill_Icon.Clear(ToSfmlColor(frmEditor_Skill.picSprite.BackColor))
            EditorSkill_Icon.Display()
            Exit Sub
        End If

        If SkillIconsGfxInfo(iconnum).IsLoaded = False Then
            LoadTexture(iconnum, 9)
        End If

        'seeying we still use it, lets update timer
        With SkillIconsGfxInfo(iconnum)
            .TextureTimer = GetTickCount() + 100000
        End With

        With sRECT
            .Y = 0
            .Height = PicY
            .X = 0
            .Width = PicX
        End With

        'drect is the same, so just copy it
        dRECT = sRECT

        EditorSkill_Icon.Clear(ToSfmlColor(frmEditor_Skill.picSprite.BackColor))

        RenderSprite(SkillIconsSprite(iconnum), EditorSkill_Icon, dRECT.X, dRECT.Y, sRECT.X, sRECT.Y, sRECT.Width, sRECT.Height)

        EditorSkill_Icon.Display()
    End Sub

    Friend Sub EditorAnim_DrawAnim()
        Dim Animationnum As Integer
        Dim sRECT As Rectangle
        Dim dRECT As Rectangle
        Dim width As Integer, height As Integer
        Dim looptime As Integer
        Dim FrameCount As Integer
        Dim ShouldRender As Boolean

        Animationnum = FrmEditor_Animation.nudSprite0.Value

        If Animationnum < 1 OrElse Animationnum > NumAnimations Then
            EditorAnimation_Anim1.Clear(ToSfmlColor(FrmEditor_Animation.picSprite0.BackColor))
            EditorAnimation_Anim1.Display()
        Else
            If AnimationsGfxInfo(Animationnum).IsLoaded = False Then
                LoadTexture(Animationnum, 6)
            End If

            'seeying we still use it, lets update timer
            With AnimationsGfxInfo(Animationnum)
                .TextureTimer = GetTickCount() + 100000
            End With

            looptime = FrmEditor_Animation.nudLoopTime0.Value
            FrameCount = FrmEditor_Animation.nudFrameCount0.Value

            ShouldRender = False

            ' check if we need to render new frame
            If AnimEditorTimer(0) + looptime <= GetTickCount() Then
                ' check if out of range
                If AnimEditorFrame(0) >= FrameCount Then
                    AnimEditorFrame(0) = 1
                Else
                    AnimEditorFrame(0) = AnimEditorFrame(0) + 1
                End If
                AnimEditorTimer(0) = GetTickCount()
                ShouldRender = True
            End If

            If ShouldRender Then
                If FrmEditor_Animation.nudFrameCount0.Value > 0 Then
                    ' total width divided by frame count
                    height = AnimationsGfxInfo(Animationnum).Height
                    width = AnimationsGfxInfo(Animationnum).Width / FrmEditor_Animation.nudFrameCount0.Value

                    With sRECT
                        .Y = 0
                        .Height = height
                        .X = (AnimEditorFrame(0) - 1) * width
                        .Width = width
                    End With

                    With dRECT
                        .Y = 0
                        .Height = height
                        .X = 0
                        .Width = width
                    End With

                    EditorAnimation_Anim1.Clear(ToSfmlColor(FrmEditor_Animation.picSprite0.BackColor))

                    RenderSprite(AnimationsSprite(Animationnum), EditorAnimation_Anim1, dRECT.X, dRECT.Y, sRECT.X, sRECT.Y, sRECT.Width, sRECT.Height)

                    EditorAnimation_Anim1.Display()
                End If
            End If
        End If

        Animationnum = FrmEditor_Animation.nudSprite1.Value

        If Animationnum < 1 OrElse Animationnum > NumAnimations Then
            EditorAnimation_Anim2.Clear(ToSfmlColor(FrmEditor_Animation.picSprite1.BackColor))
            EditorAnimation_Anim2.Display()
        Else
            If AnimationsGfxInfo(Animationnum).IsLoaded = False Then
                LoadTexture(Animationnum, 6)
            End If

            'seeying we still use it, lets update timer
            With AnimationsGfxInfo(Animationnum)
                .TextureTimer = GetTickCount() + 100000
            End With

            looptime = FrmEditor_Animation.nudLoopTime1.Value
            FrameCount = FrmEditor_Animation.nudFrameCount1.Value

            ShouldRender = False

            ' check if we need to render new frame
            If AnimEditorTimer(1) + looptime <= GetTickCount() Then
                ' check if out of range
                If AnimEditorFrame(1) >= FrameCount Then
                    AnimEditorFrame(1) = 1
                Else
                    AnimEditorFrame(1) = AnimEditorFrame(1) + 1
                End If
                AnimEditorTimer(1) = GetTickCount()
                ShouldRender = True
            End If

            If ShouldRender Then
                If FrmEditor_Animation.nudFrameCount1.Value > 0 Then
                    ' total width divided by frame count
                    height = AnimationsGfxInfo(Animationnum).Height
                    width = AnimationsGfxInfo(Animationnum).Width / FrmEditor_Animation.nudFrameCount1.Value

                    With sRECT
                        .Y = 0
                        .Height = height
                        .X = (AnimEditorFrame(1) - 1) * width
                        .Width = width
                    End With

                    With dRECT
                        .Y = 0
                        .Height = height
                        .X = 0
                        .Width = width
                    End With

                    EditorAnimation_Anim2.Clear(ToSfmlColor(FrmEditor_Animation.picSprite1.BackColor))

                    RenderSprite(AnimationsSprite(Animationnum), EditorAnimation_Anim2, dRECT.X, dRECT.Y, sRECT.X, sRECT.Y, sRECT.Width, sRECT.Height)
                    EditorAnimation_Anim2.Display()

                End If
            End If
        End If
    End Sub
End Module