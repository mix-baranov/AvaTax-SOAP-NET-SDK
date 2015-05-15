VERSION 5.00
Begin VB.Form formMain 
   Caption         =   "SDK Address Sample"
   ClientHeight    =   8070
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   7350
   LinkTopic       =   "Form1"
   ScaleHeight     =   8070
   ScaleWidth      =   7350
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton buttonClose 
      Caption         =   "Close"
      Height          =   345
      Left            =   6270
      TabIndex        =   32
      Top             =   7590
      Width           =   1005
   End
   Begin VB.Frame frameValidateMethod 
      Caption         =   "Validate Method"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   3015
      Left            =   90
      TabIndex        =   23
      Top             =   4320
      Width           =   7155
      Begin VB.TextBox textLine1 
         Height          =   330
         Left            =   2130
         TabIndex        =   16
         Top             =   360
         Width           =   3585
      End
      Begin VB.TextBox textLine2 
         Height          =   330
         Left            =   2130
         TabIndex        =   17
         Top             =   720
         Width           =   3585
      End
      Begin VB.TextBox textLine3 
         Height          =   330
         Left            =   2130
         TabIndex        =   18
         Top             =   1080
         Width           =   3585
      End
      Begin VB.TextBox textCity 
         Height          =   330
         Left            =   2130
         TabIndex        =   19
         Top             =   1440
         Width           =   2715
      End
      Begin VB.TextBox textState 
         Height          =   330
         Left            =   2130
         MaxLength       =   2
         TabIndex        =   20
         Top             =   1800
         Width           =   645
      End
      Begin VB.TextBox textZip 
         Height          =   330
         Left            =   2130
         MaxLength       =   10
         TabIndex        =   21
         Top             =   2160
         Width           =   1605
      End
      Begin VB.TextBox textCountry 
         Height          =   330
         Left            =   2130
         TabIndex        =   22
         Top             =   2520
         Width           =   1605
      End
      Begin VB.CommandButton buttonValidate 
         Caption         =   "Validate Address"
         Height          =   345
         Left            =   4245
         TabIndex        =   24
         Top             =   2520
         Width           =   1515
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Line 1:"
         Height          =   300
         Index           =   1
         Left            =   810
         TabIndex        =   31
         Top             =   405
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Line 2:"
         Height          =   300
         Index           =   2
         Left            =   810
         TabIndex        =   30
         Top             =   780
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Line 3:"
         Height          =   300
         Index           =   3
         Left            =   810
         TabIndex        =   29
         Top             =   1140
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "City:"
         Height          =   300
         Index           =   4
         Left            =   810
         TabIndex        =   28
         Top             =   1485
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "State:"
         Height          =   300
         Index           =   5
         Left            =   810
         TabIndex        =   27
         Top             =   1860
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Zip:"
         Height          =   300
         Index           =   6
         Left            =   810
         TabIndex        =   26
         Top             =   2205
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Country:"
         Height          =   300
         Index           =   7
         Left            =   810
         TabIndex        =   25
         Top             =   2580
         Width           =   1275
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
      Left            =   60
      TabIndex        =   15
      Top             =   3120
      Width           =   7155
      Begin VB.CommandButton buttonAbout 
         Caption         =   "About Adapter"
         Height          =   345
         Left            =   4598
         TabIndex        =   14
         Top             =   390
         Width           =   1275
      End
      Begin VB.CommandButton buttonIsAuthorized 
         Caption         =   "Is Authorized?"
         Height          =   345
         Left            =   3038
         TabIndex        =   13
         Top             =   390
         Width           =   1275
      End
      Begin VB.CommandButton buttonPing 
         Caption         =   "Ping Service"
         Height          =   345
         Left            =   1598
         TabIndex        =   12
         Top             =   390
         Width           =   1155
      End
   End
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
      Height          =   2835
      Left            =   60
      TabIndex        =   0
      Top             =   90
      Width           =   7155
      Begin VB.CommandButton buttonSettings 
         Caption         =   "Settings"
         Height          =   345
         Left            =   5850
         TabIndex        =   1
         Top             =   2310
         Width           =   1155
      End
      Begin VB.Label Label1 
         Alignment       =   1  'Right Justify
         Caption         =   "Url:"
         Height          =   300
         Left            =   60
         TabIndex        =   11
         Top             =   300
         Width           =   405
      End
      Begin VB.Label Label2 
         Alignment       =   1  'Right Justify
         Caption         =   "Account:"
         Height          =   300
         Left            =   60
         TabIndex        =   10
         Top             =   960
         Width           =   825
      End
      Begin VB.Label Label4 
         Alignment       =   1  'Right Justify
         Caption         =   "License:"
         Height          =   300
         Left            =   60
         TabIndex        =   9
         Top             =   1320
         Width           =   825
      End
      Begin VB.Label Label3 
         Alignment       =   1  'Right Justify
         Caption         =   "UserName:"
         Height          =   300
         Left            =   60
         TabIndex        =   8
         Top             =   1680
         Width           =   825
      End
      Begin VB.Label Label5 
         Alignment       =   1  'Right Justify
         Caption         =   "Password:"
         Height          =   300
         Left            =   60
         TabIndex        =   7
         Top             =   2040
         Width           =   825
      End
      Begin VB.Label lblUrl 
         Caption         =   "http://"
         ForeColor       =   &H00C00000&
         Height          =   405
         Left            =   570
         TabIndex        =   6
         Top             =   300
         Width           =   5145
         WordWrap        =   -1  'True
      End
      Begin VB.Label lblAccount 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   960
         TabIndex        =   5
         Top             =   960
         Width           =   2190
      End
      Begin VB.Label lblKey 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   960
         TabIndex        =   4
         Top             =   1320
         Width           =   2190
      End
      Begin VB.Label lblUserName 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   960
         TabIndex        =   3
         Top             =   1680
         Width           =   3975
      End
      Begin VB.Label lblPassword 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   960
         TabIndex        =   2
         Top             =   2040
         Width           =   2190
      End
   End
   Begin VB.Label lblStatus 
      ForeColor       =   &H00008000&
      Height          =   285
      Left            =   90
      TabIndex        =   33
      Top             =   7620
      Width           =   6105
   End
