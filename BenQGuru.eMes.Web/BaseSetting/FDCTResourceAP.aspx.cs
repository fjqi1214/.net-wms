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

using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FDCTResourceAP 的摘要说明。
    /// </summary>
    public partial class FDCTResourceAP : BaseAPageNew
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
            gridHelper = new GridHelperNew(gridWebGrid, DtSource);
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.txtDctCommandQuery.Text = this.GetRequestParam("dctCode");

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
            base.InitWebGrid();
            this.gridHelper.AddColumn("UnAssResourceCode", "未关联资源代码", null);
            this.gridHelper.AddColumn("ResourceDescription", "资源描述", null);
            this.gridHelper.AddColumn("ResourceStepSequence", "所属产线", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("DctCode", "DCT默认指令", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);

            this.gridHelper.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

        }

        protected override void AddDomainObject(ArrayList domainObject)
        {
            if (facade == null)
            {
                facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            facade.UpdateResource((Resource[])domainObject.ToArray(typeof(Resource)));
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (facade == null)
            {
                facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            Resource relation = facade.CreateNewResource();

            relation.DctCode = this.txtDctCommandQuery.Text.Trim();
            relation.ResourceCode = row.Items[1].Text;
            relation.MaintainUser = this.GetUserCode();

            return relation;
        }

        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            return this.facade.GetUnselectedResourceByDCTCodeCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDctCommandQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtResourceCodeQuery.Text)));
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    ((Resource)obj).ResourceCode.ToString(),
            //                    ((Resource)obj).ResourceDescription,
            //                    ((Resource)obj).StepSequenceCode.ToString(),
            //                    ((Resource)obj).MaintainUser.ToString(),
            //                    ((Resource)obj).DctCode.ToString(),
            //                    FormatHelper.ToDateString(((Resource)obj).MaintainDate),

            //                });
            DataRow row = this.DtSource.NewRow();
            row["UnAssResourceCode"] = ((Resource)obj).ResourceCode.ToString();
            row["ResourceDescription"] = ((Resource)obj).ResourceDescription;
            row["ResourceStepSequence"] = ((Resource)obj).StepSequenceCode.ToString();
            row["MaintainUser"] = ((Resource)obj).MaintainUser.ToString();
            row["DctCode"] = ((Resource)obj).DctCode.ToString();
            row["MaintainDate"] = FormatHelper.ToDateString(((Resource)obj).MaintainDate);
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            return facade.GetUnselectedResourceByDCTCode(
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDctCommandQuery.Text)),
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtResourceCodeQuery.Text)),
                    inclusive, exclusive);
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(
                this.MakeRedirectUrl(@"./FDCTResourceSP.aspx",
                                    new string[] { "dctCode" },
                                    new string[] { this.txtDctCommandQuery.Text }));
        }
        #endregion

    }
}
