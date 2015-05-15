using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Avalara.AvaTax.Adapter.Proxies.AddressSvcProxy;
using Avalara.AvaTax.Adapter.Proxies.TaxSvcProxy;

namespace Avalara.AvaTax.Adapter.Proxies
{
    
	/// <include file='BaseSvcProxy.Doc.xml' path='adapter/proxy/BaseAddress/*' />
	[XmlType(Namespace="http://avatax.avalara.com/services", TypeName="BaseAddress")]
	[XmlInclude(typeof(ProxyValidAddress))]
	[ComVisible(false)]
	public class ProxyBaseAddress 
	{
        
		/// <remarks/>
		public string AddressCode;
        
		/// <remarks/>
		public string Line1;
        
		/// <remarks/>
		public string Line2;
        
		/// <remarks/>
		public string Line3;
        
		/// <remarks/>
		public string City;
        
		/// <remarks/>
		public string Region;
        
		/// <remarks/>
		public string PostalCode;
        
		/// <remarks/>
		public string Country;

		/// <remarks/>Added for 5.0 Changed
		public int TaxRegionId;
		//Ticket 411:Changes done for exposing the ability to specify a Lat/Long as part of the address.
        public  string Latitude;
		public string Longitude;

	}
	
	/// <include file='BaseSvcProxy.Doc.xml' path='adapter/proxy/Message/*' />
	[XmlType(Namespace="http://avatax.avalara.com/services", TypeName="Message")]
	[ComVisible(false)]
	public class ProxyMessage 
	{
        
		/// <remarks/>
		public string Summary;
        
		/// <remarks/>
		public string Details;
        
		/// <remarks/>
		public string HelpLink;
        
		/// <remarks/>
		public string RefersTo;
        
		/// <remarks/>
		public ProxySeverityLevel Severity;
        
		/// <remarks/>
		public string Source;
        
		/// <remarks/>
		[XmlAttribute()]
		public string Name;
	}
    
	/// <include file='BaseSvcProxy.Doc.xml' path='adapter/proxy/SeverityLevel/*' />
	[XmlType(Namespace="http://avatax.avalara.com/services", TypeName="SeverityLevel")]
	[ComVisible(false)]
	public enum ProxySeverityLevel 
	{
        
		/// <remarks/>
		Success,
        
		/// <remarks/>
		Warning,
        
		/// <remarks/>
		Error,
        
		/// <remarks/>
		Exception,
	}

    /// <include file='BaseSvcProxy.Doc.xml' path='adapter/proxy/BaseRequest/*' />
    [XmlType(Namespace = "http://avatax.avalara.com/services", TypeName = "BaseRequest")]
    [ComVisible(false)]
    public class ProxyBaseRequest
    {
    }

	/// <include file='BaseSvcProxy.Doc.xml' path='adapter/proxy/BaseResult/*' />
	[XmlType(Namespace="http://avatax.avalara.com/services", TypeName="BaseResult")]
	[XmlInclude(typeof(ProxyCancelTaxResult))]
	[XmlInclude(typeof(ProxyCommitTaxResult))]
	[XmlInclude(typeof(ProxyPingResult))]
	[XmlInclude(typeof(ProxyReconcileTaxHistoryResult))]
	[XmlInclude(typeof(ProxyIsAuthorizedResult))]
	[XmlInclude(typeof(ProxyPostTaxResult))]
	[XmlInclude(typeof(ProxyGetTaxResult))]
	[XmlInclude(typeof(ProxyGetTaxHistoryResult))]
	[XmlInclude(typeof(ProxyValidateResult))]
	[ComVisible(false)]
	public class ProxyBaseResult 
	{
             
		/// <remarks/>
		public string TransactionId;
   
		/// <remarks/>
		public ProxySeverityLevel ResultCode;
        
		/// <remarks/>
		public ProxyMessage[] Messages;

		/// <remarks/>
		public string ReferenceCode;
	}
    
	/// <include file='BaseSvcProxy.Doc.xml' path='adapter/proxy/IsAuthorizedResult/*' />
	[XmlType(Namespace="http://avatax.avalara.com/services", TypeName="IsAuthorizedResult")]
	[ComVisible(false)]
	public class ProxyIsAuthorizedResult : ProxyBaseResult 
	{
        
		/// <remarks/>
		public string Operations;
        
		/// <remarks/>
		public System.DateTime Expires;
	}
    
	/// <include file='BaseSvcProxy.Doc.xml' path='adapter/proxy/PingResult/*' />
	[XmlType(Namespace="http://avatax.avalara.com/services", TypeName="PingResult")]
	[ComVisible(false)]
	public class ProxyPingResult : ProxyBaseResult 
	{
        
		/// <remarks/>
		public string Version;
	}
            
	/// <remarks/>
	[XmlType(Namespace="http://avatax.avalara.com/services", TypeName="Profile")]
	[XmlRoot(Namespace="http://avatax.avalara.com/services", IsNullable=false, ElementName="Profile")]
	[ComVisible(false)]
	public class ProxyProfile : System.Web.Services.Protocols.SoapHeader 
	{
        
		/// <remarks/>
		public string Name;
        
		/// <remarks/>
		public string Client;
        
		/// <remarks/>
		public string Adapter;
	        
		/// <remarks/>
		public string Machine;
	}

	/// <include file='BaseSvcProxy.Doc.xml' path='adapter/proxy/Password/*' />	
	/// <remarks/>
	[ComVisible(false)]
	public class ProxyPassword
	{
		/// <remarks/>
		[XmlAttributeAttribute()]
		public string Type;        

		/// <remarks/>
		[XmlTextAttribute()]
		public string Value;
	}


	/// <include file='BaseSvcProxy.Doc.xml' path='adapter/proxy/UsernameToken/*' />
	/// <remarks/>
	[ComVisible(false)]	
	public class ProxyUsernameToken : System.Web.Services.Protocols.SoapHeader 
	{
		/// <remarks/>
		public string Username;
        
		/// <remarks/>
		public ProxyPassword Password = new ProxyPassword();
	}

	/// <include file='BaseSvcProxy.Doc.xml' path='adapter/proxy/Security/*' />
	/// <remarks/>
	[XmlRoot("Security", Namespace="http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd", IsNullable=false)]
	[ComVisible(false)]
	public class ProxySecurity : System.Web.Services.Protocols.SoapHeader
	{
		/// <remarks/>
		public ProxyUsernameToken UsernameToken = new ProxyUsernameToken();
	}
}