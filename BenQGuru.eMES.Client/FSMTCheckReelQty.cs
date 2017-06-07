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
using BenQGuru.eMES.Domain.MOModel;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FSMTCheckReelQty 的摘要说明。
	/// </summary>
	public class FSMTCheckReelQty : BaseForm
	{
		private System.Windows.Forms.Panel panelTop;
		private UserControl.UCButton btnQuery;
		private UserControl.UCLabelEdit txtSSCodeQuery;
		private UserControl.UCLabelEdit txtMOCodeQuery;
		private UserControl.UCLabelEdit txtItemCodeQuery;
		private System.Windows.Forms.Panel panelBottom;
		private System.Windows.Forms.RadioButton rdoCheckQty;
		private UserControl.UCLabelEdit txtReelNoCheckQty;
		private UserControl.UCLabelEdit txtReelQty;
		private System.Windows.Forms.RadioButton rdoReturn;
		private UserControl.UCLabelEdit txtReelNoReturn;
		private UserControl.UCButton btnSave;
		private UserControl.UCButton btnExit;
		private System.Windows.Forms.Panel panelMiddle;
		private Infragistics.Win.UltraWinGrid.UltraGrid gridList;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FSMTCheckReelQty()
		{
			InitializeComponent();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FSMTCheckReelQty));
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MOCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StepSequenceCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReelNo");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MaterialCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReelLeftQty");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ActualQty");
            this.panelTop = new System.Windows.Forms.Panel();
            this.txtItemCodeQuery = new UserControl.UCLabelEdit();
            this.btnQuery = new UserControl.UCButton();
            this.txtSSCodeQuery = new UserControl.UCLabelEdit();
            this.txtMOCodeQuery = new UserControl.UCLabelEdit();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnExit = new UserControl.UCButton();
            this.btnSave = new UserControl.UCButton();
            this.txtReelNoReturn = new UserControl.UCLabelEdit();
            this.rdoReturn = new System.Windows.Forms.RadioButton();
            this.txtReelQty = new UserControl.UCLabelEdit();
            this.txtReelNoCheckQty = new UserControl.UCLabelEdit();
            this.rdoCheckQty = new System.Windows.Forms.RadioButton();
            this.panelMiddle = new System.Windows.Forms.Panel();
            this.gridList = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelMiddle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.txtItemCodeQuery);
            this.panelTop.Controls.Add(this.btnQuery);
            this.panelTop.Controls.Add(this.txtSSCodeQuery);
            this.panelTop.Controls.Add(this.txtMOCodeQuery);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(844, 45);
            this.panelTop.TabIndex = 0;
            // 
            // txtItemCodeQuery
            // 
            this.txtItemCodeQuery.AllowEditOnlyChecked = true;
            this.txtItemCodeQuery.Caption = "产品代码";
            this.txtItemCodeQuery.Checked = false;
            this.txtItemCodeQuery.EditType = UserControl.EditTypes.String;
            this.txtItemCodeQuery.Location = new System.Drawing.Point(191, 12);
            this.txtItemCodeQuery.MaxLength = 40;
            this.txtItemCodeQuery.Multiline = false;
            this.txtItemCodeQuery.Name = "txtItemCodeQuery";
            this.txtItemCodeQuery.PasswordChar = '\0';
            this.txtItemCodeQuery.ReadOnly = true;
            this.txtItemCodeQuery.ShowCheckBox = false;
            this.txtItemCodeQuery.Size = new System.Drawing.Size(161, 24);
            this.txtItemCodeQuery.TabIndex = 18;
            this.txtItemCodeQuery.TabNext = true;
            this.txtItemCodeQuery.Value = "";
            this.txtItemCodeQuery.WidthType = UserControl.WidthTypes.Small;
            this.txtItemCodeQuery.XAlign = 252;
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.SystemColors.Control;
            this.btnQuery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnQuery.BackgroundImage")));
            this.btnQuery.ButtonType = UserControl.ButtonTypes.None;
            this.btnQuery.Caption = "查询";
            this.btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuery.Location = new System.Drawing.Point(545, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(88, 22);
            this.btnQuery.TabIndex = 17;
            this.btnQuery.TabStop = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtSSCodeQuery
            // 
            this.txtSSCodeQuery.AllowEditOnlyChecked = true;
            this.txtSSCodeQuery.Caption = "产线代码";
            this.txtSSCodeQuery.Checked = false;
            this.txtSSCodeQuery.EditType = UserControl.EditTypes.String;
            this.txtSSCodeQuery.Location = new System.Drawing.Point(368, 12);
            this.txtSSCodeQuery.MaxLength = 40;
            this.txtSSCodeQuery.Multiline = false;
            this.txtSSCodeQuery.Name = "txtSSCodeQuery";
            this.txtSSCodeQuery.PasswordChar = '\0';
            this.txtSSCodeQuery.ReadOnly = false;
            this.txtSSCodeQuery.ShowCheckBox = false;
            this.txtSSCodeQuery.Size = new System.Drawing.Size(161, 24);
            this.txtSSCodeQuery.TabIndex = 16;
            this.txtSSCodeQuery.TabNext = false;
            this.txtSSCodeQuery.Value = "";
            this.txtSSCodeQuery.WidthType = UserControl.WidthTypes.Small;
            this.txtSSCodeQuery.XAlign = 429;
            this.txtSSCodeQuery.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSSCodeQuery_TxtboxKeyPress);
            // 
            // txtMOCodeQuery
            // 
            this.txtMOCodeQuery.AllowEditOnlyChecked = true;
            this.txtMOCodeQuery.Caption = "工单代码";
            this.txtMOCodeQuery.Checked = false;
            this.txtMOCodeQuery.EditType = UserControl.EditTypes.String;
            this.txtMOCodeQuery.Location = new System.Drawing.Point(13, 12);
            this.txtMOCodeQuery.MaxLength = 40;
            this.txtMOCodeQuery.Multiline = false;
            this.txtMOCodeQuery.Name = "txtMOCodeQuery";
            this.txtMOCodeQuery.PasswordChar = '\0';
            this.txtMOCodeQuery.ReadOnly = false;
            this.txtMOCodeQuery.ShowCheckBox = false;
            this.txtMOCodeQuery.Size = new System.Drawing.Size(161, 24);
            this.txtMOCodeQuery.TabIndex = 15;
            this.txtMOCodeQuery.TabNext = false;
            this.txtMOCodeQuery.Value = "";
            this.txtMOCodeQuery.WidthType = UserControl.WidthTypes.Small;
            this.txtMOCodeQuery.XAlign = 74;
            this.txtMOCodeQuery.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMOCodeQuery_TxtboxKeyPress);
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnExit);
            this.panelBottom.Controls.Add(this.btnSave);
            this.panelBottom.Controls.Add(this.txtReelNoReturn);
            this.panelBottom.Controls.Add(this.rdoReturn);
            this.panelBottom.Controls.Add(this.txtReelQty);
            this.panelBottom.Controls.Add(this.txtReelNoCheckQty);
            this.panelBottom.Controls.Add(this.rdoCheckQty);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 469);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(844, 108);
            this.panelBottom.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnExit.Caption = "退出";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(320, 74);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 22);
            this.btnExit.TabIndex = 21;
            this.btnExit.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.ButtonType = UserControl.ButtonTypes.None;
            this.btnSave.Caption = "保存";
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(180, 74);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 22);
            this.btnSave.TabIndex = 20;
            this.btnSave.TabStop = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtReelNoReturn
            // 
            this.txtReelNoReturn.AllowEditOnlyChecked = true;
            this.txtReelNoReturn.Caption = "料卷编号";
            this.txtReelNoReturn.Checked = false;
            this.txtReelNoReturn.EditType = UserControl.EditTypes.String;
            this.txtReelNoReturn.Enabled = false;
            this.txtReelNoReturn.Location = new System.Drawing.Point(66, 44);
            this.txtReelNoReturn.MaxLength = 40;
            this.txtReelNoReturn.Multiline = false;
            this.txtReelNoReturn.Name = "txtReelNoReturn";
            this.txtReelNoReturn.PasswordChar = '\0';
            this.txtReelNoReturn.ReadOnly = false;
            this.txtReelNoReturn.ShowCheckBox = false;
            this.txtReelNoReturn.Size = new System.Drawing.Size(161, 24);
            this.txtReelNoReturn.TabIndex = 19;
            this.txtReelNoReturn.TabNext = true;
            this.txtReelNoReturn.Value = "";
            this.txtReelNoReturn.WidthType = UserControl.WidthTypes.Small;
            this.txtReelNoReturn.XAlign = 127;
            this.txtReelNoReturn.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReelNoReturn_TxtboxKeyPress);
            // 
            // rdoReturn
            // 
            this.rdoReturn.Location = new System.Drawing.Point(13, 44);
            this.rdoReturn.Name = "rdoReturn";
            this.rdoReturn.Size = new System.Drawing.Size(55, 24);
            this.rdoReturn.TabIndex = 18;
            this.rdoReturn.Text = "退料";
            // 
            // txtReelQty
            // 
            this.txtReelQty.AllowEditOnlyChecked = true;
            this.txtReelQty.Caption = "实际数量";
            this.txtReelQty.Checked = false;
            this.txtReelQty.EditType = UserControl.EditTypes.Integer;
            this.txtReelQty.Location = new System.Drawing.Point(247, 14);
            this.txtReelQty.MaxLength = 40;
            this.txtReelQty.Multiline = false;
            this.txtReelQty.Name = "txtReelQty";
            this.txtReelQty.PasswordChar = '\0';
            this.txtReelQty.ReadOnly = false;
            this.txtReelQty.ShowCheckBox = false;
            this.txtReelQty.Size = new System.Drawing.Size(161, 24);
            this.txtReelQty.TabIndex = 17;
            this.txtReelQty.TabNext = true;
            this.txtReelQty.Value = "";
            this.txtReelQty.WidthType = UserControl.WidthTypes.Small;
            this.txtReelQty.XAlign = 308;
            this.txtReelQty.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReelQty_TxtboxKeyPress);
            // 
            // txtReelNoCheckQty
            // 
            this.txtReelNoCheckQty.AllowEditOnlyChecked = true;
            this.txtReelNoCheckQty.Caption = "料卷编号";
            this.txtReelNoCheckQty.Checked = false;
            this.txtReelNoCheckQty.EditType = UserControl.EditTypes.String;
            this.txtReelNoCheckQty.Location = new System.Drawing.Point(66, 14);
            this.txtReelNoCheckQty.MaxLength = 40;
            this.txtReelNoCheckQty.Multiline = false;
            this.txtReelNoCheckQty.Name = "txtReelNoCheckQty";
            this.txtReelNoCheckQty.PasswordChar = '\0';
            this.txtReelNoCheckQty.ReadOnly = false;
            this.txtReelNoCheckQty.ShowCheckBox = false;
            this.txtReelNoCheckQty.Size = new System.Drawing.Size(161, 24);
            this.txtReelNoCheckQty.TabIndex = 16;
            this.txtReelNoCheckQty.TabNext = true;
            this.txtReelNoCheckQty.Value = "";
            this.txtReelNoCheckQty.WidthType = UserControl.WidthTypes.Small;
            this.txtReelNoCheckQty.XAlign = 127;
            // 
            // rdoCheckQty
            // 
            this.rdoCheckQty.Checked = true;
            this.rdoCheckQty.Location = new System.Drawing.Point(13, 14);
            this.rdoCheckQty.Name = "rdoCheckQty";
            this.rdoCheckQty.Size = new System.Drawing.Size(55, 24);
            this.rdoCheckQty.TabIndex = 0;
            this.rdoCheckQty.TabStop = true;
            this.rdoCheckQty.Text = "点料";
            this.rdoCheckQty.CheckedChanged += new System.EventHandler(this.rdoCheckQty_CheckedChanged);
            // 
            // panelMiddle
            // 
            this.panelMiddle.Controls.Add(this.gridList);
            this.panelMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMiddle.Location = new System.Drawing.Point(0, 45);
            this.panelMiddle.Name = "panelMiddle";
            this.panelMiddle.Size = new System.Drawing.Size(844, 424);
            this.panelMiddle.TabIndex = 2;
            // 
            // gridList
            // 
            this.gridList.Cursor = System.Windows.Forms.Cursors.Default;
            ultraGridColumn1.Header.Caption = "工单代码";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 95;
            ultraGridColumn2.Header.Caption = "产线代码";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 76;
            ultraGridColumn3.Header.Caption = "料卷编号";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn4.Header.Caption = "物料代码";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn5.Header.Caption = "剩余数量";
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Width = 78;
            ultraGridColumn6.Header.Caption = "点料数量";
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Width = 81;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6});
            this.gridList.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.gridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridList.Location = new System.Drawing.Point(0, 0);
            this.gridList.Name = "gridList";
            this.gridList.Size = new System.Drawing.Size(844, 424);
            this.gridList.TabIndex = 0;
            // 
            // FSMTCheckReelQty
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(844, 577);
            this.Controls.Add(this.panelMiddle);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "FSMTCheckReelQty";
            this.Text = "料卷点料/退料";
            this.Load += new System.EventHandler(this.FSMTCheckReelQty_Load);
            this.Closed += new System.EventHandler(this.FSMTCheckReelQty_Closed);
            this.panelTop.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelMiddle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

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
		private void FSMTCheckReelQty_Load(object sender, System.EventArgs e)
		{
			smtFacade = new SMTFacade(this.DataProvider);
			tableSource = new DataTable();
			tableSource.Columns.Add("MOCode");
			tableSource.Columns.Add("StepSequenceCode");
			tableSource.Columns.Add("ReelNo");
			tableSource.Columns.Add("MaterialCode");
			tableSource.Columns.Add("ReelLeftQty", typeof(int));
			tableSource.Columns.Add("ActualQty", typeof(int));
			tableSource.Columns.Add("IsChecked");
			
			gridList.DataSource = tableSource;
			gridList.DisplayLayout.Bands[0].Columns["ReelLeftQty"].Format = "#,#";
			gridList.DisplayLayout.Bands[0].Columns["ReelLeftQty"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
			gridList.DisplayLayout.Bands[0].Columns["ActualQty"].Format = "#,#";
			gridList.DisplayLayout.Bands[0].Columns["ActualQty"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
			gridList.DisplayLayout.Bands[0].Columns["IsChecked"].Hidden = true;
			
			UserControl.UIStyleBuilder.FormUI(this);
			UserControl.UIStyleBuilder.GridUI(gridList);

            //this.InitPageLanguage();
            //this.InitGridLanguage(gridList);
		}

		private void FSMTCheckReelQty_Closed(object sender, System.EventArgs e)
		{
			this.CloseConnection();
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			this.txtMOCodeQuery.Value = this.txtMOCodeQuery.Value.Trim().ToUpper();
			this.txtSSCodeQuery.Value = this.txtSSCodeQuery.Value.Trim().ToUpper();
			if (this.txtMOCodeQuery.Value == string.Empty)
			{
				//Application.DoEvents();
				this.txtMOCodeQuery.TextFocus(false, true);
				return;
			}
			//Icyer,2007/01/15 修改	减少Open/Close的次数
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			try
			{
				if (this.checkedMOCode != this.txtMOCodeQuery.Value)
				{
					txtMOCodeQuery_TxtboxKeyPress(null, new KeyPressEventArgs('\r'));
				}
				if (this.txtSSCodeQuery.Value == string.Empty)
				{
					//Application.DoEvents();
					this.txtSSCodeQuery.TextFocus(true, true);
					return;
				}
				// 检查工单是否全部下料
				if (smtFacade.QueryReelQtyCount(string.Empty, this.txtMOCodeQuery.Value) > 0)
				{
					ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$SMT_CheckReel_MO_In_Used"));
					return;
				}
				object[] objs = smtFacade.QueryReelByMO(this.txtMOCodeQuery.Value, this.txtSSCodeQuery.Value, string.Empty, string.Empty);
				tableSource.Rows.Clear();
				if (objs != null)
				{
					for (int i = 0; i < objs.Length; i++)
					{
						Reel reel = (Reel)objs[i];
						DataRow row = tableSource.NewRow();
						row["MOCode"] = reel.MOCode;
						row["StepSequenceCode"] = reel.StepSequenceCode;
						row["ReelNo"] = reel.ReelNo;
						row["MaterialCode"] = reel.PartNo;
						row["ReelLeftQty"] = reel.Qty - reel.UsedQty;
						row["ActualQty"] = 0;
						tableSource.Rows.Add(row);
						gridList.Rows[i].Appearance.ForeColor = Color.Red;
					}
				}
				//Application.DoEvents();
				if (tableSource.Rows.Count > 0)
				{
					this.rdoCheckQty.Checked = true;
					this.txtReelNoCheckQty.TextFocus(false, true);
				}
				else
				{
					this.txtMOCodeQuery.TextFocus(false, true);
				}
			}
			catch {}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
			}
		}

		private string checkedMOCode = string.Empty;
		private void txtMOCodeQuery_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				if (this.checkedMOCode != this.txtMOCodeQuery.Value.Trim().ToUpper())
				{
					this.txtMOCodeQuery.Value = this.txtMOCodeQuery.Value.Trim().ToUpper();
					BenQGuru.eMES.MOModel.MOFacade moFacade = new BenQGuru.eMES.MOModel.MOFacade(this.DataProvider);
					MO mo = (MO)moFacade.GetMO(this.txtMOCodeQuery.Value);
					if (mo == null)
					{
						ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_MO_Not_Exist"));
						//Application.DoEvents();
						this.txtMOCodeQuery.TextFocus(true, true);
						return;
					}
					this.txtItemCodeQuery.Value = mo.ItemCode;
					this.checkedMOCode = this.txtMOCodeQuery.Value;
				}
				//Application.DoEvents();
				this.txtSSCodeQuery.TextFocus(false, true);
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			if (this.txtMOCodeQuery.Value == string.Empty)
			{
				//Application.DoEvents();
				this.txtMOCodeQuery.TextFocus(false, true);
				return;
			}
			if (this.txtSSCodeQuery.Value == string.Empty)
			{
				//Application.DoEvents();
				this.txtSSCodeQuery.TextFocus(true, true);
				return;
			}
			if (this.checkedMOCode != this.txtMOCodeQuery.Value.Trim().ToUpper())
				this.txtMOCodeQuery_TxtboxKeyPress(null, new KeyPressEventArgs('\r'));
			if (tableSource.Rows.Count == 0)
				return;
			this.txtReelNoCheckQty.Value = this.txtReelNoCheckQty.Value.Trim().ToUpper();
			this.txtReelNoReturn.Value = this.txtReelNoReturn.Value.Trim().ToUpper();
			string strReelNo = string.Empty;
			if (this.rdoCheckQty.Checked == true)
			{
				//Application.DoEvents();
				if (this.txtReelNoCheckQty.Value.Trim() == string.Empty)
				{
					txtReelNoCheckQty.TextFocus(false, true);
					return;
				}
				if (this.txtReelQty.Value.Trim() == string.Empty)
				{
					txtReelQty.TextFocus(false, true);
					return;
				}
				strReelNo = this.txtReelNoCheckQty.Value.Trim().ToUpper();
			}
			else if (this.rdoReturn.Checked == true)
			{
				if (this.txtReelNoReturn.Value.Trim() == string.Empty)
				{
					//Application.DoEvents();
					this.txtReelNoReturn.TextFocus(false, true);
					return;
				}
				strReelNo = this.txtReelNoReturn.Value.Trim().ToUpper();
			}
			DataRow[] rowsTmp = tableSource.Select("ReelNo='" + strReelNo + "'");
			if (rowsTmp.Length == 0)
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$SMT_CheckReel_Not_In_List"));
				return;
			}
			if (rowsTmp[0]["IsChecked"].ToString() == "1")
			{
				ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$SMT_CheckReel_Reel_Checked_Already"));
				return;
			}
			// 提交
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			this.DataProvider.BeginTransaction();
			Messages messages = new Messages();
			try
			{
				if (this.rdoCheckQty.Checked == true)
				{
					messages.AddMessages(smtFacade.CheckReel(strReelNo, int.Parse(this.txtReelQty.Value), Service.ApplicationService.Current().UserCode));
				}
				else
				{
					messages.AddMessages(smtFacade.CheckReel(strReelNo, int.MinValue, Service.ApplicationService.Current().UserCode));
				}
				if (messages.IsSuccess() == true)
				{
					this.DataProvider.CommitTransaction();
					if (this.rdoCheckQty.Checked == true)
						messages.Add(new UserControl.Message(MessageType.Success, "$SMT_CheckReel_Success"));
					else
						messages.Add(new UserControl.Message(MessageType.Success, "$SMT_Prepare_Reel_Return_Success"));
				}
				else
				{
					this.DataProvider.RollbackTransaction();
				}
			}
			catch (Exception ex)
			{
				messages.Add(new UserControl.Message(ex));
				this.DataProvider.RollbackTransaction();
			}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
			}
			ApplicationRun.GetInfoForm().Add(messages);
			// 更改Grid
			if (messages.IsSuccess() == true)
			{
				DataRow[] rows = tableSource.Select("ReelNo='" + strReelNo + "'");
				if (this.rdoCheckQty.Checked == true)
					rows[0]["ActualQty"] = this.txtReelQty.Value;
				else
					rows[0]["ActualQty"] = rows[0]["ReelLeftQty"];
				rows[0]["IsChecked"] = "1";
				for (int i = 0; i < tableSource.Rows.Count; i++)
				{
					if (gridList.Rows[i].Cells["ReelNo"].Text == strReelNo)
					{
						gridList.Rows[i].Appearance.ForeColor = Color.Black;
						break;
					}
				}
			}
			//Application.DoEvents();
			if (this.rdoCheckQty.Checked == true)
			{
				this.txtReelQty.Value = string.Empty;
				this.txtReelNoCheckQty.TextFocus(true, true);
			}
			else
			{
				this.txtReelNoReturn.TextFocus(true, true);
			}
		}

		private void txtSSCodeQuery_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.btnQuery_Click(null, null);
			}
		}

		private void txtReelQty_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				if (this.txtReelNoCheckQty.Value.Trim() == string.Empty)
				{
					//Application.DoEvents();
					this.txtReelNoCheckQty.TextFocus(false, true);
					return;
				}
				else
				{
					this.btnSave_Click(null, null);
				}
			}
		}

		private void txtReelNoReturn_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.btnSave_Click(null, null);
			}
		}

		private void rdoCheckQty_CheckedChanged(object sender, System.EventArgs e)
		{
			//Application.DoEvents();
			this.txtReelNoCheckQty.Enabled = false;
			this.txtReelQty.Enabled = false;
			this.txtReelNoReturn.Enabled = false;
			if (this.rdoCheckQty.Checked == true)
			{
				this.txtReelNoCheckQty.Enabled = true;
				this.txtReelQty.Enabled = true;
				this.txtReelNoCheckQty.TextFocus(true, true);
			}
			else
			{
				this.txtReelNoReturn.Enabled = true;
				this.txtReelNoReturn.TextFocus(true, true);
			}
		}
	}
}
