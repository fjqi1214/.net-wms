using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace UserControl
{
	public delegate void HeaderTextChangedHandle();

	/// <summary>
	/// UCWatchPanelHeader 的摘要说明。
	/// </summary>
	public class UCWatchPanelHeader : System.Windows.Forms.UserControl
	{
		public UserControl.UCLabelEdit txtPartNo;
		public UserControl.UCLabelEdit txtModel;
		public UserControl.UCLabelEdit txtDarfonParNo;
		public UserControl.UCLabelEdit txtLine;
		public UserControl.UCLabelEdit txtDate;
		public UserControl.UCLabelEdit txtManuFacture;

		public event HeaderTextChangedHandle HeaderTextChanged;

		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UCWatchPanelHeader()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化

			this.txtDate.Value = DateTime.Today.ToShortDateString();
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
			this.txtPartNo = new UserControl.UCLabelEdit();
			this.txtModel = new UserControl.UCLabelEdit();
			this.txtDarfonParNo = new UserControl.UCLabelEdit();
			this.txtLine = new UserControl.UCLabelEdit();
			this.txtDate = new UserControl.UCLabelEdit();
			this.txtManuFacture = new UserControl.UCLabelEdit();
			this.SuspendLayout();
			// 
			// txtPartNo
			// 
			this.txtPartNo.AllowEditOnlyChecked = true;
			this.txtPartNo.Caption = "Customer Part No";
			this.txtPartNo.Checked = false;
			this.txtPartNo.EditType = UserControl.EditTypes.String;
			this.txtPartNo.Location = new System.Drawing.Point(8, 8);
			this.txtPartNo.MaxLength = 40;
			this.txtPartNo.Multiline = false;
			this.txtPartNo.Name = "txtPartNo";
			this.txtPartNo.PasswordChar = '\0';
			this.txtPartNo.ReadOnly = false;
			this.txtPartNo.ShowCheckBox = false;
			this.txtPartNo.Size = new System.Drawing.Size(244, 24);
			this.txtPartNo.TabIndex = 0;
			this.txtPartNo.TabNext = true;
			this.txtPartNo.Value = "";
			this.txtPartNo.WidthType = UserControl.WidthTypes.Normal;
			this.txtPartNo.XAlign = 119;
			// 
			// txtModel
			// 
			this.txtModel.AllowEditOnlyChecked = true;
			this.txtModel.Caption = "Customer Model";
			this.txtModel.Checked = false;
			this.txtModel.EditType = UserControl.EditTypes.String;
			this.txtModel.Location = new System.Drawing.Point(264, 8);
			this.txtModel.MaxLength = 40;
			this.txtModel.Multiline = false;
			this.txtModel.Name = "txtModel";
			this.txtModel.PasswordChar = '\0';
			this.txtModel.ReadOnly = false;
			this.txtModel.ShowCheckBox = false;
			this.txtModel.Size = new System.Drawing.Size(232, 24);
			this.txtModel.TabIndex = 1;
			this.txtModel.TabNext = true;
			this.txtModel.Value = "";
			this.txtModel.WidthType = UserControl.WidthTypes.Normal;
			this.txtModel.XAlign = 363;
			// 
			// txtDarfonParNo
			// 
			this.txtDarfonParNo.AllowEditOnlyChecked = true;
			this.txtDarfonParNo.Caption = "DFS Part No";
			this.txtDarfonParNo.Checked = false;
			this.txtDarfonParNo.EditType = UserControl.EditTypes.String;
			this.txtDarfonParNo.Location = new System.Drawing.Point(40, 40);
			this.txtDarfonParNo.MaxLength = 40;
			this.txtDarfonParNo.Multiline = false;
			this.txtDarfonParNo.Name = "txtDarfonParNo";
			this.txtDarfonParNo.PasswordChar = '\0';
			this.txtDarfonParNo.ReadOnly = true;
			this.txtDarfonParNo.ShowCheckBox = false;
			this.txtDarfonParNo.Size = new System.Drawing.Size(214, 24);
			this.txtDarfonParNo.TabIndex = 3;
			this.txtDarfonParNo.TabNext = true;
			this.txtDarfonParNo.Value = "";
			this.txtDarfonParNo.WidthType = UserControl.WidthTypes.Normal;
			this.txtDarfonParNo.XAlign = 121;
			this.txtDarfonParNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLine_TxtboxKeyPress);
			// 
			// txtLine
			// 
			this.txtLine.AllowEditOnlyChecked = true;
			this.txtLine.Caption = "Line";
			this.txtLine.Checked = false;
			this.txtLine.EditType = UserControl.EditTypes.String;
			this.txtLine.Location = new System.Drawing.Point(328, 40);
			this.txtLine.MaxLength = 40;
			this.txtLine.Multiline = false;
			this.txtLine.Name = "txtLine";
			this.txtLine.PasswordChar = '\0';
			this.txtLine.ReadOnly = true;
			this.txtLine.ShowCheckBox = false;
			this.txtLine.Size = new System.Drawing.Size(170, 24);
			this.txtLine.TabIndex = 4;
			this.txtLine.TabNext = true;
			this.txtLine.Value = "";
			this.txtLine.WidthType = UserControl.WidthTypes.Normal;
			this.txtLine.XAlign = 365;
			this.txtLine.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLine_TxtboxKeyPress);
			// 
			// txtDate
			// 
			this.txtDate.AllowEditOnlyChecked = true;
			this.txtDate.Caption = "Date";
			this.txtDate.Checked = false;
			this.txtDate.EditType = UserControl.EditTypes.String;
			this.txtDate.Location = new System.Drawing.Point(592, 40);
			this.txtDate.MaxLength = 40;
			this.txtDate.Multiline = false;
			this.txtDate.Name = "txtDate";
			this.txtDate.PasswordChar = '\0';
			this.txtDate.ReadOnly = true;
			this.txtDate.ShowCheckBox = false;
			this.txtDate.Size = new System.Drawing.Size(170, 24);
			this.txtDate.TabIndex = 5;
			this.txtDate.TabNext = true;
			this.txtDate.Value = "";
			this.txtDate.WidthType = UserControl.WidthTypes.Normal;
			this.txtDate.XAlign = 629;
			// 
			// txtManuFacture
			// 
			this.txtManuFacture.AllowEditOnlyChecked = true;
			this.txtManuFacture.Caption = "Manufacture site";
			this.txtManuFacture.Checked = false;
			this.txtManuFacture.EditType = UserControl.EditTypes.String;
			this.txtManuFacture.Location = new System.Drawing.Point(520, 8);
			this.txtManuFacture.MaxLength = 40;
			this.txtManuFacture.Multiline = false;
			this.txtManuFacture.Name = "txtManuFacture";
			this.txtManuFacture.PasswordChar = '\0';
			this.txtManuFacture.ReadOnly = false;
			this.txtManuFacture.ShowCheckBox = false;
			this.txtManuFacture.Size = new System.Drawing.Size(244, 24);
			this.txtManuFacture.TabIndex = 2;
			this.txtManuFacture.TabNext = true;
			this.txtManuFacture.Value = "HID DARFON SUZHOU";
			this.txtManuFacture.WidthType = UserControl.WidthTypes.Normal;
			this.txtManuFacture.XAlign = 631;
			// 
			// UCWatchPanelHeader
			// 
			this.BackColor = System.Drawing.Color.PaleTurquoise;
			this.Controls.Add(this.txtDate);
			this.Controls.Add(this.txtManuFacture);
			this.Controls.Add(this.txtLine);
			this.Controls.Add(this.txtDarfonParNo);
			this.Controls.Add(this.txtModel);
			this.Controls.Add(this.txtPartNo);
			this.Name = "UCWatchPanelHeader";
			this.Size = new System.Drawing.Size(920, 72);
			this.ResumeLayout(false);

		}
		#endregion

		private void txtLine_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r' && this.HeaderTextChanged != null)
				this.HeaderTextChanged();
		}

		public string Line
		{
			get
			{
				return this.txtLine.Value;
			}
			set
			{
				this.txtLine.Value = value;
			}
		}

		public string ItemCode
		{
			get
			{
				return this.txtDarfonParNo.Value;
			}
			set
			{
				this.txtDarfonParNo.Value = value;
			}
		}
	}
}
