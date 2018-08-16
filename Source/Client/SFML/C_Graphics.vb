Imports SFML.Graphics
Imports SFML.Window

Namespace Graphics
    Friend Module modGraphics

#Region " Variables "

        Friend MainFont As SFML.Graphics.Font

#End Region

#Region " Instance Handlers "

        Friend Sub Initialize()
            MainFont = New Font(Path_Fonts() & "Arial.ttf")
            InitializeGraphics()
#If CLIENT Then
            InitializeGui()
#End If
        End Sub

        Friend Sub Destroy()
            MainFont.Dispose()
            DestroyGraphics()
#If CLIENT Then
            DestroyGui()
#End If
        End Sub

#End Region

#Region " Auto Methods "

        Friend Sub LoadGraphic(ByRef sprite As Sprite, file As String)
            sprite = New Sprite(SFML.Portable.AutoTexture(Path_Graphics() + file))
        End Sub

        Friend Sub LoadGraphicStack(ByRef sprites As Sprite(), file As String)
            sprites = SFML.Portable.AutoSpriteStack(Path_Graphics() + file)
        End Sub

        Friend Sub LoadGui(ByRef sprite As Sprite, file As String)
            sprite = New Sprite(SFML.Portable.AutoTexture(Path_Gui() + file))
        End Sub

        Friend Sub LoadGuiStack(ByRef sprites As Sprite(), file As String)
            sprites = SFML.Portable.AutoSpriteStack(Path_Gui() + file)
        End Sub

        Friend Sub UnloadSprite(ByRef sprite As Sprite)
            If sprite Is Nothing Then Exit Sub
            sprite.Dispose()
            sprite = Nothing
        End Sub

        Friend Sub UnloadSpriteStack(ByRef sprites As Sprite())
            If sprites Is Nothing Then Exit Sub
            Dim len = sprites.Length
            For i = 0 To len - 1
                sprites(i).Dispose()
            Next
        End Sub

#End Region

#Region " Graphics "

        ' Singles
        Friend Blood As Sprite '
        Friend ChatBubble As Sprite
        Friend Door As Sprite '
        Friend Light As Sprite
        Friend Night As Sprite
        Friend Shadow As Sprite
        Friend Target As Sprite '
        Friend Weather As Sprite '

        ' Stacks
        Friend Actors As Sprite()
        Friend Animations As Sprite() '
        Friend Backgrounds As Sprite() '
        Friend Characters As Sprite() '
        Friend Emotes As Sprite() '
        Friend Faces As Sprite() '
        Friend Fogs As Sprite() '
        Friend Furniture As Sprite() '
        Friend Items As Sprite() '
        Friend PaperDolls As Sprite() '
        Friend Parallaxes As Sprite() '
        Friend Projectiles As Sprite() '
        Friend Resources As Sprite() '
        Friend SkillIcons As Sprite() '
        Friend Tilesets As Sprite() '

        Private Sub InitializeGraphics()
            ' Singles
            LoadGraphic(Blood, "Blood")
            LoadGraphic(ChatBubble, "ChatBubble")
            LoadGraphic(Door, "Door")
            LoadGraphic(Light, "Light")
            LoadGraphic(Night, "Night")
            LoadGraphic(Shadow, "Shadow")
            LoadGraphic(Target, "Target")
            LoadGraphic(Weather, "Weather")

            ' Stacks
            LoadGraphicStack(Actors, "Actors")
            LoadGraphicStack(Animations, "Animations")
            LoadGraphicStack(Backgrounds, "Backgrounds")
            LoadGraphicStack(Characters, "Characters")
            LoadGraphicStack(Emotes, "Emotes")
            LoadGraphicStack(Faces, "Faces")
            LoadGraphicStack(Fogs, "Fogs")
            LoadGraphicStack(Furniture, "Furniture")
            LoadGraphicStack(Items, "Items")
            LoadGraphicStack(PaperDolls, "PaperDolls")
            LoadGraphicStack(Parallaxes, "Parallaxes")
            LoadGraphicStack(Projectiles, "Projectiles")
            LoadGraphicStack(Resources, "Resources")
            LoadGraphicStack(SkillIcons, "SkillIcons")
            LoadGraphicStack(Tilesets, "Tilesets")

            '##############################'
            '###  Prepare odd graphics  ###'
            '##############################'

            ' Set light/night center for easy positioning.
            Dim lightHalf = New Vector2f(Light.Texture.Size.X / 2,
                Light.Texture.Size.Y / 2)
            Light.Origin = lightHalf
            Night.Origin = lightHalf

            ' Set Fog Filters
            For i = 0 To Fogs.Length - 1
                Fogs(i).Texture.Repeated = True
                Fogs(i).Texture.Smooth = True
            Next
        End Sub

        Private Sub DestroyGraphics()
            ' Singles
            UnloadSprite(Blood)
            UnloadSprite(ChatBubble)
            UnloadSprite(Door)
            UnloadSprite(Light)
            UnloadSprite(Night)
            UnloadSprite(Shadow)
            UnloadSprite(Target)
            UnloadSprite(Weather)

            ' Stacks
            UnloadSpriteStack(Actors)
            UnloadSpriteStack(Animations)
            UnloadSpriteStack(Backgrounds)
            UnloadSpriteStack(Characters)
            UnloadSpriteStack(Emotes)
            UnloadSpriteStack(Faces)
            UnloadSpriteStack(Fogs)
            UnloadSpriteStack(Furniture)
            UnloadSpriteStack(Items)
            UnloadSpriteStack(PaperDolls)
            UnloadSpriteStack(Parallaxes)
            UnloadSpriteStack(Projectiles)
            UnloadSpriteStack(Resources)
            UnloadSpriteStack(SkillIcons)
            UnloadSpriteStack(Tilesets)
        End Sub

