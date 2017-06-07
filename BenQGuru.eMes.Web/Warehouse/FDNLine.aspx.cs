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
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Domain.Delivery;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FDNLine : BaseMPageNew
    {
        protected System.Web.UI.WebControls.Label lblOperationSelectTitle;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
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
            this.txtItemCodeEdit.TextBox.TextChanged += new EventHandler(TextBox_TextChanged);

        }

        void TextBox_TextChanged(object sender, EventArgs e)
        {
            GetMaterilDescription();
        }

        public void GetMaterilDescription()
        {
            ItemFacade itemFacade = new ItemFacade(this.DataProvider);

            object obj = itemFacade.GetMaterial(this.txtItemCodeEdit.Text, GlobalVariables.CurrentOrganizations.First().OrganizationID);

            if (obj != null)
            {
                BenQGuru.eMES.Domain.MOModel.Material material = obj as BenQGuru.eMES.Domain.MOModel.Material;

              //  this.txtItemDescriptionEdit.Text = material.MaterialDescription;
            }
        }

        #endregion

        #region Stable

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.txtDNNOQuery.Text = this.GetRequestParam("DNNO");

            }

            this.txtItemDescriptionEdit.Enabled = false;
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        #endregion

        #region NotStable
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("ItemCode", "产品代码", null);
            this.gridHelper.AddColumn("ItemDescription", "产品描述", null);
            //this.gridHelper.AddColumn("ItemGrade", "产品档次", null);
            this.gridHelper.AddColumn("NewOrderNo", "合同号", null);
            this.gridHelper.AddColumn("StorageType", "库别", null);
            this.gridHelper.AddColumn("MOCode", "工单", null);
            this.gridHelper.AddColumn("ReworkMOCode", "返工工单", null);
            this.gridHelper.AddColumn("DNQuantity", "计划数量", null);

            this.gridHelper.AddColumn("DNLine", "行项目号", null);


            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            this.gridWebGrid.Columns.FromKey("DNLine").Hidden = true;
            this.gridHelper.AddDefaultColumn(true, true);
            base.InitWebGrid();

            this.gridHelper.RequestData();
        }

        protected override void AddDomainObject(object domainObject)
        {
            if (facade == null)
            {
                facade = new DeliveryFacade(base.DataProvider);
            }

            DeliveryNote deliveryNote = domainObject as DeliveryNote;

            object obj = this.facade.GetDeliveryNote(deliveryNote.DNCode, deliveryNote.DNLine);

            if (obj != null)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Primary_Key_Overlap");
            }

            this.facade.AddDeliveryNote(deliveryNote);
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
                if (status == DNStatus.StatusUsing)
                {
                    throw new Exception(string.Format("$Disabled_Status_Using_Update   $Current_DNCode:{0}    $Current_Status: {1}", deliveryNote.DNCode, this.languageComponent1.GetString(status)));
                }

                deliveryNote.MaintainUser = this.GetUserCode();
                deliveryNote.MaintainDate = dateTime.DBDate;
                deliveryNote.MaintainTime = dateTime.DBTime;

                this.facade.UpdateDeliveryNote(deliveryNote);

            }
            catch (Exception ex)
            {

                ExceptionManager.Raise(this.GetType().BaseType, "$Error_Update_Domain_Object", ex);
            }
            
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
                            this.facade.DeleteDeliveryNote(deliveryNote);
                        }
                        else
                        {
                            throw new Exception(string.Format("$Disabled_Status_Init_Delete   $Current_DNCode:{0}    $Current_Status: {1}", deliveryNote.DNCode, this.languageComponent1.GetString(deliveryNote.DNStatus)));
                        }

                    }

                    this.facade.UpdateDNHeadStatus(deliveryNotes[0].DNCode, this.GetUserCode());  //Add by sandy on 20130220

                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();

                    ExceptionManager.Raise(deliveryNotes[0].GetType(), "$Error_Delete_Domain_Object", ex);
                }
            }
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtItemCodeEdit.Readonly = false;
                //this.drpItemGradeEdit.Enabled = true;
                this.drpStorageCodeEdit.Enabled = true;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtItemCodeEdit.Readonly = true;
                //this.drpItemGradeEdit.Enabled = false;
                this.drpStorageCodeEdit.Enabled = false;
            }
        }

        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new DeliveryFacade(this.DataProvider);
            }
            return this.facade.QueryDNLineCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDNNOQuery.Text)),
                GlobalVariables.CurrentOrganizations.First().OrganizationID);
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    ((DeliveryNote)obj).ItemCode.ToString(),
            //                    ((DeliveryNote)obj).ItemDescription.ToString(),
            //                    //((DeliveryNote)obj).GetDisplayText("ItemGrade"),
            //                    ((DeliveryNote)obj).OrderNo.ToString(),
            //                    ((DeliveryNote)obj).GetDisplayText("FromStorage"),
            //                    ((DeliveryNote)obj).MOCode.ToString(),
            //                    ((DeliveryNote)obj).ReworkMOCode.ToString(),
            //                    ((DeliveryNote)obj).DNQuantity.ToString(),
            //                    ((DeliveryNote)obj).DNLine.ToString()
            //                });
            DataRow row = this.DtSource.NewRow();
            row["ItemCode"] = ((DeliveryNote)obj).ItemCode.ToString();
            row["ItemDescription"] = ((DeliveryNote)obj).ItemDescription.ToString();
            row["NewOrderNo"] = ((DeliveryNote)obj).OrderNo.ToString();
            row["StorageType"] = ((DeliveryNote)obj).GetDisplayText("FromStorage");
            row["MOCode"] = ((DeliveryNote)obj).MOCode.ToString();
            row["ReworkMOCode"] = ((DeliveryNote)obj).ReworkMOCode.ToString();
            row["DNQuantity"] = ((DeliveryNote)obj).DNQuantity.ToString();
            row["DNLine"] = ((DeliveryNote)obj).DNLine.ToString();
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new DeliveryFacade(this.DataProvider);
            }
            return facade.QueryDNLine(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDNNOQuery.Text)),
                GlobalVariables.CurrentOrganizations.First().OrganizationID,
                inclusive, exclusive);
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("./FDNMP.aspx"));
        }

        protected void drpStorageCodeEdit_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownListBuilder builder = new DropDownListBuilder(this.drpStorageCodeEdit);

               
                InventoryFacade invFacade = new InventoryFacade(this.DataProvider);


                builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(invFacade.GetAllStorageCode);

                builder.Build("StorageName", "StorageCode");

                this.drpStorageCodeEdit.Items.Insert(0, new ListItem("", ""));

                this.drpStorageCodeEdit.SelectedIndex = 0;
            }
        }

        //protected void drpItemGradeEdit_Load(object sender, System.EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        DropDownListBuilder builder = new DropDownListBuilder(this.drpItemGradeEdit);

        //        SystemSettingFacade systemFacade = new SystemSettingFacade(this.DataProvider);

        //        object[] objList = systemFacade.GetParametersByParameterGroup("PRODUCTLEVEL");
        //        int i = 1;

        //        this.drpItemGradeEdit.Items.Insert(0, new ListItem(" ", " "));

        //        if (objList != null)
        //        {
        //            foreach (BenQGuru.eMES.Domain.BaseSetting.Parameter para in objList)
        //            {
        //                this.drpItemGradeEdit.Items.Insert(i, new ListItem(para.ParameterDescription, para.ParameterAlias));
        //                i++;   
        //            }
        //        }

        //        this.drpItemGradeEdit.SelectedIndex = 0;

        //    }
        //}

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (facade == null)
            {
                facade = new DeliveryFacade(base.DataProvider);
            }

            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            
            DBDateTime dateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            DeliveryNote deliveryNote = this.facade.CreateNewDN();

            object objDNLine = this.facade.GetDeliveryNote(this.txtDNNOQuery.Text.Trim(), this.txtDnLine.Text);

            if (objDNLine != null)
            {
                deliveryNote.DNLine = this.txtDnLine.Text;
                
                DeliveryNote deliveryNoteNew = objDNLine as DeliveryNote;

                deliveryNote.DNStatus = deliveryNoteNew.DNStatus;
            }
            else
            {
                //Add by Sandy 20130220
                object objDNLineHead = this.facade.GetDeliveryNote(this.txtDNNOQuery.Text.Trim(), "0");
                DeliveryNote deliveryNoteHead = objDNLineHead as DeliveryNote;

                string status = deliveryNoteHead.DNStatus;
                if (status == DNStatus.StatusClose)
                {
                    throw new Exception(string.Format("$Disabled_Status_Colse_Update   $Current_DNCode:{0}    $Current_Status: {1}", deliveryNote.DNCode, this.languageComponent1.GetString(status)));
                }

                object[] obj = facade.GetMaxDNLine(this.txtDNNOQuery.Text);

                if (obj != null)
                {
                    DeliveryNote dnLineMax = obj[0] as DeliveryNote;
                    string dnLine = dnLineMax.DNLine;

                    if (dnLine == "0")
                    {
                        deliveryNote.DNLine = "1";
                    }
                    else
                    {
                        int line = int.Parse(dnLine) + 1;

                        deliveryNote.DNLine = line.ToString();
                    }
                }

                deliveryNote.DNStatus = DNStatus.StatusInit;
            }

            deliveryNote.DNCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDNNOQuery.Text, 40));
            deliveryNote.ShipTo = " ";
            deliveryNote.ItemCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeEdit.Text, 40));
            deliveryNote.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
            deliveryNote.FromStorage = this.drpStorageCodeEdit.SelectedValue;

            Storage storage = (Storage)inventoryFacade.GetStorage(GlobalVariables.CurrentOrganizations.First().OrganizationID, this.drpStorageCodeEdit.SelectedValue);
            //if (storage != null)
            //{
            //    deliveryNote.SAPStorage = storage.SAPStorage;
            //}

            //if (this.drpItemGradeEdit.SelectedIndex == 0)
            //{
            //    deliveryNote.ItemGrade = this.drpItemGradeEdit.SelectedValue;
            //}
            //else
            //{
            //    deliveryNote.ItemGrade = FormatHelper.CleanString(this.drpItemGradeEdit.SelectedValue);
            //}
            deliveryNote.DNQuantity = decimal.Parse(FormatHelper.CleanString(this.txtDNQuantityEdit.Text));
            deliveryNote.RealQuantity = 0;
            deliveryNote.Unit = " ";
            //deliveryNote.ItemGrade = FormatHelper.CleanString(this.drpItemGradeEdit.SelectedValue);
            deliveryNote.MOCode = FormatHelper.CleanString(this.txtMOCodeEdit.Text);
            deliveryNote.OrderNo = FormatHelper.CleanString(this.txtOrderNoEdit.Text);
            deliveryNote.CustomerOrderNo = " ";
            deliveryNote.CustomerOrderNoType = " ";
            deliveryNote.ToStorage = " ";
            deliveryNote.MaintainUser = this.GetUserCode();
            deliveryNote.MaintainDate = dateTime.DBDate;
            deliveryNote.MaintainTime = dateTime.DBTime;
            deliveryNote.DNFrom = DNFrom.MES;
           
            deliveryNote.RelatedDocument = " ";
            deliveryNote.BusinessCode = " ";
            deliveryNote.Dept = " ";
            deliveryNote.Memo = " ";
            deliveryNote.ReworkMOCode = FormatHelper.CleanString(this.txtReworkMOEdit.Text);

            this.txtDnLine.Text = "";

            return deliveryNote;
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (facade == null)
            {
                facade = new DeliveryFacade(this.DataProvider);
            }
            object obj = this.facade.GetDeliveryNote(this.txtDNNOQuery.Text.Trim(), row.Items.FindItemByKey("DNLine").Text.Trim());

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
                this.txtItemCodeEdit.Text = "";
                this.txtItemDescriptionEdit.Text = "";
                //this.drpItemGradeEdit.SelectedIndex = 0;
                this.drpStorageCodeEdit.SelectedIndex = 0;
                this.txtDNQuantityEdit.Text = "";
                this.txtMOCodeEdit.Text = "";
                this.txtReworkMOEdit.Text = "";
                this.txtDnLine.Text = "";
                this.txtOrderNoEdit.Text = "";

                return;
            }

            //try
            //{
            //    this.drpItemGradeEdit.SelectedValue = ((DeliveryNote)obj).ItemGrade;
            //}
            //catch
            //{
            //    this.drpItemGradeEdit.SelectedValue = " ";
            //}
            this.txtItemCodeEdit.Text = ((DeliveryNote)obj).ItemCode;
            this.txtItemDescriptionEdit.Text = ((DeliveryNote)obj).ItemDescription;
            //this.drpItemGradeEdit.SelectedValue = ((DeliveryNote)obj).ItemGrade;
            this.drpStorageCodeEdit.SelectedValue = ((DeliveryNote)obj).FromStorage.ToString();
            this.txtDNQuantityEdit.Text = ((DeliveryNote)obj).DNQuantity.ToString();
            this.txtMOCodeEdit.Text = ((DeliveryNote)obj).MOCode;
            this.txtReworkMOEdit.Text = ((DeliveryNote)obj).ReworkMOCode;
            this.txtDnLine.Text = ((DeliveryNote)obj).DNLine; 
            this.txtRealQuantity.Text = ((DeliveryNote)obj).RealQuantity.ToString();
            this.txtOrderNoEdit.Text = ((DeliveryNote)obj).OrderNo.ToString();

            GetMaterilDescription();
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblItemCodeEdit, this.txtItemCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblOrderNoEdit, this.txtOrderNoEdit, 40, false));
            //manager.Add(new LengthCheck(this.lblItemGradeEdit, this.drpItemGradeEdit, 40, false));
            manager.Add(new LengthCheck(this.lblStorageTypeEdit, this.drpStorageCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblDNQuantityEdit, this.txtDNQuantityEdit, 13, true));
            manager.Add(new LengthCheck(this.lblMOCodeEdit, this.txtMOCodeEdit, 40, false));
            manager.Add(new LengthCheck(this.lblReworkMOEdit, this.txtReworkMOEdit, 40, false));
            
            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            int dnQuantity = int.Parse(this.txtDNQuantityEdit.Text);
            if (this.txtRealQuantity.Text != null && this.txtRealQuantity.Text.Length > 0)
            {
                int dnRealQuantity = int.Parse(this.txtRealQuantity.Text);

                if (dnQuantity < dnRealQuantity)
                {
                    WebInfoPublish.Publish(this, "$Plan_Must_Length_Than_Real", this.languageComponent1);
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{((DeliveryNote)obj).ItemCode.ToString(),
                                ((DeliveryNote)obj).ItemDescription.ToString(),
                                //((DeliveryNote)obj).GetDisplayText("ItemGrade"),
                                ((DeliveryNote)obj).OrderNo.ToString(),
                                ((DeliveryNote)obj).GetDisplayText("FromStorage"),
								((DeliveryNote)obj).MOCode.ToString(),
                                ((DeliveryNote)obj).ReworkMOCode.ToString(),
                                ((DeliveryNote)obj).DNQuantity.ToString()};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"ItemCode",
									"ItemDescription",	
									//"ItemGrade",
                                    "NewOrderNo",
                                    "StorageType",
                                    "MOCode",
									"ReworkMOCode",	
									"DNQuantity"};
        }

        #endregion
    }
}
