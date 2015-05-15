using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Avalara.AvaTax.Adapter.AccountService;
using System.Configuration;
using Avalara.AvaTax.Adapter;

namespace Test.Avalara.AvaTax.Adapter
{
    [TestFixture]
    public class AccountSvcTest
    {
        private AccountSvc _accountSvc;

        public AccountSvcTest()
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
				_accountSvc = new AccountSvc();

				_accountSvc.Configuration.Url = ConfigurationSettings.AppSettings.Get("Url");				

				//fill these only if they haven't been loaded from Avalara.AvaTax.Adapter.dll.config
				//if (_addressSvc.Configuration.Security.Account == null || _addressSvc.Configuration.Security.Account.Length == 0)
				//{
				_accountSvc.Configuration.Security.Account = ConfigurationSettings.AppSettings.Get("account");
                _accountSvc.Configuration.Security.UserName = ConfigurationSettings.AppSettings.Get("UserName");
				//}
				//if (_addressSvc.Configuration.Security.Key == null || _addressSvc.Configuration.Security.Key.Length == 0)
				//{
				_accountSvc.Configuration.Security.License = ConfigurationSettings.AppSettings.Get("key");
                _accountSvc.Configuration.Security.Password = ConfigurationSettings.AppSettings.Get("Password");
				//}

				//_addressSvc.Configuration.Security.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("timeout"));
                _accountSvc.Profile.Client = "NUnit AccountSvcTest";
				_accountSvc.Profile.Name = "";

			}
			catch (Exception ex)
			{
                Assert.Fail("AccountSvc failed creation: " + ex.Message + " : " + ex.StackTrace);
			}
		}

        [Test]
        public void AccountSvcPingTest()
        {
            try
            {
                PingResult result = _accountSvc.Ping("");

                foreach (Message message in result.Messages)
                {
                    Console.WriteLine(message.Name + ": " + message.Summary);
                }

                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
            }
            catch (Exception ex)
            {
                Assert.Fail("AccountSvcPingTest failed: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void AccountSvcIsAuthorisedTest()
        {
            try
            {
                IsAuthorizedResult result = _accountSvc.IsAuthorized("*");

                foreach (Message message in result.Messages)
                {
                    Console.WriteLine(message.Name + ": " + message.Summary);
                }

                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
            }
            catch (Exception ex)
            {
                Assert.Fail("AccountSvcIsAuthorisedTest failed: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void CompanyRequestTest()
        {
            try
            {
                FetchRequest fetchrequest = new FetchRequest();
                fetchrequest.Fields = "*";
                fetchrequest.Filters = "'CompanyCode='TWO'";
                CompanyFetchResult result = _accountSvc.CompanyFetch(fetchrequest);

                foreach (Message message in result.Messages)
                {
                    Console.WriteLine(message.Name + ": " + message.Summary);
                }

                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
            }
            catch (Exception ex)
            {
                Assert.Fail("CompanyRequestTest failed: " + ex.Message + " : " + ex.StackTrace);
            }
        }

        [Test]
        public void CompanyNexusFetchTest()
        {
            try
            {
                FetchRequest fetchrequest = new FetchRequest();
                fetchrequest.Fields = "*";
                fetchrequest.Filters = "'CompanyID=196536";
                NexusFetchResult result = _accountSvc.NexusFetch(fetchrequest);

                foreach (Message message in result.Messages)
                {
                    Console.WriteLine(message.Name + ": " + message.Summary);
                }
                Assert.AreEqual(61, result.Nexuses.Count);
                Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
            }
            catch (Exception ex)
            {
                Assert.Fail("CompanyNexusFetchTest failed: " + ex.Message + " : " + ex.StackTrace);
            }
        }



    }
}
