using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace BenQGuru.eMES.WinCeClient
{
    public partial class FPickDone : UserControl
    {
        PickDone.PickDone PickDoneService;
        private int rows = 0;
        private string IsKeyParts = string.Empty;
        public FPickDone()
        {
            InitializeComponent();
            PickDoneService = new BenQGuru.eMES.WinCeClient.PickDone.PickDone();
            PickDoneService.Url = WebServiceFacade.GetWebServiceURL() + "PickDone.asmx";
            InitDropList();
            this.rdoAllCarton.Checked = true;

        }
        private void InitDropList()
        {
            this.cboPickNo.Items.Clear();
            string[] str = PickDoneService.QueryPickNo(ApplicationService.Current().LoginInfo.UserCode.ToUpper());
            this.cboPickNo.Items.Add("");
            if (str != null)
            {
                foreach (string s in str)
                {
                    this.cboPickNo.Items.Add(s);
                }
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    if (!string.IsNullOrEmpty(dt.Rows[i][0].ToString()))
                //    {
                //        this.cboPickNo.Items.Add(dt.Rows[i][0].ToString());
                //    }
                //}
            }

        }

        private void txtCartonNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (string.IsNullOrEmpty(this.cboPickNo.Text))
                {
                    MessageBox.Show("请先选择拣货任务令号");
                    this.cboPickNo.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.txtCartonNo.Text.Trim()))
                {
                    MessageBox.Show("请输入箱号");
                    this.txtCartonNo.Focus();
                    return;
                }
                string IsKeyParts = PickDoneService.GetKeyPartsInfo(this.txtCartonNo.Text.ToUpper());

                if (IsKeyParts == "TRUE")
                {
                    this.txtNumber.Enabled = false;
                    this.txtSN.Enabled = true;
                    txtNumber.Text = string.Empty;
                }
                else
                {

                    this.txtSN.Enabled = false;
                    this.txtNumber.Enabled = true;
                    this.txtSN.Text = string.Empty;

                }
                if (this.rdoAllCarton.Checked)
                {
                    this.btnSubmit_Click(null, null);
                }
                else
                {
                    if (IsKeyParts == "TRUE")
                        this.txtSN.Focus();
                    else
                        this.txtNumber.Focus();
                }

            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cboPickNo.Text))
            {
                MessageBox.Show("请先选择拣货任务令号");
                this.cboPickNo.Focus();
                return;
            }
            int re;
            if (!string.IsNullOrEmpty(this.txtNumber.Text))
            {
                try
                {
                    re = int.Parse(this.txtNumber.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("数量只能输入大于0的数字");
                    return;
                }
                if (re <= 0)
                {
                    MessageBox.Show("数量只能输入大于0的数字");
                    return;
                }
            }

            if (string.IsNullOrEmpty(this.txtCartonNo.Text.ToUpper()) && string.IsNullOrEmpty(this.txtSN.Text.ToUpper()))
            {
                MessageBox.Show("必须输入箱号或SN");
                return;
            }
            string CartonNo = this.txtCartonNo.Text.ToUpper();
            string PickNo = this.cboPickNo.Text.ToUpper();
            string SN = this.txtSN.Text.ToUpper();
            string Number = this.txtNumber.Text.ToUpper();
            bool ISALL = true;
            if (this.rdoSplitCarton.Checked)
                ISALL = false;
            else
                ISALL = true;
            string result = PickDoneService.CheckInOutRule(PickNo, CartonNo, Number, SN, ISALL, true);
            if (result != "OK")
            {
                if (result == "箱号违反先进先出规则")
                {
                    if (MessageBox.Show("此箱号违反先进先出规则，是否继续？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                        return;
                }
                else
                {
                    this.lblMessage.Text = result;
                    return;
                }
            }
            string UserCode = ApplicationService.Current().LoginInfo.UserCode.ToUpper();
            string message = string.Empty;
            bool r = PickDoneService.SubmitButton(PickNo, CartonNo, Number, SN, UserCode, ISALL, false, out message);
            //拣料成功后清空箱号；
            //拆箱拣料时，拣料成功后，箱号、数量和SN都清空；

            if (r)
            {

                if (!string.IsNullOrEmpty(txtSN.Text))
                    this.txtSN.Text = string.Empty;
                if (!string.IsNullOrEmpty(txtNumber.Text) || this.rdoAllCarton.Checked)
                    txtCartonNo.Text = string.Empty;

                this.txtNumber.Text = string.Empty;
                //this.lblMessage.Text = "XXX";


                LoadGrid();

            }

            this.lblMessage.Text = message;
        }

        private void ClearText()
        {
            this.txtCartonNo.Text = string.Empty;
            this.txtNumber.Text = string.Empty;
            //this.lblMessage.Text = "XXX";
            this.txtNumber.Enabled = true;
            this.txtSN.Enabled = true;
        }

        private void btnInOut_Click(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //dt.Columns.Add("DQMCODE", typeof(string));
            if (string.IsNullOrEmpty(this.cboPickNo.Text))
            {
                MessageBox.Show("请先选择拣货任务令号");
                this.cboPickNo.Focus();
                return;
            }
            int j = 0;
            string DQMCode = string.Empty;
            for (int i = 0; i < rows; i++)
            {
                if (this.dataGrid1[i, 0].ToString() == "√")
                {
                    j += 1;
                    DQMCode = this.dataGrid1[i, 2].ToString();
                }
            }
            if (j == 0)
            {
                MessageBox.Show("请勾选一笔记录进行查看");
                return;
            }
            if (j > 1)
            {
                MessageBox.Show("查看先进先出只能勾选一笔物料");
                return;
            }
            ApplicationService.Current().MaterInfo = new MaterInfo(this.cboPickNo.Text.ToUpper(), DQMCode);
            try
            {
                Assembly assembly = null;
                string typeName = "BenQGuru.eMES.WinCeClient.FInOutView";

                if (assembly == null)
                {
                    assembly = Assembly.Load("BenQGuru.eMES.WinCeClient");
                }
                object obj = assembly.CreateInstance(typeName);
                if (obj == null)
                {
                    MessageBox.Show("对象创建失败" + typeName);
                }


                if (obj is UserControl)
                {
                    //this.Parent.Controls.Clear();
                    UserControl uc = obj as UserControl;

                    (obj as FInOutView).fm = this;

                    uc.Dock = DockStyle.Fill;
                    uc.BackColor = Color.White;
                    this.Visible = false;
                    this.Parent.Controls.Add(uc);
                    this.Text = "FInOutView";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //PickDoneService.GetInOutRule(DQMCode, this.cboPickNo.SelectedText.ToUpper());
        }
        //private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        //{
        //    dataGrid1.Select(dataGrid1.CurrentRowIndex);
        //    if (dataGrid1[dataGrid1.CurrentRowIndex, 0].ToString() == "√")
        //    {
        //        dataGrid1[dataGrid1.CurrentRowIndex, 0] = "";
        //    }
        //    else
        //    {
        //        dataGrid1[dataGrid1.CurrentRowIndex, 0] = "√";
        //    }
        //}

        private void btnView_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cboPickNo.Text))
            {
                MessageBox.Show("请先选择拣货任务令号");
                this.cboPickNo.Focus();
                return;
            }
            int j = 0;
            string DQMCode = string.Empty;
            for (int i = 0; i < rows; i++)
            {
                if (this.dataGrid1[i, 0].ToString() == "√")
                {
                    j += 1;
                    DQMCode = this.dataGrid1[i, 2].ToString();
                }
            }
            if (j == 0)
            {
                MessageBox.Show("请勾选一笔记录进行查看");
                return;
            }
            if (j > 1)
            {
                MessageBox.Show("查看先进先出只能勾选一笔物料");
                return;
            }
            ApplicationService.Current().MaterInfo = new MaterInfo(this.cboPickNo.Text.ToUpper(), DQMCode);
            //FPickedView f = new FPickedView();
            //f.Show();

            try
            {
                Assembly assembly = null;
                string typeName = "BenQGuru.eMES.WinCeClient.FPickedView";

                if (assembly == null)
                {
                    assembly = Assembly.Load("BenQGuru.eMES.WinCeClient");
                }
                object obj = assembly.CreateInstance(typeName);
                if (obj == null)
                {
                    MessageBox.Show("对象创建失败" + typeName);
                }


                if (obj is UserControl)
                {
                    //this.Parent.Controls.Clear();
                    UserControl uc = obj as UserControl;

                    (obj as FPickedView).fm = this;

                    uc.Dock = DockStyle.Fill;
                    uc.BackColor = Color.White;
                    this.Visible = false;
                    this.Parent.Controls.Add(uc);
                    this.Text = "FPickedView";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {

            this.Parent.Controls.Clear();


        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cboPickNo.Text))
            {
                MessageBox.Show("请先选择拣货任务令号");
                this.cboPickNo.Focus();
                return;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("PickNo", typeof(string));
            dt.Columns.Add("PickLine", typeof(string));
            //  int j = 0;
            //   string DQMCode = string.Empty;
            for (int i = 0; i < rows; i++)
            {
                if (this.dataGrid1[i, 0].ToString() == "√")
                {
                    dt.Rows.Add(this.dataGrid1[i, 6].ToString(), this.dataGrid1[i, 7].ToString());
                }
            }
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                string result = PickDoneService.ApplyButton(dt.Rows[j][0].ToString(), dt.Rows[j][1].ToString(), ApplicationService.Current().LoginInfo.UserCode.ToUpper());
                this.lblMessage.Text = result;
            }
        }

        private void cboPickNo_TextChanged(object sender, EventArgs e)
        {
            LoadGrid();
            this.lblMessage.Text = string.Empty;
        }
        private void LoadGrid()
        {
            string PickNo = this.cboPickNo.Text.ToUpper();
            DataTable dt1 = new DataTable();
            if (!string.IsNullOrEmpty(this.cboPickNo.Text))
            {
                dt1 = PickDoneService.PickNOQueryGrid(PickNo);
            }
            //this.dataGrid1.DataSource = dt1;
            rows = dt1.Rows.Count;

            if (dt1.Rows.Count > 0)
            {

                DataGridTableStyle ts = new DataGridTableStyle();
                ts.MappingName = dt1.TableName;

                DataGridColumnStyle ColStyle1 = new DataGridTextBoxColumn();
                ColStyle1.MappingName = dt1.Columns[0].ColumnName.ToString();
                ColStyle1.HeaderText = "选择";
                ColStyle1.Width = 20;
                ts.GridColumnStyles.Add(ColStyle1);

                DataGridColumnStyle ColStyle2 = new DataGridTextBoxColumn();
                ColStyle2.MappingName = dt1.Columns[1].ColumnName.ToString();
                ColStyle2.HeaderText = "合并";
                ColStyle2.Width = 20;
                ts.GridColumnStyles.Add(ColStyle2);

                DataGridColumnStyle ColStyle3 = new DataGridTextBoxColumn();
                ColStyle3.MappingName = dt1.Columns[2].ColumnName.ToString();
                ColStyle3.HeaderText = "鼎桥物料号";
                ts.GridColumnStyles.Add(ColStyle3);

                DataGridColumnStyle ColStyle4 = new DataGridTextBoxColumn();
                ColStyle4.MappingName = dt1.Columns[3].ColumnName.ToString();
                ColStyle4.HeaderText = "华为物料号";
                ts.GridColumnStyles.Add(ColStyle4);

                DataGridColumnStyle ColStyle5 = new DataGridTextBoxColumn();
                ColStyle5.MappingName = dt1.Columns[4].ColumnName.ToString();
                ColStyle5.HeaderText = "需求数量";
                ts.GridColumnStyles.Add(ColStyle5);


                DataGridColumnStyle ColStyle6 = new DataGridTextBoxColumn();
                ColStyle6.MappingName = dt1.Columns[5].ColumnName.ToString();
                ColStyle6.HeaderText = "已拣数量";
                ts.GridColumnStyles.Add(ColStyle6);

                DataGridColumnStyle ColStyle7 = new DataGridTextBoxColumn();
                ColStyle7.MappingName = dt1.Columns[6].ColumnName.ToString();
                ColStyle7.HeaderText = "拣货任务令";
                ts.GridColumnStyles.Add(ColStyle7);

                DataGridColumnStyle ColStyle8 = new DataGridTextBoxColumn();
                ColStyle8.MappingName = dt1.Columns[7].ColumnName.ToString();
                ColStyle8.HeaderText = " ";
                ts.GridColumnStyles.Add(ColStyle8);

                this.dataGrid1.TableStyles.Clear();
                this.dataGrid1.TableStyles.Add(ts);

                this.dataGrid1.DataSource = dt1;
            }
        }

        private void rdoAllCarton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoAllCarton.Checked)
            {
                this.rdoSplitCarton.Checked = false;
            }
            else
            {
                this.rdoSplitCarton.Checked = true;
            }
        }

        private void rdoSplitCarton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoSplitCarton.Checked)
            {
                this.rdoAllCarton.Checked = false;
            }
            else
            {
                this.rdoAllCarton.Checked = true;
            }
        }

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.btnSubmit_Click(null, null);
            }
        }

        private void txtSN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.btnSubmit_Click(null, null);
            }
        }

        private void dataGrid1_CurrentCellChanged_1(object sender, EventArgs e)
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

        private void btnClosePick_Click(object sender, EventArgs e)
        {
            string pickNo = this.cboPickNo.Text.ToUpper();
            string result = PickDoneService.ClosePickButton(pickNo, ApplicationService.Current().LoginInfo.UserCode.ToUpper());
            this.lblMessage.Text = result;
        }

        private void btnSAP_Click(object sender, EventArgs e)
        {
            string dqmcode = string.Empty;
            for (int i = 0; i < rows; i++)
            {
                if (this.dataGrid1[i, 0].ToString() == "√")
                {
                    dqmcode = this.dataGrid1[i, 2].ToString();
                }

            }

            if (!string.IsNullOrEmpty(cboPickNo.SelectedItem.ToString()))
            {
                if (string.IsNullOrEmpty(dqmcode))
                {
                    MessageBox.Show("请选择一行查询！");
                    return;
                }
                FSAPInvoices sap = new FSAPInvoices(this, cboPickNo.SelectedItem.ToString(), dqmcode);
                sap.Dock = DockStyle.Fill;
                sap.BackColor = Color.White;
                this.Visible = false;
                this.Parent.Controls.Add(sap);
            }
        }

        private void txtCartonNo_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
