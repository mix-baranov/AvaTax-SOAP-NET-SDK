using System;
using System.Configuration;
using Avalara.AvaTax.Services.Proxies.AccountSvcProxy;
using AccountProxy = Avalara.AvaTax.Services.Proxies.AccountSvcProxy.Account;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Addressing;
using Microsoft.Web.Services3.Security.Tokens;
using NUnit.Framework;

namespace Test.Avalara.AvaTax.Adapter
{
    public class AccountSvcWrapper : AccountSvcWse
    {
        public AccountSvcWrapper()
        {
            Destination = GetEndpoint("Account/AccountSvc.asmx");

            SetDefaultSecurity();
            
            Profile profile = new Profile();
            profile.Client = "NUnit";
            profile.Adapter = "";
            profile.Name = "";
            profile.Machine = Environment.MachineName;

            ProfileValue = profile;
        }

        public EndpointReference GetEndpoint(string path)
        {
            string url = Url;
            string viaurl = url;
            if (string.IsNullOrEmpty(viaurl))
            {
                viaurl = url;
            }
            if (!url.EndsWith("/"))
            {
                url += "/";
            }
            if (!viaurl.EndsWith("/"))
            {
                viaurl += "/";
            }
            EndpointReference endpoint = new EndpointReference(new Uri(url + path), new Uri(viaurl + path));
            return endpoint;
        }

        public string Username
        {
            get
            {
                string username = ConfigurationSettings.AppSettings.Get("username");
                if (string.IsNullOrEmpty(username))
                {
                    username = "devadmin@avalara.com";
                }
                return username;
            }
        }

        public string Password
        {
            get
            {
                string password = ConfigurationSettings.AppSettings.Get("password");
                if (string.IsNullOrEmpty(password))
                {
                    password = "kennwort";
                }
                return password;
            }
        }

        public new string Url
        {
            get
            {
                string url = ConfigurationSettings.AppSettings.Get("url");
                if (string.IsNullOrEmpty(url))
                {
                    url = "http://localhost/AvaTax.Services";
                }
                return url;
            }
        }

        public void SetDefaultSecurity()
        {
            // Setup WS-Security authentication
            UsernameToken userToken = new UsernameToken(Username, Password, PasswordOption.SendPlainText);
            SoapContext requestContext = RequestSoapContext;
            requestContext.Security.Tokens.Add(userToken);
            //requestContext.Security.Timestamp.TtlInSeconds = SvcConfig.TtlInSeconds;
        }

        public void SetRegistrarSecurity()
        {
            UsernameToken userToken = new UsernameToken("devregistrar@avalara.com", "kennwort", PasswordOption.SendPlainText);
            SoapContext requestContext = RequestSoapContext;
            requestContext.Security.Tokens.Clear();
            requestContext.Security.Tokens.Add(userToken);
        }

        public void SetTechnicalSupportUserSecurity()
        {
            UsernameToken userToken = new UsernameToken("DevTechnicalSupportAdmin@Avalara.com%1987654323", "kennwort", PasswordOption.SendPlainText);
            SoapContext requestContext = RequestSoapContext;
            requestContext.Security.Tokens.Clear();
            requestContext.Security.Tokens.Add(userToken);
        }

        public Company GetDefaultCompany()
        {
            FetchRequest fetchRequest = new FetchRequest();
            fetchRequest.Filters = "IsDefault=true";
            fetchRequest.Fields = "*";
            CompanyFetchResult fetchResult = CompanyFetch(fetchRequest);
            Assert.AreEqual(SeverityLevel.Success, fetchResult.ResultCode, "Default company");
            Assert.AreEqual(1, fetchResult.Companies.Length, "Expected 1 Company");
            return fetchResult.Companies[0];
        }

        /// <summary>
        /// Deletes the specified company
        /// </summary>
        /// <param name="companyCode"></param>
        public void DeleteCompany(string companyCode)
        {
            SetTechnicalSupportUserSecurity();
            AuditMessage auditMessage = new AuditMessage();
            auditMessage.Message = "Deleting company for NUnit test";
            AuditMessageValue = auditMessage;

            DeleteRequest request = new DeleteRequest();
            request.Filters = "CompanyCode='" + companyCode + "'";
            request.MaxCount = 1;
            DeleteResult result = CompanyDelete(request);
            Assert.AreEqual(SeverityLevel.Success, result.ResultCode);
            //Assert.AreEqual(1, result.Count, "CompanyDelete Count");

            SetDefaultSecurity();
        }
    }
}