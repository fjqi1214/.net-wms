#region system
using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
#endregion


#region Project
using BenQGuru.eMES.TS;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.DataCollect;
using UserControl;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.Domain.TS;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.Package;

#endregion

namespace BenQGuru.eMES.Client
{
	public class FTSInput : Form
	{
		private Infragistics.Win.UltraWinTabControl.UltraTabControl ultraTabControl1;
		private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
		private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl1;
		private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl2;
		private Infragistics.Win.Misc.UltraLabel ultraLabel6;
		private System.Windows.Forms.GroupBox groupBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private UserControl.UCButton ucButton1;
		private UserControl.UCButton ucButton2;
		private UserControl.UCButton ucButton5;
		private UserControl.UCLabelEdit ucEditRunningCard;
		private UserControl.UCLabelEdit ucEditItemCode;
		private UserControl.UCLabelEdit ucEditResource;
		private UserControl.UCDatetTime ucDateTimeStart;
		private UserControl.UCDatetTime ucDateTimeEnd;
		private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridMain;
		private UserControl.UCButton ucButtonExit;
		private UserControl.UCButton ucButtonQuery;
		private System.ComponentModel.IContainer components = null;
		private System.Data.DataTable dtSource = new DataTable();
		private UserControl.UCLabelEdit ucEditStepsequence;
		private UserControl.UCLabelEdit ucEditOperation;
		private UserControl.UCLabelEdit ucEditMOCode;
		private Infragistics.Win.UltraWinEditors.UltraCheckEditor ultraCheckNewStatus;
		private UserControl.UCLabelEdit ucLabEditFindUser;
		private UserControl.UCLabelEdit ucLabEditResource;
		private UserControl.UCLabelEdit ucLabEditOperation;
		private UserControl.UCLabelEdit ucLabEditStepsequence;
		private UserControl.UCLabelEdit ucLabEditItemCode;
		private UserControl.UCLabelEdit ucLabEditMOCode;
		private System.Windows.Forms.ListBox listBoxErrorCodeList;
		private UserControl.UCLabelEdit ucLabEditRunningCard;
		private UserControl.UCDatetTime ucDatetTimeFind;
		private UserControl.UCLabelEdit txtLastTSer;
		private Infragistics.Win.UltraWinEditors.UltraCheckEditor chkMoreNG;
		private UserControl.UCLabelEdit txtAgentUser; 


		private IDomainDataProvider _domainDataProvider =ApplicationService.Current().DataProvider;


		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}



