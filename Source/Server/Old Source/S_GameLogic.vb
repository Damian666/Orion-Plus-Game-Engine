Imports System

Module S_GameLogic

    Function GetTotalMapPlayers(mapNum As Integer) As Integer
        Dim i As Integer, n As Integer
        n = 0

        For i = 1 To GetPlayersOnline()
            If Network.IsPlaying(i) AndAlso GetPlayerMap(i) = mapNum Then
                n = n + 1
            End If
        Next

        GetTotalMapPlayers = n
    End Function

    Friend Function GetPlayersOnline() As Integer
        Dim x As Integer
        x = 0
        For i As Integer = 1 To Network.HighIndex
            If TempPlayer(i).InGame = True Then
                x = x + 1
            End If
        Next
        GetPlayersOnline = x
    End Function

    Function GetNpcMaxVital(NpcNum As Integer, Vital As VitalType) As Integer
        GetNpcMaxVital = 0

        ' Prevent subscript out of range
        If NpcNum <= 0 OrElse NpcNum > MAX_NPCS Then Exit Function

        Select Case Vital
            Case VitalType.HP
                GetNpcMaxVital = Npc(NpcNum).Hp
            Case VitalType.MP
                GetNpcMaxVital = Npc(NpcNum).Stat(StatType.Intelligence) * 2
            Case VitalType.SP
                GetNpcMaxVital = Npc(NpcNum).Stat(StatType.Spirit) * 2
        End Select

    End Function

    Function FindPlayer(Name As String) As Integer
        Dim i As Integer

        For i = 1 To GetPlayersOnline()
            If Network.IsPlaying(i) Then
                ' Make sure we dont try to check a name thats to small
                If GetPlayerName(i).Length >= Name.Trim.Length Then
                    If GetPlayerName(i).Substring(1, Name.Trim.Length).ToUpper() = Name.Trim.ToUpper() Then
                        FindPlayer = i
                        Exit Function
                    End If
                End If
            End If
        Next

        FindPlayer = 0
    End Function

    Friend Function Random(low As Integer, high As Integer) As Integer
        Static randomNumGen As New Random
        Return randomNumGen.Next(low, high + 1)
    End Function

    Friend Function CheckGrammar(Word As String, Optional Caps As Byte = 0) As String
        Dim FirstLetter As String

        FirstLetter = Word.Substring(0, 1).ToLower()

        If FirstLetter = "$" Then
            CheckGrammar = (Word.Substring(2, Word.Length - 1))
            Exit Function
        End If

        If FirstLetter Like "*[aeiou]*" Then
            If Caps Then CheckGrammar = "An " & Word Else CheckGrammar = "an " & Word
        Else
            If Caps Then CheckGrammar = "A " & Word Else CheckGrammar = "a " & Word
        End If
    End Function

End Module