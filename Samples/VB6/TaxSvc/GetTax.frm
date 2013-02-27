VERSION 5.00
Object = "{5E9E78A0-531B-11CF-91F6-C2863C385E30}#1.0#0"; "MSFLXGRD.OCX"
Begin VB.Form formGetTax 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "GetTax"
   ClientHeight    =   10005
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   12090
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   10005
   ScaleWidth      =   12090
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.TextBox textTaxDate 
      Height          =   330
      Left            =   1650
      TabIndex        =   48
      Top             =   5670
      Width           =   1965
   End
   Begin VB.TextBox textReason 
      Height          =   330
      Left            =   5730
      TabIndex        =   54
      Top             =   5670
      Width           =   1965
   End
   Begin VB.TextBox textCurrencyCode 
      Height          =   330
      Left            =   10020
      TabIndex        =   60
      Top             =   5640
      Width           =   1965
   End
   Begin VB.ComboBox cboTaxOverrideType 
      Height          =   315
      Left            =   5730
      Style           =   2  'Dropdown List
      TabIndex        =   53
      Top             =   5280
      Width           =   1995
   End
   Begin VB.TextBox textTaxAmount 
      Height          =   330
      Left            =   10020
      TabIndex        =   59
      Top             =   5280
      Width           =   1965
   End
   Begin VB.TextBox textLocationCode 
      Height          =   330
      Left            =   5730
      TabIndex        =   52
      Top             =   4920
      Width           =   1965
   End
   Begin VB.CheckBox chkCommit 
      Height          =   255
      Left            =   1650
      TabIndex        =   47
      Top             =   5280
      Width           =   375
   End
   Begin VB.TextBox textPurchaseOrderNo 
      Height          =   330
      Left            =   10020
      TabIndex        =   58
      Top             =   4860
      Width           =   1965
   End
   Begin VB.ComboBox cboDocumentType 
      Height          =   315
      Left            =   1650
      Style           =   2  'Dropdown List
      TabIndex        =   46
      Top             =   4890
      Width           =   1965
   End
   Begin VB.TextBox textCustomerUsageType 
      Height          =   330
      Left            =   10020
      TabIndex        =   56
      Top             =   4080
      Width           =   1965
   End
   Begin VB.CommandButton cmdCancel 
      Cancel          =   -1  'True
      Caption         =   "Close"
      Height          =   345
      Left            =   10740
      TabIndex        =   38
      Top             =   9480
      Width           =   1245
   End
   Begin VB.CommandButton buttonGetTax 
      Caption         =   "Get Tax"
      Height          =   345
      Left            =   9420
      TabIndex        =   37
      Top             =   9480
      Width           =   1245
   End
   Begin VB.CommandButton buttonAddLine 
      Caption         =   "Add Line"
      Height          =   315
      Left            =   10680
      TabIndex        =   63
      Top             =   6840
      Width           =   1275
   End
   Begin VB.CommandButton buttonEditLine 
      Caption         =   "Edit Line"
      Height          =   315
      Left            =   9330
      TabIndex        =   62
      Top             =   6840
      Width           =   1275
   End
   Begin MSFlexGridLib.MSFlexGrid gridLines 
      CausesValidation=   0   'False
      Height          =   1815
      Left            =   210
      TabIndex        =   61
      Top             =   7320
      Width           =   11745
      _ExtentX        =   20717
      _ExtentY        =   3201
      _Version        =   393216
      Cols            =   14
      FixedCols       =   0
      AllowBigSelection=   0   'False
      ScrollTrack     =   -1  'True
      SelectionMode   =   1
      AllowUserResizing=   1
   End
   Begin VB.TextBox textSalespersonCode 
      Height          =   330
      Left            =   10020
      TabIndex        =   57
      Top             =   4470
      Width           =   1965
   End
   Begin VB.TextBox textCustomerCode 
      Height          =   330
      Left            =   10020
      TabIndex        =   55
      Top             =   3690
      Width           =   1965
   End
   Begin VB.ComboBox cboDetailLevel 
      Height          =   315
      Left            =   5730
      Style           =   2  'Dropdown List
      TabIndex        =   51
      Top             =   4515
      Width           =   1995
   End
   Begin VB.TextBox textExemptionNo 
      Height          =   330
      Left            =   5730
      TabIndex        =   50
      Top             =   4110
      Width           =   1965
   End
   Begin VB.TextBox textDiscount 
      Height          =   330
      Left            =   5730
      TabIndex        =   49
      Top             =   3720
      Width           =   1965
   End
   Begin VB.TextBox textDocDate 
      Height          =   330
      Left            =   1650
      TabIndex        =   45
      Top             =   4500
      Width           =   1965
   End
   Begin VB.TextBox textDocCode 
      Height          =   330
      Left            =   1650
      TabIndex        =   44
      Top             =   4110
      Width           =   1965
   End
   Begin VB.TextBox textCompanyCode 
      Height          =   330
      Left            =   1650
      TabIndex        =   43
      Top             =   3720
      Width           =   1965
   End
   Begin VB.CommandButton buttonShipToAddress 
      Caption         =   "Edit"
      Height          =   285
      Left            =   7380
      TabIndex        =   42
      Top             =   90
      Width           =   1125
   End
   Begin VB.CommandButton buttonShipFromAddress 
      Caption         =   "Edit"
      Height          =   285
      Left            =   1440
      TabIndex        =   41
      Top             =   90
      Width           =   1125
   End
   Begin VB.Label lblTaxDate 
      Alignment       =   1  'Right Justify
      Caption         =   "Tax Date:"
      DataSource      =   " "
      Height          =   300
      Index           =   15
      Left            =   120
      TabIndex        =   73
      Top             =   5700
      Width           =   1485
   End
   Begin VB.Label lblReason 
      Alignment       =   1  'Right Justify
      Caption         =   "Reason:"
      Height          =   300
      Index           =   14
      Left            =   4200
      TabIndex        =   72
      Top             =   5685
      Width           =   1485
   End
   Begin VB.Label Label1 
      Alignment       =   1  'Right Justify
      Caption         =   "Currency Code:"
      Height          =   300
      Left            =   8190
      TabIndex        =   71
      Top             =   5655
      Width           =   1785
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "TaxOverride Type:"
      Height          =   300
      Index           =   13
      Left            =   4200
      TabIndex        =   70
      Top             =   5280
      Width           =   1485
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Tax Amount:"
      Height          =   300
      Index           =   12
      Left            =   8400
      TabIndex        =   69
      Top             =   5280
      Width           =   1485
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Location Code:"
      Height          =   300
      Index           =   11
      Left            =   4200
      TabIndex        =   68
      Top             =   4920
      Width           =   1485
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Commit:"
      Height          =   300
      Index           =   7
      Left            =   120
      TabIndex        =   67
      Top             =   5280
      Width           =   1485
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Purchase Order No:"
      Height          =   300
      Index           =   6
      Left            =   8490
      TabIndex        =   66
      Top             =   4875
      Width           =   1485
   End
   Begin VB.Label Label6 
      Alignment       =   1  'Right Justify
      Caption         =   "Customer Usage Type:"
      Height          =   300
      Left            =   8190
      TabIndex        =   65
      Top             =   4095
      Width           =   1785
   End
   Begin VB.Label lblStatus 
      ForeColor       =   &H00008000&
      Height          =   285
      Left            =   240
      TabIndex        =   64
      Top             =   9480
      Width           =   6105
   End
   Begin VB.Line Line2 
      X1              =   120
      X2              =   12000
      Y1              =   6600
      Y2              =   6600
   End
   Begin VB.Line Line1 
      X1              =   120
      X2              =   12000
      Y1              =   3510
      Y2              =   3510
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Salesperson Code:"
      Height          =   300
      Index           =   10
      Left            =   8490
      TabIndex        =   40
      Top             =   4485
      Width           =   1485
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Customer Code:"
      Height          =   300
      Index           =   9
      Left            =   8490
      TabIndex        =   39
      Top             =   3720
      Width           =   1485
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Detail Level:"
      Height          =   300
      Index           =   8
      Left            =   4200
      TabIndex        =   36
      Top             =   4530
      Width           =   1485
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Exemption No:"
      Height          =   300
      Index           =   5
      Left            =   4200
      TabIndex        =   35
      Top             =   4125
      Width           =   1485
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Discount:"
      Height          =   300
      Index           =   4
      Left            =   4200
      TabIndex        =   34
      Top             =   3735
      Width           =   1485
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Document Type:"
      Height          =   300
      Index           =   3
      Left            =   120
      TabIndex        =   33
      Top             =   4920
      Width           =   1485
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Document Date:"
      Height          =   300
      Index           =   2
      Left            =   120
      TabIndex        =   32
      Top             =   4530
      Width           =   1485
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Document Code:"
      Height          =   300
      Index           =   1
      Left            =   120
      TabIndex        =   31
      Top             =   4140
      Width           =   1485
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Company Code:"
      Height          =   300
      Index           =   0
      Left            =   120
      TabIndex        =   30
      Top             =   3750
      Width           =   1485
   End
   Begin VB.Label lblShipTo 
      ForeColor       =   &H00C00000&
      Height          =   300
      Index           =   7
      Left            =   7410
      TabIndex        =   29
      Top             =   2370
      Width           =   4335
   End
   Begin VB.Label lblShipTo 
      ForeColor       =   &H00C00000&
      Height          =   300
      Index           =   6
      Left            =   7410
      TabIndex        =   28
      Top             =   2070
      Width           =   4335
   End
   Begin VB.Label lblShipTo 
      ForeColor       =   &H00C00000&
      Height          =   300
      Index           =   5
      Left            =   7410
      TabIndex        =   27
      Top             =   1770
      Width           =   4335
   End
   Begin VB.Label lblShipTo 
      ForeColor       =   &H00C00000&
      Height          =   300
      Index           =   4
      Left            =   7410
      TabIndex        =   26
      Top             =   1470
      Width           =   4335
   End
   Begin VB.Label lblShipTo 
      ForeColor       =   &H00C00000&
      Height          =   300
      Index           =   3
      Left            =   7410
      TabIndex        =   25
      Top             =   1170
      Width           =   4335
   End
   Begin VB.Label lblShipTo 
      ForeColor       =   &H00C00000&
      Height          =   300
      Index           =   2
      Left            =   7410
      TabIndex        =   24
      Top             =   870
      Width           =   4335
   End
   Begin VB.Label lblShipTo 
      ForeColor       =   &H00C00000&
      Height          =   300
      Index           =   1
      Left            =   7410
      TabIndex        =   23
      Top             =   570
      Width           =   4335
   End
   Begin VB.Label lblShipFrom 
      ForeColor       =   &H00C00000&
      Height          =   300
      Index           =   7
      Left            =   1470
      TabIndex        =   22
      Top             =   2370
      Width           =   4335
   End
   Begin VB.Label lblShipFrom 
      ForeColor       =   &H00C00000&
      Height          =   300
      Index           =   6
      Left            =   1470
      TabIndex        =   21
      Top             =   2070
      Width           =   4335
   End
   Begin VB.Label lblShipFrom 
      ForeColor       =   &H00C00000&
      Height          =   300
      Index           =   5
      Left            =   1470
      TabIndex        =   20
      Top             =   1770
      Width           =   4335
   End
   Begin VB.Label lblShipFrom 
      ForeColor       =   &H00C00000&
      Height          =   300
      Index           =   4
      Left            =   1470
      TabIndex        =   19
      Top             =   1470
      Width           =   4335
   End
   Begin VB.Label lblShipFrom 
      ForeColor       =   &H00C00000&
      Height          =   300
      Index           =   3
      Left            =   1470
      TabIndex        =   18
      Top             =   1170
      Width           =   4335
   End
   Begin VB.Label lblShipFrom 
      ForeColor       =   &H00C00000&
      Height          =   300
      Index           =   2
      Left            =   1470
      TabIndex        =   17
      Top             =   870
      Width           =   4335
   End
   Begin VB.Label lblShipFrom 
      ForeColor       =   &H00C00000&
      Height          =   300
      Index           =   1
      Left            =   1470
      TabIndex        =   16
      Top             =   570
      Width           =   4335
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "Ship To:"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   -1  'True
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   300
      Index           =   17
      Left            =   6030
      TabIndex        =   15
      Top             =   120
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "Ship From:"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   -1  'True
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   300
      Index           =   16
      Left            =   90
      TabIndex        =   14
      Top             =   120
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "Line 1:"
      Height          =   300
      Index           =   14
      Left            =   6030
      TabIndex        =   13
      Top             =   555
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "Line 2:"
      Height          =   300
      Index           =   13
      Left            =   6030
      TabIndex        =   12
      Top             =   870
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "Line 3:"
      Height          =   300
      Index           =   12
      Left            =   6030
      TabIndex        =   11
      Top             =   1170
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "City:"
      Height          =   300
      Index           =   11
      Left            =   6030
      TabIndex        =   10
      Top             =   1455
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "State:"
      Height          =   300
      Index           =   10
      Left            =   6030
      TabIndex        =   9
      Top             =   1770
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "Zip:"
      Height          =   300
      Index           =   9
      Left            =   6030
      TabIndex        =   8
      Top             =   2055
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "Country:"
      Height          =   300
      Index           =   8
      Left            =   6030
      TabIndex        =   7
      Top             =   2370
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "Line 1:"
      Height          =   300
      Index           =   1
      Left            =   90
      TabIndex        =   6
      Top             =   555
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "Line 2:"
      Height          =   300
      Index           =   2
      Left            =   90
      TabIndex        =   5
      Top             =   870
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "Line 3:"
      Height          =   300
      Index           =   3
      Left            =   90
      TabIndex        =   4
      Top             =   1170
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "City:"
      Height          =   300
      Index           =   4
      Left            =   90
      TabIndex        =   3
      Top             =   1455
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "State:"
      Height          =   300
      Index           =   5
      Left            =   90
      TabIndex        =   2
      Top             =   1770
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "Zip:"
      Height          =   300
      Index           =   6
      Left            =   90
      TabIndex        =   1
      Top             =   2055
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "Country:"
      Height          =   300
      Index           =   7
      Left            =   90
      TabIndex        =   0
      Top             =   2370
      Width           =   1275
   End
