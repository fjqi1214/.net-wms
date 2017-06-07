using System;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.DataCollect;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// OQCFirstHandingYield 的摘要说明。
	/// </summary>
	public class OQCFirstHandingYield : DomainObject
	{
		/// <summary>
		/// 机种代码[Model]
		/// </summary>
		[FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
		public string  ModelCode;

		/// <summary>
		/// 料号[ItemCode]
		/// </summary>
		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string  ItemCode;

		/// <summary>
		/// 一次提交总数
		/// </summary>
		[FieldMapAttribute("amount", typeof(decimal), 10, true)]
		public decimal FirstHandingAmount;
 
		/// <summary>
		/// 一交合格数
		/// </summary>
		[FieldMapAttribute("yield_amount", typeof(decimal), 10, true)]
		public decimal FirstHandingYieldAmount;
		
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("yield_percent", typeof(decimal), 10, true)]
		public decimal FirstHandingYieldPercent;
	}

	/// <summary>
	/// OQC一交合格率明细
	/// </summary>
	public class OQCFirstHandingYieldDetail : DomainObject
	{
		/// <summary>
		/// 批号
		/// </summary>
		[FieldMapAttribute("LOTNO",typeof(string), 40, true)]
		public string LotNo;

		/// <summary>
		/// 批量
		/// </summary>
		[FieldMapAttribute("LOTSIZE",  typeof(decimal), 10, true)]
		public decimal LotSize;

		/// <summary>
		/// 样本数量
		/// </summary>
		[FieldMapAttribute("SSIZE",  typeof(decimal), 10, true)]
		public decimal SSize;

		/// <summary>
		/// 实际采样数量
		/// </summary>
		[FieldMapAttribute("actcheckcount",  typeof(decimal), 10, true)]
		public decimal ActCheckSize;

		/// <summary>
		/// 安全缺陷
		/// </summary>
		[FieldMapAttribute("AGRADETIMES",  typeof(decimal), 10, true)]
		public decimal Agradetimes;

		/// <summary>
		/// 严重缺陷
		/// </summary>
		[FieldMapAttribute("BGGRADETIMES",  typeof(decimal), 10, true)]
		public decimal Bggradetimes;

		/// <summary>
		/// 轻度缺陷
		/// </summary>
		[FieldMapAttribute("CGRADETIMES",  typeof(decimal), 10, true)]
		public decimal Cgradetimes;

        [FieldMapAttribute("ZGRADETIMES", typeof(decimal), 10, true)]
        public decimal ZGrageTimes;

		/// <summary>
		/// 判定结果
		/// </summary>
		[FieldMapAttribute("LOTSTATUS",typeof(string), 40, true)]
		public string LotStatus;

		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;
		
	}

	public class HistroyQuantitySummary : DomainObject
	{
		/// <summary>
		/// 机种代码[Model]
		/// </summary>
		[FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
		public string ModelCode;

		/// <summary>
		/// 料号[ItemCode]
		/// </summary>
		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string ItemCode;

		/// <summary>
		/// 工单号
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string MoCode;

		/// <summary>
		/// 工序代码
		/// </summary>
		[FieldMapAttribute("OPCODE", typeof(string), 40, true)]
		public string OperationCode;

		/// <summary>
		/// 工段代码
		/// </summary>
		[FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
		public string SegmentCode;

		/// <summary>
		/// 工序组代码
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string StepSequenceCode;

		/// <summary>
		/// 资源代码
		/// </summary>
		[FieldMapAttribute("RESCODE", typeof(string), 40, true)]
		public string ResourceCode;

		[FieldMapAttribute("MONTH", typeof(string), 8, true)]
		public string Month;

		[FieldMapAttribute("WEEK", typeof(string), 8, true)]
		public string Week;

		[FieldMapAttribute("DAY", typeof(int), 8, true)]
		public int NatureDate;

		[FieldMapAttribute("SHIFTDAY", typeof(int), 8, true)]
		public int ShiftDay;

		/// <summary>
		/// 班次代码
		/// </summary>
		[FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
		public string ShiftCode;

		/// <summary>
		/// 时间段代码
		/// </summary>
		[FieldMapAttribute("TPCODE", typeof(string), 40, true)]
		public string TimePeriodCode;


		/// <summary>
		/// 产出数
		/// </summary>
		[FieldMapAttribute("OUTPUTQTY", typeof(int), 10, true)]
		public int Quantity;

		/// <summary>
		/// 投入数
		/// </summary>
		[FieldMapAttribute("INPUTQTY", typeof(int), 10, true)]
		public int InputQty;
	}

	public class HistoryYieldPercent : DomainObject
	{
		/// <summary>
		/// 机种代码[Model]
		/// </summary>
		[FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
		public string ModelCode;

		/// <summary>
		/// 料号[ItemCode]
		/// </summary>
		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string ItemCode;

		/// <summary>
		/// 工单号
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string MoCode;

		/// <summary>
		/// 工序代码
		/// </summary>
		[FieldMapAttribute("OPCODE", typeof(string), 40, true)]
		public string OperationCode;

		/// <summary>
		/// 工段代码
		/// </summary>
		[FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
		public string SegmentCode;

		/// <summary>
		/// 工序组代码
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string StepSequenceCode;

		/// <summary>
		/// 资源代码
		/// </summary>
		[FieldMapAttribute("RESCODE", typeof(string), 40, true)]
		public string ResourceCode;

		[FieldMapAttribute("MONTH", typeof(string), 8, true)]
		public string Month;

		[FieldMapAttribute("WEEK", typeof(string), 8, true)]
		public string Week;

		[FieldMapAttribute("DAY", typeof(int), 8, true)]
		public int NatureDate;

		[FieldMapAttribute("SHIFTDAY", typeof(int), 8, true)]
		public int ShiftDay;

		/// <summary>
		/// 班次代码
		/// </summary>
		[FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
		public string ShiftCode;

		/// <summary>
		/// 时间段代码
		/// </summary>
		[FieldMapAttribute("TPCODE", typeof(string), 40, true)]
		public string TimePeriodCode;

		[FieldMapAttribute("NGTimes", typeof(int), 10, true)]
		public long NGTimes;

		[FieldMapAttribute("AllTimes", typeof(int), 10, true)]
		public long AllTimes;

		[FieldMapAttribute("OUTPUTQTY", typeof(int), 10, true)]
		public long Quantity = 0;

		[FieldMapAttribute("INPUTQTY", typeof(int), 10, true)]
		public long InputQuantity = 0;

		[FieldMapAttribute("YieldPercent", typeof(decimal), 10, true)]
		public decimal YieldPercent;

		[FieldMapAttribute("ALLGOODQTY", typeof(int), 10, true)]
		public long AllGoodQuantity;

		[FieldMapAttribute("AllGoodYieldPercent", typeof(decimal), 10, true)]
		public decimal AllGoodYieldPercent;

		[FieldMapAttribute("NotYieldPercent", typeof(decimal), 10, true)]
		public decimal NotYieldPercent;

		[FieldMapAttribute("NotYieldLocation", typeof(int), 10, true)]
		public long NotYieldLocation;

		[FieldMapAttribute("totallocation", typeof(int), 10, true)]
		public long TotalLocation;

		[FieldMapAttribute("PPM", typeof(int), 10, true)]
		public long PPM;
	}


	public class OnWipInfoOnOperation : DomainObject
	{		
		[FieldMapAttribute("OPCODE", typeof(string), 40, true)]
		public string OperationCode;

		/// <summary>
		/// 在制数量
		/// </summary>
		[FieldMapAttribute("QTY", typeof(decimal), 10, true)]
		public int OnWipQuantityOnOperation;
	}

	public class OnWipInfoOnResource : DomainObject
	{		
		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string MoCode;

		[FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
		public string ShiftCode;

		[FieldMapAttribute("SHIFTDAY", typeof(string), 40, true)]
		public int ShiftDay;
		
		[FieldMapAttribute("RESCODE", typeof(string), 40, true)]
		public string ResourceCode;

		[FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
		public string SegmentCode;

		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string StepSequenceCode;

		/// <summary>
		/// 在制良品数量
		/// </summary>
		[FieldMapAttribute("GOODQTY", typeof(decimal), 10, true)]
		public int OnWipGoodQuantityOnResource;

		/// <summary>
		/// 在制不良品数量
		/// </summary>
		[FieldMapAttribute("NGQTY", typeof(decimal), 10, true)]
		public int OnWipNGQuantityOnResource;

		/// <summary>
		/// 判退待返工数量
		/// </summary>
		[FieldMapAttribute("NGForReworksQTY", typeof(decimal), 10, true)]
		public int NGForReworksQTY;

		/* Added by jessie lee, 2005/12/8, 
		 * 一下三个字段，维修工序“TS”专用
		 * */
		/// <summary>
		/// 待修数量
		/// </summary>
		[FieldMapAttribute("TSConfirmQty", typeof(decimal), 10, true)]
		public int TSConfirmQty;

		/// <summary>
		/// 维修中数量
		/// </summary>
		[FieldMapAttribute("TSQty", typeof(decimal), 10, true)]
		public int TSQty;

		/// <summary>
		/// 待回流数量
		/// </summary>
		[FieldMapAttribute("TSReflowQty", typeof(decimal), 10, true)]
		public int TSReflowQty;


	}

	public class OnWipInfoDistributing : DomainObject
	{
		/// <summary>
		/// 序列号
		/// </summary>
		[FieldMapAttribute("RCARD", typeof(string), 40, true)]
		public string RunningCard;
		
		/// <summary>
		///
		/// </summary>
		[FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, true)]
		public decimal RunningCardSequence;

		/// <summary>
		/// 序列号
		/// </summary>
		[FieldMapAttribute("TCARD", typeof(string), 40, true)]
		public string TranslateCard;

		/// <summary>
		/// 产品状态
		/// </summary>
		[FieldMapAttribute("STATUS", typeof(string), 40, true)]
		public string ProductStatus;

		/// <summary>
		/// 工序类型
		/// </summary>
		[FieldMapAttribute("OPCONTROL", typeof(string), 40, true)]
		public string OPControl;	
	
		/// <summary>
		/// 分板比例
		/// </summary>
		[FieldMapAttribute("IDMERGERULE", typeof(decimal), 10, true)]
		public decimal  IDMergeRule;

		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;
		/// <summary>
		/// 操作工序
		/// </summary>
		[FieldMapAttribute("LACTION", typeof(string), 40, true)]
		public string  Action;

	}

	public class ComponentLoadingTracking : DomainObject
	{
		[FieldMapAttribute("SN", typeof(string), 40, true)]
		public string SN;

		[FieldMapAttribute("RCARDSEQ", typeof(int), 8, true)]
		public int SNSeq;

		[FieldMapAttribute("MCARD", typeof(string), 40, true)]
		public string MCard;

		[FieldMapAttribute("MSEQ", typeof(int), 10, true)]
		public int  MSequence;

		[FieldMapAttribute("INNO", typeof(string), 40, true)]
		public string INNO;

		[FieldMapAttribute("KEYPARTS", typeof(string), 40, true)]
		public string KeyParts;

		[FieldMapAttribute("MITEMCODE", typeof(string), 40, true)]
		public string MItemCode;

		/// <summary>
		/// 工单号
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string MoCode = "";

		/// <summary>
		/// 工单号
		/// </summary>
		[FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
		public string ModelCode = "";

		/// <summary>
		/// 工单号
		/// </summary>
		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string ItemCode = "";

		[FieldMapAttribute("SNSTATE", typeof(string), 40, true)]
		public string SNState = "";

		[FieldMapAttribute("ROUTECODE", typeof(string), 40, true)]
		public string RouteCode = "";

		/// <summary>
		/// 工序代码
		/// </summary>
		[FieldMapAttribute("OPCODE", typeof(string), 40, true)]
		public string OperationCode = "";

		/// <summary>
		/// 工段代码
		/// </summary>
		[FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
		public string SegmentCode = "";

		/// <summary>
		/// 工序组代码
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string StepSequenceCode = "";

		/// <summary>
		/// 资源代码
		/// </summary>
		[FieldMapAttribute("RESCODE", typeof(string), 40, true)]
		public string ResourceCode = "";

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser = "";

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate = 0;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime = 0;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("LOTNO", typeof(string), 40, false)]
		public string  LotNO;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PCBA", typeof(string), 40, false)]
		public string  PCBA;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("BIOS", typeof(string), 40, false)]
		public string  BIOS;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("VERSION", typeof(string), 40, false)]
		public string  Version;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("VENDORITEMCODE", typeof(string), 40, false)]
		public string  VendorItemCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("VENDORCODE", typeof(string), 40, false)]
		public string  VendorCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DATECODE", typeof(string), 40, false)]
		public string  DateCode;
	}

	public class RealTimeQuantity : DomainObject
	{
		/// <summary>
		/// 工序组代码
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string StepSequenceCode = "";

		/// <summary>
		/// 产品代码
		/// </summary>
		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string ItemCode = "";

		/// <summary>
		/// 时间段代码
		/// </summary>
		[FieldMapAttribute("TPCODE", typeof(string), 40, true)]
		public string TimePeriodCode = "";

//		[FieldMapAttribute("OUTPUTQTY", typeof(int), 10, true)]
//		public int Quantity = 0; 

		/// <summary>
		/// 产出数量
		/// </summary>
		[FieldMapAttribute("OUTPUTQTY", typeof(decimal), 10, true)]
		public int OutputQuantity = 0; 

		/// <summary>
		/// 投入数量
		/// </summary>
		[FieldMapAttribute("INPUTQTY", typeof(decimal), 10, true)]
		public int InputQuantity = 0; 

		/// <summary>
		/// 拆解数量
		/// </summary>
		[FieldMapAttribute("SCRAPQTY", typeof(decimal), 10, true)]
		public int ScrapQuantity = 0; 
	}

	//修改目的:tblonwip表查询
	//修改时间:2006.9.25
	//修改人:melo zheng
	public class TimeQuantitySum : DomainObject
	{
		/// <summary>
		/// 产线
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string StepSequenceCode = "";

		/// <summary>
		/// 资源
		/// </summary>
		[FieldMapAttribute("RESCODE", typeof(string), 40, true)]
		public string ResourceCode = "";

		/// <summary>
		/// 日期
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int MaintainDate = 0;

		/// <summary>
		/// 时间
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int MaintainTime = 0;

		/// <summary>
		/// 生产数量
		/// </summary>
		[FieldMapAttribute("ACTIONRESULT", typeof(int), 10, true)]
		public int ActionResult = 0;
	}

	 /// <summary>
	 /// 投入产出报表实体
	 /// </summary>
	public class RealTimeInputOutputQuantity : DomainObject
	{
	
		/// <summary>
		/// 工单号
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string MOCode = "";

		/// <summary>
		/// 机种
		/// </summary>
		[FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
		public string ModelCode = "";

		/// <summary>
		/// 产品Code
		/// </summary>
		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string ItemCode = "";

		/// <summary>
		/// 工段代码
		/// </summary>
		[FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
		public string SegmentCode = "";

		//工单计划数量
		[FieldMapAttribute("MOPLANQTY", typeof(decimal), 10, true)]
		public decimal MOPlanqty = 0; 

		//工单当班投入数 (当前班次投入的数量)
		[FieldMapAttribute("INPUTQTY", typeof(decimal), 10, true)]
		public decimal MOShiftInputqty = 0; 

		//工单累计投入数
		[FieldMapAttribute("MOINPUTQTY", typeof(decimal), 10, true)]
		public decimal MOInputqty = 0; 

		//工单当班产出数 (当前班次产出的数量)
		[FieldMapAttribute("OUTPUTQTY", typeof(decimal), 10, true)]
		public decimal MOShiftOutputqty = 0; 

		//工单累计产出数
		[FieldMapAttribute("MOACTQTY", typeof(decimal), 10, true)]
		public decimal MOOutputqty = 0; 

		//工单累计拆解数
		[FieldMapAttribute("moscrapqty", typeof(decimal), 10, true)]
		public decimal MOScrapqty = 0; 

		/// <summary>
		/// 脱离工单数量
		/// </summary>
		[FieldMapAttribute("OffMoQty", typeof(decimal), 10,false)]
		public decimal  MOOffQty = 0;

	}

	//实时投入产量明细
	public class RealTimeInputQuantity : DomainObject
	{
		/// <summary>
		/// 工序组代码
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string StepSequenceCode = "";

		/// <summary>
		/// 时间段代码
		/// </summary>
		[FieldMapAttribute("TPCODE", typeof(string), 40, true)]
		public string TimePeriodCode = "";

		[FieldMapAttribute("INPUTQTY", typeof(int), 10, true)]
		public int Quantity = 0; 
	}

	//实时产出产量明细
	public class RealTimeOutputQuantity : DomainObject
	{
		/// <summary>
		/// 工序组代码
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string StepSequenceCode = "";

		/// <summary>
		/// 时间段代码
		/// </summary>
		[FieldMapAttribute("TPCODE", typeof(string), 40, true)]
		public string TimePeriodCode = "";

		[FieldMapAttribute("OUTPUTQTY", typeof(int), 10, true)]
		public int Quantity = 0; 
	}

	public class RealTimeDetails : DomainObject
	{
		/// <summary>
		/// 工单号
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string MoCode = "";

		/// <summary>
		/// 工单号
		/// </summary>
		[FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
		public string ModelCode = "";

		/// <summary>
		/// 工单号
		/// </summary>
		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string ItemCode = "";

//		[FieldMapAttribute("OUTPUTQTY", typeof(int), 10, true)]
//		public int Quantity = 0;

		/// <summary>
		/// 产出数量
		/// </summary>
		[FieldMapAttribute("OUTPUTQTY", typeof(decimal), 10, true)]
		public int OutputQuantity = 0; 

		/// <summary>
		/// 投入数量
		/// </summary>
		[FieldMapAttribute("INPUTQTY", typeof(decimal), 10, true)]
		public int InputQuantity = 0; 

		/// <summary>
		/// 拆解数量
		/// </summary>
		[FieldMapAttribute("SCRAPQTY", typeof(decimal), 10, true)]
		public int ScrapQuantity = 0; 

		/// <summary>
		/// 产品名号
		/// </summary>
		[FieldMapAttribute("ITEMNAME", typeof(string), 40, true)]
		public string ItemName = "";

		/// <summary>
		/// 工单备注
		/// </summary>
		[FieldMapAttribute("MOMEMO", typeof(string), 40, true)]
		public string MoMemo = "";
	}

	public class RealTimeYieldPercent : DomainObject
	{
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
		public string SegmentCode = "";		

		/// <summary>
		/// 工序组代码
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string StepSequenceCode = "";		

		[FieldMapAttribute("ALLGOODQTY", typeof(int), 10, true)]
		public int AllGoodQuantity;

		[FieldMapAttribute("OUTPUTQTY", typeof(int), 10, true)]
		public int Quantity = 0; 

		[FieldMapAttribute("AllGoodYieldPercent", typeof(decimal), 10, true)]
		public decimal AllGoodYieldPercent;
	}

	public class RealTimeDefect : DomainObject
	{
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string StepSequenceCode = "";
		
		[FieldMapAttribute("RESCODE", typeof(string), 40, true)]
		public string ResourceCode = "";

		[FieldMapAttribute("ERRORCODE", typeof(string), 40, true)]
		public string ErrorCode = "";

		[FieldMapAttribute("ERRORCODEGROUP", typeof(string), 40, true)]
		public string ErrorCodeGroup = "";

		[FieldMapAttribute("DEFECTQTY", typeof(int), 10, true)]
		public int DefectQuantity = 0;

		//不良代码组描述
		[FieldMapAttribute("ECGDESC", typeof(string), 100, true)]
		public string ECGDESC = "";

		//不良代码描述
		[FieldMapAttribute("ECDESC", typeof(string), 100, true)]
		public string ECDESC = "";

		[FieldMapAttribute("INPUTQTY", typeof(int), 10, true)]
		public int InputQty = 0;

		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string ItemCode = "";
	} 

	public class TracedKeyParts : OnWIPItem
	{
		public bool CanTrace = false;

		//物料名称
		public string MItemName;
	}

	public class TracedMinno : MINNO
	{
		/// <summary>
		/// 0,上料
		/// 1,下料
		/// </summary>
		public int  ActionType;
	}

	public class MOQueryCode : DomainObject
	{
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string MOCode = "";
		
	} 

	public class QDORes2EC: DomainObject
	{
		[FieldMapAttribute("ECGCode", typeof(string), 40, true)]
		public string  ErrorCodeGroup;

		[FieldMapAttribute("ECCode", typeof(string), 40, true)]
		public string  ErrorCode;

		public string  ErrorCodeDesc;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("TPCode", typeof(string), 40, true)]
		public string  TPCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("TPBTime", typeof(int), 6, true)]
		public int  TPBTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("TPETime", typeof(int), 6, true)]
		public int  TPETime;

		/// <summary>
		/// 不良次数
		/// </summary>
		[FieldMapAttribute("NGTimes", typeof(decimal), 10, true)]
		public decimal  NGTimes;

	}

	public class RPTCenterQuantity : DomainObject
	{
		/// <summary>
		/// 工段代码
		/// </summary>
		[FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
		public string SegmentCode;

		/// <summary>
		/// 本日产量
		/// </summary>
		[FieldMapAttribute("DayQuantity", typeof(int), 40, true)]
		public int DayQuantity;
		
		/// <summary>
		/// 本周累计
		/// </summary>
		[FieldMapAttribute("WeekQuantity", typeof(int), 40, true)]
		public int WeekQuantity;
		
		/// <summary>
		/// 本月累计
		/// </summary>
		[FieldMapAttribute("MonthQuantity", typeof(int), 40, true)]
		public int MonthQuantity;
	}

	public class RPTCenterYield : DomainObject
	{
		/// <summary>
		/// 工序代码
		/// </summary>
		[FieldMapAttribute("OPCODE", typeof(string), 40, true)]
		public string OperationCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string ItemCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string MOCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SHIFTDAY", typeof(int), 8, true)]
		public int ShiftDay;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("NGTIMES", typeof(int), 8, true)]
		public int NGTimes;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EATTRIBUTE2", typeof(int), 8, true)]
		public int Eattribute2;

		/// <summary>
		/// 本日良率
		/// </summary>
		[FieldMapAttribute("DayPercent", typeof(decimal), 40, true)]
		public decimal DayPercent;
		
		/// <summary>
		/// 本周良率
		/// </summary>
		[FieldMapAttribute("WeekPercent", typeof(decimal), 40, true)]
		public decimal WeekPercent;
		
		/// <summary>
		/// 本月良率
		/// </summary>
		[FieldMapAttribute("MonthPercent", typeof(decimal), 40, true)]
		public decimal MonthPercent;
	}

	public class RPTCenterLRR : DomainObject
	{
		/// <summary>
		/// 工段代码
		/// </summary>
		[FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
		public string SegmentCode;

		/// <summary>
		/// 本日良率
		/// </summary>
		[FieldMapAttribute("DayLRR", typeof(decimal), 40, true)]
		public decimal DayLRR;
		
		/// <summary>
		/// 本周良率
		/// </summary>
		[FieldMapAttribute("WeekLRR", typeof(decimal), 40, true)]
		public decimal WeekLRR;
		
		/// <summary>
		/// 本月良率
		/// </summary>
		[FieldMapAttribute("MonthLRR", typeof(decimal), 40, true)]
		public decimal MonthLRR;
	}

	public class RPTCenterTPT : DomainObject
	{
		/// <summary>
		/// 工单代码
		/// </summary>
		[FieldMapAttribute("Mo_MOCode", typeof(string), 40, true)]
		public string Mo_MOCode;

		/// <summary>
		/// 产品
		/// </summary>
		[FieldMapAttribute("Mo_ItemCode", typeof(string), 40, true)]
		public string Mo_ItemCode;
		
		/// <summary>
		/// 投产日期
		/// </summary>
		[FieldMapAttribute("Mo_StartDate", typeof(int), 40, true)]
		public int Mo_StartDate;
		
		/// <summary>
		/// 预计完成日期
		/// </summary>
		[FieldMapAttribute("Mo_PlanEndDate", typeof(int), 40, true)]
		public int Mo_PlanEndDate;
		
		/// <summary>
		/// 关单日期
		/// </summary>
		[FieldMapAttribute("Mo_EndDate", typeof(int), 40, true)]
		public int Mo_EndDate;
		
		/// <summary>
		/// 生产天数
		/// </summary>
		[FieldMapAttribute("Mo_DateNum", typeof(int), 40, true)]
		public int Mo_DateNum;
		
		/// <summary>
		/// 超计划天数
		/// </summary>
		[FieldMapAttribute("Mo_OverDateNum", typeof(int), 40, true)]
		public int Mo_OverDateNum;
		
		/// <summary>
		/// 状态
		/// </summary>
		[FieldMapAttribute("Mo_Estate", typeof(string), 40, true)]
		public string Mo_Estate;
	}

	public class RPTCenterLong : DomainObject
	{
		/// <summary>
		/// 序列号
		/// </summary>
		[FieldMapAttribute("Ts_SN", typeof(string), 40, true)]
		public string Ts_SN;

		/// <summary>
		/// 进入维修站日期
		/// </summary>
		[FieldMapAttribute("Ts_ConfirmDate", typeof(int), 40, true)]
		public int Ts_ConfirmDate;
		
		/// <summary>
		/// 维修天数
		/// </summary>
		[FieldMapAttribute("Ts_Days", typeof(int), 40, true)]
		public int Ts_Days;
	}

	public class RPTCenterLine : DomainObject
	{
		/// <summary>
		/// 产线
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string StepSequenceCode;

		/// <summary>
		/// 本日产量
		/// </summary>
		[FieldMapAttribute("DayQuantity", typeof(int), 40, true)]
		public int DayQuantity;
		
		/// <summary>
		/// 本周累计
		/// </summary>
		[FieldMapAttribute("WeekQuantity", typeof(int), 40, true)]
		public int WeekQuantity;
		
		/// <summary>
		/// 本月累计
		/// </summary>
		[FieldMapAttribute("MonthQuantity", typeof(int), 40, true)]
		public int MonthQuantity;
	}

	public class RPTCenterWeekQuantity : DomainObject
	{
		/// <summary>
		/// 工段代码
		/// </summary>
		[FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
		public string SegmentCode;

		/// <summary>
		/// 本日产量
		/// </summary>
		[FieldMapAttribute("DayQuantity", typeof(int), 40, true)]
		public int DayQuantity;
		
		/// <summary>
		/// 日期
		/// </summary>
		[FieldMapAttribute("ShiftDay", typeof(int), 40, true)]
		public int ShiftDay;
	}

	public class RPTCenterMonthQuantity : DomainObject
	{
		/// <summary>
		/// 工段代码
		/// </summary>
		[FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
		public string SegmentCode;

		/// <summary>
		/// 本日产量
		/// </summary>
		[FieldMapAttribute("DayQuantity", typeof(int), 40, true)]
		public int DayQuantity;
		
		/// <summary>
		/// 日期
		/// </summary>
		[FieldMapAttribute("ShiftDay", typeof(int), 40, true)]
		public int ShiftDay;
	}

	public class RPTCenterMocode : DomainObject
	{
		/// <summary>
		/// 产线
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string StepSequenceCode;

		/// <summary>
		/// 工单
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string MoCode;

		/// <summary>
		/// 产品
		/// </summary>
		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string ItemCode;
		
		/// <summary>
		/// 产品名称
		/// </summary>
		[FieldMapAttribute("ItemName", typeof(string), 40, true)]
		public string ItemName;
		
		/// <summary>
		/// 当日产量
		/// </summary>
		[FieldMapAttribute("DayQuantity", typeof(int), 40, true)]
		public int DayQuantity;
		
		/// <summary>
		/// 工单计划
		/// </summary>
		[FieldMapAttribute("PlanQTY", typeof(int), 40, true)]
		public int PlanQTY;
		
		/// <summary>
		/// 工单累计
		/// </summary>
		[FieldMapAttribute("ActQTY", typeof(int), 40, true)]
		public int ActQTY;
	}

	public class RPTCenterWeekMocode : DomainObject
	{
		/// <summary>
		/// 产线
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string StepSequenceCode;
		
		/// <summary>
		/// 当日产量
		/// </summary>
		[FieldMapAttribute("DayQuantity", typeof(int), 40, true)]
		public int DayQuantity;
		
		/// <summary>
		/// 日期
		/// </summary>
		[FieldMapAttribute("ShiftDay", typeof(int), 40, true)]
		public int ShiftDay;
	}

	public class RPTCenterMonthMocode : DomainObject
	{
		/// <summary>
		/// 产线
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string StepSequenceCode;
		
		/// <summary>
		/// 当日产量
		/// </summary>
		[FieldMapAttribute("DayQuantity", typeof(int), 40, true)]
		public int DayQuantity;
		
		/// <summary>
		/// 日期
		/// </summary>
		[FieldMapAttribute("ShiftDay", typeof(int), 40, true)]
		public int ShiftDay;
	}

	public class RPTCenterResCode : DomainObject
	{
		/// <summary>
		/// 资源
		/// </summary>
		[FieldMapAttribute("ResCode", typeof(string), 40, true)]
		public string ResCode;
		
		/// <summary>
		/// 当日产量
		/// </summary>
		[FieldMapAttribute("DayQuantity", typeof(int), 40, true)]
		public int DayQuantity;
		
		/// <summary>
		/// 累计产量
		/// </summary>
		[FieldMapAttribute("ActQTY", typeof(int), 40, true)]
		public int ActQTY;
	}

	public class RPTCenterDayYield : DomainObject
	{
		/// <summary>
		/// 产线
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string StepSequenceCode;

		/// <summary>
		/// 产品
		/// </summary>
		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string ItemCode;

		/// <summary>
		/// 产品名称
		/// </summary>
		[FieldMapAttribute("ItemName", typeof(string), 40, true)]
		public string ItemName;

		/// <summary>
		/// 资源
		/// </summary>
		[FieldMapAttribute("ResCode", typeof(string), 40, true)]
		public string ResCode;

		/// <summary>
		/// 良率
		/// </summary>
		[FieldMapAttribute("DayPercent", typeof(decimal), 40, true)]
		public decimal DayPercent;
	}

	public class RPTCenterWeekYield : DomainObject
	{
		/// <summary>
		/// 工序代码
		/// </summary>
		[FieldMapAttribute("OPCODE", typeof(string), 40, true)]
		public string OperationCode;

		/// <summary>
		/// 良率
		/// </summary>
		[FieldMapAttribute("DayPercent", typeof(decimal), 40, true)]
		public decimal DayPercent;
		
		/// <summary>
		/// 日期
		/// </summary>
		[FieldMapAttribute("ShiftDay", typeof(int), 40, true)]
		public int ShiftDay;
	}

	public class RPTCenterMonthYield : DomainObject
	{
		/// <summary>
		/// 工序代码
		/// </summary>
		[FieldMapAttribute("OPCODE", typeof(string), 40, true)]
		public string OperationCode;

		/// <summary>
		/// 良率
		/// </summary>
		[FieldMapAttribute("DayPercent", typeof(decimal), 40, true)]
		public decimal DayPercent;

		/// <summary>
		/// 日期
		/// </summary>
		[FieldMapAttribute("ShiftDay", typeof(int), 40, true)]
		public int ShiftDay;
	}

	public class RPTCenterDayPercent : DomainObject
	{
		/// <summary>
		/// 不良代码组
		/// </summary>
		[FieldMapAttribute("ECode", typeof(string), 40, true)]
		public string ECode;

		/// <summary>
		/// 不良代码
		/// </summary>
		[FieldMapAttribute("EcgCode", typeof(string), 40, true)]
		public string EcgCode;

		/// <summary>
		/// 不良描述
		/// </summary>
		[FieldMapAttribute("Ecdesc", typeof(string), 40, true)]
		public string Ecdesc;

		/// <summary>
		/// 数量
		/// </summary>
		[FieldMapAttribute("Qty", typeof(int), 40, true)]
		public int Qty;
	}

	public class RPTCenterWeekPercent : DomainObject
	{
		/// <summary>
		/// 不良代码组
		/// </summary>
		[FieldMapAttribute("ECode", typeof(string), 40, true)]
		public string ECode;

		/// <summary>
		/// 不良代码
		/// </summary>
		[FieldMapAttribute("EcgCode", typeof(string), 40, true)]
		public string EcgCode;

		/// <summary>
		/// 不良描述
		/// </summary>
		[FieldMapAttribute("Ecdesc", typeof(string), 40, true)]
		public string Ecdesc;

		/// <summary>
		/// 数量
		/// </summary>
		[FieldMapAttribute("Qty", typeof(int), 40, true)]
		public int Qty;
	}

	public class RPTCenterMonthPercent : DomainObject
	{
		/// <summary>
		/// 不良代码组
		/// </summary>
		[FieldMapAttribute("ECode", typeof(string), 40, true)]
		public string ECode;

		/// <summary>
		/// 不良代码
		/// </summary>
		[FieldMapAttribute("EcgCode", typeof(string), 40, true)]
		public string EcgCode;

		/// <summary>
		/// 不良描述
		/// </summary>
		[FieldMapAttribute("Ecdesc", typeof(string), 40, true)]
		public string Ecdesc;

		/// <summary>
		/// 数量
		/// </summary>
		[FieldMapAttribute("Qty", typeof(int), 40, true)]
		public int Qty;
	}

	public class RPTCenterDayProduct : DomainObject
	{
		/// <summary>
		/// 产品序列号
		/// </summary>
		[FieldMapAttribute("RCard", typeof(string), 40, true)]
		public string RCard;

		/// <summary>
		/// 产品
		/// </summary>
		[FieldMapAttribute("ItemCode", typeof(string), 40, true)]
		public string ItemCode;

		/// <summary>
		/// 产线
		/// </summary>
		[FieldMapAttribute("FrmSSCode", typeof(string), 40, true)]
		public string FrmSSCode;

		/// <summary>
		/// 不良发现人员
		/// </summary>
		[FieldMapAttribute("FrmUser", typeof(string), 40, true)]
		public string FrmUser;

		/// <summary>
		/// 不良发现工序
		/// </summary>
		[FieldMapAttribute("FrmOPCode", typeof(string), 40, true)]
		public string FrmOPCode;

		/// <summary>
		/// 不良发现资源
		/// </summary>
		[FieldMapAttribute("FrmResCode", typeof(string), 40, true)]
		public string FrmResCode;

		/// <summary>
		/// 不良发现日期
		/// </summary>
		[FieldMapAttribute("ShiftDay", typeof(int), 40, true)]
		public int ShiftDay;
	}

	public class RPTCenterWeekProduct : DomainObject
	{
		/// <summary>
		/// 产品序列号
		/// </summary>
		[FieldMapAttribute("RCard", typeof(string), 40, true)]
		public string RCard;

		/// <summary>
		/// 产品
		/// </summary>
		[FieldMapAttribute("ItemCode", typeof(string), 40, true)]
		public string ItemCode;

		/// <summary>
		/// 产线
		/// </summary>
		[FieldMapAttribute("FrmSSCode", typeof(string), 40, true)]
		public string FrmSSCode;

		/// <summary>
		/// 不良发现人员
		/// </summary>
		[FieldMapAttribute("FrmUser", typeof(string), 40, true)]
		public string FrmUser;

		/// <summary>
		/// 不良发现工序
		/// </summary>
		[FieldMapAttribute("FrmOPCode", typeof(string), 40, true)]
		public string FrmOPCode;

		/// <summary>
		/// 不良发现资源
		/// </summary>
		[FieldMapAttribute("FrmResCode", typeof(string), 40, true)]
		public string FrmResCode;

		/// <summary>
		/// 不良发现日期
		/// </summary>
		[FieldMapAttribute("ShiftDay", typeof(int), 40, true)]
		public int ShiftDay;
	}

	public class RPTCenterMonthProduct : DomainObject
	{
		/// <summary>
		/// 产品序列号
		/// </summary>
		[FieldMapAttribute("RCard", typeof(string), 40, true)]
		public string RCard;

		/// <summary>
		/// 产品
		/// </summary>
		[FieldMapAttribute("ItemCode", typeof(string), 40, true)]
		public string ItemCode;

		/// <summary>
		/// 产线
		/// </summary>
		[FieldMapAttribute("FrmSSCode", typeof(string), 40, true)]
		public string FrmSSCode;

		/// <summary>
		/// 不良发现人员
		/// </summary>
		[FieldMapAttribute("FrmUser", typeof(string), 40, true)]
		public string FrmUser;

		/// <summary>
		/// 不良发现工序
		/// </summary>
		[FieldMapAttribute("FrmOPCode", typeof(string), 40, true)]
		public string FrmOPCode;

		/// <summary>
		/// 不良发现资源
		/// </summary>
		[FieldMapAttribute("FrmResCode", typeof(string), 40, true)]
		public string FrmResCode;

		/// <summary>
		/// 不良发现日期
		/// </summary>
		[FieldMapAttribute("ShiftDay", typeof(int), 40, true)]
		public int ShiftDay;
	}

	public class RPTFactoryWeekCheck : DomainObject
	{
		/// <summary>
		/// 序列号
		/// </summary>
		[FieldMapAttribute("FactoryID", typeof(string), 40, true)]
		public string FactoryID;

		/// <summary>
		/// 上周总产量
		/// </summary>
		[FieldMapAttribute("LastTotal", typeof(decimal), 10, true)]
		public decimal LastTotal;
		
		/// <summary>
		/// 上周LRR
		/// </summary>
		[FieldMapAttribute("LastLRR", typeof(decimal), 10, true)]
		public decimal LastLRR;

		/// <summary>
		/// 本周总产量
		/// </summary>
		[FieldMapAttribute("NowTotal", typeof(decimal), 10, true)]
		public decimal NowTotal;
		
		/// <summary>
		/// 本周LRR
		/// </summary>
		[FieldMapAttribute("NowLRR", typeof(decimal), 10, true)]
		public decimal NowLRR;
	}

    public class RptAchievingRate : DomainObject
    {
        [FieldMapAttribute("SHIFTDAY", typeof(int), 22, true)]
        public int ShiftDay;

        [FieldMapAttribute("ACHIEVINGQTY", typeof(decimal), 10, true)]
        public decimal AchievingQty;

        [FieldMapAttribute("PLANQTY", typeof(decimal), 10, true)]
        public decimal PlanQty;

        [FieldMapAttribute("ACHIEVINGRATE", typeof(decimal), 10, true)]
        public decimal AchievingRate;
    } 

    public class RptMOCloseRate : DomainObject
    {
        [FieldMapAttribute("YEAR", typeof(int), 22, true)]
        public int Year;

        [FieldMapAttribute("DMONTH", typeof(int), 22, true)]
        public int Month;

        [FieldMapAttribute("DWEEK", typeof(int), 22, true)]
        public int Week;

        [FieldMapAttribute("FIRSTPQTY", typeof(int), 22, true)]
        public int FirstPQty;

        [FieldMapAttribute("OPENQTY", typeof(int), 22, true)]
        public int OpenQty;

        [FieldMapAttribute("CLOSEQTY", typeof(int), 22, true)]
        public int CloseQty;

        [FieldMapAttribute("ENDPQTY", typeof(int), 22, true)]
        public int EndPQty;

        [FieldMapAttribute("CLOSESORATE", typeof(decimal), 10, true)]
        public decimal CloseRate;
    } 
}
