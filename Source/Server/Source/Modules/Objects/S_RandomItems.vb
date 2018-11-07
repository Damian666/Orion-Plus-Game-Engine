Module S_RandomItems

    Friend Sub ClearRandBank(index As Integer, BankNum As Integer)
        Dim i As Integer

        Bank(index).ItemRand(BankNum).Prefix = ""
        Bank(index).ItemRand(BankNum).Suffix = ""
        Bank(index).ItemRand(BankNum).Damage = 0
        Bank(index).ItemRand(BankNum).Speed = 0
        Bank(index).ItemRand(BankNum).Rarity = 0

        For i = 1 To StatType.Count - 1
            Bank(index).ItemRand(BankNum).Stat(i) = 0
        Next i
    End Sub

    Friend Sub ClearRandInv(index As Integer, InvNum As Integer)
        Dim i As Integer

        Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Prefix = ""
        Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Suffix = ""
        Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Damage = 0
        Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Speed = 0
        Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Rarity = 0

        For i = 1 To StatType.Count - 1
            Player(index).Character(TempPlayer(index).CurChar).RandInv(InvNum).Stat(i) = 0
        Next i
    End Sub

    Friend Sub ClearRandEq(index As Integer, Equipment As EquipmentType)
        Dim i As Integer

        Player(index).Character(TempPlayer(index).CurChar).RandEquip(Equipment).Prefix = ""
        Player(index).Character(TempPlayer(index).CurChar).RandEquip(Equipment).Suffix = ""
        Player(index).Character(TempPlayer(index).CurChar).RandEquip(Equipment).Damage = 0
        Player(index).Character(TempPlayer(index).CurChar).RandEquip(Equipment).Speed = 0
        Player(index).Character(TempPlayer(index).CurChar).RandEquip(Equipment).Rarity = 0

        For i = 1 To StatType.Count - 1
            Player(index).Character(TempPlayer(index).CurChar).RandEquip(Equipment).Stat(i) = 0
        Next i
    End Sub

    Friend Sub GivePlayerRandomItem(index As Integer, itemnum As Integer, invslot As Integer)
        Dim RandomType As Integer, StatAmount As Integer, Rarity As Integer, TempNum As Integer, TempAmount As Double, i As Integer, ItemLevel As Integer
        Dim Prefix As String = ""

        ' Check to see if we're out of range, or if the item isn't random.
        If itemnum < 1 OrElse itemnum > MAX_ITEMS Then Exit Sub
        If index < 1 OrElse index > MAX_PLAYERS Then Exit Sub
        If Item(itemnum).Randomize = 0 Then Exit Sub

        ' See what rarity we get
        TempNum = Random(1, 100)
        If TempNum >= 95 Then
            Rarity = RarityType.RARITY_EPIC
            TempAmount = 0.5
            Prefix = "Epic "
        ElseIf TempNum >= 80 AndAlso TempNum < 95 Then
            Rarity = RarityType.RARITY_RARE
            TempAmount = 0.35
            Prefix = "Rare "
        ElseIf TempNum >= 60 AndAlso TempNum < 80 Then
            Rarity = RarityType.RARITY_UNCOMMON
            TempAmount = 0.2
            Prefix = "Unrare "
        ElseIf TempNum >= 20 AndAlso TempNum < 60 Then
            Rarity = RarityType.RARITY_COMMON
            TempAmount = 0
        Else
            Rarity = RarityType.RARITY_BROKEN
            RandomType = RandomBonusType.RANDOM_BROKEN
            Prefix = "Broken "
        End If

        ' We've got a rarity! Determine the Enchant type
        If Rarity <> RarityType.RARITY_BROKEN Then
            RandomType = Random(1, MAX_RANDOM_TYPES)
        End If

        ' Set the item level for easy reference
        ItemLevel = Item(itemnum).ItemLevel

        ' set the Bonus StatAmount
        StatAmount = ItemLevel * TempAmount
        If StatAmount < 4 AndAlso Rarity = RarityType.RARITY_EPIC Then StatAmount = 4
        If StatAmount < 3 AndAlso Rarity = RarityType.RARITY_RARE Then StatAmount = 3
        If StatAmount < 2 AndAlso Rarity = RarityType.RARITY_UNCOMMON Then StatAmount = 2

        ' Give out the item based off of the randomtype
        Select Case RandomType
            Case RandomBonusType.RANDOM_SPEED
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Prefix = Prefix
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Suffix = " of Speed"
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Rarity = Rarity
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Damage = Item(itemnum).Data2
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Speed = Item(itemnum).Speed - (Item(itemnum).Speed * TempAmount)
            Case RandomBonusType.RANDOM_DAMAGE
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Prefix = Prefix
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Suffix = " of Damage"
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Rarity = Rarity
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Speed = Item(itemnum).Speed
                If TempAmount < 1 Then TempAmount = 1
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Damage = Item(itemnum).Data2 + (Item(itemnum).Data2 * TempAmount)
            Case RandomBonusType.RANDOM_WARRIOR
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Prefix = Prefix
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Suffix = " of Warrior"
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Rarity = Rarity
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Damage = Item(itemnum).Data2
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Speed = Item(itemnum).Speed
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(StatType.Strength) = ItemLevel + StatAmount
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(StatType.Endurance) = ItemLevel + StatAmount
            Case RandomBonusType.RANDOM_ARCHER
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Prefix = Prefix
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Suffix = " of Archer"
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Rarity = Rarity
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Damage = Item(itemnum).Data2
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Speed = Item(itemnum).Speed
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(StatType.Spirit) = ItemLevel + StatAmount
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(StatType.Endurance) = ItemLevel + StatAmount
            Case RandomBonusType.RANDOM_MAGE
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Prefix = Prefix
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Suffix = " of Mage"
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Rarity = Rarity
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Damage = Item(itemnum).Data2
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Speed = Item(itemnum).Speed
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(StatType.Intelligence) = ItemLevel + StatAmount
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(StatType.Endurance) = ItemLevel + StatAmount
            Case RandomBonusType.RANDOM_JESTER
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Prefix = Prefix
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Suffix = " of Jester"
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Rarity = Rarity
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Damage = Item(itemnum).Data2
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Speed = Item(itemnum).Speed
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(StatType.Intelligence) = ItemLevel + StatAmount
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(StatType.Spirit) = ItemLevel + StatAmount
            Case RandomBonusType.RANDOM_BATTLEMAGE
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Prefix = Prefix
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Suffix = " of Battlemage"
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Rarity = Rarity
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Damage = Item(itemnum).Data2
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Speed = Item(itemnum).Speed
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(StatType.Strength) = ItemLevel + StatAmount
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(StatType.Intelligence) = ItemLevel + StatAmount
            Case RandomBonusType.RANDOM_ROGUE
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Prefix = Prefix
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Suffix = " of Rogue"
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Rarity = Rarity
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Damage = Item(itemnum).Data2
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Speed = Item(itemnum).Speed
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(StatType.Strength) = ItemLevel + StatAmount
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(StatType.Spirit) = ItemLevel + StatAmount
            Case RandomBonusType.RANDOM_TOWER
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Prefix = Prefix
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Suffix = " of Tower"
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Rarity = Rarity
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Damage = Item(itemnum).Data2
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Speed = Item(itemnum).Speed
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(StatType.Endurance) = ItemLevel + StatAmount
            Case RandomBonusType.RANDOM_SURVIVALIST
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Prefix = Prefix
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Suffix = " of Survival"
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Rarity = Rarity
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Damage = Item(itemnum).Data2
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Speed = Item(itemnum).Speed
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(ResourceSkills.Fisherman) = ItemLevel + StatAmount
            Case RandomBonusType.RANDOM_PERFECTIONIST
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Prefix = Prefix
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Suffix = " of Perfection"
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Rarity = Rarity
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Damage = Item(itemnum).Data2
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Speed = Item(itemnum).Speed
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(ResourceSkills.Miner) = ItemLevel + StatAmount
            Case RandomBonusType.RANDOM_COALMEN
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Prefix = Prefix
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Suffix = " of Coalmen"
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Rarity = Rarity
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Damage = Item(itemnum).Data2
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Speed = Item(itemnum).Speed
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(ResourceSkills.Miner) = ItemLevel + StatAmount
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(ResourceSkills.WoodCutter) = ItemLevel + StatAmount
            Case RandomBonusType.RANDOM_BOWYER
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Prefix = Prefix
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Suffix = " of Bowyer"
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Rarity = Rarity
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Damage = Item(itemnum).Data2
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Speed = Item(itemnum).Speed
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(ResourceSkills.WoodCutter) = ItemLevel + StatAmount
            Case RandomBonusType.RANDOM_BROKEN
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Prefix = "Broken "
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Suffix = ""
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Rarity = RarityType.RARITY_BROKEN
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Damage = Item(itemnum).Data2 - (Item(itemnum).Data2 / 10)
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Speed = Item(itemnum).Speed + (Item(itemnum).Speed / 10)
            Case RandomBonusType.RANDOM_PRISM
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Prefix = Prefix
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Suffix = " of Prism"
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Rarity = Rarity
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Damage = Item(itemnum).Data2
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Speed = Item(itemnum).Speed
                For i = 1 To 4
                    Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(Random(1, StatType.Count - 1)) = ItemLevel + StatAmount
                Next
            Case RandomBonusType.RANDOM_CANNON
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Prefix = Prefix
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Suffix = " of Cannon"
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Rarity = Rarity
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Damage = Item(itemnum).Data2
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Speed = Item(itemnum).Speed
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(StatType.Strength) = ItemLevel + StatAmount
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(StatType.Intelligence) = ItemLevel + StatAmount
                Player(index).Character(TempPlayer(index).CurChar).RandInv(invslot).Stat(StatType.Spirit) = ItemLevel + StatAmount
        End Select
    End Sub

End Module