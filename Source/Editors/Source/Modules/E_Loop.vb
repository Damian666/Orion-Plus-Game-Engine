Imports System.IO
Imports System.Threading

Module E_Loop
#Region "Startup"
    Friend Sub Main()

        'check if we are in the right place...
        If Not IO.Directory.Exists(Application.StartupPath & "\Data") Then
            MsgBox("Run Editor from inside the Client folder!")
            End
        End If

        If GameStarted = True Then Exit Sub

        SFML.Portable.Activate()

        'Strings.Init(1, "English")
        
        ' set values for directional blocking arrows
        DirArrowX(1) = 12 ' up
        DirArrowY(1) = 0
        DirArrowX(2) = 12 ' down
        DirArrowY(2) = 23
        DirArrowX(3) = 0 ' left
        DirArrowY(3) = 12
        DirArrowX(4) = 23 ' right
        DirArrowY(4) = 12

        CheckTilesets()
        CheckCharacters()
        CheckPaperdolls()
        CheckAnimations()
        CheckItems()
        CheckResources()
        CheckSkillIcons()
        CheckFaces()
        CheckFog()
        CacheMusic()
        CacheSound()

        CheckFurniture()
        CheckProjectiles()

        InitGraphics()
        
        ''Housing
        ReDim House(MAX_HOUSES)
        ReDim HouseConfig(MAX_HOUSES)

        'quests
        ReDim Quest(MAX_QUESTS)
        ClearQuests()
        
        ReDim Item(MAX_ITEMS)
        For i = 0 To MAX_ITEMS
            For x = 0 To StatType.Count - 1
                ReDim Item(i).Add_Stat(x)
            Next
            For x = 0 To StatType.Count - 1
                ReDim Item(i).Stat_Req(x)
            Next

            ReDim Item(i).FurnitureBlocks(3, 3)
            ReDim Item(i).FurnitureFringe(3, 3)
        Next

        ReDim Npc(MAX_NPCS)
        For i = 0 To MAX_NPCS
            For x = 0 To StatType.Count - 1
                ReDim Npc(i).Stat(x)
            Next

            ReDim Npc(i).DropChance(5)
            ReDim Npc(i).DropItem(5)
            ReDim Npc(i).DropItemValue(5)

            ReDim Npc(i).Skill(6)
        Next
        
        ReDim Shop(MAX_SHOPS)
        For i = 0 To MAX_SHOPS
            For x = 0 To MAX_TRADES
                ReDim Shop(i).TradeItem(x)
            Next
        Next

        ReDim Animation(MAX_ANIMATIONS)
        For i = 0 To MAX_ANIMATIONS
            For x = 0 To 1
                ReDim Animation(i).Sprite(x)
            Next
            For x = 0 To 1
                ReDim Animation(i).Frames(x)
            Next
            For x = 0 To 1
                ReDim Animation(i).LoopCount(x)
            Next
            For x = 0 To 1
                ReDim Animation(i).LoopTime(x)
            Next
        Next

        'craft
        ClearRecipes()

        'pets
        ClearPets()

        ' load options
        If File.Exists(Application.StartupPath & "\Data\Config.xml") Then
            LoadOptions()
        Else
            CreateOptions()
        End If

        InitNetwork()

        GameDestroyed = False
        GameStarted = True

        FrmLogin.Visible = True

        GameLoop()

    End Sub
#End Region

