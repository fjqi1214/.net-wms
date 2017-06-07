#region system
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
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.Domain.SPC;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
#endregion

namespace BenQGuru.eMES.Web.MOModel
{
    /// <summary>
    /// FModelMP 的摘要说明。
    /// </summary>
    public partial class FItem2SPCItemSpecMP : BaseMPageMinus
    {
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        //private GridHelper gridHelper = null;
        private SPCFacade _spcFacade;
        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
        private ButtonHelper buttonHelper = null;
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
            //this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
           // this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
            //this.drpObjectCodeEdit.SelectedIndexChanged += new EventHandler(drpObjectCodeEdit_SelectedIndexChanged);
            // 
            // languageComponent1
            // 
            //this.languageComponent1.Language = "CHS";
            //this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
            //this.languageComponent1.RuntimePage = null;
            //this.languageComponent1.RuntimeUserControl = null;
            //this.languageComponent1.UserControlName = "";
            // 
            // excelExporter
            // 
            this.excelExporter.FileExtension = "xls";
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.Page = this;
            this.excelExporter.RowSplit = "\r\n";

        }
        #endregion


        #region form events
        protected void Page_Load(object sender, System.EventArgs e)
        {
            InitHander();
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                // 初始化界面UI
                this.InitUI();
                this.InitButton();
                this.InitWebGrid();
                this.InitPanel();
            }
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
        {
            if (_spcFacade == null) { _spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade(); }
            object obj = this.GetEditObject();
            if (obj != null)
            {
                this._spcFacade.AddSPCItemSpec((SPCItemSpec)obj);
                UpdateTestCondion((SPCItemSpec)obj, false);
                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            }
        }

        protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
        {
            if (_spcFacade == null) { _spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade(); }
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList objs = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    object obj = this.GetEditObject(row);
                    if (obj != null)
                    {
                        objs.Add((SPCItemSpec)obj);
                    }
                }

