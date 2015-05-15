VERSION 5.00
Object = "{5E9E78A0-531B-11CF-91F6-C2863C385E30}#1.0#0"; "MSFLXGRD.OCX"
Begin VB.Form formGetTaxResult 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "GetTax Result"
   ClientHeight    =   8490
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   8295
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   8490
   ScaleWidth      =   8295
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin MSFlexGridLib.MSFlexGrid gridTaxLines 
      CausesValidation=   0   'False
      Height          =   1695
      Left            =   150
      TabIndex        =   17
      Top             =   3930
      Width           =   7965
      _ExtentX        =   14049
      _ExtentY        =   2990
      _Version        =   393216
      Rows            =   1
      Cols            =   6
      FixedCols       =   0
      ScrollTrack     =   -1  'True
      SelectionMode   =   1
      AllowUserResizing=   1
   End
   Begin VB.CommandButton cmdClose 
      Cancel          =   -1  'True
      Caption         =   "Close"
      Height          =   375
      Left            =   6840
      TabIndex        =   16
      Top             =   7860
      Width           =   1245
   End
   Begin MSFlexGridLib.MSFlexGrid gridTaxDetails 
      CausesValidation=   0   'False
      Height          =   1695
      Left            =   120
      TabIndex        =   18
      Top             =   5970
      Width           =   7995
      _ExtentX        =   14102
      _ExtentY        =   2990
      _Version        =   393216
      Rows            =   1
      Cols            =   8
      FixedCols       =   0
      ScrollTrack     =   -1  'True
      SelectionMode   =   1
      AllowUserResizing=   1
   End
   Begin VB.Label Label23 
      Alignment       =   1  'Right Justify
      Caption         =   "Locked:"
      Height          =   300
      Index           =   3000
      Left            =   4260
      TabIndex        =   41
      Top             =   480
      Width           =   1485
   End
   Begin VB.Label lblLocked 
      Caption         =   "Label13"
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   5820
      TabIndex        =   40
      Top             =   480
      Width           =   2325
   End
   Begin VB.Label Label22 
      Alignment       =   1  'Right Justify
      Caption         =   "Adjustment Description:"
      Height          =   300
      Left            =   4080
      TabIndex        =   39
      Top             =   3120
      Width           =   1725
   End
   Begin VB.Label lblAdjustmentDescription 
      Caption         =   "Label13"
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   5880
      TabIndex        =   38
      Top             =   3120
      Width           =   2325
   End
   Begin VB.Label Label21 
      Alignment       =   1  'Right Justify
      Caption         =   "Adjustment Reason:"
      Height          =   300
      Left            =   0
      TabIndex        =   37
      Top             =   3120
      Width           =   1485
   End
   Begin VB.Label lblAdjustmentReason 
      Caption         =   "Label13"
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   1560
      TabIndex        =   36
      Top             =   3120
      Width           =   2325
   End
   Begin VB.Label Label20 
      Alignment       =   1  'Right Justify
      Caption         =   "Version:"
      Height          =   300
      Left            =   4320
      TabIndex        =   35
      Top             =   2760
      Width           =   1485
   End
   Begin VB.Label lblVersion 
      Caption         =   "Label13"
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   5880
      TabIndex        =   34
      Top             =   2760
      Width           =   2325
   End
   Begin VB.Label Label17 
      Alignment       =   1  'Right Justify
      Caption         =   "Tax Date:"
      Height          =   300
      Left            =   0
      TabIndex        =   33
      Top             =   2760
      Width           =   1485
   End
   Begin VB.Label lblTaxDate 
      Caption         =   "Label13"
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   1560
      TabIndex        =   32
      Top             =   2760
      Width           =   2325
   End
   Begin VB.Label Label19 
      Alignment       =   1  'Right Justify
      Caption         =   "Total Exemption:"
      Height          =   300
      Left            =   4260
      TabIndex        =   31
      Top             =   2400
      Width           =   1485
   End
   Begin VB.Label lblTotalExemption 
      Caption         =   "Label13"
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   5820
      TabIndex        =   30
      Top             =   2400
      Width           =   2325
   End
   Begin VB.Label Label10 
      Alignment       =   1  'Right Justify
      Caption         =   "Total Discount:"
      Height          =   300
      Left            =   4260
      TabIndex        =   29
      Top             =   2070
      Width           =   1485
   End
   Begin VB.Label lblTotalDiscount 
      Caption         =   "Label13"
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   5820
      TabIndex        =   28
      Top             =   2070
      Width           =   2325
   End
   Begin VB.Label Label18 
      Alignment       =   1  'Right Justify
      Caption         =   "Document Date:"
      Height          =   300
      Left            =   30
      TabIndex        =   27
      Top             =   2055
      Width           =   1485
   End
   Begin VB.Label lblDocDate 
      Caption         =   "Label13"
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   1590
      TabIndex        =   26
      Top             =   2070
      Width           =   2325
   End
   Begin VB.Label Label16 
      Alignment       =   1  'Right Justify
      Caption         =   "Document Code:"
      Height          =   300
      Left            =   30
      TabIndex        =   25
      Top             =   1740
      Width           =   1485
   End
   Begin VB.Label lblDocCode 
      Caption         =   "Label13"
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   1590
      TabIndex        =   24
      Top             =   1725
      Width           =   2325
   End
   Begin VB.Label Label5 
      Alignment       =   1  'Right Justify
      Caption         =   "Document Type:"
      Height          =   300
      Left            =   30
      TabIndex        =   23
      Top             =   1410
      Width           =   1485
   End
   Begin VB.Label lblDocType 
      Caption         =   "Label13"
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   1590
      TabIndex        =   22
      Top             =   1395
      Width           =   2325
   End
   Begin VB.Label Label15 
      Caption         =   "Properties"
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
      Left            =   300
      TabIndex        =   21
      Top             =   90
      Width           =   1395
   End
   Begin VB.Label Label14 
      Caption         =   "Tax Details (for selected Tax Line)"
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
      Left            =   150
      TabIndex        =   20
      Top             =   5730
      Width           =   3435
   End
   Begin VB.Label Label13 
      Caption         =   "Tax Lines"
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
      Left            =   150
      TabIndex        =   19
      Top             =   3690
      Width           =   1215
   End
   Begin VB.Label lblTotalTax 
      Caption         =   "Label13"
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   5820
      TabIndex        =   15
      Top             =   1740
      Width           =   2325
   End
   Begin VB.Label lblTotalAmount 
      Caption         =   "Label13"
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   5820
      TabIndex        =   14
      Top             =   1410
      Width           =   2325
   End
   Begin VB.Label lblTotalTaxable 
      Caption         =   "Label13"
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   5820
      TabIndex        =   13
      Top             =   1080
      Width           =   2325
   End
   Begin VB.Label lblReconciled 
      Caption         =   "Label13"
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   5820
      TabIndex        =   12
      Top             =   750
      Width           =   2325
   End
   Begin VB.Label lblDocStatusDate 
      Caption         =   "Label13"
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   1590
      TabIndex        =   11
      Top             =   2400
      Width           =   2325
   End
   Begin VB.Label lblDocStatus 
      Caption         =   "Label13"
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   1590
      TabIndex        =   10
      Top             =   1080
      Width           =   2325
   End
   Begin VB.Label lblResultMsg 
      Caption         =   "Label13"
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   1590
      TabIndex        =   9
      Top             =   750
      Width           =   2325
   End
   Begin VB.Label lblResultCode 
      Caption         =   "Label13"
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   1590
      TabIndex        =   8
      Top             =   420
      Width           =   2325
   End
   Begin VB.Label Label12 
      Alignment       =   1  'Right Justify
      Caption         =   "Total Tax:"
      Height          =   300
      Left            =   4260
      TabIndex        =   7
      Top             =   1740
      Width           =   1485
   End
   Begin VB.Label Label11 
      Alignment       =   1  'Right Justify
      Caption         =   "Total Amount:"
      Height          =   300
      Left            =   4260
      TabIndex        =   6
      Top             =   1410
      Width           =   1485
   End
   Begin VB.Label Label9 
      Alignment       =   1  'Right Justify
      Caption         =   "Total Taxable:"
      Height          =   300
      Left            =   4260
      TabIndex        =   5
      Top             =   1065
      Width           =   1485
   End
   Begin VB.Label Label8 
      Alignment       =   1  'Right Justify
      Caption         =   "Reconciled:"
      Height          =   300
      Left            =   4260
      TabIndex        =   4
      Top             =   750
      Width           =   1485
   End
   Begin VB.Label Label7 
      Alignment       =   1  'Right Justify
      Caption         =   "Doc Status Date:"
      Height          =   300
      Left            =   30
      TabIndex        =   3
      Top             =   2400
      Width           =   1485
   End
   Begin VB.Label Label6 
      Alignment       =   1  'Right Justify
      Caption         =   "Document Status:"
      Height          =   300
      Left            =   30
      TabIndex        =   2
      Top             =   1065
      Width           =   1485
   End
   Begin VB.Label Label2 
      Alignment       =   1  'Right Justify
      Caption         =   "Result Msg:"
      Height          =   300
      Left            =   30
      TabIndex        =   1
      Top             =   735
      Width           =   1485
   End
   Begin VB.Label Label1 
      Alignment       =   1  'Right Justify
      Caption         =   "Result Code:"
      Height          =   300
      Left            =   30
      TabIndex        =   0
      Top             =   420
      Width           =   1485
   End
