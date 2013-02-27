#region Copyright
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.XPath;
using Avalara.AvaTax.Common.Configuration;

namespace Avalara.AvaTax.Adapter
{
	/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/ServiceConfig/class/*' />
	[Guid("41F15B2C-B07C-4223-9B1E-F21C5C98E801")]
	[ComVisible(true)]
	public class ServiceConfig
	{
		//RequestSecurity _security = new RequestSecurity();
		RequestSecurity _security;
		string _url;
		string _viaUrl;
		int _timeout = 100000;
		bool _traceSoap = false;

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		public ServiceConfig()
		{			
            string configFileName = Utilities.ConfigFileName();

            if (configFileName!= null && configFileName!="")
            {
                _security = (RequestSecurity)XmlSerializerSectionHandler.CreateFromXmlFile(configFileName, "RequestSecurity");
            }

			if (_security == null)
			{
				//no config file or an invalid config file was found.
				//let's create a default one in case the consumer wants to fill in all fields manually
				_security = new RequestSecurity();		
			}
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/ServiceConfig/members[@name="Url"]/*' />
		[DispId(20)]
		public string Url
		{
			get
			{
				return _url;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(MethodBase.GetCurrentMethod().Name);
				}
				_url = _viaUrl = value;                 
			}
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/ServiceConfig/members[@name="ViaUrl"]/*' />
		[DispId(21)]
		public string ViaUrl
		{
			get
			{
				return _viaUrl;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(MethodBase.GetCurrentMethod().Name);
				}
				_viaUrl = value;
			}
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/ServiceConfig/members[@name="RequestTimeout"]/*' />
		[DispId(22)]
		public int RequestTimeout
		{
			//comes in seconds but we store in milliseconds 
			//(which is how the proxy will want it)
			get
			{
				return (_timeout / 1000);
			}
			set
			{
				//TODO:should we set an upperbound on this?
				if (value <= 0 || value > 3600)
				{
					throw new ArgumentOutOfRangeException("value", "Expected value between 1 and 3600");
				}
				_timeout = (value * 1000);
			}
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/ServiceConfig/members[@name="Security"]/*' />
		[DispId(23)]
		public RequestSecurity Security
		{
			get
			{
				return _security;
			}
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/ServiceConfig/members[@name="TraceSoap"]/*' />
		[DispId(24)]
		public bool TraceSoap
		{
			get
			{
				return _traceSoap;
			}
			set
			{
				_traceSoap = value;
			}
		}

		/// <summary>
		/// Internally only. Gets or sets the time out in milliseconds.
		/// </summary>
		internal int RequestTimeoutInMilliseconds
		{
			get
			{
				return _timeout;
			}
			set
			{
				_timeout = value;
			}
		}

        /// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/ServiceConfig/members[@name="LogMessages"]/*' />
        [DispId(26)]
        public bool LogMessages
        {
            get { return AvaLoggerConfiguration.LogMessages; }
            set { AvaLoggerConfiguration.LogMessages = value; }
        }

        /// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/ServiceConfig/members[@name="LogTransactions"]/*' />
        [DispId(27)]
        public bool LogTransactions
        {
            get { return AvaLoggerConfiguration.LogTransactions; }
            set { AvaLoggerConfiguration.LogTransactions = value; }
        }

        /// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/ServiceConfig/members[@name="LogSoap"]/*' />
        [DispId(28)]
        public bool LogSoap
        {
            get { return AvaLoggerConfiguration.LogSoap; }
            set { AvaLoggerConfiguration.LogSoap = value; }
        }

        /// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/ServiceConfig/members[@name="LogFilePath"]/*' />
        [DispId(29)]
        public string LogFilePath
        {
            get { return AvaLoggerConfiguration.LogFilePath; }
            set { AvaLoggerConfiguration.LogFilePath = value; }
        }

        /// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/ServiceConfig/members[@name="LogLevel"]/*' />
        [DispId(30)]
        public LogLevel LogLevel
        {
            get { return AvaLoggerConfiguration.CurrentLogLevel; }
            set { AvaLoggerConfiguration.CurrentLogLevel = value; }
        }

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="Finalize"]/*' />
		~ServiceConfig()
		{
			_security = null;
		}
	}
}
