Imports System.IO
Imports Asfw
Imports Asfw.IO

Friend Module S_Quest

#Region "Constants"

    'Constants
    Friend Const MAX_QUESTS As Integer = 250

    'Friend Const MAX_TASKS As Byte = 10
    'Friend Const MAX_REQUIREMENTS As Byte = 10
    Friend Const MAX_ACTIVEQUESTS = 10

    Friend Const EDITOR_TASKS As Byte = 7

    'Friend Const QUEST_TYPE_GOSLAY As Byte = 1
    'Friend Const QUEST_TYPE_GOCOLLECT As Byte = 2
    'Friend Const QUEST_TYPE_GOTALK As Byte = 3
    'Friend Const QUEST_TYPE_GOREACH As Byte = 4
    'Friend Const QUEST_TYPE_GOGIVE As Byte = 5
    'Friend Const QUEST_TYPE_GOKILL As Byte = 6
    'Friend Const QUEST_TYPE_GOGATHER As Byte = 7
    'Friend Const QUEST_TYPE_GOFETCH As Byte = 8
    'Friend Const QUEST_TYPE_TALKEVENT As Byte = 9

    'Friend Const QUEST_NOT_STARTED As Byte = 0
    'Friend Const QUEST_STARTED As Byte = 1
    'Friend Const QUEST_COMPLETED As Byte = 2
    'Friend Const QUEST_REPEATABLE As Byte = 3

    'Types
    Friend Quest(MAX_QUESTS) As QuestRec

    Friend Structure PlayerQuestRec
        Dim Status As Integer '0=not started, 1=started, 2=completed, 3=completed but repeatable
        Dim ActualTask As Integer
        Dim CurrentCount As Integer 'Used to handle the Amount property
    End Structure

    Friend Structure TaskRec
        Dim Order As Integer
        Dim NPC As Integer
        Dim Item As Integer
        Dim Map As Integer
        Dim Resource As Integer
        Dim Amount As Integer
        Dim Speech As String
        Dim TaskLog As String
        Dim QuestEnd As Byte
        Dim TaskType As Integer
    End Structure

    Friend Structure QuestRec
        Dim Name As String
        Dim QuestLog As String
        Dim Repeat As Byte
        Dim Cancelable As Byte

        Dim ReqCount As Integer
        Dim Requirement() As Integer '1=item, 2=quest, 3=class
        Dim RequirementIndex() As Integer

        Dim QuestGiveItem As Integer 'Todo: make this dynamic
        Dim QuestGiveItemValue As Integer
        Dim QuestRemoveItem As Integer
        Dim QuestRemoveItemValue As Integer

        Dim Chat() As String

        Dim RewardCount As Integer
        Dim RewardItem() As Integer
        Dim RewardItemAmount() As Integer
        Dim RewardExp As Integer

        Dim TaskCount As Integer
        Dim Task() As TaskRec

    End Structure

#End Region

