Imports System.IO
Imports ASFW.IO.Serialization

#If CLIENT Then
Public Class LanguageDef

    Public Load As New LoadDef
    Public Class LoadDef

        Public Loading As String = "Loading..."
        Public Graphics As String = "Initialising Graphics.."
        Public Network As String = "Initialising Network..."
        Public Starting As String = "Starting Game..."

    End Class

    Public MainMenu As New MainMenuDef
    Public Class MainMenuDef

        ' Main Panel
        Public ServerStatus As String = "Server Status:"
        Public ServerOnline As String = "Online"
        Public ServerReconnect As String = "Reconnecting..."
        Public ServerOffline As String = "Offline"
        Public ButtonPlay As String = "Play"
        Public ButtonRegister As String = "Register"
        Public ButtonCredits As String = "Credits"
        Public ButtonExit As String = "Exit"
        Public NewsHeader As String = "Latest News"
        Public News As String = "Welcome To the Orion Client.
                                 This Is a free open source VB.Net game engine!
                                 For help Or support please visit our site at
                                 http://ascensionforums.com."

        ' Login Panel
        Public Login As String = "Login"
        Public LoginName As String = "Name : "
        Public LoginPass As String = "Password : "
        Public LoginCheckBox As String = "Save Password?"
        Public LoginButton As String = "Submit"

        ' New Character Panel
        Public NewCharacter As String = "Create Character"
        Public NewCharacterName As String = "Name : "
        Public NewCharacterClass As String = "Class : "
        Public NewCharacterGender As String = "Gender : "
        Public NewCharacterMale As String = "Male"
        Public NewCharacterFemale As String = "Female"
        Public NewCharacterSprite As String = "Sprite"
        Public NewCharacterButton As String = "Submit"

        ' Character Select
        Public UseCharacter As String = "Character Selection"
        Public UseCharacterNew As String = "New Character"
        Public UseCharacterUse As String = "Use Character"
        Public UseCharacterDel As String = "Delete Character"

        ' Register
        Public Register As String = "Registration"
        Public RegisterName As String = "Username : "
        Public RegisterPass1 As String = "Password : "
        Public RegisterPass2 As String = "Retype Password : "

        ' Credits
        Public Credits As String = "Credits"

        ' Misc
        Public StringLegal As String = "You cannot use high ASCII characters In your name, please re-enter."
        Public SendLogin As String = "Connected, sending login information..."
        Public SendNewCharacter As String = "Connected, sending character data..."
        Public SendRegister As String = "Connected, sending registration information..."
        Public ConnectToServer As String = "Connecting To server...( {0} )"

    End Class

    Public Game As New GameDef
    Public Class GameDef

        Public MapName As String = "Map : "
        Public Time As String = "Time : "
        Public Fps As String = "Fps : "
        Public Lps As String = "Lps : "

        Public Ping As String = "Ping : "
        Public PingSync As String = "Sync"
        Public PingLocal As String = "Local"

        Public MapReceive As String = "Recieving Map..."
        Public DataReceive As String = "Receiving game data..."

        Public MapCurMap As String = "Map # {0}"
        Public MapCurLoc As String = "Loc() x: {0} y: {1}"
        Public MapLoc As String = "Cur Loc x: {0} y: {1}"

    End Class

    Public Chat As New ChatDef
    Public Class ChatDef

        ' Universal
        Public Emote As String = "Usage : /emote [1-11]"
        Public Info As String = "Usage : /info [player]"
        Public Party As String = "Usage : /party [player]"
        Public PlayerMsg As String = "Usage : ![player] [message]"
        Public HouseInvite As String = "Usage : /houseinvite [player]"
        Public InvalidCmd As String = "Not a valid command!"
        Public Help1 As String = "Social Commands : "
        Public Help2 As String = "'[message] = Global Message"
        Public Help3 As String = "-[message] = Party Message"
        Public Help4 As String = "![player] [message] = Player Message"
        Public Help5 As String = "Available Commands: /help, /info, 
                                  /who, /fps, /lps, /stats, /trade, 
                                  /party, /join, /leave, /sellhouse, 
                                  /houseinvite"

        ' Admin-Only
        Public AccessAlert As String = "You need a higher access to do this!"
        Public AdminGblMsg As String = "''msghere = Global Admin Message"
        Public AdminPvtMsg As String = "= msghere = Private Admin Message"
        Public Admin1 As String = "Social Commands:"
        Public Admin2 As String = "Available Commands: /admin, /loc, 
                                   /warpmeto, /warptome, /warpto, 
                                   /sprite, /mapreport, /kick, 
                                   /ban, /respawn, /welcome, /questreset"

        Public Welcome As String = "Usage : /welcome [message]"
        Public Access As String = "Usage : /access [player] [access]"
        Public Sprite As String = "Usage : /sprite [index]"
        Public Kick As String = "Usage : /kick [player]"
        Public Ban As String = "Usage : /ban [player]"

        Public WarpMeTo As String = "Usage : /warpmeto [player]"
        Public WarpToMe As String = "Usage : /warptome [player]"
        Public WarpTo As String = "Usage : /warpto [map index]"

        Public ResetQuest As String = "Usage : /questreset [index]"

        Public InvalidMap As String = "Invalid map index."
        Public InvalidQuest As String = "Invalid quest index."

    End Class

    Public ItemDescription As New ItemDescriptionDef
    Public Class ItemDescriptionDef

        Public NotAvailable As String = "Not Available"
        Public None As String = "None"
        Public Seconds As String = "Seconds"

        Public Currency As String = "Currency"
        Public Key As String = "Key"
        Public Furniture As String = "Furniture"
        Public Potion As String = "Potion"
        Public Skill As String = "Skill"

        Public Weapon As String = "Weapon"
        Public Armor As String = "Armor"
        Public Helmet As String = "Helmet"
        Public Shield As String = "Shield"
        Public Shoes As String = "Shoes"
        Public Gloves As String = "Gloves"

        Public Amount As String = "Amount : "
        Public Restore As String = "Restore Amount : "
        Public Damage As String = "Damage : "
        Public Defense As String = "Defense : "

    End Class

    Public SkillDescription As New SkillDescriptionDef
    Public Class SkillDescriptionDef

        Public No As String = "No"
        Public None As String = "None"
        Public Warp As String = "Warp"
        Public Tiles As String = "Tiles"
        Public SelfCast As String = "Self-Cast"

        Public Gain As String = "Regen : "
        Public GainHp As String = "Regen Hp"
        Public GainMp As String = "Regen Mp"
        Public Lose As String = "Syphon : "
        Public LoseHp As String = "Syphon Hp"
        Public LoseMp As String = "Syphon Mp"

    End Class

    Public Crafting As New CraftingDef
    Public Class CraftingDef

        Public NotEnough As String = "Not enough materials!"
        Public NotSelected As String = "Nothing selected"

    End Class

    Public Trade As New TradeDef
    Public Class TradeDef

        Public Request As String = "{0} is requesting to trade."
        Public Timeout As String = "You took too long to decide. Please try again."

        Public Value As String = "Total Value : {0}g"

        Public StatusOther As String = "Other player confirmed offer."
        Public StatusSelf As String = "You confirmed the offer."

    End Class

    Public Events As New EventDef
    Public Class EventDef

        Public OptContinue = "- Continue -"

    End Class

    Public Quest As New QuestDef
    Public Class QuestDef

        Public Cancel As String = "Cancel Started"
        Public Started As String = "Quest Started"
        Public Completed As String = "Quest Completed"

        ' Quest Types
        Public Slay As String = "Defeat {0}/{1} {2}."
        Public Collect As String = "Collect {0}/{1} {2}."
        Public Talk As String = "Go talk To {0}."
        Public Reach As String = "Go To {0}."
        Public TurnIn As String = "Give {0} the {1} {2}/{3} they requested."
        Public Kill As String = "Defeat {0}/{1} Players In Battle."
        Public Gather As String = "Gather {0}/{1} {2}."
        Public Fetch As String = "Fetch {0} X {1} from {2}."

    End Class

    Public Character As New CharacterDef
    Public Class CharacterDef

        Public Name As String = "Name : "
        Public ClassType As String = "Class : "
        Public Level As String = "Lv : "
        Public Exp As String = "Exp : "

        Public StatsLabel As String = "== Stats =="
        Public Strength As String = "Strength : "
        Public Endurance As String = "Endurance : "
        Public Vitality As String = "Vitality : "
        Public Intelligence As String = "Intelligence : "
        Public Luck As String = "Luck : "
        Public Spirit As String = "Spirit : "
        Public Points As String = "Points Available : "

        Public SkillLabel As String = "== Skill Levels =="
        Public Herbalist As String = "Herbalist : "
        Public Woodcutter As String = "Woodcutter : "
        Public Miner As String = "Miner : "
        Public Fisherman As String = "Fisherman : "

    End Class
End Class

Friend Module modLanguage
    Public Language As New LanguageDef

    Friend Sub LoadLanguage()
        Dim cf As String = Path.Config() & "\Languages\"
        If Not Directory.Exists(cf) Then
            Directory.CreateDirectory(cf)
        End If : cf = cf & Settings.Language & ".xml"

        If Not File.Exists(cf) Then
            File.Create(cf).Dispose()
            SaveXml(Of LanguageDef)(cf, New LanguageDef)
        End If : Language = LoadXml(Of LanguageDef)(cf)
    End Sub
End Module
#End If