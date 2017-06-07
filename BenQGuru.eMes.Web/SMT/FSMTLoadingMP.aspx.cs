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
using Infragistics.WebUI.UltraWebNavigator;
using Infragistics.WebUI.Shared;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Domain.SMT;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.MOModel;

using System.IO;
using System.Data.OleDb;
using BenQGuru.eMES.Web.SMT.ImportData;

namespace BenQGuru.eMES.Web.SMT
{

    /// <summary>
    /// FSMTLoadingMP 的摘要说明。
    /// </summary>
    public partial class FSMTLoadingMP : BaseMPageNew
    {

        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private BenQGuru.eMES.SMT.SMTFacade _facade;
        protected System.Web.UI.HtmlControls.HtmlInputCheckBox Checkbox1;
        ArrayList list = new ArrayList();
        protected UpdatePanel UpdatePanel1;

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
         this.gridWebGrid.InitializeRow += new Infragistics.Web.UI.GridControls.InitializeRowEventHandler(gridWebGrid_InitializeRow);
        }
        void gridWebGrid_InitializeRow(object sender, Infragistics.Web.UI.GridControls.RowEventArgs e)
        {
            e.Row.Items.FindItemByKey("Memo").CssClass = "ForeColorRed CellBackColor";
        }

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

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            if (!this.IsPostBack)
            {
                string fileType = "ImportSMTReel";
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

                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
            }

            string strMessage = this.languageComponent1.GetString("$Message_SMTLoading_DataOverwritten");
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            //this.gridHelper.AddColumn("ProductName", "规格", null);
            this.gridHelper.AddColumn("ProductCode", "产品代码", null);
            this.gridHelper.AddColumn("sscode", "产线代码", null);
            this.gridHelper.AddColumn("MachineCode", "机台编码", null);
            this.gridHelper.AddColumn("MachineItemCode", "站位编码", null);
            this.gridHelper.AddColumn("SourceMaterialCode", "主物料号", null);
            this.gridHelper.AddColumn("MaterialCode", "替代物料号", null);
            this.gridHelper.AddColumn("FeederSpecCode", "Feeder规格", null);
            this.gridHelper.AddColumn("Qty", "数量", null);
            this.gridHelper.AddColumn("Table", "Table", null);
            this.gridHelper.AddColumn("IsAllow", "是否合法", null);
            this.gridHelper.AddColumn("Memo", "备注", 200);

            //this.gridWebGrid.Columns.FromKey("Memo").Width = 200;
            //this.gridWebGrid.Columns.FromKey("Memo").CellStyle.ForeColor = System.Drawing.Color.Red;
            this.gridHelper.AddDefaultColumn(false, false);
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        #endregion

        #region Import
        private object[] items;
        protected void cmdView_ServerClick(object sender, System.EventArgs e)
        {
            string fileName = FileLoadProcess.UploadFile2ServerUploadFolder(this.Page, this.fileInit, null);
            if (fileName == null)
            {
                BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_UploadFileIsEmpty");
            }

            if (!(fileName.ToLower().EndsWith(".xls") || fileName.ToLower().EndsWith(".xlsx")))
            {
                BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_UploadFileTypeError_XLS");
            }

            this.ViewState.Add("UploadedFileName", fileName);
            this.cmdQuery_Click(null, null);
            /*
            if (this.gridWebGrid.Rows.Count > 0)
            {
                this.cmdImport.Disabled = false;
            }
            */
            this.cmdEnter.Disabled = false;
            if (this.items != null)
            {
                for (int i = 0; i < this.items.Length; i++)
                {
                    SMTFeederMaterial item = (SMTFeederMaterial)items[i];
                    if(item.EAttribute2=="false")
                   // if (item.EAttribute1.StartsWith(false.ToString()) == true)
                    {
                        this.cmdEnter.Disabled = true;
                        break;
                    }
                }
            }
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            ArrayList objs = new ArrayList();
            if (items == null)
            {
                this.GetAllItem();
            }
            for (int i = 1; i <= items.Length; i++)
            {
                if (i >= inclusive && i <= exclusive)
                {
                    objs.Add(items[i - 1]);
                }
            }

            return objs.ToArray();

        }
        protected override int GetRowCount()
        {
            if (items == null)
            {
                this.GetAllItem();
            }
            return items.Length;
        }

