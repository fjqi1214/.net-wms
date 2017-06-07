using System;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.SAPData
{
	#region 枚举类型

	//获取数据的参数
	public enum DataName
	{
		SAPMO,			//工单
		SAPMOBom,		//工单Bom
		SAPBom			//BOM
	}

	//JobClass 日志类型
	public enum JobClass
	{
		SAP ,		//SAP
		MESMOBILE,			//MESMOBILE		MES手机
		MESSMT,			    //MESSMT		MES实装(SMT)
		MESNOTEBOOK,		//MESNOTEBOOK	MES 笔记本
		MESNOTEBOOKSMT		//MESNOTEBOOK	MES 笔记本SMT
	}

	//JobName 日志名称
	public enum JobName
	{
		BOM ,		//BOM
		MO ,		//工单
		MOBom 		//工单bom
	}

	//JobActionResult 导入结果
	public enum JobActionResult
	{
		ADD ,			//新增
		UPDATE ,		//修改
		IGNORE			//忽略
	}

	#endregion

	#region 导出数据接口

	//导入实体接口,所有从SAP数据取的数据必须继承自此接口
	public interface IImport
	{
	
	}

	#endregion

	#region SAP JobLog 对象

	/// <summary>
	/// JobLog
	/// </summary>
	[Serializable, TableMap("joblog","JOBCLASS,JOBNAME,STARTTIME,ENDTIME")]
	public class JobLog : DomainObject
	{
		public JobLog()
		{
		}
		
		/// <summary>
		/// JOBCLASS job类型
		/// </summary>
		[FieldMapAttribute("JOBCLASS", typeof(string), 15, true)]
		public string  JobClass;

		/// <summary>
		/// JOBNAME job名称
		/// </summary>
		[FieldMapAttribute("JOBNAME", typeof(string), 50, true)]
		public string  JobName;

		/// <summary>
		/// STARTTIME 开始时间
		/// </summary>
		[FieldMapAttribute("STARTTIME", typeof(DateTime), 7, true)]
		public DateTime  StartTime;

		/// <summary>
		/// ENDTIME 结束时间
		/// </summary>
		[FieldMapAttribute("ENDTIME", typeof(DateTime), 7, true)]
		public DateTime  EndTime;

		/// <summary>
		/// RESULTCODE 
		/// </summary>
		[FieldMapAttribute("RESULTCODE", typeof(int), 13, true)]
		public int  Resultcode;

		/// <summary>
		/// RESULTDESC
		/// </summary>
		[FieldMapAttribute("RESULTDESC", typeof(string), 2000, true)]
		public string  Resultdesc;
		
	}

	#endregion

	#region SAPMO  SAP工单

	/// <summary>
	/// 工单
	/// </summary>
	[Serializable, TableMap("WO", "AUFNR")]
	public class SAPMO : DomainObject,IImport
	{
		public SAPMO()
		{
		}
 
		//工单	 工厂	父物料号	订单类型	订单数量	收货数量	订单开始日期	订单完成日期	计量单位
		//AUFNR	 WERKS	FMATNR	   AUART	    PSMNG	    WEMNG	    GSTRP	        GLTRP	         GMEIN
		//1000022724	1105	GSM6106SR04	PP01	1,583.00	1,583.00	2005.09.15	2005.09.19	EA

		//导入数据格式
		//生产订单号 ,生产工厂,产品物料号,订单类型 ,订单数量,预计开始日期 ,预计完成日期 ,客户代码,客户单号 ,备注

		#region 映射MO的字段

		/// <summary>
		/// 工单号
		/// </summary>
		[FieldMapAttribute("AUFNR", typeof(string), 12, true)]
		public string  MOCode;

		/// <summary>
		/// 工厂
		/// </summary>
		[FieldMapAttribute("WERKS", typeof(string), 4, false)]
		public string  Factory;

		/// <summary>
		/// 父物料号
		/// </summary>
		[FieldMapAttribute("FMATNR", typeof(string), 18, true)]
		public string  ItemCode;

		/// <summary>
		/// 订单类型
		/// </summary>
		[FieldMapAttribute("AUART", typeof(string), 4, true)]
		public string  MOType;

		/// <summary>
		/// 订单数量
		/// </summary>
		[FieldMapAttribute("PSMNG", typeof(string), 13, true)]
		public string  MOPlanQty;

		/// <summary>
		/// 订单开始日期
		/// </summary>
		[FieldMapAttribute("GSTRP", typeof(DateTime), 8, true)]
		public DateTime  MOPlanStartDate;

		/// <summary>
		/// 订单完成日期
		/// </summary>
		[FieldMapAttribute("GLTRP", typeof(DateTime), 8, true)]
		public DateTime  MOPlanEndDate;

		#endregion

		/// <summary>
		/// 收货数量
		/// </summary>
		[FieldMapAttribute("WEMNG", typeof(string), 13, true)]
		public string  MOOutputQty;

		/// <summary>
		/// 计量单位
		/// </summary>
		[FieldMapAttribute("GMEIN", typeof(string), 3, true)]
		public string  ItemUOM;
		
	}


	#endregion

	#region SAPMOBom  SAP 工单bom

	/// <summary>
	/// SAP 产品
	/// </summary>
	[Serializable, TableMap("WODETAIL", "AUFNR")]
	public class SAPMOBom : DomainObject,IImport
	{
		public SAPMOBom()
		{
		}

		#region sap WODETAIL

		//订单号	物料号码	发料工厂	工序	需求数量	领料数量	替代项	优先级	物料类型	计量单位
		//AUFNR		MATNR		PWERK		VORNR	BDMNG		ENMNG		ALPGR	ZPRI	ZFLAG		GMEIN

		#endregion

		#region 映射MOBom的字段

		/// <summary>
		/// 订单号
		/// </summary>
		[FieldMapAttribute("AUFNR", typeof(string), 12, true)]
		public string  MOCode;

		/// <summary>
		/// 父物料号
		/// </summary>
		[FieldMapAttribute("FMATNR", typeof(string), 18, true)]
		public string  ItemCode;

		/// <summary>
		/// 物料号码
		/// </summary>
		[FieldMapAttribute("MATNR", typeof(string), 18, true)]
		public string  MOBOMItemCode;

		/// <summary>
		/// 物料名称
		/// </summary>
		[FieldMapAttribute("maktx", typeof(string), 40, true)]
		public string  MOBOMItemName;

		/// <summary>
		/// 物料类型
		/// </summary>
		[FieldMapAttribute("ZFLAG", typeof(string), 1, true)]
		public string  MOBOMItemType;

		/// <summary>
		/// 计量单位
		/// </summary>
		[FieldMapAttribute("GMEIN", typeof(string), 6, true)]
		public string  MOBOMItemUOM;

		#endregion

		/// <summary>
		/// 发料工厂
		/// </summary>
		[FieldMapAttribute("PWERK", typeof(string), 4, true)]
		public string  Factory;

		/// <summary>
		/// 工序
		/// </summary>
		[FieldMapAttribute("VORNR", typeof(string), 4, true)]
		public string  OPCode;

		/// <summary>
		/// 需求数量
		/// </summary>
		[FieldMapAttribute("BDMNG", typeof(int), 13, true)]
		public int  RequireQty;

		/// <summary>
		/// 领料数量
		/// </summary>
		[FieldMapAttribute("ENMNG", typeof(int), 13, true)]
		public int  DrawQTY;

		/// <summary>
		/// 替代项
		/// </summary>
		[FieldMapAttribute("ALPGR", typeof(string), 10, true)]
		public string  PItem;

		/// <summary>
		/// 优先级
		/// </summary>
		[FieldMapAttribute("ZPRI", typeof(string), 2, true)]
		public string  PRI;

		/// <summary>
		/// 工单计划数量
		/// </summary>
		public decimal  MOPlanQty = 0;

		/// <summary>
		/// 单机用量
		/// </summary>
		public decimal UnitageQty
		{
			get
			{
				if(this.MOPlanQty!=0)
				{
					return Convert.ToDecimal(this.RequireQty/this.MOPlanQty);
				}
				return 0;
			}
		}

	}


	#endregion

	#region SAPBOM  SAP BOM

	/// <summary>
	/// SAP BOM
	/// </summary>
	[Serializable, TableMap("BOM", "AUFNR")]
	public class SAPBOM : DomainObject,IImport
	{
		public SAPBOM()
		{
		}
		

		//父夏新码	父物料工厂	子物料号	子物料工厂	生效日期	失效日期	单位	单机用量	替代项目组	 使用可能性	工序		最后更新日期	物料类型	ECN	   优先级
		//FMATNR	FWERKS		MATNR		PWERK		DATUV		DATUB		GMEIN	MENGE		ALPGR		 EWAHR		VORNR		LASTUPDATE		ZFLAG		AENNR  ZPRI

		#region 映射BOM的字段

		/// <summary>
		/// 父夏新码
		/// </summary>
		[FieldMapAttribute("FMATNR", typeof(string), 18, true)]
		public string  FItemCode;

		/// <summary>
		/// 子物料号
		/// </summary>
		[FieldMapAttribute("MATNR", typeof(string), 13, true)]
		public string  SBOMItemCode;

		/// <summary>
		/// 物料名称
		/// </summary>
		[FieldMapAttribute("MAKTX", typeof(string), 40, true)]
		public string  SOBOMItemName;

		/// <summary>
		/// 生效日期
		/// </summary>
		[FieldMapAttribute("DATUV", typeof(DateTime), 7, true)]
		public DateTime  SBOMItemEffectiveDate;

		/// <summary>
		/// 失效日期
		/// </summary>
		[FieldMapAttribute("DATUB", typeof(DateTime), 7, true)]
		public DateTime  SBOMItemInvalidDate;

		/// <summary>
		/// 单位
		/// </summary>
		[FieldMapAttribute("GMEIN", typeof(string), 3, true)]
		public string  ItemUOM;

		/// <summary>
		/// 单机用量
		/// </summary>
		[FieldMapAttribute("MENGE", typeof(decimal), 15, true)]
		public decimal  SBOMItemQty;

		/// <summary>
		/// 替代项目组
		/// </summary>
		[FieldMapAttribute("ALPGR", typeof(string), 10, true)]
		public string ALPGR = string.Empty;

		/// <summary>
		/// ECN
		/// </summary>
		[FieldMapAttribute("AENNR", typeof(string), 12, true)]
		public string  SBOMItemECN;

		#endregion

		/// <summary>
		/// 父物料工厂
		/// </summary>
		[FieldMapAttribute("FWERKS", typeof(string), 4, true)]
		public string  FFacatory;

		
		/// <summary>
		/// 子物料工厂
		/// </summary>
		[FieldMapAttribute("PWERK", typeof(string), 13, true)]
		public string  SFactory;
		

		/// <summary>
		/// 使用可能性
		/// </summary>
		[FieldMapAttribute("EWAHR", typeof(string), 13, true)]
		public string  EWAHR;


		/// <summary>
		/// 工序
		/// </summary>
		[FieldMapAttribute("VORNR", typeof(string), 13, true)]
		public string  OP;

		/// <summary>
		/// 最后更新日期
		/// </summary>
		[FieldMapAttribute("LASTUPDATE", typeof(DateTime), 7, true)]
		public DateTime  LastUpdate;

		/// <summary>
		/// 物料类型
		/// </summary>
		[FieldMapAttribute("ZFLAG", typeof(string), 1, true)]
		public string  ItemType;

		/// <summary>
		/// 优先级
		/// </summary>
		[FieldMapAttribute("ZPRI", typeof(string), 2, true)]
		public string  ZPRI;
		
	}

	#endregion

	#region SAPBOM  SAP Item

	/// <summary>
	/// SAP BOM
	/// </summary>
	[Serializable, TableMap("ITEM", "MATNR,WERKS")]
	public class SAPItem : DomainObject,IImport
	{
		public SAPItem()
		{
		}
		//物料工厂	子物料号	物料名称
		//WERKS		MATNR		MAKTX	

		/// <summary>
		/// 子物料号
		/// </summary>
		[FieldMapAttribute("MATNR", typeof(string), 18, true)]
		public string  MATNR;

		/// <summary>
		/// 物料名称
		/// </summary>
		[FieldMapAttribute("MAKTX", typeof(string), 40, true)]
		public string  MAKTX;

		/// <summary>
		/// 父物料工厂
		/// </summary>
		[FieldMapAttribute("WERKS", typeof(string), 4, true)]
		public string  WERKS;
		
	}

	#endregion
	
	
}
