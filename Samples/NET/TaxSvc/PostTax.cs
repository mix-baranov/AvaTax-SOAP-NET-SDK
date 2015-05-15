using System;
using System.ComponentModel;
using System.Windows.Forms;
using Avalara.AvaTax.Adapter.TaxService;

namespace Avalara.AvaTax.Adapter.Samples.TaxSample
{
	/// <summary>
	/// Summary description for PostTax.
	/// </summary>
	public class formPostTax : Form
	{
		private Button buttonClose;
		private Label lblStatus;
		private GroupBox groupBox2;
		private Label lblResultCode;
		private Label label6;
		private Label label5;
		private GroupBox groupBox1;
		private Button buttonPostTax;
		private TextBox textTotalAmount;
		private TextBox textDocDate;
		private Label label1;
		private Label label2;
		private TextBox textTotalTax;
		private Label label7;
		private System.Windows.Forms.LinkLabel lblResultMsg;
		private System.Windows.Forms.CheckBox chkCommit;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ComboBox comboDocType;
		private System.Windows.Forms.TextBox textNewDocCode;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textDocCode;
		private System.Windows.Forms.TextBox textCompanyCode;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label13;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public formPostTax()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//#####################
			//### UI SETUP CODE ###
			//#####################
			Util.FillDocTypeCombo(comboDocType);

