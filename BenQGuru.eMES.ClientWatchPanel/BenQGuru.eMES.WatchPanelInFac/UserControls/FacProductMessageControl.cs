using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;

using BenQGuru.eMES.Domain.Performance;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;

namespace BenQGuru.eMES.ClientWatchPanel
{
    public partial class FacProductMessageControl : System.Windows.Forms.UserControl
    {
        #region 变量

        private DataTable ProductGrid_DataTable = new DataTable();
        private DataTable BarJoin_DataTable = new DataTable();
        private DataTable Pei_DataTable = new DataTable();

        private object[] _GridDataSource = null;
        private object[] _BarJoinDataSource = null;
        private object[] _PeiDataSource = null;

        private string _BigLineCodeListInProduct = string.Empty;
        private string _BigLineCodeListOutProduct = string.Empty;
        private int _FinshItemQty = 0;
        private int _SemimanuFactureQty = 0;
        //Added By Nettie Chen 2009/09/23
        private bool _IsShowFinishedProduct=true ;
        private bool _IsShowSemimanuProduct = true;
        //End Added

        public int FinshItemQty
        {
            set { _FinshItemQty = value; }
        }

        public int SemimanuFactureQty
        {
            set { _SemimanuFactureQty = value; }
        }

        public string BigLineListInProduct
        {
            set { _BigLineCodeListInProduct = value; }
        }

        public string BigLineListOutProduct
        {
            set { _BigLineCodeListOutProduct = value; }
        }

        public object[] GridDataSource
        {
            set { _GridDataSource = value; }
        }

        public object[] BarJoinDataSource
        {
            set { _BarJoinDataSource = value; }
        }

        public object[] PeiDataSource
        {
            set { _PeiDataSource = value; }
        }
        //Added By Nettie Chen 2009/09/23
        public bool IsShowFinishedProduct
        {
            set { _IsShowFinishedProduct = value; }
        }
        public bool IsShowSemimanuProduct
        {
            set { _IsShowSemimanuProduct = value; }
        }
        //End Added
        #endregion

        #region 事件

        public FacProductMessageControl()
        {
            InitializeComponent();
            InitialultraProdcutDataGrid();
        }

        private void hearMessageControl_Load(object sender, EventArgs e)
        {

        }


        private void ultraGridProduct_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = true;

            e.Layout.Override.RowSizing = RowSizing.Free;
            this.ultraGridProduct.DisplayLayout.Override.RowSizing = RowSizing.Free;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            // 不允许删除
            e.Layout.Override.AllowDelete = DefaultableBoolean.False;

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Mtype"].Header.Caption = "";
            e.Layout.Bands[0].Columns["ManCount"].Header.Caption = "人数";
            e.Layout.Bands[0].Columns["MonthProductQty"].Header.Caption = "当月产量";
            e.Layout.Bands[0].Columns["UPPH"].Header.Caption = "UPPH";
            e.Layout.Bands[0].Columns["PassRate"].Header.Caption = "直通率";

