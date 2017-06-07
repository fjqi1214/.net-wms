using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace UserControl
{
	/// <summary>
	/// UCWatchPanelContent 的摘要说明。
	/// </summary>
	public class UCWatchPanelContent : System.Windows.Forms.UserControl,ICalculateWatchPanel
	{
		private UserControl.UCWatchPanelLeft ubLeft;
		private System.Windows.Forms.Panel pnlLeft;
		private System.Windows.Forms.Panel pnlFill;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UCWatchPanelContent()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化

			this.WatchPanels.Add(ubLeft);
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
			this.ubLeft = new UserControl.UCWatchPanelLeft();
			this.pnlLeft = new System.Windows.Forms.Panel();
			this.pnlFill = new System.Windows.Forms.Panel();
			this.pnlLeft.SuspendLayout();
			this.SuspendLayout();
			// 
			// ubLeft
			// 
			this.ubLeft.Location = new System.Drawing.Point(0, 0);
			this.ubLeft.Name = "ubLeft";
			this.ubLeft.ResCode = "";
			this.ubLeft.Size = new System.Drawing.Size(200, 143);
			this.ubLeft.TabIndex = 0;
			// 
			// pnlLeft
			// 
			this.pnlLeft.Controls.Add(this.ubLeft);
			this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlLeft.Location = new System.Drawing.Point(0, 0);
			this.pnlLeft.Name = "pnlLeft";
			this.pnlLeft.Size = new System.Drawing.Size(200, 143);
			this.pnlLeft.TabIndex = 1;
			// 
			// pnlFill
			// 
			this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlFill.Location = new System.Drawing.Point(200, 0);
			this.pnlFill.Name = "pnlFill";
			this.pnlFill.Size = new System.Drawing.Size(744, 143);
			this.pnlFill.TabIndex = 2;
			// 
			// UCWatchPanelContent
			// 
			this.Controls.Add(this.pnlFill);
			this.Controls.Add(this.pnlLeft);
			this.Name = "UCWatchPanelContent";
			this.Size = new System.Drawing.Size(944, 143);
			this.pnlLeft.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public string ResCode
		{
			get
			{
				return this.ubLeft.ResCode;
			}
			set
			{
				this.ubLeft.ResCode = value;
			}
		}

		public ArrayList WatchPanels = new ArrayList();
		public ArrayList WatchPanelCenters = new ArrayList();

		public UCWatchPanelCenter FindUCWatchPanelCenter(string TPCode)
		{
			foreach(UCWatchPanelCenter UCCenter in WatchPanelCenters)
			{
				if(UCCenter.TPCode == TPCode)
					return UCCenter;
			}

			return null;
		}

		public void ResetUCWatchPanelCenterValue()
		{
			foreach(UCWatchPanelCenter UCCenter in WatchPanelCenters)
			{
				UCCenter.ResetControlValue();
			}

			UCCenterTotal.ResetControlValue();
		}

		public void HideHeader()
		{
			foreach(IWatchPanel watchPanel in this.WatchPanels)
			{
				watchPanel.HideHeader();
			}

			if(this.WatchPanels.Count > 0)
			{
				this.Height = this.Height - ((IWatchPanel)this.WatchPanels[0]).HeaderHeight;//- 23;
			}
		}

		public void HideBottom()
		{
			foreach(IWatchPanel watchPanel in this.WatchPanels)
			{
				watchPanel.HideBottom();
			}

			//this.Height = this.Height - 30*2;

			if(this.WatchPanels.Count > 0)
			{
				this.Height = this.Height - ((IWatchPanel)this.WatchPanels[0]).BottomHeight;//- 23;
			}
		}

		public int HeaderHeight
		{
			get
			{
				return 0;//this.pnlHeader.Height;
			}
		}

		public int BottomHeight
		{
			get
			{
				return 0;//this.pnlBottom.Height;
			}
		}

		public void RemoveWatchPanelCenters()
		{
			foreach(UCWatchPanelCenter UCCenter in WatchPanelCenters)
			{
				this.Controls.Remove(UCCenter);
				this.WatchPanels.Remove(UCCenter);
			}

			this.WatchPanelCenters.Clear();
		}


		public void CreateWatchPanelCenters(ArrayList TimePeriods)
		{
			RemoveWatchPanelCenters();

			UCCenterTotal = new UCWatchPanelCenter();
			UCCenterTotal.TPCode = "";
			UCCenterTotal.TPHeader = "Total";

			UCCenterTotal.Dock = System.Windows.Forms.DockStyle.Left;

			this.WatchPanels.Add(UCCenterTotal);
			this.pnlFill.Controls.Add(UCCenterTotal);

			for(int i = TimePeriods.Count - 1 ; i >= 0 ; i--)
			{
				TimePeriodForWatchPanel tp = (TimePeriodForWatchPanel)TimePeriods[i];

				UCWatchPanelCenter UCCenter = new UCWatchPanelCenter();
				UCCenter.TPCode = tp.TPCode;
				UCCenter.TPHeader = tp.TPHeader;

				UCCenter.Dock = System.Windows.Forms.DockStyle.Left;

				this.WatchPanelCenters.Add(UCCenter);
				this.WatchPanels.Add(UCCenter);
			}

			foreach(UCWatchPanelCenter UCCenter in this.WatchPanelCenters)
			{
				this.pnlFill.Controls.Add(UCCenter);
			}

			
		}

		private UCWatchPanelCenter UCCenterTotal;

		//public decimal[] Actual

		private ICalculateStrategy Stategy;

		public void SetCalculateStrategy(ICalculateStrategy Stategy)
		{
			this.Stategy = Stategy;

			foreach(UCWatchPanelCenter UCCenter in WatchPanelCenters)
			{
				UCCenter.SetCalculateStrategy(Stategy);
			}

			UCCenterTotal.SetCalculateStrategy(Stategy);
		}

		public void CalculateActual()
		{
			UCCenterTotal.ResetControlValue();
			foreach(UCWatchPanelCenter UCCenter in WatchPanelCenters)
			{
				UCCenter.CalculateActual();

				UCCenterTotal.Input += UCCenter.Input;
				UCCenterTotal.Defects += UCCenter.Defects;
			}

			UCCenterTotal.CalculateActual();
		}

		public void SetFPYGoal(decimal fpygoal)
		{
			foreach(UCWatchPanelCenter UCCenter in WatchPanelCenters)
			{
				UCCenter.SetFPYGoal(fpygoal);
			}

			UCCenterTotal.FPYGoal = fpygoal;
		}
	}
}
