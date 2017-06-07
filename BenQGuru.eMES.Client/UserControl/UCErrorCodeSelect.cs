using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using BenQGuru.eMES.Domain.TSModel;

namespace UserControl
{
    /// <summary>
    /// UCErrorCodeSelect 的摘要说明。
    /// Laws Lu,2005/08/02,调整页面逻辑
    /// </summary>
    public class UCErrorCodeSelect : System.Windows.Forms.UserControl
    {
        public const string ClipOK = "GOOD";
        public const string ClipFail = "NG";

        private System.Windows.Forms.ListBox lstSelected;
        private System.Windows.Forms.ListBox lstSelect;
        private UserControl.UCLabelCombox cbxErrorGroup;
        private UserControl.UCButton btnAllRight;
        private UserControl.UCButton btnAllLeft;
        private ErrorCodeGroup2ErrorCode errorCodeGroup2ErrorCode;
        private System.Windows.Forms.Label labelUnSelectedErrorCode;
        private System.Windows.Forms.Label labelSelectedErrorCode;
        private System.Windows.Forms.TextBox txtInputErrorCode;
        private System.Windows.Forms.Label lblMessage;
        private UserControl.UCLabelEdit txtEndChar;
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;

        public event KeyPressEventHandler ErrorCodeGroupKeyPress = null;

        public event EventHandler EndErrorCodeInput = null;

        public event KeyPressEventHandler ErrorCodeKeyPress = null;

