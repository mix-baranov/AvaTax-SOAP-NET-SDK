using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Avalara.AvaTax.Adapter;
using Avalara.AvaTax.Adapter.AddressService;
using Avalara.AvaTax.Adapter.TaxService;
using NUnit.Framework;

namespace Test.Avalara.AvaTax.Adapter
{
	/// <summary>
	/// Summary description for CommonSvcTest.
	/// </summary>
	[TestFixture]
	public class AdapterCommonTest
	{
		private AddressSvc _addressSvc;
		private TaxSvc _taxSvc;

		public AdapterCommonTest()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		[SetUp]
		public void Init()
		{
			try
			{	
				_addressSvc = new AddressSvc();
				/*
				 * shouldn't need these settings because this test class should not be making method calls
								_addressSvc.Configuration.Url = ConfigurationSettings.AppSettings.Get("Url");
								_addressSvc.Configuration.Security.Account = ConfigurationSettings.AppSettings.Get("account");
								_addressSvc.Configuration.Security.Key = ConfigurationSettings.AppSettings.Get("key");
								_addressSvc.Configuration.Security.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("timeout"));
				*/


			} 
			catch (Exception ex)
			{
				Assert.Fail("AddressSvc failed creation: " + ex.Message + " : " + ex.StackTrace);
			}
		}

		[TearDown]
		public void Dispose()
		{
			try
			{ 
				if (_addressSvc != null)
				{
					_addressSvc.Dispose();
					_addressSvc = null;
				}

				if (_taxSvc != null)
				{
					_taxSvc.Dispose();
					_taxSvc = null;
				}
			} 
			catch (Exception ex)
			{
				Assert.Fail("TearDown failed: " + ex.Message + " : " + ex.StackTrace);
			}
		}
	
		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NullUrlTest()
		{
			_addressSvc.Configuration.Url = null;
			Assert.Fail("Expected ArgumentNullException");
		}

		//[Test] - changed this functionality
		/*
				[ExpectedException(typeof(ArgumentException))]
				public void EmptyUrlTest()
				{
					_addressSvc.Configuration.Url = "";
					Assert.Fail("Expected ArgumentException");
				}
		*/

		[Test]
		public void MachineNameTest()
		{
			string machineName = System.Environment.MachineName;
			string testName = _addressSvc.Profile.Machine;
			Assert.AreEqual(machineName, testName, "Expected the machine names to be the same");
		}
		
