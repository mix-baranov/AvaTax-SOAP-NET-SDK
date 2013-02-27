VERSION 5.00
Object = "{5E9E78A0-531B-11CF-91F6-C2863C385E30}#1.0#0"; "MSFLXGRD.OCX"
Begin VB.Form formGetTaxRequest 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "GetTax Request"
   ClientHeight    =   8295
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   11880
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   8295
   ScaleWidth      =   11880
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.CommandButton buttonClose 
      Cancel          =   -1  'True
      Caption         =   "Close"
      Height          =   345
      Left            =   10590
      TabIndex        =   20
      Top             =   7590
      Width           =   1125
   End
   Begin MSFlexGridLib.MSFlexGrid gridLines 
      CausesValidation=   0   'False
      Height          =   1815
      Left            =   180
      TabIndex        =   0
      Top             =   5610
      Width           =   11505
      _ExtentX        =   20294
      _ExtentY        =   3201
      _Version        =   393216
      Cols            =   14
      FixedCols       =   0
      AllowBigSelection=   0   'False
      ScrollTrack     =   -1  'True
      SelectionMode   =   1
      AllowUserResizing=   1
   End
   Begin MSFlexGridLib.MSFlexGrid gridAddresses 
      CausesValidation=   0   'False
      Height          =   1815
      Left            =   180
      TabIndex        =   21
      Top             =   360
      Width           =   11505
      _ExtentX        =   20294
      _ExtentY        =   3201
      _Version        =   393216
      Cols            =   10
      FixedCols       =   0
      AllowBigSelection=   0   'False
      ScrollTrack     =   -1  'True
      SelectionMode   =   1
      AllowUserResizing=   1
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Tax Override Date:"
      Height          =   300
      Index           =   16
      Left            =   8220
      TabIndex        =   38
      Top             =   4320
      Width           =   1575
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Tax Override Reason:"
      Height          =   300
      Index           =   15
      Left            =   8040
      TabIndex        =   37
      Top             =   3495
      Width           =   1725
   End
   Begin VB.Label lblReason 
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   9840
      TabIndex        =   36
      Top             =   3510
      Width           =   1905
   End
   Begin VB.Label lblTaxDate 
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   9870
      TabIndex        =   35
      Top             =   4320
      Width           =   1875
   End
   Begin VB.Label lblCurrencyCode 
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   5520
      TabIndex        =   34
      Top             =   4710
      Width           =   1995
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Currency Code:"
      Height          =   300
      Index           =   14
      Left            =   3990
      TabIndex        =   33
      Top             =   4710
      Width           =   1455
   End
   Begin VB.Label lblPurchaseOrderNo 
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   5520
      TabIndex        =   32
      Top             =   3960
      Width           =   2115
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Purchase Order No:"
      Height          =   300
      Index           =   13
      Left            =   3960
      TabIndex        =   31
      Top             =   3960
      Width           =   1455
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Tax Override Type:"
      Height          =   300
      Index           =   12
      Left            =   8280
      TabIndex        =   30
      Top             =   3120
      Width           =   1485
   End
   Begin VB.Label lblTaxOverrideType 
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   9840
      TabIndex        =   29
      Top             =   3120
      Width           =   2025
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Tax Override Amount:"
      Height          =   300
      Index           =   7
      Left            =   7920
      TabIndex        =   28
      Top             =   3960
      Width           =   1845
   End
   Begin VB.Label lblTaxAmount 
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   9840
      TabIndex        =   27
      Top             =   3960
      Width           =   1905
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Location Code:"
      Height          =   300
      Index           =   6
      Left            =   120
      TabIndex        =   26
      Top             =   4710
      Width           =   1455
   End
   Begin VB.Label lblLocationCode 
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   1560
      TabIndex        =   25
      Top             =   4710
      Width           =   1995
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Customer/Use Type:"
      Height          =   300
      Index           =   11
      Left            =   3750
      TabIndex        =   24
      Top             =   3090
      Width           =   1695
   End
   Begin VB.Label lblCustomerUsageType 
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   5520
      TabIndex        =   23
      Top             =   3090
      Width           =   2115
   End
   Begin VB.Label lblAddresses 
      Caption         =   "Addresses"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   300
      Index           =   0
      Left            =   210
      TabIndex        =   22
      Top             =   120
      Width           =   1275
   End
   Begin VB.Label lblLines 
      Caption         =   "Line Items"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   300
      Left            =   210
      TabIndex        =   19
      Top             =   5370
      Width           =   1275
   End
   Begin VB.Label lblSalespersonCode 
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   5520
      TabIndex        =   18
      Top             =   4320
      Width           =   2115
   End
   Begin VB.Label lblCustomerCode 
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   5520
      TabIndex        =   17
      Top             =   2700
      Width           =   2115
   End
   Begin VB.Label lblDetailLevel 
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   1620
      TabIndex        =   16
      Top             =   4320
      Width           =   1935
   End
   Begin VB.Label lblExemptionNo 
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   5520
      TabIndex        =   15
      Top             =   3570
      Width           =   1935
   End
   Begin VB.Label lblDiscount 
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   9780
      TabIndex        =   14
      Top             =   2700
      Width           =   1935
   End
   Begin VB.Label lblDocType 
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   1650
      TabIndex        =   13
      Top             =   3870
      Width           =   1845
   End
   Begin VB.Label lblDocDate 
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   1650
      TabIndex        =   12
      Top             =   3480
      Width           =   1905
   End
   Begin VB.Label lblDocCode 
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   1650
      TabIndex        =   11
      Top             =   3090
      Width           =   1905
   End
   Begin VB.Label lblCompanyCode 
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   1650
      TabIndex        =   10
      Top             =   2700
      Width           =   1905
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Company Code:"
      Height          =   300
      Index           =   0
      Left            =   90
      TabIndex        =   9
      Top             =   2700
      Width           =   1485
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Document Code:"
      Height          =   300
      Index           =   1
      Left            =   90
      TabIndex        =   8
      Top             =   3090
      Width           =   1485
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Document Date:"
      Height          =   300
      Index           =   2
      Left            =   90
      TabIndex        =   7
      Top             =   3480
      Width           =   1485
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Document Type:"
      Height          =   300
      Index           =   3
      Left            =   90
      TabIndex        =   6
      Top             =   3855
      Width           =   1485
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Discount:"
      Height          =   300
      Index           =   4
      Left            =   8220
      TabIndex        =   5
      Top             =   2685
      Width           =   1485
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Exemption No:"
      Height          =   300
      Index           =   5
      Left            =   4020
      TabIndex        =   4
      Top             =   3555
      Width           =   1485
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Detail Level:"
      Height          =   300
      Index           =   8
      Left            =   60
      TabIndex        =   3
      Top             =   4320
      Width           =   1485
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Customer Code:"
      Height          =   300
      Index           =   9
      Left            =   3990
      TabIndex        =   2
      Top             =   2700
      Width           =   1455
   End
   Begin VB.Label lblGetTaxRequest 
      Alignment       =   1  'Right Justify
      Caption         =   "Sales Person Code:"
      Height          =   300
      Index           =   10
      Left            =   3630
      TabIndex        =   1
      Top             =   4320
      Width           =   1815
   End
   Begin VB.Line Line1 
      X1              =   90
      X2              =   11670
      Y1              =   2490
      Y2              =   2490
   End
   Begin VB.Line Line2 
      X1              =   90
      X2              =   11670
      Y1              =   5160
      Y2              =   5160
   End
