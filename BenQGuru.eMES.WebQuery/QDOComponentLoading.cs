using System;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QDOIntegrated 的摘要说明。
	/// </summary>
	public class QDOIntegrated : DomainObject
	{
		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string MoCode;

		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string ItemCode;

		[FieldMapAttribute("rcard", typeof(string), 40, true)]
		public string SN;

		[FieldMapAttribute("OPCODE", typeof(string), 40, true)]
		public string OperationCode;

		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string StepSequenceCode;

		[FieldMapAttribute("RESCODE", typeof(string), 40, true)]
		public string ResourceCode;

		[FieldMapAttribute("mcard", typeof(string), 40, true)]
		public string INNO;

		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser = "";

		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate = 0;

		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;
	}

	public class QDOKeyparts : QDOIntegrated
	{
		[FieldMapAttribute("MITEMCODE", typeof(string), 40, true)]
		public string MItemCode;
	}
}