            e.Layout.Bands[0].Columns["Mtype"].Width = 100;
            e.Layout.Bands[0].Columns["ManCount"].Width = 130;
            e.Layout.Bands[0].Columns["ManCount"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            e.Layout.Bands[0].Columns["MonthProductQty"].Width = 100;
            e.Layout.Bands[0].Columns["MonthProductQty"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            e.Layout.Bands[0].Columns["UPPH"].Width = 120;
            e.Layout.Bands[0].Columns["UPPH"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            e.Layout.Bands[0].Columns["PassRate"].Width = 120;
            e.Layout.Bands[0].Columns["PassRate"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["Mtype"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["ManCount"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["MonthProductQty"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["UPPH"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["PassRate"].CellActivation = Activation.NoEdit;
        }

        #endregion

        #region 自定义函数

        public void InitControlsValue()
        {
            this.hearMessageControl.SetTitle = "车间概况电子看板";
            //Modified By Nettie Chen 2009/09/23
            //this.lblFinshItemQty.Text = _FinshItemQty.ToString();
            //this.lblSemimanuFactureQty.Text = _SemimanuFactureQty.ToString();
            if(_IsShowFinishedProduct==true && _IsShowSemimanuProduct ==true)
            {
                this.lblFinshCaption.Text = "成 品 产 量";
                this.lblFinshItemQty.Text = _FinshItemQty.ToString();
                this.lblSemimanuFactureQty.Text = _SemimanuFactureQty.ToString();
                this.lblFinshItemQty.ForeColor = Color.LimeGreen;
            }
            else if (_IsShowFinishedProduct == true)
            {
                this.lblFinshItemQty.Text = _FinshItemQty.ToString();
                if (this.panel6.Visible == true)
                {
                    this.lblFinshCaption.Text = "成 品 产 量";
                    this.lblFinshItemQty.ForeColor = Color.LimeGreen;
                    this.lblFinshItemQty.Height = this.lblFinshItemQty.Height + this.lblSemimanuFactureQty.Top + this.lblSemimanuFactureQty.Height;
                    this.panel6.Visible = false;
                }

            }
            else if (_IsShowSemimanuProduct == true)
            {
                this.lblFinshItemQty.Text = _SemimanuFactureQty.ToString();
                if (this.panel6.Visible == true)
                {
                    this.lblFinshCaption.Text = "半 成 品 产 量";
                    this.lblFinshItemQty.ForeColor = Color.Yellow;
                    this.lblFinshItemQty.Height = this.lblFinshItemQty.Height + this.lblSemimanuFactureQty.Top + this.lblSemimanuFactureQty.Height;
                    this.panel6.Visible = false;
                }

            }
            //End Modified
            this.lblSSCodeListInProduct.Text = _BigLineCodeListInProduct;
            this.lblSSCodeListOutProduct.Text = _BigLineCodeListOutProduct;

            SetProductGrid();
            SetBarJionChartValue();
            SetPeiChartValue();
        }

        private void InitialultraProdcutDataGrid()
        {
            ProductGrid_DataTable = new DataTable();

            ProductGrid_DataTable.Columns.Add("Mtype", typeof(string));
            ProductGrid_DataTable.Columns.Add("ManCount", typeof(int));
            ProductGrid_DataTable.Columns.Add("MonthProductQty", typeof(int));
            ProductGrid_DataTable.Columns.Add("UPPH", typeof(double));
            ProductGrid_DataTable.Columns.Add("PassRate", typeof(double));

            ProductGrid_DataTable.AcceptChanges();

            this.ultraGridProduct.DataSource = ProductGrid_DataTable;
        }

        private void SetProductGrid()
        {
            ProductGrid_DataTable.Clear();
            if (_GridDataSource != null)
            {
                foreach (watchPanelProductDate obj in _GridDataSource)
                {
                    //Added condition By Nettie Chen 2009/09/23
                    if ((obj.Mtype == ItemType.ITEMTYPE_FINISHEDPRODUCT && _IsShowFinishedProduct==true)||(obj.Mtype != ItemType.ITEMTYPE_FINISHEDPRODUCT && _IsShowSemimanuProduct==true))
                    //End Added
                    {
                        DataRow NewRow = this.ProductGrid_DataTable.NewRow();

                        NewRow["Mtype"] = string.Empty;
                        if (obj.Mtype == ItemType.ITEMTYPE_FINISHEDPRODUCT)
                        {
                            NewRow["Mtype"] = "成品";
                        }
                        else
                        {
                            NewRow["Mtype"] = "半成品";
                        }


                        NewRow["ManCount"] = obj.ManCount;
                        NewRow["MonthProductQty"] = obj.MonthProductQty;
                        NewRow["UPPH"] = Math.Round(obj.UPPH, 4);
                        NewRow["PassRate"] = Math.Round(obj.PassRate,4);

                        ProductGrid_DataTable.Rows.Add(NewRow);
                    }
                }
            }

            ProductGrid_DataTable.AcceptChanges();

            this.ultraGridProduct.ActiveRow = null;

            if (this.ultraGridProduct.Rows.Count == 1)
            {
                this.ultraGridProduct.Rows[0].CellAppearance.ForeColor = Color.LimeGreen;
                this.ultraGridProduct.Rows[0].Cells[0].Appearance.ForeColor = Color.White;
            }

            if (this.ultraGridProduct.Rows.Count == 2)
            {
                this.ultraGridProduct.Rows[0].CellAppearance.ForeColor = Color.LimeGreen;
                this.ultraGridProduct.Rows[1].CellAppearance.ForeColor = Color.Yellow;
                this.ultraGridProduct.Rows[0].Cells[0].Appearance.ForeColor = Color.White;
                this.ultraGridProduct.Rows[1].Cells[0].Appearance.ForeColor = Color.White;
            }

        }

        private void SetBarJionChartValue()
        {
            this.BarJoin_DataTable = new DataTable();
            BarJoin_DataTable.Columns.Add("QualifiedRate", typeof(double));
            BarJoin_DataTable.Columns.Add("SSCode", typeof(string));
            BarJoin_DataTable.AcceptChanges();

            if (_BarJoinDataSource != null)
            {
                foreach (watchPanelProductDate obj in _BarJoinDataSource)
                {
                    DataRow newRow = this.BarJoin_DataTable.NewRow();

                    newRow["QualifiedRate"] = Math.Round(obj.OQCLotPassRate*100, 2);
                    newRow["SSCode"] = obj.SSCode;

                    BarJoin_DataTable.Rows.Add(newRow);
                }
            }

            BarJoin_DataTable.AcceptChanges();

            LotbarJoin.JoinPen.Color = Color.White;
            LotbarJoin.JoinPen.Width = 2;
            //Added By Nettie Chen on 2009/12/10
            this.LotbarJoin.XValues.DataMember = "";
            //End Added
            LotbarJoin.YValues.DataMember = BarJoin_DataTable.Columns["QualifiedRate"].ToString();
            LotbarJoin.LabelMember = BarJoin_DataTable.Columns["SSCode"].ToString();

            LotbarJoin.Marks.Style = Steema.TeeChart.Styles.MarksStyles.Value;
            LotbarJoin.DataSource = BarJoin_DataTable;

            tChartLotPass.Refresh();
        }

        private void SetPeiChartValue()
        {
            this.Pei_DataTable = new DataTable();
            Pei_DataTable.Columns.Add("ErroeCauseDesc", typeof(string));
            Pei_DataTable.Columns.Add("ErrorCauseRate", typeof(double));
            Pei_DataTable.AcceptChanges();

            decimal rate = 0;
            if (_PeiDataSource != null)
            {
                foreach (QDOTSInfo obj in _PeiDataSource)
                {
                    DataRow newRow = this.Pei_DataTable.NewRow();

                    newRow["ErroeCauseDesc"] = obj.ErrorCauseDesc;
                    newRow["ErrorCauseRate"] = Math.Round(obj.Percent, 4);
                    rate += Math.Round(obj.Percent, 4);
                    Pei_DataTable.Rows.Add(newRow);
                }

                DataRow Row = this.Pei_DataTable.NewRow();
                Row["ErroeCauseDesc"] = "其他原因";
                Row["ErrorCauseRate"] = Math.Round(1 - rate, 4);
                Pei_DataTable.Rows.Add(Row);

                Pei_DataTable.AcceptChanges();

                NGRatePie.YValues.DataMember = Pei_DataTable.Columns["ErrorCauseRate"].ToString();
                NGRatePie.LabelMember = Pei_DataTable.Columns["ErroeCauseDesc"].ToString();
            }



            NGRatePie.Marks.Style = Steema.TeeChart.Styles.MarksStyles.LabelPercent;
            NGRatePie.DataSource = Pei_DataTable;

            tChartNGRate.Refresh();
        }

        #endregion

    }
}