End
Attribute VB_Name = "formGetTax"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Public addLine As Boolean

Private Enum AdjustLineColumns
    colLine_No = 0
    colLine_ItemCode
    colLine_Qty
    colLine_Amount
    colLine_Discounted
    'colLine_Discount
    colLine_ExemptionNo
    colLine_Reference1
    colLine_Reference2
    colLine_RevAcct
    colLine_TaxCode
    'Added for 5.0
'    colLine_IsTaxOverriden
'    colLine_TaxOverride
    'Added for 5.0.2
    colLine_TaxOverrideType
    colLine_TaxAmount
    colLine_TaxDate
    colLine_Reason
    
End Enum

Private Sub buttonAddLine_Click()
    addLine = True
    
    gridLines.Rows = gridLines.Rows + 1
    gridLines.Row = gridLines.Rows - 1
        
    buttonEditLine_Click
    
    If (formGetTax.addLine = False) Then
        gridLines.RemoveItem gridLines.Rows
        'gridLines.Rows = gridLines.Rows
        'gridLines.Row = gridLines.Rows - 1
    End If
    
End Sub

Private Sub buttonEditLine_Click()
        
    Dim oLine As Avalara_AvaTax_Adapter.Line
    
    If (gridLines.Row > 0 And gridLines.Row < gridLines.Rows) Then
        Set oLine = New Avalara_AvaTax_Adapter.Line
        
        LoadLineItem oLine, gridLines.Row
        
        Dim frm As formLine
        Set frm = New formLine
        Set frm.oLine = oLine
        
        frm.Show vbModal, Me
    
        With gridLines
            .TextMatrix(.Row, colLine_No) = oLine.No
            .TextMatrix(.Row, colLine_ItemCode) = oLine.ItemCode
            .TextMatrix(.Row, colLine_Qty) = oLine.Qty
            .TextMatrix(.Row, colLine_Amount) = oLine.Amount
            .TextMatrix(.Row, colLine_Discounted) = IIf(oLine.Discounted = True, "True", "False")
            '.TextMatrix(.Row, colLine_Discount) = oLine.Discount
            .TextMatrix(.Row, colLine_ExemptionNo) = oLine.ExemptionNo
            .TextMatrix(.Row, colLine_Reference1) = oLine.Ref1
            .TextMatrix(.Row, colLine_Reference2) = oLine.Ref2
            .TextMatrix(.Row, colLine_RevAcct) = oLine.RevAcct
            .TextMatrix(.Row, colLine_TaxCode) = oLine.TaxCode
            'Added for 5.0
