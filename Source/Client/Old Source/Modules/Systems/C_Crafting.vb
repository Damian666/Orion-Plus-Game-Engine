Imports System.Drawing
Imports ASFW

Friend Module C_Crafting

#Region "Globals & Types"

    Friend RecipeChanged(MAX_RECIPE) As Boolean
    Friend Recipe(MAX_RECIPE) As RecipeRec
    Friend InitRecipeEditor As Boolean
    Friend InitCrafting As Boolean
    Friend InCraft As Boolean
    Friend PnlCraftVisible As Boolean

    Friend Const RecipeTypeHerb As Byte = 0
    Friend Const RecipeTypeWood As Byte = 1
    Friend Const RecipeTypeMetal As Byte = 2

    Friend RecipeNames(MAX_RECIPE) As String

    Friend ChkKnownOnlyChecked As Boolean
    Friend ChkKnownOnlyEnabled As Boolean
    Friend BtnCraftEnabled As Boolean
    Friend BtnCraftStopEnabled As Boolean
    Friend NudCraftAmountEnabled As Boolean
    Friend LstRecipeEnabled As Boolean

    Friend CraftAmountValue As Byte
    Friend CraftProgressValue As Integer
    Friend PicProductindex As Integer
    Friend LblProductNameText As String
    Friend LblProductAmountText As String

    Friend PicMaterialIndex(MAX_INGREDIENT) As Integer
    Friend LblMaterialName(MAX_INGREDIENT) As String
    Friend LblMaterialAmount(MAX_INGREDIENT) As String

    Friend SelectedRecipe As Integer = 0

    Friend CraftTimerEnabled As Boolean
    Friend CraftTimer As Integer

    Friend Structure RecipeRec
        Dim Name As String
        Dim RecipeType As Byte
        Dim MakeItemNum As Integer
        Dim MakeItemAmount As Integer
        Dim Ingredients() As IngredientsRec
        Dim CreateTime As Byte
    End Structure

    Friend Structure IngredientsRec
        Dim ItemNum As Integer
        Dim Value As Integer
    End Structure

#End Region

#Region "Database"

    Sub ClearRecipes()
        Dim i As Integer

        ReDim Recipe(MAX_RECIPE)

        For i = 1 To MAX_RECIPE
            ClearRecipe(i)
        Next

    End Sub

    Sub ClearRecipe(num As Integer)
        Recipe(num).Name = ""
        Recipe(num).RecipeType = 0
        Recipe(num).MakeItemNum = 0
        ReDim Recipe(num).Ingredients(MAX_INGREDIENT)
    End Sub

    Friend Sub ClearChanged_Recipe()
        Dim i As Integer

        For i = 1 To MAX_RECIPE
            RecipeChanged(i) = Nothing
        Next

        ReDim RecipeChanged(MAX_RECIPE)
    End Sub

#End Region

