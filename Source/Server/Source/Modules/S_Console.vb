Imports System.Threading

Module S_Console
    Private threadConsole As Thread

    Sub Main()
        threadConsole = New Thread(New ThreadStart(AddressOf ConsoleThread))
        threadConsole.Start()

        ' Spin up the server on the main thread
        InitServer()
    End Sub

    Private Sub ConsoleThread()
        Dim line As String, parts As String()

        Console.WriteLine("Initializing Console Loop")

        While (True)
            line = Console.ReadLine()

            parts = line.Split(" ") : If (parts.Length < 1) Then Continue While

            Select Case parts(0).Trim().ToLower()
                Case "/help"
#Region " Body "
                    Console.WriteLine("/help, Shows this message.")
                    Console.WriteLine("/exit, Closes down the server.")
                    Console.WriteLine("/setadmin, Sets player access level, use with '/setadmin playername powerlvl' powerlevel goes from 0 for player, to 4 to creator.")
                    Console.WriteLine("/kick, Kicks user from server, use with '/kick playername'")
                    Console.WriteLine("/ban, Bans user from server, use with '/ban playername'")
#End Region
                Case "/exit"
#Region " Body "
                    DestroyServer()
#End Region
                Case "/setadmin"
                    If parts.Length < 3 Then Continue While
#Region " Body "
                    Dim Name As String = parts(1)
                    Dim Pindex as integer = FindPlayer(Name)
                    Dim Power As Integer : Integer.TryParse(parts(2), Power)

                    If Not PIndex > 0 Then
                        Console.WriteLine("Player name is empty or invalid. [Name not found]")
                    Else
                        Select Case Power
                            Case 0
                                SetPlayerAccess(PIndex, Power)
                                SendPlayerData(PIndex)
                                PlayerMsg(PIndex, "Your PowerLevel has been set to Player Rank!", ColorType.BrightCyan)
                                Console.WriteLine("Successfully set the power level to " & Power & " for player " & Name)
                            Case 1
                                SetPlayerAccess(PIndex, Power)
                                SendPlayerData(PIndex)
                                PlayerMsg(PIndex, "Your PowerLevel has been set to Monitor Rank!", ColorType.BrightCyan)
                                Console.WriteLine("Successfully set the power level to " & Power & " for player " & Name)
                            Case 2
                                SetPlayerAccess(PIndex, Power)
                                SendPlayerData(PIndex)
                                PlayerMsg(PIndex, "Your PowerLevel has been set to Mapper Rank!", ColorType.BrightCyan)
                                Console.WriteLine("Successfully set the power level to " & Power & " for player " & Name)
                            Case 3
                                SetPlayerAccess(PIndex, Power)
                                SendPlayerData(PIndex)
                                PlayerMsg(PIndex, "Your PowerLevel has been set to Developer Rank!", ColorType.BrightCyan)
                                Console.WriteLine("Successfully set the power level to " & Power & " for player " & Name)
                            Case 4
                                SetPlayerAccess(PIndex, Power)
                                SendPlayerData(PIndex)
                                PlayerMsg(PIndex, "Your PowerLevel has been set to Creator Rank!", ColorType.BrightCyan)
                                Console.WriteLine("Successfully set the power level to " & Power & " for player " & Name)
                            Case Else
                                Console.WriteLine("Failed to set the power level to " & Power & " for player " & Name)
                        End Select
                    End If
#End Region
                Case "/kick"
                    If parts.Length < 2 Then Continue While
#Region " Body "
                    Dim Name As String = parts(1)
                    Dim Pindex as integer = FindPlayer(Name)
                    If Not PIndex > 0 Then
                        Console.WriteLine("Player name is empty or invalid. [Name not found]")
                    Else
                        AlertMsg(PIndex, "You have been kicked by the server owner!")
                        LeftGame(PIndex)
                    End If
#End Region
                Case "/ban"
                    If parts.Length < 2 Then Continue While
#Region " Body "
                    Dim Name As String = parts(1)
                    Dim Pindex as integer = FindPlayer(Name)
                    If Not PIndex > 0 Then : Console.WriteLine("Player name is empty or invalid. [Name not found]")
                    Else : ServerBanIndex(PIndex) : End If
#End Region
                Case "/timespeed"
                    If parts.Length < 2 Then Exit Sub
#Region " Body "
                    Dim speed As Double
                    Double.TryParse(parts(1), speed)
                    Time.Instance.GameSpeed = speed
                    Console.WriteLine("Set GameSpeed to " & Time.Instance.GameSpeed & " secs per seconds")
#End Region
                Case "" : Continue While
                Case Else : Console.WriteLine("Invalid Command. If you are unsure of the functions type '/help'.")
            End Select
        End While
    End Sub
End Module