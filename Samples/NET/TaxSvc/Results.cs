using System;
using System.ComponentModel;
using System.Windows.Forms;
using Avalara.AvaTax.Adapter.AddressService;

namespace Avalara.AvaTax.Adapter.Samples.TaxSample
{
	/// <summary>
	/// Summary description for Results.
	/// </summary>
	public class formResults : Form
	{
		private Button buttonClose;
		private Label label1;
		private Label label2;
		private GroupBox groupBox1;
		private Label label4;
		private Label label5;
		private Label label6;
		private Label label7;
		private Label label8;
		private Label label9;
		private Label label10;
		private Label label11;
		private Label label12;
		private Label label13;
		private Label label14;
		private Label label15;
		private Label label16;
		private Label label17;
		private System.Windows.Forms.Label lblCarrierRoute;
		private System.Windows.Forms.Label lblCountry;
		private System.Windows.Forms.Label lblCounty;
		private System.Windows.Forms.Label lblPostalCode;
		private System.Windows.Forms.Label lblRegion;
		private System.Windows.Forms.Label lblCity;
		private System.Windows.Forms.Label lblLine4;
		private System.Windows.Forms.Label lblLine3;
		private System.Windows.Forms.Label lblLine2;
		private System.Windows.Forms.Label lblLine1;
		private System.Windows.Forms.Label lblAddressType;
		private System.Windows.Forms.Label lblAddressCode;
		private System.Windows.Forms.Label lblResultCode;
		private System.Windows.Forms.Label lblFIPSCode;
		private System.Windows.Forms.Label lblPOSTNet;
		private System.Windows.Forms.Label lblTotal;
		private System.Windows.Forms.Button buttonNext;
		private System.Windows.Forms.Button buttonPrevious;
		private System.Windows.Forms.LinkLabel lblResultMsg;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public formResults(ValidateResult validateResult)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			_validateResult = validateResult;
			lblResultCode.Text = _validateResult.ResultCode.ToString();
			Util.SetMessageLabelText(lblResultMsg, _validateResult);
			_addressesCount = _validateResult.Addresses.Count;
			lblTotal.Text +=  " " + _addressesCount.ToString();
			_currAddress = 0;
			EnableNavButtons();
			if (_addressesCount > 0)
			{
				LoadAddress(_currAddress);
			}
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblFIPSCode = new System.Windows.Forms.Label();
			this.lblPOSTNet = new System.Windows.Forms.Label();
			this.lblCarrierRoute = new System.Windows.Forms.Label();
			this.lblCountry = new System.Windows.Forms.Label();
			this.lblCounty = new System.Windows.Forms.Label();
			this.lblPostalCode = new System.Windows.Forms.Label();
			this.lblRegion = new System.Windows.Forms.Label();
			this.lblCity = new System.Windows.Forms.Label();
			this.lblLine4 = new System.Windows.Forms.Label();
			this.lblLine3 = new System.Windows.Forms.Label();
			this.lblLine2 = new System.Windows.Forms.Label();
			this.lblLine1 = new System.Windows.Forms.Label();
			this.lblAddressType = new System.Windows.Forms.Label();
			this.lblAddressCode = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lblTotal = new System.Windows.Forms.Label();
			this.buttonNext = new System.Windows.Forms.Button();
			this.buttonPrevious = new System.Windows.Forms.Button();
			this.lblResultCode = new System.Windows.Forms.Label();
			this.lblResultMsg = new System.Windows.Forms.LinkLabel();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(392, 484);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(72, 24);
			this.buttonClose.TabIndex = 0;
			this.buttonClose.Text = "Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 20);
			this.label1.TabIndex = 1;
			this.label1.Text = "Result Code:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "Result Msg:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblFIPSCode);
			this.groupBox1.Controls.Add(this.lblPOSTNet);
			this.groupBox1.Controls.Add(this.lblCarrierRoute);
			this.groupBox1.Controls.Add(this.lblCountry);
			this.groupBox1.Controls.Add(this.lblCounty);
			this.groupBox1.Controls.Add(this.lblPostalCode);
			this.groupBox1.Controls.Add(this.lblRegion);
			this.groupBox1.Controls.Add(this.lblCity);
			this.groupBox1.Controls.Add(this.lblLine4);
			this.groupBox1.Controls.Add(this.lblLine3);
			this.groupBox1.Controls.Add(this.lblLine2);
			this.groupBox1.Controls.Add(this.lblLine1);
			this.groupBox1.Controls.Add(this.lblAddressType);
			this.groupBox1.Controls.Add(this.lblAddressCode);
			this.groupBox1.Controls.Add(this.label17);
			this.groupBox1.Controls.Add(this.label16);
			this.groupBox1.Controls.Add(this.label15);
			this.groupBox1.Controls.Add(this.label14);
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.lblTotal);
			this.groupBox1.Controls.Add(this.buttonNext);
			this.groupBox1.Controls.Add(this.buttonPrevious);
			this.groupBox1.Location = new System.Drawing.Point(8, 72);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(456, 404);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Valid Addresses";
			// 
			// lblFIPSCode
			// 
			this.lblFIPSCode.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblFIPSCode.Location = new System.Drawing.Point(96, 376);
			this.lblFIPSCode.Name = "lblFIPSCode";
			this.lblFIPSCode.Size = new System.Drawing.Size(352, 20);
			this.lblFIPSCode.TabIndex = 31;
			// 
			// lblPOSTNet
			// 
			this.lblPOSTNet.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblPOSTNet.Location = new System.Drawing.Point(96, 352);
			this.lblPOSTNet.Name = "lblPOSTNet";
			this.lblPOSTNet.Size = new System.Drawing.Size(352, 20);
			this.lblPOSTNet.TabIndex = 30;
			// 
			// lblCarrierRoute
			// 
			this.lblCarrierRoute.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblCarrierRoute.Location = new System.Drawing.Point(96, 328);
			this.lblCarrierRoute.Name = "lblCarrierRoute";
			this.lblCarrierRoute.Size = new System.Drawing.Size(352, 20);
			this.lblCarrierRoute.TabIndex = 29;
			// 
			// lblCountry
			// 
			this.lblCountry.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblCountry.Location = new System.Drawing.Point(96, 304);
			this.lblCountry.Name = "lblCountry";
			this.lblCountry.Size = new System.Drawing.Size(352, 20);
			this.lblCountry.TabIndex = 28;
			// 
			// lblCounty
			// 
			this.lblCounty.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblCounty.Location = new System.Drawing.Point(96, 280);
			this.lblCounty.Name = "lblCounty";
			this.lblCounty.Size = new System.Drawing.Size(352, 20);
			this.lblCounty.TabIndex = 27;
			// 
			// lblPostalCode
			// 
			this.lblPostalCode.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblPostalCode.Location = new System.Drawing.Point(96, 256);
			this.lblPostalCode.Name = "lblPostalCode";
			this.lblPostalCode.Size = new System.Drawing.Size(352, 20);
			this.lblPostalCode.TabIndex = 26;
			// 
			// lblRegion
			// 
			this.lblRegion.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblRegion.Location = new System.Drawing.Point(96, 232);
			this.lblRegion.Name = "lblRegion";
			this.lblRegion.Size = new System.Drawing.Size(352, 20);
			this.lblRegion.TabIndex = 25;
			// 
			// lblCity
			// 
			this.lblCity.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblCity.Location = new System.Drawing.Point(96, 208);
			this.lblCity.Name = "lblCity";
			this.lblCity.Size = new System.Drawing.Size(352, 20);
			this.lblCity.TabIndex = 24;
			// 
			// lblLine4
			// 
			this.lblLine4.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblLine4.Location = new System.Drawing.Point(96, 184);
			this.lblLine4.Name = "lblLine4";
			this.lblLine4.Size = new System.Drawing.Size(352, 20);
			this.lblLine4.TabIndex = 23;
			// 
			// lblLine3
			// 
			this.lblLine3.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblLine3.Location = new System.Drawing.Point(96, 160);
			this.lblLine3.Name = "lblLine3";
			this.lblLine3.Size = new System.Drawing.Size(352, 20);
			this.lblLine3.TabIndex = 22;
			// 
			// lblLine2
			// 
			this.lblLine2.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblLine2.Location = new System.Drawing.Point(96, 136);
			this.lblLine2.Name = "lblLine2";
			this.lblLine2.Size = new System.Drawing.Size(352, 20);
			this.lblLine2.TabIndex = 21;
			// 
			// lblLine1
			// 
			this.lblLine1.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblLine1.Location = new System.Drawing.Point(96, 112);
			this.lblLine1.Name = "lblLine1";
			this.lblLine1.Size = new System.Drawing.Size(352, 20);
			this.lblLine1.TabIndex = 20;
			// 
			// lblAddressType
			// 
			this.lblAddressType.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblAddressType.Location = new System.Drawing.Point(96, 88);
			this.lblAddressType.Name = "lblAddressType";
			this.lblAddressType.Size = new System.Drawing.Size(352, 20);
			this.lblAddressType.TabIndex = 19;
			// 
			// lblAddressCode
			// 
			this.lblAddressCode.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblAddressCode.Location = new System.Drawing.Point(96, 64);
			this.lblAddressCode.Name = "lblAddressCode";
			this.lblAddressCode.Size = new System.Drawing.Size(352, 20);
			this.lblAddressCode.TabIndex = 18;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(8, 376);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(84, 20);
			this.label17.TabIndex = 17;
			this.label17.Text = "FIPS Code:";
			this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(8, 352);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(84, 20);
			this.label16.TabIndex = 16;
			this.label16.Text = "POSTNet:";
			this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(8, 328);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(84, 20);
			this.label15.TabIndex = 15;
			this.label15.Text = "Carrier Route:";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(8, 304);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(84, 20);
			this.label14.TabIndex = 14;
			this.label14.Text = "Country:";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(8, 280);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(84, 20);
			this.label13.TabIndex = 13;
			this.label13.Text = "County:";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(8, 256);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(84, 20);
			this.label12.TabIndex = 12;
			this.label12.Text = "Zip:";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(8, 232);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(84, 20);
			this.label11.TabIndex = 11;
			this.label11.Text = "State:";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(8, 208);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(84, 20);
			this.label10.TabIndex = 10;
			this.label10.Text = "City:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 184);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(84, 20);
			this.label9.TabIndex = 9;
			this.label9.Text = "Line 4:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 160);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(84, 20);
			this.label8.TabIndex = 8;
			this.label8.Text = "Line 3:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 136);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(84, 20);
			this.label7.TabIndex = 7;
			this.label7.Text = "Line 2:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 112);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(84, 20);
			this.label6.TabIndex = 6;
			this.label6.Text = "Line 1:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 88);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(84, 20);
			this.label5.TabIndex = 5;
			this.label5.Text = "Address Type:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(84, 20);
			this.label4.TabIndex = 4;
			this.label4.Text = "Address Code:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblTotal
			// 
			this.lblTotal.Location = new System.Drawing.Point(84, 32);
			this.lblTotal.Name = "lblTotal";
			this.lblTotal.Size = new System.Drawing.Size(64, 20);
			this.lblTotal.TabIndex = 3;
			this.lblTotal.Text = "Count:";
			this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// buttonNext
			// 
			this.buttonNext.Location = new System.Drawing.Point(148, 28);
			this.buttonNext.Name = "buttonNext";
			this.buttonNext.Size = new System.Drawing.Size(56, 24);
			this.buttonNext.TabIndex = 1;
			this.buttonNext.Text = "Next >>";
			this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
			// 
			// buttonPrevious
			// 
			this.buttonPrevious.Location = new System.Drawing.Point(28, 28);
			this.buttonPrevious.Name = "buttonPrevious";
			this.buttonPrevious.Size = new System.Drawing.Size(56, 24);
			this.buttonPrevious.TabIndex = 0;
			this.buttonPrevious.Text = "<< Prev";
			this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
			// 
			// lblResultCode
			// 
			this.lblResultCode.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblResultCode.Location = new System.Drawing.Point(96, 16);
			this.lblResultCode.Name = "lblResultCode";
			this.lblResultCode.Size = new System.Drawing.Size(352, 20);
			this.lblResultCode.TabIndex = 19;
			// 
			// lblResultMsg
			// 
			this.lblResultMsg.Location = new System.Drawing.Point(96, 40);
			this.lblResultMsg.Name = "lblResultMsg";
			this.lblResultMsg.Size = new System.Drawing.Size(352, 20);
			this.lblResultMsg.TabIndex = 20;
			this.lblResultMsg.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblResultMsg_LinkClicked);
			// 
			// formResults
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(470, 516);
			this.Controls.Add(this.lblResultMsg);
			this.Controls.Add(this.lblResultCode);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonClose);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "formResults";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Validate Results";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private ValidateResult _validateResult;
		private int _addressesCount;
		private int _currAddress;

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void LoadAddress(int index)
		{
			ValidAddress validAddress = _validateResult.Addresses[index];
		    
			//#####################################################
			//### LOAD SELECTED ADDRESS DATA FROM RESULT OBJECT ###
			//#####################################################
			lblAddressCode.Text = validAddress.AddressCode;
			lblAddressType.Text = validAddress.AddressType;
			lblLine1.Text = validAddress.Line1;
			lblLine2.Text = validAddress.Line2;
			lblLine3.Text = validAddress.Line3;
			lblLine4.Text = validAddress.Line4;
			lblCity.Text = validAddress.City;
			lblRegion.Text = validAddress.Region;
			lblPostalCode.Text = validAddress.PostalCode;
			lblCounty.Text = validAddress.County;
			lblCountry.Text = validAddress.Country;
			lblCarrierRoute.Text = validAddress.CarrierRoute;
			lblPOSTNet.Text = validAddress.PostNet;
			lblFIPSCode.Text = validAddress.FipsCode;
		    
		}

		private void EnableNavButtons() {

			//###############
			//### UI CODE ###
			//###############
			buttonPrevious.Enabled = (_addressesCount > 1) && (_currAddress > 1);
			buttonNext.Enabled = (_addressesCount > 1) && (_currAddress < _addressesCount);
		    
		}

		private void buttonPrevious_Click(object sender, System.EventArgs e)
		{
			_currAddress--;
			EnableNavButtons();
			LoadAddress(_currAddress);
		}

		private void buttonNext_Click(object sender, System.EventArgs e)
		{
			_currAddress++;
			EnableNavButtons();
			LoadAddress(_currAddress);
		}

		private void lblResultMsg_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			MessagesForm frm = new MessagesForm(_validateResult.Messages);
			frm.Owner = this;
			frm.ShowDialog();
		}

	}
}
