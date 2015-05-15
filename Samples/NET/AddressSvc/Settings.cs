using System;
using System.ComponentModel;
using System.Windows.Forms;
using Avalara.AvaTax.Adapter.AddressService;

namespace Avalara.AvaTax.Adapter.Samples.AddressSample
{
	/// <summary>
	/// Summary description for Security.
	/// </summary>
	public class formSecurity : Form
	{
		private Label label1;
		private Button buttonCancel;
		private Button buttonApply;
		private System.Windows.Forms.RadioButton radioAccount;
		private System.Windows.Forms.TextBox textPassword;
		private System.Windows.Forms.TextBox textUserName;
		private System.Windows.Forms.TextBox textKey;
		private System.Windows.Forms.TextBox textAccount;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton radioUserName;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textUrl;
		private System.Windows.Forms.Button buttonTestConnection;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;
		string settings = "";

		public formSecurity(formMain parent)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			radioAccount.Click += new EventHandler(radio_Click);
			radioUserName.Click += new EventHandler(radio_Click);

//			AddressSvc addressSvc = new AddressSvc();

			_parent = parent;
			textUrl.Text = _parent.Url;
			textAccount.Text = _parent.Account;//addressSvc.Configuration.Security.GetDecryptedText(_parent.Account);
			textKey.Text = _parent.Key;//addressSvc.Configuration.Security.GetDecryptedText(_parent.Key);
			textUserName.Text = _parent.UserName;//addressSvc.Configuration.Security.GetDecryptedText(_parent.UserName);
			textPassword.Text = _parent.Password;//addressSvc.Configuration.Security.GetDecryptedText(_parent.Password);

			if(textUserName.Text != "" || textPassword.Text != "")
			{
				radioUserName.Checked = true;
				radio_Click(null, null);
			}

