using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Avalara.AvaTax.Adapter.AddressService;
using Avalara.AvaTax.Adapter.TaxService;

namespace Avalara.AvaTax.Adapter.Samples.TaxSample
{
	/// <summary>
	/// Enum for columns used to represent Line data
	/// </summary>
	public enum LineColumns
	{
		No = 0,
		ItemCode,
		Qty,
		Amount,
		Discounted,
		//Discount,
		ExemptionNo,
		Reference1,
		Reference2,
		RevAcct,
		TaxCode,
		CustomerUsageType,
		Description,
		//Added for 5.0
//		IsTaxOverriden,
//		TaxOverride
		TaxOverrideType,
		Reason,		
		TaxAmount,
		TaxDate
	}

	/// <summary>
	/// Summary description for GetTaxRequest.
	/// </summary>
	public class formGetTax : Form
	{
		#region controls
		private Button buttonGetTax;
		private Button buttonClose;
		private Label label1;
		private Label label3;
		private Label label4;
		private Label label5;
		private Label label6;
		private Label label7;
		private Label label8;
		private Label label9;
		private Label labelFromLine1;
		private Label labelFromLine3;
		private Label labelFromLine2;
		private Label labelFromCountry;
		private Label labelFromPostalCode;
		private Label labelFromRegion;
		private Label labelToCountry;
		private Label labelToPostalCode;
		private Label labelToRegion;
		private Label labelToCity;
		private Label labelToLine3;
		private Label labelToLine2;
		private Label labelToLine1;
		private Label label18;
		private Label label19;
		private Label label20;
		private Label label21;
		private Label label22;
		private Label label23;
		private Label label24;
		private Label label26;
		private Label label10;
		private Label label11;
		private Label label12;
		private Label label13;
		private Label label16;
		private Label label17;
		private Label label28;
		private Label label29;
		private Label label30;
		private ComboBox cboDetailLevel;
		private Button buttonShipToAddress;
		private Button buttonShipFromAddress;
		private Label lblStatus;
		private TextBox textCompanyCode;
		private TextBox textDocCode;
		private TextBox textDocDate;
		private TextBox textExemptionNo;
		private TextBox textDiscount;
		private TextBox textSalespersonCode;
		private TextBox textCustomerCode;
		private DataGrid gridLines;
		private System.Windows.Forms.TextBox textCustomerUsageType;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textPurchaseOrderNo;
		private System.Windows.Forms.Label lbl_LocationCode;
		private System.Windows.Forms.TextBox textLocationCode;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.CheckBox chkCommit;
		private System.Windows.Forms.ComboBox cboDocumentType;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.ComboBox cboTaxOverrideType;
		private System.Windows.Forms.TextBox textTaxAmount;
		private System.Windows.Forms.Label lbl_TaxAmount;
		private System.Windows.Forms.TextBox textReason;
		private System.Windows.Forms.TextBox textTaxDate;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.TextBox textCurrencyCode;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.Label labelFromCity;
		#endregion controls

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public formGetTax()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			FillDetailCombo();
			FillDocumentTypeCombo();
			Util.FillTaxOverrideTypeCombo(cboTaxOverrideType);
			//Util.FillServiceModeCombo(cboServiceMode);

			FillSampleData();
			
			SetupLineHeader();			
			SetupLineData();
			gridLines.SetDataBinding(_data, "");
			SetTableStyle();
			
