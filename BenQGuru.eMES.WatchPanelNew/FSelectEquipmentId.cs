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
    public partial class FSelectEquipmentId : Form
    {
        #region  变量 属性
        public event ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>> EQPIDSelectedEvent;
        private DataTable m_EQPID = null;

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

        public FSelectEquipmentId()
        {
            InitializeComponent();            
        }

        private void FSelectEquipmentId_Load(object sender, EventArgs e)
        {
            InitialTryLotNorGrid();
            DoQuery();
            this.dataGridViewEQPID.Rows[0].Selected = false;
        }       

        private void ucButtonOK_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewEQPID.Rows.Count == 0)
            {
                return;
            }

            string eqpidList = string.Empty;
            for (int i = 0; i < dataGridViewEQPID.Rows.Count; i++)
            {
                if (dataGridViewEQPID.Rows[i].Cells[0].Value.ToString().ToLower() == "true")
                {
                    eqpidList += "," + dataGridViewEQPID.Rows[i].Cells["EQPID"].Value.ToString().Trim().ToUpper();
                }
            }

            if (eqpidList.Length > 0)
            {
                eqpidList = eqpidList.Substring(1);
            }

            ParentChildRelateEventArgs<string> args = new ParentChildRelateEventArgs<string>(eqpidList);
            this.OnEQPIDSelectedEvent(sender, args);
            this.Close();
        }

        private void ucButtonExit_Click(object sender, EventArgs e)
        {

        }

        private void TxtEQPID_InnerTextChanged(object sender, EventArgs e)
        {
            m_EQPID.DefaultView.RowFilter = "EQPID like '" + this.TxtEQPID.Value.Trim().ToUpper() + "%'";
        }

        private void dataGridViewEQPID_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewEQPID.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewCheckBoxCell)
            {
                foreach (DataGridViewRow row in this.dataGridViewEQPID.Rows)
                {
                    if (row.Index != e.RowIndex)
                    {
                        row.Cells["Checked"].Value = false;
                    }
                }

                this.dataGridViewEQPID.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
            }
        }


        #endregion

        #region 自定义事件
        private void InitialTryLotNorGrid()
        {
            this.m_EQPID = new DataTable();

            this.m_EQPID.Columns.Add("Checked", typeof(bool));
            this.m_EQPID.Columns.Add("EQPID", typeof(string));
            this.m_EQPID.Columns.Add("EQPDesc", typeof(string));

            this.m_EQPID.AcceptChanges();

            this.dataGridViewEQPID.DataSource = this.m_EQPID;
            this.dataGridViewEQPID.Columns[0].FillWeight = 5;
            this.dataGridViewEQPID.Columns[1].FillWeight = 20;
            this.dataGridViewEQPID.Columns[2].FillWeight = 30;
            this.dataGridViewEQPID.Columns[0].HeaderText = "";
            this.dataGridViewEQPID.Columns[1].HeaderText = "设备ID";
            this.dataGridViewEQPID.Columns[2].HeaderText = "设备描述";
            this.dataGridViewEQPID.Columns[1].ReadOnly = true;
            this.dataGridViewEQPID.Columns[2].ReadOnly = true;
        }

        public void OnEQPIDSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            if (this.EQPIDSelectedEvent != null)
            {
                EQPIDSelectedEvent(sender, e);
            }
        }

        private void DoQuery()
        {
            WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);
            object[] equipmentList = watchPanelFacade.GetAllEquipment();

            if (equipmentList != null)
            {
                DataRow rowNew;
                foreach (Equipment equipment in equipmentList)
                {
                    rowNew = this.m_EQPID.NewRow();
                    rowNew["Checked"] = false;
                    rowNew["EQPID"] = equipment.EqpId;
                    rowNew["EQPDesc"] = equipment.EqpDesc;
                    this.m_EQPID.Rows.Add(rowNew);
                }
                this.m_EQPID.AcceptChanges();
            }            
        }

        #endregion        

        
        
           

    }
}
