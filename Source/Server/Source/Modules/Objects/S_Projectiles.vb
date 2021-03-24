Imports System.IO
Imports Asfw
Imports Asfw.IO

Friend Module S_Projectiles

#Region "Defines"

    Friend Const MAX_PROJECTILES As Integer = 255
    Friend Projectiles(MAX_PROJECTILES) As ProjectileRec
    Friend MapProjectiles(,) As MapProjectileRec

#End Region

#Region "Types"

    Friend Structure ProjectileRec
        Dim Name As String
        Dim Sprite As Integer
        Dim Range As Byte
        Dim Speed As Integer
        Dim Damage As Integer
    End Structure

    Friend Structure MapProjectileRec
        Dim ProjectileNum As Integer
        Dim Owner As Integer
        Dim OwnerType As Byte
        Dim X As Integer
        Dim Y As Integer
        Dim Dir As Byte
        Dim Timer As Integer
    End Structure

#End Region

#Region "Database"

    Sub SaveProjectiles()
        Dim i As Integer

        For i = 1 To MAX_PROJECTILES
            SaveProjectile(i)
        Next

    End Sub

    Sub SaveProjectile(ProjectileNum As Integer)
        Dim filename As String

        filename = Path.Projectile(ProjectileNum)

        Dim writer As New ByteStream(100)

        writer.WriteString(Projectiles(ProjectileNum).Name)
        writer.WriteInt32(Projectiles(ProjectileNum).Sprite)
        writer.WriteByte(Projectiles(ProjectileNum).Range)
        writer.WriteInt32(Projectiles(ProjectileNum).Speed)
        writer.WriteInt32(Projectiles(ProjectileNum).Damage)

        ByteFile.Save(filename, writer)

    End Sub

    Sub LoadProjectiles()
        Dim filename As String
        Dim i As Integer

        CheckProjectile()

        For i = 1 To MAX_PROJECTILES
            filename = Path.Projectile(i)
            Dim reader As New ByteStream()
            ByteFile.Load(filename, reader)

            Projectiles(i).Name = reader.ReadString()
            Projectiles(i).Sprite = reader.ReadInt32()
            Projectiles(i).Range = reader.ReadByte()
            Projectiles(i).Speed = reader.ReadInt32()
            Projectiles(i).Damage = reader.ReadInt32()

            Application.DoEvents()
        Next

    End Sub

    Sub CheckProjectile()
        Dim i As Integer

        For i = 1 To MAX_PROJECTILES
            If Not File.Exists(Path.Projectile(i)) Then
                SaveProjectile(i)
            End If
        Next

    End Sub

    Sub ClearMapProjectiles()
        Dim x As Integer
        Dim y As Integer

        ReDim MapProjectiles(MAX_MAPS, MAX_PROJECTILES)
        For x = 1 To MAX_MAPS
            For y = 1 To MAX_PROJECTILES
                ClearMapProjectile(x, y)
            Next
        Next

    End Sub

    Sub ClearMapProjectile(mapNum As Integer, index As Integer)

        MapProjectiles(mapNum, index).ProjectileNum = 0
        MapProjectiles(mapNum, index).Owner = 0
        MapProjectiles(mapNum, index).OwnerType = 0
        MapProjectiles(mapNum, index).X = 0
        MapProjectiles(mapNum, index).Y = 0
        MapProjectiles(mapNum, index).Dir = 0
        MapProjectiles(mapNum, index).Timer = 0

    End Sub

    Sub ClearProjectile(index As Integer)

        Projectiles(index).Name = ""
        Projectiles(index).Sprite = 0
        Projectiles(index).Range = 0
        Projectiles(index).Speed = 0
        Projectiles(index).Damage = 0

    End Sub

    Sub ClearProjectiles()
        Dim i As Integer

        ReDim Projectiles(MAX_PROJECTILES)

        For i = 1 To MAX_PROJECTILES
            ClearProjectile(i)
        Next

    End Sub

#End Region

