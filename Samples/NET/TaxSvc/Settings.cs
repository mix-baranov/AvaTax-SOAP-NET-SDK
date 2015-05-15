using System;
using System.ComponentModel;
using System.Windows.Forms;
using Avalara.AvaTax.Adapter.TaxService;

namespace Avalara.AvaTax.Adapter.Samples.TaxSample
{
	/// <summary>
	/// Summary description for Security.
	/// </summary>
	public class formSettings : Form
	{
		private Label label1;
		private Label label2;
		private Label label3;
		private Label label4;
		private Label label5;
		private Button buttonCancel;
		private Button buttonApply;
		private TextBox textAccount;
		private TextBox textKey;
		private TextBox textUserName;
		private TextBox textPassword;
		private System.Windows.Forms.RadioButton radioAccount;
		private System.Windows.Forms.RadioButton radioUserName;
		private System.Windows.Forms.TextBox textUrl;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button buttonTestConnection;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;
		private bool isConnectionSuccess = false;
		private string settings = "";
		public formSettings(formMain parent)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			radioAccount.Click += new EventHandler(radio_Click);
			radioUserName.Click += new EventHandler(radio_Click);

//			TaxSvc taxSvc = new TaxSvc();

			_parent = parent;
			textUrl.Text = _parent.Url;
			textAccount.Text = _parent.Account;//taxSvc.Configuration.Security.GetDecryptedText(_parent.Account);
			textKey.Text = _parent.Key;//taxSvc.Configuration.Security.GetDecryptedText(_parent.Key);
			textUserName.Text = _parent.UserName;//taxSvc.Configuration.Security.GetDecryptedText(_parent.UserName);
			textPassword.Text = _parent.Password;//taxSvc.Configuration.Security.GetDecryptedText(_parent.Password);

			if(textUserName.Text != "" || textPassword.Text != "")
			{
				radioUserName.Checked = true;
				radio_Click(null, null);
			}

