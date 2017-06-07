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
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Performance;
using BenQGuru.eMES.Performance;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FShiftMP 的摘要说明。
    /// </summary>
    public partial class FExceptionCodeMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        private object[] ExceptionEventList;

        private PerformanceFacade _facade = null;
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

            this.cmdDelete1.ServerClick += new EventHandler(Delete1DomainObjects);
        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            //InitOnPostBack();
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.InitUI();
                InitButtonHelp();
                drpExceptionTypeQuery_Load();
                drpExceptionTypeEdit_Load();
                drpExceptionFlagQuery_Load();
                drpExceptionFlagEdit_Load();
            }
        }

        //private void InitOnPostBack()
        //{
        //    this.buttonHelper = new ButtonHelper(this);
        //    this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);

        //    this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
        //    this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
        //    this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

        //    this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
        //    this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
        //    this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);

        //}

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        public void InitButtonHelp()
        {
            //this.buttonHelper.AddButtonConfirm(cmdclick, languageComponent1.GetString("CloseConfirm"));
        }

        protected void drpExceptionTypeQuery_Load()
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(this.DataProvider);
            }
            DropDownListBuilder builder = new DropDownListBuilder(this.drpExceptionTypeQuery);

            builder.HandleGetObjectList = new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(_facade.QueryExceptionTypeFromSystem);
            builder.Build("ParameterDescription", "ParameterAlias");
            this.drpExceptionTypeQuery.Items.Insert(0, new ListItem("", ""));
            this.drpExceptionTypeQuery.SelectedIndex = 0;

        }

        protected void drpExceptionFlagQuery_Load()
        {
            this.drpExceptionFlagQuery.Items.Clear();
            this.drpExceptionFlagQuery.Items.Add(new ListItem("", ""));
            this.drpExceptionFlagQuery.Items.Add(new ListItem(ExceptionFlag.ExceptionFlag_Y, ExceptionFlag.ExceptionFlag_Y));
            this.drpExceptionFlagQuery.Items.Add(new ListItem(ExceptionFlag.ExceptionFlag_N, ExceptionFlag.ExceptionFlag_N));
        }

        protected void drpExceptionTypeEdit_Load()
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(this.DataProvider);
            }
            DropDownListBuilder builder = new DropDownListBuilder(this.drpExceptionTypeEdit);

            builder.HandleGetObjectList = new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(_facade.QueryExceptionTypeFromSystem);
            builder.Build("ParameterDescription", "ParameterAlias");
            this.drpExceptionTypeEdit.Items.Insert(0, new ListItem("", ""));
            this.drpExceptionTypeEdit.SelectedIndex = 0;
        }
        protected void drpExceptionFlagEdit_Load()
        {
            this.drpExceptionFlagEdit.Items.Clear();
            this.drpExceptionFlagEdit.Items.Add(new ListItem("", ""));
            this.drpExceptionFlagEdit.Items.Add(new ListItem(ExceptionFlag.ExceptionFlag_Y, ExceptionFlag.ExceptionFlag_Y));
            this.drpExceptionFlagEdit.Items.Add(new ListItem(ExceptionFlag.ExceptionFlag_N, ExceptionFlag.ExceptionFlag_N));
        }

        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("ExceptionCode", "异常事件代码", null);
            this.gridHelper.AddColumn("ExceptionName", "异常事件名称", null);
            this.gridHelper.AddColumn("ExceptionDESC", "异常事件描述", null);
            this.gridHelper.AddColumn("ExceptionType", "异常事件类型", null);
            this.gridHelper.AddColumn("ExceptionFlag", "非生产性损失", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);
            ;
            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    ((ExceptionCode)obj).Code.ToString(),
            //                    ((ExceptionCode)obj).Name.ToString(),
            //                    ((ExceptionCode)obj).Description.ToString(),
            //                    ((ExceptionCode)obj).Type.ToString(),//modified,Jarvis
            //                    //this.languageComponent1.GetString(((ExceptionCode)obj).Type.ToString()),
            //                    ((ExceptionCode)obj).Flag.ToString(),								
            //                    ((ExceptionCode)obj).MaintainUser.ToString(),	
            //                    FormatHelper.ToDateString(((ExceptionCode)obj).MaintainDate),
            //                    FormatHelper.ToTimeString(((ExceptionCode)obj).MaintainTime),
            //                    ""});
            DataRow row = this.DtSource.NewRow();
            row["ExceptionCode"] = ((ExceptionCode)obj).Code.ToString();
            row["ExceptionName"] = ((ExceptionCode)obj).Name.ToString();
            row["ExceptionDESC"] = ((ExceptionCode)obj).Description.ToString();
            row["ExceptionType"] = ((ExceptionCode)obj).Type.ToString();
            row["ExceptionFlag"] = ((ExceptionCode)obj).Flag.ToString();
            row["MaintainUser"] = ((ExceptionCode)obj).MaintainUser.ToString();
            row["MaintainDate"] = FormatHelper.ToDateString(((ExceptionCode)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((ExceptionCode)obj).MaintainTime);
            return row;

        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }
            return this._facade.QueryExceptionCode(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtExceptionCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtExceptionDESCQuery.Text)),
                this.drpExceptionTypeQuery.SelectedValue.ToUpper(),
                this.drpExceptionFlagQuery.SelectedValue.ToUpper(),
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }
            return this._facade.GetExceptionCodeCount(
                  FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtExceptionCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtExceptionDESCQuery.Text)),
                this.drpExceptionTypeQuery.SelectedValue.ToUpper(),
                this.drpExceptionFlagQuery.SelectedValue.ToUpper());
        }

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        #endregion

        #region Button
        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }

            ExceptionCode exceptionCode = (ExceptionCode)_facade.GetExceptionCode(FormatHelper.CleanString(this.txtExceptionCodeEdit.Text.ToUpper()));

            if (exceptionCode != null)
            {
                WebInfoPublish.Publish(this, "$The_Same_ExceptionCode_Is_Exist", this.languageComponent1);
                return;
            }
            this._facade.AddExceptionCode((ExceptionCode)domainObject);
        }

        //protected override void Delete1DomainObjects(ArrayList domainObjects)
        //{
        //    if (_facade == null)
        //    {
        //        _facade = new PerformanceFacade(base.DataProvider);
        //    }
        //    //this._facade.DeleteExceptionCode((ExceptionCode[])domainObjects.ToArray(typeof(ExceptionCode)));

        //    for (int i = 0; i < domainObjects.Count; i++)
        //    {
        //        ExceptionCode exceptionCode = (ExceptionCode)domainObjects[i];
        //        ExceptionEventList = _facade.QueryExceptionEvent(string.Empty, 0, string.Empty, string.Empty, exceptionCode.Code);
        //        if (ExceptionEventList!=null)
        //        {
        //            for (int j = 0; j < ExceptionEventList.Length; j++)
        //            {
        //                _facade.DeleteExceptionEvent((ExceptionEvent)ExceptionEventList[j]);
        //            }                   
        //        }
        //    }
        //}

        protected void Delete1DomainObjects(object sender, EventArgs e)
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }
            this.DataProvider.BeginTransaction();
            try
            {
                bool haveExceptionEvent = false;
                ArrayList array = this.gridHelper.GetCheckedRows();
                string exceptionCodeList = string.Empty;
                for (int i = 0; i < array.Count; i++)
                {
                    string exceptionCode = ((GridRecord)array[i]).Items.FindItemByKey("ExceptionCode").Value.ToString();
                    ExceptionCode exceptionCodeObject = (ExceptionCode)_facade.GetExceptionCode(exceptionCode);

                    object[] ExceptionEventList = _facade.QueryExceptionEvent(string.Empty, 0, string.Empty, string.Empty, exceptionCode);
                    if (ExceptionEventList != null)
                    {
                        haveExceptionEvent = true;
                        exceptionCodeList += exceptionCode + ",";
                        //for (int j = 0; j < ExceptionEventList.Length; j++)
                        //{
                        //    _facade.DeleteExceptionEvent((ExceptionEvent)ExceptionEventList[j]);
                        //}
                    }
                    else
                    {
                        if (exceptionCodeObject != null)
                        {
                            _facade.DeleteExceptionCode(exceptionCodeObject);
                        }
                    }
                }
                this.DataProvider.CommitTransaction();

                if (haveExceptionEvent)
                {
                    WebInfoPublish.PublishInfo(this, exceptionCodeList + "$Error_ExceptionCode_Cannot_Delete", this.languageComponent1);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Delete_Domain_Object", ex);
                this.DataProvider.RollbackTransaction();
            }


            this.RequestData();
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }
            this._facade.UpdateExceptionCode((ExceptionCode)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtExceptionCodeEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtExceptionCodeEdit.ReadOnly = true;
            }
        }
        #endregion

        #region Object <--> Page
        protected override object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }

            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            ExceptionCode exceptionCode = this._facade.CreateNewExceptionCode();

            exceptionCode.Code = FormatHelper.CleanString(this.txtExceptionCodeEdit.Text.ToUpper());
            exceptionCode.Name = FormatHelper.CleanString(this.txtExceptionNameEdit.Text.ToUpper());
            exceptionCode.Description = FormatHelper.CleanString(this.txtExceptionDESCEdit.Text.ToUpper());
            exceptionCode.Type = this.drpExceptionTypeEdit.SelectedValue.ToUpper();
            exceptionCode.Flag = this.drpExceptionFlagEdit.SelectedValue.ToUpper();
            exceptionCode.MaintainUser = this.GetUserCode();
            exceptionCode.MaintainDate = dbDateTime.DBDate;
            exceptionCode.MaintainTime = dbDateTime.DBTime;

            return exceptionCode;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }
            object obj = _facade.GetExceptionCode(row.Items.FindItemByKey("ExceptionCode").Text.ToString());

            if (obj != null)
            {
                return obj as ExceptionCode;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtExceptionCodeEdit.Text = string.Empty;
                this.txtExceptionDESCEdit.Text = string.Empty;
                this.txtExceptionNameEdit.Text = string.Empty;
                this.drpExceptionFlagEdit.SelectedIndex = 0;
                this.drpExceptionTypeEdit.SelectedIndex = 0;

                return;
            }

            this.txtExceptionCodeEdit.Text = ((ExceptionCode)obj).Code.ToString();
            this.txtExceptionDESCEdit.Text = ((ExceptionCode)obj).Description;
            this.txtExceptionNameEdit.Text = ((ExceptionCode)obj).Name.ToString();
            this.drpExceptionTypeEdit.SelectedValue = ((ExceptionCode)obj).Type.ToString();
            this.drpExceptionFlagEdit.SelectedValue = ((ExceptionCode)obj).Flag.ToString();
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(lblExceptionCodeEdit, txtExceptionCodeEdit, 40, true));
            manager.Add(new LengthCheck(lblExceptionDESCEdit, txtExceptionDESCEdit, 200, true));
            manager.Add(new LengthCheck(lblExceptionNameEdit, txtExceptionNameEdit, 40, true));
            manager.Add(new LengthCheck(lblExceptionTypeEdit, drpExceptionTypeEdit, 40, true));
            manager.Add(new LengthCheck(lblExceptionFlagEdit, drpExceptionFlagEdit, 40, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;
        }

        #endregion


        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{((ExceptionCode)obj).Code.ToString(),
                                ((ExceptionCode)obj).Name.ToString(),
								((ExceptionCode)obj).Description.ToString(),
                                this.languageComponent1.GetString(((ExceptionCode)obj).Type.ToString()),
								((ExceptionCode)obj).Flag.ToString(),								
                                ((ExceptionCode)obj).MaintainUser.ToString(),	
								FormatHelper.ToDateString(((ExceptionCode)obj).MaintainDate),
								FormatHelper.ToTimeString(((ExceptionCode)obj).MaintainTime)
                                };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	
									"ExceptionCode",
                                    "ExceptionName",
									"ExceptionDESC",
                                    "ExceptionType",
									"ExceptionFlag",
									"MaintainUser",
									"MaintainDate",
									"MaintainTime"};
        }
        #endregion


    }
}
