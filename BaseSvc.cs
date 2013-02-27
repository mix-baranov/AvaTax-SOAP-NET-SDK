#region Copyright
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using Avalara.AvaTax.Adapter.Proxies;
using Avalara.AvaTax.Common.Configuration;

//A group of inherited functionality that all services have in common. The members should be overidden
//    in the derived classes and defined in the interfaces of those classes.
//    It is not intended to be publicly consumed.
namespace Avalara.AvaTax.Adapter
{
	/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/BaseSvc/class/*' />
	[Guid("DDA8D331-69AD-4651-9134-0E585D4170BE")]
	[ComVisible(true)]
	public class BaseSvc : IDisposable
	{
		private AvaLogger _avaLog;
		private SoapHttpClientProtocol _svcProxy;
		private ServiceConfig _config;
		private Profile _profile = new Profile();
		private Security _security = new Security();
		private readonly long _uniqueId = DateTime.Now.Ticks;

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="Constructor"]/*' />
		public BaseSvc()
		{
			//this should be done first upon entering the assembly; expected that AddressSvc and TaxSvc will be
			//    the only instantiated classes and therefore its safe to do this here.

			_avaLog = AvaLogger.GetLogger();
			_avaLog.Debug(string.Format("Instantiating BaseSvc: {0}", _uniqueId));
			
            string configFileName = Utilities.ConfigFileName();
            if (configFileName!= null && configFileName!= "")
            {
                _config = (ServiceConfig)XmlSerializerSectionHandler.CreateFromXmlFile(configFileName, "ServiceConfig");
            }
			if (_config == null)
			{
				//no config file or an invalid config file was found.
				//let's create a default one in case the consumer wants to fill in all fields manually
				_config = new ServiceConfig();
			}
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/BaseSvc/members[@name="Profile"]/*' />
		[DispId(20)]
		public virtual Profile Profile
		{
			get 
			{
				return _profile;
			}
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/BaseSvc/members[@name="Configuration"]/*' />
		[DispId(21)]
		public virtual ServiceConfig Configuration
		{
			get
			{
				return _config;
			}
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/BaseSvc/members[@name="Proxy"]/*' />
		[DispId(22)]
		public IWebProxy Proxy
		{
			get
			{
				return _svcProxy.Proxy;
			}
			
			set
			{
				_svcProxy.Proxy = value;
			}
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/BaseSvc/members[@name="Security"]/*' />
		[DispId(23)]
		public virtual Security Security
		{
			get 
			{
				return _security;
			}
		}
		
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="Finalize"]/*' />
		~BaseSvc()
		{
			this.Dispose();
		}

		#region Internal Members

		string _serviceName;

		internal object InvokeService(Type typeOfProxy, string methodName, object[] arg)
		{
			Object obj = null;
			Transaction transaction = null;
			try
			{
				MethodInfo method = typeOfProxy.GetMethod(methodName);
				if (method == null)
				{
					string msg = string.Format("Method '{0}' does not exist in type '{1}'.", methodName, typeOfProxy.ToString());
					throw new ApplicationException(msg);
				}
				string service = typeOfProxy.Name.Substring(5);				

				//set Url, Security Token, etc immediately prior to the service call
				this.SetServiceAccessProperties();

				_avaLog.Debug("Copying Profile into proxy object");
				FieldInfo field = typeOfProxy.GetField("ProfileValue");
				this.Profile.CopyTo((ProxyProfile)field.GetValue(_svcProxy));

				_avaLog.Debug("Copying Security into proxy object");
				FieldInfo fieldSecurity = typeOfProxy.GetField("Security");
				this.Security.CopyTo((ProxySecurity)fieldSecurity.GetValue(_svcProxy));
				
				// BeginTransaction to log transaction details
				transaction = BeginTransaction(Profile.Machine, service, methodName);
				
				_avaLog.Debug(string.Format("Proxy.{0}", methodName));

                Perf monitor = new Perf();
                monitor.Start();
                obj = method.Invoke(_svcProxy, arg);
                monitor.Stop();

                if (transaction != null)
                {
                    transaction.ClientDuration = monitor.ElapsedSeconds() * 1000;//monitor.Milliseconds;
                }
                _avaLog.Debug(string.Format("{0} (proxy method): {1}", methodName, monitor.ElapsedSeconds() * 1000));
			}
			catch(Exception ex)
			{
				if(transaction != null)
				{
					transaction.SeverityLevelId = (byte)SeverityLevel.Exception;
					transaction.ErrorMessage = ex.Message;
				}
				_avaLog.Error(ex.Message);
				throw ex;
			}
			finally
			{
				EndTransaction((ProxyBaseResult)obj, transaction);
			}
			return obj;
		}

		/// <summary>
		/// Logs the transaction.
		/// </summary>
		/// <param name="machineName"></param>
		/// <param name="serviceName"></param>
		/// <param name="operation"></param>
		/// <returns></returns>
		internal Transaction BeginTransaction(string machineName, string serviceName, string operation)
		{
			if(!AvaLoggerConfiguration.LogTransactions) 
			{
				return null;
			}

			Transaction transaction = new Transaction();

			transaction.ClientTimestamp = DateTime.UtcNow;
            
            try
            {
                transaction.AccountId = int.Parse(_config.Security.Account);
            }
            catch (Exception)
            {
                transaction.AccountId = 0;
            }
			transaction.MachineName = machineName;
			transaction.ServiceName = serviceName;
			transaction.Operation = operation;

			return transaction;
		}

 
		/// <summary>
		/// Completes the transaction and log all the transaction details to log file.
		/// </summary>
		/// <param name="result"></param>
		/// <param name="transaction"></param>
		internal void EndTransaction(ProxyBaseResult result, Transaction transaction)
		{
            if (result == null)
			{
				return;
			}

            if (AvaLoggerConfiguration.LogTransactions && transaction != null)
            {
                transaction.SeverityLevelId = (byte)result.ResultCode;
                transaction.TransactionId = result.TransactionId;
                transaction.ReferenceCode = result.ReferenceCode;
           
                if (transaction.SeverityLevelId >= (int)SeverityLevel.Error && result.Messages != null && result.Messages.Length > 0)
                {
                    foreach (ProxyMessage message in result.Messages)
                    {
                        if (message.Severity == result.ResultCode)
                        {
                            if (message.Name == "SqlException")
                            {
                                transaction.ErrorMessage = message.Name + ": " + message.Summary;
                            }
                            else
                            {
                                transaction.ErrorMessage = message.Name;
                            }
                            break;
                        }
                    }
                }
			
                // Logs all the transaction details to log file
                transaction.Log();
            }
            
            FilterMessage(result);
            
		}
		/// <summary>
        /// Filter ORM and Sql messages like ORMQueryExecutionException and SqlException
		/// </summary>
		/// <param name="result"></param>
        internal void FilterMessage(ProxyBaseResult result)
		{
            if (result == null || result.Messages == null) 
			{
				return;
			}

			foreach (ProxyMessage message in result.Messages)
			{
                if (message.Name != null && (message.Name.IndexOf("ORM") > -1 || message.Name.IndexOf("SqlException") > -1))
				{
					message.Name = "Exception";
					message.Summary = "This operation could not be completed.";
					message.Details = String.Empty;
					message.RefersTo = String.Empty;					
				}
			}
		}

		/// <summary>
		/// Provides read/write access to the SoapHttpClientProtocol proxy object. Intended for internal use only.
		/// </summary>
		internal SoapHttpClientProtocol SvcProxy
		{
			get
			{
				return _svcProxy;
			}
			set
			{
				_svcProxy = value;
			}
		}

		/// <summary>
		/// Provides read/write access to the service name that will be concatenated to the end of the base Url.
		/// Should be set by the derived class's constructor.
		/// </summary>
		internal string ServiceName
		{
			get
			{
				return _serviceName;
			}
			set
			{
				_serviceName = value;
			}
		}

		/// <summary>
		/// Gets a unique id that identifies this instantiation of the service object. Useful when tracking
		/// the lifetime of an object inside a log file.
		/// </summary>
		internal long UniqueId
		{
			get
			{
				return _uniqueId;
			}
		}

		/// <summary>
		/// Packages property and token settings that must be applied to the service proxy object
		/// before a request can be made.
		/// </summary>
		/// <remarks>
		/// Due to a time-sensitive setting in the token, this method
		/// should be called immediately prior to a request.
		/// </remarks>
		internal void SetServiceAccessProperties()
		{
			_avaLog.Debug("Setting the request properties and usertoken for the service request");

			SetRequestProperties();
			SetUserToken();
		}

		/// <summary>
		/// Sets the Url that the SoapHttpClientProtocol proxy uses when making a request.
		/// </summary>
		/// <remarks>
		/// The value retrieved from the <see cref="Configuration"/> class. This method should be called
		/// before any method call since it can be changed at any time during the life of the service object.
		/// It might be overwritten, for example, after the assembly's config file has been loaded.
		/// </remarks>
		internal void SetRequestProperties()
		{
			_avaLog.Debug("SetRequestProperties");
			
			//verify we have the required field prior to manipulating it
			if (this.Configuration.Url == null || this.Configuration.Url.Trim().Length == 0)
			{
				throw new AdapterConfigException(AdapterConfigurationExceptionType.Url);
			}

            bool isAccountValid = (_config.Security.Account != null && _config.Security.Account != string.Empty);
            bool isLicenseValid = (_config.Security.License != null && _config.Security.License != string.Empty);

            if (!isAccountValid || !isLicenseValid)
            {
                bool isUserNameValid = (_config.Security.UserName != null && _config.Security.UserName != string.Empty);
                bool isPasswordValid = (_config.Security.Password != null && _config.Security.Password != string.Empty);


                if (!isUserNameValid || !isPasswordValid)
                {
                    if (!isAccountValid)
                    {
                        throw new AdapterConfigException(AdapterConfigurationExceptionType.Account);
                    }
                    if (!isLicenseValid)
                    {
                        throw new AdapterConfigException(AdapterConfigurationExceptionType.License);
                    }
                    if (!isUserNameValid)
                    {
                        throw new AdapterConfigException(AdapterConfigurationExceptionType.UserName);
                    }
                    if (!isPasswordValid)
                    {
                        throw new AdapterConfigException(AdapterConfigurationExceptionType.Password);
                    }
                }
			}
			
			// Add AccountId as a query string parameter
			string accountId = string.Empty;
			if(_config.Security.Account != string.Empty && _config.Security.Account != null)
			{
                try
                {
                    accountId = "?AccountId=" + int.Parse(_config.Security.Account);
                }
                catch (FormatException)
                {
                    accountId = string.Empty;
                }
			}
			string url = string.Format("{0}{1}{2}", Utilities.BuildSafeUrl(Configuration.Url), _serviceName, accountId);

			_avaLog.Debug(string.Format("address: {0}", url));

			Uri address = new Uri(url.ToLower());	//final destination address (server within a LAN, server behind a firewall, public (unprotected) server)

			if (this.Configuration.ViaUrl == null || this.Configuration.ViaUrl.ToString().Length == 0)
			{
				/*
				 * smart-handling for F5 forwarding. if we have a Url but no ViaUrl and the Url uses SSL,
				 * then rebuild the "address" and strip off the SSL designation (the "s" in https). The F5 servers
				 * have SSL-forwarding but no customers have been making use of ViaUrl and thus the code must
				 * handle this requirement for them. This logic does not override the case in which the customer
				 * explicitly defines a ViaUrl; in that case we default to the customer's designation.
				 */
				
