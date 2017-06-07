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
using System.Xml;

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common;

using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WarehouseWeb.ImportData
{
    public partial class FExcelDataImp : BaseMPageMinus
    {
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        protected System.Web.UI.WebControls.Label lblTitle;
        protected System.Web.UI.WebControls.Label lblBOMDownloadTitle;
        protected System.Web.UI.HtmlControls.HtmlInputButton cmdReturn;
        private System.ComponentModel.IContainer components;
        private BenQGuru.eMES.Material.MaterialFacade _Facade = null;

       // private GridHelperNew gridHelper = null;
        private ButtonHelper buttonHelper = null;
        protected System.Web.UI.WebControls.TextBox ErrorLog;
        private string ImportXMLPath = string.Empty;
        //private string InputType = string.Empty;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            PostBackTrigger trigger = new PostBackTrigger();
            trigger.ControlID = this.cmdEnter.ID;
            (this.impForm.FindControl("up1") as UpdatePanel).Triggers.Add(trigger);
            InitHander();
            this.cmdView.Attributes.Add("onclick", "return Check();");
            InputType = Request.QueryString["itype"].ToString();
            if (!IsPostBack)
            {
                this.ImportXMLPath = this.Request.MapPath("") + @"\ImportBasicData.xml";
                if (this.languageComponent1.Language.ToString() == "CHT")
                {
                    this.ImportXMLPath = this.Request.MapPath("") + @"\ImportBasicData" + "CHT.xml";
                }
                else if (this.languageComponent1.Language.ToString() == "ENU")
                {
                    this.ImportXMLPath = this.Request.MapPath("") + @"\ImportBasicData" + "ENU.xml";
                }
                this.XmlHelper = new BenQGuru.eMES.Web.WarehouseWeb.ImportData.ImportXMLHelper(ImportXMLPath);
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                // 初始化界面UI

                this.InitUI();
                this.InitWebGrid();
            }
        }

        public ImportData.ImportXMLHelper XmlHelper
        {
            get
            {
                if (this.ViewState["XmlHelper"] != null)
                {
                    return this.ViewState["XmlHelper"] as ImportData.ImportXMLHelper;
                }
                else
                {
                    return new BenQGuru.eMES.Web.WarehouseWeb.ImportData.ImportXMLHelper(ImportXMLPath);
                }
            }
            set
            {
                this.ViewState["XmlHelper"] = value;
            }
        }

        public string InputType
        {
            get
            {
                if (this.ViewState["InputType"] != null)
                {
                    return this.ViewState["InputType"].ToString();
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["InputType"] = value;
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
                return string.Empty;
            }
            set
            {
                this.ViewState["UploadedFileName"] = value;
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
            //this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            // 
            // languageComponent1
            // 
            //this.languageComponent1.Language = "CHS";
            //this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
            //this.languageComponent1.RuntimePage = null;
            //this.languageComponent1.RuntimeUserControl = null;
            //this.languageComponent1.UserControlName = "";

        }
        #endregion

        private void InitHander()
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);
            this.buttonHelper = new ButtonHelper(this);
            //this.gridHelper.Grid.DisplayLayout.AllowUpdateDefault = AllowUpdate.No;
        }

        /// <summary>
        /// 初始化Grid的栏位
        /// </summary>
        protected override void InitWebGrid()
        {
            
            this.XmlHelper.SelectedImportType = InputType;
            string fileType = InputType;
            if (this.languageComponent1.Language.ToString() == "CHT")
            {
                fileType = InputType + "CHT";
            }
            else if (this.languageComponent1.Language.ToString() == "ENU")
            {
                fileType = InputType + "ENU";
            }
            aFileDownLoad.HRef = string.Format(@"{0}download\{1}.xls", this.VirtualHostRoot, fileType);

            this.gridHelper.Grid.Visible = true;
            this.gridHelper.Grid.Columns.Clear();
            this.gridHelper.Grid.Rows.Clear();

            this.gridHelper.AddDefaultColumn(true, false);
            ArrayList htable = this.XmlHelper.GetGridBuilder(this.languageComponent1);
           
            if (htable.Count > 0)
            {
           
                foreach (DictionaryEntry de in htable)
                {
                    this.gridHelper.AddColumn(de.Key.ToString(), de.Value.ToString(), null);
                    
                }
                this.gridHelper.AddColumn("ImportResult", "导入结果", 150);
                base.InitWebGrid();
            }
        }

        /// <summary>
        /// 根据xml加载dropDownlist
        /// </summary>
        //private void BuildDataTypeDDL()
        //{
        //    this.InputTypeDDL.Items.Clear();
        //    this.InputTypeDDL.Items.Add(new ListItem("", ""));

        //    //			ArrayList htable = this.XmlHelper.ImportType;
        //    ArrayList htable = this.XmlHelper.GetImportType(this.languageComponent1);
        //    if (htable.Count > 0)
        //    {
        //        foreach (DictionaryEntry de in htable)
        //        {
        //            this.InputTypeDDL.Items.Add(
        //                new ListItem(de.Value.ToString(), de.Key.ToString()));
        //        }
        //    }
        //}


        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            string fileName = FileLoadProcess.UploadFile2ServerUploadFolder(this.Page, this.DownLoadPathBom, null);
            if (fileName == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_UploadFileIsEmpty");
            }
            string fileName2 = fileName.ToUpper();
            if (fileName2.LastIndexOf(".XLS") == -1)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_UploadFileTypeError_XLS");
            }

            //this.ViewState.Add("UploadedFileName",fileName);
            this.UploadedFileName = fileName;

            this.RequestData();

            //chbSelectAll.Checked = true;
            //this.gridHelper.CheckAllRows(CheckStatus.Checked);
        }


        private void RequestData()
        {
            this.InitWebGrid();   
            //ImportExcel imXls = new ImportExcel(this.UploadedFileName, this.InputType, this.XmlHelper.GridBuilder, this.XmlHelper.NotAllowNullField);

            ImportExcel imXls = new ImportExcel(this.UploadedFileName, this.InputType, this.XmlHelper.GridBuilder, this.XmlHelper.GridBuilder);


            DataTable dt = imXls.XlaDataTable;

            dt.Columns.Add(this.gridHelper.CheckColumnKey);
            dt.Columns.Add("GUID");
            dt.Columns.Add("ImportResult");
            //foreach (DataRow row in dt.Rows)
            //{
            //    row[this.gridHelper.CheckColumnKey] =;
            //}
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{

            //    //object[] objs = new object[dt.Columns.Count + 1];
            //    //Array.Copy(dt.Rows[i].ItemArray, 0, objs, 1, dt.Columns.Count);
            //    //Infragistics.WebUI.UltraWebGrid.UltraGridRow row = new Infragistics.WebUI.UltraWebGrid.UltraGridRow(objs);
            //    //this.gridHelper.Grid.Rows.Add(row);

                
            //}
            dt.AcceptChanges();
            this.gridWebGrid.DataSource = dt;
            this.gridWebGrid.DataBind();
            
            this.lblCount.Text = dt.Rows.Count.ToString();
        }

        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
           if( InputType=="WORKPLAN")
               this.Response.Redirect(this.MakeRedirectUrl("../FMaterialWorkPlan.aspx"));
           else if (InputType == "TESTDATA")
           {
               this.Response.Redirect(this.MakeRedirectUrl("FExcelDataImp.aspx?itype=TESTDATA"));
           }
           else
               this.Response.Redirect(this.MakeRedirectUrl("../FMaterialNeed.aspx"));
        }

        protected void cmdAdd_ServerClick(object sender, EventArgs e)
        {
            
            this.XmlHelper.SelectedImportType = InputType;
            cmdQuery_ServerClick(this, e);
           
            ArrayList importArray = new ArrayList();
            foreach (GridRecord row in this.gridWebGrid.Rows)
            {
                importArray.Add(row);
            }
            if (importArray.Count == 0)
            {
                return;
            }

            DataTable dt = this.GetImportDT(importArray);

            


            ImportData.ImportDateEngine importEngine = new BenQGuru.eMES.Web.WarehouseWeb.ImportData.ImportDateEngine(
                base.DataProvider, languageComponent1,this.InputType, dt, this.GetUserCode(), importArray);

            importEngine.fromPage = this.Page;

            #region 验证数据有效性
            try
            {
                /*added by jessie lee, 2005/11/30,
                 * 操作时间过长时添加进度条*/

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

                importEngine.CheckDataValid();

                //this.Page.Response.Write("<script language=javascript>HideWait();</script>");
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

                bool isRollBack = false;
                importEngine.Import(isRollBack);

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

            if (importEngine.ErrorArray != null)
            {
                importEngine.ErrorArray = null;
            }

        }

        private DataTable GetImportDT(ArrayList array)
        {
            /* 生成表结构 */
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            dt.Columns.Clear();
            ArrayList htable = this.XmlHelper.GridBuilder;
            if (htable.Count > 0)
            {
                foreach (DictionaryEntry de in htable)
                {
                    string key = de.Key.ToString();
                    dt.Columns.Add(key);
                }
            }

            /* 填数据 */
            for (int i = 0; i < array.Count; i++)
            {
                GridRecord gridRow = array[i] as GridRecord;
                DataRow row = dt.NewRow();
                if (htable.Count > 0)
                {
                    foreach (DictionaryEntry de in htable)
                    {
                        string key = de.Key.ToString();
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
                dt.Rows.Add(row);
            }

            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="valueWord"></param>
        /// <returns></returns>
        private string GetECode(string key, string valueWord)
        {
            return XmlHelper.GetMatchKeyWord(valueWord, XmlHelper.MatchType(key));
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            WebDataGrid Grid2 = null;
            Grid2 = this.gridHelper.Grid;
            for (int i = Grid2.Rows.Count - 1; i >= 0; i--)
            {
                GridRecord row = Grid2.Rows[i];
                if (bool.Parse(row.Items[0].ToString())
                    && (row.Items.FindItemByKey("ImportResult").Value.ToString() == "导入成功"))
                {
                    Grid2.Rows.Remove(Grid2.Rows[i]);
                }
            }

            Grid2.Columns.Remove(Grid2.Columns[0]);

          
        }
    }
}