			textCompanyCode.Text = "DEFAULT";
			textDocCode.Text = "DOC0001";
			textDocDate.Text = DateTime.Today.ToShortDateString();
			textTotalAmount.Text = "";
			textTotalTax.Text = "";
			comboDocType.SelectedIndex = 0;
			//chkCommit.Checked = true;
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
			this.buttonPostTax = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lblResultMsg = new System.Windows.Forms.LinkLabel();
			this.lblResultCode = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboDocType = new System.Windows.Forms.ComboBox();
			this.textNewDocCode = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.textDocCode = new System.Windows.Forms.TextBox();
			this.textCompanyCode = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.chkCommit = new System.Windows.Forms.CheckBox();
			this.textTotalTax = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textTotalAmount = new System.Windows.Forms.TextBox();
			this.textDocDate = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(280, 468);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(76, 24);
			this.buttonClose.TabIndex = 100;
			this.buttonClose.Text = "Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.ForeColor = System.Drawing.Color.Green;
			this.lblStatus.Location = new System.Drawing.Point(12, 472);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(260, 20);
			this.lblStatus.TabIndex = 13;
			// 
			// buttonPostTax
			// 
			this.buttonPostTax.Location = new System.Drawing.Point(140, 300);
			this.buttonPostTax.Name = "buttonPostTax";
			this.buttonPostTax.Size = new System.Drawing.Size(88, 24);
			this.buttonPostTax.TabIndex = 17;
			this.buttonPostTax.Text = "Post Tax";
			this.buttonPostTax.Click += new System.EventHandler(this.buttonPostTax_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.lblResultMsg);
			this.groupBox2.Controls.Add(this.lblResultCode);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(8, 344);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(352, 100);
			this.groupBox2.TabIndex = 11;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Result";
			// 
			// lblResultMsg
			// 
			this.lblResultMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblResultMsg.Location = new System.Drawing.Point(84, 60);
			this.lblResultMsg.Name = "lblResultMsg";
			this.lblResultMsg.Size = new System.Drawing.Size(260, 20);
			this.lblResultMsg.TabIndex = 4;
			this.lblResultMsg.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblResultMsg_LinkClicked);
			// 
			// lblResultCode
			// 
			this.lblResultCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblResultCode.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblResultCode.Location = new System.Drawing.Point(84, 36);
			this.lblResultCode.Name = "lblResultCode";
			this.lblResultCode.Size = new System.Drawing.Size(260, 20);
			this.lblResultCode.TabIndex = 2;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(8, 60);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(72, 20);
			this.label6.TabIndex = 1;
			this.label6.Text = "Result Msg:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(8, 36);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 20);
			this.label5.TabIndex = 0;
			this.label5.Text = "Result Code:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.comboDocType);
			this.groupBox1.Controls.Add(this.textNewDocCode);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.textDocCode);
			this.groupBox1.Controls.Add(this.textCompanyCode);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.chkCommit);
			this.groupBox1.Controls.Add(this.textTotalTax);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.textTotalAmount);
			this.groupBox1.Controls.Add(this.textDocDate);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(35, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(300, 276);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Request";
			// 
			// comboDocType
			// 
			this.comboDocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboDocType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.comboDocType.Location = new System.Drawing.Point(136, 56);
			this.comboDocType.Name = "comboDocType";
			this.comboDocType.Size = new System.Drawing.Size(132, 21);
			this.comboDocType.TabIndex = 8;
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label8.Location = new System.Drawing.Point(40, 56);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(96, 20);
			this.label8.TabIndex = 23;
			this.label8.Text = "Document Type:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDocCode
			// 
			this.textDocCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textDocCode.Location = new System.Drawing.Point(136, 80);
			this.textDocCode.Name = "textDocCode";
			this.textDocCode.Size = new System.Drawing.Size(132, 20);
			this.textDocCode.TabIndex = 9;
			this.textDocCode.Text = "";
			// 
			// textCompanyCode
			// 
			this.textCompanyCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textCompanyCode.Location = new System.Drawing.Point(136, 32);
			this.textCompanyCode.Name = "textCompanyCode";
			this.textCompanyCode.Size = new System.Drawing.Size(132, 20);
			this.textCompanyCode.TabIndex = 7;
			this.textCompanyCode.Text = "";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(40, 80);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 20);
			this.label4.TabIndex = 25;
			this.label4.Text = "Document Code:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(40, 32);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 20);
			this.label3.TabIndex = 21;
			this.label3.Text = "Company Code:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// chkCommit
			// 
			this.chkCommit.Location = new System.Drawing.Point(136, 224);
			this.chkCommit.Name = "chkCommit";
			this.chkCommit.Size = new System.Drawing.Size(132, 24);
			this.chkCommit.TabIndex = 16;
			// 
			// textTotalTax
			// 
			this.textTotalTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textTotalTax.Location = new System.Drawing.Point(136, 180);
			this.textTotalTax.Name = "textTotalTax";
			this.textTotalTax.Size = new System.Drawing.Size(132, 20);
			this.textTotalTax.TabIndex = 14;
			this.textTotalTax.Text = "";
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(36, 180);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(96, 20);
			this.label7.TabIndex = 13;
			this.label7.Text = "Total Tax:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textTotalAmount
			// 
			this.textTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textTotalAmount.Location = new System.Drawing.Point(136, 156);
			this.textTotalAmount.Name = "textTotalAmount";
			this.textTotalAmount.Size = new System.Drawing.Size(132, 20);
			this.textTotalAmount.TabIndex = 12;
			this.textTotalAmount.Text = "";
			// 
			// textDocDate
			// 
			this.textDocDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textDocDate.Location = new System.Drawing.Point(136, 132);
			this.textDocDate.Name = "textDocDate";
			this.textDocDate.Size = new System.Drawing.Size(132, 20);
			this.textDocDate.TabIndex = 10;
			this.textDocDate.Text = "";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(36, 156);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 20);
			this.label1.TabIndex = 11;
			this.label1.Text = "Total Amount:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(36, 132);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 20);
			this.label2.TabIndex = 9;
			this.label2.Text = "Document Date:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label11
			// 
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label11.Location = new System.Drawing.Point(36, 228);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(96, 20);
			this.label11.TabIndex = 13;
			this.label11.Text = "Commit:";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label13.Location = new System.Drawing.Point(40, 104);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(228, 20);
			this.label13.TabIndex = 19;
			this.label13.Text = "________________________________________";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textNewDocCode
			// 
			this.textNewDocCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textNewDocCode.Location = new System.Drawing.Point(136, 204);
			this.textNewDocCode.Name = "textNewDocCode";
			this.textNewDocCode.Size = new System.Drawing.Size(132, 20);
			this.textNewDocCode.TabIndex = 15;
			this.textNewDocCode.Text = "";
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label9.Location = new System.Drawing.Point(12, 204);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(120, 20);
			this.label9.TabIndex = 27;
			this.label9.Text = "New Document Code:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// formPostTax
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(370, 504);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.buttonPostTax);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "formPostTax";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Post Tax";
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		PostTaxResult _postTaxResult;

		private void buttonClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void buttonPostTax_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (!Util.IsValidDateTime(textDocDate.Text))
				{
					MessageBox.Show("Invalid date/time value.", "Invalid Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
					textDocDate.Focus();
					return;
				}
				if (!Util.CheckRequiredField(textTotalAmount, "Total Price"))
				{
					return;
				}
				if (!Util.CheckRequiredField(textTotalTax, "Total Tax"))
				{
					return;
				}

				//###########################################################################
				//### 1st WE CREATE THE REQUEST OBJECT FOR DOCUMENT THAT SHOULD BE POSTED ###
				//###########################################################################
				PostTaxRequest postTaxRequest = new PostTaxRequest();
		    
				//###########################################################
				//### 2nd WE LOAD THE REQUEST-LEVEL DATA INTO THE REQUEST ###
				//###########################################################
				postTaxRequest.CompanyCode = textCompanyCode.Text;
				postTaxRequest.DocType = (DocumentType)comboDocType.SelectedItem;
				postTaxRequest.DocCode = textDocCode.Text;

				postTaxRequest.DocDate = Convert.ToDateTime(textDocDate.Text);
				postTaxRequest.TotalAmount = Convert.ToDecimal(textTotalAmount.Text);
				postTaxRequest.TotalTax = Convert.ToDecimal(textTotalTax.Text);
				postTaxRequest.NewDocCode = textNewDocCode.Text;
				postTaxRequest.Commit = chkCommit.Checked;
		    
				//############################################################################################
				//### 3rd WE INVOKE THE POSTTAX() METHOD OF THE TAXSVC OBJECT AND GET BACK A RESULT OBJECT ###
				//############################################################################################
				Util.PreMethodCall(this, lblStatus);
		    
				TaxSvc taxSvc = new TaxSvc();
				((formMain)this.Owner).SetConfig(taxSvc);              //set the Url and Security configuration
		    
				_postTaxResult = taxSvc.PostTax(postTaxRequest);
		    
				Util.PostMethodCall(this, lblStatus);
		    
				//#####################################
				//### 4th WE READ THE RESULT OBJECT ###
				//#####################################
				lblResultCode.Text = _postTaxResult.ResultCode.ToString();
				Util.SetMessageLabelText(lblResultMsg, _postTaxResult);
		    
				postTaxRequest = null;
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
			MessagesForm frm = new MessagesForm(_postTaxResult.Messages);
			frm.Owner = this;
			frm.ShowDialog();
		}
	}
}