End
Attribute VB_Name = "formGetTaxResult"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Enum LineColumns
    colLine_No = 0
    'colLine_Discount
    'These properties have been deprecated in release version 4.1: colLine_Taxable
    'These properties have been deprecated in release version 4.1: colLine_Exemption
    'These properties have been deprecated in release version 4.1: colLine_Rate
    colLine_ExemptCertId
    colLine_Tax
    colLine_TaxCode
    colLine_Taxability
    colLine_BoundaryLevel
End Enum

Private Enum DetailColumns
    colDetail_JurisType = 0
    colDetail_JurisCode
    colDetail_Taxable
    colDetail_NonTaxable
    colDetail_Exemption
    colDetail_Rate
    colDetail_Tax
    colDetail_TaxType
End Enum

Public oGetTaxResult As GetTaxResult

Private Sub cmdClose_Click()
    Set oGetTaxResult = Nothing
    Unload Me
End Sub

Private Sub Form_Load()

    FillTaxLineHeader
    FillTaxDetailHeader
    
    '#######################################################
    '### READ THE PROPERTY VALUES FROM THE RESULT OBJECT ###
    '#######################################################
    With oGetTaxResult
        lblResultCode.Caption = TranslateSeverity(.ResultCode)
        SetMessageLabelText lblResultMsg, oGetTaxResult
        lblDocStatus.Caption = TranslateDocStatus(.DocStatus)
        lblDocType.Caption = TranslateDocType(.DocType)
        lblDocCode.Caption = .DocCode
        lblDocDate.Caption = .DocDate
        lblDocStatusDate.Caption = .TimeStamp
        lblReconciled.Caption = .Reconciled
        lblTotalTaxable.Caption = Format$(.TotalTaxable, "0.00")
        lblTotalAmount.Caption = Format$(.TotalAmount, "0.00")
        lblTotalTax.Caption = Format$(.TotalTax, "0.00")
        lblTotalDiscount.Caption = Format$(.TotalDiscount, "0.00")
        lblTotalExemption.Caption = Format$(.TotalExemption, "0.00")
        'Added new fields from 4.16
        lblTaxDate.Caption = .TaxDate
        lblVersion.Caption = .Version
        lblAdjustmentReason.Caption = .AdjustmentReason
        lblAdjustmentDescription.Caption = .AdjustmentDescription
        lblLocked.Caption = .Locked
    End With
    
    '###########################################################
    '### ITERATE THROUGH THE COLLECTION ON THE RESULT OBJECT ###
    '###########################################################
    Dim oTaxLines As TaxLines
    Dim oTaxLine As TaxLine
    
    Set oTaxLines = oGetTaxResult.TaxLines
    For Each oTaxLine In oTaxLines
        FillTaxLineData oTaxLine
    Next oTaxLine
    
    '###########################################
    '### LOAD THE DETAIL FOR A SELECTED LINE ###
    '###########################################
    gridTaxLines_SelChange
    
