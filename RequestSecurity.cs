using System;
using System.Runtime.InteropServices;

namespace Avalara.AvaTax.Adapter
{

	/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/RequestSecurity/class/*' />
	[Guid("74C759D9-DF1F-4705-A932-449F1787A09E")]
	[ComVisible(true)]	
	public class RequestSecurity
	{
		string _account;
		string _license;
		string _userName;
		string _password;
		int _timeout = 300;	//default to 5 minutes

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal RequestSecurity()
		{
			//not publicly creatable
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/RequestSecurity/members[@name="Account"]/*' />
		[DispId(20)]
		public string Account
		{
			get
			{
				return  _account;
			}
			set
			{
                _account = (value != null) ? value.Trim() : null;
			}
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/RequestSecurity/members[@name="License"]/*' />
		[DispId(21)]
		public string License
		{
			get
			{
				return _license;
			}
			set
			{
                _license = (value != null) ? value.Trim() : null;
			}
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/RequestSecurity/members[@name="UserName"]/*' />
		[DispId(22)]
		public string UserName
		{
			get
			{
				return  _userName;
			}
			set
			{
                _userName = (value != null) ? value.Trim() : null;
			}
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/RequestSecurity/members[@name="Password"]/*' />
		[DispId(23)]
		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
                _password = (value != null) ? value.Trim() : null;
		}
		}

		// 7.21.05 removed from the public API
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/RequestSecurity/members[@name="Timeout"]/*' />
		[DispId(24)]
		[Obsolete("This property is deprecated.")]
		internal int Timeout
		{
			get
			{
				return _timeout;
			}
			set
			{
				//TODO:what error will the service throw if time runs out? Add that error information to the documentation.
				if (value <= 0)
				{
					throw new ArgumentException("Value cannot be zero or a negative number.");
				}
				_timeout = value;
			}
		}


		#region Internal Members

		/// <summary>
		/// Bypasses the Account property whose Set accessor will encrypt the value. Use this method
		/// internally when the adapter has already loaded an encrypted value.
		/// </summary>
		/// <param name="encryptedValue">The account string already encrypted.</param>
		internal void SetEncryptedAccount(string encryptedValue)
		{
			_account = encryptedValue;
		}

		/// <summary>
		/// Bypasses the Key property whose Set accessor will encrypt the value. Use this method
		/// internally when the adapter has already loaded an encrypted value.
		/// </summary>
		/// <param name="encryptedValue">The account string already encrypted.</param>
		internal void SetEncryptedKey(string encryptedValue)
		{
			_license = encryptedValue;
		}

		/// <summary>
		/// Builds the username that is passed to the UsernameToken that validates a web service request.
		/// </summary>
		/// <remarks>
		/// <para>If <see cref="UserName"/> and <see cref="Password"/> are supplied, then the <b>UserName</b>
		/// is passed back unmodified.</para>
		/// <para>If <b>UserName</b> is supplied but <b>Password</b> is blank, then the <b>UserName</b>
		/// is prepended to the <see cref="Account"/> number.</para>
		/// <para>If only <b>Account</b> and <b>License</b> are supplied, then the <b>Account</b> is passed
		/// back unmodified.</para>
		/// <seealso cref="GetTokenPassword"/>
		/// </remarks>
		/// <returns>The username that should be passed to a service</returns>
		internal string GetTokenUserName()
		{
			if ((_userName != null) && (_userName.Length > 0))
			{
				string userName = _userName;
				if ((_password != null) && (_password.Length > 0))
				{
					return userName;
				}
				if (userName.IndexOf("@") >= 1)
				{
					return userName;
				}
				if (_account != null)
				{
					string account = _account;
					return string.Format("{0}@{1}", userName, account);
				}
			}
            if (_account == null || _account == "") return "";
			return _account;
		}

		
		/// <summary>
		/// Builds the password that is passed to the UsernameToken that validates a web service request.
		/// </summary>
		/// <remarks>
		/// <para>If <see cref="UserName"/> and <see cref="Password"/> are supplied, then the <b>Password</b>
		/// is passed back unmodified.</para>
		/// <para>If <b>UserName</b> is supplied but <b>Password</b> is blank, then the <see cref="License"/>
		/// is passed back.</para>
		/// <para>If only <b>Account</b> and <b>License</b> are supplied, then the <b>License</b> is passed
		/// back.</para>
		/// <seealso cref="GetTokenPassword"/>
		/// </remarks>
		/// <returns>The password that should be passed to a service</returns>
		internal string GetTokenPassword()
		{
			if ((_userName != null) && (_userName.Length > 0))
			{
				if ((_password != null) && (_password.Length > 0))
				{
					return _password;
				}
			}
			return _license;
 		}

		#endregion

//		[DispId(25)]
//		public string GetDecryptedText(string encryptedText)
//		{
//			string strTemp = encryptedText.Trim ();
//
//			if(strTemp=="") return "";
//
//			try
//			{
//				strTemp = ConfigDataProtector.Decrypt(strTemp);
//			}
//			catch(Exception ex)
//			{
//				log.Error("Exception in GetDecryptedText:\n"+"Text:"+encryptedText+"\n"+ex.Message);
//			}
//
//			return strTemp;
//		}
	}
}