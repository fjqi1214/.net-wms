using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Common;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FInvPeriodMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private InventoryFacade facade = null;


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
            if (!this.IsPostBack)
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
            this.gridHelper.AddColumn("InvPeriodCode", "账龄代码", null);
            this.gridHelper.AddColumn("DateFromPeriod", "起止天", null);
            this.gridHelper.AddColumn("DateToPeriod", "截止天", null);
            this.gridHelper.AddColumn("PeiodGroup", "账龄组", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);

            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            if (!IsPostBack)
            {
                this.gridHelper.RequestData();
            }

           // this.gridHelper.RequestData();
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    ((InvPeriod)obj).InvPeriodCode.ToString(),
            //                    ((InvPeriod)obj).DateFrom.ToString(),
            //                    ((InvPeriod)obj).DateTo.ToString(),
            //                    ((InvPeriod)obj).PeiodGroup.ToString(),
            //                    //((InvPeriod)obj).MaintainUser.ToString(),
            //                 ((InvPeriod)obj).GetDisplayText("MaintainUser"),
            //                    FormatHelper.ToDateString(((InvPeriod)obj).MaintainDate)});

            DataRow row = this.DtSource.NewRow();
            row["InvPeriodCode"] = ((InvPeriod)obj).InvPeriodCode.ToString();
            row["DateFromPeriod"] = ((InvPeriod)obj).DateFrom.ToString();
            row["DateToPeriod"] = ((InvPeriod)obj).DateTo.ToString();
            row["PeiodGroup"] = ((InvPeriod)obj).PeiodGroup.ToString();
            row["MaintainUser"] = ((InvPeriod)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((InvPeriod)obj).MaintainDate);
            return row;
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }

            return this.facade.QueryInvPeriod(inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            return this.facade.QueryInvPeriodCount();
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }

            DBDateTime dateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            InvPeriod invPeriod = domainObject as InvPeriod;

            object obj = this.facade.GetInvPeriod(invPeriod.InvPeriodCode, invPeriod.PeiodGroup);

            if (obj != null)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Primary_Key_Overlap");
            }

            invPeriod.MaintainUser = this.GetUserCode();
            invPeriod.MaintainDate = dateTime.DBDate;
            invPeriod.MaintainTime = dateTime.DBTime;

            this.facade.AddInvPeriod(invPeriod);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            this.facade.DeleteInvPeriod((InvPeriod[])domainObjects.ToArray(typeof(InvPeriod)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }

            DBDateTime dateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            InvPeriod invPeriod = domainObject as InvPeriod;
            invPeriod.MaintainUser = this.GetUserCode();
            invPeriod.MaintainDate = dateTime.DBDate;
            invPeriod.MaintainTime = dateTime.DBTime;

            this.facade.UpdateInvPeriod(invPeriod);
        }


        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtPeriodCodeEdit.ReadOnly = false;
                this.txtPeiodGroupEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtPeriodCodeEdit.ReadOnly = true;
                this.txtPeiodGroupEdit.ReadOnly = true;
            }
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }

            InvPeriod invPeriod = this.facade.CreateNewInvPeriod();

            invPeriod.InvPeriodCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPeriodCodeEdit.Text, 40));
            invPeriod.PeiodGroup = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPeiodGroupEdit.Text, 40));
            invPeriod.DateFrom = int.Parse(this.txtDateFromPeriodEdit.Text);
            invPeriod.DateTo = int.Parse(this.txtDateToPeriodEdit.Text);
            invPeriod.MaintainUser = this.GetUserCode();

            return invPeriod;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }

            object obj = facade.GetInvPeriod(row.Items.FindItemByKey("InvPeriodCode").Value.ToString(), row.Items.FindItemByKey("PeiodGroup").Value.ToString());

            if (obj != null)
            {
                return (InvPeriod)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtPeriodCodeEdit.Text = string.Empty;
                this.txtDateFromPeriodEdit.Text = string.Empty;
                this.txtDateToPeriodEdit.Text = string.Empty;
                this.txtPeiodGroupEdit.Text = string.Empty;
                return;
            }

            this.txtPeriodCodeEdit.Text = ((InvPeriod)obj).InvPeriodCode.ToString();
            this.txtDateFromPeriodEdit.Text = ((InvPeriod)obj).DateFrom.ToString();
            this.txtDateToPeriodEdit.Text = ((InvPeriod)obj).DateTo.ToString();
            this.txtPeiodGroupEdit.Text = ((InvPeriod)obj).PeiodGroup.ToString();

        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblPeriodCodeEdit, this.txtPeriodCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblPeiodGroupEdit, this.txtPeiodGroupEdit, 40,true));
            manager.Add(new LengthCheck(this.lblDateFromPeriodEdit, this.txtDateFromPeriodEdit, 8, true));
            manager.Add(new LengthCheck(this.lblDateToPeriodEdit, this.txtDateToPeriodEdit, 8, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            int dateFrom = int.Parse(this.txtDateFromPeriodEdit.Text);
            int dateTo = int.Parse(this.txtDateToPeriodEdit.Text);
            if (dateTo < dateFrom)
            {
                WebInfoPublish.Publish(this, "$DataTo_Must_Lenth_Than_DateFrom", this.languageComponent1);
                return false;
            }
            if (dateTo > 99999)
            {
                WebInfoPublish.Publish(this, "$Error_DateToTooBig", this.languageComponent1);
                return false;
            }

            return true;
        }

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{((InvPeriod)obj).InvPeriodCode.ToString(),
                                ((InvPeriod)obj).DateFrom.ToString(),
                                ((InvPeriod)obj).DateTo.ToString(),
                                ((InvPeriod)obj).PeiodGroup.ToString(),
                                ((InvPeriod)obj).MaintainUser.ToString(),
                                FormatHelper.ToDateString(((InvPeriod)obj).MaintainDate)};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"InvPeriodCode",
                                    "DateFromPeriod",
                                    "DateToPeriod",
                                    "PeiodGroup",
                                    "MaintainUser",	
                                    "MaintainDate"};
        }

        #endregion
    }
}
