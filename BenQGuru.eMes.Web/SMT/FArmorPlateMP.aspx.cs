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
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.ArmorPlate;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.MOModel;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
using System.Collections.Generic;

namespace BenQGuru.eMES.Web.SMT
{
    /// <summary>
    /// ItemMP 的摘要说明。
    /// </summary>
    public partial class FArmorPlateMP : BaseMPageMinus
    {
        protected global::System.Web.UI.WebControls.TextBox txtInFacDate;
        protected BenQGuru.eMES.Web.UserControl.eMESTime txtInFacTime;

        private System.ComponentModel.IContainer components;
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        //private GridHelper gridHelper = null;
        private ButtonHelper buttonHelper = null;
        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
        private BenQGuru.eMES.SMT.ArmorPlateFacade _apFacade = null;


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
            //this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
            this.txtItemEdit.TextBox.TextChanged += new EventHandler(this.txtItemEdit_TextChanged);
            // 
            // languageComponent1
            // 
            //this.languageComponent1.Language = "CHS";
            //this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            //this.languageComponent1.RuntimePage = null;
            //this.languageComponent1.RuntimeUserControl = null;
            //this.languageComponent1.UserControlName = "";
            //// 
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
            InitOnPostBack();
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                // 初始化界面UI
                this.InitUI();
                InitButtonHelp();
                SetEditObject(null);
                this.InitWebGrid();


                txtInFacDate.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(DateTime.Today));

