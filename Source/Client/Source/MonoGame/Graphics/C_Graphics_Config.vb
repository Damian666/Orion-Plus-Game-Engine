Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Winforms
Imports System.IO

Namespace Graphics
    Friend Module modConfig
        Friend Batcher As SpriteBatch ' Used globally to render
        Friend TexCache As Texture2D ' Used to replace broken textures
        Friend TexPixel As Texture2D ' Used in rendering primitives
        Friend Font_Default As SpriteFont

        Friend Camera As Rectangle
        Friend Viewport As Rectangle

        Friend Sub Initialize()
            Batcher = New SpriteBatch(UniversalBackend.GraphicsDevice)
            TexCache = CreateTexture(PicX, PicY, Color.HotPink)
            TexCache = CreateTexture(1, 1, Color.White)

            InitializeGraphics()
            InitializeGui()
        End Sub

        Friend Sub Destroy()
            Batcher.Dispose() : Batcher = Nothing
            TexCache.Dispose() : TexCache = Nothing
            TexPixel.Dispose() : TexPixel = Nothing

            DestroyGraphics()
            DestroyGui()
        End Sub

#Region " API "

        Public Function CreateTexture(width As Integer, height As Integer, color As Color) As Texture2D
            Dim size = width * height
            Dim pixels(size) As Color
            For p = 0 To size
                pixels(p) = color
            Next

            Dim texture = New Texture2D(UniversalBackend.GraphicsDevice, width, height)
            texture.SetData(pixels)
            Return texture
        End Function

        Private Sub LoadTexture(ByRef texture As Texture2D, filePath As String)
            If Not File.Exists(Path.Contents & filePath) Then Return
            texture = UniversalBackend.Content.Load(Of Texture2D)(filePath)
        End Sub

        Private Sub LoadTextureStack(ByRef textures() As Texture2D, dirPath As String)
            Dim cd = Path.Contents & dirPath & "/"
            If Not Directory.Exists(cd) Then Return
            Dim fis = New DirectoryInfo(cd).GetFiles("*.xnb")

            ' Begin setting up count
            Dim count = 0
            Dim index = 0
            For Each fi In fis
                index = Val(fi.Name.Substring(0, fi.Name.Length - 4))
                If index > count Then count = index
            Next : count += 1

            ' Setup textures
            ReDim textures(count)
            For i = 0 To count
                If File.Exists(cd & i & ".xnb") Then
                    textures(i) = UniversalBackend.Content.Load(
                                    Of Texture2D)(dirPath & "/" & i)
                Else
                    textures(i) = TexCache
                End If
            Next
        End Sub

        Private Sub UnloadTexture(ByRef texture As Texture2D)
            texture.Dispose()
            texture = Nothing
        End Sub

        Private Sub UnloadTextureStack(ByRef textures() As Texture2D)
            For i = 0 To UBound(textures)
                textures(i).Dispose()
            Next : textures = Nothing
        End Sub

        Friend Sub RenderTexture(texture As Texture2D, destination As Vector2)
            If texture Is Nothing Then Exit Sub
            Batcher.Draw(texture, destination, Color.White)
        End Sub

        Friend Sub RenderTexture(texture As Texture2D, destination As Vector2, color As Color)
            If texture Is Nothing Then Exit Sub
            Batcher.Draw(texture, destination, color)
        End Sub

        Friend Sub RenderTexture(texture As Texture2D, destination As Vector2, source As Rectangle)
            If texture Is Nothing Then Exit Sub
            Batcher.Draw(texture, destination, source, Color.White)
        End Sub

        Friend Sub RenderTexture(texture As Texture2D, destination As Vector2,
                                    source As Rectangle, color As Color)
            If texture Is Nothing Then Exit Sub
            Batcher.Draw(texture, destination, source, color)
        End Sub

        Friend Sub RenderTexture(texture As Texture2D, destination As Rectangle)
            If texture Is Nothing Then Exit Sub
            Batcher.Draw(texture, destination, Color.White)
        End Sub

        Friend Sub RenderTexture(texture As Texture2D, destination As Rectangle, color As Color)
            If texture Is Nothing Then Exit Sub
            Batcher.Draw(texture, destination, color)
        End Sub

        Friend Sub RenderTexture(texture As Texture2D, destination As Rectangle, source As Rectangle)
            If texture Is Nothing Then Exit Sub
            Batcher.Draw(texture, destination, source, Color.White)
        End Sub

        Friend Sub RenderTexture(texture As Texture2D, destination As Rectangle,
                                    source As Rectangle, color As Color)
            If texture Is Nothing Then Exit Sub
            Batcher.Draw(texture, destination, source, color)
        End Sub

        Friend Sub RenderText(text As String, position As Vector2)
            If Font_Default Is Nothing Then Exit Sub
            Batcher.DrawString(Font_Default, text, position, Color.Black)
        End Sub

        Friend Sub RenderText(text As String, position As Vector2, color As Color)
            If Font_Default Is Nothing Then Exit Sub
            Batcher.DrawString(Font_Default, text, position, Color.Black)
        End Sub

        Friend Sub RenderText(font As SpriteFont, text As String, position As Vector2)
            If font Is Nothing Then Exit Sub
            Batcher.DrawString(font, text, position, Color.Black)
        End Sub

        Friend Sub RenderText(font As SpriteFont, text As String, position As Vector2, color As Color)
            If font Is Nothing Then Exit Sub
            Batcher.DrawString(font, text, position, color)
        End Sub

        Friend Sub RenderRectangle(destination As Rectangle, thickness As Integer, border As Color)
            Dim l1 = New Rectangle(destination.X, destination.Y,
                                   destination.Width, thickness)

            Dim l2 = New Rectangle(destination.X, destination.Y,
                                   thickness, destination.Height)

            Dim l3 = New Rectangle(destination.Width - thickness, destination.Y,
                                   thickness, destination.Height)

            Dim l4 = New Rectangle(destination.X, destination.Height - thickness,
                                   destination.Width, thickness)

            Batcher.Draw(TexPixel, l1, border)
            Batcher.Draw(TexPixel, l2, border)
            Batcher.Draw(TexPixel, l3, border)
            Batcher.Draw(TexPixel, l4, border)
        End Sub

        Friend Sub RenderRectangle(destination As Rectangle, thickness As Integer,
                                   border As Color, fill As Color)
            Dim l1 = New Rectangle(destination.X, destination.Y,
                                   destination.Width, thickness)

            Dim l2 = New Rectangle(destination.X, destination.Y,
                                   thickness, destination.Height)

            Dim l3 = New Rectangle(destination.Width - thickness, destination.Y,
                                   thickness, destination.Height)

            Dim l4 = New Rectangle(destination.X, destination.Height - thickness,
                                   destination.Width, thickness)

            Dim l5 = New Rectangle(destination.X + thickness,
                                   destination.Y + thickness,
                                   destination.Width - thickness * 2,
                                   destination.Height - thickness * 2)

            Batcher.Draw(TexPixel, l1, border)
            Batcher.Draw(TexPixel, l2, border)
            Batcher.Draw(TexPixel, l3, border)
            Batcher.Draw(TexPixel, l4, border)
            Batcher.Draw(TexPixel, l5, fill)
        End Sub

