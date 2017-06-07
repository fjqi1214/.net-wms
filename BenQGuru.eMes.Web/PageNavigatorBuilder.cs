using System;
using System.Collections;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Web.UserControl;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Web
{
	/// <summary>
	/// PageNavigatorBuilder 的摘要说明。
	/// </summary>
	public class PageNavigatorBuilder
	{
		private static string Module_Prefix = "module_";
		public static void Build(PageNavigator pageNavigator, string moduleCode,string url ,Hashtable urls, ControlLibrary.Web.Language.LanguageComponent languageComponent ,IDomainDataProvider _domainDataProvider,BasePage page)
		{
			if ( pageNavigator == null )
			{
				return;
			}

			pageNavigator.Clear();
			pageNavigator.AddRootPageNavigator("MES", "");
			
			//BenQGuru.eMES.Common.Domain.IDomainDataProvider _domainDataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider();     
			SystemSettingFacade facade =new SystemSettingFacade(_domainDataProvider);// new SystemSettingFacadeFactory().Create();

			ITreeObjectNode moduleTree = facade.BuildModuleTree();
			ITreeObjectNode currNode = ((ITreeObjectNode)moduleTree).GetTreeObjectNodeByID(moduleCode);
			
			if ( currNode == null )
			{
				ExceptionManager.Raise(typeof(Module), "$Error_Module_Not_Exist", string.Format("[$ModuleCode={0}]", moduleCode) );	
			}

			TreeObjectNodeSet set = currNode.GetChainFromRoot();

			//foreach ( ModuleTreeNode node in set )
			for(int i = 0 ;i<set.Count;i++)
			{
				ModuleTreeNode node = (set[i] as ModuleTreeNode);
				if ( node.ID != string.Empty )
				{
					languageComponent.Language = Web.Helper.SessionHelper.Current(page.Session).Language;
					string name = languageComponent.GetString( Module_Prefix + node.Module.ModuleCode );

					if ( name == string.Empty )
					{
						name = node.Module.ModuleCode;
					}

					if(i<set.Count-1)
					{
						string key = pageNavigator.Request.ApplicationPath.TrimEnd('/','\\')+"/"+ node.Module.FormUrl;
						if( urls.Contains( key.ToUpper() ) )
						{
							pageNavigator.AddPageNavigator( name, urls[ key.ToUpper() ].ToString());
						}
						else
						{
							pageNavigator.AddPageNavigator( name, node.Module.FormUrl);
						}
						
					}
					else
					{
						pageNavigator.AddPageNavigator( name, url);
					}
				}
			}
		}
	}
}
