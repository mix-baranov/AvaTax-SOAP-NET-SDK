VERSION 5.00
Begin VB.Form formMain 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "SDK Tax Sample"
   ClientHeight    =   7560
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   7470
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   ScaleHeight     =   7560
   ScaleWidth      =   7470
   StartUpPosition =   2  'CenterScreen
   Begin VB.Frame Frame3 
      Caption         =   "Configuration"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   2895
      Left            =   120
      TabIndex        =   5
      Top             =   0
      Width           =   7155
      Begin VB.CommandButton buttonSettings 
         Caption         =   "Settings"
         Height          =   345
         Left            =   5850
         TabIndex        =   0
         Top             =   2400
         Width           =   1155
      End
      Begin VB.Label lblPassword 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1080
         TabIndex        =   16
         Top             =   1980
         Width           =   2190
      End
      Begin VB.Label lblUserName 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1080
         TabIndex        =   15
         Top             =   1620
         Width           =   2895
      End
      Begin VB.Label lblKey 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1080
         TabIndex        =   14
         Top             =   1260
         Width           =   2190
      End
      Begin VB.Label lblAccount 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1080
         TabIndex        =   13
         Top             =   900
         Width           =   2190
      End
      Begin VB.Label lblUrl 
         Caption         =   "http://"
         ForeColor       =   &H00C00000&
         Height          =   405
         Left            =   570
         TabIndex        =   12
         Top             =   300
         Width           =   5145
         WordWrap        =   -1  'True
      End
      Begin VB.Label Label5 
         Alignment       =   1  'Right Justify
         Caption         =   "Password:"
         Height          =   300
         Left            =   180
         TabIndex        =   11
         Top             =   1980
         Width           =   825
      End
      Begin VB.Label Label3 
         Alignment       =   1  'Right Justify
         Caption         =   "UserName:"
         Height          =   300
         Left            =   180
         TabIndex        =   10
         Top             =   1620
         Width           =   825
      End
      Begin VB.Label Label4 
         Alignment       =   1  'Right Justify
         Caption         =   "License:"
         Height          =   300
         Left            =   180
         TabIndex        =   9
         Top             =   1260
         Width           =   825
      End
      Begin VB.Label Label2 
         Alignment       =   1  'Right Justify
         Caption         =   "Account:"
         Height          =   300
         Left            =   180
         TabIndex        =   8
         Top             =   900
         Width           =   825
      End
      Begin VB.Label Label1 
         Alignment       =   1  'Right Justify
         Caption         =   "Url:"
         Height          =   300
         Left            =   60
         TabIndex        =   7
         Top             =   300
         Width           =   405
      End
   End
   Begin VB.Frame Frame2 
      Caption         =   "Generic Methods"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   1005
      Left            =   120
      TabIndex        =   2
      Top             =   3000
      Width           =   7155
      Begin VB.CommandButton buttonPing 
         Caption         =   "Ping Service"
         Height          =   345
         Left            =   1598
         TabIndex        =   3
         Top             =   390
         Width           =   1155
      End
      Begin VB.CommandButton buttonIsAuthorized 
         Caption         =   "Is Authorized?"
         Height          =   345
         Left            =   3038
         TabIndex        =   4
         Top             =   390
         Width           =   1275
      End
      Begin VB.CommandButton buttonAbout 
         Caption         =   "About Adapter"
         Height          =   345
         Left            =   4598
         TabIndex        =   6
         Top             =   390
         Width           =   1275
      End
   End
   Begin VB.Frame frameTaxServiceMethods 
      Caption         =   "Tax Service Methods"
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
      Left            =   120
      TabIndex        =   1
      Top             =   4200
      Width           =   7155
      Begin VB.CommandButton buttonMethods 
         Caption         =   "Adjust Tax"
         Height          =   345
         Index           =   7
         Left            =   3720
         TabIndex        =   21
         Top             =   450
         Width           =   2295
      End
      Begin VB.CommandButton buttonMethods 
         Caption         =   "Get Tax History"
         Height          =   345
         Index           =   4
         Left            =   3720
         TabIndex        =   22
         Top             =   900
         Width           =   2295
      End
      Begin VB.CommandButton buttonMethods 
         Caption         =   "Get Tax"
         Height          =   345
         Index           =   0
         Left            =   1080
         TabIndex        =   17
         Top             =   450
         Width           =   2295
      End
      Begin VB.CommandButton buttonMethods 
         Caption         =   "Reconcile Tax History"
         Height          =   345
         Index           =   6
         Left            =   3720
         TabIndex        =   23
         Top             =   1350
         Width           =   2295
      End
      Begin VB.CommandButton buttonMethods 
         Caption         =   "Commit Tax"
         Height          =   345
         Index           =   3
         Left            =   1080
         TabIndex        =   19
         Top             =   1350
         Width           =   2295
      End
      Begin VB.CommandButton buttonMethods 
         Caption         =   "Post Tax"
         Height          =   345
         Index           =   2
         Left            =   1080
         TabIndex        =   18
         Top             =   900
         Width           =   2295
      End
      Begin VB.CommandButton buttonMethods 
         Caption         =   "Cancel Tax"
         Height          =   345
         Index           =   1
         Left            =   1080
         TabIndex        =   20
         Top             =   1800
         Width           =   2295
      End
   End
   Begin VB.CommandButton buttonClose 
      Caption         =   "Close"
      Height          =   345
      Left            =   6210
      TabIndex        =   24
      Top             =   6960
      Width           =   1125
   End
