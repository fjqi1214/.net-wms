#region System
using System;
using System.Text;
using System.Runtime.Remoting;  
using System.Collections;
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Common.Helper;  
using BenQGuru.eMES.Common.DomainDataProvider;   
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Rework;

#endregion
 

namespace BenQGuru.eMES.SAPData
{
	/// <summary>
	/// SAPMaper 数据映射 SAP的数据对象映射到MES的对象
	/// </summary>
	public class SAPMapper
	{
		private static int MOBomSeq=0;
		
		public SAPMapper()
		{
		}


		private bool EffientDeleteMOPrifix  = true;	//true 表示使用2位0的前导0截取,效率高; false表示多位前导0的截取,效率低,默认2位前导0截取

		public static object[] MOTypeParams;		//工单类型参数 因为工单界面显示的工单类型可以自定义,因此需要从MES数据库中获取.

		/// <summary>
		/// 映射SAP 实体 到 MES 实体
		/// </summary>
		/// <returns></returns>
		public ArrayList MapSAPData(object[] SAPDatas)
		{
			#region 参数判断 类型判断

			if(SAPDatas==null) return new ArrayList();
			if(!(SAPDatas.Length>0)) return new ArrayList();

			if(!(SAPDatas[0] is IImport)) return new ArrayList();;

			if(!(SAPDatas[0] is DomainObject))
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Mapper_Parameter",string.Empty);//参数类型错误
			}

			#endregion

			ArrayList returnMESObjs = new ArrayList(SAPDatas.Length);	//映射返回的MES 对象

			#region 映射工厂方法
			Type DomainType = SAPDatas[0].GetType();
			if(DomainType == typeof(SAPMO))
			{
				returnMESObjs	= this.MapSAPMO(SAPDatas);			//SAP订单
			}
			else if(DomainType == typeof(SAPMOBom))
			{
				returnMESObjs	= this.MapSAPItem(SAPDatas);		//SAP产品
			}
			else if(DomainType == typeof(SAPBOM))
			{
				returnMESObjs	= this.MapSAPBom(SAPDatas);			//SAPBOM
			}


			#endregion
				
			return returnMESObjs;
		}

		#region 映射对象 订单

		/// <summary>
		/// 映射SAP订单到MES工单
		/// </summary>
		/// <returns></returns>
		private ArrayList MapSAPMO(object[] SAPMOObjs)
		{
			#region 参数判断 类型判断

			if(SAPMOObjs==null) return new ArrayList();
			if(!(SAPMOObjs.Length>0)) return new ArrayList();

			if(!(SAPMOObjs[0] is IImport)) return new ArrayList();;

			if(SAPMOObjs[0].GetType() != typeof(SAPMO))
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Mapper_Parameter",string.Empty);//参数类型错误
			}

			#endregion

			ArrayList mesObjs = new ArrayList(SAPMOObjs.Length);	//映射返回的MES 工单

			#region 映射MO的字段
			
