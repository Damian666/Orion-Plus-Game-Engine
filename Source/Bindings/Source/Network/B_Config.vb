Imports ASFW.Network
Imports System

#If DEBUG Then
Imports System.Diagnostics
Imports System.Net
Imports System.Windows.Forms
#End If

Namespace Network
    Partial Friend Module modNetwork
        ' Event based network object made by ArchaicSoft.
#If CLIENT Then
        Private WithEvents Socket As Client
#ElseIf SERVER Then
        Private WithEvents Socket As Server
#End If

        ''' <summary>
        ''' Creates Or renews local "Socket" object for network use And establishes
        ''' rules And functionality.
        ''' </summary>
        Friend Sub Initialize()

            ' Don't double initialize. Call Destroy() first.
            If Not Socket Is Nothing Then Return

#If CLIENT Then
            Socket = New Client(ServerPacket.COUNT)
#ElseIf SERVER Then
            Socket = New Server(ClientPacket.Count, MAX_PLAYERS) With {  ' Change to 0 for no player limit.
                .BufferLimit = 2048000, ' 2mb max data storage for a single packet.
                .PacketAcceptLimit = 100, ' Amount allowed to be accepted/handled before refusing more data.
                .PacketDisconnectCount = 150 ' Amount received before assuming ddos/speed-hacking.
                }
#End If

            ' Initialize the PacketID pointer values.
            InitializePacketPointers()
        End Sub

        ''' <summary>
        ''' Destroys local "Socket" object and cleans up associated data on the network.
        ''' </summary>
        Friend Sub Destroy()
            ' Do nothing is socket is nothing
            If Socket Is Nothing Then Return
            
            ' Calling a disconnect is not necessary when using destroy network as
            ' Dispose already calls it and cleans up the memory internally.
            Try ' Prevents async crash.
                Socket.Dispose()
                Socket = Nothing
            Catch ' Do Nothing
            End Try
        End Sub

#If CLIENT Then
        ''' <summary>
        ''' Makes an attempt to connect to the server.
        ''' </summary>
        Friend Sub Connect(ip As String, port As Integer)
            Socket.Connect(ip, port)
        End Sub

        ''' <summary>
        ''' Returns true if connection exists.
        ''' May return false positive if no data transfers attempt
        ''' to occur between improper network closing on server side.
        ''' EX: Crash.
        ''' </summary>
        Friend Function IsConnected() As Boolean
            Return Not Socket Is Nothing AndAlso Socket.IsConnected
        End Function

        Friend Sub SendData(data As Byte())
            Socket.SendData(data)
        End Sub
