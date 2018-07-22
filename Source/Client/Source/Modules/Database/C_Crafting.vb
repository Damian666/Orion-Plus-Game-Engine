Imports System.Drawing
Imports ASFW

Friend Module C_Crafting
#Region "Globals"

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
    Friend PicProductindex as integer
    Friend LblProductNameText As String
    Friend LblProductAmountText As String

    Friend PicMaterialIndex(MAX_INGREDIENT) As Integer
    Friend LblMaterialName(MAX_INGREDIENT) As String
    Friend LblMaterialAmount(MAX_INGREDIENT) As String

    Friend SelectedRecipe As Integer = 0

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
        Recipe(Num).Name = ""
        Recipe(Num).RecipeType = 0
        Recipe(Num).MakeItemNum = 0
        ReDim Recipe(Num).Ingredients(MAX_INGREDIENT)
    End Sub

    Friend Sub ClearChanged_Recipe()
        Dim i As Integer

        For i = 1 To MAX_RECIPE
            RecipeChanged(i) = Nothing
        Next

        ReDim RecipeChanged(MAX_RECIPE)
    End Sub
#End Region

#Region "Incoming Packets"
    Sub Packet_UpdateRecipe(ByRef data() As Byte)
        Dim n As Integer, i As Integer
        dim buffer as New ByteStream(Data)
        'recipe index
        n = Buffer.ReadInt32

        ' Update the Recipe
        Recipe(n).Name = Trim$(Buffer.ReadString)
        Recipe(n).RecipeType = Buffer.ReadInt32
        Recipe(n).MakeItemNum = Buffer.ReadInt32
        Recipe(n).MakeItemAmount = Buffer.ReadInt32

        For i = 1 To MAX_INGREDIENT
            Recipe(n).Ingredients(i).ItemNum = Buffer.ReadInt32()
            Recipe(n).Ingredients(i).Value = Buffer.ReadInt32()
        Next

        Recipe(n).CreateTime = Buffer.ReadInt32

        Buffer.Dispose()

    End Sub

    Sub Packet_RecipeEditor(ByRef data() As Byte)
        InitRecipeEditor = True
    End Sub

    Sub Packet_SendPlayerRecipe(ByRef data() As Byte)
        Dim i As Integer
        dim buffer as New ByteStream(Data)
        For i = 1 To MAX_RECIPE
            Player(MyIndex).RecipeLearned(i) = Buffer.ReadInt32
        Next

        Buffer.Dispose()
    End Sub

    Sub Packet_OpenCraft(ByRef data() As Byte)
        InitCrafting = True
    End Sub

    Sub Packet_UpdateCraft(ByRef data() As Byte)
        Dim done As Byte
        dim buffer as New ByteStream(Data)
        done = Buffer.ReadInt32

        If done = 1 Then
            InitCrafting = True
        Else
            CraftProgressValue = 0
            CraftTimerEnabled = True
        End If

        Buffer.Dispose()
    End Sub
#End Region

