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
    public partial class FReceiveASNMP : UserControl
    {
        ASNReceiveService.ASNReceiveService asnService;
        private int DataCount = 0;
        UserControl command = null;
        #region 初始化，回车事件
        private FUploadPicture up = null;

        public FReceiveASNMP()
        {
            InitializeComponent();
            asnService = new BenQGuru.eMES.WinCeClient.ASNReceiveService.ASNReceiveService();
            asnService.Url = WebServiceFacade.GetWebServiceURL() + "ASNReceiveService.asmx";


            BindResultComBox();
            ClearAll();

        }
        public FReceiveASNMP(string asn, UserControl IQCCommand)
        {
            InitializeComponent();
            asnService = new BenQGuru.eMES.WinCeClient.ASNReceiveService.ASNReceiveService();
            asnService.Url = WebServiceFacade.GetWebServiceURL() + "ASNReceiveService.asmx";
            if (string.IsNullOrEmpty(asn))
            {
                throw new Exception(asn + "asn号码为空");
            }
            this.command = IQCCommand;
            txtASNCode.Text = asn;
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;
            BindResultComBox();

            if (!asnService.CheckASNReceiveStatus(txtASNCode.Text))
            {
                MessageBox.Show("ASN状态必须是初检！"); return;

            }

            BenQGuru.eMES.WinCeClient.ASNReceiveService.AsnSimple simple = asnService.GetAsnStatus(asn);
            foreach (string key in giveReasons.Keys)
            {
                if (key == simple.GiveReason)
                    cmbGiveinResult.Text = giveReasons[key];
            }


            foreach (string key in rejectReasons.Keys)
            {
                if (key == simple.RejectReason)
                    cmbRejectResult.Text = rejectReasons[key];

            }
            txtRejectCount.Text = simple.RejectCount.ToString();
            BindGrid(false);



        }

        private void BindGrid(bool istrail)
        {

            string emergency = asnService.GetEmergency(this.txtASNCode.Text);
            if (!string.IsNullOrEmpty(emergency) && emergency.ToUpper() == "Y")
            {
                this.chkEmergency.Visible = true;
                this.lblGivenResult.Visible = true;
                this.cmbGiveinResult.Visible = true;
                this.btnGivenin.Visible = true;
            }
            else
            {
                this.chkEmergency.Visible = false;
                this.lblGivenResult.Visible = false;
                this.cmbGiveinResult.Visible = false;
                this.btnGivenin.Visible = false;
            }


            DataTable dt1 = asnService.GetDataGrid(txtASNCode.Text, istrail);


            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = dt1.TableName;

            DataGridColumnStyle ColStyle1 = new DataGridTextBoxColumn();
            ColStyle1.MappingName = dt1.Columns[0].ColumnName.ToString();
            ColStyle1.HeaderText = "选择";
            ColStyle1.Width = 20;
            ts.GridColumnStyles.Add(ColStyle1);


            DataGridColumnStyle ColStyle2 = new DataGridTextBoxColumn();
            ColStyle2.MappingName = dt1.Columns[1].ColumnName.ToString();
            ColStyle2.HeaderText = "箱号编码";
            ts.GridColumnStyles.Add(ColStyle2);

            DataGridColumnStyle ColStyle3 = new DataGridTextBoxColumn();
            ColStyle3.MappingName = dt1.Columns[2].ColumnName.ToString();
            ColStyle3.Width = 150;
            ColStyle3.HeaderText = "鼎桥料号";
            ts.GridColumnStyles.Add(ColStyle3);

            DataGridColumnStyle ColStyle4 = new DataGridTextBoxColumn();
            ColStyle4.MappingName = dt1.Columns[3].ColumnName.ToString();
            ColStyle4.HeaderText = "供应商物料";
            ColStyle4.Width = 50;
            ts.GridColumnStyles.Add(ColStyle4);




            DataGridColumnStyle ColStyle5 = new DataGridTextBoxColumn();
            ColStyle5.MappingName = dt1.Columns[4].ColumnName.ToString();
            ColStyle5.HeaderText = "管控类型";
            ColStyle5.Width = 50;
            ts.GridColumnStyles.Add(ColStyle5);

            DataGridColumnStyle ColStyle6 = new DataGridTextBoxColumn();
            ColStyle6.MappingName = dt1.Columns[5].ColumnName.ToString();
            ColStyle6.HeaderText = "来料数量";
            ColStyle6.Width = 40;
            ts.GridColumnStyles.Add(ColStyle6);

            DataGridColumnStyle ColStyle7 = new DataGridTextBoxColumn();
            ColStyle7.MappingName = dt1.Columns[6].ColumnName.ToString();
            ColStyle7.HeaderText = "行号";
            ColStyle7.Width = 0;
            ts.GridColumnStyles.Add(ColStyle7);

            this.dataGrid1.TableStyles.Clear();
            this.dataGrid1.TableStyles.Add(ts);

            this.DataCount = dt1.Rows.Count;
            lblCartonNum.Text = this.DataCount.ToString();

            this.dataGrid1.DataSource = dt1;
            RefreshQty();
        }


        //SN回车
        private void txtSN_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar != '\r')
                return;


            if (string.IsNullOrEmpty(this.txtASNCode.Text))
            {
                //提示信息
                MessageBox.Show("请选择入库指令号");
                return;
            }
            if (string.IsNullOrEmpty(this.txtSN.Text.Trim()))
            {
                MessageBox.Show("请输入SN编号");
                return;
            }

            BenQGuru.eMES.WinCeClient.ASNReceiveService.StNoLine[] obj = asnService.QueryASNDetailSN(this.txtSN.Text.Trim(), txtASNCode.Text);
            if (obj == null || obj.Length == 0)
            {
                MessageBox.Show("没有项目！");
                return;
            }
            dataGrid1.CurrentCellChanged -= dataGrid1_CurrentCellChanged;

            for (int i = 0; i < this.DataCount; i++)
            {
                string stline = this.dataGrid1[i, 6].ToString();
                string cartonno = this.dataGrid1[i, 1].ToString();
                if (!string.IsNullOrEmpty(obj[0].StLine) && obj[0].StLine == stline && string.IsNullOrEmpty(cartonno))
                {
                    this.dataGrid1[i, 0] = "√";
                   
                    break;

                }
            }

            dataGrid1.CurrentCellChanged += dataGrid1_CurrentCellChanged;

        }

        //箱号编码回车
        private void txtCarton_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (string.IsNullOrEmpty(this.txtASNCode.Text))
                {
                    //提示信息
                    MessageBox.Show("请先选择入库指令号");
                    return;
                }
                if (string.IsNullOrEmpty(this.txtCarton.Text.Trim()))
                {
                    MessageBox.Show("请输入箱号编号");
                    return;
                }
                int count = 0;

                for (int i = 0; i < this.DataCount; i++)
                {
                    if (this.dataGrid1[i, 0].ToString() == "√")
                    {
                        count++;
                    }
                }


                if (count == 0)
                {
                    MessageBox.Show("请勾选且只勾选一条数据");
                    return;
                }
                if (count > 1)
                {
                    MessageBox.Show("只能勾选一条数据");
                    return;
                }

                string stline = "";
                for (int i = 0; i < this.DataCount; i++)
                {
                    if (this.dataGrid1[i, 0].ToString() == "√")
                    {
                        stline = this.dataGrid1[i, 6].ToString();
                    }
                }
                string message = string.Empty;
                bool result = asnService.BindCarton2STLine(stline, this.txtASNCode.Text, this.txtCarton.Text.Trim().ToUpper(), out message);

                if (result)
                {
                    BindGrid(false);
                    txtCarton.Text = string.Empty;
                    txtSN.Focus();
                }
                else
                {
                    MessageBox.Show(message);
                }


            }
        }

        #endregion

        #region button
        //拒收
        private void btnReject_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbRejectResult.SelectedItem.ToString()))
            {
                MessageBox.Show("拒收原因必填！");
                return;
            }
            if (string.IsNullOrEmpty(this.txtRejectCount.Text.Trim()))
            {
                MessageBox.Show("拒收数量必填！");
                return;
            }


            DataTable dt = new DataTable();
            dt.Columns.Add("Checked", typeof(string));
            dt.Columns.Add("STNO", typeof(string));
            dt.Columns.Add("STLINE", typeof(string));
            dt.TableName = "ExampleDataTable";
            for (int i = 0; i < DataCount; i++)
            {
                dt.Rows.Add(this.dataGrid1[i, 0], this.txtASNCode.Text, this.dataGrid1[i, 6]);
            }


            string rejectVal = string.Empty;
            foreach (string key in rejectReasons.Keys)
            {
                if (rejectReasons[key] == this.cmbRejectResult.SelectedItem.ToString())
                    rejectVal = key;
            }

            string result = asnService.RejectDetail(dt, this.txtASNCode.Text, rejectVal, txtRejectCount.Text);


            MessageBox.Show(result);
            BindGrid(false);

        }

        //接收
        private void btnReceive_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Checked", typeof(string));
            dt.Columns.Add("STNO", typeof(string));
            dt.Columns.Add("STLINE", typeof(string));
            dt.TableName = "ExampleDataTable";

            for (int i = 0; i < DataCount; i++)
            {
                dt.Rows.Add(this.dataGrid1[i, 0], this.txtASNCode.Text, this.dataGrid1[i, 6]);

            }

            try
            {
                string result = asnService.ReceiveDetail(dt, this.txtASNCode.Text, cmbRejectResult.SelectedItem.ToString(), ApplicationService.Current().LoginInfo.UserCode.ToUpper());
                BindGrid(false);
                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        //让步接收
        private void btnGivenin_Click(object sender, EventArgs e)
        {
            if (!chkEmergency.Checked)
            {
                MessageBox.Show("必须是紧急物料才能让步接收！");
                return;
            }

            if (string.IsNullOrEmpty(cmbGiveinResult.SelectedItem.ToString()))
            {
                MessageBox.Show("让步接收原因必填！");
                return;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("Checked", typeof(string));
            dt.Columns.Add("STNO", typeof(string));
            dt.Columns.Add("STLINE", typeof(string));
            dt.TableName = "ExampleDataTable";
            for (int i = 0; i < DataCount; i++)
            {

                dt.Rows.Add(this.dataGrid1[i, 0], this.txtASNCode.Text, this.dataGrid1[i, 6]);

            }
            int rejectCount = 0;


            if (!string.IsNullOrEmpty(txtRejectCount.Text))
            {
                try
                {
                    rejectCount = int.Parse(this.txtRejectCount.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("请输入正确的数字格式");
                    return;
                }
            }

            string giveVal = string.Empty;
            foreach (string key in giveReasons.Keys)
            {
                if (giveReasons[key] == this.cmbGiveinResult.SelectedItem.ToString())
                    giveVal = key;
            }
            string result = asnService.GiveinDetail(dt, this.txtASNCode.Text, giveVal);
            MessageBox.Show(result);
            BindGrid(false);

        }

        //上传图片
        private void btnUpPic_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtASNCode.Text))
            {
                MessageBox.Show("必须输入入库指令号!"); return;
            }

            FPicture f = new FPicture(this.txtASNCode.Text);

            f.Show();

        }

        FPicture Fk = null;

        //返回
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (this.command != null)
                this.command.Visible = true;
            this.Dispose();

        }

        #endregion

        #region 自定义方法

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
                    ColStyle.Width = 30;//当宽度等于0的时候就可以隐藏这列
                }
                if (dt.Columns[i].ColumnName == "STLINE") //如果某个条件满足就执行该列是否隐藏
                {
                    ColStyle.Width = 0;//当宽度等于0的时候就可以隐藏这列
                }
                ts.GridColumnStyles.Add(ColStyle);
            }

            if (ts.GridColumnStyles.Count > 0)
            {
                //将样式和控件绑定到一起
                dataGrid1.TableStyles.Clear();
                this.dataGrid1.TableStyles.Add(ts);
            }
            this.dataGrid1.DataSource = dt;

            DataCount = dt.Rows.Count;
            ts = null;
            ClearAll();
        }

        public Dictionary<string, string> rejectReasons = new Dictionary<string, string>();
        public Dictionary<string, string> giveReasons = new Dictionary<string, string>();

        //绑定下拉框
        private void BindResultComBox()
        {
            //绑定拒收原因
            BenQGuru.eMES.WinCeClient.ASNReceiveService.ComBoxValue[] strs = asnService.QueryResult("REJECTRESULT");
            this.cmbRejectResult.Items.Clear();
            if (strs != null)
            {
                this.cmbRejectResult.Items.Add(string.Empty);
                foreach (BenQGuru.eMES.WinCeClient.ASNReceiveService.ComBoxValue s in strs)
                {
                    this.cmbRejectResult.Items.Add(s.Text);
                    rejectReasons.Add(s.Value, s.Text);
                }
            }
            else
            {
                this.cmbRejectResult.Items.Add(string.Empty);
            }
            this.cmbRejectResult.SelectedIndex = 0;

            //绑定让步接收原因
            strs = asnService.QueryResult("GIVEINRESULT");
            this.cmbGiveinResult.Items.Clear();
            if (strs != null)
            {
                this.cmbGiveinResult.Items.Add(string.Empty);
                foreach (BenQGuru.eMES.WinCeClient.ASNReceiveService.ComBoxValue s in strs)
                {
                    this.cmbGiveinResult.Items.Add(s.Text);
                    giveReasons.Add(s.Value, s.Text);
                }
            }
            else
            {
                this.cmbGiveinResult.Items.Add(string.Empty);
            }
            this.cmbGiveinResult.SelectedIndex = 0;
        }

        ////剩余待检全部拒收
        //protected string AllReject()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("CHECK", typeof(string));
        //    dt.Columns.Add("STNO", typeof(string));
        //    dt.Columns.Add("STLINE", typeof(string));
        //    for (int i = 0; i < DataCount; i++)
        //    {
        //        dt.Rows.Add(this.dataGrid1[i, 0], this.txtASNCode.Text, this.dataGrid1[i, 6]);
        //    }
        //    dt.TableName = "ExampleDataTable";
        //    string result = asnService.RejectDetail(dt, this.txtASNCode.Text, this.cmbRejectResult.SelectedText);
        //    return result;
        //}

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

        //刷新lbl纪录
        protected void RefreshQty()
        {
            int[] str = asnService.GetASN(this.txtASNCode.Text);
            this.lblCartonNum.Text = this.DataCount.ToString();
            if (str != null)
            {
                this.lblRejectNum.Text = str[0].ToString();
                this.lblActNum.Text = (DataCount - str[0]).ToString();
                this.lblReceiveNum.Text = (str[1] + str[2]).ToString();
                this.lblGiveinNum.Text = str[2].ToString();
            }

        }

        private void ClearAll()
        {
            this.txtSN.Text = string.Empty;
            this.txtCarton.Text = string.Empty;

            this.txtRejectCount.Text = string.Empty;
            this.txtRejectCount.Tag = 0;
            this.cmbGiveinResult.SelectedIndex = 0;
            this.cmbRejectResult.SelectedIndex = 0;
            this.lblCartonNum.Text = string.Empty;
            this.lblRejectNum.Text = string.Empty;
            this.lblActNum.Text = string.Empty;
            this.lblReceiveNum.Text = string.Empty;
            this.lblGiveinNum.Text = string.Empty;

        }
        #endregion

        private void txtASNCode_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar != '\r')
                return;

            if (!asnService.CheckASNReceiveStatus(txtASNCode.Text))
            {
                MessageBox.Show("ASN状态必须是初检！"); return;

            }

            BindGrid(false);
        }



        private void button1_Click(object sender, EventArgs e)
        {
            dataGrid1.CurrentCellChanged -= dataGrid1_CurrentCellChanged;
            for (int i = 0; i < DataCount; i++)
            {
                if (dataGrid1[i, 0].ToString() == "√")
                {
                    dataGrid1[i, 0] = "";
                }
                else
                {
                    dataGrid1[i, 0] = "√";
                }

            }
            dataGrid1.CurrentCellChanged += dataGrid1_CurrentCellChanged;

        }

        private void btnCancelCartonno_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtASNCode.Text))
            {
                //提示信息
                MessageBox.Show("请先选择入库指令号");
                return;
            }


            List<string> lines = new List<string>();
            for (int i = 0; i < this.DataCount; i++)
            {
                if (this.dataGrid1[i, 0].ToString() == "√")
                {
                    lines.Add(this.dataGrid1[i, 6].ToString());
                }
            }


            if (lines.Count == 0)
            {
                MessageBox.Show("请勾选一条数据！");
                return;
            }
            asnService.CancelCartonno(this.txtASNCode.Text, lines.ToArray());
            txtSN.Text = string.Empty;
            txtCarton.Text = string.Empty;
            BindGrid(false);
            txtSN.Focus();

        }

        private void chktrailcase_CheckStateChanged(object sender, EventArgs e)
        {
            if (chktrailcase.Checked)
                BindGrid(true);
            else
                BindGrid(false);

        }


    }
}
