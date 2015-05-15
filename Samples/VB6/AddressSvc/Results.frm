VERSION 5.00
Begin VB.Form formResults 
   Caption         =   "Validate Results"
   ClientHeight    =   7050
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   7500
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   ScaleHeight     =   7050
   ScaleWidth      =   7500
   StartUpPosition =   1  'CenterOwner
   Begin VB.Frame Frame1 
      Caption         =   "Valid Addresses"
      Height          =   5595
      Left            =   90
      TabIndex        =   5
      Top             =   930
      Width           =   7305
      Begin VB.CommandButton buttonNext 
         Caption         =   "Next >>"
         Enabled         =   0   'False
         Height          =   345
         Left            =   2010
         TabIndex        =   8
         Top             =   330
         Width           =   765
      End
      Begin VB.CommandButton buttonPrevious 
         Caption         =   "<< Prev"
         Enabled         =   0   'False
         Height          =   345
         Left            =   210
         TabIndex        =   6
         Top             =   330
         Width           =   765
      End
      Begin VB.Label lblAddressType 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1650
         TabIndex        =   34
         Top             =   840
         Width           =   5115
      End
      Begin VB.Label lblFIPSCode 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1650
         TabIndex        =   33
         Top             =   5145
         Width           =   5115
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "FIPSCode:"
         Height          =   300
         Index           =   19
         Left            =   300
         TabIndex        =   32
         Top             =   5145
         Width           =   1275
      End
      Begin VB.Label lblCarrierRoute 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1650
         TabIndex        =   31
         Top             =   4440
         Width           =   5115
      End
      Begin VB.Label lblPOSTNet 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1650
         TabIndex        =   30
         Top             =   4785
         Width           =   5115
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Carrier Route:"
         Height          =   300
         Index           =   18
         Left            =   300
         TabIndex        =   29
         Top             =   4440
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "POSTNet:"
         Height          =   300
         Index           =   17
         Left            =   300
         TabIndex        =   28
         Top             =   4785
         Width           =   1275
      End
      Begin VB.Label lblCountry 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1650
         TabIndex        =   27
         Top             =   4080
         Width           =   5115
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Country:"
         Height          =   300
         Index           =   21
         Left            =   300
         TabIndex        =   26
         Top             =   4080
         Width           =   1275
      End
      Begin VB.Label lblLine4 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1650
         TabIndex        =   25
         Top             =   2280
         Width           =   5115
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Line 4:"
         Height          =   300
         Index           =   25
         Left            =   300
         TabIndex        =   24
         Top             =   2280
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Address Type"
         Height          =   300
         Index           =   24
         Left            =   300
         TabIndex        =   23
         Top             =   840
         Width           =   1275
      End
      Begin VB.Label lblLine1 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1650
         TabIndex        =   22
         Top             =   1185
         Width           =   5115
      End
      Begin VB.Label lblLine2 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1650
         TabIndex        =   21
         Top             =   1560
         Width           =   5115
      End
      Begin VB.Label lblLine3 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1650
         TabIndex        =   20
         Top             =   1920
         Width           =   5115
      End
      Begin VB.Label lblCity 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1650
         TabIndex        =   19
         Top             =   2625
         Width           =   5115
      End
      Begin VB.Label lblRegion 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1650
         TabIndex        =   18
         Top             =   3000
         Width           =   5115
      End
      Begin VB.Label lblPostalCode 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1650
         TabIndex        =   17
         Top             =   3345
         Width           =   5115
      End
      Begin VB.Label lblCounty 
         ForeColor       =   &H00C00000&
         Height          =   300
         Left            =   1650
         TabIndex        =   16
         Top             =   3720
         Width           =   5115
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Line 1:"
         Height          =   300
         Index           =   15
         Left            =   300
         TabIndex        =   15
         Top             =   1185
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Line 2:"
         Height          =   300
         Index           =   14
         Left            =   300
         TabIndex        =   14
         Top             =   1560
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Line 3:"
         Height          =   300
         Index           =   13
         Left            =   300
         TabIndex        =   13
         Top             =   1920
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "City:"
         Height          =   300
         Index           =   12
         Left            =   300
         TabIndex        =   12
         Top             =   2625
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "State:"
         Height          =   300
         Index           =   11
         Left            =   300
         TabIndex        =   11
         Top             =   3000
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Zip:"
         Height          =   300
         Index           =   10
         Left            =   300
         TabIndex        =   10
         Top             =   3345
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "County:"
         Height          =   300
         Index           =   9
         Left            =   300
         TabIndex        =   9
         Top             =   3720
         Width           =   1275
      End
      Begin VB.Label lblTotal 
         Alignment       =   2  'Center
         Caption         =   "Count:"
         Height          =   285
         Left            =   1020
         TabIndex        =   7
         Top             =   390
         Width           =   915
      End
   End
   Begin VB.CommandButton buttonClose 
      Cancel          =   -1  'True
      Caption         =   "Close"
      Height          =   345
      Left            =   6390
      TabIndex        =   4
      Top             =   6600
      Width           =   1005
   End
   Begin VB.Label lblResultMsg 
      Caption         =   "Label4"
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   1200
      TabIndex        =   3
      Top             =   540
      Width           =   5895
      WordWrap        =   -1  'True
   End
   Begin VB.Label lblResult 
      Alignment       =   1  'Right Justify
      Caption         =   "Result Msg:"
      Height          =   300
      Index           =   1
      Left            =   60
      TabIndex        =   2
      Top             =   540
      Width           =   1065
   End
   Begin VB.Label lblResultCode 
      Caption         =   "Label2"
      ForeColor       =   &H00C00000&
      Height          =   300
      Left            =   1200
      TabIndex        =   1
      Top             =   120
      Width           =   1125
   End
   Begin VB.Label lblResult 
      Alignment       =   1  'Right Justify
      Caption         =   "Result Code:"
      Height          =   300
      Index           =   0
      Left            =   60
      TabIndex        =   0
      Top             =   120
      Width           =   1065
   End
