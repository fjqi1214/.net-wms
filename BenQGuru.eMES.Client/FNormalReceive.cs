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
	/// FNormalReceive 的摘要说明。
	/// </summary>
	public class FNormalReceive : System.Windows.Forms.Form
	{
		#region 变量声明部分

		protected UserControl.UCButton ucBtnExit;

		protected DataTable _tmpTable = new DataTable();
		protected System.ComponentModel.Container components = null;

		protected InventoryFacade _facade = null;

		protected IDomainDataProvider _domainDataProvider;
		protected UserControl.UCButton ucBtnComplete;

		//protected DataTable _tblRCard = new DataTable();
		protected System.Windows.Forms.GroupBox groupBox1;
		protected Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOsType;
		protected UserControl.UCLabelEdit txtSumNum;
		protected UserControl.UCLabelEdit txtItemDesc;
		protected UserControl.UCLabelCombox cbxItemCode;
		protected UserControl.UCLabelCombox cbxModel;
		protected System.Windows.Forms.Panel pnlAbnormal;
		protected System.Windows.Forms.Panel panelReceiveNo;
		protected System.Windows.Forms.Panel panelDataInput;
		protected UserControl.UCButton btnDeleteDetail;
		protected UserControl.UCButton btnDeleteRCard;
		protected System.Windows.Forms.Panel panelBottom;
		protected System.Windows.Forms.Panel panelDetail;
		protected System.Windows.Forms.GroupBox gbxDetail;
		protected System.Windows.Forms.Panel panel2;
		protected System.Windows.Forms.Panel panelGrid;
		protected Infragistics.Win.UltraWinGrid.UltraGrid ultraGridContent;
		private System.Windows.Forms.GroupBox groupBox2;
		private UserControl.UCButton btnAdd;
		private FInfoForm _infoForm;
		protected string _recNo = string.Empty;
		protected System.Windows.Forms.TextBox ucLEInput;
		private System.Windows.Forms.Label label1;
		protected UserControl.UCButton btnUnComplete;
		private System.Windows.Forms.CheckBox chbModel;
		protected UserControl.UCLabelEdit txtCartonNum;
		protected System.Windows.Forms.TextBox ucLETicketNo;
		private System.Windows.Forms.Label lblRecNo;
		private System.Windows.Forms.RadioButton rbAdd;
		private System.Windows.Forms.RadioButton rbDelete;
		protected System.Windows.Forms.Panel panel1;
		protected System.Windows.Forms.RadioButton rbMO;
		protected System.Windows.Forms.RadioButton rbProduct;
		protected System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel3;
		private UserControl.UCButton btnToss;
		protected string _currseq = "0";
		#endregion

		#region 系统部分（构造函数，系统服务）
		public FNormalReceive()
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
		
	
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		protected void SucessMessage(string msg)
		{
			_infoForm.Add(new UserControl.Message(UserControl.MessageType.Success,msg));
		}

		protected void ErrorMessage(string msg)
		{			
			_infoForm.Add(new UserControl.Message(UserControl.MessageType.Error,msg));
			BenQGuru.eMES.Web.Helper.SoundPlayer.PlayErrorMusic();
		}

		private void FNormalReceive_Closed(object sender, System.EventArgs e)
		{
			CloseConnection();
		}

		private void CloseConnection()
		{
			if (this._domainDataProvider!=null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this._domainDataProvider).PersistBroker.CloseConnection(); 
		}

		private void FNormalReceive_Load(object sender, System.EventArgs e)
		{
			try
			{
				_infoForm = ApplicationRun.GetInfoForm();
				_infoForm.Add("");
				_domainDataProvider =ApplicationService.Current().DataProvider;
				this._facade = new InventoryFacade( this.DataProvider );
			}
			catch(System.Exception ex)
			{
				if(!this.DesignMode) //如果不是处在设计模式，将异常重新抛出
					throw ex;
			}

			ucLETicketNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			UserControl.UIStyleBuilder.FormUI(this);
			
			#region 初始化Grid
			UserControl.UIStyleBuilder.GridUI(this.ultraGridContent);
			//UserControl.UIStyleBuilder.GridUI(this.gridRCard);

			_tmpTable.Columns.Clear();
			_tmpTable.Columns.Add("*",typeof(bool));
			_tmpTable.Columns.Add("NO",typeof(string));
			_tmpTable.Columns.Add("SEQ",typeof(int));
			_tmpTable.Columns.Add( "MoCode", typeof( string ));
			_tmpTable.Columns.Add( "ModelCode", typeof( string ));
			_tmpTable.Columns.Add( "ItemCode", typeof( string ));
			_tmpTable.Columns.Add( "ItemDesc", typeof( string ));
			_tmpTable.Columns.Add( "PlanQty", typeof( string ) );
			_tmpTable.Columns.Add( "ActQty", typeof( string ) );
			_tmpTable.Columns.Add( "status", typeof( string ) );
			BindGrid();

//			this._tblRCard.Columns.Clear();
//			this._tblRCard.Columns.Add("MoCode",typeof(string));
//			this._tblRCard.Columns.Add("RCard",typeof(string));
//			this._tblRCard.Columns.Add("CollectType",typeof(string));
//			this._tblRCard.Columns.Add("SEQ",typeof(string));
//
//			System.Data.DataView dv = new System.Data.DataView(this._tblRCard,"SEQ=" + 0,string.Empty,System.Data.DataViewRowState.CurrentRows);
//			BindRCard(dv);

			#endregion

			#region 检验对象的类型
			//默认选择为二维条码
			ultraOsType.Items.Clear();
			ultraOsType.CheckedItem =  ultraOsType.Items.Add(CollectionType.Carton.ToString(),CollectionType.Carton.ToString());
			//this.ultraOptionSetOQCExameOpion.Items.Add(OQCFacade.OQC_ExameObject_Carton,"Carton");
			ultraOsType.Items.Add(CollectionType.PCS.ToString(),CollectionType.PCS.ToString());
			#endregion

			InitControl();

            this.ucLETicketNo.Focus();
			this.ucLETicketNo.BackColor = Color.GreenYellow;
			this.ucLETicketNo.MaxLength = 10000;
		}

		private void gridRCard_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
		{
//			UltraWinGridHelper gridHelper = new UltraWinGridHelper(this.gridRCard);
//			gridHelper.AddCommonColumn("MoCode","工单");
//			gridHelper.AddCommonColumn("RCard","序列号");
//			gridHelper.AddCommonColumn("CollectType","CollectType");
//			gridHelper.AddCommonColumn("SEQ", "SEQ");
		}

		private void ultraGridContent_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
		{
			UltraWinGridHelper gridHelper = new UltraWinGridHelper(this.ultraGridContent);
			gridHelper.AddCommonColumn("*","*");
			this.ultraGridContent.DisplayLayout.Bands[0].Columns["*"].Width = 20;
			gridHelper.AddCommonColumn("NO","NO");
			gridHelper.AddCommonColumn("SEQ","SEQ");
			gridHelper.AddCommonColumn("MoCode","工单");
			gridHelper.AddCommonColumn("ModelCode","产品别");
			gridHelper.AddCommonColumn("ItemCode","产品代码");
			gridHelper.AddCommonColumn("ItemDesc","产品描述");
			gridHelper.AddCommonColumn("PlanQty", "计划数量");
			gridHelper.AddCommonColumn("ActQty","实际数量");
			gridHelper.AddCommonColumn( "status","status");
		}
		private void BindGrid()
		{
			ultraGridContent.DataSource = null;
			DataView dv = _tmpTable.DefaultView;
			dv.Sort = "MoCode,ModelCode,ItemCode";
			ultraGridContent.DataSource = dv;
			ultraGridContent.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit;
			ultraGridContent.DisplayLayout.Bands[0].Columns["NO"].Hidden = true;
			ultraGridContent.DisplayLayout.Bands[0].Columns["SEQ"].Hidden = true;
			ultraGridContent.DisplayLayout.Bands[0].Columns["status"].Hidden = true;
			for(int i=1;i<this.ultraGridContent.DisplayLayout.Bands[0].Columns.Count;i++)
			{
				this.ultraGridContent.DisplayLayout.Bands[0].Columns[i].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
			}
		}

		private void BindRCard(System.Data.DataView dv)
		{
//			this.gridRCard.DataSource = null;
//			this.gridRCard.DataSource = dv;
//			
//			this.gridRCard.DisplayLayout.Bands[0].Columns["CollectType"].Hidden = true;
//			this.gridRCard.DisplayLayout.Bands[0].Columns["SEQ"].Hidden = true;

			InitControl();
		}

		#endregion 

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FNormalReceive));
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnUnComplete = new UserControl.UCButton();
            this.ucBtnComplete = new UserControl.UCButton();
            this.ucBtnExit = new UserControl.UCButton();
            this.btnDeleteDetail = new UserControl.UCButton();
            this.btnDeleteRCard = new UserControl.UCButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ultraOsType = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.btnToss = new UserControl.UCButton();
            this.panelReceiveNo = new System.Windows.Forms.Panel();
            this.lblRecNo = new System.Windows.Forms.Label();
            this.ucLETicketNo = new System.Windows.Forms.TextBox();
            this.txtCartonNum = new UserControl.UCLabelEdit();
            this.txtSumNum = new UserControl.UCLabelEdit();
            this.btnAdd = new UserControl.UCButton();
            this.pnlAbnormal = new System.Windows.Forms.Panel();
            this.chbModel = new System.Windows.Forms.CheckBox();
            this.txtItemDesc = new UserControl.UCLabelEdit();
            this.cbxItemCode = new UserControl.UCLabelCombox();
            this.cbxModel = new UserControl.UCLabelCombox();
            this.panelDataInput = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbDelete = new System.Windows.Forms.RadioButton();
            this.rbAdd = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.rbProduct = new System.Windows.Forms.RadioButton();
            this.rbMO = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.ucLEInput = new System.Windows.Forms.TextBox();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.gbxDetail = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.ultraGridContent = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panelBottom.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOsType)).BeginInit();
            this.panelReceiveNo.SuspendLayout();
            this.pnlAbnormal.SuspendLayout();
            this.panelDataInput.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelDetail.SuspendLayout();
            this.gbxDetail.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridContent)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.groupBox2);
            this.panelBottom.Controls.Add(this.btnDeleteDetail);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 476);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(886, 71);
            this.panelBottom.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnUnComplete);
            this.groupBox2.Controls.Add(this.ucBtnComplete);
            this.groupBox2.Controls.Add(this.ucBtnExit);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(886, 41);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // btnUnComplete
            // 
            this.btnUnComplete.BackColor = System.Drawing.SystemColors.Control;
            this.btnUnComplete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUnComplete.BackgroundImage")));
            this.btnUnComplete.ButtonType = UserControl.ButtonTypes.None;
            this.btnUnComplete.Caption = "取消完成";
            this.btnUnComplete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUnComplete.Location = new System.Drawing.Point(392, 13);
            this.btnUnComplete.Name = "btnUnComplete";
            this.btnUnComplete.Size = new System.Drawing.Size(88, 22);
            this.btnUnComplete.TabIndex = 3;
            this.btnUnComplete.Click += new System.EventHandler(this.btnUnComplete_Click);
            // 
            // ucBtnComplete
            // 
            this.ucBtnComplete.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnComplete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnComplete.BackgroundImage")));
            this.ucBtnComplete.ButtonType = UserControl.ButtonTypes.None;
            this.ucBtnComplete.Caption = "完成入库单";
            this.ucBtnComplete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnComplete.Location = new System.Drawing.Point(278, 13);
            this.ucBtnComplete.Name = "ucBtnComplete";
            this.ucBtnComplete.Size = new System.Drawing.Size(88, 22);
            this.ucBtnComplete.TabIndex = 1;
            this.ucBtnComplete.Click += new System.EventHandler(this.ucBtnComplete_Click);
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(507, 13);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 2;
            // 
            // btnDeleteDetail
            // 
            this.btnDeleteDetail.BackColor = System.Drawing.SystemColors.Control;
            this.btnDeleteDetail.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteDetail.BackgroundImage")));
            this.btnDeleteDetail.ButtonType = UserControl.ButtonTypes.None;
            this.btnDeleteDetail.Caption = "删除明细";
            this.btnDeleteDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteDetail.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDeleteDetail.Location = new System.Drawing.Point(7, 7);
            this.btnDeleteDetail.Name = "btnDeleteDetail";
            this.btnDeleteDetail.Size = new System.Drawing.Size(88, 22);
            this.btnDeleteDetail.TabIndex = 6;
            this.btnDeleteDetail.Click += new System.EventHandler(this.btnDeleteDetail_Click);
            // 
            // btnDeleteRCard
            // 
            this.btnDeleteRCard.BackColor = System.Drawing.SystemColors.Control;
            this.btnDeleteRCard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteRCard.BackgroundImage")));
            this.btnDeleteRCard.ButtonType = UserControl.ButtonTypes.None;
            this.btnDeleteRCard.Caption = "删除";
            this.btnDeleteRCard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteRCard.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDeleteRCard.Location = new System.Drawing.Point(716, 4);
            this.btnDeleteRCard.Name = "btnDeleteRCard";
            this.btnDeleteRCard.Size = new System.Drawing.Size(88, 22);
            this.btnDeleteRCard.TabIndex = 7;
            this.btnDeleteRCard.Visible = false;
            this.btnDeleteRCard.Click += new System.EventHandler(this.btnDeleteRCard_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ultraOsType);
            this.groupBox1.Controls.Add(this.btnToss);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(886, 41);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "采集方式";
            // 
            // ultraOsType
            // 
            this.ultraOsType.BackColor = System.Drawing.SystemColors.Control;
            this.ultraOsType.BackColorInternal = System.Drawing.SystemColors.Control;
            this.ultraOsType.Dock = System.Windows.Forms.DockStyle.Left;
            this.ultraOsType.ImageTransparentColor = System.Drawing.Color.Gainsboro;
            valueListItem1.DisplayText = "二维条码     ";
            valueListItem2.DisplayText = "PCS";
            this.ultraOsType.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem1,
            valueListItem2});
            this.ultraOsType.ItemSpacingVertical = 10;
            this.ultraOsType.Location = new System.Drawing.Point(3, 16);
            this.ultraOsType.Name = "ultraOsType";
            this.ultraOsType.Size = new System.Drawing.Size(465, 22);
            this.ultraOsType.TabIndex = 0;
            this.ultraOsType.TabStop = false;
            this.ultraOsType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // btnToss
            // 
            this.btnToss.BackColor = System.Drawing.SystemColors.Control;
            this.btnToss.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnToss.BackgroundImage")));
            this.btnToss.ButtonType = UserControl.ButtonTypes.None;
            this.btnToss.Caption = "抛转";
            this.btnToss.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToss.Location = new System.Drawing.Point(580, 12);
            this.btnToss.Name = "btnToss";
            this.btnToss.Size = new System.Drawing.Size(88, 22);
            this.btnToss.TabIndex = 15;
            this.btnToss.Click += new System.EventHandler(this.btnToss_Click);
            // 
            // panelReceiveNo
            // 
            this.panelReceiveNo.Controls.Add(this.lblRecNo);
            this.panelReceiveNo.Controls.Add(this.ucLETicketNo);
            this.panelReceiveNo.Controls.Add(this.txtCartonNum);
            this.panelReceiveNo.Controls.Add(this.txtSumNum);
            this.panelReceiveNo.Controls.Add(this.btnAdd);
            this.panelReceiveNo.Controls.Add(this.btnDeleteRCard);
            this.panelReceiveNo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelReceiveNo.Location = new System.Drawing.Point(0, 41);
            this.panelReceiveNo.Name = "panelReceiveNo";
            this.panelReceiveNo.Size = new System.Drawing.Size(886, 30);
            this.panelReceiveNo.TabIndex = 0;
            // 
            // lblRecNo
            // 
            this.lblRecNo.Location = new System.Drawing.Point(12, 11);
            this.lblRecNo.Name = "lblRecNo";
            this.lblRecNo.Size = new System.Drawing.Size(57, 15);
            this.lblRecNo.TabIndex = 6;
            this.lblRecNo.Text = "入库单号";
            // 
            // ucLETicketNo
            // 
            this.ucLETicketNo.BackColor = System.Drawing.Color.GreenYellow;
            this.ucLETicketNo.Location = new System.Drawing.Point(75, 8);
            this.ucLETicketNo.Name = "ucLETicketNo";
            this.ucLETicketNo.Size = new System.Drawing.Size(173, 20);
            this.ucLETicketNo.TabIndex = 0;
            this.ucLETicketNo.Leave += new System.EventHandler(this.ucLETicketNo_Leave);
            this.ucLETicketNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLETicketNo_TxtboxKeyPress);
            this.ucLETicketNo.Enter += new System.EventHandler(this.ucLETicketNo_Enter);
            // 
            // txtCartonNum
            // 
            this.txtCartonNum.AllowEditOnlyChecked = true;
            this.txtCartonNum.Caption = "Carton数";
            this.txtCartonNum.Checked = false;
            this.txtCartonNum.EditType = UserControl.EditTypes.String;
            this.txtCartonNum.Location = new System.Drawing.Point(445, 4);
            this.txtCartonNum.MaxLength = 40;
            this.txtCartonNum.Multiline = false;
            this.txtCartonNum.Name = "txtCartonNum";
            this.txtCartonNum.PasswordChar = '\0';
            this.txtCartonNum.ReadOnly = true;
            this.txtCartonNum.ShowCheckBox = false;
            this.txtCartonNum.Size = new System.Drawing.Size(135, 23);
            this.txtCartonNum.TabIndex = 2;
            this.txtCartonNum.TabNext = false;
            this.txtCartonNum.TabStop = false;
            this.txtCartonNum.Value = "0";
            this.txtCartonNum.WidthType = UserControl.WidthTypes.Small;
            this.txtCartonNum.XAlign = 496;
            // 
            // txtSumNum
            // 
            this.txtSumNum.AllowEditOnlyChecked = true;
            this.txtSumNum.Caption = "数量总计";
            this.txtSumNum.Checked = false;
            this.txtSumNum.EditType = UserControl.EditTypes.String;
            this.txtSumNum.Location = new System.Drawing.Point(278, 5);
            this.txtSumNum.MaxLength = 40;
            this.txtSumNum.Multiline = false;
            this.txtSumNum.Name = "txtSumNum";
            this.txtSumNum.PasswordChar = '\0';
            this.txtSumNum.ReadOnly = true;
            this.txtSumNum.ShowCheckBox = false;
            this.txtSumNum.Size = new System.Drawing.Size(134, 23);
            this.txtSumNum.TabIndex = 1;
            this.txtSumNum.TabNext = false;
            this.txtSumNum.TabStop = false;
            this.txtSumNum.Value = "0";
            this.txtSumNum.WidthType = UserControl.WidthTypes.Small;
            this.txtSumNum.XAlign = 329;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.Control;
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.ButtonType = UserControl.ButtonTypes.Add;
            this.btnAdd.Caption = "新增";
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Location = new System.Drawing.Point(610, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 22);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // pnlAbnormal
            // 
            this.pnlAbnormal.Controls.Add(this.chbModel);
            this.pnlAbnormal.Controls.Add(this.txtItemDesc);
            this.pnlAbnormal.Controls.Add(this.cbxItemCode);
            this.pnlAbnormal.Controls.Add(this.cbxModel);
            this.pnlAbnormal.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAbnormal.Location = new System.Drawing.Point(0, 71);
            this.pnlAbnormal.Name = "pnlAbnormal";
            this.pnlAbnormal.Size = new System.Drawing.Size(886, 30);
            this.pnlAbnormal.TabIndex = 20;
            // 
            // chbModel
            // 
            this.chbModel.Checked = true;
            this.chbModel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbModel.Location = new System.Drawing.Point(580, 6);
            this.chbModel.Name = "chbModel";
            this.chbModel.Size = new System.Drawing.Size(93, 23);
            this.chbModel.TabIndex = 14;
            this.chbModel.Text = "是否比对产品别";
            this.chbModel.Visible = false;
            // 
            // txtItemDesc
            // 
            this.txtItemDesc.AllowEditOnlyChecked = true;
            this.txtItemDesc.Caption = "产品描述";
            this.txtItemDesc.Checked = false;
            this.txtItemDesc.EditType = UserControl.EditTypes.String;
            this.txtItemDesc.Location = new System.Drawing.Point(392, 7);
            this.txtItemDesc.MaxLength = 40;
            this.txtItemDesc.Multiline = false;
            this.txtItemDesc.Name = "txtItemDesc";
            this.txtItemDesc.PasswordChar = '\0';
            this.txtItemDesc.ReadOnly = true;
            this.txtItemDesc.ShowCheckBox = false;
            this.txtItemDesc.Size = new System.Drawing.Size(162, 23);
            this.txtItemDesc.TabIndex = 13;
            this.txtItemDesc.TabNext = true;
            this.txtItemDesc.Value = "";
            this.txtItemDesc.WidthType = UserControl.WidthTypes.Normal;
            this.txtItemDesc.XAlign = 443;
            // 
            // cbxItemCode
            // 
            this.cbxItemCode.AllowEditOnlyChecked = true;
            this.cbxItemCode.Caption = "产品代码";
            this.cbxItemCode.Checked = false;
            this.cbxItemCode.Location = new System.Drawing.Point(196, 6);
            this.cbxItemCode.Name = "cbxItemCode";
            this.cbxItemCode.SelectedIndex = -1;
            this.cbxItemCode.ShowCheckBox = false;
            this.cbxItemCode.Size = new System.Drawing.Size(169, 20);
            this.cbxItemCode.TabIndex = 12;
            this.cbxItemCode.WidthType = UserControl.WidthTypes.Normal;
            this.cbxItemCode.XAlign = 254;
            this.cbxItemCode.SelectedIndexChanged += new System.EventHandler(this.cbxItemCode_SelectedIndexChanged);
            // 
            // cbxModel
            // 
            this.cbxModel.AllowEditOnlyChecked = true;
            this.cbxModel.Caption = "产品别";
            this.cbxModel.Checked = false;
            this.cbxModel.Location = new System.Drawing.Point(15, 7);
            this.cbxModel.Name = "cbxModel";
            this.cbxModel.SelectedIndex = -1;
            this.cbxModel.ShowCheckBox = false;
            this.cbxModel.Size = new System.Drawing.Size(158, 20);
            this.cbxModel.TabIndex = 11;
            this.cbxModel.WidthType = UserControl.WidthTypes.Normal;
            this.cbxModel.XAlign = 62;
            this.cbxModel.SelectedIndexChanged += new System.EventHandler(this.cbxModel_SelectedIndexChanged);
            // 
            // panelDataInput
            // 
            this.panelDataInput.Controls.Add(this.panel3);
            this.panelDataInput.Controls.Add(this.panel1);
            this.panelDataInput.Controls.Add(this.label1);
            this.panelDataInput.Controls.Add(this.ucLEInput);
            this.panelDataInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDataInput.Location = new System.Drawing.Point(0, 101);
            this.panelDataInput.Name = "panelDataInput";
            this.panelDataInput.Size = new System.Drawing.Size(886, 37);
            this.panelDataInput.TabIndex = 24;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rbDelete);
            this.panel3.Controls.Add(this.rbAdd);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(440, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(143, 37);
            this.panel3.TabIndex = 11;
            // 
            // rbDelete
            // 
            this.rbDelete.Location = new System.Drawing.Point(59, 7);
            this.rbDelete.Name = "rbDelete";
            this.rbDelete.Size = new System.Drawing.Size(61, 23);
            this.rbDelete.TabIndex = 9;
            this.rbDelete.Text = "删除";
            // 
            // rbAdd
            // 
            this.rbAdd.Checked = true;
            this.rbAdd.Location = new System.Drawing.Point(7, 7);
            this.rbAdd.Name = "rbAdd";
            this.rbAdd.Size = new System.Drawing.Size(51, 23);
            this.rbAdd.TabIndex = 8;
            this.rbAdd.TabStop = true;
            this.rbAdd.Text = "新增";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.rbProduct);
            this.panel1.Controls.Add(this.rbMO);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(583, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(303, 37);
            this.panel1.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "入库对比方式";
            // 
            // rbProduct
            // 
            this.rbProduct.Location = new System.Drawing.Point(186, 7);
            this.rbProduct.Name = "rbProduct";
            this.rbProduct.Size = new System.Drawing.Size(86, 23);
            this.rbProduct.TabIndex = 1;
            this.rbProduct.Text = "按产品比对";
            // 
            // rbMO
            // 
            this.rbMO.Checked = true;
            this.rbMO.Location = new System.Drawing.Point(92, 7);
            this.rbMO.Name = "rbMO";
            this.rbMO.Size = new System.Drawing.Size(88, 23);
            this.rbMO.TabIndex = 0;
            this.rbMO.TabStop = true;
            this.rbMO.Text = "按工单比对";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "采集对象";
            // 
            // ucLEInput
            // 
            this.ucLEInput.Location = new System.Drawing.Point(75, 6);
            this.ucLEInput.Name = "ucLEInput";
            this.ucLEInput.Size = new System.Drawing.Size(240, 20);
            this.ucLEInput.TabIndex = 4;
            this.ucLEInput.Leave += new System.EventHandler(this.ucLEInput_Leave);
            this.ucLEInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLEInput_TxtboxKeyPress);
            this.ucLEInput.Enter += new System.EventHandler(this.ucLEInput_Enter);
            // 
            // panelDetail
            // 
            this.panelDetail.Controls.Add(this.gbxDetail);
            this.panelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetail.Location = new System.Drawing.Point(0, 138);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(886, 338);
            this.panelDetail.TabIndex = 25;
            // 
            // gbxDetail
            // 
            this.gbxDetail.Controls.Add(this.panel2);
            this.gbxDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxDetail.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbxDetail.Location = new System.Drawing.Point(0, 0);
            this.gbxDetail.Name = "gbxDetail";
            this.gbxDetail.Size = new System.Drawing.Size(886, 338);
            this.gbxDetail.TabIndex = 26;
            this.gbxDetail.TabStop = false;
            this.gbxDetail.Text = "入库明细";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panelGrid);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(880, 318);
            this.panel2.TabIndex = 5;
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.ultraGridContent);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 0);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(880, 318);
            this.panelGrid.TabIndex = 26;
            // 
            // ultraGridContent
            // 
            this.ultraGridContent.Cursor = System.Windows.Forms.Cursors.Default;
            this.ultraGridContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridContent.Location = new System.Drawing.Point(0, 0);
            this.ultraGridContent.Name = "ultraGridContent";
            this.ultraGridContent.Size = new System.Drawing.Size(880, 318);
            this.ultraGridContent.TabIndex = 6;
            this.ultraGridContent.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridContent_InitializeLayout);
            this.ultraGridContent.AfterRowActivate += new System.EventHandler(this.ultraGridContent_AfterRowActivate);
            // 
            // FNormalReceive
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(886, 547);
            this.Controls.Add(this.panelDetail);
            this.Controls.Add(this.panelDataInput);
            this.Controls.Add(this.pnlAbnormal);
            this.Controls.Add(this.panelReceiveNo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelBottom);
            this.Name = "FNormalReceive";
            this.Text = "生产入库采集";
            this.Load += new System.EventHandler(this.FNormalReceive_Load);
            this.Closed += new System.EventHandler(this.FNormalReceive_Closed);
            this.Activated += new System.EventHandler(this.FNormalReceive_Activated);
            this.panelBottom.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraOsType)).EndInit();
            this.panelReceiveNo.ResumeLayout(false);
            this.panelReceiveNo.PerformLayout();
            this.pnlAbnormal.ResumeLayout(false);
            this.panelDataInput.ResumeLayout(false);
            this.panelDataInput.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelDetail.ResumeLayout(false);
            this.gbxDetail.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridContent)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region 数据添加部分
		/// <summary>
		/// 输入序列呈或者是二维条码添加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		
		private bool CheckInput()
		{
			if ( this.ucLETicketNo.Text.Trim() == string.Empty )
			{
				this.ErrorMessage("$Error_CS_Input_StockIn_TicketNo");

                this.ucLETicketNo.Focus();

				return false;
			}	

			if ( this.ucLEInput.Text.Trim() == string.Empty )
			{
				return false;
			}

			return true;
		}
		private void AddData()
		{
			
			if(!this.CheckInput())
				return;

			//入库单号变化，重新load数据
			if(this._recNo != this.ucLETicketNo.Text.Trim())
			{
				this.LoadData();
			}
			try
			{	
				object[] result = null;
				if ( this.ultraOsType.CheckedItem.DataValue.ToString() == CollectionType.Planate.ToString())
				{
					#region 二维条码添加
					string[] idList = null;
					try
					{
						BarCodeParse barParser = new BarCodeParse(this._domainDataProvider);
					
						//如果比对二维条码中的产品别和用户选择的产品别
						if(this.chbModel.Checked)
						{
							if(!this.CheckModel(barParser))
								return;
						}
					
						idList = barParser.GetIDList( this.ucLEInput.Text.Trim() );
					
					}
					catch(System.Exception ex)
					{
						this.ErrorMessage(ex.Message);
						return;
					}
					if ( idList == null || idList.Length == 0)
					{
						this.ErrorMessage("$CS_RCard_List_Is_Empty");
						return;
					}

					this.DataProvider.BeginTransaction();

					//还个的序号添加，并判断是否已经存在
					foreach ( string id in idList )
					{
						result = AddInvRCard2(this.ucLETicketNo.Text.Trim(),id,ApplicationService.Current().UserCode,
							this.GetItemList(id),CollectionType.Planate,string.Empty);
					
						this.UpdateFormData(result);

						if(result != null && result.Length>= 2)
						{
							InvReceive rec = result[0] as InvReceive;
							InvRCard inv = result[1] as InvRCard;
							if(rec != null && inv != null)
							{
								//if(this.IsActiveRow(rec.RecSeq.ToString()))
								//{
								//	this._tblRCard.Rows.Add(new object[]{inv.RunningCard,inv.MOCode,inv.RecCollectType});
								//}

								//this.AddRCardGridRow(inv);
							}
						}
							
					}
					#endregion
				}
				else if(this.ultraOsType.CheckedItem.DataValue.ToString() == CollectionType.Carton.ToString())
				{
					#region Carton
					BenQGuru.eMES.DataCollect.DataCollectFacade dc = new BenQGuru.eMES.DataCollect.DataCollectFacade(this.DataProvider);

					object[] idList = dc.GetSimulationFromCarton(this.ucLEInput.Text.Trim().ToUpper());
					if ( idList == null || idList.Length == 0)
					{
						this.ErrorMessage("$CS_RCard_List_Is_Empty");
						return;
					}

					this.DataProvider.BeginTransaction();

					//一个个的序号添加，并判断是否已经存在
					foreach (BenQGuru.eMES.Domain.DataCollect.Simulation sim in idList )
					{
						result = AddInvRCard2(this.ucLETicketNo.Text.Trim(),sim.RunningCard,ApplicationService.Current().UserCode,
							this.GetItemList(sim.RunningCard),CollectionType.Carton,this.ucLEInput.Text.Trim().ToUpper());
					
						this.UpdateFormData(result);

						if(result != null && result.Length>= 2)
						{
							InvReceive rec = result[0] as InvReceive;
							InvRCard inv = result[1] as InvRCard;
							if(rec != null && inv != null)
							{
								//this.AddRCardGridRow(inv);
							}
						}
							
					}
					#endregion
				}
				else if(this.ultraOsType.CheckedItem.DataValue.ToString() == CollectionType.PCS.ToString() )
				{
					this.DataProvider.BeginTransaction();

					#region 序列号添加
					string id = this.ucLEInput.Text.Trim().ToUpper();
					id = id.Substring(0, Math.Min(40,id.Length));
					
					BenQGuru.eMES.DataCollect.DataCollectFacade dc = new BenQGuru.eMES.DataCollect.DataCollectFacade(this.DataProvider);
					BenQGuru.eMES.Domain.DataCollect.Simulation sim = dc.GetSimulation(id) as BenQGuru.eMES.Domain.DataCollect.Simulation;

					string cartoncode = string.Empty;
					if(sim != null)
						cartoncode = sim.CartonCode;

					result = AddInvRCard2(this.ucLETicketNo.Text.Trim(),id,ApplicationService.Current().UserCode,
						this.GetItemList(id),CollectionType.PCS,cartoncode);	
					
					//更新 界面上的数据
					this.UpdateFormData(result);

					InvRCard inv = result[1] as InvRCard;
					//if(inv != null)
					//this._tblRCard.Rows.Add(new object[]{inv.RunningCard,inv.MOCode});
					//if(inv!= null)
					//	this.AddRCardGridRow(inv);	
					#endregion
				}
					
				if(this.ultraOsType.CheckedItem.DataValue.ToString() == CollectionType.Carton.ToString())
				{
					this.txtCartonNum.Value = (int.Parse(this.txtCartonNum.Value)+1).ToString();
				}
				this.SucessMessage("$CS_Add_Success");

                ucLEInput.Focus();

				this.DataProvider.CommitTransaction();
			}
			catch( Exception ex )
			{
				this.DataProvider.RollbackTransaction();
				this.ErrorMessage( ex.Message );

				this.LoadData();
                this.ucLEInput.Focus();
			}
			finally
			{
				this.ucLEInput.Text = "";
                ucLEInput.Focus();
				CloseConnection();
			}
		}


		private bool IsActiveRow(string seq)
		{
			bool ret = false;
//			foreach(Infragistics.Win.UltraWinGrid.UltraGridRow row in this.ultraGridContent.Rows)
			for ( int iGridRowLoopIndex = 0; iGridRowLoopIndex < this.ultraGridContent.Rows.Count; iGridRowLoopIndex++)
			{
				Infragistics.Win.UltraWinGrid.UltraGridRow row = this.ultraGridContent.Rows[iGridRowLoopIndex];
				if(row.Cells["SEQ"].Text == seq)
				{
					if(row.Activated)
					{
						return true;
					}
				}
			}

			return ret;
		}

		//输入框回车
		private void ucLEInput_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( e.KeyChar == '\r' )
			{	
				//Modified By Karron Qiu,2006-7-21

				if(rbAdd.Checked)
				{
					//DateTime now = DateTime.Now;
					AddData();
					//System.TimeSpan span = DateTime.Now - now;
					//this.ErrorMessage("所有运行时间" + span.Seconds.ToString() + "|" + span.Milliseconds.ToString());
				}
				else if(rbDelete.Checked)
				{
					btnDeleteRCard_Click(null,null);
				}
			}
		}

		//新增按钮
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			AddData();
		}

		//向数据库中，增加一个新的入库RCard,同进更新Receive
		protected object[] AddInvRCard2(string recNo,string rcard,string user,SimulateResult sr,CollectionType c_type,string cartonno)
		{
			InvReceive rec = GetInvReceive(recNo,sr);
			this.CheckMoOrItem(rec,sr);
			
			return _facade.AddInvRCard2(rec,rcard,user,sr,c_type,cartonno);
		}

		protected virtual void CheckMoOrItem(InvReceive rec,SimulateResult sr)
		{
			if(rec == null && rbMO.Checked)
				throw new Exception(sr.RunningCard +" $Error_Inv_Mo_Error" +" $Error_Inv_Rcard_Mo "+ sr.MOCode);//没有找到可用的入库单明细
			else if(rec == null && rbProduct.Checked)
				throw new Exception(sr.RunningCard +" $Error_Inv_Product_Error" +" $Error_Inv_Rcard_Product "+ sr.ItemCode);//没有找到可用的入库单明细
		}
		protected InvReceive GetInvReceive(string recNo,SimulateResult sr)
		{
			DataRow[] drList = this.GetDetailGridRow(recNo,sr);
			if(drList != null && drList.Length > 0)
			{
				InvReceive rec = new InvReceive();
				rec.RecNo = recNo;
				rec.MoCode = drList[0]["MoCode"].ToString();
				rec.RecStatus = drList[0]["status"].ToString();
				rec.RecSeq = int.Parse(drList[0]["SEQ"].ToString());
				rec.ItemCode = drList[0]["ItemCode"].ToString();
				rec.ModelCode = drList[0]["ModelCode"].ToString();
				rec.ActQty = int.Parse(drList[0]["ActQty"].ToString());
				rec.PlanQty = int.Parse(drList[0]["PlanQty"].ToString());
				return rec;
			}
			else
				return null;
		}

		protected virtual DataRow[] GetDetailGridRow(string recno,SimulateResult sr)
		{
			//Karron Qiu,2006-7-26

			//      在生产入库采集界面增加两个Radio Button，如下图所示。
			//按工单比对方式也就是目前系统的生产入库处理方式。此时，入库单中的各细项都必须有工单资料，
			//输入产品序列号或Carton箱号后，按照产品序列号当前对应的工单号码与入库单各细项进行匹配，
			//并更新相应实际入库数量；按产品比对方式是系统原来的入库处理。无论入库单细项资料中是否维护了工单号码，
			//用户采集产品序列号或Carton箱号后，一律按照产品序列号对应的产品代码与入库单中各细项进行匹配，
			//并更新相应实际入库数量。无论是否按工单比对，生产入库采集资料都保存每个产品序列号的工单代码。

			if(rbMO.Checked)
			{
				return _tmpTable.Select(string.Format("NO='{0}' and MoCode='{1}'",recno,sr.MOCode));
			}
			else if(rbProduct.Checked)
			{
				 return _tmpTable.Select(string.Format("NO='{0}' and ItemCode='{1}'",recno,sr.ItemCode));
			}

			return null;
		}

		#endregion

		#region 数据显示部分
		
		private void LoadData()
		{
			if(this.ucLETicketNo.Text.Trim() == string.Empty)
				return;

			Infragistics.Win.UltraWinGrid.UltraGridRow row = this.ultraGridContent.DisplayLayout.ActiveRow;
			if(row != null)
			{
				this._currseq = row.Cells["SEQ"].Text;
			}
			else
			{
				this._currseq = "0";
			}

			this._recNo = this.ucLETicketNo.Text.Trim();

			txtSumNum.InnerTextBox.Text = "0";
			int num = 0;
			this._tmpTable.Rows.Clear();
			BindGrid();
			//this._tblRCard.Rows.Clear();
			//this.BindRCard();

			this.txtCartonNum.Value = _facade.GetCartonCount(this.ucLETicketNo.Text.Trim().ToUpper()).ToString();
			//查询出库单
			object[] objs = _facade.QueryInvReceive(this.ucLETicketNo.Text.Trim());
			if(objs != null && objs.Length > 0)
			{
				foreach(object obj in objs)
				{
					InvReceive rec = obj as InvReceive;
					if(rec != null)
					{
						this.AddGridRow(rec);
						num += rec.ActQty;
					}
				}
			}
			else
			{
				this.ErrorMessage("$Error_ReceiveNo");//入库单号输入错误
			}
			txtSumNum.InnerTextBox.Text = num.ToString();

			BindModel();

			if(this._currseq != "0")
				this.ActivateRow(this._currseq);

		}

		/// <summary>
		/// 输入新的入库单查询
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ucLETicketNo_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( e.KeyChar == '\r' )
			{
				LoadData();

                this.ucLEInput.Focus();
			}
		}
	
		
		private void UpdateFormData(object[] ret)
		{
			if(ret == null) 
			{
				this.LoadData();
				return;
			}
			InvReceive rec = ret[0] as InvReceive;

			if(rec == null) //如果当前行已经其它人被删除了，则重新load入库单
				this.LoadData();
			else
			{
				int n = 0;
				DataRow[] drList = this.GetDetailGridRow(rec);
				if(drList != null && drList.Length > 0)
				{
					DataRow dr = drList[0];
					n = rec.ActQty - int.Parse(dr["ActQty"].ToString());
					dr = drList[0];
					if(rec != null && rec.ActQty > 0)
						dr["ActQty"] = rec.ActQty;
					else
						dr["ActQty"] = 0;

					if(dr != null)
						ActivateRow(dr["SEQ"].ToString());  //如果当前行不是Active的行，则重新激活
				}

				this.txtSumNum.InnerTextBox.Text = (int.Parse(this.txtSumNum.InnerTextBox.Text)  + n).ToString();
			}
		}

		protected virtual DataRow[] GetDetailGridRow(InvReceive rec)
		{
			if(rbMO.Checked)
				return _tmpTable.Select(string.Format("MoCode='{0}'",rec.MoCode));
			else if(rbProduct.Checked)
				return _tmpTable.Select(string.Format("ItemCode='{0}'",rec.ItemCode));

			return null;
		}

		private void DeleteRCardGrid(string rcard)
		{
//			DataRow[] drList = this._tblRCard.Select(string.Format("RCard='{0}'",rcard));
//			if(drList != null && drList.Length > 0)
//			{
//				foreach(DataRow dr in drList)
//				{
//					dr.Delete();
//				}
//			}
		}
		private void ActivateRow(string seq)
		{
//			foreach(Infragistics.Win.UltraWinGrid.UltraGridRow row in this.ultraGridContent.Rows)
			for ( int iGridRowLoopIndex = 0; iGridRowLoopIndex < this.ultraGridContent.Rows.Count; iGridRowLoopIndex++)
			{
				Infragistics.Win.UltraWinGrid.UltraGridRow row = this.ultraGridContent.Rows[iGridRowLoopIndex];
				if(row.Cells["SEQ"].Text == seq)
				{
					if(!row.Activated)
						row.Activated = true;
				}
				else
				{
					row.Activated = false;
					row.Selected = false;
				}
			}
		}
		private DataRow AddGridRow(InvReceive rec)
		{
			DataRow dr = _tmpTable.Rows.Add(new object[]
											{
												false,
												rec.RecNo,
												rec.RecSeq,
												rec.MoCode,
												rec.ModelCode,
												rec.ItemCode,
												rec.ItemDesc,
												rec.PlanQty,
												rec.ActQty,
												rec.RecStatus
											}
								);
			return dr;
		}
	