		[Test]
		public void CreatableClassesTest()
		{
			//test for constructor visibility (i.e. should consumer be able to create a class)?
			ConstructorInfo[] cis;

			Assembly assembly = Assembly.LoadFrom("Avalara.AvaTax.Adapter.dll");
			Type[] types = assembly.GetExportedTypes();
			
			foreach (Type type in types)
			{
				cis = type.GetConstructors();
				switch(type.Name)
				{
						//Avalara.AvaTax.Common.Configuration namespace
					case "XmlSerializerSectionHandler":
						Assert.AreEqual(1, cis.Length, "Failed: " + type.Name);
						break;

						//Avalara.AvaTax.Adapter namespace
					case "BaseArrayList":
					case "BaseRequest":
					case "BaseResult":
					case "ConfigReader":
					case "IsAuthorizedResult":
					case "PingResult":
					case "Profile":
					case "ReadOnlyArrayList":
					case "RequestTest":
					case "Utilities":
					case "Messages":
					case "Message":
					case "AvaTaxException":
					case "AdapterConfigException":
                    case "RequestSecurity":
						Assert.AreEqual(0, cis.Length, "Failed: " + type.Name);
						break;
					case "AdapterTest":
					case "BaseSvc":
					case "ServiceConfig":
					case "LogSvc":
                    case "SOAPTraceRequest":
                    case "SOAPTraceRequestAttribute":
						Assert.AreEqual(1, cis.Length, "Failed: " + type.Name);
						break;

						//Avalara.AvaTax.Adapter.AddressService namespace
					case "Addresses":
					case "ValidateResult":
					case "ValidAddress":
					case "ValidAddresses":
                    case "AddressRequests":
                    case "SubmitAddressBatchResult":
                    case "GetAddressBatchResult":
                    case "CancelAddressBatchResult":
						Assert.AreEqual(0, cis.Length, "Failed: " + type.Name);
						break;
					case "Address":
					case "AddressSvc":
					case "ValidateRequest":
                    case "SubmitAddressBatchRequest":
                    case "GetAddressBatchRequest":
                    case "CancelAddressBatchRequest":
						Assert.AreEqual(1, cis.Length, "Failed: " + type.Name);
						break;

						//Avalara.AvaTax.Adapter.TaxService namespace
					case "CancelTaxRequest":
					case "CommitTaxRequest":
					case "GetTaxHistoryRequest":
					case "GetTaxRequest":
					case "Line":

					case "AdjustTaxRequest":
					case "AdjustTaxResponse":

					case "PostTaxRequest":
					case "ReconcileTaxHistoryRequest":
					case "SearchTaxHistoryRequest":
					case "TaxSvc":
                    case "SubmitTaxBatchRequest":
                    case "GetTaxBatchRequest":
                    case "CancelTaxBatchRequest":
					//Update Note : Added for 5.1
					case "ApplyPaymentRequest":
						Assert.AreEqual(1, cis.Length, "Failed: " + type.Name);
						break;

                    case "TaxRequests":
				        Assert.AreEqual(0, cis.Length, "Failed: " + type.Name);
						break;
					case "CancelTaxResult":
					case "CommitTaxResult":
					case "GetTaxHistoryResult":
					case "GetTaxResult":
					case "GetTaxResults":
					case "Lines":
					case "PostTaxResult":
                    case "AdjustTaxResult":
					case "ReconcileTaxHistoryResult":
					case "SearchTaxHistoryResult":
					case "TaxDetail":
					case "TaxDetails":
										
					case "TaxLine":
					case "TaxLines":
					//Update Note : Added for 5.0
					case "TaxOverride":
					case "TaxSummary":
					//Update Note : Added for 5.1
					case "ApplyPaymentResult":
                    case "SubmitTaxBatchResult":
                    case "GetTaxBatchResult":
                    case "CancelTaxBatchResult":
						Assert.AreEqual(0, cis.Length, "Failed: " + type.Name);
						break;

                    //Avalara.AvaTax.Adapter.AvaCert2Service namespace
                    case "AvaCert2Svc":
                    case "Customer":
                    case "CustomerSaveRequest":
                    case "CertificateRequestInitiateRequest":
                    case "CertificateGetRequest":
                    case "CertificateRequestGetRequest":
                    case "CertificateImageGetRequest":
                        Assert.AreEqual(1, cis.Length, "Failed: " + type.Name);
                        break;
                    case "CustomerSaveResult":
                    case "CertificateRequestInitiateResult":
                    case "CertificateGetResult":
                    case "CertificateRequestGetResult":
                    case "CertificateImageGetResult":
                    case "Certificate":
                    case "Certificates":
                    case "CertificateRequest":
                    case "CertificateRequests":
                    case "CertificateJurisdiction":
                    case "CertificateJurisdictions":
                        Assert.AreEqual(0, cis.Length, "Failed: " + type.Name);
                        break;
                        
						//Avalara.AvaTax.Proxies namespace
					case "ProxyBaseAddress":
					case "ProxyBaseResult":
					case "ProxyIsAuthorizedResult":
					case "ProxyPingResult":
					case "ProxyProfile":
					case "ProxyMessage":
                    case "ProxyBaseRequest":
						Assert.AreEqual(1, cis.Length, "Failed: " + type.Name);
						break;

						//Avalara.AvaTax.Proxies.AddressSvcProxy namespace
					case "ProxyValidateRequest":
					case "ProxyValidateResult":
					case "ProxyValidAddress":
                    case "ProxySubmitAddressBatchRequest":
                    case "ProxyGetAddressBatchRequest":
                    case "ProxyCancelAddressBatchRequest":
                    case "ProxySubmitAddressBatchResult":
                    case "ProxyGetAddressBatchResult":
                    case "ProxyCancelAddressBatchResult":
						Assert.AreEqual(1, cis.Length, "Failed: " + type.Name);
						break;

					case "ProxyAddressSvc":
						Assert.AreEqual(0, cis.Length, "Failed: " + type.Name);
						break;

						//Avalara.AvaTax.Proxies.TaxSvcProxy namespace
					case "ProxyCancelTaxRequest":
					case "ProxyCancelTaxResult":
					case "ProxyCommitTaxRequest":
					case "ProxyCommitTaxResult":
					case "ProxyGetTaxHistoryRequest":
					case "ProxyGetTaxHistoryResult":
					case "ProxyGetTaxRequest":
					case "ProxyGetTaxResult":
					case "ProxyLine":
					case "ProxyPostTaxRequest":
					case "ProxyPostTaxResult":
					case "ProxyReconcileTaxHistoryRequest":
					case "ProxyReconcileTaxHistoryResult":
					case "ProxySearchTaxHistoryRequest":
					case "ProxySearchTaxHistoryResult":
					case "ProxyTaxDetail":
					case "ProxyAdjustTaxRequest":
					case "ProxyAdjustTaxResult":
					case "ProxyTaxLine":
					//Update Note : Added for 5.0
					case "ProxyTaxOverride":
					//Update Note : Added for 5.1
					case "ProxyApplyPaymentRequest":
					case "ProxyApplyPaymentResult":
                    case "ProxySubmitTaxBatchRequest":
                    case "ProxyGetTaxBatchRequest":
                    case "ProxyCancelTaxBatchRequest":
                    case "ProxySubmitTaxBatchResult":
                    case "ProxyGetTaxBatchResult":
                    case "ProxyCancelTaxBatchResult":
						Assert.AreEqual(1, cis.Length, "Failed: " + type.Name);
						break;

					case "ProxyTaxSvc":
						Assert.AreEqual(0, cis.Length, "Failed:" + type.Name);
						break;

                    //Avalara.AvaTax.Proxies.AvaCertSvcProxy namespace
                    case "ProxyAvaCert2Svc":
                        Assert.AreEqual(0, cis.Length, "Failed:" + type.Name);
                        break;
                    case "ProxyCustomerSaveRequest":
                    case "ProxyCustomerSaveResult":
                    case "ProxyCustomer":
                    case "ProxyCertificateRequestInitiateRequest":
                    case "ProxyCertificateRequestInitiateResult":
                    case "ProxyCertificateGetRequest":
                    case "ProxyCertificateGetResult":
                    case "ProxyCertificateRequestGetRequest":
                    case "ProxyCertificateRequestGetResult":
                    case "ProxyCertificateImageGetRequest":
                    case "ProxyCertificateImageGetResult":
                    case "ProxyCertificate":
                    case "ProxyCertificateRequest":
                    case "ProxyCertificateJurisdiction":
                        Assert.AreEqual(1, cis.Length, "Failed: " + type.Name);
                        break;

						//Enums - no constructor to check for visibility
					case "ProxyBoundaryLevel":
					case "ProxyCancelCode":
					case "ProxyDetailLevel":
					case "ProxyDocStatus":
					case "ProxyDocumentType":
					case "ProxyJurisdictionType":
					case "AdapterConfigurationExceptionType":
					case "AddressType":
					case "BoundaryLevel":
					case "CancelCode":
					case "DetailLevel":
					case "DocStatus":
					case "DocumentType":
					case "JurisdictionType":
					case "ProxySeverityLevel":
					case "SeverityLevel":
					case "ProxyTaxType":
					case "TaxType":
					case "ProxyTextCase":
					case "TextCase":
					//Update Note : Added for 5.0
					case "ProxyTaxOverrideType":
					case "TaxOverrideType":
					case "ProxyServiceMode":
					case "ServiceMode":
					//Update Note : Added for 5.1
					case "ProxyAccountingMethod":
					case "AccountingMethod":
					case "LogLevel":

					case "ProxySecurity":
					case "Security":
					case "ProxyUsernameToken":
					case "UsernameToken":
					case "ProxyPassword":
					case "Password":
					case "ProxyConfirmationType":
					case "ConfirmationType":
                    case "ProxyCommunicationMode":
                    case "CommunicationMode":
                    case "ProxyRequestType":
                    case "RequestType":
                    case "ProxyCertificateStatus":
                    case "CertificateStatus":
                    case "ProxyReviewStatus":
                    case "ReviewStatus":
                    case "ProxyCertificateUsage":
                    case "CertificateUsage":
                    case "ProxyFormatType":
                    case "FormatType":
                    case "ProxyCertificateReviewStatus":
                    case "CertificateReviewStatus":
                    case "ProxyCertificateRequestStatus":
                    case "CertificateRequestStatus":
                    case "ProxyCertificateRequestStage":
                    case "CertificateRequestStage":
						Assert.IsTrue(true);
						break;

						//Exception cases (we don't care about the result)
					case "ConfigDataProtector":
					case "DataProtector":
					case "Store":
						Assert.IsTrue(true);
						break;

					default:
						Assert.Fail("Need to add a class to the constructor test: " + type.Name);
						break;
				}
			}
		}

