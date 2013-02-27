using System;
using System.Runtime.InteropServices;

namespace Avalara.AvaTax.Adapter
{
	/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/AdapterConfigException/class/*' />
	[Guid("73E5168B-72C2-42ca-8FBD-198369C503CB")]
	[ComVisible(true)]
	[Serializable]
	public class AdapterConfigException : Exception
	{
		private string _message = string.Empty;
		private string _refersTo = string.Empty;
		private string _details = string.Empty;
		
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal AdapterConfigException(AdapterConfigurationExceptionType type) : base()
		{
			SetVariables(type);
		}
	
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/AdapterConfigException/members[@name="Message"]/*' />
		[DispId(30)]
		public override string Message
		{
			get { return _message; }
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/AdapterConfigException/members[@name="RefersTo"]/*' />
		[DispId(31)]
		public string RefersTo
		{
			get { return _refersTo; }
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/AdapterConfigException/members[@name="Details"]/*' />
		[DispId(32)]
		public string Details
		{
			get { return _details; }
		}

		#region Private Members

		private void SetVariables(AdapterConfigurationExceptionType type)
		{
			string format = "Attempt to invoke web service method failed. {0} Check the configuration and try again.";

			switch (type)
			{
				case AdapterConfigurationExceptionType.Url:
					_message = string.Format(format, "No URL to the web service has been provided.");
					_refersTo = "Url";
					break;
                case AdapterConfigurationExceptionType.Account:
                    _message = string.Format(format, "No Account to the web service has been provided.");
                    _refersTo = "Account";
                    break;
                case AdapterConfigurationExceptionType.License:
                    _message = string.Format(format, "No License to the web service has been provided.");
                    _refersTo = "License";
                    break;
                case AdapterConfigurationExceptionType.UserName:
                    _message = string.Format(format, "No UserName to the web service has been provided.");
                    _refersTo = "UserName";
                    break;
                case AdapterConfigurationExceptionType.Password:
                    _message = string.Format(format, "No Password to the web service has been provided.");
                    _refersTo = "Password";
                    break;
			}
		}

		#endregion
	}

	/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/AdapterConfigurationExceptionType/enum/*' />
	[Guid("574F5ECF-98D3-4d6d-8A58-6C00B48F22C9")]
	[ComVisible(true)]
	public enum AdapterConfigurationExceptionType
	{
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/AdapterConfigurationExceptionType/members[@name="Url"]/*' />
		Url = 0,

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/AdapterConfigurationExceptionType/members[@name="Account"]/*' />
		Account,
		
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/AdapterConfigurationExceptionType/members[@name="License"]/*' />
		License,
		
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/AdapterConfigurationExceptionType/members[@name="UserName"]/*' />
		UserName,
		
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/AdapterConfigurationExceptionType/members[@name="Password"]/*' />
		Password,
	}
}