                txtInFacTime.Text = FormatHelper.ToTimeString(0);
            }
        }
        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
        {
            object obj = this.GetEditObject();
            if (obj != null)
            {
                if (_apFacade == null) { _apFacade = new SMTFacadeFactory(base.DataProvider).CreateArmorPlateFacade(); }
                this._apFacade.AddArmorPlate(obj as ArmorPlate);
                this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            }
        }

        protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
        {
            if (_apFacade == null) { _apFacade = new SMTFacadeFactory(base.DataProvider).CreateArmorPlateFacade(); }
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {

                ArrayList objs = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    object obj = this.GetEditObject(row);
                    if (obj != null)
                    {
                        objs.Add((ArmorPlate)obj);
                    }
                }

                this._apFacade.DeleteArmorPlate((ArmorPlate[])objs.ToArray(typeof(ArmorPlate)));

                this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Delete);
            }
        }

        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {
            if (_apFacade == null) { _apFacade = new SMTFacadeFactory(base.DataProvider).CreateArmorPlateFacade(); }
            object obj = this.GetEditObject();

            if (obj != null)
            {
                this._apFacade.UpdateArmorPlate(obj as ArmorPlate);
                this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Save);
            }

        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this.buttonHelper.PageActionStatusHandle(PageActionType.Cancel);
        }

        protected void txtItemEdit_TextChanged(object sender, EventArgs e)
        {
            //-------Add by DS22 / Crane.Liu 2014-02-27 Start--------------
            /// Description:
            /// 编辑一个钢板，编辑产品，把所有选择产品移除，然后保存，退回到钢板编辑页面时，系统报错。
            /// 因为返回时要得到联板数，没选产品就没有得到联板数，所以报错
            if (string.IsNullOrEmpty(this.txtItemEdit.Text.Trim()))
            {
                return;
            }
            //-------Add by DS22 / Crane.Liu 2014-02-27 End----------------
            string[] items = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemEdit.Text)).Split(',');
            string LBRate = string.Empty;
            if (items.Length > 0)
            {
                ItemFacade itemFacade;
                foreach (string item in items)
                {
                    itemFacade = new ItemFacade();
                    object obj = itemFacade.GetItem(item, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                    if (String.IsNullOrEmpty(LBRate))
                    {
                        LBRate = ((Item)obj).PcbaCount.ToString("##.##");
                    }
                    else
                    {
                        if (LBRate != ((Item)obj).PcbaCount.ToString("##.##"))
                        {
                            WebInfoPublish.PublishInfo(this, "$message_Item_PcbaCount_Not_Same", languageComponent1);
                            this.txtItemEdit.Text = string.Empty;
                            this.txtLBRateEdit.Text = string.Empty;
                            return;
                        }
                    }
                }
            }
            this.txtLBRateEdit.Text = LBRate;
        }

        //private void chbSelectAll_CheckedChanged(object sender, System.EventArgs e)
        //{
        //    if ( this.chbSelectAll.Checked )
        //    {
        //        this.gridHelper.CheckAllRows( CheckStatus.Checked );
        //    }
        //    else
        //    {
        //        this.gridHelper.CheckAllRows( CheckStatus.Unchecked );
        //    }
        //}

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "Edit")
            {
                object obj = this.GetEditObject(row);

                if (obj != null)
                {
                    this.SetEditObject(obj);

                    this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
                }
            }
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this.excelExporter.Export();
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

        private void InitOnPostBack()
        {
            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
            this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

            // 2005-04-06
            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }

        public void InitButtonHelp()
        {
            this.buttonHelper.AddDeleteConfirm();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
        }

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        private void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtAPIDEdit.ReadOnly = false;
                this.txtVersionEdit.ReadOnly = false;
            }
            else if (pageAction == PageActionType.Update)
            {
                this.txtAPIDEdit.ReadOnly = true;
                this.txtVersionEdit.ReadOnly = true;
            }
            else if (pageAction == PageActionType.Save)
            {
                this.txtAPIDEdit.ReadOnly = false;
                this.txtVersionEdit.ReadOnly = false;
            }
            else if (pageAction == PageActionType.Cancel)
            {
                this.txtAPIDEdit.ReadOnly = false;
                this.txtVersionEdit.ReadOnly = false;
            }
        }

        private void InitWebGrid()
        {
            this.gridHelper.AddColumn("ArmorPlateID", "厂内编号", 100);
            this.gridHelper.AddColumn("BasePlateCode", "基板料号", 100);
            this.gridHelper.AddColumn("CurrentVersion", "版本", 100);
            this.gridHelper.AddColumn("WithItem", "对应产品", 100);
            this.gridHelper.AddColumn("Thickness", "厚度", 100);
            this.gridHelper.AddColumn("TotalUsedTimes", "累计使用次数", 100);
            this.gridHelper.AddColumn("Status", "状态", 100);
            this.gridHelper.AddColumn("ManufacturerSN", "厂商编号", 100);
            this.gridHelper.AddColumn("LBRate", "联板比例", 100);
            this.gridHelper.AddColumn("TensionA", "张力A", 100);
            this.gridHelper.AddColumn("TensionB", "张力B", 100);
            this.gridHelper.AddColumn("TensionC", "张力C", 100);
            this.gridHelper.AddColumn("TensionD", "张力D", 100);
            this.gridHelper.AddColumn("TensionE", "张力E", 100);
            this.gridHelper.AddColumn("Memo", "备注", 100);

            this.gridHelper.AddColumn("InFactoryDate", "进厂日期", 40);
            this.gridHelper.AddColumn("InFactoryTime", "进厂时间", 40);

            this.gridHelper.AddColumn("MUSER", "维护人员", 100);
            this.gridHelper.AddColumn("MDATE", "维护日期", 100);
            this.gridHelper.AddColumn("MTIME", "维护时间", 100);

            this.gridHelper.AddDefaultColumn(true, true);
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        private object GetEditObject()
        {
            if (this.ValidateInput())
            {
                if (_apFacade == null) { _apFacade = new SMTFacadeFactory(base.DataProvider).CreateArmorPlateFacade(); }

                ArmorPlate obj = this._apFacade.CreateNewArmorPlate();

                obj.ArmorPlateID = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtAPIDEdit.Text, 40));
                obj.BasePlateCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBPCodeEdit.Text, 40));
                obj.Version = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtVersionEdit.Text, 40));
                obj.Thickness = Convert.ToDecimal(FormatHelper.CleanString(this.txtThicknessEdit.Text));
                obj.ManufacturerSN = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtManSNEdit.Text, 40));
                obj.LBRate = Convert.ToInt32(FormatHelper.CleanString(this.txtLBRateEdit.Text));

                obj.TensionA = Convert.ToDecimal(FormatHelper.CleanString(this.txtTenAEdit.Text));
                obj.TensionB = Convert.ToDecimal(FormatHelper.CleanString(this.txtTenBEdit.Text));
                obj.TensionC = Convert.ToDecimal(FormatHelper.CleanString(this.txtTenCEdit.Text));
                obj.TensionD = Convert.ToDecimal(FormatHelper.CleanString(this.txtTenDEdit.Text));
                obj.TensionE = Convert.ToDecimal(FormatHelper.CleanString(this.txtTenEEdit.Text));

                obj.InFactoryDate = FormatHelper.TODateInt(this.txtInFacDate.Text);
                obj.InFactoryTime = FormatHelper.TOTimeInt(this.txtInFacTime.Text);

                obj.Memo = FormatHelper.CleanString(this.txtMemoEdit.Text, 100);

                obj.UsedTimes = 0;
                obj.Status = ArmorPlateStatus.StartUsing;
                obj.MaintainUser = this.GetUserCode();

                string[] items = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemEdit.Text)).Split(',');

                if (items != null && items.Length > 0)
                {
                    //-------Modify by DS22 / Crane.Liu 2014-02-27 Start--------------
                    /// Description:
                    /// 新增钢板，选择多个产品;维护后保存，再查看该钢板，发现对应产品只有选择多个产品的第一个和最后一个;
                    /// 保存时有一段过滤重复产品的逻辑，不正确，导致只能得到头尾两个产品
                    /// 

                    List<string> itemList = new List<string>(items);
                    ItemFacade itemFacade = new SMTFacadeFactory(base.DataProvider).CreateItemFacade();
                    ArmorPlate2Item[] ap2Items = new ArmorPlate2Item[itemList.Count];

                    int i = 0;
                    foreach (string itemstr in itemList)
                    {
                        ArmorPlate2Item ap2Item = this._apFacade.CreateNewArmorPlate2Item();
                        ap2Item.ArmorPlateID = obj.ArmorPlateID;
                        ap2Item.ItemCode = itemstr.ToString();
                        ap2Item.MaintainUser = this.GetUserCode();
                        object item = itemFacade.GetItem(ap2Item.ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                        ap2Items[i++] = ap2Item;
                    }
                    //-------Modify by DS22 / Crane.Liu 2014-02-27 End----------------

                    obj.Items = ap2Items;
                }

                return obj;
            }
            else
            {
                return null;
            }
        }

        private object GetEditObject(GridRecord row)
        {
            if (_apFacade == null) { _apFacade = new SMTFacadeFactory(base.DataProvider).CreateArmorPlateFacade(); }

            object obj = this._apFacade.GetArmorPlate(row.Items.FindItemByKey("ArmorPlateID").Text.ToString());

            if (obj != null)
            {
                return (ArmorPlate)obj;
            }

            return null;
        }

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(lblAPIDEdit, txtAPIDEdit, 40, true));
            manager.Add(new LengthCheck(lblBPCodeEdit, txtBPCodeEdit, 40, true));
            manager.Add(new LengthCheck(lblVersionEdit, txtVersionEdit, 40, true));
            manager.Add(new LengthCheck(lblItemCodeEdit, txtItemEdit, int.MaxValue, true));
            manager.Add(new DecimalCheck(lblThicknessEdit, txtThicknessEdit, 0.00001M, decimal.MaxValue, true));
            manager.Add(new LengthCheck(lblManSNEdit, txtManSNEdit, 40, false));

            manager.Add(new NumberCheck(lblLBRateEdit, txtLBRateEdit, 1, int.MaxValue, true));

            //			manager.Add( new NumberCheck(lblInFacDate, txtInFacDate, 1, int.MaxValue, true) );
            //			manager.Add( new NumberCheck(lblInFacTime, txtInFaTime, 1, int.MaxValue, true) );

            manager.Add(new DecimalCheck(lblTenAEdit, txtTenAEdit, 0.00001M, decimal.MaxValue, true));
            manager.Add(new DecimalCheck(lblTenBEdit, txtTenBEdit, 0.00001M, decimal.MaxValue, true));
            manager.Add(new DecimalCheck(lblTenCEdit, txtTenCEdit, 0.00001M, decimal.MaxValue, true));
            manager.Add(new DecimalCheck(lblTenDEdit, txtTenDEdit, 0.00001M, decimal.MaxValue, true));
            manager.Add(new DecimalCheck(lblTenEEdit, txtTenEEdit, 0.00001M, decimal.MaxValue, true));

            manager.Add(new LengthCheck(lblMemoEdit, txtMemoEdit, 100, false));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }
            return true;
        }

        private void SetEditObject(object obj)
        {
            if (_apFacade == null) { _apFacade = new SMTFacadeFactory(base.DataProvider).CreateArmorPlateFacade(); }
            if (obj == null)
            {
                this.txtAPIDEdit.Text = string.Empty;
                this.txtBPCodeEdit.Text = string.Empty;
                this.txtVersionEdit.Text = string.Empty;
                this.txtItemEdit.Text = string.Empty;
                this.txtThicknessEdit.Text = string.Empty;
                this.txtManSNEdit.Text = string.Empty;
                this.txtLBRateEdit.Text = string.Empty;
                this.txtTenAEdit.Text = string.Empty;
                this.txtTenBEdit.Text = string.Empty;
                this.txtTenCEdit.Text = string.Empty;
                this.txtTenDEdit.Text = string.Empty;
                this.txtTenEEdit.Text = string.Empty;
                this.txtMemoEdit.Text = string.Empty;



                return;
            }

            ArmorPlate armorPlate = obj as ArmorPlate;

            this.txtAPIDEdit.Text = armorPlate.ArmorPlateID.ToString();
            this.txtBPCodeEdit.Text = armorPlate.BasePlateCode.ToString();
            this.txtVersionEdit.Text = armorPlate.Version.ToString();
            this.txtItemEdit.Text = armorPlate.WithItems.ToString();
            this.txtThicknessEdit.Text = armorPlate.Thickness.ToString();
            this.txtManSNEdit.Text = armorPlate.ManufacturerSN.ToString();
            this.txtLBRateEdit.Text = armorPlate.LBRate.ToString("##.##");

            this.txtInFacDate.Text = FormatHelper.ToDateString(armorPlate.InFactoryDate);
            this.txtInFacTime.Text = FormatHelper.ToTimeString(armorPlate.InFactoryTime);

            this.txtTenAEdit.Text = armorPlate.TensionA.ToString();
            this.txtTenBEdit.Text = armorPlate.TensionB.ToString();
            this.txtTenCEdit.Text = armorPlate.TensionC.ToString();
            this.txtTenDEdit.Text = armorPlate.TensionD.ToString();
            this.txtTenEEdit.Text = armorPlate.TensionE.ToString();
            this.txtMemoEdit.Text = armorPlate.Memo.ToString();
        }

        protected DataRow GetGridRow(object obj)
        {
            ArmorPlate armorPlate = obj as ArmorPlate;
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{   "false",
            //                    armorPlate.ArmorPlateID.ToString(),
            //                    armorPlate.BasePlateCode.ToString(),
            //                    armorPlate.Version.ToString(),
            //                    armorPlate.WithItems,
            //                    armorPlate.Thickness.ToString(),
            //                    armorPlate.UsedTimes.ToString("##.##"),
            //                    this.languageComponent1.GetString(armorPlate.Status.ToString()),
            //                    armorPlate.ManufacturerSN.ToString(),
            //                    armorPlate.LBRate.ToString("##.##"),
            //                    armorPlate.TensionA.ToString(),
            //                    armorPlate.TensionB.ToString(),
            //                    armorPlate.TensionC.ToString(),
            //                    armorPlate.TensionD.ToString(),
            //                    armorPlate.TensionE.ToString(),
            //                    armorPlate.Memo.ToString(),
            //                    //Laws Lu,2006/08/13 添加 进厂时间
            //                    FormatHelper.ToDateString(armorPlate.InFactoryDate),
            //                    FormatHelper.ToTimeString(armorPlate.InFactoryTime),

            //                    armorPlate.MaintainUser.ToString(),
            //                    FormatHelper.ToDateString(armorPlate.MaintainDate),
            //                    FormatHelper.ToTimeString(armorPlate.MaintainTime),
            //                    ""});
            DataRow row = this.DtSource.NewRow();
            row["ArmorPlateID"] = armorPlate.ArmorPlateID.ToString();
            row["BasePlateCode"] = armorPlate.BasePlateCode.ToString();
            row["CurrentVersion"] = armorPlate.Version.ToString();
            row["WithItem"] = armorPlate.WithItems;
            row["Thickness"] = armorPlate.Thickness.ToString();
            row["TotalUsedTimes"] = armorPlate.UsedTimes.ToString("##.##");
            row["Status"] = this.languageComponent1.GetString(armorPlate.Status.ToString());
            row["ManufacturerSN"] = armorPlate.ManufacturerSN.ToString();
            row["LBRate"] = armorPlate.LBRate.ToString("##.##");
            row["TensionA"] = armorPlate.TensionA.ToString();
            row["TensionB"] = armorPlate.TensionB.ToString();
            row["TensionC"] = armorPlate.TensionC.ToString();
            row["TensionD"] = armorPlate.TensionD.ToString();
            row["TensionE"] = armorPlate.TensionE.ToString();
            row["Memo"] = armorPlate.Memo.ToString();
            row["InFactoryDate"] = FormatHelper.ToDateString(armorPlate.InFactoryDate);
            row["InFactoryTime"] = FormatHelper.ToTimeString(armorPlate.InFactoryTime);
            row["MUSER"] = armorPlate.MaintainUser.ToString();
            row["MDATE"] = FormatHelper.ToDateString(armorPlate.MaintainDate);
            row["MTIME"] = FormatHelper.ToTimeString(armorPlate.MaintainTime);
            return row;


        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_apFacade == null)
            {
                _apFacade = new SMTFacadeFactory(base.DataProvider).CreateArmorPlateFacade();
            }

            return _apFacade.QueryArmorPlate(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtAPIDQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBPCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemQuery.Text)),
                inclusive, exclusive);
        }

        private int GetRowCount()
        {
            if (_apFacade == null) { _apFacade = new SMTFacadeFactory(base.DataProvider).CreateArmorPlateFacade(); }
            return _apFacade.QueryArmorPlateCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtAPIDQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBPCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemQuery.Text)));
        }

        private string[] FormatExportRecord(object obj)
        {
            ArmorPlate armorPlate = obj as ArmorPlate;
            return new string[]{   armorPlate.ArmorPlateID.ToString(),
								   armorPlate.BasePlateCode.ToString(),
								   armorPlate.Version.ToString(),
								   armorPlate.WithItems,
								   armorPlate.Thickness.ToString(),
								   armorPlate.UsedTimes.ToString("##.##"),
								   this.languageComponent1.GetString(armorPlate.Status.ToString()),
								   armorPlate.ManufacturerSN.ToString(),
								   armorPlate.LBRate.ToString("##.##"),
								   armorPlate.TensionA.ToString(),
								   armorPlate.TensionB.ToString(),
								   armorPlate.TensionC.ToString(),
								   armorPlate.TensionD.ToString(),
								   armorPlate.TensionE.ToString(),
								   armorPlate.Memo.ToString(),
								   //Laws Lu,2006/08/13 添加 进厂时间
								   FormatHelper.ToDateString(armorPlate.InFactoryDate),
								   FormatHelper.ToTimeString(armorPlate.InFactoryTime),

								   armorPlate.MaintainUser.ToString(),
								   FormatHelper.ToDateString(armorPlate.MaintainDate),
								   FormatHelper.ToTimeString(armorPlate.MaintainTime)
			};
        }

        private string[] GetColumnHeaderText()
        {
            return new string[] {	"ArmorPlateID",
									"BasePlateCode",
									"CurrentVersion",	
									"WithItem",
									"Thickness",
									"TotalUsedTimes",
									"Status",	
									"ManufacturerSN",
									"LBRate",
									"TensionA",
									"TensionB",
									"TensionC",
									"TensionD",	
									"TensionE",
									"Memo",
									"InFactoryDate",
									"InFactoryTime",
									"MUSER", 
									"MDATE", 
									"MTIME"
			                        };
        }

        #endregion




    }
}
