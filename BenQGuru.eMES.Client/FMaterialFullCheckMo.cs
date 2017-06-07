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
    public partial class FMaterialFullCheckMo : BaseForm
    {

        #region 变量

        private DataSet m_SampleList = null;
        private DataTable m_CheckTable = null;
        private DataTable m_CheckTableDetail = null;       
        private InventoryFacade m_INVFacade = null;
        private IDomainDataProvider m_DomainDataProvider = ApplicationService.Current().DataProvider;

        #endregion 

        #region 界面创建及加载
        public FMaterialFullCheckMo()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);

            InitUltraGrid();//设定Grid属性
        }

        private void FMaterialFullCheckMo_Load(object sender, EventArgs e)
        {
            InitializeSampleListGrid();

            //应用多语言
            //this.InitGridLanguage(ultraGridHead);
            //this.InitPageLanguage();

            //实例化页面共用的Facade
            m_INVFacade = new InventoryFacade(this.m_DomainDataProvider);
        }
        #endregion

        #region Grid样式设定
        /// <summary>
        /// 创建Grid时设定Grid属性
        /// </summary>
        private void InitUltraGrid() 
        {
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

        /// <summary>
        /// 初始化Grid样式
        /// </summary>
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
            e.Layout.Bands[0].Columns["MOCODE"].Header.Caption = "工单";
            e.Layout.Bands[0].Columns["ITEMCODE"].Header.Caption = "产品代码";
            e.Layout.Bands[0].Columns["ITEMDESC"].Header.Caption = "产品描述";
            e.Layout.Bands[0].Columns["PLANTYPE"].Header.Caption = "类型";
            e.Layout.Bands[0].Columns["PLANTYPENAME"].Header.Caption = "类型";           
            e.Layout.Bands[0].Columns["PLANQTY"].Header.Caption = "产量";
            e.Layout.Bands[0].Columns["PLANDATE"].Header.Caption = "计划生产日期";
            e.Layout.Bands[0].Columns["PLANTYPE"].Hidden = true;

            e.Layout.Bands[0].Columns["MOCODE"].Width = 200;
            e.Layout.Bands[0].Columns["ITEMCODE"].Width = 200;
            e.Layout.Bands[0].Columns["ITEMDESC"].Width = 260;
            e.Layout.Bands[0].Columns["PLANTYPE"].Width = 100;
            e.Layout.Bands[0].Columns["PLANTYPENAME"].Width = 100;
            e.Layout.Bands[0].Columns["PLANQTY"].Width = 100;
            e.Layout.Bands[0].Columns["PLANDATE"].Width = 100;

            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["MOCODE"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["ITEMCODE"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["ITEMDESC"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["PLANTYPE"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["PLANTYPENAME"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["PLANQTY"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["PLANDATE"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            // 允许筛选
            e.Layout.Bands[0].Columns["MOCODE"].AllowRowFiltering = DefaultableBoolean.True;
            // 允许冻结，且Checked栏位始终处于冻结状态，不可更改          
            //e.Layout.Bands[0].Columns["Checked"].Header.Fixed = true;
            //e.Layout.Bands[0].Columns["TransferLine"].Header.FixedHeaderIndicator = FixedHeaderIndicator.Button;
            e.Layout.Bands[0].Columns["PLANDATE"].SortIndicator = SortIndicator.Ascending;

            // CheckTableDetail      
            e.Layout.Bands[1].Columns["MOCODE"].Hidden = true;           
            e.Layout.Bands[1].Columns["MCODE"].Header.Caption = "物料代码";
            e.Layout.Bands[1].Columns["MDESC"].Header.Caption = "物料描述";           
            e.Layout.Bands[1].Columns["QTY"].Header.Caption = "物料总量";
            e.Layout.Bands[1].Columns["SHORTQTY"].Header.Caption = "缺料数量";
            e.Layout.Bands[1].Columns["LOSTQTY"].Header.Caption = "工单标准用量";

            e.Layout.Bands[1].Columns["MOCODE"].Width = 100;
            e.Layout.Bands[1].Columns["MCODE"].Width = 150;
            e.Layout.Bands[1].Columns["MDESC"].Width = 260;
            e.Layout.Bands[1].Columns["QTY"].Width = 100;
            e.Layout.Bands[1].Columns["SHORTQTY"].Width = 100;
            e.Layout.Bands[1].Columns["LOSTQTY"].Width = 100;

            e.Layout.Bands[1].Columns["MOCODE"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[1].Columns["MCODE"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[1].Columns["MDESC"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[1].Columns["QTY"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[1].Columns["SHORTQTY"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[1].Columns["LOSTQTY"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;


            //e.Layout.Bands[1].Columns["MCODE"].Header.Fixed = true;
            //e.Layout.Bands[1].Columns["MCODE"].Header.FixedHeaderIndicator = FixedHeaderIndicator.Button;
            e.Layout.Bands[1].Columns["MCODE"].AllowRowFiltering = DefaultableBoolean.True;
            e.Layout.Bands[1].Columns["MCODE"].SortIndicator = SortIndicator.Ascending;
        }

        /// <summary>
        /// 每行触发
        /// </summary>
        private void ultraGridHead_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            if (e.Row.HasParent() && (decimal.Parse(e.Row.Cells["SHORTQTY"].Value.ToString()) < 0))
            {
                e.Row.Appearance.BackColor = Color.Red;
            }
        }
        #endregion

        #region 按钮事件

        #region 选择库别
        private void btnGetStorageCode_Click(object sender, EventArgs e)
        {
            FStorageCodeMulQuery objForm = new FStorageCodeMulQuery();
            objForm.Owner = this;
            objForm.StartPosition = FormStartPosition.CenterScreen;
            objForm.StorageCodeSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(StorageCodeSelector_StorageCodeSelectedEvent);
            objForm.ShowDialog();
        }

        /// <summary>
        /// 指定从选择界面获得的值
        /// </summary>
        private void StorageCodeSelector_StorageCodeSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.ucLabelEditStorageCode.Value = e.CustomObject;
        }
        #endregion

        #region 选择产品别
        private void btnGetModelCode_Click(object sender, EventArgs e)
        {
            FModelQuery objForm = new FModelQuery();
            objForm.Owner = this;
            objForm.StartPosition = FormStartPosition.CenterScreen;
            objForm.ModelCodeSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(ModelCodeSelector_ModelCodeSelectedEvent);
            objForm.ShowDialog();
        }

        /// <summary>
        /// 指定从选择界面获得的值
        /// </summary>
        private void ModelCodeSelector_ModelCodeSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.ucLabelEditModelCode.Value = e.CustomObject;
        }
        #endregion

        #region 查询按钮触发
        private void btnQuery_Click(object sender, EventArgs e)
        {            
            if (ucLabelEditStorageCode.Value == string.Empty)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$ERROR_Storage_Must_Selected"));
                this.ucLabelEditStorageCode.TextFocus(false, true);
                return;
            }
       
            string storageCode = this.ucLabelEditStorageCode.Value.ToUpper().Trim();
            string modelCode = this.ucLabelEditModelCode.Value.ToUpper().Trim();

            ClearSampleList();//清空当前Grid信息

            #region 查询获取数据
            try
            {
                object[] obj = m_INVFacade.QueryMaterialFullMo(storageCode, modelCode);
                if (obj == null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Not_Find_Metrial"));
                    return;
                }
                foreach (MaterialFull materialFull in obj)
                {
                    #region 主表数据
                    DataRow[] dataRowQty = m_CheckTable.Select(string.Format("MOCODE='{0}'", materialFull.MoCode));  
                    if (dataRowQty.Length == 0)
                    {
                        DataRow rowCheckTable;
                        rowCheckTable = this.m_SampleList.Tables["CheckTable"].NewRow();
                        rowCheckTable["MOCODE"] = materialFull.MoCode;
                        rowCheckTable["ITEMCODE"] = materialFull.Itemcode;
                        rowCheckTable["ITEMDESC"] = materialFull.Modesc;
                        rowCheckTable["PLANTYPE"] = materialFull.PlanType;
                        rowCheckTable["PLANQTY"] = materialFull.Planqty;
                        rowCheckTable["PLANDATE"] = materialFull.Plandate;
                        if (materialFull.PlanType == "MO")
                        {
                            rowCheckTable["PLANTYPENAME"] = MutiLanguages.ParserString("MO");
                        }
                        if (materialFull.PlanType == "PLAN")
                        {
                            rowCheckTable["PLANTYPENAME"] = MutiLanguages.ParserString("PLAN");
                        }
                        
                        this.m_SampleList.Tables["CheckTable"].Rows.Add(rowCheckTable);
                        if (rowCheckTable != null)
                            this.m_SampleList.Tables["CheckTable"].AcceptChanges();
                    }
                    #endregion

                    #region 子表数据
                    DataRow rowCheckTableDetail;
                    rowCheckTableDetail = this.m_SampleList.Tables["CheckTableDetail"].NewRow();
                    rowCheckTableDetail["MCODE"] = materialFull.MCode;                   
                    rowCheckTableDetail["MOCODE"] = materialFull.MoCode;
                    rowCheckTableDetail["MDESC"] = materialFull.Mdesc;
                    rowCheckTableDetail["QTY"] = materialFull.Qty;
                    rowCheckTableDetail["SHORTQTY"] = materialFull.ShortQty;
                    rowCheckTableDetail["LOSTQTY"] = materialFull.LostQty;
                    
                    this.m_SampleList.Tables["CheckTableDetail"].Rows.Add(rowCheckTableDetail);

                    if (rowCheckTableDetail != null)
                        this.m_SampleList.Tables["CheckTableDetail"].AcceptChanges();
                    #endregion
                }
                this.m_SampleList.AcceptChanges();
                this.ultraGridHead.DataSource = this.m_SampleList;
            }
            catch (Exception ex)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, ex.Message));
                return;
            }
            #endregion
        }
        #endregion

        #region 导出按钮触发
        private void btnExport_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog save = new SaveFileDialog();
            save.CheckPathExists = true;
            //save.Filter = "数据文件(*.xls)|*.xls";
            save.Filter = MutiLanguages.ParserString("$CS_ExportFilter");
            save.DefaultExt = "xls";
            save.Title = MutiLanguages.ParserString("$SaveTitle");
            //save.Title = "保存当前数据";
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ultraGridExcelExporter1.Export(ultraGridHead, save.FileName);
            }
        }
        #endregion

        #endregion

        #region 设定Grid显示的数据
        /// <summary>
        /// 初始化Grid将绑定的数据源
        /// </summary>
        private void InitializeSampleListGrid()
        {
            this.m_SampleList = new DataSet();
            this.m_CheckTable = new DataTable("CheckTable");
            this.m_CheckTableDetail = new DataTable("CheckTableDetail");

            //主表添加列，同时需要维护多语言
            this.m_CheckTable.Columns.Add("MOCODE", typeof(string));
            this.m_CheckTable.Columns.Add("ITEMCODE", typeof(string));
            this.m_CheckTable.Columns.Add("ITEMDESC", typeof(string));
            this.m_CheckTable.Columns.Add("PLANTYPE", typeof(string));
            this.m_CheckTable.Columns.Add("PLANTYPENAME", typeof(string));
            this.m_CheckTable.Columns.Add("PLANQTY", typeof(int));
            this.m_CheckTable.Columns.Add("PLANDATE", typeof(int));

            //子表列添加，同时需要维护多语言
            this.m_CheckTableDetail.Columns.Add("MOCODE", typeof(string));
            this.m_CheckTableDetail.Columns.Add("MCODE", typeof(string));            
            this.m_CheckTableDetail.Columns.Add("MDESC", typeof(string));
            this.m_CheckTableDetail.Columns.Add("QTY", typeof(decimal));
            this.m_CheckTableDetail.Columns.Add("LOSTQTY", typeof(decimal));
            this.m_CheckTableDetail.Columns.Add("SHORTQTY", typeof(decimal));           

            this.m_SampleList.Tables.Add(this.m_CheckTable);//添加主表到DataSet
            this.m_SampleList.Tables.Add(this.m_CheckTableDetail);//添加子表到DataSet

            //设定主表和子表的列的关联关系
            DataColumn[] parentCols = new DataColumn[] { this.m_SampleList.Tables["CheckTable"].Columns["MOCODE"] };
            DataColumn[] childCols = new DataColumn[] { this.m_SampleList.Tables["CheckTableDetail"].Columns["MOCODE"] };
            this.m_SampleList.Relations.Add(new DataRelation("SampleGroupAll", parentCols, childCols));

            //绑定设定好的DataSet数据源到Grid，注：不需要子表时，直接绑定DataTable到Grid即可
            this.m_SampleList.AcceptChanges();
            this.ultraGridHead.DataSource = this.m_SampleList;
        }

        /// <summary>
        /// 清空Grid数据
        /// </summary>
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
              
        #endregion

    }
}
