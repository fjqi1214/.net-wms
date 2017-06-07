using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using Infragistics.Win.UltraWinGrid;
using System.Collections;
using UserControl;

namespace BenQGuru.eMES.Client
{
    public partial class FStackInfo : BaseForm
    {
        private DataTable m_dtStorage;
        private DataSet m_dsStack;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;

        public event ParentChildRelateEventHandler<ParentChildRelateEventArgs<Hashtable>> StackInfoEvent;

        public void OnStackInfoEvent(object sender, ParentChildRelateEventArgs<Hashtable> e)
        {
            if (this.StackInfoEvent != null)
            {
                StackInfoEvent(sender, e);
            }
        }

        private string m_StorageCode;

        public string StorageCode
        {
            get { return m_StorageCode; }
            set { m_StorageCode = value; }
        }

        private string m_StackCode;

        public string StackCode
        {
            get { return m_StackCode; }
            set { m_StackCode = value; }
        }


        private IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public FStackInfo()
        {
            InitializeComponent();
        }

        //private string m_StorageCode;

        //public string StorageCode
        //{
        //    get { return m_StorageCode; }
        //    set { m_StorageCode = value; }
        //}

        //private string  m_StackCode;

        //public string  StackCode
        //{
        //    get { return m_StackCode; }
        //    set { m_StackCode = value; }
        //}



        private void gridStorage_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            e.Layout.Override.AllowColSizing = AllowColSizing.None;

            e.Layout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            // 排序
            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            e.Layout.Bands[0].Columns["StoreroomCode"].Width = 100;
            e.Layout.Bands[0].Columns["StorageName"].Width = 140;

            // 栏位名称
            e.Layout.Bands[0].Columns["StoreroomCode"].Header.Caption = "库房代码";
            e.Layout.Bands[0].Columns["StorageName"].Header.Caption = "库房描述";

            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["StoreroomCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["StorageName"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            this.InitGridLanguage(gridStorage);
        }

        private void gridStack_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            e.Layout.Override.AllowColSizing = AllowColSizing.Free;
            e.Layout.Override.CellClickAction = CellClickAction.Edit;
            //e.Layout.Override.CellClickAction = CellClickAction.CellSelect;

            e.Layout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;