End
Attribute VB_Name = "formResults"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Public oValidateResult As ValidateResult
Private lAddressesCount As Long
Private lCurrAddress As Long

Private Sub buttonClose_Click()
    Unload Me
End Sub

Private Sub buttonNext_Click()

    lCurrAddress = lCurrAddress + 1
    EnableNavButtons
    LoadAddress lCurrAddress

End Sub

Private Sub buttonPrevious_Click()

    lCurrAddress = lCurrAddress - 1
    EnableNavButtons
    LoadAddress lCurrAddress
    
End Sub

Private Sub Form_Load()
    
    With oValidateResult
        lblResultCode.Caption = TranslateSeverity(.ResultCode)
        SetMessageLabelText lblResultMsg, oValidateResult
        lAddressesCount = .Addresses.Count
        lblTotal.Caption = lblTotal.Caption & " " & CStr(lAddressesCount)
    End With
    lCurrAddress = 0
    EnableNavButtons
    If (lAddressesCount > 0) Then
        LoadAddress lCurrAddress
    End If
    
End Sub

Private Sub LoadAddress(ByVal lIndex)

    Dim oValidAddress As ValidAddress
    
    Set oValidAddress = oValidateResult.Addresses.Item(lIndex)
    
    '#####################################################
    '### LOAD SELECTED ADDRESS DATA FROM RESULT OBJECT ###
    '#####################################################
    lblAddressType.Caption = oValidAddress.AddressType
    lblLine1.Caption = oValidAddress.Line1
    lblLine2.Caption = oValidAddress.Line2
    lblLine3.Caption = oValidAddress.Line3
    lblLine4.Caption = oValidAddress.Line4
    lblCity.Caption = oValidAddress.City
    lblRegion.Caption = oValidAddress.Region
    lblPostalCode.Caption = oValidAddress.PostalCode
    lblCounty.Caption = oValidAddress.County
    lblCountry.Caption = oValidAddress.Country
    lblCarrierRoute.Caption = oValidAddress.CarrierRoute
    lblPOSTNet.Caption = oValidAddress.PostNet
    lblFIPSCode.Caption = oValidAddress.FipsCode
    
End Sub

Private Sub EnableNavButtons()

    '###############
    '### UI CODE ###
    '###############
    buttonPrevious.Enabled = (lAddressesCount > 1) And (lCurrAddress > 1)
    buttonNext.Enabled = (lAddressesCount > 1) And (lCurrAddress < lAddressesCount)
    
End Sub

Private Sub lblResultMsg_Click()
    Dim frm As MessagesForm
    Set frm = New MessagesForm
    frm.Init oValidateResult.Messages
    frm.Show vbModal, Me
End Sub
