using System;
using System.Web.Services.Protocols;
using System.Windows.Forms;

namespace Avalara.AvaTax.Adapter.Samples.AddressSample
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
}
