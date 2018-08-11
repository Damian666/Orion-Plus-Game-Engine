Imports System.IO
Imports SFML.Graphics
Imports SFML.Window

Module E_Graphics
    Friend GameWindow As RenderWindow
    Friend TilesetWindow As RenderWindow

    Friend EditorItem_Furniture As RenderWindow

    Friend EditorSkill_Icon As RenderWindow

    Friend EditorAnimation_Anim1 As RenderWindow
    Friend EditorAnimation_Anim2 As RenderWindow

    Friend TmpItemWindow As RenderWindow

    Friend SFMLGameFont As Font

    'TileSets
    Friend TileSetTexture() As Texture
    Friend TileSetSprite() As Sprite
    Friend TileSetTextureInfo() As GraphicInfo
    'Characters
    Friend CharacterGFX() As Texture
    Friend CharacterSprite() As Sprite
    Friend CharacterGFXInfo() As GraphicInfo
    'Paperdolls
    Friend PaperDollGFX() As Texture
    Friend PaperDollSprite() As Sprite
    Friend PaperDollGFXInfo() As GraphicInfo
    'Items
    Friend ItemsGFX() As Texture
    Friend ItemsSprite() As Sprite
    Friend ItemsGFXInfo() As GraphicInfo
    'Resources
    Friend ResourcesGFX() As Texture
    Friend ResourcesSprite() As Sprite
    Friend ResourcesGFXInfo() As GraphicInfo
    'Animations
    Friend AnimationsGFX() As Texture
    Friend AnimationsSprite() As Sprite
    Friend AnimationsGFXInfo() As GraphicInfo
    'Skills
    Friend SkillIconsGFX() As Texture
    Friend SkillIconsSprite() As Sprite
    Friend SkillIconsGFXInfo() As GraphicInfo
    'Housing
    Friend FurnitureGFX() As Texture
    Friend FurnitureSprite() As Sprite
    Friend FurnitureGFXInfo() As GraphicInfo
    'Faces
    Friend FacesGFX() As Texture
    Friend FacesSprite() As Sprite
    Friend FacesGFXInfo() As GraphicInfo
    'Projectiles
    Friend ProjectileGFX() As Texture
    Friend ProjectileSprite() As Sprite
    Friend ProjectileGFXInfo() As GraphicInfo
    'Fogs
    Friend FogGFX() As Texture
    Friend FogSprite() As Sprite
    Friend FogGFXInfo() As GraphicInfo
    'Door
    Friend DoorGFX As Texture
    Friend DoorSprite As Sprite
    Friend DoorGFXInfo As GraphicInfo
    'Directions
    Friend DirectionsGfx As Texture
    Friend DirectionsSprite As Sprite
    Friend DirectionsGFXInfo As GraphicInfo
    'Weather
    Friend WeatherGFX As Texture
    Friend WeatherSprite As Sprite
    Friend WeatherGFXInfo As GraphicInfo

    ' Number of graphic files
    Friend MapEditorBackBuffer As Bitmap

    Friend MapTintSprite As Sprite

    Friend NumTileSets As Integer
    Friend NumCharacters As Integer
    Friend NumPaperdolls As Integer
    Friend NumItems As Integer
    Friend NumResources As Integer
    Friend NumAnimations As Integer
    Friend NumSkillIcons As Integer
    Friend NumFaces As Integer
    Friend NumFogs As Integer

    Friend NightGfx As New RenderTexture(245, 245)
    Friend NightSprite As Sprite
    Friend NightGfxInfo As GraphicInfo

    Friend LightGfx As Texture
    Friend LightSprite As Sprite
    Friend LightGfxInfo As GraphicInfo

    Friend Structure GraphicInfo
        Dim width As Integer
        Dim height As Integer
        Dim IsLoaded As Boolean
        Dim TextureTimer As Integer
    End Structure

    Friend Structure Graphics_Tiles
        Dim Tile(,) As Texture
    End Structure

    Sub InitGraphics()

        GameWindow.SetFramerateLimit(FPS_LIMIT)
        EditorItem_Furniture = New RenderWindow(frmItem.picFurniture.Handle)

        EditorSkill_Icon = New RenderWindow(frmSkill.picSprite.Handle)

        EditorAnimation_Anim1 = New RenderWindow(FrmAnimation.picSprite0.Handle)
        EditorAnimation_Anim2 = New RenderWindow(FrmAnimation.picSprite1.Handle)

        SFMLGameFont = New Font(Environment.GetFolderPath(Environment.SpecialFolder.Fonts) + "\" + FONT_NAME)

        'this stuff only loads when needed :)

        ReDim TileSetTexture(NumTileSets)
        ReDim TileSetSprite(NumTileSets)
        ReDim TileSetTextureInfo(NumTileSets)

        ReDim CharacterGFX(NumCharacters)
        ReDim CharacterSprite(NumCharacters)
        ReDim CharacterGFXInfo(NumCharacters)

        ReDim PaperDollGFX(NumPaperdolls)
        ReDim PaperDollSprite(NumPaperdolls)
        ReDim PaperDollGFXInfo(NumPaperdolls)

        ReDim ItemsGFX(NumItems)
        ReDim ItemsSprite(NumItems)
        ReDim ItemsGFXInfo(NumItems)

        ReDim ResourcesGFX(NumResources)
        ReDim ResourcesSprite(NumResources)
        ReDim ResourcesGFXInfo(NumResources)

        ReDim AnimationsGFX(NumAnimations)
        ReDim AnimationsSprite(NumAnimations)
        ReDim AnimationsGFXInfo(NumAnimations)

        ReDim SkillIconsGFX(NumSkillIcons)
        ReDim SkillIconsSprite(NumSkillIcons)
        ReDim SkillIconsGFXInfo(NumSkillIcons)

        ReDim FacesGFX(NumFaces)
        ReDim FacesSprite(NumFaces)
        ReDim FacesGFXInfo(NumFaces)

        ReDim FurnitureGFX(NumFurniture)
        ReDim FurnitureSprite(NumFurniture)
        ReDim FurnitureGFXInfo(NumFurniture)

        ReDim ProjectileGFX(NumProjectiles)
        ReDim ProjectileSprite(NumProjectiles)
        ReDim ProjectileGFXInfo(NumProjectiles)

        ReDim FogGFX(NumFogs)
        ReDim FogSprite(NumFogs)
        ReDim FogGFXInfo(NumFogs)

        'sadly, gui shit is always needed, so we preload it :/
        DoorGFXInfo = New GraphicInfo
        If File.Exists(Application.StartupPath & GFX_PATH & "Misc\Door" & GFX_EXT) Then
            'Load texture first, dont care about memory streams (just use the filename)
            DoorGFX = New Texture(Application.StartupPath & GFX_PATH & "Misc\Door" & GFX_EXT)
            DoorSprite = New Sprite(DoorGFX)

            'Cache the width and height
            DoorGFXInfo.width = DoorGFX.Size.X
            DoorGFXInfo.height = DoorGFX.Size.Y
        End If

        DirectionsGFXInfo = New GraphicInfo
        If File.Exists(Application.StartupPath & GFX_PATH & "Misc\Direction" & GFX_EXT) Then
            'Load texture first, dont care about memory streams (just use the filename)
            DirectionsGfx = New Texture(Application.StartupPath & GFX_PATH & "Misc\Direction" & GFX_EXT)
            DirectionsSprite = New Sprite(DirectionsGfx)

            'Cache the width and height
            DirectionsGFXInfo.width = DirectionsGfx.Size.X
            DirectionsGFXInfo.height = DirectionsGfx.Size.Y
        End If

        WeatherGFXInfo = New GraphicInfo
        If File.Exists(Application.StartupPath & GFX_PATH & "Misc\Weather" & GFX_EXT) Then
            'Load texture first, dont care about memory streams (just use the filename)
            WeatherGFX = New Texture(Application.StartupPath & GFX_PATH & "Misc\Weather" & GFX_EXT)
            WeatherSprite = New Sprite(WeatherGFX)

            'Cache the width and height
            WeatherGFXInfo.width = WeatherGFX.Size.X
            WeatherGFXInfo.height = WeatherGFX.Size.Y
        End If

        LightGfxInfo = New GraphicInfo
        If File.Exists(Application.StartupPath & GFX_PATH & "Misc\Light" & GFX_EXT) Then
            LightGfx = New Texture(Application.StartupPath & GFX_PATH & "Misc\Light" & GFX_EXT)
            LightSprite = New Sprite(LightGfx)

            'Cache the width and height
            LightGfxInfo.width = LightGfx.Size.X
            LightGfxInfo.height = LightGfx.Size.Y
        End If

    End Sub

    Friend Sub LoadTexture(index as integer, TexType As Byte)

        If TexType = 1 Then 'tilesets
            If Index <= 0 OrElse Index > NumTileSets Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            TileSetTexture(Index) = New Texture(Application.StartupPath & GFX_PATH & "tilesets\" & Index & GFX_EXT)
            TileSetSprite(Index) = New Sprite(TileSetTexture(Index))

            'Cache the width and height
            With TileSetTextureInfo(Index)
                .width = TileSetTexture(Index).Size.X
                .height = TileSetTexture(Index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With

        ElseIf TexType = 2 Then 'characters
            If Index <= 0 OrElse Index > NumCharacters Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            CharacterGFX(Index) = New Texture(Application.StartupPath & GFX_PATH & "characters\" & Index & GFX_EXT)
            CharacterSprite(Index) = New Sprite(CharacterGFX(Index))

            'Cache the width and height
            With CharacterGFXInfo(Index)
                .width = CharacterGFX(Index).Size.X
                .height = CharacterGFX(Index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With

        ElseIf TexType = 3 Then 'paperdoll
            If Index <= 0 OrElse Index > NumPaperdolls Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            PaperDollGFX(Index) = New Texture(Application.StartupPath & GFX_PATH & "Paperdolls\" & Index & GFX_EXT)
            PaperDollSprite(Index) = New Sprite(PaperDollGFX(Index))

            'Cache the width and height
            With PaperDollGFXInfo(Index)
                .width = PaperDollGFX(Index).Size.X
                .height = PaperDollGFX(Index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With

        ElseIf TexType = 4 Then 'items
            If Index <= 0 OrElse Index > NumItems Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            ItemsGFX(Index) = New Texture(Application.StartupPath & GFX_PATH & "items\" & Index & GFX_EXT)
            ItemsSprite(Index) = New Sprite(ItemsGFX(Index))

            'Cache the width and height
            With ItemsGFXInfo(Index)
                .width = ItemsGFX(Index).Size.X
                .height = ItemsGFX(Index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With

        ElseIf TexType = 5 Then 'resources
            If Index <= 0 OrElse Index > NumResources Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            ResourcesGFX(Index) = New Texture(Application.StartupPath & GFX_PATH & "resources\" & Index & GFX_EXT)
            ResourcesSprite(Index) = New Sprite(ResourcesGFX(Index))

            'Cache the width and height
            With ResourcesGFXInfo(Index)
                .width = ResourcesGFX(Index).Size.X
                .height = ResourcesGFX(Index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With

        ElseIf TexType = 6 Then 'animations
            If Index <= 0 OrElse Index > NumAnimations Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            AnimationsGFX(Index) = New Texture(Application.StartupPath & GFX_PATH & "Animations\" & Index & GFX_EXT)
            AnimationsSprite(Index) = New Sprite(AnimationsGFX(Index))

            'Cache the width and height
            With AnimationsGFXInfo(Index)
                .width = AnimationsGFX(Index).Size.X
                .height = AnimationsGFX(Index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With

        ElseIf TexType = 7 Then 'faces
            If Index <= 0 OrElse Index > NumFaces Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            FacesGFX(Index) = New Texture(Application.StartupPath & GFX_PATH & "Faces\" & Index & GFX_EXT)
            FacesSprite(Index) = New Sprite(FacesGFX(Index))

            'Cache the width and height
            With FacesGFXInfo(Index)
                .width = FacesGFX(Index).Size.X
                .height = FacesGFX(Index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With

        ElseIf TexType = 8 Then 'fogs
            If Index <= 0 OrElse Index > NumFogs Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            FogGFX(Index) = New Texture(Application.StartupPath & GFX_PATH & "Fogs\" & Index & GFX_EXT)
            FogSprite(Index) = New Sprite(FogGFX(Index))

            'Cache the width and height
            With FogGFXInfo(Index)
                .width = FogGFX(Index).Size.X
                .height = FogGFX(Index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With

        ElseIf TexType = 9 Then 'skill icons
            If Index <= 0 OrElse Index > NumSkillIcons Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            SkillIconsGFX(Index) = New Texture(Application.StartupPath & GFX_PATH & "SkillIcons\" & Index & GFX_EXT)
            SkillIconsSprite(Index) = New Sprite(SkillIconsGFX(Index))

            'Cache the width and height
            With SkillIconsGFXInfo(Index)
                .width = SkillIconsGFX(Index).Size.X
                .height = SkillIconsGFX(Index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With
        ElseIf TexType = 10 Then 'furniture
            If Index <= 0 OrElse Index > NumFurniture Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            FurnitureGFX(Index) = New Texture(Application.StartupPath & GFX_PATH & "Furniture\" & Index & GFX_EXT)
            FurnitureSprite(Index) = New Sprite(FurnitureGFX(Index))

            'Cache the width and height
            With FurnitureGFXInfo(Index)
                .width = FurnitureGFX(Index).Size.X
                .height = FurnitureGFX(Index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With
        ElseIf TexType = 11 Then 'projectiles
            If Index <= 0 OrElse Index > NumProjectiles Then Exit Sub

            'Load texture first, dont care about memory streams (just use the filename)
            ProjectileGFX(Index) = New Texture(Application.StartupPath & GFX_PATH & "Projectiles\" & Index & GFX_EXT)
            ProjectileSprite(Index) = New Sprite(ProjectileGFX(Index))

            'Cache the width and height
            With ProjectileGFXInfo(Index)
                .width = ProjectileGFX(Index).Size.X
                .height = ProjectileGFX(Index).Size.Y
                .IsLoaded = True
                .TextureTimer = GetTickCount() + 100000
            End With
        End If

    End Sub

    Friend Sub RenderSprite(TmpSprite As Sprite, Target As RenderWindow, DestX As Integer, DestY As Integer, SourceX As Integer, SourceY As Integer,
           SourceWidth As Integer, SourceHeight As Integer)

        TmpSprite.TextureRect = New IntRect(SourceX, SourceY, SourceWidth, SourceHeight)
        TmpSprite.Position = New Vector2f(DestX, DestY)
        Target.Draw(TmpSprite)
    End Sub
    
    Friend Function ConvertMapX(X As Integer) As Integer
        ConvertMapX = X - (TileView.Left * PIC_X) - Camera.Left
    End Function

    Friend Function ConvertMapY(Y As Integer) As Integer
        ConvertMapY = Y - (TileView.Top * PIC_Y) - Camera.Top
    End Function

    Friend Sub DrawResource(Resource As Integer, dx As Integer, dy As Integer, rec As Rectangle)
        If Resource < 1 OrElse Resource > NumResources Then Exit Sub
        Dim X As Integer
        Dim Y As Integer
        Dim width As Integer
        Dim height As Integer

        X = ConvertMapX(dx)
        Y = ConvertMapY(dy)
        width = (rec.Right - rec.Left)
        height = (rec.Bottom - rec.Top)

        If rec.Width < 0 OrElse rec.Height < 0 Then Exit Sub

        If ResourcesGFXInfo(Resource).IsLoaded = False Then
            LoadTexture(Resource, 5)
        End If

        'seeying we still use it, lets update timer
        With ResourcesGFXInfo(Resource)
            .TextureTimer = GetTickCount() + 100000
        End With

        RenderSprite(ResourcesSprite(Resource), GameWindow, X, Y, rec.X, rec.Y, rec.Width, rec.Height)
    End Sub

    Friend Sub DrawCharacter(Sprite As Integer, x2 As Integer, y2 As Integer, rec As Rectangle)
        Dim X As Integer
        Dim y As Integer
        Dim width As Integer
        Dim height As Integer
        On Error Resume Next

        If Sprite < 1 OrElse Sprite > NumCharacters Then Exit Sub

        If CharacterGFXInfo(Sprite).IsLoaded = False Then
            LoadTexture(Sprite, 2)
        End If

        'seeying we still use it, lets update timer
        With CharacterGFXInfo(Sprite)
            .TextureTimer = GetTickCount() + 100000
        End With

        X = ConvertMapX(x2)
        y = ConvertMapY(y2)
        width = (rec.Width)
        height = (rec.Height)

        RenderSprite(CharacterSprite(Sprite), GameWindow, X, y, rec.X, rec.Y, rec.Width, rec.Height)
    End Sub

    Sub ClearGFX()

        'clear tilesets
        For I = 1 To NumTileSets
            If TileSetTextureInfo(I).IsLoaded Then
                If TileSetTextureInfo(I).TextureTimer < GetTickCount() Then
                    TileSetTexture(I).Dispose()
                    TileSetTextureInfo(I).IsLoaded = False
                    TileSetTextureInfo(I).TextureTimer = 0
                End If
            End If
        Next

        'clear characters
        For I = 1 To NumCharacters
            If CharacterGFXInfo(I).IsLoaded Then
                If CharacterGFXInfo(I).TextureTimer < GetTickCount() Then
                    CharacterGFX(I).Dispose()
                    CharacterGFXInfo(I).IsLoaded = False
                    CharacterGFXInfo(I).TextureTimer = 0
                End If
            End If
        Next

        'clear paperdoll
        For I = 1 To NumPaperdolls
            If PaperDollGFXInfo(I).IsLoaded Then
                If PaperDollGFXInfo(I).TextureTimer < GetTickCount() Then
                    PaperDollGFX(I).Dispose()
                    PaperDollGFXInfo(I).IsLoaded = False
                    PaperDollGFXInfo(I).TextureTimer = 0
                End If
            End If
        Next

        'clear items
        For I = 1 To NumItems
            If ItemsGFXInfo(I).IsLoaded Then
                If ItemsGFXInfo(I).TextureTimer < GetTickCount() Then
                    ItemsGFX(I).Dispose()
                    ItemsGFXInfo(I).IsLoaded = False
                    ItemsGFXInfo(I).TextureTimer = 0
                End If
            End If
        Next

        'clear resources
        For I = 1 To NumResources
            If ResourcesGFXInfo(I).IsLoaded Then
                If ResourcesGFXInfo(I).TextureTimer < GetTickCount() Then
                    ResourcesGFX(I).Dispose()
                    ResourcesGFXInfo(I).IsLoaded = False
                    ResourcesGFXInfo(I).TextureTimer = 0
                End If
            End If
        Next

        'clear faces
        For I = 1 To NumFaces
            If FacesGFXInfo(I).IsLoaded Then
                If FacesGFXInfo(I).TextureTimer < GetTickCount() Then
                    FacesGFX(I).Dispose()
                    FacesGFXInfo(I).IsLoaded = False
                    FacesGFXInfo(I).TextureTimer = 0
                End If
            End If
        Next
    End Sub
    
    Sub DestroyGraphics()

        ' Number of graphic files
        If Not MapEditorBackBuffer Is Nothing Then MapEditorBackBuffer.Dispose()

        For i = 0 To NumAnimations
            If Not AnimationsGFX(i) Is Nothing Then AnimationsGFX(i).Dispose()
        Next i

        For i = 0 To NumCharacters
            If Not CharacterGFX(i) Is Nothing Then CharacterGFX(i).Dispose()
        Next

        For i = 0 To NumItems
            If Not ItemsGFX(i) Is Nothing Then ItemsGFX(i).Dispose()
        Next

        For i = 0 To NumPaperdolls
            If Not PaperDollGFX(i) Is Nothing Then PaperDollGFX(i).Dispose()
        Next

        For i = 0 To NumResources
            If Not ResourcesGFX(i) Is Nothing Then ResourcesGFX(i).Dispose()
        Next

        For i = 0 To NumSkillIcons
            If Not SkillIconsGFX(i) Is Nothing Then SkillIconsGFX(i).Dispose()
        Next

        For i = 0 To NumTileSets
            'If Not TileSetImgsGFX(i) Is Nothing Then TileSetImgsGFX(i).Dispose()
            If Not TileSetTexture(i) Is Nothing Then TileSetTexture(i).Dispose()
        Next i

        For i = 0 To NumFurniture
            If Not FurnitureGFX(i) Is Nothing Then FurnitureGFX(i).Dispose()
        Next

        For i = 0 To NumFaces
            If Not FacesGFX(i) Is Nothing Then FacesGFX(i).Dispose()
        Next

        For i = 0 To NumFogs
            If Not FogGFX(i) Is Nothing Then FogGFX(i).Dispose()
        Next

        If Not DoorGFX Is Nothing Then DoorGFX.Dispose()
        If Not DirectionsGfx Is Nothing Then DirectionsGfx.Dispose()
        If Not WeatherGFX Is Nothing Then WeatherGFX.Dispose()

    End Sub
    
    Friend Sub EditorNpc_DrawSprite()
        Dim Sprite As Integer

        Sprite = frmNPC.nudSprite.Value

        If Sprite < 1 OrElse Sprite > NumCharacters Then
            frmNPC.picSprite.BackgroundImage = Nothing
            Exit Sub
        End If

        If File.Exists(Application.StartupPath & GFX_PATH & "characters\" & Sprite & GFX_EXT) Then
            frmNPC.picSprite.Width = Drawing.Image.FromFile(Application.StartupPath & GFX_PATH & "characters\" & Sprite & GFX_EXT).Width / 4
            frmNPC.picSprite.Height = Drawing.Image.FromFile(Application.StartupPath & GFX_PATH & "characters\" & Sprite & GFX_EXT).Height / 4
            frmNPC.picSprite.BackgroundImage = Drawing.Image.FromFile(Application.StartupPath & GFX_PATH & "characters\" & Sprite & GFX_EXT)
        End If
    End Sub

    Friend Sub EditorResource_DrawSprite()
        Dim Sprite As Integer

        ' normal sprite
        Sprite = frmResource.nudNormalPic.Value

        If Sprite < 1 OrElse Sprite > NumResources Then
            frmResource.picNormalpic.BackgroundImage = Nothing
        Else
            If File.Exists(Application.StartupPath & GFX_PATH & "resources\" & Sprite & GFX_EXT) Then
                frmResource.picNormalpic.BackgroundImage = Drawing.Image.FromFile(Application.StartupPath & GFX_PATH & "resources\" & Sprite & GFX_EXT)
            End If
        End If

        ' exhausted sprite
        Sprite = frmResource.nudExhaustedPic.Value

        If Sprite < 1 OrElse Sprite > NumResources Then
            frmResource.picExhaustedPic.BackgroundImage = Nothing
        Else
            If File.Exists(Application.StartupPath & GFX_PATH & "resources\" & Sprite & GFX_EXT) Then
                frmResource.picExhaustedPic.BackgroundImage = Drawing.Image.FromFile(Application.StartupPath & GFX_PATH & "resources\" & Sprite & GFX_EXT)
            End If
        End If
    End Sub

    Friend Sub EditorSkill_BltIcon()
        Dim iconnum As Integer
        Dim sRECT As Rectangle
        Dim dRECT As Rectangle
        iconnum = frmSkill.nudIcon.Value

        If iconnum < 1 OrElse iconnum > NumSkillIcons Then
            EditorSkill_Icon.Clear(ToSFMLColor(frmSkill.picSprite.BackColor))
            EditorSkill_Icon.Display()
            Exit Sub
        End If

        If SkillIconsGFXInfo(iconnum).IsLoaded = False Then
            LoadTexture(iconnum, 9)
        End If

        'seeying we still use it, lets update timer
        With SkillIconsGFXInfo(iconnum)
            .TextureTimer = GetTickCount() + 100000
        End With

        With sRECT
            .Y = 0
            .Height = PIC_Y
            .X = 0
            .Width = PIC_X
        End With

        'drect is the same, so just copy it
        dRECT = sRECT

        EditorSkill_Icon.Clear(ToSFMLColor(frmSkill.picSprite.BackColor))

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

        Animationnum = FrmAnimation.nudSprite0.Value

        If Animationnum < 1 OrElse Animationnum > NumAnimations Then
            EditorAnimation_Anim1.Clear(ToSFMLColor(FrmAnimation.picSprite0.BackColor))
            EditorAnimation_Anim1.Display()
        Else
            If AnimationsGFXInfo(Animationnum).IsLoaded = False Then
                LoadTexture(Animationnum, 6)
            End If

            'seeying we still use it, lets update timer
            With AnimationsGFXInfo(Animationnum)
                .TextureTimer = GetTickCount() + 100000
            End With

            looptime = FrmAnimation.nudLoopTime0.Value
            FrameCount = FrmAnimation.nudFrameCount0.Value

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
                If FrmAnimation.nudFrameCount0.Value > 0 Then
                    ' total width divided by frame count
                    height = AnimationsGFXInfo(Animationnum).height
                    width = AnimationsGFXInfo(Animationnum).width / FrmAnimation.nudFrameCount0.Value

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

                    EditorAnimation_Anim1.Clear(ToSFMLColor(FrmAnimation.picSprite0.BackColor))

                    RenderSprite(AnimationsSprite(Animationnum), EditorAnimation_Anim1, dRECT.X, dRECT.Y, sRECT.X, sRECT.Y, sRECT.Width, sRECT.Height)

                    EditorAnimation_Anim1.Display()
                End If
            End If
        End If

        Animationnum = FrmAnimation.nudSprite1.Value

        If Animationnum < 1 OrElse Animationnum > NumAnimations Then
            EditorAnimation_Anim2.Clear(ToSFMLColor(FrmAnimation.picSprite1.BackColor))
            EditorAnimation_Anim2.Display()
        Else
            If AnimationsGFXInfo(Animationnum).IsLoaded = False Then
                LoadTexture(Animationnum, 6)
            End If

            'seeying we still use it, lets update timer
            With AnimationsGFXInfo(Animationnum)
                .TextureTimer = GetTickCount() + 100000
            End With

            looptime = FrmAnimation.nudLoopTime1.Value
            FrameCount = FrmAnimation.nudFrameCount1.Value

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
                If FrmAnimation.nudFrameCount1.Value > 0 Then
                    ' total width divided by frame count
                    height = AnimationsGFXInfo(Animationnum).height
                    width = AnimationsGFXInfo(Animationnum).width / FrmAnimation.nudFrameCount1.Value

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

                    EditorAnimation_Anim2.Clear(ToSFMLColor(FrmAnimation.picSprite1.BackColor))

                    RenderSprite(AnimationsSprite(Animationnum), EditorAnimation_Anim2, dRECT.X, dRECT.Y, sRECT.X, sRECT.Y, sRECT.Width, sRECT.Height)
                    EditorAnimation_Anim2.Display()

                End If
            End If
        End If
    End Sub
    
    Friend Function ToSFMLColor(ToConvert As Drawing.Color) As Color
        Return New Color(ToConvert.R, ToConvert.G, ToConvert.G, ToConvert.A)
    End Function

End Module