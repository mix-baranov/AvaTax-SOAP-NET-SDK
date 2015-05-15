using System;
using System.Configuration;
using System.IO;
using System.Text;
using Avalara.AvaTax.Services.Proxies.AccountSvcProxy;
using Avalara.AvaTax.Services.Proxies.AvaCert2SvcProxy;
using AccountProxy = Avalara.AvaTax.Services.Proxies.AccountSvcProxy;
using CustomerSaveRequest = Avalara.AvaTax.Adapter.AvaCert2Service.CustomerSaveRequest;
using CustomerSaveResult = Avalara.AvaTax.Adapter.AvaCert2Service.CustomerSaveResult;
using CertificateRequestInitiateRequest = Avalara.AvaTax.Adapter.AvaCert2Service.CertificateRequestInitiateRequest;
using CertificateRequestInitiateResult = Avalara.AvaTax.Adapter.AvaCert2Service.CertificateRequestInitiateResult;
using CertificateRequestGetRequest = Avalara.AvaTax.Adapter.AvaCert2Service.CertificateRequestGetRequest;
using CertificateRequestGetResult = Avalara.AvaTax.Adapter.AvaCert2Service.CertificateRequestGetResult;
using CertificateGetRequest = Avalara.AvaTax.Adapter.AvaCert2Service.CertificateGetRequest;
using CertificateGetResult = Avalara.AvaTax.Adapter.AvaCert2Service.CertificateGetResult;
using CertificateImageGetRequest = Avalara.AvaTax.Adapter.AvaCert2Service.CertificateImageGetRequest;
using CertificateImageGetResult = Avalara.AvaTax.Adapter.AvaCert2Service.CertificateImageGetResult;
using AvaCert2Svc = Avalara.AvaTax.Adapter.AvaCert2Service.AvaCert2Svc;
using BaseResult = Avalara.AvaTax.Adapter.BaseResult;
using PingResult = Avalara.AvaTax.Adapter.PingResult;
using Message = Avalara.AvaTax.Adapter.Message;
using SeverityLevel = Avalara.AvaTax.Adapter.SeverityLevel;
using Customer = Avalara.AvaTax.Adapter.AvaCert2Service.Customer;
using RequestType = Avalara.AvaTax.Adapter.AvaCert2Service.RequestType;
using FormatType = Avalara.AvaTax.Adapter.AvaCert2Service.FormatType;
using CommunicationMode = Avalara.AvaTax.Adapter.AvaCert2Service.CommunicationMode;
using CertificateReviewStatus = Avalara.AvaTax.Adapter.AvaCert2Service.ReviewStatus;
using CertificateRequestStatus = Avalara.AvaTax.Adapter.AvaCert2Service.CertificateRequestStatus;
using Certificate = Avalara.AvaTax.Adapter.AvaCert2Service.Certificate;
using CertificateRequest = Avalara.AvaTax.Adapter.AvaCert2Service.CertificateRequest;
using NUnit.Framework;

namespace Test.Avalara.AvaTax.Adapter
{
    /// <summary>
    /// Summary description for AdapterAvaCert2Test.
    /// </summary>
    [TestFixture]
    public class AdapterAvaCert2Test
    {
        private AvaCert2Svc avaCert2Svc;
        private AccountSvcWrapper accountSvc;
        private AvaCert2SvcWrapper avaCert2SvcProxy;
        private Company DefaultCompany;
        private int accountId;

