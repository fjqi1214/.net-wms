using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BenQGuru.eMES.WinCeClient;

namespace BenQGuru.eMES.WinCeClient
{
    public partial class FIQCCommand : UserControl
    {

        Dictionary<string, string> statusDic = new Dictionary<string, string>();
        private int rows = 0;
        public FIQCCommand()
        {
            InitializeComponent();

            DataTable dt = new DataTable();
            dt.Columns.Add(" ", typeof(string));
            dt.Columns.Add("ASN", typeof(string));
            dt.Columns.Add("STATUS", typeof(string));
            dt.Columns.Add("POSITION", typeof(string));
            dt.Columns.Add("SAPNO", typeof(string));
            dt.Columns.Add("ISEMERGENCY", typeof(string));
            dt.Columns.Add("DIRECTFLAG", typeof(string));

            dt = stub.GetCommand(string.Empty, string.Empty, string.Empty);
            rows = dt.Rows.Count;




            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = dt.TableName;

            DataGridColumnStyle ColStyle1 = new DataGridTextBoxColumn();
            ColStyle1.MappingName = dt.Columns[0].ColumnName.ToString();
            ColStyle1.HeaderText = "选择";
            ColStyle1.Width = 20;
            ts.GridColumnStyles.Add(ColStyle1);

            //DataGridColumnStyle ColStyle2 = new DataGridTextBoxColumn();
            //ColStyle2.MappingName = dt.Columns[1].ColumnName.ToString();
            //ColStyle2.HeaderText = "STLINE";
            //ts.GridColumnStyles.Add(ColStyle2);

            DataGridColumnStyle ColStyle3 = new DataGridTextBoxColumn();
            ColStyle3.MappingName = dt.Columns[1].ColumnName.ToString();
            ColStyle3.HeaderText = "入库指令";
            ts.GridColumnStyles.Add(ColStyle3);

            DataGridColumnStyle ColStyle4 = new DataGridTextBoxColumn();
            ColStyle4.MappingName = dt.Columns[2].ColumnName.ToString();
            ColStyle4.HeaderText = "状态";
            ts.GridColumnStyles.Add(ColStyle4);

            DataGridColumnStyle ColStyle5 = new DataGridTextBoxColumn();
            ColStyle5.MappingName = dt.Columns[3].ColumnName.ToString();
            ColStyle5.HeaderText = "入库库位";
            ts.GridColumnStyles.Add(ColStyle5);


            DataGridColumnStyle ColStyle6 = new DataGridTextBoxColumn();
            ColStyle6.MappingName = dt.Columns[4].ColumnName.ToString();
            ColStyle6.HeaderText = "SAP单据";
            ts.GridColumnStyles.Add(ColStyle6);

            DataGridColumnStyle ColStyle7 = new DataGridTextBoxColumn();
            ColStyle7.MappingName = dt.Columns[5].ColumnName.ToString();
            ColStyle7.HeaderText = "紧急物料";
            ts.GridColumnStyles.Add(ColStyle7);

            DataGridColumnStyle ColStyle8 = new DataGridTextBoxColumn();
            ColStyle8.MappingName = dt.Columns[6].ColumnName.ToString();
            ColStyle8.HeaderText = "供应商直发";
            ts.GridColumnStyles.Add(ColStyle8);

            this.dataGrid1.TableStyles.Clear();
            this.dataGrid1.TableStyles.Add(ts);


            statusDic.Add("待收货", "WaitReceive");
            statusDic.Add("到货初检中", "Receive");
            statusDic.Add("初检拒收", "ReceiveRejection");
            statusDic.Add("IQC检验中", "IQC");
            statusDic.Add("IQC拒收", "IQCRejection");
            statusDic.Add("上架中", "OnLocation");

            comboBox1.Items.Add(string.Empty);
            comboBox2.Items.Add(string.Empty);
            foreach (DataRow row in dt.Rows)
                comboBox1.Items.Add(row["ASN"].ToString());


            statusDic.Add(string.Empty, string.Empty);
            foreach (string s in statusDic.Keys)
                comboBox2.Items.Add(s);
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = stub.GetCommand(this.comboBox1.SelectedItem != null ? this.comboBox1.SelectedItem.ToString() : string.Empty, this.comboBox2.SelectedItem != null ? this.statusDic[this.comboBox2.SelectedItem.ToString()] : string.Empty, txtInvNo.Text);
            rows = dt.Rows.Count;


            this.dataGrid1.DataSource = dt;


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = stub.GetCommand(this.comboBox1.SelectedItem != null ? this.comboBox1.SelectedItem.ToString() : string.Empty, this.comboBox2.SelectedItem != null ? this.statusDic[this.comboBox2.SelectedItem.ToString()] : string.Empty, txtInvNo.Text);
            rows = dt.Rows.Count;
            this.dataGrid1.DataSource = dt;
        }

