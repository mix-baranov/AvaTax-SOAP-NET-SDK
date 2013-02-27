using System;
using System.ComponentModel;
using System.Windows.Forms;
using Avalara.AvaTax.Adapter.TaxService;

namespace Avalara.AvaTax.Adapter.Samples.TaxSample
{
	/// <summary>
	/// Summary description for CommitTax.
	/// </summary>
	public class formCommitTax : Form
	{
		private Button buttonClose;
		private Label lblStatus;
		private GroupBox groupBox2;
		private Label lblResultCode;
		private Label label6;
		private Label label5;
		private GroupBox groupBox1;
		private TextBox textDocCode;
		private TextBox textCompanyCode;
		private Label label4;
		private Label label3;
		private Button buttonCommitTax;
		private System.Windows.Forms.LinkLabel lblResultMsg;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textNewDocCode;
		private System.Windows.Forms.ComboBox comboDocType;
		private System.Windows.Forms.Label label9;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public formCommitTax()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//#####################
			//### UI SETUP CODE ###
			//#####################
			FillCombo();

			textCompanyCode.Text = "DEFAULT";
			textDocCode.Text = "DOC0001";
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
			this.buttonCommitTax = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lblResultMsg = new System.Windows.Forms.LinkLabel();
			this.lblResultCode = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboDocType = new System.Windows.Forms.ComboBox();
			this.textNewDocCode = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textDocCode = new System.Windows.Forms.TextBox();
			this.textCompanyCode = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(280, 328);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(76, 24);
			this.buttonClose.TabIndex = 100;
			this.buttonClose.Text = "Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.ForeColor = System.Drawing.Color.Green;
			this.lblStatus.Location = new System.Drawing.Point(12, 332);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(260, 20);
			this.lblStatus.TabIndex = 8;
			// 
			// buttonCommitTax
			// 
			this.buttonCommitTax.Location = new System.Drawing.Point(144, 172);
			this.buttonCommitTax.Name = "buttonCommitTax";
			this.buttonCommitTax.Size = new System.Drawing.Size(88, 24);
			this.buttonCommitTax.TabIndex = 10;
			this.buttonCommitTax.Text = "Commit Tax";
			this.buttonCommitTax.Click += new System.EventHandler(this.buttonCommitTax_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.lblResultMsg);
			this.groupBox2.Controls.Add(this.lblResultCode);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(8, 212);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(352, 96);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Result";
			// 
			// lblResultMsg
			// 
			this.lblResultMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblResultMsg.Location = new System.Drawing.Point(84, 56);
			this.lblResultMsg.Name = "lblResultMsg";
			this.lblResultMsg.Size = new System.Drawing.Size(260, 20);
			this.lblResultMsg.TabIndex = 3;
			this.lblResultMsg.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblResultMsg_LinkClicked);
			// 
			// lblResultCode
			// 
			this.lblResultCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblResultCode.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblResultCode.Location = new System.Drawing.Point(84, 32);
			this.lblResultCode.Name = "lblResultCode";
			this.lblResultCode.Size = new System.Drawing.Size(260, 20);
			this.lblResultCode.TabIndex = 2;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(8, 56);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(72, 20);
			this.label6.TabIndex = 1;
			this.label6.Text = "Result Msg:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(8, 32);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 20);
			this.label5.TabIndex = 0;
			this.label5.Text = "Result Code:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.comboDocType);
			this.groupBox1.Controls.Add(this.textNewDocCode);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.textDocCode);
			this.groupBox1.Controls.Add(this.textCompanyCode);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(35, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(300, 148);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Request";
			// 
			// comboDocType
			// 
			this.comboDocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDocType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.comboDocType.Location = new System.Drawing.Point(132, 80);
			this.comboDocType.Name = "comboDocType";
			this.comboDocType.Size = new System.Drawing.Size(136, 21);
			this.comboDocType.TabIndex = 8;
			// 
			// textNewDocCode
			// 
			this.textNewDocCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textNewDocCode.Location = new System.Drawing.Point(132, 104);
			this.textNewDocCode.Name = "textNewDocCode";
			this.textNewDocCode.Size = new System.Drawing.Size(132, 20);
			this.textNewDocCode.TabIndex = 9;
			this.textNewDocCode.Text = "";
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label9.Location = new System.Drawing.Point(8, 104);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(120, 20);
			this.label9.TabIndex = 29;
			this.label9.Text = "New Document Code:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			//
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(32, 80);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 20);
			this.label1.TabIndex = 7;
			this.label1.Text = "Document Type:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDocCode
			// 
			this.textDocCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textDocCode.Location = new System.Drawing.Point(132, 56);
			this.textDocCode.Name = "textDocCode";
			this.textDocCode.Size = new System.Drawing.Size(132, 20);
			this.textDocCode.TabIndex = 6;
			this.textDocCode.Text = "";
			// 
			// textCompanyCode
			// 
			this.textCompanyCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textCompanyCode.Location = new System.Drawing.Point(132, 32);
			this.textCompanyCode.Name = "textCompanyCode";
			this.textCompanyCode.Size = new System.Drawing.Size(132, 20);
			this.textCompanyCode.TabIndex = 4;
			this.textCompanyCode.Text = "";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(32, 56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 20);
			this.label4.TabIndex = 5;
			this.label4.Text = "Document Code:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(32, 32);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 20);
			this.label3.TabIndex = 3;
			this.label3.Text = "Company Code:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// formCommitTax
			// 
			this.AcceptButton = this.buttonCommitTax;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(370, 364);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.buttonCommitTax);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "formCommitTax";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Commit Tax";
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		CommitTaxResult _commitTaxResult;

		private void FillCombo()
		{
			//#####################
			//### UI SETUP CODE ###
			//#####################
			Util.FillDocTypeCombo(comboDocType);
			comboDocType.SelectedIndex = 0;
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void buttonCommitTax_Click(object sender, EventArgs e)
		{
			try
			{
				//##############################################################################
				//### 1st WE CREATE THE REQUEST OBJECT FOR DOCUMENT THAT SHOULD BE COMMITTED ###
				//##############################################################################
				CommitTaxRequest commitTaxRequest = new CommitTaxRequest();
		    
				//###########################################################
				//### 2nd WE LOAD THE REQUEST-LEVEL DATA INTO THE REQUEST ###
				//###########################################################
				commitTaxRequest.CompanyCode = textCompanyCode.Text;
				commitTaxRequest.DocType = (DocumentType)comboDocType.SelectedItem;
				commitTaxRequest.DocCode = textDocCode.Text;
				commitTaxRequest.NewDocCode = textNewDocCode.Text;
		    
				//##############################################################################################
				//### 3rd WE INVOKE THE COMMITTAX() METHOD OF THE TAXSVC OBJECT AND GET BACK A RESULT OBJECT ###
				//##############################################################################################
				Util.PreMethodCall(this, lblStatus);
		    
				TaxSvc taxSvc = new TaxSvc();
				((formMain)this.Owner).SetConfig(taxSvc);              //set the Url and Security configuration
		    
				_commitTaxResult = taxSvc.CommitTax(commitTaxRequest);
		    
				Util.PostMethodCall(this, lblStatus);
		    
				//#####################################
				//### 4th WE READ THE RESULT OBJECT ###
				//#####################################
				lblResultCode.Text = _commitTaxResult.ResultCode.ToString();
				Util.SetMessageLabelText(lblResultMsg, _commitTaxResult);
		    
				commitTaxRequest = null;
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

		private void lblResultMsg_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			MessagesForm frm = new MessagesForm(_commitTaxResult.Messages);
			frm.Owner = this;
			frm.ShowDialog();
		}
	}
}