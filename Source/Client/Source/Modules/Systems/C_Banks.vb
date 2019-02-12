Imports System.Drawing
Imports ASFW

Module C_Banks

#Region "Globals & Types"

    Friend Bank As BankStruct

    ' Stores the last bank item we showed in desc
    Friend LastBankDesc As Integer

    Friend InBank As Integer

    ' bank drag + drop
    Friend DragBankSlotNum As Integer

    Friend BankX As Integer
    Friend BankY As Integer

#End Region

#Region "Database"

    Sub ClearBank()
        ReDim Bank.Item(MAX_BANK)
        ReDim Bank.ItemRand(MAX_BANK)
        For x = 1 To MAX_BANK
            ReDim Bank.ItemRand(x).Stat(StatType.Count - 1)
        Next
    End Sub

#End Region

#Region "Incoming Packets"

    Friend Sub Packet_OpenBank(ByRef data() As Byte)
        Dim i As Integer, x As Integer
        Dim buffer As New ByteStream(data)
        For i = 1 To MAX_BANK
            Bank.Item(i).Index = buffer.ReadInt32
            Bank.Item(i).Amount = buffer.ReadInt32

            Bank.ItemRand(i).Prefix = buffer.ReadString
            Bank.ItemRand(i).Suffix = buffer.ReadString
            Bank.ItemRand(i).Rarity = buffer.ReadInt32
            Bank.ItemRand(i).Damage = buffer.ReadInt32
            Bank.ItemRand(i).Speed = buffer.ReadInt32

            For x = 1 To StatType.Count - 1
                Bank.ItemRand(i).Stat(x) = buffer.ReadInt32
            Next
        Next

        NeedToOpenBank = True

        buffer.Dispose()
    End Sub

#End Region

#Region "Outgoing Packets"

    Friend Sub DepositItem(invslot As Integer, amount As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CDepositItem)
        buffer.WriteInt32(invslot)
        buffer.WriteInt32(amount)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub WithdrawItem(bankslot As Integer, amount As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CWithdrawItem)
        buffer.WriteInt32(bankslot)
        buffer.WriteInt32(amount)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub ChangeBankSlots(oldSlot As Integer, newSlot As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CChangeBankSlots)
        buffer.WriteInt32(oldSlot)
        buffer.WriteInt32(newSlot)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub CloseBank()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CCloseBank)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()

        InBank = False
        PnlBankVisible = False
    End Sub

#End Region


End Module