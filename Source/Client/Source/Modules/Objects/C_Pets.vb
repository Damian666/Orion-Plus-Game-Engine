Imports System.Drawing
Imports ASFW

Module C_Pets

#Region "Globals etc"

    Friend Pet() As PetRec

    Friend Const PetbarTop As Byte = 2
    Friend Const PetbarLeft As Byte = 2
    Friend Const PetbarOffsetX As Byte = 4
    Friend Const MaxPetbar As Byte = 7
    Friend Const PetHpBarWidth As Integer = 129
    Friend Const PetMpBarWidth As Integer = 129

    Friend PetSkillBuffer As Integer
    Friend PetSkillBufferTimer As Integer
    Friend PetSkillCd() As Integer

    Friend ShowPetStats As Boolean

    'Pet Constants
    Friend Const PetBehaviourFollow As Byte = 0 'The pet will attack all npcs around

    Friend Const PetBehaviourGoto As Byte = 1 'If attacked, the pet will fight back
    Friend Const PetAttackBehaviourAttackonsight As Byte = 2 'The pet will attack all npcs around
    Friend Const PetAttackBehaviourGuard As Byte = 3 'If attacked, the pet will fight back
    Friend Const PetAttackBehaviourDonothing As Byte = 4 'The pet will not attack even if attacked

    Friend Structure PetRec
        Dim Num As Integer
        Dim Name As String
        Dim Sprite As Integer

        Dim Range As Integer

        Dim Level As Integer

        Dim MaxLevel As Integer
        Dim ExpGain As Integer
        Dim LevelPnts As Integer

        Dim StatType As Byte '1 for set stats, 2 for relation to owner's stats
        Dim LevelingType As Byte '0 for leveling on own, 1 for not leveling

        Dim Stat() As Byte

        Dim Skill() As Integer

        Dim Evolvable As Byte
        Dim EvolveLevel As Integer
        Dim EvolveNum As Integer
    End Structure

    Friend Structure PlayerPetRec
        Dim Num As Integer
        Dim Health As Integer
        Dim Mana As Integer
        Dim Level As Integer
        Dim Stat() As Byte
        Dim Skill() As Integer
        Dim Points As Integer
        Dim X As Integer
        Dim Y As Integer
        Dim Dir As Integer
        Dim MaxHp As Integer
        Dim MaxMp As Integer
        Dim Alive As Byte
        Dim AttackBehaviour As Integer
        Dim Exp As Integer
        Dim Tnl As Integer

        'Client Use Only
        Dim XOffset As Integer

        Dim YOffset As Integer
        Dim Moving As Byte
        Dim Attacking As Byte
        Dim AttackTimer As Integer
        Dim Steps As Byte
        Dim Damage As Integer
    End Structure

#End Region

#Region "Database"

    Sub ClearPet(index As Integer)

        Pet(index).Name = ""

        ReDim Pet(index).Stat(StatType.Count - 1)
        ReDim Pet(index).Skill(4)
    End Sub

    Sub ClearPets()
        Dim i As Integer

        ReDim Pet(MAX_PETS)
        ReDim PetSkillCd(4)

        For i = 1 To MAX_PETS
            ClearPet(i)
        Next

    End Sub

#End Region