//		private DataRow AddRCardGridRow(InvRCard inv)
//		{
//			DataRow[] drList = this._tblRCard.Select(string.Format("RCard='{0}'",inv.RunningCard));
//			if(drList == null || drList.Length ==0)
//			{
//				DataRow dr = this._tblRCard.Rows.Add(new object[]
//											{
//												inv.MOCode,
//												inv.RunningCard,
//												inv.RecCollectType,
//												inv.RecSeq
//											}
//					);
//				return dr;
//			}
//			else
//				return drList[0];
//		}

		private void ultraGridContent_AfterRowActivate(object sender, System.EventArgs e)
		{
//			//this._tblRCard.Rows.Clear();
//			
//			Infragistics.Win.UltraWinGrid.UltraGridRow row = this.ultraGridContent.DisplayLayout.ActiveRow;
//			if(row != null)
//			{
//				string recNo = row.Cells["NO"].Text;
//				int recSeq = int.Parse(row.Cells["SEQ"].Text);
//
//				System.Data.DataView dv = new System.Data.DataView(this._tblRCard,"SEQ=" + recSeq.ToString(),string.Empty,System.Data.DataViewRowState.CurrentRows);
//				
//				//记录数是０时，重新从数据库中load
//				if(dv.Count == 0)
//				{
//					object[] objsRCard = _facade.QueryInvRCard(recNo,recSeq);
//					if(objsRCard != null)
//					{
//						foreach(object obj in objsRCard)
//						{
//							InvRCard inv = obj as InvRCard;
//							if(inv != null)
//							{
//								//this._tblRCard.Rows.Add(new object[]{inv.RunningCard,inv.MOCode,inv.RecCollectType});
//
//								this.AddRCardGridRow(inv);
//							}	
//						}
//					}
//
//					dv = new System.Data.DataView(this._tblRCard,"SEQ=" + recSeq.ToString(),string.Empty,System.Data.DataViewRowState.CurrentRows);
//				}
//
//				this.BindRCard(dv);
//			}
		}

		#endregion

		#region 数据删除部分
		
		//删除一个序列号
		private void btnDeleteRCard_Click(object sender, System.EventArgs e)
		{
			if(!this.CheckInput())
				return;

			string[] idList = null;
			if ( this.ultraOsType.CheckedItem.DataValue.ToString() == CollectionType.Planate.ToString())
			{
				#region 二维条码添加
				
				#endregion
			}
			else if(this.ultraOsType.CheckedItem.DataValue.ToString() == CollectionType.Carton.ToString())
			{
				#region Carton
				BenQGuru.eMES.DataCollect.DataCollectFacade dc = new BenQGuru.eMES.DataCollect.DataCollectFacade(this.DataProvider);
				object[] objidList = dc.GetSimulationFromCarton(this.ucLEInput.Text.Trim().ToUpper());
				if ( objidList == null || objidList.Length == 0)
				{
					this.ErrorMessage("$CS_RCard_List_Is_Empty");
					return;
				}
				else
				{
					idList = new string[objidList.Length];
					int i = 0;
					foreach (BenQGuru.eMES.Domain.DataCollect.Simulation sim in objidList )
					{
						idList[i++] = sim.RunningCard;
					}
				}
				#endregion
			}
			else if(this.ultraOsType.CheckedItem.DataValue.ToString() == CollectionType.PCS.ToString() )
			{
				idList = new string[]{this.ucLEInput.Text.Trim().ToUpper()};
			}

			try
			{
				this.DataProvider.BeginTransaction();

				bool isDelete = false;

				foreach(string rcard in idList)
				{
					object[] ret =_facade.RemoveReceiveRCard(this._recNo,rcard,this.ultraOsType.CheckedItem.DataValue.ToString());
					

					this.UpdateFormData(ret);
					if(ret[1] != null)
						isDelete = true;
				}

				this.ultraGridContent_AfterRowActivate(null,null);

				if(isDelete)
				{
					if(this.ultraOsType.CheckedItem.DataValue.ToString() == CollectionType.Carton.ToString())
					{
						this.txtCartonNum.Value = (int.Parse(this.txtCartonNum.Value)-1).ToString();
					}
					this.SucessMessage("$CS_Delete_Success");
				}
				this.DataProvider.CommitTransaction();	
			}
			catch(System.Exception ex)
			{
				this.DataProvider.RollbackTransaction();
				this.ErrorMessage(ex.Message);
				this.LoadData();
			}
			finally
			{
				this.ucLEInput.Text = string.Empty;
				CloseConnection();
			}
		}


		//将当前明细下的所有RCard删除
		private void DeleteRowRCardAll(string seq)
		{
//			DataRow[] drList = this._tblRCard.Select("SEQ="+seq);
//			if(drList != null && drList.Length > 0)
//			{
//				foreach(DataRow dr in drList)
//				{
//					dr.Delete();
//				}
//			}
		}
		//删除一个明细
		private void btnDeleteDetail_Click(object sender, System.EventArgs e)
		{
			bool isDeleted = false;
			try
			{
				this.DataProvider.BeginTransaction();
//				foreach(Infragistics.Win.UltraWinGrid.UltraGridRow row in this.ultraGridContent.Rows)
				for ( int iGridRowLoopIndex = 0; iGridRowLoopIndex < this.ultraGridContent.Rows.Count; iGridRowLoopIndex++)
				{
					Infragistics.Win.UltraWinGrid.UltraGridRow row = this.ultraGridContent.Rows[iGridRowLoopIndex];
					if(row.Cells["*"].Text.ToLower() == "true")
					{
						string recNo = row.Cells["NO"].Text;
						int seq = int.Parse(row.Cells["SEQ"].Text);
						
						object[] ret = _facade.RemoveReceiveAllRCard(recNo,seq);

						//更新界面显示
						DeleteRowRCardAll(seq.ToString());
						this.UpdateFormData(ret);
						
						isDeleted = true;
					}

				}

				this.DataProvider.CommitTransaction();
				if(isDeleted)
				{
					this.txtCartonNum.Value = _facade.GetCartonCount(this.ucLETicketNo.Text.Trim().ToUpper()).ToString();
					this.ultraGridContent_AfterRowActivate(null,null);
					this.SucessMessage("$CS_Delete_Success");
				}
			}
			catch(System.Exception ex)
			{
				this.DataProvider.RollbackTransaction();
				this.ErrorMessage(ex.Message);
				this.LoadData();
			}
		}
		
		#endregion

		#region 执行完成入库的动作
		private void ucBtnComplete_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.DataProvider.BeginTransaction();

				_facade.CompleteInvReceive(this.ucLETicketNo.Text.Trim(),
					ApplicationService.Current().UserCode);	

				foreach(DataRow dr in this._tmpTable.Rows)
				{
					dr["status"] = ReceiveStatus.Received;
				}
				this.DataProvider.CommitTransaction();
				
				this.SucessMessage("$Receive_Sucess");//完成入库单成功
			}
			catch(System.Exception ex)
			{
				this.DataProvider.RollbackTransaction();
				this.ErrorMessage(ex.Message);
				this.LoadData();
			}
			finally
			{
				CloseConnection();
			}
		}
		
		//取消完成
		private void btnUnComplete_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.DataProvider.BeginTransaction();

				_facade.UnCompleteInvReceive(this.ucLETicketNo.Text.Trim(),
					ApplicationService.Current().UserCode);	

				foreach(DataRow dr in this._tmpTable.Rows)
				{
					dr["status"] = ReceiveStatus.Receiving;
				}
				this.DataProvider.CommitTransaction();
				
				this.SucessMessage("$Ship_UnCompleted");
			}
			catch(System.Exception ex)
			{
				this.DataProvider.RollbackTransaction();
				this.ErrorMessage(ex.Message);
				this.LoadData();
			}
			finally
			{
				CloseConnection();
			}
		}
		#endregion

		#region 提供继承用的方法
		protected virtual void InitControl()
		{
			this.pnlAbnormal.Hide();
			this.gbxDetail.Dock = System.Windows.Forms.DockStyle.Fill;
		}

		protected virtual bool CheckModel(BarCodeParse barParser)
		{
			return true;
		}
		protected virtual void BindModel()
		{

		}

		protected virtual void BindItemCode()
		{

		}

		protected virtual void SetItemDesc()
		{

		}

		protected virtual SimulateResult GetItemList(string rcard)
		{
			return _facade.GetItemList(rcard);
		}

		#region OLD
