VERSION 5.00
Object = "{5E9E78A0-531B-11CF-91F6-C2863C385E30}#1.0#0"; "MSFLXGRD.OCX"
Begin VB.Form formReconcileTaxHistory 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Reconcile Tax History"
   ClientHeight    =   8430
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   7605
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   8430
   ScaleWidth      =   7605
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.TextBox textPageSize 
      Height          =   330
      Left            =   2790
      TabIndex        =   24
      Top             =   2520
      Width           =   1845
   End
   Begin VB.ComboBox cboDocType 
      Height          =   315
      Left            =   2790
      TabIndex        =   23
      Top             =   2160
      Width           =   1815
   End
   Begin VB.CommandButton buttonReconcileTaxHistory 
      Caption         =   "Reconcile Tax History"
      Default         =   -1  'True
      Height          =   375
      Left            =   2925
      TabIndex        =   27
      Top             =   3120
      Width           =   1725
   End
   Begin VB.CommandButton buttonClose 
      Cancel          =   -1  'True
      Caption         =   "Close"
      Height          =   375
      Left            =   6270
      TabIndex        =   29
      Top             =   7950
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
      Height          =   2985
      Left            =   960
      TabIndex        =   9
      Top             =   0
      Width           =   5685
      Begin VB.ComboBox cboDocStatus 
         Height          =   315
         Left            =   1830
         TabIndex        =   18
         Top             =   1800
         Width           =   1815
      End
      Begin VB.TextBox textEndDate 
         Height          =   330
         Left            =   1830
         TabIndex        =   17
         Top             =   1440
         Width           =   1845
      End
      Begin VB.TextBox textStartDate 
         Height          =   330
         Left            =   1830
         TabIndex        =   16
         Top             =   1080
         Width           =   1845
      End
      Begin VB.TextBox textCompanyCode 
         Height          =   330
         Left            =   1830
         TabIndex        =   10
         Top             =   360
         Width           =   1845
      End
      Begin VB.TextBox textLastDocCode 
         Height          =   330
         Left            =   1830
         TabIndex        =   12
         Top             =   720
         Width           =   1845
      End
      Begin VB.Label Label11 
         Alignment       =   1  'Right Justify
         Caption         =   "Start Date:"
         Height          =   300
         Left            =   360
         TabIndex        =   22
         Top             =   1080
         Width           =   1365
      End
      Begin VB.Label Label10 
         Alignment       =   1  'Right Justify
         Caption         =   "Doc Status:"
         Height          =   225
         Left            =   360
         TabIndex        =   21
         Top             =   1800
         Width           =   1365
      End
      Begin VB.Label Label9 
         Alignment       =   1  'Right Justify
         Caption         =   "End Date:"
         Height          =   300
         Left            =   360
         TabIndex        =   20
         Top             =   1440
         Width           =   1365
      End
      Begin VB.Label Label8 
         Alignment       =   1  'Right Justify
         Caption         =   "Page Size:"
         Height          =   300
         Left            =   360
         TabIndex        =   19
         Top             =   2520
         Width           =   1365
      End
      Begin VB.Label Label2 
         Alignment       =   1  'Right Justify
         Caption         =   "Company Code:"
         Height          =   300
         Left            =   390
         TabIndex        =   14
         Top             =   390
         Width           =   1365
      End
      Begin VB.Label Label3 
         Alignment       =   1  'Right Justify
         Caption         =   "Last Doc Code:"
         Height          =   300
         Left            =   390
         TabIndex        =   13
         Top             =   750
         Width           =   1365
      End
      Begin VB.Label Label1 
         Alignment       =   1  'Right Justify
         Caption         =   "Doc Type:"
         Height          =   225
         Left            =   370
         TabIndex        =   11
         Top             =   2160
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
      Height          =   4155
      Left            =   120
      TabIndex        =   0
      Top             =   3600
      Width           =   7365
      Begin VB.CommandButton buttonViewGetTaxResult 
         Caption         =   "View Selected"
         Enabled         =   0   'False
         Height          =   345
         Left            =   5730
         TabIndex        =   28
         Top             =   3720
         Width           =   1395
      End
      Begin MSFlexGridLib.MSFlexGrid gridResults 
         CausesValidation=   0   'False
         Height          =   1575
         Left            =   150
         TabIndex        =   1
         Top             =   2040
         Width           =   7005
         _ExtentX        =   12356
         _ExtentY        =   2778
         _Version        =   393216
         Cols            =   12
         FixedCols       =   0
         ScrollTrack     =   -1  'True
         SelectionMode   =   1
         AllowUserResizing=   1
      End
      Begin VB.Label lblRecordCount 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1380
         TabIndex        =   26
         Top             =   1440
         Width           =   3615
      End
      Begin VB.Label Label12 
         Alignment       =   1  'Right Justify
         Caption         =   "Record Count:"
         Height          =   300
         Left            =   100
         TabIndex        =   25
         Top             =   1440
         Width           =   1245
      End
      Begin VB.Label Label4 
         Alignment       =   1  'Right Justify
         Caption         =   "Result Code:"
         Height          =   300
         Left            =   90
         TabIndex        =   8
         Top             =   360
         Width           =   1245
      End
      Begin VB.Label Label5 
         Alignment       =   1  'Right Justify
         Caption         =   "Result Msg:"
         Height          =   300
         Left            =   90
         TabIndex        =   7
         Top             =   720
         Width           =   1245
      End
      Begin VB.Label lblResultCode 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1350
         TabIndex        =   6
         Top             =   360
         Width           =   3615
      End
      Begin VB.Label lblResultMsg 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1350
         TabIndex        =   5
         Top             =   720
         Width           =   3615
      End
      Begin VB.Label Label6 
         Alignment       =   1  'Right Justify
         Caption         =   "Last Doc Code:"
         Height          =   300
         Left            =   90
         TabIndex        =   4
         Top             =   1080
         Width           =   1245
      End
      Begin VB.Label lblLastDocCode 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1350
         TabIndex        =   3
         Top             =   1080
         Width           =   5895
      End
      Begin VB.Label Label7 
         Caption         =   "GetTaxResults"
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
         Left            =   180
         TabIndex        =   2
         Top             =   1800
         Width           =   1665
      End
   End
   Begin VB.Label lblStatus 
      ForeColor       =   &H00008000&
      Height          =   285
      Left            =   90
      TabIndex        =   15
      Top             =   8010
      Width           =   3585
   End
