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
using Infragistics.WebUI.UltraWebGrid;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FLabelTemplateMP 的摘要说明。
    /// </summary>
    public partial class FLabelTemplateMP : BaseMPage
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        private GridHelper gridHelper = null;
        private ButtonHelper buttonHelper = null;
        private LabelTemplateFacade _LabelTemplateFacade;
        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;

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
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            //this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
            // 
            // languageComponent1
            // 
            this.gridWebGrid.ClickCellButton += new ClickCellButtonEventHandler(gridWebGrid_ClickCellButton);
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
            // 
            // excelExporter
            // 
            this.excelExporter.FileExtension = "xls";
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.Page = this;
            this.excelExporter.RowSplit = "\r\n";

        }
        #endregion


        #region form events
        protected void Page_Load(object sender, System.EventArgs e)
        {
            InitHander();
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                // 初始化界面UI
                this.InitUI();
                this.InitButton();
                this.InitWebGrid();
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        private void gridWebGrid_DblClick(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
        {
            object obj = this.GetEditObject(e.Row);

            if (obj != null)
            {
                this.SetEditObject(obj);

                this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
            }
        }

        protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
        {
            if (_LabelTemplateFacade == null) { _LabelTemplateFacade = new LabelTemplateFacade(); }
            object label = this.GetEditObject();
            if (label != null)
            {
                this._LabelTemplateFacade.AddLabelTemplate((BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)label);
                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            }
        }

        protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
        {
            if (_LabelTemplateFacade == null)
            {
                _LabelTemplateFacade = new LabelTemplateFacade();
            }
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList labels = new ArrayList(array.Count);

                foreach (UltraGridRow row in array)
                {
                    object label = this.GetEditObject(row);
                    if (label != null)
                    {
                        labels.Add((BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)label);
                    }
                }

                this._LabelTemplateFacade.DeleteLabelTemplate((BenQGuru.eMES.Domain.BaseSetting.LabelTemplate[])labels.ToArray(typeof(BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)));
                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Delete);
            }
        }

        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {
            if (_LabelTemplateFacade == null)
            {
                _LabelTemplateFacade = new LabelTemplateFacade();
            }
            object label = this.GetEditObject();
            if (label != null)
            {
                this._LabelTemplateFacade.UpdateLabelTemplate(((BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)label));
                this.RequestData();

                this.buttonHelper.PageActionStatusHandle(PageActionType.Save);
            }
        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this.buttonHelper.PageActionStatusHandle(PageActionType.Cancel);
        }

        protected void chbSelectAll_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.chbSelectAll.Checked)
            {
                this.gridHelper.CheckAllRows(CheckStatus.Checked);
            }
            else
            {
                this.gridHelper.CheckAllRows(CheckStatus.Unchecked);
            }
        }


        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this.excelExporter.Export();
        }

        #endregion

        #region private method

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        private int GetRowCount()
        {
            if (_LabelTemplateFacade == null)
            {
                _LabelTemplateFacade = new LabelTemplateFacade();
            }
            return this._LabelTemplateFacade.QueryLabelTemplateCount((FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtLabelCodeQuery.Text))));
        }


        private void InitHander()
        {
            this.gridHelper = new GridHelper(this.gridWebGrid);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
            this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }



        private void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Save)
            {
                this.txtLabelCodeEdit.ReadOnly = false;
            }
            if (pageAction == PageActionType.Update)
            {
                this.txtLabelCodeEdit.ReadOnly = true;
            }
            if (pageAction == PageActionType.Cancel)
            {
                this.txtLabelCodeEdit.ReadOnly = false;
            }

        }


        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        public void InitButton()
        {
            this.buttonHelper.AddDeleteConfirm();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
        }

        private void InitWebGrid()
        {
            this.gridHelper.AddColumn("LabelCode", "模板代码", null);
            this.gridHelper.AddColumn("LabelDesc", "模板描述", null);
            this.gridHelper.AddColumn("LabelCount", "打印个数", null);
            this.gridHelper.AddColumn("LabelType", "标签类型", null);
            this.gridHelper.AddColumn("LabelPath", "模板文件路径", null);


            this.gridHelper.AddColumn("MaintainUser", "维护人员", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);

            this.gridHelper.AddDefaultColumn(true, true);
            //this.gridHelper.ApplyDefaultStyle();

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        private object GetEditObject()
        {
            if (this.ValidateInput())
            {
                if (_LabelTemplateFacade == null)
                {
                    _LabelTemplateFacade = new LabelTemplateFacade();
                }
                BenQGuru.eMES.Domain.BaseSetting.LabelTemplate label = this._LabelTemplateFacade.CreateNewLabelTemplate();

                label.LabelCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtLabelCodeEdit.Text, 40));
                label.LabelDesc = this.txtLabelDescEdit.Text;
                label.LabelPath = this.txtLabelPathEdit.Text;
                label.MaintainUser = this.GetUserCode();
                return label;
            }
            else
            {
                return null;
            }
        }

        private object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
        {
            if (_LabelTemplateFacade == null)
            {
                _LabelTemplateFacade = new LabelTemplateFacade();
            }
            object obj = this._LabelTemplateFacade.GetLabelTemplate((row.Cells[1].Text.ToString()));

            if (obj != null)
            {
                return (BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)obj;
            }

            return null;
        }

        private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
        {
            if (e.Cell.Column.Key == "Edit")
            {
                object obj = this.GetEditObject(e.Cell.Row);

                if (obj != null)
                {
                    this.SetEditObject(obj);

                    this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
                }
            }
        }

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(lblLabelCodeEdit, txtLabelCodeEdit, 40, true));
            manager.Add(new LengthCheck(lblLabelDescEdit, txtLabelDescEdit, 100, true));
            manager.Add(new LengthCheck(lblLabelPathEdit, txtLabelPathEdit, 100, true));


            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }
            return true;
        }

        private void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtLabelCodeEdit.Text = string.Empty;
                this.txtLabelDescEdit.Text = string.Empty;
                this.txtLabelPathEdit.Text = string.Empty;

                return;
            }

            this.txtLabelCodeEdit.Text = ((BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)obj).LabelCode;
            this.txtLabelDescEdit.Text = ((BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)obj).LabelDesc;
            this.txtLabelPathEdit.Text = ((BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)obj).LabelPath;
        }

        protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {

            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[]{"false",
								((BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)obj).LabelCode,
								((BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)obj).LabelDesc,
                                ((BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)obj).LabelCount.ToString(),
                                languageComponent1.GetString(((BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)obj).LabelType),
                                ((BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)obj).LabelPath,
								((BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)obj).MaintainUser.ToString(),
                                FormatHelper.ToDateString(((BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)obj).MaintainDate)});
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_LabelTemplateFacade == null)
            {
                _LabelTemplateFacade = new LabelTemplateFacade();
            }
            return this._LabelTemplateFacade.QueryLabelTemplate(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtLabelCodeQuery.Text)),
                inclusive, exclusive);
        }

        private string[] FormatExportRecord(object obj)
        {
            return new string[]{
                                ((BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)obj).LabelCode,
								((BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)obj).LabelDesc,
                                ((BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)obj).LabelCount.ToString(),
                                languageComponent1.GetString(((BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)obj).LabelType),
                                ((BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)obj).LabelPath,
								((BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)obj).MaintainUser.ToString(),
                                FormatHelper.ToDateString(((BenQGuru.eMES.Domain.BaseSetting.LabelTemplate)obj).MaintainDate)
							   };
        }

        private string[] GetColumnHeaderText()
        {
            return new string[] {	"LabelCode",
                                    "LabelDesc",
                                    "LabelCount",
                                    "LabelType",
                                    "LabelPath",
                                    "MaintainUser",
			                        "MaintainDate"
									};
        }

        private object[] LoadDataSource()
        {
            return this.LoadDataSource(1, int.MaxValue);
        }

        #endregion
    }
}
