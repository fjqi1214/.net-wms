using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.WatchPanelDisToLine
{
    public partial class DisToLineGridShow : System.Windows.Forms.UserControl
    {
        #region 变量、属性
        private DataTable m_DisToLineDataTable = null;
        private double PageChangeTimer = 0;//翻页频率
        private int PageRowNum = 0;//每页显示的条数
        private double MessageChangeTimer = 0;//翻页频率
        private int _FromIQCCount = 0;
        private double _GridDataRefresh = 0;
        private object[] m_DisToLineObjs = null;
        private IDomainDataProvider _DomainDataProvider = ApplicationService.Current().DataProvider;
        private List<DisToLineQuery> listDisToLine = null;
        private int messageNO = 0;
        public double GridDataRefresh
        {
            get { return _GridDataRefresh; }
            set { _GridDataRefresh = value; }
        }
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

        public DisToLineGridShow()
        {
            InitializeComponent();
            InitialultraMaterialDataGrid();
        }

        public DisToLineGridShow(double pageChangeTimer, int pageRowNum, double messageChangeTimer)
        {
            InitializeComponent();
            InitializeListMessage();
            //初始化信息     
            InitialultraMaterialDataGrid();
            PageChangeTimer = pageChangeTimer;
            PageRowNum = pageRowNum;
            MessageChangeTimer = messageChangeTimer;
            this.timer1.Interval = Convert.ToInt32(PageChangeTimer * 60000);
            this.timer1.Enabled = true;
            this.timer2.Interval = Convert.ToInt32(MessageChangeTimer * 60000);
            this.timer2.Enabled = true;
        }

        #region  自定义事件
        private void InitializeListMessage()
        {
            listDisToLine = new List<DisToLineQuery>();
        }
        //初始化ProductGrid
        private void InitialultraMaterialDataGrid()
        {
            dataGridMaterial.ClearSelection();
            this.m_DisToLineDataTable = new DataTable();

            this.m_DisToLineDataTable.Columns.Add("SegCode", typeof(string));
            this.m_DisToLineDataTable.Columns.Add("SSCode", typeof(string));
            this.m_DisToLineDataTable.Columns.Add("MoCode", typeof(string));
            this.m_DisToLineDataTable.Columns.Add("MCode", typeof(string));
            this.m_DisToLineDataTable.Columns.Add("MName", typeof(string));
            this.m_DisToLineDataTable.Columns.Add("MoPlanQty", typeof(decimal));
            this.m_DisToLineDataTable.Columns.Add("MssSisQty", typeof(decimal));
            this.m_DisToLineDataTable.Columns.Add("MssLeftQty", typeof(decimal));
            this.m_DisToLineDataTable.Columns.Add("LeftTime", typeof(string));
            this.m_DisToLineDataTable.Columns.Add("Status", typeof(string));

            this.m_DisToLineDataTable.AcceptChanges();
            this.dataGridMaterial.DataSource = this.m_DisToLineDataTable;

            this.dataGridMaterial.Columns["SegCode"].FillWeight = 8;
            this.dataGridMaterial.Columns["SSCode"].FillWeight = 8;
            this.dataGridMaterial.Columns["MoCode"].FillWeight = 8;
            this.dataGridMaterial.Columns["MCode"].FillWeight = 10;
            this.dataGridMaterial.Columns["MName"].FillWeight = 11;
            this.dataGridMaterial.Columns["MoPlanQty"].FillWeight = 12;
            this.dataGridMaterial.Columns["MssSisQty"].FillWeight = 12;
            this.dataGridMaterial.Columns["MssLeftQty"].FillWeight = 11;
            this.dataGridMaterial.Columns["LeftTime"].FillWeight = 12;
            this.dataGridMaterial.Columns["Status"].FillWeight = 8;

            this.dataGridMaterial.Columns["SegCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridMaterial.Columns["SSCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridMaterial.Columns["MoCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridMaterial.Columns["MCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridMaterial.Columns["MName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridMaterial.Columns["MoPlanQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridMaterial.Columns["MssSisQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridMaterial.Columns["MssLeftQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridMaterial.Columns["LeftTime"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridMaterial.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.dataGridMaterial.Columns["SegCode"].HeaderText = "车间";
            this.dataGridMaterial.Columns["SSCode"].HeaderText = "生产线";
            this.dataGridMaterial.Columns["MoCode"].HeaderText = "工单";
            this.dataGridMaterial.Columns["MCode"].HeaderText = "物料编码";
            this.dataGridMaterial.Columns["MName"].HeaderText = "物料名称";
            this.dataGridMaterial.Columns["MoPlanQty"].HeaderText = "工单计划用量";
            this.dataGridMaterial.Columns["MssSisQty"].HeaderText = "产线已发数量";
            this.dataGridMaterial.Columns["MssLeftQty"].HeaderText = "产线剩余量";
            this.dataGridMaterial.Columns["LeftTime"].HeaderText = "剩余生产时间";
            this.dataGridMaterial.Columns["Status"].HeaderText = "配送状态";

            dataGridMaterial.RowsDefaultCellStyle.Font = new Font("宋体", 18, FontStyle.Bold);
            dataGridMaterial.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridMaterial.GridColor = Color.Black;
        }
        #endregion

        public void InitControlsValue()
        {
            RefreshOnlineResMessage();
            BindMessage();
        }

        //刷新物料送检信息
        private void RefreshOnlineResMessage()
        {
            this.m_DisToLineDataTable.Clear();
            WatchPanelDisToLineFacade watchPanelDisToLineFacade = new WatchPanelDisToLineFacade(this.DataProvider);
            object[] disToLineObjs = watchPanelDisToLineFacade.GetDisToLineQuery();
            if (disToLineObjs != null)
            {
                m_DisToLineObjs = disToLineObjs;
                BindDataTableMaterial(disToLineObjs);
            }
        }

        private void BindDataTableMaterial(object[] disToLineObjs)
        {
            m_DisToLineDataTable.Rows.Clear();
            if (disToLineObjs.Length <= PageRowNum)//不需要翻页
            {
                foreach (DisToLineQuery disToLine in disToLineObjs)
                {
                    DataRow newRow = this.m_DisToLineDataTable.NewRow();
                    newRow["SegCode"] = disToLine.SegCode;
                    newRow["SSCode"] = disToLine.SSCode;
                    newRow["MoCode"] = disToLine.MOCode;
                    newRow["MCode"] = disToLine.MCode;
                    newRow["MName"] = disToLine.MName;
                    newRow["MoPlanQty"] = disToLine.MOPlanQty;
                    newRow["MssSisQty"] = disToLine.MSSDisQty;
                    newRow["MssLeftQty"] = disToLine.MSSLeftQty;
                    string time = string.Empty;
                    if (disToLine.lefttime < 60)
                    {
                        time = disToLine.lefttime.ToString() + "秒";
                    }
                    else if (disToLine.lefttime % 60 > 0)
                    {
                        time = (Math.Ceiling(disToLine.lefttime / 60)).ToString() + "分" + Math.Ceiling(disToLine.lefttime % 60) + "秒";
                    }
                    else
                    {
                        time = (Math.Ceiling(disToLine.lefttime / 60)).ToString() + "分";
                    }
                    newRow["LeftTime"] = time;
                    newRow["Status"] = GetStatusCHS(GetDisToLineStatus(disToLine));
                    this.m_DisToLineDataTable.Rows.Add(newRow);
                    if (NeedAddLine(disToLine))
                    {
                        listDisToLine.Add(disToLine);
                    }
                }
            }
            else
            {
                if (_FromIQCCount + PageRowNum < disToLineObjs.Length)
                {
                    for (int i = _FromIQCCount; i < _FromIQCCount + PageRowNum; i++)
                    {
                        DisToLineQuery disToLine = (DisToLineQuery)disToLineObjs[i];
                        DataRow newRow = this.m_DisToLineDataTable.NewRow();
                        newRow["SegCode"] = disToLine.SegCode;
                        newRow["SSCode"] = disToLine.SSCode;
                        newRow["MoCode"] = disToLine.MOCode;
                        newRow["MCode"] = disToLine.MCode;
                        newRow["MName"] = disToLine.MName;
                        newRow["MoPlanQty"] = disToLine.MOPlanQty;
                        newRow["MssSisQty"] = disToLine.MSSDisQty;
                        newRow["MssLeftQty"] = disToLine.MSSLeftQty;
                        string time = string.Empty;
                        if (disToLine.lefttime < 60)
                        {
                            time = disToLine.lefttime.ToString() + "秒";
                        }
                        else if (disToLine.lefttime % 60 > 0)
                        {
                            time = (Math.Ceiling(Convert.ToDecimal(disToLine.lefttime / 60))).ToString() + "分" + disToLine.lefttime % 60 + "秒";
                        }
                        else
                        {
                            time = (Math.Ceiling(Convert.ToDecimal(disToLine.lefttime / 60))).ToString() + "分";
                        }
                        newRow["LeftTime"] = time;
                        newRow["Status"] = GetStatusCHS(GetDisToLineStatus(disToLine));
                        this.m_DisToLineDataTable.Rows.Add(newRow);
                        if (NeedAddLine(disToLine))
                        {
                            listDisToLine.Add(disToLine);
                        }
                    }
                    _FromIQCCount += PageRowNum;
                }
                else
                {
                    for (int i = _FromIQCCount; i < disToLineObjs.Length; i++)
                    {
                        DisToLineQuery disToLine = (DisToLineQuery)disToLineObjs[i];
                        DataRow newRow = this.m_DisToLineDataTable.NewRow();
                        newRow["SegCode"] = disToLine.SegCode;
                        newRow["SSCode"] = disToLine.SSCode;
                        newRow["MoCode"] = disToLine.MOCode;
                        newRow["MCode"] = disToLine.MCode;
                        newRow["MName"] = disToLine.MName;
                        newRow["MoPlanQty"] = disToLine.MOPlanQty;
                        newRow["MssSisQty"] = disToLine.MSSDisQty;
                        newRow["MssLeftQty"] = disToLine.MSSLeftQty;
                        string time = string.Empty;
                        if (disToLine.lefttime < 60)
                        {
                            time = disToLine.lefttime.ToString() + "秒";
                        }
                        else if (disToLine.lefttime % 60 > 0)
                        {
                            time = (Math.Ceiling(disToLine.lefttime / 60)).ToString() + "分" + Math.Ceiling(disToLine.lefttime % 60) + "秒";
                        }
                        else
                        {
                            time = (Math.Ceiling(disToLine.lefttime / 60)).ToString() + "分";
                        }
                        newRow["LeftTime"] = time;
                        newRow["Status"] = GetStatusCHS(GetDisToLineStatus(disToLine));
                        this.m_DisToLineDataTable.Rows.Add(newRow);
                        if (NeedAddLine(disToLine))
                        {
                            listDisToLine.Add(disToLine);
                        }
                    }
                    _FromIQCCount = 0;
                }
            }
            m_DisToLineDataTable.AcceptChanges();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BindDataTableMaterial(m_DisToLineObjs);
        }

        private void dataGridMaterial_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null) return;
            for (int i = 0; i < dataGridMaterial.Rows.Count; i++)
            {
                if (this.dataGridMaterial.Rows[i].Cells["Status"].Value.ToString() == "缺料中")
                {
                    this.dataGridMaterial.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
                else if (this.dataGridMaterial.Rows[i].Cells["Status"].Value.ToString() == "紧急配送")
                {
                    this.dataGridMaterial.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else if (this.dataGridMaterial.Rows[i].Cells["Status"].Value.ToString() == "待配送")
                {
                    this.dataGridMaterial.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                }
                this.dataGridMaterial.Rows[i].Selected = false;
            }
        }

        private string GetStatusCHS(string status)
        {
            switch (status)
            {
                case "ShortDis": return "缺料中";
                case "ERDis": return "紧急配送";
                case "WaitDis": return "待配送";
                case "Normal": return "正常";
                default: return status;
            }
        }
        private string GetStatusMessageCHS(string status)
        {
            switch (status)
            {
                case "ShortDis": return "已缺料，请立即发料！";
                case "ERDis": return "需紧急发料！";
                case "WaitDis": return "需发料！";
                default: return status;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            BindMessage();
        }

        public void BindMessage()
        {
            if (listDisToLine.Count > 0 && messageNO < listDisToLine.Count)
            {
                DisToLineQuery disToLineQuery = listDisToLine[messageNO];
                WatchPanelDisToLineFacade watchPanelDisToLineFacade = new WatchPanelDisToLineFacade(this.DataProvider);
                object[] disToLineObjs = watchPanelDisToLineFacade.GetDisToLineQuery(disToLineQuery);
                if (disToLineObjs != null && disToLineObjs.Length > 0)
                    disToLineQuery = disToLineObjs[0] as DisToLineQuery;
                disToLineQuery.status = GetDisToLineStatus(disToLineQuery);
                if (!NeedAddLine(disToLineQuery))
                {
                    listDisToLine.Remove(disToLineQuery);
                }
                string message = disToLineQuery.SegCode + "车间" + disToLineQuery.SSCode + "产线"
                    + disToLineQuery.MCode + "物料" + GetStatusMessageCHS(disToLineQuery.status);
                this.exceptionMessageControl.ExceptionMessage = message;
                if (disToLineQuery.status == "WaitDis")
                    this.exceptionMessageControl.MessageColor = Color.LightBlue;
                else if (disToLineQuery.status == "ERDis")
                    this.exceptionMessageControl.MessageColor = Color.Yellow;
                else if (disToLineQuery.status == "ShortDis")
                    this.exceptionMessageControl.MessageColor = Color.Red;

                messageNO++;
            }
            else
            {
                this.exceptionMessageControl.ExceptionMessage = string.Empty;
                messageNO = 0;
            }
        }

        private bool NeedAddLine(DisToLineQuery disToLineQuery)
        {
            if (disToLineQuery.status == "ShortDis" || disToLineQuery.status == "ERDis" || disToLineQuery.status == "WaitDis")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //获取配送实时状态
        public string GetDisToLineStatus(DisToLineQuery disToLine)//ALERTDISER
        {
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(DataProvider);
            string strNormal = systemSettingFacade.GetParameterAlias("ALERTMATERIALDISGROUP", "ALERTDISNORMAL");
            int normal = 0;
            if (!string.IsNullOrEmpty(strNormal))
            {
                normal = Convert.ToInt32(strNormal);
            }
            string strDisER = systemSettingFacade.GetParameterAlias("ALERTMATERIALDISGROUP", "ALERTDISER");
            int disER = 0;
            if (!string.IsNullOrEmpty(strDisER))
            {
                disER = Convert.ToInt32(strDisER);
            }
            //状态根据剩余生产时间换算（小于cycleTime为缺料中、小于紧急预警时间为紧急配料、小于正常预警时间为待配送）
            if (disToLine.lefttime < disToLine.CycleTime)
            {
                return "ShortDis";
            }
            if (disToLine.lefttime <= disER)
            {
                return "ERDis";
            }
            if (disToLine.lefttime <= normal)
            {
                return "WaitDis";
            }
            return "Normal";
        }

    }
}
