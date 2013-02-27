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
using Avalara.AvaTax.Adapter.Proxies.AddressSvcProxy;

namespace Avalara.AvaTax.Adapter.AddressService
{
	/// <include file='AddressSvc.Doc.xml' path='adapter/AddressSvc/class/*' />
	[Guid("671C83F8-CAFD-4d9e-A82D-15D5C11CA59C")]
	[ComVisible(true)]
	public class AddressSvc : BaseSvc, IDisposable
	{
		private const string SERVICE_NAME = "Address/AddressSvc.asmx";
		private AvaLogger _avaLog;

		/// <include file='AddressSvc.Doc.xml' path='adapter/common/members[@name="Constructor"]/*' />
		public AddressSvc()
		{
			try 
			{
				_avaLog = AvaLogger.GetLogger();
				_avaLog.Debug(string.Format("Instantiating AddressSvc: {0}", base.UniqueId));
				base.ServiceName = SERVICE_NAME;
				ProxyAddressSvc proxy = new ProxyAddressSvc();
				proxy.ProfileValue = new ProxyProfile();
				proxy.Security = new ProxySecurity();
				base.SvcProxy = proxy;

			} catch (Exception ex)
			{
				ExceptionManager.HandleException(ex);
			}
		}
		
		/// <include file='AddressSvc.Doc.xml' path='adapter/common/members[@name="Ping"]/*' />
		[DispId(30)]
		public PingResult Ping(string message)
		{
			try 
			{
				_avaLog.Debug("AddressSvc.Ping");
				ProxyPingResult svcResult = (ProxyPingResult) base.InvokeService(typeof(ProxyAddressSvc), MethodBase.GetCurrentMethod().Name, new object[] {message});

				_avaLog.Debug("Copying result from proxy object");
				PingResult localResult = new PingResult();
				localResult.CopyFrom( svcResult );					//load local object with service results

				return localResult;
			} catch (Exception ex)
			{
				return PingResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/common/members[@name="IsAuthorized"]/*' />
		[DispId(31)]
		public IsAuthorizedResult IsAuthorized(string operations)
		{
			try
			{
				_avaLog.Debug("AddressSvc.IsAuthorized");

				ProxyIsAuthorizedResult svcResult = (ProxyIsAuthorizedResult) base.InvokeService(typeof(ProxyAddressSvc), MethodBase.GetCurrentMethod().Name, new object[] {operations});

				_avaLog.Debug("Copying result from proxy object");
				IsAuthorizedResult localResult = new IsAuthorizedResult();	//local copy to hold service results
				localResult.CopyFrom( svcResult );	//load local object with service results

				return localResult;
			} catch (Exception ex)
			{
				return IsAuthorizedResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/AddressSvc/members[@name="Validate"]/*' />
		[DispId(32)]
		public ValidateResult Validate(ValidateRequest validateRequest)
		{
			try
			{
				_avaLog.Debug("AddressSvc.Validate");

				Utilities.VerifyRequestObject(validateRequest);

				_avaLog.Debug("Copying address into proxy object");
				ProxyValidateRequest proxyRequest = new ProxyValidateRequest();
				validateRequest.CopyTo(proxyRequest);
				//Record time take for address validation
				Perf monitor = new Perf();
				monitor.Start();

				ProxyValidateResult svcResult = (ProxyValidateResult) base.InvokeService(typeof(ProxyAddressSvc), MethodBase.GetCurrentMethod().Name, new object[] {proxyRequest});

				monitor.Stop(this,ref svcResult);
				_avaLog.Debug("Copying address from proxy object");
				ValidateResult localResult = new ValidateResult();
				localResult.CopyFrom(svcResult);

				return localResult;
			} 
			catch (Exception ex)
			{
				return ValidateResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/common/members[@name="Finalize"]/*' />
		~AddressSvc()
		{
			_avaLog.Debug(string.Format("Finalizing AddressSvc: {0}", base.UniqueId));
		}
		#region Internal Members

		/// <summary>
		/// Convenient wrapper property that casts the base WebServicesClientProtocol into our strongly typed proxy class.
		/// </summary>
		internal new ProxyAddressSvc SvcProxy
		{
			get
			{
				return (ProxyAddressSvc)base.SvcProxy;
			}
		}
		#endregion

	}


	/// <include file='AddressSvc.Doc.xml' path='adapter/Address/class/*' />
	[Guid("02931530-9ACF-4626-959A-B888DD391BB7")]
	[ComVisible(true)]
	public class Address
	{
		string _addressCode;
		string _line1;
		string _line2;
		string _line3;
		string _city;
		string _region;
		string _postalCode;
		string _country;
		/// <summary>
		/// Added for 5.0 changes
		/// </summary>
		int _taxRegionId;
        //Ticket 411:Changes done for exposing the ability to specify a Lat/Long as part of the address.
        string _latitude;
        string _longitude;



		/// <include file='AddressSvc.Doc.xml' path='adapter/common/members[@name="Constructor"]/*' />
		public Address()
		{
		}
 
		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="Equals"]/*' />
		[DispId(30)]
		public override bool Equals(object obj)
		{
			//are we referring to the same object?
			bool equals = base.Equals(obj);

			if (!equals && (obj.GetType() == typeof(Address)))
			{
				//do objects have the same content?
				Address compare = (Address)obj;
				equals = (
					(String.Compare(this.Line1, compare.Line1, false) == 0) &&
					(String.Compare(this.Line2, compare.Line2, false) == 0) &&
					(String.Compare(this.Line3, compare.Line3, false) == 0) &&
					(String.Compare(this.City, compare.City, false) == 0) &&
					(String.Compare(this.Region, compare.Region, false) == 0) &&
					(String.Compare(this.PostalCode, compare.PostalCode, false) == 0) && 
					(String.Compare(this.Country, compare.Country, false) == 0) &&
					(String.Compare(this.TaxRegionId.ToString(), compare.TaxRegionId.ToString(), false) == 0) &&
                    (String.Compare(this.Latitude, compare.Latitude, false) == 0) &&
                    (String.Compare(this.Longitude, compare.Longitude, false) == 0) 
					);
			}

			return equals;
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="GetHashCode"]/*' />
		[DispId(31)]
		public override int GetHashCode()
		{
			return base.GetHashCode ();
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="Clone"]/*' />
		[DispId(32)]
		public Address Clone()
		{
			Address address = new Address();

			address.Line1 = this.Line1;
			address.Line2 = this.Line2;
			address.Line3 = this.Line3;
			address.City = this.City;
			address.Region = this.Region;
			address.PostalCode = this.PostalCode;
			address.Country = this.Country;
			address.AddressCode = address.GetHashCode().ToString();
			address.TaxRegionId = this.TaxRegionId;
            address.Latitude = this.Latitude;
            address.Longitude = this.Longitude;

			return address;
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="AddressCode"]/*' />
		//no dispatch id because internal
		internal string AddressCode
		{
			get
			{
				return _addressCode;
			}
			set
			{
				_addressCode = value;
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="Line1"]/*' />
		[DispId(33)]
		public string Line1
		{
			get
			{
				return _line1;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_line1 = null;
				}
				else
				{
					_line1 = value.Trim();
				}
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="Line2"]/*' />
		[DispId(34)]
		public string Line2
		{
			get
			{
				return _line2;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_line2 = null;
				}
				else
				{
					_line2 = value.Trim();
				}
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="Line3"]/*' />
		[DispId(35)]
		public string Line3
		{
			get
			{
				return _line3;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_line3 = null;
				}
				else
				{
					_line3 = value.Trim();
				}
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="City"]/*' />
		[DispId(36)]
		public string City
		{
			get
			{
				return _city;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_city = null;
				}
				else
				{
					_city = value.Trim();
				}
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="Region"]/*' />
		[DispId(37)]
		public string Region
		{
			get
			{
				return _region;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_region = null;
				}
				else
				{
					_region = value.Trim();
				}
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="PostalCode"]/*' />
		[DispId(38)]
		public string PostalCode
		{
			get
			{
				return _postalCode;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_postalCode = null;
				}
				else
				{
					_postalCode = value.Trim();
				}
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="Country"]/*' />
		[DispId(39)]
		public string Country
		{
			get
			{
				return _country;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_country = null;
				}
				else
				{
					_country = value.Trim();
				}
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="TaxRegionId"]/*' />
		[DispId(40)]
		public int TaxRegionId
		{
			get
			{
				return _taxRegionId;
			}
			set
			{
                _taxRegionId = value;				
			}
		}
        [DispId(41)]
        public string Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    _latitude = null;
                }
                else
                {
                    _latitude = value.Trim();
                }
            }
        }
         [DispId(42)]
        public string Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    _longitude = null;
                }
                else
                {
                    _longitude = value.Trim();
                }
            }
        }
        

		#region Internal Members

		/// <summary>
		/// Load an empty local Line object from the data provided by the web service.
		/// </summary>
		/// <param name="SvcAddress">The Address object provided by the web service.</param>
		internal void CopyFrom(ProxyBaseAddress SvcAddress)
		{
			_addressCode = SvcAddress.AddressCode;
			_city        = SvcAddress.City;
			_country     = SvcAddress.Country;
			_line1       = SvcAddress.Line1;
			_line2       = SvcAddress.Line2;
			_line3       = SvcAddress.Line3;
			_postalCode  = SvcAddress.PostalCode;
			_region      = SvcAddress.Region;
			_taxRegionId = SvcAddress.TaxRegionId;
            _latitude    = SvcAddress.Latitude;
            _longitude   = SvcAddress.Longitude;
            
		}

		/// <summary>
		/// Loads a local Address object into a web service copy of the same object.
		/// </summary>
		/// <param name="SvcAddress">The Address object to be copied to.</param>
		internal void CopyTo(ProxyBaseAddress SvcAddress)
		{
			SvcAddress.AddressCode = AddressCode;
			SvcAddress.City        = City;
			SvcAddress.Country     = Country;
			SvcAddress.Line1       = Line1;
			SvcAddress.Line2       = Line2;
			SvcAddress.Line3       = Line3;
			SvcAddress.PostalCode  = PostalCode;
			SvcAddress.Region      = Region;
			SvcAddress.TaxRegionId = TaxRegionId;
            SvcAddress.Latitude    = Latitude;
            SvcAddress.Longitude   = Longitude;
		}

		#endregion
	}

	/// <include file='AddressSvc.Doc.xml' path='adapter/Addresses/class/*' />
	[Guid("7C519D0B-2C65-40c0-8B96-F8EC0F074647")]
	[ComVisible(true)]
	public class Addresses : BaseArrayList
	{
		/// <include file='AddressSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal Addresses() {}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Addresses/members[@name="Add"]/*' />
		[DispId(30)]
		public void Add(Address address)
		{
			try 
			{
				base.Add(address);
			}
			catch (ArgumentNullException)
			{
				throw new ArgumentNullException("address", "Cannot add a null address to the collection.");		
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Addresses/members[@name="this"]/*' />
		[DispId(0)]
		public new Address this[int index]
		{
			get
			{
				//we hide the base member so that we can strongly type our returned object
				return (Address) base[ index ];
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Addresses/members[@name="Clear"]/*' />
		[DispId(31)]
		public new void Clear()
		{
			base.Clear();
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="Clear"]/*' />
		//no dispatch id because internal
		internal Address Find(string addressCode)
		{
			foreach(Address address in base.List)
			{
				if (String.Compare(address.AddressCode, addressCode, false) == 0)
				{
					return address;
				}
			}
			return null;
		}
	}

	/// <include file='AddressSvc.Doc.xml' path='adapter/ValidateResult/class/*' />
	[Guid("72BF8A20-A7D3-4275-BF09-5075FCEC95A2")]
	[ComVisible(true)]
	public class ValidateResult : BaseResult
	{
		ValidAddresses _addresses = new ValidAddresses();
		private bool _taxable;

		/// <include file='AddressSvc.Doc.xml' path='adapter/common/members[@name="InternalConstuctor"]/*' />
		internal ValidateResult()
		{
			_taxable = false;
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/ValidateResult/members[@name="Addresses"]/*' />
		[DispId(30)]
		public ValidAddresses Addresses
		{
			get
			{
				return _addresses;
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/ValidateResult/members[@name="Taxable"]/*' />
		[DispId(31)]
		public bool Taxable
		{
			get
			{
				return _taxable;
			}
		}

		#region Internal Members

		/// <summary>
		/// Load an empty local ValidateResult object from the data provided by the web service.
		/// </summary>
		/// <param name="SvcResult">The ValidateResult object provided by the web service.</param>
		internal void CopyFrom( ProxyValidateResult SvcResult )
		{
			base.CopyFrom(SvcResult);

			//iterate through addresses returned by the web service and move them into
			//    a local address object and local arraylist
			if (SvcResult.ValidAddresses != null)
			{
				for (int Index = 0; Index < SvcResult.ValidAddresses.Length; Index++)
				{
					ProxyValidAddress SvcAddress = SvcResult.ValidAddresses[ Index ];
					ValidAddress localAddress = new ValidAddress();

					localAddress.CopyFrom( SvcAddress );
					//((ValidAddresses)Addresses).Add( localAddress );
					_addresses.Add( localAddress );
					_taxable = SvcResult.Taxable;
				}
			}
		}

		/// <summary>
		/// Creates a new ValidateResult based on a <see cref="BaseResult"/>.
		/// </summary>
		/// <param name="baseResult"></param>
		/// <returns></returns>
		internal static ValidateResult CastFromBaseResult(BaseResult baseResult)
		{
			ValidateResult result = new ValidateResult();
			result.CopyFrom(baseResult);
			return result;
		}

		#endregion
	}

	/// <include file='AddressSvc.Doc.xml' path='adapter/ValidAddresses/class/*' />
	[Guid("492CE337-6695-4bb5-9A55-11EFC384378E")]
	[ComVisible(true)]
	public class ValidAddresses : ReadOnlyArrayList
	{
		/// <include file='AddressSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal ValidAddresses() {}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Collection/members[@name="this"]/*' />
		[DispId(0)]
		public new ValidAddress this[int index]
		{
			get
			{
				//we hide the base member so that we can strongly type our returned object
				return (ValidAddress) base[ index ];
			}
		}
	}

	/// <include file='AddressSvc.Doc.xml' path='adapter/ValidAddress/class/*' />
	[Guid("B37F7CBF-F6F8-4459-91F6-51D8E70B9241")]
	[ComVisible(true)]
	public class ValidAddress
	{
		string _addressCode;
		string _line1;
		string _line2;
		string _line3;
		string _line4;
		string _city;
		string _region;
		string _postalCode;
		string _county;
		string _country;
		string _fipsCode;
		string _carrierRoute;
		string _postNet;
		string _addressType;
		
		//added 4.13 latitude longitude change
		string _longitude;
		string _latitude;
        int _taxRegionId;

		/// <include file='AddressSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal ValidAddress()
		{
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="AddressCode"]/*' />
		[DispId(20)]
		public string AddressCode
		{
			get
			{
				return _addressCode;
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="Line1"]/*' />
		[DispId(21)]
		public string Line1
		{
			get
			{
				return _line1;
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="Line2"]/*' />
		[DispId(22)]
		public string Line2
		{
			get
			{
				return _line2;
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="Line3"]/*' />
		[DispId(23)]
		public string Line3
		{
			get
			{
				return _line3;
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="Line4"]/*' />
		[DispId(25)]
		public string Line4
		{
			get
			{
				return _line4;
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="City"]/*' />
		[DispId(26)]
		public string City
		{
			get
			{
				return _city;
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="Region"]/*' />
		[DispId(27)]
		public string Region
		{
			get
			{
				return _region;
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="PostalCode"]/*' />
		[DispId(28)]
		public string PostalCode
		{
			get
			{
				return _postalCode;
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="County"]/*' />
		[DispId(29)]
		public string County
		{
			get
			{
				return _county;
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="Country"]/*' />
		[DispId(30)]
		public string Country
		{
			get
			{
				return _country;
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="FipsCode"]/*' />
		[DispId(31)]
		public string FipsCode
		{
			get
			{
				return _fipsCode;
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="CarrierRoute"]/*' />
		[DispId(32)]
		public string CarrierRoute
		{
			get
			{
				return _carrierRoute;
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="PostNet"]/*' />
		[DispId(33)]
		public string PostNet
		{
			get
			{
				return _postNet;
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="AddressType"]/*' />
		[DispId(34)]
		public string AddressType
		{
			get
			{
				return _addressType;
			}
		}
		
		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="Latitude"]/*' />
		[DispId(35)]
		public string Latitude
		{
			get { return _latitude; }
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="Longitude"]/*' />
		[DispId(36)]
		public string Longitude
		{
			get { return _longitude; }
		}

        /// <include file='AddressSvc.Doc.xml' path='adapter/Address/members[@name="TaxRegionId"]/*' />
        [DispId(37)]
        public int TaxRegionId
        {
            get { return _taxRegionId; }
        }

		#region Internal Members

		/// <summary>
		/// Load an empty local ValidAddress object from the data provided by the web service.
		/// </summary>
		/// <param name="SvcAddress">The ValidAddress object provided by the web service.</param>
		internal void CopyFrom(ProxyValidAddress SvcAddress)
		{
			_addressCode    = SvcAddress.AddressCode;
			_line1          = SvcAddress.Line1;
			_line2          = SvcAddress.Line2;
			_line3          = SvcAddress.Line3;
			_line4          = SvcAddress.Line4;
			_city           = SvcAddress.City;
			_region         = SvcAddress.Region;
			_postalCode     = SvcAddress.PostalCode;
			_county         = SvcAddress.County;
			_country		= SvcAddress.Country;
			_fipsCode       = SvcAddress.FipsCode;
			_carrierRoute   = SvcAddress.CarrierRoute;
			_postNet        = SvcAddress.PostNet;
			_addressType    = SvcAddress.AddressType;
			//Added for Latitude longitude changes for 4.13 release
			_latitude       = SvcAddress.Latitude;
			_longitude      = SvcAddress.Longitude;
            _taxRegionId    = SvcAddress.TaxRegionId;
		}

		#endregion

	}
    
	/// <include file='AddressSvc.Doc.xml' path='adapter/ValidateRequest/class/*' />
	[Guid("5EA2FC77-8DE7-45b5-84AE-C0F9FE2D1041")]
	[ComVisible(true)]
	public class ValidateRequest : BaseRequest
	{
        private Address _address;
		private TextCase _textCase = TextCase.Default;
		private bool _coordinates;
		private bool _taxability;

		/// <include file='AddressSvc.Doc.xml' path='adapter/common/members[@name="Constructor"]/*' />
		public ValidateRequest()
		{
			_coordinates = false;
			_taxability = false;
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/ValidateRequest/members[@name="Address"]/*' />
		[DispId(20)]
		public Address Address
		{
			get
			{
				return _address;
			}
			set
			{
				_address = value;
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/ValidateRequest/members[@name="TextCase"]/*' />
		[DispId(21)]
		public TextCase TextCase
		{
			get
			{
				return _textCase;
			}
			set
			{
				_textCase = value;
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/ValidateRequest/members[@name="Coordinates"]/*' />
		[DispId(22)]
		public bool Coordinates
		{
			get
			{
				return _coordinates;
			}
			set
			{
				_coordinates = value;
			}
		}

		/// <include file='AddressSvc.Doc.xml' path='adapter/ValidateRequest/members[@name="Taxability"]/*' />
		[DispId(23)]
		public bool Taxability
		{
			get
			{
				return _taxability;
			}
			set
			{
				_taxability = value;
			}
		}
	
		#region Internal Members

		internal void CopyTo(ProxyValidateRequest SvcRequest)
		{
			ProxyBaseAddress proxyAddress = new ProxyBaseAddress();
			this.Address.CopyTo(proxyAddress);

			SvcRequest.Address = proxyAddress;
			SvcRequest.TextCase = (ProxyTextCase)this.TextCase;
			//added for 4.13 release
			SvcRequest.Coordinates = _coordinates;
			//added for 5.1 release
			SvcRequest.Taxability = _taxability;
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
            //bool isValid = (
            //                   (Address != null) &&
            //                   ((Address.Line1 != null && Address.Line1.Trim().Length > 0) ||
            //                    (Address.Line2 != null && Address.Line2.Trim().Length > 0) ||
            //                    (Address.Line3 != null && Address.Line3.Trim().Length > 0)) &&
            //                   ((Address.PostalCode != null && Address.PostalCode.Trim().Length > 0) ||
            //                    (Address.City != null && Address.City.Trim().Length > 0) && (Address.Region != null && Address.Region.Trim().Length > 0)));

		    message = string.Empty;

            //if (!isValid)
            //{
            //    message = "Required fields for ValidateRequest are [Line and PostalCode] or [Line, City, and Region]. ";
            //}
            //return isValid;
		    return true;
		}

	    #endregion
    }




	/// <include file='AddressSvc.Doc.xml' path='adapter/TextCase/enum/*' />
	[Guid("06996B63-5543-42a1-9878-16FC0A11895A")]
	[ComVisible(true)]
	public enum TextCase 
	{
        
		/// <include file='AddressSvc.Doc.xml' path='adapter/TextCase/members[@name="Default"]/*' />
		Default = 0,
        
		/// <include file='AddressSvc.Doc.xml' path='adapter/TextCase/members[@name="Upper"]/*' />
		Upper = 1,
        
		/// <include file='AddressSvc.Doc.xml' path='adapter/TextCase/members[@name="Mixed"]/*' />
		Mixed = 2,
	}
}
