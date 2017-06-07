using System;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Common.Helper;  
using BenQGuru.eMES.Common.DomainDataProvider;   
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QueryStockFacade 的摘要说明。
	/// </summary>
	public class ReportViewConfigFacade
	{
		private  IDomainDataProvider _domainDataProvider= null;

		public ReportViewConfigFacade( IDomainDataProvider domainDataProvider )
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

		#region ReportCenterViewFacade

		public object[] GetReportCenterViewByUser(string userCode)
		{
			object[] objs = this.DataProvider.CustomQuery(typeof(ReportCenterView), new SQLCondition(string.Format("select {0} from TBLBSRPTVIEW where USERCODE='{1}' order by USERCODE,SEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReportCenterView)), userCode)));
			if (objs == null)
			{
				objs = this.DataProvider.CustomQuery(typeof(ReportCenterView), new SQLCondition(string.Format("select {0} from TBLBSRPTVIEW where USERCODE='REPORTCENTER_VIEW_LIST_SYSTEM_DEFAULT' and ISDEFAULT='1' order by USERCODE,SEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReportCenterView)), userCode)));
			}
			return objs;
		}
		public object[] GetReportCenterViewDefault()
		{
			object[] objs = this.DataProvider.CustomQuery(typeof(ReportCenterView), new SQLCondition(string.Format("select {0} from TBLBSRPTVIEW where USERCODE='REPORTCENTER_VIEW_LIST_SYSTEM_DEFAULT' order by USERCODE,SEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReportCenterView)))));
			return objs;
		}
		public void UpdateReportCenterViewList(string userCode, string viewList)
		{
			this.DataProvider.BeginTransaction();
			try
			{
				object[] objsDefault = this.GetReportCenterViewDefault();
				System.Collections.Hashtable htDefault = new System.Collections.Hashtable();
				for (int i = 0; i < objsDefault.Length; i++)
				{
					htDefault.Add(((ReportCenterView)objsDefault[i]).ReportCode, objsDefault[i]);
				}
				string strSql = "delete from TBLBSRPTVIEW where usercode='" + userCode + "'";
				this.DataProvider.CustomExecute(new SQLCondition(strSql));
				string[] moField = viewList.Split(';');
				for (int i = 0; i < moField.Length; i++)
				{
					if (moField[i].Trim() != string.Empty)
					{
						ReportCenterView view = new ReportCenterView();
						view.UserCode = userCode;
						view.Sequence = i;
						view.ReportCode = moField[i];
						view.IsDefault = "0";
						if (htDefault.ContainsKey(view.ReportCode) == true)
						{
							ReportCenterView objDefault = (ReportCenterView)htDefault[view.ReportCode];
							view.Width = objDefault.Width;
							view.WidthType = objDefault.WidthType;
							view.Height = objDefault.Height;
							view.HeightType = objDefault.HeightType;
						}
						this.DataProvider.Insert(view);
					}
				}
				this.DataProvider.CommitTransaction();
			}
			catch (Exception ex)
			{
				this.DataProvider.RollbackTransaction();
				throw ex;
			}
		}

		#endregion

	}
	
	#region ReportCenterView
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLBSRPTVIEW", "USERCODE,SEQ")]
	public class ReportCenterView : DomainObject
	{
		public ReportCenterView()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("USERCODE", typeof(string), 40, true)]
		public string  UserCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
		public decimal  Sequence;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RPTCODE", typeof(string), 40, false)]
		public string  ReportCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("HEIGHT", typeof(decimal), 15, true)]
		public decimal  Height;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("HEIGHTTYPE", typeof(string), 40, false)]
		public string  HeightType;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("WIDTH", typeof(decimal), 15, true)]
		public decimal  Width;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("WIDTHTYPE", typeof(string), 40, false)]
		public string  WidthType;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ISDEFAULT", typeof(string), 1, true)]
		public string  IsDefault;

	}
	#endregion

	public class ReportCenterViewCode
	{
		public static string Quantity = "REPORT_QUANTITY";
		public static string YieldPercent = "REPORT_YIELDPERCENT";
		public static string OQC = "REPORT_OQC";
		public static string TSInfo = "REPORT_TSINFOSORT";
	}
}
