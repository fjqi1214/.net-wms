using System;
using System.Collections;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Common.Helper;  
using BenQGuru.eMES.Common.DomainDataProvider;   
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.TS;
using BenQGuru.eMES.Domain.TSModel;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QuerySNNGFacade 的摘要说明。
	/// </summary>
	public class QuerySNNGFacade
	{
		private  IDomainDataProvider _domainDataProvider= null;

		public QuerySNNGFacade( IDomainDataProvider domainDataProvider )
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

		public object[] QuerySNNG(
			string itemCodes,string moCodes,
			string stepSequenceCodes,string startSn,string endSn,string resourceCodes,
			string FrmDateFrom,string FrmDateTo,
			int inclusive,int exclusive)
		{
			string itemCondition = "";
			if( itemCodes != "" && itemCodes != null )
			{
				itemCondition = string.Format(
					@" and itemcode in ({0})",FormatHelper.ProcessQueryValues( itemCodes ) );
			}

			string moCondition = "";
			if( moCodes != "" && moCodes != null )
			{
				moCondition = string.Format(
					@" and mocode in ({0})",FormatHelper.ProcessQueryValues( moCodes ) );
			}

			string stepSeqCondition = "";
			if( stepSequenceCodes != "" && stepSequenceCodes != null )
			{
				stepSeqCondition = string.Format(
					@" and FRMsscode in ({0})",FormatHelper.ProcessQueryValues( stepSequenceCodes ) );
			}

			string SnCondition = string.Empty;
			SnCondition = FormatHelper.GetRCardRangeSql("rcard",startSn.ToUpper(),endSn.ToUpper());
			
			// Added By Karron Qiu,2006-7-3
			string resCondition = "";
			if(resourceCodes != null && resourceCodes != "")
			{
				resCondition = string.Format(
					@" and FRMRESCODE in ({0})",FormatHelper.ProcessQueryValues( resourceCodes ) );
			}

			string frmDateCondition = "";
			frmDateCondition = FormatHelper.GetDateRangeSql("SHIFTDAY",FormatHelper.TODateInt(FrmDateFrom),
				FormatHelper.TODateInt(FrmDateTo)); 
			//End.


			string fields = DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.TS.TS)).ToUpper();
			fields = fields.Replace("TSSTATUS","decode(tsstatus,'tsstatus_reflow','tsstatus_complete',tsstatus) as tsstatus");
			string sql = string.Format(
				@"select {0} from TBLTS where 1=1 {1}{2}{3}{4} {5} {6}",
				fields,
				itemCondition,moCondition,stepSeqCondition,SnCondition,resCondition,frmDateCondition);
#if DEBUG
			Log.Info(
				new PagerCondition(sql,
				"mocode,itemcode,RCARD",
				inclusive,exclusive,true).SQLText);