		public FTSInput()
		{
			// 该调用是 Windows 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化
			UserControl.UIStyleBuilder.GridUI(this.ultraGridMain);
			UserControl.UIStyleBuilder.FormUI(this);
			InitForm();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTSInput));
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("序列号");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("产品别");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("工单");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("产品代码");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("产线");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("工序");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("机台");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("发现人员");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("发现时间");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("接收人员");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("接收时间");
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.chkMoreNG = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.txtLastTSer = new UserControl.UCLabelEdit();
            this.ucDatetTimeFind = new UserControl.UCDatetTime();
            this.ucLabEditFindUser = new UserControl.UCLabelEdit();
            this.ucLabEditResource = new UserControl.UCLabelEdit();
            this.ucLabEditOperation = new UserControl.UCLabelEdit();
            this.ucLabEditStepsequence = new UserControl.UCLabelEdit();
            this.ucLabEditItemCode = new UserControl.UCLabelEdit();
            this.ucLabEditMOCode = new UserControl.UCLabelEdit();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.ucButton5 = new UserControl.UCButton();
            this.ucLabEditRunningCard = new UserControl.UCLabelEdit();
            this.ucButton2 = new UserControl.UCButton();
            this.ucButton1 = new UserControl.UCButton();
            this.txtAgentUser = new UserControl.UCLabelEdit();
            this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
            this.listBoxErrorCodeList = new System.Windows.Forms.ListBox();
            this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ultraGridMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeEnd = new UserControl.UCDatetTime();
            this.ucDateTimeStart = new UserControl.UCDatetTime();
            this.ucEditResource = new UserControl.UCLabelEdit();
            this.ucEditStepsequence = new UserControl.UCLabelEdit();
            this.ucEditItemCode = new UserControl.UCLabelEdit();
            this.ucEditOperation = new UserControl.UCLabelEdit();
            this.ucEditRunningCard = new UserControl.UCLabelEdit();
            this.ucEditMOCode = new UserControl.UCLabelEdit();
            this.ucButtonExit = new UserControl.UCButton();
            this.ucButtonQuery = new UserControl.UCButton();
            this.ultraCheckNewStatus = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.ultraTabPageControl2.SuspendLayout();
            this.groupBox.SuspendLayout();
            this.ultraTabPageControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).BeginInit();
            this.ultraTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraTabPageControl2
            // 
            this.ultraTabPageControl2.Controls.Add(this.chkMoreNG);
            this.ultraTabPageControl2.Controls.Add(this.txtLastTSer);
            this.ultraTabPageControl2.Controls.Add(this.ucDatetTimeFind);
            this.ultraTabPageControl2.Controls.Add(this.ucLabEditFindUser);
            this.ultraTabPageControl2.Controls.Add(this.ucLabEditResource);
            this.ultraTabPageControl2.Controls.Add(this.ucLabEditOperation);
            this.ultraTabPageControl2.Controls.Add(this.ucLabEditStepsequence);
            this.ultraTabPageControl2.Controls.Add(this.ucLabEditItemCode);
            this.ultraTabPageControl2.Controls.Add(this.ucLabEditMOCode);
            this.ultraTabPageControl2.Controls.Add(this.groupBox);
            this.ultraTabPageControl2.Controls.Add(this.ultraLabel6);
            this.ultraTabPageControl2.Controls.Add(this.listBoxErrorCodeList);
            this.ultraTabPageControl2.Location = new System.Drawing.Point(2, 24);
            this.ultraTabPageControl2.Name = "ultraTabPageControl2";
            this.ultraTabPageControl2.Size = new System.Drawing.Size(660, 518);
            // 
            // chkMoreNG
            // 
            this.chkMoreNG.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkMoreNG.Enabled = false;
            this.chkMoreNG.Location = new System.Drawing.Point(264, 72);
            this.chkMoreNG.Name = "chkMoreNG";
            this.chkMoreNG.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkMoreNG.Size = new System.Drawing.Size(104, 20);
            this.chkMoreNG.TabIndex = 192;
            this.chkMoreNG.Text = "是否二次不良";
            // 
            // txtLastTSer
            // 
            this.txtLastTSer.AllowEditOnlyChecked = true;
            this.txtLastTSer.Caption = "上一位维修人员";
            this.txtLastTSer.Checked = false;
            this.txtLastTSer.EditType = UserControl.EditTypes.String;
            this.txtLastTSer.Location = new System.Drawing.Point(405, 72);
            this.txtLastTSer.MaxLength = 40;
            this.txtLastTSer.Multiline = false;
            this.txtLastTSer.Name = "txtLastTSer";
            this.txtLastTSer.PasswordChar = '\0';
            this.txtLastTSer.ReadOnly = true;
            this.txtLastTSer.ShowCheckBox = false;
            this.txtLastTSer.Size = new System.Drawing.Size(230, 24);
            this.txtLastTSer.TabIndex = 191;
            this.txtLastTSer.TabNext = true;
            this.txtLastTSer.Value = "";
            this.txtLastTSer.WidthType = UserControl.WidthTypes.Normal;
            this.txtLastTSer.XAlign = 502;
            // 
            // ucDatetTimeFind
            // 
            this.ucDatetTimeFind.Caption = "发现时间";
            this.ucDatetTimeFind.Enabled = false;
            this.ucDatetTimeFind.Location = new System.Drawing.Point(12, 72);
            this.ucDatetTimeFind.Name = "ucDatetTimeFind";
            this.ucDatetTimeFind.ShowType = UserControl.DateTimeTypes.DateTime;
            this.ucDatetTimeFind.Size = new System.Drawing.Size(222, 21);
            this.ucDatetTimeFind.TabIndex = 190;
            this.ucDatetTimeFind.Value = new System.DateTime(2005, 7, 26, 15, 13, 23, 0);
            this.ucDatetTimeFind.XAlign = 75;
            // 
            // ucLabEditFindUser
            // 
            this.ucLabEditFindUser.AllowEditOnlyChecked = true;
            this.ucLabEditFindUser.Caption = "发现人员";
            this.ucLabEditFindUser.Checked = false;
            this.ucLabEditFindUser.EditType = UserControl.EditTypes.String;
            this.ucLabEditFindUser.Location = new System.Drawing.Point(441, 38);
            this.ucLabEditFindUser.MaxLength = 40;
            this.ucLabEditFindUser.Multiline = false;
            this.ucLabEditFindUser.Name = "ucLabEditFindUser";
            this.ucLabEditFindUser.PasswordChar = '\0';
            this.ucLabEditFindUser.ReadOnly = true;
            this.ucLabEditFindUser.ShowCheckBox = false;
            this.ucLabEditFindUser.Size = new System.Drawing.Size(194, 24);
            this.ucLabEditFindUser.TabIndex = 175;
            this.ucLabEditFindUser.TabNext = true;
            this.ucLabEditFindUser.Value = "WORKER1";
            this.ucLabEditFindUser.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabEditFindUser.XAlign = 502;
            // 
            // ucLabEditResource
            // 
            this.ucLabEditResource.AllowEditOnlyChecked = true;
            this.ucLabEditResource.Caption = "资源";
            this.ucLabEditResource.Checked = false;
            this.ucLabEditResource.EditType = UserControl.EditTypes.String;
            this.ucLabEditResource.Location = new System.Drawing.Point(256, 38);
            this.ucLabEditResource.MaxLength = 40;
            this.ucLabEditResource.Multiline = false;
            this.ucLabEditResource.Name = "ucLabEditResource";
            this.ucLabEditResource.PasswordChar = '\0';
            this.ucLabEditResource.ReadOnly = true;
            this.ucLabEditResource.ShowCheckBox = false;
            this.ucLabEditResource.Size = new System.Drawing.Size(170, 24);
            this.ucLabEditResource.TabIndex = 174;
            this.ucLabEditResource.TabNext = true;
            this.ucLabEditResource.Value = "RES1";
            this.ucLabEditResource.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabEditResource.XAlign = 293;
            // 
            // ucLabEditOperation
            // 
            this.ucLabEditOperation.AllowEditOnlyChecked = true;
            this.ucLabEditOperation.Caption = "发现工序";
            this.ucLabEditOperation.Checked = false;
            this.ucLabEditOperation.EditType = UserControl.EditTypes.String;
            this.ucLabEditOperation.Location = new System.Drawing.Point(17, 38);
            this.ucLabEditOperation.MaxLength = 40;
            this.ucLabEditOperation.Multiline = false;
            this.ucLabEditOperation.Name = "ucLabEditOperation";
            this.ucLabEditOperation.PasswordChar = '\0';
            this.ucLabEditOperation.ReadOnly = true;
            this.ucLabEditOperation.ShowCheckBox = false;
            this.ucLabEditOperation.Size = new System.Drawing.Size(194, 24);
            this.ucLabEditOperation.TabIndex = 173;
            this.ucLabEditOperation.TabNext = true;
            this.ucLabEditOperation.Value = "OP1";
            this.ucLabEditOperation.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabEditOperation.XAlign = 78;
            // 
            // ucLabEditStepsequence
            // 
            this.ucLabEditStepsequence.AllowEditOnlyChecked = true;
            this.ucLabEditStepsequence.Caption = "产线";
            this.ucLabEditStepsequence.Checked = false;
            this.ucLabEditStepsequence.EditType = UserControl.EditTypes.String;
            this.ucLabEditStepsequence.Location = new System.Drawing.Point(464, 8);
            this.ucLabEditStepsequence.MaxLength = 40;
            this.ucLabEditStepsequence.Multiline = false;
            this.ucLabEditStepsequence.Name = "ucLabEditStepsequence";
            this.ucLabEditStepsequence.PasswordChar = '\0';
            this.ucLabEditStepsequence.ReadOnly = true;
            this.ucLabEditStepsequence.ShowCheckBox = false;
            this.ucLabEditStepsequence.Size = new System.Drawing.Size(170, 24);
            this.ucLabEditStepsequence.TabIndex = 172;
            this.ucLabEditStepsequence.TabNext = true;
            this.ucLabEditStepsequence.Value = "LINE1";
            this.ucLabEditStepsequence.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabEditStepsequence.XAlign = 501;
            // 
            // ucLabEditItemCode
            // 
            this.ucLabEditItemCode.AllowEditOnlyChecked = true;
            this.ucLabEditItemCode.Caption = "产品代码";
            this.ucLabEditItemCode.Checked = false;
            this.ucLabEditItemCode.EditType = UserControl.EditTypes.String;
            this.ucLabEditItemCode.Location = new System.Drawing.Point(233, 8);
            this.ucLabEditItemCode.MaxLength = 40;
            this.ucLabEditItemCode.Multiline = false;
            this.ucLabEditItemCode.Name = "ucLabEditItemCode";
            this.ucLabEditItemCode.PasswordChar = '\0';
            this.ucLabEditItemCode.ReadOnly = true;
            this.ucLabEditItemCode.ShowCheckBox = false;
            this.ucLabEditItemCode.Size = new System.Drawing.Size(194, 24);
            this.ucLabEditItemCode.TabIndex = 171;
            this.ucLabEditItemCode.TabNext = true;
            this.ucLabEditItemCode.Value = "ITEM1";
            this.ucLabEditItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabEditItemCode.XAlign = 294;
            // 
            // ucLabEditMOCode
            // 
            this.ucLabEditMOCode.AllowEditOnlyChecked = true;
            this.ucLabEditMOCode.Caption = "工单";
            this.ucLabEditMOCode.Checked = false;
            this.ucLabEditMOCode.EditType = UserControl.EditTypes.String;
            this.ucLabEditMOCode.Location = new System.Drawing.Point(41, 8);
            this.ucLabEditMOCode.MaxLength = 40;
            this.ucLabEditMOCode.Multiline = false;
            this.ucLabEditMOCode.Name = "ucLabEditMOCode";
            this.ucLabEditMOCode.PasswordChar = '\0';
            this.ucLabEditMOCode.ReadOnly = true;
            this.ucLabEditMOCode.ShowCheckBox = false;
            this.ucLabEditMOCode.Size = new System.Drawing.Size(170, 24);
            this.ucLabEditMOCode.TabIndex = 170;
            this.ucLabEditMOCode.TabNext = true;
            this.ucLabEditMOCode.Value = "MO1";
            this.ucLabEditMOCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabEditMOCode.XAlign = 78;
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.ucButton5);
            this.groupBox.Controls.Add(this.ucLabEditRunningCard);
            this.groupBox.Controls.Add(this.ucButton2);
            this.groupBox.Controls.Add(this.ucButton1);
            this.groupBox.Controls.Add(this.txtAgentUser);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox.Location = new System.Drawing.Point(0, 438);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(660, 80);
            this.groupBox.TabIndex = 169;
            this.groupBox.TabStop = false;
            // 
            // ucButton5
            // 
            this.ucButton5.BackColor = System.Drawing.SystemColors.Control;
            this.ucButton5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButton5.BackgroundImage")));
            this.ucButton5.ButtonType = UserControl.ButtonTypes.Cancle;
            this.ucButton5.Caption = "取消";
            this.ucButton5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButton5.Location = new System.Drawing.Point(432, 48);
            this.ucButton5.Name = "ucButton5";
            this.ucButton5.Size = new System.Drawing.Size(88, 22);
            this.ucButton5.TabIndex = 11;
            this.ucButton5.Visible = false;
            // 
            // ucLabEditRunningCard
            // 
            this.ucLabEditRunningCard.AllowEditOnlyChecked = true;
            this.ucLabEditRunningCard.Caption = "产品序列号";
            this.ucLabEditRunningCard.Checked = false;
            this.ucLabEditRunningCard.EditType = UserControl.EditTypes.String;
            this.ucLabEditRunningCard.Location = new System.Drawing.Point(57, 48);
            this.ucLabEditRunningCard.MaxLength = 40;
            this.ucLabEditRunningCard.Multiline = false;
            this.ucLabEditRunningCard.Name = "ucLabEditRunningCard";
            this.ucLabEditRunningCard.PasswordChar = '\0';
            this.ucLabEditRunningCard.ReadOnly = false;
            this.ucLabEditRunningCard.ShowCheckBox = false;
            this.ucLabEditRunningCard.Size = new System.Drawing.Size(206, 24);
            this.ucLabEditRunningCard.TabIndex = 0;
            this.ucLabEditRunningCard.TabNext = false;
            this.ucLabEditRunningCard.Value = "";
            this.ucLabEditRunningCard.WidthType = UserControl.WidthTypes.Normal;
            this.ucLabEditRunningCard.XAlign = 130;
            this.ucLabEditRunningCard.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLabEditRunningCard_TxtboxKeyPress);
            // 
            // ucButton2
            // 
            this.ucButton2.BackColor = System.Drawing.SystemColors.Control;
            this.ucButton2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButton2.BackgroundImage")));
            this.ucButton2.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButton2.Caption = "退出";
            this.ucButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButton2.Location = new System.Drawing.Point(552, 48);
            this.ucButton2.Name = "ucButton2";
            this.ucButton2.Size = new System.Drawing.Size(88, 22);
            this.ucButton2.TabIndex = 9;
            // 
            // ucButton1
            // 
            this.ucButton1.BackColor = System.Drawing.SystemColors.Control;
            this.ucButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButton1.BackgroundImage")));
            this.ucButton1.ButtonType = UserControl.ButtonTypes.Confirm;
            this.ucButton1.Caption = "确认";
            this.ucButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButton1.Location = new System.Drawing.Point(312, 48);
            this.ucButton1.Name = "ucButton1";
            this.ucButton1.Size = new System.Drawing.Size(88, 22);
            this.ucButton1.TabIndex = 8;
            this.ucButton1.Visible = false;
            // 
            // txtAgentUser
            // 
            this.txtAgentUser.AllowEditOnlyChecked = true;
            this.txtAgentUser.Caption = "代录维修人员";
            this.txtAgentUser.Checked = false;
            this.txtAgentUser.EditType = UserControl.EditTypes.String;
            this.txtAgentUser.Location = new System.Drawing.Point(29, 16);
            this.txtAgentUser.MaxLength = 20;
            this.txtAgentUser.Multiline = false;
            this.txtAgentUser.Name = "txtAgentUser";
            this.txtAgentUser.PasswordChar = '\0';
            this.txtAgentUser.ReadOnly = false;
            this.txtAgentUser.ShowCheckBox = true;
            this.txtAgentUser.Size = new System.Drawing.Size(234, 24);
            this.txtAgentUser.TabIndex = 193;
            this.txtAgentUser.TabNext = false;
            this.txtAgentUser.Value = "";
            this.txtAgentUser.WidthType = UserControl.WidthTypes.Normal;
            this.txtAgentUser.XAlign = 130;
            // 
            // ultraLabel6
            // 
            this.ultraLabel6.Location = new System.Drawing.Point(8, 99);
            this.ultraLabel6.Name = "ultraLabel6";
            this.ultraLabel6.Size = new System.Drawing.Size(128, 16);
            this.ultraLabel6.TabIndex = 168;
            this.ultraLabel6.Text = "不良代码组:不良代码";
            // 
            // listBoxErrorCodeList
            // 
            this.listBoxErrorCodeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxErrorCodeList.Items.AddRange(new object[] {
            "不良代码组2:Error1",
            "不良代码组2:Error2",
            "不良代码组2:Error3",
            "不良代码组2:Error4",
            "不良代码组3:Error1",
            "不良代码组3:Error3",
            "不良代码组3:Error2"});
            this.listBoxErrorCodeList.Location = new System.Drawing.Point(8, 120);
            this.listBoxErrorCodeList.Name = "listBoxErrorCodeList";
            this.listBoxErrorCodeList.Size = new System.Drawing.Size(648, 329);
            this.listBoxErrorCodeList.TabIndex = 167;
            // 
            // ultraTabPageControl1
            // 
            this.ultraTabPageControl1.Controls.Add(this.ultraGridMain);
            this.ultraTabPageControl1.Controls.Add(this.groupBox1);
            this.ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabPageControl1.Name = "ultraTabPageControl1";
            this.ultraTabPageControl1.Size = new System.Drawing.Size(660, 518);
            // 
            // ultraGridMain
            // 
            this.ultraGridMain.Cursor = System.Windows.Forms.Cursors.Default;
            ultraGridColumn3.Header.VisiblePosition = 3;
            ultraGridColumn4.Header.VisiblePosition = 2;
            ultraGridColumn6.Header.Caption = "工位";
            ultraGridColumn6.Header.VisiblePosition = 6;
            ultraGridColumn7.Header.VisiblePosition = 5;
            ultraGridBand1.Columns.Add(ultraGridColumn1);
            ultraGridBand1.Columns.Add(ultraGridColumn2);
            ultraGridBand1.Columns.Add(ultraGridColumn3);
            ultraGridBand1.Columns.Add(ultraGridColumn4);
            ultraGridBand1.Columns.Add(ultraGridColumn5);
            ultraGridBand1.Columns.Add(ultraGridColumn6);
            ultraGridBand1.Columns.Add(ultraGridColumn7);
            ultraGridBand1.Columns.Add(ultraGridColumn8);
            ultraGridBand1.Columns.Add(ultraGridColumn9);
            ultraGridBand1.Columns.Add(ultraGridColumn10);
            ultraGridBand1.Columns.Add(ultraGridColumn11);
            this.ultraGridMain.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridMain.Location = new System.Drawing.Point(0, 136);
            this.ultraGridMain.Name = "ultraGridMain";
            this.ultraGridMain.Size = new System.Drawing.Size(660, 382);
            this.ultraGridMain.TabIndex = 170;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucDateTimeEnd);
            this.groupBox1.Controls.Add(this.ucDateTimeStart);
            this.groupBox1.Controls.Add(this.ucEditResource);
            this.groupBox1.Controls.Add(this.ucEditStepsequence);
            this.groupBox1.Controls.Add(this.ucEditItemCode);
            this.groupBox1.Controls.Add(this.ucEditOperation);
            this.groupBox1.Controls.Add(this.ucEditRunningCard);
            this.groupBox1.Controls.Add(this.ucEditMOCode);
            this.groupBox1.Controls.Add(this.ucButtonExit);
            this.groupBox1.Controls.Add(this.ucButtonQuery);
            this.groupBox1.Controls.Add(this.ultraCheckNewStatus);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(660, 136);
            this.groupBox1.TabIndex = 168;
            this.groupBox1.TabStop = false;
            // 
            // ucDateTimeEnd
            // 
            this.ucDateTimeEnd.Caption = "结束时间";
            this.ucDateTimeEnd.Location = new System.Drawing.Point(271, 72);
            this.ucDateTimeEnd.Name = "ucDateTimeEnd";
            this.ucDateTimeEnd.ShowType = UserControl.DateTimeTypes.DateTime;
            this.ucDateTimeEnd.Size = new System.Drawing.Size(222, 21);
            this.ucDateTimeEnd.TabIndex = 8;
            this.ucDateTimeEnd.Value = new System.DateTime(2005, 7, 26, 15, 13, 23, 0);
            this.ucDateTimeEnd.XAlign = 334;
            // 
            // ucDateTimeStart
            // 
            this.ucDateTimeStart.Caption = "开始时间";
            this.ucDateTimeStart.Location = new System.Drawing.Point(17, 72);
            this.ucDateTimeStart.Name = "ucDateTimeStart";
            this.ucDateTimeStart.ShowType = UserControl.DateTimeTypes.DateTime;
            this.ucDateTimeStart.Size = new System.Drawing.Size(222, 21);
            this.ucDateTimeStart.TabIndex = 7;
            this.ucDateTimeStart.Value = new System.DateTime(2005, 7, 26, 15, 13, 23, 0);
            this.ucDateTimeStart.XAlign = 80;
            // 
            // ucEditResource
            // 
            this.ucEditResource.AllowEditOnlyChecked = true;
            this.ucEditResource.Caption = "资源";
            this.ucEditResource.Checked = false;
            this.ucEditResource.EditType = UserControl.EditTypes.String;
            this.ucEditResource.Location = new System.Drawing.Point(471, 40);
            this.ucEditResource.MaxLength = 40;
            this.ucEditResource.Multiline = false;
            this.ucEditResource.Name = "ucEditResource";
            this.ucEditResource.PasswordChar = '\0';
            this.ucEditResource.ReadOnly = false;
            this.ucEditResource.ShowCheckBox = false;
            this.ucEditResource.Size = new System.Drawing.Size(170, 24);
            this.ucEditResource.TabIndex = 6;
            this.ucEditResource.TabNext = true;
            this.ucEditResource.Value = "";
            this.ucEditResource.WidthType = UserControl.WidthTypes.Normal;
            this.ucEditResource.XAlign = 508;
            // 
            // ucEditStepsequence
            // 
            this.ucEditStepsequence.AllowEditOnlyChecked = true;
            this.ucEditStepsequence.Caption = "产线";
            this.ucEditStepsequence.Checked = false;
            this.ucEditStepsequence.EditType = UserControl.EditTypes.String;
            this.ucEditStepsequence.Location = new System.Drawing.Point(258, 40);
            this.ucEditStepsequence.MaxLength = 40;
            this.ucEditStepsequence.Multiline = false;
            this.ucEditStepsequence.Name = "ucEditStepsequence";
            this.ucEditStepsequence.PasswordChar = '\0';
            this.ucEditStepsequence.ReadOnly = false;
            this.ucEditStepsequence.ShowCheckBox = false;
            this.ucEditStepsequence.Size = new System.Drawing.Size(170, 24);
            this.ucEditStepsequence.TabIndex = 5;
            this.ucEditStepsequence.TabNext = true;
            this.ucEditStepsequence.Value = "";
            this.ucEditStepsequence.WidthType = UserControl.WidthTypes.Normal;
            this.ucEditStepsequence.XAlign = 295;
            // 
            // ucEditItemCode
            // 
            this.ucEditItemCode.AllowEditOnlyChecked = true;
            this.ucEditItemCode.Caption = "产品代码";
            this.ucEditItemCode.Checked = false;
            this.ucEditItemCode.EditType = UserControl.EditTypes.String;
            this.ucEditItemCode.Location = new System.Drawing.Point(234, 8);
            this.ucEditItemCode.MaxLength = 40;
            this.ucEditItemCode.Multiline = false;
            this.ucEditItemCode.Name = "ucEditItemCode";
            this.ucEditItemCode.PasswordChar = '\0';
            this.ucEditItemCode.ReadOnly = false;
            this.ucEditItemCode.ShowCheckBox = false;
            this.ucEditItemCode.Size = new System.Drawing.Size(194, 24);
            this.ucEditItemCode.TabIndex = 2;
            this.ucEditItemCode.TabNext = true;
            this.ucEditItemCode.Value = "";
            this.ucEditItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucEditItemCode.XAlign = 295;
            // 
            // ucEditOperation
            // 
            this.ucEditOperation.AllowEditOnlyChecked = true;
            this.ucEditOperation.Caption = "工位";
            this.ucEditOperation.Checked = false;
            this.ucEditOperation.EditType = UserControl.EditTypes.String;
            this.ucEditOperation.Location = new System.Drawing.Point(45, 40);
            this.ucEditOperation.MaxLength = 40;
            this.ucEditOperation.Multiline = false;
            this.ucEditOperation.Name = "ucEditOperation";
            this.ucEditOperation.PasswordChar = '\0';
            this.ucEditOperation.ReadOnly = false;
            this.ucEditOperation.ShowCheckBox = false;
            this.ucEditOperation.Size = new System.Drawing.Size(170, 24);
            this.ucEditOperation.TabIndex = 4;
            this.ucEditOperation.TabNext = true;
            this.ucEditOperation.Value = "";
            this.ucEditOperation.WidthType = UserControl.WidthTypes.Normal;
            this.ucEditOperation.XAlign = 82;
            // 
            // ucEditRunningCard
            // 
            this.ucEditRunningCard.AllowEditOnlyChecked = true;
            this.ucEditRunningCard.Caption = "序列号";
            this.ucEditRunningCard.Checked = false;
            this.ucEditRunningCard.EditType = UserControl.EditTypes.String;
            this.ucEditRunningCard.Location = new System.Drawing.Point(33, 8);
            this.ucEditRunningCard.MaxLength = 40;
            this.ucEditRunningCard.Multiline = false;
            this.ucEditRunningCard.Name = "ucEditRunningCard";
            this.ucEditRunningCard.PasswordChar = '\0';
            this.ucEditRunningCard.ReadOnly = false;
            this.ucEditRunningCard.ShowCheckBox = false;
            this.ucEditRunningCard.Size = new System.Drawing.Size(182, 24);
            this.ucEditRunningCard.TabIndex = 1;
            this.ucEditRunningCard.TabNext = true;
            this.ucEditRunningCard.Value = "";
            this.ucEditRunningCard.WidthType = UserControl.WidthTypes.Normal;
            this.ucEditRunningCard.XAlign = 82;
            // 
            // ucEditMOCode
            // 
            this.ucEditMOCode.AllowEditOnlyChecked = true;
            this.ucEditMOCode.Caption = "工单";
            this.ucEditMOCode.Checked = false;
            this.ucEditMOCode.EditType = UserControl.EditTypes.String;
            this.ucEditMOCode.Location = new System.Drawing.Point(471, 8);
            this.ucEditMOCode.MaxLength = 40;
            this.ucEditMOCode.Multiline = false;
            this.ucEditMOCode.Name = "ucEditMOCode";
            this.ucEditMOCode.PasswordChar = '\0';
            this.ucEditMOCode.ReadOnly = false;
            this.ucEditMOCode.ShowCheckBox = false;
            this.ucEditMOCode.Size = new System.Drawing.Size(170, 24);
            this.ucEditMOCode.TabIndex = 3;
            this.ucEditMOCode.TabNext = true;
            this.ucEditMOCode.Value = "";
            this.ucEditMOCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucEditMOCode.XAlign = 508;
            // 
            // ucButtonExit
            // 
            this.ucButtonExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonExit.BackgroundImage")));
            this.ucButtonExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucButtonExit.Caption = "退出";
            this.ucButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonExit.Location = new System.Drawing.Point(553, 104);
            this.ucButtonExit.Name = "ucButtonExit";
            this.ucButtonExit.Size = new System.Drawing.Size(88, 22);
            this.ucButtonExit.TabIndex = 11;
            // 
            // ucButtonQuery
            // 
            this.ucButtonQuery.BackColor = System.Drawing.SystemColors.Control;
            this.ucButtonQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButtonQuery.BackgroundImage")));
            this.ucButtonQuery.ButtonType = UserControl.ButtonTypes.Query;
            this.ucButtonQuery.Caption = "查询";
            this.ucButtonQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucButtonQuery.Location = new System.Drawing.Point(452, 104);
            this.ucButtonQuery.Name = "ucButtonQuery";
            this.ucButtonQuery.Size = new System.Drawing.Size(88, 22);
            this.ucButtonQuery.TabIndex = 10;
            this.ucButtonQuery.Click += new System.EventHandler(this.ucButtonQuery_Click);
            // 
            // ultraCheckNewStatus
            // 
            this.ultraCheckNewStatus.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraCheckNewStatus.Location = new System.Drawing.Point(561, 72);
            this.ultraCheckNewStatus.Name = "ultraCheckNewStatus";
            this.ultraCheckNewStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ultraCheckNewStatus.Size = new System.Drawing.Size(80, 20);
            this.ultraCheckNewStatus.TabIndex = 9;
            this.ultraCheckNewStatus.Text = "是否送修";
            // 
            // ultraTabControl1
            // 
            this.ultraTabControl1.Controls.Add(this.ultraTabSharedControlsPage1);
            this.ultraTabControl1.Controls.Add(this.ultraTabPageControl1);
            this.ultraTabControl1.Controls.Add(this.ultraTabPageControl2);
            this.ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.ultraTabControl1.Name = "ultraTabControl1";
            this.ultraTabControl1.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.ultraTabControl1.Size = new System.Drawing.Size(664, 544);
            this.ultraTabControl1.TabIndex = 168;
            ultraTab1.TabPage = this.ultraTabPageControl2;
            ultraTab1.Text = "送修";
            ultraTab2.TabPage = this.ultraTabPageControl1;
            ultraTab2.Text = "送修查询";
            this.ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab1,
            ultraTab2});
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(660, 518);
            // 
            // FTSInput
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(664, 544);
            this.Controls.Add(this.ultraTabControl1);
            this.Name = "FTSInput";
            this.Text = "不良品送修";
            this.Activated += new System.EventHandler(this.FTSInput_Activated);
            this.ultraTabPageControl2.ResumeLayout(false);
            this.groupBox.ResumeLayout(false);
            this.ultraTabPageControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridMain)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).EndInit();
            this.ultraTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion


		#region private method
		private void InitForm()
		{
			InitRunningCardInformation();
			InitErrorCodeList();
			//添充datetime框
			InitDateTime();
			//添充grid的列
			InitializeUltraGrid();
		}

		private void InitErrorCodeList()
		{
			this.listBoxErrorCodeList.Items.Clear();
		}

		private void InitRunningCardInformation()
		{
			this.ucLabEditMOCode.Value = string.Empty;
			this.ucLabEditItemCode.Value = string.Empty;
			this.ucLabEditStepsequence.Value = string.Empty;
			this.ucLabEditOperation.Value= string.Empty;
			this.ucLabEditResource.Value = string.Empty;
			this.ucLabEditFindUser.Value = string.Empty;
			this.ucDatetTimeFind.Value = DateTime.Now;
		}

		private void InitDateTime()
		{
			DateTime dt = new DateTime(DateTime.Now.Year
				,DateTime.Now.Month,DateTime.Now.Day,0,0,0,0);
			ucDateTimeStart.Value =dt;
			ucDateTimeEnd.Value = DateTime.Now;
		}

		private void InitializeUltraGrid()
		{
			dtSource.Columns.Clear();
			//for 多语言
			dtSource.Columns.Add("序列号",typeof(string)).ReadOnly = true;
			dtSource.Columns.Add("产品别",typeof(string)).ReadOnly = true;
			dtSource.Columns.Add("产品代码",typeof(string)).ReadOnly = true;
			dtSource.Columns.Add("工单",typeof(string)).ReadOnly = true;
			dtSource.Columns.Add("产线",typeof(string)).ReadOnly = true;
			dtSource.Columns.Add("机台",typeof(string)).ReadOnly = true;
			dtSource.Columns.Add("工位",typeof(string)).ReadOnly = true;
			dtSource.Columns.Add("发现人员",typeof(string)).ReadOnly = true;
			dtSource.Columns.Add("发现时间",typeof(string)).ReadOnly = true;
			dtSource.Columns.Add("接收人员",typeof(string)).ReadOnly = true;
			dtSource.Columns.Add("接收时间",typeof(string)).ReadOnly = true;

			this.ultraGridMain.DataSource = dtSource;

		}
		private void SetRunningCardInformation(string runningCard)
		{
			this.InitRunningCardInformation();
			this.InitErrorCodeList();
			TSFacade tsFacade = new TSFacade(this.DataProvider);
			object obj = tsFacade.GetCardLastTSRecordInNewStatus(runningCard);
			if(obj == null)
			{
				return;
			}
			this.ucLabEditMOCode.Value = ((BenQGuru.eMES.Domain.TS.TS)obj).MOCode;
			this.ucLabEditItemCode.Value = ((BenQGuru.eMES.Domain.TS.TS)obj).ItemCode;
			this.ucLabEditStepsequence.Value = ((BenQGuru.eMES.Domain.TS.TS)obj).FromStepSequenceCode;
			this.ucLabEditOperation.Value= ((BenQGuru.eMES.Domain.TS.TS)obj).FromOPCode;
			this.ucLabEditResource.Value = ((BenQGuru.eMES.Domain.TS.TS)obj).FromResourceCode;
			this.ucLabEditFindUser.Value = ((BenQGuru.eMES.Domain.TS.TS)obj).MaintainUser;
			//Laws Lu,2005/08/23,修改	发现时间、发现人员
			this.ucDatetTimeFind.Value = FormatHelper.ToDateTime(((BenQGuru.eMES.Domain.TS.TS)obj).FromDate,((BenQGuru.eMES.Domain.TS.TS)obj).FormTime);
			//End Laws Lu

			
			if(((BenQGuru.eMES.Domain.TS.TS)obj).TSTimes > 1)
			{
				chkMoreNG.Checked = true;

				object objPreviousTS = tsFacade.GetCardPreviousTSRecord(runningCard,((BenQGuru.eMES.Domain.TS.TS)obj).RunningCardSequence.ToString(),((BenQGuru.eMES.Domain.TS.TS)obj).MOCode);

				if(objPreviousTS != null)
				{
					txtLastTSer.Value = ((BenQGuru.eMES.Domain.TS.TS)objPreviousTS).ConfirmUser;
				}
			}
			//ErrorCodelist
			object[] objs = tsFacade.ExtraQueryTSErrorCode(((BenQGuru.eMES.Domain.TS.TS)obj).TSId);
			if(objs == null)
			{
				return;
			}
			else
			{
				foreach(object objTemp in objs)
				{
					this.listBoxErrorCodeList.Items.Add( ((TSErrorCode)objTemp).ErrorCodeGroup+":"+ ((TSErrorCode)objTemp).ErrorCode);
				}
			}
		}

		

		#endregion 

	

		private void button2_Click(object sender, System.EventArgs e)
		{
			FTSInputEdit f=new FTSInputEdit();
			f.ShowDialog();
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}


		private void ucButtonQuery_Click(object sender, System.EventArgs e)
		{
			//检查开始时间必须小于结束时间
			DateTime startDateTime = this.ucDateTimeStart.Value;
			DateTime endDateTime = this.ucDateTimeEnd.Value;
			Messages messages = new Messages();
			this.dtSource.Rows.Clear();
			messages.AddMessages(CheckValidDateTimeInput(startDateTime,endDateTime));

            //Add By Bernard @ 2010-11-03 根据输入的序列号获取产品的原始序列号
            DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);
            string sourceRCard = dcf.GetSourceCard(ucEditRunningCard.Value.Trim().ToUpper(), string.Empty);

			//Laws Lu,2005/08/25,新增	检查当前资源是否为TS站
			ActionOnLineHelper dataCollect = new ActionOnLineHelper(this._domainDataProvider);
			ActionEventArgs actionEventArgs = new ActionEventArgs(
				ActionType.DataCollectAction_TSConfirm,
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(sourceRCard)),
				ApplicationService.Current().UserCode,
				ApplicationService.Current().ResourceCode);

			messages.AddMessages(dataCollect.CheckResourceInOperationTS(actionEventArgs));
			//End Laws LU
			if(messages.IsSuccess())
			{
				TSFacade tsFacade = new TSFacade(DataProvider);
                FillUltraGridMain(tsFacade.IllegibilityQueryTS(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(sourceRCard)),
					FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucEditItemCode.Value)),
					FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucEditMOCode.Value)),
					FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucEditOperation.Value)),
					FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucEditStepsequence.Value)),
					FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucEditResource.Value)),
					FormatHelper.TODateInt(startDateTime),FormatHelper.TOTimeInt(startDateTime),
					FormatHelper.TODateInt(endDateTime),FormatHelper.TOTimeInt(endDateTime), !ultraCheckNewStatus.Checked));

			}
			else
			{
				ApplicationRun.GetInfoForm().Add(messages);
			}
		}

		private void FillUltraGridMain(object[] objs)
		{
			if(objs == null)
			{
				return;
			}
			else
			{
				foreach(object obj in objs)
				{
					this.dtSource.Rows.Add( new object[] {((BenQGuru.eMES.Domain.TS.TS)obj).RunningCard,
															 ((BenQGuru.eMES.Domain.TS.TS)obj).ModelCode, 
					                                      ((BenQGuru.eMES.Domain.TS.TS)obj).ItemCode,
					((BenQGuru.eMES.Domain.TS.TS)obj).MOCode,
					((BenQGuru.eMES.Domain.TS.TS)obj).FromStepSequenceCode,
					((BenQGuru.eMES.Domain.TS.TS)obj).FromResourceCode,
					((BenQGuru.eMES.Domain.TS.TS)obj).FromOPCode,
			        //发现人员，发现日期
					//Laws Lu,2005/08/23,修改	发现时间、发现人员
					((BenQGuru.eMES.Domain.TS.TS)obj).FromUser ,
				    ((BenQGuru.eMES.Domain.TS.TS)obj).FromDate.ToString() + ((BenQGuru.eMES.Domain.TS.TS)obj).FormTime.ToString() ,
					//End Laws
					((BenQGuru.eMES.Domain.TS.TS)obj).ConfirmUser,
					((BenQGuru.eMES.Domain.TS.TS)obj).ConfirmDate.ToString() + ((BenQGuru.eMES.Domain.TS.TS)obj).ConfirmTime.ToString()});
				}
			}
		}

		private Messages CheckValidDateTimeInput(DateTime startDateTime,DateTime endDateTime)
		{
			Messages messages = new Messages();
			if( startDateTime.CompareTo(endDateTime)>0)
			{
				messages.Add(new UserControl.Message(MessageType.Error,"$CSError_StartDate_LessThan_EndDate"));
			}
			return messages;
		}

		private void ucLabEditRunningCard_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar =='\r')
			{
				Messages messages = new Messages();
				

				if( ucLabEditRunningCard.Value.Trim().Length ==0)
				{
					messages.Add(new UserControl.Message(MessageType.Error,"$CSError_RCard_CanNot_Empty")); 

					ApplicationRun.GetInfoForm().Add(messages);

					ucLabEditRunningCard.TextFocus(false, true);
					return;
				}

                //add by hiro.chen 08/11/18 判断是否下地
                DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);

                string sourceRCard = dataCollectFacade.GetSourceCard(this.ucLabEditRunningCard.Value.Trim().ToUpper(), string.Empty);

                messages.AddMessages(dataCollectFacade.CheckISDown(sourceRCard));
                if (!messages.IsSuccess())
                {
                    ApplicationRun.GetInfoForm().Add(messages);
                    this.ucLabEditRunningCard.TextFocus(false, true);
                    return;
                }
                //end 

				chkMoreNG.Checked = false;
				txtLastTSer.Value = String.Empty;

				ActionFactory actionFactory = new ActionFactory(this.DataProvider);

				IAction actionTSCofirm = actionFactory.CreateAction(ActionType.DataCollectAction_TSConfirm);

				//Laws Lu,2005/11/22,新增	添加代理录入人员
				//modified by jessie lee, 2005/11/24
				string userCode = ApplicationService.Current().UserCode;
				if(txtAgentUser.Checked == true && txtAgentUser.Value.Trim().Length == 0)
				{
					messages.Add(new UserControl.Message(MessageType.Error,"$CS_Error_Vicegerent_CanNot_Empty"));
					ApplicationRun.GetInfoForm().Add(messages);
					return ;
				}
				else if(txtAgentUser.Checked == true && txtAgentUser.Value != String.Empty)
				{
					if((new BenQGuru.eMES.Security.SecurityFacade(this.DataProvider)).CheckResourceRight(txtAgentUser.Value.Trim().ToUpper()
						,ApplicationService.Current().ResourceCode))
					{
						userCode = txtAgentUser.Value;
					}
					else
					{
						messages.Add(new UserControl.Message(MessageType.Error,"$CS_Error_Vicegerent_Is_Wrong"));
						ApplicationRun.GetInfoForm().Add(messages);
						return ;
					}
				}

				//Laws Lu,2005/10/19,新增	缓解性能问题
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
				DataProvider.BeginTransaction();
				try
				{
					ActionEventArgs actionEventArgs = new ActionEventArgs(
						ActionType.DataCollectAction_TSConfirm,
						FormatHelper.PKCapitalFormat(FormatHelper.CleanString(sourceRCard)),
						userCode,
						ApplicationService.Current().ResourceCode,
						ApplicationService.Current().UserCode);

					SetRunningCardInformation(actionEventArgs.RunningCard);

					messages.AddMessages(actionTSCofirm.Execute(actionEventArgs));

                    #region 拆箱、拆栈板
                    //拆箱、拆栈板
                    if (messages.IsSuccess())
                    {
                        //DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                        Package.PackageFacade packageFacade = new BenQGuru.eMES.Package.PackageFacade(DataProvider);

                        //Remove from carton
                        SimulationReport simulationReport = (SimulationReport)dataCollectFacade.GetLastSimulationReport(actionEventArgs.RunningCard.Trim().ToUpper());
                        if (simulationReport != null && simulationReport.CartonCode.Trim().Length > 0)
                        {
                            messages.AddMessages(dataCollectFacade.RemoveFromCarton(actionEventArgs.RunningCard, actionEventArgs.UserCode));
                        }

                        //Remove from pallet
                        Pallet2RCard pallet2RCard = (Pallet2RCard)packageFacade.GetPallet2RCardByRCard(actionEventArgs.RunningCard.Trim().ToUpper());
                        if (pallet2RCard != null)
                        {
                            messages.AddMessages(dataCollectFacade.RemoveFromPallet(actionEventArgs.RunningCard, actionEventArgs.UserCode,true));
                        }
                    }
                    #endregion

                    if (!messages.IsSuccess())
					{
						this.DataProvider.RollbackTransaction();
						ApplicationRun.GetInfoForm().Add(messages);

						ucLabEditRunningCard.TextFocus(false, true);
					}
					else
					{
						this.DataProvider.CommitTransaction();
						ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success,"$CS_TSConfirm_SUCCESS"));

						ucLabEditRunningCard.TextFocus(false, true);
					}
				}
				catch
				{
					this.DataProvider.RollbackTransaction();
					throw;
				}
				finally
				{
					//Laws Lu,2005/10/19,新增	缓解性能问题
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
				}

				ucLabEditRunningCard.Value = "";
			}
		}

		private void FTSInput_Activated(object sender, System.EventArgs e)
		{
			ucLabEditRunningCard.TextFocus(false, true);
		}
	}
}