End
Attribute VB_Name = "formGetTaxRequest"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Enum AddressColumns
    colAddress_Parent = 0
    colAddress_Type
    colAddress_Line1
    colAddress_Line2
    colAddress_Line3
    colAddress_City
    colAddress_Region
    colAddress_PostalCode
    colAddress_Country
    'Added for 5.0
    colAddress_TaxRegionId
End Enum

Private Enum LineColumns
    colLine_No = 0
    colLine_ItemCode
    colLine_Qty
    colLine_Amount
    colLine_Discounted
    colLine_ExemptionNo
    colLine_Reference1
    colLine_Reference2
    colLine_RevAcct
    colLine_TaxCode
    'Added for 5.0
'    colLine_IsTaxOverriden
'    colLine_TaxOverride
    'Added for 5.0
    colLine_TaxOverrideType
    colLine_TaxAmount
    colLine_TaxDate
    colLine_Reason
End Enum

Public oGetTaxRequest As GetTaxRequest

Private Sub cmdCancel_Click()
    Unload Me
End Sub

Private Sub buttonClose_Click()

    Set oGetTaxRequest = Nothing
    Unload Me
    
End Sub

Private Sub Form_Load()

    FillAddressHeader
    FillLineHeader
    FillRequestData
    FillAddressGrid
    FillLineGrid
    
End Sub