			if(_parent.Account != null || _parent.Account != "" || _parent.Key != null || _parent.Key != "" || _parent.UserName !=null|| _parent.UserName !="" || _parent.Password!=null|| _parent.Password!="")
				settings = textAccount.Text +","+ textKey.Text +","+textUserName.Text+","+textPassword.Text;
	
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
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textAccount = new System.Windows.Forms.TextBox();
			this.textKey = new System.Windows.Forms.TextBox();
			this.textUserName = new System.Windows.Forms.TextBox();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonApply = new System.Windows.Forms.Button();
			this.radioAccount = new System.Windows.Forms.RadioButton();
			this.radioUserName = new System.Windows.Forms.RadioButton();
			this.textUrl = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.buttonTestConnection = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(382, 38);
			this.label1.TabIndex = 0;
			this.label1.Text = "Use this form to change the default values loaded from the configuration file Ava" +
				"lara.Adapter.AvaTax.dll.config.";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 132);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 20);
			this.label2.TabIndex = 1;
			this.label2.Text = "Account:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 156);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 20);
			this.label3.TabIndex = 2;
			this.label3.Text = "License:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(24, 212);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 20);
			this.label4.TabIndex = 3;
			this.label4.Text = "UserName:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(24, 236);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 20);
			this.label5.TabIndex = 4;
			this.label5.Text = "Password:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textAccount
			// 
			this.textAccount.Location = new System.Drawing.Point(96, 132);
			this.textAccount.Name = "textAccount";
			this.textAccount.Size = new System.Drawing.Size(168, 20);
			this.textAccount.TabIndex = 6;
			this.textAccount.Text = "";
			// 
			// textKey
			// 
			this.textKey.Location = new System.Drawing.Point(96, 156);
			this.textKey.Name = "textKey";
			this.textKey.PasswordChar = '*';
			this.textKey.Size = new System.Drawing.Size(168, 20);
			this.textKey.TabIndex = 7;
			this.textKey.Text = "";
			// 
			// textUserName
			// 
			this.textUserName.Enabled = false;
			this.textUserName.Location = new System.Drawing.Point(96, 212);
			this.textUserName.Name = "textUserName";
			this.textUserName.Size = new System.Drawing.Size(168, 20);
			this.textUserName.TabIndex = 8;
			this.textUserName.Text = "";
			// 
			// textPassword
			// 
			this.textPassword.Enabled = false;
			this.textPassword.Location = new System.Drawing.Point(96, 236);
			this.textPassword.Name = "textPassword";
			this.textPassword.PasswordChar = '*';
			this.textPassword.Size = new System.Drawing.Size(168, 20);
			this.textPassword.TabIndex = 9;
			this.textPassword.Text = "";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(216, 280);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(80, 24);
			this.buttonCancel.TabIndex = 11;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonApply
			// 
			this.buttonApply.Enabled = false;
			this.buttonApply.Location = new System.Drawing.Point(304, 280);
			this.buttonApply.Name = "buttonApply";
			this.buttonApply.Size = new System.Drawing.Size(80, 24);
			this.buttonApply.TabIndex = 12;
			this.buttonApply.Text = "Apply";
			this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
			// 
			// radioAccount
			// 
			this.radioAccount.Checked = true;
			this.radioAccount.Location = new System.Drawing.Point(24, 108);
			this.radioAccount.Name = "radioAccount";
			this.radioAccount.Size = new System.Drawing.Size(144, 20);
			this.radioAccount.TabIndex = 13;
			this.radioAccount.TabStop = true;
			this.radioAccount.Text = "By Account:";
			// 
			// radioUserName
			// 
			this.radioUserName.Location = new System.Drawing.Point(24, 188);
			this.radioUserName.Name = "radioUserName";
			this.radioUserName.Size = new System.Drawing.Size(144, 20);
			this.radioUserName.TabIndex = 13;
			this.radioUserName.Text = "By UserName:";
			// 
			// textUrl
			// 
			this.textUrl.Location = new System.Drawing.Point(72, 60);
			this.textUrl.Name = "textUrl";
			this.textUrl.Size = new System.Drawing.Size(312, 20);
			this.textUrl.TabIndex = 5;
			this.textUrl.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(20, 64);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 20);
			this.label6.TabIndex = 15;
			this.label6.Text = "Url:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// buttonTestConnection
			// 
			this.buttonTestConnection.Location = new System.Drawing.Point(20, 280);
			this.buttonTestConnection.Name = "buttonTestConnection";
			this.buttonTestConnection.Size = new System.Drawing.Size(116, 24);
			this.buttonTestConnection.TabIndex = 10;
			this.buttonTestConnection.Text = "Test Connection";
			this.buttonTestConnection.Click += new System.EventHandler(this.buttonTestConnection_Click);
			// 
			// formSettings
			// 
			this.AcceptButton = this.buttonTestConnection;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(394, 328);
			this.Controls.Add(this.buttonTestConnection);
			this.Controls.Add(this.textUrl);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.radioAccount);
			this.Controls.Add(this.buttonApply);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.textPassword);
			this.Controls.Add(this.textUserName);
			this.Controls.Add(this.textKey);
			this.Controls.Add(this.textAccount);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.radioUserName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "formSettings";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			this.ResumeLayout(false);

		}
		#endregion

		formMain _parent;

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			RejectChanges(settings);
			this.Close();
		}

		private void buttonApply_Click(object sender, EventArgs e)
		{
			ApplySettings();			
			//_parent.groupBoxTaxServiceMethods.Enabled = isConnectionSuccess;
			SaveConfigSettings();
			this.Close();
		}

		private void ApplySettings()
		{
			_parent.Url = textUrl.Text;

			if (radioAccount.Checked)
			{
				_parent.Account = textAccount.Text;
				_parent.Key = textKey.Text;
				_parent.UserName = "";
				_parent.Password = "";
			}
			else
			{
				_parent.Account = "";
				_parent.Key = "";
				_parent.UserName = textUserName.Text;
				_parent.Password = textPassword.Text;
			}			
		}

		private void SaveConfigSettings()
		{			
			TaxSvc taxSvc = new TaxSvc();
		
			taxSvc.Configuration.Url = _parent.Url;
			//taxSvc.Configuration.ViaUrl = _parent.ViaUrl;

			taxSvc.Configuration.Security.Account = _parent.Account;
			taxSvc.Configuration.Security.License = _parent.Key;
			taxSvc.Configuration.Security.UserName = _parent.UserName;	
			taxSvc.Configuration.Security.Password = _parent.Password;
			
			//taxSvc.Configuration.Save ();
		}

		private void radio_Click(object sender, EventArgs e)
		{			
			textAccount.Enabled = radioAccount.Checked;
			textKey.Enabled = radioAccount.Checked;
			textUserName.Enabled = radioUserName.Checked;
			textPassword.Enabled = radioUserName.Checked;
		}

		private void buttonTestConnection_Click(object sender, System.EventArgs e)
		{	
			//string settings = "";
			//if(_parent.Account != null||_parent.Key!= null||_parent.UserName!=null||_parent.Password!=null)
//				settings = textAccount.Text +","+ textKey.Text +","+textUserName.Text+","+textPassword.Text;
			
			try 
			{
				this.Cursor = Cursors.WaitCursor;
				
				ApplySettings();

				TaxSvc taxSvc = new TaxSvc();
				_parent.SetConfig(taxSvc);

				PingResult result = taxSvc.Ping("");
				if(result.ResultCode >= SeverityLevel.Error)
				{
					MessageBox.Show(result.Messages[0].Summary, "Settings Result", MessageBoxButtons.OK, MessageBoxIcon.Error);					
				}			    
				else
				{
					IsAuthorizedResult isAuthorizedResult = taxSvc.IsAuthorized("GetTax, PostTax, CommitTax, CancelTax, AdjustTax, GetTaxHistory, ReconcileTaxHistory");
				
					if(isAuthorizedResult.ResultCode >= SeverityLevel.Error)
					{
						//Util.ShowError(ex);
						MessageBox.Show(isAuthorizedResult.Messages[0].Summary, "Settings Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						isConnectionSuccess = true;
						MessageBox.Show("Result Code: " + isAuthorizedResult.ResultCode + "\r\n" +
							"# Messages: " + isAuthorizedResult.Messages.Count + "\r\n" +
							"Expires: " + isAuthorizedResult.Expires + "\r\n" +
							"Operations: " + isAuthorizedResult.Operations, "Settings Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
			} 
			catch (Exception ex)
			{
				isConnectionSuccess = false;
				Util.ShowError(ex);
			}
			finally 
			{
				RejectChanges(settings);
				if(isConnectionSuccess)
				{
					buttonApply.Enabled = true;
				}
				else
				{
					buttonApply.Enabled = false;
				}
				this.Cursor = Cursors.Default;
			}
		}

		private void RejectChanges(string settings)
		{
			if(settings.Length<=0) 
			{
				_parent.Account = _parent.Key = _parent.UserName =  _parent.Password = "";
			}
			else
			{
				string[] temp = settings.Split(',');
				_parent.Account = temp[0];
				_parent.Key = temp[1];
				_parent.UserName = temp[2];
				_parent.Password = temp[3];
			}
		}
	}
}