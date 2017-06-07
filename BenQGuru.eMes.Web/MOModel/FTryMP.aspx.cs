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
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Common;
using System.IO;
using System.Text;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.MOModel
{
    public partial class FTryMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private TryFacade facade = null;

        string tryStatusInfo = string.Empty;

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
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                chbGenerateLot.Visible = false;
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        protected void drpTryStatus_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpTryStatusQuery.Items.Insert(0, new ListItem("", ""));
                this.drpTryStatusQuery.Items.Insert(1, new ListItem(this.languageComponent1.GetString(TryStatus.STATUS_INITIAL), TryStatus.STATUS_INITIAL));
                this.drpTryStatusQuery.Items.Insert(2, new ListItem(this.languageComponent1.GetString(TryStatus.STATUS_RELEASE), TryStatus.STATUS_RELEASE));
                this.drpTryStatusQuery.Items.Insert(3, new ListItem(this.languageComponent1.GetString(TryStatus.STATUS_PRODUCE), TryStatus.STATUS_PRODUCE));
                this.drpTryStatusQuery.Items.Insert(4, new ListItem(this.languageComponent1.GetString(TryStatus.STATUS_PAUSE), TryStatus.STATUS_PAUSE));
                this.drpTryStatusQuery.Items.Insert(4, new ListItem(this.languageComponent1.GetString(TryStatus.STATUS_FINISH), TryStatus.STATUS_FINISH));
            }
        }

        protected void drpTryTypeQuery_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpTryTypeQuery.Items.Insert(0, new ListItem("", ""));
                this.drpTryTypeQuery.Items.Insert(1, new ListItem(this.languageComponent1.GetString(TryType.TYPE_TRYPART), TryType.TYPE_TRYPART));
                this.drpTryTypeQuery.Items.Insert(2, new ListItem(this.languageComponent1.GetString(TryType.TYPE_IMPROTEPRODUCT), TryType.TYPE_IMPROTEPRODUCT));
            }
        }

        protected void drpTryTypeEdit_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpTryTypeEdit.Items.Insert(0, new ListItem("", ""));
                this.drpTryTypeEdit.Items.Insert(1, new ListItem(this.languageComponent1.GetString(TryType.TYPE_TRYPART), TryType.TYPE_TRYPART));
                this.drpTryTypeEdit.Items.Insert(2, new ListItem(this.languageComponent1.GetString(TryType.TYPE_IMPROTEPRODUCT), TryType.TYPE_IMPROTEPRODUCT));
            }
        }

        protected void drpLotWaitReslutEdit_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpLotWaitReslutEdit.Items.Insert(0, new ListItem("", ""));
                this.drpLotWaitReslutEdit.Items.Insert(1, new ListItem("是", "1"));
                this.drpLotWaitReslutEdit.Items.Insert(2, new ListItem("否", "0"));
            }
        }

        protected void drpBothChangedEdit_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpBothChangedEdit.Items.Insert(0, new ListItem("", ""));
                this.drpBothChangedEdit.Items.Insert(1, new ListItem("是", "1"));
                this.drpBothChangedEdit.Items.Insert(2, new ListItem("否", "0"));
            }
        }

        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("TryCode", "试流单编号", null);

            this.gridHelper.AddLinkColumn("Download", "下载", null);

            this.gridHelper.AddColumn("TryStatus", "试流单状态", null);
            this.gridHelper.AddColumn("PlanQty", "计划数量", null);
            this.gridHelper.AddColumn("ActualQty", "实际数量", null);
            this.gridHelper.AddColumn("MaterialCode", "物料代码", null);
            this.gridHelper.AddColumn("MaterialDescription", "物料描述", null);
            this.gridHelper.AddColumn("Dept", "部门", null);
            this.gridHelper.AddColumn("VendorName", "供货厂家", null);

            this.gridHelper.AddColumn("TryType", "试流类型", null);
            this.gridHelper.AddColumn("TryReason", "试流原因（或改进事由）", null);
            this.gridHelper.AddColumn("SoftwareVersion", "软件版本", null);
            this.gridHelper.AddColumn("WaitTry", "后续批量是否待试产结果", null);
            this.gridHelper.AddColumn("Change", "软、硬件是否配套更改", null);
            this.gridHelper.AddColumn("BurnINDuration", "老化时间", null);

            this.gridHelper.AddColumn("Result", "结论", null);
            this.gridHelper.AddColumn("Memo", "备注", null);
            this.gridHelper.AddColumn("TryDocument", "试流文件", null);
            this.gridHelper.AddColumn("CreateUser", "创建人", null);
            this.gridHelper.AddColumn("CreateDate", "创建日期", null);
            this.gridHelper.AddColumn("ReleaseUser", "下发人", null);
            this.gridHelper.AddColumn("ReleaseDate", "下发日期", null);
            this.gridHelper.AddColumn("FinishUser", "关单人", null);
            this.gridHelper.AddColumn("FinishDate", "关单日期", null);

            this.gridHelper.AddDefaultColumn(true, true);

            this.gridWebGrid.Columns.FromKey("TryDocument").Hidden = true;
            ((BoundDataField)this.gridWebGrid.Columns.FromKey("ActualQty")).HtmlEncode = false;
            ((BoundDataField)this.gridWebGrid.Columns.FromKey("Download")).CssClass = "tdDownLoad";
            //this.gridWebGrid.Columns.FromKey("Download").CellButtonStyle.BackgroundImage = this.Request.Url.Segments[0] + this.Request.Url.Segments[1] + "skin/image/Print.gif";
            //this.gridWebGrid.Columns.FromKey("Download").CellStyle.BackgroundImage = this.Request.Url.Segments[0] + this.Request.Url.Segments[1] + "skin/image/Print.gif";

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    ((TryWithDesc)obj).TryCode.ToString(),
            //                    "",
            //                    languageComponent1.GetString(((TryWithDesc)obj).Status.ToString()),
            //                    ((TryWithDesc)obj).PlanQty.ToString(),
            //                    this.GetTryCodeLink( ((TryWithDesc)obj).TryCode.ToString() ,
            //                    ((TryWithDesc)obj).ActualQty.ToString()),
            //                    ((TryWithDesc)obj).ItemCode.ToString(),
            //                    ((TryWithDesc)obj).ItemDesc.ToString(),
            //                    ((TryWithDesc)obj).Department.ToString(),
            //                    ((TryWithDesc)obj).VendorName.ToString(),

            //                    languageComponent1.GetString(((TryWithDesc)obj).TryType),
            //                    ((TryWithDesc)obj).TryReason,
            //                    ((TryWithDesc)obj).SoftVersion,
            //                    languageComponent1.GetString(((TryWithDesc)obj).WaitTry),
            //                    languageComponent1.GetString(((TryWithDesc)obj).Change),
            //                    ((TryWithDesc)obj).BurnINDuration,
            //                    ((TryWithDesc)obj).Result.ToString(),
            //                    ((TryWithDesc)obj).Memo.ToString(),
            //                    ((TryWithDesc)obj).TryDocument.ToString(),
            //                    //((TryWithDesc)obj).CreateUser.ToString(),
            //                  ((TryWithDesc)obj).GetDisplayText("CreateUser"),
            //                    FormatHelper.ToDateString(((TryWithDesc)obj).CreateDate),
            //                    //((TryWithDesc)obj).ReleaseUser.ToString(),
            //                     ((TryWithDesc)obj).GetDisplayText("ReleaseUser"),
            //                    FormatHelper.ToDateString(((TryWithDesc)obj).ReleaseDate),
            //                    //((TryWithDesc)obj).FinishUser.ToString(),
            //                     ((TryWithDesc)obj).GetDisplayText("FinishUser"),
            //                    FormatHelper.ToDateString(((TryWithDesc)obj).FinishDate)});
            DataRow row = this.DtSource.NewRow();
            row["TryCode"] = ((TryWithDesc)obj).TryCode.ToString();
            row["TryStatus"] = languageComponent1.GetString(((TryWithDesc)obj).Status.ToString());
            row["PlanQty"] = ((TryWithDesc)obj).PlanQty.ToString();
            row["ActualQty"] = this.GetTryCodeLink( ((TryWithDesc)obj).TryCode.ToString() ,
                                ((TryWithDesc)obj).ActualQty.ToString());
            row["MaterialCode"] = ((TryWithDesc)obj).ItemCode.ToString();
            row["MaterialDescription"] = ((TryWithDesc)obj).ItemDesc.ToString();
            row["Dept"] = ((TryWithDesc)obj).Department.ToString();
            row["VendorName"] = ((TryWithDesc)obj).VendorName.ToString();
            row["TryType"] = languageComponent1.GetString(((TryWithDesc)obj).TryType);
            row["TryReason"] = ((TryWithDesc)obj).TryReason;
            row["SoftwareVersion"] = ((TryWithDesc)obj).SoftVersion;
            row["WaitTry"] = languageComponent1.GetString(((TryWithDesc)obj).WaitTry);
            row["Change"] = languageComponent1.GetString(((TryWithDesc)obj).Change);
            row["BurnINDuration"] = ((TryWithDesc)obj).BurnINDuration;
            row["Result"] = ((TryWithDesc)obj).Result.ToString();
            row["Memo"] = ((TryWithDesc)obj).Memo.ToString();
            row["TryDocument"] = ((TryWithDesc)obj).TryDocument.ToString();
            row["CreateUser"] = ((TryWithDesc)obj).GetDisplayText("CreateUser");
            row["CreateDate"] = FormatHelper.ToDateString(((TryWithDesc)obj).CreateDate);
            row["ReleaseUser"] = ((TryWithDesc)obj).GetDisplayText("ReleaseUser");
            row["ReleaseDate"] = FormatHelper.ToDateString(((TryWithDesc)obj).ReleaseDate);
            row["FinishUser"] = ((TryWithDesc)obj).GetDisplayText("FinishUser");
            row["FinishDate"] = FormatHelper.ToDateString(((TryWithDesc)obj).FinishDate);
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new TryFacade(base.DataProvider);
            }

            return this.facade.QueryTry(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtTryCodeQuery.Text.ToUpper())),
                this.drpTryStatusQuery.SelectedValue,
                FormatHelper.CleanString(this.txtMaterialCodeQuery.Text.ToUpper()),
                FormatHelper.CleanString(this.txtDeptQuery.Text.ToUpper()),
                FormatHelper.CleanString(this.txtVendorNameQuery.Text.ToUpper()),
                FormatHelper.TODateInt(this.dateCreatDateQuery.Text),
                FormatHelper.CleanString(this.txtCreateUserQuery.Text.ToUpper()),
                this.drpTryTypeQuery.SelectedValue,
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new TryFacade(base.DataProvider);
            }

            return this.facade.QueryTryCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtTryCodeQuery.Text)),
                this.drpTryStatusQuery.SelectedValue,
                FormatHelper.CleanString(this.txtMaterialCodeQuery.Text),
                FormatHelper.CleanString(this.txtDeptQuery.Text),
                FormatHelper.CleanString(this.txtVendorNameQuery.Text),
                FormatHelper.TODateInt(this.dateCreatDateQuery.Text),
                FormatHelper.CleanString(this.txtCreateUserQuery.Text.ToUpper()),
                this.drpTryTypeQuery.SelectedValue
            );
        }

        protected override void Grid_ClickCellButton(GridRecord row, string commandName)
        {
           // base.Grid_ClickCellButton(sender, e);

            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            UserFacade userFacade = new UserFacade(this.DataProvider);

            if (commandName == "Download")
            {
                string tryCode = row.Items.FindItemByKey("TryCode").Value.ToString();
                if (facade == null)
                {
                    facade = new TryFacade(base.DataProvider);
                }
                object trySelected = facade.GetTry(tryCode);
                if (trySelected == null)
                {
                    ExceptionManager.Raise(this.GetType(), "试流单不存在");
                }
                Try selectedTry = trySelected as Try;

                string originalFilePath = this.Request.PhysicalApplicationPath + @"download\TryCode.htm";
                if (!File.Exists(originalFilePath))
                {
                    ExceptionManager.Raise(this.GetType(), "文件[TryCode.htm]不存在");
                }

                //获取Deparment Name
                string deparmentCode = selectedTry.Department;
                string deparmentName = " ";
                if (deparmentCode.Trim().Length > 0)
                {
                    Domain.BaseSetting.Parameter department = (Domain.BaseSetting.Parameter)systemSettingFacade.GetParameter(selectedTry.Department, "DEPARTMENT");
                    if (department != null)
                    {
                        deparmentName = department.ParameterAlias;
                    }
                }

                //获取Create User Name
                string createUserCode = selectedTry.CreateUser;
                string createUserName = " ";
                if (createUserCode.Trim().Length > 0)
                {
                    User createUser = (User)userFacade.GetUser(createUserCode);
                    if (createUser != null)
                    {
                        createUserName = createUser.UserName;
                    }
                }

                string fileContent = File.ReadAllText(originalFilePath, Encoding.GetEncoding("GB2312"));
                fileContent = fileContent.Replace("$$TryCode$$", selectedTry.TryCode);
                fileContent = fileContent.Replace("$$Department$$", deparmentName);
                fileContent = fileContent.Replace("$$CreateUser$$", createUserName);
                fileContent = fileContent.Replace("$$ReleaseDate$$", selectedTry.ReleaseDate == 0 ? " " : FormatHelper.ToDateString(selectedTry.ReleaseDate, "/"));
                //fileContent = fileContent.Replace("$$ReleaseDate$$", selectedTry.ReleaseDate == 0 ? " " : selectedTry.ReleaseDate.ToString());
                fileContent = fileContent.Replace("$$MaterialCode$$", selectedTry.ItemCode == "" ? " " : selectedTry.ItemCode);
                fileContent = fileContent.Replace("$$MaterialDescription$$", row.Items.FindItemByKey("Dept").Value.ToString() == "" ? " " : row.Items.FindItemByKey("Dept").Value.ToString());
                fileContent = fileContent.Replace("$$PlanQuantity$$", selectedTry.PlanQty.ToString());
                fileContent = fileContent.Replace("$$VendorCode$$", selectedTry.VendorName == "" ? " " : selectedTry.VendorName);
                fileContent = fileContent.Replace("$$Result$$", selectedTry.Result == "" ? " " : selectedTry.Result);
                fileContent = fileContent.Replace("$$Memo$$", selectedTry.Memo == "" ? " " : selectedTry.Memo);
                //fileContent = fileContent.Replace("$$PrintDateTime$$", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss", new System.Globalization.CultureInfo("en-US")));
                fileContent = fileContent.Replace("$$TryType$$", selectedTry.TryType.Trim() == "" ? " " : this.languageComponent1.GetString(selectedTry.TryType));
                fileContent = fileContent.Replace("$$TryReason$$", selectedTry.TryReason == "" ? " " : selectedTry.TryReason);
                fileContent = fileContent.Replace("$$SoftVersion$$", selectedTry.SoftVersion == "" ? " " : selectedTry.SoftVersion);
                fileContent = fileContent.Replace("$$WaitTry$$", selectedTry.WaitTry == "" ? " " : this.languageComponent1.GetString(selectedTry.WaitTry));
                fileContent = fileContent.Replace("$$Change$$", selectedTry.Change == "" ? " " : this.languageComponent1.GetString(selectedTry.Change));
                fileContent = fileContent.Replace("$$BurnINDuration$$", selectedTry.BurnINDuration == "" ? " " : selectedTry.BurnINDuration);

                string downloadPhysicalPath = this.Request.PhysicalApplicationPath + @"upload\";
                if (!Directory.Exists(downloadPhysicalPath))
                {
                    Directory.CreateDirectory(downloadPhysicalPath);
                }

                string filename = string.Format("{0}_{1}_{2}", row.Items.FindItemByKey("TryCode").Value.ToString(), FormatHelper.TODateInt(System.DateTime.Now).ToString(), FormatHelper.TOTimeInt(System.DateTime.Now).ToString());
                string filepath = string.Format(@"{0}{1}{2}", downloadPhysicalPath, filename, ".xls");

                while (File.Exists(filepath))
                {
                    filename = string.Format("{0}_{1}", filename, "0");
                    filepath = string.Format(@"{0}{1}{2}", downloadPhysicalPath, filename, ".xls");
                }

                StreamWriter writer = new StreamWriter(filepath, false, System.Text.Encoding.GetEncoding("GB2312"));
                writer.Write(fileContent);
                writer.Flush();
                writer.Close();

                this.DownloadFile(filename);
            }
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (facade == null)
            {
                facade = new TryFacade(base.DataProvider);
            }
            this.facade.AddTRY((Try)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (facade == null)
            {
                facade = new TryFacade(base.DataProvider);
            }

            this.DataProvider.BeginTransaction();
            try
            {
                foreach (Try tryTemp in (Try[])domainObjects.ToArray(typeof(Try)))
                {
                    if (tryTemp.Status != TryStatus.STATUS_INITIAL && tryTemp.Status != TryStatus.STATUS_RELEASE)
                    {
                        throw new Exception("$Current_TryCode:" + tryTemp.TryCode + ", $Current_Status:" + this.languageComponent1.GetString(tryTemp.Status));
                    }
                    this.facade.DeleteTry(tryTemp);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_CanNotDeleteTryCode", ex);
            }
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (facade == null)
            {
                facade = new TryFacade(base.DataProvider);
            }
            this.facade.UpdateTry((Try)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                UseStatus(false);

            }

            if (pageAction == PageActionType.Update)
            {
                UseStatus(true);
            }
        }

        private void UseStatus(bool forEdit)
        {
            if (forEdit)
            {
                switch (tryStatusInfo)
                {
                    case TryStatus.STATUS_INITIAL:
                        this.txtTryCodeEdit.ReadOnly = true;   //update

                        this.txtMaterialCodeEdit.Readonly = false;
                        this.txtPlanQtyEdit.ReadOnly = false;
                        this.txtDeptEdit.Readonly = false;
                        this.txtVendorNameEdit.ReadOnly = false;
                        this.chbGenerateLot.Enabled = true;
                        this.txtResultEdit.ReadOnly = false;
                        this.txtMemoEdit.ReadOnly = false;
                        this.cmdSave.Disabled = false;
                        break;
                    case TryStatus.STATUS_RELEASE:
                        this.txtTryCodeEdit.ReadOnly = true;
                        this.chbGenerateLot.Enabled = false;

                        this.txtMaterialCodeEdit.Readonly = false;
                        this.txtPlanQtyEdit.ReadOnly = false;
                        this.txtDeptEdit.Readonly = false;
                        this.txtVendorNameEdit.ReadOnly = false;
                        this.txtResultEdit.ReadOnly = false;
                        this.txtMemoEdit.ReadOnly = false;
                        this.cmdSave.Disabled = false;
                        break;
                    case TryStatus.STATUS_PRODUCE:
                        this.txtTryCodeEdit.ReadOnly = true;
                        this.chbGenerateLot.Enabled = false;

                        this.txtMaterialCodeEdit.Readonly = false;
                        this.txtPlanQtyEdit.ReadOnly = false;
                        this.txtDeptEdit.Readonly = false;
                        this.txtVendorNameEdit.ReadOnly = false;
                        this.txtResultEdit.ReadOnly = false;
                        this.txtMemoEdit.ReadOnly = false;
                        this.cmdSave.Disabled = false;
                        break;
                    case TryStatus.STATUS_PAUSE:
                        this.txtTryCodeEdit.ReadOnly = true;   //update

                        this.txtMaterialCodeEdit.Readonly = false;
                        this.txtPlanQtyEdit.ReadOnly = false;
                        this.txtDeptEdit.Readonly = false;
                        this.txtVendorNameEdit.ReadOnly = false;
                        this.chbGenerateLot.Enabled = true;
                        this.txtResultEdit.ReadOnly = false;
                        this.txtMemoEdit.ReadOnly = false;
                        this.cmdSave.Disabled = false;
                        break;
                    case TryStatus.STATUS_FINISH:
                        this.txtTryCodeEdit.ReadOnly = true;
                        this.txtMaterialCodeEdit.Readonly = true;
                        this.txtPlanQtyEdit.ReadOnly = true;
                        this.txtDeptEdit.Readonly = true;
                        this.txtVendorNameEdit.ReadOnly = true;
                        this.chbGenerateLot.Enabled = false;
                        this.txtResultEdit.ReadOnly = true;
                        this.txtMemoEdit.ReadOnly = true;
                        this.cmdSave.Disabled = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                this.txtTryCodeEdit.ReadOnly = false;  //add

                this.txtMaterialCodeEdit.Readonly = false;
                this.txtPlanQtyEdit.ReadOnly = false;
                this.txtDeptEdit.Readonly = false;
                this.txtVendorNameEdit.ReadOnly = false;
                this.chbGenerateLot.Enabled = true;
                this.txtResultEdit.ReadOnly = false;
                this.txtMemoEdit.ReadOnly = false;
                this.cmdSave.Disabled = true;
            }
        }

        protected void cmdRelease_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count <= 0)
            {
                return;
            }

            this.SetTryStatus(TryStatus.STATUS_RELEASE);
        }


        protected void cmdReleaseCancle_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count <= 0)
            {
                return;
            }

            this.SetTryStatus(TryStatus.STATUS_INITIAL);
        }

        protected void cmdPending_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count <= 0)
            {
                return;
            }

            this.SetTryStatus(TryStatus.STATUS_PAUSE);
        }

        protected void cmdPendCancle_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count <= 0)
            {
                return;
            }

            this.SetTryStatus(TryStatus.STATUS_PRODUCE);
        }

        protected void cmdTryClose_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count <= 0)
            {
                return;
            }

            this.SetTryStatus(TryStatus.STATUS_FINISH);
        }

        private void SetTryStatus(string newStatus)
        {
            if (facade == null)
            {
                facade = new TryFacade(this.DataProvider);
            }

            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList objs = new ArrayList(array.Count);
                DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                try
                {
                    foreach (GridRecord row in array)
                    {
                        object obj = this.GetEditObject(row);
                        Try tryObj = null;
                        if (obj != null)
                        {
                            tryObj = this.facade.GetTry(((Try)obj).TryCode) as Try;
                            string status = tryObj.Status;

                            if (newStatus == TryStatus.STATUS_RELEASE)     //release button
                            {
                                if (status == TryStatus.STATUS_INITIAL)
                                {
                                    tryObj.Status = TryStatus.STATUS_RELEASE;
                                    tryObj.ReleaseUser = this.GetUserCode();
                                    tryObj.ReleaseDate = currentDateTime.DBDate;
                                    tryObj.ReleaseTime = currentDateTime.DBTime;
                                }
                                else
                                {
                                    throw new Exception(string.Format("$Current_TryCode:{0} $Current_Status: {1}", tryObj.TryCode, this.languageComponent1.GetString(status)));
                                }
                            }

                            if (newStatus == TryStatus.STATUS_PAUSE)   //pause button
                            {
                                if (status == TryStatus.STATUS_PRODUCE)
                                {
                                    tryObj.Status = TryStatus.STATUS_PAUSE;
                                }
                                else
                                {
                                    throw new Exception(string.Format("$Current_TryCode:{0} $Current_Status: {1}", tryObj.TryCode, this.languageComponent1.GetString(status)));
                                }
                            }

                            if (newStatus == TryStatus.STATUS_INITIAL)    //cancle release button
                            {
                                if (status == TryStatus.STATUS_RELEASE)
                                {
                                    tryObj.Status = TryStatus.STATUS_INITIAL;
                                    tryObj.ReleaseDate = 0;
                                    tryObj.ReleaseTime = 0;
                                    tryObj.ReleaseUser = "";
                                }
                                else
                                {
                                    throw new Exception(string.Format("$Current_TryCode:{0} $Current_Status: {1}", tryObj.TryCode, this.languageComponent1.GetString(status)));
                                }
                            }

                            if (newStatus == TryStatus.STATUS_PRODUCE)   //cancle pend button
                            {
                                if (status == TryStatus.STATUS_PAUSE)
                                {
                                    tryObj.Status = TryStatus.STATUS_PRODUCE;
                                }
                                else
                                {
                                    throw new Exception(string.Format("$Current_TryCode:{0} $Current_Status: {1}", tryObj.TryCode, this.languageComponent1.GetString(status)));
                                }
                            }

                            if (newStatus == TryStatus.STATUS_FINISH)  //close button
                            {
                                if (status != TryStatus.STATUS_FINISH)
                                {
                                    tryObj.Status = TryStatus.STATUS_FINISH;
                                    tryObj.FinishDate = currentDateTime.DBDate;
                                    tryObj.FinishTime = currentDateTime.DBTime;
                                    tryObj.FinishUser = this.GetUserCode();
                                }
                                else
                                {
                                    throw new Exception(string.Format("$Current_TryCode:{0} $Current_Status: {1}", tryObj.TryCode, this.languageComponent1.GetString(status)));
                                }
                            }

                            objs.Add(tryObj);
                        }
                    }

                    this.facade.TryStatusChanged((Try[])objs.ToArray(typeof(Try)));

                    this.RequestData();
                }
                catch (Exception ex)
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_TryStatusChanged", ex);
                }
            }
        }

        private void RequestData()
        {
            // 2005-04-06
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);

            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (facade == null)
            {
                facade = new TryFacade(base.DataProvider);
            }

            Try tryObj = null;
            DBDateTime dateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            if (this.txtTryCodeEdit.ReadOnly)
            {
                tryObj = this.facade.GetTry(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtTryCodeEdit.Text, 40))) as Try;
            }
            else
            {
                tryObj = this.facade.CreateNewTry();
                tryObj.TryCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtTryCodeEdit.Text, 40));
                tryObj.Status = TryStatus.STATUS_INITIAL;

                tryObj.CreateUser = this.GetUserCode();
                tryObj.CreateDate = dateTime.DBDate;
                tryObj.CreateTime = dateTime.DBTime;

                tryObj.ActualQty = 0;
            }

            tryObj.MaintainUser = this.GetUserCode();
            tryObj.MaintainDate = dateTime.DBDate;
            tryObj.MaintainTime = dateTime.DBTime;

            tryObj.ItemCode = this.txtMaterialCodeEdit.Text;
            tryObj.Department = this.txtDeptEdit.Text;

            string planqty = FormatHelper.CleanString(this.txtPlanQtyEdit.Text);

            if (planqty != null && planqty.Length != 0)
            {
                tryObj.PlanQty = int.Parse(planqty);
            }
            else
            {
                tryObj.PlanQty = 0;
            }

            if (this.chbGenerateLot.Checked)
            {
                tryObj.LinkLot = "Y";
            }
            else
            {
                tryObj.LinkLot = "N";
            }
            tryObj.VendorName = FormatHelper.CleanString(this.txtVendorNameEdit.Text);
            //tryObj.TryDocument = FormatHelper.CleanString(this.fileInit.Value);
            tryObj.TryDocument = "";
            tryObj.Memo = FormatHelper.CleanString(this.txtMemoEdit.Text);
            tryObj.Result = FormatHelper.CleanString(this.txtResultEdit.Text);

            tryObj.TryType = this.drpTryTypeEdit.SelectedValue;
            tryObj.TryReason = FormatHelper.CleanString(this.txtTryReasonEdit.Text);
            tryObj.SoftVersion = FormatHelper.CleanString(this.txtSoftWareVersionEdit.Text);
            tryObj.WaitTry = drpLotWaitReslutEdit.SelectedValue;
            tryObj.Change = drpBothChangedEdit.SelectedValue;
            tryObj.BurnINDuration = FormatHelper.CleanString(this.txtAgingDateEdit.Text);

            return tryObj;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (facade == null)
            {
                facade = new TryFacade(base.DataProvider);
            }
            object obj = facade.GetTry(row.Items.FindItemByKey("TryCode").Text.ToString());

            if (obj != null)
            {
                return (Try)obj;
            }

            return null;

        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtTryCodeEdit.Text = string.Empty;
                this.txtMaterialCodeEdit.Text = string.Empty;
                this.txtPlanQtyEdit.Text = string.Empty;
                this.txtDeptEdit.Text = string.Empty;
                this.txtVendorNameEdit.Text = string.Empty;
                this.chbGenerateLot.Checked = false;
                //this.fileInit.Value = "";
                this.txtResultEdit.Text = string.Empty;
                this.txtMemoEdit.Text = string.Empty;
                this.txtActualQty.Text = string.Empty;

                this.drpTryTypeEdit.SelectedIndex = 0;
                this.txtTryReasonEdit.Text = string.Empty;
                this.txtSoftWareVersionEdit.Text = string.Empty;
                this.drpBothChangedEdit.SelectedIndex = 0;
                this.drpLotWaitReslutEdit.SelectedIndex = 0;
                this.txtAgingDateEdit.Text = string.Empty;
                return;
            }

            this.txtTryCodeEdit.Text = ((Try)obj).TryCode.ToString();
            this.txtMaterialCodeEdit.Text = ((Try)obj).ItemCode.ToString();
            this.txtPlanQtyEdit.Text = ((Try)obj).PlanQty.ToString();
            this.txtDeptEdit.Text = ((Try)obj).Department.ToString();
            this.txtVendorNameEdit.Text = ((Try)obj).VendorName.ToString();
            this.txtActualQty.Text = ((Try)obj).ActualQty.ToString();
            string autoGenerateLot = ((Try)obj).LinkLot.ToString();
            if (autoGenerateLot == "Y")
            {
                this.chbGenerateLot.Checked = true;
            }
            else
            {
                this.chbGenerateLot.Checked = false;
            }
            //this.fileInit.Value = ((Try)obj).TryDocument.ToString();
            this.txtResultEdit.Text = ((Try)obj).Result.ToString();
            this.txtMemoEdit.Text = ((Try)obj).Memo.ToString();
            tryStatusInfo = ((Try)obj).Status.ToString();
            this.drpTryTypeEdit.SelectedValue = ((Try)obj).TryType.Trim();
            this.txtTryReasonEdit.Text = ((Try)obj).TryReason.ToString();
            this.txtSoftWareVersionEdit.Text = ((Try)obj).SoftVersion.ToString();
            this.drpBothChangedEdit.SelectedValue = ((Try)obj).Change;
            this.drpLotWaitReslutEdit.SelectedValue = ((Try)obj).WaitTry;
            this.txtAgingDateEdit.Text = ((Try)obj).BurnINDuration.ToString();
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblTryCodeEdit, this.txtTryCodeEdit, 40, true));
            if (this.txtPlanQtyEdit.Text.Trim().Length > 0)
            {
                manager.Add(new NumberCheck(this.lblQTYAndLoopEdit, this.txtPlanQtyEdit, 0, 99999999, false));
            }

            manager.Add(new LengthCheck(this.lblVendorNameEdit, this.txtVendorNameEdit, 100, false));
            manager.Add(new LengthCheck(this.lblTryResultEdit, this.txtResultEdit, 200, false));
            manager.Add(new LengthCheck(this.lblMemoEdit, this.txtMemoEdit, 1000, false));
            manager.Add(new LengthCheck(this.lblTryTypeEdit, this.drpTryTypeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblAgingDateEdit, this.txtAgingDateEdit, 40, false));
            manager.Add(new LengthCheck(this.lblSoftWareVersionEdit, this.txtSoftWareVersionEdit, 40, false));
            manager.Add(new LengthCheck(this.lblTryReasonEdit, this.txtTryReasonEdit, 1000, false));


            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            if (this.txtPlanQtyEdit.Text.Trim().Length > 0
                && this.txtActualQty.Text.Trim().Length > 0
                && Convert.ToInt32(this.txtPlanQtyEdit.Text.Trim()) < Convert.ToInt32(this.txtActualQty.Text.Trim()))
            {
                WebInfoPublish.Publish(this, "$PlanQty_big_QctualQty", this.languageComponent1);
                return false;
            }
            return true;
        }

        #endregion

        #region Export


        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{((TryWithDesc)obj).TryCode.ToString(),
                                languageComponent1.GetString(((TryWithDesc)obj).Status.ToString()),
                                ((TryWithDesc)obj).PlanQty.ToString(),
                                ((TryWithDesc)obj).ActualQty.ToString(),
                                ((TryWithDesc)obj).ItemCode.ToString(),
                                ((TryWithDesc)obj).ItemDesc.ToString(),
                                ((TryWithDesc)obj).Department.ToString(),
                                ((TryWithDesc)obj).VendorName.ToString(),
                                languageComponent1.GetString(((TryWithDesc)obj).TryType),
                                ((TryWithDesc)obj).TryReason,
                                ((TryWithDesc)obj).SoftVersion,
                                languageComponent1.GetString(((TryWithDesc)obj).WaitTry),
                                languageComponent1.GetString(((TryWithDesc)obj).Change),
                                ((TryWithDesc)obj).BurnINDuration,
                                ((TryWithDesc)obj).Result.ToString(),
                                ((TryWithDesc)obj).Memo.ToString(),
                                ((TryWithDesc)obj).TryDocument.ToString(),
                                //((TryWithDesc)obj).CreateUser.ToString(),
                                 ((TryWithDesc)obj).GetDisplayText("CreateUser"),
                                FormatHelper.ToDateString(((TryWithDesc)obj).CreateDate),
                                //((TryWithDesc)obj).ReleaseUser.ToString(),
                                 ((TryWithDesc)obj).GetDisplayText("ReleaseUser"),
                                FormatHelper.ToDateString(((TryWithDesc)obj).ReleaseDate),
                                //((TryWithDesc)obj).FinishUser.ToString(),
                                 ((TryWithDesc)obj).GetDisplayText("FinishUser"),
                                FormatHelper.ToDateString(((TryWithDesc)obj).FinishDate)};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"TryCode",
                                    "TryStatus",
                                    "PlanQty",	
                                    "ActualQty",
                                    "MaterialCode",
                                    "MaterialDescription",
                                    "Dept",	
                                    "VendorName",
                                    "TryType",
                                    "TryReason",
                                    "SoftwareVersion",
                                    "WaitTry",
                                    "Change",
                                    "BurnINDuration",
                                    "Result",
                                    "Memo",
                                    "TryDocument",	
                                    "CreateUser",
                                    "CreateDate",
                                    "ReleaseUser",
                                    "ReleaseDate",	
                                    "FinishUser",
                                    "FinishDate"};
        }

        #endregion

        private string GetTryCodeLink(string tryCode, string actualQty)
        {
            return string.Format("<a href='../MOModel/FRcardListOfTry.aspx?TryCode={0}'>{1}</a>", tryCode, actualQty);
        }

    }
}
