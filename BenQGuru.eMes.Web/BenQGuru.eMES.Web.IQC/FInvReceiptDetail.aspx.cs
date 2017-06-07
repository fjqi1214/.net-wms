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
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.IQC;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Domain.IQC;
//using BenQGuru.eMES

using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    /// <summary>
    /// FRouteMP 的摘要说明。



    /// </summary>
    public partial class FInvReceiptDetail : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        protected System.Web.UI.WebControls.Label lblResourceTitle;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
        protected System.Web.UI.WebControls.TextBox txtShiftTypeEdit;

        private IQCFacade _iqcfacade = null;
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
                this.txtReceiveNoQuery.Text = this.GetRequestParam("ReceiptNo");
                this.txtReceiveNoQuery.ReadOnly = true;
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
            this.gridHelper.AddColumn("ReceiptLine", "行号", null);
            this.gridHelper.AddColumn("OrderNo", "采购单号", null);
            this.gridHelper.AddColumn("OrderLine", "采购单行号", null);
            this.gridHelper.AddColumn("VenderLotNO", "厂商批号", null);
            this.gridHelper.AddColumn("MaterialCode", "物料代码", null);
            this.gridHelper.AddColumn("MaterialDesc", "物料描述", null);
            this.gridHelper.AddColumn("MMachineType", "型号", null);
            this.gridHelper.AddColumn("PlanQty", "计划数量", null);
            this.gridHelper.AddColumn("InvUser", "保管员", null);//added by Jarvis
            this.gridHelper.AddColumn("Memo", "备注", null);
            this.gridHelper.AddColumn("IsInStorage", "是否入库", null);

            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            if (!IsPostBack)
            {
                this.gridHelper.RequestData();
            }
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["ReceiptLine"] = ((InvReceiptDetailForQuery)obj).Receiptline;
            row["OrderNo"] = ((InvReceiptDetailForQuery)obj).Orderno;
            row["OrderLine"] = ((InvReceiptDetailForQuery)obj).Orderline;
            row["VenderLotNO"] = ((InvReceiptDetailForQuery)obj).VenderLotNO;
            row["MaterialCode"] = ((InvReceiptDetailForQuery)obj).MaterialCode;
            row["MaterialDesc"] = ((InvReceiptDetailForQuery)obj).MaterialDescription;
            row["MMachineType"] = ((InvReceiptDetailForQuery)obj).MaterialMachineType;
            row["PlanQty"] = ((InvReceiptDetailForQuery)obj).Planqty;
            row["InvUser"] = ((InvReceiptDetailForQuery)obj).GetDisplayText("InvUser");//added by Jarvis
            row["Memo"] = ((InvReceiptDetailForQuery)obj).Memo;
            row["IsInStorage"] = ((InvReceiptDetailForQuery)obj).IsInStorage;
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_iqcfacade == null)
            {
                _iqcfacade = new BenQGuru.eMES.IQC.IQCFacade(base.DataProvider);
            }
            return this._iqcfacade.GetInvReceiptDetailForQuery(
               FormatHelper.CleanString(this.txtReceiveNoQuery.Text),
               OrgId, inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_iqcfacade == null)
            {
                _iqcfacade = new BenQGuru.eMES.IQC.IQCFacade(base.DataProvider);
            }

            return this._iqcfacade.GetInvReceiptDetailCount(
               FormatHelper.CleanString(this.txtReceiveNoQuery.Text),
                OrgId);

        }
        #endregion

        #region Button

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_iqcfacade == null)
            {
                _iqcfacade = new BenQGuru.eMES.IQC.IQCFacade(base.DataProvider);
            }

            foreach (Domain.IQC.InvReceiptDetail invreceipt in (Domain.IQC.InvReceiptDetail[])domainObjects.ToArray(typeof(Domain.IQC.InvReceiptDetail)))
            {
                if (invreceipt.Recstatus.ToUpper() != "NEW")
                {
                    WebInfoPublish.Publish(this, "$BS_TicketStatus_Error", this.languageComponent1);
                    return;
                }

                object obj = _iqcfacade.GetASN(invreceipt.Receiptno);
                if (obj != null)
                {
                    WebInfoPublish.Publish(this, "$BS_ReNo_Create_ANSNO $InvReceiptNO:" + invreceipt.Receiptno, this.languageComponent1);
                    return;
                }
                //删除字表
            }
            this._iqcfacade.DeleteInvReceiptDetail((Domain.IQC.InvReceiptDetail[])domainObjects.ToArray(typeof(Domain.IQC.InvReceiptDetail)));
        }

        protected override void AddDomainObject(object domainObject)
        {
            if (_iqcfacade == null)
            {
                _iqcfacade = new BenQGuru.eMES.IQC.IQCFacade(base.DataProvider);
            }
            //单据已经关闭的TBLINVReceipt. RECSTATUS不能新增行项目

            string ReceiptNo = this.txtReceiveNoQuery.Text.Trim();
            int ReceiptLine = int.Parse(this.txtReceiveLineEdit.Text.Trim());
            object obj = _iqcfacade.GetINVRecepitForUpdate(ReceiptNo, OrgId);
            if (((InvReceipt)obj).Recstatus == "CLOSE")
            {
                WebInfoPublish.Publish(this, "$BS_TicketStatus_IsClose_CannotAdd", this.languageComponent1);
                return;
            }

            ((InvReceiptDetail)domainObject).Qualifyqty = 0;
            ((InvReceiptDetail)domainObject).Actqty = 0;
            ((InvReceiptDetail)domainObject).Iqcstatus = "NEW";

            //新增前唯一性检查
            int count = this._iqcfacade.GetInvReceiptDetailRepeatCount(ReceiptNo, ReceiptLine);
            if (count > 0)
            {
                WebInfoPublish.Publish(this, "$BS_ALREADY_INVReceipt", this.languageComponent1);
                return;
            }

            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            ((InvReceiptDetail)domainObject).Recstatus = "NEW";
            ((InvReceiptDetail)domainObject).Recdate = dbDateTime.DBDate;
            ((InvReceiptDetail)domainObject).Rectime = dbDateTime.DBTime;
            ((InvReceiptDetail)domainObject).Recuser = this.GetUserCode();
            ((InvReceiptDetail)domainObject).Mdate = dbDateTime.DBDate;
            ((InvReceiptDetail)domainObject).Mtime = dbDateTime.DBTime;
            ((InvReceiptDetail)domainObject).Muser = this.GetUserCode();
            ((InvReceiptDetail)domainObject).IsInStorage = "N";

            this._iqcfacade.AddInvReceiptDetail((InvReceiptDetail)domainObject);
        }

        protected override void UpdateDomainObject(object domainObject)
        {


            if (_iqcfacade == null)
            {
                _iqcfacade = new BenQGuru.eMES.IQC.IQCFacade(base.DataProvider);
            }
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            ((InvReceiptDetail)domainObject).Mdate = dbDateTime.DBDate;
            ((InvReceiptDetail)domainObject).Mtime = dbDateTime.DBTime;
            ((InvReceiptDetail)domainObject).Muser = this.GetUserCode();

            this._iqcfacade.UpdateInvReceiptDetail((InvReceiptDetail)domainObject);

        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {

            if (pageAction == PageActionType.Update)
            {

                this.txtReceiveLineEdit.ReadOnly = true;
            }
            if (pageAction == PageActionType.Add)
            {

                this.txtReceiveLineEdit.ReadOnly = false;
            }

        }
        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {

            this.Response.Redirect(this.MakeRedirectUrl("./FINVReceiptMP.aspx",
               new string[] { "ReceiptNo" },
               new string[] { this.txtReceiveNoQuery.Text }));

        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject(GridRecord row)
        {
            if (_iqcfacade == null)
            {
                _iqcfacade = new BenQGuru.eMES.IQC.IQCFacade(base.DataProvider);
            }
            string ReceiptNo = this.txtReceiveNoQuery.Text;
            int ReceiptLine = int.Parse(row.Items.FindItemByKey("ReceiptLine").Value.ToString());
            object obj = _iqcfacade.GetInvReceiptDetailForUpdate(ReceiptNo, ReceiptLine);
            if (((InvReceiptDetail)obj).Recstatus == "CLOSE" || ((InvReceiptDetail)obj).Recstatus == "WAITCHECK")
            {
                WebInfoPublish.Publish(this, "$BS_TicketStatus_CannotUpdate", this.languageComponent1);
                return null;
            }

            object objAsn = _iqcfacade.GetASN(ReceiptNo);
            if (objAsn != null)
            {
                WebInfoPublish.Publish(this, "$BS_ReNo_Create_ANSNO $InvReceiptNO:" + this.txtReceiveNoQuery.Text, this.languageComponent1);
                return null;
            }
            return obj;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtReceiveLineEdit.Text = "";
                this.txtMaterialCodeEdit.Text = "";
                this.txtPlanQtyEdit.Text = "";
                this.txtOrderLineEdit.Text = "";
                this.txtOrderNoEdit.Text = "";
                this.txtMemoEdit.Text = "";
                this.txtTicketStatus.Text = "";
                this.txtManagerCode.Text = "";//added by Jarvis
                return;
            }
            this.txtReceiveLineEdit.Text = ((InvReceiptDetail)obj).Receiptline.ToString();
            this.txtMaterialCodeEdit.Text = ((InvReceiptDetail)obj).Itemcode.ToString();
            this.txtPlanQtyEdit.Text = ((InvReceiptDetail)obj).Planqty.ToString();
            this.txtOrderLineEdit.Text = ((InvReceiptDetail)obj).Orderline.ToString();
            this.txtOrderNoEdit.Text = ((InvReceiptDetail)obj).Orderno.ToString();
            this.txtMemoEdit.Text = ((InvReceiptDetail)obj).Memo.ToString();
            this.txtTicketStatus.Text = ((InvReceiptDetail)obj).Recstatus.ToString();
            this.txtManagerCode.Text = ((InvReceiptDetail)obj).InvUser.ToString();//added by Jarvis

        }

        protected override object GetEditObject()
        {
            if (_iqcfacade == null)
            {
                _iqcfacade = new BenQGuru.eMES.IQC.IQCFacade(base.DataProvider);
            }
            string ReceiptNo = FormatHelper.CleanString(this.txtReceiveNoQuery.Text);
            int ReceiptLine = 0;
            if (FormatHelper.CleanString(this.txtReceiveLineEdit.Text).Length > 0)
            {
                ReceiptLine = int.Parse(FormatHelper.CleanString(this.txtReceiveLineEdit.Text));
            }
            object obj = this._iqcfacade.GetInvReceiptDetailForUpdate(ReceiptNo, ReceiptLine);
            InvReceiptDetail invreceiptdetail = (InvReceiptDetail)obj;
            if (invreceiptdetail == null)
            {
                invreceiptdetail = this._iqcfacade.CreateNewINVReceiptDetail();

                invreceiptdetail.Receiptno = FormatHelper.CleanString(this.txtReceiveNoQuery.Text.ToUpper(), 40);
                invreceiptdetail.Receiptline = int.Parse(this.txtReceiveLineEdit.Text);
                invreceiptdetail.Itemcode = FormatHelper.CleanString(this.txtMaterialCodeEdit.Text, 40);
                invreceiptdetail.Orderline = int.Parse(this.txtOrderLineEdit.Text);
                invreceiptdetail.Orderno = FormatHelper.CleanString(this.txtOrderNoEdit.Text.ToUpper(), 40);
                invreceiptdetail.Planqty = decimal.Parse(this.txtPlanQtyEdit.Text);
                invreceiptdetail.Memo = FormatHelper.CleanString(this.txtMemoEdit.Text, 2000);
                invreceiptdetail.InvUser = FormatHelper.CleanString(this.txtManagerCode.Text, 100);//added by Jarvis
                invreceiptdetail.Muser = this.GetUserCode();
                invreceiptdetail.Recstatus = FormatHelper.CleanString(this.txtTicketStatus.Text, 40);
                return invreceiptdetail;
            }
            else
            {
                invreceiptdetail.Receiptno = FormatHelper.CleanString(this.txtReceiveNoQuery.Text.ToUpper(), 40);
                invreceiptdetail.Receiptline = int.Parse(this.txtReceiveLineEdit.Text);
                invreceiptdetail.Itemcode = FormatHelper.CleanString(this.txtMaterialCodeEdit.Text, 40);
                invreceiptdetail.Orderline = int.Parse(this.txtOrderLineEdit.Text);
                invreceiptdetail.Orderno = FormatHelper.CleanString(this.txtOrderNoEdit.Text.ToUpper(), 40);
                invreceiptdetail.Planqty = decimal.Parse(this.txtPlanQtyEdit.Text);
                invreceiptdetail.Memo = FormatHelper.CleanString(this.txtMemoEdit.Text, 2000);
                invreceiptdetail.InvUser = FormatHelper.CleanString(this.txtManagerCode.Text, 100);//added by Jarvis
                invreceiptdetail.Muser = this.GetUserCode();
                invreceiptdetail.Recstatus = FormatHelper.CleanString(this.txtTicketStatus.Text, 40);
                return invreceiptdetail;
            }
        }

        private object GetUpdateObject()
        {
            if (_iqcfacade == null)
            {
                _iqcfacade = new BenQGuru.eMES.IQC.IQCFacade(base.DataProvider);
            }
            string ReceiptNo = FormatHelper.CleanString(this.txtReceiveNoQuery.Text);
            int ReceiptLine = 0;
            if (FormatHelper.CleanString(this.txtReceiveLineEdit.Text).Length > 0)
            {
                ReceiptLine = int.Parse(FormatHelper.CleanString(this.txtReceiveLineEdit.Text));
            }
            object obj = this._iqcfacade.GetInvReceiptDetailForUpdate(ReceiptNo, ReceiptLine);
            InvReceiptDetail invreceiptdetail = (InvReceiptDetail)obj;
            if (invreceiptdetail == null)
            {
                invreceiptdetail = this._iqcfacade.CreateNewINVReceiptDetail();

                invreceiptdetail.Receiptno = FormatHelper.CleanString(this.txtReceiveNoQuery.Text.ToUpper(), 40);
                invreceiptdetail.Receiptline = int.Parse(this.txtReceiveLineEdit.Text);
                invreceiptdetail.Itemcode = FormatHelper.CleanString(this.txtMaterialCodeEdit.Text, 40);
                invreceiptdetail.Orderline = int.Parse(this.txtOrderLineEdit.Text);
                invreceiptdetail.Orderno = FormatHelper.CleanString(this.txtOrderNoEdit.Text, 40);
                invreceiptdetail.Planqty = decimal.Parse(this.txtPlanQtyEdit.Text);
                invreceiptdetail.Memo = FormatHelper.CleanString(this.txtMemoEdit.Text, 2000);
                invreceiptdetail.InvUser = FormatHelper.CleanString(this.txtManagerCode.Text, 100);//added by Jarvis
                invreceiptdetail.Muser = this.GetUserCode();
                invreceiptdetail.Recstatus = FormatHelper.CleanString(this.txtTicketStatus.Text, 2000);
                return invreceiptdetail;
            }
            else
            {
                invreceiptdetail.Receiptno = FormatHelper.CleanString(this.txtReceiveNoQuery.Text.ToUpper(), 40);
                invreceiptdetail.Receiptline = int.Parse(this.txtReceiveLineEdit.Text);
                invreceiptdetail.Itemcode = FormatHelper.CleanString(this.txtMaterialCodeEdit.Text, 40);
                invreceiptdetail.Orderline = int.Parse(this.txtOrderLineEdit.Text);
                invreceiptdetail.Orderno = FormatHelper.CleanString(this.txtOrderNoEdit.Text, 40);
                invreceiptdetail.Planqty = decimal.Parse(this.txtPlanQtyEdit.Text);
                invreceiptdetail.Memo = FormatHelper.CleanString(this.txtMemoEdit.Text, 2000);
                invreceiptdetail.InvUser = FormatHelper.CleanString(this.txtManagerCode.Text, 100);//added by Jarvis
                invreceiptdetail.Muser = this.GetUserCode();
                invreceiptdetail.Recstatus = FormatHelper.CleanString(this.txtTicketStatus.Text, 2000);
                return invreceiptdetail;
            }
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblOrderNoEd, this.txtOrderNoEdit, 40, true));
            manager.Add(new NumberCheck(this.lblReceiveLineEdit, this.txtReceiveLineEdit, Int32.MinValue, Int32.MaxValue, true));
            manager.Add(new DecimalCheck(this.lblPlanQtyEdit, txtPlanQtyEdit, decimal.MinValue, decimal.MaxValue, true));
            manager.Add(new NumberCheck(this.lblOrderLineEdit, txtOrderLineEdit, Int32.MinValue, Int32.MaxValue, true));
            manager.Add(new LengthCheck(this.lblMaterialCodeEdit, this.txtMaterialCodeEdit, 40, true));


            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }
            if (_iqcfacade == null)
            {
                _iqcfacade = new BenQGuru.eMES.IQC.IQCFacade(base.DataProvider);
            }
            object objAsn = _iqcfacade.GetASN(this.txtReceiveNoQuery.Text);
            if (objAsn != null)
            {
                WebInfoPublish.Publish(this, "$BS_ReNo_Create_ANSNO $InvReceiptNO:" + this.txtReceiveNoQuery.Text, this.languageComponent1);
                return false;
            }

            return true;
        }

        #endregion

        #region private

        public int OrgId
        {
            get
            {
                return GlobalVariables.CurrentOrganizations.First().OrganizationID;
            }

        }

        #endregion


        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{   
                ((InvReceiptDetailForQuery)obj).Receiptno.ToString(),
                                   ((InvReceiptDetailForQuery)obj).Receiptline.ToString()  ,
                                   ((InvReceiptDetailForQuery)obj).Orderno ,
                                   ((InvReceiptDetailForQuery)obj).Orderline.ToString() ,
                                   ((InvReceiptDetailForQuery)obj).VenderLotNO ,
                                   ((InvReceiptDetailForQuery)obj).MaterialCode ,
                                   ((InvReceiptDetailForQuery)obj).MaterialDescription ,
                                   ((InvReceiptDetailForQuery)obj).MaterialMachineType ,
                                   ((InvReceiptDetailForQuery)obj).Planqty.ToString() ,
                                   ((InvReceiptDetailForQuery)obj).InvUser.ToString() ,//added by Jarvis
                                   ((InvReceiptDetailForQuery)obj).Memo,
                                   ((InvReceiptDetailForQuery)obj).IsInStorage
                                        };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"Receiptno",
                                    "Receiptline",
                                    "Orderno",
                                    "Orderline",
                                    "VenderLotNO",
                                    "MaterialCode",
                                    "MaterialDescription",
                                    "MaterialMachineType",
                                    "Planqty",
                                    "InvUser",//added by Jarvis
                                    "Memo",
                                    "IsInStorage"
            };
        }


        #endregion

    }
}