		[Test]
		public void COMGuidTest()
		{
			//test for existence of custom GUID attribute
			bool defined;

			Assembly assembly = Assembly.LoadFrom("Avalara.AvaTax.Adapter.dll");
			Type[] types = assembly.GetExportedTypes();
			
			foreach (Type type in types)
			{
				if (!type.IsAbstract)
				{
					defined = Attribute.IsDefined(type, typeof(GuidAttribute));
					switch(type.Name)
					{
							//Avalara.AvaTax.Common.Configuration namespace
						case "XmlSerializerSectionHandler":
							Assert.IsTrue(defined, "Failed: " + type.Name);
							break;
						
							//Avalara.AvaTax.Adapter namespace
						case "BaseArrayList":
						case "BaseRequest":
						case "BaseResult":
						case "BaseSvc":
						case "ConfigReader":
						case "IsAuthorizedResult":
						case "PingResult":
						case "Profile":
						case "ReadOnlyArrayList":
						case "RequestSecurity":
						case "RequestTest":
						case "ServiceConfig":
						case "Utilities":
						case "Messages":
						case "Message":
						case "AvaTaxException":
						case "AdapterConfigException":
						case "Security":
						case "UsernameToken":
						case "Password":
						case "LogSvc":
							Assert.IsTrue(defined, "Failed: " + type.Name);
							break;
						case "AdapterTest":
                        case "SOAPTraceRequest":
                        case "SOAPTraceRequestAttribute":
							Assert.IsFalse(defined, "Failed: " + type.Name);
							break;

							//Avalara.AvaTax.Adapter.AddressService namespace
						case "Address":
						case "Addresses":
						case "AddressSvc":
						case "ValidateRequest":
						case "ValidateResult":
						case "ValidAddress":
						case "ValidAddresses":
                        case "AddressRequests":
                        case "SubmitAddressBatchRequest":
                        case "GetAddressBatchRequest":
                        case "CancelAddressBatchRequest":
                        case "SubmitAddressBatchResult":
                        case "GetAddressBatchResult":
                        case "CancelAddressBatchResult":
							Assert.IsTrue(defined, "Failed: " + type.Name);
							break;

							//Avalara.AvaTax.Adapter.TaxService namespace
						case "CancelTaxRequest":
						case "CommitTaxRequest":
						case "GetTaxHistoryRequest":
						case "GetTaxRequest":
						case "Line":
						case "PostTaxRequest":
						case "ReconcileTaxHistoryRequest":
						case "SearchTaxHistoryRequest":
						case "TaxSvc":
                        case "TaxRequests":
                        case "SubmitTaxBatchRequest":
                        case "GetTaxBatchRequest":
                        case "CancelTaxBatchRequest":
							Assert.IsTrue(defined, "Failed: " + type.Name);
							break;

						case "CancelTaxResult":
						case "CommitTaxResult":
						case "GetTaxHistoryResult":
						case "GetTaxResult":
						case "GetTaxResults":
						case "Lines":
						case "PostTaxResult":
						case "ReconcileTaxHistoryResult":
						case "SearchTaxHistoryResult":
						case "TaxDetail":
						case "TaxDetails":
						case "TaxLine":
						case "AdjustTaxRequest":
						case "AdjustTaxResult":	
						case "TaxLines":

						//Update Note : Added for 5.0
						case "TaxOverride":
						case "TaxSummary":
							
						//Update Note : Added for 5.1
						case "ApplyPaymentRequest":
						case "ApplyPaymentResult":
                        case "SubmitTaxBatchResult":
                        case "GetTaxBatchResult":
                        case "CancelTaxBatchResult":
							Assert.IsTrue(defined, "Failed: " + type.Name);
							break;

                        //Avalara.AvaTax.Adapter.AvaCert2Service namespace
                        case "AvaCert2Svc":
                        case "CustomerSaveRequest":
                        case "CustomerSaveResult":
                        case "Customer":
                        case "CertificateRequestInitiateRequest":
                        case "CertificateRequestInitiateResult":
                        case "CertificateGetRequest":
                        case "CertificateGetResult":
                        case "CertificateRequestGetRequest":
                        case "CertificateRequestGetResult":
                        case "CertificateImageGetRequest":
                        case "CertificateImageGetResult":
                        case "Certificate":
                        case "Certificates":
                        case "CertificateRequest":
                        case "CertificateRequests":
                        case "CertificateJurisdiction":
                        case "CertificateJurisdictions":
                            Assert.IsTrue(defined, "Failed: " + type.Name);
                            break;
							//Avalara.AvaTax.Proxies namespace
						case "ProxyBaseAddress":
						case "ProxyBaseResult":
						case "ProxyIsAuthorizedResult":
						case "ProxyPingResult":
						case "ProxyProfile":
						case "ProxySecurity":
						case "ProxyUsernameToken":
						case "ProxyPassword":
						case "ProxyMessage":
						case "ProxyBaseRequest":
							Assert.IsFalse(defined, "Failed: " + type.Name);
							break;

							//Avalara.AvaTax.Proxies.AddressSvcProxy namespace
						case "ProxyAddressSvc":
						case "ProxyValidateRequest":
						case "ProxyValidateResult":
						case "ProxyValidAddress":
                        case "ProxySubmitAddressBatchRequest":
                        case "ProxyGetAddressBatchRequest":
                        case "ProxyCancelAddressBatchRequest":
                        case "ProxySubmitAddressBatchResult":
                        case "ProxyGetAddressBatchResult":
                        case "ProxyCancelAddressBatchResult":
							Assert.IsFalse(defined, "Failed: " + type.Name);
							break;

							//Avalara.AvaTax.Proxies.TaxSvcProxy namespace
						case "ProxyCancelTaxRequest":
						case "ProxyCancelTaxResult":
						case "ProxyCommitTaxRequest":
						case "ProxyCommitTaxResult":
						case "ProxyGetTaxHistoryRequest":
						case "ProxyGetTaxHistoryResult":
						case "ProxyGetTaxRequest":
						case "ProxyGetTaxResult":
						case "ProxyLine":
						case "ProxyPostTaxRequest":
						case "ProxyPostTaxResult":
						case "ProxyReconcileTaxHistoryRequest":
						case "ProxyReconcileTaxHistoryResult":
						case "ProxySearchTaxHistoryRequest":
						case "ProxySearchTaxHistoryResult":
						case "ProxyTaxDetail":
						case "ProxyTaxLine":
						case "ProxyAdjustTaxRequest":
						case "ProxyAdjustTaxResult":						
						case "ProxyTaxSvc":
						//Update Note : Added for 5.0
						case "ProxyTaxOverride":
						//Update Note : Added for 5.1
						case "ProxyApplyPaymentRequest":
						case "ProxyApplyPaymentResult":
                        case "ProxySubmitTaxBatchRequest":
                        case "ProxyGetTaxBatchRequest":
                        case "ProxyCancelTaxBatchRequest":
                        case "ProxySubmitTaxBatchResult":
                        case "ProxyGetTaxBatchResult":
                        case "ProxyCancelTaxBatchResult":
							Assert.IsFalse(defined, "Failed: " + type.Name);
							break;

                        //Avalara.AvaTax.Proxies.AvaCert2SvcProxy namespace
                        case "ProxyAvaCert2Svc":
                        case "ProxyCustomerSaveRequest":
                        case "ProxyCustomerSaveResult":
                        case "ProxyCustomer":
                        case "ProxyCertificateRequestInitiateRequest":
                        case "ProxyCertificateRequestInitiateResult":
                        case "ProxyCertificateGetRequest":
                        case "ProxyCertificateGetResult":
                        case "ProxyCertificateRequestGetRequest":
                        case "ProxyCertificateRequestGetResult":
                        case "ProxyCertificateImageGetRequest":
                        case "ProxyCertificateImageGetResult":
                        case "ProxyCertificate":
                        case "ProxyCertificateRequest":
                        case "ProxyCertificateJurisdiction":
                            Assert.IsFalse(defined, "Failed: " + type.Name);
                            break;

							//Enums
						case "ProxyBoundaryLevel":
						case "ProxyCancelCode":
						case "ProxyDetailLevel":
						case "ProxyDocStatus":
						case "ProxyDocumentType":
						case "ProxyJurisdictionType":
						case "ProxySeverityLevel":
						case "ProxyTaxType":
						case "ProxyTextCase":
						//Update Note : Added for 5.0
						case "ProxyTaxOverrideType":
						case "ProxyServiceMode":
						//Update Note : Added for 5.1
						case "ProxyAccountingMethod":						
						case "ProxyConfirmationType":					
                        case "ProxyCommunicationMode":
                        case "ProxyRequestType":
                        case "ProxyCertificateStatus":
                        case "ProxyReviewStatus":
                        case "ProxyCertificateUsage":
                        case "ProxyFormatType":
                        case "ProxyCertificateReviewStatus":
                        case "ProxyCertificateRequestStatus":
                        case "ProxyCertificateRequestStage":
							Assert.IsFalse(defined, "Failed: " + type.Name);
							break;

						case "AdapterConfigurationExceptionType":
						case "AddressType":
						case "BoundaryLevel":
						case "CancelCode":
						case "DetailLevel":
						case "DocStatus":
						case "DocumentType":
						case "JurisdictionType":
						case "SeverityLevel":
						case "TaxType":
						case "TextCase":
						//Update Note : Added for 5.0
						case "TaxOverrideType":
						case "ServiceMode":
						//Update Note : Added for 5.1
						case "AccountingMethod":
						case "LogLevel":
						case "ConfirmationType":
                        case "CommunicationMode":
                        case "RequestType":
                        case "CertificateStatus":
                        case "ReviewStatus":
                        case "CertificateUsage":
                        case "FormatType":
                        case "CertificateReviewStatus":
                        case "CertificateRequestStatus":
                        case "CertificateRequestStage":
							Assert.IsTrue(defined, "Failed: " + type.Name);
							break;

							//Exception cases (we don't care about the result)
						case "ConfigDataProtector":
						case "DataProtector":
						case "Store":
							Assert.IsTrue(true);
							break;

						default:
							Assert.Fail("Need to add a class to the constructor test: " + type.Name);
							break;
					}
				}
			}
		}
		
