using System;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Avalara.AvaTax.Adapter
{
	/// <summary>
	/// Summary description for AvaLogger.
	/// </summary>
	internal class AvaLogger : IDisposable
	{
	    private bool registeredForTraceListener = false;

		public static AvaLogger GetLogger()
		{
			if(instance == null)
			{
				instance = new AvaLogger();
			}
			return instance;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		public void Error(string message) 
		{	
			if(logLevel <= LogLevel.ERROR)	
			{
				LogMessage("ERROR", message);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		public void Fail(string message) 
		{
			if(logLevel <= LogLevel.FATAL)	
			{
				LogMessage("FATAL", message);
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		public void Information(string message) 
		{
			if(logLevel <= LogLevel.INFO)
			{
				LogMessage("INFO", message);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		public void Warning(string message)
		{
			if(logLevel <= LogLevel.WARNING)
			{
				LogMessage("WARNING", message);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		public void Debug(string message) 
		{
			if(logLevel <= LogLevel.DEBUG)
			{
				LogMessage("DEBUG", message);
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="messageType"></param>
		/// <param name="message"></param>
		public void LogMessage(string messageType, string message)
		{
            if (!registeredForTraceListener && AvaLoggerConfiguration.CurrentLogLevel > LogLevel.NONE)
            {
                if (AvaLoggerConfiguration.LogMessages)
                {
                    RegisterForTraceListener(AvaLoggerConfiguration.MessagesFileName);
                    registeredForTraceListener = true;
                }
            }

			// Write LogMessages in csv format
            //ram
            if (AvaLoggerConfiguration.LogMessages)
            {
                string csv = string.Format("{0},{1},{2}", DateTime.UtcNow, messageType, message);
                try
                {
                    Trace.WriteLine(csv);
                    Trace.Flush();
                }
                catch (Exception)
                {
                    //To handle closed file access exception
                }
            }
		}

		private AvaLogger()
		{			
			logLevel = AvaLoggerConfiguration.CurrentLogLevel;

            if(!AvaLoggerConfiguration.LoadConfiguration())
			{
				Trace.WriteLine("Configuration error");
			}
		}

		// Add Trace Listeners to get messages for logging
		private void RegisterForTraceListener(string logFileName)
		{
			try
			{
				objStream = new FileStream(logFileName, FileMode.OpenOrCreate | FileMode.Append );
				TextWriterTraceListener objTraceListener = new TextWriterTraceListener(objStream) ;
                //ram
                Trace.Listeners.Clear();
				Trace.Listeners.Add(objTraceListener);
			}
			catch(IOException ex)
			{
				Trace.WriteLine("An IO Exception has occurred while Open or Create of " + logFileName);
				Trace.WriteLine(ex.Message);
			}
		}

		public void Dispose()
		{
			if(objStream != null)
			{				
				//Trace.Listeners.RemoveAt(0);
				objStream.Close();
			}
		}		
		private static AvaLogger instance = null;
		private FileStream objStream = null;
		private LogLevel logLevel = LogLevel.NONE;		
	}

	/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/LogLevel/enum/*' />
	[Guid("9617E387-B435-4b69-BD54-2566BA034A5A")]
	[ComVisible(true)]
	public enum LogLevel 
	{ 
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/LogLevel/members[@name="NONE"]/*' />
		NONE = -1, 
		
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/LogLevel/members[@name="DEBUG"]/*' />
		DEBUG, 
		
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/LogLevel/members[@name="INFO"]/*' />
		INFO, 
		
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/LogLevel/members[@name="WARNING"]/*' />
		WARNING, 
		
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/LogLevel/members[@name="ERROR"]/*' />
		ERROR, 
		
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/LogLevel/members[@name="FATAL"]/*' />
		FATAL 
	}

	/// <summary>
	/// Wrapper class for fetching adapter.config logging values for AvaLogger
	/// </summary>
	internal class AvaLoggerConfiguration
	{
		internal static bool LoadConfiguration()
		{
			string configFile = Utilities.ConfigFileName();
			XmlDocument xml = new XmlDocument();
			try 
			{
				xml.Load(configFile);
			}
			catch(Exception ex)
			{
				Trace.WriteLine(string.Format("Information : Unable to load configuration file at \"{0}\". {1}", configFile, ex.Message));
				return false;
			}
			XmlNode node = xml.SelectSingleNode("/configuration/AvaLogger");
			if (node != null)
			{
				if(node.Attributes["logLevel"] != null)
				{
					currentLogLevel = ConvertToLogLevel(node.Attributes["logLevel"].InnerText);

					if(currentLogLevel == LogLevel.NONE)
					{
						logMessages = logTransactions = logSoap = false;
					}
					else
					{
						if(node.Attributes["logMessages"] != null)
						{
							logMessages = Utilities.ToBoolean(node.Attributes["logMessages"].InnerText, false);
						}
						if(node.Attributes["logTransactions"] != null)
						{
							logTransactions = Utilities.ToBoolean(node.Attributes["logTransactions"].InnerText, false);
						}
						if(node.Attributes["logSoap"] != null)
						{
							logSoap = Utilities.ToBoolean(node.Attributes["logSoap"].InnerText, false);
						}
						if(node.Attributes["logFilePath"] != null)
						{
							logFilePath = node.Attributes["logFilePath"].InnerText;
                            CreatePath();
                        }
                    }
                }
            }
            else
            {
                Trace.WriteLine(string.Format("Unable to find section AvaLogger in \"{0}\".", configFile));
                return false;
            }
            return true;
        }

        private static void CreatePath()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (!baseDirectory.EndsWith("/"))
            {
                baseDirectory += "/";
            }
            if (logFilePath == "")
            {
                CreateLogFilePath(baseDirectory + "/logs");
            }
            else
            {
                if (Directory.Exists(baseDirectory + logFilePath))
                {
                    logFilePath = baseDirectory + logFilePath;
                }
                else
                {
                    if (!Directory.Exists(logFilePath))
                    {
                        CreateLogFilePath(logFilePath);
                    }
                }
            }
            if (!logFilePath.EndsWith("/"))
            {
                logFilePath += "/";
            }
		}

        /// <summary>
        /// Check if directory exists for the input filepath else creates a new directory.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static void CreateLogFilePath(string filePath)
        {
            logFilePath = filePath;
            if (!Directory.Exists(logFilePath))
            {
                try
                {
                    Directory.CreateDirectory(logFilePath);
                }
                catch (IOException ex)
                {
                    Trace.WriteLine("An IO Exception has occurred while creating the directory " + logFilePath);
                    Trace.WriteLine(ex.Message);
                }
            }
		}

		/// <summary>
		/// True if logging messages is enabled.  This logs adapter messages like DEBUG, INFO etc.
		/// </summary>
        /// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/AvaLoggerConfiguration/members[@name="LogMessages"]/*' />
        [DispId(20)]
		public static bool LogMessages
		{
			get { return logMessages; }			
            set { logMessages = value; }
		}
		
		/// <summary>
		/// True if logging transactions is enabled.  This logs all transaction details for adapter.
		/// </summary>
        /// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/AvaLoggerConfiguration/members[@name="LogTransactions"]/*' />
        [DispId(21)]
        public static bool LogTransactions
		{
            get { return logTransactions; }
            set { logTransactions = value; }		
		}
		 
		/// <summary>
		/// True if logging SoapTrace is enabled.  This logs adapter SoapTrace in form of SoapRequest and SoapResponse.
		/// </summary>
        /// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/AvaLoggerConfiguration/members[@name="LogSoap"]/*' />
        [DispId(22)]
        public static bool LogSoap
		{
            get { return logSoap; }
            set { logSoap = value; }		
		}
		
		/// <summary>
		/// CurrentLogLevel specified in config file, in AvaLogger element; default value is NONE.
		/// </summary>
        /// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/AvaLoggerConfiguration/members[@name="CurrentLogLevel"]/*' />
        [DispId(23)]
        public static LogLevel CurrentLogLevel
		{
            get { return currentLogLevel; }
            set { currentLogLevel = value; }			
		}

        /// <summary>
        /// File Path to log all messages
        /// </summary>
        /// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/AvaLoggerConfiguration/members[@name="LogFilePath"]/*' />
        [DispId(24)]
        public static string LogFilePath
        {
            get { return logFilePath; }
            set { logFilePath = value;
                CreatePath(); }
		}

		/// <summary>
		/// File name to log all messages
		/// </summary>
		public static string MessagesFileName
		{			
			get{ return logFilePath + "Adapter." + DateTime.Today.ToString("yyyy-MM-dd") + ".log"; }
		}
		
		/// <summary>
		/// File name to log all transaction details
		/// </summary>
		public static string TransactionsFileName
		{
			get{ return logFilePath + "AdapterTransactions." + DateTime.Today.ToString("yyyy-MM-dd") + ".log"; }
		}

		/// <summary>
		/// File name to log SoapRequest and SoapResponse 
		/// </summary>
		public static string SoapFileName
		{
			get{ return logFilePath + "AdapterSoap." + DateTime.Today.ToString("yyyy-MM-dd") + ".log"; }
		}
			
		/// <summary>
		/// Convert string input to LogLevel
		/// </summary>
		/// <param name="logLevel">String logLevel that needs to be converted to LogLevel enum value</param>
		/// <returns>Converted enum LogLevel value</returns>
		/// <remarks>
		///     FATAL = SeverityLevel.Exception
		///		ERROR = SeverityLevel.Error
		///		WARNING = SeverityLevel.Warning
		///		INFO = SeverityLevel.Success
		///		DEBUG or INFO = SeverityLevel.Success
		///		
		///		currentLogLevel="INFO" logSoap="true" would log all soap messages
		///		currentLogLevel="ERROR" logSoap="true" would log Error and Exception result soap messages
        /// SeverityLevel { Success = 0, Warning = 1, Error = 2, Exception }
		///		</remarks>
		public static LogLevel ConvertToLogLevel(string logLevel)
		{	
			switch(logLevel.ToUpper())
			{				
				case "DEBUG" : return LogLevel.DEBUG;
				case "INFO" : 
				case "SUCCESS" : return LogLevel.INFO;
				case "WARNING" : return LogLevel.WARNING;					
				case "ERROR" : return LogLevel.ERROR;					
				case "FATAL" :
				case "EXCEPTION": return LogLevel.FATAL;								
			}
			return LogLevel.NONE;
		}

        private AvaLoggerConfiguration()
        {
        }

		private static bool logMessages = false;
		private static bool logTransactions = false;
		private static bool logSoap = false;
		private static string logFilePath = string.Empty;
		private static LogLevel currentLogLevel = LogLevel.NONE;		
	}
}