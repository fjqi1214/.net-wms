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
    public partial class FIQCExceptionPicUpload : UserControl
    {
        PackagingOperationsService.PackagingOperationsService _PackagingOperationsService;
        private string IQCNo = "";
        private Form parent;
        private int DataCount = 0;

        public FIQCExceptionPicUpload(string iqcNo, Form parent)
        {
            if (parent == null)
            {
                throw new Exception("必须有一个父窗口！");
            }
            this.parent = parent;
            InitializeComponent();
            this.IQCNo = iqcNo;

            _PackagingOperationsService = new BenQGuru.eMES.WinCeClient.PackagingOperationsService.PackagingOperationsService();
            _PackagingOperationsService.Url = WebServiceFacade.GetWebServiceURL() + "PackagingOperationsService.asmx";

            DataTable dt = _PackagingOperationsService.GetDataGridDoc(iqcNo);
            //this.dataGrid1.DataSource = dt;
            //this.DataCount = dt.Rows.Count;
            BindDataGrid(dt);
        }

        //图片上传
        private void btnUpPic_Click(object sender, EventArgs e)
        {
            DialogResult r = this.openFileDialog1.ShowDialog();
            if (r != DialogResult.OK) return;

            string fileName = this.openFileDialog1.FileName;

            //string fileName = "111.jpg";
            System.IO.FileStream fs = new FileStream(fileName, FileMode.Open);
            byte[] bs = new byte[fs.Length];
            //MessageBox.Show(bs.Length.ToString());
            int count = fs.Read(bs, 0, (int)fs.Length);
            fs.Close();

            string userCode = ApplicationService.Current().LoginInfo.UserCode.ToUpper();
            bool isTrue = _PackagingOperationsService.UploadFile(bs, this.IQCNo, userCode);
            if (isTrue)
            {
                this.lblMessage.Text = "上传成功";
                BindDataGrid(_PackagingOperationsService.GetDataGridDoc(this.IQCNo));
            }
            else
            {
                this.lblMessage.Text = "上传失败";
            }
        }

        //删除
        private void btnDelete_Click(object sender, EventArgs e)
        {
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
            _PackagingOperationsService.DeleteDoc(fileName.ToArray());
            this.lblMessage.Text = "删除成功！";
            BindDataGrid(_PackagingOperationsService.GetDataGridDoc(this.IQCNo));
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
                    ColStyle.Width = 40;//当宽度等于0的时候就可以隐藏这列
                }
                else if (dt.Columns[i].ColumnName == "SERIAL") //如果某个条件满足就执行该列是否隐藏
                {
                    ColStyle.Width = 0;//当宽度等于0的时候就可以隐藏这列
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
                dataGrid1.TableStyles.Clear();
                this.dataGrid1.TableStyles.Add(ts);
            }
            this.dataGrid1.DataSource = dt;

            DataCount = dt.Rows.Count;
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

    }
}
