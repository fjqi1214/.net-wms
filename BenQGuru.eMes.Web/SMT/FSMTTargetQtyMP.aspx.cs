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
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Domain.SMT;
using BenQGuru.eMES.Domain.BaseSetting;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.SMT
{
    /// <summary>
    /// FReelMP 的摘要说明。
    /// </summary>
    public partial class FSMTTargetQtyMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private BenQGuru.eMES.SMT.SMTFacade _facade;//= new SMTFacadeFactory(base.DataProvider).Create();

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
            this.PreRender += new System.EventHandler(this.FSMTTargetQtyMP_PreRender);

        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                BenQGuru.eMES.BaseSetting.BaseModelFacade modelFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
                object[] objs = modelFacade.GetAllSegment();
                this.drpSegmentCode.Items.Clear();
                for (int i = 0; i < objs.Length; i++)
                {
                    this.drpSegmentCode.Items.Add(new ListItem(((Segment)objs[i]).SegmentCode, ((Segment)objs[i]).SegmentCode));
                }
                if (this.drpSegmentCode.Items.Count > 0)
                {
                    this.drpSegmentCode.SelectedIndex = 0;
                    drpSegmentCode_SelectedIndexChanged(null, null);
                }
            }
            this.cmdSave.Disabled = false;
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
            this.gridHelper.AddColumn("MOCode", "工单代码", null);
            this.gridHelper.AddColumn("sscode", "产线代码", null);
            this.gridHelper.AddColumn("SegmentCode", "工段代码", null);
            this.gridHelper.AddColumn("TimePeriodCode", "时段代码", null);
            this.gridHelper.AddColumn("TimePeriodDescription", "时段描述", null);
            this.gridHelper.AddColumn("TargetQty", "目标产量", HorizontalAlign.Right);
            this.gridHelper.AddColumn("MaintainUser", "维护人员", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);
            //this.gridWebGrid.Columns.FromKey("TargetQty").Format = "#,#";
            //this.gridWebGrid.Columns.FromKey("TargetQty").CellStyle.HorizontalAlign = HorizontalAlign.Right;

            this.gridHelper.AddDefaultColumn(false, false);
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            SMTTargetQty targetQty = (SMTTargetQty)obj;
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{
            //                    targetQty.MOCode,
            //                    targetQty.SSCode,
            //                    targetQty.SegmentCode,
            //                    targetQty.TPCode,
            //                    targetQty.TPDescription,
            //                    targetQty.TPQty,
            //                    targetQty.MaintainUser,
            //                    FormatHelper.ToDateString(targetQty.MaintainDate),
            //                    FormatHelper.ToTimeString(targetQty.MaintainTime)
            //                });
            DataRow row = this.DtSource.NewRow();
            row["MOCode"] = targetQty.MOCode;
            row["sscode"] = targetQty.SSCode;
            row["SegmentCode"] = targetQty.SegmentCode;
            row["TimePeriodCode"] = targetQty.TPCode;
            row["TimePeriodDescription"] = targetQty.TPDescription;
            row["TargetQty"] = targetQty.TPQty;
            row["MaintainUser"] = targetQty.MaintainUser;
            row["MaintainDate"] = FormatHelper.ToDateString(targetQty.MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(targetQty.MaintainTime);
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null) { _facade = new SMTFacadeFactory(base.DataProvider).Create(); }
            return this._facade.QuerySMTTargetQty(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSSCodeQuery.Text)),
                inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (_facade == null) { _facade = new SMTFacadeFactory(base.DataProvider).Create(); }
            return this._facade.QuerySMTTargetQtyCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSSCodeQuery.Text)));
        }

        #endregion

        #region Button
        protected override void AddDomainObject(object domainObject)
        {
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
        }

        protected override void UpdateDomainObject(object domainObject)
        {
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {

        }
        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            return null;
        }


        protected override object GetEditObject(GridRecord row)
        {
            return null;
        }

        protected override void SetEditObject(object obj)
        {

        }


        protected override bool ValidateInput()
        {

            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(lblMOIDEdit, txtMOCode, 40, true));
            manager.Add(new DecimalCheck(lblQtyPerHour, txtQtyPerHour, 0, decimal.MaxValue, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;

        }

        protected void drpSegmentCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            BenQGuru.eMES.BaseSetting.BaseModelFacade modelFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
            object[] objs = modelFacade.GetStepSequenceBySegmentCode(this.drpSegmentCode.SelectedValue);
            this.drpSSCode.Items.Clear();
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    this.drpSSCode.Items.Add(new ListItem(((StepSequence)objs[i]).StepSequenceCode, ((StepSequence)objs[i]).StepSequenceCode));
                }
            }

            if (this.drpSSCode.Items.Count > 0)
                this.drpSSCode.SelectedIndex = 0;
        }

        protected void FSMTTargetQtyMP_PreRender(object sender, EventArgs e)
        {
            this.cmdSave.Disabled = false;
        }

        protected void cmdSave_ServerClick(object sender, EventArgs e)
        {
            if (_facade == null) { _facade = new SMTFacadeFactory(base.DataProvider).Create(); }
            this._facade.UpdateSMTTargetQty(this.drpSegmentCode.SelectedValue, this.drpSSCode.SelectedValue, this.txtMOCode.Text.Trim().ToUpper(), decimal.Parse(this.txtQtyPerHour.Text), this.GetUserCode());
            this.cmdQuery_Click(null, null);
        }

        protected void cmdCancel_ServerClick(object sender, EventArgs e)
        {
            if (this.drpSegmentCode.Items.Count > 0)
            {
                this.drpSegmentCode.SelectedIndex = 0;
                drpSegmentCode_SelectedIndexChanged(null, null);
            }
            else
            {
                this.drpSegmentCode.SelectedIndex = -1;
                this.drpSSCode.SelectedIndex = -1;
            }
            this.txtMOCode.Text = "";
            this.txtQtyPerHour.Text = "";
        }
        #endregion

        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            SMTTargetQty targetQty = (SMTTargetQty)obj;
            return new string[]{ targetQty.MOCode,
								   targetQty.SSCode,
								   targetQty.SegmentCode,
								   targetQty.TPCode,
								   targetQty.TPDescription,
								   targetQty.TPQty.ToString(),
								   targetQty.MaintainUser,
								   FormatHelper.ToDateString(targetQty.MaintainDate),
								   FormatHelper.ToTimeString(targetQty.MaintainTime)
							   };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"MOCode",
									"sscode",
									"SegmentCode",
									"TimePeriodCode",	
									"TimePeriodDescription",
									"TargetQty",
									"MaintainUser",
									"MaintainDate",
									"MaintainTime"
								};
        }
        #endregion
    }
}
