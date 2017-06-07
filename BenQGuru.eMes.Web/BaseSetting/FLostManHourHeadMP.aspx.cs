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
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Performance;
using BenQGuru.eMES.Performance;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FShiftMP 的摘要说明。
    /// </summary>
    public partial class FLostManHourHeadMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        private PerformanceFacade _facade = null;
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

          //  this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
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
            this.gridHelper.AddColumn("Date", "日期", null);
            this.gridHelper.AddColumn("sscode", "产线代码", null);
            this.gridHelper.AddColumn("StepSequenceDescription", "生产线描述", null);
            this.gridHelper.AddColumn("ShiftCode", "班次代码", null);
            this.gridHelper.AddColumn("ShiftDescription", "班次描述", null);
            this.gridHelper.AddColumn("ItemCode", "产品代码", null);
            this.gridHelper.AddColumn("ItemDesc", "产品描述", null);
            this.gridHelper.AddColumn("ACTManHour", "出勤工时", null);
            this.gridHelper.AddColumn("ACTOutPut", "实际产量", null);
            this.gridHelper.AddColumn("AcquireManHour", "获得工时", null);
            this.gridHelper.AddColumn("LostManHour", "损失工时", null);
            this.gridHelper.AddLinkColumn("detail", "详细信息", null);
            
            this.gridHelper.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    FormatHelper.ToDateString(((LostManHourHeadWithMessage)obj).ShiftDate),
            //                    ((LostManHourHeadWithMessage)obj).SSCode.ToString(),
            //                    ((LostManHourHeadWithMessage)obj).StepSequenceDescription.ToString(),
            //                    ((LostManHourHeadWithMessage)obj).ShiftCode.ToString(),
            //                    ((LostManHourHeadWithMessage)obj).ShiftDescription.ToString(),
            //                    ((LostManHourHeadWithMessage)obj).ItemCode.ToString(),
            //                    ((LostManHourHeadWithMessage)obj).ItemDescription.ToString(),			
            //                    Convert.ToString(Math.Round(Convert.ToDouble(((LostManHourHeadWithMessage)obj).ActManHour)/3600,2)),
            //                    ((LostManHourHeadWithMessage)obj).ActOutput.ToString(),
            //                     Convert.ToString(Math.Round(Convert.ToDouble(((LostManHourHeadWithMessage)obj).AcquireManHour)/3600,2)),
            //                    Convert.ToString(Math.Round(Convert.ToDouble(((LostManHourHeadWithMessage)obj).LostManHour)/3600,2)),
            //                    ""});
            DataRow row = this.DtSource.NewRow();
            row["Date"] = FormatHelper.ToDateString(((LostManHourHeadWithMessage)obj).ShiftDate);
            row["sscode"] = ((LostManHourHeadWithMessage)obj).SSCode.ToString();
            row["StepSequenceDescription"] = ((LostManHourHeadWithMessage)obj).StepSequenceDescription.ToString();
            row["ShiftCode"] = ((LostManHourHeadWithMessage)obj).ShiftCode.ToString();
            row["ShiftDescription"] = ((LostManHourHeadWithMessage)obj).ShiftDescription.ToString();
            row["ItemCode"] = ((LostManHourHeadWithMessage)obj).ItemCode.ToString();
            row["ItemDesc"] = ((LostManHourHeadWithMessage)obj).ItemDescription.ToString();
            row["ACTManHour"] = Convert.ToString(Math.Round(Convert.ToDouble(((LostManHourHeadWithMessage)obj).ActManHour) / 3600, 2));
            row["ACTOutPut"] = ((LostManHourHeadWithMessage)obj).ActOutput.ToString();
            row["AcquireManHour"] = Convert.ToString(Math.Round(Convert.ToDouble(((LostManHourHeadWithMessage)obj).AcquireManHour) / 3600, 2));
            row["LostManHour"] = Convert.ToString(Math.Round(Convert.ToDouble(((LostManHourHeadWithMessage)obj).LostManHour) / 3600, 2));
            return row;

        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }
            return this._facade.QueryLostManHourHead(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSSQuery.Text)),
                FormatHelper.TODateInt(this.DateQuery.Text),                
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtShiftCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }
            return this._facade.GetLostManHourHeadCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSSQuery.Text)),
                FormatHelper.TODateInt(this.DateQuery.Text),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtShiftCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)));
        }

        #endregion

        #region Button

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName=="detail")
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FLostManDetailMP.aspx",
                                                            new string[] { "Date", "sscode", "ShiftCode", "ItemCode", "ItemDesc", "LostManHour" },
                                                            new string[] { row.Items.FindItemByKey("Date").Text.Trim(), row.Items.FindItemByKey("sscode").Text.Trim(),
                                                                           row.Items.FindItemByKey("ShiftCode").Text.Trim(), row.Items.FindItemByKey("ItemCode").Text.Trim(),
                                                                           row.Items.FindItemByKey("ItemDesc").Text.Trim(),row.Items.FindItemByKey("LostManHour").Text.Trim(),}));
            }
        }
        #endregion


        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{FormatHelper.ToDateString(((LostManHourHeadWithMessage)obj).ShiftDate),
								((LostManHourHeadWithMessage)obj).SSCode.ToString(),
                                ((LostManHourHeadWithMessage)obj).StepSequenceDescription.ToString(),
								((LostManHourHeadWithMessage)obj).ShiftCode.ToString(),
                                ((LostManHourHeadWithMessage)obj).ShiftDescription.ToString(),
								((LostManHourHeadWithMessage)obj).ItemCode.ToString(),
					            ((LostManHourHeadWithMessage)obj).ItemDescription.ToString(),			
                                Convert.ToString(Math.Round(Convert.ToDouble(((LostManHourHeadWithMessage)obj).ActManHour)/3600,2)),
                                ((LostManHourHeadWithMessage)obj).ActOutput.ToString(),
					             Convert.ToString(Math.Round(Convert.ToDouble(((LostManHourHeadWithMessage)obj).AcquireManHour)/3600,2)),
                                 Convert.ToString(Math.Round(Convert.ToDouble(((LostManHourHeadWithMessage)obj).LostManHour)/3600,2)),
                                };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	
									"Date",
									"SSCode",
									"StepSequenceDescription",
									"ShiftCode",
                                    "ShiftDescription",
                                    "ItemCode",
                                    "ItemDesc",
                                    "ACTManHour",
									"ACTOutPut",
									"AcquireManHour",
									"LostManHour"};
        }
        #endregion
    }
}
