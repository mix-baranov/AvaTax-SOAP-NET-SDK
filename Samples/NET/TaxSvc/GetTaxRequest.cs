using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Avalara.AvaTax.Adapter.AddressService;
using Avalara.AvaTax.Adapter.TaxService;

namespace Avalara.AvaTax.Adapter.Samples.TaxSample
{
	public enum AddressColumns
	{
		Parent = 0,
		Type,
		Line1,
		Line2,
		Line3,
		City,
		Region,
		PostalCode,
		Country,
		//Added for 5.0
		TaxRegionId
	}

	/// <summary>
	/// Summary description for GetTaxRequest2.
	/// </summary>
	public class formGetTaxRequest : Form
	{
		private Label label28;
		private Label label29;
		private Label label30;
		private Label label16;
		private Label label17;
		private Label label12;
		private Label label13;
		private Label label11;
		private Label label10;
		private Label label41;
		private Label label40;
		private Button buttonClose;
		private Label labelCompanyCode;
		private Label labelDocCode;
		private Label labelDocDate;
		private Label labelDocType;
		private Label labelExemptionNo;
		private Label labelDiscount;
		private Label labelSalespersonCode;
		private Label labelCustomerCode;
		private Label labelDetailLevel;
		private DataGrid gridLines;
		private DataGrid gridAddresses;
		private System.Windows.Forms.Label labelCustomerUsageType;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelPurchaseOrderNo;
		private System.Windows.Forms.Label lbl_LocationCode;
		private System.Windows.Forms.Label labelLocationCode;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label labelTaxOverrideType;
		private System.Windows.Forms.Label labelTaxAmount;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label labelTaxDate;
		private System.Windows.Forms.Label labelReason;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label labelServiceMode;
		private System.Windows.Forms.Label labelCurrencyCode;
		private System.Windows.Forms.Label label18;
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public formGetTaxRequest(GetTaxRequest getTaxRequest)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//#####################
			//### UI SETUP CODE ###
			//#####################
			SetupLineHeader();
			SetupAddressHeader();

			_getTaxRequest = getTaxRequest;

