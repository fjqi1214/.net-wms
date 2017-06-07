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
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Web.UserControl;
using Infragistics.Web.UI.GridControls;

namespace BenQGuru.eMES.Web.SelectQuery
{
    /// <summary>
    /// FOrgSP 的摘要说明。
    /// </summary>
    public partial class FOrgSP : BaseSelectorPageNew
    {

        private BenQGuru.eMES.SelectQuery.SPFacade _facade = null;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
            }
            //if (!IsPostBack)
            //{
            //    base.Page_Load(sender, e);
            //    base.cmdQuery_ServerClick(sender, e);
            //}
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Control control;
            control = this.FindControl("cmdSave");
            if (control != null)
            {
                ((System.Web.UI.HtmlControls.HtmlInputButton)control).Attributes.Remove("OnClick");
                ((System.Web.UI.HtmlControls.HtmlInputButton)control).Attributes.Add("OnClick", "returnSelectedOrg('" + MessageCenter.ParserMessage("$UserMP_No_Default_Org", this.languageComponent1) + "')");
            }
            //control = this.FindControl("cmdInit");
            //if (control != null)
            //{
            //    ((System.Web.UI.HtmlControls.HtmlInputButton)control).ServerClick += new System.EventHandler(this.cmdInit_ServerClick);
            //}
        }

        private BenQGuru.eMES.SelectQuery.SPFacade facade
        {
            get
            {
                if (_facade == null)
                    _facade = new FacadeFactory(base.DataProvider).CreateSPFacade();

                return _facade;
            }
        }

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
            //gridSelected.DisplayLayout.ClientSideEvents.CellClickHandler = "SingleRowSelectForDefaultOrg";   
        }
        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            this.gridSelectedHelper.AddCheckBoxColumn("DefaultOrg", "默认组织", false);
            this.gridSelectedHelper.AddColumn("Selector_SelectedCode", "组织编号", null);
            this.gridSelectedHelper.AddColumn("OrgDesc", "组织", null);
            this.gridSelectedHelper.AddDefaultColumn(true, false);
            this.gridSelectedHelper.ApplyLanguage(this.languageComponent1);


            this.gridUnSelectedHelper.AddColumn("Selector_UnselectedCode", "组织编号", null);
            this.gridUnSelectedHelper.AddColumn("OrgDesc", "组织", null);
            this.gridUnSelectedHelper.AddDefaultColumn(true, false);
            this.gridUnSelectedHelper.ApplyLanguage(this.languageComponent1);

            //this.gridSelectedHelper.Grid.DisplayLayout.ClientSideEvents.CellClickHandler = "SingleRowSelectForDefaultOrg";

            this.gridHelper2 = gridSelectedHelper;
            this.gridHelper = gridUnSelectedHelper;

            base.InitWebGrid();
            base.InitWebGrid2();

            this.gridSelected.Columns.FromKey("Selector_SelectedCode").Hidden = true;
            this.gridUnSelected.Columns.FromKey("Selector_UnselectedCode").Hidden = true;
            this.gridSelected.Columns.FromKey("Selector_SelectedDesc").Hidden = true;
            this.gridUnSelected.Columns.FromKey("Selector_UnSelectedDesc").Hidden = true;
        }

        protected override DataRow GetSelectedGridRow(object obj)
        {
            DataRow row = DtSourceSelected.NewRow();
            row["DefaultOrg"] = false;
            if (!IsPostBack)
            {
                if (Request.Params["defaultOthers"] != null)
                {
                    string defaultOthers = System.Web.HttpUtility.UrlDecode(Request.Params["defaultOthers"].ToString());
                    if (((BenQGuru.eMES.Domain.BaseSetting.Organization)obj).OrganizationID.ToString() == defaultOthers)
                        row["DefaultOrg"] = true;
                }
            }
            row["Selector_SelectedCode"] = ((BenQGuru.eMES.Domain.BaseSetting.Organization)obj).OrganizationID.ToString();
            row["OrgDesc"] = ((BenQGuru.eMES.Domain.BaseSetting.Organization)obj).OrganizationDescription;
            return row;
        }

        protected override DataRow GetUnSelectedGridRow(object obj)
        {
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = ((BenQGuru.eMES.Domain.BaseSetting.Organization)obj).OrganizationID.ToString();
            row["OrgDesc"] = ((BenQGuru.eMES.Domain.BaseSetting.Organization)obj).OrganizationDescription;
            return row;
        }

        protected override object[] LoadSelectedDataSource(int inclusive, int exclusive)
        {
            return this.facade.QuerySelectedOrg(this.GetSelectedCodes());

        }

        protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
            return this.facade.QueryUnSelectedOrg(this.GetSelectedCodes(), inclusive, exclusive);
        }


        protected override int GetUnSelectedRowCount()
        {
            return this.facade.QueryUnSelectedOrgCount(this.GetSelectedCodes());
        }

        protected override void AddNewRow(ArrayList rows)
        {
            foreach (GridRecord row in rows)
            {
                DataRow newrow = DtSourceSelected.NewRow();
                newrow["GUID"] = row.Items.FindItemByKey("GUID").Value;
                newrow["Selector_SelectedCode"] = row.Items.FindItemByKey("Selector_UnselectedCode").Value;
                newrow["OrgDesc"] = row.Items.FindItemByKey("OrgDesc").Value;
                DtSourceSelected.Rows.Add(newrow);
            }
            this.gridSelectedHelper.Grid.DataSource = DtSourceSelected;
            this.gridSelectedHelper.Grid.DataBind();
        }

        protected override void SetSelectedCodes()
        {
            int rowCount = this.gridSelectedHelper.Grid.Rows.Count;
            if (rowCount <= 0)
                rowCount = 0;
            string[] codes = new string[rowCount];
            for (int i = 0; i < codes.Length; i++)
            {
                codes[i] = this.gridSelectedHelper.Grid.Rows[i].Items.FindItemByKey("OrgDesc").Text;
            }

            string[] others = new string[rowCount];
            for (int i = 0; i < others.Length; i++)
            {
                others[i] = this.gridSelectedHelper.Grid.Rows[i].Items.FindItemByKey("Selector_SelectedCode").Text;
            }

            Control control = this.FindControl("txtSelected");
            if (control == null)
            {
                return;
            }

            else
            {
                ((System.Web.UI.HtmlControls.HtmlTextArea)control).Value = string.Join(DATA_SPLITER, codes);
            }

            control = this.FindControl("txtOthers");
            if (control == null)
            {
                return;
            }

            else
            {
                string defaultOrgID = string.Empty;
                for (int i = 0; i < rowCount; i++)
                {
                    if (gridSelected.Rows[i].Items.FindItemByKey(gridSelectedHelper.CheckColumnKey).Text == "true")
                    {
                        defaultOrgID = "(" + gridSelected.Rows[i].Items.FindItemByKey("Selector_SelectedCode").Text + ")";
                        break;
                    }
                }

                ((System.Web.UI.HtmlControls.HtmlTextArea)control).Value = defaultOrgID + string.Join(DATA_SPLITER, others);
            }
        }

        protected override string[] GetSelectedCodes()
        {
            if (!IsPostBack)
            {
                if (Request.Params["Others"] != null)
                {
                    if (Request.Params["Others"].ToString().Trim() == string.Empty)
                    {
                        return new string[0];
                    }
                    else
                    {
                        return System.Web.HttpUtility.UrlDecode(Request.Params["Others"].ToString()).Split(DATA_SPLITER.ToCharArray());
                    }
                }
            }

            Control control = this.FindControl("txtOthers");
            if (control == null)
            {
                return new string[0];
            }

            else
            {
                string orgIDList = ((System.Web.UI.HtmlControls.HtmlTextArea)control).Value.Trim();
                if (orgIDList.Length == 0)
                {
                    return new string[0];
                }

                if (orgIDList.IndexOf(")") > 0)
                {
                    orgIDList = orgIDList.Substring(orgIDList.IndexOf(")") + 1).Trim();
                }

                return orgIDList.Split(DATA_SPLITER.ToCharArray());
            }
        }

        protected override void cmdInit_ServerClick(object sender, System.EventArgs e)
        {
            this.gridSelectedHelper.RequestData();
            this.gridUnSelectedHelper.RequestData();

            Control control = this.FindControl("txtOthers");
            if (control != null)
            {
                string orgIDList = ((System.Web.UI.HtmlControls.HtmlTextArea)control).Value.Trim();
                string defaultOrgID = string.Empty;

                if (orgIDList.IndexOf(")") > 0)
                {
                    defaultOrgID = orgIDList.Substring(1, orgIDList.IndexOf(")") - 1);

                    for (int i = 0; i < gridSelected.Rows.Count; i++)
                    {
                        if (gridSelected.Rows[i].Items.FindItemByKey("Selector_SelectedCode").Text == defaultOrgID)
                        {
                            gridSelected.Rows[i].Items.FindItemByKey("DefaultOrg").Value = true;
                            break;
                        }
                    }
                }
            }

            SetSelectedCodes();
        }
        #endregion
    }
}
