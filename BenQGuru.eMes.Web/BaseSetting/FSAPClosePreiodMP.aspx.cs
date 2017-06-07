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
    public partial class FSAPClosePreiodMP : BaseMPageNew
    {
        protected System.Web.UI.WebControls.Label lblRouteTitle;


        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        BenQGuru.eMES.BaseSetting.SystemSettingFacade sysFacade = null;
        private BenQGuru.eMES.Material.WarehouseFacade _facade = null;//	new BaseModelFacadeFactory().Create();
        private BenQGuru.eMES.BaseSetting.BaseModelFacade _BaseModelFacadeFactory = null;


        public TextBox txtDateUseBeginQuery;
        public TextBox txtDateUseEndQuery;
        public TextBox txtDateUseEdit;
        public TextBox txtToDateEdit;

        public BenQGuru.eMES.Web.UserControl.eMESTime txtOnTimeEdit;
        public BenQGuru.eMES.Web.UserControl.eMESTime txtOffTimeEdit;

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
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.Form.Focus();

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
            this.gridHelper.AddColumn("serial", "序号", null);
            this.gridHelper.AddColumn("sapStartTime", "开始时间", null);
            this.gridHelper.AddColumn("sapEndTime", "结束时间", null);

            this.gridHelper.AddColumn("MaintainUser", "维护人员", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);
            this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
            this.gridWebGrid.Columns.FromKey("serial").Hidden = true;
            this.gridHelper.AddDefaultColumn(true, true);
           
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            
            DataRow row = this.DtSource.NewRow();
            row["serial"] = ((Domain.Warehouse.Sapcloseperiod)obj).Serial.ToString();
            row["sapStartTime"] = FormatHelper.ToDateString(((Domain.Warehouse.Sapcloseperiod)obj).StartDate) + " " + FormatHelper.ToTimeString(((Domain.Warehouse.Sapcloseperiod)obj).StartTime);
            row["sapEndTime"] = FormatHelper.ToDateString(((Domain.Warehouse.Sapcloseperiod)obj).EndDate) + " " + FormatHelper.ToTimeString(((Domain.Warehouse.Sapcloseperiod)obj).EndTime);
            row["MaintainUser"] = ((Domain.Warehouse.Sapcloseperiod)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((Domain.Warehouse.Sapcloseperiod)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((Domain.Warehouse.Sapcloseperiod)obj).MaintainTime);
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            int beginDate = FormatHelper.TODateInt(this.txtDateUseBeginQuery.Text.Trim());
            int endDate = FormatHelper.TODateInt(this.txtDateUseEndQuery.Text.Trim());

            return this._facade.QueryRecordDetail(beginDate,endDate,inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }

            int beginDate = FormatHelper.TODateInt(this.txtDateUseBeginQuery.Text.Trim());
            int endDate = FormatHelper.TODateInt(this.txtDateUseEndQuery.Text.Trim());

            return this._facade.QueryRecordDetailCOUNT( beginDate,endDate);

        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }

            int num = this._facade.GetRecordCount(((Domain.Warehouse.Sapcloseperiod)domainObject).StartDate, ((Domain.Warehouse.Sapcloseperiod)domainObject).StartTime, ((Domain.Warehouse.Sapcloseperiod)domainObject).EndDate, ((Domain.Warehouse.Sapcloseperiod)domainObject).EndTime);
            if (num == 0)
            {
                this._facade.AddSapcloseperiod((Domain.Warehouse.Sapcloseperiod)domainObject);
            }
            else
            {
                WebInfoPublish.Publish(this, "$Error_SAPCloseTime_Exist  ", languageComponent1);
            }
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }

            this._facade.DeleteSapcloseperiod((Domain.Warehouse.Sapcloseperiod[])domainObjects.ToArray(typeof(Domain.Warehouse.Sapcloseperiod)));

        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
     
            object obj = _facade.GetSapcloseperiod(Int32.Parse(this.txtserial.Text.Trim()));
            ((Domain.Warehouse.Sapcloseperiod)domainObject).CUser = ((Domain.Warehouse.Sapcloseperiod)obj).CUser;
            ((Domain.Warehouse.Sapcloseperiod)domainObject).CDate = ((Domain.Warehouse.Sapcloseperiod)obj).CDate;
            ((Domain.Warehouse.Sapcloseperiod)domainObject).CTime = ((Domain.Warehouse.Sapcloseperiod)obj).CTime;

            this._facade.UpdateSapcloseperiod((Domain.Warehouse.Sapcloseperiod)domainObject);
        }

        //protected override void Grid_ClickCell(UltraGridCell cell)
        //{
        //    base.Grid_ClickCell(cell);
        //}

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            //if (pageAction == PageActionType.Add)
            //{
            //    this.txtEQPIDEdit.Readonly = false;
            //    this.txtDateUseEdit.Enabled = true;
            //    this.txtDateUseEdit.ReadOnly = false;
            //}

            //if (pageAction == PageActionType.Update)
            //{
            //    this.txtEQPIDEdit.Readonly = true;
            //    this.txtDateUseEdit.Enabled = false;
            //    this.txtDateUseEdit.ReadOnly = true;
            //}
            //if (pageAction == PageActionType.Delete)
            //{
            //    this.gridHelper.RequestData();
            //}
        }
        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }

            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            Domain.Warehouse.Sapcloseperiod route = this._facade.CreateNewSapcloseperiod();

            route.Serial =string.IsNullOrEmpty(this.txtserial.Text.Trim()) ? 0: Int32.Parse(this.txtserial.Text.Trim());
            route.StartDate = FormatHelper.TODateInt(this.txtDateUseEdit.Text.Trim());
            route.StartTime = FormatHelper.TOTimeInt(this.txtOnTimeEdit.Text.Trim());
            route.EndDate = FormatHelper.TODateInt(this.txtToDateEdit.Text.Trim());
            route.EndTime = FormatHelper.TOTimeInt(this.txtOffTimeEdit.Text.Trim());
            route.CDate = dbDateTime.DBDate;
            route.CTime = dbDateTime.DBTime;
            route.CUser = this.GetUserCode();
            route.MaintainUser = this.GetUserCode();
            route.MaintainDate = dbDateTime.DBDate;
            route.MaintainTime = dbDateTime.DBTime;

            return route;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            object obj = _facade.GetSapcloseperiod(Int32.Parse(row.Items.FindItemByKey("serial").Text));

            if (obj != null)
            {
                return (Domain.Warehouse.Sapcloseperiod)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtserial.Text = "";
                this.txtDateUseEdit.Text = "";
                this.txtToDateEdit.Text = "";
                this.txtOffTimeEdit.Text = "";
                this.txtOnTimeEdit.Text = "";
               
                return;
            }

             this.txtserial.Text = ((Domain.Warehouse.Sapcloseperiod)obj).Serial.ToString();
             this.txtDateUseEdit.Text = FormatHelper.ToDateString(((Domain.Warehouse.Sapcloseperiod)obj).StartDate);
             this.txtToDateEdit.Text = FormatHelper.ToDateString(((Domain.Warehouse.Sapcloseperiod)obj).EndDate);

             this.txtOffTimeEdit.Text = FormatHelper.ToTimeString(((Domain.Warehouse.Sapcloseperiod)obj).EndTime);
             this.txtOnTimeEdit.Text = FormatHelper.ToTimeString(((Domain.Warehouse.Sapcloseperiod)obj).StartTime);
             

        }


        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new DateCheck(lblFromDateTimeEdit, txtDateUseEdit.Text, false));
            manager.Add(new DateCheck(lblEndDateTimeEdit, txtToDateEdit.Text, false));

            string datetimeFrom = Convert.ToString(FormatHelper.TODateInt(this.txtDateUseEdit.Text).ToString() + FormatHelper.TOTimeInt(this.txtOnTimeEdit.Text).ToString().PadLeft(6,'0'));
            string datetimeEnd = Convert.ToString(FormatHelper.TODateInt(this.txtToDateEdit.Text).ToString()  + FormatHelper.TOTimeInt(this.txtOffTimeEdit.Text).ToString().PadLeft(6,'0'));
           // manager.Add(new DateRangeCheck(this.lblFromDateTimeEdit, datetimeFrom, this.lblEndDateTimeEdit, datetimeEnd, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }



            if (this.txtDateUseEdit.Text.Trim().Length <= 0)
            {
                WebInfoPublish.Publish(this, "$Error_StartTime_Must", this.languageComponent1);
                return false;
            }
            if (this.txtToDateEdit.Text.Trim().Length <= 0)
            {
                WebInfoPublish.Publish(this, "$Error_EndTime_Must", this.languageComponent1);
                return false;
            }
            if (this.txtOnTimeEdit.Text.Trim().Length <= 0)
            {
                WebInfoPublish.Publish(this, "$Error_OnTime_Null", this.languageComponent1);
                return false;
            }
            if (this.txtOffTimeEdit.Text.Trim().Length <= 0)
            {
                WebInfoPublish.Publish(this, "$Error_OffTime_Null", this.languageComponent1);
                return false;
            }
            if (Int64.Parse(datetimeFrom) > Int64.Parse(datetimeEnd))
            {                
                WebInfoPublish.Publish(this, "$Message_OffTime_Must_Bigger_Than_OnTime", this.languageComponent1);
                return false;
            }
            
            return true;
        }

        //实际运行时间=关机时间-开机时间
        //public int GetRunTime(int today,int offtime,int ontime)
        //{
        //    DateTime dtOfftime =  FormatHelper.ToDateTime(today,offtime);
        //    DateTime dtOntime = FormatHelper.ToDateTime(today, ontime);
        //    int runtime = (int)((dtOfftime.Ticks - dtOntime.Ticks) / (10000000 * 60));                                
        //    return runtime;
        //}

        #endregion

        #region Export
        // 2005-04-06
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{
                             
                                FormatHelper.ToDateString(((Domain.Warehouse.Sapcloseperiod)obj).StartDate) + " " + FormatHelper.ToTimeString(((Domain.Warehouse.Sapcloseperiod)obj).StartTime),
                                FormatHelper.ToDateString(((Domain.Warehouse.Sapcloseperiod)obj).EndDate) + " " + FormatHelper.ToTimeString(((Domain.Warehouse.Sapcloseperiod)obj).EndTime),
                                ((Domain.Warehouse.Sapcloseperiod)obj).GetDisplayText("MaintainUser"),
                                FormatHelper.ToDateString(((Domain.Warehouse.Sapcloseperiod)obj).MaintainDate)
                             //    FormatHelper.ToTimeString(((Domain.Warehouse.Sapcloseperiod)obj).MaintainTime)
                                };

        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"sapStartTime",
									"sapEndTime",
                                    "MaintainUser",
                                    "MaintainDate"
                                   
                                   };
        }
        
        #endregion
    }
}