'            .TextMatrix(.Row, colLine_IsTaxOverriden) = IIf(oLine.IsTaxOverriden = True, "True", "False")
'            .TextMatrix(.Row, colLine_TaxOverride) = oLine.TaxOverride
            'Added for 5.0.2
            .TextMatrix(.Row, colLine_TaxOverrideType) = TranslateTaxOverrideTypeToString(oLine.TaxOverride.TaxOverrideType)
            .TextMatrix(.Row, colLine_TaxAmount) = oLine.TaxOverride.TaxAmount
            .TextMatrix(.Row, colLine_TaxDate) = oLine.TaxOverride.TaxDate
            .TextMatrix(.Row, colLine_Reason) = oLine.TaxOverride.Reason
            
        End With
    Else
        MsgBox "No row selected", vbInformation
    End If

    
End Sub

Private Sub buttonGetTax_Click()

    Dim oGetTaxRequest As GetTaxRequest
    
    On Error GoTo errGetTax
    
    If (IsValidDateTime(textDocDate.text) = False) Then
        MsgBox "Invalid date/time value.", vbOKOnly Or vbExclamation, "Invalid Field"
        textDocDate.SetFocus
        Exit Sub
    End If
  
    '#################################################################################
    '### 1st WE CREATE THE REQUEST OBJECT FOR THE ORDER THAT NEEDS TAX CALCULATION ###
    '#################################################################################
    Set oGetTaxRequest = New GetTaxRequest
    
    '##############################################################################
    '### 2nd WE HAVE TO LOAD ADDRESS INFORMATION FOR THE ORDER INTO THE REQUEST ###
    '##############################################################################
    Dim oAddress As Address
    
    Set oAddress = New Address              'Ship From
    LoadShipFromAddress oAddress
    Set oGetTaxRequest.OriginAddress = oAddress
    
    Set oAddress = New Address              'Ship To
    LoadShipToAddress oAddress
    Set oGetTaxRequest.DestinationAddress = oAddress
    
    Set oAddress = Nothing
    
    '##################################################################
    '### 3rd WE LOAD THE LINE(S) WITH ITEM DATA INTO THE REQUEST    ###
    '### *we are not loading addresses for the individual lines     ###
    '### *they will default to the addresses on the GetTaxRequest   ###
    '##################################################################
    Dim oLine As Avalara_AvaTax_Adapter.Line
    Dim lIndex As Long
    
    For lIndex = 1 To gridLines.Rows - 1
        Set oLine = New Avalara_AvaTax_Adapter.Line
        LoadLineItem oLine, lIndex
        oGetTaxRequest.Lines.Add oLine
    Next lIndex
    Set oLine = Nothing
    
    '###########################################################
    '### 4th WE LOAD THE REQUEST-LEVEL DATA INTO THE REQUEST ###
    '###########################################################
    oGetTaxRequest.CompanyCode = textCompanyCode.text
    oGetTaxRequest.DocCode = textDocCode.text
    oGetTaxRequest.DocDate = CDate(textDocDate.text)
    oGetTaxRequest.DocType = Val(cboDocumentType.List(cboDocumentType.ListIndex))
    oGetTaxRequest.Discount = textDiscount.text
    oGetTaxRequest.ExemptionNo = textExemptionNo.text
    oGetTaxRequest.DetailLevel = Val(cboDetailLevel.List(cboDetailLevel.ListIndex))
    oGetTaxRequest.CustomerCode = textCustomerCode.text
    oGetTaxRequest.CustomerUsageType = textCustomerUsageType.text
    oGetTaxRequest.SalespersonCode = textSalespersonCode.text
    oGetTaxRequest.PurchaseOrderNo = textPurchaseOrderNo.text
    'Added new fields from 4.16
    oGetTaxRequest.LocationCode = textLocationCode.text
    oGetTaxRequest.Commit = (chkCommit.Value = vbChecked)
    'Added for 5.0
