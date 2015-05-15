VERSION 5.00
Begin VB.Form formGetTaxHistory 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Get Tax History"
   ClientHeight    =   6255
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   5250
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6255
   ScaleWidth      =   5250
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.CommandButton buttonGetTaxResult 
      Caption         =   "View GetTaxResult"
      Enabled         =   0   'False
      Height          =   375
      Left            =   2663
      TabIndex        =   11
      Top             =   4590
      Width           =   1875
   End
   Begin VB.CommandButton buttonGetTaxRequest 
      Caption         =   "View GetTaxRequest"
      Enabled         =   0   'False
      Height          =   375
      Left            =   713
      TabIndex        =   10
      Top             =   4590
      Width           =   1875
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
      Height          =   1185
      Left            =   120
      TabIndex        =   13
      Top             =   3300
      Width           =   4965
      Begin VB.Label lblResultMsg 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1260
         TabIndex        =   17
         Top             =   720
         Width           =   3615
      End
      Begin VB.Label lblResultCode 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1260
         TabIndex        =   16
         Top             =   360
         Width           =   3615
      End
      Begin VB.Label Label5 
         Alignment       =   1  'Right Justify
         Caption         =   "Result Msg:"
         Height          =   300
         Left            =   30
         TabIndex        =   15
         Top             =   720
         Width           =   1185
      End
      Begin VB.Label Label4 
         Alignment       =   1  'Right Justify
         Caption         =   "Result Code:"
         Height          =   300
         Left            =   30
         TabIndex        =   14
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
      Height          =   2445
      Left            =   465
      TabIndex        =   0
      Top             =   60
      Width           =   4275
      Begin VB.ComboBox cboDocType 
         Height          =   315
         Left            =   1800
         Style           =   2  'Dropdown List
         TabIndex        =   6
         Top             =   1050
         Width           =   1875
      End
      Begin VB.ComboBox cboDetailLevel 
         Height          =   315
         Left            =   1800
         Style           =   2  'Dropdown List
         TabIndex        =   8
         Top             =   1770
         Width           =   1875
      End
      Begin VB.TextBox textDocCode 
         Height          =   330
         Left            =   1800
         TabIndex        =   4
         Top             =   690
         Width           =   1845
      End
      Begin VB.TextBox textCompanyCode 
         Height          =   330
         Left            =   1800
         TabIndex        =   2
         Top             =   330
         Width           =   1845
      End
      Begin VB.Line Line1 
         X1              =   600
         X2              =   3720
         Y1              =   1560
         Y2              =   1560
      End
      Begin VB.Label Label6 
         Alignment       =   1  'Right Justify
         Caption         =   "Document Type:"
         Height          =   300
         Left            =   360
         TabIndex        =   5
         Top             =   1080
         Width           =   1365
      End
      Begin VB.Label Label1 
         Alignment       =   1  'Right Justify
         Caption         =   "Detail Level:"
         Height          =   300
         Left            =   360
         TabIndex        =   7
         Top             =   1800
         Width           =   1365
      End
      Begin VB.Label Label3 
         Alignment       =   1  'Right Justify
         Caption         =   "Document Code:"
         Height          =   300
         Left            =   360
         TabIndex        =   3
         Top             =   720
         Width           =   1365
      End
      Begin VB.Label Label2 
         Alignment       =   1  'Right Justify
         Caption         =   "Company Code:"
         Height          =   300
         Left            =   360
         TabIndex        =   1
         Top             =   360
         Width           =   1365
      End
   End
   Begin VB.CommandButton buttonClose 
      Cancel          =   -1  'True
      Caption         =   "Close"
      Height          =   375
      Left            =   3840
      TabIndex        =   12
      Top             =   5220
      Width           =   1215
   End
   Begin VB.CommandButton buttonGetTaxHistory 
      Caption         =   "Get Tax History"
      Default         =   -1  'True
      Height          =   375
      Left            =   1920
      TabIndex        =   9
      Top             =   2640
      Width           =   1365
   End
   Begin VB.Label lblStatus 
      ForeColor       =   &H00008000&
      Height          =   285
      Left            =   150
      TabIndex        =   18
      Top             =   5280
      Width           =   3585
   End
End
Attribute VB_Name = "formGetTaxHistory"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private oGetTaxHistoryResult As GetTaxHistoryResult
Private oGetTaxRequest As GetTaxRequest
Private oGetTaxResult As GetTaxResult

