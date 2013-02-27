using System;
using System.Collections;
using System.Configuration;
using System.Text;
using System.Threading;
using Avalara.AvaTax.Adapter;
using Avalara.AvaTax.Adapter.AddressService;
using Avalara.AvaTax.Adapter.TaxService;
using NUnit.Framework;

namespace Test.Avalara.AvaTax.Adapter
{
	/// <summary>
	/// Summary description for TaxSvcTest.
	/// </summary>
	[TestFixture]
	public class AdapterTaxTest
	{
		private TaxSvc _taxSvc;
	
		[SetUp]
		public void Init()
		{
			try
			{				
				_taxSvc = new TaxSvc();
				_taxSvc.Configuration.Url = ConfigurationSettings.AppSettings.Get("Url");
				
				//fill these only if they haven't been loaded from Avalara.AvaTax.Adapter.dll.config
				if (_taxSvc.Configuration.Security.Account == null || _taxSvc.Configuration.Security.Account.Length == 0)
				{
					_taxSvc.Configuration.Security.Account = ConfigurationSettings.AppSettings.Get("account");
				}
				if (_taxSvc.Configuration.Security.License == null || _taxSvc.Configuration.Security.License.Length == 0)
				{
					_taxSvc.Configuration.Security.License = ConfigurationSettings.AppSettings.Get("key");
				}

				//_taxSvc.Configuration.Security.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("timeout"));
				_taxSvc.Profile.Client = "NUnit TaxSvcTest";
                //rahul - put this in to workaround a Service bug in ReconciliationTaxHistory 
                //where we determine which implementation to call based on the Version (derived from Profile.Name) passed in
                _taxSvc.Profile.Name = "5.9";


			} 
			catch (Exception ex)
			{
				
				Assert.Fail("TaxSvc failed creation: " + ex.Message + " : " + ex.StackTrace);
			}
		}

		[TearDown]
		public void Dispose()
		{
			try
			{ 
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

		#region Supporting Methods

		public GetTaxRequest CreateDefaultGetTaxRequest()
		{
			GetTaxRequest request = new GetTaxRequest();
			request.CompanyCode = "DEFAULT";
			request.DocDate = DateTime.Today;
			request.DocType = DocumentType.SalesInvoice;			
			request.DocCode = "Adapter_TaxSvcTest_" + DateTime.Now.Ticks;
			request.CustomerCode = "Purchaser01";
			request.DetailLevel = DetailLevel.Tax;
			request.CustomerUsageType = "Purchaser Type";
			request.SalespersonCode = "Salesperson Code";
			request.ExchangeRate = 1.0m;
		    request.ExchangeRateEffDate = DateTime.Today;
			return request;
		}

		public Address CreateDefaultAddress1()
		{
			Address address = new Address();
			address.Line1 = "900 Winslow Way E";
			address.City = "Bainbridge Island";
			address.Region = "WA";
			address.PostalCode = "98110-2450";

			return address;
		}

		public Address CreateDefaultAddress2()
		{
			Address address = new Address();
			address.Line1 = "5474 NE Foster Rd";
			address.City = "Bainbridge Island";
			address.Region = "WA";
			address.PostalCode = "98110-1612";

			return address;
		}

       

        public Address CreateDefaultAddressForTaxOverRide1()
        {
            Address address = new Address();
            address.Line1 = "435 Ericksen Ave";
            address.City = "Bainbridge Island";
            address.Region = "WA";
            address.PostalCode = "98110";

            return address;
        }

        public Address CreateDefaultAddressForTaxOverRide2()
        {
            Address address = new Address();
            address.Line1 = "76 Belinda Pkwy";
            address.City = "Mount Juliet";
            address.Region = "TN";
            address.PostalCode = "37122-3772";

            return address;
        }

       //  TaxSvcWrapper.AddLine(taxRequest, "1", "SKU-001", "", 1m, 0m);
        public Line CreateDefaultGetTaxRequestLineForTaxOverRide(string no, string itemCode, string taxCode, double qty, decimal amount)
        {
            Line line = new Line();
            line.No = no;
            line.Amount = amount;
            line.Qty = qty;

            return line;
        }

		public Address CreateDefaultShipFromAddress()
		{
			Address address = CreateDefaultAddress1();
			//address.AddressCode = "SHIP_FROM_01";

			return address;
		}

		public Address CreateDefaultShipToAddress()
		{
			Address address = CreateDefaultAddress2();
			//address.AddressCode = "SHIP_TO_01";

			return address;
		}

        public Address CreateDefaultShippingAddressWithLatLong()
        {
            Address address = new Address
                                  {
                                      Longitude = "-122.510359",
                                      Latitude = "47.624972"
                                  };
            return address;
        }

        public Address CreateDefaultShipToAddressWithLatLong()
        {
            Address address = new Address
            {
                Longitude = "-122.5362",
                Latitude = "47.6411"
            };
            return address;
        }

		public Line CreateDefaultGetTaxRequestLine()
		{
			return CreateDefaultGetTaxRequestLine(1, false);
		}

		public Line CreateDefaultGetTaxRequestLine(int lineNo)
		{
			return CreateDefaultGetTaxRequestLine(lineNo, false);
		}

		public Line CreateDefaultGetTaxRequestLine(int lineNo, bool discounted)
		{
			Line line = new Line();
			line.No = lineNo.ToString();
			line.Amount = 15.00m * lineNo;
			line.Discounted = discounted;
			line.Qty = 1 * (double)lineNo;
			line.ItemCode = "20000"; // Clothing
			line.Ref1 = "Reference Field 1";
			line.Ref2 = "Reference Field 2";
			line.RevAcct = "Revenue Account Field";

			return line;
		}

		public PostTaxRequest CreatePostTaxRequest(GetTaxRequest getTaxRequest, GetTaxResult getTaxResult)
		{
			PostTaxRequest postTaxRequest = new PostTaxRequest();
			postTaxRequest.CompanyCode = getTaxRequest.CompanyCode;
			postTaxRequest.DocCode = getTaxRequest.DocCode;
			postTaxRequest.DocType = getTaxRequest.DocType;
			postTaxRequest.DocDate = getTaxRequest.DocDate;
			foreach (Line line in getTaxRequest.Lines)
			{
				postTaxRequest.TotalAmount += line.Amount;
			}
			foreach (TaxLine taxLine in getTaxResult.TaxLines)
			{
				postTaxRequest.TotalTax += taxLine.Tax;
			}

			return postTaxRequest;
		}

		#endregion

        /// <summary>
        /// Ticket #16099: If the Service is unreachable (network is down or URL is incorrect), 
        /// then we are incorrectly reporting a NullReferenceException
        /// </summary>
        [Test]
        public void InvalidUrlTest()
        {
            try
            {
                _taxSvc.Configuration.Url = "https://avatax.avalara.net1";
                PingResult pingResult = _taxSvc.Ping("");

                foreach (Message message in pingResult.Messages)
                {
                    Console.WriteLine(message.Name + ": " + message.Summary);
                }
                Assert.AreEqual(SeverityLevel.Error, pingResult.ResultCode);
                Assert.AreEqual("System.Net.WebException", pingResult.Messages[0].Name);
                Assert.AreEqual("The remote name could not be resolved: 'avatax.avalara.net1'",
                                pingResult.Messages[0].Summary);
            }
            catch (Exception ex)
            {
                Assert.Fail("InvalidUrlTest: " + ex.Message + " : " + ex.StackTrace);
            }
        }

		[Test]
		public void PingTest()
		{
			PingResult pingResult;

			try
			{
				pingResult = _taxSvc.Ping("");
				string[] versionSplit = pingResult.Version.Split('.');
				Assert.IsTrue(versionSplit.Length == 4,"Version split should return four parts");

				try
				{
					int major = int.Parse(versionSplit[0]);
					Assert.IsTrue(major >= 4,"Major version should be greater than 4");
				}
				catch (Exception e)
				{
					Assert.Fail("converting major part of version '{0}' to int returned exception {1}",versionSplit[0],e.Message);
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
			IsAuthorizedResult isAuthorizedResult;

			try
			{
				isAuthorizedResult = _taxSvc.IsAuthorized("GetTax");

				Assert.AreEqual("GetTax", isAuthorizedResult.Operations);
				Assert.IsTrue(isAuthorizedResult.Expires > new DateTime(9997, 12, 31));
				Assert.AreEqual(SeverityLevel.Success, isAuthorizedResult.ResultCode);
				Assert.AreEqual(0, isAuthorizedResult.Messages.Count);
			}  
			catch (Exception ex)
			{
				Assert.Fail("IsAuthorizedTest: " + ex.Message + " : " + ex.StackTrace);
			}
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException))]
		public void ValidGetTaxRequestTest()
		{
            try
            {
                GetTaxRequest request = new GetTaxRequest();
                GetTaxResult getTaxResult = _taxSvc.GetTax(request);

                foreach (Message message in getTaxResult.Messages)
                {
                    Console.WriteLine(message.Name + ": " + message.Summary);
                }
                Assert.AreEqual(SeverityLevel.Error, getTaxResult.ResultCode);
                Assert.AreEqual("RequiredError", getTaxResult.Messages[0].Name);
            }
            catch (Exception ex)
            {
                Assert.Fail("ValidGetTaxRequestTest failed: " + ex.Message + " : " + ex.StackTrace);
            }
		}

		[Test]
		public void GetTaxTest()
		{
			GetTaxRequest request = CreateDefaultGetTaxRequest();
			request.OriginAddress = CreateDefaultShipFromAddress();
			request.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			request.Lines.Add(line);

			GetTaxResult result = _taxSvc.GetTax(request);

			if (result.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(result));
			}
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
            try
            {
                //retest with no lines
                //20070618 j.wadlow - no longer allow gettax with 0 lines
                request.Lines.Clear();
                result = _taxSvc.GetTax(request);
                Assert.AreEqual(SeverityLevel.Error, result.ResultCode, "Test case should failed with no lines.");
                Assert.AreEqual("RangeError", result.Messages[0].Name);
            }
            catch (Exception ex)
            {
                Assert.Fail("GetTaxTest failed: " + ex.Message + " : " + ex.StackTrace);
            }
		}


        [Test]
        public void GetTaxTestWithLatLongAddress()
        {
            GetTaxRequest request = CreateDefaultGetTaxRequest();
            request.DetailLevel = DetailLevel.Diagnostic;
            request.OriginAddress = CreateDefaultShippingAddressWithLatLong();
            request.DestinationAddress = CreateDefaultShipToAddressWithLatLong();

            Line line = CreateDefaultGetTaxRequestLine();
            request.Lines.Add(line);

            GetTaxResult result = _taxSvc.GetTax(request);

            if (result.Messages.Count > 0)
            {
                Console.WriteLine(FormatMessages(result));
            }
            Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
            Assert.AreEqual(15,result.TotalTaxable);
            Assert.AreEqual(1.3, result.TotalTax);
            Assert.AreEqual(1,result.TaxLines.Count);
            Assert.AreEqual(15,result.TaxLines[0].Taxable);
            Assert.AreEqual(1.3, result.TaxLines[0].Tax);
        }

        /// <summary>
        /// StateAssignedNo is returned by GetTax and GetTaxHistory in the TaxLine and TaxSummary collections
        /// </summary>
        [Test]
        public void TaxDetailStateAssignedNoTest()
        {
            // 1. StateAssignedNo is returned by GetTax
            GetTaxRequest request = CreateDefaultGetTaxRequest();
            request.OriginAddress = CreateDefaultShipFromAddress();

            Address address = new Address();
            address.Line1 = "400 Embassy Row NE Ste 580";
            address.City = "Atlanta";
            address.Region = "GA";
            address.PostalCode = "30328-7000";
            request.DestinationAddress = address;

            Line line = CreateDefaultGetTaxRequestLine();
            request.Lines.Add(line);

            request.DetailLevel = DetailLevel.Diagnostic;
            GetTaxResult result = _taxSvc.GetTax(request);

            if (result.Messages.Count > 0)
            {
                Console.WriteLine(FormatMessages(result));
            }
            Assert.AreEqual(SeverityLevel.Success, result.ResultCode);

            bool isStateAssignedNo = false;
            if (result.TaxSummary.Count > 0)
            {
                foreach (TaxDetail detail in result.TaxSummary)
                {
                    if (detail.StateAssignedNo != null && detail.StateAssignedNo.Trim().Length > 0)
                    {
                        Assert.AreEqual("060", detail.StateAssignedNo);
                        isStateAssignedNo = true;
                    }
                }
            }
            if (!isStateAssignedNo)
            {
                Assert.Fail("Failed to fetch State Assigned No for the given address");
            }

            isStateAssignedNo = false;
            if (result.TaxLines.Count > 0 && result.TaxLines[0].TaxDetails.Count > 0)
            {
                foreach (TaxDetail detail in result.TaxLines[0].TaxDetails)
                {
                    if (detail.StateAssignedNo != null && detail.StateAssignedNo.Trim().Length > 0)
                    {
                        Assert.AreEqual("060", detail.StateAssignedNo);
                        isStateAssignedNo = true;
                    }
                }
            }
            if (!isStateAssignedNo)
            {
                Assert.Fail("Failed to fetch State Assigned No for the given address");
            }

            // 2. StateAssignedNo is returned by GetTaxHistory
            GetTaxHistoryRequest historyRequest = new GetTaxHistoryRequest();
            historyRequest.CompanyCode = request.CompanyCode;
            historyRequest.DocType = request.DocType;
            historyRequest.DocCode = request.DocCode;
            historyRequest.DetailLevel = DetailLevel.Diagnostic;

            GetTaxHistoryResult historyResult = _taxSvc.GetTaxHistory(historyRequest);

            if (historyResult.Messages.Count > 0)
            {
                Console.WriteLine(FormatMessages(historyResult));
            }
            Assert.AreEqual(SeverityLevel.Success, historyResult.ResultCode);
            Assert.IsNotNull(historyResult.GetTaxRequest);
            Assert.IsNotNull(historyResult.GetTaxResult);

            isStateAssignedNo = false;
            if (historyResult.GetTaxResult.TaxSummary.Count > 0)
            {
                foreach (TaxDetail detail in historyResult.GetTaxResult.TaxSummary)
                {
                    if (detail.StateAssignedNo != null && detail.StateAssignedNo.Trim().Length > 0)
                    {
                        Assert.AreEqual("060", detail.StateAssignedNo);
                        isStateAssignedNo = true;
                    }
                }
            }
            if (!isStateAssignedNo)
            {
                Assert.Fail("Failed to fetch State Assigned No for the given address");
            }

            isStateAssignedNo = false;
            if (historyResult.GetTaxResult.TaxLines.Count > 0 && historyResult.GetTaxResult.TaxLines[0].TaxDetails.Count > 0)
            {
                foreach (TaxDetail detail in historyResult.GetTaxResult.TaxLines[0].TaxDetails)
                {
                    if (detail.StateAssignedNo != null && detail.StateAssignedNo.Trim().Length > 0)
                    {
                        Assert.AreEqual("060", detail.StateAssignedNo);
                        isStateAssignedNo = true;
                    }
                }
            }
            if (!isStateAssignedNo)
            {
                Assert.Fail("Failed to fetch State Assigned No for the given address");
            }
        }
        
        //[Test]
        //public void PostTaxConfirmationTest()
        //{
        //    GetTaxRequest getTaxRequest = CreateDefaultGetTaxRequest();
        //    getTaxRequest.DocCode = "Adapter_PostTest_" + DateTime.Now.Ticks;
        //    getTaxRequest.DocType = DocumentType.SalesInvoice;
        //    getTaxRequest.CustomerCode = "PostTest01";
        //    getTaxRequest.OriginAddress = CreateDefaultShipFromAddress();
        //    getTaxRequest.DestinationAddress = CreateDefaultShipToAddress();

        //    Line line = CreateDefaultGetTaxRequestLine();
        //    getTaxRequest.Lines.Add(line);

        //    GetTaxResult getTaxResult = _taxSvc.GetTax(getTaxRequest);
        //    Assert.AreEqual(SeverityLevel.Success, getTaxResult.ResultCode, "Expected GetTax to pass");

        //    // 1. PostTax Error: ConfirmationType.Required
        //    PostTaxRequest postTaxRequest = CreatePostTaxRequest(getTaxRequest, getTaxResult);
        //    postTaxRequest.TotalAmount = 1;
        //    postTaxRequest.Confirmation = ConfirmationType.Required;
        //    PostTaxResult postTaxResult = _taxSvc.PostTax(postTaxRequest);
		
        //    foreach (Message message in postTaxResult.Messages)
        //    {
        //        Console.WriteLine(message.Name + ": " + message.Summary);
        //    }
        //    Assert.AreEqual(SeverityLevel.Error, postTaxResult.ResultCode);
        //    Assert.AreEqual("OutOfBalanceError", postTaxResult.Messages[0].Name);
        //    Assert.AreEqual("Document was posted, but TotalAmount is out of balance.", postTaxResult.Messages[0].Summary);

        //    GetTaxHistoryRequest historyRequest = new GetTaxHistoryRequest();
        //    historyRequest.CompanyCode = postTaxRequest.CompanyCode;
        //    historyRequest.DocType = postTaxRequest.DocType;
        //    historyRequest.DocCode = postTaxRequest.DocCode;
        //    historyRequest.DetailLevel = DetailLevel.Tax;
			
        //    GetTaxHistoryResult historyResult = _taxSvc.GetTaxHistory(historyRequest);
        //    Assert.AreEqual(SeverityLevel.Success, historyResult.ResultCode);
        //    Assert.IsNotNull(historyResult.GetTaxResult);
        //    Assert.AreEqual(DocStatus.Saved, historyResult.GetTaxResult.DocStatus);

        //    // 2. PostTax Warning: ConfirmationType.Optional
        //    postTaxRequest.Confirmation = ConfirmationType.Optional;
        //    postTaxResult = _taxSvc.PostTax(postTaxRequest);
		
        //    foreach (Message message in postTaxResult.Messages)
        //    {
        //        Console.WriteLine(message.Name + ": " + message.Summary);
        //    }
        //    Assert.AreEqual(SeverityLevel.Warning, postTaxResult.ResultCode);
        //    Assert.AreEqual("OutOfBalanceWarning", postTaxResult.Messages[0].Name);
        //    Assert.AreEqual("Document was posted, but TotalAmount is out of balance.", postTaxResult.Messages[0].Summary);

        //    historyResult = _taxSvc.GetTaxHistory(historyRequest);
        //    Assert.AreEqual(SeverityLevel.Success, historyResult.ResultCode);
        //    Assert.IsNotNull(historyResult.GetTaxResult);
        //    Assert.AreEqual(DocStatus.Posted, historyResult.GetTaxResult.DocStatus);

        //    // 3. PostTax Success: ConfirmationType.None
        //    postTaxRequest.Confirmation = ConfirmationType.None;
        //    postTaxResult = _taxSvc.PostTax(postTaxRequest);

        //    foreach (Message message in postTaxResult.Messages)
        //    {
        //        Console.WriteLine(message.Name + ": " + message.Summary);
        //    }
        //    Assert.AreEqual(SeverityLevel.Success, postTaxResult.ResultCode);

        //    historyResult = _taxSvc.GetTaxHistory(historyRequest);
        //    Assert.AreEqual(SeverityLevel.Success, historyResult.ResultCode);
        //    Assert.IsNotNull(historyResult.GetTaxResult);
        //    Assert.AreEqual(DocStatus.Posted, historyResult.GetTaxResult.DocStatus);
        //}

		[Test]
		//[Ignore("Need to track down issue")]
		public void TaxOverrideGetTaxTest()
		{
			GetTaxRequest request = CreateDefaultGetTaxRequest();
			request.OriginAddress = CreateDefaultShipFromAddress();
			request.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			request.Lines.Add(line);
			
			//TaxOverride
			//request.IsTotalTaxOverriden = true;
			//request.TotalTaxOverride = 5m;
			request.TaxOverride.TaxOverrideType = TaxOverrideType.TaxAmount;
			request.TaxOverride.TaxAmount = 5m;
			request.TaxOverride.Reason = "Return";

			GetTaxResult result = _taxSvc.GetTax(request);

			if (result.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(result));
			}
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode);

