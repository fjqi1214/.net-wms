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

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
//using BenQGuru.eMES.Domain.MOModel;
//using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.Alert;
using BenQGuru.eMES.AlertModel;
using BenQGuru.eMES.Web.Helper;
using ControlLibrary.Web.Language;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.Alert
{
    public partial class FAlertItemMP : BaseMPageMinus
    {
        private System.ComponentModel.IContainer components;

        //private LanguageComponent languageComponent1;
        //private GridHelper gridHelper;
        private ButtonHelper _ButtonHelper;
        private ExcelExporter _ExcelExporter;

        private AlertFacade _AlertFacade;


        #region Form Init

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            //this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            //this.languageComponent1.Language = "CHS";
            //this.languageComponent1.LanguagePackageDir = "";
            //this.languageComponent1.RuntimePage = null;
            //this.languageComponent1.RuntimeUserControl = null;
            //this.languageComponent1.UserControlName = "";

            this._ExcelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            this._ExcelExporter.FileExtension = "xls";
            this._ExcelExporter.LanguageComponent = this.languageComponent1;
            this._ExcelExporter.Page = this;
            this._ExcelExporter.RowSplit = "\r\n";

            this._AlertFacade = new AlertFacade(this.DataProvider);

            //this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, System.EventArgs e)
        {
            InitHander();

            if (!IsPostBack)
            {
                InitPageLanguage(this.languageComponent1, false);

                InitUI();
                InitButton();
                InitWebGrid();
                //InitAlertTypeList();
            }
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.RequestData();
            this._ButtonHelper.PageActionStatusHandle(PageActionType.Query);
            //this.txtAlertItemDescEdit.Text = this.languageComponent1.GetString(this.ddlAlertTypeEdit.SelectedValue);
        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "Edit")
            {
                object obj = this.GetEditObject(row);
                txtOldAlertItemEdit.Text = row.Items.FindItemByKey("AlertItemSequence").Text.Trim();
                if (obj != null)
                {
                    this.SetEditObject(obj);
                    this._ButtonHelper.PageActionStatusHandle(PageActionType.Update);
                    ViewState["editItemPK"] = ((AlertItem)obj).ItemSequence;
                }
            }

            if (commandName == "AlertMailSetup")
            {
                object obj = this.GetEditObject(row);

                if (obj != null)
                {
                    string url = "./FAlertMailSettingMP.aspx?ITEMSEQUENCE=";
                    url += ((AlertItem)obj).ItemSequence;
                    url += "&FROMPAGE=AlertItem";

                    this.Response.Redirect(this.MakeRedirectUrl(url));
                }
            }

            if (commandName == "detail")
            {
                object obj = this.GetEditObject(row);

                if (obj != null)
                {
                    RedirectToDetailPage(((AlertItem)obj).AlertType, ((AlertItem)obj).ItemSequence);
                }
            }
        }

        protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
        {
            AlertItem model = (AlertItem)GetEditObject();
            if (model != null)
            {
                this._AlertFacade.AddAlertItem((AlertItem)model);
                this.RequestData();
                this._ButtonHelper.PageActionStatusHandle(PageActionType.Add);
            }
            //this.txtAlertItemDescEdit.Text = this.languageComponent1.GetString(this.ddlAlertTypeEdit.SelectedValue);
        }

        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {
            AlertItem model = (AlertItem)GetEditObject();
            AlertItem oldModel = new AlertItem();

            oldModel.ItemSequence = txtOldAlertItemEdit.Text;
            if (model != null)
            {
                try
                {
                    this.DataProvider.BeginTransaction();
                    _AlertFacade.DeleteAlertItem(oldModel);

                    this._AlertFacade.AddAlertItem(model);
                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {

                    this.DataProvider.RollbackTransaction();
                    throw ex;
                }
                    
                this.RequestData();

                this._ButtonHelper.PageActionStatusHandle(PageActionType.Save);
            }
            //this.txtAlertItemDescEdit.Text = this.languageComponent1.GetString(this.ddlAlertTypeEdit.SelectedValue);
        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this._ButtonHelper.PageActionStatusHandle(PageActionType.Cancel);
            //this.txtAlertItemDescEdit.Text = this.languageComponent1.GetString(this.ddlAlertTypeEdit.SelectedValue);
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

        protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList rowArray = this.gridHelper.GetCheckedRows();
            if (rowArray != null && rowArray.Count > 0)
            {
                ArrayList modelArray = new ArrayList(rowArray.Count);

                foreach (GridRecord row in rowArray)
                {
                    AlertItem model = (AlertItem)GetEditObject(row);

                    if (model != null)
                    {
                        modelArray.Add(model);
                    }
                }
                this._AlertFacade.DeleteAlertItems((AlertItem[])modelArray.ToArray(typeof(AlertItem)));
                this.RequestData();
                this._ButtonHelper.PageActionStatusHandle(PageActionType.Delete);
            }
            //this.txtAlertItemDescEdit.Text = this.languageComponent1.GetString(this.ddlAlertTypeEdit.SelectedValue);
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this._ExcelExporter.Export();
        }

        private void ButtonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            //if (pageAction == PageActionType.Query)
            //{
            //    this.ddlAlertTypeEdit.Enabled = true;
            //    this.txtAlertItemDescEdit.ReadOnly = false;
            //    ViewState["currentPageAction"] = PageActionType.Query;
            //}
            //else if (pageAction == PageActionType.Add)
            //{
            //    this.ddlAlertTypeEdit.Enabled = true;
            //    this.txtAlertItemDescEdit.ReadOnly = false;
            //    ViewState["currentPageAction"] = PageActionType.Add;
            //}
            //else if (pageAction == PageActionType.Update)
            //{
            //    this.ddlAlertTypeEdit.Enabled = false;
            //    this.txtAlertItemDescEdit.ReadOnly = false;
            //    ViewState["currentPageAction"] = PageActionType.Update;
            //}
            //else if (pageAction == PageActionType.Save)
            //{
            //    this.ddlAlertTypeEdit.Enabled = true;
            //    this.txtAlertItemDescEdit.ReadOnly = false;
            //    ViewState["currentPageAction"] = PageActionType.Save;

            //}
            //else if (pageAction == PageActionType.Cancel)
            //{
            //    this.ddlAlertTypeEdit.Enabled = true;
            //    this.txtAlertItemDescEdit.ReadOnly = false;
            //    ViewState["currentPageAction"] = PageActionType.Cancel;
            //}
        }

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        protected void ddlAlertTypeEdit_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //this.txtAlertItemDescEdit.Text = this.languageComponent1.GetString(this.ddlAlertTypeEdit.SelectedValue);
        }

        #endregion

        #region LoadData

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        private object[] LoadDataSource()
        {
            return this.LoadDataSource(1, int.MaxValue);
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            return this._AlertFacade.QueryAlertItems(string.Empty, inclusive, exclusive);
        }

        private int GetRowCount()
        {
            return this._AlertFacade.QueryAlertItemsCount(string.Empty);
        }

        #endregion

        #region Init Functions

        private void InitHander()
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this._ButtonHelper = new ButtonHelper(this);
            this._ButtonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
            this._ButtonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.ButtonHelper_AfterPageStatusChangeHandle);

            this._ExcelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this._ExcelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this._ExcelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }

        private void InitButton()
        {
            this._ButtonHelper.AddDeleteConfirm();
            this._ButtonHelper.PageActionStatusHandle(PageActionType.Add);
        }

        private void InitWebGrid()
        {
            this.gridHelper.Grid.Columns.Clear();

            this.gridHelper.AddColumn("AlertItemSequence", "项次", null);
            this.gridHelper.AddColumn("AlertItemDesc", "描述信息", null);
            this.gridHelper.AddColumn("AlertItemType", "预警项目", null);
            this.gridHelper.AddColumn("AlertMaintainUser", "设定人员", null);
            this.gridHelper.AddColumn("AlertMaintainDate", "设定日期", null);
            this.gridHelper.AddColumn("AlertMaintainTime", "设定时间", null);
            this.gridHelper.AddLinkColumn("AlertMailSetup", "邮件设定", null);
            this.gridHelper.AddLinkColumn("detail", "详细信息", null);

            this.gridHelper.AddDefaultColumn(true, true);

            this.gridHelper.ApplyLanguage(this.languageComponent1);

        }

        //private void InitAlertTypeList()
        //{
        //    AlertType alertType = new AlertType();

        //    this.ddlAlertTypeEdit.Items.Clear();
        //    this.ddlAlertTypeQuery.Items.Clear();


        //    this.ddlAlertTypeEdit.Items.Add(new ListItem("到货初检拒收预警", "alerttype_ASNReceive"));
        //    this.ddlAlertTypeQuery.Items.Add(new ListItem("到货初检拒收预警", "alerttype_ASNReceive"));
        //    this.ddlAlertTypeQuery.Items.Insert(0, new ListItem("", ""));

        //    //this.txtAlertItemDescEdit.Text = this.languageComponent1.GetString(this.ddlAlertTypeEdit.SelectedValue);
        //}

        #endregion

        #region Get/Set Edit Object

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(lblAlertItemEdit, txtAlertItemEdit, 100, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }

            return true;
        }

        protected DataRow GetGridRow(object obj)
        {
            AlertItem model = (AlertItem)obj;


            DataRow row = this.DtSource.NewRow();
            row["AlertItemSequence"] = model.ItemSequence;
            row["AlertItemDesc"] = model.Description;
            row["AlertItemType"] = this.languageComponent1.GetString(model.AlertType);
            row["AlertMaintainUser"] = model.GetDisplayText("MaintainUser");
            row["AlertMaintainDate"] = FormatHelper.ToDateString(model.MaintainDate);
            row["AlertMaintainTime"] = FormatHelper.ToTimeString(model.MaintainTime);
            return row;

        }

        private object GetEditObject()
        {
            if (this.ValidateInput())
            {
                AlertItem model = this._AlertFacade.CreateNewAlertItem();

                //if (ViewState["currentPageAction"] != null && (string)ViewState["currentPageAction"] == PageActionType.Update)
                //{
                //    model.ItemSequence = (string)ViewState["editItemPK"];
                //}
                //else
                //{
                model.ItemSequence = this.txtAlertItemEdit.Text;
                //}

                model.AlertType = this.txtAlertItemEdit.Text;
                model.Description = string.IsNullOrEmpty(this.txtAlertItemDescEdit.Text) ? txtAlertItemEdit.Text : this.txtAlertItemDescEdit.Text;
                model.MailSubject = this.txtAlertItemEdit.Text;
                model.MailContent = " ";
                model.MaintainUser = this.GetUserCode();
                model.MaintainDate = FormatHelper.TODateInt(DateTime.Now.ToShortDateString());
                model.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);


                return model;
            }
            else
            {
                return null;
            }
        }

        private object GetEditObject(GridRecord row)
        {

            return this._AlertFacade.GetAlertItem(row.Items.FindItemByKey("AlertItemSequence").Text);
        }

        private void SetEditObject(object obj)
        {
            AlertItem model = (AlertItem)obj;

            if (model == null)
            {
                //this.ddlAlertTypeEdit.SelectedIndex = 0;
                //this.txtAlertItemDescEdit.Text = string.Empty;
                this.txtAlertItemDescEdit.Text = string.Empty;
                this.txtAlertItemEdit.Text = string.Empty;
            }
            else
            {
                this.txtAlertItemDescEdit.Text = model.Description;


                this.txtAlertItemEdit.Text = model.AlertType;


            }
        }

        #endregion

        #region Export

        private string[] GetColumnHeaderText()
        {
            return new string[] {	
                "AlertItemSequence",
                "AlertItemDesc",
                "AlertItemType",
                "AlertMaintainUser",
                "AlertMaintainDate",
                "AlertMaintainTime"
            };
        }

        private string[] FormatExportRecord(object obj)
        {
            AlertItem model = (AlertItem)obj;

            return new string[]{
                model.ItemSequence,
                model.Description.ToString(),
                this.languageComponent1.GetString(model.AlertType),
                model.GetDisplayText("MaintainUser"),
                FormatHelper.ToDateString(model.MaintainDate),
                FormatHelper.ToTimeString(model.MaintainTime)
            };
        }

        #endregion

        #region 获取邮件模板及ItemSequence前缀
        private string GetMailTemplate(string alertType)
        {
            string mailTemplate = "";
            switch (alertType)
            {
                case AlertType.AlertType_DirectPass: //直通率预警
                    mailTemplate = "$$datetime$$，产品：$$mcode$$，产线：$$bigsscode$$，下线数 $$molineoutputcount$$，直通率$$directpass$$，达到预警标准，请及时处理。";
                    break;
                case AlertType.AlertType_Error: //故障预警
                    mailTemplate = "$$datetime$$，产品：$$mcode$$，产线：$$bigsscode$$，不良代码：$$ecode$$ 累计达到 $$alertvalue$$次，达到预警标准，请及时处理。";
                    break;
                case AlertType.AlertType_ErrorCode://不良原因预警
                    mailTemplate = "$$datetime$$，产品：$$mcode$$，产线：$$bigsscode$$，不良位置：$$eloc$$，不良原因：$$ecscode$$ 累计达到 $$alertvalue$$次，达到预警标准，请及时处理。";
                    break;
                case AlertType.AlertType_LinePause://停线时长预警
                    mailTemplate = "$$datetime$$，产线：$$bigsscode$$，停线时间达到 $$pauseduration$$，达到预警标准，请及时处理。";
                    break;
                case AlertType.AlertType_OQCNG://OQC抽检故障重复预警
                    mailTemplate = "$$datetime$$，产品：$$mcode$$，不良代码：$$ecode$$，已累计有$$alertvalue$$台不良样本出现相同问题，达到预警标准，请关注。";
                    break;
                case AlertType.AlertType_IQCNG://IQC检验不合格预警
                    mailTemplate = "$$datetime$$，厂商：$$vendor$$，物料：$$itemcode$$，被IQC检验人员认定为不合格，送检单号：$$iqcno$$ ，判不合格原因：$$reason$$ ，请关注！";
                    break;
                case AlertType.AlertType_OQCReject://OQC判退预警
                    mailTemplate = "$$datetime$$，产品：$$mcode$$，产线：$$bigsscode$$，已累计判退$$alertvalue$$批，当前判退批号：$$lotno$$，判退原因：$$reason$$，请关注。";
                    break;

            }
            return mailTemplate;
        }

        private string GetSequencePrefix(string alertType)
        {
            string sequencePrefix = "";
            switch (alertType)
            {
                case AlertType.AlertType_DirectPass: //直通率预警
                    sequencePrefix = "DIRECTPASS";
                    break;
                case AlertType.AlertType_Error: //故障预警
                    sequencePrefix = "ERROR";
                    break;
                case AlertType.AlertType_ErrorCode://不良原因预警
                    sequencePrefix = "ERRORCODE";
                    break;
                case AlertType.AlertType_LinePause://停线时长预警
                    sequencePrefix = "LINEPAUSE";
                    break;
                case AlertType.AlertType_OQCNG://OQC抽检故障重复预警
                    sequencePrefix = "OQCNG";
                    break;
                case AlertType.AlertType_IQCNG://IQC检验不合格预警
                    sequencePrefix = "IQCNG";
                    break;
                case AlertType.AlertType_OQCReject://OQC判退预警
                    sequencePrefix = "OQCREJECT";
                    break;
            }
            return sequencePrefix;
        }
        #endregion

        #region 私有方法
        private void RedirectToDetailPage(string alertType, string itemSeq)
        {
            string url = "";
            switch (alertType)
            {
                case AlertType.AlertType_DirectPass: //直通率预警
                    url = "FAlertDirectPassMP.aspx?ITEMSEQUENCE=" + itemSeq;
                    break;
                case AlertType.AlertType_Error: //故障预警
                    url = "FAlertErrorMP.aspx?ITEMSEQUENCE=" + itemSeq;
                    break;
                case AlertType.AlertType_ErrorCode://不良原因预警
                    url = "FAlertErrorCodeMP.aspx?ITEMSEQUENCE=" + itemSeq;
                    break;
                case AlertType.AlertType_LinePause://停线时长预警
                    url = "FAlertLinePauseMP.aspx?ITEMSEQUENCE=" + itemSeq;
                    break;
                case AlertType.AlertType_OQCNG://OQC抽检故障重复预警
                    url = "FAlertOQCNGMP.aspx?ITEMSEQUENCE=" + itemSeq;

                    break;
                case "alerttype_ASNReceive":
                    url = "FAlertASNReceiveMP.aspx?ITEMSEQUENCE=" + itemSeq;
                    break;
                case AlertType.AlertType_IQCNG://IQC检验不合格预警
                    //ExceptionManager.Raise(this.GetType(), "$Detail_Not_Need");
                    throw new Exception("$Detail_Not_Need");
                case AlertType.AlertType_OQCReject://OQC判退预警
                    //ExceptionManager.Raise(this.GetType(), "$Detail_Not_Need");
                    throw new Exception("$Detail_Not_Need");



            }

            this.Response.Redirect(url);
        }
        #endregion

    }
}