Private Sub buttonClose_Click()

    Set oGetTaxRequest = Nothing
    Set oGetTaxResult = Nothing
    
    Unload Me
    
End Sub

Private Sub buttonGetTaxHistory_Click()

    Dim oGetTaxHistoryRequest As GetTaxHistoryRequest
    
    On Error GoTo errGetTaxHistory
    
    '#########################################################################
    '### 1st WE CREATE THE REQUEST OBJECT FOR THE DOCUMENT HISTORY WE WANT ###
    '#########################################################################
    Set oGetTaxHistoryRequest = New GetTaxHistoryRequest
    
    '###########################################################
    '### 2nd WE LOAD THE REQUEST-LEVEL DATA INTO THE REQUEST ###
    '###########################################################
    oGetTaxHistoryRequest.CompanyCode = textCompanyCode.text
    oGetTaxHistoryRequest.DocCode = textDocCode.text
    oGetTaxHistoryRequest.DocType = Val(cboDocType.List(cboDocType.ListIndex))
    oGetTaxHistoryRequest.DetailLevel = Val(cboDetailLevel.List(cboDetailLevel.ListIndex))
    
    '##################################################################################################
    '### 3rd WE INVOKE THE GETTAXHISTORY() METHOD OF THE TAXSVC OBJECT AND GET BACK A RESULT OBJECT ###
    '##################################################################################################
    Dim oTaxSvc As TaxSvc
    
    PreMethodCall lblStatus
    
    Set oTaxSvc = New TaxSvc
    formMain.SetConfig oTaxSvc              'set the Url and Security configuration
    
    Set oGetTaxHistoryResult = oTaxSvc.GetTaxHistory(oGetTaxHistoryRequest)
    
    PostMethodCall lblStatus
    
    '#####################################
    '### 4th WE READ THE RESULT OBJECT ###
    '#####################################
    lblResultCode.Caption = TranslateSeverity(oGetTaxHistoryResult.ResultCode)
    SetMessageLabelText lblResultMsg, oGetTaxHistoryResult
    Set oGetTaxRequest = oGetTaxHistoryResult.GetTaxRequest
    Set oGetTaxResult = oGetTaxHistoryResult.GetTaxResult
    
    buttonGetTaxRequest.Enabled = (Not oGetTaxRequest Is Nothing)
    buttonGetTaxResult.Enabled = (Not oGetTaxResult Is Nothing)
    
    Set oGetTaxHistoryRequest = Nothing
    Set oTaxSvc = Nothing
    
    Exit Sub
    
errGetTaxHistory:
    lblStatus.Caption = ""
    Screen.MousePointer = vbDefault
    ShowError Err
End Sub

Private Sub buttonGetTaxRequest_Click()

    Dim frm As formGetTaxRequest
    Set frm = New formGetTaxRequest
    Set frm.oGetTaxRequest = oGetTaxRequest
    frm.Show vbModal, Me
    
End Sub

Private Sub buttonGetTaxResult_Click()

    Dim frm As formGetTaxResult
    Set frm = New formGetTaxResult
    Set frm.oGetTaxResult = oGetTaxResult
    frm.Show vbModal, Me
    
End Sub

Private Sub Form_Load()
    
    '#####################
    '### UI SETUP CODE ###
    '#####################
    buttonGetTaxRequest.Enabled = False
    buttonGetTaxResult.Enabled = False
    FillCombos
    
    textCompanyCode.text = "DEFAULT"
    textDocCode.text = "DOC0001"
    cboDetailLevel.ListIndex = 3
    
End Sub

Private Sub FillCombos()
    '#####################
    '### UI SETUP CODE ###
    '#####################
    cboDetailLevel.AddItem DetailLevel.DetailLevel_Summary & " : Summary"
    cboDetailLevel.AddItem DetailLevel.DetailLevel_Document & " : Document"
    cboDetailLevel.AddItem DetailLevel.DetailLevel_Line & " : Line"
    cboDetailLevel.AddItem DetailLevel.DetailLevel_Tax & " : Tax"
    cboDetailLevel.ListIndex = 0
    
    FillDocTypeCombo cboDocType
    cboDocType.ListIndex = 0

End Sub

Private Sub lblResultMsg_Click()
    Dim frm As MessagesForm
    Set frm = New MessagesForm
    frm.Init oGetTaxHistoryResult.Messages
    frm.Show vbModal, Me
End Sub