		[Test]
		public void COMVisibleAttributeTest()
		{
			//test for existence of custom ComVisible attribute
			bool defined;

			Assembly assembly = Assembly.LoadFrom("Avalara.AvaTax.Adapter.dll");
			Type[] types = assembly.GetExportedTypes();
			
			foreach (Type type in types)
			{
				if (!type.IsAbstract)
				{
					defined = Attribute.IsDefined(type, typeof(ComVisibleAttribute));
					switch(type.Name)
					{
							//Avalara.AvaTax.Common.Configuration namespace
						case "XmlSerializerSectionHandler":
							Assert.IsTrue(defined, "Failed: " + type.Name);
							break;
					
							//Avalara.AvaTax.Adapter namespace
						case "BaseArrayList":
						case "BaseRequest":
						case "BaseResult":
						case "BaseSvc":
						case "ConfigReader":
						case "IsAuthorizedResult":
						case "PingResult":
						case "Profile":
						case "ReadOnlyArrayList":
						case "RequestSecurity":
						case "RequestTest":
						case "ServiceConfig":
						case "Utilities":
						case "Messages":
						case "Message":
						case "AvaTaxException":
						case "AdapterConfigException":
							Assert.IsTrue(defined, "Failed: " + type.Name);
							break;
						case "AdapterTest":
                        case "SOAPTraceRequest":
                        case "SOAPTraceRequestAttribute":
							Assert.IsFalse(defined, "Failed: " + type.Name);
							break;

							//Avalara.AvaTax.Adapter.AddressService namespace
						case "Address":
						case "Addresses":
						case "AddressSvc":
						case "ValidateRequest":
						case "ValidateResult":
						case "ValidAddress":
						case "ValidAddresses":
                        case "AddressRequests":
                        case "SubmitAddressBatchRequest":
                        case "GetAddressBatchRequest":
                        case "CancelAddressBatchRequest":
                        case "SubmitAddressBatchResult":
                        case "GetAddressBatchResult":
                        case "CancelAddressBatchResult":
							Assert.IsTrue(defined, "Failed: " + type.Name);
							break;

							//Avalara.AvaTax.Adapter.TaxService namespace
						case "CancelTaxRequest":
						case "CommitTaxRequest":
						case "GetTaxHistoryRequest":
						case "GetTaxRequest":
						case "Line":
						case "PostTaxRequest":
						case "ReconcileTaxHistoryRequest":
						case "SearchTaxHistoryRequest":
						case "TaxSvc":
                        case "TaxRequests":
                        case "SubmitTaxBatchRequest":
                        case "GetTaxBatchRequest":
                        case "CancelTaxBatchRequest":
						case "LogSvc":
							Assert.IsTrue(defined, "Failed: " + type.Name);
							break;

						case "CancelTaxResult":
						case "CommitTaxResult":
						case "GetTaxHistoryResult":
						case "GetTaxResult":
						case "GetTaxResults":
						case "Lines":
						case "PostTaxResult":
						case "ReconcileTaxHistoryResult":
						case "SearchTaxHistoryResult":
						case "TaxDetail":
						case "TaxDetails":
						case "TaxLine":
						case "AdjustTaxRequest":
						case "AdjustTaxResult":							
						case "TaxLines":
						//Update Note : Added for 5.0
						case "TaxOverride":
						case "TaxSummary":							
						//Update Note : Added for 5.1
						case "ApplyPaymentRequest":
						case "ApplyPaymentResult":
                        case "SubmitTaxBatchResult":
                        case "GetTaxBatchResult":
                        case "CancelTaxBatchResult":
							Assert.IsTrue(defined, "Failed: " + type.Name);
							break;

                        //Avalara.AvaTax.Adapter.AvaCert2Service namespace
                        case "AvaCert2Svc":
                        case "CustomerSaveRequest":
                        case "CustomerSaveResult":
                        case "Customer":
                        case "CertificateRequestInitiateRequest":
                        case "CertificateRequestInitiateResult":
                        case "CertificateGetRequest":
                        case "CertificateGetResult":
                        case "CertificateRequestGetRequest":
                        case "CertificateRequestGetResult":
                        case "CertificateImageGetRequest":
                        case "CertificateImageGetResult":
                        case "Certificate":
                        case "Certificates":
                        case "CertificateRequest":
                        case "CertificateRequests":
                        case "CertificateJurisdiction":
                        case "CertificateJurisdictions":
                            Assert.IsTrue(defined, "Failed: " + type.Name);
                            break;

							//Avalara.AvaTax.Proxies namespace
						case "ProxyBaseAddress":
						case "ProxyBaseResult":
						case "ProxyIsAuthorizedResult":
						case "ProxyPingResult":
						case "ProxyProfile":
						case "ProxyMessage":
						case "ProxySecurity":
						case "ProxyUsernameToken":
						case "ProxyPassword":
                        case "ProxyBaseRequest":
							Assert.IsTrue(defined, "Failed: " + type.Name);
							break;

							//Avalara.AvaTax.Proxies.AddressSvcProxy namespace
						case "ProxyAddressSvc":
						case "ProxyValidateRequest":
						case "ProxyValidateResult":
						case "ProxyValidAddress":
                        case "ProxySubmitAddressBatchRequest":
                        case "ProxyGetAddressBatchRequest":
                        case "ProxyCancelAddressBatchRequest":
                        case "ProxySubmitAddressBatchResult":
                        case "ProxyGetAddressBatchResult":
                        case "ProxyCancelAddressBatchResult":
							Assert.IsTrue(defined, "Failed: " + type.Name);
							break;

							//Avalara.AvaTax.Proxies.TaxSvcProxy namespace
						case "ProxyCancelTaxRequest":
						case "ProxyCancelTaxResult":
						case "ProxyCommitTaxRequest":
						case "ProxyCommitTaxResult":
						case "ProxyGetTaxHistoryRequest":
						case "ProxyGetTaxHistoryResult":
						case "ProxyGetTaxRequest":
						case "ProxyGetTaxResult":
						case "ProxyLine":
						case "ProxyPostTaxRequest":
						case "ProxyPostTaxResult":
						case "ProxyReconcileTaxHistoryRequest":
						case "ProxyReconcileTaxHistoryResult":
						case "ProxySearchTaxHistoryRequest":
						case "ProxySearchTaxHistoryResult":
						case "ProxyTaxDetail":
						case "ProxyTaxLine":
						case "ProxyAdjustTaxRequest":
						case "ProxyAdjustTaxResult":							
						case "ProxyTaxSvc":
						//Update Note : Added for 5.0
						case "ProxyTaxOverride":
						case "ProxyServiceMode":
							
						//Update Note : Added for 5.1
						case "ProxyApplyPaymentRequest":
						case "ProxyApplyPaymentResult":
                        case "ProxySubmitTaxBatchRequest":
                        case "ProxyGetTaxBatchRequest":
                        case "ProxyCancelTaxBatchRequest":
                        case "ProxySubmitTaxBatchResult":
                        case "ProxyGetTaxBatchResult":
                        case "ProxyCancelTaxBatchResult":
							Assert.IsTrue(defined, "Failed: " + type.Name);
							break;

                        //Avalara.AvaTax.Proxies.AvaCert2SvcProxy namespace
                        case "ProxyAvaCert2Svc":
                        case "ProxyCustomerSaveRequest":
                        case "ProxyCustomerSaveResult":
                        case "ProxyCustomer":
                        case "ProxyCertificateRequestInitiateRequest":
                        case "ProxyCertificateRequestInitiateResult":
                        case "ProxyCertificateGetRequest":
                        case "ProxyCertificateGetResult":
                        case "ProxyCertificateRequestGetRequest":
                        case "ProxyCertificateRequestGetResult":
                        case "ProxyCertificateImageGetRequest":
                        case "ProxyCertificateImageGetResult":
                        case "ProxyCertificate":
                        case "ProxyCertificateRequest":
                        case "ProxyCertificateJurisdiction":
                            Assert.IsTrue(defined, "Failed: " + type.Name);
                            break;

							//Enums
						case "ProxyBoundaryLevel":
						case "ProxyCancelCode":
						case "ProxyDetailLevel":
						case "ProxyDocStatus":
						case "ProxyDocumentType":
						case "ProxyJurisdictionType":
						case "ProxySeverityLevel":
						case "ProxyTaxType":
						case "ProxyTextCase":
						//Update Note : Added for 5.0
						case "ProxyTaxOverrideType":
						//Update Note : Added for 5.1
						case "ProxyAccountingMethod":
						case "ProxyConfirmationType":
                        case "ProxyCommunicationMode":
                        case "ProxyRequestType":
                        case "ProxyCertificateStatus":
                        case "ProxyReviewStatus":
                        case "ProxyCertificateUsage":
                        case "ProxyFormatType":
                        case "ProxyCertificateReviewStatus":
                        case "ProxyCertificateRequestStatus":
                        case "ProxyCertificateRequestStage":
							Assert.IsTrue(defined, "Failed: " + type.Name);
							break;

						case "AdapterConfigurationExceptionType":
						case "AddressType":
						case "BoundaryLevel":
						case "CancelCode":
						case "DetailLevel":
						case "DocStatus":
						case "DocumentType":
						case "JurisdictionType":
						case "SeverityLevel":
						case "TaxType":
						case "TextCase":
						//Update Note : Added for 5.0
						case "TaxOverrideType":
						case "ServiceMode":
						//Update Note : Added for 5.1
						case "AccountingMethod":
						case "LogLevel":
						case "ConfirmationType":
                        case "CommunicationMode":
                        case "RequestType":
                        case "CertificateStatus":
                        case "ReviewStatus":
                        case "CertificateUsage":
                        case "FormatType":
                        case "CertificateReviewStatus":
                        case "CertificateRequestStatus":
                        case "CertificateRequestStage":
							Assert.IsTrue(defined, "Failed: " + type.Name);
							break;

							//Exception cases (we don't care about the result)
						case "ConfigDataProtector":
						case "DataProtector":
						case "Store":
						case "Security":
						case "UsernameToken":
						case "Password":
							Assert.IsTrue(true);
							break;

						default:
							Assert.Fail("Need to add a class to the constructor test: " + type.Name);
							break;
					}
				}
			}
		}
		