#Region "OutGoing Packets"
    Sub SendRequestRecipes()
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CRequestRecipes)

        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()
    End Sub

    Sub SendRequestEditRecipes()
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(EditorPackets.RequestEditRecipes)

        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()
    End Sub

    Sub SendSaveRecipe(recipeNum As Integer)
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(EditorPackets.SaveRecipe)

        Buffer.WriteInt32(RecipeNum)

        buffer.WriteString((Trim$(Recipe(recipeNum).Name)))
        buffer.WriteInt32(Recipe(RecipeNum).RecipeType)
        Buffer.WriteInt32(Recipe(RecipeNum).MakeItemNum)
        Buffer.WriteInt32(Recipe(RecipeNum).MakeItemAmount)

        For i = 1 To MAX_INGREDIENT
            Buffer.WriteInt32(Recipe(RecipeNum).Ingredients(i).ItemNum)
            Buffer.WriteInt32(Recipe(RecipeNum).Ingredients(i).Value)
        Next

        Buffer.WriteInt32(Recipe(RecipeNum).CreateTime)

        Socket.SendData(Buffer.Data, Buffer.Head)
        Buffer.Dispose()
    End Sub

    Friend Sub SendCraftIt(recipeName As String, amount As Integer)
        dim buffer as New ByteStream(4), i As Integer
        Dim recipeindex as integer

        recipeindex = GetRecipeIndex(RecipeName)

        If recipeindex <= 0 Then Exit Sub

        'check,check, double check

        'we dont even know the damn recipe xD
        If Player(MyIndex).RecipeLearned(recipeindex) = 0 Then Exit Sub

        'enough ingredients?
        For i = 1 To MAX_INGREDIENT
            If Recipe(recipeindex).Ingredients(i).ItemNum > 0 AndAlso HasItem(MyIndex, Recipe(recipeindex).Ingredients(i).ItemNum) < (Amount * Recipe(recipeindex).Ingredients(i).Value) Then
                AddText(Strings.Get("crafting", "notenough"), ColorType.Red)
                Exit Sub
            End If
        Next

        'all seems fine...

        Buffer.WriteInt32(ClientPackets.CStartCraft)

        Buffer.WriteInt32(recipeindex)
        Buffer.WriteInt32(Amount)

        Socket.SendData(Buffer.Data, Buffer.Head)

        Buffer.Dispose()

        CraftTimer = GetTickCount()
        CraftTimerEnabled = True

        btnCraftEnabled = False
        btnCraftStopEnabled = False
        btnCraftStopEnabled = False
        nudCraftAmountEnabled = False
        lstRecipeEnabled = False
        chkKnownOnlyEnabled = False
    End Sub

    Sub SendCloseCraft()
        dim buffer as New ByteStream(4)

        Buffer.WriteInt32(ClientPackets.CCloseCraft)

        Socket.SendData(Buffer.Data, Buffer.Head)

        Buffer.Dispose()
    End Sub
#End Region

