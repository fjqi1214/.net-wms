#region System
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
using System.Text;
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
#endregion

namespace BenQGuru.eMES.Web.MOModel
{
    /// <summary>
    /// FItemRouteSP 的摘要说明。
    /// </summary>
    public partial class FItemRouteSP : BaseMPageMinus
    {

        // private GridHelper gridHelper = null;
        private ButtonHelper buttonHelper = null;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        //private ModelFacade _modelFacade = FacadeFactory.CreateModelFacade();
        private ItemFacade _itemFacade;// = new FacadeFactory(base.DataProvider).CreateItemFacade();
        private BaseModelFacade _baseModelFacade;// = new FacadeFactory(base.DataProvider).CreateBaseModelFacade();
        private ArrayList arrayDeleteRoutes = new ArrayList();

        protected void Page_Load(object sender, System.EventArgs e)
        {
            InitHanders();
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                // 初始化界面UI
                this.InitUI();
                InitParameters();
                this.InitWebGrid();
                this.gridWebGrid.Height = new Unit(100);
                RequestData();
            }


            ResetGrid();
        }

        public void ResetGrid()
        {
            if (_itemFacade == null) { _itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade(); }
            object[] routes = this._itemFacade.GetSelectedRoutesByItemCode(ItemCode);


            foreach (GridRecord row in this.gridWebGrid.Rows)
            {
                if (routes != null)
                {
                    bool iFlag = false;
                    for (int i = 0; i < routes.Length; i++)
                    {
                        if ((((Route)routes[i])).RouteCode == row.Items.FindItemByKey("RouteCode").Value.ToString())
                        {
                            iFlag = true;
                            break;
                        }
                    }

                    if (iFlag)
                        row.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = iFlag;
                }
                //
                if (row.Items.FindItemByKey(gridHelper.CheckColumnKey).Text.Trim().ToLower() == "true")
                {
                    object item2Route = this._itemFacade.GetItem2Route(ItemCode, row.Items.FindItemByKey("RouteCode").Text.Trim(), GlobalVariables.CurrentOrganizations.First().OrganizationID.ToString());
                    if (item2Route != null)
                    {
                        if (((Item2Route)item2Route).IsReference == ItemFacade.IsReference_Used)
                        {
                            //checkbox不可用时，增加灰显样式
                            string strScript = string.Format(@"
                        var  tdImage=  $('#gridWebGrid').children('table').children('tbody').children('tr').children('td').children('table').children('tbody:eq(1)').children('tr').children('td').children('div').children('table').children('tbody').children('tr:eq({0})').children('td:eq({1})').find('img');
                                tdImage.css('background-color', 'gray');
                                tdImage.css('filter', 'alpha(opacity=30)');
                                tdImage.css('-moz-opacity', '0.3');
                                tdImage.css('-khtml-opacity', '0.3');
                                tdImage.css('opacity', '0.3');
                                tdImage.wrap('<div style=\'cursor: auto;height: 100%;width: 100%;z-index: 100;\'></div>')",
                             row.Index, row.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Column.VisibleIndex);

                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), strScript, true);
                        }
                        else
                        {
                            //CheckBox chkBox = (CheckBox)row.Items.FindItemByKey(gridHelper.CheckColumnKey).FindControl("Check");
                            //chkBox.Enabled = true;
                            //this.gridWebGrid.Rows[i].Cells[0].AllowEditing = AllowEditing.Yes;
                        }
                    }
                }

