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
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using UserControl;


namespace BenQGuru.eMES.Client
{
    public partial class FReturneMaterial : Form
    {

        #region  变量

        private IDomainDataProvider _DomainDataProvider = ApplicationService.Current().DataProvider;

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


        public FReturneMaterial()
        {
            InitializeComponent();
        }

        private void edtMaterialLot_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.edtMaterialLot.Value.Trim() == string.Empty)
                {
                    this.edtMaterialLot.TextFocus(false, true);
                    return;
                }

                InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
                MaterialLot materialLot = (MaterialLot)inventoryFacade.GetMaterialLot(FormatHelper.CleanString(this.edtMaterialLot.Value.Trim().ToUpper()));

                if (materialLot == null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_CS_MaterialLot_Must_Exist"));
                    this.edtMaterialLot.TextFocus(false, true);
                    return;
                }

                this.edtReturnNumber.TextFocus(false, true);
            }
        }

        private void edtReturnNumber_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.edtReturnNumber.Value.Trim() == string.Empty)
                {
                    ApplicationRun.GetInfoForm().AddEx("$CS_InPut_ReturnNumber");
                    this.edtReturnNumber.TextFocus(false, true);
                    return;
                }

                if (Convert.ToInt32(this.edtReturnNumber.Value.Trim()) < 1)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_CS_Retun_Qty_Should_Over_Zero"));
                    this.edtReturnNumber.TextFocus(false, true);
                    return;
                }
            }
        }

        private void btnSendMetrial_Click(object sender, EventArgs e)
        {
            if (this.edtMaterialLot.Value.Trim() == string.Empty)
            {
                ApplicationRun.GetInfoForm().AddEx("$Please_Input_MaterialLot");
                this.edtMaterialLot.TextFocus(false, true);
                return;
            }

            if (this.edtReturnNumber.Value.Trim() == string.Empty)
            {
                ApplicationRun.GetInfoForm().AddEx("$CS_InPut_ReturnNumber");
                this.edtReturnNumber.TextFocus(false, true);
                return;
            }

            if (Convert.ToInt32(this.edtReturnNumber.Value.Trim()) < 1)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_CS_Retun_Qty_Should_Over_Zero"));
                this.edtReturnNumber.TextFocus(false, true);
                return;
            }

            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            MaterialLot materialLot = (MaterialLot)inventoryFacade.GetMaterialLot(FormatHelper.CleanString(this.edtMaterialLot.Value.Trim().ToUpper()));

            if (materialLot == null)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_CS_MaterialLot_Must_Exist"));
                this.edtMaterialLot.TextFocus(false, true);
                return;
            }

            if (materialLot.LotQty + Convert.ToInt32(this.edtReturnNumber.Value.Trim()) > materialLot.LotInQty)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MaterialLot_LotQty:" + materialLot.LotQty + "$CS_MaterialLot_LotInQty:" + materialLot.LotInQty));
                this.edtReturnNumber.TextFocus(false, true);
                return;
            }

            DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

           
            try
            {
                this.DataProvider.BeginTransaction();

                materialLot.LotQty += Convert.ToInt32(this.edtReturnNumber.Value.Trim());
                materialLot.MaintainUser = ApplicationService.Current().UserCode;
                materialLot.MaintainDate = dBDateTime.DBDate;
                materialLot.MaintainTime = dBDateTime.DBTime;

                inventoryFacade.UpdateMaterialLot(materialLot);

                MaterialReturn materialReturn = new MaterialReturn();

                materialReturn.MaterialLotNo = materialLot.MaterialLotNo;
                materialReturn.PostSeq = inventoryFacade.GetMaterialReturnsMaxSeq(materialLot.MaterialLotNo);
                materialReturn.TransQty = Convert.ToInt32(this.edtReturnNumber.Value.Trim());
                materialReturn.MaintainUser = ApplicationService.Current().UserCode;
                materialReturn.MaintainDate = dBDateTime.DBDate;
                materialReturn.MaintainTime = dBDateTime.DBTime;

                inventoryFacade.AddMaterialReturn(materialReturn);

                this.DataProvider.CommitTransaction();

                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_ReturnMaterialLot_Success"));
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();

                Messages msg = new Messages();               
                msg.Add(new UserControl.Message(ex));
                ApplicationRun.GetInfoForm().Add(msg);
            }           

            this.edtMaterialLot.TextFocus(false, true);
        }
   
    }
}