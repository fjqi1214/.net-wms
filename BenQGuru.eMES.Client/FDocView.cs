using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Infragistics.Win.UltraWinGrid;
using BenQGuru.eMES.Domain.Equipment;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using UserControl;
using BenQGuru.eMES.Domain.Document;
using System.IO;
using System.Web;
using System.Net;

namespace BenQGuru.eMES.Client
{
    public partial class FDocView : BaseForm
    {

        #region  变量

        private UltraWinGridHelper _UltraWinGridHelper1 = null;
        private IDomainDataProvider _DomainDataProvider = ApplicationService.Current().DataProvider;
        private DataTable _DataTableLoadedPart = new DataTable();

        private DocumentFacade _documentFacade;
        SystemSettingFacade _facade = null;
        private string itemCode = string.Empty;
        private string opCode = string.Empty;

        #endregion

        #region 属性

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _DomainDataProvider;
            }
        }

        #endregion

        public FDocView()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);
        }

        public FDocView(string itemCode, string opCode)
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);
            this.itemCode = itemCode;
            this.opCode = opCode;
        }

        private void FDocView_Load(object sender, EventArgs e)
        {
            InitializeUltraGrid();
            InitializedrpTypeCode();
            BuildCheckStatus();
            BuildValidStatus();

            this.txtDocnameQuery.Focus();

            if (itemCode != "" && opCode != "")
            {
                this.txtMcodelistQuery.Value = itemCode;
                this.txtOplistQuery.Value = opCode;
                this.btnQuery_Click(null, null);
            }

            //this.InitPageLanguage();
        }


        #region 初始化Grid
        private void InitUltraGridUI(UltraGrid ultraGrid)
        {
            ultraGrid.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            ultraGrid.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGrid.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGrid.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGrid.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            ultraGrid.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            ultraGrid.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            ultraGrid.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            ultraGrid.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void InitializeUltraGrid()
        {
            InitUltraGridUI(this.ultraGridScrutiny);

            _DataTableLoadedPart.Columns.Add("Docserial", typeof(string));
            _DataTableLoadedPart.Columns.Add("Dirserial", typeof(int));
            _DataTableLoadedPart.Columns.Add("Docname", typeof(string));
            _DataTableLoadedPart.Columns.Add("Docnum", typeof(string));
            _DataTableLoadedPart.Columns.Add("Docver", typeof(string));
            _DataTableLoadedPart.Columns.Add("DirName", typeof(string));
            _DataTableLoadedPart.Columns.Add("Mcodelist", typeof(string));
            _DataTableLoadedPart.Columns.Add("Oplist", typeof(string));
            _DataTableLoadedPart.Columns.Add("Docchgnum", typeof(string));
            _DataTableLoadedPart.Columns.Add("Docchgfile", typeof(string));
            _DataTableLoadedPart.Columns.Add("Memo", typeof(string));
            _DataTableLoadedPart.Columns.Add("Keyword", typeof(string));
            _DataTableLoadedPart.Columns.Add("Doctype", typeof(string));
            _DataTableLoadedPart.Columns.Add("CHECKEDSTATUS", typeof(string));
            _DataTableLoadedPart.Columns.Add("VALIDATESTATUS", typeof(string));
            _DataTableLoadedPart.Columns.Add("Upfiledate", typeof(string));
            _DataTableLoadedPart.Columns.Add("Upuser", typeof(string));
            _DataTableLoadedPart.Columns.Add("Mdate", typeof(string));
            _DataTableLoadedPart.Columns.Add("Muser", typeof(string));
            _DataTableLoadedPart.Columns.Add("Mtime", typeof(string));
            _DataTableLoadedPart.Columns.Add("ServerFullName", typeof(string));


            this.ultraGridScrutiny.DataSource = this._DataTableLoadedPart;

            _DataTableLoadedPart.Clear();


            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Docname"].CellClickAction = CellClickAction.CellSelect;

            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Docserial"].Width = 16;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Docserial"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Docserial"].Hidden = true;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Dirserial"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Dirserial"].Hidden = true;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Mtime"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Mtime"].Hidden = true;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["ServerFullName"].CellActivation = Activation.ActivateOnly;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["ServerFullName"].Hidden = true;

            //ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Docname"].CellAppearance.BackColor = Color.Green;
            //ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Docname"].Header.Appearance.BackColor = Color.Green;
            
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["Docname"].CellAppearance.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
        }

        private void ultraGridScrutiny_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            _UltraWinGridHelper1 = new UltraWinGridHelper(this.ultraGridScrutiny);

            _UltraWinGridHelper1.AddReadOnlyColumn("Docserial", "文件序号");
            _UltraWinGridHelper1.AddReadOnlyColumn("Dirserial", "目录序号");
            _UltraWinGridHelper1.AddReadOnlyColumn("Docname", "文件名称");
            _UltraWinGridHelper1.AddReadOnlyColumn("Docnum", "文件编号");
            _UltraWinGridHelper1.AddReadOnlyColumn("Docver", "版本");
            _UltraWinGridHelper1.AddReadOnlyColumn("DirName", "文档目录");
            _UltraWinGridHelper1.AddReadOnlyColumn("Mcodelist", "产品代码");
            _UltraWinGridHelper1.AddReadOnlyColumn("Oplist", "工序");
            _UltraWinGridHelper1.AddReadOnlyColumn("Docchgnum", "更改单号");
            _UltraWinGridHelper1.AddReadOnlyColumn("Docchgfile", "更改文件");
            _UltraWinGridHelper1.AddReadOnlyColumn("Memo", "备注");
            _UltraWinGridHelper1.AddReadOnlyColumn("Keyword", "关键字");
            _UltraWinGridHelper1.AddReadOnlyColumn("Doctype", "文档类型");
            _UltraWinGridHelper1.AddReadOnlyColumn("CHECKEDSTATUS", "审核是否通过");
            _UltraWinGridHelper1.AddReadOnlyColumn("VALIDATESTATUS", "是否有效");
            _UltraWinGridHelper1.AddReadOnlyColumn("Upfiledate", "上传日期");
            _UltraWinGridHelper1.AddReadOnlyColumn("Upuser", "上传人");
            _UltraWinGridHelper1.AddReadOnlyColumn("Mdate", "最后编辑日期");
            _UltraWinGridHelper1.AddReadOnlyColumn("Muser", "最后编辑人");
            _UltraWinGridHelper1.AddReadOnlyColumn("Mtime", "最后编辑时间");
            _UltraWinGridHelper1.AddReadOnlyColumn("ServerFullName", "服务器文件名");

            //this.InitGridLanguage(ultraGridScrutiny);
        }

        #endregion

        #region 自定义事件

        private void MCodeSelector_MCodeSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.txtMcodelistQuery.Value = e.CustomObject;
        }

        private void OPCodeSelector_OPCodeSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.txtOplistQuery.Value = e.CustomObject;
        }


        #endregion

        #region 页面事件

        private void btnQuery_Click(object sender, EventArgs e)
        {

            DocumentFacade _documentFacade = new DocumentFacade(this.DataProvider);
            object[] objs = _documentFacade.QueryDocuments(this.txtDocnameQuery.Value,
                                                        this.txtDocnumQuery.Value,
                                                        this.txtMcodelistQuery.Value,
                                                        this.txtOplistQuery.Value,
                                                        this.txtKeywordQuery.Value,
                                                        this.txtMemoQuery.Value,
                                                        FormatString(this.drpDoctypeQuery.SelectedItemValue),
                                                        FormatString(this.drpValidStatusQuery.SelectedItemValue),
                                                        FormatString(this.drpCheckedStatusQuery.SelectedItemValue),
                                                        1,
                                                        int.MaxValue
                                                        );
            _DataTableLoadedPart.Clear();

            if (objs == null)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_No_Data_To_Display"));
                return;
            }

            for (int i = 0; i < objs.Length; i++)
            {
                DocForQuery doc = objs[i] as DocForQuery;
                _DataTableLoadedPart.Rows.Add(new object[] {
                                                            
                                                            doc.Docserial.ToString(),
                                                            doc.Dirserial.ToString(),
                                                            doc.Docname.ToString(),
								                            doc.Docnum.ToString(),
                                                            doc.Docver.ToString(),
                                                            doc.DirName.ToString(),
                                                            doc.Itemlist.ToString(),
                                                            doc.Oplist.ToString(),
                                                            doc.Docchgnum.ToString(),
                                                            doc.Docchgfile.ToString(),
                                                            doc.Memo.ToString(),
                                                            doc.Keyword.ToString(),
                                                            doc.Doctype.ToString(),
                                                            MutiLanguages.ParserString(doc.Checkedstatus),
                                                            MutiLanguages.ParserString(doc.Validstatus),
                                                            FormatHelper.ToDateString(doc.Upfiledate),
                                                            doc.GetDisplayText("Upuser"),
                                                            FormatHelper.ToDateString(doc.Mdate),
                                                            doc.GetDisplayText("MaintainUser"),
                                                            FormatHelper.ToTimeString(doc.Mtime),
                                                            doc.ServerFileName.ToString()
                                                             });
            }
        }

        private void btnGetMcodelist_Click(object sender, EventArgs e)
        {
            FItemCodeQuery fItemCodeQuery = new FItemCodeQuery();
            fItemCodeQuery.Owner = this;
            fItemCodeQuery.StartPosition = FormStartPosition.CenterScreen;
            fItemCodeQuery.ItemCodeSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(MCodeSelector_MCodeSelectedEvent);
            fItemCodeQuery.ShowDialog();
            fItemCodeQuery = null;

            this.txtMcodelistQuery.TextFocus(false, true);
        }

        private void btnGetOplistQuery_Click(object sender, EventArgs e)
        {
            FOPCodeQuery fOPCodeQuery = new FOPCodeQuery();
            fOPCodeQuery.Owner = this;
            fOPCodeQuery.StartPosition = FormStartPosition.CenterScreen;
            fOPCodeQuery.OPCodeSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(OPCodeSelector_OPCodeSelectedEvent);
            fOPCodeQuery.ShowDialog();
            fOPCodeQuery = null;

            this.txtOplistQuery.TextFocus(false, true);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ultraGridScrutiny_ClickCell(object sender, ClickCellEventArgs e)
        {
            try
            {
                if (e.Cell.Column.Key == "Docname")
                {
                    if (_documentFacade == null)
                    {
                        _documentFacade = new DocumentFacade(this.DataProvider);
                    }

                    bool right = _documentFacade.GetDocDirRight(int.Parse(e.Cell.Row.Cells[1].Value.ToString()), ApplicationService.Current().UserCode, "QUERY");
                    Doc doc = _documentFacade.GetDOC(int.Parse(e.Cell.Row.Cells[0].Value.ToString())) as Doc;

                    if (right)
                    {
                        if (doc.Checkedstatus == "Y" && doc.Validstatus == "Y")
                        {

                            if (_facade == null)
                            {
                                _facade = new SystemSettingFacade(this.DataProvider);
                            }
                            object parameter = _facade.GetParameter("PUBLISHDOCDIRPATH", "PUBLISHDOCDIRPATHGROUP");
                            if (parameter != null)
                            {
                                //发布服务器目录路径
                                string filePath = ((Domain.BaseSetting.Parameter)parameter).ParameterAlias;
                                if (filePath.LastIndexOf('/') == filePath.Length - 1)
                                {
                                    filePath = filePath.Substring(0, filePath.Length - 1);
                                }

                                #region//下载文件
                                if (e.Cell.Row.Cells[20].Value.ToString().Trim() != string.Empty)
                                {
                                    //服务器文件名
                                    string fileName = e.Cell.Row.Cells[20].Value.ToString();
                                    System.Diagnostics.Process.Start(filePath + "/" + fileName + "?tmp=" + (new System.Random()).Next(1000000).ToString());

                                }
                                else
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$Error_QueryFile_NotExist"));
                                }
                                #endregion
                            }
                            else
                            {
                                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$Error_PublishDocDirPath_NotExist"));
                            }

                        }
                        else
                        {
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$Error_FileIsNotCheckedOrValid"));
                        }
                    }
                    else
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$Error_HaveNoRightToDownload"));
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_QueryFile_Exception"));
            }

        }


        #endregion


        #region 自定义方法

        private string FormatString(object value)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                return FormatHelper.CleanString(value.ToString().Trim());
            }
        }



        private void InitializedrpTypeCode()
        {
            this.drpDoctypeQuery.Clear();
            this.drpDoctypeQuery.AddItem("", "");

            if (_facade == null) { _facade = new SystemSettingFacade(this.DataProvider); }
            object[] objs = _facade.GetAllDocType();
            if (objs != null)
            {
                foreach (Parameter obj in objs)
                {
                    this.drpDoctypeQuery.AddItem(obj.ParameterAlias, obj.ParameterAlias);
                }
            }
        }

        private void BuildValidStatus()
        {
            this.drpValidStatusQuery.Clear();
            this.drpValidStatusQuery.AddItem("", "");
            this.drpValidStatusQuery.AddItem(MutiLanguages.ParserString("Y"), "Y");
            this.drpValidStatusQuery.AddItem(MutiLanguages.ParserString("N"), "N");
            this.drpValidStatusQuery.SelectedIndex = 1;
        }

        private void BuildCheckStatus()
        {
            this.drpCheckedStatusQuery.Clear();
            this.drpCheckedStatusQuery.AddItem("", "");
            this.drpCheckedStatusQuery.AddItem(MutiLanguages.ParserString("Y"), "Y");
            this.drpCheckedStatusQuery.AddItem(MutiLanguages.ParserString("N"), "N");
            this.drpCheckedStatusQuery.SelectedIndex = 1;
        }


        #endregion




    }
}