#Region "Editor"

    Friend Sub RecipeEditorInit()
        Dim i As Integer

        With FrmRecipe
            Editor = EDITOR_RECIPE
            .lstIndex.Items.Clear()

            ' Add the names
            For i = 1 To MAX_RECIPE
                .lstIndex.Items.Add(i & ": " & Recipe(i).Name.Trim)
            Next

            'fill comboboxes
            .cmbMakeItem.Items.Clear()
            .cmbIngredient.Items.Clear()

            .cmbMakeItem.Items.Add("None")
            .cmbIngredient.Items.Add("None")
            For i = 1 To MAX_ITEMS
                .cmbMakeItem.Items.Add(Item(i).Name.Trim)
                .cmbIngredient.Items.Add(Item(i).Name.Trim)
            Next

            .Show()
            .lstIndex.SelectedIndex = 0
        End With

        Editorindex = FrmRecipe.lstIndex.SelectedIndex + 1

        With Recipe(Editorindex)
            FrmRecipe.txtName.Text = .Name.Trim

            FrmRecipe.lstIngredients.Items.Clear()

            FrmRecipe.cmbType.SelectedIndex = .RecipeType
            FrmRecipe.cmbMakeItem.SelectedIndex = .MakeItemNum

            If .MakeItemAmount < 1 Then .MakeItemAmount = 1
            FrmRecipe.nudAmount.Value = .MakeItemAmount

            If .CreateTime < 1 Then .CreateTime = 1
            FrmRecipe.nudCreateTime.Value = .CreateTime

            UpdateIngredient()
        End With

        RecipeChanged(Editorindex) = True

    End Sub

    Friend Sub RecipeEditorCancel()
        Editor = 0
        FrmRecipe.Visible = False
        ClearChanged_Recipe()
        ClearRecipes()
        SendRequestRecipes()
    End Sub

    Friend Sub RecipeEditorOk()
        Dim i As Integer

        For i = 1 To MAX_RECIPE
            If RecipeChanged(i) Then
                SendSaveRecipe(i)
            End If
        Next

        FrmRecipe.Visible = False
        Editor = 0
        ClearChanged_Recipe()
    End Sub

    Friend Sub UpdateIngredient()
        Dim i As Integer
        FrmRecipe.lstIngredients.Items.Clear()

        For i = 1 To MAX_INGREDIENT
            With Recipe(Editorindex).Ingredients(i)
                ' if none, show as none
                If .ItemNum <= 0 AndAlso .Value = 0 Then
                    FrmRecipe.lstIngredients.Items.Add("Empty")
                Else
                    FrmRecipe.lstIngredients.Items.Add(Item(.ItemNum).Name.Trim & " X " & .Value)
                End If

            End With
        Next

        FrmRecipe.lstIngredients.SelectedIndex = 0
    End Sub

#End Region

#Region "Incoming Packets"

    Sub Packet_UpdateRecipe(ByRef data() As Byte)
        Dim n As Integer, i As Integer
        Dim buffer As New ByteStream(data)
        'recipe index
        n = buffer.ReadInt32

        ' Update the Recipe
        Recipe(n).Name = buffer.ReadString.Trim
        Recipe(n).RecipeType = buffer.ReadInt32
        Recipe(n).MakeItemNum = buffer.ReadInt32
        Recipe(n).MakeItemAmount = buffer.ReadInt32

        For i = 1 To MAX_INGREDIENT
            Recipe(n).Ingredients(i).ItemNum = buffer.ReadInt32()
            Recipe(n).Ingredients(i).Value = buffer.ReadInt32()
        Next

        Recipe(n).CreateTime = buffer.ReadInt32

        buffer.Dispose()

    End Sub

    Sub Packet_RecipeEditor(ByRef data() As Byte)
        InitRecipeEditor = True
    End Sub

    Sub Packet_SendPlayerRecipe(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)
        For i = 1 To MAX_RECIPE
            Player(Myindex).RecipeLearned(i) = buffer.ReadInt32
        Next

        buffer.Dispose()
    End Sub

    Sub Packet_OpenCraft(ByRef data() As Byte)
        InitCrafting = True
    End Sub

    Sub Packet_UpdateCraft(ByRef data() As Byte)
        Dim done As Byte
        Dim buffer As New ByteStream(data)
        done = buffer.ReadInt32

        If done = 1 Then
            InitCrafting = True
        Else
            CraftProgressValue = 0
            CraftTimerEnabled = True
        End If

        buffer.Dispose()
    End Sub

#End Region

