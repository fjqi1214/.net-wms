using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
//using BenQGuru.eMES.Material;
//using BenQGuru.eMES.Web.Helper;
//using BenQGuru.eMES.MOModel;
//using BenQGuru.eMES.Domain.Warehouse;
//using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.WinCeClient
{
    public partial class FLocStorTransMP : UserControl
    {
        //PickDone.PickDone PickDoneService;
        LocStorTrans.LocStorTrans LocStorTransService;
        //private int rows = 0;
        //private string IsKeyParts = string.Empty;
        //private WarehouseFacade _WarehouseFacade = null;
        //private string usercode = ApplicationService.Current().LoginInfo.UserCode.ToUpper();
        public FLocStorTransMP()
        {
            InitializeComponent();
            LocStorTransService = new BenQGuru.eMES.WinCeClient.LocStorTrans.LocStorTrans();
            LocStorTransService.Url = WebServiceFacade.GetWebServiceURL() + "LocStorTrans.asmx";
            //InitDropList();
            this.rdoAllCarton.Checked = true;//整箱

        }

       #region
        //private void InitDropList()
        //{
        //    this.cboPickNo.Items.Clear();
        //    DataTable dt = PickDoneService.QueryPickNo();
        //    this.cboPickNo.Items.Add("");
        //    if (dt != null)
        //    {
        //        for(int i=0;i<dt.Rows.Count;i++)
        //        this.cboPickNo.Items.Add(dt.Rows[i][0].ToString());
        //    }
            
        //}

        //private void txtCartonNo_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == '\r')
        //    {
        //        if (string.IsNullOrEmpty(this.cboPickNo.Text))
        //        {
        //            MessageBox.Show("请先选择拣货任务令号");
        //            this.cboPickNo.Focus();
        //            return;
        //        }
        //if (string.IsNullOrEmpty(this.txtCartonNo.Text.Trim()))
        //{
        //    MessageBox.Show("请输入箱号");
        //    this.txtCartonNo.Focus();
        //    return;
        //}
        //        string IsKeyParts = PickDoneService.GetKeyPartsInfo(this.txtCartonNo.Text.ToUpper());
        //        if (IsKeyParts != "TRUE" && IsKeyParts != "FALSE")
        //        {
        //            MessageBox.Show(IsKeyParts);
        //            return;
        //        }
        //        else if (IsKeyParts == "TRUE")
        //        {
        //            this.txtNumEdit.Enabled = false;
        //        }
        //        else
        //        {
        //            this.txtSNEdit.Enabled = false;
        //        }
        //        if (this.rdoAllCarton.Checked)
        //        {
        //            this.btnSubmit_Click(null, null);
        //        }
        //        else
        //        {
        //            if(IsKeyParts == "TRUE")
        //                this.txtSNEdit.Focus();
        //            else
        //                this.txtNumEdit.Focus();
        //        }
        //        this.lblMessage.Text = "XXX";
        //    }
        //}

        //private void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(this.cboPickNo.Text))
        //    {
        //        MessageBox.Show("请先选择拣货任务令号");
        //        this.cboPickNo.Focus();
        //        return;
        //    }
        //    int re;
        //    if (!string.IsNullOrEmpty(this.txtNumEdit.Text))
        //    {
        //        try
        //        {
        //            re = Int32.Parse(this.txtNumEdit.Text.Trim());
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("数量只能输入大于0的数字");
        //            return;
        //        }
        //        if (re <= 0)
        //        {
        //            MessageBox.Show("数量只能输入大于0的数字");
        //            return;
        //        }
        //    }
           
        //    if (string.IsNullOrEmpty(this.txtCartonNo.Text.ToUpper()) && string.IsNullOrEmpty(this.txtSNEdit.Text.ToUpper()))
        //    {
        //        MessageBox.Show("必须输入箱号或SN");
        //        return;
        //    }
        //    string CartonNo = this.txtCartonNo.Text.ToUpper();
        //    string PickNo = this.cboPickNo.Text.ToUpper();
        //    string SN = this.txtSNEdit.Text.ToUpper();
        //    string Number = this.txtNumEdit.Text.ToUpper();
        //    bool ISALL = true;
        //    if (this.rdoSplitCarton.Checked)
        //        ISALL = false;
        //    else
        //        ISALL = true;
        //    string result = PickDoneService.CheckInOutRule(PickNo, CartonNo, Number, SN, ISALL, true);
        //    if (result != "OK")
        //    {
        //        if (result == "箱号违反先进先出规则")
        //        {
        //            if (MessageBox.Show("此箱号违反先进先出规则，是否继续？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
        //                return;
        //        }
        //        else
        //        {
        //            this.lblMessage.Text = result;
        //            return;
        //        }
        //    }
        //    string UserCode = ApplicationService.Current().LoginInfo.UserCode.ToUpper();

        //    result=PickDoneService.SubmitButton(PickNo,CartonNo,Number,SN,UserCode,ISALL,false);
        //    this.lblMessage.Text = result;
        //    this.txtCartonNo.Text = string.Empty;
        //    this.txtNumEdit.Text = string.Empty;
        //    //this.lblMessage.Text = "XXX";
        //    this.txtNumEdit.Enabled = true;
        //    this.txtSNEdit.Enabled = true;
        //    LoadGrid();
            
        //}

        //private void btnInOut_Click(object sender, EventArgs e)
        //{
        //     //DataTable dt = new DataTable();
        //    //dt.Columns.Add("DQMCODE", typeof(string));
        //    if (string.IsNullOrEmpty(this.cboPickNo.Text))
        //    {
        //        MessageBox.Show("请先选择拣货任务令号");
        //        this.cboPickNo.Focus();
        //        return;
        //    }
        //    int j=0;
        //    string DQMCode = string.Empty;
        //    for (int i = 0; i < rows; i++)
        //    {
        //        if (this.dataGrid1[i, 0].ToString() == "√")
        //        {
        //            j += 1;
        //            DQMCode = this.dataGrid1[i, 1].ToString();
        //        }
        //    }
        //    if (j == 0)
        //    {
        //        MessageBox.Show("请勾选一笔记录进行查看");
        //        return;
        //    }
        //    if (j > 1)
        //    {
        //        MessageBox.Show("查看先进先出只能勾选一笔物料");
        //        return;
        //    }
        //    ApplicationService.Current().MaterInfo = new MaterInfo(this.cboPickNo.Text.ToUpper(), DQMCode);
        //    try
        //    {
        //        Assembly assembly = null;
        //        string typeName = "BenQGuru.eMES.WinCeClient.FInOutView";

        //        if (assembly == null)
        //        {
        //            assembly = Assembly.Load("BenQGuru.eMES.WinCeClient");
        //        }
        //        object obj = assembly.CreateInstance(typeName);
        //        if (obj == null)
        //        {
        //            MessageBox.Show("对象创建失败" + typeName);
        //        }


        //        if (obj is UserControl)
        //        {
        //            //this.Parent.Controls.Clear();
        //            UserControl uc = obj as UserControl;

        //            (obj as FInOutView).fm = this;

        //            uc.Dock = DockStyle.Fill;
        //            uc.BackColor = Color.White;
        //            this.Visible = false;
        //            this.Parent.Controls.Add(uc);
        //            this.Text = "FInOutView";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
           
        //    //PickDoneService.GetInOutRule(DQMCode, this.cboPickNo.SelectedText.ToUpper());
        //}
      

        //private void btnView_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(this.cboPickNo.Text))
        //    {
        //        MessageBox.Show("请先选择拣货任务令号");
        //        this.cboPickNo.Focus();
        //        return;
        //    }
        //    int j = 0;
        //    string DQMCode = string.Empty;
        //    for (int i = 0; i < rows; i++)
        //    {
        //        if (this.dataGrid1[i, 0].ToString() == "√")
        //        {
        //            j += 1;
        //            DQMCode = this.dataGrid1[i, 1].ToString();
        //        }
        //    }
        //    if (j == 0)
        //    {
        //        MessageBox.Show("请勾选一笔记录进行查看");
        //        return;
        //    }
        //    if (j > 1)
        //    {
        //        MessageBox.Show("查看先进先出只能勾选一笔物料");
        //        return;
        //    }
        //    ApplicationService.Current().MaterInfo = new MaterInfo(this.cboPickNo.Text.ToUpper(), DQMCode);
        //    try
        //    {
        //        Assembly assembly = null;
        //        string typeName = "BenQGuru.eMES.WinCeClient.FPickedView";

        //        if (assembly == null)
        //        {
        //            assembly = Assembly.Load("BenQGuru.eMES.WinCeClient");
        //        }
        //        object obj = assembly.CreateInstance(typeName);
        //        if (obj == null)
        //        {
        //            MessageBox.Show("对象创建失败" + typeName);
        //        }


        //        if (obj is UserControl)
        //        {
        //            //this.Parent.Controls.Clear();
        //            UserControl uc = obj as UserControl;

        //            (obj as FPickedView).fm = this;

        //            uc.Dock = DockStyle.Fill;
        //            uc.BackColor = Color.White;
        //            this.Visible = false;
        //            this.Parent.Controls.Add(uc);
        //            this.Text = "FPickedView";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void btnReturn_Click(object sender, EventArgs e)
        //{
        //    this.Visible = false;
        //}

        //private void btnApply_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(this.cboPickNo.Text))
        //    {
        //        MessageBox.Show("请先选择拣货任务令号");
        //        this.cboPickNo.Focus();
        //        return;
        //    }
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("PickNo", typeof(string));
        //    dt.Columns.Add("PickLine", typeof(string));
        //  //  int j = 0;
        // //   string DQMCode = string.Empty;
        //    for (int i = 0; i < rows; i++)
        //    {
        //        if (this.dataGrid1[i, 0].ToString() == "√")
        //        {
        //            dt.Rows.Add(this.dataGrid1[i, 5].ToString(), this.dataGrid1[i, 6].ToString());
        //        }
        //    }
        //    for (int j = 0; j < dt.Rows.Count; j++)
        //    {
        //        string result = PickDoneService.ApplyButton(dt.Rows[j][0].ToString(), dt.Rows[j][1].ToString(), ApplicationService.Current().LoginInfo.UserCode.ToUpper());
        //        this.lblMessage.Text = result;
        //    }
        //}

        //private void cboPickNo_TextChanged(object sender, EventArgs e)
        //{
        //    LoadGrid();
        //}
        //private void LoadGrid()
        //{
        //    string PickNo = this.cboPickNo.Text.ToUpper();
        //    DataTable dt = new DataTable();
        //    if (!string.IsNullOrEmpty(this.cboPickNo.Text))
        //    {
        //        dt = PickDoneService.PickNOQueryGrid(PickNo);
        //    }
        //    this.dataGrid1.DataSource = dt;
        //    rows = dt.Rows.Count;
        //}

        //private void rdoAllCarton_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (this.rdoAllCarton.Checked)
        //    {
        //        this.rdoSplitCarton.Checked = false;
        //    }
        //    else
        //    {
        //        this.rdoSplitCarton.Checked = true;
        //    }
        //}

        //private void rdoSplitCarton_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (this.rdoSplitCarton.Checked)
        //    {
        //        this.rdoAllCarton.Checked = false;
        //    }
        //    else
        //    {
        //        this.rdoAllCarton.Checked = true;
        //    }
        //}

        //private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == '\r')
        //    {
        //        this.btnSubmit_Click(null,null);
        //    }
        //}

        //private void txtSN_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == '\r')
        //    {
        //        this.btnSubmit_Click(null, null);
        //    }
        //}

        #endregion

       #region 创建
        private void btnCreate_Click(object sender, EventArgs e)
        {
            string newIqcNo = LocStorTransService.CreateAutoDocmentsNo();
            txtLocationNoEdit.Text = newIqcNo;
        }
        #endregion

       #region 新增
        private string  AddStorLoc( )
        {
            string newIqcNo =  txtLocationNoEdit.Text.Trim();

            string fromCarton = txtOriginalCartonEdit.Text.Trim();
            string result = LocStorTransService.Add(ApplicationService.Current().LoginInfo.UserCode.ToUpper(), newIqcNo, fromCarton);
           if (result != null)
           {
               return result;
           }
            return "";
        }
       #endregion

        #region 提交
        private void btnSubmit_Click(object sender, EventArgs e)
        {
           string msg= AddStorLoc();

            if (msg != "新增成功")
            {
                this.lblMessage.Text = msg;
                return;
            }

            #region check
            string transNo = txtLocationNoEdit.Text.Trim();
            //string dqmCode = "";// txtDQMoCodeEdit.Text.Trim();

            string tLocationCode = txtTLocationCodeEdit.Text.Trim();//目标货位
            string fromCarton = txtOriginalCartonEdit.Text.Trim();//原箱号 FromCARTONNO 
            string tcarton = txtTLocationCartonEdit.Text.Trim();//目标箱号
            string inputsn = txtSNEdit.Text.Trim();//SN
            string  inputqty = txtNumEdit.Text.Trim();//数量
            string statusList = "";
            if (this.rdoAllCarton.Checked)
            {
                statusList = "AllCarton"; //CartonType.CartonType_AllCarton;
            }
            else
            {
                statusList ="SplitCarton";//= CartonType.CartonType_SplitCarton;
            }
            #endregion 

            string userCode = ApplicationService.Current().LoginInfo.UserCode.ToUpper();
            string result = LocStorTransService.Commit_Click(userCode, statusList, transNo, tLocationCode,
                fromCarton, tcarton, inputsn, inputqty);

           
            if (result == "提交成功")
            {
              
                txtNumEdit.Text = string.Empty;
                txtSNEdit.Text = string.Empty;
                txtTLocationCartonEdit.Text = string.Empty;
            }
            this.lblMessage.Text = result;
        }
        #endregion

        #region 查看
        private void btnView_Click(object sender, EventArgs e)
        {
           #region
            //查看按钮：点击跳转到图6.13.4：货位移动清单PDA介面
            string transNo = txtLocationNoEdit.Text.Trim().ToUpper();

            string locationCode = txtTLocationCodeEdit.Text.Trim();//目标货位
            string fromCartonno = txtOriginalCartonEdit.Text.Trim();//原箱号 FromCARTONNO 
            string cartonno = txtTLocationCartonEdit.Text.Trim();//目标箱号
            string dqmCode = LocStorTransService.GetDqmCode(fromCartonno);// txtDQMoCodeEdit.Text.Trim().ToUpper();

            try
            {
 
                FLocStorTransDetailMP fLocStorTransDetailMp = new FLocStorTransDetailMP(transNo, dqmCode, fromCartonno, locationCode, cartonno);
                this.Visible = false;
                this.Parent.Controls.Add(fLocStorTransDetailMp);
              
                #region
                //Assembly assembly = null;
                //string typeName = "BenQGuru.eMES.WinCeClient.FLocStorTransDetailMP";

                //if (assembly == null)
                //{
                //    assembly = Assembly.Load("BenQGuru.eMES.WinCeClient");
                //}
                ////object obj = Activator.CreateInstance(typeName); 
                //object obj = assembly.CreateInstance(typeName);
                //if (obj == null)
                //{
                //    MessageBox.Show("对象创建失败" + typeName);
                //}
                //if (obj is UserControl)
                //{
                //    UserControl uc = obj as UserControl;
                //    this.Visible = false;
                //    this.Parent.Controls.Add(uc);
                //}
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                    #endregion
        }
        #endregion

        #region 返回
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            //this.Visible = false;
        }
        #endregion

        #region _CheckedChanged

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
            if (this.rdoAllCarton.Checked)
            {
                txtNumEdit.Text = "";
                txtSNEdit.Text = "";
                txtNumEdit.ReadOnly = true;
                txtSNEdit.ReadOnly = true;
            }
            else
            {
                txtNumEdit.ReadOnly = false;
                txtSNEdit.ReadOnly = false;
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

            if (this.rdoAllCarton.Checked)
            {
                txtNumEdit.Text = "";
                txtSNEdit.Text = "";
                txtNumEdit.ReadOnly = true;
                txtSNEdit.ReadOnly = true;
            }
            else
            {
                txtNumEdit.ReadOnly = false;
                txtSNEdit.ReadOnly = false;
            }
        }

        #endregion

        #region KeyPress

        private void txtNumEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.btnSubmit_Click(null, null);
            }
        }

        private void txtOriginalCartonEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.btnSubmit_Click(null, null);
            }
        }

        private void txtSNEdit_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == '\r')
            {
                this.btnSubmit_Click(null, null);
            }
        }

        #endregion
    }
}
