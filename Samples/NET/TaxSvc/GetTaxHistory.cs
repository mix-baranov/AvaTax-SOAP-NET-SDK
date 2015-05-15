using System;
using System.ComponentModel;
using System.Windows.Forms;
using Avalara.AvaTax.Adapter.TaxService;

namespace Avalara.AvaTax.Adapter.Samples.TaxSample
{
	/// <summary>
	/// Summary description for GetTaxHistory.
	/// </summary>
	public class formGetTaxHistory : Form
	{
		private Button buttonClose;
		private Label lblStatus;
		private Button buttonGetTaxHistory;
		private GroupBox groupBox2;
		private Label lblResultCode;
		private Label label6;
		private Label label5;
		private GroupBox groupBox1;
		private TextBox textDocCode;
		private TextBox textCompanyCode;
		private Label label4;
		private Label label3;
		private ComboBox cboDetailLevel;
		private Label label2;
		private Button buttonGetTaxRequest;
		private Button buttonGetTaxResult;
		private LinkLabel lblResultMsg;
		private System.Windows.Forms.ComboBox comboDocType;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label13;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public formGetTaxHistory()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//#####################
			//### UI SETUP CODE ###
			//#####################
			buttonGetTaxRequest.Enabled = false;
			buttonGetTaxResult.Enabled = false;
			FillCombos();

