VERSION 5.00
Begin VB.Form formCancelTax 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Cancel Tax"
   ClientHeight    =   5865
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   5130
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   5865
   ScaleWidth      =   5130
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.Frame Frame2 
      Caption         =   "Result"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   1335
      Left            =   120
      TabIndex        =   11
      Top             =   3600
      Width           =   4965
      Begin VB.Label lblResultMsg 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1260
         TabIndex        =   15
         Top             =   720
         Width           =   3615
      End
      Begin VB.Label lblResultCode 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1260
         TabIndex        =   14
         Top             =   360
         Width           =   3615
      End
      Begin VB.Label Label5 
         Alignment       =   1  'Right Justify
         Caption         =   "Result Msg:"
         Height          =   300
         Left            =   30
         TabIndex        =   13
         Top             =   720
         Width           =   1185
      End
      Begin VB.Label Label4 
         Alignment       =   1  'Right Justify
         Caption         =   "Result Code:"
         Height          =   300
         Left            =   30
         TabIndex        =   12
         Top             =   360
         Width           =   1185
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "Request"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   2655
      Left            =   428
      TabIndex        =   0
      Top             =   120
      Width           =   4275
      Begin VB.ComboBox cboDocType 
         Height          =   315
         Left            =   1830
         Style           =   2  'Dropdown List
         TabIndex        =   8
         Top             =   1980
         Width           =   1875
      End
      Begin VB.TextBox textDocCode 
         Height          =   330
         Left            =   1830
         TabIndex        =   6
         Top             =   1620
         Width           =   1845
      End
      Begin VB.TextBox textCompanyCode 
         Height          =   330
         Left            =   1830
         TabIndex        =   4
         Top             =   1260
         Width           =   1845
      End
      Begin VB.ComboBox cboCancelCode 
         Height          =   315
         Left            =   1830
         Style           =   2  'Dropdown List
         TabIndex        =   2
         Top             =   300
         Width           =   1875
      End
      Begin VB.Line Line1 
         X1              =   600
         X2              =   3720
         Y1              =   840
         Y2              =   840
      End
      Begin VB.Label Label6 
         Alignment       =   1  'Right Justify
         Caption         =   "Document Type:"
         Height          =   300
         Left            =   390
         TabIndex        =   7
         Top             =   2040
         Width           =   1365
      End
      Begin VB.Label Label3 
         Alignment       =   1  'Right Justify
         Caption         =   "Document Code:"
         Height          =   300
         Left            =   390
         TabIndex        =   5
         Top             =   1650
         Width           =   1365
      End
      Begin VB.Label Label2 
         Alignment       =   1  'Right Justify
         Caption         =   "Company Code:"
         Height          =   300
         Left            =   390
         TabIndex        =   3
         Top             =   1290
         Width           =   1365
      End
      Begin VB.Label Label1 
         Alignment       =   1  'Right Justify
         Caption         =   "Cancel Code:"
         Height          =   300
         Left            =   390
         TabIndex        =   1
         Top             =   330
         Width           =   1365
      End
   End
   Begin VB.CommandButton buttonClose 
      Cancel          =   -1  'True
      Caption         =   "Close"
      Height          =   375
      Left            =   3810
      TabIndex        =   10
      Top             =   5250
      Width           =   1215
   End
   Begin VB.CommandButton buttonCancelTax 
      Caption         =   "Cancel Tax"
      Default         =   -1  'True
      Height          =   375
      Left            =   1883
      TabIndex        =   9
      Top             =   3000
      Width           =   1365
   End
   Begin VB.Label lblStatus 
      ForeColor       =   &H00008000&
      Height          =   285
      Left            =   120
      TabIndex        =   16
      Top             =   5310
      Width           =   3585
   End
End
Attribute VB_Name = "formCancelTax"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private oCancelTaxResult As CancelTaxResult

Private Sub buttonCancelTax_Click()

    Dim oCancelTaxRequest As CancelTaxRequest
    
    On Error GoTo errCancelTax
    
    '##############################################################################
    '### 1st WE CREATE THE REQUEST OBJECT FOR DOCUMENT THAT SHOULD BE CANCELLED ###
    '##############################################################################
    Set oCancelTaxRequest = New CancelTaxRequest
    
    '###########################################################
    '### 2nd WE LOAD THE REQUEST-LEVEL DATA INTO THE REQUEST ###
    '###########################################################
    oCancelTaxRequest.CompanyCode = textCompanyCode.text
    oCancelTaxRequest.DocType = Val(cboDocType.List(cboDocType.ListIndex))
    oCancelTaxRequest.DocCode = textDocCode.text
    oCancelTaxRequest.CancelCode = Val(cboCancelCode.List(cboCancelCode.ListIndex))
    
    '##############################################################################################
    '### 3rd WE INVOKE THE CANCELTAX() METHOD OF THE TAXSVC OBJECT AND GET BACK A RESULT OBJECT ###
    '##############################################################################################
    Dim oTaxSvc As TaxSvc
    
    PreMethodCall lblStatus
    
    Set oTaxSvc = New TaxSvc
    formMain.SetConfig oTaxSvc              'set the Url and Security configuration
    
    Set oCancelTaxResult = oTaxSvc.CancelTax(oCancelTaxRequest)
    
    PostMethodCall lblStatus
    
    '#####################################
    '### 4th WE READ THE RESULT OBJECT ###
    '#####################################
    lblResultCode.Caption = TranslateSeverity(oCancelTaxResult.ResultCode)
    SetMessageLabelText lblResultMsg, oCancelTaxResult
    
    Set oCancelTaxRequest = Nothing
    Set oTaxSvc = Nothing
    
    Exit Sub
    
errCancelTax:
    lblStatus.Caption = ""
    Screen.MousePointer = vbDefault
    ShowError Err
End Sub

Private Sub buttonClose_Click()
    Unload Me
End Sub

Private Sub Form_Load()

    '#####################
    '### UI SETUP CODE ###
    '#####################
    FillCombos
    
    cboCancelCode.ListIndex = 1
    textCompanyCode.text = "DEFAULT"
    textDocCode.text = "DOC0001"
    
End Sub

Private Sub FillCombos()
    '#####################
    '### UI SETUP CODE ###
    '#####################
    cboCancelCode.AddItem CancelCode_Unspecified & " : Unspecified"
    cboCancelCode.AddItem CancelCode_PostFailed & " : Post Failed"
    cboCancelCode.AddItem CancelCode_DocDeleted & " : Document Deleted"
    cboCancelCode.AddItem CancelCode_DocVoided & " : Document Voided"
    cboCancelCode.AddItem CancelCode_AdjustmentCancelled & " : Adjustment Cancelled"
    cboCancelCode.ListIndex = 1
    
    FillDocTypeCombo cboDocType
    cboDocType.ListIndex = 0

End Sub

Private Sub lblResultMsg_Click()
    Dim frm As MessagesForm
    Set frm = New MessagesForm
    frm.Init oCancelTaxResult.Messages
    frm.Show vbModal, Me
End Sub
