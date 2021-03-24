Imports System.IO
Imports Asfw.IO.Serialization

#If CLIENT Then
Public Class InputsDef
    Public Class Input
        Public MoveUp As Keys = Keys.W
        Public MoveDown As Keys = Keys.S
        Public MoveLeft As Keys = Keys.A
        Public MoveRight As Keys = Keys.D

        Public Attack As Keys = Keys.ControlKey
        Public Run As Keys = Keys.ShiftKey
        Public Loot As Keys = Keys.Space

        Public HotBar1 As Keys = Keys.NumPad1
        Public HotBar2 As Keys = Keys.NumPad2
        Public HotBar3 As Keys = Keys.NumPad3
        Public HotBar4 As Keys = Keys.NumPad4
        Public HotBar5 As Keys = Keys.NumPad5
        Public HotBar6 As Keys = Keys.NumPad6
        Public HotBar7 As Keys = Keys.NumPad7

        Public Inventory As Keys = Keys.I
        Public Character As Keys = Keys.C
        Public Quest As Keys = Keys.Q
        Public Skill As Keys = Keys.K
        Public Settings As Keys = Keys.N

        Public HudToggle As Keys = Keys.F10
        Public Admin As Keys = Keys.Insert
    End Class

    Public Primary As New Input
    Public Secondary As New Input

    Friend Function MoveUp(keyCode As Keys) As Boolean
        Return keyCode = Primary.MoveUp OrElse
               keyCode = Secondary.MoveUp
    End Function
    Friend Function MoveDown(keyCode As Keys) As Boolean
        Return keyCode = Primary.MoveDown OrElse
               keyCode = Secondary.MoveDown
    End Function
    Friend Function MoveLeft(keyCode As Keys) As Boolean
        Return keyCode = Primary.MoveLeft OrElse
               keyCode = Secondary.MoveLeft
    End Function
    Friend Function MoveRight(keyCode As Keys) As Boolean
        Return keyCode = Primary.MoveRight OrElse
               keyCode = Secondary.MoveRight
    End Function

    Friend Function Attack(keyCode As Keys) As Boolean
        Return keyCode = Primary.Attack OrElse
               keyCode = Secondary.Attack
    End Function
    Friend Function Run(keyCode As Keys) As Boolean
        Return keyCode = Primary.Run OrElse
               keyCode = Secondary.Run
    End Function
    Friend Function Loot(keyCode As Keys) As Boolean
        Return keyCode = Primary.Loot OrElse
               keyCode = Secondary.Loot
    End Function

    Friend Function HotBar1(keyCode As Keys) As Boolean
        Return keyCode = Primary.HotBar1 OrElse
               keyCode = Secondary.HotBar1
    End Function
    Friend Function HotBar2(keyCode As Keys) As Boolean
        Return keyCode = Primary.HotBar2 OrElse
               keyCode = Secondary.HotBar2
    End Function
    Friend Function HotBar3(keyCode As Keys) As Boolean
        Return keyCode = Primary.HotBar3 OrElse
               keyCode = Secondary.HotBar3
    End Function
    Friend Function HotBar4(keyCode As Keys) As Boolean
        Return keyCode = Primary.HotBar4 OrElse
               keyCode = Secondary.HotBar4
    End Function
    Friend Function HotBar5(keyCode As Keys) As Boolean
        Return keyCode = Primary.HotBar5 OrElse
               keyCode = Secondary.HotBar5
    End Function
    Friend Function HotBar6(keyCode As Keys) As Boolean
        Return keyCode = Primary.HotBar6 OrElse
               keyCode = Secondary.HotBar6
    End Function
    Friend Function HotBar7(keyCode As Keys) As Boolean
        Return keyCode = Primary.HotBar7 OrElse
               keyCode = Secondary.HotBar7
    End Function

    Friend Function Inventory(keyCode As Keys) As Boolean
        Return keyCode = Primary.Inventory OrElse
               keyCode = Secondary.Inventory
    End Function
    Friend Function Character(keyCode As Keys) As Boolean
        Return keyCode = Primary.Character OrElse
               keyCode = Secondary.Character
    End Function
    Friend Function Quest(keyCode As Keys) As Boolean
        Return keyCode = Primary.Quest OrElse
               keyCode = Secondary.Quest
    End Function
    Friend Function Skill(keyCode As Keys) As Boolean
        Return keyCode = Primary.Skill OrElse
               keyCode = Secondary.Skill
    End Function
    Friend Function Settings(keyCode As Keys) As Boolean
        Return keyCode = Primary.Settings OrElse
               keyCode = Secondary.Settings
    End Function

    Friend Function HudToggle(keyCode As Keys) As Boolean
        Return keyCode = Primary.HudToggle OrElse
               keyCode = Secondary.HudToggle
    End Function
    Friend Function Admin(keyCode As Keys) As Boolean
        Return keyCode = Primary.Admin OrElse
               keyCode = Secondary.Admin
    End Function
End Class

Friend Module modInputs
    Public Inputs As New InputsDef

    Friend Sub LoadInputs()
        Dim cf As String = Path.Config()
        If Not Directory.Exists(cf) Then
            Directory.CreateDirectory(cf)
        End If : cf = cf & "\Inputs.xml"

        If Not File.Exists(cf) Then
            File.Create(cf).Dispose()
            SaveXml(Of InputsDef)(cf, New InputsDef)
        End If : Inputs = LoadXml(Of InputsDef)(cf)
    End Sub

    Friend Sub SaveInputs()
        Dim cf As String = Path.Config()
        If Not Directory.Exists(cf) Then
            Directory.CreateDirectory(cf)
        End If : cf = cf & "\Inputs.xml"

        SaveXml(Of InputsDef)(cf, Inputs)
    End Sub

End Module
#End If