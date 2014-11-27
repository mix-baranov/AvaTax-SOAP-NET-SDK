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
using Avalara.AvaTax.Adapter.Proxies.AccountSvcProxy;

namespace Avalara.AvaTax.Adapter.AccountService
{
    /// <include file='AccountSvc.Doc.xml' path='adapter/AccountSvc/class/*' />
	[Guid("671C83F8-CAFD-4d9e-A82D-15D5C11CA59C")]
	[ComVisible(true)]
	public class AccountSvc : BaseSvc, IDisposable
	{
		private const string SERVICE_NAME = "Account/AccountSvc.asmx";
		private AvaLogger _avaLog;

        /// <include file='AccountSvc.Doc.xml' path='adapter/common/members[@name="Constructor"]/*' />
		public AccountSvc()
		{
			try 
			{
				_avaLog = AvaLogger.GetLogger();
				_avaLog.Debug(string.Format("Instantiating AccountSvc: {0}", base.UniqueId));
				base.ServiceName = SERVICE_NAME;
				ProxyAccountSvc proxy = new ProxyAccountSvc();
				proxy.ProfileValue = new ProxyProfile();
				proxy.Security = new ProxySecurity();
				base.SvcProxy = proxy;

			} catch (Exception ex)
			{
				ExceptionManager.HandleException(ex);
			}
		}