End Sub

Private Sub FillTaxLineData(oTaxLine As TaxLine)

    Dim lRow As Long
    
    With gridTaxLines
        .Rows = .Rows + 1
        lRow = .Rows - 1
        
        .TextMatrix(lRow, colLine_No) = oTaxLine.No
        '.TextMatrix(lRow, colLine_Discount) = oTaxLine.Discount
'
'These properties have been deprecated in release version 4.1.
'        .TextMatrix(lRow, colLine_Taxable) = Format$(oTaxLine.Taxable, "0.00")
'        .TextMatrix(lRow, colLine_Exemption) = Format$(oTaxLine.Exemption, "0.00")
'        .TextMatrix(lRow, colLine_Rate) = Format$(oTaxLine.Rate, "0.00######")

        .TextMatrix(lRow, colLine_ExemptCertId) = Format$(oTaxLine.ExemptCertId, "0.00")
        
        .TextMatrix(lRow, colLine_Tax) = Format$(oTaxLine.Tax, "0.00")
        .TextMatrix(lRow, colLine_TaxCode) = oTaxLine.TaxCode
        .TextMatrix(lRow, colLine_Taxability) = IIf(oTaxLine.Taxability, "Yes", "No")
        .TextMatrix(lRow, colLine_BoundaryLevel) = TranslateBoundaryLevel(oTaxLine.boundaryLevel)
    End With

