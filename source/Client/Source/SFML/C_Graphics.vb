Imports SFML.Graphics
Imports SFML.System
Imports System.IO

Namespace Graphics
    Friend Module modGraphics

        ' Utility objects
        Friend SpriteBatch As SpriteBatch
        Friend ColorUnavailable As Color
        Friend Camera As Rectangle
        Friend Viewport As Rectangle
        Friend VectorZero As Vector2f
        Friend VectorOne As Vector2f

        ' Universal Graphic Values
        Friend TileSize As UInteger = 32

        Friend Sub Initialize()
            SpriteBatch = New SpriteBatch()
            ColorUnavailable = New Color(80, 80, 80)
            VectorZero = New Vector2f(0, 0)
            VectorOne = New Vector2f(1, 1)

            InitializeFonts()
            InitializeGraphics()
            InitializeGui()
        End Sub

        Friend Sub Destroy()

            DestroyFonts()
            DestroyGraphics()
            DestroyGui()
        End Sub

#Region " API "

        Private Sub LoadFont(ByRef font As Font, name As String)
            font = New Font(Environment.GetFolderPath(
                            Environment.SpecialFolder.Fonts) + "/" + name + ".ttf")
        End Sub

        Private Sub LoadTexture(ByRef texture As Texture, filePath As String)
            Dim cd = Path.Contents & filePath
            If Not File.Exists(cd) Then Return

            UnloadTexture(texture)
            texture = SFML.Portable.AutoTexture(cd)
        End Sub

        Private Sub LoadTextureStack(ByRef textures() As Texture, dirPath As String)
            Dim cd = Path.Contents & dirPath
            If Not Directory.Exists(cd) Then Return

            UnloadTextureStack(textures)
            textures = SFML.Portable.AutoTextureStack(cd)
        End Sub

        Private Sub UnloadFont(ByRef font As Font)
            If font Is Nothing Then Return

            font.Dispose()
            font = Nothing
        End Sub

        Private Sub UnloadTexture(ByRef texture As Texture)
            If texture Is Nothing Then Return

            texture.Dispose()
            texture = Nothing
        End Sub

        Private Sub UnloadTextureStack(ByRef textures() As Texture)
            If textures Is Nothing Then Return

            For i = 0 To UBound(textures)
                If textures(i) Is Nothing Then Continue For

                textures(i).Dispose()
                textures(i) = Nothing
            Next

            textures = Nothing
        End Sub

        Friend Function CreateDefaultText() As Text
            Return New Text With {
                .Font = Font_Default,
                .CharacterSize = Font_Default_Size
            }
        End Function

        Friend Sub RenderTexture(texture As Texture, position As Vector2f)
            If texture Is Nothing Then Return
            SpriteBatch.Draw(texture, VectorZero, position, VectorOne, Color.White)
        End Sub

        Friend Sub RenderTexture(texture As Texture, position As Vector2f, color As Color)
            If texture Is Nothing Then Return
            SpriteBatch.Draw(texture, VectorZero, position, VectorOne, color)
        End Sub

        Friend Sub RenderTexture(texture As Texture, textureRect As IntRect, position As Vector2f)
            If texture Is Nothing Then Return
            SpriteBatch.Draw(texture, textureRect, VectorZero, position, VectorOne, Color.White)
        End Sub

        Friend Sub RenderTexture(texture As Texture, textureRect As IntRect,
                                 position As Vector2f, color As Color)
            If texture Is Nothing Then Return
            SpriteBatch.Draw(texture, textureRect, VectorZero, position, VectorOne, color)
        End Sub

        Friend Sub RenderTexture(texture As Texture, destination As FloatRect)
            If texture Is Nothing Then Return
            Dim texSize = texture.Size
            Dim destSize As New Vector2f(destination.Width, destination.Height)
            Dim scale As New Vector2f(destSize.X / texSize.X, destSize.Y / texSize.Y)
            Dim position As New Vector2f(destination.Left, destination.Top)
            SpriteBatch.Draw(texture, VectorZero, position, scale, Color.White)
        End Sub

        Friend Sub RenderTexture(texture As Texture, destination As FloatRect, color As Color)
            If texture Is Nothing Then Return
            Dim texSize = texture.Size
            Dim destSize As New Vector2f(destination.Width, destination.Height)
            Dim scale As New Vector2f(destSize.X / texSize.X, destSize.Y / texSize.Y)
            Dim position As New Vector2f(destination.Left, destination.Top)
            SpriteBatch.Draw(texture, VectorZero, position, scale, color)
        End Sub

        Friend Sub RenderTexture(texture As Texture, textureRect As IntRect, destination As FloatRect)
            If texture Is Nothing Then Return
            Dim position As New Vector2f(destination.Left, destination.Top)
            Dim destSize As New Vector2f(destination.Width, destination.Height)
            Dim texSize As New Vector2f(textureRect.Width, textureRect.Height)
            Dim scale As New Vector2f(destSize.X / texSize.X, destSize.Y / texSize.Y)
            SpriteBatch.Draw(texture, textureRect, VectorZero, position, scale, Color.White)
        End Sub

        Friend Sub RenderTexture(texture As Texture, textureRect As IntRect,
                                 destination As FloatRect, color As Color)
            If texture Is Nothing Then Return
            Dim position As New Vector2f(destination.Left, destination.Top)
            Dim destSize As New Vector2f(destination.Width, destination.Height)
            Dim texSize As New Vector2f(textureRect.Width, textureRect.Height)
            Dim scale As New Vector2f(destSize.X / texSize.X, destSize.Y / texSize.Y)
            SpriteBatch.Draw(texture, textureRect, VectorZero, position, scale, color)
        End Sub

        Friend Sub RenderText(text As String, position As Vector2f)
            If Font_Default Is Nothing Then Return
            SpriteBatch.Draw(text, Font_Default, position, Color.Black)
        End Sub

        Friend Sub RenderText(text As String, position As Vector2f, color As Color)
            If Font_Default Is Nothing Then Return
            SpriteBatch.Draw(text, Font_Default, position, color)
        End Sub

        Friend Sub RenderText(text As String, font As Font, position As Vector2f)
            If font Is Nothing Then Return
            SpriteBatch.Draw(text, font, position, Color.Black)
        End Sub

        Friend Sub RenderText(text As String, font As Font, position As Vector2f, color As Color)
            If font Is Nothing Then Return
            SpriteBatch.Draw(text, font, position, color)
        End Sub

        Friend Sub RenderRectangle(position As Vector2f, size As Vector2f,
                                   thickness As Integer, border As Color)
            Dim primitive As New RectangleShape(size) With {
                .Position = position,
                .OutlineThickness = thickness,
                .OutlineColor = border
            } : SpriteBatch.Draw(primitive)
            primitive.Dispose()
        End Sub

        Friend Sub RenderRectangle(position As Vector2f, size As Vector2f,
                                   thickness As Integer, border As Color, fill As Color)
            Dim primitive As New RectangleShape(size) With {
                .Position = position,
                .OutlineThickness = thickness,
                .OutlineColor = border,
                .FillColor = fill
            } : SpriteBatch.Draw(primitive)
            primitive.Dispose()
        End Sub

        Friend Sub RenderDrawable(drawable As Drawable)
            SpriteBatch.Draw(drawable)
        End Sub

