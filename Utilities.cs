using System;
using System.Text;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace Avalara.AvaTax.Adapter
{
	/// <summary>
	/// Summary description for Utilities.
	/// </summary>
	internal class Utilities
	{
		private static string avaTraceSoap = "AdapterSoap";

		private Utilities()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		internal static string SoapTraceFileName
		{
			get
			{
				return avaTraceSoap;
			}
			set
			{
				avaTraceSoap = value;
			}
		}

		internal static string AdapterVersion()
		{
			return Assembly.GetExecutingAssembly().GetName().Version.ToString();
		}

		internal static string AdapterName()
		{
			return Assembly.GetExecutingAssembly().GetName().Name;
		}
		/// <summary>
		/// Returns the confilefile path
		/// Ref :http://msdn2.microsoft.com/en-us/library/system.reflection.assembly.location.aspx
		/// </summary>
		/// <returns></returns>
		internal static string ConfigFileName()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			//facing issue with this method adopted another way to return filepath
			//return assembly.CodeBase.Substring(0, assembly.CodeBase.Length) + ".config";
            string filePath = null;
            if (assembly.CodeBase != "")
            {
                filePath = assembly.CodeBase.Substring(0, assembly.CodeBase.Length) + ".config";
            }
            return filePath;
		}

		/// <summary>
		/// Checks the end of the base Url for a "/". If one does not exist, then it adds one.
		/// </summary>
		/// <param name="baseUrl">The base of a Url string. Should not contain the service name.</param>
		/// <returns>A correct base Url path.</returns>
		/// <example>
		/// <code>
		/// string url = BuildSafeUrl("http://localhost/AvaTax.Services");
		/// </code>
		/// Result:
		/// <code>
		/// url = "http://localhost/AvaTax.Services/";
		/// </code>
		/// </example>
		internal static string BuildSafeUrl(string baseUrl)
		{
			if (!baseUrl.EndsWith("/"))
			{
				return baseUrl += "/";
			} 
			else
			{
				return baseUrl;
			}
		}

		/// <summary>
		/// Given an object derived from <see cref="BaseRequest"/>, will verify that the minimum data necessary
		/// to complete the method call to the service exists.  The derived class must fully implement the
		/// <see cref="BaseRequest.IsValid"/> method.
		/// </summary>
		/// <param name="request">An object derived from <see cref="BaseRequest"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown if the request passed in is null.</exception>
		/// <exception cref="ArgumentException">Thrown if the request passed in is determined to be invalid.</exception>
		internal static void VerifyRequestObject(BaseRequest request)
		{
		    string message = string.Empty;

			if (request == null)
			{
				throw new ArgumentNullException("request","Request object");
			}
			if (!request.IsValid(out message))
			{
				throw new ArgumentException(message);
			}
		}

		/// <summary>
		/// While .NET will not let an invalid value be assigned to a variable of type DetailLevel, for example, at compile-time,
		/// a COM client can set the variable to an invalid value at run-time.  Each property member of this type
		/// should call this method to ensure the value is valid before setting an internal variable.
		/// </summary>
		/// <param name="enumType">The enum's type for which a value is being tested</param>
		/// <param name="value">The value to test</param>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if the argument value is not defined in the enum.</exception>
		internal static void VerifyEnum(Type enumType, object value)
		{
			if (!Enum.IsDefined(enumType, value))
			{
				throw new ArgumentOutOfRangeException(enumType.ToString(), value, "Invalid value. Use one of the predefined values provided by the " + enumType.ToString() + " enum.");
			}
		}

		/// <summary>
		/// Throws a FormatException error if the value is not a date.  This method is intended to be called by a
		/// property's set accessor if the property accepts a date as string.
		/// </summary>
		/// <param name="value">The string to test.</param>
		/// <exception cref="FormatException"/>
		internal static void VerifyDate(string value)
		{
			if (value != null && value.Length > 0)
			{
				DateTime.Parse(value);
			}
        }

        /// <summary>
        /// Validate a guid using regular expressions
        /// </summary>
        /// <param name="guid">value to be validated</param>
        /// <returns>true/false</returns>
        internal static bool IsValidGuid(string guid)
        {
            if (guid != null && guid.Trim().Length > 0)
            {
                Regex reg = new Regex(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$");

                return reg.IsMatch(guid);
            }

            return false;
		}
		
		/// <summary>
		/// Enumerates through a collection to determine if an equal object already exists.
		/// </summary>
		/// <param name="collection">The collection to enumerate.</param>
		/// <param name="item">The item for which to search.</param>
		/// <returns>The duplicate item in the collection if it was found, otherwise null.</returns>
		internal static object FindInCollection(BaseArrayList collection, object item)
		{
			object obj = null;

			foreach (Object testObject in collection)
			{
				if (testObject.Equals(item))
				{
					obj = testObject;
					break;
				}
			}

			return obj;
		}

		/// <summary>
		/// Builds Stting to be sent to Ping as AuditMetrics
		/// </summary>
		/// <param name="originalMetric">ClientMetrics:TransactionId,MetricName,Value\n</param>
		/// <param name="transactionId"></param>
		/// <param name="duration">duration taken for a webmethod call</param>
		/// <returns></returns>
		internal static string BuildAuditMetrics(string originalMetric,string transactionId, long duration)
		{			
			StringBuilder str = new StringBuilder(originalMetric);
			str.Append("ClientMetrics:");
			str.Append(transactionId);			
			str.Append(",ClientDuration,");
			str.Append(duration.ToString());		
			str.Append("\n");			
		
			return str.ToString();
		}

		internal static bool HasClientMetricMessage(Avalara.AvaTax.Adapter.Proxies.ProxyMessage[] message)
		{
			if (message != null)
			{
				for (int i = 0; i < message.Length; i++)
				{
					if(message[i].Name.ToLower() == "clientmetricsrequest")
					{
                        return true;						
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Convert Stream to String
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		internal static string ConvertStreamToString(Stream stream)
		{			
			byte[] bytes = new byte[stream.Length];
			stream.Position = 0;
			stream.Read(bytes, 0, (int)stream.Length);
			return System.Text.Encoding.ASCII.GetString(bytes);
		}

		/// <summary>
		/// Writes input string data to given file.
		/// </summary>
		/// <param name="filename">File in which string data to be write</param>
		/// <param name="data">The string data to be written to file</param>
		internal static void FileWriter(string filename, string data)
		{
			StreamWriter writer = null;
			try
			{
				FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Write);
				writer = new StreamWriter(fs);
				writer.WriteLine(data);				
			}
			catch(IOException ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.Message);
			}
			catch(Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.Message);
			}
			finally
			{
				if(writer != null)
				{
					writer.Flush();
					writer.Close();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		internal static bool ToBoolean(string name, bool defaultValue)
		{
			try
			{
				return Convert.ToBoolean(name);
			}
			catch (Exception)
			{
				return defaultValue;
			}
		}
	}

	/// <summary>
	/// Provides date/time related constants and utility operations
	/// </summary>
	internal class DateUtil
	{
		/// <summary>
		/// Minimum date for AvaTax (1/1/1900)
		/// </summary>
		internal static DateTime MinDate = new DateTime(1900, 1, 1);

		/// <summary>
		/// Maximum date for AvaTax (12/31/9999)
		/// </summary>
		internal static DateTime MaxDate = new DateTime(9999, 12, 31);

		/// <summary>
		/// Get Utc Offset
		/// </summary>
		/// <returns>String Utc Offset</returns>
		internal static string GetGMTOffset()
		{
			TimeZone localZone = TimeZone.CurrentTimeZone;
			DateTime localTime = localZone.ToLocalTime( DateTime.Today );
			TimeSpan localOffset = localZone.GetUtcOffset( localTime );
			return (localOffset.Hours >= 0) ? "+" + localOffset.ToString() : localOffset.ToString();
		}

		/// <summary>
		/// Get DateTimeStamp with GMTOffset
		/// </summary>
		/// <returns>String Date TimeStamp</returns>
		internal static string GetDateTimeStamp()
		{
			return DateTime.Now.ToString("s") + DateUtil.GetGMTOffset();
		}
	}
}
