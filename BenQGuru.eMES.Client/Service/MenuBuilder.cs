using System;
using System.Collections;
using System.Drawing;
using System.Globalization ;

using BenQGuru.eMES.Client.Command;
using BenQGuru.eMES.Client.Menu;
using BenQGuru.eMES.Client;
using BenQGuru.eMES.Security;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.Client.Service
{
	/// <summary>
	/// MenuBuilder 的摘要说明。
	/// 文件名:
	///		MenuBuilder.aspx.cs
	/// Copyright (c) 
	///		1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
	/// 创建人:
	///		Jane Shu
	/// 创建日期:
	///		2005-3-18
	/// 修改人:
	///		
	/// 修改日期:
	///		
	/// 描 述: 
	///		构建Menu
	/// 版 本:	
	///		1.0.0
	/// </summary>
	public class MenuBuilder
	{			
		private  string Menu_Prefix = "menu_";		
		private  string Module_Prefix = "module_";

		public MenuBuilder()
		{
		
		}

		public IMenu[] Build()
		{
			this.GetUnVisibilityMenu(ApplicationService.Current().DataProvider);
			
			SystemSettingFacade facade =new SystemSettingFacade(ApplicationService.Current().DataProvider);
            bool bIsAdmin = false;
			string strUserCode = ApplicationService.Current().UserCode;
            if (strUserCode.ToUpper() == "ADMIN")
            {
                bIsAdmin = true;
            }
            else
            {
                for (int i = 0; ApplicationService.Current().LoginInfo.UserGroups != null && i < ApplicationService.Current().LoginInfo.UserGroups.Length; i++)
                {
                    if (((UserGroup)ApplicationService.Current().LoginInfo.UserGroups[i]).UserGroupType == "ADMIN")
                    {
                        bIsAdmin = true;
                        break;
                    }
                }
            }
            if (bIsAdmin == true)
                strUserCode = string.Empty;

			ITreeObjectNode rootNode = facade.BuildMenuTreeCS(strUserCode);
	
			TreeObjectNodeSet set = rootNode.GetSubLevelChildrenNodes();

			ArrayList listMenu = new ArrayList();
			foreach ( MenuTreeNode node in set )
			{
				if( node.MenuWithUrl.MenuType.ToUpper() == MenuType.MenuType_CS.ToUpper() )
				{
					if(this.menuHT != null && this.menuHT.Contains(node.MenuWithUrl.ModuleCode))
					{
						continue;
					}
					if (this.htUnVisibilityMenu != null && this.htUnVisibilityMenu.Contains(node.MenuWithUrl.MenuCode))
						continue;
					listMenu.Add( BuildUltraMenuItem(node) );
				}
			}
			
			if (listMenu.Count > 0)
			{
				MenuCommand[] menuList = new MenuCommand[listMenu.Count];
				listMenu.CopyTo(menuList);
				return menuList;
			}
			return null;
		}

		private  IMenu BuildUltraMenuItem( MenuTreeNode node )
		{

			string menuName = UserControl.MutiLanguages.ParserString( Menu_Prefix + node.MenuWithUrl.MenuCode );

			if ( menuName == string.Empty )
			{
				menuName = UserControl.MutiLanguages.ParserString( Module_Prefix + node.MenuWithUrl.ModuleCode );

				if ( menuName == string.Empty )
				{
					menuName = node.MenuWithUrl.MenuCode;
				}
			}
			
			string strUrl = node.MenuWithUrl.FormUrl;
			string strKey = strUrl;
			if (strKey == string.Empty)
				strKey = node.MenuWithUrl.MenuCode;
			MenuCommand item = new MenuCommand(strKey, menuName, 0, new CommandOpenForm(strUrl));

			TreeObjectNodeSet set = node.GetSubLevelChildrenNodes();

			ArrayList listSubMenu = new ArrayList();
			foreach ( MenuTreeNode subNode in set )
			{
				if( subNode.MenuWithUrl.MenuType.ToUpper() == MenuType.MenuType_CS.ToUpper() )
				{
					if(this.menuHT != null && this.menuHT.Contains(subNode.MenuWithUrl.ModuleCode))
					{
						continue;
					}
					if (this.htUnVisibilityMenu != null && this.htUnVisibilityMenu.Contains(subNode.MenuWithUrl.MenuCode))
						continue;
					listSubMenu.Add( BuildUltraMenuItem(subNode) );
				}
			}
			if (listSubMenu.Count > 0)
			{
				MenuCommand[] subMenu = new MenuCommand[listSubMenu.Count];
				listSubMenu.CopyTo(subMenu);
				item.SubMenus = subMenu;
			}

			return item;
		}

		public  Hashtable menuHT ;

		public  void GetXMLMenu()
		{
			try
			{
				if(menuHT != null) return;

				menuHT = new Hashtable();
				string strFile = System.Windows.Forms.Application.StartupPath + "\\MenuRight.xml";
				if (System.IO.File.Exists(strFile) == false)
					return;
				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
				doc.Load(strFile);

				#region 加载版本限制Menu (将不会显示)

				System.Xml.XmlNode node = doc.SelectSingleNode(string.Format("//MenuRightVersion"));
				if (node != null)
				{
					System.Xml.XmlNodeList selectNodes = node.SelectNodes("ModuleCode");
					if(selectNodes!=null && selectNodes.Count>0)
						foreach(System.Xml.XmlNode _childnode in selectNodes)
						{
							string disply = _childnode.Attributes["Disply"].Value.Trim().ToUpper();
							if(disply == "FALSE")
							{
								string code = _childnode.Attributes["Code"].Value;
								if(!menuHT.Contains(code))
									menuHT.Add(code,code);
							}
						}
				}
				node = null;

				#endregion

				#region 加载限制Menu (只有Admin才可以访问,其它用户不会显示)

				if(ApplicationService.Current().UserCode !=null && ApplicationService.Current().UserCode.ToUpper() != "ADMIN")
				{
					System.Xml.XmlNode node2 = doc.SelectSingleNode(string.Format("//MenuRightAdmin"));
					if (node2 != null)
					{
						System.Xml.XmlNodeList selectNodes = node2.SelectNodes("ModuleCode");
						if(selectNodes!=null && selectNodes.Count>0)
							foreach(System.Xml.XmlNode _childnode in selectNodes)
							{
								string disply = _childnode.Attributes["Disply"].Value.Trim().ToUpper();
								if(disply == "FALSE")
								{
									string code = _childnode.Attributes["Code"].Value;
									if(!menuHT.Contains(code))
										menuHT.Add(code,code);
								}
							}
					}
					node2 = null;
				

				}
				#endregion

				doc = null;
			}
			catch
			{}
		}

		private Hashtable htUnVisibilityMenu;
		private void GetUnVisibilityMenu(IDomainDataProvider _domainDataProvider)
		{
			htUnVisibilityMenu = new Hashtable();
			SystemSettingFacade sysFacade = new SystemSettingFacade(_domainDataProvider);
			object[] objs = sysFacade.GetAllMenuUnVisibility(MenuType.MenuType_CS);
			if (objs != null)
			{
				for (int i = 0; i < objs.Length; i++)
				{
					htUnVisibilityMenu.Add(((BenQGuru.eMES.Domain.BaseSetting.Menu)objs[i]).MenuCode, ((BenQGuru.eMES.Domain.BaseSetting.Menu)objs[i]).MenuCode);
				}
			}
		}
	}
}
