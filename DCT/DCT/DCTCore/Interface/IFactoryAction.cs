using System;

namespace BenQGuru.eMES.Common.DCT.Core
{
	/// <summary>
	/// ICommandAction 的摘要说明。
	/// </summary>
	public interface IFactoryAction
	{
		BaseDCTAction CachedAction
		{
			get;
			set;
		}
	}
}