#endif
			return this.DataProvider.CustomQuery(
				typeof(Domain.TS.TS),
				new PagerCondition(sql,
				"mocode,itemcode,RCARD",
				inclusive,exclusive,true));
		}

		public int QuerySNNGCount(
			string itemCodes,string moCodes,
			string stepSequenceCodes,string startSn,string endSn,string resourceCodes,string FrmDateFrom,string FrmDateTo)
		{
			string itemCondition = "";
			if( itemCodes != "" && itemCodes != null )
			{
				itemCondition = string.Format(
					@" and itemcode in ({0})",FormatHelper.ProcessQueryValues( itemCodes ) );
			}

			string moCondition = "";
			if( moCodes != "" && moCodes != null )
			{
				moCondition = string.Format(
					@" and mocode in ({0})",FormatHelper.ProcessQueryValues( moCodes ) );
			}

			string stepSeqCondition = "";
			if( stepSequenceCodes != "" && stepSequenceCodes != null )
			{
				stepSeqCondition = string.Format(
					@" and FRMsscode in ({0})",FormatHelper.ProcessQueryValues( stepSequenceCodes ) );
			}

			string SnCondition = string.Empty;
			SnCondition = FormatHelper.GetRCardRangeSql("rcard",startSn.ToUpper(),endSn.ToUpper());

			// Added By Karron Qiu,2006-7-3
			string resCondition = "";
			if(resourceCodes != null && resourceCodes != "")
			{
				resCondition = string.Format(
					@" and FRMRESCODE in ({0})",FormatHelper.ProcessQueryValues( resourceCodes ) );
			}

			string frmDateCondition = "";
			frmDateCondition = FormatHelper.GetDateRangeSql("FRMDATE",FormatHelper.TODateInt(FrmDateFrom),
				FormatHelper.TODateInt(FrmDateTo)); 
			//End.

//			string startSnCondition = "";
//			if( startSn != "" && startSn != null )
//			{
//				startSnCondition = string.Format(
//					@" and rcard >= '{0}'",startSn.ToUpper());
//			}
//
//			string endSnCondition = "";
//			if( endSn != "" && endSn != null )
//			{
//				endSnCondition = string.Format(
//					@" and rcard <= '{0}'",endSn.ToUpper() );
//			}

			string sql = string.Format(
				@"select {0} from TBLTS where 1=1 {1}{2}{3}{4} {5} {6}",
				"count(*)",
				itemCondition,moCondition,stepSeqCondition,SnCondition,resCondition,frmDateCondition);
#if DEBUG
//			Log.Info(
//				new SQLCondition(
//				string.Format(
//				@"select {0} from TBLTS where 1=1 {1}{2}{3}{4}{5}",
//				"count(mocode)",
//				itemCondition,moCondition,stepSeqCondition,startSnCondition,endSnCondition)).SQLText);
			Log.Info(new SQLCondition(sql).SQLText);
#endif
//			return this.DataProvider.GetCount(
//				new SQLCondition(
//				string.Format(
//				@"select {0} from TBLTS where 1=1 {1}{2}{3}{4}{5}",
//				"count(mocode)",
//				itemCondition,moCondition,stepSeqCondition,startSnCondition,endSnCondition)));
			return this.DataProvider.GetCount(new SQLCondition(sql));
		}

		public object[] QuerySNNGDetails(
			string moCode,string sn,string opCode,string resourceCode,
			int inclusive,int exclusive)
		{
#if DEBUG
			Log.Info(
				new PagerCondition(
				string.Format(
				@"select distinct {0} from TBLTSERRORCODE,TBLTS where TBLTSERRORCODE.TSID = TBLTS.TSID and TBLTSERRORCODE.rcard = '{1}'",
				"FRMSSCODE,FRMRESCODE,ECGCODE,ECODE,FRMDATE,FRMTIME,FRMMEMO",
				sn.ToUpper()),
				"FRMSSCODE,FRMRESCODE,ECGCODE,ECODE",
				inclusive,exclusive,true).SQLText);
#endif
			object[] objs = this.DataProvider.CustomQuery(
				typeof(QDOSNNGList),
				new PagerCondition(
				string.Format(
				@"select distinct {0} from TBLTSERRORCODE,TBLTS where TBLTSERRORCODE.TSID = TBLTS.TSID and TBLTSERRORCODE.rcard = '{1}'",
				"FRMSSCODE,FRMRESCODE,ECGCODE,ECODE,FRMDATE,FRMTIME,FRMMEMO",
				sn.ToUpper()),
				"FRMSSCODE,FRMRESCODE,ECGCODE,ECODE",
				inclusive,exclusive,true));

			if( objs != null )
			{
				foreach(QDOSNNGList obj in objs)
				{
					object[] ec = this.DataProvider.CustomQuery(typeof(ErrorCodeA),
						new SQLCondition(
						string.Format(@"select ecdesc from tblec where ecode = '{0}'",obj.ErrorCode)));
					if( ec != null )
					{
						obj.ErrorCodeDesc = (ec[0] as ErrorCodeA).ErrorDescription;
					}
					else
					{
						obj.ErrorCodeDesc = string.Empty;
					}

					object[] eg = this.DataProvider.CustomQuery(typeof(ErrorCodeGroupA),
						new SQLCondition(String.Format("select ecgdesc from tblecg where ecgcode ='{0}'",obj.ErrorCodeGroup)));

					if(eg != null)
					{
						obj.ErrorCodeGroupDesc = ((ErrorCodeGroupA)eg[0]).ErrorCodeGroupDescription ;
					}
				}
			}
			return objs;
		}

		public int QuerySNNGDetailsCount(
			string moCode,string sn,string opCode,string resourceCode)
		{
#if DEBUG
			Log.Info(
				new SQLCondition(
				string.Format(
				@"select {0} from TBLTSERRORCODE,TBLTS where TBLTSERRORCODE.TSID = TBLTS.TSID and TBLTSERRORCODE.rcard = '{1}'",
				"count(ecode)",
				sn.ToUpper())).SQLText);
#endif
			return this.DataProvider.GetCount(
				new SQLCondition(
				string.Format(
				@"select {0} from TBLTSERRORCODE,TBLTS where TBLTSERRORCODE.TSID = TBLTS.TSID and TBLTSERRORCODE.rcard = '{1}'",
				"count(ecode)",
				sn.ToUpper())));
		}
	}
}
