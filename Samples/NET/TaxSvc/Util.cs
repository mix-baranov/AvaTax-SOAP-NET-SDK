using System;
using System.Drawing;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using Avalara.AvaTax.Adapter.TaxService;

namespace Avalara.AvaTax.Adapter.Samples.TaxSample
{
	/// <summary>
	/// Summary description for Util.
	/// </summary>
	public class Util
	{
		public Util()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void ShowError(Exception ex)
		{
			MessageBox.Show("Message: " + ex.Message + "\r\n" + 
				"Source: " + ex.Source + "\r\n" +
				"Stack: " + ex.StackTrace, "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public static void ShowError(SoapException ex)
		{
			MessageBox.Show("Message: " + ex.Message + "\r\n" + 
				"Actor: " + ex.Actor + "\r\n" +
				"Code: " + ex.Code + "\r\n" +
				"Detail: " + ex.Detail.InnerText + "\r\n" + 
				"Source: " + ex.Source + "\r\n" +
				"Stack: " + ex.StackTrace, "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public static void PreMethodCall(Form frm, Label lblStatus)
		{
			frm.Cursor = Cursors.WaitCursor;
			lblStatus.Text = "Contacting web service...";
			lblStatus.Refresh();
		}

		public static void PostMethodCall(Form frm, Label lblStatus)
		{
			lblStatus.Text = "";
			frm.Cursor = Cursors.Default;
		}

		public static void SetMessageLabelText(LinkLabel label, BaseResult result)
		{
			if (result.Messages.Count == 0)
			{
				label.Text = "No messages returned";
				label.LinkBehavior = LinkBehavior.NeverUnderline;
			}
			else
			{
				label.Text = "Click here for messages...";
				label.LinkBehavior = LinkBehavior.AlwaysUnderline;
			}
		}

		public static bool IsValidDateTime(string dateTime)
		{
			if (dateTime == null || dateTime.Trim().Length == 0)
			{
				return true;
			}

			try
			{
				Convert.ToDateTime(dateTime);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static bool IsValidLong(string number)
		{
			if (number == null || number.Trim().Length == 0)
			{
				return true;
			}

			try
			{
				Convert.ToInt64(number);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static bool CheckRequiredField(Control control, string fieldName)
		{
			string value = control.Text.Trim();
			if (value.Length == 0)
			{
				string message = string.Format("{0} is a required field. Please fill in a value.", fieldName);
				MessageBox.Show(message, "Required Field Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				control.Focus();
				return false;
			}
			return true;
		}

		public static void FillDocTypeCombo(ComboBox control)
		{
			//control.Items.Add(DocumentType.SalesOrder);
			control.Items.Add(DocumentType.SalesInvoice);
			//control.Items.Add(DocumentType.PurchaseOrder);
			control.Items.Add(DocumentType.PurchaseInvoice);
			//control.Items.Add(DocumentType.ReturnOrder);
			control.Items.Add(DocumentType.ReturnInvoice);
		}		

		public static void FillTaxOverrideTypeCombo(ComboBox control)
		{
			control.Items.Add(TaxOverrideType.None);
			control.Items.Add(TaxOverrideType.TaxAmount);
			control.Items.Add(TaxOverrideType.Exemption);
			control.Items.Add(TaxOverrideType.TaxDate);
		}

		public static void FillServiceModeCombo(ComboBox control)
		{
			control.Items.Add(ServiceMode.Automatic);
			control.Items.Add(ServiceMode.Local);
			control.Items.Add(ServiceMode.Remote);
		}		

		public static TaxOverrideType GetTaxOverrideType(string taxOverrideType)
		{
			TaxOverrideType oTaxOverrideType = TaxOverrideType.None;
			switch(taxOverrideType)
			{
				case "None": oTaxOverrideType = TaxOverrideType.None;
					break;
				case "TaxAmount": oTaxOverrideType = TaxOverrideType.TaxAmount;
					break;
				case "Exemption": oTaxOverrideType = TaxOverrideType.Exemption;
					break;
				case "TaxDate": oTaxOverrideType = TaxOverrideType.TaxDate;
					break;
                case "AccruedTaxAmount": oTaxOverrideType = TaxOverrideType.AccruedTaxAmount;
                    break;
				default: break;
			}
			return oTaxOverrideType;
		}

		/// <summary>
		/// Get Adapter config file name
		/// </summary>
		/// <returns></returns>
		public static string ConfigFileName()
		{
			string path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
			int index = path.LastIndexOf('/');
			string filePath = path.Substring(0, index+1) + "Avalara.AvaTax.Adapter.dll.config";
			//fix to ignore the file:/// prefix before the filepath
			filePath = filePath.Remove(0,8); 
			return filePath.Replace("/",@"\");
		}
	}

	public class DataGridComboBoxColumn : DataGridColumnStyle
	{
		private CustomComboBox comboBox = new CustomComboBox();
		private bool isEditing;

		public DataGridComboBoxColumn()
			: base()
		{
			comboBox.Visible = false;
			comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		}
		protected override void Abort(int rowNum)
		{
			isEditing = false;
			comboBox.SelectedValueChanged -= new EventHandler(comboBoxValueChanged);
			comboBox.LostFocus -= new EventHandler(comboBoxLostFocus);
			Invalidate();
		}
		public Array DataSource
		{
			get
			{
				return comboBox.DataSource as Array;
			}
			set
			{
				comboBox.DataSource = value;
			}
		}

		protected override bool Commit(CurrencyManager dataSource, int rowNum)
		{
			comboBox.Bounds = Rectangle.Empty;

			comboBox.SelectedValueChanged -= new EventHandler(comboBoxValueChanged);
			comboBox.LostFocus -= new EventHandler(comboBoxLostFocus);

			if (!isEditing)
				return true;

			isEditing = false;

			try
			{
				string cellValue = comboBox.Text;
				SetColumnValueAtRow(dataSource, rowNum, cellValue);
			}
			catch (Exception)
			{
				Abort(rowNum);
				return false;
			}

			Invalidate();
			return true;
		}

		protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string displayText, bool cellIsVisible)
		{
			string cellValue = (string)GetColumnValueAtRow(source, rowNum);

			if (cellIsVisible)
			{
				comboBox.Bounds = new Rectangle(bounds.X + 2, bounds.Y + 2, bounds.Width - 4, bounds.Height - 4);
				comboBox.Text = cellValue;
				comboBox.Visible = true;
				comboBox.SelectedValueChanged += new EventHandler(comboBoxValueChanged);
				comboBox.LostFocus += new EventHandler(comboBoxLostFocus);
			}
			else
			{
				comboBox.Text = cellValue;
				comboBox.Visible = false;
			}

			if (comboBox.Visible)
				DataGridTableStyle.DataGrid.Invalidate(bounds);

			comboBox.Focus();
		}

		protected override Size GetPreferredSize(Graphics g, object cellValue)
		{
			return new Size(100, comboBox.PreferredHeight + 4);
		}

		protected override int GetMinimumHeight()
		{
			return comboBox.PreferredHeight + 4;
		}

		protected override int GetPreferredHeight(Graphics g, object value)
		{
			return comboBox.PreferredHeight + 4;
		}

		protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum)
		{
			Paint(g, bounds, source, rowNum, false);
		}

		protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, bool alignToRight)
		{
			Paint(g, bounds, source, rowNum, Brushes.Red, Brushes.Blue, alignToRight);
		}

		protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			string cellValue = (string)GetColumnValueAtRow(source, rowNum);
			Rectangle rect = bounds;
			g.FillRectangle(backBrush, rect);
			rect.Offset(0, 2);
			rect.Height -= 2;
			g.DrawString(cellValue.ToString(), this.DataGridTableStyle.DataGrid.Font, foreBrush, rect);
		}

		protected override void SetDataGridInColumn(DataGrid cellValue)
		{
			base.SetDataGridInColumn(cellValue);
			if (comboBox.Parent != null)
			{
				comboBox.Parent.Controls.Remove(comboBox);
			}
			if (cellValue != null)
			{
				cellValue.Controls.Add(comboBox);
			}
		}

		private void comboBoxValueChanged(object sender, EventArgs e)
		{
			comboBox.SelectedValueChanged -= new EventHandler(comboBoxValueChanged);
			comboBox.LostFocus -= new EventHandler(comboBoxLostFocus);
			this.isEditing = true;
			base.ColumnStartedEditing(comboBox);
		}

		private void comboBoxLostFocus(object sender, EventArgs e)
		{
			comboBox.Visible = false;
			comboBox.LostFocus -= new EventHandler(comboBoxLostFocus);
		}
	}

	public class CustomComboBox : ComboBox
	{
		protected override bool ProcessKeyMessage(ref System.Windows.Forms.Message m)
		{
			return ProcessKeyEventArgs(ref m);
		}
	}
}
