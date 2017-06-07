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
    public partial class FStorLocTransOperations : UserControl
    {
        PackagingOperationsService.PackagingOperationsService _PackagingOperationsService;

        private string transNo = string.Empty;
        private string type = string.Empty;
        private string locationCode = string.Empty;
        private string fromCartonNo = string.Empty;
        private string qty = string.Empty;
        private string sn = string.Empty;
        private string tLocationCartonNo = string.Empty;
        private string userCode = string.Empty;

        private int DataCount = 0;

        #region 初始化，回车事件

        public FStorLocTransOperations()
        {
            InitializeComponent();

            _PackagingOperationsService = new BenQGuru.eMES.WinCeClient.PackagingOperationsService.PackagingOperationsService();
            _PackagingOperationsService.Url = WebServiceFacade.GetWebServiceURL() + "PackagingOperationsService.asmx";

            BindTransNOComBox();

            this.txtLocationCode.Text = string.Empty;
            this.txtFromCartonNo.Text = string.Empty;
            this.txtQTY.Text = string.Empty;
            this.txtSN.Text = string.Empty;
            this.txtTLocationCartonNo.Text = string.Empty;
            this.dataGrid1.DataSource = null;
            this.lblMessage.Text = string.Empty;
        }

        //绑定转储单
        private void BindTransNOComBox()
        {
            string[] str = _PackagingOperationsService.QueryTransNo("Pick", ApplicationService.Current().LoginInfo.UserCode.ToUpper());
            this.cmbTransNo.Items.Clear();
            if (str != null)
            {
                this.cmbTransNo.Items.Add(string.Empty);
                foreach (string s in str)
                {
                    this.cmbTransNo.Items.Add(s);
                }
            }
            else
            {
                this.cmbTransNo.Items.Add(string.Empty);
            }
            this.cmbTransNo.SelectedIndex = 0;
        }

        private void cmbTransNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            transNo = this.cmbTransNo.Text;
            DataTable dt = new DataTable();
            dt = this._PackagingOperationsService.GetStorLocTransOperationsDataGrid(transNo);
            BindDataGrid(dt);
        }

        private void rdbSplitCarton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdbSplitCarton.Checked)
            {
                this.rdbAllCarton.Checked = false;
                this.txtQTY.Enabled = true;
                this.txtSN.Enabled = true;
            }
            else
            {
                this.rdbAllCarton.Checked = true;
                this.txtQTY.Enabled = false;
                this.txtSN.Enabled = false;
                this.txtQTY.Text = string.Empty;
                this.txtSN.Text = string.Empty;
            }
        }

        private void txtLocationCode_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void txtFromCartonNo_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtSN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.btnSubmit_Click(null, null);
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

            transNo = this.cmbTransNo.Text;

           
            locationCode = this.txtLocationCode.Text.Trim().ToUpper();
            if (this._PackagingOperationsService.IsBelong(transNo, locationCode))
            {
                this.txtFromCartonNo.Focus();
            }
            else
            {
                this.lblMessage.Text = "目标货位对应的目标库位不属于转储单对应的目标库位";
                return;
            }

            fromCartonNo = this.txtFromCartonNo.Text.Trim().ToUpper();
            if (this._PackagingOperationsService.IsCompliance(transNo, fromCartonNo))
            {
                if (this.rdbAllCarton.Checked)
                {
                    this.txtTLocationCartonNo.Focus();
                }
            }
            else
            {
                this.lblMessage.Text = "原箱号对应的库位和出库库位不符";
                return;
            }


            locationCode = this.txtLocationCode.Text.Trim().ToUpper();
            if (this.rdbAllCarton.Checked)
            {
                type = "AllCarton";
            }
            else
            {
                type = "SplitCarton";
            }
            fromCartonNo = this.txtFromCartonNo.Text.Trim().ToUpper();
            qty = this.txtQTY.Text.Trim();
            sn = this.txtSN.Text.Trim().ToUpper();
            tLocationCartonNo = this.txtTLocationCartonNo.Text.Trim().ToUpper();
            userCode = ApplicationService.Current().LoginInfo.UserCode.ToUpper();

            this.lblMessage.Text = this._PackagingOperationsService.StorLocTransOperationsSubmitReturnMessage(transNo, type, locationCode, fromCartonNo, qty, sn, tLocationCartonNo, userCode);
            if (this.lblMessage.Text == "提交成功")
            {
                this.cmbTransNo_SelectedIndexChanged(null, null);
            }
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
            transNo = this.cmbTransNo.Text;
            locationCode = this.txtLocationCode.Text.Trim().ToUpper();
            fromCartonNo=this.txtFromCartonNo.Text.Trim().ToUpper();
            tLocationCartonNo = this.txtTLocationCartonNo.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(transNo))
            {
                //提示信息
                this.lblMessage.Text = "请选择转储单";
                return false;
            }
            if (string.IsNullOrEmpty(locationCode))
            {
                //提示信息
                this.lblMessage.Text = "请输入目标货位";
                this.txtLocationCode.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(fromCartonNo))
            {
                //提示信息
                this.lblMessage.Text = "请输入原箱号";
                this.txtFromCartonNo.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(tLocationCartonNo))
            {
                //提示信息
                this.lblMessage.Text = "请输入目标箱号";
                this.txtTLocationCartonNo.Focus();
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
    }
}
