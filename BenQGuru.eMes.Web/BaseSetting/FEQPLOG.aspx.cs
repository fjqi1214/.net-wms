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
    public partial class FEQPLOG : BaseMPageNew
    {
        protected System.Web.UI.WebControls.Label lblRouteTitle;


        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        BenQGuru.eMES.BaseSetting.SystemSettingFacade sysFacade = null;
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

                drpEQPStatusEdit_Load();


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
            this.gridHelper.AddColumn("EQPID", "设备ID", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("EQPStatus", "设备状态", null);
            this.gridHelper.AddColumn("EQPMEMO", "备注", null);

            this.gridHelper.AddDefaultColumn(false, false);

            this.gridWebGrid.Columns.FromKey("EQPID").Hidden = true;

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            this.gridHelper.RequestData();
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{		
            //                    ((Domain.Equipment.EQPLog)obj).Eqpid,	
            //                    FormatHelper.ToDateString(((Domain.Equipment.EQPLog)obj).Mdate),
            //                    FormatHelper.ToTimeString(((Domain.Equipment.EQPLog)obj).Mtime),
            //                    ((Domain.Equipment.EQPLog)obj).GetDisplayText("MaintainUser"),
            //                    GetStatusDesc(((Domain.Equipment.EQPLog)obj).Eqpstatus),
            //                    ((Domain.Equipment.EQPLog)obj).Memo,
            //                    });
            DataRow row = this.DtSource.NewRow();
            row["EQPID"] = ((Domain.Equipment.EQPLog)obj).Eqpid;
            row["MaintainDate"] = FormatHelper.ToDateString(((Domain.Equipment.EQPLog)obj).Mdate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((Domain.Equipment.EQPLog)obj).Mtime);
            row["MaintainUser"] = ((Domain.Equipment.EQPLog)obj).GetDisplayText("MaintainUser");
            row["EQPStatus"] = GetStatusDesc(((Domain.Equipment.EQPLog)obj).Eqpstatus);
            row["EQPMEMO"] = ((Domain.Equipment.EQPLog)obj).Memo;
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            return this._facade.QueryEQPLog(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtEQPIDQuery.Text)),
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            return this._facade.QueryEQPLogCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtEQPIDQuery.Text)));
        }


        protected string GetStatusDesc(string status)
        {
            if (sysFacade == null)
            {
                sysFacade = new SystemSettingFacade(this.DataProvider);
            }

            object obj = sysFacade.GetParameter(status, "EQPSTATUS");

            if (obj != null)
            {
                return (obj as Domain.BaseSetting.Parameter).ParameterDescription;
            }

            return " ";
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }

            this.DataProvider.BeginTransaction();
            try
            {

                this._facade.AddEQPLOG((Domain.Equipment.EQPLog)domainObject);

                object objEqp = _facade.GetEquipment(this.EQPID);
                if (objEqp != null)
                {
                    (objEqp as Domain.Equipment.Equipment).Eqpstatus = this.drpEQPStatusEdit.SelectedValue.Trim();
                    _facade.UpdateEquipment((objEqp as Domain.Equipment.Equipment));
                }
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
            }

            this.DataProvider.CommitTransaction();


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


                this._facade.DeleteEQPLog((Domain.Equipment.EQPLog[])domainObjects.ToArray(typeof(Domain.Equipment.EQPLog)));
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
            }
            this.DataProvider.CommitTransaction();


        }

 

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                //this.txtEQPIDEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                //this.txtEQPIDEdit.ReadOnly = true;
            }
            if (pageAction == PageActionType.Delete)
            {
                this.gridHelper.RequestData();
            }
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl("FEquimentMP.aspx"));
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
            Domain.Equipment.EQPLog route = this._facade.CreateNewEQPLOG();
            //route.Serial = int.Parse();
            route.Eqpid = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtEQPIDQuery.Text, 40));
            route.Memo = FormatHelper.CleanString(this.txtEqpMemoEdit.Text, 400);
            route.MaintainUser = this.GetUserCode();
            route.Eqpstatus = FormatHelper.CleanString(this.drpEQPStatusEdit.SelectedValue, 40);
            route.Mdate = dbDateTime.DBDate;
            route.Mtime = dbDateTime.DBTime;

            return route;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            object obj = _facade.GetEquipment(row.Items.FindItemByKey("EQPID").Text.ToString());

            if (obj != null)
            {
                return (Domain.Equipment.EQPLog)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtEqpMemoEdit.Text = "";
                return;
            }

           try
            {
                this.drpEQPStatusEdit.SelectedValue = ((Domain.Equipment.EQPLog)obj).Eqpstatus.ToString();
            }
            catch
            {
                this.drpEQPStatusEdit.SelectedIndex = 0;
            }

           this.txtEqpMemoEdit.Text = ((Domain.Equipment.EQPLog)obj).Memo.ToString();

        }


        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblEQPStatusEdit, this.drpEQPStatusEdit, 40, true));
            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;
        }

        #endregion

        #region 数据初始化

        private void drpEQPStatusEdit_Load()
        {
            if (!this.IsPostBack)
            {
                SystemParameterListBuilder _builder = new SystemParameterListBuilder("EQPSTATUS", base.DataProvider);
                _builder.BuildShowDescription(this.drpEQPStatusEdit);
            }
        }
        #endregion

        #region Export
        // 2005-04-06
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{
                                ((Domain.Equipment.EQPLog)obj).Eqpid,
                                FormatHelper.ToDateString(((Domain.Equipment.EQPLog)obj).Mdate),
								FormatHelper.ToTimeString(((Domain.Equipment.EQPLog)obj).Mtime),
                                ((Domain.Equipment.EQPLog)obj).GetDisplayText("MaintainUser"),
                                GetStatusDesc(((Domain.Equipment.EQPLog)obj).Eqpstatus),
                                ((Domain.Equipment.EQPLog)obj).Memo,
                                };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {"EQPID","MaintainDate","MaintainTime","MaintainUser","EQPStatus","Memo"};
        }

        #endregion
    }
}

