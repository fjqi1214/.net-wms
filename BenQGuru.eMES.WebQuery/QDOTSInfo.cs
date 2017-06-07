using System;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QDOTSInfo 的摘要说明。
	/// </summary>
	public class QDOTSInfo : DomainObject
	{
		[FieldMapAttribute("errorcodegroup", typeof(string), 40, true)]
		public string ErrorCodeGroup;

		[FieldMapAttribute("ecgdesc", typeof(string), 40, true)]
		public string ErrorCodeGroupDesc;

		[FieldMapAttribute("errorcode", typeof(string), 40, true)]
		public string ErrorCode;

		[FieldMapAttribute("ecdesc", typeof(string), 40, true)]
		public string ErrorCodeDesc;

        [FieldMapAttribute("errorcausegroup", typeof(string), 40, true)]
        public string ErrorCauseGroup;

        [FieldMapAttribute("ecsgdesc", typeof(string), 40, true)]
        public string ErrorCauseGroupDesc;

		[FieldMapAttribute("errorcause", typeof(string), 40, true)]
		public string ErrorCause;

		[FieldMapAttribute("ecsdesc", typeof(string), 40, true)]
		public string ErrorCauseDesc;

		[FieldMapAttribute("errorlocation", typeof(string), 40, true)]
		public string ErrorLocation;

		[FieldMapAttribute("duty", typeof(string), 40, true)]
		public string Duty;

        [FieldMapAttribute("ERRORCOMPONENT", typeof(string), 40, true)]
        public string ErrorComponent;

		[FieldMapAttribute("dutydesc", typeof(string), 40, true)]
		public string DutyDesc;

		[FieldMapAttribute("qty", typeof(int), 10, true)]
		public int Quantity;


		//统计总数
		[FieldMapAttribute("allqty", typeof(int), 10, true)]
		public int AllQuantity;
 
		[FieldMapAttribute("percent", typeof(decimal), 10, true)]
		public decimal Percent;
	}

	/// <summary>
	/// QDOTSErrorCodeAnalyse 的摘要说明。
	/// </summary>
	public class QDOTSErrorCodeAnalyse : DomainObject
	{
		[FieldMapAttribute("modelcode", typeof(string), 40, true)]
		public string ModelCode;

		[FieldMapAttribute("ecgcode", typeof(string), 40, true)]
		public string ErrorCodeGroup;

		[FieldMapAttribute("ecgdesc", typeof(string), 40, true)]
		public string ErrorCodeGroupDesc;

		[FieldMapAttribute("ecode", typeof(string), 40, true)]
		public string ErrorCode;

		[FieldMapAttribute("ecdesc", typeof(string), 40, true)]
		public string ErrorCodeDesc;

		[FieldMapAttribute("qty", typeof(int), 10, true)]
		public int Quantity;

		[FieldMapAttribute("dategroup", typeof(int), 10, true)]
		public int DateGroup;
	}

}
