using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.SPC;
using System.Collections.Generic;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Domain.OQC;

namespace BenQGuru.eMES.Web.MOModel
{
    /// <summary>
    /// FSPCTestItemStoreSP 的摘要说明。
    /// </summary>
    public partial class FSPCTestItemStoreSP : BaseMPageMinus
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        //private GridHelperNew gridHelper = null;
        private ButtonHelper buttonHelper = null;
        private BenQGuru.eMES.MOModel.SPCFacade _facade;
        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;

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
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            this.txtCKGroup.TextBox.TextChanged += new EventHandler(this.txtCKGroup_TextChanged);
            //this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);

            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
            // 
            // excelExporter
            // 
            this.excelExporter.FileExtension = "xls";
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.Page = this;
            this.excelExporter.RowSplit = "\r\n";

        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            InitHander();
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.txtObjectCode.Text = this.GetRequestParam("objectcode").Trim().ToUpper();
                if (_facade == null) { _facade = new SPCFacade(this.DataProvider); }
                object objSpc = _facade.GetSPCObject(this.txtObjectCode.Text);
                this.txtObjectName.Text = ((SPCObject)objSpc).ObjectName;

                // 初始化界面UI
                this.InitUI();
                this.InitButton();
                this.InitWebGrid();
                this.RequestData();


            }
        }

        #endregion

        #region WebGrid
        protected void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("Serial", "序号", null);
            this.gridHelper.AddColumn("SPCControlItem", "管控项目", null);
            this.gridHelper.AddColumn("ObjectName", "项目名称", null);
            this.gridHelper.AddColumn("GroupSeq", "测试组次", null);
            this.gridHelper.AddColumn("CheckGroup", "检验项目组", null);
            this.gridHelper.AddColumn("CHECKITEMLIST", "检验项目", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);

            this.gridHelper.AddDefaultColumn(true, false);
            this.gridWebGrid.Columns.FromKey("Serial").Hidden = true;

            this.gridHelper.ApplyLanguage(this.languageComponent1);

        }

        protected  DataRow GetGridRow(object obj)
        {
            SPCObjectStore spcObj = (SPCObjectStore)obj;
            //spcObj.eAttribute1 = this.txtObjectName.Text;
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    spcObj.Serial,
            //                    this.txtObjectCode.Text,
            //                    this.txtObjectName.Text,
            //                    spcObj.GroupSeq,
            //                    spcObj.CKGroup,
            //                    spcObj.CKItemCode,
            //                    spcObj.MaintainUser,
            //                    FormatHelper.ToDateString(spcObj.MaintainDate),
            //                    FormatHelper.ToTimeString(spcObj.MaintainTime),
            //                    ""});
            DataRow row = this.DtSource.NewRow();
            row["Serial"] = spcObj.Serial;
            row["SPCControlItem"] = this.txtObjectCode.Text;
            row["ObjectName"] = this.txtObjectName.Text;
            row["GroupSeq"] = spcObj.GroupSeq;
            row["CheckGroup"] = spcObj.CKGroup;
            row["CHECKITEMLIST"] = spcObj.CKItemCode;
            row["MaintainUser"] = spcObj.MaintainUser;
            row["MaintainDate"] = FormatHelper.ToDateString(spcObj.MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(spcObj.MaintainTime);
            return row;
        }

        protected  object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null) { _facade = new SPCFacade(this.DataProvider); }
            return this._facade.QuerySPCObjectStore(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtObjectCode.Text)),
                inclusive, exclusive);
        }

        protected  int GetRowCount()
        {
            if (_facade == null) { _facade = new SPCFacade(base.DataProvider); }
            return this._facade.QuerySPCObjectStoreCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtObjectCode.Text))
                );
        }

        #endregion

        #region Button
        private void InitHander()
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
            this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }

        public void InitButton()
        {
            this.buttonHelper.AddDeleteConfirm();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
        }

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        protected  void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
        {
            if (this.ValidateInput())
            {
                if (!CheckSPCObjectStore())
                {
                    return;
                }
                List<SPCObjectStore> objs = this.GetEditObjects();

                if (objs == null)
                {
                    return;
                }

                if (_facade == null) { _facade = new SPCFacade(base.DataProvider); }
                foreach (SPCObjectStore obj in objs)
                {
                    this._facade.AddSPCObjectStore(obj);
                }

                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            }

        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this.buttonHelper.PageActionStatusHandle(PageActionType.Cancel);
        }

        //protected void chbSelectAll_CheckedChanged(object sender, System.EventArgs e)
        //{
        //    if (this.chbSelectAll.Checked)
        //    {
        //        this.gridHelper.CheckAllRows(CheckStatus.Checked);
        //    }
        //    else
        //    {
        //        this.gridHelper.CheckAllRows(CheckStatus.Unchecked);
        //    }
        //}

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this.excelExporter.Export();
        }

        protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
        {
            if (_facade == null) { _facade = new FacadeFactory(base.DataProvider).CreateSPCFacade(); }
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList objs = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    object obj = this.GetEditObject(row);
                    if (obj != null)
                    {
                        objs.Add((SPCObjectStore)obj);
                    }
                }

                this._facade.DeleteSPCObjectStore((SPCObjectStore[])objs.ToArray(typeof(SPCObjectStore)));
                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Delete);
            }
        }

        //protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        //{
        //    if (this.ValidateInput())
        //    {
        //        if (CheckSPCObjectStore())
        //        {
        //            return;
        //        }
        //        object obj = this.GetEditObject();

        //        if (obj == null)
        //        {
        //            return;
        //        }

        //        if (_facade == null) { _facade = new SPCFacade(base.DataProvider); }
        //        this._facade.UpdateSPCObjectStore((SPCObjectStore)obj);


        //        this.RequestData();
        //        this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
        //    }
        //}

        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("FSPCTestItemMP.aspx");
        }

        //private void gridWebGrid_ClickCellButton(object sender, CellEventArgs e)
        //{
        //    object obj = this.GetEditObject(e.Cell.Row);

        //    if (obj != null)
        //    {
        //        this.SetEditObject(obj);

        //        this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
        //    }
        //}

        protected void txtCKGroup_TextChanged(object sender, EventArgs e)
        {
            this.txtCKItemCode.Text = "";
        }

        protected void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtGroupIndex.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtGroupIndex.ReadOnly = true;
            }

            if (pageAction == PageActionType.Cancel)
            {
                this.txtGroupIndex.ReadOnly = false;
            }
        }

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }
        #endregion

        #region Object <--> Page

        protected object GetEditObject()
        {
            if (_facade == null) { _facade = new SPCFacade(base.DataProvider); }
            SPCObjectStore spcObj = (SPCObjectStore)_facade.GetSPCObjectStore(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtObjectCode.Text)),
                                                                              Convert.ToDecimal(this.txtGroupIndex.Text),
                                                                              FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCKGroup.Text)),
                                                                              FormatHelper.CleanString(this.txtCKItemCode.Text));
            if (spcObj == null)
                spcObj = this._facade.CreateNewSPCObjectStore();

            spcObj.ObjectCode = this.txtObjectCode.Text;
            spcObj.GroupSeq = Convert.ToDecimal(this.txtGroupIndex.Text);
            spcObj.CKGroup = this.txtCKGroup.Text;
            spcObj.CKItemCode = this.txtCKItemCode.Text;
            spcObj.MaintainUser = this.GetUserCode();
            spcObj.MaintainDate = FormatHelper.TODateInt(DateTime.Today);
            spcObj.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);

            return spcObj;
        }

        protected List<SPCObjectStore> GetEditObjects()
        {
            if (_facade == null) { _facade = new SPCFacade(base.DataProvider); }

            SPCObjectStore spcObj;
            List<SPCObjectStore> spcObjList = new List<SPCObjectStore>();

            if (this.txtCKItemCode.Text == "")
            {
                OQCFacade _OQCFacade = new OQCFacade(base.DataProvider);
                object[] CKItemCodes = _OQCFacade.GetOQCCheckListByCheckGroup(this.txtCKGroup.Text);
                if (CKItemCodes != null)
                {
                    foreach (OQCCheckListQuery item in CKItemCodes)
                    {
                        spcObj = new SPCObjectStore();
                        spcObj.CKItemCode = item.CheckItemCode;

                        spcObj.ObjectCode = this.txtObjectCode.Text;
                        spcObj.GroupSeq = Convert.ToDecimal(this.txtGroupIndex.Text);
                        spcObj.CKGroup = this.txtCKGroup.Text;

                        spcObj.MaintainUser = this.GetUserCode();
                        spcObj.MaintainDate = FormatHelper.TODateInt(DateTime.Today);
                        spcObj.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                        spcObjList.Add(spcObj);
                    }
                }
            }
            else
            {
                string[] ckItemCodes = this.txtCKItemCode.Text.Split(',');
                foreach (string str in ckItemCodes)
                {
                    spcObj = new SPCObjectStore();
                    spcObj.CKItemCode = str;

                    spcObj.ObjectCode = this.txtObjectCode.Text;
                    spcObj.GroupSeq = Convert.ToDecimal(this.txtGroupIndex.Text);
                    spcObj.CKGroup = this.txtCKGroup.Text;

                    spcObj.MaintainUser = this.GetUserCode();
                    spcObj.MaintainDate = FormatHelper.TODateInt(DateTime.Today);
                    spcObj.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    spcObjList.Add(spcObj);
                }
            }

            return spcObjList;
        }

        protected object GetEditObject(GridRecord row)
        {
            if (_facade == null) { _facade = new SPCFacade(base.DataProvider); }
            object obj = _facade.GetSPCObjectStore(row.Items.FindItemByKey("Serial").Text.ToString());

            if (obj != null)
            {
                return (SPCObjectStore)obj;
            }

            return null;
        }

        protected void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtGroupIndex.Text = "0";
                this.txtCKGroup.Text = "";
                this.txtCKItemCode.Text = "";

                return;
            }

            SPCObjectStore spcObj = (SPCObjectStore)obj;
            this.txtGroupIndex.Text = spcObj.GroupSeq.ToString();
            this.txtCKGroup.Text = spcObj.CKGroup.ToString();
            this.txtCKItemCode.Text = spcObj.CKItemCode.ToString();
        }


        protected bool ValidateInput()
        {

            PageCheckManager manager = new PageCheckManager();

            manager.Add(new DecimalCheck(lblGroupIndex, txtGroupIndex, 0, decimal.MaxValue, true));
            manager.Add(new LengthCheck(lblCKGroup, txtCKGroup, 40, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;

        }

        public bool CheckSPCObjectStore()
        {
            string objectCode = this.txtObjectCode.Text;
            decimal groupSeq = Convert.ToDecimal(this.txtGroupIndex.Text);
            string ckGroup = this.txtCKGroup.Text;
            string ckItemCode = this.txtCKItemCode.Text;

            if (_facade == null) { _facade = new SPCFacade(base.DataProvider); }

            object[] objs1 = _facade.GetSPCObjectStore(objectCode, groupSeq);
            if (objs1 != null)
            {
                foreach (SPCObjectStore item1 in objs1)
                {
                    //check同一个管理项目，组次，对应唯一的检验项目组
                    if (item1.CKGroup != ckGroup)
                    {
                        WebInfoPublish.Publish(this, "$one_objectCode_and_groupSeq_must_has_one_ckGroup", this.languageComponent1);
                        return false;
                    }
                }


                object[] objs2 = _facade.GetSPCObjectStore(objectCode, groupSeq, ckGroup);
                if (objs2 != null)
                {
                    if (ckItemCode == "")
                    {
                        WebInfoPublish.Publish(this, "$Error_PK_is_Repeat", this.languageComponent1);
                        return false;
                    }

                    string[] ckItemCodes = ckItemCode.Split(',');
                    foreach (string str in ckItemCodes)
                    {
                        foreach (SPCObjectStore item2 in objs2)
                        {
                            if (item2.CKItemCode == str)
                            {
                                WebInfoPublish.Publish(this, "$Error_PK_is_Repeat", this.languageComponent1);
                                return false;
                            }
                        }
                    }
                }

            }


            return true;
        }

        #endregion

        #region Export
        protected string[] FormatExportRecord(object obj)
        {
            SPCObjectStore spcObj = (SPCObjectStore)obj;
            //spcObj.eAttribute1 = this.txtObjectName.Text;
            return new string[]{   this.txtObjectCode.Text,
								   this.txtObjectName.Text,
								   spcObj.GroupSeq.ToString(),
								   spcObj.CKGroup,
                                   spcObj.CKItemCode,
								   spcObj.MaintainUser,
								   FormatHelper.ToDateString(spcObj.MaintainDate),
								   FormatHelper.ToTimeString(spcObj.MaintainTime) };
        }

        protected string[] GetColumnHeaderText()
        {
            return new string[] {	
									"SPCControlItem",
									"ObjectName",
									"GroupSeq",
									"CheckGroup",
                                    "CHECKITEMLIST",
									"MaintainUser",	
									"MaintainDate",
									"MaintainTime"};
        }
        #endregion


    }
}
