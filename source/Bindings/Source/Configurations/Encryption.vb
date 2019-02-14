Imports ASFW.IO.Encryption

Friend Module modEncryption
    Friend NetKeys As New KeyPair

#If SERVER Then
    ''' <summary>
    ''' Generates or Loads an Encryption Key file.
    ''' </summary>
    Friend Sub InitializeEncryption()
        Dim cf = Environment.CurrentDirectory & "\NetKeys.ssl"

        If Not IO.File.Exists(cf) Then
            NetKeys.GenerateKeys(KeyPair.KeyType.Signature)
            NetKeys.ExportKey(cf, True)
        Else
            NetKeys.ImportKey(cf)
        End If
    End Sub
#End If
End Module