#ElseIf SERVER Then
        ''' <summary>
        ''' Makes an attempt to retrieve public Ip. Returns localhost if failed.
        ''' </summary>
        Friend Function GetPublicIp() As String
            Dim request = WebRequest.Create(New Uri("http://ascensiongamedev.com/resources/myip.php"))
            request.Method = WebRequestMethods.Http.Get

            Try
                Dim reader As New IO.StreamReader(request.GetResponse().GetResponseStream())
                Return reader.ReadToEnd()
            Catch
                Return "localhost"
            End Try
        End Function

        ''' <summary>
        ''' Begins listening. Reports error if listening is already being done.
        ''' </summary>
        Friend Sub StartListening(port As Integer, backlog As Integer)
            ' Begin listening for clients.
            Try
                Socket.StartListening(port, backlog, 1)
            Catch
                Console.WriteLine("Port already in use. Listening failed.")
                Socket.StopListening() ' Just to make sure we don't have broken values internally.
            End Try
        End Sub

        ''' <summary>
        ''' Stops listening for new connections.
        ''' </summary>
        Friend Sub StopListening()
            If Socket Is Nothing OrElse
               Not Socket.IsListening Then Return

            Socket.StopListening()
        End Sub

        ''' <summary>
        ''' Returns true if server is currently listening
        ''' for incoming connections on any port.
        ''' </summary>
        Friend Function IsListening() As Boolean
            Return Socket.IsListening
        End Function

        ''' <summary>
        ''' Disconnects the client on "index" and clears their connection
        ''' data from "Socket" object.
        ''' </summary>
        Friend Sub Disconnect(index As Integer)
            ' Save the player then remove their player object.
            SavePlayer(index)
            ClearPlayer(index)
            Socket.Disconnect(index)
        End Sub

        ''' <summary>
        ''' Returns highest index used on the network.
        ''' </summary>
        Friend Function HighIndex() As Integer
            Return Socket.HighIndex
        End Function

        ''' <summary>
        ''' Returns remote ip-address of client index.
        ''' </summary>
        Friend Function ClientIp(index As Integer) As String
            Return Socket.ClientIp(index)
        End Function

        ''' <summary>
        ''' Returns true if "address" is already active and in-use.
        ''' </summary>
        Friend Function IsIpActive(address As String) As Boolean
            For i = 1 To Socket.HighIndex
                If Socket.IsConnected(i) AndAlso Socket.ClientIp(i) = address Then
                    Return True
                End If
            Next
            Return False
        End Function

        ''' <summary>
        ''' Returns true if player object contains a login.
        ''' </summary>
        Friend Function IsLoggedIn(index As Integer) As Boolean
            Return Player(index).Login.Trim.Length > 0
        End Function

        ''' <summary>
        ''' Returns true if TempPlayer registers 'InGame'.
        ''' </summary>
        Friend Function IsPlaying(index As Integer) As Boolean
            Return TempPlayer(index).InGame
        End Function

        ''' <summary>
        ''' Returns true if account is already active.
        ''' </summary>
        Friend Function IsAccountActive(Login As String) As Boolean
            ' Lol this was broke before ~ SpiceyWolf
            For i As Integer = 1 To Socket.HighIndex
                If Player(i).Login.Trim.ToLower() = Login.Trim.ToLower() Then Return True
            Next
            Return False
        End Function

        ''' <summary>
        ''' Returns true if connection exists on the index.
        ''' May return false positive if no data transfers attempt
        ''' to occur between improper network closing on client side.
        ''' EX: Crash.
        ''' </summary>
        Friend Function IsConnected(index As Integer) As Boolean
            Return Socket.IsConnected(index)
        End Function

        ''' <summary>
        ''' Send Packet-Data to a specified index.
        ''' </summary>
        Friend Sub SendDataTo(index As Integer, data As Byte())
            Socket.SendDataTo(index, data)
        End Sub

        ''' <summary>
        ''' Send Packet-Data to all in-game connections.
        ''' </summary>
        Friend Sub SendDataToAll(data() As Byte)
            For i = 1 To Socket.HighIndex
                If IsPlaying(i) Then
                    Socket.SendDataTo(i, data)
                End If
            Next
        End Sub

        ''' <summary>
        ''' Send Packet-Data to all in-game connections except the specified index.
        ''' </summary>
        Friend Sub SendDataToAllBut(index As Integer, data() As Byte)
            For i = 1 To Socket.HighIndex
                If IsPlaying(i) AndAlso i <> index Then
                    Socket.SendDataTo(i, data)
                End If
            Next
        End Sub

        ''' <summary>
        ''' Send Packet-Data to all connections on the specified map.
        ''' </summary>
        Friend Sub SendDataToMap(mapIndex As Integer, data() As Byte)
            For i = 1 To Socket.HighIndex
                If IsPlaying(i) AndAlso GetPlayerMap(i) = mapIndex Then
                    Socket.SendDataTo(i, data)
                End If
            Next
        End Sub

        ''' <summary>
        ''' Send Packet-Data to all connections on the specified map
        ''' except the specified index.
        ''' </summary>
        Friend Sub SendDataToMapBut(index As Integer, mapIndex As Integer, data() As Byte)
            For i = 1 To Socket.HighIndex
                If IsPlaying(i) AndAlso GetPlayerMap(i) = mapIndex AndAlso i <> index Then
                    Socket.SendDataTo(i, data)
                End If
            Next
        End Sub
#End If

#Region " Events "
#If CLIENT Then
        Private Sub Socket_ConnectionSuccess() Handles Socket.ConnectionSuccess
            ' Do nothing for now.
        End Sub

        Private Sub Socket_ConnectionFailed() Handles Socket.ConnectionFailed
            ' Attempt Reconnect. We should be on main menu when this occurs.
            Connect(Configuration.Settings.Ip, Configuration.Settings.Port)
        End Sub

        Private Sub Socket_ConnectionLost() Handles Socket.ConnectionLost
            MessageBox.Show("You have been disconnected from the Server.",
                            "Connection Lost",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
            Destroy()
            Terminate()
        End Sub

        Private Sub Socket_CrashReport(err As String) Handles Socket.CrashReport
            ' Need to destroy no matter what on a crash.
            Destroy()

            ' These errors are harmless but we'd rather not see them or crash due to them.
            If err = "BufferUnderflowException" OrElse ' Server disconnected mid-read.
               err = "ConnectionForciblyClosedException" Then ' Server turned itself off.

                ' Reset the network
                Initialize()

                ' Set us back to main menu. TODO: FIX THIS
                'MainWindow.Instance.SetScreen_MainMenu()

            Else
                ' Fatal crash can't be repaired. Close app.
                MessageBox.Show("Error: " & err,
                            "Network Crash Report",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
                Terminate()
            End If
        End Sub
#ElseIf SERVER Then
        Private Sub Socket_ConnectionReceived(index As Integer) Handles Socket.ConnectionReceived
#If DEBUG Then
            Console.WriteLine("Connection received on index[" & index & "] - IP[" & Socket.ClientIp(index) & "]")
#End If

            ' Send encryption key so they can safely send login or registration data.
            SendEncryptionKey(index)
            ' Send the latest news.
            SendNews(index)
        End Sub

        Private Sub Socket_ConnectionLost(index As Integer) Handles Socket.ConnectionLost
#If DEBUG Then
            Console.WriteLine("Connection lost on index[" & index & "] - IP[" & Socket.ClientIp(index) & "]")
#End If
            ' Remove their player object.
            LeftGame(index)
        End Sub

        Private Sub Socket_CrashReport(index As Integer, err As String) Handles Socket.CrashReport
            ' These errors are harmless but we'd rather not see them or crash due to them.
            If err = "BufferUnderflowException" OrElse ' Client disconnected mid-read.
               err = "ConnectionForciblyClosedException" Then ' Client turned itself off.
                ' Do nothing!
            Else
                ' Something was ACTUALLY wrong, so report it.
                Console.WriteLine("There was a network error -> Index[" & index & "]")
                Console.WriteLine("Report: " & err)
            End If

            ' Remove their player object.
            LeftGame(index)
        End Sub
#End If

#If DEBUG Then
        ''' <summary>
        ''' This packet is for debugging. Can be used to detect frequent traffic
        ''' and what information is sent in general. Intended use is mainly to
        ''' detect how much data is sent over time and sometimes catch spam/ddossers.
        ''' </summary>
        Private Sub Socket_TrafficReceived(size As Integer, ByRef data() As Byte) Handles Socket.TrafficReceived
            Console.WriteLine("Traffic Received : [Size: " & size & "]")

            '  Uncomment this to print off ALL data received by the packet.
            '*****************************************************************'
            '* For i = 1 To size - 1
            '*     Debug.Print("Traffic response[" & i & "]: " & data(i))
            '* Next
            '*****************************************************************'
        End Sub

        ''' <summary>
        ''' This packet is for debugging. Can be used to find broken packets,
        ''' or detect data being sent through faulty packets by a hacker,
        ''' as well as just track what information specificly is sent on a 
        ''' given packet.
        ''' </summary>
        Private Sub Socket_PacketReceived(size As Integer, header As Integer, ByRef data() As Byte) Handles Socket.PacketReceived
#If CLIENT Then
            Console.WriteLine("Packet Received : [Size: " & size & "| Packet: " & CType(header, ServerPacket).ToString() & "]")
#ElseIf SERVER Then
            Console.WriteLine("Packet Received : [Size: " & size & "| Packet: " & CType(header, ClientPacket).ToString() & "]")
#End If

            '  Uncomment this to print off ALL data received by the packet.
            '*****************************************************************'
            '* For i = 1 To size - 1
            '*     Debug.Print("Traffic response[" & i & "]: " & data(i))
            '* Next
            '*****************************************************************'
        End Sub
#End If
#End Region
    End Module
End Namespace