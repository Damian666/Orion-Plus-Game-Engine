Module S_Globals
    Friend Debugging As Boolean
    Friend DebugTxt As Boolean = False
    Friend ConsoleText As String
    Friend ErrorCount As Integer

    ' Used for closing key doors again
    Friend KeyTimer As Integer

    ' Used for gradually giving back npcs hp
    Friend GiveNPCHPTimer As Integer

    Friend GiveNPCMPTimer As Integer

    ' Used for logging
    Friend ServerLog As Boolean

    ' Used for server loop
    Friend ServerOnline As Boolean

    ' Used for outputting text
    Friend NumLines As Integer

    ' Used to handle shutting down server with countdown.
    Friend isShuttingDown As Boolean

    Friend Secs As Integer
    Friend TempMapData As Byte

    Friend Gettingmap As Boolean
    Friend EKeyPair As New Asfw.IO.Encryption.KeyPair()
End Module