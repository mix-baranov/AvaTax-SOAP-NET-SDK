using System;
using System.Configuration ;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml ;
using System.Xml.Serialization ;
using System.Xml.XPath ;
using System.Diagnostics;
using Avalara.AvaTax.Adapter;

namespace Avalara.AvaTax.Common.Configuration
{
	/// <summary>
	/// Serializes a section of an application config file.
	/// </summary>
	/// <remarks>
	/// This class is used by the .NET ConfigurationSettings class and should not
	/// be instantiated directly.
	/// </remarks>
	/// <example>
	/// The following snippet is from a web.config file:
	/// &lt;?xml version="1.0" encoding="utf-8" ?&gt;
	/// &lt;configuration&gt;
	///     &lt;configSections&gt;
	///         &lt;section name ="AddressSvcConfig" type ="Avalara.AvaTax.Common.Configuration.XmlSerializerSectionHandler, Avalara.AvaTax.Common.Configuration"/&gt;
	///     &lt;/configSections&gt;
	/// &lt;/configuration&gt;
	/// </example>
	[Guid("08bca33a-45b8-4a53-a8af-5442569bf8a3")]
	[ComVisible(false)]
	public class XmlSerializerSectionHandler :
		IConfigurationSectionHandler 
	{
        AvaLogger _avaLog = AvaLogger.GetLogger();
		/// <summary>
		/// Used by ConfigurationSettings to serialize a configuration section.
		/// </summary>
		/// <param name="parent">The configuration settings in a corresponding parent configuration section.</param>
		/// <param name="configContext">An HttpConfigurationContext when Create is called from the ASP.NET configuration system. 
		/// Otherwise, this parameter is reserved and is a null reference (Nothing in Visual Basic).</param>
		/// <param name="section">The XmlNode that contains the configuration information from the configuration file. Provides direct access to the XML contents of the configuration section.</param>
		/// <returns>A configuration object.</returns>
		[DispId(31)]
		public object Create(
			object parent,
			object configContext ,
			System.Xml.XmlNode section) 
		{
			string callingAssemblyName = System.Reflection.Assembly.GetCallingAssembly().GetName().Name;
			string timeStamp = DateTime.Now.ToString();
			string message = "";

			try 
			{
				message = "Attempting to deserialize a configuration object based on a configuration section node.";
                //Ram
                _avaLog.Debug(string.Format(GetDateTimeStamp() + ",DEBUG," + message));
                //Debug.WriteLine(GetDateTimeStamp()+ ",DEBUG," + message);

				XPathNavigator nav = section.CreateNavigator ();
				string typename = ( string ) nav.Evaluate ("string(@type)");

				message = "Object to deserialize: " + typename;
                //Debug.WriteLine(GetDateTimeStamp()+ ",DEBUG," + message);
                _avaLog.Debug(string.Format(GetDateTimeStamp() + ",DEBUG," + message));

				Type t = Type.GetType ( typename );
				XmlSerializer ser = new XmlSerializer (t);
				return ser.Deserialize (new XmlNodeReader (section));
			}
			catch(Exception ex)
			{
#if DEBUG
				Console.WriteLine(ex);
#endif
				message = "Failed to deserialize object. "+ ex.Message;
                //Trace.WriteLine(GetDateTimeStamp()+ ",ERROR," + message);
                _avaLog.Error(GetDateTimeStamp() + ",ERROR," + message);
				throw;
			}
		}

		/// <summary>
		/// Used by AvaTax solutions to manually load a config file.
		/// <seealso cref="XmlSerializerSectionHandler.Create"/>
		/// </summary>
		/// <param name="configFilePath">The full path to the configuration file, including the filename.</param>
		/// <param name="section">The section of the configuration file to load. Assumed to be a child of &lt;configuration&gt;.</param>
		/// <returns>A configuration object if the path to the configuration file and the section name are valid; otherwise null.</returns>
		[DispId(32)]
		public static object CreateFromXmlFile(string configFilePath, string section)
		{
			string callingAssemblyName = System.Reflection.Assembly.GetCallingAssembly().GetName().Name;
			string timeStamp = DateTime.Now.ToString();
			string message = string.Empty;

			XmlDocument xml = new XmlDocument();
			try 
			{
				xml.Load(configFilePath);
			}
			catch
			{
				timeStamp = DateTime.Now.ToString();
				message = string.Format("Unable to load configuration file at \"{0}\".", configFilePath);
			    Trace.WriteLine(GetDateTimeStamp()+ ",INFO," + message);
				return null;
			}

			XmlSerializerSectionHandler handler = new XmlSerializerSectionHandler();
			XmlNode node = xml.SelectSingleNode("/configuration/" + section);
			if (node == null)
			{
				message = string.Format("Unable to find section \"{0}\" in \"{1}\".", section, configFilePath);
				Trace.WriteLine(GetDateTimeStamp()+ ",INFO," + message);
				return null;
			}

			try 
			{
				return handler.Create(null, null,  node);
			}
			catch
			{
				return null;
			}
		}

        ///// <summary>
        ///// Get Utc Offset
        ///// </summary>
        ///// <returns>String Utc Offset</returns>
        //internal static string GetGMTOffset()
        //{
        //    TimeZone localZone = TimeZone.CurrentTimeZone;
        //    DateTime localTime = localZone.ToLocalTime( DateTime.Today );
        //    TimeSpan localOffset = localZone.GetUtcOffset( localTime );			
        //    return (localOffset.Hours >= 0)? "+" + localOffset.ToString() : localOffset.ToString();
        //}

		/// <summary>
		/// Get DateTimeStamp with GMTOffset
		/// </summary>
		/// <returns>String Date TimeStamp</returns>
		internal static string GetDateTimeStamp()
		{
			//return DateTime.Now.ToString("s") + GetGMTOffset();
		    return DateTime.UtcNow.ToString();
		}
	}
}