End
Attribute VB_Name = "formMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Enum Buttons
    btnGetTax = 0
    btnCancelTax
    btnPostTax
    btnCommitTax
    btnGetTaxHistory
    btnSearchTaxHistory
    btnReconcileTaxHistory
    btnAdjustTax
End Enum

Public Url As String
Public ViaUrl As String
Public Account As String
Public Key As String
Public UserName As String
Public Password As String
Public LoadSettingsFromConfig As Boolean

Private Sub buttonAbout_Click()

    Dim oTaxSvc As TaxSvc
    
    On Error GoTo errAbout
    
    Me.MousePointer = vbHourglass
    
    Set oTaxSvc = New TaxSvc
    
    MsgBox "About (Version): " & oTaxSvc.Profile.Adapter, vbInformation, "About Adapter"
            
    Me.MousePointer = vbDefault
    Exit Sub
    
errAbout:
    Me.MousePointer = vbDefault
    ShowError Err
End Sub

Private Sub buttonClose_Click()
    Unload Me
    End
End Sub

Private Sub buttonIsAuthorized_Click()
    Dim frm As Form
    Set frm = New formOperationAuthorization
    frm.Show vbModal, Me
            
'    Dim oTaxSvc As TaxSvc
'    Dim oResult As IsAuthorizedResult
'
'    On Error GoTo errIsAuthorized
'
'    Me.MousePointer = vbHourglass
'
'    Set oTaxSvc = New TaxSvc
'    SetConfig oTaxSvc
'
'    Set oResult = oTaxSvc.IsAuthorized("")
'    MsgBox "Result Code: " & TranslateSeverity(oResult.ResultCode) & vbCrLf & _
'            "# Messages: " & oResult.Messages.Count & vbCrLf & _
'            "Expires: " & oResult.Expires & vbCrLf & _
'            "Operations: " & oResult.operations, vbInformation, "IsAuthorized Result"
'
'    Me.MousePointer = vbDefault
'    Exit Sub
'
'errIsAuthorized:
'    Me.MousePointer = vbDefault
'    ShowError Err
End Sub

Private Sub buttonMethods_Click(index As Integer)

    On Error GoTo errMethodsClick
    
    Dim frm As Form
    
    Select Case index
        Case btnGetTax
            Set frm = New formGetTax
        
        Case btnCancelTax
            Set frm = New formCancelTax
    
        Case btnPostTax
            Set frm = New formPostTax
            
        Case btnCommitTax
            Set frm = New formCommitTax
            
        Case btnGetTaxHistory
            Set frm = New formGetTaxHistory
            
        Case btnReconcileTaxHistory
            Set frm = New formReconcileTaxHistory
            
        Case btnAdjustTax
            Set frm = New formAdjustTax
            
    End Select
    frm.Show vbModal, Me
    
    Exit Sub
    
errMethodsClick:
    Screen.MousePointer = vbDefault
    ShowError Err
End Sub