			gridLines.SetDataBinding(_dataLines, "");
			gridAddresses.SetDataBinding(_dataAddresses, "");

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
			this.gridLines = new System.Windows.Forms.DataGrid();
			this.gridAddresses = new System.Windows.Forms.DataGrid();
			this.label41 = new System.Windows.Forms.Label();
			this.label40 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.label30 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.labelCompanyCode = new System.Windows.Forms.Label();
			this.labelDocCode = new System.Windows.Forms.Label();
			this.labelDocDate = new System.Windows.Forms.Label();
			this.labelDocType = new System.Windows.Forms.Label();
			this.labelExemptionNo = new System.Windows.Forms.Label();
			this.labelDiscount = new System.Windows.Forms.Label();
			this.labelSalespersonCode = new System.Windows.Forms.Label();
			this.labelCustomerCode = new System.Windows.Forms.Label();
			this.labelDetailLevel = new System.Windows.Forms.Label();
			this.buttonClose = new System.Windows.Forms.Button();
			this.labelCustomerUsageType = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.labelPurchaseOrderNo = new System.Windows.Forms.Label();
			this.lbl_LocationCode = new System.Windows.Forms.Label();
			this.labelLocationCode = new System.Windows.Forms.Label();
			this.labelTaxOverrideType = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.labelTaxAmount = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.labelTaxDate = new System.Windows.Forms.Label();
			this.labelReason = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.labelServiceMode = new System.Windows.Forms.Label();
			this.labelCurrencyCode = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.gridLines)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridAddresses)).BeginInit();
			this.SuspendLayout();
			// 
			// gridLines
			// 
			this.gridLines.AllowSorting = false;
			this.gridLines.DataMember = "";
			this.gridLines.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.gridLines.Location = new System.Drawing.Point(12, 332);
			this.gridLines.Name = "gridLines";
			this.gridLines.ReadOnly = true;
			this.gridLines.Size = new System.Drawing.Size(772, 97);
			this.gridLines.TabIndex = 69;
			// 
			// gridAddresses
			// 
			this.gridAddresses.AllowSorting = false;
			this.gridAddresses.DataMember = "";
			this.gridAddresses.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.gridAddresses.Location = new System.Drawing.Point(12, 28);
			this.gridAddresses.Name = "gridAddresses";
			this.gridAddresses.ReadOnly = true;
			this.gridAddresses.Size = new System.Drawing.Size(772, 97);
			this.gridAddresses.TabIndex = 68;
			// 
			// label41
			// 
			this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label41.Location = new System.Drawing.Point(8, 300);
			this.label41.Name = "label41";
			this.label41.Size = new System.Drawing.Size(80, 20);
			this.label41.TabIndex = 67;
			this.label41.Text = "Line Items";
			// 
			// label40
			// 
			this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label40.Location = new System.Drawing.Point(8, 12);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(84, 20);
			this.label40.TabIndex = 66;
			this.label40.Text = "Addresses";
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(508, 188);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(120, 20);
			this.label28.TabIndex = 80;
			this.label28.Text = "Salesperson Code:";
			this.label28.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label29
			// 
			this.label29.Location = new System.Drawing.Point(508, 140);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(120, 20);
			this.label29.TabIndex = 79;
			this.label29.Text = "Customer Code:";
			this.label29.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label30
			// 
			this.label30.Location = new System.Drawing.Point(260, 188);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(103, 20);
			this.label30.TabIndex = 78;
			this.label30.Text = "Detail Level:";
			this.label30.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(260, 164);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(103, 20);
			this.label16.TabIndex = 75;
			this.label16.Text = "Exemption No:";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(260, 140);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(103, 20);
			this.label17.TabIndex = 74;
			this.label17.Text = "Discount:";
			this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(-8, 212);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(103, 20);
			this.label12.TabIndex = 73;
			this.label12.Text = "Document Type:";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(-8, 188);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(103, 20);
			this.label13.TabIndex = 72;
			this.label13.Text = "Document Date:";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(-8, 164);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(103, 20);
			this.label11.TabIndex = 71;
			this.label11.Text = "Document Code:";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(-8, 140);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(103, 20);
			this.label10.TabIndex = 70;
			this.label10.Text = "Company Code:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelCompanyCode
			// 
			this.labelCompanyCode.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelCompanyCode.Location = new System.Drawing.Point(100, 140);
			this.labelCompanyCode.Name = "labelCompanyCode";
			this.labelCompanyCode.Size = new System.Drawing.Size(152, 20);
			this.labelCompanyCode.TabIndex = 81;
			// 
			// labelDocCode
			// 
			this.labelDocCode.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelDocCode.Location = new System.Drawing.Point(100, 164);
			this.labelDocCode.Name = "labelDocCode";
			this.labelDocCode.Size = new System.Drawing.Size(152, 20);
			this.labelDocCode.TabIndex = 82;
			// 
			// labelDocDate
			// 
			this.labelDocDate.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelDocDate.Location = new System.Drawing.Point(100, 188);
			this.labelDocDate.Name = "labelDocDate";
			this.labelDocDate.Size = new System.Drawing.Size(152, 20);
			this.labelDocDate.TabIndex = 83;
			// 
			// labelDocType
			// 
			this.labelDocType.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelDocType.Location = new System.Drawing.Point(100, 212);
			this.labelDocType.Name = "labelDocType";
			this.labelDocType.Size = new System.Drawing.Size(152, 20);
			this.labelDocType.TabIndex = 84;
			// 
			// labelExemptionNo
			// 
			this.labelExemptionNo.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelExemptionNo.Location = new System.Drawing.Point(368, 164);
			this.labelExemptionNo.Name = "labelExemptionNo";
			this.labelExemptionNo.Size = new System.Drawing.Size(132, 20);
			this.labelExemptionNo.TabIndex = 86;
			// 
			// labelDiscount
			// 
			this.labelDiscount.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelDiscount.Location = new System.Drawing.Point(368, 140);
			this.labelDiscount.Name = "labelDiscount";
			this.labelDiscount.Size = new System.Drawing.Size(132, 20);
			this.labelDiscount.TabIndex = 85;
			// 
			// labelSalespersonCode
			// 
			this.labelSalespersonCode.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelSalespersonCode.Location = new System.Drawing.Point(632, 188);
			this.labelSalespersonCode.Name = "labelSalespersonCode";
			this.labelSalespersonCode.Size = new System.Drawing.Size(156, 20);
			this.labelSalespersonCode.TabIndex = 91;
			// 
			// labelCustomerCode
			// 
			this.labelCustomerCode.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelCustomerCode.Location = new System.Drawing.Point(632, 140);
			this.labelCustomerCode.Name = "labelCustomerCode";
			this.labelCustomerCode.Size = new System.Drawing.Size(156, 20);
			this.labelCustomerCode.TabIndex = 90;
			// 
			// labelDetailLevel
			// 
			this.labelDetailLevel.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelDetailLevel.Location = new System.Drawing.Point(368, 188);
			this.labelDetailLevel.Name = "labelDetailLevel";
			this.labelDetailLevel.Size = new System.Drawing.Size(132, 20);
			this.labelDetailLevel.TabIndex = 89;
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(712, 440);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(72, 24);
			this.buttonClose.TabIndex = 92;
			this.buttonClose.Text = "Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// labelCustomerUsageType
			// 
			this.labelCustomerUsageType.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelCustomerUsageType.Location = new System.Drawing.Point(632, 164);
			this.labelCustomerUsageType.Name = "labelCustomerUsageType";
			this.labelCustomerUsageType.Size = new System.Drawing.Size(156, 20);
			this.labelCustomerUsageType.TabIndex = 94;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(508, 164);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 20);
			this.label2.TabIndex = 93;
			this.label2.Text = "Customer Usage Type:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(508, 212);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 20);
			this.label1.TabIndex = 80;
			this.label1.Text = "Purchase Order No:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelPurchaseOrderNo
			// 
			this.labelPurchaseOrderNo.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelPurchaseOrderNo.Location = new System.Drawing.Point(632, 212);
			this.labelPurchaseOrderNo.Name = "labelPurchaseOrderNo";
			this.labelPurchaseOrderNo.Size = new System.Drawing.Size(156, 20);
			this.labelPurchaseOrderNo.TabIndex = 91;
			// 
			// lbl_LocationCode
			// 
			this.lbl_LocationCode.Location = new System.Drawing.Point(260, 212);
			this.lbl_LocationCode.Name = "lbl_LocationCode";
			this.lbl_LocationCode.Size = new System.Drawing.Size(104, 20);
			this.lbl_LocationCode.TabIndex = 95;
			this.lbl_LocationCode.Text = "Location Code:";
			this.lbl_LocationCode.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelLocationCode
			// 
			this.labelLocationCode.Location = new System.Drawing.Point(368, 212);
			this.labelLocationCode.Name = "labelLocationCode";
			this.labelLocationCode.Size = new System.Drawing.Size(132, 20);
			this.labelLocationCode.TabIndex = 96;
			// 
			// labelTaxOverrideType
			// 
			this.labelTaxOverrideType.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelTaxOverrideType.Location = new System.Drawing.Point(100, 236);
			this.labelTaxOverrideType.Name = "labelTaxOverrideType";
			this.labelTaxOverrideType.Size = new System.Drawing.Size(152, 20);
			this.labelTaxOverrideType.TabIndex = 84;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(-8, 236);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(103, 20);
			this.label4.TabIndex = 73;
			this.label4.Text = "TaxOverride Type:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelTaxAmount
			// 
			this.labelTaxAmount.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelTaxAmount.Location = new System.Drawing.Point(368, 236);
			this.labelTaxAmount.Name = "labelTaxAmount";
			this.labelTaxAmount.Size = new System.Drawing.Size(132, 20);
			this.labelTaxAmount.TabIndex = 98;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(260, 236);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(103, 20);
			this.label5.TabIndex = 97;
			this.label5.Text = "Tax Amount:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(508, 236);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 20);
			this.label3.TabIndex = 100;
			this.label3.Text = "Tax Date:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelTaxDate
			// 
			this.labelTaxDate.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelTaxDate.Location = new System.Drawing.Point(632, 236);
			this.labelTaxDate.Name = "labelTaxDate";
			this.labelTaxDate.Size = new System.Drawing.Size(156, 20);
			this.labelTaxDate.TabIndex = 102;
			// 
			// labelReason
			// 
			this.labelReason.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelReason.Location = new System.Drawing.Point(100, 260);
			this.labelReason.Name = "labelReason";
			this.labelReason.Size = new System.Drawing.Size(152, 20);
			this.labelReason.TabIndex = 101;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(-8, 260);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(103, 20);
			this.label8.TabIndex = 99;
			this.label8.Text = "Reason:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(508, 260);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(120, 20);
			this.label9.TabIndex = 105;
			this.label9.Text = "Service Mode:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelServiceMode
			// 
			this.labelServiceMode.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelServiceMode.Location = new System.Drawing.Point(632, 260);
			this.labelServiceMode.Name = "labelServiceMode";
			this.labelServiceMode.Size = new System.Drawing.Size(156, 20);
			this.labelServiceMode.TabIndex = 106;
			// 
			// labelCurrencyCode
			// 
			this.labelCurrencyCode.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelCurrencyCode.Location = new System.Drawing.Point(368, 260);
			this.labelCurrencyCode.Name = "labelCurrencyCode";
			this.labelCurrencyCode.Size = new System.Drawing.Size(132, 20);
			this.labelCurrencyCode.TabIndex = 104;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(260, 260);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(103, 20);
			this.label18.TabIndex = 103;
			this.label18.Text = "Currency Code:";
			this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// formGetTaxRequest
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(794, 472);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.labelServiceMode);
			this.Controls.Add(this.labelCurrencyCode);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.labelTaxDate);
			this.Controls.Add(this.labelReason);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.labelTaxAmount);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.labelLocationCode);
			this.Controls.Add(this.lbl_LocationCode);
			this.Controls.Add(this.labelCustomerUsageType);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.labelSalespersonCode);
			this.Controls.Add(this.labelCustomerCode);
			this.Controls.Add(this.labelDetailLevel);
			this.Controls.Add(this.labelExemptionNo);
			this.Controls.Add(this.labelDiscount);
			this.Controls.Add(this.labelDocType);
			this.Controls.Add(this.labelDocDate);
			this.Controls.Add(this.labelDocCode);
			this.Controls.Add(this.labelCompanyCode);
			this.Controls.Add(this.label28);
			this.Controls.Add(this.label29);
			this.Controls.Add(this.label30);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.gridLines);
			this.Controls.Add(this.gridAddresses);
			this.Controls.Add(this.label41);
			this.Controls.Add(this.label40);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.labelPurchaseOrderNo);
			this.Controls.Add(this.labelTaxOverrideType);
			this.Controls.Add(this.label4);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "formGetTaxRequest";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Load += new System.EventHandler(this.formGetTaxRequest_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridLines)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridAddresses)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		GetTaxRequest _getTaxRequest;
		DataTable _dataLines = new DataTable("Lines");
		DataTable _dataAddresses = new DataTable("Addresses");

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		public void LoadResultData()
		{
			//####################################################
			//### LOAD DATA FROM THE RESULT OBJECT INTO THE UI ###
			//####################################################
			labelCompanyCode.Text = _getTaxRequest.CompanyCode;
			labelDocCode.Text = _getTaxRequest.DocCode;
			labelDocDate.Text = _getTaxRequest.DocDate.ToShortDateString();
			labelDocType.Text = _getTaxRequest.DocType.ToString();
			labelDiscount.Text = _getTaxRequest.Discount.ToString();
			labelExemptionNo.Text = _getTaxRequest.ExemptionNo;
			labelDetailLevel.Text = _getTaxRequest.DetailLevel.ToString();
			labelCustomerCode.Text = _getTaxRequest.CustomerCode;
			labelCustomerUsageType.Text = _getTaxRequest.CustomerUsageType;
			labelSalespersonCode.Text = _getTaxRequest.SalespersonCode;
			labelLocationCode.Text = _getTaxRequest.LocationCode;
			labelPurchaseOrderNo.Text = _getTaxRequest.PurchaseOrderNo;
			//Added for 5.0
//			labelIsTaxOverriden.Text = _getTaxRequest.IsTotalTaxOverriden.ToString();
//			labelTaxOverride.Text = _getTaxRequest.TotalTaxOverride.ToString();

			//Added for 5.0.2
			labelTaxOverrideType.Text = _getTaxRequest.TaxOverride.TaxOverrideType.ToString();
			labelTaxAmount.Text = _getTaxRequest.TaxOverride.TaxAmount.ToString();
			labelTaxDate.Text = _getTaxRequest.TaxOverride.TaxDate.ToString();
			labelReason.Text = (_getTaxRequest.TaxOverride.Reason!=null)? _getTaxRequest.TaxOverride.Reason.ToString():"";
			labelCurrencyCode.Text = (_getTaxRequest.CurrencyCode!=null)? _getTaxRequest.CurrencyCode.ToString():"";
			labelServiceMode.Text = _getTaxRequest.ServiceMode.ToString();

			LoadLineData(_getTaxRequest);
			LoadAddressData(_getTaxRequest);
		}

		public void LoadAddressData(GetTaxRequest getTaxRequest)
		{
			//###########################################################
			//### ITERATE THROUGH ADDRESSES ON ORIGINAL GETTAXREQUEST ###
			//###########################################################
			FillAddressRow(getTaxRequest.OriginAddress, "Origin", "Request");
			FillAddressRow(getTaxRequest.DestinationAddress, "Destination", "Request");

			foreach (Line line in getTaxRequest.Lines)
			{
				FillAddressRow(line.OriginAddress, "Origin", string.Format("Line({0})", line.No));
				FillAddressRow(line.DestinationAddress, "Destination", string.Format("Line({0})", line.No));
			}
		}

		private void FillAddressRow(Address address, string type, string parent)
		{
			DataRow row;

			if (address != null)
			{
				row = _dataAddresses.NewRow();
				row[AddressColumns.Parent.ToString()] = parent;
				row[AddressColumns.Type.ToString()] = type;
				row[AddressColumns.Line1.ToString()] = address.Line1;
				row[AddressColumns.Line2.ToString()] = address.Line2;
				row[AddressColumns.Line3.ToString()] = address.Line3;
				row[AddressColumns.City.ToString()] = address.City;
				row[AddressColumns.Region.ToString()] = address.Region;
				row[AddressColumns.PostalCode.ToString()] = address.PostalCode;
				row[AddressColumns.Country.ToString()] = address.Country;

				//Added for 5.0
				row[AddressColumns.TaxRegionId.ToString()] = address.TaxRegionId; 
				_dataAddresses.Rows.Add(row);
			}
		}

		public void LoadLineData(GetTaxRequest getTaxRequest)
		{
			//#######################################################
			//### ITERATE THROUGH LINES ON ORIGINAL GETTAXREQUEST ###
			//#######################################################
			DataRow row;

			foreach (Line line in getTaxRequest.Lines)
			{
				row = _dataLines.NewRow();
				row[LineColumns.No.ToString()] = line.No;
				row[LineColumns.ItemCode.ToString()] = line.ItemCode;
				row[LineColumns.Qty.ToString()] = line.Qty.ToString();
				row[LineColumns.Amount.ToString()] = line.Amount.ToString();
				row[LineColumns.Discounted.ToString()] = line.Discounted.ToString();
				row[LineColumns.ExemptionNo.ToString()] = line.ExemptionNo;
				row[LineColumns.Reference1.ToString()] = line.Ref1;
				row[LineColumns.Reference2.ToString()] = line.Ref2;
				row[LineColumns.RevAcct.ToString()] = line.RevAcct;
				row[LineColumns.TaxCode.ToString()] = line.TaxCode;
				//Added for 5.0
//				row[LineColumns.IsTaxOverriden.ToString()] = line.IsTaxOverriden.ToString();
//				row[LineColumns.TaxOverride.ToString()] = line.TaxOverride;
				row[LineColumns.TaxOverrideType.ToString()] = line.TaxOverride.TaxOverrideType.ToString();
				row[LineColumns.TaxAmount.ToString()] = line.TaxOverride.TaxAmount.ToString();
				row[LineColumns.TaxDate.ToString()] = line.TaxOverride.TaxDate.ToString();
				row[LineColumns.Reason.ToString()] = line.TaxOverride.Reason;

				_dataLines.Rows.Add(row);
			}
		}
	
		public void SetupLineHeader()
		{
			//#####################
			//### UI SETUP CODE ###
			//#####################
			DataColumn col;

			col = new DataColumn(LineColumns.No.ToString(), typeof(string));
			col.Caption = LineColumns.No.ToString();
			_dataLines.Columns.Add(col);

			col = new DataColumn(LineColumns.ItemCode.ToString(), typeof(string));
			col.Caption = LineColumns.ItemCode.ToString();
			_dataLines.Columns.Add(col);

			col = new DataColumn(LineColumns.Qty.ToString(), typeof(Decimal));
			col.Caption = LineColumns.Qty.ToString();
			_dataLines.Columns.Add(col);

			col = new DataColumn(LineColumns.Amount.ToString(), typeof(Decimal));
			col.Caption = LineColumns.Amount.ToString();
			_dataLines.Columns.Add(col);

			col = new DataColumn(LineColumns.Discounted.ToString(), typeof(bool));
			col.Caption = LineColumns.Discounted.ToString();
			_dataLines.Columns.Add(col);			

			col = new DataColumn(LineColumns.ExemptionNo.ToString(), typeof(string));
			col.Caption = LineColumns.ExemptionNo.ToString();
			_dataLines.Columns.Add(col);

			col = new DataColumn(LineColumns.Reference1.ToString(), typeof(string));
			col.Caption = LineColumns.Reference1.ToString();
			_dataLines.Columns.Add(col);

			col = new DataColumn(LineColumns.Reference2.ToString(), typeof(string));
			col.Caption = LineColumns.Reference2.ToString();
			_dataLines.Columns.Add(col);

			col = new DataColumn(LineColumns.RevAcct.ToString(), typeof(string));
			col.Caption = LineColumns.RevAcct.ToString();
			_dataLines.Columns.Add(col);
			
			col = new DataColumn(LineColumns.TaxCode.ToString(), typeof(string));
			col.Caption = LineColumns.TaxCode.ToString();
			_dataLines.Columns.Add(col);

			//Added for 5.0
//			col = new DataColumn(LineColumns.IsTaxOverriden.ToString(), typeof(bool));
//			col.Caption = LineColumns.IsTaxOverriden.ToString();
//			_dataLines.Columns.Add(col);
//
//			col = new DataColumn(LineColumns.TaxOverride.ToString(), typeof(string));
//			col.Caption = LineColumns.TaxOverride.ToString();
//			_dataLines.Columns.Add(col);
			
			col = new DataColumn(LineColumns.TaxOverrideType.ToString(), typeof(string));
			col.Caption = LineColumns.TaxOverrideType.ToString();
			_dataLines.Columns.Add(col);

			col = new DataColumn(LineColumns.TaxAmount.ToString(), typeof(Decimal));
			col.Caption = LineColumns.TaxAmount.ToString();
			_dataLines.Columns.Add(col);

			col = new DataColumn(LineColumns.TaxDate.ToString(), typeof(DateTime));
			col.Caption = LineColumns.TaxDate.ToString();
			_dataLines.Columns.Add(col);

			col = new DataColumn(LineColumns.Reason.ToString(), typeof(string));
			col.Caption = LineColumns.Reason.ToString();
			_dataLines.Columns.Add(col);
		}

			
		public void SetupAddressHeader()
		{
			//#####################
			//### UI SETUP CODE ###
			//#####################
			DataColumn col;

			col = new DataColumn(AddressColumns.Parent.ToString(), typeof(string));
			col.Caption = AddressColumns.Parent.ToString();
			_dataAddresses.Columns.Add(col);

			col = new DataColumn(AddressColumns.Type.ToString(), typeof(string));
			col.Caption = AddressColumns.Type.ToString();
			_dataAddresses.Columns.Add(col);

			col = new DataColumn(AddressColumns.Line1.ToString(), typeof(string));
			col.Caption = AddressColumns.Line1.ToString();
			_dataAddresses.Columns.Add(col);

			col = new DataColumn(AddressColumns.Line2.ToString(), typeof(string));
			col.Caption = AddressColumns.Line2.ToString();
			_dataAddresses.Columns.Add(col);

			col = new DataColumn(AddressColumns.Line3.ToString(), typeof(string));
			col.Caption = AddressColumns.Line3.ToString();
			_dataAddresses.Columns.Add(col);

			col = new DataColumn(AddressColumns.City.ToString(), typeof(string));
			col.Caption = AddressColumns.City.ToString();
			_dataAddresses.Columns.Add(col);

			col = new DataColumn(AddressColumns.Region.ToString(), typeof(string));
			col.Caption = AddressColumns.Region.ToString();
			_dataAddresses.Columns.Add(col);

			col = new DataColumn(AddressColumns.PostalCode.ToString(), typeof(string));
			col.Caption = AddressColumns.PostalCode.ToString();
			_dataAddresses.Columns.Add(col);
			
			col = new DataColumn(AddressColumns.Country.ToString(), typeof(string));
			col.Caption = AddressColumns.Country.ToString();
			_dataAddresses.Columns.Add(col);

			//Added for 5.0
			col = new DataColumn(AddressColumns.TaxRegionId.ToString(), typeof(string));
			col.Caption = AddressColumns.TaxRegionId.ToString();
			_dataAddresses.Columns.Add(col);
		}

		private void formGetTaxRequest_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