			settings = textUrl.Text +","+ textAccount.Text +","+ textKey.Text +","+textUserName.Text+","+textPassword.Text;
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
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonApply = new System.Windows.Forms.Button();
			this.radioAccount = new System.Windows.Forms.RadioButton();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.textUserName = new System.Windows.Forms.TextBox();
			this.textKey = new System.Windows.Forms.TextBox();
			this.textAccount = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.radioUserName = new System.Windows.Forms.RadioButton();
			this.label6 = new System.Windows.Forms.Label();
			this.textUrl = new System.Windows.Forms.TextBox();
			this.buttonTestConnection = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(386, 38);
			this.label1.TabIndex = 0;
			this.label1.Text = "Use this form to change the default values loaded from the configuration file Ava" +
				"lara.Adapter.AvaTax.dll.config.";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(212, 268);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(80, 24);
			this.buttonCancel.TabIndex = 25;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonApply
			// 
			this.buttonApply.Enabled = false;
			this.buttonApply.Location = new System.Drawing.Point(300, 268);
			this.buttonApply.Name = "buttonApply";
			this.buttonApply.Size = new System.Drawing.Size(80, 24);
			this.buttonApply.TabIndex = 26;
			this.buttonApply.Text = "Apply";
			this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
			// 
			// radioAccount
			// 
			this.radioAccount.Checked = true;
			this.radioAccount.Location = new System.Drawing.Point(24, 100);
			this.radioAccount.Name = "radioAccount";
			this.radioAccount.Size = new System.Drawing.Size(144, 20);
			this.radioAccount.TabIndex = 23;
			this.radioAccount.TabStop = true;
			this.radioAccount.Text = "By Account:";
			// 
			// textPassword
			// 
			this.textPassword.Enabled = false;
			this.textPassword.Location = new System.Drawing.Point(96, 228);
			this.textPassword.Name = "textPassword";
			this.textPassword.PasswordChar = '*';
			this.textPassword.Size = new System.Drawing.Size(168, 20);
			this.textPassword.TabIndex = 21;
			this.textPassword.Text = "";
			// 
			// textUserName
			// 
			this.textUserName.Enabled = false;
			this.textUserName.Location = new System.Drawing.Point(96, 204);
			this.textUserName.Name = "textUserName";
			this.textUserName.Size = new System.Drawing.Size(168, 20);
			this.textUserName.TabIndex = 20;
			this.textUserName.Text = "";
			// 
			// textKey
			// 
			this.textKey.Location = new System.Drawing.Point(96, 148);
			this.textKey.Name = "textKey";
			this.textKey.PasswordChar = '*';
			this.textKey.Size = new System.Drawing.Size(168, 20);
			this.textKey.TabIndex = 19;
			this.textKey.Text = "";
			// 
			// textAccount
			// 
			this.textAccount.Location = new System.Drawing.Point(96, 124);
			this.textAccount.Name = "textAccount";
			this.textAccount.Size = new System.Drawing.Size(168, 20);
			this.textAccount.TabIndex = 18;
			this.textAccount.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(24, 228);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 20);
			this.label5.TabIndex = 17;
			this.label5.Text = "Password:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(24, 204);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 20);
			this.label4.TabIndex = 16;
			this.label4.Text = "UserName:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 148);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 20);
			this.label3.TabIndex = 15;
			this.label3.Text = "License:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 124);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 20);
			this.label2.TabIndex = 14;
			this.label2.Text = "Account:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// radioUserName
			// 
			this.radioUserName.Location = new System.Drawing.Point(24, 180);
			this.radioUserName.Name = "radioUserName";
			this.radioUserName.Size = new System.Drawing.Size(144, 20);
			this.radioUserName.TabIndex = 22;
			this.radioUserName.Text = "By UserName:";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(12, 56);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 20);
			this.label6.TabIndex = 14;
			this.label6.Text = "Url:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textUrl
			// 
			this.textUrl.Location = new System.Drawing.Point(64, 56);
			this.textUrl.Name = "textUrl";
			this.textUrl.Size = new System.Drawing.Size(316, 20);
			this.textUrl.TabIndex = 1;
			this.textUrl.Text = "";
			// 
			// buttonTestConnection
			// 
			this.buttonTestConnection.Location = new System.Drawing.Point(20, 268);
			this.buttonTestConnection.Name = "buttonTestConnection";
			this.buttonTestConnection.Size = new System.Drawing.Size(116, 24);
			this.buttonTestConnection.TabIndex = 24;
			this.buttonTestConnection.Text = "Test Connection";
			this.buttonTestConnection.Click += new System.EventHandler(this.buttonTestConnection_Click);
			// 
			// formSecurity
			// 
			this.AcceptButton = this.buttonTestConnection;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(390, 312);
			this.Controls.Add(this.buttonTestConnection);
			this.Controls.Add(this.radioAccount);
			this.Controls.Add(this.textPassword);
			this.Controls.Add(this.textUserName);
			this.Controls.Add(this.textKey);
			this.Controls.Add(this.textAccount);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.radioUserName);
			this.Controls.Add(this.buttonApply);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textUrl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "formSecurity";
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
			SaveConfigSettings();
			this.Close();			
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
			bool isConnectionSuccess = false;
			//if(_parent.Account != null || _parent.Account != "" || _parent.Key!= null|| _parent.Key!=""||_parent.UserName!=null||_parent.UserName!=""||_parent.Password!=null||_parent.Password!="")
				//settings = textAccount.Text +","+ textKey.Text +","+textUserName.Text+","+textPassword.Text;
			
			try 
			{
				this.Cursor = Cursors.WaitCursor;
				
				ApplySettings();

				AddressSvc addressSvc = new AddressSvc();
				_parent.SetConfig(addressSvc);

				PingResult result = addressSvc.Ping("");
				if(result.ResultCode >= SeverityLevel.Error)
				{
					MessageBox.Show(result.Messages[0].Summary, "Settings Result", MessageBoxButtons.OK, MessageBoxIcon.Error);					
				}			    
				else
				{
					IsAuthorizedResult isAuthorizedResult = addressSvc.IsAuthorized("Validate");
				
					if(isAuthorizedResult.ResultCode >= SeverityLevel.Error)
					{
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
			AddressSvc addressSvc = new AddressSvc();
		
			addressSvc.Configuration.Url = _parent.Url;

			addressSvc.Configuration.Security.Account = _parent.Account;
			addressSvc.Configuration.Security.License = _parent.Key;
			addressSvc.Configuration.Security.UserName = _parent.UserName;	
			addressSvc.Configuration.Security.Password = _parent.Password;

			//addressSvc.Configuration.Save ();
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
				_parent.Url = temp[0];
				_parent.Account = temp[1];
				_parent.Key = temp[2];
				_parent.UserName = temp[3];
				_parent.Password = temp[4];
			}
		}
	}
}