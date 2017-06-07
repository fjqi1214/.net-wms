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
    public partial class FGFPackagingOperations : UserControl
    {
        PackagingOperationsService.PackagingOperationsService _PackagingOperationsService;

        private string pickNo = string.Empty;
        private string gfHWItemCode = string.Empty;
        private string gfPackingSEQ = string.Empty;
        private string suiteQTY = string.Empty;
        private string dqMCode = string.Empty;
        private string dqsMCode = string.Empty;
        private string cartonNo = string.Empty;
        private string qty = string.Empty;
        private string sn = string.Empty;
        private string userCode = string.Empty;

        private int DataCount = 0;

        #region 初始化，回车事件

        public FGFPackagingOperations()
        {
            InitializeComponent();

            _PackagingOperationsService = new BenQGuru.eMES.WinCeClient.PackagingOperationsService.PackagingOperationsService();
            _PackagingOperationsService.Url = WebServiceFacade.GetWebServiceURL() + "PackagingOperationsService.asmx";

            BindPickNOComBox();

            this.txtSuiteQTY.Text = string.Empty;
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
            string[] str = _PackagingOperationsService.QueryPickNO("X", ApplicationService.Current().LoginInfo.UserCode.ToUpper());
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

            //绑定光伏华为编码
            string[] str = _PackagingOperationsService.QueryGFHWItemCode(pickNo);
            this.cmbGFHWItemCode.Items.Clear();
            if (str != null)
            {
                this.cmbGFHWItemCode.Items.Add(string.Empty);
                foreach (string s in str)
                {
                    this.cmbGFHWItemCode.Items.Add(s);
                }
            }
            else
            {
                this.cmbGFHWItemCode.Items.Add(string.Empty);
            }
            this.cmbGFHWItemCode.SelectedIndex = 0;

            BindDQMaterialNO();

            string _str = this._PackagingOperationsService.GetString(pickNo);
            this.lblPQTY.Text = _str.Split(',')[0];
            this.lblCarInvNO.Text = _str.Split(',')[1];

            DataTable dt = new DataTable();
            dt = this._PackagingOperationsService.GetDataGrid(pickNo);
            BindDataGrid(dt);
        }

        //绑定鼎桥物料编码
        protected void BindDQMaterialNO()
        {
            pickNo = this.cmbPickNO.Text;
            //if (string.IsNullOrEmpty(pickNo))
            //{
            //    return;
            //}
            string[] str = _PackagingOperationsService.QueryDQMaterialNO(pickNo);
            this.cmbDQMCode.Items.Clear();
            this.drpDQSMCodeEdit.Items.Clear();
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
                this.drpDQSMCodeEdit.Items.Clear();
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

            #region DQSMcode
            this.drpDQSMCodeEdit.Items.Clear();

            string pickNo = this.cmbPickNO.Text.Trim().ToUpper();
            string dqmcode = this.cmbDQMCode.Text.Trim().ToUpper();
            string[] str = _PackagingOperationsService.QueryGFDqsMcode(pickNo, dqmcode);
            if (str != null)
            {
                this.drpDQSMCodeEdit.Items.Add(string.Empty);
                foreach (string s in str)
                {
                    this.drpDQSMCodeEdit.Items.Add(s);
                }
            }
            else
            {
                this.drpDQSMCodeEdit.Items.Add(string.Empty);
            }
            this.drpDQSMCodeEdit.SelectedIndex = 0;
            #endregion
        }

        //光伏华为编码选择变更
        private void cmbGFHWItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            gfHWItemCode = this.cmbGFHWItemCode.Text;
            //if (string.IsNullOrEmpty(gfHWItemCode))
            //{
            //    return;
            //}
            //绑定光伏包装序号
            string[] str = _PackagingOperationsService.QueryGFPackingSEQ(pickNo, gfHWItemCode);
            this.cmbGFPackingSEQ.Items.Clear();
            if (str != null)
            {
                this.cmbGFPackingSEQ.Items.Add(string.Empty);
                foreach (string s in str)
                {
                    this.cmbGFPackingSEQ.Items.Add(s);
                }
            }
            else
            {
                this.cmbGFPackingSEQ.Items.Add(string.Empty);
            }
            this.cmbGFPackingSEQ.SelectedIndex = 0;
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
                gfHWItemCode = this.cmbGFHWItemCode.Text;
                gfPackingSEQ = this.cmbGFPackingSEQ.Text;
                suiteQTY = this.txtSuiteQTY.Text.Trim();
                dqMCode = this.cmbDQMCode.Text;

                cartonNo = this.txtCartonNO.Text.Trim().ToUpper();
                userCode = ApplicationService.Current().LoginInfo.UserCode.ToUpper();

                #region
                //if (this._PackagingOperationsService.QueryPickDetailMaterial(pickNo, cartonNo) == null)
                //{
                //    this.txtSN.Focus();
                //    this.btnPickNSNMaterial.Enabled = true;
                //    return;
                //}
                //else
                //{
                //    //1)	检查包装箱号在已拣物料明细表(TBLPICKDetailMaterial)对应拣货任务令明细表(TBLPICKDETAIL)中的光伏华为编码(TBLPICKDETAIL.GFHWITEMCODE)、光伏包装序号(TBLPICKDETAIL.GFPACKINGSEQ)与编辑区域中选择的光伏华为编码、光伏包装序号是否同等，如不相等则报错提示该箱号对应选择的光伏华为编码、光伏包装序号不相等
                //    if (!this._PackagingOperationsService.IsEqual(pickNo, cartonNo, gfHWItemCode, gfPackingSEQ))
                //    {
                //        this.lblMessage.Text = "该箱号对应选择的光伏华为编码、光伏包装序号不相等";
                //        return;
                //    }

                //    //2)	检查选择拣货任务令号、光伏华为编码、光伏包装序号在发货箱单明细物料信息表(TBLCartonInvDetailMaterial)中对应发货箱号条码(TBLCartonInvDetail.CARTONNO)的数量是否小于编辑区域输入的套件数，如小于则报错提示选择的拣货任务令号、光伏华为编码、光伏包装序号已包装的套件数已经大于等于输入套件数
                //    if (this._PackagingOperationsService.GetCartonNoCount(pickNo, gfHWItemCode, gfPackingSEQ) < suiteQTY)
                //    {
                //        this.lblMessage.Text = "选择的拣货任务令号、光伏华为编码、光伏包装序号已包装的套件数已经大于等于输入套件数";
                //        return;
                //    }

                //    //3)	检查包装箱号在已拣物料明细表(TBLPICKDetailMaterial)中PQTY不等于0时，则报错提显示该箱号已部分包装
                //    decimal pQty = this._PackagingOperationsService.QueryPickDetailMaterial_PQty(pickNo, cartonNo);
                //    if (pQty != 0)
                //    {
                //        this.lblMessage.Text = "该箱号已部分包装";
                //        this.txtCartonNO.Focus();
                //        return;
                //    }
                //    if (pQty == 0)
                //    {
                //        try
                //        {
                //            this._PackagingOperationsService.BeginTransaction();

                //            //1>	新增发货箱单明细信息表(TBLCartonInvDetail)数据
                //            this._PackagingOperationsService.AddCartonInvDetail(carInvNo, pickNo, "ClosePack", cartonNo, userCode);

                //            this._PackagingOperationsService.CommitTransaction();

                //            //2>	新增发货箱单明细物料信息表(TBLCartonInvDetailMaterial)数据
                //            this._PackagingOperationsService.AddCartonInvDetailMaterial(pickNo, cartonNo, carInvNo, userCode);

                //            //3>	新增发货箱单明细SN信息表(TBLCartonInvDetailSN)数据
                //            this._PackagingOperationsService.AddCARTONINVDETAILSN(pickNo, cartonNo, carInvNo, userCode);

                //            //4>	更新拣拣货任务令头表(TBLPICK)数据
                //            this._PackagingOperationsService.UpdatePick(pickNo, "Pack", userCode);

                //            //5>	更新拣货任务令明细表(TBLPICKDETAIL)数据
                //            this._PackagingOperationsService.UpdatePickdetail(pickNo, cartonNo, "ClosePack", -1, userCode);

                //            //6>	更新已拣物料明细表(TBLPICKDetailMaterial)数据
                //            this._PackagingOperationsService.UpdatePickdetailmaterial(pickNo, cartonNo, -1, userCode);

                //            //7>	当前包装箱号在发货箱单明细信息表(TBLCartonInvDetail)中的状态更新为：ClosePack:包装完成
                //            this._PackagingOperationsService.UpdateCartonInvDetail(carInvNo, cartonNo, "ClosePack", userCode);

                //            //8>	检查当前拣货任务令号在拣货任务令明细表(TBLPICKDETAIL)中所有记录SQTY=PQTY时，更新发货箱单号状态(TBLCartonInvoices .STATUS)为：ClosePack:包装完成
                //            this._PackagingOperationsService.UpdateCartonInvoices(pickNo, carInvNo, "ClosePack", userCode);

                //            this._PackagingOperationsService.CommitTransaction();
                //        }
                //        catch (Exception ex)
                //        {
                //            this.lblMessage.Text = ex.Message;
                //            this._PackagingOperationsService.RollbackTransaction();
                //        }
                //    }
                //}
                #endregion

                this.lblMessage.Text = this._PackagingOperationsService.GFCartonNOKeyPressReturnMessage(pickNo, gfHWItemCode, gfPackingSEQ, suiteQTY, dqMCode, cartonNo, userCode);

                string _str = this._PackagingOperationsService.GetString(pickNo);
                this.lblPQTY.Text = _str.Split(',')[0];
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
                gfHWItemCode = this.cmbGFHWItemCode.Text;
                gfPackingSEQ = this.cmbGFPackingSEQ.Text;
                suiteQTY = this.txtSuiteQTY.Text.Trim();
                dqMCode = this.cmbDQMCode.Text;
                dqsMCode = this.drpDQSMCodeEdit.Text;
                cartonNo = this.txtCartonNO.Text.Trim().ToUpper();
                qty = this.txtQTY.Text.Trim();
                userCode = ApplicationService.Current().LoginInfo.UserCode.ToUpper();
                
                this.lblMessage.Text = this._PackagingOperationsService.GFQTYKeyPressReturnMessage(pickNo, gfHWItemCode, gfPackingSEQ, suiteQTY, dqMCode, dqsMCode, cartonNo, qty, userCode);

                string _str = this._PackagingOperationsService.GetString(pickNo);
                this.lblPQTY.Text = _str.Split(',')[0];
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
                gfHWItemCode = this.cmbGFHWItemCode.Text;
                gfPackingSEQ = this.cmbGFPackingSEQ.Text;
                suiteQTY = this.txtSuiteQTY.Text.Trim();
                dqMCode = this.cmbDQMCode.Text;
                dqsMCode = this.drpDQSMCodeEdit.Text;
                cartonNo = this.txtCartonNO.Text.Trim().ToUpper();
                sn = this.txtSN.Text.Trim().ToUpper();
                userCode = ApplicationService.Current().LoginInfo.UserCode.ToUpper();

                #region
                ////1.	检查SN条码是否存在当前拣货任务令号所在的已拣物料明细表SN信息表(TBLPICKDetailMaterialSN)中，不存在则报错提示刷入SN条码不存在
                //if (this._PackagingOperationsService.GetPickDetailMaterialSN(pickNo, sn) == null)
                //{
                //    this.lblMessage.Text = "刷入SN条码不存在";
                //    this.txtSN.Focus();
                //    return;
                //}

                ////2.	检查SN条码是否存在当前拣货任务令号对应发货箱单的发货箱单明细SN信息表(TBLCartonInvDetailSN)中，存在则报错提示刷入SN条码已包装过
                //if (this._PackagingOperationsService.GetCartonInvDetailSN(carInvNo, sn) != null)
                //{
                //    this.lblMessage.Text = "刷入SN条码已包装过";
                //    this.txtSN.Focus();
                //    return;
                //}

                ////3.	检查通过SN条码在已拣物料明细表SN信息表(TBLPICKDetailMaterialSN)中对应的箱号(TBLPICKDetailMaterialSN.CARTONNO)在已拣物料明细表(TBLPICKDetailMaterial)对应拣货任务令明细表(TBLPICKDETAIL)中的光伏华为编码(TBLPICKDETAIL.GFHWITEMCODE)、光伏包装序号(TBLPICKDETAIL.GFPACKINGSEQ)与编辑区域中选择的光伏华为编码、光伏包装序号是否同等，如不相等则报错提示该箱号对应选择的光伏华为编码、光伏包装序号不相等
                //if (!this._PackagingOperationsService._IsEqual(pickNo, sn, gfHWItemCode, gfPackingSEQ))
                //{
                //    this.lblMessage.Text = "该箱号对应选择的光伏华为编码、光伏包装序号不相等";
                //    return;
                //}

                ////4.	检查选择拣货任务令号、光伏华为编码、光伏包装序号在发货箱单明细物料信息表(TBLCartonInvDetailMaterial)中对应发货箱号条码(TBLCartonInvDetail.CARTONNO)的数量是否小于编辑区域输入的套件数，如小于则报错提示选择的拣货任务令号、光伏华为编码、光伏包装序号已包装的套件数已经大于等于输入套件数
                //if (this._PackagingOperationsService.GetCartonNoCount(pickNo, gfHWItemCode, gfPackingSEQ) < suiteQTY)
                //{
                //    this.lblMessage.Text = "选择的拣货任务令号、光伏华为编码、光伏包装序号已包装的套件数已经大于等于输入套件数";
                //    return;
                //}

                //try
                //{
                //    this._PackagingOperationsService.BeginTransaction();

                //    //5.	检查包装箱号是否存在当前拣货任务令号对应发货箱单的发货箱单明细信息表(TBLCartonInvDetail)中，不存在则新增发货箱单明细信息表(TBLCartonInvDetail)数据
                //    if (this._PackagingOperationsService.GetCartonInvDetail(carInvNo, cartonNo) == null)
                //    {
                //        this._PackagingOperationsService.AddCartonInvDetail(carInvNo, pickNo, "Pack", cartonNo, userCode);
                //    }

                //    //6.	检查包装箱号、鼎桥物料编码与刷入SN条码对应鼎桥物料编码相同的记录是否存在当前拣货任务令号对应发货箱单的发货箱单明细物料信息表(TBLCartonInvDetailMaterial)中
                //    //1>	存在则更新发货箱单明细物料信息表(TBLCartonInvDetailMaterial)数据
                //    //2>	不存则新增发货箱单明细物料信息表(TBLCartonInvDetailMaterial)数据

                //    //SN + [PICKNO]
                //    //->(已拣物料明细表SN信息)TBLPICKDetailMaterialSN.CARTONNO(原箱号) + [PICKNO]
                //    //->(已拣物料明细表)TBLPICKDetailMaterial.MCODE + CARTONNO(要包入的箱号) + [CARINVNO]
                //    //->(发货箱单明细物料信息)TBLCartonInvDetailMaterial
                //    this._PackagingOperationsService.OperateCartonInvDetailMaterial(pickNo, sn, carInvNo, cartonNo, userCode);

                //    //3>	新增发货箱单明细SN信息表(TBLCartonInvDetailSN)数据
                //    this._PackagingOperationsService.AddCARTONINVDETAILSN(pickNo, cartonNo, carInvNo, userCode);

                //    //4>	更新拣拣货任务令头表(TBLPICK)数据
                //    this._PackagingOperationsService.UpdatePick(pickNo, "Pack", userCode);

                //    //5>	更新拣货任务令明细表(TBLPICKDETAIL)数据
                //    this._PackagingOperationsService.UpdatePickdetail(pickNo, cartonNo, "ClosePack", 1, userCode);

                //    //6>	更新已拣物料明细表(TBLPICKDetailMaterial)数据
                //    this._PackagingOperationsService.UpdatePickdetailmaterial(pickNo, cartonNo, 1, userCode); 

                //    this._PackagingOperationsService.CommitTransaction();
                //}
                //catch (Exception ex)
                //{
                //    this.lblMessage.Text = ex.Message;
                //    this._PackagingOperationsService.RollbackTransaction();
                //}
                #endregion
                string message = this._PackagingOperationsService.GFSNKeyPressReturnMessage(pickNo, gfHWItemCode, gfPackingSEQ, suiteQTY, dqMCode, dqsMCode, cartonNo, sn, userCode);
                if (message == "操作成功")
                {
                    txtSN.Text = string.Empty;
                    string _str = this._PackagingOperationsService.GetString(pickNo);
                    this.lblPQTY.Text = _str.Split(',')[0];
                }
                this.lblMessage.Text = message;

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

            //    //1. 当前包装箱号在发货箱单明细信息表(TBLCartonInvDetail)中的状态更新为：ClosePack:包装完成
            //    this._PackagingOperationsService.UpdateCartonInvDetail(carInvNo, cartonNo, "ClosePack", userCode);

            //    //2. 检查当前拣货任务令号在拣货任务令明细表(TBLPICKDETAIL)中所有记录SQTY=PQTY时，更新发货箱单号状态(TBLCartonInvoices .STATUS)为：ClosePack:包装完成
            //    this._PackagingOperationsService.UpdateCartonInvoices(pickNo, carInvNo, "ClosePack", userCode);

            //    this._PackagingOperationsService.CommitTransaction();

            //    this.lblMessage.Text = "提交成功";
            //}
            //catch (Exception ex)
            //{
            //    this.lblMessage.Text = "提交失败：" + ex.Message;
            //    this._PackagingOperationsService.RollbackTransaction();
            //}
            #endregion

            this.lblMessage.Text = this._PackagingOperationsService.GFSubmitReturnMessage(pickNo, cartonNo, userCode);
        }

        //挑非SN物料
        //private void btnPickNSNMaterial_Click(object sender, EventArgs e)
        //{
        //    FGFPickMaterial fGFPickMaterial = new FGFPickMaterial(this.cmbPickNO.Text, this.lblCarInvNO.Text, this.txtCartonNO.Text.Trim(), this.cmbGFHWItemCode.Text, this.cmbGFPackingSEQ.Text, this.txtSuiteQTY.Text.Trim());
        //    fGFPickMaterial.Show();
        //    this.Visible = true;
        //}

        //包装详细
        private void btnPackingDetail_Click(object sender, EventArgs e)
        {
            FGFPackingDetailForm fGFPackingDetailForm = new FGFPackingDetailForm(this.cmbPickNO.Text, this.lblCarInvNO.Text);
            fGFPackingDetailForm.Show();
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
                //提示信息
                this.lblMessage.Text = "请输入包装箱号";
                this.txtCartonNO.Focus();
                return false;
            }

            return true;
        }

        private void BindDataGrid(DataTable dt) //控件dataGrid1的列宽，注意这里传入的是DataTable
        {
            //样式定义
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = dt.TableName;//此处非常关键,数据表的名字不对,将无法映射成功
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataGridColumnStyle ColStyle = new DataGridTextBoxColumn();
                ColStyle.MappingName = dt.Columns[i].ColumnName.ToString();
                ColStyle.HeaderText = dt.Columns[i].ColumnName.ToString();
                ColStyle.Width = dt.Rows[0][i].ToString().Length * 7 - 1;

                if (dt.Columns[i].ColumnName == "选择") //如果某个条件满足就执行该列是否隐藏
                {
                    ColStyle.Width = 40;//当宽度等于0的时候就可以隐藏这列
                }
                //if (dt.Columns[i].ColumnName == "PickLine") //如果某个条件满足就执行该列是否隐藏
                //{
                //    ColStyle.Width = 0;//当宽度等于0的时候就可以隐藏这列
                //}
                else
                {
                    ColStyle.Width = 100;
                }
                ts.GridColumnStyles.Add(ColStyle);
            }

            if (ts.GridColumnStyles.Count > 0)
            {
                //将样式和控件绑定到一起
                this.dataGrid1.TableStyles.Clear();
                this.dataGrid1.TableStyles.Add(ts);
            }
            this.dataGrid1.DataSource = dt;

            DataCount = dt.Rows.Count;
            ts = null;
            GC.Collect();//回收一下有好处的
        }
        #endregion

        private void btnPackFinish_Click(object sender, EventArgs e)
        {
            string message = this._PackagingOperationsService.GFPackFinish(cmbPickNO.SelectedItem.ToString(), ApplicationService.Current().LoginInfo.UserCode.ToUpper());
            MessageBox.Show(message);
        }

        private void btnApplyOQC_Click(object sender, EventArgs e)
        {
            string message = this._PackagingOperationsService.ApplyOQC(this.lblCarInvNO.Text, cmbPickNO.SelectedItem.ToString(), ApplicationService.Current().LoginInfo.UserCode.ToUpper());
            MessageBox.Show(message);
        }
    }
}