			foreach(SAPMO _sapEntity in SAPMOObjs)
			{
				MO _mesEntity = new MO();
				_mesEntity.MOCode			= this.DeletePrifix(_sapEntity.MOCode);//_sapEntity.MOCode;							//工单号 需要去除前导0
				_mesEntity.Factory			= _sapEntity.Factory;							//工厂
				_mesEntity.ItemCode			= _sapEntity.ItemCode;							//父物料号
				_mesEntity.MOType			= _sapEntity.MOType;//this.GetMOType(_sapEntity.MOType);							//订单类型(需要从sap类型转换到mes类型)
				_mesEntity.MOPlanQty		= decimal.Parse(_sapEntity.MOPlanQty);							//订单数量
				_mesEntity.MOPlanStartDate	= FormatHelper.TODateInt(_sapEntity.MOPlanStartDate);					//订单开始日期
				_mesEntity.MOPlanEndDate	= FormatHelper.TODateInt(_sapEntity.MOPlanEndDate);						//订单完成日期
				
				_mesEntity.MOStatus			= MOManufactureStatus.MOSTATUS_INITIAL;				//状态默认为初始
				_mesEntity.MODownloadDate	= FormatHelper.TODateInt(DateTime.Today);
				_mesEntity.MOUser			= "ADMIN";							//因为系统自动执行默认开单人员为ADMIN	
				_mesEntity.MaintainUser     = "ADMIN";								//因为系统自动执行默认维护人员为ADMIN	
				_mesEntity.MaintainDate     = FormatHelper.TODateInt(DateTime.Today);
				_mesEntity.MaintainTime     = FormatHelper.TOTimeInt(DateTime.Now);
				_mesEntity.MOImportDate     = FormatHelper.TODateInt(DateTime.Today);
				_mesEntity.MOImportTime     = FormatHelper.TOTimeInt(DateTime.Now);
				_mesEntity.MOVersion		= "1";
				_mesEntity.IsControlInput	= "0";
				_mesEntity.IsBOMPass		= "0";
				//_mesEntity.MOOffQty         = 0;						//脱离工单数量默认０
				_mesEntity.IDMergeRule      = 1;						//分板比列默认是1
                _mesEntity.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
				//以下是没有映射的字段
				//				_mesEntity.CustomerOrderNO = FormatHelper.CleanString(this.txtCustomerOrderNOEdit.Text, 40);
				//				_mesEntity.CustomerCode = FormatHelper.CleanString(this.txtCustomerCodeEdit.Text, 40);
				//				_mesEntity.MOActualStartDate = (this.txtActualStartDateEdit.Text == "") ? 10101: FormatHelper.TODateInt(this.txtActualStartDateEdit.Text);		//modify by Simone
				//				_mesEntity.MOActualEndDate = (this.txtActualEndDateEdit.Text == "") ? 99991231: FormatHelper.TODateInt(this.txtActualEndDateEdit.Text);
				
				//				_mesEntity.IDMergeRule = System.Int32.Parse(FormatHelper.CleanString(this.txtDenominatorEdit.Text));

				mesObjs.Add(_mesEntity);
			}

			#endregion
				