End
Attribute VB_Name = "formReconcileTaxHistory"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Enum ResultsColumns
    colResult_ResultCode = 0
    colResult_ResultMsg
    colResult_DocStatus
    colResult_DocStatusDate
    colResult_Reconciled
    colResult_TotalBase
    colResult_TotalAmount
    colResult_TotalTax
End Enum

Private oReconcileTaxHistoryResult As ReconcileTaxHistoryResult
Private oGetTaxResults As GetTaxResults

Private Sub buttonClose_Click()

    Set oGetTaxResults = Nothing
    
    Unload Me
End Sub

Private Sub buttonReconcileTaxHistory_Click()

    Dim oReconcileTaxHistoryRequest As ReconcileTaxHistoryRequest
    
    On Error GoTo errReconcileTaxHistory
    
    '#################################################################
    '### 1st WE CREATE THE REQUEST OBJECT WITH DATA FOR OUR SEARCH ###
    '#################################################################
    Set oReconcileTaxHistoryRequest = New ReconcileTaxHistoryRequest
    
    '###########################################################
    '### 2nd WE LOAD THE REQUEST-LEVEL DATA INTO THE REQUEST ###
    '###########################################################
    oReconcileTaxHistoryRequest.CompanyCode = textCompanyCode.text
    oReconcileTaxHistoryRequest.LastDocCode = textLastDocCode.text
    oReconcileTaxHistoryRequest.DocStatus = Val(cboDocStatus.List(cboDocStatus.ListIndex))
    oReconcileTaxHistoryRequest.DocType = Val(cboDocType.List(cboDocType.ListIndex))
    
    If (textPageSize.text <> "") Then
        oReconcileTaxHistoryRequest.PageSize = Val(textPageSize.text)
    End If
    If (IsDate(textStartDate.text)) Then
       oReconcileTaxHistoryRequest.StartDate = (textStartDate.text) 'CDate
    End If
    If (IsDate(textEndDate.text)) Then
       oReconcileTaxHistoryRequest.EndDate = (textEndDate.text)
    End If
    
    '########################################################################################################
    '### 3rd WE INVOKE THE RECONCILETAXHISTORY() METHOD OF THE TAXSVC OBJECT AND GET BACK A RESULT OBJECT ###
    '########################################################################################################
    Dim oTaxSvc As TaxSvc
    
    PreMethodCall lblStatus
    
    Set oTaxSvc = New TaxSvc
    formMain.SetConfig oTaxSvc              'set the Url and Security configuration
       
    
    Set oReconcileTaxHistoryResult = oTaxSvc.ReconcileTaxHistory(oReconcileTaxHistoryRequest)
    
    PostMethodCall lblStatus
    
    '#####################################
    '### 4th WE READ THE RESULT OBJECT ###
    '#####################################
    lblResultCode.Caption = TranslateSeverity(oReconcileTaxHistoryResult.ResultCode)
    SetMessageLabelText lblResultMsg, oReconcileTaxHistoryResult
    lblLastDocCode.Caption = oReconcileTaxHistoryResult.LastDocCode
    lblRecordCount.Caption = oReconcileTaxHistoryResult.RecordCount
    Set oGetTaxResults = oReconcileTaxHistoryResult.GetTaxResults
    
    FillResultsData
    
    Set oReconcileTaxHistoryRequest = Nothing
    Set oTaxSvc = Nothing
    
    Exit Sub
    
errReconcileTaxHistory:
    lblStatus.Caption = ""
    Screen.MousePointer = vbDefault
    ShowError Err
End Sub

Private Sub buttonViewGetTaxResult_Click()

    Dim lIndex As Long
    Dim oGetTaxResult As GetTaxResult
    Dim frm As formGetTaxResult
    
    If (gridResults.Row > 0) Then
        lIndex = gridResults.Row - 1
        
        Set oGetTaxResult = oGetTaxResults.Item(lIndex)
        Set frm = New formGetTaxResult
        Set frm.oGetTaxResult = oGetTaxResult
        frm.Show vbModal, Me
    End If
    