			/*Set TabIndex dynamically
			int i=0;
			foreach(Control c in this.Controls)
			{
				Label l = c as Label;
				if (l==null)
				{
					c.TabStop=true;
					c.TabIndex=i;
					i++;
				}
			}
			textCompanyCode.Focus();
			*/
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
			this.buttonGetTax = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.labelFromLine1 = new System.Windows.Forms.Label();
			this.labelFromLine3 = new System.Windows.Forms.Label();
			this.labelFromLine2 = new System.Windows.Forms.Label();
			this.labelFromCountry = new System.Windows.Forms.Label();
			this.labelFromPostalCode = new System.Windows.Forms.Label();
			this.labelFromRegion = new System.Windows.Forms.Label();
			this.labelToCountry = new System.Windows.Forms.Label();
			this.labelToPostalCode = new System.Windows.Forms.Label();
			this.labelToRegion = new System.Windows.Forms.Label();
			this.labelToCity = new System.Windows.Forms.Label();
			this.labelToLine3 = new System.Windows.Forms.Label();
			this.labelToLine2 = new System.Windows.Forms.Label();
			this.labelToLine1 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.label30 = new System.Windows.Forms.Label();
			this.textCompanyCode = new System.Windows.Forms.TextBox();
			this.textDocCode = new System.Windows.Forms.TextBox();
			this.textDocDate = new System.Windows.Forms.TextBox();
			this.textExemptionNo = new System.Windows.Forms.TextBox();
			this.textDiscount = new System.Windows.Forms.TextBox();
			this.textSalespersonCode = new System.Windows.Forms.TextBox();
			this.textCustomerCode = new System.Windows.Forms.TextBox();
			this.cboDetailLevel = new System.Windows.Forms.ComboBox();
			this.buttonShipToAddress = new System.Windows.Forms.Button();
			this.buttonShipFromAddress = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.gridLines = new System.Windows.Forms.DataGrid();
			this.textCustomerUsageType = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.textPurchaseOrderNo = new System.Windows.Forms.TextBox();
			this.textLocationCode = new System.Windows.Forms.TextBox();
			this.lbl_LocationCode = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.chkCommit = new System.Windows.Forms.CheckBox();
			this.cboDocumentType = new System.Windows.Forms.ComboBox();
			this.textTaxAmount = new System.Windows.Forms.TextBox();
			this.lbl_TaxAmount = new System.Windows.Forms.Label();
			this.label31 = new System.Windows.Forms.Label();
			this.cboTaxOverrideType = new System.Windows.Forms.ComboBox();
			this.textReason = new System.Windows.Forms.TextBox();
			this.textTaxDate = new System.Windows.Forms.TextBox();
			this.label32 = new System.Windows.Forms.Label();
			this.label33 = new System.Windows.Forms.Label();
			this.textCurrencyCode = new System.Windows.Forms.TextBox();
			this.label34 = new System.Windows.Forms.Label();
			this.labelFromCity = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.gridLines)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonGetTax
			// 
			this.buttonGetTax.Location = new System.Drawing.Point(620, 508);
			this.buttonGetTax.Name = "buttonGetTax";
			this.buttonGetTax.Size = new System.Drawing.Size(80, 24);
			this.buttonGetTax.TabIndex = 4;
			this.buttonGetTax.Text = "Get Tax";
			this.buttonGetTax.Click += new System.EventHandler(this.buttonGetTax_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(708, 508);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(80, 24);
			this.buttonClose.TabIndex = 5;
			this.buttonClose.Text = "Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "Ship From:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 52);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84, 20);
			this.label3.TabIndex = 5;
			this.label3.Text = "Line 2:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(84, 20);
			this.label4.TabIndex = 4;
			this.label4.Text = "Line 1:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 132);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(84, 20);
			this.label5.TabIndex = 9;
			this.label5.Text = "Zip:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 112);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(84, 20);
			this.label6.TabIndex = 8;
			this.label6.Text = "State:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 92);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(84, 20);
			this.label7.TabIndex = 7;
			this.label7.Text = "City:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 72);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(84, 20);
			this.label8.TabIndex = 6;
			this.label8.Text = "Line 3:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 152);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(84, 20);
			this.label9.TabIndex = 10;
			this.label9.Text = "Country:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelFromLine1
			// 
			this.labelFromLine1.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelFromLine1.Location = new System.Drawing.Point(100, 32);
			this.labelFromLine1.Name = "labelFromLine1";
			this.labelFromLine1.Size = new System.Drawing.Size(289, 20);
			this.labelFromLine1.TabIndex = 12;
			// 
			// labelFromLine3
			// 
			this.labelFromLine3.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelFromLine3.Location = new System.Drawing.Point(100, 72);
			this.labelFromLine3.Name = "labelFromLine3";
			this.labelFromLine3.Size = new System.Drawing.Size(289, 20);
			this.labelFromLine3.TabIndex = 14;
			// 
			// labelFromLine2
			// 
			this.labelFromLine2.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelFromLine2.Location = new System.Drawing.Point(100, 52);
			this.labelFromLine2.Name = "labelFromLine2";
			this.labelFromLine2.Size = new System.Drawing.Size(289, 20);
			this.labelFromLine2.TabIndex = 13;
			// 
			// labelFromCountry
			// 
			this.labelFromCountry.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelFromCountry.Location = new System.Drawing.Point(100, 152);
			this.labelFromCountry.Name = "labelFromCountry";
			this.labelFromCountry.Size = new System.Drawing.Size(289, 20);
			this.labelFromCountry.TabIndex = 18;
			// 
			// labelFromPostalCode
			// 
			this.labelFromPostalCode.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelFromPostalCode.Location = new System.Drawing.Point(100, 132);
			this.labelFromPostalCode.Name = "labelFromPostalCode";
			this.labelFromPostalCode.Size = new System.Drawing.Size(289, 20);
			this.labelFromPostalCode.TabIndex = 17;
			// 
			// labelFromRegion
			// 
			this.labelFromRegion.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelFromRegion.Location = new System.Drawing.Point(100, 112);
			this.labelFromRegion.Name = "labelFromRegion";
			this.labelFromRegion.Size = new System.Drawing.Size(289, 20);
			this.labelFromRegion.TabIndex = 16;
			// 
			// labelToCountry
			// 
			this.labelToCountry.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelToCountry.Location = new System.Drawing.Point(496, 152);
			this.labelToCountry.Name = "labelToCountry";
			this.labelToCountry.Size = new System.Drawing.Size(289, 20);
			this.labelToCountry.TabIndex = 35;
			// 
			// labelToPostalCode
			// 
			this.labelToPostalCode.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelToPostalCode.Location = new System.Drawing.Point(496, 132);
			this.labelToPostalCode.Name = "labelToPostalCode";
			this.labelToPostalCode.Size = new System.Drawing.Size(289, 20);
			this.labelToPostalCode.TabIndex = 34;
			// 
			// labelToRegion
			// 
			this.labelToRegion.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelToRegion.Location = new System.Drawing.Point(496, 112);
			this.labelToRegion.Name = "labelToRegion";
			this.labelToRegion.Size = new System.Drawing.Size(289, 20);
			this.labelToRegion.TabIndex = 33;
			// 
			// labelToCity
			// 
			this.labelToCity.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelToCity.Location = new System.Drawing.Point(496, 92);
			this.labelToCity.Name = "labelToCity";
			this.labelToCity.Size = new System.Drawing.Size(289, 20);
			this.labelToCity.TabIndex = 32;
			// 
			// labelToLine3
			// 
			this.labelToLine3.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelToLine3.Location = new System.Drawing.Point(496, 72);
			this.labelToLine3.Name = "labelToLine3";
			this.labelToLine3.Size = new System.Drawing.Size(289, 20);
			this.labelToLine3.TabIndex = 31;
			// 
			// labelToLine2
			// 
			this.labelToLine2.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelToLine2.Location = new System.Drawing.Point(496, 52);
			this.labelToLine2.Name = "labelToLine2";
			this.labelToLine2.Size = new System.Drawing.Size(289, 20);
			this.labelToLine2.TabIndex = 30;
			// 
			// labelToLine1
			// 
			this.labelToLine1.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelToLine1.Location = new System.Drawing.Point(496, 32);
			this.labelToLine1.Name = "labelToLine1";
			this.labelToLine1.Size = new System.Drawing.Size(289, 20);
			this.labelToLine1.TabIndex = 29;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(404, 152);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(84, 20);
			this.label18.TabIndex = 27;
			this.label18.Text = "Country:";
			this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(404, 132);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(84, 20);
			this.label19.TabIndex = 26;
			this.label19.Text = "Zip:";
			this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(404, 112);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(84, 20);
			this.label20.TabIndex = 25;
			this.label20.Text = "State:";
			this.label20.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(404, 92);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(84, 20);
			this.label21.TabIndex = 24;
			this.label21.Text = "City:";
			this.label21.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(404, 72);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(84, 20);
			this.label22.TabIndex = 23;
			this.label22.Text = "Line 3:";
			this.label22.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(404, 52);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(84, 20);
			this.label23.TabIndex = 22;
			this.label23.Text = "Line 2:";
			this.label23.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(404, 32);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(84, 20);
			this.label24.TabIndex = 21;
			this.label24.Text = "Line 1:";
			this.label24.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label26
			// 
			this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label26.Location = new System.Drawing.Point(404, 8);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(84, 20);
			this.label26.TabIndex = 19;
			this.label26.Text = "Ship To:";
			this.label26.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(12, 200);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(104, 20);
			this.label10.TabIndex = 36;
			this.label10.Text = "Company Code:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(12, 224);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(104, 20);
			this.label11.TabIndex = 37;
			this.label11.Text = "Document Code:";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(12, 272);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(104, 20);
			this.label12.TabIndex = 39;
			this.label12.Text = "Document Type:";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(12, 248);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(104, 20);
			this.label13.TabIndex = 38;
			this.label13.Text = "Document Date:";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(280, 248);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(104, 20);
			this.label16.TabIndex = 41;
			this.label16.Text = "Exemption No:";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(548, 200);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(104, 20);
			this.label17.TabIndex = 40;
			this.label17.Text = "Discount:";
			this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(272, 296);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(112, 20);
			this.label28.TabIndex = 46;
			this.label28.Text = "Sales Person Code:";
			this.label28.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label29
			// 
			this.label29.Location = new System.Drawing.Point(280, 200);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(104, 20);
			this.label29.TabIndex = 45;
			this.label29.Text = "Customer Code:";
			this.label29.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label30
			// 
			this.label30.Location = new System.Drawing.Point(12, 320);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(104, 20);
			this.label30.TabIndex = 44;
			this.label30.Text = "Detail Level:";
			this.label30.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCompanyCode
			// 
			this.textCompanyCode.Location = new System.Drawing.Point(120, 200);
			this.textCompanyCode.Name = "textCompanyCode";
			this.textCompanyCode.Size = new System.Drawing.Size(131, 20);
			this.textCompanyCode.TabIndex = 8;
			this.textCompanyCode.Text = "";
			// 
			// textDocCode
			// 
			this.textDocCode.Location = new System.Drawing.Point(120, 224);
			this.textDocCode.Name = "textDocCode";
			this.textDocCode.Size = new System.Drawing.Size(131, 20);
			this.textDocCode.TabIndex = 9;
			this.textDocCode.Text = "";
			// 
			// textDocDate
			// 
			this.textDocDate.Location = new System.Drawing.Point(120, 248);
			this.textDocDate.Name = "textDocDate";
			this.textDocDate.Size = new System.Drawing.Size(131, 20);
			this.textDocDate.TabIndex = 10;
			this.textDocDate.Text = "";
			// 
			// textExemptionNo
			// 
			this.textExemptionNo.Location = new System.Drawing.Point(388, 248);
			this.textExemptionNo.Name = "textExemptionNo";
			this.textExemptionNo.Size = new System.Drawing.Size(131, 20);
			this.textExemptionNo.TabIndex = 23;
			this.textExemptionNo.Text = "";
			// 
			// textDiscount
			// 
			this.textDiscount.Location = new System.Drawing.Point(656, 200);
			this.textDiscount.Name = "textDiscount";
			this.textDiscount.Size = new System.Drawing.Size(131, 20);
			this.textDiscount.TabIndex = 27;
			this.textDiscount.Text = "";
			// 
			// textSalespersonCode
			// 
			this.textSalespersonCode.Location = new System.Drawing.Point(388, 296);
			this.textSalespersonCode.Name = "textSalespersonCode";
			this.textSalespersonCode.Size = new System.Drawing.Size(131, 20);
			this.textSalespersonCode.TabIndex = 25;
			this.textSalespersonCode.Text = "";
			// 
			// textCustomerCode
			// 
			this.textCustomerCode.Location = new System.Drawing.Point(388, 200);
			this.textCustomerCode.Name = "textCustomerCode";
			this.textCustomerCode.Size = new System.Drawing.Size(131, 20);
			this.textCustomerCode.TabIndex = 21;
			this.textCustomerCode.Text = "";
			// 
			// cboDetailLevel
			// 
			this.cboDetailLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDetailLevel.Location = new System.Drawing.Point(120, 316);
			this.cboDetailLevel.Name = "cboDetailLevel";
			this.cboDetailLevel.Size = new System.Drawing.Size(132, 21);
			this.cboDetailLevel.TabIndex = 13;
			// 
			// buttonShipToAddress
			// 
			this.buttonShipToAddress.Location = new System.Drawing.Point(496, 4);
			this.buttonShipToAddress.Name = "buttonShipToAddress";
			this.buttonShipToAddress.Size = new System.Drawing.Size(52, 20);
			this.buttonShipToAddress.TabIndex = 7;
			this.buttonShipToAddress.Text = "Edit";
			this.buttonShipToAddress.Click += new System.EventHandler(this.buttonShipToAddress_Click);
			// 
			// buttonShipFromAddress
			// 
			this.buttonShipFromAddress.Location = new System.Drawing.Point(100, 4);
			this.buttonShipFromAddress.Name = "buttonShipFromAddress";
			this.buttonShipFromAddress.Size = new System.Drawing.Size(52, 20);
			this.buttonShipFromAddress.TabIndex = 6;
			this.buttonShipFromAddress.Text = "Edit";
			this.buttonShipFromAddress.Click += new System.EventHandler(this.buttonShipFromAddress_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.ForeColor = System.Drawing.Color.Green;
			this.lblStatus.Location = new System.Drawing.Point(12, 512);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(596, 20);
			this.lblStatus.TabIndex = 62;
			// 
			// gridLines
			// 
			this.gridLines.AllowSorting = false;
			this.gridLines.DataMember = "";
			this.gridLines.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.gridLines.Location = new System.Drawing.Point(8, 364);
			this.gridLines.Name = "gridLines";
			this.gridLines.Size = new System.Drawing.Size(784, 132);
			this.gridLines.TabIndex = 33;
			// 
			// textCustomerUsageType
			// 
			this.textCustomerUsageType.Location = new System.Drawing.Point(388, 224);
			this.textCustomerUsageType.Name = "textCustomerUsageType";
			this.textCustomerUsageType.Size = new System.Drawing.Size(131, 20);
			this.textCustomerUsageType.TabIndex = 22;
			this.textCustomerUsageType.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(264, 224);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 20);
			this.label2.TabIndex = 64;
			this.label2.Text = "Customer/Use Type:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(276, 272);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(108, 20);
			this.label14.TabIndex = 57;
			this.label14.Text = "Purchase Order No:";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textPurchaseOrderNo
			// 
			this.textPurchaseOrderNo.Location = new System.Drawing.Point(388, 272);
			this.textPurchaseOrderNo.Name = "textPurchaseOrderNo";
			this.textPurchaseOrderNo.Size = new System.Drawing.Size(131, 20);
			this.textPurchaseOrderNo.TabIndex = 24;
			this.textPurchaseOrderNo.Text = "";
			// 
			// textLocationCode
			// 
			this.textLocationCode.Location = new System.Drawing.Point(388, 320);
			this.textLocationCode.Name = "textLocationCode";
			this.textLocationCode.Size = new System.Drawing.Size(131, 20);
			this.textLocationCode.TabIndex = 26;
			this.textLocationCode.Text = "";
			// 
			// lbl_LocationCode
			// 
			this.lbl_LocationCode.Location = new System.Drawing.Point(280, 320);
			this.lbl_LocationCode.Name = "lbl_LocationCode";
			this.lbl_LocationCode.Size = new System.Drawing.Size(104, 20);
			this.lbl_LocationCode.TabIndex = 66;
			this.lbl_LocationCode.Text = "Location Code:";
			this.lbl_LocationCode.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(12, 296);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(103, 20);
			this.label15.TabIndex = 79;
			this.label15.Text = "Commit:";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// chkCommit
			// 
			this.chkCommit.Location = new System.Drawing.Point(120, 296);
			this.chkCommit.Name = "chkCommit";
			this.chkCommit.Size = new System.Drawing.Size(16, 16);
			this.chkCommit.TabIndex = 12;
			// 
			// cboDocumentType
			// 
			this.cboDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDocumentType.Location = new System.Drawing.Point(120, 272);
			this.cboDocumentType.Name = "cboDocumentType";
			this.cboDocumentType.Size = new System.Drawing.Size(132, 21);
			this.cboDocumentType.TabIndex = 11;
			this.cboDocumentType.SelectedIndexChanged += new System.EventHandler(this.cboDocumentType_SelectedIndexChanged);
			// 
			// textTaxAmount
			// 
			this.textTaxAmount.Location = new System.Drawing.Point(656, 296);
			this.textTaxAmount.Name = "textTaxAmount";
			this.textTaxAmount.Size = new System.Drawing.Size(131, 20);
			this.textTaxAmount.TabIndex = 31;
			this.textTaxAmount.Text = "";
			// 
			// lbl_TaxAmount
			// 
			this.lbl_TaxAmount.Location = new System.Drawing.Point(536, 296);
			this.lbl_TaxAmount.Name = "lbl_TaxAmount";
			this.lbl_TaxAmount.Size = new System.Drawing.Size(116, 20);
			this.lbl_TaxAmount.TabIndex = 66;
			this.lbl_TaxAmount.Text = "Tax Override Amount:";
			this.lbl_TaxAmount.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label31
			// 
			this.label31.Location = new System.Drawing.Point(548, 248);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(103, 20);
			this.label31.TabIndex = 81;
			this.label31.Text = "Tax Override Type:";
			this.label31.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// cboTaxOverrideType
			// 
			this.cboTaxOverrideType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTaxOverrideType.Location = new System.Drawing.Point(656, 248);
			this.cboTaxOverrideType.Name = "cboTaxOverrideType";
			this.cboTaxOverrideType.Size = new System.Drawing.Size(132, 21);
			this.cboTaxOverrideType.TabIndex = 29;
			// 
			// textReason
			// 
			this.textReason.Location = new System.Drawing.Point(656, 272);
			this.textReason.Name = "textReason";
			this.textReason.Size = new System.Drawing.Size(131, 20);
			this.textReason.TabIndex = 30;
			this.textReason.Text = "";
			// 
			// textTaxDate
			// 
			this.textTaxDate.Location = new System.Drawing.Point(656, 320);
			this.textTaxDate.Name = "textTaxDate";
			this.textTaxDate.Size = new System.Drawing.Size(131, 20);
			this.textTaxDate.TabIndex = 32;
			this.textTaxDate.Text = "";
			// 
			// label32
			// 
			this.label32.Location = new System.Drawing.Point(536, 272);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(116, 20);
			this.label32.TabIndex = 85;
			this.label32.Text = "Tax Override Reason:";
			this.label32.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label33
			// 
			this.label33.Location = new System.Drawing.Point(548, 320);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(104, 20);
			this.label33.TabIndex = 84;
			this.label33.Text = "Tax Override Date:";
			this.label33.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textCurrencyCode
			// 
			this.textCurrencyCode.Location = new System.Drawing.Point(656, 224);
			this.textCurrencyCode.Name = "textCurrencyCode";
			this.textCurrencyCode.Size = new System.Drawing.Size(131, 20);
			this.textCurrencyCode.TabIndex = 28;
			this.textCurrencyCode.Text = "";
			// 
			// label34
			// 
			this.label34.Location = new System.Drawing.Point(548, 224);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(104, 20);
			this.label34.TabIndex = 87;
			this.label34.Text = "Currency Code:";
			this.label34.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelFromCity
			// 
			this.labelFromCity.BackColor = System.Drawing.SystemColors.Control;
			this.labelFromCity.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.labelFromCity.Location = new System.Drawing.Point(100, 92);
			this.labelFromCity.Name = "labelFromCity";
			this.labelFromCity.Size = new System.Drawing.Size(289, 20);
			this.labelFromCity.TabIndex = 15;
			// 
			// formGetTax
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(798, 548);
			this.Controls.Add(this.textCurrencyCode);
			this.Controls.Add(this.textReason);
			this.Controls.Add(this.textTaxDate);
			this.Controls.Add(this.textLocationCode);
			this.Controls.Add(this.textCustomerUsageType);
			this.Controls.Add(this.textSalespersonCode);
			this.Controls.Add(this.textCustomerCode);
			this.Controls.Add(this.textExemptionNo);
			this.Controls.Add(this.textDiscount);
			this.Controls.Add(this.textDocDate);
			this.Controls.Add(this.textDocCode);
			this.Controls.Add(this.textCompanyCode);
			this.Controls.Add(this.textPurchaseOrderNo);
			this.Controls.Add(this.textTaxAmount);
			this.Controls.Add(this.label34);
			this.Controls.Add(this.label32);
			this.Controls.Add(this.label33);
			this.Controls.Add(this.label31);
			this.Controls.Add(this.cboDocumentType);
			this.Controls.Add(this.chkCommit);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.lbl_LocationCode);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.gridLines);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.buttonShipToAddress);
			this.Controls.Add(this.buttonShipFromAddress);
			this.Controls.Add(this.cboDetailLevel);
			this.Controls.Add(this.label28);
			this.Controls.Add(this.label29);
			this.Controls.Add(this.label30);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.labelToCountry);
			this.Controls.Add(this.labelToPostalCode);
			this.Controls.Add(this.labelToRegion);
			this.Controls.Add(this.labelToCity);
			this.Controls.Add(this.labelToLine3);
			this.Controls.Add(this.labelToLine2);
			this.Controls.Add(this.labelToLine1);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.label20);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.label22);
			this.Controls.Add(this.label23);
			this.Controls.Add(this.label24);
			this.Controls.Add(this.label26);
			this.Controls.Add(this.labelFromCountry);
			this.Controls.Add(this.labelFromPostalCode);
			this.Controls.Add(this.labelFromRegion);
			this.Controls.Add(this.labelFromCity);
			this.Controls.Add(this.labelFromLine3);
			this.Controls.Add(this.labelFromLine2);
			this.Controls.Add(this.labelFromLine1);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonGetTax);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.lbl_TaxAmount);
			this.Controls.Add(this.cboTaxOverrideType);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "formGetTax";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Get Tax Request";
			((System.ComponentModel.ISupportInitialize)(this.gridLines)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		DataTable _data = new DataTable("Lines");

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void FillDetailCombo()
		{
			//#####################
			//### UI SETUP CODE ###
			//#####################
			cboDetailLevel.Items.Add(DetailLevel.Summary);
			cboDetailLevel.Items.Add(DetailLevel.Document);
			cboDetailLevel.Items.Add(DetailLevel.Line);
			cboDetailLevel.Items.Add(DetailLevel.Tax);
		}

		private void FillDocumentTypeCombo()
		{
			//#####################
			//### UI SETUP CODE ###
			//#####################
			cboDocumentType.Items.Add(DocumentType.SalesOrder);
			cboDocumentType.Items.Add(DocumentType.SalesInvoice);
			cboDocumentType.Items.Add(DocumentType.PurchaseOrder);
			cboDocumentType.Items.Add(DocumentType.PurchaseInvoice);
			cboDocumentType.Items.Add(DocumentType.ReturnOrder);
			cboDocumentType.Items.Add(DocumentType.ReturnInvoice);
		}

		public void FillSampleData()
		{
			//#####################
			//### UI SETUP CODE ###
			//#####################
			labelFromLine1.Text = "435 Ericksen Ave NE";
			labelFromLine2.Text = "";
			labelFromLine3.Text = "";
			labelFromCity.Text = "Bainbridge Island";
			labelFromRegion.Text = "WA";
			labelFromPostalCode.Text = "98110";
			labelFromCountry.Text = "US";

			labelToLine1.Text = "435 Ericksen Ave NE";
			labelToLine2.Text = "";
			labelToLine3.Text = "";
			labelToCity.Text = "Bainbridge Island";
			labelToRegion.Text = "WA";
			labelToPostalCode.Text = "98110";
			labelToCountry.Text= "US";
		    
			textCompanyCode.Text = "DEFAULT";
			textDocCode.Text = "DOC0001";
			textDocDate.Text = DateTime.Today.ToShortDateString();
			cboDocumentType.SelectedIndex = 1;
			textDiscount.Text = "0.00";
			textCustomerCode.Text = "CUST1";
			cboDetailLevel.SelectedIndex = 3;
			//Added for 5.0
			cboTaxOverrideType.SelectedIndex = 0;
			textTaxAmount.Text = "";
			textTaxDate.Text = "";
			textReason.Text = "";
			textCurrencyCode.Text = "";
			//cboServiceMode.SelectedIndex = 0;
		}

		public void SetupLineHeader()
		{
			//#####################
			//### UI SETUP CODE ###
			//#####################
			DataColumn col;

			col = new DataColumn(LineColumns.No.ToString(), typeof(string));
			col.Caption = LineColumns.No.ToString();			
			_data.Columns.Add(col);

			col = new DataColumn(LineColumns.ItemCode.ToString(), typeof(string));
			col.Caption = LineColumns.ItemCode.ToString();
			_data.Columns.Add(col);

			col = new DataColumn(LineColumns.Qty.ToString(), typeof(Decimal));
			col.Caption = LineColumns.Qty.ToString();
			_data.Columns.Add(col);

			col = new DataColumn(LineColumns.Amount.ToString(), typeof(Decimal));
			col.Caption = LineColumns.Amount.ToString();
			_data.Columns.Add(col);

			
			col = new DataColumn(LineColumns.Discounted.ToString(), typeof(bool));
			col.Caption = LineColumns.Discounted.ToString();
			_data.Columns.Add(col);
			col.DefaultValue = false;

			/*
			col = new DataColumn(LineColumns.Discount.ToString(),typeof(Decimal));
			col.Caption = LineColumns.Discount.ToString();
			_data.Columns.Add(col);
			*/

			col = new DataColumn(LineColumns.ExemptionNo.ToString(), typeof(string));
			col.Caption = LineColumns.ExemptionNo.ToString();
			_data.Columns.Add(col);

			col = new DataColumn(LineColumns.Reference1.ToString(), typeof(string));
			col.Caption = LineColumns.Reference1.ToString();
			_data.Columns.Add(col);

			col = new DataColumn(LineColumns.Reference2.ToString(), typeof(string));
			col.Caption = LineColumns.Reference2.ToString();
			_data.Columns.Add(col);

			col = new DataColumn(LineColumns.RevAcct.ToString(), typeof(string));
			col.Caption = LineColumns.RevAcct.ToString();
			_data.Columns.Add(col);

			col = new DataColumn(LineColumns.TaxCode.ToString(), typeof(string));
			col.Caption = LineColumns.TaxCode.ToString();
			_data.Columns.Add(col);

			col = new DataColumn(LineColumns.CustomerUsageType.ToString(), typeof(string));
			col.Caption = LineColumns.CustomerUsageType.ToString();
			_data.Columns.Add(col);
			
			col = new DataColumn(LineColumns.Description.ToString(), typeof(string));
			col.Caption = LineColumns.Description.ToString();
			_data.Columns.Add(col);

			//Added for 5.0
//			col = new DataColumn(LineColumns.IsTaxOverriden.ToString(), typeof(bool));
//			col.Caption = LineColumns.IsTaxOverriden.ToString();
//			_data.Columns.Add(col);
//
//			col = new DataColumn(LineColumns.TaxOverride.ToString(), typeof(string));
//			col.Caption = LineColumns.TaxOverride.ToString();
//			_data.Columns.Add(col);

			col = new DataColumn(LineColumns.TaxOverrideType.ToString(), typeof(string));
			col.Caption = LineColumns.TaxOverrideType.ToString();
			col.DefaultValue = "None";
			_data.Columns.Add(col);

			col = new DataColumn(LineColumns.Reason.ToString(), typeof(string));
			col.Caption = LineColumns.Reason.ToString();			
			_data.Columns.Add(col);

			col = new DataColumn(LineColumns.TaxAmount.ToString(), typeof(Decimal));
			col.Caption = LineColumns.TaxAmount.ToString();
			
			_data.Columns.Add(col);
			
			col = new DataColumn(LineColumns.TaxDate.ToString(), typeof(string));//DateTime
			col.Caption = LineColumns.TaxDate.ToString();
			_data.Columns.Add(col);

		}

		public void SetupLineData()
		{
			//#####################
			//### UI SETUP CODE ###
			//#####################
			DataRow row;

			row = _data.NewRow();
			row[LineColumns.No.ToString()] = "1";
			row[LineColumns.ItemCode.ToString()] = "SAMPLE01";
			row[LineColumns.Qty.ToString()] = 1.00m;
			row[LineColumns.Amount.ToString()] = 10.00m;
			row[LineColumns.Discounted.ToString()] = false;
			//row[LineColumns.Discount.ToString()] = 0.0;
			row[LineColumns.ExemptionNo.ToString()] = "";
			row[LineColumns.Reference1.ToString()] = "";
			row[LineColumns.Reference2.ToString()] = "";
			row[LineColumns.RevAcct.ToString()] = "";
			row[LineColumns.TaxCode.ToString()] = "TAXCODE01";
			row[LineColumns.CustomerUsageType.ToString()] = "";
			row[LineColumns.Description.ToString()] = "";
			//Added for 5.0
//			row[LineColumns.IsTaxOverriden.ToString()] = false;
//			row[LineColumns.TaxOverride.ToString()] = "";
			row[LineColumns.TaxOverrideType.ToString()] = "None";
			row[LineColumns.TaxAmount.ToString()] = 0.0m;
			row[LineColumns.TaxDate.ToString()] = "";//DateTime.Today.ToShortDateString();
			row[LineColumns.Reason.ToString()] = "";
			_data.Rows.Add(row);

			row = _data.NewRow();
			row[LineColumns.No.ToString()] = "2";
			row[LineColumns.ItemCode.ToString()] = "SAMPLE02";
			row[LineColumns.Qty.ToString()] = 1.00m;
			row[LineColumns.Amount.ToString()] = 40.00m;
			row[LineColumns.Discounted.ToString()] = false;
			//row[LineColumns.Discount.ToString()] = 0.0;
			row[LineColumns.ExemptionNo.ToString()] = "";
			row[LineColumns.Reference1.ToString()] = "";
			row[LineColumns.Reference2.ToString()] = "";
			row[LineColumns.RevAcct.ToString()] = "";
			row[LineColumns.TaxCode.ToString()] = "TAXCODE02";
			row[LineColumns.CustomerUsageType.ToString()] = "";
			row[LineColumns.Description.ToString()] = "";
			//Added for 5.0
			row[LineColumns.TaxOverrideType.ToString()] = "None";
			row[LineColumns.TaxAmount.ToString()] = 0.0m;
			row[LineColumns.TaxDate.ToString()] = "";
			row[LineColumns.Reason.ToString()] = "";
			_data.Rows.Add(row);
		}
	
		private void LoadShipFromAddress(Address address) {
			address.Line1 = labelFromLine1.Text;
			address.Line2 = labelFromLine2.Text;
			address.Line3 = labelFromLine3.Text;
			address.City = labelFromCity.Text;
			address.Region = labelFromRegion.Text;
			address.PostalCode = labelFromPostalCode.Text;
			address.Country = labelFromCountry.Text;
		}

		private void LoadShipToAddress(Address address) {
			address.Line1 = labelToLine1.Text;
			address.Line2 = labelToLine2.Text;
			address.Line3 = labelToLine3.Text;
			address.City = labelToCity.Text;
			address.Region = labelToRegion.Text;
			address.PostalCode = labelToPostalCode.Text;
			address.Country = labelToCountry.Text;
		}

		private void LoadLineItem(DataRow row, Line line)
		{
			line.No = row[LineColumns.No.ToString()].ToString();
			line.ItemCode = row[LineColumns.ItemCode.ToString()].ToString();
			line.Qty = Convert.ToDouble(row[LineColumns.Qty.ToString()]);
			line.Amount = Convert.ToDecimal(row[LineColumns.Amount.ToString()]);
			line.Discounted = Convert.ToBoolean(row[LineColumns.Discounted.ToString()]);
			//line.Discount = Convert.ToDecimal(row[LineColumns.Discount.ToString()]);
			line.ExemptionNo = row[LineColumns.ExemptionNo.ToString()].ToString();
			line.Ref1 = row[LineColumns.Reference1.ToString()].ToString();
			line.Ref2 = row[LineColumns.Reference2.ToString()].ToString();
			line.RevAcct = row[LineColumns.RevAcct.ToString()].ToString();
			line.TaxCode = row[LineColumns.TaxCode.ToString()].ToString();
			line.CustomerUsageType = row[LineColumns.CustomerUsageType.ToString()].ToString();
			line.Description = row[LineColumns.Description.ToString()].ToString();
			//Added in 5.0
			//line.TaxOverride.TaxOverrideType = (TaxOverrideType)row[LineColumns.TaxOverrideType.ToString()];
			line.TaxOverride.TaxOverrideType = Util.GetTaxOverrideType(row[LineColumns.TaxOverrideType.ToString()].ToString());
			line.TaxOverride.TaxAmount = (row[LineColumns.TaxAmount.ToString()].ToString() == "") ? 0 : decimal.Parse(row[LineColumns.TaxAmount.ToString()].ToString());			
			//line.TaxOverride.TaxAmount = decimal.Parse(row[LineColumns.TaxAmount.ToString()].ToString());
			//TaxDate value can't be null so assign it to MinValue
			//line.TaxOverride.TaxDate = DateTime.Parse(row[LineColumns.TaxDate.ToString()].ToString());
			line.TaxOverride.TaxDate = (row[LineColumns.TaxDate.ToString()].ToString()=="") ? new DateTime(1900,1,1) : DateTime.Parse(row[LineColumns.TaxDate.ToString()].ToString());
			line.TaxOverride.Reason = row[LineColumns.Reason.ToString()].ToString();
		}

		private void buttonGetTax_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (!Util.IsValidDateTime(textDocDate.Text))
				{
					MessageBox.Show("Invalid date/time value.", "Invalid Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
					textDocDate.Focus();
					return;
				}

				//#################################################################################
				//### 1st WE CREATE THE REQUEST OBJECT FOR THE ORDER THAT NEEDS TAX CALCULATION ###
				//#################################################################################
				GetTaxRequest getTaxRequest = new GetTaxRequest();
		    
				//##############################################################################
				//### 2nd WE HAVE TO LOAD ADDRESS INFORMATION FOR THE ORDER INTO THE REQUEST ###
				//##############################################################################
				Address address;
		    
				address = new Address();              //Ship From
				LoadShipFromAddress(address);
				getTaxRequest.OriginAddress = address;
		    
				address = new Address();              //Ship To
				LoadShipToAddress(address);
				getTaxRequest.DestinationAddress = address;
		    
				address = null;
		    
				//##################################################################
				//### 3rd WE LOAD THE LINE(S) WITH ITEM DATA INTO THE REQUEST    ###
				//### *we are not loading addresses for the individual lines     ###
				//### *they will default to the addresses on the GetTaxRequest   ###
				//##################################################################
				Line line;
		    
				foreach (DataRow row in _data.Rows)
				{
					line = new Line();
					LoadLineItem(row, line);
					getTaxRequest.Lines.Add(line);
				}
				line = null;
		    
				//###########################################################
				//### 4th WE LOAD THE REQUEST-LEVEL DATA INTO THE REQUEST ###
				//###########################################################
				getTaxRequest.CompanyCode = textCompanyCode.Text;
				getTaxRequest.DocCode = textDocCode.Text;
				getTaxRequest.DocDate = Convert.ToDateTime(textDocDate.Text);
				getTaxRequest.DocType = (DocumentType)cboDocumentType.SelectedItem;
				getTaxRequest.Discount = Convert.ToDecimal(textDiscount.Text);
				getTaxRequest.ExemptionNo = textExemptionNo.Text;
				getTaxRequest.DetailLevel = (DetailLevel)cboDetailLevel.SelectedItem;
				getTaxRequest.CustomerCode = textCustomerCode.Text;
				getTaxRequest.CustomerUsageType = textCustomerUsageType.Text;
				getTaxRequest.SalespersonCode = textSalespersonCode.Text;
				getTaxRequest.PurchaseOrderNo = textPurchaseOrderNo.Text;
				getTaxRequest.LocationCode = textLocationCode.Text;				
				getTaxRequest.Commit = chkCommit.Checked;
				//Added for 5.0
//				getTaxRequest.IsTotalTaxOverriden = chkIsTaxOverriden.Checked;
//				getTaxRequest.TotalTaxOverride = (textTaxOverride.Text == "") ? 0 : decimal.Parse(textTaxOverride.Text);				

				//getTaxRequest.TaxOverride = new TaxOverride();
				getTaxRequest.TaxOverride.TaxOverrideType = (TaxOverrideType)cboTaxOverrideType.SelectedItem;
				getTaxRequest.TaxOverride.TaxAmount = (textTaxAmount.Text == "") ? 0 : decimal.Parse(textTaxAmount.Text);
				getTaxRequest.TaxOverride.TaxDate = (textTaxDate.Text!="") ? DateTime.Parse(textTaxDate.Text):getTaxRequest.DocDate;
				getTaxRequest.TaxOverride.Reason = textReason.Text;
				getTaxRequest.CurrencyCode = textCurrencyCode.Text;
				//getTaxRequest.ServiceMode = (ServiceMode)cboServiceMode.SelectedItem;

				//###########################################################################################
				//### 5th WE INVOKE THE GETTAX() METHOD OF THE TAXSVC OBJECT AND GET BACK A RESULT OBJECT ###
				//###########################################################################################	    
				Util.PreMethodCall(this, lblStatus);
		    
				TaxSvc taxSvc = new TaxSvc();
				((formMain)this.Owner).SetConfig(taxSvc) ;              //set the Url and Security configuration
		    
				GetTaxResult getTaxResult = taxSvc.GetTax(getTaxRequest);
		    
				Util.PostMethodCall(this, lblStatus);
		    
				//#######################################
				//### 6th WE REVIEW THE RESULT OBJECT ###
				//#######################################
				formGetTaxResult frm = new formGetTaxResult(getTaxResult);
				frm.ShowDialog();
				frm = null;

				getTaxResult = null;
				getTaxRequest = null;
				taxSvc = null;
			}
			catch (Exception ex)
			{
				Util.ShowError(ex);
			}
			finally
			{
				Util.PostMethodCall(this, lblStatus);
			}
		}

		private void buttonShipFromAddress_Click(object sender, System.EventArgs e)
		{
			Address address = new Address();
			address.Line1 = labelFromLine1.Text;
			address.Line2 = labelFromLine2.Text;
			address.Line3 = labelFromLine3.Text;
			address.City = labelFromCity.Text;
			address.Region = labelFromRegion.Text;
			address.PostalCode = labelFromPostalCode.Text;
			address.Country = labelFromCountry.Text;

			formAddress frm = new formAddress(address);
			frm.Owner = this;
			frm.ShowDialog();

			labelFromLine1.Text = address.Line1;
			labelFromLine2.Text = address.Line2;
			labelFromLine3.Text = address.Line3;
			labelFromCity.Text = address.City;
			labelFromRegion.Text = address.Region;
			labelFromPostalCode.Text = address.PostalCode;
			labelFromCountry.Text = address.Country;
		}

		private void buttonShipToAddress_Click(object sender, System.EventArgs e)
		{
			Address address = new Address();
			address.Line1 = labelToLine1.Text;
			address.Line2 = labelToLine2.Text;
			address.Line3 = labelToLine3.Text;
			address.City = labelToCity.Text;
			address.Region = labelToRegion.Text;
			address.PostalCode = labelToPostalCode.Text;
			address.Country = labelToCountry.Text;

			formAddress frm = new formAddress(address);
			frm.Owner = this;
			frm.ShowDialog();

			labelToLine1.Text = address.Line1;
			labelToLine2.Text = address.Line2;
			labelToLine3.Text = address.Line3;
			labelToCity.Text = address.City;
			labelToRegion.Text = address.Region;
			labelToPostalCode.Text = address.PostalCode;
			labelToCountry.Text = address.Country;
		}

		private void cboDocumentType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(cboDocumentType.SelectedIndex == 0 | cboDocumentType.SelectedIndex == 2 | cboDocumentType.SelectedIndex == 4)
			{
				chkCommit.Enabled = false;				
			}
			else
			{
				chkCommit.Enabled = true;
			}
		}

		private void SetTableStyle()
		{
			DataGridTableStyle ts = new DataGridTableStyle ();
			DataGridColumnStyle textCol = new DataGridTextBoxColumn();
			textCol.MappingName = "No";
			textCol.HeaderText = "No";
			textCol.Width = 30;
			ts.GridColumnStyles.Add(textCol);
			
			textCol = new DataGridTextBoxColumn();
			textCol.MappingName = "ItemCode";
			textCol.HeaderText = "Item Code";
			textCol.Width = 80;
			ts.GridColumnStyles.Add(textCol);
			
			textCol = new DataGridTextBoxColumn();
			textCol.MappingName = "Qty";
			textCol.HeaderText = "Quantity";
			textCol.Width = 50;
			ts.GridColumnStyles.Add(textCol);
			
			textCol = new DataGridTextBoxColumn();
			textCol.MappingName = "Amount";
			textCol.HeaderText = "Amount";
			textCol.Width = 50;
			ts.GridColumnStyles.Add(textCol);

			DataGridColumnStyle boolCol = new DataGridBoolColumn();
			boolCol.MappingName = "Discounted";
			boolCol.HeaderText = "Discounted";
			boolCol.Width = 50;
			ts.GridColumnStyles.Add(boolCol);
			
			textCol = new DataGridTextBoxColumn();
			textCol.MappingName = "ExemptionNo";
			textCol.HeaderText = "Exemption No";
			textCol.Width = 80;
			ts.GridColumnStyles.Add(textCol);
			
			textCol = new DataGridTextBoxColumn();
			textCol.MappingName = "Reference1";
			textCol.HeaderText = "Reference1";
			textCol.Width = 80;
			ts.GridColumnStyles.Add(textCol);
			
			textCol = new DataGridTextBoxColumn();
			textCol.MappingName = "Reference2";
			textCol.HeaderText = "Reference2";
			textCol.Width = 80;
			ts.GridColumnStyles.Add(textCol);
		
			textCol = new DataGridTextBoxColumn();
			textCol.MappingName = "RevAcct";
			textCol.HeaderText = "Rev Acct";
			textCol.Width = 80;
			ts.GridColumnStyles.Add(textCol);
		
			textCol = new DataGridTextBoxColumn();
			textCol.MappingName = "TaxCode";
			textCol.HeaderText = "Tax Code";
			textCol.Width = 80;
			ts.GridColumnStyles.Add(textCol);
		
			textCol = new DataGridTextBoxColumn();
			textCol.MappingName = "CustomerUsageType";
			textCol.HeaderText = "Customer/UseType";
			textCol.Width = 120;
			ts.GridColumnStyles.Add(textCol);
		
			textCol = new DataGridTextBoxColumn();
			textCol.MappingName = "Description";
			textCol.HeaderText = "Description";
			textCol.Width = 100;
			ts.GridColumnStyles.Add(textCol);


			DataGridComboBoxColumn comboCol = new DataGridComboBoxColumn();
			comboCol.MappingName = "TaxOverrideType";
			comboCol.HeaderText = "Tax Override Type";
			comboCol.Width = 100;
			comboCol.DataSource=Enum.GetNames(typeof(TaxOverrideType));			
			ts.GridColumnStyles.Add(comboCol);

		
			textCol = new DataGridTextBoxColumn();		
			textCol.MappingName = "Reason";
			textCol.HeaderText = "Tax Override Reason";
			textCol.Width = 120;
			ts.GridColumnStyles.Add(textCol);
		
			textCol = new DataGridTextBoxColumn();
			textCol.MappingName = "TaxAmount";
			textCol.HeaderText = "Tax Override Amount";
			textCol.Width = 120;
			ts.GridColumnStyles.Add(textCol);
		
			textCol = new DataGridTextBoxColumn();
			textCol.MappingName = "TaxDate";
			textCol.HeaderText = "Tax Override Date";
			textCol.Width = 120;
			ts.GridColumnStyles.Add(textCol);
			
			ts.MappingName = _data.TableName;
			gridLines.TableStyles.Add(ts);
		}
	}
}