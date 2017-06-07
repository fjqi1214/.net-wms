using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace UserControl
{
	/// <summary>
	/// UCLabEdit 的摘要说明。
	/// </summary>
	public class UCLabelEdit : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UCLabelEdit()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化
			
			txtboxInput.Top=0;
			labCaption.Top = 5;
			checkBoxEdit.Top=0;
			this.Height=24;
			//this.txtboxInput.Font.Bold =false;
			_dateTime=DateTime.Now ;
			
		}

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.labCaption = new System.Windows.Forms.Label();
            this.txtboxInput = new System.Windows.Forms.TextBox();
            this.checkBoxEdit = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labCaption
            // 
            this.labCaption.AutoSize = true;
            this.labCaption.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.labCaption.Location = new System.Drawing.Point(32, 5);
            this.labCaption.Name = "labCaption";
            this.labCaption.Size = new System.Drawing.Size(41, 12);
            this.labCaption.TabIndex = 0;
            this.labCaption.Text = "label1";
            this.labCaption.SizeChanged += new System.EventHandler(this.labCaption_SizeChanged);
            // 
            // txtboxInput
            // 
            this.txtboxInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtboxInput.BackColor = System.Drawing.SystemColors.Window;
            this.txtboxInput.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtboxInput.Location = new System.Drawing.Point(82, 1);
            this.txtboxInput.Multiline = true;
            this.txtboxInput.Name = "txtboxInput";
            this.txtboxInput.Size = new System.Drawing.Size(133, 21);
            this.txtboxInput.TabIndex = 2;
            this.txtboxInput.TextChanged += new System.EventHandler(this.txtboxInput_TextChanged);
            this.txtboxInput.Click += new System.EventHandler(this.txtboxInput_Click);
            this.txtboxInput.GotFocus += new System.EventHandler(this.txtboxInput_GotFocus);
            this.txtboxInput.Leave += new System.EventHandler(this.txtboxInput_Leave);
            this.txtboxInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtboxInput_KeyPress);
            this.txtboxInput.Enter += new System.EventHandler(this.txtboxInput_Enter);
            // 
            // checkBoxEdit
            // 
            this.checkBoxEdit.Location = new System.Drawing.Point(8, 1);
            this.checkBoxEdit.Name = "checkBoxEdit";
            this.checkBoxEdit.Size = new System.Drawing.Size(16, 24);
            this.checkBoxEdit.TabIndex = 1;
            this.checkBoxEdit.Visible = false;
            this.checkBoxEdit.Click += new System.EventHandler(this.checkBoxEdit_Click);
            this.checkBoxEdit.CheckedChanged += new System.EventHandler(this.checkBoxEdit_CheckedChanged);
            // 
            // UCLabelEdit
            // 
            this.Controls.Add(this.checkBoxEdit);
            this.Controls.Add(this.txtboxInput);
            this.Controls.Add(this.labCaption);
            this.Name = "UCLabelEdit";
            this.Size = new System.Drawing.Size(216, 24);
            this.GotFocus += new System.EventHandler(this.txtboxInput_GotFocus);
            this.Leave += new System.EventHandler(this.UCLabelEdit_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void labCaption_SizeChanged(object sender, System.EventArgs e)
		{
			AutoChange();
		}

		private System.Windows.Forms.Label labCaption;
		private System.Windows.Forms.TextBox txtboxInput;
	
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
				labCaption.Text =value;
			}
		}
		/// <summary>
		/// 获取TextBox
		/// </summary>
		public TextBox InnerTextBox
		{
			get
			{
				return txtboxInput;
			}
		}

		/// <summary>
		/// 获取CheckBox
		/// </summary>
		public CheckBox InnerCheckBox
		{
			get
			{
				return checkBoxEdit;
			}
		}

		[Bindable(true),
		Category("外观")]
		public string Value
		{
			get
			{
				return txtboxInput.Text.ToUpper();
			}
			set
			{
				txtboxInput.Text =value;
			}
		}
		/// <summary>
		/// 选择全部
		/// </summary>
		public void SelectAll()
		{
			txtboxInput.SelectAll();	
		}
		
		private bool _readOnly=false;
		[Bindable(true),
		Category("外观")]
		public bool ReadOnly
		{
			get
			{
				return _readOnly;
			}
			set
			{
				_readOnly =value;
				txtboxInput.ReadOnly=_readOnly;
				if (_readOnly)
				{
					this.txtboxInput.ForeColor =Color.Red;	
					this.txtboxInput.BackColor =Color.FromName("Window");
				}
				else
				{
					this.txtboxInput.ForeColor =Color.FromName("WindowText");
				}
				//this.ReadOnly=_readOnly;
				checkBoxEdit.Enabled=!_readOnly;
			}
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
				checkBoxEdit.Visible =value;
				AutoChange();
				checkBoxEdit_CheckedChanged(null,null);
			}
		}
		private bool _allowEditOnlyChecked=true;
		private System.Windows.Forms.CheckBox checkBoxEdit;
	
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
				_allowEditOnlyChecked =value;
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
				checkBoxEdit.Checked =value;
			}
		}
		private WidthTypes _widthType=WidthTypes.Normal;

		public event System.EventHandler CheckBoxCheckedChanged;
		public event System.EventHandler CheckBoxClick;
		private void checkBoxEdit_CheckedChanged(object sender, System.EventArgs e)
		{
			txtboxInput.Enabled =true;
			if (checkBoxEdit.Visible )
				if (_allowEditOnlyChecked)
				{
					txtboxInput.Enabled =checkBoxEdit.Checked;
					if (!checkBoxEdit.Checked)
						txtboxInput.Text = string.Empty;
				}
			if (!txtboxInput.Focus())
				if (checkBoxEdit.Visible)
					checkBoxEdit.Checked =false;
			if (CheckBoxCheckedChanged != null)
				CheckBoxCheckedChanged(sender,e);;
		}
	
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
				_widthType =value;
				
				switch (_widthType)
				{
					case WidthTypes.TooLong :
						txtboxInput.Width=400;						
						break;
					case WidthTypes.Long :
						//
						//this.Width =labCaption.Width +200+UICommon.SepOfLabAndEditBox;
						txtboxInput.Width=200;						
						break;
					case WidthTypes.Normal :
						//this.Width =labCaption.Width +133+UICommon.SepOfLabAndEditBox;
						txtboxInput.Width=133;
						break;
					case WidthTypes.Small :
						//this.Width =labCaption.Width +100+UICommon.SepOfLabAndEditBox;
						txtboxInput.Width=100;
						break;
					case WidthTypes.Tiny ://Laws Lu,2006/06/05 Add ,support tiny scale
						//this.Width =labCaption.Width +100+UICommon.SepOfLabAndEditBox;
						txtboxInput.Width=	50;
						break;
				}
				AutoChange();
			}
		}

		private EditTypes _editType=EditTypes.String;
		[Bindable(true),
		Category("外观")]
		public EditTypes EditType
		{
			get 
			{
				return _editType;
			}
			set
			{
				_editType =value;
			}

		}

		private char _passwordChar;
		[Bindable(true),
		Category("外观")]
		public char PasswordChar
		{
			get 
			{
				return _passwordChar;
			}
			set
			{
				_passwordChar =value;
				txtboxInput.PasswordChar=_passwordChar;

			}

		}

		private int _maxLength=40;		
		[Bindable(true),
		Category("外观")]
		public int MaxLength
		{
			get 
			{  
				return 	_maxLength;		
			}
			set
			{   
				_maxLength = value;
				txtboxInput.MaxLength = _maxLength;	
				
			}
		}
		
		private int _xAlign=-1;		
		[Bindable(true),
		Category("外观")]
		public int XAlign
		{
			get 
			{  
				_xAlign = this.Left+txtboxInput.Left;
				return 	_xAlign;		
			}
			set
			{   
				_xAlign = value;
				this.Left = value-txtboxInput.Left;	
				
			}
		}

		public void AutoChange()
		{
			if (checkBoxEdit.Visible)
			{
				this.Width =checkBoxEdit.Width+labCaption.Width +txtboxInput.Width+UIStyleBuilder.SepOfLabAndEditBox;
				txtboxInput.Left=checkBoxEdit.Width+labCaption.Width +UIStyleBuilder.SepOfLabAndEditBox;
				checkBoxEdit.Left =0;
				labCaption.Left =checkBoxEdit.Width;
				
			}
			else
			{
				this.Width =labCaption.Width +txtboxInput.Width+ UIStyleBuilder.SepOfLabAndEditBox;		
				txtboxInput.Left=labCaption.Width + UIStyleBuilder.SepOfLabAndEditBox;
				labCaption.Left =0;
            }
			if (_xAlign!=-1)
				this.Left = _xAlign-txtboxInput.Left;	
		}

		private bool _multiline=false;
		[Bindable(true),
		Category("外观")]
		public bool Multiline
		{
			get 
			{  
				return 	_multiline;		
			}
			set
			{   
				_multiline = value;				
			}
		}

        private bool _autoUpper = true;
        [Bindable(true),
        Category("外观")]
        public bool AutoUpper
        {
            get
            {
                return _autoUpper;
            }
            set
            {
                _autoUpper = value;
            }
        }


        private bool _autoSelectAll = false;
        [Bindable(true),
        Category("外观")]
        public bool AutoSelectAll
        {
            get
            {
                return _autoSelectAll;
            }
            set
            {
                _autoSelectAll = value;

            }
        }



		/// <summary>
		/// 是否允许按回车=按TAB
		/// </summary>
		private bool _tabNext=true;
		[Bindable(true),
		Category("外观")]
		public bool TabNext
		{
			get 
			{  
				return 	_tabNext;		
			}
			set
			{   
				_tabNext = value;				
			}
		}




        public void TextFocus(bool clear, bool selectAll)
        {
            if (clear)
            {
                txtboxInput.Text = string.Empty;
            }
            if (txtboxInput.Enabled)
            {
                txtboxInput.Focus();
            }            
            if (selectAll)
            {
                txtboxInput.SelectAll();
            }
        }

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);
		}

		public event System.Windows.Forms.KeyPressEventHandler TxtboxKeyPress;

		public event System.EventHandler InnerTextChanged;

		public event System.EventHandler TxtboxGotFocus = null;	// Added by Icyer 2006/06/09

		private void txtboxInput_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ((e.KeyChar == '\r') && (!_multiline))
			{
				e.Handled = true;
                if (_tabNext)
                {
                    Application.DoEvents();
                    System.Windows.Forms.SendKeys.Send("{TAB}");
                    Application.DoEvents();
                }

			}
			if (!(("0123456789.-".IndexOf(e.KeyChar) >= 0)||(e.KeyChar==8))&&(EditType==EditTypes.Number))
			{
				e.Handled =true;
			}
			if (!(("0123456789-".IndexOf(e.KeyChar) >= 0)||(e.KeyChar==8))&&(EditType==EditTypes.Integer))
			{
				e.Handled =true;
			}
			if (TxtboxKeyPress!=null)
				TxtboxKeyPress(sender,e);
		}

		private void txtboxInput_TextChanged(object sender, System.EventArgs e)
		{
            if (this._maxLength > 0)
            {
                System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("gb2312");
                string text = this.txtboxInput.Text;         

                if (encoding.GetByteCount(text) > _maxLength)
                {                 
                    while (encoding.GetByteCount(text) > _maxLength)
                    {
                        text = text.Substring(0, text.Length - 1);
                    }

                    this.txtboxInput.Text = this.txtboxInput.Text.Substring(0, this.txtboxInput.Text.Length - 1);
                    System.Windows.Forms.SendKeys.Send("^{END}");
                }
            }

			if(InnerTextChanged != null)
			{
				InnerTextChanged(sender,e);
			}
			//			switch (_editType)
			//			{
			//				case EditTypes.String :
			//							
			//					break;
			//				case EditTypes.Number:
			//					
			//					break;
			//			}


		}
		private DateTime _dateTime;
		private int _leaveTimes=0;

		private void txtboxInput_Leave(object sender, System.EventArgs e)
		{
			if (this.Checked && txtboxInput.Text.Trim()==string.Empty)
			{
				if (DateTime.Now >_dateTime.AddSeconds(1))
				{
					_dateTime=DateTime.Now;
				}
				else
				{
					_leaveTimes++;
					if (_leaveTimes>5)
					{
						this.Checked =false;
						return;
					}
				}

				if (!txtboxInput.Focus())
				{
					this.Checked=false;
				}

			}
			this.txtboxInput.BackColor = Color.White;
		}

		// Added by Icyer 2006/06/09
		private void txtboxInput_GotFocus(object sender, EventArgs e)
		{
			if (TxtboxGotFocus != null)
				TxtboxGotFocus(this, e);
		}

		private void checkBoxEdit_Click(object sender, System.EventArgs e)
		{
			if (CheckBoxClick != null)
				CheckBoxClick(sender,e);
		}

		private void txtboxInput_Enter(object sender, System.EventArgs e)
		{
			this.txtboxInput.BackColor = Color.GreenYellow;
		}

		private void UCLabelEdit_Leave(object sender, System.EventArgs e)
		{
			this.txtboxInput.BackColor = Color.White;
		}

        private void txtboxInput_Click(object sender, EventArgs e)
        {
            if (_autoSelectAll)
            {
                this.txtboxInput.SelectAll();
            }
        }

	}
}
