using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Web.Helper;
using UserControl;
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FErrorCodeSelect 的摘要说明。
	/// </summary>
	public class FTSErrorCodeSelect : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private UserControl.UCButton ucButtonSave;
		private UserControl.UCButton ucButtonExit;
		private UserControl.UCErrorCodeSelect errorCodeSelect;
		private UserControl.UCLabelEdit ucLabEditProduct;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		TSModelFacade tsFacade;
		public FTSErrorCodeSelect()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			UserControl.UIStyleBuilder.FormUI(this);
			tsFacade = new TSModelFacade(this.DataProvider);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FTSErrorCodeSelect));
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.ucLabEditProduct = new UserControl.UCLabelEdit();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.ucButtonSave = new UserControl.UCButton();
			this.ucButtonExit = new UserControl.UCButton();
			this.errorCodeSelect = new UserControl.UCErrorCodeSelect();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.ucLabEditProduct);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(536, 48);
			this.groupBox2.TabIndex = 166;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "产品信息";
			// 
			// ucLabEditProduct
			// 
			this.ucLabEditProduct.AllowEditOnlyChecked = true;
			this.ucLabEditProduct.Caption = "产品序列号";
			this.ucLabEditProduct.Checked = false;
			this.ucLabEditProduct.EditType = UserControl.EditTypes.String;
			this.ucLabEditProduct.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.ucLabEditProduct.Location = new System.Drawing.Point(16, 16);
			this.ucLabEditProduct.MaxLength = 40;
			this.ucLabEditProduct.Multiline = false;
			this.ucLabEditProduct.Name = "ucLabEditProduct";
			this.ucLabEditProduct.PasswordChar = '\0';
			this.ucLabEditProduct.ReadOnly = true;
			this.ucLabEditProduct.ShowCheckBox = false;
			this.ucLabEditProduct.Size = new System.Drawing.Size(274, 24);
			this.ucLabEditProduct.TabIndex = 0;
			this.ucLabEditProduct.TabNext = true;
			this.ucLabEditProduct.Value = "";
			this.ucLabEditProduct.WidthType = UserControl.WidthTypes.Long;
			this.ucLabEditProduct.XAlign = 90;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.ucButtonSave);
			this.groupBox1.Controls.Add(this.ucButtonExit);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(0, 446);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(536, 56);
			this.groupBox1.TabIndex = 293;
			this.groupBox1.TabStop = false;
			// 
			// ucButtonSave
			// 
			this.ucButtonSave.BackColor = System.Drawing.SystemColors.Control;
			this.ucButtonSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonSave.BackgroundImage")));
			this.ucButtonSave.ButtonType = UserControl.ButtonTypes.Save;
			this.ucButtonSave.Caption = "保存";
			this.ucButtonSave.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ucButtonSave.Location = new System.Drawing.Point(144, 16);
			this.ucButtonSave.Name = "ucButtonSave";
			this.ucButtonSave.Size = new System.Drawing.Size(88, 22);
			this.ucButtonSave.TabIndex = 11;
			this.ucButtonSave.Click += new System.EventHandler(this.ucButtonSave_Click);
			// 
			// ucButtonExit
			// 
			this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
			this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
			this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
			this.ucButtonExit.Caption = "退出";
			this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ucButtonExit.Location = new System.Drawing.Point(280, 16);
			this.ucButtonExit.Name = "ucButtonExit";
			this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
			this.ucButtonExit.TabIndex = 9;
			this.ucButtonExit.Click += new System.EventHandler(this.ucButtonExit_Click);
			// 
			// errorCodeSelect
			// 
			this.errorCodeSelect.AddButtonTop = 101;
			this.errorCodeSelect.Dock = System.Windows.Forms.DockStyle.Fill;
			this.errorCodeSelect.Location = new System.Drawing.Point(0, 48);
			this.errorCodeSelect.Name = "errorCodeSelect";
			this.errorCodeSelect.RemoveButtonTop = 189;
			this.errorCodeSelect.Size = new System.Drawing.Size(536, 398);
			this.errorCodeSelect.TabIndex = 294;
			this.errorCodeSelect.SelectedIndexChanged += new System.EventHandler(this.errorCodeSelect_SelectedIndexChanged);
			// 
			// FTSErrorCodeSelect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(536, 502);
			this.Controls.Add(this.errorCodeSelect);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FTSErrorCodeSelect";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "不良信息编辑";
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public string ItemCode
		{
			get
			{
				return ucLabEditProduct.Value;
			}
			set
			{
				ucLabEditProduct.Value = value;
				this.InitForm();
			}
		}
		private object[] _SelectedErrorCode = null;
		public object[] SelectedErrorCode
		{
			get
			{
				return this._SelectedErrorCode;
			}
			set
			{
				this._SelectedErrorCode = value;
			}
		}

		private void InitForm()
		{			
			object[] errorCodeGroups = tsFacade.GetErrorCodeGroupByItemCode(ItemCode);
			if (errorCodeGroups!= null)
			{
				errorCodeSelect.ClearErrorGroup();
				errorCodeSelect.ClearSelectedErrorCode();
				errorCodeSelect.ClearSelectErrorCode();
				errorCodeSelect.AddErrorGroups(errorCodeGroups);

			}

			if(this.SelectedErrorCode != null)
			{
				this.errorCodeSelect.AddSelectedErrorCodes(this.SelectedErrorCode);
			}
		}

		private IDomainDataProvider _domainDataProvider =ApplicationService.Current().DataProvider;
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		private void errorCodeSelect_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			TSModelFacade tsFacade = new TSModelFacade(this.DataProvider);
			object[] errorCodes = tsFacade.GetSelectedErrorCodeByErrorCodeGroupCode(errorCodeSelect.SelectedErrorCodeGroup);
			if (errorCodes!= null)
			{
				errorCodeSelect.ClearSelectErrorCode();
				errorCodeSelect.AddErrorCodes(errorCodes);
			}
		}

		private void ucButtonExit_Click(object sender, System.EventArgs e)
		{
			//this.SelectedErrorCode = null;
			this.Close();
		}

		private void ucButtonSave_Click(object sender, System.EventArgs e)
		{
			this.SelectedErrorCode = errorCodeSelect.GetSelectedErrorCodes();
			if(this.SelectedErrorCode == null || this.SelectedErrorCode.Length < 1)
			{
				if(MessageBox.Show(this,"请选择不良代码","请选择不良代码",MessageBoxButtons.OKCancel) == DialogResult.Cancel)
					this.Close();
			}
			else
			{
				this.Close();
			}
		}
	}
}
