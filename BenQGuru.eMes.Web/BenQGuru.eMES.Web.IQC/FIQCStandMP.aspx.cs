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
using BenQGuru.eMES.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.OQC;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.IQC
{
    public partial class FIQCStandMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        protected System.Web.UI.WebControls.Label lblResourceTitle;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
        protected System.Web.UI.WebControls.TextBox txtShiftTypeEdit;
        private BenQGuru.eMES.OQC.OQCFacade _oqcfade = null;

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
            this.gridHelper.AddColumn("AqlLevel", "AQL标准", null);
            this.gridHelper.AddColumn("AqlSql", "序号", null);
            this.gridHelper.AddColumn("LotSizeMin", "批量起始数量", null);
            this.gridHelper.AddColumn("LotSizeMax", "批量截止数量", null);
            this.gridHelper.AddColumn("SampleSize", "样本数量", null);
            this.gridHelper.AddColumn("RejectSieze", "判退数量", null);
            this.gridHelper.AddColumn("MaintainUser", "维护人员", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);

            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["AqlLevel"] = ((Domain.OQC.AQL)obj).AqlLevel;
            row["AqlSql"] = ((Domain.OQC.AQL)obj).AQLSeq;
            row["LotSizeMin"] = ((Domain.OQC.AQL)obj).LotSizeMin;
            row["LotSizeMax"] = ((Domain.OQC.AQL)obj).LotSizeMax;
            row["SampleSize"] = ((Domain.OQC.AQL)obj).SampleSize;
            row["RejectSieze"] = ((Domain.OQC.AQL)obj).RejectSize;
            row["MaintainUser"] = ((Domain.OQC.AQL)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((Domain.OQC.AQL)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((Domain.OQC.AQL)obj).MaintainTime);
            return row;
        }



        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_oqcfade == null)
            {
                _oqcfade = new BenQGuru.eMES.OQC.OQCFacade(base.DataProvider);
            }

            return this._oqcfade.GetAqlForQuery(this.txtAQLLevelQuery.Text.Trim().ToUpper(), inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_oqcfade == null)
            {
                _oqcfade = new BenQGuru.eMES.OQC.OQCFacade(base.DataProvider);
            }
            return this._oqcfade.GetAqlCountForQuery(this.txtAQLLevelQuery.Text.Trim().ToUpper());

        }


        #endregion

        #region Button
        protected void cmdQuery_ServerClick(object sender, EventArgs e)
        {
            this.gridHelper.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);

        }

        protected override void AddDomainObject(object domainObject)
        {
            if (_oqcfade == null)
            {
                _oqcfade = new BenQGuru.eMES.OQC.OQCFacade(base.DataProvider);
            }

            Object objAQL = _oqcfade.GetAQL((domainObject as Domain.OQC.AQL).AQLSeq, (domainObject as Domain.OQC.AQL).AqlLevel);

            if (objAQL != null)
            {
                WebInfoPublish.Publish(this, "$Error_PK_is_Repeat", this.languageComponent1);
                return;
            }

            int LotSizeMin = int.Parse(this.txtLotSizeMin.Text.Trim());
            int LotSizeMax = int.Parse(this.txtLotSizeMax.Text.Trim());
            int count = _oqcfade.GetIsRepeatLotSizeMin2MaxForAdd(LotSizeMin, LotSizeMax, FormatHelper.CleanString(this.txtAqlLevel.Text));
            if (count > 0)
            {
                WebInfoPublish.Publish(this, "$BS_Repeat_LotSize", this.languageComponent1);
                return;
            }

            this._oqcfade.AddAQL((Domain.OQC.AQL)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_oqcfade == null)
            {
                _oqcfade = new BenQGuru.eMES.OQC.OQCFacade(base.DataProvider);
            }
            this._oqcfade.DeleteAQL((Domain.OQC.AQL[])domainObjects.ToArray(typeof(Domain.OQC.AQL)));
        }
        protected override void UpdateDomainObject(object domainObject)
        {
            if (_oqcfade == null)
            {
                _oqcfade = new OQCFacade(this.DataProvider);
            }
            int LotSizeMin = int.Parse(this.txtLotSizeMin.Text.Trim());
            int LotSizeMax = int.Parse(this.txtLotSizeMax.Text.Trim());
            int AqlSeq = int.Parse(this.txtAqlSeq.Text.Trim());
            int count = _oqcfade.GetIsRepeatLotSizeMin2MaxForUpdate(LotSizeMin, LotSizeMax, AqlSeq, FormatHelper.CleanString(this.txtAqlLevel.Text));
            if (count > 0)
            {
                WebInfoPublish.Publish(this, "$BS_Repeat_LotSize", this.languageComponent1);
                return;
            }

            this._oqcfade.UpdateAQL((Domain.OQC.AQL)this.GetEditObject());


        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Update)
            {
                this.txtAqlSeq.ReadOnly = true;
                this.txtAqlLevel.ReadOnly = true;
                txtAqlSeq.Enabled = false;
                txtAqlLevel.Enabled = false;
            }
            if (pageAction == PageActionType.Add)
            {
                this.txtAqlSeq.ReadOnly = false;
                this.txtAqlLevel.ReadOnly = false;
                txtAqlSeq.Enabled = true;
                txtAqlLevel.Enabled = true;
            }
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_oqcfade == null)
            {
                _oqcfade = new OQCFacade(base.DataProvider);
            }
            Domain.OQC.AQL aql = this._oqcfade.CreateNewAQL();
            aql.AQLSeq = int.Parse(this.txtAqlSeq.Text.Trim());
            aql.AqlLevel = FormatHelper.CleanString(this.txtAqlLevel.Text, 40);
            aql.LotSizeMin = int.Parse(this.txtLotSizeMin.Text.Trim());
            aql.LotSizeMax = int.Parse(this.txtLotSizeMax.Text.Trim());
            aql.SampleSize = int.Parse(this.txtSampleSize.Text.Trim());
            aql.RejectSize = int.Parse(this.txtRejectSize.Text.Trim());
            aql.MaintainUser = this.GetUserCode();
            return aql;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_oqcfade == null)
            {
                _oqcfade = new OQCFacade(base.DataProvider);
            }
            object obj = _oqcfade.GetAQL(int.Parse(row.Items.FindItemByKey("AqlSql").Value.ToString()), FormatHelper.CleanString(row.Items.FindItemByKey("AqlLevel").Value.ToString()));
            if (obj != null)
            {
                return obj;
            }
            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtAqlSeq.Text = "";
                this.txtAqlLevel.Text = "";
                this.txtLotSizeMin.Text = "";
                this.txtLotSizeMax.Text = "";
                this.txtSampleSize.Text = "";
                this.txtRejectSize.Text = "";
                return;
            }

            this.txtAqlSeq.Text = ((Domain.OQC.AQL)obj).AQLSeq.ToString();
            this.txtAqlLevel.Text = ((Domain.OQC.AQL)obj).AqlLevel.ToString();
            this.txtLotSizeMin.Text = ((Domain.OQC.AQL)obj).LotSizeMin.ToString();
            this.txtLotSizeMax.Text = ((Domain.OQC.AQL)obj).LotSizeMax.ToString();
            this.txtSampleSize.Text = ((Domain.OQC.AQL)obj).SampleSize.ToString();
            this.txtRejectSize.Text = ((Domain.OQC.AQL)obj).RejectSize.ToString();
        }


        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new NumberCheck(this.lblAqlSeq, this.txtAqlSeq, 0, Int32.MaxValue, true));
            manager.Add(new LengthCheck(this.lblAqlLevel, this.txtAqlLevel, 40, true));
            manager.Add(new NumberCheck(this.lblLotSizeMin, this.txtLotSizeMin, 0, Int32.MaxValue, true));
            manager.Add(new NumberCheck(this.lblLotSizeMax, this.txtLotSizeMax, 0, Int32.MaxValue, true));
            manager.Add(new NumberCheck(this.lblSampleSize, this.txtSampleSize, 0, Int32.MaxValue, true));
            manager.Add(new NumberCheck(this.lblRejectSize, this.txtRejectSize, 0, Int32.MaxValue, true));
            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }
            if (this.txtAqlLevel.Text.Trim().Length == 0)
            {
                WebInfoPublish.Publish(this, "$BS_AqlLevel_Cannot_Empty", this.languageComponent1);
                return false;
            }
            //样本数量不能为零，判退数量不能为零
            if (int.Parse(this.txtSampleSize.Text.Trim()) == 0)
            {
                WebInfoPublish.Publish(this, "$BS_SampleSize_Cannot_Zero", this.languageComponent1);
                return false;
            }
            if (int.Parse(this.txtRejectSize.Text.Trim()) == 0)
            {
                WebInfoPublish.Publish(this, "$BS_RejectSize_Cannot_Zero", this.languageComponent1);
                return false;
            }
            int LotSizeMin = int.Parse(this.txtLotSizeMin.Text.Trim());
            int LotSizeMax = int.Parse(this.txtLotSizeMax.Text.Trim());
            int SampleSize = int.Parse(this.txtSampleSize.Text.Trim());
            int RejectSize = int.Parse(this.txtRejectSize.Text.Trim());
            if (LotSizeMin > LotSizeMax)
            {
                WebInfoPublish.Publish(this, "$BS_RejectSize_CannotExceed_Min2Max", this.languageComponent1);
                return false;
            }
            if (RejectSize > SampleSize)
            {
                WebInfoPublish.Publish(this, "$BS_RejectSize_CannotExceed_Reject2Sample", this.languageComponent1);
                return false;
            }


            if (SampleSize > LotSizeMax)
            {
                WebInfoPublish.Publish(this, "$BS_RejectSize_CannotExceed_Sample2Lot", this.languageComponent1);
                return false;
            }
            return true;
        }


        #endregion

        #region Export


        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{ 
                           ((Domain.OQC.AQL)obj).AqlLevel.ToString(),
                           ((Domain.OQC.AQL)obj).AQLSeq.ToString(),
                           ((Domain.OQC.AQL)obj).LotSizeMin.ToString(),
                           ((Domain.OQC.AQL)obj).LotSizeMax.ToString(),
                           ((Domain.OQC.AQL)obj).SampleSize.ToString(),
                           ((Domain.OQC.AQL)obj).RejectSize.ToString(), 
                           ((Domain .OQC.AQL )obj).GetDisplayText("MaintainUser"),
                           FormatHelper.ToDateString(((Domain.OQC.AQL)obj).MaintainDate),
                           FormatHelper.ToTimeString(((Domain.OQC.AQL)obj).MaintainTime)
                              
                                };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {
	        "AqlLevel",
            "AqlSeq",
            "LotSizeMin",
            "LotSizeMax", 
            "SampleSize", 
            "RejectSize",
            "MaintainUser", 
            "MaintainDate", 
            "MaintainTime", 
            };
        }

        #endregion
    }
}