        [SetUp]
        public void Init()
        {
            try
            {
                avaCert2Svc = new AvaCert2Svc();
                avaCert2Svc.Configuration.Url = ConfigurationSettings.AppSettings.Get("Url");

                //fill these only if they haven't been loaded from Avalara.AvaTax.Adapter.dll.config
                if (avaCert2Svc.Configuration.Security.Account == null || avaCert2Svc.Configuration.Security.Account.Length == 0)
                {
                    avaCert2Svc.Configuration.Security.Account = ConfigurationSettings.AppSettings.Get("account");
                }
                if (avaCert2Svc.Configuration.Security.License == null || avaCert2Svc.Configuration.Security.License.Length == 0)
                {
                    avaCert2Svc.Configuration.Security.License = ConfigurationSettings.AppSettings.Get("key");
                }

                avaCert2Svc.Profile.Client = "NUnit AvaCert2SvcTest";
                avaCert2Svc.Profile.Name = "";

                accountSvc = new AccountSvcWrapper();
                avaCert2SvcProxy = new AvaCert2SvcWrapper();
                accountId = int.Parse(avaCert2Svc.Configuration.Security.Account);
                DefaultCompany = accountSvc.GetDefaultCompany();
                SetServiceConfig(true, DefaultCompany.CompanyCode, "tax_man", "tax_org",
                             AvaCertServiceStatus.Enabled, DateTime.MinValue, true);
            }
            catch (Exception ex)
            {
                Assert.Fail("AvaCert2Svc failed creation: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [TearDown]
        public void Dispose()
        {
            try
            {
                if (avaCert2Svc != null)
                {
                    avaCert2Svc.Dispose();
                    avaCert2Svc = null;
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("TearDown failed: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void PingTest()
        {
            PingResult pingResult;

            try
            {
                pingResult = avaCert2Svc.Ping("");
                string[] versionSplit = pingResult.Version.Split('.');
                Assert.IsTrue(versionSplit.Length == 4, "Version split should return four parts");

                try
                {
                    int major = int.Parse(versionSplit[0]);
                    Assert.IsTrue(major >= 4, "Major version should be greater than 4");
                }
                catch (Exception e)
                {
                    Assert.Fail("converting major part of version '{0}' to int returned exception {1}", versionSplit[0], e.Message);
                }
                Assert.AreEqual(SeverityLevel.Success, pingResult.ResultCode);
                Assert.AreEqual(0, pingResult.Messages.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail("PingTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void IsAuthorizedTest()
        {
            try
            {
                SetAvaCertServices(false);
                var result = avaCert2Svc.IsAuthorized(string.Empty);
                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                SetAvaCertServices(true);
                result = avaCert2Svc.IsAuthorized(string.Empty);
                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
            }
            finally
            {
                SetAvaCertServices(true);
            }
        }

        #region CustomerSave Tests

        /// <summary>
        /// CustomerSave success test
        /// </summary>
        [Test]
        public void CustomerSaveTest()
        {
            try
            {
                CustomerSaveRequest request = new CustomerSaveRequest();
                request.CompanyCode = "Default";
                request.Customer = CreateCustomer();
                CustomerSaveResult result = avaCert2Svc.CustomerSave(request);

                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
            }            
            catch (Exception ex)
            {
                Assert.Fail("CustomerSaveTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        /// <summary>
        /// If a value for the NEW_CUSTOMER_REF_ID field is provided, and the ACTION field value is C or the ACTION field value is R and no customer record with the given CUSTOMER_REF_ID is found, 
        /// then a new customer record will be created with the NEW_CUSTOMER_REF_ID field value, and a warning will be issued.
        /// </summary>
        [Test]
        public void CustomerSaveWarningTest()
        {
            try
            {
                CustomerSaveRequest request = new CustomerSaveRequest();
                request.CompanyCode = "Default";
                request.Customer = CreateCustomer();
                request.Customer.NewCustomerCode = "New" + request.Customer.CustomerCode;
                CustomerSaveResult result = avaCert2Svc.CustomerSave(request);

                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Warning, result.ResultCode);
                Assert.AreEqual("NewCustomerCode", result.Messages[0].RefersTo);
                Assert.AreEqual("AvaCertNewCustCodeWarning", result.Messages[0].Name);
            }
            catch (Exception ex)
            {
                Assert.Fail("CustomerSaveWarningTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        /// <summary>
        /// Invalid EmailId check
        /// </summary>
        [Test]
        public void CustomerSaveInvalidEmailTest()
        {
            try
            {
                CustomerSaveRequest request = new CustomerSaveRequest();
                request.CompanyCode = "Default";
                request.Customer = CreateCustomer();
                request.Customer.Email = "Invalid_EmailId";
                CustomerSaveResult result = avaCert2Svc.CustomerSave(request);
                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                Assert.AreEqual("Error saving the Customer.", result.Messages[0].Summary);
                Assert.AreEqual("Record Skipped; Error: 'Invalid_EmailId' is not a well-formed email address", result.Messages[0].Details);
            }
            catch (Exception ex)
            {
                Assert.Fail("CustomerSaveInvalidEmailTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        /// <summary>
        /// NEW_CUSTOMER_REF_ID already exists with other customer
        /// </summary>
        [Test]
        public void CustomerSaveNewCustomerCodeErrorTest()
        {
            try
            {
                CustomerSaveRequest request = new CustomerSaveRequest();
                request.CompanyCode = "Default";
                request.Customer = CreateCustomer();
                CustomerSaveResult result = avaCert2Svc.CustomerSave(request);
                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);

                // Adding existing CustomerCode as NewCustomerCode
                request.Customer.NewCustomerCode = request.Customer.CustomerCode;
                request.Customer.CustomerCode = "TEST_" + DateTime.Now;
                result = avaCert2Svc.CustomerSave(request);
                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                Assert.AreEqual("Customer", result.Messages[0].RefersTo);
                Assert.AreEqual("Error saving the Customer.", result.Messages[0].Summary);
            }
            catch (Exception ex)
            {
                Assert.Fail("CustomerSaveNewCustomerCodeTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        /// <summary>
        /// ParentCustomerCode does't exists in the database
        /// </summary>
        [Test]
        public void CustomerSaveParentCustCodeErrorTest()
        {
            try
            {
                CustomerSaveRequest request = new CustomerSaveRequest();
                request.CompanyCode = "Default";
                request.Customer = CreateCustomer();
                request.Customer.ParentCustomerCode = "ParentCustomerCode_NotExist";
                CustomerSaveResult result = avaCert2Svc.CustomerSave(request);
                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                Assert.AreEqual("Error saving the Customer.", result.Messages[0].Summary);
            }
            catch (Exception ex)
            {
                Assert.Fail("CustomerSaveParentCustomerCodeTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        /// <summary>
        /// Invalid state error
        /// </summary>
        [Test]
        public void CustomerSaveStateErrorTest()
        {
            try
            {
                CustomerSaveRequest request = new CustomerSaveRequest();
                request.CompanyCode = "Default";
                request.Customer = CreateCustomer();
                request.Customer.State = "AA";
                CustomerSaveResult result = avaCert2Svc.CustomerSave(request);

                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                Assert.AreEqual("Error saving the Customer.", result.Messages[0].Summary);
                Assert.AreEqual("Record Skipped; Error: 'AA' is not a valid USPS State Code", result.Messages[0].Details);
            }
            catch (Exception ex)
            {
                Assert.Fail("CustomerSaveStateErrorTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        /// <summary>
        /// Success: Adding customer replaces the record if already exists 
        /// (as every requests is sent with action type R, no error should be generated)
        /// </summary>
        [Test]
        public void CustomerSaveDuplicateTest()
        {
            try
            {
                CustomerSaveRequest request = new CustomerSaveRequest();
                request.CompanyCode = "Default";
                request.Customer = CreateCustomer();
                CustomerSaveResult result = avaCert2Svc.CustomerSave(request);
                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);

                // Adding same customer should success
                result = avaCert2Svc.CustomerSave(request);
                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
            }            
            catch (Exception ex)
            {
                Assert.Fail("CustomerSaveDuplicateTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void CustomerSaveValidateTest()
        {
            try
            {
                CustomerSaveRequest request = new CustomerSaveRequest();
                request.CompanyCode = "Default";
                request.Customer = CreateCustomer();
                request.Customer.CustomerCode = "";
                request.Customer.BusinessName = "";
                CustomerSaveResult result = avaCert2Svc.CustomerSave(request);
                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                Assert.AreEqual("Error saving the Customer.", result.Messages[0].Summary);
                Assert.AreEqual("Record Skipped; Error: BUSINESS_NAME is required; Error: CustomerCode is required", result.Messages[0].Details);
            }
            catch (Exception ex)
            {
                Assert.Fail("CustomerSaveValidateTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void CustomerSaveInvalidCustomerTypeTest()
        {
            try
            {
                CustomerSaveRequest request = new CustomerSaveRequest();
                request.CompanyCode = "Default";
                request.Customer = CreateCustomer();
                request.Customer.Type = "InvalidCustomerType";
                CustomerSaveResult result = avaCert2Svc.CustomerSave(request);

                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                Assert.AreEqual("Record Skipped; Error: Invalid customer type", result.Messages[0].Details);
            }
            catch (Exception ex)
            {
                Assert.Fail("CustomerSaveInvalidCustomerTypeTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        /// <summary>
        /// ServiceNotEnabled Error test
        /// </summary>
        [Test]
        public void AccountIsDisabledErrorTest()
        {
            try
            {
                SetAvaCertServices(false);
                CustomerSaveRequest request = new CustomerSaveRequest();
                request.CompanyCode = "Default";
                request.Customer = CreateCustomer();
                CustomerSaveResult result = avaCert2Svc.CustomerSave(request);
                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                Assert.AreEqual("ServiceNotEnabledError", result.Messages[0].Name);
                Assert.AreEqual("CustomerSave", result.Messages[0].RefersTo);
                
                SetAvaCertServices(true);
                request.Customer = CreateCustomer();
                result = avaCert2Svc.CustomerSave(request);
                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                } 
                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
            }
            catch (Exception ex)
            {
                Assert.Fail("AccountIsDisabledErrorTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        /// <summary>
        /// AvaCertNotEnabled Error test
        /// </summary>
        [Test]
        public void CompanyLevelDisabledErrorTest()
        {
            try
            {
                SetServiceConfig(true, DefaultCompany.CompanyCode, "tax_man", "tax_org", AvaCertServiceStatus.Disabled, DateTime.MinValue, true);
                CustomerSaveRequest request = new CustomerSaveRequest();
                request.CompanyCode = "Default";
                request.Customer = CreateCustomer();
                CustomerSaveResult result = avaCert2Svc.CustomerSave(request);
                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                Assert.AreEqual("AvaCertNotEnabledError", result.Messages[0].Name);
                Assert.AreEqual("CustomerSave", result.Messages[0].RefersTo);

                SetServiceConfig(true, DefaultCompany.CompanyCode, "tax_man", "tax_org", AvaCertServiceStatus.Enabled, DateTime.MinValue, true);
                result = avaCert2Svc.CustomerSave(request);
                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
            }
            catch (Exception ex)
            {
                Assert.Fail("CompanyLevelDisabledErrorTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        #endregion CustomerSave Tests

        #region CertificateRequestInitiate Tests

        /// <summary>
        /// CertificateRequestInitiate success test
        /// </summary>
        [Test]
        public void CertificateRequestInitiateTest()
        {
            try
            {
                CustomerSaveRequest request = new CustomerSaveRequest();
                request.CompanyCode = "Default";
                request.Customer = CreateCustomer();
                CustomerSaveResult result = avaCert2Svc.CustomerSave(request);
                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
                
                CertificateRequestInitiateRequest initiateRequest = new CertificateRequestInitiateRequest();
                initiateRequest.CompanyCode = "Default";
                initiateRequest.CustomerCode = request.Customer.CustomerCode;
                initiateRequest.CommunicationMode = CommunicationMode.EMAIL;
                initiateRequest.CustomMessage = "Test";

                CertificateRequestInitiateResult initiateResult = avaCert2Svc.CertificateRequestInitiate(initiateRequest);

                if (initiateResult.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(initiateResult));
                }
                Assert.AreEqual(SeverityLevel.Success, initiateResult.ResultCode);
                Assert.AreEqual(initiateRequest.CustomerCode, initiateResult.CustomerCode, "CustomerCode");
                Console.WriteLine("Customer Code: " + initiateResult.CustomerCode);
                Console.WriteLine("Tracking Code: " + initiateResult.TrackingCode);
                Console.WriteLine("Request Id: " + initiateResult.RequestId);
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateRequestInitiateTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        /// <summary>
        /// CertificateRequestInitiate with non exists customer test
        /// </summary>
        [Test]
        public void CertificateRequestInitiateNonExistCustomerCodeTest()
        {
            try
            {
                CertificateRequestInitiateRequest initiateRequest = new CertificateRequestInitiateRequest();
                initiateRequest.CompanyCode = "Default";
                initiateRequest.CustomerCode = "TEST" + DateTime.Now.Ticks;
                initiateRequest.CommunicationMode = CommunicationMode.EMAIL;
                initiateRequest.CustomMessage = "Test";

                CertificateRequestInitiateResult initiateResult = avaCert2Svc.CertificateRequestInitiate(initiateRequest);

                if (initiateResult.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(initiateResult));
                }
                Assert.AreEqual(SeverityLevel.Error, initiateResult.ResultCode);
                Assert.AreEqual("Error saving the CertificateRequestInitiate.", initiateResult.Messages[0].Summary);
                Assert.AreEqual("application.customer.customer-not-found", initiateResult.Messages[0].RefersTo);
                Assert.AreEqual("Request Skipped; customer does not exist", initiateResult.Messages[0].Details);
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateRequestInitiateNonExistCustomerCodeTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void CertificateRequestInitiateCustomerCompanyCodesTest()
        {
            try
            {
                CustomerSaveRequest customerSaveRequest = new CustomerSaveRequest();
                customerSaveRequest.CompanyCode = "Default";
                customerSaveRequest.Customer = CreateCustomer();
                CustomerSaveResult customerSaveResult = avaCert2Svc.CustomerSave(customerSaveRequest);
                Assert.AreEqual(SeverityLevel.Success, customerSaveResult.ResultCode);

                CertificateRequestInitiateRequest request = new CertificateRequestInitiateRequest();
                request.CompanyCode = "Default";
                request.CustomerCode = customerSaveRequest.Customer.CustomerCode;
                request.CommunicationMode = CommunicationMode.EMAIL;
                CertificateRequestInitiateResult result = avaCert2Svc.CertificateRequestInitiate(request);
                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
                Console.WriteLine("Tracking Code: " + result.TrackingCode);
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateRequestInitiateCustCompanyCodesTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void CertificateRequestInitiateDirectTypeTest()
        {
            try
            {
                CustomerSaveRequest customerSaveRequest = new CustomerSaveRequest();
                customerSaveRequest.CompanyCode = "Default";
                customerSaveRequest.Customer = CreateCustomer();
                CustomerSaveResult customerSaveResult = avaCert2Svc.CustomerSave(customerSaveRequest);
                Assert.AreEqual(SeverityLevel.Success, customerSaveResult.ResultCode);

                CertificateRequestInitiateRequest request = new CertificateRequestInitiateRequest();
                request.CompanyCode = "Default";
                request.CustomerCode = customerSaveRequest.Customer.CustomerCode;
                request.Type = RequestType.DIRECT;

                CertificateRequestInitiateResult result = avaCert2Svc.CertificateRequestInitiate(request);

                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
                Assert.IsTrue(!string.IsNullOrEmpty(result.WizardLaunchUrl));
                Console.WriteLine("Tracking Code: " + result.TrackingCode);
                Console.WriteLine("WizardLaunchUrl: " + result.WizardLaunchUrl);
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateRequestInitiateDirectTypeTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void CertificateRequestInitiateMinRequiredFieldsTest()
        {
            try
            {
                CustomerSaveRequest customerSaveRequest = new CustomerSaveRequest();
                customerSaveRequest.CompanyCode = "Default";
                customerSaveRequest.Customer = new Customer();
                customerSaveRequest.Customer.BusinessName = "Foo Shop" + DateTime.Now.Ticks;
                customerSaveRequest.Customer.CustomerCode = "FOO" + DateTime.Now.Ticks;
                CustomerSaveResult customerSaveResult = avaCert2Svc.CustomerSave(customerSaveRequest);

                if (customerSaveResult.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(customerSaveResult));
                }
                Assert.AreEqual(SeverityLevel.Success, customerSaveResult.ResultCode);

                CertificateRequestInitiateRequest request = new CertificateRequestInitiateRequest();
                request.CompanyCode = "Default";
                request.CustomerCode = customerSaveRequest.Customer.CustomerCode;
                request.CommunicationMode = CommunicationMode.EMAIL;
                CertificateRequestInitiateResult result = avaCert2Svc.CertificateRequestInitiate(request);

                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                Assert.AreEqual("validation.common.email-required", result.Messages[0].RefersTo);
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateRequestInitiateMinRequiredFieldsTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        /// <summary>
        /// Check LengthError for each ExemptCert request fields 
        /// </summary>
        [Test]
        public void CertificateRequestInitiateLengthErrorTest()
        {
            try
            {
                string maxLengthString = "Avalara, A leading Sales Tax Calc Company.";
                for (int ii = 0; ii <= 5; ii++)
                {
                    maxLengthString += maxLengthString;
                }
                CertificateRequestInitiateRequest request = new CertificateRequestInitiateRequest();
                request.CompanyCode = "Default";
                request.CommunicationMode = CommunicationMode.EMAIL;
                request.CustomerCode = "TEST" + DateTime.Now.Ticks;
                request.CustomMessage = maxLengthString;
                request.SourceLocationCode = maxLengthString;

                CertificateRequestInitiateResult result = avaCert2Svc.CertificateRequestInitiate(request);

                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                Assert.AreEqual("Error saving the CertificateRequestInitiate.", result.Messages[0].Summary);
                Assert.AreEqual("validation.common.field-value-error", result.Messages[0].RefersTo);
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateRequestInitiateLengthErrorTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void CertificateRequestInitiateCustomerNotFoundErrorTest()
        {
            try
            {
                CertificateRequestInitiateRequest request = new CertificateRequestInitiateRequest();
                request.CompanyCode = "Default";
                request.CustomerCode = "TEST" + DateTime.Now.Ticks;
                request.CommunicationMode = CommunicationMode.EMAIL;
                request.CustomMessage = "Test";
                CertificateRequestInitiateResult result = avaCert2Svc.CertificateRequestInitiate(request);

                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                Assert.AreEqual("Request Skipped; customer does not exist", result.Messages[0].Details);
                Assert.AreEqual("application.customer.customer-not-found", result.Messages[0].RefersTo);
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateRequestInitiateCustomerNotFoundErrorTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        /// <summary>
        /// Customer object missing
        /// </summary>
        [Test]
        public void CertificateRequestInitiateCustomerCodeMissingTest()
        {
            try
            {
                CertificateRequestInitiateRequest request = new CertificateRequestInitiateRequest();
                request.CompanyCode = "Default";
                request.CommunicationMode = CommunicationMode.EMAIL;
                CertificateRequestInitiateResult result = avaCert2Svc.CertificateRequestInitiate(request);

                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                Assert.AreEqual("Request Skipped; Error: CustomerCode is required; Warning: REQUEST_ID ignored; Warning: TYPE ignored", result.Messages[0].Details);
                Assert.AreEqual("validation.common.field-value-error", result.Messages[0].RefersTo);
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateRequestInitiateCustomerCodeMissingTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void CertificateRequestInitiateLocationErrorTest()
        {
            try
            {
                CustomerSaveRequest request = new CustomerSaveRequest();
                request.CompanyCode = "Default";
                request.Customer = CreateCustomer();
                CustomerSaveResult result = avaCert2Svc.CustomerSave(request);
                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);

                CertificateRequestInitiateRequest initiateRequest = new CertificateRequestInitiateRequest();
                initiateRequest.CompanyCode = "Default";
                initiateRequest.CustomerCode = request.Customer.CustomerCode;
                initiateRequest.CommunicationMode = CommunicationMode.EMAIL;
                initiateRequest.SourceLocationCode = "LocationCode";
                CertificateRequestInitiateResult initiateResult = avaCert2Svc.CertificateRequestInitiate(initiateRequest);

                if (initiateResult.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(initiateResult));
                }
                Assert.AreEqual(SeverityLevel.Error, initiateResult.ResultCode);
                //Ticket 21203 >> commented as diff error message could come, for example 'application.configuration.location-override-disabled'
                //Assert.AreEqual("application.configuration.location-override-disabled", initiateResult.Messages[0].RefersTo);
                //Assert.AreEqual("Request Skipped; location override is disabled", initiateResult.Messages[0].Details);
                //Ticket 21203 >> Modified for handeling "application.common.location-not-found"
                Assert.AreEqual("Request Skipped; location not found", initiateResult.Messages[0].Details);
                Assert.AreEqual("application.common.location-not-found", initiateResult.Messages[0].RefersTo);
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateRequestInitiateLocationErrorTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void CertificateRequestInitiateFieldMissingTest()
        {
            try
            {
                CertificateRequestInitiateRequest request = new CertificateRequestInitiateRequest();
                request.CompanyCode = "Default";
                request.CustomerCode = "";
                CertificateRequestInitiateResult result = avaCert2Svc.CertificateRequestInitiate(request);

                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                Assert.AreEqual("validation.common.field-value-error", result.Messages[0].RefersTo);
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateRequestInitiateFieldMissingTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }
        
        [Test]
        public void CertificateRequestInitiateDuplicateTest()
        {
            try
            {
                CustomerSaveRequest customerRequest = new CustomerSaveRequest();
                customerRequest.CompanyCode = "Default";
                customerRequest.Customer = CreateCustomer();
                CustomerSaveResult customerResult = avaCert2Svc.CustomerSave(customerRequest);
                if (customerResult.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(customerResult));
                }
                Assert.AreEqual(SeverityLevel.Success, customerResult.ResultCode);

                CertificateRequestInitiateRequest request = new CertificateRequestInitiateRequest();
                request.CompanyCode = "Default";
                request.CustomerCode = customerRequest.Customer.CustomerCode;
                request.CommunicationMode = CommunicationMode.EMAIL;
                request.CustomMessage = "Test";
                CertificateRequestInitiateResult result = avaCert2Svc.CertificateRequestInitiate(request);

                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
                Console.WriteLine("Tracking Code: " + result.TrackingCode);

                // Initiate same request again should result in error
                result = avaCert2Svc.CertificateRequestInitiate(request);

                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                Assert.AreEqual("application.request.open-request-exists", result.Messages[0].RefersTo);
                Assert.AreEqual("Request Skipped; open request exists for customer", result.Messages[0].Details);
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateRequestInitiateDuplicateTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        /// <summary>
        /// Communication mode is Email(/Mail/Fax) but the value is not specified
        /// </summary>
        [Test]
        public void CertificateRequestInitiateCommunicationModeTest()
        {
            try
            {
                CustomerSaveRequest customerRequest = new CustomerSaveRequest();
                customerRequest.CompanyCode = "Default";
                customerRequest.Customer = CreateCustomer();
                customerRequest.Customer.Email = "";
                CustomerSaveResult customerResult = avaCert2Svc.CustomerSave(customerRequest);
                if (customerResult.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(customerResult));
                }
                Assert.AreEqual(SeverityLevel.Success, customerResult.ResultCode);

                CertificateRequestInitiateRequest request = new CertificateRequestInitiateRequest();
                request.CompanyCode = "Default";
                request.CustomerCode = customerRequest.Customer.CustomerCode;
                request.CommunicationMode = CommunicationMode.EMAIL;
                request.CustomMessage = "Test";
                CertificateRequestInitiateResult result = avaCert2Svc.CertificateRequestInitiate(request);

                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                Assert.AreEqual("validation.common.email-required", result.Messages[0].RefersTo);
                Assert.AreEqual("Request Skipped; email address is required", result.Messages[0].Details);

                request.CommunicationMode = CommunicationMode.FAX;
                result = avaCert2Svc.CertificateRequestInitiate(request);

                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                Assert.AreEqual("validation.common.fax-required", result.Messages[0].RefersTo);
                Assert.AreEqual("Request Skipped; fax number is required", result.Messages[0].Details);

                customerRequest.Customer = CreateCustomer();
                customerRequest.Customer.Address1 = "";
                customerResult = avaCert2Svc.CustomerSave(customerRequest);
                if (customerResult.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(customerResult));
                }
                Assert.AreEqual(SeverityLevel.Success, customerResult.ResultCode);

                request.CustomerCode = customerRequest.Customer.CustomerCode;
                request.CommunicationMode = CommunicationMode.MAIL;
                result = avaCert2Svc.CertificateRequestInitiate(request);

                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                Assert.AreEqual("validation.request.mail-address-incomplete", result.Messages[0].RefersTo);
                Assert.AreEqual("Request Skipped; mailing address is incomplete", result.Messages[0].Details);
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateRequestInitiateCommunicationModeTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        #endregion CertificateRequestInitiate Tests

        #region CertificateGet Tests

        [Test]
        public void CertificateGetTest()
        {
            try
            {
                CertificateGetRequest request = new CertificateGetRequest();
                request.CompanyCode = "Default";
                request.ModFromDate = DateTime.Now.AddDays(-10);
                request.ModToDate = DateTime.Now;

                CertificateGetResult result = avaCert2Svc.CertificateGet(request);
                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
                Console.Write("Certificates.Count: " + result.Certificates.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateGetTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void CertificateGetReasonCodeTest()
        {
            try
            {
                CertificateGetRequest request = new CertificateGetRequest();
                request.CompanyCode = "Default";

                CertificateGetResult result = avaCert2Svc.CertificateGet(request);
                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
                Console.Write("Certificates.Count: " + result.Certificates.Count);
                if (result.Certificates.Count > 0)
                {
                    foreach (Certificate certificate in result.Certificates)
                    {
                        if (certificate.ReviewStatus == CertificateReviewStatus.REJECTED)
                        {
                            Assert.AreNotEqual(string.Empty, certificate.RejectionReasonCode);

                            if (certificate.RejectionReasonCode == "OTHER_REASON")
                            {
                                Assert.AreNotEqual(string.Empty, certificate.RejectionReasonCustomText);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateGetReasonCodeTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void CertificateGetByCustomerTest()
        {
            try
            {
                CertificateGetRequest request = new CertificateGetRequest();
                request.CompanyCode = "Default";
                request.CustomerCode = "TEST" + DateTime.Now.Ticks;
                request.ModFromDate = DateTime.Now.AddDays(-10);
                request.ModToDate = DateTime.Now;

                CertificateGetResult result = avaCert2Svc.CertificateGet(request);
                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
                Console.Write("ExemptionCertificates.Count: " + result.Certificates.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateGetByCustomerTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void CertificateGetWithoutDatesTest()
        {
            try
            {
                CertificateGetRequest request = new CertificateGetRequest();
                request.CompanyCode = "Default";

                CertificateGetResult result = avaCert2Svc.CertificateGet(request);
                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
                Console.Write("Certificates.Count: " + result.Certificates.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateGetWithoutDatesTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        #endregion CertificateGet Tests

        #region CertificateRequestGet Tests

        [Test]
        public void CertificateRequestGet_CustomerCodeTest()
        {
            try
            {
                CustomerSaveRequest customerSaveRequest = new CustomerSaveRequest();
                customerSaveRequest.CompanyCode = "Default";
                customerSaveRequest.Customer = CreateCustomer();
                CustomerSaveResult customerSaveResult = avaCert2Svc.CustomerSave(customerSaveRequest);
                Assert.AreEqual(SeverityLevel.Success, customerSaveResult.ResultCode);

                CertificateRequestInitiateRequest certificateRequestInitiateRequest = new CertificateRequestInitiateRequest();
                certificateRequestInitiateRequest.CompanyCode = "Default";
                certificateRequestInitiateRequest.CustomerCode = customerSaveRequest.Customer.CustomerCode;
                certificateRequestInitiateRequest.CommunicationMode = CommunicationMode.EMAIL;
                CertificateRequestInitiateResult certificateRequestInitiateResult = avaCert2Svc.CertificateRequestInitiate(certificateRequestInitiateRequest);
                Assert.AreEqual(SeverityLevel.Success, certificateRequestInitiateResult.ResultCode);

                CertificateRequestGetRequest certificateRequestGetRequest = new CertificateRequestGetRequest();
                certificateRequestGetRequest.CompanyCode = "Default";
                certificateRequestGetRequest.CustomerCode = customerSaveRequest.Customer.CustomerCode;
                CertificateRequestGetResult certificateRequestGetResult =
                    avaCert2Svc.CertificateRequestGet(certificateRequestGetRequest);
                Assert.AreEqual(SeverityLevel.Success, certificateRequestGetResult.ResultCode);
                Assert.AreEqual(1, certificateRequestGetResult.CertificateRequests.Count);
                Assert.AreEqual(certificateRequestInitiateResult.CustomerCode,
                                certificateRequestGetResult.CertificateRequests[0].CustomerCode);
                Assert.AreEqual(certificateRequestInitiateResult.RequestId,
                                certificateRequestGetResult.CertificateRequests[0].RequestId);
                Assert.AreEqual(certificateRequestInitiateResult.TrackingCode,
                               certificateRequestGetResult.CertificateRequests[0].TrackingCode);
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateRequestGet_CustomerCodeTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void CertificateRequestGet_CustomerCodeRequestStatusTest()
        {
            try
            {
                CustomerSaveRequest customerSaveRequest = new CustomerSaveRequest();
                customerSaveRequest.CompanyCode = "Default";
                customerSaveRequest.Customer = CreateCustomer();
                CustomerSaveResult customerSaveResult = avaCert2Svc.CustomerSave(customerSaveRequest);
                Assert.AreEqual(SeverityLevel.Success, customerSaveResult.ResultCode);

                CertificateRequestInitiateRequest certificateRequestInitiateRequest = new CertificateRequestInitiateRequest();
                certificateRequestInitiateRequest.CompanyCode = "Default";
                certificateRequestInitiateRequest.CustomerCode = customerSaveRequest.Customer.CustomerCode;
                certificateRequestInitiateRequest.CommunicationMode = CommunicationMode.EMAIL;
                CertificateRequestInitiateResult certificateRequestInitiateResult = avaCert2Svc.CertificateRequestInitiate(certificateRequestInitiateRequest);
                Assert.AreEqual(SeverityLevel.Success, certificateRequestInitiateResult.ResultCode);

                CertificateRequestGetRequest certificateRequestGetRequest = new CertificateRequestGetRequest();
                certificateRequestGetRequest.CompanyCode = "Default";
                certificateRequestGetRequest.CustomerCode = customerSaveRequest.Customer.CustomerCode;
                certificateRequestGetRequest.RequestStatus = CertificateRequestStatus.OPEN;
                CertificateRequestGetResult certificateRequestGetResult =
                    avaCert2Svc.CertificateRequestGet(certificateRequestGetRequest);
                Assert.AreEqual(SeverityLevel.Success, certificateRequestGetResult.ResultCode);
                Assert.AreEqual(1, certificateRequestGetResult.CertificateRequests.Count);
                Assert.AreEqual(certificateRequestInitiateResult.TrackingCode, certificateRequestGetResult.CertificateRequests[0].TrackingCode);
                Assert.AreEqual(certificateRequestInitiateResult.CustomerCode, certificateRequestGetResult.CertificateRequests[0].CustomerCode);
                Assert.AreEqual(certificateRequestInitiateRequest.CommunicationMode, certificateRequestGetResult.CertificateRequests[0].CommunicationMode);
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateRequestGet_CustomerCodeRequestStatusTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void CertificateRequestGet_CustomerCodeRequestStatusClosedTest()
        {
            try
            {
                CustomerSaveRequest customerSaveRequest = new CustomerSaveRequest();
                customerSaveRequest.CompanyCode = "Default";
                customerSaveRequest.Customer = CreateCustomer();
                CustomerSaveResult customerSaveResult = avaCert2Svc.CustomerSave(customerSaveRequest);
                Assert.AreEqual(SeverityLevel.Success, customerSaveResult.ResultCode);

                CertificateRequestInitiateRequest certificateRequestInitiateRequest = new CertificateRequestInitiateRequest();
                certificateRequestInitiateRequest.CompanyCode = "Default";
                certificateRequestInitiateRequest.CustomerCode = customerSaveRequest.Customer.CustomerCode;
                certificateRequestInitiateRequest.CommunicationMode = CommunicationMode.EMAIL;
                CertificateRequestInitiateResult certificateRequestInitiateResult = avaCert2Svc.CertificateRequestInitiate(certificateRequestInitiateRequest);
                Assert.AreEqual(SeverityLevel.Success, certificateRequestInitiateResult.ResultCode);

                CertificateRequestGetRequest certificateRequestGetRequest = new CertificateRequestGetRequest();
                certificateRequestGetRequest.CompanyCode = "Default";
                certificateRequestGetRequest.CustomerCode = customerSaveRequest.Customer.CustomerCode;
                certificateRequestGetRequest.RequestStatus = CertificateRequestStatus.CLOSED;
                CertificateRequestGetResult certificateRequestGetResult =
                    avaCert2Svc.CertificateRequestGet(certificateRequestGetRequest);
                Assert.AreEqual(SeverityLevel.Success, certificateRequestGetResult.ResultCode);
                Assert.AreEqual(0, certificateRequestGetResult.CertificateRequests.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateRequestGet_CustomerCodeRequestStatusClosedTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void CertificateRequestGet_RequestStatusTest()
        {
            try
            {
                CustomerSaveRequest customerSaveRequest = new CustomerSaveRequest();
                customerSaveRequest.CompanyCode = "Default";
                customerSaveRequest.Customer = CreateCustomer();
                CustomerSaveResult customerSaveResult = avaCert2Svc.CustomerSave(customerSaveRequest);
                Assert.AreEqual(SeverityLevel.Success, customerSaveResult.ResultCode);

                CertificateRequestInitiateRequest certificateRequestInitiateRequest = new CertificateRequestInitiateRequest();
                certificateRequestInitiateRequest.CompanyCode = "Default";
                certificateRequestInitiateRequest.CustomerCode = customerSaveRequest.Customer.CustomerCode;
                certificateRequestInitiateRequest.CommunicationMode = CommunicationMode.EMAIL;
                CertificateRequestInitiateResult certificateRequestInitiateResult = avaCert2Svc.CertificateRequestInitiate(certificateRequestInitiateRequest);
                Assert.AreEqual(SeverityLevel.Success, certificateRequestInitiateResult.ResultCode);

                CertificateRequestGetRequest certificateRequestGetRequest = new CertificateRequestGetRequest();
                certificateRequestGetRequest.CompanyCode = "Default";
                certificateRequestGetRequest.RequestStatus = CertificateRequestStatus.OPEN;
                CertificateRequestGetResult certificateRequestGetResult =
                    avaCert2Svc.CertificateRequestGet(certificateRequestGetRequest);
                Assert.AreEqual(SeverityLevel.Success, certificateRequestGetResult.ResultCode);
                
                bool certificateExists = false;
                foreach (CertificateRequest certificateRequest in certificateRequestGetResult.CertificateRequests)
                {
                    if (string.Compare(certificateRequest.CustomerCode, customerSaveRequest.Customer.CustomerCode) == 0)
                    {
                        certificateExists = true;
                        Assert.AreEqual(certificateRequestInitiateResult.RequestId, certificateRequest.RequestId);
                        Assert.AreEqual(certificateRequestInitiateResult.TrackingCode, certificateRequest.TrackingCode);
                        break;
                    }

                }
                if (!certificateExists)
                {
                    Assert.Fail(string.Format("No Certificate Request exists for customerCode {0}", customerSaveRequest.Customer.CustomerCode));
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateRequestGet_RequestStatusTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void CertificateRequestGet_ModDateRangeTest()
        {
            try
            {
                CustomerSaveRequest customerSaveRequest = new CustomerSaveRequest();
                customerSaveRequest.CompanyCode = "Default";
                customerSaveRequest.Customer = CreateCustomer();
                CustomerSaveResult customerSaveResult = avaCert2Svc.CustomerSave(customerSaveRequest);
                Assert.AreEqual(SeverityLevel.Success, customerSaveResult.ResultCode);

                CertificateRequestInitiateRequest certificateRequestInitiateRequest = new CertificateRequestInitiateRequest();
                certificateRequestInitiateRequest.CompanyCode = "Default";
                certificateRequestInitiateRequest.CustomerCode = customerSaveRequest.Customer.CustomerCode;
                certificateRequestInitiateRequest.CommunicationMode = CommunicationMode.EMAIL;
                CertificateRequestInitiateResult certificateRequestInitiateResult = avaCert2Svc.CertificateRequestInitiate(certificateRequestInitiateRequest);
                Assert.AreEqual(SeverityLevel.Success, certificateRequestInitiateResult.ResultCode);

                CertificateRequestGetRequest certificateRequestGetRequest = new CertificateRequestGetRequest();
                certificateRequestGetRequest.CompanyCode = "Default";
                certificateRequestGetRequest.CustomerCode = customerSaveRequest.Customer.CustomerCode;
                CertificateRequestGetResult certificateRequestGetResult =
                    avaCert2Svc.CertificateRequestGet(certificateRequestGetRequest);
                Assert.AreEqual(SeverityLevel.Success, certificateRequestGetResult.ResultCode);
                Assert.AreEqual(1, certificateRequestGetResult.CertificateRequests.Count);
                Assert.AreEqual(certificateRequestInitiateResult.CustomerCode, certificateRequestGetResult.CertificateRequests[0].CustomerCode);

                certificateRequestGetRequest.ModFromDate = certificateRequestGetResult.CertificateRequests[0].RequestDate;
                certificateRequestGetRequest.ModToDate = certificateRequestGetResult.CertificateRequests[0].LastModifyDate;
                certificateRequestGetResult =
                    avaCert2Svc.CertificateRequestGet(certificateRequestGetRequest);
                Assert.AreEqual(SeverityLevel.Success, certificateRequestGetResult.ResultCode);
                Assert.AreEqual(1, certificateRequestGetResult.CertificateRequests.Count);
                Assert.AreEqual(certificateRequestInitiateResult.TrackingCode, certificateRequestGetResult.CertificateRequests[0].TrackingCode);
                Assert.AreEqual(certificateRequestInitiateResult.CustomerCode, certificateRequestGetResult.CertificateRequests[0].CustomerCode);
                Assert.AreEqual(certificateRequestInitiateRequest.CommunicationMode, certificateRequestGetResult.CertificateRequests[0].CommunicationMode);
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateRequestGet_ModDateRangeTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void CertificateRequestGet_InvalidModDateRangeTest()
        {
            try
            {
                CustomerSaveRequest customerSaveRequest = new CustomerSaveRequest();
                customerSaveRequest.CompanyCode = "Default";
                customerSaveRequest.Customer = CreateCustomer();
                CustomerSaveResult customerSaveResult = avaCert2Svc.CustomerSave(customerSaveRequest);
                Assert.AreEqual(SeverityLevel.Success, customerSaveResult.ResultCode);

                CertificateRequestInitiateRequest certificateRequestInitiateRequest = new CertificateRequestInitiateRequest();
                certificateRequestInitiateRequest.CompanyCode = "Default";
                certificateRequestInitiateRequest.CustomerCode = customerSaveRequest.Customer.CustomerCode;
                certificateRequestInitiateRequest.CommunicationMode = CommunicationMode.EMAIL;
                CertificateRequestInitiateResult certificateRequestInitiateResult = avaCert2Svc.CertificateRequestInitiate(certificateRequestInitiateRequest);
                Assert.AreEqual(SeverityLevel.Success, certificateRequestInitiateResult.ResultCode);

                CertificateRequestGetRequest certificateRequestGetRequest = new CertificateRequestGetRequest();
                certificateRequestGetRequest.CompanyCode = "Default";
                certificateRequestGetRequest.CustomerCode = customerSaveRequest.Customer.CustomerCode;
                certificateRequestGetRequest.ModFromDate = DateTime.Now.AddDays(-10);
                certificateRequestGetRequest.ModToDate = DateTime.Now.AddDays(-5);
                CertificateRequestGetResult certificateRequestGetResult =
                    avaCert2Svc.CertificateRequestGet(certificateRequestGetRequest);
                Assert.AreEqual(SeverityLevel.Success, certificateRequestGetResult.ResultCode);
                Assert.AreEqual(0, certificateRequestGetResult.CertificateRequests.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateRequestGet_InvalidModDateRangeTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void CertificateRequestGet_CustomerCodeModFromDateTest()
        {
            try
            {
                CustomerSaveRequest customerSaveRequest = new CustomerSaveRequest();
                customerSaveRequest.CompanyCode = "Default";
                customerSaveRequest.Customer = CreateCustomer();
                CustomerSaveResult customerSaveResult = avaCert2Svc.CustomerSave(customerSaveRequest);
                Assert.AreEqual(SeverityLevel.Success, customerSaveResult.ResultCode);

                CertificateRequestInitiateRequest certificateRequestInitiateRequest = new CertificateRequestInitiateRequest();
                certificateRequestInitiateRequest.CompanyCode = "Default";
                certificateRequestInitiateRequest.CustomerCode = customerSaveRequest.Customer.CustomerCode;
                certificateRequestInitiateRequest.CommunicationMode = CommunicationMode.EMAIL;
                CertificateRequestInitiateResult certificateRequestInitiateResult = avaCert2Svc.CertificateRequestInitiate(certificateRequestInitiateRequest);
                Assert.AreEqual(SeverityLevel.Success, certificateRequestInitiateResult.ResultCode);

                CertificateRequestGetRequest certificateRequestGetRequest = new CertificateRequestGetRequest();
                certificateRequestGetRequest.CompanyCode = "Default";
                certificateRequestGetRequest.CustomerCode = customerSaveRequest.Customer.CustomerCode;
                certificateRequestGetRequest.ModFromDate = DateTime.Now.AddDays(-1);
                CertificateRequestGetResult certificateRequestGetResult =
                    avaCert2Svc.CertificateRequestGet(certificateRequestGetRequest);
                Assert.AreEqual(SeverityLevel.Success, certificateRequestGetResult.ResultCode);
                Assert.AreEqual(1, certificateRequestGetResult.CertificateRequests.Count);
                Assert.AreEqual(certificateRequestInitiateResult.TrackingCode, certificateRequestGetResult.CertificateRequests[0].TrackingCode);
                Assert.AreEqual(certificateRequestInitiateResult.CustomerCode, certificateRequestGetResult.CertificateRequests[0].CustomerCode);
                Assert.AreEqual(certificateRequestInitiateRequest.CommunicationMode, certificateRequestGetResult.CertificateRequests[0].CommunicationMode);
            }
            catch (Exception ex)
            {
                Assert.Fail("CertificateRequestGet_CustomerCodeModFromDateTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        #endregion CertificateRequestGet Tests

        #region CertificateImageGet Tests

        [Test]
        [Explicit("Relies on a certificate being present in vCert, which cannot be automated")]
        public void CertificateImageGet_PNGTest()
        {
            CertificateImageGetRequest request = new CertificateImageGetRequest();

            request.AvaCertId = "CBSK";
            request.CompanyCode = "Default";
            request.Format = FormatType.PNG;
            request.PageNumber = 1;
            CertificateImageGetResult result = avaCert2Svc.CertificateImageGet(request);

            Assert.AreEqual(SeverityLevel.Success, result.ResultCode);

            string filepath = ConfigurationSettings.AppSettings.Get("RootUploadPath") + "\\" +
                              request.AvaCertId + "." + request.Format;

            WriteImageToFile(filepath, result.Image);

            Assert.IsTrue(File.Exists(filepath));
        }

        [Test]
        [Explicit("Relies on a certificate being present in vCert, which cannot be automated")]
        public void CertificateImageGet_PDFTest()
        {
            CertificateImageGetRequest request = new CertificateImageGetRequest();

            request.AvaCertId = "CBSK";
            request.CompanyCode = "Default";
            request.Format = FormatType.PDF;
            request.PageNumber = 1;
            CertificateImageGetResult result = avaCert2Svc.CertificateImageGet(request);

            Assert.AreEqual(SeverityLevel.Success, result.ResultCode);

            string filepath = ConfigurationSettings.AppSettings.Get("RootUploadPath") + "\\" +
                              request.AvaCertId + "." + request.Format;

            WriteImageToFile(filepath, result.Image);

            Assert.IsTrue(File.Exists(filepath));
        }

        [Test]
        public void CertificateImageGet_MissingAvaCertIdTest()
        {
            CertificateImageGetRequest request = new CertificateImageGetRequest();

            request.CompanyCode = "Default";
            request.Format = FormatType.PNG;
            CertificateImageGetResult result = avaCert2Svc.CertificateImageGet(request);

            Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
            Assert.AreEqual("AvaCertImageError", result.Messages[0].Name);
            Assert.AreEqual("ERROR: Resource not found.  One or more of the following is invalid: CompanyCode, AvaCertId.\r\n", result.Messages[0].Details);
        }

        [Test]
        public void CertificateImageGet_InvalidAvaCertIdTest()
        {
            CertificateImageGetRequest request = new CertificateImageGetRequest();

            request.AvaCertId = "InvalidId";
            request.CompanyCode = "Default";
            request.Format = FormatType.PNG;
            CertificateImageGetResult result = avaCert2Svc.CertificateImageGet(request);

            Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
            Assert.AreEqual("AvaCertImageError", result.Messages[0].Name);
            Assert.AreEqual("CertificateNotFound", result.Messages[0].RefersTo);
            Assert.AreEqual("ERROR: Invalid certificate ID.\r\n", result.Messages[0].Details);
        }

        [Test]
        [Explicit("Relies on a certificate being present in vCert, which cannot be automated")]
        public void CertificateImageGet_NoImageTest()
        {
            CertificateImageGetRequest request = new CertificateImageGetRequest();

            request.AvaCertId = "CBSM";
            request.CompanyCode = "Default";
            request.Format = FormatType.PNG;
            CertificateImageGetResult result = avaCert2Svc.CertificateImageGet(request);

            Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
            Assert.AreEqual("AvaCertImageError", result.Messages[0].Name);
            Assert.AreEqual("ImageNotFound", result.Messages[0].RefersTo);
            Assert.AreEqual("ERROR: The Certificate associated with the cert ID 'CBSM' does not have any images.\r\n", result.Messages[0].Details);
        }

        [Test]
        [Explicit("Relies on a certificate being present in vCert, which cannot be automated")]
        public void CertificateImageGet_InvalidPageNumberTest()
        {
            CertificateImageGetRequest request = new CertificateImageGetRequest();

            request.AvaCertId = "CBSK";
            request.CompanyCode = "Default";
            request.Format = FormatType.PNG;
            request.PageNumber = 5;
            CertificateImageGetResult result = avaCert2Svc.CertificateImageGet(request);

            Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
            Assert.AreEqual("AvaCertImageError", result.Messages[0].Name);
            Assert.AreEqual("PageNotFound", result.Messages[0].RefersTo);
            Assert.AreEqual("ERROR: Invalid page number.\r\n", result.Messages[0].Details);
        }

        #endregion CertificateImageGet Tests

        #region HelperMethods

        private string FormatMessages(BaseResult result)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("--- BEGIN DEBUG MESSAGES ---");

            int counter = 1;
            foreach (Message message in result.Messages)
            {
                builder.AppendFormat("\n\tMessage {0}: {1}\n", counter++, message.Summary);
                builder.AppendFormat("\tSeverity: {0}\n", message.Severity);
                builder.AppendFormat("\tName: {0}\n", message.Name);
                builder.AppendFormat("\tDetails: {0}\n", message.Details);
                if (!string.IsNullOrEmpty(message.RefersTo))
                {
                    builder.AppendFormat("\tRefers To: {0}\n", message.RefersTo);
                }
                builder.AppendFormat("\tSource: {0}\n", message.Source);
            }
            builder.Append("--- END DEBUG MESSAGES ---");

            return builder.ToString();
        }

        private Customer CreateCustomer()
        {
            Customer customer = new Customer();
            customer.CustomerCode = "TEST" + DateTime.Now.Ticks;
            customer.BusinessName = "Avalara, Inc";
            customer.Type = "Bill_To";
            customer.Address1 = "900 Winslow Way E";
            customer.City = "Bainbridge Island";
            customer.State = "WA";
            customer.Zip = "98110-2450";
            customer.Country = "US";
            customer.Attn = "";
            customer.Phone = "";
            customer.Email = ConfigurationManager.AppSettings.Get("ToEmail");
            if (string.IsNullOrEmpty(customer.Email))
            {
                customer.Email = "test@avalara.com";
            }

            customer.Fax = "";
            return customer;
        }

        private void SetAvaCertServices(bool IsAvaServiceEnabled)
        {
            accountSvc.SetRegistrarSecurity();
            if (!IsAvaServiceEnabled)
            {
                DeleteRequest deleteRequest = new DeleteRequest();
                deleteRequest.Filters = string.Format("AccountId={0} and ServiceTypeId={1}", accountId, (int)ServiceTypeId.AvaCert);
                DeleteResult deleteResult = accountSvc.ServiceDelete(deleteRequest);
                Assert.AreEqual(AccountProxy.SeverityLevel.Success, deleteResult.ResultCode);
            }
            else
            {
                FetchRequest fetchRequest = new FetchRequest();
                fetchRequest.Fields = "";
                fetchRequest.Filters = string.Format("AccountId={0} and ServiceTypeId={1}", accountId, (int)ServiceTypeId.AvaCert);
                ServiceFetchResult result = accountSvc.ServiceFetch(fetchRequest);
                Assert.AreEqual(AccountProxy.SeverityLevel.Success, result.ResultCode);
                bool isAvaCert = false;
                foreach (Service s in result.Services)
                {
                    if (s.ServiceTypeId == ServiceTypeId.AvaCert)
                    {
                        isAvaCert = true;
                    }
                }
                if (!isAvaCert)
                {
                    Service ser = new Service
                    {
                        AccountId = accountId,
                        ServiceTypeId = ServiceTypeId.AvaCert,
                        Quantity = 1,
                    };
                    ServiceSaveResult saveResult = accountSvc.ServiceSave(ser);
                    Assert.AreEqual(AccountProxy.SeverityLevel.Success, saveResult.ResultCode);
                }
            }
            accountSvc.SetDefaultSecurity();
        }

        private void SetServiceConfig(bool CreateNew, string CompanyCode, string ClientCode, string OrgCode, AvaCertServiceStatus Status, DateTime LastUpdatedDate, bool IsUpdateEnabled)
        {
            try
            {
                AvaCertServiceConfig avaCertServiceConfig = new AvaCertServiceConfig();

                if (CreateNew)
                {
                    SetAvaCertServiceConfigRequest request = new SetAvaCertServiceConfigRequest();
                    avaCertServiceConfig.LastUpdate = LastUpdatedDate;
                    avaCertServiceConfig.AllowPending = true;
                    avaCertServiceConfig.ClientCode = ClientCode;
                    avaCertServiceConfig.CompanyCode = CompanyCode;
                    avaCertServiceConfig.IsUpdateEnabled = IsUpdateEnabled;

                    avaCertServiceConfig.AvaCertServiceStatus = Status;
                    avaCertServiceConfig.OrgCode = OrgCode;
                    request.AvaCertServiceConfig = avaCertServiceConfig;
                    SetAvaCertServiceConfigResult result = avaCert2SvcProxy.SetAvaCertServiceConfig(request);
                    Assert.AreEqual(global::Avalara.AvaTax.Services.Proxies.AvaCert2SvcProxy.SeverityLevel.Success, result.ResultCode);
                }

                GetAvaCertServiceConfigRequest getAvaCertServiceConfigRequest = new GetAvaCertServiceConfigRequest();

                getAvaCertServiceConfigRequest.CompanyCode = CompanyCode;
                GetAvaCertServiceConfigResult getAvaCertServiceConfigResult = avaCert2SvcProxy.GetAvaCertServiceConfig(getAvaCertServiceConfigRequest);
                Assert.AreEqual(global::Avalara.AvaTax.Services.Proxies.AvaCert2SvcProxy.SeverityLevel.Success, getAvaCertServiceConfigResult.ResultCode);
                if (CreateNew)
                {
                    Assert.AreEqual(avaCertServiceConfig.OrgCode, getAvaCertServiceConfigResult.AvaCertServiceConfigs[0].OrgCode);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        public void WriteImageToFile(string filepath, byte[] image)
        {
            try
            {
                const int LENGTH = 256;
                Stream stream = new MemoryStream(image);
                FileStream file = File.Create(filepath);

                Byte[] buffer = new Byte[LENGTH];
                int bytesRead = stream.Read(buffer, 0, LENGTH);

                while (bytesRead > 0)
                {
                    file.Write(buffer, 0, bytesRead);
                    bytesRead = stream.Read(buffer, 0, LENGTH);
                }

                stream.Close();
                file.Close();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        #endregion HelperMethods
    }
}
