using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using UserControl;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;

namespace BenQGuru.eMES.Client
{
    public partial class FMaterialFullCheckMcode : BaseForm
    {
        #region 变量     

        private DataSet m_SampleList = null;
        private DataTable m_CheckTable = null;
        private DataTable m_CheckTableDetail = null;        
        private InventoryFacade m_INVFacade = null;
        private ItemFacade m_ItemNoFacade = null;      
        
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        #region InventoryFacade

        public InventoryFacade INVFacade
        {
            get
            {
                if (m_INVFacade == null)
                {
                    m_INVFacade = new InventoryFacade(this.DataProvider);
                }
                return m_INVFacade;
            }
        }

        #endregion

        #region ItemFacade

        public ItemFacade ItemNoFacade
        {
            get
            {
                if (m_ItemNoFacade == null)
                {
                    m_ItemNoFacade = new ItemFacade(this.DataProvider);
                }
                return m_ItemNoFacade;
            }
        }

        #endregion
       
        #endregion 
        public FMaterialFullCheckMcode()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);

            this.ultraGridHead.DisplayLayout.Appearance.BackColor = System.Drawing.Color.Gainsboro; ;
            this.ultraGridHead.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridHead.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridHead.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridHead.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            this.ultraGridHead.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridHead.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridHead.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            this.ultraGridHead.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            this.ultraGridHead.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void FMaterialFullCheckMcode_Load(object sender, EventArgs e)
        {
            InitializeSampleListGrid();
            //this.InitGridLanguage(ultraGridHead);
            //this.InitPageLanguage();
            //BindPlanSerialCode();
        }

        private void ultraGridHead_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            e.Layout.Override.AllowColSizing = AllowColSizing.Free;
            e.Layout.ScrollBounds = ScrollBounds.ScrollToFill;

            //e.Layout.MaxBandDepth = 1;
            // 自动判断是否显示前面的+、-号
            e.Layout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            // 冻结列
            e.Layout.UseFixedHeaders = true;
            e.Layout.Override.FixedHeaderAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedHeaderAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedCellAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedCellAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedHeaderIndicator = FixedHeaderIndicator.None;

            // 排序
            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            // 不允许删除
            e.Layout.Override.AllowDelete = DefaultableBoolean.False;

            // 滚动提示
            e.Layout.Bands[0].ScrollTipField = "CheckTable";
            e.Layout.Bands[1].ScrollTipField = "CheckTableDetail";

            //设置列宽和列名称                        
            e.Layout.Bands[0].Columns["MCODE"].Header.Caption = "物料代码";
            e.Layout.Bands[0].Columns["MDESC"].Header.Caption = "物料描述";
            e.Layout.Bands[0].Columns["SUMQTY"].Header.Caption = "总需求量";
            e.Layout.Bands[0].Columns["QTY"].Header.Caption = "库存总量";
            e.Layout.Bands[0].Columns["SHORTQTY"].Header.Caption = "总缺料数量";

            e.Layout.Bands[0].Columns["MCODE"].Width = 200;
            e.Layout.Bands[0].Columns["MDESC"].Width = 260;
            e.Layout.Bands[0].Columns["SUMQTY"].Width = 100;
            e.Layout.Bands[0].Columns["QTY"].Width = 100;
            e.Layout.Bands[0].Columns["SHORTQTY"].Width = 100;
            // 设置栏位是否允许编辑，及栏位的显示形式

