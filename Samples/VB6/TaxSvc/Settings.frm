VERSION 5.00
Begin VB.Form formSettings 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Settings"
   ClientHeight    =   4830
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   5625
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   4830
   ScaleWidth      =   5625
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.CommandButton buttonTestConnection 
      Caption         =   "Test Connection"
      Default         =   -1  'True
      Height          =   345
      Left            =   240
      TabIndex        =   13
      Top             =   4320
      Width           =   1485
   End
   Begin VB.TextBox textUrl 
      Height          =   330
      Left            =   1080
      TabIndex        =   1
      Top             =   840
      Width           =   4395
   End
   Begin VB.TextBox textAccount 
      Height          =   330
      Left            =   1440
      TabIndex        =   3
      Top             =   1920
      Width           =   2805
   End
   Begin VB.TextBox textLicense 
      Height          =   330
      IMEMode         =   3  'DISABLE
      Left            =   1440
      PasswordChar    =   "*"
      TabIndex        =   4
      Top             =   2340
      Width           =   2805
   End
   Begin VB.TextBox textUserName 
      Enabled         =   0   'False
      Height          =   330
      Left            =   1440
      TabIndex        =   6
      Top             =   3240
      Width           =   2805
   End
   Begin VB.TextBox textPassword 
      Enabled         =   0   'False
      Height          =   330
      IMEMode         =   3  'DISABLE
      Left            =   1440
      PasswordChar    =   "*"
      TabIndex        =   7
      Top             =   3660
      Width           =   2805
   End
   Begin VB.OptionButton optionAccount 
      Caption         =   "By Account:"
      Height          =   255
      Left            =   510
      TabIndex        =   2
      Top             =   1560
      Value           =   -1  'True
      Width           =   1695
   End
   Begin VB.OptionButton optionUserName 
      Caption         =   "By UserName:"
      Height          =   255
      Left            =   510
      TabIndex        =   5
      Top             =   2880
      Width           =   1695
   End
   Begin VB.CommandButton buttonApply 
      Caption         =   "Apply"
      Enabled         =   0   'False
      Height          =   345
      Left            =   4485
      TabIndex        =   15
      Top             =   4320
      Width           =   1005
   End
   Begin VB.CommandButton buttonCancel 
      Cancel          =   -1  'True
      Caption         =   "Cancel"
      Height          =   345
      Left            =   3405
      TabIndex        =   14
      Top             =   4320
      Width           =   1005
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "Url:"
      Height          =   300
      Index           =   4
      Left            =   240
      TabIndex        =   12
      Top             =   900
      Width           =   795
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "Account:"
      Height          =   300
      Index           =   0
      Left            =   120
      TabIndex        =   11
      Top             =   1980
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "License:"
      Height          =   300
      Index           =   1
      Left            =   120
      TabIndex        =   10
      Top             =   2400
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "UserName:"
      Height          =   300
      Index           =   2
      Left            =   120
      TabIndex        =   9
      Top             =   3300
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "Password:"
      Height          =   300
      Index           =   3
      Left            =   120
      TabIndex        =   8
      Top             =   3720
      Width           =   1275
   End
   Begin VB.Label Label1 
      Caption         =   "Use this form to change the default values loaded from the configuration file Avalara.Adapter.AvaTax.dll.config."
      Height          =   555
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   5715
   End
End
Attribute VB_Name = "formSettings"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim settings As String

Private Sub buttonApply_Click()
    ApplySettings
    'formMain.EnableDisableTaxServiceMethods (True)
    SaveConfigSettings
    
    Unload Me
End Sub

Private Sub buttonCancel_Click()
    Unload Me
End Sub

Private Sub buttonTestConnection_Click()

    On Error GoTo errIsValidConnection
