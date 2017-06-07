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
using System.Collections.Generic;

using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Performance;
using BenQGuru.eMES.Performance;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FRouteMP 的摘要说明。
    /// </summary>
    public partial class FLostManDetailMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
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
        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            InitOnPostBack();
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.InitWebGrid();
                this.GetPageValues();
                this.RequestData();
                this.DateQuery.ReadOnly = true;
                this.DateQuery.Enabled = false;

            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();

            this.txtLostManHourEdit.Text = string.Empty;
            this.txtExceptionCodeEdit.Text = string.Empty;
            this.txtRealExceptionCodeEdit.Text = string.Empty;
            this.txtDutyCodeEdit.Text = string.Empty;
            this.txtMemoEdit.Text = string.Empty;
            this.txtSeq.Text = string.Empty;
        }

        private void GetPageValues()
        {
            if (this.GetRequestParam("Date") != null)
            {
                this.DateQuery.Text = this.GetRequestParam("Date").ToString();
            }

            if (this.GetRequestParam("ShiftCode") != null)
            {
                this.txtShiftCodeQuery.Text = this.GetRequestParam("ShiftCode").ToString();
            }

            if (this.GetRequestParam("sscode") != null)
            {
                this.txtSSQuery.Text = this.GetRequestParam("sscode").ToString();
            }

            if (this.GetRequestParam("LostManHour") != null)
            {
                this.txtLostManHourQuery.Text = this.GetRequestParam("LostManHour").ToString();
            }

            if (this.GetRequestParam("ItemCode") != null)
            {
                ViewState["ItemCode"] = this.GetRequestParam("ItemCode").ToString();
                this.txtitemCode.Text = this.GetRequestParam("ItemCode").ToString();
            }
            else
            {
                ViewState["ItemCode"] = string.Empty;
            }

            if (this.GetRequestParam("ItemDesc") != null)
            {
                ViewState["ItemDesc"] = this.GetRequestParam("ItemDesc").ToString();
            }
            else
            {
                ViewState["ItemDesc"] = string.Empty;
            }

            this.txtItemCodeQuery.Text = ViewState["ItemCode"].ToString() + "-" + ViewState["ItemDesc"].ToString();
        }


        #endregion

        #region WebGrid
        protected void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("LostManHour", "损失工时", null);
            this.gridHelper.AddColumn("ExceptionBeginTime", "异常开始时间", null);
            this.gridHelper.AddColumn("ExceptionEndTime", "异常结束时间", null);
            this.gridHelper.AddColumn("ExceptionCode", "异常事件代码", null);
            this.gridHelper.AddColumn("ExceptionDESC", "异常事件描述", null);
            this.gridHelper.AddColumn("ExceptionMemo", "异常事件备注", null);
            this.gridHelper.AddColumn("Duty", "责任别", null);
            this.gridHelper.AddColumn("Memo", "备注", null);
            this.gridHelper.AddColumn("Seq", "", null);

            this.gridHelper.AddDefaultColumn(true, true);
            this.gridWebGrid.Columns.FromKey("Seq").Hidden = true;

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        private void InitOnPostBack()
        {
            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
            this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

            this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                 Convert.ToString(Math.Round(Convert.ToDouble(((LostManHourDetailWithMessage)obj).LostManHour)/3600,2)),
            //                FormatHelper.ToTimeString(((LostManHourDetailWithMessage)obj).BeginTime),
            //                FormatHelper.ToTimeString(((LostManHourDetailWithMessage)obj).EndTime),
            //                ((LostManHourDetailWithMessage)obj).ExceptionCode.ToString(),
            //                ((LostManHourDetailWithMessage)obj).ExceptionDescription.ToString(),
            //                ((LostManHourDetailWithMessage)obj).ExceptionMemo.ToString(),
            //                ((LostManHourDetailWithMessage)obj).DutyCode.ToString(),
            //                ((LostManHourDetailWithMessage)obj).Memo.ToString(),
            //                ((LostManHourDetailWithMessage)obj).Seq.ToString(),
            //                 ""
            //                });
            DataRow row = this.DtSource.NewRow();
            row["LostManHour"] = Convert.ToString(Math.Round(Convert.ToDouble(((LostManHourDetailWithMessage)obj).LostManHour) / 3600, 2));
            row["ExceptionBeginTime"] = FormatHelper.ToTimeString(((LostManHourDetailWithMessage)obj).BeginTime);
            row["ExceptionEndTime"] = FormatHelper.ToTimeString(((LostManHourDetailWithMessage)obj).EndTime);
            row["ExceptionCode"] = ((LostManHourDetailWithMessage)obj).ExceptionCode.ToString();
            row["ExceptionDESC"] = ((LostManHourDetailWithMessage)obj).ExceptionDescription.ToString();
            row["ExceptionMemo"] = ((LostManHourDetailWithMessage)obj).ExceptionMemo.ToString();
            row["Duty"] = ((LostManHourDetailWithMessage)obj).DutyCode.ToString();
            row["Memo"] = ((LostManHourDetailWithMessage)obj).Memo.ToString();
            row["Seq"] = ((LostManHourDetailWithMessage)obj).Seq.ToString();
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(this.DataProvider);
            }

            return this._facade.QueryLostManHourDetail(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSSQuery.Text)),
                FormatHelper.TODateInt(this.DateQuery.Text),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtShiftCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ViewState["ItemCode"].ToString().Trim())),
                0,
                inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(this.DataProvider);
            }
            return this._facade.GetLostManHourDetailcount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSSQuery.Text)),
                FormatHelper.TODateInt(this.DateQuery.Text),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtShiftCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ViewState["ItemCode"].ToString().Trim())),
                0
                );
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }

            ((LostManHourDetail)domainObject).Seq = _facade.GetLostManHourDetailSeq(FormatHelper.CleanString(this.txtSSQuery.Text.ToUpper()),
                                                                                    FormatHelper.TODateInt(this.DateQuery.Text),
                                                                                    FormatHelper.CleanString(this.txtShiftCodeQuery.Text.ToUpper()),
                                                                                    FormatHelper.CleanString(this.txtitemCode.Text.ToUpper()));

            this._facade.AddLostManHourDetail((LostManHourDetail)domainObject);
            this.RequestData();
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }

            this._facade.DeleteLostManHourDetail((LostManHourDetail[])domainObjects.ToArray(typeof(LostManHourDetail)));

            this.RequestData();
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }

            ((LostManHourDetail)domainObject).Seq = int.Parse(this.txtSeq.Text);

            this._facade.UpdateLostManHourDetail((LostManHourDetail)domainObject);
            this.RequestData();
        }

        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("FLostManHourHeadMP.aspx", new string[] { }, new string[] { }));
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(this.DataProvider);
            }
            DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            LostManHourDetail lostManHourDetail = this._facade.CreateNewLostManHourDetail();

            lostManHourDetail.ShiftDate = FormatHelper.TODateInt(this.DateQuery.Text);
            lostManHourDetail.SSCode = FormatHelper.CleanString(this.txtSSQuery.Text.Trim().ToUpper());
            lostManHourDetail.ShiftCode = FormatHelper.CleanString(this.txtShiftCodeQuery.Text.Trim().ToUpper());
            lostManHourDetail.ItemCode = FormatHelper.CleanString(this.txtitemCode.Text.Trim().ToUpper());
            lostManHourDetail.LostManHour =  Convert.ToInt32(Convert.ToDouble(this.txtLostManHourEdit.Text.Trim()) * 3600);
            lostManHourDetail.ExceptionCode = FormatHelper.CleanString(this.txtExceptionCodeEdit.Text.Trim().ToUpper());
            lostManHourDetail.ExceptionSerial = this.txtRealExceptionCodeEdit.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(this.txtRealExceptionCodeEdit.Text.Trim());
            lostManHourDetail.DutyCode = FormatHelper.CleanString(this.txtDutyCodeEdit.Text.Trim().ToUpper());
            lostManHourDetail.Memo = FormatHelper.CleanString(this.txtMemoEdit.Text.Trim().ToUpper());
            lostManHourDetail.MaintainUser = this.GetUserCode();
            lostManHourDetail.MaintainDate = dBDateTime.DBDate;
            lostManHourDetail.MaintainTime = dBDateTime.DBTime;

            return lostManHourDetail;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtLostManHourEdit.Text = string.Empty;
                this.txtExceptionCodeEdit.Text = string.Empty;
                this.txtRealExceptionCodeEdit.Text = string.Empty;
                this.txtDutyCodeEdit.Text = string.Empty;
                this.txtMemoEdit.Text = string.Empty;
                this.txtSeq.Text = string.Empty;

                return;
            }

            this.txtLostManHourEdit.Text =Convert.ToString(Math.Round(Convert.ToDouble(((LostManHourDetail)obj).LostManHour)/3600,2));
            this.txtExceptionCodeEdit.Text = ((LostManHourDetail)obj).ExceptionCode.ToString();
            this.txtRealExceptionCodeEdit.Text = ((LostManHourDetail)obj).ExceptionSerial == 0 ? string.Empty : ((LostManHourDetail)obj).ExceptionSerial.ToString();
            this.txtDutyCodeEdit.Text = ((LostManHourDetail)obj).DutyCode.ToString();
            this.txtMemoEdit.Text = ((LostManHourDetail)obj).Memo.ToString();
            this.txtSeq.Text = ((LostManHourDetail)obj).Seq.ToString();
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }
            object obj = _facade.GetLostManHourDetail(this.txtSSQuery.Text,
                                                    FormatHelper.TODateInt(this.DateQuery.Text),
                                                    this.txtShiftCodeQuery.Text,
                                                    this.txtitemCode.Text,
                                                    int.Parse(row.Items.FindItemByKey("Seq").Text.ToString()));

            if (obj != null)
            {
                return obj as LostManHourDetail;
            }

            return null;
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new DecimalCheck(lblLostManHourEdit, txtLostManHourEdit, 0, 9999999999, true));
            manager.Add(new LengthCheck(lblExceptionCodeEdit, txtExceptionCodeEdit, 40, true));
            manager.Add(new LengthCheck(lblRealExceptionCodeEdit, txtRealExceptionCodeEdit, 40, false));
            manager.Add(new LengthCheck(lblDutyCodeEdit, txtDutyCodeEdit, 40, true));
            manager.Add(new LengthCheck(lblMemoEdit, txtMemoEdit, 500, false));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            if (Convert.ToDouble(this.txtLostManHourEdit.Text) == 0)
            {
                WebInfoPublish.Publish(this, "$Error_LostManHour_Cannot_Zero", this.languageComponent1);
                return false;
            }

            if (_facade == null)
            {
                _facade = new PerformanceFacade(this.DataProvider);
            }
            //检查实际异常事件和事件代码的一致性
            if (this.txtRealExceptionCodeEdit.Text.Trim() != string.Empty)
            {
                ExceptionEvent exceptionEvent = (ExceptionEvent)_facade.GetExceptionEvent(int.Parse(this.txtRealExceptionCodeEdit.Text));
                if (exceptionEvent == null)
                {
                    WebInfoPublish.Publish(this, "$Error_ExceptionEvent_Not_Exist", this.languageComponent1);
                    return false;
                }

                if (exceptionEvent.ExceptionCode.Trim().ToUpper() != this.txtExceptionCodeEdit.Text.Trim().ToUpper())
                {
                    WebInfoPublish.Publish(this, "$Error_ExceptionEvent_Not_Macth", this.languageComponent1);
                    return false;
                }
            }

            //检查损失工时之和不能大于总损失工时
            LostManHourHead lostManHourHead = (LostManHourHead)_facade.GetLostManHourHead(this.txtSSQuery.Text,
                                                                         FormatHelper.TODateInt(this.DateQuery.Text),
                                                                         this.txtShiftCodeQuery.Text,
                                                                         this.txtitemCode.Text);

            object[] LostManHourDetailList = _facade.QueryLostManHourDetail(this.txtSSQuery.Text,
                                                                         FormatHelper.TODateInt(this.DateQuery.Text),
                                                                         this.txtShiftCodeQuery.Text,
                                                                         this.txtitemCode.Text);

            double lostManHourCount = 0;
            if (lostManHourHead != null && LostManHourDetailList != null)
            {

                for (int i = 0; i < LostManHourDetailList.Length; i++)
                {
                    if (((LostManHourDetailWithMessage)LostManHourDetailList[i]).Seq.ToString() != this.txtSeq.Text.ToString())
                    {
                        lostManHourCount += Math.Round(Convert.ToDouble(((LostManHourDetailWithMessage)LostManHourDetailList[i]).LostManHour) / 3600, 2);
                    }

                }

                lostManHourCount +=Math.Round(Convert.ToDouble(this.txtLostManHourEdit.Text),2);
                lostManHourCount = Math.Round(lostManHourCount, 3);

                if (lostManHourCount > Math.Round(Convert.ToDouble(lostManHourHead.LostManHour)/3600,2))
                {
                    WebInfoPublish.Publish(this, "$Error_LostManHour_Over", this.languageComponent1);
                    return false;
                }
            }

            if (lostManHourHead != null && LostManHourDetailList == null)
            {
                lostManHourCount = 0;
                lostManHourCount += Convert.ToDouble(this.txtLostManHourEdit.Text);

                if (lostManHourCount > Math.Round(Convert.ToDouble(lostManHourHead.LostManHour) / 3600, 2))
                {
                    WebInfoPublish.Publish(this, "$Error_LostManHour_Over", this.languageComponent1);
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region 数据初始化


        #endregion

        #region Export
        // 2005-04-06
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{this.DateQuery.Text,
                                this.txtSSQuery.Text.Trim(),
                                this.txtShiftCodeQuery.Text,
                                this.txtItemCodeQuery.Text,
                                Convert.ToString(Math.Round(Convert.ToDouble(((LostManHourDetailWithMessage)obj).LostManHour)/3600,2)),
							    FormatHelper.ToTimeString(((LostManHourDetailWithMessage)obj).BeginTime),
							    FormatHelper.ToTimeString(((LostManHourDetailWithMessage)obj).EndTime),
							    ((LostManHourDetailWithMessage)obj).ExceptionCode.ToString(),
                                ((LostManHourDetailWithMessage)obj).ExceptionDescription.ToString(),
                                ((LostManHourDetailWithMessage)obj).ExceptionMemo.ToString(),
                                ((LostManHourDetailWithMessage)obj).DutyCode.ToString(),
                                ((LostManHourDetailWithMessage)obj).Memo.ToString()};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"Date",
                                    "sscode",
                                    "ShiftCode",
                                    "MaterialCode",
                                    "LostManHour",
									"ExceptionBeginTime",									
									"ExceptionEndTime",
									"ExceptionCode",
                                    "ExceptionDESC",
									"ExceptionMemo",
									"Duty",
									"Memo"};
        }

        #endregion

    }
}

