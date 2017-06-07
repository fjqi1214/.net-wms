#region system
using System;
using System.IO;
using System.Web;
using System.Web.UI;

#endregion

#region project
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.SopPicture;
using BenQGuru.eMES.Common.DomainDataProvider;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

#endregion

namespace BenQGuru.eMES.Web.BaseSetting
{
    public partial class FItem2OpPics : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private EsopPicsFacade _esoppicsFacade;
        protected SystemSettingFacade _facade = null;
        protected UpdatePanel UpdatePanel1;
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
            //this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            //this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
            //this.gridWebGrid. += new Infragistics.WebUI.UltraWebGrid.InitializeRowEventHandler(gridWebGrid_InitializeRow);
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
            // 
            // excelExporter
            // 
            //this.excelExporter.FileExtension = "xls";
            //this.excelExporter.LanguageComponent = this.languageComponent1;
            //this.excelExporter.Page = this;
            //this.excelExporter.RowSplit = "\r\n";

        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.needVScroll = true;
            this.imgPic.ImageUrl = "";
            imgPicLink.Attributes["target"] = "_self";
            imgPicLink.Attributes["href"] = "#";
            //this.imgPic.Visible = false;
            // InitOnPostBack();
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                // 初始化界面UI
                //this.InitUI();
                // InitButtonHelp();
                // SetEditObject(null);
                //  this.InitWebGrid();
            }
        }

        //protected override void AddParsedSubObject(object obj)
        //{
        //    needUpdatePanel = false;
        //    base.AddParsedSubObject(obj);
        //}
        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion Init

        #region WebGrid

        protected override void InitWebGrid()
        {
            this.gridHelper.AddColumn("SERIAL", "图片序列号", null);
            this.gridHelper.AddColumn("PICFULLNAME", "图片名称", null);
            this.gridHelper.AddColumn("PICTITLE", "图片概述", null);
            this.gridHelper.AddColumn("PICMEMO", "图片备注", null);
            this.gridHelper.AddColumn("ITEMCODE", "产品代码", null);
            this.gridHelper.AddColumn("OPCODE", "工序代码", null);
            this.gridHelper.AddColumn("PICSEQ", "图片顺序", null);
            this.gridHelper.AddColumn("PICTYPE", "图片类型", null);
            this.gridHelper.AddColumn("Muser", "维护人", null);
            this.gridHelper.AddColumn("Mdate", "维护日期", null);
            this.gridHelper.AddColumn("Mtime", "维护时间", null);
            this.gridWebGrid.Columns.FromKey("SERIAL").Hidden = true;
            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            base.InitWebGrid();
        }

        protected override DataRow GetGridRow(object obj)
        {
            //Infragistics.WebUI.UltraWebGrid.UltraGridRow row = new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{
            //        "false",
            //        ((Esoppics)obj).Serial.ToString(),
            //        ((Esoppics)obj).Picfullname.ToString(),
            //        ((Esoppics)obj).Pictitle.ToString(),
            //        ((Esoppics)obj).Picmemo.ToString(),
            //        ((Esoppics)obj).Itemcode.ToString(),
            //        ((Esoppics)obj).Opcode.ToString(),
            //        ((Esoppics)obj).Picseq.ToString(),
            //         GetPicTypeResult(((Esoppics)obj).Pictype),
            //        ((Esoppics)obj).GetDisplayText("Muser"),
            //        FormatHelper.ToDateString(((Esoppics)obj).Mdate),
            //        FormatHelper.ToTimeString(((Esoppics)obj).Mtime),                   
            //        ""});
            DataRow row = this.DtSource.NewRow();
            row["SERIAL"] = ((Esoppics)obj).Serial.ToString();
            row["PICFULLNAME"] = ((Esoppics)obj).Picfullname.ToString();
            row["PICTITLE"] = ((Esoppics)obj).Pictitle.ToString();
            row["PICMEMO"] = ((Esoppics)obj).Picmemo.ToString();
            row["ITEMCODE"] = ((Esoppics)obj).Itemcode.ToString();
            row["OPCODE"] = ((Esoppics)obj).Opcode.ToString();
            row["PICSEQ"] = ((Esoppics)obj).Picseq.ToString();
            row["PICTYPE"] = GetPicTypeResult(((Esoppics)obj).Pictype);
            row["Muser"] = ((Esoppics)obj).GetDisplayText("Muser");
            row["Mdate"] = FormatHelper.ToDateString(((Esoppics)obj).Mdate);
            row["Mtime"] = FormatHelper.ToTimeString(((Esoppics)obj).Mtime);
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_esoppicsFacade == null) { _esoppicsFacade = new EsopPicsFacade(this.DataProvider); }
            return this._esoppicsFacade.QueryEsopPics(FormatHelper.CleanString(this.txtItemlistQuery.Text),
                                                      FormatHelper.CleanString(this.txtOplistQuery.Text),
                                                      inclusive,
                                                      exclusive
                                                        );
        }

        protected override int GetRowCount()
        {
            if (_esoppicsFacade == null) { _esoppicsFacade = new EsopPicsFacade(this.DataProvider); }
            return this._esoppicsFacade.QueryEsopPicsCount(FormatHelper.CleanString(this.txtItemlistQuery.Text),
                                                           FormatHelper.CleanString(this.txtOplistQuery.Text));
        }

        #endregion WebGrid

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (_esoppicsFacade == null)
            {
                _esoppicsFacade = new EsopPicsFacade(base.DataProvider);
            }
            this.DataProvider.BeginTransaction();

            object[] items = _esoppicsFacade.CheckEsoppicExist((Esoppics)domainObject);
            if (items != null && items.Length > 0)
            {
                WebInfoPublish.PublishInfo(this, "$Error_Primary_Key_Overlap", this.languageComponent1);
                return;
            }
            this._esoppicsFacade.AddEsoppics((Esoppics)domainObject);
            if (UpLoadFile("ADD", (Esoppics)domainObject))
            {
                this.DataProvider.CommitTransaction();
            }
            else
            {
                this.DataProvider.RollbackTransaction();
            }

        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_esoppicsFacade == null)
            {
                _esoppicsFacade = new EsopPicsFacade(base.DataProvider);
            }
            this._esoppicsFacade.DeleteEsoppics((Esoppics[])domainObjects.ToArray(typeof(Esoppics)));
            string filaPath = "";
            if (_facade == null)
            {
                _facade = new SystemSettingFacade(this.DataProvider);
            }
            //object parameter = _facade.GetParameter("PICUPLOADPATH", "ESOPPICDIRPATHGROUP");

            //if (parameter != null)
            //{
            //服务器目录路径
            filaPath = System.AppDomain.CurrentDomain.BaseDirectory
