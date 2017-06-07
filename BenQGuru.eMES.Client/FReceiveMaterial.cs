using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Infragistics.Win.UltraWinGrid;

using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using UserControl;

namespace BenQGuru.eMES.Client
{
    public partial class FReceiveMaterial : Form
    {

        #region  变量

        private UltraWinGridHelper _UltraWinGridHelper1 = null;
        private IDomainDataProvider _DomainDataProvider = ApplicationService.Current().DataProvider;
        private DataTable _DataTableLoadedPart = new DataTable();


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

        public FReceiveMaterial()
        {
            InitializeComponent();
        }

        private void FReceiveMaterial_Load(object sender, EventArgs e)
        {
            InitializeUltraGrid();
            this.ucPlanDate.Value = DateTime.Now.Date;            
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
            InitUltraGridUI(this.ultraGridMaterial);

            _DataTableLoadedPart.Columns.Add("Check", typeof(bool));
            _DataTableLoadedPart.Columns.Add("IssueSEQ", typeof(int));
            _DataTableLoadedPart.Columns.Add("PlanDate", typeof(int));
            _DataTableLoadedPart.Columns.Add("BigSSCode", typeof(string));
            _DataTableLoadedPart.Columns.Add("PlanSEQ", typeof(int));
            _DataTableLoadedPart.Columns.Add("MoCode", typeof(string));
            _DataTableLoadedPart.Columns.Add("MoSEQ", typeof(int));
            _DataTableLoadedPart.Columns.Add("IssueQTY", typeof(int));
            _DataTableLoadedPart.Columns.Add("RecevieQTY", typeof(int));
            _DataTableLoadedPart.Columns.Add("MUSER", typeof(string));
            _DataTableLoadedPart.Columns.Add("MDATE", typeof(int));
            _DataTableLoadedPart.Columns.Add("MTIME", typeof(int));


            _DataTableLoadedPart.Columns["Check"].ReadOnly = false;
            _DataTableLoadedPart.Columns["IssueSEQ"].ReadOnly = true;
            _DataTableLoadedPart.Columns["PlanDate"].ReadOnly = true;
            _DataTableLoadedPart.Columns["BigSSCode"].ReadOnly = true;
            _DataTableLoadedPart.Columns["PlanSEQ"].ReadOnly = true;
            _DataTableLoadedPart.Columns["MoCode"].ReadOnly = true;
            _DataTableLoadedPart.Columns["MoSEQ"].ReadOnly = true;
            _DataTableLoadedPart.Columns["IssueQTY"].ReadOnly = true;
            _DataTableLoadedPart.Columns["RecevieQTY"].ReadOnly = false;
            _DataTableLoadedPart.Columns["MUSER"].ReadOnly = true;
            _DataTableLoadedPart.Columns["MDATE"].ReadOnly = true;
            _DataTableLoadedPart.Columns["MTIME"].ReadOnly = true;

            this.ultraGridMaterial.DataSource = this._DataTableLoadedPart;

            _DataTableLoadedPart.Clear();

            ultraGridMaterial.DisplayLayout.Bands[0].Columns["Check"].Width = 18;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["IssueSEQ"].Width = 60;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["PlanDate"].Width = 70;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["BigSSCode"].Width = 90;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["PlanSEQ"].Width = 60;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["MoCode"].Width = 100;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["MoSEQ"].Width = 60;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["IssueQTY"].Width = 60;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["RecevieQTY"].Width = 60;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["MUSER"].Width =90;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["MDATE"].Width = 60;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["MTIME"].Width = 60;

            ultraGridMaterial.DisplayLayout.Bands[0].Columns["Check"].CellActivation = Activation.AllowEdit;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["IssueSEQ"].CellActivation = Activation.NoEdit;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["PlanDate"].CellActivation = Activation.NoEdit;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["BigSSCode"].CellActivation = Activation.NoEdit;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["PlanSEQ"].CellActivation = Activation.NoEdit;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["MoCode"].CellActivation = Activation.NoEdit;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["MoSEQ"].CellActivation = Activation.NoEdit;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["IssueQTY"].CellActivation = Activation.NoEdit;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["RecevieQTY"].CellActivation = Activation.AllowEdit;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["MUSER"].CellActivation = Activation.NoEdit;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["MDATE"].CellActivation = Activation.NoEdit;
            ultraGridMaterial.DisplayLayout.Bands[0].Columns["MTIME"].CellActivation = Activation.NoEdit;
        }

        private void ultraGridMaterial_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            _UltraWinGridHelper1 = new UltraWinGridHelper(this.ultraGridMaterial);

