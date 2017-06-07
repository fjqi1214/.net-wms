#region system
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
using System.Web.UI.WebControls.WebParts;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
#endregion

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.IQC;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.Domain.Equipment;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;

namespace BenQGuru.eMES.Web.IQC
{
    public partial class FIQCCheckResultDetailMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        private GridHelperNew gridHelperF = null;
        private ButtonHelper buttonHelper = null;
        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
        IQCFacade _IQCFacade = null;

        #region Init
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
            this.gridWebGridF.EnableDataViewState = true;
            this.gridWebGridF.EnableViewState = true;
            this.gridWebGridF.EnableAjax = false;
            this.gridWebGridF.StyleSetName = "Office2007Blue";
            this.gridWebGridF.AutoGenerateColumns = false;
            this.gridWebGridF.ClientEvents.Initialize = "Grid_Initialize";
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            this.txtCheckGroupCodeEdit.TextBox.TextChanged += new EventHandler(this.txtCheckGroupEdit_TextChanged);
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
            // 
            // excelExporter
            // 
            this.excelExporter.FileExtension = "xls";
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.Page = this;
            this.excelExporter.RowSplit = "\r\n";

            // this.gridWebGridF.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
        }
        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("IQCNo", "送检单号", null);
            this.gridHelper.AddColumn("Receiptno", "入库单号", null);
            this.gridHelper.AddColumn("IQCSTLine", "行号码", null);
            this.gridHelper.AddColumn("MaterialCode", "物料代码", null);
            this.gridHelper.AddColumn("MaterialDesc", "物料描述", null);
            this.gridHelper.AddColumn("STDStatus", "状态", null);
            this.gridHelper.AddColumn("IsInStorage", "是否入库", null);
            this.gridHelper.AddColumn("Unit", "单位", null);
            this.gridHelper.AddColumn("ReceiveQty", "收货数量", null);
            //this.gridHelper.AddColumn("IQCPlanQTY", "排程数量", null);
            this.gridHelper.AddColumn("OrderLine", "订单行", null);
            this.gridHelper.AddColumn("INSType", "检验形式", null);
            this.gridHelper.AddColumn("IQCResult", "检验结果", null);
            this.gridHelper.AddColumn("AttriBute", "类型", null);
            this.gridHelper.AddColumn("SampleQty", "抽样数", null);
            this.gridHelper.AddColumn("NGQty", "不良数", null);
            this.gridHelper.AddColumn("MemoEx", "不合格描述", null);
            this.gridHelper.AddColumn("PurchaseMEMO", "采购备注", null);
            this.gridHelper.AddColumn("Memo", "备注", null);
            this.gridHelper.AddColumn("Type", "接收方式", null);
            this.gridHelper.AddColumn("Action", "永久性措施说明", null);

            this.gridHelper.AddColumn("ConcessionQTY", "让步接收数量", null);
            this.gridHelper.AddColumn("ConcessionNO", "让步接收单号", null);
            this.gridHelper.AddColumn("ConcessionMemo", "让步接收说明", null);

            this.gridWebGrid.Columns.FromKey("IQCNo").Hidden = true;
            this.gridWebGrid.Columns.FromKey("Receiptno").Hidden = true;
            this.gridWebGrid.Columns.FromKey("ConcessionQTY").Hidden = true;
            this.gridWebGrid.Columns.FromKey("ConcessionNO").Hidden = true;
            this.gridWebGrid.Columns.FromKey("ConcessionMemo").Hidden = true;

            // 2005-04-06
            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

        }

        //add by seven    2010-12-14
        protected void InitWebGridF()
        {
            //添加系统主键
            this.gridHelperF.AddDataColumn("GUID", "GUID", true);

            this.gridWebGridF.DataKeyFields = "GUID";
            this.DtSource2.PrimaryKey = new DataColumn[] { DtSource2.Columns["GUID"] };
            this.gridHelperF.AddColumn("CheckGroup", "测试项目组", null);
            this.gridHelperF.AddColumn("CheckItemCode", "检验项目", null);
            this.gridHelperF.AddColumn("SetValueMin", "最小设定值", null);
            this.gridHelperF.AddColumn("SetValueMax", "最大设定值", null);
            this.gridHelperF.AddColumn("RealValue", "实际值", null);
            this.gridHelperF.AddCheckBoxColumn("UnPass", "不通过", false);

            this.gridWebGridF.Behaviors.CreateBehavior<EditingCore>().Behaviors.CreateBehavior<CellEditing>().ColumnSettings["RealValue"].ReadOnly = false;
            (this.gridWebGridF.Columns.FromKey("RealValue") as BoundDataField).CssClass = "allowEdit";

            this.gridWebGridF.Behaviors.CreateBehavior<EditingCore>().EditingClientEvents.CellValueChanged = "CellValueChanged";

            this.gridWebGridF.Columns.FromKey("CheckGroup").Hidden = true;
            (this.gridWebGridF.Columns.FromKey("UnPass") as BoundCheckBoxField).Width = new Unit(70);

            //this.gridWebGridF.Columns.FromKey("CheckGroup").Width = Unit.Percentage(25.0);
            //this.gridWebGridF.Columns.FromKey("CheckItemCode").Width = Unit.Percentage(32.0);
            //this.gridWebGridF.Columns.FromKey("SetValueMin").Width = Unit.Percentage(19.0);
            //this.gridWebGridF.Columns.FromKey("SetValueMax").Width = Unit.Percentage(19.0);
            //this.gridWebGridF.Columns.FromKey("RealValue").Width = Unit.Percentage(18.0);
            //this.gridWebGridF.Columns.FromKey("UnPass").Width = Unit.Percentage(12.0);

            //this.gridWebGridF.Columns.FromKey("CheckItemCode").AllowUpdate = AllowUpdate.No;
            //this.gridWebGridF.Columns.FromKey("SetValueMin").AllowUpdate = AllowUpdate.No;
            //this.gridWebGridF.Columns.FromKey("SetValueMax").AllowUpdate = AllowUpdate.No;
            // 2005-04-06
            this.gridHelperF.AddDefaultColumn(true, false);

            //多语言
            this.gridHelperF.ApplyLanguage(this.languageComponent1);

            // this.gridWebGridF.FromKey("RealValue"). = Color.FromArgb(255, 252, 240);
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);
            this.gridHelperF = new GridHelperNew(this.gridWebGridF, this.DtSource2);
            this.gridHelperF.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSourceF);
            this.gridHelperF.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRowF);
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
                InitWebGridF();

                InitParameters();
                this.txtIQCNo.Enabled = false;
                this.cmdRegressToAcceptButton.Disabled = true;

                IInternalSystemVariable IInternalSystemVariableCheckType = new IQCCheckType();
                RadioButtonListBuilder builderCheckType = new RadioButtonListBuilder(IInternalSystemVariableCheckType, this.rblCheckType, this.languageComponent1);
                builderCheckType.Build();
                this.rblCheckType.SelectedIndex = 1;

                IInternalSystemVariable IInternalSystemVariableNew = new IQCCheckStatus();
                IInternalSystemVariableNew.Items.Remove(IQCCheckStatus.IQCCheckStatus_WaitCheck);
                RadioButtonListBuilder builder1 = new RadioButtonListBuilder(IInternalSystemVariableNew, this.rblPass, this.languageComponent1);
                builder1.Build();
                this.rblPass.SelectedIndex = 0;

                this.ViewState["ReceiveQty"] = "";
                if (this.txtIQCNo.Text.Trim() != string.Empty)
                {
                    RequestData();
                    this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
                }
            }
            this.gridWebGridF.DataSource = this.DtSource2;
            this.gridWebGridF.DataBind();
            //可编辑列的背景色设置
            this.RunScript("$('.allowEdit').css('background-color','#fffdf1');");
        }

        protected override void cmdSave_Click(object sender, EventArgs e)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(this.DataProvider);
            }
            if (this.ValidateInput())
            {
                object objectIQCDetail = this.GetEditObject();

                if (objectIQCDetail != null)
                {
                    DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                    //增加检验项目检查
                    ItemFacade _ItemFacade = new ItemFacade();
                    object material = _ItemFacade.GetMaterial(((IQCDetail)objectIQCDetail).ItemCode, ((IQCDetail)objectIQCDetail).OrganizationID);
                    if (material == null)
                    {
                        WebInfoPublish.Publish(this, "$Error_Material_NotFound:" + ((IQCDetail)objectIQCDetail).ItemCode, this.languageComponent1);
                        return;
                    }




                    //修改检查状态时的check
                    if (((IQCDetail)objectIQCDetail).CheckStatus != FormatHelper.CleanString(this.rblPass.SelectedValue))
                    {
                        if (((IQCDetail)objectIQCDetail).STDStatus == IQCStatus.IQCStatus_Close)
                        {
                            WebInfoPublish.Publish(this, "$Error_Close_Not_Change_CheckStatus $STLINE:" + ((IQCDetail)objectIQCDetail).STLine, this.languageComponent1);
                            return;
                        }
                    }
                    else
                    {
                        object objectIQCHead = this._IQCFacade.GetIQCHead(((IQCDetail)objectIQCDetail).STNo);

                        if (objectIQCHead != null && ((IQCHead)objectIQCHead).Status != IQCStatus.IQCStatus_WaitCheck)
                        {
                            WebInfoPublish.Publish(this, "$IQHead_Status_NOT_WaitCheck  $IQCNo:" + ((IQCHead)objectIQCHead).IQCNo + " $STLINE:" + ((IQCDetail)objectIQCDetail).STLine, this.languageComponent1);
                            return;
                        }
                    }

                    if (((IQCDetail)objectIQCDetail).ConcessionStatus == IQCConcessionStatus.IQCConcessionStatus_Y)
                    {
                        WebInfoPublish.Publish(this, "$Error_HaveConcession_Not_Change_CheckStatus $STLINE:" + ((IQCDetail)objectIQCDetail).STLine, this.languageComponent1);
                        return;
                    }

                    object materialReceive = this._IQCFacade.GetMaterialReceive(((IQCDetail)objectIQCDetail).IQCNo, ((IQCDetail)objectIQCDetail).STLine);

                    if (materialReceive != null)
                    {
                        WebInfoPublish.Publish(this, "$Error_HaveMaterialReceive_Not_Change_CheckStatus $STLINE:" + ((IQCDetail)objectIQCDetail).STLine, this.languageComponent1);
                        return;
                    }


                    ASN objectASN = (ASN)this._IQCFacade.GetASN(((IQCDetail)objectIQCDetail).STNo);

                    if (objectASN != null && objectASN.STStatus == IQCStatus.IQCStatus_Release)
                    {
                        WebInfoPublish.Publish(this, "$Error_ASNStatus_NOT_WaitCheck $ASN:" + ((IQCDetail)objectIQCDetail).STNo, this.languageComponent1);
                        return;
                    }


                    this.DataProvider.BeginTransaction();
                    try
                    {
                        //增加检验项目
                        IQCTestData iqcTestData;
                        if (this.gridWebGridF.Rows != null)
                        {
                            foreach (GridRecord row in this.gridWebGridF.Rows)
                            {
                                if (row.Items.FindItemByKey("Check").Value.ToString() == "True")
                                {
                                    iqcTestData = new IQCTestData();
                                    iqcTestData.CheckGroup = row.Items.FindItemByKey("CheckGroup").Value.ToString();
                                    iqcTestData.CKItemCode = row.Items.FindItemByKey("CheckItemCode").Value.ToString();
                                    iqcTestData.IQCNO = ((IQCDetail)objectIQCDetail).IQCNo;
                                    iqcTestData.ItemCode = ((IQCDetail)objectIQCDetail).ItemCode;
                                    iqcTestData.STNO = ((IQCDetail)objectIQCDetail).STNo;
                                    iqcTestData.STLine = ((IQCDetail)objectIQCDetail).STLine;
                                    iqcTestData.USL = float.Parse(row.Items.FindItemByKey("SetValueMax").Value == null || row.Items.FindItemByKey("SetValueMax").Value.ToString() == "" ? "0" : row.Items.FindItemByKey("SetValueMax").Value.ToString());
                                    iqcTestData.LSL = float.Parse(row.Items.FindItemByKey("SetValueMin").Value == null || row.Items.FindItemByKey("SetValueMin").Value.ToString() == "" ? "0" : row.Items.FindItemByKey("SetValueMin").Value.ToString());
                                    iqcTestData.TestingValue = row.Items.FindItemByKey("RealValue").Value == null ? "" : row.Items.FindItemByKey("RealValue").Value.ToString();
                                    iqcTestData.TestingDate = currentDateTime.DBDate;
                                    iqcTestData.TestingTime = currentDateTime.DBTime;

                                    if (row.Items.FindItemByKey("UnPass").Value.ToString() == "True")
                                    {
                                        iqcTestData.TestingResult = "NG";
                                    }
                                    else
                                    {
                                        iqcTestData.TestingResult = "GOOD";
                                    }
                                    _IQCFacade.AddIQCTestData(iqcTestData);
                                }
                            }
                        }


                        //更新入库单
                        InvReceiptDetail objIRD = (InvReceiptDetail)this._IQCFacade.GetInvReceiptDetailForUpdate(((IQCDetail)objectIQCDetail).STNo, ((IQCDetail)objectIQCDetail).STLine);
                        if (FormatHelper.CleanString(this.rblPass.SelectedValue) == IQCCheckStatus.IQCCheckStatus_Qualified)
                        {
                            objIRD.Qualifyqty = Convert.ToInt32(((IQCDetail)objectIQCDetail).ReceiveQty) - Convert.ToInt32(((IQCDetail)objectIQCDetail).NGQty);
                            objIRD.Iqcstatus = "Qualified";
                        }
                        else if (FormatHelper.CleanString(this.rblPass.SelectedValue) == IQCCheckStatus.IQCCheckStatus_UnQualified)
                        {
                            objIRD.Qualifyqty = 0;
                            objIRD.Iqcstatus = "UNQualified";
                        }
                        objIRD.Muser = this.GetUserCode();
                        objIRD.Mdate = currentDateTime.DBDate;
                        objIRD.Mtime = currentDateTime.DBTime;
                        this._IQCFacade.UpdateInvReceiptDetail(objIRD);

                        this.DataProvider.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        this.DataProvider.RollbackTransaction();
                        ExceptionManager.Raise(objectIQCDetail.GetType(), "$Error_Update_Domain_Object", ex);
                        //throw ex;
                        return;

                    }

                    //
                    IQCDetail[] IQCDetailList = new IQCDetail[] { (IQCDetail)objectIQCDetail };
                    this._IQCFacade.CheckIQCDetail(IQCDetailList, this.GetUserCode(), FormatHelper.CleanString(this.rblPass.SelectedValue));

                }

            }

            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Save);
        }

        protected void cmdCancel_ServerClick(object sender, EventArgs e)
        {
            this.RequestData();
            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Cancel);
        }

        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            //Response.Redirect(this.MakeRedirectUrl("FIQCCheckResultMP.aspx"));
            Response.Redirect(this.MakeRedirectUrl("FIQCCheckResultMP.aspx", new string[] { "IQCNo" }, new string[] { this.ViewState["iqcno"].ToString() }));
        }

        protected void cmdGoodChecked_ServerClick(object sender, EventArgs e)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(this.DataProvider);
            }

            ArrayList GetFromGridRowList = this.gridHelper.GetCheckedRows();

            ArrayList IQCDetailList = new ArrayList(GetFromGridRowList.Count);

            if (GetFromGridRowList.Count > 0)
            {
                foreach (GridRecord row in GetFromGridRowList)
                {
                    object objectIQCDetail = this._IQCFacade.GetIQCDetail(row.Items.FindItemByKey("IQCNo").Text, System.Int32.Parse(row.Items.FindItemByKey("IQCSTLine").Text));
                    if (objectIQCDetail == null)
                    {
                        WebInfoPublish.Publish(this, "$CheckedRow_IS_Not_Exist", this.languageComponent1);
                        return;
                    }

                    //全检抽检的选项，抽样数，备注，点击批量合格时，按实际选择的值进行更新
                    if (!String.IsNullOrEmpty(this.txtSampleQtyEdit.Text))
                    {
                        ((IQCDetail)objectIQCDetail).SampleQty = Math.Round(Convert.ToDecimal(FormatHelper.CleanString(this.txtSampleQtyEdit.Text)), 2);
                    }
                    ((IQCDetail)objectIQCDetail).Memo = this.txtDescriptionEdit.Text.ToString();
                    ((IQCDetail)objectIQCDetail).INSType = this.rblCheckType.SelectedValue.ToLower();

                    object objectIQCHead = this._IQCFacade.GetIQCHead(((IQCDetail)objectIQCDetail).IQCNo);

                    if (objectIQCHead != null && ((IQCHead)objectIQCHead).Status != IQCStatus.IQCStatus_WaitCheck)
                    {
                        WebInfoPublish.Publish(this, "$IQHead_Status_NOT_WaitCheck  $IQCNo:" + ((IQCHead)objectIQCHead).STNo + " $STLINE:" + ((IQCDetail)objectIQCDetail).STLine, this.languageComponent1);
                        return;
                    }

                    if (((IQCDetail)objectIQCDetail).ConcessionStatus == IQCConcessionStatus.IQCConcessionStatus_Y)
                    {
                        WebInfoPublish.Publish(this, "$Error_HaveConcession_Not_Change_CheckStatus $STLINE:" + ((IQCDetail)objectIQCDetail).STLine, this.languageComponent1);
                        return;
                    }

                    object materialReceive = this._IQCFacade.GetMaterialReceive(((IQCDetail)objectIQCDetail).IQCNo, ((IQCDetail)objectIQCDetail).STLine);

                    if (materialReceive != null)
                    {
                        WebInfoPublish.Publish(this, "$Error_HaveMaterialReceive_Not_Change_CheckStatus $STLINE:" + ((IQCDetail)objectIQCDetail).STLine, this.languageComponent1);
                        return;
                    }

                    //更新入库单
                    InvReceiptDetail objIRD = (InvReceiptDetail)this._IQCFacade.GetInvReceiptDetailForUpdate(((IQCDetail)objectIQCDetail).STNo, ((IQCDetail)objectIQCDetail).STLine);
                    objIRD.Qualifyqty = Convert.ToInt32(((IQCDetail)objectIQCDetail).ReceiveQty);
                    objIRD.Iqcstatus = "Qualified";
                    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                    objIRD.Muser = this.GetUserCode();
                    objIRD.Mdate = dbDateTime.DBDate;
                    objIRD.Mtime = dbDateTime.DBTime;
                    this._IQCFacade.UpdateInvReceiptDetail(objIRD);


                    ASN objectASN = (ASN)this._IQCFacade.GetASN(((IQCDetail)objectIQCDetail).STNo);

                    if (objectASN != null && objectASN.STStatus == IQCStatus.IQCStatus_Release)
                    {
                        WebInfoPublish.Publish(this, "$Error_ASNStatus_NOT_WaitCheck $ASN:" + ((IQCDetail)objectIQCDetail).STNo, this.languageComponent1);
                        return;
                    }


                    IQCDetailList.Add((IQCDetail)objectIQCDetail);
                }


                this._IQCFacade.CheckIQCDetail((IQCDetail[])IQCDetailList.ToArray(typeof(IQCDetail)), this.GetUserCode(), IQCCheckStatus.IQCCheckStatus_Qualified);

            }

            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
        }

        protected void cmdRegressToAcceptButton_ServerClick(object sender, EventArgs e)
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblRegressToAcceptNo, this.txtRegressToAcceptNoEdit, 100, false)); ;
            manager.Add(new DecimalCheck(this.lblRegressToAcceptNUmber, this.txtRegressToAcceptNUmberEdit, 1, 9999999999999, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return;
            }

            if (_IQCFacade == null) { _IQCFacade = new IQCFacade(this.DataProvider); }
            object objectIQCDetail = this._IQCFacade.GetIQCDetail(FormatHelper.CleanString(this.txtIQCNo.Text.Trim().ToUpper()),
                                                                 System.Int32.Parse(FormatHelper.CleanString(this.txtIQCLineNoEdit.Text)));

            if (objectIQCDetail == null)
            {
                WebInfoPublish.Publish(this, "$CheckedRow_IS_Not_Exist", languageComponent1);
                return;
            }

            if (Math.Round(((IQCDetail)objectIQCDetail).ReceiveQty, 2) < Math.Round(Convert.ToDecimal(this.txtRegressToAcceptNUmberEdit.Text.Trim()), 2))
            {

                WebInfoPublish.Publish(this, "$ConcessionNumber_Is_TooBig", languageComponent1);
                return;
            }

            if (this._IQCFacade.QueryIQCDetailConcessionCount(((IQCDetail)objectIQCDetail).STNo, ((IQCDetail)objectIQCDetail).ItemCode, IQCConcessionStatus.IQCConcessionStatus_Y) >= 3)
            {
                Response.Write(("<script   language='javascript'>alert('该供应商此种物料超过3次让步');</script>"));
            }

            ((IQCDetail)objectIQCDetail).ConcessionNo = FormatHelper.CleanString(this.txtRegressToAcceptNoEdit.Text.Trim());
            ((IQCDetail)objectIQCDetail).ConcessionQty = Math.Round(Convert.ToDecimal(FormatHelper.CleanString(this.txtRegressToAcceptNUmberEdit.Text.Trim())), 2);
            ((IQCDetail)objectIQCDetail).ConcessionMemo = FormatHelper.CleanString(this.txtRegressToAcceptCaptionEdit.Text.Trim(), 1000);
            ((IQCDetail)objectIQCDetail).ConcessionStatus = IQCConcessionStatus.IQCConcessionStatus_Y;

            this._IQCFacade.UpdateIQCDetail((IQCDetail)objectIQCDetail);


            InvReceiptDetail objIRD = (InvReceiptDetail)this._IQCFacade.GetInvReceiptDetailForUpdate(((IQCDetail)objectIQCDetail).STNo, ((IQCDetail)objectIQCDetail).STLine);
            objIRD.Qualifyqty = Convert.ToInt32(((IQCDetail)objectIQCDetail).ConcessionQty);
            objIRD.Iqcstatus = "Qualified";
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            objIRD.Muser = this.GetUserCode();
            objIRD.Mdate = dbDateTime.DBDate;
            objIRD.Mtime = dbDateTime.DBTime;
            this._IQCFacade.UpdateInvReceiptDetail(objIRD);


            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);


        }

        protected void drpAcceptStyleEdit_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpAcceptStyleEdit.Items.Clear();
                this.drpAcceptStyleEdit.Items.Add(new ListItem("", ""));
                this.drpAcceptStyleEdit.Items.Add(new ListItem(this.languageComponent1.GetString(IQCReceiveType.IQCReceiveType_Normal), IQCReceiveType.IQCReceiveType_Normal));
                this.drpAcceptStyleEdit.Items.Add(new ListItem(this.languageComponent1.GetString(IQCReceiveType.IQCReceiveType_SpecialPurchase), IQCReceiveType.IQCReceiveType_SpecialPurchase));
                this.drpAcceptStyleEdit.Items.Add(new ListItem(this.languageComponent1.GetString(IQCReceiveType.IQCReceiveType_Instead), IQCReceiveType.IQCReceiveType_Instead));
            }

        }

        protected void drpAQLLevel_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpAQLLevel.Items.Clear();
                OQCFacade _OQCFacade = new OQCFacade(this.DataProvider);
                object[] objs = _OQCFacade.GetAllAQLLevel();
                if (objs != null)
                {
                    this.drpAQLLevel.Items.Add(new ListItem("", ""));
                    foreach (AQL obj in objs)
                    {
                        this.drpAQLLevel.Items.Add(new ListItem(obj.AqlLevel, obj.AqlLevel));
                    }
                }
            }

        }

        protected void rblCheckType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblCheckType.SelectedIndex == 1) //抽检
            {
                if (drpAQLLevel.Items.Count > 0)
                {
                    drpAQLLevel.SelectedIndex = 0;
                }
                drpAQLLevel.Enabled = true;
                this.drpAQLLevel_SelectedIndexChanged(null, null);
            }
            else
            {
                drpAQLLevel.SelectedIndex = -1;
                drpAQLLevel.Enabled = false;

                this.txtAdviseSampleQty.Text = this.ViewState["ReceiveQty"].ToString();
            }

        }

        protected void drpAQLLevel_SelectedIndexChanged(object sender, EventArgs e)
        {

            OQCFacade _OQCFacade = new OQCFacade(this.DataProvider);
            object obj = _OQCFacade.GetAQLByLevelAndLotSize((int)Convert.ToDecimal(this.ViewState["ReceiveQty"].ToString() == "" ? 0 : this.ViewState["ReceiveQty"]), drpAQLLevel.SelectedValue);
            if (obj != null)
            {
                this.txtAdviseSampleQty.Text = ((AQL)obj).SampleSize.ToString();
            }
            else
            {
                this.txtAdviseSampleQty.Text = "";
            }

        }

        protected void txtCheckGroupEdit_TextChanged(object sender, EventArgs e)
        {

            this.gridHelperF.RequestData();

        }

        protected override void gridWebGrid_ItemCommand(GridRecord row, string command)
        {
            if (command == "Edit")
            {
                object obj = this.GetEditObject(row);

                if (obj != null)
                {
                    this.SetEditObject(obj);

                    //this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
                }

            }

            if (command == "MaterialCode")
            {
                Response.Redirect(this.MakeRedirectUrl("../BaseSetting/FDocView.aspx", new string[] { "MaterialCode", "IQCNO" }, new string[] { row.Items.FindItemByKey("MaterialCode").Text, this.txtIQCNo.Text }));
            }
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblIQCLineNoEdit, this.txtIQCLineNoEdit, 22, true));
            manager.Add(new LengthCheck(this.lblPIC, this.txtPICEdit, 100, false));
            manager.Add(new LengthCheck(this.lblDescription, this.txtDescriptionEdit, 2000, false));
            manager.Add(new LengthCheck(this.lblAction, this.txtActionEidt, 1000, false));
            manager.Add(new DecimalCheck(this.lblSampleQty, this.txtSampleQtyEdit, 0, int.MaxValue, false));
            manager.Add(new DecimalCheck(this.lblNGQty, this.txtNGQtyEdit, 0, int.MaxValue, false));
            manager.Add(new LengthCheck(this.lblNGDescEdit, this.txtNGDescEdit, 1000, false));

            if (this.rblPass.SelectedIndex == 1)
            {
                manager.Add(new DecimalCheck(this.lblSampleQty, this.txtSampleQtyEdit, 0, int.MaxValue, true));
                manager.Add(new DecimalCheck(this.lblNGQty, this.txtNGQtyEdit, 0, int.MaxValue, true));
                manager.Add(new LengthCheck(this.lblNGDescEdit, this.txtNGDescEdit, 1000, true));
            }

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }
            return true;
        }



        protected void cmdGridExport_ServerClick(object sender, EventArgs e)
        {
            this.excelExporter.Export();
        }
        #endregion

        #region For Page_Load

        //protected override void Render(HtmlTextWriter writer)
        //{

        //    base.Render(writer);
        //    writer.Write("<script language=javascript>try{ResetSelectAllPosition('chbUnSelected','gridWebGrid');ResetSelectAllPosition('chbSelected','gridWebGridF');}catch(e){};</script>");
        //    writer.Write("<script language=javascript>try{if(window.top.valueLoaded != true){document.getElementById('txtSelected').innerText = window.top.dialogArguments.Codes;if(window.top.dialogArguments.DataObject.DocumentSegment){document.getElementById('txtSegmentCodeQuery').value=window.top.dialogArguments.DataObject.DocumentSegment;document.getElementById('txtSegmentCodeQuery').readOnly=true;}document.getElementById('cmdInit').click();window.top.valueLoaded = true ;}}catch(e){};</script>");
        //}

        private void InitParameters()
        {
            if (this.Request.Params["iqcno"] == null)
            {
                this.ViewState["iqcno"] = string.Empty;
            }
            else
            {
                this.ViewState["iqcno"] = this.Request.Params["iqcno"];
            }

            this.txtIQCNo.Text = this.ViewState["iqcno"].ToString();
        }

        public void InitButtonHelp()
        {
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);

            this.buttonHelper.AddButtonConfirm(cmdRegressToAcceptButton, languageComponent1.GetString("ConcessionConfirm"));
            this.buttonHelper.AddButtonConfirm(cmdGoodChecked, languageComponent1.GetString("GoodConfirm"));
        }

        private void InitOnPostBack()
        {
            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);

            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            //this.gridHelperF = new GridHelperNew(this.gridWebGridF, this.DtSource2);
            //this.gridHelperF.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSourceF);
            //this.gridHelperF.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRowF);

            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }

        #endregion

        #region For Query Data

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_IQCFacade == null) { _IQCFacade = new IQCFacade(this.DataProvider); }
            return this._IQCFacade.QueryIQCDetailWithMaterial(FormatHelper.CleanString(this.txtIQCNo.Text.Trim().ToUpper()), string.Empty, IQCStatus.IQCStatus_Cancel, inclusive, exclusive);
        }

        protected object[] LoadDataSourceF(int inclusive, int exclusive)
        {
            OQCFacade _OQCFacade = new OQCFacade(this.DataProvider);
            return _OQCFacade.GetCheckItemByCheckGroupNew(FormatHelper.CleanString(this.txtCheckGroupCodeEdit.Text.Trim().ToUpper()));
        }

        protected override int GetRowCount()
        {
            if (_IQCFacade == null) { _IQCFacade = new IQCFacade(this.DataProvider); }
            return this._IQCFacade.QueryIQCDetailWithMaterialCount(FormatHelper.CleanString(this.txtIQCNo.Text.Trim().ToUpper()), string.Empty, IQCStatus.IQCStatus_Cancel);
        }

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
            this.cmdRegressToAcceptButton.Disabled = true;
            //this.rblCheckType.Enabled = false;
            this.rblPass.Enabled = false;
            this.txtActionEidt.Enabled = false;
            //this.txtDescriptionEdit.Enabled = false;
            this.txtIQCLineNoEdit.Enabled = false;
            this.txtNGDescEdit.Enabled = false;
            this.txtNGQtyEdit.Enabled = false;
            this.txtPICEdit.Enabled = false;
            this.drpAcceptStyleEdit.Enabled = false;
            this.txtRegressToAcceptCaptionEdit.Enabled = false;
            this.txtRegressToAcceptNoEdit.Enabled = false;
            this.txtRegressToAcceptNUmberEdit.Enabled = false;
            //this.txtSampleQtyEdit.Enabled = false;
            this.cmdSave.Disabled = true;
            this.cmdCancel.Disabled = true;

            this.rblCheckType.Enabled = false;
            this.drpAQLLevel.SelectedIndex = -1;
            this.drpAQLLevel.Enabled = false;
            this.txtAdviseSampleQty.Enabled = false;
            this.txtCheckGroupCodeEdit.Enabled = false;
        }

        #endregion

        #region For Grid And Edit

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{
            //        "false",
            //        ((IQCDetailWithMaterial)obj).IQCNo ,
            //        ((IQCDetailWithMaterial)obj).STNo,
            //        ((IQCDetailWithMaterial)obj).STLine ,
            //        ((IQCDetailWithMaterial)obj).ItemCode ,
            //        ((IQCDetailWithMaterial)obj).MaterialDescription ,
            //        this.languageComponent1.GetString(((IQCDetailWithMaterial)obj).STDStatus) ,
            //        ((IQCDetailWithMaterial)obj).IsInStorage,
            //         ((IQCDetailWithMaterial)obj).Unit ,
            //         Math.Round(((IQCDetailWithMaterial)obj).ReceiveQty,2),

            //        ((IQCDetailWithMaterial)obj).OrderLine,
            //        this.languageComponent1.GetString(((IQCDetailWithMaterial)obj).INSType),
            //        this.languageComponent1.GetString(((IQCDetailWithMaterial)obj).CheckStatus),
            //         this.languageComponent1.GetString(((IQCDetailWithMaterial)obj).Attribute),
            //         Math.Round(((IQCDetailWithMaterial)obj).SampleQty,2),
            //        Math.Round(((IQCDetailWithMaterial)obj).NGQty,2),
            //        ((IQCDetailWithMaterial)obj).MemoEx,
            //        ((IQCDetailWithMaterial)obj).PurchaseMEMO,
            //        ((IQCDetailWithMaterial)obj).Memo,
            //        this.languageComponent1.GetString(((IQCDetailWithMaterial)obj).Type),
            //        ((IQCDetailWithMaterial)obj).Action,
            //        Math.Round(((IQCDetailWithMaterial)obj).ConcessionQty,2),
            //        ((IQCDetailWithMaterial)obj).ConcessionNo,
            //        ((IQCDetailWithMaterial)obj).ConcessionMemo,
            //        ""
            //    });
            DataRow row = this.DtSource.NewRow();
            row["IQCNo"] = ((IQCDetailWithMaterial)obj).IQCNo;
            row["Receiptno"] = ((IQCDetailWithMaterial)obj).STNo;
            row["IQCSTLine"] = ((IQCDetailWithMaterial)obj).STLine;
            row["MaterialCode"] = ((IQCDetailWithMaterial)obj).ItemCode;
            row["MaterialDesc"] = ((IQCDetailWithMaterial)obj).MaterialDescription;
            row["STDStatus"] = this.languageComponent1.GetString(((IQCDetailWithMaterial)obj).STDStatus);
            row["IsInStorage"] = ((IQCDetailWithMaterial)obj).IsInStorage;
            row["Unit"] = ((IQCDetailWithMaterial)obj).Unit;
            row["ReceiveQty"] = Math.Round(((IQCDetailWithMaterial)obj).ReceiveQty, 2);
            row["OrderLine"] = ((IQCDetailWithMaterial)obj).OrderLine;
            row["INSType"] = this.languageComponent1.GetString(((IQCDetailWithMaterial)obj).INSType);
            row["IQCResult"] = this.languageComponent1.GetString(((IQCDetailWithMaterial)obj).CheckStatus);
            row["AttriBute"] = this.languageComponent1.GetString(((IQCDetailWithMaterial)obj).Attribute);
            row["SampleQty"] = Math.Round(((IQCDetailWithMaterial)obj).SampleQty, 2);
            row["NGQty"] = Math.Round(((IQCDetailWithMaterial)obj).NGQty, 2);
            row["MemoEx"] = ((IQCDetailWithMaterial)obj).MemoEx;
            row["PurchaseMEMO"] = ((IQCDetailWithMaterial)obj).PurchaseMEMO;
            row["Memo"] = ((IQCDetailWithMaterial)obj).Memo;
            row["Type"] = this.languageComponent1.GetString(((IQCDetailWithMaterial)obj).Type);
            row["Action"] = ((IQCDetailWithMaterial)obj).Action;
            row["ConcessionQTY"] = Math.Round(((IQCDetailWithMaterial)obj).ConcessionQty, 2);
            row["ConcessionNO"] = ((IQCDetailWithMaterial)obj).ConcessionNo;
            row["ConcessionMemo"] = ((IQCDetailWithMaterial)obj).ConcessionMemo;
            return row;

        }

        protected DataRow GetGridRowF(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{
            //        "true",
            //        ((OQCCheckListQuery)obj).CheckGroupCode ,
            //        ((OQCCheckListQuery)obj).CheckItemCode ,
            //        ((OQCCheckListQuery)obj).CheckValueMin,
            //        ((OQCCheckListQuery)obj).CheckValueMax ,
            //        "" ,
            //        "false"

            //    });
            DataRow row = this.DtSource2.NewRow();
            row["GUID"] = Guid.NewGuid().ToString();
            row["CheckGroup"] = ((OQCCheckListQuery)obj).CheckGroupCode;
            row["CheckItemCode"] = ((OQCCheckListQuery)obj).CheckItemCode;
            row["SetValueMin"] = ((OQCCheckListQuery)obj).CheckValueMin;
            row["SetValueMax"] = ((OQCCheckListQuery)obj).CheckValueMax;
            row["RealValue"] = "";
            row["UnPass"] = false;
       
            return row;
        }


        protected override void SetEditObject(object obj)
        {
            this.DtSource2.Rows.Clear();
            this.DtSource2.AcceptChanges();
            this.gridWebGridF.DataSource = this.DtSource2;
            this.gridWebGridF.DataBind();

            if (obj == null)
            {
                this.txtIQCLineNoEdit.Text = string.Empty;
                if (this.drpAcceptStyleEdit.Items.Count > 0)
                {
                    this.drpAcceptStyleEdit.SelectedIndex = 0;
                }
                if (this.drpAQLLevel.Items.Count > 0)
                {
                    this.drpAQLLevel.SelectedIndex = 0;
                }

                this.txtAdviseSampleQty.Text = string.Empty;
                this.txtSampleQtyEdit.Text = string.Empty;
                this.txtNGQtyEdit.Text = string.Empty;
                this.txtPICEdit.Text = string.Empty;
                this.txtActionEidt.Text = string.Empty;
                this.txtNGDescEdit.Text = string.Empty;
                this.txtDescriptionEdit.Text = string.Empty;
                this.txtRegressToAcceptNUmberEdit.Text = string.Empty;
                this.txtRegressToAcceptNoEdit.Text = string.Empty;
                this.txtRegressToAcceptCaptionEdit.Text = string.Empty;

                this.txtCheckGroupCodeEdit.Text = string.Empty;
                return;
            }

            if (((IQCDetail)obj).STDStatus.ToString() == IQCStatus.IQCStatus_WaitCheck)
            {
                this.txtIQCLineNoEdit.Enabled = false;
                this.drpAcceptStyleEdit.Enabled = true;
                //this.txtSampleQtyEdit.Enabled = true;
                this.txtNGQtyEdit.Enabled = true;
                this.txtPICEdit.Enabled = true;
                this.txtActionEidt.Enabled = true;
                this.txtNGDescEdit.Enabled = true;
                //this.txtDescriptionEdit.Enabled = true;
                //this.rblCheckType.Enabled = true;
                this.rblPass.Enabled = true;
                this.cmdSave.Disabled = false;

                this.rblCheckType.Enabled = true;
                this.txtAdviseSampleQty.Enabled = true;
                this.txtCheckGroupCodeEdit.Enabled = true;
            }
            else if (((IQCDetail)obj).STDStatus.ToString() == IQCStatus.IQCStatus_Close)
            {
                this.txtPICEdit.Enabled = true;
                this.txtActionEidt.Enabled = true;
                this.txtIQCLineNoEdit.Enabled = false;
                this.drpAcceptStyleEdit.Enabled = false;
                //this.txtSampleQtyEdit.Enabled = false;
                this.txtNGQtyEdit.Enabled = false;
                this.txtNGDescEdit.Enabled = false;
                //this.txtDescriptionEdit.Enabled = false;
                //this.rblCheckType.Enabled = false;
                this.rblPass.Enabled = false;
                this.cmdSave.Disabled = false;

                this.rblCheckType.Enabled = true;
                this.txtAdviseSampleQty.Enabled = true;
                this.txtCheckGroupCodeEdit.Enabled = true;
            }
            else
            {
                this.txtPICEdit.Enabled = false;
                this.txtActionEidt.Enabled = false;
                this.txtIQCLineNoEdit.Enabled = false;
                this.drpAcceptStyleEdit.Enabled = false;
                //this.txtSampleQtyEdit.Enabled = false;
                this.txtNGQtyEdit.Enabled = false;
                this.txtNGDescEdit.Enabled = false;
                //this.txtDescriptionEdit.Enabled = false;
                //this.rblCheckType.Enabled = false;
                this.rblPass.Enabled = false;
                this.cmdSave.Disabled = true;

                this.rblCheckType.Enabled = true;
                this.txtAdviseSampleQty.Enabled = true;
                this.txtCheckGroupCodeEdit.Enabled = true;
            }

            if (((IQCDetail)obj).ConcessionStatus.ToString() == IQCConcessionStatus.IQCConcessionStatus_Y)
            {
                this.txtRegressToAcceptCaptionEdit.Enabled = false;
                this.txtRegressToAcceptNoEdit.Enabled = false;
                this.txtRegressToAcceptNUmberEdit.Enabled = false;
                this.cmdRegressToAcceptButton.Disabled = true;
            }

            this.txtIQCLineNoEdit.Text = ((IQCDetail)obj).STLine.ToString();
            this.drpAcceptStyleEdit.Text = ((IQCDetail)obj).Type.ToString();
            this.txtSampleQtyEdit.Text = Convert.ToString((Math.Round(((IQCDetail)obj).SampleQty, 2)));
            this.txtNGQtyEdit.Text = Convert.ToString((Math.Round(((IQCDetail)obj).NGQty, 2)));
            this.txtPICEdit.Text = ((IQCDetail)obj).PIC.ToString();
            this.txtActionEidt.Text = ((IQCDetail)obj).Action.ToString();
            this.txtNGDescEdit.Text = ((IQCDetail)obj).MemoEx.ToString();
            this.txtDescriptionEdit.Text = ((IQCDetail)obj).Memo.ToString();

            this.txtRegressToAcceptNUmberEdit.Text = Convert.ToString(Math.Round(((IQCDetail)obj).ConcessionQty, 2));
            this.txtRegressToAcceptNoEdit.Text = ((IQCDetail)obj).ConcessionNo.ToString();
            this.txtRegressToAcceptCaptionEdit.Text = ((IQCDetail)obj).ConcessionMemo.ToString();
            this.txtAdviseSampleQty.Text = "";
            this.ViewState["ReceiveQty"] = ((IQCDetail)obj).ReceiveQty.ToString();

            this.txtCheckGroupCodeEdit.Text = "";
            //this.gridHelperF.Grid.Clear();

            if (((IQCDetail)obj).CheckStatus.ToString() == IQCCheckStatus.IQCCheckStatus_UnQualified)
            {
                this.cmdRegressToAcceptButton.Disabled = false;
                this.txtRegressToAcceptNUmberEdit.Enabled = true;
                this.txtRegressToAcceptNoEdit.Enabled = true;
                this.txtRegressToAcceptCaptionEdit.Enabled = true;
            }
            else
            {
                this.cmdRegressToAcceptButton.Disabled = true;
            }

            if (((IQCDetail)obj).INSType.ToString().ToLower() == IQCCheckType.IQCCheckType_All.ToLower())
            {
                this.rblCheckType.SelectedIndex = 0;
            }
            else
            {
                this.rblCheckType.SelectedIndex = 1;
            }
            this.rblCheckType_SelectedIndexChanged(null, null);

            if (((IQCDetail)obj).CheckStatus.ToString() == IQCCheckStatus.IQCCheckStatus_Qualified)
            {
                this.rblPass.SelectedIndex = 0;
            }
            else
            {
                this.rblPass.SelectedIndex = 1;
            }


        }

        protected override object GetEditObject()
        {
            if (this.ValidateInput())
            {
                DBDateTime DBDateTimeNow = FormatHelper.GetNowDBDateTime(this.DataProvider);
                if (_IQCFacade == null) { _IQCFacade = new IQCFacade(this.DataProvider); }

                object GetIQCDetail = this._IQCFacade.GetIQCDetail(FormatHelper.CleanString(this.txtIQCNo.Text.Trim().ToUpper()),
                                                                                 System.Int32.Parse(FormatHelper.CleanString(this.txtIQCLineNoEdit.Text)));
                if (GetIQCDetail == null)
                {
                    return null;
                }


                if (((IQCDetail)GetIQCDetail).STDStatus == IQCStatus.IQCStatus_WaitCheck)
                {
                    ((IQCDetail)GetIQCDetail).IQCNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtIQCNo.Text));
                    ((IQCDetail)GetIQCDetail).STLine = System.Int32.Parse(FormatHelper.CleanString(this.txtIQCLineNoEdit.Text));
                    ((IQCDetail)GetIQCDetail).Type = FormatHelper.CleanString(this.drpAcceptStyleEdit.SelectedValue);
                    ((IQCDetail)GetIQCDetail).SampleQty = Math.Round(Convert.ToDecimal(FormatHelper.CleanString(this.txtSampleQtyEdit.Text)), 2);
                    ((IQCDetail)GetIQCDetail).NGQty = Math.Round(Convert.ToDecimal(FormatHelper.CleanString(this.txtNGQtyEdit.Text)), 2);
                    ((IQCDetail)GetIQCDetail).MemoEx = FormatHelper.CleanString(this.txtNGDescEdit.Text.ToString(), 1000);
                    ((IQCDetail)GetIQCDetail).Memo = FormatHelper.CleanString(this.txtDescriptionEdit.Text.ToString(), 2000);
                    ((IQCDetail)GetIQCDetail).INSType = FormatHelper.CleanString(this.rblCheckType.SelectedValue.ToLower());
                    ((IQCDetail)GetIQCDetail).MaintainDate = DBDateTimeNow.DBDate;
                    ((IQCDetail)GetIQCDetail).MaintainTime = DBDateTimeNow.DBTime;
                    ((IQCDetail)GetIQCDetail).MaintainUser = this.GetUserCode();
                }

                ((IQCDetail)GetIQCDetail).PIC = FormatHelper.CleanString(this.txtPICEdit.Text);
                ((IQCDetail)GetIQCDetail).Action = this.txtActionEidt.Text.ToString();


                return GetIQCDetail;
            }
            else
            {
                return null;
            }
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(this.DataProvider);
            }
            object obj = _IQCFacade.GetIQCDetail(row.Items[1].Text.ToString(), System.Int32.Parse(row.Items[3].Text.ToString()));

            if (obj != null)
            {
                return (IQCDetail)obj;
            }

            return null;
        }

        #endregion

        #region For Export To Excel

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{
               ((IQCDetailWithMaterial)obj).IQCNo ,
                    ((IQCDetailWithMaterial)obj).STNo,
                    ((IQCDetailWithMaterial)obj).STLine.ToString() ,
                    ((IQCDetailWithMaterial)obj).ItemCode ,
                    ((IQCDetailWithMaterial)obj).MaterialDescription ,
                    this.languageComponent1.GetString(((IQCDetailWithMaterial)obj).STDStatus) ,
                    ((IQCDetailWithMaterial)obj).IsInStorage ,
                     ((IQCDetailWithMaterial)obj).Unit ,
                     Convert.ToString(Math.Round(((IQCDetailWithMaterial)obj).ReceiveQty,2)),
                    ((IQCDetailWithMaterial)obj).OrderLine.ToString(),
                    this.languageComponent1.GetString(((IQCDetailWithMaterial)obj).INSType),
                    this.languageComponent1.GetString(((IQCDetailWithMaterial)obj).CheckStatus),
                     this.languageComponent1.GetString(((IQCDetailWithMaterial)obj).Attribute),
                    Convert.ToString(Math.Round(((IQCDetailWithMaterial)obj).SampleQty,2)),
                    Convert.ToString(Math.Round(((IQCDetailWithMaterial)obj).NGQty,2)),
                    ((IQCDetailWithMaterial)obj).MemoEx,
                    ((IQCDetailWithMaterial)obj).PurchaseMEMO,
                    ((IQCDetailWithMaterial)obj).Memo,
                    this.languageComponent1.GetString(((IQCDetailWithMaterial)obj).Type),
                    ((IQCDetailWithMaterial)obj).Action,
                    Convert.ToString(Math.Round(((IQCDetailWithMaterial)obj).ConcessionQty,2)),
                    ((IQCDetailWithMaterial)obj).ConcessionNo  ,
                     ((IQCDetailWithMaterial)obj).ConcessionMemo 
                           
                            };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {

                                "IQCNo",
                                "Receiptno",
                                "IQCSTLine",
                                "MaterialCode",
                                "MaterialDesc",
                                "STDStatus",
                                "IsInStorage",
                                "Unit",
                                "ReceiveQty",
                                "OrderLine",
                                "INSType",
                                "CheckStatus",
                                "AttriBute",
                                "SampleQty",
                                "NGQty",
                                "MemoEx",
                                "PurchaseMEMO",
                                "Memo",
                                "Type",
                                "Action",
                                "ConcessionQTY",
                                "ConcessionNO",
                                "ConcessionMemo"
                                };
        }

        #endregion

    }
}