            e.Layout.Bands[0].Columns["MCODE"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["MDesc"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["SUMQTY"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["QTY"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["SHORTQTY"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            // 允许筛选
            e.Layout.Bands[0].Columns["MCODE"].AllowRowFiltering = DefaultableBoolean.True;
            // 允许冻结，且Checked栏位始终处于冻结状态，不可更改          
            //e.Layout.Bands[0].Columns["Checked"].Header.Fixed = true;
            //e.Layout.Bands[0].Columns["TransferLine"].Header.FixedHeaderIndicator = FixedHeaderIndicator.Button;

            e.Layout.Bands[0].Columns["MCODE"].SortIndicator = SortIndicator.Ascending;

            // CheckTableDetail      
            e.Layout.Bands[1].Columns["MCODE"].Hidden = true;
            e.Layout.Bands[1].Columns["PLANTYPE"].Header.Caption = "类型";
            e.Layout.Bands[1].Columns["PLANTYPENAME"].Header.Caption = "类型";
            e.Layout.Bands[1].Columns["MOCODE"].Header.Caption = "工单";
            e.Layout.Bands[1].Columns["ITEMCODE"].Header.Caption = "产品代码";
            e.Layout.Bands[1].Columns["ITEMDESC"].Header.Caption = "产品描述";
            e.Layout.Bands[1].Columns["PLANDATE"].Header.Caption = "计划生产日期";
            e.Layout.Bands[1].Columns["PLANQTY"].Header.Caption = "产量";
            e.Layout.Bands[1].Columns["LOSTQTY"].Header.Caption = "物料用量";
            e.Layout.Bands[1].Columns["SHORTQTY"].Header.Caption = "累计剩余物料量";
            e.Layout.Bands[1].Columns["PLANTYPE"].Hidden = true;

            e.Layout.Bands[1].Columns["MCODE"].Width = 100;
            e.Layout.Bands[1].Columns["PLANTYPE"].Width = 100;
            e.Layout.Bands[1].Columns["PLANTYPENAME"].Width = 100;
            e.Layout.Bands[1].Columns["MOCODE"].Width = 150;
            e.Layout.Bands[1].Columns["ITEMCODE"].Width = 150;
            e.Layout.Bands[1].Columns["ITEMDESC"].Width = 150;
            e.Layout.Bands[1].Columns["PLANDATE"].Width = 100;
            e.Layout.Bands[1].Columns["PLANQTY"].Width = 100;
            e.Layout.Bands[1].Columns["LOSTQTY"].Width = 100;
            e.Layout.Bands[1].Columns["SHORTQTY"].Width = 100;
            
            
            e.Layout.Bands[1].Columns["MCODE"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[1].Columns["PLANTYPE"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[1].Columns["PLANTYPENAME"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[1].Columns["MOCODE"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[1].Columns["ITEMCODE"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[1].Columns["ITEMDESC"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[1].Columns["PLANDATE"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[1].Columns["PLANQTY"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[1].Columns["LOSTQTY"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[1].Columns["SHORTQTY"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;


            e.Layout.Bands[1].Columns["PLANTYPE"].Header.Fixed = true;
            e.Layout.Bands[1].Columns["PLANTYPE"].Header.FixedHeaderIndicator = FixedHeaderIndicator.Button;
            e.Layout.Bands[1].Columns["PLANTYPE"].AllowRowFiltering = DefaultableBoolean.True;

            e.Layout.Bands[1].Columns["PLANTYPE"].SortIndicator = SortIndicator.Ascending;
        }

        private void btnGetItemCode_Click(object sender, EventArgs e)
        {
            FMCodeQuery fMCodeQuery = new FMCodeQuery();
            fMCodeQuery.Owner = this;
            fMCodeQuery.StartPosition = FormStartPosition.CenterScreen;
            fMCodeQuery.MCodeSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(MCodeSelector_MCodeSelectedEvent);
            fMCodeQuery.ShowDialog();
            fMCodeQuery = null;
        }

        private void MCodeSelector_MCodeSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.ucLabelEditMaterialCode.Value = e.CustomObject;
        }

        private void btnGetStorageCode_Click(object sender, EventArgs e)
        {
            FStorageCodeMulQuery objForm = new FStorageCodeMulQuery();
            objForm.Owner = this;
            objForm.StartPosition = FormStartPosition.CenterScreen;
            objForm.StorageCodeSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(StorageCodeSelector_StorageCodeSelectedEvent);
            objForm.ShowDialog();
        }

        private void StorageCodeSelector_StorageCodeSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.ucLabelEditStorageCode.Value = e.CustomObject;
        }



        private void btnGetModelCode_Click(object sender, EventArgs e)
        {
            FModelQuery objForm = new FModelQuery();
            objForm.Owner = this;
            objForm.StartPosition = FormStartPosition.CenterScreen;
            objForm.ModelCodeSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(ModelCodeSelector_ModelCodeSelectedEvent);
            objForm.ShowDialog();
        }

        private void ModelCodeSelector_ModelCodeSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.ucLabelEditModelCode.Value = e.CustomObject;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (ucLabelEditMaterialCode.Value == string.Empty)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Please_Input_MaterialCode"));
                this.ucLabelEditMaterialCode.TextFocus(false, true);
                return;
            }
            if (ucLabelEditStorageCode.Value == string.Empty)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$ERROR_Storage_Must_Selected"));
                this.ucLabelEditStorageCode.TextFocus(false, true);
                return;
            }
            //if (ucLabelComboxPlanSerial.SelectedItemValue == null)
            //{
            //    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Please_Select_PlanSerial"));                
            //    return;
            //}
            string mCode = this.ucLabelEditMaterialCode.Value.ToUpper().Trim();
            string storageCode = this.ucLabelEditStorageCode.Value.ToUpper().Trim();
            //int planSerial = -1;
            //if (ucLabelComboxPlanSerial.SelectedItemText != string.Empty)
            //{

            //    planSerial = int.Parse(this.ucLabelComboxPlanSerial.SelectedItemValue.ToString().ToUpper().Trim());
            //}
            string modelCode = this.ucLabelEditModelCode.Value.ToUpper().Trim();
            ClearSampleList();
            try
            {
                object[] obj = INVFacade.QueryMaterialFullMcode(mCode, storageCode, -1,modelCode);
                if (obj == null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Not_Find_Metrial"));                    
                    return;
                }
                foreach (MaterialFull materialFull in obj)
                {
                    DataRow[] dataRowQty = m_CheckTable.Select(string.Format("MCODE='{0}'", materialFull.MCode));                   
                    if (dataRowQty.Length == 0)
                    {
                        DataRow rowCheckTable;
                        rowCheckTable = this.m_SampleList.Tables["CheckTable"].NewRow();
                       
                        rowCheckTable["MCODE"] = materialFull.MCode;
                        rowCheckTable["MDESC"] = materialFull.Mdesc;
                        rowCheckTable["SUMQTY"] = materialFull.SumQty;
                        rowCheckTable["QTY"] = materialFull.Qty;
                        if (materialFull.ShortQty >= 0)
                        {
                            rowCheckTable["SHORTQTY"] = 0;
                        }
                        else
                        {
                            rowCheckTable["SHORTQTY"] = materialFull.ShortQty;
                        }
                        this.m_SampleList.Tables["CheckTable"].Rows.Add(rowCheckTable);
                        if (rowCheckTable != null)
                            this.m_SampleList.Tables["CheckTable"].AcceptChanges();
                    }
                    else
                    {
                        DataRow rowCheckTable = dataRowQty[0];
                        rowCheckTable["SUMQTY"] = materialFull.SumQty;
                        rowCheckTable["QTY"] = materialFull.Qty;
                        if (materialFull.ShortQty >= 0)
                        {
                            rowCheckTable["SHORTQTY"] = 0;
                        }
                        else
                        {
                            rowCheckTable["SHORTQTY"] = materialFull.ShortQty;
                        }
                        if (rowCheckTable != null)
                            this.m_SampleList.Tables["CheckTable"].AcceptChanges();
                    }
                    DataRow rowCheckTableDetail;
                    rowCheckTableDetail = this.m_SampleList.Tables["CheckTableDetail"].NewRow();
                    rowCheckTableDetail["MCODE"] = materialFull.MCode;
                    rowCheckTableDetail["PLANTYPE"] = materialFull.PlanType;                    
                    rowCheckTableDetail["MOCODE"] = materialFull.MoCode;
                    rowCheckTableDetail["ITEMCODE"] = materialFull.Itemcode;
                    rowCheckTableDetail["ITEMDESC"] = materialFull.Modesc;
                    rowCheckTableDetail["PLANDATE"] = materialFull.Plandate;
                    rowCheckTableDetail["PLANQTY"] = materialFull.Planqty;
                    rowCheckTableDetail["LOSTQTY"] = materialFull.LostQty;
                    rowCheckTableDetail["SHORTQTY"] = materialFull.ShortQty;
                    if (materialFull.PlanType == "MO")
                    {
                        rowCheckTableDetail["PLANTYPENAME"] = "工单"; 
                    }
                    if (materialFull.PlanType == "PLAN")
                    {
                        rowCheckTableDetail["PLANTYPENAME"] = "计划"; 
                    }
                    this.m_SampleList.Tables["CheckTableDetail"].Rows.Add(rowCheckTableDetail);

                    if (rowCheckTableDetail != null)
                        this.m_SampleList.Tables["CheckTableDetail"].AcceptChanges();

                }
                this.m_SampleList.AcceptChanges();
                this.ultraGridHead.DataSource = this.m_SampleList;
            }
            catch (Exception ex)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, ex.Message));                
                return;
            }
        }

        private void ultraGridHead_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            if (e.Row.HasParent() && (decimal.Parse(e.Row.Cells["SHORTQTY"].Value.ToString()) < 0))
            {
                e.Row.Appearance.BackColor = Color.Red;
            }            
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog save = new SaveFileDialog();
            save.CheckPathExists = true;
            //save.Filter = "数据文件(*.xls)|*.xls";
            save.Filter=MutiLanguages.ParserString("$CS_ExportFilter");
            save.DefaultExt = "xls";
            save.Title = MutiLanguages.ParserString("$SaveTitle");
            //save.Title = "保存当前数据";
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ultraGridExcelExporter1.Export(ultraGridHead, save.FileName);
            }
        }

        #region method

        private void InitializeSampleListGrid()
        {
            this.m_SampleList = new DataSet();
            this.m_CheckTable = new DataTable("CheckTable");
            this.m_CheckTableDetail = new DataTable("CheckTableDetail");  

            this.m_CheckTable.Columns.Add("MCODE", typeof(string));
            this.m_CheckTable.Columns.Add("MDESC", typeof(string));
            this.m_CheckTable.Columns.Add("SUMQTY", typeof(decimal));
            this.m_CheckTable.Columns.Add("QTY", typeof(decimal));
            this.m_CheckTable.Columns.Add("SHORTQTY", typeof(decimal));

            this.m_CheckTableDetail.Columns.Add("MCODE", typeof(string));
            this.m_CheckTableDetail.Columns.Add("PLANTYPE", typeof(string));
            this.m_CheckTableDetail.Columns.Add("PLANTYPENAME", typeof(string));
            this.m_CheckTableDetail.Columns.Add("MOCODE", typeof(string));
            this.m_CheckTableDetail.Columns.Add("ITEMCODE", typeof(string));
            this.m_CheckTableDetail.Columns.Add("ITEMDESC", typeof(string));
            this.m_CheckTableDetail.Columns.Add("PLANDATE", typeof(int));
            this.m_CheckTableDetail.Columns.Add("PLANQTY", typeof(int));
            this.m_CheckTableDetail.Columns.Add("LOSTQTY", typeof(decimal));
            this.m_CheckTableDetail.Columns.Add("SHORTQTY", typeof(decimal));       

            this.m_SampleList.Tables.Add(this.m_CheckTable);
            this.m_SampleList.Tables.Add(this.m_CheckTableDetail);
            DataColumn[] parentCols = new DataColumn[] { this.m_SampleList.Tables["CheckTable"].Columns["MCODE"] };
            DataColumn[] childCols = new DataColumn[] { this.m_SampleList.Tables["CheckTableDetail"].Columns["MCODE"] };

            this.m_SampleList.Relations.Add(new DataRelation("SampleGroupAll", parentCols, childCols));

            this.m_SampleList.AcceptChanges();
            this.ultraGridHead.DataSource = this.m_SampleList;
        }

        private void ClearSampleList()
        {
            if (this.m_SampleList == null)
            {
                return;
            }

            this.m_SampleList.Tables["CheckTableDetail"].Rows.Clear();
            this.m_SampleList.Tables["CheckTable"].Rows.Clear();

            this.m_SampleList.Tables["CheckTableDetail"].AcceptChanges();
            this.m_SampleList.Tables["CheckTable"].AcceptChanges();
            this.m_SampleList.AcceptChanges();
        }

        ////初始化临时生产计划序号下拉框
        //private void BindPlanSerialCode()
        //{
        //    this.ucLabelComboxPlanSerial.ComboBoxData.Items.Clear();
        //    this.ucLabelComboxPlanSerial.AddItem("", "");
        //    object[] objs = this.INVFacade.QueryTempPlanQuerySeq();
        //    if (objs != null)
        //    {
        //        foreach (object obj in objs)
        //        {
        //            TempPlan tempPlan = (TempPlan)obj;
        //            if (tempPlan != null)
        //            {
        //                this.ucLabelComboxPlanSerial.AddItem(tempPlan.Queryseq.ToString(), tempPlan.Queryseq);
        //            }
        //        }
        //    }
        //}
        
        #endregion     

       

       


    }
}
