using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Avalara.AvaTax.Adapter.TaxService;

namespace Avalara.AvaTax.Adapter.Samples.TaxSample
{
	
	public enum TaxLineColumns
	{
		No = 0,
		//Discount,
		Taxable,
		Exemption,
		ExemptCertId,
		Rate,
		Tax,
		TaxCode,
		Taxability,
		BoundaryLevel
	}

	public enum TaxDetailColumns
	{
		JurisType = 0,
		JurisCode,
		Taxable,
		NonTaxable,
		Exemption,
		Rate,
		Tax,
		TaxType,
		TaxName
	}

	/// <summary>
	/// Summary description for GetTaxResult.
	/// </summary>
	public class formGetTaxResult : Form
	{
		private Label label1;
		private Label label2;
		private Label label4;
		private Label label5;
		private Label label6;
		private Label label9;
		private Label label10;
		private Label label12;
		private Label label13;
		private Label label26;
		private Label label27;
		private Button buttonClose;
		private Label lblResultCode;
		private Label labelDocStatus;
		private Label labelDocStatusDate;
		private Label labelReconciled;
		private Label lblTotalAmount;
		private Label labelTotalTax;
		private DataGrid gridTaxLines;
		private DataGrid gridTaxDetails;
		private System.Windows.Forms.LinkLabel lblResultMsg;
		private System.Windows.Forms.Label labelTotalTaxable;
		private System.Windows.Forms.Label labelDocCode;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label labelDocDate;
		private System.Windows.Forms.Label labelDocType;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label labelTotalDiscount;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label labelTotalExemption;
		//private System.Windows.Forms.CheckBox chkLocked;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label labelAdjustmentReason;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label labelAdjustmentDescription;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.Label labelTaxDate;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label labelLocked;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public formGetTaxResult(GetTaxResult getTaxResult)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//#####################
			//### UI SETUP CODE ###
			//#####################
			SetupTaxLineHeader();
			SetupTaxDetailHeader();

			_getTaxResult = getTaxResult;

			gridTaxLines.SetDataBinding(_dataLines, "");
			gridTaxDetails.SetDataBinding(_dataDetails, "");

