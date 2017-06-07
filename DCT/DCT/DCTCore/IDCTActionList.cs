using System;
using System.Collections;

namespace BenQGuru.eMES.Common.DCT.Core
{
	/// <author>Laws Lu</author>
	/// <since>2006/04/14</since>
	/// <version>1.0.0</version>
	public interface IDCTActionList
	{
		BaseDCTAction CurrentDCTAction
		{
			get;
			set;
		}

		BaseDCTAction NextDCTAction
		{
			get;
			set;
		}


		ArrayList DCTActionList
		{
			get;
			set;
		}
	}
}
