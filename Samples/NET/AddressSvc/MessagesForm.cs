using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Avalara.AvaTax.Adapter.Samples.AddressSample
{
	/// <summary>
	/// Summary description for Messages.
	/// </summary>
	public class MessagesForm : Form
	{
		private Label label1;
		private ListBox listMessages;
		private Button buttonClose;
		private TextBox textMessage;
		private Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public MessagesForm(Messages messages)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			_messages = messages;
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
			this.listMessages = new System.Windows.Forms.ListBox();
			this.textMessage = new System.Windows.Forms.TextBox();
			this.buttonClose = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Messages:";
			// 
			// listMessages
			// 
			this.listMessages.Location = new System.Drawing.Point(8, 36);
			this.listMessages.Name = "listMessages";
			this.listMessages.Size = new System.Drawing.Size(152, 212);
			this.listMessages.TabIndex = 1;
			this.listMessages.SelectedIndexChanged += new System.EventHandler(this.listMessages_SelectedIndexChanged);
			// 
			// textMessage
			// 
			this.textMessage.BackColor = System.Drawing.Color.White;
			this.textMessage.Location = new System.Drawing.Point(164, 36);
			this.textMessage.Multiline = true;
			this.textMessage.Name = "textMessage";
			this.textMessage.ReadOnly = true;
			this.textMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textMessage.Size = new System.Drawing.Size(324, 212);
			this.textMessage.TabIndex = 2;
			this.textMessage.Text = "";
			this.textMessage.WordWrap = false;
			// 
			// buttonClose
			// 
			this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonClose.Location = new System.Drawing.Point(424, 256);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(64, 24);
			this.buttonClose.TabIndex = 3;
			this.buttonClose.Text = "Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(164, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(132, 20);
			this.label2.TabIndex = 4;
			this.label2.Text = "Selected Message Info:";
			// 
			// MessagesForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonClose;
			this.ClientSize = new System.Drawing.Size(494, 288);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.textMessage);
			this.Controls.Add(this.listMessages);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "MessagesForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Messages";
			this.Load += new System.EventHandler(this.MessagesForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		Messages _messages;

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void MessagesForm_Load(object sender, System.EventArgs e)
		{
			foreach(Message message in _messages)
			{
				listMessages.Items.Add(message.Name);
			}
			if (listMessages.Items.Count > 0)
			{
				listMessages.SelectedIndex = 0;
			}
		}

		private void listMessages_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if (listMessages.SelectedIndex >= 0)
				{
					textMessage.Clear();
					int index = listMessages.SelectedIndex;

					Message message = _messages[index];
					textMessage.AppendText("Name: " + message.Name + "\r\n\r\n");
					textMessage.AppendText("Severity: " + message.Severity.ToString() + "\r\n\r\n");
					textMessage.AppendText("Summary: " + message.Summary + "\r\n\r\n");
					textMessage.AppendText("Details: " + message.Details + "\r\n\r\n");
					textMessage.AppendText("Source: " + message.Source + "\r\n\r\n");
					textMessage.AppendText("RefersTo: " + message.RefersTo + "\r\n\r\n");
					textMessage.AppendText("HelpLink: " + message.HelpLink + "\r\n\r\n");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error retrieving message from Messages collection.");
				Console.Write(ex);
			}
		}
	}
}
