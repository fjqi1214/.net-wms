using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.WatchPanelNew
{
    public partial class FSelectSSCode : Form
    {
        #region  变量 属性
        public event ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>> BiglineSelectedEvent;
        private DataTable m_BigLine = null;

        private IDomainDataProvider _dataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                if (_dataProvider == null)
                {

                    _dataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider(Program.DBName);
                }

                return _dataProvider;
            }

            set
            {
                _dataProvider = value;
            }
        }

        #endregion

        #region 页面事件

        public FSelectSSCode()
        {
            InitializeComponent();                              
        }

        private void FSelectBigLines_Load(object sender, EventArgs e)
        {
            InitialTryLotNorGrid();           
            DoQuery();
            this.dataGridViewSSCode.Rows[0].Selected = false;
        }

        private void ucButtonOK_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewSSCode.Rows.Count == 0)
            {
                return;
            }

            string bigLineList = string.Empty;
            for (int i = 0; i < dataGridViewSSCode.Rows.Count; i++)
            {
                if (dataGridViewSSCode.Rows[i].Cells[0].Value.ToString().ToLower() == "true")
                {
                    bigLineList += "," + dataGridViewSSCode.Rows[i].Cells["SSCode"].Value.ToString().Trim().ToUpper();
                }
            }

            if (bigLineList.Length > 0)
            {
                bigLineList = bigLineList.Substring(1);
            }

            ParentChildRelateEventArgs<string> args = new ParentChildRelateEventArgs<string>(bigLineList);
            this.OnBigLineSelectedEvent(sender, args);
            this.Close();
        }

        private void ucButtonExit_Click(object sender, EventArgs e)
        {

        }

        private void TxtSSCode_InnerTextChanged(object sender, EventArgs e)
        {
            m_BigLine.DefaultView.RowFilter = "SSCode like '" + this.TxtSSCode.Value.Trim().ToUpper() + "%'";
        }

        private void dataGridViewSSCode_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewSSCode.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewCheckBoxCell)
            {
                this.dataGridViewSSCode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
            }
        }
        #endregion

        #region 自定义事件
        private void InitialTryLotNorGrid()
        {
            this.m_BigLine = new DataTable();

            this.m_BigLine.Columns.Add("Checked", typeof(bool));
            this.m_BigLine.Columns.Add("SSCode", typeof(string));
            this.m_BigLine.Columns.Add("SSCodeDesc", typeof(string));

            this.m_BigLine.AcceptChanges();            
            this.dataGridViewSSCode.DataSource = this.m_BigLine;
            this.dataGridViewSSCode.Columns[0].FillWeight = 5;
            this.dataGridViewSSCode.Columns[1].FillWeight = 20; 
            this.dataGridViewSSCode.Columns[2].FillWeight = 30;
            this.dataGridViewSSCode.Columns[0].HeaderText = "";
            this.dataGridViewSSCode.Columns[1].HeaderText = "产线";
            this.dataGridViewSSCode.Columns[2].HeaderText = "产线描述";
            this.dataGridViewSSCode.Columns[1].ReadOnly = true;
            this.dataGridViewSSCode.Columns[2].ReadOnly = true;
        }

        public void OnBigLineSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            if (this.BiglineSelectedEvent != null)
            {
                BiglineSelectedEvent(sender, e);
            }
        }

        private void DoQuery()
        {
            WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);
            object[] bigLineList = watchPanelFacade.GetAllStepSequence();

            if (bigLineList != null)
            {
                DataRow rowNew;
                foreach (StepSequence ss in bigLineList)
                {
                    rowNew = this.m_BigLine.NewRow();
                    rowNew["Checked"] = false;
                    rowNew["SSCode"] = ss.StepSequenceCode;
                    rowNew["SSCodeDesc"] = ss.StepSequenceDescription;
                    this.m_BigLine.Rows.Add(rowNew);
                }
                this.m_BigLine.AcceptChanges();
            }            
        }

        #endregion



       
        
    }
}