'    oGetTaxRequest.IsTotalTaxOverriden = (chkIsTaxOverriden.Value = vbChecked)
'    oGetTaxRequest.TotalTaxOverride = Val(textTaxOverride.text)
    'Added for 5.0.2
    oGetTaxRequest.TaxOverride.TaxOverrideType = Val(cboTaxOverrideType.List(cboTaxOverrideType.ListIndex))
    oGetTaxRequest.TaxOverride.TaxAmount = Val(textTaxAmount.text)
    
    If IsDate(textTaxDate.text) Then
        oGetTaxRequest.TaxOverride.TaxDate = CDate(textTaxDate.text)
    End If
        
    oGetTaxRequest.TaxOverride.Reason = textReason.text
    oGetTaxRequest.CurrencyCode = textCurrencyCode.text
    'oGetTaxRequest.ServiceMode = Val(cboServiceMode.List(cboServiceMode.ListIndex))
    

    '###########################################################################################
    '### 5th WE INVOKE THE GETTAX() METHOD OF THE TAXSVC OBJECT AND GET BACK A RESULT OBJECT ###
    '###########################################################################################
    Dim oTaxSvc As TaxSvc
    Dim oGetTaxResult As GetTaxResult
    
    PreMethodCall lblStatus
    
    Set oTaxSvc = New TaxSvc
    formMain.SetConfig oTaxSvc              'set the Url and Security configuration
    
    Set oGetTaxResult = oTaxSvc.GetTax(oGetTaxRequest)
    
    PostMethodCall lblStatus
    
    '#######################################
    '### 6th WE REVIEW THE RESULT OBJECT ###
    '#######################################
    Dim frm As formGetTaxResult
    Set frm = New formGetTaxResult
    Set frm.oGetTaxResult = oGetTaxResult
    
    frm.Show vbModal, Me
    
    Set oGetTaxResult = Nothing
    Set oGetTaxRequest = Nothing
    Set oTaxSvc = Nothing
    
    Exit Sub
    
