using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using Avalara.AvaTax.Adapter;
using Avalara.AvaTax.Adapter.TaxService;

namespace Avalara.AvaTax.Adapter.Samples.TaxSample
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class formMain : Form
	{
		private GroupBox groupBox1;
		private GroupBox groupBox2;
		private Button buttonClose;
		private Button buttonPing;
		private Button buttonIsAuthorized;
		private Button buttonAbout;
		private Label label1;
		private Label label2;
		private Label label3;
		private Label label4;
		private Label label5;
		private Label lblUrl;
		private Label lblAccount;
		private Label lblKey;
		private Label lblUserName;
		private Label lblPassword;
		private Label lblStatus;
		private Button buttonGetTax;
		private Button buttonCancelTax;
		private Button buttonPostTax;
		private Button buttonCommitTax;
		private Button buttonGetTaxHistory;
		private Button buttonReconcileTaxHistory;
		private System.Windows.Forms.Button buttonAdjustTax;
		public System.Windows.Forms.GroupBox groupBoxTaxServiceMethods;
		private System.Windows.Forms.Button buttonSettings;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public formMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			LoadConfig();
			SetUrlLabels();
			SetSecurityLabels();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblPassword = new System.Windows.Forms.Label();
			this.lblUserName = new System.Windows.Forms.Label();
			this.lblKey = new System.Windows.Forms.Label();
			this.lblAccount = new System.Windows.Forms.Label();
			this.lblUrl = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonSettings = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.buttonAbout = new System.Windows.Forms.Button();
			this.buttonIsAuthorized = new System.Windows.Forms.Button();
			this.buttonPing = new System.Windows.Forms.Button();
			this.groupBoxTaxServiceMethods = new System.Windows.Forms.GroupBox();
			this.buttonReconcileTaxHistory = new System.Windows.Forms.Button();
			this.buttonGetTaxHistory = new System.Windows.Forms.Button();
			this.buttonCommitTax = new System.Windows.Forms.Button();
			this.buttonPostTax = new System.Windows.Forms.Button();
			this.buttonCancelTax = new System.Windows.Forms.Button();
			this.buttonGetTax = new System.Windows.Forms.Button();
			this.buttonAdjustTax = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBoxTaxServiceMethods.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblPassword);
			this.groupBox1.Controls.Add(this.lblUserName);
			this.groupBox1.Controls.Add(this.lblKey);
			this.groupBox1.Controls.Add(this.lblAccount);
			this.groupBox1.Controls.Add(this.lblUrl);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.buttonSettings);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(8, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(477, 196);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Configuration";
			// 
			// lblPassword
			// 
			this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblPassword.Location = new System.Drawing.Point(88, 136);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(277, 20);
			this.lblPassword.TabIndex = 12;
			// 
			// lblUserName
			// 
			this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblUserName.Location = new System.Drawing.Point(88, 112);
			this.lblUserName.Name = "lblUserName";
			this.lblUserName.Size = new System.Drawing.Size(277, 20);
			this.lblUserName.TabIndex = 11;
			// 
			// lblKey
			// 
			this.lblKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblKey.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblKey.Location = new System.Drawing.Point(88, 88);
			this.lblKey.Name = "lblKey";
			this.lblKey.Size = new System.Drawing.Size(277, 20);
			this.lblKey.TabIndex = 10;
			// 
			// lblAccount
			// 
			this.lblAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblAccount.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblAccount.Location = new System.Drawing.Point(88, 64);
			this.lblAccount.Name = "lblAccount";
			this.lblAccount.Size = new System.Drawing.Size(277, 20);
			this.lblAccount.TabIndex = 9;
			// 
			// lblUrl
			// 
			this.lblUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblUrl.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblUrl.Location = new System.Drawing.Point(52, 24);
			this.lblUrl.Name = "lblUrl";
			this.lblUrl.Size = new System.Drawing.Size(324, 28);
			this.lblUrl.TabIndex = 8;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(8, 136);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(76, 20);
			this.label5.TabIndex = 6;
			this.label5.Text = "Password:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(8, 112);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(76, 20);
			this.label4.TabIndex = 5;
			this.label4.Text = "UserName:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(8, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(76, 20);
			this.label3.TabIndex = 4;
			this.label3.Text = "License:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 20);
			this.label2.TabIndex = 3;
			this.label2.Text = "Account:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(11, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "Url:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// buttonSettings
			// 
			this.buttonSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.buttonSettings.Location = new System.Drawing.Point(388, 160);
			this.buttonSettings.Name = "buttonSettings";
			this.buttonSettings.Size = new System.Drawing.Size(80, 24);
			this.buttonSettings.TabIndex = 1;
			this.buttonSettings.Text = "Settings";
			this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.buttonAbout);
			this.groupBox2.Controls.Add(this.buttonIsAuthorized);
			this.groupBox2.Controls.Add(this.buttonPing);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(8, 216);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(477, 67);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Generic Methods";
			// 
			// buttonAbout
			// 
			this.buttonAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.buttonAbout.Location = new System.Drawing.Point(312, 27);
			this.buttonAbout.Name = "buttonAbout";
			this.buttonAbout.Size = new System.Drawing.Size(96, 25);
			this.buttonAbout.TabIndex = 4;
			this.buttonAbout.Text = "About Adapter";
			this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
			// 
			// buttonIsAuthorized
			// 
			this.buttonIsAuthorized.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.buttonIsAuthorized.Location = new System.Drawing.Point(188, 27);
			this.buttonIsAuthorized.Name = "buttonIsAuthorized";
			this.buttonIsAuthorized.Size = new System.Drawing.Size(96, 25);
			this.buttonIsAuthorized.TabIndex = 3;
			this.buttonIsAuthorized.Text = "Is Authorized?";
			this.buttonIsAuthorized.Click += new System.EventHandler(this.buttonIsAuthorized_Click);
			// 
			// buttonPing
			// 
			this.buttonPing.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.buttonPing.Location = new System.Drawing.Point(68, 27);
			this.buttonPing.Name = "buttonPing";
			this.buttonPing.Size = new System.Drawing.Size(96, 25);
			this.buttonPing.TabIndex = 2;
			this.buttonPing.Text = "Ping Service";
			this.buttonPing.Click += new System.EventHandler(this.buttonPing_Click);
			// 
			// groupBoxTaxServiceMethods
			// 
			this.groupBoxTaxServiceMethods.Controls.Add(this.buttonReconcileTaxHistory);
			this.groupBoxTaxServiceMethods.Controls.Add(this.buttonGetTaxHistory);
			this.groupBoxTaxServiceMethods.Controls.Add(this.buttonCommitTax);
			this.groupBoxTaxServiceMethods.Controls.Add(this.buttonPostTax);
			this.groupBoxTaxServiceMethods.Controls.Add(this.buttonCancelTax);
			this.groupBoxTaxServiceMethods.Controls.Add(this.buttonGetTax);
			this.groupBoxTaxServiceMethods.Controls.Add(this.buttonAdjustTax);
			this.groupBoxTaxServiceMethods.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBoxTaxServiceMethods.Location = new System.Drawing.Point(8, 292);
			this.groupBoxTaxServiceMethods.Name = "groupBoxTaxServiceMethods";
			this.groupBoxTaxServiceMethods.Size = new System.Drawing.Size(477, 168);
			this.groupBoxTaxServiceMethods.TabIndex = 2;
			this.groupBoxTaxServiceMethods.TabStop = false;
			this.groupBoxTaxServiceMethods.Text = "Tax Service Methods";
			// 
			// buttonReconcileTaxHistory
			// 
			this.buttonReconcileTaxHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.buttonReconcileTaxHistory.Location = new System.Drawing.Point(244, 92);
			this.buttonReconcileTaxHistory.Name = "buttonReconcileTaxHistory";
			this.buttonReconcileTaxHistory.Size = new System.Drawing.Size(156, 24);
			this.buttonReconcileTaxHistory.TabIndex = 11;
			this.buttonReconcileTaxHistory.Text = "Reconcile Tax History";
			this.buttonReconcileTaxHistory.Click += new System.EventHandler(this.buttonReconcileTaxHistory_Click);
			// 
			// buttonGetTaxHistory
			// 
			this.buttonGetTaxHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.buttonGetTaxHistory.Location = new System.Drawing.Point(244, 60);
			this.buttonGetTaxHistory.Name = "buttonGetTaxHistory";
			this.buttonGetTaxHistory.Size = new System.Drawing.Size(156, 24);
			this.buttonGetTaxHistory.TabIndex = 10;
			this.buttonGetTaxHistory.Text = "Get Tax History";
			this.buttonGetTaxHistory.Click += new System.EventHandler(this.buttonGetTaxHistory_Click);
			// 
			// buttonCommitTax
			// 
			this.buttonCommitTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.buttonCommitTax.Location = new System.Drawing.Point(76, 92);
			this.buttonCommitTax.Name = "buttonCommitTax";
			this.buttonCommitTax.Size = new System.Drawing.Size(156, 24);
			this.buttonCommitTax.TabIndex = 7;
			this.buttonCommitTax.Text = "Commit Tax";
			this.buttonCommitTax.Click += new System.EventHandler(this.buttonCommitTax_Click);
			// 
			// buttonPostTax
			// 
			this.buttonPostTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.buttonPostTax.Location = new System.Drawing.Point(76, 60);
			this.buttonPostTax.Name = "buttonPostTax";
			this.buttonPostTax.Size = new System.Drawing.Size(156, 24);
			this.buttonPostTax.TabIndex = 6;
			this.buttonPostTax.Text = "Post Tax";
			this.buttonPostTax.Click += new System.EventHandler(this.buttonPostTax_Click);
			// 
			// buttonCancelTax
			// 
			this.buttonCancelTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.buttonCancelTax.Location = new System.Drawing.Point(76, 124);
			this.buttonCancelTax.Name = "buttonCancelTax";
			this.buttonCancelTax.Size = new System.Drawing.Size(156, 24);
			this.buttonCancelTax.TabIndex = 8;
			this.buttonCancelTax.Text = "Cancel Tax";
			this.buttonCancelTax.Click += new System.EventHandler(this.buttonCancelTax_Click);
			// 
			// buttonGetTax
			// 
			this.buttonGetTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.buttonGetTax.Location = new System.Drawing.Point(76, 28);
			this.buttonGetTax.Name = "buttonGetTax";
			this.buttonGetTax.Size = new System.Drawing.Size(156, 24);
			this.buttonGetTax.TabIndex = 5;
			this.buttonGetTax.Text = "Get Tax";
			this.buttonGetTax.Click += new System.EventHandler(this.buttonGetTax_Click);
			// 
			// buttonAdjustTax
			// 
			this.buttonAdjustTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.buttonAdjustTax.Location = new System.Drawing.Point(244, 28);
			this.buttonAdjustTax.Name = "buttonAdjustTax";
			this.buttonAdjustTax.Size = new System.Drawing.Size(156, 24);
			this.buttonAdjustTax.TabIndex = 9;
			this.buttonAdjustTax.Text = "Adjust Tax";
			this.buttonAdjustTax.Click += new System.EventHandler(this.buttonAdjustTax_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(404, 480);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(84, 24);
			this.buttonClose.TabIndex = 13;
			this.buttonClose.Text = "Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.ForeColor = System.Drawing.Color.Green;
			this.lblStatus.Location = new System.Drawing.Point(16, 484);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(376, 20);
			this.lblStatus.TabIndex = 4;
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// formMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(498, 515);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.groupBoxTaxServiceMethods);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "formMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SDK Tax Sample";
			this.Load += new System.EventHandler(this.formMain_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBoxTaxServiceMethods.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private string _url;
		private string _viaUrl;
		private string _account;
		private string _key;
		private string _userName;
		private string _password;
		private bool loadSettingsFromConfig = true;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new formMain());
		}

		public string Url
		{
			get { return _url; }
			set { _url = value; }
		}

		public string ViaUrl
		{
			get { return _viaUrl; }
			set { _viaUrl = value; }
		}

		public string Account
		{
			get { return _account; }
			set { _account = value; }
		}

		public string Key
		{
			get { return _key; }
			set { _key = value; }
		}

		public string UserName
		{
			get { return _userName; }
			set { _userName = value; }
		}

		public string Password
		{
			get { return _password; }
			set { _password = value; }
		}

		private void SetUrlLabels() {
			lblUrl.Text = Url;
		}

		private void SetSecurityLabels() {

			if(loadSettingsFromConfig)
			{
				XmlDocument doc = new XmlDocument();
				try
				{
					doc.Load(Util.ConfigFileName());
				
					XmlNode node = doc.SelectSingleNode("//RequestSecurity/Account");
					if(node != null)
					{
						Account = node.InnerText;
					}
					node = doc.SelectSingleNode("//RequestSecurity/License");
					if(node != null)
					{
						Key = node.InnerText;
					}
				}
				catch(Exception exp)
				{
					
				}
				loadSettingsFromConfig = false;
			}

			lblAccount.Text = Account;
			lblKey.Text = (Key == "" || Key == null) ? "" :  "{ encrypted }";			
			lblUserName.Text = UserName;//new TaxSvc().Configuration.Security.GetDecryptedText(UserName);
			lblPassword.Text = (Password == "" || Password == null) ? "" : "{ encrypted }";
		}


		private void LoadConfig() {

			TaxSvc taxSvc = new TaxSvc();
		    
			Url = taxSvc.Configuration.Url;
			//ViaUrl = taxSvc.Configuration.ViaUrl;
			UserName = taxSvc.Configuration.Security.UserName;	 
			Password = taxSvc.Configuration.Security.Password;
			Account = taxSvc.Configuration.Security.Account;	 
			Key = taxSvc.Configuration.Security.License;
		}

		public void SetConfig(TaxSvc svc) {

			svc.Profile.Client = "AdapterSampleCode,1.0";

			if (Url != null)
			{
				svc.Configuration.Url = Url;
			}
			if (Account != null && Account.Length > 0)
			{
				svc.Configuration.Security.Account = Account;
			}
			if (Key != null && Key.Length > 0)
			{
				svc.Configuration.Security.License = Key;
			}
			svc.Configuration.Security.UserName = UserName;
			//write as plain text
			svc.Configuration.Security.Password = Password;		
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void buttonSettings_Click(object sender, EventArgs e)
		{
			formSettings frm = new formSettings(this);
			frm.Owner = this;
			frm.ShowDialog();

			//LoadConfig();
			SetUrlLabels();
			SetSecurityLabels();
		}

		private void buttonPing_Click(object sender, EventArgs e)
		{		    
			try 
			{
				Util.PreMethodCall(this, lblStatus);

				TaxSvc taxSvc = new TaxSvc();
				SetConfig(taxSvc);

				PingResult result = taxSvc.Ping("");
				if(result.ResultCode >= SeverityLevel.Error)
				{
					MessageBox.Show(result.Messages[0].Summary, "Ping Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					MessageBox.Show("Result Code: " + result.ResultCode + "\r\n" +
						"# Messages: " + result.Messages.Count.ToString() + "\r\n" +
						"Service Version: " + result.Version, "Ping Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
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

		private void buttonIsAuthorized_Click(object sender, EventArgs e)
		{	
			OperationAuthorization frm = new OperationAuthorization(this);
			frm.Owner = this;
			frm.ShowDialog();

//			try
//			{
//				Util.PreMethodCall(this, lblStatus);
//
//				TaxSvc taxSvc = new TaxSvc();
//				SetConfig(taxSvc);
//			    
//				//IsAuthorizedResult result = taxSvc.IsAuthorized("GetTax, PostTax, CommitTax, CancelTax, AdjustTax, GetTaxHistory, ReconcileTaxHistory");
//				IsAuthorizedResult result = taxSvc.IsAuthorized("");
//				if(result.ResultCode >= SeverityLevel.Error)
//				{
//					MessageBox.Show(result.Messages[0].Summary, "IsAuthorized Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
//				}
//				else
//				{
//					MessageBox.Show("Result Code: " + result.ResultCode + "\r\n" +
//						"# Messages: " + result.Messages.Count + "\r\n" +
//						"Expires: " + result.Expires + "\r\n" +
//						"Operations: " + result.Operations, "IsAuthorized Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
//				}
//			}
//			catch (Exception ex)
//			{
//				Util.ShowError(ex);
//			}
//			finally
//			{
//				Util.PostMethodCall(this, lblStatus);
//			}
		}

		private void buttonAbout_Click(object sender, EventArgs e)
		{
			try 
			{
				Util.PreMethodCall(this, lblStatus);

				TaxSvc taxSvc = new TaxSvc();
		    
				MessageBox.Show("About (Version): " + taxSvc.Profile.Adapter, "About Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

		private void buttonGetTax_Click(object sender, System.EventArgs e)
		{
			formGetTax frm = new formGetTax();
			frm.Owner = this;
			frm.ShowDialog();
		}

		private void buttonCancelTax_Click(object sender, System.EventArgs e)
		{
			formCancelTax frm = new formCancelTax();
			frm.Owner = this;
			frm.ShowDialog();
		
		}

		private void buttonPostTax_Click(object sender, System.EventArgs e)
		{
			formPostTax frm = new formPostTax();
			frm.Owner = this;
			frm.ShowDialog();
		}

		private void buttonCommitTax_Click(object sender, System.EventArgs e)
		{
			formCommitTax frm = new formCommitTax();
			frm.Owner = this;
			frm.ShowDialog();
		}

		private void buttonGetTaxHistory_Click(object sender, System.EventArgs e)
		{
			formGetTaxHistory frm = new formGetTaxHistory();
			frm.Owner = this;
			frm.ShowDialog();
		}

		private void buttonReconcileTaxHistory_Click(object sender, System.EventArgs e)
		{
			formReconcileTaxHistory frm = new formReconcileTaxHistory();
			frm.Owner = this;
			frm.ShowDialog();
		}

		private void buttonAdjustTax_Click(object sender, System.EventArgs e)
		{
			formAdjustTax frm = new formAdjustTax();
			frm.Owner = this;
			frm.ShowDialog();
		}

		private void formMain_Load(object sender, System.EventArgs e)
		{
			//ConfigSettings configSettings = ConfigSettings.GetInstance();
			//MessageBox.Show("url:"+configSettings.Url);
			//configSettings.Url="zz";			
		}
	}
}
