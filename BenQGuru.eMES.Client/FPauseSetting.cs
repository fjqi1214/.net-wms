using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Win.UltraWinGrid;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Warehouse;
using UserControl;
using BenQGuru.eMES.DataCollect;

namespace BenQGuru.eMES.Client
{
    public partial class FPauseSetting : BaseForm
    {
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;

        private DataSet m_dsPauseInfo;
        private DataTable m_dtHead;
        private DataTable m_dtDetail;
        private DataTable m_dtPalletDetail;

        private IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public FPauseSetting()
        {
            InitializeComponent();
        }

        private void FPauseSetting_Load(object sender, EventArgs e)
        {
            UserControl.UIStyleBuilder.FormUI(this);
            UserControl.UIStyleBuilder.GridUI(this.gridInfo);
            
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            this.inINVdateFrom.Value = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);
            this.inINVDateTo.Value = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

            this.InitialDataSet();
            //this.InitPageLanguage();
        }

        private void InitialDataSet()
        {
            this.m_dsPauseInfo = new DataSet();

            this.m_dtHead = new DataTable();
            this.m_dtHead.Columns.Add("checked");
            this.m_dtHead.Columns.Add("itemcode");
            this.m_dtHead.Columns.Add("itemdesc");
            this.m_dtHead.Columns.Add("bom");
            this.m_dtHead.Columns.Add("invqty");
            this.m_dtHead.TableName = "Head";

            this.m_dtDetail = new DataTable();
            this.m_dtDetail.Columns.Add("checked");
            this.m_dtDetail.Columns.Add("itemcode");
            this.m_dtDetail.Columns.Add("palletcode");
            this.m_dtDetail.Columns.Add("bom");
            this.m_dtDetail.Columns.Add("palletqty");
            this.m_dtDetail.TableName = "Detail";

            this.m_dtPalletDetail = new DataTable();
            this.m_dtPalletDetail.Columns.Add("checked");
            this.m_dtPalletDetail.Columns.Add("itemcode");
            this.m_dtPalletDetail.Columns.Add("bigsscode");
            this.m_dtPalletDetail.Columns.Add("rcard");
            this.m_dtPalletDetail.Columns.Add("palletcode");
            this.m_dtPalletDetail.Columns.Add("mocode");
            this.m_dtPalletDetail.Columns.Add("finishdate");
            this.m_dtPalletDetail.Columns.Add("ininvdate");
            this.m_dtPalletDetail.Columns.Add("bom");
            this.m_dtPalletDetail.TableName = "PalletDetail";

            this.m_dsPauseInfo.Tables.Add(this.m_dtHead);
            this.m_dsPauseInfo.Tables.Add(this.m_dtDetail);
            this.m_dsPauseInfo.Tables.Add(this.m_dtPalletDetail);


            DataColumn[] dataColumnHead = new DataColumn[2];
            dataColumnHead[0] = this.m_dsPauseInfo.Tables["Head"].Columns["itemcode"];
            dataColumnHead[1] = this.m_dsPauseInfo.Tables["Head"].Columns["bom"];

            DataColumn[] dataColumnDetail = new DataColumn[2];
            dataColumnDetail[0] = this.m_dsPauseInfo.Tables["Detail"].Columns["itemcode"];
            dataColumnDetail[1] = this.m_dsPauseInfo.Tables["Detail"].Columns["bom"];

            this.m_dsPauseInfo.Relations.Add("PauseSetting", dataColumnHead, dataColumnDetail, false);

            DataColumn[] dataColumnDetailHead = new DataColumn[3];
            dataColumnDetailHead[0] = this.m_dsPauseInfo.Tables["Detail"].Columns["itemcode"];
            dataColumnDetailHead[1] = this.m_dsPauseInfo.Tables["Detail"].Columns["bom"];
            dataColumnDetailHead[2] = this.m_dsPauseInfo.Tables["Detail"].Columns["palletcode"];

            DataColumn[] dataColumnPalletDetail = new DataColumn[3];
            dataColumnPalletDetail[0] = this.m_dsPauseInfo.Tables["PalletDetail"].Columns["itemcode"];
            dataColumnPalletDetail[1] = this.m_dsPauseInfo.Tables["PalletDetail"].Columns["bom"];
            dataColumnPalletDetail[2] = this.m_dsPauseInfo.Tables["PalletDetail"].Columns["palletcode"];

            this.m_dsPauseInfo.Relations.Add("PauseSettingForPallet", dataColumnDetailHead, dataColumnPalletDetail, false);


            this.m_dsPauseInfo.AcceptChanges();
            this.gridInfo.DataSource = this.m_dsPauseInfo;
        }


