using System;

using UserControl;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Common.MutiLanguage; 
using BenQGuru.eMES.Common.Helper;  
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper; 

namespace BenQGuru.eMES.Dashboard
{
	/// <summary>
	/// DashboardFactory 的摘要说明。
	/// </summary>
	public class ModelFacade
	{
		private  IDomainDataProvider _domainDataProvider = null;
		public IDomainDataProvider DataProvider
		{
			get
			{
//				if (_domainDataProvider == null)
//				{
//					_domainDataProvider = DomainDataProviderManager.DomainDataProvider(); 
//				}

				return _domainDataProvider;
			}	
		}

		public ModelFacade(IDomainDataProvider dataProvider)
		{
			_domainDataProvider = dataProvider;
		}


		#region 获取机种产品列表
		public string getModel2Item()
		{
			string strReturn = String.Empty;

			#region Contract Total SQL String

			string sqlCondition = String.Empty;

			string sqlMain = @"select * from tblmodel2item where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " order by modelcode";

			string sql = String.Format(sqlMain,sqlCondition);
			#endregion

			object[] objs = DataProvider.CustomQuery(typeof(ModelQueryObject)
				,new SQLCondition(sql));

			System.Collections.Hashtable htTotal = new System.Collections.Hashtable();

			if(objs != null && objs.Length > 0)
			{
				foreach(ModelQueryObject obj in objs)
				{
					//处理Total Object
					if(htTotal.ContainsKey(obj.ModelCode))
					{
						ModelObject mo = (ModelObject)htTotal[obj.ModelCode];

						mo.ModelCode = obj.ModelCode;
						mo.ItemCode = obj.ItemCode;
						
						#region 处理Items

						if(mo.Items == null)
						{
							mo.Items = new System.Collections.ArrayList();
						}
						mo.Items.Add(obj);

						htTotal[obj.ModelCode] = mo;
						#endregion
					}
					else
					{
						ModelObject mo = new ModelObject();

						mo.ModelCode = obj.ModelCode;
						mo.ItemCode = obj.ItemCode;
						
						#region 处理Items

						if(mo.Items == null)
						{
							mo.Items = new System.Collections.ArrayList();
						}
						mo.Items.Add(obj);

						htTotal.Add(obj.ModelCode,mo);
						#endregion
					}
				}
			}

			ModelXmlBuilder xml = new ModelXmlBuilder();
			xml.BeginBuildRoot();
			//写入汇总
			xml.BeginBuildModel("", "");
			xml.EndBuildModel();
			foreach(ModelObject mo in htTotal.Values)
			{
				xml.BeginBuildModel(mo.ModelCode,mo.ModelCode);
				//写入天数据
				xml.BeginBuildItem("", "");
				foreach(ModelQueryObject mqo in mo.Items)
				{
					xml.BeginBuildItem(mqo.ItemCode,mqo.ItemCode);
				}
				xml.EndBuildModel();
			}
			xml.EndBuildRoot();

			strReturn = xml.XmlContent.ToString();

		return strReturn;
		}
		#endregion

	}

}
