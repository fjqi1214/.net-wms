using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Web.Helper;
using UserControl;
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.ATE;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.TS;

namespace BenQGuru.eMES.Client
{
    public partial class FCollectErrorCode : System.Windows.Forms.Form
    {
        ProductInfo product = null;
        public FCollectErrorCode(ProductInfo productInfo)
        {
            InitializeComponent();
            product = productInfo;

           

            this.txtResource.Value = Service.ApplicationService.Current().ResourceCode;
        }

        //如果产品已经有不良代码, 将已经有的不良代码选择
        public bool IsSelectErrorCode = false;
        public bool IsCheckRights = true;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public string[] SelectedErrorCodes = null;
        public string[] SelectedErrorCodeLocation = null;
        public string[] SelectedErrorCodePoints = null;
        public string CheckResource
        {
            get { return FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtResource.Value)); }
            set { this.txtResource.Value = value; }
        }
        public bool IsOK = false;

        private bool _isResActive = false;
        public void SetResTextBoxFocus()
        {
            _isResActive = true;
        }
        public void SetCheckResult(string value)
        {
            this.txtResource.Value = value;
            this.CheckResource = value;
        }
        public void ShowHidePointColumn(bool show)
        {
            this.ucErrorCodeSelect21.ShowHidePointColumn(show);
        }

        public object[] Old_ErrorCodes;

        private void FCollectErrorCode_Load(object sender, EventArgs e)
        {
            UserControl.UIStyleBuilder.FormUI(this);

            //补录不良时默认不带出不良代码
            if(!_isResActive)
                this.LoadInitData();

            if (this.IsSelectErrorCode && Old_ErrorCodes != null && Old_ErrorCodes.Length > 0)
            {
                TSErrorCode2Location[] errorInfo = new TSErrorCode2Location[Old_ErrorCodes.Length];

                for (int i = 0; i < Old_ErrorCodes.Length; i++)
                {
                    TSErrorCode ec = Old_ErrorCodes[i] as TSErrorCode;

                    errorInfo[i] = new BenQGuru.eMES.Domain.TS.TSErrorCode2Location();
                    errorInfo[i].ErrorCode = ec.ErrorCode;
                    errorInfo[i].ErrorCodeGroup = ec.ErrorCodeGroup;
                    if (ec.EAttribute1 != "" && ec.EAttribute1 != null)
                    {
                        string[] strLocPoint = ec.EAttribute1.Split('|');
                        errorInfo[i].ErrorLocation = strLocPoint[0];

                        // Marked by Scott
                        //if (strLocPoint.Length > 1 && strLocPoint[1] != "")
                        //    errorInfo[i].Points = int.Parse(strLocPoint[1]);
                    }
                    errorInfo[i].AB = ItemLocationSide.ItemLocationSide_AB;
                }

                ucErrorCodeSelect21.AddSelectedErrorCodes(errorInfo);
            }
            if (this.IsSelectErrorCode)
            {
                txtResource.Visible = false;
            }
        }

        private void LoadInitData()
        {
            if (product == null)
            {
                TSModel.TSModelFacade tsmodelFacade = new TSModelFacade(this.DataProvider);
                object[] objsECodeAll = tsmodelFacade.GetAllErrorCode();
                if (objsECodeAll == null || objsECodeAll.Length == 0)
                    return;

                ErrorCodeA[] errorsAll = new ErrorCodeA[objsECodeAll.Length];
                objsECodeAll.CopyTo(errorsAll, 0);
                this.ucErrorCodeSelect21.AddErrorCodes(errorsAll);
                this.ucErrorCodeSelect21.Focus();
                this.ucErrorCodeSelect21.FocusInput();
                return;
            }

            if (product == null || product.LastSimulation == null)
                return;
            ItemFacade itFacade = new ItemFacade(this.DataProvider);
            BaseSetting.BaseModelFacade baseFacade = new BaseSetting.BaseModelFacade(this.DataProvider);

            object op = null;

            if (product.LastSimulation.RouteCode != null && product.LastSimulation.RouteCode != string.Empty)
            {
                try
                {
                    op = itFacade.GetItemRoute2Operation(product.LastSimulation.ItemCode,
                    product.LastSimulation.RouteCode,
                    product.LastSimulation.OPCode);
                }
                catch { }
            }

            string strOPCode = "";
            if (op != null)
            {
                ItemRoute2OP CurrentOP = op as ItemRoute2OP;
                object objOP2RES = baseFacade.GetOperation2Resource(CurrentOP.OPCode, this.txtResource.Value.Trim().ToUpper());
                if (objOP2RES != null)
                {
                    strOPCode = CurrentOP.OPCode;
                }
                else
                {
                    /* joe song 不知道这段代码为什么这样写,为了增加对线外工序的支持, 修改之
                    //object objSOP = baseFacade.GetOperationByRouteAndResource(product.LastSimulation.RouteCode, this.txtResource.Value.Trim().ToUpper());
                    //if (objSOP != null)
                    //{
                    //    object actOP = itFacade.GetItemRoute2Operation(product.LastSimulation.ItemCode, product.LastSimulation.RouteCode, (objSOP as Operation).OPCode);
                   //     if (actOP != null)
                    //    {
                   //         CurrentOP = actOP as ItemRoute2OP;
                   //         strOPCode = CurrentOP.OPCode;
                   //     }
                   // }
                     * */
                    Operation2Resource or = baseFacade.GetOperationByResource(this.txtResource.Value.Trim().ToUpper()) as Operation2Resource;
                    if (or != null)
                    {
                        strOPCode = or.OPCode;
                    }
                }
            }
            if (strOPCode == "")
            {
                Operation2Resource or = baseFacade.GetOperationByResource(this.txtResource.Value.Trim().ToUpper()) as Operation2Resource;
                if (or != null)
                {
                    strOPCode = or.OPCode;
                }
            }
            if (strOPCode == "")
                return;

            object[] objsECode = baseFacade.GetAllErrorCodeByOperationCode(strOPCode);
            if (objsECode == null || objsECode.Length == 0)
                return;

            ErrorCodeA[] errors = new ErrorCodeA[objsECode.Length];
            objsECode.CopyTo(errors, 0);
            this.ucErrorCodeSelect21.AddErrorCodes(errors);
            this.ucErrorCodeSelect21.Focus();
            this.ucErrorCodeSelect21.FocusInput();
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.SelectedErrorCodes = null;
            this.SelectedErrorCodeLocation = null;
            IsOK = false;
            this.Hide();
        }

        private void txtResource_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!this.CheckResourceRight())
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error, "$Error_No_Resource_Right [" + this.CheckResource + "]"));
                    this.txtResource.Value = "";
                    Application.DoEvents();
                    this.txtResource.TextFocus(false, true);
                    return;
                }
                this.LoadInitData();
            }
        }

        private bool CheckResourceRight()
        {
            if (!this.IsCheckRights)
                return true;

            this.txtResource.Value = this.txtResource.Value.Trim().ToUpper();
            this.CheckResource = this.txtResource.Value.Trim().ToUpper();
            if (this.CheckResource == "")
                this.CheckResource = Service.ApplicationService.Current().ResourceCode;
            // 检查资源的用户权限
            if (this.CheckResource != Service.ApplicationService.Current().ResourceCode)
            {
                bool bIsAdmin = false;
                if (Service.ApplicationService.Current().LoginInfo.UserGroups != null)
                {
                    foreach (object o in Service.ApplicationService.Current().LoginInfo.UserGroups)
                    {
                        if (((UserGroup)o).UserGroupType == "ADMIN")
                        {
                            bIsAdmin = true;
                            break;
                        }
                    }
                }
                if (!bIsAdmin)
                {
                    Security.SecurityFacade securityFacade = new BenQGuru.eMES.Security.SecurityFacade(this.DataProvider);
                    if (!securityFacade.CheckResourceRight(Service.ApplicationService.Current().LoginInfo.UserCode, this.CheckResource))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.CheckResourceRight())
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error, "$Error_No_Resource_Right [" + this.CheckResource + "]"));
                this.txtResource.Value = "";
                Application.DoEvents();
                this.txtResource.TextFocus(false, true);
                return;
            }

            this.ucErrorCodeSelect21.UpdateGridValue();
            this.ucErrorCodeSelect21.SelectedItem(out this.SelectedErrorCodes, out this.SelectedErrorCodeLocation, out this.SelectedErrorCodePoints);
            IsOK = true;
            this.Hide();
        }

        private void FCollectErrorCode_Activated(object sender, EventArgs e)
        {
            if(_isResActive)
                this.txtResource.TextFocus(false, true);
        }

    }
}