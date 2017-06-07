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
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FRouteMP 的摘要说明。
    /// </summary>
    public partial class FEQPTSLOG : BaseMPageNew
    {
        protected System.Web.UI.WebControls.Label lblRouteTitle;


        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        //BenQGuru.eMES.BaseSetting.SystemSettingFacade sysFacade = null;
        private BenQGuru.eMES.Material.EquipmentFacade _facade = null;//	new BaseModelFacadeFactory().Create();

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

        #region property
        public string EQPID
        {
            get
            {
                return this.Request.QueryString["EQPID"];
            }
        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.txtEQPIDQuery.Text = this.EQPID;

                drpEQPTSTypeEdit_Load();

                this.txtEqpMemoEdit.Enabled = false;
                this.txtEqpReasonEdit.Enabled = false;
                this.txtEqpResultEdit.Enabled = false;
                this.txtEqpSolutionEdit.Enabled = false;
                this.drpEqpTSTypeEdit.Enabled = false;
                
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
            this.gridHelper.AddColumn("Serial", "序号", null);
            this.gridHelper.AddColumn("EQPID", "设备ID", null);
            this.gridHelper.AddColumn("FINDUSER", "报修人", null);
            this.gridHelper.AddColumn("FINDMDATE", "报修日期", null);
            this.gridHelper.AddColumn("FINDMTIME", "报修时间", null);
            this.gridHelper.AddColumn("TSINFO", "故障现象", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("EQPReason", "故障原因", null);
            this.gridHelper.AddColumn("EQPSolution", "维修措施", null);
            this.gridHelper.AddColumn("EQPResult", "故障结果", null);
            this.gridHelper.AddColumn("EQPTSType", "维修类型", null);
            this.gridHelper.AddColumn("STATUS", "状态", null);
            this.gridHelper.AddColumn("EQPDuration", "维修时长", null);
            this.gridHelper.AddColumn("EQPMEMO", "备注", null);

            this.gridHelper.AddDefaultColumn(false, true);

            this.gridWebGrid.Columns.FromKey("EQPID").Hidden = true;
            this.gridWebGrid.Columns.FromKey("Serial").Hidden = true;

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            this.gridHelper.RequestData();
        }

        protected override DataRow GetGridRow(object obj)
        {
            
            DataRow row = this.DtSource.NewRow();
            row["Serial"] = ((Domain.Equipment.EQPTSLog)obj).Serial;
            row["EQPID"] = ((Domain.Equipment.EQPTSLog)obj).Eqpid;
            row["FINDUSER"] = ((Domain.Equipment.EQPTSLog)obj).GetDisplayText("FindUser");
            row["FINDMDATE"] = FormatHelper.ToDateString(((Domain.Equipment.EQPTSLog)obj).FindMdate);
            row["FINDMTIME"] = FormatHelper.ToTimeString(((Domain.Equipment.EQPTSLog)obj).FindMtime);
            row["TSINFO"] = ((Domain.Equipment.EQPTSLog)obj).TsInfo;
            row["MaintainDate"] = FormatHelper.ToDateString(((Domain.Equipment.EQPTSLog)obj).Mdate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((Domain.Equipment.EQPTSLog)obj).Mtime);
            row["MaintainUser"] = ((Domain.Equipment.EQPTSLog)obj).GetDisplayText("MaintainUser");
            row["EQPReason"] = ((Domain.Equipment.EQPTSLog)obj).Reason;
            row["EQPSolution"] = ((Domain.Equipment.EQPTSLog)obj).Solution;
            row["EQPResult"] = ((Domain.Equipment.EQPTSLog)obj).Result;
            row["EQPTSType"] = ((Domain.Equipment.EQPTSLog)obj).Tstype;
            row["STATUS"] = languageComponent1.GetString(((Domain.Equipment.EQPTSLog)obj).Status);
            row["EQPDuration"] = ((Domain.Equipment.EQPTSLog)obj).Duration;
            row["EQPMEMO"] = ((Domain.Equipment.EQPTSLog)obj).Memo;
            return row;
        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            //if (this.gridHelper.IsClickEditColumn(cell))
            if (commandName!=null)
            {
                object obj = this.GetEditObject(row);
                if (((Domain.Equipment.EQPTSLog)obj).Status.ToUpper() == EquipmentTSLogStatus.EquipmentTSLogStatus_Closed)
                {
                    WebInfoPublish.Publish(this, "$EQPTSLOG_STATUS_CLOSED", this.languageComponent1);
                    return;
                }
                if (obj != null)
                {
                    this.SetEditObject(obj);

                    this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
                }
            }
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            return this._facade.QueryEQPTSLog(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtEQPIDQuery.Text)),
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            return this._facade.QueryEQPTSLogCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtEQPIDQuery.Text)));
        }


        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            //if (_facade == null)
            //{
            //    _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            //}

            //this.DataProvider.BeginTransaction();

            //if (_facade.CheckEQPTSLogExists(this.txtEQPIDQuery.Text, EquipmentTSLogStatus.EquipmentTSLogStatus_New) > 0)
            //{
            //    WebInfoPublish.Publish(this, "$EQPTSLog_Exists", this.languageComponent1);
            //}
            //else
            //{
            //    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            //    Domain.Equipment.EQPTSLog eqptsLog = new Domain.Equipment.EQPTSLog();
            //    eqptsLog.Eqpid = this.txtEQPIDQuery.Text;
            //    eqptsLog.FindUser = this.GetUserCode();
            //    eqptsLog.TsInfo = FormatHelper.CleanString(this.txtEqpTsInfoEdit.Text, 400);
            //    eqptsLog.FindMdate = dbDateTime.DBDate;
            //    eqptsLog.FindMtime = dbDateTime.DBTime;
            //    eqptsLog.Status = EquipmentTSLogStatus.EquipmentTSLogStatus_New;
            //    eqptsLog.Duration = 0;
            //    eqptsLog.MaintainUser = this.GetUserCode();
            //    eqptsLog.Mdate = dbDateTime.DBDate;
            //    eqptsLog.Mtime = dbDateTime.DBTime;
            //    _facade.AddEQPTSLog(eqptsLog);
            //    //WebInfoPublish.Publish(this, "$AddEQPTSLog_Success", this.languageComponent1);
            //}

            //this.DataProvider.CommitTransaction();


        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }

            this.DataProvider.BeginTransaction();
            try
            {


                this._facade.DeleteEQPTSLog((Domain.Equipment.EQPTSLog[])domainObjects.ToArray(typeof(Domain.Equipment.EQPTSLog)));
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
            }
            this.DataProvider.CommitTransaction();


        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            this._facade.UpdateEQPTSLog((Domain.Equipment.EQPTSLog)domainObject);
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl("FEquimentMP.aspx"));
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                //this.txtEQPIDEdit.ReadOnly = false;
                this.txtEqpMemoEdit.Enabled = false;
                this.txtEqpReasonEdit.Enabled = false;
                this.txtEqpResultEdit.Enabled = false;
                this.txtEqpSolutionEdit.Enabled = false;
                this.drpEqpTSTypeEdit.Enabled = false;

                this.txtEqpTsInfoEdit.Text = "";
            }

            if (pageAction == PageActionType.Update)
            {
                //this.txtEQPIDEdit.ReadOnly = true;
                this.txtEqpTsInfoEdit.ReadOnly = true;

                this.txtEqpMemoEdit.Enabled = true;
                this.txtEqpReasonEdit.Enabled = true;
                this.txtEqpResultEdit.Enabled = true;
                this.txtEqpSolutionEdit.Enabled = true;
                this.drpEqpTSTypeEdit.Enabled = true;

            }
            if (pageAction == PageActionType.Save)
            {
                this.txtEqpTsInfoEdit.ReadOnly = false;

                this.txtEqpMemoEdit.Enabled = false;
                this.txtEqpReasonEdit.Enabled = false;
                this.txtEqpResultEdit.Enabled = false;
                this.txtEqpSolutionEdit.Enabled = false;
                this.drpEqpTSTypeEdit.Enabled = false;
            }
            if (pageAction == PageActionType.Cancel)
            {
                this.txtEqpTsInfoEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Delete)
            {
                this.gridHelper.RequestData();
            }
        }
        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new Material.EquipmentFacade(base.DataProvider);
            }

            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            Domain.Equipment.EQPTSLog route = this._facade.CreateNewEQPTSLOG();

            route.Eqpid = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtEQPIDQuery.Text, 40));

            if (this.txtSerialEdit.Text.Trim() == string.Empty)
            {
                route.Serial = 0;
            }
            else
            {
                route.Serial = int.Parse(this.txtSerialEdit.Text);
            }
            route = (Domain.Equipment.EQPTSLog)this._facade.GetEQPTSLog(route.Serial);
            route.TsInfo = FormatHelper.CleanString(this.txtEqpTsInfoEdit.Text, 400);
            route.Memo = FormatHelper.CleanString(this.txtEqpMemoEdit.Text, 400);
            route.Reason = FormatHelper.CleanString(this.txtEqpReasonEdit.Text, 400);
            route.Result = FormatHelper.CleanString(this.txtEqpResultEdit.Text, 400);
            route.Solution = FormatHelper.CleanString(this.txtEqpSolutionEdit.Text, 400);
            route.Tstype = FormatHelper.CleanString(this.drpEqpTSTypeEdit.SelectedValue, 40);
            route.MaintainUser = this.GetUserCode();
            route.Status = EquipmentTSLogStatus.EquipmentTSLogStatus_Closed;
            route.Mdate = dbDateTime.DBDate;
            route.Mtime = dbDateTime.DBTime;
            DateTime findTime = new DateTime(route.FindMdate / 10000, (route.FindMdate / 100) % 100, route.FindMdate % 100,
                route.FindMtime / 10000, (route.FindMtime / 100) % 100, route.FindMtime % 100);
            route.Duration = (int)(dbDateTime.DateTime - findTime).TotalMinutes;
            return route;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            object obj = _facade.GetEQPTSLog(int.Parse(row.Items.FindItemByKey("Serial").Text.ToString()));

            if (obj != null)
            {
                return (Domain.Equipment.EQPTSLog)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtEqpTsInfoEdit.Text = "";
                this.txtEqpMemoEdit.Text = "";
                this.txtEqpSolutionEdit.Text = "";
                this.txtEqpResultEdit.Text = "";
                this.txtEqpReasonEdit.Text = "";
                this.txtSerialEdit.Text = "";
                this.txtEqpTsDurationEdit.Text = "";
                this.drpEqpTSTypeEdit.SelectedIndex = -1;
                return;
            }
            this.txtEqpTsInfoEdit.Text = ((Domain.Equipment.EQPTSLog)obj).TsInfo.ToString();
            this.txtEqpMemoEdit.Text = ((Domain.Equipment.EQPTSLog)obj).Memo.ToString();
            this.txtEqpSolutionEdit.Text = ((Domain.Equipment.EQPTSLog)obj).Solution.ToString();
            this.txtEqpResultEdit.Text = ((Domain.Equipment.EQPTSLog)obj).Result.ToString();
            this.txtEqpReasonEdit.Text = ((Domain.Equipment.EQPTSLog)obj).Reason.ToString();
            //this.txtEqpTsDurationEdit.Text = ((Domain.Equipment.EQPTSLog)obj).Duration.ToString();
            this.txtSerialEdit.Text = ((Domain.Equipment.EQPTSLog)obj).Serial.ToString();
            try
            {
                this.drpEqpTSTypeEdit.SelectedValue = ((Domain.Equipment.EQPTSLog)obj).Tstype.ToString();
            }
            catch
            {
                this.drpEqpTSTypeEdit.SelectedIndex = -1;
            }

        }

        protected override void cmdAdd_Click(object sender, System.EventArgs e)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }


            this.DataProvider.BeginTransaction();

            if (_facade.CheckEQPTSLogExists(this.txtEQPIDQuery.Text, EquipmentTSLogStatus.EquipmentTSLogStatus_New) > 0)
            {
                WebInfoPublish.Publish(this, "$EQPTSLog_Exists", this.languageComponent1);
            }
            else
            {

                PageCheckManager manager = new PageCheckManager();
                manager.Add(new LengthCheck(this.lblEqpTsInfoEdit, this.txtEqpTsInfoEdit, 400, true));
                if (!manager.Check())
                {
                    WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                    return;
                }


                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
                Domain.Equipment.EQPTSLog eqptsLog = new Domain.Equipment.EQPTSLog();
                eqptsLog.Eqpid = this.txtEQPIDQuery.Text;
                eqptsLog.FindUser = this.GetUserCode();
                eqptsLog.TsInfo = FormatHelper.CleanString(this.txtEqpTsInfoEdit.Text, 400);
                eqptsLog.FindMdate = dbDateTime.DBDate;
                eqptsLog.FindMtime = dbDateTime.DBTime;
                eqptsLog.Status = EquipmentTSLogStatus.EquipmentTSLogStatus_New;
                eqptsLog.Duration = 0;
                eqptsLog.MaintainUser = this.GetUserCode();
                eqptsLog.Mdate = dbDateTime.DBDate;
                eqptsLog.Mtime = dbDateTime.DBTime;
                _facade.AddEQPTSLog(eqptsLog);
                //WebInfoPublish.Publish(this, "$AddEQPTSLog_Success", this.languageComponent1);
            }

            this.DataProvider.CommitTransaction();

            this.gridHelper.RefreshData();
        }
        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblEqpTSTypeEdit, this.drpEqpTSTypeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblEqpReasonEdit, this.txtEqpReasonEdit, 400, true));
            manager.Add(new LengthCheck(this.lblEqpResultEdit, this.txtEqpResultEdit, 400, true));
            manager.Add(new LengthCheck(this.lblEqpSolutionEdit, this.txtEqpSolutionEdit, 400, true));
            //manager.Add(new NumberCheck(this.lblEqpTsDurationEdit, this.txtEqpTsDurationEdit, 0, 99999999, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;
        }

        #endregion

        #region 数据初始化

        private void drpEQPTSTypeEdit_Load()
        {
            if (!this.IsPostBack)
            {
                if (_facade == null)
                {
                    _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
                }

                object[] objs = _facade.GetAllEquipmentTsType();

                foreach (Domain.Equipment.EquipmentTsType type in objs)
                {
                    this.drpEqpTSTypeEdit.Items.Add(new ListItem(type.Eqptstype, type.Eqptstype));
                }
                this.drpEqpTSTypeEdit.Items.Insert(0,new ListItem("",""));
            }
        }
        #endregion

        protected string GetEqpTSTypeDesc(string tsType)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }

            object obj = _facade.GetEquipmentTsType(tsType);

            if (obj != null)
            {
                return (obj as Domain.Equipment.EquipmentTsType).Eqptstypedesc;
            }

            return " ";
        }

        #region Export
        // 2005-04-06
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{
                                ((Domain.Equipment.EQPTSLog)obj).Eqpid,
                                ((Domain.Equipment.EQPTSLog)obj).GetDisplayText("FindUser"),	
                                FormatHelper.ToDateString(((Domain.Equipment.EQPTSLog)obj).FindMdate),
								FormatHelper.ToTimeString(((Domain.Equipment.EQPTSLog)obj).FindMtime),
                                ((Domain.Equipment.EQPTSLog)obj).TsInfo,	
                                FormatHelper.ToDateString(((Domain.Equipment.EQPTSLog)obj).Mdate),
								FormatHelper.ToTimeString(((Domain.Equipment.EQPTSLog)obj).Mtime),
                                ((Domain.Equipment.EQPTSLog)obj).GetDisplayText("MaintainUser"),
                                ((Domain.Equipment.EQPTSLog)obj).Reason,
                                ((Domain.Equipment.EQPTSLog)obj).Solution,
                                ((Domain.Equipment.EQPTSLog)obj).Result,
                                GetEqpTSTypeDesc(((Domain.Equipment.EQPTSLog)obj).Tstype),
                                languageComponent1.GetString(((Domain.Equipment.EQPTSLog)obj).Status),
                                ((Domain.Equipment.EQPTSLog)obj).Duration.ToString(),
                                ((Domain.Equipment.EQPTSLog)obj).Memo,
                                };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {   "EQPID", 
                                    "FINDUSER",
                                    "FINDMDATE",
                                    "FINDMTIME",
                                    "TSINFO",
                                    "MaintainDate",
                                    "MaintainTime",
                                    "MaintainUser", 
                                    "EQPReason",
                                    "EQPSolution",
                                    "EQPResult",
                                    "EQPTstype",
                                    "STATUS",
                                    "EQPDuration",
                                    "EQPMemo" };
        }


        #endregion
    }
}


