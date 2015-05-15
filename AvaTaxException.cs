using System;
using System.Runtime.Serialization;
using System.Web.Services.Protocols;
using System.Xml;
using System.Runtime.InteropServices;

namespace Avalara.AvaTax.Adapter
{
	/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/AvaTaxException/class/*' />
	[Guid("C48C1DDE-5931-42af-931B-BCEB15BD4250")]
	[ComVisible(true)]
	[Serializable]
	public class AvaTaxException : Exception
	{
		private string _message;
		private string _refersTo = null;
		private string _details;
		private XmlQualifiedName _faultCode;

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal AvaTaxException(SoapException soapException)
		{
			if (soapException.Detail != null && soapException.Detail.HasChildNodes)
			{
				_details = soapException.Detail.InnerText;
			}
			_message = soapException.Message;
			_faultCode = soapException.Code;
			this.HelpLink = soapException.HelpLink;
			this.Source = soapException.Actor;
		}

		/// <summary>
		/// Initializes the object for serializing.
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected AvaTaxException(SerializationInfo info, 
		                        StreamingContext context) : base(info, context)
		{
			// Implement type-specific serialization constructor logic.
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal AvaTaxException(SoapHeaderException soapHeaderException)
		{
			if (soapHeaderException.Detail != null && soapHeaderException.Detail.HasChildNodes)
			{
				_details = soapHeaderException.Detail.InnerText;
			}
			_refersTo = null;
			_message = soapHeaderException.Message;
			_faultCode = soapHeaderException.Code;
			this.HelpLink = soapHeaderException.HelpLink;
			this.Source = soapHeaderException.Actor;
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/AvaTaxException/members[@name="Message"]/*' />
		[DispId(30)]
		public override string Message
		{
			get { return _message; }
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/AvaTaxException/members[@name="RefersTo"]/*' />
		[DispId(31)]
		public string RefersTo
		{
			get { return _refersTo; }
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/AvaTaxException/members[@name="Details"]/*' />
		[DispId(32)]
		public string Details
		{
			get { return _details; }
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/AvaTaxException/members[@name="FaultCode"]/*' />
		[DispId(33)]
		public XmlQualifiedName FaultCode
		{
			get { return _faultCode; }
		}
	}
}