#Region "Options"
    Friend Sub CreateOptions()
        Dim myXml As New XmlClass With {
            .Filename = Application.StartupPath & "\Data\Config.xml",
            .Root = "Options"
        }

        myXml.NewXmlDocument()

        Options.Password = ""
        Options.SavePass = False
        Options.Username = ""
        Options.IP = "Localhost"
        Options.Port = 7001
        Options.MenuMusic = ""
        Options.Music = 1
        Options.Sound = 1
        Options.Volume = 100

        myXml.LoadXml()

        myXml.WriteString("UserInfo", "Username", Trim$(Options.Username))
        myXml.WriteString("UserInfo", "Password", Trim$(Options.Password))
        myXml.WriteString("UserInfo", "SavePass", Trim$(Options.SavePass))

        myXml.WriteString("Connection", "Ip", Trim$(Options.IP))
        myXml.WriteString("Connection", "Port", Trim$(Options.Port))

        myXml.WriteString("Sfx", "MenuMusic", Trim$(Options.MenuMusic))
        myXml.WriteString("Sfx", "Music", Trim$(Options.Music))
        myXml.WriteString("Sfx", "Sound", Trim$(Options.Sound))
        myXml.WriteString("Sfx", "Volume", Trim$(Options.Volume))

        myXml.CloseXml(True)
    End Sub

    Friend Sub LoadOptions()
        Dim myXml As New XmlClass With {
            .Filename = Application.StartupPath & "\Data\Config.xml",
            .Root = "Options"
        }

        myXml.LoadXml()

        Options.Username = myXml.ReadString("UserInfo", "Username", "")
        Options.Password = myXml.ReadString("UserInfo", "Password", "")
        Options.SavePass = myXml.ReadString("UserInfo", "SavePass", "False")

        Options.IP = myXml.ReadString("Connection", "Ip", "127.0.0.1")
        Options.Port = Val(myXml.ReadString("Connection", "Port", "7001"))

        Options.MenuMusic = myXml.ReadString("Sfx", "MenuMusic", "")
        Options.Music = myXml.ReadString("Sfx", "Music", "1")
        Options.Sound = myXml.ReadString("Sfx", "Sound", "1")
        Options.Volume = Val(myXml.ReadString("Sfx", "Volume", "100"))

        myXml.CloseXml(False)

        FrmLogin.txtLogin.Text = Options.Username
        FrmLogin.txtPassword.Text = Options.Password
    End Sub