End Sub

Private Sub FillTaxDetailData(oTaxDetail As TaxDetail)

    Dim lRow As Long
    
    With gridTaxDetails
        .Rows = .Rows + 1
        lRow = .Rows - 1
        
        .TextMatrix(lRow, colDetail_JurisType) = TranslateJurisdictionType(oTaxDetail.JurisType)
        .TextMatrix(lRow, colDetail_JurisCode) = oTaxDetail.JurisCode
        .TextMatrix(lRow, colDetail_Taxable) = Format$(oTaxDetail.Taxable, "0.00")
        .TextMatrix(lRow, colDetail_NonTaxable) = Format$(oTaxDetail.NonTaxable, "0.00")
        .TextMatrix(lRow, colDetail_Exemption) = Format$(oTaxDetail.Exemption, "0.00######")
        .TextMatrix(lRow, colDetail_Rate) = Format$(oTaxDetail.Rate, "0.00######")
        .TextMatrix(lRow, colDetail_Tax) = Format$(oTaxDetail.Tax, "0.00")
        .TextMatrix(lRow, colDetail_TaxType) = TranslateTaxType(oTaxDetail.taxType)
    End With
    
End Sub

Private Sub FillTaxLineHeader()

    '#####################
    '### UI SETUP CODE ###
    '#####################
    With gridTaxLines
        .TextMatrix(0, colLine_No) = "No"
        '.TextMatrix(0, colLine_Discount) = "Discount"
'
'These properties have been deprecated in release version 4.1.
'        .TextMatrix(0, colLine_Taxable) = "Taxable"
'        .TextMatrix(0, colLine_Exemption) = "Exemption"
'        .TextMatrix(0, colLine_Rate) = "Rate"

        .TextMatrix(0, colLine_ExemptCertId) = "ExemptCertId"
        .TextMatrix(0, colLine_Tax) = "Tax"
        .TextMatrix(0, colLine_TaxCode) = "Tax Code"
        .TextMatrix(0, colLine_Taxability) = "Taxability"
        .TextMatrix(0, colLine_BoundaryLevel) = "Boundary Level"
        
        .ColWidth(colLine_No) = .ColWidth(colLine_No) + (10 * Screen.TwipsPerPixelX)
        .ColWidth(colLine_TaxCode) = .ColWidth(colLine_TaxCode) + (15 * Screen.TwipsPerPixelX)
    End With
    
End Sub

Private Sub FillTaxDetailHeader()

    '#####################
    '### UI SETUP CODE ###
    '#####################
    With gridTaxDetails
        .TextMatrix(0, colDetail_JurisType) = "Jurisdiction Type"
        .TextMatrix(0, colDetail_JurisCode) = "FIPS Code"
        .TextMatrix(0, colDetail_Taxable) = "Taxable"
        .TextMatrix(0, colDetail_NonTaxable) = "NonTaxable"
        .TextMatrix(0, colDetail_Exemption) = "Exemption"
        .TextMatrix(0, colDetail_Rate) = "Rate"
        .TextMatrix(0, colDetail_Tax) = "Tax"
        .TextMatrix(0, colDetail_TaxType) = "Tax Type"
    
        .ColWidth(colDetail_JurisType) = .ColWidth(colDetail_JurisType) + (27 * Screen.TwipsPerPixelX)
        .ColWidth(colDetail_JurisCode) = .ColWidth(colDetail_JurisType) + (10 * Screen.TwipsPerPixelX)
        .ColWidth(colDetail_TaxType) = .ColWidth(colDetail_TaxType) + (10 * Screen.TwipsPerPixelX)
    End With
    
End Sub


Private Sub gridTaxLines_SelChange()
    
    Dim oTaxLine As TaxLine
    Dim oTaxDetail As TaxDetail
    
    gridTaxDetails.Rows = 1
    
    '#####################################################################################
    '### ITERATE THROUGH THE TAX DETAILS (JURISDICTION TAX DATA) FOR A SINGLE TAX LINE ###
    '#####################################################################################
    If (gridTaxLines.Row > 0) And (oGetTaxResult.TaxLines.Count > 0) Then
        Set oTaxLine = oGetTaxResult.TaxLines.Item(gridTaxLines.Row - 1)
        
        For Each oTaxDetail In oTaxLine.TaxDetails
            FillTaxDetailData oTaxDetail
        Next oTaxDetail
    End If
End Sub

Private Sub lblResultMsg_Click()
    Dim frm As MessagesForm
    Set frm = New MessagesForm
    frm.Init oGetTaxResult.Messages
    frm.Show vbModal, Me
End Sub
