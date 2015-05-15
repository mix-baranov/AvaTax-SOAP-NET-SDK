using System;
using System.Web.Services.Protocols;
using Avalara.AvaTax.Adapter.Proxies;

namespace Avalara.AvaTax.Adapter
{
	/// <summary>
	/// Provides the adapter with a single point for logging and handling exceptions.
	/// Not exposed outside of the assembly.
	/// </summary>
	internal class ExceptionManager
	{
		private AvaLogger _avaLog;
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal ExceptionManager()
		{
			_avaLog = AvaLogger.GetLogger();
		}

		/// <summary>
		/// Inspects an exception for type and determines the appropriate custom or generic exception that should be rethrown.
		/// </summary>
		/// <param name="e">The exception caught by a consumer-facing method.</param>
		internal static BaseResult HandleException(Exception e)
		{
			AvaTaxException avaEx;
			Exception ex;
			AvaLogger _avaLog1 = AvaLogger.GetLogger();
			if ((e.GetType() == typeof(System.Reflection.TargetInvocationException)) && (e.InnerException != null))
			{
				ex = e.InnerException;
			}
			else if (e.InnerException != null && (e.InnerException.GetType() == typeof(System.Net.WebException)))
			{
				ex = e.InnerException;
			}
			else
			{
				ex = e;
			}

			if (ex.GetType() == typeof(System.Net.WebException))
			{
				System.Net.WebException webEx = (System.Net.WebException)ex;
				_avaLog1.Error(webEx.Message);

				ProxyBaseResult proxyResult = new ProxyBaseResult();
				proxyResult.ResultCode = ProxySeverityLevel.Error;
				proxyResult.Messages = new ProxyMessage[1];
				proxyResult.Messages[0] = new ProxyMessage();
				proxyResult.Messages[0].Severity = ProxySeverityLevel.Error;
				proxyResult.Messages[0].Summary = webEx.Message;
				proxyResult.Messages[0].Source = webEx.Source;
				proxyResult.Messages[0].HelpLink = webEx.HelpLink;
				proxyResult.Messages[0].Name = webEx.GetType().ToString();

				BaseResult result = new BaseResult();
				result.CopyFrom(proxyResult);

				return result;
			}
			else if (ex.GetType() == typeof(SoapException))
			{
				SoapException soapEx = (SoapException)ex;
				_avaLog1.Fail(soapEx.Message);
				avaEx = new AvaTaxException(soapEx);
				throw avaEx;
			} 
			else if (ex.GetType() == typeof(SoapHeaderException))
			{
				SoapHeaderException soapHeaderEx = (SoapHeaderException)ex;
				avaEx = new AvaTaxException(soapHeaderEx);
				_avaLog1.Fail(soapHeaderEx.Message);
				throw avaEx;
			}
			else if (ex.GetType() == typeof(InvalidOperationException))
			{
				InvalidOperationException operationEx = (InvalidOperationException)ex;
				_avaLog1.Fail(operationEx.Message);
				if (operationEx.InnerException != null)
				{
					throw operationEx.InnerException;
				}
				else
				{
					throw operationEx;
				}

			}
			else
			{
				_avaLog1.Fail(ex.Message);
				throw ex;
			}
		}
	}
}