using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UserControl
{
	/// <summary>
	/// UCCoolToolBar 的摘要说明。
	/// </summary>
	public class UCCoolToolBar : System.Windows.Forms.UserControl
	{
		private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager ultraToolbarsManager;
		private System.Windows.Forms.ImageList imageList;
		private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _UCCoolToolBar_Toolbars_Dock_Area_Left;
		private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _UCCoolToolBar_Toolbars_Dock_Area_Right;
		private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _UCCoolToolBar_Toolbars_Dock_Area_Top;
		private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _UCCoolToolBar_Toolbars_Dock_Area_Bottom;
		private System.ComponentModel.IContainer components;

		public System.EventHandler OnToolBarAddClick = null;

		public UCCoolToolBar()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化

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
			this.components = new System.ComponentModel.Container();
			Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("Standard");
			Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Add");
			Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Delete");
			Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Save");
			Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Cancel");
			Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Refresh");
			Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Query");
			Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Exit");
			Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("StockOut");
			Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("StockIn");
			Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Add");
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Delete");
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool12 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Save");
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool13 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Cancel");
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool14 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Refresh");
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool15 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Query");
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool16 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Exit");
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool17 = new Infragistics.Win.UltraWinToolbars.ButtonTool("StockOut");
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool18 = new Infragistics.Win.UltraWinToolbars.ButtonTool("StockIn");
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UCCoolToolBar));
			this.ultraToolbarsManager = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this._UCCoolToolBar_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
			this._UCCoolToolBar_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
			this._UCCoolToolBar_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
			this._UCCoolToolBar_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
			((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager)).BeginInit();
			this.SuspendLayout();
			// 
			// ultraToolbarsManager
			// 
			this.ultraToolbarsManager.AlwaysShowFullMenus = true;
			this.ultraToolbarsManager.DockWithinContainer = this;
			this.ultraToolbarsManager.FlatMode = true;
			this.ultraToolbarsManager.ImageListSmall = this.imageList;
			this.ultraToolbarsManager.LockToolbars = true;
			this.ultraToolbarsManager.ShowFullMenusDelay = 500;
			this.ultraToolbarsManager.ShowShortcutsInToolTips = true;
			ultraToolbar1.DockedColumn = 0;
			ultraToolbar1.DockedRow = 0;
			appearance1.ImageHAlign = Infragistics.Win.HAlign.Center;
			appearance1.ImageVAlign = Infragistics.Win.VAlign.Top;
			appearance1.TextHAlign = Infragistics.Win.HAlign.Center;
			appearance1.TextVAlign = Infragistics.Win.VAlign.Bottom;
			ultraToolbar1.Settings.ToolAppearance = appearance1;
			ultraToolbar1.Settings.ToolDisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
			ultraToolbar1.Text = "Standard";
			ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
																							  buttonTool1,
																							  buttonTool2,
																							  buttonTool3,
																							  buttonTool4,
																							  buttonTool5,
																							  buttonTool6,
																							  buttonTool7,
																							  buttonTool8,
																							  buttonTool9});
			this.ultraToolbarsManager.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[] {
																												 ultraToolbar1});
			this.ultraToolbarsManager.ToolbarSettings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
			appearance2.Image = 15;
			buttonTool10.SharedProps.AppearancesSmall.Appearance = appearance2;
			buttonTool10.SharedProps.Caption = "&新增";
			buttonTool10.SharedProps.Category = "Standard";
			buttonTool10.SharedProps.CustomizerCaption = "&新增";
			buttonTool10.SharedProps.Visible = false;
			appearance3.Image = 12;
			buttonTool11.SharedProps.AppearancesSmall.Appearance = appearance3;
			buttonTool11.SharedProps.Caption = "删除";
			buttonTool11.SharedProps.Category = "Standard";
			buttonTool11.SharedProps.CustomizerCaption = "&删除";
			buttonTool11.SharedProps.Visible = false;
			appearance4.Image = 29;
			buttonTool12.SharedProps.AppearancesSmall.Appearance = appearance4;
			buttonTool12.SharedProps.Caption = "&保存";
			buttonTool12.SharedProps.Category = "Standard";
			buttonTool12.SharedProps.CustomizerCaption = "&保存";
			buttonTool12.SharedProps.Visible = false;
			appearance5.Image = 27;
			buttonTool13.SharedProps.AppearancesSmall.Appearance = appearance5;
			buttonTool13.SharedProps.Caption = "&取消";
			buttonTool13.SharedProps.Category = "Standard";
			buttonTool13.SharedProps.CustomizerCaption = "&取消";
			buttonTool13.SharedProps.Visible = false;
			appearance6.Image = 42;
			buttonTool14.SharedProps.AppearancesSmall.Appearance = appearance6;
			buttonTool14.SharedProps.Caption = "&刷新";
			buttonTool14.SharedProps.Category = "Standard";
			buttonTool14.SharedProps.CustomizerCaption = "&刷新";
			buttonTool14.SharedProps.Visible = false;
			appearance7.Image = 32;
			buttonTool15.SharedProps.AppearancesSmall.Appearance = appearance7;
			buttonTool15.SharedProps.Caption = "&查询";
			buttonTool15.SharedProps.Category = "Standard";
			buttonTool15.SharedProps.CustomizerCaption = "&查询";
			buttonTool15.SharedProps.Visible = false;
			appearance8.Image = 43;
			buttonTool16.SharedProps.AppearancesSmall.Appearance = appearance8;
			buttonTool16.SharedProps.Caption = "&退出";
			buttonTool16.SharedProps.Category = "Standard";
			buttonTool16.SharedProps.CustomizerCaption = "&退出";
			appearance9.Image = 37;
			buttonTool17.SharedProps.AppearancesSmall.Appearance = appearance9;
			buttonTool17.SharedProps.Caption = "&出库";
			buttonTool17.SharedProps.Category = "Standard";
			buttonTool17.SharedProps.CustomizerCaption = "&出库";
			buttonTool17.SharedProps.Visible = false;
			appearance10.Image = 38;
			buttonTool18.SharedProps.AppearancesSmall.Appearance = appearance10;
			buttonTool18.SharedProps.Caption = "&入库";
			buttonTool18.SharedProps.Category = "Standard";
			buttonTool18.SharedProps.CustomizerCaption = "&入库";
			buttonTool18.SharedProps.Visible = false;
			this.ultraToolbarsManager.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
																										  buttonTool10,
																										  buttonTool11,
																										  buttonTool12,
																										  buttonTool13,
																										  buttonTool14,
																										  buttonTool15,
																										   buttonTool17,
																										  buttonTool18,
																										  buttonTool16
																										 });
			this.ultraToolbarsManager.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.ultraToolbarsManager_ToolClick);
			// 
			// imageList
			// 
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// _UCCoolToolBar_Toolbars_Dock_Area_Left
			// 
			this._UCCoolToolBar_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
			this._UCCoolToolBar_Toolbars_Dock_Area_Left.BackColor = System.Drawing.SystemColors.Control;
			this._UCCoolToolBar_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
			this._UCCoolToolBar_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
			this._UCCoolToolBar_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 22);
			this._UCCoolToolBar_Toolbars_Dock_Area_Left.Name = "_UCCoolToolBar_Toolbars_Dock_Area_Left";
			this._UCCoolToolBar_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 2);
			this._UCCoolToolBar_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager;
			// 
			// _UCCoolToolBar_Toolbars_Dock_Area_Right
			// 
			this._UCCoolToolBar_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
			this._UCCoolToolBar_Toolbars_Dock_Area_Right.BackColor = System.Drawing.SystemColors.Control;
			this._UCCoolToolBar_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
			this._UCCoolToolBar_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
			this._UCCoolToolBar_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(544, 22);
			this._UCCoolToolBar_Toolbars_Dock_Area_Right.Name = "_UCCoolToolBar_Toolbars_Dock_Area_Right";
			this._UCCoolToolBar_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 2);
			this._UCCoolToolBar_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager;
			// 
			// _UCCoolToolBar_Toolbars_Dock_Area_Top
			// 
			this._UCCoolToolBar_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
			this._UCCoolToolBar_Toolbars_Dock_Area_Top.BackColor = System.Drawing.SystemColors.Control;
			this._UCCoolToolBar_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
			this._UCCoolToolBar_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
			this._UCCoolToolBar_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
			this._UCCoolToolBar_Toolbars_Dock_Area_Top.Name = "_UCCoolToolBar_Toolbars_Dock_Area_Top";
			this._UCCoolToolBar_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(544, 22);
			this._UCCoolToolBar_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager;
			// 
			// _UCCoolToolBar_Toolbars_Dock_Area_Bottom
			// 
			this._UCCoolToolBar_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
			this._UCCoolToolBar_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.SystemColors.Control;
			this._UCCoolToolBar_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
			this._UCCoolToolBar_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
			this._UCCoolToolBar_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 24);
			this._UCCoolToolBar_Toolbars_Dock_Area_Bottom.Name = "_UCCoolToolBar_Toolbars_Dock_Area_Bottom";
			this._UCCoolToolBar_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(544, 0);
			this._UCCoolToolBar_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager;
			// 
			// UCCoolToolBar
			// 
			this.Controls.Add(this._UCCoolToolBar_Toolbars_Dock_Area_Left);
			this.Controls.Add(this._UCCoolToolBar_Toolbars_Dock_Area_Right);
			this.Controls.Add(this._UCCoolToolBar_Toolbars_Dock_Area_Top);
			this.Controls.Add(this._UCCoolToolBar_Toolbars_Dock_Area_Bottom);
			this.Name = "UCCoolToolBar";
			this.Size = new System.Drawing.Size(544, 24);
			((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void ultraToolbarsManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
		{
			switch (e.Tool.Key)
			{
				case "Add":    // ButtonTool
					// Place code here
					if (OnToolBarAddClick!=null)
					{
						OnToolBarAddClick(sender, e);
					}
					break;

				case "Delete":    // ButtonTool
					// Place code here
					break;

				case "Save":    // ButtonTool
					// Place code here
					break;

				case "Cancel":    // ButtonTool
					// Place code here
					break;
			}
		}

		public bool IsButtonAddVisible
		{
			get
			{
				return this.ultraToolbarsManager.Tools["Add"].SharedProps.Visible; 
			}
			set
			{
				this.ultraToolbarsManager.Tools["Add"].SharedProps.Visible = value;
			}
		}

		public bool IsButtonDeleteVisible
		{
			get
			{
				return this.ultraToolbarsManager.Tools["Delete"].SharedProps.Visible; 
			}
			set
			{
				this.ultraToolbarsManager.Tools["Delete"].SharedProps.Visible = value;
			}
		}


		public bool IsButtonSaveVisible
		{
			get
			{
				return this.ultraToolbarsManager.Tools["Save"].SharedProps.Visible; 
			}
			set
			{
				this.ultraToolbarsManager.Tools["Save"].SharedProps.Visible = value;
			}
		}

		public bool IsButtonCancelVisible
		{
			get
			{
				return this.ultraToolbarsManager.Tools["Cancel"].SharedProps.Visible; 
			}
			set
			{
				this.ultraToolbarsManager.Tools["Cancel"].SharedProps.Visible = value;
			}
		}

		public bool IsButtonExitVisible
		{
			get
			{
				return this.ultraToolbarsManager.Tools["Exit"].SharedProps.Visible; 
			}
			set
			{
				this.ultraToolbarsManager.Tools["Exit"].SharedProps.Visible = value;
			}
		}

		public bool IsButtonRefreshVisible
		{
			get
			{
				return this.ultraToolbarsManager.Tools["Refresh"].SharedProps.Visible; 
			}
			set
			{
				this.ultraToolbarsManager.Tools["Refresh"].SharedProps.Visible = value;
			}
		}

		public bool IsButtonQueryVisible
		{
			get
			{
				return this.ultraToolbarsManager.Tools["Query"].SharedProps.Visible; 
			}
			set
			{
				this.ultraToolbarsManager.Tools["Query"].SharedProps.Visible = value;
			}
		}

		public bool IsButtonStockInVisible
		{
			get
			{
				return this.ultraToolbarsManager.Tools["StockIn"].SharedProps.Visible; 
			}
			set
			{
				this.ultraToolbarsManager.Tools["StockIn"].SharedProps.Visible = value;
			}
		}

		public bool IsButtonStockOutVisible
		{
			get
			{
				return this.ultraToolbarsManager.Tools["StockOut"].SharedProps.Visible; 
			}
			set
			{
				this.ultraToolbarsManager.Tools["StockOut"].SharedProps.Visible = value;
			}
		}
	}
}
