Imports Asfw

Module C_Classes

#Region "Incoming Traffic"

    Sub Packet_NewCharClasses(ByRef data() As Byte)
        Dim i As Integer, z As Integer, x As Integer
        Dim buffer As New ByteStream(data)

        SelectedChar = 1

        For i = 1 To MAX_CLASSES

            With Classes(i)
                .Name = Trim(buffer.ReadString)
                .Desc = Trim(buffer.ReadString)

                ReDim .Vital(VitalType.Count - 1)

                .Vital(VitalType.HP) = buffer.ReadInt32
                .Vital(VitalType.MP) = buffer.ReadInt32
                .Vital(VitalType.SP) = buffer.ReadInt32

                ' get array size
                z = buffer.ReadInt32
                ' redim array
                ReDim .MaleSprite(z + 1)
                ' loop-receive data
                For x = 1 To z + 1
                    .MaleSprite(x) = buffer.ReadInt32
                Next

                ' get array size
                z = buffer.ReadInt32
                ' redim array
                ReDim .FemaleSprite(z + 1)
                ' loop-receive data
                For x = 1 To z + 1
                    .FemaleSprite(x) = buffer.ReadInt32
                Next

                ReDim .Stat(StatType.Count - 1)

                .Stat(StatType.Strength) = buffer.ReadInt32
                .Stat(StatType.Endurance) = buffer.ReadInt32
                .Stat(StatType.Vitality) = buffer.ReadInt32
                .Stat(StatType.Intelligence) = buffer.ReadInt32
                .Stat(StatType.Luck) = buffer.ReadInt32
                .Stat(StatType.Spirit) = buffer.ReadInt32

                ReDim .StartItem(5)
                ReDim .StartValue(5)
                For q = 1 To 5
                    .StartItem(q) = buffer.ReadInt32
                    .StartValue(q) = buffer.ReadInt32
                Next

                .StartMap = buffer.ReadInt32
                .StartX = buffer.ReadInt32
                .StartY = buffer.ReadInt32

                .BaseExp = buffer.ReadInt32
            End With

        Next

        buffer.Dispose()

        ' Used for if the player is creating a new character
        Frmmenuvisible = True
        Pnlloadvisible = False
        PnlCreditsVisible = False
        PnlRegisterVisible = False
        PnlCharCreateVisible = True
        PnlLoginVisible = False

        ReDim Cmbclass(MAX_CLASSES)

        For i = 1 To MAX_CLASSES
            Cmbclass(i) = Classes(i).Name
        Next

        FrmMenu.DrawCharacter()

        NewCharSprite = 1
    End Sub

    Sub Packet_ClassesData(ByRef data() As Byte)
        Dim i As Integer, z As Integer, x As Integer
        Dim buffer As New ByteStream(data)

        SelectedChar = 1

        For i = 1 To MAX_CLASSES

            With Classes(i)
                .Name = Trim(buffer.ReadString)
                .Desc = Trim(buffer.ReadString)

                ReDim .Vital(VitalType.Count - 1)

                .Vital(VitalType.HP) = buffer.ReadInt32
                .Vital(VitalType.MP) = buffer.ReadInt32
                .Vital(VitalType.SP) = buffer.ReadInt32

                ' get array size
                z = buffer.ReadInt32
                ' redim array
                ReDim .MaleSprite(z + 1)
                ' loop-receive data
                For x = 1 To z + 1
                    .MaleSprite(x) = buffer.ReadInt32
                Next

                ' get array size
                z = buffer.ReadInt32
                ' redim array
                ReDim .FemaleSprite(z + 1)
                ' loop-receive data
                For x = 1 To z + 1
                    .FemaleSprite(x) = buffer.ReadInt32
                Next

                ReDim .Stat(StatType.Count - 1)

                .Stat(StatType.Strength) = buffer.ReadInt32
                .Stat(StatType.Endurance) = buffer.ReadInt32
                .Stat(StatType.Vitality) = buffer.ReadInt32
                .Stat(StatType.Intelligence) = buffer.ReadInt32
                .Stat(StatType.Luck) = buffer.ReadInt32
                .Stat(StatType.Spirit) = buffer.ReadInt32

                ReDim .StartItem(5)
                ReDim .StartValue(5)
                For q = 1 To 5
                    .StartItem(q) = buffer.ReadInt32
                    .StartValue(q) = buffer.ReadInt32
                Next

                .StartMap = buffer.ReadInt32
                .StartX = buffer.ReadInt32
                .StartY = buffer.ReadInt32

                .BaseExp = buffer.ReadInt32
            End With

        Next

        ReDim Cmbclass(MAX_CLASSES)
        For i = 1 To MAX_CLASSES
            Cmbclass(i) = Classes(i).Name
        Next
        FrmMenu.DrawCharacter()
        NewCharSprite = 1

        buffer.Dispose()
    End Sub

#End Region

#Region "Outgoing Traffic"

    Friend Sub SendRequestClasses()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestClasses)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

#End Region

End Module