Imports ASFW

Module E_Pets

#Region "Globals etc"

    Friend Pet(MAX_PETS) As PetRec
    Friend Const EDITOR_PET As Byte = 7
    Friend Pet_Changed() As Boolean

    Friend Const PetHpBarWidth As Integer = 129
    Friend Const PetMpBarWidth As Integer = 129

    Friend PetSkillBuffer As Integer
    Friend PetSkillBufferTimer As Integer
    Friend PetSkillCD() As Integer

    Friend InitPetEditor As Boolean

    'Pet Constants
    Friend Const PET_BEHAVIOUR_FOLLOW As Byte = 0 'The pet will attack all npcs around

    Friend Const PET_BEHAVIOUR_GOTO As Byte = 1 'If attacked, the pet will fight back
    Friend Const PET_ATTACK_BEHAVIOUR_ATTACKONSIGHT As Byte = 2 'The pet will attack all npcs around
    Friend Const PET_ATTACK_BEHAVIOUR_GUARD As Byte = 3 'If attacked, the pet will fight back
    Friend Const PET_ATTACK_BEHAVIOUR_DONOTHING As Byte = 4 'The pet will not attack even if attacked

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
        Dim MaxMP As Integer
        Dim Alive As Byte
        Dim AttackBehaviour As Integer
        Dim Exp As Integer
        Dim TNL As Integer

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

#Region "Outgoing Packets"

    Sub SendRequestPets()
        Dim buffer As ByteStream

        buffer = New ByteStream(4)
        buffer.WriteInt32(ClientPackets.CRequestPets)
        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

    Friend Sub SendRequestEditPet()
        Dim buffer As ByteStream
        buffer = New ByteStream(4)

        buffer.WriteInt32(EditorPackets.CRequestEditPet)

        Socket.SendData(buffer.Data, buffer.Head)

        buffer.Dispose()

    End Sub

    Friend Sub SendSavePet(petNum As Integer)
        Dim buffer As ByteStream
        Dim i As Integer

        buffer = New ByteStream(4)
        buffer.WriteInt32(EditorPackets.CSavePet)
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

    Friend Sub Packet_PetEditor(ByRef data() As Byte)
        InitPetEditor = True
    End Sub

    Friend Sub Packet_UpdatePet(ByRef data() As Byte)
        Dim n As Integer, i As Integer
        Dim buffer As New ByteStream(data)
        n = buffer.ReadInt32

        ReDim Pet(n).Stat(StatType.Count - 1)
        ReDim Pet(n).Skill(4)

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

#End Region

#Region "DataBase"

    Sub ClearPet(index As Integer)

        Pet(index).Name = ""

        ReDim Pet(index).Stat(StatType.Count - 1)
        ReDim Pet(index).Skill(4)
    End Sub

    Sub ClearPets()
        Dim i As Integer

        ReDim Pet(MAX_PETS)
        ReDim PetSkillCD(4)

        For i = 1 To MAX_PETS
            ClearPet(i)
        Next

    End Sub

#End Region

#Region "Editor"

    Friend Sub PetEditorInit()
        Dim i As Integer

        If frmPet.Visible = False Then Exit Sub
        Editorindex = frmPet.lstIndex.SelectedIndex + 1

        With frmPet
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

        frmPet.Dispose()

        Editor = 0
        ClearChanged_Pet()

    End Sub

    Friend Sub PetEditorCancel()

        Editor = 0

        frmPet.Dispose()

        ClearChanged_Pet()
        ClearPets()
        SendRequestPets()

    End Sub

    Friend Sub ClearChanged_Pet()

        ReDim Pet_Changed(MAX_PETS)

    End Sub

#End Region

End Module