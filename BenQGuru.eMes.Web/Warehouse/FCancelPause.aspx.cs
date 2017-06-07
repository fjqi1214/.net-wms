using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Common;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FCancelPause : BaseMPageMinus
    {
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        //private BenQGuru.eMES.Web.Helper.GridHelper gridHelper = null;
        private System.ComponentModel.IContainer components = null;
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
            //this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            //this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
            // 
            // languageComponent1
            // 
            //this.languageComponent1.Language = "CHS";
            //this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
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



        #region Init
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitOnPostBack();
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.txtPauseReason.ReadOnly = true;
                this.InitWebGrid();

            }
        }

        private void RequestData(string pauseCode)
        {
            PauseFacade objFacade = new PauseFacade(this.DataProvider);
            // 2005-04-06
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = objFacade.GetNotCancelPause2RcardListCount(pauseCode);
            this.pagerToolBar.InitPager();
            this.txtCancelReason.Text = string.Empty;
            this.txtCancelSeq.Text = string.Empty;
        }

        private void InitOnPostBack()
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);
            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);


            // 2005-04-06
            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);

        }

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        protected DataRow GetGridRow(object obj)
        {
            DataRow row = DtSource.NewRow();
            row["StorageCode"] = ((CancelPauseQuery)obj).StorageCode;
            row["StorageDisplay"] = ((CancelPauseQuery)obj).GetDisplayText("StorageCode");
            row["StackCode"] = ((CancelPauseQuery)obj).StackCode;
            row["PauseQty"] = ((CancelPauseQuery)obj).PauseQty;
            row["CancelQty"] = ((CancelPauseQuery)obj).CancelQty;
            row["ItemCode"] = ((CancelPauseQuery)obj).ItemCode;
            row["ItemDesc"] = ((CancelPauseQuery)obj).ItemDescription;
            row["PauseCode"] = ((CancelPauseQuery)obj).PauseCode;
            return row;
        }


        private object[] LoadDataSource()
        {
            string strPauseCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPauseCode.Text.Trim()));

            PauseFacade objFacade = new PauseFacade(this.DataProvider);
            object objPause = objFacade.GetPause(strPauseCode);

            if (objPause == null || ((Pause)objPause).Status.EndsWith(PauseStatus.status_cancel))
            {
                return null;
            }


            return this.LoadDataSource(1, int.MaxValue);
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {

            string strPauseCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPauseCode.Text.Trim()));

            PauseFacade objFacade = new PauseFacade(this.DataProvider);
            object objPause = objFacade.GetPause(strPauseCode);

            if (objPause == null || ((Pause)objPause).Status.EndsWith(PauseStatus.status_cancel))
            {
                return null;
            }


            return objFacade.GetNotCancelPause2RcardList(FormatHelper.CleanString(strPauseCode),
              inclusive,
              exclusive);
        }

        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("StorageCode", "库别", null);
            this.gridHelper.AddColumn("StorageDisplay", "库别", null);
            this.gridHelper.AddColumn("StackCode", "垛位", null);
            this.gridHelper.AddColumn("PauseQty", "停发数量", null);
            this.gridHelper.AddColumn("CancelQty", "已解除数量", null);
            this.gridHelper.AddColumn("ItemCode", "产品代码", null);
            this.gridHelper.AddColumn("ItemDesc", "产品描述", null);
            this.gridHelper.AddColumn("PauseCode", "停发单号", null);
            this.gridHelper.AddDefaultColumn(true, false);
            this.gridWebGrid.Columns.FromKey("StorageCode").Hidden = true;
            this.gridWebGrid.Columns.FromKey("PauseCode").Hidden = true;
            //this.gridHelper.Grid.Bands[0].Columns.FromKey("PauseQty").DataType = "System.Decimal";
            //this.gridHelper.Grid.Bands[0].Columns.FromKey("CancelQty").DataType = "System.Decimal";
            //this.gridHelper.Grid.Bands[0].Columns.FromKey("PauseQty").CellStyle.HorizontalAlign = HorizontalAlign.Right;
            //this.gridHelper.Grid.Bands[0].Columns.FromKey("CancelQty").CellStyle.HorizontalAlign = HorizontalAlign.Right;


            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);



        }
        #endregion

        #region Button
        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.cmdQuery_ServerClick(sender, e, false);
        }

        private void cmdQuery_ServerClick(object sender, System.EventArgs e, bool checkAfterSave)
        {
            if (!CheckQueryCondition())
            {
                return;
            }
            ChearUI();

            string strPauseCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPauseCode.Text.Trim()));

            PauseFacade objFacade = new PauseFacade(this.DataProvider);
            object objPause = objFacade.GetPause(strPauseCode);

            if (objPause == null && checkAfterSave == false)
            {
                //Message:停发通知单不存在
                WebInfoPublish.Publish(this, "$CS_PAUSE_NOT_EXIST", this.languageComponent1);
                this.txtPauseCode.Focus();
                return;
            }

            if (((Pause)objPause).Status.EndsWith(PauseStatus.status_cancel) && checkAfterSave == false)
            {
                //Message:该停发通知单已经关闭
                WebInfoPublish.Publish(this, "$CS_PAUSE_IN_CANCEL", this.languageComponent1);
                this.txtPauseCode.Focus();
                return;
            }

            this.txtPauseReason.Text = ((Pause)objPause).PauseReason;

            this.RequestData(strPauseCode);

        }

        private void ChearUI()
        {
            this.txtCancelReason.Text = "";
            this.txtCancelSeq.Text = "";
            this.txtPauseReason.Text = "";
            this.gridHelper._grid.Rows.Clear();
            this.pagerToolBar.RowCount = 0;
        }

        private bool CheckQueryCondition()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(lblPauseCode, txtPauseCode, 40, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                this.txtPauseCode.Focus();
                return false;
            }
            return true;
        }

        protected void cmdCancelPause_ServerClick(object sender, System.EventArgs e)
        {
            if (!this.CheckCancelPauseUI())
            {
                return;
            }

            ArrayList arryList = this.gridHelper.GetCheckedRows();

            if (arryList.Count == 0)
            {
                WebInfoPublish.Publish(this, "$CS_CHOOSE_ONE_RECORD_AT_LEAST", languageComponent1);
                return;
            }

            IList<CancelPauseQuery> queryList = new List<CancelPauseQuery>();

            foreach (GridRecord row in arryList)
            {
                CancelPauseQuery obj = new CancelPauseQuery();
                obj.PauseCode = row.Items.FindItemByKey("PauseCode").Text;
                obj.StorageCode = row.Items.FindItemByKey("StorageCode").Text;
                obj.StackCode = row.Items.FindItemByKey("StackCode").Text;
                obj.PauseQty = Convert.ToDecimal(row.Items.FindItemByKey("PauseQty").Text);
                obj.CancelQty = Convert.ToDecimal(row.Items.FindItemByKey("CancelQty").Text);
                obj.ItemCode = row.Items.FindItemByKey("ItemCode").Text;
                obj.ItemDescription = row.Items.FindItemByKey("ItemDesc").Text;
                queryList.Add(obj);

            }

            PauseFacade objFacade = new PauseFacade(this.DataProvider);

            objFacade.CancelPause(this.GetUserCode(),
                                  FormatHelper.CleanString(this.txtCancelSeq.Text.Trim()),
                                  FormatHelper.CleanString(this.txtCancelReason.Text.Trim()),
                                  queryList);

            cmdQuery_ServerClick(null, null, true);

            ////Message:解除停发成功
            //
            //WebInfoPublish.Publish(this, "$CS_CANCEL_PAUSE_SUCCESS", languageComponent1);


        }

        private bool CheckCancelPauseUI()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(lblPauseCode, txtPauseCode, 40, true));
            manager.Add(new LengthCheck(lblCancelReason, txtCancelReason, 200, true));
            manager.Add(new LengthCheck(lblCancelSeq, txtCancelSeq, 40, false));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }

            return true;
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this.excelExporter.Export();
        }


        #endregion

        #region export
        private string[] FormatExportRecord(object obj)
        {
            return new string[]{
                ((CancelPauseQuery)obj).GetDisplayText("StorageCode") ,
                ((CancelPauseQuery)obj).StackCode ,
                Convert.ToString(((CancelPauseQuery)obj).PauseQty) ,
                Convert.ToString(((CancelPauseQuery)obj).CancelQty) ,
                ((CancelPauseQuery)obj).ItemCode,
                ((CancelPauseQuery)obj).ItemDescription
            };
        }

        private string[] GetColumnHeaderText()
        {
            return new string[] {    
                "StorageCode",
                "StackCode",
                "PauseQty", 
                "CancelQty",
                "ItemCode",
                "ItemDesc"
            };
        }
        #endregion
    }
}
