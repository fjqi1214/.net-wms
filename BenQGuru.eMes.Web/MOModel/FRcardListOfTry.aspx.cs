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
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.MOModel;
using Infragistics.Web.UI.GridControls;

namespace BenQGuru.eMES.Web.MOModel
{
    public partial class FRcardListOfTry : BaseMPageNew
    {
        protected System.Web.UI.WebControls.Label lblOperationSelectTitle;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        private TryFacade facade = null;

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

        #region Stable
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                if (Page.Request["tryCode"] != null && Page.Request["tryCode"] != string.Empty)
                {
                    this.txtTryCodeQuery.Text = Page.Request["tryCode"];
                }

            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        #endregion

        #region NotStable
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("RunningCard", "序列号", null);
            this.gridHelper.AddColumn("CartonCode", "箱号", null);
            this.gridHelper.AddColumn("PalletCode", "栈板号", null);
            this.gridHelper.AddColumn("ItemCode", "产品", null);
            this.gridHelper.AddColumn("ItemDescription", "产品描述", null);

            this.gridHelper.AddColumn("OPCode", "工序代码", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);

            this.gridHelper.AddDefaultColumn(false, false);
            ((BoundDataField)this.gridWebGrid.Columns.FromKey("RunningCard")).HtmlEncode = false;
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            this.gridHelper.RequestData();
        }

        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new TryFacade(this.DataProvider);
            }
            return this.facade.QueryTry2RcardByTryCodeCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtTryCodeQuery.Text)));
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = DtSource.NewRow();
            row["RunningCard"] = GetRCardLink(((Try2RcardNew)obj).RCard.ToString());
            row["CartonCode"] = ((Try2RcardNew)obj).CartonCode.ToString();
            row["PalletCode"] = ((Try2RcardNew)obj).PalletCode.ToString();
            row["ItemCode"] = ((Try2RcardNew)obj).ItemCode.ToString();
            row["ItemDescription"] = ((Try2RcardNew)obj).ItemDescription.ToString();
            row["OPCode"] = ((Try2RcardNew)obj).OPCode.ToString();
            row["MaintainUser"] = ((Try2RcardNew)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((Try2RcardNew)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((Try2RcardNew)obj).MaintainTime);
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new TryFacade(this.DataProvider);
            }
            return facade.QueryTry2RcardByTryCode(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtTryCodeQuery.Text)),
                inclusive, exclusive);
        }

        #endregion


        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{
                                ((Try2RcardNew)obj).RCard.ToString(),
                                ((Try2RcardNew)obj).CartonCode.ToString(),
                                ((Try2RcardNew)obj).PalletCode.ToString(),
                                ((Try2RcardNew)obj).ItemCode.ToString(),
                                ((Try2RcardNew)obj).ItemDescription.ToString(),
                                ((Try2RcardNew)obj).OPCode.ToString(),
                                ((Try2RcardNew)obj).GetDisplayText("MaintainUser"),
								FormatHelper.ToDateString(((Try2RcardNew)obj).MaintainDate),							
								FormatHelper.ToTimeString(((Try2RcardNew)obj).MaintainTime)
            };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"RunningCard",
									"CartonCode",	
									"PalletCode",
                                    "ItemCode",
                                    "ItemDescription",
                                    "OPCode",
									"MaintainUser",	
									"MaintainDate",
                                    "MaintainTime",                        
                                };
        }

        #endregion

        private string GetRCardLink(string rcard)
        {
            return string.Format("<a href='../WebQuery/FItemTracingQP.aspx?RCARDFROM={0}&RCARDTO={1}'>{2}</a>", rcard, rcard, rcard);
        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("FTryMP.aspx"));
        }
    }
}
