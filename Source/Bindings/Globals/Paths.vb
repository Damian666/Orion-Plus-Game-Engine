Imports System
Imports System.Windows.Forms

Friend Module modPaths
    '##################'
    '###  OS Paths  ###'
    '##################'
    Friend Function Path_Fonts() As String
        Return Environment.GetFolderPath(Environment.SpecialFolder.Fonts) & "\"
    End Function
    Friend Function Path_Local() As String
        Return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\" & Application.CompanyName & "\" & Application.ProductName & "\"
    End Function

#If Not SERVER Then
    '########################'
    '###  Resource Paths  ###'
    '########################'
    Friend Function Path_Graphics() As String
        Return Environment.CurrentDirectory & "\Contents\Graphics\"
    End Function
    Friend Function Path_Gui() As String
        Return Environment.CurrentDirectory & "\Contents\Gui\"
    End Function
    Friend Function Path_Log() As String
        Return Environment.CurrentDirectory & "\Contents\Logs\"
    End Function
    Friend Function Path_Music() As String
        Return Environment.CurrentDirectory & "\Contents\Music\"
    End Function
    Friend Function Path_Sound() As String
        Return Environment.CurrentDirectory & "\Contents\Sound\"
    End Function
#End If
End Module