				string secure = "https://";
				string nonsecure = "http://";

				if (url.Trim().StartsWith(secure))
				{
					string newUrl = url.Trim().Replace(secure, nonsecure);	//do not change "url" variable b/c we need that downstream
					_avaLog.Debug(string.Format("new address: {0}", newUrl));
					address = new Uri(newUrl.ToLower());
				}
			}
			else
			{
				url = Utilities.BuildSafeUrl(this.Configuration.ViaUrl.ToString()) + _serviceName ;
			}

			_avaLog.Debug(string.Format("via: {0}", url));

			Uri via = new Uri(url.ToLower());		//intermediary address (such as a specific port on a firewall or an outward-facing address different from an internal server)
			_svcProxy.Url = address.ToString();
			_svcProxy.Timeout = this.Configuration.RequestTimeoutInMilliseconds;			
		}

		/// <summary>
		/// Sets a security token for SoapHeader to protect use of the SOAP request.
		/// </summary>
		/// <remarks>
		/// Because it has a timeout associated with it, this method should be called just prior to the request.
		/// </remarks>
		internal void SetUserToken()
		{
			_avaLog.Debug("SetUserToken");

			RequestSecurity security = this.Configuration.Security;
			string tokenUserName = security.GetTokenUserName();
			// Do not set credentials if not provided
			if (tokenUserName != string.Empty)
			{
				_security.UsernameToken = new UsernameToken();
				_security.UsernameToken.Username = tokenUserName;
				_security.UsernameToken.Password = new Password();
				_security.UsernameToken.Password.Value = security.GetTokenPassword();
			}
		}

		#endregion

		#region Private Members
		
