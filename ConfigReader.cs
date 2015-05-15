#region Copyright (c) 2005 Advantage Solutions, Inc.
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;
using log4net;

//This class is not intended to stand alone or be deployed in its own assembly.
//   Should be included as part of any project that needs this functionality.
namespace Avalara.AvaTax.Adapter
{

	/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/ConfigReader/class/*' />
	[Guid("4D400233-67D2-4083-BFD8-747C70F7F542")]
	[ComVisible(true)]
	internal class ConfigReader
	{
		ILog _log;
		NameValueCollection _appSettings;
		NameValueCollection _securitySettings;
		string _configFile;
		XmlDocument _configXml = new XmlDocument();

		// Loads the <c>appSettings</c> and <c>securitySettings</c> section of a configuration file for an assembly.
		// The configuration file should be in the form <c>[assembly name].config</c>
		// and should be found in the same location as the assembly.
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal ConfigReader()
		{
			Assembly assembly;
			string location;

			try
			{
				_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

				assembly = Assembly.GetExecutingAssembly();
				location = assembly.Location.Substring(0, assembly.Location.Length);

				if (_log.IsDebugEnabled)
				{
					_log.Debug("Config expected at: " + location + ".config");
				}
				this.FileName = location + ".config";

			} catch (Exception ex) {
				_log.Fatal(ex);
				throw;
			}
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/ConfigReader/members[@name="FileName"]/*' />
		public string FileName
		{
			get
			{
				return _configFile;
			}

			set
			{
				if (_log.IsDebugEnabled)
				{
					_log.Debug(value);
				}
				_configFile = value;
				LoadConfigXml();
				_appSettings = null;		//want this to reload the new document if one previously existed.
				_securitySettings = null;	//ditto
			}
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/BaseResult/members[@name="AppSettings"]/*' />
		public NameValueCollection AppSettings
		{
			get
			{
				//if our first time here, load up the collection
				if (_appSettings == null)
				{
					_appSettings = LoadConfig("appSettings");
				}

				return _appSettings;
			}
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/BaseResult/members[@name="SecuritySettings"]/*' />
		public NameValueCollection SecuritySettings
		{
			get
			{
				//if our first time here, load up the collection
				if (_securitySettings == null)
				{
					_securitySettings = LoadConfig("securitySettings");
				}

				return _securitySettings;
			}
		}

		private void LoadConfigXml()
		{
			//TODO: should we fail on a user-defined config name and only eat the error when the system looks for the default config file?
			XmlTextReader reader = null;
			try
			{			
				_configXml = new XmlDocument();
				reader = new XmlTextReader(_configFile);
				_configXml.Load(reader);
			} 
			catch 
			{
				_log.Info("Failed to load config file.");
				//No config file was found. Return empty NameValueCollection.
				_appSettings = new NameValueCollection();
				_securitySettings = new NameValueCollection();
				//  Don't return error; calling code will have to revert to defaults.
			}
			finally
			{
				if (reader != null && reader.ReadState != ReadState.Closed) 
				{
					reader.Close();
				}
			}
			
		}

		/// <summary>
		/// Loads a section of the internal XmlDocument into a collection.
		/// </summary>
		/// <param name="section">The name of the node containing the name-value pairs to be loaded.</param>
		/// <returns>NameValueCollection</returns>
		private NameValueCollection LoadConfig(string section)
		{
			NameValueCollection collection = new NameValueCollection();
			
			XmlNodeList nodeList = _configXml.GetElementsByTagName(section);
			foreach(XmlNode node in nodeList)
			{
				foreach(XmlNode nodeChild in node.ChildNodes) 
				{
					if (nodeChild.LocalName == "add") 
					{
						XmlAttributeCollection attribs = nodeChild.Attributes;
						XmlNode keyNode = attribs.GetNamedItem("key");
						XmlNode valueNode = attribs.GetNamedItem("value");

						//if we have no key, we cannot add anything
						if ((keyNode != null)) 
						{
							//if we have a key but no value, use a default empty string
							string valueAttrib = (valueNode != null ? valueNode.Value : "");
							collection.Add(keyNode.Value, valueAttrib);
						}
					}
				}
			}

			return collection;
		}
	}
}