//		protected virtual SimulateResult GetItemList_OLD(string rcard)
//		{
//			ArrayList retList = new ArrayList();
//			BenQGuru.eMES.MOModel.MOFacade moFacade = new MOFacade(this.DataProvider);
//			//先从MORCard中取值，再
//			
//			#region 先从MORCard中取出满足条件的ＲＣＡＲＤ
//			object[] objRCards = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.MORunningCard),
//				new SQLCondition(string.Format("select * from tblmorcard where morcardstart='{0}'",rcard)));
//
//			if(objRCards != null && objRCards.Length > 0)
//			{
//				foreach(object obj in objRCards)
//				{
//					BenQGuru.eMES.Domain.MOModel.MORunningCard mr = obj as BenQGuru.eMES.Domain.MOModel.MORunningCard;
//					if(mr != null)
//					{
//						SimulateResult sr = new SimulateResult();
//						sr.IsCompleted = false;
//						sr.IsInv = true;
//						sr.MOCode = mr.MOCode;
//						sr.RunningCard = rcard;
//						BenQGuru.eMES.Domain.MOModel.MO mo = moFacade.GetMO(mr.MOCode) as BenQGuru.eMES.Domain.MOModel.MO ;
//						if(mo != null)
//						{
//							sr.ItemCode = mo.ItemCode;
//							sr.IsClosed = (mo.MOStatus == BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_CLOSE);
//							sr.IsCompleted = sr.IsClosed;
//							retList.Add(sr);
//						}
//					}
//				}
//			}
//			if(retList.Count == 0)
//				throw new Exception(rcard + "提示产品序列号没有生产信息");
//
//			#endregion
//
//			#region 到RunningCard中判断是否完工
//			bool somecomplete = false;
//			for(int i=0;i<retList.Count; i++)
//			{
//				SimulateResult sr = retList[i] as SimulateResult;
//				if(IsCompletedOnLine(sr.RunningCard,sr.MOCode))
//				{
//					sr.IsCompleted = true;
//					somecomplete = true;
//				}
//				else
//					sr.IsCompleted = false;
//			}
//
//			//不存在序列号，或者几个中没有一个完工
//			if(retList.Count == 0 || !somecomplete)
//				throw new Exception(rcard + " 产品序列号还没有完工");
//
//			#endregion
//
//			#region 到入库资料中判断是否已经入库
//			bool some_not_inv = false;
//			for(int i=0;i<retList.Count; i++)
//			{
//				SimulateResult sr = retList[i] as SimulateResult;
//				if(IsInv(sr.RunningCard,sr.MOCode))
//				{
//					sr.IsInv = true;
//					somecomplete = true;
//				}
//				else
//				{
//					sr.IsInv = false;
//					some_not_inv = true;
//				}
//			}
//
//			if(!some_not_inv)
//				throw new Exception(rcard + "已经入库了");
//			#endregion
//		
//			#region 找到任何一个已经完工，并且还没有入库的RCard返回
//			for(int i=0;i<retList.Count; i++)
//			{
//				SimulateResult sr = retList[i] as SimulateResult;
//				if(sr.IsCompleted && !sr.IsInv)
//				{
//					return sr;
//				}
//			}
//
//			return null;
//
//			#endregion
//		}

