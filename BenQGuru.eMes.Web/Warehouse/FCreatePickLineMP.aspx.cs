using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Warehouse;

namespace BenQGuru.eMES.Web.Warehouse
{
    /// <summary>
    /// FCreatePickHeadMP 的摘要说明。
    /// </summary>
    public partial class FCreatePickLineMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private InventoryFacade _InventoryFacade = null;
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
            InitHander();
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.txtPickNoQuery.Text = this.GetRequestParam("PickNo");
                txtPickNoQuery1.Text = this.GetRequestParam("PickNo");
                lblMOSelectMOViewField.Visible = false;
                if (facade == null)
                {
                    facade = new InventoryFacade(base.DataProvider);
                }
                Pick pick = (Pick)facade.GetPick(this.txtPickNoQuery.Text);
                txtPickTypeQuery.Text = pick.PickType;
                txtInvNoEidt.Text = pick.InvNo;
                if (pick.PickType == PickType.PickType_BFC)//BFC：报废
                {
                    txtDQMCodeEdit.Visible = false;
                    txtIsScrapEdit.Text = "Y";
                    txtStorageCodeEidt.Text = pick.StorageCode;
                }
                else
                {
                    txtScarpDQMCodeEdit.Visible = false;
                    txtStorageCodeEidt.Text = pick.StorageCode;
                }
                this.InitWebGrid();
                this.cmdQuery_Click(null, null);
                this.RequestData();

            }
            //TextChange
            txtScarpDQMCodeEdit.TextBox.TextChanged += new EventHandler(txtScarpDQMCodeEdit_TextChanged);
            txtDQMCodeEdit.TextBox.TextChanged += new EventHandler(txtDQMCodeEdit_TextChanged);
        }

        protected void txtScarpDQMCodeEdit_TextChanged(object sender, EventArgs e)
        {
            ScarpDqmCodeChange();
            //cmdQuery_Click(null, null);
        }

        protected void txtDQMCodeEdit_TextChanged(object sender, EventArgs e)
        {
            ItemFacade itemFacade = new ItemFacade();
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            txtInvLineEidt.Text = "";
            if (!string.IsNullOrEmpty(this.txtDQMCodeEdit.Text))
            {
                if (!IsInt(this.txtDQMCodeEdit.Text))
                {
                    return;
                }
                txtInvLineEidt.Text = this.txtDQMCodeEdit.Text;
                int invline = Convert.ToInt32(this.txtDQMCodeEdit.Text);
                string invno = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtInvNoEidt.Text, 40));
                InvoicesDetail invoicesDetail = (InvoicesDetail)facade.GetInvoicesDetail(invno, invline);
                if (invoicesDetail != null)
                {
                    string dqmcode = invoicesDetail.DQMCode;
                    this.txtDQMCodeEdit.Text = dqmcode;
                    this.txtCustmCodeEdit.Text = invoicesDetail.CustmCode;
                    //FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDQMCodeEdit.Text, 40));
                    Domain.MOModel.Material material = (Domain.MOModel.Material)itemFacade.GetMaterialByDQMCode(dqmcode);
                    if (material != null)
                    { txtMDescEdit.Text = material.MchlongDesc; }
                    else
                    {
                        txtMDescEdit.Text = " ";
                    }
                }
            }

        }


        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        #region
        private void ScarpDqmCodeChange()
        {
            ItemFacade itemFacade = new ItemFacade();
            string dqmcode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtScarpDQMCodeEdit.Text, 40));
            Domain.MOModel.Material material = (Domain.MOModel.Material)itemFacade.GetMaterialByDQMCode(dqmcode);
            if (material != null)
            { txtMDescEdit.Text = material.MchlongDesc; }
            else
            {
                txtMDescEdit.Text = " ";
            }
        }
        #endregion

        #region 默认查询
        private void RequestData()
        {

            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);

            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }
        private void InitHander()
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.GetRowCountHandle = new GetRowCountDelegateNew(this.GetRowCount);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.buttonHelper = new ButtonHelper(this);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);


            #region Exporter
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            this.excelExporter.Page = this;
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
            #endregion

        }
        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        #endregion

        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            for (int i = 0; i < this.PickHeadViewFieldList.Length; i++)
            {
                this.gridHelper.AddColumn(this.PickHeadViewFieldList[i].FieldName, /*this.languageComponent1.GetString(*/this.PickHeadViewFieldList[i].Description/*)*/, null);
            }
            this.gridHelper.AddDefaultColumn(true, true);
            this.gridHelper.AddLinkColumn("LinkDetailMaterial", "已拣明细");
            this.gridWebGrid.Columns.FromKey("PickNo").Hidden = true;

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();

            PickDetailQuery pickLine = obj as PickDetailQuery;
            Type type = pickLine.GetType();
            for (int i = 0; i < this.PickHeadViewFieldList.Length; i++)
            {
                ViewField field = this.PickHeadViewFieldList[i];
                string strValue = string.Empty;
                System.Reflection.FieldInfo fieldInfo = type.GetField(field.FieldName);
                if (fieldInfo != null)
                {
                    strValue = fieldInfo.GetValue(pickLine).ToString();
                }
                if (field.FieldName == "Status")
                {
                    strValue = languageComponent1.GetString(pickLine.Status);
                }
                if (field.FieldName == "QTY")//2位小数
                {
                    strValue = pickLine.QTY.ToString("0.00");
                }
                if (field.FieldName == "OutStorageCode")
                {
                    strValue = pickLine.StorageCode;
                }
                row[i + 1] = strValue;
            }

            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            return this._InventoryFacade.QueryPickDetail(
             FormatHelper.CleanString(this.txtPickNoQuery.Text),
             FormatHelper.CleanString(this.txtDQMCodeQuery.Text),
             "", "", "", inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            return this._InventoryFacade.QueryPickDetailCount(
                  FormatHelper.CleanString(this.txtPickNoQuery.Text),
                  FormatHelper.CleanString(this.txtDQMCodeQuery.Text),
                  "", "", ""
               );
        }

        #endregion

        #region Button
        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            if (commandName == "LinkDetailMaterial")
            {
                string pickNo = row.Items.FindItemByKey("PickNo").Text.Trim();
                Response.Redirect(this.MakeRedirectUrl("FCreateCheckedPickLineMP.aspx",
                         new string[] { "PickNo" },
                         new string[] { pickNo }));
            }
        }
        #endregion

        #region ToolButton

        #region 返回
        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {

            Response.Redirect(this.MakeRedirectUrl("FCreatePickHeadMP.aspx",
                             new string[] { "PickNo" },
                             new string[] { txtPickNoQuery.Text.Trim().ToUpper()
                                        
                                    }));


        }
        #endregion

        #region 编辑
        protected override void SetEditObject(object obj)
        {
            PickDetail pickDetail = obj as PickDetail;
            if (pickDetail == null)
            {
                this.txtDQMCodeEdit.Text = string.Empty;
                txtScarpDQMCodeEdit.Text = string.Empty;
                this.txtMDescEdit.Text = "";
                this.txtQtyEdit.Text = "";
                txtPickLineEdit.Text = "";
                txtInvLineEidt.Text = "";
                return;
            }

            ItemFacade itemFacade = new ItemFacade();
            this.txtDQMCodeEdit.Text = pickDetail.DQMCode;
            txtScarpDQMCodeEdit.Text = pickDetail.DQMCode;
            Domain.MOModel.Material material = (Domain.MOModel.Material)itemFacade.GetMaterial(pickDetail.MCode);
            if (material != null)
            {
                this.txtMDescEdit.Text = material.MchlongDesc;
            }
            this.txtQtyEdit.Text = pickDetail.QTY.ToString("0.00");
            txtPickLineEdit.Text = pickDetail.PickLine;
            txtInvLineEidt.Text = pickDetail.InvLine.ToString();
        }

        #endregion

        #region 保存
        protected override object GetEditObject()
        {
            if (this.ValidateInput())
            {
                if (facade == null)
                {
                    facade = new InventoryFacade(base.DataProvider);
                }
                ItemFacade itemFacade = new ItemFacade();
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                string pickno = FormatHelper.CleanString(this.txtPickNoQuery.Text, 40);
                string pickline = FormatHelper.CleanString(this.txtPickLineEdit.Text, 40);
                string dqmcode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDQMCodeEdit.Text, 40));
                string custMCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCustmCodeEdit.Text, 40));
                if (txtPickTypeQuery.Text == PickType.PickType_BFC) //BFC：报废
                {
                    dqmcode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtScarpDQMCodeEdit.Text, 40));
                }

                string mdesc = FormatHelper.CleanString(this.txtMDescEdit.Text);
                int invline = 0;
                PickDetail pickDetail = (PickDetail)facade.GetPickDetail(pickno, pickline);
                if (pickDetail == null)
                {
                    pickDetail = new PickDetail();
                    int maxpickline = facade.GetMaxPickLine1(pickno);
                    pickDetail.PickNo = pickno;

                    pickDetail.PickLine = (maxpickline + 1).ToString();


                    #region MCode
                    Domain.MOModel.Material material = (Domain.MOModel.Material)itemFacade.GetMaterialByDQMCode(dqmcode);
                    if (material != null)
                    {
                        pickDetail.MCode = material.MCode;
                        pickDetail.Unit = material.Muom;
                    }
                    else
                    {
                        pickDetail.MCode = " ";
                        pickDetail.Unit = string.Empty;
                    }
                    #endregion
                    //根据鼎桥物料编码在物料表(TBLMATERIAL)带出TBLMATERIAL.MCODE值
                    pickDetail.MaintainUser = this.GetUserCode();
                    pickDetail.MaintainDate = dbDateTime.DBDate;
                    pickDetail.MaintainTime = dbDateTime.DBTime;
                    pickDetail.CUser = this.GetUserCode();
                    pickDetail.CDate = dbDateTime.DBDate;
                    pickDetail.CTime = dbDateTime.DBTime;
                    pickDetail.Status = PickHeadStatus.PickHeadStatus_Release;//Release:初始化
                    //pickDetail.CustMCode = custMCode;
                }

                if (!string.IsNullOrEmpty(txtInvLineEidt.Text))
                {
                    invline = Convert.ToInt32(this.txtInvLineEidt.Text);
                    pickDetail.InvLine = invline;
                }
                pickDetail.MDesc = mdesc;
                pickDetail.DQMCode = dqmcode;
                pickDetail.CustMCode = custMCode;
                string qty = FormatHelper.CleanString(this.txtQtyEdit.Text, 40);
                if (!string.IsNullOrEmpty(qty))
                {
                    if (IsDecimal(qty))
                    {
                        pickDetail.QTY = decimal.Parse((decimal.Parse(qty)).ToString("0.00"));
                    }
                }
                else
                {
                    pickDetail.QTY = 0;
                }
                return pickDetail;
            }
            else
            {
                return null;
            }
        }


        protected override bool ValidateInput()
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            PageCheckManager manager = new PageCheckManager();
            if (txtIsScrapEdit.Text == "Y")
            {
                manager.Add(new LengthCheck(this.lblDQMCodeEdit, this.txtScarpDQMCodeEdit, 40, true));
            }
            else
            {
                manager.Add(new LengthCheck(this.lblDQMCodeEdit, this.txtDQMCodeEdit, 40, true));
            }
            //manager.Add(new NumberCheck(this.lblQty, this.txtQtyEdit, 0,int.MaxValue, true));
            string qty = FormatHelper.CleanString(this.txtQtyEdit.Text, 40);
            if (!IsDecimal(qty))
            {
                WebInfoPublish.Publish(this, "数量必须为数值型", this.languageComponent1);
                return false;
            }
            decimal decqty = decimal.Parse(qty);
            if (decqty <= 0)
            {
                WebInfoPublish.Publish(this, "数量必须为大于0的数字", this.languageComponent1);
                return false;
            }
            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;


        }
        private bool IsDecimal(object obj)
        {
            try
            {
                Decimal.Parse(obj.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool IsInt(object obj)
        {
            try
            {
                Int32.Parse(obj.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
        protected override void UpdateDomainObject(object domainObject)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            var pickDetail = (PickDetail)domainObject;
            try
            {
                this.DataProvider.BeginTransaction();
                ItemFacade itemFacade = new ItemFacade();
                Domain.MOModel.Material material = (Domain.MOModel.Material)itemFacade.GetMaterial(pickDetail.MCode);
                if (material != null)
                {
                    material.MchlongDesc = this.txtMDescEdit.Text;
                    itemFacade.UpdateMaterial(material);
                }
                this.facade.UpdatePickDetail(pickDetail);
                this.DataProvider.CommitTransaction();
                WebInfoPublish.Publish(this, "更新成功", this.languageComponent1);
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            }
        }

        #endregion

        #region 新增
        //新增检查
        protected bool ValidateAddInput()
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            string pickno = FormatHelper.CleanString(this.txtPickNoQuery.Text, 40);
            string pickline = FormatHelper.CleanString(this.txtPickLineEdit.Text, 40);
            string dqmcode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDQMCodeEdit.Text, 40));
            PickDetail pick = (PickDetail)facade.GetPickDetail(pickno, pickline);
            if (pick != null)
            {
                if (pick.DQMCode == dqmcode)
                {
                    WebInfoPublish.Publish(this, "鼎桥物料编码已存在", this.languageComponent1);
                    return false;
                }
            }

            if (txtIsScrapEdit.Text != "Y")
            {
                decimal decqty = decimal.Parse(FormatHelper.CleanString(txtQtyEdit.Text));
                int invline = Convert.ToInt32(this.txtInvLineEidt.Text);
                string invno = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtInvNoEidt.Text, 40));
                InvoicesDetail invoicesDetail = (InvoicesDetail)facade.GetInvoicesDetail(invno, invline);
                if (invoicesDetail != null)
                {
                    decimal pickqty = facade.GetPickDetailQty(invno, invline);
                    if (invoicesDetail.OutQty + decqty + pickqty > invoicesDetail.PlanQty)
                    {
                        WebInfoPublish.Publish(this, "超出可领数量", this.languageComponent1);
                        return false;
                    }
                }
            }
            return true;
        }

        protected bool ValidateSaveInput()
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            string pickno = FormatHelper.CleanString(this.txtPickNoQuery.Text, 40);
            string pickline = FormatHelper.CleanString(this.txtPickLineEdit.Text, 40);
            //string dqmcode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDQMCodeEdit.Text, 40));
            PickDetail pickdetail = (PickDetail)facade.GetPickDetail(pickno, pickline);
            if (pickdetail == null)
            {
                WebInfoPublish.Publish(this, "拣货任务令行明细不存在", this.languageComponent1);
                return false;
            }
            if (txtIsScrapEdit.Text != "Y")
            {
                decimal decqty = decimal.Parse(FormatHelper.CleanString(txtQtyEdit.Text));
                int invline = Convert.ToInt32(this.txtInvLineEidt.Text);
                string invno = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtInvNoEidt.Text, 40));
                InvoicesDetail invoicesDetail = (InvoicesDetail)facade.GetInvoicesDetail(invno, invline);
                if (invoicesDetail != null)
                {
                    decimal pickqty = facade.GetPickDetailQty(invno, invline);
                    if (invoicesDetail.OutQty + decqty + pickqty - pickdetail.QTY > invoicesDetail.PlanQty)
                    {
                        WebInfoPublish.Publish(this, "超出可领数量", this.languageComponent1);
                        return false;
                    }
                }
            }
            return true;
        }
        protected override void cmdSave_Click(object sender, System.EventArgs e)
        {
            if (this.ValidateInput())
            {
                if (!this.ValidateSaveInput())
                {
                    return;
                }

                object obj = this.GetEditObject();

                if (obj == null)
                {
                    return;
                }

                this.UpdateDomainObject(obj);

                this.gridHelper.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Save);
            }
        }

        protected override void cmdAdd_Click(object sender, System.EventArgs e)
        {
            if (this.ValidateInput())
            {
                if (!this.ValidateAddInput())
                {
                    return;
                }
                object obj = this.GetEditObject();

                if (obj == null)
                {
                    return;
                }

                this.AddDomainObject(obj);

                this.gridHelper.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            }
        }

        protected override void AddDomainObject(object domainObject)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            this.facade.AddPickDetail((PickDetail)domainObject);
        }
        #endregion

        #region 删除
        protected override object GetEditObject(GridRecord row)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            object obj = facade.GetPickDetail(row.Items.FindItemByKey("PickNo").Text, row.Items.FindItemByKey("PickLine").Text);
            if (obj != null)
            {
                return obj;
            }

            return null;
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {


            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }

            foreach (PickDetail p in domainObjects)
            {
                if (facade.GetPickDetailMaterialsCount(p.PickNo, p.PickLine) > 0)
                {
                    WebInfoPublish.Publish(this, p.PickNo + ":" + p.PickLine + "正在拣料中，不能删除！", this.languageComponent1);
                    return ;
                }
            }
            var picklist = (PickDetail[])domainObjects.ToArray(typeof(PickDetail));
            this.facade.DeletePickDetail(picklist);
        }
        #endregion
        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            string[] objs = new string[this.PickHeadViewFieldList.Length];

            PickDetailQuery pickLine = obj as PickDetailQuery;
            Type type = pickLine.GetType();
            for (int i = 0; i < this.PickHeadViewFieldList.Length; i++)
            {
                ViewField field = this.PickHeadViewFieldList[i];
                string strValue = string.Empty;
                System.Reflection.FieldInfo fieldInfo = type.GetField(field.FieldName);
                if (fieldInfo != null)
                {
                    strValue = fieldInfo.GetValue(pickLine).ToString();
                }
                if (field.FieldName == "Status")
                {
                    strValue = languageComponent1.GetString(pickLine.Status);
                }
                if (field.FieldName == "QTY")//2位小数
                {
                    strValue = pickLine.QTY.ToString("0.00");
                }

                if (field.FieldName == "OutStorageCode")
                {
                    strValue = pickLine.StorageCode;
                }
                objs[i] = strValue;
            }
            return objs;
        }

        protected override string[] GetColumnHeaderText()
        {
            string[] strHeader = new string[this.PickHeadViewFieldList.Length];
            for (int i = 0; i < strHeader.Length; i++)
            {
                strHeader[i] = this.PickHeadViewFieldList[i].Description;
            }
            return strHeader;
        }

        #endregion





        private ViewField[] viewFieldList = null;
        private ViewField[] PickHeadViewFieldList
        {
            get
            {
                if (viewFieldList == null)
                {
                    if (_InventoryFacade == null)
                    {
                        _InventoryFacade = new InventoryFacade(base.DataProvider);
                    }
                    object[] objs = _InventoryFacade.QueryViewFieldByUserCode(this.GetUserCode(), "TBLCPICKDETAIL");
                    if (objs != null)
                    {
                        viewFieldList = new ViewField[objs.Length];
                        objs.CopyTo(viewFieldList, 0);
                    }
                    else
                    {
                        objs = _InventoryFacade.QueryViewFieldDefault("PickLine_FIELD_LIST_SYSTEM_DEFAULT", "TBLCPICKDETAIL");
                        if (objs != null)
                        {
                            ArrayList list = new ArrayList();
                            for (int i = 0; i < objs.Length; i++)
                            {
                                ViewField field = (ViewField)objs[i];
                                if (FormatHelper.StringToBoolean(field.IsDefault) == true)
                                {
                                    list.Add(field);
                                }
                            }
                            viewFieldList = new ViewField[list.Count];
                            list.CopyTo(viewFieldList, 0);
                        }
                    }
                    if (viewFieldList != null)
                    {
                        bool bExistPickNo = false;
                        for (int i = 0; i < viewFieldList.Length; i++)
                        {
                            if (viewFieldList[i].FieldName == "PickLine")
                            {
                                bExistPickNo = true;
                                break;
                            }
                        }
                        if (bExistPickNo == false)
                        {
                            ViewField field = new ViewField();
                            field.FieldName = "PickLine";
                            field.Description = "行号";
                            ArrayList list = new ArrayList();
                            list.Add(field);
                            list.AddRange(viewFieldList);
                            viewFieldList = new ViewField[list.Count];
                            list.CopyTo(viewFieldList, 0);
                        }
                    }
                }
                return viewFieldList;
            }
        }

    }

}
