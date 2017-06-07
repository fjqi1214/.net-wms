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
	public class FNewMo : System.Windows.Forms.Form
	{
		private UserControl.UCButton ucBtnCancel;
		private System.Windows.Forms.Label lblMessage;
		private UserControl.UCButton ucBtnOK;

//		private FStockInput frmStockIn;
		private FKeyParts frmKeyPart;
		private UserControl.UCLabelEdit txtMo;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FNewMo()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
			UserControl.UIStyleBuilder.FormUI(this);

			txtMo.TextFocus(false, true);
		}

		public FNewMo(Form frm)
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			if(frm is FKeyParts)
			{
				frmKeyPart = (FKeyParts)frm;
			}
//			if(frm is FStockInput)
//			{
//				frmStockIn = (FStockInput)frm;
//			}
			//
			UserControl.UIStyleBuilder.FormUI(this);

			txtMo.TextFocus(false, true);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FNewMo));
			this.ucBtnCancel = new UserControl.UCButton();
			this.lblMessage = new System.Windows.Forms.Label();
			this.ucBtnOK = new UserControl.UCButton();
			this.txtMo = new UserControl.UCLabelEdit();
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
			this.ucBtnCancel.Location = new System.Drawing.Point(144, 96);
			this.ucBtnCancel.Name = "ucBtnCancel";
			this.ucBtnCancel.Size = new System.Drawing.Size(88, 22);
			this.ucBtnCancel.TabIndex = 3;
			this.ucBtnCancel.Click += new System.EventHandler(this.ucBtnCancel_Click);
			// 
			// lblMessage
			// 
			this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblMessage.Location = new System.Drawing.Point(16, 11);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(142, 16);
			this.lblMessage.TabIndex = 5;
			this.lblMessage.Text = "请输入工单号码";
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
			this.ucBtnOK.Location = new System.Drawing.Point(24, 96);
			this.ucBtnOK.Name = "ucBtnOK";
			this.ucBtnOK.Size = new System.Drawing.Size(88, 22);
			this.ucBtnOK.TabIndex = 2;
			this.ucBtnOK.Click += new System.EventHandler(this.ucBtnOK_Click);
			// 
			// txtMo
			// 
			this.txtMo.AllowEditOnlyChecked = true;
			this.txtMo.Caption = "";
			this.txtMo.Checked = false;
			this.txtMo.EditType = UserControl.EditTypes.String;
			this.txtMo.Location = new System.Drawing.Point(8, 48);
			this.txtMo.MaxLength = 40;
			this.txtMo.Multiline = false;
			this.txtMo.Name = "txtMo";
			this.txtMo.PasswordChar = '\0';
			this.txtMo.ReadOnly = false;
			this.txtMo.ShowCheckBox = false;
			this.txtMo.Size = new System.Drawing.Size(208, 24);
			this.txtMo.TabIndex = 1;
			this.txtMo.TabNext = true;
			this.txtMo.Value = "";
			this.txtMo.WidthType = UserControl.WidthTypes.Long;
			this.txtMo.XAlign = 16;
			this.txtMo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMo_TxtboxKeyPress);
			// 
			// FNewMo
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(242, 135);
			this.Controls.Add(this.txtMo);
			this.Controls.Add(this.ucBtnCancel);
			this.Controls.Add(this.lblMessage);
			this.Controls.Add(this.ucBtnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FNewMo";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.ResumeLayout(false);

		}
		#endregion

//		public string Input
//		{
//			get
//			{
//				return this.rtxtInput.Text.Trim().ToUpper();
//			}
//			set
//			{
//				this.rtxtInput.Text = value;
//			}
//		}

		protected void ShowMessage(string message)
		{
			ApplicationRun.GetInfoForm().Add( message );
		}

		private void ucBtnOK_Click(object sender, System.EventArgs e)
		{
			if ( this.txtMo.Value.Trim() == string.Empty )
			{
				this.ShowMessage("$CS_CMPleaseInputMO");	//请输入待移除的二维条码
			}
			else
			{
				this.DialogResult = DialogResult.OK;

				if(frmKeyPart != null)
				{
					frmKeyPart.MOCode = this.txtMo.Value.ToString().ToUpper().Trim();
				}
//				if(frmPack != null)
//				{
//					frmPack.PlanateNum = this.rtxtInput.Text.ToString().ToUpper().Trim();
//				}
				this.Close();
			}
		}

		private void ucBtnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();		
		}

		private void txtMo_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( e.KeyChar == '\r' )
			{
				this.ucBtnOK_Click( this, null );
			}
		}
	}
}
