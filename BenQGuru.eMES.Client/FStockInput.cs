using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using UserControl;
using BenQGuru.eMES.OQC;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FStockInput 的摘要说明。
	/// </summary>
	public class FStockInput : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox grpQuery;
		private Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOsType;
		private UserControl.UCLabelEdit ucLEInput;
		private System.Windows.Forms.Panel panel1;
		private UserControl.UCButton ucBtnRemove;
		private UserControl.UCButton ucBtnExit;
		private UserControl.UCButton ucBtnSave;
		private System.Windows.Forms.Panel panel2;
		private UserControl.UCButton ucBtnDelete;
		private UserControl.UCLabelEdit ucLETicketNo;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Data.DataColumn dataColumn8;
		private System.Data.DataColumn dataColumn11;
		private System.Data.DataColumn dataColumn12;
		private System.Data.DataColumn dataColumn1;
		private System.Data.DataSet dsStockIn;
		private System.Data.DataTable dtStockIn;
		private UserControl.UCLabelCombox ucLCModel;
		private UserControl.UCLabelEdit ucLEMOCode;
		private System.Data.DataColumn dataColumn2;

		private DataTable _tmpTable = new DataTable();
		UltraWinGridHelper ultraWinGridHelper;
		private System.Windows.Forms.Panel panel3;
		private UserControl.UCLabelEdit txtSumNum;

		public string DeleteInfor;
		private UserControl.UCLabelEdit txtMEMO;

		private Hashtable sum = new Hashtable();
		private System.Data.DataColumn dataColumn3;
		private System.Data.DataColumn dataColumn4;
		private System.Windows.Forms.Panel panel4;
		private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridContent;
		
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FStockInput()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
			UserControl.UIStyleBuilder.FormUI(this);	
			UserControl.UIStyleBuilder.UltraOptionSetUI(this.ultraOsType);
		
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
		
		private MaterialStockFacade _facade = null;

		private IDomainDataProvider _domainDataProvider =ApplicationService.Current().DataProvider;
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		protected void ShowMessage(string message)
		{
			ApplicationRun.GetInfoForm().Add( message );
		}

		protected void ShowMessage(Messages messages)
		{			
			ApplicationRun.GetInfoForm().Add(messages);
		}

		protected void ShowMessage(UserControl.Message message)
		{			
			ApplicationRun.GetInfoForm().Add(message);
		}


		protected void ShowMessage(Exception e)
		{			
			ApplicationRun.GetInfoForm().Add(new UserControl.Message(e));
		}

		private enum CollectionType
		{
			Planate, PCS
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FStockInput));
            this.grpQuery = new System.Windows.Forms.GroupBox();
            this.txtMEMO = new UserControl.UCLabelEdit();
            this.ucLEMOCode = new UserControl.UCLabelEdit();
            this.ucLCModel = new UserControl.UCLabelCombox();
            this.ucLEInput = new UserControl.UCLabelEdit();
            this.ultraOsType = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucBtnRemove = new UserControl.UCButton();
            this.ucBtnExit = new UserControl.UCButton();
            this.ucBtnSave = new UserControl.UCButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ucBtnDelete = new UserControl.UCButton();
            this.ucLETicketNo = new UserControl.UCLabelEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ultraGridContent = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtSumNum = new UserControl.UCLabelEdit();
            this.dsStockIn = new System.Data.DataSet();
            this.dtStockIn = new System.Data.DataTable();
            this.dataColumn8 = new System.Data.DataColumn();
            this.dataColumn11 = new System.Data.DataColumn();
            this.dataColumn12 = new System.Data.DataColumn();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.grpQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOsType)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridContent)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsStockIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStockIn)).BeginInit();
            this.SuspendLayout();
            // 
            // grpQuery
            // 
            this.grpQuery.Controls.Add(this.txtMEMO);
            this.grpQuery.Controls.Add(this.ucLEMOCode);
            this.grpQuery.Controls.Add(this.ucLCModel);
            this.grpQuery.Controls.Add(this.ucLEInput);
            this.grpQuery.Controls.Add(this.ultraOsType);
            this.grpQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpQuery.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpQuery.Location = new System.Drawing.Point(0, 37);
            this.grpQuery.Name = "grpQuery";
            this.grpQuery.Size = new System.Drawing.Size(552, 149);
            this.grpQuery.TabIndex = 1;
            this.grpQuery.TabStop = false;
            this.grpQuery.Text = "采集方式";
            // 
            // txtMEMO
            // 
            this.txtMEMO.AllowEditOnlyChecked = true;
            this.txtMEMO.Caption = "备注";
            this.txtMEMO.Checked = false;
            this.txtMEMO.EditType = UserControl.EditTypes.String;
            this.txtMEMO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMEMO.Location = new System.Drawing.Point(20, 82);
            this.txtMEMO.MaxLength = 80;
            this.txtMEMO.Multiline = true;
            this.txtMEMO.Name = "txtMEMO";
            this.txtMEMO.PasswordChar = '\0';
            this.txtMEMO.ReadOnly = false;
            this.txtMEMO.ShowCheckBox = false;
            this.txtMEMO.Size = new System.Drawing.Size(197, 56);
            this.txtMEMO.TabIndex = 7;
            this.txtMEMO.TabNext = true;
            this.txtMEMO.Value = "";
            this.txtMEMO.WidthType = UserControl.WidthTypes.Long;
            this.txtMEMO.XAlign = 51;
            // 
            // ucLEMOCode
            // 
            this.ucLEMOCode.AllowEditOnlyChecked = true;
            this.ucLEMOCode.Caption = "工单";
            this.ucLEMOCode.Checked = false;
            this.ucLEMOCode.EditType = UserControl.EditTypes.String;
            this.ucLEMOCode.Location = new System.Drawing.Point(365, 15);
            this.ucLEMOCode.MaxLength = 40;
            this.ucLEMOCode.Multiline = false;
            this.ucLEMOCode.Name = "ucLEMOCode";
            this.ucLEMOCode.PasswordChar = '\0';
            this.ucLEMOCode.ReadOnly = false;
            this.ucLEMOCode.ShowCheckBox = false;
            this.ucLEMOCode.Size = new System.Drawing.Size(142, 22);
            this.ucLEMOCode.TabIndex = 3;
            this.ucLEMOCode.TabNext = false;
            this.ucLEMOCode.Value = "";
            this.ucLEMOCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEMOCode.XAlign = 396;
            // 
            // ucLCModel
            // 
            this.ucLCModel.AllowEditOnlyChecked = true;
            this.ucLCModel.Caption = "产品别";
            this.ucLCModel.Checked = false;
            this.ucLCModel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucLCModel.Location = new System.Drawing.Point(142, 15);
            this.ucLCModel.Name = "ucLCModel";
            this.ucLCModel.SelectedIndex = -1;
            this.ucLCModel.ShowCheckBox = false;
            this.ucLCModel.Size = new System.Drawing.Size(207, 19);
            this.ucLCModel.TabIndex = 2;
            this.ucLCModel.WidthType = UserControl.WidthTypes.Long;
            this.ucLCModel.XAlign = 183;
            // 
            // ucLEInput
            // 
            this.ucLEInput.AllowEditOnlyChecked = true;
            this.ucLEInput.Caption = "输入框";
            this.ucLEInput.Checked = false;
            this.ucLEInput.EditType = UserControl.EditTypes.String;
            this.ucLEInput.Location = new System.Drawing.Point(14, 45);
            this.ucLEInput.MaxLength = 1000;
            this.ucLEInput.Multiline = false;
            this.ucLEInput.Name = "ucLEInput";
            this.ucLEInput.PasswordChar = '\0';
            this.ucLEInput.ReadOnly = false;
            this.ucLEInput.ShowCheckBox = false;
            this.ucLEInput.Size = new System.Drawing.Size(374, 22);
            this.ucLEInput.TabIndex = 1;
            this.ucLEInput.TabNext = false;
            this.ucLEInput.Value = "";
            this.ucLEInput.WidthType = UserControl.WidthTypes.TooLong;
            this.ucLEInput.XAlign = 55;
            this.ucLEInput.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLEInput_TxtboxKeyPress);
            // 
            // ultraOsType
            // 
            this.ultraOsType.BackColor = System.Drawing.SystemColors.Control;
            this.ultraOsType.BackColorInternal = System.Drawing.SystemColors.Control;
            this.ultraOsType.ImageTransparentColor = System.Drawing.Color.Gainsboro;
            valueListItem1.DisplayText = "二维条码     ";
            valueListItem2.DisplayText = "PCS";
            this.ultraOsType.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem1,
            valueListItem2});
            this.ultraOsType.Location = new System.Drawing.Point(7, 15);
            this.ultraOsType.Name = "ultraOsType";
            this.ultraOsType.Size = new System.Drawing.Size(129, 15);
            this.ultraOsType.TabIndex = 0;
            this.ultraOsType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ucBtnRemove);
            this.panel1.Controls.Add(this.ucBtnExit);
            this.panel1.Controls.Add(this.ucBtnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 449);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(552, 37);
            this.panel1.TabIndex = 3;
            // 
            // ucBtnRemove
            // 
            this.ucBtnRemove.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnRemove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnRemove.BackgroundImage")));
            this.ucBtnRemove.ButtonType = UserControl.ButtonTypes.Move;
            this.ucBtnRemove.Caption = "移除";
            this.ucBtnRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnRemove.Location = new System.Drawing.Point(87, 8);
            this.ucBtnRemove.Name = "ucBtnRemove";
            this.ucBtnRemove.Size = new System.Drawing.Size(88, 22);
            this.ucBtnRemove.TabIndex = 0;
            this.ucBtnRemove.Click += new System.EventHandler(this.ucBtnRemove_Click);
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(300, 8);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 2;
            // 
            // ucBtnSave
            // 
            this.ucBtnSave.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnSave.BackgroundImage")));
            this.ucBtnSave.ButtonType = UserControl.ButtonTypes.Save;
            this.ucBtnSave.Caption = "保存";
            this.ucBtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnSave.Location = new System.Drawing.Point(193, 8);
            this.ucBtnSave.Name = "ucBtnSave";
            this.ucBtnSave.Size = new System.Drawing.Size(88, 22);
            this.ucBtnSave.TabIndex = 1;
            this.ucBtnSave.Click += new System.EventHandler(this.ucBtnSave_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ucBtnDelete);
            this.panel2.Controls.Add(this.ucLETicketNo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(552, 37);
            this.panel2.TabIndex = 0;
            // 
            // ucBtnDelete
            // 
            this.ucBtnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnDelete.BackgroundImage")));
            this.ucBtnDelete.ButtonType = UserControl.ButtonTypes.None;
            this.ucBtnDelete.Caption = "删除入库单";
            this.ucBtnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnDelete.Location = new System.Drawing.Point(300, 7);
            this.ucBtnDelete.Name = "ucBtnDelete";
            this.ucBtnDelete.Size = new System.Drawing.Size(88, 22);
            this.ucBtnDelete.TabIndex = 1;
            this.ucBtnDelete.Click += new System.EventHandler(this.ucBtnDelete_Click);
            // 
            // ucLETicketNo
            // 
            this.ucLETicketNo.AllowEditOnlyChecked = true;
            this.ucLETicketNo.Caption = "入库单号";
            this.ucLETicketNo.Checked = false;
            this.ucLETicketNo.EditType = UserControl.EditTypes.String;
            this.ucLETicketNo.Location = new System.Drawing.Point(14, 6);
            this.ucLETicketNo.MaxLength = 40;
            this.ucLETicketNo.Multiline = false;
            this.ucLETicketNo.Name = "ucLETicketNo";
            this.ucLETicketNo.PasswordChar = '\0';
            this.ucLETicketNo.ReadOnly = false;
            this.ucLETicketNo.ShowCheckBox = false;
            this.ucLETicketNo.Size = new System.Drawing.Size(218, 23);
            this.ucLETicketNo.TabIndex = 0;
            this.ucLETicketNo.TabNext = false;
            this.ucLETicketNo.Value = "";
            this.ucLETicketNo.WidthType = UserControl.WidthTypes.Long;
            this.ucLETicketNo.XAlign = 65;
            this.ucLETicketNo.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLETicketNo_TxtboxKeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel4);
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 186);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(552, 263);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "入库明细";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ultraGridContent);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 17);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(546, 205);
            this.panel4.TabIndex = 5;
            // 
            // ultraGridContent
            // 
            this.ultraGridContent.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridContent.Location = new System.Drawing.Point(0, 0);
            this.ultraGridContent.Name = "ultraGridContent";
            this.ultraGridContent.Size = new System.Drawing.Size(546, 205);
            this.ultraGridContent.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtSumNum);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 222);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(546, 38);
            this.panel3.TabIndex = 4;
            // 
            // txtSumNum
            // 
            this.txtSumNum.AllowEditOnlyChecked = true;
            this.txtSumNum.Caption = "数量总计";
            this.txtSumNum.Checked = false;
            this.txtSumNum.EditType = UserControl.EditTypes.String;
            this.txtSumNum.Location = new System.Drawing.Point(314, 7);
            this.txtSumNum.MaxLength = 40;
            this.txtSumNum.Multiline = false;
            this.txtSumNum.Name = "txtSumNum";
            this.txtSumNum.PasswordChar = '\0';
            this.txtSumNum.ReadOnly = true;
            this.txtSumNum.ShowCheckBox = false;
            this.txtSumNum.Size = new System.Drawing.Size(134, 23);
            this.txtSumNum.TabIndex = 1;
            this.txtSumNum.TabNext = false;
            this.txtSumNum.Value = "0";
            this.txtSumNum.WidthType = UserControl.WidthTypes.Small;
            this.txtSumNum.XAlign = 365;
            // 
            // dsStockIn
            // 
            this.dsStockIn.DataSetName = "dsStockIn";
            this.dsStockIn.Locale = new System.Globalization.CultureInfo("zh-CN");
            this.dsStockIn.Tables.AddRange(new System.Data.DataTable[] {
            this.dtStockIn});
            // 
            // dtStockIn
            // 
            this.dtStockIn.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn8,
            this.dataColumn11,
            this.dataColumn12,
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4});
            this.dtStockIn.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.UniqueConstraint("Constraint1", new string[] {
                        "产品序列号"}, true)});
            this.dtStockIn.PrimaryKey = new System.Data.DataColumn[] {
        this.dataColumn1};
            this.dtStockIn.TableName = "dtStockIn";
            // 
            // dataColumn8
            // 
            this.dataColumn8.Caption = "产品别";
            this.dataColumn8.ColumnName = "产品别";
            // 
            // dataColumn11
            // 
            this.dataColumn11.ColumnName = "数量";
            this.dataColumn11.DataType = typeof(short);
            // 
            // dataColumn12
            // 
            this.dataColumn12.AllowDBNull = false;
            this.dataColumn12.Caption = "工单";
            this.dataColumn12.ColumnName = "工单";
            // 
            // dataColumn1
            // 
            this.dataColumn1.AllowDBNull = false;
            this.dataColumn1.Caption = "产品序列号";
            this.dataColumn1.ColumnName = "产品序列号";
            // 
            // dataColumn2
            // 
            this.dataColumn2.Caption = "CollectType";
            this.dataColumn2.ColumnName = "CollectType";
            // 
            // dataColumn3
            // 
            this.dataColumn3.Caption = "MODEL";
            this.dataColumn3.ColumnName = "MODEL";
            // 
            // dataColumn4
            // 
            this.dataColumn4.Caption = "MO";
            this.dataColumn4.ColumnName = "MO";
            // 
            // FStockInput
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(552, 486);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grpQuery);
            this.Controls.Add(this.panel2);
            this.Name = "FStockInput";
            this.Text = "入库采集";
            this.Load += new System.EventHandler(this.FStockInput_Load);
            this.Closed += new System.EventHandler(this.FStockInput_Closed);
            this.grpQuery.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraOsType)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridContent)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dsStockIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStockIn)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void FStockInput_Load(object sender, System.EventArgs e)
		{
			this._facade = new MaterialStockFacade( this.DataProvider );
			UserControl.UIStyleBuilder.FormUI(this);
			UserControl.UIStyleBuilder.GridUI(this.ultraGridContent);

			InitializeTempDatable();

			DataView dv = _tmpTable.DefaultView;

			dv.Sort = "MODEL,MO";

			ultraGridContent.DataSource = dv;

			ultraGridContent.DisplayLayout.Bands[0].Columns["MODEL"].Hidden = true;
			ultraGridContent.DisplayLayout.Bands[0].Columns["MO"].Hidden = true;

			#region 检验对象的类型
			//默认选择为二维条码
			ultraOsType.Items.Clear();
			ultraOsType.CheckedItem =  ultraOsType.Items.Add(OQCFacade.OQC_ExameObject_PlanarCode,"二维条码");
			//this.ultraOptionSetOQCExameOpion.Items.Add(OQCFacade.OQC_ExameObject_Carton,"Carton");
			ultraOsType.Items.Add(OQCFacade.OQC_ExameObject_PCS,"PCS");
			#endregion

			#region Laws Lu,2005/08/27,新增 初始化产品别
			this.ucLCModel.Clear();
			this.ucLCModel.AddItem("", "");

			object[] objs = new ModelFacade( this.DataProvider ).GetAllModels();

			if ( objs == null )
			{
				return;
			}

			foreach (BenQGuru.eMES.Domain.MOModel.Model model in objs )
			{
				this.ucLCModel.AddItem( model.ModelCode, model.ModelCode );
			}

			this.ucLCModel.SelectedIndex = 0;
			#endregion

			this.ucLETicketNo.TextFocus(false, true);
		}

		private void ucLETicketNo_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( e.KeyChar == '\r' )
			{
				if ( this._facade.IsStockInTicketExist(this.ucLETicketNo.Value.Trim().ToUpper()) )
				{
					this.ShowMessage(new UserControl.Message(MessageType.Error,"$Error_CS_Stock_In_Ticket_Exist"));	// 入库单号已存在
					this.ucLETicketNo.TextFocus(false, true);
				}
				else
				{
					this.ucLEInput.TextFocus(false, true);
				}
			}
		}

		private void ucLEInput_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( e.KeyChar == '\r' )
			{	
				if ( this.ucLETicketNo.Value.Trim() == string.Empty )
				{
					this.ShowMessage(new UserControl.Message(MessageType.Error,"$Error_CS_Input_StockIn_TicketNo"));

					this.ucLETicketNo.TextFocus(false, true);

					return;
				}	

				if ( this.ucLEInput.Value.Trim() == string.Empty )
				{
					return;
				}

				try
				{		
					Messages msg = new Messages();
					//二维条码
					if ( this.ultraOsType.CheckedIndex == (int)CollectionType.Planate )
					{
						msg.AddMessages(CollectPlanate());
					}
					//PCS
					if ( this.ultraOsType.CheckedIndex == (int)CollectionType.PCS )
					{
						msg.AddMessages(CollectPCS());
					}

					ultraGridContent.DisplayLayout.Bands[0].Columns["MODEL"].Hidden = true;
					ultraGridContent.DisplayLayout.Bands[0].Columns["MO"].Hidden = true;

					this.ShowMessage(msg);
					ucLEInput.TextFocus(false, true);
				}
				catch( Exception ex )
				{
					this.ShowMessage( ex.Message );
					this.ucLEInput.TextFocus(false, true);

					return;
				}
				finally
				{
					this.ucLEInput.Value = "";
				}
			}
		}

		private void ucBtnDelete_Click(object sender, System.EventArgs e)
		{
			#region 删除入库单
			// 入库单号未输			
			if ( this.ucLETicketNo.Value.Trim() == string.Empty )
			{
				this.ShowMessage(new UserControl.Message(MessageType.Error,"$Error_CS_Input_StockIn_TicketNo"));
				this.ucLETicketNo.TextFocus(false, true);
				return;
			}	

			
			if ( !this._facade.IsStockInTicketIncludeDeletedExist(this.ucLETicketNo.Value.Trim().ToUpper()) )
			{
				this.ShowMessage(new UserControl.Message(MessageType.Error,"$Error_CS_Stock_In_Ticket_Not_Exist"));	// 入库单号不存在
				this.ucLETicketNo.TextFocus(false, true);
				return;
			}

			FStockDelInfo confirm = new FStockDelInfo();
			// 确认删除入库单
			if (confirm.ShowDialog(this) == DialogResult.OK )
			{
			
				this._domainDataProvider.BeginTransaction();
				try
				{
					object[] objStockIns = this._facade.QueryMaterialStockIn(this.ucLETicketNo.Value.Trim());

					bool result = false;

					//Laws Lu,2005/09/05,	修改入库单状态为已删除
					if(objStockIns != null && objStockIns.Length > 0)
					{
						for(int i = 0 ;i < objStockIns.Length ; i++)
						{
							MaterialStockIn mso = (MaterialStockIn)objStockIns[i];
							if(mso.Status != StockStatus.Deleted)
							{
								mso.DelUser = ApplicationService.Current().UserCode;
								mso.DelDate = BenQGuru.eMES.Web.Helper.FormatHelper.TODateInt(DateTime.Now);
								mso.DelTime = BenQGuru.eMES.Web.Helper.FormatHelper.TOTimeInt(DateTime.Now);
								mso.Status = BenQGuru.eMES.Material.StockStatus.Deleted;
								mso.DelMemo = this.DeleteInfor;

								this._facade.UpdateMaterialStockIn(mso);

								result = true;
							}
						}
					}

					this._domainDataProvider.CommitTransaction();

					//入库和出货部分，删除出货单和删除入库单时，需要检测出货单或入库单是否已经被删除，如果已经处于删除状态则直接提示用户，出货单××或入库单××已经删除
					if(!result)
					{
						this.ShowMessage(new UserControl.Message(MessageType.Error,"$Error_CS_Stock_In_Ticket_Has_Been_Deleted"));	
						this.ucLETicketNo.TextFocus(false, true);
						return;
					}
				
					// 删除入库单
					//this._facade.UpdateMaterialStockIn( this.ucLETicketNo.Value.Trim() );
				}
				catch(Exception ex)
				{
					this._domainDataProvider.RollbackTransaction();
					this.ShowMessage( ex);
					this.ucLETicketNo.TextFocus(false, true);
					return;
				}
				// 删除成功
				this.ShowMessage(new UserControl.Message(MessageType.Success,"$CS_Delete_Success"));		// 删除成功
			}
			
			this.ucLETicketNo.Value = string.Empty;
			#endregion
		}

		private void ucBtnRemove_Click(object sender, System.EventArgs e)
		{
			try
			{	
				Messages msg = new Messages();
				//二维条码
				if ( this.ultraOsType.CheckedIndex == (int)CollectionType.Planate )
				{
					msg.AddMessages(RemovePlanate());
				}
				//PCS
				//Laws Lu,2005/09/07,修改	Check采集类型和列表中的采集类型			
				if ( this.ultraOsType.CheckedIndex == (int)CollectionType.PCS )
				{
					if( ultraGridContent.Selected.Rows.Count > 0)
					{
						Infragistics.Win.UltraWinGrid.UltraGridRow ultraDR = ultraGridContent.Selected.Rows[0];
				
						DataRow[] drs = dtStockIn.Select("产品序列号='" +ultraDR.Cells["产品序列号"].Text.ToUpper().Trim()+"'");
						if(drs.Length > 0)
						{
							if(drs[0]["CollectType"].ToString().ToUpper().Trim() != StockCollectionType.PCS)
							{
								msg.Add(new UserControl.Message(MessageType.Error,"$CS_COLLECTTYPE_NOT_MATCH"));
							}
						}
					
						if(msg.IsSuccess())
						{
							msg.AddMessages(RemovePCS());
						}
					}
					else
					{
						msg.Add(new UserControl.Message(MessageType.Error,"$Error_CS_Select_ID_To_Delete"));
					}
				}

				if(!msg.IsSuccess())
				{
					this.ShowMessage(msg);
				}
			}			
			catch( Exception ex )
			{
				this.ShowMessage( ex );
			}
		}

		private void ucBtnSave_Click(object sender, System.EventArgs e)
		{		
			#region 保存入库单
			if ( this.ucLETicketNo.Value.Trim() == string.Empty )
			{
				this.ShowMessage(new UserControl.Message(MessageType.Error,"$Error_CS_Input_StockIn_TicketNo"));
				this.ucLETicketNo.TextFocus(false, true);
				return;
			}	

			//Laws Lu,2005/08/27
			if ( this.dtStockIn.Rows.Count == 0 )
			{
				this.ShowMessage(new UserControl.Message(MessageType.Error,"$CS_RCard_List_Is_Empty"));

				return;
			}

			if ( this._facade.IsStockInTicketExist(this.ucLETicketNo.Value.Trim().ToUpper()) )
			{
				this.ShowMessage(new UserControl.Message(MessageType.Error,"$Error_CS_Stock_In_Ticket_Exist"));	// 入库单号已存在
				this.ucLETicketNo.TextFocus(false, true);
				return;
			}

			this.DataProvider.BeginTransaction();

			//TODO:Laws Lu,2005/08/27
			try
			{				
				this._facade.AddMaterialStockIn( 
					this.ucLETicketNo.Value.Trim()
					,dtStockIn
					,ApplicationService.Current().UserCode
					,txtMEMO.Value);	
					
				this.DataProvider.CommitTransaction();
				//Laws Lu,2005/09/05,	清空数据
				//Laws Lu,2005/09/06,	清空Memo
				txtSumNum.Value = "0";
				ucLCModel.SelectedIndex = -1;
				ucLEMOCode.Value = String.Empty;
				txtMEMO.Value = String.Empty;
			}
			catch( Exception exp )
			{
				this.DataProvider.RollbackTransaction();
				throw exp;
			}

			this.ShowMessage(new UserControl.Message(MessageType.Success,"$CS_Save_Success"));		// 保存成功 入库单号

			//TODO:Laws Lu,2005/08/27

			sum.Clear();
			dtStockIn.Clear();
			_tmpTable.Clear();
			_tmpTable.AcceptChanges();
			dtStockIn.AcceptChanges();
//			this.lstRCardList.Items.Clear();
			this.ucLETicketNo.Value = string.Empty;
			this.ucLETicketNo.TextFocus(false, true);

			#endregion
		}

		private bool findItemFromList( string runningCard,out int rowNumber)
		{
			bool bReturn = false;
			rowNumber = -1;
			//TODO:Laws Lu,2005/08/27
			//判断产品序列号是否存在
			if(dtStockIn.Rows.Count > 0)
			{
				for(int iRowNum = 0; iRowNum< dtStockIn.Rows.Count;iRowNum++)
				{
					if ( dtStockIn.Rows[iRowNum]["产品序列号"].ToString().Trim() == runningCard)
					{
						rowNumber = iRowNum;
						bReturn = true;
						break;
					}
				}
			}

			return bReturn;

		}

		private Messages checkRCardExist( string runningCard,out System.Data.DataRow foundRow,out bool found)
		{
			foundRow = null;
			Messages msg = new Messages();
			int iFoundRowNum ;
			found = findItemFromList(runningCard,out iFoundRowNum);

			if (found)
			{
				foundRow = dtStockIn.Rows[iFoundRowNum];
			}
			else if( iFoundRowNum != -1 && false == found)
			{
				msg.Add(new UserControl.Message(MessageType.Error,"$Error_CS_ID_Exist_In_StockIn_Ticket $RCard=" + runningCard));	// 序列号已存在
			}

			return msg;
		
		}

		private void FStockInput_Closed(object sender, System.EventArgs e)
		{
			if (this.DataProvider!=null)
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();  
			}

		
		}

		private Messages CollectPlanate()
		{
			#region 二维条码采集
			Messages msg = new Messages();
			
			// 解析二维条码
			BarCodeParse barParser = new BarCodeParse(this._domainDataProvider);
			string[] idList = barParser.GetIDList( this.ucLEInput.Value.Trim() );
			string model = barParser.GetModelCode( this.ucLEInput.Value.Trim() );

			if(ucLCModel.ComboBoxData.SelectedIndex >= 0)
			{
				if (model.Trim().ToUpper() 
					!= ucLCModel.SelectedItemText.Trim().ToUpper())
				{
					msg.Add(new UserControl.Message(MessageType.Error,"$Error_CS_Planate_Model_Code_Not_Match"));

                    ucLCModel.Focus();

					return msg;
				}
			}
			else
			{
				msg.Add(new UserControl.Message(MessageType.Error,"$Error_CS_Planate_Model_Code_Not_Match"));

                ucLCModel.Focus();

				return msg;
			}
				
			if ( idList == null )
			{
				msg.Add(new UserControl.Message(MessageType.Error,"$CS_RCard_List_Is_Empty"));
				return msg;
			}

			DataRow dr = null;
			//Laws Lu,2005/08/27
			// 检查序列号是否已在List中存在
			ArrayList ar = new ArrayList();
			foreach ( string id in idList )
			{
				bool found;
				msg.AddMessages(checkRCardExist( id,out dr,out found));
			
				if(found)
				{
					msg.Add(new UserControl.Message(MessageType.Error,"$Error_CS_ID_Exist_In_StockIn_Ticket $RCard=" + id));	// 序列号已存在
					return msg;
				}

				if(dr == null && !found)
				{
					dr = dtStockIn.NewRow();
				}

				dr["产品序列号"] = id.ToUpper().Trim();
				dr["产品别"] = ucLCModel.SelectedItemText.Trim().ToUpper();
				//dr["数量"] = ApplicationService.Current().UserCode;
				dr["工单"] = ucLEMOCode.Value.Trim();
				dr["CollectType"] = StockCollectionType.Planate;

				dr["MODEL"] = ucLCModel.SelectedItemText.Trim().ToUpper();;
				dr["MO"] = ucLEMOCode.Value.Trim();;

				if(!dtStockIn.Rows.Contains(id.ToUpper().Trim()))
				{
					//Laws Lu,2005/09/05,修改	允许按产品别 + 工单	统计
					InsertRow(id,dr);
				}
				else
				{
					ar.Add(id.ToUpper().Trim());
				}
			}

			foreach(object obj in ar)
			{
				msg.Add(new UserControl.Message("$CS_Param_ID " + obj.ToString().ToUpper() +" $CS_IDRepeatCollect"));
			}

			return msg;
			#endregion
		}


		private Messages CollectPCS()
		{
			#region 序列号采集
			Messages msg = new Messages();

			string id = this.ucLEInput.Value.Trim().ToUpper();
			id = id.Substring(0, Math.Min(40,id.Length));

//			if(ucLCModel.ComboBoxData.SelectedIndex < 0)
//			{
//				//msg.Add(new UserControl.Message(MessageType.Error,"$Error_CS_Planate_Model_Code_Not_Match"));
//
//				ucLCModel.TextFocus(false, true);
//
//				return msg;
//			}

			// 检查序列号是否已在List中存在
			DataRow dr = null;
			bool found;
			msg.AddMessages(checkRCardExist( id,out dr,out found));
			
			if(found)
			{
				msg.Add(new UserControl.Message(MessageType.Error,"$Error_CS_ID_Exist_In_StockIn_Ticket $RCard=" + id));	// 序列号已存在
				return msg;
			}
			if(dr == null && !found)
			{
				dr = dtStockIn.NewRow();
			}

			dr["产品序列号"] = id.ToUpper();
			
			dr["产品别"] = ucLCModel.SelectedItemValue;

			//dr["数量"] = ApplicationService.Current().UserCode;
			dr["工单"] = ucLEMOCode.Value.Trim();

			dr["CollectType"] = StockCollectionType.PCS;

			dr["MODEL"] = ucLCModel.SelectedItemText.Trim().ToUpper();;
			dr["MO"] = ucLEMOCode.Value.Trim();;
			
			msg.AddMessages(InsertRow(id,dr));

			return msg;
			#endregion
		}

		private Messages InsertRow(string id,DataRow dr)
		{
			Messages msg = new Messages();
			//Laws Lu,2005/08/27
			if(!dtStockIn.Rows.Contains(id.ToUpper().Trim()))
			{
				#region 入库采集
				//Laws Lu,2005/09/05,修改	允许按产品别 + 工单	统计
				DataRow[] drs = dtStockIn.Select("产品别='" + dr["产品别"].ToString().Trim() 
					+ "' and 工单='" + dr["工单"].ToString().Trim() + "'");
				if(drs == null || (drs != null && drs.Length < 1))
				{
					_tmpTable.Rows.Add(new object[]{
													   ucLCModel.SelectedItemText.Trim().ToUpper()
													   ,1
													   ,ucLEMOCode.Value.Trim()
													   ,id.ToUpper().Trim()
													   ,ucLCModel.SelectedItemText.Trim().ToUpper()
													   ,ucLEMOCode.Value.Trim()});

					sum.Add(dr["产品别"].ToString().Trim()+dr["工单"].ToString().Trim(),1);

					dtStockIn.Rows.Add(dr);

					_tmpTable.AcceptChanges();
					dtStockIn.AcceptChanges();
				}
				else
				{
					DataRow[] drtmps = _tmpTable.Select("产品别='" + dr["产品别"].ToString().Trim() 
						+ "' and 工单='" + dr["工单"].ToString().Trim() + "'");

					if(drtmps == null || drtmps.Length < 1)
					{
						sum.Add(dr["产品别"].ToString().Trim()+dr["工单"].ToString().Trim(),1);

						_tmpTable.Rows.Add(new object[]{
														   ucLCModel.SelectedItemText.Trim().ToUpper()
														   ,1
														   ,ucLEMOCode.Value.Trim()
														   ,id.ToUpper().Trim()
														   ,ucLCModel.SelectedItemText.Trim().ToUpper()
														   ,ucLEMOCode.Value.Trim()});

						dtStockIn.Rows.Add(dr);

						_tmpTable.AcceptChanges();
						dtStockIn.AcceptChanges();
					}
					else
					{
						foreach(DataRow drOld in drtmps)
						{
							if(drOld["数量"] != null 
								&& drOld["MODEL"].ToString().ToUpper() == dr["产品别"].ToString().ToUpper() 
								&& drOld["MO"].ToString().ToUpper() == dr["工单"].ToString().ToUpper())
							{
								if(sum.ContainsKey(dr["产品别"].ToString().Trim()+dr["工单"].ToString().Trim()))
								{
									sum[dr["产品别"].ToString().Trim()+dr["工单"].ToString().Trim()] = Convert.ToInt32(sum[dr["产品别"].ToString().Trim()+dr["工单"].ToString().Trim()]) + 1;

									drOld["数量"] = sum[dr["产品别"].ToString().Trim()+dr["工单"].ToString().Trim()];

									_tmpTable.Rows.Add(new object[]{
																	   null
																	   ,null
																	   ,null
																	   ,id.ToUpper().Trim()
																	   ,ucLCModel.SelectedItemText.Trim().ToUpper()
																	   ,ucLEMOCode.Value.Trim()});

									dtStockIn.Rows.Add(dr);

									_tmpTable.AcceptChanges();
									dtStockIn.AcceptChanges();
								}
								else
								{
									sum.Add(dr["产品别"].ToString().Trim()+dr["工单"].ToString().Trim(),1);

									_tmpTable.Rows.Add(new object[]{
																	   ucLCModel.SelectedItemText.Trim().ToUpper()
																	   ,1
																	   ,ucLEMOCode.Value.Trim()
																	   ,id.ToUpper().Trim()
																	   ,ucLCModel.SelectedItemText.Trim().ToUpper()
																	   ,ucLEMOCode.Value.Trim()});

									dtStockIn.Rows.Add(dr);

									_tmpTable.AcceptChanges();
									dtStockIn.AcceptChanges();
								}
							}
						}
					}
					
				}
				//End Laws Lu

				int iSum = txtSumNum.Value.Trim()==String.Empty?0:Convert.ToInt32(txtSumNum.Value.Trim());
				txtSumNum.Value = Convert.ToString(iSum + 1);
				#endregion	
			}
			else
			{
				msg.Add(new UserControl.Message("$CS_Param_ID " + id.ToUpper() +" $CS_IDRepeatCollect"));
			}

			return msg;
		}

		public string PlanateNum;

		private Messages RemovePlanate()
		{
			#region 移除二维条码
			Messages msg = new Messages();
			if ( this.ultraOsType.CheckedIndex == (int)CollectionType.Planate)
			{	
				FStockRemove form = new FStockRemove(this);
				form.Text = this.Text;
						
				if ( form.ShowDialog() == DialogResult.OK )
				{
					BarCodeParse barParser = new BarCodeParse(this._domainDataProvider);

					string[] idList = barParser.GetIDList( PlanateNum );
					string Model = barParser.GetModelCode( PlanateNum );

					if(ucLCModel.ComboBoxData.SelectedIndex >= 0)
					{
						if (Model.Trim().ToUpper() 
							!= ucLCModel.SelectedItemText.Trim().ToUpper())
						{
							msg.Add(new UserControl.Message(MessageType.Error,"$Error_CS_Planate_Model_Code_Not_Match"));

                            ucLCModel.Focus();
						}
					}
					else
					{
						msg.Add(new UserControl.Message(MessageType.Error,"$Error_CS_Planate_Model_Code_Not_Match"));

                        ucLCModel.Focus();
					}
				
					if ( idList == null )
					{
						msg.Add(new UserControl.Message(MessageType.Error,"$CS_RCard_List_Is_Empty"));
						return msg;
					}

					// 检查序列号是否已在List中存在
					DataRow dr = null;
					ArrayList ar = new ArrayList();

					foreach ( string id in idList )
					{
						bool found;
						msg.AddMessages(checkRCardExist( id,out dr,out found));
			
						if(dr != null)
						{
							//Laws Lu,2005/08/27
							//Laws Lu,2005/09/07,修改	允许移除同时减小计
							string model = String.Empty;
							string mo = String.Empty;
							int iSubSum = 0;
							bool bNeed = false;
							if(dtStockIn.Rows.Contains(id.ToUpper().Trim()))
							{

								foreach(DataRow tempDR in _tmpTable.Select("产品序列号='" 
									+ dr["产品序列号"].ToString().ToUpper().Trim()
									+ "'"))
								{

									if(tempDR["数量"].ToString() != String.Empty)
									{
										bNeed = true;
								
										iSubSum = Convert.ToInt32(tempDR["数量"].ToString().ToUpper().Trim()) - 1;
									}

									model = tempDR["MODEL"].ToString();
									mo = tempDR["MO"].ToString();

									_tmpTable.Rows.Remove(tempDR);

								}

								//Laws Lu,2005/09/07,新增	清除小计
//								sum[dr["产品别"].ToString().ToUpper().Trim()+dr["工单"].ToString().ToUpper().Trim()]
//									= Convert.ToInt32(sum[dr["产品别"].ToString().ToUpper().Trim()+dr["工单"].ToString().ToUpper().Trim()]) - 1;
//								if( Convert.ToInt32(sum[dr["产品别"].ToString().ToUpper().Trim()+dr["工单"].ToString().ToUpper().Trim()]) == 0)
//								{
//									sum.Remove(dr["产品别"].ToString().ToUpper().Trim()+dr["工单"].ToString().ToUpper().Trim());
//								}

								dtStockIn.Rows.Remove(dr);

								DataView dv = (DataView)ultraGridContent.DataSource;
								if(bNeed == true)
								{

									DataRow[] drs = _tmpTable.Select("MODEL='" 
										+ model + "' and MO='"
										+ mo +"'");
						
									if(drs.Length > 0)
									{
										drs[0]["产品别"] = model;
										drs[0]["工单"] = mo;
										drs[0]["数量"] = Convert.ToInt32(sum[model+mo]) - 1;

										sum[model+mo] = drs[0]["数量"];

										if(Convert.ToInt32(sum[model+mo]) == 0)
										{
											sum.Remove(model+mo);
										}
									}
									else
									{
										sum.Remove(model+mo);
									}

									//						if(dv.Table.Rows.Count > 0)
									//						{
									//							dv.Table.Rows[iRowIndex + 1]["产品别"] = model;
									//							dv.Table.Rows[iRowIndex + 1]["工单"] = mo;
									//							dv.Table.Rows[iRowIndex + 1]["数量"] = iSubSum;
									//						}

								}
								else
								{
									DataRow[] drs = _tmpTable.Select("MODEL='" 
										+ model + "' and MO='"
										+ mo +"' and 数量<>''");
						
									if(drs.Length > 0)
									{
										drs[0]["产品别"] = model;
										drs[0]["工单"] = mo;
										drs[0]["数量"] = Convert.ToInt32(drs[0]["数量"]) - 1;

										sum[model+mo] = drs[0]["数量"];

										if(Convert.ToInt32(sum[model+mo]) == 0)
										{
											sum.Remove(model+mo);
										}
									}
									else
									{
										sum.Remove(model+mo);
									}
						
								}

								int iSum = txtSumNum.Value.Trim()==String.Empty?0:Convert.ToInt32(txtSumNum.Value.Trim());

								if(iSum != 0)
								{
									txtSumNum.Value = Convert.ToString(iSum - 1);
								}
								
									
								_tmpTable.AcceptChanges();
								dtStockIn.AcceptChanges();
							}
//							else
//							{
//								ar.Add(id.ToUpper().Trim());
//							}
						}
					}

//					foreach(object obj in ar)
//					{
//						msg.Add(new UserControl.Message("$CS_Param_ID " + obj.ToString().ToUpper() +" $CS_IDRepeatCollect"));
//					}
				}
			}

			return msg;
			#endregion
		}

		private Messages RemovePCS()
		{
			#region 移除序列号
			Messages msg = new Messages();
			if ( this.ultraOsType.CheckedIndex == (int)CollectionType.PCS )
			{
				//Laws Lu,2005/08/27
				if (dtStockIn.Rows.Count < 1 || ultraGridContent.Selected.Rows.Count < 1)
				{
					msg.Add(new UserControl.Message(MessageType.Error,"$Error_CS_Select_ID_To_Delete"));
				}
				else
				{
					//Laws Lu,2005/09/07,修改	允许移除同时减小计
					string model = String.Empty;
					string mo = String.Empty;
					int iSubSum = 0;
					bool bNeed = false;
//					foreach(Infragistics.Win.UltraWinGrid.UltraGridRow dr in ultraGridContent.Selected.Rows)
					for ( int iGridRowLoopIndex = 0; iGridRowLoopIndex < ultraGridContent.Selected.Rows.Count; iGridRowLoopIndex++)
					{
						Infragistics.Win.UltraWinGrid.UltraGridRow dr = ultraGridContent.Selected.Rows[iGridRowLoopIndex];
						foreach(DataRow drDT in dtStockIn.Select("产品序列号='" 
							+ dr.Cells["产品序列号"].Text.ToString().ToUpper().Trim()
							+ "'"))
						{

							if(dr.Cells["数量"].Text.ToString() != String.Empty)
							{
								bNeed = true;
								
								iSubSum = Convert.ToInt32(dr.Cells["数量"].Text.ToString().ToUpper().Trim()) - 1;
							}

							model = drDT["MODEL"].ToString();
							mo = drDT["MO"].ToString();

							dtStockIn.Rows.Remove(drDT);

						}

					}

					ultraGridContent.DeleteSelectedRows(false);

					_tmpTable.AcceptChanges();
					dtStockIn.AcceptChanges();
					
					DataView dv = (DataView)ultraGridContent.DataSource;
					if(bNeed == true)
					{

						DataRow[] drs = _tmpTable.Select("MODEL='" 
							+ model + "' and MO='"
							+ mo +"'");
						
						if(drs.Length > 0)
						{
							drs[0]["产品别"] = model;
							drs[0]["工单"] = mo;
							drs[0]["数量"] = Convert.ToInt32(sum[model+mo]) - 1;

							sum[model+mo] = drs[0]["数量"];

							if(Convert.ToInt32(sum[model+mo]) == 0)
							{
								sum.Remove(model+mo);
							}
						}
						else
						{
							sum.Remove(model+mo);
						}

//						if(dv.Table.Rows.Count > 0)
//						{
//							dv.Table.Rows[iRowIndex + 1]["产品别"] = model;
//							dv.Table.Rows[iRowIndex + 1]["工单"] = mo;
//							dv.Table.Rows[iRowIndex + 1]["数量"] = iSubSum;
//						}

					}
					else
					{
						DataRow[] drs = _tmpTable.Select("MODEL='" 
							+ model + "' and MO='"
							+ mo +"' and 数量<>''");
						
						if(drs.Length > 0)
						{
							drs[0]["产品别"] = model;
							drs[0]["工单"] = mo;
							drs[0]["数量"] = Convert.ToInt32(drs[0]["数量"]) - 1;

							sum[model+mo] = drs[0]["数量"];

							if(Convert.ToInt32(sum[model+mo]) == 0)
							{
								sum.Remove(model+mo);
							}
						}
						else
						{
							sum.Remove(model+mo);
						}
						
					}

					int iSum = txtSumNum.Value.Trim()==String.Empty?0:Convert.ToInt32(txtSumNum.Value.Trim());

					if(iSum != 0)
					{
						txtSumNum.Value = Convert.ToString(iSum - 1);
					}
					
				}

				
			}

			return msg;
			#endregion
		}

		#region 数据显示相关
		private void InitializeTempDatable()
		{
			_tmpTable.Columns.Clear();

			_tmpTable.Columns.Add( "产品别", typeof( string ));
			_tmpTable.Columns.Add( "数量", typeof( string ) );
			_tmpTable.Columns.Add( "工单", typeof( string ) );
			_tmpTable.Columns.Add( "产品序列号", typeof( string )).Unique = true;
			_tmpTable.Columns.Add( "MODEL", typeof( string ) );
			_tmpTable.Columns.Add( "MO", typeof( string ) );

		}

		private void ultraGridMain_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
		{
			ultraWinGridHelper = new UltraWinGridHelper(ultraGridContent);

			ultraWinGridHelper.AddCommonColumn("产品别","产品别");
			ultraWinGridHelper.AddCommonColumn("数量","数量");
			ultraWinGridHelper.AddCommonColumn("工单","工单");
			ultraWinGridHelper.AddCommonColumn("产品序列号","产品序列号");

		}

		#endregion
	}
}
