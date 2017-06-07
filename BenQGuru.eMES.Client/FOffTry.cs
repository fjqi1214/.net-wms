using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using UserControl;

namespace BenQGuru.eMES.Client
{
    public partial class FOffTry : BaseForm
    {

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        private string sourceRCard = string.Empty;

        private const string Carton = "0";
        private const string Rcard = "1";
        //产品序列号(根据输入的Carton和Rcard获取)
        private string _InputRcard = string.Empty;
        UltraWinGridHelper ultraWinGridHelper;
        DataTable dtCheckTry = new DataTable();
        TryFacade _TryFacade = null;


        public FOffTry()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);

            ultraGridTry.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            ultraGridTry.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridTry.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridTry.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridTry.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            ultraGridTry.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridTry.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            ultraGridTry.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            ultraGridTry.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            ultraGridTry.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void ultraGridTry_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            ultraWinGridHelper = new UltraWinGridHelper(this.ultraGridTry);

            ultraWinGridHelper.AddCheckColumn("Checked", "");
            ultraWinGridHelper.AddReadOnlyColumn("TryCode", "试流单号");
            ultraWinGridHelper.AddReadOnlyColumn("PlanQty", "计划数量");
            ultraWinGridHelper.AddReadOnlyColumn("ActualQty", "实际数量");
            ultraWinGridHelper.AddReadOnlyColumn("Memo", "备注");
        }


        private void opsetOffTry_ValueChanged(object sender, EventArgs e)
        {
            if (this.opsetOffTry.Value.ToString() == Carton)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_PLEASE_INPUT_CARTONNO"));
                this.txtInPutSN.TextFocus(true, true);
            }
            else
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"));
                this.txtInPutSN.TextFocus(true, true);
            }
        }

        private void txtRCard_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.QueryDataAndBind();
            }
        }

        private void FOffTry_Load(object sender, EventArgs e)
        {
            InitializeUltraGrid();
            this.opsetOffTry.Value = Rcard;
            this.txtCollected.Value = "0";
            this.txtInPutSN.TextFocus(true, true);

            //this.InitPageLanguage();
            //this.InitGridLanguage(this.ultraGridTry);
        }

        private void InitializeUltraGrid()
        {
            dtCheckTry.Columns.Clear();

            dtCheckTry.Columns.Add("Checked", typeof(bool));
            dtCheckTry.Columns.Add("TryCode", typeof(string));
            dtCheckTry.Columns.Add("PlanQty", typeof(int));
            dtCheckTry.Columns.Add("ActualQty", typeof(int));
            dtCheckTry.Columns.Add("Memo", typeof(string));

            dtCheckTry.Columns[0].ReadOnly = false;
            dtCheckTry.Columns[1].ReadOnly = true;
            dtCheckTry.Columns[2].ReadOnly = true;
            dtCheckTry.Columns[3].ReadOnly = true;
            dtCheckTry.Columns[4].ReadOnly = true;

            this.ultraGridTry.DataSource = dtCheckTry;

            dtCheckTry.Clear();

            ultraGridTry.DisplayLayout.Bands[0].Columns[0].Width = 40;
            ultraGridTry.DisplayLayout.Bands[0].Columns[1].Width = 200;
            ultraGridTry.DisplayLayout.Bands[0].Columns[2].Width = 100;
            ultraGridTry.DisplayLayout.Bands[0].Columns[3].Width = 100;
            ultraGridTry.DisplayLayout.Bands[0].Columns[4].Width = 300;
        }


        private void BindGird(object[] TryList)
        {
            dtCheckTry.Rows.Clear();
            foreach (Try NewTry in TryList)
            {
                dtCheckTry.Rows.Add(new object[] { false, NewTry.TryCode, NewTry.PlanQty, NewTry.ActualQty, NewTry.Memo });
            }
            this.dtCheckTry.AcceptChanges();
        }

        //更新Grid
        private void UpdateGrid()
        {
            if (_TryFacade == null)
            {
                this._TryFacade = new TryFacade(this.DataProvider);
            }

            object[] TryListByCarton = this._TryFacade.QueryTryByRcard(_InputRcard);
            if (TryListByCarton != null)
            {

                this.BindGird(TryListByCarton);
            }
            else
            {
                dtCheckTry.Rows.Clear();
                this.dtCheckTry.AcceptChanges();
            }
        }

        //查询并Load数据
        private void QueryDataAndBind()
        {
            if (_TryFacade == null)
            {
                this._TryFacade = new TryFacade(this.DataProvider);
            }
            switch (this.opsetOffTry.Value.ToString())
            {
                case Carton:
                    if (string.IsNullOrEmpty(this.txtInPutSN.Value))
                    {
                        this.txtInPutSN.TextFocus(false, true);
                        return;
                    }

                    DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                    object objectSimulationReport = dataCollectFacade.GetRCardByCartonCode(this.txtInPutSN.Value.Trim().ToUpper());
                    if (objectSimulationReport == null)
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_ProductInfo_IS_Null"));
                        this.txtInPutSN.TextFocus(false, true);
                        return;
                    }

                    _InputRcard = ((SimulationReport)objectSimulationReport).RunningCard;

                    object[] TryListByCarton = this._TryFacade.QueryTryByRcard(_InputRcard);
                    if (TryListByCarton != null)
                    {

                        this.BindGird(TryListByCarton);
                    }
                    else
                    {
                        dtCheckTry.Rows.Clear();
                        this.dtCheckTry.AcceptChanges();
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_RCARD_HAVE_NO_TRY"));
                        this.txtInPutSN.TextFocus(false, true);
                        return;
                    }

                    break;
                case Rcard:
                    if (string.IsNullOrEmpty(this.txtInPutSN.Value))
                    {
                        this.txtInPutSN.TextFocus(false, true);
                        return;
                    }

                    _InputRcard = this.txtInPutSN.Value.Trim().ToUpper();

                    //根据当前序列号获取对应的原始的序列号
                    dataCollectFacade = new DataCollectFacade(this.DataProvider);
                    sourceRCard = dataCollectFacade.GetSourceCard(_InputRcard.Trim().ToUpper(), string.Empty);

                    object[] TryListByRcard = this._TryFacade.QueryTryByRcard(FormatHelper.CleanString(sourceRCard.Trim().ToUpper()));

                    if (TryListByRcard != null)
                    {
                        this.BindGird(TryListByRcard);
                    }
                    else
                    {
                        dtCheckTry.Rows.Clear();
                        this.dtCheckTry.AcceptChanges();
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_RCARD_HAVE_NO_TRY"));
                        this.txtInPutSN.TextFocus(false, true);
                        return;
                    }

                    break;
            }
        }



        private void btnOffTry_Click(object sender, EventArgs e)
        {
            DBDateTime DBDateTimeNow = FormatHelper.GetNowDBDateTime(this.DataProvider);
            int OffTryNumber = 0;

            if (MessageBox.Show(MutiLanguages.ParserMessage("$Dissmis_Rcard_And_Try"), MutiLanguages.ParserMessage("$ShowMessage"), MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                this.DataProvider.BeginTransaction();
                try
                {
                    for (int i = 0; i < this.ultraGridTry.Rows.Count; i++)
                    {
                        if (ultraGridTry.Rows[i].Cells[0].Text.ToLower() == "true")
                        {
                            string TryCode = ultraGridTry.Rows[i].Cells[1].Text.ToUpper();

                            object objectTry = this._TryFacade.GetTry(TryCode);

                            if (objectTry != null)
                            {
                                ((Try)objectTry).ActualQty -= 1;
                                ((Try)objectTry).MaintainDate = DBDateTimeNow.DBDate;
                                ((Try)objectTry).MaintainTime = DBDateTimeNow.DBTime;
                                ((Try)objectTry).MaintainUser = ApplicationService.Current().UserCode;

                                this._TryFacade.UpdateTry(((Try)objectTry));
                                if (opsetOffTry.Value.ToString() == Rcard)
                                {
                                    this._TryFacade.DeleteTry2RCard(TryCode, sourceRCard.Trim());
                                }
                                else
                                {
                                    this._TryFacade.DeleteTry2RCard(TryCode, _InputRcard);
                                }

                                OffTryNumber += 1;
                            }
                        }
                    }
                    if (OffTryNumber == 0)
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ToCheck_OffTry"));
                    }
                    else
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_OffTry_Success"));
                        this.txtCollected.Value = Convert.ToString(OffTryNumber + Convert.ToInt32(this.txtCollected.Value.Trim()));
                        this.txtInPutSN.TextFocus(false, true);
                    }
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, ex.Message));
                }
                finally
                {
                    this.DataProvider.CommitTransaction();
                    this.UpdateGrid();
                }
            }

        }

        private void uBtnExit_Click(object sender, EventArgs e)
        {

        }
    }
}