#Region "Outgoing Packets"

    Friend Sub SendPetBehaviour(index As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CSetBehaviour)

        buffer.WriteInt32(index)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

    Sub SendTrainPetStat(statNum As Byte)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CPetUseStatPoint)

        buffer.WriteInt32(statNum)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

    Sub SendRequestPets()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestPets)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

    Sub SendUsePetSkill(skill As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CPetSkill)
        buffer.WriteInt32(skill)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()

        PetSkillBuffer = skill
        PetSkillBufferTimer = GetTickCount()
    End Sub

    Sub SendSummonPet()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CSummonPet)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

    Sub SendReleasePet()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CReleasePet)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

    Friend Sub SendRequestEditPet()
        Dim buffer As ByteStream
        buffer = New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestEditPet)

        Socket.SendData(buffer.Data, buffer.Head)

        buffer.Dispose()

    End Sub

    Friend Sub SendSavePet(petNum As Integer)
        Dim buffer As ByteStream
        Dim i As Integer

        buffer = New ByteStream(4)
        buffer.WriteInt32(ClientPackets.CSavePet)
        buffer.WriteInt32(petNum)

        With Pet(petNum)
            buffer.WriteInt32(.Num)
            buffer.WriteString((Trim$(.Name)))
            buffer.WriteInt32(.Sprite)
            buffer.WriteInt32(.Range)
            buffer.WriteInt32(.Level)
            buffer.WriteInt32(.MaxLevel)
            buffer.WriteInt32(.ExpGain)
            buffer.WriteInt32(.LevelPnts)
            buffer.WriteInt32(.StatType)
            buffer.WriteInt32(.LevelingType)

            For i = 1 To StatType.Count - 1
                buffer.WriteInt32(.Stat(i))
            Next

            For i = 1 To 4
                buffer.WriteInt32(.Skill(i))
            Next

            buffer.WriteInt32(.Evolvable)
            buffer.WriteInt32(.EvolveLevel)
            buffer.WriteInt32(.EvolveNum)
        End With

        Socket.SendData(buffer.Data, buffer.Head)

        buffer.Dispose()

    End Sub
#End Region

#Region "Incoming Packets"

    Friend Sub Packet_UpdatePlayerPet(ByRef data() As Byte)
        Dim n As Integer, i As Integer
        Dim buffer As New ByteStream(data)
        n = buffer.ReadInt32

        'pet
        Player(n).Pet.Num = buffer.ReadInt32
        Player(n).Pet.Health = buffer.ReadInt32
        Player(n).Pet.Mana = buffer.ReadInt32
        Player(n).Pet.Level = buffer.ReadInt32

        For i = 1 To StatType.Count - 1
            Player(n).Pet.Stat(i) = buffer.ReadInt32
        Next

        For i = 1 To 4
            Player(n).Pet.Skill(i) = buffer.ReadInt32
        Next

        Player(n).Pet.X = buffer.ReadInt32
        Player(n).Pet.Y = buffer.ReadInt32
        Player(n).Pet.Dir = buffer.ReadInt32

        Player(n).Pet.MaxHp = buffer.ReadInt32
        Player(n).Pet.MaxMp = buffer.ReadInt32

        Player(n).Pet.Alive = buffer.ReadInt32

        Player(n).Pet.AttackBehaviour = buffer.ReadInt32
        Player(n).Pet.Points = buffer.ReadInt32
        Player(n).Pet.Exp = buffer.ReadInt32
        Player(n).Pet.Tnl = buffer.ReadInt32

        buffer.Dispose()
    End Sub

    Friend Sub Packet_UpdatePet(ByRef data() As Byte)
        Dim n As Integer, i As Integer
        Dim buffer As New ByteStream(data)
        n = buffer.ReadInt32

        With Pet(n)
            .Num = buffer.ReadInt32
            .Name = buffer.ReadString
            .Sprite = buffer.ReadInt32
            .Range = buffer.ReadInt32
            .Level = buffer.ReadInt32
            .MaxLevel = buffer.ReadInt32
            .ExpGain = buffer.ReadInt32
            .LevelPnts = buffer.ReadInt32
            .StatType = buffer.ReadInt32
            .LevelingType = buffer.ReadInt32

            For i = 1 To StatType.Count - 1
                .Stat(i) = buffer.ReadInt32
            Next

            For i = 1 To 4
                .Skill(i) = buffer.ReadInt32
            Next

            .Evolvable = buffer.ReadInt32
            .EvolveLevel = buffer.ReadInt32
            .EvolveNum = buffer.ReadInt32
        End With

        buffer.Dispose()

    End Sub

    Friend Sub Packet_PetMove(ByRef data() As Byte)
        Dim i As Integer, x As Integer, y As Integer
        Dim dir As Integer, movement As Integer
        Dim buffer As New ByteStream(data)
        i = buffer.ReadInt32
        x = buffer.ReadInt32
        y = buffer.ReadInt32
        dir = buffer.ReadInt32
        movement = buffer.ReadInt32

        With Player(i).Pet
            .X = x
            .Y = y
            .Dir = dir
            .XOffset = 0
            .YOffset = 0
            .Moving = movement

            Select Case .Dir
                Case DirectionType.Up
                    .YOffset = PicY
                Case DirectionType.Down
                    .YOffset = PicY * -1
                Case DirectionType.Left
                    .XOffset = PicX
                Case DirectionType.Right
                    .XOffset = PicX * -1
            End Select
        End With

        buffer.Dispose()
    End Sub

    Friend Sub Packet_PetDir(ByRef data() As Byte)
        Dim i As Integer
        Dim dir As Integer
        Dim buffer As New ByteStream(data)
        i = buffer.ReadInt32
        dir = buffer.ReadInt32

        Player(i).Pet.Dir = dir

        buffer.Dispose()
    End Sub

    Friend Sub Packet_PetVital(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)
        i = buffer.ReadInt32

        If buffer.ReadInt32 = 1 Then
            Player(i).Pet.MaxHp = buffer.ReadInt32
            Player(i).Pet.Health = buffer.ReadInt32
        Else
            Player(i).Pet.MaxMp = buffer.ReadInt32
            Player(i).Pet.Mana = buffer.ReadInt32
        End If

        buffer.Dispose()

    End Sub

    Friend Sub Packet_ClearPetSkillBuffer(ByRef data() As Byte)
        PetSkillBuffer = 0
        PetSkillBufferTimer = 0
    End Sub

    Friend Sub Packet_PetAttack(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)
        i = buffer.ReadInt32

        ' Set pet to attacking
        Player(i).Pet.Attacking = 1
        Player(i).Pet.AttackTimer = GetTickCount()

        buffer.Dispose()
    End Sub

    Friend Sub Packet_PetXY(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)
        Player(i).Pet.X = buffer.ReadInt32
        Player(i).Pet.Y = buffer.ReadInt32

        buffer.Dispose()
    End Sub

    Friend Sub Packet_PetExperience(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)
        Player(Myindex).Pet.Exp = buffer.ReadInt32
        Player(Myindex).Pet.Tnl = buffer.ReadInt32

        buffer.Dispose()
    End Sub

