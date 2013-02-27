VERSION 5.00
Begin VB.Form formPostTax 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Post Tax"
   ClientHeight    =   7275
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   5205
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   7275
   ScaleWidth      =   5205
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
      Height          =   1155
      Left            =   90
      TabIndex        =   18
      Top             =   5250
      Width           =   4965
      Begin VB.Label lblResultMsg 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1260
         TabIndex        =   22
         Top             =   720
         Width           =   3615
      End
      Begin VB.Label lblResultCode 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1260
         TabIndex        =   21
         Top             =   360
         Width           =   3615
      End
      Begin VB.Label Label5 
         Alignment       =   1  'Right Justify
         Caption         =   "Result Msg:"
         Height          =   300
         Left            =   30
         TabIndex        =   20
         Top             =   720
         Width           =   1185
      End
      Begin VB.Label Label4 
         Alignment       =   1  'Right Justify
         Caption         =   "Result Code:"
         Height          =   300
         Left            =   30
         TabIndex        =   19
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
      Height          =   4335
      Left            =   480
      TabIndex        =   0
      Top             =   120
      Width           =   4275
      Begin VB.CheckBox chkCommit 
         Height          =   255
         Left            =   1800
         TabIndex        =   15
         Top             =   3600
         Width           =   375
      End
      Begin VB.ComboBox cboDocType 
         Height          =   315
         Left            =   1830
         Style           =   2  'Dropdown List
         TabIndex        =   5
         Top             =   840
         Width           =   1875
      End
      Begin VB.TextBox textNewDocCode 
         Height          =   330
         Left            =   1830
         TabIndex        =   14
         Top             =   3240
         Width           =   1845
      End
      Begin VB.TextBox textTotalTax 
         Height          =   330
         Left            =   1830
         TabIndex        =   13
         Top             =   2880
         Width           =   1845
      End
      Begin VB.TextBox textTotalAmount 
         Height          =   330
         Left            =   1830
         TabIndex        =   11
         Top             =   2520
         Width           =   1845
      End
      Begin VB.TextBox textDocDate 
         Height          =   330
         Left            =   1830
         TabIndex        =   9
         Top             =   2160
         Width           =   1845
      End
      Begin VB.TextBox textDocCode 
         Height          =   330
         Left            =   1830
         TabIndex        =   7
         Top             =   1200
         Width           =   1845
      End
      Begin VB.TextBox textCompanyCode 
         Height          =   330
         Left            =   1830
         TabIndex        =   3
         Top             =   480
         Width           =   1845
      End
      Begin VB.Line Line1 
         X1              =   600
         X2              =   3720
         Y1              =   1800
         Y2              =   1800
      End
      Begin VB.Label Label11 
         Alignment       =   1  'Right Justify
         Caption         =   "Commit:"
         Height          =   300
         Left            =   360
         TabIndex        =   24
         Top             =   3600
         Width           =   1365
      End
      Begin VB.Label Label9 
         Alignment       =   1  'Right Justify
         Caption         =   "New Document Code:"
         Height          =   300
         Left            =   60
         TabIndex        =   1
         Top             =   3270
         Width           =   1695
      End
      Begin VB.Label Label8 
         Alignment       =   1  'Right Justify
         Caption         =   "Document Type:"
         Height          =   300
         Left            =   390
         TabIndex        =   4
         Top             =   870
         Width           =   1365
      End
      Begin VB.Label Label7 
         Alignment       =   1  'Right Justify
         Caption         =   "Total Tax:"
         Height          =   300
         Left            =   390
         TabIndex        =   12
         Top             =   2910
         Width           =   1365
      End
      Begin VB.Label Label6 
         Alignment       =   1  'Right Justify
         Caption         =   "Total Amount:"
         Height          =   300
         Left            =   390
         TabIndex        =   10
         Top             =   2550
         Width           =   1365
      End
      Begin VB.Label Label1 
         Alignment       =   1  'Right Justify
         Caption         =   "Document Date:"
         Height          =   300
         Left            =   390
         TabIndex        =   8
         Top             =   2190
         Width           =   1365
      End
      Begin VB.Label Label3 
         Alignment       =   1  'Right Justify
         Caption         =   "Document Code:"
         Height          =   300
         Left            =   390
         TabIndex        =   6
         Top             =   1230
         Width           =   1365
      End
      Begin VB.Label Label2 
         Alignment       =   1  'Right Justify
         Caption         =   "Company Code:"
         Height          =   300
         Left            =   390
         TabIndex        =   2
         Top             =   510
         Width           =   1365
      End
   End
   Begin VB.CommandButton buttonClose 
      Cancel          =   -1  'True
      Caption         =   "Close"
      Height          =   375
      Left            =   3810
      TabIndex        =   16
      Top             =   6720
      Width           =   1215
   End
   Begin VB.CommandButton buttonPostTax 
      Caption         =   "Post Tax"
      Default         =   -1  'True
      Height          =   375
      Left            =   1890
      TabIndex        =   17
      Top             =   4710
      Width           =   1365
   End
   Begin VB.Label lblStatus 
      ForeColor       =   &H00008000&
      Height          =   285
      Left            =   120
      TabIndex        =   23
      Top             =   6780
      Width           =   3585
   End
