Friend Module C_Entry
    Friend Sub Main(args() As String)
        If args.Length < 1 OrElse
           args(0) <> My.Settings.Password Then Return


    End Sub

    Friend Sub Destroy()

    End Sub
End Module