using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UserControl
{
	public class UserControl1 : UserControl.UCButton
	{
		private System.ComponentModel.IContainer components = null;

		public UserControl1()
		{
			// 该调用是 Windows 窗体设计器所必需的。
			InitializeComponent();
			//InitializeLayout();

			// TODO: 在 InitializeComponent 调用后添加任何初始化
		}

		private void InitializeLayout()
		{

			if (this.DesignMode)
			{
				if(this.Container.Components.Count>0)
				{
					object obj = this.Container.Components[0];
					if (obj is System.Windows.Forms.Form)
					{
					   System.Windows.Forms.MessageBox.Show(obj.ToString());  
					}

					System.Windows.Forms.MessageBox.Show(obj.ToString()); 
				}
			}

		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region 设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// UserControl1
			// 
			this.Name = "UserControl1";

		}
		#endregion
		protected override void OnPaint(PaintEventArgs e)
		{

			base.OnPaint (e);
		}
		public bool _autoPanle;
		[Bindable(true),
		Category("外观")]
		public bool AutoPanle
		{
			get
			{
				return _autoPanle;
			}
			set
			{
				_autoPanle=value;
				this.Left =200-this.Width;

			}
		}

	}
}

