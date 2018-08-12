Imports System
Imports ASFW.IO.Encryption

Namespace Configuration
    Friend Module modEncryption
        Friend Encryption As New KeyPair

#If SERVER Then
        ''' <summary>
        ''' Generates or Loads an Encryption Key file.
        ''' </summary>
        Friend Sub InitializeEncryption()
            Dim cf = Environment.CurrentDirectory & "\Encryption.ssl"
            If Not IO.File.Exists(cf) Then
                Encryption.GenerateKeys(KeyPair.KeyType.Signature)
                Encryption.ExportKey(cf, True)
            Else : Encryption.ImportKey(cf) : End If
        End Sub
#End If
    End Module
End Namespace