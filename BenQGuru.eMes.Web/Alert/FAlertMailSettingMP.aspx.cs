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
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using ControlLibrary.Web.Language;

using BenQGuru.eMES.Domain.Alert;
using BenQGuru.eMES.AlertModel;


namespace BenQGuru.eMES.Web.Alert
{
    public partial class FAlertMailSettingMP : BaseMPageMinus
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

            // this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);

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
                //InitFirstClassList();
                InitAlertItemInfo();
                QueryMailSettings();
            }
        }

        protected void cmdSaveMailSubject_ServerClick(object sender, System.EventArgs e)
        {
            if (ValidateSubjectInput())
            {
                AlertItem obj = (AlertItem)_AlertFacade.GetAlertItem(Request["ITEMSEQUENCE"]);
                //obj.MailSubject = this.txtAlertMailSubject.Text.Trim();
                _AlertFacade.UpdateAlertItem(obj);
            }

        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "Edit")
            {
                object obj = this.GetEditObject(row);

                if (obj != null)
                {
                    this.SetEditObject(obj);
                    this._ButtonHelper.PageActionStatusHandle(PageActionType.Update);
                }
            }
        }

        protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
        {
            AlertMailSetting model = (AlertMailSetting)GetEditObject();
            if (model != null)
            {
                model.ItemSequence = model.ItemSequence.ToLower();
                this._AlertFacade.AddAlertMailSetting(model);
                this.RequestData();
                this._ButtonHelper.PageActionStatusHandle(PageActionType.Add);
            }
        }

        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {
            AlertMailSetting model = (AlertMailSetting)GetEditObject();
            if (model != null)
            {
                model.ItemSequence = model.ItemSequence.ToLower();
                this._AlertFacade.UpdateAlertMailSetting(model);
                this.RequestData();

                this._ButtonHelper.PageActionStatusHandle(PageActionType.Save);
            }
        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this._ButtonHelper.PageActionStatusHandle(PageActionType.Cancel);
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
                    AlertMailSetting model = (AlertMailSetting)GetEditObject(row);

                    if (model != null)
                    {
                        modelArray.Add(model);
                    }
                }
                this._AlertFacade.DeleteAlertMailSetting((AlertMailSetting[])modelArray.ToArray(typeof(AlertMailSetting)));
                this.RequestData();
                this._ButtonHelper.PageActionStatusHandle(PageActionType.Delete);
            }
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this._ExcelExporter.Export();
        }

        private void ButtonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Query)
            {

            }
            else if (pageAction == PageActionType.Add)
            {

            }
            else if (pageAction == PageActionType.Update)
            {

            }
            else if (pageAction == PageActionType.Save)
            {

            }
            else if (pageAction == PageActionType.Cancel)
            {

            }
        }

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            if (Request["FROMPAGE"] == "AlertError")
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FAlertErrorMP.aspx?ITEMSEQUENCE=" + Request["ITEMSEQUENCE"]));
            }
            else if (Request["FROMPAGE"] == "AlertErrorCode")
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FAlertErrorCodeMP.aspx?ITEMSEQUENCE=" + Request["ITEMSEQUENCE"]));
            }
            else if (Request["FROMPAGE"] == "AlertOQCNG")
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FAlertOQCNGMP.aspx?ITEMSEQUENCE=" + Request["ITEMSEQUENCE"]));
            }
            else if (Request["FROMPAGE"] == "AlertDirectPass")
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FAlertDirectPassMP.aspx?ITEMSEQUENCE=" + Request["ITEMSEQUENCE"]));
            }
            else if (Request["FROMPAGE"] == "AlertLinePause")
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FAlertLinePauseMP.aspx?ITEMSEQUENCE=" + Request["ITEMSEQUENCE"]));
            }
            else if (Request["FROMPAGE"] == "AlertItem")
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FAlertItemMP.aspx"));
            }
        }

        protected void cmdOpenEmail_ServerClick(object sender, System.EventArgs e)
        {

            string[] mails = this.txtAlertMailRecipients.Text.Trim().Split(';');

            ArrayList arrMails = new ArrayList(mails);

            object[] obj = this._AlertFacade.QueryUserMailByCodes(this.hiddenSelectedID.Value);

            if (obj != null && obj.Length > 0)
            {
                foreach (User user in obj)
                {
                    if (arrMails.Contains(user.UserEmail))
                    {
                        continue;
                    }
                    if (this.txtAlertMailRecipients.Text.Trim() == string.Empty)
                    {
                        this.txtAlertMailRecipients.Text = user.UserEmail;
                    }
                    else
                    {
                        this.txtAlertMailRecipients.Text += ";" + user.UserEmail;
                    }
                }
            }

        }



        protected void ddlItemFirstClass_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //InitSecondClassList();
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
            return this._AlertFacade.QueryAlertMailSettings1(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtAlertItemSequence.Text)), inclusive, exclusive);
        }

        private int GetRowCount()
        {
            return this._AlertFacade.QueryAlertMailSettingsCount1(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtAlertItemSequence.Text)));
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
            base.InitWebGrid();
            this.gridHelper.Grid.Columns.Clear();

            this.gridHelper.AddColumn("SERIAL", "序号", null);
            //this.gridHelper.AddColumn("AlertItemSequence", "项次", 150);
            this.gridHelper.AddColumn("AlertItemDesc", "描述信息", null);
            //this.gridHelper.AddColumn("SSCODE", "产线代码", 100);
            //this.gridHelper.AddColumn("ItemFirstClass", "物料一级分类", 100);
            //this.gridHelper.AddColumn("ItemSecondClass", "物料二级分类", 100);
            this.gridHelper.AddColumn("AlertMailRecipients", "收件人", null);

            this.gridHelper.Grid.Columns.FromKey("SERIAL").Hidden = true;

            this.gridHelper.AddDefaultColumn(true, true);

            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        //private void InitFirstClassList()
        //{
        //    DropDownListBuilder builder = new DropDownListBuilder(this.ddlItemFirstClass);
        //    builder.HandleGetObjectList = new GetObjectListDelegate((new ItemFacade(this.DataProvider)).GetItemFirstClass);
        //    builder.Build("FirstClass", "FirstClass");
        //    this.ddlItemFirstClass.Items.Insert(0, new ListItem("", ""));
        //    this.ddlItemFirstClass.SelectedIndex = 0;

        //    this.ddlItemSecondClass.Items.Insert(0, new ListItem("", ""));
        //    this.ddlItemSecondClass.SelectedIndex = 0;
        //}

        //private void InitSecondClassList()
        //{
        //    this.ddlItemSecondClass.Items.Clear();

        //    ItemFacade itemFacade = new ItemFacade(this.DataProvider);
        //    object[] itemClasses = itemFacade.GetItemSecondClass(FormatHelper.CleanString(this.ddlItemFirstClass.SelectedValue));

        //    if (itemClasses == null)
        //    {
        //        this.ddlItemSecondClass.Items.Insert(0, new ListItem("", ""));
        //        this.ddlItemSecondClass.SelectedIndex = 0;
        //        return;
        //    }

        //    foreach (ItemClass itemClass in itemClasses)
        //    {
        //        this.ddlItemSecondClass.Items.Add(new ListItem(itemClass.SecondClass, itemClass.SecondClass));
        //    }
        //    this.ddlItemSecondClass.Items.Insert(0, new ListItem("", ""));
        //    this.ddlItemSecondClass.SelectedIndex = 0;
        //}

        private void InitAlertItemInfo()
        {
            AlertItem obj = (AlertItem)_AlertFacade.GetAlertItem(Request["ITEMSEQUENCE"]);
            this.txtAlertItemSequence.Text = obj.ItemSequence;
            this.txtAlertItemDesc.Text = obj.Description;
            //this.txtAlertMailSubject.Text = obj.MailSubject;
        }

        private void QueryMailSettings()
        {
            this.RequestData();
            this._ButtonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        #endregion

        #region Get/Set Edit Object

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(lblAlertMailRecipients, txtAlertMailRecipients, 2000, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }

            return true;
        }

        private bool ValidateSubjectInput()
        {
            //PageCheckManager manager = new PageCheckManager();
            //manager.Add(new LengthCheck(lblAlertMailSubject, txtAlertMailSubject, 150, true));

            //if (!manager.Check())
            //{
            //    WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
            //    return false;
            //}

            return true;
        }

        protected DataRow GetGridRow(object obj)
        {
            AlertMailSetting model = (AlertMailSetting)obj;

            //return new UltraGridRow(
            //    new object[]{
            //        "false",
            //        model.Serial,
            //        model.ItemSequence,
            //        txtAlertItemDesc.Text,
            //        model.BIGSSCode,
            //        model.ItemFirstClass,
            //        model.ItemSecondClass,
            //        model.Recipients
            //    });
            DataRow row = this.DtSource.NewRow();
            row["SERIAL"] = model.Serial;
            //row["AlertItemSequence"] = model.ItemSequence;
            row["AlertItemDesc"] = txtAlertItemDesc.Text;
            //row["SSCODE"] = model.BIGSSCode;
            //row["ItemFirstClass"] = model.ItemFirstClass;
            //row["ItemSecondClass"] = model.ItemSecondClass;
            row["AlertMailRecipients"] = model.Recipients;
            return row;
        }

        private object GetEditObject()
        {
            if (this.ValidateInput())
            {
                AlertMailSetting model = this._AlertFacade.CreateNewAlertMailSetting();

                model.ItemSequence = this.txtAlertItemSequence.Text;
                //model.BIGSSCode = this.txtSSEDIT.Text.Trim();
                //model.ItemFirstClass = this.ddlItemFirstClass.SelectedValue;
                //model.ItemSecondClass = this.ddlItemSecondClass.SelectedValue;
                model.MaintainDate = FormatHelper.TODateInt(DateTime.Now.ToShortDateString());
                model.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                model.Recipients = distinctUserMail(this.txtAlertMailRecipients.Text.Trim());
                model.MaintainUser = this.GetUserCode();
                //model.Serial = int.Parse(this.hidSerial.Value);
                return model;
            }
            else
            {
                return null;
            }
        }

        private object GetEditObject(GridRecord row)
        {
            return this._AlertFacade.GetAlertMailSetting(int.Parse(row.Items.FindItemByKey("SERIAL").Text));
        }

        private void SetEditObject(object obj)
        {
            AlertMailSetting model = (AlertMailSetting)obj;

            if (model == null)
            {
                //this.txtSSEDIT.Text = string.Empty;
                //this.ddlItemFirstClass.SelectedIndex = 0;
                //this.ddlItemSecondClass.SelectedIndex = 0;
                this.txtAlertMailRecipients.Text = string.Empty;
                //this.hidSerial.Value = "0";
            }
            else
            {
                //this.txtSSEDIT.Text = model.BIGSSCode;
                this.txtAlertMailRecipients.Text = model.Recipients;
                //this.hidSerial.Value = model.Serial.ToString();

                //try
                //{
                //    this.ddlItemFirstClass.SelectedValue = model.ItemFirstClass;
                //    InitSecondClassList();
                //}
                //catch
                //{
                //    this.ddlItemFirstClass.SelectedIndex = 0;
                //}

                //try
                //{
                //    this.ddlItemSecondClass.SelectedValue = model.ItemSecondClass;
                //}
                //catch
                //{
                //    this.ddlItemSecondClass.SelectedIndex = 0;
                //}

            }
        }

        #endregion

        #region 私有方法
        private string distinctUserMail(string userMail)
        {
            string rtUserMail = "";

            ArrayList arrTemp = new ArrayList();

            string[] mails = userMail.Split(';');

            foreach (string s in mails)
            {
                if (!arrTemp.Contains(s))
                {
                    arrTemp.Add(s);
                    if (rtUserMail == string.Empty)
                    {
                        rtUserMail = s;
                    }
                    else
                    {
                        rtUserMail += ";" + s;
                    }
                }
            }

            return rtUserMail;
        }
        #endregion

        #region Export

        private string[] GetColumnHeaderText()
        {
            return new string[] {	
                //"AlertItemSequence",
                "AlertItemDesc",
                //"SSCODE",
                //"ItemFirstClass",
                //"ItemSecondClass",
                "AlertMailRecipients"
            };
        }

        private string[] FormatExportRecord(object obj)
        {
            AlertMailSetting model = (AlertMailSetting)obj;

            return new string[]{
                //model.ItemSequence,
                txtAlertItemDesc.Text,
                //model.BIGSSCode,
                //model.ItemFirstClass,
                //model.ItemSecondClass,
                model.Recipients
            };
        }

        #endregion
    }
}