#End Region

#Region "Movement"

    Sub ProcessPetMovement(index As Integer)

        ' Check if pet is walking, and if so process moving them over

        If Player(index).Pet.Moving = MovementType.Walking Then

            Select Case Player(index).Pet.Dir
                Case DirectionType.Up
                    Player(index).Pet.YOffset = Player(index).Pet.YOffset - ((ElapsedTime / 1000) * (WalkSpeed * SizeX))
                    If Player(index).Pet.YOffset < 0 Then Player(index).Pet.YOffset = 0

                Case DirectionType.Down
                    Player(index).Pet.YOffset = Player(index).Pet.YOffset + ((ElapsedTime / 1000) * (WalkSpeed * SizeX))
                    If Player(index).Pet.YOffset > 0 Then Player(index).Pet.YOffset = 0

                Case DirectionType.Left
                    Player(index).Pet.XOffset = Player(index).Pet.XOffset - ((ElapsedTime / 1000) * (WalkSpeed * SizeX))
                    If Player(index).Pet.XOffset < 0 Then Player(index).Pet.XOffset = 0

                Case DirectionType.Right
                    Player(index).Pet.XOffset = Player(index).Pet.XOffset + ((ElapsedTime / 1000) * (WalkSpeed * SizeX))
                    If Player(index).Pet.XOffset > 0 Then Player(index).Pet.XOffset = 0

            End Select

            ' Check if completed walking over to the next tile
            If Player(index).Pet.Moving > 0 Then
                If Player(index).Pet.Dir = DirectionType.Right OrElse Player(index).Pet.Dir = DirectionType.Down Then
                    If (Player(index).Pet.XOffset >= 0) AndAlso (Player(index).Pet.YOffset >= 0) Then
                        Player(index).Pet.Moving = 0
                        If Player(index).Pet.Steps = 1 Then
                            Player(index).Pet.Steps = 2
                        Else
                            Player(index).Pet.Steps = 1
                        End If
                    End If
                Else
                    If (Player(index).Pet.XOffset <= 0) AndAlso (Player(index).Pet.YOffset <= 0) Then
                        Player(index).Pet.Moving = 0
                        If Player(index).Pet.Steps = 1 Then
                            Player(index).Pet.Steps = 2
                        Else
                            Player(index).Pet.Steps = 1
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Friend Sub PetMove(x As Integer, y As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CPetMove)

        buffer.WriteInt32(x)
        buffer.WriteInt32(y)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

