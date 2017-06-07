using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FStockRemove 的摘要说明。
	/// </summary>
	public class FStockRemove : System.Windows.Forms.Form
	{
		private UserControl.UCButton ucBtnCancel;
		private System.Windows.Forms.Label lblMessage;
		private UserControl.UCButton ucBtnOK;
		private System.Windows.Forms.RichTextBox rtxtInput;

		private FStockInput frmStockIn;
		private FGenLot frmPack;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FStockRemove()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
			UserControl.UIStyleBuilder.FormUI(this);
		}

		public FStockRemove(Form frm)
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			if(frm is FGenLot)
			{
				frmPack = (FGenLot)frm;
			}
			if(frm is FStockInput)
			{
				frmStockIn = (FStockInput)frm;
			}
			//
			UserControl.UIStyleBuilder.FormUI(this);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FStockRemove));
			this.ucBtnCancel = new UserControl.UCButton();
			this.lblMessage = new System.Windows.Forms.Label();
			this.ucBtnOK = new UserControl.UCButton();
			this.rtxtInput = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// ucBtnCancel
			// 
			this.ucBtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.ucBtnCancel.BackColor = System.Drawing.SystemColors.Control;
			this.ucBtnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnCancel.BackgroundImage")));
			this.ucBtnCancel.ButtonType = UserControl.ButtonTypes.None;
			this.ucBtnCancel.Caption = "取消";
			this.ucBtnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ucBtnCancel.Location = new System.Drawing.Point(160, 112);
			this.ucBtnCancel.Name = "ucBtnCancel";
			this.ucBtnCancel.Size = new System.Drawing.Size(88, 22);
			this.ucBtnCancel.TabIndex = 2;
			this.ucBtnCancel.Click += new System.EventHandler(this.ucBtnCancel_Click);
			// 
			// lblMessage
			// 
			this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblMessage.Location = new System.Drawing.Point(24, 11);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(248, 16);
			this.lblMessage.TabIndex = 5;
			this.lblMessage.Text = "请输入待移除的二维条码";
			this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ucBtnOK
			// 
			this.ucBtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ucBtnOK.BackColor = System.Drawing.SystemColors.Control;
			this.ucBtnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnOK.BackgroundImage")));
			this.ucBtnOK.ButtonType = UserControl.ButtonTypes.None;
			this.ucBtnOK.Caption = "确定";
			this.ucBtnOK.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ucBtnOK.Location = new System.Drawing.Point(40, 112);
			this.ucBtnOK.Name = "ucBtnOK";
			this.ucBtnOK.Size = new System.Drawing.Size(88, 22);
			this.ucBtnOK.TabIndex = 1;
			this.ucBtnOK.Click += new System.EventHandler(this.ucBtnOK_Click);
			// 
			// rtxtInput
			// 
			this.rtxtInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.rtxtInput.Location = new System.Drawing.Point(16, 32);
			this.rtxtInput.Name = "rtxtInput";
			this.rtxtInput.Size = new System.Drawing.Size(256, 72);
			this.rtxtInput.TabIndex = 0;
			this.rtxtInput.Text = "";
			this.rtxtInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtxtInput_KeyPress);
			// 
			// FStockRemove
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(292, 141);
			this.Controls.Add(this.rtxtInput);
			this.Controls.Add(this.ucBtnCancel);
			this.Controls.Add(this.lblMessage);
			this.Controls.Add(this.ucBtnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FStockRemove";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.ResumeLayout(false);

		}
		#endregion

		public string Input
		{
			get
			{
				return this.rtxtInput.Text.Trim().ToUpper();
			}
			set
			{
				this.rtxtInput.Text = value;
			}
		}

		protected void ShowMessage(string message)
		{
			ApplicationRun.GetInfoForm().Add( message );
		}

		private void ucBtnOK_Click(object sender, System.EventArgs e)
		{
			if ( this.rtxtInput.Text.Trim() == string.Empty )
			{
				this.ShowMessage("$CS_Please_Input_Remove_Planate");	//请输入待移除的二维条码
			}
			else
			{
				this.DialogResult = DialogResult.OK;

				if(frmStockIn != null)
				{
					frmStockIn.PlanateNum = this.rtxtInput.Text.ToString().ToUpper().Trim();
				}
				if(frmPack != null)
				{
					frmPack.PlanateNum = this.rtxtInput.Text.ToString().ToUpper().Trim();
				}
				this.Close();
			}
		}

		private void ucBtnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();		
		}

		private void rtxtInput_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( e.KeyChar == '\r' )
			{
				this.ucBtnOK_Click( this, null );
			}
		
		}
	}
}
