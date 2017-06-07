using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Package;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using UserControl;


using Infragistics.Win.UltraWinGrid;

namespace BenQGuru.eMES.Client
{
    public partial class FCollectionCarton_SKD : Form
    {
        #region  变量

        private UltraWinGridHelper _UltraWinGridHelper1 = null;
        private IDomainDataProvider _DomainDataProvider = ApplicationService.Current().DataProvider;
        private DataTable _DataTableLoadedPart = new DataTable();
        private string _itemCode = string.Empty;

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

        public FCollectionCarton_SKD()
        {
            InitializeComponent();
        }

        private void FCollectionCarton_SKD_Load(object sender, EventArgs e)
        {
            InitializeUltraGrid();
            this.checkCINNO.Checked = true;
            this.edtitemDesc.Enabled = false;
        }

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
            InitUltraGridUI(this.ultraGridDetail);

            _DataTableLoadedPart.Columns.Add("Check", typeof(bool));
            _DataTableLoadedPart.Columns.Add("ItemCode", typeof(string));
            _DataTableLoadedPart.Columns.Add("ItemDesc", typeof(string));
            _DataTableLoadedPart.Columns.Add("PlanQty", typeof(string));
            _DataTableLoadedPart.Columns.Add("CartonQty", typeof(string));
            _DataTableLoadedPart.Columns.Add("MoQty", typeof(string));

            _DataTableLoadedPart.Columns["Check"].ReadOnly = false;
            _DataTableLoadedPart.Columns["ItemCode"].ReadOnly = true;
            _DataTableLoadedPart.Columns["ItemDesc"].ReadOnly = true;
            _DataTableLoadedPart.Columns["PlanQty"].ReadOnly = true;
            _DataTableLoadedPart.Columns["CartonQty"].ReadOnly = true;
            _DataTableLoadedPart.Columns["MoQty"].ReadOnly = true;

            this.ultraGridDetail.DataSource = this._DataTableLoadedPart;

            _DataTableLoadedPart.Clear();

            ultraGridDetail.DisplayLayout.Bands[0].Columns["Check"].Width = 60;
            ultraGridDetail.DisplayLayout.Bands[0].Columns["ItemCode"].Width = 200;
            ultraGridDetail.DisplayLayout.Bands[0].Columns["ItemDesc"].Width = 300;
            ultraGridDetail.DisplayLayout.Bands[0].Columns["PlanQty"].Width = 80;
            ultraGridDetail.DisplayLayout.Bands[0].Columns["CartonQty"].Width = 80;
            ultraGridDetail.DisplayLayout.Bands[0].Columns["MoQty"].Width = 80;

            ultraGridDetail.DisplayLayout.Bands[0].Columns["Check"].CellActivation = Activation.AllowEdit;
            ultraGridDetail.DisplayLayout.Bands[0].Columns["ItemCode"].CellActivation = Activation.NoEdit;
            ultraGridDetail.DisplayLayout.Bands[0].Columns["ItemDesc"].CellActivation = Activation.NoEdit;
            ultraGridDetail.DisplayLayout.Bands[0].Columns["PlanQty"].CellActivation = Activation.NoEdit;
            ultraGridDetail.DisplayLayout.Bands[0].Columns["CartonQty"].CellActivation = Activation.NoEdit;
            ultraGridDetail.DisplayLayout.Bands[0].Columns["MoQty"].CellActivation = Activation.NoEdit;

        }

        private void ultraGridDetail_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            _UltraWinGridHelper1 = new UltraWinGridHelper(this.ultraGridDetail);