#End Region

#Region "Drawing"

    Friend Sub DrawPet(index As Integer)
        Dim anim As Byte, x As Integer, y As Integer
        Dim sprite As Integer, spriteleft As Integer
        Dim srcrec As Rectangle
        Dim attackspeed As Integer

        sprite = Pet(Player(index).Pet.Num).Sprite

        If sprite < 1 OrElse sprite > NumCharacters Then Exit Sub

        attackspeed = 1000

        ' Reset frame
        If Player(index).Pet.Steps = 3 Then
            anim = 0
        ElseIf Player(index).Pet.Steps = 1 Then
            anim = 2
        ElseIf Player(index).Pet.Steps = 2 Then
            anim = 3
        End If

        ' Check for attacking animation
        If Player(index).Pet.AttackTimer + (attackspeed / 2) > GetTickCount() Then
            If Player(index).Pet.Attacking = 1 Then
                anim = 3
            End If
        Else
            ' If not attacking, walk normally
            Select Case Player(index).Pet.Dir
                Case DirectionType.Up
                    If (Player(index).Pet.YOffset > 8) Then anim = Player(index).Pet.Steps
                Case DirectionType.Down
                    If (Player(index).Pet.YOffset < -8) Then anim = Player(index).Pet.Steps
                Case DirectionType.Left
                    If (Player(index).Pet.XOffset > 8) Then anim = Player(index).Pet.Steps
                Case DirectionType.Right
                    If (Player(index).Pet.XOffset < -8) Then anim = Player(index).Pet.Steps
            End Select
        End If

        ' Check to see if we want to stop making him attack
        With Player(index).Pet
            If .AttackTimer + attackspeed < GetTickCount() Then
                .Attacking = 0
                .AttackTimer = 0
            End If
        End With

        ' Set the left
        Select Case Player(index).Pet.Dir
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
        x = Player(index).Pet.X * PicX + Player(index).Pet.XOffset - ((CharacterGfxInfo(sprite).Width / 4 - 32) / 2)

        ' Is the player's height more than 32..?
        If (CharacterGfxInfo(sprite).Height / 4) > 32 Then
            ' Create a 32 pixel offset for larger sprites
            y = Player(index).Pet.Y * PicY + Player(index).Pet.YOffset - ((CharacterGfxInfo(sprite).Width / 4) - 32)
        Else
            ' Proceed as normal
            y = Player(index).Pet.Y * PicY + Player(index).Pet.YOffset
        End If

        ' render the actual sprite
        DrawCharacter(sprite, x, y, srcrec)

    End Sub

    Friend Sub DrawPlayerPetName(index As Integer)
        Dim textX As Integer
        Dim textY As Integer
        Dim color As SFML.Graphics.Color, backcolor As SFML.Graphics.Color
        Dim name As String

        ' Check access level
        If GetPlayerPk(index) = False Then

            Select Case GetPlayerAccess(index)
                Case 0
                    color = SFML.Graphics.Color.Red
                    backcolor = SFML.Graphics.Color.Black
                Case 1
                    color = SFML.Graphics.Color.Black
                    backcolor = SFML.Graphics.Color.White
                Case 2
                    color = SFML.Graphics.Color.Cyan
                    backcolor = SFML.Graphics.Color.Black
                Case 3
                    color = SFML.Graphics.Color.Green
                    backcolor = SFML.Graphics.Color.Black
                Case 4
                    color = SFML.Graphics.Color.Yellow
                    backcolor = SFML.Graphics.Color.Black
            End Select
        Else
            color = SFML.Graphics.Color.Red
        End If

        name = Trim$(GetPlayerName(index)) & "'s " & Trim$(Pet(Player(index).Pet.Num).Name)
        ' calc pos
        textX = ConvertMapX(Player(index).Pet.X * PicX) + Player(index).Pet.XOffset + (PicX \ 2) - GetTextWidth(name) / 2
        If Pet(Player(index).Pet.Num).Sprite < 1 OrElse Pet(Player(index).Pet.Num).Sprite > NumCharacters Then
            textY = ConvertMapY(Player(index).Pet.Y * PicY) + Player(index).Pet.YOffset - 16
        Else
            ' Determine location for text
            textY = ConvertMapY(Player(index).Pet.Y * PicY) + Player(index).Pet.YOffset - (CharacterGfxInfo(Pet(Player(index).Pet.Num).Sprite).Height / 4) + 16
        End If

        ' Draw name
        DrawText(textX, textY, Trim$(name), color, backcolor, GameWindow)

    End Sub

    Sub DrawPetBar()
        Dim skillnum As Integer, skillpic As Integer
        Dim rec As Rectangle, recPos As Rectangle

        If Not HasPet(Myindex) Then Exit Sub

        If Not PetAlive(Myindex) Then
            RenderSprite(PetBarSprite, GameWindow, PetbarX, PetbarY, 0, 0, 32, PetbarGfxInfo.Height)
        Else
            RenderSprite(PetBarSprite, GameWindow, PetbarX, PetbarY, 0, 0, PetbarGfxInfo.Width, PetbarGfxInfo.Height)

            For i = 1 To 4
                skillnum = Player(Myindex).Pet.Skill(i)

                If skillnum > 0 Then
                    skillpic = Skill(skillnum).Icon

                    If SkillIconsGfxInfo(skillpic).IsLoaded = False Then
                        LoadTexture(skillpic, 9)
                    End If

                    'seeying we still use it, lets update timer
                    With SkillIconsGfxInfo(skillpic)
                        .TextureTimer = GetTickCount() + 100000
                    End With

                    With rec
                        .Y = 0
                        .Height = 32
                        .X = 0
                        .Width = 32
                    End With

                    If Not PetSkillCd(i) = 0 Then
                        rec.X = 32
                        rec.Width = 32
                    End If

                    With recPos
                        .Y = PetbarY + PetbarTop
                        .Height = PicY
                        .X = PetbarX + PetbarLeft + ((PetbarOffsetX - 2) + 32) * (((i - 1) + 3))
                        .Width = PicX
                    End With

                    RenderSprite(SkillIconsSprite(skillpic), GameWindow, recPos.X, recPos.Y, rec.X, rec.Y, rec.Width, rec.Height)
                End If
            Next
        End If

    End Sub

    Sub DrawPetStats()
        Dim sprite As Integer, rec As Rectangle

        If Not HasPet(Myindex) Then Exit Sub

        If Not ShowPetStats Then Exit Sub

        'draw panel
        RenderSprite(PetStatsSprite, GameWindow, PetStatX, PetStatY, 0, 0, PetStatsGfxInfo.Width, PetStatsGfxInfo.Height)

        'lets get player sprite to render
        sprite = Pet(Player(Myindex).Pet.Num).Sprite

        With rec
            .Y = 0
            .Height = CharacterGfxInfo(sprite).Height / 4
            .X = 0
            .Width = CharacterGfxInfo(sprite).Width / 4
        End With

        Dim petname As String = Trim$(Pet(Player(Myindex).Pet.Num).Name)

        DrawText(PetStatX + 70, PetStatY + 10, petname & " Lvl: " & Player(Myindex).Pet.Level, SFML.Graphics.Color.Yellow, SFML.Graphics.Color.Black, GameWindow)

        RenderSprite(CharacterSprite(sprite), GameWindow, PetStatX + 10, PetStatY + 10 + (PetStatsGfxInfo.Height / 4) - (rec.Height / 2), rec.X, rec.Y, rec.Width, rec.Height)

        'stats
        DrawText(PetStatX + 65, PetStatY + 50, "Strength: " & Player(Myindex).Pet.Stat(StatType.Strength), SFML.Graphics.Color.Yellow, SFML.Graphics.Color.Black, GameWindow)
        DrawText(PetStatX + 65, PetStatY + 65, "Endurance: " & Player(Myindex).Pet.Stat(StatType.Endurance), SFML.Graphics.Color.Yellow, SFML.Graphics.Color.Black, GameWindow)
        DrawText(PetStatX + 65, PetStatY + 80, "Vitality: " & Player(Myindex).Pet.Stat(StatType.Vitality), SFML.Graphics.Color.Yellow, SFML.Graphics.Color.Black, GameWindow)

        DrawText(PetStatX + 165, PetStatY + 50, "Luck: " & Player(Myindex).Pet.Stat(StatType.Luck), SFML.Graphics.Color.Yellow, SFML.Graphics.Color.Black, GameWindow)
        DrawText(PetStatX + 165, PetStatY + 65, "Intelligence: " & Player(Myindex).Pet.Stat(StatType.Intelligence), SFML.Graphics.Color.Yellow, SFML.Graphics.Color.Black, GameWindow)
        DrawText(PetStatX + 165, PetStatY + 80, "Spirit: " & Player(Myindex).Pet.Stat(StatType.Spirit), SFML.Graphics.Color.Yellow, SFML.Graphics.Color.Black, GameWindow)

        DrawText(PetStatX + 65, PetStatY + 95, "Experience: " & Player(Myindex).Pet.Exp & "/" & Player(Myindex).Pet.Tnl, SFML.Graphics.Color.Yellow, SFML.Graphics.Color.Black, GameWindow)
    End Sub