#End Region

#Region " Fonts "

        ' Fonts
        Friend Font_Default As Font

        ' Font Sizes
        Friend Const Font_Default_Size As UInteger = 12

        Private Sub InitializeFonts()
            LoadFont(Font_Default, "Arial")
        End Sub

        Private Sub DestroyFonts()
            UnloadFont(Font_Default)
        End Sub

#End Region

#Region " Graphics "

        ' Singles
        Friend Blood As Texture
        Friend ChatBubble As Texture
        Friend Directions As Texture
        Friend Door As Texture
        Friend Light As Texture
        Friend Shadow As Texture
        Friend Target As Texture
        Friend Weather As Texture

        ' Stacks
        Friend Animations() As Texture
        Friend Characters() As Texture
        Friend Emotes() As Texture
        Friend Faces() As Texture
        Friend Fogs() As Texture
        Friend Furniture() As Texture
        Friend Items() As Texture
        Friend Npcs() As Texture
        Friend Panoramas() As Texture
        Friend Paperdolls() As Texture
        Friend Parallaxes() As Texture
        Friend Projectiles() As Texture
        Friend Resources() As Texture
        Friend Skills() As Texture
        Friend Tilesets() As Texture

        Private Sub InitializeGraphics()
            ' Singles
            LoadTexture(Blood, "Graphics/Misc/Blood")
            LoadTexture(ChatBubble, "Graphics/Misc/ChatBubble")
            LoadTexture(Directions, "Graphics/Misc/Directions")
            LoadTexture(Door, "Graphics/Misc/Door")
            LoadTexture(Light, "Graphics/Misc/Light")
            LoadTexture(Shadow, "Graphics/Misc/Shadow")
            LoadTexture(Target, "Graphics/Misc/Target")
            LoadTexture(Weather, "Graphics/Misc/Weather")

            ' Stacks
            LoadTextureStack(Animations, "Graphics/Animations")
            LoadTextureStack(Characters, "Graphics/Characters")
            LoadTextureStack(Emotes, "Graphics/Emotes")
            LoadTextureStack(Faces, "Graphics/Faces")
            LoadTextureStack(Fogs, "Graphics/Fogs")
            LoadTextureStack(Furniture, "Graphics/Furniture")
            LoadTextureStack(Items, "Graphics/Items")
            LoadTextureStack(Items, "Graphics/Npcs")
            LoadTextureStack(Panoramas, "Graphics/Panoramas")
            LoadTextureStack(Paperdolls, "Graphics/Paperdolls")
            LoadTextureStack(Parallaxes, "Graphics/Parallaxes")
            LoadTextureStack(Projectiles, "Graphics/Projectiles")
            LoadTextureStack(Resources, "Graphics/Resources")
            LoadTextureStack(Skills, "Graphics/Skills")
            LoadTextureStack(Tilesets, "Graphics/Tilesets")
        End Sub

        Private Sub DestroyGraphics()
            ' Singles
            UnloadTexture(Blood)
            UnloadTexture(ChatBubble)
            UnloadTexture(Directions)
            UnloadTexture(Door)
            UnloadTexture(Light)
            UnloadTexture(Shadow)
            UnloadTexture(Target)
            UnloadTexture(Weather)

            ' Stacks
            UnloadTextureStack(Animations)
            UnloadTextureStack(Characters)
            UnloadTextureStack(Emotes)
            UnloadTextureStack(Faces)
            UnloadTextureStack(Fogs)
            UnloadTextureStack(Furniture)
            UnloadTextureStack(Items)
            UnloadTextureStack(Npcs)
            UnloadTextureStack(Panoramas)
            UnloadTextureStack(Paperdolls)
            UnloadTextureStack(Parallaxes)
            UnloadTextureStack(Projectiles)
            UnloadTextureStack(Resources)
            UnloadTextureStack(Skills)
            UnloadTextureStack(Tilesets)
        End Sub

