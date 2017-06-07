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
    public partial class FIQCCheckResultMP : UserControl
    {
        PackagingOperationsService.PackagingOperationsService _PackagingOperationsService;

        private string iqcNo = string.Empty;

        private int DataCount = 0;

        #region 初始化，回车事件

        public FIQCCheckResultMP()
        {
            InitializeComponent();

            _PackagingOperationsService = new BenQGuru.eMES.WinCeClient.PackagingOperationsService.PackagingOperationsService();
            _PackagingOperationsService.Url = WebServiceFacade.GetWebServiceURL() + "PackagingOperationsService.asmx";

            //BindIQCNoComBox();

            this.txtCartonNo.Text = string.Empty;
            //this.rdbSpotCheck.Checked = true;
            //this.rdbFullCheck.Checked = true;
            this.lblAQLStandardDescInfo.Text = string.Empty;
            this.lblSamplesNum.Text = string.Empty;
            this.lblRejectionNum.Text = string.Empty;
            this.dataGrid1.DataSource = null;
            this.lblMessage.Text = string.Empty;
        }

        //绑定IQC检验单
        //private void BindIQCNoComBox()
        //{
        //    string[] str = _PackagingOperationsService.QueryIQCNo();
        //    this.cmbIQCNo.Items.Clear();
        //    if (str != null)
        //    {
        //        this.cmbIQCNo.Items.Add(string.Empty);
        //        foreach (string s in str)
        //        {
        //            this.cmbIQCNo.Items.Add(s);
        //        }
        //    }
        //    else
        //    {
        //        this.cmbIQCNo.Items.Add(string.Empty);
        //    }
        //    this.cmbIQCNo.SelectedIndex = 0;
        //}

        //绑定AQL标准
        private void BindAQLStandardComBox(string aqlStr)
        {

            DataTable dt = _PackagingOperationsService.QueryAQLStandard1(cmbIQCNo.SelectedItem.ToString());
            this.cmbAQLStandard.Items.Clear();
            this.cmbAQLStandard.Items.Add(string.Empty);
            if (dt != null)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.cmbAQLStandard.Items.Add(dt.Rows[i][1].ToString() + "," + dt.Rows[i][0].ToString());

                }

            }

            this.cmbAQLStandard.Text = aqlStr;

            InitPageInfoByComboBox(this.cmbAQLStandard);

        }

        //根据AQL标准下拉框值带出页面相关信息
        /// <summary>
        /// 根据下拉框值带出页面相关信息
        /// </summary>
        /// <param name="drp">下拉框DropDownList</param>
        private void InitPageInfoByComboBox(ComboBox cmb)
        {
            string aql = cmb.Text;

            if (!string.IsNullOrEmpty(aql))
            {

                string[] aqls = aql.Split(',');
                string aqlSeq = aqls[1];
                string aqlLevel = aqls[0];
                string[] str = this._PackagingOperationsService.GetAQLStr(int.Parse(aqlSeq), aqlLevel).Split(',');
                if (str != null && str.Length > 0)
                {
                    this.lblAQLStandardDescInfo.Text = str[0];
                    this.lblSamplesNum.Text = str[1];
                    this.lblRejectionNum.Text = str[2];
                }
                return;

            }
        }

        private void InitAQLStandard()
        {
            if (string.IsNullOrEmpty(this.cmbIQCNo.Text))
            {
                return;
            }
            string aqlLevel = this._PackagingOperationsService.GetAQLLevel(this.cmbIQCNo.Text);
            this.cmbAQLStandard.SelectedItem = aqlLevel;
            InitPageInfoByComboBox(this.cmbAQLStandard);
        }

        //IQC检验单选择
        private void cmbIQCNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cmbIQCNo.Text))
            {
                return;
            }
            string aqlStr = _PackagingOperationsService.QueryAQLStr(this.cmbIQCNo.Text);

            BindAQLStandardComBox(string.IsNullOrEmpty(aqlStr) ? string.Empty : aqlStr);

            DataTable dt = this._PackagingOperationsService.GetIQCCheckResultMPDataGrid(this.cmbIQCNo.Text);

            BindDataGrid(dt);
        }

        //单选按钮选择
        private void rdbFullCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdbFullCheck.Checked)
            {
                this.rdbSpotCheck.Checked = false;
                this.cmbAQLStandard.Enabled = false;
                this.cmbAQLStandard.Text = string.Empty;
                this.lblAQLStandardDescInfo.Text = string.Empty;
            }
            else
            {
                this.rdbSpotCheck.Checked = true;
                this.cmbAQLStandard.Enabled = true;
            }
        }

        //AQL标准选择
        private void cmbAQLStandard_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string aqlStr = this._PackagingOperationsService.GetAQLStrByLevel(this.cmbAQLStandard.SelectedValue);
            //this.lblAQLStandardDescInfo.Text = aqlStr.Split(',')[0];
            //this.lblSamplesNum.Text = aqlStr.Split(',')[1];
            //this.lblRejectionNum.Text = aqlStr.Split(',')[2];

            InitPageInfoByComboBox(this.cmbAQLStandard);
        }

        //箱号回车
        private void txtCartonNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                dataGrid1.CurrentCellChanged -= dataGrid1_CurrentCellChanged;
                for (int i = 0; i < DataCount; i++)
                {
                    if (this.dataGrid1[i, 1].ToString().ToUpper().Equals(this.txtCartonNo.Text.Trim().ToUpper()))
                    {
                        this.dataGrid1[i, 0] = "√";
                    }
                    else
                    {
                        this.dataGrid1[i, 0] = "";
                    }
                }
                dataGrid1.CurrentCellChanged += dataGrid1_CurrentCellChanged;
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
            iqcNo = this.cmbIQCNo.Text;
            string rejectionNum = this.lblRejectionNum.Text;
            bool checkType = true;
            if (this.rdbFullCheck.Checked)
            {
                checkType = true;
            }
            else
            {
                checkType = false;
            }

            if (!checkType && string.IsNullOrEmpty(cmbAQLStandard.SelectedItem.ToString()))
            {
                this.lblMessage.Text = "请选择一个AQL标准！";
                return;
            }


            this.lblMessage.Text = this._PackagingOperationsService.IQCCheckResultMPSubmitReturnMessage(iqcNo, 
                checkType,cmbAQLStandard.SelectedItem.ToString(), 
                rejectionNum
                , ApplicationService.Current().LoginInfo.UserCode.ToUpper());
            if (this.lblMessage.Text == "提交成功")
            {
                DataTable dt = this._PackagingOperationsService.GetIQCCheckResultMPDataGrid(this.cmbIQCNo.Text);
                BindDataGrid(dt);
            }
        }

        //记录缺陷
        private void btnRecordNG_Click(object sender, EventArgs e)
        {
            string cartonNo = string.Empty;
            string dqMCode = string.Empty;
            int count = 0;
            for (int i = 0; i < DataCount; i++)
            {
                if (this.dataGrid1[i, 0].ToString() == "√")
                {
                    cartonNo = dataGrid1[i, 1].ToString();
                    dqMCode = dataGrid1[i, 4].ToString();
                    count++;
                }
            }
            if (count != 1)
            {
                this.lblMessage.Text = "请选择一条数据";
                return;
            }
            else
            {
                this.lblMessage.Text = "";
            }
            FIQCNGRecordForm fIQCNGRecordForm = new FIQCNGRecordForm(this.cmbIQCNo.Text, cartonNo, dqMCode);
            fIQCNGRecordForm.Show();
        }

        //返回
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);

        }

        #endregion

        #region 自定义方法
        private bool ValidateInput()
        {
            iqcNo = this.cmbIQCNo.Text;

            if (string.IsNullOrEmpty(iqcNo))
            {
                //提示信息
                this.lblMessage.Text = "请选择IQC检验单";
                return false;
            }
            else
            {
                this.lblMessage.Text = "";
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
                else if (dt.Columns[i].ColumnName == "鼎桥料号")
                {
                    ColStyle.Width = 150;
                }
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
            int total = 0;
            for (int i = 0; i < DataCount; i++)
            {
                string qtyStr = dataGrid1[i, 2].ToString();
                if (!string.IsNullOrEmpty(qtyStr))
                    total += int.Parse(qtyStr);

            }
            lblTotal.Text = total.ToString();
            ts = null;
            GC.Collect();//回收一下有好处的
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            dataGrid1.Select(dataGrid1.CurrentRowIndex);
            if (dataGrid1[dataGrid1.CurrentRowIndex, 0].ToString() == "√")
            {
                dataGrid1[dataGrid1.CurrentRowIndex, 0] = "";
            }
            else
            {
                dataGrid1[dataGrid1.CurrentRowIndex, 0] = "√";
            }
        }

        #endregion

        private void txtCartonnoCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\r')
                return;
            string[] iqcNos = _PackagingOperationsService.GetIQCNoFromCartonno(txtCartonnoCode.Text);
            this.cmbIQCNo.Items.Clear();
            this.cmbIQCNo.Items.Add(string.Empty);
            foreach (string s in iqcNos)
                this.cmbIQCNo.Items.Add(s);
            if (this.cmbIQCNo.Items.Count > 0)
                this.cmbIQCNo.SelectedIndex = 0;
        }

        //add by sam 
        //private void cmbDqMcode_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(this.cmbDqMcode.Text))
        //    {
        //        return;
        //    }
        //    //InitAQLStandard();
        //    DataTable dt = this._PackagingOperationsService.GetIQCCheckResultMPDataGrid(this.cmbIQCNo.Text, this.cmbDqMcode.Text);
        //    BindDataGrid(dt);
        //}

    }
}