			return mesObjs;
		}

		//去除SAP 订单Code的前导0 (目前有2个0)
		private string DeletePrifix(string woCode)
		{
			if(woCode.Length>2)
			{
				//只针对两个前导0的算法
				string moCode = woCode;
				if(EffientDeleteMOPrifix)
				{
					moCode = woCode.Substring(2,woCode.Length-2);	//只针对两个前导0的算法
				}
				else
				{
					moCode = this.DeletePrifix2(woCode);			//针对多个前导0的算法
				}
				return moCode;
			}
			return woCode;
		}

		//针对多个前导0的算法,已经测试通过,暂时不用,因为效率问题
		private string DeletePrifix2(string woCode)
		{
			//针对多个前导0的算法
			int notZeroPosition = 0; //从左边计数,不是0的第一个字符的位置
			for(int i=0;i<woCode.Length;i++)
			{
				if(woCode[i].CompareTo('0') != 0)
				{
					notZeroPosition = i;
					break;
				}
			}
			string moCode = woCode.Substring(notZeroPosition,woCode.Length-notZeroPosition);
			return moCode;
		}

		#endregion

		#region 映射对象 BOM

		/// <summary>
		/// 映射SAP BOM到MES BOM
		/// </summary>
		/// <returns></returns>
		private ArrayList MapSAPBom(object[] SAPBOMObjs)
		{
			#region 参数判断 类型判断

			if(SAPBOMObjs==null) return new ArrayList();
			if(!(SAPBOMObjs.Length>0)) return new ArrayList();

			if(!(SAPBOMObjs[0] is IImport)) return new ArrayList();;

			if(SAPBOMObjs[0].GetType() != typeof(SAPBOM))
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Mapper_Parameter",string.Empty);//参数类型错误
			}

			#endregion

			ArrayList mesObjs = new ArrayList(SAPBOMObjs.Length);	//映射返回的MES 工单

			#region 映射BOM的字段
			//SAPBOM 主键 ITEMCODE,Sequence
			int seq = 1;
			string lastALPGR = string.Empty;											
			
			foreach(SAPBOM _sapEntity in SAPBOMObjs)
			{
				SBOM _mesEntity = new SBOM();

                _mesEntity.OrganizationID           = GlobalVariables.CurrentOrganizations.First().OrganizationID;

				_mesEntity.ItemCode					= _sapEntity.FItemCode;					//产品代码
				_mesEntity.SBOMItemECN				= _sapEntity.SBOMItemECN;
				_mesEntity.SBOMItemCode				= _sapEntity.SBOMItemCode;									//子阶料
				_mesEntity.SBOMSourceItemCode		= _sapEntity.SBOMItemCode ;									//首选料
				_mesEntity.SBOMItemName				= _sapEntity.SOBOMItemName;										
				_mesEntity.SBOMItemEffectiveDate	= FormatHelper.TODateInt(_sapEntity.SBOMItemEffectiveDate.Date);
				_mesEntity.SBOMItemInvalidDate		= FormatHelper.TODateInt(_sapEntity.SBOMItemInvalidDate.Date);
				_mesEntity.ALPGR					= _sapEntity.ALPGR;											//替代项目组

				_mesEntity.SBOMItemEffectiveTime	= _sapEntity.SBOMItemEffectiveDate.Hour * 10000 + _sapEntity.SBOMItemEffectiveDate.Minute * 100 + _sapEntity.SBOMItemEffectiveDate.Second;
				_mesEntity.SBOMItemInvalidTime		= _sapEntity.SBOMItemInvalidDate.Hour * 10000 + _sapEntity.SBOMItemInvalidDate.Minute * 100 + _sapEntity.SBOMItemInvalidDate.Second;
				
				_mesEntity.SBOMItemControlType		= BOMItemControlType.ITEM_CONTROL_NOCONTROL;	//导入的数据默认是不受管控的，由用户自行决定是受lot管控还是keyparts管控
				_mesEntity.SBOMItemStatus			= "0";
				_mesEntity.SBOMItemQty				= _sapEntity.SBOMItemQty;
				if(_sapEntity.ALPGR!=null && _sapEntity.ALPGR != lastALPGR)
				{
					lastALPGR = _sapEntity.ALPGR;	
					seq = 1;
				}
				_mesEntity.Sequence					= seq++;										//Sequence 不再作为主键
				_mesEntity.SBOMItemUOM				= (_sapEntity.ItemUOM == string.Empty) ? "EA":_sapEntity.ItemUOM;

				_mesEntity.MaintainDate				= FormatHelper.TODateInt(DateTime.Today);
				_mesEntity.MaintainTime				= FormatHelper.TOTimeInt(DateTime.Now);
				_mesEntity.MaintainUser				= "ADMIN";
				_mesEntity.MaintainDate				= FormatHelper.TODateInt(DateTime.Today);
				_mesEntity.MaintainTime				= FormatHelper.TOTimeInt(DateTime.Now);

				//以下是没有映射的字段

				mesObjs.Add(_mesEntity);

				
			}

			#endregion
				
			return mesObjs;
		}

		#endregion

		#region 映射对象 订单物料

		
		/// <summary>
		/// 映射SAP 工单bom到MES工单bom
		/// </summary>
		/// <returns></returns>
		private ArrayList MapSAPItem(object[] SAPItemObjs)
		{
			#region 参数判断 类型判断

			if(SAPItemObjs==null) return new ArrayList();
			if(!(SAPItemObjs.Length>0)) return new ArrayList();

			if(!(SAPItemObjs[0] is IImport)) return new ArrayList();;

			if(SAPItemObjs[0].GetType() != typeof(SAPMOBom))
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Mapper_Parameter",string.Empty);//参数类型错误
			}

			#endregion

			ArrayList mesObjs = new ArrayList(SAPItemObjs.Length);	//映射返回的MES 工单

			#region 映射MOBOM的字段
			int seq = 0;
			foreach(SAPMOBom _sapEntity in SAPItemObjs)
			{
				MOBOM _mesEntity = new MOBOM();

				//WODETAIL 数据结构
				//订单号	物料号码	发料工厂	工序	需求数量	领料数量	替代项	优先级	物料类型	计量单位
				//AUFNR		MATNR		PWERK		VORNR	BDMNG		ENMNG		ALPGR	ZPRI	ZFLAG		GMEIN

				_mesEntity.MOCode			= this.DeletePrifix(_sapEntity.MOCode);//_sapEntity.MOCode;							//工单号 需要去除前导0
				
				_mesEntity.MOBOMItemCode = _sapEntity.MOBOMItemCode;
				_mesEntity.MOBOMItemName = _sapEntity.MOBOMItemName;				
				_mesEntity.MOBOMItemQty = _sapEntity.UnitageQty;					//单机用量
				_mesEntity.MOBOMSourceItemCode = _sapEntity.PItem;					//替代料
				_mesEntity.MOBOMItemUOM = _sapEntity.MOBOMItemUOM;					//计量单位
				_mesEntity.OPCode = _sapEntity.OPCode;								//工序Code

				_mesEntity.MOBOMItemStatus = "0";									// 1 -  使用中  0 -  正常  -1 - 已删除
				_mesEntity.MaintainDate = FormatHelper.TODateInt(DateTime.Today);	//最后维护日期
				_mesEntity.MaintainTime = FormatHelper.TOTimeInt(DateTime.Today);	//最后维护时间
				_mesEntity.MaintainUser = "ADMIN";									//维护人员	默认为ADMIN
				_mesEntity.MOBOMItemVersion = "0";									//子阶料版本 默认为0	
				_mesEntity.MOBOMItemEffectiveDate = 20000101;						//生效日期
				_mesEntity.MOBOMItemEffectiveTime = 0;								//生效时间
				_mesEntity.MOBOMItemInvalidDate = 21001229;							//失效日期
				_mesEntity.MOBOMItemInvalidTime = 235959;							//失效时间
				_mesEntity.Sequence = ++MOBomSeq;										//序号
				++seq;

				_mesEntity.MOBOMItemECN = "";
				_mesEntity.MOBOMItemLocation = "";
				_mesEntity.MOBOMItemDescription = "";								//子阶料描述
				_mesEntity.MOBOMItemControlType = "";								//控制类型
				
				_mesEntity.ItemCode = _sapEntity.ItemCode;							//产品代码
				

				//以下是没有映射的字段

				mesObjs.Add(_mesEntity);
			}

			#endregion
				
			return mesObjs;
		}


		#endregion

		#region 工单类型映射 暂时不用