#Region "Functions"
    Friend Sub CraftingInit()
        Dim i As Integer, x As Integer

        x = 1

        For i = 1 To MAX_RECIPE
            If chkKnownOnlyChecked = True Then
                If Player(MyIndex).RecipeLearned(i) = 1 Then
                    RecipeNames(x) = Trim$(Recipe(i).Name)
                    x = x + 1
                End If
            Else
                If Len(Trim(Recipe(i).Name)) > 0 Then
                    RecipeNames(x) = Trim$(Recipe(i).Name)
                    x = x + 1
                End If
            End If
        Next

        CraftAmountValue = 1

        InCraft = True

        LoadRecipe(RecipeNames(SelectedRecipe))

        pnlCraftVisible = True
    End Sub

    Sub LoadRecipe(recipeName As String)
        Dim recipeindex as integer

        recipeindex = GetRecipeIndex(RecipeName)

        If recipeindex <= 0 Then Exit Sub

        picProductIndex = Item(Recipe(recipeindex).MakeItemNum).Pic
        lblProductNameText = Item(Recipe(recipeindex).MakeItemNum).Name
        lblProductAmountText = "X 1"

        For i = 1 To MAX_INGREDIENT
            If Recipe(recipeindex).Ingredients(i).ItemNum > 0 Then
                picMaterialIndex(i) = Item(Recipe(recipeindex).Ingredients(i).ItemNum).Pic
                lblMaterialName(i) = Item(Recipe(recipeindex).Ingredients(i).ItemNum).Name
                lblMaterialAmount(i) = "X " & HasItem(MyIndex, Recipe(recipeindex).Ingredients(i).ItemNum) & "/" & Recipe(recipeindex).Ingredients(i).Value
            Else
                picMaterialIndex(i) = 0
                lblMaterialName(i) = ""
                lblMaterialAmount(i) = ""
            End If
        Next

    End Sub

    Function GetRecipeIndex(recipeName As String) As Integer
        Dim i As Integer

        GetRecipeIndex = 0

        For i = 1 To MAX_RECIPE
            If Trim$(Recipe(i).Name) = Trim$(RecipeName) Then
                GetRecipeIndex = i
                Exit For
            End If
        Next

    End Function

    Friend Sub DrawCraftPanel()
        Dim i As Integer, y As Integer
        Dim rec As Rectangle, pgbvalue As Integer

        'first render panel
        RenderSprite(CraftSprite, GameWindow, CraftPanelX, CraftPanelY, 0, 0, CraftGFXInfo.Width, CraftGFXInfo.Height)

        y = 10

        'draw recipe names
        For i = 1 To MAX_RECIPE
            If Len(Trim$(RecipeNames(i))) > 0 Then
                DrawText(CraftPanelX + 12, CraftPanelY + y, Trim$(RecipeNames(i)), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
                y = y + 20
            End If
        Next

        'progress bar
        pgbvalue = (CraftProgressValue / 100) * 100

        With rec
            .Y = 0
            .Height = ProgBarGFXInfo.Height
            .X = 0
            .Width = pgbvalue * ProgBarGFXInfo.Width / 100
        End With

        RenderSprite(ProgBarSprite, GameWindow, CraftPanelX + 410, CraftPanelY + 417, rec.X, rec.Y, rec.Width, rec.Height)

        'amount controls
        RenderSprite(CharPanelMinSprite, GameWindow, CraftPanelX + 340, CraftPanelY + 422, 0, 0, CharPanelMinGFXInfo.Width, CharPanelMinGFXInfo.Height)

        DrawText(CraftPanelX + 367, CraftPanelY + 418, Trim$(CraftAmountValue), SFML.Graphics.Color.Black, SFML.Graphics.Color.White, GameWindow)

        RenderSprite(CharPanelPlusSprite, GameWindow, CraftPanelX + 392, CraftPanelY + 422, 0, 0, CharPanelPlusGFXInfo.Width, CharPanelPlusGFXInfo.Height)

        If SelectedRecipe = 0 Then Exit Sub

        If picProductIndex > 0 Then
            If ItemsGFXInfo(picProductIndex).IsLoaded = False Then
                LoadTexture(picProductIndex, 4)
            End If

            'seeying we still use it, lets update timer
            With ItemsGFXInfo(picProductIndex)
                .TextureTimer = GetTickCount() + 100000
            End With

            RenderSprite(ItemsSprite(picProductIndex), GameWindow, CraftPanelX + 267, CraftPanelY + 20, 0, 0, ItemsGFXInfo(picProductIndex).Width, ItemsGFXInfo(picProductIndex).Height)

            DrawText(CraftPanelX + 310, CraftPanelY + 20, Trim$(lblProductNameText), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)

            DrawText(CraftPanelX + 310, CraftPanelY + 35, Trim$(lblProductAmountText), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)
        End If

        y = 107

        For i = 1 To MAX_INGREDIENT
            If picMaterialIndex(i) > 0 Then
                If ItemsGFXInfo(picMaterialIndex(i)).IsLoaded = False Then
                    LoadTexture(picMaterialIndex(i), 4)
                End If

                'seeying we still use it, lets update timer
                With ItemsGFXInfo(picMaterialIndex(i))
                    .TextureTimer = GetTickCount() + 100000
                End With

                RenderSprite(ItemsSprite(picMaterialIndex(i)), GameWindow, CraftPanelX + 275, CraftPanelY + y, 0, 0, ItemsGFXInfo(picMaterialIndex(i)).Width, ItemsGFXInfo(picMaterialIndex(i)).Height)

                DrawText(CraftPanelX + 315, CraftPanelY + y, Trim$(lblMaterialName(i)), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)

                DrawText(CraftPanelX + 315, CraftPanelY + y + 15, Trim$(lblMaterialAmount(i)), SFML.Graphics.Color.White, SFML.Graphics.Color.Black, GameWindow)

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

        picProductIndex = 0
        lblProductNameText = Strings.Get("crafting", "noneselected")
        lblProductAmountText = "0"

        For i = 1 To MAX_INGREDIENT
            picMaterialIndex(i) = 0
            lblMaterialName(i) = ""
            lblMaterialAmount(i) = ""
        Next

        CraftTimerEnabled = False

        btnCraftEnabled = True
        btnCraftStopEnabled = True
        nudCraftAmountEnabled = True
        lstRecipeEnabled = True
        chkKnownOnlyEnabled = True

        SelectedRecipe = 0
    End Sub

#End Region

End Module