End Sub

Private Sub Form_Load()

    FillResultsHeader
    FillDocStatusCombo
    FillDocTypeCombo
    
    textCompanyCode.text = "DEFAULT"
    cboDocStatus.ListIndex = 0
    cboDocType.ListIndex = 0
End Sub

Private Sub FillResultsData()

    '#################################################################################################
    '### ITERATE THROUGH ALL GETTAXRESULT OBJECTS RETURNED IN THE RECONCILETAXHISTORYRESULT OBJECT ###
    '#################################################################################################
    Dim oGetTaxResult As GetTaxResult
    Dim lRow As Long
    
    gridResults.Rows = 1
    
    For Each oGetTaxResult In oGetTaxResults
        With gridResults
            .Rows = .Rows + 1
            lRow = .Rows - 1
            
            .TextMatrix(lRow, colResult_ResultCode) = TranslateSeverity(oGetTaxResult.ResultCode)
            .TextMatrix(lRow, colResult_ResultMsg) = CStr(oGetTaxResult.Messages.Count) & " messages"
            .TextMatrix(lRow, colResult_DocStatus) = TranslateDocStatus(oGetTaxResult.DocStatus)
            .TextMatrix(lRow, colResult_DocStatusDate) = oGetTaxResult.TimeStamp
            .TextMatrix(lRow, colResult_Reconciled) = oGetTaxResult.Reconciled
            .TextMatrix(lRow, colResult_TotalBase) = oGetTaxResult.TotalTaxable
            .TextMatrix(lRow, colResult_TotalAmount) = oGetTaxResult.TotalAmount
            .TextMatrix(lRow, colResult_TotalTax) = oGetTaxResult.TotalTax
        End With
    Next oGetTaxResult
    
    buttonViewGetTaxResult.Enabled = (oGetTaxResults.Count > 0)
    
End Sub

Private Sub FillDocStatusCombo()

    cboDocStatus.AddItem DocStatus.DocStatus_Saved & " : Saved"
    cboDocStatus.AddItem DocStatus.DocStatus_Posted & " : Posted"
    cboDocStatus.AddItem DocStatus.DocStatus_Committed & " : Committed"
    cboDocStatus.AddItem DocStatus.DocStatus_Cancelled & " : Cancelled"
    
End Sub

Private Sub FillDocTypeCombo()

    cboDocType.AddItem documentType.DocumentType_SalesInvoice & " : SalesInvoice"
    cboDocType.AddItem documentType.DocumentType_PurchaseInvoice & " : PurchaseInvoice"
    cboDocType.AddItem documentType.DocumentType_ReturnInvoice & " : ReturnInvoice"
    cboDocType.AddItem documentType.DocumentType_Any & " : Any"
    
End Sub

Private Sub FillResultsHeader()

    '#####################
    '### UI SETUP CODE ###
    '#####################
    With gridResults
        .Rows = 1
        
        .TextMatrix(0, colResult_ResultCode) = "Result Code"
        .TextMatrix(0, colResult_ResultMsg) = "Result Msg"
        .TextMatrix(0, colResult_DocStatus) = "Doc Status"
        .TextMatrix(0, colResult_DocStatusDate) = "Doc Status Date"
        .TextMatrix(0, colResult_Reconciled) = "Reconciled"
        .TextMatrix(0, colResult_TotalBase) = "Total Base"
        .TextMatrix(0, colResult_TotalAmount) = "Total Amount"
        .TextMatrix(0, colResult_TotalTax) = "Total Tax"
   
    
        .ColWidth(colResult_ResultCode) = .ColWidth(colResult_ResultCode) + (15 * Screen.TwipsPerPixelX)
        .ColWidth(colResult_ResultMsg) = .ColWidth(colResult_ResultMsg) + (15 * Screen.TwipsPerPixelX)
        .ColWidth(colResult_DocStatus) = .ColWidth(colResult_DocStatus) + (20 * Screen.TwipsPerPixelX)
        .ColWidth(colResult_DocStatusDate) = .ColWidth(colResult_DocStatusDate) + (15 * Screen.TwipsPerPixelX)
        .ColWidth(colResult_Reconciled) = .ColWidth(colResult_Reconciled) + (15 * Screen.TwipsPerPixelX)
        .ColWidth(colResult_TotalBase) = .ColWidth(colResult_TotalBase) + (15 * Screen.TwipsPerPixelX)
        .ColWidth(colResult_TotalAmount) = .ColWidth(colResult_TotalAmount) + (15 * Screen.TwipsPerPixelX)
        .ColWidth(colResult_TotalTax) = .ColWidth(colResult_TotalTax) + (15 * Screen.TwipsPerPixelX)
    End With

End Sub

Private Sub lblResultMsg_Click()
    Dim frm As MessagesForm
    Set frm = New MessagesForm
    frm.Init oReconcileTaxHistoryResult.Messages
    frm.Show vbModal, Me
End Sub