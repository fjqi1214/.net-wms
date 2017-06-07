using System;
using System.Reflection;
using System.Collections;
using System.Runtime.Remoting;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.DataLink;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using UserControl;

namespace BenQGuru.eMES.DataLink
{
	/// <summary>
	/// Class1 的摘要说明。
	/// </summary>
	public class DataLinkFacade
	{
		private IDomainDataProvider _domainDataProvider = null;
		private FacadeHelper _helper					= null;

		public DataLinkFacade(IDomainDataProvider domainDataProvider)
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

		#region FT

		#region 基本操作(增删改,获取)

		public void AddFT(FT ft)
		{
			this._helper.AddDomainObject(ft);
		}

		private void DeleteFT(FT ft)
		{
			if(ft == null)
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Null_Paramter");
			}
			
			try
			{
				this._helper.DeleteDomainObject(ft);
			}
			catch(Exception ex)
			{
				ExceptionManager.Raise(this.GetType(),"$Error_DeleteFT",String.Format("[$FT='{0}']",ft.RCard),ex);
			}

		}

		public void UpdateFT(FT ft)
		{
			try
			{
				//Laws Lu,2006/11/13 uniform system collect date
				DBDateTime dbDateTime;
						
				dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

				ft.MaintainDate = dbDateTime.DBDate;
				ft.MaintainTime = dbDateTime.DBTime;
				this._helper.UpdateDomainObject(ft);
			}
			catch(Exception ex)
			{
				ExceptionManager.Raise(this.GetType(),"$Error_UpdateFT",String.Format("[$FT='{0}']",ft.RCard),ex);
			}
		}

		public object GetFT(string rcard,int tseq)
		{
			return this.DataProvider.CustomSearch(typeof(FT), new object[]{rcard,tseq});
		}

		#endregion

		private int getTSeq(string rcard)
		{
			string sql = string.Format("select * from tblft where rcard='{0}'",rcard);
			object[] fts = this.DataProvider.CustomQuery(typeof(FT),new SQLCondition(sql));

			int returnInt = 1;
			if(fts != null && fts.Length>0)
			{
				returnInt += fts.Length;
			}
			return returnInt;
		}

		//根据rcard获取FT当前的对应的产品代码
		private string getItemCode(string rcard)
		{
			/* modified by jessie lee, 2006/08/09, for Power0152 */
			string sql = 
				string.Format(
				@"select *
				    from (select *
						    from tblsimulation
						   where rcard = '{0}'
						   order by mdate * 1000000 + mtime desc)
				   where rownum = 1",rcard);
			object[] sims = this.DataProvider.CustomQuery(typeof(Simulation),new SQLCondition(sql));

			string itemCode = "ITEM001";
			if(sims != null && sims.Length>0)
			{
				itemCode = ((Simulation)sims[0]).ItemCode;
			}
			return itemCode;
		}
	
		#endregion

		#region FTDetail

		#region 基本操作(增删改,获取)

		public void AddFTDetail(FTDetail ftdetail)
		{
			this._helper.AddDomainObject(ftdetail);
		}

		public void AddFTDetail(FTDetail[] ftdetails)
		{
			foreach(FTDetail ftdetail in ftdetails)
			{
				this._helper.AddDomainObject(ftdetail);
			}
		}

		private void DeleteFTDetail(FTDetail ftdetail)
		{
			if(ftdetail == null)
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Null_Paramter");
			}
			
			try
			{
				this._helper.DeleteDomainObject(ftdetail);
			}
			catch(Exception ex)
			{
				ExceptionManager.Raise(this.GetType(),"$Error_DeleteFTDetail",String.Format("[$FTDetail='{0}']",ftdetail.RCard),ex);
			}

		}


		public void UpdateFTDetail(FTDetail ftdetail)
		{
			try
			{
				this._helper.UpdateDomainObject(ftdetail);
			}
			catch(Exception ex)
			{
				ExceptionManager.Raise(this.GetType(),"$Error_UpdateFTDetail",String.Format("[$FTDetail='{0}']",ftdetail.RCard),ex);
			}
		}

		public object[] QueryFTDetail(string ssCode,string itemCode,string moCode,string resCode,string beginSN,string endSN,int inclusive,int exclusive)
		{
			#region sql condition

			//产线   
			string ssCondition = string.Empty;
			if(ssCode != null && ssCode != string.Empty)
			{
				ssCondition = string.Format(" AND tblft.LINECODE IN  ({0}) ",FormatHelper.ProcessQueryValues( ssCode ));
			}

			//产品 
			string itemCodeCondition = string.Empty;
			if(itemCode != null && itemCode != string.Empty)
			{
				itemCodeCondition = string.Format(" AND tblft.ITEMCODE IN  ({0}) ",FormatHelper.ProcessQueryValues( itemCode ));
			}

			//资源  起始序列号 结束序列号 
			string resCodeCondition = string.Empty;
			if(resCode != null && resCode != string.Empty)
			{
				resCodeCondition = string.Format(" AND tblft.RESCODE IN  ({0}) ",FormatHelper.ProcessQueryValues( resCode ));
			}

			//工单  
			string moCodeCondition = string.Empty;
			if(moCode != null && moCode != string.Empty)
			{
				//moCodeCondition = string.Format(" AND MOCODE IN  ({0}) ",FormatHelper.ProcessQueryValues( moCode ));
			}

			string snCondition = string.Empty;
			if(beginSN != null && beginSN != string.Empty)
			{
				snCondition = FormatHelper.GetRCardRangeSql("tblft.rcard",beginSN,endSN);
			}

			string sqlCondition = ssCondition + itemCodeCondition + resCodeCondition + snCondition;

			#endregion

			string sql = string.Format("select tblftdetail.* from tblftdetail INNER JOIN tblft ON tblftdetail.RCARD= tblft.RCARD AND tblftdetail.TestSeq = tblft.TestSeq where 1=1 {0} order by  tblftdetail.rcard",sqlCondition);
			object[] returnObjs = this.DataProvider.CustomQuery(typeof(FTDetail),new PagerCondition(sql,inclusive,exclusive));
			
			if(returnObjs == null)
			{
				return new object[]{};
			}

			return returnObjs;
		}

		public object GetFTDetail(string rcard,int tseq,int tgroup)
		{
			return this.DataProvider.CustomSearch(typeof(FT), new object[]{rcard,tseq,tgroup});
		}

		#endregion

		public object[] QueryFTDetail(string rcard)
		{
			string sql = string.Format("select * from tblftdetail");
			object[] returnObjs = this.DataProvider.CustomQuery(typeof(FTDetail),new SQLCondition(sql));
			return returnObjs;
		}

		public object[] QueryFTDetail(string rcard,int tseq)
		{
			string sql = string.Format("select * from tblftdetail");
			object[] returnObjs = this.DataProvider.CustomQuery(typeof(FTDetail),new SQLCondition(sql));
			return returnObjs;
		}
	
		#endregion

	}
}
