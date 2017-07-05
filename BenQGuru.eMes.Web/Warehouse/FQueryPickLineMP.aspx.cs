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
    public partial class FQueryPickLineMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
        private InventoryFacade facade = null;
        private InventoryFacade _InventoryFacade = null;
        private WarehouseFacade _WarehouseFacade = null;
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
                lblMOSelectMOViewField.Visible = false;
                this.InitWebGrid();
                this.cmdQuery_Click(null, null);
                this.RequestData();
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

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
            #region
            //this.gridHelper.AddColumn("PickNo", "拣货任务令号",null);
            //  for (int i = 0; i < this.PickHeadViewFieldList.Length; i++)
            //  {
            //      this.gridHelper.AddColumn(this.PickHeadViewFieldList[i].FieldName, /*this.languageComponent1.GetString(*/this.PickHeadViewFieldList[i].Description/*)*/, null);
            //  }
            //  this.gridHelper.AddDefaultColumn(true, false);
            //  this.gridHelper.AddLinkColumn("LinkDetailMaterial", "已拣明细");
            //this.gridWebGrid.Columns.FromKey("PickNo").Hidden = true;
            #endregion
            //												

            this.gridHelper.AddColumn("PickNo", "拣货任务令号", null);
            this.gridHelper.AddColumn("PickLine", "行号", null);
            this.gridHelper.AddColumn("Status", "状态", null);
            this.gridHelper.AddColumn("DQMCode", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("MDesc", "物料描述", null);
            this.gridHelper.AddColumn("HWItemCode", "华为物料编码", null);
            this.gridHelper.AddColumn("QTY", "数量", null);
            this.gridHelper.AddColumn("Unit", "单位", null);
            this.gridHelper.AddColumn("SQTY", "已拣数量", null);
            this.gridHelper.AddColumn("OutQTY", "已出库数量", null);
            this.gridHelper.AddColumn("OweQTY", "欠料发货数量", null);
            this.gridHelper.AddColumn("CDate", "创建日期", null);
            this.gridHelper.AddColumn("CTime", "创建时间", null);
            this.gridHelper.AddColumn("CUser", "创建人", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);
            this.gridHelper.AddColumn("MaintainUser", "维护人", null);
            this.gridHelper.AddDefaultColumn(true, false);
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            #region MyRegion
            //DataRow row = this.DtSource.NewRow();

            //PickDetailQuery pickLine = obj as PickDetailQuery;
            //Type type = pickLine.GetType();
            //for (int i = 0; i < this.PickHeadViewFieldList.Length; i++)
            //{
            //    ViewField field = this.PickHeadViewFieldList[i];
            //    string strValue = string.Empty;
            //    System.Reflection.FieldInfo fieldInfo = type.GetField(field.FieldName);
            //    if (fieldInfo != null)
            //    {
            //        strValue = fieldInfo.GetValue(pickLine).ToString();
            //    }
            //    if (field.FieldName == "Status")
            //    {
            //        strValue = languageComponent1.GetString(pickLine.Status);
            //    }
            //    if (field.FieldName == "NewOrderNo")
            //    {
            //        strValue = pickLine.OrderNo;
            //    }
            //    row[i + 1] = strValue;
            //}

            //return row;
            
            #endregion
            DataRow row = this.DtSource.NewRow();
            PickDetailQuery p = (PickDetailQuery)obj;
            row["PickNo"] = p.PickNo;
            row["PickLine"] = p.PickLine;
            row["Status"] = languageComponent1.GetString(p.Status);
            row["DQMCode"] = p.DQMCode;
            row["MDesc"] = p.MDesc;

            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            Pick pick = (Pick)_InventoryFacade.GetPick(p.PickNo);
            if (pick.PickType == PickType.PickType_UB)

                row["HWItemCode"] = p.CustMCode; 
            else
                row["HWItemCode"] = p.VEnderItemCode;

            row["QTY"] = p.QTY;
            row["Unit"] = p.Unit;
            row["SQTY"] = p.SQTY.ToString("G0");
            row["OutQTY"] = p.OutQTY.ToString("G0");
            row["OweQTY"] = p.OweQTY.ToString("G0");
            row["CDate"] = FormatHelper.ToDateString(p.CDate);
            row["CTime"] = FormatHelper.ToTimeString(p.CTime);
            row["CUser"] = p.CUser;
            row["MaintainDate"] = FormatHelper.ToDateString(p.MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(p.MaintainTime);
            row["MaintainUser"] = p.MaintainUser;
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
             FormatHelper.CleanString(this.txtCusItemCodeQuery.Text),
             FormatHelper.CleanString(this.txtCusBatchNo.Text),
               FormatHelper.CleanString(this.txtOrderNoQuery.Text),
             inclusive, exclusive);
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
                  FormatHelper.CleanString(this.txtCusItemCodeQuery.Text),
                  FormatHelper.CleanString(this.txtCusBatchNo.Text),
                    FormatHelper.CleanString(this.txtOrderNoQuery.Text)
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
            string pickNo = row.Items.FindItemByKey("PickNo").Text.Trim();
            string pickLine = row.Items.FindItemByKey("PickLine").Text.Trim();
            string DQMCode = row.Items.FindItemByKey("DQMCode").Text.Trim();

            if (commandName == "LinkDetailMaterial")
            {
                Response.Redirect(this.MakeRedirectUrl("FQueryCheckedPickLineMP.aspx",
                         new string[] { "PickNo", "PickLine", "DQMCode" },
                         new string[] { pickNo, pickLine, DQMCode }));
            }
        }

        #region 确认欠料发货数量
        protected void cmdConfirmOweQty_ServerClick(object sender, EventArgs e)
        {
            GetServerClick("cmdConfirmOweQty_ServerClick");
        }

        protected void CmdConfirmOweQtyObjects(object[] pickList)
        {
            // 点击该按钮，初始TBLPICKDETAIL. OweQTY为空or零，
            //点击该按钮后保存TBLPICKDETAIL. OweQTY= TBLPICKDETAIL. SQTY
            //注：只有状态为：Owe:欠料的物料行可以执行此按钮，确认后该行更新状态为ClosePick:拣料完成
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            try
            {
                this.DataProvider.BeginTransaction();
                foreach (PickDetail pickdetail in pickList)
                {
                    if (pickdetail.Status == PickDetail_STATUS.Status_Owe)
                    {
                        if (pickdetail.OweQTY == 0 || string.IsNullOrEmpty(pickdetail.OweQTY.ToString()))
                        {
                            PickDetail oldpickdetail = (PickDetail)facade.GetPickDetail(pickdetail.PickNo, pickdetail.PickLine);
                            oldpickdetail.OweQTY = oldpickdetail.SQTY;
                            oldpickdetail.Status = PickDetail_STATUS.Status_ClosePick;
                            facade.UpdatePickDetail(oldpickdetail);
                        }
                        else
                        {
                            WebInfoPublish.Publish(this, "已申请欠料发货！", this.languageComponent1);
                            return;
                        }
                    }
                    else
                    {
                        WebInfoPublish.Publish(this, "只有状态为欠料的物料行可以执行此按钮", this.languageComponent1);
                        return;
                    }
                }

                #region 在invinouttrans表中增加一条数据 箱单完成日期
                WarehouseFacade wfacade = new WarehouseFacade(base.DataProvider);
                DBDateTime dbTime1 = FormatHelper.GetNowDBDateTime(this.DataProvider);
                foreach (PickDetail pickDetail in pickList)
                {
                    Pick pick = facade.GetPick(pickDetail.PickNo) as Pick;
                    InvInOutTrans trans = wfacade.CreateNewInvInOutTrans();
                    trans.CartonNO = string.Empty;
                    trans.DqMCode = pickDetail.DQMCode;
                    trans.FacCode = string.Empty;
                    trans.FromFacCode = string.Empty;
                    trans.FromStorageCode = string.Empty;
                    trans.InvNO = pick.InvNo;//.InvNo;
                    trans.InvType = pick.PickType;
                    trans.LotNo = string.Empty;
                    trans.MaintainDate = dbTime1.DBDate;
                    trans.MaintainTime = dbTime1.DBTime;
                    trans.MaintainUser = this.GetUserCode();
                    trans.MCode = pickDetail.MCode;
                    trans.ProductionDate = 0;
                    trans.Qty = pickDetail.QTY;
                    trans.Serial = 0;
                    trans.StorageAgeDate = 0;
                    trans.StorageCode = string.Empty;
                    trans.SupplierLotNo = string.Empty;
                    trans.TransNO = pickDetail.PickNo;
                    trans.TransType = "OUT";
                    trans.Unit = string.Empty;
                    trans.ProcessType = "ClosePick";
                    wfacade.AddInvInOutTrans(trans);
                }

                #endregion

                string pickno = FormatHelper.CleanString(this.txtPickNoQuery.Text);
                bool isUpdatePick = facade.QueryPickDetailCount(pickno, PickDetail_STATUS.Status_ClosePick) == 0;
                if (isUpdatePick)
                {
                    facade.UpdatePickStatusByPickno(pickno, PickHeadStatus.PickHeadStatus_MakePackingList);
                }
                #region  创建发货箱单

                string carInvNo = string.Empty;
                object obj = _WarehouseFacade.GetCartonInvoices(pickno);
                if (obj == null)
                {
                    CARTONINVOICES CartonH = _WarehouseFacade.CreateNewCartoninvoices();
                    object objLot = _WarehouseFacade.GetNewLotNO("K", dbTime.DBDate.ToString().Substring(2, 6).ToString());
                    Serialbook serbook = _WarehouseFacade.CreateNewSerialbook();
                    if (objLot == null)
                    {
                        #region
                        CartonH.CARINVNO = "K" + dbTime.DBDate.ToString().Substring(2, 6).ToString() + "001";
                        CartonH.PICKNO = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPickNoQuery.Text));
                        CartonH.STATUS = CartonInvoices_STATUS.Status_Release;
                        CartonH.CDATE = dbTime.DBDate;
                        CartonH.CTIME = dbTime.DBTime;
                        CartonH.CUSER = this.GetUserCode();
                        CartonH.MDATE = dbTime.DBDate;
                        CartonH.MTIME = dbTime.DBTime;
                        CartonH.MUSER = this.GetUserCode();
                        _WarehouseFacade.AddCartoninvoices(CartonH);

                        serbook.SNprefix = "K" + dbTime.DBDate.ToString().Substring(2, 6).ToString();
                        serbook.MAXSerial = "2";
                        serbook.MUser = this.GetUserCode();
                        serbook.MDate = dbTime.DBDate;
                        serbook.MTime = dbTime.DBTime;

                        _WarehouseFacade.AddSerialbook(serbook);
                        #endregion
                    }
                    else
                    {
                        #region
                        string MAXNO = (objLot as Serialbook).MAXSerial;
                        string SNNO = (objLot as Serialbook).SNprefix;
                        CartonH.CARINVNO = SNNO + Convert.ToString(MAXNO).PadLeft(3, '0');
                        CartonH.PICKNO = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPickNoQuery.Text));
                        CartonH.STATUS = CartonInvoices_STATUS.Status_Release;
                        CartonH.CDATE = dbTime.DBDate;
                        CartonH.CTIME = dbTime.DBTime;
                        CartonH.CUSER = this.GetUserCode();
                        CartonH.MDATE = dbTime.DBDate;
                        CartonH.MTIME = dbTime.DBTime;
                        CartonH.MUSER = this.GetUserCode();
                        _WarehouseFacade.AddCartoninvoices(CartonH);

                        //更新tblserialbook
                        serbook.SNprefix = SNNO;
                        serbook.MAXSerial = Convert.ToString((int.Parse(MAXNO) + 1));
                        serbook.MUser = this.GetUserCode();
                        serbook.MDate = dbTime.DBDate;
                        serbook.MTime = dbTime.DBTime;
                        _WarehouseFacade.UpdateSerialbook(serbook);
                        #endregion
                    }
                }
                # endregion
                this.DataProvider.CommitTransaction();
                WebInfoPublish.Publish(this, "确认欠料发货数量成功", this.languageComponent1);
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            }

        }
        # endregion

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {



          

         

           
          
        
      
            Pick pick = (Pick)_InventoryFacade.GetPick(((PickDetailQuery)obj).PickNo);
            string hwItemCode = string.Empty;
            if (pick.PickType == PickType.PickType_UB)

                hwItemCode = ((PickDetailQuery)obj).CustMCode;
            else
                hwItemCode = ((PickDetailQuery)obj).VEnderItemCode;

            return new string[]{((PickDetailQuery)obj).PickNo,
                                ((PickDetailQuery)obj).PickLine,
                                languageComponent1.GetString( ((PickDetailQuery)obj).Status),
                                ((PickDetailQuery)obj).DQMCode,
                                ((PickDetailQuery)obj).MDesc,
                                hwItemCode,
                                ((PickDetailQuery)obj).QTY.ToString(),
                                ((PickDetailQuery)obj).Unit,
                                ((PickDetailQuery)obj).SQTY.ToString("G0"),
                                ((PickDetailQuery)obj).OutQTY.ToString("G0"),
                                ((PickDetailQuery)obj).OweQTY.ToString("G0"),
                                FormatHelper.ToDateString(((PickDetailQuery)obj).CDate),
                               FormatHelper.ToTimeString(((PickDetailQuery)obj).CTime),
                               ((PickDetailQuery)obj).CUser,
                                FormatHelper.ToDateString(((PickDetailQuery)obj).MaintainDate),

                                FormatHelper.ToTimeString(((PickDetailQuery)obj).MaintainTime),
                             
                           ((PickDetailQuery)obj).MaintainUser,
                              
                                };

        }

        protected override string[] GetColumnHeaderText()
        {




            return new string[] { "拣货任务令号", "行号", "状态", "鼎桥物料编码", "物料描述", "华为物料编码", "数量", "单位", "已拣数量", "已出库数量", "欠料发货数量", "创建日期", "创建时间", "创建人", "维护日期", "维护时间", "维护人" };
        }

        #endregion


        #region GetServerClick

        private void GetServerClick(string clickName)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            object obj = null;

            if (array.Count > 0)
            {
                ArrayList objList = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    obj = this.GetEditObject(row);

                    if (obj != null)
                    {
                        objList.Add(obj);
                    }
                }
                object[] asnList = ((PickDetail[])objList.ToArray(typeof(PickDetail)));
                if (clickName == "cmdConfirmOweQty_ServerClick")
                {
                    this.CmdConfirmOweQtyObjects(asnList);
                }
                this.gridHelper.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            }
        }

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
        #endregion


        #region 返回
        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {


            Response.Redirect(this.MakeRedirectUrl("FQueryPickHeadMP.aspx",
                                new string[] { "PickNo" },
                                new string[] { txtPickNoQuery.Text.Trim().ToUpper()
                                        
                                    }));


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
                    object[] objs = _InventoryFacade.QueryViewFieldByUserCode(this.GetUserCode(), "TBLPICKDETAIL");
                    if (objs != null)
                    {
                        viewFieldList = new ViewField[objs.Length];
                        objs.CopyTo(viewFieldList, 0);
                    }
                    else
                    {
                        objs = _InventoryFacade.QueryViewFieldDefault("PickLine_FIELD_LIST_SYSTEM_DEFAULT", "TBLPICKDETAIL");
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
