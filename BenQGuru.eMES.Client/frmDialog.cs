using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// frmDialog 的摘要说明。
	/// </summary>
	public class frmDialog : BaseForm
	{
		private System.Windows.Forms.Label lblMessageForFrmDialog;
		private System.Windows.Forms.Button btnConfirmY;
		private System.Windows.Forms.Button btnCancleN;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmDialog()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
            //this.InitPageLanguage();
			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
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
            this.lblMessageForFrmDialog = new System.Windows.Forms.Label();
            this.btnConfirmY = new System.Windows.Forms.Button();
            this.btnCancleN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMessageForFrmDialog
            // 
            this.lblMessageForFrmDialog.Location = new System.Drawing.Point(16, 16);
            this.lblMessageForFrmDialog.Name = "lblMessageForFrmDialog";
            this.lblMessageForFrmDialog.Size = new System.Drawing.Size(248, 32);
            this.lblMessageForFrmDialog.TabIndex = 0;
            // 
            // btnConfirmY
            // 
            this.btnConfirmY.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirmY.Location = new System.Drawing.Point(56, 56);
            this.btnConfirmY.Name = "btnConfirmY";
            this.btnConfirmY.Size = new System.Drawing.Size(75, 23);
            this.btnConfirmY.TabIndex = 0;
            this.btnConfirmY.TabStop = false;
            this.btnConfirmY.Text = "是";
            this.btnConfirmY.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancleN
            // 
            this.btnCancleN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancleN.Location = new System.Drawing.Point(144, 56);
            this.btnCancleN.Name = "btnCancleN";
            this.btnCancleN.Size = new System.Drawing.Size(75, 23);
            this.btnCancleN.TabIndex = 0;
            this.btnCancleN.TabStop = false;
            this.btnCancleN.Text = "否";
            this.btnCancleN.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // frmDialog
            // 
            this.AcceptButton = this.btnConfirmY;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this.btnCancleN;
            this.ClientSize = new System.Drawing.Size(274, 87);
            this.Controls.Add(this.btnCancleN);
            this.Controls.Add(this.btnConfirmY);
            this.Controls.Add(this.lblMessageForFrmDialog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmDialog";
            this.ResumeLayout(false);

		}
		#endregion

		private void btnConfirm_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;

			this.Close();
		}

		private void btnCancle_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;

			this.Close();
		}

		public string DialogTitle
		{
			set
			{
				this.Text = value;
			}
		}

		public string DialogMessage
		{
			set
			{
				this.lblMessageForFrmDialog.Text = value;
			}
		}
	}
}
