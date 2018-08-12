Imports ASFW.Network

Friend Module E_NetworkConfig
    Friend WithEvents Socket As Client

    Friend Sub InitNetwork()
        If Not Socket Is Nothing Then Return
        Socket = New Client(ServerPacket.COUNT)
        PacketRouter()
    End Sub

    Friend Sub Connect()
        Socket.Connect(Options.IP, Options.Port)
    End Sub

    Friend Sub DestroyNetwork()
        ' Calling a disconnect is not necessary when using destroy network as
        ' Dispose already calls it and cleans up the memory internally.
        Socket.Dispose()
    End Sub

#Region " Events "
    Private Sub Socket_ConnectionSuccess() Handles Socket.ConnectionSuccess

    End Sub

    Private Sub Socket_ConnectionFailed() Handles Socket.ConnectionFailed

    End Sub

    Private Sub Socket_ConnectionLost() Handles Socket.ConnectionLost
        MsgBox("Connection was terminated!")
        DestroyNetwork()
        CloseEditor()
    End Sub

    Private Sub Socket_CrashReport(err As String) Handles Socket.CrashReport
        MsgBox("There was a network error -> Report: " & err)
        DestroyNetwork()
        CloseEditor()
    End Sub
#End Region

End Module



