using System;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QDOSNNGList 的摘要说明。
	/// </summary>
	public class QDOSNNGList : DomainObject
	{
		[FieldMapAttribute("FRMSSCODE", typeof(string), 40, true)]
		public string StepSequenceCode;

		[FieldMapAttribute("FRMRESCODE", typeof(string), 40, true)]
		public string ResourceCode;

		[FieldMapAttribute("ECGCODE", typeof(string), 40, true)]
		public string ErrorCodeGroup;

		[FieldMapAttribute("ECODE", typeof(string), 40, true)]
		public string ErrorCode;

		[FieldMapAttribute("FRMDATE", typeof(int), 8, true)]
		public int  MaintainDate = 0;

		[FieldMapAttribute("FRMTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		[FieldMapAttribute("FRMMEMO", typeof(string), 100, true)]
		public string  FrmMemo;

		public string ErrorCodeGroupDesc;
		public string ErrorCodeDesc;
	}
}