        protected override DataRow GetGridRow(object obj)
        {
            SMTFeederMaterial item = (SMTFeederMaterial)obj;
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{
            //                    //getItemNameByCode(item.ProductCode),
            //                    item.ProductCode,
            //                    item.StepSequenceCode,
            //                    item.MachineCode,
            //                    item.MachineStationCode,
            //                    item.SourceMaterialCode,
            //                    item.MaterialCode,
            //                    item.FeederSpecCode,
            //                    item.Qty,
            //                    item.TableGroup,
            //                    item.EAttribute2,
            //                    FormartErrorMessage(item.EAttribute1)
            //                    //(item.EAttribute1.IndexOf(":") > 0 ? item.EAttribute1.Substring(0, item.EAttribute1.IndexOf(":")) : item.EAttribute1),
            //                    //(item.EAttribute1.IndexOf(":") > 0 ? item.EAttribute1.Substring(item.EAttribute1.IndexOf(":") + 1) : string.Empty)
            //                });
            DataRow row = this.DtSource.NewRow();
            row["ProductCode"] = item.ProductCode;
            row["sscode"] = item.StepSequenceCode;
            row["MachineCode"] = item.MachineCode;
            row["MachineItemCode"] = item.MachineStationCode;
            row["SourceMaterialCode"] = item.SourceMaterialCode;
            row["MaterialCode"] = item.MaterialCode;
            row["FeederSpecCode"] = item.FeederSpecCode;
            row["Qty"] = item.Qty;
            row["Table"] = item.TableGroup;
            row["IsAllow"] = item.EAttribute2;
            row["Memo"] = FormartErrorMessage(item.EAttribute1);
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

        private string getItemNameByCode(string itemcode)
        {
            object[] objItems = null;
            string itemname = string.Empty;
            //try
            //{
            //    if (_facade == null) { _facade = new SMTFacade(base.DataProvider); }
            //    objItems = _facade.QueryItemByCode(itemcode.Trim().ToUpper());
            //}
            //catch
            //{ }
            if (objItems != null)
            {
                itemname = ((BenQGuru.eMES.Domain.MOModel.Item)objItems[0]).ItemName;
                if (itemname.IndexOf("--") > 0)
                {
                    return itemname.Substring(itemname.IndexOf("--") + 2, itemname.Length - itemname.IndexOf("--") - 2);
                }
                else
                {
                    return itemname;
                }
            }
            else
            {
                return " ";
            }
        }


        private object[] GetAllItem()
        {
            try
            {
                string fileName = string.Empty;

                if (this.ViewState["UploadedFileName"] == null)
                {
                    BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_UploadFileIsEmpty");
                }
                fileName = this.ViewState["UploadedFileName"].ToString();

                //原来的逻辑，读取CSV文件
                //string configFile = this.getParseConfigFileName();
                //BenQGuru.eMES.Web.Helper.DataFileParser parser = new BenQGuru.eMES.Web.Helper.DataFileParser();
                //parser.FormatName = "SMTFeederMaterial";
                //parser.ConfigFile = configFile;
                //items = parser.Parse(fileName); 

                ArrayList columnList = new ArrayList();

                //定义变量，用来取多语言
                string sscode = string.Empty;// 产线代码
                string itemcode = string.Empty;// 产品代码
                string machine = string.Empty;// 机台代码
                string machineStation = string.Empty;  //站位
                string sourceMaterial = string.Empty;// 首选料
                string materialCode = string.Empty;// 物料代码
                string feeder = string.Empty;// Feeder规格编码
                string qty = string.Empty;
                string table = string.Empty;

                //读取XML文件取出字段键值对
                string xmlPath = this.Request.MapPath("") + @"\ImportSMTReelData.xml";
                string dataType = "ImportSMTReel";
                ArrayList lineValues = new ArrayList();
                lineValues = GetXMLHeader(xmlPath, dataType);

                for (int i = 0; i < lineValues.Count; i++)
                {
                    DictionaryEntry dictonary = new DictionaryEntry();
                    dictonary = (DictionaryEntry)lineValues[i];

                    if (dictonary.Key.ToString().ToUpper().Equals("SSCODE"))
                    {
                        sscode = dictonary.Value.ToString().ToUpper();
                        columnList.Add(sscode);
                    }
                    if (dictonary.Key.ToString().ToUpper().Equals("ITEMCODE"))
                    {
                        itemcode = dictonary.Value.ToString().ToUpper();
                        columnList.Add(itemcode);
                    }
                    if (dictonary.Key.ToString().ToUpper().Equals("MACHINECODE"))
                    {
                        machine = dictonary.Value.ToString().ToUpper();
                        columnList.Add(machine);
                    }
                    if (dictonary.Key.ToString().ToUpper().Equals("MACHINESTATIONCODE"))
                    {
                        machineStation = dictonary.Value.ToString().ToUpper();
                        columnList.Add(machineStation);
                    }
                    if (dictonary.Key.ToString().ToUpper().Equals("SOURCEMATERIALCODE"))
                    {
                        sourceMaterial = dictonary.Value.ToString().ToUpper();
                        columnList.Add(sourceMaterial);
                    }
                    if (dictonary.Key.ToString().ToUpper().Equals("MATERIALCODE"))
                    {
                        materialCode = dictonary.Value.ToString().ToUpper();
                        columnList.Add(materialCode);
                    }
                    if (dictonary.Key.ToString().ToUpper().Equals("FEEDERSPECCODE"))
                    {
                        feeder = dictonary.Value.ToString().ToUpper();
                        columnList.Add(feeder);
                    }
                    if (dictonary.Key.ToString().ToUpper().Equals("QTY"))
                    {
                        qty = dictonary.Value.ToString().ToUpper();
                        columnList.Add(qty);
                    }
                    if (dictonary.Key.ToString().ToUpper().Equals("PCBTABLE"))
                    {
                        table = dictonary.Value.ToString().ToUpper();
                        columnList.Add(table);
                    }
                }

                ////读取EXCEL格式文件
                //System.Data.DataTable dt = new DataTable();
                //try
                //{
                //    dt = GetExcelData(fileName);
                //}
                //catch (Exception)
                //{
                //    ExceptionManager.Raise(this.GetType().BaseType, "$GetExcelDataFiledCheckTemplate");
                //}

                //读取EXCEL格式文件
                System.Data.DataTable dt = new DataTable();
                try
                {
                    ImportXMLHelper xmlHelper = new ImportXMLHelper(xmlPath, dataType);
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

                //检查当前读入字段格式是否与当前登录语言相同
                CheckTemplateRight(dt, columnList);

                if ((dt != null) && (dt.Rows.Count > 0))
                {
                    for (int h = 0; h < dt.Rows.Count; h++)
                    {
                        //modified by kathy @20130813 产品代码缺少输入也要提示
                        //if (dt.Rows[h][itemcode].ToString() != string.Empty)
                        //{
                            SMTFeederMaterial material = new SMTFeederMaterial();
                            material.ProductCode = dt.Rows[h][itemcode].ToString().ToUpper();
                            material.StepSequenceCode = dt.Rows[h][sscode].ToString().ToUpper();
                            material.MachineCode = dt.Rows[h][machine].ToString().ToUpper();
                            material.SourceMaterialCode = dt.Rows[h][sourceMaterial].ToString().ToUpper();
                            material.MachineStationCode = dt.Rows[h][machineStation].ToString().ToUpper();
                            material.MaterialCode = dt.Rows[h][materialCode].ToString().ToUpper();
                            material.FeederSpecCode = dt.Rows[h][feeder].ToString().ToUpper();
                            if (dt.Rows[h][qty].ToString() != string.Empty)
                            {
                                material.Qty = Decimal.Parse(dt.Rows[h][qty].ToString());
                            }
                            material.TableGroup = dt.Rows[h][table].ToString().ToUpper();
                            if (material.TableGroup == string.Empty)
                                material.TableGroup = "A";
                            list.Add(material);
                        //}
                    }
                }

                items = (SMTFeederMaterial[])list.ToArray(typeof(SMTFeederMaterial));
                CheckImportResult(items);


                ////原来的逻辑
                //if (items != null)
                //{
                //    for (int i = 0; i < items.Length; i++)
                //    {
                //        SMTFeederMaterial item = (SMTFeederMaterial)items[i];
                //        item.ProductCode = item.ProductCode.ToUpper();
                //        item.StepSequenceCode = item.StepSequenceCode.ToUpper();
                //        item.MachineCode = item.MachineCode.ToUpper();
                //        item.SourceMaterialCode = item.SourceMaterialCode.ToUpper();
                //        item.MachineStationCode = item.MachineStationCode.ToUpper();
                //        item.MaterialCode = item.MaterialCode.ToUpper();
                //        item.FeederSpecCode = item.FeederSpecCode.ToUpper();
                //        item.TableGroup = item.TableGroup.ToUpper();
                //        if (item.TableGroup == string.Empty)
                //            item.TableGroup = "A";
                //    }
                //    CheckImportResult(items);
                //}
            }
            catch (Exception e)
            {
                throw e;
            }

            return items;

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

        protected void cmdImport_ServerClick(object sender, System.EventArgs e)
        {
            if (_facade == null) { _facade = new SMTFacade(base.DataProvider); }
            if (items == null)
            {
                items = GetAllItem();
                if (items == null || items.Length == 0)
                    return;
            }
            string strMessage = "";
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            this.DataProvider.BeginTransaction();
            try
            {
                int iRet = this._facade.ImportSMTFeederMaterial(items, this.GetUserCode(), this.chkReplaceAll.Checked);
                this.DataProvider.CommitTransaction();
                if (iRet > 0)
                {
                    WebInfoPublish.Publish(this, "$SMTFeederMaterialImport_Success", this.languageComponent1);
                    this.cmdEnter.Disabled = true;
                }
                else
                {
                    WebInfoPublish.Publish(this, "$SMTFeederMaterialImport_Error", this.languageComponent1);
                }
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
            finally
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
            }
            
            items = null;
        }

        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            this.Response.Redirect("FSMTLoadMaterialMP.aspx");
        }

        private void CheckImportResult(object[] items)
        {
            BenQGuru.eMES.MOModel.ItemFacade itemFacade = new ItemFacade(base.DataProvider);
            BenQGuru.eMES.BaseSetting.BaseModelFacade modelFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
            if (_facade == null) { _facade = new SMTFacade(base.DataProvider); }
            ArrayList checkedSpec = new ArrayList();
            ArrayList checkedProduct = new ArrayList();
            ArrayList checkedSSCode = new ArrayList();
            for (int i = 0; i < items.Length; i++)
            {
                SMTFeederMaterial item = (SMTFeederMaterial)items[i];
                item.EAttribute2 ="true";
                item.EAttribute1 =string.Empty;
                if (item.ProductCode == string.Empty ||
                    item.StepSequenceCode == string.Empty ||
                    item.MachineCode == string.Empty ||
                    item.MachineStationCode == string.Empty ||
                    item.MaterialCode == string.Empty)
                {
                    item.EAttribute1 +=";"+ languageComponent1.GetString("$Error_Input_Empty");
                    item.EAttribute2 = "false";
                    //item.EAttribute1 = false.ToString() + ":" + languageComponent1.GetString("$Error_Input_Empty");
                   // continue;
                }
                if (checkedProduct.Contains(item.ProductCode) == false)
                {
                    object obj = itemFacade.GetItem(item.ProductCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                    if (obj == null)
                    {
                        item.EAttribute1 += ";" + languageComponent1.GetString("$Error_ItemCode_NotExist");
                        item.EAttribute2 = "false";
                        //item.EAttribute1 = false.ToString() + ":" + languageComponent1.GetString("$Error_ItemCode_NotExist");
                        //continue;
                    }
                    else
                    {
                        checkedProduct.Add(item.ProductCode);
                    }
                }
                if (checkedSSCode.Contains(item.StepSequenceCode) == false)
                {
                    object obj = modelFacade.GetStepSequence(item.StepSequenceCode);
                    if (obj == null)
                    {
                        item.EAttribute1 += ";" + languageComponent1.GetString("$Error_SSCode_NotExist");
                        item.EAttribute2 = "false";
                        //item.EAttribute1 = false.ToString() + ":" + languageComponent1.GetString("$Error_SSCode_NotExist");
                        //continue;
                    }
                    else
                    {
                        checkedSSCode.Add(item.StepSequenceCode);
                    }
                }
                if (item.FeederSpecCode == string.Empty || item.Qty == 0)
                {
                    if (item.SourceMaterialCode == string.Empty || i == 0)
                    {
                        item.EAttribute1 += ";" + languageComponent1.GetString("$MaterialCode_Not_Exist");
                        item.EAttribute2 = "false";
                        //item.EAttribute1 = false.ToString() + ":" + languageComponent1.GetString("$MaterialCode_Not_Exist");
                       // continue;
                    }
                    for (int n = 0; n < items.Length; n++)
                    {
                        SMTFeederMaterial item1 = (SMTFeederMaterial)items[n];
                        if (item1.MaterialCode == item.SourceMaterialCode)
                        {
                            if (item.FeederSpecCode == string.Empty)
                                item.FeederSpecCode = item1.FeederSpecCode;
                            if (item.Qty == 0)
                                item.Qty = item1.Qty;
                            break;
                        }
                    }
                    //if (item.FeederSpecCode == string.Empty)
                    //{
                        //item.EAttribute1 += ";" + languageComponent1.GetString("$FeederSpec_Not_Exist");
                        //item.EAttribute1 = false.ToString() + ":" + languageComponent1.GetString("$FeederSpec_Not_Exist");
                       // continue;
                   // }
                }
                if (checkedSpec.Contains(item.FeederSpecCode) == false)
                {
                    object obj = _facade.GetFeederSpec(item.FeederSpecCode);
                    if (obj == null)
                    {
                        item.EAttribute1 += ";" + languageComponent1.GetString("$FeederSpec_Not_Exist");
                        item.EAttribute2 = "false";
                        //item.EAttribute1 = false.ToString() + ":" + languageComponent1.GetString("$FeederSpec_Not_Exist");
                        //continue;
                    }
                    else
                    {
                        checkedSpec.Add(item.FeederSpecCode);
                    }
                }
            }
        }

        #endregion

        #region 从Excle中读取数据

        /// <summary> 
        /// 获取指定路径、指定工作簿名称的Excel数据:取第一个sheet的数据 
        /// </summary> 
        /// <param name="FilePath">文件存储路径</param> 
        /// <param name="WorkSheetName">工作簿名称</param> 
        /// <returns>如果争取找到了数据会返回一个完整的Table，否则返回异常</returns> 
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
                ExceptionManager.Raise(this.GetType().BaseType, "$ImportSMTReelData.xml_NOT_Exists");
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
    }
}