Private Sub FillRequestData()

    '################################################
    '### RETRIEVE THE DATA FROM THE SAVED REQUEST ###
    '################################################
    lblCompanyCode.Caption = oGetTaxRequest.CompanyCode
    lblDocCode.Caption = oGetTaxRequest.DocCode
    lblDocDate.Caption = oGetTaxRequest.DocDate
    lblDocType.Caption = oGetTaxRequest.DocType
    lblDiscount.Caption = oGetTaxRequest.Discount
    lblExemptionNo.Caption = oGetTaxRequest.ExemptionNo
    Select Case oGetTaxRequest.DetailLevel
        Case DetailLevel_Summary
            lblDetailLevel.Caption = "Summary"
        Case DetailLevel_Document
            lblDetailLevel.Caption = "Document"
        Case DetailLevel_Line
            lblDetailLevel.Caption = "Line"
        Case DetailLevel_Tax
            lblDetailLevel.Caption = "Tax"
    End Select
    lblCustomerCode.Caption = oGetTaxRequest.CustomerCode
    lblCustomerUsageType.Caption = oGetTaxRequest.CustomerUsageType
    lblSalespersonCode.Caption = oGetTaxRequest.SalespersonCode
    lblPurchaseOrderNo.Caption = oGetTaxRequest.PurchaseOrderNo
    
    'Added for 5.0
'    lblIsTaxOverriden.Caption = oGetTaxRequest.IsTotalTaxOverriden
'    lblTaxOverride.Caption = oGetTaxRequest.TotalTaxOverride
    'Added for 5.0.2
    lblTaxOverrideType.Caption = TranslateTaxOverrideTypeToString(oGetTaxRequest.TaxOverride.TaxOverrideType)
    lblTaxAmount.Caption = oGetTaxRequest.TaxOverride.TaxAmount
    lblTaxDate.Caption = oGetTaxRequest.TaxOverride.TaxDate
    lblReason.Caption = oGetTaxRequest.TaxOverride.Reason
    lblCurrencyCode.Caption = oGetTaxRequest.CurrencyCode
    'lblServiceMode.Caption = oGetTaxRequest.ServiceMode
    
End Sub

Private Sub FillAddressGrid()

    Dim oLine As Avalara_AvaTax_Adapter.Line
    
    '#########################################################
    '### RETRIEVE LIST OF ADDRESSES FROM THE SAVED REQUEST ###
    '#########################################################
    LoadAddressData oGetTaxRequest.OriginAddress, "Origin", "Request"
    LoadAddressData oGetTaxRequest.DestinationAddress, "Destination", "Request"
    
    For Each oLine In oGetTaxRequest.Lines
        LoadAddressData oLine.OriginAddress, "Origin", "Line(" & oLine.No & ")"
        LoadAddressData oLine.DestinationAddress, "Destination", "Line(" & oLine.No & ")"
    Next oLine
    
End Sub

Private Sub LoadAddressData(oAddress As Address, sType As String, sParent As String)

    Dim lRow As Long

    If (Not oAddress Is Nothing) Then
        With gridAddresses
            .Rows = .Rows + 1
            lRow = .Rows - 1
            
            .TextMatrix(lRow, colAddress_Parent) = sParent
            .TextMatrix(lRow, colAddress_Type) = sType
            .TextMatrix(lRow, colAddress_Line1) = oAddress.Line1
            .TextMatrix(lRow, colAddress_Line2) = oAddress.Line2
            .TextMatrix(lRow, colAddress_Line3) = oAddress.Line3
            .TextMatrix(lRow, colAddress_City) = oAddress.City
            .TextMatrix(lRow, colAddress_Region) = oAddress.Region
            .TextMatrix(lRow, colAddress_PostalCode) = oAddress.PostalCode
            .TextMatrix(lRow, colAddress_Country) = oAddress.Country
            'Added for 5.0
            .TextMatrix(lRow, colAddress_TaxRegionId) = oAddress.TaxRegionId
        End With
    End If
    
End Sub

Private Sub FillLineGrid()

    Dim lRow As Long
    Dim oLine As Avalara_AvaTax_Adapter.Line
    
    '#####################################################
    '### RETRIEVE LIST OF LINES FROM THE SAVED REQUEST ###
    '#####################################################
    For Each oLine In oGetTaxRequest.Lines
        With gridLines
            .Rows = .Rows + 1
            lRow = .Rows - 1
            
            .TextMatrix(lRow, colLine_No) = oLine.No
            .TextMatrix(lRow, colLine_ItemCode) = oLine.ItemCode
            .TextMatrix(lRow, colLine_Qty) = oLine.Qty
            .TextMatrix(lRow, colLine_Amount) = oLine.Amount
            .TextMatrix(lRow, colLine_Discounted) = oLine.Discounted
            .TextMatrix(lRow, colLine_ExemptionNo) = oLine.ExemptionNo
            .TextMatrix(lRow, colLine_Reference1) = oLine.Ref1
            .TextMatrix(lRow, colLine_Reference2) = oLine.Ref2
            .TextMatrix(lRow, colLine_RevAcct) = oLine.RevAcct
            .TextMatrix(lRow, colLine_TaxCode) = oLine.TaxCode
            'Added for 5.0
