using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace UserControl
{
	public class UCButton : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label labUC;
		private System.Windows.Forms.ImageList imageListBack;
		private System.ComponentModel.IContainer components = null;

		public UCButton()
		{
			// 该调用是 Windows 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化
			this.BackgroundImage =imageListBack.Images[0];
			//ChangeCaption();
			labUC.Left=(this.Width-labUC.Width)/2;
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UCButton));
			this.labUC = new System.Windows.Forms.Label();
			this.imageListBack = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// labUC
			// 
			this.labUC.AutoSize = true;
			this.labUC.BackColor = System.Drawing.Color.Transparent;
			this.labUC.Location = new System.Drawing.Point(31, 6);
			this.labUC.Name = "labUC";
			this.labUC.Size = new System.Drawing.Size(29, 17);
			this.labUC.TabIndex = 0;
			this.labUC.Text = "确定";
			this.labUC.Click += new System.EventHandler(this.labUC_Click);
			this.labUC.SizeChanged += new System.EventHandler(this.labUC_SizeChanged);
			this.labUC.MouseEnter += new System.EventHandler(this.labUC_MouseEnter);
			this.labUC.MouseLeave += new System.EventHandler(this.labUC_MouseLeave);
			// 
			// imageListBack
			// 
			this.imageListBack.ImageSize = new System.Drawing.Size(88, 22);
			this.imageListBack.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListBack.ImageStream")));
			this.imageListBack.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// UCButton
			// 
			this.BackColor = System.Drawing.SystemColors.Control;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Controls.Add(this.labUC);
			this.Cursor = System.Windows.Forms.Cursors.Hand;
			this.Name = "UCButton";
			this.Size = new System.Drawing.Size(88, 22);
			this.MouseEnter += new System.EventHandler(this.UCButton_MouseEnter);
			this.MouseLeave += new System.EventHandler(this.UCButton_MouseLeave);
			this.ResumeLayout(false);

		}
		#endregion

		
		protected override void InitLayout()
		{
			//ChangeCaption();
			labUC.Left=(this.Width-labUC.Width)/2;
			base.InitLayout ();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			
			//ChangeCaption();
			labUC.Left=(this.Width-labUC.Width)/2;
			base.OnPaint (e);
		}

		private void UCButton_MouseEnter(object sender, System.EventArgs e)
		{
			this.BackgroundImage =imageListBack.Images[1];
		}

		private void UCButton_MouseLeave(object sender, System.EventArgs e)
		{
			this.BackgroundImage =imageListBack.Images[0];
			
		}

		private void labUC_MouseEnter(object sender, System.EventArgs e)
		{
			UCButton_MouseEnter(sender,e);
		
		}

		private void labUC_MouseLeave(object sender, System.EventArgs e)
		{
			UCButton_MouseLeave(sender,e);
		
		}

		private void labUC_Click(object sender, System.EventArgs e)
		{
			this.OnClick (e);			
		}
		private Form getControlForm(Control control)
		{
			if (control is Form )
				return (Form)control;
			else
				return getControlForm(control.Parent);
		}
		private void labUC_CloseClick(object sender, System.EventArgs e)
		{
			getControlForm(this).Close();			
		}
		protected override void OnClick(EventArgs e)
		{
			base.OnClick (e);
			if (this._buttonType ==ButtonTypes.Exit)
				labUC_CloseClick(this,null);
		}


		[Bindable(true),
		Category("外观")]
		public System.Windows.Forms.ImageList.ImageCollection Images
		{
			get
			{
				return imageListBack.Images;
			}
		}
		[Bindable(true),
		Category("外观")]
		public Label LabelText
		{
			get
			{
				return labUC;
			}
		}
		[Bindable(true),
		Category("外观")]
		public string Caption
		{
			get
			{
				return labUC.Text;
			}
			set
			{
				labUC.Text =value;
			}
		}
		private ButtonTypes _buttonType=ButtonTypes.None;
		[Bindable(true),
		Category("外观")]
		public ButtonTypes ButtonType
		{
			get
			{
				return _buttonType;
			}
			set
			{
				_buttonType =value;
                //ChangeCaption();
				
			}
		}
        //private void ChangeCaption()
        //{
        //    switch (_buttonType)
        //    {
        //        case ButtonTypes.Add :
        //            this.Caption ="新增";
        //            break;
        //        case ButtonTypes.Cancle :
        //            this.Caption ="取消";
        //            break;
        //        case ButtonTypes.Confirm :
        //            this.Caption ="确认";
        //            break;
        //        case ButtonTypes.Delete  :
        //            this.Caption ="删除";
        //            break;
        //        case ButtonTypes.Edit  :
        //            this.Caption ="编辑";
        //            break;
        //        case ButtonTypes.Exit  :
        //            this.Caption ="退出";					
        //            break;
        //        case ButtonTypes.Query  :
        //            this.Caption ="查询";
        //            break;
        //        case ButtonTypes.Refresh:
        //            this.Caption ="刷新";
        //            break;
        //        case ButtonTypes.Save  :
        //            this.Caption ="保存";
        //            break;
        //        case ButtonTypes.Copy  :
        //            this.Caption ="复制";
        //            break;
        //        case ButtonTypes.AllLeft  :
        //            this.Caption ="<<";
        //            break;
        //        case ButtonTypes.AllRight  :
        //            this.Caption =">>";
        //            break;
        //        case ButtonTypes.Change :
        //            this.Caption ="更正";
        //            break;
        //        case ButtonTypes.Move  :
        //            this.Caption ="移除";
        //            break;
        //    }
			
        //}

		private void labUC_SizeChanged(object sender, System.EventArgs e)
		{
            //ChangeCaption();
			labUC.Left=(this.Width-labUC.Width)/2;
		}

	}
}