End
Attribute VB_Name = "formMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Public Url As String
Public ViaUrl As String
Public Account As String
Public Key As String
Public UserName As String
Public Password As String
Public loadSettingsFromConfig As Boolean

Private Sub buttonAbout_Click()

    Dim oAddressSvc As AddressSvc
    
    On Error GoTo errAbout
    
    Screen.MousePointer = vbHourglass
    
    Set oAddressSvc = New AddressSvc
    
    MsgBox "About (Version): " & oAddressSvc.Profile.Adapter, vbInformation, "About Adapter"
            
    Screen.MousePointer = vbDefault
    Exit Sub
    
errAbout:
    Screen.MousePointer = vbDefault
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
End Sub

Private Sub buttonPing_Click()

    Dim oAddressSvc As AddressSvc
    Dim oResult As PingResult

    On Error GoTo errPing
    
    Screen.MousePointer = vbHourglass
    
    Set oAddressSvc = New AddressSvc
    SetConfig oAddressSvc
    
    Set oResult = oAddressSvc.Ping("")
    If (oResult.ResultCode >= SeverityLevel.SeverityLevel_Error) Then
        MsgBox oResult.Messages(0).Summary, vbCritical, "Ping Result"
    Else
    MsgBox "Result Code: " & TranslateSeverity(oResult.ResultCode) & vbCrLf & _
            "# Messages: " & oResult.Messages.Count & vbCrLf & _
            "Service Version: " & oResult.Version, vbInformation, "Ping Result"
    End If
            
    Screen.MousePointer = vbDefault
    Exit Sub
    
errPing:
    Screen.MousePointer = vbDefault
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