#Region "OutGoing Packets"

    Sub SendRequestRecipes()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CRequestRecipes)

        Network.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Sub SendRequestEditRecipes()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.RequestEditRecipes)

        Network.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Sub SendSaveRecipe(recipeNum As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(EditorPackets.SaveRecipe)

        buffer.WriteInt32(recipeNum)

        buffer.WriteString((Recipe(recipeNum).Name.Trim))
        buffer.WriteInt32(Recipe(recipeNum).RecipeType)
        buffer.WriteInt32(Recipe(recipeNum).MakeItemNum)
        buffer.WriteInt32(Recipe(recipeNum).MakeItemAmount)

        For i = 1 To MAX_INGREDIENT
            buffer.WriteInt32(Recipe(recipeNum).Ingredients(i).ItemNum)
            buffer.WriteInt32(Recipe(recipeNum).Ingredients(i).Value)
        Next

        buffer.WriteInt32(Recipe(recipeNum).CreateTime)

        Network.SendData(buffer.ToPacket)
        buffer.Dispose()
    End Sub

    Friend Sub SendCraftIt(recipeName As String, amount As Integer)
        Dim buffer As New ByteStream(4), i As Integer
        Dim recipeindex As Integer

        recipeindex = GetRecipeIndex(recipeName)

        If recipeindex <= 0 Then Exit Sub

        'check,check, double check

        'we dont even know the damn recipe xD
        If Player(Myindex).RecipeLearned(recipeindex) = 0 Then Exit Sub

        'enough ingredients?
        For i = 1 To MAX_INGREDIENT
            If Recipe(recipeindex).Ingredients(i).ItemNum > 0 AndAlso HasItem(Myindex, Recipe(recipeindex).Ingredients(i).ItemNum) < (amount * Recipe(recipeindex).Ingredients(i).Value) Then
                AddText(Configuration.Language.Crafting.notenough, ColorType.Red)
                Exit Sub
            End If
        Next

        'all seems fine...

        buffer.WriteInt32(ClientPacket.CStartCraft)

        buffer.WriteInt32(recipeindex)
        buffer.WriteInt32(amount)

        Network.SendData(buffer.ToPacket)

        buffer.Dispose()

        CraftTimer = GetTickCount()
        CraftTimerEnabled = True

        BtnCraftEnabled = False
        BtnCraftStopEnabled = False
        BtnCraftStopEnabled = False
        NudCraftAmountEnabled = False
        LstRecipeEnabled = False
        ChkKnownOnlyEnabled = False
    End Sub

    Sub SendCloseCraft()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPacket.CCloseCraft)

        Network.SendData(buffer.ToPacket)

        buffer.Dispose()
    End Sub

#End Region

