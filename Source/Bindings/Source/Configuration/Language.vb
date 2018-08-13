#If CLIENT Then
Imports System
Imports System.IO
Imports Serial = ASFW.IO.Serialization

Public Class LanguageDef
    Public LoadScreen As New LoadScreenDef
    Public Class LoadScreenDef
        Public loading As String = "Loading..."
        Public options As String = "Loading Options..."
        Public network As String = "Initialising Network..."
        Public graphics As String = "Initialising Graphics..."
        Public starting As String = "Starting Game..."
    End Class

    Public MainMenu As New MainMenuDef
    Public Class MainMenuDef
        ' Main Panel
        Public newsheader As String = "Latest News"
        Public news As String = "Welcome To the Orion Client." & Environment.NewLine &
                                "This Is a free open source VB.Net game engine!" & Environment.NewLine &
                                "For help Or support please visit our site at http://ascensionforums.com."

        Public serverstatus As String = "Server Status:"
        Public serveronline As String = "Online"
        Public serverreconnect As String = "Reconnecting..."
        Public serveroffline As String = "Offline"
        Public buttonplay As String = "Play"
        Public buttonregister As String = "Register"
        Public buttoncredits As String = "Credits"
        Public buttonexit As String = "Exit"

        ' Login Panel
        Public login As String = "Login"
        Public loginname As String = "Name : "
        Public loginpass As String = "Password : "
        Public loginchkbox As String = "Save Password?"
        Public loginbutton As String = "Login"

        ' New Char Panel
        Public newchar As String = "Create Character"
        Public newcharname As String = "Name : "
        Public newcharclass As String = "Class : "
        Public newchargender As String = "Gender : "
        Public newcharmale As String = "Male"
        Public newcharfemale As String = "Female"
        Public newcharsprite As String = "Sprite"
        Public newcharbutton As String = "Create Character"

        ' Char Select
        Public selchar As String = "Character Selection"
        Public selcharnew As String = "New Character"
        Public selcharuse As String = "Use Character"
        Public selchardel As String = "Delete Character"

        ' New Account
        Public newacc As String = "New Account"
        Public newaccname As String = "Username : "
        Public newaccpass As String = "Password : "
        Public newaccpass2 As String = "Retype Password : "

        ' Credits
        Public credits As String = "Credits"

        ' Ip Config
        Public ipconfig As String = "IP Config"
        Public ipconfigadres As String = "IP Adress : "
        Public ipconfigport As String = "Port : "

        ' Misc
        Public stringlegal As String = "You cannot use high ASCII characters In your name, please re-enter."
        Public sendaddchar As String = "Connected, sending character addition data..."
        Public sendnewacc As String = "Connected, sending New account information..."
        Public sendlogin As String = "Connected, sending login information..."
        Public connectserver As String = "Connecting To server...( {0} )"
    End Class

    Public GameGui As New GameGuiDef
    Public Class GameGuiDef
        Public mapname As String = "Map : "
        Public fps As String = "Fps : "
        Public lps As String = "Lps : "
        Public ping As String = "Ping : "
        Public clock As String = "Time : "
        Public pingsync As String = "Sync"
        Public pinglocal As String = "Local"
        Public maprecieve As String = "Recieving Map..."
        Public datarecieve As String = "Receiving game data..."
        Public curloc As String = "Mouse Coordinates[{0}, {1}]"
        Public loc As String = "Coordinates[{0}, {1}]"
        Public curmap As String = "Map # {0}"
    End Class

    Public ChatCommand As New ChatCommandDef
    Public Class ChatCommandDef
        Public playermsg As String = "Usage : !playername(message)"
        Public emote As String = "Usage : /emote (number 1 to 11)"
        Public help1 As String = "Social Commands : "
        Public help2 As String = "'msghere = Global Message"
        Public help3 As String = "-msghere = Party Message"
        Public help4 As String = "!namehere msghere = Player Message"
        Public help5 As String = "Available Commands: /help, /info, /who, " &
                                 "/fps, /lps, /stats, /trade, /party, /join, /leave, /sellhouse, /houseinvite"

        Public houseinvite As String = "Usage : /houseinvite (name)"
        Public info As String = "Usage : /info (name)"
        Public party As String = "Usage : /party (name)"
        Public wrongcmd As String = "''Not a valid command!''"
    End Class
      
    Public AdminChatCommand As New AdminChatCommandDef
    Public Class AdminChatCommandDef
        Public accesswarning As String = "You need To be a high enough staff member To Do this!"
        Public questreset As String = "Usage : /questreset (quest #)"
        Public wrongquestnr As String = "Invalid quest number."
        Public admin1 As String = "Social Commands : "
        Public adminglobal As String = "''''msghere = Global Admin Message"
        Public adminprivate As String = "= msghere = Private Admin Message"
        Public admin2 As String = "Available Commands: /admin, /loc, /mapeditor, /warpmeto, /warptome, /warpto, " &
                                  "/setsprite, /mapreport, /kick, /ban, /respawn,  /welcome, /questreset"

        Public kick As String = "Usage : /kick (name)"
        Public warpmeto As String = "Usage : /warpmeto (name)"
        Public warptome As String = "Usage : /warptome (name)"
        Public warpto As String = "Usage : /warpto (map #)"
        Public wrongmapnr As String = "Invalid map number."
        Public setsprite As String = "Usage : /setsprite (sprite #)"
        Public welcome As String = "Usage : /welcome (New welcome)"
        Public ban As String = "Usage : /ban (name)"
        Public setaccess As String = "Usage : /setaccess (name) (access)"
    End Class
      
    Public ItemDescription As New ItemDescriptionDef
    Public Class ItemDescriptionDef
        Public notavail As String = "Not Available"
        Public damage As String = "Damage : "
        Public defense As String = "Defence : "
        Public weapon As String = "Weapon"
        Public armor As String = "Armor"
        Public helmet As String = "Helmet"
        Public shield As String = "Shield"
        Public shoes As String = "Shoes"
        Public gloves As String = "Gloves"
        Public restore As String = "Restore Amount :"
        Public potion As String = "Potion"
        Public key As String = "Key"
        Public currency As String = "Currency"
        Public skill As String = "Skill"
        Public furniture As String = "Furniture"
        Public none As String = "None"
        Public secs As String = "Secs"
        Public amount As String = "Amount : "
    End Class
      
    Public SkillDescription As New SkillDescriptionDef
    Public Class SkillDescriptionDef
        Public no As String = "No"
        Public damage As String = "Damage : "
        Public damagehp As String = "Damage Hp"
        Public damagemp As String = "Damage Mp"
        Public heal As String = "Heal : "
        Public healhp As String = "Heal Hp"
        Public healmp As String = "Heal Mp"
        Public warp As String = "Warp"
        Public none As String = "None"
        Public tiles As String = "Tiles"
        Public selfcast As String = "Self - Cast"
    End Class
  
    Public Crafting As New CraftingDef
    Public Class CraftingDef
        Public notenough As String = "Not Enough Materials!"
        Public noneselected As String = "None Selected"
    End Class
      
    Public Trade As New TradeDef
    Public Class TradeDef
        Public tradetimeout As String = "You took too Long To decide. Please Try again."
        Public tradeinvite As String = "{0} invites you To a trade!"
        Public tradeworth As String = "Total Worth : {0} g"
        Public tradestatusok As String = "Other player has accepted."
        Public tradestatuswait As String = "You accepted the trade."
    End Class
      
    Public Events As New EventsDef
    Public Class EventsDef
        Public _continue As String = "- Continue -"
    End Class

    Public Quests As New QuestsDef
    Public Class QuestsDef
        Public queststarted As String = "Quest Started"
        Public questcancel As String = "Cancel Quest"
        Public questcomplete As String = "Quest Completed"
        Public questgoslay As String = "Defeat {0}/{1} {2}."
        Public questgocollect As String = "Collect {0}/{1} {2}."
        Public questtalkto As String = "Go talk To {0}."
        Public questgoto As String = "Go To {0}."
        Public questgive As String = "Give {0} the {1} {2}/{3} they requested."
        Public questkill As String = "Defeat {0}/{1} Players In Battle."
        Public questgather As String = "Gather {0}/{1} {2}."
        Public questfetch As String = "Fetch {0} X {1} from {2}."
    End Class
    
    Public CharWindow As New CharWindowDef
    Public Class CharWindowDef
        Public charname As String = "Name : "
        Public charclass As String = "Class : "
        Public charlvl As String = "Lvl : "
        Public charpoints As String = "StatPoints : "
        Public charstatslbl As String = "== Basic Stats=="
        Public charstrength As String = "Strength : "
        Public charendurance As String = "Endurance : "
        Public charvitality As String = "Vitality : "
        Public charintelligence As String = "Intelligence : "
        Public charluck As String = "Luck : "
        Public charspirit As String = "Spirit : "
        Public chargather As String = "== Gather Stats =="
        Public charherb As String = "Herbalist Lvl : "
        Public charwood As String = "Woodcutter Lvl : "
        Public charmine As String = "Miner Lvl : "
        Public charfish As String = "Fisherman Lvl : "
        Public charexp As String = "Exp : "
    End Class
