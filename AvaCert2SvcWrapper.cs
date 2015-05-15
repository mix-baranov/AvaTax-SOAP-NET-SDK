using System;
using System.Configuration;
using Avalara.AvaTax.Services.Proxies.AvaCert2SvcProxy;
using Profile=Avalara.AvaTax.Services.Proxies.AvaCert2SvcProxy.Profile;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Addressing;
using Microsoft.Web.Services3.Security.Tokens;

namespace Test.Avalara.AvaTax.Adapter
{
    public class AvaCert2SvcWrapper : AvaCert2SvcWse
    {
        public AvaCert2SvcWrapper()
        {
            Destination = GetEndpoint("AvaCert2/AvaCert2Svc.asmx");

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
    }
}

