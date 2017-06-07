using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BenQGuru.eMES.WinCeClient
{
    public partial class FPackagingOperations : UserControl
    {
        PackagingOperationsService.PackagingOperationsService _PackagingOperationsService;

        private string pickNo = string.Empty;
        private string dqMCode = string.Empty;
        private string cartonNo = string.Empty;
        private string qty = string.Empty;
        private string sn = string.Empty;
        private string userCode = string.Empty;

        private int DataCount = 0;

        #region 初始化，回车事件

        public FPackagingOperations()
        {
            InitializeComponent();

            _PackagingOperationsService = new BenQGuru.eMES.WinCeClient.PackagingOperationsService.PackagingOperationsService();
            _PackagingOperationsService.Url = WebServiceFacade.GetWebServiceURL() + "PackagingOperationsService.asmx";

            BindPickNOComBox();

            this.txtCartonNO.Text = string.Empty;
            this.txtQTY.Text = string.Empty;
            this.txtSN.Text = string.Empty;
            this.lblPQTY.Text = string.Empty;
            this.lblCarInvNO.Text = string.Empty;
            this.dataGrid1.DataSource = null;
            this.lblMessage.Text = string.Empty;

            //this.btnPickNSNMaterial.Enabled = false;
        }

        //绑定拣货任务令号
        private void BindPickNOComBox()
        {
            string[] str = _PackagingOperationsService.QueryPickNONotY(ApplicationService.Current().LoginInfo.UserCode.ToUpper());
            this.cmbPickNO.Items.Clear();
            if (str != null)
            {
                this.cmbPickNO.Items.Add(string.Empty);
                foreach (string s in str)
                {
                    this.cmbPickNO.Items.Add(s);
                }
            }
            else
            {
                this.cmbPickNO.Items.Add(string.Empty);
            }
            this.cmbPickNO.SelectedIndex = 0;
        }

        //拣货任务令号选择变更
        public void cmbPickNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            pickNo = this.cmbPickNO.Text;
            //if (string.IsNullOrEmpty(pickNo))
            //{
            //    return;
            //}

            BindDQMaterialNO();

            string str = this._PackagingOperationsService.GetString(pickNo);
            this.lblPQTY.Text = str.Split(',')[0];
            this.lblCarInvNO.Text = str.Split(',')[1];


            DataTable dt = this._PackagingOperationsService.GetGridTableForPackageWinc(pickNo);
            BindDataGrid(dt);
        }

        //绑定鼎桥物料编码
        protected void BindDQMaterialNO()
        {
            pickNo = this.cmbPickNO.Text;
            //if(string.IsNullOrEmpty(pickNo))
            //{
            //    return;
            //}
            string[] str = _PackagingOperationsService.QueryDQMaterialNO(pickNo);
            this.cmbDQMCode.Items.Clear();
            if (str != null)
            {
                this.cmbDQMCode.Items.Add(string.Empty);
                foreach (string s in str)
                {
                    this.cmbDQMCode.Items.Add(s);
                }
            }
            else
            {
                this.cmbDQMCode.Items.Add(string.Empty);
            }
            this.cmbDQMCode.SelectedIndex = 0;
        }

        //鼎桥物料编码选择变更
        private void cmbDQMCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cmbDQMCode.Text))
            {
                this.txtSN.Enabled = true;
                this.txtQTY.Enabled = true;
                return;
            }
            bool isKeyparts = this._PackagingOperationsService.isKeyparts(this.cmbDQMCode.Text);
            if (isKeyparts)
            {
                this.txtSN.Enabled = true;

                this.txtQTY.Enabled = false;
                this.txtQTY.Text = string.Empty;
            }
            else
            {
                this.txtSN.Enabled = false;
                this.txtSN.Text = string.Empty;

                this.txtQTY.Enabled = true;
            }
        }

        //包装箱号回车
        private void txtCartonNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!this.ValidateInput())
                {
                    return;
                }
                pickNo = this.cmbPickNO.Text;
                dqMCode = this.cmbDQMCode.Text;
                cartonNo = this.txtCartonNO.Text.Trim().ToUpper();
                userCode = ApplicationService.Current().LoginInfo.UserCode.ToUpper();
                string message = string.Empty;
                bool result = this._PackagingOperationsService.CartonNOKeyPressReturnMessage(pickNo, dqMCode, cartonNo, userCode, out message);

                this.lblMessage.Text = message;
                if (result)
                {
                    txtCartonNO.Text = string.Empty;
                    DataTable dt = this._PackagingOperationsService.GetGridTableForPackageWinc(pickNo);
                    BindDataGrid(dt);

                    string str = this._PackagingOperationsService.GetString(pickNo);
                    this.lblPQTY.Text = str.Split(',')[0];
                 


                }
            }
        }

        //数量回车
        private void txtQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!this.ValidateInput())
                {
                    return;
                }
                pickNo = this.cmbPickNO.Text;
                dqMCode = this.cmbDQMCode.Text;
                cartonNo = this.txtCartonNO.Text.Trim().ToUpper();
                qty = this.txtQTY.Text.Trim();
                userCode = ApplicationService.Current().LoginInfo.UserCode.ToUpper();
                string message = string.Empty;
                bool result = this._PackagingOperationsService.QTYKeyPressReturnMessage(pickNo, dqMCode, cartonNo, qty, userCode, out message);
                this.lblMessage.Text=message;
                if (result == true)
                {
                    txtQTY.Text = string.Empty;
                    DataTable dt = this._PackagingOperationsService.GetGridTableForPackageWinc(pickNo);
                    BindDataGrid(dt);


                    string str = this._PackagingOperationsService.GetString(pickNo);
                    this.lblPQTY.Text = str.Split(',')[0];
                   
                }
            }
        }

        //SN回车
        private void txtSN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!this.ValidateInput())
                {
                    return;
                }
                pickNo = this.cmbPickNO.Text;
                dqMCode = this.cmbDQMCode.Text;
                cartonNo = this.txtCartonNO.Text.Trim().ToUpper();
                sn = this.txtSN.Text.Trim().ToUpper();
                userCode = ApplicationService.Current().LoginInfo.UserCode.ToUpper();

            
                string message = string.Empty;
                bool result = this._PackagingOperationsService.SNKeyPressReturnMessage(pickNo, dqMCode, cartonNo, sn, userCode, out message);
                lblMessage.Text = message;
                if (result)
                {
                    txtSN.Text = string.Empty;
                    DataTable dt = this._PackagingOperationsService.GetGridTableForPackageWinc(pickNo);
                    BindDataGrid(dt);


                    string str = this._PackagingOperationsService.GetString(pickNo);
                    this.lblPQTY.Text = str.Split(',')[0];
                }
            }
        }

        #endregion

        #region button
        //提交
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!this.ValidateInput())
            {
                return;
            }
            pickNo = this.cmbPickNO.Text;
            cartonNo = this.txtCartonNO.Text.Trim().ToUpper();
            userCode = ApplicationService.Current().LoginInfo.UserCode.ToUpper();

            #region
            //try
            //{
            //    this._PackagingOperationsService.BeginTransaction();

            //    object obj = this._PackagingOperationsService.GetCartonInvoices(pickNo);
            //    if (obj == null)
            //    {
            //        this.lblMessage.Text = "当前拣货任务令号没有对应的发货箱单信息";
            //        return;
            //    }

            //    //1. 当前包装箱号在发货箱单明细信息表(TBLCartonInvDetail)中的状态更新为：ClosePack:包装完成
            //    object _obj = this._PackagingOperationsService.GetCartonInvDetail(carInvNo, cartonNo);
            //    if (_obj == null)
            //    {
            //        this.lblMessage.Text = "当前包装箱号没有对应的发货箱单明细信息";
            //        return;
            //    }
            //    this._PackagingOperationsService.UpdateCartonInvDetail(carInvNo, cartonNo, "ClosePack", userCode);

            //    //2. 检查当前拣货任务令号在拣货任务令明细表(TBLPICKDETAIL)中所有记录SQTY=PQTY时，更新发货箱单号状态(TBLCartonInvoices .STATUS)为：ClosePack:包装完成
            //    object[] objs = this._PackagingOperationsService.QueryPickDetails(pickNo);
            //    if (objs == null)
            //    {
            //        this._PackagingOperationsService.RollbackTransaction();
            //        this.lblMessage.Text = "当前拣货任务令号没有对应的拣货任务令明细信息";
            //        return;
            //    }
            //    this._PackagingOperationsService.UpdateCartonInvoices(pickNo, carInvNo, "ClosePack", userCode);
            //    this._PackagingOperationsService.UpdatePickDetails(pickNo, "ClosePack", userCode);

            //    this._PackagingOperationsService.CommitTransaction();
            //    this.lblMessage.Text = "提交成功";
            //}
            //catch (Exception ex)
            //{
            //    this._PackagingOperationsService.RollbackTransaction();
            //    this.lblMessage.Text = "提交失败：" + ex.Message;
            //}
            #endregion

            this.lblMessage.Text = this._PackagingOperationsService.SubmitReturnMessage(pickNo, cartonNo, userCode);
        }

        //挑非SN物料
        //private void btnPickNSNMaterial_Click(object sender, EventArgs e)
        //{
        //    FPickMaterial fPickMaterial = new FPickMaterial(this.cmbPickNO.Text,this.lblCarInvNO.Text,this.txtCartonNO.Text.Trim());
        //    fPickMaterial.Show();
        //    this.Visible = true;
        //}

        //包装详细
        private void btnPackingDetail_Click(object sender, EventArgs e)
        {
            FPackingDetailForm fPackingDetailForm = new FPackingDetailForm(this.cmbPickNO.Text, this.lblCarInvNO.Text);
            fPackingDetailForm.Show();
        }

        //返回
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        #endregion

        #region 自定义方法
        private bool ValidateInput()
        {
            pickNo = this.cmbPickNO.Text;
            cartonNo = this.txtCartonNO.Text.Trim();

            if (string.IsNullOrEmpty(pickNo))
            {
                //提示信息
                this.lblMessage.Text = "请选择拣货任务令号";
                return false;
            }
            if (string.IsNullOrEmpty(cartonNo))
            {
                this.lblMessage.Text = "请输入包装箱号";
                this.txtCartonNO.Focus();
                return false;
            }

            return true;
        }

        private void BindDataGrid(DataTable dt) //控件dataGrid1的列宽，注意这里传入的是DataTable
        {


            if (dt.Rows.Count > 0)
            {


                DataGridTableStyle ts = new DataGridTableStyle();
                ts.MappingName = dt.TableName;

                DataGridColumnStyle ColStyle1 = new DataGridTextBoxColumn();
                ColStyle1.MappingName = dt.Columns[0].ColumnName.ToString();
                ColStyle1.HeaderText = "鼎桥物料号";
                ColStyle1.Width = 20;
                ts.GridColumnStyles.Add(ColStyle1);

                DataGridColumnStyle ColStyle3 = new DataGridTextBoxColumn();
                ColStyle3.MappingName = dt.Columns[1].ColumnName.ToString();
                ColStyle3.HeaderText = "箱号";
                ts.GridColumnStyles.Add(ColStyle3);

                DataGridColumnStyle ColStyle4 = new DataGridTextBoxColumn();
                ColStyle4.MappingName = dt.Columns[2].ColumnName.ToString();
                ColStyle4.HeaderText = "包装数量";
                ts.GridColumnStyles.Add(ColStyle4);


                this.dataGrid1.TableStyles.Clear();
                this.dataGrid1.TableStyles.Add(ts);

                this.DataCount = dt.Rows.Count;
                this.dataGrid1.DataSource = dt;
            }

        }
        #endregion

        private void btnPackFinish_Click(object sender, EventArgs e)
        {
            string message = _PackagingOperationsService.PackFinish(cmbPickNO.SelectedItem.ToString(), ApplicationService.Current().LoginInfo.UserCode.ToUpper());
            MessageBox.Show(message);
        }

        private void btnApplyOQC_Click(object sender, EventArgs e)
        {

        }

        private void btnApplyOQC_Click_1(object sender, EventArgs e)
        {
            string message = this._PackagingOperationsService.ApplyOQC(this.lblCarInvNO.Text, cmbPickNO.SelectedItem.ToString(), ApplicationService.Current().LoginInfo.UserCode.ToUpper());
            MessageBox.Show(message);
        }
    }
}