			LoadResultData();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.lblResultCode = new System.Windows.Forms.Label();
			this.labelDocStatus = new System.Windows.Forms.Label();
			this.labelDocStatusDate = new System.Windows.Forms.Label();
			this.labelReconciled = new System.Windows.Forms.Label();
			this.lblTotalAmount = new System.Windows.Forms.Label();
			this.labelTotalTax = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.label27 = new System.Windows.Forms.Label();
			this.gridTaxLines = new System.Windows.Forms.DataGrid();
			this.gridTaxDetails = new System.Windows.Forms.DataGrid();
			this.buttonClose = new System.Windows.Forms.Button();
			this.lblResultMsg = new System.Windows.Forms.LinkLabel();
			this.labelTotalTaxable = new System.Windows.Forms.Label();
			this.labelDocCode = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.labelDocDate = new System.Windows.Forms.Label();
			this.labelDocType = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.labelTotalDiscount = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.labelTotalExemption = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.labelAdjustmentReason = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.labelAdjustmentDescription = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.labelVersion = new System.Windows.Forms.Label();
			this.labelTaxDate = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.labelLocked = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.gridTaxLines)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridTaxDetails)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Properties";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(108, 20);
			this.label2.TabIndex = 1;
			this.label2.Text = "Result Code:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(108, 20);
			this.label4.TabIndex = 2;
			this.label4.Text = "Result Msg:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 176);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(108, 20);
			this.label5.TabIndex = 7;
			this.label5.Text = "Doc Status Date:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 80);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(108, 20);
			this.label6.TabIndex = 6;
			this.label6.Text = "Document Status:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(288, 128);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(124, 20);
			this.label9.TabIndex = 12;
			this.label9.Text = "Total Tax:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(288, 104);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(124, 20);
			this.label10.TabIndex = 11;
			this.label10.Text = "Total Price:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(288, 80);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(124, 20);
			this.label12.TabIndex = 9;
			this.label12.Text = "Total Taxable:";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(288, 56);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(124, 20);
			this.label13.TabIndex = 8;
			this.label13.Text = "Reconciled:";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblResultCode
			// 
			this.lblResultCode.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblResultCode.Location = new System.Drawing.Point(120, 32);
			this.lblResultCode.Name = "lblResultCode";
			this.lblResultCode.Size = new System.Drawing.Size(155, 20);
			this.lblResultCode.TabIndex = 13;
			// 
			// labelDocStatus
			// 
			this.labelDocStatus.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelDocStatus.Location = new System.Drawing.Point(120, 80);
			this.labelDocStatus.Name = "labelDocStatus";
			this.labelDocStatus.Size = new System.Drawing.Size(155, 20);
			this.labelDocStatus.TabIndex = 18;
			// 
			// labelDocStatusDate
			// 
			this.labelDocStatusDate.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelDocStatusDate.Location = new System.Drawing.Point(120, 176);
			this.labelDocStatusDate.Name = "labelDocStatusDate";
			this.labelDocStatusDate.Size = new System.Drawing.Size(155, 20);
			this.labelDocStatusDate.TabIndex = 19;
			// 
			// labelReconciled
			// 
			this.labelReconciled.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelReconciled.Location = new System.Drawing.Point(416, 56);
			this.labelReconciled.Name = "labelReconciled";
			this.labelReconciled.Size = new System.Drawing.Size(155, 20);
			this.labelReconciled.TabIndex = 20;
			// 
			// lblTotalAmount
			// 
			this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblTotalAmount.Location = new System.Drawing.Point(416, 104);
			this.lblTotalAmount.Name = "lblTotalAmount";
			this.lblTotalAmount.Size = new System.Drawing.Size(155, 20);
			this.lblTotalAmount.TabIndex = 23;
			// 
			// labelTotalTax
			// 
			this.labelTotalTax.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelTotalTax.Location = new System.Drawing.Point(416, 128);
			this.labelTotalTax.Name = "labelTotalTax";
			this.labelTotalTax.Size = new System.Drawing.Size(155, 20);
			this.labelTotalTax.TabIndex = 24;
			// 
			// label26
			// 
			this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label26.Location = new System.Drawing.Point(8, 272);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(116, 20);
			this.label26.TabIndex = 25;
			this.label26.Text = "Tax Lines";
			// 
			// label27
			// 
			this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label27.Location = new System.Drawing.Point(12, 404);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(200, 20);
			this.label27.TabIndex = 26;
			this.label27.Text = "Tax Details (for selected Tax Line)";
			// 
			// gridTaxLines
			// 
			this.gridTaxLines.AllowSorting = false;
			this.gridTaxLines.DataMember = "";
			this.gridTaxLines.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.gridTaxLines.Location = new System.Drawing.Point(8, 288);
			this.gridTaxLines.Name = "gridTaxLines";
			this.gridTaxLines.ReadOnly = true;
			this.gridTaxLines.Size = new System.Drawing.Size(564, 97);
			this.gridTaxLines.TabIndex = 64;
			this.gridTaxLines.CurrentCellChanged += new System.EventHandler(this.gridTaxLines_CurrentCellChanged);
			// 
			// gridTaxDetails
			// 
			this.gridTaxDetails.AllowSorting = false;
			this.gridTaxDetails.DataMember = "";
			this.gridTaxDetails.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.gridTaxDetails.Location = new System.Drawing.Point(8, 420);
			this.gridTaxDetails.Name = "gridTaxDetails";
			this.gridTaxDetails.ReadOnly = true;
			this.gridTaxDetails.Size = new System.Drawing.Size(564, 97);
			this.gridTaxDetails.TabIndex = 65;
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(488, 540);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(84, 24);
			this.buttonClose.TabIndex = 66;
			this.buttonClose.Text = "Close";
			// 
			// lblResultMsg
			// 
			this.lblResultMsg.Location = new System.Drawing.Point(120, 56);
			this.lblResultMsg.Name = "lblResultMsg";
			this.lblResultMsg.Size = new System.Drawing.Size(156, 20);
			this.lblResultMsg.TabIndex = 67;
			this.lblResultMsg.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblResultMsg_LinkClicked);
			// 
			// labelTotalTaxable
			// 
			this.labelTotalTaxable.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelTotalTaxable.Location = new System.Drawing.Point(416, 80);
			this.labelTotalTaxable.Name = "labelTotalTaxable";
			this.labelTotalTaxable.Size = new System.Drawing.Size(155, 20);
			this.labelTotalTaxable.TabIndex = 70;
			// 
			// labelDocCode
			// 
			this.labelDocCode.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelDocCode.Location = new System.Drawing.Point(120, 128);
			this.labelDocCode.Name = "labelDocCode";
			this.labelDocCode.Size = new System.Drawing.Size(155, 20);
			this.labelDocCode.TabIndex = 76;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 128);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(108, 20);
			this.label8.TabIndex = 75;
			this.label8.Text = "Document Code:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelDocDate
			// 
			this.labelDocDate.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelDocDate.Location = new System.Drawing.Point(120, 152);
			this.labelDocDate.Name = "labelDocDate";
			this.labelDocDate.Size = new System.Drawing.Size(155, 20);
			this.labelDocDate.TabIndex = 74;
			// 
			// labelDocType
			// 
			this.labelDocType.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelDocType.Location = new System.Drawing.Point(120, 104);
			this.labelDocType.Name = "labelDocType";
			this.labelDocType.Size = new System.Drawing.Size(155, 20);
			this.labelDocType.TabIndex = 73;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(8, 152);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(108, 20);
			this.label15.TabIndex = 72;
			this.label15.Text = "Document Date:";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(8, 104);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(108, 20);
			this.label16.TabIndex = 71;
			this.label16.Text = "Document Type:";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(288, 152);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(124, 20);
			this.label3.TabIndex = 12;
			this.label3.Text = "Total Discount:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelTotalDiscount
			// 
			this.labelTotalDiscount.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelTotalDiscount.Location = new System.Drawing.Point(416, 152);
			this.labelTotalDiscount.Name = "labelTotalDiscount";
			this.labelTotalDiscount.Size = new System.Drawing.Size(155, 20);
			this.labelTotalDiscount.TabIndex = 24;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(288, 176);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(124, 20);
			this.label14.TabIndex = 12;
			this.label14.Text = "Total Exemption:";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelTotalExemption
			// 
			this.labelTotalExemption.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelTotalExemption.Location = new System.Drawing.Point(416, 176);
			this.labelTotalExemption.Name = "labelTotalExemption";
			this.labelTotalExemption.Size = new System.Drawing.Size(155, 20);
			this.labelTotalExemption.TabIndex = 24;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(288, 32);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(124, 20);
			this.label11.TabIndex = 7;
			this.label11.Text = "Locked:";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(8, 224);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(108, 20);
			this.label17.TabIndex = 12;
			this.label17.Text = "Adjustment Reason:";
			this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelAdjustmentReason
			// 
			this.labelAdjustmentReason.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelAdjustmentReason.Location = new System.Drawing.Point(120, 224);
			this.labelAdjustmentReason.Name = "labelAdjustmentReason";
			this.labelAdjustmentReason.Size = new System.Drawing.Size(156, 20);
			this.labelAdjustmentReason.TabIndex = 24;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(288, 224);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(124, 20);
			this.label18.TabIndex = 12;
			this.label18.Text = "Adjustment Description:";
			this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelAdjustmentDescription
			// 
			this.labelAdjustmentDescription.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelAdjustmentDescription.Location = new System.Drawing.Point(416, 224);
			this.labelAdjustmentDescription.Name = "labelAdjustmentDescription";
			this.labelAdjustmentDescription.Size = new System.Drawing.Size(156, 20);
			this.labelAdjustmentDescription.TabIndex = 24;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(288, 200);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(124, 20);
			this.label19.TabIndex = 12;
			this.label19.Text = "Version:";
			this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelVersion
			// 
			this.labelVersion.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelVersion.Location = new System.Drawing.Point(416, 200);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(155, 20);
			this.labelVersion.TabIndex = 24;
			// 
			// labelTaxDate
			// 
			this.labelTaxDate.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelTaxDate.Location = new System.Drawing.Point(120, 200);
			this.labelTaxDate.Name = "labelTaxDate";
			this.labelTaxDate.Size = new System.Drawing.Size(155, 20);
			this.labelTaxDate.TabIndex = 74;
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(8, 200);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(108, 20);
			this.label21.TabIndex = 72;
			this.label21.Text = "Tax Date:";
			this.label21.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelLocked
			// 
			this.labelLocked.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelLocked.Location = new System.Drawing.Point(416, 32);
			this.labelLocked.Name = "labelLocked";
			this.labelLocked.Size = new System.Drawing.Size(155, 20);
			this.labelLocked.TabIndex = 74;
			// 
			// formGetTaxResult
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(582, 571);
			this.Controls.Add(this.labelDocCode);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.labelDocDate);
			this.Controls.Add(this.labelDocType);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.labelTotalTaxable);
			this.Controls.Add(this.lblResultMsg);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.gridTaxDetails);
			this.Controls.Add(this.gridTaxLines);
			this.Controls.Add(this.label27);
			this.Controls.Add(this.label26);
			this.Controls.Add(this.labelTotalTax);
			this.Controls.Add(this.lblTotalAmount);
			this.Controls.Add(this.labelReconciled);
			this.Controls.Add(this.labelDocStatusDate);
			this.Controls.Add(this.labelDocStatus);
			this.Controls.Add(this.lblResultCode);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.labelTotalDiscount);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.labelTotalExemption);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.labelAdjustmentReason);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.labelAdjustmentDescription);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.labelVersion);
			this.Controls.Add(this.labelTaxDate);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.labelLocked);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "formGetTaxResult";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Get Tax Result";			
			((System.ComponentModel.ISupportInitialize)(this.gridTaxLines)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridTaxDetails)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		GetTaxResult _getTaxResult;
		DataTable _dataLines = new DataTable("TaxLines");
		DataTable _dataDetails = new DataTable("TaxDetails");

		private void SetupTaxLineHeader()
		{
			//#####################
			//### UI SETUP CODE ###
			//#####################
			DataColumn col;

			col = new DataColumn(TaxLineColumns.No.ToString(), typeof(string));
			col.Caption = TaxLineColumns.No.ToString();
			_dataLines.Columns.Add(col);

			/*col = new DataColumn(TaxLineColumns.Discount.ToString(), typeof(string));
			col.Caption = TaxLineColumns.Discount.ToString();
			_dataLines.Columns.Add(col);*/
/*
 * These properties have been deprecated in release version 4.1.
			col = new DataColumn(TaxLineColumns.Taxable.ToString(), typeof(string));
			col.Caption = TaxLineColumns.Taxable.ToString();
			_dataLines.Columns.Add(col);

			col = new DataColumn(TaxLineColumns.Exemption.ToString(), typeof(string));
			col.Caption = TaxLineColumns.Exemption.ToString();
			_dataLines.Columns.Add(col);

			col = new DataColumn(TaxLineColumns.Rate.ToString(), typeof(string));
			col.Caption = TaxLineColumns.Rate.ToString();
			_dataLines.Columns.Add(col);
*/

			//4.16 change for newly added field
			col = new DataColumn(TaxLineColumns.ExemptCertId.ToString(), typeof(string));
			col.Caption = TaxLineColumns.ExemptCertId.ToString();
			_dataLines.Columns.Add(col);



			col = new DataColumn(TaxLineColumns.Tax.ToString(), typeof(string));
			col.Caption = TaxLineColumns.Tax.ToString();
			_dataLines.Columns.Add(col);

			col = new DataColumn(TaxLineColumns.TaxCode.ToString(), typeof(string));
			col.Caption = TaxLineColumns.TaxCode.ToString();
			_dataLines.Columns.Add(col);
			
			col = new DataColumn(TaxLineColumns.Taxability.ToString(), typeof(string));
			col.Caption = TaxLineColumns.Taxability.ToString();
			_dataLines.Columns.Add(col);

			col = new DataColumn(TaxLineColumns.BoundaryLevel.ToString(), typeof(string));
			col.Caption = TaxLineColumns.BoundaryLevel.ToString();
			_dataLines.Columns.Add(col);

		}

		private void SetupTaxDetailHeader()
		{
			//#####################
			//### UI SETUP CODE ###
			//#####################
			DataColumn col;

			col = new DataColumn(TaxDetailColumns.JurisType.ToString(), typeof(string));
			col.Caption = TaxDetailColumns.JurisType.ToString();
			_dataDetails.Columns.Add(col);

			col = new DataColumn(TaxDetailColumns.JurisCode.ToString(), typeof(string));
			col.Caption = TaxDetailColumns.JurisCode.ToString();
			_dataDetails.Columns.Add(col);

			col = new DataColumn(TaxDetailColumns.Taxable.ToString(), typeof(string));
			col.Caption = TaxDetailColumns.Taxable.ToString();
			_dataDetails.Columns.Add(col);

			col = new DataColumn(TaxDetailColumns.NonTaxable.ToString(), typeof(string));
			col.Caption = TaxDetailColumns.NonTaxable.ToString();
			_dataDetails.Columns.Add(col);

			col = new DataColumn(TaxDetailColumns.Exemption.ToString(), typeof(string));
			col.Caption = TaxDetailColumns.Exemption.ToString();
			_dataDetails.Columns.Add(col);

			col = new DataColumn(TaxDetailColumns.Rate.ToString(), typeof(string));
			col.Caption = TaxDetailColumns.Rate.ToString();
			_dataDetails.Columns.Add(col);

			col = new DataColumn(TaxDetailColumns.Tax.ToString(), typeof(string));
			col.Caption = TaxDetailColumns.Tax.ToString();
			_dataDetails.Columns.Add(col);

			col = new DataColumn(TaxDetailColumns.TaxType.ToString(), typeof(string));
			col.Caption = TaxDetailColumns.TaxType.ToString();
			_dataDetails.Columns.Add(col);

			col = new DataColumn(TaxDetailColumns.TaxName.ToString(), typeof(string));
			col.Caption = TaxDetailColumns.TaxName.ToString();
			_dataDetails.Columns.Add(col);
		}

		public void LoadResultData()
		{
			//####################################################
			//### LOAD DATA FROM THE RESULT OBJECT INTO THE UI ###
			//####################################################
			lblResultCode.Text = _getTaxResult.ResultCode.ToString();
			Util.SetMessageLabelText(lblResultMsg, _getTaxResult);
			labelDocStatus.Text = _getTaxResult.DocStatus.ToString();
			labelDocType.Text = _getTaxResult.DocType.ToString();
			labelDocCode.Text = _getTaxResult.DocCode;
			labelDocDate.Text = _getTaxResult.DocDate.ToShortDateString();
			labelDocStatusDate.Text = _getTaxResult.Timestamp.ToString();
			labelReconciled.Text = _getTaxResult.Reconciled.ToString();
			labelTotalTaxable.Text = _getTaxResult.TotalTaxable.ToString();
			lblTotalAmount.Text = _getTaxResult.TotalAmount.ToString();
			labelTotalTax.Text = _getTaxResult.TotalTax.ToString();
			labelTotalDiscount.Text = _getTaxResult.TotalDiscount.ToString();
			labelTotalExemption.Text = _getTaxResult.TotalExemption.ToString();
			//chkLocked.Checked = _getTaxResult.Locked;
			labelLocked.Text = _getTaxResult.Locked.ToString();
			labelAdjustmentReason.Text = _getTaxResult.AdjustmentReason.ToString();
			labelAdjustmentDescription.Text = _getTaxResult.AdjustmentDescription;
			labelVersion.Text = _getTaxResult.Version.ToString();
			labelTaxDate.Text = _getTaxResult.TaxDate.ToString();
			
			LoadTaxLineData();
			gridTaxLines_CurrentCellChanged(null, null);
		}

		public void LoadTaxLineData()
		{
			//################################################
			//### LOAD TAXLINE DATA FROM THE RESULT OBJECT ###
			//################################################
			DataRow row;

			foreach (TaxLine taxLine in _getTaxResult.TaxLines)
			{
				row = _dataLines.NewRow();
				row[TaxLineColumns.No.ToString()] = taxLine.No;
				//row[TaxLineColumns.Discount.ToString()] = taxLine.Discount.ToString();
/*
 * These properties have been deprecated in release version 4.1.
				row[TaxLineColumns.Taxable.ToString()] = taxLine.Taxable.ToString();
				row[TaxLineColumns.Exemption.ToString()] = taxLine.Exemption.ToString();
				row[TaxLineColumns.Rate.ToString()] = taxLine.Rate.ToString();
*/

				//row[TaxLineColumns.Exemption.ToString()] = taxLine.Exemption.ToString();
				row[TaxLineColumns.ExemptCertId.ToString()] = taxLine.ExemptCertId.ToString();

				row[TaxLineColumns.Tax.ToString()] = taxLine.Tax.ToString();
				row[TaxLineColumns.TaxCode.ToString()] = taxLine.TaxCode;
				row[TaxLineColumns.Taxability.ToString()] = taxLine.Taxability.ToString();
				row[TaxLineColumns.BoundaryLevel.ToString()] = taxLine.BoundaryLevel.ToString();
				_dataLines.Rows.Add(row);
				
			}
		}

		public void LoadTaxDetailData(TaxLine taxLine)
		{
			//###################################################
			//### LOAD TAXDETAIL DATA FROM THE TAXLINE OBJECT ###
			//###################################################
			DataRow row;

			_dataDetails.Rows.Clear();
			foreach (TaxDetail taxDetail in taxLine.TaxDetails)
			{
				row = _dataDetails.NewRow();
				row[TaxDetailColumns.JurisType.ToString()] = taxDetail.JurisType.ToString();
				row[TaxDetailColumns.JurisCode.ToString()] = taxDetail.JurisCode;
				row[TaxDetailColumns.Taxable.ToString()] = taxDetail.Taxable.ToString();
				row[TaxDetailColumns.NonTaxable.ToString()] = taxDetail.NonTaxable.ToString();
				row[TaxDetailColumns.Exemption.ToString()] = taxDetail.Exemption.ToString();
				row[TaxDetailColumns.Rate.ToString()] = taxDetail.Rate.ToString();
				row[TaxDetailColumns.Tax.ToString()] = taxDetail.Tax.ToString();
				row[TaxDetailColumns.TaxType.ToString()] = taxDetail.TaxType.ToString();
				row[TaxDetailColumns.TaxName.ToString()] = taxDetail.TaxName.ToString();
				_dataDetails.Rows.Add(row);
			}
		}

		private int _currentRow = -1;
		private void gridTaxLines_CurrentCellChanged(object sender, EventArgs e)
		{
			if (_currentRow != gridTaxLines.CurrentRowIndex)
			{
				_currentRow = gridTaxLines.CurrentRowIndex;
				if (_currentRow >= 0 && _getTaxResult.TaxLines.Count > 0) 
				{
					TaxLine taxLine = _getTaxResult.TaxLines[_currentRow];
					LoadTaxDetailData(taxLine);
				}
			}
		}

		private void lblResultMsg_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			MessagesForm frm = new MessagesForm(_getTaxResult.Messages);
			frm.Owner = this;
			frm.ShowDialog();
		}

		
	}
}
