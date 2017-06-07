using System;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.DataImportConsole
{
	#region ImpItem
	/// <summary>
	/// 料号
	/// </summary>
	[Serializable, TableMap("siim", "iprod,idep")]
	public class ImpItem : DomainObject
	{
		public ImpItem()
		{
		}

		[FieldMapAttribute("iprod", typeof(string), 40, true)]
		public string  ItemCode;

		/// <summary>
		/// 料品描述[ItemDesc]
		/// </summary>
		[FieldMapAttribute("idesc", typeof(string), 100, false)]
		public string  ItemDescription;

		/// <summary>
		/// 料品类别[ItemType]
		/// </summary>
		[FieldMapAttribute("ityp", typeof(string), 40, true)]
		public string  ItemType;

		/// <summary>
		/// 料品名称[ItemName]
		/// </summary>
		[FieldMapAttribute("iname", typeof(string), 100, false)]
		public string  ItemName;

		/// <summary>
		/// 计量单位[ItemUOM]
		/// </summary>
		[FieldMapAttribute("iumn", typeof(string), 40, true)]
		public string  ItemUOM;

		/// <summary>
		/// 所属工厂
		/// </summary>
		[FieldMapAttribute("idep", typeof(string), 40, true)]
		public string  Factory;

		
	}
	#endregion

	#region ImpModel2Item
	/// <summary>
	/// 料号
	/// </summary>
	[Serializable, TableMap("siim", "imodl,Iprod,idep")]
	public class ImpModel2Item : DomainObject
	{
		public ImpModel2Item()
		{
		}

		/// <summary>
		/// 机种代码[Model]
		/// </summary>
		[FieldMapAttribute("imodl", typeof(string), 40, true)]
		public string  ModelCode;

		/// <summary>
		/// 料号[ItemCode]
		/// </summary>
		[FieldMapAttribute("Iprod", typeof(string), 40, true)]
		public string  ItemCode;

		/// <summary>
		/// 所属工厂
		/// </summary>
		[FieldMapAttribute("idep", typeof(string), 40, true)]
		public string  Factory;

	}
	#endregion

	#region ImpSBOM
	/// <summary>
	/// [SBOM]
	/// </summary>
	[Serializable, TableMap("sbom", "bprod,bdep,bfac,bitem,bchld")]
	public class ImpSBOM : DomainObject
	{
		public ImpSBOM()
		{
		}
 
		/// <summary>
		/// ECN
		/// </summary>
		[FieldMapAttribute("becn", typeof(string), 40, false)]
		public string  SBOMItemECN;

		/// <summary>
		/// 物料代码
		/// </summary>
		[FieldMapAttribute("bchld", typeof(string), 40, true)]
		public string  SBOMItemCode;

		/// <summary>
		/// 物料名称
		/// </summary>
		[FieldMapAttribute("bdesc", typeof(string), 40, false)]
		public string  SBOMItemName;

		/// <summary>
		/// 生效日期
		/// </summary>
		[FieldMapAttribute("bdeff", typeof(int), 8, true)]
		public int  SBOMItemEffectiveDate;

		/// <summary>
		/// 失效日期
		/// </summary>
		[FieldMapAttribute("bdiss", typeof(int), 8, true)]
		public int  SBOMItemInvalidDate;

		/// <summary>
		/// 位号
		/// </summary>
		[FieldMapAttribute("bpn", typeof(string), 40, false)]
		public string  SBOMItemLocation;

		/// <summary>
		/// 产品代码
		/// </summary>
		[FieldMapAttribute("bprod", typeof(string), 40, true)]
		public string  ItemCode;


		/// <summary>
		/// 计量单位
		/// </summary>
		[FieldMapAttribute("bumn", typeof(string), 40, true)]
		public string  SBOMItemUOM;

		/// <summary>
		/// 单位用量
		/// </summary>
		[FieldMapAttribute("bqreq", typeof(decimal), 15, true)]
		public decimal  SBOMItemQty;

		/// <summary>
		/// 首选料
		/// </summary>
		[FieldMapAttribute("bitem", typeof(string), 40, true)]
		public string  SBOMSourceItemCode;

		/// <summary>
		/// 库别
		/// </summary>
		[FieldMapAttribute("bfac", typeof(string), 40, true)]
		public string  SBOMWH;

		/// <summary>
		/// 物料父阶料号
		/// </summary>
		[FieldMapAttribute("bpars", typeof(string), 40, true)]
		public string  SBOMParentItemCode;

		/// <summary>
		/// 所属工厂
		/// </summary>
		[FieldMapAttribute("bdep", typeof(string), 40, true)]
		public string  Factory;

	}
	#endregion

	#region ImpMO
	/// <summary>
	/// 工单
	/// </summary>
	[Serializable, TableMap("sfso", "fsord,fdep")]
	public class ImpMO : DomainObject
	{
		public ImpMO()
		{
		}
 
		/// <summary>
		/// 工单号
		/// </summary>
		[FieldMapAttribute("fsord", typeof(string), 40, true)]
		public string  MOCode;
		/// <summary>
		/// 工单类型
		/// 来自系统参数的定义:
		/// 1. 系统参数类型: MOTYPE
		/// </summary>
		[FieldMapAttribute("fcom", typeof(string), 40, true)]
		public string  MOType;

		/// <summary>
		/// 预计开工日期
		/// </summary>
		[FieldMapAttribute("frdte", typeof(int), 8, true)]
		public int  MOPlanStartDate;

		/// <summary>
		/// 预计完工日期
		/// </summary>
		[FieldMapAttribute("fddte", typeof(int), 8, true)]
		public int  MOPlanEndDate;


		/// <summary>
		/// 预计产出量
		/// </summary>
		[FieldMapAttribute("fqreq", typeof(decimal), 10, true)]
		public decimal  MOPlanQty;

		/// <summary>
		/// 客户单号
		/// </summary>
		[FieldMapAttribute("fpo", typeof(string), 40, false)]
		public string  CustomerOrderNO;

		/// <summary>
		/// 客户代号
		/// </summary>
		[FieldMapAttribute("fcus", typeof(string), 40, false)]
		public string  CustomerCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("fmemo", typeof(string), 100, false)]
		public string  MOMemo;

		/// <summary>
		/// 料号[ItemCode]
		/// </summary>
		[FieldMapAttribute("fprod", typeof(string), 40, true)]
		public string  ItemCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("fdep", typeof(string), 40, true)]
		public string  Factory;

	}
	#endregion

	#region ImpMOBOM
	/// <summary>
	/// [MOBOM]
	/// </summary>
	[Serializable, TableMap("sfma", "fsord,fdep,fchld")]
	public class ImpMOBOM : DomainObject
	{
		public ImpMOBOM()
		{
		}

		/// <summary>
		/// [CItemCode]
		/// </summary>
		[FieldMapAttribute("fchld", typeof(string), 40, false)]
		public string  MOBOMItemCode;

		/// <summary>
		/// [CItemName]
		/// </summary>
		[FieldMapAttribute("fdesc", typeof(string), 40, false)]
		public string  MOBOMItemName;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("fqreq", typeof(decimal), 15, true)]
		public decimal  MOBOMItemQty;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("fitem", typeof(string), 40, false)]
		public string  MOBOMSourceItemCode;
        
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("fumn", typeof(string), 100, true)]
		public string  MOBOMItemUOM;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("fsord", typeof(string), 40, true)]
		public string  MOCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("fdep", typeof(string), 40, true)]
		public string  Factory;
	}

	#endregion

	#region ImpERPBOM
	/// <summary>
	/// [MOBOM]
	/// </summary>
	[Serializable, TableMap("lotformes", "serialno")]
	public class ImpERPBOM : DomainObject
	{
		public ImpERPBOM()
		{
		}

		/// <summary>
		/// [CItemCode]
		/// </summary>
		[FieldMapAttribute("itemno", typeof(string), 40, false)]
		public string  BITEMCODE;

		/// <summary>
		/// [QTY]
		/// </summary>
		[FieldMapAttribute("qty", typeof(decimal),15, false)]
		public decimal  BQTY;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("lot", typeof(string), 40, true)]
		public string  LOTNO;

		/// <summary>
		/// [MoCode]
		/// </summary>
		[FieldMapAttribute("so", typeof(string), 40, false)]
		public string  MOCODE;
        
		/// <summary>
		/// [Factory]
		/// </summary>
		[FieldMapAttribute("dep", typeof(string), 40, true)]
		public string  FACTORY;

		/// <summary>
		/// [Sequence]
		/// </summary>
		[FieldMapAttribute("serialno", typeof(decimal), 15, true)]
		public decimal  SEQUENCE;

		#endregion
	}
}
