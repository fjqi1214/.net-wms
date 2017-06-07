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
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Domain.OQC;
using Infragistics.Web.UI.GridControls;

namespace BenQGuru.eMES.Web.OQC
{
    /// <summary>
    /// FOQCCheckGroup2ListAdd 的摘要说明。
    /// </summary>
    public partial class FOQCCheckItemByCheckGroup : BaseAPageNew
    {
        protected System.Web.UI.WebControls.Label lblOperationSelectTitle;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private OQCFacade facade = null;
        protected ExcelExporter excelExporter = null;

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

                this.txtCheckGroupQuery.Text = this.GetRequestParam("checkGroup");

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
            this.gridHelper.AddColumn("UnassCheckItemCode", "检验项目代码", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);


            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            this.gridHelper.AddDefaultColumn(true, false);
            base.InitWebGrid();

            //  this.gridHelper.RequestData();
        }

        protected override void AddDomainObject(ArrayList domainObject)
        {
            if (facade == null)
            {
                facade = new OQCFacade(this.DataProvider);
            }
            facade.AddOQCCheckGroup2List((OQCCheckGroup2List[])domainObject.ToArray(typeof(OQCCheckGroup2List)));
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (facade == null)
            {
                facade = new OQCFacade(this.DataProvider);
            }
            OQCCheckGroup2List oqcCheckGroup2List = facade.CreateNewOQCCheckGroup2List();

            oqcCheckGroup2List.CheckGroupCode = this.txtCheckGroupQuery.Text;
            oqcCheckGroup2List.CheckItemCode = row.Items.FindItemByKey("UnassCheckItemCode").Text.Trim();

            oqcCheckGroup2List.MaintainUser = this.GetUserCode();

            return oqcCheckGroup2List;
        }


        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new OQCFacade(this.DataProvider);
            }
            return this.facade.QueryOQCCheckItemByCheckGroupCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCheckGroupQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCheckItemCodeQuery.Text)));
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = DtSource.NewRow();
            row["UnassCheckItemCode"] = ((OQCCheckGroup2List)obj).CheckItemCode.ToString();
            row["MaintainUser"] = ((OQCCheckGroup2List)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((OQCCheckGroup2List)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((OQCCheckGroup2List)obj).MaintainTime);
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new OQCFacade(this.DataProvider);
            }
            return facade.QueryOQCCheckItemByCheckGroup(
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCheckGroupQuery.Text)),
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCheckItemCodeQuery.Text)),
                    inclusive, exclusive);
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            //this.Response.Redirect(
            //    this.MakeRedirectUrl(@"./FOQCCheckGroup2List.aspx",
            //                        new string[] { "checkGroup" },
            //                        new string[] { this.txtCheckGroupQuery.Text }));
        }


        #endregion

    }
}
