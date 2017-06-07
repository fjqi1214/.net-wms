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
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.BaseSetting;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    public partial class FShiftCrewResourceSP : BaseMPageNew
    {
        protected System.Web.UI.WebControls.Label lblOperationSelectTitle;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        private ShiftModel facade = null;
        private BaseModelFacade _facade = null;

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
            //this.pagerSizeSelector.Readonly = true;

            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.txtCrewCodeQuery.Text = this.GetRequestParam("crewCode");
       
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
            this.gridHelper.AddColumn("AssResourceCode", "资源代码", null);
            this.gridHelper.AddColumn("ResourceDescription", "资源描述", null);
            this.gridHelper.AddColumn("StepSequenceCode", "所属产线", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            this.gridHelper.AddDefaultColumn(true, false);

            this.gridHelper.RequestData();
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (facade == null)
            {
                facade = new ShiftModel(this.DataProvider);
            }
            this.facade.DeleteShiftCrewResource((Resource[])domainObjects.ToArray(typeof(Resource)));
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacade(base.DataProvider);
            }
            object obj = _facade.GetResource(row.Items.FindItemByKey("AssResourceCode").Text.ToString());

            if (obj != null)
            {
                return (Resource)obj;
            }

            return null;
        }

        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new ShiftModel(this.DataProvider);
            }
            return this.facade.QueryShiftCrewResourceCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCrewCodeQuery.Text)));
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    ((Resource)obj).ResourceCode.ToString(),
            //                    ((Resource)obj).ResourceDescription.ToString(),
            //                    ((Resource)obj).StepSequenceCode.ToString(),
            //                    ((Resource)obj).MaintainUser,
            //                    FormatHelper.ToDateString(((Resource)obj).MaintainDate)							
            //                });
            DataRow row = this.DtSource.NewRow();
            row["AssResourceCode"] = ((Resource)obj).ResourceCode.ToString();
            row["ResourceDescription"] = ((Resource)obj).ResourceDescription.ToString();
            row["StepSequenceCode"] = ((Resource)obj).StepSequenceCode.ToString();
            row["MaintainUser"] = ((Resource)obj).MaintainUser;
            row["MaintainDate"] = FormatHelper.ToDateString(((Resource)obj).MaintainDate);
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new ShiftModel(this.DataProvider);
            }
            return facade.QueryShiftCrewResource(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCrewCodeQuery.Text)),
                inclusive, exclusive);
        }

        //protected void cmdSelect_ServerClick(object sender, System.EventArgs e)
        //{
        //    this.Response.Redirect(
        //            this.MakeRedirectUrl("./FShiftCrewResourceAP.aspx",
        //                                    new string[] { "crewCode" },
        //                                    new string[] { this.txtCrewCodeQuery.Text.Trim() }));
        //}

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("./FShiftCrewMP.aspx"));
        }

        #endregion


        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{((Resource)obj).ResourceCode.ToString(),
                                   ((Resource)obj).ResourceDescription.ToString(),
                                   ((Resource)obj).StepSequenceCode.ToString(),
								   ((Resource)obj).MaintainUser.ToString(),
								   FormatHelper.ToDateString(((Resource)obj).MaintainDate)};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"AssResourceCode",
                                    "ResourceDescription",
                                    "StepSequenceCode",
									"MaintainUser",	
									"MaintainDate"};
        }

        #endregion

        protected void btnRefesh_Click(object sender, EventArgs e)
        {
            this.gridHelper.RequestData();
        }

    }
}
