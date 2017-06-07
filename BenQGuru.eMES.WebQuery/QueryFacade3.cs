using System;
using System.Collections;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Common.Helper;  
using BenQGuru.eMES.Common.DomainDataProvider;   
using BenQGuru.eMES.Domain.DeviceInterface;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QueryFacade3 的摘要说明。
	/// </summary>
	public class QueryFacade3
	{
		private  IDomainDataProvider _domainDataProvider= null;

		public QueryFacade3( IDomainDataProvider domainDataProvider )
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
		
		#region On Wip 
		/// <summary>
		/// Added by Jessie Lee For P4.4 AM0137, 2005/8/30
		/// </summary>
		/// <param name="itemCode">多选,精确查询</param>
		/// <param name="moCode">多选,精确查询</param>
		/// <param name="factoryCode">单选,精确查询</param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="inclusive"></param>
		/// <param name="exclusive"></param>
		/// <returns>OnWipInfoOnOperation数组</returns>
		public object[] QueryOnWipInfoOnOperation( string itemCode, string moCode,int startDate,int endDate, int inclusive,int exclusive)
		{
			string itemCondition = String.Empty;

			if(itemCode.Trim() != String.Empty)
			{
				itemCondition += string.Format(" and ITEMCODE in ( {0} ) ",FormatHelper.ProcessQueryValues(itemCode) ) ;
			}

			string moCondition = " and MOCODE in( select MOCODE from TBLMO where ";

			moCondition += string.Format(" MOSTATUS in ('{0}', '{1}') ", 
				BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_OPEN,
				BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_PENDING );

            moCondition += GlobalVariables.CurrentOrganizations.GetSQLCondition();

			if ( moCode.Trim() != string.Empty )
			{
				moCondition += string.Format(" and MOCODE in ({0}))", FormatHelper.ProcessQueryValues(moCode) );
			}
			else
			{
				moCondition += ")";
			}
			moCondition += FormatHelper.GetDateRangeSql("SHIFTDAY",startDate,endDate);

			//added by jessie lee for AM0220,2005/10/11
			//修改对在制品查询的定义
			//非最后工序：工序的良品数量＋工序的尚未被送修确认的不良品数量＝工序在制数量
			//最后工序：工序的尚未被送修确认的不良品数量＝工序在制数量(该逻辑不再使用)
			//modified by jessie for AM0224,2005/10/18,P4.11
			string rcardCondition = 
				//QA岗位在制数量（FQC）＝在检已确认样本数＋QA判退数中尚未被在线返工的数量
				string.Format(
				@"and rcard in
						   (select rcard
							from tbllot2cardcheck
							where tbllot2cardcheck.rcard = tblsimulationreport.rcard
							and tbllot2cardcheck.rcardseq = tblsimulationreport.rcardseq
							and tbllot2cardcheck.mocode = tblsimulationreport.mocode
							union all
							select rcard
							from tblreject
							where tblreject.rcard = tblsimulationreport.rcard
							and tblreject.rcardseq = tblsimulationreport.rcardseq
							and tblreject.mocode = tblsimulationreport.mocode
							union all
							select rcard
							from tblsimulationreport
							where instr(laction,'OQC')=0 or instr(laction,'OQCLOT')=1 ) "+
				//排除掉最后一道工序的良品，逻辑：状态为Good,IsCom（是否完成标志位）为1
				@"and iscom = {1} "+
				//进入维修的不良品不再为在制品
				@"and rcard not in
					(select a.rcard
					from tblts a
					where a.rcard = tblsimulationreport.rcard 
						and a.rcardseq = tblsimulationreport.rcardseq
						and a.mocode = tblsimulationreport.mocode
						and tsstatus <> '{2}')",ProductStatus.GOOD,ProductComplete.NoComplete,TSStatus.TSStatus_New);

			string sql =  string.Format("select OPCODE, sum (IDMERGERULE) as QTY from TBLSIMULATIONREPORT where 1=1 {0} {1} {2} group by OPCODE",
				itemCondition, moCondition,rcardCondition); //modified by jessie lee for AM0220,2005/10/11,添加条件

			//added by jessie lee
			//添加“维修工序”
			/* modified by jessie lee, 2005/12/26, 优化在制品查询速度－－维修工序 */
			#region 优化前的逻辑，保留用于以后核对
			/*
			string tsRcardCondition = 
				string.Format(
					@"and rcard in
					(select a.rcard
					from tblts a
					where a.rcard = tblsimulationreport.rcard 
						and a.rcardseq = tblsimulationreport.rcardseq
						and a.mocode = tblsimulationreport.mocode	
						and tsstatus in ('{0}','{1}','{2}') ) ",
				TSStatus.TSStatus_TS,
				TSStatus.TSStatus_Confirm,
				TSStatus.TSStatus_Reflow);
			string tsSql = string.Format(
				@"select 'TS' as OPCODE,sum (IDMERGERULE) as QTY from TBLSIMULATIONREPORT where 1=1 {0} {1} {2} ",
				itemCondition, moCondition,tsRcardCondition);
			*/
			#endregion
			
			string tsSql = string.Format(
				@"select sum(IDMERGERULE) as QTY
				from TBLSIMULATIONREPORT
				where 1=1 {0}
					and (rcard, rcardseq, mocode) in
					(select rcard, rcardseq, mocode
						from tblts 
						where tsstatus in
							('{2}', '{3}', '{4}')
						 {1} )",
				itemCondition,moCondition,
				TSStatus.TSStatus_TS,
				TSStatus.TSStatus_Confirm,
				TSStatus.TSStatus_Reflow);
			
			//无论大小板,都以分板比例作为在制统计数量,(大板分板比例大于1,小板等于1)
			/*
			return this.DataProvider.CustomQuery( 
				typeof(OnWipInfoOnOperation),
				new PagerCondition(sql + " union " + tsSql,
				"OPCODE", inclusive, exclusive, true ));
			*/
            
			if( inclusive==1 )
			{
				object[] tsSims = this.DataProvider.CustomQuery( typeof(OnWipInfoOnOperation),new SQLCondition(tsSql) );
				/*	Removed by Icyer 2006/12/25 @ YHI
				object[] Sims = this.DataProvider.CustomQuery( 
					typeof(OnWipInfoOnOperation),
					new PagerCondition(sql,
					"OPCODE", inclusive, exclusive-1, true ));
				*/
				sql += " ORDER BY OPCODE ";
				object[] Sims = this.DataProvider.CustomQuery( 
					typeof(OnWipInfoOnOperation),
					new SQLCondition(sql));

				ArrayList array = new ArrayList();
				if( (tsSims[0] as OnWipInfoOnOperation).OnWipQuantityOnOperation > 0)
				{
					 (tsSims[0] as OnWipInfoOnOperation).OperationCode = "TS" ;
					array.Add(tsSims[0]);
					
				}
				if( Sims!=null )
				{
					for( int i=0; i<Sims.Length; i++ )
					{
						array.Add(Sims[i]);
					}

					if( array.Count == 0 )
					{
						return null;
					}
				}
				else
				{
					if( array.Count == 0 )
					{
						return null;
					}
				}

				return (object[])array.ToArray(typeof(System.Object));
			}
			else
			{
				/*	Removed by Icyer 2006/12/25 @ YHI
				object[] Sims = this.DataProvider.CustomQuery( 
					typeof(OnWipInfoOnOperation),
					new PagerCondition(sql,
					"OPCODE", inclusive-1, exclusive-1, true ));
				*/
				sql += " ORDER BY OPCODE ";
				object[] Sims = this.DataProvider.CustomQuery( 
					typeof(OnWipInfoOnOperation),
					new SQLCondition(sql));

				return Sims;
			}		
		}

		/// <summary>
		/// ** 功能描述:	在制品查询的总数量
		///						按工序统计在制品数量
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-06-23
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="itemCode">单选,精确查询</param>
		/// <param name="moCode">多选,精确查询，以','或';'分割的字符串</param>
		/// <param name="inclusive"></param>
		/// <param name="exclusive"></param>
		/// <returns></returns>
		public int QueryOnWipInfoOnOperationCount( string itemCode, string moCode ,int startDate,int endDate)
		{
			string itemCondition = String.Empty;
			if(itemCode.Trim() != String.Empty)
			{
				itemCondition += string.Format(" and ITEMCODE in ( {0} ) ",FormatHelper.ProcessQueryValues(itemCode) ) ;
			}

			string moCondition = string.Format("and MOCODE in( select MOCODE from TBLMO where MOSTATUS in ('{0}', '{1}') ", 
				BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_OPEN,
				BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_PENDING );

            moCondition += GlobalVariables.CurrentOrganizations.GetSQLCondition();

			if ( moCode.Trim() != string.Empty )
			{
				moCondition += string.Format(" and MOCODE in ({0}))", FormatHelper.ProcessQueryValues(moCode) );
			}
			else
			{
				moCondition += ")";
			}
			moCondition += FormatHelper.GetDateRangeSql("SHIFTDAY",startDate,endDate);

			//added by jessie lee for AM0220,2005/10/11
			//修改对在制品查询的定义
			//非最后工序：工序的良品数量＋工序的尚未被送修确认的不良品数量＝工序在制数量
			//最后工序：工序的尚未被送修确认的不良品数量＝工序在制数量(该逻辑不再使用)
			//modified by jessie for AM0224,2005/10/18,P4.11
			string rcardCondition = 
				//排除掉最后一道工序的良品，逻辑：状态为Good,IsCom（是否完成标志位）为1(该逻辑不再使用)
				//QA岗位在制数量（FQC）＝在检已确认样本数＋QA判退数中尚未被在线返工的数量
				string.Format(
				@"and rcard in
						   (select rcard
							from tbllot2cardcheck
							where tbllot2cardcheck.rcard = tblsimulationreport.rcard
							and tbllot2cardcheck.rcardseq = tblsimulationreport.rcardseq
							and tbllot2cardcheck.mocode = tblsimulationreport.mocode
							union all
							select rcard
							from tblreject
							where tblreject.rcard = tblsimulationreport.rcard
							and tblreject.rcardseq = tblsimulationreport.rcardseq
							and tblreject.mocode = tblsimulationreport.mocode
							union all
							select rcard
							from tblsimulationreport
							where instr(laction,'OQC')=0 or instr(laction,'OQCLOT')=1 ) "+
				//排除掉最后一道工序的良品，逻辑：状态为Good,IsCom（是否完成标志位）为1
				@"and iscom = {1} "+
				//进入维修的不良品不再为在制品
				@"and rcard not in
					(select a.rcard
					from tblts a
					where a.rcard = tblsimulationreport.rcard 
						and a.rcardseq = tblsimulationreport.rcardseq
						and a.mocode = tblsimulationreport.mocode
						and tsstatus <> '{2}')",ProductStatus.GOOD,ProductComplete.NoComplete,TSStatus.TSStatus_New);	
		
			//added by jessie lee
			//添加“维修工序”
			#region 优化前的逻辑，保留用于以后核对
			/*
			string tsRcardCondition = 
				string.Format(
				@"and rcard in
					(select a.rcard
					from tblts a
					where a.rcard = tblsimulationreport.rcard 
						and a.rcardseq = tblsimulationreport.rcardseq
						and a.mocode = tblsimulationreport.mocode
						and tsstatus in ('{0}','{1}','{2}') ) ",
				TSStatus.TSStatus_TS,
				TSStatus.TSStatus_Confirm,
				TSStatus.TSStatus_Reflow);
			string tsSql = string.Format(
				@"select 'TS' as OPCODE,sum (IDMERGERULE) as QTY from TBLSIMULATIONREPORT where 1=1 {0} {1} {2} ",
				itemCondition, moCondition,tsRcardCondition);
			*/
			#endregion

			string tsSql = string.Format(
				@"select IDMERGERULE
				from TBLSIMULATIONREPORT
				where 1=1 {0}
					and (rcard, rcardseq, mocode) in
					(select rcard, rcardseq, mocode
						from tblts 
						where tsstatus in
							('{2}', '{3}', '{4}')
						 {1} )",
				itemCondition,moCondition,
				TSStatus.TSStatus_TS,
				TSStatus.TSStatus_Confirm,
				TSStatus.TSStatus_Reflow);

			int tsCount = this.DataProvider.GetCount( new SQLCondition( string.Format(" select count( * ) from ( {0} ) ", tsSql) ));

			int count = this.DataProvider.GetCount( 
				new SQLCondition( string.Format("select count(*) from (select distinct OPCODE from TBLSIMULATIONREPORT where 1=1 {0} {1} {2} )",
				itemCondition, moCondition,rcardCondition)) );

			if( tsCount>0 )
			{
				return count+1;
			}

			return count;
		 
		}

#endregion		

		#region 在制品分布 
		/// <summary>
		/// ** 功能描述:	在制品分布，
		///						按资源统计在制品数量
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-06-23
		/// ** 修 改:		Jessie Lee
		/// ** 日 期:       2005-09-07
		/// </summary>
		/// <param name="itemCode">单选,精确查询</param>
		/// <param name="moCode">多选,精确查询，以','或';'分割的字符串</param>
		/// <param name="operationCode">工序,单选,精确查询</param>
		/// <param name="inclusive"></param>
		/// <param name="exclusive"></param>
		/// <returns>OnWipInfoOnResource数组</returns>
		public object[] QueryOnWipInfoOnResource( string itemCode, string moCode, string operationCode,int startDate,int endDate, int inclusive, int exclusive)
		{
			string itemCondition = string.Empty;
			if(itemCode.Trim() != string.Empty)
			{
				itemCondition = string.Format(" and ITEMCODE in ( {0} ) ",FormatHelper.ProcessQueryValues(itemCode) );
			}

			string moCondition = string.Format("and MOCODE in( select MOCODE from TBLMO where MOSTATUS in ('{0}', '{1}') ", 
				BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_OPEN,
				BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_PENDING );

            moCondition += GlobalVariables.CurrentOrganizations.GetSQLCondition();

			if ( moCode.Trim() != string.Empty )
			{
				moCondition += string.Format(" and MOCODE in ({0}))", FormatHelper.ProcessQueryValues(moCode) );
			}
			else
			{
				moCondition += ")";
			}

			moCondition += FormatHelper.GetDateRangeSql("SHIFTDAY",startDate,endDate);

			/* modified by jessie lee, 2005/12/8
			 * 如果待查询的工序是 TS
			 * */
			string rcardCondition = string.Empty ;
			string querySQL = string.Empty ;
			if( string.Compare( operationCode,"TS",true )==0 )
			{
				//added by jessie lee
				//添加“维修工序”
				if(itemCode.Trim() != string.Empty)
				{
					itemCondition = string.Format(" and a.ITEMCODE in ( {0} ) ",FormatHelper.ProcessQueryValues(itemCode) );
				}

				moCondition = string.Format("and a.MOCODE in( select MOCODE from TBLMO where MOSTATUS in ('{0}', '{1}') ", 
					BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_OPEN,
					BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_PENDING );

                moCondition += GlobalVariables.CurrentOrganizations.GetSQLCondition();

				if ( moCode.Trim() != string.Empty )
				{
					moCondition += string.Format(" and MOCODE in ({0}))", FormatHelper.ProcessQueryValues(moCode) );
				}
				else
				{
					moCondition += ")";
				}

				moCondition += FormatHelper.GetDateRangeSql("a.SHIFTDAY",startDate,endDate);

				querySQL = string.Format(
					@"select '' as SEGCODE,
							a.SHIFTDAY,
							a.SHIFTCODE,
							'' as SSCODE,
							a.CRESCODE as RESCODE,
							a.MOCODE,
							sum( decode( a.tsstatus,'{0}',b.IDMERGERULE,0 )) as TSConfirmQty,
							sum( decode( a.tsstatus,'{1}',b.IDMERGERULE,0 )) as TSQty,
							sum( decode( a.tsstatus,'{2}',b.IDMERGERULE,0 )) as TSReflowQty
						from TBLSIMULATIONREPORT b, tblts a
						where a.rcard = b.rcard
						and a.rcardseq = b.rcardseq
						and a.mocode = b.mocode
						{3} {4}
						and b.rcard in (select c.rcard
											from tblts c
											where c.rcard = b.rcard
											and c.rcardseq = b.rcardseq
											and c.mocode = b.mocode
											and c.tsstatus in ('{0}', '{1}',
												'{2}'))              
						group by a.SHIFTDAY, a.SHIFTCODE, a.CRESCODE, a.MOCODE",
					TSStatus.TSStatus_Confirm,
					TSStatus.TSStatus_TS,
					TSStatus.TSStatus_Reflow,
					itemCondition, 
					moCondition);
			}
			else
			{
				//added by jessie lee for AM0220,2005/10/11
				//修改对在制品查询的定义
				//非最后工序：工序的良品数量＋工序的尚未被送修确认的不良品数量＝工序在制数量
				//最后工序：工序的尚未被送修确认的不良品数量＝工序在制数量(该逻辑不再使用)
				//modified by jessie for AM0224,2005/10/18,P4.11
				rcardCondition = 
					//排除掉最后一道工序的良品，逻辑：状态为Good,IsCom（是否完成标志位）为1(该逻辑不再使用)
					//QA岗位在制数量（FQC）＝在检已确认样本数＋QA判退数中尚未被在线返工的数量
					string.Format(
					@"and rcard in
						   (select rcard
							from tbllot2cardcheck
							where tbllot2cardcheck.rcard = tblsimulationreport.rcard
							and tbllot2cardcheck.rcardseq = tblsimulationreport.rcardseq
							and tbllot2cardcheck.mocode = tblsimulationreport.mocode
							union all
							select rcard
							from tblreject
							where tblreject.rcard = tblsimulationreport.rcard
							and tblreject.rcardseq = tblsimulationreport.rcardseq
							and tblreject.mocode = tblsimulationreport.mocode
							union all
							select rcard
							from tblsimulationreport
							where instr(laction,'OQC')=0 or instr(laction,'OQCLOT')=1 ) "+
					//排除掉最后一道工序的良品，逻辑：状态为Good,IsCom（是否完成标志位）为1
					@"and iscom = {1} "+
					//进入维修的不良品不再为在制品
					@"and rcard not in
					(select a.rcard
					from tblts a
					where a.rcard = tblsimulationreport.rcard 
						and a.rcardseq = tblsimulationreport.rcardseq
						and a.mocode = tblsimulationreport.mocode
						and tsstatus <> '{2}')",ProductStatus.GOOD,ProductComplete.NoComplete,TSStatus.TSStatus_New);

				//modified by jessie lee for AM0220,2005/10/11
				//在制良品数量＝该工序下的某资源某工单的良品数量（最后工序的资源，在制良品数量＝0）
				//在制不良品数量＝该工序下的某资源某工单的尚未被送修确认的不良品数量
				querySQL = string.Format(@"select SEGCODE,SHIFTDAY,SHIFTCODE, SSCODE, RESCODE, MOCODE, 
				sum(decode(status, '{4}', 0,'{5}' ,0,IDMERGERULE)) as GOODQTY,
				sum(decode(status, '{4}', IDMERGERULE, 0)) as NGQTY,
				sum(decode(status, '{5}', IDMERGERULE, 0)) as NGForReworksQTY 
				from TBLSIMULATIONREPORT where 1=1 {0} and OPCODE='{1}' {2} {3} group by SEGCODE,SHIFTDAY,SHIFTCODE, SSCODE, RESCODE, MOCODE",
					itemCondition, operationCode, moCondition,rcardCondition,ProductStatus.NG,ProductStatus.Reject);
			}

			return this.DataProvider.CustomQuery( 
				typeof(OnWipInfoOnResource),
				new PagerCondition( querySQL , 
				inclusive, exclusive, true ));
		}

		/// <summary>
		/// ** 功能描述:	在制品分布，
		///						按资源统计在制品数量
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-06-23
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="itemCode">单选,精确查询</param>
		/// <param name="moCode">多选,精确查询，以','或';'分割的字符串</param>
		/// <param name="operationCode">工序,单选,精确查询</param>
		/// <param name="inclusive"></param>
		/// <param name="exclusive"></param>
		/// <returns></returns>
		public int QueryOnWipInfoOnResourceCount( string itemCode,string moCode,string operationCode ,int startDate,int endDate)
		{
			string itemCondition = string.Empty;
			string moCondition = string.Empty;
			string querySQL = string.Empty ;
			/* modified by jessie lee, 2005/12/8
			 * 如果待查询的工序是 TS
			 * */
			if( string.Compare( operationCode,"TS",true )==0 )
			{
				//added by jessie lee
				//添加“维修工序”
				if(itemCode.Trim() != string.Empty)
				{
					itemCondition = string.Format(" and ITEMCODE in ( {0} ) ",FormatHelper.ProcessQueryValues(itemCode) );
				}

				moCondition = string.Format("and MOCODE in( select MOCODE from TBLMO where MOSTATUS in ('{0}', '{1}') ", 
					BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_OPEN,
					BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_PENDING );

                moCondition += GlobalVariables.CurrentOrganizations.GetSQLCondition();

				if ( moCode.Trim() != string.Empty )
				{
					moCondition += string.Format(" and MOCODE in ({0}))", FormatHelper.ProcessQueryValues(moCode) );
				}
				else
				{
					moCondition += ")";
				}

				moCondition += FormatHelper.GetDateRangeSql("SHIFTDAY",startDate,endDate);
				string dayCondition = FormatHelper.GetDateRangeSql("SHIFTDAY",startDate,endDate);

				querySQL = string.Format(
					@"select distinct SHIFTDAY, SHIFTCODE, CRESCODE, MOCODE
					from tblts
					where tsstatus in ('{0}', '{1}', '{2}') {3}
					and (rcard, rcardseq, mocode) in
						(select rcard, rcardseq,mocode
							from tblsimulationreport
							where 1=1 {4} {5})",
					TSStatus.TSStatus_Confirm,
					TSStatus.TSStatus_TS,
					TSStatus.TSStatus_Reflow,
					dayCondition,
					itemCondition, 
					moCondition);
				querySQL = string.Format( " select count(*) from ( {0} ) ",querySQL );
			}
			else
			{
				if(itemCode.Trim() != string.Empty)
				{
					itemCondition = string.Format(" and ITEMCODE in ( {0} ) ",FormatHelper.ProcessQueryValues(itemCode) );
				}

				moCondition = string.Format("and MOCODE in( select MOCODE from TBLMO where MOSTATUS in ('{0}', '{1}') ", 
					BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_OPEN,
					BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_PENDING );

                moCondition += GlobalVariables.CurrentOrganizations.GetSQLCondition();

				if ( moCode.Trim() != string.Empty )
				{
					moCondition += string.Format(" and MOCODE in ({0}))", FormatHelper.ProcessQueryValues(moCode) );
				}
				else
				{
					moCondition += ")";
				}
				moCondition += FormatHelper.GetDateRangeSql("SHIFTDAY",startDate,endDate);
				//added by jessie lee for AM0220,2005/10/11
				//修改对在制品查询的定义
				//非最后工序：工序的良品数量＋工序的尚未被送修确认的不良品数量＝工序在制数量
				//最后工序：工序的尚未被送修确认的不良品数量＝工序在制数量(该逻辑不再使用)
				//modified by jessie for AM0224,2005/10/18,P4.11
				string rcardCondition = 
					//排除掉最后一道工序的良品，逻辑：状态为Good,IsCom（是否完成标志位）为1(该逻辑不再使用)
					//QA岗位在制数量（FQC）＝在检已确认样本数＋QA判退数中尚未被在线返工的数量
					string.Format(
					@"and rcard in
						   (select rcard
							from tbllot2cardcheck
							where tbllot2cardcheck.rcard = tblsimulationreport.rcard
							and tbllot2cardcheck.rcardseq = tblsimulationreport.rcardseq
							and tbllot2cardcheck.mocode = tblsimulationreport.mocode
							union all
							select rcard
							from tblreject
							where tblreject.rcard = tblsimulationreport.rcard
							and tblreject.rcardseq = tblsimulationreport.rcardseq
							and tblreject.mocode = tblsimulationreport.mocode
							union all
							select rcard
							from tblsimulationreport
							where instr(laction,'OQC')=0 or instr(laction,'OQCLOT')=1 ) "+
					//排除掉最后一道工序的良品，逻辑：状态为Good,IsCom（是否完成标志位）为1
					@"and iscom = {1} "+
					//进入维修的不良品不再为在制品
					@"and rcard not in
					(select a.rcard
					from tblts a
					where a.rcard = tblsimulationreport.rcard 
						and a.rcardseq = tblsimulationreport.rcardseq
						and a.mocode = tblsimulationreport.mocode
						and tsstatus <> '{2}')",ProductStatus.GOOD,ProductComplete.NoComplete,TSStatus.TSStatus_New);
				//querySQL = string.Format("select count(*) from (select distinct RESCODE,SHIFTDAY,SHIFTCODE from TBLSIMULATIONREPORT where 1=1 {0} and OPCODE='{1}' {2} {3})",
				/* modified by jessie lee, 2005/12/14 */
				querySQL = string.Format("select count(*) from (select distinct SEGCODE,SHIFTDAY,SHIFTCODE, SSCODE, RESCODE, MOCODE from TBLSIMULATIONREPORT where 1=1 {0} and OPCODE='{1}' {2} {3})",
					itemCondition, operationCode, moCondition,rcardCondition);
			}

			return this.DataProvider.GetCount( 
				new SQLCondition( querySQL ) );
		}
		#endregion

		#region 在制品明细
		/// <summary>
		/// ** 功能描述:	在制品明细
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-06-23
		/// ****************************
		/// ** 修 改:		jessie lee	
		/// ** 日 期:		2005-9-7
		/// ****************************
		/// ** 修 改:       Jessie Lee
		/// ** 日 期:       2005-9-29
		/// ** 原 因:       增加日期范围的查询条件
		/// </summary>
		/// <param name="itemCode"></param>
		/// <param name="operationCode"></param>
		/// <param name="resourceCode"></param>
		/// <param name="moCode"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="inclusive"></param>
		/// <param name="exclusive"></param>
		/// <returns></returns>
		public object[] QueryOnWipInfoDistributing(string status, string itemCode,string shiftCode, string operationCode, string resourceCode, string moCode, int startDate,int endDate,
			int inclusive,int exclusive )
		{
			string dateCondition = FormatHelper.GetDateRangeSql("a.SHIFTDAY",startDate,endDate);

			/* modified by jessie lee
			 * 添加维修工序 */
			string rcardCondition = string.Empty ;

			if( string.Compare( operationCode,"TS",true) !=0 )
			{
				//added by jessie lee for AM0220,2005/10/11
				//修改对在制品查询的定义
				//非最后工序：工序的良品数量＋工序的尚未被送修确认的不良品数量＝工序在制数量
				//最后工序：工序的尚未被送修确认的不良品数量＝工序在制数量(该逻辑不再使用)
				//modified by jessie for AM0224,2005/10/18,P4.11
				rcardCondition = 
					//排除掉最后一道工序的良品，逻辑：状态为Good,IsCom（是否完成标志位）为1(该逻辑不再使用)
					//QA岗位在制数量（FQC）＝在检已确认样本数＋QA判退数中尚未被在线返工的数量
					string.Format(
					@"and rcard in
						   (select rcard
							from tbllot2cardcheck
							where tbllot2cardcheck.rcard = a.rcard
							and tbllot2cardcheck.rcardseq = a.rcardseq
							and tbllot2cardcheck.opcode = a.opcode
							and tbllot2cardcheck.mocode = a.mocode
							union all
							select rcard
							from tblreject
							where tblreject.rcard = a.rcard
							and tblreject.rcardseq = a.rcardseq
							and tblreject.mocode = a.mocode
							union all
							select rcard
							from tblsimulationreport
							where instr(laction,'OQC')=0 or instr(laction,'OQCLOT')=1 ) "+
					//排除掉最后一道工序的良品，逻辑：状态为Good,IsCom（是否完成标志位）为1
					@"and a.iscom = {1} "+
					//进入维修的不良品不再为在制品
					@"and rcard not in
					(select c.rcard
					from tblts c
					where c.rcard = a.rcard and c.rcardseq = a.rcardseq and c.mocode = a.mocode
						and c.tsstatus <> '{2}')",ProductStatus.GOOD,ProductComplete.NoComplete,TSStatus.TSStatus_New);
			}
			else/* TS */
			{
				rcardCondition = string.Format(@"and a.rcard in
					(select c.rcard
					from tblts c
					where c.rcard = a.rcard and c.rcardseq = a.rcardseq and c.mocode = a.mocode
						and c.tsstatus = '{0}' )",
					status);
			}

			string statusCondition = string.Empty;
			if( string.Compare( operationCode,"TS",true) !=0 )
			{
				if( string.Compare(status,"GOOD",true)==0 )
				{
					statusCondition = string.Format(" and a.status not in ( '{0}','{1}' ) ",ProductStatus.NG,ProductStatus.Reject);
				}
				else if( string.Compare(status,"NG",true)==0 )
				{
					statusCondition = string.Format(" and a.status = '{0}'",ProductStatus.NG);
				}
				else if( string.Compare(status,"REJECT",true)==0 )
				{
					statusCondition = string.Format(" and a.status = '{0}'",ProductStatus.Reject);
				}
			}

			if( string.Compare( operationCode,"TS",true) !=0 )
			{
                string sql = "";
				if( itemCode.Trim() != string.Empty )
				{
                    sql = "select RCARD, RCARDSEQ,TCARD, STATUS, b.OPCONTROL, a.MUSER, a.MDATE, a.MTIME ,a.IDMERGERULE,a.LACTION " +
                          "						from TBLSIMULATIONREPORT a, TBLITEMROUTE2OP b " +
                          "						where a.OPCODE=b.OPCODE and a.ROUTECODE=b.ROUTECODE and a.ITEMCODE=b.ITEMCODE {1} {2} {3} ";
                    if (GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName().Length > 0)
                    {
                        sql += " and b.orgid in (" + GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName() + ")";
                    }
					sql +="							and a.ITEMCODE in ( {0} ) and a.shiftcode = '{4}' and a.OPCODE=$OPCODE1 and RESCODE=$RESCODE1 and MOCODE=$MOCODE1 " +
						  "				union  " +
						  "					select RCARD, RCARDSEQ, TCARD,STATUS, b.OPCONTROL, a.MUSER, a.MDATE, a.MTIME ,a.IDMERGERULE,a.LACTION " +
						  "						from TBLSIMULATIONREPORT a, TBLOP b " +
						  "						where a.OPCODE=b.OPCODE and a.ROUTECODE is null {1} " +
						  "							and a.ITEMCODE in ( {0} ) and a.shiftcode = '{4}' and a.OPCODE=$OPCODE2 and RESCODE=$RESCODE2 and MOCODE=$MOCODE2";
					return this.DataProvider.CustomQuery( 
						typeof(OnWipInfoDistributing),
                        new PagerParamCondition(string.Format(sql ,
						FormatHelper.ProcessQueryValues(itemCode),dateCondition,statusCondition,rcardCondition,shiftCode),
						"RCARD", inclusive, exclusive,
						new SQLParameter[]{	  new SQLParameter("OPCODE1", typeof(string), operationCode),
											  new SQLParameter("RESCODE1", typeof(string), resourceCode),
											  new SQLParameter("MOCODE1", typeof(string), moCode),
											  new SQLParameter("OPCODE2", typeof(string), operationCode),
											  new SQLParameter("RESCODE2", typeof(string), resourceCode),
											  new SQLParameter("MOCODE2", typeof(string), moCode)
										  }, true ));
				}

                sql = "select RCARD, RCARDSEQ,TCARD, STATUS, b.OPCONTROL, a.MUSER, a.MDATE, a.MTIME ,a.IDMERGERULE,a.LACTION " +
                        "						from TBLSIMULATIONREPORT a, TBLITEMROUTE2OP b " +
                        "						where a.OPCODE=b.OPCODE and a.ROUTECODE=b.ROUTECODE and a.ITEMCODE=b.ITEMCODE {0} {1} {2} ";
                if (GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName().Length > 0)
                {
                    sql += " and b.orgid in (" + GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName() + ")";
                }
				sql +=  "							and a.shiftcode = '{3}' and a.OPCODE=$OPCODE1 and RESCODE=$RESCODE1 and MOCODE=$MOCODE1  " +
						"				union  " +
						"					select RCARD, RCARDSEQ,TCARD, STATUS, b.OPCONTROL, a.MUSER, a.MDATE, a.MTIME ,a.IDMERGERULE,a.LACTION " +
						"						from TBLSIMULATIONREPORT a, TBLOP b " +
						"						where a.OPCODE=b.OPCODE and a.ROUTECODE is null {0} {1} {2} " +
						"							and a.shiftcode = '{3}' and a.OPCODE=$OPCODE2 and RESCODE=$RESCODE2 and MOCODE=$MOCODE2";

				return this.DataProvider.CustomQuery( 
					typeof(OnWipInfoDistributing),
                    new PagerParamCondition(string.Format(sql,
					dateCondition,statusCondition,rcardCondition,shiftCode),
					"RCARD", inclusive, exclusive,
					new SQLParameter[]{	  new SQLParameter("OPCODE1", typeof(string), operationCode),
										  new SQLParameter("RESCODE1", typeof(string), resourceCode),
										  new SQLParameter("MOCODE1", typeof(string), moCode),
										  new SQLParameter("OPCODE2", typeof(string), operationCode),
										  new SQLParameter("RESCODE2", typeof(string), resourceCode),
										  new SQLParameter("MOCODE2", typeof(string), moCode)
									  }, true ));
			}
			else/* TS */
			{
				if( itemCode.Trim() != string.Empty )
				{
					return this.DataProvider.CustomQuery( 
						typeof(OnWipInfoDistributing),
						new PagerParamCondition( string.Format( @"select b.RCARD, b.RCARDSEQ,b.TCARD,b.TSSTATUS AS STATUS, b.MUSER, b.MDATE, b.MTIME ,a.IDMERGERULE,'TS' AS LACTION
												from TBLSIMULATIONREPORT a, TBLTS b
												where a.RCARD=b.RCARD and a.RCARDSEQ=b.RCARDSEQ and a.MOCODE=b.MOCODE {1} {2} 
													and a.ITEMCODE in ( {0} ) and b.shiftcode = '{3}' and b.CRESCODE=$RESCODE1 and a.MOCODE=$MOCODE1" ,
						FormatHelper.ProcessQueryValues(itemCode),dateCondition,rcardCondition,shiftCode),
						"RCARD", inclusive, exclusive,
						new SQLParameter[]{	  new SQLParameter("RESCODE1", typeof(string), resourceCode),
											  new SQLParameter("MOCODE1", typeof(string), moCode)
										  }, true ));
				}

				return this.DataProvider.CustomQuery( 
					typeof(OnWipInfoDistributing),
					new PagerParamCondition( string.Format( @"select b.RCARD, b.RCARDSEQ,b.TCARD, b.TSSTATUS AS STATUS, b.MUSER, b.MDATE, b.MTIME ,a.IDMERGERULE,'TS' AS LACTION
												from TBLSIMULATIONREPORT a, TBLTS b
												where a.RCARD=b.RCARD and a.RCARDSEQ=b.RCARDSEQ and a.MOCODE=b.MOCODE {0} {1} 
													and b.shiftcode = '{2}' and b.CRESCODE=$RESCODE1 and a.MOCODE=$MOCODE1 " ,
					dateCondition,rcardCondition,shiftCode),
					"RCARD", inclusive, exclusive,
					new SQLParameter[]{	  new SQLParameter("RESCODE1", typeof(string), resourceCode),
										  new SQLParameter("MOCODE1", typeof(string), moCode)
									  }, true ));
			}

		}
		
		/// <summary>
		/// ** 修 改:       Jessie Lee
		/// ** 日 期:       2005-9-29
		/// ** 原 因:       增加日期范围的查询条件
		/// </summary>
		/// <param name="itemCode"></param>
		/// <param name="operationCode"></param>
		/// <param name="resourceCode"></param>
		/// <param name="moCode"></param>
		/// <returns></returns>
		public int QueryOnWipInfoDistributingCount(string status, string itemCode,string shiftCode, string operationCode, string resourceCode, string moCode, int startDate,int endDate )
		{
			string dateCondition = FormatHelper.GetDateRangeSql("SHIFTDAY",startDate,endDate);

			/* modified by jessie lee
			 * 添加维修工序 */
			string rcardCondition = string.Empty ;
			if( string.Compare( operationCode,"TS",true) !=0 )
			{
				//added by jessie lee for AM0220,2005/10/11
				//修改对在制品查询的定义
				//非最后工序：工序的良品数量＋工序的尚未被送修确认的不良品数量＝工序在制数量
				//最后工序：工序的尚未被送修确认的不良品数量＝工序在制数量(该逻辑不再使用)
				//modified by jessie for AM0224,2005/10/18,P4.11
				rcardCondition = 
					//排除掉最后一道工序的良品，逻辑：状态为Good,IsCom（是否完成标志位）为1(该逻辑不再使用)
					//QA岗位在制数量（FQC）＝在检已确认样本数＋QA判退数中尚未被在线返工的数量
					string.Format(
					@"and rcard in
						   (select rcard
							from tbllot2cardcheck
							where tbllot2cardcheck.rcard = tblsimulationreport.rcard
							and tbllot2cardcheck.rcardseq = tblsimulationreport.rcardseq
							and tbllot2cardcheck.mocode = tblsimulationreport.mocode
							union all
							select rcard
							from tblreject
							where tblreject.rcard = tblsimulationreport.rcard
							and tblreject.rcardseq = tblsimulationreport.rcardseq
							and tblreject.mocode = tblsimulationreport.mocode
							union all
							select rcard
							from tblsimulationreport
							where instr(laction,'OQC')=0 or instr(laction,'OQCLOT')=1 ) "+
					//排除掉最后一道工序的良品，逻辑：状态为Good,IsCom（是否完成标志位）为1
					@"and iscom = {1} "+
					//进入维修的不良品不再为在制品
					@"and rcard not in
					(select a.rcard
					from tblts a
					where a.rcard = tblsimulationreport.rcard and a.rcardseq = tblsimulationreport.rcardseq and a.mocode=tblsimulationreport.mocode 
						and tsstatus <> '{2}')",ProductStatus.GOOD,ProductComplete.NoComplete,TSStatus.TSStatus_New);
			}
			else/* TS */
			{
				rcardCondition = string.Format(@"and rcard in
					(select c.rcard
					from tblts c
					where c.rcard = tblsimulationreport.rcard and c.rcardseq = tblsimulationreport.rcardseq and c.mocode = tblsimulationreport.mocode
						and c.tsstatus = '{0}' )",
					status);
			}

			string statusCondition = string.Empty;
			if( string.Compare( operationCode,"TS",true) !=0 )
			{
				if( string.Compare(status,"GOOD",true)==0 )
				{
					statusCondition = string.Format(" and status not in ( '{0}','{1}' ) ",ProductStatus.NG,ProductStatus.Reject);
				}
				else if( string.Compare(status,"NG",true)==0 )
				{
					statusCondition = string.Format(" and status = '{0}'",ProductStatus.NG);
				}
				else if( string.Compare(status,"REJECT",true)==0 )
				{
					statusCondition = string.Format(" and status = '{0}'",ProductStatus.Reject);
				}
			}

			if( string.Compare( operationCode,"TS",true) !=0 )
			{
				if(itemCode.Trim() != String.Empty)
				{
					return this.DataProvider.GetCount( 
						new SQLParamCondition( String.Format( @"select count(*) from TBLSIMULATIONREPORT 
											where ITEMCODE in ({0}) {1} {2} {3} and shiftcode = '{4}' and OPCODE=$OPCODE and RESCODE=$RESCODE and MOCODE=$MOCODE",
						FormatHelper.ProcessQueryValues(itemCode),dateCondition,statusCondition, rcardCondition,shiftCode),
						new SQLParameter[]{ new SQLParameter("OPCODE", typeof(string), operationCode),
											  new SQLParameter("RESCODE", typeof(string), resourceCode),
											  new SQLParameter("MOCODE", typeof(string), moCode)
										  } ));
				}

				return this.DataProvider.GetCount( 
					new SQLParamCondition( String.Format(@"select count(*) from TBLSIMULATIONREPORT 
											where OPCODE=$OPCODE {0} {1} {2} and shiftcode = '{3}' and RESCODE=$RESCODE and MOCODE=$MOCODE",
					dateCondition,statusCondition, rcardCondition,shiftCode),
					new SQLParameter[]{   new SQLParameter("OPCODE", typeof(string), operationCode),
										  new SQLParameter("RESCODE", typeof(string), resourceCode),
										  new SQLParameter("MOCODE", typeof(string), moCode)
									  } ));
			}
			else/* TS */
			{
				if(itemCode.Trim() != String.Empty)
				{
					return this.DataProvider.GetCount( 
						new SQLParamCondition( String.Format( @"select count(*) from TBLSIMULATIONREPORT 
											where ITEMCODE in ({0}) {1} {2} and shiftcode = '{3}' and RESCODE=$RESCODE and MOCODE=$MOCODE",
						FormatHelper.ProcessQueryValues(itemCode),dateCondition, rcardCondition,shiftCode),
						new SQLParameter[]{   new SQLParameter("RESCODE", typeof(string), resourceCode),
											  new SQLParameter("MOCODE", typeof(string), moCode)
										  } ));
				}

				return this.DataProvider.GetCount( 
					new SQLParamCondition( String.Format(@"select count(*) from TBLSIMULATIONREPORT 
											where 1=1 {0} {1} and shiftcode = '{2}' and RESCODE=$RESCODE and MOCODE=$MOCODE",
					dateCondition, rcardCondition,shiftCode),
					new SQLParameter[]{   new SQLParameter("RESCODE", typeof(string), resourceCode),
										  new SQLParameter("MOCODE", typeof(string), moCode)
									  }));
			}

		}
		#endregion

		#region help method
	
		public bool IsOpFQC( string opCode,string itemCode, string moCode ,int startDate,int endDate )
		{
			string itemCondition = String.Empty;
			if(itemCode.Trim() != String.Empty)
			{
				itemCondition += string.Format(" and ITEMCODE in ( {0} ) ",FormatHelper.ProcessQueryValues(itemCode) ) ;
			}

			string moCondition = string.Format("and MOCODE in( select MOCODE from TBLMO where MOSTATUS in ('{0}', '{1}') ", 
				BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_OPEN,
				BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_PENDING );

            moCondition += GlobalVariables.CurrentOrganizations.GetSQLCondition();

			if ( moCode.Trim() != string.Empty )
			{
				moCondition += string.Format(" and MOCODE in ({0}))", FormatHelper.ProcessQueryValues(moCode) );
			}
			else
			{
				moCondition += ")";
			}
			moCondition += FormatHelper.GetDateRangeSql("SHIFTDAY",startDate,endDate);

			int count = this.DataProvider.GetCount(
				new SQLCondition(string.Format(
				@"select count(opcode) from 
				(select opcode from tblsimulationreport where opcode = '{0}' 
				and instr(laction,'OQC')=1 and instr(laction,'OQCLOT')=0
				{1} {2} and rownum = 1 )",
				opCode,itemCondition,moCondition)));

			if(count>0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion
        
	}
}
