#region Copyright
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Avalara.AvaTax.Adapter
{

	/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/ReadOnlyCollection/class/*' />
	[Guid("81307774-3891-443c-AC58-2A6F77CDDE48")]
	[ComVisible(true)]
	public class ReadOnlyArrayList : BaseArrayList
	{
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal ReadOnlyArrayList()
		{
			//not publicly creatable
		}

		//Hides the members but we'll still need access w/in the assembly to load them
		//no disptach id because internal
		internal new void Add(Object item)
		{
			base.Add(item);
		}
		
		//no dispatch id because internal
		internal new void Clear()
		{
			base.Clear();
		}

	}

	/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/BaseCollection/class/*' />
	[Guid("8419E559-1D1A-4335-A6C5-0B85BC54D7E3")]
	[ComVisible(true)]
	public class BaseArrayList
	{
		//Wrap an arraylist in our own class to limit the methods available
		//    and control inputs (force to be of strongly typed).
		ArrayList _list = new ArrayList();

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="InternalConstructor"]/*' />
		internal BaseArrayList()
		{
			//not publicly creatable
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/BaseCollection/members[@name="List"]/*' />
		//no dispatch id because protected
		protected ArrayList List
		{
			get { return _list; }
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/BaseCollection/members[@name="GetEnumerator"]/*' />
		[DispId(-4)]
		public virtual System.Collections.IEnumerator GetEnumerator()
		{
			return _list.GetEnumerator();
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/BaseCollection/members[@name="Count"]/*' />
		[DispId(20)]
		public virtual int Count
		{
			get
			{
				return _list.Count;
			}
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/BaseCollection/members[@name="this"]/*' />
		//no dispatch id because protected
		protected virtual Object this[int index]
		{
			get
			{
				return _list[ index ];
			}
		}

		//Any class derived from BaseCollection must implement its own Add function and invoke this one
		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/BaseCollection/members[@name="Add"]/*' />
		//no dipatch id because protected
		protected virtual void Add(Object item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item","A null object cannot be added to a collection.");
			}
			_list.Add( item );
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/BaseCollection/members[@name="Clear"]/*' />
		//no dispatch id because protected
		protected void Clear()
		{
			if ( _list != null )
			{
				_list.Clear();
			}
		}

		/// <include file='Avalara.AvaTax.Adapter.Doc.xml' path='adapter/common/members[@name="Finalize"]/*' />
		~BaseArrayList()
		{
			Clear();
			_list = null;
		}
	}
}
