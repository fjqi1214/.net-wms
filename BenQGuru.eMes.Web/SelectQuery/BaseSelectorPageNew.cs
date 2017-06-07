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
using System.Collections.Generic;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.Helper
{
    /// <summary>
    /// Selector 的摘要说明。
    /// </summary>
    public class BaseSelectorPageNew : BasePageSelectNew
    {
        protected System.ComponentModel.IContainer components;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        //protected GridHelperNew gridSelectedHelper = null;
        //protected GridHelperNew gridUnSelectedHelper = null;

        // 分隔符
        protected const string DATA_SPLITER = ",";
        protected bool writerOutted = false;

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
            this.gridUnSelected = this.gridWebGrid;
            this.gridSelected = this.gridWebGrid2;
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
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Control control;
            control = this.FindControl("cmdUnSelect");
            if (control != null)
            {
                ((System.Web.UI.WebControls.Button)control).Attributes.Add("onclick", "GetSelectRowGUIDS('" + this.gridSelected.ID+ "');");
                ((System.Web.UI.WebControls.Button)control).Click += new System.EventHandler(this.cmdUnSelect_Click);
            }
            control = this.FindControl("cmdSelect");
            if (control != null)
            {
                ((System.Web.UI.WebControls.Button)control).Attributes.Add("onclick", "GetSelectRowGUIDS('" + this.gridUnSelected.ID + "');");
                ((System.Web.UI.WebControls.Button)control).Click += new System.EventHandler(this.cmdSelect_Click);
            }
            control = this.FindControl("cmdQuery");
            if (control != null)
            {
                ((System.Web.UI.HtmlControls.HtmlInputButton)control).ServerClick += new System.EventHandler(this.cmdQuery_ServerClick);
            }

            this.gridSelectedHelper = new GridHelperNew(this.GetGridSelected(), DtSourceSelected);
            this.gridHelper2= gridSelectedHelper;

            this.gridUnSelectedHelper = new GridHelperNew(this.GetGridUnSelected(), DtSourceUnSelected);
            this.gridHelper = gridUnSelectedHelper;

            this.gridSelectedHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetSelectedGridRow);
            this.gridSelectedHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadSelectedDataSource);

            this.gridUnSelectedHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetUnSelectedGridRow);
            this.gridUnSelectedHelper.GetRowCountHandle = new GetRowCountDelegateNew(this.GetUnSelectedRowCount);
            this.gridUnSelectedHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadUnSelectedDataSource);

            control = this.FindControl("cmdSave");
            if (control != null)
            {
                ((System.Web.UI.HtmlControls.HtmlInputButton)control).Attributes.Add("OnClick", "try{window.parent.returnValue=$('#txtSelected').val();window.parent.close();return false ;}catch(e){}");
            }
            control = this.FindControl("cmdCancel");
            if (control != null)
            {
                ((System.Web.UI.HtmlControls.HtmlInputButton)control).Attributes.Add("OnClick", "window.parent.close();return false ;");
            }

            //control = this.FindControl("cmdInit");
            //if (control != null)
            //{
            //    ((System.Web.UI.HtmlControls.HtmlInputButton)control).ServerClick += new System.EventHandler(this.cmdInit_ServerClick);
            //}

            if (!this.IsPostBack)
            {
                this.InitWebGrid();

                this.gridUnSelectedHelper.RequestData();
                this.gridSelectedHelper.RequestData();
            }

            SelectableTextBox();

        }

        #endregion

        #region WebGrid
        protected virtual void InitWebGrid()
        {
            base.InitWebGrid2();
            this.gridSelectedHelper.AddColumn("Selector_SelectedCode", "已选择的项目", null);
            this.gridSelectedHelper.AddColumn("Selector_SelectedDesc", "描述", null);
            this.gridSelectedHelper.AddDefaultColumn(true, false);
            this.gridSelectedHelper.ApplyLanguage(this.languageComponent1);
            base.InitWebGrid();
            this.gridUnSelectedHelper.AddColumn("Selector_UnselectedCode", "未选择的项目", null);
            this.gridUnSelectedHelper.AddColumn("Selector_UnSelectedDesc", "描述", null);
            this.gridUnSelectedHelper.AddDefaultColumn(true, false);
            this.gridUnSelectedHelper.ApplyLanguage(this.languageComponent1);

            //this.gridSelectedHelper.Grid.DisplayLayout.ClientSideEvents.CellClickHandler = "RowSelect";
            //this.gridUnSelectedHelper.Grid.DisplayLayout.ClientSideEvents.CellClickHandler = "RowSelect";
        }

        protected virtual DataRow GetSelectedGridRow(object obj)
        {
            return null;
        }

        protected virtual DataRow GetUnSelectedGridRow(object obj)
        {
            return null;
        }

        protected virtual object[] LoadSelectedDataSource(int inclusive, int exclusive)
        {
            return null;
        }

        protected virtual object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
            return null;
        }

        protected virtual int GetUnSelectedRowCount()
        {
            return 0;
        }




        #endregion

        #region Misc
        protected void cmdUnSelect_Click(object sender, System.EventArgs e)
        {
            ArrayList rows = this.gridSelectedHelper.GetCheckedRows();
            foreach (GridRecord row in rows)
            {
                DtSourceSelected.Rows.Remove(DtSourceSelected.Rows.Find(row.Items.FindItemByKey("GUID").Value));
            }
            this.gridSelectedHelper.Grid.DataSource = DtSourceSelected;
            this.gridSelectedHelper.Grid.DataBind();
            this.RequestData();

        }

        protected void cmdSelect_Click(object sender, System.EventArgs e)
        {
            ArrayList rows = this.gridUnSelectedHelper.GetCheckedRows();
            this.AddNewRow(rows);
            this.RequestData();
        }

        protected virtual void AddNewRow(ArrayList rows)
        {
            foreach (GridRecord row in rows)
            {
                DataRow newRow = DtSourceSelected.NewRow();
                newRow["GUID"] = row.Items.FindItemByKey("GUID").Value;
                newRow["Selector_SelectedCode"] = row.Items.FindItemByKey("Selector_UnselectedCode").Value;
                newRow["Selector_SelectedDesc"] = row.Items.FindItemByKey("Selector_UnSelectedDesc").Value;

                this.DtSourceSelected.Rows.Add(newRow);
            }
            this.gridSelectedHelper.Grid.DataSource = DtSourceSelected;
            this.gridSelectedHelper.Grid.DataBind();
        }


        protected virtual string[] GetSelectedCodes()
        {
            if (!IsPostBack)
            {
                if (Request.Params["Codes"] != null)
                {
                    if (Request.Params["Codes"].ToString().Trim() == string.Empty)
                    {
                        return new string[0];
                    }
                    else
                    {
                        return System.Web.HttpUtility.UrlDecode(Request.Params["Codes"].ToString()).Split(DATA_SPLITER.ToCharArray());
                    }
                }
            }

            Control control = this.FindControl("txtSelected");
            if (control == null)
            {
                return new string[0];
            }

            else
            {
                if (((System.Web.UI.HtmlControls.HtmlTextArea)control).Value.Trim().Length == 0)
                {
                    return new string[0];
                }
                return ((System.Web.UI.HtmlControls.HtmlTextArea)control).Value.Split(DATA_SPLITER.ToCharArray());

            }
        }

        protected virtual void SetSelectedCodes()
        {
            List<string> codes = new List<string>();
            string key = "";
            for (int i = 0; i < this.gridSelectedHelper.Grid.Rows.Count; i++)
            {
                key = this.gridSelectedHelper.Grid.Rows[i].Items[1].Text;
                if (!codes.Contains(key))
                {
                    codes.Add(key);
                }
            }
            Control control = this.FindControl("txtSelected");
            if (control == null)
            {
                return;
            }
            else
            {
                ((System.Web.UI.HtmlControls.HtmlTextArea)control).Value = string.Join(DATA_SPLITER, codes.ToArray());
            }
        }


        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);


        }

        protected virtual void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            RequestData();
        }

        private void RequestData()
        {
            SetSelectedCodes();

            this.gridUnSelectedHelper.RequestData();
            this.gridSelectedHelper.RequestData();
            
        }

        protected virtual WebDataGrid GetGridUnSelected()
        {
            Control control = this.FindControl("gridUnselected");
            if (control == null)
            {
                return null;
            }
            return (WebDataGrid)control;
        }

        protected virtual WebDataGrid GetGridSelected()
        {
            Control control = this.FindControl("gridSelected");
            if (control == null)
            {
                return null;
            }
            return (WebDataGrid)control;
        }


        protected virtual void cmdInit_ServerClick(object sender, System.EventArgs e)
        {
            this.gridSelectedHelper.RequestData();
            this.gridUnSelectedHelper.RequestData();
            SetSelectedCodes();
        }
        #endregion

        #region 注册客户端事件

        private void SelectableTextBox()
        {
            if (!this.ClientScript.IsStartupScriptRegistered("SelectableTextBox_Startup_js"))
            {
                string scriptString = string.Format("<script>var STB_Virtual_Path = \"{0}\";</script><script src='{0}SelectQuery/selectableTextBox.js'></script>", this.VirtualHostRoot);
                this.ClientScript.RegisterStartupScript(this.GetType(), "SelectableTextBox_Startup_js", scriptString, false);
            }

        }

        #endregion

    }
}
