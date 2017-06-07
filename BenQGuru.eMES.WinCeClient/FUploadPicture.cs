using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;


namespace BenQGuru.eMES.WinCeClient
{
    public partial class FUploadPicture : UserControl
    {
        ASNReceiveService.ASNReceiveService asnService;
        public string STNo = "";
        private int DataCount = 0;
        private Form parent;
        //public FUploadPicture()
        //{
        //    InitializeComponent();
        //    asnService = new BenQGuru.eMES.WinCeClient.ASNReceiveService.ASNReceiveService();
        //    asnService.Url = WebServiceFacade.GetWebServiceURL() + "ASNReceiveService.asmx";


        //}

        public FUploadPicture(string stno, Form parent)
        {
            if (parent == null)
                throw new Exception("必须有一个父窗口！");
            this.parent = parent;
            InitializeComponent();
            this.STNo = stno;
            asnService = new BenQGuru.eMES.WinCeClient.ASNReceiveService.ASNReceiveService();
            asnService.Url = WebServiceFacade.GetWebServiceURL() + "ASNReceiveService.asmx";
            DataTable dt = asnService.GetDataGridDoc(stno);
            this.dataGrid1.DataSource = dt;
            this.DataCount = dt.Rows.Count;
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

        private void rdbRejectPic_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRejectPic.Checked)
            {
                rdbGiveinPic.Checked = false;
                BindDataGrid(asnService.GetDataGridDoc(STNo));
            }
        }

        private void rdbGiveinPic_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbGiveinPic.Checked)
            {
                rdbRejectPic.Checked = false;
                BindDataGrid(asnService.GetDataGridDoc(STNo));
            }
        }

        //上传图片
        private void btnUpPic_Click(object sender, EventArgs e)
        {
            DialogResult r = this.openFileDialog1.ShowDialog();
            if (r != DialogResult.OK) return;

            string fileName = this.openFileDialog1.FileName;

             byte[] bs;
             using (System.IO.FileStream fs = new FileStream(fileName, FileMode.Open))
             {
                 bs = new byte[fs.Length];
                 //MessageBox.Show(bs.Length.ToString());
                 int count = fs.Read(bs, 0, (int)fs.Length);
                 if (count <= 0)
                 {
                     MessageBox.Show("读取的文件长度为0"); return;
                 }
             }
            string type = string.Empty;
            if (this.rdbGiveinPic.Checked)
            {
                type = "InitGivein";
            }
            else if (this.rdbRejectPic.Checked)
            {
                type = "InitReject";
            }
            else
            {
                MessageBox.Show("必须选择图片类型！"); return;
            }
            string userCode = ApplicationService.Current().LoginInfo.UserCode.ToUpper();
            asnService.UploadFile(bs, this.STNo, type, userCode);
        }



        //删除
        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool isSelect = false;
            DataTable dt = new DataTable();
            List<string> fileName = new List<string>();
            int count = 0;
            for (int i = 0; i < DataCount; i++)
            {
                if (this.dataGrid1[i, 0].ToString() == "√")
                {
                    fileName.Add(this.dataGrid1[i, 1].ToString());
                    count++;
                }
            }
            if (count == 0)
            {
                this.lblMessage.Text = "请至少勾选一条数据";
                return;
            }
            asnService.DeleteDoc(fileName.ToArray());
            this.lblMessage.Text = "删除成功！";
            BindDataGrid(asnService.GetDataGridDoc(STNo));
        }

        //返回
        private void btnReturn_Click(object sender, EventArgs e)
        {
            parent.Close();
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
                    ColStyle.Width = 30;//当宽度等于0的时候就可以隐藏这列
                }
                if (dt.Columns[i].ColumnName == "SERIAL") //如果某个条件满足就执行该列是否隐藏
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
            GC.Collect();//回收一下有好处的
        }

        private delegate void DoSomething();

    }
}