            _UltraWinGridHelper1.AddCheckColumn("Check", "选择框");
            _UltraWinGridHelper1.AddReadOnlyColumn("ItemCode", "物料代码");
            _UltraWinGridHelper1.AddReadOnlyColumn("ItemDesc", "物料描述");
            _UltraWinGridHelper1.AddReadOnlyColumn("PlanQty", "需求数量");
            _UltraWinGridHelper1.AddReadOnlyColumn("CartonQty", "箱内数量");
            _UltraWinGridHelper1.AddReadOnlyColumn("MoQty", "总装箱数量");
        }

        private void edtMoCode_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string moCode = FormatHelper.CleanString(this.edtMoCode.Value.Trim().ToUpper());
                if (string.IsNullOrEmpty(moCode))
                {
                    this.edtMoCode.TextFocus(true, true);
                    return;
                }

                ItemFacade itemFacade = new ItemFacade(this.DataProvider);
                Domain.MOModel.Material material = (Domain.MOModel.Material)itemFacade.GetMaterialWithMoCode(moCode);
                if (material != null)
                {
                    this.edtitemDesc.Value = material.MaterialName;
                }

                this.LoadData();
                this.edtCarton.TextFocus(true, true);
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.edtMoCode.Value.Trim() == string.Empty)
            {              
                ApplicationRun.GetInfoForm().AddEx("$CS_CMPleaseInputMO");
                this.edtMoCode.TextFocus(true, true);
                return;
            }

            this.LoadData();
        }

        private void edtCarton_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.edtMoCode.Value.Trim() == string.Empty)
                {                    
                    ApplicationRun.GetInfoForm().AddEx("$CS_CMPleaseInputMO");
                    this.edtMoCode.TextFocus(true, true);
                    return;
                }

                if (this.edtCarton.Value.Trim() == string.Empty)
                {
                    this.edtCarton.TextFocus(true, true);
                    return;
                }

                Package.PackageFacade packageFacade = new BenQGuru.eMES.Package.PackageFacade(this.DataProvider);

                SKDCartonDetail skdCartonDetail = (SKDCartonDetail)packageFacade.QuerySKDCartobDetailWithCarton(FormatHelper.CleanString(this.edtCarton.Value.Trim().ToUpper()));

                if (skdCartonDetail != null && skdCartonDetail.moCode.Trim().ToUpper() != FormatHelper.CleanString(this.edtMoCode.Value.Trim().ToUpper()))
                {                    
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error,"$CS_CartonNO_Have_Used $CS_Param_MOCode:" + skdCartonDetail.moCode.Trim().ToUpper()));
                    this.edtCarton.TextFocus(true, true);
                    return;
                }

                this.LoadData();
                this.edtItemCode.TextFocus(true, true);
            }
        }

        private void edtItemCode_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.edtMoCode.Value.Trim() == string.Empty)
                {                    
                    ApplicationRun.GetInfoForm().AddEx("$CS_CMPleaseInputMO");
                    this.edtMoCode.TextFocus(true, true);
                    return;
                }

                if (this.edtCarton.Value.Trim() == string.Empty)
                {                    
                    ApplicationRun.GetInfoForm().AddEx("$CS_PLEASE_INPUT_CARTONNO");
                    this.edtCarton.TextFocus(true, true);
                    return;
                }

                if (ultraGridDetail.Rows.Count <= 0)
                {                   
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_GRID_NO_RECORD"));
                    this.edtItemCode.TextFocus(true, true);
                    return;
                }

                if (!this.CheckGrid())
                {                  
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_GRID_SELECT_ONE_RECORD"));
                    this.edtItemCode.TextFocus(false, true);
                    return;
                }

                Package.PackageFacade packageFacade = new BenQGuru.eMES.Package.PackageFacade(this.DataProvider);
                DataCollect.DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                ItemFacade itemFacade = new ItemFacade(this.DataProvider);

                SKDCartonDetail skdCartonDetail = (SKDCartonDetail)packageFacade.GetSKDCartonDetail(this.edtMoCode.Value.Trim().ToUpper(),
                                                                                                    this.edtItemCode.Value.Trim().ToUpper());
                if (skdCartonDetail != null)
                {                    
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Carton_Have_In_This_moCode"));
                    this.edtItemCode.TextFocus(true, true);
                    return;
                }

                //下面做物料的检查
                Messages msg = new Messages();
                string materialCode = string.Empty;

                for (int i = 0; i < ultraGridDetail.Rows.Count; i++)
                {
                    if (ultraGridDetail.Rows[i].Cells[0].Value.ToString().ToLower() == "true")
                    {
                        materialCode = ultraGridDetail.Rows[i].Cells[1].Value.ToString();
                        _itemCode = materialCode;
                        break;
                    }
                }

                Domain.MOModel.Material material = (Domain.MOModel.Material)itemFacade.GetMaterial(materialCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);

                if (material == null)
                {                   
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_Material_NotFound"));
                    this.edtItemCode.TextFocus(true, true);
                    return;
                }

                OPBOMDetail detailTemp = new OPBOMDetail();

                //模拟一个OPBOMDetail
                detailTemp.OPBOMItemControlType = material.MaterialControlType;
                detailTemp.OPBOMParseType = material.MaterialParseType;
                detailTemp.OPBOMCheckType = material.MaterialCheckType;
                detailTemp.CheckStatus = material.CheckStatus;
                detailTemp.SerialNoLength = material.SerialNoLength;
                detailTemp.NeedVendor = material.NeedVendor;
                detailTemp.OPBOMSourceItemCode = materialCode;
                detailTemp.OPBOMItemCode = materialCode;
                detailTemp.OPBOMItemQty = 1;

                MINNO newMinno = new MINNO();

                newMinno.MOCode = this.edtMoCode.Value.Trim().ToUpper();
                newMinno.MItemCode = material.MaterialCode.Trim();

                msg = dataCollectFacade.GetMINNOByBarcode(detailTemp, this.edtItemCode.Value.Trim().ToUpper(), this.edtMoCode.Value.Trim().ToUpper(), null, false, true, out newMinno);

                if (!msg.IsSuccess())
                {                   
                    ApplicationRun.GetInfoForm().Add(msg);
                    this.edtItemCode.TextFocus(true, true);
                    return;
                }

                //检查上料资料
                if (this.checkCINNO.Checked)
                {
                    object[] onWipItemObjects = dataCollectFacade.QueryOnWIPItemWithmoCode(this.edtItemCode.Value.Trim().ToUpper(), materialCode, this.edtMoCode.Value.Trim().ToUpper());
                    if (onWipItemObjects == null)
                    {                        
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Have_CollectMertial"));
                        this.edtItemCode.TextFocus(true, true);
                        return;
                    }
                }

                this.DataProvider.BeginTransaction();
                try
                {
                    MOFacade moFacade = new MOFacade(this.DataProvider);
                    DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                    Domain.MOModel.MO mo =(Domain.MOModel.MO)moFacade.GetMO(this.edtMoCode.Value.Trim().ToUpper());

                    SKDCartonDetail newSKDCartonDetail = new SKDCartonDetail();

                    newSKDCartonDetail.moCode = this.edtMoCode.Value.Trim().ToUpper();
                    newSKDCartonDetail.CartonNO = this.edtCarton.Value.Trim().ToUpper();
                    if (mo != null)
                    {
                        newSKDCartonDetail.ItemCode = mo.ItemCode;
                    }

                    newSKDCartonDetail.SBItemCode = materialCode;
                    newSKDCartonDetail.MCard = this.edtItemCode.Value.Trim().ToUpper();
                    newSKDCartonDetail.MaintainUser = ApplicationService.Current().UserCode;
                    newSKDCartonDetail.MaintainDate = dBDateTime.DBDate;
                    newSKDCartonDetail.MaintainTime = dBDateTime.DBTime;

                    packageFacade.AddSKDCartonDetail(newSKDCartonDetail);

                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_SKDCarton_Succes"));
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    msg.Add(new UserControl.Message(ex));
                    ApplicationRun.GetInfoForm().Add(msg);
                }
                finally
                {
                    this.DataProvider.CommitTransaction();
                    this.LoadData();
                    this.MakeGridChecked();
                    this.edtItemCode.TextFocus(true, true);
                }
            }
        }

        private void ClearValues()
        {
            this.edtCarton.Value = string.Empty;
            this.edtItemCode.Value = string.Empty;
            this.edtitemDesc.Value = string.Empty;
            _DataTableLoadedPart.Clear();
        }

        private void edtMoCode_InnerTextChanged(object sender, EventArgs e)
        {
            this.ClearValues();
        }

        private void edtCarton_InnerTextChanged(object sender, EventArgs e)
        {
            _DataTableLoadedPart.Clear();
            this.edtItemCode.Value = string.Empty;
        }

        private void ultraGridDetail_CellChange(object sender, CellEventArgs e)
        {
            if (ultraGridDetail.ActiveRow != null)
            {
                for (int i = 0; i < ultraGridDetail.Rows.Count; i++)
                {
                    if (ultraGridDetail.Rows[i].Activated == false)
                    {
                        ultraGridDetail.Rows[i].Cells[0].Value = false;
                    }
                }
            }
        }

        private void LoadData()
        {
            Package.PackageFacade packageFacade = new BenQGuru.eMES.Package.PackageFacade(this.DataProvider);

            object[] skdCartonDetailList = packageFacade.QuerySKDCartobDetailWithCapity(this.edtCarton.Value.Trim().ToUpper(),
                                                                                this.edtMoCode.Value.Trim().ToUpper());
            _DataTableLoadedPart.Clear();
            if (skdCartonDetailList != null)
            {
                foreach (SKDCartonDetailWithCapity skdCartonDetailWithCapity in skdCartonDetailList)
                {
                    _DataTableLoadedPart.Rows.Add(new object[]{
                        false,
                        skdCartonDetailWithCapity.SBItemCode,
                        skdCartonDetailWithCapity.MaterialName,
                        skdCartonDetailWithCapity.planQty,
                        skdCartonDetailWithCapity.cartonQty,
                        skdCartonDetailWithCapity.moQty});
                }
            }
        }

        private bool CheckGrid()
        {
            if (ultraGridDetail.Rows.Count >= 0)
            {
                for (int i = 0; i < ultraGridDetail.Rows.Count; i++)
                {
                    if (ultraGridDetail.Rows[i].Cells[0].Value.ToString().ToLower() == "true")
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void MakeGridChecked()
        {
            if (ultraGridDetail.Rows.Count >= 0)
            {
                for (int i = 0; i < ultraGridDetail.Rows.Count; i++)
                {
                    if (ultraGridDetail.Rows[i].Cells[1].Value.ToString().ToUpper() == _itemCode.ToUpper())
                    {
                        ultraGridDetail.Rows[i].Cells[0].Value = true;
                    }
                }
            }
        }

    }
}