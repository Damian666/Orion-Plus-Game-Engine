Imports System.IO
Imports ASFW

Friend Module E_Projectiles

#Region "Defines"

    Friend Const MAX_PROJECTILES As Integer = 255
    Friend Projectiles(MAX_PROJECTILES) As ProjectileRec
    Friend MapProjectiles(MAX_PROJECTILES) As MapProjectileRec
    Friend NumProjectiles As Integer
    Friend InitProjectileEditor As Boolean
    Friend Const EDITOR_PROJECTILE As Byte = 10
    Friend Projectile_Changed(MAX_PROJECTILES) As Boolean

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
        Dim dir As Byte
        Dim Range As Integer
        Dim TravelTime As Integer
        Dim Timer As Integer
    End Structure

#End Region

#Region "Sending"

    Sub SendRequestEditProjectiles()
        Dim buffer As ByteStream

        buffer = New ByteStream(4)
        buffer.WriteInt32(EditorPackets.RequestEditProjectiles)
        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

    Sub SendSaveProjectile(ProjectileNum As Integer)
        Dim buffer As ByteStream

        buffer = New ByteStream(4)

        buffer.WriteInt32(EditorPackets.SaveProjectile)
        buffer.WriteInt32(ProjectileNum)

        buffer.WriteString((Trim(Projectiles(ProjectileNum).Name)))
        buffer.WriteInt32(Projectiles(ProjectileNum).Sprite)
        buffer.WriteInt32(Projectiles(ProjectileNum).Range)
        buffer.WriteInt32(Projectiles(ProjectileNum).Speed)
        buffer.WriteInt32(Projectiles(ProjectileNum).Damage)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

    Sub SendRequestProjectiles()
        Dim buffer As ByteStream

        buffer = New ByteStream(4)
        buffer.WriteInt32(ClientPackets.CRequestProjectiles)
        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

    Sub SendClearProjectile(ProjectileNum As Integer, Collisionindex As Integer, CollisionType As Byte, CollisionZone As Integer)
        Dim buffer As ByteStream

        buffer = New ByteStream(4)
        buffer.WriteInt32(ClientPackets.CClearProjectile)
        buffer.WriteInt32(ProjectileNum)
        buffer.WriteInt32(Collisionindex)
        buffer.WriteInt32(CollisionType)
        buffer.WriteInt32(CollisionZone)
        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

#End Region

#Region "Recieving"

    Friend Sub HandleProjectileEditor(ByRef data() As Byte)

        InitProjectileEditor = True

    End Sub

    Friend Sub HandleUpdateProjectile(ByRef data() As Byte)
        Dim ProjectileNum As Integer
        Dim buffer As New ByteStream(data)
        ProjectileNum = buffer.ReadInt32

        Projectiles(ProjectileNum).Name = buffer.ReadString
        Projectiles(ProjectileNum).Sprite = buffer.ReadInt32
        Projectiles(ProjectileNum).Range = buffer.ReadInt32
        Projectiles(ProjectileNum).Speed = buffer.ReadInt32
        Projectiles(ProjectileNum).Damage = buffer.ReadInt32

        buffer.Dispose()

    End Sub

    Friend Sub HandleMapProjectile(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)
        i = buffer.ReadInt32

        With MapProjectiles(i)
            .ProjectileNum = buffer.ReadInt32
            .Owner = buffer.ReadInt32
            .OwnerType = buffer.ReadInt32
            .dir = buffer.ReadInt32
            .X = buffer.ReadInt32
            .Y = buffer.ReadInt32
            .Range = 0
            .Timer = GetTickCount() + 60000
        End With

        buffer.Dispose()

    End Sub

#End Region

#Region "Database"

    Sub ClearProjectiles()
        Dim i As Integer

        For i = 1 To MAX_PROJECTILES
            Call ClearProjectile(i)
        Next

    End Sub

    Sub ClearProjectile(index As Integer)

        Projectiles(index).Name = ""
        Projectiles(index).Sprite = 0
        Projectiles(index).Range = 0
        Projectiles(index).Speed = 0
        Projectiles(index).Damage = 0

    End Sub

    Sub ClearMapProjectile(ProjectileNum As Integer)

        MapProjectiles(ProjectileNum).ProjectileNum = 0
        MapProjectiles(ProjectileNum).Owner = 0
        MapProjectiles(ProjectileNum).OwnerType = 0
        MapProjectiles(ProjectileNum).X = 0
        MapProjectiles(ProjectileNum).Y = 0
        MapProjectiles(ProjectileNum).dir = 0
        MapProjectiles(ProjectileNum).Timer = 0

    End Sub

#End Region

#Region "Drawing"

    Friend Sub CheckProjectiles()
        Dim i As Integer

        i = 1

        While File.Exists(Application.StartupPath & GFX_PATH & "projectiles\" & i & GFX_EXT)

            NumProjectiles = NumProjectiles + 1
            i = i + 1
        End While

        If NumProjectiles = 0 Then Exit Sub

    End Sub

    Friend Sub EditorProjectile_DrawProjectile()
        Dim iconnum As Integer

        iconnum = frmProjectile.nudPic.Value

        If iconnum < 1 OrElse iconnum > NumProjectiles Then
            frmProjectile.picProjectile.BackgroundImage = Nothing
            Exit Sub
        End If

        If File.Exists(Application.StartupPath & GFX_PATH & "Projectiles\" & iconnum & GFX_EXT) Then
            frmProjectile.picProjectile.BackgroundImage = Image.FromFile(Application.StartupPath & GFX_PATH & "Projectiles\" & iconnum & GFX_EXT)
        End If

    End Sub

#End Region

#Region "Projectile Editor"

    Friend Sub ProjectileEditorInit()

        If frmProjectile.Visible = False Then Exit Sub
        Editorindex = frmProjectile.lstIndex.SelectedIndex + 1

        With Projectiles(Editorindex)
            frmProjectile.txtName.Text = Trim$(.Name)
            frmProjectile.nudPic.Value = .Sprite
            frmProjectile.nudRange.Value = .Range
            frmProjectile.nudSpeed.Value = .Speed
            frmProjectile.nudDamage.Value = .Damage
        End With

        Projectile_Changed(Editorindex) = True

    End Sub

    Friend Sub ProjectileEditorOk()
        Dim i As Integer

        For i = 1 To MAX_PROJECTILES
            If Projectile_Changed(i) Then
                Call SendSaveProjectile(i)
            End If
        Next

        frmProjectile.Dispose()
        Editor = 0
        ClearChanged_Projectile()

    End Sub

    Friend Sub ProjectileEditorCancel()

        Editor = 0
        frmProjectile.Dispose()
        ClearChanged_Projectile()
        ClearProjectiles()
        SendRequestProjectiles()

    End Sub

    Friend Sub ClearChanged_Projectile()
        Dim i As Integer

        For i = 0 To MAX_PROJECTILES
            Projectile_Changed(i) = False
        Next

    End Sub

#End Region

End Module