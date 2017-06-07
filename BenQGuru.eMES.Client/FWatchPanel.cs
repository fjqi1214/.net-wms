using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Web.Helper;
using UserControl;
using BenQGuru.eMES.WatchPanel;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FWatchPanel 的摘要说明。
	/// </summary>
	public class FWatchPanel : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel pnlCenter;
		private UserControl.UCWatchPanelHeader ucHeader;
		private System.ComponentModel.IContainer components;
		private WatchPanelFacade _facade;
		private System.Windows.Forms.Timer timer1;
	
		public WatchPanelFacade facade
		{
			get
			{
				if(_facade == null)
					_facade = new WatchPanelFacade(this.DataProvider);

				return _facade;
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

		public FWatchPanel()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//

			//this.Visible = false;

			GetShiftCode();
			//CreateFormControl();
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
			this.components = new System.ComponentModel.Container();
			this.panel1 = new System.Windows.Forms.Panel();
			this.ucHeader = new UserControl.UCWatchPanelHeader();
			this.pnlCenter = new System.Windows.Forms.Panel();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.ucHeader);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(920, 72);
			this.panel1.TabIndex = 0;
			// 
			// ucHeader
			// 
			this.ucHeader.BackColor = System.Drawing.Color.PaleTurquoise;
			this.ucHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucHeader.ItemCode = "";
			this.ucHeader.Line = "";
			this.ucHeader.Location = new System.Drawing.Point(0, 0);
			this.ucHeader.Name = "ucHeader";
			this.ucHeader.Size = new System.Drawing.Size(920, 72);
			this.ucHeader.TabIndex = 0;
			this.ucHeader.HeaderTextChanged += new UserControl.HeaderTextChangedHandle(this.ucHeader_HeaderTextChanged);
			// 
			// pnlCenter
			// 
			this.pnlCenter.BackColor = System.Drawing.Color.White;
			this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlCenter.Location = new System.Drawing.Point(0, 72);
			this.pnlCenter.Name = "pnlCenter";
			this.pnlCenter.Size = new System.Drawing.Size(920, 462);
			this.pnlCenter.TabIndex = 1;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 900000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// FWatchPanel
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(920, 534);
			this.Controls.Add(this.pnlCenter);
			this.Controls.Add(this.panel1);
			this.Name = "FWatchPanel";
			this.Text = "Darfon Maggie FPY  及時看版";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Resize += new System.EventHandler(this.FWatchPanel_Resize);
			this.Load += new System.EventHandler(this.FWatchPanel_Load);
			this.MaximumSizeChanged += new System.EventHandler(this.FWatchPanel_MaximumSizeChanged);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FWatchPanel_KeyUp);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion



		private string _ShiftCode = "";
		private string ShiftCode
		{
			get
			{
				return _ShiftCode;
			}
			set
			{
				if(value != _ShiftCode)
				{
					_ShiftCode = value;

					ReCreateUCCenter();
				}
			}
		}

		private void ReCreateUCCenter()
		{
			this.pnlCenter.Controls.Clear();
			this.WatchPanelContents.Clear();

			CreateFormControl();

			RefreshData();
//			//获得时段
//			ArrayList tpHeaders = GetTPHeaders();
//
//			for( int i = 0 ; i< this.WatchPanelContents.Count ; i++)
//			{
//				UCWatchPanelContent content = this.WatchPanelContents[i] as UCWatchPanelContent;
//
//				content.RemoveWatchPanelCenters();
//				content.CreateWatchPanelCenters(tpHeaders);
//
//				//反的
//				if(i != 0 )
//				{
//					content.HideBottom();
//				}
//
//				if(i != this.WatchPanelContents.Count - 1)
//				{
//					content.HideHeader();
//				}
//			}
		}
		
		private void GetShiftCode()
		{
			Resource resource = ApplicationService.Current().LoginInfo.Resource;

			this.ucHeader.Line = resource.StepSequenceCode;

			//string SSCode = resource.StepSequenceCode;
			//string ShiftCode = ApplicationService.Current().LoginInfo.Resource.
			Shift shift = this.facade.GetShift(resource.ShiftTypeCode) as Shift;

			if(shift == null)
				return;

			this.ShiftCode = shift.ShiftCode;		
		}

		private ArrayList GetTPHeaders()
		{
			object[] tps = this.facade.GetTimePeriods(this.ShiftCode);

			ArrayList tpHeaders = new ArrayList();

			for(int i = 0 ; i < tps.Length ; i++)
			{
				TimePeriod tp = (TimePeriod)tps[i];

				TimePeriodForWatchPanel tpf = new TimePeriodForWatchPanel();
				tpf.TPCode = tp.TimePeriodCode;

				string BTime = tp.TimePeriodBeginTime.ToString();
				if(BTime.Length > 4)
				{
					BTime = BTime.Substring(0,BTime.Length - 4);
				}

				string ETime = tp.TimePeriodEndTime.ToString();
				if(ETime.Length > 4)
				{
					ETime = ETime.Substring(0,ETime.Length - 4);
				}

				tpf.TPHeader = BTime + ":00 - " + ETime + ":00";

				tpHeaders.Add(tpf);
			}

			return tpHeaders;
		}

		//private ArrayList Resources = new ArrayList();

		#region 创建控件
		private void CreateFormControl()
		{
			//获得时段
			ArrayList tpHeaders = GetTPHeaders();

			Resource resource = ApplicationService.Current().LoginInfo.Resource;

			//获得资源
			ArrayList reses = ConfigReader.Read();

			//this.pnlCenter.Visible = false;

			//this.pnlCenter.SuspendLayout();
			//this.SuspendLayout();
			
			//this.pnlCenter.b
			//创建控件
			for(int i = reses.Count - 1 ; i >= 0 ; i--)
			{
				ResourceConfig res = reses[i] as ResourceConfig;

				UCWatchPanelContent content = new UCWatchPanelContent();
				//content.Visible = false;
			
				//UCWatchPanelContent.re
				content.CreateWatchPanelCenters(tpHeaders);
				content.ResCode = res.ResCode;
				content.SetFPYGoal(res.FPYGoal);

				content.Dock = System.Windows.Forms.DockStyle.Top;
				

				//反的
				if(i != reses.Count - 1 )
				{
					content.HideBottom();
				}

				if(i != 0)
				{
					content.HideHeader();
				}

				this.WatchPanelContents.Add(content);
			}

			foreach(UCWatchPanelContent content in this.WatchPanelContents)
			{
				this.pnlCenter.Controls.Add(content);
				//content.Visible = true;
			
			}

			//this.pnlCenter.Visible = true;
			//this.pnlCenter.ResumeLayout(false);
			//this.ResumeLayout(false);
		}

		#endregion

		public ArrayList WatchPanelContents = new ArrayList();

		private void FWatchPanel_Load(object sender, System.EventArgs e)
		{
			//this.IsMdiChild = false;
			this.MdiParent = null;
			//this.WindowState = FormWindowState.Maximized;

			//this.Visible = true;
		}

		#region 刷新数据
		private void RefreshData()
		{
			//Resource resource = ApplicationService.Current().LoginInfo.Resource;

			if(this.ucHeader.Line == "" )
				return;

			object[] items = this.facade.GetItemCodes(this.ucHeader.Line,this.ShiftCode);
			
			this.ucHeader.ItemCode = "";

			if(items == null || items.Length == 0)
				return;
			
			foreach(Item item in items)
			{
				this.ucHeader.ItemCode += item.ItemCode + ",";
			}

			if(this.ucHeader.ItemCode == "")
				return;

			this.ucHeader.ItemCode = this.ucHeader.ItemCode.Substring(0,this.ucHeader.ItemCode.Length - 1);

			string ResCodes = "";
			foreach( UCWatchPanelContent content in this.WatchPanelContents)
			{
				ResCodes += content.ResCode + ",";
			}

			if(ResCodes == "")
				return;

			ResCodes = ResCodes.Substring(0,ResCodes.Length - 1);

			DataSet ds = this.facade.GetWatchingData(this.ucHeader.Line,this.ucHeader.ItemCode,this.ShiftCode,ResCodes);

			//			if(ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
			//				return;

			ICalculateStrategy strategy = new CalculateStategy();

			
			for(int i = this.WatchPanelContents.Count - 1 ; i >= 0; i--)
			{
				UCWatchPanelContent content = this.WatchPanelContents[i] as UCWatchPanelContent;

				content.ResetUCWatchPanelCenterValue();

				if(ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
					continue;

				DataRow[] rows = ds.Tables[0].Select(string.Format("RESCODE='{0}'",content.ResCode));

				if(rows == null || rows.Length == 0)
					continue;

				for(int k = 0 ; k < rows.Length ; k++)
				{
					UCWatchPanelCenter UCCenter = content.FindUCWatchPanelCenter(rows[k]["TPCODE"].ToString());

					if(UCCenter == null)
						continue;

					UCCenter.Input = decimal.Parse(rows[k]["input"].ToString());
					UCCenter.Defects = decimal.Parse(rows[k]["defects"].ToString());
					//UCCenter.CalculateActual();
				}

				content.SetCalculateStrategy(strategy);

				content.CalculateActual();

				

				//content = content;
				//content.CalculateActual();
			}

			#region 计算TotalActual

			//int g = 0;
			for(int i = this.WatchPanelContents.Count - 1 ; i >= 0; i--)
			{
				UCWatchPanelContent content = this.WatchPanelContents[i] as UCWatchPanelContent;

				if(i == this.WatchPanelContents.Count - 1 )
				{
					for(int j = 0 ; j < content.WatchPanelCenters.Count ; j++)
					{
						UCWatchPanelCenter c = (UCWatchPanelCenter)content.WatchPanelCenters[j];
						c.TotalActual = c.Actual;// * Convert.ToDecimal(Math.Pow(0.01,g));
					}

					//g++;
				}
				else
				{
					for(int j = 0 ; j < content.WatchPanelCenters.Count ; j++)
					{
						UCWatchPanelCenter c = (UCWatchPanelCenter)content.WatchPanelCenters[j];
						UCWatchPanelCenter c2 = (UCWatchPanelCenter)((UCWatchPanelContent)WatchPanelContents[i+1]).WatchPanelCenters[j];
						
						if(c2.TotalActual != 0 )
						{
							if(c.Actual != 0)
							{
								c.TotalActual = c.Actual*c2.TotalActual;// * Convert.ToDecimal(Math.Pow(0.01,g));
								//g++;
							}
							else
							{
								c.TotalActual = c2.TotalActual;
							}
						}
						else
						{
							c.TotalActual = c.Actual;
						}
					}
				}
				
			}


			if(this.WatchPanelContents.Count == 0)
				return;

			UCWatchPanelContent content1 = this.WatchPanelContents[0] as UCWatchPanelContent;

			for(int i = 0 ; i < content1.WatchPanelCenters.Count ; i++)
			{
				

				UCWatchPanelCenter c = (UCWatchPanelCenter)content1.WatchPanelCenters[i];

				DataRow[] rows = ds.Tables[0].Select(string.Format("TPCODE='{0}'",c.TPCode));

				int g = 0;
				for(int k = 0 ; k < rows.Length ; k++)
				{
					if(decimal.Parse(rows[k]["input"].ToString()) == decimal.Parse(rows[k]["defects"].ToString()))
						continue;

					g++;
				}

				if(g > 0)
					c.TotalActual = c.TotalActual / Convert.ToDecimal(Math.Pow(100,g - 1));
			}

			#endregion
		}

		#endregion

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			GetShiftCode();

			RefreshData();
		}

		private void ucHeader_HeaderTextChanged()
		{
			GetShiftCode();

			RefreshData();
		}

		private void FWatchPanel_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
