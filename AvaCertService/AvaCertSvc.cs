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
using Avalara.AvaTax.Adapter.Proxies.AvaCertSvcProxy;

namespace Avalara.AvaTax.Adapter.AvaCertService
{
    /// <include file='AvaCertSvc.Doc.xml' path='adapter/AvaCertSvc/class/*' />
    [Guid("9CF264A1-9023-4add-A625-6D4FABBF1BCD")]
    [ComVisible(true)]
    public class AvaCertSvc : BaseSvc
    {
        private const string SERVICE_NAME = "AvaCert/AvaCertSvc.asmx";
        private AvaLogger _avaLog;

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/common/members[@name="Constructor"]/*' />
        public AvaCertSvc()
        {
            try
            {
                _avaLog = AvaLogger.GetLogger();
                _avaLog.Debug(string.Format("Instantiating AvaCertSvc: {0}", base.UniqueId));
                base.ServiceName = SERVICE_NAME;
                ProxyAvaCertSvc proxy = new ProxyAvaCertSvc();
                proxy.ProfileValue = new ProxyProfile();
                proxy.Security = new ProxySecurity();
                base.SvcProxy = proxy;

            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(ex);
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/common/members[@name="Ping"]/*' />
        [DispId(30)]
        public PingResult Ping(string message)
        {
            try
            {
                _avaLog.Debug("AvaCertSvc.Ping");
                ProxyPingResult svcResult = (ProxyPingResult)base.InvokeService(typeof(ProxyAvaCertSvc), MethodBase.GetCurrentMethod().Name, new object[] { message });

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

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/common/members[@name="IsAuthorized"]/*' />
        [DispId(31)]
        public IsAuthorizedResult IsAuthorized(string operations)
        {
            try
            {
                _avaLog.Debug("AvaCertSvc.IsAuthorized");

                ProxyIsAuthorizedResult svcResult = (ProxyIsAuthorizedResult)base.InvokeService(typeof(ProxyAvaCertSvc), MethodBase.GetCurrentMethod().Name, new object[] { operations });

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

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/AvaCertSvc/members[@name="AddCustomer"]/*' />
        [DispId(32)]
        public AddCustomerResult AddCustomer(AddCustomerRequest addCustomerRequest)
        {
            try
            {
                _avaLog.Debug("AvaCertSvc.AddCustomer");
                _avaLog.Debug("Validate request");
                Utilities.VerifyRequestObject(addCustomerRequest);

                _avaLog.Debug("Copying customer into proxy object");
                ProxyAddCustomerRequest proxyRequest = new ProxyAddCustomerRequest();
                addCustomerRequest.CopyTo(proxyRequest);

                ProxyAddCustomerResult svcResult = (ProxyAddCustomerResult)base.InvokeService(typeof(ProxyAvaCertSvc), MethodBase.GetCurrentMethod().Name, new object[] { proxyRequest });

                _avaLog.Debug("Copying result from proxy object");
                
                AddCustomerResult localResult = new AddCustomerResult();
                localResult.CopyFrom(svcResult);

                return localResult;
            }
            catch (Exception ex)
            {
                return AddCustomerResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/AvaCertSvc/members[@name="InitiateExemptCert"]/*' />
        [DispId(33)]
        public InitiateExemptCertResult InitiateExemptCert(InitiateExemptCertRequest initiateExemptCertRequest)
        {
            try
            {
                _avaLog.Debug("AvaCertSvc.InitiateExemptCert");
                _avaLog.Debug("Validate request");
                Utilities.VerifyRequestObject(initiateExemptCertRequest);

                _avaLog.Debug("Copying exempt certificate into proxy object");
                ProxyInitiateExemptCertRequest proxyRequest = new ProxyInitiateExemptCertRequest();
                initiateExemptCertRequest.CopyTo(proxyRequest);

                ProxyInitiateExemptCertResult svcResult = (ProxyInitiateExemptCertResult)base.InvokeService(typeof(ProxyAvaCertSvc), MethodBase.GetCurrentMethod().Name, new object[] { proxyRequest });


                _avaLog.Debug("Copying result from proxy object");
                InitiateExemptCertResult localResult = new InitiateExemptCertResult();
                localResult.CopyFrom(svcResult);

                return localResult;
            }
            catch (Exception ex)
            {
                return InitiateExemptCertResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/AvaCertSvc/members[@name="GetExemptionCertificates"]/*' />
        [DispId(34)]
        public GetExemptionCertificatesResult GetExemptionCertificates(GetExemptionCertificatesRequest getExemptionCertificatesRequest)
        {
            try
            {
                _avaLog.Debug("AvaCertSvc.GetExemptionCertificates");
                _avaLog.Debug("Validate request");
                Utilities.VerifyRequestObject(getExemptionCertificatesRequest);

                _avaLog.Debug("Copying ExemptionCertificates into proxy object");
                ProxyGetExemptionCertificatesRequest proxyRequest = new ProxyGetExemptionCertificatesRequest();
                getExemptionCertificatesRequest.CopyTo(proxyRequest);

                ProxyGetExemptionCertificatesResult svcResult = (ProxyGetExemptionCertificatesResult)base.InvokeService(typeof(ProxyAvaCertSvc), MethodBase.GetCurrentMethod().Name, new object[] { proxyRequest });

                _avaLog.Debug("Copying result from proxy object");

                GetExemptionCertificatesResult localResult = new GetExemptionCertificatesResult();
                localResult.CopyFrom(svcResult);

                return localResult;
            }
            catch (Exception ex)
            {
                return GetExemptionCertificatesResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/common/members[@name="Finalize"]/*' />
        ~AvaCertSvc()
		{
			if(_avaLog != null)
                _avaLog.Debug(string.Format("Finalizing AvaCertSvc: {0}", base.UniqueId));
		}

		#region Internal Members

		/// <summary>
		/// Convenient wrapper property that casts the base WebServicesClientProtocol into our strongly typed proxy class.
		/// </summary>
        internal new ProxyAvaCertSvc SvcProxy
		{
			get
			{
				return (ProxyAvaCertSvc)base.SvcProxy;
			}
		}
		#endregion

    }

    /// <include file='AvaCertSvc.Doc.xml' path='adapter/Customer/class/*' />
    [Guid("7DE00086-BB2F-408e-BCE7-7A71938AE148")]
    [ComVisible(true)]
    public class Customer
    {
        string _companyCode;
        string _customerCode;
        string _newCustomerCode;
        string _parentCustomerCode;
        string _customerType;
        string _customerName;
        string _attn;
        string _address1;
        string _address2;
        string _city;
        string _region;
        string _postalCode;
        string _country;
        string _phone;
        string _fax;
        string _email;
        

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/common/members[@name="Constructor"]/*' />
        public Customer()
        {

        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/Customer/members[@name="CompanyCode"]/*' />
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

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/Customer/members[@name="CustomerCode"]/*' />
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

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/Customer/members[@name="NewCustomerCode"]/*' />
        [DispId(22)]
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

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/Customer/members[@name="ParentCustomerCode"]/*' />
        [DispId(23)]
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

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/Customer/members[@name="CustomerType"]/*' />
        [DispId(24)]
        public string CustomerType
        {
            get
            {
                return _customerType;
            }
            set
            {
                _customerType = value;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/Customer/members[@name="CustomerName"]/*' />
        [DispId(25)]
        public string CustomerName
        {
            get
            {
                return _customerName;
            }
            set
            {
                _customerName = value;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/Customer/members[@name="Attn"]/*' />
        [DispId(26)]
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

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/Customer/members[@name="Address1"]/*' />
        [DispId(27)]
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

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/Customer/members[@name="Address2"]/*' />
        [DispId(28)]
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

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/Customer/members[@name="City"]/*' />
        [DispId(29)]
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

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/Customer/members[@name="Region"]/*' />
        [DispId(30)]
        public string Region
        {
            get
            {
                return _region;
            }
            set
            {
                _region = value;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/Customer/members[@name="PostalCode"]/*' />
        [DispId(31)]
        public string PostalCode
        {
            get
            {
                return _postalCode;
            }
            set
            {
                _postalCode = value;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/Customer/members[@name="Country"]/*' />
        [DispId(32)]
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

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/Customer/members[@name="Phone"]/*' />
        [DispId(33)]
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

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/Customer/members[@name="Fax"]/*' />
        [DispId(34)]
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

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/Customer/members[@name="Email"]/*' />
        [DispId(35)]
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
            _companyCode = SvcCustomer.CompanyCode;
            _customerCode = SvcCustomer.CustomerCode;
            _newCustomerCode = SvcCustomer.NewCustomerCode;
            _parentCustomerCode = SvcCustomer.ParentCustomerCode;
            _customerType = SvcCustomer.CustomerType;
            _customerName = SvcCustomer.CustomerName;
            _attn = SvcCustomer.Attn;
            _address1 = SvcCustomer.Address1;
            _address2 = SvcCustomer.Address2;
            _city = SvcCustomer.City;
            _region = SvcCustomer.Region;
            _postalCode = SvcCustomer.PostalCode;
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
            SvcCustomer.CompanyCode = CompanyCode;
            SvcCustomer.CustomerCode = CustomerCode;
            SvcCustomer.NewCustomerCode = NewCustomerCode;
            SvcCustomer.ParentCustomerCode = ParentCustomerCode;
            SvcCustomer.CustomerType = CustomerType;
            SvcCustomer.CustomerName = CustomerName;
            SvcCustomer.Attn = Attn;
            SvcCustomer.Address1 = Address1;
            SvcCustomer.Address2 = Address2;
            SvcCustomer.City = City;
            SvcCustomer.Region = Region;
            SvcCustomer.PostalCode = PostalCode;
            SvcCustomer.Country = Country;
            SvcCustomer.Phone = Phone;
            SvcCustomer.Fax = Fax;
            SvcCustomer.Email = Email;
        }

        #endregion

    }


    /// <include file='AvaCertSvc.Doc.xml' path='adapter/AddCustomerRequest/class/*' />
    [Guid("9A2A1CE6-2E6A-4661-837C-AD027A5A010D")]
    [ComVisible(true)]
    public class AddCustomerRequest : BaseRequest
    {
        private Customer _customer;

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/AddCustomerRequest/members[@name="Customer"]/*' />
        [DispId(20)]
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
        /// Loads a local AddCustomerRequest object into a web service copy of the same object.
        /// </summary>
        /// <param name="SvcRequest">The AddCustomerRequest object to be copied to.</param>
        internal void CopyTo(ProxyAddCustomerRequest SvcRequest)
        {
            ProxyCustomer proxyCustomer = new ProxyCustomer();
            this.Customer.CopyTo(proxyCustomer);

            SvcRequest.Customer = proxyCustomer;
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

    /// <include file='AvaCertSvc.Doc.xml' path='adapter/AddCustomerResult/class/*' />
    [Guid("734CC742-8342-4a0e-AD15-4B4D9098ED22")]
    [ComVisible(true)]
    public class AddCustomerResult : BaseResult
    {
        /// <include file='AvaCertSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal AddCustomerResult()
		{
		}

        #region Internal Members

        /// <summary>
        /// Creates a new AddCustomerResult based on a <see cref="BaseResult"/>.
        /// </summary>
        /// <param name="baseResult"></param>
        /// <returns></returns>
        internal static AddCustomerResult CastFromBaseResult(BaseResult baseResult)
        {
            AddCustomerResult result = new AddCustomerResult();
            result.CopyFrom(baseResult);
            return result;
        }

        #endregion

    }


    /// <include file='AvaCertSvc.Doc.xml' path='adapter/InitiateExemptCertRequest/class/*' />
    [Guid("30F792F9-3DDD-4ce3-91C9-C352A1ECB8EB")]
    [ComVisible(true)]
    public class InitiateExemptCertRequest : BaseRequest
    {
        Customer _customer;
        CommunicationMode _communicationMode;
        string _locationCode;
        string _customMessage;
        RequestType _type;

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/InitiateExemptCertRequest/members[@name="Customer"]/*' />
        [DispId(20)]
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

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/InitiateExemptCertRequest/members[@name="CommunicationMode"]/*' />
        [DispId(21)]
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

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/InitiateExemptCertRequest/members[@name="LocationCode"]/*' />
        [DispId(22)]
        public string LocationCode
        {
            get
            {
                return _locationCode;
            }
            set
            {
                _locationCode = value;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/InitiateExemptCertRequest/members[@name="CustomMessage"]/*' />
        [DispId(23)]
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

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/InitiateExemptCertRequest/members[@name="Type"]/*' />
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

        #region Internal Members

        /// <summary>
        /// Loads a local InitiateExemptCertRequest object into a web service copy of the same object.
        /// </summary>
        /// <param name="SvcRequest">The InitiateExemptCertRequest object to be copied to.</param>
        internal void CopyTo(ProxyInitiateExemptCertRequest SvcRequest)
        {
            ProxyCustomer proxyCustomer = new ProxyCustomer();
            if (Customer != null)
            {
                Customer.CopyTo(proxyCustomer);
                SvcRequest.Customer = proxyCustomer;
            }            
            SvcRequest.CommunicationMode = (ProxyCommunicationMode)CommunicationMode;
            SvcRequest.LocationCode = LocationCode;
            SvcRequest.CustomMessage = CustomMessage;
            SvcRequest.Type = (ProxyRequestType)Type;
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

    /// <include file='AvaCertSvc.Doc.xml' path='adapter/InitiateExemptCertResult/class/*' />
    [Guid("D90A8CEC-69C7-41d4-A0A3-205337F3053E")]
    [ComVisible(true)]
    public class InitiateExemptCertResult : BaseResult
    {
        string _trackingCode;
        string _wizardLaunchUrl;

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal InitiateExemptCertResult()
		{
		}

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/InitiateExemptCertResult/members[@name="TrackingCode"]/*' />
        [DispId(30)]
        public string TrackingCode
        {
            get
            {
                return _trackingCode;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/InitiateExemptCertResult/members[@name="WizardLaunchUrl"]/*' />
        [DispId(31)]
        public string WizardLaunchUrl
        {
            get
            {
                return _wizardLaunchUrl;
            }
        }

        #region Internal Members

        /// <summary>
        /// Load an empty local InitiateExemptCert object from the data provided by the web service.
        /// </summary>
        /// <param name="SvcInitiateExemptCertResult">The InitiateExemptCertResult object provided by the web service.</param>
        internal void CopyFrom(ProxyInitiateExemptCertResult SvcInitiateExemptCertResult)
        {
            base.CopyFrom(SvcInitiateExemptCertResult);
            _trackingCode = SvcInitiateExemptCertResult.TrackingCode;
            _wizardLaunchUrl = SvcInitiateExemptCertResult.WizardLaunchUrl;
        }

        /// <summary>
        /// Creates a new InitiateExemptCertResult based on a <see cref="BaseResult"/>.
        /// </summary>
        /// <param name="baseResult"></param>
        /// <returns></returns>
        internal static InitiateExemptCertResult CastFromBaseResult(BaseResult baseResult)
        {
            InitiateExemptCertResult result = new InitiateExemptCertResult();
            result.CopyFrom(baseResult);
            return result;
        }

        #endregion
    }

    /// <include file='AvaCertSvc.Doc.xml' path='adapter/GetExemptionCertificatesRequest/class/*' />
    [Guid("72A23952-CE00-4cf3-AD0F-EFC731BB1B80")]
    [ComVisible(true)]
    public class GetExemptionCertificatesRequest : BaseRequest
    {
        string _companyCode;
        string _customerCode;
        DateTime _fromDate;
        DateTime _toDate;
        string _region;

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/GetExemptionCertificatesRequest/members[@name="CompanyCode"]/*' />
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

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/GetExemptionCertificatesRequest/members[@name="CustomerCode"]/*' />
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

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/GetExemptionCertificatesRequest/members[@name="FromDate"]/*' />
        [DispId(22)]
        public DateTime FromDate
        {
            get
            {
                return _fromDate;
            }
            set
            {
                _fromDate = value;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/GetExemptionCertificatesRequest/members[@name="ToDate"]/*' />
        [DispId(23)]
        public DateTime ToDate
        {
            get
            {
                return _toDate;
            }
            set
            {
                _toDate = value;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/GetExemptionCertificatesRequest/members[@name="Region"]/*' />
        [DispId(24)]
        public string Region
        {
            get
            {
                return _region;
            }
            set
            {
                _region = value;
            }
        }

        #region Internal Members

        /// <summary>
        /// Loads a local GetExemptionCertificatesRequest object into a web service copy of the same object.
        /// </summary>
        /// <param name="SvcRequest">The GetExemptionCertificatesRequest object to be copied to.</param>
        internal void CopyTo(ProxyGetExemptionCertificatesRequest SvcRequest)
        {
            SvcRequest.CompanyCode = CompanyCode;
            SvcRequest.CustomerCode = CustomerCode;
            SvcRequest.FromDate = FromDate;
            SvcRequest.ToDate = ToDate;
            SvcRequest.Region = Region;
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

    /// <include file='AvaCertSvc.Doc.xml' path='adapter/GetExemptionCertificatesResult/class/*' />
    [Guid("E148D41A-D7D0-4f92-AD62-FF3F864CFBAF")]
    [ComVisible(true)]
    public class GetExemptionCertificatesResult : BaseResult
    {
        ExemptionCertificates _exemptionCertificates = new ExemptionCertificates();
        int _recordCount;

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal GetExemptionCertificatesResult()
		{
		}

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/GetExemptionCertificatesResult/members[@name="ExemptionCertificates"]/*' />
        [DispId(30)]
        public ExemptionCertificates ExemptionCertificates
        {
            get
            {
                return _exemptionCertificates;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/GetExemptionCertificatesResult/members[@name="RecordCount"]/*' />
        [DispId(31)]
        public int RecordCount
        {
            get
            {
                return _recordCount;
            }
        }

        #region Internal Members

        /// <summary>
        /// Load an empty local GetExemptionCertificates object from the data provided by the web service.
        /// </summary>
        /// <param name="SvcGetExemptionCertificatesResult">The GetExemptionCertificatesResult object provided by the web service.</param>
        internal void CopyFrom(ProxyGetExemptionCertificatesResult SvcGetExemptionCertificatesResult)
        {
            base.CopyFrom(SvcGetExemptionCertificatesResult);
            _recordCount = SvcGetExemptionCertificatesResult.RecordCount;

            if (SvcGetExemptionCertificatesResult.ExemptionCertificates != null && SvcGetExemptionCertificatesResult.ExemptionCertificates.Length > 0)
            {
                for (int idx = 0; idx < SvcGetExemptionCertificatesResult.ExemptionCertificates.Length; idx++)
                {
                    ExemptionCertificate exemptionCertificate = new ExemptionCertificate();
                    exemptionCertificate.CopyFrom(SvcGetExemptionCertificatesResult.ExemptionCertificates[idx]);
                    _exemptionCertificates.Add(exemptionCertificate);
                }
            }
        }

        /// <summary>
        /// Creates a new GetExemptionCertificatesResult based on a <see cref="BaseResult"/>.
        /// </summary>
        /// <param name="baseResult"></param>
        /// <returns></returns>
        internal static GetExemptionCertificatesResult CastFromBaseResult(BaseResult baseResult)
        {
            GetExemptionCertificatesResult result = new GetExemptionCertificatesResult();
            result.CopyFrom(baseResult);
            return result;
        }

        #endregion
    }

    /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/class/*' />
    [Guid("41E0A817-BACB-47d8-A750-C41BB900634B")]
    [ComVisible(true)]
    public class ExemptionCertificate
    {
        string _avaCertId;
        Jurisdictions _jurisdictions = new Jurisdictions();
        string[] _customerCodes;
        string _customerType;
        string _locationName;
        string _locationCode;
        CertificateStatus _certificateStatus;
        ReviewStatus _reviewStatus;
        DateTime _createdDate;
        DateTime _modifiedDate;
        DateTime _receivedDate;
        string _businessName;
        string _address1;
        string _address2;
        string _city;
        string _region;
        string _country;
        string _postalCode;
        string _phone;
        string _email;
        string _exemptFormName;
        string _custom1;
        string _custom2;
        string _custom3;
        string _signerName;
        string _signerTitle;
        DateTime _signedDate;
        string _businessDescription;
        string _sellerPropertyDescription;
        CertificateUsage _certificateUsage;
        bool _isPartialExemption;
        string _exemptReasonCode;

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal ExemptionCertificate()
        {
            _certificateUsage = CertificateUsage.NULL;
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="AvaCertId"]/*' />
        [DispId(20)]
        public string AvaCertId
        {
            get
            {
                return _avaCertId;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="Jurisdictions"]/*' />
        [DispId(21)]
        public Jurisdictions Jurisdictions
        {
            get
            {
                return _jurisdictions;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="CustomerCodes"]/*' />
        [DispId(22)]
        public string[] CustomerCodes
        {
            get
            {
                return _customerCodes;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="CustomerType"]/*' />
        [DispId(23)]
        public string CustomerType
        {
            get
            {
                return _customerType;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="LocationName"]/*' />
        [DispId(24)]
        public string LocationName
        {
            get
            {
                return _locationName;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="LocationCode"]/*' />
        [DispId(25)]
        public string LocationCode
        {
            get
            {
                return _locationCode;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="CertificateStatus"]/*' />
        [DispId(26)]
        public CertificateStatus CertificateStatus
        {
            get
            {
                return _certificateStatus;
            }

        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="ReviewStatus"]/*' />
        [DispId(27)]
        public ReviewStatus ReviewStatus
        {
            get
            {
                return _reviewStatus;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="CreatedDate"]/*' />
        [DispId(28)]
        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="ModifiedDate"]/*' />
        [DispId(29)]
        public DateTime ModifiedDate
        {
            get
            {
                return _modifiedDate;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="ReceivedDate"]/*' />
        [DispId(30)]
        public DateTime ReceivedDate
        {
            get
            {
                return _receivedDate;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="BusinessName"]/*' />
        [DispId(31)]
        public string BusinessName
        {
            get
            {
                return _businessName;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="Address1"]/*' />
        [DispId(32)]
        public string Address1
        {
            get
            {
                return _address1;
            }
        }
        
        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="Address2"]/*' />
        [DispId(33)]
        public string Address2
        {
            get
            {
                return _address2;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="City"]/*' />
        [DispId(34)]
        public string City
        {
            get
            {
                return _city;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="Region"]/*' />
        [DispId(35)]
        public string Region
        {
            get
            {
                return _region;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="Country"]/*' />
        [DispId(36)]
        public string Country
        {
            get
            {
                return _country;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="PostalCode"]/*' />
        [DispId(37)]
        public string PostalCode
        {
            get
            {
                return _postalCode;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="Phone"]/*' />
        [DispId(38)]
        public string Phone
        {
            get
            {
                return _phone;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="Email"]/*' />
        [DispId(39)]
        public string Email
        {
            get
            {
                return _email;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="ExemptFormName"]/*' />
        [DispId(40)]
        public string ExemptFormName
        {
            get
            {
                return _exemptFormName;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="Custom1"]/*' />
        [DispId(41)]
        public string Custom1
        {
            get
            {
                return _custom1;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="Custom2"]/*' />
        [DispId(42)]
        public string Custom2
        {
            get
            {
                return _custom2;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="Custom3"]/*' />
        [DispId(43)]
        public string Custom3
        {
            get
            {
                return _custom3;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="SignerName"]/*' />
        [DispId(44)]
        public string SignerName
        {
            get
            {
                return _signerName;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="SignerTitle"]/*' />
        [DispId(45)]
        public string SignerTitle
        {
            get
            {
                return _signerTitle;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="SignedDate"]/*' />
        [DispId(46)]
        public DateTime SignedDate
        {
            get
            {
                return _signedDate;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="BusinessDescription"]/*' />
        [DispId(47)]
        public string BusinessDescription
        {
            get
            {
                return _businessDescription;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="SellerPropertyDescription"]/*' />
        [DispId(48)]
        public string SellerPropertyDescription
        {
            get
            {
                return _sellerPropertyDescription;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="CertificateUsage"]/*' />
        [DispId(49)]
        public CertificateUsage CertificateUsage
        {
            get
            {
                return _certificateUsage;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="IsPartialExemption"]/*' />
        [DispId(50)]
        public bool IsPartialExemption
        {
            get
            {
                return _isPartialExemption;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificate/members[@name="ExemptReasonCode"]/*' />
        [DispId(51)]
        public string ExemptReasonCode
        {
            get
            {
                return _exemptReasonCode;
            }
        }

        #region Internal Members

        /// <summary>
        /// Load an empty local ExemptionCertificate object from the data provided by the web service.
        /// </summary>
        /// <param name="SvcExemptionCertificate">The ExemptionCertificate object provided by the web service.</param>
        internal void CopyFrom(ProxyExemptionCertificate SvcExemptionCertificate)
        {
            _avaCertId = SvcExemptionCertificate.AvaCertId;

            for (int ii = 0; ii < SvcExemptionCertificate.Jurisdictions.Length; ii++)
            {
                Jurisdiction jurisdiction = new Jurisdiction();
                jurisdiction.CopyFrom(SvcExemptionCertificate.Jurisdictions[ii]);
                _jurisdictions.Add(jurisdiction);
            }
            _customerCodes = SvcExemptionCertificate.CustomerCodes;
            _customerType = SvcExemptionCertificate.CustomerType;
            _locationName = SvcExemptionCertificate.LocationName;
            _locationCode = SvcExemptionCertificate.LocationCode;
            _certificateStatus = (CertificateStatus)SvcExemptionCertificate.CertificateStatus;
            _reviewStatus = (ReviewStatus)SvcExemptionCertificate.ReviewStatus;
            _createdDate = SvcExemptionCertificate.CreatedDate;
            _modifiedDate = SvcExemptionCertificate.ModifiedDate;
            _receivedDate = SvcExemptionCertificate.ReceivedDate;
            _businessName = SvcExemptionCertificate.BusinessName;
            _address1 = SvcExemptionCertificate.Address1;
            _address2 = SvcExemptionCertificate.Address2;
            _city = SvcExemptionCertificate.City;
            _region = SvcExemptionCertificate.Region;
            _country = SvcExemptionCertificate.Country;
            _postalCode = SvcExemptionCertificate.PostalCode;
            _phone = SvcExemptionCertificate.Phone;
            _email = SvcExemptionCertificate.Email;
            _exemptFormName = SvcExemptionCertificate.ExemptFormName;
            _custom1 = SvcExemptionCertificate.Custom1;
            _custom2 = SvcExemptionCertificate.Custom2;
            _custom3 = SvcExemptionCertificate.Custom3;
            _signerName = SvcExemptionCertificate.SignerName;
            _signerTitle = SvcExemptionCertificate.SignerTitle;
            _signedDate = SvcExemptionCertificate.SignedDate;
            _businessDescription = SvcExemptionCertificate.BusinessDescription;
            _sellerPropertyDescription = SvcExemptionCertificate.SellerPropertyDescription;
            _certificateUsage = (CertificateUsage)SvcExemptionCertificate.CertificateUsage;
            _isPartialExemption = SvcExemptionCertificate.IsPartialExemption;
            _exemptReasonCode = SvcExemptionCertificate.ExemptReasonCode;
        }

        #endregion

    }

    /// <include file='AvaCertSvc.Doc.xml' path='adapter/ExemptionCertificates/class/*' />
    [Guid("0B512992-7CD3-44eb-A653-7CB17D05B0DD")]
    [ComVisible(true)]
    public class ExemptionCertificates : ReadOnlyArrayList
    {
        /// <include file='AvaCertSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal ExemptionCertificates() { }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/collection/members[@name="this"]/*' />
        [DispId(0)]
        public new ExemptionCertificate this[int index]
        {
            get
            {
                return (ExemptionCertificate)base[index];
            }
        }
    }

    /// <include file='AvaCertSvc.Doc.xml' path='adapter/Jurisdiction/class/*' />
    [Guid("1C1B25F4-A324-417a-A8D1-9DB3A3B1A4E2")]
    [ComVisible(true)]
    public class Jurisdiction
    {
        string _jurisdictionCode;
        string _country;
        DateTime _expiryDate;
        bool _doesNotExpire;
        string[] _permitNumbers;

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal Jurisdiction() { }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/Jurisdiction/members[@name="JurisdictionCode"]/*' />
        [DispId(20)]
        public string JurisdictionCode
        {
            get
            {
                return _jurisdictionCode;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/Jurisdiction/members[@name="Country"]/*' />
        [DispId(21)]
        public string Country
        {
            get
            {
                return _country;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/Jurisdiction/members[@name="ExpiryDate"]/*' />
        [DispId(22)]
        public DateTime ExpiryDate
        {
            get
            {
                return _expiryDate;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/Jurisdiction/members[@name="DoesNotExpire"]/*' />
        [DispId(23)]
        public bool DoesNotExpire
        {
            get
            {
                return _doesNotExpire;
            }
        }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/Jurisdiction/members[@name="PermitNumbers"]/*' />
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
        /// Load an empty local Jurisdiction object from the data provided by the web service.
        /// </summary>
        /// <param name="SvcJurisdiction">The Jurisdiction object provided by the web service.</param>
        internal void CopyFrom(ProxyJurisdiction SvcJurisdiction)
        {
            _jurisdictionCode = SvcJurisdiction.JurisdictionCode;
            _country = SvcJurisdiction.Country;
            _expiryDate = SvcJurisdiction.ExpiryDate;
            _doesNotExpire = SvcJurisdiction.DoesNotExpire;
            _permitNumbers = SvcJurisdiction.PermitNumbers;
        }

        #endregion

    }

    /// <include file='AvaCertSvc.Doc.xml' path='adapter/Jurisdictions/class/*' />
    [Guid("5DB373A9-7E3D-452b-89E1-6C44EC17EF46")]
    [ComVisible(true)]
    public class Jurisdictions : ReadOnlyArrayList
    {
        /// <include file='AvaCertSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal Jurisdictions() { }

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/collection/members[@name="this"]/*' />
        [DispId(0)]
        public new Jurisdiction this[int index]
        {
            get
            {
                return (Jurisdiction)base[index];
            }
        }
    }

    /// <include file='AvaCertSvc.Doc.xml' path='adapter/CommunicationMode/enum/*' />
    [Guid("F47B1E34-256B-41bc-9946-E192043C5729")]
    [ComVisible(true)]
    public enum CommunicationMode
    {

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/CommunicationMode/members[@name="Email"]/*' />
        Email = 0,

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/CommunicationMode/members[@name="Mail"]/*' />
        Mail = 1,

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/CommunicationMode/members[@name="Fax"]/*' />
        Fax = 2
    }

    /// <include file='AvaCertSvc.Doc.xml' path='adapter/RequestType/enum/*' />
    [Guid("7B2EBBF4-60E3-4a31-8764-951DE844D61C")]
    [ComVisible(true)]
    public enum RequestType
    {
        /// <include file='AvaCertSvc.Doc.xml' path='adapter/RequestType/members[@name="STANDARD"]/*' />
        STANDARD = 0,

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/RequestType/members[@name="DIRECT"]/*' />
        DIRECT = 1
    }

    /// <include file='AvaCertSvc.Doc.xml' path='adapter/CertificateStatus/enum/*' />
    [Guid("28B5C95D-5D36-4602-84B0-DBF808D3E0D8")]
    [ComVisible(true)]
    public enum CertificateStatus
    {
        /// <include file='AvaCertSvc.Doc.xml' path='adapter/CertificateStatus/members[@name="ACTIVE"]/*' />
        ACTIVE = 0,

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/CertificateStatus/members[@name="VOID"]/*' />
        VOID = 1,

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/CertificateStatus/members[@name="INCOMPLETE"]/*' />
        INCOMPLETE = 2
    }

    /// <include file='AvaCertSvc.Doc.xml' path='adapter/ReviewStatus/enum/*' />
    [Guid("47EAED64-5ADC-4fe2-82C6-00EB79F0B624")]
    [ComVisible(true)]
    public enum ReviewStatus
    {
        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ReviewStatus/members[@name="PENDING"]/*' />
        PENDING = 0,

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ReviewStatus/members[@name="ACCEPTED"]/*' />
        ACCEPTED = 1,

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/ReviewStatus/members[@name="REJECTED"]/*' />
        REJECTED = 2
    }

    /// <include file='AvaCertSvc.Doc.xml' path='adapter/CertificateUsage/enum/*' />
    [Guid("B3F955E6-7A70-4cb6-885A-4F40A0269CC5")]
    [ComVisible(true)]
    public enum CertificateUsage
    {
        /// <include file='AvaCertSvc.Doc.xml' path='adapter/CertificateUsage/members[@name="BLANKET"]/*' />
        BLANKET = 0,

        /// <include file='AvaCertSvc.Doc.xml' path='adapter/CertificateUsage/members[@name="SINGLE"]/*' />
        SINGLE = 1,
        
        /// <include file='AvaCertSvc.Doc.xml' path='adapter/CertificateUsage/members[@name="NULL"]/*' />
        NULL = 2
    }
}
