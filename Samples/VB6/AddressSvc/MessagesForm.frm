VERSION 5.00
Begin VB.Form MessagesForm 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Messages"
   ClientHeight    =   4320
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   7410
   LinkTopic       =   "Form1"
   LockControls    =   -1  'True
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   4320
   ScaleWidth      =   7410
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.CommandButton buttonClose 
      Cancel          =   -1  'True
      Caption         =   "Close"
      Height          =   375
      Left            =   6120
      TabIndex        =   4
      Top             =   3840
      Width           =   1185
   End
   Begin VB.TextBox textMessage 
      Height          =   3180
      Left            =   2430
      Locked          =   -1  'True
      MultiLine       =   -1  'True
      ScrollBars      =   3  'Both
      TabIndex        =   1
      Top             =   540
      Width           =   4860
   End
   Begin VB.ListBox listMessages 
      Height          =   3180
      Left            =   90
      TabIndex        =   0
      Top             =   540
      Width           =   2280
   End
   Begin VB.Label Label2 
      Caption         =   "Selected Message Info:"
      Height          =   345
      Left            =   2460
      TabIndex        =   3
      Top             =   270
      Width           =   2265
   End
   Begin VB.Label Label1 
      Caption         =   "Messages:"
      Height          =   300
      Left            =   150
      TabIndex        =   2
      Top             =   270
      Width           =   1125
   End
End
Attribute VB_Name = "MessagesForm"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private oMessages As Messages

Public Sub Init(oMsgs As Messages)
    Set oMessages = oMsgs
End Sub

Private Sub buttonClose_Click()
    Unload Me
End Sub

Private Sub Form_Load()

    Dim oMsg As message
    For Each oMsg In oMessages
        listMessages.AddItem oMsg.Name
    Next oMsg
    
    If (listMessages.ListCount > 0) Then
        listMessages.ListIndex = 0
    End If
    
End Sub

Private Sub listMessages_Click()

    On Error GoTo errListMessages_Click

    If (listMessages.ListIndex >= 0) Then
        Dim index As Integer
        Dim message As message
        Dim msgInfo As String
        
        textMessage.text = ""
        index = listMessages.ListIndex

        Set message = oMessages.Item(index)
        msgInfo = "Name: " + message.Name & vbCrLf & vbCrLf
        msgInfo = msgInfo & "Severity: " & TranslateSeverity(message.severity) & vbCrLf & vbCrLf
        msgInfo = msgInfo & "Summary: " & message.Summary & vbCrLf & vbCrLf
        msgInfo = msgInfo & "Details: " & message.Details & vbCrLf & vbCrLf
        msgInfo = msgInfo & "Source: " & message.Source & vbCrLf & vbCrLf
        msgInfo = msgInfo & "RefersTo: " & message.RefersTo & vbCrLf & vbCrLf
        msgInfo = msgInfo & "HelpLink: " & message.HelpLink & vbCrLf & vbCrLf
        textMessage.text = msgInfo
    End If
    
    Exit Sub
    
errListMessages_Click:
    MsgBox Err.Number & ": " & Err.Description & vbCrLf & vbCrLf & "Error retrieving message from Messages collection.", vbOKOnly Or vbExclamation, "Error"
End Sub
