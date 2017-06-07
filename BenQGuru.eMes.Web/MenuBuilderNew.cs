using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;

//using Infragistics.WebUI.UltraWebNavigator;
using Infragistics.Web.UI.NavigationControls;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.ReportView;
using BenQGuru.eMES.ReportView;
using BenQGuru.eMES.Web.Helper;
using System.Web.UI;

namespace BenQGuru.eMES.Web
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
    public class MenuBuilderNew
    {
        private string Menu_Prefix = "menu_";
        private string Module_Prefix = "module_";
        public Page currentPage;
        public string UserName;

        public MenuBuilderNew()
        {

        }

        public void Build(WebDataMenu ultraMenu, ControlLibrary.Web.Language.LanguageComponent languageComponent, IDomainDataProvider _domainDataProvider)
        {
            this.GetXMLMenu();	//获取不要显示的模块
            this.GetUnVisibilityMenu(_domainDataProvider);

            if (ultraMenu == null)
            {
                return;
            }

            ultraMenu.Items.Clear();

            //BenQGuru.eMES.Common.Domain.IDomainDataProvider _domainDataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider();     
            SystemSettingFacade facade = new SystemSettingFacade(_domainDataProvider);

            #region 添加报表平台菜单入口

            //获得系统参数里面的报表平台菜单入口
            string reportViewMenuCode = string.Empty;
            Domain.BaseSetting.Parameter parameter = (Domain.BaseSetting.Parameter)facade.GetParameter("REPORTMENU", "REPORTMENU");
            if (parameter != null)
            {
                reportViewMenuCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(parameter.ParameterAlias));
            }

            //获得系统参数里面的报表平台菜单Item
            DataMenuItem reportViewMenuItem = GetReportViewMenuRoot(reportViewMenuCode, languageComponent, _domainDataProvider);

            #endregion

            ITreeObjectNode rootNode = facade.BuildMenuTree();

            TreeObjectNodeSet set = rootNode.GetSubLevelChildrenNodes();
            foreach (MenuTreeNode node in set)
            {
                if (node.MenuWithUrl.MenuType.ToUpper() == MenuType.MenuType_BS.ToUpper())
                {
                    if (this.menuHT != null && this.menuHT.Contains(node.MenuWithUrl.ModuleCode))
                    {
                        continue;
                    }
                    if (this.htUnVisibilityMenu != null && this.htUnVisibilityMenu.Contains(node.MenuWithUrl.MenuCode))
                        continue;
                    ultraMenu.Items.Add(BuildUltraMenuItem(node, languageComponent, reportViewMenuCode, reportViewMenuItem));
                }
            }

            DataMenuItem item = new DataMenuItem();
            
            item.Text = "";
            item.NavigateUrl = "#";
    
            ultraMenu.Items.Add(item);

            if (_domainDataProvider != null)
            {
                ((SQLDomainDataProvider)_domainDataProvider).PersistBroker.CloseConnection();
            }
        }

        public DataMenuItem GetReportViewMenuRoot(string reportViewMenuCode, ControlLibrary.Web.Language.LanguageComponent languageComponent, IDomainDataProvider domainDataProvider)
        {
            if (reportViewMenuCode.Trim().Length <= 0)
            {
                return null;
            }

            DataMenuItem returnValue = new DataMenuItem();

            //从tblrptventry中获得报表平台的所有菜单、文字显示、父子关系
            ReportViewFacade reportViewFacade = new ReportViewFacade(domainDataProvider);
            object[] entryArray = reportViewFacade.QueryRptViewEntryForMenu(string.Empty);
            if (entryArray != null)
            {
                foreach (RptViewEntry entry in entryArray)
                {
                    if (entry.ParentEntryCode == null)
                    {
                        entry.ParentEntryCode = string.Empty;
                    }
                }

                //使用一个递归函数，加载报表平台菜单          
                AddMenuItemFromRptViewEntryList(returnValue, entryArray, string.Empty);
            }

            return returnValue;
        }

        private void AddMenuItemFromRptViewEntryList(DataMenuItem rootItem, object[] entryArray, string parentCode)
        {
            foreach (RptViewEntry entry in entryArray)
            {
                if (entry.Visible == FormatHelper.TRUE_STRING && string.Compare(entry.ParentEntryCode, parentCode, true) == 0)
                {
                    entry.Visible = FormatHelper.FALSE_STRING;

                    DataMenuItem item = new DataMenuItem();
                    item.Text = entry.EntryName;
                    if (entry.EntryType == ReportEntryType.Report)
                        item.NavigateUrl = "ReportView/FRptViewMP.aspx?reportid=" + entry.ReportID.ToString();
                    //item.Style.Width = new Unit(180);
                    item.Target = "frmWorkSpace";

                    AddMenuItemFromRptViewEntryList(item, entryArray, entry.EntryCode);

                    rootItem.Items.Add(item);
                }
            }
        }

        public void BuildRPT(WebDataMenu ultraMenu, ControlLibrary.Web.Language.LanguageComponent languageComponent, IDomainDataProvider _domainDataProvider)
        {
            this.GetXMLMenu();	//获取不要显示的模块
            this.GetUnVisibilityMenu(_domainDataProvider);

            if (ultraMenu == null)
            {
                return;
            }

            ultraMenu.Items.Clear();

            //BenQGuru.eMES.Common.Domain.IDomainDataProvider _domainDataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider();     
            SystemSettingFacade facade = new SystemSettingFacade(_domainDataProvider);
            ITreeObjectNode rootNode = facade.BuildMenuTreeRPT();

            TreeObjectNodeSet set = rootNode.GetSubLevelChildrenNodes();

            foreach (MenuTreeNode node in set)
            {
                if (node.MenuWithUrl.MenuType.ToUpper() == MenuType.MenuType_RPT.ToUpper())
                {
                    if (this.menuHT != null && this.menuHT.Contains(node.MenuWithUrl.ModuleCode))
                    {
                        continue;
                    }
                    if (this.htUnVisibilityMenu != null && this.htUnVisibilityMenu.Contains(node.MenuWithUrl.MenuCode))
                        continue;

                    ultraMenu.Items.Add(BuildUltraMenuItemRPT(node, languageComponent));
                }
            }

            DataMenuItem item = new DataMenuItem();

            item.Text = "";
            item.NavigateUrl = "#";

            ultraMenu.Items.Add(item);

            if (_domainDataProvider != null)
            {
                ((SQLDomainDataProvider)_domainDataProvider).PersistBroker.CloseConnection();
            }
        }
        public void BuildRPTNew(WebDataMenu ultraMenu, ControlLibrary.Web.Language.LanguageComponent languageComponent, IDomainDataProvider _domainDataProvider)
        {
            this.GetXMLMenu();	//获取不要显示的模块
            this.GetUnVisibilityMenu(_domainDataProvider);

            if (ultraMenu == null)
            {
                return;
            }

            ultraMenu.Items.Clear();

            //BenQGuru.eMES.Common.Domain.IDomainDataProvider _domainDataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider();     
            SystemSettingFacade facade = new SystemSettingFacade(_domainDataProvider);
            
            #region 添加报表平台菜单入口

            //原：获得系统参数里面的报表平台菜单入口，现报表平台中的报表平台菜单入口：RPT_REPORTMENU
            string reportViewMenuCode = "RPT_REPORTMENU";
            //Domain.BaseSetting.Parameter parameter = (Domain.BaseSetting.Parameter)facade.GetParameter("REPORTMENU", "REPORTMENU");
            //if (parameter != null)
            //{
            //    reportViewMenuCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(parameter.ParameterAlias));
            //}

            //获得报表平台菜单Item
            DataMenuItem reportViewMenuItem = GetReportViewMenuRoot(reportViewMenuCode, languageComponent, _domainDataProvider);

            #endregion
            ITreeObjectNode rootNode = facade.BuildMenuTreeRPT();

            TreeObjectNodeSet set = rootNode.GetSubLevelChildrenNodes();

            foreach (MenuTreeNode node in set)
            {
                if (node.MenuWithUrl.MenuType.ToUpper() == MenuType.MenuType_RPT.ToUpper())
                {
                    if (this.menuHT != null && this.menuHT.Contains(node.MenuWithUrl.ModuleCode))
                    {
                        continue;
                    }
                    if (this.htUnVisibilityMenu != null && this.htUnVisibilityMenu.Contains(node.MenuWithUrl.MenuCode))
                        continue;

                    ultraMenu.Items.Add(BuildUltraMenuItemRPTNew(node, languageComponent, reportViewMenuCode, reportViewMenuItem));
                }
            }

            DataMenuItem item = new DataMenuItem();

            item.Text = "";
            item.NavigateUrl = "#";

            ultraMenu.Items.Add(item);

            if (_domainDataProvider != null)
            {
                ((SQLDomainDataProvider)_domainDataProvider).PersistBroker.CloseConnection();
            }
        }
        private DataMenuItem BuildUltraMenuItem(MenuTreeNode node, ControlLibrary.Web.Language.LanguageComponent languageComponent, string reportViewMenuCode, DataMenuItem reportViewMenuItem)
        {
            DataMenuItem item = new DataMenuItem();

            //item.Style.Width = new Unit(180);
            item.Target = "frmWorkSpace";

            string menuName = languageComponent.GetString(Menu_Prefix + node.MenuWithUrl.MenuCode);

            if (menuName == string.Empty)
            {
                menuName = languageComponent.GetString(Module_Prefix + node.MenuWithUrl.ModuleCode);

                if (menuName == string.Empty)
                {
                    menuName = node.MenuWithUrl.MenuCode;
                }
            }

            item.Text = menuName;

            item.NavigateUrl = node.MenuWithUrl.FormUrl;

            TreeObjectNodeSet set = node.GetSubLevelChildrenNodes();

            foreach (MenuTreeNode subNode in set)
            {
                if (subNode.MenuWithUrl.MenuType.ToUpper() == MenuType.MenuType_BS.ToUpper())
                {
                    if (this.menuHT != null && this.menuHT.Contains(subNode.MenuWithUrl.ModuleCode))
                    {
                        continue;
                    }
                    if (this.htUnVisibilityMenu != null && this.htUnVisibilityMenu.Contains(subNode.MenuWithUrl.MenuCode))
                        continue;
                    item.Items.Add(BuildUltraMenuItem(subNode, languageComponent, reportViewMenuCode, reportViewMenuItem));
                }
            }

            if (string.Compare(node.MenuWithUrl.MenuCode, reportViewMenuCode, true) == 0 && reportViewMenuItem != null)
            {
                foreach (DataMenuItem reportViewitem in reportViewMenuItem.Items)
                {
                    item.Items.Add(reportViewitem);
                }
            }

            return item;
        }

        private DataMenuItem BuildUltraMenuItemRPT(MenuTreeNode node, ControlLibrary.Web.Language.LanguageComponent languageComponent)
        {
            DataMenuItem item = new DataMenuItem();

            //item.Style.Width = new Unit(180);
            item.Target = "frmWorkSpace";
     
            string menuName = languageComponent.GetString(Menu_Prefix + node.MenuWithUrl.MenuCode);

            if (menuName == string.Empty)
            {
                menuName = languageComponent.GetString(Module_Prefix + node.MenuWithUrl.ModuleCode);

                if (menuName == string.Empty)
                {
                    menuName = node.MenuWithUrl.MenuCode;
                }
            }

            item.Text = menuName;

            item.NavigateUrl = node.MenuWithUrl.FormUrl;

            TreeObjectNodeSet set = node.GetSubLevelChildrenNodes();

            foreach (MenuTreeNode subNode in set)
            {
                if (subNode.MenuWithUrl.MenuType.ToUpper() == MenuType.MenuType_RPT.ToUpper())
                {
                    if (this.menuHT != null && this.menuHT.Contains(subNode.MenuWithUrl.ModuleCode))
                    {
                        continue;
                    }
                    if (this.htUnVisibilityMenu != null && this.htUnVisibilityMenu.Contains(subNode.MenuWithUrl.MenuCode))
                        continue;
                    item.Items.Add(BuildUltraMenuItemRPT(subNode, languageComponent));
                }
            }

            return item;
        }

        private DataMenuItem BuildUltraMenuItemRPTNew(MenuTreeNode node, ControlLibrary.Web.Language.LanguageComponent languageComponent, string reportViewMenuCode, DataMenuItem reportViewMenuItem)
        {
            DataMenuItem item = new DataMenuItem();

            //item.Style.Width = new Unit(180);
            item.Target = "frmWorkSpace";

            string menuName = languageComponent.GetString(Menu_Prefix + node.MenuWithUrl.MenuCode);

            if (menuName == string.Empty)
            {
                menuName = languageComponent.GetString(Module_Prefix + node.MenuWithUrl.ModuleCode);

                if (menuName == string.Empty)
                {
                    menuName = node.MenuWithUrl.MenuCode;
                }
            }

            item.Text = menuName;

            item.NavigateUrl = node.MenuWithUrl.FormUrl;

            TreeObjectNodeSet set = node.GetSubLevelChildrenNodes();

            foreach (MenuTreeNode subNode in set)
            {
                if (subNode.MenuWithUrl.MenuType.ToUpper() == MenuType.MenuType_RPT.ToUpper())
                {
                    if (this.menuHT != null && this.menuHT.Contains(subNode.MenuWithUrl.ModuleCode))
                    {
                        continue;
                    }
                    if (this.htUnVisibilityMenu != null && this.htUnVisibilityMenu.Contains(subNode.MenuWithUrl.MenuCode))
                        continue;
                    item.Items.Add(BuildUltraMenuItemRPTNew(subNode, languageComponent, reportViewMenuCode, reportViewMenuItem));
                }
            }

            if (string.Compare(node.MenuWithUrl.MenuCode, reportViewMenuCode, true) == 0 && reportViewMenuItem != null)
            {
                foreach (DataMenuItem reportViewitem in reportViewMenuItem.Items)
                {
                    item.Items.Add(reportViewitem);
                }
            }

            return item;
        }
        public Hashtable menuHT;

        public void GetXMLMenu()
        {
            try
            {
                if (menuHT != null) return;
                if (currentPage == null) return;

                menuHT = new Hashtable();
                string strFile = currentPage.MapPath("MenuRight.xml");
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(strFile);

                #region 加载版本限制Menu (将不会显示)

                System.Xml.XmlNode node = doc.SelectSingleNode(string.Format("//MenuRightVersion"));
                if (node != null)
                {
                    System.Xml.XmlNodeList selectNodes = node.SelectNodes("ModuleCode");
                    if (selectNodes != null && selectNodes.Count > 0)
                        foreach (System.Xml.XmlNode _childnode in selectNodes)
                        {
                            string disply = _childnode.Attributes["Disply"].Value.Trim().ToUpper();
                            if (disply == "FALSE")
                            {
                                string code = _childnode.Attributes["Code"].Value;
                                if (!menuHT.Contains(code))
                                    menuHT.Add(code, code);
                            }
                        }
                }
                node = null;

                #endregion

                #region 加载限制Menu (只有Admin才可以访问,其它用户不会显示)

                if (this.UserName != null && this.UserName.ToUpper() != "ADMIN")
                {
                    System.Xml.XmlNode node2 = doc.SelectSingleNode(string.Format("//MenuRightAdmin"));
                    if (node2 != null)
                    {
                        System.Xml.XmlNodeList selectNodes = node2.SelectNodes("ModuleCode");
                        if (selectNodes != null && selectNodes.Count > 0)
                            foreach (System.Xml.XmlNode _childnode in selectNodes)
                            {
                                string disply = _childnode.Attributes["Disply"].Value.Trim().ToUpper();
                                if (disply == "FALSE")
                                {
                                    string code = _childnode.Attributes["Code"].Value;
                                    if (!menuHT.Contains(code))
                                        menuHT.Add(code, code);
                                }
                            }
                    }
                    node2 = null;


                }
                #endregion

                doc = null;
            }
            catch
            { }
        }

        private Hashtable htUnVisibilityMenu;
        private void GetUnVisibilityMenu(IDomainDataProvider _domainDataProvider)
        {
            htUnVisibilityMenu = new Hashtable();
            SystemSettingFacade sysFacade = new SystemSettingFacade(_domainDataProvider);
            object[] objs = sysFacade.GetAllMenuUnVisibility(MenuType.MenuType_BS);
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
