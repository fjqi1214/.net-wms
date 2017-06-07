using System;
using System.Text;
using System.Collections;
using System.Runtime.Remoting;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Material
{

	/// <summary>
	/// Laws Lu,2005/09/05
	/// 出、入库单的状态
	/// </summary>
	public struct StockStatus
	{
		/// <summary>
		/// 普通状态
		/// </summary>
		public const string Initial = "INITIAL";
		/// <summary>
		/// 处理完毕
		/// </summary>
		public const string Already = "ALREADY";
		/// <summary>
		/// 删除
		/// </summary>
		public const string Deleted = "DELETED";
	}

	/// <summary>
	/// MaterialFacade 的摘要说明。
	/// 文件名:		MaterialStockFacade.cs
	/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
	/// 创建人:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
	/// 创建日期:	2005-07-26 23:59:08
	/// 修改人:
	/// 修改日期:
	/// 描 述:	
	/// 版 本:	
	/// </summary>
	public class MaterialStockFacade:MarshalByRefObject
	{
		private IDomainDataProvider _domainDataProvider = null;
		private FacadeHelper _helper = null;

		public MaterialStockFacade(IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
			this._helper = new FacadeHelper( DataProvider );
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}


		public MaterialStockFacade()
		{
			this._helper = new FacadeHelper( DataProvider );
		}
		//Laws Lu,2005/09/09,修改
//		~MaterialStockFacade()
//		{
//			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.CloseConnection();
//		}

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

		#region MaterialStockIn
		/// <summary>
		/// 
		/// </summary>
		public MaterialStockIn CreateNewMaterialStockIn()
		{
			return new MaterialStockIn();
		}

		/// <summary>
		/// ** 功能描述:	将materialStockIns中的序列号关联至入库单
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-07-29
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="ticketNo">入库单号</param>
		/// <param name="materialStockIns">入库单的序列号数组</param>
		public void AddMaterialStockIn( string ticketNo, System.Data.DataTable materialStockIns,string user ,string memo)
		{
			bool alreadyMemo = false;
			foreach ( System.Data.DataRow dr in materialStockIns.Rows )
			{
				MaterialStockIn stockIn = new MaterialStockIn();
				//karron qiu ,2005/09/16 ,增加OID主键
				stockIn.OID = Guid.NewGuid().ToString();
				//-----------------------------------

				stockIn.TicketNO = ticketNo;
				stockIn.Status = StockStatus.Initial;
				stockIn.CollectType = dr["CollectType"].ToString().ToUpper();
				stockIn.MaintainUser = user;
				//2006/11/17,Laws Lu add get DateTime from db Server
				DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

				DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate,dbDateTime.DBTime);

				stockIn.MaintainDate = FormatHelper.TODateInt(dtNow);
				stockIn.MaintainTime = FormatHelper.TOTimeInt(dtNow);

				stockIn.RunningCard = dr["产品序列号"].ToString().ToUpper().Trim();
				stockIn.MOCode = dr["工单"].ToString().ToUpper().Trim();
				stockIn.ModelCode = dr["机种"].ToString().ToUpper().Trim();

				if(alreadyMemo == false)
				{
					stockIn.StockMemo = memo;
					alreadyMemo = true;
				}

				this.DataProvider.Insert( stockIn );
			}
		}

		/// <summary>
		/// ** 功能描述:	删除入库单下的序列号
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-07-29
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="tickerNo">入库单号</param>
		/// <param name="runningCard">序列号</param>
		public void DeleteMaterialStockIn( string tickerNo, string runningCard )
		{
			this.DataProvider.CustomExecute( new SQLParamCondition(
													"delete from TBLMSTOCKIN where TKTNO=$TKTNO and RCARD=$RCARD",
													new SQLParameter[]{ new SQLParameter("TKTNO", typeof(string), tickerNo),
																	    new SQLParameter("RCARD", typeof(string), runningCard) } ));
		}

		/// <summary>
		/// ** 功能描述:	删除入库单下的序列号
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-07-29
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="tickerNo">入库单号</param>
		/// <param name="runningCard">序列号</param>
		public void DeleteMaterialStockIn( string tickerNo, string runningCard,string status )
		{
			this.DataProvider.CustomExecute( new SQLCondition(
				"update TBLMSTOCKIN set STATUS='" + StockStatus.Deleted 
				+ "' where TKTNO='" + tickerNo + "'"
				+ " and RCARD = '" + runningCard + "'"
				+ " and STATUS = '" + status + "'"));
		}
		
		/// <summary>
		/// ** 功能描述:	按入库单号删除入库单
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-07-29
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="tickerNo">入库单号</param>
		public void DeleteMaterialStockIn( string tickerNo )
		{
			this.DataProvider.CustomExecute( new SQLParamCondition(
													"delete from TBLMSTOCKIN where TKTNO=$TKTNO",
													new SQLParameter[]{ new SQLParameter("TKTNO", typeof(string), tickerNo) } ));
		}

		/// <summary>
		/// ** 功能描述:	删除入库单的一组序列号
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-07-29
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="tickerNo">入库单号</param>
		/// <param name="runningCards">序列号数组</param>
		public void DeleteMaterialStockIn( string tickerNo, string[] runningCards )
		{
			if ( runningCards == null || runningCards.Length == 0 )
			{
				return;
			}

			StringBuilder builder = new StringBuilder();

			foreach ( string runningCard in runningCards )
			{
				builder.Append( string.Format("'{0}',", runningCard ) );
			}

			this.DataProvider.CustomExecute( new SQLCondition( 
															string.Format("delete from TBLMSTOCKIN where TKTNO='{0}' and RCARD in ({1})",
																		tickerNo, builder.ToString(0, builder.Length-1) )));
		}

		/// <summary>
		/// ** 功能描述:	求入库单的入库数量
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-07-26 23:59:08
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="ticketNo">ticketNo，精确查询</param>
		/// <returns> MaterialStockIn的总记录数</returns>
		public int QueryMaterialStockInCount( string ticketNo )
		{
			return this.DataProvider.GetCount(
											new SQLParamCondition(
												"select count(*) from TBLMSTOCKIN where TKTNO=$TKTNO" , 
											new SQLParameter[]{ new SQLParameter("TKTNO", typeof(string), ticketNo) } ));
		}

		/// <summary>
		/// ** 功能描述:	查询MaterialStockIn
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-07-26 23:59:08
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="ticketNo">ticketNo，精确查询</param>
		/// <returns> MaterialStockIn数组</returns>
		public object[] QueryMaterialStockIn( string ticketNo )
		{
			return this.DataProvider.CustomQuery(
					typeof(MaterialStockIn),	
					new SQLParamCondition(
										string.Format( "select {0} from TBLMSTOCKIN where TKTNO=$TKTNO" ,
														DomainObjectUtility.GetDomainObjectFieldsString(typeof(MaterialStockIn)) ),
                						new SQLParameter[]{ new SQLParameter("TKTNO", typeof(string), ticketNo) } ));
		}

		public bool IsStockInTicketExist( string ticketNo )
		{
			return this.DataProvider.GetCount(
					new SQLParamCondition("select count(*) from TBLMSTOCKIN where TKTNO=$TKTNO and STATUS<>'" + StockStatus.Deleted + "'" , 
											new SQLParameter[]{ new SQLParameter("TKTNO", typeof(string), ticketNo) } )) > 0;
		}

		public bool IsStockInTicketIncludeDeletedExist( string ticketNo )
		{
			return this.DataProvider.GetCount(
				new SQLParamCondition("select count(*) from TBLMSTOCKIN where TKTNO=$TKTNO " , 
				new SQLParameter[]{ new SQLParameter("TKTNO", typeof(string), ticketNo) } )) > 0;
		}

		//Laws Lu,2005/09/05,更新入库信息
		public void UpdateMaterialStockIn(MaterialStockIn materialStockIn)
		{
			this._helper.UpdateDomainObject( materialStockIn );
		}
		//Laws Lu,2005/09/05,获取入库信息
		public object GetMaterialStockIn( string ticketNO, string runningCard )
		{
			return this.DataProvider.CustomSearch(typeof(MaterialStockIn), new object[]{ ticketNO, runningCard });
		}
		#endregion

		#region MaterialStockOut
		/// <summary>
		/// 
		/// </summary>
		public MaterialStockOut CreateNewMaterialStockOut()
		{
			return new MaterialStockOut();
		}

		/// <summary>
		/// ** 功能描述:	查询MaterialStockOut
		/// ** 作 者:		Laws Lu
		/// ** 日 期:		2005-09-05
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns> MaterialStockOut数组</returns>
		public object[] QueryMaterialStockOut( string ticketNo )
		{
			return this.DataProvider.CustomQuery(
				typeof(MaterialStockOut),	
				new SQLParamCondition(
				string.Format( "select {0} from TBLMSTOCKOUT where TKTNO=$TKTNO" ,
				DomainObjectUtility.GetDomainObjectFieldsString(typeof(MaterialStockOut)) ),
				new SQLParameter[]{ new SQLParameter("TKTNO", typeof(string), ticketNo) } ));
		}

		/// <summary>
		/// ** 功能描述:	查询MaterialStockOut
		/// ** 作 者:		Laws Lu
		/// ** 日 期:		2005-09-05
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns> MaterialStockOut数组</returns>
		public object[] QueryMaterialStockOut( string ticketNo ,string status)
		{
			return this.DataProvider.CustomQuery(
				typeof(MaterialStockOut),	
				new SQLParamCondition(
				string.Format( "select {0} from TBLMSTOCKOUT where TKTNO=$TKTNO and STATUS='" + status + "'" ,
				DomainObjectUtility.GetDomainObjectFieldsString(typeof(MaterialStockOut)) ),
				new SQLParameter[]{ new SQLParameter("TKTNO", typeof(string), ticketNo) } ));
		}

		/// <summary>
		/// ** 功能描述:	判断是否有相同机种、经销商、发货日期的出货单存在
		///					如果存在返回流水号
		///					如果不存在,则产生新的流水号,将出货单信息新增入表中,并返回生成的流水号
		///					
		///					返回结果
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-07-29
		/// ** 修 改:       Karron Qiu
		/// ** 日 期:		2005-9-19 
		/// </summary>
		/// <param name="materialStockOut"></param>
		/// <returns></returns>
		public MaterialStockOut AddMaterialStockOut( MaterialStockOut materialStockOut)
		{
			MaterialStockOut stockOut = (MaterialStockOut)this.GetMaterialStockOut( materialStockOut );
			
			//Laws Lu,2005/09/10 修改
			//			if ( stockOut != null )
			//			{
			//				throw new Exception(string.Format("$Error_CS_Stock_Info_Exist $StockOutTicket={0},$ModelCode={1},$Dealer={2},$OutDate={3}",
			//													materialStockOut.TicketNO,
			//													materialStockOut.ModelCode,
			//													materialStockOut.Dealer,
			//													FormatHelper.ToDateString(materialStockOut.OutDate))	);
			//			}
			//
			//			materialStockOut.Sequence = this.GetUniqueSequenceByTicketNo( materialStockOut.TicketNO );
			//			materialStockOut.Status = StockStatus.Initial;
			//			materialStockOut.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
			//			materialStockOut.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);

			if ( stockOut == null )
			{
				//karron qiu ,2005/9/16 ,增加OID主键
				materialStockOut.OID = Guid.NewGuid().ToString();

				materialStockOut.Sequence = this.GetUniqueSequenceByTicketNo( materialStockOut.TicketNO );
				materialStockOut.Status = StockStatus.Initial;
				//2006/11/17,Laws Lu add get DateTime from db Server
				DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

				DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate,dbDateTime.DBTime);

				materialStockOut.MaintainDate = FormatHelper.TODateInt(dtNow);
				materialStockOut.MaintainTime = FormatHelper.TOTimeInt(dtNow);

				this.DataProvider.Insert( materialStockOut );

				stockOut = materialStockOut;
			}

			return stockOut;
		}	
		
		//Laws Lu,2005/09/05,	获取出库单
		//Karron Qiu,2005-9-19 增加主键OID
		public object GetMaterialStockOut( string ticketNO, decimal sequence,string OID )
		{
			return this.DataProvider.CustomSearch(typeof(MaterialStockOut), new object[]{ OID,ticketNO, sequence });
		}

		//Laws Lu,2005/09/05,	更新出库单
		public void UpdateMaterialStockOut(MaterialStockOut materialStockOut)
		{
			this._helper.UpdateDomainObject( materialStockOut );
		}
		/// <summary>
		/// ** 功能描述:	获得入库单的唯一流水号
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-07-29
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="ticketNo"></param>
		/// <returns></returns>
		private decimal GetUniqueSequenceByTicketNo( string ticketNo )
		{
			object[] objs = this.DataProvider.CustomQuery( typeof(MaterialStockOut), 
				new PagerParamCondition( "select seq from TBLMSTOCKOUT where TKTNO=$TKTNO",
										"seq desc", 1, 1,
										new SQLParameter[]{ new SQLParameter("TKTNO", typeof(string), ticketNo) } ));

			if ( objs == null || objs.Length < 1 )
			{
				return 0;
			}

			return ((MaterialStockOut)objs[0]).Sequence + 1;
		}

		/// <summary>
		/// 出货是否存在
		/// </summary>
		/// <param name="ticketNo"></param>
		/// <returns></returns>
		public bool IsStockOutTicketExist( string ticketNo ,string ModelCode,string Dealer,int OutDate)
		{
			//判断是否存在 karron qiu changed at 2005-9-19,判断除了 机种,经销商,出货日期和当前不一样(如果这几项一样的话,系统会在新增的时候自动覆盖),但是 单号和当前一样,状态 为 StockStatus.Already  的数据是否存在
			return  this.DataProvider.GetCount( new SQLParamCondition("select count(*) from TBLMSTOCKOUT where TKTNO=$TKTNO and (MODELCODE <> $MODELCODE or upper(DEALER) <> $DEALER or OUTDATE <> $OUTDATE ) and STATUS = '" + StockStatus.Already + "'" ,
				new SQLParameter[]{ new SQLParameter("TKTNO", typeof(string), ticketNo),
									  new SQLParameter("MODELCODE", typeof(string), ModelCode), 
									  new SQLParameter("DEALER",	typeof(string), Dealer.ToUpper()), 
									  new SQLParameter("OUTDATE",	typeof(int),	OutDate)} )) > 0;
		}

		/// <summary>
		/// 出货是否存在
		/// </summary>
		/// <param name="ticketNo"></param>
		/// <returns></returns>
		public bool IsStockOutTicketExist( string ticketNo )
		{
			return  this.DataProvider.GetCount( new SQLParamCondition("select count(*) from TBLMSTOCKOUT where TKTNO=$TKTNO"/* and STATUS<>'" + StockStatus.Deleted + "'" */,
				new SQLParameter[]{ new SQLParameter("TKTNO", typeof(string), ticketNo) } )) > 0;

		}

		public bool IsStockOutTicketDeleteOrAlready( string ticketNo )
		{
			return  this.DataProvider.GetCount( new SQLParamCondition("select count(*) from TBLMSTOCKOUT where TKTNO=$TKTNO and STATUS<>'" + StockStatus.Initial + "'" ,
				new SQLParameter[]{ new SQLParameter("TKTNO", typeof(string), ticketNo) } )) > 0;

		}

		/// <summary>
		/// ** 功能描述:	按出货单号删除出货单
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-07-29
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="ticketNo"></param>
		public void DeleteMaterialStockOut( string ticketNo )
		{

			this.DataProvider.CustomExecute( new SQLParamCondition(
										"delete from TBLMSTOCKOUT where TKTNO=$TKTNO",
										new SQLParameter[]{ new SQLParameter("TKTNO", typeof(string), ticketNo) } ));

			this.DataProvider.CustomExecute( new SQLParamCondition(
										"delete from TBLMSTOCKOUTDETAIL where TKTNO=$TKTNO",
										new SQLParameter[]{ new SQLParameter("TKTNO", typeof(string), ticketNo) } ));
		}

		/// <summary>
		/// ** 功能描述:	获得相同机种、经销商、发货日期的出货单信息
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-07-29
		/// ** 修 改:
		/// ** 日 期:
		/// Laws Lu,2005/09/20,修改	排除删除状态的出货单
		/// Karron Qiu,2005/09/20 ,修改,只查询初始状态的出货单
		/// </summary>
		/// <param name="materialStockOut">包含出货单号、机种、经销商、发货日期</param>
		/// <returns></returns>
		public object GetMaterialStockOut( MaterialStockOut materialStockOut )
		{
			object[] objs = this.DataProvider.CustomQuery( typeof(MaterialStockOut), 
					new SQLParamCondition( string.Format("select {0} from TBLMSTOCKOUT where TKTNO=$TKTNO and MODELCODE=$MODELCODE and upper(DEALER)=$DEALER and OUTDATE=$OUTDATE AND STATUS = '"+ StockStatus.Initial + "' order by seq desc",
															DomainObjectUtility.GetDomainObjectFieldsString(typeof(MaterialStockOut))),
										new SQLParameter[]{ new SQLParameter("TKTNO",		typeof(string), materialStockOut.TicketNO), 
															new SQLParameter("MODELCODE", typeof(string), materialStockOut.ModelCode), 
															new SQLParameter("DEALER",	typeof(string), materialStockOut.Dealer.ToUpper()), 
															new SQLParameter("OUTDATE",	typeof(int),	materialStockOut.OutDate) }));

			if ( objs != null )
			{
				return objs[0];
			}

			return null;
		}

		/// <summary>
		/// 获得明细数量  added by karron qiu ,2005-9-19
		/// </summary>
		/// <param name="materialStockOut"></param>
		/// <returns></returns>
		public int GetMaterialStockOutDetailCount( MaterialStockOut materialStockOut )
		{
			return this.DataProvider.GetCount(new SQLCondition("select count(*) from TBLMSTOCKOUTDETAIL where TKTNO='" + materialStockOut.TicketNO +"' and SEQ=" + materialStockOut.Sequence ));
		}

		public MaterialStockOutDetail CreateNewMaterialStockOutDetail()
		{
			return new MaterialStockOutDetail();
		}

		/// <summary>
		/// ** 功能描述:	新增出货单IMEI信息
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-07-29
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="stockOutDetail"></param>
		public void AddMaterialStockOutDetail( MaterialStockOutDetail stockOutDetail )
		{
			//2006/11/17,Laws Lu add get DateTime from db Server
			DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

			DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate,dbDateTime.DBTime);

			stockOutDetail.MaintainDate = FormatHelper.TODateInt(dtNow);
			stockOutDetail.MaintainTime = FormatHelper.TOTimeInt(dtNow);


			this.DataProvider.Insert( stockOutDetail );
		}

		/// <summary>
		/// ** 功能描述:	查询MaterialStockOut
		/// ** 作 者:		Laws Lu
		/// ** 日 期:		2005-09-05
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns> MaterialStockOut数组</returns>
		public bool RunningCardIsExist( string runningCard ,string tkNO,string tkSeq)
		{
			bool bReturn = false;

			object[] objs = this.DataProvider.CustomQuery(
				typeof(MaterialStockOutDetail),	
				new SQLCondition("select tkTNO from TBLMSTOCKOUTDETAIL where TKTNO='" + tkNO +"' and SEQ=" + tkSeq + " and RCARD='" + runningCard + "'"));


			if(objs != null && objs.Length > 0)
			{
				bReturn = true;
			}

			return bReturn;
		}
		#endregion

        // Added By Hi1/Venus.Feng on 20081028 for Hisense Version : Add SAP Material Stock In and Stock Out
        public RawReceive4SAP CreateNewRawReceive4SAP()
        {
            return new RawReceive4SAP();
        }

        public void AddRawReceive4SAP(RawReceive4SAP rawReceive4SAP)
        {
            this.DataProvider.Insert(rawReceive4SAP);
        }

        public void DeleteRawReceive4SAP(RawReceive4SAP rawReceive4SAP)
        {
            this.DataProvider.Delete(rawReceive4SAP);
        }

        public void UpdateRawReceive4SAP(RawReceive4SAP rawReceive4SAP)
        {
            this.DataProvider.Update(rawReceive4SAP);
        }

        public decimal GetRawReceive4SAPMaxPostSequence(string pono)
        {
            string sql = "SELECT seq_tblrawreceive2sap_postseq.nextval AS postseq FROM dual ";
            object[] list = this.DataProvider.CustomQuery(typeof(RawReceive4SAP), new SQLCondition(sql));
            return (list[0] as RawReceive4SAP).PostSequence;
        }

        public RawIssue4SAP CreateNewRawIssue4SAP()
        {
            return new RawIssue4SAP();
        }

        public void AddRawIssue4SAP(RawIssue4SAP rawIssue4SAP)
        {
            this.DataProvider.Insert(rawIssue4SAP);
        }

        public void DeleteRawIssue4SAP(RawIssue4SAP rawIssue4SAP)
        {
            this.DataProvider.Delete(rawIssue4SAP);
        }


        // End Added
	}
}


