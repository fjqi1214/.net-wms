using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Web.Helper;
using UserControl;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FInofForm 的摘要说明。
	/// </summary>
	public class FInfoForm : System.Windows.Forms.Form
	{
		private UserControl.UCMessage memInfor;
		private UserControl.UCButton ucButtonClear;

        private IDomainDataProvider _domainDataProvider = Service.ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FInfoForm()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FInfoForm));
            this.memInfor = new UserControl.UCMessage();
            this.ucButtonClear = new UserControl.UCButton();
            this.SuspendLayout();
            // 
            // memInfor
            // 
            this.memInfor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.memInfor.AutoScroll = true;
            this.memInfor.BackColor = System.Drawing.Color.Gainsboro;
            this.memInfor.ButtonVisible = false;
            this.memInfor.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.memInfor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.memInfor.Location = new System.Drawing.Point(0, 0);
            this.memInfor.Name = "memInfor";
            this.memInfor.Size = new System.Drawing.Size(496, 244);
            this.memInfor.TabIndex = 2;
            this.memInfor.WorkingErrorAdded += new UserControl.WorkingErrorAddedEventHandler(this.memInfor_WorkingErrorAdded);
            // 
            // ucButtonClear
            // 
            this.ucButtonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ucButtonClear.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonClear.BackgroundImage")));
            this.ucButtonClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucButtonClear.ButtonType = UserControl.ButtonTypes.Refresh;
            this.ucButtonClear.Caption = "清除";
            this.ucButtonClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonClear.Location = new System.Drawing.Point(400, 253);
            this.ucButtonClear.Name = "ucButtonClear";
            this.ucButtonClear.Size = new System.Drawing.Size(89, 21);
            this.ucButtonClear.TabIndex = 3;
            this.ucButtonClear.Click += new System.EventHandler(this.ucButton1_Click);
            // 
            // FInfoForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.ClientSize = new System.Drawing.Size(496, 280);
            this.Controls.Add(this.ucButtonClear);
            this.Controls.Add(this.memInfor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FInfoForm";
            this.Text = "信息";
            this.Load += new System.EventHandler(this.FInfoForm_Load);
            this.ResumeLayout(false);

		}
		#endregion
//		public void Add(UserControl.MessageEventArgsUnit messages)
//		{
//				memInfor.Add(messages);
//		}

		public void Add(string text)
		{
			memInfor.Add(text);
		}

		public void Add(UserControl.Message message)
		{
			UserControl.Messages messages = new UserControl.Messages();
			messages.Add(message);
			this.Add(messages);
		}

		public void Add(UserControl.Messages messages)
		{
			memInfor.Add(messages);
		}

        public void AddEx(string text)
        {
            Add(text);
        }

        public void AddEx(string function, string inputContent, UserControl.Message message, bool recordWorkingError)
        {
            UserControl.Messages messages = new UserControl.Messages();
            messages.Add(message);
            AddEx(function, inputContent, messages, recordWorkingError);
        }

        public void AddEx(string function, string inputContent, Messages messages, bool recordWorkingError)
        {
            memInfor.AddEx(function, inputContent, messages, recordWorkingError);
        }

		public void Clear()
		{
			memInfor.Clear();
		}

		private void FInfoForm_Load(object sender, System.EventArgs e)
		{
//			this.Width=256;
//			this.Left=0;
//			this.Top=512;
		}
		//Amoi,Laws Lu,2005/08/01,添加	清除信息框
		private void ucButton1_Click(object sender, System.EventArgs e)
		{
			memInfor.Clear();
		}

        private void memInfor_WorkingErrorAdded(object sender, UserControl.WorkingErrorAddedEventArgs e)
        {
            CSHelper.ucMessageWorkingErrorAdded(e, this.DataProvider);
        }
	}
}