Private Sub buttonValidate_Click()

    Dim oAddress As Address
    
    On Error GoTo errValidate
    
    '##############################################################################
    '### 1st WE CREATE THE REQUEST OBJECT FOR THE ADDRESS THAT NEEDS VALIDATION ###
    '##############################################################################
    Set oAddress = New Address
    oAddress.Line1 = textLine1.text
    oAddress.Line2 = textLine2.text
    oAddress.Line3 = textLine3.text
    oAddress.City = textCity.text
    oAddress.Region = textState.text
    oAddress.PostalCode = textZip.text
    oAddress.Country = textCountry.text
    
    '#################################################################################################
    '### 2nd WE INVOKE THE VALIDATE() METHOD OF THE ADDRESSSVC OBJECT AND GET BACK A RESULT OBJECT ###
    '#################################################################################################
    Dim oAddressSvc As AddressSvc
    Dim oRequest As ValidateRequest
    Dim oResult As ValidateResult
    
    PreMethodCall lblStatus
    
    Set oAddressSvc = New AddressSvc
    SetConfig oAddressSvc   'set user-defined config data
    
    Set oRequest = New ValidateRequest
    Set oRequest.Address = oAddress
    oRequest.TextCase = TextCase_Upper
    
    Set oResult = oAddressSvc.Validate(oRequest)
    
    PostMethodCall lblStatus
    
    '#######################################
    '### 3rd WE REVIEW THE RESULT OBJECT ###
    '#######################################
    Dim frm As formResults
    Set frm = New formResults
    
    Set frm.oValidateResult = oResult
    frm.Show vbModal, Me
    
    Exit Sub
    
errValidate:
    Screen.MousePointer = vbDefault
    ShowError Err
End Sub

Private Sub LoadConfig()

    Dim oAddressSvc As AddressSvc
    
    Set oAddressSvc = New AddressSvc
    
    Url = oAddressSvc.Configuration.Url
    Account = oAddressSvc.Configuration.Security.Account
    Key = oAddressSvc.Configuration.Security.License
    UserName = oAddressSvc.Configuration.Security.UserName
    Password = oAddressSvc.Configuration.Security.Password
    
End Sub

Public Sub SetConfig(svc As AddressSvc)

        svc.Profile.Client = "AdapterSampleCode,1.0"
        
        If (Len(Url) > 0) Then
            svc.Configuration.Url = Url
        End If
        If (Len(ViaUrl) > 0) Then
            svc.Configuration.ViaUrl = ViaUrl
        End If
        
        'this could be null if we haven't set the plain-text via the Security dialog
        If (Account <> "") Then
            svc.Configuration.Security.Account = Account
        End If
        'this could be null if we haven't set the plain-text via the Security dialog
        If (Key <> "") Then
            svc.Configuration.Security.License = Key
        End If
        
        svc.Configuration.Security.UserName = UserName
        'write as plain text
        svc.Configuration.Security.Password = Password
    
End Sub

Private Sub Form_Load()

    On Error GoTo errFormLoad
    
    loadSettingsFromConfig = True
    LoadConfig
    
    SetUrlLabels
    SetSecurityLabels

    textLine1.text = "435 Ericksen Ave NE"
    textLine2.text = ""
    textCity.text = "Bainbridge Island"
    textState.text = "WA"
    textZip.text = "98110"
    textCountry.text = "US"
    
    'EnableDisableTaxServiceMethods (False)
    Exit Sub
    
errFormLoad:
    ShowError Err
End Sub

Private Sub SetUrlLabels()
    lblUrl.Caption = Url
End Sub

Private Sub SetSecurityLabels()

    If loadSettingsFromConfig Then
     
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
            loadSettingsFromConfig = False
         End If
    End If
            
    lblAccount.Caption = Account
    lblKey.Caption = IIf(Key = "" Or Key = Null, "", "{ encrypted }")
    
    lblUserName.Caption = UserName 'oTaxSvc.Configuration.Security.GetDecryptedText(UserName)
    lblPassword.Caption = IIf(Password = "" Or Password = Null, "", "{ encrypted }")

End Sub

Public Sub EnableDisableTaxServiceMethods(flag As Boolean)
    frameValidateMethod.Enabled = flag
    buttonValidate.Enabled = flag
End Sub
