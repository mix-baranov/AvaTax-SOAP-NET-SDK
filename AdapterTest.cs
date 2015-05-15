using System;
using Avalara.AvaTax.Adapter.TaxService;
using NUnit.Framework;

#if DEBUG
namespace Avalara.AvaTax.Adapter
{
	/// <summary>
	/// Summary description for AdapterTest.
	/// </summary>
	[TestFixture]
	public class AdapterTest
	{
		/// <remarks/>
		public AdapterTest()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <remarks/>
		[Test]
		[ExpectedException(typeof(FormatException))]
		public void VerifyInvalidDateTest()
		{
			Utilities.VerifyDate("a");
		}

		/// <remarks/>
		[Test]
		public void VerifyValidDateTest()
		{
			Utilities.VerifyDate(null);
			Assert.IsTrue(true, "Passed VerifyDate for a null value");

			Utilities.VerifyDate("01/01/05");
			Assert.IsTrue(true, "Passed VerifyDate for a valid date string");
		}

		/// <remarks/>
		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void VerifyNullRequestObjectTest()
		{
			Utilities.VerifyRequestObject(null);
		}

		/// <remarks/>
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void VerifyInvalidRequestObjectTest()
		{
			RequestTest request = new RequestTest(false);
			Utilities.VerifyRequestObject(request);
		}

		/// <remarks/>
		[Test]
		public void VerifyValidRequestObjectTest()
		{
			RequestTest request = new RequestTest(true);
			Utilities.VerifyRequestObject(request);
		}
	}

	internal class RequestTest : BaseRequest
	{
		private bool _isValid;
	
		internal RequestTest(bool isValid)
		{
			_isValid = isValid;
		}

		internal override bool IsValid()
		{
			return _isValid;
		}
	}
}
#endif