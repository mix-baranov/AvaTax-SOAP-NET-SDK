#region Copyright
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System;
using System.Configuration;
using Avalara.AvaTax.Adapter;
using Avalara.AvaTax.Adapter.AddressService;
using NUnit.Framework;

namespace Test.Avalara.AvaTax.Adapter
{
	/// <summary>
	/// Summary description for AddressSvcTest.
	/// </summary>
	[TestFixture]
	public class AdapterAddressTest
	{
		private AddressSvc _addressSvc;

		public AdapterAddressTest()
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

				_addressSvc.Configuration.Url = ConfigurationSettings.AppSettings.Get("Url");				

				//fill these only if they haven't been loaded from Avalara.AvaTax.Adapter.dll.config
				//if (_addressSvc.Configuration.Security.Account == null || _addressSvc.Configuration.Security.Account.Length == 0)
				//{
				_addressSvc.Configuration.Security.Account = ConfigurationSettings.AppSettings.Get("account");
				//}
				//if (_addressSvc.Configuration.Security.Key == null || _addressSvc.Configuration.Security.Key.Length == 0)
				//{
				_addressSvc.Configuration.Security.License = ConfigurationSettings.AppSettings.Get("key");
				//}

				//_addressSvc.Configuration.Security.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("timeout"));
				_addressSvc.Profile.Client = "NUnit AddressSvcTest";
				_addressSvc.Profile.Name = "";

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
			}
			catch (Exception ex)
			{
				Assert.Fail("TearDown failed: " + ex.Message + " : " + ex.StackTrace);
			}
		}

		//[Test]
		public void QATest()
		{
			_addressSvc.Configuration.Url = "http://qa.avalara.com/avatax.services";

			PingResult pingResult = _addressSvc.Ping("");

			Assert.IsTrue(pingResult.Version.StartsWith("4.0."));
			Assert.AreEqual(SeverityLevel.Success, pingResult.ResultCode);

			Address address = new Address();
			address.Line1 = "900 Winslow Way";
			address.Line2 = "Ste 130";
			address.City = "Bainbridge Island";
			address.Region = "WA";
			address.PostalCode = "98110";

			ValidateRequest validateRequest = new ValidateRequest();
			validateRequest.Address = address;
			validateRequest.TextCase = TextCase.Upper;

			ValidateResult result = _addressSvc.Validate(validateRequest);
			Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
		}

		[Test]
		public void PingTest()
		{
			PingResult pingResult;

			try
			{
				pingResult = _addressSvc.Ping("");

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
				isAuthorizedResult = _addressSvc.IsAuthorized("Validate");

				Assert.AreEqual("Validate", isAuthorizedResult.Operations);
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
		public void ValidAddressRequestTest()
        {
            try
            {
                Address address = new Address();
                ValidateRequest validateRequest = new ValidateRequest();
                validateRequest.Address = address;
                ValidateResult result = _addressSvc.Validate(validateRequest);

                foreach (Message message in result.Messages)
                {
                    Console.WriteLine(message.Name + ": " + message.Summary);
                }

                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                Assert.AreEqual("InsufficientAddressError", result.Messages[0].Name);
            }
            catch (Exception ex)
            {
                Assert.Fail("ValidAddressRequestTest failed: " + ex.Message + " : " + ex.StackTrace);
            }
		}

        /// <summary>
        /// Address Request validation test
        /// </summary>
        [Test]
        public void ValidAddressRequestTest1()
        {
            try
            {
                Address address = new Address();
                address.Line2 = "900 Winslow Way";
                address.PostalCode = "98110";

                ValidateRequest validateRequest = new ValidateRequest();
                validateRequest.Address = address;

                ValidateResult result = _addressSvc.Validate(validateRequest);

                foreach (Message message in result.Messages)
                {
                    Console.WriteLine(message.Name + ": " + message.Summary);
                }

                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
                Assert.AreEqual(1, result.Addresses.Count);

                address.Line1 = "900 Winslow Way";
                address.PostalCode = "";

                validateRequest.Address = address;

                result = _addressSvc.Validate(validateRequest);
                foreach (Message message in result.Messages)
                {
                    Console.WriteLine(message.Name + ": " + message.Summary);
                }

                Assert.AreEqual(SeverityLevel.Error, result.ResultCode);
                Assert.AreEqual("InsufficientAddressError", result.Messages[0].Name);
            }
            catch (Exception ex)
            {
                Assert.Fail("ValidAddressRequestTest1 failed: " + ex.Message + " : " + ex.StackTrace);
            }
        }

		[Test]
		public void ValidateTest()
		{
           
			Address address;
			ValidateResult result;
			ValidAddress validAddress;

            try
            {
                address = new Address();
                address.Line1 = "900 Winslow Way";
                address.Line2 = "Ste 130";
                address.City = "Bainbridge Island";
                address.Region = "WA";
                address.PostalCode = "98110";

                ValidateRequest validateRequest = new ValidateRequest();
                validateRequest.Address = address;
                validateRequest.TextCase = TextCase.Upper;
                //Ticket #21203 :For Dev Test account (1987654323)
                // Profile name "Jaas" will force it to Jaas (PostLocate)
                _addressSvc.Profile.Name = "Jaas";
                result = _addressSvc.Validate(validateRequest);
                _addressSvc.Profile.Name = "";
                foreach (Message message in result.Messages)
                {
                    Console.WriteLine(message.Name + ": " + message.Summary);
                }

                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
                Assert.AreEqual(1, result.Addresses.Count);
                if (result.Addresses.Count != 1)
                {
                    Assert.Fail("Unexpected number of addresses returned.  Expected one address.");
                }
                else
                {
                    validAddress = result.Addresses[0];

                    Assert.AreEqual(address.Line1.ToUpper() + " E " + address.Line2.ToUpper(), validAddress.Line1);
                    Assert.AreEqual("", validAddress.Line2);
                    Assert.AreEqual(address.City.ToUpper(), validAddress.City);
                    Assert.AreEqual(address.Region.ToUpper(), validAddress.Region);
                    Assert.AreEqual(address.PostalCode + "-2450", validAddress.PostalCode);
                    Assert.AreEqual("H", validAddress.AddressType);
                    Assert.AreEqual("C051", validAddress.CarrierRoute);
                    Assert.AreEqual("5303503736", validAddress.FipsCode);

                    Assert.AreEqual("KITSAP", validAddress.County);
                    Assert.AreEqual(address.City.ToUpper() + " " + address.Region.ToUpper() + " " + address.PostalCode + "-2450", validAddress.Line4);
                    Assert.AreEqual("981102450307", validAddress.PostNet);
                    

                    //Added 4.13 changes for the Lat Long
                    Assert.AreEqual("", validAddress.Latitude);
                    Assert.AreEqual("", validAddress.Longitude);
                    //Ticket 21203: For CSP Test account
                    _addressSvc.Configuration.Security.Account = "1987654309";
                    _addressSvc.Configuration.Security.License = "89DB000812AB76CB";
                    result = _addressSvc.Validate(validateRequest);

                    foreach (Message message in result.Messages)
                    {
                        Console.WriteLine(message.Name + ": " + message.Summary);
                    }

                    Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
                    Assert.AreEqual(1, result.Addresses.Count);
                    if (result.Addresses.Count != 1)
                    {
                        Assert.Fail("Unexpected number of addresses returned.  Expected one address.");
                    }
                    else
                    {
                        validAddress = result.Addresses[0];

                        Assert.AreEqual(address.Line1.ToUpper() + " E " + address.Line2.ToUpper(), validAddress.Line1);
                        Assert.AreEqual("", validAddress.Line2);
                        Assert.AreEqual(address.City.ToUpper(), validAddress.City);
                        Assert.AreEqual(address.Region.ToUpper(), validAddress.Region);
                        Assert.AreEqual(address.PostalCode + "-2450", validAddress.PostalCode);
                        Assert.AreEqual("H", validAddress.AddressType);
                        Assert.AreEqual("C051", validAddress.CarrierRoute);
                        Assert.AreEqual("5303500000", validAddress.FipsCode);

                        Assert.AreEqual("KITSAP", validAddress.County);
                        Assert.AreEqual(address.City.ToUpper() + " " + address.Region.ToUpper() + " " + address.PostalCode + "-2450", validAddress.Line4);
                        Assert.AreEqual("981102450307", validAddress.PostNet);


                        //Added 4.13 changes for the Lat Long
                        Assert.AreEqual("", validAddress.Latitude);
                        Assert.AreEqual("", validAddress.Longitude);
                    }
                }
            }
            
            finally
            {
                _addressSvc.Configuration.Security.Account = ConfigurationSettings.AppSettings.Get("account");
                _addressSvc.Configuration.Security.License = ConfigurationSettings.AppSettings.Get("key");
                
            }
		}

        /// <summary>
        /// ValidateRequest.Taxability = true should return Addresses[0].TaxRegionId value
        /// </summary>
        [Test]
        public void ValidateTaxRegionIdTest()
        {
            Address address = new Address();
            address.Line1 = "900 Winslow Way";
            address.Line2 = "Ste 130";
            address.City = "Bainbridge Island";
            address.Region = "WA";
            address.PostalCode = "98110";

            ValidateRequest validateRequest = new ValidateRequest();
            validateRequest.Address = address;
            validateRequest.TextCase = TextCase.Upper;

            ValidateResult result = _addressSvc.Validate(validateRequest);

            foreach (Message message in result.Messages)
            {
                Console.WriteLine(message.Name + ": " + message.Summary);
            }

            Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
            Assert.AreEqual(1, result.Addresses.Count);
            Assert.AreEqual(false, result.Taxable);
            Assert.IsTrue(result.Addresses[0].TaxRegionId == 0, "TaxRegionId should be 0");


            validateRequest.Taxability = true;
            result = _addressSvc.Validate(validateRequest);

            foreach (Message message in result.Messages)
            {
                Console.WriteLine(message.Name + ": " + message.Summary);
            }

            Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
            Assert.AreEqual(1, result.Addresses.Count);
            Assert.AreEqual(true, result.Taxable);
            Assert.IsTrue(result.Addresses[0].TaxRegionId != 0, "TaxRegionId should not be 0");
            Console.WriteLine("Address.TaxRegionId: " + result.Addresses[0].TaxRegionId);
        }

        [Test]
        public void TaxabilityTest()
        {
            Address address;
            ValidateResult result;

            address = new Address();
            address.Line1 = "900 Winslow Way";
            address.Line2 = "Ste 130";
            address.City = "Bainbridge Island";
            address.Region = "WA";
            address.PostalCode = "98110";

            ValidateRequest validateRequest = new ValidateRequest();
            validateRequest.Address = address;
            validateRequest.TextCase = TextCase.Upper;
            validateRequest.Taxability = true;

            result = _addressSvc.Validate(validateRequest);

            foreach (Message message in result.Messages)
            {
                Console.WriteLine(message.Name + ": " + message.Summary);
            }

            Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
            Assert.AreEqual(1, result.Addresses.Count);
            Assert.AreEqual(true, result.Taxable);
        }

        [Test]
		public void ValidateLatLongTest()
		{
			Address address;
			ValidateResult result;
			ValidAddress validAddress;
            address = new Address();
		    address.Line1 = "900 Winslow Way";
		    address.Line2 = "Ste 130";
		    address.City = "Bainbridge Island";
		    address.Region = "WA";
		    address.PostalCode = "98110";

		    ValidateRequest validateRequest = new ValidateRequest();
		    validateRequest.Address = address;
		    validateRequest.TextCase = TextCase.Upper;
		    //added for 4.13 changes
		    validateRequest.Coordinates = true;
            // Profile name "Jaas" will force it to Jaas (PostLocate)
            _addressSvc.Profile.Name = "Jaas";
		    result = _addressSvc.Validate(validateRequest);
            _addressSvc.Profile.Name = "";

		    foreach (Message message in result.Messages)
		    {
			    Console.WriteLine(message.Name + ": " + message.Summary);
		    }

		    Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
		    Assert.AreEqual(1, result.Addresses.Count);
		    if (result.Addresses.Count != 1)
		    {
			    Assert.Fail("Unexpected number of addresses returned.  Expected one address.");
		    }
		    else
		    {
			    validAddress = result.Addresses[0];
			    Assert.AreEqual(address.Line1.ToUpper() + " E " + address.Line2.ToUpper(), validAddress.Line1);
			    Assert.AreEqual("", validAddress.Line2);
			    Assert.AreEqual(address.City.ToUpper(), validAddress.City);
			    Assert.AreEqual(address.Region.ToUpper(), validAddress.Region);
			    Assert.AreEqual(address.PostalCode + "-2450", validAddress.PostalCode);
			    Assert.AreEqual("H", validAddress.AddressType);
			    Assert.AreEqual("C051", validAddress.CarrierRoute);
                //Ticket 21203: Modified Fips Code value for jaas enabled account.
			    //Assert.AreEqual("5303500000", validAddress.FipsCode);
                Assert.AreEqual("5303503736", validAddress.FipsCode);
			    Assert.AreEqual("KITSAP", validAddress.County);
			    Assert.AreEqual(address.City.ToUpper() + " " + address.Region.ToUpper() + " " + address.PostalCode + "-2450", validAddress.Line4);
			    Assert.AreEqual("981102450307", validAddress.PostNet);
               
			    // Added 4.13 changes for the Lat Long
			    // Update to check for ZIP+4 precision				
			    // Zip+4 precision coordinates 
			    if (validAddress.Latitude.Length > 7)
			    {
				    Console.WriteLine("ZIP+4 precision coordinates received");
				    Assert.AreEqual("47.624972", validAddress.Latitude);
				    Assert.AreEqual("-122.510359", validAddress.Longitude);
			    }
			    else
			    {
				    Console.WriteLine("ZIP5 precision coordinates received");
				    // (j.wadlow) updated due to Apr 2011 Melissa Data update
                    Assert.IsTrue(validAddress.Latitude.StartsWith("47.6"), "Expected Latitude to start with '47.64'");
                    Assert.IsTrue(validAddress.Longitude.StartsWith("-122.5"), "Expected Longitude to start with '-122.53'");
                }
				
			}
			
		}


        [Test]
        //Ticket 411:Testcase for Changes done for exposing the ability to specify a Lat/Long as part of the address.
        public void ValidateLatLongTest1()
        {
            Address address;
            ValidateResult result;
            ValidAddress validAddress;
            address = new Address();
            address.Line1 = "900 Winslow Way";
            address.Line2 = "Ste 130";
            address.City = "Bainbridge Island";
            address.Region = "WA";
            address.PostalCode = "98110";
            address.Longitude = "-122.510359";
            address.Latitude = "47.624972";

            ValidateRequest validateRequest = new ValidateRequest();
            validateRequest.Address = address;
            validateRequest.TextCase = TextCase.Upper;
            //added for 4.13 changes
            validateRequest.Coordinates = true;
            // Profile name "Jaas" will force it to Jaas (PostLocate)
            _addressSvc.Profile.Name = "Jaas";
            result = _addressSvc.Validate(validateRequest);
            _addressSvc.Profile.Name = "";

            foreach (Message message in result.Messages)
            {
                Console.WriteLine(message.Name + ": " + message.Summary);
            }

            Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
            Assert.AreEqual(1, result.Addresses.Count);
            if (result.Addresses.Count != 1)
            {
                Assert.Fail("Unexpected number of addresses returned.  Expected one address.");
            }
            else
            {
                validAddress = result.Addresses[0];
                Assert.AreEqual(address.Line1.ToUpper() + " E " + address.Line2.ToUpper(), validAddress.Line1);
                Assert.AreEqual("", validAddress.Line2);
                Assert.AreEqual(address.City.ToUpper(), validAddress.City);
                Assert.AreEqual(address.Region.ToUpper(), validAddress.Region);
                Assert.AreEqual(address.PostalCode + "-2450", validAddress.PostalCode);
                Assert.AreEqual("H", validAddress.AddressType);
                Assert.AreEqual("C051", validAddress.CarrierRoute);
                //Ticket 21203: Modified Fips Code value for jaas enabled account.
                //Assert.AreEqual("5303500000", validAddress.FipsCode);
                Assert.AreEqual("5303503736", validAddress.FipsCode);
                Assert.AreEqual("KITSAP", validAddress.County);
                Assert.AreEqual(address.City.ToUpper() + " " + address.Region.ToUpper() + " " + address.PostalCode + "-2450", validAddress.Line4);
                Assert.AreEqual("981102450307", validAddress.PostNet);

                // Added 4.13 changes for the Lat Long
                // Update to check for ZIP+4 precision				
                // Zip+4 precision coordinates 
                if (validAddress.Latitude.Length > 7)
                {
                    Console.WriteLine("ZIP+4 precision coordinates received");
                    Assert.AreEqual(address.Latitude, validAddress.Latitude);
                    Assert.AreEqual(address.Longitude, validAddress.Longitude);
                }
                else
                {
                    Console.WriteLine("ZIP5 precision coordinates received");
                   
                    Assert.IsTrue(validAddress.Latitude.StartsWith(address.Latitude.Substring(0,4)), "Expected Latitude to start with '47.64'");
                    Assert.IsTrue(validAddress.Longitude.StartsWith(address.Longitude.Substring(0,6)), "Expected Longitude to start with '-122.53'");
                }

            }

        }

#if DEBUG && false
		[Test]
		[ExpectedException(typeof(AvaTaxException))]
		public void HandleSoapExceptionTest()
		{
			_addressSvc.Ping("Exception");

			Assert.Fail("Expected AvaTaxException");
		}

		[Test]
		[ExpectedException(typeof(AvaTaxException))]
		public void HandleSoapExceptionTest2()
		{
			_addressSvc.Ping("AvaTaxException");

			Assert.Fail("Expected AvaTaxException");
		}
#endif

		[Test]
		[ExpectedException(typeof(AvaTaxException))]
		public void HandleSoapHeaderExceptionTest()
		{
			_addressSvc.Configuration.Security.Account = "1";
			_addressSvc.Configuration.Security.License = "1";

			_addressSvc.Ping("");

			Assert.Fail("Expected AvaTaxException");
		}

        /// <summary>
        /// Address batch operations test
        /// </summary>
        //[Test]
        //[Ignore("Enable once service side Batch operations are done")]
        //public void AddressBatchOperationsTest()
        //{
        //    SubmitAddressBatchRequest submitAddressBatchRequest = new SubmitAddressBatchRequest();
        //    submitAddressBatchRequest.Requests.Add(CreateAddressRequest());
        //    submitAddressBatchRequest.Requests.Add(CreateAddressRequest());
        //    SubmitAddressBatchResult submitAddressBatchResult = _addressSvc.SubmitBatch(submitAddressBatchRequest);
        //    Assert.AreEqual(SeverityLevel.Success, submitAddressBatchResult.ResultCode);

        //    GetAddressBatchRequest getAddressBatchRequest = new GetAddressBatchRequest();
        //    getAddressBatchRequest.BatchId = submitAddressBatchResult.BatchId;
        //    GetAddressBatchResult getAddressBatchResult = _addressSvc.GetBatch(getAddressBatchRequest);
        //    Assert.AreEqual(SeverityLevel.Success, getAddressBatchResult.ResultCode);
        //    Assert.AreEqual("Completed", getAddressBatchResult.BatchStatus);
        //    Assert.AreEqual(submitAddressBatchRequest.Requests.Count, getAddressBatchResult.RecordCount);
        //    Assert.AreEqual(submitAddressBatchRequest.Requests.Count, getAddressBatchResult.Results.Count);

        //    CancelAddressBatchRequest cancelAddressBatchRequest = new CancelAddressBatchRequest();
        //    cancelAddressBatchRequest.BatchId = submitAddressBatchResult.BatchId;
        //    CancelAddressBatchResult cancelAddressBatchResult = _addressSvc.CancelBatch(cancelAddressBatchRequest);
        //    Assert.AreEqual(SeverityLevel.Success, cancelAddressBatchResult.ResultCode);
        //}

        /// <summary>
        /// SubmitAddressBatchRequest validation test
        /// </summary>
        //[Test]
        //public void ValidSubmitAddressBatchRequestTest()
        //{
        //    try
        //    {
        //        SubmitAddressBatchRequest submitAddressBatchRequest = new SubmitAddressBatchRequest();
        //        _addressSvc.SubmitBatch(submitAddressBatchRequest);
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.AreEqual("Requests cannot be empty.", ex.Message);
        //    }
        //}

        /// <summary>
        /// Get address request.
        /// </summary>
        /// <returns>
        /// </returns>
        private ValidateRequest CreateAddressRequest()
        {
            Address address = new Address();
            address.Line1 = "900 Winslow Way";
            address.Line2 = "Ste 130";
            address.City = "Bainbridge Island";
            address.Region = "WA";
            address.PostalCode = "98110";

            ValidateRequest request = new ValidateRequest();
            request.Address = address;

            return request;
        }
	}
}