#Region "Database"

    Sub SaveQuests()
        Dim I As Integer
        For I = 1 To MAX_QUESTS
            SaveQuest(I)
            Application.DoEvents()
        Next
    End Sub

    Sub SaveQuest(QuestNum As Integer)
        Dim filename As String
        Dim I As Integer
        filename = Path.Quest(QuestNum)

        Dim writer As New ByteStream(100)

        writer.WriteString(Quest(QuestNum).Name)
        writer.WriteString(Quest(QuestNum).QuestLog)
        writer.WriteByte(Quest(QuestNum).Repeat)
        writer.WriteByte(Quest(QuestNum).Cancelable)

        writer.WriteInt32(Quest(QuestNum).ReqCount)
        For I = 1 To Quest(QuestNum).ReqCount
            writer.WriteInt32(Quest(QuestNum).Requirement(I))
            writer.WriteInt32(Quest(QuestNum).RequirementIndex(I))
        Next

        writer.WriteInt32(Quest(QuestNum).QuestGiveItem)
        writer.WriteInt32(Quest(QuestNum).QuestGiveItemValue)
        writer.WriteInt32(Quest(QuestNum).QuestRemoveItem)
        writer.WriteInt32(Quest(QuestNum).QuestRemoveItemValue)

        For I = 1 To 3
            writer.WriteString(Quest(QuestNum).Chat(I))
        Next

        writer.WriteInt32(Quest(QuestNum).RewardCount)
        For I = 1 To Quest(QuestNum).RewardCount
            writer.WriteInt32(Quest(QuestNum).RewardItem(I))
            writer.WriteInt32(Quest(QuestNum).RewardItemAmount(I))
        Next
        writer.WriteInt32(Quest(QuestNum).RewardExp)

        writer.WriteInt32(Quest(QuestNum).TaskCount)
        For I = 1 To Quest(QuestNum).TaskCount
            writer.WriteInt32(Quest(QuestNum).Task(I).Order)
            writer.WriteInt32(Quest(QuestNum).Task(I).NPC)
            writer.WriteInt32(Quest(QuestNum).Task(I).Item)
            writer.WriteInt32(Quest(QuestNum).Task(I).Map)
            writer.WriteInt32(Quest(QuestNum).Task(I).Resource)
            writer.WriteInt32(Quest(QuestNum).Task(I).Amount)
            writer.WriteString(Quest(QuestNum).Task(I).Speech)
            writer.WriteString(Quest(QuestNum).Task(I).TaskLog)
            writer.WriteByte(Quest(QuestNum).Task(I).QuestEnd)
            writer.WriteInt32(Quest(QuestNum).Task(I).TaskType)
        Next

        ByteFile.Save(filename, writer)
    End Sub

    Sub LoadQuests()
        Dim I As Integer

        CheckQuests()

        For I = 1 To MAX_QUESTS
            LoadQuest(I)
            Application.DoEvents()
        Next
    End Sub

    Sub LoadQuest(QuestNum As Integer)
        Dim FileName As String
        Dim I As Integer

        FileName = Path.Quest(QuestNum)

        Dim reader As New ByteStream()
        ByteFile.Load(FileName, reader)

        Quest(QuestNum).Name = reader.ReadString()
        Quest(QuestNum).QuestLog = reader.ReadString()
        Quest(QuestNum).Repeat = reader.ReadByte()
        Quest(QuestNum).Cancelable = reader.ReadByte()

        Quest(QuestNum).ReqCount = reader.ReadInt32()
        ReDim Quest(QuestNum).Requirement(Quest(QuestNum).ReqCount)
        ReDim Quest(QuestNum).RequirementIndex(Quest(QuestNum).ReqCount)
        For I = 1 To Quest(QuestNum).ReqCount
            Quest(QuestNum).Requirement(I) = reader.ReadInt32()
            Quest(QuestNum).RequirementIndex(I) = reader.ReadInt32()
        Next

        Quest(QuestNum).QuestGiveItem = reader.ReadInt32()
        Quest(QuestNum).QuestGiveItemValue = reader.ReadInt32()
        Quest(QuestNum).QuestRemoveItem = reader.ReadInt32()
        Quest(QuestNum).QuestRemoveItemValue = reader.ReadInt32()

        For I = 1 To 3
            Quest(QuestNum).Chat(I) = reader.ReadString()
        Next

        Quest(QuestNum).RewardCount = reader.ReadInt32()
        ReDim Quest(QuestNum).RewardItem(Quest(QuestNum).RewardCount)
        ReDim Quest(QuestNum).RewardItemAmount(Quest(QuestNum).RewardCount)
        For I = 1 To Quest(QuestNum).RewardCount
            Quest(QuestNum).RewardItem(I) = reader.ReadInt32()
            Quest(QuestNum).RewardItemAmount(I) = reader.ReadInt32()
        Next
        Quest(QuestNum).RewardExp = reader.ReadInt32()

        Quest(QuestNum).TaskCount = reader.ReadInt32()
        ReDim Quest(QuestNum).Task(Quest(QuestNum).TaskCount)
        For I = 1 To Quest(QuestNum).TaskCount
            Quest(QuestNum).Task(I).Order = reader.ReadInt32()
            Quest(QuestNum).Task(I).NPC = reader.ReadInt32()
            Quest(QuestNum).Task(I).Item = reader.ReadInt32()
            Quest(QuestNum).Task(I).Map = reader.ReadInt32()
            Quest(QuestNum).Task(I).Resource = reader.ReadInt32()
            Quest(QuestNum).Task(I).Amount = reader.ReadInt32()
            Quest(QuestNum).Task(I).Speech = reader.ReadString()
            Quest(QuestNum).Task(I).TaskLog = reader.ReadString()
            Quest(QuestNum).Task(I).QuestEnd = reader.ReadByte()
            Quest(QuestNum).Task(I).TaskType = reader.ReadInt32()
        Next
    End Sub

    Sub CheckQuests()
        Dim I As Integer
        For I = 1 To MAX_QUESTS
            If Not File.Exists(Path.Quest(I)) Then
                SaveQuest(I)
                Application.DoEvents()
            End If
        Next
    End Sub

    Sub ClearQuest(QuestNum As Integer)
        Dim I As Integer

        ' clear the Quest
        Quest(QuestNum).Name = ""
        Quest(QuestNum).QuestLog = ""
        Quest(QuestNum).Repeat = 0
        Quest(QuestNum).Cancelable = 0

        Quest(QuestNum).ReqCount = 0
        ReDim Quest(QuestNum).Requirement(Quest(QuestNum).ReqCount)
        ReDim Quest(QuestNum).RequirementIndex(Quest(QuestNum).ReqCount)
        For I = 1 To Quest(QuestNum).ReqCount
            Quest(QuestNum).Requirement(QuestNum) = 0
            Quest(QuestNum).RequirementIndex(QuestNum) = 0
        Next

        Quest(QuestNum).QuestGiveItem = 0
        Quest(QuestNum).QuestGiveItemValue = 0
        Quest(QuestNum).QuestRemoveItem = 0
        Quest(QuestNum).QuestRemoveItemValue = 0

        ReDim Quest(QuestNum).Chat(3)
        For I = 1 To 3
            Quest(QuestNum).Chat(I) = ""
        Next

        Quest(QuestNum).RewardCount = 0
        ReDim Quest(QuestNum).RewardItem(Quest(QuestNum).RewardCount)
        ReDim Quest(QuestNum).RewardItemAmount(Quest(QuestNum).RewardCount)
        For I = 1 To Quest(QuestNum).RewardCount
            Quest(QuestNum).RewardItem(I) = 0
            Quest(QuestNum).RewardItemAmount(I) = 0
        Next
        Quest(QuestNum).RewardExp = 0

        Quest(QuestNum).TaskCount = 0
        ReDim Quest(QuestNum).Task(Quest(QuestNum).TaskCount)
        For I = 1 To Quest(QuestNum).TaskCount
            Quest(QuestNum).Task(I).Order = 0
            Quest(QuestNum).Task(I).NPC = 0
            Quest(QuestNum).Task(I).Item = 0
            Quest(QuestNum).Task(I).Map = 0
            Quest(QuestNum).Task(I).Resource = 0
            Quest(QuestNum).Task(I).Amount = 0
            Quest(QuestNum).Task(I).Speech = ""
            Quest(QuestNum).Task(I).TaskLog = ""
            Quest(QuestNum).Task(I).QuestEnd = 0
            Quest(QuestNum).Task(I).TaskType = 0
        Next

    End Sub

    Sub ClearQuests()
        Dim I As Integer

        For I = 1 To MAX_QUESTS
            ClearQuest(I)
        Next
    End Sub