Private Sub buttonPing_Click()

    Dim oTaxSvc As TaxSvc
    Dim oResult As PingResult

    On Error GoTo errPing
    
    Me.MousePointer = vbHourglass
    
    Set oTaxSvc = New TaxSvc
    SetConfig oTaxSvc
    
    Set oResult = oTaxSvc.Ping("")
    If (oResult.ResultCode >= SeverityLevel.SeverityLevel_Error) Then
        MsgBox oResult.Messages(0).Summary, vbCritical, "Ping Result"
    Else
        MsgBox "Result Code: " & TranslateSeverity(oResult.ResultCode) & vbCrLf & _
            "# Messages: " & oResult.Messages.Count & vbCrLf & _
            "Service Version: " & oResult.Version, vbInformation, "Ping Result"
    End If
    Me.MousePointer = vbDefault
    Exit Sub
    
errPing:
    Me.MousePointer = vbDefault
    ShowError Err
End Sub

Private Sub buttonSettings_Click()
    Dim frm As formSettings
    
    Set frm = New formSettings
    frm.Show vbModal, Me
    Set frm = Nothing
    
    'LoadConfig
    SetUrlLabels
    SetSecurityLabels
    
End Sub

Private Sub LoadConfig()

    Dim oTaxSvc As TaxSvc
    Set oTaxSvc = New TaxSvc
    
    Url = oTaxSvc.Configuration.Url
    'ViaUrl = oTaxSvc.Configuration.ViaUrl
    Account = oTaxSvc.Configuration.Security.Account
    Key = oTaxSvc.Configuration.Security.License
    UserName = oTaxSvc.Configuration.Security.UserName
    Password = oTaxSvc.Configuration.Security.Password
    
End Sub

Public Sub SetConfig(svc As TaxSvc)

        svc.Profile.Client = "AdapterSampleCode,1.0"
        
        If (Len(Trim$(Url)) > 0) Then
            svc.Configuration.Url = Url
        End If
        If (Len(Trim$(ViaUrl)) > 0) Then
            svc.Configuration.ViaUrl = ViaUrl
        End If
        If (Len(Trim$(Account)) > 0) Then
            svc.Configuration.Security.Account = Account
        End If
        If (Len(Trim$(Key)) > 0) Then
            svc.Configuration.Security.License = Key
        End If
        svc.Configuration.Security.UserName = UserName
        'write as plain text
        svc.Configuration.Security.Password = Password
    
End Sub

Private Sub Form_Load()

    On Error GoTo errFormLoad

    LoadSettingsFromConfig = True
    LoadConfig
    
    SetUrlLabels
    SetSecurityLabels
    
    'EnableDisableTaxServiceMethods (False)
    Exit Sub
    
errFormLoad:
    ShowError Err
End Sub

Private Sub SetUrlLabels()
    lblUrl.Caption = Url
End Sub

Private Sub SetSecurityLabels()

	If LoadSettingsFromConfig Then
     
        Dim doc As MSXML2.DOMDocument
        Set doc = New MSXML2.DOMDocument
        
        Dim configFilePath As String
        
        ChDir (App.Path)
        ChDir ("../../../bin")
        configFilePath = CurDir() & "\Avalara.AvaTax.Adapter.dll.config"
        
        If doc.Load(configFilePath) Then
            Dim node As MSXML2.IXMLDOMElement
             
            Set node = doc.selectSingleNode("//RequestSecurity/Account")
            If Not IsNull(node) Then
                Account = node.nodeTypedValue
            End If
            
            Set node = doc.selectSingleNode("//RequestSecurity/License")
            If Not IsNull(node) Then
                Key = node.nodeTypedValue
            End If
            LoadSettingsFromConfig = False
         End If
    End If
    
    lblAccount.Caption = Account
    lblKey.Caption = IIf(Key = "" Or Key = Null, "", "{ encrypted }")
    
    lblUserName.Caption = UserName 'oTaxSvc.Configuration.Security.GetDecryptedText(UserName)
    lblPassword.Caption = IIf(Password = "" Or Password = Null, "", "{ encrypted }")

End Sub

Public Sub EnableDisableTaxServiceMethods(flag As Boolean)
    frameTaxServiceMethods.Enabled = flag
    Dim i As Integer
    
    For i = 0 To 7
        If i <> 5 Then
            buttonMethods(i).Enabled = flag
        End If
    Next i
End Sub
