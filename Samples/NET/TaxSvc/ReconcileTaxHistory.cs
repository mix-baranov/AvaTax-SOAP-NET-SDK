using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Avalara.AvaTax.Adapter.TaxService;

namespace Avalara.AvaTax.Adapter.Samples.TaxSample
{
	/// <summary>
	/// Summary description for ReconcileTaxHistory.
	/// </summary>
	public class formReconcileTaxHistory : Form
	{
		public enum ResultColumns
		{
			ResultCode = 0,
			ResultMsg,
			DocStatus,
			DocStatusDate,
			Reconciled,
			TotalTaxable,
			TotalAmount,
			TotalTax
		}
		
		private Button buttonClose;
		private Label lblStatus;
		private Button buttonReconcileTaxHistory;
		private GroupBox groupBox2;
		private Button buttonViewGetTaxResult;
		private DataGrid gridResults;
		private Label label26;
		private Label label7;
		private Label lblResultCode;
		private Label label6;
		private Label label5;
		private GroupBox groupBox1;
		private TextBox textCompanyCode;
		private Label label4;
		private Label label3;
		private Label label1;
		private System.Windows.Forms.LinkLabel lblResultMsg;
		private System.Windows.Forms.Label lblLastDocCode;
		private System.Windows.Forms.TextBox textLastDocCode;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox comboDocStatus;
		private System.Windows.Forms.TextBox textEndDate;
		private System.Windows.Forms.TextBox textStartDate;
		private System.Windows.Forms.ComboBox comboDocType;
		private System.Windows.Forms.TextBox textPageSize;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label lblRecordCount;
		private System.Windows.Forms.Label label12;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public formReconcileTaxHistory()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//#####################
			//### UI SETUP CODE ###
			//#####################
			buttonViewGetTaxResult.Enabled = false;
			FillCombos();
			SetupResultsHeader();
			gridResults.SetDataBinding(_dataResults, "");
			