                row.Items.FindItemByKey("IsReference").Text = FormatHelper.DisplayBoolean(true, this.languageComponent1);

            }

            this.gridWebGrid.Behaviors.CreateBehavior<EditingCore>().Behaviors.CreateBehavior<CellEditing>().ColumnSettings["DefaultItemRoute"].ReadOnly = false;
        }

        private string EditGrid
        {
            get
            {
                return (string)this.ViewState["editgrid"];
            }
            set
            {
                this.ViewState["editgrid"] = value;
            }
        }

        private void InitParameters()
        {
            if (this.Request.Params["itemcode"] == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_RequestUrlParameter_Lost");
            }
            else
            {
                this.ViewState["itemcode"] = this.Request.Params["itemcode"];
            }
        }

        public string ItemCode
        {
            get
            {
                return (string)this.ViewState["itemcode"];
            }
        }

        private void InitHanders()
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.buttonHelper = new ButtonHelper(this);
        }

        protected DataRow GetGridRow(object obj)
        {

            MOFacade moFacade = new MOFacade(this.DataProvider);
            DefaultItem2Route defroute = (DefaultItem2Route)moFacade.GetDefaultItem2Route(ItemCode);
            bool bIsDefRoute = (defroute != null && defroute.RouteCode == ((Route)obj).RouteCode);
            DataRow row = this.DtSource.NewRow();

            row["RouteCode"] = ((Route)obj).RouteCode.ToString();
            row["RouteDescription"] = ((Route)obj).RouteDescription.ToString();
            row["DefaultItemRoute"] = bIsDefRoute.ToString().ToLower();
            row["EffectiveDate"] = FormatHelper.ToDateString(((Route)obj).EffectiveDate);
            row["InvalidDate"] = FormatHelper.ToDateString(((Route)obj).InvalidDate);
            row["MaintainUser"] = ((Route)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((Route)obj).MaintainDate);
            row["IsReference"] = "";
            row["IsOPBOMUsed"] = ((Route)obj).EAttribute1;
            return row;

        }


        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("RouteCode", "生产途程代码", null);
            this.gridHelper.AddColumn("RouteDescription", "生产途程描述", null);
            this.gridHelper.AddCheckBoxColumn("DefaultItemRoute", "默认途程", true, null);
            this.gridHelper.AddColumn("EffectiveDate", "生效日期", null);
            this.gridHelper.AddColumn("InvalidDate", "失效日期", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("IsReference", "被引用", null);
            this.gridHelper.AddColumn("IsOPBOMUsed", "是否被OPBOM使用", null);

            this.gridWebGrid.Columns.FromKey("IsReference").Hidden = true;
            this.gridWebGrid.Columns.FromKey("IsOPBOMUsed").Hidden = true;
            this.gridWebGrid.Columns.FromKey("EffectiveDate").Hidden = true;
            this.gridWebGrid.Columns.FromKey("InvalidDate").Hidden = true;

            this.gridHelper.AddDefaultColumn(true, false);
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            if (!IsPostBack)
            {
                EditGrid = "gridWebGrid";
            }
            lblItemRouteMaintain.Visible = false;
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_baseModelFacade == null) { _baseModelFacade = new FacadeFactory(base.DataProvider).CreateBaseModelFacade(); }
            if (EditGrid == "gridWebGrid")
            {
                this.gridWebGrid.Columns.FromKey("IsOPBOMUsed").Hidden = true;
                this.lblItemRouteMaintain.Visible = false;
                return this._baseModelFacade.GetAllRouteEnabled();
            }
            else
            {
                lblItemRouteMaintain.Visible = true;
                lblItemRouteMaintain.Text = this.languageComponent1.GetString("$Error_ItemRouteInformation");
                this.gridWebGrid.Columns.FromKey("IsOPBOMUsed").Hidden = false;
                object[] objs = this._baseModelFacade.GetAllRouteEnabled();
                ArrayList tmpArrayList = new ArrayList();
                if (objs != null)
                {
                    for (int i = 0; i < objs.Length; i++)
                    {
                        for (int j = 0; j < arrayDeleteRoutes.Count; j++)
                        {
                            if (((Route)objs[i]).RouteCode == ((Item2Route)arrayDeleteRoutes[j]).RouteCode)
                            {
                                ((Route)objs[i]).EAttribute1 = FormatHelper.DisplayBoolean(true, this.languageComponent1);
                                tmpArrayList.Add(objs[i]);
                                break;
                            }
                        }

                    }
                }
                return tmpArrayList.ToArray();
            }
        }

        private int GetRowCount()
        {
            if (_baseModelFacade == null) { _baseModelFacade = new FacadeFactory(base.DataProvider).CreateBaseModelFacade(); }
            return this._baseModelFacade.GetAllRouteEnabled().Length;
        }

        private void RequestData()
        {
            if (_itemFacade == null) { _itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade(); }
            this.gridHelper.GridBind(PageGridBunding.Page, int.MaxValue);
        }

        private object GetEditObject(GridRecord row)
        {
            if (_itemFacade == null) { _itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade(); }
            Item2Route item2Route = this._itemFacade.CreateItem2Route();
            item2Route.ItemCode = ItemCode;
            item2Route.RouteCode = row.Items.FindItemByKey("RouteCode").Value.ToString();
            item2Route.MaintainUser = this.GetUserCode();
            item2Route.IsReference = ItemFacade.IsReference_NotUsed;
            // Added By Hi1/Venus.Feng on 20080625 for Hisense Version : Add OrgID
            item2Route.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
            // End Added

            return item2Route;
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

        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {
            if (_itemFacade == null) { _itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade(); }
            if (EditGrid == "gridWebGrid")
            {
                //add by roger.xue
                int iDefaultRouteCount = 0;
                foreach (GridRecord row in this.gridWebGrid.Rows)
                {
                    if (row.Items.FindItemByKey("DefaultItemRoute").Value.ToString().ToLower() == "true")
                    {
                        iDefaultRouteCount++;
                    }

                }

                if (iDefaultRouteCount > 1)
                {
                    //throw new Exception("$Error_Default_Route_More_than_one");
                    WebInfoPublish.PublishInfo(this.Page, "$Error_Default_Route_More_than_one", this.languageComponent1);
                    return;
                }

                if (iDefaultRouteCount < 1)
                {
                    WebInfoPublish.PublishInfo(this.Page, "$Error_Default_Route_Must_one", this.languageComponent1);
                    return;
                }

                for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
                {
                    if (this.gridWebGrid.Rows[i].Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value.ToString().ToLower() == "false")
                    {
                        if (this.gridWebGrid.Rows[i].Items.FindItemByKey("DefaultItemRoute").Value.ToString().ToLower() == "true")
                        {
                            WebInfoPublish.PublishInfo(this.Page, "$Error_Only_Chose_This_Product_route", this.languageComponent1);
                            return;
                            //throw new Exception("$Error_Only_Chose_This_Product_route");
                        }
                    }
                }

                //end add

                foreach (GridRecord row in this.gridWebGrid.Rows)
                {
                    if (row.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value.ToString().ToLower() == "true")
                    {
                        //选择则到数据可查找如果不存在添加，
                        this._itemFacade.AddItemRoutes(ItemCode, new Item2Route[] { (Item2Route)this.GetEditObject(row) });

                        //add by roger.xue 添加默认途程
                        AddDefaultRoute(row);
                        //end add
                    }
                    else
                    {
                        object item2Route = this._itemFacade.GetItem2Route(ItemCode, row.Items.FindItemByKey("RouteCode").Value.ToString(), GlobalVariables.CurrentOrganizations.First().OrganizationID.ToString());
                        if (item2Route != null)
                        {
                            if (this._itemFacade.IsItemRouteComponentLoading((Item2Route)item2Route))
                            {
                                row.Items.FindItemByKey("IsReference").Text = FormatHelper.BooleanToString(true);
                                arrayDeleteRoutes.Add(item2Route);
                            }
                            else
                            {
                                row.Items.FindItemByKey("IsReference").Text = FormatHelper.BooleanToString(false);
                                //this._itemFacade.DeleteItem2Route((Item2Route)item2Route);
                                this._itemFacade.DeleteItem2RouteWithOPBOM((Item2Route)item2Route);
                                //add by roger.xue
                                DeleteDefaultRoute((Item2Route)item2Route);
                                //end add
                            }

                        }
                    }
                }
                if (arrayDeleteRoutes.Count == 0)
                {
                    EditGrid = "gridWebGrid1";
                    RegistScript();
                }
                else
                {
                    EditGrid = "gridWebGrid1";
                }
            }
            else
            {
                foreach (GridRecord row in this.gridWebGrid.Rows)
                {
                    if (row.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value.ToString().ToLower() == "true")
                    {
                        object item2Route = this._itemFacade.GetItem2Route(ItemCode, row.Items.FindItemByKey("RouteCode").Value.ToString(), GlobalVariables.CurrentOrganizations.First().OrganizationID.ToString());
                        if (item2Route != null)
                        {
                            this._itemFacade.DeleteItem2RouteWithOPBOM((Item2Route)item2Route);
                            //add by roger.xue
                            DeleteDefaultRoute((Item2Route)item2Route);
                            //end add
                        }
                    }
                }
                EditGrid = "gridWebGrid";
                lblItemRouteMaintain.Visible = true;
                lblItemRouteMaintain.Text = this.languageComponent1.GetString("$Error_ItemRouteInformation");
                RegistScript();
            }


            //			this._itemFacade.AddItemRoutes(ItemCode, (Item2Route[])routes.ToArray( typeof(Item2Route) ) );
            this.gridHelper.GridBind(PageGridBunding.Page, int.MaxValue);

        }

        //add by roger.xue
        private void AddDefaultRoute(GridRecord row)
        {
            MOFacade moFacade = new MOFacade(this.DataProvider);
            DefaultItem2Route defroute = (DefaultItem2Route)moFacade.GetDefaultItem2Route(ItemCode);
            // 如果有选择默认途程
            if (row.Items.FindItemByKey("DefaultItemRoute").Value.ToString().ToLower() == "true")
            {
                if (defroute == null || defroute.RouteCode != row.Items.FindItemByKey("RouteCode").Value.ToString())
                {
                    bool bIsNew = false;
                    if (defroute == null)
                    {
                        defroute = new DefaultItem2Route();
                        bIsNew = true;
                    }
                    defroute.ItemCode = ItemCode;
                    defroute.RouteCode = row.Items.FindItemByKey("RouteCode").Value.ToString();
                    defroute.MDate = FormatHelper.TODateInt(DateTime.Now);
                    defroute.MTime = FormatHelper.TOTimeInt(DateTime.Now);
                    if (bIsNew == true)
                        moFacade.AddDefaultItem2Route(defroute);
                    else
                        moFacade.UpdateDefaultItem2Route(defroute);
                }
            }
            else if (row.Items.FindItemByKey("DefaultItemRoute").Value.ToString().ToLower() == "false" &&
                defroute != null && defroute.RouteCode == row.Items.FindItemByKey("RouteCode").Value.ToString())
            {

                moFacade.DeleteDefaultItem2Route(defroute);

            }
        }
        private void DeleteDefaultRoute(Item2Route item2Route)
        {
            MOFacade moFacade = new MOFacade(this.DataProvider);
            DefaultItem2Route defroute = (DefaultItem2Route)moFacade.GetDefaultItem2Route(ItemCode);
            if (defroute != null && defroute.RouteCode == item2Route.RouteCode)
            {
                moFacade.DeleteDefaultItem2Route(defroute);
            }
        }
        //end add

        private void RegistScript()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<script language=\"javascript\">");
            stringBuilder.Append("document.parentWindow.parent.location.reload(); ");
            stringBuilder.Append("</script>");
            this.RegisterClientScriptBlock("refresh", stringBuilder.ToString());
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "refresh", stringBuilder.ToString(), false);
        }
    }
}
