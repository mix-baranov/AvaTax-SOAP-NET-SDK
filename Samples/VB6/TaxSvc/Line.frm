VERSION 5.00
Begin VB.Form formLine 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Line Item"
   ClientHeight    =   6180
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   5505
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6180
   ScaleWidth      =   5505
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.TextBox textReason 
      Height          =   330
      Left            =   1890
      TabIndex        =   21
      Top             =   4080
      Width           =   1605
   End
   Begin VB.TextBox textTaxDate 
      Height          =   330
      Left            =   1890
      TabIndex        =   25
      Top             =   4800
      Width           =   1605
   End
   Begin VB.ComboBox cboTaxOverrideType 
      Height          =   315
      Left            =   1890
      Style           =   2  'Dropdown List
      TabIndex        =   20
      Top             =   3720
      Width           =   1635
   End
   Begin VB.TextBox textTaxAmount 
      Height          =   330
      Left            =   1890
      TabIndex        =   24
      Top             =   4440
      Width           =   1605
   End
   Begin VB.CheckBox chkDiscounted 
      Caption         =   "Check1"
      Height          =   195
      Left            =   1890
      TabIndex        =   9
      Top             =   1650
      Width           =   285
   End
   Begin VB.TextBox textTaxCode 
      Height          =   330
      Left            =   1890
      TabIndex        =   19
      Top             =   3360
      Width           =   1605
   End
   Begin VB.TextBox textRevAcct 
      Height          =   330
      Left            =   1890
      TabIndex        =   17
      Top             =   3000
      Width           =   3405
   End
   Begin VB.TextBox textRef2 
      Height          =   330
      Left            =   1890
      TabIndex        =   15
      Top             =   2640
      Width           =   3405
   End
   Begin VB.TextBox textRef1 
      Height          =   330
      Left            =   1890
      TabIndex        =   13
      Top             =   2280
      Width           =   3405
   End
   Begin VB.CommandButton buttonCancel 
      Cancel          =   -1  'True
      Caption         =   "Cancel"
      Height          =   345
      Left            =   1537
      TabIndex        =   27
      Top             =   5610
      Width           =   1185
   End
   Begin VB.TextBox textExemptionNo 
      Height          =   330
      Left            =   1890
      MaxLength       =   10
      TabIndex        =   11
      Top             =   1920
      Width           =   1605
   End
   Begin VB.TextBox textAmount 
      Height          =   330
      Left            =   1890
      TabIndex        =   7
      Top             =   1200
      Width           =   1665
   End
   Begin VB.TextBox textQty 
      Height          =   330
      Left            =   1890
      TabIndex        =   5
      Top             =   840
      Width           =   975
   End
   Begin VB.TextBox textItemCode 
      Height          =   330
      Left            =   1890
      TabIndex        =   4
      Top             =   480
      Width           =   2265
   End
   Begin VB.TextBox textNo 
      Height          =   330
      Left            =   1890
      TabIndex        =   3
      Top             =   120
      Width           =   975
   End
   Begin VB.CommandButton buttonUpdate 
      Caption         =   "Update"
      Default         =   -1  'True
      Height          =   345
      Left            =   2760
      TabIndex        =   29
      Top             =   5610
      Width           =   1185
   End
   Begin VB.Label lblLine 
      Alignment       =   1  'Right Justify
      Caption         =   "Tax Override Reason:"
      Height          =   300
      Index           =   13
      Left            =   240
      TabIndex        =   28
      Top             =   4140
      Width           =   1635
   End
   Begin VB.Label lblLine 
      Alignment       =   1  'Right Justify
      Caption         =   "Tax Override Date:"
      Height          =   300
      Index           =   8
      Left            =   240
      TabIndex        =   26
      Top             =   4860
      Width           =   1635
   End
   Begin VB.Label lblLine 
      Alignment       =   1  'Right Justify
      Caption         =   "Tax Override Type:"
      Height          =   300
      Index           =   7
      Left            =   480
      TabIndex        =   23
      Top             =   3760
      Width           =   1395
   End
   Begin VB.Label lblLine 
      Alignment       =   1  'Right Justify
      Caption         =   "Tax Override Amount:"
      Height          =   300
      Index           =   3
      Left            =   240
      TabIndex        =   22
      Top             =   4500
      Width           =   1635
   End
   Begin VB.Label lblLine 
      Alignment       =   1  'Right Justify
      Caption         =   "Tax Code:"
      Height          =   300
      Index           =   12
      Left            =   570
      TabIndex        =   18
      Top             =   3420
      Width           =   1275
   End
   Begin VB.Label lblLine 
      Alignment       =   1  'Right Justify
      Caption         =   "Revenue Acct:"
      Height          =   300
      Index           =   11
      Left            =   570
      TabIndex        =   16
      Top             =   3060
      Width           =   1275
   End
   Begin VB.Label lblLine 
      Alignment       =   1  'Right Justify
      Caption         =   "Reference 2:"
      Height          =   300
      Index           =   10
      Left            =   570
      TabIndex        =   14
      Top             =   2700
      Width           =   1275
   End
   Begin VB.Label lblLine 
      Alignment       =   1  'Right Justify
      Caption         =   "Reference 1:"
      Height          =   300
      Index           =   9
      Left            =   570
      TabIndex        =   12
      Top             =   2340
      Width           =   1275
   End
   Begin VB.Label lblLine 
      Alignment       =   1  'Right Justify
      Caption         =   "Exemption No:"
      Height          =   300
      Index           =   6
      Left            =   570
      TabIndex        =   10
      Top             =   1980
      Width           =   1275
   End
   Begin VB.Label lblLine 
      Alignment       =   1  'Right Justify
      Caption         =   "Discounted:"
      Height          =   300
      Index           =   5
      Left            =   600
      TabIndex        =   8
      Top             =   1620
      Width           =   1275
   End
   Begin VB.Label lblLine 
      Alignment       =   1  'Right Justify
      Caption         =   "Amount:"
      Height          =   300
      Index           =   4
      Left            =   570
      TabIndex        =   6
      Top             =   1245
      Width           =   1275
   End
   Begin VB.Label lblLine 
      Alignment       =   1  'Right Justify
      Caption         =   "Quantity:"
      Height          =   300
      Index           =   2
      Left            =   570
      TabIndex        =   2
      Top             =   900
      Width           =   1275
   End
   Begin VB.Label lblLine 
      Alignment       =   1  'Right Justify
      Caption         =   "Item Code:"
      Height          =   300
      Index           =   1
      Left            =   570
      TabIndex        =   1
      Top             =   525
      Width           =   1275
   End
   Begin VB.Label lblLine 
      Alignment       =   1  'Right Justify
      Caption         =   "No:"
      Height          =   300
      Index           =   0
      Left            =   570
      TabIndex        =   0
      Top             =   180
      Width           =   1275
   End
