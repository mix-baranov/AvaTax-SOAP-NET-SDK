VERSION 5.00
Begin VB.Form formAddress 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Update Address"
   ClientHeight    =   3870
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   5475
   ForeColor       =   &H00800000&
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3870
   ScaleWidth      =   5475
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.CommandButton buttonUpdate 
      Caption         =   "Update"
      Height          =   345
      Left            =   2767
      TabIndex        =   7
      Top             =   3330
      Width           =   1185
   End
   Begin VB.TextBox textLine1 
      Height          =   330
      Left            =   1440
      TabIndex        =   0
      Top             =   480
      Width           =   3585
   End
   Begin VB.TextBox textLine2 
      Height          =   330
      Left            =   1440
      TabIndex        =   1
      Top             =   840
      Width           =   3585
   End
   Begin VB.TextBox textLine3 
      Height          =   330
      Left            =   1440
      TabIndex        =   2
      Top             =   1200
      Width           =   3585
   End
   Begin VB.TextBox textCity 
      Height          =   330
      Left            =   1440
      TabIndex        =   3
      Top             =   1560
      Width           =   2715
   End
   Begin VB.TextBox textState 
      Height          =   330
      Left            =   1440
      MaxLength       =   2
      TabIndex        =   4
      Top             =   1920
      Width           =   645
   End
   Begin VB.TextBox textZip 
      Height          =   330
      Left            =   1440
      MaxLength       =   10
      TabIndex        =   5
      Top             =   2280
      Width           =   1605
   End
   Begin VB.TextBox textCountry 
      Height          =   330
      Left            =   1440
      TabIndex        =   6
      Top             =   2640
      Width           =   1605
   End
   Begin VB.CommandButton buttonCancel 
      Cancel          =   -1  'True
      Caption         =   "Cancel"
      Height          =   345
      Left            =   1522
      TabIndex        =   8
      Top             =   3330
      Width           =   1185
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "Line 1:"
      Height          =   300
      Index           =   1
      Left            =   120
      TabIndex        =   15
      Top             =   525
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "Line 2:"
      Height          =   300
      Index           =   2
      Left            =   120
      TabIndex        =   14
      Top             =   900
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "Line 3:"
      Height          =   300
      Index           =   3
      Left            =   120
      TabIndex        =   13
      Top             =   1260
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "City:"
      Height          =   300
      Index           =   4
      Left            =   120
      TabIndex        =   12
      Top             =   1605
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "State:"
      Height          =   300
      Index           =   5
      Left            =   120
      TabIndex        =   11
      Top             =   1980
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "Zip:"
      Height          =   300
      Index           =   6
      Left            =   120
      TabIndex        =   10
      Top             =   2325
      Width           =   1275
   End
   Begin VB.Label lblAddress 
      Alignment       =   1  'Right Justify
      Caption         =   "Country:"
      Height          =   300
      Index           =   7
      Left            =   120
      TabIndex        =   9
      Top             =   2700
      Width           =   1275
   End
End
Attribute VB_Name = "formAddress"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Public oAddress As Address

Private Sub buttonCancel_Click()
    Unload Me
End Sub

Private Sub buttonUpdate_Click()

    If (Not oAddress Is Nothing) Then
        oAddress.Line1 = textLine1.text
        oAddress.Line2 = textLine2.text
        oAddress.Line3 = textLine3.text
        oAddress.City = textCity.text
        oAddress.Region = textState.text
        oAddress.PostalCode = textZip.text
        oAddress.Country = textCountry.text
    End If
    
    Unload Me
    
End Sub


Private Sub Form_Load()

    If (Not oAddress Is Nothing) Then
        textLine1.text = oAddress.Line1
        textLine2.text = oAddress.Line2
        textLine3.text = oAddress.Line3
        textCity.text = oAddress.City
        textState.text = oAddress.Region
        textZip.text = oAddress.PostalCode
        textCountry.text = oAddress.Country
    End If
    
End Sub
