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
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
using BenQGuru.eMES.MOModel;
using System.IO;



namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FASNForVendorMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
        private static string storageType = string.Empty;
        private InventoryFacade _InventoryFacade = null;
        private SystemSettingFacade _SystemSettingFacade = null;
        private bool _RedirectFlag = false;//页面跳转标识
        private bool _IsAdd = false;//新增标识

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
        protected void Page_Init(object sender, System.EventArgs e)
        {
            PostBackTrigger tri = new PostBackTrigger();
            tri.ControlID = this.cmdEnter.ID;
            (this.FindControl("up1") as UpdatePanel).Triggers.Add(tri);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            string invNo = this.GetRequestParam("InvNo");
            storageType = this.GetRequestParam("StorageInType");
            if (!string.IsNullOrEmpty(invNo) && !string.IsNullOrEmpty(storageType))
            {
                _RedirectFlag = true;
            }

            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.InitStorageInTypeList();
                this.SetInvNoAndStorageTypeByRequestParam(invNo, storageType);
                this.InitStorageInList();
                this.InitStatusList();
                this.InitVendorCodeList();
                string stno = Request.QueryString["StNo"];
                txtStorageInASNQuery.Text = stno;
                this.InitControlsStatus();
                ChangeCblItmeEnabled();
                cmdCreat_ServerClick(null, null);
            }
        }

        //protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        //{

        //}
        //入库类型 改变下拉框中值
        protected void drpStorageInTypeEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeCblItmeEnabled();
        }
        /// <summary>
        ///改变复选框启用状态
        /// </summary>
        private void ChangeCblItmeEnabled()
        {
            ////this.cblFlag.Items[0].Enabled = true;
            ////this.cblFlag.Items[1].Enabled = true;
            ////this.cblFlag.Items[2].Enabled = true;

            //#region add by sam 
            ////供应商直发入库指令复选框，只有当入库类型是PO入库时候才为可用状态，其他入库类型为不可用状态。
            ////生产不良品入库复选框只有入库类型为生产退料入库时为可用状态，其他入库类型为不可用状态。
            ////入库类型为生产退料入库时，供应商代码下来框为必选，需要红色显示，其他入库类型为不可用状态
            //this.cblFlag.Items[0].Enabled = false;
            //this.cblFlag.Items[1].Enabled = false;
            //this.cblFlag.Items[2].Enabled = false;
            string storageInType = this.drpStorageInTypeEdit.SelectedValue;
            //drpVendorCodeEdit.Enabled = false;
            //lblVendorCodeEdit.ForeColor = System.Drawing.Color.Black;
            //if (storageInType == InInvType.POR)
            //{
            //    this.cblFlag.Items[0].Enabled = true;
            //}
            //else if (storageInType == InInvType.SCTR)//生产退料
            //{
            //    this.cblFlag.Items[2].Enabled = true;
            //    drpVendorCodeEdit.Enabled = true;
            //    lblVendorCodeEdit.ForeColor = System.Drawing.Color.Red;
            //}
            //#endregion

            #region
            this.cblFlag.Items[0].Selected = false;
            this.cblFlag.Items[0].Enabled = false;
            this.cblFlag.Items[1].Selected = false;
            this.cblFlag.Items[1].Enabled = false;


            if (storageInType == InInvType.POR)
            {
                this.cblFlag.Items[0].Enabled = true;
            }
            else
            {
                this.cblFlag.Items[0].Enabled = false;

            }

            if (storageInType == InInvType.SCTR)
            {
                this.cblFlag.Items[1].Enabled = true;
                txtSAPInvNoEdit.Enabled = false;
                txtSAPInvNoEdit.Text = string.Empty;
            }
            else
            {
                this.cblFlag.Items[1].Enabled = false;
                txtSAPInvNoEdit.Enabled = true;
              
            }
            ////如果选择研发入库改变控件启用状态
            //else if (storageType == InInvType.YFR)
            //{
            //    this.drpVendorCodeEdit.Enabled = false;
            //    this.txtPickNoEdit.Enabled = false;
            //    this.cmdInStorage.Disabled = true;

            //    this.cblFlag.Items[0].Enabled = false;
            //    this.cblFlag.Items[2].Enabled = false;
            //}
            //else
            //{
            //    this.drpVendorCodeEdit.Enabled = true;
            //    this.txtPickNoEdit.Enabled = true;
            //    this.cmdInStorage.Disabled = false;
            //}
            #endregion
        }
        //供应商代码下拉框
        /// <summary>
        /// 供应商代码下拉框
        /// </summary>
        private void InitVendorCodeList()
        {
            if (_SystemSettingFacade == null)
            {
                _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            }
            object[] parameters = _SystemSettingFacade.GetParametersByParameterGroup("VENDORCODE");
            if (parameters == null)
            {
                WebInfoPublish.PublishInfo(this, "请先配置供应商代码", this.languageComponent1);
                // WebInfoPublish.Publish(this, "$Error_ASN_NO_DATA", this.languageComponent1);
                return;
            }
            this.drpVendorCodeEdit.Items.Add(new ListItem("", ""));
            foreach (Domain.BaseSetting.Parameter parameter in parameters)
            {
                this.drpVendorCodeEdit.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterAlias));
            }
            this.drpVendorCodeEdit.SelectedIndex = 0;
        }

        //初始化入库类型下拉框
        /// <summary>
        /// 初始化入库类型下拉框
        /// </summary>
        private void InitStorageInTypeList()
        {
            drpStorageInTypeQuery.Items.Clear();
            drpStorageInTypeEdit.Items.Clear();
            drpStorageInTypeEdit.Enabled = true;
            this.drpStorageInTypeQuery.Items.Add(new ListItem("", ""));
            this.drpStorageInTypeQuery.Items.Add(new ListItem("PO入库", InInvType.POR));
            this.drpStorageInTypeQuery.Items.Add(new ListItem("生产退料入库", InInvType.SCTR));

            this.drpStorageInTypeEdit.Items.Add(new ListItem("", ""));
            this.drpStorageInTypeEdit.Items.Add(new ListItem("PO入库", InInvType.POR));
            this.drpStorageInTypeEdit.Items.Add(new ListItem("生产退料入库", InInvType.SCTR));


            this.drpStorageInTypeQuery.SelectedIndex = 0;
            if (string.IsNullOrEmpty(storageType))
            {
                this.drpStorageInTypeEdit.SelectedIndex = 1;
            }
            else
            {
                this.drpStorageInTypeEdit.SelectedIndex = this.drpStorageInTypeEdit.Items.IndexOf(this.drpStorageInTypeEdit.Items.FindByValue(storageType));
            }
        }


        //初始化入库库位下拉框
        /// <summary>
        /// 初始化入库库位
        /// </summary>
        private void InitStorageInList()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            this.drpStorageInEdit.Items.Add(new ListItem("", ""));
            object[] objStorage = _InventoryFacade.GetStorage("SAP");
            if (objStorage != null && objStorage.Length > 0)
            {
                foreach (Storage storage in objStorage)
                {
                    this.drpStorageInEdit.Items.Add(new ListItem(storage.StorageCode + "-" + storage.StorageName, storage.StorageCode));
                }
            }
            string invNo = FormatHelper.CleanString(this.txtSAPInvNoEdit.Text);
            if (!string.IsNullOrEmpty(invNo))
            {
                InvoicesDetail invoicesDetail = (InvoicesDetail)_InventoryFacade.GetInvoicesDetail(invNo);
                if (invoicesDetail != null)
                {
                    this.drpStorageInEdit.SelectedValue = invoicesDetail.StorageCode;
                }
            }
            else
            {
                this.drpStorageInEdit.SelectedIndex = 0;
            }
        }

        //初始化状态下拉框
        /// <summary>
        /// 初始化状态下拉框
        /// </summary>
        private void InitStatusList()
        {
            if (_SystemSettingFacade == null)
            {
                _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            }
            this.drpStatusQuery.Items.Add(new ListItem("", ""));
            object[] parameters = _SystemSettingFacade.GetParametersByParameterGroup("ASNHEADSTATUS");
            if (parameters != null && parameters.Length > 0)
            {
                foreach (Domain.BaseSetting.Parameter parameter in parameters)
                {
                    this.drpStatusQuery.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterAlias));
                }
            }
            this.drpStatusQuery.SelectedIndex = 0;
        }

        //根据QueryString初始化入库指令号和入库类型
        /// <summary>
        /// 根据QueryString初始化入库指令号和入库类型
        /// </summary>
        /// <param name="invNo"></param>
        /// <param name="storageType"></param>
        private void SetInvNoAndStorageTypeByRequestParam(string invNo, string storageType)
        {
            if (!string.IsNullOrEmpty(invNo) && !string.IsNullOrEmpty(storageType))
            {
                //查询区域
                this.txtInvNoQuery.Text = invNo;
                this.drpStorageInTypeQuery.SelectedValue = storageType;

                //编辑区域
                this.txtSAPInvNoEdit.Text = invNo;
                this.drpStorageInTypeEdit.SelectedValue = storageType;
            }
        }

        //初始化页面控件状态
        /// <summary>
        /// 初始化页面控件状态
        /// </summary>
        private void InitControlsStatus()
        {
            this.txtStorageInASNEdit.Enabled = false;//入库指令号
            this.cmdReturn.Visible = false;//返回按钮
            if (_RedirectFlag)
            {
                this.cmdReturn.Visible = true;
                this.drpStorageInTypeEdit.Enabled = false;
                this.txtSAPInvNoEdit.Enabled = false;
            }

            //导入控件是否启用
            if (this.cblFlag.Items[0].Selected)
            {
                this.FileImport.Disabled = false;
                this.cmdEnter.Disabled = false;
            }
            else
            {
                this.FileImport.Disabled = true;
                this.cmdEnter.Disabled = true;
            }
        }

        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            //this.gridHelper.AddColumn("ASN", "入库指令号", null);
            //this.gridHelper.AddColumn("CDate", "创建日期", null);
            //this.gridHelper.AddColumn("CUser", "创建人", null);
            //this.gridHelper.AddColumn("StorageInType", "入库类型", null);
            //this.gridHelper.AddColumn("SAPInvNo", "SAP单据号", null);
            //this.gridHelper.AddColumn("Status", "状态", null);
            //this.gridHelper.AddLinkColumn("LinkToCartonImport", "导入/查看箱单", null);
            //this.gridHelper.AddDefaultColumn(true, true);
            //this.gridHelper.AddColumn("StorageInCode", "入库库位", null);
            //this.gridHelper.AddColumn("PredictDate", "预计到货日期", null);
            //this.gridHelper.AddColumn("DirectFlag", "供应商直发标识", null);
            //this.gridHelper.AddColumn("PickNo", "拣货任务令号", null);
            //this.gridHelper.AddColumn("VendorCode", "供应商代码", null);
            //this.gridHelper.AddColumn("ExigencyFlag", "紧急标识", null);
            //this.gridHelper.AddColumn("RejectsFlag", "生产退料入不良品库标识", null);
            //this.gridHelper.AddColumn("OANo", "OA流水号", null);
            //this.gridHelper.AddColumn("PackingListNo", "供应商装箱号", null);
            //this.gridHelper.AddColumn("ProvideDate", "箱单出具日期", null);
            //this.gridHelper.AddColumn("GrossWeight", "毛重", null);
            //this.gridHelper.AddColumn("Volume", "体积", null);
            //this.gridHelper.AddColumn("FromStorageCode", "出库库位", null);
            //this.gridHelper.AddColumn("Remark1", "备注", null);

            if (this.SAPHeadViewFieldList.Length > 6)
            {
                for (int i = 0; i < 6; i++)
                {
                    this.gridHelper.AddColumn(this.SAPHeadViewFieldList[i].FieldName, /*this.languageComponent1.GetString(*/this.SAPHeadViewFieldList[i].Description/*)*/, null);
                }
                this.gridHelper.AddLinkColumn("LinkToCartonImport", "导入/查看箱单", null);
                this.gridHelper.AddDefaultColumn(true, true);
                for (int i = 6; i < this.SAPHeadViewFieldList.Length; i++)
                {
                    this.gridHelper.AddColumn(this.SAPHeadViewFieldList[i].FieldName, /*this.languageComponent1.GetString(*/this.SAPHeadViewFieldList[i].Description/*)*/, null);
                }
            }
            else
            {
                for (int i = 0; i < this.SAPHeadViewFieldList.Length; i++)
                {
                    this.gridHelper.AddColumn(this.SAPHeadViewFieldList[i].FieldName, /*this.languageComponent1.GetString(*/this.SAPHeadViewFieldList[i].Description/*)*/, null);
                }
                this.gridHelper.AddLinkColumn("LinkToCartonImport", "导入/查看箱单", null);
                this.gridHelper.AddDefaultColumn(true, true);
            }

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            this.gridHelper.RequestData();
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            //row["ASN"] = ((ASN)obj).StNo;
            //row["CDate"] = FormatHelper.ToDateString(((ASN)obj).CDate);
            //row["CUser"] = ((ASN)obj).CUser;
            //row["StorageInType"] =this.GetInvInName(((ASN)obj).StType);
            //row["SAPInvNo"] = ((ASN)obj).InvNo;
            //row["Status"] = this.GetStatusName(((ASN)obj).Status);
            //row["StorageInCode"] = ((ASN)obj).StorageCode;
            //row["PredictDate"] = FormatHelper.ToDateString(((ASN)obj).PreictDate);
            //row["DirectFlag"] = ((ASN)obj).DirectFlag;
            //row["PickNo"] = ((ASN)obj).PickNo;
            //row["VendorCode"] = ((ASN)obj).VendorCode;
            //row["ExigencyFlag"] = ((ASN)obj).ExigencyFlag;
            //row["RejectsFlag"] = ((ASN)obj).RejectsFlag;
            //row["OANo"] = ((ASN)obj).OANo;
            //row["PackingListNo"] = ((ASN)obj).PackingListNo;
            //row["ProvideDate"] = FormatHelper.ToDateString(((ASN)obj).ProvideDate);
            //row["GrossWeight"] = ((ASN)obj).GrossWeight;
            //row["Volume"] = ((ASN)obj).Volume;
            //row["FromStorageCode"] = ((ASN)obj).FromStorageCode;
            //row["Remark1"] = ((ASN)obj).Remark1;
            ASN inv = obj as ASN;
            Type type = inv.GetType();
            if (this.SAPHeadViewFieldList.Length > 6)
            {
                for (int i = 0; i < 6; i++)
                {
                    ViewField field = this.SAPHeadViewFieldList[i];
                    string strValue = string.Empty;
                    System.Reflection.FieldInfo fieldInfo = type.GetField(field.FieldName);
                    if (fieldInfo != null)
                    {
                        strValue = fieldInfo.GetValue(inv).ToString();
                    }
                    if (field.FieldName == "CDate")
                    {
                        strValue = FormatHelper.ToDateString(inv.CDate);

                    }
                    else if (field.FieldName == "StType")
                    {
                        strValue = this.GetInvInName(inv.StType);
                    }
                    else if (field.FieldName == "StorageInType")
                    {
                        strValue = this.GetInvInName(inv.StType);
                    }
                    else if (field.FieldName == "Status")
                    {
                        strValue = this.GetStatusName(inv.Status);
                    }
                    else if (field.FieldName == "PredictDate")
                    {
                        strValue = FormatHelper.ToDateString(inv.PreictDate);
                    }
                    else if (field.FieldName == "ProvideDate")
                    {
                        strValue = inv.ProvideDate;// FormatHelper.ToDateString(inv.ProvideDate);
                    }

                    //PlanSendDate
                    row[i + 1] = strValue;
                }
                for (int i = 6; i < this.SAPHeadViewFieldList.Length; i++)
                {
                    ViewField field = this.SAPHeadViewFieldList[i];
                    string strValue = string.Empty;
                    System.Reflection.FieldInfo fieldInfo = type.GetField(field.FieldName);
                    if (fieldInfo != null)
                    {
                        strValue = fieldInfo.GetValue(inv).ToString();
                    }
                    else if (field.FieldName == "StType")
                    {
                        strValue = this.GetInvInName(inv.StType);
                    }
                    if (field.FieldName == "CDate")
                    {
                        strValue = FormatHelper.ToDateString(inv.CDate);

                    }
                    else if (field.FieldName == "StorageInType")
                    {
                        strValue = this.GetInvInName(inv.StType);
                    }
                    else if (field.FieldName == "Status")
                    {
                        strValue = this.GetStatusName(inv.Status);
                    }
                    else if (field.FieldName == "PredictDate")
                    {
                        strValue = FormatHelper.ToDateString(inv.PreictDate);
                    }
                    else if (field.FieldName == "ProvideDate")
                    {
                        strValue = inv.ProvideDate;//  FormatHelper.ToDateString(inv.ProvideDate);
                    }

                    //PlanSendDate
                    row[i + 4] = strValue;
                }
            }
            else
            {
                for (int i = 0; i < this.SAPHeadViewFieldList.Length; i++)
                {
                    ViewField field = this.SAPHeadViewFieldList[i];
                    string strValue = string.Empty;
                    System.Reflection.FieldInfo fieldInfo = type.GetField(field.FieldName);
                    if (fieldInfo != null)
                    {
                        strValue = fieldInfo.GetValue(inv).ToString();
                    }
                    if (field.FieldName == "CDate")
                    {
                        strValue = FormatHelper.ToDateString(inv.CDate);

                    }
                    else if (field.FieldName == "StorageInType")
                    {
                        strValue = this.GetInvInName(inv.StType);
                    }
                    else if (field.FieldName == "Status")
                    {
                        strValue = this.GetStatusName(inv.Status);
                    }
                    else if (field.FieldName == "PredictDate")
                    {
                        strValue = FormatHelper.ToDateString(inv.PreictDate);
                    }
                    else if (field.FieldName == "ProvideDate")
                    {
                        strValue = inv.ProvideDate;// FormatHelper.ToDateString(inv.ProvideDate);
                    }
                    else if (field.FieldName == "FacName")
                    {
                        strValue = inv.ProvideDate;// FormatHelper.ToDateString(inv.ProvideDate);
                    }
                    //PlanSendDate
                    row[i + 1] = strValue;
                }
            }

            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            return this._InventoryFacade.QueryASN1(
                FormatHelper.CleanString(this.txtStorageInASNQuery.Text),
                this.drpStorageInTypeQuery.SelectedValue,
                FormatHelper.CleanString(this.txtInvNoQuery.Text),
                FormatHelper.CleanString(this.txtCreateUserQuery.Text),
                FormatHelper.CleanString(this.drpStatusQuery.SelectedValue),
                FormatHelper.TODateInt(this.txtCBDateQuery.Text),
                FormatHelper.TODateInt(this.txtCEDateQuery.Text),
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            return this._InventoryFacade.QueryASN1Count(
               FormatHelper.CleanString(this.txtStorageInASNQuery.Text),
               this.drpStorageInTypeQuery.SelectedValue,
               FormatHelper.CleanString(this.txtInvNoQuery.Text),
               FormatHelper.CleanString(this.txtCreateUserQuery.Text),
               FormatHelper.CleanString(this.drpStatusQuery.SelectedValue),
               FormatHelper.TODateInt(this.txtCBDateQuery.Text),
               FormatHelper.TODateInt(this.txtCEDateQuery.Text)
               );
        }

        #endregion

        #region Button

        //复选框 选择改变
        protected void cblFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cblFlag.Items[0].Selected)
            {
                this.FileImport.Disabled = false;
                this.cmdEnter.Disabled = false;
            }
            else
            {
                this.FileImport.Disabled = true;
                this.cmdEnter.Disabled = true;
            }
        }

        //点击Grid中按钮
        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            string asn = row.Items.FindItemByKey("StNo").Text.Trim();

            if (commandName == "LinkToCartonImport")
            {
                Response.Redirect(this.MakeRedirectUrl("FCartonDataImpMP.aspx",
                                    new string[] { "ASN", "Page" },
                                    new string[] { asn, "FASNForVendorMP.aspx" }));
            }

        }

        //生成
        protected void cmdCreat_ServerClick(object sender, System.EventArgs e)
        {
            WarehouseFacade warehouseFacade = new WarehouseFacade(base.DataProvider);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(base.DataProvider);
            string preFix = "IN" + dbDateTime.DBDate.ToString().Substring(2);
            object objSerialBook = warehouseFacade.GetSerialBook(preFix);
            if (objSerialBook == null)
            {
                SERIALBOOK serialBook = new SERIALBOOK();
                serialBook.SNPrefix = preFix;
                serialBook.MaxSerial = "1";
                serialBook.MUser = this.GetUserCode();
                serialBook.MDate = dbDateTime.DBDate;
                serialBook.MTime = dbDateTime.DBTime;
                try
                {
                    warehouseFacade.AddSerialBook(serialBook);
                    this.txtStorageInASNEdit.Text = preFix + "0001";
                }
                catch (Exception ex)
                {

                    WebInfoPublish.Publish(this, "生成失败：" + ex.Message, this.languageComponent1);
                }
            }
            else
            {
                SERIALBOOK serialBook = (SERIALBOOK)objSerialBook;
                if (serialBook.MaxSerial == "9999")
                {
                    WebInfoPublish.Publish(this, "今天的入库指令号已用完！", this.languageComponent1);
                    return;
                }
                serialBook.MaxSerial = (Convert.ToInt32(serialBook.MaxSerial) + 1).ToString();
                try
                {
                    warehouseFacade.UpdateSerialBook(serialBook);
                    this.txtStorageInASNEdit.Text = serialBook.SNPrefix + serialBook.MaxSerial.PadLeft(4, '0');
                }
                catch (Exception ex)
                {

                    WebInfoPublish.Publish(this, "生成失败：" + ex.Message, this.languageComponent1);
                }
            }
        }

        //保存
        protected override void UpdateDomainObject(object domainObject)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            WarehouseFacade facade = new WarehouseFacade(base.DataProvider);
            if (_IsAdd)
            {
                #region 在invinouttrans表中增加一条数据
                ASN asn = (ASN)domainObject;
                DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                InvInOutTrans trans = facade.CreateNewInvInOutTrans();
                trans.CartonNO = string.Empty;
                trans.DqMCode = string.Empty;
                trans.FacCode = asn.FacCode;
                trans.FromFacCode = asn.FromFacCode;
                trans.FromStorageCode = asn.FromStorageCode;
                trans.InvNO = asn.InvNo;
                trans.InvType = asn.StType;
                trans.LotNo = string.Empty;
                trans.MaintainDate = dbTime.DBDate;
                trans.MaintainTime = dbTime.DBTime;
                trans.MaintainUser = this.GetUserCode();
                trans.MCode = string.Empty;
                trans.ProductionDate = 0;
                trans.Qty = 0;
                trans.Serial = 0;
                trans.StorageAgeDate = 0;
                trans.StorageCode = asn.StorageCode;
                trans.SupplierLotNo = string.Empty;
                trans.TransNO = asn.StNo;
                trans.TransType = "IN";
                trans.Unit = string.Empty;
                trans.ProcessType = "ASN";
                facade.AddInvInOutTrans(trans);
                #endregion
                this._InventoryFacade.AddASN((ASN)domainObject);
            }
            else
            {
                this._InventoryFacade.UpdateASN((ASN)domainObject);
            }
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            WarehouseFacade _warehouseFacade = new WarehouseFacade(base.DataProvider);
            //this._InventoryFacade.DeleteASN(domainObjects.ToArray(typeof(ASN)) as ASN[]);
            this.DataProvider.BeginTransaction();
            try
            {
                this._InventoryFacade.DeleteASN1(domainObjects.ToArray(typeof(ASN)) as ASN[]);
                foreach (ASN asn in domainObjects.ToArray(typeof(ASN)) as ASN[])
                {
                    object[] objs_asnd = _InventoryFacade.GetASNDetailByStNo(asn.StNo);
                    if (objs_asnd != null)
                    {
                        foreach (ASNDetail asnd in objs_asnd)
                        {
                            _InventoryFacade.DeleteASNDetail(asnd);
                            object[] objs_item = _warehouseFacade.GetASNDetailItembyStnoAndStline(asnd.StNo, asnd.StLine);
                            if (objs_item != null)
                            {
                                foreach (Asndetailitem item in objs_item)
                                {
                                    _warehouseFacade.DeleteAsndetailitem(item);
                                }
                            }
                            object[] objs_sn = _warehouseFacade.GetASNDetailSNbyStnoandStline(asnd.StNo, asnd.StLine);
                            if (objs_sn != null)
                            {
                                foreach (Asndetailsn sn in objs_sn)
                                {
                                    _warehouseFacade.DeleteAsndetailsn(sn);
                                }
                            }
                        }
                    }


                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
        }

        //返回
        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("FSAPStorageInDemandQuery.aspx"));
        }

        //导入
        protected void cmdEnter_ServerClick(object sender, EventArgs e)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            string asnNo = FormatHelper.CleanString(this.txtStorageInASNEdit.Text);
            if (string.IsNullOrEmpty(asnNo))
            {
                WebInfoPublish.PublishInfo(this, "入库指令号为空", this.languageComponent1);
                return;
            }

            if (this.FileImport.PostedFile != null)
            {
                try
                {
                    HttpPostedFile postedFile = this.FileImport.PostedFile;

                    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(base.DataProvider);
                    InvDoc invDoc = _InventoryFacade.CreateNewInvDoc();

                    invDoc.InvDocNo = asnNo;
                    invDoc.InvDocType = "DirectSign";
                    invDoc.DocType = Path.GetExtension(postedFile.FileName);
                    invDoc.DocName = Path.GetFileNameWithoutExtension(postedFile.FileName);
                    invDoc.DocSize = postedFile.ContentLength / 1024;
                    invDoc.UpUser = this.GetUserCode();
                    invDoc.UpfileDate = dbDateTime.DBDate;
                    invDoc.MaintainUser = this.GetUserCode();
                    invDoc.MaintainDate = dbDateTime.DBDate;
                    invDoc.MaintainTime = dbDateTime.DBTime;
                    invDoc.Dirname = "InvDoc";
                    string path = Server.MapPath(this.VirtualHostRoot + "InvDoc/" + "直发入库客户接受凭证/");
                    string fileName = string.Format("{0}_DirectSign_{1}{2}{3}",
                        this.txtStorageInASNEdit.Text, dbDateTime.DBDate, dbDateTime.DBTime, invDoc.DocType);

                    invDoc.ServerFileName = fileName;

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    invDoc.Dirname = "直发入库客户接受凭证";
                    this.FileImport.PostedFile.SaveAs(path + fileName);
                    _InventoryFacade.AddInvDoc(invDoc);
                    WebInfoPublish.PublishInfo(this, "$Success_UpLoadFile", this.languageComponent1);
                }
                catch (Exception ex)
                {

                    WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
                }

            }
            else
            {
                WebInfoPublish.PublishInfo(this, "导入文件不能为空", this.languageComponent1);
            }
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Query)
            {
                this.cmdSave.Disabled = false;

                if (this.cblFlag.Items[0].Selected)
                {
                    this.FileImport.Disabled = false;
                    this.cmdEnter.Disabled = false;
                }
                else
                {
                    this.FileImport.Disabled = true;
                    this.cmdEnter.Disabled = true;
                }
                if (_RedirectFlag)
                {
                    this.drpStorageInTypeEdit.Enabled = false;
                    this.txtSAPInvNoEdit.Enabled = false;
                }
                else
                {
                    this.drpStorageInTypeEdit.Enabled = true;
                    this.txtSAPInvNoEdit.Enabled = true;
                }
            }
            if (pageAction == PageActionType.Save)
            {
                this.cmdSave.Disabled = false;
            }
            if (pageAction == PageActionType.Add)
            {
                this.cmdCreat.Disabled = false;
                this.cmdSave.Disabled = false;

                if (_RedirectFlag)
                {
                    this.drpStorageInTypeEdit.Enabled = false;
                    this.txtSAPInvNoEdit.Enabled = false;
                }
                else
                {
                    this.drpStorageInTypeEdit.Enabled = true;
                    this.txtSAPInvNoEdit.Enabled = true;
                }
            }
            if (pageAction == PageActionType.Update)
            {
                this.cmdCreat.Disabled = true;
                if (this.cblFlag.Items[0].Selected)
                {
                    this.FileImport.Disabled = false;
                    this.cmdEnter.Disabled = false;
                }
                else
                {
                    this.FileImport.Disabled = true;
                    this.cmdEnter.Disabled = true;
                }
                this.drpStorageInTypeEdit.Enabled = false;
                this.txtSAPInvNoEdit.Enabled = false;
            }
        }
        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            string stNo = FormatHelper.CleanString(this.txtStorageInASNEdit.Text, 40);
            ASN asn = (ASN)_InventoryFacade.GetASN(stNo);
            if (asn == null)
            {
                _IsAdd = true;
                asn = this._InventoryFacade.CreateNewASN();
            }
            asn.StNo = stNo;
            asn.StType = FormatHelper.CleanString(this.drpStorageInTypeEdit.SelectedValue, 40);
            asn.InvNo = FormatHelper.CleanString(this.txtSAPInvNoEdit.Text, 40);
            asn.PreictDate = FormatHelper.TODateInt(this.txtExpectedArrivalDateEdit.Text);



            asn.DirectFlag = this.cblFlag.Items[0].Selected ? "Y" : "";
            asn.RejectsFlag = this.cblFlag.Items[1].Selected ? "Y" : "";
            asn.Remark1 = FormatHelper.CleanString(this.txtMemoEdit.Text, 300);
            asn.VendorCode = FormatHelper.CleanString(this.drpVendorCodeEdit.SelectedValue, 40);
            if (_IsAdd)
            {
                asn.Status = ASNLineStatus.Release;
                asn.FacCode = this.GetFacCode();
                asn.StorageCode = FormatHelper.CleanString(this.drpStorageInEdit.Text, 40);
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(base.DataProvider);
                asn.CDate = dbDateTime.DBDate;
                asn.CTime = dbDateTime.DBTime;
                asn.CUser = this.GetUserCode();
            }
            asn.MaintainUser = this.GetUserCode();

            return asn;
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            object obj = _InventoryFacade.GetASN(row.Items.FindItemByKey("StNo").Value.ToString());

            if (obj != null)
            {
                return (ASN)obj;
            }
            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtStorageInASNEdit.Text = "";
                this.drpStorageInTypeEdit.SelectedIndex = 1;
                this.drpVendorCodeEdit.SelectedIndex = 0;
                this.txtSAPInvNoEdit.Text = "";
                this.txtExpectedArrivalDateEdit.Text = "";
                this.txtMemoEdit.Text = "";
                this.cblFlag.Items[0].Selected = false;
                this.cblFlag.Items[1].Selected = false;
                InitStorageInTypeList();
                return;
            }
            this.txtStorageInASNEdit.Text = ((ASN)obj).StNo;

            try
            {
                this.drpStorageInTypeEdit.SelectedValue = ((ASN)obj).StType;

            }
            catch (Exception)
            {

                //this.drpStorageInTypeEdit.SelectedIndex= 1 ;
                this.drpStorageInTypeEdit.Items.Clear();
                this.drpStorageInTypeEdit.Items.Add(new ListItem(this.GetInvInName(((ASN)obj).StType), ((ASN)obj).StType));
                this.drpStorageInTypeEdit.SelectedValue = ((ASN)obj).StType;
            }
            drpStorageInTypeEdit.Enabled = false;
            try
            {
                this.drpStorageInEdit.SelectedValue = ((ASN)obj).StorageCode;
            }
            catch (Exception)
            {

                this.drpStorageInEdit.SelectedIndex = 1;
            }
            try
            {
                this.drpVendorCodeEdit.SelectedValue = ((ASN)obj).VendorCode;
            }
            catch (Exception)
            {
                this.drpVendorCodeEdit.SelectedIndex = 1;
            }
            this.txtSAPInvNoEdit.Text = ((ASN)obj).InvNo;
            this.txtExpectedArrivalDateEdit.Text = FormatHelper.ToDateString(((ASN)obj).PreictDate, "-");
            this.txtMemoEdit.Text = ((ASN)obj).Remark1;
            this.cblFlag.Items[0].Selected = ((ASN)obj).DirectFlag == "Y";

        }

        protected override bool ValidateInput()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            PageCheckManager manager = new PageCheckManager();
            string storageInType = this.drpStorageInTypeEdit.SelectedValue;
            manager.Add(new LengthCheck(this.lblStorageInASNEdit, this.txtStorageInASNEdit, 40, true));
            if (storageInType == InInvType.SCTR)//生产退料
            {
                manager.Add(new LengthCheck(this.lblMesFacNameEdit, this.drpVendorCodeEdit, 40, true));
            }
            else
            {
                manager.Add(new LengthCheck(this.lblMesFacNameEdit, this.drpVendorCodeEdit, 40, false));
                manager.Add(new LengthCheck(this.lblStorageInEdit, this.drpStorageInEdit, 40, true));

            }
            if (this.cblFlag.Items[0].Selected)
            {
                object[] objInvDoc = this._InventoryFacade.GetInvDocByInvDocNo(this.txtStorageInASNEdit.Text.Trim());
                if (objInvDoc == null)
                {
                    WebInfoPublish.Publish(this, "请上传直发入库客户接受凭证", this.languageComponent1);
                    return false;
                }
            }
            manager.Add(new LengthCheck(this.lblStorageInTypeEdit, this.drpStorageInTypeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblExpectedArrivalDateEdit, this.txtExpectedArrivalDateEdit, 22, true));



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
            //return new string[]{((ASN)obj).StNo,
            //                    FormatHelper.ToDateString(((ASN)obj).CDate),
            //                    ((ASN)obj).CUser,
            //                    this.GetInvInName(((ASN)obj).StType),
            //                    ((ASN)obj).InvNo,
            //                    this.GetStatusName(((ASN)obj).Status),
            //                    ((ASN)obj).StorageCode,
            //                    FormatHelper.ToDateString(((ASN)obj).PreictDate),
            //                    ((ASN)obj).DirectFlag,
            //                    ((ASN)obj).PickNo,
            //                    ((ASN)obj).VendorCode,
            //                    ((ASN)obj).ExigencyFlag,
            //                    ((ASN)obj).RejectsFlag,
            //                    ((ASN)obj).OANo,
            //                    ((ASN)obj).PackingListNo,
            //                    FormatHelper.ToDateString(((ASN)obj).ProvideDate),
            //                    ((ASN)obj).GrossWeight.ToString(),
            //                    ((ASN)obj).Volume,
            //                    ((ASN)obj).FromStorageCode,
            //                    ((ASN)obj).Remark1
            //                  };
            string[] objs = new string[this.SAPHeadViewFieldList.Length];
            ASN inv = obj as ASN;
            Type type = inv.GetType();
            for (int i = 0; i < this.SAPHeadViewFieldList.Length; i++)
            {
                ViewField field = this.SAPHeadViewFieldList[i];
                string strValue = string.Empty;
                System.Reflection.FieldInfo fieldInfo = type.GetField(field.FieldName);
                if (fieldInfo != null)
                {
                    strValue = fieldInfo.GetValue(inv).ToString();
                }
                if (field.FieldName == "CDate")
                {
                    strValue = FormatHelper.ToDateString(inv.CDate);

                }
                else if (field.FieldName == "StorageInType")
                {
                    strValue = this.GetInvInName(inv.StType);
                }
                else if (field.FieldName == "Status")
                {
                    strValue = this.GetStatusName(inv.Status);
                }
                else if (field.FieldName == "PredictDate")
                {
                    strValue = FormatHelper.ToDateString(inv.PreictDate);
                }
                else if (field.FieldName == "ProvideDate")
                {
                    strValue = inv.ProvideDate;// FormatHelper.ToDateString(inv.ProvideDate);
                }
                objs[i] = strValue;
            }
            return objs;
        }

        protected override string[] GetColumnHeaderText()
        {
            //return new string[] {	"ASN",
            //                        "CDate",
            //                        "CUser",
            //                        "StorageInType",
            //                        "SAPInvNo",
            //                        "Status",	
            //                        "StorageInCode",
            //                        "PredictDate",	
            //                        "DirectFlag",
            //                        "PickNo",	
            //                        "VendorCode",
            //                        "ExigencyFlag",	
            //                        "RejectsFlag",
            //                        "OANo",
            //                        "PackingListNo",	
            //                        "ProvideDate",
            //                        "GrossWeight",	
            //                        "Volume",
            //                        "FromStorageCode",	
            //                        "Remark1"};
            string[] strHeader = new string[this.SAPHeadViewFieldList.Length];

            for (int i = 0; i < strHeader.Length; i++)
            {
                strHeader[i] = this.SAPHeadViewFieldList[i].Description;
            }
            return strHeader;
        }

        #endregion

        private ViewField[] viewFieldList = null;
        private ViewField[] SAPHeadViewFieldList
        {
            get
            {
                if (viewFieldList == null)
                {
                    if (_InventoryFacade == null)
                    {
                        _InventoryFacade = new InventoryFacade(base.DataProvider);
                    }
                    object[] objs = _InventoryFacade.QueryViewFieldByUserCode(this.GetUserCode(), "TBLASN");
                    if (objs != null)
                    {
                        viewFieldList = new ViewField[objs.Length];
                        objs.CopyTo(viewFieldList, 0);
                    }
                    else
                    {
                        objs = _InventoryFacade.QueryViewFieldDefault("ASN_FIELD_LIST_SYSTEM_DEFAULT", "TBLASN");
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
                            if (viewFieldList[i].FieldName == "StNo")
                            {
                                bExistPickNo = true;
                                break;
                            }
                        }
                        if (bExistPickNo == false)
                        {
                            ViewField field = new ViewField();
                            field.FieldName = "StNo";
                            field.Description = "入库指令号";
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
