using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Drawing;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;

using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using System.Collections.Generic;
using System.Windows.Forms;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.MOModel;
using Infragistics.Win.UltraWinGrid;
using BenQGuru.eMES.Client.Service;
using UserControl;

namespace BenQGuru.eMES.Client
{
    public partial class FMSDAlter : BaseForm
    {
        DataTable dt = new DataTable();
        InventoryFacade _facade = null;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public FMSDAlter()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);
        }

        #region FMSDALERT_Load
        private void FMSDALERT_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("LotNO", typeof(string));
            dt.Columns.Add("MCode", typeof(string));
            dt.Columns.Add("MName", typeof(string));
            dt.Columns.Add("MDesc", typeof(string));
            dt.Columns.Add("Floorlife", typeof(string));
            dt.Columns.Add("INDryingTime", typeof(string));
            dt.Columns.Add("LastTime", typeof(string));
            dt.Columns.Add("OverFloorlife", typeof(string));
            dt.Columns.Add("MaintainUser", typeof(string));
            dt.Columns.Add("MaintainDate", typeof(string));
            dt.Columns.Add("MaintainTime", typeof(string));

            this.ultraGridMSDAlert.DataSource = dt;
            this.ultraGridMSDAlert.DataBind();
            //this.InitPageLanguage();
        }
        #endregion

        #region ultraGridMSDAlert_InitializeLayout
        private void ultraGridMSDAlert_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            e.Layout.Appearance.BackColor = System.Drawing.Color.White; ;
            e.Layout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            e.Layout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            e.Layout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            e.Layout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            e.Layout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            e.Layout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            //e.Layout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.SlateGray;
            //e.Layout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            // e.Layout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");

            //this.ultraGridMSDAlert.DisplayLayout.Override.SelectTypeRow = SelectType.Single;//单行
            //this.ultraGridMSDAlert.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;//行选择
            //this.ultraGridMSDAlert.DisplayLayout.Override.SelectedRowAppearance.BackColor = Color.Blue;//选中后的背景颜色

            //this.ultraGridMSDAlert.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            //this.ultraGridMSDAlert.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.VisibleIndex;
            //this.ultraGridMSDAlert.DisplayLayout.Override.RowSelectorWidth = 30;

            e.Layout.Bands[0].Columns["LotNO"].Header.Caption = "物料批号";
            e.Layout.Bands[0].Columns["MCode"].Header.Caption = "物料代码";
            e.Layout.Bands[0].Columns["MName"].Header.Caption = "物料名称";
            e.Layout.Bands[0].Columns["MDesc"].Header.Caption = "物料描述";
            e.Layout.Bands[0].Columns["Floorlife"].Header.Caption = "有效车间寿命";
            e.Layout.Bands[0].Columns["INDryingTime"].Header.Caption = "最大暴露时间";
            e.Layout.Bands[0].Columns["LastTime"].Header.Caption = "拆封未使用时间";
            e.Layout.Bands[0].Columns["OverFloorlife"].Header.Caption = "剩余车间寿命";
            e.Layout.Bands[0].Columns["MaintainUser"].Header.Caption = "维护人员";
            e.Layout.Bands[0].Columns["MaintainDate"].Header.Caption = "维护日期";
            e.Layout.Bands[0].Columns["MaintainTime"].Header.Caption = "维护时间";


            //e.Layout.Bands[0].Columns["mItemCode"].Width = 80;
            //e.Layout.Bands[0].Columns["mName"].Width = 80;
            //e.Layout.Bands[0].Columns["mDesc"].Width = 80;
            //e.Layout.Bands[0].Columns["SourceWh"].Width = 80;
            //e.Layout.Bands[0].Columns["PlanQty"].Width = 45;
            //e.Layout.Bands[0].Columns["Actqty"].Width = 45;
            //e.Layout.Bands[0].Columns["WaitQty"].Width = 45;


            e.Layout.Bands[0].Columns["LotNO"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["MCode"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["MName"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["MDesc"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["Floorlife"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["INDryingTime"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["LastTime"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["OverFloorlife"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["MaintainUser"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["MaintainDate"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["MaintainTime"].CellActivation = Activation.NoEdit;
            //this.InitGridLanguage(ultraGridMSDAlert);
        }
        #endregion

        #region btnSearch_Click
        private void btnSearch_Click(object sender, EventArgs e)
        {
            dt.Rows.Clear();

            if ((txtAlertFloorLife.Value == string.Empty) || (txtAlertFloorLife.Value == "0"))
            {
                this.ErrorMessage("$CS_InPut_FloorLife_Over_Zero");
                return;
            }
            if ((txtAlterTime.Value == string.Empty) || (txtAlterTime.Value == "0"))
            {
                this.ErrorMessage("$CS_InPut_AlterTime_Over_Zero");
                return;
            }

            if (_facade == null)
            {
                _facade = new InventoryFacade(this.DataProvider);
            }

            DataTable dtInfo = _facade.QueryMSDALERT();

            if (dtInfo == null)
            {
                this.ErrorMessage("$CS_MSDAlert_Is_NODATA");
                return;
            }

            foreach (DataRow dr in dtInfo.Rows)
            {
                DataRow drNew = dt.NewRow();
                drNew["LotNO"] = dr["LotNO"];
                drNew["MCode"] = dr["MCode"];
                drNew["MName"] = dr["MName"];
                drNew["MDesc"] = dr["MDesc"];
                drNew["Floorlife"] = Math.Round(Convert.ToDouble(dr["Floorlife"]), 1).ToString();
                drNew["INDryingTime"] = dr["INDryingTime"];
                drNew["LastTime"] = dr["LastTime"]; ;
                drNew["OverFloorlife"] = dr["OverFloorlife"];
                drNew["MaintainUser"] = dr["MUser"];
                drNew["MaintainDate"] = FormatHelper.ToDateString(decimal.Parse(dr["MDate"].ToString()));
                drNew["MaintainTime"] = FormatHelper.ToTimeString(int.Parse(dr["MTime"].ToString()));

                this.dt.Rows.Add(drNew);
            }


        }
        #endregion

        #region Message
        protected void ErrorMessage(string msg)
        {
            ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error, msg));
        }

        protected void SuccessMessage(string msg)
        {
            ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Success, msg));
        }
        #endregion

        #region ultraGridMSDAlert_InitializeRow
        private void ultraGridMSDAlert_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            bool blnOverTime = false;
            string status = string.Empty;

            //查询TBLMSDLOT表状态
            InventoryFacade _facade = new InventoryFacade(this.DataProvider);
            object obj = _facade.GetMSDLot(e.Row.Cells["LotNO"].Value.ToString());
            if (obj != null)
            {
                status = ((MSDLOT)obj).Status;
            }
            if (status == "MSD_OVERTIME")
            {
                e.Row.Appearance.BackColor = Color.Red;
                return;
            }

            //if ((decimal.Parse(e.Row.Cells["INDryingTime"].Value.ToString()) - decimal.Parse(e.Row.Cells["LastTime"].Value.ToString())) <= 0)
            if (decimal.Parse(e.Row.Cells["OverFloorlife"].Value.ToString()) <= 0)
            {
                e.Row.Appearance.BackColor = Color.Red;
                blnOverTime = true;
            }
            else if (decimal.Parse(e.Row.Cells["OverFloorlife"].Value.ToString()) <= decimal.Parse(txtAlertFloorLife.Value))
            {
                e.Row.Appearance.BackColor = Color.Yellow;
            }
            else if ((decimal.Parse(e.Row.Cells["INDryingTime"].Value.ToString()) - decimal.Parse(e.Row.Cells["LastTime"].Value.ToString())) <= decimal.Parse(txtAlterTime.Value))
            {
                e.Row.Appearance.BackColor = Color.Orange;
            }


            if (blnOverTime == true)
            {
                //查询TBLMSDLOT表状态
                //InventoryFacade _facade = new InventoryFacade(this.DataProvider);
                //object obj = _facade.GetMSDLot(e.Row.Cells["LotNO"].Value.ToString());
                //if (obj != null)
                //{
                //    status = ((MSDLOT)obj).Status;
                //}
                //'MSD_OPENED','MSD_OVERTIME

               
                //else 
                if (status == "MSD_OPENED")
                {
                    //得到时间
                    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                    int date = dbDateTime.DBDate;
                    int time = dbDateTime.DBTime;

                    //更新TBLMSDLOT
                    MSDLOT msdLot = new MSDLOT();
                    msdLot.LotNo = e.Row.Cells["LotNO"].Value.ToString();
                    msdLot.Status = "MSD_OVERTIME";
                    msdLot.Floorlife = ((MSDLOT)obj).Floorlife;
                    msdLot.OverFloorlife = 0;
                    //msdLot.OverFloorlife = ((MSDLOT)obj).OverFloorlife;
                    msdLot.MaintainUser = ApplicationService.Current().UserCode;
                    msdLot.MaintainDate = date;
                    msdLot.MaintainTime = time;

                    _facade.UpdateMSDLOT(msdLot);

                    //添加TBLMSDWIP
                    MSDWIP msdwip = new MSDWIP();
                    msdwip.serial = 0;
                    msdwip.LotNo = e.Row.Cells["LotNO"].Value.ToString();
                    msdwip.Status = "MSD_OVERTIME";
                    msdwip.MaintainUser = ApplicationService.Current().UserCode;
                    msdwip.MaintainDate = date;
                    msdwip.MaintainTime = time;

                    _facade.AddMSDWIP(msdwip);
                }
            }
        }
        #endregion

        #region chkAutoRefresh_CheckedChanged
        private void chkAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoRefresh.Checked == true)
            {
                if ((txtRefreshRateV.Value == string.Empty) || (txtRefreshRateV.Value == "0"))
                {
                    this.ErrorMessage("$CS_InPut_Refresh_Over_Zero");
                    return;
                }

                timer1.Interval = int.Parse(txtRefreshRateV.Value) * 60000;
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
            }
        }
        #endregion

        #region timer1_Tick
        private void timer1_Tick(object sender, EventArgs e)
        {
            btnSearch_Click(sender, e);
        }
        #endregion

        private void txtRefreshRate_InnerTextChanged(object sender, EventArgs e)
        {
            if (this.txtRefreshRateV.Value.Trim() == "0")
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_Timer_Must_Bigger_Than_Zero"));
                return;
            }
            if (this.txtRefreshRateV.Value.Trim() != "")
            {
                timer1.Interval = (int)(double.Parse(this.txtRefreshRateV.Value) * 60 * 1000);
            }
        }
    }
}
