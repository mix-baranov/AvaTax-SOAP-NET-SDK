Attribute VB_Name = "basCommon"
Option Explicit

Public Sub ShowError(Err As ErrObject)

    MsgBox "[" & Err.number & "] " & Err.Description & vbCrLf & "at Source: " & Err.Source, vbInformation, "Error"
    
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

Public Sub FillDocTypeCombo(oCombo As ComboBox)
    
    'oCombo.AddItem DocumentType_SalesOrder & " : Sales Order"
    oCombo.AddItem DocumentType_SalesInvoice & " : Sales Invoice"
    'oCombo.AddItem DocumentType_PurchaseOrder & " : Purchase Order"
    oCombo.AddItem DocumentType_PurchaseInvoice & " : Purchase Invoice"
    'oCombo.AddItem DocumentType_ReturnOrder & " : Return Order"
    oCombo.AddItem DocumentType_ReturnInvoice & " : Return Invoice"
    
End Sub

Public Function IsValidDateTime(dateTime As String) As Boolean
    On Error GoTo errIsValidDateTime
    
    If (Len(Trim$(dateTime)) = 0) Then
        IsValidDateTime = True
        Exit Function
    End If
    
    Dim test As Date
    test = CDate(dateTime)
    IsValidDateTime = True
    Exit Function
    
errIsValidDateTime:
    Err.Clear
    IsValidDateTime = False
End Function

Public Function IsValidLong(number As String) As Boolean
    On Error GoTo errIsValidLong
    
    If (Len(Trim$(number)) = 0) Then
        IsValidLong = True
        Exit Function
    End If
    
    IsValidLong = True
    Exit Function
    
errIsValidLong:
    Err.Clear
    IsValidLong = False
End Function

Public Function TranslateSeverity(severity As Long) As String

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

Public Function TranslateDocStatus(status As Long) As String

    Dim text As String
    
    Select Case status
        Case DocStatus_Cancelled
            text = "Cancelled"
        Case DocStatus_Committed
            text = "Committed"
        Case DocStatus_Posted
            text = "Posted"
        Case DocStatus_Saved
            text = "Saved"
        Case DocStatus_Temporary
            text = "Temporary"
    End Select

    TranslateDocStatus = text
    
End Function

Public Function TranslateDocType(documentType As Long) As String

    Dim text As String
    
    Select Case documentType
        Case DocumentType_SalesOrder
            text = "Sales Order"
        Case DocumentType_SalesInvoice
            text = "Sales Invoice"
        Case DocumentType_PurchaseOrder
            text = "Purchase Order"
        Case DocumentType_PurchaseInvoice
            text = "Purchase Invoice"
        Case DocumentType_ReturnOrder
            text = "Return Order"
        Case DocumentType_ReturnInvoice
            text = "Return Invoice"
    End Select

    TranslateDocType = text
    
End Function

Public Function TranslateTaxType(taxType As Long) As String

    Dim text As String
    
    Select Case taxType
        Case TaxType_Sales
            text = "Sales"
        Case TaxType_Use
            text = "Use"
    End Select

    TranslateTaxType = text
    
End Function

Public Function TranslateJurisdictionType(jurisdictionType As Long) As String

    Dim text As String
    
    Select Case jurisdictionType
        Case JurisdictionType_Composite
            text = "Composite"
        Case JurisdictionType_State
            text = "State"
        Case JurisdictionType_County
            text = "County"
        Case JurisdictionType_City
            text = "City"
        Case JurisdictionType_Special
            text = "Special"
    End Select
    
    TranslateJurisdictionType = text
    
End Function

Public Function TranslateBoundaryLevel(boundaryLevel As Long) As String

    Dim text As String
    
    Select Case boundaryLevel
        Case BoundaryLevel_Address
            text = "Address"
        Case BoundaryLevel_Zip9
            text = "Zip 9"
        Case BoundaryLevel_Zip5
            text = "Zip 5"
    End Select
    
    TranslateBoundaryLevel = text
    
End Function

'Added for 5.0.2
Public Sub FillTaxOverrideTypeCombo(oCombo As ComboBox)
    
    oCombo.AddItem TaxOverrideType_None & " : None"
    oCombo.AddItem TaxOverrideType_TaxAmount & " : Tax Amount"
    oCombo.AddItem TaxOverrideType_Exemption & " : Exemption"
    oCombo.AddItem TaxOverrideType_TaxDate & " : Tax Date"
    
End Sub

'Added for 5.0.2
Public Sub FillServiceModeCombo(oCombo As ComboBox)
    
    oCombo.AddItem TaxOverrideType_None & " : None"
    oCombo.AddItem TaxOverrideType_TaxAmount & " : Tax Amount"
    oCombo.AddItem TaxOverrideType_Exemption & " : Exemption"
    oCombo.AddItem TaxOverrideType_TaxDate & " : Tax Date"
    
End Sub

Public Function TranslateTaxOverrideType(strTaxOverrideType As String) As Integer
    
    Dim oTaxOverrideType As Integer
   
    oTaxOverrideType = TaxOverrideType_None
  
    Select Case (strTaxOverrideType)
    
        Case "None": oTaxOverrideType = TaxOverrideType_None
          
        Case "TaxAmount": oTaxOverrideType = TaxOverrideType_TaxAmount
           
        Case "Exemption": oTaxOverrideType = TaxOverrideType_Exemption
            
        Case "TaxDate" : oTaxOverrideType = TaxOverrideType_TaxDate

        Case "AccruedTaxAmount" : oTaxOverrideType = TaxOverrideType_AccruedTaxAmount
    End Select

    TranslateTaxOverrideType = oTaxOverrideType

End Function

Public Function TranslateTaxOverrideTypeToString(strTaxOverrideType As Integer) As String

    Dim oTaxOverrideType As String

    oTaxOverrideType = "None"

    Select Case (strTaxOverrideType)

        Case 0 : oTaxOverrideType = "None"

        Case 1 : oTaxOverrideType = "TaxAmount"

        Case 2 : oTaxOverrideType = "Exemption"

        Case 3 : oTaxOverrideType = "TaxDate"

        Case 4 : oTaxOverrideType = "AccruedTaxAmount"
    End Select

    TranslateTaxOverrideTypeToString = oTaxOverrideType

End Function
