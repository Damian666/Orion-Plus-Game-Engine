Namespace Path
    Friend Module modPaths

        ''' <summary> Returns app directory </summary>
        Friend ReadOnly Property Local As String
            Get
                Return Application.StartupPath() & "/"
            End Get
        End Property

        ''' <summary> Returns contents directory </summary>
        Friend ReadOnly Property Contents As String
            Get
                Return Application.StartupPath() & "/Contents/"
            End Get
        End Property

        ''' <summary> Returns database directory </summary>
        Friend ReadOnly Property Database As String
            Get
                Return Application.StartupPath() & "/Database/"
            End Get
        End Property

        ''' <summary> Returns configuration directory </summary>
        Friend ReadOnly Property Config As String
            Get
#If CLIENT Then
                Return Environment.GetFolderPath(
                       Environment.SpecialFolder.MyDocuments) & "/" &
                       Settings.GameName & "/"
#ElseIf SERVER Then
                Return Application.StartupPath() & "/Configuration/"
#End If
            End Get
        End Property


        '############################
        '###  Unique Directories  ###
        '############################

#If CLIENT Then

        ''' <summary> Returns graphics directory </summary>
        Friend ReadOnly Property Graphics As String
            Get
                Return Contents & "/Graphics/"
            End Get
        End Property

        ''' <summary> Returns gui directory </summary>
        Friend ReadOnly Property Gui As String
            Get
                Return Contents & "/Gui/"
            End Get
        End Property

        ''' <summary> Returns music directory </summary>
        Friend ReadOnly Property Music As String
            Get
                Return Contents & "/Music/"
            End Get
        End Property

        ''' <summary> Returns sounds directory </summary>
        Friend ReadOnly Property Sounds As String
            Get
                Return Contents & "/Sounds/"
            End Get
        End Property

#ElseIf SERVER Then

        ''' <summary> Returns accounts directory </summary>
        Friend ReadOnly Property Accounts As String
            Get
                Return Application.StartupPath() & "/Database/Accounts/"
            End Get
        End Property

        ''' <summary> Returns account file </summary>
        Friend Function Account(index As Integer) As String
            Return Accounts & index & ".dat"
        End Function

        ''' <summary> Returns animations directory </summary>
        Friend ReadOnly Property Animations As String
            Get
                Return Application.StartupPath() & "/Database/Animations/"
            End Get
        End Property

        ''' <summary> Returns animation file </summary>
        Friend Function Animation(index As Integer) As String
            Return Animations & index & ".dat"
        End Function

        ''' <summary> Returns items directory </summary>
        Friend ReadOnly Property Items As String
            Get
                Return Application.StartupPath() & "/Database/Items/"
            End Get
        End Property

        ''' <summary> Returns item file </summary>
        Friend Function Item(index As Integer) As String
            Return Items & index & ".dat"
        End Function

        ''' <summary> Returns logs directory </summary>
        Friend ReadOnly Property Logs As String
            Get
                Return Application.StartupPath() & "/Logs/"
            End Get
        End Property

        ''' <summary> Returns maps directory </summary>
        Friend ReadOnly Property Maps As String
            Get
                Return Application.StartupPath() & "/Database/Maps/"
            End Get
        End Property

        ''' <summary> Returns map file </summary>
        Friend Function Map(index As Integer) As String
            Return Maps & index & ".dat"
        End Function

        ''' <summary> Returns npcs directory </summary>
        Friend ReadOnly Property Npcs As String
            Get
                Return Application.StartupPath() & "/Database/Npcs/"
            End Get
        End Property

        ''' <summary> Returns npc file </summary>
        Friend Function Npc(index As Integer) As String
            Return Npcs() & index & ".dat"
        End Function

        ''' <summary> Returns pets directory </summary>
        Friend ReadOnly Property Pets As String
            Get
                Return Application.StartupPath() & "/Database/Pets/"
            End Get
        End Property

        ''' <summary> Returns pet file </summary>
        Friend Function Pet(index As Integer) As String
            Return Pets() & index & ".dat"
        End Function

        ''' <summary> Returns projectiles directory </summary>
        Friend ReadOnly Property Projectiles As String
            Get
                Return Application.StartupPath() & "/Database/Projectiles/"
            End Get
        End Property

        ''' <summary> Returns projectile file </summary>
        Friend Function Projectile(index As Integer) As String
            Return Projectiles() & index & ".dat"
        End Function

        ''' <summary> Returns quests directory </summary>
        Friend ReadOnly Property Quests As String
            Get
                Return Application.StartupPath() & "/Database/Quests/"
            End Get
        End Property

        ''' <summary> Returns quest file </summary>
        Friend Function Quest(index As Integer) As String
            Return Quests() & index & ".dat"
        End Function

        ''' <summary> Returns recipes directory </summary>
        Friend ReadOnly Property Recipes As String
            Get
                Return Application.StartupPath() & "/Database/Recipes/"
            End Get
        End Property

        ''' <summary> Returns recipe file </summary>
        Friend Function Recipe(index As Integer) As String
            Return Recipes() & index & ".dat"
        End Function

        ''' <summary> Returns resources directory </summary>
        Friend ReadOnly Property Resources As String
            Get
                Return Application.StartupPath() & "/Database/Resources/"
            End Get
        End Property

        ''' <summary> Returns resource file </summary>
        Friend Function Resource(index As Integer) As String
            Return Resources() & index & ".dat"
        End Function

        ''' <summary> Returns shops directory </summary>
        Friend ReadOnly Property Shops As String
            Get
                Return Application.StartupPath() & "/Database/Shops/"
            End Get
        End Property

        ''' <summary> Returns shop file </summary>
        Friend Function Shop(index As Integer) As String
            Return Shops() & index & ".dat"
        End Function

        ''' <summary> Returns skills directory </summary>
        Friend ReadOnly Property Skills As String
            Get
                Return Application.StartupPath() & "/Database/Skills/"
            End Get
        End Property

        ''' <summary> Returns skill file </summary>
        Friend Function Skill(index As Integer) As String
            Return Skills() & index & ".dat"
        End Function

#End If

    End Module
End Namespace