#Region "Incoming"

    Sub HandleRequestEditProjectiles(index As Integer, ByRef data() As Byte)
        Dim buffer As New ByteStream(4)

        ' Prevent hacking
        If GetPlayerAccess(index) < AdminType.Developer Then Exit Sub

        buffer.WriteInt32(ServerPackets.SProjectileEditor)

        Socket.SendDataTo(index, buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

    Sub HandleSaveProjectile(index As Integer, ByRef data() As Byte)
        Dim ProjectileNum As Integer
        Dim buffer As New ByteStream(data)
        If GetPlayerAccess(index) < AdminType.Developer Then Exit Sub

        ProjectileNum = buffer.ReadInt32

        ' Prevent hacking
        If ProjectileNum < 0 OrElse ProjectileNum > MAX_PROJECTILES Then
            Exit Sub
        End If

        Projectiles(ProjectileNum).Name = buffer.ReadString
        Projectiles(ProjectileNum).Sprite = buffer.ReadInt32
        Projectiles(ProjectileNum).Range = buffer.ReadInt32
        Projectiles(ProjectileNum).Speed = buffer.ReadInt32
        Projectiles(ProjectileNum).Damage = buffer.ReadInt32

        ' Save it
        SendUpdateProjectileToAll(ProjectileNum)
        SaveProjectile(ProjectileNum)
        Addlog(GetPlayerLogin(index) & " saved Projectile #" & ProjectileNum & ".", ADMIN_LOG)
        buffer.Dispose()

    End Sub

    Sub HandleRequestProjectiles(index As Integer, ByRef data() As Byte)

        SendProjectiles(index)

    End Sub

    Sub HandleClearProjectile(index As Integer, ByRef data() As Byte)
        Dim ProjectileNum As Integer
        Dim Targetindex As Integer
        Dim TargetType As TargetType
        Dim TargetZone As Integer
        Dim mapNum As Integer
        Dim Damage As Integer
        Dim armor As Integer
        Dim npcnum As Integer
        Dim buffer As New ByteStream(data)
        ProjectileNum = buffer.ReadInt32
        Targetindex = buffer.ReadInt32
        TargetType = buffer.ReadInt32
        TargetZone = buffer.ReadInt32
        buffer.Dispose()

        mapNum = GetPlayerMap(index)

        Select Case MapProjectiles(mapNum, ProjectileNum).OwnerType
            Case TargetType.Player
                If MapProjectiles(mapNum, ProjectileNum).Owner = index Then
                    Select Case TargetType
                        Case TargetType.Player

                            If IsPlaying(Targetindex) Then
                                If Targetindex <> index Then
                                    If CanPlayerAttackPlayer(index, Targetindex, True) = True Then

                                        ' Get the damage we can do
                                        Damage = GetPlayerDamage(index) + Projectiles(MapProjectiles(mapNum, ProjectileNum).ProjectileNum).Damage

                                        ' if the npc blocks, take away the block amount
                                        armor = CanPlayerBlockHit(Targetindex)
                                        Damage = Damage - armor

                                        ' randomise for up to 10% lower than max hit
                                        Damage = Random(1, Damage)

                                        If Damage < 1 Then Damage = 1

                                        AttackPlayer(index, Targetindex, Damage)
                                    End If
                                End If
                            End If

                        Case TargetType.Npc
                            npcnum = MapNpc(mapNum).Npc(Targetindex).Num
                            If CanPlayerAttackNpc(index, Targetindex, True) = True Then
                                ' Get the damage we can do
                                Damage = GetPlayerDamage(index) + Projectiles(MapProjectiles(mapNum, ProjectileNum).ProjectileNum).Damage

                                ' if the npc blocks, take away the block amount
                                armor = 0
                                Damage = Damage - armor

                                ' randomise from 1 to max hit
                                Damage = Random(1, Damage)

                                If Damage < 1 Then Damage = 1

                                PlayerAttackNpc(index, Targetindex, Damage)
                            End If
                    End Select
                End If

        End Select

        ClearMapProjectile(mapNum, ProjectileNum)

    End Sub

#End Region

#Region "Outgoing"

    Sub SendUpdateProjectileToAll(ProjectileNum As Integer)
        Dim buffer As ByteStream

        buffer = New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SUpdateProjectile)
        buffer.WriteInt32(ProjectileNum)
        buffer.WriteString((Trim(Projectiles(ProjectileNum).Name)))
        buffer.WriteInt32(Projectiles(ProjectileNum).Sprite)
        buffer.WriteInt32(Projectiles(ProjectileNum).Range)
        buffer.WriteInt32(Projectiles(ProjectileNum).Speed)
        buffer.WriteInt32(Projectiles(ProjectileNum).Damage)

        SendDataToAll(buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

    Sub SendUpdateProjectileTo(index As Integer, ProjectileNum As Integer)
        Dim buffer As ByteStream

        buffer = New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SUpdateProjectile)
        buffer.WriteInt32(ProjectileNum)
        buffer.WriteString((Trim(Projectiles(ProjectileNum).Name)))
        buffer.WriteInt32(Projectiles(ProjectileNum).Sprite)
        buffer.WriteInt32(Projectiles(ProjectileNum).Range)
        buffer.WriteInt32(Projectiles(ProjectileNum).Speed)
        buffer.WriteInt32(Projectiles(ProjectileNum).Damage)

        Socket.SendDataTo(index, buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

    Sub SendProjectiles(index As Integer)
        Dim i As Integer

        For i = 1 To MAX_PROJECTILES
            If Len(Trim$(Projectiles(i).Name)) > 0 Then
                Call SendUpdateProjectileTo(index, i)
            End If
        Next

    End Sub

    Sub SendProjectileToMap(mapNum As Integer, ProjectileNum As Integer)
        Dim buffer As ByteStream

        buffer = New ByteStream(4)
        buffer.WriteInt32(ServerPackets.SMapProjectile)

        With MapProjectiles(mapNum, ProjectileNum)
            buffer.WriteInt32(ProjectileNum)
            buffer.WriteInt32(.ProjectileNum)
            buffer.WriteInt32(.Owner)
            buffer.WriteInt32(.OwnerType)
            buffer.WriteInt32(.Dir)
            buffer.WriteInt32(.X)
            buffer.WriteInt32(.Y)
        End With

        SendDataToMap(mapNum, buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

#End Region

#Region "Functions"

    Friend Sub PlayerFireProjectile(index As Integer, Optional IsSkill As Integer = 0)
        Dim ProjectileSlot As Integer
        Dim ProjectileNum As Integer
        Dim mapNum As Integer
        Dim i As Integer

        mapNum = GetPlayerMap(index)

        'Find a free projectile
        For i = 1 To MAX_PROJECTILES
            If MapProjectiles(mapNum, i).ProjectileNum = 0 Then ' Free Projectile
                ProjectileSlot = i
                Exit For
            End If
        Next

        'Check for no projectile, if so just overwrite the first slot
        If ProjectileSlot = 0 Then ProjectileSlot = 1

        'Check for skill, if so then load data acordingly
        If IsSkill > 0 Then
            ProjectileNum = Skill(IsSkill).Projectile
        Else
            ProjectileNum = Item(GetPlayerEquipment(index, EquipmentType.Weapon)).Projectile
        End If

        If ProjectileNum = 0 Then Exit Sub

        With MapProjectiles(mapNum, ProjectileSlot)
            .ProjectileNum = ProjectileNum
            .Owner = index
            .OwnerType = TargetType.Player
            .Dir = GetPlayerDir(index)
            .X = GetPlayerX(index)
            .Y = GetPlayerY(index)
            .Timer = GetTimeMs() + 60000
        End With

        SendProjectileToMap(mapNum, ProjectileSlot)

    End Sub

    Friend Function Engine_GetAngle(CenterX As Integer, CenterY As Integer, targetX As Integer, targetY As Integer) As Single
        '************************************************************
        'Gets the angle between two points in a 2d plane
        '************************************************************
        Dim SideA As Single
        Dim SideC As Single

        On Error GoTo ErrOut

        'Check for horizontal lines (90 or 270 degrees)
        If CenterY = targetY Then
            'Check for going right (90 degrees)
            If CenterX < targetX Then
                Engine_GetAngle = 90
                'Check for going left (270 degrees)
            Else
                Engine_GetAngle = 270
            End If

            'Exit the function
            Exit Function
        End If

        'Check for horizontal lines (360 or 180 degrees)
        If CenterX = targetX Then
            'Check for going up (360 degrees)
            If CenterY > targetY Then
                Engine_GetAngle = 360

                'Check for going down (180 degrees)
            Else
                Engine_GetAngle = 180
            End If

            'Exit the function
            Exit Function
        End If

        'Calculate Side C
        SideC = Math.Sqrt(Math.Abs(targetX - CenterX) ^ 2 + Math.Abs(targetY - CenterY) ^ 2)

        'Side B = CenterY

        'Calculate Side A
        SideA = Math.Sqrt(Math.Abs(targetX - CenterX) ^ 2 + targetY ^ 2)

        'Calculate the angle
        Engine_GetAngle = (SideA ^ 2 - CenterY ^ 2 - SideC ^ 2) / (CenterY * SideC * -2)
        Engine_GetAngle = (Math.Atan(-Engine_GetAngle / Math.Sqrt(-Engine_GetAngle * Engine_GetAngle + 1)) + 1.5708) * 57.29583

        'If the angle is >180, subtract from 360
        If targetX < CenterX Then Engine_GetAngle = 360 - Engine_GetAngle

        'Exit function
        Exit Function

        'Check for error
ErrOut:

        'Return a 0 saying there was an error
        Engine_GetAngle = 0

        Exit Function
    End Function

#End Region

End Module