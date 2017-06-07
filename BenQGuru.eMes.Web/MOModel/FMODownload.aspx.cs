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
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Common;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using BenQGuru.eMES.Web.MOModel.ImportData;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.MOModel
{
    /// <summary>
    /// FMODownload 的摘要说明。
    /// </summary>
    public partial class FMODownload : BaseMPageMinus
    {
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        //private System.ComponentModel.IContainer components = null;

        private object[] mos = null;
        //private BenQGuru.eMES.Web.Helper.GridHelper gridHelper = null;
        private BenQGuru.eMES.MOModel.MOFacade facade;
        // = new FacadeFactory(base.DataProvider).CreateMOFacade();
        private BenQGuru.eMES.BaseSetting.SystemSettingFacade sysSettingFacade;// = new ItemFacadeFactory().CreateSystemSettingFacade()  ;
         protected UpdatePanel UpdatePanel1;
        //protected override void AddParsedSubObject(object obj)
        //{
        //    this.needUpdatePanel = false;
        //    if (obj is HtmlForm)
        //    {
        //        HtmlForm form1 = obj as HtmlForm;

        //        UpdatePanel up1 = new UpdatePanel();

        //        PostBackTrigger trigger = new PostBackTrigger();
        //        trigger.ControlID = this.cmdViewMO.ID;
        //        up1.Triggers.Add(trigger);

        //        up1.ID = "up1";
        //        foreach (Control formChildren in form1.Controls)
        //        {
        //            if (formChildren is HtmlTable)
        //            {
        //                up1.ContentTemplateContainer.Controls.Add(formChildren);
        //            }
        //        }
        //        form1.Controls.Add(up1);

        //    }
        //    base.AddParsedSubObject(obj);
        //}

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.InitOnPostBack();

            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.InitUI();
                string fileType = "ImportMO";
                if (this.languageComponent1.Language.ToString() == "CHT")
                {
                    fileType = fileType + "_CHT";
                }
                else if (this.languageComponent1.Language.ToString() == "ENU")
                {
                    fileType = fileType + "_ENU";
                }
                aFileDownLoad.HRef = string.Format(@"{0}download\{1}.xls", this.VirtualHostRoot, fileType);
                this.cmdEnter.Disabled = true;

                this.InitWebGrid();
            }

            
        }

        private void InitOnPostBack()
        {

            this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

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
            //this.components = new System.ComponentModel.Container();
            //this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            //// 
            //// languageComponent1
            //// 
            //this.languageComponent1.Language = "CHS";
            //this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            //this.languageComponent1.RuntimePage = null;
            //this.languageComponent1.RuntimeUserControl = null;
            //this.languageComponent1.UserControlName = "";

        }
        #endregion

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl("FMOMP.aspx"));
        }

        protected void cmdView_ServerClick(object sender, System.EventArgs e)
        {
            string fileName = FileLoadProcess.UploadFile2ServerUploadFolder(this.Page, this.DownLoadPathMO, null);
            if (fileName == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_UploadFileIsEmpty");
            }
            //add by crystal chu 2005/07/15
            if (!(fileName.ToLower().EndsWith(".xls") || fileName.ToLower().EndsWith(".xlsx")))
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_UploadFileTypeError_XLS");
            }

            this.ViewState.Add("UploadedFileName", fileName);
            this.RequestData();
            //this.gridHelper.CheckAllRows(CheckStatus.Checked); // modify by Simone

            //如果没有异常信息，将导入按钮置为可用
            for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(this.gridWebGrid.Rows[i].Items.FindItemByKey("importException").Text))
                {
                    this.cmdEnter.Disabled = false;
                }
            }
            if (this.gridWebGrid.Rows.Count == 0)
            {
                this.cmdEnter.Disabled = true;
            }
        }

        private void InitWebGrid()
        {
            this.gridHelper.AddColumn("Factory", "工厂", null);
            this.gridHelper.AddColumn("MOCode", "工单", null);
            this.gridHelper.AddColumn("ItemCode", "产品", null);
            this.gridHelper.AddColumn("MOType", "工单类型", null);
            this.gridHelper.AddColumn("MOPlanQTY", "计划数量", null);
            this.gridHelper.AddColumn("MOPlanStartDate", "预计开工日期", null);
            this.gridHelper.AddColumn("MOPlanEndDate", "预计完成日期", null);
            this.gridHelper.AddColumn("CustomerCode", "客户", null);
            this.gridHelper.AddColumn("CustomerOrderNO", "客户单号", null);
            this.gridHelper.AddColumn("MOMemo", "备注", null);
            this.gridHelper.AddColumn("BOMVersion", "工单BOM", null);
            this.gridHelper.AddColumn("IsExist", "是否已存在", null);
            this.gridHelper.AddColumn("importException", "异常信息", null);
            //add by crystal chu 2005/07/15
            this.gridHelper.Grid.Columns.FromKey("IsExist").Hidden = true;

            this.gridHelper.AddDefaultColumn(true, false);
            //this.gridWebGrid.Columns.FromKey("importException").Width = 200;
            //this.gridWebGrid.Columns.FromKey("importException").CellStyle.ForeColor = System.Drawing.Color.Red;

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            // TODO:这里要改
            ArrayList newMOs = new ArrayList();
            if (mos == null)
            {
                this.GetAllMO();
            }
            for (int i = 1; i <= mos.Length; i++)
            {
                if (i >= inclusive && i <= exclusive)
                {
                    newMOs.Add(mos[i - 1]);
                }
            }
            return newMOs.ToArray();
        }


        protected DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["Factory"] = ((MO)obj).Factory;
            row["MOCode"] = ((MO)obj).MOCode;
            row["ItemCode"] = ((MO)obj).ItemCode;
            row["MOType"] = ((MO)obj).MOType;
            row["MOPlanQTY"] = ((MO)obj).MOPlanQty.ToString();
            row["MOPlanStartDate"] = FormatHelper.ToDateString(((MO)obj).MOPlanStartDate);
            row["MOPlanEndDate"] = FormatHelper.ToDateString(((MO)obj).MOPlanEndDate);
            row["CustomerCode"] = ((MO)obj).CustomerCode.ToString();
            row["CustomerOrderNO"] = ((MO)obj).CustomerOrderNO.ToString();
            row["MOMemo"] = ((MO)obj).MOMemo.ToString();
            row["BOMVersion"] = ((MO)obj).BOMVersion.ToString();
            row["IsExist"] = IsMOExist(((MO)obj).MOCode);
            row["importException"] = FormartErrorMessage(((MO)obj).EAttribute6);
            return row;
        }

        private string FormartErrorMessage(string errorMesssage)
        {
            if (!string.IsNullOrEmpty(errorMesssage))
            {
                errorMesssage = errorMesssage.TrimEnd(';').TrimStart(';');
            }
            return errorMesssage;
        }


        private string IsMOExist(string moCode)
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateMOFacade(); }
            MO mo = (MO)this.facade.GetMO(moCode);
            if (mo != null)
            {
                return FormatHelper.DisplayBoolean(true, this.languageComponent1);
            }
            else
            {
                return FormatHelper.DisplayBoolean(false, this.languageComponent1);
            }
        }

        private object[] GetAllMO()
        {
            string fileName = string.Empty;

            fileName = this.ViewState["UploadedFileName"].ToString();

            string configFile = this.getParseConfigFileName();

            DataFileParser parser = new DataFileParser();
            parser.FormatName = "MO";
            parser.ConfigFile = configFile;
            parser.CheckValidHandle = new CheckValid(this.MODownloadCheck);
            mos = parser.Parse(fileName);



            foreach (MO mo in mos)
            {
                mo.MOCode = FormatHelper.PKCapitalFormat(mo.MOCode);
                mo.ItemCode = FormatHelper.PKCapitalFormat(mo.ItemCode);
                mo.MODownloadDate = FormatHelper.TODateInt(DateTime.Today);
                mo.MOUser = this.GetUserCode();
                mo.MORemark = " ";
                mo.MaintainUser = this.GetUserCode();
                mo.MaintainDate = FormatHelper.TODateInt(DateTime.Today);
                mo.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);

            }

            return mos;

        }

        private object[] moObjects;
        private object[] GetAllMOByExcle()
        {
            try
            {
                string fileName = string.Empty;
                ArrayList columnList = new ArrayList();

                //定义变量，用来取多语言
                string factory = string.Empty;// 工厂
                string mocode = string.Empty;// 工单
                string itemcode = string.Empty;// 产品代码
                string moType = string.Empty;// 工单类型
                string moPlanQty = string.Empty;// 工单计划数量
                string planSDate = string.Empty;// 计划开始日期
                string planEDate = string.Empty;// 计划完成日期
                string customerCode = string.Empty;// 客户代码
                string customerOrder = string.Empty;// 客户单号
                string moMemo = string.Empty;
                string moBOM = string.Empty;

                string xmlPath = this.Request.MapPath("") + @"\ImportMOData.xml";
                string dataType = "ImportMO";
            
                ArrayList lineValues = new ArrayList();
                lineValues = GetXMLHeader(xmlPath, dataType);

                for (int i = 0; i < lineValues.Count; i++)
                {
                    DictionaryEntry dictonary = new DictionaryEntry();
                    dictonary = (DictionaryEntry)lineValues[i];
                    if (dictonary.Key.ToString().Equals(MoDictionary.Factory))
                    {
                        factory = dictonary.Value.ToString().ToUpper();
                        columnList.Add(factory);
                    }
                    if (dictonary.Key.ToString().Equals(MoDictionary.Mocode))
                    {
                        mocode = dictonary.Value.ToString().ToUpper();
                        columnList.Add(mocode);
                    }
                    if (dictonary.Key.ToString().Equals(MoDictionary.Itemcode))
                    {
                        itemcode = dictonary.Value.ToString().ToUpper();
                        columnList.Add(itemcode);
                    }
                    if (dictonary.Key.ToString().Equals(MoDictionary.MoType))
                    {
                        moType = dictonary.Value.ToString().ToUpper();
                        columnList.Add(moType);
                    }
                    if (dictonary.Key.ToString().Equals(MoDictionary.MoPlanQty))
                    {
                        moPlanQty = dictonary.Value.ToString().ToUpper();
                        columnList.Add(moPlanQty);
                    }
                    if (dictonary.Key.ToString().Equals(MoDictionary.PlanSDate))
                    {
                        planSDate = dictonary.Value.ToString().ToUpper();
                        columnList.Add(planSDate);
                    }
                    if (dictonary.Key.ToString().Equals(MoDictionary.PlanEDate))
                    {
                        planEDate = dictonary.Value.ToString().ToUpper();
                        columnList.Add(planEDate);
                    }
                    if (dictonary.Key.ToString().Equals(MoDictionary.CustomerCode))
                    {
                        customerCode = dictonary.Value.ToString().ToUpper();
                        columnList.Add(customerCode);
                    }
                    if (dictonary.Key.ToString().Equals(MoDictionary.CustomerOrder))
                    {
                        customerOrder = dictonary.Value.ToString().ToUpper();
                        columnList.Add(customerOrder);
                    }
                    if (dictonary.Key.ToString().Equals(MoDictionary.MoMemo))
                    {
                        moMemo = dictonary.Value.ToString().ToUpper();
                        columnList.Add(moMemo);
                    }
                    if (dictonary.Key.ToString().Equals(MoDictionary.MoBOM))
                    {
                        moBOM = dictonary.Value.ToString().ToUpper();
                        columnList.Add(moBOM);
                    }
                }

                if (this.ViewState["UploadedFileName"] == null)
                {
                    BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_UploadFileIsEmpty");
                }
                fileName = this.ViewState["UploadedFileName"].ToString();

                //读取EXCEL格式文件
                System.Data.DataTable dt = new DataTable();
                try
                {
                    ImportXMLHelper xmlHelper=new ImportXMLHelper(xmlPath,dataType);
                    ArrayList gridBuilder = new ArrayList();
                    ArrayList notAllowNullField = new ArrayList();

                    gridBuilder = xmlHelper.GetGridBuilder(this.languageComponent1, dataType);
                    notAllowNullField = xmlHelper.GetNotAllowNullField(this.languageComponent1);

                    ImportExcel imXls = new ImportExcel(fileName, dataType, gridBuilder, notAllowNullField);
                     dt = imXls.XlaDataTable;
                  //  dt = GetExcelData(fileName);
                }
                catch (Exception)
                {
                    ExceptionManager.Raise(this.GetType().BaseType, "$GetExcelDataFiledCheckTemplate");
                }

                //对dt进行数据检查，去掉空行
                List<DataRow> removelist = new List<DataRow>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bool rowdataisnull = true;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i][j].ToString().Trim() != "")
                        {
                            rowdataisnull = false;
                        }
                    }
                    if (rowdataisnull)
                    {
                        removelist.Add(dt.Rows[i]);
                    }
                }
                for (int i = 0; i < removelist.Count; i++)
                {
                    dt.Rows.Remove(removelist[i]);
                }

                CheckTemplateRight(dt, columnList);

                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                MOFacade mofacade = new MOFacade(this.DataProvider);
                ArrayList objList = new ArrayList();
                if ((dt != null) && (dt.Rows.Count > 0))
                {
                    for (int h = 0; h < dt.Rows.Count; h++)
                    {
                        MO mo = new MO();
                        try
                        {
                            //填充DOMAIN,这里不做Check
                            mo.MOCode = FormatHelper.CleanString(dt.Rows[h][mocode].ToString().ToUpper());
                            mo.Factory = FormatHelper.CleanString(dt.Rows[h][factory].ToString().ToUpper());
                            mo.ItemCode = FormatHelper.CleanString(dt.Rows[h][itemcode].ToString().ToUpper());
                            mo.MOType = FormatHelper.CleanString(dt.Rows[h][moType].ToString().ToUpper());
                            try
                            {
                                mo.MOPlanQty = Convert.ToDecimal(FormatHelper.CleanString(dt.Rows[h][moPlanQty].ToString().ToUpper()));
                            }
                            catch (Exception)
                            {
                                ExceptionManager.Raise(this.GetType().BaseType, "$MOPlanQty_Must_Be_Decimal");
                                return null;
                            }

                            mo.MOPlanStartTime = 0;
                            mo.MOPlanEndTime = 0;
                            try
                            {
                                mo.MOPlanStartDate = FormatHelper.TODateInt(FormatHelper.CleanString(dt.Rows[h][planSDate].ToString().ToUpper()));
                                mo.MOPlanEndDate = FormatHelper.TODateInt(FormatHelper.CleanString(dt.Rows[h][planEDate].ToString().ToUpper()));
                            }
                            catch (Exception)
                            {
                                ExceptionManager.Raise(this.GetType().BaseType,"$Data_Formart_Must_Be_YYYY-MM-DD");
                                return null;
                            }

                            mo.CustomerCode = FormatHelper.CleanString(dt.Rows[h][customerCode].ToString().ToUpper());
                            mo.CustomerOrderNO = FormatHelper.CleanString(dt.Rows[h][customerOrder].ToString().ToUpper());
                            mo.MOMemo = FormatHelper.CleanString(dt.Rows[h][moMemo].ToString().ToUpper());
                            mo.BOMVersion = FormatHelper.CleanString(dt.Rows[h][moBOM].ToString().ToUpper());

                            mo.MaintainUser = this.GetUserCode();
                            mo.MaintainDate = dbDateTime.DBDate;
                            mo.MaintainTime = dbDateTime.DBTime;

                            mo.MOInputQty = 0;
                            mo.MORemark = " ";
                            mo.MOScrapQty = 0;
                            mo.MOActualQty = 0;
                            mo.MOActualStartDate = 0;
                            mo.MOActualEndDate = 0;
                            mo.MOStatus = MOManufactureStatus.MOSTATUS_INITIAL;
                            mo.IDMergeRule = 1;
                            mo.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                            mo.MOPCBAVersion = "1";
                            mo.MOVersion = "1.0";
                            mo.MODownloadDate = dbDateTime.DBDate;   //当前时间
                            mo.IsControlInput = "0"; ;
                            mo.IsBOMPass = "9";
                            mo.MOUser = this.GetUserCode();

                            objList.Add(mo);
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.Raise(this.GetType().BaseType, ex.Message);
                        }
                    }
                }
                moObjects = (MO[])objList.ToArray(typeof(MO));

                //检查当前数据是否符合导入要求(现在返回一个是否通过检查的标志)
                if (moObjects != null)
                {
                    foreach (MO mo in moObjects)
                    {
                        MODownloadCheck(mo);
                    }
                }
            }
            catch (Exception e)
            {
                ExceptionManager.Raise(this.GetType().BaseType, e.Message);
            }

            return moObjects;
        }

        private void RequestData()
        {
            if (mos == null)
            {
                // mos = GetAllMO() ;
                mos = GetAllMOByExcle();
            }

            //this.gridHelper.Grid.DisplayLayout.Pager.AllowPaging = true;
            //this.gridHelper.Grid.DisplayLayout.Pager.PageSize = int.MaxValue;
            this.gridHelper.GridBind(1, int.MaxValue);

            
        }        

        protected void cmdDownload_ServerClick(object sender, System.EventArgs e)
        {
            if (facade == null) { facade = new FacadeFactory(base.DataProvider).CreateMOFacade(); }
            if (this.ViewState["UploadedFileName"] == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_NOExamineFile");
            }

            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList moCodes = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    moCodes.Add(row.Items.FindItemByKey("MOCode").Value.ToString());
                }
                this.RequestData();

                this.DataProvider.BeginTransaction();
                try
                {
                    foreach (MO mo in this.mos)
                    {
                        foreach (string code in moCodes)
                        {
                            if (mo.MOCode == code)
                            {
                                /*CS187 工单增加“导入日期”栏位*/
                                //DateTime newDT = DateTime.Now;
                                //mo.MOImportDate = FormatHelper.TODateInt(newDT);
                                //mo.MOImportTime = FormatHelper.TOTimeInt(newDT);
                                DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                                mo.MOImportDate = dbTime.DBDate;
                                mo.MOImportTime = dbTime.DBTime;
                                mo.IDMergeRule = 1;
                                //mo.MORemark = string.Empty;
                                mo.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                                //清空提示信息
                                mo.EAttribute6 = string.Empty;
                                try
                                {
                                    facade.AddMO(mo);
                                    continue;
                                }
                                catch (Exception)
                                {
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, "$MoCode_Import_Filed", this.languageComponent1);
                                    this.cmdEnter.Disabled = true;
                                }
                        
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    ExceptionManager.Raise(this.GetType().BaseType,ex.Message);
                }

                this.DataProvider.CommitTransaction();
                this.gridWebGrid.Rows.Clear();
                WebInfoPublish.Publish(this, "$MoCode_Import_Success", this.languageComponent1);
                this.cmdEnter.Disabled = true;
            }
        }

        private string getParseConfigFileName()
        {
            string configFile = this.Server.MapPath(this.TemplateSourceDirectory);
            if (configFile[configFile.Length - 1] != '\\')
            {
                configFile += "\\";
            }
            configFile += "DataFileParser.xml";
            return configFile;
        }

        /// <summary>
        /// 判断导入的MO是否有效
        /// </summary>
        /// <param name="obj">导入的MO</param>
        /// <returns>如果有效,返回true,否则,返回false;返回false时,这个MO将不会被导入</returns>
        private bool MODownloadCheck(object obj)
        {
            MO mo = (MO)obj;
            //非空检查
            CheckMODomain(mo);

            if (sysSettingFacade == null) { sysSettingFacade = new BenQGuru.eMES.BaseSetting.SystemSettingFacade(base.DataProvider); }
            object[] parameters = sysSettingFacade.GetParametersByParameterGroup(MOType.GroupType);

            //add by crystal chu 2005/05/08
            if (parameters == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_System_Error");
            }

            if (mo.MOPlanStartDate > mo.MOPlanEndDate)
            {
               // ExceptionManager.Raise(this.GetType().BaseType, "$Error_MOPlanEndDateBigThanMOPlanStartDate");
                mo.EAttribute6 += ";"+this.languageComponent1.GetString("$Error_MOPlanEndDateBigThanMOPlanStartDate");
            }

            bool isParamExist = false;
            foreach (BenQGuru.eMES.Domain.BaseSetting.Parameter parameter in parameters)
            {
                if (mo.MOType == parameter.ParameterCode)
                {
                    isParamExist = true;
                    break;
                }
            }

            if (!isParamExist)
            {
                //ExceptionManager.Raise(this.GetType().BaseType, "$Error_MOType_NotExisted");
                //modified by kathy @20130812 修正提示信息：工单类型不存在
                mo.EAttribute6 += ";" + this.languageComponent1.GetString("$Error_MOType_NotExisted");
            }

            // 检查 ItemCode
            BenQGuru.eMES.MOModel.ItemFacade itemFacade = new ItemFacade(base.DataProvider);
            object item = itemFacade.GetItem(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(mo.ItemCode)), GlobalVariables.CurrentOrganizations.First().OrganizationID);
            if (item == null)
            {
                //ExceptionManager.Raise(this.GetType().BaseType, "$Error_ItemCode_NotExist");
                mo.EAttribute6 += ";" + this.languageComponent1.GetString("$Error_ItemCode_NotExist");
            }

            MOFacade moFacade = new FacadeFactory(base.DataProvider).CreateMOFacade();
            object isExistMO = moFacade.GetMO(FormatHelper.PKCapitalFormat(mo.MOCode));
            if (isExistMO != null)
            {
              //  ExceptionManager.Raise(this.GetType().BaseType, "$Error_MO_HasExisted", String.Format("[$MOCode='{0}']", mo.MOCode));
                mo.EAttribute6 += ";" + this.languageComponent1.GetString("$Error_MO_HasExisted");
            }

            return true;
        }


        #region 从Excle中读取数据
        public DataTable GetExcelData(string astrFileName)
        {
            string strSheetName = GetExcelWorkSheets(astrFileName)[0].ToString();
            return GetExcelData(astrFileName, strSheetName);
        }

        /// <summary> 
        /// 返回指定文件所包含的工作簿列表;如果有WorkSheet，就返回以工作簿名字命名的ArrayList，否则返回空 
        /// </summary> 
        /// <param name="strFilePath">要获取的Excel</param> 
        /// <returns>如果有WorkSheet，就返回以工作簿名字命名的ArrayList，否则返回空</returns> 
        public ArrayList GetExcelWorkSheets(string strFilePath)
        {
            ArrayList alTables = new ArrayList();
            OleDbConnection odn = new OleDbConnection(GetExcelConnection(strFilePath));
            odn.Open();
            DataTable dt = new DataTable();
            dt = odn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (dt == null)
            {
                throw new Exception("无法获取指定Excel的架构。");
            }
            foreach (DataRow dr in dt.Rows)
            {
                string tempName = dr["Table_Name"].ToString();
                int iDolarIndex = tempName.IndexOf('$');
                if (iDolarIndex > 0)
                {
                    tempName = tempName.Substring(0, iDolarIndex);
                }
                //修正了Excel2003中某些工作薄名称为汉字的表无法正确识别的BUG。 
                if (tempName[0] == '\'')
                {
                    if (tempName[tempName.Length - 1] == '\'')
                    {
                        tempName = tempName.Substring(1, tempName.Length - 2);
                    }
                    else
                    {
                        tempName = tempName.Substring(1, tempName.Length - 1);
                    }
                }
                if (!alTables.Contains(tempName))
                {
                    alTables.Add(tempName);
                }
            }
            odn.Close();
            if (alTables.Count == 0)
            {
                return null;
            }
            return alTables;
        }

        /// <summary> 
        /// 获取指定路径、指定工作簿名称的Excel数据 
        /// </summary> 
        /// <param name="FilePath">文件存储路径</param> 
        /// <param name="WorkSheetName">工作簿名称</param> 
        /// <returns>如果争取找到了数据会返回一个完整的Table，否则返回异常</returns> 
        public DataTable GetExcelData(string FilePath, string WorkSheetName)
        {
            DataTable dtExcel = new DataTable();
            OleDbConnection con = new OleDbConnection(GetExcelConnection(FilePath));
            OleDbDataAdapter adapter = new OleDbDataAdapter("Select * from [" + WorkSheetName + "$]", con);
            //读取 
            con.Open();
            adapter.FillSchema(dtExcel, SchemaType.Mapped);
            adapter.Fill(dtExcel);
            con.Close();
            dtExcel.TableName = WorkSheetName;
            //返回 
            return dtExcel;
        }

        /// <summary> 
        /// 获取链接字符串 
        /// </summary> 
        /// <param name="strFilePath"></param> 
        /// <returns></returns> 
        public string GetExcelConnection(string strFilePath)
        {
            if (!File.Exists(strFilePath))
            {
                throw new Exception("指定的Excel文件不存在！");
            }
            //return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFilePath + ";Extended properties=\"Excel 8.0;Imex=1;HDR=Yes;\"";
            return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFilePath + ";Extended properties=\"Excel 12.0;Imex=1;HDR=Yes;\"";
            //@"Provider=Microsoft.Jet.OLEDB.4.0;" + 
            //@"Data Source=" + strFilePath + ";" + 
            //@"Extended Properties=" + Convert.ToChar(34).ToString() + 
            //@"Excel 8.0;" + "Imex=1;HDR=Yes;" + Convert.ToChar(34).ToString(); 
        }
        #endregion

        #region 读取Excle文件辅助方法
        //读取xml文件获取列名
        private ArrayList GetXMLHeader(string xmlPath, string dateType)
        {
            ImportXMLHelper importHelper = new ImportXMLHelper(xmlPath, dateType);
            ArrayList lineValues = new ArrayList();
            lineValues = importHelper.GetColumnNameByLanguage(this.languageComponent1);
            if (lineValues != null)
            {
                return lineValues;
            }
            else
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$ImportMOData.xml_NOT_Exists");
                return null;
            }
        }

        //判断当前导入文件列名和当前语言类型匹配
        protected void CheckTemplateRight(DataTable dt, ArrayList columnList)
        {
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                try
                {
                    for (int i = 0; i < columnList.Count; i++)
                    {
                        if (!dt.Columns.Contains(columnList[i].ToString()))
                        {
                            ExceptionManager.Raise(this.GetType().BaseType, "$Error_Template_Is_Not_Right");
                        }
                    }
                }
                catch (Exception e)
                {
                    ExceptionManager.Raise(this.GetType().BaseType, e.Message.ToString());
                }
            }
        }
        #endregion

        //检查工单Domain的非空字段信息
        private void CheckMODomain(MO mo)
        {
            if (string.IsNullOrEmpty(mo.MOCode))
            {
                mo.EAttribute6 += ";" + this.languageComponent1.GetString("$Error_MOCODE_Is_Null");
            }
            if (string.IsNullOrEmpty(mo.ItemCode))
            {
                mo.EAttribute6 += ";" + this.languageComponent1.GetString("$Error_ItemCode_NotExist");
            }
            if (string.IsNullOrEmpty(mo.MOType))
            {
                mo.EAttribute6 += ";" + this.languageComponent1.GetString("$Error_MoType_NotExist");
            }
            if (string.IsNullOrEmpty(mo.MOPlanQty.ToString()))
            {
                mo.EAttribute6 += ";" + this.languageComponent1.GetString("$Error_MoPlanQty_NotExist");
            }
            if (string.IsNullOrEmpty(mo.MOPlanEndDate.ToString()))
            {
                mo.EAttribute6 += ";" + this.languageComponent1.GetString("$Error_MoPlanEndDate_NotExist");
            }
            if (string.IsNullOrEmpty(mo.MOPlanStartDate.ToString()))
            {
                mo.EAttribute6 += ";" + this.languageComponent1.GetString("$Error_MoPlanStartDate_NotExist");
            }
            if (string.IsNullOrEmpty(mo.BOMVersion.ToString()))
            {
                mo.EAttribute6 += ";" + this.languageComponent1.GetString("$Error_BOM_NotExist");
            }
        }

    }

    public class MoDictionary
    {
        public static string Factory
        {
            get { return "FACTORY"; }
        }

        public static string Mocode
        {
            get { return "MOCODE"; }
        }

        public static string Itemcode
        {
            get { return "ITEMCODE"; }
        }

        public static string MoType
        {
            get { return "MOTYPE"; }
        }

        public static string MoPlanQty
        {
            get { return "MOPLANQTY"; }
        }

        public static string PlanSDate
        {
            get { return "STARTDATE"; }
        }

        public static string PlanEDate
        {
            get { return "ENDDATE"; }
        }

        public static string CustomerCode
        {
            get { return "CUSTOMERCODE"; }
        }

        public static string CustomerOrder
        {
            get { return "CUSTOMERORDER"; }
        }

        public static string MoMemo
        {
            get { return "MOMEMO"; }
        }

        public static string MoBOM
        {
            get { return "MOBOM"; }
        }
    }
}