			result = _taxSvc.GetTax(request);			
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode, "Test case should pass with TaxOverride amount .");
			Assert.AreEqual(5m, result.TotalTax);
		}

		[Test]
		public void TaxOverrideOnLineGetTaxTest()
		{
			GetTaxRequest request = CreateDefaultGetTaxRequest();
			request.OriginAddress = CreateDefaultShipFromAddress();
			request.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			request.Lines.Add(line);
			
			//TaxOverride on Line
			request.Lines[0].TaxOverride.TaxOverrideType = TaxOverrideType.TaxAmount;
			request.Lines[0].TaxOverride.TaxAmount = 5m;
			request.Lines[0].TaxOverride.Reason = "Return";
			
			GetTaxResult result = _taxSvc.GetTax(request);

			if (result.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(result));
			}
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode);

			result = _taxSvc.GetTax(request);			
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode, "Test case should pass with TaxOverride amount .");
			Assert.AreEqual(5m, result.TotalTax);
		}

		[Test]
		public void TaxRegionIdOnGetTaxTest()
		{
			GetTaxRequest request = CreateDefaultGetTaxRequest();
			request.OriginAddress = CreateDefaultShipFromAddress();
			request.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			request.Lines.Add(line);
			
			//TaxOverride on Line
            request.DestinationAddress.TaxRegionId = 1051190;
			GetTaxResult result = _taxSvc.GetTax(request);

			if (result.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(result));
			}
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode);

			result = _taxSvc.GetTax(request);			
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
            Assert.AreEqual("KITSAP", result.TaxLines[0].TaxDetails[1].JurisName);
		}

		[Test]
		public void GetTaxTest2()
		{
			//test that GetTax passes if Address is set at the line level only
			GetTaxRequest request = CreateDefaultGetTaxRequest();	

			Line line = CreateDefaultGetTaxRequestLine();
			request.Lines.Add(line);
			line.OriginAddress = CreateDefaultShipFromAddress();
			line.DestinationAddress = CreateDefaultShipToAddress();

			GetTaxResult result = _taxSvc.GetTax(request);

			if (result.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(result));
			}
			//a bug that forces a request to have addresses is in the server,
			// when this test fails (request succeeds) then we'll know to fix the test
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode, "Expected success.");
		}

        [Test]
        public void GetTaxGUIDDocCodeTest()
        {
            GetTaxRequest getTaxRequest = new GetTaxRequest();
            getTaxRequest.CompanyCode = "DEFAULT";
            getTaxRequest.DocDate = DateTime.Today;
            getTaxRequest.DocType = DocumentType.SalesInvoice;
            getTaxRequest.DocCode = "Adapter_TaxSvcTest_" + DateTime.Now.Ticks;
            getTaxRequest.CustomerCode = "Purchaser01";
            getTaxRequest.DetailLevel = DetailLevel.Tax;
            getTaxRequest.OriginAddress = CreateDefaultShipFromAddress();
            getTaxRequest.DestinationAddress = CreateDefaultShipToAddress();

            Line line = CreateDefaultGetTaxRequestLine();
            getTaxRequest.Lines.Add(line);

            GetTaxResult getTaxResult = _taxSvc.GetTax(getTaxRequest);
            if (getTaxResult.Messages.Count > 0)
            {
                Console.WriteLine(FormatMessages(getTaxResult));
            }
            Assert.AreEqual(SeverityLevel.Success, getTaxResult.ResultCode, "Expected successful GetTax");
            Assert.IsTrue(getTaxResult.DocCode != "", "Expected DocCode in GUID form");
		}

		[Test]
		//[Ignore("ISSUE: Is this an error?")]
		public void GetTaxWithNoAddressesTest()
        {
                GetTaxRequest request = CreateDefaultGetTaxRequest();

                GetTaxResult result = _taxSvc.GetTax(request);

                if (result.Messages.Count > 0)
                {
                    Console.WriteLine(FormatMessages(result));
                }
                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                Assert.AreEqual("TaxAddressError", result.Messages[0].Name);
           
		}
        [Test]
        //[Ignore("ISSUE: Is this an error?")]
        public void GetTaxWithNoLinesTest()
        {
            GetTaxRequest request = CreateDefaultGetTaxRequest();
            request.DestinationAddress = CreateDefaultAddress1();
            GetTaxResult result = _taxSvc.GetTax(request);

            if (result.Messages.Count > 0)
            {
                Console.WriteLine(FormatMessages(result));
            }
            Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
            Assert.AreEqual("RangeError", result.Messages[0].Name);
            Assert.AreEqual("Lines is expected to be between 1 and 10000.", result.Messages[0].Summary);

        }
		[Test]
        //[ExpectedException(typeof(ArgumentException))]
		public void ValidCancelTaxRequestTest()
        {
            try
            {
                CancelTaxRequest cancelTaxRequest = new CancelTaxRequest();
                CancelTaxResult cancelTaxResult = _taxSvc.CancelTax(cancelTaxRequest);

                foreach (Message message in cancelTaxResult.Messages)
                {
                    Console.WriteLine(message.Name + ": " + message.Summary);
                }
                Assert.AreEqual(SeverityLevel.Error, cancelTaxResult.ResultCode);
                Assert.AreEqual("RequiredError", cancelTaxResult.Messages[0].Name);
            }
            catch (Exception ex)
            {
                Assert.Fail("ValidCancelTaxRequestTest failed: " + ex.Message + " : " + ex.StackTrace);
            }
		}

		[Test]
		public void CancelTaxTest()
		{
			GetTaxRequest getTaxRequest = CreateDefaultGetTaxRequest();
			getTaxRequest.DocCode = "Adapter_CancelTest_" + DateTime.Now.Ticks;
			getTaxRequest.DocType = DocumentType.SalesInvoice;
			getTaxRequest.CustomerCode = "CancelTest01";
			getTaxRequest.OriginAddress = CreateDefaultShipFromAddress();
			getTaxRequest.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			getTaxRequest.Lines.Add(line);

			GetTaxResult getTaxResult = _taxSvc.GetTax(getTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, getTaxResult.ResultCode, "Expected GetTax to pass");

			CancelTaxRequest cancelTaxRequest = new CancelTaxRequest();
			cancelTaxRequest.CompanyCode = getTaxRequest.CompanyCode;
			cancelTaxRequest.DocType = getTaxRequest.DocType;
			cancelTaxRequest.DocCode = getTaxRequest.DocCode;
			cancelTaxRequest.CancelCode = CancelCode.DocDeleted;

			CancelTaxResult cancelTaxResult = _taxSvc.CancelTax(cancelTaxRequest);

			if (cancelTaxResult.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(cancelTaxResult));
			}
			Assert.AreEqual(SeverityLevel.Success, cancelTaxResult.ResultCode);
		}

		[Test]
		public void CancelTaxVerifyCancelledTest()
		{
			GetTaxRequest getTaxRequest = CreateDefaultGetTaxRequest();
			getTaxRequest.DocCode = "Adapter_PostTest_" + DateTime.Now.Ticks;
			getTaxRequest.DocType = DocumentType.SalesInvoice;
			getTaxRequest.CustomerCode = "PostTest02";
			getTaxRequest.OriginAddress = CreateDefaultShipFromAddress();
			getTaxRequest.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			getTaxRequest.Lines.Add(line);

			GetTaxResult getTaxResult = _taxSvc.GetTax(getTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, getTaxResult.ResultCode, "Expected successful GetTax");

			CancelTaxRequest cancelTaxRequest = new CancelTaxRequest();
            cancelTaxRequest.CompanyCode = getTaxRequest.CompanyCode;
            cancelTaxRequest.DocType = getTaxRequest.DocType;
            cancelTaxRequest.DocCode = getTaxRequest.DocCode;

			cancelTaxRequest.CancelCode = CancelCode.DocDeleted;

			CancelTaxResult cancelTaxResult = _taxSvc.CancelTax(cancelTaxRequest);

			if (cancelTaxResult.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(cancelTaxResult));
			}
			Assert.AreEqual(SeverityLevel.Success, cancelTaxResult.ResultCode, "Expected successful CancelTax");

			GetTaxHistoryRequest historyRequest = new GetTaxHistoryRequest();
            historyRequest.CompanyCode = getTaxRequest.CompanyCode;
            historyRequest.DocType = getTaxRequest.DocType;
            historyRequest.DocCode = getTaxRequest.DocCode;

			historyRequest.DetailLevel = DetailLevel.Document;
			GetTaxHistoryResult historyResult = _taxSvc.GetTaxHistory(historyRequest);
			Assert.AreEqual(SeverityLevel.Error, historyResult.ResultCode, "Not expecting document to exist because of DocDeleted cancel code");
		}
	
		[Test]
		public void CancelTaxVerifyCancelledTest2()
		{
			GetTaxRequest getTaxRequest = CreateDefaultGetTaxRequest();
			getTaxRequest.DocCode = "Adapter_PostTest_" + DateTime.Now.Ticks;
			getTaxRequest.DocType = DocumentType.SalesInvoice;
			getTaxRequest.CustomerCode = "PostTest02";
			getTaxRequest.OriginAddress = CreateDefaultShipFromAddress();
			getTaxRequest.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			getTaxRequest.Lines.Add(line);

			GetTaxResult getTaxResult = _taxSvc.GetTax(getTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, getTaxResult.ResultCode, "Expected successful GetTax");

			CancelTaxRequest cancelTaxRequest = new CancelTaxRequest();
            cancelTaxRequest.CompanyCode = getTaxRequest.CompanyCode;
            cancelTaxRequest.DocType = getTaxRequest.DocType;
            cancelTaxRequest.DocCode = getTaxRequest.DocCode;

			cancelTaxRequest.CancelCode = CancelCode.PostFailed;
			CancelTaxResult cancelTaxResult = _taxSvc.CancelTax(cancelTaxRequest);

            if (cancelTaxResult.Messages.Count > 0)
            {
                Console.WriteLine(FormatMessages(cancelTaxResult));
            }
			Assert.AreEqual(SeverityLevel.Error, cancelTaxResult.ResultCode, "Expected error because document has not been posted");
		}
	
		[Test]
		public void CancelTaxVerifyCancelledAfterPostTest()
		{
			GetTaxRequest getTaxRequest = CreateDefaultGetTaxRequest();
			getTaxRequest.DocCode = "Adapter_PostTest_" + DateTime.Now.Ticks;
			getTaxRequest.DocType = DocumentType.SalesInvoice;
			getTaxRequest.CustomerCode = "PostTest02";
			getTaxRequest.OriginAddress = CreateDefaultShipFromAddress();
			getTaxRequest.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			getTaxRequest.Lines.Add(line);

			GetTaxResult getTaxResult = _taxSvc.GetTax(getTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, getTaxResult.ResultCode, "Expected successful GetTax");

			PostTaxRequest postTaxRequest = new PostTaxRequest();
            postTaxRequest.CompanyCode = getTaxRequest.CompanyCode;
            postTaxRequest.DocType = getTaxRequest.DocType;
            postTaxRequest.DocCode = getTaxRequest.DocCode;

			postTaxRequest.DocDate = getTaxResult.DocDate;
			postTaxRequest.TotalAmount = getTaxResult.TotalAmount;
			postTaxRequest.TotalTax = getTaxResult.TotalTax;
			PostTaxResult postTaxResult = _taxSvc.PostTax(postTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, postTaxResult.ResultCode);

			CancelTaxRequest cancelTaxRequest = new CancelTaxRequest();
            cancelTaxRequest.CompanyCode = getTaxRequest.CompanyCode;
            cancelTaxRequest.DocType = getTaxRequest.DocType;
            cancelTaxRequest.DocCode = getTaxRequest.DocCode;

			cancelTaxRequest.CancelCode = CancelCode.DocDeleted;
			CancelTaxResult cancelTaxResult = _taxSvc.CancelTax(cancelTaxRequest);

            if (cancelTaxResult.Messages.Count > 0)
            {
                Console.WriteLine(FormatMessages(cancelTaxResult));
            }
			Assert.AreEqual(SeverityLevel.Success, cancelTaxResult.ResultCode, "Expected successful CancelTax");

			GetTaxHistoryRequest historyRequest = new GetTaxHistoryRequest();
            historyRequest.CompanyCode = getTaxRequest.CompanyCode;
            historyRequest.DocType = getTaxRequest.DocType;
            historyRequest.DocCode = getTaxRequest.DocCode;

			historyRequest.DetailLevel = DetailLevel.Document;
			GetTaxHistoryResult historyResult = _taxSvc.GetTaxHistory(historyRequest);
			Assert.AreEqual(SeverityLevel.Error, historyResult.ResultCode);
		}
	
		[Test]
		public void CancelTaxVerifyCancelledAfterPostTest2()
		{
			GetTaxRequest getTaxRequest = CreateDefaultGetTaxRequest();
			getTaxRequest.DocCode = "Adapter_PostTest_" + DateTime.Now.Ticks;
			getTaxRequest.DocType = DocumentType.SalesInvoice;
			getTaxRequest.CustomerCode = "PostTest02";
			getTaxRequest.OriginAddress = CreateDefaultShipFromAddress();
			getTaxRequest.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			getTaxRequest.Lines.Add(line);

			GetTaxResult getTaxResult = _taxSvc.GetTax(getTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, getTaxResult.ResultCode, "Expected successful GetTax");

			PostTaxRequest postTaxRequest = new PostTaxRequest();
            postTaxRequest.CompanyCode = getTaxRequest.CompanyCode;
            postTaxRequest.DocType = getTaxRequest.DocType;
            postTaxRequest.DocCode = getTaxRequest.DocCode;

			postTaxRequest.DocDate = getTaxResult.DocDate;
			postTaxRequest.TotalAmount = getTaxResult.TotalAmount;
			postTaxRequest.TotalTax = getTaxResult.TotalTax;
			PostTaxResult postTaxResult = _taxSvc.PostTax(postTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, postTaxResult.ResultCode);

			CancelTaxRequest cancelTaxRequest = new CancelTaxRequest();
            cancelTaxRequest.CompanyCode = getTaxRequest.CompanyCode;
            cancelTaxRequest.DocType = getTaxRequest.DocType;
            cancelTaxRequest.DocCode = getTaxRequest.DocCode;

			cancelTaxRequest.CancelCode = CancelCode.PostFailed;
			CancelTaxResult cancelTaxResult = _taxSvc.CancelTax(cancelTaxRequest);

            if (cancelTaxResult.Messages.Count > 0)
            {
                Console.WriteLine(FormatMessages(cancelTaxResult));
            }
			Assert.AreEqual(SeverityLevel.Success, cancelTaxResult.ResultCode, "Expected successful CancelTax");

			GetTaxHistoryRequest historyRequest = new GetTaxHistoryRequest();
            historyRequest.CompanyCode = getTaxRequest.CompanyCode;
            historyRequest.DocType = getTaxRequest.DocType;
            historyRequest.DocCode = getTaxRequest.DocCode;

			historyRequest.DetailLevel = DetailLevel.Document;
			GetTaxHistoryResult historyResult = _taxSvc.GetTaxHistory(historyRequest);
			Assert.AreEqual(SeverityLevel.Success, historyResult.ResultCode);
			Assert.AreEqual(DocStatus.Saved, historyResult.GetTaxResult.DocStatus, "Expected document to be back in saved state");
		}
	
		[Test]
        public void CancelTaxVerifyCancelledAfterCommitTest()
        {
            GetTaxRequest getTaxRequest = CreateDefaultGetTaxRequest();
            getTaxRequest.DocCode = "Adapter_PostTest_" + DateTime.Now.Ticks;
            getTaxRequest.DocType = DocumentType.SalesInvoice;
            getTaxRequest.CustomerCode = "PostTest02";
            getTaxRequest.OriginAddress = CreateDefaultShipFromAddress();
            getTaxRequest.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			getTaxRequest.Lines.Add(line);

			GetTaxResult getTaxResult = _taxSvc.GetTax(getTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, getTaxResult.ResultCode, "Expected successful GetTax");

			PostTaxRequest postTaxRequest = new PostTaxRequest();
            postTaxRequest.CompanyCode = getTaxRequest.CompanyCode;
            postTaxRequest.DocType = getTaxRequest.DocType;
            postTaxRequest.DocCode = getTaxRequest.DocCode;

			postTaxRequest.DocDate = getTaxResult.DocDate;
			postTaxRequest.TotalAmount = getTaxResult.TotalAmount;
			postTaxRequest.TotalTax = getTaxResult.TotalTax;
			PostTaxResult postTaxResult = _taxSvc.PostTax(postTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, postTaxResult.ResultCode);

			CommitTaxRequest commitTaxRequest = new CommitTaxRequest();
            commitTaxRequest.CompanyCode = getTaxRequest.CompanyCode;
            commitTaxRequest.DocType = getTaxRequest.DocType;
            commitTaxRequest.DocCode = getTaxRequest.DocCode;

			CommitTaxResult commitTaxResult = _taxSvc.CommitTax(commitTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, commitTaxResult.ResultCode);

			CancelTaxRequest cancelTaxRequest = new CancelTaxRequest();
            cancelTaxRequest.CompanyCode = getTaxRequest.CompanyCode;
            cancelTaxRequest.DocType = getTaxRequest.DocType;
            cancelTaxRequest.DocCode = getTaxRequest.DocCode;

			cancelTaxRequest.CancelCode = CancelCode.DocDeleted;
			CancelTaxResult cancelTaxResult = _taxSvc.CancelTax(cancelTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, cancelTaxResult.ResultCode, "Expected successful CancelTax");

			GetTaxHistoryRequest historyRequest = new GetTaxHistoryRequest();
            historyRequest.CompanyCode = getTaxRequest.CompanyCode;
            historyRequest.DocType = getTaxRequest.DocType;
            historyRequest.DocCode = getTaxRequest.DocCode;

			historyRequest.DetailLevel = DetailLevel.Document;
			GetTaxHistoryResult historyResult = _taxSvc.GetTaxHistory(historyRequest);
			Assert.AreEqual(SeverityLevel.Success, historyResult.ResultCode);
			Assert.AreEqual(DocStatus.Cancelled, historyResult.GetTaxResult.DocStatus, "Expected document to be in cancelled state");
		}

		[Test]
        //[ExpectedException(typeof(ArgumentException))]
		public void ValidPostTaxRequestTest()
        {
            try
            {
                PostTaxRequest request = new PostTaxRequest();
                PostTaxResult postTaxResult = _taxSvc.PostTax(request);
                foreach (Message message in postTaxResult.Messages)
                {
                    Console.WriteLine(message.Name + ": " + message.Summary);
                }
                Assert.AreEqual(SeverityLevel.Error, postTaxResult.ResultCode);
                Assert.AreEqual("RequiredError", postTaxResult.Messages[0].Name);
            }
            catch (Exception ex)
            {
                Assert.Fail("ValidPostTaxRequestTest failed: " + ex.Message + " : " + ex.StackTrace);
            }
		}

        /// <summary>
        /// PostTaxRequest validation test for DocId( as GUID- 
        /// </summary>
        [Test]
        public void ValidPostTaxRequestTest1()
        {
            try
            {
                PostTaxRequest postTaxRequest = new PostTaxRequest();
                postTaxRequest.DocCode = "";
                PostTaxResult postTaxResult = _taxSvc.PostTax(postTaxRequest);

                foreach (Message message in postTaxResult.Messages)
                {
                    Console.WriteLine(message.Name + ": " + message.Summary);
                }
                Assert.AreEqual(SeverityLevel.Error, postTaxResult.ResultCode);
                Assert.AreEqual("RequiredError", postTaxResult.Messages[0].Name);
            }
            catch (Exception ex)
            {
                Assert.Fail("ValidPostTaxRequestTest1 failed: " + ex.Message + " : " + ex.StackTrace);
            }
        }

		[Test]
		public void PostTaxTest()
		{
			GetTaxRequest getTaxRequest = CreateDefaultGetTaxRequest();
			getTaxRequest.DocCode = "Adapter_PostTest_" + DateTime.Now.Ticks;
			getTaxRequest.DocType = DocumentType.SalesInvoice;
			getTaxRequest.CustomerCode = "PostTest01";
			getTaxRequest.OriginAddress = CreateDefaultShipFromAddress();
			getTaxRequest.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			getTaxRequest.Lines.Add(line);

			GetTaxResult getTaxResult = _taxSvc.GetTax(getTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, getTaxResult.ResultCode, "Expected GetTax to pass");

			PostTaxRequest postTaxRequest = CreatePostTaxRequest(getTaxRequest, getTaxResult);
			PostTaxResult postTaxResult = _taxSvc.PostTax(postTaxRequest);
		
			foreach (Message message in postTaxResult.Messages)
			{
				Console.WriteLine(message.Name + ": " + message.Summary);
			}
			Assert.AreEqual(SeverityLevel.Success, postTaxResult.ResultCode);
		}

        /// <summary>
        /// This test updates the DocDate during posting
        /// </summary>
        [Test]
        public void PostTaxDateTest()
        {
            GetTaxRequest getTaxRequest = CreateDefaultGetTaxRequest();
            getTaxRequest.DocCode = "PostTaxDateTest_" + DateTime.Now.Ticks;
            getTaxRequest.DocType = DocumentType.SalesInvoice;
            getTaxRequest.CustomerCode = "PostTest01";
            getTaxRequest.OriginAddress = CreateDefaultShipFromAddress();
            getTaxRequest.DestinationAddress = CreateDefaultShipToAddress();
            getTaxRequest.DocDate = DateTime.Today.AddDays(-2);

            Line line = CreateDefaultGetTaxRequestLine();
            getTaxRequest.Lines.Add(line);

            GetTaxResult getTaxResult = _taxSvc.GetTax(getTaxRequest);
            Assert.AreEqual(SeverityLevel.Success, getTaxResult.ResultCode, "Expected GetTax to pass");

            PostTaxRequest postTaxRequest = CreatePostTaxRequest(getTaxRequest, getTaxResult);
            postTaxRequest.DocDate = DateTime.Today;
            PostTaxResult postTaxResult = _taxSvc.PostTax(postTaxRequest);

            foreach (Message message in postTaxResult.Messages)
            {
                Console.WriteLine(message.Name + ": " + message.Summary);
            }
            Assert.AreEqual(SeverityLevel.Success, postTaxResult.ResultCode);

            GetTaxHistoryRequest getTaxHistoryRequest = new GetTaxHistoryRequest();
            getTaxHistoryRequest.CompanyCode = getTaxRequest.CompanyCode;
            getTaxHistoryRequest.DocType = getTaxRequest.DocType;
            getTaxHistoryRequest.DocCode = getTaxRequest.DocCode;

            getTaxHistoryRequest.DetailLevel = DetailLevel.Line;
            GetTaxHistoryResult getTaxHistoryResult = _taxSvc.GetTaxHistory(getTaxHistoryRequest);
            Assert.AreEqual(SeverityLevel.Success, getTaxHistoryResult.ResultCode);
            Assert.AreEqual(postTaxRequest.DocDate, getTaxHistoryResult.GetTaxRequest.DocDate);
            Assert.AreEqual(postTaxRequest.DocDate, getTaxHistoryResult.GetTaxResult.DocDate);
        }

        [Test]
		public void PostTaxVerifyPostedTest()
		{
			GetTaxRequest getTaxRequest = CreateDefaultGetTaxRequest();
			getTaxRequest.DocCode = "Adapter_PostTest_" + DateTime.Now.Ticks;
			getTaxRequest.DocType = DocumentType.SalesInvoice;
			getTaxRequest.CustomerCode = "PostTest02";
			getTaxRequest.OriginAddress = CreateDefaultShipFromAddress();
			getTaxRequest.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			getTaxRequest.Lines.Add(line);

			GetTaxResult getTaxResult = _taxSvc.GetTax(getTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, getTaxResult.ResultCode, "Expected successful GetTax");

			PostTaxRequest postTaxRequest = new PostTaxRequest();
            postTaxRequest.CompanyCode = getTaxRequest.CompanyCode;
            postTaxRequest.DocType = getTaxRequest.DocType;
            postTaxRequest.DocCode = getTaxRequest.DocCode;

			postTaxRequest.DocDate = getTaxResult.DocDate;
			postTaxRequest.TotalAmount = getTaxResult.TotalAmount;
			postTaxRequest.TotalTax = getTaxResult.TotalTax;
			PostTaxResult postTaxResult = _taxSvc.PostTax(postTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, postTaxResult.ResultCode);

			GetTaxHistoryRequest historyRequest = new GetTaxHistoryRequest();
            historyRequest.CompanyCode = getTaxRequest.CompanyCode;
            historyRequest.DocType = getTaxRequest.DocType;
            historyRequest.DocCode = getTaxRequest.DocCode;

			historyRequest.DetailLevel = DetailLevel.Document;
			GetTaxHistoryResult historyResult = _taxSvc.GetTaxHistory(historyRequest);
			Assert.AreEqual(SeverityLevel.Success, historyResult.ResultCode);
			Assert.AreEqual(DocStatus.Posted, historyResult.GetTaxResult.DocStatus, "Expected document to be in posted state");
		}

		[Test]
		public void MissingPostTaxTest()
		{
			PostTaxRequest postTaxRequest = new PostTaxRequest();
			postTaxRequest.CompanyCode = "DEFAULT";
			postTaxRequest.DocType = DocumentType.SalesInvoice;
			postTaxRequest.DocCode = "NOSUCHDOCCODEEXISTS";
			postTaxRequest.DocDate = new DateTime(1900, 1, 1);

			PostTaxResult postTaxResult = _taxSvc.PostTax(postTaxRequest);
			Assert.AreEqual(SeverityLevel.Error, postTaxResult.ResultCode, "Expected Error because should not have found any existing document.");
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException))]
		public void ValidCommitTaxRequestTest()
		{
            try
            {
                CommitTaxRequest request = new CommitTaxRequest();
                CommitTaxResult commitTaxResult = _taxSvc.CommitTax(request);

                foreach (Message message in commitTaxResult.Messages)
                {
                    Console.WriteLine(message.Name + ": " + message.Summary);
                }
                Assert.AreEqual(SeverityLevel.Error, commitTaxResult.ResultCode);
                Assert.AreEqual("RequiredError", commitTaxResult.Messages[0].Name);
            }
            catch (Exception ex)
            {
                Assert.Fail("ValidCommitTaxRequestTest failed: " + ex.Message + " : " + ex.StackTrace);
            }
		}

		[Test]
		public void CommitTaxTest()
		{
			GetTaxRequest getTaxRequest = CreateDefaultGetTaxRequest();
			getTaxRequest.DocCode = "Adapter_PostTest_" + DateTime.Now.Ticks;
			getTaxRequest.DocType = DocumentType.SalesInvoice;
			getTaxRequest.CustomerCode = "PostTest01";
			getTaxRequest.OriginAddress = CreateDefaultShipFromAddress();
			getTaxRequest.DestinationAddress = CreateDefaultShipToAddress();
		
			Line line = CreateDefaultGetTaxRequestLine();
			getTaxRequest.Lines.Add(line);

			GetTaxResult getTaxResult = _taxSvc.GetTax(getTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, getTaxResult.ResultCode, "Expected GetTax to pass");

			PostTaxResult postTaxResult = _taxSvc.PostTax(CreatePostTaxRequest(getTaxRequest, getTaxResult));
			Assert.AreEqual(SeverityLevel.Success, postTaxResult.ResultCode, "Expected PostTax to pass");

			CommitTaxRequest commitTaxRequest = new CommitTaxRequest();
			commitTaxRequest.CompanyCode = getTaxRequest.CompanyCode;
			commitTaxRequest.DocType = getTaxRequest.DocType;
			commitTaxRequest.DocCode = getTaxRequest.DocCode;
			
			CommitTaxResult commitTaxResult = _taxSvc.CommitTax(commitTaxRequest);

			if (commitTaxResult.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(commitTaxResult));
			}
			Assert.AreEqual(SeverityLevel.Success, commitTaxResult.ResultCode);
		}

		[Test]
		public void CommitTaxOnGetTaxTest()
		{
			GetTaxRequest getTaxRequest = CreateDefaultGetTaxRequest();
			getTaxRequest.DocCode = "CommitTaxOnGetTaxTest" + DateTime.Now.Ticks;
			getTaxRequest.DocType = DocumentType.SalesInvoice;
			getTaxRequest.CustomerCode = "CommitTaxOnGetTaxTest";
			getTaxRequest.OriginAddress = CreateDefaultShipFromAddress();
			getTaxRequest.DestinationAddress = CreateDefaultShipToAddress();
			getTaxRequest.Commit = true;	
			Line line = CreateDefaultGetTaxRequestLine();
			getTaxRequest.Lines.Add(line);

			GetTaxResult getTaxResult = _taxSvc.GetTax(getTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, getTaxResult.ResultCode, "Expected GetTax to pass");
			
			//Check if document is in Commited state
			GetTaxHistoryRequest historyRequest = new GetTaxHistoryRequest();
            historyRequest.CompanyCode = getTaxRequest.CompanyCode;
            historyRequest.DocType = getTaxRequest.DocType;
            historyRequest.DocCode = getTaxRequest.DocCode;

			historyRequest.DetailLevel = DetailLevel.Document;
			GetTaxHistoryResult historyResult = _taxSvc.GetTaxHistory(historyRequest);
			Assert.AreEqual(SeverityLevel.Success, historyResult.ResultCode);
			Assert.AreEqual(DocStatus.Committed, historyResult.GetTaxResult.DocStatus, "Expected document to be in committed state");

		}

		[Test]
		public void CommitTaxOnPostTaxTest()
		{
			GetTaxRequest getTaxRequest = CreateDefaultGetTaxRequest();
			getTaxRequest.DocCode = "Adapter_PostTest_" + DateTime.Now.Ticks;
			getTaxRequest.DocType = DocumentType.SalesInvoice;
			getTaxRequest.CustomerCode = "PostTest02";
			getTaxRequest.OriginAddress = CreateDefaultShipFromAddress();
			getTaxRequest.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			getTaxRequest.Lines.Add(line);

			GetTaxResult getTaxResult = _taxSvc.GetTax(getTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, getTaxResult.ResultCode, "Expected successful GetTax");

			PostTaxRequest postTaxRequest = new PostTaxRequest();
            postTaxRequest.CompanyCode = getTaxRequest.CompanyCode;
            postTaxRequest.DocType = getTaxRequest.DocType;
            postTaxRequest.DocCode = getTaxRequest.DocCode;

			postTaxRequest.DocDate = getTaxResult.DocDate;
			postTaxRequest.TotalAmount = getTaxResult.TotalAmount;
			postTaxRequest.TotalTax = getTaxResult.TotalTax;
			postTaxRequest.Commit = true;
			PostTaxResult postTaxResult = _taxSvc.PostTax(postTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, postTaxResult.ResultCode);

			GetTaxHistoryRequest historyRequest = new GetTaxHistoryRequest();
            historyRequest.CompanyCode = getTaxRequest.CompanyCode;
            historyRequest.DocType = getTaxRequest.DocType;
            historyRequest.DocCode = getTaxRequest.DocCode;

			historyRequest.DetailLevel = DetailLevel.Document;
			GetTaxHistoryResult historyResult = _taxSvc.GetTaxHistory(historyRequest);
			Assert.AreEqual(SeverityLevel.Success, historyResult.ResultCode);
			Assert.AreEqual(DocStatus.Committed, historyResult.GetTaxResult.DocStatus, "Expected document to be in posted state");
		}

		[Test]
		public void PostTaxCommitTaxNewDocCodeTest()
		{
			GetTaxRequest getTaxRequest = CreateDefaultGetTaxRequest();
			getTaxRequest.DocCode = "Adapter_PostTest_" + DateTime.Now.Ticks;
			getTaxRequest.DocType = DocumentType.SalesInvoice;
			getTaxRequest.CustomerCode = "PostTest02";
			getTaxRequest.OriginAddress = CreateDefaultShipFromAddress();
			getTaxRequest.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			getTaxRequest.Lines.Add(line);

			GetTaxResult getTaxResult = _taxSvc.GetTax(getTaxRequest);
            if (getTaxResult.Messages.Count > 0)
            {
                Console.WriteLine(FormatMessages(getTaxResult));
            }
			Assert.AreEqual(SeverityLevel.Success, getTaxResult.ResultCode, "Expected successful GetTax");

			PostTaxRequest postTaxRequest = new PostTaxRequest();
			//postTaxRequest.DocId = getTaxResult.DocId;
            postTaxRequest.CompanyCode = getTaxRequest.CompanyCode;
            postTaxRequest.DocType = getTaxRequest.DocType;
            postTaxRequest.DocCode = getTaxRequest.DocCode;
            postTaxRequest.NewDocCode = "NewDocCode_PostTest_" + DateTime.Now.Ticks;

			postTaxRequest.DocDate = getTaxResult.DocDate;
			postTaxRequest.TotalAmount = getTaxResult.TotalAmount;
			postTaxRequest.TotalTax = getTaxResult.TotalTax;
			PostTaxResult postTaxResult = _taxSvc.PostTax(postTaxRequest);

            if (postTaxResult.Messages.Count > 0)
            {
                Console.WriteLine(FormatMessages(postTaxResult));
            }
            Assert.AreEqual(SeverityLevel.Success, postTaxResult.ResultCode, "Expected successful PostTax");

            CommitTaxRequest commitTaxRequest = new CommitTaxRequest();
            commitTaxRequest.CompanyCode = postTaxRequest.CompanyCode;
            commitTaxRequest.DocType = postTaxRequest.DocType;
            commitTaxRequest.DocCode = postTaxRequest.NewDocCode;
            commitTaxRequest.NewDocCode = "NewDocCode_CommitTest_" + DateTime.Now.Ticks;

            CommitTaxResult commitTaxResult = _taxSvc.CommitTax(commitTaxRequest);
            if (commitTaxResult.Messages.Count > 0)
            {
                Console.WriteLine(FormatMessages(commitTaxResult));
            }
            Assert.AreEqual(SeverityLevel.Success, commitTaxResult.ResultCode, "Expected successful CommitTax");
		}

		[Test]
		public void CommitTaxOnGetTaxCallTest()
		{
			GetTaxRequest getTaxRequest = CreateDefaultGetTaxRequest();
			getTaxRequest.DocCode = "CommitTaxOnGetTaxTest" + DateTime.Now.Ticks;
			getTaxRequest.DocType = DocumentType.SalesInvoice;
			getTaxRequest.CustomerCode = "CommitTaxOnGetTaxTest";
			getTaxRequest.OriginAddress = CreateDefaultShipFromAddress();
			getTaxRequest.DestinationAddress = CreateDefaultShipToAddress();
			getTaxRequest.Commit = true;	
			Line line = CreateDefaultGetTaxRequestLine();
			getTaxRequest.Lines.Add(line);

			GetTaxResult getTaxResult = _taxSvc.GetTax(getTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, getTaxResult.ResultCode, "Expected GetTax to pass");
			
            //Check if document is in Commited state
            GetTaxHistoryRequest historyRequest = new GetTaxHistoryRequest();
            historyRequest.CompanyCode = getTaxRequest.CompanyCode;
            historyRequest.DocType = getTaxRequest.DocType;
            historyRequest.DocCode = getTaxRequest.DocCode;

            historyRequest.DetailLevel = DetailLevel.Document;
            GetTaxHistoryResult historyResult = _taxSvc.GetTaxHistory(historyRequest);
            Assert.AreEqual(SeverityLevel.Success, historyResult.ResultCode);
            Assert.AreEqual(DocStatus.Committed, historyResult.GetTaxResult.DocStatus, "Expected document to be in committed state");
		}
	
		[Test]
		public void CommitTaxVerifyCommittedTest()
		{
			GetTaxRequest getTaxRequest = CreateDefaultGetTaxRequest();
			getTaxRequest.DocCode = "Adapter_PostTest_" + DateTime.Now.Ticks;
			getTaxRequest.DocType = DocumentType.SalesInvoice;
			getTaxRequest.CustomerCode = "PostTest02";
			getTaxRequest.OriginAddress = CreateDefaultShipFromAddress();
			getTaxRequest.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			getTaxRequest.Lines.Add(line);

			GetTaxResult getTaxResult = _taxSvc.GetTax(getTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, getTaxResult.ResultCode, "Expected successful GetTax");

			PostTaxRequest postTaxRequest = new PostTaxRequest();
            postTaxRequest.CompanyCode = getTaxRequest.CompanyCode;
            postTaxRequest.DocType = getTaxRequest.DocType;
            postTaxRequest.DocCode = getTaxRequest.DocCode;

			postTaxRequest.DocDate = getTaxResult.DocDate;
			postTaxRequest.TotalAmount = getTaxResult.TotalAmount;
			postTaxRequest.TotalTax = getTaxResult.TotalTax;
			PostTaxResult postTaxResult = _taxSvc.PostTax(postTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, postTaxResult.ResultCode);

			CommitTaxRequest commitTaxRequest = new CommitTaxRequest();
            commitTaxRequest.CompanyCode = getTaxRequest.CompanyCode;
            commitTaxRequest.DocType = getTaxRequest.DocType;
            commitTaxRequest.DocCode = getTaxRequest.DocCode;

			CommitTaxResult commitTaxResult = _taxSvc.CommitTax(commitTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, commitTaxResult.ResultCode);

			GetTaxHistoryRequest historyRequest = new GetTaxHistoryRequest();
            historyRequest.CompanyCode = getTaxRequest.CompanyCode;
            historyRequest.DocType = getTaxRequest.DocType;
            historyRequest.DocCode = getTaxRequest.DocCode;

			historyRequest.DetailLevel = DetailLevel.Document;
			GetTaxHistoryResult historyResult = _taxSvc.GetTaxHistory(historyRequest);
			Assert.AreEqual(SeverityLevel.Success, historyResult.ResultCode);
			Assert.AreEqual(DocStatus.Committed, historyResult.GetTaxResult.DocStatus, "Expected document to be in committed state");
		}

		[Test]
		public void MissingCommitTaxTest()
		{
			CommitTaxRequest commitTaxRequest = new CommitTaxRequest();
			commitTaxRequest.CompanyCode = "DEFAULT";
			commitTaxRequest.DocType = DocumentType.SalesInvoice;
			commitTaxRequest.DocCode = "NOSUCHDOCCODEEXISTS";

			CommitTaxResult commitTaxResult = _taxSvc.CommitTax(commitTaxRequest);
			Assert.AreEqual(SeverityLevel.Error, commitTaxResult.ResultCode, "Expected Error because should not have found any existing document.");
		}

		[Test]
		//[ExpectedException(typeof(ArgumentException))]
		public void ValidGetTaxHistoryRequestTest()
		{
            try
            {
                GetTaxHistoryRequest request = new GetTaxHistoryRequest();
                GetTaxHistoryResult getTaxHistoryResult = _taxSvc.GetTaxHistory(request);

                foreach (Message message in getTaxHistoryResult.Messages)
                {
                    Console.WriteLine(message.Name + ": " + message.Summary);
                }
                Assert.AreEqual(SeverityLevel.Error, getTaxHistoryResult.ResultCode);
                Assert.AreEqual("RequiredError", getTaxHistoryResult.Messages[0].Name);
            }
            catch (Exception ex)
            {
                Assert.Fail("ValidGetTaxHistoryRequestTest failed: " + ex.Message + " : " + ex.StackTrace);
            }
		}

		[Test]
		public void GetTaxHistoryTest()
		{
			GetTaxRequest taxRequest = CreateDefaultGetTaxRequest();
			taxRequest.OriginAddress = CreateDefaultShipFromAddress();
			taxRequest.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			taxRequest.Lines.Add(line);

			GetTaxResult taxResult = _taxSvc.GetTax(taxRequest);

			Assert.AreEqual(SeverityLevel.Success, taxResult.ResultCode);

			GetTaxHistoryRequest historyRequest = new GetTaxHistoryRequest();
			historyRequest.CompanyCode = taxRequest.CompanyCode;
			historyRequest.DocType = taxRequest.DocType;
			historyRequest.DocCode = taxRequest.DocCode;
			historyRequest.DetailLevel = DetailLevel.Tax;
			
			GetTaxHistoryResult historyResult = _taxSvc.GetTaxHistory(historyRequest);

			if (historyResult.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(historyResult));
			}
			Assert.AreEqual(SeverityLevel.Success, historyResult.ResultCode);
			Assert.IsNotNull(historyResult.GetTaxRequest);
			Assert.IsNotNull(historyResult.GetTaxResult);
			Assert.AreEqual(historyRequest.CompanyCode, historyResult.GetTaxRequest.CompanyCode);
			Assert.AreEqual(historyRequest.DocCode.ToUpper(), historyResult.GetTaxRequest.DocCode.ToUpper());
		}

		[Test]
		public void GetTaxHistoryTest2()
		{
			//same address
			GetTaxRequest taxRequest = CreateDefaultGetTaxRequest();
			taxRequest.OriginAddress = CreateDefaultShipFromAddress();
			taxRequest.DestinationAddress = CreateDefaultShipFromAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			taxRequest.Lines.Add(line);

			GetTaxResult taxResult = _taxSvc.GetTax(taxRequest);

			Assert.AreEqual(SeverityLevel.Success, taxResult.ResultCode);

			GetTaxHistoryRequest historyRequest = new GetTaxHistoryRequest();
			historyRequest.CompanyCode = taxRequest.CompanyCode;
			historyRequest.DocType = taxRequest.DocType;
			historyRequest.DocCode = taxRequest.DocCode;
			historyRequest.DetailLevel = DetailLevel.Tax;
			
			GetTaxHistoryResult historyResult = _taxSvc.GetTaxHistory(historyRequest);

			if (historyResult.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(historyResult));
			}
			Assert.AreEqual(SeverityLevel.Success, historyResult.ResultCode);
			Assert.IsNotNull(historyResult.GetTaxRequest);
			Assert.IsNotNull(historyResult.GetTaxResult);
			Assert.AreEqual(historyRequest.CompanyCode, historyResult.GetTaxRequest.CompanyCode);
			Assert.AreEqual(historyRequest.DocCode.ToUpper(), historyResult.GetTaxRequest.DocCode.ToUpper());
		}

		[Test]
		public void GetTaxHistoryTest3()
		{
			//same address, posted document
			GetTaxRequest taxRequest = CreateDefaultGetTaxRequest();
			taxRequest.OriginAddress = CreateDefaultShipFromAddress();
			taxRequest.DestinationAddress = CreateDefaultShipFromAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			taxRequest.Lines.Add(line);

			GetTaxResult taxResult = _taxSvc.GetTax(taxRequest);
			Assert.IsTrue(taxResult.ResultCode == SeverityLevel.Success, "Expected successful GetTax.");

			PostTaxRequest postTaxRequest = new PostTaxRequest();
            postTaxRequest.CompanyCode = taxRequest.CompanyCode;
            postTaxRequest.DocType = taxRequest.DocType;
            postTaxRequest.DocCode = taxRequest.DocCode;

			postTaxRequest.DocDate = taxResult.DocDate;
			postTaxRequest.TotalAmount = taxResult.TotalAmount;
			postTaxRequest.TotalTax = taxResult.TotalTax;
			PostTaxResult postTaxResult = _taxSvc.PostTax(postTaxRequest);
			if (postTaxResult.ResultCode != SeverityLevel.Success)
			{
				if (postTaxResult.Messages.Count > 0)
				{
					Console.WriteLine(FormatMessages(postTaxResult));
				}
			}
			Assert.IsTrue(postTaxResult.ResultCode == SeverityLevel.Success, "Expected successful PostTax.");

			GetTaxHistoryRequest historyRequest = new GetTaxHistoryRequest();
			historyRequest.CompanyCode = taxRequest.CompanyCode;
			historyRequest.DocType = taxRequest.DocType;
			historyRequest.DocCode = taxRequest.DocCode;
			historyRequest.DetailLevel = DetailLevel.Tax;
			GetTaxHistoryResult historyResult = _taxSvc.GetTaxHistory(historyRequest);

			if (historyResult.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(historyResult));
			}
			Assert.AreEqual(SeverityLevel.Success, historyResult.ResultCode);
			Assert.IsNotNull(historyResult.GetTaxRequest);
			Assert.IsNotNull(historyResult.GetTaxResult);
			//TODO: more testing/comparison -- can't use CompareHistory() because we Posted
		}

		[Test]
		//[Ignore("TODO: Track down why it is failing")]
		public void GetTaxHistoryTest4()
		{
			//same address, posted document
			GetTaxRequest taxRequest = CreateDefaultGetTaxRequest();
			taxRequest.OriginAddress = CreateDefaultShipFromAddress();
			taxRequest.DestinationAddress = CreateDefaultShipFromAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			taxRequest.Lines.Add(line);

			line = CreateDefaultGetTaxRequestLine(2);
			taxRequest.Lines.Add(line);

			line = CreateDefaultGetTaxRequestLine(3);
			line.OriginAddress = CreateDefaultShipFromAddress();
			line.DestinationAddress = CreateDefaultShipToAddress();
			taxRequest.Lines.Add(line);

			GetTaxResult taxResult = _taxSvc.GetTax(taxRequest);
			Assert.IsTrue(taxResult.ResultCode == SeverityLevel.Success, "Expected successful GetTax.");

			Assert.AreEqual(2, CountAddresses(taxRequest), "Expected addresses to be consolidated to 2 unique addresses");

			GetTaxHistoryRequest historyRequest = new GetTaxHistoryRequest();
			historyRequest.CompanyCode = taxRequest.CompanyCode;
			historyRequest.DocType = taxRequest.DocType;
			historyRequest.DocCode = taxRequest.DocCode;
			historyRequest.DetailLevel = DetailLevel.Tax;
			GetTaxHistoryResult historyResult = _taxSvc.GetTaxHistory(historyRequest);

			if (historyResult.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(historyResult));
			}
			Assert.AreEqual(SeverityLevel.Success, historyResult.ResultCode);
			Assert.IsNotNull(historyResult.GetTaxRequest);
			Assert.IsNotNull(historyResult.GetTaxResult);
			CompareHistory(taxRequest, taxResult, historyResult);
		
			//test addresses specific to this request object
			//not true: Assert.IsNotNull(historyResult.GetTaxRequest.OriginAddress, "Expected Origin Address (DefaultShipFrom)");
			//not true: Assert.IsNotNull(historyResult.GetTaxRequest.DestinationAddress, "Expected Destination Address (DefaultShipFrom)");
			//not true: Assert.IsTrue(historyResult.GetTaxRequest.OriginAddress.GetHashCode() == historyResult.GetTaxRequest.DestinationAddress.GetHashCode(), "Expected same address object");
			//expect server to consolidate equivalent addresses //Assert.IsTrue(historyRequest.OriginAddress.Equals(historyRequest.DestinationAddress), "Expected identical addresses");
			//not true: Assert.IsTrue(AreEquivalentAddresses(taxRequest.OriginAddress, historyResult.GetTaxRequest.OriginAddress), "Expected saved origin address to equal input origin address");
			//not true: Assert.IsTrue(AreEquivalentAddresses(taxRequest.DestinationAddress, historyResult.GetTaxRequest.DestinationAddress), "Expected saved destination address to equal input destination address"); //if objects are same, this should automatically be true

			int index = 0;
			foreach (Line requestLine in taxRequest.Lines)
			{
				Line historyLine = historyResult.GetTaxRequest.Lines.GetItemByNo(requestLine.No);
		
				if (index == 0 || index == 1)
				{
					Assert.IsTrue(requestLine.OriginAddress.GetHashCode() == requestLine.DestinationAddress.GetHashCode(), "Expected same address object on request line");
					Assert.IsTrue(historyLine.OriginAddress.GetHashCode() == historyLine.DestinationAddress.GetHashCode(), "Expected same address object on historical request line");
				} 
				else if (index == 2)
				{
					Assert.IsTrue(requestLine.OriginAddress.GetHashCode() != requestLine.DestinationAddress.GetHashCode(), "Expected same address object on request line");
					Assert.IsTrue(historyLine.OriginAddress.GetHashCode() != historyLine.DestinationAddress.GetHashCode(), "Expected same address object on historical request line");
				} 
				else
				{
					Assert.Fail("More lines than accounted for in the test");
				}
				Assert.IsTrue(AreEquivalentAddresses(requestLine.OriginAddress, historyLine.OriginAddress), "Expected equivalent origin addresses (line vs. historical line)");
				Assert.IsTrue(AreEquivalentAddresses(requestLine.DestinationAddress, historyLine.DestinationAddress), "Expected equivalent destination addresses (line vs. historical line)");

				index++;
			}
		}

		[Test]
		public void GetTaxHistoryBugTest01()
		{
			//same address, posted document
			GetTaxRequest taxRequest = new GetTaxRequest();
			taxRequest.CompanyCode = "DEFAULT";
			taxRequest.DetailLevel = DetailLevel.Tax;
			taxRequest.Discount = 0m;
			taxRequest.DocCode = "DOC000105";
			taxRequest.DocDate = DateTime.Today;
			taxRequest.DocType = DocumentType.SalesInvoice;
			taxRequest.CustomerCode = "123";

			Address address = new Address();
			address.Line1 = "900 Winslow Way E";
			address.Line2 = "Ste 130";
			address.City = "Bainbridge Island";
			address.Region = "WA";
			address.PostalCode = "98110";
			taxRequest.OriginAddress = address;

			address = new Address();
			address.Line1 = "900 Winslow Way E";
			address.Line2 = "Ste 130";
			address.City = "Bainbridge Island";
			address.Region = "WA";
			address.PostalCode = "98110";
			taxRequest.DestinationAddress = address;

			Line line = new Line();
			line.No = "1";
			line.ItemCode = "20000"; // Clothing
			line.Qty = 1;
			line.Amount = 10m;
			line.Discounted = false;
			line.TaxCode = "";

			taxRequest.Lines.Add(line);

			line = new Line();
			line.No = "2";
			line.ItemCode = "FR"; // Freight
			line.Qty = 2;
			line.Amount = 40m;
			line.Discounted = false;
			line.TaxCode = "FR020000";

			taxRequest.Lines.Add(line);

			GetTaxResult taxResult = _taxSvc.GetTax(taxRequest);
			Assert.IsTrue(taxResult.ResultCode == SeverityLevel.Success, "Expected successful GetTax.");

			GetTaxHistoryRequest historyRequest = new GetTaxHistoryRequest();
			historyRequest.CompanyCode = taxRequest.CompanyCode;
			historyRequest.DocType = taxRequest.DocType;
			historyRequest.DocCode = taxRequest.DocCode;
			historyRequest.DetailLevel = DetailLevel.Tax;
			GetTaxHistoryResult historyResult = _taxSvc.GetTaxHistory(historyRequest);

			if (historyResult.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(historyResult));
			}
			Assert.AreEqual(SeverityLevel.Success, historyResult.ResultCode);
			Assert.IsNotNull(historyResult.GetTaxRequest);
			Assert.IsNotNull(historyResult.GetTaxResult);
			Assert.AreEqual(historyRequest.CompanyCode, historyResult.GetTaxRequest.CompanyCode);
			Assert.AreEqual(historyRequest.DocCode.ToUpper(), historyResult.GetTaxRequest.DocCode.ToUpper());
		}

        //[Test]
        //[ExpectedException(typeof(ArgumentException))]
        //public void ValidReconcileTaxHistoryRequestTest()
        //{
        //    ReconcileTaxHistoryRequest request = new ReconcileTaxHistoryRequest();
        //    _taxSvc.ReconcileTaxHistory(request);
        //    Assert.Fail("Expected ArgumentException");
        //}

		//Review : Ammit
		//Adding a test to check normal GetReconcileTaxHistory with the new Date Filter
		[Test]
		public void ReconcileTaxHistoryReloadedTest()
		{
			//Pusing a dummy doc with Future date
			GetTaxRequest taxRequest = CreateDefaultGetTaxRequest();
			
			taxRequest.OriginAddress = CreateDefaultShipFromAddress();
			taxRequest.DestinationAddress = CreateDefaultShipToAddress();
			
			Line line = CreateDefaultGetTaxRequestLine();
			taxRequest.Lines.Add(line);

			DateTime fakeDate = new DateTime(2090,12,17);
			taxRequest.DocDate = fakeDate;

			GetTaxResult taxRes = _taxSvc.GetTax(taxRequest);

			if (taxRes.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(taxRes));
			}

			Assert.AreEqual(SeverityLevel.Success, taxRes.ResultCode);
			
			//calling ReconHostory
			ReconcileTaxHistoryRequest request = new ReconcileTaxHistoryRequest();
			request.CompanyCode = "DEFAULT";
		
			request.StartDate = new DateTime(2090,12,16);
			request.EndDate = new DateTime(2090,12,18);
			request.DocStatus = DocStatus.Saved;

			ReconcileTaxHistoryResult result = _taxSvc.ReconcileTaxHistory(request);
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode, FormatMessages(result));
			Console.WriteLine("Assert Call to check result has only one DOC");
			Assert.AreEqual(1, result.GetTaxResults.Count);
			foreach (GetTaxResult taxResult in result.GetTaxResults)
			{
				if (taxResult.DocStatus == DocStatus.Saved)
				{
					// Cancel it
					CancelTaxRequest cancelRequest = new CancelTaxRequest();
                    cancelRequest.CompanyCode = taxRequest.CompanyCode;
                    cancelRequest.DocType = taxRequest.DocType;
                    cancelRequest.DocCode = taxRequest.DocCode;

					cancelRequest.CancelCode = CancelCode.DocDeleted;
					CancelTaxResult cancelResult = _taxSvc.CancelTax(cancelRequest);
					Assert.AreEqual(SeverityLevel.Success, cancelResult.ResultCode, FormatMessages(result));
					Console.WriteLine("Document canceled Succefully!!!");
				}
			}
		}
        
		[Test]
		public void ReconcileTaxHistoryTest()
		{
			ReconcileTaxHistoryRequest request = new ReconcileTaxHistoryRequest();
			request.CompanyCode = "DEFAULT";
			ReconcileTaxHistoryResult result = _taxSvc.ReconcileTaxHistory(request);
			
			while (result.GetTaxResults.Count > 0)
			{
				Assert.AreEqual(SeverityLevel.Success, result.ResultCode);

				foreach (GetTaxResult taxResult in result.GetTaxResults)
				{
					//Assert.AreEqual(false, taxResult.Reconciled);
					if (taxResult.DocStatus == DocStatus.Saved)
					{
						// Cancel it
						CancelTaxRequest cancelRequest = new CancelTaxRequest();
                        cancelRequest.CompanyCode = request.CompanyCode;
                        cancelRequest.DocType = taxResult.DocType;
                        cancelRequest.DocCode = taxResult.DocCode;

						cancelRequest.CancelCode = CancelCode.DocDeleted;
						CancelTaxResult cancelResult = _taxSvc.CancelTax(cancelRequest);
						Assert.AreEqual(SeverityLevel.Success, cancelResult.ResultCode, FormatMessages(result));
					}
					else if (taxResult.DocStatus == DocStatus.Posted)
					{
						// Commit it
						CommitTaxRequest commitRequest = new CommitTaxRequest();
                        commitRequest.CompanyCode = request.CompanyCode;
                        commitRequest.DocType = taxResult.DocType;
                        commitRequest.DocCode = taxResult.DocCode;

						CommitTaxResult commitResult = _taxSvc.CommitTax(commitRequest);
						Assert.AreEqual(SeverityLevel.Success, commitResult.ResultCode, FormatMessages(result));
					}
					else if (taxResult.DocStatus == DocStatus.Cancelled)
					{
						// Do nothing
					}
					else
					{
						Assert.AreEqual(DocStatus.Committed, taxResult.DocStatus);
					}
				}

                request.LastDocCode = result.LastDocCode;
				result = _taxSvc.ReconcileTaxHistory(request);
			}
			
			result = _taxSvc.ReconcileTaxHistory(request);
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode);

            request.LastDocCode = "0";
			result = _taxSvc.ReconcileTaxHistory(request);

			Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
			// TODO: Assert.AreEqual(0, result.GetTaxResults.Count);
        }

        [Test]
        //[Ignore("Need to enable after service work completes")]
        public void Reconciliation60Test()
        {
            GetTaxRequest taxSaveRequest = CreateTaxRequest();
            taxSaveRequest.DocDate = DateTime.Today.AddDays(-2);
            taxSaveRequest.DocCode = "Reconcile 6.0 SavedTest";
            GetTaxResult taxSaveResult = _taxSvc.GetTax(taxSaveRequest);
            Assert.AreEqual(SeverityLevel.Success, taxSaveResult.ResultCode, FormatMessages(taxSaveResult));

            GetTaxRequest taxCommittedRequest = CreateTaxRequest();
            taxCommittedRequest.DocDate = DateTime.Today.AddDays(-2);
            taxCommittedRequest.DocCode = "Reconcile 6.0 CommittedTest " + DateTime.Now.Ticks;
            GetTaxResult taxCommittedResult = _taxSvc.GetTax(taxCommittedRequest);
            Assert.AreEqual(SeverityLevel.Success, taxCommittedResult.ResultCode, FormatMessages(taxCommittedResult));


            // Post it
            PostTaxRequest postRequest = new PostTaxRequest();
            postRequest.CompanyCode = taxCommittedRequest.CompanyCode;
            postRequest.DocCode = taxCommittedResult.DocCode;
            postRequest.DocDate = DateTime.Today;
            postRequest.TotalAmount = taxCommittedResult.TotalAmount;
            postRequest.TotalTax = taxCommittedResult.TotalTax;
            postRequest.DocType = taxCommittedResult.DocType;
            PostTaxResult postResult = _taxSvc.PostTax(postRequest);
            Assert.AreEqual(SeverityLevel.Success, postResult.ResultCode, FormatMessages(postResult));


            /// Commit it
            CommitTaxRequest commitRequest = new CommitTaxRequest();
            commitRequest.CompanyCode = taxCommittedRequest.CompanyCode;
            commitRequest.DocCode = taxCommittedResult.DocCode;
            commitRequest.DocType = taxCommittedResult.DocType;
            CommitTaxResult commitResult = _taxSvc.CommitTax(commitRequest);
            Assert.AreEqual(SeverityLevel.Success, commitResult.ResultCode, FormatMessages(commitResult));


            // Search for committed document which should have DocDate of today
            ReconcileTaxHistoryRequest reconcileTaxHistoryRequest = new ReconcileTaxHistoryRequest();
            reconcileTaxHistoryRequest.CompanyCode = "DEFAULT";
            reconcileTaxHistoryRequest.StartDate = DateTime.Today;
            reconcileTaxHistoryRequest.EndDate = DateTime.Today;
            reconcileTaxHistoryRequest.DocStatus = DocStatus.Committed;
            reconcileTaxHistoryRequest.DocType = DocumentType.SalesInvoice;
            reconcileTaxHistoryRequest.LastDocCode = "0";
            reconcileTaxHistoryRequest.PageSize = 1000;
            ReconcileTaxHistoryResult reconcileTaxHistoryResult = _taxSvc.ReconcileTaxHistory(reconcileTaxHistoryRequest);

            Assert.AreEqual(SeverityLevel.Success, reconcileTaxHistoryResult.ResultCode, FormatMessages(reconcileTaxHistoryResult));
            Assert.IsTrue(reconcileTaxHistoryResult.GetTaxResults.Count > 0, "Expected > 0 reconcile records");

            bool found = false;

            do
            {
                foreach (GetTaxResult taxResult in reconcileTaxHistoryResult.GetTaxResults)
                {
                    Assert.AreEqual(DocStatus.Committed, taxResult.DocStatus);
                    Assert.AreEqual(DateTime.Today.ToShortDateString(), taxResult.DocDate.ToShortDateString());

                    if (taxResult.DocCode.Equals(taxCommittedRequest.DocCode))
                    {
                        found = true;
                    }
                }
                reconcileTaxHistoryRequest.LastDocCode = reconcileTaxHistoryResult.LastDocCode;
                reconcileTaxHistoryResult = _taxSvc.ReconcileTaxHistory(reconcileTaxHistoryRequest);
            } while (reconcileTaxHistoryResult.GetTaxResults.Count > 0);

            Assert.IsTrue(found, "ReconcileCommittedTest doc not found");
        }

	    [Test]
		public void RequestTimeoutPropertyTest()
		{
			_taxSvc.Configuration.RequestTimeout = 50;
			Assert.AreEqual(50, _taxSvc.Configuration.RequestTimeout);
		}

		//[Test]
		//cancel this test for the present; the code now resets the timeout
		//back to its configured setting after adding time for the extra lines
		public void RequestTimeoutIncrementedTest()
		{
			const int INCREMENT = 500;
			int timeout = _taxSvc.Configuration.RequestTimeout;

			GetTaxRequest request = CreateDefaultGetTaxRequest();	
			request.OriginAddress = CreateDefaultShipFromAddress();
			request.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			request.Lines.Add(line);

			_taxSvc.GetTax(request);
			Assert.AreEqual(timeout + INCREMENT, _taxSvc.Configuration.RequestTimeout);

			line = CreateDefaultGetTaxRequestLine();
			request.Lines.Add(line);
			
			_taxSvc.GetTax(request);
			Assert.AreEqual(timeout + (INCREMENT * 2), _taxSvc.Configuration.RequestTimeout);
		}


		//[Test]
		//[ExpectedException(typeof(WebException))]
		public void RequestTimesOut()
		{
			//RequestTimeout is now in seconds, not milliseconds, so more difficult to force a time out
			_taxSvc.Configuration.RequestTimeout = 1;
			GetTaxRequest request = CreateDefaultGetTaxRequest();	
			request.OriginAddress = CreateDefaultShipFromAddress();
			request.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			request.Lines.Add(line);

			_taxSvc.GetTax(request);
		}

		[Test]
		public void GetTaxAddressTest()
		{
			//test that addresses are not same object
			GetTaxRequest request = CreateDefaultGetTaxRequest();	
			request.OriginAddress = CreateDefaultShipFromAddress();
			request.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			request.Lines.Add(line);

			GetTaxResult result = _taxSvc.GetTax(request);
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode, "Expected a successful result.");
			Assert.IsFalse(request.OriginAddress == request.DestinationAddress, "Should not be the same object.");

		}

		[Test]
		public void GetTaxAddressTest2()
		{
			//test that addresses are the same object
			GetTaxRequest request = CreateDefaultGetTaxRequest();	
			request.OriginAddress = CreateDefaultShipFromAddress();
			request.DestinationAddress = request.OriginAddress;

			Line line = CreateDefaultGetTaxRequestLine();
			request.Lines.Add(line);

			GetTaxResult result = _taxSvc.GetTax(request);
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode, "Expected a successful result.");
			Assert.IsTrue(request.OriginAddress == request.DestinationAddress, "Should be the same object.");

			//test that a line without an address defaults to the request's address
			Assert.IsTrue(request.OriginAddress == request.Lines[0].OriginAddress, "Should be the same object.");
			Assert.IsTrue(request.DestinationAddress == request.Lines[0].DestinationAddress, "Should be the same object.");
		}

		[Test]
		public void GetTaxAddressTest3()
		{
			//test that a line with a specified address (new instance) is not overwritten by request's address
			GetTaxRequest request = CreateDefaultGetTaxRequest();	
			request.OriginAddress = CreateDefaultShipFromAddress();
			request.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			request.Lines.Add(line);

			line.OriginAddress = request.OriginAddress.Clone();
			line.DestinationAddress = request.DestinationAddress.Clone();

			//have to alter addresses so that they aren't equal and consolidated
			line.OriginAddress.Line2 = "Ste 130";
			line.DestinationAddress.Line1 = "5474 Foster Rd";

			GetTaxResult result = _taxSvc.GetTax(request);
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode, "Expected a successful result.");
	
			Assert.IsTrue(request.OriginAddress != request.Lines[0].OriginAddress, "Should not be the same OriginAddress object.");
			Assert.IsTrue(request.DestinationAddress != request.Lines[0].DestinationAddress, "Should not be the same DestinationAddress object.");
		}

		[Test]
		public void GetTaxAddressTest4()
		{
			//test that a line with an address referencing the request address is referenced internally, i.e. not new
			GetTaxRequest request = CreateDefaultGetTaxRequest();	
			request.OriginAddress = CreateDefaultShipFromAddress();
			request.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			request.Lines.Add(line);

			line.OriginAddress = request.OriginAddress;
			line.DestinationAddress = request.DestinationAddress;

			GetTaxResult result = _taxSvc.GetTax(request);
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode, "Expected a successful result.");
	
			Assert.IsTrue(request.OriginAddress == request.Lines[0].OriginAddress, "Should be the same object.");
			Assert.IsTrue(request.DestinationAddress == request.Lines[0].DestinationAddress, "Should be the same object.");
		}

		[Test]
		public void GetTaxAddressTest5()
		{
			//test that two separate but similar address instances on request are replaced by a single address object
			GetTaxRequest request = CreateDefaultGetTaxRequest();	
			request.OriginAddress = CreateDefaultShipFromAddress();
			request.DestinationAddress = CreateDefaultShipFromAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			request.Lines.Add(line);

			line.OriginAddress = CreateDefaultShipFromAddress();
			line.DestinationAddress = CreateDefaultShipFromAddress();

			GetTaxResult result = _taxSvc.GetTax(request);
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode, "Expected a successful result.");
	
			Assert.IsTrue(request.OriginAddress == request.DestinationAddress, "Should be the same object.");

			//test that a separate but similar address instance on a child is replaced by a reference to request's address
			Assert.IsTrue(request.OriginAddress == request.Lines[0].OriginAddress, "Should be the same object.");
			Assert.IsTrue(request.DestinationAddress == request.Lines[0].DestinationAddress, "Should be the same object.");
		}

		[Test]
		public void GetTaxAddressTest6()
		{
			//test that addresses specified on lines but not at the request level
			GetTaxRequest request = CreateDefaultGetTaxRequest();	
			Line line = CreateDefaultGetTaxRequestLine();
			request.Lines.Add(line);

			line.OriginAddress = CreateDefaultShipFromAddress();
			line.DestinationAddress = CreateDefaultShipFromAddress();

			GetTaxResult result = _taxSvc.GetTax(request);
			
			foreach (Message message in result.Messages)
			{
				Console.WriteLine(message.Name + ": " + message.Summary);
			}
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode, "Expected a successful result.");
		}

		[Test]
		//[Ignore("Not yet implemented in 4.1")]
		public void GetTaxTransactionIdTest()
		{
			GetTaxRequest request = CreateDefaultGetTaxRequest();	
			request.OriginAddress = CreateDefaultShipFromAddress();
			request.DestinationAddress = CreateDefaultShipFromAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			request.Lines.Add(line);

			line.OriginAddress = CreateDefaultShipFromAddress();
			line.DestinationAddress = CreateDefaultShipFromAddress();

			GetTaxResult result = _taxSvc.GetTax(request);
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode, "Expected a successful result.");
			Assert.IsTrue(Int64.Parse(result.TransactionId) > 0, "Expected a transaction ID.");
		}

		[Test]
		[ExpectedException(typeof(AvaTaxException))]
		public void BadAccountExceptionTest()
		{
			_taxSvc.Configuration.Security.Account = "111111111";
            GetTaxRequest request = CreateTaxRequest();	
			_taxSvc.GetTax(request);
		}

		[Test]
		public void APIChangesTest()
		{
			//if this compiles, then it passes the initial test: all changes are there
			GetTaxRequest request = new GetTaxRequest();
			request.DocType = DocumentType.ReturnOrder;
			request.DocType = DocumentType.ReturnInvoice;
			request.CustomerCode = "ABC";
			request.CustomerUsageType = "TEST";
			request.PurchaseOrderNo = "AB123";

			request = CreateDefaultGetTaxRequest();
			request.DocType = DocumentType.SalesOrder;
			request.OriginAddress = CreateDefaultShipFromAddress();
			request.DestinationAddress = CreateDefaultShipFromAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			request.Lines.Add(line);

			GetTaxResult result = _taxSvc.GetTax(request);
			Assert.IsTrue(result.TotalTaxable != 0);
			Assert.IsTrue(result.TotalDiscount == 0);
			Assert.IsTrue(result.TotalExemption == 0);

			TaxLine taxLine = result.TaxLines[0];
			Assert.IsTrue(taxLine.Taxable != 0);
			Assert.IsTrue(taxLine.Exemption == 0);
			Assert.IsTrue(taxLine.Taxability);
			//Assert.IsTrue(taxLine.BoundaryLevel == BoundaryLevel.Zip9);

			TaxDetail taxDetail = taxLine.TaxDetails[0];
			Assert.IsTrue(taxDetail.JurisType == JurisdictionType.State);
			Assert.IsTrue(taxDetail.JurisCode != null);

			PostTaxRequest postRequest = new PostTaxRequest();
			postRequest.DocType = DocumentType.PurchaseInvoice;

			CommitTaxRequest commitRequest = new CommitTaxRequest();
			commitRequest.DocType = DocumentType.PurchaseInvoice;

			CancelTaxRequest cancelRequest = new CancelTaxRequest();
			cancelRequest.DocType = DocumentType.PurchaseInvoice;

			GetTaxHistoryRequest historyRequest = new GetTaxHistoryRequest();
			historyRequest.DocType = DocumentType.PurchaseInvoice;
		}

		[Test]
		public void APIChangesTest2()
		{
			//for 4.1 release
			//if this compiles, then it passes the initial test: all changes are there
			GetTaxRequest request = new GetTaxRequest();
			request.DocType = DocumentType.SalesOrder;
			request.DetailLevel = DetailLevel.Tax;
			request.CustomerCode = "ABC";
			request.CustomerUsageType = "TEST";
			request.PurchaseOrderNo = "AB123";

			request = CreateDefaultGetTaxRequest();
			request.DocType = DocumentType.SalesOrder;
			request.OriginAddress = CreateDefaultShipFromAddress();
			request.DestinationAddress = CreateDefaultShipFromAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			request.Lines.Add(line);

			GetTaxResult result = _taxSvc.GetTax(request);
			Assert.IsTrue(result != null);
			foreach (TaxLine taxLine in result.TaxLines)
			{
				foreach (TaxDetail taxDetail in taxLine.TaxDetails)
				{
					//we don't care what the values are, just that the property exists
					Assert.IsTrue(taxDetail.Exemption == 0 || taxDetail.Exemption != 0);
					Assert.IsTrue(taxDetail.Taxable == 0 || taxDetail.Taxable != 0);
					Assert.IsTrue(taxDetail.NonTaxable == 0 || taxDetail.NonTaxable != 0);
				}
			}
		}

		[Test]
		public void ReturnOrderTest()
		{
			GetTaxRequest request = CreateDefaultGetTaxRequest();
			request.DocType = DocumentType.ReturnOrder;
			request.OriginAddress = CreateDefaultShipFromAddress();
			request.DestinationAddress = CreateDefaultShipToAddress();
			request.Lines.Add(CreateDefaultGetTaxRequestLine(1));

			GetTaxResult result = _taxSvc.GetTax(request);
			if (result.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(result));
			}
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
		}

		[Test]
		public void ReturnInvoiceTest()
		{
			GetTaxRequest request = CreateDefaultGetTaxRequest();
			request.DocType = DocumentType.ReturnInvoice;
			request.OriginAddress = CreateDefaultShipFromAddress();
			request.DestinationAddress = CreateDefaultShipToAddress();
			request.Lines.Add(CreateDefaultGetTaxRequestLine(1));

			GetTaxResult result = _taxSvc.GetTax(request);
			if (result.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(result));
			}
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
		}

		[Test]
		public void TotalDiscountTest()
		{
			GetTaxRequest request = CreateDefaultGetTaxRequest();
			request.Discount = 10M;
			request.OriginAddress = CreateDefaultShipFromAddress();
			request.DestinationAddress = CreateDefaultShipToAddress();
			request.Lines.Add(CreateDefaultGetTaxRequestLine(1));
			request.Lines[0].Discounted = true;

			GetTaxResult result = _taxSvc.GetTax(request);
			if (result.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(result));
			}
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
			Assert.AreEqual(request.Discount, result.TotalDiscount);

			TaxLine taxLine = result.TaxLines[0];
			Assert.AreEqual(true, taxLine.Taxability);
		}

		[Test]
		public void Bug7340Test()
		{
			for (int i = 0; i < 20; i++)
			{
				Thread.Sleep(100);
				_taxSvc.Ping("");
			}
			Assert.IsTrue(true, "If we reached this line, then bug fixed; otherwise fill fail on the 6th Ping()");
		}

		[Test]
		public void NewAPIMethodsTest()
		{
			//ensures the new methods are on the affected classes
			GetTaxRequest request = CreateDefaultGetTaxRequest();
			request.OriginAddress = CreateDefaultShipFromAddress();
			request.DestinationAddress = CreateDefaultShipToAddress();
			request.Lines.Add(CreateDefaultGetTaxRequestLine(1));

			GetTaxResult result = _taxSvc.GetTax(request);
			if (result.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(result));
			}
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
			TaxLine taxLine = result.TaxLines[0];

			//new methods
			Assert.IsTrue(request.ReferenceCode == null || request.ReferenceCode != null);
			Assert.IsTrue(taxLine.TaxDetails[0].JurisName == null || taxLine.TaxDetails[0].JurisName != null);
			Assert.IsTrue(true, "Getting to this line means we didn't throw an exception so methods exist on public API");

		}

		/// <summary>
        /// LocationCode Test for GetTax
		/// </summary>
		[Test]		
		public void LocationCodeAndCommitDocCodeTest()
		{
			GetTaxRequest getTaxRequest = CreateDefaultGetTaxRequest();//true
			getTaxRequest.DocCode = "Adapter_PostTest_" + DateTime.Now.Ticks;
			getTaxRequest.DocType = DocumentType.SalesInvoice;
			getTaxRequest.CustomerCode = "PostTest01";
			getTaxRequest.OriginAddress = CreateDefaultShipFromAddress();
			getTaxRequest.DestinationAddress = CreateDefaultShipToAddress();
			//newly added field Location Code
			getTaxRequest.LocationCode = "TestLocationCode007";
			Line line = CreateDefaultGetTaxRequestLine();
			getTaxRequest.Lines.Add(line);

			GetTaxResult getTaxResult = _taxSvc.GetTax(getTaxRequest);
			Assert.AreEqual(SeverityLevel.Success, getTaxResult.ResultCode, "Expected GetTax to pass");

			PostTaxResult postTaxResult = _taxSvc.PostTax(CreatePostTaxRequest(getTaxRequest, getTaxResult));
			Assert.AreEqual(SeverityLevel.Success, postTaxResult.ResultCode, "Expected PostTax to pass");

            CommitTaxRequest commitTaxRequest = new CommitTaxRequest();
            commitTaxRequest.CompanyCode = getTaxRequest.CompanyCode;
            commitTaxRequest.DocType = getTaxRequest.DocType;
		    string docCode = getTaxRequest.DocCode;// +"NEW";
            commitTaxRequest.DocCode = docCode;
            commitTaxRequest.CompanyCode = getTaxRequest.CompanyCode;
            commitTaxRequest.DocType = getTaxRequest.DocType;
            commitTaxRequest.DocCode = getTaxRequest.DocCode;

			CommitTaxResult commitTaxResult = _taxSvc.CommitTax(commitTaxRequest);
			
			if (commitTaxResult.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(commitTaxResult));
			}
			Assert.AreEqual(SeverityLevel.Success, commitTaxResult.ResultCode);
			
			//Calling GetTaxHistory Method to verify the DocCode
			GetTaxHistoryRequest historyRequest = new GetTaxHistoryRequest();
			historyRequest.CompanyCode = getTaxRequest.CompanyCode;
			historyRequest.DocType = getTaxRequest.DocType;
			historyRequest.DocCode = docCode;
			historyRequest.DetailLevel = DetailLevel.Tax;
			
			GetTaxHistoryResult historyResult = _taxSvc.GetTaxHistory(historyRequest);

			if (historyResult.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(historyResult));
			}
			Assert.AreEqual(SeverityLevel.Success, historyResult.ResultCode);
			Assert.IsNotNull(historyResult.GetTaxRequest);
			Assert.IsNotNull(historyResult.GetTaxResult);
			Assert.AreEqual(historyRequest.CompanyCode, historyResult.GetTaxRequest.CompanyCode);
			
			Assert.AreEqual(getTaxRequest.LocationCode, historyResult.GetTaxRequest.LocationCode);
			Console.WriteLine("Printing LocationCodeRequset :"+ getTaxRequest.LocationCode +"Printing LocationCodeResuset :"+ historyResult.GetTaxRequest.LocationCode);
			
			Assert.AreEqual(docCode, historyResult.GetTaxRequest.DocCode);
			Console.WriteLine("Printing DocCode :"+ docCode +" Printing historyResult.GetTaxRequest DocCode :"+ historyResult.GetTaxRequest.DocCode);				
			Assert.AreEqual(docCode, historyResult.GetTaxResult.DocCode);
			Console.WriteLine("Printing DocCode  :"+ docCode +" Printing historyResult.GetTaxRequest DocCode :"+ historyResult.GetTaxResult.DocCode);
		}

		//4.16 TestCase to check newly added fields
		[Test]
		public void AdjustTest()
		{
			GetTaxRequest request = CreateDefaultGetTaxRequest();
			request.OriginAddress = CreateDefaultShipFromAddress();
			request.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			request.Lines.Add(line);
			
			Line line1 = new Line();
			line1.Amount = 100m;
			line1.No = "2";
			line1.Qty = 2;
			line1.ItemCode = "B";
			request.Lines.Add(line1);
			request.Commit = true;
			
			GetTaxResult result = _taxSvc.GetTax(request);

			if (result.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(result));
			}
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
			Assert.AreEqual(1, result.Version);
			Assert.AreEqual(false, result.Locked);
			Assert.AreEqual(0, result.AdjustmentReason);
			Assert.AreEqual("", result.AdjustmentDescription);
			Assert.AreEqual(DocStatus.Committed, result.DocStatus);
			Assert.AreEqual(result.DocDate, result.TaxDate);

			request.Lines[0].Amount = 200m;
			request.Lines[0].Qty = 2;
			// And change the destination address
			request.DestinationAddress.Line1 = "302 Betsy Pack Dr";
			request.DestinationAddress.City = "Jasper";
			request.DestinationAddress.Region = "TN";
			request.DestinationAddress.PostalCode = "37347-3316";
            request.Commit = false;

			AdjustTaxRequest adjustTaxRequest = new AdjustTaxRequest();
			adjustTaxRequest.AdjustmentReason = 4; // Price or Qty Adjustment
			adjustTaxRequest.GetTaxRequest = request;
			adjustTaxRequest.AdjustmentDescription = "Testing Adjustment";
			AdjustTaxResult adjustTaxResult = _taxSvc.AdjustTax(adjustTaxRequest);

            if (adjustTaxResult.Messages.Count > 0)
            {
                Console.WriteLine(FormatMessages(adjustTaxResult));
            }
            Assert.AreEqual(SeverityLevel.Success, adjustTaxResult.ResultCode);
			Assert.AreEqual(2, adjustTaxResult.Version);
			Assert.AreEqual(4, adjustTaxResult.AdjustmentReason);
			Assert.AreEqual("Testing Adjustment", adjustTaxResult.AdjustmentDescription);
			Assert.AreEqual(DocStatus.Committed, result.DocStatus);

            // Cancel the last adjustment
            CancelTaxRequest cancelRequest = new CancelTaxRequest();
            cancelRequest.CompanyCode = request.CompanyCode;
            cancelRequest.DocType = adjustTaxResult.DocType;
            cancelRequest.DocCode = adjustTaxResult.DocCode;

            cancelRequest.CancelCode = CancelCode.AdjustmentCancelled;
            CancelTaxResult cancelResult = _taxSvc.CancelTax(cancelRequest);

            if (cancelResult.Messages.Count > 0)
            {
                Console.WriteLine(FormatMessages(cancelResult));
            }
            Assert.AreEqual(SeverityLevel.Success, cancelResult.ResultCode);

			// Check the version
			GetTaxHistoryRequest taxHistoryRequest = new GetTaxHistoryRequest();
            taxHistoryRequest.CompanyCode = request.CompanyCode;
            taxHistoryRequest.DocType = request.DocType;
            taxHistoryRequest.DocCode = request.DocCode;

			taxHistoryRequest.DetailLevel = DetailLevel.Document;
			GetTaxHistoryResult taxHistoryResult = _taxSvc.GetTaxHistory(taxHistoryRequest);
			//TaxSvcAssert.IsSuccess(taxHistoryResult);
			Assert.AreEqual(0, taxHistoryResult.GetTaxResult.AdjustmentReason);			
		}

        /// <summary>
        /// This tests the return of TaxIncluded.
        /// </summary>
        [Test]
        public void TaxIncludedTest()
        {
            GetTaxRequest taxRequest = new GetTaxRequest();
            taxRequest.CompanyCode = "";
            taxRequest.CustomerCode = "None";
            taxRequest.CustomerUsageType = "";
            taxRequest.DetailLevel = DetailLevel.Tax;
            taxRequest.DocCode = "5.2 Issues";
            taxRequest.DocDate = DateTime.Today;
            taxRequest.DocType = DocumentType.SalesInvoice;
            taxRequest.Commit = false;

            Address address = new Address();
            address.Line1 = "435 Ericksen Ave";
            address.City = "Bainbridge Island";
            address.Region = "WA";
            address.PostalCode = "98110";
            address.Country = "US";

            taxRequest.OriginAddress = taxRequest.DestinationAddress = address;

            Line line = new Line();
            line.No = "1";
            line.ItemCode = "SKU-001";
            line.TaxCode = "";
            line.Qty = 1;
            line.Amount = 100m;

            taxRequest.Lines.Add(line);
            line = new Line();
            line.No = "2";
            line.ItemCode = "SKU-001";
            line.TaxCode = "";
            line.Qty = 1;
            line.Amount = 100m;
            line.TaxIncluded = true;

            taxRequest.Lines.Add(line);

            GetTaxResult taxResult = _taxSvc.GetTax(taxRequest);

            if (taxResult.Messages.Count > 0)
            {
                Console.WriteLine(FormatMessages(taxResult));
            }
            Assert.AreEqual(SeverityLevel.Success, taxResult.ResultCode);
            Assert.AreEqual(8.6m, taxResult.TaxLines[0].Tax);
			Assert.AreEqual(false, taxResult.TaxLines[0].TaxIncluded);
            Assert.AreEqual(7.92m, taxResult.TaxLines[1].Tax);
			Assert.AreEqual(true, taxResult.TaxLines[1].TaxIncluded);			
        }


        /// <summary>
        /// This tests the return of TaxCalculated.
        /// </summary>
        [Test]
        public void TaxCalculatedTest()
        {
            GetTaxRequest taxRequest = new GetTaxRequest();
            taxRequest.CompanyCode = "";
            taxRequest.CustomerCode = "None";
            taxRequest.CustomerUsageType = "";
            taxRequest.DetailLevel = DetailLevel.Tax;
            taxRequest.DocCode = "5.2 Issues";
            taxRequest.DocDate = DateTime.Today;
            taxRequest.DocType = DocumentType.SalesInvoice;
            taxRequest.Commit = false;

            Address address = new Address();
            address.Line1 = "435 Ericksen Ave";
            address.City = "Bainbridge Island";
            address.Region = "WA";
            address.PostalCode = "98110";
            address.Country = "US";

            taxRequest.OriginAddress = taxRequest.DestinationAddress = address;

            Line line = new Line();
            line.No = "1";
            line.ItemCode = "SKU-001";
            line.TaxCode = "";
            line.Qty = 1;
            line.Amount = 100m;

            taxRequest.Lines.Add(line);
            line = new Line();
            line.No = "2";
            line.ItemCode = "SKU-001";
            line.TaxCode = "";
            line.Qty = 1;
            line.Amount = 100m;

            // Now put in a tax override on the second line
            line.TaxOverride.TaxOverrideType = TaxOverrideType.TaxAmount;
            line.TaxOverride.TaxAmount = 10.00m;
            line.TaxOverride.Reason = "Testing";

            taxRequest.Lines.Add(line);

            GetTaxResult taxResult = _taxSvc.GetTax(taxRequest);

            if (taxResult.Messages.Count > 0)
            {
                Console.WriteLine(FormatMessages(taxResult));
            }
            Assert.AreEqual(SeverityLevel.Success, taxResult.ResultCode);
            Assert.AreEqual(18.60m, taxResult.TotalTax);
            Assert.AreEqual(17.20m, taxResult.TotalTaxCalculated);
            Assert.AreEqual(taxResult.TaxLines[0].Tax, taxResult.TaxLines[0].TaxCalculated);
            Assert.AreEqual(taxResult.TaxLines[0].TaxDetails[0].Tax, taxResult.TaxLines[0].TaxDetails[0].TaxCalculated);
            Assert.AreEqual(taxResult.TaxLines[0].TaxDetails[1].Tax, taxResult.TaxLines[0].TaxDetails[1].TaxCalculated);
            Assert.AreEqual(line.TaxOverride.TaxAmount, taxResult.TaxLines[1].Tax);
            Assert.AreEqual(6.5m, taxResult.TaxLines[0].TaxDetails[0].TaxCalculated);
            Assert.AreEqual(2.1m, taxResult.TaxLines[0].TaxDetails[1].TaxCalculated);
            Assert.AreEqual(6.5m, taxResult.TaxLines[0].TaxDetails[0].Tax);
            Assert.AreEqual(2.1m, taxResult.TaxLines[0].TaxDetails[1].Tax);
            Assert.AreEqual(6.5m, taxResult.TaxLines[1].TaxDetails[0].TaxCalculated);
            Assert.AreEqual(2.1m, taxResult.TaxLines[1].TaxDetails[1].TaxCalculated);
            Assert.AreEqual(7.56m, taxResult.TaxLines[1].TaxDetails[0].Tax);
            Assert.AreEqual(2.44m, taxResult.TaxLines[1].TaxDetails[1].Tax);
        }


		/// <summary>
		/// This tests the return RateTypeId.
		/// </summary>
		[Test]
		public void RateTypeIdTest()
		{
			// Check for RateType = "G" (General)
			// 1. RateType returned by GetTax in both the TaxLine and TaxSummary
			GetTaxRequest taxRequest = CreateDefaultGetTaxRequest();
            taxRequest.DocDate = new DateTime(2010,1,1); // RateType changed on 3/1/2010, so this needs to be prior
			taxRequest.OriginAddress = CreateDefaultShipFromAddress();
			taxRequest.DestinationAddress = CreateDefaultShipFromAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			taxRequest.Lines.Add(line);
			taxRequest.DetailLevel = DetailLevel.Diagnostic;

			GetTaxResult taxResult = _taxSvc.GetTax(taxRequest);
			Assert.AreEqual(SeverityLevel.Success, taxResult.ResultCode);

			if (taxResult.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(taxResult));
			}
			ValidateTaxResultForRateType(taxResult, "G");
			
			// 2. RateType returned by GetTaxHistory in both the TaxLine and TaxSummary
			GetTaxHistoryRequest historyRequest = new GetTaxHistoryRequest();
			historyRequest.CompanyCode = taxRequest.CompanyCode;
			historyRequest.DocType = taxRequest.DocType;
			historyRequest.DocCode = taxRequest.DocCode;
			historyRequest.DetailLevel = taxRequest.DetailLevel;
							
			GetTaxHistoryResult historyResult = _taxSvc.GetTaxHistory(historyRequest);
			Assert.AreEqual(SeverityLevel.Success, historyResult.ResultCode);

			if (historyResult.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(historyResult));
			}
			ValidateTaxResultForRateType(historyResult.GetTaxResult, "G");


			// Check for RateType = "F" (Food)
			// 1. RateType returned by GetTax in both the TaxLine and TaxSummary
			taxRequest= CreateDefaultGetTaxRequest();
            taxRequest.DocDate = new DateTime(2010, 1, 1); // RateType changed on 3/1/2010, so this needs to be prior

			Address address = new Address();
			address.Line1 = "5474 NE Foster Rd";
			address.City = "Lakewood";
			address.Region = "CO";
			address.PostalCode = "80226-1510";
			
			taxRequest.OriginAddress = address;
			taxRequest.DestinationAddress = address;

			line = CreateDefaultGetTaxRequestLine();
			line.TaxCode = "PF050600";
			taxRequest.Lines.Add(line);
			taxRequest.DetailLevel = DetailLevel.Diagnostic;

			taxResult = _taxSvc.GetTax(taxRequest);
			Assert.AreEqual(SeverityLevel.Success, taxResult.ResultCode);

			if (taxResult.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(taxResult));
			}
			ValidateTaxResultForRateType(taxResult, "F");
			
			// 2. RateType returned by GetTaxHistory in both the TaxLine and TaxSummary
			historyRequest = new GetTaxHistoryRequest();
			historyRequest.CompanyCode = taxRequest.CompanyCode;
			historyRequest.DocType = taxRequest.DocType;
			historyRequest.DocCode = taxRequest.DocCode;
			historyRequest.DetailLevel = taxRequest.DetailLevel;
							
			historyResult = _taxSvc.GetTaxHistory(historyRequest);
			Assert.AreEqual(SeverityLevel.Success, historyResult.ResultCode);
			
			if (historyResult.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(historyResult));
			}
			
			ValidateTaxResultForRateType(historyResult.GetTaxResult, "F");
		}

	    /// <summary>
        /// Tax batch operations test for multiple GetTax
        /// </summary>
        //[Test]
        //[Ignore("Enable once service side Batch operations are done")]
        //public void TaxBatchOperationsMultiGetTaxTest()
        //{
        //    SubmitTaxBatchRequest submitTaxBatchRequest = new SubmitTaxBatchRequest();
        //    submitTaxBatchRequest.Requests.Add(CreateTaxRequest());
        //    submitTaxBatchRequest.Requests.Add(CreateTaxRequest());
        //    SubmitTaxBatchResult submitTaxBatchResult = _taxSvc.SubmitBatch(submitTaxBatchRequest);
        //    Assert.AreEqual(SeverityLevel.Success, submitTaxBatchResult.ResultCode);

        //    GetTaxBatchRequest getTaxBatchRequest = new GetTaxBatchRequest();
        //    getTaxBatchRequest.BatchId = submitTaxBatchResult.BatchId;
        //    GetTaxBatchResult getTaxBatchResult = _taxSvc.GetBatch(getTaxBatchRequest);
        //    Assert.AreEqual(SeverityLevel.Success, getTaxBatchResult.ResultCode);
        //    Assert.AreEqual("Completed", getTaxBatchResult.BatchStatus);
        //    Assert.AreEqual(submitTaxBatchRequest.Requests.Count, getTaxBatchResult.Results.Count);

        //    CancelTaxBatchRequest cancelTaxBatchRequest = new CancelTaxBatchRequest();
        //    cancelTaxBatchRequest.BatchId = submitTaxBatchResult.BatchId;
        //    CancelTaxBatchResult cancelTaxBatchResult = _taxSvc.CancelBatch(cancelTaxBatchRequest);
        //    Assert.AreEqual(SeverityLevel.Success, cancelTaxBatchResult.ResultCode);
        //}

        /// <summary>
        /// Tax batch operations test for GetTax, PostTax, CommitTax, and GetTaxHistory
        /// </summary>
        //[Test]
        //[Ignore("Enable once service side Batch operations are done")]
        //public void TaxBatchOperationsMultiTaxTest()
        //{ 
        //    SubmitTaxBatchRequest submitTaxBatchRequest = new SubmitTaxBatchRequest();

        //    GetTaxRequest getTaxRequest = CreateTaxRequest();
        //    submitTaxBatchRequest.Requests.Add(getTaxRequest);
        //    submitTaxBatchRequest.Requests.Add(CreatePostTaxRequest(getTaxRequest));
        //    submitTaxBatchRequest.Requests.Add(CreateCommitTaxRequest(getTaxRequest));
        //    submitTaxBatchRequest.Requests.Add(CreateGetTaxHistoryRequest(getTaxRequest));

        //    SubmitTaxBatchResult submitTaxBatchResult = _taxSvc.SubmitBatch(submitTaxBatchRequest);
        //    Assert.AreEqual(SeverityLevel.Success, submitTaxBatchResult.ResultCode);

        //    GetTaxBatchRequest getTaxBatchRequest = new GetTaxBatchRequest();
        //    getTaxBatchRequest.BatchId = submitTaxBatchResult.BatchId;
        //    GetTaxBatchResult getTaxBatchResult = _taxSvc.GetBatch(getTaxBatchRequest);
        //    Assert.AreEqual(SeverityLevel.Success, getTaxBatchResult.ResultCode);
        //    Assert.AreEqual("Completed", getTaxBatchResult.BatchStatus);
        //    Assert.AreEqual(submitTaxBatchRequest.Requests.Count, getTaxBatchResult.RecordCount);
        //    Assert.AreEqual(submitTaxBatchRequest.Requests.Count, getTaxBatchResult.Results.Count);

        //    CancelTaxBatchRequest cancelTaxBatchRequest = new CancelTaxBatchRequest();
        //    cancelTaxBatchRequest.BatchId = submitTaxBatchResult.BatchId;
        //    CancelTaxBatchResult cancelTaxBatchResult = _taxSvc.CancelBatch(cancelTaxBatchRequest);
        //    Assert.AreEqual(SeverityLevel.Success, cancelTaxBatchResult.ResultCode);
        //}

        /// <summary>
        /// Tax batch operations test
        /// </summary>
        //[Test]
        //[Ignore("Enable once service side Batch operations are done")]
        //public void TaxBatchOperationsTest()
        //{
        //    try
        //    {
        //        SubmitTaxBatchRequest submitTaxBatchRequest = new SubmitTaxBatchRequest();
        //        GetTaxRequest getTaxRequest = CreateTaxRequest();
        //        submitTaxBatchRequest.Requests.Add(getTaxRequest);

        //        getTaxRequest = CreateTaxRequest();
        //        string documentCode = getTaxRequest.DocCode;
        //        submitTaxBatchRequest.Requests.Add(getTaxRequest);
        //        SubmitTaxBatchResult submitTaxBatchResult = _taxSvc.SubmitBatch(submitTaxBatchRequest);
        //        Assert.AreEqual(SeverityLevel.Success, submitTaxBatchResult.ResultCode);

        //        GetTaxBatchRequest getTaxBatchRequest = new GetTaxBatchRequest();
        //        getTaxBatchRequest.BatchId = submitTaxBatchResult.BatchId;
        //        GetTaxBatchResult getTaxBatchResult = _taxSvc.GetBatch(getTaxBatchRequest);
        //        Assert.AreEqual(SeverityLevel.Success, getTaxBatchResult.ResultCode);
        //        Assert.AreEqual(submitTaxBatchRequest.Requests.Count, getTaxBatchResult.Results.Count);

        //        CancelTaxBatchRequest cancelTaxBatchRequest = new CancelTaxBatchRequest();
        //        cancelTaxBatchRequest.BatchId = submitTaxBatchResult.BatchId;
        //        CancelTaxBatchResult cancelTaxBatchResult = _taxSvc.CancelBatch(cancelTaxBatchRequest);
        //        Assert.AreEqual(SeverityLevel.Success, cancelTaxBatchResult.ResultCode);

        //        // Verify history
        //        GetTaxHistoryRequest historyRequest = new GetTaxHistoryRequest();
        //        historyRequest.CompanyCode = "DEFAULT";
        //        historyRequest.DetailLevel = DetailLevel.Tax;
        //        historyRequest.DocCode = Convert.ToString(documentCode);
        //        historyRequest.DocType = DocumentType.SalesInvoice;

        //        SubmitTaxBatchRequest submitTaxBatchHistoryRequest = new SubmitTaxBatchRequest();
        //        submitTaxBatchHistoryRequest.Requests.Add(historyRequest);
        //        SubmitTaxBatchResult submitTaxBatchHistoryResult = _taxSvc.SubmitBatch(submitTaxBatchHistoryRequest);
        //        Assert.AreEqual(SeverityLevel.Success, submitTaxBatchHistoryResult.ResultCode);

        //        GetTaxBatchRequest getTaxBatchHistoryRequest = new GetTaxBatchRequest();
        //        getTaxBatchHistoryRequest.BatchId = submitTaxBatchResult.BatchId;
        //        GetTaxBatchResult getTaxBatchHistoryResult = _taxSvc.GetBatch(getTaxBatchHistoryRequest);
        //        Assert.AreEqual(SeverityLevel.Success, getTaxBatchHistoryResult.ResultCode);
        //        Assert.AreEqual(submitTaxBatchRequest.Requests.Count, getTaxBatchHistoryResult.Results.Count);

        //        CancelTaxBatchRequest cancelTaxBatchHistoryRequest = new CancelTaxBatchRequest();
        //        cancelTaxBatchRequest.BatchId = submitTaxBatchResult.BatchId;
        //        CancelTaxBatchResult cancelTaxBatchHistoryResult = _taxSvc.CancelBatch(cancelTaxBatchHistoryRequest);
        //        Assert.AreEqual(SeverityLevel.Success, cancelTaxBatchHistoryResult.ResultCode);

        //        // Reconcile
        //        ReconcileTaxHistoryRequest reconcileTaxHistoryRequest = new ReconcileTaxHistoryRequest();
        //        reconcileTaxHistoryRequest.CompanyCode = "DEFAULT";
        //        reconcileTaxHistoryRequest.StartDate = DateTime.Today;
        //        reconcileTaxHistoryRequest.EndDate = DateTime.Today;
        //        reconcileTaxHistoryRequest.DocStatus = DocStatus.Committed;

        //        SubmitTaxBatchRequest submitTaxBatchReconcileRequest = new SubmitTaxBatchRequest();
        //        submitTaxBatchReconcileRequest.Requests.Add(reconcileTaxHistoryRequest);
        //        SubmitTaxBatchResult submitTaxBatchReconcileResult = _taxSvc.SubmitBatch(submitTaxBatchReconcileRequest);
        //        Assert.AreEqual(SeverityLevel.Success, submitTaxBatchReconcileResult.ResultCode);

        //        GetTaxBatchRequest getTaxBatchReconcileRequest = new GetTaxBatchRequest();
        //        getTaxBatchReconcileRequest.BatchId = submitTaxBatchReconcileResult.BatchId;
        //        GetTaxBatchResult getTaxBatchReconcileResult = _taxSvc.GetBatch(getTaxBatchReconcileRequest);
        //        Assert.AreEqual(SeverityLevel.Success, getTaxBatchResult.ResultCode);
        //        Assert.AreEqual(submitTaxBatchReconcileRequest.Requests.Count, getTaxBatchReconcileResult.Results.Count);

        //        foreach (ReconcileTaxHistoryResult reconcileTaxHistoryResult in getTaxBatchReconcileResult.Results)
        //        {
        //            foreach (GetTaxResult taxResult in reconcileTaxHistoryResult.GetTaxResults)
        //            {
        //                Assert.AreEqual(DocStatus.Committed, taxResult.DocStatus);
        //                Assert.AreEqual(DateTime.Today.ToShortDateString(), taxResult.DocDate.ToShortDateString());
        //            }
        //        }

        //        CancelTaxBatchRequest cancelTaxBatchReconcileRequest = new CancelTaxBatchRequest();
        //        cancelTaxBatchReconcileRequest.BatchId = submitTaxBatchReconcileResult.BatchId;
        //        CancelTaxBatchResult cancelTaxBatchReconcileResult = _taxSvc.CancelBatch(cancelTaxBatchReconcileRequest);
        //        Assert.AreEqual(SeverityLevel.Success, cancelTaxBatchReconcileResult.ResultCode);
        //    }
        //    catch (Exception exception)
        //    {
        //        Console.Write(exception.Message);
        //    }
        //}

	    /// <summary>
        /// GetTaxRequest validation test
        /// </summary>
        [Test]
        public void ValidGetTaxRequestTest1()
        {
            try
            {
                GetTaxRequest getTaxRequest = CreateTaxRequest();
                getTaxRequest.DocCode = "";
                GetTaxResult result = _taxSvc.GetTax(getTaxRequest);

                foreach (Message message in result.Messages)
                {
                    Console.WriteLine(message.Name + ": " + message.Summary);
                }
                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);           
            }
            catch (Exception ex)
            {
                Assert.Fail("ValidGetTaxRequestTest1 failed: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void GetTaxWithInvalidLocationCode()
        {
                
                GetTaxRequest request = CreateDefaultGetTaxRequest();
                request.LocationCode = "LocationCode";
                Line line = CreateDefaultGetTaxRequestLine();
                request.Lines.Add(line);

                GetTaxResult result = _taxSvc.GetTax(request);
                
                foreach (Message message in result.Messages)
                {
                    Console.WriteLine(message.Name + ": " + message.Summary);
                }
                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                Assert.AreEqual("TaxAddressError", result.Messages[0].Name);
      

        }

        [Test]
        public void GetTaxRequestForAccruedTaxAmount()
		{
            
			GetTaxRequest request = CreateDefaultGetTaxRequest();
			request.OriginAddress = CreateDefaultShipFromAddress();
			request.DestinationAddress = CreateDefaultShipToAddress();

			Line line = CreateDefaultGetTaxRequestLine();
			request.Lines.Add(line);
			
			//TaxOverride
			//request.IsTotalTaxOverriden = true;
			//request.TotalTaxOverride = 5m;
            request.TaxOverride.TaxOverrideType = TaxOverrideType.AccruedTaxAmount; 
			
			request.TaxOverride.Reason = "Return";

			GetTaxResult result = _taxSvc.GetTax(request);

			if (result.Messages.Count > 0)
			{
				Console.WriteLine(FormatMessages(result));
			}
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode);

			result = _taxSvc.GetTax(request);
            Assert.AreEqual(SeverityLevel.Success, result.ResultCode, "Test case should pass with AccruedTaxAmount.");
			
		}

        [Test]
        public void AccruedTaxAmountTest()
        {
            GetTaxRequest request = CreateDefaultGetTaxRequest();
            request.OriginAddress = CreateDefaultShipFromAddress();
            request.DestinationAddress = CreateDefaultShipToAddress();

            GetTaxRequest taxRequest = new GetTaxRequest
            {
                CompanyCode = "",
                CustomerCode = "None",
                CustomerUsageType = "",
                DetailLevel = DetailLevel.Tax,
                DocDate = DateTime.Today,
                DocType = DocumentType.PurchaseInvoice,
                Commit = false
                
            };

            request.OriginAddress = CreateDefaultAddressForTaxOverRide1();
            request.DestinationAddress = CreateDefaultAddressForTaxOverRide2();

            Line line = CreateDefaultGetTaxRequestLineForTaxOverRide("1", "SKU-001", "", 1, 1m);
            line.Amount = 10.00m;
            // TaxOverrideType.AccruedTaxAmount;
            
            request.Lines.Add(line);

            line = CreateDefaultGetTaxRequestLineForTaxOverRide("2", "SKU-001", "", 1, 0m);
            request.Lines.Add(line);

            line = CreateDefaultGetTaxRequestLineForTaxOverRide("3", "SKU-001", "", 1, 0m);
            request.Lines.Add(line);

            request.TaxOverride.Reason = "Testing";
         
            //TaxOverride taxOverride1 = new TaxOverride
            //{
            //    TaxOverrideType = TaxOverrideType.AccruedTaxAmount,
            //    TaxAmount = 10.00m,
            //    Reason = "Testing"
            //};
            //TaxOverride taxOverride2 = new TaxOverride
            //{
            //    TaxOverrideType = TaxOverrideType.AccruedTaxAmount,
            //    TaxAmount = 37.00m,
            //    Reason = "Testing"
            //};

            //TaxOverride taxOverride3 = new TaxOverride
            //{
            //    TaxOverrideType = TaxOverrideType.AccruedTaxAmount,
            //    TaxAmount = 99.00m,
            //    Reason = "Testing"
            //};
            // Now put in a tax override on the first line
            //taxRequest.Lines[0].TaxOverride = taxOverride1;
            //taxRequest.Lines[1].TaxOverride = taxOverride2;
            //taxRequest.Lines[2].TaxOverride = taxOverride3;


            GetTaxResult result = _taxSvc.GetTax(request);

            if (result.Messages.Count > 0)
            {
                Console.WriteLine(FormatMessages(result));
            }
            Assert.AreEqual(SeverityLevel.Success, result.ResultCode);

            result = _taxSvc.GetTax(request);
            Assert.AreEqual(SeverityLevel.Success, result.ResultCode, "Test case should pass with AccruedTaxAmount.");

        }

		#region Helper Methods

		private void CompareHistory(GetTaxRequest taxRequest, GetTaxResult taxResult, GetTaxHistoryResult history)
		{
			GetTaxRequest historyRequest = history.GetTaxRequest;
			GetTaxResult historyResult = history.GetTaxResult;

			Assert.AreEqual(ToUpper(taxRequest.CompanyCode), ToUpper(historyRequest.CompanyCode), "Expected same CompanyCode");
			Assert.AreEqual(ToUpper(taxRequest.CompanyCode), ToUpper(historyRequest.CompanyCode), "Expected same Discount");
			Assert.AreEqual(taxRequest.Discount, historyRequest.Discount, "Expected same Discount");
			Assert.AreEqual(ToUpper(taxRequest.DocCode), ToUpper(historyRequest.DocCode), "Expected same DocCode (request)");
			Assert.AreEqual(taxRequest.DocDate, historyRequest.DocDate, "Expected same DocDate (request)");
			Assert.AreEqual(taxRequest.DocType, historyRequest.DocType, "Expected same DocType (request)");
			Assert.AreEqual(ToUpper(taxRequest.ExemptionNo), ToUpper(historyRequest.ExemptionNo), "Expected same ExemptionNo");
			Assert.AreEqual(ToUpper(taxRequest.CustomerCode), ToUpper(historyRequest.CustomerCode), "Expected same PurchaserCode");
			Assert.AreEqual(ToUpper(taxRequest.CustomerUsageType), ToUpper(historyRequest.CustomerUsageType), "Expected same PurchaserType");
			//not supported in 1.4: Assert.AreEqual(ToUpper(taxRequest.SalespersonCode), ToUpper(historyRequest.SalespersonCode), "Expected same SalespersonCode");

			Assert.AreEqual(ToUpper(taxResult.DocCode), ToUpper(historyResult.DocCode), "Expected same DocCode (result)");
			Assert.AreEqual(taxResult.DocDate, historyResult.DocDate, "Expected same DocDate (result)");
			Assert.AreEqual(taxResult.DocType, historyResult.DocType, "Expected same DocType (result)");
			Assert.AreEqual(taxResult.DocStatus, historyResult.DocStatus, "Expected same DocStatus");
			Assert.AreEqual(taxResult.Reconciled, historyResult.Reconciled, "Expected same Reconciled");
			Assert.AreEqual(taxResult.Timestamp.ToString(), historyResult.Timestamp.ToString(), "Expected same Timestamp");
			Assert.AreEqual(taxResult.TotalAmount, historyResult.TotalAmount, "Expected same TotalAmount");
			Assert.AreEqual(taxResult.TotalTaxable, historyResult.TotalTaxable, "Expected same TotalBase");
			Assert.AreEqual(taxResult.TotalTax, historyResult.TotalTax, "Expected same TotalTax");

			Assert.AreEqual(taxRequest.Lines.Count, historyRequest.Lines.Count, "Expected same number of lines");
			foreach (Line requestLine in taxRequest.Lines)
			{
				Line historyLine = historyRequest.Lines.GetItemByNo(requestLine.No);
				Assert.IsNotNull(historyLine, "Expected to find a history request line matching the original request line");

				Assert.AreEqual(requestLine.No, historyLine.No, "Expected same No");
				Assert.AreEqual(requestLine.Amount, historyLine.Amount, "Expected same Amount");
				Assert.AreEqual(requestLine.Discounted, historyLine.Discounted, "Expected same Discounted");
				Assert.AreEqual(ToUpper(requestLine.ExemptionNo), ToUpper(historyLine.ExemptionNo), "Expected same ExemptionNo");
				Assert.AreEqual(ToUpper(requestLine.ItemCode), ToUpper(historyLine.ItemCode), "Expected same ItemCode");
				// Assert.AreEqual(requestLine.Qty, historyLine.Qty); Qty is not currently preserved
				Assert.AreEqual(ToUpper(requestLine.Ref1), ToUpper(historyLine.Ref1), "Expected same Ref1");
				Assert.AreEqual(ToUpper(requestLine.Ref2), ToUpper(historyLine.Ref2), "Expected same Ref2");
				Assert.AreEqual(ToUpper(requestLine.RevAcct), ToUpper(historyLine.RevAcct), "Expected same RevAcct");
			}

			Assert.AreEqual(taxResult.TaxLines.Count, historyResult.TaxLines.Count, "Expected same number of tax lines");
			foreach (TaxLine resultLine in taxResult.TaxLines)
			{
				TaxLine historyResultLine = historyResult.TaxLines.GetItemByNo(resultLine.No);
				Assert.IsNotNull(historyResultLine, "Expected to find a history result taxline matching the original result taxline");

				Assert.AreEqual(resultLine.No, historyResultLine.No, "Expected same TaxLine No");
				Assert.AreEqual(resultLine.Taxable, historyResultLine.Taxable, "Expected same Base for Line No. " + historyResultLine.No);
				Assert.AreEqual(resultLine.Discount, historyResultLine.Discount, "Expected same Discount for Line No. " + historyResultLine.No);
				Assert.AreEqual(resultLine.Rate, historyResultLine.Rate, "Expected same Rate for Line No. " + historyResultLine.No);
				Assert.AreEqual(resultLine.Tax, historyResultLine.Tax, "Expected same Tax for Line No. " + historyResultLine.No);
				Assert.AreEqual(resultLine.TaxCode, historyResultLine.TaxCode, "Expected same TaxCode for Line No. " + historyResultLine.No);

				// TODO: Addresses

				// Line details
				Assert.AreEqual(resultLine.TaxDetails.Count, historyResultLine.TaxDetails.Count, "Expected same number of tax detail lines");
				foreach (TaxDetail resultDetail in resultLine.TaxDetails)
				{
					TaxDetail historyDetail = FindTaxDetail(resultDetail.JurisType, historyResultLine.TaxDetails);
					Assert.IsNotNull(historyDetail, "Expected to find a history result taxdetail matching the original result taxdetail");

					Assert.AreEqual(resultDetail.TaxType, historyDetail.TaxType, "Expected same TaxType");
					Assert.AreEqual(resultDetail.Base, historyDetail.Base, "Expected same Base for Detail TaxType " + historyDetail.TaxType.ToString());
					Assert.AreEqual(resultDetail.JurisCode, historyDetail.JurisCode, "Expected same FipsCode for Detail TaxType " + historyDetail.TaxType.ToString());
					Assert.AreEqual(resultDetail.JurisType, historyDetail.JurisType, "Expected same JurisdictionType for Detail TaxType " + historyDetail.TaxType.ToString());
					Assert.AreEqual(resultDetail.Rate, historyDetail.Rate, "Expected same Rate for Detail TaxType " + historyDetail.TaxType.ToString());
					Assert.AreEqual(resultDetail.Tax, historyDetail.Tax, "Expected same Tax for Detail TaxType " + historyDetail.TaxType.ToString());
				}
			}
		}

		private TaxDetail FindTaxDetail(JurisdictionType jurisdictionType, TaxDetails taxDetails)
		{
			TaxDetail match = null;

			foreach (TaxDetail detail in taxDetails)
			{
				if (detail.JurisType.Equals(jurisdictionType))
				{
					match = detail;
					break;
				}
			}

			return match;
		}

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
				if (message.RefersTo.Length > 0)
				{
					builder.AppendFormat("\tRefers To: {0}\n", message.RefersTo);
				}
				builder.AppendFormat("\tSource: {0}\n", message.Source);
			}
			builder.Append("--- END DEBUG MESSAGES ---");

			return builder.ToString();
		}

		private static string ToUpper(string s)
		{
			if (s != null)
			{
				return s.ToUpper();
			}
			else
			{
				return string.Empty;
			}
		}

		private static int CountAddresses(GetTaxRequest request)
		{
			ArrayList addresses = new ArrayList();

			if (request.OriginAddress != null && !addresses.Contains(request.OriginAddress))
			{
				addresses.Add(request.OriginAddress);
			}

			if (request.DestinationAddress != null && !addresses.Contains(request.DestinationAddress))
			{
				addresses.Add(request.DestinationAddress);
			}

			foreach (Line line in request.Lines)
			{
				if (line.OriginAddress != null && !addresses.Contains(line.OriginAddress))
				{
					addresses.Add(line.OriginAddress);
				}
				if (line.DestinationAddress != null && !addresses.Contains(line.DestinationAddress))
				{
					addresses.Add(line.DestinationAddress);
				}
			}

			return addresses.Count;
		}

		/// <summary>
		/// Tests the immutable values of two addresses to determine if they are *roughly* equivalent
		/// </summary>
		/// <param name="address1">Expected address</param>
		/// <param name="address2">Address to test</param>
		/// <returns>True if the addresses are roughly equivalent</returns>
		private static bool AreEquivalentAddresses(Address address1, Address address2)
		{
			bool equiv;

			if (address1 == null && address2 == null)
			{
				return true;
			}

			if (address1 == null || address2 == null)
			{
				//can't compare null address to non-null address
				return false;
			}

			equiv = (ToUpper(address1.Region) == ToUpper(address2.Region) && 
				address1.PostalCode == address2.PostalCode);

			//are there other reasonable tests we can add to test for equivalence
			//	between pre-validated and post-validated addresses?

			return equiv;
		}
        
        /// <summary>
        /// Create GetTax Request
        /// </summary>
        /// <returns>
        /// </returns>
        private GetTaxRequest CreateTaxRequest()
        {
            GetTaxRequest request = CreateDefaultGetTaxRequest();
            request.OriginAddress = CreateDefaultShipFromAddress();
            request.DestinationAddress = CreateDefaultShipToAddress();

            Line line = CreateDefaultGetTaxRequestLine();
            request.Lines.Add(line);

            return request;
        }

        /// <summary>
        /// Create PostTax Request
        /// </summary>
        /// <returns></returns>
        private PostTaxRequest CreatePostTaxRequest(GetTaxRequest getTaxRequest)
        {
            PostTaxRequest postTaxRequest = new PostTaxRequest();
            postTaxRequest.CompanyCode = getTaxRequest.CompanyCode;
            postTaxRequest.DocType = getTaxRequest.DocType;
            postTaxRequest.DocCode = getTaxRequest.DocCode;
            postTaxRequest.DocDate = getTaxRequest.DocDate;

            return postTaxRequest;
        }

        /// <summary>
        /// Create CommitTax Request
        /// </summary>
        /// <returns></returns>
        private CommitTaxRequest CreateCommitTaxRequest(GetTaxRequest getTaxRequest)
        {
            CommitTaxRequest commitTaxRequest = new CommitTaxRequest();
            commitTaxRequest.CompanyCode = getTaxRequest.CompanyCode;
            commitTaxRequest.DocType = getTaxRequest.DocType;
            commitTaxRequest.DocCode = getTaxRequest.DocCode;

            return commitTaxRequest;
        }

        /// <summary>
        /// Create GetTaxHistory Request
        /// </summary>
        /// <returns></returns>
        private GetTaxHistoryRequest CreateGetTaxHistoryRequest(GetTaxRequest getTaxRequest)
        {
            GetTaxHistoryRequest getTaxHistoryRequest = new GetTaxHistoryRequest();
            getTaxHistoryRequest.CompanyCode = getTaxRequest.CompanyCode;
            getTaxHistoryRequest.DocType = getTaxRequest.DocType;
            getTaxHistoryRequest.DocCode = getTaxRequest.DocCode;
            getTaxHistoryRequest.DetailLevel = DetailLevel.Tax;

            return getTaxHistoryRequest;
        }

        private void ValidateTaxResultForRateType(GetTaxResult taxResult, string rateType)
        {
            int taxLines = taxResult.TaxLines.Count;
            int taxDetails = 0;
            if (taxLines > 0)
            {
                foreach (TaxLine taxLine in taxResult.TaxLines)
                {
                    taxDetails = taxLine.TaxDetails.Count;
                    if (taxDetails > 0)
                    {
                        foreach (TaxDetail taxDetail in taxLine.TaxDetails)
                        {
                            Assert.AreEqual(rateType, taxDetail.RateType);
                        }
                    }
                }
            }
            int taxSummaries = taxResult.TaxSummary.Count;
            if (taxSummaries > 0)
            {
                foreach (TaxDetail taxSummary in taxResult.TaxSummary)
                {
                    Assert.AreEqual(rateType, taxSummary.RateType);
                }
            }
        }
		#endregion
	}
}