'            .TextMatrix(lRow, colLine_IsTaxOverriden) = oLine.IsTaxOverriden
'            .TextMatrix(lRow, colLine_TaxOverride) = oLine.TaxOverride
            'Added for 5.0.2
            .TextMatrix(lRow, colLine_TaxOverrideType) = oLine.TaxOverride.TaxOverrideType
            .TextMatrix(lRow, colLine_TaxAmount) = oLine.TaxOverride.TaxAmount
            .TextMatrix(lRow, colLine_TaxDate) = oLine.TaxOverride.TaxDate
            .TextMatrix(lRow, colLine_Reason) = oLine.TaxOverride.Reason
            
        End With
    Next oLine
    
End Sub

Private Sub FillAddressHeader()
    '#####################
    '### UI SETUP CODE ###
    '#####################
    With gridAddresses
        .Rows = 1
        
        .TextMatrix(0, colAddress_Parent) = "Parent"
        .TextMatrix(0, colAddress_Type) = "Type"
        .TextMatrix(0, colAddress_Line1) = "Line1"
        .TextMatrix(0, colAddress_Line2) = "Line2"
        .TextMatrix(0, colAddress_Line3) = "Line3"
        .TextMatrix(0, colAddress_City) = "City"
        .TextMatrix(0, colAddress_Region) = "State"
        .TextMatrix(0, colAddress_PostalCode) = "Zip"
        .TextMatrix(0, colAddress_Country) = "Country"
        'Added for 5.0
        .TextMatrix(0, colAddress_TaxRegionId) = ""
    
        .ColWidth(colAddress_Parent) = .ColWidth(colAddress_Parent) + (30 * Screen.TwipsPerPixelX)
        .ColWidth(colAddress_Line1) = .ColWidth(colAddress_Line1) + (50 * Screen.TwipsPerPixelX)
        .ColWidth(colAddress_Line2) = .ColWidth(colAddress_Line2) + (50 * Screen.TwipsPerPixelX)
        .ColWidth(colAddress_Line3) = .ColWidth(colAddress_Line3) + (50 * Screen.TwipsPerPixelX)
        .ColWidth(colAddress_City) = .ColWidth(colAddress_City) + (25 * Screen.TwipsPerPixelX)
        .ColWidth(colAddress_Region) = .ColWidth(colAddress_Region) ' + (10 * Screen.TwipsPerPixelX)
        .ColWidth(colAddress_PostalCode) = .ColWidth(colAddress_PostalCode) ' + (14 * Screen.TwipsPerPixelX)
        .ColWidth(colAddress_Country) = .ColWidth(colAddress_Country) + (15 * Screen.TwipsPerPixelX)
        'Added for 5.0
        .ColWidth(colAddress_TaxRegionId) = .ColWidth(colAddress_TaxRegionId) + (15 * Screen.TwipsPerPixelX)
    End With
End Sub

Private Sub FillLineHeader()
    '#####################
    '### UI SETUP CODE ###
    '#####################
    With gridLines
        .Rows = 1
        .TextMatrix(0, colLine_No) = "No"
        .TextMatrix(0, colLine_ItemCode) = "Item Code"
        .TextMatrix(0, colLine_Qty) = "Qty"
        .TextMatrix(0, colLine_Amount) = "Amount"
        .TextMatrix(0, colLine_Discounted) = "Discounted"
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
'        .ColWidth(colLine_IsTaxOverriden) = .ColWidth(colLine_IsTaxOverriden) + (15 * Screen.TwipsPerPixelX)
'        .ColWidth(colLine_TaxOverride) = .ColWidth(colLine_TaxOverride) + (15 * Screen.TwipsPerPixelX)
        'Added for 5.0.2
        .ColWidth(colLine_TaxOverrideType) = .ColWidth(colLine_TaxOverrideType) + (15 * Screen.TwipsPerPixelX)
        .ColWidth(colLine_TaxAmount) = .ColWidth(colLine_TaxAmount) + (15 * Screen.TwipsPerPixelX)
        .ColWidth(colLine_TaxDate) = .ColWidth(colLine_TaxDate) + (15 * Screen.TwipsPerPixelX)
        .ColWidth(colLine_Reason) = .ColWidth(colLine_Reason) + (15 * Screen.TwipsPerPixelX)
    End With
End Sub