#End Region

#Region " Gui "

        ' Singles
        Friend BarExp As Sprite
        Friend BarHp As Sprite
        Friend BarMp As Sprite
        Friend BarProgress As Sprite
        Friend ButtonDefault As Sprite
        Friend ButtonHover As Sprite
        Friend ButtonMathAdd As Sprite
        Friend ButtonMathSub As Sprite
        Friend ChatBox As Sprite
        Friend ChatSend As Sprite
        Friend Cursor As Sprite
        Friend Hotbar As Sprite
        Friend Hud As Sprite
        Friend PanelActions As Sprite
        Friend PanelBank As Sprite
        Friend PanelCharacter As Sprite
        Friend PanelCraft As Sprite
        Friend PanelDescription As Sprite
        Friend PanelDialogue As Sprite
        Friend PanelHotMenu As Sprite
        Friend PanelInventory As Sprite
        Friend PanelPet As Sprite
        Friend PanelPetBar As Sprite
        Friend PanelQuest As Sprite
        Friend PanelShop As Sprite
        Friend PanelSkill As Sprite
        Friend PanelTrade As Sprite

        Private Sub InitializeGui()
            ' Singles
            LoadGui(BarExp, "Game\BarExp")
            LoadGui(BarHp, "Game\BarHp")
            LoadGui(BarMp, "Game\BarMp")
            LoadGui(BarProgress, "Game\BarProgress")
            LoadGui(ButtonDefault, "Game\ButtonDefault")
            LoadGui(ButtonHover, "Game\ButtonHover")
            LoadGui(ButtonMathAdd, "Game\ButtonMathAdd")
            LoadGui(ButtonMathSub, "Game\ButtonMathSub")
            LoadGui(ChatBox, "Game\ChatBox")
            LoadGui(ChatSend, "Game\ChatSend")
            LoadGui(Cursor, "Game\Cursor")
            LoadGui(Hotbar, "Game\Hotbar")
            LoadGui(Hud, "Game\Hud")
            LoadGui(PanelActions, "Game\PanelActions")
            LoadGui(PanelBank, "Game\PanelBank")
            LoadGui(PanelCharacter, "Game\PanelCharacter")
            LoadGui(PanelCraft, "Game\PanelCraft")
            LoadGui(PanelDescription, "Game\PanelDescription")
            LoadGui(PanelDialogue, "Game\PanelDialogue")
            LoadGui(PanelHotMenu, "Game\PanelHotMenu")
            LoadGui(PanelInventory, "Game\PanelInventory")
            LoadGui(PanelPet, "Game\PanelPet")
            LoadGui(PanelPetBar, "Game\PanelPetBar")
            LoadGui(PanelQuest, "Game\PanelQuest")
            LoadGui(PanelShop, "Game\PanelShop")
            LoadGui(PanelSkill, "Game\PanelSkill")
            LoadGui(PanelTrade, "Game\PanelTrade")

            ' Stacks
        End Sub

        Private Sub DestroyGui()
            ' Singles
            UnloadSprite(BarExp)
            UnloadSprite(BarHp)
            UnloadSprite(BarMp)
            UnloadSprite(BarProgress)
            UnloadSprite(ButtonDefault)
            UnloadSprite(ButtonHover)
            UnloadSprite(ButtonMathAdd)
            UnloadSprite(ButtonMathSub)
            UnloadSprite(ChatBox)
            UnloadSprite(ChatSend)
            UnloadSprite(Cursor)
            UnloadSprite(Hotbar)
            UnloadSprite(Hud)
            UnloadSprite(PanelActions)
            UnloadSprite(PanelBank)
            UnloadSprite(PanelCharacter)
            UnloadSprite(PanelCraft)
            UnloadSprite(PanelDescription)
            UnloadSprite(PanelDialogue)
            UnloadSprite(PanelHotMenu)
            UnloadSprite(PanelInventory)
            UnloadSprite(PanelPet)
            UnloadSprite(PanelPetBar)
            UnloadSprite(PanelQuest)
            UnloadSprite(PanelShop)
            UnloadSprite(PanelSkill)
            UnloadSprite(PanelTrade)

            ' Stacks
        End Sub

#End Region
    End Module
End Namespace