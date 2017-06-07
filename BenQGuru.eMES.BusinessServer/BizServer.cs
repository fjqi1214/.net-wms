using System;
using System.Runtime.Remoting;
using System.Windows.Forms;

namespace Frame.WinFormBizServer
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class BizServer : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblStartTime;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ListBox EntriesList;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public BizServer()
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
				if (components != null) 
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblStartTime = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.EntriesList = new System.Windows.Forms.ListBox();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lblStartTime);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(416, 32);
			this.panel1.TabIndex = 5;
			// 
			// lblStartTime
			// 
			this.lblStartTime.Location = new System.Drawing.Point(148, 4);
			this.lblStartTime.Name = "lblStartTime";
			this.lblStartTime.Size = new System.Drawing.Size(264, 24);
			this.lblStartTime.TabIndex = 6;
			this.lblStartTime.Text = "启动时间:";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((System.Byte)(134)));
			this.label1.Location = new System.Drawing.Point(4, 4);
			this.label1.Name = "label1";
			this.label1.TabIndex = 5;
			this.label1.Text = "已注册类型：";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.EntriesList);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 32);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(416, 309);
			this.panel2.TabIndex = 6;
			// 
			// EntriesList
			// 
			this.EntriesList.BackColor = System.Drawing.Color.White;
			this.EntriesList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.EntriesList.ForeColor = System.Drawing.Color.Black;
			this.EntriesList.ItemHeight = 12;
			this.EntriesList.Location = new System.Drawing.Point(0, 0);
			this.EntriesList.Name = "EntriesList";
			this.EntriesList.Size = new System.Drawing.Size(416, 304);
			this.EntriesList.TabIndex = 4;
			// 
			// BizServer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(416, 341);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BizServer";
			this.Text = "MES V3.0 APP Server";
			this.Load += new System.EventHandler(this.BizServer_Load);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new BizServer());
		}

		private void BizServer_Load(object sender, System.EventArgs e)
		{
			

			int iCount = 0;
			try
			{
				RemotingConfiguration.Configure("BenQGuru.eMES.BusinessServer.exe.config");

				//this.EntriesList.Items.Add( "ApplicationName:" + RemotingConfiguration.ApplicationName );

				iCount = RemotingConfiguration.GetRegisteredActivatedServiceTypes().Length;
				for( int i=0;i<iCount;i++ )
				{
					this.EntriesList.Items.Add( RemotingConfiguration.GetRegisteredActivatedServiceTypes()[i].TypeName );
				}

				lblStartTime.Text = "启动时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			}
			catch(Exception ex)
			{
				BenQGuru.eMES.Common.Log.Error(ex.Message);
				throw ex;
			}
			finally
			{
				//RemotingConfiguration.
			}

		}

		
	}
}
