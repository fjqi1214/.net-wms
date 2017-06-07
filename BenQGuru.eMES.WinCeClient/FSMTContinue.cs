using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BenQGuru.eMES.WinCeClient.SMTLoadService;
using System.Collections;

namespace BenQGuru.eMES.WinCeClient
{
    public partial class FSMTContinue : UserControl
    {
        SMTLoadService.SMTLoadService smtLoadService;
        string usercode = ApplicationService.Current().LoginInfo.UserCode.ToUpper();
        string rescode = ApplicationService.Current().LoginInfo.Resource.ToUpper();
        bool stationTableGroupActive = false;

        public FSMTContinue()
        {
            InitializeComponent();
            smtLoadService = new BenQGuru.eMES.WinCeClient.SMTLoadService.SMTLoadService();
            smtLoadService.Url = WebServiceFacade.GetWebServiceURL() + "SMTLoadService.asmx";
        } 

        // 工单回车
        /// <summary>
        /// 工单回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMocode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.txtMocode.Text.Trim() == string.Empty)
                {
                    this.txtmessage.Text = "请输入工单代码";
                    return;
                }
                this.txtMachineCode.Focus();
                this.txtMachineCode.SelectAll();
            }
        }

        // 机台编码
        /// <summary>
        /// 机台编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMachineCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar=='\r')
            {
                if(this.txtMachineCode.Text.Trim()==string.Empty)
                {
                    this.txtmessage.Text = "请输入机台编码";
                    this.txtMachineCode.Focus();
                    this.txtMachineCode.SelectAll();
                    return;
                }
                if (this.txtMocode.Text.Trim() == string.Empty)
                {
                    this.txtmessage.Text = "请输入工单代码";
                    this.txtMachineCode.Focus();
                    this.txtMachineCode.SelectAll();
                    return;
                }
                bool istrue = InitTableGroup(false);
                if (istrue == true)
                {
                    this.txtStationCode.Focus();
                    this.txtStationCode.SelectAll();
                }
            }
        }

        // Table 回车
        /// <summary>
        /// Table 回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>  
        private void cboStationTable_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r')
                {
                    if (this.cboStationTable.Text.Trim() == string.Empty)
                    {
                        this.txtmessage.Text = "请输入Table";
                        return;
                    }
                    this.txtStationCode.Focus();
                    this.txtStationCode.SelectAll();
                }
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message, Enums.WinCE_MsgBox_Title_Tips);            
            }
        }         

        // 站位
        /// <summary>
        /// 站位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtStationCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                if (e.KeyChar == '\r')
                {
                    if (this.txtStationCode.Text.Trim() == string.Empty)
                    {
                        this.txtmessage.Text = "请输入站位";
                        return;
                    }
                    this.txtOldReelNo.Focus();
                    this.txtOldReelNo.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Enums.WinCE_MsgBox_Title_Tips);
            }
        }

        // 原有料卷编号回车
        /// <summary>
        /// 原有料卷编号回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOldReelNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r')
                {
                    if (this.txtOldReelNo.Text.Trim() == string.Empty)
                    {
                        this.txtmessage.Text = "请输入原有料卷编号";
                        return;
                    }
                    this.txtReelNo.Focus();
                    this.txtReelNo.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Enums.WinCE_MsgBox_Title_Tips);
            }
        }

        // 料卷号回车
        /// <summary>
        /// 料卷号回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtReelNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r')
                {
                    string Mocode = this.txtMocode.Text.Trim().ToUpper();
                    string MachineCode = this.txtMachineCode.Text.Trim().ToUpper();
                    string StationTable = this.cboStationTable.Text.Trim().ToUpper();
                    string ReelNo = this.txtReelNo.Text.Trim().ToUpper();
                    string OldReelNo = this.txtOldReelNo.Text.Trim().ToUpper();
                    string StationCode = this.txtStationCode.Text.Trim().ToUpper();
                   
                    if (Mocode == string.Empty)
                    {
                        this.txtmessage.Text = "请输入工单代码";
                        this.txtMocode.Focus();
                        this.txtMocode.SelectAll();
                        return;
                    }
                    if (MachineCode == string.Empty)
                    {
                        this.txtmessage.Text = "请输入机台编码";
                        this.txtMachineCode.Focus();
                        this.txtMachineCode.SelectAll();
                        return;
                    }
                    if (StationTable == string.Empty)
                    {
                        this.txtmessage.Text = "请输入Table";
                        this.cboStationTable.Focus();
                        this.cboStationTable.SelectAll();
                        return;
                    }
                    if (StationCode == string.Empty)
                    {
                        this.txtmessage.Text = "请输入站位";
                        this.txtStationCode.Focus();
                        this.txtStationCode.SelectAll();
                        return;
                    }
                    if (OldReelNo == string.Empty)
                    {
                        this.txtmessage.Text = "请输入原有料卷编号";
                        this.txtOldReelNo.Focus();
                        this.txtStationCode.SelectAll();
                        return;
                    }
                    if (ReelNo == string.Empty)
                    {
                        this.txtmessage.Text = "请输入料卷编号";
                        this.txtReelNo.Focus();
                        this.txtReelNo.SelectAll();
                        return;
                    }

                    string[] res = new CeHelper().JsonToStrs(smtLoadService.SMTContinue(Mocode, MachineCode, StationTable,OldReelNo, ReelNo, StationCode, usercode, rescode));
                    string message = string.Empty;
                    if (res.Length > 0)
                    {
                        message = res[0];
                    }
                    //直接调用WEBSERVICE                              
                    if (message.Contains("OK"))
                    {
                        this.txtmessage.SuccessMsgAndPlaySound(message.Substring(message.IndexOf(' ') + 1));
                        this.txtReelNo.Text = "";
                        this.txtStationCode.Focus();
                        this.txtStationCode.SelectAll();
                        this.txtStationCode.SelectAll();
                        this.txtStationCode.Text = "";
                        this.txtOldReelNo.Text = "";

                    }
                    else
                    {
                        this.txtmessage.Text = message.Substring(message.IndexOf(' ') + 1);
                         if (message.Contains("PDA_Old_ReelNo"))
                        {
                            this.txtOldReelNo.Focus();
                            this.txtOldReelNo.SelectAll();
                        }
                        else if (message.Contains("PDA_ReelNo"))
                        {
                            this.txtReelNo.Focus();
                            this.txtReelNo.SelectAll();
                        }
                        else if (message.Contains("PDA_FeederCode"))
                        {
                            this.txtOldReelNo.Focus();
                            this.txtOldReelNo.SelectAll();
                        }
                        else if (message.Contains("PDA_MachineStation"))
                        {
                            this.txtStationCode.Focus();
                            this.txtStationCode.SelectAll();
                        }
                        else if (message.Contains("PDA_MoCode"))
                        {
                            this.txtMocode.Focus();
                            this.txtMocode.SelectAll();
                        }
                        else
                        {
                            this.txtStationCode.Focus();
                            this.txtStationCode.SelectAll();
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Enums.WinCE_MsgBox_Title_Tips);
            }

        }

        // 退出
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        // 清空
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.txtMocode.Text = "";
            this.txtMocode.Focus();
            this.txtMocode.SelectAll();
            this.txtMachineCode.Text = "";
            this.cboStationTable.Items.Clear();
            this.txtReelNo.Text = "";
            this.txtOldReelNo.Text = "";
            this.txtStationCode.Text = "";
        }

        private void FSMTContinue_Resize(object sender, EventArgs e)
        {
            this.txtMocode.Focus();
            this.txtMocode.SelectAll();
        }

        // 加载Table下拉框数据源
        /// <summary>
        /// 加载Table下拉框数据源
        /// </summary>
        /// <param name="isBindingCboGrp"></param>
        private bool InitTableGroup(bool isBindingCboGrp)
        {

            // 查询Table组次
            string[] strTableGrp = smtLoadService.GetSMTFeederMatrialTableGroup(this.txtMocode.Text.Trim().ToUpper(), rescode, txtMachineCode.Text.Trim().ToUpper());
            if (strTableGrp != null)
            {
                if (strTableGrp[0].Contains("PDA_MoCode"))
                {
                    this.txtmessage.Text = strTableGrp[0].Substring(strTableGrp[0].IndexOf(' ') + 1);
                    this.txtMocode.Focus();
                    this.txtMocode.SelectAll();
                    return false;
                }
            }

            bool bUpdateTableGrp = true;
            if (strTableGrp != null && strTableGrp.Length == this.cboStationTable.Items.Count)
            {
                bUpdateTableGrp = false;
                for (int i = 0; i < strTableGrp.Length; i++)
                {
                    if (strTableGrp[i] != this.cboStationTable.Items[i].ToString())
                    {
                        bUpdateTableGrp = true;
                        break;
                    }
                }
            }

            if (bUpdateTableGrp == true)
            {
                this.cboStationTable.Items.Clear();
                if (strTableGrp != null && strTableGrp.Length > 0)
                {
                    for (int i = 0; i < strTableGrp.Length; i++)
                    {
                        this.cboStationTable.Items.Add(strTableGrp[i]);
                    }
                }
                else
                {
                    this.cboStationTable.Items.Add(string.Empty);
                }
            }
            if (isBindingCboGrp == false)
            {
                this.cboStationTable.SelectedIndex = 0;
            }
            this.cboStationTable.SelectedIndex = 0;
            this.cboStationTable.Enabled = true;
            stationTableGroupActive = false;
            string strGrp = smtLoadService.GetActiveStationTable(this.txtMocode.Text.Trim().ToUpper(), rescode, this.txtMachineCode.Text.Trim().ToUpper());
            for (int i = 0; i < this.cboStationTable.Items.Count; i++)
            {
                if (this.cboStationTable.Items[i].ToString() == strGrp)
                {
                    this.cboStationTable.SelectedIndex = i;
                    stationTableGroupActive = true;
                    break;
                }
            }

            if (this.cboStationTable.Items.Count <= 1)
            {
                this.cboStationTable.Enabled = false;
                stationTableGroupActive = true;
            }
            return true;
        }

        // Table下拉框选择
        /// <summary>
        /// Table下拉框选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboStationTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.txtMachineCode.Text.Trim() == string.Empty)
            {
                this.txtmessage.Text = "请输入机台编码";
                this.txtMachineCode.Focus();
                this.txtMachineCode.SelectAll();
                return;
            }
            if (this.txtMocode.Text.Trim() == string.Empty)
            {
                this.txtmessage.Text = "请输入工单代码";
                this.txtMachineCode.Focus();
                this.txtMachineCode.SelectAll();
                return;
            }
            string strGrp = smtLoadService.GetActiveStationTable(this.txtMocode.Text.Trim().ToUpper(), rescode, this.txtMachineCode.Text.Trim().ToUpper());
            if (this.cboStationTable.Text.ToString() == strGrp)
            {
                stationTableGroupActive = true;
            }
            else
            {
                stationTableGroupActive = false;
            }
            this.txtStationCode.Focus();
            this.txtStationCode.SelectAll();
        }

   
      

       
    }
}