#End Region

#Region "Incoming Packets"

    Sub Packet_RequestEditQuest(index As Integer, ByRef data() As Byte)
        ' Prevent hacking
        If GetPlayerAccess(index) < AdminType.Developer Then Exit Sub

        Dim Buffer = New ByteStream(4)
        Buffer.WriteInt32(ServerPackets.SQuestEditor)
        Socket.SendDataTo(index, Buffer.Data, Buffer.Head)
        Buffer.Dispose()
    End Sub

    Sub Packet_SaveQuest(index As Integer, ByRef data() As Byte)
        Dim QuestNum As Integer
        Dim buffer As New ByteStream(data)
        ' Prevent hacking
        If GetPlayerAccess(index) < AdminType.Developer Then Exit Sub

        QuestNum = buffer.ReadInt32
        If QuestNum < 0 OrElse QuestNum > MAX_QUESTS Then Exit Sub

        ' Update the Quest
        Quest(QuestNum).Name = buffer.ReadString
        Quest(QuestNum).QuestLog = buffer.ReadString
        Quest(QuestNum).Repeat = buffer.ReadInt32
        Quest(QuestNum).Cancelable = buffer.ReadInt32

        Quest(QuestNum).ReqCount = buffer.ReadInt32
        ReDim Quest(QuestNum).Requirement(Quest(QuestNum).ReqCount)
        ReDim Quest(QuestNum).RequirementIndex(Quest(QuestNum).ReqCount)
        For I = 1 To Quest(QuestNum).ReqCount
            Quest(QuestNum).Requirement(I) = buffer.ReadInt32
            Quest(QuestNum).RequirementIndex(I) = buffer.ReadInt32
        Next

        Quest(QuestNum).QuestGiveItem = buffer.ReadInt32
        Quest(QuestNum).QuestGiveItemValue = buffer.ReadInt32
        Quest(QuestNum).QuestRemoveItem = buffer.ReadInt32
        Quest(QuestNum).QuestRemoveItemValue = buffer.ReadInt32

        For I = 1 To 3
            Quest(QuestNum).Chat(I) = buffer.ReadString
        Next

        Quest(QuestNum).RewardCount = buffer.ReadInt32
        ReDim Quest(QuestNum).RewardItem(Quest(QuestNum).RewardCount)
        ReDim Quest(QuestNum).RewardItemAmount(Quest(QuestNum).RewardCount)
        For i = 1 To Quest(QuestNum).RewardCount
            Quest(QuestNum).RewardItem(i) = buffer.ReadInt32
            Quest(QuestNum).RewardItemAmount(i) = buffer.ReadInt32
        Next

        Quest(QuestNum).RewardExp = buffer.ReadInt32

        Quest(QuestNum).TaskCount = buffer.ReadInt32
        ReDim Quest(QuestNum).Task(Quest(QuestNum).TaskCount)
        For I = 1 To Quest(QuestNum).TaskCount
            Quest(QuestNum).Task(I).Order = buffer.ReadInt32
            Quest(QuestNum).Task(I).NPC = buffer.ReadInt32
            Quest(QuestNum).Task(I).Item = buffer.ReadInt32
            Quest(QuestNum).Task(I).Map = buffer.ReadInt32
            Quest(QuestNum).Task(I).Resource = buffer.ReadInt32
            Quest(QuestNum).Task(I).Amount = buffer.ReadInt32
            Quest(QuestNum).Task(I).Speech = buffer.ReadString
            Quest(QuestNum).Task(I).TaskLog = buffer.ReadString
            Quest(QuestNum).Task(I).QuestEnd = buffer.ReadInt32
            Quest(QuestNum).Task(I).TaskType = buffer.ReadInt32
        Next

        buffer.Dispose()

        ' Save it
        SendQuests(index) ' editor
        SendUpdateQuestToAll(QuestNum) 'players
        SaveQuest(QuestNum)
        Addlog(GetPlayerLogin(index) & " saved Quest #" & QuestNum & ".", ADMIN_LOG)
    End Sub

    Sub Packet_RequestQuests(index As Integer, ByRef data() As Byte)
        SendQuests(index)
    End Sub

    Sub Packet_PlayerHandleQuest(index As Integer, ByRef data() As Byte)
        Dim QuestNum As Integer, Order As Integer ', I As Integer
        Dim buffer As New ByteStream(data)
        QuestNum = buffer.ReadInt32
        Order = buffer.ReadInt32 '1 = accept, 2 = cancel

        If Order = 1 Then
            Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).Status = QuestStatusType.Started '1
            Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).ActualTask = 1
            Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).CurrentCount = 0
            PlayerMsg(index, "New quest accepted: " & Trim$(Quest(QuestNum).Name) & "!", ColorType.BrightGreen)
        ElseIf Order = 2 Then
            Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).Status = QuestStatusType.NotStarted '2
            Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).ActualTask = 1
            Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).CurrentCount = 0

            PlayerMsg(index, Trim$(Quest(QuestNum).Name) & " has been canceled!", ColorType.BrightRed)

            If GetPlayerAccess(index) > 0 AndAlso QuestNum = 1 Then
                For I = 1 To MAX_QUESTS
                    Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(I).Status = QuestStatusType.NotStarted '2
                    Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(I).ActualTask = 1
                    Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(I).CurrentCount = 0
                Next
            End If
        End If

        SavePlayer(index)
        SendPlayerData(index)
        SendPlayerQuests(index)
        buffer.Dispose()
    End Sub

    Sub Packet_QuestLogUpdate(index As Integer, ByRef data() As Byte)
        SendPlayerQuests(index)
    End Sub

    Sub Packet_QuestReset(index As Integer, ByRef data() As Byte)
        Dim QuestNum As Integer

        ' Prevent hacking
        If GetPlayerAccess(index) < AdminType.Mapper Then Exit Sub
        Dim buffer As New ByteStream(data)
        QuestNum = buffer.ReadInt32

        ResetQuest(index, QuestNum)

        buffer.Dispose()
    End Sub