        private void btnCancelDown_Click(object sender, EventArgs e)
        {

            List<string> asnList = new List<string>();

            for (int i = 0; i < rows; i++)
            {
                if (this.dataGrid1[i, 0].ToString() == "√")
                {
                    if (this.dataGrid1[i, 6].ToString().ToUpper() != "Y")
                    {
                        MessageBox.Show(this.dataGrid1[i, 1].ToString() + "入库指令是供应商直发，不能做以下操作[取消下发][初检][申请IQC]");
                        return;
                    }
                    asnList.Add(this.dataGrid1[i, 1].ToString());
                }
            }

            if (asnList.Count > 0)
            {
                string mes = stub.CancelDown(asnList.ToArray());
                MessageBox.Show(mes);
            }
            else
            {
                MessageBox.Show("请选择要取消的下发指令！");
            }
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

        private void btnFirstCheck_Click(object sender, EventArgs e)
        {
            List<string> asnList = new List<string>();

            for (int i = 0; i < rows; i++)
            {
                if (this.dataGrid1[i, 0].ToString() == "√")
                {
                    asnList.Add(this.dataGrid1[i, 1].ToString());
                }
            }
            if (asnList.Count > 1)
            {
                MessageBox.Show("只能选择一条记录！");
                return;
            }
            if (asnList.Count == 0)
            {
                MessageBox.Show("请选择需要初检的指令号！");
                return;
            }

            string message = string.Empty;
            bool result = stub.GetFirstCheck(asnList.ToArray(), out message);
            if (result)
            {
                this.Visible = false;
                FReceiveASNMP f = new FReceiveASNMP(asnList[0], this);

                this.Parent.Controls.Add(f);


            }
            else
            {
                MessageBox.Show(message);
            }



        }

        private void btnApplyIQC_Click(object sender, EventArgs e)
        {

            List<string> asnList = new List<string>();

            for (int i = 0; i < rows; i++)
            {

                if (this.dataGrid1[i, 0].ToString() == "√")
                {

                    asnList.Add(this.dataGrid1[i, 1].ToString());
                }
            }
            if (asnList.Count == 0)
            {
                MessageBox.Show("请选择入库指令号！");
                return;
            }
            if (asnList.Count > 1)
            {
                MessageBox.Show("只能选择一条入库指令号！");
                return;
            }
            string[] asnArr = asnList.ToArray();


            string message = stub.SaveIQCInfo(asnArr, ApplicationService.Current().LoginInfo.UserCode.ToUpper());
            MessageBox.Show(message);

        }



        private void txtInvNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\r')
                return;
            DataTable dt = stub.GetCommand(this.comboBox1.SelectedItem != null ? this.comboBox1.SelectedItem.ToString() : string.Empty, this.comboBox2.SelectedItem != null ? this.statusDic[this.comboBox2.SelectedItem.ToString()] : string.Empty, txtInvNo.Text);
            rows = dt.Rows.Count;
            this.dataGrid1.DataSource = dt;

        }

        private void FIQCCommand_Click(object sender, EventArgs e)
        {

        }
    }

    class stub
    {
        private static BenQGuru.eMES.WinCeClient.IQCCommand.IQCCommand command;
        static stub()
        {

            command = new BenQGuru.eMES.WinCeClient.IQCCommand.IQCCommand();
            command.Url = WebServiceFacade.GetWebServiceURL() + "IQCCommand.asmx";
        }

        public static string CancelDown(string[] asns)
        {
            return command.CancelDownCommand(asns);
        }
        //public static DataTable GetCommand(string no, string statu)
        //{
        //    return new DataTable();
        //}

        public static DataTable GetCommand(string no, string statu, string invNo)
        {
            return command.GetCommand(no, statu, invNo, ApplicationService.Current().LoginInfo.UserCode.ToUpper());
        }

        public static bool GetFirstCheck(string[] asns, out string message)
        {
            return command.FirstCheckWithUser(asns, ApplicationService.Current().LoginInfo.UserCode.ToUpper(), out message);
        }

        public static string SaveIQCInfo(string[] asns, string usrcode)
        {
            return command.SaveIQCInfo(asns, usrcode);
        }
        public static string[] ValidateASNForIQC(string[] asns)
        {

            return command.ValidateASNStatusForIQC(asns);

        }


        public static string[] ValidateASNSTTypeForIQC(string[] asns)
        {
            return command.ValidateASNSTTypeForIQC(asns);
        }

    }
}
