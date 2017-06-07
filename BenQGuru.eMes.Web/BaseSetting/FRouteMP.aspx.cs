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

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FRouteMP 的摘要说明。
    /// </summary>
    public partial class FRouteMP : BaseMPageNew
    {
        protected System.Web.UI.WebControls.Label lblRouteTitle;

        public BenQGuru.eMES.Web.UserControl.eMESDate dateEffectiveDateEdit;
        public BenQGuru.eMES.Web.UserControl.eMESDate dateInvalidDateEdit;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private BenQGuru.eMES.BaseSetting.BaseModelFacade _facade = null;//	new BaseModelFacadeFactory().Create();

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
            //this.gridWebGrid.InitializeRow += new Infragistics.Web.UI.GridControls.InitializeRowEventHandler(gridWebGrid_InitializeRow);
        }

//        void gridWebGrid_InitializeRow(object sender, Infragistics.Web.UI.GridControls.RowEventArgs e)
//        {
//            e.Row.Items.FindItemByKey("RouteDescription").CssClass = "ForeColorRed CellBackColor";
//            string strScript = string.Format(@"
//                         $('#gridWebGrid').children('table').children('tbody').children('tr').children('td').children('table').children('tbody:eq(1)').children('tr').children('td').children('div').children('table').children('tbody').children('tr:eq({0})').children('td:eq({1})').css('background-color','yellow');
//                               ",
//                            e.Row.Index, e.Row.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Column.VisibleIndex);

//            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), strScript, true);