errGetTax:
    lblStatus.Caption = ""
    Screen.MousePointer = vbDefault
    ShowError Err
End Sub

Private Sub buttonShipFromAddress_Click()

    Dim oAddress As Address
    Set oAddress = New Address
    
    LoadShipFromAddress oAddress
    
    Dim frm As formAddress
    Set frm = New formAddress
    Set frm.oAddress = oAddress
    frm.Show vbModal, Me
    
    lblShipFrom(1).Caption = oAddress.Line1
    lblShipFrom(2).Caption = oAddress.Line2
    lblShipFrom(3).Caption = oAddress.Line3
    lblShipFrom(4).Caption = oAddress.City
    lblShipFrom(5).Caption = oAddress.Region
    lblShipFrom(6).Caption = oAddress.PostalCode
    lblShipFrom(7).Caption = oAddress.Country

End Sub

Private Sub buttonShipToAddress_Click()

    Dim oAddress As Address
    Set oAddress = New Address
    
    LoadShipToAddress oAddress
    
    Dim frm As formAddress
    Set frm = New formAddress
    Set frm.oAddress = oAddress
    frm.Show vbModal, Me
    
    lblShipTo(1).Caption = oAddress.Line1
    lblShipTo(2).Caption = oAddress.Line2
    lblShipTo(3).Caption = oAddress.Line3
    lblShipTo(4).Caption = oAddress.City
    lblShipTo(5).Caption = oAddress.Region
    lblShipTo(6).Caption = oAddress.PostalCode
    lblShipTo(7).Caption = oAddress.Country