'    Dim settings As String
'    settings = ""
'    If (formMain.Account <> Null Or formMain.Key <> Null Or formMain.UserName <> Null Or formMain.Password <> Null) Then
'        settings = textAccount.text + "," + textLicense.text + "," + textUserName.text + "," + textPassword.text
'    End If
    
    Me.MousePointer = vbHourglass
    
    ApplySettings

    Dim oTaxSvc As TaxSvc
    Set oTaxSvc = New TaxSvc
    formMain.SetConfig oTaxSvc

    Dim oPingResult As PingResult
    Set oPingResult = oTaxSvc.Ping("")
     
    Dim isConnectionSuccess As Boolean
    isConnectionSuccess = False
     
    If (oPingResult.ResultCode >= SeverityLevel.SeverityLevel_Error) Then
        isConnectionSuccess = False
        MsgBox oPingResult.Messages(0).Summary, vbCritical, "Settings Result"
    Else
        Dim oIsAuthorizedResult As IsAuthorizedResult
        Set oIsAuthorizedResult = oTaxSvc.IsAuthorized("GetTax, PostTax, CommitTax, CancelTax, AdjustTax, GetTaxHistory, ReconcileTaxHistory")
    
        If (oIsAuthorizedResult.ResultCode >= SeverityLevel.SeverityLevel_Error) Then
            MsgBox oIsAuthorizedResult.Messages(0).Summary, vbCritical, "Settings Result"
        Else
            isConnectionSuccess = True
            MsgBox "Result Code: " & TranslateSeverity(oIsAuthorizedResult.ResultCode) & vbCrLf & "# Messages: " & oIsAuthorizedResult.Messages.Count & vbCrLf & "Expires: " & oIsAuthorizedResult.Expires & vbCrLf & "Operations: " & oIsAuthorizedResult.operations, vbInformation, "Settings Result"
        End If
    End If
    
    RejectChanges (settings)
    If (isConnectionSuccess) Then
        buttonApply.Enabled = True
    End If
    
    Me.MousePointer = vbDefault
    Exit Sub
    
errIsValidConnection:
    buttonApply.Enabled = False
    Me.MousePointer = vbDefault
    ShowError Err
    
End Sub

Private Sub Form_Load()

    textUrl.text = formMain.Url
    
'    Dim oTaxSvc As TaxSvc
'    Set oTaxSvc = New TaxSvc
    textAccount.text = formMain.Account 'oTaxSvc.Configuration.Security.GetDecryptedText(formMain.Account)
    textLicense.text = formMain.Key 'oTaxSvc.Configuration.Security.GetDecryptedText(formMain.Key)
    textUserName.text = formMain.UserName 'oTaxSvc.Configuration.Security.GetDecryptedText(formMain.UserName)
    textPassword.text = formMain.Password 'oTaxSvc.Configuration.Security.GetDecryptedText(formMain.Password)
    
    If (textUserName.text <> "" Or textPassword.text <> "") Then
        optionUserName.Value = True
        optionUserName_Click
    End If
    
    'If (formMain.Account <> Null Or formMain.Key <> Null Or formMain.UserName <> Null Or formMain.Password <> Null) Then
        settings = textAccount.text + "," + textLicense.text + "," + textUserName.text + "," + textPassword.text
    'End If
    
End Sub

Private Sub optionAccount_Click()

    textAccount.Enabled = optionAccount.Value
    textLicense.Enabled = optionAccount.Value
    textUserName.Enabled = optionUserName.Value
    textPassword.Enabled = optionUserName.Value
    
End Sub

Private Sub optionUserName_Click()

    textAccount.Enabled = optionAccount.Value
    textLicense.Enabled = optionAccount.Value
    textUserName.Enabled = optionUserName.Value
    textPassword.Enabled = optionUserName.Value
    
End Sub

Private Sub ApplySettings()

    formMain.Url = textUrl.text
    If (optionAccount.Value) Then
        formMain.Account = textAccount.text
        formMain.Key = textLicense.text
        formMain.UserName = ""
        formMain.Password = ""
    Else
        formMain.Account = ""
        formMain.Key = ""
        formMain.UserName = textUserName.text
        formMain.Password = textPassword.text
    End If
End Sub

Private Sub SaveConfigSettings()

    Dim oTaxSvc As TaxSvc
    Set oTaxSvc = New TaxSvc
        
    oTaxSvc.Configuration.Url = formMain.Url
    'oTaxSvc.Configuration.ViaUrl = formMain.ViaUrl

    oTaxSvc.Configuration.Security.Account = formMain.Account
    oTaxSvc.Configuration.Security.License = formMain.Key
    oTaxSvc.Configuration.Security.UserName = formMain.UserName
    oTaxSvc.Configuration.Security.Password = formMain.Password
    
    'oTaxSvc.Configuration.Save
End Sub

Private Sub RejectChanges(settings As String)
    If (Len(settings) <= 0) Then
        formMain.Account = ""
        formMain.Key = ""
        formMain.UserName = ""
        formMain.Password = ""
    Else
        Dim temp() As String
        temp() = Split(settings, ",")
        formMain.Account = temp(0)
        formMain.Key = temp(1)
        formMain.UserName = temp(2)
        formMain.Password = temp(3)
    End If
End Sub
