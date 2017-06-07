using System;
using System.Runtime.Remoting;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.Report;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Report
{
	/// <summary>
	/// ReportFacade 的摘要说明。
	/// 文件名:		ReportFacade.cs
	/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
	/// 创建人:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
	/// 创建日期:	2005-6-15 上午 15:14:38
	/// 修改人:
	/// 修改日期:
	/// 描 述:	
	/// 版 本:	
	/// </summary>
	
	public class ReportFacade:MarshalByRefObject
	{
		private  FacadeHelper _helper					= null;

		private IDomainDataProvider _domainDataProvider = null;

		public ReportFacade()
		{}

		public override object InitializeLifetimeService()
		{
			return null;
		}


		public ReportFacade(IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
			this._helper = new FacadeHelper(DataProvider);
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

		#region ReportHistoryOPQty
		/// <summary>
		/// 
		/// </summary>
		public ReportHistoryOPQty CreateNewReportHistoryOPQty()
		{
			return new ReportHistoryOPQty();
		}

		public void AddReportHistoryOPQty( ReportHistoryOPQty reportHistoryOPQty)
		{
			this.DataProvider.Insert( reportHistoryOPQty );
		}

//		public void UpdateReportHistoryOPQty(ReportHistoryOPQty reportHistoryOPQty)
//		{
//			this.DataProvider.Update( reportHistoryOPQty );
//		}
		/// <summary>
		/// Laws Lu	
		///	2005/10/10	
		/// 更新报表数量
		/// </summary>
		/// <param name="reportHistoryOPQty">实体</param>
		///<param name="qty">数量</param>
		///<param name="ngtimes">ng次数</param>
		public void ModifyReportHistoryOPQty(ReportHistoryOPQty reportHistoryOPQty,int qty,int ngtimes)
		{
			string sql = "update TBLRPTHISOPQTY set outputqty = outputqty + " + qty.ToString()
				+ ",ngtimes = ngtimes + " + ngtimes.ToString()
				+ ",EAttribute2 = EAttribute2 + " + reportHistoryOPQty.EAttribute2.ToString()
				+ ",ECG2EC = ECG2EC || '" + reportHistoryOPQty.ErrorGroup2Err + "'"
				//+ ",qtyflag = '" + reportHistoryOPQty.QtyFlag + "'"
				+ " where MODELCODE = '" + reportHistoryOPQty.ModelCode + "'"
				+ " and ITEMCODE = '" + reportHistoryOPQty.ItemCode + "'"
				+ " and MOCODE = '" + reportHistoryOPQty.MOCode + "'"
				+ " and ShiftDay = '" + reportHistoryOPQty.ShiftDay + "'"
				+ " and SHIFTCODE = '" + reportHistoryOPQty.ShiftCode + "'"
				+ " and TPCODE = '" + reportHistoryOPQty.TimePeriodCode + "'"
				+ " and SEGCODE = '" + reportHistoryOPQty.SegmnetCode + "'"
				+ " and SSCODE = '" + reportHistoryOPQty.StepSequenceCode + "'"
				+ " and OPCODE = '" + reportHistoryOPQty.OPCode + "'"
				+ " and RESCODE = '" + reportHistoryOPQty.ResourceCode + "'"
				+ " and QTYFLAG = '" + reportHistoryOPQty.QtyFlag + "'";

			this.DataProvider.CustomExecute(new SQLCondition(sql));
		}

		public void DeleteReportHistoryOPQty(ReportHistoryOPQty reportHistoryOPQty)
		{
			this.DataProvider.Delete( reportHistoryOPQty );
		}

//		public void DeleteReportHistoryOPQty(ReportHistoryOPQty[] reportHistoryOPQty)
//		{
//			this.DataProvider.Delete( reportHistoryOPQty );
//		}

		public object GetReportHistoryOPQty( string modelCode, int shiftDay, string mOCode, string timePeriodCode, string stepSequenceCode, string segmnetCode, string itemCode, string shiftCode, string oPCode, string resourceCode,string qtyFlag )
		{
			return this.DataProvider.CustomSearch(typeof(ReportHistoryOPQty), new object[]{ modelCode, shiftDay, mOCode, timePeriodCode, stepSequenceCode, segmnetCode, itemCode, shiftCode, oPCode, resourceCode ,qtyFlag});
		}

		public object GetOutLineReportHistoryOPQty( string modelCode, int shiftDay
			, string mOCode, string timePeriodCode
			, string stepSequenceCode, string segmnetCode
			, string itemCode, string shiftCode
			, string oPCode, string resCode
			,string qtyFlag )
		{
			string sql = "select {0} from  TBLRPTHISOPQTY "
				+ " where MODELCODE = '" + modelCode + "'"
				+ " and ITEMCODE = '" + itemCode + "'"
				+ " and MOCODE = '" + mOCode + "'"
				+ " and ShiftDay = " + shiftDay
				+ " and SHIFTCODE = '" + shiftCode + "'"
				+ " and TPCODE = '" + timePeriodCode + "'"
				+ " and SEGCODE = '" + segmnetCode + "'"
				+ " and SSCODE = '" + stepSequenceCode + "'"
				+ " and OPCODE = '" + oPCode + "'"
				+ " and RESCODE = '" + resCode + "'"
				+ " and QTYFLAG = '" + qtyFlag + "'";

			object[] objs = this.DataProvider.CustomQuery(typeof(ReportHistoryOPQty),
				new SQLCondition(String.Format(sql,DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReportHistoryOPQty)))));

			if(objs != null && objs.Length > 0)
			{
				return objs[0];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// ** 功能描述:	查询ReportHistoryOPQty的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-6-15 上午 15:14:38
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="modelCode">ModelCode，模糊查询</param>
		/// <param name="shiftDay">ShiftDay，模糊查询</param>
		/// <param name="mOCode">MOCode，模糊查询</param>
		/// <param name="timePeriodCode">TimePeriodCode，模糊查询</param>
		/// <param name="stepSequenceCode">StepSequenceCode，模糊查询</param>
		/// <param name="segmnetCode">SegmnetCode，模糊查询</param>
		/// <param name="itemCode">ItemCode，模糊查询</param>
		/// <param name="shiftCode">ShiftCode，模糊查询</param>
		/// <param name="oPCode">OPCode，模糊查询</param>
		/// <param name="resourceCode">ResourceCode，模糊查询</param>
		/// <returns> ReportHistoryOPQty的总记录数</returns>
		public int QueryReportHistoryOPQtyCount( string modelCode, int shiftDay, string mOCode, string timePeriodCode, string stepSequenceCode, string segmnetCode, string itemCode, string shiftCode, string oPCode, string resourceCode)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTHISOPQTY where 1=1 and MODELCODE like '{0}%'  and SHIFTDAY like '{1}%'  and MOCODE like '{2}%'  and TPCODE like '{3}%'  and SSCODE like '{4}%'  and SEGCODE like '{5}%'  and ITEMCODE like '{6}%'  and SHIFTCODE like '{7}%'  and OPCODE like '{8}%'  and RESCODE like '{9}%' " , modelCode, shiftDay, mOCode, timePeriodCode, stepSequenceCode, segmnetCode, itemCode, shiftCode, oPCode, resourceCode)));
		}

		public int QueryOutLineReportHistoryOPQtyCount( string modelCode, int shiftDay, string mOCode, string timePeriodCode, string stepSequenceCode, string segmnetCode, string itemCode, string shiftCode, string oPCode,string resCode)
		{
			return this.DataProvider.GetCount(
				new SQLCondition(
				string.Format("select count(*) from TBLRPTHISOPQTY where MODELCODE = '{0}'  and SHIFTDAY = {1} "
+ " and MOCODE = '{2}'  and TPCODE = '{3}'  and SSCODE = '{4}'  and SEGCODE = '{5}' " 
+ " and ITEMCODE = '{6}'  and SHIFTCODE = '{7}'  and OPCODE = '{8}'  and RESCODE = '{9}'" 
				, modelCode, shiftDay, mOCode, timePeriodCode, stepSequenceCode, segmnetCode, itemCode, shiftCode, oPCode,resCode)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询ReportHistoryOPQty
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-6-15 上午 15:14:38
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="modelCode">ModelCode，模糊查询</param>
		/// <param name="shiftDay">ShiftDay，模糊查询</param>
		/// <param name="mOCode">MOCode，模糊查询</param>
		/// <param name="timePeriodCode">TimePeriodCode，模糊查询</param>
		/// <param name="stepSequenceCode">StepSequenceCode，模糊查询</param>
		/// <param name="segmnetCode">SegmnetCode，模糊查询</param>
		/// <param name="itemCode">ItemCode，模糊查询</param>
		/// <param name="shiftCode">ShiftCode，模糊查询</param>
		/// <param name="oPCode">OPCode，模糊查询</param>
		/// <param name="resourceCode">ResourceCode，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> ReportHistoryOPQty数组</returns>
		public object[] QueryReportHistoryOPQty( string modelCode, int shiftDay, string mOCode, string timePeriodCode, string stepSequenceCode, string segmnetCode, string itemCode, string shiftCode, string oPCode, string resourceCode, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(ReportHistoryOPQty), new PagerCondition(string.Format("select {0} from TBLRPTHISOPQTY where 1=1 and MODELCODE like '{1}%'  and SHIFTDAY like '{2}%'  and MOCODE like '{3}%'  and TPCODE like '{4}%'  and SSCODE like '{5}%'  and SEGCODE like '{6}%'  and ITEMCODE like '{7}%'  and SHIFTCODE like '{8}%'  and OPCODE like '{9}%'  and RESCODE like '{10}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReportHistoryOPQty)) , modelCode, shiftDay, mOCode, timePeriodCode, stepSequenceCode, segmnetCode, itemCode, shiftCode, oPCode, resourceCode), "MODELCODE,SHIFTDAY,MOCODE,TPCODE,SSCODE,SEGCODE,ITEMCODE,SHIFTCODE,OPCODE,RESCODE", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的ReportHistoryOPQty
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-6-15 上午 15:14:38
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>ReportHistoryOPQty的总记录数</returns>
		public object[] GetAllReportHistoryOPQty()
		{
			return this.DataProvider.CustomQuery(typeof(ReportHistoryOPQty), new SQLCondition(string.Format("select {0} from TBLRPTHISOPQTY order by MODELCODE,SHIFTDAY,MOCODE,TPCODE,SSCODE,SEGCODE,ITEMCODE,SHIFTCODE,OPCODE,RESCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReportHistoryOPQty)))));
		}


		#endregion

		#region ReportRealtimeLineErrorCauseQty
		/// <summary>
		/// 
		/// </summary>
		public ReportRealtimeLineErrorCauseQty CreateNewReportRealtimeLineErrorCauseQty()
		{
			return new ReportRealtimeLineErrorCauseQty();
		}

		public void AddReportRealtimeLineErrorCauseQty( ReportRealtimeLineErrorCauseQty reportRealtimeLineErrorCauseQty)
		{
			this.DataProvider.Insert( reportRealtimeLineErrorCauseQty );
		}

		public void UpdateReportRealtimeLineErrorCauseQty(ReportRealtimeLineErrorCauseQty reportRealtimeLineErrorCauseQty)
		{
			this.DataProvider.Update( reportRealtimeLineErrorCauseQty );
		}

		public void DeleteReportRealtimeLineErrorCauseQty(ReportRealtimeLineErrorCauseQty reportRealtimeLineErrorCauseQty)
		{
			this.DataProvider.Delete( reportRealtimeLineErrorCauseQty );
		}

//		public void DeleteReportRealtimeLineErrorCauseQty(ReportRealtimeLineErrorCauseQty[] reportRealtimeLineErrorCauseQty)
//		{
//			this.DataProvider.Delete( reportRealtimeLineErrorCauseQty );
//		}

		public object GetReportRealtimeLineErrorCauseQty( string modelCode, int shiftDay, string mOCode, string timePeriodCode, string stepSequenceCode, string segmnetCode, string itemCode, string shiftCode, string errorCause )
		{
			return this.DataProvider.CustomSearch(typeof(ReportRealtimeLineErrorCauseQty), new object[]{ modelCode, shiftDay, mOCode, timePeriodCode, stepSequenceCode, segmnetCode, itemCode, shiftCode, errorCause });
		}

		/// <summary>
		/// ** 功能描述:	查询ReportRealtimeLineErrorCauseQty的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-6-15 上午 15:14:38
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="modelCode">ModelCode，模糊查询</param>
		/// <param name="shiftDay">ShiftDay，模糊查询</param>
		/// <param name="mOCode">MOCode，模糊查询</param>
		/// <param name="timePeriodCode">TimePeriodCode，模糊查询</param>
		/// <param name="stepSequenceCode">StepSequenceCode，模糊查询</param>
		/// <param name="segmnetCode">SegmnetCode，模糊查询</param>
		/// <param name="itemCode">ItemCode，模糊查询</param>
		/// <param name="shiftCode">ShiftCode，模糊查询</param>
		/// <param name="errorCause">ErrorCause，模糊查询</param>
		/// <returns> ReportRealtimeLineErrorCauseQty的总记录数</returns>
		public int QueryReportRealtimeLineErrorCauseQtyCount( string modelCode, int shiftDay, string mOCode, string timePeriodCode, string stepSequenceCode, string segmnetCode, string itemCode, string shiftCode, string errorCause)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTREALLINEECEQTY where 1=1 and MODELCODE like '{0}%'  and SHIFTDAY like '{1}%'  and MOCODE like '{2}%'  and TPCODE like '{3}%'  and SSCODE like '{4}%'  and SEGCODE like '{5}%'  and ITEMCODE like '{6}%'  and SHIFTCODE like '{7}%'  and ECCODE like '{8}%' " , modelCode, shiftDay, mOCode, timePeriodCode, stepSequenceCode, segmnetCode, itemCode, shiftCode, errorCause)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询ReportRealtimeLineErrorCauseQty
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-6-15 上午 15:14:38
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="modelCode">ModelCode，模糊查询</param>
		/// <param name="shiftDay">ShiftDay，模糊查询</param>
		/// <param name="mOCode">MOCode，模糊查询</param>
		/// <param name="timePeriodCode">TimePeriodCode，模糊查询</param>
		/// <param name="stepSequenceCode">StepSequenceCode，模糊查询</param>
		/// <param name="segmnetCode">SegmnetCode，模糊查询</param>
		/// <param name="itemCode">ItemCode，模糊查询</param>
		/// <param name="shiftCode">ShiftCode，模糊查询</param>
		/// <param name="errorCause">ErrorCause，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> ReportRealtimeLineErrorCauseQty数组</returns>
		public object[] QueryReportRealtimeLineErrorCauseQty( string modelCode, int shiftDay, string mOCode, string timePeriodCode, string stepSequenceCode, string segmnetCode, string itemCode, string shiftCode, string errorCause, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(ReportRealtimeLineErrorCauseQty), new PagerCondition(string.Format("select {0} from TBLRPTREALLINEECEQTY where 1=1 and MODELCODE like '{1}%'  and SHIFTDAY like '{2}%'  and MOCODE like '{3}%'  and TPCODE like '{4}%'  and SSCODE like '{5}%'  and SEGCODE like '{6}%'  and ITEMCODE like '{7}%'  and SHIFTCODE like '{8}%'  and ECCODE like '{9}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReportRealtimeLineErrorCauseQty)) , modelCode, shiftDay, mOCode, timePeriodCode, stepSequenceCode, segmnetCode, itemCode, shiftCode, errorCause), "MODELCODE,SHIFTDAY,MOCODE,TPCODE,SSCODE,SEGCODE,ITEMCODE,SHIFTCODE,ECCODE", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的ReportRealtimeLineErrorCauseQty
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-6-15 上午 15:14:38
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>ReportRealtimeLineErrorCauseQty的总记录数</returns>
		public object[] GetAllReportRealtimeLineErrorCauseQty()
		{
			return this.DataProvider.CustomQuery(typeof(ReportRealtimeLineErrorCauseQty), new SQLCondition(string.Format("select {0} from TBLRPTREALLINEECEQTY order by MODELCODE,SHIFTDAY,MOCODE,TPCODE,SSCODE,SEGCODE,ITEMCODE,SHIFTCODE,ECCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReportRealtimeLineErrorCauseQty)))));
		}


		#endregion

		#region ReportRealtimeLineErrorCodeQty
		/// <summary>
		/// 
		/// </summary>
		public ReportRealtimeLineErrorCodeQty CreateNewReportRealtimeLineErrorCodeQty()
		{
			return new ReportRealtimeLineErrorCodeQty();
		}

		public void AddReportRealtimeLineErrorCodeQty( ReportRealtimeLineErrorCodeQty reportRealtimeLineErrorCodeQty)
		{
			this.DataProvider.Insert( reportRealtimeLineErrorCodeQty );
		}

//		public void UpdateReportRealtimeLineErrorCodeQty(ReportRealtimeLineErrorCodeQty reportRealtimeLineErrorCodeQty)
//		{
//			this.DataProvider.Update( reportRealtimeLineErrorCodeQty );
//		}
		/// <summary>
		/// Laws Lu	
		///	2005/10/10	
		/// 更新报表数量
		/// </summary>
		/// <param name="reportRealtimeLineErrorCodeQty">实体</param>
		/// <param name="ngtimes">NG次数</param>
		public void ModifyReportRealtimeLineErrorCodeQty(ReportRealtimeLineErrorCodeQty reportRealtimeLineErrorCodeQty,int ngtimes/*,int IDMergeRule*/)
		{
			string sql = "update TBLRPTREALLINEECQTY set ectimes = ectimes  + " + ngtimes.ToString()
				//+ 1 * IDMergeRule 
				+ " where MODELCODE = '" + reportRealtimeLineErrorCodeQty.ModelCode + "'"
				+ " and ITEMCODE = '" + reportRealtimeLineErrorCodeQty.ItemCode + "'"
				+ " and MOCODE = '" + reportRealtimeLineErrorCodeQty.MOCode + "'"
				+ " and ShiftDay = '" + reportRealtimeLineErrorCodeQty.ShiftDay + "'"
				+ " and SHIFTCODE = '" + reportRealtimeLineErrorCodeQty.ShiftCode + "'"
				+ " and TPCODE = '" + reportRealtimeLineErrorCodeQty.TimePeriodCode + "'"
				+ " and SEGCODE = '" + reportRealtimeLineErrorCodeQty.SegmnetCode + "'"
				+ " and SSCODE = '" + reportRealtimeLineErrorCodeQty.StepSequenceCode + "'"
				+ " and ECODE = '" + reportRealtimeLineErrorCodeQty.ErrorCode + "'"
				+ " and ECGCODE = '" + reportRealtimeLineErrorCodeQty.ErrorCodeGroup + "'";

			this.DataProvider.CustomExecute(new SQLCondition(sql));
		}

		public void DeleteReportRealtimeLineErrorCodeQty(ReportRealtimeLineErrorCodeQty reportRealtimeLineErrorCodeQty)
		{
			this.DataProvider.Delete( reportRealtimeLineErrorCodeQty );
		}

//		public void DeleteReportRealtimeLineErrorCodeQty(ReportRealtimeLineErrorCodeQty[] reportRealtimeLineErrorCodeQty)
//		{
//			this.DataProvider.Delete( reportRealtimeLineErrorCodeQty );
//		}

		public object GetReportRealtimeLineErrorCodeQty( string modelCode, int shiftDay, string mOCode, string timePeriodCode, string stepSequenceCode, string segmnetCode, string itemCode, string shiftCode, string errorCode, string errorCodeGroup )
		{
			return this.DataProvider.CustomSearch(typeof(ReportRealtimeLineErrorCodeQty), new object[]{ modelCode, shiftDay, mOCode, timePeriodCode, stepSequenceCode, segmnetCode, itemCode, shiftCode, errorCode, errorCodeGroup });
		}

		/// <summary>
		/// ** 功能描述:	查询ReportRealtimeLineErrorCodeQty的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-6-15 上午 15:14:38
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="modelCode">ModelCode，模糊查询</param>
		/// <param name="shiftDay">ShiftDay，模糊查询</param>
		/// <param name="mOCode">MOCode，模糊查询</param>
		/// <param name="timePeriodCode">TimePeriodCode，模糊查询</param>
		/// <param name="stepSequenceCode">StepSequenceCode，模糊查询</param>
		/// <param name="segmnetCode">SegmnetCode，模糊查询</param>
		/// <param name="itemCode">ItemCode，模糊查询</param>
		/// <param name="shiftCode">ShiftCode，模糊查询</param>
		/// <param name="errorCode">ErrorCode，模糊查询</param>
		/// <param name="errorCodeGroup">ErrorCodeGroup，模糊查询</param>
		/// <returns> ReportRealtimeLineErrorCodeQty的总记录数</returns>
		public int QueryReportRealtimeLineErrorCodeQtyCount( string modelCode, int shiftDay, string mOCode, string timePeriodCode, string stepSequenceCode, string segmnetCode, string itemCode, string shiftCode, string errorCode, string errorCodeGroup)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTREALLINEECQTY where 1=1 and MODELCODE like '{0}%'  and SHIFTDAY like '{1}%'  and MOCODE like '{2}%'  and TPCODE like '{3}%'  and SSCODE like '{4}%'  and SEGCODE like '{5}%'  and ITEMCODE like '{6}%'  and SHIFTCODE like '{7}%'  and ECODE like '{8}%'  and ECGCODE like '{9}%' " , modelCode, shiftDay, mOCode, timePeriodCode, stepSequenceCode, segmnetCode, itemCode, shiftCode, errorCode, errorCodeGroup)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询ReportRealtimeLineErrorCodeQty
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-6-15 上午 15:14:38
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="modelCode">ModelCode，模糊查询</param>
		/// <param name="shiftDay">ShiftDay，模糊查询</param>
		/// <param name="mOCode">MOCode，模糊查询</param>
		/// <param name="timePeriodCode">TimePeriodCode，模糊查询</param>
		/// <param name="stepSequenceCode">StepSequenceCode，模糊查询</param>
		/// <param name="segmnetCode">SegmnetCode，模糊查询</param>
		/// <param name="itemCode">ItemCode，模糊查询</param>
		/// <param name="shiftCode">ShiftCode，模糊查询</param>
		/// <param name="errorCode">ErrorCode，模糊查询</param>
		/// <param name="errorCodeGroup">ErrorCodeGroup，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> ReportRealtimeLineErrorCodeQty数组</returns>
		public object[] QueryReportRealtimeLineErrorCodeQty( string modelCode, int shiftDay, string mOCode, string timePeriodCode, string stepSequenceCode, string segmnetCode, string itemCode, string shiftCode, string errorCode, string errorCodeGroup, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(ReportRealtimeLineErrorCodeQty), new PagerCondition(string.Format("select {0} from TBLRPTREALLINEECQTY where 1=1 and MODELCODE like '{1}%'  and SHIFTDAY like '{2}%'  and MOCODE like '{3}%'  and TPCODE like '{4}%'  and SSCODE like '{5}%'  and SEGCODE like '{6}%'  and ITEMCODE like '{7}%'  and SHIFTCODE like '{8}%'  and ECODE like '{9}%'  and ECGCODE like '{10}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReportRealtimeLineErrorCodeQty)) , modelCode, shiftDay, mOCode, timePeriodCode, stepSequenceCode, segmnetCode, itemCode, shiftCode, errorCode, errorCodeGroup), "MODELCODE,SHIFTDAY,MOCODE,TPCODE,SSCODE,SEGCODE,ITEMCODE,SHIFTCODE,ECODE,ECGCODE", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的ReportRealtimeLineErrorCodeQty
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-6-15 上午 15:14:38
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>ReportRealtimeLineErrorCodeQty的总记录数</returns>
		public object[] GetAllReportRealtimeLineErrorCodeQty()
		{
			return this.DataProvider.CustomQuery(typeof(ReportRealtimeLineErrorCodeQty), new SQLCondition(string.Format("select {0} from TBLRPTREALLINEECQTY order by MODELCODE,SHIFTDAY,MOCODE,TPCODE,SSCODE,SEGCODE,ITEMCODE,SHIFTCODE,ECODE,ECGCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReportRealtimeLineErrorCodeQty)))));
		}


		#endregion

		#region ReportRealtimeLineErrorLocationQty
		/// <summary>
		/// 
		/// </summary>
		public ReportRealtimeLineErrorLocationQty CreateNewReportRealtimeLineErrorLocationQty()
		{
			return new ReportRealtimeLineErrorLocationQty();
		}

		public void AddReportRealtimeLineErrorLocationQty( ReportRealtimeLineErrorLocationQty reportRealtimeLineErrorLocationQty)
		{
			this.DataProvider.Insert( reportRealtimeLineErrorLocationQty );
		}

		public void UpdateReportRealtimeLineErrorLocationQty(ReportRealtimeLineErrorLocationQty reportRealtimeLineErrorLocationQty)
		{
			this.DataProvider.Update( reportRealtimeLineErrorLocationQty );
		}

		public void DeleteReportRealtimeLineErrorLocationQty(ReportRealtimeLineErrorLocationQty reportRealtimeLineErrorLocationQty)
		{
			this.DataProvider.Delete( reportRealtimeLineErrorLocationQty );
		}

//		public void DeleteReportRealtimeLineErrorLocationQty(ReportRealtimeLineErrorLocationQty[] reportRealtimeLineErrorLocationQty)
//		{
//			this.DataProvider.Delete( reportRealtimeLineErrorLocationQty );
//		}

		public object GetReportRealtimeLineErrorLocationQty( string modelCode, int shiftDay, string mOCode, string timePeriodCode, string stepSequenceCode, string segmnetCode, string itemCode, string shiftCode, string errorLoaction )
		{
			return this.DataProvider.CustomSearch(typeof(ReportRealtimeLineErrorLocationQty), new object[]{ modelCode, shiftDay, mOCode, timePeriodCode, stepSequenceCode, segmnetCode, itemCode, shiftCode, errorLoaction });
		}

		/// <summary>
		/// ** 功能描述:	查询ReportRealtimeLineErrorLocationQty的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-6-15 上午 15:14:38
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="modelCode">ModelCode，模糊查询</param>
		/// <param name="shiftDay">ShiftDay，模糊查询</param>
		/// <param name="mOCode">MOCode，模糊查询</param>
		/// <param name="timePeriodCode">TimePeriodCode，模糊查询</param>
		/// <param name="stepSequenceCode">StepSequenceCode，模糊查询</param>
		/// <param name="segmnetCode">SegmnetCode，模糊查询</param>
		/// <param name="itemCode">ItemCode，模糊查询</param>
		/// <param name="shiftCode">ShiftCode，模糊查询</param>
		/// <param name="errorLoaction">ErrorLoaction，模糊查询</param>
		/// <returns> ReportRealtimeLineErrorLocationQty的总记录数</returns>
		public int QueryReportRealtimeLineErrorLocationQtyCount( string modelCode, int shiftDay, string mOCode, string timePeriodCode, string stepSequenceCode, string segmnetCode, string itemCode, string shiftCode, string errorLoaction)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTREALLINEELQTY where 1=1 and MODELCODE like '{0}%'  and SHIFTDAY like '{1}%'  and MOCODE like '{2}%'  and TPCODE like '{3}%'  and SSCODE like '{4}%'  and SEGCODE like '{5}%'  and ITEMCODE like '{6}%'  and SHIFTCODE like '{7}%'  and ELOC like '{8}%' " , modelCode, shiftDay, mOCode, timePeriodCode, stepSequenceCode, segmnetCode, itemCode, shiftCode, errorLoaction)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询ReportRealtimeLineErrorLocationQty
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-6-15 上午 15:14:38
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="modelCode">ModelCode，模糊查询</param>
		/// <param name="shiftDay">ShiftDay，模糊查询</param>
		/// <param name="mOCode">MOCode，模糊查询</param>
		/// <param name="timePeriodCode">TimePeriodCode，模糊查询</param>
		/// <param name="stepSequenceCode">StepSequenceCode，模糊查询</param>
		/// <param name="segmnetCode">SegmnetCode，模糊查询</param>
		/// <param name="itemCode">ItemCode，模糊查询</param>
		/// <param name="shiftCode">ShiftCode，模糊查询</param>
		/// <param name="errorLoaction">ErrorLoaction，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> ReportRealtimeLineErrorLocationQty数组</returns>
		public object[] QueryReportRealtimeLineErrorLocationQty( string modelCode, int shiftDay, string mOCode, string timePeriodCode, string stepSequenceCode, string segmnetCode, string itemCode, string shiftCode, string errorLoaction, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(ReportRealtimeLineErrorLocationQty), new PagerCondition(string.Format("select {0} from TBLRPTREALLINEELQTY where 1=1 and MODELCODE like '{1}%'  and SHIFTDAY like '{2}%'  and MOCODE like '{3}%'  and TPCODE like '{4}%'  and SSCODE like '{5}%'  and SEGCODE like '{6}%'  and ITEMCODE like '{7}%'  and SHIFTCODE like '{8}%'  and ELOC like '{9}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReportRealtimeLineErrorLocationQty)) , modelCode, shiftDay, mOCode, timePeriodCode, stepSequenceCode, segmnetCode, itemCode, shiftCode, errorLoaction), "MODELCODE,SHIFTDAY,MOCODE,TPCODE,SSCODE,SEGCODE,ITEMCODE,SHIFTCODE,ELOC", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的ReportRealtimeLineErrorLocationQty
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-6-15 上午 15:14:38
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>ReportRealtimeLineErrorLocationQty的总记录数</returns>
		public object[] GetAllReportRealtimeLineErrorLocationQty()
		{
			return this.DataProvider.CustomQuery(typeof(ReportRealtimeLineErrorLocationQty), new SQLCondition(string.Format("select {0} from TBLRPTREALLINEELQTY order by MODELCODE,SHIFTDAY,MOCODE,TPCODE,SSCODE,SEGCODE,ITEMCODE,SHIFTCODE,ELOC", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReportRealtimeLineErrorLocationQty)))));
		}


		#endregion

		#region ReportRealtimeLineErrorPartQty
		/// <summary>
		/// 
		/// </summary>
		public ReportRealtimeLineErrorPartQty CreateNewReportRealtimeLineErrorPartQty()
		{
			return new ReportRealtimeLineErrorPartQty();
		}

		public void AddReportRealtimeLineErrorPartQty( ReportRealtimeLineErrorPartQty reportRealtimeLineErrorPartQty)
		{
			this.DataProvider.Insert( reportRealtimeLineErrorPartQty );
		}

		public void UpdateReportRealtimeLineErrorPartQty(ReportRealtimeLineErrorPartQty reportRealtimeLineErrorPartQty)
		{
			this.DataProvider.Update( reportRealtimeLineErrorPartQty );
		}

		public void DeleteReportRealtimeLineErrorPartQty(ReportRealtimeLineErrorPartQty reportRealtimeLineErrorPartQty)
		{
			this.DataProvider.Delete( reportRealtimeLineErrorPartQty );
		}

//		public void DeleteReportRealtimeLineErrorPartQty(ReportRealtimeLineErrorPartQty[] reportRealtimeLineErrorPartQty)
//		{
//			this.DataProvider.Delete( reportRealtimeLineErrorPartQty );
//		}

		public object GetReportRealtimeLineErrorPartQty( string modelCode, int shiftDay, string mOCode, string timePeriodCode, string stepSequenceCode, string segmnetCode, string itemCode, string shiftCode, string errorPart )
		{
			return this.DataProvider.CustomSearch(typeof(ReportRealtimeLineErrorPartQty), new object[]{ modelCode, shiftDay, mOCode, timePeriodCode, stepSequenceCode, segmnetCode, itemCode, shiftCode, errorPart });
		}

		/// <summary>
		/// ** 功能描述:	查询ReportRealtimeLineErrorPartQty的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-6-15 上午 15:14:38
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="modelCode">ModelCode，模糊查询</param>
		/// <param name="shiftDay">ShiftDay，模糊查询</param>
		/// <param name="mOCode">MOCode，模糊查询</param>
		/// <param name="timePeriodCode">TimePeriodCode，模糊查询</param>
		/// <param name="stepSequenceCode">StepSequenceCode，模糊查询</param>
		/// <param name="segmnetCode">SegmnetCode，模糊查询</param>
		/// <param name="itemCode">ItemCode，模糊查询</param>
		/// <param name="shiftCode">ShiftCode，模糊查询</param>
		/// <param name="errorPart">ErrorPart，模糊查询</param>
		/// <returns> ReportRealtimeLineErrorPartQty的总记录数</returns>
		public int QueryReportRealtimeLineErrorPartQtyCount( string modelCode, int shiftDay, string mOCode, string timePeriodCode, string stepSequenceCode, string segmnetCode, string itemCode, string shiftCode, string errorPart)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTREALLINEEPQTY where 1=1 and MODELCODE like '{0}%'  and SHIFTDAY like '{1}%'  and MOCODE like '{2}%'  and TPCODE like '{3}%'  and SSCODE like '{4}%'  and SEGCODE like '{5}%'  and ITEMCODE like '{6}%'  and SHIFTCODE like '{7}%'  and EPART like '{8}%' " , modelCode, shiftDay, mOCode, timePeriodCode, stepSequenceCode, segmnetCode, itemCode, shiftCode, errorPart)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询ReportRealtimeLineErrorPartQty
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-6-15 上午 15:14:38
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="modelCode">ModelCode，模糊查询</param>
		/// <param name="shiftDay">ShiftDay，模糊查询</param>
		/// <param name="mOCode">MOCode，模糊查询</param>
		/// <param name="timePeriodCode">TimePeriodCode，模糊查询</param>
		/// <param name="stepSequenceCode">StepSequenceCode，模糊查询</param>
		/// <param name="segmnetCode">SegmnetCode，模糊查询</param>
		/// <param name="itemCode">ItemCode，模糊查询</param>
		/// <param name="shiftCode">ShiftCode，模糊查询</param>
		/// <param name="errorPart">ErrorPart，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> ReportRealtimeLineErrorPartQty数组</returns>
		public object[] QueryReportRealtimeLineErrorPartQty( string modelCode, int shiftDay, string mOCode, string timePeriodCode, string stepSequenceCode, string segmnetCode, string itemCode, string shiftCode, string errorPart, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(ReportRealtimeLineErrorPartQty), new PagerCondition(string.Format("select {0} from TBLRPTREALLINEEPQTY where 1=1 and MODELCODE like '{1}%'  and SHIFTDAY like '{2}%'  and MOCODE like '{3}%'  and TPCODE like '{4}%'  and SSCODE like '{5}%'  and SEGCODE like '{6}%'  and ITEMCODE like '{7}%'  and SHIFTCODE like '{8}%'  and EPART like '{9}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReportRealtimeLineErrorPartQty)) , modelCode, shiftDay, mOCode, timePeriodCode, stepSequenceCode, segmnetCode, itemCode, shiftCode, errorPart), "MODELCODE,SHIFTDAY,MOCODE,TPCODE,SSCODE,SEGCODE,ITEMCODE,SHIFTCODE,EPART", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的ReportRealtimeLineErrorPartQty
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-6-15 上午 15:14:38
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>ReportRealtimeLineErrorPartQty的总记录数</returns>
		public object[] GetAllReportRealtimeLineErrorPartQty()
		{
			return this.DataProvider.CustomQuery(typeof(ReportRealtimeLineErrorPartQty), new SQLCondition(string.Format("select {0} from TBLRPTREALLINEEPQTY order by MODELCODE,SHIFTDAY,MOCODE,TPCODE,SSCODE,SEGCODE,ITEMCODE,SHIFTCODE,EPART", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReportRealtimeLineErrorPartQty)))));
		}


		#endregion

		#region ReportRealtimeLineQty
		/// <summary>
		/// 
		/// </summary>
		public ReportRealtimeLineQty CreateNewReportRealtimeLineQty()
		{
			return new ReportRealtimeLineQty();
		}

		public void AddReportRealtimeLineQty( ReportRealtimeLineQty reportRealtimeLineQty)
		{
			this.DataProvider.Insert( reportRealtimeLineQty );
		}

//		public void UpdateReportRealtimeLineQty(ReportRealtimeLineQty reportRealtimeLineQty)
//		{
//			this.DataProvider.Update( reportRealtimeLineQty );
//		}
		/// <summary>
		/// Laws Lu	
		///	2005/10/10	
		/// 更新报表数量
		/// </summary>
		/// <param name="reportRealtimeLineQty">实体</param>
		///<param name="qty">数量</param>
		///<param name="ngtimes">ng次数</param>
		///<param name="allGoodQty">直通个数</param>
		public void ModifyReportRealtimeLineQty(ReportRealtimeLineQty reportRealtimeLineQty,int qty,int ngtimes,int allGoodQty,string aType)
		{
			string sql = String.Empty;
			if(aType == ActionType.DataCollectAction_GoMO)
			{
				sql = "update TBLRPTREALLINEQTY set outputqty = outputqty  + " + qty.ToString() 
					//Laws Lu,2005/10/26,新增	中间投入量
					//+ ",INPUTQTY = INPUTQTY + " + reportRealtimeLineQty.InputQty.ToString() + ""
					//+ ",ngtimes = ngtimes + " + ngtimes.ToString() + ""
					+ ",ALLGOODQTY = ALLGOODQTY + " + allGoodQty.ToString() + ""
					+ ",EAttribute1 = EAttribute1 + " + reportRealtimeLineQty.EAttribute1.ToString() + ""
					//+ ",qtyflag = '" + reportRealtimeLineQty.QtyFlag + "'"
					+ " where MODELCODE = '" + reportRealtimeLineQty.ModelCode + "'"
					+ " and ITEMCODE = '" + reportRealtimeLineQty.ItemCode + "'"
					+ " and MOCODE = '" + reportRealtimeLineQty.MOCode + "'"
					+ " and ShiftDay = '" + reportRealtimeLineQty.ShiftDay + "'"
					+ " and SHIFTCODE = '" + reportRealtimeLineQty.ShiftCode + "'"
					+ " and TPCODE = '" + reportRealtimeLineQty.TimePeriodCode + "'"
					+ " and SEGCODE = '" + reportRealtimeLineQty.SegmentCode + "'"
					+ " and SSCODE = '" + reportRealtimeLineQty.StepSequenceCode + "'"
					+ " and QTYFLAG = '" + reportRealtimeLineQty.QtyFlag + "'";
			}
			else if(qty != 0 || reportRealtimeLineQty.InputQty != 0 || allGoodQty != 0 || ngtimes != 0)
			{
				sql = "update TBLRPTREALLINEQTY set outputqty = outputqty  + " + qty.ToString() 
					//Laws Lu,2005/10/26,新增	中间投入量
					+ ",INPUTQTY = INPUTQTY + " + reportRealtimeLineQty.InputQty.ToString() + ""
					+ ",ngtimes = ngtimes + " + ngtimes.ToString() + ""
					+ ",ALLGOODQTY = ALLGOODQTY + " + allGoodQty.ToString() + ""
					+ ",MoAllGoodQty = MoAllGoodQty + " + reportRealtimeLineQty.MoAllGoodQty.ToString() + ""
					//+ ",qtyflag = '" + reportRealtimeLineQty.QtyFlag + "'"
					+ " where MODELCODE = '" + reportRealtimeLineQty.ModelCode + "'"
					+ " and ITEMCODE = '" + reportRealtimeLineQty.ItemCode + "'"
					+ " and MOCODE = '" + reportRealtimeLineQty.MOCode + "'"
					+ " and ShiftDay = '" + reportRealtimeLineQty.ShiftDay + "'"
					+ " and SHIFTCODE = '" + reportRealtimeLineQty.ShiftCode + "'"
					+ " and TPCODE = '" + reportRealtimeLineQty.TimePeriodCode + "'"
					+ " and SEGCODE = '" + reportRealtimeLineQty.SegmentCode + "'"
					+ " and SSCODE = '" + reportRealtimeLineQty.StepSequenceCode + "'"
					+ " and QTYFLAG = '" + reportRealtimeLineQty.QtyFlag + "'";
			}
			int iReturn = 0;
			iReturn = ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).CustomExecuteWithReturn(new SQLCondition(sql));

			if(iReturn <= 0)
			{
				throw new Exception("$Error_UpdateMO $CS_PLEASE_RETRY");
			}


		}

		public void DeleteReportRealtimeLineQty(ReportRealtimeLineQty reportRealtimeLineQty)
		{
			this.DataProvider.Delete( reportRealtimeLineQty );
		}

//		public void DeleteReportRealtimeLineQty(ReportRealtimeLineQty[] reportRealtimeLineQty)
//		{
//			this.DataProvider.Delete( reportRealtimeLineQty );
//		}

		public object GetReportRealtimeLineQty( string mOCode, string timePeriodCode, string stepSequenceCode, string segmentCode, string itemCode, string shiftCode, string modelCode, int shiftDay ,string qtyFlag)
		{
			return this.DataProvider.CustomSearch(typeof(ReportRealtimeLineQty), new object[]{ mOCode, timePeriodCode, stepSequenceCode, segmentCode, itemCode, shiftCode, modelCode, shiftDay ,qtyFlag });
		}



		/// <summary>
		/// ** 功能描述:	查询ReportRealtimeLineQty的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-6-15 上午 15:14:38
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="mOCode">MOCode，模糊查询</param>
		/// <param name="timePeriodCode">TimePeriodCode，模糊查询</param>
		/// <param name="stepSequenceCode">StepSequenceCode，模糊查询</param>
		/// <param name="segmentCode">SegmentCode，模糊查询</param>
		/// <param name="itemCode">ItemCode，模糊查询</param>
		/// <param name="shiftCode">ShiftCode，模糊查询</param>
		/// <param name="modelCode">ModelCode，模糊查询</param>
		/// <param name="shiftDay">ShiftDay，模糊查询</param>
		/// <returns> ReportRealtimeLineQty的总记录数</returns>
		public int QueryReportRealtimeLineQtyCount( string mOCode, string timePeriodCode, string stepSequenceCode, string segmentCode, string itemCode, string shiftCode, string modelCode, int shiftDay)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTREALLINEQTY where 1=1 and MOCODE like '{0}%'  and TPCODE like '{1}%'  and SSCODE like '{2}%'  and SEGCODE like '{3}%'  and ITEMCODE like '{4}%'  and SHIFTCODE like '{5}%'  and MODELCODE like '{6}%'  and SHIFTDAY like '{7}%' " , mOCode, timePeriodCode, stepSequenceCode, segmentCode, itemCode, shiftCode, modelCode, shiftDay)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询ReportRealtimeLineQty
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-6-15 上午 15:14:38
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="mOCode">MOCode，模糊查询</param>
		/// <param name="timePeriodCode">TimePeriodCode，模糊查询</param>
		/// <param name="stepSequenceCode">StepSequenceCode，模糊查询</param>
		/// <param name="segmentCode">SegmentCode，模糊查询</param>
		/// <param name="itemCode">ItemCode，模糊查询</param>
		/// <param name="shiftCode">ShiftCode，模糊查询</param>
		/// <param name="modelCode">ModelCode，模糊查询</param>
		/// <param name="shiftDay">ShiftDay，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> ReportRealtimeLineQty数组</returns>
		public object[] QueryReportRealtimeLineQty( string mOCode, string timePeriodCode, string stepSequenceCode, string segmentCode, string itemCode, string shiftCode, string modelCode, int shiftDay, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(ReportRealtimeLineQty), new PagerCondition(string.Format("select {0} from TBLRPTREALLINEQTY where 1=1 and MOCODE like '{1}%'  and TPCODE like '{2}%'  and SSCODE like '{3}%'  and SEGCODE like '{4}%'  and ITEMCODE like '{5}%'  and SHIFTCODE like '{6}%'  and MODELCODE like '{7}%'  and SHIFTDAY like '{8}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReportRealtimeLineQty)) , mOCode, timePeriodCode, stepSequenceCode, segmentCode, itemCode, shiftCode, modelCode, shiftDay), "MOCODE,TPCODE,SSCODE,SEGCODE,ITEMCODE,SHIFTCODE,MODELCODE,SHIFTDAY", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的ReportRealtimeLineQty
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-6-15 上午 15:14:38
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>ReportRealtimeLineQty的总记录数</returns>
		public object[] GetAllReportRealtimeLineQty()
		{
			return this.DataProvider.CustomQuery(typeof(ReportRealtimeLineQty), new SQLCondition(string.Format("select {0} from TBLRPTREALLINEQTY order by MOCODE,TPCODE,SSCODE,SEGCODE,ITEMCODE,SHIFTCODE,MODELCODE,SHIFTDAY", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReportRealtimeLineQty)))));
		}

		public int GetMOOutPutQty( string mOCode)
		{
			int iReturn = 0;
			//			object[] objs;
			//			objs = this.DataProvider.CustomQuery(typeof(Domain.Report.ReportRealtimeLineQty),new SQLCondition(string.Format("SELECT count(*) FROM tblrptreallineqty WHERE mocode = '{0}' AND qtyflag = 'Y'",mOCode)));
			//
			//			if(objs != null && objs.Length > 0)
			//			{
			iReturn = this.DataProvider.GetCount(new SQLCondition(
				string.Format("SELECT decode(SUM (outputqty),null,0,SUM (outputqty)) FROM tblrptreallineqty WHERE mocode = '{0}' AND qtyflag = 'Y'",mOCode)));
			//			}

			return iReturn;
		}

		#endregion

		#region USERGROUP2ITEM
		/// <summary>
		/// 
		/// </summary>
		public USERGROUP2ITEM CreateNewUSERGROUP2ITEM()
		{
			return new USERGROUP2ITEM();
		}

		public void AddUSERGROUP2ITEM( USERGROUP2ITEM uSERGROUP2ITEM)
		{
			this._helper.AddDomainObject( uSERGROUP2ITEM );
		}

		public void UpdateUSERGROUP2ITEM(USERGROUP2ITEM uSERGROUP2ITEM)
		{
			this._helper.UpdateDomainObject( uSERGROUP2ITEM );
		}

		public void DeleteUSERGROUP2ITEM(USERGROUP2ITEM uSERGROUP2ITEM)
		{
			this._helper.DeleteDomainObject( uSERGROUP2ITEM );
		}

		public void DeleteUSERGROUP2ITEM(USERGROUP2ITEM[] uSERGROUP2ITEM)
		{
			this._helper.DeleteDomainObject( uSERGROUP2ITEM );
		}

		public object GetUSERGROUP2ITEM( string iTEMCODE, string uSERGROUPCODE )
		{
			return this.DataProvider.CustomSearch(typeof(USERGROUP2ITEM), new object[]{ iTEMCODE, uSERGROUPCODE });
		}

		/// <summary>
		/// ** 功能描述:	查询USERGROUP2ITEM的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2006-8-12 12:47:19
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="iTEMCODE">ITEMCODE，模糊查询</param>
		/// <param name="uSERGROUPCODE">USERGROUPCODE，模糊查询</param>
		/// <returns> USERGROUP2ITEM的总记录数</returns>
		public int QueryUSERGROUP2ITEMCount( string iTEMCODE, string uSERGROUPCODE)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from USERGROUP2ITEM where 1=1 and ITEMCODE like '{0}%'  and USERGROUPCODE like '{1}%' " , iTEMCODE, uSERGROUPCODE)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询USERGROUP2ITEM
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2006-8-12 12:47:19
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="iTEMCODE">ITEMCODE，模糊查询</param>
		/// <param name="uSERGROUPCODE">USERGROUPCODE，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> USERGROUP2ITEM数组</returns>
		public object[] QueryUSERGROUP2ITEM( string iTEMCODE, string uSERGROUPCODE, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(USERGROUP2ITEM), new PagerCondition(string.Format("select {0} from USERGROUP2ITEM where 1=1 and ITEMCODE like '{1}%'  and USERGROUPCODE like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(USERGROUP2ITEM)) , iTEMCODE, uSERGROUPCODE), "ITEMCODE,USERGROUPCODE", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的USERGROUP2ITEM
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2006-8-12 12:47:19
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>USERGROUP2ITEM的总记录数</returns>
		public object[] GetAllUSERGROUP2ITEM()
		{
			return this.DataProvider.CustomQuery(typeof(USERGROUP2ITEM), new SQLCondition(string.Format("select {0} from USERGROUP2ITEM order by ITEMCODE,USERGROUPCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(USERGROUP2ITEM)))));
		}


		#endregion

		#region ReportErrorCode2Resource
		/// <summary>
		/// 
		/// </summary>
		public ReportErrorCode2Resource CreateNewReportErrorCode2Resource()
		{
			return new ReportErrorCode2Resource();
		}

		public void AddReportErrorCode2Resource( ReportErrorCode2Resource reportErrorCode2Resource)
		{
			this._helper.AddDomainObject( reportErrorCode2Resource );
		}

		public void UpdateReportErrorCode2Resource(ReportErrorCode2Resource reportErrorCode2Resource)
		{
			this._helper.UpdateDomainObject( reportErrorCode2Resource );
		}

		public void UpdateReportErrorCode2ResourceQty(ReportErrorCode2Resource reportErrorCode2Resource)
		{
			string sql = " UPDATE TBLRPTRESECG SET NGTIMES = NGTIMES + " + reportErrorCode2Resource.NGTimes 
				+ " WHERE MODELCODE = '" + reportErrorCode2Resource.ModelCode + "'" 
				+ " AND ITEMCODE = '" + reportErrorCode2Resource.ItemCode + "'" 
				+ " AND MOCODE = '" + reportErrorCode2Resource.MOCode + "'" 
				+ " AND SHIFTDAY = " + reportErrorCode2Resource.ShiftDay + "" 
				+ " AND SHIFTCODE = '" +reportErrorCode2Resource.ShiftCode +  "'" 
				+ " AND TPCODE = '" + reportErrorCode2Resource.TPCode + "'"
				+ " AND OPCODE = '" + reportErrorCode2Resource.OPCode + "'" 
				+ " AND RESCODE = '" + reportErrorCode2Resource.ResourceCode + "'" 
				+ " AND SEGCODE = '" +reportErrorCode2Resource.SegCode +  "'" 
				+ " AND SSCODE = '" + reportErrorCode2Resource.SSCode + "'" 
				+ " AND ECGCODE = '" +reportErrorCode2Resource.ErrorCodeGroup +  "'" 
				+ " AND ECCODE = '" + reportErrorCode2Resource.ErrorCode + "'";

			this.DataProvider.CustomExecute(new SQLCondition(sql));
		}

		public void DeleteReportErrorCode2Resource(ReportErrorCode2Resource reportErrorCode2Resource)
		{
			this._helper.DeleteDomainObject( reportErrorCode2Resource );
		}

		public void DeleteReportErrorCode2Resource(ReportErrorCode2Resource[] reportErrorCode2Resource)
		{
			this._helper.DeleteDomainObject( reportErrorCode2Resource );
		}

		public object GetReportErrorCode2Resource( string modelCode, string itemCode, string mOCode, int shiftDay, string shiftCode, string tPCode, string oPCode, string resourceCode, string segCode, string sSCode, string errorCodeGroup, string errorCode )
		{
			return this.DataProvider.CustomSearch(typeof(ReportErrorCode2Resource), new object[]{ modelCode, itemCode, mOCode, shiftDay, shiftCode, tPCode, oPCode, resourceCode, segCode, sSCode, errorCodeGroup, errorCode });
		}

		/// <summary>
		/// ** 功能描述:	查询ReportErrorCode2Resource的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by ****
		/// ** 日 期:		2006-9-7 9:28:56
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="modelCode">ModelCode，模糊查询</param>
		/// <param name="itemCode">ItemCode，模糊查询</param>
		/// <param name="mOCode">MOCode，模糊查询</param>
		/// <param name="shiftDay">ShiftDay，模糊查询</param>
		/// <param name="shiftCode">ShiftCode，模糊查询</param>
		/// <param name="tPCode">TPCode，模糊查询</param>
		/// <param name="oPCode">OPCode，模糊查询</param>
		/// <param name="resourceCode">ResourceCode，模糊查询</param>
		/// <param name="segCode">SegCode，模糊查询</param>
		/// <param name="sSCode">SSCode，模糊查询</param>
		/// <param name="errorCodeGroup">ErrorCodeGroup，模糊查询</param>
		/// <param name="errorCode">ErrorCode，模糊查询</param>
		/// <returns> ReportErrorCode2Resource的总记录数</returns>
		public int QueryReportErrorCode2ResourceCount( string modelCode, string itemCode, string mOCode, int shiftDay, string shiftCode, string tPCode, string oPCode, string resourceCode, string segCode, string sSCode, string errorCodeGroup, string errorCode)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTRESECG where 1=1 and ModelCode like '{0}%'  and ItemCode like '{1}%'  and MOCode like '{2}%'  and ShiftDay like '{3}%'  and ShiftCode like '{4}%'  and TPCode like '{5}%'  and OPCode like '{6}%'  and RESCODE like '{7}%'  and SegCode like '{8}%'  and SSCode like '{9}%'  and ECGCode like '{10}%'  and ECCode like '{11}%' " , modelCode, itemCode, mOCode, shiftDay, shiftCode, tPCode, oPCode, resourceCode, segCode, sSCode, errorCodeGroup, errorCode)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询ReportErrorCode2Resource
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by ****
		/// ** 日 期:		2006-9-7 9:28:56
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="modelCode">ModelCode，模糊查询</param>
		/// <param name="itemCode">ItemCode，模糊查询</param>
		/// <param name="mOCode">MOCode，模糊查询</param>
		/// <param name="shiftDay">ShiftDay，模糊查询</param>
		/// <param name="shiftCode">ShiftCode，模糊查询</param>
		/// <param name="tPCode">TPCode，模糊查询</param>
		/// <param name="oPCode">OPCode，模糊查询</param>
		/// <param name="resourceCode">ResourceCode，模糊查询</param>
		/// <param name="segCode">SegCode，模糊查询</param>
		/// <param name="sSCode">SSCode，模糊查询</param>
		/// <param name="errorCodeGroup">ErrorCodeGroup，模糊查询</param>
		/// <param name="errorCode">ErrorCode，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> ReportErrorCode2Resource数组</returns>
		public object[] QueryReportErrorCode2Resource( string modelCode, string itemCode, string mOCode, int shiftDay, string shiftCode, string tPCode, string oPCode, string resourceCode, string segCode, string sSCode, string errorCodeGroup, string errorCode, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(ReportErrorCode2Resource), new PagerCondition(string.Format("select {0} from TBLRPTRESECG where 1=1 and ModelCode like '{1}%'  and ItemCode like '{2}%'  and MOCode like '{3}%'  and ShiftDay like '{4}%'  and ShiftCode like '{5}%'  and TPCode like '{6}%'  and OPCode like '{7}%'  and RESCODE like '{8}%'  and SegCode like '{9}%'  and SSCode like '{10}%'  and ECGCode like '{11}%'  and ECCode like '{12}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReportErrorCode2Resource)) , modelCode, itemCode, mOCode, shiftDay, shiftCode, tPCode, oPCode, resourceCode, segCode, sSCode, errorCodeGroup, errorCode), "ModelCode,ItemCode,MOCode,ShiftDay,ShiftCode,TPCode,OPCode,RESCODE,SegCode,SSCode,ECGCode,ECCode", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的ReportErrorCode2Resource
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by ****
		/// ** 日 期:		2006-9-7 9:28:56
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>ReportErrorCode2Resource的总记录数</returns>
		public object[] GetAllReportErrorCode2Resource()
		{
			return this.DataProvider.CustomQuery(typeof(ReportErrorCode2Resource), new SQLCondition(string.Format("select {0} from TBLRPTRESECG order by ModelCode,ItemCode,MOCode,ShiftDay,ShiftCode,TPCode,OPCode,RESCODE,SegCode,SSCode,ECGCode,ECCode", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReportErrorCode2Resource)))));
		}


		#endregion

		#region FactoryWeekCheck
		/// <summary>
		/// 
		/// </summary>
		public FactoryWeekCheck CreateNewFactoryWeekCheck()
		{
			return new FactoryWeekCheck();
		}

		public void AddFactoryWeekCheck( FactoryWeekCheck factoryWeekCheck)
		{
			this.DataProvider.Insert( factoryWeekCheck );
		}

		public void UpdateFactoryWeekCheck(FactoryWeekCheck factoryWeekCheck)
		{
			this.DataProvider.Update( factoryWeekCheck );
		}

		public void DeleteFactoryWeekCheck(FactoryWeekCheck factoryWeekCheck)
		{
			this.DataProvider.Delete( factoryWeekCheck );
		}

		public void DeleteFactoryWeekCheck(FactoryWeekCheck[] factoryWeekCheck)
		{
			foreach(FactoryWeekCheck weekCheck in factoryWeekCheck)
			{
				this.DataProvider.Delete( weekCheck );
			}
		}

		public object GetFactoryWeekCheck( string CheckID )
		{
			return this.DataProvider.CustomSearch(typeof(FactoryWeekCheck), new object[]{ CheckID });
		}

		/// <summary>
		/// 查询FactoryWeekCheck的总行数
		/// </summary>
		/// <param name="factoryID">factoryID</param>
		/// <param name="weekNo">weekNo</param>
		/// <returns> FactoryWeekCheck的总记录数</returns>
		public int QueryFactoryWeekCheckCount( string factoryID, string weekNo )
		{
			if (factoryID == "0")
			{
				factoryID = "";
			}
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLFACTORYWEEKCHECK where 1=1 and FACTORYID like '{0}%'  and WEEKNO like '{1}%' " , factoryID, weekNo )));
		}

		/// <summary>
		/// 分页查询FactoryWeekCheck
		/// </summary>
		/// <param name="factoryID">factoryID</param>
		/// <param name="weekNo">weekNo</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> FactoryWeekCheck数组</returns>
		public object[] QueryFactoryWeekCheck( string factoryID, string weekNo, int inclusive, int exclusive  )
		{
			if (factoryID == "0")
			{
				factoryID = "";
			}
			return this.DataProvider.CustomQuery(typeof(FactoryWeekCheck), new PagerCondition(string.Format("select {0} from TBLFACTORYWEEKCHECK where 1=1 and FACTORYID like '{1}%'  and WEEKNO like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(FactoryWeekCheck)) , factoryID, weekNo), "FACTORYID,WEEKNO", inclusive, exclusive));
		}

		/// <summary>
		/// 获得所有的FactoryWeekCheck
		/// </summary>
		/// <returns>FactoryWeekCheck的总记录数</returns>
		public object[] GetAllFactoryWeekCheck()
		{
			return this.DataProvider.CustomQuery(typeof(FactoryWeekCheck), new SQLCondition(string.Format("select {0} from TBLFACTORYWEEKCHECK order by FACTORYID,WEEKNO", DomainObjectUtility.GetDomainObjectFieldsString(typeof(FactoryWeekCheck)))));
		}

		#endregion

        #region ReportSOQty

        public object[] QueryMOCodeFromReportSOQty(int shiftDay, string shiftCode, string resCode)
        {
            object[] returnValue = null;

            string sql = string.Empty;

            sql += "SELECT DISTINCT mocode " + "\r\n";
            sql += "FROM tblrptsoqty, tblmesentitylist " + "\r\n";
            sql += "WHERE tblrptsoqty.tblmesentitylist_serial = tblmesentitylist.serial " + "\r\n";

            if (shiftDay > 0)
            {
                sql += "AND tblrptsoqty.shiftday = " + shiftDay.ToString() + " " + "\r\n";
            }

            if (shiftCode.Trim().Length > 0)
            {
                sql += "AND tblmesentitylist.shiftcode = '" + shiftCode.Trim().ToUpper() + "' " + "\r\n";
            }

            if (resCode.Trim().Length > 0)
            {
                sql += "AND tblmesentitylist.rescode = '" + resCode.Trim().ToUpper() + "' " + "\r\n";
            }

            returnValue = this.DataProvider.CustomQuery(typeof(ReportSOQty), new SQLCondition(sql));

            return returnValue;
        }

        public object[] QueryOPCountSumFromReportSOQty(int shiftDay, string shiftCode, string resCode, string moCode)
        {
            object[] returnValue = null;

            string sql = string.Empty;

            sql += "SELECT SUM(tblrptsoqty.opcount) AS opcount " + "\r\n";
            sql += "FROM tblrptsoqty, tblmesentitylist " + "\r\n";
            sql += "WHERE tblrptsoqty.tblmesentitylist_serial = tblmesentitylist.serial " + "\r\n";

            if (shiftDay > 0)
            {
                sql += "AND tblrptsoqty.shiftday = " + shiftDay.ToString() + " " + "\r\n";
            }

            if (shiftCode.Trim().Length > 0)
            {
                sql += "AND tblmesentitylist.shiftcode = '" + shiftCode.Trim().ToUpper() + "' " + "\r\n";
            }

            if (resCode.Trim().Length > 0)
            {
                sql += "AND tblmesentitylist.rescode = '" + resCode.Trim().ToUpper() + "' " + "\r\n";
            }

            if (moCode.Trim().Length > 0)
            {
                sql += "AND tblrptsoqty.mocode = '" + moCode.Trim().ToUpper() + "' " + "\r\n";
            }

            returnValue = this.DataProvider.CustomQuery(typeof(ReportSOQty), new SQLCondition(sql));

            return returnValue;
        }

        #endregion


        #region TimeDimension
        public object GetTimeDimension(int ddate)
        {
            return this.DataProvider.CustomSearch(typeof(Domain.Report.TimeDimension), new object[] { ddate });
        }
        #endregion

    }
}