#End Region

#Region " Gui "

        ' Singles
        Friend Button_Hover As Texture
        Friend Button_Normal As Texture
        Friend Cursor As Texture

        Friend BarExp As Texture
        Friend BarHp As Texture
        Friend BarMp As Texture
        Friend BarProgress As Texture

        Friend ButtonMinus As Texture
        Friend ButtonPlus As Texture

        Friend PanelAction As Texture
        Friend PanelBank As Texture
        Friend PanelChatBox As Texture
        Friend PanelChatEvent As Texture
        Friend PanelChatInput As Texture
        Friend PanelCharacter As Texture
        Friend PanelCraft As Texture
        Friend PanelDescription As Texture
        Friend PanelHotbar As Texture
        Friend PanelHud As Texture
        Friend PanelInventory As Texture
        Friend PanelMenu As Texture
        Friend PanelPetBar As Texture
        Friend PanelPetStats As Texture
        Friend PanelQuest As Texture
        Friend PanelShop As Texture
        Friend PanelSkill As Texture
        Friend PanelTrade As Texture

        ' Stacks

        Private Sub InitializeGui()
            ' Singles
            LoadTexture(Button_Hover, "Gui/Button_Hover")
            LoadTexture(Button_Normal, "Gui/Button_Normal")
            LoadTexture(Cursor, "Gui/Cursor")

            LoadTexture(BarExp, "Gui/Game/BarExp")
            LoadTexture(BarHp, "Gui/Game/BarHp")
            LoadTexture(BarMp, "Gui/Game/BarMp")
            LoadTexture(BarProgress, "Gui/Game/BarProgress")

            LoadTexture(ButtonMinus, "Gui/Game/ButtonMinus")
            LoadTexture(ButtonPlus, "Gui/Game/ButtonPlus")

            LoadTexture(PanelAction, "Gui/Game/PanelAction")
            LoadTexture(PanelBank, "Gui/Game/PanelBank")
            LoadTexture(PanelChatBox, "Gui/Game/PanelChatBox")
            LoadTexture(PanelChatEvent, "Gui/Game/PanelChatEvent")
            LoadTexture(PanelChatInput, "Gui/Game/PanelChatInput")
            LoadTexture(PanelCharacter, "Gui/Game/PanelCharacter")
            LoadTexture(PanelCraft, "Gui/Game/PanelCraft")
            LoadTexture(PanelDescription, "Gui/Game/PanelDescription")
            LoadTexture(PanelHotbar, "Gui/Game/PanelHotbar")
            LoadTexture(PanelHud, "Gui/Game/PanelHud")
            LoadTexture(PanelInventory, "Gui/Game/PanelInventory")
            LoadTexture(PanelMenu, "Gui/Game/PanelMenu")
            LoadTexture(PanelPetBar, "Gui/Game/PanelPetBar")
            LoadTexture(PanelPetStats, "Gui/Game/PanelPetStats")
            LoadTexture(PanelQuest, "Gui/Game/PanelQuest")
            LoadTexture(PanelShop, "Gui/Game/PanelShop")
            LoadTexture(PanelSkill, "Gui/Game/PanelSkill")
            LoadTexture(PanelTrade, "Gui/Game/PanelTrade")

            ' Stacks
        End Sub

        Private Sub DestroyGui()
            ' Singles
            UnloadTexture(Button_Hover)
            UnloadTexture(Button_Normal)
            UnloadTexture(Cursor)

            UnloadTexture(BarExp)
            UnloadTexture(BarHp)
            UnloadTexture(BarMp)
            UnloadTexture(BarProgress)

            UnloadTexture(ButtonMinus)
            UnloadTexture(ButtonPlus)

            UnloadTexture(PanelAction)
            UnloadTexture(PanelBank)
            UnloadTexture(PanelChatBox)
            UnloadTexture(PanelChatEvent)
            UnloadTexture(PanelChatInput)
            UnloadTexture(PanelCharacter)
            UnloadTexture(PanelCraft)
            UnloadTexture(PanelDescription)
            UnloadTexture(PanelHotbar)
            UnloadTexture(PanelHud)
            UnloadTexture(PanelInventory)
            UnloadTexture(PanelMenu)
            UnloadTexture(PanelPetBar)
            UnloadTexture(PanelPetStats)
            UnloadTexture(PanelQuest)
            UnloadTexture(PanelShop)
            UnloadTexture(PanelSkill)
            UnloadTexture(PanelTrade)

            ' Stacks
        End Sub

