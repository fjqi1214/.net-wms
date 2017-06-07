#region system;
using System;
using UserControl;
#endregion

#region project
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Domain.MOModel;

#endregion


namespace BenQGuru.eMES.DataCollect
{
	public class ActionOQCHelper
	{
		private IDomainDataProvider _domainDataProvider = null;

//		public ActionOQCHelper()
//		{	
//		}

		public ActionOQCHelper(IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
		}

		public IDomainDataProvider DataProvider
		{
			get
			{
				if (_domainDataProvider == null)
				{
					_domainDataProvider = DomainDataProviderManager.DomainDataProvider();
				}

				return _domainDataProvider;
			}
		}

		protected Messages messages = new Messages();



		public object[] GetOQCLot2Cards(string oqcLot)
		{
			OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
			return oqcFacade.ExactQueryOQCLot2Card(oqcLot.ToUpper(),OQCFacade.Lot_Sequence_Default);
		}

		//判断批号是否重复
		public bool IsOQCLotNOExisted(string OQCLot)
		{
			OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
			object  obj = oqcFacade.GetOQCLot(OQCLot,OQCFacade.Lot_Sequence_Default);
			if(obj == null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		//是否存在混单
		public bool IsRemixMOCode(string oqcLotNO,string moCode)
		{
			OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
			if( oqcFacade.GetUnSelectOQCLot2Card(oqcLotNO,moCode,string.Empty)>0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		//是否存在多个Item
		public bool IsRemixItemCode(string itemCode,string oqcLotNO)
		{
			OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
			if( oqcFacade.GetUnSelectOQCLot2Card(oqcLotNO,string.Empty,itemCode)>0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}



		//检查抽检批是否合法，如果存在但状态为(pass,reject)就出错
		public bool IsOQCLotComplete(string OQCLot)
		{
			OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
			object  obj = oqcFacade.GetOQCLot(OQCLot,OQCFacade.Lot_Sequence_Default);
			if(obj == null)
			{
				throw new Exception("Error_OQCLot_NotExisted,OQCLot='"+OQCLot+"'");
			}
			OQCLot oqcLot = obj as OQCLot;
			if( (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Pass)||(oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Reject))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		//检查抽检批是否合法，如果存在但状态为(pass,reject)就出错
		public bool IsOQCLotCompleteByLot(Domain.OQC.OQCLot obj)
		{
//			OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
//			object  obj = oqcFacade.GetOQCLot(OQCLot,OQCFacade.Lot_Sequence_Default);
			if(obj == null)
			{
				throw new Exception("Error_OQCLot_NotExisted,OQCLot='"+obj.LOTNO+"'");
			}
			OQCLot oqcLot = obj as OQCLot;
			if( (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Pass)||(oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Reject))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		
		//检查抽检批是否合法，如果存在但状态为(pass,reject)就出错
		public bool IsOQCLotCanExam(string OQCLot)
		{
			OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
			object  obj = oqcFacade.GetOQCLot(OQCLot,OQCFacade.Lot_Sequence_Default);
			if(obj == null)
			{
				throw new Exception("Error_OQCLot_NotExisted,OQCLot='"+OQCLot+"'");
			}
			OQCLot oqcLot = obj as OQCLot;
			if( (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Pass)||(oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Reject)||(oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Initial) )
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool IsOQCLotInitial(string OQCLot)
		{
			OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
			object  obj = oqcFacade.GetOQCLot(OQCLot,OQCFacade.Lot_Sequence_Default);
			if(obj == null)
			{
				throw new Exception("Error_OQCLot_NotExisted,OQCLot='"+OQCLot+"'");
			}
			OQCLot oqcLot = obj as OQCLot;
			if( oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Initial)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool IsPackingOperation(string itemCode,string routeCode,string opCode)
		{
			ItemFacade itemFacade = new ItemFacade(this.DataProvider);
			object obj = itemFacade.GetItemRoute2Operation(itemCode,routeCode,opCode);
			if(obj == null)
			{
				throw new Exception("Error_ItemRoute2OP_NotExisted");
			}
			ItemRoute2OP itemRoute2Op = obj as ItemRoute2OP;

			return FormatHelper.StringToBoolean(itemRoute2Op.OPControl,(int)OperationList.Packing);
		}


		//判断一个ID只能属于一个批
		public bool IsIDHasOnlyOQCLotNo(string runningID,string moCode,string oqcLotNO)
		{
			OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
			return !oqcFacade.IsCardUsedByAnotherLot(runningID,moCode,oqcLotNO,OQCFacade.Lot_Sequence_Default);
		}

		//获得检验抽检批中数量
		public int GetIDCountInOQCLotNo(string oqcLotNO)
		{
			OQCFacade oqcFacde = new OQCFacade(this.DataProvider);
			return oqcFacde.ExactQueryCountOQCLot2Card(oqcLotNO,OQCFacade.Lot_Sequence_Default);
		}

		/// <summary>
		/// 查询LOT中所有CARD的SIMULATION记录
		/// 
		/// </summary>
		/// <param name="lOTNO"></param>
		/// <param name="lotSequence"></param>
		/// <returns></returns>
		public object[] QueryCardOfLot(string lOTNO, decimal lotSequence)
		{
			return this.DataProvider.CustomQuery(typeof(Simulation),
				new SQLParamCondition(string.Format("SELECT {0} FROM TBLSIMULATION s,TBLLOT2CARD l WHERE s.RCARD=l.RCARD and s.MOCODE=l.MOCODE and "
				+" l.LOTNO = $LOTNO AND l.LOTSEQ=$LOTSEQ "
				+" ORDER BY s.MOCODE,s.ROUTECODE,s.OPCODE,s.RESCODE,s.PRODUCTSTATUS,s.LACTION,s.ACTIONLIST,s.ISCOM for update nowait",
				GetDomainObjectFieldsString(typeof(Simulation),"s")) ,
				new SQLParameter[]{ new SQLParameter("LOTNO", typeof(string), lOTNO.ToUpper()) 
									  ,new SQLParameter("LOTSEQ", typeof(string), lotSequence)} ));
			//return simulations;
			//return this.DataProvider.CustomQuery(typeof(OQCLot2Card), new PagerCondition(
			//	string.Format("SELECT {0} FROM TBLLOT2CARD WHERE 1=1 AND LOTNO = '{1}'  AND LOTSEQ = '{2}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2Card)) , lOTNO,lotSequence), "RCARD,MOCODE,LOTNO,LOTSEQ",int.MinValue, int.MaxValue)); 
		}

		/// <summary>
		/// 获得DomainObject对应的所有数据库字段名，用','拼成的字符串
		/// </summary>
		/// <param name="type">DomainObject的类型</param>
		/// <returns></returns>
		public string GetDomainObjectFieldsString(Type type,string tblName)
		{
			string s=tblName+"."+ String.Join( ", "+tblName+".", (string[])DomainObjectUtility.GetDomainObjectFieldsName(type).ToArray(typeof(string)) );
			return s;
		}
		

	}
}