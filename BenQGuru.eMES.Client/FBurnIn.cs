using System;
using System.Data;
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
	/// FBurnIn 的摘要说明。
	/// </summary>
	public class FBurnIn : Form
	{
		[System.Runtime.InteropServices.DllImport("winmm.DLL", EntryPoint = "PlaySound", SetLastError = true)]
		public static extern bool PlaySound(string szSound, int hMod, int flags);
		private int SND_FILENAME = 0x00020000;

		public UserControl.UCLabelEdit uclShelfNO;
		public UserControl.UCLabelEdit uclVolumn;
		public UserControl.UCLabelEdit txtRunningCard;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private UserControl.UCButton ucbBurnIn;
		private UserControl.UCButton ucbCancel;

		private ShelfFacade _shelfFacade = null; 
		private System.Windows.Forms.Label lblTimePeriod;
		public System.Windows.Forms.Label lblRecentQTY;

		//private DataTable dtIDList = null;
		private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid1;
		private Infragistics.Win.UltraWinDataSource.UltraDataSource ultraDataSource1;

		private string itemcode = string.Empty;
		public string ItemCode
		{
			get
			{
				return itemcode;
			}
			set
			{
				itemcode = value;
			}
		}

		private string firstletter = string.Empty;
		public string FirstLetter
		{
			get
			{
				return firstletter;
			}
			set
			{
				firstletter = value;
			}
		}
	
		private IDomainDataProvider _domainDataProvider =ApplicationService.Current().DataProvider;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private UserControl.UCButton ucbRemove;
		private UserControl.UCLabelEdit uclShelfStation;
		public System.Windows.Forms.CheckBox txtGOMO;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
        private IContainer components;
	
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
        }

		public FBurnIn()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			UserControl.UIStyleBuilder.FormUI(this);	

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FBurnIn));
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("runningcard");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("itemcode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("mocode");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn1 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("runningcard");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn2 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("itemcode");
            Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn3 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("mocode");
            this.uclShelfNO = new UserControl.UCLabelEdit();
            this.uclVolumn = new UserControl.UCLabelEdit();
            this.txtRunningCard = new UserControl.UCLabelEdit();
            this.ucbCancel = new UserControl.UCButton();
            this.ucbBurnIn = new UserControl.UCButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTimePeriod = new System.Windows.Forms.Label();
            this.lblRecentQTY = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ultraGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.uclShelfStation = new UserControl.UCLabelEdit();
            this.panel2 = new System.Windows.Forms.Panel();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.txtGOMO = new System.Windows.Forms.CheckBox();
            this.ucbRemove = new UserControl.UCButton();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // uclShelfNO
            // 
            this.uclShelfNO.AllowEditOnlyChecked = true;
            this.uclShelfNO.Caption = "车号";
            this.uclShelfNO.Checked = false;
            this.uclShelfNO.EditType = UserControl.EditTypes.String;
            this.uclShelfNO.Location = new System.Drawing.Point(31, 7);
            this.uclShelfNO.MaxLength = 6;
            this.uclShelfNO.Multiline = false;
            this.uclShelfNO.Name = "uclShelfNO";
            this.uclShelfNO.PasswordChar = '\0';
            this.uclShelfNO.ReadOnly = false;
            this.uclShelfNO.ShowCheckBox = false;
            this.uclShelfNO.Size = new System.Drawing.Size(170, 24);
            this.uclShelfNO.TabIndex = 0;
            this.uclShelfNO.TabNext = true;
            this.uclShelfNO.Value = "";
            this.uclShelfNO.WidthType = UserControl.WidthTypes.Normal;
            this.uclShelfNO.XAlign = 68;
            this.uclShelfNO.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.uclShelfNO_TxtboxKeyPress);
            // 
            // uclVolumn
            // 
            this.uclVolumn.AllowEditOnlyChecked = true;
            this.uclVolumn.Caption = "本车容量";
            this.uclVolumn.Checked = false;
            this.uclVolumn.EditType = UserControl.EditTypes.String;
            this.uclVolumn.Location = new System.Drawing.Point(234, 7);
            this.uclVolumn.MaxLength = 40;
            this.uclVolumn.Multiline = false;
            this.uclVolumn.Name = "uclVolumn";
            this.uclVolumn.PasswordChar = '\0';
            this.uclVolumn.ReadOnly = false;
            this.uclVolumn.ShowCheckBox = false;
            this.uclVolumn.Size = new System.Drawing.Size(194, 24);
            this.uclVolumn.TabIndex = 1;
            this.uclVolumn.TabNext = true;
            this.uclVolumn.Value = "";
            this.uclVolumn.WidthType = UserControl.WidthTypes.Normal;
            this.uclVolumn.XAlign = 295;
            // 
            // txtRunningCard
            // 
            this.txtRunningCard.AllowEditOnlyChecked = true;
            this.txtRunningCard.Caption = "产品序列号";
            this.txtRunningCard.Checked = false;
            this.txtRunningCard.EditType = UserControl.EditTypes.String;
            this.txtRunningCard.Location = new System.Drawing.Point(31, 29);
            this.txtRunningCard.MaxLength = 40;
            this.txtRunningCard.Multiline = false;
            this.txtRunningCard.Name = "txtRunningCard";
            this.txtRunningCard.PasswordChar = '\0';
            this.txtRunningCard.ReadOnly = false;
            this.txtRunningCard.ShowCheckBox = false;
            this.txtRunningCard.Size = new System.Drawing.Size(206, 24);
            this.txtRunningCard.TabIndex = 3;
            this.txtRunningCard.TabNext = true;
            this.txtRunningCard.Value = "";
            this.txtRunningCard.WidthType = UserControl.WidthTypes.Normal;
            this.txtRunningCard.XAlign = 104;
            this.txtRunningCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRunningCard_TxtboxKeyPress);
            // 
            // ucbCancel
            // 
            this.ucbCancel.BackColor = System.Drawing.SystemColors.Control;
            this.ucbCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucbCancel.BackgroundImage")));
            this.ucbCancel.ButtonType = UserControl.ButtonTypes.Cancle;
            this.ucbCancel.Caption = "取消";
            this.ucbCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucbCancel.Location = new System.Drawing.Point(307, 59);
            this.ucbCancel.Name = "ucbCancel";
            this.ucbCancel.Size = new System.Drawing.Size(88, 22);
            this.ucbCancel.TabIndex = 13;
            this.ucbCancel.Click += new System.EventHandler(this.ucbCancel_Click);
            // 
            // ucbBurnIn
            // 
            this.ucbBurnIn.BackColor = System.Drawing.SystemColors.Control;
            this.ucbBurnIn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucbBurnIn.BackgroundImage")));
            this.ucbBurnIn.ButtonType = UserControl.ButtonTypes.None;
            this.ucbBurnIn.Caption = "Burn In";
            this.ucbBurnIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucbBurnIn.Location = new System.Drawing.Point(100, 59);
            this.ucbBurnIn.Name = "ucbBurnIn";
            this.ucbBurnIn.Size = new System.Drawing.Size(88, 22);
            this.ucbBurnIn.TabIndex = 12;
            this.ucbBurnIn.Click += new System.EventHandler(this.ucbBurnIn_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(28, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 15);
            this.label1.TabIndex = 15;
            this.label1.Text = "时长：";
            // 
            // lblTimePeriod
            // 
            this.lblTimePeriod.Location = new System.Drawing.Point(141, 7);
            this.lblTimePeriod.Name = "lblTimePeriod";
            this.lblTimePeriod.Size = new System.Drawing.Size(59, 15);
            this.lblTimePeriod.TabIndex = 16;
            this.lblTimePeriod.Text = "hours";
            // 
            // lblRecentQTY
            // 
            this.lblRecentQTY.Location = new System.Drawing.Point(345, 7);
            this.lblRecentQTY.Name = "lblRecentQTY";
            this.lblRecentQTY.Size = new System.Drawing.Size(83, 15);
            this.lblRecentQTY.TabIndex = 18;
            this.lblRecentQTY.Text = "0";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(272, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 17;
            this.label4.Text = "当前数量：";
            // 
            // ultraGrid1
            // 
            this.ultraGrid1.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGrid1.DataSource = this.ultraDataSource1;
            ultraGridColumn1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn1.Header.Caption = "产品序列号";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn2.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn2.Header.Caption = "产品";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn3.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            ultraGridColumn3.Header.Caption = "工单";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3});
            this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid1.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.AutoFixed;
            this.ultraGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGrid1.Location = new System.Drawing.Point(0, 0);
            this.ultraGrid1.Name = "ultraGrid1";
            this.ultraGrid1.Size = new System.Drawing.Size(819, 374);
            this.ultraGrid1.TabIndex = 19;
            // 
            // ultraDataSource1
            // 
            this.ultraDataSource1.Band.Columns.AddRange(new object[] {
            ultraDataColumn1,
            ultraDataColumn2,
            ultraDataColumn3});
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uclShelfStation);
            this.panel1.Controls.Add(this.uclShelfNO);
            this.panel1.Controls.Add(this.uclVolumn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(819, 37);
            this.panel1.TabIndex = 20;
            // 
            // uclShelfStation
            // 
            this.uclShelfStation.AllowEditOnlyChecked = true;
            this.uclShelfStation.Caption = "车位";
            this.uclShelfStation.Checked = false;
            this.uclShelfStation.EditType = UserControl.EditTypes.String;
            this.uclShelfStation.Location = new System.Drawing.Point(462, 7);
            this.uclShelfStation.MaxLength = 6;
            this.uclShelfStation.Multiline = false;
            this.uclShelfStation.Name = "uclShelfStation";
            this.uclShelfStation.PasswordChar = '\0';
            this.uclShelfStation.ReadOnly = false;
            this.uclShelfStation.ShowCheckBox = false;
            this.uclShelfStation.Size = new System.Drawing.Size(170, 24);
            this.uclShelfStation.TabIndex = 2;
            this.uclShelfStation.TabNext = true;
            this.uclShelfStation.Value = "";
            this.uclShelfStation.WidthType = UserControl.WidthTypes.Normal;
            this.uclShelfStation.XAlign = 499;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.numericUpDown1);
            this.panel2.Controls.Add(this.txtGOMO);
            this.panel2.Controls.Add(this.ucbRemove);
            this.panel2.Controls.Add(this.ucbCancel);
            this.panel2.Controls.Add(this.txtRunningCard);
            this.panel2.Controls.Add(this.ucbBurnIn);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblTimePeriod);
            this.panel2.Controls.Add(this.lblRecentQTY);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 411);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(819, 89);
            this.panel2.TabIndex = 21;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.AllowDrop = true;
            this.numericUpDown1.DecimalPlaces = 1;
            this.numericUpDown1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown1.InterceptArrowKeys = false;
            this.numericUpDown1.Location = new System.Drawing.Point(75, 5);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.ReadOnly = true;
            this.numericUpDown1.Size = new System.Drawing.Size(60, 20);
            this.numericUpDown1.TabIndex = 21;
            this.numericUpDown1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // txtGOMO
            // 
            this.txtGOMO.Checked = true;
            this.txtGOMO.CheckState = System.Windows.Forms.CheckState.Checked;
            this.txtGOMO.Location = new System.Drawing.Point(275, 29);
            this.txtGOMO.Name = "txtGOMO";
            this.txtGOMO.Size = new System.Drawing.Size(101, 22);
            this.txtGOMO.TabIndex = 20;
            this.txtGOMO.Text = "设定归属工单";
            // 
            // ucbRemove
            // 
            this.ucbRemove.BackColor = System.Drawing.SystemColors.Control;
            this.ucbRemove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucbRemove.BackgroundImage")));
            this.ucbRemove.ButtonType = UserControl.ButtonTypes.None;
            this.ucbRemove.Caption = "删除";
            this.ucbRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucbRemove.Location = new System.Drawing.Point(200, 59);
            this.ucbRemove.Name = "ucbRemove";
            this.ucbRemove.Size = new System.Drawing.Size(88, 22);
            this.ucbRemove.TabIndex = 19;
            this.ucbRemove.Click += new System.EventHandler(this.ucbRemove_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ultraGrid1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 37);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(819, 374);
            this.panel3.TabIndex = 22;
            // 
            // FBurnIn
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(819, 500);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FBurnIn";
            this.Text = "FBurnIn";
            this.Load += new System.EventHandler(this.FBurnIn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void ClearPanel()
		{
			this.uclShelfNO.Value = string.Empty;
			this.uclVolumn.Value = string.Empty;
			this.uclShelfStation.Value = string.Empty;

			//this.lblTimePeriod.Text = string.Empty;
			this.lblRecentQTY.Text = "0";

			this.txtRunningCard.Value = string.Empty;
			//this.txtGOMO.Checked = false;

			this.ultraDataSource1.Rows.Clear();

			this.ItemCode = string.Empty;
			this.FirstLetter = string.Empty;
			this.rcardCollect = null;

			this.uclShelfNO.TextFocus(true, true) ;
			//System.Windows.Forms.SendKeys.Send("+{TAB}");
		}

		private void ucbCancel_Click(object sender, EventArgs e)
		{
			ClearPanel();
		}

		public void uclShelfNO_TxtboxKeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar =='\r')
			{
				string temp = FormatHelper.CleanString(this.uclShelfNO.Value);
				this.ClearPanel();
				this.uclShelfNO.Value = temp;

				if( FormatHelper.CleanString(this.uclShelfNO.Value) == string.Empty)
				{
					ApplicationRun.GetInfoForm().Add("$CS_Please_Input_ShelfNO");

					uclShelfNO.TextFocus(true, true);
					//System.Windows.Forms.SendKeys.Send("+{TAB}");
                    //Remove UCLabel.SelectAll;
					return;
				}
				
				Messages messages = new Messages();
				if( _shelfFacade==null )
				{
					_shelfFacade = new ShelfFacade( this.DataProvider );
				}
				object shelf = _shelfFacade.GetShelf (  FormatHelper.CleanString(this.uclShelfNO.Value) );
				if( shelf==null )
				{
					messages.Add(new UserControl.Message( MessageType.Error, "$CS_Shelf_Not_Exist"));
					ApplicationRun.GetInfoForm().Add(messages);
					uclShelfNO.TextFocus(true, true);
					//System.Windows.Forms.SendKeys.Send("+{TAB}");
                    //Remove UCLabel.SelectAll;
					return ;
				}

				if( (shelf as Shelf).Status == ShelfStatus.BurnIn )
				{
					messages.Add(new UserControl.Message( MessageType.Error, "$CS_Shelf_Status_Error"));
					ApplicationRun.GetInfoForm().Add(messages);
					uclShelfNO.TextFocus(true, true);
					//System.Windows.Forms.SendKeys.Send("+{TAB}");
                    //Remove UCLabel.SelectAll;
					return ;
				}
				// Added by Icyer 2006/06/13
				this.uclShelfStation.Value = (shelf as Shelf).Memo;
				// Added end;

				//通过车号检查，将焦点定位在产品序列号输入框
				Application.DoEvents();
				this.txtRunningCard.TextFocus(true, true);
				//System.Windows.Forms.SendKeys.Send("+{TAB}");
			}
		}


		private Hashtable rcardCollect = null;
		private Domain.BaseSetting.Resource Resource = null ;
		private ActionCheckStatus actionCheckStatus = null ;
		private ProductInfo product = null;
		public void txtRunningCard_TxtboxKeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{	
				//PlayAlertMusic(SoundType.Error);
				UserControl.Messages msg = new UserControl.Messages();

				if( FormatHelper.CleanString(this.uclShelfNO.Value) == string.Empty)
				{
					ApplicationRun.GetInfoForm().Add("$CS_Please_Input_ShelfNO");

					txtRunningCard.Value = string.Empty;
					uclShelfNO.TextFocus(true, true);
					//System.Windows.Forms.SendKeys.Send("+{TAB}");
                    //Remove UCLabel.SelectAll;

					PlayAlertMusic(SoundType.Error);
					return;
				}
				else if ( FormatHelper.CleanString( txtRunningCard.Value ) == string.Empty)
				{
					ApplicationRun.GetInfoForm().Add("$CS_Please_Input_RunningCard");
					
					txtRunningCard.TextFocus(true, true);
					//System.Windows.Forms.SendKeys.Send("+{TAB}");
                    //Remove UCLabel.SelectAll;
					PlayAlertMusic(SoundType.Error);
					return;
				}
				else
				{
					if( rcardCollect == null )
					{
						rcardCollect = new Hashtable();
					}
					//Laws Lu,2006/12/27 close connection manually
					((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
					((SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
					Messages msgs = new Messages();
					msgs = GetProduct();

					if( msgs.IsSuccess() )
					{
						if (Resource == null)
						{
							BenQGuru.eMES.BaseSetting.BaseModelFacade dataModel = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
							Resource = (Domain.BaseSetting.Resource)dataModel.GetResource(ApplicationService.Current().ResourceCode);
						}

						actionCheckStatus = new ActionCheckStatus();
						actionCheckStatus.ProductInfo = product;
						if(actionCheckStatus.ProductInfo != null)
						{
							actionCheckStatus.ProductInfo.Resource = Resource;
						}

						//如果勾选了归属工单，先归属工单。
						if( txtGOMO.Checked == true )
						{
							msgs = RunGOMO( actionCheckStatus );
							ApplicationRun.GetInfoForm().Add( msgs );

							/* 无论归属工单是否成功，都进行下面的操作 */
							msgs = GetProduct();

							if(msgs.IsSuccess())
							{
								actionCheckStatus = new ActionCheckStatus();
								actionCheckStatus.ProductInfo = product;
								if(actionCheckStatus.ProductInfo != null)
								{
									actionCheckStatus.ProductInfo.Resource = Resource;
								}
							}
						}
					}

					if( msgs.IsSuccess() )
					{
						if( actionCheckStatus.ProductInfo.LastSimulation == null )
						{
							ApplicationRun.GetInfoForm().Add( new UserControl.Message( MessageType.Error, "$CS_MUSTGOMO" ));

							txtRunningCard.TextFocus(true, true);
							//System.Windows.Forms.SendKeys.Send("+{TAB}");
                            //Remove UCLabel.SelectAll;

							PlayAlertMusic(SoundType.Error);

							//Laws Lu,2006/12/27 close connection manually
					
							((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
							((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;

							return;
						}
						//Laws Lu,2006/12/20 获取Rcard不需要两次从界面获取
						string rcard = actionCheckStatus.ProductInfo.LastSimulation.RunningCard;
//						string rcard = FormatHelper.PKCapitalFormat( FormatHelper.CleanString(txtRunningCard.Value) );
						string recentitemcode =  actionCheckStatus.ProductInfo.LastSimulation.ItemCode;
						//表示是第一笔rcard
						//if( this.ItemCode == String.Empty )
						if( this.ItemCode == String.Empty || rcardCollect == null || rcardCollect.Count == 0 )
						{
							this.ItemCode = recentitemcode;

							ItemFacade itemfacade = new ItemFacade( this.DataProvider );
                            object item = itemfacade.GetItem(this.ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
							this.uclVolumn.Value = (item as Item).ItemBurnInQty.ToString();
						}
						else
						{
							//ItemFacade itemfacade = new ItemFacade( this.DataProvider );
							//object item = itemfacade.GetItem( this.ItemCode );
							//if( string.Compare( this.uclVolumn.Value, (item as Item).ItemBurnInQty.ToString() ,true )!=0 )
							if (string.Compare(this.ItemCode, recentitemcode, true) != 0)	// Changed by Icyer 2006/06/13
							{
								ApplicationRun.GetInfoForm().Add( new UserControl.Message( MessageType.Error, "$CS_ItemCode_Not_Compare" ));

								txtRunningCard.TextFocus(true, true);
								//System.Windows.Forms.SendKeys.Send("+{TAB}");
                                //Remove UCLabel.SelectAll;
								PlayAlertMusic(SoundType.Error);

								//Laws Lu,2006/12/27 close connection manually
					
								((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
								((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;

								return;

							}
						}
					
						//表示是第一笔rcard
						#region 不再验证首字母
						/*
						if( this.FirstLetter == String.Empty )
						{
							this.FirstLetter = rcard.Substring(0,1);

							if( this.FirstLetter.StartsWith(BurnInTP.A))
							{
								this.lblTimePeriod.Text = "2 hours";
							}
							else if(this.FirstLetter.StartsWith(BurnInTP.B))
							{
								this.lblTimePeriod.Text = "4 hours";
							}
							else if(this.FirstLetter.StartsWith(BurnInTP.C))
							{
								this.lblTimePeriod.Text = "24 hours";
							}
							else
							{
								this.FirstLetter = String.Empty ;

								ApplicationRun.GetInfoForm().Add( new UserControl.Message( MessageType.Error, "$RCard_Not_Correct" ));

								txtRunningCard.TextFocus(true, true);
								System.Windows.Forms.SendKeys.Send("+{TAB}");
								return;
							}
						}
						else
						{
							if( string.Compare( this.FirstLetter, rcard.Substring(0,1), true )!=0 )
							{
								ApplicationRun.GetInfoForm().Add( new UserControl.Message( MessageType.Error, "$CS_FirstLetter_Not_Compare" ));

								txtRunningCard.TextFocus(true, true);
								System.Windows.Forms.SendKeys.Send("+{TAB}");
								return;

							}
						}
						*/
						#endregion
						

						if(!rcardCollect.ContainsKey( rcard ))
						{
							int count = Convert.ToInt32(this.lblRecentQTY.Text)+1 ;
							int volumn = Convert.ToInt32(this.uclVolumn.Value);

							if( count>volumn )
							{
								ApplicationRun.GetInfoForm().Add( new UserControl.Message( MessageType.Error, "$CS_Shelf_Is_Full" ));

								txtRunningCard.TextFocus(true, true);
								//System.Windows.Forms.SendKeys.Send("+{TAB}");
                                //Remove UCLabel.SelectAll;

								PlayAlertMusic(SoundType.Error);

								//Laws Lu,2006/12/27 close connection manually
					
								((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
								((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;

								return;
							}

							/* added by jessie lee 
							 * 提示检查信息 
							 * CheckID虽然会填simulation等，但是在实际Burn操作的时候，会重新获取ProductInfo*/
							#region
							ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
							//填写SIMULATION 检查工单、ID、途程、操作
							ActionEventArgs actionEventArgs = new ActionEventArgs(
								ActionType.DataCollectAction_BurnIn, 
								rcard,
								ApplicationService.Current().UserCode,
								ApplicationService.Current().ResourceCode,
								"",
								actionCheckStatus.ProductInfo);
							msgs.AddMessages( dataCollect.CheckID(actionEventArgs,actionCheckStatus));


							#endregion
	
							if(msgs.IsSuccess())
							{						
								this.lblRecentQTY.Text = count.ToString();
								rcardCollect.Add( rcard, actionCheckStatus);

								Infragistics.Win.UltraWinDataSource.UltraDataRow row1 = ultraDataSource1.Rows.Add();			
								//row1["checkbox"] = false;
								row1["runningcard"] = rcard;
								row1["itemcode"] = this.ItemCode;
								row1["mocode"] = actionCheckStatus.ProductInfo.LastSimulation.MOCode;

								//PlayAlertMusic(SoundType.Success);
							}
							else
							{
								PlayAlertMusic(SoundType.Error);
							}
						}
						else
						{
							ApplicationRun.GetInfoForm().Add( new UserControl.Message( MessageType.Error, "$CS_RCard_Is_Be_Collected" ));

							txtRunningCard.TextFocus(true, true);
							//System.Windows.Forms.SendKeys.Send("+{TAB}");
                            //Remove UCLabel.SelectAll;

							PlayAlertMusic(SoundType.Error);
							return;
						}

						//Laws Lu,2006/12/27 close connection manually
					
						((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
						((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
					}
					

					ApplicationRun.GetInfoForm().Add( msgs );
					txtRunningCard.TextFocus(true, true);
					//System.Windows.Forms.SendKeys.Send("+{TAB}");
                    //Remove UCLabel.SelectAll;
				}
			}
		}

		public void ucbBurnIn_Click(object sender, EventArgs e)
		{
			Messages messages = new Messages();
			if( _shelfFacade==null )
			{
				_shelfFacade = new ShelfFacade( this.DataProvider );
			}
			
			// Added end

			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			DataProvider.BeginTransaction();
			try
			{
				//Laws Lu,2006/12/27 修改错误提示信息
				object shelf = null;
				try
				{
					shelf = _shelfFacade.GetShelfAndLockit (  FormatHelper.CleanString(this.uclShelfNO.Value) );
				}
				catch(Exception ex)
				{
					throw new Exception("$CS_Shelf_Status_Error");
				}

				if( shelf==null )
				{
					messages.Add(new UserControl.Message( MessageType.Error, "$CS_Shelf_Not_Exist"));
					ApplicationRun.GetInfoForm().Add(messages);
					uclShelfNO.TextFocus(true, true);
					//System.Windows.Forms.SendKeys.Send("+{TAB}");

					PlayAlertMusic(SoundType.Error);
					//Laws Lu,2006/11/28 write off must commit transaction or roll back transaction
					this.DataProvider.RollbackTransaction();
					return ;
				}

				
				if(messages.IsSuccess())
				{
					object shelfTMP = _shelfFacade.GetShelfActionList( FormatHelper.CleanString(this.uclShelfNO.Value) ,Web.Helper.ShelfStatus.BurnIn);
					if( shelfTMP != null )
					{
						messages.Add(new UserControl.Message( MessageType.Error, "$CS_Shelf_Status_Error"));
						ApplicationRun.GetInfoForm().Add(messages);

						ClearPanel();
						uclShelfNO.TextFocus(true, true);
						//System.Windows.Forms.SendKeys.Send("+{TAB}");

						PlayAlertMusic(SoundType.Error);
						//Laws Lu,2006/11/28 write off must commit transaction or roll back transaction
						this.DataProvider.RollbackTransaction();
						return ;
					}
				}

				if(messages.IsSuccess())
				{
					if( (shelf as Shelf).Status == ShelfStatus.BurnIn )
					{
						messages.Add(new UserControl.Message( MessageType.Error, "$CS_Shelf_Status_Error"));
						ApplicationRun.GetInfoForm().Add(messages);

						ClearPanel();
						uclShelfNO.TextFocus(true, true);
						//System.Windows.Forms.SendKeys.Send("+{TAB}");

						PlayAlertMusic(SoundType.Error);
						//Laws Lu,2006/11/28 write off must commit transaction or roll back transaction
						this.DataProvider.RollbackTransaction();
						return ;
					}
				}

				if(messages.IsSuccess())
				{
					object volumn = _shelfFacade.GetBurnInOutVolumn( Guid.Empty.ToString() );
					if( volumn==null )
					{
						messages.Add(new UserControl.Message( MessageType.Error, "$CS_BurnInOutVolumn_Not_Maintain"));
						ApplicationRun.GetInfoForm().Add(messages);

						ClearPanel();
						uclShelfNO.TextFocus(true, true);
						//System.Windows.Forms.SendKeys.Send("+{TAB}");

						PlayAlertMusic(SoundType.Error);
						//Laws Lu,2006/11/28 write off must commit transaction or roll back transaction
						this.DataProvider.RollbackTransaction();
						return ;
					}

					if(messages.IsSuccess())
					{
						if( (volumn as BurnInOutVolumn).Total < ((volumn as BurnInOutVolumn).Used +1) )
						{
							messages.Add(new UserControl.Message( MessageType.Error, "$CS_BurnInOutVolumn_Is_Full"));
							ApplicationRun.GetInfoForm().Add(messages);

							ClearPanel();
							uclShelfNO.TextFocus(true, true);
							//System.Windows.Forms.SendKeys.Send("+{TAB}");

							PlayAlertMusic(SoundType.Error);
							//Laws Lu,2006/11/28 write off must commit transaction or roll back transaction
							this.DataProvider.RollbackTransaction();
							return ;
						}
					}
				}

				if(messages.IsSuccess())
				{
					// Added by Icyer 2006/07/05
					if (rcardCollect == null || rcardCollect.Count == 0)
					{
						messages.Add(new UserControl.Message( MessageType.Error, "$CS_Please_Input_RunningCard"));
						ApplicationRun.GetInfoForm().Add(messages);
						Application.DoEvents();
						this.txtRunningCard.TextFocus(true, true);

						PlayAlertMusic(SoundType.Error);
						//Laws Lu,2006/11/28 write off must commit transaction or roll back transaction
						this.DataProvider.RollbackTransaction();
						return ;
					}
				}
				ShelfActionList shelfActionList = _shelfFacade.CreateNewShelfActionList();
				if(messages.IsSuccess())
				{
					/* step1: 改变shelf的状态 */
					(shelf as Shelf).Status = ShelfStatus.BurnIn;
					(shelf as Shelf).MaintainUser = ApplicationService.Current().UserCode;

					/* step2: 记录shelf */
					
					shelfActionList.PKID = Guid.NewGuid().ToString();
					shelfActionList.ShelfNO = (shelf as Shelf).ShelfNO;
					shelfActionList.Status = ShelfStatus.BurnIn;

					/* modified by jessie lee, 2006/6/22
					 * eAttribute1记录本车容量 */
					shelfActionList.eAttribute1 = this.lblRecentQTY.Text;

					//shelfActionList.BurnInTimePeriod = this.FirstLetter;
					/* modified by jessie lee,改为直接记录BrunIn时长，以小时为单位 */
					shelfActionList.BurnInTimePeriod = this.numericUpDown1.Value.ToString();

					DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

					shelfActionList.BurnInDate = dbDateTime.DBDate;
					shelfActionList.BurnInTime = dbDateTime.DBTime;
					shelfActionList.BurnInUser = ApplicationService.Current().UserCode;
					shelfActionList.MaintainUser = ApplicationService.Current().UserCode;
					

					/* step4: rcard burn in */
					foreach( DictionaryEntry dic in rcardCollect )
					{
						string rcard = dic.Key.ToString();
						ActionCheckStatus actionCheckStatus1 = (ActionCheckStatus)dic.Value;

						messages.AddMessages( RunBurnIn(rcard, actionCheckStatus1, shelfActionList.PKID) );
					
						if (!messages.IsSuccess())
						{
							messages.ClearMessages();
							messages.Add( new UserControl.Message( MessageType.Error, "$CS_RCARD_CanNot_Burn_In [" + rcard + "]"));
							// Added by Icyer 2006/06/13
							for (int i = 0; i < ultraDataSource1.Rows.Count; i++)
							{
								if (ultraDataSource1.Rows[i]["runningcard"].ToString() == rcard)
								{
									this.ultraGrid1.Selected.Rows.Clear();
									this.ultraGrid1.Rows[i].Selected = true;
									break;
								}
							}
							break;
							// Added end
						}
					}
				}

				if (messages.IsSuccess())
				{
					//Laws Lu,2006/11/31 change update database sequence 
					Common.Log.Info("Start Update BurnIn");
					_shelfFacade.UpdateShelf(shelf as Shelf);
					//Laws Lu,2006/11/31 update detail data 
					Common.Log.Info("Start Update BurnIn Detail");
					_shelfFacade.AddShelfActionList(shelfActionList);	

					/* 车位计数，加1 */
					_shelfFacade.UpdateBurnInOutVolumnBySqlAdd(ApplicationService.Current().UserCode);

					DataProvider.CommitTransaction();
					ApplicationRun.GetInfoForm().Add(messages);
					ClearPanel();
					PlayAlertMusic(SoundType.Success);
				}
				else
				{
					DataProvider.RollbackTransaction();
					ApplicationRun.GetInfoForm().Add(messages);
					//ClearPanel();
					PlayAlertMusic(SoundType.Error);
				}
			}
			catch (Exception ex)
			{
				DataProvider.RollbackTransaction();
				messages.Add(new UserControl.Message(ex));
				ApplicationRun.GetInfoForm().Add(messages);
				PlayAlertMusic(SoundType.Error);
			}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
			}

		}

		/// <summary>
		/// 获得产品信息
		/// </summary>
		/// <returns></returns>
		private Messages GetProduct()
		{
			Messages productmessages=new Messages ();
			ActionOnLineHelper dataCollect = new ActionOnLineHelper(DataProvider);		
			try
			{
				productmessages.AddMessages( dataCollect.GetIDInfo(txtRunningCard.Value.Trim()));
				if (productmessages.IsSuccess() )
				{  
					product=(ProductInfo)productmessages.GetData().Values[0];					
				}
				else
				{
					txtRunningCard.Value = String.Empty;
				}
			}
			catch (Exception e)
			{
				productmessages.Add(new UserControl.Message(e));

			}
			return productmessages;
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

		/// <summary>
		/// 工单归属采集
		/// </summary>
		/// <returns></returns>
		private Messages RunGOMO(ActionCheckStatus actionCheckStatus)
		{
			Messages messages=new Messages ();
			try
			{
				//Laws Lu,2005/09/14,新增	工单不能为空
				if(txtGOMO.Checked == true)// && txtGOMO.Value.Trim() != String.Empty)
				{
					actionCheckStatus.ProductInfo = product;

					/* Power0015  Burn in 站工单不能自动读条码识别    6月19日  
					 * Feedback中的第15项,需要按照如下逻辑实现:
					 * 1,保留目前的归属工单勾选项,去掉后面的文本框
					 * 2,如果勾选了归属工单,系统自动截取产品序列号的第7~11位,在截取的字符串前加年和短横线"-",作为工单号码,但是考虑到以后可能变更产品序列号编码规则,因此截取的位置可以由用户自行定义(可放在界面或配置文件中)
					 * 3,执行归属工单时如果系统判断到产品序列号已经归属工单系统给出Msg提示,但是仍然允许其作BurnIn.(类似目前的良品/不良品采集的处理方式)
					 * 4,如果用户不勾选归属工单选项则不处理归属工单逻辑. 
					 * 
					 * added by jessie lee, 2006/6/16
					 * 解析工单*/

					string rcard = FormatHelper.PKCapitalFormat( FormatHelper.CleanString(txtRunningCard.Value) );
					string index = System.Configuration.ConfigurationSettings.AppSettings["AutoGoMOIndex"];
					string len = System.Configuration.ConfigurationSettings.AppSettings["AutoGoMOLen"];

					// Added by Icyer 2006/07/06
					if (rcard.Length < Convert.ToInt32(index) + Convert.ToInt32(len) - 1)
					{
						messages.Add(new UserControl.Message(MessageType.Error, "$Format_Error"));
						return messages;
					}
					// Added end

					string mocode = DateTime.Now.Year.ToString()+"-"+ rcard.Substring( Convert.ToInt32(index)-1, Convert.ToInt32(len) );

					//Laws Lu,新建数据采集Action
					ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);

					IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_GoMO);
					messages.AddMessages(onLine.Action(new GoToMOActionEventArgs(
						ActionType.DataCollectAction_GoMO, 
						rcard,
						ApplicationService.Current().UserCode,
						ApplicationService.Current().ResourceCode,
						product,
						mocode ),actionCheckStatus));
				
					if (messages.IsSuccess())
					{
						messages.Add(new UserControl.Message(MessageType.Success ,string.Format("$CS_GOMOSUCCESS,$CS_Param_ID:{0}", txtRunningCard.Value.Trim())));
					}
				}
				return messages;
			}
			catch (Exception e)
			{
				messages.Add(new UserControl.Message(e));
				return messages;
			}
		}

		/// <summary>
		/// RCard Burn In
		/// </summary>
		/// <returns></returns>
		private Messages RunBurnIn(string rcard, ActionCheckStatus actionCheckStatus, string shelfPK)
		{
			Messages messages=new Messages ();
			try
			{
				messages = GetProduct( rcard );
				if (messages.IsSuccess())
				{
					ProductInfo product1 = (ProductInfo)messages.GetData().Values[0];
					actionCheckStatus.ProductInfo = product1;					

					IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_BurnIn);
					ActionEventArgs actionEventArgs = new ActionEventArgs(
						ActionType.DataCollectAction_BurnIn, 
						rcard,
						ApplicationService.Current().UserCode,
						ApplicationService.Current().ResourceCode,
						shelfPK,
						product1);
					messages.AddMessages( ((IActionWithStatus)dataCollectModule).Execute(
						actionEventArgs,
						actionCheckStatus));
				}

				if (messages.IsSuccess())
				{
					messages.Add(new UserControl.Message(MessageType.Success ,string.Format("$CS_BurnInSUCCESS,$CS_Param_ID:{0}", rcard )));
				}
				return messages;
			}
			catch (Exception e)
			{
				messages.Add(new UserControl.Message(e));
				return messages;
			}
		}

		// Added by Icyer 2006/06/13
		private void ucbRemove_Click(object sender, System.EventArgs e)
		{
			if (this.ultraGrid1.Selected.Rows.Count > 0)
			{
				for (int i = this.ultraGrid1.Selected.Rows.Count - 1; i >= 0; i--)
				{
					int iIdx = this.ultraGrid1.Selected.Rows[i].Index;
					rcardCollect.Remove(this.ultraDataSource1.Rows[iIdx]["runningcard"].ToString());
					this.ultraDataSource1.Rows.RemoveAt(iIdx);
					this.lblRecentQTY.Text = (Convert.ToInt32(this.lblRecentQTY.Text) - 1).ToString();
				}
			}
		}

		private void FBurnIn_Load(object sender, EventArgs e)
		{
			UserControl.UIStyleBuilder.FormUI(this);  
			UserControl.UIStyleBuilder.GridUI(ultraGrid1);
		}
		// Added end

		private void PlayAlertMusic(string operationType)
		{
			string strPath = Application.StartupPath;
			if (strPath.EndsWith(@"\") == false)
				strPath += @"\";
			strPath += "Music";
			string[] files = null;
			if (System.IO.Directory.Exists(strPath))
			{
				files = System.IO.Directory.GetFiles(strPath, "*.wav");
			}
			else
			{
				System.IO.Directory.CreateDirectory(strPath);
			}

			if ( files!=null && files.Length>0 )
			{
				strPath += @"\" + operationType +".wav";
				if(System.IO.File.Exists(strPath))
				{
					PlaySound(strPath, 0, SND_FILENAME);
				}
			}
		}

	}
}
