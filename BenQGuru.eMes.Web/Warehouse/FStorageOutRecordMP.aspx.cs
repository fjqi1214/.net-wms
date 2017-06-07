using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FStorageOutRecordMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private InventoryFacade _InventoryFacade = null;
        private SystemSettingFacade _SystemSettingFacade = null;


        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion

        #region Init

        protected void Page_Load(object sender, System.EventArgs e)
        {
            _SystemSettingFacade = new SystemSettingFacade(this.DataProvider);

            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                InitStorageList();



                object[] parameters = _SystemSettingFacade.GetParametersByParameterGroup("PICKTYPE");
                this.drpPickTypeQuery.Items.Add(new ListItem("", ""));
                foreach (Domain.BaseSetting.Parameter parameter in parameters)
                {
                    this.drpPickTypeQuery.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterAlias));
                }
                this.drpPickTypeQuery.SelectedIndex = 0;
            }
        }
        #region  单据类型下拉框

        private void InitStorageList()
        {

            InventoryFacade facade = new InventoryFacade(base.DataProvider);

            this.drpStorageQuery.Items.Add(new ListItem("", ""));
            object[] objStorage = facade.GetAllStorage();
            if (objStorage != null && objStorage.Length > 0)
            {
                foreach (Storage storage in objStorage)
                {

                    this.drpStorageQuery.Items.Add(new ListItem(
                         storage.StorageName + "-" + storage.StorageCode, storage.StorageCode)
                        );
                }
            }
            this.drpStorageQuery.SelectedIndex = 0;
        }

        //private void InitStorageList()
        //{
        //    SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
        //    UserFacade userFacade = new UserFacade(this.DataProvider);
        //    string[] usergroupList = userFacade.GetUserGroupCodeofUser("ADMIN");//GetUserCode()
        //    this.drpStorageQuery.Items.Add(new ListItem("", ""));
        //    object[] parameters = systemSettingFacade.GetDistinctParaInParameterGroup(usergroupList);
        //    if (parameters != null)
        //    {
        //        foreach (Domain.BaseSetting.Parameter parameter in parameters)
        //        {
        //            drpStorageQuery.Items.Add(new ListItem(parameter.ParameterCode + "-" + parameter.ParameterDescription, parameter.ParameterCode));
        //        }
        //    }
        //    this.drpStorageQuery.SelectedIndex = 0;

        //}
        #endregion
        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("StorageNo", "库位", null);
            this.gridHelper.AddColumn("PickNo", "拣货任务令号", null);
            this.gridHelper.AddColumn("MaterialNo", "物料编码", null);
            this.gridHelper.AddColumn("PickID", "拣料ID", null);
            this.gridHelper.AddColumn("PickFinishDate", "拣料完成日期", null);
            this.gridHelper.AddColumn("PackID", "包装ID", null);
            this.gridHelper.AddColumn("PackFinishDate", "包装完成日期", null);
            this.gridHelper.AddColumn("OQCID", "OQC操作ID", null);
            this.gridHelper.AddColumn("OQCFinishDate", "OQC完成日期", null);
            this.gridHelper.AddColumn("PackingListID", "制作箱单ID", null);
            this.gridHelper.AddColumn("PackingListFinishDate", "箱单完成日期", null);
            this.gridHelper.AddColumn("SendCaseID", "发货ID", null);
            this.gridHelper.AddColumn("SendCaseFinishDate", "发货完成时间", null);

            this.gridHelper.AddDefaultColumn(false, false);



            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            InventoryFacade facade = new InventoryFacade(this.DataProvider);
            PickQuery pick = obj as PickQuery;
            if (pick == null)
            {
                return null;
            }
            row["StorageNo"] = pick.StorageCode;
            row["PickNo"] = pick.PickNo;
            row["MaterialNo"] = pick.MCode;

            row["PickID"] = GetTransUser(facade, pick, "ClosePick");// ;
            row["PickFinishDate"] = facade.MaxOutDate(pick.PickNo, pick.MCode, "ClosePick");


            row["PackID"] = GetTransUser(facade, pick, "ClosePack");// ;
            row["PackFinishDate"] = facade.MaxOutDate(pick.PickNo, pick.MCode, "ClosePack");//包装完成日期



            row["OQCID"] = GetTransUser(facade, pick, "OQC");// ;
            row["OQCFinishDate"] = facade.MaxOutDate(pick.PickNo, pick.MCode, "OQC");


            row["PackingListID"] = GetTransUser(facade, pick, "ClosePackingList");// 
            row["PackingListFinishDate"] = facade.MaxOutDate(pick.PickNo, pick.MCode, "ClosePackingList");//箱单完成日期 


            row["SendCaseID"] = GetTransUser(facade, pick, "SendCase");// 
            row["SendCaseFinishDate"] = facade.MaxOutDate(pick.PickNo, pick.MCode, "SendCase"); //发货完成时间
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            return this._InventoryFacade.QueryStorageOut(drpPickTypeQuery.SelectedValue, txtInvNoQuery.Text,
                FormatHelper.CleanString(this.txtPickNoQuery.Text),
                FormatHelper.CleanString(this.drpStorageQuery.SelectedValue),
                       FormatHelper.TODateInt(this.dateInDateFromQuery.Text),
                    FormatHelper.TODateInt(this.dateInDateToQuery.Text),
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            return this._InventoryFacade.QueryStorageOutCount(drpPickTypeQuery.SelectedValue, txtInvNoQuery.Text,
                FormatHelper.CleanString(this.txtPickNoQuery.Text),
                FormatHelper.CleanString(this.drpStorageQuery.SelectedValue),
                       FormatHelper.TODateInt(this.dateInDateFromQuery.Text),
                    FormatHelper.TODateInt(this.dateInDateToQuery.Text));
        }

        #endregion

        #region Button


        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            InventoryFacade facade = new InventoryFacade(this.DataProvider);
            PickQuery pick = obj as PickQuery;
            if (pick == null)
            {
                return null;
            }

            return new string[]{
                          pick.StorageCode,
                          pick.PickNo,
                        pick.MCode,
                         GetTransUser(facade, pick, "ClosePick"),
                                facade.MaxOutDate(pick.PickNo, pick.MCode, "ClosePick").ToString(),
                           GetTransUser(facade, pick, "OQC"),
                                facade.MaxOutDate(pick.PickNo, pick.MCode, "OQC").ToString(),
                          GetTransUser(facade, pick, "ClosePack"),
                                facade.MaxOutDate(pick.PickNo, pick.MCode, "ClosePack").ToString(),
                                 GetTransUser(facade, pick, "ClosePackingList"),
                                facade.MaxOutDate(pick.PickNo, pick.MCode, "ClosePackingList").ToString(),
                                 GetTransUser(facade, pick, "SendCase"),
                                facade.MaxOutDate(pick.PickNo, pick.MCode, "SendCase").ToString() };
        }

        private string GetTransUser(InventoryFacade facade, PickQuery pick, string processType)
        {
            object[] userList = facade.GetTransUser(pick.PickNo, pick.MCode, processType);
            string user = "";
            if (userList != null)
            {
                foreach (InvInOutTrans inv in userList)
                {
                    user += inv.MaintainUser + ",";
                }
                user = user.TrimEnd(',');
            }
            return user;
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	           "StorageNo",                  
            "PickNo",                    
            "MaterialNo",                
            "PickID",                    
            "PickFinishDate",            
            "PackID",                    
            "PackFinishDate",            
            "OQCID",                     
            "OQCFinishDate",             
            "PackingListID",             
            "PackingListFinishDate",     
            "SendCaseID",                
            "SendCaseFinishDate"       };
        }

        #endregion

    }
}
