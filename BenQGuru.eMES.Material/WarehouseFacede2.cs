using System;
using System.Collections;
using System.Text;
using System.Runtime.Remoting;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.TS;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Material
{
	/// <summary>
	/// WarehouseFacede2 的摘要说明。
	/// </summary>
	public class WarehouseFacede2:WarehouseFacade
	{
		public WarehouseFacede2(IDomainDataProvider domainDataProvider):base(domainDataProvider)
		{	
		}

		public WarehouseFacede2()
		{	
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}


		#region 盘点在制品

		/// <summary>
		/// 盘点在制品 (上料记录)
		/// </summary>
		/// <param name="warehouseCode"></param>
		private Hashtable CycleSimulation2(string warehouseCode)
		{
			//获取要盘点的库房对应的产线资源的在制品上料信息
			//tblonwipitem 表中 ActionType表示上料（0）或者下料（1）
			//TRANSSTATUS表示是否进行库房操作 NO表示没有库房操作，YES表式进行了库房操作 

			#region oldsql
//			string sql = string.Format(@" SELECT {0}
//										FROM tblonwipitem
//										WHERE 1 = 1
//										AND ACTIONTYPE=0
//										AND TRANSSTATUS = 'YES'
//										AND mitemcode > '0'
//										AND EXISTS (
//												SELECT mocode
//													FROM tblmo
//												WHERE tblmo.mocode = tblonwipitem.mocode
//													AND mostatus IN ('mostatus_pending', 'mostatus_open'))
//										AND EXISTS (
//												SELECT rescode
//													FROM tblres
//												WHERE tblres.rescode = tblonwipitem.rescode
//													AND EXISTS (
//															SELECT sscode
//																FROM tblwh2sscode
//																WHERE tblwh2sscode.sscode = tblres.sscode
//																	AND whcode = '{1}')) ",DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.DataCollect.OnWIPItem))
//				,warehouseCode);

			#endregion

			string sql = string.Format(
				"select mitemcode, sum(qty) as qty" + 
				"	from tblonwipitem " + 
				"	where   1 = 1 " +
				"		AND ACTIONTYPE = 0 " +
				"		AND TRANSSTATUS = 'YES' " +
				"		AND mitemcode > '0' " +
				"		and mocode in " +
				"		(select mocode " +
				"			from tblsimulation " +
				"			where EXISTS " + 
				"			(SELECT mocode " +
				"					FROM tblmo " +
				"					WHERE tblmo.mocode = tblsimulation.mocode " +
                GlobalVariables.CurrentOrganizations.GetSQLCondition() + 
				"					AND mostatus IN ('mostatus_pending', 'mostatus_open')) " +
				"			AND EXISTS " +
				"			(SELECT rescode " +
				"					FROM tblres " +
				"					WHERE tblres.rescode = tblsimulation.rescode " +
                GlobalVariables.CurrentOrganizations.GetSQLCondition() + 
				"					AND EXISTS " +
				"					(SELECT sscode " +
				"							FROM tblwh2sscode " +
				"							WHERE tblwh2sscode.sscode = tblres.sscode " +
				"							AND whcode = '{0}'))) " +
				"	group by mitemcode",warehouseCode);

			object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.DataCollect.OnWIPItem), new SQLCondition(sql));

			//对上料信息作统计。
			Hashtable _ht = new Hashtable();	//上料料号作键值，上料数量作Value
			if(objs !=null && objs.Length > 0)
				foreach(BenQGuru.eMES.Domain.DataCollect.OnWIPItem _onwipitem in objs)
				{
					string htkey = _onwipitem.MItemCode;
					if(_ht.Contains(htkey))
					{
						_ht[htkey] = Convert.ToDecimal(_ht[htkey]) + _onwipitem.Qty;
					}
					else
					{
						_ht.Add(_onwipitem.MItemCode,_onwipitem.Qty);
					}
				}

			//返回统计结果
			return _ht;
		}

		#endregion

		/// <summary>
		/// 盘点时查询库存数量 , 会盘点在制品
		/// </summary>
		/// <returns></returns>
		public object[] QueryWarehouseStockInCheck3( string itemCode, string warehouseCode,/* string segmentCode,*/ string factoryCode, int inclusive, int exclusive )
		{
			object[] objs = this.QueryWarehouseStockInCheck( itemCode, warehouseCode,/* segmentCode,*/ factoryCode, inclusive, exclusive );

			//匹配在制品盘点结果（上料记录）
			Hashtable _ht =this.CycleSimulation2(warehouseCode);

			foreach(WarehouseCycleCountDetail dtl in objs)
			{
				if(_ht.Contains(dtl.ItemCode))
				{
					//dtl.Qty ;														//离散数量
					dtl.LineQty = Convert.ToDecimal(_ht[dtl.ItemCode]);				//在制品虚拆数量
					dtl.Warehouse2LineQty = dtl.Qty + dtl.LineQty;					//账面数
				}
			}

			return objs;
		}

		
	}
}
