using System;
using System.Windows.Forms;
using Avalara.AvaTax.Adapter.TaxService;
namespace Avalara.AvaTax.Adapter.Samples.TaxSample
{
	/// <summary>
	/// Summary description for OperationAuthorization.
	/// </summary>
	public class OperationAuthorization : Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textPassword;
		private System.Windows.Forms.TextBox textUserName;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.RadioButton radioUserName;
		private System.Windows.Forms.TextBox textOperations;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonAuthorized;

		formMain _parent;
		string settings = "";

		public OperationAuthorization(formMain parent)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			radioButton1.Click += new EventHandler(radio_Click);
			radioUserName.Click += new EventHandler(radio_Click);

			_parent = parent;

			if(_parent.UserName!=null||_parent.Password!=null)
				settings = _parent.UserName+","+_parent.Password;
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
			this.textPassword = new System.Windows.Forms.TextBox();
			this.textUserName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.radioUserName = new System.Windows.Forms.RadioButton();
			this.textOperations = new System.Windows.Forms.TextBox();
			this.buttonAuthorized = new System.Windows.Forms.Button();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.buttonClose = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(304, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Use this form to check method(s) given profile can access.";
			// 
			// textPassword
			// 
			this.textPassword.Enabled = false;
			this.textPassword.Location = new System.Drawing.Point(136, 152);
			this.textPassword.Name = "textPassword";
			this.textPassword.PasswordChar = '*';
			this.textPassword.Size = new System.Drawing.Size(168, 20);
			this.textPassword.TabIndex = 17;
			this.textPassword.Text = "";
			// 
			// textUserName
			// 
			this.textUserName.Enabled = false;
			this.textUserName.Location = new System.Drawing.Point(136, 128);
			this.textUserName.Name = "textUserName";
			this.textUserName.Size = new System.Drawing.Size(168, 20);
			this.textUserName.TabIndex = 16;
			this.textUserName.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(24, 152);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(104, 20);
			this.label5.TabIndex = 15;
			this.label5.Text = "License/Password:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 128);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(112, 20);
			this.label4.TabIndex = 14;
			this.label4.Text = "Account/UserName:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// radioUserName
			// 
			this.radioUserName.Location = new System.Drawing.Point(26, 99);
			this.radioUserName.Name = "radioUserName";
			this.radioUserName.Size = new System.Drawing.Size(144, 20);
			this.radioUserName.TabIndex = 15;
			this.radioUserName.Text = "By UserName:";
			// 
			// textOperations
			// 
			this.textOperations.Location = new System.Drawing.Point(16, 232);
			this.textOperations.Multiline = true;
			this.textOperations.Name = "textOperations";
			this.textOperations.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textOperations.Size = new System.Drawing.Size(288, 64);
			this.textOperations.TabIndex = 19;
			this.textOperations.Text = "";
			// 
			// buttonAuthorized
			// 
			this.buttonAuthorized.Location = new System.Drawing.Point(152, 312);
			this.buttonAuthorized.Name = "buttonAuthorized";
			this.buttonAuthorized.TabIndex = 20;
			this.buttonAuthorized.Text = "Authorized";
			this.buttonAuthorized.Click += new System.EventHandler(this.buttonAuthorized_Click);
			// 
			// radioButton1
			// 
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(24, 64);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(144, 20);
			this.radioButton1.TabIndex = 14;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "Current Account/User:";
			this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(232, 312);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.TabIndex = 21;
			this.buttonClose.Text = "Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 192);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(176, 32);
			this.label2.TabIndex = 0;
			this.label2.Text = "Enter the method name(s):       e.g. GetTax, PostTax, CommitTax";
			// 
			// OperationAuthorization
			// 
			this.AcceptButton = this.buttonAuthorized;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(328, 350);
			this.Controls.Add(this.radioButton1);
			this.Controls.Add(this.buttonAuthorized);
			this.Controls.Add(this.textOperations);
			this.Controls.Add(this.textPassword);
			this.Controls.Add(this.textUserName);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.radioUserName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.label2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OperationAuthorization";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Operation Authorization";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonAuthorized_Click(object sender, System.EventArgs e)
		{
			if(textOperations.Text == "") 
			{
				MessageBox.Show("Please insert the method name(s).", "Autherization Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
				textOperations.Focus();
				return;
			}
			else
			{
				try 
				{
					this.Cursor = Cursors.WaitCursor;

					ApplySettings();

//					if(_parent.UserName!=null||_parent.Password!=null)
//						settings = _parent.UserName+","+_parent.Password;
							//textUserName.Text+","+textPassword.Text;
			
					TaxSvc taxSvc = new TaxSvc();
					_parent.SetConfig(taxSvc);

					//				PingResult result = taxSvc.Ping("");
					//				if(result.ResultCode >= SeverityLevel.Error)
					//				{
					//					MessageBox.Show(result.Messages[0].Summary, "Ping Result", MessageBoxButtons.OK, MessageBoxIcon.Error);					
					//				}			    
					//				else
					//				{
					String operations = textOperations.Text.Replace(" ",",");
					IsAuthorizedResult isAuthorizedResult = taxSvc.IsAuthorized(operations.Trim());
				
					if(isAuthorizedResult.ResultCode >= SeverityLevel.Error)
					{
						MessageBox.Show(isAuthorizedResult.Messages[0].Summary, "Authorization Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						if(isAuthorizedResult.Operations.Length==0)
						{
							MessageBox.Show("User is not authorized to listed method(s).", "Authorization Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
						else
						{
							MessageBox.Show("Operation(s) authorized: " + isAuthorizedResult.Operations, "Authorization Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
					}
				} 
				catch (Exception ex)
				{
					Util.ShowError(ex);
				}
				finally 
				{
					RejectChanges(settings);
					this.Cursor = Cursors.Default;
				}
			}
		}

		
		private void radio_Click(object sender, EventArgs e)
		{
			textUserName.Enabled = radioUserName.Checked;
			textPassword.Enabled = radioUserName.Checked;
		}

		private void ApplySettings()
		{
			if (radioUserName.Checked)			
			{
				_parent.UserName = textUserName.Text;
				_parent.Password = textPassword.Text;
			}
		}

		private void buttonClose_Click(object sender, System.EventArgs e)
		{
			RejectChanges(settings);
			this.Close();
		}

		private void RejectChanges(string settings)
		{
			if(settings.Length<=0) 
			{
				_parent.UserName =  _parent.Password = "";
			}
			else
			{
				string[] temp = settings.Split(',');
				_parent.UserName = temp[0];
				_parent.Password = temp[1];
			}
		}

		private void radioButton1_CheckedChanged(object sender, System.EventArgs e)
		{
			RejectChanges(settings);
		}
	}
}