End
Attribute VB_Name = "formPostTax"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private oPostTaxResult As PostTaxResult

Private Sub buttonPostTax_Click()

    Dim oPostTaxRequest As PostTaxRequest
    
    On Error GoTo errPostTax
    
    If (IsValidDateTime(textDocDate.text) = False) Then
        MsgBox "Invalid date/time value.", vbOKOnly Or vbExclamation, "Invalid Field"
        textDocDate.SetFocus
        Exit Sub
    End If
    
    '###########################################################################
    '### 1st WE CREATE THE REQUEST OBJECT FOR DOCUMENT THAT SHOULD BE POSTED ###
    '###########################################################################
    Set oPostTaxRequest = New PostTaxRequest
    
    '###########################################################
    '### 2nd WE LOAD THE REQUEST-LEVEL DATA INTO THE REQUEST ###
    '###########################################################
    oPostTaxRequest.CompanyCode = textCompanyCode.text
    oPostTaxRequest.DocType = Val(cboDocType.List(cboDocType.ListIndex))
    oPostTaxRequest.DocCode = textDocCode.text
    
    If (textDocDate.text <> "") Then
        oPostTaxRequest.DocDate = CDate(textDocDate.text)
    End If
    If (textTotalAmount.text <> "") Then
        oPostTaxRequest.TotalAmount = CCur(textTotalAmount.text)
    End If
    If (textTotalTax.text <> "") Then
        oPostTaxRequest.TotalTax = CCur(textTotalTax.text)
    End If
    oPostTaxRequest.NewDocCode = textNewDocCode.text
    'Added new Commit field from 4.16 updates
    oPostTaxRequest.Commit = (chkCommit.Value = vbChecked)
    
    '############################################################################################
    '### 3rd WE INVOKE THE POSTTAX() METHOD OF THE TAXSVC OBJECT AND GET BACK A RESULT OBJECT ###
    '############################################################################################
    Dim oTaxSvc As TaxSvc
    
    PreMethodCall lblStatus
    
    Set oTaxSvc = New TaxSvc
    formMain.SetConfig oTaxSvc              'set the Url and Security configuration
    
    Set oPostTaxResult = oTaxSvc.PostTax(oPostTaxRequest)
    
    PostMethodCall lblStatus
    
    '#####################################
    '### 4th WE READ THE RESULT OBJECT ###
    '#####################################
    lblResultCode.Caption = TranslateSeverity(oPostTaxResult.ResultCode)
    SetMessageLabelText lblResultMsg, oPostTaxResult
    
    Set oPostTaxRequest = Nothing
    Set oTaxSvc = Nothing
    
    Exit Sub
    
errPostTax:
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
    textDocDate.text = Format$(Date, "m/d/yy")
    textTotalAmount.text = ""
    textTotalTax.text = ""
    
End Sub

Private Sub lblResultMsg_Click()
    Dim frm As MessagesForm
    Set frm = New MessagesForm
    frm.Init oPostTaxResult.Messages
    frm.Show vbModal, Me
End Sub
