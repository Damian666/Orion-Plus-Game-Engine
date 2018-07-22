Imports System.IO
Imports ASFW
Imports ASFW.IO.FileIO

Friend Module modCrafting
#Region "Globals"
    Friend Recipe(MAX_RECIPE) As RecipeRec

    Friend Const RecipeType_Herb As Byte = 0
    Friend Const RecipeType_Wood As Byte = 1
    Friend Const RecipeType_Metal As Byte = 2

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
    Sub CheckRecipes()
        Dim i As Integer

        For i = 1 To MAX_RECIPE
            If Not File.Exists(Path.Combine(Application.StartupPath, "data", "recipes", String.Format("recipe{0}.dat", i))) Then
                SaveRecipe(i)
                Application.DoEvents()
            End If
        Next

    End Sub

    Sub SaveRecipes()
        Dim i As Integer

        For i = 1 To MAX_RECIPE
            SaveRecipe(i)
            Application.DoEvents()
        Next

    End Sub

    Sub SaveRecipe(RecipeNum As Integer)
        Dim filename As String
        Dim i As Integer

        filename = Path.Combine(Application.StartupPath, "data", "recipes", String.Format("recipe{0}.dat", RecipeNum))

        Dim writer As New ByteStream(100)

        writer.WriteString(Recipe(RecipeNum).Name)
        writer.WriteByte(Recipe(RecipeNum).RecipeType)
        writer.WriteInt32(Recipe(RecipeNum).MakeItemNum)
        writer.WriteInt32(Recipe(RecipeNum).MakeItemAmount)

        For i = 1 To MAX_INGREDIENT
            writer.WriteInt32(Recipe(RecipeNum).Ingredients(i).ItemNum)
            writer.WriteInt32(Recipe(RecipeNum).Ingredients(i).Value)
        Next

        writer.WriteByte(Recipe(RecipeNum).CreateTime)

        BinaryFile.Save(filename, writer)
    End Sub

    Sub LoadRecipes()
        Dim i As Integer

        For i = 1 To MAX_RECIPE
            LoadRecipe(i)
            Application.DoEvents()
        Next

    End Sub

    Sub LoadRecipe(RecipeNum As Integer)
        Dim filename As String
        Dim i As Integer

        CheckRecipes()

        filename = Path.Combine(Application.StartupPath, "data", "recipes", String.Format("recipe{0}.dat", RecipeNum))
        Dim reader As New ByteStream()
        BinaryFile.Load(filename, reader)

        Recipe(RecipeNum).Name = reader.ReadString()
        Recipe(RecipeNum).RecipeType = reader.ReadByte()
        Recipe(RecipeNum).MakeItemNum = reader.ReadInt32()
        Recipe(RecipeNum).MakeItemAmount = reader.ReadInt32()

        ReDim Recipe(RecipeNum).Ingredients(MAX_INGREDIENT)
        For i = 1 To MAX_INGREDIENT
            Recipe(RecipeNum).Ingredients(i).ItemNum = reader.ReadInt32()
            Recipe(RecipeNum).Ingredients(i).Value = reader.ReadInt32()
        Next

        Recipe(RecipeNum).CreateTime = reader.ReadByte()

    End Sub

    Sub ClearRecipes()
        Dim i As Integer

        For i = 1 To MAX_RECIPE
            ClearRecipe(i)
            Application.DoEvents()
        Next

    End Sub

    Sub ClearRecipe(Num As Integer)
        Recipe(Num).Name = ""
        Recipe(Num).RecipeType = 0
        Recipe(Num).MakeItemNum = 0
        Recipe(Num).MakeItemAmount = 0
        Recipe(Num).CreateTime = 0
        ReDim Recipe(Num).Ingredients(MAX_INGREDIENT)
    End Sub

#End Region

