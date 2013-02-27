VERSION 5.00
Begin VB.Form formOperationAuthorization 
   Caption         =   "Operation Authorization"
   ClientHeight    =   5190
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   5460
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   5190
   ScaleWidth      =   5460
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.CommandButton buttonClose 
      Cancel          =   -1  'True
      Caption         =   "Close"
      Height          =   345
      Left            =   4200
      TabIndex        =   10
      Top             =   4560
      Width           =   1005
   End
   Begin VB.OptionButton optionUserName 
      Caption         =   "By UserName:"
      Height          =   255
      Left            =   510
      TabIndex        =   1
      Top             =   1440
      Width           =   1695
   End
   Begin VB.OptionButton optionAccount 
      Caption         =   "Current Account/User:"
      Height          =   255
      Left            =   510
      TabIndex        =   0
      Top             =   840
      Value           =   -1  'True
      Width           =   2055
   End
   Begin VB.TextBox textPassword 
      Enabled         =   0   'False
      Height          =   330
      IMEMode         =   3  'DISABLE
      Left            =   2040
      PasswordChar    =   "*"
      TabIndex        =   4
      Top             =   2220
      Width           =   2805
   End
   Begin VB.TextBox textUserName 
      Enabled         =   0   'False
      Height          =   330
      Left            =   2040
      TabIndex        =   2
      Top             =   1800
      Width           =   2805
   End
   Begin VB.TextBox textOperations 
      Height          =   930
      Left            =   480
      MultiLine       =   -1  'True
      TabIndex        =   6
      Top             =   3480
      Width           =   4605
   End
   Begin VB.CommandButton buttonAuthorized 
      Caption         =   "Authorized"
      Default         =   -1  'True
      Height          =   345
      Left            =   2640
      TabIndex        =   8
      Top             =   4560
      Width           =   1485
   End
   Begin VB.Label Label1 
      Caption         =   "Use this form to check method(s) given profile can access."
      Height          =   315
      Left            =   120
      TabIndex        =   9
      Top             =   120
      Width           =   4515
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "License/Password:"
      Height          =   300
      Index           =   3
      Left            =   480
      TabIndex        =   7
      Top             =   2280
      Width           =   1515
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "Account/UserName:"
      Height          =   300
      Index           =   2
      Left            =   480
      TabIndex        =   5
      Top             =   1860
      Width           =   1515
   End
   Begin VB.Label lblAddress 
      Caption         =   "Enter the method name(s):       e.g. Validate"
      Height          =   540
      Index           =   4
      Left            =   480
      TabIndex        =   3
      Top             =   3000
      Width           =   2415
   End
End
Attribute VB_Name = "formOperationAuthorization"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim settings As String

Private Sub buttonAuthorized_Click()
    
    If (textOperations.text = "") Then
        MsgBox "Please insert the method name(s).", vbInformation, "Autherization Result"
        textOperations.SetFocus
    Else
        Me.MousePointer = vbHourglass
    
        ApplySettings
        
        Dim oAddressSvc As AddressSvc
        Set oAddressSvc = New AddressSvc
        
        On Error GoTo errIsAuthorized
        
        formMain.SetConfig oAddressSvc
        
        Dim operations As String
        operations = Trim(Replace(textOperations.text, " ", ","))
    
        Dim oIsAuthorizedResult As IsAuthorizedResult
        Set oIsAuthorizedResult = oAddressSvc.IsAuthorized(operations)
            
        If (oIsAuthorizedResult.ResultCode >= SeverityLevel.SeverityLevel_Error) Then
            MsgBox oIsAuthorizedResult.Messages(0).Summary, vbCritical, "IsAuthorized Result"
        Else
            If (oIsAuthorizedResult.operations = "") Then
                MsgBox "User is not authorized to listed method(s).", vbInformation, "Authorization Result"
            Else
                MsgBox "Operation(s) authorized: " + oIsAuthorizedResult.operations, vbInformation, "Authorization Result"
            End If
        End If
    
        RejectChanges (settings)
        Me.MousePointer = vbDefault
    End If
    Exit Sub
    
errIsAuthorized:
    Me.MousePointer = vbDefault
    ShowError Err

End Sub

Private Sub buttonClose_Click()
    RejectChanges (settings)
    Unload Me
End Sub

Private Sub ApplySettings()

If (optionUserName.Value) Then
    formMain.UserName = textUserName.text
    formMain.Password = textPassword.text
End If
     
End Sub

Private Sub Form_Load()
    settings = formMain.UserName + "," + formMain.Password
End Sub

Private Sub optionAccount_Click()
    EnableDisableText
    RejectChanges (settings)
End Sub

Private Sub optionUserName_Click()
    EnableDisableText
End Sub

Private Sub RejectChanges(settings As String)
    If (Len(settings) <= 0) Then
        formMain.UserName = formMain.Password = ""
    Else
        Dim temp() As String
        temp() = Split(settings, ",")
        formMain.UserName = temp(0)
        formMain.Password = temp(1)
    End If
End Sub

Private Sub EnableDisableText()
    textUserName.Enabled = optionUserName.Value
    textPassword.Enabled = optionUserName.Value
End Sub