            // 自动判断是否显示前面的+、-号
            e.Layout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            // 排序
            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            if (this.m_dsStack.Tables["Head"] != null)
            {
                e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
                e.Layout.Bands[0].Columns["Checked"].Width = 30;
                e.Layout.Bands[0].Columns["Checked"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;


                e.Layout.Bands[0].Columns["stackcode"].Header.Caption = "垛位";
                e.Layout.Bands[0].Columns["stackcode"].Width = 100;
                e.Layout.Bands[0].Columns["stackcode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

                e.Layout.Bands[0].Columns["maxcapacity"].Header.Caption = "最大容量";
                e.Layout.Bands[0].Columns["maxcapacity"].Width = 60;
                e.Layout.Bands[0].Columns["maxcapacity"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                e.Layout.Bands[0].Columns["maxcapacity"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

                e.Layout.Bands[0].Columns["palletcount"].Header.Caption = "当前栈板数";
                e.Layout.Bands[0].Columns["palletcount"].Width = 75;
                e.Layout.Bands[0].Columns["palletcount"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                e.Layout.Bands[0].Columns["palletcount"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

                e.Layout.Bands[0].Columns["remaincount"].Header.Caption = "剩余";
                e.Layout.Bands[0].Columns["remaincount"].Width = 50;
                e.Layout.Bands[0].Columns["remaincount"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                e.Layout.Bands[0].Columns["remaincount"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

                e.Layout.Bands[0].Columns["percent"].Header.Caption = "百分比";
                e.Layout.Bands[0].Columns["percent"].Width = 190;
                e.Layout.Bands[0].Columns["percent"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                e.Layout.Bands[0].Columns["percent"].Format = " ";
            }
            if (this.m_dsStack.Tables["Detail"] != null && this.m_dsStack.Tables["Head"] != null)
            {
                e.Layout.Bands[1].Columns["stackcode"].Hidden = true;

                e.Layout.Bands[1].Columns["itemcode"].Header.Caption = "产品代码";
                e.Layout.Bands[1].Columns["itemcode"].Width = 100;
                e.Layout.Bands[1].Columns["itemcode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

                e.Layout.Bands[1].Columns["itemdesc"].Header.Caption = "产品描述";
                e.Layout.Bands[1].Columns["itemdesc"].Width = 180;
                e.Layout.Bands[1].Columns["itemdesc"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

                e.Layout.Bands[1].Columns["palletcode"].Header.Caption = "栈板号";
                e.Layout.Bands[1].Columns["palletcode"].Width = 130;
                e.Layout.Bands[1].Columns["palletcode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

                e.Layout.Bands[1].Columns["qtyinfo"].Header.Caption = "数量";
                e.Layout.Bands[1].Columns["qtyinfo"].Width = 75;
                e.Layout.Bands[1].Columns["qtyinfo"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                e.Layout.Bands[1].Columns["qtyinfo"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            }
            this.InitGridLanguage(gridStack);
        }

        private void FStackInfo_Load(object sender, EventArgs e)
        {
            UserControl.UIStyleBuilder.FormUI(this);
            UserControl.UIStyleBuilder.GridUI(this.gridStack);
            UserControl.UIStyleBuilder.GridUI(this.gridStorage);

            this.InitialDataTable();

            this.FillStorageGrid();

            this.InitialSelectedRecord();



            this.gridStack.DrawFilter = new GridProgress();
            //this.InitPageLanguage();
        }

        private void InitialSelectedRecord()
        {
            if (this.m_StorageCode.Length != 0)
            {
                //设置选择的库位
                for (int i = 0; i < this.gridStorage.Rows.Count; i++)
                {
                    if (this.gridStorage.Rows[i].Cells["StoreroomCode"].Value.ToString().Equals(this.m_StorageCode))
                    {
                        this.gridStorage.ActiveRow = this.gridStorage.Rows[i];
                        this.gridStorage.Rows[i].Selected = true;

                        FillStackGrid(this.m_StorageCode);
                        break;
                    }
                }

                //设置选择的Stack
                if (this.m_StackCode.Length != 0)
                {
                    for (int i = 0; i < this.gridStack.Rows.Count; i++)
                    {
                        if (this.gridStack.Rows[i].Cells["stackcode"].Value.ToString().Equals(this.m_StackCode))
                        {
                            this.gridStack.Rows[i].Cells["Checked"].Value = "True";
                            break;
                        }
                    }
                }
            }
        }

        private void FillStackGrid(string storageCode)
        {
            //if (m_dsStack == null)
            //{
            //    m_dsStack = new DataSet();
            //}
            DataSet ds = new DataSet();

            InventoryFacade objFacade = new InventoryFacade(this.DataProvider);
            DataTable dtHeadTmp = objFacade.GetStackPalletInfoHead(storageCode);
            dtHeadTmp.TableName = "Head";



            DataTable dtDetail = objFacade.GetStackPalleteInfoDetail(storageCode).Copy();

            if (dtDetail.Rows.Count == 0)
            {
                dtDetail.Columns.Add("itemcode");
                dtDetail.Columns.Add("itemdesc");

                dtDetail.Columns.Add("palletcode");
                dtDetail.Columns.Add("qtyinfo");
            }
            dtDetail.TableName = "Detail";

            DataTable dtHead = new DataTable();

            dtHead.Columns.Add("Checked");
            dtHead.Columns.Add("stackcode");

            dtHead.Columns.Add("maxcapacity");
            dtHead.Columns.Add("palletcount");

            dtHead.Columns.Add("remaincount");
            dtHead.Columns.Add("percent");

            if (dtHeadTmp.Rows.Count != 0)
            {
                dtHead = dtHeadTmp.Clone();
                dtHead.Columns["percent"].DataType = typeof(Int32);

                foreach (DataRow row in dtHeadTmp.Rows)
                {
                    dtHead.Rows.Add(new object[] { row[0], row[1], row[2], row[3], row[4], Convert.ToInt32(row[5]) });
                }
                dtHead.AcceptChanges();

                ds.Tables.Add(dtHead);
            }

            if (dtDetail.Rows.Count != 0)
            {
                ds.Tables.Add(dtDetail);
            }
            if (dtHead.Rows.Count != 0 && dtDetail.Rows.Count != 0)
            {
                //ds.Relations.c
                ds.Relations.Add("StackHeadDtail", ds.Tables["Head"].Columns["stackcode"], ds.Tables["Detail"].Columns["stackcode"], false);
            }
            //
            if (dtHead.Rows.Count != 0)
            {
                m_dsStack = ds.Copy();
                this.gridStack.DataSource = m_dsStack.Tables["Head"];
            }
            else
            {
                if (m_dsStack == null)
                {
                    m_dsStack = new DataSet();
                }
                m_dsStack.Clear();
                //if (m)
                //{

                //}
                //DataSet dsClear = m_dsStack.Copy();
                //this.gridStack.DataSource = dsClear;
                //m_dsStack = null;

            }

        }


        private void FillStorageGrid()
        {
            InventoryFacade objFacade = new InventoryFacade(this.DataProvider);
            object[] storageList = objFacade.GetAllStorage();

            if (storageList != null)
            {
                DataRow dr;
                foreach (Storage storage in storageList)
                {
                    dr = this.m_dtStorage.NewRow();
                    dr["StoreroomCode"] = storage.StorageCode;
                    dr["StorageName"] = storage.StorageName;

                    this.m_dtStorage.Rows.Add(dr);
                }
            }

            this.gridStorage.DataSource = m_dtStorage;


        }

        private void InitialDataTable()
        {
            this.m_dtStorage = new DataTable();
            this.m_dtStorage.Columns.Add("StoreroomCode");
            this.m_dtStorage.Columns.Add("StorageName");
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            this.gridStack.UpdateData();
            //this.gridStorage.UpdateData();

            if (this.gridStorage.ActiveRow != null)
            {
                //this.m_StorageCode = this.gridStorage.ActiveRow.Cells["StoreroomCode"].Text.Trim();
                Int32 intCheckNum = 0;
                string strStackCode = string.Empty;
                for (int i = 0; i < this.gridStack.Rows.Count; i++)
                {
                    if (this.gridStack.Rows[i].Cells["Checked"].Value.ToString().ToLower() == "true")
                    {
                        strStackCode = this.gridStack.Rows[i].Cells["stackcode"].Value.ToString();
                        intCheckNum = intCheckNum + 1;
                    }
                }

                if (intCheckNum != 1)
                {
                    //请选中一笔垛位
                    ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_SELECT_ONLY_ONE_STACK"));
                }
                else
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("storagecode", this.gridStorage.ActiveRow.Cells["StoreroomCode"].Value.ToString());
                    ht.Add("stackcode", strStackCode);
                    ParentChildRelateEventArgs<Hashtable> args = new ParentChildRelateEventArgs<Hashtable>(ht);
                    this.OnStackInfoEvent(sender, args);
                    this.Close();
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridStorage_Click(object sender, EventArgs e)
        {
            if (this.gridStorage.ActiveRow != null)
            {
                this.txtStackCode.Value = string.Empty;
                FillStackGrid(this.gridStorage.ActiveRow.Cells["StoreroomCode"].Text.Trim());
            }
        }

        private void txtStackCode_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                int inputLength = this.txtStackCode.Value.Trim().Length;

                if (inputLength == 0)
                {
                    this.txtStackCode.TextFocus(true, true);
                    return;
                }

                if (this.gridStack.ActiveRow != null)
                {
                    //所有checkbox为false
                    for (int i = 0; i < gridStack.Rows.Count; i++)
                    {
                        gridStack.Rows[i].Cells["Checked"].Value = false;
                    }
                    //左匹配所有符合的行
                    for (int i = 0; i < gridStack.Rows.Count; i++)
                    {
                        if (gridStack.Rows[i].Cells["stackcode"].Value.ToString().Length >= inputLength
                            && gridStack.Rows[i].Cells["stackcode"].Value.ToString().ToUpper().Substring(0, inputLength) == this.txtStackCode.Value.Trim().ToUpper())
                        {
                            if (gridStack.Rows[i].Cells["Checked"].Value.ToString().ToLower() == "false")
                            {
                                gridStack.Rows[i].Cells["Checked"].Value = true;
                                break;
                            }
                        }
                    }

                    string strStackCode = string.Empty;
                    //回车时只带回第一笔
                    for (int i = 0; i < this.gridStack.Rows.Count; i++)
                    {
                        if (this.gridStack.Rows[i].Cells["Checked"].Value.ToString().ToLower() == "true")
                        {
                            strStackCode = this.gridStack.Rows[i].Cells["stackcode"].Value.ToString();
                            break;
                        }
                    }

                    if (strStackCode.Trim() != string.Empty && strStackCode.Trim().Length > 0)
                    {
                        Hashtable ht = new Hashtable();
                        ht.Add("storagecode", this.gridStorage.ActiveRow.Cells["StoreroomCode"].Value.ToString());
                        ht.Add("stackcode", strStackCode);
                        ParentChildRelateEventArgs<Hashtable> args = new ParentChildRelateEventArgs<Hashtable>(ht);
                        this.OnStackInfoEvent(sender, args);
                        this.Close();
                    }
                }
            }
        }

        private void gridStack_CellChange(object sender, CellEventArgs e)
        {
            if (this.gridStack.ActiveRow != null)
            {
                for (int i = 0; i < gridStack.Rows.Count; i++)
                {
                    if (gridStack.Rows[i].Activated == false)
                    {
                        gridStack.Rows[i].Cells["Checked"].Value = false;
                    }
                }
            }
        }

        private void txtStackCode_InnerTextChanged(object sender, EventArgs e)
        {
            m_dsStack.Tables["Head"].DefaultView.RowFilter = "stackcode like '" + Common.Helper.CommonHelper.Convert(this.txtStackCode.Value.Trim().ToUpper()) + "%'";
        }

        private void FStackInfo_Activated(object sender, EventArgs e)
        {
            this.txtStackCode.TextFocus(true, true);
        }
    }
}