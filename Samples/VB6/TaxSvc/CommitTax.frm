VERSION 5.00
Begin VB.Form formCommitTax 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Commit Tax"
   ClientHeight    =   5355
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   5160
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   5355
   ScaleWidth      =   5160
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.CommandButton buttonCommitTax 
      Caption         =   "Commit Tax"
      Default         =   -1  'True
      Height          =   375
      Left            =   1890
      TabIndex        =   8
      Top             =   2790
      Width           =   1365
   End
   Begin VB.CommandButton buttonClose 
      Cancel          =   -1  'True
      Caption         =   "Close"
      Height          =   375
      Left            =   3810
      TabIndex        =   9
      Top             =   4710
      Width           =   1215
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
      Height          =   2385
      Left            =   360
      TabIndex        =   0
      Top             =   120
      Width           =   4275
      Begin VB.TextBox textNewDocCode 
         Height          =   330
         Left            =   1830
         TabIndex        =   7
         Top             =   1680
         Width           =   1845
      End
      Begin VB.ComboBox cboDocType 
         Height          =   315
         Left            =   1830
         Style           =   2  'Dropdown List
         TabIndex        =   6
         Top             =   1320
         Width           =   1875
      End
      Begin VB.TextBox textCompanyCode 
         Height          =   330
         Left            =   1830
         TabIndex        =   2
         Top             =   600
         Width           =   1845
      End
      Begin VB.TextBox textDocCode 
         Height          =   330
         Left            =   1830
         TabIndex        =   4
         Top             =   960
         Width           =   1845
      End
      Begin VB.Label Label7 
         Alignment       =   1  'Right Justify
         Caption         =   "New Document Code:"
         Height          =   300
         Left            =   30
         TabIndex        =   16
         Top             =   1710
         Width           =   1725
      End
      Begin VB.Label Label6 
         Alignment       =   1  'Right Justify
         Caption         =   "Document Type:"
         Height          =   300
         Left            =   390
         TabIndex        =   5
         Top             =   1350
         Width           =   1365
      End
      Begin VB.Label Label2 
         Alignment       =   1  'Right Justify
         Caption         =   "Company Code:"
         Height          =   300
         Left            =   390
         TabIndex        =   1
         Top             =   630
         Width           =   1365
      End
      Begin VB.Label Label3 
         Alignment       =   1  'Right Justify
         Caption         =   "Document Code:"
         Height          =   300
         Left            =   390
         TabIndex        =   3
         Top             =   990
         Width           =   1365
      End
   End
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
      Height          =   1215
      Left            =   90
      TabIndex        =   10
      Top             =   3330
      Width           =   4965
      Begin VB.Label Label4 
         Alignment       =   1  'Right Justify
         Caption         =   "Result Code:"
         Height          =   300
         Left            =   30
         TabIndex        =   14
         Top             =   360
         Width           =   1185
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
      Begin VB.Label lblResultCode 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1260
         TabIndex        =   12
         Top             =   360
         Width           =   3615
      End
      Begin VB.Label lblResultMsg 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1260
         TabIndex        =   11
         Top             =   720
         Width           =   3615
      End
   End
   Begin VB.Label lblStatus 
      ForeColor       =   &H00008000&
      Height          =   285
      Left            =   120
      TabIndex        =   15
      Top             =   4770
      Width           =   3585
   End
End
Attribute VB_Name = "formCommitTax"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private oCommitTaxResult As CommitTaxResult

Private Sub buttonCommitTax_Click()

    Dim oCommitTaxRequest As CommitTaxRequest
    
    On Error GoTo errCommitTax
    
    '##############################################################################
    '### 1st WE CREATE THE REQUEST OBJECT FOR DOCUMENT THAT SHOULD BE COMMITTED ###
    '##############################################################################
    Set oCommitTaxRequest = New CommitTaxRequest
    
    '###########################################################
    '### 2nd WE LOAD THE REQUEST-LEVEL DATA INTO THE REQUEST ###
    '###########################################################
    oCommitTaxRequest.CompanyCode = textCompanyCode.text
    oCommitTaxRequest.DocType = Val(cboDocType.List(cboDocType.ListIndex))
    oCommitTaxRequest.DocCode = textDocCode.text
    oCommitTaxRequest.NewDocCode = textNewDocCode.text
    
    '##############################################################################################
    '### 3rd WE INVOKE THE COMMITTAX() METHOD OF THE TAXSVC OBJECT AND GET BACK A RESULT OBJECT ###
    '##############################################################################################
    Dim oTaxSvc As TaxSvc
    
    PreMethodCall lblStatus
    
    Set oTaxSvc = New TaxSvc
    formMain.SetConfig oTaxSvc              'set the Url and Security configuration
    
    Set oCommitTaxResult = oTaxSvc.CommitTax(oCommitTaxRequest)
    
    PostMethodCall lblStatus
    
    '#####################################
    '### 4th WE READ THE RESULT OBJECT ###
    '#####################################
    lblResultCode.Caption = TranslateSeverity(oCommitTaxResult.ResultCode)
    SetMessageLabelText lblResultMsg, oCommitTaxResult
    
    Set oCommitTaxRequest = Nothing
    Set oTaxSvc = Nothing
    
    Exit Sub
    
errCommitTax:
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
    FillDocTypeCombo cboDocType
    cboDocType.ListIndex = 0
    
    textCompanyCode.text = "DEFAULT"
    textDocCode.text = "DOC0001"
    
End Sub

Private Sub lblResultMsg_Click()
    Dim frm As MessagesForm
    Set frm = New MessagesForm
    frm.Init oCommitTaxResult.Messages
    frm.Show vbModal, Me
End Sub

