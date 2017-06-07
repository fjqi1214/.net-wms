using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using BenQGuru.eMES.TS;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.DataCollect;
using UserControl;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using Infragistics.Win.UltraWinGrid;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FBurnOut 的摘要说明。
	/// </summary>
	public class FBurnOut : System.Windows.Forms.Form
	{
		private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid1;
		private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid2;
		private Infragistics.Win.UltraWinDataSource.UltraDataSource ultraDataSource1;
		public Infragistics.Win.UltraWinDataSource.UltraDataSource ultraDataSource2;
		public UserControl.UCLabelEdit uclShelfNO;
		private UserControl.UCButton ucbCancel;
		private UserControl.UCButton ucbBurnOut;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblTotal;
		private System.Windows.Forms.Label lblUsed;
		private System.Windows.Forms.Label lblResidual;
		private ShelfFacade _shelfFacade = null; 

		private IDomainDataProvider _domainDataProvider =ApplicationService.Current().DataProvider;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Splitter splitter2;
		private UserControl.UCLabelEdit txtAutoRefresh;
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;
	
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		public FBurnOut()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			this.ultraGrid1.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White;;
			this.ultraGrid1.DisplayLayout.CaptionAppearance.BackColor =Color.FromName("WhiteSmoke");
			this.ultraGrid1.DisplayLayout.Appearance.BackColor=Color.FromArgb(255, 255, 255);
			this.ultraGrid1.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
			this.ultraGrid1.DisplayLayout.Override.RowAppearance.BackColor =Color.FromArgb(230, 234, 245);
			this.ultraGrid1.DisplayLayout.Override.RowAlternateAppearance.BackColor=Color.FromArgb(255, 255, 255);
			this.ultraGrid1.DisplayLayout.Override.RowSelectors =Infragistics.Win.DefaultableBoolean.False;
			this.ultraGrid1.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
			this.ultraGrid1.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
			this.ultraGrid1.DisplayLayout.ScrollBarLook.Appearance.BackColor =Color.FromName("LightGray");

			this.ultraGrid2.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White;;
			this.ultraGrid2.DisplayLayout.CaptionAppearance.BackColor =Color.FromName("WhiteSmoke");
			this.ultraGrid2.DisplayLayout.Appearance.BackColor=Color.FromArgb(255, 255, 255);
			this.ultraGrid2.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
			this.ultraGrid2.DisplayLayout.Override.RowAppearance.BackColor =Color.FromArgb(230, 234, 245);
			this.ultraGrid2.DisplayLayout.Override.RowAlternateAppearance.BackColor=Color.FromArgb(255, 255, 255);
			this.ultraGrid2.DisplayLayout.Override.RowSelectors =Infragistics.Win.DefaultableBoolean.False;
			this.ultraGrid2.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
			this.ultraGrid2.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
			this.ultraGrid2.DisplayLayout.ScrollBarLook.Appearance.BackColor =Color.FromName("LightGray");
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShelfNO1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ResidualTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShelfPosition");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn1 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("ShelfNO1");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn2 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("ResidualTime");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn3 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("ShelfPosition");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShelfNO2");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShelfPosition2");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn4 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("ShelfNO2");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn5 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("ShelfPosition2");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FBurnOut));
            this.ultraGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
            this.ultraGrid2 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraDataSource2 = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
            this.uclShelfNO = new UserControl.UCLabelEdit();
            this.ucbCancel = new UserControl.UCButton();
            this.ucbBurnOut = new UserControl.UCButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblResidual = new System.Windows.Forms.Label();
            this.lblUsed = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtAutoRefresh = new UserControl.UCLabelEdit();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraGrid1
            // 
            this.ultraGrid1.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGrid1.DataSource = this.ultraDataSource1;
            ultraGridColumn1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.None;
            ultraGridColumn1.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn1.Header.Caption = "车号";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn2.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn2.Header.Caption = "剩余时间";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 120;
            ultraGridColumn3.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn3.Header.Caption = "车位";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3});
            this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGrid1.Location = new System.Drawing.Point(0, 0);
            this.ultraGrid1.Name = "ultraGrid1";
            this.ultraGrid1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ultraGrid1.Size = new System.Drawing.Size(640, 477);
            this.ultraGrid1.TabIndex = 0;
            this.ultraGrid1.Text = "尚未到期";
            // 
            // ultraDataSource1
            // 
            ultraDataColumn1.ReadOnly = Infragistics.Win.DefaultableBoolean.True;
            this.ultraDataSource1.Band.Columns.AddRange(new object[] {
            ultraDataColumn1,
            ultraDataColumn2,
            ultraDataColumn3});
            // 
            // ultraGrid2
            // 
            this.ultraGrid2.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGrid2.DataSource = this.ultraDataSource2;
            ultraGridColumn4.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.None;
            ultraGridColumn4.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn4.Header.Caption = "车号";
            ultraGridColumn4.Header.VisiblePosition = 0;
            ultraGridColumn5.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn5.Header.Caption = "车位";
            ultraGridColumn5.Header.VisiblePosition = 1;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn4,
            ultraGridColumn5});
            this.ultraGrid2.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.ultraGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGrid2.Location = new System.Drawing.Point(0, 0);
            this.ultraGrid2.Name = "ultraGrid2";
            this.ultraGrid2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ultraGrid2.Size = new System.Drawing.Size(220, 403);
            this.ultraGrid2.TabIndex = 1;
            this.ultraGrid2.Text = "已经到期";
            // 
            // ultraDataSource2
            // 
            ultraDataColumn4.ReadOnly = Infragistics.Win.DefaultableBoolean.True;
            this.ultraDataSource2.Band.Columns.AddRange(new object[] {
            ultraDataColumn4,
            ultraDataColumn5});
            // 
            // uclShelfNO
            // 
            this.uclShelfNO.AllowEditOnlyChecked = true;
            this.uclShelfNO.Caption = "车号";
            this.uclShelfNO.Checked = false;
            this.uclShelfNO.EditType = UserControl.EditTypes.String;
            this.uclShelfNO.Location = new System.Drawing.Point(8, 10);
            this.uclShelfNO.MaxLength = 6;
            this.uclShelfNO.Multiline = false;
            this.uclShelfNO.Name = "uclShelfNO";
            this.uclShelfNO.PasswordChar = '\0';
            this.uclShelfNO.ReadOnly = false;
            this.uclShelfNO.ShowCheckBox = false;
            this.uclShelfNO.Size = new System.Drawing.Size(170, 24);
            this.uclShelfNO.TabIndex = 19;
            this.uclShelfNO.TabNext = true;
            this.uclShelfNO.Value = "";
            this.uclShelfNO.WidthType = UserControl.WidthTypes.Normal;
            this.uclShelfNO.XAlign = 45;
            this.uclShelfNO.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.uclShelfNO_TxtboxKeyPress);
            // 
            // ucbCancel
            // 
            this.ucbCancel.BackColor = System.Drawing.SystemColors.Control;
            this.ucbCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucbCancel.BackgroundImage")));
            this.ucbCancel.ButtonType = UserControl.ButtonTypes.Refresh;
            this.ucbCancel.Caption = "刷新";
            this.ucbCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucbCancel.Location = new System.Drawing.Point(309, 10);
            this.ucbCancel.Name = "ucbCancel";
            this.ucbCancel.Size = new System.Drawing.Size(88, 22);
            this.ucbCancel.TabIndex = 21;
            this.ucbCancel.Click += new System.EventHandler(this.ucbCancel_Click);
            // 
            // ucbBurnOut
            // 
            this.ucbBurnOut.BackColor = System.Drawing.SystemColors.Control;
            this.ucbBurnOut.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucbBurnOut.BackgroundImage")));
            this.ucbBurnOut.ButtonType = UserControl.ButtonTypes.None;
            this.ucbBurnOut.Caption = "Burn Out";
            this.ucbBurnOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucbBurnOut.Location = new System.Drawing.Point(202, 10);
            this.ucbBurnOut.Name = "ucbBurnOut";
            this.ucbBurnOut.Size = new System.Drawing.Size(88, 22);
            this.ucbBurnOut.TabIndex = 20;
            this.ucbBurnOut.Click += new System.EventHandler(this.ucbBurnOut_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblResidual);
            this.groupBox1.Controls.Add(this.lblUsed);
            this.groupBox1.Controls.Add(this.lblTotal);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(640, 37);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            // 
            // lblResidual
            // 
            this.lblResidual.Location = new System.Drawing.Point(293, 15);
            this.lblResidual.Name = "lblResidual";
            this.lblResidual.Size = new System.Drawing.Size(47, 15);
            this.lblResidual.TabIndex = 24;
            // 
            // lblUsed
            // 
            this.lblUsed.Location = new System.Drawing.Point(180, 15);
            this.lblUsed.Name = "lblUsed";
            this.lblUsed.Size = new System.Drawing.Size(47, 15);
            this.lblUsed.TabIndex = 23;
            // 
            // lblTotal
            // 
            this.lblTotal.Location = new System.Drawing.Point(53, 15);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(47, 15);
            this.lblTotal.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(240, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 15);
            this.label4.TabIndex = 21;
            this.label4.Text = "空余车位：";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(127, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 20;
            this.label2.Text = "使用车位：";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 15);
            this.label3.TabIndex = 19;
            this.label3.Text = "总车位：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtAutoRefresh);
            this.panel1.Controls.Add(this.splitter2);
            this.panel1.Controls.Add(this.ucbBurnOut);
            this.panel1.Controls.Add(this.uclShelfNO);
            this.panel1.Controls.Add(this.ucbCancel);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 403);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 74);
            this.panel1.TabIndex = 23;
            // 
            // txtAutoRefresh
            // 
            this.txtAutoRefresh.AllowEditOnlyChecked = true;
            this.txtAutoRefresh.Caption = "自动刷新";
            this.txtAutoRefresh.Checked = false;
            this.txtAutoRefresh.EditType = UserControl.EditTypes.Integer;
            this.txtAutoRefresh.Location = new System.Drawing.Point(420, 10);
            this.txtAutoRefresh.MaxLength = 40;
            this.txtAutoRefresh.Multiline = false;
            this.txtAutoRefresh.Name = "txtAutoRefresh";
            this.txtAutoRefresh.PasswordChar = '\0';
            this.txtAutoRefresh.ReadOnly = false;
            this.txtAutoRefresh.ShowCheckBox = true;
            this.txtAutoRefresh.Size = new System.Drawing.Size(127, 24);
            this.txtAutoRefresh.TabIndex = 28;
            this.txtAutoRefresh.TabNext = false;
            this.txtAutoRefresh.Value = "";
            this.txtAutoRefresh.WidthType = UserControl.WidthTypes.Tiny;
            this.txtAutoRefresh.XAlign = 497;
            this.txtAutoRefresh.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAutoRefresh_TxtboxKeyPress);
            this.txtAutoRefresh.CheckBoxCheckedChanged += new System.EventHandler(this.txtAutoRefresh_CheckBoxCheckedChanged);
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(0, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(2, 37);
            this.splitter2.TabIndex = 23;
            this.splitter2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ultraGrid2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(420, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(220, 403);
            this.panel2.TabIndex = 24;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.splitter1);
            this.panel3.Controls.Add(this.ultraGrid1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(640, 477);
            this.panel3.TabIndex = 19;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(2, 477);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FBurnOut
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(640, 477);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Name = "FBurnOut";
            this.Text = "FBurnOut";
            this.Load += new System.EventHandler(this.FBurnOut_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public void RefreshPanel()
		{
			//Laws Lu,2006/12/27 减少Open/Close次数
			((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
			((SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			if( _shelfFacade==null )
			{
				_shelfFacade = new ShelfFacade(this.DataProvider);
			}
			/* 尚未到期的 */
			this.ultraDataSource1.Rows.Clear();
			Hashtable expiredHt = _shelfFacade.GetNotExpiredShelf();

			if( expiredHt!=null )
			{

				System.Data.DataTable dtExpire = new System.Data.DataTable();
			
				dtExpire.Columns.Add("ShelfNO1",typeof(string));
				dtExpire.Columns.Add("ResidualTime",typeof(int));
				dtExpire.Columns.Add("ShelfPosition",typeof(string));
				foreach(DictionaryEntry dic in expiredHt)
				{
					BurnOutShelf sf = (BurnOutShelf)dic.Value;
					TimeSpan span = sf.TimeSpan;
					int min = span.Days*24*60 + span.Hours*60 + span.Minutes;

					//she
					dtExpire.Rows.Add(new object[]{sf.ShelfNO,min,sf.memo});
					dtExpire.AcceptChanges();
				}
				
				System.Data.DataView dv = new System.Data.DataView(dtExpire);

				dv.Sort = "ResidualTime DESC";

				for(int i = 0;i<dv.Count;i++ )
				{
//					try
//					{
						Infragistics.Win.UltraWinDataSource.UltraDataRow row = ultraDataSource1.Rows.Add();		
						
						row["ShelfNO1"] = dv[i]["ShelfNO1"].ToString();
						
					row["ResidualTime"] = dv[i]["ResidualTime"].ToString() +"min";

					row["ShelfPosition"] = dv[i]["ShelfPosition"].ToString();
						
//					}
//					catch{}
				}
				//ultraGrid1.DisplayLayout.Override. = Infragistics.Win.UltraWinGrid.RowFilterMode.
				//ultraDataSource1.Band.Columns.
			}			

			/* 已经到期的 */
			this.ultraDataSource2.Rows.Clear();
			Hashtable notExpiredHt = _shelfFacade.GetExpiredShelf();
			if( notExpiredHt!=null )
			{
				ArrayList alList = new ArrayList();
				alList.AddRange(notExpiredHt.Keys);
				alList.Sort();
			foreach( object key in alList )
				{
//					try
//					{
						
						Infragistics.Win.UltraWinDataSource.UltraDataRow row = ultraDataSource2.Rows.Add();	
						
					if(notExpiredHt.ContainsKey(key))
					{
						BurnOutShelf bs = (BurnOutShelf)notExpiredHt[key];
						row["ShelfNO2"] = bs.ShelfNO;
						row["ShelfPosition2"] = bs.memo;
					}
						
//					}
//					catch{}
				}
			}			


			/* 车位数量的 */
			object volumn = _shelfFacade.GetBurnInOutVolumn( Guid.Empty.ToString() );
			if( volumn !=null )
			{
				this.lblTotal.Text = (volumn as BurnInOutVolumn).Total.ToString("##.##");
				this.lblUsed.Text = (volumn as BurnInOutVolumn).Used.ToString("##.##");
				decimal residual = (volumn as BurnInOutVolumn).Total - (volumn as BurnInOutVolumn).Used;
				this.lblResidual.Text = residual.ToString("##.##");
			}
			
			//Laws Lu,2006/12/27 减少Open/Close次数
			((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
			((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;

			Application.DoEvents();
			this.uclShelfNO.TextFocus(true, true) ;
//			System.Windows.Forms.SendKeys.Send("+{TAB}");

		}

		private void ucbCancel_Click(object sender, EventArgs e)
		{
			RefreshPanel() ;	
		}

		public void ucbBurnOut_Click(object sender, EventArgs e)
		{
			/* 检查车号 */
			string shelfno = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.uclShelfNO.Value ) );

			if(shelfno.Length==0)
			{
				ApplicationRun.GetInfoForm().Add( new UserControl.Message( MessageType.Error, "$CS_Please_Input_ShelfNO" ));

				Application.DoEvents();
				uclShelfNO.TextFocus(true, true);
				//System.Windows.Forms.SendKeys.Send("+{TAB}");
				return;
			}

			bool expired = false;
			for(int i=0; i<this.ultraDataSource2.Rows.Count; i++)
			{
				if( string.Compare( shelfno, this.ultraDataSource2.Rows[i]["ShelfNO2"].ToString() )==0 )
				{
					expired=true;
					break;
				}
			}

			if(!expired)
			{
				ApplicationRun.GetInfoForm().Add( new UserControl.Message( MessageType.Error, "$CS_Shelf_Not_Expired" ));

				Application.DoEvents();
				uclShelfNO.TextFocus(true, true);
				//System.Windows.Forms.SendKeys.Send("+{TAB}");
				return;
			}

			Messages messages = new Messages();
			if( _shelfFacade==null )
			{
				_shelfFacade = new ShelfFacade( this.DataProvider );
			}

			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			DataProvider.BeginTransaction();
			try
			{
				/* step1: 改变shelf的状态 */
				object shelf = _shelfFacade.GetShelfAndLockit ( shelfno );

				if( shelf==null )
				{
					messages.Add(new UserControl.Message( MessageType.Error, "$CS_Shelf_Not_Exist"));
					ApplicationRun.GetInfoForm().Add(messages);
					//RefreshPanel() ;	
//					this.DataProvider.RollbackTransaction();	// Added by Icyer 2006/06/13 @Darfon
				}

				if(messages.IsSuccess())
				{
				if( (shelf as Shelf).Status == ShelfStatus.BurnOut )
				{
					messages.Add(new UserControl.Message( MessageType.Error, "$CS_Shelf_Has_Burn_Out"));
					ApplicationRun.GetInfoForm().Add(messages);
						//RefreshPanel() ;	
						//					this.DataProvider.RollbackTransaction();	// Added by Icyer 2006/06/13 @Darfon
					}
				}
				
				object shelfActionList	 = null;
				if(messages.IsSuccess())
				{
				(shelf as Shelf).Status = ShelfStatus.BurnOut;
				(shelf as Shelf).MaintainUser = ApplicationService.Current().UserCode;
				

				/* step2: 记录shelf */
				DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

					shelfActionList = _shelfFacade.GetShelfActionList(shelfno, ShelfStatus.BurnIn) ;
				(shelfActionList as ShelfActionList).Status = ShelfStatus.BurnOut;
//				(shelfActionList as ShelfActionList).BurnOutDate = FormatHelper.TODateInt( DateTime.Now );
//				(shelfActionList as ShelfActionList).BurnOutTIme = FormatHelper.TOTimeInt( DateTime.Now );

				(shelfActionList as ShelfActionList).BurnOutDate = dbDateTime.DBDate;
				(shelfActionList as ShelfActionList).BurnOutTIme = dbDateTime.DBTime;;

				(shelfActionList as ShelfActionList).BurnOutUser = ApplicationService.Current().UserCode;
				(shelfActionList as ShelfActionList).MaintainUser = ApplicationService.Current().UserCode;
				
			
				/* step3: 车位计数，减1 */
				

				/* step4: rcard burn Out */
				string[] rcards = _shelfFacade.GetShelf2RCard( (shelfActionList as ShelfActionList).PKID );
				if(rcards !=null && rcards.Length>0 )
				{
					for(int i=0; i<rcards.Length; i++)
					{
						messages.AddMessages( RunBurnOut(rcards[i], (shelfActionList as ShelfActionList).PKID) );
						if( !messages.IsSuccess() )
						{
							break;
						}
					}
				}
				}

				if (messages.IsSuccess())
				{
					//Laws Lu,2006/11/31 change update database sequence 
					_shelfFacade.UpdateShelf(shelf as Shelf);
					//Laws Lu,2006/11/31 update detail data 
					_shelfFacade.UpdateShelfActionList(shelfActionList as ShelfActionList);	
					//Laws Lu,2006/11/31 车位计数
					_shelfFacade.UpdateBurnInOutVolumnBySqlMinus(ApplicationService.Current().UserCode);

					
					ApplicationRun.GetInfoForm().Add(messages);

					DataProvider.CommitTransaction();
					//RefreshPanel() ;	
				}
				else
				{
					DataProvider.RollbackTransaction();
					ApplicationRun.GetInfoForm().Add(messages);
					//RefreshPanel() ;	
				}
			}
			catch (Exception ex)
			{
				DataProvider.RollbackTransaction();
				if(ex.Message.IndexOf("ORA-00054") >= 0)
				{
					messages.Add(new UserControl.Message(MessageType.Error,"$ERR_CURRENT_SHELF_BE_LOCKED"));
				}
				else
				{
				messages.Add(new UserControl.Message(ex));
				}
				ApplicationRun.GetInfoForm().Add(messages);
				//RefreshPanel() ;	
			}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
				RefreshPanel() ;
			}
		}

		private void uclShelfNO_TxtboxKeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar=='\r')
			{
				this.ucbBurnOut_Click( null, null );
			}
		}

		private void FBurnOut_Load(object sender, System.EventArgs e)
		{
			RefreshPanel() ;
		}

		private Messages GetProduct(string rcard)
		{
			Messages productmessages=new Messages ();
			ActionOnLineHelper dataCollect = new ActionOnLineHelper(DataProvider);		
			try
			{
				productmessages.AddMessages( dataCollect.GetIDInfo(rcard));
			}
			catch (Exception e)
			{
				productmessages.Add(new UserControl.Message(e));

			}
			return productmessages;
		}

		
		private Domain.BaseSetting.Resource Resource = null ;
		private ActionCheckStatus actionCheckStatus = null ;
		private ProductInfo product = null;
		/// <summary>
		/// RCard Burn Out
		/// </summary>
		/// <returns></returns>
		private Messages RunBurnOut(string rcard, string shelfpk)
		{
			if (Resource == null)
			{
				BenQGuru.eMES.BaseSetting.BaseModelFacade dataModel = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
				Resource = (Domain.BaseSetting.Resource)dataModel.GetResource(ApplicationService.Current().ResourceCode);
			}

			Messages messages=new Messages ();
			try
			{
				messages = GetProduct( rcard );
				if (messages.IsSuccess())
				{
					product = (ProductInfo)messages.GetData().Values[0];
					actionCheckStatus = new ActionCheckStatus();
					actionCheckStatus.ProductInfo = product;
					if(actionCheckStatus.ProductInfo != null)
					{
						actionCheckStatus.ProductInfo.Resource = Resource;
					}

					IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_BurnOutGood);
					ActionEventArgs actionEventArgs = new ActionEventArgs(
                        ActionType.DataCollectAction_BurnOutGood, 
						rcard,
						ApplicationService.Current().UserCode,
						ApplicationService.Current().ResourceCode,
						shelfpk,
						product );
					messages.AddMessages( ((IActionWithStatus)dataCollectModule).Execute(
						actionEventArgs,
						actionCheckStatus));
				}

				if (messages.IsSuccess())
				{
					messages.Add(new UserControl.Message(MessageType.Success ,string.Format("$CS_BurnOutSUCCESS,$CS_Param_ID:{0}",rcard)));
				}
				return messages;
			}
			catch (Exception e)
			{
				messages.Add(new UserControl.Message(e));
				return messages;
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			//MessageBox.Show("OK"); //测试使用

			this.RefreshPanel();

			this.timer1.Start();
		}

		private void txtAutoRefresh_TxtboxKeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar == '\r')
			{
				if(txtAutoRefresh.Checked && txtAutoRefresh.Value.Trim().Length!=0)
				{
					this.timer1.Interval = int.Parse(txtAutoRefresh.Value) * 60 * 1000;
					this.timer1.Start();
				}

				this.uclShelfNO.TextFocus(true, true) ;
				//System.Windows.Forms.SendKeys.Send("+{TAB}");
                //Remove UCLabel.SelectAll;
			}
		}

		private void txtAutoRefresh_CheckBoxCheckedChanged(object sender, EventArgs e)
		{
			if( ! txtAutoRefresh.Checked)
			{
				this.timer1.Stop();
			}
		}
		
	}
}