        public UCErrorCodeSelect()
        {
            // 该调用是 Windows.Forms 窗体设计器所必需的。
            InitializeComponent();

            this.txtEndChar.Value = ClipOK;

            cbxErrorGroup.Clear();
            lstSelect.Items.Clear();
            lstSelected.Items.Clear();
            errorCodeGroup2ErrorCode = new ErrorCodeGroup2ErrorCode();

            cbxErrorGroup.ComboBoxData.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbxErrorGroup.ComboBoxData.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbxErrorGroup_KeyPress);

        }

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码
        /// <summary> 
        /// 设计器支持所需的方法 - 不要使用代码编辑器 
        /// 修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAllRight = new UserControl.UCButton();
            this.btnAllLeft = new UserControl.UCButton();
            this.lstSelected = new System.Windows.Forms.ListBox();
            this.lstSelect = new System.Windows.Forms.ListBox();
            this.cbxErrorGroup = new UserControl.UCLabelCombox();
            this.labelUnSelectedErrorCode = new System.Windows.Forms.Label();
            this.labelSelectedErrorCode = new System.Windows.Forms.Label();
            this.txtInputErrorCode = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtEndChar = new UserControl.UCLabelEdit();
            this.SuspendLayout();
            // 
            // btnAllRight
            // 
            this.btnAllRight.BackColor = System.Drawing.SystemColors.Control;
            this.btnAllRight.ButtonType = UserControl.ButtonTypes.AllRight;
            this.btnAllRight.Caption = ">>";
            this.btnAllRight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAllRight.Location = new System.Drawing.Point(208, 101);
            this.btnAllRight.Name = "btnAllRight";
            this.btnAllRight.Size = new System.Drawing.Size(88, 22);
            this.btnAllRight.TabIndex = 3;
            this.btnAllRight.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnAllLeft
            // 
            this.btnAllLeft.BackColor = System.Drawing.SystemColors.Control;
            this.btnAllLeft.ButtonType = UserControl.ButtonTypes.AllLeft;
            this.btnAllLeft.Caption = "<<";
            this.btnAllLeft.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAllLeft.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAllLeft.Location = new System.Drawing.Point(208, 189);
            this.btnAllLeft.Name = "btnAllLeft";
            this.btnAllLeft.Size = new System.Drawing.Size(88, 22);
            this.btnAllLeft.TabIndex = 4;
            this.btnAllLeft.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lstSelected
            // 
            this.lstSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSelected.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstSelected.Location = new System.Drawing.Point(312, 96);
            this.lstSelected.Name = "lstSelected";
            this.lstSelected.Size = new System.Drawing.Size(184, 173);
            this.lstSelected.TabIndex = 5;
            this.lstSelected.DoubleClick += new System.EventHandler(this.lstSelected_DoubleClick);
            // 
            // lstSelect
            // 
            this.lstSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstSelect.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstSelect.Location = new System.Drawing.Point(8, 120);
            this.lstSelect.Name = "lstSelect";
            this.lstSelect.Size = new System.Drawing.Size(184, 147);
            this.lstSelect.TabIndex = 2;
            this.lstSelect.DoubleClick += new System.EventHandler(this.lstSelect_DoubleClick);
            // 
            // cbxErrorGroup
            // 
            this.cbxErrorGroup.AllowEditOnlyChecked = true;
            this.cbxErrorGroup.Caption = "不良代码组";
            this.cbxErrorGroup.Checked = false;
            this.cbxErrorGroup.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxErrorGroup.Location = new System.Drawing.Point(5, 8);
            this.cbxErrorGroup.Name = "cbxErrorGroup";
            this.cbxErrorGroup.SelectedIndex = -1;
            this.cbxErrorGroup.ShowCheckBox = false;
            this.cbxErrorGroup.Size = new System.Drawing.Size(285, 24);
            this.cbxErrorGroup.TabIndex = 0;
            this.cbxErrorGroup.WidthType = UserControl.WidthTypes.Long;
            this.cbxErrorGroup.XAlign = 90;
            this.cbxErrorGroup.SelectedIndexChanged += new System.EventHandler(this.cbxErrorGroup_SelectedIndexChanged);
            // 
            // labelUnSelectedErrorCode
            // 
            this.labelUnSelectedErrorCode.AutoSize = true;
            this.labelUnSelectedErrorCode.Location = new System.Drawing.Point(8, 64);
            this.labelUnSelectedErrorCode.Name = "labelUnSelectedErrorCode";
            this.labelUnSelectedErrorCode.Size = new System.Drawing.Size(77, 12);
            this.labelUnSelectedErrorCode.TabIndex = 6;
            this.labelUnSelectedErrorCode.Text = "待选不良代码";
            // 
            // labelSelectedErrorCode
            // 
            this.labelSelectedErrorCode.AutoSize = true;
            this.labelSelectedErrorCode.Location = new System.Drawing.Point(312, 72);
            this.labelSelectedErrorCode.Name = "labelSelectedErrorCode";
            this.labelSelectedErrorCode.Size = new System.Drawing.Size(77, 12);
            this.labelSelectedErrorCode.TabIndex = 7;
            this.labelSelectedErrorCode.Text = "已选不良代码";
            // 
            // txtInputErrorCode
            // 
            this.txtInputErrorCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtInputErrorCode.Location = new System.Drawing.Point(8, 88);
            this.txtInputErrorCode.Name = "txtInputErrorCode";
            this.txtInputErrorCode.Size = new System.Drawing.Size(184, 21);
            this.txtInputErrorCode.TabIndex = 1;
            this.txtInputErrorCode.Leave += new System.EventHandler(this.txtInputErrorCode_Leave);
            this.txtInputErrorCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInputErrorCode_KeyPress);
            this.txtInputErrorCode.Enter += new System.EventHandler(this.txtInputErrorCode_Enter);
            // 
            // lblMessage
            // 
            this.lblMessage.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblMessage.Location = new System.Drawing.Point(304, 8);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(200, 48);
            this.lblMessage.TabIndex = 8;
            // 
            // txtEndChar
            // 
            this.txtEndChar.AllowEditOnlyChecked = true;
            this.txtEndChar.AutoUpper = true;
            this.txtEndChar.BackColor = System.Drawing.Color.Gainsboro;
            this.txtEndChar.Caption = "采集命令";
            this.txtEndChar.Checked = false;
            this.txtEndChar.EditType = UserControl.EditTypes.String;
            this.txtEndChar.Location = new System.Drawing.Point(305, 8);
            this.txtEndChar.MaxLength = 10;
            this.txtEndChar.Multiline = false;
            this.txtEndChar.Name = "txtEndChar";
            this.txtEndChar.PasswordChar = '\0';
            this.txtEndChar.ReadOnly = false;
            this.txtEndChar.ShowCheckBox = true;
            this.txtEndChar.Size = new System.Drawing.Size(177, 24);
            this.txtEndChar.TabIndex = 285;
            this.txtEndChar.TabNext = true;
            this.txtEndChar.Value = "OK";
            this.txtEndChar.WidthType = UserControl.WidthTypes.Small;
            this.txtEndChar.XAlign = 382;
            // 
            // UCErrorCodeSelect
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.txtInputErrorCode);
            this.Controls.Add(this.labelSelectedErrorCode);
            this.Controls.Add(this.labelUnSelectedErrorCode);
            this.Controls.Add(this.btnAllRight);
            this.Controls.Add(this.btnAllLeft);
            this.Controls.Add(this.lstSelected);
            this.Controls.Add(this.lstSelect);
            this.Controls.Add(this.cbxErrorGroup);
            this.Name = "UCErrorCodeSelect";
            this.Size = new System.Drawing.Size(512, 280);
            this.Load += new System.EventHandler(this.UCErrorCodeSelect_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        [Bindable(true),
        Category("数据"),]
        public int Count
        {
            get
            {
                return lstSelected.Items.Count;
            }
        }

        [Bindable(true),
        Category("数据"),]
        public int ErrorGroupCount
        {
            get
            {
                return cbxErrorGroup.ComboBoxData.Items.Count;
            }
        }

        // Added by Icyer 02/06/2006
        private bool _canInput = true;
        [Bindable(true),
        Category("数据"),]
        public bool CanInput
        {
            get { return _canInput; }
            set
            {
                if (value == true && _canInput == false)
                {
                    lstSelect.Top += 28;
                    lstSelect.Height = lstSelected.Top + lstSelected.Height - lstSelect.Top;
                    txtInputErrorCode.Visible = true;
                    cbxErrorGroup.ComboBoxData.DropDownStyle = ComboBoxStyle.DropDown;
                }
                else if (value == false && _canInput == true)
                {
                    lstSelect.Top -= 28;
                    lstSelect.Height = lstSelected.Top + lstSelected.Height - lstSelect.Top;
                    txtInputErrorCode.Visible = false;
                    cbxErrorGroup.ComboBoxData.DropDownStyle = ComboBoxStyle.DropDownList;
                }
                _canInput = value;
            }
        }

        public TextBox ErrorInpuTextBox
        {
            get
            {
                return txtInputErrorCode;
            }
        }
        // Added end

        public void AddErrorGroups(object[] errorCodeGroups)
        {
            //Amoi,Laws Lu,2005/08/02,修改 显示不良代码组名称 code + " " + name
            for (int i = 0; i < errorCodeGroups.Length; i++)
            {
                ErrorCodeGroupA errorGroup = ((ErrorCodeGroupA)errorCodeGroups[i]);
                cbxErrorGroup.AddItem(errorGroup.ErrorCodeGroup
                    + " " + errorGroup.ErrorCodeGroupDescription, errorGroup.ErrorCodeGroup);
            }
            cbxErrorGroup.SelectedIndex = 0;
            //EndAmoi
        }

        public void ClearErrorGroup()
        {
            cbxErrorGroup.Clear();
        }

        public void AddErrorCodes(object[] errorCodes)
        {
            for (int i = 0; i < errorCodes.Length; i++)
            {
                ErrorCodeA err = (ErrorCodeA)errorCodes[i];
                lstSelect.Items.Add(err);
            }
        }

        public void ClearSelectErrorCode()
        {
            lstSelect.Items.Clear();
        }

        public void ClearSelectedErrorCode()
        {
            lstSelected.Items.Clear();
        }

        public ErrorCodeGroup2ErrorCode[] GetSelectedErrorCodes()
        {
            ErrorCodeGroup2ErrorCode[] results = new ErrorCodeGroup2ErrorCode[lstSelected.Items.Count];
            string str = null;
            for (int i = 0; i <= lstSelected.Items.Count - 1; i++)
            {
                ErrorCodeGroup2ErrorCode errorGroup2Code = new ErrorCodeGroup2ErrorCode();
                str = lstSelected.Items[i].ToString();
                errorGroup2Code.ErrorCodeGroup = str.Substring(0, str.IndexOf(":", 0, str.Length));

                string errorcode = String.Empty;
                //if(str.Split(new char[]{' '}).Length > 1)
                //{
                //    string tmpString = str.Split(new char[]{' '})[0];
                //    errorcode = tmpString.Substring(tmpString.IndexOf(":",0,tmpString.Length)+1,tmpString.Length-tmpString.IndexOf(":",0,tmpString.Length)-1);
                //}
                //else
                {
                    errorcode = str.Substring(str.IndexOf(":", 0, str.Length) + 1, str.Length - str.IndexOf(":", 0, str.Length) - 1);
                }
                errorGroup2Code.ErrorCode = errorcode;
                results[i] = errorGroup2Code;
            }
            return results;
        }

        private void lstSelect_DoubleClick(object sender, System.EventArgs e)
        {
            if (lstSelect.Items.Count != 0 && lstSelect.SelectedIndex >= 0)
            {
                // Added by Icyer 2006/06/16
                if (this.cbxErrorGroup.SelectedItemValue == null)
                {
                    Application.DoEvents();
                    this.cbxErrorGroup.Focus();
                    return;
                }
                // Added end
                //Amoi,Laws Lu,2005/08/02,添加不良代码名称显示
                //string[] errCode = lstSelect.SelectedItem.ToString().Split(new char[]{' '});

                string ec = ((ErrorCodeA)lstSelect.SelectedItem).ErrorCode;
                string en = ((ErrorCodeA)lstSelect.SelectedItem).ErrorDescription;

                //if(errCode.Length > 1)
                //{
                //    ec = errCode[0];
                //    en = errCode[1];
                //}
                //if(errCode.Length == 1)
                //{
                //    ec = errCode[0];
                //}

                errorCodeGroup2ErrorCode.ErrorCode = ec;

                errorCodeGroup2ErrorCode.ErrorCodeGroup = cbxErrorGroup.SelectedItemValue.ToString();
                //lstSelected.Items.Add(errorCodeGroup2ErrorCode);
                if (CheckRepeat(errorCodeGroup2ErrorCode.ErrorCodeGroup + ":" + ec))
                    lstSelected.Items.Add(errorCodeGroup2ErrorCode.ErrorCodeGroup + ":" + ec);
                //EndAmoi
            }
        }

        private void lstSelected_DoubleClick(object sender, System.EventArgs e)
        {
            if (lstSelected.Items.Count != 0 && lstSelected.SelectedIndex >= 0)
            {
                lstSelected.Items.RemoveAt(lstSelected.SelectedIndex);
            }
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            if (lstSelect.Items.Count != 0 && lstSelect.SelectedIndex >= 0)
            {
                // Added by Icyer 2006/06/16
                if (this.cbxErrorGroup.SelectedItemValue == null)
                {
                    Application.DoEvents();
                    this.cbxErrorGroup.Focus();
                    return;
                }
                // Added end
                //Amoi,Laws Lu,2005/08/02,添加不良代码名称显示
                //string[] errCode = lstSelect.SelectedItem.ToString().Split(new char[]{' '});

                string ec = ((ErrorCodeA)lstSelect.SelectedItem).ErrorCode;
                string en = ((ErrorCodeA)lstSelect.SelectedItem).ErrorDescription;

                //if(errCode.Length > 1)
                //{
                //    ec = errCode[0];
                //    en = errCode[1];
                //}
                //if(errCode.Length == 1)
                //{
                //    ec = errCode[0];
                //}

                errorCodeGroup2ErrorCode.ErrorCode = ec;


                errorCodeGroup2ErrorCode.ErrorCodeGroup = cbxErrorGroup.SelectedItemValue.ToString();
                //lstSelected.Items.Add(errorCodeGroup2ErrorCode);
                if (CheckRepeat(errorCodeGroup2ErrorCode.ErrorCodeGroup + ":" + ec))
                    lstSelected.Items.Add(errorCodeGroup2ErrorCode.ErrorCodeGroup + ":" + ec);
                //EndAmoi
            }
        }

        private void btnRemove_Click(object sender, System.EventArgs e)
        {
            if (lstSelected.Items.Count != 0 && lstSelected.SelectedIndex >= 0)
            {
                lstSelected.Items.RemoveAt(lstSelected.SelectedIndex);
            }
        }

        public event System.EventHandler SelectedIndexChanged;
        private void cbxErrorGroup_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(sender, e);
        }

        //Amoi,Laws Lu,2005/08/02,修改	不良代码取值
        public string SelectedErrorCodeGroup
        {
            get
            {
                if (cbxErrorGroup.ComboBoxData.Items.Count > 0)
                {
                    // Added by Icyer 2006/06/19
                    if (cbxErrorGroup.SelectedItemValue == null)
                        return string.Empty;
                    // Added end
                    return cbxErrorGroup.SelectedItemValue.ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    //					for(int i = 0;i <  cbxErrorGroup.ComboBoxData.Items; i++)
                    //					{
                    //						if(cbxErrorGroup.ComboBoxData.Items[i].ToString() == value)
                    //						{
                    cbxErrorGroup.SetSelectItem(value);
                    //cbxErrorGroup.ComboBoxData.SelectedValue = value;
                    //						}
                    //					}
                }
                catch
                {
                }
            }
        }
        //EndAmoi

        private bool CheckRepeat(string Value)
        {
            if (lstSelected.Items.IndexOf(Value) > -1)
                return false;
            else
                return true;
        }

        //Nanjing  crystal  2005/07/29  添加
        //Laws Lu,2005/08/13,修改	允许传入其他类型的对象
        public void AddSelectedErrorCodes(object[] selectedErrorCodeGroup2ErrorCodes)
        {
            foreach (object obj in selectedErrorCodeGroup2ErrorCodes)
            {
                if (obj is ErrorCodeGroup2ErrorCode)
                {
                    if (CheckRepeat(((ErrorCodeGroup2ErrorCode)obj).ErrorCodeGroup + ":" + ((ErrorCodeGroup2ErrorCode)obj).ErrorCode))
                        lstSelected.Items.Add(((ErrorCodeGroup2ErrorCode)obj).ErrorCodeGroup + ":" + ((ErrorCodeGroup2ErrorCode)obj).ErrorCode);
                }
                else
                {
                    if (CheckRepeat(obj.ToString()))
                        lstSelected.Items.Add(obj);
                }
            }
        }
        //Nanjing End


        //Amoi,Laws Lu,2005/07/25,添加	允许外部控制AddButton的Top位置
        [Bindable(true),
        Category("位置"),]
        public int AddButtonTop
        {
            get
            {
                return btnAllRight.Top;
            }
            set
            {
                btnAllRight.Top = value;
            }
        }
        //EndAmoi

        //Amoi,Laws Lu,2005/07/25,添加	允许外部控制RemoveButton的Top位置
        [Bindable(true),
        Category("位置"),]
        public int RemoveButtonTop
        {
            get
            {
                return btnAllLeft.Top;
            }
            set
            {
                btnAllLeft.Top = value;
            }
        }
        //EndAmoi

        //Amoi,Laws Lu,2005/07/26,添加 允许自动调整RemoveButton和AddButton的Top位置
        public void AutoAdjustButtonLocation()
        {
            //计算位置
            btnAllRight.Top = this.Top + this.Height / 3;
            btnAllLeft.Top = this.Top + this.Height / 3 * 2;
            //意外其它处理
            if (btnAllRight.Top < lstSelect.Top)
            {
                btnAllRight.Top = lstSelect.Top;
            }
            if (btnAllLeft.Top <= btnAllRight.Top || btnAllLeft.Top <= (btnAllRight.Top + btnAllRight.Height))
            {
                btnAllLeft.Top = btnAllRight.Top + btnAllRight.Height + 5;
            }

            this.lstSelect.Width = Convert.ToInt32((this.Width - btnAllRight.Width) / 2 * 0.8);
            this.lstSelected.Width = this.lstSelect.Width + 50;

            this.btnAllRight.Left = this.Left + this.lstSelect.Width + 20;
            this.btnAllLeft.Left = this.Left + this.lstSelect.Width + 20;
            this.lstSelected.Left = this.btnAllLeft.Left + this.btnAllRight.Width + 10;
            this.lblMessage.Width = Convert.ToInt32(this.Width * 0.5);

            this.labelSelectedErrorCode.Left = this.lstSelected.Left;
        }
        //EndAmoi

        // Added by Icyer 02/06/2006
        private void cbxErrorGroup_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (cbxErrorGroup.SelectedItemValue != null)
                {
                    cbxErrorGroup_SelectedIndexChanged(cbxErrorGroup, e);
                    Application.DoEvents();
                    txtInputErrorCode.Focus();
                }
                else
                {
                    bool bExist = false;
                    string strSelect = cbxErrorGroup.ComboBoxData.Text.Trim().ToUpper();
                    for (int i = 0; i < cbxErrorGroup.ComboBoxData.Items.Count; i++)
                    {
                        string strItem = cbxErrorGroup.ComboBoxData.Items[i].ToString().ToUpper();
                        if (strItem.Substring(0, strItem.IndexOf(" ")) == strSelect)
                        {
                            cbxErrorGroup.SelectedIndex = i;
                            cbxErrorGroup_SelectedIndexChanged(cbxErrorGroup, e);
                            bExist = true;
                            Application.DoEvents();
                            txtInputErrorCode.Focus();
                            break;
                        }
                    }
                    if (bExist == false)
                    {
                        Application.DoEvents();
                        cbxErrorGroup.Focus();
                        cbxErrorGroup.ComboBoxData.SelectAll();
                        cbxErrorGroup.ComboBoxData.Focus();
                    }
                }
            }
            if (ErrorCodeGroupKeyPress != null)
            {
                ErrorCodeGroupKeyPress(cbxErrorGroup, e);
            }
        }

        public void txtInputErrorCode_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            string strInput = txtInputErrorCode.Text.Trim().ToUpper();

            if (e.KeyChar == '\r')
            {
                if (this.cbxErrorGroup.SelectedItemValue == null)
                {
                    Application.DoEvents();
                    txtInputErrorCode.Clear();
                    this.cbxErrorGroup.Focus();
                    return;
                }
                if (strInput != String.Empty && (strInput == ClipOK || strInput == ClipFail))
                {
                    if (EndErrorCodeInput != null)
                    {
                        EndErrorCodeInput(sender, e);
                    }
                }
                else
                {
                    for (int i = 0; i < lstSelect.Items.Count; i++)
                    {

                        //string strItem = lstSelect.Items[i].ToString().ToUpper();
                        //if (strItem.Substring(0, strItem.IndexOf(" ")) == strInput)
                        ErrorCodeA errorCode = (ErrorCodeA)lstSelect.Items[i];
                        if (string.Compare(errorCode.ErrorCode, strInput, true) == 0)
                        {
                            lstSelect.SelectedIndex = i;
                            this.btnAdd_Click(null, null);
                            break;
                        }
                    }
                }
            }
            if (ErrorCodeKeyPress != null)
            {
                ErrorCodeKeyPress(txtInputErrorCode, e);
            }
            if (e.KeyChar == '\r' && strInput != ClipOK && strInput != ClipFail)
            {
                txtInputErrorCode.Clear();
                txtInputErrorCode.Focus();
            }
        }
        // Added end

        // Added by Icyer 2006/06/12
        public void SetFocusErrorGroup()
        {
            cbxErrorGroup.Focus();
        }
        public void SetFocusErrorCode()
        {
            if (this.txtInputErrorCode.Visible == true)
                this.txtInputErrorCode.Focus();
            else
                this.lstSelect.Focus();
        }

        private void UCErrorCodeSelect_Load(object sender, System.EventArgs e)
        {
            lblMessage.Text = MutiLanguages.ParserMessage("$CS_SLIP_OK") + "\r\n"
                + MutiLanguages.ParserMessage("$CS_SLIP_FAIL");
        }

        private void txtInputErrorCode_Enter(object sender, System.EventArgs e)
        {
            txtInputErrorCode.BackColor = Color.GreenYellow;
        }

        private void txtInputErrorCode_Leave(object sender, System.EventArgs e)
        {
            txtInputErrorCode.BackColor = Color.White;
        }
        // Added end

    }
}