            _UltraWinGridHelper1.AddCheckColumn("Check", "");
            _UltraWinGridHelper1.AddReadOnlyColumn("IssueSEQ", "发料序号");
            _UltraWinGridHelper1.AddReadOnlyColumn("PlanDate", "计划日期");
            _UltraWinGridHelper1.AddReadOnlyColumn("BigSSCode", "大线");
            _UltraWinGridHelper1.AddReadOnlyColumn("PlanSEQ", "生产顺序");
            _UltraWinGridHelper1.AddReadOnlyColumn("MoCode", "工单");
            _UltraWinGridHelper1.AddReadOnlyColumn("MoSEQ", "工单项次");
            _UltraWinGridHelper1.AddReadOnlyColumn("IssueQTY", "配送数量");
            _UltraWinGridHelper1.AddReadOnlyColumn("RecevieQTY", "接受数量");
            _UltraWinGridHelper1.AddReadOnlyColumn("MUSER", "配送人员");
            _UltraWinGridHelper1.AddReadOnlyColumn("MDATE", "配送日期");
            _UltraWinGridHelper1.AddReadOnlyColumn("MTIME", "配送时间");
        }

        #endregion

        #region 页面事件
        private void btnGetBigSSCode_Click(object sender, EventArgs e)
        {
            FBigSSCodeQuery fBigSSCodeQuery = new FBigSSCodeQuery();
            fBigSSCodeQuery.Owner = this;
            fBigSSCodeQuery.StartPosition = FormStartPosition.CenterScreen;
            fBigSSCodeQuery.BigSSCodeSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(BigSSCodeSelector_BigSSCodeSelectedEvent);
            fBigSSCodeQuery.ShowDialog();
            fBigSSCodeQuery = null;

            this.txtMoCode.TextFocus(false, true);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.txtMoCode.TextFocus(false, true);
            MaterialFacade materialFacade = new MaterialFacade(this.DataProvider);
            object[] materialIssueAndPlanSeqs = materialFacade.QueryMaterialIssueAndPlanSeq(this.txtBigSSCode.Value.Trim().ToUpper(),
                                                                                            FormatHelper.TODateInt(this.ucPlanDate.Value),
                                                                                            this.txtMoCode.Value.Trim().ToUpper(),
                                                                                            MaterialIssueType.MaterialIssueType_Issue,
                                                                                            MaterialIssueStatus.MaterialIssueStatus_Delivered);

            _DataTableLoadedPart.Clear();

            if (materialIssueAndPlanSeqs == null)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_No_Data_To_Display"));
                return;
            }

          

            for (int i = 0; i < materialIssueAndPlanSeqs.Length; i++)
            {
                MaterialIssueAndPlanSeq materialIssueAndPlanSeq = materialIssueAndPlanSeqs[i] as MaterialIssueAndPlanSeq;
                _DataTableLoadedPart.Rows.Add(new object[] {"false",
                                                            materialIssueAndPlanSeq.IssueSEQ,
                                                            materialIssueAndPlanSeq.PlanDate,
                                                             materialIssueAndPlanSeq.BigSSCode,
                                                            materialIssueAndPlanSeq.PlanSeq,
                                                            materialIssueAndPlanSeq.MoCode,
                                                            materialIssueAndPlanSeq.MoSeq,
                                                            materialIssueAndPlanSeq.IssueQTY,
                                                            materialIssueAndPlanSeq.IssueQTY,
                                                            materialIssueAndPlanSeq.MaintainUser,
                                                            materialIssueAndPlanSeq.MaintainDate,
                                                            materialIssueAndPlanSeq.MaintainTime
                                                             });
            }


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.txtMoCode.TextFocus(false, true);

            MaterialFacade materialFacade = new MaterialFacade(this.DataProvider);
            if (!HaveSelectedGrid())
            {               
                return;
            }

            if (!GridCheckd())
            {
                return;
            }