End
Attribute VB_Name = "formLine"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Public addLine As Boolean

Public oLine As Avalara_AvaTax_Adapter.Line

Private Sub buttonCancel_Click()
    formGetTax.addLine = False
    Unload Me
End Sub

Private Sub buttonUpdate_Click()

    oLine.No = textNo.text
    oLine.ItemCode = textItemCode.text
    oLine.Qty = Val(textQty.text)
    oLine.Amount = Val(textAmount.text)
    oLine.Discounted = CBool(chkDiscounted.Value)
    oLine.ExemptionNo = textExemptionNo.text
    oLine.Ref1 = textRef1.text
    oLine.Ref2 = textRef2.text
    oLine.RevAcct = textRevAcct.text
    oLine.TaxCode = textTaxCode.text
    'Added for 5.0
'    oLine.IsTaxOverriden = CBool(chkIsTaxOverriden.Value)
'    oLine.TaxOverride = textTaxOverride.text
     'Added for 5.0.2
    oLine.TaxOverride.TaxOverrideType = Val(cboTaxOverrideType.List(cboTaxOverrideType.ListIndex)) 'TranslateTaxOverrideTypeToString(Val(cboTaxOverrideType.List(cboTaxOverrideType.ListIndex)))
    oLine.TaxOverride.TaxAmount = Val(textTaxAmount.text)
      
    If (IsDate(textTaxDate.text)) Then
       oLine.TaxOverride.TaxDate = CDate(textTaxDate.text)
    End If
   
    oLine.TaxOverride.Reason = textReason.text

    formGetTax.addLine = True
    Unload Me
    
End Sub

Private Sub Form_Load()

    FillTaxOverrideTypeCombo cboTaxOverrideType
    
    textNo.text = oLine.No
    textItemCode.text = oLine.ItemCode
    textQty.text = oLine.Qty
    textAmount.text = oLine.Amount
    chkDiscounted.Value = IIf(oLine.Discounted = "1", 1, 0)
    textExemptionNo.text = oLine.ExemptionNo
    textRef1.text = oLine.Ref1
    textRef2.text = oLine.Ref2
    textRevAcct.text = oLine.RevAcct
    textTaxCode.text = oLine.TaxCode
    'Added for 5.0
'    chkIsTaxOverriden.Value = IIf(oLine.IsTaxOverriden = "1", 1, 0)
'    textTaxOverride.text = oLine.TaxOverride

    'Added for 5.0.2
    cboTaxOverrideType.ListIndex = oLine.TaxOverride.TaxOverrideType ' TranslateTaxOverrideType()
    
    textTaxAmount.text = oLine.TaxOverride.TaxAmount
    If (oLine.TaxOverride.TaxDate <> #1/1/1900#) Then
        textTaxDate.text = oLine.TaxOverride.TaxDate
    End If
    
    textReason.text = oLine.TaxOverride.Reason
    
End Sub
