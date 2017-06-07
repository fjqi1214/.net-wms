using System;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QDOTSRecord 的摘要说明。
	/// </summary>
	public class QDOTSRecord : DomainObject
	{
		[FieldMapAttribute("RCARD", typeof(string), 40, true)]
		public string SN;

		[FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, true)]
		public decimal  RunningCardSequence;

		[FieldMapAttribute("TSSTATUS", typeof(string), 40, true)]
		public string TsState;

		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string MoCode = "";
		
		[FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
		public string ModelCode = "";

		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string ItemCode = "";

		[FieldMapAttribute("TSDATE", typeof(int), 8, true)]
		public int NGDate = 0;

		//不良时间
		[FieldMapAttribute("TSTIME", typeof(int), 6, true)]
		public int NGTime = FormatHelper.TIME_DEFAULT;

		//不良次数
		[FieldMapAttribute("TSTIMES", typeof(int), 6, true)]
		public int NGCount = 0;

		[FieldMapAttribute("FRMRESCODE", typeof(string), 40, true)]
		public string SourceResource;

		[FieldMapAttribute("FRMDATE", typeof(int), 8, true)]
		public int SourceResourceDate = 0;

		[FieldMapAttribute("FRMTIME", typeof(int), 6, true)]
		public int SourceResourceTime = FormatHelper.TIME_DEFAULT;

		[FieldMapAttribute("RRESCODE", typeof(string), 40, true)]
		public string RepaireResource;

		[FieldMapAttribute("RDATE", typeof(string), 40, true)]
		public int RepaireDate = 0;

		[FieldMapAttribute("RTIME", typeof(string), 40, true)]
		public int RepaireTime = FormatHelper.TIME_DEFAULT;

		[FieldMapAttribute("REFRESCODE", typeof(string), 40, true)]
		public string DestResource;

		//Added by jessie Lee for P3.4,2005/8/25
		[FieldMapAttribute("REFOPCODE", typeof(string), 40, true)]
		public string DestOpCode;
		
		//操作工
		[FieldMapAttribute("FRMUSER", typeof(string), 40, true)]
		public string FrmUser;

		//维修工
		[FieldMapAttribute("TSUSER", typeof(string), 40, true)]
		public string TSUser;

		/// <summary>
		/// 接受站
		/// </summary>
		[FieldMapAttribute("CRESCODE", typeof(string), 40, true)]
		public string ConfirmResource;

		/// <summary>
		/// 接受人
		/// </summary>
		[FieldMapAttribute("CONFIRMUSER", typeof(string), 40, true)]
		public string ConfirmUser;
		
		/// <summary>
		/// 接受日期
		/// </summary>
		[FieldMapAttribute("CONFIRMDATE", typeof(int), 8, true)]
		public int ConfirmDate;

		/// <summary>
		/// 接受时间
		/// </summary>
		[FieldMapAttribute("CONFIRMTIME", typeof(int), 6, true)]
		public int ConfiemTime;

		/// <summary>
		/// TSID
		/// </summary>
		[FieldMapAttribute("TSID", typeof(string), 40, true)]
		public string TSID;

        [FieldMapAttribute("FRMMEMO", typeof(string), 100, true)]
        public string TSMemo;

		/// <summary>
		/// 报废原因
		/// </summary>
		[FieldMapAttribute("SCRAPCAUSE", typeof(string), 100, true)]
		public string ScrapCause;

        [FieldMapAttribute("MODELTYPE", typeof(string), 40, true)]
        public string ModelType;

        [FieldMapAttribute("BOMVERSION", typeof(string), 40, true)]
        public string BOMVersion;

        [FieldMapAttribute("MDESC", typeof(string), 40, true)]
        public string ItemDesc = "";
		
		public int DestResourceDate = 0;
		
		public int DestResourceTime = 0;

		public override string ToString()
		{
			return this.TSID;
		}
	}
	
}
