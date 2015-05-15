using System.Runtime.InteropServices;

namespace Avalara.AvaTax.Adapter
{
	/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/BaseRequest/class/*' />
	[Guid("02DE912C-71FF-49c7-A103-C55ABA8BD7D3")]
	[ComVisible(true)]
	public class BaseRequest
	{
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="Constructor"]/*' />
		internal BaseRequest()
		{
			
		}

		/// <summary>
		/// Must be overridden in the derived class. Called from the services classes to validate an object
		/// before passing it to the proxy service. Not exposed as part of the API.
		/// </summary>
		/// <remarks>There is no implementation in the base class.</remarks>
		/// <returns>False. Must be overridden by the derived class.</returns>
        internal virtual bool IsValid(out string message)
		{
		    message = "";
			return false;
		}
	}
}