#End Region

#Region "Misc"

    Friend Function PetAlive(index As Integer) As Boolean
        PetAlive = False

        If Player(index).Pet.Alive = 1 Then
            PetAlive = True
        End If

    End Function

    Friend Function HasPet(index As Integer) As Boolean
        HasPet = False

        If Player(index).Pet.Num > 0 Then
            HasPet = True
        End If
    End Function

    Friend Function IsPetBarSlot(x As Single, y As Single) As Integer
        Dim tempRec As Rect
        Dim i As Integer

        IsPetBarSlot = 0

        For i = 1 To MaxPetbar

            With tempRec
                .Top = PetbarY + PetbarTop
                .Bottom = .Top + PicY
                .Left = PetbarX + PetbarLeft + ((PetbarOffsetX + 32) * (((i - 1) Mod MaxPetbar)))
                .Right = .Left + PicX
            End With

            If x >= tempRec.Left AndAlso x <= tempRec.Right Then
                If y >= tempRec.Top AndAlso y <= tempRec.Bottom Then
                    IsPetBarSlot = i
                    Exit Function
                End If
            End If
        Next

    End Function

#End Region

#Region "Editor"

    Friend Sub PetEditorInit()
        Dim i As Integer

        If frmEditor_Pet.Visible = False Then Exit Sub
        Editorindex = frmEditor_Pet.lstIndex.SelectedIndex + 1

        With frmEditor_Pet
            'populate skill combo's
            .cmbSkill1.Items.Clear()
            .cmbSkill2.Items.Clear()
            .cmbSkill3.Items.Clear()
            .cmbSkill4.Items.Clear()

            .cmbSkill1.Items.Add("None")
            .cmbSkill2.Items.Add("None")
            .cmbSkill3.Items.Add("None")
            .cmbSkill4.Items.Add("None")

            For i = 1 To MAX_SKILLS
                .cmbSkill1.Items.Add(i & ": " & Skill(i).Name)
                .cmbSkill2.Items.Add(i & ": " & Skill(i).Name)
                .cmbSkill3.Items.Add(i & ": " & Skill(i).Name)
                .cmbSkill4.Items.Add(i & ": " & Skill(i).Name)
            Next
            .txtName.Text = Trim$(Pet(Editorindex).Name)
            If Pet(Editorindex).Sprite < 0 OrElse Pet(Editorindex).Sprite > .nudSprite.Maximum Then Pet(Editorindex).Sprite = 0

            .nudSprite.Value = Pet(Editorindex).Sprite
            .EditorPet_DrawPet()

            .nudRange.Value = Pet(Editorindex).Range

            .nudStrength.Value = Pet(Editorindex).Stat(StatType.Strength)
            .nudEndurance.Value = Pet(Editorindex).Stat(StatType.Endurance)
            .nudVitality.Value = Pet(Editorindex).Stat(StatType.Vitality)
            .nudLuck.Value = Pet(Editorindex).Stat(StatType.Luck)
            .nudIntelligence.Value = Pet(Editorindex).Stat(StatType.Intelligence)
            .nudSpirit.Value = Pet(Editorindex).Stat(StatType.Spirit)
            .nudLevel.Value = Pet(Editorindex).Level

            If Pet(Editorindex).StatType = 1 Then
                .optCustomStats.Checked = True
                .pnlCustomStats.Visible = True
            Else
                .optAdoptStats.Checked = True
                .pnlCustomStats.Visible = False
            End If

            .nudPetExp.Value = Pet(Editorindex).ExpGain

            .nudPetPnts.Value = Pet(Editorindex).LevelPnts

            .nudMaxLevel.Value = Pet(Editorindex).MaxLevel

            'Set skills
            .cmbSkill1.SelectedIndex = Pet(Editorindex).Skill(1)

            .cmbSkill2.SelectedIndex = Pet(Editorindex).Skill(2)

            .cmbSkill3.SelectedIndex = Pet(Editorindex).Skill(3)

            .cmbSkill4.SelectedIndex = Pet(Editorindex).Skill(4)

            If Pet(Editorindex).LevelingType = 1 Then
                .optLevel.Checked = True

                .pnlPetlevel.Visible = True
                .pnlPetlevel.BringToFront()
                .nudPetExp.Value = Pet(Editorindex).ExpGain
                If Pet(Editorindex).MaxLevel > 0 Then .nudMaxLevel.Value = Pet(Editorindex).MaxLevel
                .nudPetPnts.Value = Pet(Editorindex).LevelPnts
            Else
                .optDoNotLevel.Checked = True

                .pnlPetlevel.Visible = False
                .nudPetExp.Value = Pet(Editorindex).ExpGain
                .nudMaxLevel.Value = Pet(Editorindex).MaxLevel
                .nudPetPnts.Value = Pet(Editorindex).LevelPnts
            End If

            If Pet(Editorindex).Evolvable = 1 Then
                .chkEvolve.Checked = True
            Else
                .chkEvolve.Checked = False
            End If

            .nudEvolveLvl.Value = Pet(Editorindex).EvolveLevel
            .cmbEvolve.SelectedIndex = Pet(Editorindex).EvolveNum
        End With

        ClearChanged_Pet()

        Pet_Changed(Editorindex) = True

    End Sub

    Friend Sub PetEditorOk()
        Dim i As Integer

        For i = 1 To MAX_PETS
            If Pet_Changed(i) Then
                SendSavePet(i)
            End If
        Next

        frmEditor_Pet.Dispose()

        Editor = 0
        ClearChanged_Pet()

    End Sub

    Friend Sub PetEditorCancel()

        Editor = 0

        frmEditor_Pet.Dispose()

        ClearChanged_Pet()
        ClearPets()
        SendRequestPets()

    End Sub

    Friend Sub ClearChanged_Pet()

        ReDim Pet_Changed(MAX_PETS)

    End Sub

#End Region

End Module