                this._spcFacade.DeleteSPCItemSpec((SPCItemSpec[])objs.ToArray(typeof(SPCItemSpec)));
                for (int i = 0; i < objs.Count; i++)
                {
                    UpdateTestCondion((SPCItemSpec)objs[i], true);
                }
                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Delete);
            }
        }

        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {
            if (_spcFacade == null) { _spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade(); }
            object obj = this.GetEditObject();
            if (obj != null)
            {
                this._spcFacade.UpdateSPCItemSpec((SPCItemSpec)obj);
                UpdateTestCondion((SPCItemSpec)obj, false);
                this.RequestData();

                this.buttonHelper.PageActionStatusHandle(PageActionType.Save);
            }
        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this.buttonHelper.PageActionStatusHandle(PageActionType.Cancel);
        }

        //private void chbSelectAll_CheckedChanged(object sender, System.EventArgs e)
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

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            object obj = this.GetEditObject(row);

            if (obj != null)
            {
                this.SetEditObject(obj);

                this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
            }
        }


        #endregion

        #region private method

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        private int GetRowCount()
        {
            if (_spcFacade == null) { _spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade(); }
            return this._spcFacade.QuerySPCItemSpecCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
                FormatHelper.CleanString(this.drpObjectCodeQuery.SelectedValue));
        }


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



        private void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Save)
            {
                this.txtItemCodeEdit.Readonly = false;
                this.drpObjectCodeEdit.Enabled = true;
                this.txtGroupSeqEdit.Enabled = true;
            }
            else if (pageAction == PageActionType.Update)
            {
                this.txtItemCodeEdit.Readonly = true;
                this.drpObjectCodeEdit.Enabled = false;
                this.txtGroupSeqEdit.Enabled = true;
            }
            else if (pageAction == PageActionType.Cancel)
            {
                this.txtItemCodeEdit.Readonly = false;
                this.drpObjectCodeEdit.Enabled = true;
                this.txtGroupSeqEdit.Enabled = true;

                if (this.txtGroupSeqEdit.Items != null)
                {
                    this.txtGroupSeqEdit.Items.Clear();
                }
            }

        }


        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        public void InitButton()
        {
            this.buttonHelper.AddDeleteConfirm();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
        }

        private void InitWebGrid()
        {
            this.gridHelper.AddColumn("ItemCode", "产品代码", null);
            this.gridHelper.AddColumn("ObjectName", "管控项目", null);
            this.gridHelper.AddColumn("GroupSeq", "组次", null);
            this.gridHelper.AddColumn("TestCondition", "规格条件", null);
            //			this.gridHelper.AddColumn( "ColumnName",	"存放栏位",	null);
            //this.gridHelper.AddColumn( "TestDataCount",	"测试次数",	null);
            this.gridHelper.AddColumn("AutoCL", "自动生成CL", null);
            this.gridHelper.AddColumn("LimitUp", "单边上限", null);
            this.gridHelper.AddColumn("LimitLow", "单边下限", null);
            this.gridHelper.AddColumn("UCL", "UCL", null);
            this.gridHelper.AddColumn("LCL", "LCL", null);
            this.gridHelper.AddColumn("USL", "USL", null);
            this.gridHelper.AddColumn("LSL", "LSL", null);

            this.gridHelper.AddColumn("Memo", "备注", null);
            this.gridHelper.AddColumn("MUSER", "维护用户", null);
            this.gridHelper.AddColumn("MDATE", "维护日期", null);
            this.gridHelper.AddColumn("MTIME", "维护时间", null);
            this.gridHelper.AddColumn("ObjectCode", "管控项目", null);


            this.gridHelper.AddDefaultColumn(true, true);
            this.gridWebGrid.Columns.FromKey("ObjectCode").Hidden = true;

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        private void InitPanel()
        {
            this.drpObjectCodeQuery.Items.Clear();
            this.drpObjectCodeEdit.Items.Clear();

            if (_spcFacade == null) { _spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade(); }
            object[] objs = _spcFacade.GetAllSPCObject();

            if (objs != null && objs.Length > 0)
            {
                this.drpObjectCodeQuery.Items.Add(new ListItem("", ""));
                this.drpObjectCodeEdit.Items.Add(new ListItem("", ""));

                for (int i = 0; i < objs.Length; i++)
                {
                    this.drpObjectCodeQuery.Items.Add(new ListItem((objs[i] as SPCObject).ObjectName, (objs[i] as SPCObject).ObjectCode));
                    this.drpObjectCodeEdit.Items.Add(new ListItem((objs[i] as SPCObject).ObjectName, (objs[i] as SPCObject).ObjectCode));
                }
            }
        }

        private object GetEditObject()
        {
            if (this.ValidateInput())
            {
                if (_spcFacade == null) { _spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade(); }
                SPCItemSpec obj = this._spcFacade.CreateNewSPCItemSpec();

                obj.ItemCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeEdit.Text, 40));
                obj.ObjectCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpObjectCodeEdit.SelectedValue));
                obj.GroupSeq = Convert.ToInt32(FormatHelper.CleanString(this.txtGroupSeqEdit.Text));
                obj.ConditionName = FormatHelper.CleanString(this.txtConditionEdit.Text, 40);
                obj.Memo = FormatHelper.CleanString(this.txtMemoEdit.Text, 100);


                obj.AutoCL = this.chbAutoCLEdit.Checked ? FormatHelper.TRUE_STRING : FormatHelper.FALSE_STRING;
                obj.LimitLowOnly = this.chbLowEdit.Checked ? FormatHelper.TRUE_STRING : FormatHelper.FALSE_STRING;
                obj.LimitUpOnly = this.chbUpEdit.Checked ? FormatHelper.TRUE_STRING : FormatHelper.FALSE_STRING;

                obj.LCL = FormatHelper.CleanString(this.txtLCLEdit.Text) == string.Empty ? 0 : Convert.ToDecimal(FormatHelper.CleanString(this.txtLCLEdit.Text));
                obj.UCL = FormatHelper.CleanString(this.txtUCLEdit.Text) == string.Empty ? 0 : Convert.ToDecimal(FormatHelper.CleanString(this.txtUCLEdit.Text));
                obj.LSL = FormatHelper.CleanString(this.txtLSLEdit.Text) == string.Empty ? 0 : Convert.ToDecimal(FormatHelper.CleanString(this.txtLSLEdit.Text));
                obj.USL = FormatHelper.CleanString(this.txtUSLEdit.Text) == string.Empty ? 0 : Convert.ToDecimal(FormatHelper.CleanString(this.txtUSLEdit.Text));

                obj.MaintainUser = this.GetUserCode();

                return obj;
            }
            else
            {
                return null;
            }
        }

        private object GetEditObject(GridRecord row)
        {
            if (_spcFacade == null) { _spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade(); }
            object obj = this._spcFacade.GetSPCItemSpec(
                row.Items.FindItemByKey("ItemCode").Text.ToString(),
                Convert.ToDecimal(row.Items.FindItemByKey("GroupSeq").Value),
                row.Items.FindItemByKey("ObjectCode").Text.ToString());

            if (obj != null)
            {
                return (SPCItemSpec)obj;
            }

            return null;
        }

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(this.lblItemCodeEdit, this.txtItemCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblObjectCodeEdit, this.drpObjectCodeEdit, 40, true));
            manager.Add(new NumberCheck(this.lblGroupSeqEdit, this.txtGroupSeqEdit, 0, int.MaxValue, true));
            manager.Add(new LengthCheck(this.lblConditionEdit, this.txtConditionEdit, 40, true));
            //manager.Add( new NumberCheck( this.lblColumnNamedit, this.txtColumnNamedit, 1, int.MaxValue, true));
            manager.Add(new LengthCheck(this.lblMemoEdit, this.txtMemoEdit, 100, false));

            if (!this.chbAutoCLEdit.Checked && !this.chbUpEdit.Checked && !this.chbLowEdit.Checked)
            {
                manager.Add(new DecimalCheck(lblLCLEdit, txtLCLEdit, 0.00001M, decimal.MaxValue, true));
                manager.Add(new DecimalCheck(lblUCLEdit, txtUCLEdit, 0.00001M, decimal.MaxValue, true));
                manager.Add(new DecimalCheck(lblLSLEdit, txtLSLEdit, 0.00001M, decimal.MaxValue, true));
                manager.Add(new DecimalCheck(lblUSLEdit, txtUSLEdit, 0.00001M, decimal.MaxValue, true));

                if (!manager.Check())
                {
                    WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                    return false;
                }

                manager.Add(new DecimalRangeCheck(
                    lblLCLEdit, FormatHelper.CleanString(txtLCLEdit.Text),
                    lblUCLEdit, FormatHelper.CleanString(txtUCLEdit.Text), true));
            }

            else if (!this.chbAutoCLEdit.Checked && !this.chbUpEdit.Checked && this.chbLowEdit.Checked)
            {
                manager.Add(new DecimalCheck(lblLCLEdit, txtLCLEdit, 0.00001M, decimal.MaxValue, true));

            }
            else if (!this.chbAutoCLEdit.Checked && !this.chbLowEdit.Checked && this.chbUpEdit.Checked)
            {
                manager.Add(new DecimalCheck(lblUCLEdit, txtUCLEdit, 0.00001M, decimal.MaxValue, false));
            }

            manager.Add(new DecimalRangeCheck(
                lblLSLEdit, FormatHelper.CleanString(txtLSLEdit.Text),
                lblUSLEdit, FormatHelper.CleanString(txtUSLEdit.Text), true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }

            return true;
        }

        private void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtItemCodeEdit.Text = string.Empty;
                this.drpObjectCodeEdit.SelectedIndex = -1;
                this.txtGroupSeqEdit.SelectedIndex = -1;
                this.txtConditionEdit.Text = string.Empty;
                this.txtColumnNamedit.Text = "0";
                this.txtMemoEdit.Text = string.Empty;
                this.txtTestCount.Text = string.Empty;

                this.chbAutoCLEdit.Checked = false;
                this.chbUpEdit.Checked = false;
                this.chbLowEdit.Checked = false;

                this.txtUCLEdit.Text = string.Empty;
                this.txtLCLEdit.Text = string.Empty;
                this.txtUSLEdit.Text = string.Empty;
                this.txtLSLEdit.Text = string.Empty;

                return;
            }

            SPCItemSpec spcObj = obj as SPCItemSpec;

            this.txtItemCodeEdit.Text = spcObj.ItemCode.ToString();
            //this.drpObjectCodeEdit.SelectedValue = spcObj.ObjectCode.ToString();
            if (this.drpObjectCodeEdit.Items.FindByValue(spcObj.ObjectCode) != null)
            {
                this.drpObjectCodeEdit.SelectedIndex = this.drpObjectCodeEdit.Items.IndexOf(this.drpObjectCodeEdit.Items.FindByValue(spcObj.ObjectCode));
                SetGroupSeqEdit();
                this.txtGroupSeqEdit.Text = spcObj.GroupSeq.ToString("#0.##");
            }
            else
            {
                this.drpObjectCodeEdit.SelectedIndex = -1;
                this.txtGroupSeqEdit.SelectedIndex = -1;
            }

            this.txtConditionEdit.Text = spcObj.ConditionName.ToString();
            this.txtMemoEdit.Text = spcObj.Memo.ToString();
            //this.txtTestCount.Text = spcObj.TestDataCount;

            this.chbAutoCLEdit.Checked = string.Compare(spcObj.AutoCL.ToString(), FormatHelper.TRUE_STRING, true) == 0 ? true : false;
            this.chbUpEdit.Checked = string.Compare(spcObj.LimitUpOnly.ToString(), FormatHelper.TRUE_STRING, true) == 0 ? true : false;
            this.chbLowEdit.Checked = string.Compare(spcObj.LimitLowOnly.ToString(), FormatHelper.TRUE_STRING, true) == 0 ? true : false;

            this.txtUCLEdit.Text = spcObj.UCL.ToString("##.##");
            this.txtLCLEdit.Text = spcObj.LCL.ToString("##.##");
            this.txtUSLEdit.Text = spcObj.USL.ToString("##.##");
            this.txtLSLEdit.Text = spcObj.LSL.ToString("##.##");
        }

        protected DataRow GetGridRow(object obj)
        {
            SPCItemSpec spcObj = obj as SPCItemSpec;
//            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
//                new object[]{
//                                false,
//                                spcObj.ItemCode.ToString(),
//                                this.drpObjectCodeQuery.Items.FindByValue(spcObj.ObjectCode.ToString()).Text,
//                                spcObj.GroupSeq.ToString(),
//                                spcObj.ConditionName.ToString(),
////								spcObj.StoreColumn.ToString(),
//                                //spcObj.TestDataCount,
//                                this.languageComponent1.GetString(spcObj.AutoCL.ToString()),
//                                this.languageComponent1.GetString(spcObj.LimitUpOnly.ToString()),
//                                this.languageComponent1.GetString(spcObj.LimitLowOnly.ToString()),
//                                spcObj.UCL.ToString("##.##"),
//                                spcObj.LCL.ToString("##.##"),
//                                spcObj.USL.ToString("##.##"),
//                                spcObj.LSL.ToString("##.##"),
//                                spcObj.Memo.ToString(),
//                                spcObj.MaintainUser.ToString(),
//                                FormatHelper.ToDateString(spcObj.MaintainDate),
//                                FormatHelper.ToTimeString(spcObj.MaintainTime),
//                                spcObj.ObjectCode.ToString(),
//                                ""
//                            });

            DataRow row = this.DtSource.NewRow();
            row["ItemCode"] = spcObj.ItemCode.ToString();
            row["ObjectName"] = this.drpObjectCodeQuery.Items.FindByValue(spcObj.ObjectCode.ToString()).Text;
            row["GroupSeq"] = spcObj.GroupSeq.ToString();
            row["TestCondition"] = spcObj.ConditionName.ToString();
            row["AutoCL"] = this.languageComponent1.GetString(spcObj.AutoCL.ToString());
            row["LimitUp"] = this.languageComponent1.GetString(spcObj.LimitUpOnly.ToString());
            row["LimitLow"] = this.languageComponent1.GetString(spcObj.LimitLowOnly.ToString());
            row["UCL"] = spcObj.UCL.ToString("##.##");
            row["LCL"] = spcObj.LCL.ToString("##.##");
            row["USL"] = spcObj.USL.ToString("##.##");
            row["LSL"] = spcObj.LSL.ToString("##.##");
            row["Memo"] = spcObj.Memo.ToString();
            row["MUSER"] = spcObj.MaintainUser.ToString();
            row["MDATE"] = FormatHelper.ToDateString(spcObj.MaintainDate);
            row["MTIME"] = FormatHelper.ToTimeString(spcObj.MaintainTime);
            row["ObjectCode"] = spcObj.ObjectCode.ToString();
            return row;

        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_spcFacade == null) { _spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade(); }
            return this._spcFacade.QuerySPCItemSpec(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
                FormatHelper.CleanString(this.drpObjectCodeQuery.SelectedValue),
                inclusive, exclusive);
        }

        private string[] FormatExportRecord(object obj)
        {
            SPCItemSpec spcObj = obj as SPCItemSpec;
            return new string[]{
								   spcObj.ItemCode.ToString(),
								   this.drpObjectCodeQuery.Items.FindByValue(spcObj.ObjectCode.ToString()).Text,
								   spcObj.GroupSeq.ToString(),
								   spcObj.ConditionName.ToString(),
//								   spcObj.StoreColumn.ToString(),
								   this.languageComponent1.GetString(spcObj.AutoCL.ToString()),
								   this.languageComponent1.GetString(spcObj.LimitUpOnly.ToString()),
								   this.languageComponent1.GetString(spcObj.LimitLowOnly.ToString()),
								   spcObj.UCL.ToString("##.##"),
								   spcObj.LCL.ToString("##.##"),
								   spcObj.USL.ToString("##.##"),
								   spcObj.LSL.ToString("##.##"),
								   spcObj.Memo.ToString(),
								   spcObj.MaintainUser.ToString(),
								   FormatHelper.ToDateString(spcObj.MaintainDate),
								   FormatHelper.ToTimeString(spcObj.MaintainTime)	
							   };
        }

        private string[] GetColumnHeaderText()
        {
            return new string[] {	
									"ItemCode",
									"ObjectCode",
									"GroupSeq",
									"TestCondition",
//									"ColumnName",
									"AutoCL",
									"LimitUp",
									"LimitLow",
									"UCL",
									"LCL",
									"USL",
									"LSL",
									"Memo",
									"MUSER",
									"MDATE",
									"MTIME"
								};
        }

        #endregion

        private void UpdateTestCondion(SPCItemSpec spcItemSpec, bool delete)
        {
            if (string.Compare(spcItemSpec.ObjectCode, SPCObjectList.PT_ELECTRIC, true) == 0)
            {
                ItemFacade facade = new FacadeFactory(base.DataProvider).CreateItemFacade();
                object obj = facade.GetItem(spcItemSpec.ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                if (obj != null)
                {
                    (obj as Item).ElectricCurrentMaxValue = spcItemSpec.USL;
                    (obj as Item).ElectricCurrentMinValue = spcItemSpec.LSL;
                    (obj as Item).MaintainUser = this.GetUserCode();
                    facade.UpdateItem(obj as Item);
                }
            }
            else if (string.Compare(spcItemSpec.ObjectCode, SPCObjectList.OQC_DUTY_RATO, true) == 0)
            {
                OQCFacade facade = new FacadeFactory(base.DataProvider).CreateOQCFacade();
                object obj = facade.GetOQCFuncTest(spcItemSpec.ItemCode);
                if (obj != null)
                {
                    if (delete == false)
                    {
                        (obj as OQCFuncTest).MinDutyRatoMax = spcItemSpec.USL;
                        (obj as OQCFuncTest).MinDutyRatoMin = spcItemSpec.LSL;
                        (obj as OQCFuncTest).MaintainUser = this.GetUserCode();
                        facade.UpdateOQCFuncTest(obj as OQCFuncTest);
                    }
                    else
                    {
                        (obj as OQCFuncTest).MinDutyRatoMax = 0;
                        (obj as OQCFuncTest).MinDutyRatoMin = 0;
                        (obj as OQCFuncTest).MaintainUser = this.GetUserCode();
                        facade.UpdateOQCFuncTest(obj as OQCFuncTest);
                    }
                }
                else if (delete == false)
                {
                    obj = facade.CreateNewOQCFuncTest();
                    (obj as OQCFuncTest).ItemCode = spcItemSpec.ItemCode;
                    (obj as OQCFuncTest).MinDutyRatoMax = spcItemSpec.USL;
                    (obj as OQCFuncTest).MinDutyRatoMin = spcItemSpec.LSL;
                    (obj as OQCFuncTest).MaintainUser = this.GetUserCode();
                    facade.AddOQCFuncTest(obj as OQCFuncTest);
                }
            }
            else if (string.Compare(spcItemSpec.ObjectCode, SPCObjectList.OQC_BURST_MD, true) == 0)
            {
                OQCFacade facade = new FacadeFactory(base.DataProvider).CreateOQCFacade();
                object obj = facade.GetOQCFuncTest(spcItemSpec.ItemCode);
                if (obj != null)
                {
                    if (delete == false)
                    {
                        (obj as OQCFuncTest).BurstMdFreMax = spcItemSpec.USL;
                        (obj as OQCFuncTest).BurstMdFreMin = spcItemSpec.LSL;
                        (obj as OQCFuncTest).MaintainUser = this.GetUserCode();
                        facade.UpdateOQCFuncTest(obj as OQCFuncTest);
                    }
                    else
                    {
                        (obj as OQCFuncTest).BurstMdFreMax = 0;
                        (obj as OQCFuncTest).BurstMdFreMin = 0;
                        (obj as OQCFuncTest).MaintainUser = this.GetUserCode();
                        facade.UpdateOQCFuncTest(obj as OQCFuncTest);
                    }
                }
                else if (delete == false)
                {
                    obj = facade.CreateNewOQCFuncTest();
                    (obj as OQCFuncTest).ItemCode = spcItemSpec.ItemCode;
                    (obj as OQCFuncTest).BurstMdFreMax = spcItemSpec.USL;
                    (obj as OQCFuncTest).BurstMdFreMin = spcItemSpec.LSL;
                    (obj as OQCFuncTest).MaintainUser = this.GetUserCode();
                    facade.AddOQCFuncTest(obj as OQCFuncTest);
                }
            }
            else if (string.Compare(spcItemSpec.ObjectCode, SPCObjectList.OQC_FT_FREQUENCY, true) == 0)
            {
                OQCFacade facade = new FacadeFactory(base.DataProvider).CreateOQCFacade();
                object obj = facade.GetOQCFuncTestSpec(spcItemSpec.ItemCode, spcItemSpec.GroupSeq);
                if (obj != null)
                {
                    if (delete == false)
                    {
                        (obj as OQCFuncTestSpec).FreMax = spcItemSpec.USL;
                        (obj as OQCFuncTestSpec).FreMin = spcItemSpec.LSL;
                        (obj as OQCFuncTestSpec).MaintainUser = this.GetUserCode();
                        facade.UpdateOQCFuncTestSpec(obj as OQCFuncTestSpec);
                    }
                    else
                    {
                        object objTmp = this._spcFacade.GetSPCItemSpec(spcItemSpec.ItemCode, spcItemSpec.GroupSeq, SPCObjectList.OQC_FT_ELECTRIC);
                        if (objTmp == null)
                        {
                            facade.DeleteOQCFuncTestSpec(obj as OQCFuncTestSpec);
                        }
                        else
                        {
                            (obj as OQCFuncTestSpec).FreMax = 0;
                            (obj as OQCFuncTestSpec).FreMin = 0;
                            (obj as OQCFuncTestSpec).MaintainUser = this.GetUserCode();
                            facade.UpdateOQCFuncTestSpec(obj as OQCFuncTestSpec);
                        }
                    }
                }
                else if (delete == false)
                {
                    obj = facade.CreateNewOQCFuncTestSpec();
                    (obj as OQCFuncTestSpec).ItemCode = spcItemSpec.ItemCode;
                    (obj as OQCFuncTestSpec).GroupSequence = spcItemSpec.GroupSeq;
                    (obj as OQCFuncTestSpec).FreMax = spcItemSpec.USL;
                    (obj as OQCFuncTestSpec).FreMin = spcItemSpec.LSL;
                    (obj as OQCFuncTestSpec).MaintainUser = this.GetUserCode();
                    facade.AddOQCFuncTestSpec(obj as OQCFuncTestSpec);
                }
            }
            else if (string.Compare(spcItemSpec.ObjectCode, SPCObjectList.OQC_FT_ELECTRIC, true) == 0)
            {
                OQCFacade facade = new FacadeFactory(base.DataProvider).CreateOQCFacade();
                object obj = facade.GetOQCFuncTestSpec(spcItemSpec.ItemCode, spcItemSpec.GroupSeq);
                if (obj != null)
                {
                    if (delete == false)
                    {
                        (obj as OQCFuncTestSpec).ElectricMax = spcItemSpec.USL;
                        (obj as OQCFuncTestSpec).ElectricMin = spcItemSpec.LSL;
                        (obj as OQCFuncTestSpec).MaintainUser = this.GetUserCode();
                        facade.UpdateOQCFuncTestSpec(obj as OQCFuncTestSpec);
                    }
                    else
                    {
                        object objTmp = this._spcFacade.GetSPCItemSpec(spcItemSpec.ItemCode, spcItemSpec.GroupSeq, SPCObjectList.OQC_FT_FREQUENCY);
                        if (objTmp == null)
                        {
                            facade.DeleteOQCFuncTestSpec(obj as OQCFuncTestSpec);
                        }
                        else
                        {
                            (obj as OQCFuncTestSpec).ElectricMax = 0;
                            (obj as OQCFuncTestSpec).ElectricMin = 0;
                            (obj as OQCFuncTestSpec).MaintainUser = this.GetUserCode();
                            facade.UpdateOQCFuncTestSpec(obj as OQCFuncTestSpec);
                        }
                    }
                }
                else if (delete == false)
                {
                    obj = facade.CreateNewOQCFuncTestSpec();
                    (obj as OQCFuncTestSpec).ItemCode = spcItemSpec.ItemCode;
                    (obj as OQCFuncTestSpec).GroupSequence = spcItemSpec.GroupSeq;
                    (obj as OQCFuncTestSpec).ElectricMax = spcItemSpec.USL;
                    (obj as OQCFuncTestSpec).ElectricMin = spcItemSpec.LSL;
                    (obj as OQCFuncTestSpec).MaintainUser = this.GetUserCode();
                    facade.AddOQCFuncTestSpec(obj as OQCFuncTestSpec);
                }
            }
            else if (string.Compare(spcItemSpec.ObjectCode, SPCObjectList.OQC_DIM_LENGTH, true) == 0)
            {
                UpdateItem2Dimention(spcItemSpec, "LengthMax", "LengthMin", delete);
            }
            else if (string.Compare(spcItemSpec.ObjectCode, SPCObjectList.OQC_DIM_WIDTH, true) == 0)
            {
                UpdateItem2Dimention(spcItemSpec, "WidthMax", "WidthMin", delete);
            }
            else if (string.Compare(spcItemSpec.ObjectCode, SPCObjectList.OQC_DIM_BOARDHEIGHT, true) == 0)
            {
                UpdateItem2Dimention(spcItemSpec, "BoardHeightMax", "BoardHeightMin", delete);
            }
            else if (string.Compare(spcItemSpec.ObjectCode, SPCObjectList.OQC_DIM_HEIGHT, true) == 0)
            {
                UpdateItem2Dimention(spcItemSpec, "HeightMax", "HeightMin", delete);
            }
            else if (string.Compare(spcItemSpec.ObjectCode, SPCObjectList.OQC_DIM_ALLHEIGHT, true) == 0)
            {
                UpdateItem2Dimention(spcItemSpec, "AllHeightMax", "AllHeightMin", delete);
            }
            else if (string.Compare(spcItemSpec.ObjectCode, SPCObjectList.OQC_DIM_LEFT2RIGHT, true) == 0)
            {
                UpdateItem2Dimention(spcItemSpec, "Left2RightMax", "Left2RightMin", delete);
            }
            else if (string.Compare(spcItemSpec.ObjectCode, SPCObjectList.OQC_DIM_LEFT2MIDDLE, true) == 0)
            {
                UpdateItem2Dimention(spcItemSpec, "Left2MiddleMax", "Left2MiddleMin", delete);
            }
            else if (string.Compare(spcItemSpec.ObjectCode, SPCObjectList.OQC_DIM_RIGHT2MIDDLE, true) == 0)
            {
                UpdateItem2Dimention(spcItemSpec, "Right2MiddleMax", "Right2MiddleMin", delete);
            }
        }

        private void UpdateItem2Dimention(SPCItemSpec spcItemSpec, string maxParam, string minParam, bool delete)
        {
            ItemFacade facade = new FacadeFactory(base.DataProvider).CreateItemFacade();
            object obj = null;
            obj = facade.GetItem2Dimention(spcItemSpec.ItemCode, maxParam, GlobalVariables.CurrentOrganizations.First().OrganizationID);
            if (obj != null)
            {
                if (delete == false)
                {
                    (obj as Item2Dimention).ParamValue = spcItemSpec.USL;
                    (obj as Item2Dimention).MaintainUser = this.GetUserCode();
                    facade.UpdateItem2Dimention(obj as Item2Dimention);
                }
                else
                {
                    facade.DeleteItem2Dimention(obj as Item2Dimention);
                }
            }
            else if (delete == false)
            {
                obj = facade.CreateNewItem2Dimention();
                (obj as Item2Dimention).ItemCode = spcItemSpec.ItemCode;
                (obj as Item2Dimention).ParamName = maxParam;
                (obj as Item2Dimention).ParamValue = spcItemSpec.USL;
                (obj as Item2Dimention).MaintainUser = this.GetUserCode();
                (obj as Item2Dimention).OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                facade.AddItem2Dimention(obj as Item2Dimention);
            }

            obj = facade.GetItem2Dimention(spcItemSpec.ItemCode, minParam, GlobalVariables.CurrentOrganizations.First().OrganizationID);
            if (obj != null)
            {
                if (delete == false)
                {
                    (obj as Item2Dimention).ParamValue = spcItemSpec.LSL;
                    (obj as Item2Dimention).MaintainUser = this.GetUserCode();
                    facade.UpdateItem2Dimention(obj as Item2Dimention);
                }
                else
                {
                    facade.DeleteItem2Dimention(obj as Item2Dimention);
                }
            }
            else if (delete == false)
            {
                obj = facade.CreateNewItem2Dimention();
                (obj as Item2Dimention).ItemCode = spcItemSpec.ItemCode;
                (obj as Item2Dimention).ParamName = minParam;
                (obj as Item2Dimention).ParamValue = spcItemSpec.LSL;
                (obj as Item2Dimention).MaintainUser = this.GetUserCode();
                (obj as Item2Dimention).OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                facade.AddItem2Dimention(obj as Item2Dimention);
            }
        }

        protected void drpObjectCodeEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetGroupSeqEdit();
        }

        private void SetGroupSeqEdit()
        {
            if (this.txtGroupSeqEdit.Items != null)
            {
                this.txtGroupSeqEdit.Items.Clear();
            }
            if (_spcFacade == null) { _spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade(); }

            string objectCode = this.drpObjectCodeEdit.Text.Trim();
            object[] objs = _spcFacade.QuerySPCObjectStoreByObjectCode(objectCode, 0, int.MaxValue);

            Hashtable ht = new Hashtable();

            if (objs != null)
            {

                foreach (SPCObjectStore obj in objs)
                {
                    if (!ht.ContainsKey(obj.GroupSeq))
                    {
                        ht.Add(obj.GroupSeq, "");
                        this.txtGroupSeqEdit.Items.Add(new ListItem(obj.GroupSeq.ToString(), obj.GroupSeq.ToString()));
                    }
                }
            }
        }


    }
}
