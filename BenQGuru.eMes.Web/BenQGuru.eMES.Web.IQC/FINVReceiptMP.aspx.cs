using System;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.IQC;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    /// <summary>
    /// FRouteMP 的摘要说明。



    /// </summary>
    public partial class FINVReceiptMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        protected System.Web.UI.WebControls.Label lblResourceTitle;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
        protected System.Web.UI.WebControls.TextBox txtShiftTypeEdit;
        private SystemSettingFacade _SystemSettingFacade = null;
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
                this.BuildTicketType();
                this.BuildTicketStatus();
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
            this.gridHelper.AddColumn("ReceiveNo", "入库单号", null);
            this.gridHelper.AddColumn("TicketType", "单据类型", null);
            this.gridHelper.AddColumn("TicketStatus", "单据状态", null);
            this.gridHelper.AddColumn("Storage", "库别", null);
            this.gridHelper.AddColumn("StorageDesc", "库别名称", null);
            this.gridHelper.AddColumn("VendorCode", "供应商代码", null);
            this.gridHelper.AddColumn("VendorName", "供应商名称", null);
            this.gridHelper.AddColumn("IsAllInStorage", "是否都入库", null);
            this.gridHelper.AddColumn("Memo", "备注", null);
            this.gridHelper.AddColumn("CreateUser", "创建人", null);
            this.gridHelper.AddColumn("CreatDate", "创建日期", null);
            this.gridHelper.AddLinkColumn("Detail", "详细", null);
            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = DtSource.NewRow();
            row["ReceiveNo"] = ((Domain.IQC.InvReceiptForQuery)obj).Receiptno;
            row["TicketType"] = this.languageComponent1.GetString(((Domain.IQC.InvReceiptForQuery)obj).Rectype);
            row["TicketStatus"] = this.languageComponent1.GetString(((Domain.IQC.InvReceiptForQuery)obj).Recstatus);
            row["Storage"] = ((Domain.IQC.InvReceiptForQuery)obj).Storageid;
            row["StorageDesc"] = ((Domain.IQC.InvReceiptForQuery)obj).StorageName;
            row["VendorCode"] = ((Domain.IQC.InvReceiptForQuery)obj).Vendorcode;
            row["VendorName"] = ((Domain.IQC.InvReceiptForQuery)obj).VendorName;
            row["IsAllInStorage"] = ((Domain.IQC.InvReceiptForQuery)obj).IsAllInStorage;
            row["Memo"] = ((Domain.IQC.InvReceiptForQuery)obj).Memo;
            row["CreateUser"] = ((Domain.IQC.InvReceiptForQuery)obj).GetDisplayText("Createuser");
            row["CreatDate"] = FormatHelper.ToDateString(((Domain.IQC.InvReceiptForQuery)obj).Createdate);
            row["Detail"] = "";
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_iqcfacade == null)
            {
                _iqcfacade = new BenQGuru.eMES.IQC.IQCFacade(base.DataProvider);
            }
            return this._iqcfacade.GetINVReceipt(
               FormatHelper.CleanString(this.txtReceiveNoQuery.Text),
               FormatHelper.CleanString(this.txtMaterialCodeQuery.Text),
               FormatHelper.CleanString(this.drpTicketTypeQuery.SelectedValue),
               FormatHelper.CleanString(this.txtVendorCodeQuery.Text),
              FormatHelper.TODateInt(this.dateCreateDateStart.Text),
              FormatHelper.TODateInt(this.dateCreateDateEnd.Text),
               FormatHelper.CleanString(this.drpTicketStatus.SelectedValue),
               FormatHelper.CleanString(this.txtStorageQuery.Text),
               OrgId, inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_iqcfacade == null)
            {
                _iqcfacade = new BenQGuru.eMES.IQC.IQCFacade(base.DataProvider);
            }
            return this._iqcfacade.GetINVReceiptRowCount(
               FormatHelper.CleanString(this.txtReceiveNoQuery.Text),
               FormatHelper.CleanString(this.txtMaterialCodeQuery.Text),
               FormatHelper.CleanString(this.drpTicketTypeQuery.SelectedValue),
               FormatHelper.CleanString(this.txtVendorCodeQuery.Text),
               FormatHelper.TODateInt(this.dateCreateDateStart.Text),
               FormatHelper.TODateInt(this.dateCreateDateEnd.Text),
               FormatHelper.CleanString(this.drpTicketStatus.SelectedValue),
               FormatHelper.CleanString(this.txtStorageQuery.Text),
               OrgId
                 );

        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "Detail")
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FInvReceiptDetail.aspx",
                        new string[] { "ReceiptNo" },
                        new string[] { row.Items.FindItemByKey("ReceiveNo").Value.ToString() }));
            }
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (_iqcfacade == null)
            {
                _iqcfacade = new BenQGuru.eMES.IQC.IQCFacade(base.DataProvider);
            }
            //新增前唯一性检查

            int count = this._iqcfacade.GetINVReceiptRepeateCount(FormatHelper.CleanString(this.txtReceiveNoEdit.Text.Trim()), OrgId);

            if (count > 0)
            {
                WebInfoPublish.Publish(this, "$BS_ALREADY_INVReceipt", this.languageComponent1);
                return;
            }
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            ((Domain.IQC.InvReceipt)domainObject).Recstatus = "NEW";
            ((Domain.IQC.InvReceipt)domainObject).Createdate = dbDateTime.DBDate;
            ((Domain.IQC.InvReceipt)domainObject).Createtime = dbDateTime.DBTime;
            ((Domain.IQC.InvReceipt)domainObject).Createuser = this.GetUserCode();
            ((Domain.IQC.InvReceipt)domainObject).Mdate = dbDateTime.DBDate;
            ((Domain.IQC.InvReceipt)domainObject).Mtime = dbDateTime.DBTime;
            ((Domain.IQC.InvReceipt)domainObject).Muser = this.GetUserCode();
            ((Domain.IQC.InvReceipt)domainObject).IsAllInStorage = "N";
            this._iqcfacade.AddInvReceipt((Domain.IQC.InvReceipt)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_iqcfacade == null)
            {
                _iqcfacade = new BenQGuru.eMES.IQC.IQCFacade(base.DataProvider);
            }
            foreach (Domain.IQC.InvReceipt invreceipt in (Domain.IQC.InvReceipt[])domainObjects.ToArray(typeof(Domain.IQC.InvReceipt)))
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
                object[] objs = this._iqcfacade.QueryINVReceiptDetail2ReceiptNo(invreceipt.Receiptno);
                if (objs != null)
                {
                    foreach (Domain.IQC.InvReceiptDetail invreceiptdetail in objs)
                    {
                        this._iqcfacade.DeleteInvReceiptDetail(invreceiptdetail);
                    }
                }


            }

            this._iqcfacade.DeleteInvReceipt((Domain.IQC.InvReceipt[])domainObjects.ToArray(typeof(Domain.IQC.InvReceipt)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {

            if (_iqcfacade == null)
            {
                _iqcfacade = new BenQGuru.eMES.IQC.IQCFacade(base.DataProvider);
            }




            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            ((Domain.IQC.InvReceipt)domainObject).Mdate = dbDateTime.DBDate;
            ((Domain.IQC.InvReceipt)domainObject).Mtime = dbDateTime.DBTime;
            ((Domain.IQC.InvReceipt)domainObject).Muser = this.GetUserCode();
            this._iqcfacade.UpdateInvReceipt((Domain.IQC.InvReceipt)domainObject);

        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Update)
            {

                //this.drpTicketTypeEdit.Enabled = false;
                this.txtReceiveNoEdit.ReadOnly = true;
            }
            if (pageAction == PageActionType.Add)
            {

                this.drpTicketTypeEdit.Enabled = true;
                this.txtReceiveNoEdit.ReadOnly = false;
            }
        }
        #endregion

        #region Object <--> Page
        protected override object GetEditObject()
        {
            if (_iqcfacade == null)
            {
                _iqcfacade = new BenQGuru.eMES.IQC.IQCFacade(base.DataProvider);
            }
            string ReceiptNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtReceiveNoEdit.Text.ToUpper(), 40));
            object obj = this._iqcfacade.GetINVRecepitForUpdate(ReceiptNo);
            Domain.IQC.InvReceipt invreceipt = (Domain.IQC.InvReceipt)obj;
            if (invreceipt == null)
            {
                invreceipt = this._iqcfacade.CreateNewInvReceipt();
                invreceipt.Receiptno = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtReceiveNoEdit.Text.Trim().ToUpper(), 40));
                invreceipt.Rectype = FormatHelper.CleanString(this.drpTicketTypeEdit.SelectedValue.Trim(), 40);
                invreceipt.Vendorcode = FormatHelper.CleanString(this.txtVendorCodeEdit.Text.Trim(), 40);
                if (invreceipt.Vendorcode == "")
                {
                    invreceipt.Vendorcode = " ";
                }
                invreceipt.Storageid = FormatHelper.CleanString(this.txtStorageEdit.Text.Trim(), 40);
                invreceipt.Muser = this.GetUserCode();
                invreceipt.Orgid = OrgId;
                invreceipt.Memo = FormatHelper.CleanString(this.txtDescription.Text.Trim(), 2000);
                invreceipt.Recstatus = FormatHelper.CleanString(this.txtTicketStatus.Text, 40);

                return invreceipt;
            }
            else
            {
                invreceipt.Receiptno = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtReceiveNoEdit.Text.Trim().ToUpper(), 40));
                invreceipt.Rectype = FormatHelper.CleanString(this.drpTicketTypeEdit.SelectedValue.Trim(), 40);
                invreceipt.Vendorcode = FormatHelper.CleanString(this.txtVendorCodeEdit.Text.Trim(), 40);
                if (invreceipt.Vendorcode == "")
                {
                    invreceipt.Vendorcode = " ";
                }
                invreceipt.Storageid = FormatHelper.CleanString(this.txtStorageEdit.Text.Trim(), 40);
                invreceipt.Muser = this.GetUserCode();
                invreceipt.Orgid = OrgId;
                invreceipt.Memo = FormatHelper.CleanString(this.txtDescription.Text.Trim(), 2000);
                invreceipt.Recstatus = FormatHelper.CleanString(this.txtTicketStatus.Text, 40);

                return invreceipt;

            }
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (row.Items.FindItemByKey("TicketStatus").Value.ToString() == "关闭")
            {
                WebInfoPublish.Publish(this, "$BS_TicketStatus_IsClose", this.languageComponent1);
                return null;
            }



            if (_iqcfacade == null)
            {
                _iqcfacade = new BenQGuru.eMES.IQC.IQCFacade(base.DataProvider);
            }

            object objASN = _iqcfacade.GetASN(row.Items.FindItemByKey("ReceiveNo").Value.ToString());
            if (objASN != null)
            {
                WebInfoPublish.Publish(this, "$BS_ReNo_Create_ANSNO $InvReceiptNO:" + row.Items.FindItemByKey("ReceiveNo").Value.ToString(), this.languageComponent1);
                return null;
            }


            //object[] objs = _iqcfacade.QueryINVReceiptDetail2ReceiptNo(row.Cells[1].Value.ToString());
            //if (objs != null)
            //{
            //    WebInfoPublish.Publish(this, "$BS_ReNo_Create_Detail $InvReceiptNO:" + row.Cells[1].Value.ToString(), this.languageComponent1);
            //    return null;
            //}


            object obj = this._iqcfacade.GetINVRecepitForUpdate(row.Items.FindItemByKey("ReceiveNo").Value.ToString(), OrgId);
            if (obj != null)
            {
                return (Domain.IQC.InvReceipt)obj;
            }
            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtReceiveNoEdit.Text = "";
                this.drpTicketTypeEdit.Text = "";
                this.txtVendorCodeEdit.Text = "";
                this.txtStorageEdit.Text = "";
                this.txtDescription.Text = "";
                this.txtTicketStatus.Text = "";
                return;
            }

            this.txtReceiveNoEdit.Text = ((Domain.IQC.InvReceipt)obj).Receiptno.ToString();
            this.drpTicketTypeEdit.SelectedValue = ((Domain.IQC.InvReceipt)obj).Rectype.ToString();
            this.txtVendorCodeEdit.Text = ((Domain.IQC.InvReceipt)obj).Vendorcode.ToString();
            this.txtStorageEdit.Text = ((Domain.IQC.InvReceipt)obj).Storageid.ToString();
            this.txtDescription.Text = ((Domain.IQC.InvReceipt)obj).Memo.ToString();
            this.txtTicketStatus.Text = ((Domain.IQC.InvReceipt)obj).Recstatus.ToString();
        }


        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblReceiveNoEdit, this.txtReceiveNoEdit, 40, true));
            manager.Add(new LengthCheck(this.lblTicketTypeEdit, this.drpTicketTypeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblVendorCodeEdit, this.txtVendorCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblStorageEdit, this.txtStorageEdit, 40, true));

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
            return new string[]{   
                                   ((Domain.IQC.InvReceiptForQuery)obj ).Receiptno,
                                   this.languageComponent1 .GetString (((Domain.IQC.InvReceiptForQuery )obj ).Rectype) ,
                                   this.languageComponent1 .GetString (((Domain.IQC.InvReceiptForQuery )obj ).Recstatus) ,
                                   ((Domain.IQC.InvReceiptForQuery )obj ).Storageid ,
                                   ((Domain.IQC.InvReceiptForQuery )obj ).Vendorcode ,
                                   ((Domain.IQC.InvReceiptForQuery )obj ).VendorName ,
                                   ((Domain.IQC.InvReceiptForQuery )obj ).IsAllInStorage ,
                                   ((Domain.IQC.InvReceiptForQuery )obj ).Memo ,
                                   ((Domain.IQC.InvReceiptForQuery )obj ).GetDisplayText("Createuser"),
                                   FormatHelper.ToDateString(((Domain.IQC.InvReceiptForQuery )obj).Createdate)
                                        };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"Receiptno",
                                    "Rectype",
                                    "Recstatus",
                                    "Storageid",
                                    "Vendorcode",
                                    "VendorName",
                                    "IsAllInStorage",
                                    "Memo",
                                    "CreateUser",
                                    "Createdate"
            };
        }


        #endregion

        #region private
        private void BuildTicketType()
        {
            this.drpTicketTypeQuery.Items.Add(new ListItem("", ""));
            this.drpTicketTypeQuery.Items.Add(new ListItem("外购", "P"));
            this.drpTicketTypeQuery.Items.Add(new ListItem("外协", "WX"));
            this.drpTicketTypeQuery.Items.Add(new ListItem("其他", "O"));

            this.drpTicketTypeEdit.Items.Add(new ListItem("", ""));
            this.drpTicketTypeEdit.Items.Add(new ListItem("外购", "P"));
            this.drpTicketTypeEdit.Items.Add(new ListItem("外协", "WX"));
            this.drpTicketTypeEdit.Items.Add(new ListItem("其他", "O"));

        }

        private void BuildTicketStatus()
        {
            this.drpTicketStatus.Items.Add(new ListItem("", ""));
            this.drpTicketStatus.Items.Add(new ListItem("新建", "NEW"));
            this.drpTicketStatus.Items.Add(new ListItem("等待检验", "WaitCheck"));
            this.drpTicketStatus.Items.Add(new ListItem("关闭", "Close"));
        }

        public int OrgId
        {
            get
            {
                return GlobalVariables.CurrentOrganizations.First().OrganizationID;
            }

        }

        #endregion

    }
}