#End Region

#Region "Outgoing packets"

    Sub SendQuests(index As Integer)
        Dim I As Integer

        For I = 1 To MAX_QUESTS
            If Len(Trim$(Quest(I).Name)) > 0 Then
                SendUpdateQuestTo(index, I)
            End If
        Next
    End Sub

    Sub SendUpdateQuestToAll(QuestNum As Integer)
        Dim buffer As ByteStream
        buffer = New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SUpdateQuest)
        buffer.WriteInt32(QuestNum)

        buffer.WriteString((Trim(Quest(QuestNum).Name)))
        buffer.WriteString((Trim(Quest(QuestNum).QuestLog)))
        buffer.WriteInt32(Quest(QuestNum).Repeat)
        buffer.WriteInt32(Quest(QuestNum).Cancelable)

        buffer.WriteInt32(Quest(QuestNum).ReqCount)
        For I = 1 To Quest(QuestNum).ReqCount
            buffer.WriteInt32(Quest(QuestNum).Requirement(I))
            buffer.WriteInt32(Quest(QuestNum).RequirementIndex(I))
        Next

        buffer.WriteInt32(Quest(QuestNum).QuestGiveItem)
        buffer.WriteInt32(Quest(QuestNum).QuestGiveItemValue)
        buffer.WriteInt32(Quest(QuestNum).QuestRemoveItem)
        buffer.WriteInt32(Quest(QuestNum).QuestRemoveItemValue)

        For I = 1 To 3
            buffer.WriteString((Trim(Quest(QuestNum).Chat(I))))
        Next

        buffer.WriteInt32(Quest(QuestNum).RewardCount)
        For i = 1 To Quest(QuestNum).RewardCount
            buffer.WriteInt32(Quest(QuestNum).RewardItem(i))
            buffer.WriteInt32(Quest(QuestNum).RewardItemAmount(i))
        Next

        buffer.WriteInt32(Quest(QuestNum).RewardExp)

        buffer.WriteInt32(Quest(QuestNum).TaskCount)
        For I = 1 To Quest(QuestNum).TaskCount
            buffer.WriteInt32(Quest(QuestNum).Task(I).Order)
            buffer.WriteInt32(Quest(QuestNum).Task(I).NPC)
            buffer.WriteInt32(Quest(QuestNum).Task(I).Item)
            buffer.WriteInt32(Quest(QuestNum).Task(I).Map)
            buffer.WriteInt32(Quest(QuestNum).Task(I).Resource)
            buffer.WriteInt32(Quest(QuestNum).Task(I).Amount)
            buffer.WriteString((Trim(Quest(QuestNum).Task(I).Speech)))
            buffer.WriteString((Trim(Quest(QuestNum).Task(I).TaskLog)))
            buffer.WriteInt32(Quest(QuestNum).Task(I).QuestEnd)
            buffer.WriteInt32(Quest(QuestNum).Task(I).TaskType)
        Next

        SendDataToAll(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Sub SendUpdateQuestTo(index As Integer, QuestNum As Integer)
        Dim buffer As ByteStream, I As Integer
        buffer = New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SUpdateQuest)
        buffer.WriteInt32(QuestNum)

        buffer.WriteString((Trim(Quest(QuestNum).Name)))
        buffer.WriteString((Trim(Quest(QuestNum).QuestLog)))
        buffer.WriteInt32(Quest(QuestNum).Repeat)
        buffer.WriteInt32(Quest(QuestNum).Cancelable)

        buffer.WriteInt32(Quest(QuestNum).ReqCount)
        For I = 1 To Quest(QuestNum).ReqCount
            buffer.WriteInt32(Quest(QuestNum).Requirement(I))
            buffer.WriteInt32(Quest(QuestNum).RequirementIndex(I))
        Next

        buffer.WriteInt32(Quest(QuestNum).QuestGiveItem)
        buffer.WriteInt32(Quest(QuestNum).QuestGiveItemValue)
        buffer.WriteInt32(Quest(QuestNum).QuestRemoveItem)
        buffer.WriteInt32(Quest(QuestNum).QuestRemoveItemValue)

        For I = 1 To 3
            buffer.WriteString((Trim(Quest(QuestNum).Chat(I))))
        Next

        buffer.WriteInt32(Quest(QuestNum).RewardCount)
        For I = 1 To Quest(QuestNum).RewardCount
            buffer.WriteInt32(Quest(QuestNum).RewardItem(I))
            buffer.WriteInt32(Quest(QuestNum).RewardItemAmount(I))
        Next

        buffer.WriteInt32(Quest(QuestNum).RewardExp)

        buffer.WriteInt32(Quest(QuestNum).TaskCount)
        For I = 1 To Quest(QuestNum).TaskCount
            buffer.WriteInt32(Quest(QuestNum).Task(I).Order)
            buffer.WriteInt32(Quest(QuestNum).Task(I).NPC)
            buffer.WriteInt32(Quest(QuestNum).Task(I).Item)
            buffer.WriteInt32(Quest(QuestNum).Task(I).Map)
            buffer.WriteInt32(Quest(QuestNum).Task(I).Resource)
            buffer.WriteInt32(Quest(QuestNum).Task(I).Amount)
            buffer.WriteString((Trim(Quest(QuestNum).Task(I).Speech)))
            buffer.WriteString((Trim(Quest(QuestNum).Task(I).TaskLog)))
            buffer.WriteInt32(Quest(QuestNum).Task(I).QuestEnd)
            buffer.WriteInt32(Quest(QuestNum).Task(I).TaskType)
        Next

        Socket.SendDataTo(index, buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendPlayerQuests(index As Integer)
        Dim I As Integer
        Dim buffer As ByteStream
        buffer = New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SPlayerQuests)

        For I = 1 To MAX_QUESTS
            buffer.WriteInt32(Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(I).Status)
            buffer.WriteInt32(Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(I).ActualTask)
            buffer.WriteInt32(Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(I).CurrentCount)
        Next

        Socket.SendDataTo(index, buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

    Friend Sub SendPlayerQuest(index As Integer, QuestNum As Integer)
        Dim buffer As ByteStream

        buffer = New ByteStream(4)
        buffer.WriteInt32(ServerPackets.SPlayerQuest)

        buffer.WriteInt32(QuestNum)
        buffer.WriteInt32(Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).Status)
        buffer.WriteInt32(Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).ActualTask)
        buffer.WriteInt32(Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).CurrentCount)

        Socket.SendDataTo(index, buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    'sends a message to the client that is shown on the screen
    Friend Sub QuestMessage(index As Integer, QuestNum As Integer, message As String, QuestNumForStart As Integer)
        Dim buffer As ByteStream
        buffer = New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SQuestMessage)

        buffer.WriteInt32(QuestNum)
        buffer.WriteString((Trim$(message)))
        buffer.WriteInt32(QuestNumForStart)

        Socket.SendDataTo(index, buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

#End Region

#Region "Functions"

    Friend Sub ResetQuest(index As Integer, QuestNum As Integer)
        If GetPlayerAccess(index) > 0 Then
            Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).Status = QuestStatusType.NotStarted
            Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).ActualTask = 1
            Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).CurrentCount = 0

            SendPlayerQuests(index)
            PlayerMsg(index, "Quest " & QuestNum & " reset!", ColorType.BrightRed)
        End If
    End Sub

    Friend Function CanStartQuest(index As Integer, QuestNum As Integer) As Boolean
        CanStartQuest = False
        If QuestNum < 1 OrElse QuestNum > MAX_QUESTS Then Exit Function
        If QuestInProgress(index, QuestNum) Then Exit Function

        'Check if player has the quest 0 (not started) or 3 (completed but it can be started again)
        If Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).Status = QuestStatusType.NotStarted OrElse Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).Status = QuestStatusType.Repeatable Then
            For i = 1 To Quest(QuestNum).ReqCount
                'Check if item is needed
                If Quest(QuestNum).Requirement(i) = 1 Then
                    If Quest(QuestNum).RequirementIndex(i) > 0 AndAlso Quest(QuestNum).RequirementIndex(i) <= MAX_ITEMS Then
                        If HasItem(index, Quest(QuestNum).RequirementIndex(i)) = 0 Then
                            PlayerMsg(index, "You need " & Item(Quest(QuestNum).Requirement(2)).Name & " to take this quest!", ColorType.Yellow)
                            Exit Function
                        End If
                    End If
                End If

                'Check if previous quest is needed
                If Quest(QuestNum).Requirement(i) = 2 Then
                    If Quest(QuestNum).RequirementIndex(i) > 0 AndAlso Quest(QuestNum).RequirementIndex(i) <= MAX_QUESTS Then
                        If Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(Quest(QuestNum).Requirement(2)).Status = QuestStatusType.NotStarted OrElse Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(Quest(QuestNum).Requirement(2)).Status = QuestStatusType.Started Then
                            PlayerMsg(index, "You need to complete the " & Trim$(Quest(Quest(QuestNum).Requirement(2)).Name) & " quest in order to take this quest!", ColorType.Yellow)
                            Exit Function
                        End If
                    End If
                End If

            Next

            'Go on :)
            CanStartQuest = True
        Else
            'PlayerMsg Index, "You can't start that quest again!", BrightRed
        End If
    End Function

    Friend Function CanEndQuest(index As Integer, QuestNum As Integer) As Boolean
        CanEndQuest = False

        If Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).ActualTask >= Quest(QuestNum).Task.Length Then
            CanEndQuest = True
        End If
        If Quest(QuestNum).Task(Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).ActualTask).QuestEnd = 1 Then
            CanEndQuest = True
        End If
    End Function

    'Tells if the quest is in progress or not
    Friend Function QuestInProgress(index As Integer, QuestNum As Integer) As Boolean
        QuestInProgress = False
        If QuestNum < 1 OrElse QuestNum > MAX_QUESTS Then Exit Function

        If Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).Status = QuestStatusType.Started Then 'Status=1 means started
            QuestInProgress = True
        End If
    End Function

    Friend Function QuestCompleted(index As Integer, QuestNum As Integer) As Boolean
        QuestCompleted = False
        If QuestNum < 1 OrElse QuestNum > MAX_QUESTS Then Exit Function

        If Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).Status = 2 OrElse Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).Status = 3 Then
            QuestCompleted = True
        End If
    End Function

    'Gets the quest reference num (id) from the quest name (it shall be unique)
    Friend Function GetQuestNum(QuestName As String) As Integer
        Dim I As Integer
        GetQuestNum = 0

        For I = 1 To MAX_QUESTS
            If Trim$(Quest(I).Name) = Trim$(QuestName) Then
                GetQuestNum = I
                Exit For
            End If
        Next
    End Function

    Friend Function GetItemNum(ItemName As String) As Integer
        Dim I As Integer
        GetItemNum = 0

        For I = 1 To MAX_ITEMS
            If Trim$(Item(I).Name) = Trim$(ItemName) Then
                GetItemNum = I
                Exit For
            End If
        Next
    End Function

    ' /////////////////////
    ' // General Purpose //
    ' /////////////////////

    Friend Sub CheckTasks(index As Integer, TaskType As Integer, Targetindex As Integer)
        Dim I As Integer

        For I = 1 To MAX_QUESTS
            If QuestInProgress(index, I) Then
                CheckTask(index, I, TaskType, Targetindex)
            End If
        Next
    End Sub

    Friend Sub CheckTask(index As Integer, QuestNum As Integer, TaskType As Integer, Targetindex As Integer)
        Dim ActualTask As Integer, I As Integer
        ActualTask = Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).ActualTask

        If ActualTask >= Quest(QuestNum).Task.Length Then Exit Sub

        Select Case TaskType
            Case QuestType.Slay 'defeat X amount of X npc's.

                'is npc defeated id same as the npc i have to defeat?
                If Targetindex = Quest(QuestNum).Task(ActualTask).NPC Then
                    'Count +1
                    Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).CurrentCount = Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).CurrentCount + 1
                    'did i finish the work?
                    If Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).CurrentCount >= Quest(QuestNum).Task(ActualTask).Amount Then
                        QuestMessage(index, QuestNum, Trim$(Quest(QuestNum).Task(ActualTask).TaskLog) & " - Task completed", 0)
                        'is the quest's end?
                        If CanEndQuest(index, QuestNum) Then
                            EndQuest(index, QuestNum)
                        Else
                            'otherwise continue to the next task
                            Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).ActualTask = ActualTask + 1
                        End If
                    End If
                End If

            Case QuestType.Collect 'Gather X amount of X item.
                If Targetindex = Quest(QuestNum).Task(ActualTask).Item Then
                    Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).CurrentCount = Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).CurrentCount + 1
                    If Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).CurrentCount >= Quest(QuestNum).Task(ActualTask).Amount Then
                        QuestMessage(index, QuestNum, Trim$(Quest(QuestNum).Task(ActualTask).TaskLog) & " - Task completed", 0)
                        If CanEndQuest(index, QuestNum) Then
                            EndQuest(index, QuestNum)
                        Else
                            Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).ActualTask = ActualTask + 1
                        End If
                    End If
                End If

            Case QuestType.Talk 'Interact with X npc.
                If Targetindex = Quest(QuestNum).Task(ActualTask).NPC AndAlso Quest(QuestNum).Task(ActualTask).Amount = 0 Then
                    QuestMessage(index, QuestNum, Quest(QuestNum).Task(ActualTask).Speech, 0)
                    If CanEndQuest(index, QuestNum) Then
                        EndQuest(index, QuestNum)
                    Else
                        Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).ActualTask = ActualTask + 1
                    End If
                End If

            Case QuestType.Reach 'Reach X map.
                If Targetindex = Quest(QuestNum).Task(ActualTask).Map Then
                    QuestMessage(index, QuestNum, Trim$(Quest(QuestNum).Task(ActualTask).TaskLog) & " - Task completed", 0)
                    If CanEndQuest(index, QuestNum) Then
                        EndQuest(index, QuestNum)
                    Else
                        Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).ActualTask = ActualTask + 1
                    End If
                End If

            Case QuestType.Give 'Give X amount of X item to X npc.
                If Targetindex = Quest(QuestNum).Task(ActualTask).NPC Then
                    For I = 1 To MAX_INV
                        If GetPlayerInvItemNum(index, I) = Quest(QuestNum).Task(ActualTask).Item Then
                            If GetPlayerInvItemValue(index, I) >= Quest(QuestNum).Task(ActualTask).Amount Then
                                TakeInvItem(index, GetPlayerInvItemNum(index, I), Quest(QuestNum).Task(ActualTask).Amount)
                                QuestMessage(index, QuestNum, Quest(QuestNum).Task(ActualTask).Speech, 0)
                                If CanEndQuest(index, QuestNum) Then
                                    EndQuest(index, QuestNum)
                                Else
                                    Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).ActualTask = ActualTask + 1
                                End If
                                Exit For
                            End If
                        End If
                    Next
                End If

            Case QuestType.Kill 'Kill X amount of players.
                Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).CurrentCount = Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).CurrentCount + 1
                If Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).CurrentCount >= Quest(QuestNum).Task(ActualTask).Amount Then
                    QuestMessage(index, QuestNum, Trim$(Quest(QuestNum).Task(ActualTask).TaskLog) & " - Task completed", 0)
                    If CanEndQuest(index, QuestNum) Then
                        EndQuest(index, QuestNum)
                    Else
                        Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).ActualTask = ActualTask + 1
                    End If
                End If

            Case QuestType.Gather 'Hit X amount of times X resource.
                If Targetindex = Quest(QuestNum).Task(ActualTask).Resource Then
                    Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).CurrentCount = Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).CurrentCount + 1
                    If Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).CurrentCount >= Quest(QuestNum).Task(ActualTask).Amount Then
                        QuestMessage(index, QuestNum, Trim$(Quest(QuestNum).Task(ActualTask).TaskLog) & " - Task completed", 0)
                        If CanEndQuest(index, QuestNum) Then
                            EndQuest(index, QuestNum)
                        Else
                            Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).ActualTask = ActualTask + 1
                        End If
                    End If
                End If

            Case QuestType.Fetch 'Get X amount of X item from X npc.
                If Targetindex = Quest(QuestNum).Task(ActualTask).NPC Then
                    GiveInvItem(index, Quest(QuestNum).Task(ActualTask).Item, Quest(QuestNum).Task(ActualTask).Amount)
                    QuestMessage(index, QuestNum, Quest(QuestNum).Task(ActualTask).Speech, 0)
                    If CanEndQuest(index, QuestNum) Then
                        EndQuest(index, QuestNum)
                    Else
                        Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).ActualTask = ActualTask + 1
                    End If
                End If
        End Select

        SendPlayerQuest(index, QuestNum)
    End Sub

    Friend Sub ShowQuest(index As Integer, QuestNum As Integer)
        If QuestInProgress(index, QuestNum) Then
            QuestMessage(index, QuestNum, Trim$(Quest(QuestNum).Chat(2)), 0) 'show meanwhile message
            Exit Sub
        End If
        If Not CanStartQuest(index, QuestNum) Then Exit Sub

        QuestMessage(index, QuestNum, Trim$(Quest(QuestNum).Chat(1)), QuestNum) 'chat 1 = request message
    End Sub

    Friend Sub EndQuest(index As Integer, QuestNum As Integer)
        Dim I As Integer

        QuestMessage(index, QuestNum, Trim$(Quest(QuestNum).Chat(3)), 0)

        For I = 1 To Quest(QuestNum).RewardCount
            If Quest(QuestNum).RewardItem(I) > 0 Then
                PlayerMsg(index, "You recieved " & Quest(QuestNum).RewardItemAmount(I) & " " & Trim(Item(Quest(QuestNum).RewardItem(I)).Name), ColorType.BrightGreen)
            End If
            GiveInvItem(index, Quest(QuestNum).RewardItem(I), Quest(QuestNum).RewardItemAmount(I))
        Next

        If Quest(QuestNum).RewardExp > 0 Then
            SetPlayerExp(index, GetPlayerExp(index) + Quest(QuestNum).RewardExp)
            SendExp(index)
            ' Check for level up
            CheckPlayerLevelUp(index)
        End If

        'Check if quest is repeatable, set it as completed
        If Quest(QuestNum).Repeat = True Then
            Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).Status = QuestStatusType.Repeatable
        Else
            Player(index).Character(TempPlayer(index).CurChar).PlayerQuest(QuestNum).Status = QuestStatusType.Completed
        End If
        PlayerMsg(index, Trim$(Quest(QuestNum).Name) & ": quest completed", ColorType.BrightGreen)

        SavePlayer(index)
        SendPlayerData(index)
        SendPlayerQuest(index, QuestNum)
    End Sub

#End Region

End Module