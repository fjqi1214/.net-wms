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
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Delivery;
using BenQGuru.eMES.Common;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FDNMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private DeliveryFacade facade = null;


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

        protected void drpDNStaus_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpDNStatus.Items.Insert(0, new ListItem("", ""));

                this.drpDNStatus.Items.Insert(1, new ListItem(languageComponent1.GetString(DNStatus.StatusInit), DNStatus.StatusInit));

                this.drpDNStatus.Items.Insert(2, new ListItem(languageComponent1.GetString(DNStatus.StatusUsing), DNStatus.StatusUsing));

                this.drpDNStatus.Items.Insert(3, new ListItem(languageComponent1.GetString(DNStatus.StatusClose), DNStatus.StatusClose));

                this.drpDNStatus.SelectedIndex = 0;
            }
        }

        protected void drpBusinessCode_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    DropDownListBuilder builder = new DropDownListBuilder(this.drpBusinessCodeQuery);
                    DropDownListBuilder builder1 = new DropDownListBuilder(this.drpBusinessCodeEdit);

                    InventoryFacade invFacade = new InventoryFacade(this.DataProvider);

                    builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(invFacade.GetAllBusiness);
                    builder1.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(invFacade.GetAllBusiness);

                    builder.Build("BusinessDescription", "BusinessCode");
                    builder1.Build("BusinessDescription", "BusinessCode");

                    this.drpBusinessCodeQuery.Items.Insert(0, new ListItem("", ""));
                    this.drpBusinessCodeEdit.Items.Insert(0, new ListItem("", ""));

                    this.drpBusinessCodeQuery.SelectedIndex = 0;
                    this.drpBusinessCodeEdit.SelectedIndex = 0;
                }
            }
        }
        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("BusinessType", "业务类型", null);
            this.gridHelper.AddColumn("DNNO", "单据号码", null);
            this.gridHelper.AddColumn("RelatedDocument", "关联单据", null);
            this.gridHelper.AddColumn("Dept", "部门/单位", null);
            this.gridHelper.AddColumn("NewOrderNo", "合同号", null);
            this.gridHelper.AddColumn("ShipToParty", "送达方", null);
            this.gridHelper.AddColumn("DNStatus", "单据状态", null);
            this.gridHelper.AddColumn("Memo", "备注", null);
            this.gridHelper.AddLinkColumn("DNLine", "行项目", null);

            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            this.gridWebGrid.Columns.FromKey("NewOrderNo").Hidden = true;
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    ((DNWithBusinessDesc)obj).BusinessDesc.ToString(),
            //                    ((DNWithBusinessDesc)obj).DNCode.ToString(),
            //                    ((DNWithBusinessDesc)obj).RelatedDocument.ToString(),
            //                    ((DNWithBusinessDesc)obj).GetDisplayText("Dept"),
            //                    ((DNWithBusinessDesc)obj).OrderNo.ToString(),
            //                    ((DNWithBusinessDesc)obj).ShipTo.ToString(),
            //                    languageComponent1.GetString(((DNWithBusinessDesc)obj).DNStatus.ToString()),
            //                    ((DNWithBusinessDesc)obj).Memo.ToString(),
            //                    ""});
            DataRow row = this.DtSource.NewRow();
            row["BusinessType"] = ((DNWithBusinessDesc)obj).BusinessDesc.ToString();
            row["DNNO"] = ((DNWithBusinessDesc)obj).DNCode.ToString();
            row["RelatedDocument"] = ((DNWithBusinessDesc)obj).RelatedDocument.ToString();
            row["Dept"] = ((DNWithBusinessDesc)obj).GetDisplayText("Dept");
            row["NewOrderNo"] = ((DNWithBusinessDesc)obj).OrderNo.ToString();
            row["ShipToParty"] = ((DNWithBusinessDesc)obj).ShipTo.ToString();
            row["DNStatus"] = languageComponent1.GetString(((DNWithBusinessDesc)obj).DNStatus.ToString());
            row["Memo"] = ((DNWithBusinessDesc)obj).Memo.ToString();
            return row;
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new DeliveryFacade(base.DataProvider);
            }

            return this.facade.QueryDN(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpBusinessCodeQuery.SelectedValue)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDNNOQuery.Text)),
                FormatHelper.CleanString(this.drpDNStatus.SelectedValue),
                FormatHelper.CleanString(this.txtDeptQuery.Text),
                GlobalVariables.CurrentOrganizations.First().OrganizationID,
                inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new DeliveryFacade(base.DataProvider);
            }
            return this.facade.QueryDNCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpBusinessCodeQuery.SelectedValue)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDNNOQuery.Text)),
                FormatHelper.CleanString(this.drpDNStatus.SelectedValue),
                FormatHelper.CleanString(this.txtDeptQuery.Text),
                GlobalVariables.CurrentOrganizations.First().OrganizationID);
        }

        #endregion

        #region Button

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
           // base.Grid_ClickCell(cell);

            if (commandName=="DNLine")
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FDNLine.aspx",
                    new string[] { "DNNO" },
                    new string[] { row.Items.FindItemByKey("DNNO").Value.ToString() }));
            }
        }

        protected override void AddDomainObject(object domainObject)
        {
            if (facade == null)
            {
                facade = new DeliveryFacade(base.DataProvider);
            }

            DBDateTime dateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            DeliveryNote deliveryNote = domainObject as DeliveryNote;

            object obj = this.facade.GetDeliveryNote(deliveryNote.DNCode, deliveryNote.DNLine);

            if (obj != null)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Primary_Key_Overlap");
            }

            deliveryNote.MaintainUser = this.GetUserCode();
            deliveryNote.MaintainDate = dateTime.DBDate;
            deliveryNote.MaintainTime = dateTime.DBTime;

            this.facade.AddDeliveryNote(deliveryNote);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (facade == null)
            {
                facade = new DeliveryFacade(base.DataProvider);
            }

            DeliveryNote[] deliveryNotes = (DeliveryNote[])domainObjects.ToArray(typeof(DeliveryNote));

            if (deliveryNotes != null)
            {
                this.DataProvider.BeginTransaction();

                try
                {
                    foreach (DeliveryNote deliveryNote in deliveryNotes)
                    {
                        if (deliveryNote.DNStatus == DNStatus.StatusInit)
                        {
                            this.facade.DeleteDNWithOutLine(deliveryNote);
                        }
                        else
                        {
                            throw new Exception(string.Format("$Disabled_Status_Init_Delete   $Current_DNCode:{0}    $Current_Status: {1}", deliveryNote.DNCode, this.languageComponent1.GetString(deliveryNote.DNStatus)));
                        }

                    }

                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();

                    ExceptionManager.Raise(deliveryNotes[0].GetType(), "$Error_Delete_Domain_Object", ex);

                }
            }
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (facade == null)
            {
                facade = new DeliveryFacade(base.DataProvider);
            }

            DBDateTime dateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            DeliveryNote deliveryNote = domainObject as DeliveryNote;
            try
            {
                string status = deliveryNote.DNStatus;
                if (status == DNStatus.StatusClose)
                {
                    throw new Exception(string.Format("$Disabled_Status_Colse_Update   $Current_DNCode:{0}    $Current_Status: {1}", deliveryNote.DNCode, this.languageComponent1.GetString(status)));

                }
                else
                {
                    deliveryNote.MaintainUser = this.GetUserCode();
                    deliveryNote.MaintainDate = dateTime.DBDate;
                    deliveryNote.MaintainTime = dateTime.DBTime;

                    this.facade.UpdateDeliveryNote(deliveryNote);
                }
            }
            catch (Exception ex)
            {

                ExceptionManager.Raise(this.GetType().BaseType, "$Error_Update_Domain_Object", ex);
            }

        }


        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtDNNOEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtDNNOEdit.ReadOnly = true;
            }
        }

        protected void cmdDNClose_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count <= 0)
            {
                return;
            }

            this.UpdateStatus();

        }

        private void UpdateStatus()
        {
            if (facade == null)
            {
                facade = new DeliveryFacade(this.DataProvider);
            }

            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList objs = new ArrayList(array.Count);
                DBDateTime dateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                try
                {
                    foreach (GridRecord row in array)
                    {
                        object obj = this.GetEditObject(row);

                        if (((DeliveryNote)obj).DNStatus == DNStatus.StatusClose)
                        {
                            throw new Exception(string.Format("$Current_DNCode:{0}    $Current_Status: {1}", ((DeliveryNote)obj).DNCode, this.languageComponent1.GetString(((DeliveryNote)obj).DNStatus)));
                        }

                        object[] objDN = this.facade.GetDNForClose(((DeliveryNote)obj).DNCode);

                        if (objDN != null)
                        {
                            foreach (DeliveryNote deliveryNote in objDN)
                            {
                                deliveryNote.DNStatus = DNStatus.StatusClose;
                                deliveryNote.MaintainUser = this.GetUserCode();
                                deliveryNote.MaintainDate = dateTime.DBDate;
                                deliveryNote.MaintainTime = dateTime.DBTime;

                                objs.Add(deliveryNote);
                            }
                        }

                    }
                    this.facade.DNClose((DeliveryNote[])objs.ToArray(typeof(DeliveryNote)));

                    this.gridHelper.RequestData();

                }
                catch (Exception ex)
                {
                    ExceptionManager.Raise(this.GetType().BaseType, "$Error_DeliveryNote_Close", ex);
                }

            }
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (facade == null)
            {
                facade = new DeliveryFacade(base.DataProvider);
            }

            DBDateTime dateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            DeliveryNote deliveryNote = this.facade.CreateNewDN();

            deliveryNote.DNCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDNNOEdit.Text, 40));
            deliveryNote.ShipTo = FormatHelper.CleanString(this.txtShipToPartyEdit.Text);
            deliveryNote.DNLine = "0";
            deliveryNote.ItemCode = " ";
            deliveryNote.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
            deliveryNote.DNQuantity = 0;
            deliveryNote.RealQuantity = 0;
            deliveryNote.Unit = " ";
            deliveryNote.MOCode = " ";
            deliveryNote.OrderNo = FormatHelper.CleanString(this.txtOrderNoEdit.Text);
            deliveryNote.CustomerOrderNo = " ";
            deliveryNote.CustomerOrderNoType = " ";
            deliveryNote.MaintainUser = this.GetUserCode();
            deliveryNote.MaintainDate = dateTime.DBDate;
            deliveryNote.MaintainTime = dateTime.DBTime;
            deliveryNote.DNFrom = DNFrom.MES; //"MES";

            object obj = facade.GetDeliveryNote(deliveryNote.DNCode, "0");
            if (obj != null)
            {
                DeliveryNote deliveryNoteNew = obj as DeliveryNote;
                deliveryNote.DNStatus = deliveryNoteNew.DNStatus;
            }
            else
            {
                deliveryNote.DNStatus = DNStatus.StatusInit;
            }

            deliveryNote.RelatedDocument = FormatHelper.CleanString(this.txtRelateDocumentEdit.Text);
            deliveryNote.BusinessCode = FormatHelper.CleanString(this.drpBusinessCodeEdit.Text);
            deliveryNote.Dept = FormatHelper.CleanString(this.txtDeptEdit.Text);
            deliveryNote.Memo = FormatHelper.CleanString(this.txtMemoEdit.Text);

            return deliveryNote;
        }


        protected override object GetEditObject(GridRecord  row)
        {
            if (facade == null)
            {
                facade = new DeliveryFacade(base.DataProvider);
            }
            object obj = facade.GetDeliveryNote(row.Items.FindItemByKey("DNNO").Value.ToString(), "0");

            if (obj != null)
            {
                return (DeliveryNote)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.drpBusinessCodeEdit.SelectedIndex = 0;
                this.txtDNNOEdit.Text = "";
                this.txtRelateDocumentEdit.Text = "";
                this.txtDeptEdit.Text = "";
                this.txtOrderNoEdit.Text = "";
                this.txtShipToPartyEdit.Text = "";
                this.txtMemoEdit.Text = "";

                return;
            }

            this.drpBusinessCodeEdit.SelectedValue = ((DeliveryNote)obj).BusinessCode.ToString();
            this.txtDNNOEdit.Text = ((DeliveryNote)obj).DNCode.ToString();
            this.txtRelateDocumentEdit.Text = ((DeliveryNote)obj).RelatedDocument.ToString();
            this.txtDeptEdit.Text = ((DeliveryNote)obj).Dept.ToString();
            this.txtOrderNoEdit.Text = ((DeliveryNote)obj).OrderNo.ToString();
            this.txtShipToPartyEdit.Text = ((DeliveryNote)obj).ShipTo.ToString();
            this.txtMemoEdit.Text = ((DeliveryNote)obj).Memo.ToString();
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblBusinessTypeEdit, this.drpBusinessCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblDNNOEdit, this.txtDNNOEdit, 40, true));
            manager.Add(new LengthCheck(this.lblShipToPartyEdit, this.txtShipToPartyEdit, 40, true));
            manager.Add(new LengthCheck(this.lblRelateDocumentEdit, this.txtRelateDocumentEdit, 100, false));
            manager.Add(new LengthCheck(this.lblOrderNoEdit, this.txtOrderNoEdit, 40, false));
            manager.Add(new LengthCheck(this.lblMemoEdit, this.txtMemoEdit, 100, false));

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
            return new string[]{((DeliveryNote)obj).BusinessCode.ToString(),
                                ((DeliveryNote)obj).DNCode.ToString(),
                                ((DeliveryNote)obj).RelatedDocument.ToString(),
                                ((DeliveryNote)obj).GetDisplayText("Dept"),
                                ((DeliveryNote)obj).OrderNo.ToString(),
                                ((DeliveryNote)obj).ShipTo.ToString(),
                                languageComponent1.GetString(((DeliveryNote)obj).DNStatus.ToString()),
                                ((DeliveryNote)obj).Memo.ToString()}
                ;
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"BusinessType",
                                    "DNNO",   
                                    "RelatedDocument",
                                    "Dept",
                                    "NewOrderNo",
                                    "ShipToParty",   
                                    "DNStatus",
                                    "Memo"};
        }

        #endregion

    }
}