End Class

Namespace Configuration
    Friend Module modLanguage
        Public Language As New LanguageDef
                
        ''' <summary>
        ''' Loads language file.
        ''' </summary>
        Friend Sub LoadLanguage(lang As String)
            Dim cd = Environment.CurrentDirectory
            Dim cf = "\Languages\" & lang & ".xml"

            ' Use local path if App Dir contains no override.            
            If Not File.Exists(cd & cf) Then cd = Path_Local()

            Try ' Load the file
                Language = Serial.LoadXml(Of LanguageDef)(cf)
            Catch ' The file is missing or incompatible so overwrite it.
                If File.Exists(cd & cf) Then File.Delete(cd & cf)
                SaveLanguage(cd, lang)
            End Try
        End Sub

        ''' <summary>
        ''' Saves language file.
        ''' </summary>
        private Sub SaveLanguage(cd As String, lang As string)
            Dim csd = "\Languages\"
            Dim cf = cd & csd & lang & ".xml"
            
            If Not Directory.Exists(cd) Then Directory.CreateDirectory(cd)
            If Not Directory.Exists(cd & csd) Then Directory.CreateDirectory(cd & csd)
            If Not File.Exists(cf) Then File.Create(cf).Dispose()
            If Language Is Nothing Then Language = New LanguageDef
            Serial.SaveXml(Of LanguageDef)(cf, Language)
        End Sub
    End Module
End Namespace
#End If