#End Region

    Sub GameLoop()
        Dim starttime As Integer, Tick As Integer, fogtmr As Integer
        Dim FrameTime As Integer, tmr500 As Integer
        Dim tmpfps As Integer, rendercount As Integer

        starttime = GetTickCount()

        Do
            If GameDestroyed = True Then End

            UpdateUI()

            If GameStarted = True Then
                Tick = GetTickCount()

                ' update animation editor
                If Editor = EDITOR_ANIMATION Then
                    EditorAnim_DrawAnim()
                End If

                FrameTime = Tick
                If InMapEditor AndAlso Not GettingMap Then

                    'Calculate FPS
                    If starttime < Tick Then
                        FPS = tmpfps

                        tmpfps = 0
                        starttime = GetTickCount() + 1000
                    End If
                    tmpfps = tmpfps + 1

                        ' fog scrolling
                        If fogtmr < Tick Then
                            If CurrentFogSpeed > 0 Then
                                'move
                                fogOffsetX = fogOffsetX - 1
                                fogOffsetY = fogOffsetY - 1
                                Reset()
                                If fogOffsetX < -256 Then fogOffsetX = 0
                                If fogOffsetY < -256 Then fogOffsetY = 0
                                fogtmr = Tick + 255 - CurrentFogSpeed
                            End If
                        End If
                        
                        If FadeInSwitch = True Then
                            FadeIn()
                        End If

                        If FadeOutSwitch = True Then
                            FadeOut()
                        End If

                        Application.DoEvents()

                End If
            End If

            Application.DoEvents()
            Thread.Sleep(1)
        Loop
    End Sub

    Sub UpdateUI()
        If InitEditor = True Then
            FrmLogin.pnlAdmin.Visible = True
            InitEditor = False
        End If

        If InitPetEditor = True Then
            With frmPet
                Editor = EDITOR_PET
                .lstIndex.Items.Clear()

                ' Add the names
                For i = 1 To MAX_PETS
                    .lstIndex.Items.Add(i & ": " & Trim$(Pet(i).Name))
                Next

                .cmbEvolve.Items.Clear()

                .cmbEvolve.Items.Add("None")

                ' Add the names
                For i = 1 To MAX_PETS
                    .cmbEvolve.Items.Add(i & ": " & Trim$(Pet(i).Name))
                Next

                .Show()
                .lstIndex.SelectedIndex = 0
                .cmbEvolve.SelectedIndex = 0
                PetEditorInit()
            End With
            InitPetEditor = False
        End If

        If QuestEditorShow = True Then
            With frmQuest
                Editor = EDITOR_TASKS
                .lstIndex.Items.Clear()
                .cmbQuestReq.Items.Clear()
                .cmbQuestReq.Items.Add("None")
                ' Add the names
                For I = 1 To MAX_QUESTS
                    .lstIndex.Items.Add(I & ": " & Trim$(Quest(I).Name))
                    .cmbQuestReq.Items.Add(I & ": " & Trim$(Quest(I).Name))
                Next

                .Show()
                .lstIndex.SelectedIndex = 0
                QuestEditorInit()
            End With
            QuestEditorShow = False
        End If

        If InitAnimationEditor = True Then
            With FrmAnimation
                Editor = EDITOR_ANIMATION
                .lstIndex.Items.Clear()

                ' Add the names
                For i = 1 To MAX_ANIMATIONS
                    .lstIndex.Items.Add(i & ": " & Trim$(Animation(i).Name))
                Next

                .Show()
                .lstIndex.SelectedIndex = 0
                AnimationEditorInit()
            End With
            InitAnimationEditor = False
        End If
        
        If InitItemEditor = True Then
            ItemEditorPreInit()
            InitItemEditor = False
        End If

        If InitRecipeEditor = True Then
            RecipeEditorPreInit()
            InitRecipeEditor = False
        End If

        If InitClassEditor = True Then
            ClassEditorInit()
            InitClassEditor = False
        End If

        If LoadClassInfo = True Then
            LoadClass()
            LoadClassInfo = False
        End If

        If InitResourceEditor = True Then
            Dim i As Integer

            With frmResource
                Editor = EDITOR_RESOURCE
                .lstIndex.Items.Clear()

                ' Add the names
                For i = 1 To MAX_RESOURCES
                    If Resource(i).Name Is Nothing Then Resource(i).Name = ""
                    If Resource(i).SuccessMessage Is Nothing Then Resource(i).SuccessMessage = ""
                    If Resource(i).EmptyMessage Is Nothing Then Resource(i).EmptyMessage = ""
                    .lstIndex.Items.Add(i & ": " & Trim$(Resource(i).Name))
                Next

                .Show()
                .lstIndex.SelectedIndex = 0
                ResourceEditorInit()
            End With
            InitResourceEditor = False
        End If

        If InitNPCEditor = True Then
            With frmNPC
                Editor = EDITOR_NPC
                .lstIndex.Items.Clear()

                ' Add the names
                For i = 1 To MAX_NPCS
                    .lstIndex.Items.Add(i & ": " & Trim$(Npc(i).Name))
                Next

                .Show()
                .lstIndex.SelectedIndex = 0
                NpcEditorInit()
            End With
            InitNPCEditor = False
        End If

        If InitSkillEditor = True Then
            With frmSkill
                Editor = EDITOR_SKILL
                .lstIndex.Items.Clear()

                ' Add the names
                For i = 1 To MAX_SKILLS
                    .lstIndex.Items.Add(i & ": " & Trim$(Skill(i).Name))
                Next

                .Show()
                .lstIndex.SelectedIndex = 0
                SkillEditorInit()
            End With
            InitSkillEditor = False
        End If

        If InitShopEditor = True Then
            With frmShop
                Editor = EDITOR_SHOP
                .lstIndex.Items.Clear()

                ' Add the names
                For i = 1 To MAX_SHOPS
                    .lstIndex.Items.Add(i & ": " & Trim$(Shop(i).Name))
                Next

                .Show()
                .lstIndex.SelectedIndex = 0
                ShopEditorInit()
            End With
            InitShopEditor = False
        End If

        If InitAnimationEditor = True Then
            With FrmAnimation
                Editor = EDITOR_ANIMATION
                .lstIndex.Items.Clear()

                ' Add the names
                For i = 1 To MAX_ANIMATIONS
                    .lstIndex.Items.Add(i & ": " & Trim$(Animation(i).Name))
                Next

                .Show()
                .lstIndex.SelectedIndex = 0
                AnimationEditorInit()
            End With
            InitAnimationEditor = False
        End If

        If HouseEdit = True Then
            With FrmHouse
                Editor = EDITOR_HOUSE
                .lstIndex.Items.Clear()

                ' Add the names
                For i = 1 To MAX_HOUSES
                    .lstIndex.Items.Add(i & ": " & Trim$(House(i).ConfigName))
                Next

                .Show()
                .lstIndex.SelectedIndex = 0
            End With

            HouseEditorInit()

            HouseEdit = False
        End If
        
        If InitProjectileEditor = True Then
            With frmProjectile
                Editor = EDITOR_PROJECTILE
                .lstIndex.Items.Clear()

                ' Add the names
                For i = 1 To MAX_PROJECTILES
                    .lstIndex.Items.Add(i & ": " & Trim$(Projectiles(i).Name))
                Next

                .Show()
                .lstIndex.SelectedIndex = 0
                ProjectileEditorInit()
            End With

            InitProjectileEditor = False
        End If

        If frmProjectile.Visible Then
            EditorProjectile_DrawProjectile()
        End If

        If InitAutoMapper = True Then
            OpenAutomapper()
            InitAutoMapper = False
        End If
    End Sub

    Sub CloseEditor()
        SendLeaveGame()

        GameDestroyed = True
        GameStarted = False

        Application.Exit()
    End Sub

End Module