#Region "Incoming Packets"
    Sub Packet_RequestRecipes(index as integer, ByRef data() As Byte)
        AddDebug("Recieved CMSG: CRequestRecipes")

        SendRecipes(Index)
    End Sub

    Sub Packet_RequestEditRecipes(index as integer, ByRef data() As Byte)
        ' Prevent hacking
        If GetPlayerAccess(Index) < AdminType.Developer Then Exit Sub

        Dim Buffer = New ByteStream(4)
        Buffer.WriteInt32(ServerPackets.SRecipeEditor)
        Socket.SendDataTo(Index, Buffer.Data, Buffer.Head)

        AddDebug("Sent SMSG: SRecipeEditor")

        Buffer.Dispose()

    End Sub

    Sub Packet_SaveRecipe(index as integer, ByRef data() As Byte)
        Dim n As Integer

        ' Prevent hacking
        If GetPlayerAccess(Index) < AdminType.Developer Then Exit Sub
        dim buffer as New ByteStream(data)
        AddDebug("Recieved EMSG: SaveRecipe")

        'recipe index
        n = Buffer.ReadInt32

        ' Update the Recipe
        Recipe(n).Name = buffer.ReadString
        Recipe(n).RecipeType = Buffer.ReadInt32
        Recipe(n).MakeItemNum = Buffer.ReadInt32
        Recipe(n).MakeItemAmount = Buffer.ReadInt32

        For i = 1 To MAX_INGREDIENT
            Recipe(n).Ingredients(i).ItemNum = Buffer.ReadInt32()
            Recipe(n).Ingredients(i).Value = Buffer.ReadInt32()
        Next

        Recipe(n).CreateTime = Buffer.ReadInt32

        'save
        SaveRecipe(n)

        'send to all
        SendUpdateRecipeToAll(n)

        Buffer.Dispose()

    End Sub

    Sub Packet_CloseCraft(index as integer, ByRef data() As Byte)
        AddDebug("Recieved CMSG: CCloseCraft")

        TempPlayer(Index).IsCrafting = False
    End Sub

    Sub Packet_StartCraft(index as integer, ByRef data() As Byte)
        Dim recipeindex as integer, amount As Integer
        Dim buffer As New ByteStream(data)

        AddDebug("Recieved CMSG: CStartCraft")

        recipeindex = Buffer.ReadInt32
        amount = Buffer.ReadInt32

        If TempPlayer(Index).IsCrafting = False Then Exit Sub

        If recipeindex = 0 OrElse amount = 0 Then Exit Sub

        If Not CheckLearnedRecipe(Index, recipeindex) Then Exit Sub

        StartCraft(Index, recipeindex, amount)

        Buffer.Dispose()

    End Sub

#End Region