//		private string GetMOType(string WOType)
//		{
//			//工单类型编码	工单描述
//			//PP01	正常生产
//			//PP02	试制订单
//			//PP03	返修订单
//			//PP04	返工订单
//			
//			string moType =BenQGuru.eMES.Web.Helper.MOType.MOTYPE_NORMALMOTYPE;//默认是正常类型
//
//			switch(WOType.Trim())
//			{
//				case "PP01" :						//PP01	正常生产
//					moType = "NORMAL";//BenQGuru.eMES.Web.Helper.MOType.MOTYPE_NORMALMOTYPE;
//					break;
//				case "PP02" :						//PP02	试制订单
//					moType ="NORMAL"; //BenQGuru.eMES.Web.Helper.MOType.MOTYPE_NORMALMOTYPE;				//MES 中没有试制订单, 暂时用正常订单
//					break;
//				case "PP03" :						//PP03	返修订单 (每月大返工订单)
//					moType ="MONTHREWORK"; // BenQGuru.eMES.Web.Helper.MOType.MOTYPE_MONTHREWORKMOTYPE;				//MES 中没有返修订单, 暂时用每月大返工订单
//					break;
//				case "PP04" :						//PP04	返工订单
//					moType ="REWORK"; // BenQGuru.eMES.Web.Helper.MOType.MOTYPE_REWORKMOTYPE;
//					break;
//				default:
//					moType ="NORMAL"; // BenQGuru.eMES.Web.Helper.MOType.MOTYPE_NORMALMOTYPE;
//					break;
//			}
//
//			return moType;
//		}

		#endregion

		#region 物料类型映射 (未完成)

		private string GetMaterialType(string WOMaterialType)
		{
			//物料类型	物料类型描述
			//FERT	成品
			//HALB	半成品
			//ROH	原物料

			string materialType = "ITEMTYPE_FINISHEDPRODUCT";		//默认是成品

			switch(WOMaterialType.Trim())
			{
				case "FERT" :						//FERT	成品
					materialType = "ITEMTYPE_FINISHEDPRODUCT";
					break;
				case "HALB" :						//HALB	半成品
					materialType = "ITEMTYPE_SEMIMANUFACTURE";				
					break;
				case "ROH" :						//ROH	原物料
					materialType = "ITEMTYPE_RAWMATERIAL";				
					break;
			}

			return materialType;
		}

		#endregion

		


		

	}
}
