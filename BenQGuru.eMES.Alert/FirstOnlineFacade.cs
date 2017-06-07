using System;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;  
using BenQGuru.eMES.Domain.Alert;
using BenQGuru.eMES.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.AlertModel
{
	/// <summary>
	/// FirstOnlineFacade 的摘要说明。
	/// </summary>
	public class FirstOnlineFacade
	{
		private IDomainDataProvider _domainDataProvider = null;
		private FacadeHelper _helper = null;

		

		public FirstOnlineFacade(IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
			this._helper = new FacadeHelper( DataProvider );
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
			
		public FirstOnline CreateNewFirstOnline()
		{
			return new FirstOnline();
		}

		public void AddFirstOnline( FirstOnline firstOnline)
		{
			this._helper.AddDomainObject( firstOnline );
		}

		public void UpdateFirstOnline(FirstOnline firstOnline)
		{
			this._helper.UpdateDomainObject( firstOnline );
		}

		public void DeleteFirstOnline(FirstOnline firstOnline)
		{
			this._helper.DeleteDomainObject( firstOnline );
		}

		public void DeleteFirstOnline(FirstOnline[] firstOnline)
		{
			this._helper.DeleteDomainObject( firstOnline );
		}

		public object GetFirstOnline( string sSCode, int maintainDate, string itemcode,string shiftcode,int begintime )
		{
			return this.DataProvider.CustomSearch(typeof(FirstOnline), new object[]{ sSCode, maintainDate, itemcode,shiftcode,begintime });
		}

		/// <summary>
		/// ** 功能描述:	查询FirstOnline的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-9-5 14:02:07
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="sSCode">SSCode，模糊查询</param>
		/// <param name="maintainDate">MaintainDate，模糊查询</param>
		/// <param name="actionType">ActionType，模糊查询</param>
		/// <returns> FirstOnline的总记录数</returns>
		public int QueryFirstOnlineCount( string sSCode, int maintainDate, string actionType)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLFIRSTONLINE where 1=1 and SSCODE like '{0}%'  and MDATE like '{1}%'  and ActionType like '{2}%' " , sSCode, maintainDate, actionType)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询FirstOnline
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-9-5 14:02:07
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="sSCode">SSCode，模糊查询</param>
		/// <param name="maintainDate">MaintainDate，模糊查询</param>
		/// <param name="actionType">ActionType，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> FirstOnline数组</returns>
		public object[] QueryFirstOnline( string sSCode, int maintainDate, string actionType, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(FirstOnline), new PagerCondition(string.Format("select {0} from TBLFIRSTONLINE where 1=1 and SSCODE like '{1}%'  and MDATE like '{2}%'  and ActionType like '{3}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(FirstOnline)) , sSCode, maintainDate, actionType), "SSCODE,MDATE,ActionType", inclusive, exclusive));
		}

		//取出做过的上线信息
		public object[] QueryFirst( string sSCode, int maintainDate, string itemcode,string shiftcode )
		{
			return this.DataProvider.CustomQuery(typeof(FirstOnline), new SQLCondition(string.Format("select {0} from TBLFIRSTONLINE where 1=1 and SSCODE = '{1}'  and MDATE = '{2}'  and itemcode = '{3}' and shiftcode='{4}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(FirstOnline)) , sSCode, maintainDate, itemcode,shiftcode)));
		}

		//根据产线，产品代码，取出今天和跨天的班次昨天做的末件上线
		public object[] QueryLast( string sSCode,string itemcode)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("select {0} from TBLFIRSTONLINE where 1=1 and SSCODE = '{1}' and itemcode = '{2}' and lasttype='ON'")
			.Append(" and (")
						.Append("mdate=").Append(FormatHelper.TODateInt(DateTime.Now))
						.Append(" or (")
								.Append("mdate=").Append(FormatHelper.TODateInt(DateTime.Now.AddDays(-1)))
								.Append(" and isoverday='1'")
						.Append(")")
					.Append(")");

			return this.DataProvider.CustomQuery(typeof(FirstOnline), new SQLCondition(string.Format(sb.ToString(), DomainObjectUtility.GetDomainObjectFieldsString(typeof(FirstOnline)) , sSCode,itemcode)));
		}

		//根据产线，产品代码，取出今天和跨天的班次昨天做的首件上线
		public object[] QueryFirst( string sSCode,string itemcode)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("select {0} from TBLFIRSTONLINE where 1=1 and SSCODE = '{1}' and itemcode = '{2}' and actiontype='ON'")
				.Append(" and (")
				.Append("mdate=").Append(FormatHelper.TODateInt(DateTime.Now))
				.Append(" or (")
				.Append("mdate=").Append(FormatHelper.TODateInt(DateTime.Now.AddDays(-1)))
				.Append(" and isoverday='1'")
				.Append(")")
				.Append(")");

			return this.DataProvider.CustomQuery(typeof(FirstOnline), new SQLCondition(string.Format(sb.ToString(), DomainObjectUtility.GetDomainObjectFieldsString(typeof(FirstOnline)) , sSCode,itemcode)));
		}

		//根据产线，产品代码，shiftcode,下班时间，取出今天和跨天的班次昨天做的末件上线
		public object[] QueryLastBegin( string sSCode,string itemcode,string shiftcode,int endtime)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("select {0} from TBLFIRSTONLINE where 1=1 and SSCODE = '{1}' and itemcode = '{2}' and shiftcode='{3}' and endtime={4} and lasttype='ON'")
				.Append(" and (")
				.Append("mdate=").Append(FormatHelper.TODateInt(DateTime.Now))
				.Append(" or (")
				.Append("mdate=").Append(FormatHelper.TODateInt(DateTime.Now.AddDays(-1)))
				.Append(" and isoverday='1'")
				.Append(")")
				.Append(")");

			return this.DataProvider.CustomQuery(typeof(FirstOnline), new SQLCondition(string.Format(sb.ToString(), DomainObjectUtility.GetDomainObjectFieldsString(typeof(FirstOnline)) , sSCode,itemcode,shiftcode,endtime)));
		}
		public object[] QueryFirstOnline( string sSCode, int maintainDate)
		{
			return this.DataProvider.CustomQuery(typeof(FirstOnline), new SQLCondition(string.Format("select {0} from TBLFIRSTONLINE where 1=1 and SSCODE = '{1}'  and MDATE = '{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(FirstOnline)) , sSCode, maintainDate)));
		}

		
		/// <summary>
		/// ** 功能描述:	获得所有的FirstOnline
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-9-5 14:02:07
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>FirstOnline的总记录数</returns>
		public object[] GetAllFirstOnline()
		{
			return this.DataProvider.CustomQuery(typeof(FirstOnline), new SQLCondition(string.Format("select {0} from TBLFIRSTONLINE order by SSCODE,MDATE,ActionType", DomainObjectUtility.GetDomainObjectFieldsString(typeof(FirstOnline)))));
		}


		
		//Bind上班时间下拉列表，返回根据当前时间选定的记录
		public int BindShiftTime(System.Collections.IList list,Resource res)
		{
			//根据res取得班制
			BenQGuru.eMES.BaseSetting.BaseModelFacade bm = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);

			if(res == null) return -1;
            //Segment seg = bm.GetSegment(res.SegmentCode) as Segment;
            //if(seg == null) return -1;
            //string shifttype = seg.ShiftTypeCode;
            string shifttype = res.ShiftTypeCode;

			//根据
			BenQGuru.eMES.BaseSetting.ShiftModelFacade shift = new BenQGuru.eMES.BaseSetting.ShiftModelFacade(this.DataProvider);
			object[] objs = this.DataProvider.CustomQuery(typeof(Shift), new SQLCondition(string.Format("select {0} from TBLSHIFT where shifttypecode='{1}'order by SHIFTBTIME", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Shift)),shifttype)));
			if(objs != null && objs.Length > 0)
			{
				foreach(object obj in objs)
				{
					BenQGuru.eMES.Domain.BaseSetting.Shift s = obj as BenQGuru.eMES.Domain.BaseSetting.Shift;
					if( s!= null)
					{
						list.Add(new UIShift(s.ShiftCode,s.ShiftBeginTime,s.ShiftEndTime,s.IsOverDate));
					}
				}
			}

			int time = FormatHelper.TOTimeInt(DateTime.Now);

			return GetCurrShiftIndex(time,list);
		}

		//取出当前时间所在的班次
		public int GetCurrShiftIndex(int time,System.Collections.IList list)
		{
			

			for(int i= list.Count - 1;i>-1;i--)
			{
				if(
					(time >= ((UIShift)list[i]).BeginTime && time <= ((UIShift)list[i]).EndTime) //不跨天，在时间段之内
					||
					(time >= ((UIShift)list[i]).BeginTime && ((UIShift)list[i]).IsOverDay== FormatHelper.TRUE_STRING)　///跨天，在第一天
					||
					(time <= ((UIShift)list[i]).EndTime && ((UIShift)list[i]).IsOverDay== FormatHelper.TRUE_STRING) ///跨天，比较每二天
					)
				{
					return i;
				}
			}

			return -1;
		}
	}

	public class UIShift
	{
		public string ShiftCode;
		public int BeginTime;
		public int EndTime;
		public string IsOverDay;

		public UIShift(string code,int time,int e_time,string isOver)
		{
			ShiftCode = code;
			BeginTime = time;
			EndTime = e_time;

			this.IsOverDay = isOver;
		}

		public override string ToString()
		{
			return FormatHelper.ToTimeString(BeginTime);
		}
			
		public override bool Equals(object obj)
		{
			if(obj == null) 
				return false;

			string str = obj as string;
			if(str != null)
			{
				return str == this.ShiftCode;
			}

			UIShift s = obj as UIShift;
			if(s != null)
			{
				return s.BeginTime == this.BeginTime;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return this.BeginTime.GetHashCode();
		}


	}
	public class LineActionType
	{
		public const string ON = "ON";
		public const string OFF = "OFF";
	}

	public class UILastOnline
	{
		public FirstOnline _first;
		public UILastOnline(FirstOnline first)
		{
			_first = first;
		}
		public override string ToString()
		{
			return FormatHelper.ToTimeString(this._first.EndTime);
		}

		public override bool Equals(object obj)
		{
			if(obj==null) return false;
			UILastOnline last = obj as UILastOnline;
			if(last == null) return false;
			if(this._first == null) return false;

			return this._first.EndTime == last._first.EndTime;
		}

		public override int GetHashCode()
		{
			return this._first.EndTime.ToString().GetHashCode();
		}


	}

	public class UIFirstOnline
	{
		public FirstOnline _first;
		public UIFirstOnline(FirstOnline first)
		{
			_first = first;
		}
		public override string ToString()
		{
			return FormatHelper.ToTimeString(this._first.ShiftTime);
		}

		public override bool Equals(object obj)
		{
			if(obj==null) return false;
			UILastOnline last = obj as UILastOnline;
			if(last == null) return false;
			if(this._first == null) return false;

			return this._first.ShiftTime == last._first.ShiftTime;
		}

		public override int GetHashCode()
		{
			return this._first.ShiftTime.ToString().GetHashCode();
		}


	}
}
