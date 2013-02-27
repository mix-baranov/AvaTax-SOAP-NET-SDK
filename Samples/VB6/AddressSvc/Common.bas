Attribute VB_Name = "baseCommon"
Option Explicit

Public Sub ShowError(Err As ErrObject)

    MsgBox "[" & Err.Number & "] " & Err.Description & vbCrLf & "at Source: " & Err.Source, vbInformation, "Error"
    
End Sub

Public Sub PreMethodCall(lblStatus As Label)
    Screen.MousePointer = vbHourglass
    lblStatus.Caption = "Contacting web service..."
    lblStatus.Refresh
End Sub

Public Sub PostMethodCall(lblStatus As Label)
    lblStatus.Caption = ""
    Screen.MousePointer = vbDefault
End Sub

Public Sub SetMessageLabelText(oLabel As Label, oResult As BaseResult)

    oLabel.MousePointer = MousePointerConstants.vbIconPointer
    If (oResult.Messages.Count = 0) Then
        oLabel.Caption = "No messages returned"
        oLabel.FontUnderline = False
    Else
        oLabel.Caption = "Click here for messages..."
        oLabel.FontUnderline = True
    End If
    
End Sub

Public Function TranslateSeverity(severity As Long)

    Dim text As String
    
    Select Case severity
        Case SeverityLevel_Success
            text = "Success"
        Case SeverityLevel_Warning
            text = "Warning"
        Case SeverityLevel_Error
            text = "Error"
        Case SeverityLevel_Exception
            text = "Exception"
        Case Else
            text = "Unknown"
    End Select

    TranslateSeverity = text
    
End Function

