using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BenQGuru.eMES.PDAClient
{
	/// <summary>
	/// FDialogInput 的摘要说明。
	/// </summary>
	public class FDialogInput : System.Windows.Forms.Form
	{
		private UserControl.UCLabelEdit txtInput;
		private System.Windows.Forms.Label lblTitle;
		private UserControl.UCButton btnOK;
		private UserControl.UCButton btnExit;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FDialogInput()
		{
			InitializeComponent();
		}

		public FDialogInput(string titleMessage)
		{
			InitializeComponent();
			this.lblTitle.Text = titleMessage;
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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FDialogInput));
            this.txtInput = new UserControl.UCLabelEdit();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnOK = new UserControl.UCButton();
            this.btnExit = new UserControl.UCButton();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.AllowEditOnlyChecked = true;
            this.txtInput.Caption = "";
            this.txtInput.Checked = false;
            this.txtInput.EditType = UserControl.EditTypes.String;
            this.txtInput.Location = new System.Drawing.Point(19, 37);
            this.txtInput.MaxLength = 40;
            this.txtInput.Multiline = false;
            this.txtInput.Name = "txtInput";
            this.txtInput.PasswordChar = '\0';
            this.txtInput.ReadOnly = false;
            this.txtInput.ShowCheckBox = false;
            this.txtInput.Size = new System.Drawing.Size(208, 22);
            this.txtInput.TabIndex = 1;
            this.txtInput.TabNext = true;
            this.txtInput.Value = "";
            this.txtInput.WidthType = UserControl.WidthTypes.Long;
            this.txtInput.XAlign = 27;
            this.txtInput.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInput_TxtboxKeyPress);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(27, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(35, 13);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "label1";
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.ButtonType = UserControl.ButtonTypes.None;
            this.btnOK.Caption = "确定";
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Location = new System.Drawing.Point(20, 74);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 22);
            this.btnOK.TabIndex = 4;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.ButtonType = UserControl.ButtonTypes.None;
            this.btnExit.Caption = "退出";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(124, 74);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 22);
            this.btnExit.TabIndex = 5;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FDialogInput
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(250, 122);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FDialogInput";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "输入";
            this.Load += new System.EventHandler(this.FDialogInput_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FDialogInput_Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public string TitleMessage
		{
			get { return this.lblTitle.Text; }
			set { this.lblTitle.Text = value; }
		}
		public string InputText
		{
			get { return this.txtInput.Value; }
		}

        public char InputPasswordChar
        {
            set { this.txtInput.PasswordChar = value; }
        }
		
		private void FDialogInput_Load(object sender, System.EventArgs e)
		{
			this.txtInput.TextFocus(true, true);
			UserControl.UIStyleBuilder.FormUI(this);
		}

		private bool bOK = false;
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			bOK = true;
			this.Hide();
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.No;
			this.Hide();
		}

		private void FDialogInput_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (bOK == false)
				this.DialogResult = DialogResult.No;
		}

		private void txtInput_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				btnOK_Click(null, null);
			}
		}
	}
}
