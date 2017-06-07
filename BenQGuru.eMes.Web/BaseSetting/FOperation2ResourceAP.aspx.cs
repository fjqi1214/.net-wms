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
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using Infragistics.Web.UI.GridControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FOperation2ResourceSP 的摘要说明。
    /// </summary>
    public partial class FOperation2ResourceAP : BaseAPageNew
    {
        protected System.Web.UI.WebControls.Label lblOperationSelectTitle;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private BaseModelFacade facade = null;//new BaseModelFacadeFactory().Create();	

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

                this.txtOperationCodeQuery.Text = this.GetRequestParam("opcode");
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        #region Not Stable

        protected override void InitWebGrid()
        {
            this.gridHelper.AddColumn("UnAssResourceCode", "未关联资源代码", null);
            this.gridHelper.AddColumn("ResourceDescription", "资源描述", null);
            this.gridHelper.AddColumn("ResourceStepSequence", "所属产线", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);

            this.gridHelper.AddColumn("ResourceType", "资源类别", null);
            this.gridHelper.AddColumn("ResourceGroup", "资源归属", null);
            this.gridHelper.AddColumn("ShiftTypeCode", "班制代码", null);

            this.gridHelper.AddColumn("SegmentCode", "工段代码", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);

            this.gridWebGrid.Columns.FromKey("ResourceType").Hidden = true;
            this.gridWebGrid.Columns.FromKey("ResourceGroup").Hidden = true;
            this.gridWebGrid.Columns.FromKey("ShiftTypeCode").Hidden = true;
            this.gridWebGrid.Columns.FromKey("SegmentCode").Hidden = true;
            this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;

            this.gridHelper.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            base.InitWebGrid();
        }

        protected override void AddDomainObject(ArrayList domainObject)
        {
            if (facade == null)
            {
                facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            facade.AddOperation2Resource((Operation2Resource[])domainObject.ToArray(typeof(Operation2Resource)));
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (facade == null)
            {
                facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            Operation2Resource relation = facade.CreateNewOperation2Resource();

            relation.OPCode = this.txtOperationCodeQuery.Text.Trim();
            relation.ResourceCode = row.Items.FindItemByKey("UnAssResourceCode").Text;
            relation.MaintainUser = this.GetUserCode();

            return relation;
        }

        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            return this.facade.GetUnselectedResourceByOperationCodeCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOperationCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtResourceCodeQuery.Text)));
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = DtSource.NewRow();
            row["UnAssResourceCode"] = ((Resource)obj).ResourceCode.ToString();
            row["ResourceDescription"] = ((Resource)obj).ResourceDescription;
            row["ResourceStepSequence"] = ((Resource)obj).GetDisplayText("StepSequenceCode");
            row["MaintainUser"] = ((Resource)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((Resource)obj).MaintainDate);
            row["ResourceType"] = ((Resource)obj).ResourceType.ToString();
            row["ResourceGroup"] = ((Resource)obj).ResourceGroup.ToString();
            row["ShiftTypeCode"] = ((Resource)obj).ShiftTypeCode;
            row["SegmentCode"] = ((Resource)obj).SegmentCode.ToString();
            row["MaintainTime"] = FormatHelper.ToTimeString(((Resource)obj).MaintainTime);
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            return facade.GetUnselectedResourceByOperationCode(
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOperationCodeQuery.Text)),
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtResourceCodeQuery.Text)),
                    inclusive, exclusive);
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(
                this.MakeRedirectUrl(@"./FOperation2ResourceSP.aspx",
                                    new string[] { "opcode" },
                                    new string[] { this.txtOperationCodeQuery.Text }));
        }
        #endregion

    }
}
