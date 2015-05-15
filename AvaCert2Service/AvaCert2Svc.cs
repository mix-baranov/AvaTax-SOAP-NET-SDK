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
using Avalara.AvaTax.Adapter.Proxies;
using Avalara.AvaTax.Adapter.Proxies.AvaCert2SvcProxy;

namespace Avalara.AvaTax.Adapter.AvaCert2Service
{
    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/AvaCert2Svc/class/*' />
    [Guid("704383D9-6960-4943-B8D1-763D2EBFCBA1")]
    [ComVisible(true)]
    public class AvaCert2Svc : BaseSvc
    {
        private const string SERVICE_NAME = "AvaCert2/AvaCert2Svc.asmx";
        private AvaLogger _avaLog;

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/common/members[@name="Constructor"]/*' />
        public AvaCert2Svc()
        {
            try
            {
                _avaLog = AvaLogger.GetLogger();
                _avaLog.Debug(string.Format("Instantiating AvaCert2Svc: {0}", UniqueId));
                ServiceName = SERVICE_NAME;
                ProxyAvaCert2Svc proxy = new ProxyAvaCert2Svc();
                proxy.ProfileValue = new ProxyProfile();
                proxy.Security = new ProxySecurity();
                base.SvcProxy = proxy;

            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex);
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/common/members[@name="Ping"]/*' />
        [DispId(30)]
        public PingResult Ping(string message)
        {
            try
            {
                _avaLog.Debug("AvaCert2Svc.Ping");
                ProxyPingResult svcResult = (ProxyPingResult)InvokeService(typeof(ProxyAvaCert2Svc), MethodBase.GetCurrentMethod().Name, new object[] { message });

                _avaLog.Debug("Copying result from proxy object");
                PingResult localResult = new PingResult();
                localResult.CopyFrom(svcResult);					//load local object with service results

                return localResult;
            }
            catch (Exception ex)
            {
                return PingResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/common/members[@name="IsAuthorized"]/*' />
        [DispId(31)]
        public IsAuthorizedResult IsAuthorized(string operations)
        {
            try
            {
                _avaLog.Debug("AvaCert2Svc.IsAuthorized");

                ProxyIsAuthorizedResult svcResult = (ProxyIsAuthorizedResult)InvokeService(typeof(ProxyAvaCert2Svc), MethodBase.GetCurrentMethod().Name, new object[] { operations });

                _avaLog.Debug("Copying result from proxy object");
                IsAuthorizedResult localResult = new IsAuthorizedResult();	//local copy to hold service results
                localResult.CopyFrom(svcResult);	//load local object with service results

                return localResult;
            }
            catch (Exception ex)
            {
                return IsAuthorizedResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/AvaCert2Svc/members[@name="CustomerSave"]/*' />
        [DispId(32)]
        public CustomerSaveResult CustomerSave(CustomerSaveRequest customerSaveRequest)
        {
            try
            {
                _avaLog.Debug("AvaCert2Svc.CustomerSave");
                _avaLog.Debug("Validate request");
                Utilities.VerifyRequestObject(customerSaveRequest);

                _avaLog.Debug("Copying customer into proxy object");
                ProxyCustomerSaveRequest proxyRequest = new ProxyCustomerSaveRequest();
                customerSaveRequest.CopyTo(proxyRequest);

                ProxyCustomerSaveResult svcResult = (ProxyCustomerSaveResult)InvokeService(typeof(ProxyAvaCert2Svc), MethodBase.GetCurrentMethod().Name, new object[] { proxyRequest });

                _avaLog.Debug("Copying result from proxy object");

                CustomerSaveResult localResult = new CustomerSaveResult();
                localResult.CopyFrom(svcResult);

                return localResult;
            }
            catch (Exception ex)
            {
                return CustomerSaveResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/AvaCert2Svc/members[@name="CertificateRequestInitiate"]/*' />
        [DispId(33)]
        public CertificateRequestInitiateResult CertificateRequestInitiate(CertificateRequestInitiateRequest certificateRequestInitiateRequest)
        {
            try
            {
                _avaLog.Debug("AvaCert2Svc.CertificateRequestInitiate");
                _avaLog.Debug("Validate request");
                Utilities.VerifyRequestObject(certificateRequestInitiateRequest);

                _avaLog.Debug("Copying certificate into proxy object");
                ProxyCertificateRequestInitiateRequest proxyRequest = new ProxyCertificateRequestInitiateRequest();
                certificateRequestInitiateRequest.CopyTo(proxyRequest);

                ProxyCertificateRequestInitiateResult svcResult = (ProxyCertificateRequestInitiateResult)InvokeService(typeof(ProxyAvaCert2Svc), MethodBase.GetCurrentMethod().Name, new object[] { proxyRequest });


                _avaLog.Debug("Copying result from proxy object");
                CertificateRequestInitiateResult localResult = new CertificateRequestInitiateResult();
                localResult.CopyFrom(svcResult);

                return localResult;
            }
            catch (Exception ex)
            {
                return CertificateRequestInitiateResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/AvaCert2Svc/members[@name="CertificateGet"]/*' />
        [DispId(34)]
        public CertificateGetResult CertificateGet(CertificateGetRequest certificateGetRequest)
        {
            try
            {
                _avaLog.Debug("AvaCert2Svc.CertificateGet");
                _avaLog.Debug("Validate request");
                Utilities.VerifyRequestObject(certificateGetRequest);

                _avaLog.Debug("Copying Certificates into proxy object");
                ProxyCertificateGetRequest proxyRequest = new ProxyCertificateGetRequest();
                certificateGetRequest.CopyTo(proxyRequest);

                ProxyCertificateGetResult svcResult = (ProxyCertificateGetResult)InvokeService(typeof(ProxyAvaCert2Svc), MethodBase.GetCurrentMethod().Name, new object[] { proxyRequest });

                _avaLog.Debug("Copying result from proxy object");

                CertificateGetResult localResult = new CertificateGetResult();
                localResult.CopyFrom(svcResult);

                return localResult;
            }
            catch (Exception ex)
            {
                return CertificateGetResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/AvaCert2Svc/members[@name="CertificateRequestGet"]/*' />
        [DispId(35)]
        public CertificateRequestGetResult CertificateRequestGet(CertificateRequestGetRequest certificateRequestGetRequest)
        {
            try
            {
                _avaLog.Debug("AvaCert2Svc.CertificateRequestGet");
                _avaLog.Debug("Validate request");
                Utilities.VerifyRequestObject(certificateRequestGetRequest);

                _avaLog.Debug("Copying Certificates into proxy object");
                ProxyCertificateRequestGetRequest proxyRequest = new ProxyCertificateRequestGetRequest();
                certificateRequestGetRequest.CopyTo(proxyRequest);

                ProxyCertificateRequestGetResult svcResult = (ProxyCertificateRequestGetResult)InvokeService(typeof(ProxyAvaCert2Svc), MethodBase.GetCurrentMethod().Name, new object[] { proxyRequest });

                _avaLog.Debug("Copying result from proxy object");

                CertificateRequestGetResult localResult = new CertificateRequestGetResult();
                localResult.CopyFrom(svcResult);

                return localResult;
            }
            catch (Exception ex)
            {
                return CertificateRequestGetResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/AvaCert2Svc/members[@name="CertificateImageGet"]/*' />
        [DispId(36)]
        public CertificateImageGetResult CertificateImageGet(CertificateImageGetRequest certificateImageGetRequest)
        {
            try
            {
                _avaLog.Debug("AvaCert2Svc.CertificateImageGet");
                _avaLog.Debug("Validate request");
                Utilities.VerifyRequestObject(certificateImageGetRequest);

                _avaLog.Debug("Copying Certificates into proxy object");
                ProxyCertificateImageGetRequest proxyRequest = new ProxyCertificateImageGetRequest();
                certificateImageGetRequest.CopyTo(proxyRequest);

                ProxyCertificateImageGetResult svcResult = (ProxyCertificateImageGetResult)InvokeService(typeof(ProxyAvaCert2Svc), MethodBase.GetCurrentMethod().Name, new object[] { proxyRequest });

                _avaLog.Debug("Copying result from proxy object");

                CertificateImageGetResult localResult = new CertificateImageGetResult();
                localResult.CopyFrom(svcResult);

                return localResult;
            }
            catch (Exception ex)
            {
                return CertificateImageGetResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/common/members[@name="Finalize"]/*' />
        ~AvaCert2Svc()
		{
			if(_avaLog != null)
                _avaLog.Debug(string.Format("Finalizing AvaCert2Svc: {0}", UniqueId));
		}

		#region Internal Members

		/// <summary>
		/// Convenient wrapper property that casts the base WebServicesClientProtocol into our strongly typed proxy class.
		/// </summary>
        internal new ProxyAvaCert2Svc SvcProxy
		{
			get
			{
				return (ProxyAvaCert2Svc)base.SvcProxy;
			}
		}
		#endregion

    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Customer/class/*' />
    [Guid("1EB3A650-9E5F-476e-BCA3-62E8FDDD38F7")]
    [ComVisible(true)]
    public class Customer
    {
        string _customerCode;
        string _newCustomerCode;
        string _parentCustomerCode;
        string _type;
        string _businessName;
        string _attn;
        string _address1;
        string _address2;
        string _city;
        string _state;
        string _country;
        string _zip;
        string _phone;
        string _fax;
        string _email;

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Customer/members[@name="CustomerCode"]/*' />
        [DispId(20)]
        public string CustomerCode
        {
            get
            {
                return _customerCode;
            }
            set
            {
                _customerCode = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Customer/members[@name="NewCustomerCode"]/*' />
        [DispId(21)]
        public string NewCustomerCode
        {
            get
            {
                return _newCustomerCode;
            }
            set
            {
                _newCustomerCode = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Customer/members[@name="ParentCustomerCode"]/*' />
        [DispId(22)]
        public string ParentCustomerCode
        {
            get
            {
                return _parentCustomerCode;
            }
            set
            {
                _parentCustomerCode = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Customer/members[@name="Type"]/*' />
        [DispId(23)]
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Customer/members[@name="BusinessName"]/*' />
        [DispId(24)]
        public string BusinessName
        {
            get
            {
                return _businessName;
            }
            set
            {
                _businessName = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Customer/members[@name="Attn"]/*' />
        [DispId(25)]
        public string Attn
        {
            get
            {
                return _attn;
            }
            set
            {
                _attn = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Customer/members[@name="Address1"]/*' />
        [DispId(26)]
        public string Address1
        {
            get
            {
                return _address1;
            }
            set
            {
                _address1 = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Customer/members[@name="Address2"]/*' />
        [DispId(27)]
        public string Address2
        {
            get
            {
                return _address2;
            }
            set
            {
                _address2 = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Customer/members[@name="City"]/*' />
        [DispId(28)]
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Customer/members[@name="State"]/*' />
        [DispId(29)]
        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Customer/members[@name="Country"]/*' />
        [DispId(30)]
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Customer/members[@name="Zip"]/*' />
        [DispId(31)]
        public string Zip
        {
            get
            {
                return _zip;
            }
            set
            {
                _zip = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Customer/members[@name="Phone"]/*' />
        [DispId(32)]
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Customer/members[@name="Fax"]/*' />
        [DispId(33)]
        public string Fax
        {
            get
            {
                return _fax;
            }
            set
            {
                _fax = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Customer/members[@name="Email"]/*' />
        [DispId(34)]
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        #region Internal Members

        /// <summary>
        /// Load an empty local Customer object from the data provided by the web service.
        /// </summary>
        /// <param name="SvcCustomer">The Customer object provided by the web service.</param>
        internal void CopyFrom(ProxyCustomer SvcCustomer)
        {
            _customerCode = SvcCustomer.CustomerCode;
            _newCustomerCode = SvcCustomer.NewCustomerCode;
            _parentCustomerCode = SvcCustomer.ParentCustomerCode;
            _type = SvcCustomer.Type;
            _businessName = SvcCustomer.BusinessName;
            _attn = SvcCustomer.Attn;
            _address1 = SvcCustomer.Address1;
            _address2 = SvcCustomer.Address2;
            _city = SvcCustomer.City;
            _state = SvcCustomer.State;
            _zip = SvcCustomer.Zip;
            _country = SvcCustomer.Country;
            _phone = SvcCustomer.Phone;
            _fax = SvcCustomer.Fax;
            _email = SvcCustomer.Email;
        }

        /// <summary>
        /// Loads a local Customer object into a web service copy of the same object.
        /// </summary>
        /// <param name="SvcCustomer">The Customer object to be copied to.</param>
        internal void CopyTo(ProxyCustomer SvcCustomer)
        {
            SvcCustomer.CustomerCode = CustomerCode;
            SvcCustomer.NewCustomerCode = NewCustomerCode;
            SvcCustomer.ParentCustomerCode = ParentCustomerCode;
            SvcCustomer.Type = Type;
            SvcCustomer.BusinessName = BusinessName;
            SvcCustomer.Attn = Attn;
            SvcCustomer.Address1 = Address1;
            SvcCustomer.Address2 = Address2;
            SvcCustomer.City = City;
            SvcCustomer.State = State;
            SvcCustomer.Zip = Zip;
            SvcCustomer.Country = Country;
            SvcCustomer.Phone = Phone;
            SvcCustomer.Fax = Fax;
            SvcCustomer.Email = Email;
        }

        #endregion

    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CustomerSaveRequest/class/*' />
    [Guid("1F43CFD0-1B83-4bf7-85CF-D2D70D7BF1F6")]
    [ComVisible(true)]
    public class CustomerSaveRequest : BaseRequest
    {
        private string _companyCode;
        private Customer _customer;

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CustomerSaveRequest/members[@name="CompanyCode"]/*' />
        [DispId(20)]
        public string CompanyCode
        {
            get
            {
                return _companyCode;
            }
            set
            {
                _companyCode = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CustomerSaveRequest/members[@name="Customer"]/*' />
        [DispId(21)]
        public Customer Customer
        {
            get
            {
                return _customer;
            }
            set
            {
                _customer = value;
            }
        }

        #region Internal Members

        /// <summary>
        /// Loads a local CustomerSaveRequest object into a web service copy of the same object.
        /// </summary>
        /// <param name="SvcRequest">The CustomerSaveRequest object to be copied to.</param>
        internal void CopyTo(ProxyCustomerSaveRequest SvcRequest)
        {
            ProxyCustomer proxyCustomer = new ProxyCustomer();
            Customer.CopyTo(proxyCustomer);
            SvcRequest.Customer = proxyCustomer;
            SvcRequest.CompanyCode = CompanyCode;
        }

        /// <summary>
        /// Is this object a valid request that can be sent to the service?
        /// </summary>
        /// <returns>
        /// Returns true if the minimum data requirement is satisfied,
        /// false if the object is empty.
        /// </returns>
        internal override bool IsValid(out string message)
        {
            message = string.Empty;
            return true;
        }

        #endregion

    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CustomerSaveResult/class/*' />
    [Guid("400FBC87-3717-4a7f-8F0F-7014FE6518C9")]
    [ComVisible(true)]
    public class CustomerSaveResult : BaseResult
    {
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal CustomerSaveResult()
		{
        }

        #region Internal Members

        /// <summary>
        /// Load an empty local CustomerSaveResult object from the data provided by the web service.
        /// </summary>
        /// <param name="SvcResult">The CustomerSaveResult object provided by the web service.</param>
        internal void CopyFrom(ProxyCustomerSaveResult SvcResult)
        {
            base.CopyFrom(SvcResult);
        }

        /// <summary>
        /// Creates a new CustomerSaveResult based on a <see cref="BaseResult"/>.
        /// </summary>
        /// <param name="baseResult"></param>
        /// <returns></returns>
        internal static CustomerSaveResult CastFromBaseResult(BaseResult baseResult)
        {
            CustomerSaveResult result = new CustomerSaveResult();
            result.CopyFrom(baseResult);
            return result;
        }

        #endregion

    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestInitiateRequest/class/*' />
    [Guid("20082567-A5DC-4c4a-9272-6861EFEA7337")]
    [ComVisible(true)]
    public class CertificateRequestInitiateRequest : BaseRequest
    {
        string _companyCode;
        string _customerCode;
        CommunicationMode _communicationMode;
        string _sourceLocationCode;
        RequestType _type;
        string _customMessage;
        string _letterTemplate;
        bool _includeCoverPage;
        string _closeReason;
        string _requestId;

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestInitiateRequest/members[@name="CompanyCode"]/*' />
        [DispId(20)]
        public string CompanyCode
        {
            get
            {
                return _companyCode;
            }
            set
            {
                _companyCode = value;
            }
        }
        
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestInitiateRequest/members[@name="CustomerCode"]/*' />
        [DispId(21)]
        public string CustomerCode
        {
            get
            {
                return _customerCode;
            }
            set
            {
                _customerCode = value;
            }
        }
        
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestInitiateRequest/members[@name="CommunicationMode"]/*' />
        [DispId(22)]
        public CommunicationMode CommunicationMode
        {
            get
            {
                return _communicationMode;
            }
            set
            {
                _communicationMode = value;
            }
        }
        
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestInitiateRequest/members[@name="SourceLocationCode"]/*' />
        [DispId(23)]
        public string SourceLocationCode
        {
            get
            {
                return _sourceLocationCode;
            }
            set
            {
                _sourceLocationCode = value;
            }
        }
        
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestInitiateRequest/members[@name="Type"]/*' />
        [DispId(24)]
        public RequestType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }
        
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestInitiateRequest/members[@name="CustomMessage"]/*' />
        [DispId(25)]
        public string CustomMessage
        {
            get
            {
                return _customMessage;
            }
            set
            {
                _customMessage = value;
            }
        }
        
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestInitiateRequest/members[@name="LetterTemplate"]/*' />
        [DispId(26)]
        public string LetterTemplate
        {
            get
            {
                return _letterTemplate;
            }
            set
            {
                _letterTemplate = value;
            }
        }
        
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestInitiateRequest/members[@name="IncludeCoverPage"]/*' />
        [DispId(27)]
        public bool IncludeCoverPage
        {
            get
            {
                return _includeCoverPage;
            }
            set
            {
                _includeCoverPage = value;
            }
        }
        
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestInitiateRequest/members[@name="CloseReason"]/*' />
        [DispId(28)]
        public string CloseReason
        {
            get
            {
                return _closeReason;
            }
            set
            {
                _closeReason = value;
            }
        }
        
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestInitiateRequest/members[@name="RequestId"]/*' />
        [DispId(29)]
        public string RequestId
        {
            get
            {
                return _requestId;
            }
            set
            {
                _requestId = value;
            }
        }

        #region Internal Members

        /// <summary>
        /// Loads a local CertificateRequestInitiateRequest object into a web service copy of the same object.
        /// </summary>
        /// <param name="SvcRequest">The CertificateRequestInitiateRequest object to be copied to.</param>
        internal void CopyTo(ProxyCertificateRequestInitiateRequest SvcRequest)
        {
            SvcRequest.CompanyCode = CompanyCode;
            SvcRequest.CustomerCode = CustomerCode;
            SvcRequest.CommunicationMode = (ProxyCommunicationMode)CommunicationMode;
            SvcRequest.SourceLocationCode = SourceLocationCode;
            SvcRequest.Type = (ProxyRequestType)Type;
            SvcRequest.CustomMessage = CustomMessage;
            SvcRequest.LetterTemplate = LetterTemplate;
            SvcRequest.IncludeCoverPage = IncludeCoverPage;
            SvcRequest.CloseReason = CloseReason;
            SvcRequest.RequestId = RequestId;
        }

        /// <summary>
        /// Is this object a valid request that can be sent to the service?
        /// </summary>
        /// <returns>
        /// Returns true if the minimum data requirement is satisfied,
        /// false if the object is empty.
        /// </returns>
        internal override bool IsValid(out string message)
        {
            message = string.Empty;
            return true;
        }

        #endregion

    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestInitiateResult/class/*' />
    [Guid("75D64A04-04FA-4677-9160-479AC6B653A2")]
    [ComVisible(true)]
    public class CertificateRequestInitiateResult : BaseResult
    {
        string _trackingCode;
        string _wizardLaunchUrl;
        string _requestId;
        string _customerCode;

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal CertificateRequestInitiateResult()
		{
		}

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestInitiateResult/members[@name="TrackingCode"]/*' />
        [DispId(30)]
        public string TrackingCode
        {
            get
            {
                return _trackingCode;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestInitiateResult/members[@name="WizardLaunchUrl"]/*' />
        [DispId(31)]
        public string WizardLaunchUrl
        {
            get
            {
                return _wizardLaunchUrl;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestInitiateResult/members[@name="RequestId"]/*' />
        [DispId(32)]
        public string RequestId
        {
            get
            {
                return _requestId;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestInitiateResult/members[@name="CustomerCode"]/*' />
        [DispId(33)]
        public string CustomerCode
        {
            get
            {
                return _customerCode;
            }
        }

        #region Internal Members

        /// <summary>
        /// Load an empty local CertificateRequestInitiate object from the data provided by the web service.
        /// </summary>
        /// <param name="SvcResult">The CertificateRequestInitiateResult object provided by the web service.</param>
        internal void CopyFrom(ProxyCertificateRequestInitiateResult SvcResult)
        {
            base.CopyFrom(SvcResult);
            _trackingCode = SvcResult.TrackingCode;
            _wizardLaunchUrl = SvcResult.WizardLaunchUrl;
            _requestId = SvcResult.RequestId;
            _customerCode = SvcResult.CustomerCode;
        }

        /// <summary>
        /// Creates a new CertificateRequestInitiateResult based on a <see cref="BaseResult"/>.
        /// </summary>
        /// <param name="baseResult"></param>
        /// <returns></returns>
        internal static CertificateRequestInitiateResult CastFromBaseResult(BaseResult baseResult)
        {
            CertificateRequestInitiateResult result = new CertificateRequestInitiateResult();
            result.CopyFrom(baseResult);
            return result;
        }

        #endregion
    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateGetRequest/class/*' />
    [Guid("81413722-97C5-42c6-910C-472061E6B9E6")]
    [ComVisible(true)]
    public class CertificateGetRequest : BaseRequest
    {
        string _companyCode;
        string _customerCode;
        DateTime _modFromDate;
        DateTime _modToDate;

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateGetRequest/members[@name="CompanyCode"]/*' />
        [DispId(20)]
        public string CompanyCode
        {
            get
            {
                return _companyCode;
            }
            set
            {
                _companyCode = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateGetRequest/members[@name="CustomerCode"]/*' />
        [DispId(21)]
        public string CustomerCode
        {
            get
            {
                return _customerCode;
            }
            set
            {
                _customerCode = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateGetRequest/members[@name="ModFromDate"]/*' />
        [DispId(22)]
        public DateTime ModFromDate
        {
            get
            {
                return _modFromDate;
            }
            set
            {
                _modFromDate = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateGetRequest/members[@name="ModToDate"]/*' />
        [DispId(23)]
        public DateTime ModToDate
        {
            get
            {
                return _modToDate;
            }
            set
            {
                _modToDate = value;
            }
        }

        #region Internal Members

        /// <summary>
        /// Loads a local CertificateGetRequest object into a web service copy of the same object.
        /// </summary>
        /// <param name="SvcRequest">The CertificateGetRequest object to be copied to.</param>
        internal void CopyTo(ProxyCertificateGetRequest SvcRequest)
        {
            SvcRequest.CompanyCode = CompanyCode;
            SvcRequest.CustomerCode = CustomerCode;
            SvcRequest.ModFromDate = ModFromDate;
            SvcRequest.ModToDate = ModToDate;
        }

        /// <summary>
        /// Is this object a valid request that can be sent to the service?
        /// </summary>
        /// <returns>
        /// Returns true if the minimum data requirement is satisfied,
        /// false if the object is empty.
        /// </returns>
        internal override bool IsValid(out string message)
        {
            message = string.Empty;
            return true;
        }

        #endregion

    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateGetResult/class/*' />
    [Guid("076D26B4-72DB-47d2-B4B3-B062C21A1A62")]
    [ComVisible(true)]
    public class CertificateGetResult : BaseResult
    {
        Certificates _certificates = new Certificates();

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal CertificateGetResult()
		{
		}

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateGetResult/members[@name="Certificates"]/*' />
        [DispId(30)]
        public Certificates Certificates
        {
            get
            {
                return _certificates;
            }
        }

        #region Internal Members

        /// <summary>
        /// Load an empty local CertificateGet object from the data provided by the web service.
        /// </summary>
        /// <param name="SvcResult">The CertificateGetResult object provided by the web service.</param>
        internal void CopyFrom(ProxyCertificateGetResult SvcResult)
        {
            base.CopyFrom(SvcResult);

            if (SvcResult.Certificates != null && SvcResult.Certificates.Length > 0)
            {
                for (int idx = 0; idx < SvcResult.Certificates.Length; idx++)
                {
                    Certificate certificate = new Certificate();
                    certificate.CopyFrom(SvcResult.Certificates[idx]);
                    _certificates.Add(certificate);
                }
            }
        }

        /// <summary>
        /// Creates a new CertificateGetResult based on a <see cref="BaseResult"/>.
        /// </summary>
        /// <param name="baseResult"></param>
        /// <returns></returns>
        internal static CertificateGetResult CastFromBaseResult(BaseResult baseResult)
        {
            CertificateGetResult result = new CertificateGetResult();
            result.CopyFrom(baseResult);
            return result;
        }

        #endregion
    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestGetRequest/class/*' />
    [Guid("04540C2F-6E8B-4005-B86B-62E045A21B44")]
    [ComVisible(true)]
    public class CertificateRequestGetRequest : BaseRequest
    {
        string _companyCode;
        string _customerCode;
        CertificateRequestStatus _requestStatus;
        DateTime _modFromDate;
        DateTime _modToDate;

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestGetRequest/members[@name="CompanyCode"]/*' />
        [DispId(20)]
        public string CompanyCode
        {
            get
            {
                return _companyCode;
            }
            set
            {
                _companyCode = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestGetRequest/members[@name="CustomerCode"]/*' />
        [DispId(21)]
        public string CustomerCode
        {
            get
            {
                return _customerCode;
            }
            set
            {
                _customerCode = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestGetRequest/members[@name="RequestStatus"]/*' />
        [DispId(22)]
        public CertificateRequestStatus RequestStatus
        {
            get
            {
                return _requestStatus;
            }
            set
            {
                _requestStatus = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestGetRequest/members[@name="ModFromDate"]/*' />
        [DispId(23)]
        public DateTime ModFromDate
        {
            get
            {
                return _modFromDate;
            }
            set
            {
                _modFromDate = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestGetRequest/members[@name="ModToDate"]/*' />
        [DispId(24)]
        public DateTime ModToDate
        {
            get
            {
                return _modToDate;
            }
            set
            {
                _modToDate = value;
            }
        }

        #region Internal Members

        /// <summary>
        /// Loads a local CertificateRequestGetRequest object into a web service copy of the same object.
        /// </summary>
        /// <param name="SvcRequest">The CertificateRequestGetRequest object to be copied to.</param>
        internal void CopyTo(ProxyCertificateRequestGetRequest SvcRequest)
        {
            SvcRequest.CompanyCode = CompanyCode;
            SvcRequest.CustomerCode = CustomerCode;
            SvcRequest.RequestStatus = (ProxyCertificateRequestStatus)RequestStatus;
            SvcRequest.ModFromDate = ModFromDate;
            SvcRequest.ModToDate = ModToDate;
        }

        /// <summary>
        /// Is this object a valid request that can be sent to the service?
        /// </summary>
        /// <returns>
        /// Returns true if the minimum data requirement is satisfied,
        /// false if the object is empty.
        /// </returns>
        internal override bool IsValid(out string message)
        {
            message = string.Empty;
            return true;
        }

        #endregion

    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestGetResult/class/*' />
    [Guid("21B12209-7C8D-4564-A4EC-9589B2AA8228")]
    [ComVisible(true)]
    public class CertificateRequestGetResult : BaseResult
    {
        CertificateRequests _certificateRequests = new CertificateRequests();

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal CertificateRequestGetResult()
        {
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestGetResult/members[@name="CertificateRequests"]/*' />
        [DispId(30)]
        public CertificateRequests CertificateRequests
        {
            get
            {
                return _certificateRequests;
            }
        }

        #region Internal Members

        /// <summary>
        /// Load an empty local CertificateRequestGet object from the data provided by the web service.
        /// </summary>
        /// <param name="SvcResult">The CertificateRequestGetResult object provided by the web service.</param>
        internal void CopyFrom(ProxyCertificateRequestGetResult SvcResult)
        {
            base.CopyFrom(SvcResult);

            if (SvcResult.CertificateRequests != null && SvcResult.CertificateRequests.Length > 0)
            {
                for (int idx = 0; idx < SvcResult.CertificateRequests.Length; idx++)
                {
                    CertificateRequest certificateRequest = new CertificateRequest();
                    certificateRequest.CopyFrom(SvcResult.CertificateRequests[idx]);
                    _certificateRequests.Add(certificateRequest);
                }
            }
        }

        /// <summary>
        /// Creates a new CertificateRequestGetResult based on a <see cref="BaseResult"/>.
        /// </summary>
        /// <param name="baseResult"></param>
        /// <returns></returns>
        internal static CertificateRequestGetResult CastFromBaseResult(BaseResult baseResult)
        {
            CertificateRequestGetResult result = new CertificateRequestGetResult();
            result.CopyFrom(baseResult);
            return result;
        }

        #endregion
    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateImageGetRequest/class/*' />
    [Guid("5B118363-7FE4-4783-8A90-ACEA6D395F66")]
    [ComVisible(true)]
    public class CertificateImageGetRequest : BaseRequest
    {
        string _companyCode;
        string _avaCertId;
        FormatType _format;
        int _pageNumber;

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateImageGetRequest/members[@name="CompanyCode"]/*' />
        [DispId(20)]
        public string CompanyCode
        {
            get
            {
                return _companyCode;
            }
            set
            {
                _companyCode = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateImageGetRequest/members[@name="AvaCertId"]/*' />
        [DispId(21)]
        public string AvaCertId
        {
            get
            {
                return _avaCertId;
            }
            set
            {
                _avaCertId = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateImageGetRequest/members[@name="Format"]/*' />
        [DispId(22)]
        public FormatType Format
        {
            get
            {
                return _format;
            }
            set
            {
                _format = value;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateImageGetRequest/members[@name="PageNumber"]/*' />
        [DispId(23)]
        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                _pageNumber = value;
            }
        }

        #region Internal Members

        /// <summary>
        /// Loads a local CertificateImageGetRequest object into a web service copy of the same object.
        /// </summary>
        /// <param name="SvcRequest">The CertificateImageGetRequest object to be copied to.</param>
        internal void CopyTo(ProxyCertificateImageGetRequest SvcRequest)
        {
            SvcRequest.CompanyCode = CompanyCode;
            SvcRequest.AvaCertId = AvaCertId;
            SvcRequest.Format = (ProxyFormatType)Format;
            SvcRequest.PageNumber = PageNumber;
        }

        /// <summary>
        /// Is this object a valid request that can be sent to the service?
        /// </summary>
        /// <returns>
        /// Returns true if the minimum data requirement is satisfied,
        /// false if the object is empty.
        /// </returns>
        internal override bool IsValid(out string message)
        {
            message = string.Empty;
            return true;
        }

        #endregion

    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateImageGetResult/class/*' />
    [Guid("EB372DC7-DF05-4dc5-89CD-EEE79DE09C14")]
    [ComVisible(true)]
    public class CertificateImageGetResult : BaseResult
    {
        string _avaCertId;
        byte[] _image;

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal CertificateImageGetResult()
        {
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateImageGetResult/members[@name="AvaCertId"]/*' />
        [DispId(30)]
        public string AvaCertId
        {
            get
            {
                return _avaCertId;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateImageGetResult/members[@name="Image"]/*' />
        [DispId(31)]
        public byte[] Image
        {
            get
            {
                return _image;
            }
        }

        #region Internal Members

        /// <summary>
        /// Load an empty local CertificateImageGet object from the data provided by the web service.
        /// </summary>
        /// <param name="SvcResult">The CertificateImageGetResult object provided by the web service.</param>
        internal void CopyFrom(ProxyCertificateImageGetResult SvcResult)
        {
            base.CopyFrom(SvcResult);
            _avaCertId = SvcResult.AvaCertId;
            _image = SvcResult.Image;
        }

        /// <summary>
        /// Creates a new CertificateImageGetResult based on a <see cref="BaseResult"/>.
        /// </summary>
        /// <param name="baseResult"></param>
        /// <returns></returns>
        internal static CertificateImageGetResult CastFromBaseResult(BaseResult baseResult)
        {
            CertificateImageGetResult result = new CertificateImageGetResult();
            result.CopyFrom(baseResult);
            return result;
        }

        #endregion
    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/class/*' />
    [Guid("7BD1FA25-8F3A-4905-B828-AE8BDF848AD8")]
    [ComVisible(true)]
    public class Certificate
    {
        string _avaCertId;
        CertificateJurisdictions _certificateJurisdictions = new CertificateJurisdictions();
        string[] _customerCodes;
        string _sourceLocationName;
        string _sourceLocationCode;
        CertificateStatus _certificateStatus;
        ReviewStatus _reviewStatus;
        string _rejectionReasonCode;
        string _rejectionReasonDetailCode;
        string _rejectionReasonCustomText;
        DateTime _createdDate;
        DateTime _lastModifyDate;
        DateTime _docReceivedDate;
        string _businessName;
        string _address1;
        string _address2;
        string _city;
        string _state;
        string _country;
        string _zip;
        string _phone;
        string _email;
        string _signerName;
        string _signerTitle;
        DateTime _signedDate;
        string _businessDescription;
        string _sellerPropertyDescription;
        CertificateUsage _certificateUsage;
        bool _isPartialExemption;
        string _exemptReasonCode;
        string _exemptFormName;
        string _custom1;
        string _custom2;
        string _custom3;
        int _pageCount;

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal Certificate()
        {
            _certificateUsage = CertificateUsage.NULL;
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="AvaCertId"]/*' />
        [DispId(20)]
        public string AvaCertId
        {
            get
            {
                return _avaCertId;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="CertificateJurisdictions"]/*' />
        [DispId(21)]
        public CertificateJurisdictions CertificateJurisdictions
        {
            get
            {
                return _certificateJurisdictions;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="CustomerCodes"]/*' />
        [DispId(22)]
        public string[] CustomerCodes
        {
            get
            {
                return _customerCodes;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="SourceLocationName"]/*' />
        [DispId(23)]
        public string SourceLocationName
        {
            get
            {
                return _sourceLocationName;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="SourceLocationCode"]/*' />
        [DispId(24)]
        public string SourceLocationCode
        {
            get
            {
                return _sourceLocationCode;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="CertificateStatus"]/*' />
        [DispId(25)]
        public CertificateStatus CertificateStatus
        {
            get
            {
                return _certificateStatus;
            }

        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="ReviewStatus"]/*' />
        [DispId(26)]
        public ReviewStatus ReviewStatus
        {
            get
            {
                return _reviewStatus;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="RejectionReasonCode"]/*' />
        [DispId(27)]
        public string RejectionReasonCode
        {
            get
            {
                return _rejectionReasonCode;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="RejectionReasonDetailCode"]/*' />
        [DispId(28)]
        public string RejectionReasonDetailCode
        {
            get
            {
                return _rejectionReasonDetailCode;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="RejectionReasonCustomText"]/*' />
        [DispId(29)]
        public string RejectionReasonCustomText
        {
            get
            {
                return _rejectionReasonCustomText;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="CreatedDate"]/*' />
        [DispId(30)]
        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="LastModifyDate"]/*' />
        [DispId(31)]
        public DateTime LastModifyDate
        {
            get
            {
                return _lastModifyDate;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="DocReceivedDate"]/*' />
        [DispId(32)]
        public DateTime DocReceivedDate
        {
            get
            {
                return _docReceivedDate;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="BusinessName"]/*' />
        [DispId(33)]
        public string BusinessName
        {
            get
            {
                return _businessName;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="Address1"]/*' />
        [DispId(34)]
        public string Address1
        {
            get
            {
                return _address1;
            }
        }
        
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="Address2"]/*' />
        [DispId(35)]
        public string Address2
        {
            get
            {
                return _address2;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="City"]/*' />
        [DispId(36)]
        public string City
        {
            get
            {
                return _city;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="State"]/*' />
        [DispId(37)]
        public string State
        {
            get
            {
                return _state;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="Country"]/*' />
        [DispId(38)]
        public string Country
        {
            get
            {
                return _country;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="Zip"]/*' />
        [DispId(39)]
        public string Zip
        {
            get
            {
                return _zip;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="Phone"]/*' />
        [DispId(40)]
        public string Phone
        {
            get
            {
                return _phone;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="Email"]/*' />
        [DispId(41)]
        public string Email
        {
            get
            {
                return _email;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="SignerName"]/*' />
        [DispId(42)]
        public string SignerName
        {
            get
            {
                return _signerName;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="SignerTitle"]/*' />
        [DispId(43)]
        public string SignerTitle
        {
            get
            {
                return _signerTitle;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="SignedDate"]/*' />
        [DispId(44)]
        public DateTime SignedDate
        {
            get
            {
                return _signedDate;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="BusinessDescription"]/*' />
        [DispId(45)]
        public string BusinessDescription
        {
            get
            {
                return _businessDescription;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="SellerPropertyDescription"]/*' />
        [DispId(46)]
        public string SellerPropertyDescription
        {
            get
            {
                return _sellerPropertyDescription;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="CertificateUsage"]/*' />
        [DispId(47)]
        public CertificateUsage CertificateUsage
        {
            get
            {
                return _certificateUsage;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="IsPartialExemption"]/*' />
        [DispId(48)]
        public bool IsPartialExemption
        {
            get
            {
                return _isPartialExemption;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="ExemptReasonCode"]/*' />
        [DispId(49)]
        public string ExemptReasonCode
        {
            get
            {
                return _exemptReasonCode;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="ExemptFormName"]/*' />
        [DispId(50)]
        public string ExemptFormName
        {
            get
            {
                return _exemptFormName;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="Custom1"]/*' />
        [DispId(51)]
        public string Custom1
        {
            get
            {
                return _custom1;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="Custom2"]/*' />
        [DispId(52)]
        public string Custom2
        {
            get
            {
                return _custom2;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="Custom3"]/*' />
        [DispId(53)]
        public string Custom3
        {
            get
            {
                return _custom3;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificate/members[@name="PageCount"]/*' />
        [DispId(54)]
        public int PageCount
        {
            get
            {
                return _pageCount;
            }
        }

        #region Internal Members

        /// <summary>
        /// Load an empty local Certificate object from the data provided by the web service.
        /// </summary>
        /// <param name="SvcCertificate">The Certificate object provided by the web service.</param>
        internal void CopyFrom(ProxyCertificate SvcCertificate)
        {
            _avaCertId = SvcCertificate.AvaCertId;

            for (int ii = 0; ii < SvcCertificate.CertificateJurisdictions.Length; ii++)
            {
                CertificateJurisdiction certificateJurisdiction = new CertificateJurisdiction();
                certificateJurisdiction.CopyFrom(SvcCertificate.CertificateJurisdictions[ii]);
                _certificateJurisdictions.Add(certificateJurisdiction);
            }
            _customerCodes = SvcCertificate.CustomerCodes;
            _sourceLocationName = SvcCertificate.SourceLocationName;
            _sourceLocationCode = SvcCertificate.SourceLocationCode;
            _certificateStatus = (CertificateStatus)SvcCertificate.CertificateStatus;
            _reviewStatus = (ReviewStatus)SvcCertificate.ReviewStatus;
            _rejectionReasonCode = SvcCertificate.RejectionReasonCode;
            _rejectionReasonDetailCode = SvcCertificate.RejectionReasonDetailCode;
            _rejectionReasonCustomText = SvcCertificate.RejectionReasonCustomText;
            _createdDate = SvcCertificate.CreatedDate;
            _lastModifyDate = SvcCertificate.LastModifyDate;
            _docReceivedDate = SvcCertificate.DocReceivedDate;
            _businessName = SvcCertificate.BusinessName;
            _address1 = SvcCertificate.Address1;
            _address2 = SvcCertificate.Address2;
            _city = SvcCertificate.City;
            _state = SvcCertificate.State;
            _country = SvcCertificate.Country;
            _zip = SvcCertificate.Zip;
            _phone = SvcCertificate.Phone;
            _email = SvcCertificate.Email;
            _signerName = SvcCertificate.SignerName;
            _signerTitle = SvcCertificate.SignerTitle;
            _signedDate = SvcCertificate.SignedDate;
            _businessDescription = SvcCertificate.BusinessDescription;
            _sellerPropertyDescription = SvcCertificate.SellerPropertyDescription;
            _certificateUsage = (CertificateUsage)SvcCertificate.CertificateUsage;
            _isPartialExemption = SvcCertificate.IsPartialExemption;
            _exemptReasonCode = SvcCertificate.ExemptReasonCode;
            _exemptFormName = SvcCertificate.ExemptFormName;
            _custom1 = SvcCertificate.Custom1;
            _custom2 = SvcCertificate.Custom2;
            _custom3 = SvcCertificate.Custom3;
            _pageCount = SvcCertificate.PageCount;
        }

        #endregion

    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/Certificates/class/*' />
    [Guid("29931F0B-BFC7-42c4-B55E-F28333C5DCAC")]
    [ComVisible(true)]
    public class Certificates : ReadOnlyArrayList
    {
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal Certificates() { }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/collection/members[@name="this"]/*' />
        [DispId(0)]
        public new Certificate this[int index]
        {
            get
            {
                return (Certificate)base[index];
            }
        }
    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequest/class/*' />
    [Guid("3D2B6603-396A-4fef-B2A3-E9DC23C51502")]
    [ComVisible(true)]
    public class CertificateRequest
    {
        string _requestId;
        string _trackingCode;
        string _sourceLocationCode;
        DateTime _requestDate;
        string _customerCode;
        string _creatorName;
        DateTime _lastModifyDate;
        CertificateRequestStatus _requestStatus;
        CertificateRequestStage _requestStage;
        CommunicationMode _communicationMode;

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal CertificateRequest()
        {
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequest/members[@name="RequestId"]/*' />
        [DispId(30)]
        public string RequestId
        {
            get
            {
                return _requestId;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequest/members[@name="TrackingCode"]/*' />
        [DispId(31)]
        public string TrackingCode
        {
            get
            {
                return _trackingCode;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequest/members[@name="SourceLocationCode"]/*' />
        [DispId(32)]
        public string SourceLocationCode
        {
            get
            {
                return _sourceLocationCode;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequest/members[@name="RequestDate"]/*' />
        [DispId(33)]
        public DateTime RequestDate
        {
            get
            {
                return _requestDate;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequest/members[@name="CustomerCode"]/*' />
        [DispId(34)]
        public string CustomerCode
        {
            get
            {
                return _customerCode;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequest/members[@name="CreatorName"]/*' />
        [DispId(35)]
        public string CreatorName
        {
            get
            {
                return _creatorName;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequest/members[@name="LastModifyDate"]/*' />
        [DispId(36)]
        public DateTime LastModifyDate
        {
            get
            {
                return _lastModifyDate;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequest/members[@name="RequestStatus"]/*' />
        [DispId(37)]
        public CertificateRequestStatus RequestStatus
        {
            get
            {
                return _requestStatus;
            }

        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequest/members[@name="RequestStage"]/*' />
        [DispId(38)]
        public CertificateRequestStage RequestStage
        {
            get
            {
                return _requestStage;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequest/members[@name="CommunicationMode"]/*' />
        [DispId(39)]
        public CommunicationMode CommunicationMode
        {
            get
            {
                return _communicationMode;
            }
        }

        #region Internal Members

        /// <summary>
        /// Load an empty local CertificateRequest object from the data provided by the web service.
        /// </summary>
        /// <param name="SvcRequest">The CertificateRequest object provided by the web service.</param>
        internal void CopyFrom(ProxyCertificateRequest SvcRequest)
        {
            _requestId = SvcRequest.RequestId;
            _trackingCode = SvcRequest.TrackingCode;
            _sourceLocationCode = SvcRequest.SourceLocationCode;
            _requestDate = SvcRequest.RequestDate;
            _customerCode = SvcRequest.CustomerCode;
            _creatorName = SvcRequest.CreatorName;
            _lastModifyDate = SvcRequest.LastModifyDate;
            _requestStatus = (CertificateRequestStatus)SvcRequest.RequestStatus;
            _requestStage = (CertificateRequestStage)SvcRequest.RequestStage;
            _communicationMode = (CommunicationMode)SvcRequest.CommunicationMode;
        }

        #endregion

    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequests/class/*' />
    [Guid("6B850720-78FE-471e-B5F2-7941D154838B")]
    [ComVisible(true)]
    public class CertificateRequests : ReadOnlyArrayList
    {
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal CertificateRequests() { }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/collection/members[@name="this"]/*' />
        [DispId(0)]
        public new CertificateRequest this[int index]
        {
            get
            {
                return (CertificateRequest)base[index];
            }
        }
    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateJurisdiction/class/*' />
    [Guid("B737D563-6CD8-48f4-894E-962B487ADFE2")]
    [ComVisible(true)]
    public class CertificateJurisdiction
    {
        string _jurisdiction;
        string _country;
        DateTime _expiryDate;
        bool _doesNotExpire;
        string[] _permitNumbers;

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal CertificateJurisdiction() { }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateJurisdiction/members[@name="Jurisdiction"]/*' />
        [DispId(20)]
        public string Jurisdiction
        {
            get
            {
                return _jurisdiction;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateJurisdiction/members[@name="Country"]/*' />
        [DispId(21)]
        public string Country
        {
            get
            {
                return _country;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateJurisdiction/members[@name="ExpiryDate"]/*' />
        [DispId(22)]
        public DateTime ExpiryDate
        {
            get
            {
                return _expiryDate;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateJurisdiction/members[@name="DoesNotExpire"]/*' />
        [DispId(23)]
        public bool DoesNotExpire
        {
            get
            {
                return _doesNotExpire;
            }
        }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateJurisdiction/members[@name="PermitNumbers"]/*' />
        [DispId(24)]
        public string[] PermitNumbers
        {
            get
            {
                return _permitNumbers;
            }
        }

        #region Internal Members

        /// <summary>
        /// Load an empty local CertificateJurisdiction object from the data provided by the web service.
        /// </summary>
        /// <param name="SvcCertificateJurisdiction">The CertificateJurisdiction object provided by the web service.</param>
        internal void CopyFrom(ProxyCertificateJurisdiction SvcCertificateJurisdiction)
        {
            _jurisdiction = SvcCertificateJurisdiction.Jurisdiction;
            _country = SvcCertificateJurisdiction.Country;
            _expiryDate = SvcCertificateJurisdiction.ExpiryDate;
            _doesNotExpire = SvcCertificateJurisdiction.DoesNotExpire;
            _permitNumbers = SvcCertificateJurisdiction.PermitNumbers;
        }

        #endregion

    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateJurisdictions/class/*' />
    [Guid("DD29788F-E01B-49e5-B0DD-7419ABCF9DC2")]
    [ComVisible(true)]
    public class CertificateJurisdictions : ReadOnlyArrayList
    {
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal CertificateJurisdictions() { }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/collection/members[@name="this"]/*' />
        [DispId(0)]
        public new CertificateJurisdiction this[int index]
        {
            get
            {
                return (CertificateJurisdiction)base[index];
            }
        }
    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/RequestType/enum/*' />
    [Guid("79FF0AFD-FB60-4300-A89F-A070BADC41C2")]
    [ComVisible(true)]
    public enum RequestType
    {
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/RequestType/members[@name="STANDARD"]/*' />
        STANDARD = 0,

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/RequestType/members[@name="DIRECT"]/*' />
        DIRECT = 1
    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/FormatType/enum/*' />
    [Guid("F072170E-6DEB-433a-BA3E-C60C68C67DD0")]
    [ComVisible(true)]
    public enum FormatType
    {
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/FormatType/members[@name="NULL"]/*' />
        NULL = 0,

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/FormatType/members[@name="PNG"]/*' />
        PNG = 1,

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/FormatType/members[@name="PDF"]/*' />
        PDF = 2
    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateStatus/enum/*' />
    [Guid("CBEC7600-323C-4c11-94D1-34335475F4A5")]
    [ComVisible(true)]
    public enum CertificateStatus
    {
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateStatus/members[@name="ACTIVE"]/*' />
        ACTIVE = 0,

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateStatus/members[@name="VOID"]/*' />
        VOID = 1,

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateStatus/members[@name="INCOMPLETE"]/*' />
        INCOMPLETE = 2
    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/ReviewStatus/enum/*' />
    [Guid("F22A5E92-05A6-4ed4-B61C-BC5A3626AABB")]
    [ComVisible(true)]
    public enum ReviewStatus
    {
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/ReviewStatus/members[@name="PENDING"]/*' />
        PENDING = 0,

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/ReviewStatus/members[@name="ACCEPTED"]/*' />
        ACCEPTED = 1,

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/ReviewStatus/members[@name="REJECTED"]/*' />
        REJECTED = 2
    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestStatus/enum/*' />
    [Guid("BB68F1DC-4399-4d6d-B328-84E784F588AC")]
    [ComVisible(true)]
    public enum CertificateRequestStatus
    {
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestStatus/members[@name="ALL"]/*' />
        ALL = 0,

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestStatus/members[@name="OPEN"]/*' />
        OPEN = 1,

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestStatus/members[@name="CLOSED"]/*' />
        CLOSED = 2
    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestStage/enum/*' />
    [Guid("FAE2EFB7-B871-4a18-91DD-1E62C90002B4")]
    [ComVisible(true)]
    public enum CertificateRequestStage
    {
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestStage/members[@name="REQUESTINITIATED"]/*' />
        REQUESTINITIATED = 0,

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestStage/members[@name="CUSTOMERRESPONDED"]/*' />
        CUSTOMERRESPONDED = 1,

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateRequestStage/members[@name="CERTIFICATERECEIVED"]/*' />
        CERTIFICATERECEIVED = 2
    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateUsage/enum/*' />
    [Guid("1D8464C3-3F14-4645-8EE2-A8CC8E7BB466")]
    [ComVisible(true)]
    public enum CertificateUsage
    {
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateUsage/members[@name="BLANKET"]/*' />
        BLANKET = 0,

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateUsage/members[@name="SINGLE"]/*' />
        SINGLE = 1,
        
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CertificateUsage/members[@name="NULL"]/*' />
        NULL = 2
    }

    /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CommunicationMode/enum/*' />
    [Guid("EF15E902-028C-4ced-9525-B95A2408A5C9")]
    [ComVisible(true)]
    public enum CommunicationMode
    {
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CommunicationMode/members[@name="NULL"]/*' />
        NULL = 0,

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CommunicationMode/members[@name="EMAIL"]/*' />
        EMAIL = 1,

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CommunicationMode/members[@name="MAIL"]/*' />
        MAIL = 2,

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/CommunicationMode/members[@name="FAX"]/*' />
        FAX = 3
    }
}