//		//检查ＲＣＡＲＤ是否已经完工
//		private bool IsCompletedOnLine(string rcard,string mocode)
//		{
//			object[] objs= this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.DataCollect.Simulation),
//				new SQLCondition(string.Format("select * from TBLSIMULATION where RCARD = '{0}' and mocode = '{1}'",
//				rcard,mocode))
//				);
//			
//			if(objs == null) //产线上没有，证明已经完工
//				return true;
//
//			if(objs != null && objs.Length > 0)
//			{
//				BenQGuru.eMES.Domain.DataCollect.Simulation sim = objs[0] as BenQGuru.eMES.Domain.DataCollect.Simulation;
//				if(sim.IsComplete == "1" &&	sim.ProductStatus == "GOOD")
//					return true;
//				else
//					return false;
//			}
//			else
//				return false;
//		}
		#endregion
		private void cbxModel_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.BindItemCode();
		}
		
		private void cbxItemCode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.SetItemDesc();
		}

		#endregion	

		private void btnToss_Click(object sender, System.EventArgs e)
		{
			FTossDarfonERP frm = new FTossDarfonERP();
			frm.ShowDialog();
		}
		//Laws Lu,2006/12/25 焦点进入背景色变为浅绿，移出恢复正常
		private void ucLETicketNo_Enter(object sender, System.EventArgs e)
		{
			ucLETicketNo.BackColor =  Color.GreenYellow;
		}

		private void ucLETicketNo_Leave(object sender, System.EventArgs e)
		{
			ucLETicketNo.BackColor =  Color.White;
		}

		private void ucLEInput_Enter(object sender, System.EventArgs e)
		{
			ucLEInput.BackColor =  Color.GreenYellow;
		}

		private void ucLEInput_Leave(object sender, System.EventArgs e)
		{
			ucLEInput.BackColor =  Color.White;
		}

		private void FNormalReceive_Activated(object sender, System.EventArgs e)
		{
            ucLETicketNo.Focus();
			ucLETicketNo.BackColor = Color.GreenYellow;
		}
	}
}
