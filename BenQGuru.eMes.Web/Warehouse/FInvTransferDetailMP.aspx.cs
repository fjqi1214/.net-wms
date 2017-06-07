using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Web.Helper;
using Infragistics.WebUI.UltraWebGrid;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FInvTransferDetailMP : BaseMPageMinus
    {
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        //private GridHelper gridHelper = null;
        private ButtonHelper buttonHelper = null;
        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
        WarehouseFacade _TransferFacade = null;


        #region Web Init
        override protected void OnInit(EventArgs e)
        {

            InitializeComponent();
            base.OnInit(e);
        }


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            //this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            // 
            // languageComponent1
            // 
            //this.languageComponent1.Language = "CHS";
            //this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            //this.languageComponent1.RuntimePage = null;
            //this.languageComponent1.RuntimeUserControl = null;
            //this.languageComponent1.UserControlName = "";
            // 
            // excelExporter
            // 
            this.excelExporter.FileExtension = "xls";
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.Page = this;
            this.excelExporter.RowSplit = "\r\n";
            //this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
        }
        #endregion

        #region  Events

        protected void Page_Load(object sender, EventArgs e)
        {
            InitOnPostBack();
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                // 初始化界面UI
                this.InitUI();
                InitButtonHelp();
                SetEditObject(null);
                this.InitWebGrid();

                this.InitParameters();

                // 初始化控件


                this.txtTransferNo.ReadOnly = true;

                ViewState["operate"] = "Add";
                RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
                //单据已经关闭的不能新增行项目
                if (this.ViewState["TransferStatus"].ToString() == this.languageComponent1.GetString(RecordStatus.RecordStatus_CLOSE))
                {
                    this.cmdAdd.Disabled = true;
                }
                else
                {
                    this.cmdAdd.Disabled = false;
                }

            }
        }

        protected void cmdSave_ServerClick(object sender, EventArgs e)
        {
            ViewState["operate"] = "Edit";
            if (this.ValidateInput())
            {
                if (_TransferFacade == null)
                {
                    _TransferFacade = new WarehouseFacade(this.DataProvider);
                }

                object obj = this.GetEditObject();

                if (obj != null)
                {
                    if (((InvTransferDetail)obj).TransferStatus != RecordStatus.RecordStatus_NEW)
                    {
                        WebInfoPublish.Publish(this, "$Error_OnlyNEWCanEdit", this.languageComponent1);
                        return;
                    }

                    if (!String.IsNullOrEmpty(((InvTransferDetail)obj).MOCode))
                    {
                        MOFacade _MOFacade = new MOFacade();
                        object MO = _MOFacade.GetMO(((InvTransferDetail)obj).MOCode);
                        if (MO == null)
                        {
                            WebInfoPublish.Publish(this, "$Error_MOCodeNotInTBLMO", this.languageComponent1);
                            return;
                        }
                    }
                    //根据客户代码从表TblCustomer中取得客户名称

                    if (!String.IsNullOrEmpty(((InvTransferDetail)obj).CustomerCode))
                    {
                        Customer customer = (Customer)_TransferFacade.GetCustomer(((InvTransferDetail)obj).CustomerCode);
                        if (customer != null)
                        {
                            ((InvTransferDetail)obj).CustomerName = customer.CustomerName;
                        }
                    }

                    _TransferFacade.UpdateInvTransferDetail((InvTransferDetail)obj);
                }
            }

            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Save);
            if (this.ViewState["TransferStatus"].ToString() == this.languageComponent1.GetString(RecordStatus.RecordStatus_CLOSE))
            {
                this.cmdAdd.Disabled = true;
            }
            else
            {
                this.cmdAdd.Disabled = false;
            }
        }

        protected void cmdAdd_ServerClick(object sender, EventArgs e)
        {
            ViewState["operate"] = "Add";
            if (this.ValidateInput())
            {
                if (_TransferFacade == null)
                {
                    _TransferFacade = new WarehouseFacade(this.DataProvider);
                }

                object obj = this.GetEditObject();

                if (obj != null)
                {
                    //add前检验唯一性

                    object hasData = _TransferFacade.GetInvTransferDetail(((InvTransferDetail)obj).TransferNO, ((InvTransferDetail)obj).TransferLine);
                    if (hasData != null)
                    {
                        WebInfoPublish.Publish(this, "$Error_TransferHasData", this.languageComponent1);
                        return;
                    }

                    if (!String.IsNullOrEmpty(((InvTransferDetail)obj).MOCode))
                    {
                        MOFacade _MOFacade = new MOFacade();
                        object MO = _MOFacade.GetMO(((InvTransferDetail)obj).MOCode);
                        if (MO == null)
                        {
                            WebInfoPublish.Publish(this, "$Error_MOCodeNotInTBLMO", this.languageComponent1);
                            return;
                        }
                    }

                    ((InvTransferDetail)obj).TransferStatus = RecordStatus.RecordStatus_NEW;
                    ((InvTransferDetail)obj).TransferDate = ((InvTransferDetail)obj).Mdate;
                    ((InvTransferDetail)obj).TransferTime = ((InvTransferDetail)obj).Mtime;
                    ((InvTransferDetail)obj).TransferUser = ((InvTransferDetail)obj).Muser;

                    //根据客户代码从表TblCustomer中取得客户名称

                    if (!String.IsNullOrEmpty(((InvTransferDetail)obj).CustomerCode))
                    {
                        Customer customer = (Customer)_TransferFacade.GetCustomer(((InvTransferDetail)obj).CustomerCode);
                        if (customer != null)
                        {
                            ((InvTransferDetail)obj).CustomerName = customer.CustomerName;
                        }
                    }

                    _TransferFacade.AddInvTransferDetail((InvTransferDetail)obj);
                }
            }
            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
        }


        protected void cmdCancel_ServerClick(object sender, EventArgs e)
        {
            //this.cmdSave.Disabled = true;
            this.txtLineNoEdit.Enabled = true;


            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Cancel);

            if (this.ViewState["TransferStatus"].ToString() == this.languageComponent1.GetString(RecordStatus.RecordStatus_CLOSE))
            {
                this.cmdAdd.Disabled = true;
            }
            else
            {
                this.cmdAdd.Disabled = false;
            }
        }

        protected void cmdDelete_ServerClick(object sender, EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            ArrayList transferDetailList = new ArrayList(array.Count);

            if (array.Count > 0)
            {
                foreach (GridRecord row in array)
                {
                    object obj = this.GetEditObject(row);
                    if (obj != null)
                    {
                        if (((InvTransferDetail)obj).TransferStatus == RecordStatus.RecordStatus_CLOSE || ((InvTransferDetail)obj).TransferStatus == RecordStatus.RecordStatus_USING)
                        {
                            WebInfoPublish.Publish(this, "$Error_TransferCannotDelete", this.languageComponent1);
                            return;
                        }

                    }
                    transferDetailList.Add((InvTransferDetail)obj);
                }
            }

            this._TransferFacade.DeleteInvTransferDetail(((InvTransferDetail[])transferDetailList.ToArray(typeof(InvTransferDetail))));

            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Delete);
            if (this.ViewState["TransferStatus"].ToString() == this.languageComponent1.GetString(RecordStatus.RecordStatus_CLOSE))
            {
                this.cmdAdd.Disabled = true;
            }
            else
            {
                this.cmdAdd.Disabled = false;
            }
        }



        protected void ReturnClick(Object obj, EventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl("./FInvTransferMP.aspx"));
        }


        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();


            manager.Add(new NumberCheck(this.lblLineNoEdit, this.txtLineNoEdit, 0, int.MaxValue, true));
            manager.Add(new LengthCheck(this.lblItemIDEdit, this.txtItemIDEdit, 40, true));
            manager.Add(new LengthCheck(this.lblPlanQTYEdit, this.txtPlanQTYEdit, 22, true));
            if (this.txtOrderLine.Text.Trim() != "")
            {
                manager.Add(new NumberCheck(this.lblOrderLine, this.txtOrderLine, 0, int.MaxValue, false));
            }
            if (this.ViewState["RECType"].ToString() == this.languageComponent1.GetString(RecordType.RecordType_MO))
            {
                manager.Add(new LengthCheck(this.lblMOCode, this.txtMOCode, 40, true));
            }

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }
            return true;
        }

        private bool ValidateQueryInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblTransferNo, this.txtTransferNo, 40, true));
            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }

            return true;
        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "Edit")
            {
                object obj = this.GetEditObject(row);

                if (obj != null)
                {
                    if (((InvTransferDetail)obj).TransferStatus != RecordStatus.RecordStatus_NEW)
                    {
                        WebInfoPublish.Publish(this, "$Error_OnlyNEWCanEdit", this.languageComponent1);
                        return;
                    }

                    this.cmdSave.Disabled = false;
                    this.cmdAdd.Disabled = true;
                    this.txtLineNoEdit.Enabled = false;
                    this.SetEditObject(obj);

                }
            }
        }

        #endregion

        #region For Page Load

        private void InitParameters()
        {
            if (this.Request.Params["RECType"] == null)
            {
                this.ViewState["RECType"] = string.Empty;
            }
            else
            {
                this.ViewState["RECType"] = this.Request.Params["RECType"];
            }

            if (this.Request.Params["TransferStatus"] == null)
            {
                this.ViewState["TransferStatus"] = string.Empty;
            }
            else
            {
                this.ViewState["TransferStatus"] = this.Request.Params["TransferStatus"];
            }

            if (this.Request.Params["TransferNO"] == null)
            {
                this.ViewState["TransferNO"] = string.Empty;
            }
            else
            {
                this.ViewState["TransferNO"] = this.Request.Params["TransferNO"];
            }

            this.txtTransferNo.Text = this.ViewState["TransferNO"].ToString();
        }

        private void InitOnPostBack()
        {
            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);

            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);

        }

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("TransferLine", "行号", null);
            this.gridHelper.AddColumn("OrderNO", "订单号", null);
            this.gridHelper.AddColumn("OrderLine", "订单行号", null);
            this.gridHelper.AddColumn("ItemID", "物料代码", null);
            this.gridHelper.AddColumn("ItemDESC", "物料描述", null);
            this.gridHelper.AddColumn("PlanQTY", "计划数量", null);
            this.gridHelper.AddColumn("CustomerCode", "客户代码", null);
            this.gridHelper.AddColumn("CustomerName", "客户名称", null);
            this.gridHelper.AddColumn("MOCode", "工单号", null);
            this.gridHelper.AddColumn("Memo", "备注", null);

            // 2005-04-06
            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        public void InitButtonHelp()
        {
            //this.buttonHelper.AddDeleteConfirm();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);

        }




        #endregion

        #region For Query Data

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_TransferFacade == null) { _TransferFacade = new WarehouseFacade(this.DataProvider); }

            return this._TransferFacade.QueryTransferDetail(
                FormatHelper.CleanString(this.txtTransferNo.Text.Trim().ToUpper()),
                inclusive, exclusive);
        }

        private int GetRowCount()
        {
            if (_TransferFacade == null) { _TransferFacade = new WarehouseFacade(this.DataProvider); }


            return this._TransferFacade.QueryTransferCountDetail(
                FormatHelper.CleanString(this.txtTransferNo.Text.Trim().ToUpper()));
        }

        #endregion

        #region For Grid And Edit

        protected DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["TransferLine"] = ((InvTransferDetailForQuey)obj).TransferLine.ToString();
            row["OrderNO"] = ((InvTransferDetailForQuey)obj).OrderNO.ToString();
            row["OrderLine"] = ((InvTransferDetailForQuey)obj).OrderLine.ToString();
            row["ItemID"] = ((InvTransferDetailForQuey)obj).ItemCode.ToString();
            row["ItemDESC"] = ((InvTransferDetailForQuey)obj).MaterialDescription.ToString();
            row["PlanQTY"] = ((InvTransferDetailForQuey)obj).Planqty.ToString();
            row["CustomerCode"] = ((InvTransferDetailForQuey)obj).CustomerCode.ToString();
            row["CustomerName"] = ((InvTransferDetailForQuey)obj).CustomerName.ToString();
            row["MOCode"] = ((InvTransferDetailForQuey)obj).MOCode.ToString();
            row["Memo"] = ((InvTransferDetailForQuey)obj).Memo.ToString();
            return row;
        }

        private void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtLineNoEdit.Text = string.Empty;
                this.txtItemIDEdit.Text = string.Empty;
                this.txtPlanQTYEdit.Text = string.Empty;
                this.txtOrderCodeEdit.Text = string.Empty;
                this.txtOrderLine.Text = string.Empty;
                this.txtCustomerCode.Text = string.Empty;
                this.txtMOCode.Text = string.Empty;
                this.txtRemark.Text = string.Empty;

            }
            else
            {
                this.txtLineNoEdit.Text = ((InvTransferDetail)obj).TransferLine.ToString();
                this.txtItemIDEdit.Text = ((InvTransferDetail)obj).ItemCode.ToString();
                this.txtPlanQTYEdit.Text = ((InvTransferDetail)obj).Planqty.ToString();
                this.txtOrderCodeEdit.Text = ((InvTransferDetail)obj).OrderNO.ToString();
                this.txtOrderLine.Text = ((InvTransferDetail)obj).OrderLine.ToString();
                this.txtCustomerCode.Text = ((InvTransferDetail)obj).CustomerCode.ToString();
                this.txtMOCode.Text = ((InvTransferDetail)obj).MOCode.ToString();
                this.txtRemark.Text = ((InvTransferDetail)obj).Memo.ToString();

            }
        }

        protected object GetEditObject()
        {
            if (_TransferFacade == null)
            {
                _TransferFacade = new WarehouseFacade(this.DataProvider);
            }
            DBDateTime DBDateTimeNow = FormatHelper.GetNowDBDateTime(this.DataProvider);
            InvTransferDetail transfer = new InvTransferDetail();
            if (ViewState["operate"].ToString() == "Edit")
            {
                transfer = (InvTransferDetail)this._TransferFacade.GetInvTransferDetail(FormatHelper.CleanString(this.txtTransferNo.Text), int.Parse(FormatHelper.CleanString(this.txtLineNoEdit.Text)));
                if (transfer == null)
                {
                    return null;
                }
            }
            transfer.TransferNO = FormatHelper.CleanString(this.txtTransferNo.Text, 40);
            if (!String.IsNullOrEmpty(this.txtLineNoEdit.Text))
            {
                transfer.TransferLine = System.Int32.Parse(FormatHelper.CleanString(this.txtLineNoEdit.Text, 8));
            }
            transfer.ItemCode = FormatHelper.CleanString(this.txtItemIDEdit.Text);
            if (!String.IsNullOrEmpty(this.txtPlanQTYEdit.Text))
            {
                transfer.Planqty = System.Decimal.Parse(FormatHelper.CleanString(this.txtPlanQTYEdit.Text, 22));
            }
            transfer.OrderNO = FormatHelper.CleanString(this.txtOrderCodeEdit.Text);
            if (!String.IsNullOrEmpty(this.txtOrderLine.Text))
            {
                transfer.OrderLine = System.Int32.Parse(FormatHelper.CleanString(this.txtOrderLine.Text, 8));
            }
            transfer.CustomerCode = FormatHelper.CleanString(this.txtCustomerCode.Text, 40);
            transfer.MOCode = FormatHelper.CleanString(this.txtMOCode.Text, 40);
            transfer.Memo = FormatHelper.CleanString(this.txtRemark.Text, 2000);

            transfer.Mdate = DBDateTimeNow.DBDate;
            transfer.Mtime = DBDateTimeNow.DBTime;
            transfer.Muser = this.GetUserCode();

            return transfer;
        }

        private object GetEditObject(GridRecord row)
        {
            if (_TransferFacade == null)
            {
                _TransferFacade = new WarehouseFacade(this.DataProvider);
            }
            object obj = _TransferFacade.GetInvTransferDetail(this.txtTransferNo.Text.Trim().ToUpper(), int.Parse(row.Items.FindItemByKey("TransferLine").Text.Trim().ToUpper()));

            if (obj != null)
            {
                return (InvTransferDetail)obj;
            }
            return null;
        }

        #endregion

        #region For Export To Excel

        protected void cmdGridExport_ServerClick(object sender, EventArgs e)
        {
            this.excelExporter.Export();
        }

        private string[] FormatExportRecord(object obj)
        {
            return new string[]{
                                ((InvTransferDetailForQuey)obj).TransferNO.ToString(),
			                    ((InvTransferDetailForQuey)obj).OrderNO.ToString(),
                    			((InvTransferDetailForQuey)obj).OrderLine.ToString(),
                    			((InvTransferDetailForQuey)obj).ItemCode.ToString(),
                                ((InvTransferDetailForQuey)obj).MaterialDescription.ToString(),
                    			((InvTransferDetailForQuey)obj).Planqty.ToString(),
                                ((InvTransferDetailForQuey)obj).CustomerCode.ToString(),
                                ((InvTransferDetailForQuey)obj).CustomerName.ToString(),
                                ((InvTransferDetailForQuey)obj).MOCode.ToString(),
                                ((InvTransferDetailForQuey)obj).Memo.ToString(), 

                                
            };
        }


        private string[] GetColumnHeaderText()
        {
            return new string[] {	
                "TransferNO",
                "OrderNO",
                "OrderLine",
                "ItemID",
                "MDESC",
                "PlanQTYEdit",
                "CustomerCode",
                "CustomerName",
                "MOCode",
                "Memo",
                
            };
        }

        #endregion
    }
}