			textCompanyCode.Text = "DEFAULT";
			textDocCode.Text = "DOC0001";
			cboDetailLevel.SelectedIndex = 3;
			comboDocType.SelectedIndex = 0;

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
			this.buttonGetTaxHistory = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lblResultMsg = new System.Windows.Forms.LinkLabel();
			this.lblResultCode = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboDocType = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cboDetailLevel = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textDocCode = new System.Windows.Forms.TextBox();
			this.textCompanyCode = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonGetTaxRequest = new System.Windows.Forms.Button();
			this.buttonGetTaxResult = new System.Windows.Forms.Button();
			this.label13 = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(280, 396);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(76, 24);
			this.buttonClose.TabIndex = 100;
			this.buttonClose.Text = "Close";
			// 
			// lblStatus
			// 
			this.lblStatus.ForeColor = System.Drawing.Color.Green;
			this.lblStatus.Location = new System.Drawing.Point(12, 400);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(260, 20);
			this.lblStatus.TabIndex = 8;
			// 
			// buttonGetTaxHistory
			// 
			this.buttonGetTaxHistory.Location = new System.Drawing.Point(132, 224);
			this.buttonGetTaxHistory.Name = "buttonGetTaxHistory";
			this.buttonGetTaxHistory.Size = new System.Drawing.Size(100, 24);
			this.buttonGetTaxHistory.TabIndex = 11;
			this.buttonGetTaxHistory.Text = "Get Tax History";
			this.buttonGetTaxHistory.Click += new System.EventHandler(this.buttonGetTaxHistory_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.lblResultMsg);
			this.groupBox2.Controls.Add(this.lblResultCode);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(8, 264);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(352, 84);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Result";
			// 
			// lblResultMsg
			// 
			this.lblResultMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblResultMsg.Location = new System.Drawing.Point(84, 52);
			this.lblResultMsg.Name = "lblResultMsg";
			this.lblResultMsg.Size = new System.Drawing.Size(260, 20);
			this.lblResultMsg.TabIndex = 4;
			this.lblResultMsg.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblResultMsg_LinkClicked);
			// 
			// lblResultCode
			// 
			this.lblResultCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblResultCode.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblResultCode.Location = new System.Drawing.Point(84, 28);
			this.lblResultCode.Name = "lblResultCode";
			this.lblResultCode.Size = new System.Drawing.Size(260, 20);
			this.lblResultCode.TabIndex = 2;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(8, 52);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(72, 20);
			this.label6.TabIndex = 1;
			this.label6.Text = "Result Msg:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(8, 28);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 20);
			this.label5.TabIndex = 0;
			this.label5.Text = "Result Code:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Controls.Add(this.comboDocType);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.cboDetailLevel);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.textDocCode);
			this.groupBox1.Controls.Add(this.textCompanyCode);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(32, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(300, 196);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Request";
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label13.Location = new System.Drawing.Point(28, 120);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(228, 20);
			this.label13.TabIndex = 20;
			this.label13.Text = "________________________________________";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboDocType
			// 
			this.comboDocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDocType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.comboDocType.Location = new System.Drawing.Point(124, 92);
			this.comboDocType.Name = "comboDocType";
			this.comboDocType.Size = new System.Drawing.Size(132, 21);
			this.comboDocType.TabIndex = 8;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(24, 92);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 20);
			this.label1.TabIndex = 7;
			this.label1.Text = "Document Type:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// cboDetailLevel
			// 
			this.cboDetailLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDetailLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboDetailLevel.Location = new System.Drawing.Point(124, 148);
			this.cboDetailLevel.Name = "cboDetailLevel";
			this.cboDetailLevel.Size = new System.Drawing.Size(132, 21);
			this.cboDetailLevel.TabIndex = 10;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(24, 148);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 20);
			this.label2.TabIndex = 9;
			this.label2.Text = "Detail Level:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDocCode
			// 
			this.textDocCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textDocCode.Location = new System.Drawing.Point(124, 68);
			this.textDocCode.Name = "textDocCode";
			this.textDocCode.Size = new System.Drawing.Size(132, 20);
			this.textDocCode.TabIndex = 6;
			this.textDocCode.Text = "";
			// 
			// textCompanyCode
			// 
			this.textCompanyCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textCompanyCode.Location = new System.Drawing.Point(124, 44);
			this.textCompanyCode.Name = "textCompanyCode";
			this.textCompanyCode.Size = new System.Drawing.Size(132, 20);
			this.textCompanyCode.TabIndex = 4;
			this.textCompanyCode.Text = "";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(24, 68);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 20);
			this.label4.TabIndex = 5;
			this.label4.Text = "Document Code:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(24, 44);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 20);
			this.label3.TabIndex = 3;
			this.label3.Text = "Company Code:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// buttonGetTaxRequest
			// 
			this.buttonGetTaxRequest.Location = new System.Drawing.Point(60, 356);
			this.buttonGetTaxRequest.Name = "buttonGetTaxRequest";
			this.buttonGetTaxRequest.Size = new System.Drawing.Size(124, 24);
			this.buttonGetTaxRequest.TabIndex = 50;
			this.buttonGetTaxRequest.Text = "View GetTaxRequest";
			this.buttonGetTaxRequest.Click += new System.EventHandler(this.buttonGetTaxRequest_Click);
			// 
			// buttonGetTaxResult
			// 
			this.buttonGetTaxResult.Location = new System.Drawing.Point(188, 356);
			this.buttonGetTaxResult.Name = "buttonGetTaxResult";
			this.buttonGetTaxResult.Size = new System.Drawing.Size(124, 24);
			this.buttonGetTaxResult.TabIndex = 51;
			this.buttonGetTaxResult.Text = "View GetTaxResult";
			this.buttonGetTaxResult.Click += new System.EventHandler(this.buttonGetTaxResult_Click);
			// 
			// formGetTaxHistory
			// 
			this.AcceptButton = this.buttonGetTaxHistory;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(366, 428);
			this.Controls.Add(this.buttonGetTaxResult);
			this.Controls.Add(this.buttonGetTaxRequest);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.buttonGetTaxHistory);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "formGetTaxHistory";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Get Tax History";
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	
		GetTaxHistoryResult _getTaxHistoryResult;
		GetTaxRequest _getTaxRequest;
		GetTaxResult _getTaxResult;
		private void FillCombos()
		{
			//#####################
			//### UI SETUP CODE ###
			//#####################
			cboDetailLevel.Items.Add(DetailLevel.Summary);
			cboDetailLevel.Items.Add(DetailLevel.Document);
			cboDetailLevel.Items.Add(DetailLevel.Line);
			cboDetailLevel.Items.Add(DetailLevel.Tax);

			Util.FillDocTypeCombo(comboDocType);
		}

		private void buttonGetTaxHistory_Click(object sender, EventArgs e)
		{
			try
			{ //#########################################################################
				//### 1st WE CREATE THE REQUEST OBJECT FOR THE DOCUMENT HISTORY WE WANT ###
				//#########################################################################
				GetTaxHistoryRequest getTaxHistoryRequest = new GetTaxHistoryRequest();
		    
				//###########################################################
				//### 2nd WE LOAD THE REQUEST-LEVEL DATA INTO THE REQUEST ###
				//###########################################################
				getTaxHistoryRequest.CompanyCode = textCompanyCode.Text;
				getTaxHistoryRequest.DocCode = textDocCode.Text;
				getTaxHistoryRequest.DocType = (DocumentType)comboDocType.SelectedItem;
				getTaxHistoryRequest.DetailLevel = (DetailLevel)cboDetailLevel.SelectedItem;
		    
				//##################################################################################################
				//### 3rd WE INVOKE THE GETTAXHISTORY() METHOD OF THE TAXSVC OBJECT AND GET BACK A RESULT OBJECT ###
				//##################################################################################################		    
				Util.PreMethodCall(this, lblStatus);
		    
				TaxSvc taxSvc = new TaxSvc();
				((formMain)this.Owner).SetConfig(taxSvc);              //set the Url and Security configuration
		    
				_getTaxHistoryResult = taxSvc.GetTaxHistory(getTaxHistoryRequest);
		    
				Util.PostMethodCall(this, lblStatus);
		    
				//#####################################
				//### 4th WE READ THE RESULT OBJECT ###
				//#####################################
				lblResultCode.Text = _getTaxHistoryResult.ResultCode.ToString();
				Util.SetMessageLabelText(lblResultMsg, _getTaxHistoryResult);
				_getTaxRequest = _getTaxHistoryResult.GetTaxRequest;
				_getTaxResult = _getTaxHistoryResult.GetTaxResult;
		    
				buttonGetTaxRequest.Enabled = (_getTaxRequest != null);
				buttonGetTaxResult.Enabled = (_getTaxResult != null);
		    
				getTaxHistoryRequest = null;
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

		private void buttonGetTaxRequest_Click(object sender, EventArgs e)
		{
			formGetTaxRequest frm = new formGetTaxRequest(_getTaxRequest);
			frm.Owner = this;
			frm.ShowDialog();
			frm = null;
		}

		private void buttonGetTaxResult_Click(object sender, EventArgs e)
		{
			formGetTaxResult frm = new formGetTaxResult(_getTaxResult);
			frm.Owner = this;
			frm.ShowDialog();
			frm = null;
		}

		private void lblResultMsg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			MessagesForm frm = new MessagesForm(_getTaxHistoryResult.Messages);
			frm.Owner = this;
			frm.ShowDialog();
		}
	}
}