#End Region

#Region " Utility "

        Friend Function MapToWorld(position As Vector2f) As Vector2f
            Dim td = TileSize
            Return New Vector2f(position.X * td - Viewport.Left * td - Camera.Left,
                                position.Y * td - Viewport.Top * td - Camera.Top)
        End Function

        Friend Function MapToWorld(x As Single, y As Single) As Vector2f
            Dim td = TileSize
            Return New Vector2f(x * td - Viewport.Left * td - Camera.Left,
                                y * td - Viewport.Top * td - Camera.Top)
        End Function

        Friend Function MapToWorldX(x As Single) As Single
            Dim td = TileSize
            Return x * td - Viewport.Left * td - Camera.Left
        End Function

        Friend Function MapToWorldY(y As Single) As Single
            Dim td = TileSize
            Return y * td - Viewport.Top * td - Camera.Top
        End Function

        Friend Function IsMapPoint(position As Vector2f) As Boolean
            Return Not (position.X < 0 OrElse
                        position.Y < 0 OrElse
                        position.X > Map.MaxX OrElse
                        position.Y > Map.MaxY)
        End Function

        Friend Function IsMapPoint(x As Single, y As Single) As Boolean
            Return Not (x < 0 OrElse
                        y < 0 OrElse
                        x > Map.MaxX OrElse
                        y > Map.MaxY)
        End Function

        'Friend Sub UpdateView()
        '    Dim offset As New Vector2f(Player(Myindex).XOffset,
        '                             Player(Myindex).YOffset)
        '    Dim posTL As New Vector2f(GetPlayerX(Myindex) - (ScreenMapx + 1) \ 2 - 1,
        '                            GetPlayerY(Myindex) - (ScreenMapy + 1) \ 2 - 1)

        '    If posTL.X < 0 Then
        '        If Not (posTL.X = -1 AndAlso offset.X > 0) Then
        '            offset.X = 0
        '        End If : posTL.X = 0
        '    End If

        '    If posTL.Y < 0 Then
        '        If Not (posTL.Y = -1 AndAlso offset.Y > 0) Then
        '            offset.Y = 0
        '        End If : posTL.Y = 0
        '    End If

        '    Dim posBR As New Vector2f(posTL.X + (ScreenMapx + 1) + 1,
        '                            posTL.Y + (ScreenMapy + 1) + 1)

        '    If posBR.X > Map.MaxX Then
        '        If posBR.X = Map.MaxX + 1 AndAlso offset.X < 0 Then
        '            offset.X += PicX
        '        Else
        '            offset.X = PicX
        '        End If

        '        posBR.X = Map.MaxX
        '        posTL.X = posBR.X - ScreenMapx - 1
        '    End If

        '    If posBR.Y > Map.MaxY Then
        '        If posBR.Y = Map.MaxY + 1 AndAlso offset.Y < 0 Then
        '            offset.Y += PicY
        '        Else
        '            offset.Y = PicY
        '        End If

        '        posBR.Y = Map.MaxY
        '        posTL.Y = posBR.Y - ScreenMapy - 1
        '    End If

        '    Camera = New Rectangle(offset.X, offset.Y,
        '                           ScreenX + PicX,
        '                           ScreenY + PicY)

        '    Viewport = New Rectangle(posTL.X, posTL.Y,
        '                             posBR.X - posTL.X,
        '                             posBR.Y - posTL.Y)
        'End Sub

#End Region

    End Module
End Namespace