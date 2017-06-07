using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.SMT;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Web.Helper;
using UserControl;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FFeederMaintain 的摘要说明。
	/// </summary>
	public class FFeederMaintain : BaseForm
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton rdoTakeCare;
		private System.Windows.Forms.RadioButton rdoAdjust;
		private System.Windows.Forms.RadioButton rdoAnalyse;
		private UserControl.UCLabelEdit txtFeederNo;
		private UserControl.UCLabelEdit txtReturnReason;
		private System.Windows.Forms.CheckBox chkScrapFlag;
		private UserControl.UCButton ucBtnExit;
		private UserControl.UCButton btnOK;
		private Infragistics.Win.UltraWinGrid.UltraGrid gridList;
		private Infragistics.Win.UltraWinDataSource.UltraDataSource ultraDataSource1;
		private System.Windows.Forms.Label lblAnalyseReason;
		private System.Windows.Forms.Label lblOperationMessage;
		private System.Windows.Forms.ComboBox cboAnalyseReason;
        private System.Windows.Forms.ComboBox cboOperationMessage;
        private IContainer components;

		public FFeederMaintain()
		{
			InitializeComponent();
			this.InitGridColumn();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FFeederMaintain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboOperationMessage = new System.Windows.Forms.ComboBox();
            this.cboAnalyseReason = new System.Windows.Forms.ComboBox();
            this.lblOperationMessage = new System.Windows.Forms.Label();
            this.lblAnalyseReason = new System.Windows.Forms.Label();
            this.ucBtnExit = new UserControl.UCButton();
            this.btnOK = new UserControl.UCButton();
            this.chkScrapFlag = new System.Windows.Forms.CheckBox();
            this.txtReturnReason = new UserControl.UCLabelEdit();
            this.txtFeederNo = new UserControl.UCLabelEdit();
            this.rdoAnalyse = new System.Windows.Forms.RadioButton();
            this.rdoAdjust = new System.Windows.Forms.RadioButton();
            this.rdoTakeCare = new System.Windows.Forms.RadioButton();
            this.gridList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboOperationMessage);
            this.panel1.Controls.Add(this.cboAnalyseReason);
            this.panel1.Controls.Add(this.lblOperationMessage);
            this.panel1.Controls.Add(this.lblAnalyseReason);
            this.panel1.Controls.Add(this.ucBtnExit);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.chkScrapFlag);
            this.panel1.Controls.Add(this.txtReturnReason);
            this.panel1.Controls.Add(this.txtFeederNo);
            this.panel1.Controls.Add(this.rdoAnalyse);
            this.panel1.Controls.Add(this.rdoAdjust);
            this.panel1.Controls.Add(this.rdoTakeCare);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 402);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(821, 183);
            this.panel1.TabIndex = 0;
            // 
            // cboOperationMessage
            // 
            this.cboOperationMessage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOperationMessage.Location = new System.Drawing.Point(96, 111);
            this.cboOperationMessage.Name = "cboOperationMessage";
            this.cboOperationMessage.Size = new System.Drawing.Size(495, 21);
            this.cboOperationMessage.TabIndex = 19;
            this.cboOperationMessage.Leave += new System.EventHandler(this.cboOperationMessage_Leave);
            this.cboOperationMessage.Enter += new System.EventHandler(this.cboOperationMessage_Enter);
            // 
            // cboAnalyseReason
            // 
            this.cboAnalyseReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAnalyseReason.Location = new System.Drawing.Point(96, 80);
            this.cboAnalyseReason.Name = "cboAnalyseReason";
            this.cboAnalyseReason.Size = new System.Drawing.Size(495, 21);
            this.cboAnalyseReason.TabIndex = 18;
            this.cboAnalyseReason.Leave += new System.EventHandler(this.cboAnalyseReason_Leave);
            this.cboAnalyseReason.Enter += new System.EventHandler(this.cboAnalyseReason_Enter);
            // 
            // lblOperationMessage
            // 
            this.lblOperationMessage.AutoSize = true;
            this.lblOperationMessage.Location = new System.Drawing.Point(24, 114);
            this.lblOperationMessage.Name = "lblOperationMessage";
            this.lblOperationMessage.Size = new System.Drawing.Size(55, 13);
            this.lblOperationMessage.TabIndex = 17;
            this.lblOperationMessage.Text = "解决方法";
            // 
            // lblAnalyseReason
            // 
            this.lblAnalyseReason.AutoSize = true;
            this.lblAnalyseReason.Location = new System.Drawing.Point(24, 82);
            this.lblAnalyseReason.Name = "lblAnalyseReason";
            this.lblAnalyseReason.Size = new System.Drawing.Size(55, 13);
            this.lblAnalyseReason.TabIndex = 16;
            this.lblAnalyseReason.Text = "原因分析";
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(376, 152);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 15;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.ButtonType = UserControl.ButtonTypes.None;
            this.btnOK.Caption = "确定";
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Location = new System.Drawing.Point(232, 152);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 22);
            this.btnOK.TabIndex = 14;
            this.btnOK.TabStop = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkScrapFlag
            // 
            this.chkScrapFlag.Location = new System.Drawing.Point(644, 48);
            this.chkScrapFlag.Name = "chkScrapFlag";
            this.chkScrapFlag.Size = new System.Drawing.Size(94, 24);
            this.chkScrapFlag.TabIndex = 8;
            this.chkScrapFlag.Text = "是否报废";
            // 
            // txtReturnReason
            // 
            this.txtReturnReason.AllowEditOnlyChecked = true;
            this.txtReturnReason.Caption = "退回原因";
            this.txtReturnReason.Checked = false;
            this.txtReturnReason.EditType = UserControl.EditTypes.String;
            this.txtReturnReason.Enabled = false;
            this.txtReturnReason.Location = new System.Drawing.Point(330, 48);
            this.txtReturnReason.MaxLength = 40;
            this.txtReturnReason.Multiline = false;
            this.txtReturnReason.Name = "txtReturnReason";
            this.txtReturnReason.PasswordChar = '\0';
            this.txtReturnReason.ReadOnly = false;
            this.txtReturnReason.ShowCheckBox = false;
            this.txtReturnReason.Size = new System.Drawing.Size(261, 26);
            this.txtReturnReason.TabIndex = 5;
            this.txtReturnReason.TabNext = false;
            this.txtReturnReason.Value = "";
            this.txtReturnReason.WidthType = UserControl.WidthTypes.Long;
            this.txtReturnReason.XAlign = 391;
            // 
            // txtFeederNo
            // 
            this.txtFeederNo.AllowEditOnlyChecked = true;
            this.txtFeederNo.Caption = "Feeder编号";
            this.txtFeederNo.Checked = false;
            this.txtFeederNo.EditType = UserControl.EditTypes.String;
            this.txtFeederNo.Location = new System.Drawing.Point(23, 48);
            this.txtFeederNo.MaxLength = 40;
            this.txtFeederNo.Multiline = false;
            this.txtFeederNo.Name = "txtFeederNo";
            this.txtFeederNo.PasswordChar = '\0';
            this.txtFeederNo.ReadOnly = false;
            this.txtFeederNo.ShowCheckBox = false;
            this.txtFeederNo.Size = new System.Drawing.Size(273, 26);
            this.txtFeederNo.TabIndex = 4;
            this.txtFeederNo.TabNext = false;
            this.txtFeederNo.Value = "";
            this.txtFeederNo.WidthType = UserControl.WidthTypes.Long;
            this.txtFeederNo.XAlign = 96;
            this.txtFeederNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFeederNo_TxtboxKeyPress);
            // 
            // rdoAnalyse
            // 
            this.rdoAnalyse.Location = new System.Drawing.Point(170, 16);
            this.rdoAnalyse.Name = "rdoAnalyse";
            this.rdoAnalyse.Size = new System.Drawing.Size(59, 24);
            this.rdoAnalyse.TabIndex = 2;
            this.rdoAnalyse.Text = "分析";
            this.rdoAnalyse.CheckedChanged += new System.EventHandler(this.rdoTakeCare_CheckedChanged);
            // 
            // rdoAdjust
            // 
            this.rdoAdjust.Location = new System.Drawing.Point(96, 16);
            this.rdoAdjust.Name = "rdoAdjust";
            this.rdoAdjust.Size = new System.Drawing.Size(67, 24);
            this.rdoAdjust.TabIndex = 1;
            this.rdoAdjust.Text = "校正";
            this.rdoAdjust.CheckedChanged += new System.EventHandler(this.rdoTakeCare_CheckedChanged);
            // 
            // rdoTakeCare
            // 
            this.rdoTakeCare.Checked = true;
            this.rdoTakeCare.Location = new System.Drawing.Point(24, 16);
            this.rdoTakeCare.Name = "rdoTakeCare";
            this.rdoTakeCare.Size = new System.Drawing.Size(65, 24);
            this.rdoTakeCare.TabIndex = 0;
            this.rdoTakeCare.TabStop = true;
            this.rdoTakeCare.Text = "保养";
            this.rdoTakeCare.CheckedChanged += new System.EventHandler(this.rdoTakeCare_CheckedChanged);
            // 
            // gridList
            // 
            this.gridList.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridList.DataSource = this.ultraDataSource1;
            this.gridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridList.Location = new System.Drawing.Point(0, 10);
            this.gridList.Name = "gridList";
            this.gridList.Size = new System.Drawing.Size(821, 392);
            this.gridList.TabIndex = 3;
            // 
            // FFeederMaintain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(821, 585);
            this.Controls.Add(this.gridList);
            this.Controls.Add(this.panel1);
            this.Name = "FFeederMaintain";
            this.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.Text = "Feeder维护";
            this.Load += new System.EventHandler(this.FFeederMaintain_Load);
            this.Closed += new System.EventHandler(this.FFeederMaintain_Closed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void InitGridColumn()
		{
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FeederCode");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaxCount");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldStatus");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaintainType");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReturnReason");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AnalyseReason");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OperationMessage");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaintainUser");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaintainDate");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaintainTime");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn1 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("FeederCode");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn2 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("MaxCount");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn3 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("OldStatus");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn4 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("MaintainType");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn5 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("ReturnReason");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn6 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("AnalyseReason");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn7 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("OperationMessage");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn8 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Status");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn9 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("MaintainUser");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn10 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("MaintainDate");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn11 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("MaintainTime");
			ultraGridColumn1.AutoEdit = false;
			ultraGridColumn1.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
			ultraGridColumn1.Header.Caption = "Feeder编号";
			ultraGridColumn2.AutoEdit = false;
			ultraGridColumn2.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
			ultraGridColumn2.Header.Caption = "最大使用次数";
			ultraGridColumn2.Width = 88;
			ultraGridColumn3.AutoEdit = false;
			ultraGridColumn3.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
			ultraGridColumn3.Header.Caption = "原状态";
			ultraGridColumn3.Width = 80;
			ultraGridColumn4.AutoEdit = false;
			ultraGridColumn4.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
			ultraGridColumn4.Header.Caption = "维护类型";
			ultraGridColumn4.Width = 77;
			ultraGridColumn5.AutoEdit = false;
			ultraGridColumn5.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
			ultraGridColumn5.Header.Caption = "退回原因";
			ultraGridColumn6.AutoEdit = false;
			ultraGridColumn6.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
			ultraGridColumn6.Header.Caption = "原因分析";
			ultraGridColumn7.AutoEdit = false;
			ultraGridColumn7.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
			ultraGridColumn7.Header.Caption = "处理方法";
			ultraGridColumn8.AutoEdit = false;
			ultraGridColumn8.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
			ultraGridColumn8.Header.Caption = "最新状态";
			ultraGridColumn8.Width = 92;
			ultraGridColumn9.AutoEdit = false;
			ultraGridColumn9.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
			ultraGridColumn9.Header.Caption = "维护人员";
			ultraGridColumn9.Width = 90;
			ultraGridColumn10.AutoEdit = false;
			ultraGridColumn10.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
			ultraGridColumn10.Header.Caption = "维护日期";
			ultraGridColumn10.Width = 81;
			ultraGridColumn11.AutoEdit = false;
			ultraGridColumn11.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
			ultraGridColumn11.Header.Caption = "维护时间";
			ultraGridColumn11.Width = 81;
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
			this.gridList.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
			// 
			// ultraDataSource1
			// 
			this.ultraDataSource1.Band.Columns.AddRange(new object[] {
																		 ultraDataColumn1,
																		 ultraDataColumn2,
																		 ultraDataColumn3,
																		 ultraDataColumn4,
																		 ultraDataColumn5,
																		 ultraDataColumn6,
																		 ultraDataColumn7,
																		 ultraDataColumn8,
																		 ultraDataColumn9,
																		 ultraDataColumn10,
																		 ultraDataColumn11});
		}

		private string checkedAnalyseReason = string.Empty;
		private string checkedOperationMessage = string.Empty;
		private SMTFacade smtFacade = null;
		private IDomainDataProvider _domainDataProvider = Service.ApplicationService.Current().DataProvider;
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		private void CloseConnection()
		{
			if (this.DataProvider!=null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection(); 
		}
		
		private DataTable tableSource = null;
		private Hashtable htStatus = null;
		private object[] analyseReasonList = null;
		private object[] operationMessageList = null;
		private void FFeederMaintain_Load(object sender, System.EventArgs e)
		{
			if (tableSource == null)
			{
				tableSource = new DataTable();
				tableSource.Columns.Add("FeederCode");
				tableSource.Columns.Add("MaxCount", typeof(int));
				tableSource.Columns.Add("UsedCount", typeof(int));
				tableSource.Columns.Add("OldStatus");
				tableSource.Columns.Add("MaintainType");
				tableSource.Columns.Add("ReturnReason");
				tableSource.Columns.Add("AnalyseReason");
				tableSource.Columns.Add("OperationMessage");
				tableSource.Columns.Add("Status");
				tableSource.Columns.Add("MaintainUser");
				tableSource.Columns.Add("MaintainDate");
				tableSource.Columns.Add("MaintainTime");
			}
			gridList.DataSource = tableSource;
			gridList.DisplayLayout.Bands[0].Columns["UsedCount"].Hidden = true;
			SetControlEnabled();
			this.txtFeederNo.TextFocus(false, true);
			smtFacade = new SMTFacade(this.DataProvider);
			
			UserControl.UIStyleBuilder.FormUI(this);  
			UserControl.UIStyleBuilder.GridUI(gridList);
			gridList.DisplayLayout.Bands[0].Columns["MaxCount"].Format = "#,#";
			gridList.DisplayLayout.Bands[0].Columns["MaxCount"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
			gridList.DisplayLayout.Bands[0].Columns["UsedCount"].Format = "#,#";
			gridList.DisplayLayout.Bands[0].Columns["UsedCount"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

			BenQGuru.eMES.BaseSetting.SystemSettingFacade sysFacade = new BenQGuru.eMES.BaseSetting.SystemSettingFacade(this.DataProvider);
			analyseReasonList = sysFacade.GetParametersByParameterGroup("FEEDERANALYSEREASON");
			if (analyseReasonList != null)
			{
				for (int i = 0; i < analyseReasonList.Length; i++)
				{
					BenQGuru.eMES.Domain.BaseSetting.Parameter parameter = (BenQGuru.eMES.Domain.BaseSetting.Parameter)analyseReasonList[i];
					this.cboAnalyseReason.Items.Add(parameter.ParameterAlias);
				}
			}
			operationMessageList = sysFacade.GetParametersByParameterGroup("FEEDERSOLUTION");
			if (operationMessageList != null)
			{
				for (int i = 0; i < operationMessageList.Length; i++)
				{
					BenQGuru.eMES.Domain.BaseSetting.Parameter parameter = (BenQGuru.eMES.Domain.BaseSetting.Parameter)operationMessageList[i];
					this.cboOperationMessage.Items.Add(parameter.ParameterAlias);
				}
			}
            //this.InitPageLanguage();
            //this.InitGridLanguage(gridList);
		}

		private void FFeederMaintain_Closed(object sender, System.EventArgs e)
		{
			this.CloseConnection();
		}

		private void SetControlEnabled()
		{
			if (this.rdoAdjust.Checked == true || this.rdoAnalyse.Checked == true)
			{
				this.cboAnalyseReason.Enabled = true;
				this.cboOperationMessage.Enabled = true;
				this.chkScrapFlag.Enabled = true;
			}
			else
			{
				this.cboAnalyseReason.Enabled = false;
				this.cboOperationMessage.Enabled = false;
				this.chkScrapFlag.Enabled = false;
			}
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			//Icyer,2007/01/15 修改	减少Open/Close的次数
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			try
			{
				if (this.rdoTakeCare.Checked == true)
					this.FeederMaintain();
				else if (this.rdoAdjust.Checked == true)
					this.FeederAdjust();
				else if (this.rdoAnalyse.Checked == true)
					this.FeederAnalyse();
			}
			catch {}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
			}
		}

		private void rdoTakeCare_CheckedChanged(object sender, System.EventArgs e)
		{
			SetControlEnabled();
			this.txtFeederNo.TextFocus(false, true);
		}

		private void FeederMaintain()
		{
			this.txtFeederNo.Value = this.txtFeederNo.Value.Trim().ToUpper();
			if (this.txtFeederNo.Value == string.Empty)
			{
				ApplicationRun.GetInfoForm().Add("$Please_Input_FeederCode");
				return;
			}
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.BeginTransaction();
			try
			{
				Messages messages = smtFacade.FeederMaintain(txtFeederNo.Value, Service.ApplicationService.Current().UserCode, this.tableSource);
				if (messages.IsSuccess())
				{
					ConvertStatusText();
					messages.Add(new UserControl.Message(MessageType.Success, "$Feeder_Maintain_Success"));
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CommitTransaction();
				}
				else
				{
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
				}
				ApplicationRun.GetInfoForm().Add(messages);
			}
			catch (Exception e)
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(e));
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
			}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
			}
			this.txtFeederNo.Value = string.Empty;
			this.txtFeederNo.TextFocus(false, true);
		}

		private void FeederAdjust()
		{
			this.txtFeederNo.Value = this.txtFeederNo.Value.Trim().ToUpper();
			if (this.txtFeederNo.Value == string.Empty)
			{
				ApplicationRun.GetInfoForm().Add("$Please_Input_FeederCode");
				return;
			}
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.BeginTransaction();
			try
			{
				string strAnalyseReason = string.Empty;
				if (cboAnalyseReason.SelectedIndex >= 0)
				{
					strAnalyseReason = ((BenQGuru.eMES.Domain.BaseSetting.Parameter)this.analyseReasonList[cboAnalyseReason.SelectedIndex]).ParameterCode;
				}
				string strOperationMessage = string.Empty;
				if (cboOperationMessage.SelectedIndex >= 0)
				{
					strOperationMessage = ((BenQGuru.eMES.Domain.BaseSetting.Parameter)this.operationMessageList[cboOperationMessage.SelectedIndex]).ParameterCode;
				}
				Messages messages = smtFacade.FeederAdjust(txtFeederNo.Value, strAnalyseReason, strOperationMessage, this.chkScrapFlag.Checked, Service.ApplicationService.Current().UserCode, this.tableSource);
				if (messages.IsSuccess())
				{
					ConvertStatusText();
					messages.Add(new UserControl.Message(MessageType.Success, "$Feeder_Adjust_Success"));
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CommitTransaction();
				}
				else
				{
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
				}
				ApplicationRun.GetInfoForm().Add(messages);
			}
			catch (Exception e)
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(e));
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
			}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
			}
			this.txtFeederNo.Value = string.Empty;
			this.txtReturnReason.Value = string.Empty;
			this.txtFeederNo.TextFocus(false, true);
		}

		private void FeederAnalyse()
		{
			this.txtFeederNo.Value = this.txtFeederNo.Value.Trim().ToUpper();
			if (this.txtFeederNo.Value == string.Empty)
			{
				ApplicationRun.GetInfoForm().Add("$Please_Input_FeederCode");
				txtFeederNo.TextFocus(false, true);
				return;
			}
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.BeginTransaction();
			try
			{
				string strAnalyseReason = string.Empty;
				if (cboAnalyseReason.SelectedIndex >= 0)
				{
					strAnalyseReason = ((BenQGuru.eMES.Domain.BaseSetting.Parameter)this.analyseReasonList[cboAnalyseReason.SelectedIndex]).ParameterCode;
				}
				string strOperationMessage = string.Empty;
				if (cboOperationMessage.SelectedIndex >= 0)
				{
					strOperationMessage = ((BenQGuru.eMES.Domain.BaseSetting.Parameter)this.operationMessageList[cboOperationMessage.SelectedIndex]).ParameterCode;
				}
				Messages messages = smtFacade.FeederAnalyse(txtFeederNo.Value, strAnalyseReason, strOperationMessage, this.chkScrapFlag.Checked, Service.ApplicationService.Current().UserCode, this.tableSource);
				if (messages.IsSuccess())
				{
					ConvertStatusText();
					messages.Add(new UserControl.Message(MessageType.Success, "$Feeder_Analyse_Success"));
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CommitTransaction();
				}
				else
				{
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
				}
				ApplicationRun.GetInfoForm().Add(messages);
			}
			catch (Exception e)
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(e));
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.RollbackTransaction();
			}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
			}
			this.txtFeederNo.Value = string.Empty;
			this.txtReturnReason.Value = string.Empty;
			this.txtFeederNo.TextFocus(false, true);
		}

		private void txtFeederNo_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				if (this.rdoTakeCare.Checked == true)
					this.btnOK_Click(null, null);
				else if (this.rdoAnalyse.Checked == true || this.rdoAdjust.Checked == true)
                    this.cboAnalyseReason.Focus();
			}
		}

		private void ConvertStatusText()
		{
			if (tableSource.Rows.Count == 0)
				return;
			if (htStatus == null)
			{
				htStatus = new Hashtable();
				BenQGuru.eMES.BaseSetting.SystemSettingFacade sysSetting = new BenQGuru.eMES.BaseSetting.SystemSettingFacade(this.DataProvider);
				object[] objs = sysSetting.GetParametersByParameterGroup("FEEDERSTATUS");
				if (objs != null)
				{
					for (int i = 0; i < objs.Length; i++)
					{
						BenQGuru.eMES.Domain.BaseSetting.Parameter param = (BenQGuru.eMES.Domain.BaseSetting.Parameter)objs[i];
						htStatus.Add(param.ParameterCode, param.ParameterAlias);
					}
				}
			}
			DataRow row = tableSource.Rows[tableSource.Rows.Count - 1];
			if (htStatus.ContainsKey(row["Status"].ToString()) == true)
			{
				row["Status"] = htStatus[row["Status"].ToString()];
				row["OldStatus"] = htStatus[row["OldStatus"].ToString()];
			}
			if (this.rdoTakeCare.Checked == true)
				row["MaintainType"] = this.rdoTakeCare.Text;
			else if (this.rdoAdjust.Checked == true)
				row["MaintainType"] = this.rdoAdjust.Text;
			else if (this.rdoAnalyse.Checked == true)
				row["MaintainType"] = this.rdoAnalyse.Text;
			// 原因分析
			if (row["AnalyseReason"].ToString() != string.Empty)
			{
				row["AnalyseReason"] = ((BenQGuru.eMES.Domain.BaseSetting.Parameter)this.analyseReasonList[this.cboAnalyseReason.SelectedIndex]).ParameterAlias;
			}
			if (row["OperationMessage"].ToString() != string.Empty)
			{
				row["OperationMessage"] = ((BenQGuru.eMES.Domain.BaseSetting.Parameter)this.operationMessageList[this.cboOperationMessage.SelectedIndex]).ParameterAlias;
			}
		}

		private void cboAnalyseReason_Enter(object sender, System.EventArgs e)
		{
			cboAnalyseReason.BackColor = Color.GreenYellow;
		}

		private void cboAnalyseReason_Leave(object sender, System.EventArgs e)
		{	
			cboAnalyseReason.BackColor = Color.White;
		}

		private void cboOperationMessage_Enter(object sender, System.EventArgs e)
		{
			cboOperationMessage.BackColor = Color.GreenYellow;
		}

		private void cboOperationMessage_Leave(object sender, System.EventArgs e)
		{
			cboOperationMessage.BackColor = Color.White;
		}

	}
}