+ "ESFileUpload";//((Domain.BaseSetting.Parameter)parameter).ParameterAlias;
            //}

            DirectoryInfo dir = new DirectoryInfo(filaPath);

            foreach (FileInfo file in dir.GetFiles())
            {
                foreach (Esoppics pic in domainObjects)
                {
                    if (file.Name == pic.Picfullname + ".jpg")
                    {
                        file.Delete();
                    }
                }
            }

        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_esoppicsFacade == null)
            {
                _esoppicsFacade = new EsopPicsFacade(base.DataProvider);
            }
            this.DataProvider.BeginTransaction();
            Esoppics pic = (Esoppics)domainObject;
            object[] items = _esoppicsFacade.CheckEsoppicExist((Esoppics)domainObject);
            if (items != null && items.Length > 0)
            {
                if (items.Length == 1 && ((Esoppics)items[0]).Serial == this.HiddenPicSerial.Text)
                {
                    this._esoppicsFacade.UpdateEsoppics((Esoppics)domainObject);
                    if (UpLoadFile("EDIT", (Esoppics)domainObject))
                    {
                        this.DataProvider.CommitTransaction();
                        WebInfoPublish.PublishInfo(this, "$ReportDesign_Save_Success", this.languageComponent1);
                    }
                    else
                    {
                        this.DataProvider.RollbackTransaction();
                    }
                }
                else
                {
                    WebInfoPublish.PublishInfo(this, "$Error_Primary_Key_Overlap", this.languageComponent1);
                    return;
                }
            }
            else
            {
                this._esoppicsFacade.UpdateEsoppics((Esoppics)domainObject);
                if (UpLoadFile("EDIT", (Esoppics)domainObject))
                {
                    this.DataProvider.CommitTransaction();
                    WebInfoPublish.PublishInfo(this, "$ReportDesign_Save_Success", this.languageComponent1);
                }
                else
                {
                    this.DataProvider.RollbackTransaction();
                }
            }
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtItemCode.Readonly = false;
                this.txtOplist.Readonly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtItemCode.Readonly = true;
                this.txtOplist.Readonly = true;
            }
        }

        private bool UpLoadFile(string action, Esoppics pic)
        {
            if (action == "EDIT" && string.IsNullOrEmpty(this.fileUpload.Value))
            {
                return true;
            }
            try
            {
                if (_facade == null)
                {
                    _facade = new SystemSettingFacade(this.DataProvider);
                }
                //object parameter = _facade.GetParameter("PICUPLOADPATH", "ESOPPICDIRPATHGROUP");
                //if (parameter != null)
                //{
                //服务器目录路径
                string filePath = System.AppDomain.CurrentDomain.BaseDirectory
+ "ESFileUpload";//((Domain.BaseSetting.Parameter)parameter).ParameterAlias;

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                if (filePath.LastIndexOf('\\') == filePath.Length - 1)
                {
                    filePath = filePath.Substring(0, filePath.Length - 1);
                }

                if (pic != null)
                {
                    DirectoryInfo dir = new DirectoryInfo(filePath);

                    foreach (FileInfo file in dir.GetFiles())
                    {
                        if (file.Name == (pic.Picfullname + ".jpg"))
                        {
                            file.Delete();
                        }
                    }


                    /// '检查文件扩展名字 
                    HttpPostedFile postedFile = fileUpload.PostedFile;
                    string fileName;
                    fileName = System.IO.Path.GetFileName(postedFile.FileName);
                    if (fileName != "")
                    {
                        string currentPath = filePath + "\\" + pic.Picfullname + ".jpg";
                        postedFile.SaveAs(currentPath);
                    }


                    WebInfoPublish.PublishInfo(this, "$Success_UpLoadFile", this.languageComponent1);
                }
                return true;
                //}
                //else
                //{
                //    WebInfoPublish.PublishInfo(this, "$Error_PicDirPath_NotExist", this.languageComponent1);
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                WebInfoPublish.PublishInfo(this, "$Error_UpLoadFile_Exception", this.languageComponent1);
                return false;
            }
        }

        #endregion Button

        #region 解析图片类型
        private string GetPicTypeResult(string typeValue)
        {
            string result = string.Empty;
            if (typeValue.IndexOf(PicType.Operating_Instructions) >= 0)
            {
                result = languageComponent1.GetString(PicType.Operating_Instructions);
            }
            if (typeValue.IndexOf(PicType.Maintenance_instructions) >= 0)
            {
                result += ("," + languageComponent1.GetString(PicType.Maintenance_instructions));
                result = result.Trim(',');
            }
            return result;
        }
        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            if (_esoppicsFacade == null)
            {
                _esoppicsFacade = new EsopPicsFacade(base.DataProvider);
            }

            Esoppics pic = this._esoppicsFacade.CreateNewEsoppics();

            if (string.IsNullOrEmpty(this.HiddenPicSerial.Text))
            {
                pic.Serial = ((Esoppics)_esoppicsFacade.GetEsoppicNextSerial()[0]).Serial;
            }
            else
            {
                pic.Serial = this.HiddenPicSerial.Text;
            }

            string picType = string.Empty;
            if (chkOInstruction.Checked)
            {
                picType = PicType.Operating_Instructions;
            }
            if (chkMInstruction.Checked)
            {
                picType += ("," + PicType.Maintenance_instructions);
                picType = picType.Trim(',');
            }

            pic.Itemcode = FormatHelper.CleanString(this.txtItemCode.Text, 40);
            pic.Picseq = Convert.ToInt32(this.txtPICOrderEdit.Text.Trim());
            pic.Opcode = FormatHelper.CleanString(this.txtOplist.Text, 40);
            pic.Pictitle = FormatHelper.CleanString(this.txtPICTitleEdit.Text, 100);
            pic.Picmemo = FormatHelper.CleanString(this.txtPICMemoEdit.Text, 40);
            //pic.Pictype = FormatHelper.CleanString(this.DropdownlistPICType.SelectedValue);
            pic.Pictype = picType;
            pic.Picfullname = pic.Itemcode + "_" + pic.Opcode + "_" + pic.Serial;
            pic.Muser = this.GetUserCode();
            pic.Mdate = dbDateTime.DBDate;
            pic.Mtime = dbDateTime.DBTime;

            return pic;
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (_esoppicsFacade == null) { _esoppicsFacade = new BaseModelFacadeFactory(base.DataProvider).CreateEsopPicsFacade(); }
            object obj = this._esoppicsFacade.GetEsoppics(row.Items.FindItemByKey("SERIAL").Text.ToString());

            if (obj != null)
            {
                return (Esoppics)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (_esoppicsFacade == null) { _esoppicsFacade = new BaseModelFacadeFactory(base.DataProvider).CreateEsopPicsFacade(); }
            if (obj == null)
            {
                this.txtItemCode.Text = string.Empty;
                this.txtOplist.Text = string.Empty;
                this.txtPICMemoEdit.Text = string.Empty;
                this.txtPICOrderEdit.Text = string.Empty;
                this.txtPICTitleEdit.Text = string.Empty;
                this.HiddenPicFullName.Text = string.Empty;
                //this.imgPic.Visible = false;
                this.imgPic.ImageUrl = "";
                imgPicLink.Attributes["target"] = "_self";
                imgPicLink.Attributes["href"] = "#";
                this.HiddenPicSerial.Text = string.Empty;
                this.chkOInstruction.Checked = false;
                this.chkMInstruction.Checked = false;
                return;
            }

            this.txtItemCode.Text = ((Esoppics)obj).Itemcode.ToString();
            this.txtOplist.Text = ((Esoppics)obj).Opcode.ToString();
            //try
            //{
            //    this.DropdownlistPICType.SelectedValue = ((Esoppics)obj).Pictype.ToString();
            //}
            //catch
            //{
            //    this.DropdownlistPICType.SelectedIndex = 0;
            //}
            this.chkOInstruction.Checked = ((Esoppics)obj).Pictype.IndexOf(PicType.Operating_Instructions) >= 0;
            this.chkMInstruction.Checked = ((Esoppics)obj).Pictype.IndexOf(PicType.Maintenance_instructions) >= 0;
            this.txtPICOrderEdit.Text = ((Esoppics)obj).Picseq.ToString();
            this.txtPICMemoEdit.Text = ((Esoppics)obj).Picmemo.ToString();
            this.txtPICTitleEdit.Text = ((Esoppics)obj).Pictitle.ToString();
            this.HiddenPicFullName.Text = ((Esoppics)obj).Picfullname.ToString();
            this.HiddenPicSerial.Text = ((Esoppics)obj).Serial.ToString();

            try
            {
                if (_facade == null)
                {
                    _facade = new SystemSettingFacade(this.DataProvider);
                }
                //object parameter = _facade.GetParameter("PICUPLOADPATH", "ESOPPICDIRPATHGROUP");
                //if (parameter != null)
                //{
                //服务器目录路径
                string fileFullName = System.AppDomain.CurrentDomain.BaseDirectory
+ "ESFileUpload//" + (obj as Esoppics).Picfullname + ".jpg";
                if (File.Exists(fileFullName))
                {
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //this.imgPic.Visible = true;
                    this.imgPic.ImageUrl = this.VirtualHostRoot + "ESFileUpload//" + ((Esoppics)obj).Picfullname + ".jpg?i=" + DateTime.Now.Ticks;
                    imgPicLink.Attributes["target"] = "_blank";
                    imgPicLink.Attributes["href"] = this.imgPic.ImageUrl;
                    //this.imgPic.ImageUrl = "ShowImg.aspx?PICFULLNAME=" + file.Name;
                }
                else
                {
                    //this.imgPic.Visible = false;
                    this.imgPic.ImageUrl = "";
                    imgPicLink.Attributes["target"] = "_self";
                    imgPicLink.Attributes["href"] = "#";
                }

                //}
            }
            catch (Exception ex)
            {
                WebInfoPublish.PublishInfo(this, "$Error_PicDirPath_NotExist", this.languageComponent1);
                return;
            }

        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(lblItemCodeEdit, txtItemCode, 40, true));
            manager.Add(new LengthCheck(this.lblOPCodeEdit, txtOplist, 40, true));
            manager.Add(new NumberCheck(lblPICOrderEdit, txtPICOrderEdit, 0, 40, true));
            manager.Add(new LengthCheck(this.lblPICMemo, this.txtPICMemoEdit, 40, false));
            manager.Add(new LengthCheck(this.lblPICTitle, this.txtPICTitleEdit, 40, false));
            //manager.Add(new LengthCheck(this.lblPICTypeEdit, this.DropdownlistPICType, 40, true));
            if (!manager.Check())
            {
                WebInfoPublish.PublishInfo(this, manager.CheckMessage, languageComponent1);
                return false;
            }

            if (!chkOInstruction.Checked && !chkMInstruction.Checked)
            {
                WebInfoPublish.PublishInfo(this, "$Error_PicType_NotCheck", this.languageComponent1);
                return false;
            }

            HttpFileCollection files = HttpContext.Current.Request.Files;
            HttpPostedFile postedFile = fileUpload.PostedFile;

            if (postedFile != null && postedFile.FileName.Trim() != string.Empty)
            {
                //Check extension
                string extension = Path.GetExtension(postedFile.FileName);
                if (string.Compare(extension, ".jpg", true) != 0)
                {
                    WebInfoPublish.PublishInfo(this, "$Error_CS_File_Format_Error", this.languageComponent1);
                    return false;
                }
            }
            else
            {
                //Check unUpload
                if (string.IsNullOrEmpty(this.HiddenPicFullName.Text))
                {
                    WebInfoPublish.PublishInfo(this, "$Error_UploadFileIsEmpty", this.languageComponent1);
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{              
                    ((Esoppics)obj).Picfullname.ToString(),
                    ((Esoppics)obj).Pictitle.ToString(),
                    ((Esoppics)obj).Picmemo.ToString(),
                    ((Esoppics)obj).Itemcode.ToString(),
                    ((Esoppics)obj).Opcode.ToString(),
                    ((Esoppics)obj).Picseq.ToString(),
                      GetPicTypeResult(((Esoppics)obj).Pictype),
                      ((Esoppics)obj).GetDisplayText("Muser"),
                    FormatHelper.ToDateString(((Esoppics)obj).Mdate),
                    FormatHelper.ToTimeString(((Esoppics)obj).Mtime)                          
            };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	                                
                                 "PICFULLNAME",
                                 "PICTITLE",
                                 "PICMEMO",
                                 "ITEMCODE",
                                 "OPCODE",
                                 "PICSEQ",
                                 "PICTYPE",
                                 "MUSER",
                                 "MDATE", 
                                 "MTIME"
            };
        }

        #endregion

        #region DropDownList

        protected void drpesopPicTypeQuery_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    this.DropdownlistPICType.Items.Clear();
            //    DropdownlistPICType.Items.Add(new ListItem(this.languageComponent1.GetString(PicType.Operating_Instructions), PicType.Operating_Instructions));
            //    DropdownlistPICType.Items.Add(new ListItem(this.languageComponent1.GetString(PicType.Maintenance_instructions), PicType.Maintenance_instructions));
            //    DropdownlistPICType.SelectedIndex = 0;
            //}
        }

        #endregion DropDownList
    }
}