End Sub


Private Sub cboDocumentType_Click()
    If (cboDocumentType.ListIndex = 0 Or cboDocumentType.ListIndex = 2 Or cboDocumentType.ListIndex = 4) Then
        chkCommit.Enabled = False
    Else
        chkCommit.Enabled = True
    End If

End Sub


Private Sub cboDocumentType_LostFocus()

    If (cboDocumentType.ListIndex = 0) Then
        chkCommit.Enabled = False
    Else
        chkCommit.Enabled = True
    End If

End Sub

Private Sub cmdCancel_Click()
    Unload Me
End Sub


Private Sub Form_Load()

    '#####################
    '### UI SETUP CODE ###
    '#####################
    FillDetailCombo
    FillDocumentTypeCombo
    FillTaxOverrideTypeCombo cboTaxOverrideType
    'FillServiceModeCombo cboServiceMode
    FillLineHeader
    FillLineData
    
    '### SAMPLE DATA
    lblShipFrom(1).Caption = "435 Eriksen Ave NE"
    lblShipFrom(2).Caption = ""
    lblShipFrom(3).Caption = ""
    lblShipFrom(4).Caption = "Bainbridge Island"
    lblShipFrom(5).Caption = "WA"
    lblShipFrom(6).Caption = "98110"
    lblShipFrom(7).Caption = "US"

    lblShipTo(1).Caption = "435 Eriksen Ave NE"
    lblShipTo(2).Caption = ""
    lblShipTo(3).Caption = ""
    lblShipTo(4).Caption = "Bainbridge Island"
    lblShipTo(5).Caption = "WA"
    lblShipTo(6).Caption = "98110"
    lblShipTo(7).Caption = "US"

    
    textCompanyCode.text = "DEFAULT"
    textDocCode.text = "DOC0001"
    textDocDate.text = Format$(Date, "m/d/yy")
    cboDocumentType.ListIndex = 1
    textDiscount.text = "0.00"
    textCustomerCode.text = "CUST1"
    cboDetailLevel.ListIndex = 3
    'Added for 5.0.2
    cboTaxOverrideType.ListIndex = 0
    textTaxAmount.text = ""
    textTaxDate.text = Format$(Date, "m/d/yy")
    textReason.text = ""
    textCurrencyCode.text = ""
    'cboServiceMode.ListIndex = 0
    
End Sub

Private Sub FillDetailCombo()
    '#####################
    '### UI SETUP CODE ###
    '#####################
    cboDetailLevel.AddItem DetailLevel.DetailLevel_Summary & " : Summary"
    cboDetailLevel.AddItem DetailLevel.DetailLevel_Document & " : Document"
    cboDetailLevel.AddItem DetailLevel.DetailLevel_Line & " : Line"
    cboDetailLevel.AddItem DetailLevel.DetailLevel_Tax & " : Tax"
End Sub

Private Sub FillDocumentTypeCombo()
    '#####################
    '### UI SETUP CODE ###
    '#####################
    
    'FillDocTypeCombo cboDocumentType
    cboDocumentType.AddItem DocumentType_SalesOrder & " : Sales Order"
    cboDocumentType.AddItem DocumentType_SalesInvoice & " : Sales Invoice"
    cboDocumentType.AddItem DocumentType_PurchaseOrder & " : Purchase Order"
    cboDocumentType.AddItem DocumentType_PurchaseInvoice & " : Purchase Invoice"
    cboDocumentType.AddItem DocumentType_ReturnOrder & " : Return Order"
    cboDocumentType.AddItem DocumentType_ReturnInvoice & " : Return Invoice"
