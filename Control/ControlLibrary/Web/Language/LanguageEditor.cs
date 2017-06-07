using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ControlLibrary.Web.Language
{
	/// <summary>
	/// LanguageEditor 的摘要说明。
	/// </summary>
	public class LanguageEditor : System.Windows.Forms.Form  
	{
		private System.Windows.Forms.TabControl tabControlLanguage;
		private System.Windows.Forms.TabPage tabPageENU;
		private System.Windows.Forms.TabPage tabPageCHS;
		private System.Windows.Forms.TabPage tabPageCHT;
		private System.Windows.Forms.PropertyGrid propertyGridENU;
		private System.Windows.Forms.PropertyGrid propertyGridCHS;
		private System.Windows.Forms.PropertyGrid propertyGridCHT;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuItemFile;
		private System.Windows.Forms.MenuItem menuItemSave;
		private System.Windows.Forms.MenuItem menuItemExit;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public LanguageEditor()
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
			this.tabControlLanguage = new System.Windows.Forms.TabControl();
			this.tabPageENU = new System.Windows.Forms.TabPage();
			this.tabPageCHS = new System.Windows.Forms.TabPage();
			this.tabPageCHT = new System.Windows.Forms.TabPage();
			this.propertyGridENU = new System.Windows.Forms.PropertyGrid();
			this.propertyGridCHS = new System.Windows.Forms.PropertyGrid();
			this.propertyGridCHT = new System.Windows.Forms.PropertyGrid();
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.menuItemFile = new System.Windows.Forms.MenuItem();
			this.menuItemSave = new System.Windows.Forms.MenuItem();
			this.menuItemExit = new System.Windows.Forms.MenuItem();
			this.tabControlLanguage.SuspendLayout();
			this.tabPageENU.SuspendLayout();
			this.tabPageCHS.SuspendLayout();
			this.tabPageCHT.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControlLanguage
			// 
			this.tabControlLanguage.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabControlLanguage.Controls.Add(this.tabPageENU);
			this.tabControlLanguage.Controls.Add(this.tabPageCHS);
			this.tabControlLanguage.Controls.Add(this.tabPageCHT);
			this.tabControlLanguage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlLanguage.Location = new System.Drawing.Point(0, 0);
			this.tabControlLanguage.Name = "tabControlLanguage";
			this.tabControlLanguage.SelectedIndex = 0;
			this.tabControlLanguage.Size = new System.Drawing.Size(482, 408);
			this.tabControlLanguage.TabIndex = 0;
			// 
			// tabPageENU
			// 
			this.tabPageENU.Controls.Add(this.propertyGridENU);
			this.tabPageENU.Location = new System.Drawing.Point(4, 24);
			this.tabPageENU.Name = "tabPageENU";
			this.tabPageENU.Size = new System.Drawing.Size(474, 300);
			this.tabPageENU.TabIndex = 0;
			this.tabPageENU.Text = "ENU";
			// 
			// tabPageCHS
			// 
			this.tabPageCHS.Controls.Add(this.propertyGridCHS);
			this.tabPageCHS.Location = new System.Drawing.Point(4, 24);
			this.tabPageCHS.Name = "tabPageCHS";
			this.tabPageCHS.Size = new System.Drawing.Size(474, 300);
			this.tabPageCHS.TabIndex = 1;
			this.tabPageCHS.Text = "CHS";
			// 
			// tabPageCHT
			// 
			this.tabPageCHT.Controls.Add(this.propertyGridCHT);
			this.tabPageCHT.Location = new System.Drawing.Point(4, 24);
			this.tabPageCHT.Name = "tabPageCHT";
			this.tabPageCHT.Size = new System.Drawing.Size(474, 380);
			this.tabPageCHT.TabIndex = 2;
			this.tabPageCHT.Text = "CHT";
			// 
			// propertyGridENU
			// 
			this.propertyGridENU.CommandsVisibleIfAvailable = true;
			this.propertyGridENU.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGridENU.HelpVisible = false;
			this.propertyGridENU.LargeButtons = false;
			this.propertyGridENU.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGridENU.Location = new System.Drawing.Point(0, 0);
			this.propertyGridENU.Name = "propertyGridENU";
			this.propertyGridENU.Size = new System.Drawing.Size(474, 300);
			this.propertyGridENU.TabIndex = 0;
			this.propertyGridENU.Text = "ENU";
			this.propertyGridENU.ToolbarVisible = false;
			this.propertyGridENU.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGridENU.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// propertyGridCHS
			// 
			this.propertyGridCHS.CommandsVisibleIfAvailable = true;
			this.propertyGridCHS.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGridCHS.HelpVisible = false;
			this.propertyGridCHS.LargeButtons = false;
			this.propertyGridCHS.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGridCHS.Location = new System.Drawing.Point(0, 0);
			this.propertyGridCHS.Name = "propertyGridCHS";
			this.propertyGridCHS.Size = new System.Drawing.Size(474, 300);
			this.propertyGridCHS.TabIndex = 0;
			this.propertyGridCHS.Text = "CHS";
			this.propertyGridCHS.ToolbarVisible = false;
			this.propertyGridCHS.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGridCHS.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// propertyGridCHT
			// 
			this.propertyGridCHT.CommandsVisibleIfAvailable = true;
			this.propertyGridCHT.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGridCHT.LargeButtons = false;
			this.propertyGridCHT.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGridCHT.Location = new System.Drawing.Point(0, 0);
			this.propertyGridCHT.Name = "propertyGridCHT";
			this.propertyGridCHT.Size = new System.Drawing.Size(474, 380);
			this.propertyGridCHT.TabIndex = 0;
			this.propertyGridCHT.Text = "propertyGridCHT";
			this.propertyGridCHT.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGridCHT.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuItemFile});
			// 
			// menuItemFile
			// 
			this.menuItemFile.Index = 0;
			this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItemSave,
																						 this.menuItemExit});
			this.menuItemFile.Text = "File";
			// 
			// menuItemSave
			// 
			this.menuItemSave.Index = 0;
			this.menuItemSave.Text = "Save";
			// 
			// menuItemExit
			// 
			this.menuItemExit.Index = 1;
			this.menuItemExit.Text = "Exit";
			// 
			// LanguageEditor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(482, 408);
			this.Controls.Add(this.tabControlLanguage);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Menu = this.mainMenu;
			this.Name = "LanguageEditor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.tabControlLanguage.ResumeLayout(false);
			this.tabPageENU.ResumeLayout(false);
			this.tabPageCHS.ResumeLayout(false);
			this.tabPageCHT.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
