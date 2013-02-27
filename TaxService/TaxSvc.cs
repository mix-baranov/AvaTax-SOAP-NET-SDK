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
using Avalara.AvaTax.Adapter.AddressService;
using Avalara.AvaTax.Adapter.Proxies;
using Avalara.AvaTax.Adapter.Proxies.TaxSvcProxy;

namespace Avalara.AvaTax.Adapter.TaxService
{
	/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/class/*' />
	[Guid("0535ECB9-6753-4cfa-AF79-50292069DE90")]
	[ComVisible(true)]
	public class TaxSvc : BaseSvc
	{
		private const string SERVICE_NAME = "Tax/TaxSvc.asmx";
		private AvaLogger _avaLog;

		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="Constructor"]/*' />
		public TaxSvc()
		{
			try 
			{
				_avaLog = AvaLogger.GetLogger();
				_avaLog.Debug(string.Format("Instantiating TaxSvc: {0}", base.UniqueId));

				base.ServiceName = SERVICE_NAME;
				ProxyTaxSvc proxy = new ProxyTaxSvc();
				proxy.ProfileValue = new ProxyProfile();
				proxy.Security = new ProxySecurity();
				base.SvcProxy = proxy;

			} 
			catch (Exception ex)
			{
				ExceptionManager.HandleException(ex);
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="Ping"]/*' />
		[DispId(30)]
		public PingResult Ping(string message)
		{
			try 
			{
				_avaLog.Debug("TaxSvc.Ping");
				
				ProxyPingResult svcResult = (ProxyPingResult) base.InvokeService(typeof(ProxyTaxSvc), MethodBase.GetCurrentMethod().Name, new object[] {message});

				_avaLog.Debug("Copying result from proxy object");
				PingResult localResult = new PingResult();
				localResult.CopyFrom( svcResult );					//load local object with service results

				return localResult;
			} 
			catch (Exception ex)
			{
				return PingResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="IsAuthorized"]/*' />
		[DispId(31)]
		public IsAuthorizedResult IsAuthorized(string operations)
		{
			try
			{
				_avaLog.Debug("TaxSvc.IsAuthorized");

				ProxyIsAuthorizedResult svcResult = (ProxyIsAuthorizedResult) base.InvokeService(typeof(ProxyTaxSvc), MethodBase.GetCurrentMethod().Name, new object[] {operations});

				_avaLog.Debug("Copying result from proxy object");
				IsAuthorizedResult localResult = new IsAuthorizedResult();	//local copy to hold service results
				localResult.CopyFrom( svcResult );	//load local object with service results

				return localResult;
			} 
			catch (Exception ex)
			{
				return IsAuthorizedResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
			}
		}
	
		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/members[@name="GetTax"]/*' />
		[DispId(32)]
		public GetTaxResult GetTax(GetTaxRequest getTaxRequest)
		{
			try 
			{
				_avaLog.Debug("TaxSvc.GetTax");

				Perf monitor1 = new Perf();
				monitor1.Start();

				_avaLog.Debug("Validate request");
				Utilities.VerifyRequestObject(getTaxRequest);

#if DEBUG
				int totalAddresses = (getTaxRequest.OriginAddress == null ? 0 : 1) + (getTaxRequest.DestinationAddress == null ? 0 : 1);
				foreach (Line line in getTaxRequest.Lines)
				{
					totalAddresses += (line.OriginAddress == null ? 0 : 1) + (line.DestinationAddress == null ? 0 : 1);
				}
				_avaLog.Debug(string.Format("Counted {0} address(es)", totalAddresses));
#endif
				ProxyGetTaxRequest proxyRequest = new ProxyGetTaxRequest();
				getTaxRequest.ConsolidateAddresses();
				_avaLog.Debug(string.Format("Consolidated to {0} address(es)", getTaxRequest.Addresses.Count));

				_avaLog.Debug("Copying request into proxy object");
				getTaxRequest.CopyTo(proxyRequest);

				//int timeout = base.Configuration.RequestTimeout;	//store the current seconds (not milliseconds)

				//for each Line in the request, increase the timeout by 500 milliseconds.
				//base.Configuration.RequestTimeoutInMilliseconds += (getTaxRequest.Lines.Count * 500);
				_avaLog.Debug(string.Format("Request timeout set to {0} seconds", ((float)base.Configuration.RequestTimeoutInMilliseconds / 1000.0)));

				ProxyGetTaxResult svcResult = (ProxyGetTaxResult) base.InvokeService(typeof(ProxyTaxSvc), MethodBase.GetCurrentMethod().Name, new object[] {proxyRequest});

				//base.Configuration.RequestTimeout = timeout;		//reset the original value (in seconds)

				_avaLog.Debug("Copying result from proxy object");
				GetTaxResult localResult = new GetTaxResult();
				localResult.CopyFrom(svcResult);
				
				
				//monitor1.Stop(this,svcResult.TransactionId,_log);	
				monitor1.Stop(this,ref svcResult);
				_avaLog.Debug("GetTax (adapter method): " + monitor1.ElapsedSeconds(3).ToString());
				
				return localResult;
				
			} 
			catch (Exception ex)
			{
				return GetTaxResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/members[@name="GetTaxHistory"]/*' />
		[DispId(33)]
		public GetTaxHistoryResult GetTaxHistory(GetTaxHistoryRequest getTaxHistoryRequest)
		{
			try 
			{
				_avaLog.Debug("TaxSvc.GetTaxHistory");
				_avaLog.Debug("Validate request");
				Utilities.VerifyRequestObject(getTaxHistoryRequest);

				_avaLog.Debug("Copying request into proxy object");
				ProxyGetTaxHistoryRequest proxyRequest = new ProxyGetTaxHistoryRequest();
				getTaxHistoryRequest.CopyTo(proxyRequest);

				ProxyGetTaxHistoryResult svcResult = (ProxyGetTaxHistoryResult) base.InvokeService(typeof(ProxyTaxSvc), MethodBase.GetCurrentMethod().Name, new object[] {proxyRequest});

				_avaLog.Debug("Copying result from proxy object");
				GetTaxHistoryResult localResult = new GetTaxHistoryResult();
				localResult.CopyFrom(svcResult);

				return localResult;
			} 
			catch (Exception ex)
			{
				return GetTaxHistoryResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/members[@name="PostTax"]/*' />
		[DispId(34)]
		public PostTaxResult PostTax(PostTaxRequest postTaxRequest)
		{
			try 
			{
				_avaLog.Debug("TaxSvc.PostTax");
				_avaLog.Debug("Validate request");
				Utilities.VerifyRequestObject(postTaxRequest);

				_avaLog.Debug("Copying request into proxy object");
				ProxyPostTaxRequest proxyRequest = new ProxyPostTaxRequest();
				postTaxRequest.CopyTo(proxyRequest);

				ProxyPostTaxResult svcResult = (ProxyPostTaxResult) base.InvokeService(typeof(ProxyTaxSvc), MethodBase.GetCurrentMethod().Name, new object[] {proxyRequest});

				_avaLog.Debug("Copying result from proxy object");
				PostTaxResult localResult = new PostTaxResult();
				localResult.CopyFrom(svcResult);

				return localResult;
			} 
			catch (Exception ex)
			{
				return PostTaxResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/members[@name="CommitTax"]/*' />
		[DispId(35)]
		public CommitTaxResult CommitTax(CommitTaxRequest commitTaxRequest)
		{
			try 
			{
				_avaLog.Debug("TaxSvc.CommitTax");
				_avaLog.Debug("Validate request");
				Utilities.VerifyRequestObject(commitTaxRequest);

				_avaLog.Debug("Copying request into proxy object");
				ProxyCommitTaxRequest proxyRequest = new ProxyCommitTaxRequest();
				commitTaxRequest.CopyTo(proxyRequest);

				ProxyCommitTaxResult svcResult = (ProxyCommitTaxResult) base.InvokeService(typeof(ProxyTaxSvc), MethodBase.GetCurrentMethod().Name, new object[] {proxyRequest});

				_avaLog.Debug("Copying result from proxy object");
				CommitTaxResult localResult = new CommitTaxResult();
				localResult.CopyFrom(svcResult);

				return localResult;
			} 
			catch (Exception ex)
			{
				return CommitTaxResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/members[@name="CancelTax"]/*' />
		[DispId(36)]
		public CancelTaxResult CancelTax(CancelTaxRequest cancelTaxRequest)
		{
			try 
			{
				_avaLog.Debug("TaxSvc.CancelTax");
				_avaLog.Debug("Validate request");
				Utilities.VerifyRequestObject(cancelTaxRequest);

				_avaLog.Debug("Copying request into proxy object");
				ProxyCancelTaxRequest proxyRequest = new ProxyCancelTaxRequest();
				cancelTaxRequest.CopyTo(proxyRequest);

				ProxyCancelTaxResult svcResult = (ProxyCancelTaxResult) base.InvokeService(typeof(ProxyTaxSvc), MethodBase.GetCurrentMethod().Name, new object[] {proxyRequest});

				_avaLog.Debug("Copying result from proxy object");
				CancelTaxResult localResult = new CancelTaxResult();
				localResult.CopyFrom(svcResult);

				return localResult;
			} 
			catch (Exception ex)
			{
				return CancelTaxResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
			}
		}

/*
		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/members[@name="SearchTaxHistory"]/*' />
		[DispId(37)]
		internal SearchTaxHistoryResult SearchTaxHistory(SearchTaxHistoryRequest searchTaxHistoryRequest)
		{
			try 
			{
				_log.Debug("TaxSvc.SearchTaxHistory");
				_log.Debug("Validate request");
				Utilities.VerifyRequestObject(searchTaxHistoryRequest);

				_log.Debug("Copying request into proxy object");
				ProxySearchTaxHistoryRequest proxyRequest = new ProxySearchTaxHistoryRequest();
				searchTaxHistoryRequest.CopyTo(proxyRequest);

				ProxySearchTaxHistoryResult svcResult = (ProxySearchTaxHistoryResult) base.InvokeService(typeof(ProxyTaxSvc), "SearchTaxHistory", new object[] {proxyRequest});

				_log.Debug("Copying result from proxy object");
				SearchTaxHistoryResult localResult = new SearchTaxHistoryResult();
				localResult.CopyFrom(svcResult);

				return localResult;
			} 
			catch (Exception ex)
			{
				return ExceptionManager.HandleException(ex, _log);
			}
		}
*/

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/members[@name="ReconcileTaxHistory"]/*' />
		[DispId(38)]
		public ReconcileTaxHistoryResult ReconcileTaxHistory(ReconcileTaxHistoryRequest reconcileTaxHistoryRequest)
		{
			try 
			{
				_avaLog.Debug("TaxSvc.ReconcileTaxHistory");
				_avaLog.Debug("Validate request");
				Utilities.VerifyRequestObject(reconcileTaxHistoryRequest);

				_avaLog.Debug("Copying request into proxy object");
				ProxyReconcileTaxHistoryRequest proxyRequest = new ProxyReconcileTaxHistoryRequest();
				reconcileTaxHistoryRequest.CopyTo(proxyRequest);

				ProxyReconcileTaxHistoryResult svcResult = (ProxyReconcileTaxHistoryResult) base.InvokeService(typeof(ProxyTaxSvc), MethodBase.GetCurrentMethod().Name, new object[] {proxyRequest});

				_avaLog.Debug("Copying result from proxy object");
				ReconcileTaxHistoryResult localResult = new ReconcileTaxHistoryResult();
				localResult.CopyFrom(svcResult);

				return localResult;
			} 
			catch (Exception ex)
			{
				return ReconcileTaxHistoryResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
			}
		}

		// Update Note : Added for 4.16	
		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/members[@name="AdjustTax"]/*' />
		[DispId(39)]
		public AdjustTaxResult AdjustTax(AdjustTaxRequest adjustTaxRequest)
		{
			try 
			{				
				_avaLog.Debug("TaxSvc.AdjustTax");
				_avaLog.Debug("Validate request");
				Utilities.VerifyRequestObject(adjustTaxRequest);

				_avaLog.Debug("Copying request into proxy object");
				ProxyAdjustTaxRequest proxyRequest = new ProxyAdjustTaxRequest();			

#if DEBUG
				int totalAddresses = (adjustTaxRequest.GetTaxRequest.OriginAddress == null ? 0 : 1) + (adjustTaxRequest.GetTaxRequest.DestinationAddress == null ? 0 : 1);
				foreach (Line line in adjustTaxRequest.GetTaxRequest.Lines)
				{
					totalAddresses += (line.OriginAddress == null ? 0 : 1) + (line.DestinationAddress == null ? 0 : 1);
				}
				_avaLog.Debug(string.Format("Counted {0} address(es)", totalAddresses));
#endif
				adjustTaxRequest.GetTaxRequest.ConsolidateAddresses();
				_avaLog.Debug(string.Format("Consolidated to {0} address(es)", adjustTaxRequest.GetTaxRequest.Addresses.Count));

				adjustTaxRequest.CopyTo(proxyRequest);
				ProxyAdjustTaxResult svcResult = (ProxyAdjustTaxResult) base.InvokeService(typeof(ProxyTaxSvc), MethodBase.GetCurrentMethod().Name, new object[] {proxyRequest});

				_avaLog.Debug("Copying result from proxy object");
				AdjustTaxResult localResult = new AdjustTaxResult();
				localResult.CopyFrom(svcResult);

				return localResult;
			} 
			catch (Exception ex)
			{
				return AdjustTaxResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
			}
		}


		/// Update Note : Added for 5.1	
		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/members[@name="ApplyPayment"]/*' />
		[DispId(40)]
		public ApplyPaymentResult ApplyPayment(ApplyPaymentRequest applyPaymentRequest)
		{
			try 
			{				
				_avaLog.Debug("TaxSvc.ApplyPayment");
				_avaLog.Debug("Validate request");
				Utilities.VerifyRequestObject(applyPaymentRequest);

				_avaLog.Debug("Copying request into proxy object");
				ProxyApplyPaymentRequest proxyRequest = new ProxyApplyPaymentRequest();
				applyPaymentRequest.CopyTo(proxyRequest);
				ProxyApplyPaymentResult svcResult = (ProxyApplyPaymentResult) base.InvokeService(typeof(ProxyTaxSvc), MethodBase.GetCurrentMethod().Name, new object[] {proxyRequest});

				_avaLog.Debug("Copying result from proxy object");
				ApplyPaymentResult localResult = new ApplyPaymentResult();
				localResult.CopyFrom(svcResult);

				return localResult;
			} 
			catch (Exception ex)
			{
				return ApplyPaymentResult.CastFromBaseResult(ExceptionManager.HandleException(ex));
			}
		}



		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="Finalize"]/*' />
		~TaxSvc()
		{
			if(_avaLog != null)
				_avaLog.Debug(string.Format("Finalizing TaxSvc: {0}", base.UniqueId));
		}

		#region Internal Members

		/// <summary>
		/// Convenient wrapper property that casts the base WebServicesClientProtocol into our strongly typed proxy class.
		/// </summary>
		internal new ProxyTaxSvc SvcProxy
		{
			get
			{
				return (ProxyTaxSvc)base.SvcProxy;
			}
		}
		#endregion

	}

	/// <include file='TaxSvc.Doc.xml' path='adapter/Line/class/*' />
	[Guid("F848EF54-CFEE-4d44-A45C-56626A1F2C65")]
	[ComVisible(true)]
	public class Line : BaseRequest
	{
		Address _originAddress;
		Address _destinationAddress;

		string _no;
		string _itemCode;
		string _taxCode;
		Double _qty;
		Decimal _amount;
		bool _discounted;
		string _revAcct;
		string _ref1;
		string _ref2;
		string _exemptionNo;	
		string _description;
		string _customerUsageType; 
        string _businessIdentificationNo;

		//4.17 Changes
		//COmmented out 
		// this will be in use in future release so let the code be in here
		//double _discount;

//		/// <summary>
//		/// Added for 5.0 changes
//		/// </summary>
//		bool _isTaxOverriden;
//		decimal _taxOverride;
		TaxOverride _taxOverride =  new TaxOverride();
		// Update Note : Added for 5.3
		bool _taxIncluded;

		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="Constructor"]/*' />
		public Line() {}

		/// <include file='TaxSvc.Doc.xml' path='adapter/Line/members[@name="OriginAddress"]/*' />
		[DispId(21)]
		public Address OriginAddress
		{
			get { return _originAddress; }
			set
			{
				_originAddress = value;
				SetAddressCode(_originAddress);
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/Line/members[@name="DestinationAddress"]/*' />
		[DispId(22)]
		public Address DestinationAddress
		{
			get { return _destinationAddress; }
			set
			{
				_destinationAddress = value;
				SetAddressCode(_destinationAddress);
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/common/members[@name="No"]/*' />
		[DispId(23)]
		public string No
		{
			get
			{
				return _no;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_no = null;
				}
				else
				{
					_no = value.Trim();
				}
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/Line/members[@name="OriginCode"]/*' />
		//no dispatch id because internal
		internal string OriginCode
		{
			get
			{
				return (_originAddress == null ? "" : _originAddress.AddressCode);
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/Line/members[@name="DestinationCode"]/*' />
		//no dispatch id because internal
		internal string DestinationCode
		{
			get
			{
				return (_destinationAddress == null ? "" : _destinationAddress.AddressCode);
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/Line/members[@name="ItemCode"]/*' />
		[DispId(24)]
		public string ItemCode
		{
			get
			{
				return _itemCode;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_itemCode = null;
				}
				else
				{
					_itemCode = value.Trim();
				}
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/Line/members[@name="TaxCode"]/*' />
		[DispId(25)]
		public string TaxCode
		{
			get
			{
				return _taxCode;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_taxCode = null;
				}
				else
				{
					_taxCode = value.Trim();
				}
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/Line/members[@name="Qty"]/*' />
		[DispId(26)]
		public Double Qty
		{
			get
			{			
				return _qty;
			}
			set
			{
				_qty = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/Line/members[@name="Amount"]/*' />
		[DispId(28)]
		public Decimal Amount
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _amount;
			}
			[param: MarshalAs(UnmanagedType.Currency)]
			set
			{
				_amount = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/Line/members[@name="Discounted"]/*' />
		[DispId(29)]
		public bool Discounted
		{
			get
			{
				return _discounted;
			}
			set
			{
				_discounted = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/Line/members[@name="RevAcct"]/*' />
		[DispId(30)]
		public string RevAcct
		{
			get
			{
				return _revAcct;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_revAcct = null;
				}
				else
				{
					_revAcct = value.Trim();
				}
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/Line/members[@name="Ref1"]/*' />
		[DispId(31)]
		public string Ref1
		{
			get
			{
				return _ref1;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_ref1 = null;
				}
				else
				{
					_ref1 = value.Trim();
				}
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/Line/members[@name="Ref2"]/*' />
		[DispId(32)]
		public string Ref2
		{
			get
			{
				return _ref2;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_ref2 = null;
				}
				else
				{
					_ref2 = value.Trim();
				}
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/Line/members[@name="ExemptionNo"]/*' />
		[DispId(33)]
		public string ExemptionNo
		{
			get
			{
				return _exemptionNo;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_exemptionNo = null;
				}
				else
				{
					_exemptionNo = value.Trim();
				}
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/Line/members[@name="Description"]/*' />
		[DispId(34)]
		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
			}
		}
		/// <include file='TaxSvc.Doc.xml' path='adapter/Line/members[@name="CustomerUsageType"]/*' />
		[DispId(35)]
		public string CustomerUsageType
		{
			get
			{
				return _customerUsageType;
			}
			set
			{
				_customerUsageType = value;
			}
		}

		//Update Note : Added for 5.0
		/// <include file='TaxSvc.Doc.xml' path='adapter/Line/members[@name="TaxOverride"]/*' />
		[DispId(36)]
		public TaxOverride TaxOverride
		{
			get
			{
				return _taxOverride;
			}
			set
			{				
				_taxOverride = value;
			}
		}

		//Update Note : Added for 5.3
		/// <include file='TaxSvc.Doc.xml' path='adapter/Line/members[@name="TaxIncluded"]/*' />
		[DispId(37)]
		public bool TaxIncluded
		{
			get
			{
				return _taxIncluded;
			}
			set
			{				
				_taxIncluded = value;
			}
		}

        [DispId(38)]
        public string BusinessIdentificationNo
        {
            get
            {
                return _businessIdentificationNo;
            }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    _businessIdentificationNo = null;
                }
                else
                {
                    _businessIdentificationNo = value.Trim();
                }
            }
        }

		#region Internal Members

		/// <summary>
		/// Load an empty local Line object from the data provided by the web service.
		/// </summary>
		/// <param name="SvcLine">The Line object provided by the web service.</param>
		/// <param name="addresses">The collection of addresses maintained by the parent <see cref="GetTaxRequest"/>.</param>
		internal void CopyFrom(ProxyLine SvcLine, Addresses addresses)//,bool hasTaxOverride)
		{
			this.OriginAddress = addresses.Find(SvcLine.OriginCode);
			this.DestinationAddress = addresses.Find(SvcLine.DestinationCode);
			_no = SvcLine.No;
			//_originCode = SvcLine.OriginCode;
			//_destinationCode = SvcLine.DestinationCode;
			_itemCode = SvcLine.ItemCode;
			_taxCode = SvcLine.TaxCode;
			_qty = SvcLine.Qty;
			_amount = SvcLine.Amount;
			_discounted = SvcLine.Discounted;
			_revAcct = SvcLine.RevAcct;
			_ref1 = SvcLine.Ref1;
			_ref2 = SvcLine.Ref2;
			_exemptionNo = SvcLine.ExemptionNo;

			//TODO: Added Ammit
			_description = SvcLine.Description;
			_customerUsageType = SvcLine.CustomerUsageType;
			//_discount = SvcLine.Discount;
			//			_isTaxOverriden = SvcLine.IsTaxOverriden;
			//			_taxOverride = SvcLine.TaxOverride;

			//Update Note : Added for 5.0	
			if(SvcLine.TaxOverride != null)// && !hasTaxOverride)
			{
				_taxOverride.TaxOverrideType = (TaxOverrideType)SvcLine.TaxOverride.TaxOverrideType;			
				_taxOverride.TaxAmount = SvcLine.TaxOverride.TaxAmount;

				if(SvcLine.TaxOverride.TaxDate.ToString()!=null || SvcLine.TaxOverride.TaxDate.ToString()!="")
					_taxOverride.TaxDate = TaxOverride.TaxDate;

				_taxOverride.Reason = SvcLine.TaxOverride.Reason;
			}
			//Update Note : Added for 5.3
			_taxIncluded = SvcLine.TaxIncluded;

            _businessIdentificationNo = SvcLine.BusinessIdentificationNo;
		}

		/// <summary>
		/// Loads a local Line object into a web service copy of the same object.
		/// </summary>
		/// <param name="SvcLine">The Line object to be copied to.</param>
		internal void CopyTo(ProxyLine SvcLine)
		{
			SvcLine.DestinationCode = DestinationCode;
			SvcLine.Discounted = Discounted;
			SvcLine.ExemptionNo = ExemptionNo;
			SvcLine.ItemCode = ItemCode;
			SvcLine.No = No;
			SvcLine.OriginCode = OriginCode;
			SvcLine.Amount = Amount;
			SvcLine.Qty = Qty;
			SvcLine.Ref1 = Ref1;
			SvcLine.Ref2 = Ref2;
			SvcLine.RevAcct = RevAcct;
			SvcLine.TaxCode = TaxCode;
			//TODO: Added Ammit
			SvcLine.Description = _description;
			SvcLine.CustomerUsageType = _customerUsageType;
			//SvcLine.Discount = _discount;
//			SvcLine.IsTaxOverriden = _isTaxOverriden;
//			SvcLine.TaxOverride = _taxOverride;

			//Update Note : Added for 5.0	
			if(TaxOverride != null && TaxOverride.TaxOverrideType != TaxOverrideType.None)
			{
				SvcLine.TaxOverride = new ProxyTaxOverride();

				SvcLine.TaxOverride.TaxOverrideType = (ProxyTaxOverrideType)TaxOverride.TaxOverrideType;			
				SvcLine.TaxOverride.TaxAmount = TaxOverride.TaxAmount;
				SvcLine.TaxOverride.TaxDate = TaxOverride.TaxDate;
				SvcLine.TaxOverride.Reason = TaxOverride.Reason;
			}
            else
            {
                SvcLine.TaxOverride = null; // Don't send unnecessarily
            }
			//Update Note : Added for 5.3
			SvcLine.TaxIncluded = _taxIncluded;
		    SvcLine.BusinessIdentificationNo = BusinessIdentificationNo;
		}		

		internal override bool IsValid(out string message)
		{
            message = string.Empty;

            //bool isValid = ((No != null && No.Trim().Length > 0) &&
            //                (Amount >= 0) &&
            //                TaxOverride.IsValid(out message));

            //if (!isValid)
            //{
            //    message += "Required fields for Line are [No and Amount]. ";
            //}

            //return isValid;
		    return true;
		}
		#endregion

		#region Private Members

		private void SetAddressCode(Address address)
		{ //AddressCode is maintained by the adapter and is not exposed read/write
			//through the interface. If we come in with an address that already has an
			//AddressCode, then this address object is coming from a request object
			//or another line object.  The adapter will consolidate these later.
			if (address.AddressCode == null || address.AddressCode.Length == 0)
			{
				address.AddressCode = address.GetHashCode().ToString();
			}
		}

		#endregion
	}

	/// <include file='TaxSvc.Doc.xml' path='adapter/Lines/class/*' />
	[Guid("70911C94-A0DF-49d7-984D-972B6528E803")]
	[ComVisible(true)]
	public class Lines : BaseArrayList
	{
		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal Lines() {}

		/// <include file='TaxSvc.Doc.xml' path='adapter/Lines/members[@name="Add"]/*' />
		[DispId(30)]
		public void Add(Line line)
		{
			//we hide the base member so that we can strongly type our parameter
			try
			{
				base.Add(line);
			}
			catch (ArgumentNullException)
			{
				throw new ArgumentNullException("line", "Cannot add a null line to the collection.");		
			}

		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/Lines/members[@name="this"]/*' />
		[DispId(0)]
		public new Line this[int index]
		{
			get
			{
				//we hide the base member so that we can strongly type our returned object
				return (Line) base[ index ];
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/Lines/members[@name="Clear"]/*' />
		[DispId(31)]
		public new void Clear()
		{
			base.Clear();
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/Lines/members[@name="GetItemByNo"]/*' />
		[DispId(32)]
		public Line GetItemByNo(string lineNo)
		{
			Line item = null;

			foreach (Line line in base.List)
			{
				if (line.No == lineNo)
				{
					item = line;
					break;
				}
			}

			return item;
		}

	}

	/// <include file='TaxSvc.Doc.xml' path='adapter/TaxDetail/class/*' />
	[Guid("86885A2B-BDFC-46c1-9DAF-FC5950D758EB")]
	[ComVisible(true)]
	public class TaxDetail
	{
		JurisdictionType _jurisType;
		string _jurisCode;
		TaxType _taxType;
		//deprecated in 4.1: Decimal _base;
		Decimal _taxable;
		Double _rate;
		Decimal _tax;
		Decimal _nonTaxable;
		Decimal _exemption;
		string _jurisName;
		string _taxName;
		
		//Update Note : Added for 5.0
		string _country;
		string _region;
//		Decimal _base;
		int _taxAuthorityType;
		//Update Note : Added for 5.1
		Decimal _taxCalculated;
		//Update Note : Added for 5.3
		string _taxGroup;
		string _rateType;
		string _stateAssignedNo;

		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal TaxDetail() {}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxDetail/members[@name="JurisType"]/*' />
		[DispId(20)]
		public JurisdictionType JurisType
		{
			get
			{
				return _jurisType;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxDetail/members[@name="JurisCode"]/*' />
		[DispId(21)]
		public string JurisCode
		{
			get
			{
				return _jurisCode;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxDetail/members[@name="TaxType"]/*' />
		[DispId(22)]
		public TaxType TaxType
		{
			get
			{
				return _taxType;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxDetail/members[@name="Taxable"]/*' />
		[DispId(23)]
		public Decimal Taxable
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _taxable;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxDetail/members[@name="Rate"]/*' />
		[DispId(24)]
		public Double Rate
		{
			get
			{
				return _rate;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxDetail/members[@name="Tax"]/*' />
		[DispId(25)]
		public Decimal Tax
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _tax;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxDetail/members[@name="JurisName"]/*' />
		[DispId(26)]
		public string JurisName
		{
			get
			{
				return _jurisName;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxDetail/members[@name="Base"]/*' />
		[DispId(27)]
		public Decimal Base
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _taxable;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxDetail/members[@name="NonTaxable"]/*' />
		[DispId(28)]
		public Decimal NonTaxable
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _nonTaxable;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxDetail/members[@name="Exemption"]/*' />
		[DispId(29)]
		public Decimal Exemption
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _exemption;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxDetail/members[@name="TaxName"]/*' />
		[DispId(30)]
		public string TaxName
		{
			get
			{
				return _taxName;
			}
		}
		
		//Update Note : Added for 5.0
		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxDetail/members[@name="Country"]/*' />
		[DispId(31)]
		public string Country
		{
			get
			{
				return _country;
			}
		}

		//Update Note : Added for 5.0
		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxDetail/members[@name="Region"]/*' />
		[DispId(32)]
		public string Region
		{
			get
			{
				return _region;
			}
		}
		
		//Update Note : Added for 5.0
//		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxDetail/members[@name="Base"]/*' />
//		[DispId(33)]
//		public Decimal Base
//		{
//			[return: MarshalAs(UnmanagedType.Currency)]
//			get
//			{
//				return _base;
//			}
//		}

		//Update Note : Added for 5.0
		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxDetail/members[@name="TaxAuthorityType"]/*' />
		[DispId(34)]
		public int TaxAuthorityType
		{
			get
			{
				return _taxAuthorityType;
			}
		}

		//Update Note : Added for 5.1
		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxDetail/members[@name="TaxCalculated"]/*' />
		[DispId(35)]
		public Decimal TaxCalculated
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _taxCalculated;
			}
		}

		//Update Note : Added for 5.3
		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxDetail/members[@name="TaxGroup"]/*' />
		[DispId(36)]
		public string TaxGroup
		{
			get
			{
				return _taxGroup;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxDetail/members[@name="RateType"]/*' />
		[DispId(37)]
		public string RateType
		{
			get
			{
				return _rateType;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxDetail/members[@name="StateAssignedNo"]/*' />
		[DispId(38)]
		public string StateAssignedNo
		{
			get
			{
				return _stateAssignedNo;
			}
		}

		#region Internal Members

		/// <summary>
		/// Load an empty local TaxDetail object from the data provided by the web service.
		/// </summary>
		/// <param name="SvcTaxDetail">The TaxDetail object provided by the web service.</param>
		internal void CopyFrom(ProxyTaxDetail SvcTaxDetail)
		{
			_jurisType = (JurisdictionType) SvcTaxDetail.JurisType;
			_jurisCode = SvcTaxDetail.JurisCode;
			_taxType = (TaxType) SvcTaxDetail.TaxType;
			//deprecated in 4.1: _base = SvcTaxDetail.Base;
			_rate = SvcTaxDetail.Rate;
			_tax = SvcTaxDetail.Tax;
			_jurisName = SvcTaxDetail.JurisName;
			_taxable = SvcTaxDetail.Taxable;
			_nonTaxable = SvcTaxDetail.NonTaxable;
			_exemption = SvcTaxDetail.Exemption;
			_taxName = SvcTaxDetail.TaxName;

			//Update Note : Added for 5.0
			_country = SvcTaxDetail.Country;
			_region = SvcTaxDetail.Region;
			//_base = SvcTaxDetail.Base;
			_taxAuthorityType = SvcTaxDetail.TaxAuthorityType;
			//Update Note : Added for 5.1
			_taxCalculated = SvcTaxDetail.TaxCalculated;
			//Update Note : Added for 5.3
			_taxGroup = SvcTaxDetail.TaxGroup;
			_rateType = SvcTaxDetail.RateType;
			_stateAssignedNo = SvcTaxDetail.StateAssignedNo;
		}

		#endregion

	}


	/// <include file='TaxSvc.Doc.xml' path='adapter/TaxDetails/class/*' />
	[Guid("BAE3A4C5-1682-43ea-A8E0-553BC10C81E8")]
	[ComVisible(true)]
	public class TaxDetails : ReadOnlyArrayList
	{
		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal TaxDetails() {}

		/// <include file='TaxSvc.Doc.xml' path='adapter/collection/members[@name="this"]/*' />
		[DispId(0)]
		public new TaxDetail this[int index]
		{
			get
			{
				return (TaxDetail) base[ index ];
			}
		}
	}

	//Update Note : Added for 5.0
	/// <include file='TaxSvc.Doc.xml' path='adapter/TaxOverride/class/*' />
	[Guid("36486263-99dc-4506-9b3e-bbfebcc19e16")]
	[ComVisible(true)]
	public class TaxOverride : BaseRequest
	{
		
		TaxOverrideType _taxOverrideType;
		Decimal _taxAmount;
		DateTime _taxDate = DateUtil.MinDate;
		string _reason;

		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal TaxOverride() {}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxOverride/members[@name="TaxOverrideType"]/*' />
		[DispId(30)]
		public TaxOverrideType TaxOverrideType
		{
			get
			{
				return _taxOverrideType;
			}
			set
			{
				_taxOverrideType = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxOverride/members[@name="TaxAmount"]/*' />
		[DispId(31)]
		public Decimal TaxAmount
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _taxAmount;
			}
			[param: MarshalAs(UnmanagedType.Currency)]
			set
			{
				_taxAmount = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxOverride/members[@name="TaxDate"]/*' />
		[DispId(32)]
		public DateTime TaxDate
		{
			get
			{
				return _taxDate;
			}
			set
			{
				_taxDate = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxOverride/members[@name="Reason"]/*' />
		[DispId(33)]
		public string Reason
		{
			get
			{
				return _reason;
			}
			set
			{
				_reason = value;
			}
		}

		#region Internal Members

		/// <summary>
		/// Load an empty local TaxOverride object from the data provided by the web service.
		/// </summary>
		/// <param name="SvcTaxOverride">The TaxOverride object provided by the web service.</param>
		internal void CopyFrom(ProxyTaxOverride SvcTaxOverride)
		{
			_taxOverrideType = (TaxOverrideType)SvcTaxOverride.TaxOverrideType;
			_taxAmount = SvcTaxOverride.TaxAmount;
			_taxDate = SvcTaxOverride.TaxDate;
			_reason = SvcTaxOverride.Reason;
		}

        internal override bool IsValid(out string message)
        {
            message = string.Empty;
            return true;
		}

		#endregion

	}


	/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSummary/class/*' />
	[Guid("eba7f5ec-b011-49f5-a1e2-ed50366cf31a")]
	[ComVisible(true)]
	public class TaxSummary : ReadOnlyArrayList
	{
		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal TaxSummary() {}

		/// <include file='TaxSvc.Doc.xml' path='adapter/collection/members[@name="this"]/*' />
		[DispId(0)]
		public new TaxDetail this[int index]
		{
			get
			{
				return (TaxDetail) base[ index ];
			}
		}
	}

	/// <include file='TaxSvc.Doc.xml' path='adapter/TaxLine/class/*' />
	[Guid("B1AF846D-6A20-402c-9F63-5ACB5FD92187")]
	[ComVisible(true)]
	public class TaxLine
	{
		string _no;
		string _taxCode;
		Decimal _discount;
		Decimal _taxable;
		Double _rate;
		Decimal _tax;
		TaxDetails _taxDetails = new TaxDetails();
		Decimal _exemption;
		bool _taxability;
		BoundaryLevel _boundaryLevel;
		//Update Note : Added for 4.16
		int _exemptCertId;

		//Update Note : Added for 5.1
		AccountingMethod _accountingMethod = AccountingMethod.Accrual;
		Decimal _taxCalculated;
        // Update Note : Added for 5.3
        bool _taxIncluded;

		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal TaxLine() {}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/common/members[@name="No"]/*' />
		[DispId(20)]
		public string No
		{
			get
			{
				return _no;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxLine/members[@name="TaxCode"]/*' />
		[DispId(21)]
		public string TaxCode
		{
			get
			{
				return _taxCode;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxLine/members[@name="Discount"]/*' />
		[DispId(22)]
		public Decimal Discount
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _discount;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxLine/members[@name="Taxable"]/*' />
		[DispId(23)]
		public Decimal Taxable
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _taxable;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxLine/members[@name="Rate"]/*' />
		[DispId(24)]
		public Double Rate
		{
			get
			{
				return _rate;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxLine/members[@name="Tax"]/*' />
		[DispId(25)]
		public Decimal Tax
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _tax;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxLine/members[@name="TaxDetails"]/*' />
		[DispId(26)]
		public TaxDetails TaxDetails
		{
			get
			{
				return _taxDetails;
			}

		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxLine/members[@name="Exemption"]/*' />
		[DispId(27)]
		public Decimal Exemption
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _exemption;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxLine/members[@name="Taxability"]/*' />
		[DispId(28)]
		public bool Taxability
		{
			get
			{
				return _taxability;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxLine/members[@name="BoundaryLevel"]/*' />
		[DispId(29)]
		public BoundaryLevel BoundaryLevel
		{
			get
			{
				return _boundaryLevel;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxLine/members[@name="ExemptCertId"]/*' />
		[DispId(30)]
		public int ExemptCertId
		{
			get
			{
				return _exemptCertId;
			}
		}

		//Update Note : Added for 5.1
		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxLine/members[@name="AccountingMethod"]/*' />
		[DispId(32)]
		public AccountingMethod AccountingMethod
		{
			get
			{
				return _accountingMethod;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxLine/members[@name="TaxCalculated"]/*' />
		[DispId(33)]
		public Decimal TaxCalculated
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _taxCalculated;
			}
		}

        //Update Note : Added for 5.3
        /// <include file='TaxSvc.Doc.xml' path='adapter/TaxLine/members[@name="TaxIncluded"]/*' />
        [DispId(34)]
        public bool TaxIncluded
        {
            get
            {
                return _taxIncluded;
            }
            set
            {
                _taxIncluded = value;
            }
        }

		#region Internal Members

		/// <summary>
		/// Load an empty local TaxLine object from the data provided by the web service.
		/// </summary>
		/// <param name="SvcTaxLine">The TaxLine object provided by the web service.</param>
		internal void CopyFrom(ProxyTaxLine SvcTaxLine)
		{
			_no = SvcTaxLine.No;
			_taxCode = SvcTaxLine.TaxCode;
			_discount = SvcTaxLine.Discount;
			_taxable = SvcTaxLine.Taxable;
			_rate = SvcTaxLine.Rate;
			_tax = SvcTaxLine.Tax;
			_exemption = SvcTaxLine.Exemption;
			_taxability = SvcTaxLine.Taxability;
			_boundaryLevel = (BoundaryLevel)SvcTaxLine.BoundaryLevel;
			_exemptCertId = SvcTaxLine.ExemptCertId;
			//Update Note : Added for 5.1
			_accountingMethod = (AccountingMethod)SvcTaxLine.AccountingMethod;

			for (int i = 0; i < SvcTaxLine.TaxDetails.Length; i++)
			{
				TaxDetail taxDetail = new TaxDetail();
				taxDetail.CopyFrom(SvcTaxLine.TaxDetails[i]);
				_taxDetails.Add(taxDetail);
			}
			//Update Note : Added for 5.1
			_taxCalculated = SvcTaxLine.TaxCalculated;
            //Update Note : Added for 5.3
            _taxIncluded = SvcTaxLine.TaxIncluded;
		}

		#endregion

	}

	/// <include file='TaxSvc.Doc.xml' path='adapter/TaxLines/class/*' />
	[Guid("011E30DC-45B6-4f43-80CB-5BB2CD44F3E9")]
	[ComVisible(true)]
	public class TaxLines : ReadOnlyArrayList
	{
		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal TaxLines() {}

		/// <include file='TaxSvc.Doc.xml' path='adapter/collection/members[@name="this"]/*' />
		[DispId(0)]
		public new TaxLine this[int index]
		{
			get
			{
				return (TaxLine) base[ index ];
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxLines/members[@name="GetItemByNo"]/*' />
		[DispId(40)]
		public TaxLine GetItemByNo(string lineNo)
		{
			TaxLine item = null;

			foreach (TaxLine line in base.List)
			{
				if (line.No == lineNo)
				{
					item = line;
					break;
				}
			}

			return item;
		}

	}

	/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/class/*' />
	[Guid("1933CA1C-C141-4329-9152-8BB732338F7B")]
	[ComVisible(true)]
	public class GetTaxRequest : BaseRequest
	{
		Address _originAddress;
		Address _destinationAddress;
		string _companyCode;
		DocumentType _docType;
		string _docCode;
		DateTime _docDate;
		string _salespersonCode;
		string _customerCode;
		string _customerUsageType;
		Decimal _discount;
		string _exemptionNo;
		string _locationCode;
		Addresses _addresses = new Addresses();
		Lines _lines = new Lines();
		DetailLevel _detailLevel = DetailLevel.Document;
		string _purchaseOrderNo;
		string _referenceCode;
		bool _commit;
        string _businessIdentificationNo;

		//added for 5.0
		//bool _isTotalTaxOverriden;
		//decimal _totalTaxOverride;
		TaxOverride _taxOverride =  new TaxOverride();
        string _currencyCode;
        ServiceMode _serviceMode;
		//added for 5.1
		DateTime _paymentDate;
		//added for 5.2
		decimal _exchangeRate;
		DateTime _exchangeRateEffDate;

		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="Constructor"]/*' />
		public GetTaxRequest()
		{
			_paymentDate = DateUtil.MinDate;
			_exchangeRate = 1m;
			_exchangeRateEffDate = DateUtil.MinDate;
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="OriginAddress"]/*' />
		[DispId(31)]
		public Address OriginAddress
		{
			get { return _originAddress; }
			set
			{
				_originAddress = value;
				SetAddressCode(_originAddress);
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="DestinationAddress"]/*' />
		[DispId(32)]
		public Address DestinationAddress
		{
			get { return _destinationAddress; }
			set
			{
				_destinationAddress = value;
				SetAddressCode(_destinationAddress);
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="CompanyCode"]/*' />
		[DispId(33)]
		public string CompanyCode
		{
			get
			{
				return _companyCode;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_companyCode = null;
				}
				else
				{
					_companyCode = value.Trim();
				}
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="DocType"]/*' />
		[DispId(34)]
		public DocumentType DocType
		{
			get
			{
				return _docType;
			}
			set
			{
				Utilities.VerifyEnum(typeof(DocumentType), value);
				_docType = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/common/members[@name="DocCode"]/*' />
		[DispId(35)]
		public string DocCode
		{
			get
			{
				return _docCode;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_docCode = null;
				}
				else
				{
					_docCode = value.Trim();
				}
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="DocDate"]/*' />
		[DispId(36)]
		public DateTime DocDate
		{
			get
			{
				return _docDate;
			}
			set
			{
				//Utilities.VerifyDate(value);
				_docDate = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="SalespersonCode"]/*' />
		[DispId(37)]
		public string SalespersonCode
		{
			get
			{
				return _salespersonCode;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_salespersonCode = null;
				}
				else
				{
					_salespersonCode = value.Trim();
				}
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="CustomerCode"]/*' />
		[DispId(38)]
		public string CustomerCode
		{
			get
			{
				return _customerCode;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_customerCode = null;
				}
				else
				{
					_customerCode = value.Trim();
				}
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="CustomerUsageType"]/*' />
		[DispId(39)]
		public string CustomerUsageType
		{
			get
			{
				return _customerUsageType;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_customerUsageType = null;
				}
				else
				{
					_customerUsageType = value.Trim();
				}
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="Discount"]/*' />
		[DispId(40)]
		public Decimal Discount
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _discount;
			}
			[param: MarshalAs(UnmanagedType.Currency)]
			set
			{
				_discount = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="ExemptionNo"]/*' />
		[DispId(41)]
		public string ExemptionNo
		{
			get
			{
				return _exemptionNo;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_exemptionNo = null;
				}
				else
				{
					_exemptionNo = value.Trim();
				}
			}
		}		

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="OriginCode"]/*' />
		//no dispatch id because internal
		internal string OriginCode
		{
			get
			{
				return (_originAddress == null ? "" : _originAddress.AddressCode);
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="DestinationCode"]/*' />
		//no dispatch id because internal
		internal string DestinationCode
		{
			get
			{
				return (_destinationAddress == null ? "" : _destinationAddress.AddressCode);
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="Addresses"]/*' />
		//no dispatch id because internal
		internal Addresses Addresses
		{
			get
			{
				return _addresses;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="Lines"]/*' />
		[DispId(42)]
		public Lines Lines
		{
			get
			{
				return _lines;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="DetailLevel"]/*' />
		[DispId(43)]
		public DetailLevel DetailLevel
		{
			get
			{
				return _detailLevel;
			}
			set
			{
				Utilities.VerifyEnum(typeof(TaxService.DetailLevel), value);
				_detailLevel = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="PurchaseOrderNo"]/*' />
		[DispId(44)]
		public string PurchaseOrderNo
		{
			get
			{
				return _purchaseOrderNo;
			}
			set
			{
				_purchaseOrderNo = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="ReferenceCode"]/*' />
		[DispId(45)]
		public string ReferenceCode
		{
			get { return _referenceCode; }
			set { _referenceCode = value; }
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="LocationCode"]/*' />
		[DispId(46)]
		public string LocationCode
		{
			get
			{
				return _locationCode;
			}
			set
			{				
				_locationCode = value;
			}
		}

		//Update Note : Added for 4.16
		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="Commit"]/*' />
		[DispId(48)]
		public bool Commit
		{
			get
			{
				return _commit;
			}
			set
			{				
				_commit = value;
			}
		}
		
		//Update Note : Added for 5.0
		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="TaxOverride"]/*' />
		[DispId(49)]
		public TaxOverride TaxOverride
		{
			get
			{
				return _taxOverride;
			}
			set
			{				
				_taxOverride = value;
			}
		}

        /// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="CurrencyCode"]/*' />
        [DispId(50)]
        public string CurrencyCode
        {
            get
            {
                return _currencyCode;
            }
            set
            {
                _currencyCode = value;
            }
        }

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="ServiceMode"]/*' />
        [DispId(51)]
        public ServiceMode ServiceMode
        {
            get
            {
                return _serviceMode;
            }
            set
            {
                _serviceMode = value;
            }
        }

		//Update Note : Added for 5.1
        /// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="PaymentDate"]/*' />
        [DispId(52)]
        public DateTime PaymentDate
        {
            get
            {
                return _paymentDate;
            }
            set
            {
                _paymentDate = value;
            }
        }

		//Update Note : Added for 5.2
		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="ExchangeRate"]/*' />
		[DispId(53)]
		public Decimal ExchangeRate
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _exchangeRate;
			}
			[param: MarshalAs(UnmanagedType.Currency)]
			set
			{
				_exchangeRate = value;
			}
		}
		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxRequest/members[@name="ExchangeRateEffDate"]/*' />
		[DispId(54)]
		public DateTime ExchangeRateEffDate
		{
			get
			{
				return _exchangeRateEffDate;
			}
			set
			{
				_exchangeRateEffDate = value;
			}
		}

        [DispId(55)]
        public string BusinessIdentificationNo
        {
            get
            {
                return _businessIdentificationNo;
            }
            set
            {
                if (value == null || value.Trim() == "")
                {
                    _businessIdentificationNo = null;
                }
                else
                {
                    _businessIdentificationNo = value.Trim();
                }
            }
        }

        #region Internal Members

		/// <summary>
		/// Called by <see cref="TaxSvc.GetTax"/> prior to passing to the web service. Consolidates Request
		/// and Line addresses into a single collection of non-duplicated addresses.
		/// </summary>
		//no dispatch id because internal
		internal void ConsolidateAddresses()
		{
			Address obj;

			//technically nothing should be in the collection before we add origin and destination to it;
			//but in an effort to build bullet-proof code, we are checking anyway. The cost of the call
			//on an empty collection should be near enough to nil.
			if (_originAddress != null)
			{
				obj = (Address)Utilities.FindInCollection(_addresses, _originAddress);
				if (obj == null)
				{
					_addresses.Add(_originAddress);
				}
				else
				{
					//duplicate found, use original already in collection
					this.OriginAddress = obj;
				}
			}

			if (_destinationAddress != null)
			{
				obj = (Address)Utilities.FindInCollection(_addresses, _destinationAddress);
				if (obj == null)
				{
					_addresses.Add(_destinationAddress);
				}
				else
				{
					//duplicate found, use original already in collection
					this.DestinationAddress = obj;
				}
			}

			//if we find duplicates in a line's address, we are going to reset it with the
			//one found in the collection. this takes care of the case of a line address
			//that had all the same information but a different reference/instance (and therefore
			//a different AddressCode). See also GetTaxRequest.CopyTo().
			foreach (Line line in _lines)
			{
				if (line.OriginAddress == null)
				{
					//if both are null, then this error will be raised later.
					//TODO: if Adapter implements the Rules Engine, then it will be raised in TaxSvc.GetTax(), otherwise we let the Service catch it and throw the error
					if (this.OriginAddress != null)
					{ 
						//default to parent's address
						line.OriginAddress = this.OriginAddress;
					}
				}
				else
				{
					obj = (Address)Utilities.FindInCollection(_addresses, line.OriginAddress);
					if (obj == null)
					{
						_addresses.Add(line.OriginAddress);
					}
					else
					{
						//duplicate found, use original already in collection
						line.OriginAddress = obj;
					}
				}

				if (line.DestinationAddress == null)
				{
					//if both are null, then this error will be raised later.
					//TODO: if Adapter implements the Rules Engine, then it will be raised in TaxSvc.GetTax(), otherwise we let the Service catch it and throw the error
					if (this.DestinationAddress != null)
					{ 
						//default to parent's address
						line.DestinationAddress = this.DestinationAddress;
					}
				}
				else
				{
					obj = (Address)Utilities.FindInCollection(_addresses, line.DestinationAddress);
					if (obj == null)
					{
						_addresses.Add(line.DestinationAddress);
					}
					else
					{
						//duplicate found, use original already in collection
						line.DestinationAddress = obj;
					}
				}
			}

/*
			//if request level addresses were left blank, then we need to roll the first available 
			//line address (post consolidation) up to the request level
			if (_originAddress == null)
			{
				foreach (Line line in _lines)
				{
					if (line.OriginAddress != null)
					{
						_originAddress = line.OriginAddress;
						break;
					}
				}
			}
			if (_destinationAddress == null)
			{
				foreach (Line line in _lines)
				{
					if (line.DestinationAddress != null)
					{
						_destinationAddress = line.DestinationAddress;
						break;
					}
				}
			}
*/
		}

		/// <summary>
		/// Load an empty local GetTaxRequest object from the data provided by the web service.
		/// </summary>
		/// <param name="SvcResult">The GetTaxRequest object provided by the web service.</param>
		internal void CopyFrom(ProxyGetTaxRequest SvcResult)
		{
			_companyCode = SvcResult.CompanyCode;
			_docType = (DocumentType)SvcResult.DocType;
			_docCode = SvcResult.DocCode;
			_docDate = SvcResult.DocDate;
			_salespersonCode = SvcResult.SalespersonCode;
			_customerCode = SvcResult.CustomerCode;
			_customerUsageType = SvcResult.CustomerUsageType;
			_discount = SvcResult.Discount;
			_exemptionNo = SvcResult.ExemptionNo;
			//_originCode = SvcResult.OriginCode;
			//_destinationCode = SvcResult.DestinationCode;
			_detailLevel = (DetailLevel)SvcResult.DetailLevel;
			_purchaseOrderNo = SvcResult.PurchaseOrderNo;
			_referenceCode = SvcResult.ReferenceCode;
			_locationCode = SvcResult.LocationCode;
			_commit = SvcResult.Commit;
		    _businessIdentificationNo = SvcResult.BusinessIdentificationNo;
			//_totalTaxOverride = SvcResult.TotalTaxOverride;
			//_isTotalTaxOverriden = SvcResult.IsTotalTaxOverriden;

			//Update Note : Added for 5.0
			//bool hasTaxOverride=false;		
			if(SvcResult.TaxOverride != null)
			{
				_taxOverride.TaxOverrideType = (TaxOverrideType)SvcResult.TaxOverride.TaxOverrideType;			
				_taxOverride.TaxAmount = SvcResult.TaxOverride.TaxAmount;
				_taxOverride.TaxDate = (SvcResult.TaxOverride.TaxDate.ToString()==null || SvcResult.TaxOverride.TaxDate.ToString().Trim()=="")?SvcResult.DocDate:SvcResult.TaxOverride.TaxDate;
				_taxOverride.Reason = SvcResult.TaxOverride.Reason;
				//hasTaxOverride = true;
			}
            _currencyCode = SvcResult.CurrencyCode;
            _serviceMode = (ServiceMode)SvcResult.ServiceMode;
			//Update Note : Added for 5.1
			_paymentDate = SvcResult.PaymentDate;
			//Update Note : Added for 5.2
			_exchangeRate = SvcResult.ExchangeRate;
			_exchangeRateEffDate = SvcResult.ExchangeRateEffDate;

			Address address;
			if (SvcResult.Addresses != null && SvcResult.Addresses.Length > 0) 
			{
				for (int idx = 0; idx < SvcResult.Addresses.Length; idx++)
				{
					address = new Address();
					address.CopyFrom(SvcResult.Addresses[idx]);
					_addresses.Add(address);
				}

				address = _addresses.Find(SvcResult.OriginCode);
				if (address != null)
				{
					this.OriginAddress = address;
				}
				address = _addresses.Find(SvcResult.DestinationCode);
				if (address != null)
				{
					this.DestinationAddress = address;
				}
			}

			if (SvcResult.Lines != null) 
			{
				for (int idx = 0; idx < SvcResult.Lines.Length; idx++)
				{
					ProxyLine proxyLine = SvcResult.Lines[idx];
					// Handle default addresses
					if (proxyLine.DestinationCode.Length == 0)
					{
						proxyLine.DestinationCode = SvcResult.DestinationCode;
					}
					if (proxyLine.OriginCode.Length == 0)
					{
						proxyLine.OriginCode = SvcResult.OriginCode;
					}
					Line line = new Line();					
					line.CopyFrom(proxyLine, _addresses);//, hasTaxOverride);

					//If the date is not overridden, then it should be set to the same as the DocDate.
					if(proxyLine.TaxOverride!=null)
					{
						if ((proxyLine.TaxOverride.TaxDate.ToString() == null || proxyLine.TaxOverride.TaxDate.ToString() == "") && ((TaxOverrideType)proxyLine.TaxOverride.TaxOverrideType > TaxOverrideType.None))
						{
							proxyLine.TaxOverride.TaxDate = SvcResult.DocDate;
						}
					}
					_lines.Add(line);
				}
			}
		}

		/// <summary>
		/// Loads a local GetTaxRequest object into a web service copy of the same object.
		/// </summary>
		/// <param name="SvcRequest">The GetTaxRequest object to be copied to.</param>
		internal void CopyTo(ProxyGetTaxRequest SvcRequest)
		{
			SvcRequest.CompanyCode = CompanyCode;
			SvcRequest.DestinationCode = DestinationCode;
			SvcRequest.DetailLevel = (ProxyDetailLevel)DetailLevel;
			SvcRequest.Discount = Discount;
			SvcRequest.DocCode = DocCode;
			SvcRequest.DocDate = DocDate;
			SvcRequest.DocType = (ProxyDocumentType)DocType;
			SvcRequest.ExemptionNo = ExemptionNo;
			SvcRequest.OriginCode = OriginCode;
			SvcRequest.CustomerCode = CustomerCode;
			SvcRequest.CustomerUsageType = CustomerUsageType;
			SvcRequest.SalespersonCode = SalespersonCode;
			SvcRequest.PurchaseOrderNo = PurchaseOrderNo;
			SvcRequest.ReferenceCode = ReferenceCode;
			SvcRequest.LocationCode = LocationCode;
			SvcRequest.Commit = Commit;
		    SvcRequest.BusinessIdentificationNo = BusinessIdentificationNo;
			//SvcRequest.TotalTaxOverride = TotalTaxOverride;
			//SvcRequest.IsTotalTaxOverriden = IsTotalTaxOverriden;
			
			//Update Note : Added for 5.0	
			if(TaxOverride != null && TaxOverride.TaxOverrideType != TaxOverrideType.None)
			{
				SvcRequest.TaxOverride = new ProxyTaxOverride();

				SvcRequest.TaxOverride.TaxOverrideType = (ProxyTaxOverrideType)TaxOverride.TaxOverrideType;
				SvcRequest.TaxOverride.TaxAmount = TaxOverride.TaxAmount;
				SvcRequest.TaxOverride.TaxDate = TaxOverride.TaxDate;
				SvcRequest.TaxOverride.Reason = TaxOverride.Reason;
			}
            else
			{
			    SvcRequest.TaxOverride = null; // Don't send unnecessarily
			}

            SvcRequest.CurrencyCode = CurrencyCode;
            SvcRequest.ServiceMode = (ProxyServiceMode)ServiceMode;
			//Update Note : Added for 5.1
			SvcRequest.PaymentDate = PaymentDate;
			//Update Note : Added for 5.2
			SvcRequest.ExchangeRate = (ExchangeRate != 0) ? ExchangeRate : 1.0m;
			SvcRequest.ExchangeRateEffDate = (ExchangeRateEffDate > DateUtil.MinDate) ? ExchangeRateEffDate : DateUtil.MinDate;
            
			int idx = 0;
			SvcRequest.Addresses = new ProxyBaseAddress[Addresses.Count];
			foreach (Address address in Addresses)
			{
				ProxyBaseAddress svcAddress = new ProxyBaseAddress();
				address.CopyTo(svcAddress);
				SvcRequest.Addresses[idx++] = svcAddress;
			}

			idx = 0;
			SvcRequest.Lines = new ProxyLine[Lines.Count];
			foreach (Line line in Lines)
			{
				ProxyLine svcLine = new ProxyLine();
				line.CopyTo(svcLine);
				SvcRequest.Lines[idx++] = svcLine;
			}
		}

        internal override bool IsValid(out string message)
		{
            //bool hasValidAddress = (DestinationAddress != null);

            //if(!hasValidAddress )
            //{
            //    foreach (Line line in Lines)
            //    {
            //        if(line.DestinationAddress==null)
            //        {
            //            hasValidAddress = false;
            //            break;
            //        }
            //        hasValidAddress = true;                   
            //    }
            //}

            message = string.Empty;

            //bool isValid = ((hasValidAddress || (LocationCode != null && LocationCode.Trim().Length > 0)) &&
            //                (DocCode != null && DocCode.Trim().Length > 0) &&
            //                (DocDate > DateTime.MinValue) &&
            //                (CustomerCode != null && CustomerCode.Trim().Length > 0) &&
            //                (Lines.Count > 0));

            //if(!isValid)
            //{
            //    message += "Required fields for GetTaxRequest are [LocationCode or DestinationAddress (header or line level), Line, CustomerCode, DocCode, and DocDate]. ";
            //}

            //return isValid;
            return true;
		}

		#endregion

		#region Private Members

		private void SetAddressCode(Address address)
		{ //AddressCode is maintained by the adapter and is not exposed read/write
			//through the interface. If we come in with an address that already has an
			//AddressCode, then this address object is coming from a request object
			//or another line object.  The adapter will consolidate these later.
			if (address.AddressCode == null || address.AddressCode.Length == 0)
			{
				address.AddressCode = address.GetHashCode().ToString();
			}
		}

		#endregion

	}

	/// <include file='TaxSvc.Doc.xml' path='adapter/ReconcileTaxHistoryRequest/class/*' />
	[Guid("A70B42E3-F0F6-45d8-BBA3-10B81A9BBFA8")]
	[ComVisible(true)]
	public class ReconcileTaxHistoryRequest : BaseRequest
	{
		string _companyCode;
		//TODO: verify added manually...to be inline with proxy		
		DateTime _startDate;	
		DateTime _endDate;		
		DocStatus _docStatus;
        DocumentType _docType;
		string _lastDocCode;
	    int _pageSize;

		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="Constructor"]/*' />
		public ReconcileTaxHistoryRequest()
		{
            _docStatus = DocStatus.Any;
            _docType = DocumentType.Any;
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/common/members[@name="CompanyCode"]/*' />
		[DispId(30)]
		public string CompanyCode
		{
			get
			{
				return _companyCode;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_companyCode = null;
				}
				else
				{
					_companyCode = value.Trim();
				}
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/ReconcileTaxHistoryRequest/members[@name="StartDate"]/*' />
		[DispId(31)]
		public DateTime StartDate
		{
			get
			{
				return _startDate;
			}
			set
			{
				_startDate = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/ReconcileTaxHistoryRequest/members[@name="EndDate"]/*' />
		[DispId(32)]
		public DateTime EndDate
		{
			get
			{
				return _endDate;
			}
			set
			{
				_endDate = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/ReconcileTaxHistoryRequest/members[@name="DocStatus"]/*' />
		[DispId(33)]
		public DocStatus DocStatus
		{
			get
			{
				return _docStatus;
			}
			set
			{
				_docStatus = value;
			}
        }

        /// <include file='TaxSvc.Doc.xml' path='adapter/ReconcileTaxHistoryRequest/members[@name="DocType"]/*' />
        [DispId(34)]
        public DocumentType DocType
        {
            get
            {
                return _docType;
            }
            set
            {
                Utilities.VerifyEnum(typeof(DocumentType), value);
                _docType = value;
            }
        }

		/// <include file='TaxSvc.Doc.xml' path='adapter/ReconcileTaxHistoryRequest/members[@name="LastDocCode"]/*' />
		[DispId(35)]
		public string LastDocCode
		{
			get
			{
				return _lastDocCode;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_lastDocCode = null;
				}
				else
				{
					_lastDocCode = value.Trim();
				}
			}
		}

        /// <include file='TaxSvc.Doc.xml' path='adapter/ReconcileTaxHistoryRequest/members[@name="PageSize"]/*' />
		[DispId(36)]
		public int PageSize
		{
			get
			{
				return _pageSize;
			}
			set
			{
				_pageSize = value;
			}
		}

		#region Internal Members

		/// <summary>
		/// Loads a local ReconcileTaxHistoryRequest object into a web service copy of the same object.
		/// </summary>
		/// <param name="SvcRequest">The ReconcileTaxHistoryRequest object to be copied to.</param>
		internal void CopyTo(ProxyReconcileTaxHistoryRequest SvcRequest)
		{
			SvcRequest.CompanyCode = _companyCode;
			SvcRequest.StartDate = _startDate;
			SvcRequest.EndDate = _endDate;			
			SvcRequest.DocStatus = (ProxyDocStatus)_docStatus;
            SvcRequest.DocType = (ProxyDocumentType)_docType;
			SvcRequest.LastDocCode = _lastDocCode;
			SvcRequest.PageSize = _pageSize;
		}

        internal override bool IsValid(out string message)
        {
            message = string.Empty;
            return true;
		}

		#endregion
	}

/*
	/// <include file='TaxSvc.Doc.xml' path='adapter/SearchTaxHistoryRequest/class/*' />
	[Guid("30076681-179E-4e0a-9704-8581D92D682E")]
	[ComVisible(true)]
	internal class SearchTaxHistoryRequest : BaseRequest
	{
		string _companyCode;
		string _query;
		string _lastDocId;

		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="Constructor"]/*' />
		public SearchTaxHistoryRequest()
		{
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/common/members[@name="CompanyCode"]/*' />
		[DispId(30)]
		public string CompanyCode
		{
			get
			{
				return _companyCode;
			}
			set
			{
				_companyCode = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/SearchTaxHistoryRequest/members[@name="Query"]/*' />
		[DispId(31)]
		public string Query
		{
			get
			{
				return _query;
			}
			set
			{
				_query = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/SearchTaxHistoryRequest/members[@name="LastDocId"]/*' />
		[DispId(32)]
		public string LastDocId
		{
			get
			{
				return _lastDocId.ToString();
			}
			set
			{
				_lastDocId = value;
			}
		}

		#region Internal Members
		
		/// <summary>
		/// Loads a local SearchTaxHistoryRequest object into a web service copy of the same object.
		/// </summary>
		/// <param name="SvcRequest">The SearchTaxHistoryRequest object to be copied to.</param>
		internal void CopyTo(ProxySearchTaxHistoryRequest SvcRequest)
		{
			SvcRequest.CompanyCode = _companyCode;
			SvcRequest.LastDocCode = _lastDocId.ToString();
			SvcRequest.Query = _query;
		}

		internal override bool IsValid()
		{
			return (
				(this._companyCode != null && this._companyCode.Trim().Length > 0) ||
				(this._lastDocId != null && this._lastDocId.Trim().Length > 0) ||
				(this._query != null && this._query.Trim().Length > 0)
				);
		}

		#endregion
	}
*/

	/// <include file='TaxSvc.Doc.xml' path='adapter/CancelTaxRequest/class/*' />
	[Guid("55C0F34C-81F3-4cbc-B08C-65B82FCD6476")]
	[ComVisible(true)]
	public class CancelTaxRequest : BaseRequest
	{
		string _companyCode;
		DocumentType _docType;
		string _docCode;
		CancelCode _cancelCode;

		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="Constructor"]/*' />
		public CancelTaxRequest()
		{
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/common/members[@name="CompanyCode"]/*' />
		[DispId(31)]
		public string CompanyCode
		{
			get
			{
				return _companyCode;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_companyCode = null;
				}
				else
				{
					_companyCode = value.Trim();
				}
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/common/members[@name="DocType"]/*' />
		[DispId(32)]
		public DocumentType DocType
		{
			get
			{
				return _docType;
			}
			set
			{
				Utilities.VerifyEnum(typeof(DocumentType), value);
				_docType = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/common/members[@name="DocCode"]/*' />
		[DispId(33)]
		public string DocCode
		{
			get
			{
				return _docCode;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_docCode= null;
				}
				else
				{
					_docCode = value.Trim();
				}
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/CancelTaxRequest/members[@name="CancelCode"]/*' />
		[DispId(34)]
		public CancelCode CancelCode
		{
			get
			{
				return _cancelCode;
			}
			set
			{
				Utilities.VerifyEnum(typeof(TaxService.CancelCode), value);
				_cancelCode = value;
			}
		}

		#region Internal Members
		
		/// <summary>
		/// Loads a local CancelTaxRequest object into a web service copy of the same object.
		/// </summary>
		/// <param name="SvcRequest">The CancelTaxRequest object to be copied to.</param>
		internal void CopyTo(ProxyCancelTaxRequest SvcRequest)
		{
			SvcRequest.CancelCode = (ProxyCancelCode)_cancelCode;
			SvcRequest.DocType = (ProxyDocumentType)_docType;
			SvcRequest.CompanyCode = _companyCode;
			SvcRequest.DocCode = _docCode;
		}

        internal override bool IsValid(out string message)
        {
            //bool isValid = (
            //                   (_companyCode != null && _companyCode.Trim().Length > 0) &&
            //                   (_docCode != null && _docCode.Trim().Length > 0)
            //               );

            message = string.Empty;

            //if (!isValid)
            //{
            //    message = "Required fields for CancelTaxRequest are [CompanyCode, DocCode, and DocType]. ";
            //}
            //return isValid;
            return true;
		}

		#endregion
	}

	/// <include file='TaxSvc.Doc.xml' path='adapter/CommitTaxRequest/class/*' />
	[Guid("678D112B-8A29-4091-BC7E-B63BD6668391")]
	[ComVisible(true)]
	public class CommitTaxRequest : BaseRequest
	{
		string _companyCode;
		DocumentType _docType;
		string _docCode;	
        //Update Note : Added for HA
        string _newDocCode;

		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="Constructor"]/*' />
		public CommitTaxRequest()
		{
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/common/members[@name="CompanyCode"]/*' />
		[DispId(31)]
		public string CompanyCode
		{
			get
			{
				return _companyCode;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_companyCode = null;
				}
				else
				{
					_companyCode = value.Trim();
				}
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/common/members[@name="DocType"]/*' />
		[DispId(32)]
		public DocumentType DocType
		{
			get
			{
				return _docType;
			}
			set
			{
				Utilities.VerifyEnum(typeof(DocumentType), value);
				_docType = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/common/members[@name="DocCode"]/*' />
		[DispId(33)]
		public string DocCode
		{
			get
			{
				return _docCode;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_docCode = null;
				}
				else
				{
					_docCode = value.Trim();
				}
			}
		}	

        /// <include file='TaxSvc.Doc.xml' path='adapter/CommitTaxRequest/members[@name="NewDocCode"]/*' />
        [DispId(34)]
        public string NewDocCode
        {
            get
            {
                return _newDocCode;
            }
            set
            {
                _newDocCode = value;
            }
        }

		#region Internal Members
		
		/// <summary>
		/// Loads a local CommitTaxRequest object into a web service copy of the same object.
		/// </summary>
		/// <param name="SvcRequest">The CommitTaxRequest object to be copied to.</param>
		internal void CopyTo(ProxyCommitTaxRequest SvcRequest)
		{
			SvcRequest.CompanyCode = _companyCode;
			SvcRequest.DocType = (ProxyDocumentType)_docType;
			SvcRequest.DocCode = _docCode;			
            SvcRequest.NewDocCode = _newDocCode;
		}

        internal override bool IsValid(out string message)
        {
            //bool isValid = (
            //                   (_companyCode != null && _companyCode.Trim().Length > 0) &&
            //                   (_docCode != null && _docCode.Trim().Length > 0)
            //               );

            message = string.Empty;

            //if (!isValid)
            //{
            //    message = "Required fields for CommitTaxRequest are [CompanyCode, DocCode, and DocType]. ";
            //}

            //return isValid;
            return true;
		}

		#endregion
	}

	/// <include file='TaxSvc.Doc.xml' path='adapter/PostTaxRequest/class/*' />
	[Guid("679D1B84-33CA-4a09-9D3A-7C39148EB224")]
	[ComVisible(true)]
	public class PostTaxRequest : BaseRequest
	{
		string _companyCode;
		DocumentType _docType;
		string _docCode;
		DateTime _docDate;
		Decimal _totalAmount;
		Decimal _totalTax;

		//Updated for 4.17
		bool _commit;
		//Update Note : Added for HA
		string _newDocCode;


		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="Constructor"]/*' />
		public PostTaxRequest()
		{
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/common/members[@name="CompanyCode"]/*' />
		[DispId(31)]
		public string CompanyCode
		{
			get
			{
				return _companyCode;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_companyCode = null;
				}
				else
				{
					_companyCode = value.Trim();
				}
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/common/members[@name="DocType"]/*' />
		[DispId(32)]
		public DocumentType DocType
		{
			get
			{
				return _docType;
			}
			set
			{
				Utilities.VerifyEnum(typeof(DocumentType), value);
				_docType = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/common/members[@name="DocCode"]/*' />
		[DispId(33)]
		public string DocCode
		{
			get
			{
				return _docCode;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_docCode = null;
				}
				else
				{
					_docCode = value.Trim();
				}
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/PostTaxRequest/members[@name="DocDate"]/*' />
		[DispId(34)]
		public DateTime DocDate
		{
			get
			{
				return _docDate;
			}
			set
			{
				//Utilities.VerifyDate(value);
				_docDate = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/PostTaxRequest/members[@name="TotalAmount"]/*' />
		[DispId(35)]
		public Decimal TotalAmount
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _totalAmount;
			}
			[param: MarshalAs(UnmanagedType.Currency)]
			set
			{
				_totalAmount = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/PostTaxRequest/members[@name="TotalTax"]/*' />
		[DispId(36)]
		public Decimal TotalTax
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _totalTax;
			}
			[param: MarshalAs(UnmanagedType.Currency)]
			set
			{
				_totalTax = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/PostTaxRequest/members[@name="Commit"]/*' />
		[DispId(38)]
		public bool Commit
		{
			get
			{
				return _commit;
			}			
			set
			{
				_commit = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/PostTaxRequest/members[@name="NewDocCode"]/*' />
		[DispId(39)]
		public string NewDocCode
		{
			get
			{
				return _newDocCode;
			}			
			set
			{
				_newDocCode = value;
			}
		}


		#region Internal Members
		
		/// <summary>
		/// Loads a local PostTaxRequest object into a web service copy of the same object.
		/// </summary>
		/// <param name="SvcRequest">The PostTaxRequest object to be copied to.</param>
		internal void CopyTo(ProxyPostTaxRequest SvcRequest)
		{
			SvcRequest.CompanyCode = _companyCode;
			SvcRequest.DocType = (ProxyDocumentType)_docType;
			SvcRequest.DocCode = _docCode;
			SvcRequest.DocDate = _docDate;
			SvcRequest.TotalAmount = _totalAmount;
			SvcRequest.TotalTax = _totalTax;
			SvcRequest.Commit = _commit;
			SvcRequest.NewDocCode = _newDocCode;
		}

        internal override bool IsValid(out string message)
        {
            //bool isValid = (
            //                   (_companyCode != null && _companyCode.Trim().Length > 0) &&
            //                   (_docCode != null && _docCode.Trim().Length > 0)
            //               );

            message = string.Empty;

            //if (!isValid)
            //{
            //    message = "Required fields for PostTaxRequest are [CompanyCode, DocCode, and DocType]. ";
            //}
            //return isValid;
            return true;
		}

		#endregion
	}

	/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxHistoryRequest/class/*' />
	[Guid("55685354-441E-49db-B40C-C0BE91199F14")]
	[ComVisible(true)]
	public class GetTaxHistoryRequest : BaseRequest
	{
		string _companyCode;
		DocumentType _docType;
		string _docCode;
        DetailLevel _detailLevel = DetailLevel.Document;

		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="Constructor"]/*' />
		public GetTaxHistoryRequest()
		{
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/common/members[@name="CompanyCode"]/*' />
		[DispId(31)]
		public string CompanyCode
		{
			get
			{
				return _companyCode;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_companyCode = null;
				}
				else
				{
					_companyCode = value.Trim();
				}
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/common/members[@name="DocType"]/*' />
		[DispId(32)]
		public DocumentType DocType
		{
			get
			{
				return _docType;
			}
			set
			{
				Utilities.VerifyEnum(typeof(DocumentType), value);
				_docType = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/common/members[@name="DocCode"]/*' />
		[DispId(33)]
		public string DocCode
		{
			get
			{
				return _docCode;
			}
			set
			{
				if (value == null || value.Trim() == "")
				{
					_docCode = null;
				}
				else
				{
					_docCode = value.Trim();
				}
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxHistoryRequest/members[@name="DetailLevel"]/*' />
		[DispId(34)]
		public DetailLevel DetailLevel
		{
			get
			{
				return _detailLevel;
			}
			set
			{
				Utilities.VerifyEnum(typeof(TaxService.DetailLevel), value);
				_detailLevel = value;
			}
		}

		#region Internal Members
		
		/// <summary>
		/// Loads a local GetTaxHistoryRequest object into a web service copy of the same object.
		/// </summary>
		/// <param name="SvcRequest">The GetTaxHistoryRequest object to be copied to.</param>
		internal void CopyTo(ProxyGetTaxHistoryRequest SvcRequest)
		{
			SvcRequest.CompanyCode = _companyCode;
			SvcRequest.DocType = (ProxyDocumentType)_docType;
			SvcRequest.DetailLevel = (ProxyDetailLevel)_detailLevel;
			SvcRequest.DocCode = _docCode;
		}

        internal override bool IsValid(out string message)
        {
            //bool isValid = (
            //                   (_companyCode != null && _companyCode.Trim().Length > 0) &&
            //                   (_docCode != null && _docCode.Trim().Length > 0)
            //               );

            message = string.Empty;

            //if (!isValid)
            //{
            //    message = "Required fields for GetTaxHistoryRequest are [CompanyCode, DocCode, and DocType]. ";
            //}
            //return isValid;
            return true;
		}

		#endregion
	}

    /// <include file='TaxSvc.Doc.xml' path='adapter/TaxRequests/class/*' />
    [Guid("94614744-20EB-42bf-98F5-70FC0BE40657")]
    [ComVisible(true)]
    public class TaxRequests : BaseArrayList
    {
        /// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal TaxRequests() { }

        /// <include file='TaxSvc.Doc.xml' path='adapter/TaxRequests/members[@name="Add"]/*' />
        [DispId(30)]
        public void Add(GetTaxRequest request)
        {
            //we hide the base member so that we can strongly type our parameter
            try
            {
                base.Add(request);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("request", "Cannot add a null request to the collection.");
            }
        }

        /// <include file='TaxSvc.Doc.xml' path='adapter/TaxRequests/members[@name="Add"]/*' />
        [DispId(32)]
        public void Add(PostTaxRequest request)
        {
            //we hide the base member so that we can strongly type our parameter
            try
            {
                base.Add(request);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("request", "Cannot add a null request to the collection.");
            }
        }

        /// <include file='TaxSvc.Doc.xml' path='adapter/TaxRequests/members[@name="Add"]/*' />
        [DispId(33)]
        public void Add(CommitTaxRequest request)
        {
            //we hide the base member so that we can strongly type our parameter
            try
            {
                base.Add(request);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("request", "Cannot add a null request to the collection.");
            }
        }

        /// <include file='TaxSvc.Doc.xml' path='adapter/TaxRequests/members[@name="Add"]/*' />
        [DispId(34)]
        public void Add(CancelTaxRequest request)
        {
            //we hide the base member so that we can strongly type our parameter
            try
            {
                base.Add(request);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("request", "Cannot add a null request to the collection.");
            }
        }

        /// <include file='TaxSvc.Doc.xml' path='adapter/TaxRequests/members[@name="Add"]/*' />
        [DispId(35)]
        public void Add(AdjustTaxRequest request)
        {
            //we hide the base member so that we can strongly type our parameter
            try
            {
                base.Add(request);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("request", "Cannot add a null request to the collection.");
            }
        }

        /// <include file='TaxSvc.Doc.xml' path='adapter/TaxRequests/members[@name="Add"]/*' />
        [DispId(36)]
        public void Add(ApplyPaymentRequest request)
        {
            //we hide the base member so that we can strongly type our parameter
            try
            {
                base.Add(request);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("request", "Cannot add a null request to the collection.");
            }
        }

        /// <include file='TaxSvc.Doc.xml' path='adapter/TaxRequests/members[@name="Add"]/*' />
        [DispId(37)]
        public void Add(GetTaxHistoryRequest request)
        {
            //we hide the base member so that we can strongly type our parameter
            try
            {
                base.Add(request);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("request", "Cannot add a null request to the collection.");
            }
        }

        /// <include file='TaxSvc.Doc.xml' path='adapter/TaxRequests/members[@name="Add"]/*' />
        [DispId(38)]
        public void Add(ReconcileTaxHistoryRequest request)
        {
            //we hide the base member so that we can strongly type our parameter
            try
            {
                base.Add(request);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("request", "Cannot add a null request to the collection.");
            }
        }

        /// <include file='TaxSvc.Doc.xml' path='adapter/TaxRequests/members[@name="this"]/*' />
        [DispId(0)]
        public new BaseRequest this[int index]
        {
            get
            {
                //we hide the base member so that we can strongly type our returned object
                return (BaseRequest)base[index];
            }
        }

        /// <include file='TaxSvc.Doc.xml' path='adapter/TaxRequests/members[@name="Clear"]/*' />
        [DispId(31)]
        public new void Clear()
        {
            base.Clear();
        }
    }




	/// <include file='TaxSvc.Doc.xml' path='adapter/CancelTaxResult/class/*' />
	[Guid("0951227B-70C7-441d-A244-943065CA9A17")]
	[ComVisible(true)]
	public class CancelTaxResult : BaseResult
	{
		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal CancelTaxResult()
		{
		}

		#region Internal Members

		/// <summary>
		/// Load an empty local CancelTaxResult object from the data provided by the web service.
		/// </summary>
		/// <param name="SvcResult">The CancelTaxResult object provided by the web service.</param>
		internal void CopyFrom(ProxyCancelTaxResult SvcResult)
		{
			base.CopyFrom(SvcResult);
		}

		/// <summary>
		/// Creates a new CancelTaxResult based on a <see cref="BaseResult"/>.
		/// </summary>
		/// <param name="baseResult"></param>
		/// <returns></returns>
		internal static CancelTaxResult CastFromBaseResult(BaseResult baseResult)
		{
			CancelTaxResult result = new CancelTaxResult();
			result.CopyFrom(baseResult);
			return result;
		}

		#endregion

	}

	/// <include file='TaxSvc.Doc.xml' path='adapter/CommitTaxResult/class/*' />
	[Guid("2A390235-8A88-4007-B9D7-79BD9C89DC0D")]
	[ComVisible(true)]
	public class CommitTaxResult : BaseResult
	{
		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal CommitTaxResult()
		{
		}
        
		#region Internal Members

		/// <summary>
		/// Load an empty local CommitTaxResult object from the data provided by the web service.
		/// </summary>
		/// <param name="SvcResult">The CommitTaxResult object provided by the web service.</param>
		internal void CopyFrom(ProxyCommitTaxResult SvcResult)
		{
			base.CopyFrom(SvcResult);
		}

		/// <summary>
		/// Creates a new CommitTaxResult based on a <see cref="BaseResult"/>.
		/// </summary>
		/// <param name="baseResult"></param>
		/// <returns></returns>
		internal static CommitTaxResult CastFromBaseResult(BaseResult baseResult)
		{
			CommitTaxResult result = new CommitTaxResult();
			result.CopyFrom(baseResult);
			return result;
		}

		#endregion

	}


	/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxResult/class/*' />
	[Guid("3DBEAF92-0FD1-4dff-9C63-5A3320A98D75")]
	[ComVisible(true)]
	public class GetTaxResult : BaseResult
	{
		DocumentType _docType;
		string _docCode;
		DateTime _docDate;
		bool _reconciled;
		DocStatus _docStatus;
		DateTime _timestamp;
		Decimal _totalAmount;
		Decimal _totalTaxable;
		Decimal _totalDiscount;
		Decimal _totalExemption;
		Decimal _totalTax;


		TaxLines _taxLines = new TaxLines();


		//Update Note : Added for 4.16
		bool _locked;
		//Update Note : Added for 4.16
		/// <remarks/>
		int _adjustmentReason;
		//Update Note : Added for 4.16
		/// <remarks/>
		string _adjustmentDescription;
		//Update Note : Added for 4.16
		/// <remarks/>
		int _version;
		//Update Note : Added for 4.16
		/// <remarks/>		
		System.DateTime _taxDate;	

		//Update Note : Added for 5.0
		//TaxDetails _taxSummary = new TaxDetails();
		TaxSummary _taxSummary = new TaxSummary();

		//Update Note : Added for 5.1
		Decimal _totalTaxCalculated;


		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal GetTaxResult()
		{
		}
        
		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxResult/members[@name="DocType"]/*' />
		[DispId(31)]
		public DocumentType DocType
		{
			get
			{
				return _docType;
			}
		}
        
		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxResult/members[@name="DocCode"]/*' />
		[DispId(32)]
		public string DocCode
		{
			get
			{
				return _docCode;
			}
		}
        
		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxResult/members[@name="DocDate"]/*' />
		[DispId(33)]
		public System.DateTime DocDate
		{
			get
			{
				return _docDate;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxResult/members[@name="DocStatus"]/*' />
		[DispId(34)]
		public DocStatus DocStatus
		{
			get
			{
				return _docStatus;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxResult/members[@name="Reconciled"]/*' />
		[DispId(35)]
		public bool Reconciled
		{
			get
			{
				return _reconciled;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxResult/members[@name="Timestamp"]/*' />
		[DispId(36)]
		public DateTime Timestamp
		{
			get
			{
				return _timestamp;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxResult/members[@name="TotalAmount"]/*' />
		[DispId(37)]
		public Decimal TotalAmount
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _totalAmount;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxResult/members[@name="TotalTaxable"]/*' />
		[DispId(38)]
		public Decimal TotalTaxable
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _totalTaxable;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxResult/members[@name="TotalTax"]/*' />
		[DispId(39)]
		public Decimal TotalTax
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _totalTax;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxResult/members[@name="TaxLines"]/*' />
		[DispId(40)]
		public TaxLines TaxLines
		{
			get
			{
				return _taxLines;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxResult/members[@name="TotalDiscount"]/*' />
		[DispId(41)]
		public Decimal TotalDiscount
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _totalDiscount;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxResult/members[@name="TotalExemption"]/*' />
		[DispId(42)]
		public Decimal TotalExemption
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _totalExemption;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxResult/members[@name="Locked"]/*' />
		[DispId(44)]
		public bool Locked
		{			
			get
			{
				return _locked;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxResult/members[@name="AdjustmentReason"]/*' />
		[DispId(45)]
		public int AdjustmentReason
		{			
			get
			{
				return _adjustmentReason;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxResult/members[@name="AdjustmentDescription"]/*' />
		[DispId(46)]
		public string AdjustmentDescription
		{			
			get
			{
				return _adjustmentDescription;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxResult/members[@name="Version"]/*' />
		[DispId(47)]
		public int Version
		{			
			get
			{
				return _version;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxResult/members[@name="TaxDate"]/*' />
		[DispId(48)]
		public System.DateTime TaxDate
		{			
			get
			{
				return _taxDate;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxResult/members[@name="TaxSummary"]/*' />
		[DispId(49)]
		public TaxSummary TaxSummary
		{
			get
			{
				return _taxSummary;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxResult/members[@name="TotalTaxCalculated"]/*' />
		[DispId(50)]
		public Decimal TotalTaxCalculated
		{
			[return: MarshalAs(UnmanagedType.Currency)]
			get
			{
				return _totalTaxCalculated;
			}
		}

		#region Internal Members

		/// <summary>
		/// Load an empty local GetTaxResult object from the data provided by the web service.
		/// </summary>
		/// <param name="SvcResult">The GetTaxResult object provided by the web service.</param>
		internal void CopyFrom(ProxyGetTaxResult SvcResult)
		{
			_docType = (DocumentType)SvcResult.DocType;
			_docCode = SvcResult.DocCode;
			_docDate = SvcResult.DocDate;
			_docStatus = (DocStatus)SvcResult.DocStatus;
			_reconciled = SvcResult.Reconciled;
			//Convet the TimeStamp[modified time on serverside for the doc] in to LocalTime
			_timestamp = SvcResult.Timestamp.ToLocalTime();
			_totalAmount = SvcResult.TotalAmount;
			_totalTaxable = SvcResult.TotalTaxable;
			_totalTax = SvcResult.TotalTax;
			_totalDiscount = SvcResult.TotalDiscount;
			_totalExemption = SvcResult.TotalExemption;			
			_locked = SvcResult.Locked;
			_adjustmentReason = SvcResult.AdjustmentReason;
			_adjustmentDescription = SvcResult.AdjustmentDescription;
			_version = SvcResult.Version;
			_taxDate = SvcResult.TaxDate;
			//Update Note : Added for 5.1
			_totalTaxCalculated = SvcResult.TotalTaxCalculated;

			base.CopyFrom(SvcResult);

			if (SvcResult.TaxLines != null && SvcResult.TaxLines.Length>0) 
			{
				for (int idx = 0; idx < SvcResult.TaxLines.Length; idx++)
				{
					TaxLine taxLine = new TaxLine();
					taxLine.CopyFrom(SvcResult.TaxLines[idx]);
					_taxLines.Add(taxLine);
				}
			}

			//Update Note : Added for 5.0
			if (SvcResult.TaxSummary.Length>0) 
			{
				for (int idx = 0; idx < SvcResult.TaxSummary.Length; idx++)
				{
					TaxDetail taxDetail = new TaxDetail();
					taxDetail.CopyFrom(SvcResult.TaxSummary[idx]);
					_taxSummary.Add(taxDetail);
				}
			}
		}

		/// <summary>
		/// Creates a new GetTaxResult based on a <see cref="BaseResult"/>.
		/// </summary>
		/// <param name="baseResult"></param>
		/// <returns></returns>
		internal static GetTaxResult CastFromBaseResult(BaseResult baseResult)
		{
			GetTaxResult result = new GetTaxResult();
			result.CopyFrom(baseResult);
			return result;
		}

		#endregion

	}

	/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxResults/class/*' />
	[Guid("77B2FFC2-AC46-4cc8-98FF-3C569D087127")]
	[ComVisible(true)]
	public class GetTaxResults : ReadOnlyArrayList
	{
		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal GetTaxResults() {}

		/// <include file='TaxSvc.Doc.xml' path='adapter/collection/members[@name="this"]/*' />
		[DispId(0)]
		public new GetTaxResult this[int index]
		{
			get
			{
				return (GetTaxResult)base[index];
			}
		}

	}

	/// <include file='TaxSvc.Doc.xml' path='adapter/ReconcileTaxHistoryResult/class/*' />
	[Guid("6A85C8A1-1B9F-44b2-8188-E56C09CD4F58")]
	[ComVisible(true)]
	public class ReconcileTaxHistoryResult : BaseResult
	{
		GetTaxResults _getTaxResults = new GetTaxResults();
		string _lastDocCode;
        int _recordCount;

		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal ReconcileTaxHistoryResult()
		{
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/ReconcileTaxHistoryResult/members[@name="GetTaxResults"]/*' />
		[DispId(30)]
		public GetTaxResults GetTaxResults
		{
			get
			{
				return _getTaxResults;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/ReconcileTaxHistoryResult/members[@name="LastDocCode"]/*' />
		[DispId(31)]
		public string LastDocCode
		{
			get
			{
				return _lastDocCode;
			}
        }

        /// <include file='TaxSvc.Doc.xml' path='adapter/ReconcileTaxHistoryResult/members[@name="RecordCount"]/*' />
        [DispId(32)]
        public int RecordCount
        {
            get
            {
                return _recordCount;
            }
        }

		#region Internal Members

		/// <summary>
		/// Load an empty local ReconcileTaxHistoryResult object from the data provided by the web service.
		/// </summary>
		/// <param name="SvcResult">The ReconcileTaxHistoryResult object provided by the web service.</param>
		internal void CopyFrom(ProxyReconcileTaxHistoryResult SvcResult)
		{
			_lastDocCode = SvcResult.LastDocCode;
            _recordCount = SvcResult.RecordCount;
			base.CopyFrom(SvcResult);

			for (int idx = 0; idx < SvcResult.GetTaxResults.Length; idx++)
			{
				GetTaxResult getTaxResult = new GetTaxResult();
				getTaxResult.CopyFrom(SvcResult.GetTaxResults[idx]);
				_getTaxResults.Add(getTaxResult);
			}
		}

		/// <summary>
		/// Creates a new ReconcileTaxHistoryResult based on a <see cref="BaseResult"/>.
		/// </summary>
		/// <param name="baseResult"></param>
		/// <returns></returns>
		internal static ReconcileTaxHistoryResult CastFromBaseResult(BaseResult baseResult)
		{
			ReconcileTaxHistoryResult result = new ReconcileTaxHistoryResult();
			result.CopyFrom(baseResult);
			return result;
		}

		#endregion

	}

	/// <include file='TaxSvc.Doc.xml' path='adapter/PostTaxResult/class/*' />
	[Guid("E345D911-30A2-41c9-9CD2-1DA9697AE938")]
	[ComVisible(true)]
	public class PostTaxResult : BaseResult
	{

		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal PostTaxResult()
		{
		}

		#region Internal Members

		/// <summary>
		/// Load an empty local PostTaxResult object from the data provided by the web service.
		/// </summary>
		/// <param name="SvcResult">The PostTaxResult object provided by the web service.</param>
		internal void CopyFrom(ProxyPostTaxResult SvcResult)
		{
			base.CopyFrom(SvcResult);
		}

		/// <summary>
		/// Creates a new PostTaxResult based on a <see cref="BaseResult"/>.
		/// </summary>
		/// <param name="baseResult"></param>
		/// <returns></returns>
		internal static PostTaxResult CastFromBaseResult(BaseResult baseResult)
		{
			PostTaxResult result = new PostTaxResult();
			result.CopyFrom(baseResult);
			return result;
		}

		#endregion

	}

	/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxHistoryResult/class/*' />
	[Guid("30E7392D-D0A9-44c5-929E-F92B71E788CD")]
	[ComVisible(true)]
	public class GetTaxHistoryResult : BaseResult
	{
		GetTaxRequest _getTaxRequest = new GetTaxRequest();
		GetTaxResult _getTaxResult = new GetTaxResult();

		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal GetTaxHistoryResult()
		{
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxHistoryResult/members[@name="GetTaxRequest"]/*' />
		[DispId(30)]
		public GetTaxRequest GetTaxRequest
		{
			get
			{
				return _getTaxRequest;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/GetTaxHistoryResult/members[@name="GetTaxResult"]/*' />
		[DispId(31)]
		public GetTaxResult GetTaxResult
		{
			get
			{
				return _getTaxResult;
			}
		}

		#region Internal Members

		/// <summary>
		/// Load an empty local GetTaxHistoryResult object from the data provided by the web service.
		/// </summary>
		/// <param name="SvcResult">The GetTaxHistoryResult object provided by the web service.</param>
		internal void CopyFrom(ProxyGetTaxHistoryResult SvcResult)
		{
			base.CopyFrom(SvcResult);
			_getTaxRequest.CopyFrom(SvcResult.GetTaxRequest);
			_getTaxResult.CopyFrom(SvcResult.GetTaxResult);
		}

		/// <summary>
		/// Creates a new GetTaxHistoryResult based on a <see cref="BaseResult"/>.
		/// </summary>
		/// <param name="baseResult"></param>
		/// <returns></returns>
		internal static GetTaxHistoryResult CastFromBaseResult(BaseResult baseResult)
		{
			GetTaxHistoryResult result = new GetTaxHistoryResult();
			result.CopyFrom(baseResult);
			return result;
		}

		#endregion

	}


	/// <include file='TaxSvc.Doc.xml' path='adapter/AdjustTaxRequest/class/*' />
	[Guid("aeec647e-d433-4750-9ed8-131b0ba1bd79")]
	[ComVisible(true)]
	public class AdjustTaxRequest : BaseRequest
	{
		int _adjustmentReason;
		string _adjustmentDescription;
		GetTaxRequest _getTaxRequest;

		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		public AdjustTaxRequest()
		{
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/AdjustTaxRequest/members[@name="AdjustmentReason"]/*' />
		[DispId(30)]
		public int AdjustmentReason
		{
			get
			{
				return _adjustmentReason;
			}
			set
			{
				_adjustmentReason = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/AdjustTaxRequest/members[@name="AdjustmentDescription"]/*' />
		[DispId(31)]
		public string AdjustmentDescription
		{
			get
			{
				return _adjustmentDescription;
			}
			set
			{
				_adjustmentDescription = value;
			}

		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/AdjustTaxRequest/members[@name="GetTaxRequest"]/*' />
		[DispId(32)]
		public GetTaxRequest GetTaxRequest
		{
			get
			{
				return _getTaxRequest;
			}
			set
			{
				_getTaxRequest = value;
			}
		}

		#region Internal Members

		/// <summary>
		/// Load an empty local AdjustTaxRequest object from the data provided by the web service.
		/// </summary>
		/// <param name="SvcRequest">The AdjustTaxRequest object provided by the web service.</param>
		internal void CopyFrom(ProxyAdjustTaxRequest SvcRequest)
		{
			
			_adjustmentReason = SvcRequest.AdjustmentReason;
			_adjustmentDescription = SvcRequest.AdjustmentDescription;
			_getTaxRequest.CopyFrom(SvcRequest.GetTaxRequest);			
		}

		/// <summary>
		/// Load an empty local AdjustTaxRequest object from the data provided by the web service.
		/// </summary>
		/// <param name="SvcRequest">The AdjustTaxRequest object provided by the web service.</param>
		internal void CopyTo(ProxyAdjustTaxRequest SvcRequest)
		{
			//SvcRequest.GetTaxRequest = new ProxyAdjustTaxRequest();
			ProxyGetTaxRequest request = new ProxyGetTaxRequest();	
			SvcRequest.AdjustmentReason = _adjustmentReason;
			SvcRequest.AdjustmentDescription = _adjustmentDescription;;
			//SvcRequest.GetTaxRequest = _getTaxRequest;
			_getTaxRequest.CopyTo(request);//SvcRequest.GetTaxRequest);
			SvcRequest.GetTaxRequest = request;
		}

        internal override bool IsValid(out string message)
        {
            message = string.Empty;
            return true;
		}

		#endregion

	}
	
	/// <include file='TaxSvc.Doc.xml' path='adapter/AdjustTaxResult/class/*' />
	[Guid("9a18523d-ca06-4ccc-bad0-c1663133a267")]
	[ComVisible(true)]
	public class AdjustTaxResult : GetTaxResult
	{
		
		//GetTaxResult _getTaxResult;

		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
        internal AdjustTaxResult()
		{
		}
		internal new static AdjustTaxResult CastFromBaseResult(BaseResult baseResult)
		{
			AdjustTaxResult result = new AdjustTaxResult();
			result.CopyFrom(baseResult);
			return result;
		}

	}	

	//Update Note : Added for 5.1
	/// <include file='TaxSvc.Doc.xml' path='adapter/ApplyPaymentRequest/class/*' />
	[Guid("8ccdfbc9-a6c8-4415-ba93-778bca1f05ef")]
	[ComVisible(true)]
	public class ApplyPaymentRequest : BaseRequest
	{
		string _companyCode;
		DocumentType _docType;
		string _docCode;
		DateTime _paymentDate;

		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="Constructor"]/*' />
		public ApplyPaymentRequest()
		{
			_companyCode = string.Empty;
			_docType = DocumentType.SalesOrder;
			_docCode = string.Empty;
			_paymentDate = DateUtil.MinDate;
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/common/members[@name="CompanyCode"]/*' />
		[DispId(31)]
		public string CompanyCode
		{
			get
			{
				return _companyCode;
			}
			set
			{
				if (value == null)
				{
					_companyCode = "";
				}
				else
				{
					_companyCode = value.Trim();
				}
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/common/members[@name="DocType"]/*' />
		[DispId(32)]
		public DocumentType DocType
		{
			get
			{
				return _docType;
			}
			set
			{
				Utilities.VerifyEnum(typeof(DocumentType), value);
				_docType = value;
			}
		}

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxSvc/common/members[@name="DocCode"]/*' />
		[DispId(33)]
		public string DocCode
		{
			get
			{
				return _docCode;
			}
			set
			{
				if (value == null)
				{
					_docCode = "";
				}
				else
				{
					_docCode = value.Trim();
				}
			}
		}			

		/// <include file='TaxSvc.Doc.xml' path='adapter/ApplyPaymentRequest/members[@name="PaymentDate"]/*' />
		[DispId(34)]
		public DateTime PaymentDate
		{
			get
			{
				return _paymentDate;
			}
			set
			{
				_paymentDate = value;
			}
		}

		#region Internal Members
		
		/// <summary>
		/// Loads a local ApplyPaymentRequest object into a web service copy of the same object.
		/// </summary>
		/// <param name="SvcRequest">The ApplyPaymentRequest object to be copied to.</param>
		internal void CopyTo(ProxyApplyPaymentRequest SvcRequest)
		{
			SvcRequest.CompanyCode = _companyCode;
			SvcRequest.DocType = (ProxyDocumentType)_docType;
			SvcRequest.DocCode = _docCode;	
			SvcRequest.PaymentDate = _paymentDate;
		}

        internal override bool IsValid(out string message)
        {
            //bool isValid = (
            //                   (_companyCode != null && _companyCode.Trim().Length > 0) &&
            //                   (_docCode != null && _docCode.Trim().Length > 0)
            //               );

            message = string.Empty;

            //if (!isValid)
            //{
            //    message = "Required fields for ApplyPaymentRequest are [CompanyCode, DocCode, and DocType]. ";
            //}

            //return isValid;
            return true;
		}

		#endregion
	}	


	//Update Note : Added for 5.1
	/// <include file='TaxSvc.Doc.xml' path='adapter/ApplyPaymentResult/class/*' />
	[Guid("070d90c0-0400-456c-bd27-9d15ff180079")]
	[ComVisible(true)]
	public class ApplyPaymentResult : BaseResult
	{

		/// <include file='TaxSvc.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal ApplyPaymentResult()
		{
		}

		#region Internal Members

		/// <summary>
		/// Load an empty local ApplyPaymentResult object from the data provided by the web service.
		/// </summary>
		/// <param name="SvcResult">The ApplyPaymentResult object provided by the web service.</param>
		internal void CopyFrom(ProxyApplyPaymentResult SvcResult)
		{
			base.CopyFrom(SvcResult);
		}

		/// <summary>
		/// Creates a new ApplyPaymentResult based on a <see cref="BaseResult"/>.
		/// </summary>
		/// <param name="baseResult"></param>
		/// <returns></returns>
		internal static ApplyPaymentResult CastFromBaseResult(BaseResult baseResult)
		{
			ApplyPaymentResult result = new ApplyPaymentResult();
			result.CopyFrom(baseResult);
			return result;
		}

		#endregion

	}

	#region Enums

	/// <include file='TaxSvc.Doc.xml' path='adapter/DetailLevel/enum/*' />
	[Guid("6D625CCB-9A61-40d4-A8E5-9A1F13FDBD3D")]
	[ComVisible(true)]
	public enum DetailLevel 
	{
		/// <include file='TaxSvc.Doc.xml' path='adapter/DetailLevel/members[@name="Summary"]/*' />
		Summary = 0,

		/// <include file='TaxSvc.Doc.xml' path='adapter/DetailLevel/members[@name="Document"]/*' />
		Document = 1,

		/// <include file='TaxSvc.Doc.xml' path='adapter/DetailLevel/members[@name="Line"]/*' />
		Line = 2,

		/// <include file='TaxSvc.Doc.xml' path='adapter/DetailLevel/members[@name="Tax"]/*' />
		Tax = 3,

		/// <include file='TaxSvc.Doc.xml' path='adapter/DetailLevel/members[@name="Diagnostic"]/*' />
		Diagnostic = 4,
}
 
	/// <include file='TaxSvc.Doc.xml' path='adapter/DocumentType/enum/*' />
	[Guid("AC8D836C-6061-4405-BAE9-A415D99F7B37")]
	[ComVisible(true)]
	public enum DocumentType 
	{
        /// <include file='TaxSvc.Doc.xml' path='adapter/DocumentType/members[@name="Any"]/*' />
        Any = -1,

		/// <include file='TaxSvc.Doc.xml' path='adapter/DocumentType/members[@name="SalesOrder"]/*' />
		SalesOrder = 0,
        
		/// <include file='TaxSvc.Doc.xml' path='adapter/DocumentType/members[@name="SalesInvoice"]/*' />
		SalesInvoice = 1,
        
		/// <include file='TaxSvc.Doc.xml' path='adapter/DocumentType/members[@name="PurchaseOrder"]/*' />
		PurchaseOrder = 2,
        
		/// <include file='TaxSvc.Doc.xml' path='adapter/DocumentType/members[@name="PurchaseInvoice"]/*' />
		PurchaseInvoice = 3,
	        
		/// <include file='TaxSvc.Doc.xml' path='adapter/DocumentType/members[@name="ReturnOrder"]/*' />
		ReturnOrder = 4,
        
		/// <include file='TaxSvc.Doc.xml' path='adapter/DocumentType/members[@name="ReturnInvoice"]/*' />
		ReturnInvoice = 5,

        /// <include file='TaxSvc.Doc.xml' path='adapter/DocumentType/members[@name="InventoryTransferOrder"]/*' />
        InventoryTransferOrder = 6,

        /// <include file='TaxSvc.Doc.xml' path='adapter/DocumentType/members[@name="InventoryTransferInvoice"]/*' />
        InventoryTransferInvoice = 7
	}

	/// <include file='TaxSvc.Doc.xml' path='adapter/CancelCode/enum/*' />
	[Guid("CAD5511B-459C-410f-9F56-843319EBCE69")]
	[ComVisible(true)]
	public enum CancelCode 
	{
		/// <include file='TaxSvc.Doc.xml' path='adapter/CancelCode/members[@name="Unspecified"]/*' />
		Unspecified = 0,

		/// <include file='TaxSvc.Doc.xml' path='adapter/CancelCode/members[@name="PostFailed"]/*' />
		PostFailed = 1,

		/// <include file='TaxSvc.Doc.xml' path='adapter/CancelCode/members[@name="DocDeleted"]/*' />
		DocDeleted = 2,
		
		/// <include file='TaxSvc.Doc.xml' path='adapter/CancelCode/members[@name="DocVoided"]/*' />
		DocVoided = 3,

		/// <include file='TaxSvc.Doc.xml' path='adapter/CancelCode/members[@name="AdjustmentCancelled"]/*' />
		//Update Note : Added for 4.16
		AdjustmentCancelled = 4
	}
    
	/// <include file='TaxSvc.Doc.xml' path='adapter/JurisdictionType/enum/*' />
	[Guid("1CE2AC91-F931-4b94-9A52-36968064696D")]
	[ComVisible(true)]
	public enum JurisdictionType 
	{
		/// <include file='TaxSvc.Doc.xml' path='adapter/JurisdictionType/members[@name="Country"]/*' />
		Country = 0,

		/// <include file='TaxSvc.Doc.xml' path='adapter/JurisdictionType/members[@name="Composite"]/*' />
		Composite = 0,

		/// <include file='TaxSvc.Doc.xml' path='adapter/JurisdictionType/members[@name="State"]/*' />
		State = 1,

		/// <include file='TaxSvc.Doc.xml' path='adapter/JurisdictionType/members[@name="County"]/*' />
		County = 2,

		/// <include file='TaxSvc.Doc.xml' path='adapter/JurisdictionType/members[@name="City"]/*' />
		City = 3,

		/// <include file='TaxSvc.Doc.xml' path='adapter/JurisdictionType/members[@name="Special"]/*' />
		Special = 4
	}
    
	/// <include file='TaxSvc.Doc.xml' path='adapter/DocStatus/enum/*' />
	[Guid("8018DA60-08AA-4468-81B2-F79CBF58A197")]
	[ComVisible(true)]
	public enum DocStatus 
	{
		/// <include file='TaxSvc.Doc.xml' path='adapter/DocStatus/members[@name="Temporary"]/*' />
		Temporary = 0,

		/// <include file='TaxSvc.Doc.xml' path='adapter/DocStatus/members[@name="Saved"]/*' />
		Saved = 1,

		/// <include file='TaxSvc.Doc.xml' path='adapter/DocStatus/members[@name="Posted"]/*' />
		Posted = 2,

		/// <include file='TaxSvc.Doc.xml' path='adapter/DocStatus/members[@name="Committed"]/*' />
		Committed = 3,

		/// <include file='TaxSvc.Doc.xml' path='adapter/DocStatus/members[@name="Cancelled"]/*' />
		Cancelled = 4,

		//Update Note : Added for 4.16
		/// <remarks/>
		/// <include file='TaxSvc.Doc.xml' path='adapter/DocStatus/members[@name="Adjusted"]/*' />
		Adjusted = 5,

		/// <include file='TaxSvc.Doc.xml' path='adapter/DocStatus/members[@name="Any"]/*' />
		Any = -1
	}
       
	/// <include file='TaxSvc.Doc.xml' path='adapter/TaxType/enum/*' />
	[Guid("2B41D1E0-8D2E-45d0-A175-C827031989FD")]
	[ComVisible(true)]
	public enum TaxType 
	{
        /// <include file='TaxSvc.Doc.xml' path='adapter/TaxType/members[@name="None"]/*' />
        None = 0,

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxType/members[@name="Sales"]/*' />
		Sales = 1,
        
		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxType/members[@name="Use"]/*' />
		Use = 2,

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxType/members[@name="ConsumerUse"]/*' />
		ConsumerUse = 3,

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxType/members[@name="Output"]/*' />
		Output = 4,
		
		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxType/members[@name="Input"]/*' />
		Input = 5,
        
		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxType/members[@name="Nonrecoverable"]/*' />
		Nonrecoverable = 6,

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxType/members[@name="Fee"]/*' />
		Fee = 7,

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxType/members[@name="Rental"]/*' />
        Rental = 8,

        /// <include file='TaxSvc.Doc.xml' path='adapter/TaxType/members[@name="Excise"]/*' />
        Excise = 9
        

	}
       
	/// <include file='TaxSvc.Doc.xml' path='adapter/BoundaryLevel/enum/*' />
	[Guid("20F3CC94-A299-4480-B3DB-02EF5BCEBD3F")]
	[ComVisible(true)]
	public enum BoundaryLevel 
	{
        
		/// <include file='TaxSvc.Doc.xml' path='adapter/BoundaryLevel/members[@name="Address"]/*' />
		Address = 0,
        
		/// <include file='TaxSvc.Doc.xml' path='adapter/BoundaryLevel/members[@name="Zip9"]/*' />
		Zip9 = 1,

		/// <include file='TaxSvc.Doc.xml' path='adapter/BoundaryLevel/members[@name="Zip5"]/*' />
		Zip5 = 2
	}

	//Update Note : Added for 5.0
	/// <include file='TaxSvc.Doc.xml' path='adapter/TaxOverrideType/enum/*' />
	[Guid("946854ef-8a17-48e5-834a-25f31c59ca98")]
	[ComVisible(true)]
	public enum TaxOverrideType 
	{

        	/// <include file='TaxSvc.Doc.xml' path='adapter/TaxOverrideType/members[@name="None"]/*' />
		None = 0,
        
		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxOverrideType/members[@name="TaxAmount"]/*' />
		TaxAmount = 1,
        
		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxOverrideType/members[@name="Exemption"]/*' />
		Exemption = 2,

		/// <include file='TaxSvc.Doc.xml' path='adapter/TaxOverrideType/members[@name="TaxDate"]/*' />
		TaxDate = 3,

        /// <include file='TaxSvc.Doc.xml' path='adapter/TaxOverrideType/members[@name="AccruedTaxAmount"]/*' />
        AccruedTaxAmount=4
        
       
	}

	//Update Note : Added for 5.0
	/// <include file='TaxSvc.Doc.xml' path='adapter/ServiceMode/enum/*' />
	[Guid("a5452601-c717-4846-9256-11adf08cdbb0")]
	[ComVisible(true)]
	public enum ServiceMode 
	{
        
		/// <include file='TaxSvc.Doc.xml' path='adapter/ServiceMode/members[@name="Automatic"]/*' />
		Automatic=0,

		/// <include file='TaxSvc.Doc.xml' path='adapter/ServiceMode/members[@name="Local"]/*' />
		Local=1,

		/// <include file='TaxSvc.Doc.xml' path='adapter/ServiceMode/members[@name="Remote"]/*' />
		Remote=2
	}

	//Update Note : Added for 5.1
	/// <include file='TaxSvc.Doc.xml' path='adapter/AccountingMethod/enum/*' />
	[Guid("46708e9a-6e78-4969-b777-81f8deddabcd")]
	[ComVisible(true)]
	public enum AccountingMethod
	{
		/// <include file='TaxSvc.Doc.xml' path='adapter/AccountingMethod/members[@name="Accrual"]/*' />
		Accrual = 0,

		/// <include file='TaxSvc.Doc.xml' path='adapter/AccountingMethod/members[@name="Cash"]/*' />
		Cash = 1
	}

    /// <include file='TaxSvc.Doc.xml' path='adapter/ConfirmationType/enum/*' />
    [Guid("DD6F1178-8994-4d84-9165-E703BC819708")]
    [ComVisible(true)]
    public enum ConfirmationType
    {
        /// <include file='TaxSvc.Doc.xml' path='adapter/ConfirmationType/members[@name="None"]/*' />
        None = 0,

        /// <include file='TaxSvc.Doc.xml' path='adapter/ConfirmationType/members[@name="Optional"]/*' />
        Optional = 1,

        /// <include file='TaxSvc.Doc.xml' path='adapter/ConfirmationType/members[@name="Required"]/*' />
        Required = 2
    }

	#endregion

}