//        }

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
            //this.gridWebGrid.Behaviors.CreateBehavior<ColumnFixing>().Enabled = true;
            //this.gridWebGrid.Behaviors.CreateBehavior<ColumnFixing>().ColumnSettings.Add(new ColumnFixingSetting(gridWebGrid));
            //this.gridWebGrid.Behaviors.CreateBehavior<ColumnMoving>().Enabled = true;
            //this.gridWebGrid.Behaviors.CreateBehavior<ColumnMoving>().ColumnSettings.Add(new ColumnMoveSetting(gridWebGrid));
            //this.gridWebGrid.Behaviors.CreateBehavior<Sorting>().Enabled = true;
            //this.gridWebGrid.Behaviors.CreateBehavior<Sorting>().SortingMode = SortingMode.Multi;
            //this.gridWebGrid.Behaviors.CreateBehavior<Sorting>().EnableCellMerging = true;
            //this.gridWebGrid.Behaviors.CreateBehavior<Sorting>().ColumnSorted += new ColumnSortedHandler(gridWebGrid_ColumnSorted);
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
            this.gridHelper.AddColumn("RouteCode", "生产途程代码", null);
            this.gridHelper.AddDataColumn("RouteDescription", "生产途程描述", HorizontalAlign.Center);
            this.gridHelper.AddColumn("RouteType", "生产途程类别", null);
            this.gridHelper.AddDataColumn("Enabled", "是否生效", 40);
            this.gridHelper.AddLinkColumn("operation", "工序列表", null);
            this.gridHelper.AddLinkColumn("graphic", "图形化界面", null);

            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);

            this.gridHelper.AddColumn("EffectiveDate", "生效日期", null);
            this.gridHelper.AddColumn("InvalidDate", "失效日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);

            //(this.gridWebGrid.Columns.FromKey("operation") as BoundDataField).Width = new Unit(60);
            //(this.gridWebGrid.Columns.FromKey("graphic") as BoundDataField).Width = new Unit(65);

            this.gridWebGrid.Columns.FromKey("EffectiveDate").Hidden = true;
            this.gridWebGrid.Columns.FromKey("InvalidDate").Hidden = true;
            this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
            this.gridWebGrid.Columns.FromKey("RouteType").Hidden = true;

            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        //protected void WebDataGrid1_ColumnMoved(object sender, ColumnMovingEventArgs e)
        //{
        //    int oldIndex = e.PreviousVisibleIndex;
        //    int newIndex = e.Column.VisibleIndex;
        //    if (oldIndex != newIndex)
        //    {
        //        SortedColumnInfo info = this.gridWebGrid.Behaviors.Sorting.SortedColumns[oldIndex];
        //        this.gridWebGrid.Behaviors.Sorting.SortedColumns.RemoveAt(oldIndex);
        //        this.gridWebGrid.Behaviors.Sorting.SortedColumns.Insert(newIndex, info);
        //        this.gridWebGrid.Rows.Clear();
        //        this.gridWebGrid.DataBind();
        //    }
        //}

        //protected void WebDataGrid1_FixedStateChanged(object sender, FixedStateChangedEventArgs e)
        //{
        //    if (!e.IsFixed)
        //    {
        //        foreach (FixedColumnInfo info in e.AffectedColumns)
        //        {
        //            this.gridWebGrid.Behaviors.ColumnFixing.ColumnSettings[info.ColumnKey].ShowFixButton = false;
        //            this.gridWebGrid.Columns[info.ColumnKey].VisibleIndex = this.gridWebGrid.Columns[info.ColumnKey].Index + this.gridWebGrid.Behaviors.ColumnFixing.FixedColumns.Count;
        //            this.gridWebGrid.Behaviors.ColumnMoving.ColumnSettings[info.ColumnKey].EnableMove = false;
        //            for (int x = 0; x < this.gridWebGrid.Behaviors.Sorting.SortedColumns.Count; ++x)
        //            {
        //                if (this.gridWebGrid.Behaviors.Sorting.SortedColumns[x].ColumnKey == info.ColumnKey)
        //                {
        //                    this.gridWebGrid.Behaviors.Sorting.SortedColumns.RemoveAt(x);
        //                    break;
        //                }
        //            }

        //        }
        //    }
        //}
        //protected void gridWebGrid_ColumnSorted(object sender, SortingEventArgs e)
        //{
        //    if (e.Column != null && e.SortedColumns.Count > 1)
        //    {
        //        foreach (SortedColumnInfo info in e.SortedColumns)
        //        {
        //            this.gridWebGrid.Behaviors.ColumnFixing.FixedColumns.Add(info.Column);
        //            this.gridWebGrid.Behaviors.ColumnFixing.ColumnSettings[info.Column.Key].ShowFixButton = true;
        //            this.gridWebGrid.Behaviors.ColumnMoving.ColumnSettings[info.Column.Key].EnableMove = true;
        //        }
        //    }
        //    else
        //    {
        //        for (int x = this.gridWebGrid.Behaviors.ColumnFixing.FixedColumns.Count - 1; x >= 0; --x)
        //        {
        //            GridField field = this.gridWebGrid.Columns[this.gridWebGrid.Behaviors.ColumnFixing.FixedColumns[x].ColumnKey];
        //            this.gridWebGrid.Behaviors.ColumnFixing.FixedColumns.RemoveAt(x);
        //            field.VisibleIndex = field.Index + this.gridWebGrid.Behaviors.ColumnFixing.FixedColumns.Count;

        //        }
        //        this.gridWebGrid.Behaviors.ColumnFixing.ColumnSettings[e.Column.Key].ShowFixButton = false;
        //        this.gridWebGrid.Behaviors.ColumnMoving.ColumnSettings[e.Column.Key].EnableMove = false;
        //    }
        //}

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["RouteCode"] = (obj as Route).RouteCode.ToString();
            row["RouteDescription"] = (obj as Route).RouteDescription.ToString();
            row["RouteType"] = this.languageComponent1.GetString(((Route)obj).RouteType.ToString());
            row["Enabled"] = FormatHelper.DisplayBoolean((obj as Route).Enabled, this.languageComponent1);
            row["operation"] = string.Empty;
            row["graphic"] = string.Empty;
            row["MaintainUser"] = (obj as Route).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString((obj as Route).MaintainDate);
            row["EffectiveDate"] = FormatHelper.ToDateString((obj as Route).EffectiveDate);
            row["InvalidDate"] = FormatHelper.ToDateString((obj as Route).InvalidDate);
            row["MaintainTime"] = FormatHelper.ToTimeString((obj as Route).MaintainTime);
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            return this._facade.QueryRoute(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtRouteCodeQuery.Text)),
                inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            return this._facade.QueryRouteCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtRouteCodeQuery.Text)));
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            this._facade.AddRoute((Route)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            this._facade.DeleteRoute((Route[])domainObjects.ToArray(typeof(Route)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            this._facade.UpdateRoute((Route)domainObject);
        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "operation")
            {
                this.Response.Redirect(this.MakeRedirectUrl("FRoute2OperationSP.aspx", new string[] { "routecode" }, new string[] { row.Items.FindItemByKey("RouteCode").Value.ToString() }));
            }
            if (commandName == "graphic")
            {
                this.Response.Redirect(this.MakeRedirectUrl("../BenQGuru.eMES.Web.Graphical/FgRoute2Op.htm", new string[] { "code" }, new string[] { row.Items.FindItemByKey("RouteCode").Value.ToString() }));
            }
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtRouteCodeEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtRouteCodeEdit.ReadOnly = true;
            }
        }
        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            Route route = this._facade.CreateNewRoute();

            route.RouteDescription = FormatHelper.CleanString(this.txtRouteDescriptionEdit.Text, 100);
            route.RouteType = this.drpRouteTypeEdit.SelectedValue;
            route.EffectiveDate = 0;//FormatHelper.TODateInt(this.dateEffectiveDateEdit.Text);
            route.InvalidDate = 0;//FormatHelper.TODateInt(this.dateInvalidDateEdit.Text);
            route.RouteCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtRouteCodeEdit.Text, 40));
            route.MaintainUser = this.GetUserCode();
            route.Enabled = FormatHelper.BooleanToString(this.chbRouteEnabled.Checked);

            return route;
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            string strCode = string.Empty;
            object objCode = row.Items.FindItemByKey("RouteCode").Value;
            if (objCode != null)
            {
                strCode = objCode.ToString();
            }
            object obj = _facade.GetRoute(strCode);
            if (obj != null)
            {
                return (Route)obj;
            }
            return null;
        }


        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtRouteDescriptionEdit.Text = "";
                this.drpRouteTypeEdit.SelectedIndex = 0;
                //				this.dateEffectiveDateEdit.Text	= "";
                //				this.dateInvalidDateEdit.Text	= "";
                this.txtRouteCodeEdit.Text = "";
                this.chbRouteEnabled.Checked = true;

                return;
            }

            this.txtRouteDescriptionEdit.Text = ((Route)obj).RouteDescription.ToString();
            try
            {
                this.drpRouteTypeEdit.SelectedValue = ((Route)obj).RouteType.ToString();
            }
            catch
            {
                this.drpRouteTypeEdit.SelectedIndex = 0;
            }

            //			this.dateEffectiveDateEdit.Text	= FormatHelper.ToDateString(((Route)obj).EffectiveDate);
            //			this.dateInvalidDateEdit.Text	= FormatHelper.ToDateString(((Route)obj).InvalidDate);

            this.txtRouteCodeEdit.Text = ((Route)obj).RouteCode.ToString();
            this.chbRouteEnabled.Checked = (((Route)obj).Enabled == FormatHelper.TRUE_STRING);
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            if (_facade.IsRouteRef(((Route)obj).RouteCode.ToString()))
            {
                this.txtRouteCodeEdit.ReadOnly = true;
            }
        }


        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblPRouteCodeEdit, this.txtRouteCodeEdit, 40, true));
            //			manager.Add( new LengthCheck(this.lblRouteTypeEdit, this.drpRouteTypeEdit, 40, true) );			
            manager.Add(new LengthCheck(this.lblRouteDescriptionEdit, this.txtRouteDescriptionEdit, 100, false));
            //			manager.Add( new DateRangeCheck(this.lblEffectiveDateEdit,this.dateEffectiveDateEdit.Text, this.dateInvalidDateEdit.Text,true) );

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;
        }

        #endregion

        #region 数据初始化
        protected void drpRouteTypeEdit_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpRouteTypeEdit.Items.Clear();

                if (InternalSystemVariable.Lookup("RouteType") == null)
                {
                    return;
                }

                foreach (string item in (InternalSystemVariable.Lookup("RouteType").Items))
                {
                    this.drpRouteTypeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(item), item));
                }
            }
        }
        #endregion

        #region Export
        // 2005-04-06
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{  ((Route)obj).RouteCode.ToString(),
								   ((Route)obj).RouteDescription.ToString(),
								   FormatHelper.DisplayBoolean(((Route)obj).Enabled,this.languageComponent1) ,
								   ((Route)obj).GetDisplayText("MaintainUser"),
								   FormatHelper.ToDateString(((Route)obj).MaintainDate), };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"RouteCode",
									"RouteDescription",
									"Enabled",
									"MaintainUser",
									"MaintainDate"};
        }

        #endregion
    }
}
