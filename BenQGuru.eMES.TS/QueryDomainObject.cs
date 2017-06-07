using System;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.TS;
using BenQGuru.eMES.Domain.DataCollect;

namespace BenQGuru.eMES.TS
{
	/// <summary>
	/// QueryDomainObject 的摘要说明。
	/// </summary>
	/// 
	[Serializable]
	public class TSErrorInfo : TSErrorCode
	{
		public TSErrorInfo() : base()
		{
		}
			
		public object[] ErrorCauseList;
	}

	[Serializable]
	public class ErrorLocation : DomainObject
	{
		public ErrorLocation() : base()
		{
		}
		
		[FieldMapAttribute("LOCCODE", typeof(string), 40, true)]
		public string LocationCode;
	}

	[Serializable]
	public class ErrorPart : DomainObject
	{
		public ErrorPart() : base()
		{
		}
		
		[FieldMapAttribute("PARTCODE", typeof(string), 40, true)]
		public string PartCode;
	}
	[Serializable]
	public class ItemOfRunningCard : OnWIPItem
	{
		public ItemOfRunningCard() : base()
		{
		}

		[FieldMapAttribute("SPLITQTY", typeof(decimal), 10, true)]
		public decimal ScraptQty;	

		[FieldMapAttribute("NEEDTS", typeof(bool), 1, true)]
		public bool NeedTS;	

		/// <summary>
		/// 是否检查KeyPart状态
		/// </summary>
		public bool CheckMaterialStatus;
	}
    [Serializable]
    public class TSSmartErrorCause : TSErrorCause
    {
        public TSSmartErrorCause()
            : base()
        {
        }

        [FieldMapAttribute("CNT", typeof(decimal), 10, true)]
        public decimal Count;

        public TSErrorCause2Location[] Locations;
        public TSErrorCause2ErrorPart[] ErrorParts;
        
    }
}
