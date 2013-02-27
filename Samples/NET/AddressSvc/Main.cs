using System;
using System.ComponentModel;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using System.Xml;
using Avalara.AvaTax.Adapter;
using Avalara.AvaTax.Adapter.AddressService;

namespace Avalara.AvaTax.Adapter.Samples.AddressSample
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class formMain : Form
	{
		private GroupBox groupBox1;
		private GroupBox groupBox2;
		private GroupBox groupBox3;
		private Button buttonClose;
		private Button buttonPing;
		private Button buttonIsAuthorized;
		private Button buttonAbout;
		private Button buttonValidate;
		private Label label1;
		private Label label2;
		private Label label3;
		private Label label4;
		private Label label5;
		private Label label8;
		private Label label9;
		private Label label10;
		private Label label11;
		private Label label12;
		private Label label13;
		private Label label14;
		private Label lblUrl;
		private Label lblAccount;
		private Label lblKey;
		private Label lblUserName;
		private Label lblPassword;
		private System.Windows.Forms.TextBox textCountry;
		private System.Windows.Forms.TextBox textCity;
		private System.Windows.Forms.TextBox textLine3;
		private System.Windows.Forms.TextBox textLine2;
		private System.Windows.Forms.TextBox textLine1;
		private System.Windows.Forms.TextBox textZip;
		private System.Windows.Forms.TextBox textState;
		private System.Windows.Forms.Label lblStatus;
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

			textLine1.Text = "435 Ericksen Ave NE";
			textLine2.Text = "";
			textCity.Text = "Bainbridge Island";
			textState.Text = "WA";
			textZip.Text = "98110";
			textCountry.Text = "US";
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
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.textCountry = new System.Windows.Forms.TextBox();
			this.textZip = new System.Windows.Forms.TextBox();
			this.textState = new System.Windows.Forms.TextBox();
			this.textCity = new System.Windows.Forms.TextBox();
			this.textLine3 = new System.Windows.Forms.TextBox();
			this.textLine2 = new System.Windows.Forms.TextBox();
			this.textLine1 = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.buttonValidate = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
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
			this.groupBox1.Size = new System.Drawing.Size(477, 192);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Configuration";
			// 
			// lblPassword
			// 
			this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblPassword.Location = new System.Drawing.Point(96, 136);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(277, 20);
			this.lblPassword.TabIndex = 12;
			this.lblPassword.Text = "label19";
			// 
			// lblUserName
			// 
			this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblUserName.Location = new System.Drawing.Point(96, 112);
			this.lblUserName.Name = "lblUserName";
			this.lblUserName.Size = new System.Drawing.Size(277, 20);
			this.lblUserName.TabIndex = 11;
			this.lblUserName.Text = "label18";
			// 
			// lblKey
			// 
			this.lblKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblKey.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblKey.Location = new System.Drawing.Point(96, 88);
			this.lblKey.Name = "lblKey";
			this.lblKey.Size = new System.Drawing.Size(277, 20);
			this.lblKey.TabIndex = 10;
			this.lblKey.Text = "label17";
			// 
			// lblAccount
			// 
			this.lblAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblAccount.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblAccount.Location = new System.Drawing.Point(96, 64);
			this.lblAccount.Name = "lblAccount";
			this.lblAccount.Size = new System.Drawing.Size(277, 20);
			this.lblAccount.TabIndex = 9;
			this.lblAccount.Text = "label16";
			// 
			// lblUrl
			// 
			this.lblUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblUrl.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblUrl.Location = new System.Drawing.Point(52, 20);
			this.lblUrl.Name = "lblUrl";
			this.lblUrl.Size = new System.Drawing.Size(324, 28);
			this.lblUrl.TabIndex = 8;
			this.lblUrl.Text = "label15";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(16, 136);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(76, 20);
			this.label5.TabIndex = 6;
			this.label5.Text = "Password:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(16, 112);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(76, 20);
			this.label4.TabIndex = 5;
			this.label4.Text = "UserName:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(16, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(76, 20);
			this.label3.TabIndex = 4;
			this.label3.Text = "License:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(16, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 20);
			this.label2.TabIndex = 3;
			this.label2.Text = "Account:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(11, 20);
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
			this.buttonAbout.TabIndex = 2;
			this.buttonAbout.Text = "About Adapter";
			this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
			// 
			// buttonIsAuthorized
			// 
			this.buttonIsAuthorized.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.buttonIsAuthorized.Location = new System.Drawing.Point(188, 27);
			this.buttonIsAuthorized.Name = "buttonIsAuthorized";
			this.buttonIsAuthorized.Size = new System.Drawing.Size(96, 25);
			this.buttonIsAuthorized.TabIndex = 1;
			this.buttonIsAuthorized.Text = "Is Authorized?";
			this.buttonIsAuthorized.Click += new System.EventHandler(this.buttonIsAuthorized_Click);
			// 
			// buttonPing
			// 
			this.buttonPing.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.buttonPing.Location = new System.Drawing.Point(68, 27);
			this.buttonPing.Name = "buttonPing";
			this.buttonPing.Size = new System.Drawing.Size(96, 25);
			this.buttonPing.TabIndex = 0;
			this.buttonPing.Text = "Ping Service";
			this.buttonPing.Click += new System.EventHandler(this.buttonPing_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.textCountry);
			this.groupBox3.Controls.Add(this.textZip);
			this.groupBox3.Controls.Add(this.textState);
			this.groupBox3.Controls.Add(this.textCity);
			this.groupBox3.Controls.Add(this.textLine3);
			this.groupBox3.Controls.Add(this.textLine2);
			this.groupBox3.Controls.Add(this.textLine1);
			this.groupBox3.Controls.Add(this.label14);
			this.groupBox3.Controls.Add(this.label13);
			this.groupBox3.Controls.Add(this.label12);
			this.groupBox3.Controls.Add(this.label11);
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Controls.Add(this.label9);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Controls.Add(this.buttonValidate);
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox3.Location = new System.Drawing.Point(10, 292);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(477, 204);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Validate Method";
			// 
			// textCountry
			// 
			this.textCountry.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textCountry.Location = new System.Drawing.Point(144, 168);
			this.textCountry.Name = "textCountry";
			this.textCountry.Size = new System.Drawing.Size(128, 20);
			this.textCountry.TabIndex = 18;
			this.textCountry.Text = "";
			// 
			// textZip
			// 
			this.textZip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textZip.Location = new System.Drawing.Point(144, 144);
			this.textZip.Name = "textZip";
			this.textZip.Size = new System.Drawing.Size(128, 20);
			this.textZip.TabIndex = 17;
			this.textZip.Text = "";
			// 
			// textState
			// 
			this.textState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textState.Location = new System.Drawing.Point(143, 120);
			this.textState.Name = "textState";
			this.textState.Size = new System.Drawing.Size(49, 20);
			this.textState.TabIndex = 16;
			this.textState.Text = "";
			// 
			// textCity
			// 
			this.textCity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textCity.Location = new System.Drawing.Point(143, 96);
			this.textCity.Name = "textCity";
			this.textCity.Size = new System.Drawing.Size(189, 20);
			this.textCity.TabIndex = 15;
			this.textCity.Text = "";
			// 
			// textLine3
			// 
			this.textLine3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textLine3.Location = new System.Drawing.Point(143, 72);
			this.textLine3.Name = "textLine3";
			this.textLine3.Size = new System.Drawing.Size(249, 20);
			this.textLine3.TabIndex = 14;
			this.textLine3.Text = "";
			// 
			// textLine2
			// 
			this.textLine2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textLine2.Location = new System.Drawing.Point(143, 48);
			this.textLine2.Name = "textLine2";
			this.textLine2.Size = new System.Drawing.Size(249, 20);
			this.textLine2.TabIndex = 13;
			this.textLine2.Text = "";
			// 
			// textLine1
			// 
			this.textLine1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textLine1.Location = new System.Drawing.Point(143, 24);
			this.textLine1.Name = "textLine1";
			this.textLine1.Size = new System.Drawing.Size(249, 20);
			this.textLine1.TabIndex = 12;
			this.textLine1.Text = "";
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label14.Location = new System.Drawing.Point(52, 172);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(87, 20);
			this.label14.TabIndex = 10;
			this.label14.Text = "Country:";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label13.Location = new System.Drawing.Point(52, 148);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(87, 20);
			this.label13.TabIndex = 9;
			this.label13.Text = "Zip:";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label12.Location = new System.Drawing.Point(52, 124);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(87, 20);
			this.label12.TabIndex = 8;
			this.label12.Text = "State:";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label11
			// 
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label11.Location = new System.Drawing.Point(52, 100);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(87, 20);
			this.label11.TabIndex = 7;
			this.label11.Text = "City:";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label10.Location = new System.Drawing.Point(52, 76);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(87, 20);
			this.label10.TabIndex = 6;
			this.label10.Text = "Line 3:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label9.Location = new System.Drawing.Point(52, 52);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(87, 20);
			this.label9.TabIndex = 5;
			this.label9.Text = "Line 2:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label8.Location = new System.Drawing.Point(52, 28);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(87, 20);
			this.label8.TabIndex = 4;
			this.label8.Text = "Line 1:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// buttonValidate
			// 
			this.buttonValidate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.buttonValidate.Location = new System.Drawing.Point(280, 164);
			this.buttonValidate.Name = "buttonValidate";
			this.buttonValidate.Size = new System.Drawing.Size(112, 24);
			this.buttonValidate.TabIndex = 19;
			this.buttonValidate.Text = "Validate Address";
			this.buttonValidate.Click += new System.EventHandler(this.buttonValidate_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(400, 512);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(84, 24);
			this.buttonClose.TabIndex = 3;
			this.buttonClose.Text = "Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.ForeColor = System.Drawing.Color.Green;
			this.lblStatus.Location = new System.Drawing.Point(16, 512);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(376, 20);
			this.lblStatus.TabIndex = 4;
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// formMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(498, 544);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "formMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SDK Address Sample";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
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
			//lblViaUrl.Text = ViaUrl;
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
			lblUserName.Text = UserName;//new AddressSvc().Configuration.Security.GetDecryptedText(UserName);
			lblPassword.Text = (Password == "" || Password == null) ? "" : "{ encrypted }";
		}


		private void LoadConfig() {

			AddressSvc addressSvc = new AddressSvc();
		    
			Url = addressSvc.Configuration.Url;
			UserName = addressSvc.Configuration.Security.UserName;
			Password = addressSvc.Configuration.Security.Password;
			Account = addressSvc.Configuration.Security.Account;	 
			Key = addressSvc.Configuration.Security.License;
		}

		public void SetConfig(AddressSvc svc) {

			svc.Profile.Client = "AdapterSampleCode,1.0";

			if (Url != null)
			{
				svc.Configuration.Url = Url;
			}
			if (ViaUrl != null)
			{
				svc.Configuration.ViaUrl = ViaUrl;
			}
			//this could be null if we haven't set the plain-text via the Security dialog
			if (Account != null)
			{
				svc.Configuration.Security.Account = Account;
			}
			//this could be null if we haven't set the plain-text via the Security dialog
			if (Key != null)
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
			formSecurity frm = new formSecurity(this);
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
		    
				AddressSvc addressSvc = new AddressSvc();
				SetConfig(addressSvc);

				PingResult result = addressSvc.Ping("");
				if(result.ResultCode >= SeverityLevel.Error)
				{
					MessageBox.Show(result.Messages[0].Summary, "Ping Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					MessageBox.Show("Result Code: " + result.ResultCode.ToString() + "\r\n" +
						"# Messages: " + result.Messages.Count + "\r\n" +
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
		}

		private void buttonAbout_Click(object sender, EventArgs e)
		{
			try 
			{
				Util.PreMethodCall(this, lblStatus);
		    
				AddressSvc addressSvc = new AddressSvc();
		    
				MessageBox.Show("About (Version): " + addressSvc.Profile.Adapter,"About Adapter", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

		private void buttonValidate_Click(object sender, System.EventArgs e)
		{
			try
			{
		    
				//##############################################################################
				//### 1st WE CREATE THE REQUEST OBJECT FOR THE ADDRESS THAT NEEDS VALIDATION ###
				//##############################################################################
				Address address = new Address();
				address.Line1 = textLine1.Text;
				address.Line2 = textLine2.Text;
				address.Line3 = textLine3.Text;
				address.City = textCity.Text;
				address.Region = textState.Text;
				address.PostalCode = textZip.Text;
				address.Country = textCountry.Text;
		    
				//#################################################################################################
				//### 2nd WE INVOKE THE VALIDATE() METHOD OF THE ADDRESSSVC OBJECT AND GET BACK A RESULT OBJECT ###
				//#################################################################################################
				Util.PreMethodCall(this, lblStatus);
		    
				AddressSvc addressSvc = new AddressSvc();
				SetConfig(addressSvc);   //set user-defined config data
		    
				ValidateRequest request = new ValidateRequest();
				request.Address = address;
				request.TextCase = TextCase.Upper;

				ValidateResult result = addressSvc.Validate(request);
		    
				Util.PostMethodCall(this, lblStatus);
		    
				//#######################################
				//### 3rd WE REVIEW THE RESULT OBJECT ###
				//#######################################
				formResults frm = new formResults(result);
				frm.Owner = this;
				frm.ShowDialog();
			} catch (SoapException soapEx)
			{
				Util.ShowError(soapEx);
			} 
			catch (Exception ex)
			{
				Util.ShowError(ex);
			} finally
			{
				Util.PostMethodCall(this, lblStatus);
			}
		}		
	}
}