		[Test]
		public void COMVisibleAttributeValueTest()
		{
			//IF COMVisibleAttributeTest HAS NOT PASSED, THEN THIS TEST IS UNRELIABLE

			//test for value of custom ComVisible attribute
			bool isTrue;

			Assembly assembly = Assembly.LoadFrom("Avalara.AvaTax.Adapter.dll");
			Type[] types = assembly.GetExportedTypes();
			
			foreach (Type type in types)
			{
				if (!type.IsAbstract)
				{
					if (Attribute.IsDefined(type, typeof(ComVisibleAttribute)))
					{
						ComVisibleAttribute attribute = (ComVisibleAttribute)Attribute.GetCustomAttribute(type, typeof(ComVisibleAttribute));
						isTrue = attribute.Value;
						switch(type.Name)
						{
								//Avalara.AvaTax.Common.Configuration namespace
							case "XmlSerializerSectionHandler":
								Assert.IsFalse(isTrue, "Failed: " + type.Name);
								break;

								//Avalara.AvaTax.Adapter namespace
							case "BaseArrayList":
							case "BaseRequest":
							case "BaseResult":
							case "BaseSvc":
							case "ConfigReader":
							case "IsAuthorizedResult":
							case "PingResult":
							case "Profile":
							case "ReadOnlyArrayList":
							case "RequestSecurity":
							case "RequestTest":
							case "ServiceConfig":
							case "Utilities":
							case "Messages":
							case "Message":
							case "AvaTaxException":
							case "AdapterConfigException":
							case "Password":
							case "Security":
							case "UsernameToken":
							case "LogSvc":
								Assert.IsTrue(isTrue, "Failed: " + type.Name);
								break;

								//Avalara.AvaTax.Adapter.AddressService namespace
							case "Address":
							case "Addresses":
							case "AddressSvc":
							case "ValidateRequest":
							case "ValidateResult":
							case "ValidAddress":
							case "ValidAddresses":
                            case "AddressRequests":
                            case "SubmitAddressBatchRequest":
                            case "GetAddressBatchRequest":
                            case "CancelAddressBatchRequest":
                            case "SubmitAddressBatchResult":
                            case "GetAddressBatchResult":
                            case "CancelAddressBatchResult":
								Assert.IsTrue(isTrue, "Failed: " + type.Name);
								break;

								//Avalara.AvaTax.Adapter.TaxService namespace
							case "CancelTaxRequest":
							case "CommitTaxRequest":
							case "GetTaxHistoryRequest":
							case "GetTaxRequest":
							case "Line":
							case "PostTaxRequest":
							case "ReconcileTaxHistoryRequest":
							case "SearchTaxHistoryRequest":
							case "TaxSvc":
                            case "TaxRequests":
                            case "SubmitTaxBatchRequest":
                            case "GetTaxBatchRequest":
                            case "CancelTaxBatchRequest":
								Assert.IsTrue(isTrue, "Failed: " + type.Name);
								break;

							case "CancelTaxResult":
							case "CommitTaxResult":
							case "GetTaxHistoryResult":
							case "GetTaxResult":
							case "GetTaxResults":
							case "Lines":
							case "PostTaxResult":
							case "ReconcileTaxHistoryResult":
							case "SearchTaxHistoryResult":
							case "TaxDetail":
							case "TaxDetails":
							case "TaxLine":
							case "AdjustTaxRequest":
							case "AdjustTaxResult":								
							case "TaxLines":
							//Update Note : Added for 5.0
							case "TaxOverride":
							case "TaxSummary":
							//Update Note : Added for 5.1
							case "ApplyPaymentRequest":
							case "ApplyPaymentResult":
                            case "SubmitTaxBatchResult":
                            case "GetTaxBatchResult":
                            case "CancelTaxBatchResult":
								Assert.IsTrue(isTrue, "Failed: " + type.Name);
								break;

                            //Avalara.AvaTax.Adapter.AvaCert2Service namespace
                            case "AvaCert2Svc":
                            case "CustomerSaveRequest":
                            case "CustomerSaveResult":
                            case "Customer":
                            case "CertificateRequestInitiateRequest":
                            case "CertificateRequestInitiateResult":
                            case "CertificateGetRequest":
                            case "CertificateGetResult":
                            case "CertificateRequestGetRequest":
                            case "CertificateRequestGetResult":
                            case "CertificateImageGetRequest":
                            case "CertificateImageGetResult":
                            case "Certificate":
                            case "Certificates":
                            case "CertificateRequest":
                            case "CertificateRequests":
                            case "CertificateJurisdiction":
                            case "CertificateJurisdictions":
                                Assert.IsTrue(isTrue, "Failed: " + type.Name);
                                break;

								//Avalara.AvaTax.Proxies namespace
							case "ProxyBaseAddress":
							case "ProxyBaseResult":
							case "ProxyIsAuthorizedResult":
							case "ProxyPingResult":
							case "ProxyProfile":
							case "ProxyMessage":
                            case "ProxyBaseRequest":
								Assert.IsFalse(isTrue, "Failed: " + type.Name);
								break;

								//Avalara.AvaTax.Proxies.AddressSvcProxy namespace
							case "ProxyAddressSvc":
							case "ProxyValidateRequest":
							case "ProxyValidateResult":
							case "ProxyValidAddress":
                            case "ProxySubmitAddressBatchRequest":
                            case "ProxyGetAddressBatchRequest":
                            case "ProxyCancelAddressBatchRequest":
                            case "ProxySubmitAddressBatchResult":
                            case "ProxyGetAddressBatchResult":
                            case "ProxyCancelAddressBatchResult":
								Assert.IsFalse(isTrue, "Failed: " + type.Name);
								break;

								//Avalara.AvaTax.Proxies.TaxSvcProxy namespace
							case "ProxyCancelTaxRequest":
							case "ProxyCancelTaxResult":
							case "ProxyCommitTaxRequest":
							case "ProxyCommitTaxResult":
							case "ProxyGetTaxHistoryRequest":
							case "ProxyGetTaxHistoryResult":
							case "ProxyGetTaxRequest":
							case "ProxyGetTaxResult":
							case "ProxyLine":
							case "ProxyPostTaxRequest":
							case "ProxyPostTaxResult":
							case "ProxyReconcileTaxHistoryRequest":
							case "ProxyReconcileTaxHistoryResult":
							case "ProxySearchTaxHistoryRequest":
							case "ProxySearchTaxHistoryResult":
							case "ProxyTaxDetail":
							case "ProxyTaxLine":
							case "ProxyAdjustTaxRequest":
							case "ProxyAdjustTaxResult":							
							case "ProxyTaxSvc":
							//Update Note : Added for 5.0
							case "ProxyTaxOverride":
							case "ProxyServiceMode":
							//Update Note : Added for 5.1
							case "ProxyApplyPaymentRequest":
							case "ProxyApplyPaymentResult":
                            case "ProxySubmitTaxBatchRequest":
                            case "ProxyGetTaxBatchRequest":
                            case "ProxyCancelTaxBatchRequest":
                            case "ProxySubmitTaxBatchResult":
                            case "ProxyGetTaxBatchResult":
                            case "ProxyCancelTaxBatchResult":
								Assert.IsFalse(isTrue, "Failed: " + type.Name);
								break;

                            //Avalara.AvaTax.Proxies.AvaCert2SvcProxy namespace
                            case "ProxyAvaCert2Svc":
                            case "ProxyCustomerSaveRequest":
                            case "ProxyCustomerSaveResult":
                            case "ProxyCustomer":
                            case "ProxyCertificateRequestInitiateRequest":
                            case "ProxyCertificateRequestInitiateResult":
                            case "ProxyCertificateGetRequest":
                            case "ProxyCertificateGetResult":
                            case "ProxyCertificateRequestGetRequest":
                            case "ProxyCertificateRequestGetResult":
                            case "ProxyCertificateImageGetRequest":
                            case "ProxyCertificateImageGetResult":
                            case "ProxyCertificate":
                            case "ProxyCertificateRequest":
                            case "ProxyCertificateJurisdiction":
                                Assert.IsFalse(isTrue, "Failed: " + type.Name);
                                break;

								//Enums
							case "ProxyBoundaryLevel":
							case "ProxyCancelCode":
							case "ProxyDetailLevel":
							case "ProxyDocStatus":
							case "ProxyDocumentType":
							case "ProxyJurisdictionType":
							case "ProxySeverityLevel":
							case "ProxyTaxType":
							case "ProxyTextCase":
							//Update Note : Added for 5.0
							case "ProxyTaxOverrideType":
							//Update Note : Added for 5.1
							case "ProxyAccountingMethod":
							case "ProxySecurity":
							case "ProxyUsernameToken":
							case "ProxyPassword":
							case "ProxyConfirmationType":
                            case "ProxyCommunicationMode":
                            case "ProxyRequestType":
                            case "ProxyCertificateStatus":
                            case "ProxyReviewStatus":
                            case "ProxyCertificateUsage":
                            case "ProxyFormatType":
                            case "ProxyCertificateReviewStatus":
                            case "ProxyCertificateRequestStatus":
                            case "ProxyCertificateRequestStage":
								Assert.IsFalse(isTrue, "Failed: " + type.Name);
								break;

							case "AdapterConfigurationExceptionType":
							case "AddressType":
							case "BoundaryLevel":
							case "CancelCode":
							case "DetailLevel":
							case "DocStatus":
							case "DocumentType":
							case "JurisdictionType":
							case "SeverityLevel":
							case "TaxType":
							case "TextCase":
							//Update Note : Added for 5.0
							case "TaxOverrideType":
							case "ServiceMode":
							//Update Note : Added for 5.1
							case "AccountingMethod":
							case "LogLevel":
							case "ConfirmationType":
                            case "CommunicationMode":
                            case "RequestType":
                            case "CertificateStatus":
                            case "ReviewStatus":
                            case "CertificateUsage":
                            case "FormatType":
                            case "CertificateReviewStatus":
                            case "CertificateRequestStatus":
                            case "CertificateRequestStage":
								Assert.IsTrue(isTrue, "Failed: " + type.Name);
								break;

							default:
								Assert.Fail("Need to add a class to the constructor test: " + type.Name);
								break;
						}
						
					}
				}
			}
		}

