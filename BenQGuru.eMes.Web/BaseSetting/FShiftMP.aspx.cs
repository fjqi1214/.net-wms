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
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FShiftMP 的摘要说明。
    /// </summary>
    public partial class FShiftMP : BaseMPageNew
    {
        protected System.Web.UI.WebControls.Label lblShiftTitle;
        protected BenQGuru.eMES.Web.UserControl.eMESTime timeShiftBeginTimeEdit;
        protected BenQGuru.eMES.Web.UserControl.eMESTime timeShiftEndTimeEdit;

        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private BenQGuru.eMES.BaseSetting.ShiftModelFacade _facade = null; //	new ShiftModelFacadeFactory().Create();

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
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
            }
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
            this.gridHelper.AddColumn("ShiftSequence", "班次序列", null);
            this.gridHelper.AddColumn("ShiftCode", "班次代码", null);
            this.gridHelper.AddColumn("ShiftDescription", "班次描述", null);
            this.gridHelper.AddColumn("ShiftSequenceShiftTypeCode", "所属班制", null);
            this.gridHelper.AddColumn("ShiftBeginTime", "起始时间", null);
            this.gridHelper.AddColumn("ShiftEndTime", "结束时间", null);
            this.gridHelper.AddColumn("IsOverDate", "是否跨日期", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);

            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);

            this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["ShiftSequence"] = ((Shift)obj).ShiftSequence.ToString();
            row["ShiftCode"] = ((Shift)obj).ShiftCode.ToString();
            row["ShiftDescription"] = ((Shift)obj).ShiftDescription.ToString();
            row["ShiftSequenceShiftTypeCode"] = ((Shift)obj).GetDisplayText("ShiftTypeCode");
            row["ShiftBeginTime"] = FormatHelper.ToTimeString(((Shift)obj).ShiftBeginTime);
            row["ShiftEndTime"] = FormatHelper.ToTimeString(((Shift)obj).ShiftEndTime);
            row["IsOverDate"] = FormatHelper.DisplayBoolean(((Shift)obj).IsOverDate, this.languageComponent1);
            row["MaintainUser"] = ((Shift)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((Shift)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((Shift)obj).MaintainTime);
            return row;
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new ShiftModelFacadeFactory(base.DataProvider).Create();
            }
            return this._facade.QueryShift(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtShiftCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtShiftTypeCodeQuery.Text)),
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new ShiftModelFacadeFactory(base.DataProvider).Create();
            }
            return this._facade.QueryShiftCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtShiftCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtShiftTypeCodeQuery.Text)));
        }

        #endregion

        #region Button
        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new ShiftModelFacadeFactory(base.DataProvider).Create();
            }
            this._facade.AddShift((Shift)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new ShiftModelFacadeFactory(base.DataProvider).Create();
            }
            this._facade.DeleteShift((Shift[])domainObjects.ToArray(typeof(Shift)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new ShiftModelFacadeFactory(base.DataProvider).Create();
            }
            this._facade.UpdateShift((Shift)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtShiftCodeEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtShiftCodeEdit.ReadOnly = true;
            }
        }
        #endregion

        #region Object <--> Page
        protected override object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new ShiftModelFacadeFactory(base.DataProvider).Create();
            }
            Shift shift = this._facade.CreateNewShift();

            shift.ShiftDescription = FormatHelper.CleanString(this.txtShiftDescriptionEdit.Text, 100);
            shift.ShiftBeginTime = FormatHelper.TOTimeInt(this.timeShiftBeginTimeEdit.Text);
            shift.ShiftEndTime = FormatHelper.TOTimeInt(this.timeShiftEndTimeEdit.Text);
            shift.IsOverDate = FormatHelper.BooleanToString(this.chbIsOverDateEdit.Checked);
            shift.ShiftSequence = System.Decimal.Parse(this.txtShiftSequenceEdit.Text);
            shift.ShiftCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtShiftCodeEdit.Text, 40));
            shift.ShiftTypeCode = this.drpShiftTypeCodeEdit.SelectedValue;
            shift.MaintainUser = this.GetUserCode();

            return shift;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new ShiftModelFacadeFactory(base.DataProvider).Create();
            }
            string strCode = string.Empty;
            object objCode = row.Items.FindItemByKey("ShiftCode").Value;
            if (objCode != null)
            {
                strCode = objCode.ToString();
            }
            object obj = _facade.GetShift(strCode);
            if (obj != null)
            {
                return (Shift)obj;
            }
            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtShiftDescriptionEdit.Text = "";
                this.timeShiftBeginTimeEdit.Text = "";
                this.timeShiftEndTimeEdit.Text = "";
                this.chbIsOverDateEdit.Checked = false;
                this.txtShiftSequenceEdit.Text = "";
                this.txtShiftCodeEdit.Text = "";
                this.drpShiftTypeCodeEdit.SelectedIndex = 0;

                return;
            }

            this.txtShiftDescriptionEdit.Text = ((Shift)obj).ShiftDescription.ToString();
            this.timeShiftBeginTimeEdit.Text = FormatHelper.ToTimeString(((Shift)obj).ShiftBeginTime);
            this.timeShiftEndTimeEdit.Text = FormatHelper.ToTimeString(((Shift)obj).ShiftEndTime);
            this.txtShiftSequenceEdit.Text = ((Shift)obj).ShiftSequence.ToString();
            this.txtShiftCodeEdit.Text = ((Shift)obj).ShiftCode.ToString();
            try
            {
                this.drpShiftTypeCodeEdit.SelectedValue = (obj as Shift).ShiftTypeCode.ToString();
            }
            catch
            {
                this.drpShiftTypeCodeEdit.SelectedIndex = 0;
            }
            this.chbIsOverDateEdit.Checked = FormatHelper.StringToBoolean(((Shift)obj).IsOverDate);
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(lblShiftCodeEdit, txtShiftCodeEdit, 40, true));
            manager.Add(new NumberCheck(lblShiftSequenceEdit, txtShiftSequenceEdit, true));
            manager.Add(new LengthCheck(lblShiftTypeCodeEdit, drpShiftTypeCodeEdit, 40, true));
            manager.Add(new LengthCheck(lblShiftDescriptionEdit, txtShiftDescriptionEdit, 100, false));

            //			if ( !this.chbIsOverDateEdit.Checked )
            //			{
            //				manager.Add( new TimeRangeCheck(this.lblShiftBeginTimeEdit, this.timeShiftBeginTimeEdit.Text, this.lblShiftEndTimeEdit, this.timeShiftEndTimeEdit.Text, true) );
            //			}
            //			else
            //			{
            //				manager.Add( new TimeRangeCheck(this.lblShiftEndTimeEdit, this.timeShiftEndTimeEdit.Text, this.lblShiftBeginTimeEdit, this.timeShiftBeginTimeEdit.Text, true) );
            //			}

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }
            return true;
        }

        #endregion

        #region 数据初始化
        protected void drpShiftTypeCodeEdit_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    DropDownListBuilder builder = new DropDownListBuilder(this.drpShiftTypeCodeEdit);
                    if (_facade == null)
                    {
                        _facade = new ShiftModelFacadeFactory(base.DataProvider).Create();
                    }
                    builder.HandleGetObjectList = new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(this._facade.GetAllShiftType);

                    if (builder.HandleGetObjectList != null)
                    {
                        builder.Build("ShiftTypeDescription", "ShiftTypeCode");
                    }
                    else
                    {
                        this.drpShiftTypeCodeEdit.Items.Add(new ListItem("", ""));
                    }
                }
                catch (Exception)
                {
                    this.drpShiftTypeCodeEdit.Items.Add(new ListItem("", ""));
                }

            }
        }
        #endregion

        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{  ((Shift)obj).ShiftSequence.ToString(),
								   ((Shift)obj).ShiftCode.ToString(),
								   ((Shift)obj).ShiftDescription.ToString(),
								   ((Shift)obj).GetDisplayText("ShiftTypeCode"),
								   FormatHelper.ToTimeString(((Shift)obj).ShiftBeginTime),
								   FormatHelper.ToTimeString(((Shift)obj).ShiftEndTime),
								   FormatHelper.DisplayBoolean(((Shift)obj).IsOverDate, this.languageComponent1),
								   
								   ((Shift)obj).GetDisplayText("MaintainUser"),
								   FormatHelper.ToDateString(((Shift)obj).MaintainDate) };
        }

        protected override string[] GetColumnHeaderText()
        {
            // TODO: 调整字段值的顺序，使之与Grid的列对应
            return new string[] {	
									"ShiftSequence",
									"ShiftCode",
									"ShiftDescription",
									"ShiftSequenceShiftTypeCode",
									"ShiftBeginTime",
									"ShiftEndTime",
									"IsOverDate",
									"MaintainUser",
									"MaintainDate"};
        }
        #endregion

    }
}