#Region "Functions"

    Friend Sub CraftingInit()
        Dim i As Integer, x As Integer

        x = 1

        For i = 1 To MAX_RECIPE
            If ChkKnownOnlyChecked = True Then
                If Player(Myindex).RecipeLearned(i) = 1 Then
                    RecipeNames(x) = Recipe(i).Name.Trim
                    x = x + 1
                End If
            Else
                If Recipe(i).Name.Trim.Length > 0 Then
                    RecipeNames(x) = Recipe(i).Name.Trim
                    x = x + 1
                End If
            End If
        Next

        CraftAmountValue = 1

        InCraft = True

        LoadRecipe(RecipeNames(SelectedRecipe))

        PnlCraftVisible = True
    End Sub

    Sub LoadRecipe(recipeName As String)
        Dim recipeindex As Integer

        recipeindex = GetRecipeIndex(recipeName)

        If recipeindex <= 0 Then Exit Sub

        PicProductindex = Item(Recipe(recipeindex).MakeItemNum).Pic
        LblProductNameText = Item(Recipe(recipeindex).MakeItemNum).Name
        LblProductAmountText = "X 1"

        For i = 1 To MAX_INGREDIENT
            If Recipe(recipeindex).Ingredients(i).ItemNum > 0 Then
                PicMaterialIndex(i) = Item(Recipe(recipeindex).Ingredients(i).ItemNum).Pic
                LblMaterialName(i) = Item(Recipe(recipeindex).Ingredients(i).ItemNum).Name
                LblMaterialAmount(i) = "X " & HasItem(Myindex, Recipe(recipeindex).Ingredients(i).ItemNum) & "/" & Recipe(recipeindex).Ingredients(i).Value
            Else
                PicMaterialIndex(i) = 0
                LblMaterialName(i) = ""
                LblMaterialAmount(i) = ""
            End If
        Next

    End Sub

    Function GetRecipeIndex(recipeName As String) As Integer
        Dim i As Integer

        GetRecipeIndex = 0

        For i = 1 To MAX_RECIPE
            If Recipe(i).Name.Trim = recipeName.Trim Then
                GetRecipeIndex = i
                Exit For
            End If
        Next

    End Function

    Friend Sub DrawCraftPanel()
        Dim i As Integer, y As Integer
        Dim rec As Rectangle, pgbvalue As Integer

        'first render panel
        RenderSprite(CraftSprite, GameWindow, CraftPanelX, CraftPanelY, 0, 0, CraftGfxInfo.Width, CraftGfxInfo.Height)

        y = 10

        'draw recipe names
        For i = 1 To MAX_RECIPE
            If RecipeNames(i).Trim.Length > 0 Then
                DrawText(CraftPanelX + 12, CraftPanelY + y, RecipeNames(i).Trim, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
                y = y + 20
            End If
        Next

        'progress bar
        pgbvalue = (CraftProgressValue / 100) * 100

        With rec
            .Y = 0
            .Height = ProgBarGfxInfo.Height
            .X = 0
            .Width = pgbvalue * ProgBarGfxInfo.Width / 100
        End With

        RenderSprite(ProgBarSprite, GameWindow, CraftPanelX + 410, CraftPanelY + 417, rec.X, rec.Y, rec.Width, rec.Height)

        'amount controls
        RenderSprite(CharPanelMinSprite, GameWindow, CraftPanelX + 340, CraftPanelY + 422, 0, 0, CharPanelMinGfxInfo.Width, CharPanelMinGfxInfo.Height)

        DrawText(CraftPanelX + 367, CraftPanelY + 418, CraftAmountValue, SFML.Graphics.Color.Black, SFML.Graphics.Color.White, GameWindow)

        RenderSprite(CharPanelPlusSprite, GameWindow, CraftPanelX + 392, CraftPanelY + 422, 0, 0, CharPanelPlusGfxInfo.Width, CharPanelPlusGfxInfo.Height)

        If SelectedRecipe = 0 Then Exit Sub

        If PicProductindex > 0 Then
            If ItemsGfxInfo(PicProductindex).IsLoaded = False Then
                LoadTexture(PicProductindex, 4)
            End If

            'seeying we still use it, lets update timer
            With ItemsGfxInfo(PicProductindex)
                .TextureTimer = GetTickCount() + 100000
            End With

            RenderSprite(ItemsSprite(PicProductindex), GameWindow, CraftPanelX + 267, CraftPanelY + 20, 0, 0, ItemsGfxInfo(PicProductindex).Width, ItemsGfxInfo(PicProductindex).Height)

            DrawText(CraftPanelX + 310, CraftPanelY + 20, LblProductNameText.Trim, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)

            DrawText(CraftPanelX + 310, CraftPanelY + 35, LblProductAmountText.Trim, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        End If

        y = 107

        For i = 1 To MAX_INGREDIENT
            If PicMaterialIndex(i) > 0 Then
                If ItemsGfxInfo(PicMaterialIndex(i)).IsLoaded = False Then
                    LoadTexture(PicMaterialIndex(i), 4)
                End If

                'seeying we still use it, lets update timer
                With ItemsGfxInfo(PicMaterialIndex(i))
                    .TextureTimer = GetTickCount() + 100000
                End With

                RenderSprite(ItemsSprite(PicMaterialIndex(i)), GameWindow, CraftPanelX + 275, CraftPanelY + y, 0, 0, ItemsGfxInfo(PicMaterialIndex(i)).Width, ItemsGfxInfo(PicMaterialIndex(i)).Height)

                DrawText(CraftPanelX + 315, CraftPanelY + y, LblMaterialName(i).Trim, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)

                DrawText(CraftPanelX + 315, CraftPanelY + y + 15, LblMaterialAmount(i).Trim, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)

                y = y + 63
            End If
        Next

    End Sub

    Friend Sub ResetCraftPanel()
        'reset the panel's info
        ReDim RecipeNames(MAX_RECIPE)

        For i = 1 To MAX_RECIPE
            RecipeNames(i) = ""
        Next

        CraftProgressValue = 0

        CraftAmountValue = 1

        PicProductindex = 0
        LblProductNameText = Configuration.Language.Crafting.noneselected
        LblProductAmountText = "0"

        For i = 1 To MAX_INGREDIENT
            PicMaterialIndex(i) = 0
            LblMaterialName(i) = ""
            LblMaterialAmount(i) = ""
        Next

        CraftTimerEnabled = False

        BtnCraftEnabled = True
        BtnCraftStopEnabled = True
        NudCraftAmountEnabled = True
        LstRecipeEnabled = True
        ChkKnownOnlyEnabled = True

        SelectedRecipe = 0
    End Sub

#End Region

End Module