/*
		/// <summary>
		/// Loads the <see cref="ServiceConfig"/> object with the data read from the Avalara.AvaTax.Adapter.dll.config file.
		/// </summary>
		/// <param name="config">The <see cref="ServiceConfig"/> object to load.</param>
		/// <param name="settings">The general appSettings section of the config file.</param>
		private void LoadConfigurationObject(ServiceConfig config, NameValueCollection settings)
		{
			const int DEFAULT_TIMEOUT = 100;

			_log.Debug("Setting ServiceConfig properties");

			if (settings.Get("url") != null && settings.Get("url").Length > 0)
			{
				config.Url = settings.Get("url");
			}
			if (settings.Get("viaurl") != null && settings.Get("viaurl").Length > 0)
			{
				config.ViaUrl = settings.Get("viaurl");
			}

			try 
			{
				config.RequestTimeout = (settings.Get("timeOut") == null ? DEFAULT_TIMEOUT : Int32.Parse(settings.Get("timeout")));
			}
			catch
			{
				config.RequestTimeout = DEFAULT_TIMEOUT;
			}

			_log.Debug("Request Timeout: " + this.Configuration.RequestTimeout.ToString());

			if (settings.Get("tracesoap") != null && settings.Get("tracesoap").Length > 0)
			{
				try
				{
					config.TraceSoap = Convert.ToBoolean(settings.Get("tracesoap"));
				}
				catch
				{
					config.TraceSoap = false;
				}
			}
		}

		/// <summary>
		/// Loads the <see cref="RequestSecurity"/> object with the data read from the Avalara.AvaTax.Adapter.dll.config file.
		/// </summary>
		/// <param name="security">The <see cref="RequestSecurity"/> object to load.</param>
		/// <param name="settings">The security section of the config file.</param>
		private void LoadSecurityObject(RequestSecurity security, NameValueCollection settings)
		{
			const int DEFAULT_TIMEOUT = 300;

			_log.Debug("Setting RequestSecurity properties");
			
			if (settings.Get("account") != null && settings.Get("account").Length > 0)
			{
				security.SetEncryptedAccount(settings.Get("account"));
			}
			if (settings.Get("key") != null && settings.Get("key").Length > 0)
			{
				security.SetEncryptedKey(settings.Get("key"));
			}
			security.UserName = settings.Get("userName");
			security.Password = settings.Get("password");
			try 
			{
				security.Timeout = (settings.Get("timeOut") == null ? DEFAULT_TIMEOUT : Int32.Parse(settings.Get("timeout")));
			}
			catch
			{
				security.Timeout = DEFAULT_TIMEOUT;
			}
			
		}
*/

		#endregion

		#region IDisposable Members

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/dispose/members[@name="Dispose"]/*' />
		[DispId(128)]
		public virtual void Dispose()
		{
			_avaLog.Debug(string.Format("Disposing BaseSvc class: {0}", _uniqueId));

			_svcProxy = null;
			_avaLog = null;
			_config = null;
			_profile = null;
			_security = null;
	  
			// Suppress finalization of this disposed instance.
			GC.SuppressFinalize(this);
		}

		#endregion
	}

	/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/PingResult/class/*' />
	[Guid("B755C197-6859-449d-B164-E932B39D9E79")]
	[ComVisible(true)]
	public class PingResult : BaseResult
	{
		string _version;

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal PingResult()
		{
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/PingResult/members[@name="Version"]/*' />
		[DispId(31)]
		public string Version
		{
			get
			{
				return _version;
			}
		}

		#region Internal Members

		internal void CopyFrom(ProxyPingResult SvcResult)
		{
			//load local object with service return values
			base.CopyFrom(SvcResult);
			_version    = SvcResult.Version;
			
		}

		/// <summary>
		/// Creates a new PingResult based on a <see cref="BaseResult"/>.
		/// </summary>
		/// <param name="baseResult"></param>
		/// <returns></returns>
		internal static PingResult CastFromBaseResult(BaseResult baseResult)
		{
			PingResult result = new PingResult();
			result.CopyFrom(baseResult);
			return result;
		}

		#endregion

	}

	/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/IsAuthorizedResult/class/*' />
	[Guid("0A5D20C8-6A08-4c0e-8633-7B39795C1B3C")]
	[ComVisible(true)]
	public class IsAuthorizedResult : BaseResult
	{
		string _operations;
		DateTime _expires;

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal IsAuthorizedResult()
		{
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/IsAuthorizedResult/members[@name="Operations"]/*' />
		[DispId(30)]
		public string Operations
		{
			get
			{
				return _operations;
			}
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/IsAuthorizedResult/members[@name="Expires"]/*' />
		[DispId(31)]
		public DateTime Expires
		{
			get
			{
				return _expires;
			}
		}

		#region Internal Members

		internal void CopyFrom(ProxyIsAuthorizedResult SvcResult)
		{
			//load local object with service return values
			base.CopyFrom(SvcResult);
			_operations = SvcResult.Operations;
			_expires    = SvcResult.Expires;
		}

		/// <summary>
		/// Creates a new IsAuthorizedResult based on a <see cref="BaseResult"/>.
		/// </summary>
		/// <param name="baseResult"></param>
		/// <returns></returns>
		internal static IsAuthorizedResult CastFromBaseResult(BaseResult baseResult)
		{
			IsAuthorizedResult result = new IsAuthorizedResult();
			result.CopyFrom(baseResult);
			return result;
		}

		#endregion

	}

	/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/Profile/class/*' />
	[Guid("18BA2DA0-6BD1-4e19-8F09-907C3A7EEDB1")]
	[ComVisible(true)]
	public class Profile
	{
		private string _name;
		private string _client;
		private string _adapter;
		private string _machine;

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal Profile()
		{
			_adapter = string.Format("{0},{1}", Utilities.AdapterName(), Utilities.AdapterVersion());
			_machine = Environment.MachineName;
            _client = Utilities.AdapterVersion();
            _name = Utilities.AdapterVersion();
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/Profile/members[@name="Name"]/*' />
		[DispId(20)]
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/Profile/members[@name="Client"]/*' />
		[DispId(21)]
		public string Client
		{
			get { return _client; }
			set { _client = (value.Trim() == string.Empty) ? _client : value; }
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/Profile/members[@name="Adapter"]/*' />
		[DispId(22)]
		public string Adapter
		{
			get { return _adapter; }
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/Profile/members[@name="Machine"]/*' />
		[DispId(23)]
		public string Machine
		{
			get { return _machine; }
		}

		#region Internal Members

		internal void CopyTo(ProxyProfile proxyProfile)
		{
			proxyProfile.Adapter = _adapter;
			proxyProfile.Client = _client;
			proxyProfile.Machine = _machine;
			proxyProfile.Name = _name;
		}
		#endregion
	}

	/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/Password/class/*' />
	[Guid("cf068455-1f4c-430b-bd54-da9f3cfc021c")]
	[ComVisible(true)]
	public class Password
	{
		private string _type;
		private string _value;
 
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal Password()
		{
			_type = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText";
			_value = "";
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/Password/members[@name="Type"]/*' />
		[DispId(20)]
		[XmlAttribute()]
		public string Type
		{
			get { return _type; }
			set { _type = value; }
		} 

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/Password/members[@name="Value"]/*' />
		[DispId(21)]
		[XmlText()]
		public string Value
		{
			get { return _value; }
			set { _value = value; }
		}

		#region Internal Members

		internal void CopyTo(ProxyPassword proxyPassword)
		{
			proxyPassword.Type = _type;
			proxyPassword.Value = _value;
		}
		#endregion
	}
	
	/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/UsernameToken/class/*' />
	[Guid("9e04e2fe-2fe9-4161-b80e-62c51e988d15")]
	[ComVisible(true)]
	public class UsernameToken
	{
		private string _username;
		private Password _password;

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal UsernameToken()
		{
			_username = "";
			_password = null;
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/UsernameToken/members[@name="Username"]/*' />
		[DispId(20)]
		public string Username
		{
			get { return _username; }
			set { _username = value; }
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/UsernameToken/members[@name="Password"]/*' />
		[DispId(21)]
		public Password Password
		{
			get { return _password; }
			set { _password = value; }
		}

		#region Internal Members

		internal void CopyTo(ProxyUsernameToken proxyUsernameToken)
		{
			proxyUsernameToken.Username = _username;
			proxyUsernameToken.Password.Type = _password.Type;
			proxyUsernameToken.Password.Value = _password.Value;
		}
		#endregion
	}

    /// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/Security/class/*' />
	[Guid("83c54260-0149-43e1-9427-8c7daebd1ad1")]
	[ComVisible(true)]
	public class Security : System.Web.Services.Protocols.SoapHeader
	{
		private string _actor;
		private bool _mustUnderstand;
		private UsernameToken _usernameToken;

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal Security()
		{	
			_actor = "http://schemas.xmlsoap.org/soap/actor/next";
			_mustUnderstand = true;
			_usernameToken = null;
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/Security/members[@name="UsernameToken"]/*' />
		[DispId(20)]
		internal UsernameToken UsernameToken
		{
			get { return _usernameToken; }
			set { _usernameToken = value; }
		}

//		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/Security/members[@name="Actor"]/*' />
//		[DispId(21)]
//		[XmlAttribute()]
//		public string Actor
//		{
//			get { return actor; }
//			set { actor = value; }
//		}
//
//		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/Security/members[@name="MustUnderstand"]/*' />
//		[DispId(22)]
//		[XmlAttribute()]
//		public bool MustUnderstand
//		{
//			get { return _mustUnderstand; }
//			set { _mustUnderstand = value; }
//		}

		#region Internal Members

		internal void CopyTo(ProxySecurity proxySecurity)
		{
			proxySecurity.UsernameToken.Username = _usernameToken.Username;
			proxySecurity.UsernameToken.Password.Value = _usernameToken.Password.Value;
			proxySecurity.UsernameToken.Password.Type =_usernameToken.Password.Type;
			proxySecurity.Actor = _actor;
			proxySecurity.MustUnderstand = _mustUnderstand;
		}
		#endregion
	}

	/// <summary>
	/// To trace SoapRequest and SoapResponse
	/// </summary>
	public class SOAPTraceRequest : SoapExtension
	{
		private AvaLogger _avaLog = AvaLogger.GetLogger();
		Stream oldStream;
		Stream newStream;
		string filename = string.Empty;
		static string logString = string.Empty;
		static string header = string.Empty;

		/// <summary>
		/// Save the Stream representing the SOAP request or SOAP response into a local memory buffer.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public override Stream ChainStream( Stream stream )
		{
			oldStream = stream;
			newStream = new MemoryStream();
			return newStream;
		}
		
		/// <summary>
		/// When the SOAP extension is accessed for the first time, the XML Web service method it is applied to
		/// is accessed to store the file name passed in, using the corresponding SoapExtensionAttribute.  
		/// </summary>
		/// <param name="methodInfo"></param>
		/// <param name="attribute"></param>
		/// <returns></returns>
		public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute) 
		{
			return ((SOAPTraceRequestAttribute) attribute).Filename;
		}

		/// <summary>
		/// The SOAP extension was configured to run using a configuration file
		/// instead of an attribute applied to a specific XML Web service method.
		/// </summary>
		/// <param name="WebServiceType"></param>
		/// <returns></returns>
		public override object GetInitializer(Type WebServiceType) 
		{
			// Return a file name to log the trace information to, based on the type.
			return "C:\\" + WebServiceType.FullName + ".log";    
		}
 
		/// <summary>
		/// Receive the file name stored by GetInitializer and store it in a
		/// member variable for this specific instance.
		/// </summary>
		/// <param name="initializer"></param>
		public override void Initialize(object initializer) 
		{
			filename = AvaLoggerConfiguration.SoapFileName;
		}

		/// <summary>
		/// If the SoapMessageStage is such that the SoapRequest or SoapResponse is 
		/// still in the SOAP format to be sent or received, save it out to a file.
		/// </summary>
		/// <param name="message"></param>
		public override void ProcessMessage(SoapMessage message) 
		{
			switch (message.Stage) 
			{
				case SoapMessageStage.BeforeSerialize:
					break;
				case SoapMessageStage.AfterSerialize:
					WriteInput(message);											
					break;
				case SoapMessageStage.BeforeDeserialize:
					WriteOutput(message);				
					break;
				case SoapMessageStage.AfterDeserialize:
					break;
			}
		}
		
		/// <summary>
		/// Write Input
		/// </summary>
		/// <param name="message"></param>
		public void WriteInput(SoapMessage message)
		{	
			CopyToLogFile(newStream, message, true);
			newStream.Position = 0;
			Copy(newStream, oldStream);	
		}

		/// <summary>
		/// Write Output
		/// </summary>
		/// <param name="message"></param>
		public void WriteOutput(SoapMessage message)
		{			
			Copy(oldStream, newStream);						
			CopyToLogFile(newStream, message, false);		
			newStream.Position = 0;
		}

		private void Copy(Stream from, Stream to)
		{
			TextReader reader = new StreamReader(from);
			TextWriter writer = new StreamWriter(to);
			writer.WriteLine(reader.ReadToEnd());		
			writer.Flush();
		}

		/// <summary>
		/// Copy To LogFile
		/// </summary>
		/// <param name="fromStream"></param>
		/// <param name="message"></param>
		/// <param name="isSoapRequest"></param>
		public void CopyToLogFile(Stream fromStream, SoapMessage message, bool isSoapRequest)
		{	
			if(!AvaLoggerConfiguration.LogSoap) 
			{
				return;
			}

			string data = string.Empty;
			string soapString = string.Empty;			
			LogicalMethodInfo methodInfo = message.MethodInfo;
			string service = methodInfo.DeclaringType.Name;

			if(service.StartsWith("Proxy"))
			{
				service = service.Substring(5);
			}

			if(isSoapRequest)
			{
				header = "<SoapOperation service=\"" + service + "\" name=\"" + methodInfo.Name + "\"";
				soapString = (message is SoapServerMessage) ? "SoapResponse" : "SoapRequest";
			}
			else
			{
				soapString = (message is SoapServerMessage) ? "SoapRequest" : "SoapResponse";
			}

			fromStream.Position = 0;
			data += "<" + soapString + " time=\"" + DateUtil.GetDateTimeStamp() + "\">";
			data += Utilities.ConvertStreamToString(fromStream);
			
			data = data.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
			data = Regex.Replace(data, "PasswordText\">.*</Password>", "PasswordText\"></Password>");	

			data += "</" + soapString +">";
						
			if(!isSoapRequest)
				data += "</SoapOperation>";
			
			try
			{
				if(!isSoapRequest)
				{
					string searchElement = "<ResultCode>";
					int startIndex = data.IndexOf(searchElement);
					
					LogLevel currentSeverityLevel = LogLevel.NONE;
					if(startIndex != -1)
					{
						// To retrieve ResultCode from SoapTrace						
						int endIndex = data.IndexOf("</ResultCode>");
						startIndex += searchElement.Length;
						int len = endIndex - startIndex;
						string severityLevel = data.Substring(startIndex, len);
						currentSeverityLevel = AvaLoggerConfiguration.ConvertToLogLevel(severityLevel);

						// To retrieve TransactionId from SoapTrace
						searchElement = "<TransactionId>";
						startIndex = data.IndexOf(searchElement);
						endIndex = data.IndexOf("</TransactionId>");
						startIndex += searchElement.Length;
						len = endIndex - startIndex;						
						header += " TransactionId=\"" + data.Substring(startIndex, len) + "\">";
					}

					if((AvaLoggerConfiguration.CurrentLogLevel <= currentSeverityLevel) && (currentSeverityLevel != LogLevel.NONE))
					{
						Utilities.FileWriter(filename, header + logString + Environment.NewLine + data);
					}
					logString = string.Empty;
				}
				else
				{
					logString = data;
				}
			}
			catch(IOException ex)
			{	
				_avaLog.Error(string.Format("File open or creation failed: {0}. {1}", filename, ex.Message));
			}	
		}
	}
	
	/// <summary>
	/// Create a SoapExtensionAttribute for the SOAP Extension that can be
	/// applied to an XML Web service method.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class SOAPTraceRequestAttribute : SoapExtensionAttribute 
	{
		private string filename = AvaLoggerConfiguration.SoapFileName; 
		private int priority;

		/// <summary>
		/// ExtensionType
		/// </summary>
		public override Type ExtensionType 
		{
			get { return typeof(SOAPTraceRequest); }
		}

		/// <summary>
		/// Priority
		/// </summary>
		public override int Priority 
		{
			get { return priority; }
			set { priority = value; }
		}

		/// <summary>
		/// Filename
		/// </summary>
		public string Filename 
		{
			get { return filename; }
			set { filename = value; }
		}
	}
}
