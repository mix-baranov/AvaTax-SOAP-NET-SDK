VERSION 5.00
Begin VB.Form formResults 
   Caption         =   "Validate Results"
   ClientHeight    =   7935
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   7680
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   ScaleHeight     =   7935
   ScaleWidth      =   7680
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame Frame1 
      Caption         =   "Valid Addresses"
      Height          =   5985
      Left            =   180
      TabIndex        =   5
      Top             =   1290
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
      Begin VB.Label lblFIPSCode 
         Caption         =   "FIPSCode"
         Height          =   300
         Left            =   1650
         TabIndex        =   36
         Top             =   5505
         Width           =   5115
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "FIPSCode:"
         Height          =   300
         Index           =   19
         Left            =   300
         TabIndex        =   35
         Top             =   5505
         Width           =   1275
      End
      Begin VB.Label lblCarrierRoute 
         Caption         =   "CarrierRoute"
         Height          =   300
         Left            =   1650
         TabIndex        =   34
         Top             =   4800
         Width           =   5115
      End
      Begin VB.Label lblPOSTNet 
         Caption         =   "POSTNet"
         Height          =   300
         Left            =   1650
         TabIndex        =   33
         Top             =   5145
         Width           =   5115
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Carrier Route:"
         Height          =   300
         Index           =   18
         Left            =   300
         TabIndex        =   32
         Top             =   4800
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "POSTNet:"
         Height          =   300
         Index           =   17
         Left            =   300
         TabIndex        =   31
         Top             =   5145
         Width           =   1275
      End
      Begin VB.Label lblCountry 
         Caption         =   "Country"
         Height          =   300
         Left            =   1650
         TabIndex        =   30
         Top             =   4440
         Width           =   5115
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Country:"
         Height          =   300
         Index           =   21
         Left            =   300
         TabIndex        =   29
         Top             =   4440
         Width           =   1275
      End
      Begin VB.Label lblLine4 
         Caption         =   "Line 4"
         Height          =   300
         Left            =   1650
         TabIndex        =   28
         Top             =   2640
         Width           =   5115
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Line 4:"
         Height          =   300
         Index           =   25
         Left            =   300
         TabIndex        =   27
         Top             =   2640
         Width           =   1275
      End
      Begin VB.Label lblAddressType 
         Caption         =   "Type"
         Height          =   300
         Left            =   1650
         TabIndex        =   26
         Top             =   1200
         Width           =   5115
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Address Type"
         Height          =   300
         Index           =   24
         Left            =   300
         TabIndex        =   25
         Top             =   1200
         Width           =   1275
      End
      Begin VB.Label lblAddressCode 
         Caption         =   "Code"
         Height          =   300
         Left            =   1650
         TabIndex        =   24
         Top             =   840
         Width           =   5115
      End
      Begin VB.Label lblLine1 
         Caption         =   "Line 1"
         Height          =   300
         Left            =   1650
         TabIndex        =   23
         Top             =   1545
         Width           =   5115
      End
      Begin VB.Label lblLine2 
         Caption         =   "Line 2"
         Height          =   300
         Left            =   1650
         TabIndex        =   22
         Top             =   1920
         Width           =   5115
      End
      Begin VB.Label lblLine3 
         Caption         =   "Line 3"
         Height          =   300
         Left            =   1650
         TabIndex        =   21
         Top             =   2280
         Width           =   5115
      End
      Begin VB.Label lblCity 
         Caption         =   "City"
         Height          =   300
         Left            =   1650
         TabIndex        =   20
         Top             =   2985
         Width           =   5115
      End
      Begin VB.Label lblRegion 
         Caption         =   "Region"
         Height          =   300
         Left            =   1650
         TabIndex        =   19
         Top             =   3360
         Width           =   5115
      End
      Begin VB.Label lblPostalCode 
         Caption         =   "PostalCode"
         Height          =   300
         Left            =   1650
         TabIndex        =   18
         Top             =   3705
         Width           =   5115
      End
      Begin VB.Label lblCounty 
         Caption         =   "County"
         Height          =   300
         Left            =   1650
         TabIndex        =   17
         Top             =   4080
         Width           =   5115
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Address Code:"
         Height          =   300
         Index           =   16
         Left            =   300
         TabIndex        =   16
         Top             =   840
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Line 1:"
         Height          =   300
         Index           =   15
         Left            =   300
         TabIndex        =   15
         Top             =   1545
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Line 2:"
         Height          =   300
         Index           =   14
         Left            =   300
         TabIndex        =   14
         Top             =   1920
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Line 3:"
         Height          =   300
         Index           =   13
         Left            =   300
         TabIndex        =   13
         Top             =   2280
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "City:"
         Height          =   300
         Index           =   12
         Left            =   300
         TabIndex        =   12
         Top             =   2985
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "State:"
         Height          =   300
         Index           =   11
         Left            =   300
         TabIndex        =   11
         Top             =   3360
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "Zip:"
         Height          =   300
         Index           =   10
         Left            =   300
         TabIndex        =   10
         Top             =   3705
         Width           =   1275
      End
      Begin VB.Label lblAddress 
         Alignment       =   1  'Right Justify
         Caption         =   "County"
         Height          =   300
         Index           =   9
         Left            =   300
         TabIndex        =   9
         Top             =   4080
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
      Left            =   6480
      TabIndex        =   4
      Top             =   7440
      Width           =   1005
   End
   Begin VB.Label lblResultMsg 
      Caption         =   "Label4"
      Height          =   570
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

Private Sub Form_Load()
    
    With oValidateResult
        lblResultCode.Caption = CStr(.ResultCode)
        lblResultMsg.Caption = .ResultMsg
        lAddressesCount = .Addresses.Count
        lblTotal.Caption = lblTotal.Caption & " " & CStr(lAddressesCount)
    End With
    lCurrAddress = 0
    EnableNavButtons
    LoadCaptions lCurrAddress
    
End Sub

Private Sub LoadCaptions(ByVal lIndex)

    Dim oValidAddress As ValidAddress
    
    Set oValidAddress = oValidateResult.Addresses.Item(lIndex)
    
    lblAddressCode.Caption = oValidAddress.AddressCode
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

    buttonPrevious = (lAddressesCount > 1) And (lCurrAddress > 1)
    buttonNext = (lAddressesCount > 1) And (lCurrAddress < lAddressesCount)
    
End Sub