			textCompanyCode.Text = "DEFAULT";
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
			this.buttonClose = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.buttonReconcileTaxHistory = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lblResultMsg = new System.Windows.Forms.LinkLabel();
			this.buttonViewGetTaxResult = new System.Windows.Forms.Button();
			this.gridResults = new System.Windows.Forms.DataGrid();
			this.label26 = new System.Windows.Forms.Label();
			this.lblLastDocCode = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.lblResultCode = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textEndDate = new System.Windows.Forms.TextBox();
			this.textStartDate = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textLastDocCode = new System.Windows.Forms.TextBox();
			this.textCompanyCode = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.comboDocStatus = new System.Windows.Forms.ComboBox();
			this.comboDocType = new System.Windows.Forms.ComboBox();
			this.textPageSize = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.lblRecordCount = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridResults)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(403, 512);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(76, 24);
			this.buttonClose.TabIndex = 21;
			this.buttonClose.Text = "Close";
			// 
			// lblStatus
			// 
			this.lblStatus.ForeColor = System.Drawing.Color.Green;
			this.lblStatus.Location = new System.Drawing.Point(15, 512);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(260, 20);
			this.lblStatus.TabIndex = 13;
			// 
			// buttonReconcileTaxHistory
			// 
			this.buttonReconcileTaxHistory.Location = new System.Drawing.Point(178, 208);
			this.buttonReconcileTaxHistory.Name = "buttonReconcileTaxHistory";
			this.buttonReconcileTaxHistory.Size = new System.Drawing.Size(134, 24);
			this.buttonReconcileTaxHistory.TabIndex = 18;
			this.buttonReconcileTaxHistory.Text = "Reconcile Tax History";
			this.buttonReconcileTaxHistory.Click += new System.EventHandler(this.buttonReconcileTaxHistory_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.lblRecordCount);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.lblResultMsg);
			this.groupBox2.Controls.Add(this.buttonViewGetTaxResult);
			this.groupBox2.Controls.Add(this.gridResults);
			this.groupBox2.Controls.Add(this.label26);
			this.groupBox2.Controls.Add(this.lblLastDocCode);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.lblResultCode);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(7, 232);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(476, 272);
			this.groupBox2.TabIndex = 11;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Result";
			// 
			// lblResultMsg
			// 
			this.lblResultMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblResultMsg.Location = new System.Drawing.Point(100, 52);
			this.lblResultMsg.Name = "lblResultMsg";
			this.lblResultMsg.Size = new System.Drawing.Size(260, 20);
			this.lblResultMsg.TabIndex = 68;
			this.lblResultMsg.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblResultMsg_LinkClicked);
			// 
			// buttonViewGetTaxResult
			// 
			this.buttonViewGetTaxResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.buttonViewGetTaxResult.Location = new System.Drawing.Point(372, 244);
			this.buttonViewGetTaxResult.Name = "buttonViewGetTaxResult";
			this.buttonViewGetTaxResult.Size = new System.Drawing.Size(96, 20);
			this.buttonViewGetTaxResult.TabIndex = 20;
			this.buttonViewGetTaxResult.Text = "View Selected";
			this.buttonViewGetTaxResult.Click += new System.EventHandler(this.buttonViewGetTaxResult_Click);
			// 
			// gridResults
			// 
			this.gridResults.AllowSorting = false;
			this.gridResults.DataMember = "";
			this.gridResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.gridResults.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.gridResults.Location = new System.Drawing.Point(12, 140);
			this.gridResults.Name = "gridResults";
			this.gridResults.ReadOnly = true;
			this.gridResults.Size = new System.Drawing.Size(456, 97);
			this.gridResults.TabIndex = 19;
			// 
			// label26
			// 
			this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label26.Location = new System.Drawing.Point(12, 124);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(116, 20);
			this.label26.TabIndex = 65;
			this.label26.Text = "GetTaxResults";
			// 
			// lblLastDocCode
			// 
			this.lblLastDocCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblLastDocCode.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblLastDocCode.Location = new System.Drawing.Point(100, 76);
			this.lblLastDocCode.Name = "lblLastDocCode";
			this.lblLastDocCode.Size = new System.Drawing.Size(372, 20);
			this.lblLastDocCode.TabIndex = 5;
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(8, 76);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(88, 20);
			this.label7.TabIndex = 4;
			this.label7.Text = "Last Doc Code:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblResultCode
			// 
			this.lblResultCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblResultCode.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblResultCode.Location = new System.Drawing.Point(100, 28);
			this.lblResultCode.Name = "lblResultCode";
			this.lblResultCode.Size = new System.Drawing.Size(260, 20);
			this.lblResultCode.TabIndex = 2;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(8, 52);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(88, 20);
			this.label6.TabIndex = 1;
			this.label6.Text = "Result Msg:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(8, 28);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(88, 20);
			this.label5.TabIndex = 0;
			this.label5.Text = "Result Code:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textPageSize);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.comboDocType);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.textEndDate);
			this.groupBox1.Controls.Add(this.textStartDate);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.textLastDocCode);
			this.groupBox1.Controls.Add(this.textCompanyCode);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.comboDocStatus);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(40, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(408, 196);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Request";
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label9.Location = new System.Drawing.Point(24, 116);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(96, 20);
			this.label9.TabIndex = 12;
			this.label9.Text = "Doc Status:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textEndDate
			// 
			this.textEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textEndDate.Location = new System.Drawing.Point(124, 92);
			this.textEndDate.Name = "textEndDate";
			this.textEndDate.Size = new System.Drawing.Size(132, 20);
			this.textEndDate.TabIndex = 11;
			this.textEndDate.Text = "";
			// 
			// textStartDate
			// 
			this.textStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textStartDate.Location = new System.Drawing.Point(124, 68);
			this.textStartDate.Name = "textStartDate";
			this.textStartDate.Size = new System.Drawing.Size(132, 20);
			this.textStartDate.TabIndex = 10;
			this.textStartDate.Text = "";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(24, 92);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 20);
			this.label2.TabIndex = 9;
			this.label2.Text = "End Date:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label8.Location = new System.Drawing.Point(24, 68);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(96, 20);
			this.label8.TabIndex = 8;
			this.label8.Text = "Start Date:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(24, 140);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 20);
			this.label1.TabIndex = 6;
			this.label1.Text = "Doc Type:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textLastDocCode
			// 
			this.textLastDocCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textLastDocCode.Location = new System.Drawing.Point(124, 44);
			this.textLastDocCode.Name = "textLastDocCode";
			this.textLastDocCode.Size = new System.Drawing.Size(132, 20);
			this.textLastDocCode.TabIndex = 5;
			this.textLastDocCode.Text = "";
			// 
			// textCompanyCode
			// 
			this.textCompanyCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textCompanyCode.Location = new System.Drawing.Point(124, 20);
			this.textCompanyCode.Name = "textCompanyCode";
			this.textCompanyCode.Size = new System.Drawing.Size(132, 20);
			this.textCompanyCode.TabIndex = 4;
			this.textCompanyCode.Text = "";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(24, 44);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 20);
			this.label4.TabIndex = 2;
			this.label4.Text = "Last Doc Code:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(24, 20);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 20);
			this.label3.TabIndex = 1;
			this.label3.Text = "Company Code:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboDocStatus
			// 
			this.comboDocStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDocStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.comboDocStatus.Location = new System.Drawing.Point(124, 116);
			this.comboDocStatus.Name = "comboDocStatus";
			this.comboDocStatus.Size = new System.Drawing.Size(132, 21);
			this.comboDocStatus.TabIndex = 15;
			// 
			// comboDocType
			// 
			this.comboDocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDocType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.comboDocType.Location = new System.Drawing.Point(124, 140);
			this.comboDocType.Name = "comboDocType";
			this.comboDocType.Size = new System.Drawing.Size(132, 21);
			this.comboDocType.TabIndex = 16;
			// 
			// textPageSize
			// 
			this.textPageSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textPageSize.Location = new System.Drawing.Point(124, 164);
			this.textPageSize.Name = "textPageSize";
			this.textPageSize.Size = new System.Drawing.Size(132, 20);
			this.textPageSize.TabIndex = 17;
			this.textPageSize.Text = "";
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label10.Location = new System.Drawing.Point(24, 164);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(96, 20);
			this.label10.TabIndex = 17;
			this.label10.Text = "Page Size:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblRecordCount
			// 
			this.lblRecordCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblRecordCount.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblRecordCount.Location = new System.Drawing.Point(100, 100);
			this.lblRecordCount.Name = "lblRecordCount";
			this.lblRecordCount.Size = new System.Drawing.Size(260, 20);
			this.lblRecordCount.TabIndex = 70;
			// 
			// label12
			// 
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label12.Location = new System.Drawing.Point(8, 100);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(88, 20);
			this.label12.TabIndex = 69;
			this.label12.Text = "Record Count:";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// formReconcileTaxHistory
			// 
			this.AcceptButton = this.buttonReconcileTaxHistory;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(490, 540);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.buttonReconcileTaxHistory);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "formReconcileTaxHistory";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Reconcile Tax History";
			this.Load += new System.EventHandler(this.formReconcileTaxHistory_Load);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridResults)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	
		ReconcileTaxHistoryResult _reconcileTaxHistoryResult;
		DataTable _dataResults = new DataTable();

		private void FillCombos()
		{
			//#####################
			//### UI SETUP CODE ###
			//#####################
			comboDocStatus.Items.Add(DocStatus.Cancelled);
			comboDocStatus.Items.Add(DocStatus.Committed);
			comboDocStatus.Items.Add(DocStatus.Posted);
			comboDocStatus.Items.Add(DocStatus.Saved);
			
			comboDocType.Items.Add(DocumentType.SalesInvoice);
			comboDocType.Items.Add(DocumentType.PurchaseInvoice);
			comboDocType.Items.Add(DocumentType.ReturnInvoice);
			comboDocType.Items.Add(DocumentType.Any);
		}
		private void SetupResultsHeader()
		{
			//#####################
			//### UI SETUP CODE ###
			//#####################
			DataColumn col;

			col = new DataColumn(ResultColumns.ResultCode.ToString(), typeof(string));
			col.Caption = ResultColumns.ResultCode.ToString();
			_dataResults.Columns.Add(col);

			col = new DataColumn(ResultColumns.ResultMsg.ToString(), typeof(string));
			col.Caption = ResultColumns.ResultMsg.ToString();
			_dataResults.Columns.Add(col);

			col = new DataColumn(ResultColumns.DocStatus.ToString(), typeof(DocStatus));
			col.Caption = ResultColumns.DocStatus.ToString();
			_dataResults.Columns.Add(col);

			col = new DataColumn(ResultColumns.DocStatusDate.ToString(), typeof(string));
			col.Caption = ResultColumns.DocStatusDate.ToString();
			_dataResults.Columns.Add(col);

			col = new DataColumn(ResultColumns.Reconciled.ToString(), typeof(bool));
			col.Caption = ResultColumns.Reconciled.ToString();
			_dataResults.Columns.Add(col);

			col = new DataColumn(ResultColumns.TotalTaxable.ToString(), typeof(Decimal));
			col.Caption = ResultColumns.TotalTaxable.ToString();
			_dataResults.Columns.Add(col);

			col = new DataColumn(ResultColumns.TotalAmount.ToString(), typeof(Decimal));
			col.Caption = ResultColumns.TotalAmount.ToString();
			_dataResults.Columns.Add(col);

			col = new DataColumn(ResultColumns.TotalTax.ToString(), typeof(Decimal));
			col.Caption = ResultColumns.TotalTax.ToString();
			_dataResults.Columns.Add(col);
		}
	
		public void LoadGetTaxResultData()
		{
			//##############################################################
			//### LOAD DATA FROM THE RESULT OBJECT INTO THE UI           ###
			//### ITERATE THROUGH GETTAXRESULT DATA IN THE RESULT OBJECT ###
			//##############################################################
			_dataResults.Clear();
			DataRow row;

			foreach (GetTaxResult getTaxResult in _reconcileTaxHistoryResult.GetTaxResults)
			{
				row = _dataResults.NewRow();
				row[ResultColumns.ResultCode.ToString()] = getTaxResult.ResultCode.ToString();
				row[ResultColumns.ResultMsg.ToString()] = getTaxResult.Messages.Count.ToString() + " messages";
				row[ResultColumns.DocStatus.ToString()] = getTaxResult.DocStatus;
				row[ResultColumns.DocStatusDate.ToString()] = getTaxResult.Timestamp.ToString();
				row[ResultColumns.Reconciled.ToString()] = getTaxResult.Reconciled.ToString();
				row[ResultColumns.TotalTaxable.ToString()] = getTaxResult.TotalTaxable.ToString();
				row[ResultColumns.TotalAmount.ToString()] = getTaxResult.TotalAmount.ToString();
				row[ResultColumns.TotalTax.ToString()] = getTaxResult.TotalTax.ToString();
				_dataResults.Rows.Add(row);
			}

			buttonViewGetTaxResult.Enabled = (_reconcileTaxHistoryResult.GetTaxResults.Count > 0);
		}

		private void buttonReconcileTaxHistory_Click(object sender, EventArgs e)
		{
			try
			{ 
				//###################################################################
				//### 1st WE CREATE THE REQUEST OBJECT WITH DATA FOR OUR SEARCH ###
				//#################################################################
				ReconcileTaxHistoryRequest reconcileTaxHistoryRequest = new ReconcileTaxHistoryRequest();
		    
				//###########################################################
				//### 2nd WE LOAD THE REQUEST-LEVEL DATA INTO THE REQUEST ###
				//###########################################################
				reconcileTaxHistoryRequest.CompanyCode = textCompanyCode.Text;
				reconcileTaxHistoryRequest.LastDocCode = textLastDocCode.Text;
				reconcileTaxHistoryRequest.DocStatus = (DocStatus)comboDocStatus.SelectedItem;
				reconcileTaxHistoryRequest.DocType = (DocumentType)comboDocType.SelectedItem;
				if(textPageSize.Text != "")
				{
					reconcileTaxHistoryRequest.PageSize = int.Parse(textPageSize.Text);
				}
				if(IsDate(textStartDate.Text))
				{
					reconcileTaxHistoryRequest.StartDate = DateTime.Parse(textStartDate.Text);
				}
				if(IsDate(textEndDate.Text))
				{
					reconcileTaxHistoryRequest.EndDate = DateTime.Parse(textEndDate.Text);
				}
				//########################################################################################################
				//### 3rd WE INVOKE THE RECONCILETAXHISTORY() METHOD OF THE TAXSVC OBJECT AND GET BACK A RESULT OBJECT ###
				//########################################################################################################
				Util.PreMethodCall(this, lblStatus);
		    
				TaxSvc taxSvc = new TaxSvc();
				((formMain)this.Owner).SetConfig(taxSvc);              //set the Url and Security configuration
		    
				_reconcileTaxHistoryResult = taxSvc.ReconcileTaxHistory(reconcileTaxHistoryRequest);
		    
				Util.PostMethodCall(this, lblStatus);
		    
				//#####################################
				//### 4th WE READ THE RESULT OBJECT ###
				//#####################################
				lblResultCode.Text = _reconcileTaxHistoryResult.ResultCode.ToString();
				Util.SetMessageLabelText(lblResultMsg, _reconcileTaxHistoryResult);
				lblLastDocCode.Text = _reconcileTaxHistoryResult.LastDocCode;
				lblRecordCount.Text = _reconcileTaxHistoryResult.RecordCount.ToString();

				LoadGetTaxResultData();
		    
				reconcileTaxHistoryRequest = null;
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

		public static bool IsDate(Object obj)
		{
			string strDate = obj.ToString();
			try 
			{
				DateTime dt = DateTime.Parse(strDate);
				if(dt != DateTime.MinValue  && dt != DateTime.MaxValue)
					return true;
				return false;
			}
			catch
			{
				return false;
			}
		}

		private void buttonViewGetTaxResult_Click(object sender, EventArgs e)
		{
			if (gridResults.CurrentRowIndex >= 0)
			{
				GetTaxResult getTaxResult = _reconcileTaxHistoryResult.GetTaxResults[gridResults.CurrentRowIndex];
				formGetTaxResult frm = new formGetTaxResult(getTaxResult);
				frm.Owner = this;
				frm.ShowDialog();
			}
		}

		private void lblResultMsg_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			MessagesForm frm = new MessagesForm(_reconcileTaxHistoryResult.Messages);
			frm.Owner = this;
			frm.ShowDialog();
		}

		private void formReconcileTaxHistory_Load(object sender, System.EventArgs e)
		{
			comboDocStatus.SelectedIndex = 0;
			comboDocType.SelectedIndex = 0;
		}
	}
}
