using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using UserControl;
using Infragistics.Win.UltraWinGrid;

using BenQGuru.eMES.CodeSoftPrint;
using BenQGuru.eMES.Domain.Warehouse;


namespace BenQGuru.eMES.Client
{
    public partial class FMaterialLabPrint : Form
    {
        #region  变量

        private UltraWinGridHelper _UltraWinGridHelper1 = null;
        private IDomainDataProvider _DomainDataProvider = ApplicationService.Current().DataProvider;
        private DataTable _DataTable = new DataTable();

        private InventoryFacade _InventoryFacade = null;

        Messages msg = new Messages();

        #endregion

        #region 属性

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _DomainDataProvider;
            }
        }

        #endregion

        public FMaterialLabPrint()
        {
            InitializeComponent();
        }

        private void FMaterialLabPrint_Load(object sender, EventArgs e)
        {
            InitializeUltraGrid();
            this.InitDateTime();
            this.LoadPrinter();
            this.BindStorage();
        }

        private void BindStorage()
        {
            ucLabelComboxStorage.ComboBoxData.Items.Clear();
            this._InventoryFacade = new InventoryFacade(this.DataProvider);
            object[] objs = _InventoryFacade.GetAllStorage();
            ucLabelComboxStorage.AddItem("", "");
            if (objs != null)
            {
                foreach (object obj in objs)
                {
                    Storage storage = (Storage)obj;
                    if (storage != null)
                    {
                        ucLabelComboxStorage.AddItem(storage.StorageName, storage.StorageCode);
                    }
                }
            }
        }

        private void InitDateTime()
        {
            this.ucStartDate.Value = DateTime.Today;
            this.ucEndDate.Value = DateTime.Today;
        }

        protected void ShowMessage(string message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }

        protected void ShowMessage(Exception e)
        {
            ApplicationRun.GetInfoForm().Add(new UserControl.Message(e));
        }

        protected void ShowMessage(Messages messages)
        {
            ApplicationRun.GetInfoForm().Add(messages);
        }

        protected void ShowMessage(UserControl.Message message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }

        #region 初始化Grid

        private void InitUltraGridUI(UltraGrid ultraGrid)
        {
            ultraGrid.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            ultraGrid.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGrid.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGrid.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGrid.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            ultraGrid.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            ultraGrid.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            ultraGrid.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            ultraGrid.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void InitializeUltraGrid()
        {
            InitUltraGridUI(this.ultraGridMetrialDetial);

            _DataTable.Columns.Add("Check", typeof(bool));
            _DataTable.Columns.Add("MaterialLot", typeof(string));
            _DataTable.Columns.Add("IQCNO", typeof(string));
            _DataTable.Columns.Add("IQCLine", typeof(int));
            _DataTable.Columns.Add("VendorCode", typeof(string));

            _DataTable.Columns.Add("VendorDesc", typeof(string));//供应商描述


            _DataTable.Columns.Add("ItemCode", typeof(string));
            _DataTable.Columns.Add("ItemCodeDesc", typeof(string));
            _DataTable.Columns.Add("ReceiveDate", typeof(int));
            _DataTable.Columns.Add("StockInQty", typeof(int));
            _DataTable.Columns.Add("lotqty", typeof(int));
            _DataTable.Columns.Add("FifoChcek", typeof(string));
            _DataTable.Columns.Add("Storage", typeof(string));


            _DataTable.Columns["Check"].ReadOnly = false;
            _DataTable.Columns["MaterialLot"].ReadOnly = true;
            _DataTable.Columns["IQCNO"].ReadOnly = true;
            _DataTable.Columns["IQCLine"].ReadOnly = true;
            _DataTable.Columns["VendorCode"].ReadOnly = true;
            _DataTable.Columns["VendorDesc"].ReadOnly = true; ;//供应商描述
            _DataTable.Columns["ItemCode"].ReadOnly = true;
            _DataTable.Columns["ItemCodeDesc"].ReadOnly = true;
            _DataTable.Columns["ReceiveDate"].ReadOnly = true;
            _DataTable.Columns["StockInQty"].ReadOnly = true;
            _DataTable.Columns["lotqty"].ReadOnly = false;
            _DataTable.Columns["FifoChcek"].ReadOnly = false;
            _DataTable.Columns["Storage"].ReadOnly = true;

            this.ultraGridMetrialDetial.DataSource = this._DataTable;

            _DataTable.Clear();

            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["Check"].Width = 16;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["MaterialLot"].Width = 128;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["IQCNO"].Width = 115;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["IQCLine"].Width = 50;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["VendorCode"].Width = 48;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["VendorDesc"].Width = 68;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["ItemCode"].Width = 60;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["ItemCodeDesc"].Width = 73;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["ReceiveDate"].Width = 59;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["StockInQty"].Width = 59;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["lotqty"].Width = 59;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["FifoChcek"].Width = 60;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["FifoChcek"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["FifoChcek"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["Storage"].Width = 60;

            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["Check"].CellActivation = Activation.AllowEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["MaterialLot"].CellActivation = Activation.NoEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["IQCNO"].CellActivation = Activation.NoEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["IQCLine"].CellActivation = Activation.NoEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["VendorCode"].CellActivation = Activation.NoEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["VendorDesc"].CellActivation = Activation.NoEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["ItemCode"].CellActivation = Activation.NoEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["ItemCodeDesc"].CellActivation = Activation.NoEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["ReceiveDate"].CellActivation = Activation.NoEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["StockInQty"].CellActivation = Activation.NoEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["lotqty"].CellActivation = Activation.AllowEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["FifoChcek"].CellActivation = Activation.NoEdit;
            ultraGridMetrialDetial.DisplayLayout.Bands[0].Columns["Storage"].CellActivation = Activation.NoEdit;

            ultraGridMetrialDetial.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
        }

        private void ultraGridMetrialDetial_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            _UltraWinGridHelper1 = new UltraWinGridHelper(this.ultraGridMetrialDetial);

            _UltraWinGridHelper1.AddCheckColumn("Check", "");
            _UltraWinGridHelper1.AddReadOnlyColumn("MaterialLot", "物料批号");
            _UltraWinGridHelper1.AddReadOnlyColumn("IQCNO", "单据号");
            _UltraWinGridHelper1.AddReadOnlyColumn("IQCLine", "行号");
            _UltraWinGridHelper1.AddReadOnlyColumn("VendorCode", "供应商");
            _UltraWinGridHelper1.AddReadOnlyColumn("VendorDesc", "供应商描述");
            _UltraWinGridHelper1.AddReadOnlyColumn("ItemCode", "物料代码");
            _UltraWinGridHelper1.AddReadOnlyColumn("ItemCodeDesc", "物料描述");
            _UltraWinGridHelper1.AddReadOnlyColumn("ReceiveDate", "收料日期");
            _UltraWinGridHelper1.AddReadOnlyColumn("StockInQty", "入库数量");
            _UltraWinGridHelper1.AddReadOnlyColumn("lotqty", "在库数量");
            _UltraWinGridHelper1.AddCommonColumn("FifoChcek", "FIFO检查");
            _UltraWinGridHelper1.AddCommonColumn("Storage", "库别");

        }

        #region DataSource

        private void RequestData()
        {
            this.GridBind();
        }

        private void GridBind()
        {
            object[] objMaterialDetail = this.LoadDataSource();

            _DataTable.Rows.Clear();

            if (objMaterialDetail == null)
            {
                this.ShowMessage("$CS_No_Data_To_Display");//没有符合查询条件的记录
                return;
            }

            foreach (object obj in objMaterialDetail)
            {
                bool rowChecked = true;
                _DataTable.Rows.Add(this.GetRow(obj, rowChecked));
            }
        }

        private object[] LoadDataSource()
        {
            try
            {
                this._InventoryFacade = new InventoryFacade(this.DataProvider);

                object[] objs = this._InventoryFacade.QueryMaterialLotPrintLabel(
                   FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucBatch.Value)),
                   FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucIQCNO.Value)),
                   FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLEItemCodeQuery.Value)),
                   FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucVendor.Value)),
                   FormatHelper.TODateInt(this.ucStartDate.Value),
                   FormatHelper.TODateInt(this.ucEndDate.Value),
                   FormatString(ucLabelComboxStorage.SelectedItemValue));

                return objs;
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
            }

            return null;
        }

        private object[] GetRow(object obj, bool rowChecked)
        {
            if (obj == null)
            {
                return new object[] { "", "", "", "", "", "", "", "", "", "", "", "" ,""};
            }

            return new object[] {
			   rowChecked,
                ((MaterialLotWithItemDesc)obj).MaterialLotNo,
			    ((MaterialLotWithItemDesc)obj).IQCNo,
			    ((MaterialLotWithItemDesc)obj).STLine,
                ((MaterialLotWithItemDesc)obj).VendorCode,
			    ((MaterialLotWithItemDesc)obj).VendorDesc,
			    ((MaterialLotWithItemDesc)obj).ItemCode,
			    ((MaterialLotWithItemDesc)obj).ItemDesc,
			    ((MaterialLotWithItemDesc)obj).CreateDate,
			    ((MaterialLotWithItemDesc)obj).LotInQty,
			    ((MaterialLotWithItemDesc)obj).LotQty,
			    ((MaterialLotWithItemDesc)obj).FIFOFlag,
                ((MaterialLotWithItemDesc)obj).StorageID
								};
        }

        #endregion
        #endregion


        #region 页面事件
        private void ucBtnQuery_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                _DataTable.Rows.Clear();
                return;
            }

            this.RequestData();
            this.chkAll.Checked = true;


        }
       
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                for (int i = 0; i < ultraGridMetrialDetial.Rows.Count; i++)
                {
                    ultraGridMetrialDetial.Rows[i].Cells[0].Value = chkAll.Checked;
                }

                ultraGridMetrialDetial.UpdateData();

                if (chkAll.Checked)
                {
                    this.ucSum.Value = ultraGridMetrialDetial.Rows.Count.ToString();
                }
                else
                {
                    this.ucSum.Value = "0";
                }

            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
            }
        }


        private void ucButtonPrint_Click(object sender, EventArgs e)
        {
            try
            { 
   
               string printNum = this.txtPrintNum.Value.Trim();

                if (printNum=="" || printNum==string.Empty)
                {
                    printNum = "0";
                }
                if (Convert.ToInt32(printNum)<=0)
                {
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_Error_PrintNumber_Must_Over_Zero"));
                    return ;
                }

                if (this.ucLabelComboxPrinter.ComboBoxData.Items.Count == 0)
                {
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_PleaseInstallPrinter"));
                    return;
                }

                SetPrintButtonStatus(false);

                CodeSoftPrintFacade codeSoftPrintFacade = new CodeSoftPrintFacade(this.DataProvider);

                string printer = string.Empty;
                string templatePath = string.Empty; ; //print path

                printer = this.ucLabelComboxPrinter.SelectedItemText;


                List<string> materialLot = new List<string>();

                for (int i = 0; i < ultraGridMetrialDetial.Rows.Count; i++)
                {
                    if (ultraGridMetrialDetial.Rows[i].Cells[0].Value.ToString().Trim().ToUpper() == "TRUE")
                    {
                        materialLot.Add( FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ultraGridMetrialDetial.Rows[i].Cells["MaterialLot"].Value.ToString())));

                    }
                }

                if (!CheckPrintCondition(printer, materialLot))
                {
                    return;
                }


                if (MessageBox.Show(UserControl.MutiLanguages.ParserMessage("$CS_YES_OR_NO_Print"), this.Text, MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    for (int j = 0; j < Convert.ToInt16(this.txtPrintNum.Value.Trim()); j++)
                    {

                        msg = codeSoftPrintFacade.PrintMaterialLot(printer,materialLot);
                    }

                    if (msg.IsSuccess())
                    {
                        this.ShowMessage(new UserControl.Message(MessageType.Success, "$Success_Print_Label"));
                    }
                }

                this.ShowMessage(msg);

            }
            catch (Exception ex)
            {
                this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, ex.Message));
                return;

            }
            finally
            {
                SetPrintButtonStatus(true);
            }

        }


        private void ultraGridMetrialDetial_ClickCellButton(object sender, CellEventArgs e)
        {
            if (e.Cell.Column.Key.ToUpper() == "FifoChcek".ToUpper())
            {

                string fifoResult = "Y";

                if (e.Cell.Row.Cells["FifoChcek"].Value.ToString() == "Y")
                {


                    if (MessageBox.Show(UserControl.MutiLanguages.ParserMessage("$CS_YES_NO_CANCEL_FIFOCHECK"), this.Text, MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        fifoResult = "N";
                    }
                }
                else
                {
                    if (MessageBox.Show(UserControl.MutiLanguages.ParserMessage("$CS_YES_NO_NEED_FIFOCHECK"), this.Text, MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        fifoResult = "N";
                    }

                } 
                
                if (fifoResult != e.Cell.Row.Cells["FifoChcek"].Value.ToString())
                {
                    try
                    {
                        e.Cell.Row.Cells["FifoChcek"].Value = fifoResult;
                        ultraGridMetrialDetial.UpdateData();


                        InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
                        MaterialLot materialLot = (MaterialLot)inventoryFacade.GetMaterialLot(e.Cell.Row.Cells["MaterialLot"].Value.ToString().Trim().ToUpper());

                        string userCode = ApplicationService.Current().UserCode;
                        DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                        this.DataProvider.BeginTransaction();

                        materialLot.FIFOFlag = fifoResult;
                        materialLot.MaintainUser = userCode;
                        materialLot.MaintainDate = dbDateTime.DBDate;
                        materialLot.MaintainTime = dbDateTime.DBTime;

                        inventoryFacade.UpdateMaterialLot(materialLot);

                        this.DataProvider.CommitTransaction();
                        this.ShowMessage(new UserControl.Message(MessageType.Success, "$CS_Save_Success"));
                                            
                    }
                    catch (System.Exception ex)
                    {
                        this.DataProvider.RollbackTransaction();
                        this.ShowMessage(new UserControl.Message(UserControl.MessageType.Error, ex.Message));
                        return;
                    }
                }
            }

        }

        private void ultraGridMetrialDetial_CellChange(object sender, CellEventArgs e)
        {
            if (e.Cell.Column.Index == 0)
            {
                if (!bool.Parse(e.Cell.Value.ToString()))
                {
                    AddCount(this.ucSum, 1);
                }
                else
                {
                    AddCount(this.ucSum, -1);
                }
                ultraGridMetrialDetial.UpdateData();
            }

        }
      
        #endregion     


        #region 自定义方法

        private string FormatString(object value)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                return FormatHelper.CleanString(value.ToString().Trim());
            }
        }

        protected bool ValidateInput()
        {
            bool validate = true;

            string ItemCode = FormatHelper.CleanString(this.ucLEItemCodeQuery.Value);
            string Lot = FormatHelper.CleanString(this.ucBatch.Value);
            string IqcNo = FormatHelper.CleanString(this.ucIQCNO.Value);
            string Vendor = FormatHelper.CleanString(this.ucVendor.Value.Trim());
            string storage = FormatString(ucLabelComboxStorage.SelectedItemValue);

            if (ItemCode == string.Empty &&
                Lot == string.Empty &&
                IqcNo == string.Empty &&
                Vendor == string.Empty &&
                storage==string.Empty)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$At_Least_One_Conditon_Not_NULL"));

                if (Lot == string.Empty)
                    ucBatch.TextFocus(false, true);
                else if (IqcNo == string.Empty)
                    ucIQCNO.TextFocus(false, true);
                else if (ItemCode == string.Empty)
                    ucLEItemCodeQuery.TextFocus(false, true);
                else if (Vendor == string.Empty)
                    ucVendor.TextFocus(false, true);

                validate = false;
            }

            return validate;
        }

        private void SetPrintButtonStatus(bool enabled)
        {
            this.ucButtonPrint.Enabled = enabled;

            if (enabled)
            {
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
            }
            else
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            }
        }

        private void AddCount(UCLabelEdit ucLabelEdit, int add)
        {
            int count = int.Parse(ucLabelEdit.Value);
            count += add;
            ucLabelEdit.Value = count.ToString();
        }


        private void LoadPrinter()
        {
            this.ucLabelComboxPrinter.Clear();

            if (System.Drawing.Printing.PrinterSettings.InstalledPrinters == null ||
                System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count == 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_PleaseInstallPrinter"));
                return;
            }

            int defaultprinter = 0;
            for (int i = 0; i < System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count; i++)
            {
                this.ucLabelComboxPrinter.AddItem(System.Drawing.Printing.PrinterSettings.InstalledPrinters[i], System.Drawing.Printing.PrinterSettings.InstalledPrinters[i]);
                System.Drawing.Printing.PrinterSettings pts = new System.Drawing.Printing.PrinterSettings();
                pts.PrinterName = System.Drawing.Printing.PrinterSettings.InstalledPrinters[i];
                if (pts.IsDefaultPrinter)
                {
                    defaultprinter = i;
                }
            }
            this.ucLabelComboxPrinter.SelectedIndex = defaultprinter;
        }

        private bool CheckPrintCondition(string printer, List<string> materialLot)
        {
           

            if (materialLot == null || materialLot.Count <= 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Message_NoMaterialLotToPrint"));
                return false;
            }
            //模板
            //if (templatePath == null || templatePath.Length <= 0 || templatePath=="")
            //{
            //    this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_PrintTemplate_Empty"));
            //    return false;
            //}

            //打印机
            if (printer == null || printer.Trim().Length <= 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_Printer_Empty"));
                return false;
            }
            return true;
        }

        #endregion

  

     




    }
}