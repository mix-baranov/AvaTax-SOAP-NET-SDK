using System;
using System.ComponentModel;
using System.Windows.Forms;
using Avalara.AvaTax.Adapter.AddressService;

namespace Avalara.AvaTax.Adapter.Samples.TaxSample
{
	/// <summary>
	/// Summary description for Address.
	/// </summary>
	public class formAddress : Form
	{
		private Label label2;
		private Label label3;
		private Label label4;
		private Label label5;
		private Label label6;
		private Label label7;
		private Label label8;
		private Button buttonCancel;
		private Button buttonUpdate;
		private TextBox textLine1;
		private TextBox textLine2;
		private TextBox textLine3;
		private TextBox textCity;
		private TextBox textRegion;
		private TextBox textPostalCode;
		private TextBox textCountry;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public formAddress(Address address)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			_address = address;
			LoadAddressData();
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
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.textLine1 = new System.Windows.Forms.TextBox();
			this.textLine2 = new System.Windows.Forms.TextBox();
			this.textLine3 = new System.Windows.Forms.TextBox();
			this.textCity = new System.Windows.Forms.TextBox();
			this.textRegion = new System.Windows.Forms.TextBox();
			this.textPostalCode = new System.Windows.Forms.TextBox();
			this.textCountry = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(-4, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 20);
			this.label2.TabIndex = 1;
			this.label2.Text = "Line 1:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(-4, 44);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 20);
			this.label3.TabIndex = 2;
			this.label3.Text = "Line 2:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(-4, 68);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 20);
			this.label4.TabIndex = 3;
			this.label4.Text = "Line 3:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(-4, 92);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 20);
			this.label5.TabIndex = 4;
			this.label5.Text = "City:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(-4, 116);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(80, 20);
			this.label6.TabIndex = 5;
			this.label6.Text = "State:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(-4, 140);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(80, 20);
			this.label7.TabIndex = 6;
			this.label7.Text = "Zip:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(-4, 164);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(80, 20);
			this.label8.TabIndex = 7;
			this.label8.Text = "Country:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(100, 216);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(71, 24);
			this.buttonCancel.TabIndex = 8;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Location = new System.Drawing.Point(176, 216);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(71, 24);
			this.buttonUpdate.TabIndex = 9;
			this.buttonUpdate.Text = "Update";
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// textLine1
			// 
			this.textLine1.Location = new System.Drawing.Point(76, 20);
			this.textLine1.Name = "textLine1";
			this.textLine1.Size = new System.Drawing.Size(252, 20);
			this.textLine1.TabIndex = 11;
			this.textLine1.Text = "";
			// 
			// textLine2
			// 
			this.textLine2.Location = new System.Drawing.Point(76, 44);
			this.textLine2.Name = "textLine2";
			this.textLine2.Size = new System.Drawing.Size(252, 20);
			this.textLine2.TabIndex = 12;
			this.textLine2.Text = "";
			// 
			// textLine3
			// 
			this.textLine3.Location = new System.Drawing.Point(76, 68);
			this.textLine3.Name = "textLine3";
			this.textLine3.Size = new System.Drawing.Size(252, 20);
			this.textLine3.TabIndex = 13;
			this.textLine3.Text = "";
			// 
			// textCity
			// 
			this.textCity.Location = new System.Drawing.Point(76, 92);
			this.textCity.Name = "textCity";
			this.textCity.Size = new System.Drawing.Size(176, 20);
			this.textCity.TabIndex = 14;
			this.textCity.Text = "";
			// 
			// textRegion
			// 
			this.textRegion.Location = new System.Drawing.Point(76, 116);
			this.textRegion.Name = "textRegion";
			this.textRegion.Size = new System.Drawing.Size(44, 20);
			this.textRegion.TabIndex = 15;
			this.textRegion.Text = "";
			// 
			// textPostalCode
			// 
			this.textPostalCode.Location = new System.Drawing.Point(76, 140);
			this.textPostalCode.Name = "textPostalCode";
			this.textPostalCode.Size = new System.Drawing.Size(116, 20);
			this.textPostalCode.TabIndex = 16;
			this.textPostalCode.Text = "";
			// 
			// textCountry
			// 
			this.textCountry.Location = new System.Drawing.Point(76, 164);
			this.textCountry.Name = "textCountry";
			this.textCountry.Size = new System.Drawing.Size(116, 20);
			this.textCountry.TabIndex = 17;
			this.textCountry.Text = "";
			// 
			// formAddress
			// 
			this.AcceptButton = this.buttonUpdate;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(350, 260);
			this.Controls.Add(this.textCountry);
			this.Controls.Add(this.textPostalCode);
			this.Controls.Add(this.textRegion);
			this.Controls.Add(this.textCity);
			this.Controls.Add(this.textLine3);
			this.Controls.Add(this.textLine2);
			this.Controls.Add(this.textLine1);
			this.Controls.Add(this.buttonUpdate);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "formAddress";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Address";
			this.ResumeLayout(false);

		}
		#endregion

		private Address _address;

		private void buttonUpdate_Click(object sender, EventArgs e)
		{
			_address.Line1 = textLine1.Text;
			_address.Line2 = textLine2.Text;
			_address.Line3 = textLine3.Text;
			_address.City = textCity.Text;
			_address.Region = textRegion.Text;
			_address.PostalCode = textPostalCode.Text;
			_address.Country = textCountry.Text;
			
			this.Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void LoadAddressData()
		{
			textLine1.Text = _address.Line1;
			textLine2.Text = _address.Line2;
			textLine3.Text = _address.Line3;
			textCity.Text = _address.City;
			textRegion.Text = _address.Region;
			textPostalCode.Text = _address.PostalCode;
			textCountry.Text = _address.Country;			
		}
	}
}
