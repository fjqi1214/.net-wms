using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.TS;
using BenQGuru.eMES.Domain.TSModel;

namespace UserControl
{
    public partial class UCErrorCodeSelectNew : System.Windows.Forms.UserControl
    {
        public DataSet m_ErrorList = null;
        public DataTable m_ErrorGroup = null;
        public DataTable m_ErrorCode = null;

        public UCErrorCodeSelectNew()
        {
            InitializeComponent();
            this.ultraGridErrorList.UpdateMode = UpdateMode.OnCellChange;
            this.ultraGridErrorList.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            this.ultraGridErrorList.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridErrorList.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridErrorList.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridErrorList.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            this.ultraGridErrorList.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridErrorList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridErrorList.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            this.ultraGridErrorList.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            this.ultraGridErrorList.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void ucLabelEditErrorCode_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.ucLabelEditErrorCode.Value.Trim().Length == 0)
                {
                    this.ucLabelEditErrorCode.TextFocus(false, true);
                    return;
                }

                SetGridSelected(ucLabelEditErrorCode.Value,"");

                this.ucLabelEditErrorCode.TextFocus(false, true);
            }
        }

        public bool SetGridSelected(string value,string location)
        {
            bool returnValue = false;
            if (this.ultraGridErrorList.ActiveRow != null)
            {
                this.ultraGridErrorList.ActiveRow.Selected = false;
                this.ultraGridErrorList.ActiveRow = null;
            }

            for (int i = 0; i < this.ultraGridErrorList.Rows.Count; i++)
            {
                if (this.ultraGridErrorList.Rows[i].HasChild(false))
                {
                    for (int j = 0; j < this.ultraGridErrorList.Rows[i].ChildBands[0].Rows.Count; j++)
                    {
                        if (string.Compare(this.ultraGridErrorList.Rows[i].ChildBands[0].Rows[j].Cells["ErrorCodeCode"].Value.ToString(),
                            value.Trim(), true) == 0)
                        {
                            this.ultraGridErrorList.Rows[i].Expanded = true;
                            this.ultraGridErrorList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"].Value = "true";
                            this.ultraGridErrorList.Rows[i].ChildBands[0].Rows[j].Selected = true;
                            this.ultraGridErrorList.Rows[i].ChildBands[0].Rows[j].Activate();
                            returnValue = true;
                        }
                    }
                }
            }
            this.ultraGridErrorList.UpdateData();
            return returnValue;
        }


        private void ultraGridErrorList_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
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

            // 排序
            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            // 不允许删除
            e.Layout.Override.AllowDelete = DefaultableBoolean.False;

            // 滚动提示
            e.Layout.Bands[0].ScrollTipField = "ErrorGroupCode";
            e.Layout.Bands[1].ScrollTipField = "ErrorCodeCode";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["ErrorGroupCode"].Header.Caption = "不良代码组";
            e.Layout.Bands[0].Columns["ErrorGroupDescription"].Header.Caption = "不良代码组描述";
            e.Layout.Bands[0].Columns["ErrorGroupCode"].Width = 100;
            e.Layout.Bands[0].Columns["ErrorGroupDescription"].Width = 200;

            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["ErrorGroupCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["ErrorGroupDescription"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["ErrorGroupCode"].SortIndicator = SortIndicator.Ascending;

            // ErrorCode
            e.Layout.Bands[1].Columns["ErrorGroupCode"].Hidden = true;
            e.Layout.Bands[1].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[1].Columns["ErrorCodeCode"].Header.Caption = "不良代码";
            e.Layout.Bands[1].Columns["ErrorCodeDescription"].Header.Caption = "不良代码描述";

            e.Layout.Bands[1].Columns["Checked"].Width = 40;
            e.Layout.Bands[1].Columns["ErrorCodeCode"].Width = 100;
            e.Layout.Bands[1].Columns["ErrorCodeDescription"].Width = 150;


            e.Layout.Bands[1].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[1].Columns["ErrorCodeCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["ErrorCodeDescription"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["ErrorCodeCode"].SortIndicator = SortIndicator.Ascending;
        }

        private void InitializeErrorListGrid()
        {
            this.m_ErrorList = new DataSet();
            this.m_ErrorGroup = new DataTable("ErrorGroup");
            this.m_ErrorCode = new DataTable("ErrorCode");

            this.m_ErrorGroup.Columns.Add("ErrorGroupCode", typeof(string));
            this.m_ErrorGroup.Columns.Add("ErrorGroupDescription", typeof(string));

            this.m_ErrorCode.Columns.Add("Checked", typeof(string));
            this.m_ErrorCode.Columns.Add("ErrorGroupCode", typeof(string));
            this.m_ErrorCode.Columns.Add("ErrorCodeCode", typeof(string));
            this.m_ErrorCode.Columns.Add("ErrorCodeDescription", typeof(string));

            this.m_ErrorList.Tables.Add(this.m_ErrorGroup);
            this.m_ErrorList.Tables.Add(this.m_ErrorCode);

            this.m_ErrorList.Relations.Add(new DataRelation("ErrorGroupAndErrorCode",
                                                this.m_ErrorList.Tables["ErrorGroup"].Columns["ErrorGroupCode"],
                                                this.m_ErrorList.Tables["ErrorCode"].Columns["ErrorGroupCode"]));
            this.m_ErrorList.AcceptChanges();
            this.ultraGridErrorList.DataSource = this.m_ErrorList;
        }

        private void UCErrorCodeSelectNew_Load(object sender, EventArgs e)
        {
            this.InitializeErrorListGrid();
        }


        public void LoadErrorList(string itemCode, IDomainDataProvider dataProvider)
        {
            TSModelFacade tsFacade = new TSModelFacade(dataProvider);
            try
            {
                this.ClearErrorList();

                object[] errorCodeGroups = tsFacade.GetErrorCodeGroupByItemCode(itemCode);
                if (errorCodeGroups != null)
                {
                    string errorGroupList = "";
                    DataRow newRow;
                    foreach (ErrorCodeGroupA errorGroup in errorCodeGroups)
                    {
                        errorGroupList += errorGroup.ErrorCodeGroup + ",";

                        newRow = this.m_ErrorList.Tables["ErrorGroup"].NewRow();
                        newRow["ErrorGroupCode"] = errorGroup.ErrorCodeGroup;
                        newRow["ErrorGroupDescription"] = errorGroup.ErrorCodeGroupDescription;
                        this.m_ErrorList.Tables["ErrorGroup"].Rows.Add(newRow);
                    }
                    if (errorGroupList.Length > 0)
                    {
                        errorGroupList = errorGroupList.Substring(0, errorGroupList.Length - 1);
                    }

                    // Get ErrorCode List By ErrorGroupList
                    if (errorGroupList.Length > 0)
                    {
                        object[] errorCodes = tsFacade.GetErrorCodeByErrorGroupList(errorGroupList);
                        if (errorCodes != null)
                        {
                            DataRow row;
                            foreach (ErrorGrou2ErrorCode4OQC eg2ec in errorCodes)
                            {
                                row = this.m_ErrorList.Tables["ErrorCode"].NewRow();
                                row["Checked"] = "false";
                                row["ErrorCodeCode"] = eg2ec.ErrorCode;
                                row["ErrorCodeDescription"] = eg2ec.ErrorCodeDescription;
                                row["ErrorGroupCode"] = eg2ec.ErrorCodeGroup;
                                this.m_ErrorList.Tables["ErrorCode"].Rows.Add(row);
                            }
                        }
                    }

                    this.m_ErrorList.Tables["ErrorGroup"].AcceptChanges();
                    this.m_ErrorList.Tables["ErrorCode"].AcceptChanges();
                    this.m_ErrorList.AcceptChanges();
                    this.ultraGridErrorList.DataSource = this.m_ErrorList;
                }
            }
            catch (Exception ex)
            {
            }

        }

        public void ClearErrorList()
        {
            ucLabelEditErrorCode.Value = string.Empty;
            if (this.m_ErrorList == null)
            {
                return;
            }
            this.m_ErrorList.Tables["ErrorCode"].Rows.Clear();
            this.m_ErrorList.Tables["ErrorGroup"].Rows.Clear();

            this.m_ErrorList.Tables["ErrorCode"].AcceptChanges();
            this.m_ErrorList.Tables["ErrorGroup"].AcceptChanges();
            this.m_ErrorList.AcceptChanges();
        }



        public object[] GetSelectedErrorCodeList()
        {
            ArrayList errorCodeList = new ArrayList();
            ErrorCodeGroup2ErrorCode tsErrorCode;

            foreach (DataRow row in this.m_ErrorList.Tables["ErrorCode"].Rows)
            {
                if (string.Compare(Convert.ToString(row["Checked"]), "true", true) == 0)
                {
                    tsErrorCode = new ErrorCodeGroup2ErrorCode();
                    tsErrorCode.ErrorCodeGroup = Convert.ToString(row["ErrorGroupCode"]);
                    tsErrorCode.ErrorCode = Convert.ToString(row["ErrorCodeCode"]);
                    
                    errorCodeList.Add(tsErrorCode);
                }
            }

            return errorCodeList.ToArray();
        }
    }
}