#Region "Outgoing Packets"
    Sub SendRecipes(index as integer)
        Dim i As Integer

        For i = 1 To MAX_RECIPE

            If Len(Trim$(Recipe(i).Name)) > 0 Then
                SendUpdateRecipeTo(Index, i)
            End If

        Next

    End Sub

    Sub SendUpdateRecipeTo(index as integer, RecipeNum As Integer)
        dim buffer as ByteStream, i As Integer
        Buffer = New ByteStream(4)
        Buffer.WriteInt32(ServerPackets.SUpdateRecipe)
        Buffer.WriteInt32(RecipeNum)

        AddDebug("Sent SMSG: SUpdateRecipe")

        buffer.WriteString((Trim$(Recipe(RecipeNum).Name)))
        buffer.WriteInt32(Recipe(RecipeNum).RecipeType)
        buffer.WriteInt32(Recipe(RecipeNum).MakeItemNum)
        buffer.WriteInt32(Recipe(RecipeNum).MakeItemAmount)

        For i = 1 To MAX_INGREDIENT
            buffer.WriteInt32(Recipe(RecipeNum).Ingredients(i).ItemNum)
            buffer.WriteInt32(Recipe(RecipeNum).Ingredients(i).Value)
        Next

        buffer.WriteInt32(Recipe(RecipeNum).CreateTime)

        Socket.SendDataTo(index, buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

    Sub SendUpdateRecipeToAll(RecipeNum As Integer)
        Dim buffer As ByteStream
        buffer = New ByteStream(4)
        buffer.WriteInt32(ServerPackets.SUpdateRecipe)
        buffer.WriteInt32(RecipeNum)

        AddDebug("Sent SMSG: SUpdateRecipe To All")

        buffer.WriteString((Trim$(Recipe(RecipeNum).Name)))
        buffer.WriteInt32(Recipe(RecipeNum).RecipeType)
        Buffer.WriteInt32(Recipe(RecipeNum).MakeItemNum)
        Buffer.WriteInt32(Recipe(RecipeNum).MakeItemAmount)

        For i = 1 To MAX_INGREDIENT
            Buffer.WriteInt32(Recipe(RecipeNum).Ingredients(i).ItemNum)
            Buffer.WriteInt32(Recipe(RecipeNum).Ingredients(i).Value)
        Next

        Buffer.WriteInt32(Recipe(RecipeNum).CreateTime)

        SendDataToAll(Buffer.Data, Buffer.Head)

        Buffer.Dispose()
    End Sub

    Sub SendPlayerRecipes(index as integer)
        Dim i As Integer
        dim buffer as ByteStream
        Buffer = New ByteStream(4)
        Buffer.WriteInt32(ServerPackets.SSendPlayerRecipe)

        AddDebug("Sent SMSG: SSendPlayerRecipe")

        For i = 1 To MAX_RECIPE
            Buffer.WriteInt32(Player(Index).Character(TempPlayer(Index).CurChar).RecipeLearned(i))
        Next

        Socket.SendDataTo(Index, Buffer.Data, Buffer.Head)

        Buffer.Dispose()
    End Sub

    Sub SendOpenCraft(index as integer)
        dim buffer as ByteStream
        Buffer = New ByteStream(4)
        Buffer.WriteInt32(ServerPackets.SOpenCraft)

        AddDebug("Sent SMSG: SOpenCraft")

        Socket.SendDataTo(Index, Buffer.Data, Buffer.Head)

        Buffer.Dispose()
    End Sub

    Sub SendCraftUpdate(index as integer, done As Byte)
        dim buffer as ByteStream
        Buffer = New ByteStream(4)
        Buffer.WriteInt32(ServerPackets.SUpdateCraft)

        AddDebug("Sent SMSG: SUpdateCraft")

        buffer.WriteInt32(done)

        Socket.SendDataTo(Index, Buffer.Data, Buffer.Head)

        Buffer.Dispose()
    End Sub
#End Region

#Region "Functions"

    Friend Function CheckLearnedRecipe(index as integer, RecipeNum As Integer) As Boolean
        CheckLearnedRecipe = False

        If Player(Index).Character(TempPlayer(Index).CurChar).RecipeLearned(RecipeNum) = 1 Then
            CheckLearnedRecipe = True
        End If
    End Function

    Friend Sub LearnRecipe(index as integer, RecipeNum As Integer, InvNum As Integer)
        If CheckLearnedRecipe(Index, RecipeNum) Then ' we know this one allready
            PlayerMsg(Index, "You allready know this recipe!", ColorType.BrightRed)
        Else ' lets learn it
            Player(Index).Character(TempPlayer(Index).CurChar).RecipeLearned(RecipeNum) = 1

            PlayerMsg(Index, "You learned the " & Recipe(RecipeNum).Name & " recipe!", ColorType.BrightGreen)

            TakeInvItem(Index, GetPlayerInvItemNum(Index, InvNum), 0)

            SavePlayer(Index)
            SendPlayerData(Index)
        End If
    End Sub

    Friend Sub StartCraft(index as integer, RecipeNum As Integer, Amount As Integer)

        If TempPlayer?(Index).IsCrafting Then
            TempPlayer(Index).CraftRecipe = RecipeNum
            TempPlayer(Index).CraftAmount = Amount

            TempPlayer(Index).CraftTimer = GetTimeMs()
            TempPlayer(Index).CraftTimeNeeded = Recipe(RecipeNum).CreateTime

            TempPlayer(Index).CraftIt = 1
        End If

    End Sub

    Friend Sub UpdateCraft(index as integer)
        Dim i As Integer

        'ok, we made the item, give and take the shit
        If GiveInvItem(Index, Recipe(TempPlayer(Index).CraftRecipe).MakeItemNum, Recipe(TempPlayer(Index).CraftRecipe).MakeItemAmount, True) Then
            For i = 1 To MAX_INGREDIENT
                TakeInvItem(Index, Recipe(TempPlayer(Index).CraftRecipe).Ingredients(i).ItemNum, Recipe(TempPlayer(Index).CraftRecipe).Ingredients(i).Value)
            Next
            PlayerMsg(Index, "You created " & Trim(Item(Recipe(TempPlayer(Index).CraftRecipe).MakeItemNum).Name) & " X " & Recipe(TempPlayer(Index).CraftRecipe).MakeItemAmount, ColorType.BrightGreen)
        End If

        If TempPlayer?(Index).IsCrafting Then
            TempPlayer(Index).CraftAmount = TempPlayer(Index).CraftAmount - 1

            If TempPlayer(Index).CraftAmount > 0 Then
                TempPlayer(Index).CraftTimer = GetTimeMs()
                TempPlayer(Index).CraftTimeNeeded = Recipe(TempPlayer(Index).CraftRecipe).CreateTime

                TempPlayer(Index).CraftIt = 1

                SendCraftUpdate(Index, 0)
            End If

            SendCraftUpdate(Index, 1)
        End If

    End Sub

#End Region

End Module