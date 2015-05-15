#region Copyright
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System.Runtime.InteropServices;
using Avalara.AvaTax.Adapter.Proxies;

namespace Avalara.AvaTax.Adapter
{
	/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/BaseResult/class/*' />
	[Guid("15EEBF10-7C11-4923-846A-0AAA46661A78")]
	[ComVisible(true)]
	public class BaseResult
	{
		string _transactionId;
		SeverityLevel _resultCode;
		Messages _messages = new Messages();
        //string _referenceCode;

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal BaseResult()
		{
			
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/BaseResult/members[@name="TransactionId"]/*' />
		[DispId(20)]
		public virtual string TransactionId
		{
			get
			{
				return _transactionId;
			}
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/BaseResult/members[@name="ResultCode"]/*' />
		[DispId(21)]
		public virtual SeverityLevel ResultCode
		{
			get
			{
				return _resultCode;
			}
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/BaseResult/members[@name="Messages"]/*' />
		[DispId(22)]
		public virtual Messages Messages
		{
			get
			{
				return _messages;
			}
		}
		
        ///// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/BaseResult/members[@name="ReferenceCode"]/*' />
        //[DispId(23)]
        //public string ReferenceCode
        //{
        //    get { return _referenceCode; }
        //}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="Finalize"]/*' />
		~BaseResult()
		{
			//Messages object will clean up its own collection
			_messages = null;
		}

		#region Internal Members

		internal void CopyFrom(ProxyBaseResult SvcBaseResult)
		{
			_resultCode = (SeverityLevel)SvcBaseResult.ResultCode;
			_transactionId = SvcBaseResult.TransactionId;

			if (SvcBaseResult.Messages != null)
			{
				for (int i = 0; i < SvcBaseResult.Messages.Length; i++)
				{
					//Ignore clientMetrics message 
                    if (SvcBaseResult.Messages[i].Name != null && SvcBaseResult.Messages[i].Name.ToLower() != "clientmetricsrequest")
					{
						Message message = new Message();
						message.CopyFrom(SvcBaseResult.Messages[i]);
						_messages.Add(message);
					}
				}
			}
		}

		/// <summary>
		/// Loads a BaseResult based on an external base result.
		/// </summary>
		/// <param name="baseResult"></param>
		internal void CopyFrom(BaseResult baseResult)
		{
			_resultCode = baseResult.ResultCode;
			_transactionId = baseResult.TransactionId;

			if (baseResult.Messages != null)
			{
				for (int i = 0; i < baseResult.Messages.Count; i++)
				{
					Message message = new Message();
					message.CopyFrom(baseResult.Messages[i]);
					_messages.Add(message);
				}
			}
		}
		#endregion
	}

	/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/Messages/class/*' />
	[Guid("CAB0FC14-8859-4bda-8940-83D87861896B")]
	[ComVisible(true)]
	public class Messages : ReadOnlyArrayList
	{
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal Messages() {}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/collection/members[@name="this"]/*' />
		[DispId(0)]
		public new Message this[int index]
		{
			get
			{
				return (Message)base[ index ];
			}
		}
	}

	/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/Message/class/*' />
	[Guid("003A7D15-DA5B-47ba-8037-7C912A2AD4CE")]
	[ComVisible(true)]
	public class Message 
	{
		string _summary;
		string _details;
		string _helpLink;
 		string _refersTo;
 		SeverityLevel _severity;
 		string _source;
  		string _name;

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal Message() {}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/Message/members[@name="Summary"]/*' />
		[DispId(20)]
		public string Summary
		{
			get { return _summary; }
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/Message/members[@name="Details"]/*' />
		[DispId(21)]
		public string Details
		{
			get { return _details; }
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/Message/members[@name="HelpLink"]/*' />
		[DispId(22)]
		public string HelpLink
		{
			get { return _helpLink; }
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/Message/members[@name="RefersTo"]/*' />
		[DispId(23)]
		public string RefersTo
		{
			get { return _refersTo; }
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/Message/members[@name="Severity"]/*' />
		[DispId(24)]
		public SeverityLevel Severity
		{
			get { return _severity; }
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/Message/members[@name="Source"]/*' />
		[DispId(25)]
		public string Source
		{
			get { return _source; }
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/Message/members[@name="Name"]/*' />
		[DispId(26)]
		public string Name
		{
			get { return _name; }
		}

		#region Internal Members

		internal void CopyFrom(ProxyMessage SvcMessage)
		{
			_summary = SvcMessage.Summary;
			_details = SvcMessage.Details;
			_helpLink = SvcMessage.HelpLink;
			_refersTo = SvcMessage.RefersTo;
			_severity = (SeverityLevel)SvcMessage.Severity;
			_source = SvcMessage.Source;
			_name = SvcMessage.Name;
		}

		/// <summary>
		/// Loads Message based on an external message object.
		/// </summary>
		/// <param name="message"></param>
		internal void CopyFrom(Message message)
		{
			_summary = message.Summary;
			_details = message.Details;
			_helpLink = message.HelpLink;
			_refersTo = message.RefersTo;
			_severity = message.Severity;
			_source = message.Source;
			_name = message.Name;
		}
		#endregion
	}
    
	/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/SeverityLevel/enum/*' />
	[Guid("FDB3DFC7-E562-4a7e-B440-9F28A74D76D5")]
	[ComVisible(true)]
	public enum SeverityLevel 
	{
        
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/SeverityLevel/members[@name="Success"]/*' />
		Success = 0,
        
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/SeverityLevel/members[@name="Warning"]/*' />
		Warning = 1,
        
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/SeverityLevel/members[@name="Error"]/*' />
		Error = 2,
        
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/SeverityLevel/members[@name="Exception"]/*' />
		Exception = 3
	}
}
