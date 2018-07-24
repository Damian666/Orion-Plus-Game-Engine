Friend Class NoResetPanel : Inherits Panel
    Protected Overrides Function ScrollToControl(activeControl As Control) As Point
        ' Returning the current location prevents the panel from
        ' scrolling to the active control when the panel loses And regains focus
        Return DisplayRectangle.Location
    End Function
End Class