            try
            {
                this.DataProvider.BeginTransaction();
                DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                for (int i = 0; i < ultraGridMaterial.Rows.Count; i++)
                {
                    if (ultraGridMaterial.Rows[i].Cells[0].Value.ToString().ToLower() == "true")
                    {
                        MaterialIssue materialIssue = (MaterialIssue)materialFacade.GetMaterialIssue(ultraGridMaterial.Rows[i].Cells["BigSSCode"].Value.ToString(),
                                                                                      int.Parse(ultraGridMaterial.Rows[i].Cells["PlanDate"].Value.ToString()),
                                                                                      ultraGridMaterial.Rows[i].Cells["MoCode"].Value.ToString(),
                                                                                      int.Parse(ultraGridMaterial.Rows[i].Cells["MoSEQ"].Value.ToString()),
                                                                                      int.Parse(ultraGridMaterial.Rows[i].Cells["IssueSEQ"].Value.ToString()));
                        if (materialIssue != null)
                        {
                            materialFacade.UpdateMaterialIssueIssueStatus(materialIssue);

                            MaterialIssue newMaterialIssue = materialFacade.CreateNewMaterialIssue();
                            newMaterialIssue.BigSSCode = ultraGridMaterial.Rows[i].Cells["BigSSCode"].Value.ToString();
                            newMaterialIssue.PlanDate = int.Parse(ultraGridMaterial.Rows[i].Cells["PlanDate"].Value.ToString());
                            newMaterialIssue.MoCode = ultraGridMaterial.Rows[i].Cells["MoCode"].Value.ToString();
                            newMaterialIssue.MoSeq = Convert.ToDecimal(ultraGridMaterial.Rows[i].Cells["MoSEQ"].Value.ToString());
                            newMaterialIssue.IssueSEQ = materialFacade.GetMaterialIssueMaxIssueSEQ(newMaterialIssue.BigSSCode,
                                                                                                   newMaterialIssue.PlanDate,
                                                                                                   newMaterialIssue.MoCode,
                                                                                                   newMaterialIssue.MoSeq);
                            newMaterialIssue.IssueQTY = Convert.ToDecimal(ultraGridMaterial.Rows[i].Cells["IssueQTY"].Value.ToString());
                            newMaterialIssue.IssueType = MaterialIssueType.MaterialIssueType_Receive;
                            newMaterialIssue.IssueStatus = MaterialIssueStatus.MaterialIssueStatus_Close;
                            newMaterialIssue.MaintainUser = ApplicationService.Current().UserCode;
                            newMaterialIssue.MaintainDate = dBDateTime.DBDate;
                            newMaterialIssue.MaintainTime = dBDateTime.DBTime;

                            materialFacade.AddMaterialIssue(newMaterialIssue);

                            //更新BigSSCode+MoCode所有的预警信息
                            object[] workPlanObjects = materialFacade.QueryWorkPlan(newMaterialIssue.BigSSCode, newMaterialIssue.MoCode);
                            if (workPlanObjects!=null)
                            {
                                for (int j = 0; j < workPlanObjects.Length; j++)
                                {
                                    WorkPlan workPlanUpdate = workPlanObjects[j] as WorkPlan;

                                    //更新当前项次的数量
                                    if (workPlanUpdate.PlanDate==newMaterialIssue.PlanDate &&　workPlanUpdate.MoSeq==newMaterialIssue.MoSeq)
                                    {
                                        workPlanUpdate.MaterialQty += Convert.ToInt32(ultraGridMaterial.Rows[i].Cells["RecevieQTY"].Value.ToString());
                                    }

                                    workPlanUpdate.LastReceiveTime = dBDateTime.DBTime;
                                    workPlanUpdate.LastReqTime = 0;
                                    workPlanUpdate.PromiseTime = 0;
                                    workPlanUpdate.MaterialStatus = MaterialWarningStatus.MaterialWarningStatus_No;

                                    materialFacade.UpdateWorkPlan(workPlanUpdate);
                                }
                            }

                            materialFacade.UpdateMaterialReqInfo(ultraGridMaterial.Rows[i].Cells["BigSSCode"].Value.ToString(), ultraGridMaterial.Rows[i].Cells["MoCode"].Value.ToString());
                        }

                    }
                }

                this.DataProvider.CommitTransaction();
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_RecevieMaterial_Success"));
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();

                Messages msg = new Messages();
                msg.Add(new UserControl.Message(ex));
                ApplicationRun.GetInfoForm().Add(msg);
            }

            this.btnQuery_Click(sender, e);
        }

        #endregion

        #region 自定义事件

        private void BigSSCodeSelector_BigSSCodeSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.txtBigSSCode.Value = e.CustomObject;
        }

        //检查是否选择数据
        private bool HaveSelectedGrid()
        {
            if (ultraGridMaterial.Rows.Count <= 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_GRID_NO_RECORD"));
                return false;
            }

            for (int i = 0; i < ultraGridMaterial.Rows.Count; i++)
            {
                if (ultraGridMaterial.Rows[i].Cells[0].Value.ToString().ToLower()=="true")
                {
                    return true;
                }
            }

            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Please_Take_OneDate"));
            return false;
        }

        private bool GridCheckd()
        {          

            for (int i = 0; i < ultraGridMaterial.Rows.Count; i++)
            {
                if (ultraGridMaterial.Rows[i].Cells[0].Value.ToString().ToLower() == "true")
                {
                    if (string.IsNullOrEmpty(ultraGridMaterial.Rows[i].Cells["RecevieQTY"].Value.ToString()))
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ReceiveQty_Not_Empty"));
                        return false;
                    }


                    if (int.Parse(ultraGridMaterial.Rows[i].Cells["IssueQTY"].Value.ToString()) < int.Parse(ultraGridMaterial.Rows[i].Cells["RecevieQTY"].Value.ToString()))
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ReceiveQty_Must_Smaller_IssueQty"));
                        return false;
                    }

                    if (int.Parse(ultraGridMaterial.Rows[i].Cells["RecevieQTY"].Value.ToString()) < 1)
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS__ReceiveQty_Must_Over_Zorre"));
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion


    }
}