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
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FOperation2ResourceSP 的摘要说明。
    /// </summary>
    public partial class FOperation2ResourceSP : BaseMPageNew
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
            //this.pagerSizeSelector.Readonly = true;

            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.txtOperationCodeQuery.Text = this.GetRequestParam("opcode");

                if (facade == null)
                {
                    facade = new BaseModelFacadeFactory(base.DataProvider).Create();
                }
                Operation op = (Operation)facade.GetOperation(this.GetRequestParam("opcode"));
                if (op != null)
                {
                    this.txtOPDescriptionQuery.Text = op.OPDescription;
                }
         
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
            this.gridHelper.AddColumn("AssResourceCode", "已关联资源代码", null);
            this.gridHelper.AddColumn("ResourceDescription", "资源描述", null);
            this.gridHelper.AddColumn("ResourceStepSequence", "所属产线", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);

            this.gridHelper.AddColumn("ResourceType", "资源类别", null);
            this.gridHelper.AddColumn("ResourceGroup", "资源归属", null);
            this.gridHelper.AddColumn("ShiftTypeCode", "班制代码", null);

            this.gridHelper.AddColumn("SegmentCode", "工段代码", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);
            this.gridHelper.AddColumn("ResourceSequence", "资源序列", null);

            this.gridWebGrid.Columns.FromKey("ResourceType").Hidden = true;
            this.gridWebGrid.Columns.FromKey("ResourceGroup").Hidden = true;
            this.gridWebGrid.Columns.FromKey("ShiftTypeCode").Hidden = true;
            this.gridWebGrid.Columns.FromKey("SegmentCode").Hidden = true;
            this.gridWebGrid.Columns.FromKey("ResourceSequence").Hidden = true;
            this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            this.gridHelper.AddDefaultColumn(true, false);
            this.gridHelper.RequestData();
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (facade == null)
            {
                facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            this.facade.UpdateOperation2Resource((Operation2Resource)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (facade == null)
            {
                facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            this.facade.DeleteOperation2Resource((Operation2Resource[])domainObjects.ToArray(typeof(Operation2Resource)));
        }

        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            return this.facade.GetSelectedResourceByOperationCodeCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOperationCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtResourceCodeQuery.Text)));
        }

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{((ResourceOfOperation)obj).ResourceCode.ToString(),
								   ((ResourceOfOperation)obj).ResourceDescription,
								   ((ResourceOfOperation)obj).GetDisplayText("StepSequenceCode"),
								   ((ResourceOfOperation)obj).GetDisplayText("MaintainUser"),
								   FormatHelper.ToDateString(((ResourceOfOperation)obj).MaintainDate)};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"ResourceCode",
									"ResourceDescription",
									"StepSequenceCode",
									"MaintainUser",	
									"MaintainDate"};
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = DtSource.NewRow();
            row["AssResourceCode"] = ((ResourceOfOperation)obj).ResourceCode.ToString();
            row["ResourceDescription"] = ((ResourceOfOperation)obj).ResourceDescription;
            row["ResourceStepSequence"] = ((ResourceOfOperation)obj).GetDisplayText("StepSequenceCode");
            row["MaintainUser"] = ((ResourceOfOperation)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((ResourceOfOperation)obj).MaintainDate);
            row["ResourceType"] = ((ResourceOfOperation)obj).ResourceType.ToString();
            row["ResourceGroup"] = ((ResourceOfOperation)obj).ResourceGroup.ToString();
            row["ShiftTypeCode"] = ((ResourceOfOperation)obj).ShiftTypeCode;
            row["SegmentCode"] = ((ResourceOfOperation)obj).SegmentCode.ToString();
            row["MaintainTime"] = ((ResourceOfOperation)obj).ResourceSequence.ToString();
            row["ResourceSequence"] = FormatHelper.ToTimeString(((ResourceOfOperation)obj).MaintainTime);
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            return facade.GetSelectedResourceOfOperationByOperationCode(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOperationCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtResourceCodeQuery.Text)),
                inclusive, exclusive);
        }

        //protected void cmdSelect_ServerClick(object sender, System.EventArgs e)
        //{
        //    this.Response.Redirect(
        //            this.MakeRedirectUrl("./FOperation2ResourceAP.aspx",
        //                                    new string[] {"opcode"},
        //                                    new string[] {this.txtOperationCodeQuery.Text.Trim()}));
        //}

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("./FOperationMP.aspx"));
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtResourceCodeEdit.Text = "";
                this.txtResourceSequenceEdit.Text = "";

                return;
            }
            this.txtResourceCodeEdit.Text = ((Operation2Resource)obj).ResourceCode;
            this.txtResourceSequenceEdit.Text = ((Operation2Resource)obj).ResourceSequence.ToString();

        }

        protected override object GetEditObject()
        {
            if (facade == null)
            {
                facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            Operation2Resource relation = this.facade.CreateNewOperation2Resource();
            relation.OPCode = FormatHelper.CleanString(this.txtOperationCodeQuery.Text.Trim(), 40);
            relation.ResourceCode = FormatHelper.CleanString(this.txtResourceCodeEdit.Text, 40);
            relation.ResourceSequence = 0;            // System.Int32.Parse( this.txtResourceSequenceEdit.Text );
            relation.MaintainUser = this.GetUserCode();

            return relation;
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (facade == null)
            {
                facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            object obj = this.facade.GetOperation2Resource(this.txtOperationCodeQuery.Text.Trim(), row.Items.FindItemByKey("AssResourceCode").Text.Trim());

            if (obj != null)
            {
                return (Operation2Resource)obj;
            }

            return null;
        }

        protected override bool ValidateInput()
        {
            //PageCheckManager manager = new PageCheckManager();

            //manager.Add(new NumberCheck(lblResourceSequenceEdit,txtResourceSequenceEdit,true));

            //if ( !manager.Check() )
            //{
            //    WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);

            //    return false;
            //}

            return true;
        }
        #endregion

        protected void btnRefesh_Click(object sender, EventArgs e)
        {
            this.gridHelper.RequestData();
        }

    }
}