#End Region

#Region " Graphics "

        ' Singles
        Friend Blood As Texture2D
        Friend ChatBubble As Texture2D
        Friend Directions As Texture2D
        Friend Door As Texture2D
        Friend Shadow As Texture2D
        Friend Target As Texture2D
        Friend Weather As Texture2D

        ' Stacks
        Friend Animations() As Texture2D
        Friend Characters() As Texture2D
        Friend Emotes() As Texture2D
        Friend Faces() As Texture2D
        Friend Fogs() As Texture2D
        Friend Furniture() As Texture2D
        Friend Items() As Texture2D
        Friend Npcs() As Texture2D
        Friend Panoramas() As Texture2D
        Friend Paperdolls() As Texture2D
        Friend Parallaxes() As Texture2D
        Friend Projectiles() As Texture2D
        Friend Resources() As Texture2D
        Friend Skills() As Texture2D
        Friend Tilesets() As Texture2D

        Private Sub InitializeGraphics()
            ' Singles
            LoadTexture(Blood, "Graphics/Misc/Blood")
            LoadTexture(ChatBubble, "Graphics/Misc/ChatBubble")
            LoadTexture(Directions, "Graphics/Misc/Directions")
            LoadTexture(Door, "Graphics/Misc/Door")
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
        Friend Button_Hover As Texture2D
        Friend Button_Normal As Texture2D
        Friend Cursor As Texture2D

        Friend BarExp As Texture2D
        Friend BarHp As Texture2D
        Friend BarMp As Texture2D
        Friend BarProgress As Texture2D

        Friend ButtonMinus As Texture2D
        Friend ButtonPlus As Texture2D

        Friend PanelAction As Texture2D
        Friend PanelBank As Texture2D
        Friend PanelChatBox As Texture2D
        Friend PanelChatEvent As Texture2D
        Friend PanelChatInput As Texture2D
        Friend PanelCharacter As Texture2D
        Friend PanelCraft As Texture2D
        Friend PanelDescription As Texture2D
        Friend PanelHotbar As Texture2D
        Friend PanelHud As Texture2D
        Friend PanelInventory As Texture2D
        Friend PanelMenu As Texture2D
        Friend PanelPetBar As Texture2D
        Friend PanelPetStats As Texture2D
        Friend PanelQuest As Texture2D
        Friend PanelShop As Texture2D
        Friend PanelSkill As Texture2D
        Friend PanelTrade As Texture2D

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

        Friend Function ConvertMapX(x As Integer) As Integer
            Return x - (Viewport.Left * PicX) - Camera.Left
        End Function

        Friend Function ConvertMapY(y As Integer) As Integer
            Return y - (Viewport.Top * PicY) - Camera.Top
        End Function

        Friend Function IsValidMapPoint(x As Integer, y As Integer) As Boolean
            Return Not (x < 0 OrElse y < 0 OrElse x > Map.MaxX OrElse y > Map.MaxY)
        End Function

        Friend Sub UpdateView()
            Dim offset = New Vector2(Player(Myindex).XOffset,
                                     Player(Myindex).YOffset)
            Dim posTL = New Vector2(GetPlayerX(Myindex) - (ScreenMapx + 1) \ 2 - 1,
                                    GetPlayerY(Myindex) - (ScreenMapy + 1) \ 2 - 1)

            If posTL.X < 0 Then
                If Not (posTL.X = -1 AndAlso offset.X > 0) Then
                    offset.X = 0
                End If : posTL.X = 0
            End If

            If posTL.Y < 0 Then
                If Not (posTL.Y = -1 AndAlso offset.Y > 0) Then
                    offset.Y = 0
                End If : posTL.Y = 0
            End If

            Dim posBR = New Vector2(posTL.X + (ScreenMapx + 1) + 1,
                                    posTL.Y + (ScreenMapy + 1) + 1)

            If posBR.X > Map.MaxX Then
                If posBR.X = Map.MaxX + 1 AndAlso offset.X < 0 Then
                    offset.X += PicX
                Else
                    offset.X = PicX
                End If

                posBR.X = Map.MaxX
                posTL.X = posBR.X - ScreenMapx - 1
            End If

            If posBR.Y > Map.MaxY Then
                If posBR.Y = Map.MaxY + 1 AndAlso offset.Y < 0 Then
                    offset.Y += PicY
                Else
                    offset.Y = PicY
                End If

                posBR.Y = Map.MaxY
                posTL.Y = posBR.Y - ScreenMapy - 1
            End If

            Camera = New Rectangle(offset.X, offset.Y,
                                   ScreenX + PicX,
                                   ScreenY + PicY)

            Viewport = New Rectangle(posTL.X, posTL.Y,
                                     posBR.X - posTL.X,
                                     posBR.Y - posTL.Y)
        End Sub

#End Region

    End Module
End Namespace