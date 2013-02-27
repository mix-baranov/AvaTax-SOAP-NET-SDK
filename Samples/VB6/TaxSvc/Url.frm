VERSION 5.00
Begin VB.Form formUrl 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Url Setting"
   ClientHeight    =   2115
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   6705
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2115
   ScaleWidth      =   6705
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.TextBox textViaUrl 
      Height          =   330
      Left            =   720
      TabIndex        =   3
      Top             =   1140
      Width           =   5865
   End
   Begin VB.CommandButton buttonCancel 
      Cancel          =   -1  'True
      Caption         =   "Cancel"
      Height          =   345
      Left            =   4530
      TabIndex        =   4
      Top             =   1620
      Width           =   1005
   End
   Begin VB.TextBox textUrl 
      Height          =   330
      Left            =   720
      TabIndex        =   2
      Top             =   720
      Width           =   5865
   End
   Begin VB.CommandButton buttonApply 
      Caption         =   "Apply"
      Default         =   -1  'True
      Height          =   345
      Left            =   5610
      TabIndex        =   5
      Top             =   1620
      Width           =   1005
   End
   Begin VB.Label Label3 
      Alignment       =   1  'Right Justify
      Caption         =   "Via Url:"
      Height          =   300
      Left            =   -240
      TabIndex        =   6
      Top             =   1200
      Width           =   915
   End
   Begin VB.Label Label1 
      Caption         =   "Use this form to change the default value loaded from the configuration file Avalara.Adapter.AvaTax.dll.config."
      Height          =   555
      Left            =   120
      TabIndex        =   0
      Top             =   150
      Width           =   6465
   End
   Begin VB.Label lblUrl 
      Alignment       =   1  'Right Justify
      Caption         =   "Url:"
      Height          =   300
      Left            =   -240
      TabIndex        =   1
      Top             =   780
      Width           =   915
   End
End
Attribute VB_Name = "formUrl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub buttonApply_Click()
    formMain.Url = textUrl.text
    formMain.ViaUrl = textViaUrl.text
    Unload Me
End Sub

Private Sub buttonCancel_Click()
    Unload Me
End Sub

Private Sub Form_Load()
    textUrl.text = formMain.Url
    textViaUrl.text = formMain.ViaUrl
End Sub

