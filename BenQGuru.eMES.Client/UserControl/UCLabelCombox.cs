using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;


namespace UserControl
{
    /// <summary>
    /// UCLabCombox 的摘要说明。
    /// </summary>
    public class UCLabelCombox : System.Windows.Forms.UserControl
    {
        private System.Windows.Forms.ComboBox cmbSelectBox;
        private System.Windows.Forms.Label labCaption;
        private System.Windows.Forms.CheckBox checkBoxEdit;
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>

        private System.ComponentModel.Container components = null;

        public UCLabelCombox()
        {
            // 该调用是 Windows.Forms 窗体设计器所必需的。
            InitializeComponent();

            // TODO: 在 InitializeComponent 调用后添加任何初始化
            cmbSelectBox.Top = 0;
            labCaption.Top = 5;
            checkBoxEdit.Top = 0;
            this.Height = cmbSelectBox.Height;

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
            this.cmbSelectBox = new System.Windows.Forms.ComboBox();
            this.labCaption = new System.Windows.Forms.Label();
            this.checkBoxEdit = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cmbSelectBox
            // 
            this.cmbSelectBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSelectBox.DropDownHeight = 212;
            this.cmbSelectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectBox.IntegralHeight = false;
            this.cmbSelectBox.Location = new System.Drawing.Point(83, 3);
            this.cmbSelectBox.Name = "cmbSelectBox";
            this.cmbSelectBox.Size = new System.Drawing.Size(133, 20);
            this.cmbSelectBox.TabIndex = 0;
            this.cmbSelectBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.cmbSelectBox.Leave += new System.EventHandler(this.cmbSelectBox_Leave);
            this.cmbSelectBox.Enter += new System.EventHandler(this.cmbSelectBox_Enter);
            this.cmbSelectBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbSelectBox_KeyPress);
            this.cmbSelectBox.DropDown += new System.EventHandler(this.cmbSelectBox_DropDown);
            // 
            // labCaption
            // 
            this.labCaption.AutoSize = true;
            this.labCaption.Location = new System.Drawing.Point(26, 7);
            this.labCaption.Name = "labCaption";
            this.labCaption.Size = new System.Drawing.Size(41, 12);
            this.labCaption.TabIndex = 1;
            this.labCaption.Text = "label1";
            this.labCaption.SizeChanged += new System.EventHandler(this.label1_SizeChanged);
            // 
            // checkBoxEdit
            // 
            this.checkBoxEdit.Location = new System.Drawing.Point(3, 2);
            this.checkBoxEdit.Name = "checkBoxEdit";
            this.checkBoxEdit.Size = new System.Drawing.Size(16, 24);
            this.checkBoxEdit.TabIndex = 3;
            this.checkBoxEdit.Visible = false;
            this.checkBoxEdit.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // UCLabelCombox
            // 
            this.Controls.Add(this.checkBoxEdit);
            this.Controls.Add(this.labCaption);
            this.Controls.Add(this.cmbSelectBox);
            this.Name = "UCLabelCombox";
            this.Size = new System.Drawing.Size(216, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void label1_SizeChanged(object sender, System.EventArgs e)
        {
            AutoChange();
        }

        [Bindable(true),
        Category("外观")]
        public string Caption
        {
            get
            {
                return labCaption.Text;
            }
            set
            {
                labCaption.Text = value;
            }
        }
        [Bindable(true),
        Category("数据"),
        ]
        public System.Windows.Forms.ComboBox ComboBoxData
        {
            get
            {
                return cmbSelectBox;
            }
        }
        public string SelectedItemText
        {
            get
            {
                if (cmbSelectBox.SelectedItem is ItemObject)
                    return ((ItemObject)cmbSelectBox.SelectedItem).ItemText;
                else
                    return cmbSelectBox.SelectedText;
            }
        }
        public object SelectedItemValue
        {
            get
            {
                if (cmbSelectBox.SelectedItem is ItemObject)
                    return ((ItemObject)cmbSelectBox.SelectedItem).ItemValue;
                else
                    return cmbSelectBox.SelectedValue;
            }
        }
        public int SelectedIndex
        {
            get
            {
                return cmbSelectBox.SelectedIndex;
            }
            set
            {
                cmbSelectBox.SelectedIndex = value;
            }
        }
        public void AddItem(string itemText, object itemValue)
        {
            ItemObject itemObject = new ItemObject();
            itemObject.ItemText = itemText;
            itemObject.ItemValue = itemValue;
            cmbSelectBox.Items.Add(itemObject);
        }
        public void SetSelectItem(object itemValue)
        {
            for (int i = 0; i < this.ComboBoxData.Items.Count; i++)
            {
                ItemObject itemObject = (ItemObject)cmbSelectBox.Items[i];
                if (itemObject.ItemValue.Equals(itemValue))
                {
                    ComboBoxData.SelectedIndex = i;
                    return;
                }
            }
            ComboBoxData.SelectedIndex = -1;

        }
        public void SetSelectItemText(string text)
        {
            for (int i = 0; i < this.ComboBoxData.Items.Count; i++)
            {
                ItemObject itemObject = (ItemObject)cmbSelectBox.Items[i];
                if (itemObject.ItemText == text)
                {
                    ComboBoxData.SelectedIndex = i;
                    return;
                }
            }
            ComboBoxData.SelectedIndex = -1;

        }
        public void Clear()
        {
            cmbSelectBox.Items.Clear();
        }

        public event System.EventHandler SelectedIndexChanged;

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(sender, e);
        }




