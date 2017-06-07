using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using Infragistics.Win.UltraWinGrid;
using UserControl;
using BenQGuru.eMES.Domain.DataCollect;

namespace BenQGuru.eMES.Client
{
    public partial class FDisTOLineList : BaseForm
    {

        #region 变量、属性
        private UltraWinGridHelper GridHelper = null;
        private DataTable DataLoadDetail = new DataTable();
        private BaseModelFacade baseModelFacade = null;
        private DataCollectFacade dataCollectFacade = null;
        private Timer timerRefresh;

        private IDomainDataProvider _DomainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get { return _DomainDataProvider; }
        }
        #endregion
        public FDisTOLineList()
        {
            InitializeComponent();
        }

        public FDisTOLineList(string moCode,string mCode,string segCode,string ssCode,string opCode)
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);
            InitializeUltraGrid();

            this.txtMoQuery.Value = moCode;
            this.txtMCodeQuery.Value = mCode;
            this.txtSegCode.Value = segCode;
            this.txtSSCode.Value = ssCode;
            this.txtMoQuery.Enabled = false;
            this.txtMCodeQuery.Enabled = false;
            this.txtSegCode.Enabled = false;
            this.txtSSCode.Enabled = false;

            QueryResult(moCode, mCode, segCode, ssCode, opCode);
        }

        #region 方法
        //初始化Grid区域
        private void InitUltraGridUI(UltraGrid ultraGrid)
        {
            ultraGrid.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            ultraGrid.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGrid.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGrid.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("LightBlue");
            ultraGrid.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            ultraGrid.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            ultraGrid.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            ultraGrid.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            ultraGrid.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }
        private void InitializeUltraGrid()
        {
            InitUltraGridUI(this.ultraGridMain);

            DataLoadDetail.Columns.Add("SeqNo", typeof(int));
            DataLoadDetail.Columns.Add("Status", typeof(string));//产线预警状态
            DataLoadDetail.Columns.Add("StatusCN", typeof(string));//产线预警状态
            DataLoadDetail.Columns.Add("MSSDisQty", typeof(decimal));
            DataLoadDetail.Columns.Add("MSSLeftQty", typeof(decimal));
            DataLoadDetail.Columns.Add("MSSLeftTime", typeof(string));
            DataLoadDetail.Columns.Add("MQty", typeof(decimal));//上次发料数量
            DataLoadDetail.Columns.Add("DelFlag", typeof(string));//重新配送
            DataLoadDetail.Columns.Add("DelFlagCN", typeof(string));

            DataLoadDetail.Columns["SeqNo"].ReadOnly = true;
            DataLoadDetail.Columns["Status"].ReadOnly = true;
            DataLoadDetail.Columns["StatusCN"].ReadOnly = true;
            DataLoadDetail.Columns["MSSDisQty"].ReadOnly = true;
            DataLoadDetail.Columns["MSSLeftQty"].ReadOnly = true;
            DataLoadDetail.Columns["MSSLeftTime"].ReadOnly = true;
            DataLoadDetail.Columns["MQty"].ReadOnly = true;
            DataLoadDetail.Columns["DelFlag"].ReadOnly = true;
            DataLoadDetail.Columns["DelFlagCN"].ReadOnly = true;
           
            this.ultraGridMain.DataSource = this.DataLoadDetail;
            DataLoadDetail.Clear();

            ultraGridMain.DisplayLayout.Bands[0].Columns["SeqNo"].Width = 28;
            ultraGridMain.DisplayLayout.Bands[0].Columns["Status"].Width = 70;
            ultraGridMain.DisplayLayout.Bands[0].Columns["Status"].Hidden = true;
            ultraGridMain.DisplayLayout.Bands[0].Columns["StatusCN"].Width = 70;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MSSDisQty"].Width = 90;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MSSLeftQty"].Width = 90;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MSSLeftTime"].Width = 90;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MQty"].Width = 90;
            ultraGridMain.DisplayLayout.Bands[0].Columns["DelFlag"].Width = 70;
            ultraGridMain.DisplayLayout.Bands[0].Columns["DelFlag"].Hidden = true;
            ultraGridMain.DisplayLayout.Bands[0].Columns["DelFlagCN"].Width = 70;

            ultraGridMain.DisplayLayout.Bands[0].Columns["SeqNo"].CellActivation = Activation.NoEdit;
            ultraGridMain.DisplayLayout.Bands[0].Columns["Status"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["StatusCN"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MSSDisQty"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MSSLeftQty"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MSSLeftTime"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["MQty"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["DelFlag"].CellActivation = Activation.ActivateOnly;
            ultraGridMain.DisplayLayout.Bands[0].Columns["DelFlagCN"].CellActivation = Activation.ActivateOnly;
        }

        private void QueryResult(string moCode, string mCode, string segCode, string ssCode, string opCode)
        {
            if (dataCollectFacade == null)
                dataCollectFacade = new DataCollectFacade(this.DataProvider);

            try
            {
                object[] objDis = dataCollectFacade.GetDisToLineList(moCode, mCode, segCode, ssCode, opCode);

                DataLoadDetail.Clear();
                if (objDis == null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_No_Data_To_Display"));
                    return;
                }

                int cnt = 0;
                foreach (DisToLineList list in objDis)
                {
                    cnt++;
                    string leftMin = string.Empty;
                    //计算剩余生产时间
                    if (list.MssleftTime <= 59)
                        leftMin = Math.Ceiling(list.MssleftTime) + "秒";
                    else
                        leftMin = (Math.Ceiling(Convert.ToDecimal(list.MssleftTime / 60))).ToString() + "分" + Math.Ceiling(list.MssleftTime % 60) + "秒";

                    DataLoadDetail.Rows.Add(new object[] {
                        cnt,
                        list.Status,
                        MutiLanguages.ParserString("$CS_DisLine_" + list.Status),
                        list.MssdisQty,
                        list.MssleftQty,
                        leftMin,
                        list.MQty,
                        list.Delflag,
                        list.Delflag == "Y" ? "是" : "否",
                    });
                }
            }
            catch (Exception ex)
            {
                ApplicationRun.GetInfoForm().Add(ex.Message);
            }
        }
        #endregion

        #region 事件
        void ultraGridMain_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            GridHelper = new UltraWinGridHelper(this.ultraGridMain);

            GridHelper.AddReadOnlyColumn("SeqNo", "No.");
            GridHelper.AddReadOnlyColumn("Status", "预警");
            GridHelper.AddReadOnlyColumn("StatusCN", "配送预警");
            GridHelper.AddReadOnlyColumn("MSSDisQty", "产线已发数量");
            GridHelper.AddReadOnlyColumn("MSSLeftQty", "产线剩余数量");
            GridHelper.AddReadOnlyColumn("MSSLeftTime", "剩余生产时间");
            GridHelper.AddReadOnlyColumn("MQty", "本次发料数量");
            GridHelper.AddReadOnlyColumn("DelFlag", "");
            GridHelper.AddReadOnlyColumn("DelFlagCN", "重新发料");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ultraGridMain_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            if (e.Row.Cells["DelFlag"].Value.ToString() == "Y")
            {
                e.Row.Cells["DelFlag"].Row.Appearance.BackColor = Color.Yellow;
            }
        }
        #endregion
    }
}
