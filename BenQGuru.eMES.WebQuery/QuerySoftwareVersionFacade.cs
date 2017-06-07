using System;
using System.Collections;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Common.Helper;  
using BenQGuru.eMES.Common.DomainDataProvider;   
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.DataCollect;

using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QuerySoftwareVersionFacade 的摘要说明。
	/// </summary>
	public class QuerySoftwareVersionFacade
	{
		private  IDomainDataProvider _domainDataProvider= null;

		public QuerySoftwareVersionFacade( IDomainDataProvider domainDataProvider )
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

		#region 最新修改

		public object[] QuerySoftwareVersion(
			string itemCodes,string moCodes,
			string startSn,string endSn,
			int startDate,int endDate,
			string compareResult,
			string softwareName,string softwareVersion,
			int inclusive,int exclusive)
		{
			if(compareResult == SoftCompareStatus.Success)
			{
				#region 比对成功的信息 TBLONWIPSOFTVER 

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

				string SnCondition = string.Empty;
				CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
				if( string.Compare( startSn,endSn,true )==0  && startSn!=string.Empty  )
				{
					ArrayList array = new ArrayList();
					array.Add(startSn);
					castDownHelper.GetAllRCard(ref array,startSn);

					string[] rCards = (string[])array.ToArray(typeof(System.String));
				
					SnCondition = string.Format(" and rcard in ({0}) ",FormatHelper.ProcessQueryValues(rCards));
				}
				else if( string.Compare( startSn,endSn,true )!=0 )
				{
					ArrayList array = new ArrayList();
					castDownHelper.BuildProcessRcardCondition(ref array,startSn.ToUpper(),endSn.ToUpper());
					string rcardCondition = string.Empty ;
					if(array.Count>0)
					{
						for(int i=0; i<array.Count; i++)
						{
							if(i<array.Count-1)
							{
								rcardCondition += array[i].ToString() + " union ";
							}
							else
							{
								rcardCondition += array[i].ToString();
							}
						}
					}
					SnCondition = "and ( 1=1 " + FormatHelper.GetRCardRangeSql("rcard",startSn.ToUpper(),endSn.ToUpper());

					if( rcardCondition!=string.Empty )
					{
						SnCondition += string.Format(" or rcard in ({0}) ",rcardCondition);
					}
					SnCondition += " ) ";
				}

				string nameCondition = "";
				if( softwareName != "" && softwareName != null )
				{
					nameCondition = string.Format( @" and SOFTNAME like '{0}%'",softwareName);
				}

				string versionCondition = "";
				if( softwareVersion != "" && softwareVersion != null )
				{
					versionCondition = string.Format(@" and SOFTVER like '{0}%'",softwareVersion);
				}

				string sql = string.Format(@"select distinct {0} from TBLONWIPSOFTVER where 1=1 {1}{2}{3}{4}{5}",
					DomainObjectUtility.GetDomainObjectFieldsString(typeof(OnWIPSoftVersion)),
					itemCondition,moCondition,SnCondition,
					nameCondition,versionCondition);

#if DEBUG
				Log.Info(
					new PagerCondition(sql,"mocode,itemcode",inclusive,exclusive,true).SQLText);
#endif

				return this.DataProvider.CustomQuery(
					typeof(OnWIPSoftVersion),
					new PagerCondition(sql,"mocode,itemcode",inclusive,exclusive,true
					));

				#endregion
			}
			else if(compareResult == SoftCompareStatus.Failed)
			{
				#region 比对失败的信息 TBLVERSIONERROR


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
						@" and TBLVERSIONERROR.mocode in ({0})",FormatHelper.ProcessQueryValues( moCodes ) );
				}

				string SnCondition = string.Empty;
				CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
				if( string.Compare( startSn,endSn,true )==0  && startSn!=string.Empty  )
				{
					ArrayList array = new ArrayList();
					array.Add(startSn);
					castDownHelper.GetAllRCard(ref array,startSn);

					string[] rCards = (string[])array.ToArray(typeof(System.String));
				
					SnCondition = string.Format(" and TBLVERSIONERROR.rcard in ({0}) ",FormatHelper.ProcessQueryValues(rCards));
				}
				else if( string.Compare( startSn,endSn,true )!=0 )
				{
					ArrayList array = new ArrayList();
					castDownHelper.BuildProcessRcardCondition(ref array,startSn.ToUpper(),endSn.ToUpper());
					string rcardCondition = string.Empty ;
					if(array.Count>0)
					{
						for(int i=0; i<array.Count; i++)
						{
							if(i<array.Count-1)
							{
								rcardCondition += array[i].ToString() + " union ";
							}
							else
							{
								rcardCondition += array[i].ToString();
							}
						}
					}
					SnCondition = "and ( 1=1 " + FormatHelper.GetRCardRangeSql("TBLVERSIONERROR.rcard",startSn.ToUpper(),endSn.ToUpper());

					if( rcardCondition!=string.Empty )
					{
						SnCondition += string.Format(" or TBLVERSIONERROR.rcard in ({0}) ",rcardCondition);
					}
					SnCondition += " ) ";
				}

				string nameCondition = "";
				if( softwareName != "" && softwareName != null )
				{
					nameCondition = string.Format( @" and SOFTNAME like '{0}%'",softwareName);
				}

				string versionCondition = "";
				if( softwareVersion != "" && softwareVersion != null )
				{
					versionCondition = string.Format(@" and MOVERSIONINFO like '{0}%'",softwareVersion);
				}

				string dateCondition = "";
				if( startDate != 0 && endDate != 0 )
				{
					dateCondition = FormatHelper.GetDateRangeSql("TBLVERSIONERROR.mdate",startDate,endDate);
				}

				string sql = string.Format(@"select distinct TBLVERSIONERROR.* ,tblonwipsoftver.RCARDSEQ,SOFTNAME,TPCODE,SHIFTCODE,SHIFTTYPECODE,RESCODE,OPCODE,SSCODE,SEGCODE,ROUTECODE,MODELCODE,ITEMCODE
							 from TBLVERSIONERROR left join tblonwipsoftver on (TBLVERSIONERROR.rcard = tblonwipsoftver.rcard)
							 where 1=1 {0}{1}{2}{3}{4}{5}",
					itemCondition,moCondition,
					SnCondition,dateCondition,
					nameCondition,versionCondition);

#if DEBUG
				Log.Info(
					new PagerCondition(sql,"mocode,itemcode",inclusive,exclusive,true).SQLText);
#endif

				return this.DataProvider.CustomQuery(
					typeof(OnWIPSoftVersionError),
					new PagerCondition(sql,"mocode,itemcode",inclusive,exclusive,true
					));

				#endregion
			}

			return null;
		}

		public int QuerySoftwareVersionCount(
			string itemCodes,string moCodes,
			string startSn,string endSn,
			int startDate,int endDate,
			string compareResult,
			string softwareName,string softwareVersion)
		{
			if(compareResult == SoftCompareStatus.Success)
			{
				#region 比对成功的信息 TBLONWIPSOFTVER 

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

				string SnCondition = string.Empty;
				CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
				if( string.Compare( startSn,endSn,true )==0  && startSn!=string.Empty  )
				{
					ArrayList array = new ArrayList();
					array.Add(startSn);
					castDownHelper.GetAllRCard(ref array,startSn);

					string[] rCards = (string[])array.ToArray(typeof(System.String));
				
					SnCondition = string.Format(" and rcard in ({0}) ",FormatHelper.ProcessQueryValues(rCards));
				}
				else if( string.Compare( startSn,endSn,true )!=0 )
				{
					ArrayList array = new ArrayList();
					castDownHelper.BuildProcessRcardCondition(ref array,startSn.ToUpper(),endSn.ToUpper());
					string rcardCondition = string.Empty ;
					if(array.Count>0)
					{
						for(int i=0; i<array.Count; i++)
						{
							if(i<array.Count-1)
							{
								rcardCondition += array[i].ToString() + " union ";
							}
							else
							{
								rcardCondition += array[i].ToString();
							}
						}
					}
					SnCondition = "and ( 1=1 " + FormatHelper.GetRCardRangeSql("rcard",startSn.ToUpper(),endSn.ToUpper());

					if( rcardCondition!=string.Empty )
					{
						SnCondition += string.Format(" or rcard in ({0}) ",rcardCondition);
					}
					SnCondition += " ) ";
				}

				string nameCondition = "";
				if( softwareName != "" && softwareName != null )
				{
					nameCondition = string.Format( @" and SOFTNAME like '{0}%'",softwareName);
				}

				string versionCondition = "";
				if( softwareVersion != "" && softwareVersion != null )
				{
					versionCondition = string.Format(@" and SOFTVER like '{0}%'",softwareVersion);
				}

				string sql = string.Format(@"select {0} from TBLONWIPSOFTVER where 1=1 {1}{2}{3}{4}{5}",
					"count(mocode)",
					itemCondition,moCondition,SnCondition,
					nameCondition,versionCondition);

#if DEBUG
				Log.Info(
					new SQLCondition(sql).SQLText);
#endif
				return this.DataProvider.GetCount(
					new SQLCondition(sql));

				#endregion
			}
			else if(compareResult == SoftCompareStatus.Failed)
			{
				#region 比对失败的信息 TBLVERSIONERROR

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
						@" and TBLVERSIONERROR.mocode in ({0})",FormatHelper.ProcessQueryValues( moCodes ) );
				}

				string SnCondition = string.Empty;
				CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
				if( string.Compare( startSn,endSn,true )==0  && startSn!=string.Empty  )
				{
					ArrayList array = new ArrayList();
					array.Add(startSn);
					castDownHelper.GetAllRCard(ref array,startSn);

					string[] rCards = (string[])array.ToArray(typeof(System.String));
				
					SnCondition = string.Format(" and TBLVERSIONERROR.rcard in ({0}) ",FormatHelper.ProcessQueryValues(rCards));
				}
				else if( string.Compare( startSn,endSn,true )!=0 )
				{
					ArrayList array = new ArrayList();
					castDownHelper.BuildProcessRcardCondition(ref array,startSn.ToUpper(),endSn.ToUpper());
					string rcardCondition = string.Empty ;
					if(array.Count>0)
					{
						for(int i=0; i<array.Count; i++)
						{
							if(i<array.Count-1)
							{
								rcardCondition += array[i].ToString() + " union ";
							}
							else
							{
								rcardCondition += array[i].ToString();
							}
						}
					}
					SnCondition = "and ( 1=1 " + FormatHelper.GetRCardRangeSql("TBLVERSIONERROR.rcard",startSn.ToUpper(),endSn.ToUpper());

					if( rcardCondition!=string.Empty )
					{
						SnCondition += string.Format(" or TBLVERSIONERROR.rcard in ({0}) ",rcardCondition);
					}
					SnCondition += " ) ";
				}

				string nameCondition = "";
				if( softwareName != "" && softwareName != null )
				{
					nameCondition = string.Format( @" and SOFTNAME like '{0}%'",softwareName);
				}

				string versionCondition = "";
				if( softwareVersion != "" && softwareVersion != null )
				{
					versionCondition = string.Format(@" and MOVERSIONINFO like '{0}%'",softwareVersion);
				}

				string dateCondition = "";
				if( startDate != 0 && endDate != 0 )
				{
					dateCondition = FormatHelper.GetDateRangeSql("TBLVERSIONERROR.mdate",startDate,endDate);
				}

				string sql = string.Format(@"select  count(PKID)
							 from TBLVERSIONERROR left join tblonwipsoftver on (TBLVERSIONERROR.rcard = tblonwipsoftver.rcard)
							 where 1=1 {1}{2}{3}{4}",
					itemCondition,moCondition,
					SnCondition,dateCondition,
					nameCondition,versionCondition);