        private void gridInfo_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            //e.Layout.Override.AllowColSizing = AllowColSizing.None;
            e.Layout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            e.Layout.Override.CellClickAction = CellClickAction.Edit;

            // 自动判断是否显示前面的+、-号
            e.Layout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;


            // 排序
            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            e.Layout.Bands[0].Columns["checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["checked"].Width = 100;
            e.Layout.Bands[0].Columns["checked"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
            e.Layout.Bands[0].Columns["checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;

            e.Layout.Bands[0].Columns["itemcode"].Header.Caption = "产品代码";
            e.Layout.Bands[0].Columns["itemcode"].Width = 150;
            e.Layout.Bands[0].Columns["itemcode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["itemdesc"].Header.Caption = "产品描述";
            e.Layout.Bands[0].Columns["itemdesc"].Width = 150;
            e.Layout.Bands[0].Columns["itemdesc"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["bom"].Header.Caption = "BOM版本";
            e.Layout.Bands[0].Columns["bom"].Width = 100;
            e.Layout.Bands[0].Columns["bom"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["invqty"].Header.Caption = "在库数量(非停发)";
            e.Layout.Bands[0].Columns["invqty"].Width = 150;
            e.Layout.Bands[0].Columns["invqty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;


           
            e.Layout.Bands[1].Columns["checked"].Header.Caption = "";
            e.Layout.Bands[1].Columns["checked"].Width = 100;
            e.Layout.Bands[1].Columns["checked"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
            e.Layout.Bands[1].Columns["checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;

            e.Layout.Bands[1].Columns["itemcode"].Hidden = true;

            e.Layout.Bands[1].Columns["palletcode"].Header.Caption = "栈板号";
            e.Layout.Bands[1].Columns["palletcode"].Width = 150;
            e.Layout.Bands[1].Columns["palletcode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[1].Columns["palletqty"].Header.Caption = "栈板中的数量";
            e.Layout.Bands[1].Columns["palletqty"].Width = 150;
            e.Layout.Bands[1].Columns["palletqty"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[1].Columns["bom"].Hidden = true;
        

            e.Layout.Bands[2].Columns["checked"].Header.Caption = "";
            e.Layout.Bands[2].Columns["checked"].Width = 100;
            e.Layout.Bands[2].Columns["checked"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
            e.Layout.Bands[2].Columns["checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;

            e.Layout.Bands[2].Columns["itemcode"].Hidden = true;

            e.Layout.Bands[2].Columns["bigsscode"].Header.Caption = "大线";
            e.Layout.Bands[2].Columns["bigsscode"].Width = 150;
            e.Layout.Bands[2].Columns["bigsscode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[2].Columns["rcard"].Header.Caption = "产品序列号";
            e.Layout.Bands[2].Columns["rcard"].Width = 150;
            e.Layout.Bands[2].Columns["rcard"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[2].Columns["palletcode"].Hidden = true;

            e.Layout.Bands[2].Columns["mocode"].Header.Caption = "工单代码";
            e.Layout.Bands[2].Columns["mocode"].Width = 150;
            e.Layout.Bands[2].Columns["mocode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[2].Columns["finishdate"].Header.Caption = "已完工日期";
            e.Layout.Bands[2].Columns["finishdate"].Width = 150;
            e.Layout.Bands[2].Columns["finishdate"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[2].Columns["ininvdate"].Header.Caption = "入库日期";
            e.Layout.Bands[2].Columns["ininvdate"].Width = 150;
            e.Layout.Bands[2].Columns["ininvdate"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[2].Columns["bom"].Hidden = true;

            //this.InitGridLanguage(gridInfo);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            this.txtModelCode.Value = "";
            this.txtBOM.Value = "";
            this.txtItemDesc.Value = "";
            this.inINVdateFrom.Value = FormatHelper.ToDateTime(dbDateTime.DBDate,dbDateTime.DBTime);
            this.inINVDateTo.Value = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);
            this.txtRcardFrom.Value = "";
            this.txtRcardTo.Value = "";
            this.chkIsFinished.Checked = false;
            this.m_dsPauseInfo.Clear();
            this.txtPauseCode.Value = "";
            this.txtPauseReason.Value = "";
        }

        private void btnQuiry_Click(object sender, EventArgs e)
        {
            ////检查查询必输项
            //
            if (!this.CheckUI())
            {
                return;
            }

            this.m_dsPauseInfo.Clear();
            this.m_dtHead.Clear();
            this.m_dtDetail.Clear();
            
            PauseFacade objFacade = new PauseFacade(this.DataProvider);

            DataCollectFacade dcf = new DataCollectFacade(DataProvider);
            string sourceRCardStart = dcf.GetSourceCard(this.txtRcardFrom.Value.Trim().ToUpper(), string.Empty);
            string sourceRCardEnd = dcf.GetSourceCard(this.txtRcardTo.Value.Trim().ToUpper(), string.Empty);

            ////Query Grid Head
            //
            object[] objHeadList = objFacade.GetPauseInfoFromHead(FormatHelper.CleanString(this.txtModelCode.Value.Trim()),
                                                                   FormatHelper.CleanString(this.txtBOM.Value.Trim()),
                                                                   FormatHelper.CleanString(this.txtItemDesc.Value.Trim()),
                                                                   this.inINVdateFrom.Value.ToString() == null ? "" : FormatHelper.TODateInt(this.inINVdateFrom.Value).ToString(),
                                                                   this.inINVDateTo.Value.ToString() == null ? "" : FormatHelper.TODateInt(this.inINVDateTo.Value).ToString(),
                                                                   FormatHelper.CleanString(sourceRCardStart),
                                                                   FormatHelper.CleanString(sourceRCardEnd),
                                                                   this.chkIsFinished.Checked,
                                                                   FormatHelper.CleanString(this.txtBigSSCode.Value.Trim()));

            if (objHeadList == null)
            {
                ApplicationRun.GetInfoForm().Add(
                   new UserControl.Message(MessageType.Error, "$CS_No_Data_To_Display"));
                return;
            }

            DataRow dr;

            foreach (PauseSetting obj in objHeadList)
            {
                dr = this.m_dtHead.NewRow();
                dr["checked"] = "false";
                dr["itemcode"] = obj.ItemCode;
                dr["itemdesc"] = obj.ItemDescription;
                dr["bom"] = obj.BOM;
                dr["invqty"] = obj.qty;

                this.m_dtHead.Rows.Add(dr);
            }

            ////Query Grid Detail
            //
            object[] objDetailList = objFacade.GetPauseInfoFromDetail(FormatHelper.CleanString(this.txtModelCode.Value.Trim()),
                                                                       FormatHelper.CleanString(this.txtBOM.Value.Trim()),
                                                                       FormatHelper.CleanString(this.txtItemDesc.Value.Trim()),
                                                                       this.inINVdateFrom.Value.ToString() == null ? "" : FormatHelper.TODateInt(this.inINVdateFrom.Value).ToString(),
                                                                       this.inINVDateTo.Value.ToString() == null ? "" : FormatHelper.TODateInt(this.inINVDateTo.Value).ToString(),
                                                                       FormatHelper.CleanString(sourceRCardStart),
                                                                       FormatHelper.CleanString(sourceRCardEnd),
                                                                       this.chkIsFinished.Checked,
                                                                       FormatHelper.CleanString(this.txtBigSSCode.Value.Trim()));

            foreach (PauseSetting obj in objDetailList)
            {
                dr = this.m_dtDetail.NewRow();
                dr["checked"] = "false";
                dr["itemcode"] = obj.ItemCode;
                dr["palletcode"] = obj.PalletCode;
                dr["palletqty"] = obj.qty;
                dr["bom"] = obj.BOM;

                this.m_dtDetail.Rows.Add(dr);
            }

            ////Query Grid PalletDetail
            //
            object[] objPalletDetailList = objFacade.GetPauseInfoFromPalletDetail(FormatHelper.CleanString(this.txtModelCode.Value.Trim()),
                                                                      FormatHelper.CleanString(this.txtBOM.Value.Trim()),
                                                                      FormatHelper.CleanString(this.txtItemDesc.Value.Trim()),
                                                                      this.inINVdateFrom.Value.ToString() == null ? "" : FormatHelper.TODateInt(this.inINVdateFrom.Value).ToString(),
                                                                      this.inINVDateTo.Value.ToString() == null ? "" : FormatHelper.TODateInt(this.inINVDateTo.Value).ToString(),
                                                                      FormatHelper.CleanString(sourceRCardStart),
                                                                      FormatHelper.CleanString(sourceRCardEnd),
                                                                      this.chkIsFinished.Checked,
                                                                      FormatHelper.CleanString(this.txtBigSSCode.Value.Trim()));


            foreach (PauseSetting obj in objPalletDetailList)
            {
                dr = this.m_dtPalletDetail.NewRow();
                dr["checked"] = "false";
                dr["itemcode"] = obj.ItemCode;
                dr["bigsscode"] = obj.BigSSCode;
                dr["rcard"] = obj.Rcard;
                dr["palletcode"] = obj.PalletCode;
                dr["mocode"] = obj.MOCode;
                dr["finishdate"] = FormatHelper.ToDateString(obj.FinishedDate, "/");
                dr["ininvdate"] = FormatHelper.ToDateString(obj.InInvDate, "/");
                dr["bom"] = obj.BOM;

                this.m_dtPalletDetail.Rows.Add(dr);
            }
        }

        private bool CheckUI()
        {
            if (this.txtModelCode.Value.Trim().Length == 0 &&
                this.txtBOM.Value.Trim().Length == 0 &&
                this.txtItemDesc.Value.Trim().Length == 0 &&
                this.inINVdateFrom.Value == null &&
                this.inINVDateTo.Value == null &&
                this.txtRcardFrom.Value.Trim().Length == 0 &&
                this.txtRcardTo.Value.Trim().Length == 0 &&
                this.chkIsFinished.Checked == false
                )
            {
                //Message:请输入查询条件
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_INPUT_QUERY_CONDITION"));
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.gridInfo.Rows.Count == 0)
            {
                //表格中没有数据
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_GRID_NO_RECORD"));
                return;
            }

            ////检查是否有选中的数据
            //
            bool blnIsSelected = false;
            for (int i = 0; i < this.gridInfo.Rows.Count; i++)
            {
                if (this.gridInfo.Rows[i].Cells["checked"].Value.ToString().ToLower().Equals("true"))
                {
                    blnIsSelected = true;
                    break;
                }

                for (int j = 0; j < this.gridInfo.Rows[i].ChildBands[0].Rows.Count; j++)
                {
                    if (this.gridInfo.Rows[i].ChildBands[0].Rows[j].Cells["checked"].Value.ToString().ToLower().Equals("true"))
                    {
                        blnIsSelected = true;
                        break;
                    }
                    else
                    {
                        for (int m = 0; m < this.gridInfo.Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows.Count; m++)
                        {
                            if (this.gridInfo.Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[m].Cells["checked"].Value.ToString().ToLower().Equals("true"))
                            {
                                blnIsSelected = true;
                                break;
                            }
                        }
                    }
                }
            }

            if (!blnIsSelected)
            {
                //Message:请至少选中一笔记录
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_CHOOSE_ONE_RECORD_AT_LEAST"));
                return;
            }

            
            if (this.txtPauseCode.Value.Trim().Length == 0)
            {
                //Message:请输入停发通知单
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_INPUT_PAUSE_CODE"));
                this.txtPauseCode.TextFocus(true, true);
                return;
            }

            string strPauseCode = FormatHelper.CleanString(this.txtPauseCode.Value.Trim());

            if (this.txtPauseReason.Value.Trim().Length == 0)
            {
                //Message:请输入停发原因
                ApplicationRun.GetInfoForm().Add(
                    new UserControl.Message(MessageType.Error, "$CS_INPUT_PAUSE_REASON"));
                this.txtPauseReason.TextFocus(true, true);
                return;
            }

            string strPauseReason = FormatHelper.CleanString(this.txtPauseReason.Value.Trim());

            PauseFacade objFacade = new PauseFacade(this.DataProvider);

            object objPause = objFacade.GetPause(strPauseCode);

            if (objPause != null)
            {
                if (((Pause)objPause).Status.Equals(PauseStatus.status_cancel))
                {
                    //Message:停发通知单已经关闭，请输入停发通知单
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_PAUSE_IN_CANCEL,$CS_INPUT_PAUSE_CODE"));
                    this.txtPauseCode.TextFocus(false, true);
                    return;
                }

                if (!((Pause)objPause).Status.Equals(PauseStatus.status_pause))
                {
                    //Message:停发通知单状态错误
                    ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Error, "$CS_PAUSE_STATUS_ERROR " + ((Pause)objPause).Status));
                    this.txtPauseCode.TextFocus(false, true);
                    return;
                }
            }



            ////Message:确定把所选择的序列号加入该停发通知单内?
            //
            if (MessageBox.Show(UserControl.MutiLanguages.ParserMessage("$CS_ADD_RCARD_TO_PAUSE")+"?",UserControl.MutiLanguages.ParserMessage("$CS_CONFIRM"),MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
                
                Pause pause = new Pause();
                pause.PauseCode = strPauseCode;
                pause.PauseReason = strPauseReason;
                pause.CancelReason = " ";
                pause.Status = PauseStatus.status_pause;
                pause.PUser = ApplicationService.Current().LoginInfo.UserCode;
                pause.PDate = dbDateTime.DBDate;
                pause.PTime = dbDateTime.DBTime;
                pause.CancelUser = " ";
                //pause.CancelDate = dbDateTime.DBDate;
                //pause.CancelTime = dbDateTime.DBTime;
                pause.MaintainUser = ApplicationService.Current().LoginInfo.UserCode;
                pause.MaintainDate = dbDateTime.DBDate;
                pause.MaintainTime = dbDateTime.DBTime;

                IList<Pause2Rcard> pause2RcardList = new List<Pause2Rcard>();

                for (int i = 0; i < this.gridInfo.Rows.Count; i++)
                {
                    for (int j = 0; j < this.gridInfo.Rows[i].ChildBands[0].Rows.Count; j++)
                    {
                        for (int m = 0; m < this.gridInfo.Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows.Count; m++)
                        {
                            if (this.gridInfo.Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[m].Cells["checked"].Value.ToString().ToLower().Equals("true"))
                            {
                                string rcard= this.gridInfo.Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[m].Cells["rcard"].Value.ToString();

                                if (objFacade.CheckExistPause2Rcard(strPauseCode, rcard))
                                {
                                    ////Message: 已经存在停发资料
                                    //
                                    ApplicationRun.GetInfoForm().Add(
                                    new UserControl.Message(MessageType.Error, "$CS_ALREADY_EXIST_PAUSE2RCARD $CS_Param_RunSeq=" + rcard));
                                    return;
                                }


                                Pause2Rcard objPause2Rcard = new Pause2Rcard();
                                objPause2Rcard.PauseCode = strPauseCode;
                                objPause2Rcard.SerialNo = rcard;
                                objPause2Rcard.ItemCode = this.gridInfo.Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[m].Cells["itemcode"].Value.ToString();
                                objPause2Rcard.CancelSeq = " ";
                                objPause2Rcard.BOM = this.gridInfo.Rows[i].Cells["bom"].Value.ToString();
                                objPause2Rcard.MOCode = this.gridInfo.Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[m].Cells["mocode"].Value.ToString();
                                objPause2Rcard.Status = PauseStatus.status_pause;
                                objPause2Rcard.CancelReason = " ";
                                objPause2Rcard.PUser = ApplicationService.Current().LoginInfo.UserCode;
                                objPause2Rcard.PDate = dbDateTime.DBDate;
                                objPause2Rcard.PTime = dbDateTime.DBTime;
                                objPause2Rcard.CancelUser = " ";
                                objPause2Rcard.MaintainUser = ApplicationService.Current().LoginInfo.UserCode;
                                objPause2Rcard.MaintainDate = dbDateTime.DBDate;
                                objPause2Rcard.MaintainTime = dbDateTime.DBTime;

                                pause2RcardList.Add(objPause2Rcard);
                            }
                        }
                    }
                }

                objFacade.PauseRcard(pause, pause2RcardList);

                ApplicationRun.GetInfoForm().Add(
                        new UserControl.Message(MessageType.Success, "$CS_Save_Success"));

                this.m_dsPauseInfo.Clear();
                this.m_dtDetail.Clear();
                this.m_dtHead.Clear();
                this.txtPauseReason.Value = "";
                this.txtPauseCode.TextFocus(true, true);
            }
            
        }


        private void gridInfo_CellChange(object sender, CellEventArgs e)
        {
            this.gridInfo.UpdateData();
            if (this.gridInfo.ActiveCell != null && this.gridInfo.ActiveCell.Row.ChildBands!=null)
            {
                if (e.Cell.Column.Key == "checked")
                {

                    if (this.gridInfo.ActiveCell.Row.Cells["checked"].Value.ToString().ToLower().Equals("true"))
                    {
                        for (int i = 0; i < this.gridInfo.ActiveCell.Row.ChildBands[0].Rows.Count; i++)
                        {
                            this.gridInfo.ActiveCell.Row.ChildBands[0].Rows[i].Cells["checked"].Value = "true";

                            if (this.gridInfo.ActiveCell.Row.ChildBands[0].Rows[i].ChildBands!=null)
                            {
                                for (int j = 0; j < this.gridInfo.ActiveCell.Row.ChildBands[0].Rows[i].ChildBands[0].Rows.Count; j++)
                                {
                                    this.gridInfo.ActiveCell.Row.ChildBands[0].Rows[i].ChildBands[0].Rows[j].Cells["checked"].Value = "true";
                                }
                            }
                           
                        }
                    }
                    else
                    {
                        for (int i = 0; i < this.gridInfo.ActiveCell.Row.ChildBands[0].Rows.Count; i++)
                        {
                            this.gridInfo.ActiveCell.Row.ChildBands[0].Rows[i].Cells["checked"].Value = "false";
                            if (this.gridInfo.ActiveCell.Row.ChildBands[0].Rows[i].ChildBands != null)
                            {
                                for (int j = 0; j < this.gridInfo.ActiveCell.Row.ChildBands[0].Rows[i].ChildBands[0].Rows.Count; j++)
                                {
                                    this.gridInfo.ActiveCell.Row.ChildBands[0].Rows[i].ChildBands[0].Rows[j].Cells["checked"].Value = "false";
                                }
                            }
                        }
                    }
                }
            }
        }

        private void txtPauseCode_Leave(object sender, EventArgs e)
        {
            PauseFacade objFacade = new PauseFacade(this.DataProvider);

            if (this.txtPauseCode.Value.Trim().Length != 0)
            {
                object objPause = objFacade.GetPause(FormatHelper.CleanString(this.txtPauseCode.Value.Trim()));
                if (objPause != null)
                {
                    this.txtPauseReason.Value = ((Pause)objPause).PauseReason;
                    this.txtPauseReason.ReadOnly = true;
                }
                else
                {
                    this.txtPauseReason.ReadOnly = false;
                }
            }
            else
            {
                this.txtPauseReason.ReadOnly = false;
            }
        }

    }
}