//			if(e.KeyCode == Keys.Escape)
//			{
//				this.FormBorderStyle = FormBorderStyle.Sizable;
//				this.WindowState = FormWindowState.Normal;
//			}
		}

		private void FWatchPanel_MaximumSizeChanged(object sender, System.EventArgs e)
		{
			//this.FormBorderStyle = FormBorderStyle.None;
		}

		private void FWatchPanel_Resize(object sender, System.EventArgs e)
		{
//			if(WindowState == FormWindowState.Maximized) 
//			{ 
//				this.FormBorderStyle = FormBorderStyle.None;
//			} 
		}

	}

	public class CalculateStategy : ICalculateStrategy
	{
		public decimal Calculate(decimal Input,decimal Defects,decimal FPYGoal)
		{
			if(Input == 0)
				return -1;

			return (1 - Defects/Input) * 100;
		}
	}

	public class ResourceConfig
	{
		public string ResCode;
		public decimal FPYGoal;
	}

	public class ConfigReader
	{
		private static readonly string ConfigFile = "WatchPanel.pnl";

		public static ArrayList Read()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(ConfigFile);

			XmlNodeList nodes = doc.SelectNodes("/config/resource");

			ArrayList list = new ArrayList();

			foreach(XmlNode node in nodes)
			{
				ResourceConfig config = new ResourceConfig();

				if(ReadAttribute(node,"rescode") != "")
					config.ResCode = ReadAttribute(node,"rescode");
				
				if(ReadAttribute(node,"fpygoal") != "")
				{
					decimal fpygoal = 0;

					try
					{
						fpygoal = decimal.Parse(ReadAttribute(node,"fpygoal"));
					}
					catch
					{
					}

					config.FPYGoal = fpygoal;
				}

				if(config.ResCode != "")
					list.Add(config);
			}

			return list;
		}

		private static string ReadAttribute(XmlNode node ,string attribute)
		{
			if(node.Attributes[attribute] != null)
				return node.Attributes[attribute].Value;
			else
				return "";
		}
	}
}
