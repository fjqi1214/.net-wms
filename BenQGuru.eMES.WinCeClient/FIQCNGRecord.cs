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
    public partial class FIQCNGRecord : UserControl
    {
        PackagingOperationsService.PackagingOperationsService _PackagingOperationsService;

        private string IQCNo = string.Empty;
        private string CartonNo = string.Empty;
        private string _DQMCode = string.Empty;
        private Form parent;

        private string stNo = string.Empty;
        private string stLine = string.Empty;
        private string eCode = string.Empty;
        private string sn = string.Empty;

        private int DataCount = 0;

        #region 初始化，回车事件

        public FIQCNGRecord(string iqcNo, string cartonNo, string dqMCode, Form parent)
        {
            if (parent == null)
            {
                throw new Exception("必须有一个父窗口！");
            }
            this.parent = parent;
            InitializeComponent();

            this.IQCNo = iqcNo;
            this.CartonNo = cartonNo;
            this._DQMCode = dqMCode;

            _PackagingOperationsService = new BenQGuru.eMES.WinCeClient.PackagingOperationsService.PackagingOperationsService();
            _PackagingOperationsService.Url = WebServiceFacade.GetWebServiceURL() + "PackagingOperationsService.asmx";

            DataTable dt = new DataTable();
            dt = this._PackagingOperationsService.GetIQCNGRecordDataGrid(iqcNo);
            BindDataGrid(dt);

            InitDrpNGType();
            cmbNGType.SelectedIndexChanged += new EventHandler(cmbNGType_SelectedIndexChanged);

            this.txtSN.Text = string.Empty;
            this.txtNGQty.Text = string.Empty;
            this.txtMemo.Text = string.Empty;
            this.txtCartonNo.Text = cartonNo;

            KeypartsSet();
        }

        void cmbNGType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ngTypeDesc = this.cmbNGType.SelectedItem.ToString();
            string ngType = string.Empty;

            if (this.dicNGType.TryGetValue(ngTypeDesc, out ngType))
            {
                this.cmbNGDesc.Items.Clear();
                this.dicNGDESC.Clear();
                string[] sss = this._PackagingOperationsService.InitDrpNGDesc(ngType);
                foreach (string s in sss)
                {
                    string[] two = s.Split(',');
                    if (!dicNGDESC.ContainsKey(two[0]))
                    {
                        dicNGDESC.Add(two[0], two[1]);
                        cmbNGDesc.Items.Add(two[0]);
                    }
                    else
                    {
                        MessageBox.Show(two[0] + "已存在重复！");
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show(ngTypeDesc + "不存在！");
            }
        }

        private void KeypartsSet()
        {
            bool isKeyparts = this._PackagingOperationsService.isKeyparts(this._DQMCode);
            if (isKeyparts)
            {
                this.txtSN.Enabled = true;
                this.txtNGQty.Enabled = false;
            }
            else
            {
                this.txtSN.Enabled = false;
                this.txtNGQty.Enabled = true;
            }
        }

        private void Refesh()
        {
            this.cmbNGType.Enabled = true;
            this.cmbNGDesc.Enabled = true;

            KeypartsSet();

            this.cmbNGType.SelectedIndex = 0;
            this.cmbNGDesc.SelectedIndex = 0;
            this.txtSN.Text = string.Empty;
            this.txtNGQty.Text = string.Empty;
            this.txtMemo.Text = string.Empty;
        }
        private Dictionary<string, string> dicNGType = new Dictionary<string, string>();
        private Dictionary<string, string> dicNGDESC = new Dictionary<string, string>();
        //缺陷类型下拉框
        private void InitDrpNGType()
        {

            dicNGType.Clear();
            cmbNGType.Items.Clear();
            string[] types = _PackagingOperationsService.GetDrpNGType();
            foreach (string t in types)
            {
                string[] s = t.Split(',');
                if (!dicNGType.ContainsKey(s[0]))
                {
                    dicNGType.Add(s[0], s[1]);
                    this.cmbNGType.Items.Add(s[0]);
                }
                //else
                //{
                //    MessageBox.Show(s[0] + "缺陷类型有重复！");
                //    return;
                //}


            }

        
        }

        //缺陷描述下拉框
        //private void InitDrpNGDesc()
        //{
        //    this.cmbNGDesc.Items.Add("");
        //    this.cmbNGDesc.Items.Add("缺陷描述1");
        //    this.cmbNGDesc.Items.Add("缺陷描述2");
        //    this.cmbNGDesc.Items.Add("缺陷描述3");

        //    this.cmbNGDesc.SelectedIndex = 0;
        //}

        #endregion

        #region button
        //保存
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateInput())
                return;
            
            int count = 0;
            for (int i = 0; i < DataCount; i++)
            {
                if (this.dataGrid1[i, 0].ToString() == "√")
                {
                    count++;
                }
            }

            bool isAdd = false;
            if (count == 0)
            {
                isAdd = true;
                this.lblMessage.Text = "";
            }
            else if (count == 1)
            {
                stNo = dataGrid1[dataGrid1.CurrentRowIndex, 7].ToString();
                stLine = dataGrid1[dataGrid1.CurrentRowIndex, 8].ToString();
                eCode = dataGrid1[dataGrid1.CurrentRowIndex, 3].ToString();
                sn = dataGrid1[dataGrid1.CurrentRowIndex, 4].ToString();
                this.lblMessage.Text = "";
            }
            else
            {
                this.lblMessage.Text = "只能勾选一条数据";
                return;
            }

            string ngType = dicNGType[this.cmbNGType.Text];
            string ngDesc = dicNGDESC[this.cmbNGDesc.Text];
            string _sn = this.txtSN.Text.Trim().ToUpper();
            string ngQty = this.txtNGQty.Text.Trim();
            string memo = this.txtMemo.Text.Trim();
            string cartonNo = this.txtCartonNo.Text.Trim().ToUpper();
            string userCode = ApplicationService.Current().UserCode.ToUpper();

            this.lblMessage.Text = this._PackagingOperationsService.IQCNGRecordSaveReturnMessage(isAdd, this.IQCNo, this._DQMCode, stNo, stLine, eCode, sn, ngType, ngDesc, _sn, ngQty, memo, cartonNo, userCode);
            if (this.lblMessage.Text == "保存成功")
            {
                DataTable dt = new DataTable();
                dt = this._PackagingOperationsService.GetIQCNGRecordDataGrid(this.IQCNo);
                BindDataGrid(dt);
                Refesh();
            }
        }

        //图片上传
        private void btnPicUpload_Click(object sender, EventArgs e)
        {
            FIQCExceptionPic fIQCExceptionPic = new FIQCExceptionPic(IQCNo);
            fIQCExceptionPic.Show();
        }

        //删除
        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<string> strs = new List<string>();
            int count = 0;
            for (int i = 0; i < DataCount; i++)
            {
                if (this.dataGrid1[i, 0].ToString() == "√")
                {
                    eCode = dataGrid1[i, 3].ToString();
                    stLine = dataGrid1[i, 8].ToString();
                    stNo = dataGrid1[i, 7].ToString();
                    sn = dataGrid1[i, 4].ToString();

                    strs.Add(eCode + "," + stLine + "," + stNo + "," + sn);
                    count++;
                }
            }
            if (count == 0)
            {
                this.lblMessage.Text = "请至少勾选一条数据";
                return;
            }
            else
            {
                this.lblMessage.Text = "";
            }
            this.lblMessage.Text = this._PackagingOperationsService.IQCNGRecordDeleteReturnMessage(IQCNo, strs.ToArray());
            if (this.lblMessage.Text == "删除成功")
            {
                DataTable dt = new DataTable();
                dt = this._PackagingOperationsService.GetIQCNGRecordDataGrid(IQCNo);
                BindDataGrid(dt);
                Refesh();
            }
        }

        //返回
        private void btnReturn_Click(object sender, EventArgs e)
        {
            parent.Close();
        }

        #endregion

        #region 自定义方法
        private bool ValidateInput()
        {
            string ngType = this.cmbNGType.Text;
            string ngDesc = this.cmbNGDesc.Text;
            string _sn = this.txtSN.Text.Trim();
            string ngQty = this.txtNGQty.Text.Trim();
            string memo = this.txtMemo.Text.Trim();
            string cartonNo = this.txtCartonNo.Text.Trim();

            if (string.IsNullOrEmpty(ngType))
            {
                //提示信息
                this.lblMessage.Text = "请选择缺陷类型";
                return false;
            }
            if (string.IsNullOrEmpty(ngDesc))
            {
                //提示信息
                this.lblMessage.Text = "请选择缺陷描述";
                return false;
            }
            //if (string.IsNullOrEmpty(_sn))
            //{
            //    //提示信息
            //    this.lblMessage.Text = "请输入SN";
            //    return false;
            //}
            //if (string.IsNullOrEmpty(ngQty))
            //{
            //    //提示信息
            //    this.lblMessage.Text = "请输入不良数";
            //    return false;
            //}
            //else
            //{
            //    try
            //    {
            //        decimal qty = decimal.Parse(ngQty);
            //        if (qty <= 0)
            //        {
            //            this.lblMessage.Text = "不良数必须为大于零的数字";
            //            return false;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        this.lblMessage.Text = "不良数必须为大于零的数字";
            //        return false;
            //    }
            //}

            //if (string.IsNullOrEmpty(memo))
            //{
            //    //提示信息
            //    this.lblMessage.Text = "请输入备注";
            //    return false;
            //}
            //if (string.IsNullOrEmpty(cartonNo))
            //{
            //    //提示信息
            //    this.lblMessage.Text = "请输入箱号";
            //    return false;
            //}

            this.lblMessage.Text = "";
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
                else if (dt.Columns[i].ColumnName == "ASN单号" || dt.Columns[i].ColumnName == "ASN单行项目") //如果某个条件满足就执行该列是否隐藏
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
                this.dataGrid1.TableStyles.Clear();
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

                this.cmbNGType.SelectedItem = "";
                this.cmbNGDesc.SelectedItem = "";
                this.txtSN.Text = "";
                this.txtNGQty.Text = "";
                this.txtMemo.Text = "";
                this.txtCartonNo.Text = "";

                this.cmbNGType.Enabled = true;
                this.cmbNGDesc.Enabled = true;
                this.txtSN.Enabled = true;
                this.txtNGQty.Enabled = true;
                this.txtCartonNo.Enabled = true;
            }
            else
            {
                dataGrid1[dataGrid1.CurrentRowIndex, 0] = "√";

                this.cmbNGType.SelectedItem = dataGrid1[dataGrid1.CurrentRowIndex, 2].ToString();
                this.cmbNGDesc.SelectedItem = dataGrid1[dataGrid1.CurrentRowIndex, 3].ToString();
                this.txtSN.Text = dataGrid1[dataGrid1.CurrentRowIndex, 4].ToString();
                this.txtNGQty.Text = dataGrid1[dataGrid1.CurrentRowIndex, 5].ToString();
                this.txtMemo.Text = dataGrid1[dataGrid1.CurrentRowIndex, 6].ToString();
                this.txtCartonNo.Text = dataGrid1[dataGrid1.CurrentRowIndex, 1].ToString();

                this.cmbNGType.Enabled = false;
                this.cmbNGDesc.Enabled = false;
                this.txtSN.Enabled = false;
                this.txtNGQty.Enabled = false;
                this.txtCartonNo.Enabled = false;
            }
        }

        #endregion

    }
}