        /// <include file='AccountSvc.Doc.xml' path='adapter/common/members[@name="Ping"]/*' />
        [DispId(30)]
        public PingResult Ping(string message)
        {
            try
            {
                _avaLog.Debug("AccountSvc.Ping");
                ProxyPingResult svcResult = (ProxyPingResult)base.InvokeService(typeof(ProxyAccountSvc), MethodBase.GetCurrentMethod().Name, new object[] { message });

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

        /// <include file='AccountSvc.Doc.xml' path='adapter/common/members[@name="IsAuthorized"]/*' />
		[DispId(31)]
		public IsAuthorizedResult IsAuthorized(string operations)
		{
			try
			{
                _avaLog.Debug("AccountSvc.IsAuthorized");

                ProxyIsAuthorizedResult svcResult = (ProxyIsAuthorizedResult)base.InvokeService(typeof(ProxyAccountSvc), MethodBase.GetCurrentMethod().Name, new object[] { operations });

                _avaLog.Debug("Copying result from proxy object");
                IsAuthorizedResult localResult = new IsAuthorizedResult();	//local copy to hold service results
                localResult.CopyFrom(svcResult);	//load local object with service results

				return localResult;
			} catch (Exception ex)
			{
				return IsAuthorizedResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
			}
		}

        /// <include file='AccountSvc.Doc.xml' path='adapter/AccountSvc/members[@name="CompanyFetch"]/*' />
		[DispId(32)]
        public CompanyFetchResult CompanyFetch(FetchRequest fetchRequest)
		{
			try
			{
                _avaLog.Debug("AccountSvc.CompanyFetch");

                Utilities.VerifyRequestObject(fetchRequest);

                _avaLog.Debug("Copying Company Fetch info into proxy object");
                ProxyFetchRequest proxyRequest = new ProxyFetchRequest();
                fetchRequest.CopyTo(proxyRequest);
                //Record time take for address validation
                Perf monitor = new Perf();
                monitor.Start();

                ProxyCompanyFetchResult svcResult = (ProxyCompanyFetchResult)base.InvokeService(typeof(ProxyAccountSvc), MethodBase.GetCurrentMethod().Name, new object[] { proxyRequest });

                monitor.Stop(this, ref svcResult);
                _avaLog.Debug("Copying Company info from proxy object");
                CompanyFetchResult localResult = new CompanyFetchResult();
                localResult.CopyFrom(svcResult);

				return localResult;
			} 
			catch (Exception ex)
			{
                return CompanyFetchResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
			}
		}

        /// <include file='AccountSvc.Doc.xml' path='adapter/AccountSvc/members[@name="NexusFetch"]/*' />
        [DispId(33)]
        public NexusFetchResult NexusFetch(FetchRequest fetchRequest)
        {
            try
            {
                _avaLog.Debug("AccountSvc.NexusFetch");

                Utilities.VerifyRequestObject(fetchRequest);

                _avaLog.Debug("Copying Nexus Fetch info into proxy object");
                ProxyFetchRequest proxyRequest = new ProxyFetchRequest();
                fetchRequest.CopyTo(proxyRequest);
                //Record time take for address validation
                Perf monitor = new Perf();
                monitor.Start();

                ProxyNexusFetchResult svcResult = (ProxyNexusFetchResult)base.InvokeService(typeof(ProxyAccountSvc), MethodBase.GetCurrentMethod().Name, new object[] { proxyRequest });

                monitor.Stop(this, ref svcResult);
                _avaLog.Debug("Copying Nexues info from proxy object");
                NexusFetchResult localResult = new NexusFetchResult();
                localResult.CopyFrom(svcResult);

                return localResult;
            }
            catch (Exception ex)
            {
                return NexusFetchResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
            }
        }

        /// <include file='AccountSvc.Doc.xml' path='adapter/common/members[@name="Finalize"]/*' />
        ~AccountSvc()
		{
            _avaLog.Debug(string.Format("Finalizing AccountSvc: {0}", base.UniqueId));
		}
		#region Internal Members

		/// <summary>
		/// Convenient wrapper property that casts the base WebServicesClientProtocol into our strongly typed proxy class.
		/// </summary>
		internal new ProxyAccountSvc SvcProxy
		{
			get
			{
                return (ProxyAccountSvc)base.SvcProxy;
			}
		}
		#endregion

	}

    /// <remarks/>
    [Guid("29931F1B-BFC8-42c4-B55E-F28332C5DCAC")]
    [ComVisible(true)]
    public class FetchRequest : BaseRequest
    {

        private string fieldsField;

        private string filtersField;

        private string sortField;

        private int maxCountField;

        private int pageIndexField;

        private int pageSizeField;

        private int recordCountField;

        /// <remarks/>
        public string Fields
        {
            get
            {
                return this.fieldsField;
            }
            set
            {
                this.fieldsField = value;
            }
        }

        /// <remarks/>
        public string Filters
        {
            get
            {
                return this.filtersField;
            }
            set
            {
                this.filtersField = value;
            }
        }

        /// <remarks/>
        public string Sort
        {
            get
            {
                return this.sortField;
            }
            set
            {
                this.sortField = value;
            }
        }

        /// <remarks/>
        public int MaxCount
        {
            get
            {
                return this.maxCountField;
            }
            set
            {
                this.maxCountField = value;
            }
        }

        /// <remarks/>
        public int PageIndex
        {
            get
            {
                return this.pageIndexField;
            }
            set
            {
                this.pageIndexField = value;
            }
        }

        /// <remarks/>
        public int PageSize
        {
            get
            {
                return this.pageSizeField;
            }
            set
            {
                this.pageSizeField = value;
            }
        }

        /// <remarks/>
        public int RecordCount
        {
            get
            {
                return this.recordCountField;
            }
            set
            {
                this.recordCountField = value;
            }
        }

        internal void CopyTo(ProxyFetchRequest proxyFetchRequest)
        {
             proxyFetchRequest.Fields = fieldsField;
             proxyFetchRequest.Filters = filtersField;
             proxyFetchRequest.Sort = sortField;
             proxyFetchRequest.MaxCount = maxCountField;
             proxyFetchRequest.PageIndex = pageIndexField;
             proxyFetchRequest.PageSize = pageSizeField;
             proxyFetchRequest.RecordCount = recordCountField;
        }

        internal override bool IsValid(out string message)
        {
            message = "";
            return true;
        }
    }

    /// <remarks/>
    [Guid("29931F0B-BFC8-42c4-B55E-F28332C5DCAC")]
    [ComVisible(true)]
    public class CompanyFetchResult : BaseResult
    {

        private Companies companiesField;

        private int recordCountField;

        /// <remarks/>
        public Companies Companies
        {
            get
            {
                return this.companiesField;
            }
            set
            {
                this.companiesField = value;
            }
        }

        /// <remarks/>
        public int RecordCount
        {
            get
            {
                return this.recordCountField;
            }
            set
            {
                this.recordCountField = value;
            }
        }

        internal void CopyFrom(ProxyCompanyFetchResult SvcResult)
        {
            base.CopyFrom(SvcResult);

            //iterate through addresses returned by the web service and move them into
            //    a local address object and local arraylist
            if (SvcResult.Companies != null)
            {
                companiesField = new Companies();
                for (int Index = 0; Index < SvcResult.Companies.Length; Index++)
                {
                    ProxyCompany SvcAddress = SvcResult.Companies[Index];
                    Company localCompany = new Company();
                    localCompany.CopyFrom(SvcAddress);
                    companiesField.Add(localCompany);
                }
            }
        }

        internal static CompanyFetchResult CastFromBaseResult(BaseResult baseResult)
        {
            CompanyFetchResult result = new CompanyFetchResult();
            result.CopyFrom(baseResult);
            return result;
        }
    }

    /// <remarks/>
    /// <remarks/>
    [Guid("29931F9B-BFC8-42c4-B55E-F28332C5DCAC")]
    [ComVisible(true)]
    public class NexusFetchResult : BaseResult
    {

        private Nexuses nexusesField;

        private int recordCountField;

        /// <remarks/>
        public Nexuses Nexuses
        {
            get
            {
                return this.nexusesField;
            }
            set
            {
                this.nexusesField = value;
            }
        }

        /// <remarks/>
        public int RecordCount
        {
            get
            {
                return this.recordCountField;
            }
            set
            {
                this.recordCountField = value;
            }
        }

        internal void CopyFrom(ProxyNexusFetchResult SvcResult)
        {
            base.CopyFrom(SvcResult);
            recordCountField = SvcResult.RecordCount;
            //iterate through addresses returned by the web service and move them into
            //    a local address object and local arraylist
            if (SvcResult.Nexuses != null)
            {
                nexusesField = new Nexuses();
                for (int Index = 0; Index < SvcResult.Nexuses.Length; Index++)
                {
                    ProxyNexus SvcNexus = SvcResult.Nexuses[Index];
                    Nexus locaNexus = new Nexus();
                    locaNexus.CopyFrom(SvcNexus);
                    nexusesField.Add(locaNexus);
                }
            }
        }

        internal static NexusFetchResult CastFromBaseResult(BaseResult baseResult)
        {
            NexusFetchResult result = new NexusFetchResult();
            result.CopyFrom(baseResult);
            return result;
        }
    }

    /// <include file='AccountSvc.Doc.xml' path='adapter/Certificates/class/*' />
    [Guid("29931F0B-BFC7-42c4-B55E-F28332C5DDAC")]
    [ComVisible(true)]
    public class Company
    {
        private int accountIdField;

        private int companyIdField;

        private string companyCodeField;

        private string companyNameField;

        private System.DateTime createdDateField;

        private int createdUserIdField;

        private int entityNoField;

        private bool hasProfileField;

        private bool isActiveField;

        private bool isDefaultField;

        private bool isReportingEntityField;

        private System.DateTime modifiedDateField;

        private int modifiedUserIdField;

        private int parentIdField;

        private System.DateTime sSTEffDateField;

        private string sSTPIDField;

        private string tINField;

        private string regalBankIdField;

        private string defaultCountryField;

        private string baseCurrencyCodeField;

        private RoundingLevelId roundingLevelIdField;

        private bool cashBasisAccountingEnabledField;

        private Companies childrenField;

        private CompanyContact[] contactsField;

        private Item[] itemsField;

        private Nexus[] nexusesField;

        private Company parentField;

        private TaxCode[] taxCodesField;

        private TaxRule[] taxRulesField;

        private CompanyReturn[] filingCalendarsField;

        private bool warningsEnabledField;

        private bool isTestField;

        private TaxDependencyLevelId taxDependencyLevelIdField;

        private bool inProgressField;

        private int defaultLocationIdField;

        private string businessIdentificationNoField;

        /// <remarks/>
        public int AccountId
        {
            get
            {
                return this.accountIdField;
            }
            set
            {
                this.accountIdField = value;
            }
        }

        /// <remarks/>
        public int CompanyId
        {
            get
            {
                return this.companyIdField;
            }
            set
            {
                this.companyIdField = value;
            }
        }

        /// <remarks/>
        public string CompanyCode
        {
            get
            {
                return this.companyCodeField;
            }
            set
            {
                this.companyCodeField = value;
            }
        }

        /// <remarks/>
        public string CompanyName
        {
            get
            {
                return this.companyNameField;
            }
            set
            {
                this.companyNameField = value;
            }
        }

        /// <remarks/>
        public System.DateTime CreatedDate
        {
            get
            {
                return this.createdDateField;
            }
            set
            {
                this.createdDateField = value;
            }
        }

        /// <remarks/>
        public int CreatedUserId
        {
            get
            {
                return this.createdUserIdField;
            }
            set
            {
                this.createdUserIdField = value;
            }
        }

        /// <remarks/>
        public int EntityNo
        {
            get
            {
                return this.entityNoField;
            }
            set
            {
                this.entityNoField = value;
            }
        }

        /// <remarks/>
        public bool HasProfile
        {
            get
            {
                return this.hasProfileField;
            }
            set
            {
                this.hasProfileField = value;
            }
        }

        /// <remarks/>
        public bool IsActive
        {
            get
            {
                return this.isActiveField;
            }
            set
            {
                this.isActiveField = value;
            }
        }

        /// <remarks/>
        public bool IsDefault
        {
            get
            {
                return this.isDefaultField;
            }
            set
            {
                this.isDefaultField = value;
            }
        }

        /// <remarks/>
        public bool IsReportingEntity
        {
            get
            {
                return this.isReportingEntityField;
            }
            set
            {
                this.isReportingEntityField = value;
            }
        }

        /// <remarks/>
        public System.DateTime ModifiedDate
        {
            get
            {
                return this.modifiedDateField;
            }
            set
            {
                this.modifiedDateField = value;
            }
        }

        /// <remarks/>
        public int ModifiedUserId
        {
            get
            {
                return this.modifiedUserIdField;
            }
            set
            {
                this.modifiedUserIdField = value;
            }
        }

        /// <remarks/>
        public int ParentId
        {
            get
            {
                return this.parentIdField;
            }
            set
            {
                this.parentIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime SSTEffDate
        {
            get
            {
                return this.sSTEffDateField;
            }
            set
            {
                this.sSTEffDateField = value;
            }
        }

        /// <remarks/>
        public string SSTPID
        {
            get
            {
                return this.sSTPIDField;
            }
            set
            {
                this.sSTPIDField = value;
            }
        }

        /// <remarks/>
        public string TIN
        {
            get
            {
                return this.tINField;
            }
            set
            {
                this.tINField = value;
            }
        }

        /// <remarks/>
        public string RegalBankId
        {
            get
            {
                return this.regalBankIdField;
            }
            set
            {
                this.regalBankIdField = value;
            }
        }

        /// <remarks/>
        public string DefaultCountry
        {
            get
            {
                return this.defaultCountryField;
            }
            set
            {
                this.defaultCountryField = value;
            }
        }

        /// <remarks/>
        public string BaseCurrencyCode
        {
            get
            {
                return this.baseCurrencyCodeField;
            }
            set
            {
                this.baseCurrencyCodeField = value;
            }
        }

        /// <remarks/>
        public RoundingLevelId RoundingLevelId
        {
            get
            {
                return this.roundingLevelIdField;
            }
            set
            {
                this.roundingLevelIdField = value;
            }
        }

        /// <remarks/>
        public bool CashBasisAccountingEnabled
        {
            get
            {
                return this.cashBasisAccountingEnabledField;
            }
            set
            {
                this.cashBasisAccountingEnabledField = value;
            }
        }

        /// <remarks/>
        public Companies Children
        {
            get
            {
                return this.childrenField;
            }
            set
            {
                this.childrenField = value;
            }
        }

        /// <remarks/>
        public CompanyContact[] Contacts
        {
            get
            {
                return this.contactsField;
            }
            set
            {
                this.contactsField = value;
            }
        }

        /// <remarks/>
        public Item[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        public Nexus[] Nexuses
        {
            get
            {
                return this.nexusesField;
            }
            set
            {
                this.nexusesField = value;
            }
        }

        /// <remarks/>
        public Company Parent
        {
            get
            {
                return this.parentField;
            }
            set
            {
                this.parentField = value;
            }
        }

        /// <remarks/>
        public TaxCode[] TaxCodes
        {
            get
            {
                return this.taxCodesField;
            }
            set
            {
                this.taxCodesField = value;
            }
        }

        /// <remarks/>
        public TaxRule[] TaxRules
        {
            get
            {
                return this.taxRulesField;
            }
            set
            {
                this.taxRulesField = value;
            }
        }

        /// <remarks/>
        public CompanyReturn[] FilingCalendars
        {
            get
            {
                return this.filingCalendarsField;
            }
            set
            {
                this.filingCalendarsField = value;
            }
        }

        /// <remarks/>
        public bool WarningsEnabled
        {
            get
            {
                return this.warningsEnabledField;
            }
            set
            {
                this.warningsEnabledField = value;
            }
        }

        /// <remarks/>
        public bool IsTest
        {
            get
            {
                return this.isTestField;
            }
            set
            {
                this.isTestField = value;
            }
        }

        /// <remarks/>
        public TaxDependencyLevelId TaxDependencyLevelId
        {
            get
            {
                return this.taxDependencyLevelIdField;
            }
            set
            {
                this.taxDependencyLevelIdField = value;
            }
        }

        /// <remarks/>
        public bool InProgress
        {
            get
            {
                return this.inProgressField;
            }
            set
            {
                this.inProgressField = value;
            }
        }

        /// <remarks/>
        public int DefaultLocationId
        {
            get
            {
                return this.defaultLocationIdField;
            }
            set
            {
                this.defaultLocationIdField = value;
            }
        }

        /// <remarks/>
        public string BusinessIdentificationNo
        {
            get
            {
                return this.businessIdentificationNoField;
            }
            set
            {
                this.businessIdentificationNoField = value;
            }
        }

        internal void CopyFrom(ProxyCompany SvcResult)
        {
            accountIdField = SvcResult.AccountId;
            companyIdField = SvcResult.CompanyId;
            companyCodeField = SvcResult.CompanyCode;
            companyNameField = SvcResult.CompanyName;
            createdDateField = SvcResult.CreatedDate;
            createdUserIdField = SvcResult.CreatedUserId;
            entityNoField = SvcResult.EntityNo;
            hasProfileField = SvcResult.HasProfile;
            isActiveField = SvcResult.IsActive;
            isDefaultField = SvcResult.IsDefault;
            isReportingEntityField = SvcResult.IsReportingEntity;
            modifiedDateField = SvcResult.ModifiedDate;
            modifiedUserIdField = SvcResult.ModifiedUserId;
            parentIdField = SvcResult.ParentId;
            sSTEffDateField = SvcResult.SSTEffDate;
            sSTPIDField = SvcResult.SSTPID;
            tINField = SvcResult.TIN;
            regalBankIdField = SvcResult.RegalBankId;
            defaultCountryField = SvcResult.DefaultCountry;
            baseCurrencyCodeField = SvcResult.BaseCurrencyCode;
            roundingLevelIdField = (RoundingLevelId)SvcResult.RoundingLevelId;
            cashBasisAccountingEnabledField = SvcResult.CashBasisAccountingEnabled;

          //for (int Index = 0; Index < SvcResult.Children.Length; Index++)
          //{
          //    ProxyCompany Svcchildren = SvcResult.Children[Index];
          //    Company localCompany = new Company();
          //    localCompany.CopyFrom(Svcchildren);
          //    childrenField.Add(localCompany);
          //}

          //ram - pending to implement
          //for (int Index = 0; Index < SvcResult.Contacts.Length; Index++)
          //{
          //    ProxyCompanyContact SvccContacts = SvcResult.Contacts[Index];
          //    CompanyContact localCompanyContact = new CompanyContact();
          //    localCompanyContact.CopyFrom(SvccContacts);
          //    contactsField.Add(localCompanyContact);
          //}

            //itemsField = (Item[])SvcResult.Items;
            //nexusesField = SvcResult.Nexuses;
            //parentField = (Company)SvcResult.Parent;
            //taxCodesField = SvcResult.TaxCodes;
            //taxRulesField = SvcResult.TaxRules;
            //filingCalendarsField = SvcResult.FilingCalendars;

          warningsEnabledField = SvcResult.WarningsEnabled;
          isTestField = SvcResult.IsTest;
          taxDependencyLevelIdField = (TaxDependencyLevelId)SvcResult.TaxDependencyLevelId;
          inProgressField = SvcResult.InProgress;
          defaultLocationIdField = SvcResult.DefaultLocationId;
          businessIdentificationNoField = SvcResult.BusinessIdentificationNo;
        }
    }

    /// <include file='AccountSvc.Doc.xml' path='adapter/Certificates/class/*' />
    [Guid("29931F0B-BFC7-42c4-B55E-F28332C5DCAC")]
    [ComVisible(true)]
    public class Companies : ReadOnlyArrayList
    {
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal Companies() { }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/collection/members[@name="this"]/*' />
        [DispId(0)]
        public new Company this[int index]
        {
            get
            {
                return (Company)base[index];
            }
        }
    }

    /// <remarks/>
    [Guid("29931F1B-BFC8-42c4-B55E-F28338C5DCAC")]
    [ComVisible(true)]
    public enum RoundingLevelId
    {

        /// <remarks/>
        Line,

        /// <remarks/>
        Document,
    }

    /// <remarks/>
    [Guid("29931F1B-BFC8-42c4-B55E-F28332C5DC1C")]
    [ComVisible(true)]
    public class CompanyContact
    {

        private string cityField;

        private int companyIdField;

        private string companyContactCodeField;

        private int companyContactIdField;

        private System.DateTime createdDateField;

        private int createdUserIdField;

        private string emailField;

        private string faxField;

        private string firstNameField;

        private string lastNameField;

        private string countryField;

        private string line1Field;

        private string line2Field;

        private string line3Field;

        private string middleNameField;

        private System.DateTime modifiedDateField;

        private int modifiedUserIdField;

        private string phoneField;

        private string phone2Field;

        private string postalCodeField;

        private string regionField;

        private string titleField;

        private Company companyField;

        /// <remarks/>
        public string City
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }

        /// <remarks/>
        public int CompanyId
        {
            get
            {
                return this.companyIdField;
            }
            set
            {
                this.companyIdField = value;
            }
        }

        /// <remarks/>
        public string CompanyContactCode
        {
            get
            {
                return this.companyContactCodeField;
            }
            set
            {
                this.companyContactCodeField = value;
            }
        }

        /// <remarks/>
        public int CompanyContactId
        {
            get
            {
                return this.companyContactIdField;
            }
            set
            {
                this.companyContactIdField = value;
            }
        }

        /// <remarks/>
        public System.DateTime CreatedDate
        {
            get
            {
                return this.createdDateField;
            }
            set
            {
                this.createdDateField = value;
            }
        }

        /// <remarks/>
        public int CreatedUserId
        {
            get
            {
                return this.createdUserIdField;
            }
            set
            {
                this.createdUserIdField = value;
            }
        }

        /// <remarks/>
        public string Email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }

        /// <remarks/>
        public string Fax
        {
            get
            {
                return this.faxField;
            }
            set
            {
                this.faxField = value;
            }
        }

        /// <remarks/>
        public string FirstName
        {
            get
            {
                return this.firstNameField;
            }
            set
            {
                this.firstNameField = value;
            }
        }

        /// <remarks/>
        public string LastName
        {
            get
            {
                return this.lastNameField;
            }
            set
            {
                this.lastNameField = value;
            }
        }

        /// <remarks/>
        public string Country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }

        /// <remarks/>
        public string Line1
        {
            get
            {
                return this.line1Field;
            }
            set
            {
                this.line1Field = value;
            }
        }

        /// <remarks/>
        public string Line2
        {
            get
            {
                return this.line2Field;
            }
            set
            {
                this.line2Field = value;
            }
        }

        /// <remarks/>
        public string Line3
        {
            get
            {
                return this.line3Field;
            }
            set
            {
                this.line3Field = value;
            }
        }

        /// <remarks/>
        public string MiddleName
        {
            get
            {
                return this.middleNameField;
            }
            set
            {
                this.middleNameField = value;
            }
        }

        /// <remarks/>
        public System.DateTime ModifiedDate
        {
            get
            {
                return this.modifiedDateField;
            }
            set
            {
                this.modifiedDateField = value;
            }
        }

        /// <remarks/>
        public int ModifiedUserId
        {
            get
            {
                return this.modifiedUserIdField;
            }
            set
            {
                this.modifiedUserIdField = value;
            }
        }

        /// <remarks/>
        public string Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }

        /// <remarks/>
        public string Phone2
        {
            get
            {
                return this.phone2Field;
            }
            set
            {
                this.phone2Field = value;
            }
        }

        /// <remarks/>
        public string PostalCode
        {
            get
            {
                return this.postalCodeField;
            }
            set
            {
                this.postalCodeField = value;
            }
        }

        /// <remarks/>
        public string Region
        {
            get
            {
                return this.regionField;
            }
            set
            {
                this.regionField = value;
            }
        }

        /// <remarks/>
        public string Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        public Company Company
        {
            get
            {
                return this.companyField;
            }
            set
            {
                this.companyField = value;
            }
        }
    }

    /// <remarks/>
    [Guid("29931F1B-BFC8-42c4-B56E-F28332C5DC1C")]
    [ComVisible(true)]
    public class Item
    {

        private long itemIdField;

        private string itemCodeField;

        private int companyIdField;

        private System.DateTime createdDateField;

        private int createdUserIdField;

        private int taxCodeIdField;

        private string descriptionField;

        private System.DateTime modifiedDateField;

        private int modifiedUserIdField;

        private Company companyField;

        private TaxCode taxCodeField;

        /// <remarks/>
        public long ItemId
        {
            get
            {
                return this.itemIdField;
            }
            set
            {
                this.itemIdField = value;
            }
        }

        /// <remarks/>
        public string ItemCode
        {
            get
            {
                return this.itemCodeField;
            }
            set
            {
                this.itemCodeField = value;
            }
        }

        /// <remarks/>
        public int CompanyId
        {
            get
            {
                return this.companyIdField;
            }
            set
            {
                this.companyIdField = value;
            }
        }

        /// <remarks/>
        public System.DateTime CreatedDate
        {
            get
            {
                return this.createdDateField;
            }
            set
            {
                this.createdDateField = value;
            }
        }

        /// <remarks/>
        public int CreatedUserId
        {
            get
            {
                return this.createdUserIdField;
            }
            set
            {
                this.createdUserIdField = value;
            }
        }

        /// <remarks/>
        public int TaxCodeId
        {
            get
            {
                return this.taxCodeIdField;
            }
            set
            {
                this.taxCodeIdField = value;
            }
        }

        /// <remarks/>
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public System.DateTime ModifiedDate
        {
            get
            {
                return this.modifiedDateField;
            }
            set
            {
                this.modifiedDateField = value;
            }
        }

        /// <remarks/>
        public int ModifiedUserId
        {
            get
            {
                return this.modifiedUserIdField;
            }
            set
            {
                this.modifiedUserIdField = value;
            }
        }

        /// <remarks/>
        public Company Company
        {
            get
            {
                return this.companyField;
            }
            set
            {
                this.companyField = value;
            }
        }

        /// <remarks/>
        public TaxCode TaxCode
        {
            get
            {
                return this.taxCodeField;
            }
            set
            {
                this.taxCodeField = value;
            }
        }
    }

    /// <remarks/>
    [Guid("29931F1B-BFC8-42c4-B55D-F28332C5DC1C")]
    [ComVisible(true)]
    public class TaxCode
    {

        private int taxCodeIdField;

        private string taxCodeValueField;

        private string taxCodeTypeIdField;

        private int companyIdField;

        private string descriptionField;

        private bool isPhysicalField;

        private System.DateTime createdDateField;

        private int createdUserIdField;

        private System.DateTime modifiedDateField;

        private int modifiedUserIdField;

        private string parentTaxCodeField;

        private bool isActiveField;

        private bool isSstCertifiedField;

        /// <remarks/>
        public int TaxCodeId
        {
            get
            {
                return this.taxCodeIdField;
            }
            set
            {
                this.taxCodeIdField = value;
            }
        }

        /// <remarks/>
        public string TaxCodeValue
        {
            get
            {
                return this.taxCodeValueField;
            }
            set
            {
                this.taxCodeValueField = value;
            }
        }

        /// <remarks/>
        public string TaxCodeTypeId
        {
            get
            {
                return this.taxCodeTypeIdField;
            }
            set
            {
                this.taxCodeTypeIdField = value;
            }
        }

        /// <remarks/>
        public int CompanyId
        {
            get
            {
                return this.companyIdField;
            }
            set
            {
                this.companyIdField = value;
            }
        }

        /// <remarks/>
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public bool IsPhysical
        {
            get
            {
                return this.isPhysicalField;
            }
            set
            {
                this.isPhysicalField = value;
            }
        }

        /// <remarks/>
        public System.DateTime CreatedDate
        {
            get
            {
                return this.createdDateField;
            }
            set
            {
                this.createdDateField = value;
            }
        }

        /// <remarks/>
        public int CreatedUserId
        {
            get
            {
                return this.createdUserIdField;
            }
            set
            {
                this.createdUserIdField = value;
            }
        }

        /// <remarks/>
        public System.DateTime ModifiedDate
        {
            get
            {
                return this.modifiedDateField;
            }
            set
            {
                this.modifiedDateField = value;
            }
        }

        /// <remarks/>
        public int ModifiedUserId
        {
            get
            {
                return this.modifiedUserIdField;
            }
            set
            {
                this.modifiedUserIdField = value;
            }
        }

        /// <remarks/>
        public string ParentTaxCode
        {
            get
            {
                return this.parentTaxCodeField;
            }
            set
            {
                this.parentTaxCodeField = value;
            }
        }

        /// <remarks/>
        public bool IsActive
        {
            get
            {
                return this.isActiveField;
            }
            set
            {
                this.isActiveField = value;
            }
        }

        /// <remarks/>
        public bool IsSstCertified
        {
            get
            {
                return this.isSstCertifiedField;
            }
            set
            {
                this.isSstCertifiedField = value;
            }
        }
    }

    /// <remarks/>
    [Guid("29931F1B-BFC8-42c4-B55D-F28332C5EC1C")]
    [ComVisible(true)]
    public class Nexus
    {

        private int nexusIdField;

        private int companyIdField;

        private string countryField;

        private string stateField;

        private JurisTypeId jurisTypeIdField;

        private string jurisCodeField;

        private string jurisNameField;

        private string shortNameField;

        private System.DateTime effDateField;

        private System.DateTime endDateField;

        private System.DateTime createdDateField;

        private int createdUserIdField;

        private System.DateTime modifiedDateField;

        private int modifiedUserIdField;

        private NexusTypeId nexusTypeIdField;

        private int accountingMethodIdField;

        private bool hasLocalNexusField;

        private string sourcingField;

        private LocalNexusTypeId localNexusTypeIdField;

        /// <remarks/>
        public int NexusId
        {
            get
            {
                return this.nexusIdField;
            }
            set
            {
                this.nexusIdField = value;
            }
        }

        /// <remarks/>
        public int CompanyId
        {
            get
            {
                return this.companyIdField;
            }
            set
            {
                this.companyIdField = value;
            }
        }

        /// <remarks/>
        public string Country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }

        /// <remarks/>
        public string State
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }

        /// <remarks/>
        public JurisTypeId JurisTypeId
        {
            get
            {
                return this.jurisTypeIdField;
            }
            set
            {
                this.jurisTypeIdField = value;
            }
        }

        /// <remarks/>
        public string JurisCode
        {
            get
            {
                return this.jurisCodeField;
            }
            set
            {
                this.jurisCodeField = value;
            }
        }

        /// <remarks/>
        public string JurisName
        {
            get
            {
                return this.jurisNameField;
            }
            set
            {
                this.jurisNameField = value;
            }
        }

        /// <remarks/>
        public string ShortName
        {
            get
            {
                return this.shortNameField;
            }
            set
            {
                this.shortNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime EffDate
        {
            get
            {
                return this.effDateField;
            }
            set
            {
                this.effDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime EndDate
        {
            get
            {
                return this.endDateField;
            }
            set
            {
                this.endDateField = value;
            }
        }

        /// <remarks/>
        public System.DateTime CreatedDate
        {
            get
            {
                return this.createdDateField;
            }
            set
            {
                this.createdDateField = value;
            }
        }

        /// <remarks/>
        public int CreatedUserId
        {
            get
            {
                return this.createdUserIdField;
            }
            set
            {
                this.createdUserIdField = value;
            }
        }

        /// <remarks/>
        public System.DateTime ModifiedDate
        {
            get
            {
                return this.modifiedDateField;
            }
            set
            {
                this.modifiedDateField = value;
            }
        }

        /// <remarks/>
        public int ModifiedUserId
        {
            get
            {
                return this.modifiedUserIdField;
            }
            set
            {
                this.modifiedUserIdField = value;
            }
        }

        /// <remarks/>
        public NexusTypeId NexusTypeId
        {
            get
            {
                return this.nexusTypeIdField;
            }
            set
            {
                this.nexusTypeIdField = value;
            }
        }

        /// <remarks/>
        public int AccountingMethodId
        {
            get
            {
                return this.accountingMethodIdField;
            }
            set
            {
                this.accountingMethodIdField = value;
            }
        }

        /// <remarks/>
        public bool HasLocalNexus
        {
            get
            {
                return this.hasLocalNexusField;
            }
            set
            {
                this.hasLocalNexusField = value;
            }
        }

        /// <remarks/>
        public string Sourcing
        {
            get
            {
                return this.sourcingField;
            }
            set
            {
                this.sourcingField = value;
            }
        }

        /// <remarks/>
        public LocalNexusTypeId LocalNexusTypeId
        {
            get
            {
                return this.localNexusTypeIdField;
            }
            set
            {
                this.localNexusTypeIdField = value;
            }
        }

        internal void CopyFrom(ProxyNexus SvcResult)
        {
         nexusIdField = SvcResult.NexusId;
         companyIdField = SvcResult.CompanyId;
         countryField = SvcResult.Country;
         stateField = SvcResult.State;
         jurisTypeIdField = (JurisTypeId)SvcResult.JurisTypeId;
         jurisCodeField = SvcResult.JurisCode;
         jurisNameField = SvcResult.JurisName;
         shortNameField = SvcResult.ShortName;
          effDateField = SvcResult.EffDate;
          endDateField = SvcResult.EndDate;
          createdDateField = SvcResult.CreatedDate;
          createdUserIdField = SvcResult.CreatedUserId;
          modifiedDateField = SvcResult.ModifiedDate;
          modifiedUserIdField = SvcResult.ModifiedUserId;
          nexusTypeIdField = (NexusTypeId)SvcResult.NexusTypeId;
          accountingMethodIdField = SvcResult.AccountingMethodId;
          hasLocalNexusField = SvcResult.HasLocalNexus;
          sourcingField = SvcResult.Sourcing;
          localNexusTypeIdField = (LocalNexusTypeId)SvcResult.LocalNexusTypeId;
        }

    }

    /// <include file='AccountSvc.Doc.xml' path='adapter/Certificates/class/*' />
    [Guid("29931F0B-BFC7-42c4-B55E-F23682C5DCAC")]
    [ComVisible(true)]
    public class Nexuses : ReadOnlyArrayList
    {
        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal Nexuses() { }

        /// <include file='AvaCert2Svc.Doc.xml' path='adapter/collection/members[@name="this"]/*' />
        [DispId(0)]
        public new Nexus this[int index]
        {
            get
            {
                return (Nexus)base[index];
            }
        }
    }

    /// <remarks/>
    [Guid("29931F1B-BFC8-42c4-C55D-F28332C5DC1C")]
    [ComVisible(true)]
    public enum NexusTypeId
    {

        /// <remarks/>
        None,

        /// <remarks/>
        Volunteer,

        /// <remarks/>
        NonVolunteer,

        /// <remarks/>
        SSTVolunteer,

        /// <remarks/>
        SSTNonVolunteer,

        /// <remarks/>
        Collect,

        /// <remarks/>
        Legal,
    }

    /// <remarks/>
    [Guid("29931F1B-BFC8-42D4-B55D-F28332C5DC1C")]
    [ComVisible(true)]
    public enum LocalNexusTypeId
    {

        /// <remarks/>
        Selected,

        /// <remarks/>
        StateAdministered,

        /// <remarks/>
        All,
    }

    /// <remarks/>
    [Guid("20931F1B-BFC8-42D4-B55D-F28332C6DC1C")]
    [ComVisible(true)]
    public enum JurisTypeId
    {

        /// <remarks/>
        STA,

        /// <remarks/>
        CTY,

        /// <remarks/>
        CIT,

        /// <remarks/>
        STJ,

        /// <remarks/>
        CNT,
    }

    /// <remarks/>
    [Guid("20031F1B-BFC8-42c4-B55D-F28332C5DC0C")]
    [ComVisible(true)]
    public enum TaxDependencyLevelId
    {

        /// <remarks/>
        Document,

        /// <remarks/>
        State,

        /// <remarks/>
        TaxRegion,

        /// <remarks/>
        Address,
    }

    /// <remarks/>
    [Guid("20931D1B-BFC8-42c4-B55D-F28332D5DC1C")]
    [ComVisible(true)]
    public class CompanyReturn
    {

        private int yearStartField;

        private long companyReturnIdField;

        private CompanySupportingReturn[] companySupportingReturnField;

        private int companyIdField;

        private string companyNameField;

        private string returnNameField;

        private FilingFrequencyId filingFrequencyIdField;

        private short monthsField;

        private string registrationIdField;

        private string einField;

        private string line1Field;

        private string line2Field;

        private string cityField;

        private string regionField;

        private string postalCodeField;

        private string countryField;

        private string phoneField;

        private string descriptionField;

        private string legalEntityNameField;

        private System.DateTime effDateField;

        private System.DateTime endDateField;

        private int createdUserIdField;

        private System.DateTime createdDateField;

        private int modifiedUserIdField;

        private System.DateTime modifiedDateField;

        private int filingCalendarIdField;

        private FilingTypeId filingTypeIdField;

        private string efilePasswordField;

        private byte prepayPercentageField;

        private string taxTypeIdField;

        private string noteField;

        private string alSignOnField;

        private string alAccessCodeField;

        private string meBusinessCodeField;

        private string iaBenField;

        private string ctRegField;

        private string other1NameField;

        private string other1ValueField;

        private string other2NameField;

        private string other2ValueField;

        private string other3NameField;

        private string other3ValueField;

        private string locationCodeField;

        private OutletTypeId outletTypeIdField;

        /// <remarks/>
        public int YearStart
        {
            get
            {
                return this.yearStartField;
            }
            set
            {
                this.yearStartField = value;
            }
        }

        /// <remarks/>
        public long CompanyReturnId
        {
            get
            {
                return this.companyReturnIdField;
            }
            set
            {
                this.companyReturnIdField = value;
            }
        }

        /// <remarks/>
        public CompanySupportingReturn[] CompanySupportingReturn
        {
            get
            {
                return this.companySupportingReturnField;
            }
            set
            {
                this.companySupportingReturnField = value;
            }
        }

        /// <remarks/>
        public int CompanyId
        {
            get
            {
                return this.companyIdField;
            }
            set
            {
                this.companyIdField = value;
            }
        }

        /// <remarks/>
        public string CompanyName
        {
            get
            {
                return this.companyNameField;
            }
            set
            {
                this.companyNameField = value;
            }
        }

        /// <remarks/>
        public string ReturnName
        {
            get
            {
                return this.returnNameField;
            }
            set
            {
                this.returnNameField = value;
            }
        }

        /// <remarks/>
        public FilingFrequencyId FilingFrequencyId
        {
            get
            {
                return this.filingFrequencyIdField;
            }
            set
            {
                this.filingFrequencyIdField = value;
            }
        }

        /// <remarks/>
        public short Months
        {
            get
            {
                return this.monthsField;
            }
            set
            {
                this.monthsField = value;
            }
        }

        /// <remarks/>
        public string RegistrationId
        {
            get
            {
                return this.registrationIdField;
            }
            set
            {
                this.registrationIdField = value;
            }
        }

        /// <remarks/>
        public string Ein
        {
            get
            {
                return this.einField;
            }
            set
            {
                this.einField = value;
            }
        }

        /// <remarks/>
        public string Line1
        {
            get
            {
                return this.line1Field;
            }
            set
            {
                this.line1Field = value;
            }
        }

        /// <remarks/>
        public string Line2
        {
            get
            {
                return this.line2Field;
            }
            set
            {
                this.line2Field = value;
            }
        }

        /// <remarks/>
        public string City
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }

        /// <remarks/>
        public string Region
        {
            get
            {
                return this.regionField;
            }
            set
            {
                this.regionField = value;
            }
        }

        /// <remarks/>
        public string PostalCode
        {
            get
            {
                return this.postalCodeField;
            }
            set
            {
                this.postalCodeField = value;
            }
        }

        /// <remarks/>
        public string Country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }

        /// <remarks/>
        public string Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }

        /// <remarks/>
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public string LegalEntityName
        {
            get
            {
                return this.legalEntityNameField;
            }
            set
            {
                this.legalEntityNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime EffDate
        {
            get
            {
                return this.effDateField;
            }
            set
            {
                this.effDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime EndDate
        {
            get
            {
                return this.endDateField;
            }
            set
            {
                this.endDateField = value;
            }
        }

        /// <remarks/>
        public int CreatedUserId
        {
            get
            {
                return this.createdUserIdField;
            }
            set
            {
                this.createdUserIdField = value;
            }
        }

        /// <remarks/>
        public System.DateTime CreatedDate
        {
            get
            {
                return this.createdDateField;
            }
            set
            {
                this.createdDateField = value;
            }
        }

        /// <remarks/>
        public int ModifiedUserId
        {
            get
            {
                return this.modifiedUserIdField;
            }
            set
            {
                this.modifiedUserIdField = value;
            }
        }

        /// <remarks/>
        public System.DateTime ModifiedDate
        {
            get
            {
                return this.modifiedDateField;
            }
            set
            {
                this.modifiedDateField = value;
            }
        }

        /// <remarks/>
        public int FilingCalendarId
        {
            get
            {
                return this.filingCalendarIdField;
            }
            set
            {
                this.filingCalendarIdField = value;
            }
        }

        /// <remarks/>
        public FilingTypeId FilingTypeId
        {
            get
            {
                return this.filingTypeIdField;
            }
            set
            {
                this.filingTypeIdField = value;
            }
        }

        /// <remarks/>
        public string EfilePassword
        {
            get
            {
                return this.efilePasswordField;
            }
            set
            {
                this.efilePasswordField = value;
            }
        }

        /// <remarks/>
        public byte PrepayPercentage
        {
            get
            {
                return this.prepayPercentageField;
            }
            set
            {
                this.prepayPercentageField = value;
            }
        }

        /// <remarks/>
        public string TaxTypeId
        {
            get
            {
                return this.taxTypeIdField;
            }
            set
            {
                this.taxTypeIdField = value;
            }
        }

        /// <remarks/>
        public string Note
        {
            get
            {
                return this.noteField;
            }
            set
            {
                this.noteField = value;
            }
        }

        /// <remarks/>
        public string AlSignOn
        {
            get
            {
                return this.alSignOnField;
            }
            set
            {
                this.alSignOnField = value;
            }
        }

        /// <remarks/>
        public string AlAccessCode
        {
            get
            {
                return this.alAccessCodeField;
            }
            set
            {
                this.alAccessCodeField = value;
            }
        }

        /// <remarks/>
        public string MeBusinessCode
        {
            get
            {
                return this.meBusinessCodeField;
            }
            set
            {
                this.meBusinessCodeField = value;
            }
        }

        /// <remarks/>
        public string IaBen
        {
            get
            {
                return this.iaBenField;
            }
            set
            {
                this.iaBenField = value;
            }
        }

        /// <remarks/>
        public string CtReg
        {
            get
            {
                return this.ctRegField;
            }
            set
            {
                this.ctRegField = value;
            }
        }

        /// <remarks/>
        public string Other1Name
        {
            get
            {
                return this.other1NameField;
            }
            set
            {
                this.other1NameField = value;
            }
        }

        /// <remarks/>
        public string Other1Value
        {
            get
            {
                return this.other1ValueField;
            }
            set
            {
                this.other1ValueField = value;
            }
        }

        /// <remarks/>
        public string Other2Name
        {
            get
            {
                return this.other2NameField;
            }
            set
            {
                this.other2NameField = value;
            }
        }

        /// <remarks/>
        public string Other2Value
        {
            get
            {
                return this.other2ValueField;
            }
            set
            {
                this.other2ValueField = value;
            }
        }

        /// <remarks/>
        public string Other3Name
        {
            get
            {
                return this.other3NameField;
            }
            set
            {
                this.other3NameField = value;
            }
        }

        /// <remarks/>
        public string Other3Value
        {
            get
            {
                return this.other3ValueField;
            }
            set
            {
                this.other3ValueField = value;
            }
        }

        /// <remarks/>
        public string LocationCode
        {
            get
            {
                return this.locationCodeField;
            }
            set
            {
                this.locationCodeField = value;
            }
        }

        /// <remarks/>
        public OutletTypeId OutletTypeId
        {
            get
            {
                return this.outletTypeIdField;
            }
            set
            {
                this.outletTypeIdField = value;
            }
        }
    }

    /// <remarks/>
    [Guid("20931D1B-BFC8-49c4-B55D-F28332D5DC1C")]
    [ComVisible(true)]
    public enum FilingFrequencyId
    {

        /// <remarks/>
        Monthly,

        /// <remarks/>
        Quarterly,

        /// <remarks/>
        SemiAnnually,

        /// <remarks/>
        Annually,

        /// <remarks/>
        Bimonthly,

        /// <remarks/>
        Occasional,
    }

    /// <remarks/>
    [Guid("20931F1B-BFC8-42D4-B55D-F28332C5DC1C")]
    [ComVisible(true)]
    public enum FilingTypeId
    {

        /// <remarks/>
        PaperReturn,

        /// <remarks/>
        ElectronicReturn,

        /// <remarks/>
        SER,

        /// <remarks/>
        EFTPaper,

        /// <remarks/>
        PhonePaper,

        /// <remarks/>
        SignatureReady,

        /// <remarks/>
        EfileCheck,
    }

    /// <remarks/>
    [Guid("20991F1B-BFC8-42c4-B55D-F28332C5DC1C")]
    [ComVisible(true)]
    public enum OutletTypeId
    {

        /// <remarks/>
        None,

        /// <remarks/>
        Schedule,

        /// <remarks/>
        Duplicate,
    }

    /// <remarks/>
    [Guid("20931F1D-BFC8-42c4-B55D-F28332C5DC1C")]
    [ComVisible(true)]
    public enum TaxTypeId
    {

        /// <remarks/>
        B,

        /// <remarks/>
        C,

        /// <remarks/>
        S,

        /// <remarks/>
        U,

        /// <remarks/>
        O,

        /// <remarks/>
        I,

        /// <remarks/>
        N,

        /// <remarks/>
        R,

        /// <remarks/>
        F,

        /// <remarks/>
        E,
    }

    /// <remarks/>
    [Guid("20931F1B-BFD8-42c4-B55D-F28332C5DC1C")]
    [ComVisible(true)]
    public enum TaxRuleTypeId
    {

        /// <remarks/>
        RateRule,

        /// <remarks/>
        RateOverrideRule,

        /// <remarks/>
        BaseRule,

        /// <remarks/>
        ExemptEntityRule,

        /// <remarks/>
        ProductTaxabilityRule,

        /// <remarks/>
        NexusRule,
    }

    /// <remarks/>
    [Guid("20931F1B-BFC8-42c4-B65D-F28332C5DC1C")]
    [ComVisible(true)]
    public class TaxRule
    {

        private int taxRuleIdField;

        private int companyIdField;

        private string stateField;

        private string stateCodeField;

        private string countyCodeField;

        private JurisTypeId jurisTypeIdField;

        private string jurisCodeField;

        private string jurisNameField;

        private int taxCodeIdField;

        private string customerUsageTypeField;

        private TaxTypeId taxTypeIdField;

        private TaxRuleTypeId taxRuleTypeIdField;

        private bool isAllJurisField;

        private decimal valueField;

        private decimal capField;

        private decimal thresholdField;

        private string optionsField;

        private System.DateTime effDateField;

        private System.DateTime endDateField;

        private string descriptionField;

        private System.DateTime createdDateField;

        private int createdUserIdField;

        private System.DateTime modifiedDateField;

        private int modifiedUserIdField;

        private bool isStProField;

        private string rateTypeIdField;

        private Company companyField;

        private TaxCode taxCodeField;

        private string countryField;

        private string sourcingField;

        /// <remarks/>
        public int TaxRuleId
        {
            get
            {
                return this.taxRuleIdField;
            }
            set
            {
                this.taxRuleIdField = value;
            }
        }

        /// <remarks/>
        public int CompanyId
        {
            get
            {
                return this.companyIdField;
            }
            set
            {
                this.companyIdField = value;
            }
        }

        /// <remarks/>
        public string State
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }

        /// <remarks/>
        public string StateCode
        {
            get
            {
                return this.stateCodeField;
            }
            set
            {
                this.stateCodeField = value;
            }
        }

        /// <remarks/>
        public string CountyCode
        {
            get
            {
                return this.countyCodeField;
            }
            set
            {
                this.countyCodeField = value;
            }
        }

        /// <remarks/>
        public JurisTypeId JurisTypeId
        {
            get
            {
                return this.jurisTypeIdField;
            }
            set
            {
                this.jurisTypeIdField = value;
            }
        }

        /// <remarks/>
        public string JurisCode
        {
            get
            {
                return this.jurisCodeField;
            }
            set
            {
                this.jurisCodeField = value;
            }
        }

        /// <remarks/>
        public string JurisName
        {
            get
            {
                return this.jurisNameField;
            }
            set
            {
                this.jurisNameField = value;
            }
        }

        /// <remarks/>
        public int TaxCodeId
        {
            get
            {
                return this.taxCodeIdField;
            }
            set
            {
                this.taxCodeIdField = value;
            }
        }

        /// <remarks/>
        public string CustomerUsageType
        {
            get
            {
                return this.customerUsageTypeField;
            }
            set
            {
                this.customerUsageTypeField = value;
            }
        }

        /// <remarks/>
        public TaxTypeId TaxTypeId
        {
            get
            {
                return this.taxTypeIdField;
            }
            set
            {
                this.taxTypeIdField = value;
            }
        }

        /// <remarks/>
        public TaxRuleTypeId TaxRuleTypeId
        {
            get
            {
                return this.taxRuleTypeIdField;
            }
            set
            {
                this.taxRuleTypeIdField = value;
            }
        }

        /// <remarks/>
        public bool IsAllJuris
        {
            get
            {
                return this.isAllJurisField;
            }
            set
            {
                this.isAllJurisField = value;
            }
        }

        /// <remarks/>
        public decimal Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        public decimal Cap
        {
            get
            {
                return this.capField;
            }
            set
            {
                this.capField = value;
            }
        }

        /// <remarks/>
        public decimal Threshold
        {
            get
            {
                return this.thresholdField;
            }
            set
            {
                this.thresholdField = value;
            }
        }

        /// <remarks/>
        public string Options
        {
            get
            {
                return this.optionsField;
            }
            set
            {
                this.optionsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime EffDate
        {
            get
            {
                return this.effDateField;
            }
            set
            {
                this.effDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime EndDate
        {
            get
            {
                return this.endDateField;
            }
            set
            {
                this.endDateField = value;
            }
        }

        /// <remarks/>
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public System.DateTime CreatedDate
        {
            get
            {
                return this.createdDateField;
            }
            set
            {
                this.createdDateField = value;
            }
        }

        /// <remarks/>
        public int CreatedUserId
        {
            get
            {
                return this.createdUserIdField;
            }
            set
            {
                this.createdUserIdField = value;
            }
        }

        /// <remarks/>
        public System.DateTime ModifiedDate
        {
            get
            {
                return this.modifiedDateField;
            }
            set
            {
                this.modifiedDateField = value;
            }
        }

        /// <remarks/>
        public int ModifiedUserId
        {
            get
            {
                return this.modifiedUserIdField;
            }
            set
            {
                this.modifiedUserIdField = value;
            }
        }

        /// <remarks/>
        public bool IsStPro
        {
            get
            {
                return this.isStProField;
            }
            set
            {
                this.isStProField = value;
            }
        }

        /// <remarks/>
        public string RateTypeId
        {
            get
            {
                return this.rateTypeIdField;
            }
            set
            {
                this.rateTypeIdField = value;
            }
        }

        /// <remarks/>
        public Company Company
        {
            get
            {
                return this.companyField;
            }
            set
            {
                this.companyField = value;
            }
        }

        /// <remarks/>
        public TaxCode TaxCode
        {
            get
            {
                return this.taxCodeField;
            }
            set
            {
                this.taxCodeField = value;
            }
        }

        /// <remarks/>
        public string Country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }

        /// <remarks/>
        public string Sourcing
        {
            get
            {
                return this.sourcingField;
            }
            set
            {
                this.sourcingField = value;
            }
        }
    }

    /// <remarks/>
    [Guid("20931F1B-BFC8-42c4-B55D-F29332C5DC1C")]
    [ComVisible(true)]
    public class CompanySupportingReturn
    {

        private long companyReturnIdField;

        private int companySupportingReturnIdField;

        private string returnNameField;

        /// <remarks/>
        public long CompanyReturnId
        {
            get
            {
                return this.companyReturnIdField;
            }
            set
            {
                this.companyReturnIdField = value;
            }
        }

        /// <remarks/>
        public int CompanySupportingReturnId
        {
            get
            {
                return this.companySupportingReturnIdField;
            }
            set
            {
                this.companySupportingReturnIdField = value;
            }
        }

        /// <remarks/>
        public string ReturnName
        {
            get
            {
                return this.returnNameField;
            }
            set
            {
                this.returnNameField = value;
            }
        }
    }
 
    /// <remarks/>
    [Guid("20931F1B-BFC8-42c4-B55D-F28332C5DC1C")]
    [ComVisible(true)]
    public class CompanySetting
    {

        private int companySettingIdField;

        private int companyIdField;

        private string setField;

        private string nameField;

        private string valueField;

        /// <remarks/>
        public int CompanySettingId
        {
            get
            {
                return this.companySettingIdField;
            }
            set
            {
                this.companySettingIdField = value;
            }
        }

        /// <remarks/>
        public int CompanyId
        {
            get
            {
                return this.companyIdField;
            }
            set
            {
                this.companyIdField = value;
            }
        }

        /// <remarks/>
        public string Set
        {
            get
            {
                return this.setField;
            }
            set
            {
                this.setField = value;
            }
        }

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

}