End Sub

Private Sub FillLineHeader()
    '#####################
    '### UI SETUP CODE ###
    '#####################
    With gridLines
        .TextMatrix(0, colLine_No) = "No"
        .TextMatrix(0, colLine_ItemCode) = "Item Code"
        .TextMatrix(0, colLine_Qty) = "Qty"
        .TextMatrix(0, colLine_Amount) = "Amount"
        .TextMatrix(0, colLine_Discounted) = "Discounted"
        '.TextMatrix(0, colLine_Discount) = "Discount"
        .TextMatrix(0, colLine_ExemptionNo) = "Exemption No"
        .TextMatrix(0, colLine_Reference1) = "Reference 1"
        .TextMatrix(0, colLine_Reference2) = "Reference 2"
        .TextMatrix(0, colLine_RevAcct) = "Revenue Acct"
        .TextMatrix(0, colLine_TaxCode) = "Tax Code"
        'Added for 5.0
'        .TextMatrix(0, colLine_IsTaxOverriden) = "IsTaxOverriden"
'        .TextMatrix(0, colLine_TaxOverride) = "Tax Override"
        'Added for 5.0.2
        .TextMatrix(0, colLine_TaxOverrideType) = "TaxOverride Type"
        .TextMatrix(0, colLine_TaxAmount) = "Tax Amount"
        .TextMatrix(0, colLine_TaxDate) = "Tax Date"
        .TextMatrix(0, colLine_Reason) = "Reason"
    
        .ColWidth(colLine_ItemCode) = .ColWidth(colLine_ItemCode) + (5 * Screen.TwipsPerPixelX)
        .ColWidth(colLine_ExemptionNo) = .ColWidth(colLine_ExemptionNo) + (10 * Screen.TwipsPerPixelX)
        .ColWidth(colLine_Reference1) = .ColWidth(colLine_Reference1) + (10 * Screen.TwipsPerPixelX)
        .ColWidth(colLine_Reference2) = .ColWidth(colLine_Reference2) + (10 * Screen.TwipsPerPixelX)
        .ColWidth(colLine_RevAcct) = .ColWidth(colLine_RevAcct) + (14 * Screen.TwipsPerPixelX)
        .ColWidth(colLine_TaxCode) = .ColWidth(colLine_TaxCode) + (15 * Screen.TwipsPerPixelX)
        'Added for 5.0
'        .ColWidth(colLine_IsTaxOverriden) = .ColWidth(colLine_IsTaxOverriden) + (15 * Screen.TwipsPerPixelX)
'        .ColWidth(colLine_TaxOverride) = .ColWidth(colLine_TaxOverride) + (15 * Screen.TwipsPerPixelX)
        'Added for 5.0.2
        .ColWidth(colLine_TaxOverrideType) = .ColWidth(colLine_TaxOverrideType) + (15 * Screen.TwipsPerPixelX)
        .ColWidth(colLine_TaxAmount) = .ColWidth(colLine_TaxAmount) + (15 * Screen.TwipsPerPixelX)
        .ColWidth(colLine_TaxDate) = .ColWidth(colLine_TaxDate) + (15 * Screen.TwipsPerPixelX)
        .ColWidth(colLine_Reason) = .ColWidth(colLine_Reason) + (15 * Screen.TwipsPerPixelX)
    End With
    
End Sub

Private Sub FillLineData()

    '## LINE ITEM 1
    With gridLines
        .TextMatrix(1, colLine_No) = "1"
        .TextMatrix(1, colLine_ItemCode) = "SAMPLE01"
        .TextMatrix(1, colLine_Qty) = "1"
        .TextMatrix(1, colLine_Amount) = "10.00"
        .TextMatrix(1, colLine_Discounted) = "False"
        '.TextMatrix(1, colLine_Discount) = "0"
        .TextMatrix(1, colLine_ExemptionNo) = ""
        .TextMatrix(1, colLine_Reference1) = ""
        .TextMatrix(1, colLine_Reference2) = ""
        .TextMatrix(1, colLine_RevAcct) = ""
        .TextMatrix(1, colLine_TaxCode) = "TAXCODE01"
        'Added for 5.0
