using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.ImportData
{
    public partial class FExcelDataImp : BaseMPageMinus
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        private System.ComponentModel.IContainer components;
        private BenQGuru.eMES.Material.MaterialFacade _Facade = null;

        //private GridHelper gridHelper = null;
        private ButtonHelper buttonHelper = null;
        protected System.Web.UI.WebControls.TextBox ErrorLog;

        #region 自动生成的初始化

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
        }

        #endregion

        #region 属性

        public string ImportType
        {
            get
            {
                if (this.ViewState["ImportType"] != null)
                {
                    return this.ViewState["ImportType"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                this.ViewState["ImportType"] = value;
            }
        }

        public string ImportXMLPath
        {
            get
            {
                if (this.ViewState["ImportXMLPath"] != null)
                {
                    return this.ViewState["ImportXMLPath"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                this.ViewState["ImportXMLPath"] = value;
            }
        }

        public ImportXMLHelper XmlHelper
        {
            get
            {
                if (this.ViewState["XmlHelper"] != null)
                {
                    return this.ViewState["XmlHelper"] as ImportXMLHelper;
                }
                else
                {
                    return new ImportXMLHelper(ImportXMLPath);
                }
            }
            set
            {
                this.ViewState["XmlHelper"] = value;
            }
        }

        public string UploadedFileName
        {
            get
            {
                if (this.ViewState["UploadedFileName"] != null)
                {
                    return this.ViewState["UploadedFileName"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                this.ViewState["UploadedFileName"] = value;
            }
        }

        #endregion

        #region 事件

        protected void Page_Load(object sender, System.EventArgs e)
        {
            PostBackTrigger trigger = new PostBackTrigger();
            trigger.ControlID = this.cmdImport.ID;
            (this.FORM1.FindControl("up1") as UpdatePanel).Triggers.Add(trigger);
            InitHander();

            if (!IsPostBack)
            {
                this.ImportType = Request.QueryString["ImportType"].ToString();
                this.ImportXMLPath = this.Request.MapPath("") + @"\ImportBasicData.xml";
                this.XmlHelper = new ImportXMLHelper(ImportXMLPath);

                this.InitPageLanguage(this.languageComponent1, false);
                this.InitUI();
                this.InitWebGrid();
            }

            this.cmdView.Attributes.Add("onclick", "return Check();");
            aFileDownLoad.HRef = string.Format(@"{0}download\{1}.xls", this.VirtualHostRoot, this.ImportType);
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            string fileName = FileLoadProcess.UploadFile2ServerUploadFolder(this.Page, this.DownLoadPathBom, null);
            if (fileName == null || fileName.Trim().Length <= 0)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_UploadFileIsEmpty");
            }
            else if (fileName.ToUpper().LastIndexOf(".XLS") < 0)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_UploadFileTypeError_XLS");
            }

            this.UploadedFileName = fileName;
            this.RequestData();

            //chbSelectAll.Checked = true;
            //this.gridHelper.CheckAllRows(CheckStatus.Checked);
        }

        protected void cmdImport_ServerClick(object sender, EventArgs e)
        {
            this.XmlHelper.ImportType = this.ImportType;
            cmdQuery_ServerClick(this, e);

            ArrayList importRowArray = new ArrayList();
            foreach (GridRecord row in this.gridWebGrid.Rows)
            {
                importRowArray.Add(row);
            }
            if (importRowArray.Count == 0)
            {
                return;
            }

            DataTable dataTable = this.GetImportDataTable(importRowArray);

            ImportDateEngine importDateEngine = new ImportDateEngine(base.DataProvider, languageComponent1, this.ImportType, dataTable, this.GetUserCode(), importRowArray, gridHelper);

            #region 验证数据有效性

            try
            {
                //// 操作时间过长时添加进度条
                //this.Page.Response.Write("<div id='mydiv' >");
                //this.Page.Response.Write("_");
                //this.Page.Response.Write("</div>");
                //this.Page.Response.Write("<script>mydiv.innerText = '';</script>");
                //this.Page.Response.Write("<script language=javascript>;");
                //this.Page.Response.Write("var dots = 0;var dotmax = 10;function ShowWait()");
                //this.Page.Response.Write("{var output; output = '正在验证导入数据的有效性,请稍后';dots++;if(dots>=dotmax)dots=1;");
                //this.Page.Response.Write("for(var x = 0;x < dots;x++){output += '·';}mydiv.innerText =  output;}");
                //this.Page.Response.Write("function StartShowWait(){mydiv.style.visibility = 'visible'; ");
                //this.Page.Response.Write("window.setInterval('ShowWait()',1000);}");
                //this.Page.Response.Write("function HideWait(){mydiv.style.display = 'none';");
                //this.Page.Response.Write("window.clearInterval();}");
                //this.Page.Response.Write("StartShowWait();</script>");
                //this.Page.Response.Flush();

                importDateEngine.CheckDataValid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //finally
            //{
            //    this.Page.Response.Write("<script language=javascript>HideWait();</script>");
            //}

            #endregion

            #region 导入数据

            try
            {
                //this.Page.Response.Write("<div id='mydiv2' >");
                //this.Page.Response.Write("_");
                //this.Page.Response.Write("</div>");
                //this.Page.Response.Write("<script>mydiv2.innerText = '';</script>");
                //this.Page.Response.Write("<script language=javascript>;");
                //this.Page.Response.Write("var dots = 0;var dotmax = 10;function ShowWait()");
                //this.Page.Response.Write("{var output; output = '正在导入数据,请稍后';dots++;if(dots>=dotmax)dots=1;");
                //this.Page.Response.Write("for(var x = 0;x < dots;x++){output += '·';}mydiv2.innerText =  output;}");
                //this.Page.Response.Write("function StartShowWait(){mydiv2.style.visibility = 'visible'; ");
                //this.Page.Response.Write("window.setInterval('ShowWait()',1000);}");
                //this.Page.Response.Write("function HideWait(){mydiv2.style.display = 'none';");
                //this.Page.Response.Write("window.clearInterval();}");
                //this.Page.Response.Write("StartShowWait();</script>");
                //this.Page.Response.Flush();

                importDateEngine.Import(false);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //this.Page.Response.Write("<script language=javascript>HideWait();</script>");
            }

            #endregion

            if (importDateEngine.ErrorArray != null)
            {
                importDateEngine.ErrorArray = null;
            }
        }

        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            if (string.Compare(this.ImportType, "WorkPlan", true) == 0)
            {
                this.Response.Redirect(this.MakeRedirectUrl("../Warehouse/FMaterialWorkPlan.aspx"));
            }
            else if (string.Compare(this.ImportType, "MaterialNeed", true) == 0)
            {
                this.Response.Redirect(this.MakeRedirectUrl("../Warehouse/FMaterialNeed.aspx"));
            }
            else if (string.Compare(this.ImportType, "PlanWorkTime", true) == 0)
            {
                this.Response.Redirect(this.MakeRedirectUrl("../BaseSetting/FPlanWorkTimeMP.aspx"));
            }
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            WebDataGrid grid = null;
            grid = this.gridHelper.Grid;
            for (int i = grid.Rows.Count - 1; i >= 0; i--)
            {
                GridRecord row = grid.Rows[i];
                if (bool.Parse(row.Items.FindItemByKey(gridHelper.CheckColumnKey).ToString()) && string.Compare(row.Items.FindItemByKey("ImportResult").Value.ToString(), "导入成功", true) == 0)
                {
                    grid.Rows.Remove(grid.Rows[i]);
                }
            }

            grid.Columns.Remove(grid.Columns[0]);
        }

        #endregion

        #region 函数

        private void InitHander()
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid, DtSource);
            this.buttonHelper = new ButtonHelper(this);
            //this.gridHelper.Grid.DisplayLayout.AllowUpdateDefault = AllowUpdate.No;
        }

        private void RequestData()
        {
            this.InitWebGrid();
            ImportExcel importExcel = new ImportExcel(this.UploadedFileName, this.ImportType, this.XmlHelper.GetGridColumns(), null);

            DataTable dataTable = importExcel.XlaDataTable;

            dataTable.Columns.Add(this.gridHelper.CheckColumnKey);
            dataTable.Columns.Add("GUID");
            dataTable.Columns.Add("ImportResult");
            dataTable.AcceptChanges();
            this.gridWebGrid.DataSource = dataTable;
            this.gridWebGrid.DataBind();

            //for (int i = 0; i < dataTable.Rows.Count; i++)
            //{
            //    object[] objectArray = new object[dataTable.Columns.Count + 1];
            //    Array.Copy(dataTable.Rows[i].ItemArray, 0, objectArray, 1, dataTable.Columns.Count);

            //    UltraGridRow row = new UltraGridRow(objectArray);
            //    this.gridHelper.Grid.Rows.Add(row);
            //}

            this.lblCount.Text = dataTable.Rows.Count.ToString();
        }

        private DataTable GetImportDataTable(ArrayList array)
        {
            // 生成表结构 
            DataTable dataTable = new DataTable();
            dataTable.Rows.Clear();
            dataTable.Columns.Clear();

            Dictionary<string, string> columnDictionary = this.XmlHelper.GetGridColumns();
            if (columnDictionary != null && columnDictionary.Keys.Count > 0)
            {
                foreach (string key in columnDictionary.Keys)
                {
                    dataTable.Columns.Add(key);
                }
            }

            // 填数据
            for (int i = 0; i < array.Count; i++)
            {
                GridRecord gridRow = array[i] as GridRecord;
                DataRow row = dataTable.NewRow();
                if (columnDictionary != null && columnDictionary.Keys.Count > 0)
                {
                    foreach (string key in columnDictionary.Keys)
                    {
                        if (XmlHelper.NeedMatchCtoE(key))
                        {
                            string value1 = GetECode(key, gridRow.Items.FindItemByKey(key).Value.ToString());
                            if (gridRow.Items.FindItemByKey(key).Value.ToString().Trim().Length != 0
                                && value1.Length == 0)
                            {
                                ExceptionManager.Raise(this.GetType().BaseType, "$Error_UploadFile_ContentError");
                            }
                            else
                            {
                                row[key] = value1;
                            }
                        }
                        else
                        {
                            string a = gridRow.Items.FindItemByKey(key).Value.ToString();
                            row[key] = gridRow.Items.FindItemByKey(key).Value.ToString();
                        }
                    }
                }
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        private string GetECode(string key, string valueWord)
        {
            return XmlHelper.GetMatchKeyWord(valueWord, XmlHelper.MatchType(key));
        }

        #endregion

        #region WebGrid

        private void InitWebGrid()
        {
            this.XmlHelper.ImportType = ImportType;

            if (string.Compare(this.languageComponent1.Language.ToString(), "CHT", true) == 0)
            {
                this.ImportType = this.ImportType + "CHT";
            }
            else if (string.Compare(this.languageComponent1.Language.ToString(), "ENU", true) == 0)
            {
                this.ImportType = this.ImportType + "ENU";
            }

            this.gridHelper.Grid.Rows.Clear();
            this.gridHelper.Grid.Columns.Clear();
            Dictionary<string, string> columnDictionary = this.XmlHelper.GetGridColumns();
            if (columnDictionary != null && columnDictionary.Keys.Count > 0)
            {
                foreach (string key in columnDictionary.Keys)
                {
                    this.gridHelper.AddColumn(key, columnDictionary[key], null);
                }
            }
            this.gridHelper.AddColumn("ImportResult", "导入结果", 150);
            this.gridHelper.AddDefaultColumn(true, false);
            this.gridHelper.Grid.Visible = true;
        }

        #endregion
    }
}