#if DEBUG
				Log.Info(sql);
#endif

				return this.DataProvider.GetCount(new SQLCondition(sql));


				#endregion
			}

			return 0;
		}

		#endregion

		public object[] QuerySoftwareVersion(
			string itemCodes,string moCodes,
			string startSn,string endSn,
			string softwareName,string softwareVersion,
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

			string SnCondition = string.Empty;
			CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
			if( string.Compare( startSn,endSn,true )==0  && startSn!=string.Empty  )
			{
				ArrayList array = new ArrayList();
				array.Add(startSn);
				castDownHelper.GetAllRCard(ref array,startSn);

				string[] rCards = (string[])array.ToArray(typeof(System.String));
				
				SnCondition = string.Format(" and rcard in ({0}) ",FormatHelper.ProcessQueryValues(rCards));
			}
			else if( string.Compare( startSn,endSn,true )!=0 )
			{
				ArrayList array = new ArrayList();
				castDownHelper.BuildProcessRcardCondition(ref array,startSn.ToUpper(),endSn.ToUpper());
				string rcardCondition = string.Empty ;
				if(array.Count>0)
				{
					for(int i=0; i<array.Count; i++)
					{
						if(i<array.Count-1)
						{
							rcardCondition += array[i].ToString() + " union ";
						}
						else
						{
							rcardCondition += array[i].ToString();
						}
					}
				}
				SnCondition = "and ( 1=1 " + FormatHelper.GetRCardRangeSql("rcard",startSn.ToUpper(),endSn.ToUpper());

				if( rcardCondition!=string.Empty )
				{
					SnCondition += string.Format(" or rcard in ({0}) ",rcardCondition);
				}
				SnCondition += " ) ";
			}

			string nameCondition = "";
			if( softwareName != "" && softwareName != null )
			{
				nameCondition = string.Format( @" and SOFTNAME like '{0}%'",softwareName);
			}

			string versionCondition = "";
			if( softwareVersion != "" && softwareVersion != null )
			{
				versionCondition = string.Format(@" and SOFTVER like '{0}%'",softwareVersion);
			}

			string sql = string.Format(@"select distinct {0} from TBLONWIPSOFTVER where 1=1 {1}{2}{3}{4}{5}",
				DomainObjectUtility.GetDomainObjectFieldsString(typeof(OnWIPSoftVersion)),
				itemCondition,moCondition,SnCondition,
				nameCondition,versionCondition);

#if DEBUG
			Log.Info(
				new PagerCondition(sql,"mocode,itemcode",inclusive,exclusive,true).SQLText);
#endif

			return this.DataProvider.CustomQuery(
				typeof(OnWIPSoftVersion),
				new PagerCondition(sql,"mocode,itemcode",inclusive,exclusive,true
				));
		}

		public int QuerySoftwareVersionCount(
			string itemCodes,string moCodes,
			string startSn,string endSn,
			string softwareName,string softwareVersion)
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

			string SnCondition = string.Empty;
			CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
			if( string.Compare( startSn,endSn,true )==0  && startSn!=string.Empty  )
			{
				ArrayList array = new ArrayList();
				array.Add(startSn);
				castDownHelper.GetAllRCard(ref array,startSn);

				string[] rCards = (string[])array.ToArray(typeof(System.String));
				
				SnCondition = string.Format(" and rcard in ({0}) ",FormatHelper.ProcessQueryValues(rCards));
			}
			else if( string.Compare( startSn,endSn,true )!=0 )
			{
				ArrayList array = new ArrayList();
				castDownHelper.BuildProcessRcardCondition(ref array,startSn.ToUpper(),endSn.ToUpper());
				string rcardCondition = string.Empty ;
				if(array.Count>0)
				{
					for(int i=0; i<array.Count; i++)
					{
						if(i<array.Count-1)
						{
							rcardCondition += array[i].ToString() + " union ";
						}
						else
						{
							rcardCondition += array[i].ToString();
						}
					}
				}
				SnCondition = "and ( 1=1 " + FormatHelper.GetRCardRangeSql("rcard",startSn.ToUpper(),endSn.ToUpper());

				if( rcardCondition!=string.Empty )
				{
					SnCondition += string.Format(" or rcard in ({0}) ",rcardCondition);
				}
				SnCondition += " ) ";
			}

			string nameCondition = "";
			if( softwareName != "" && softwareName != null )
			{
				nameCondition = string.Format( @" and SOFTNAME like '{0}%'",softwareName);
			}

			string versionCondition = "";
			if( softwareVersion != "" && softwareVersion != null )
			{
				versionCondition = string.Format(@" and SOFTVER like '{0}%'",softwareVersion);
			}

			string sql = string.Format(@"select {0} from TBLONWIPSOFTVER where 1=1 {1}{2}{3}{4}{5}",
				"count(mocode)",
				itemCondition,moCondition,SnCondition,
				nameCondition,versionCondition);

#if DEBUG
			Log.Info(
				new SQLCondition(sql).SQLText);
#endif
			return this.DataProvider.GetCount(
				new SQLCondition(sql));
		}
	}
}