'        .TextMatrix(1, colLine_IsTaxOverriden) = "False"
'        .TextMatrix(1, colLine_TaxOverride) = ""
        'Added for 5.0.2
        .TextMatrix(1, colLine_TaxOverrideType) = "None"
        .TextMatrix(1, colLine_TaxAmount) = "0"
        .TextMatrix(1, colLine_TaxDate) = "" 'dateTime.Date
        .TextMatrix(1, colLine_Reason) = ""
    End With

    '## LINE ITEM 2
    With gridLines
        .Rows = .Rows + 1
        .TextMatrix(2, colLine_No) = "2"
        .TextMatrix(2, colLine_ItemCode) = "SAMPLE02"
        .TextMatrix(2, colLine_Qty) = "2"
        .TextMatrix(2, colLine_Amount) = "40.00"
        .TextMatrix(2, colLine_Discounted) = "False"
        '.TextMatrix(1, colLine_Discount) = "0"
        .TextMatrix(2, colLine_ExemptionNo) = ""
        .TextMatrix(2, colLine_Reference1) = ""
        .TextMatrix(2, colLine_Reference2) = ""
        .TextMatrix(2, colLine_RevAcct) = ""
        .TextMatrix(2, colLine_TaxCode) = "TAXCODE02"
        'Added for 5.0
'        .TextMatrix(1, colLine_IsTaxOverriden) = "False"
'        .TextMatrix(1, colLine_TaxOverride) = ""
        'Added for 5.0.2
        .TextMatrix(2, colLine_TaxOverrideType) = "None"
        .TextMatrix(2, colLine_TaxAmount) = "0"
        .TextMatrix(2, colLine_TaxDate) = "" 'dateTime.Date
        .TextMatrix(2, colLine_Reason) = ""
    End With

End Sub

Private Sub LoadShipFromAddress(ByRef oAddress As Address)
    oAddress.Line1 = lblShipFrom(1).Caption
    oAddress.Line2 = lblShipFrom(2).Caption
    oAddress.Line3 = lblShipFrom(3).Caption
    oAddress.City = lblShipFrom(4).Caption
    oAddress.Region = lblShipFrom(5).Caption
    oAddress.PostalCode = lblShipFrom(6).Caption
    oAddress.Country = lblShipFrom(7).Caption
End Sub

Private Sub LoadShipToAddress(ByRef oAddress As Address)
    oAddress.Line1 = lblShipTo(1).Caption
    oAddress.Line2 = lblShipTo(2).Caption
    oAddress.Line3 = lblShipTo(3).Caption
    oAddress.City = lblShipTo(4).Caption
    oAddress.Region = lblShipTo(5).Caption
    oAddress.PostalCode = lblShipTo(6).Caption
    oAddress.Country = lblShipTo(7).Caption
End Sub

Private Sub LoadLineItem(ByRef oLine As Avalara_AvaTax_Adapter.Line, lRow As Long)
    With gridLines
        oLine.No = .TextMatrix(lRow, colLine_No)
        oLine.ItemCode = .TextMatrix(lRow, colLine_ItemCode)
        oLine.Qty = Val(.TextMatrix(lRow, colLine_Qty))
        oLine.Amount = Val(.TextMatrix(lRow, colLine_Amount))
        oLine.Discounted = IIf(.TextMatrix(lRow, colLine_Discounted) = "True", True, False)
        'oLine.Discount = Val(.TextMatrix(lRow, colLine_Discount))
        oLine.ExemptionNo = .TextMatrix(lRow, colLine_ExemptionNo)
        oLine.Ref1 = .TextMatrix(lRow, colLine_Reference1)
        oLine.Ref2 = .TextMatrix(lRow, colLine_Reference2)
        oLine.RevAcct = .TextMatrix(lRow, colLine_RevAcct)
        oLine.TaxCode = .TextMatrix(lRow, colLine_TaxCode)
        'Added for 5.0
'        oLine.IsTaxOverriden = IIf(.TextMatrix(lRow, colLine_IsTaxOverriden) = "True", True, False)
'        oLine.TaxOverride = Val(.TextMatrix(lRow, colLine_TaxOverride))
        'Added for 5.0.2
        oLine.TaxOverride.TaxOverrideType = TranslateTaxOverrideType(.TextMatrix(lRow, colLine_TaxOverrideType))
        oLine.TaxOverride.TaxAmount = Val(.TextMatrix(lRow, colLine_TaxAmount))
        
        If (IsDate(.TextMatrix(lRow, colLine_TaxDate))) Then
            oLine.TaxOverride.TaxDate = CDate(.TextMatrix(lRow, colLine_TaxDate))
        End If
        oLine.TaxOverride.Reason = .TextMatrix(lRow, colLine_Reason)
    End With
End Sub