        [Bindable(true),
        Category("CheckBox")]
        public bool ShowCheckBox
        {
            get
            {
                return checkBoxEdit.Visible;
            }
            set
            {
                checkBoxEdit.Visible = value;
                AutoChange();
                checkBox_CheckedChanged(null, null);
            }
        }
        private bool _allowEditOnlyChecked = true;
        [Bindable(true),
        Category("CheckBox")]
        public bool AllowEditOnlyChecked
        {
            get
            {
                return _allowEditOnlyChecked;
            }
            set
            {
                _allowEditOnlyChecked = value;
            }
        }
        [Bindable(true),
        Category("CheckBox")]
        public bool Checked
        {
            get
            {
                return checkBoxEdit.Checked;
            }
            set
            {
                checkBoxEdit.Checked = value;
            }
        }

        public event System.EventHandler CheckBoxCheckedChanged;
        private void checkBox_CheckedChanged(object sender, System.EventArgs e)
        {
            cmbSelectBox.Enabled = true;
            if (checkBoxEdit.Visible)
                if (_allowEditOnlyChecked)
                    cmbSelectBox.Enabled = checkBoxEdit.Checked;

            if (CheckBoxCheckedChanged != null)
                CheckBoxCheckedChanged(sender, e);

        }

        public event System.Windows.Forms.KeyPressEventHandler ComboBoxKeyPress;


        private int _xAlign = -1;
        [Bindable(true),
        Category("外观")]
        public int XAlign
        {
            get
            {
                _xAlign = this.Left + cmbSelectBox.Left;
                return _xAlign;
            }
            set
            {
                _xAlign = value;
                this.Left = value - cmbSelectBox.Left;

            }
        }

        private WidthTypes _widthType = WidthTypes.Normal;

        [Bindable(true),
        Category("外观")]
        public WidthTypes WidthType
        {
            get
            {
                return _widthType;
            }
            set
            {
                //txtboxInput.Anchor=AnchorStyles.
                _widthType = value;

                switch (_widthType)
                {
                    case WidthTypes.TooLong:
                        cmbSelectBox.Width = 400;
                        break;
                    case WidthTypes.Long:
                        //
                        //this.Width =labCaption.Width +200+UICommon.SepOfLabAndEditBox;
                        cmbSelectBox.Width = 200;
                        break;
                    case WidthTypes.Normal:
                        //this.Width =labCaption.Width +133+UICommon.SepOfLabAndEditBox;
                        cmbSelectBox.Width = 133;
                        break;
                    case WidthTypes.Small:
                        //this.Width =labCaption.Width +100+UICommon.SepOfLabAndEditBox;
                        cmbSelectBox.Width = 100;
                        break;
                }
                AutoChange();
            }
        }

        public void AutoChange()
        {
            if (checkBoxEdit.Visible)
            {
                this.Width = checkBoxEdit.Width + labCaption.Width + cmbSelectBox.Width + UIStyleBuilder.SepOfLabAndEditBox;
                cmbSelectBox.Left = checkBoxEdit.Width + labCaption.Width + UIStyleBuilder.SepOfLabAndEditBox;
                checkBoxEdit.Left = 0;
                labCaption.Left = checkBoxEdit.Width;

            }
            else
            {
                this.Width = labCaption.Width + cmbSelectBox.Width + UIStyleBuilder.SepOfLabAndEditBox;
                cmbSelectBox.Left = labCaption.Width + UIStyleBuilder.SepOfLabAndEditBox;
                labCaption.Left = 0;
            }
            if (_xAlign != -1)
                this.Left = _xAlign - cmbSelectBox.Left;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        public event System.EventHandler SelectBox_DropDown;
        private void cmbSelectBox_DropDown(object sender, System.EventArgs e)
        {
            if (SelectBox_DropDown != null)
                SelectBox_DropDown(sender, e);
        }

        private void cmbSelectBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                System.Windows.Forms.SendKeys.Send("{TAB}");
                Application.DoEvents();
            }

            if (ComboBoxKeyPress != null)
                ComboBoxKeyPress(sender, e);
        }

        private void cmbSelectBox_Enter(object sender, System.EventArgs e)
        {
            this.cmbSelectBox.BackColor = Color.GreenYellow;
        }

        private void cmbSelectBox_Leave(object sender, System.EventArgs e)
        {
            this.cmbSelectBox.BackColor = Color.White;
        }

    }
}