		[Test]
		public void ClassInterfaceTest()
		{
			Assembly assembly = Assembly.LoadFrom("Avalara.AvaTax.Adapter.dll");
			bool defined = Attribute.IsDefined(assembly, typeof(ClassInterfaceAttribute));

			Assert.IsTrue(defined, "Expected to find ClassInterfaceAttribute at the Assembly level.");

			ClassInterfaceAttribute attribute = (ClassInterfaceAttribute)Attribute.GetCustomAttribute(assembly, typeof(ClassInterfaceAttribute));
			Assert.IsTrue(attribute.Value == ClassInterfaceType.AutoDual, "Expected ClassInterfaceType is AutoDual");
		}

		[Test]
		public void DispatchIdAttributeTest()
		{
			//IF COMVisibleAttributeValueTest & COMVisibleAttributeTest HAVE NOT PASSED, THEN THIS TEST IS UNRELIABLE

			//test for default member: DispId(0) and enumeration member: DispId(-4)

			bool isVisible;

			Assembly assembly = Assembly.LoadFrom("Avalara.AvaTax.Adapter.dll");
			Type[] types = assembly.GetExportedTypes();
			
			foreach (Type type in types)
			{
				if (!type.IsAbstract)
				{
					if (Attribute.IsDefined(type, typeof(ComVisibleAttribute)))
					{
						ComVisibleAttribute attribute = (ComVisibleAttribute)Attribute.GetCustomAttribute(type, typeof(ComVisibleAttribute));
						isVisible = attribute.Value;

						if (isVisible)
						{
							MemberInfo[] memberInfos = type.GetMembers();

							foreach (MemberInfo memberInfo in memberInfos)
							{
								Type declaringType = memberInfo.DeclaringType;
								//we're only inspecting explicit (not inherited members)
								if ((type == declaringType) && 
									(memberInfo.MemberType != MemberTypes.Constructor) && 
									(memberInfo.MemberType != MemberTypes.Field))
								{
									if (Attribute.IsDefined(memberInfo, typeof(DispIdAttribute)))
									{
										DispIdAttribute dispAttribute = (DispIdAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(DispIdAttribute));
										int dispId = dispAttribute.Value;
										if (string.Compare(memberInfo.Name, "GetEnumerator", true) == 0)
										{
											Assert.AreEqual(-4, dispId, string.Format("{0}:{1} must have a DispId of -4", type.Name, memberInfo.Name));
										}
										else if(string.Compare(memberInfo.Name, "Item", true) == 0)
										{
											Assert.AreEqual(0, dispId, string.Format("{0}:{1} must have a DispId of 0", type.Name, memberInfo.Name));
										}
										else
										{
											Assert.IsTrue(dispId > 0, string.Format("{0}:{1} must have a DispId greater than 0", type.Name, memberInfo.Name));
										}
									}
									else
									{
										string subString = (memberInfo.Name.Length >= 4 ? memberInfo.Name.Substring(0, 4) : memberInfo.Name);
										if ((memberInfo.MemberType == MemberTypes.Method) && 
											(string.Compare(subString, "get_", false) == 0) || (string.Compare(subString, "set_", false) == 0))
										{
											/*
											 * Reflection gives us each of the property accessors as Methods then gives us the overriding Property as well.
											 * So for a given Property, we end up checking the DispId on the Property, on the Get accessor, and if it exists,
											 * on the Set accessor.  We don't want to set the DispId on the accessors (it isn't necessary), so we're going to 
											 * ignore a case where we look like an accessor.  If a method out there exists with a "get_" or "set_" prefix
											 * (case-sensitive), then we would fall in here. Bad.  BUT, it would break coding standards to have a method which begins
											 * with a lower case letter and/or includes an underscore.  So the chance of that happening should be slim to none, 
											 * making this check acceptable.
											*/
										}
										else
										{
											Assert.Fail(string.Format("{0}:{1} is public and ComVisible but has no DispId", type.Name, memberInfo.Name));
										}
									}
									
								}
							}
						}
					}
				}
			}
		}

		[Test]
		[ExpectedException(typeof(AdapterConfigException))]
		public void AdapterConfigExceptionTest()
		{
			_addressSvc.Configuration.Url = string.Empty;
			_addressSvc.Ping(string.Empty);
		}
	}
}