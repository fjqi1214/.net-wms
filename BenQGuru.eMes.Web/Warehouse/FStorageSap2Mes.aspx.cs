using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.IQC;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
//using BenQGuru.eMES.CodeSoftPrint;
using UserControl;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FStorageSap2Mes : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private WarehouseFacade _WarehouseFacade = null;
        private UserFacade _UserFacade = null;
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
            UnLoadPageLoad();
        }
        #endregion

        #region Init

        protected void Page_Load(object sender, System.EventArgs e)
        {
           
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }


      
        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("MCODE", "物料编码", null);
            this.gridHelper.AddColumn("DQMCODE", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("SapStorageCode", "SAP库位", null);
            this.gridHelper.AddColumn("MesStorageCode", "MES库位", null);
            this.gridHelper.AddColumn("SapStorageQty", "SAP数量", null);
            this.gridHelper.AddColumn("MesStorageQty", "MES数量", null);
            this.gridHelper.AddColumn("DisQty", "差异数量", null);
            this.gridHelper.AddColumn("MUser", "维护人", null);
            this.gridHelper.AddColumn("MDate", "维护日期", null);
            this.gridHelper.AddColumn("MTime", "维护时间", null);
            this.gridHelper.AddDefaultColumn(true, false);
            
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();

            row["MCODE"] = ((Storagesap2mes)obj).Mcode;
            row["DQMCODE"] = ((Storagesap2mes)obj).Dqmcode;
            row["SapStorageCode"] = ((Storagesap2mes)obj).Sapstoragecode;
            row["MesStorageCode"] = ((Storagesap2mes)obj).Messtoragecode;
            row["SapStorageQty"] = ((Storagesap2mes)obj).Sapqty;
            row["MesStorageQty"] = ((Storagesap2mes)obj).Mesqty;
            row["DisQty"] = ((Storagesap2mes)obj).Disqty;
            row["MUser"] = ((Storagesap2mes)obj).MaintainUser;
            row["MDate"] = ((Storagesap2mes)obj).MaintainDate;
            row["MTime"] = ((Storagesap2mes)obj).MaintainTime;
            return row;
        }

   

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }

            return this._WarehouseFacade.QueryStoragesap2mes(
           FormatHelper.CleanString(this.txtMCodeQurey.Text.ToUpper()),
           inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            return this._WarehouseFacade.QueryStoragesap2mesCount(
                     FormatHelper.CleanString(this.txtMCodeQurey.Text.ToUpper())
                  );
        }

        #endregion

     

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{
                          
               ((Storagesap2mes)obj).Mcode,
                 ((Storagesap2mes)obj).Dqmcode,
                ((Storagesap2mes)obj).Sapstoragecode,
               ((Storagesap2mes)obj).Messtoragecode,
                ((Storagesap2mes)obj).Sapqty.ToString(),
                ((Storagesap2mes)obj).Mesqty.ToString(),
                ((Storagesap2mes)obj).Disqty.ToString(),
               ((Storagesap2mes)obj).MaintainUser,
               FormatHelper.ToDateString(((Storagesap2mes)obj).MaintainDate),
                FormatHelper.ToTimeString(((Storagesap2mes)obj).MaintainTime)
                               };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[]
                {
                              "MCODE"  ,            
                            "DQMCODE",
                            "SapStorageCode"     , 
                            "MesStorageCode"     , 
                            "SapQty"              ,
                            "MesQty"              ,
                            "DisQty"              ,
                            "MUser"               ,
                            "MDate"               ,
                            "MTime" 
                };
        }

        #endregion


        #region Button

        #endregion

         
    



         

    }
}
