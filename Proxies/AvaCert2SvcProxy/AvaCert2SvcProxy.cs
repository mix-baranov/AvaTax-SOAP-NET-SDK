#region Copyright
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Avalara.AvaTax.Adapter.Proxies.AvaCert2SvcProxy
{
    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/AvaCert2Svc/*' />
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [WebServiceBinding(Name = "AvaCert2SvcSoap", Namespace = "http://avatax.avalara.com/services")]
    [XmlInclude(typeof(ProxyBaseResult))]
    [ComVisible(false)]
    public class ProxyAvaCert2Svc : SoapHttpClientProtocol
    {
        /// <remarks/>
		public ProxySecurity Security;
		/// <remarks/>
		public ProxyProfile ProfileValue;
 
		/// <remarks/>
        internal ProxyAvaCert2Svc() 
		{
			//not publicly creatable
		}

        /// <remarks/>
        [SOAPTraceRequest]
        [SoapHeaderAttribute("ProfileValue"), SoapHeaderAttribute("Security")]
        [SoapDocumentMethod("http://avatax.avalara.com/services/CustomerSave", RequestNamespace = "http://avatax.avalara.com/services", ResponseNamespace = "http://avatax.avalara.com/services", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        public ProxyCustomerSaveResult CustomerSave(ProxyCustomerSaveRequest CustomerSaveRequest)
        {
            object[] results = this.Invoke("CustomerSave", new object[] {CustomerSaveRequest});
            return ((ProxyCustomerSaveResult) (results[0]));
        }

        /// <remarks/>
        public IAsyncResult BeginCustomerSave(ProxyCustomerSaveRequest CustomerSaveRequest, AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("CustomerSave", new object[] { CustomerSaveRequest}, callback, asyncState);
        }

        /// <remarks/>
        public ProxyCustomerSaveResult EndCustomerSave(IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ProxyCustomerSaveResult)(results[0]));
        }

        /// <remarks/>
        [SOAPTraceRequest]
        [SoapHeaderAttribute("ProfileValue"), SoapHeaderAttribute("Security")]
        [SoapDocumentMethod("http://avatax.avalara.com/services/CertificateRequestInitiate", RequestNamespace = "http://avatax.avalara.com/services", ResponseNamespace = "http://avatax.avalara.com/services", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        public ProxyCertificateRequestInitiateResult CertificateRequestInitiate(ProxyCertificateRequestInitiateRequest CertificateRequestInitiateRequest)
        {
            object[] results = this.Invoke("CertificateRequestInitiate", new object[] { CertificateRequestInitiateRequest});
            return ((ProxyCertificateRequestInitiateResult)(results[0]));
        }

        /// <remarks/>
        public IAsyncResult BeginCertificateRequestInitiate(ProxyCertificateRequestInitiateRequest CertificateRequestInitiateRequest, AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("CertificateRequestInitiate", new object[] { CertificateRequestInitiateRequest}, callback, asyncState);
        }

        /// <remarks/>
        public ProxyCertificateRequestInitiateResult EndCertificateRequestInitiate(IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ProxyCertificateRequestInitiateResult)(results[0]));
        }

        /// <remarks/>
        [SOAPTraceRequest]
        [SoapHeaderAttribute("ProfileValue"), SoapHeaderAttribute("Security")]
        [SoapDocumentMethod("http://avatax.avalara.com/services/CertificateGet", RequestNamespace = "http://avatax.avalara.com/services", ResponseNamespace = "http://avatax.avalara.com/services", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        public ProxyCertificateGetResult CertificateGet(ProxyCertificateGetRequest CertificateGetRequest)
        {
            object[] results = this.Invoke("CertificateGet", new object[] { CertificateGetRequest });
            return ((ProxyCertificateGetResult)(results[0]));
        }

        /// <remarks/>
        public IAsyncResult BeginCertificateGet(ProxyCertificateGetRequest CertificateGetRequest, AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("CertificateGet", new object[] { CertificateGetRequest }, callback, asyncState);
        }

        /// <remarks/>
        public ProxyCertificateGetResult EndCertificateGet(IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ProxyCertificateGetResult)(results[0]));
        }

        /// <remarks/>
        [SOAPTraceRequest]
        [SoapHeaderAttribute("ProfileValue"), SoapHeaderAttribute("Security")]
        [SoapDocumentMethod("http://avatax.avalara.com/services/CertificateRequestGet", RequestNamespace = "http://avatax.avalara.com/services", ResponseNamespace = "http://avatax.avalara.com/services", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        public ProxyCertificateRequestGetResult CertificateRequestGet(ProxyCertificateRequestGetRequest CertificateRequestGetRequest)
        {
            object[] results = this.Invoke("CertificateRequestGet", new object[] { CertificateRequestGetRequest });
            return ((ProxyCertificateRequestGetResult)(results[0]));
        }

        /// <remarks/>
        public IAsyncResult BeginCertificateRequestGet(ProxyCertificateRequestGetRequest CertificateRequestGetRequest, AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("CertificateRequestGet", new object[] { CertificateRequestGetRequest }, callback, asyncState);
        }

        /// <remarks/>
        public ProxyCertificateRequestGetResult EndCertificateRequestGet(IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ProxyCertificateRequestGetResult)(results[0]));
        }

        /// <remarks/>
        [SOAPTraceRequest]
        [SoapHeaderAttribute("ProfileValue"), SoapHeaderAttribute("Security")]
        [SoapDocumentMethod("http://avatax.avalara.com/services/CertificateImageGet", RequestNamespace = "http://avatax.avalara.com/services", ResponseNamespace = "http://avatax.avalara.com/services", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        public ProxyCertificateImageGetResult CertificateImageGet(ProxyCertificateImageGetRequest CertificateImageGetRequest)
        {
            object[] results = this.Invoke("CertificateImageGet", new object[] { CertificateImageGetRequest });
            return ((ProxyCertificateImageGetResult)(results[0]));
        }

        /// <remarks/>
        public IAsyncResult BeginCertificateImageGet(ProxyCertificateImageGetRequest CertificateImageGetRequest, AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("CertificateImageGet", new object[] { CertificateImageGetRequest }, callback, asyncState);
        }

        /// <remarks/>
        public ProxyCertificateImageGetResult EndCertificateImageGet(IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ProxyCertificateImageGetResult)(results[0]));
        }

        /// <remarks/>
        [SOAPTraceRequest]
        [SoapHeaderAttribute("ProfileValue"), SoapHeaderAttribute("Security")]
        [SoapDocumentMethod("http://avatax.avalara.com/services/Ping", RequestNamespace = "http://avatax.avalara.com/services", ResponseNamespace = "http://avatax.avalara.com/services", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        public ProxyPingResult Ping(string Message)
        {
            object[] results = this.Invoke("Ping", new object[] {
																	Message});
            return ((ProxyPingResult)(results[0]));
        }

        /// <remarks/>
        public IAsyncResult BeginPing(string Message, AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Ping", new object[] {
															 Message}, callback, asyncState);
        }

        /// <remarks/>
        public ProxyPingResult EndPing(IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ProxyPingResult)(results[0]));
        }

        /// <remarks/>
        [SOAPTraceRequest]
        [SoapHeaderAttribute("ProfileValue"), SoapHeaderAttribute("Security")]
        [SoapDocumentMethod("http://avatax.avalara.com/services/IsAuthorized", RequestNamespace = "http://avatax.avalara.com/services", ResponseNamespace = "http://avatax.avalara.com/services", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        public ProxyIsAuthorizedResult IsAuthorized(string Operations)
        {
            object[] results = this.Invoke("IsAuthorized", new object[] {
																			Operations});
            return ((ProxyIsAuthorizedResult)(results[0]));
        }

        /// <remarks/>
        public IAsyncResult BeginIsAuthorized(string Operations, AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("IsAuthorized", new object[] {
																	 Operations}, callback, asyncState);
        }

        /// <remarks/>
        public ProxyIsAuthorizedResult EndIsAuthorized(IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ProxyIsAuthorizedResult)(results[0]));
        }

    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/CustomerSaveRequest/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "CustomerSaveRequest")]
    [ComVisible(false)]
    public class ProxyCustomerSaveRequest : ProxyBaseRequest
    {
        /// <remarks/>
        public string CompanyCode;

        /// <remarks/>
        public ProxyCustomer Customer;
    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/CustomerSaveResult/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "CustomerSaveResult")]
    [ComVisible(false)]
    public class ProxyCustomerSaveResult : ProxyBaseResult
    {
    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/CertificateRequestInitiateRequest/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "CertificateRequestInitiateRequest")]
    [ComVisible(false)]
    public class ProxyCertificateRequestInitiateRequest : ProxyBaseRequest
    {
        /// <remarks/>
        public string CompanyCode;

        /// <remarks/>
        public string CustomerCode;

        /// <remarks/>
        public ProxyCommunicationMode CommunicationMode;

        /// <remarks/>
        public string SourceLocationCode;
        
        /// <remarks/>
        public ProxyRequestType Type;
        
        /// <remarks/>
        public string CustomMessage;
        
        /// <remarks/>
        public string LetterTemplate;
        
        /// <remarks/>
        public bool IncludeCoverPage;
        
        /// <remarks/>
        public string CloseReason;
        
        /// <remarks/>
        public string RequestId;
    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/CertificateRequestInitiateResult/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "CertificateRequestInitiateResult")]
    [ComVisible(false)]
    public class ProxyCertificateRequestInitiateResult : ProxyBaseResult
    {
        /// <remarks/>
        public string TrackingCode;

        /// <remarks/>
        public string WizardLaunchUrl;

        /// <remarks/>
        public string RequestId;

        /// <remarks/>
        public string CustomerCode;
    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/CertificateGetRequest/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "CertificateGetRequest")]
    [ComVisible(false)]
    public class ProxyCertificateGetRequest : ProxyBaseRequest
    {
        /// <remarks/>
        public string CompanyCode;

        /// <remarks/>
        public string CustomerCode;

        /// <remarks/>
        public DateTime ModFromDate;

        /// <remarks/>
        public DateTime ModToDate;
    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/CertificateGetResult/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "CertificateGetResult")]
    [ComVisible(false)]
    public class ProxyCertificateGetResult : ProxyBaseResult
    {
        /// <remarks/>
        public ProxyCertificate[] Certificates;
    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/CertificateRequestGetRequest/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "CertificateRequestGetRequest")]
    [ComVisible(false)]
    public class ProxyCertificateRequestGetRequest : ProxyBaseRequest
    {
        /// <remarks/>
        public string CompanyCode;

        /// <remarks/>
        public string CustomerCode;

        /// <remarks/>
        public ProxyCertificateRequestStatus RequestStatus;

        /// <remarks/>
        public DateTime ModFromDate;

        /// <remarks/>
        public DateTime ModToDate;
    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/CertificateRequestGetResult/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "CertificateRequestGetResult")]
    [ComVisible(false)]
    public class ProxyCertificateRequestGetResult : ProxyBaseResult
    {
        /// <remarks/>
        public ProxyCertificateRequest[] CertificateRequests;
    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/CertificateImageGetRequest/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "CertificateImageGetRequest")]
    [ComVisible(false)]
    public class ProxyCertificateImageGetRequest : ProxyBaseRequest
    {
        /// <remarks/>
        public string CompanyCode;

        /// <remarks/>
        public string AvaCertId;

        /// <remarks/>
        public ProxyFormatType Format;

        /// <remarks/>
        public int PageNumber;
    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/CertificateImageGetResult/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "CertificateImageGetResult")]
    [ComVisible(false)]
    public class ProxyCertificateImageGetResult : ProxyBaseResult
    {
        /// <remarks/>
        public string AvaCertId;

        /// <remarks/>
        public byte[] Image;
    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/Certificate/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "Certificate")]
    [ComVisible(false)]
    public class ProxyCertificate
    {
        /// <remarks/>
        public string AvaCertId;

        /// <remarks/>
        public ProxyCertificateJurisdiction[] CertificateJurisdictions;

        /// <remarks/>
        public string[] CustomerCodes;

        /// <remarks/>
        public string SourceLocationName;

        /// <remarks/>
        public string SourceLocationCode;

        /// <remarks/>
        public ProxyCertificateStatus CertificateStatus;

        /// <remarks/>
        public ProxyReviewStatus ReviewStatus;

        /// <remarks/>
        public string RejectionReasonCode;

        /// <remarks/>
        public string RejectionReasonDetailCode;

        /// <remarks/>
        public string RejectionReasonCustomText;

        /// <remarks/>
        public DateTime CreatedDate;

        /// <remarks/>
        public DateTime LastModifyDate;

        /// <remarks/>
        public DateTime DocReceivedDate;

        /// <remarks/>
        public string BusinessName;

        /// <remarks/>
        public string Address1;

        /// <remarks/>
        public string Address2;

        /// <remarks/>
        public string City;

        /// <remarks/>
        public string State;
        
        /// <remarks/>
        public string Country;

        /// <remarks/>
        public string Zip;

        /// <remarks/>
        public string Phone;

        /// <remarks/>
        public string Email;

        /// <remarks/>
        public string SignerName;

        /// <remarks/>
        public string SignerTitle;

        /// <remarks/>
        public DateTime SignedDate;

        /// <remarks/>
        public string BusinessDescription;

        /// <remarks/>
        public string SellerPropertyDescription;

        /// <remarks/>
        public ProxyCertificateUsage CertificateUsage;

        /// <remarks/>
        public bool IsPartialExemption;

        /// <remarks/>
        public string ExemptReasonCode;

        /// <remarks/>
        public string ExemptFormName;

        /// <remarks/>
        public string Custom1;

        /// <remarks/>
        public string Custom2;

        /// <remarks/>
        public string Custom3;

        /// <remarks/>
        public int PageCount;
    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/CertificateJurisdiction/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "CertificateJurisdiction")]
    [ComVisible(false)]
    public class ProxyCertificateJurisdiction
    {
        /// <remarks/>
        public string Jurisdiction;

        /// <remarks/>
        public string Country;

        /// <remarks/>
        public DateTime ExpiryDate;

        /// <remarks/>
        public bool DoesNotExpire;

        /// <remarks/>
        public string[] PermitNumbers;
    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/CertificateRequest/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "CertificateRequest")]
    [ComVisible(false)]
    public class ProxyCertificateRequest
    {
        /// <remarks/>
        public string RequestId;

        /// <remarks/>
        public string TrackingCode;

        /// <remarks/>
        public string SourceLocationCode;

        /// <remarks/>
        public DateTime RequestDate;

        /// <remarks/>
        public string CustomerCode;

        /// <remarks/>
        public string CreatorName;

        /// <remarks/>
        public DateTime LastModifyDate;

        /// <remarks/>
        public ProxyCertificateRequestStatus RequestStatus;

        /// <remarks/>
        public ProxyCertificateRequestStage RequestStage;

        /// <remarks/>
        public ProxyCommunicationMode CommunicationMode;
    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/Customer/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "Customer")]
    [ComVisible(false)]
    public class ProxyCustomer
    {
        /// <remarks/>
        public string CustomerCode;

        /// <remarks/>
        public string NewCustomerCode;

        /// <remarks/>
        public string ParentCustomerCode;

        /// <remarks/>
        public string Type;

        /// <remarks/>
        public string BusinessName;

        /// <remarks/>
        public string Attn;

        /// <remarks/>
        public string Address1;

        /// <remarks/>
        public string Address2;

        /// <remarks/>
        public string City;

        /// <remarks/>
        public string State;

        /// <remarks/>
        public string Country;

        /// <remarks/>
        public string Zip;

        /// <remarks/>
        public string Phone;

        /// <remarks/>
        public string Fax;

        /// <remarks/>
        public string Email;
    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/ReviewStatus/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "ReviewStatus")]
    [ComVisible(false)]
    public enum ProxyReviewStatus
    {
        /// <remarks/>
        PENDING = 0,

        /// <remarks/>
        ACCEPTED = 1,

        /// <remarks/>
        REJECTED = 2
    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/CertificateRequestStatus/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "CertificateRequestStatus")]
    [ComVisible(false)]
    public enum ProxyCertificateRequestStatus
    {
        /// <remarks/>
        ALL = 0,

        /// <remarks/>
        OPEN = 1,

        /// <remarks/>
        CLOSED = 2
    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/CertificateRequestStage/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "CertificateRequestStage")]
    [ComVisible(false)]
    public enum ProxyCertificateRequestStage
    {
        /// <remarks/>
        REQUESTINITIATED = 0,

        /// <remarks/>
        CUSTOMERRESPONDED = 1,

        /// <remarks/>
        CERTIFICATERECEIVED = 2
    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/FormatType/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "FormatType")]
    [ComVisible(false)]
    public enum ProxyFormatType
    {
        /// <remarks/>
        NULL = 0,

        /// <remarks/>
        PNG = 1,

        /// <remarks/>
        PDF = 2
    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/RequestType/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "RequestType")]
    [ComVisible(false)]
    public enum ProxyRequestType
    {
        /// <remarks/>
        STANDARD = 0,

        /// <remarks/>
        DIRECT = 1
    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/CertificateStatus/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "CertificateStatus")]
    [ComVisible(false)]
    public enum ProxyCertificateStatus
    {
        /// <remarks/>
        ACTIVE = 0,

        /// <remarks/>
        VOID = 1,

        /// <remarks/>
        INCOMPLETE = 2
    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/CertificateUsage/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "CertificateUsage")]
    [ComVisible(false)]
    public enum ProxyCertificateUsage
    {
        /// <remarks/>
        BLANKET = 0,

        /// <remarks/>
        SINGLE = 1,

        /// <remarks/>
        NULL = 2
    }

    /// <include file='AvaCert2SvcProxy.Doc.xml' path='adapter/proxy/CommunicationMode/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "CommunicationMode")]
    [ComVisible(false)]
    public enum ProxyCommunicationMode
    {
        /// <remarks/>
        NULL = 0,

        /// <remarks/>
        EMAIL = 1,

        /// <remarks/>
        MAIL = 2,

        /// <remarks/>
        FAX = 3
    }
}
