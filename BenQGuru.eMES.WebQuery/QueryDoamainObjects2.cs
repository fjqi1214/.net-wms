using System;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.WebQuery
{

    #region 产品追溯

    /// <summary>
    /// 产品追溯
    /// </summary>
    [Serializable, TableMap("TBLSIMULATIONREPORT", "RCARD,MOCODE")]
    public class ItemTracing : DomainObject
    {
        /// <summary>
        /// 序列号
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string  RCard;

		/// <summary>
		/// 序列号
		/// </summary>
		[FieldMapAttribute("TCARD", typeof(string), 40, true)]
		public string  TCard;

        /// <summary>
        /// 序列号
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(string), 40, true)]
        public decimal  RCardSeq;

        /// <summary>
        /// 产品
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string  ItemCode;

        /// <summary>
        /// 产品状态
        /// </summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, true)]
        public string  ItemStatus;

        /// <summary>
        /// 工单
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string  MOCode;

        /// <summary>
        /// 机种
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string  ModelCode;

        /// <summary>
        /// 所在工序
        /// </summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, true)]
        public string  OPCode;

        /// <summary>
        /// 工序类型
        /// </summary>
        [FieldMapAttribute("OPTYPE", typeof(string), 40, true)]
        public string  OPType;
    
        /// <summary>
        /// 生产途程
        /// </summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, true)]
        public string  RouteCode;

        /// <summary>
        /// 工段
        /// </summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        public string  SegmentCode;

        /// <summary>
        /// 生产线
        /// </summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string  LineCode;

        /// <summary>
        /// 资源
        /// </summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, true)]
        public string  ResCode;

        /// <summary>
        /// 日期
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int  MaintainDate;

        /// <summary>
        /// 时间
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int  MaintainTime;

        /// <summary>
        /// 操作工
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string  MaintainUser;

		/// <summary>
		/// 最后操作工序
		/// </summary>
		[FieldMapAttribute("LACTION", typeof(string), 40, true)]
		public string  LastAction;
    }

    public class ItemTracingQuery : ItemTracing
    {
        [FieldMapAttribute("MMODELCODE", typeof(string), 40, true)]
        public string MaterialModelCode;

        [FieldMapAttribute("BIGSSCODE", typeof(string), 40, true)]
        public string BigStepSequenceCode;
    }

    #endregion

    #region 生产过程

    /// <summary>
    /// 生产过程
    /// </summary>
    public class ProductionProcess : DomainObject
    {
        /// <summary>
        /// 序列号
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string  RCard;

        /// <summary>
        /// 序列号顺序
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(string), 40, true)]
        public decimal  RCardSequence;


        /// <summary>
        /// 生产途程
        /// </summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, true)]
        public string  RouteCode;

        /// <summary>
        /// 工序
        /// </summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, true)]
        public string  OPCode;

        /// <summary>
        /// 工单
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string  MOCode;

        /// <summary>
        /// 机种
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string  ModelCode;

        /// <summary>
        /// 产品
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string  ItemCode;

        /// <summary>
        /// 产品状态
        /// </summary>
        [FieldMapAttribute("ACTIONRESULT", typeof(string), 40, true)]
        public string  ItemStatus;

        /// <summary>
        /// 工序类型
        /// </summary>
        [FieldMapAttribute("OPTYPE", typeof(string), 40, true)]
        public string  OPType;

        /// <summary>
        /// 工段
        /// </summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        public string  SegmentCode;

        /// <summary>
        /// 生产线
        /// </summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string  LineCode;

        /// <summary>
        /// 资源
        /// </summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, true)]
        public string  ResCode;

        /// <summary>
        /// 日期
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int  MaintainDate;

        /// <summary>
        /// 时间
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int  MaintainTime;

        /// <summary>
        /// 操作工
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string  MaintainUser;

		/// <summary>
		/// 操作工序
		/// </summary>
		[FieldMapAttribute("ACTION", typeof(string), 40, true)]
		public string  Action;


    }



    #endregion

    #region 工序结果
    /// <summary>
    /// 工序结果
    /// </summary>
    public class OPResult : DomainObject
    {
        /// <summary>
        /// 序列号
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string  RCard;

        /// <summary>
        /// 序列号顺序
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(string), 40, true)]
        public decimal  RCardSequence;


        /// <summary>
        /// 生产途程
        /// </summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, true)]
        public string  RouteCode;

        /// <summary>
        /// 工序
        /// </summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, true)]
        public string  OPCode;

        /// <summary>
        /// 工单
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string  MOCode;

        /// <summary>
        /// 机种
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string  ModelCode;

        /// <summary>
        /// 产品
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string  ItemCode;

        /// <summary>
        /// 产品状态
        /// </summary>
        [FieldMapAttribute("ACTIONRESULT", typeof(string), 40, true)]
        public string  ItemStatus;

        /// <summary>
        /// 工序类型
        /// </summary>
        [FieldMapAttribute("OPTYPE", typeof(string), 40, true)]
        public string  OPType;

        /// <summary>
        /// 工段
        /// </summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        public string  SegmentCode;

        /// <summary>
        /// 生产线
        /// </summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string  LineCode;

        /// <summary>
        /// 资源
        /// </summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, true)]
        public string  ResCode;

        /// <summary>
        /// 日期
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int  MaintainDate;

        /// <summary>
        /// 时间
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int  MaintainTime;

        /// <summary>
        /// 操作工
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string  MaintainUser;
		
		/// <summary>
		/// CARTON号
		/// </summary>
		[FieldMapAttribute("CARTONCODE", typeof(string), 40, false)]
		public string  CartonCode;

		/// <summary>
		/// ShelfPK
		/// </summary>
		[FieldMapAttribute("SHELFNO", typeof(string), 40, false)]
		public string  ShelfPK;

    }

    #endregion

    #region 集成上料号

    /// <summary>
    /// 集成上料号
    /// </summary>
    public class LotItemInfo : DomainObject
    {
        /// <summary>
        /// 集成上料号
        /// </summary>
        [FieldMapAttribute("MCARD", typeof(string), 40, true)]
        public string MCard ;
    
        /// <summary>
        /// 厂内料号
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode ;

        /// <summary>
        /// 厂商
        /// </summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 40, true)]
        public string Customer ;

        /// <summary>
        /// 厂商料号
        /// </summary>
        [FieldMapAttribute("VENDORITEMCODE", typeof(string), 40, true)]
        public string CustomerItem ;

        /// <summary>
        /// 生产批号
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, true)]
        public string LotNO ;

        /// <summary>
        /// 生产日期
        /// </summary>
        [FieldMapAttribute("DATECODE", typeof(string), 40, true)]
        public string DateCode ;

        /// <summary>
        /// 规格
        /// </summary>
        [FieldMapAttribute("VERSION", typeof(string), 40, true)]
        public string Version ;


        [FieldMapAttribute("PCBA", typeof(string), 40, true)]
        public string PCBA ;


        [FieldMapAttribute("BIOS", typeof(string), 40, true)]
        public string BIOS ;

        [FieldMapAttribute("TRYITEMCODE", typeof(string), 40, true)]
        public string TryItemCode ;

    }


    #endregion

    #region 单件料
    /// <summary>
    /// 单件料
    /// </summary>
    public class RunningItemInfo : DomainObject
    {
        /// <summary>
        /// 原始序列号
        /// </summary>
        [FieldMapAttribute("MCARD", typeof(string), 40, true)]
        public string MCARD ;
    
        /// <summary>
        /// 厂内料号
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode ;

        /// <summary>
        /// 厂商
        /// </summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 40, true)]
        public string Customer ;

        /// <summary>
        /// 厂商料号
        /// </summary>
        [FieldMapAttribute("VENDORITEMCODE", typeof(string), 40, true)]
        public string CustomerItem ;

        /// <summary>
        /// 生产批号
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, true)]
        public string LotNO ;

        /// <summary>
        /// 生产日期
        /// </summary>
        [FieldMapAttribute("DATECODE", typeof(string), 40, true)]
        public string DateCode ;

        /// <summary>
        /// 规格
        /// </summary>
        [FieldMapAttribute("VERSION", typeof(string), 40, true)]
        public string Version ;


        [FieldMapAttribute("PCBA", typeof(string), 40, true)]
        public string PCBA ;


        [FieldMapAttribute("BIOS", typeof(string), 40, true)]
        public string BIOS ;

        [FieldMapAttribute("TRACINGABLE", typeof(bool), 40, true)]
        public bool CanTracing ;

    }
    #endregion

    #region 序列号转换
    /// <summary>
    /// 序列号转换
    /// </summary>
    public class SNInfo : DomainObject
    {
        /// <summary>
        /// 原始序列号
        /// </summary>
        [FieldMapAttribute("SCARD", typeof(string), 40, true)]
        public string SourceCard ;
    
        /// <summary>
        /// 新序列号
        /// </summary>
        [FieldMapAttribute("TCARD", typeof(string), 40, true)]
        public string TranslateCard ;

        /// <summary>
        /// 新序列号
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard ;



    }

    #endregion

    #region 包装

    public class PackingInfo : DomainObject
    {
        /// <summary>
        /// 原始序列号
        /// </summary>
        [FieldMapAttribute("SCARD", typeof(string), 40, true)]
        public string SourceCard ;
    
        /// <summary>
        /// 新序列号
        /// </summary>
        [FieldMapAttribute("TCARD", typeof(string), 40, true)]
        public string TranslateCard ;


    }


    #endregion

	#region 维修工序结果
	/// <summary>
	/// 维修工序结果
	/// </summary>
	public class TSOPResult : DomainObject
	{
		/// <summary>
		/// 序列号
		/// </summary>
		[FieldMapAttribute("RCARD", typeof(string), 40, true)]
		public string  RCard;

		/// <summary>
		/// 序列号顺序
		/// </summary>
		[FieldMapAttribute("RCARDSEQ", typeof(string), 40, true)]
		public decimal  RCardSequence;


		/// <summary>
		/// 生产途程
		/// </summary>
		[FieldMapAttribute("FRMROUTECODE", typeof(string), 40, true)]
		public string  RouteCode;

		/// <summary>
		/// 工序
		/// </summary>
		[FieldMapAttribute("COPCODE", typeof(string), 40, true)]
		public string  OPCode;

		/// <summary>
		/// 产品状态
		/// </summary>
		[FieldMapAttribute("TSSTATUS", typeof(string), 40, true)]
		public string  ItemStatus;

		/// <summary>
		/// 工序类型
		/// </summary>
		[FieldMapAttribute("OPTYPE", typeof(string), 40, true)]
		public string  OPType;

		/// <summary>
		/// 工段
		/// </summary>
		[FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
		public string  SegmentCode;

		/// <summary>
		/// 生产线
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string  LineCode;

		/// <summary>
		/// 资源
		/// </summary>
		[FieldMapAttribute("CRESCODE", typeof(string), 40, true)]
		public string  ResCode;

		/// <summary>
		/// 日期
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 时间
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 操作工
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
		public string  MaintainUser;


	}

	#endregion

	#region 维修信息
	/// <summary>
	/// 维修信息
	/// </summary>
	public class TSInfo : DomainObject
	{
		/// <summary>
		/// 不良代码组
		/// </summary>
		[FieldMapAttribute("ECGCODE", typeof(string), 40, true)]
		public string  ErrorCodeGroup;

		/// <summary>
		/// 不良代码
		/// </summary>
		[FieldMapAttribute("ECODE", typeof(string), 40, true)]
		public string  ErrorCode;


		/// <summary>
		/// 不良原因
		/// </summary>
		[FieldMapAttribute("ECSCODE", typeof(string), 40, true)]
		public string  ErrorCauseCode;

		/// <summary>
		/// 不良位置
		/// </summary>
		[FieldMapAttribute("ELOC", typeof(string), 40, true)]
		public string  ErrorLocation;

		/// <summary>
		/// 不良零件
		/// </summary>
		[FieldMapAttribute("EPART", typeof(string), 40, true)]
		public string  ErrorPart;

		/// <summary>
		/// 解决方案
		/// </summary>
		[FieldMapAttribute("SOLCODE", typeof(string), 40, true)]
		public string  SolutionCode;

		/// <summary>
		/// 责任别
		/// </summary>
		[FieldMapAttribute("DUTYCODE", typeof(string), 40, true)]
		public string  DutyCode;

		/// <summary>
		/// 补充说明
		/// </summary>
		[FieldMapAttribute("SOLMEMO", typeof(string), 40, true)]
		public string  SolutionMemo;

		/// <summary>
		/// 维修工
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// 维修日期
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(string), 40, true)]
		public int  MaintainDate;

		/// <summary>
		/// 维修时间
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(string), 40, true)]
		public int  MaintainTime;
	}

	#endregion

	#region 维修信息
	/// <summary>
	/// 换料信息
	/// </summary>
	public class HLInfo : DomainObject
	{

		/// <summary>
		/// 新物料料号
		/// </summary>
		[FieldMapAttribute("MITEMCODE", typeof(string), 40, true)]
		public string  MItemCode;

		/// <summary>
		/// 新物料序列号
		/// </summary>
		[FieldMapAttribute("MCARD", typeof(string), 40, true)]
		public string  MCard;

		/// <summary>
		/// 原物料料号
		/// </summary>
		[FieldMapAttribute("SITEMCODE", typeof(string), 40, false)]
		public string  SourceItemCode;

		/// <summary>
		/// 原物料序列号
		/// </summary>
		[FieldMapAttribute("MSCARD", typeof(string), 40, false)]
		public string  MSourceCard;

		/// <summary>
		/// 部件位置
		/// </summary>
		[FieldMapAttribute("LOC", typeof(string), 40, true)]
		public string  Location;

		/// <summary>
		/// 批号
		/// </summary>
		[FieldMapAttribute("LOTNO", typeof(string), 40, true)]
		public string  LotNO;

		/// <summary>
		/// 厂商
		/// </summary>
		[FieldMapAttribute("VENDORCODE", typeof(string), 40, true)]
		public string  VendorCode;

		/// <summary>
		/// 厂商料号
		/// </summary>
		[FieldMapAttribute("VENDORITEMCODE", typeof(string), 40, true)]
		public string  VendorItemCode;

		/// <summary>
		/// 生产日期
		/// </summary>
		[FieldMapAttribute("DATECODE", typeof(string), 40, true)]
		public string  DateCode;

		/// <summary>
		/// 料品版本
		/// </summary>
		[FieldMapAttribute("REVERSION", typeof(string), 40, true)]
		public string  Version;

		/// <summary>
		/// PCBA版本
		/// </summary>
		[FieldMapAttribute("PCBA", typeof(string), 40, true)]
		public string  PCBA;

		/// <summary>
		/// BIOS版本
		/// </summary>
		[FieldMapAttribute("BIOS", typeof(string), 40, true)]
		public string  BIOS;

		/// <summary>
		/// 补充说明
		/// </summary>
		[FieldMapAttribute("MEMO", typeof(string), 40, true)]
		public string  Memo;
	}

	#endregion

	#region OQC 抽检

	public class OQCLRR : DomainObject
	{
		[FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
		public string  ModelCode;

		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string  ItemCode;

		/// <summary>
		/// 抽检批的批数量
		/// </summary>
		[FieldMapAttribute("LOTTOTALCOUNT", typeof(decimal), 15, true)]
		public decimal  LotTotalCount;

		/// <summary>
		/// 批退的批数量
		/// </summary>
		[FieldMapAttribute("LOTNGCOUNT", typeof(decimal), 15, true)]
		public decimal  LotNGCount;

		[FieldMapAttribute("DATEGROUP", typeof(int), 15, true)]
		public int DateGroup;

		[FieldMapAttribute("LRR", typeof(decimal), 15, true)]
		public decimal  LRR
		{
			get
			{
				if(LotTotalCount == 0)
				{
					return 0;
				}
				return Math.Round(LotNGCount/LotTotalCount,4);
			}
		}

		/// <summary>
		/// 样本总数
		/// </summary>
		[FieldMapAttribute("LOTSAMPLECOUNT", typeof(decimal), 15, true)]
		public decimal  LotSampleCount;

		/// <summary>
		/// 样本不良总数
		/// </summary>
		[FieldMapAttribute("LOTSAMPLENGCOUNT", typeof(decimal), 15, true)]
		public decimal  LotSampleNGCount;

		[FieldMapAttribute("DPPM", typeof(decimal), 15, true)]
		public decimal  DPPM
		{
			get
			{
				if(LotSampleCount == 0)
				{
					return 0;
				}
				return Convert.ToInt32((LotSampleNGCount/LotSampleCount) * 1000000);
			}
		}

		/// <summary>
		/// 送检总数
		/// </summary>
		[FieldMapAttribute("LOTSIZE", typeof(decimal), 15, true)]
		public decimal  LotSize;
		
	}

	public class OQCSDR : DomainObject
	{
		[FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
		public string  ModelCode;

		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string  ItemCode;

		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string  MOCode;

		/// <summary>
		/// 抽检批的产品数量
		/// </summary>
		[FieldMapAttribute("SAMPLECOUNT", typeof(decimal), 15, true)]
		public decimal  SampleCount;

		/// <summary>
		/// 抽检批的不良产品数量
		/// </summary>
		[FieldMapAttribute("SAMPLENGCOUNT", typeof(decimal), 15, true)]
		public decimal  SampleNGCount;

		[FieldMapAttribute("DATEGROUP", typeof(int), 15, true)]
		public int DateGroup;

		[FieldMapAttribute("LRR", typeof(decimal), 15, true)]
		public decimal  SDR
		{
			get
			{
				if(SampleCount == 0)
				{
					return 0;
				}
				return Math.Round((SampleNGCount/SampleCount)*1000000,0);
			}
		}
		
	}

	public class OQCErrorCode : DomainObject
	{
		[FieldMapAttribute("ECODE", typeof(string), 40, true)]
		public string  ErrorCode;

		[FieldMapAttribute("ECDESC", typeof(string), 40, true)]
		public string  ErrorCodeDesc;

		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string  ItemCode;

		[FieldMapAttribute("ErrorCodeCardQty", typeof(decimal), 15, true)]
		public decimal  ErrorCodeCardQty;

		[FieldMapAttribute("ItemCardQty", typeof(decimal), 15, true)]
		public decimal  ItemCardQty;
		
	}
     

	#endregion
}
